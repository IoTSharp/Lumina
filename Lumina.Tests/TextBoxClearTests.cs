using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxClearTests
{
    [Fact]
    public void ClearHasStandardNonvirtualBaseSignature()
    {
        MethodInfo method = typeof(TextBox).GetMethod(nameof(TextBoxBase.Clear))!;
        Assert.Equal(typeof(TextBoxBase), method.DeclaringType);
        Assert.True(method.IsPublic);
        Assert.False(method.IsVirtual);
        Assert.Equal(typeof(void), method.ReturnType);
        Assert.Empty(method.GetParameters());
    }

    [Fact]
    public void ClearAlwaysPassesNullToVirtualTextWithoutReadingTextOrCreatingHandle()
        => OnSta(() =>
        {
            using var textBox = new ObservingTextBox { BypassTextSetter = true, ObserveText = true };
            TextBoxBase baseReference = textBox;

            baseReference.Clear();
            baseReference.Clear();

            Assert.Equal(new string?[] { null, null }, textBox.TextWrites);
            Assert.Equal(0, textBox.TextReads);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void ClearPreservesOriginalVirtualSetterFailureAndPreviousText()
        => OnSta(() =>
        {
            using var textBox = new ObservingTextBox { Text = "before", ObserveText = true };
            var failure = new InvalidOperationException("clear text setter");
            textBox.TextWriteFailure = failure;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.Clear()));

            Assert.Equal(new string?[] { null }, textBox.TextWrites);
            Assert.Equal("before", textBox.StoredText);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void ClearAllowsFiniteReentryThroughTheVirtualTextSetter()
        => OnSta(() =>
        {
            using var textBox = new ObservingTextBox { BypassTextSetter = true, ObserveText = true };
            textBox.AfterTextWrite = () =>
            {
                if (textBox.TextWrites.Count == 1) { textBox.Clear(); }
            };

            textBox.Clear();

            Assert.Equal(new string?[] { null, null }, textBox.TextWrites);
            Assert.Equal(0, textBox.TextReads);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData("", 0)]
    [InlineData("甲\r\n\U0001F600", 1)]
    public void ClearNormalizesPendingTextAndNotifiesOnlyForAChange(string initialText, int expectedChanges)
        => OnSta(() =>
        {
            using var textBox = new TextBox { Text = initialText };
            int changes = 0;
            textBox.TextChanged += (_, _) => changes++;

            textBox.Clear();
            textBox.Clear();

            Assert.Equal(string.Empty, textBox.Text);
            Assert.Equal(expectedChanges, changes);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void ClearDoesNotOverwriteTextAssignedByAReentrantTextChangedHandler()
        => OnSta(() =>
        {
            using var textBox = new TextBox { Text = "before" };
            var observedTexts = new List<string>();
            textBox.TextChanged += (_, _) =>
            {
                observedTexts.Add(textBox.Text);
                if (observedTexts.Count == 1) { textBox.Text = "from handler"; }
            };

            textBox.Clear();

            Assert.Equal(new[] { string.Empty, "from handler" }, observedTexts);
            Assert.Equal("from handler", textBox.Text);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void ClearPreservesOriginalTextChangedFailureAfterCommittingEmptyText()
        => OnSta(() =>
        {
            using var textBox = new TextBox { Text = "before" };
            var failure = new InvalidOperationException("clear text changed");
            textBox.TextChanged += (_, _) => throw failure;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.Clear()));

            Assert.Equal(string.Empty, textBox.Text);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ClearResetsTheWholeNativeEditAndUndoBufferWithoutRecreatingHandle(bool readOnly)
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new TextBox { Text = "abcd", MaxLength = 64 };
            form.Controls.Add(textBox);
            _ = form.Handle;
            nint handle = textBox.Handle;
            Assert.Equal(form.Handle, GetParent(handle));
            Assert.Same(form, Control.FromHandle(GetParent(handle)));
            _ = SendMessageW(handle, 0x00B1, 1, 2); // EM_SETSEL
            _ = SendMessageW(handle, 0x00C2, -1, "X"); // EM_REPLACESEL
            Assert.Equal("aXcd", textBox.Text);
            Assert.True(textBox.CanUndo);
            textBox.ReadOnly = readOnly;
            textBox.Select(1, 1);
            int changes = 0;
            textBox.TextChanged += (_, _) => changes++;

            textBox.Clear();

            Assert.Equal(handle, textBox.Handle);
            Assert.Equal(string.Empty, textBox.Text);
            var nativeText = new StringBuilder(16);
            Assert.Equal(0, GetWindowTextW(handle, nativeText, nativeText.Capacity));
            Assert.Equal(string.Empty, nativeText.ToString());
            Assert.Equal(readOnly, textBox.ReadOnly);
            Assert.Equal(64, textBox.MaxLength);
            Assert.False(textBox.CanUndo);
            Assert.False(textBox.Modified);
            Assert.Equal(1, changes);
            textBox.Clear();
            Assert.Equal(1, changes);
        });

    private static void OnSta(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            try { action(); }
            catch (Exception exception) { failure = exception; }
            finally
            {
                try
                {
                    Application.ExitThread();
                    typeof(Application).GetMethod("DisposeThemedCommonControlsActivation",
                        BindingFlags.Static | BindingFlags.NonPublic)!.Invoke(null, null);
                }
                catch (Exception exception)
                {
                    failure = failure is null ? exception : new AggregateException(failure, exception);
                }
            }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox clear test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class ObservingTextBox : TextBox
    {
        internal bool ObserveText { get; set; }
        internal bool BypassTextSetter { get; set; }
        internal int TextReads { get; private set; }
        internal List<string?> TextWrites { get; } = new();
        internal Exception? TextWriteFailure { get; set; }
        internal Action? AfterTextWrite { get; set; }
        internal string StoredText => base.Text;

        [AllowNull]
        public override string Text
        {
            get
            {
                if (ObserveText) { TextReads++; }
                return base.Text;
            }
            set
            {
                if (ObserveText)
                {
                    TextWrites.Add(value);
                    if (TextWriteFailure is not null) { throw TextWriteFailure; }
                    AfterTextWrite?.Invoke();
                }
                if (!BypassTextSetter) { base.Text = value; }
            }
        }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetParent(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern int GetWindowTextW(nint hWnd, StringBuilder text, int maxCount);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, string lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, nint lParam);
}
