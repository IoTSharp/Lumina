using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxMultilineLifecycleTests
{
    private const int GwlStyle = -16;
    private const int EsMultiline = 0x0004;
    private const int EsReadOnly = 0x0800;
    private const int EmGetModify = 0x00B8;
    private const int EmSetModify = 0x00B9;

    [Fact]
    public void MultilineBeforeCreationNotifiesOnlyChangesWithoutCreatingHandle()
        => OnStaThread(() =>
        {
            using var textBox = new TextBox();
            var values = new List<bool>();
            textBox.MultilineChanged += (_, _) => values.Add(textBox.Multiline);

            textBox.Multiline = false;
            textBox.Multiline = true;
            textBox.Multiline = true;
            textBox.Multiline = false;

            Assert.Equal(new[] { true, false }, values);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void MultilineChangeRecreatesRealEditBeforeChangedEventAndSameValueDoesNothing()
        => OnStaThread(() =>
        {
            using var textBox = new TextBox { Text = "alpha beta" };
            nint firstHandle = textBox.Handle;
            var events = new List<string>();
            textBox.HandleDestroyed += (_, _) => events.Add("destroyed");
            textBox.HandleCreated += (_, _) => events.Add("created");
            textBox.MultilineChanged += (_, _) =>
            {
                Assert.True(textBox.IsHandleCreated);
                Assert.NotEqual(0, GetWindowLongW(textBox.Handle, GwlStyle) & EsMultiline);
                events.Add("changed");
            };

            textBox.Multiline = true;

            Assert.NotEqual(firstHandle, textBox.Handle);
            Assert.False(IsWindow(firstHandle));
            Assert.Equal(new[] { "destroyed", "created", "changed" }, events);
            nint secondHandle = textBox.Handle;
            textBox.Multiline = true;
            Assert.Equal(secondHandle, textBox.Handle);
            Assert.Equal(3, events.Count);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void MultilineRoundTripPreservesTextSelectionReadOnlyAndNativeModifiedBit(bool readOnly)
        => OnStaThread(() =>
        {
            using var textBox = new TextBox { Text = "alpha beta", ReadOnly = readOnly };
            _ = textBox.Handle;
            textBox.Select(2, 5);
            _ = SendMessageW(textBox.Handle, EmSetModify, 1, 0);
            int changes = 0;
            textBox.TextChanged += (_, _) => changes++;

            textBox.Multiline = true;

            Assert.Equal("alpha beta", textBox.Text);
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(5, textBox.SelectionLength);
            Assert.NotEqual(0, SendMessageW(textBox.Handle, EmGetModify, 0, 0));
            Assert.Equal(readOnly, (GetWindowLongW(textBox.Handle, GwlStyle) & EsReadOnly) != 0);
            textBox.Select(1, 3);
            _ = SendMessageW(textBox.Handle, EmSetModify, 0, 0);

            textBox.Multiline = false;

            Assert.Equal(0, GetWindowLongW(textBox.Handle, GwlStyle) & EsMultiline);
            Assert.Equal("alpha beta", textBox.Text);
            Assert.Equal(1, textBox.SelectionStart);
            Assert.Equal(3, textBox.SelectionLength);
            Assert.Equal(0, SendMessageW(textBox.Handle, EmGetModify, 0, 0));
            Assert.Equal(0, changes);
        });

    [Fact]
    public void SelectionSetBeforeCreationIsAppliedToFirstEditAndSubsequentRecreation()
        => OnStaThread(() =>
        {
            using var textBox = new TextBox { Text = "ab\U0001F600cdef" };
            textBox.Select(2, 3);
            Assert.False(textBox.IsHandleCreated);
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(3, textBox.SelectionLength);

            _ = textBox.Handle;
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(3, textBox.SelectionLength);
            textBox.Multiline = true;
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(3, textBox.SelectionLength);
        });

    [Fact]
    public void PendingSelectionSurvivesTemporaryTextShorteningWithoutCreatingHandle()
        => OnStaThread(() =>
        {
            using var textBox = new TextBox { Text = "abcdefghij" };
            textBox.Select(4, 3);
            textBox.Text = "ab";
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(0, textBox.SelectionLength);
            textBox.Text = "abcdefghij";
            Assert.Equal(4, textBox.SelectionStart);
            Assert.Equal(3, textBox.SelectionLength);
            Assert.False(textBox.IsHandleCreated);

            _ = textBox.Handle;
            Assert.Equal(4, textBox.SelectionStart);
            Assert.Equal(3, textBox.SelectionLength);
        });

    [Theory]
    [InlineData(2, int.MaxValue, 2, 4)]
    [InlineData(4, -2, 2, 2)]
    [InlineData(4, int.MinValue, 0, 4)]
    [InlineData(int.MaxValue, int.MaxValue, 6, 0)]
    [InlineData(8, -4, 4, 2)]
    public void DirectedSelectionAndOverflowRangesNormalizeAtRealEditCreation(int start, int length, int expectedStart, int expectedLength)
        => OnStaThread(() =>
        {
            using var textBox = new TextBox { Text = "abcdef" };
            textBox.Select(start, length);
            Assert.False(textBox.IsHandleCreated);
            _ = textBox.Handle;

            Assert.Equal(expectedStart, textBox.SelectionStart);
            Assert.Equal(expectedLength, textBox.SelectionLength);
            textBox.Multiline = true;
            Assert.Equal(expectedStart, textBox.SelectionStart);
            Assert.Equal(expectedLength, textBox.SelectionLength);
        });

    [Fact]
    public void NegativeSelectionPropertiesAndStartThrowWithoutCreatingHandle()
        => OnStaThread(() =>
        {
            using var textBox = new TextBox { Text = "abcdef" };

            Assert.Equal("start", Assert.Throws<ArgumentOutOfRangeException>(() => textBox.Select(-1, 2)).ParamName);
            Assert.Equal("value", Assert.Throws<ArgumentOutOfRangeException>(() => textBox.SelectionStart = -1).ParamName);
            Assert.Equal("value", Assert.Throws<ArgumentOutOfRangeException>(() => textBox.SelectionLength = -1).ParamName);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void SelectionPropertyUsesCurrentNativeRangeInsteadOfStaleCache()
        => OnStaThread(() =>
        {
            using var textBox = new TextBox { Text = "abcdefghij" };
            _ = textBox.Handle;
            _ = SendMessageW(textBox.Handle, 0x00B1, 2, 5);

            textBox.SelectionStart = 3;
            Assert.Equal(3, textBox.SelectionStart);
            Assert.Equal(3, textBox.SelectionLength);
            _ = SendMessageW(textBox.Handle, 0x00B1, 4, 7);
            textBox.SelectionLength = 2;
            Assert.Equal(4, textBox.SelectionStart);
            Assert.Equal(2, textBox.SelectionLength);
        });

    [Fact]
    public void MultilineChangedUsesStandardVirtualHookAndPreservesOriginalException()
        => OnStaThread(() =>
        {
            using var textBox = new HookTextBox();
            var failure = new InvalidOperationException("multiline callback");
            EventHandler handler = (_, _) => throw failure;
            textBox.MultilineChanged += handler;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.Multiline = true));
            Assert.True(textBox.Multiline);
            Assert.Equal(1, textBox.ChangedCalls);
            textBox.MultilineChanged -= handler;
            textBox.Multiline = false;
            Assert.Equal(2, textBox.ChangedCalls);
            Assert.False(textBox.IsHandleCreated);
        });

    private static void OnStaThread(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            try { action(); }
            catch (Exception exception) { failure = exception; }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox multiline lifecycle timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class HookTextBox : TextBox
    {
        internal int ChangedCalls { get; private set; }
        protected override void OnMultilineChanged(EventArgs e)
        {
            ChangedCalls++;
            base.OnMultilineChanged(e);
        }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern int GetWindowLongW(nint hWnd, int index);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint hWnd);
}
