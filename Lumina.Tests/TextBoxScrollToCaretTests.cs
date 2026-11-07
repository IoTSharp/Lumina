using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

// This scope exercises the ordinary EDIT path. It does not establish the
// RichEdit OLE/text-document scrolling branch or visible/device acceptance.
[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxScrollToCaretTests
{
    private readonly Xunit.Abstractions.ITestOutputHelper _output;

    public TextBoxScrollToCaretTests(Xunit.Abstractions.ITestOutputHelper output) => _output = output;

    private const int EmScrollCaret = 0x00B7;
    private const int EmSetSel = 0x00B1;
    private const int EmSetModify = 0x00B9;
    private const int EmLimitText = 0x00C5;
    private const int EmGetFirstVisibleLine = 0x00CE;
    private const int EmLineScroll = 0x00B6;

    [Fact]
    public void StandardMethodIsNonvirtualVoidDeclaredOnTextBoxBase()
    {
        MethodInfo method = typeof(TextBox).GetMethod(nameof(TextBox.ScrollToCaret))!;
        Assert.Equal(typeof(TextBoxBase), method.DeclaringType);
        Assert.True(method.IsPublic);
        Assert.False(method.IsStatic);
        Assert.False(method.IsVirtual);
        Assert.Equal(typeof(void), method.ReturnType);
        Assert.Empty(method.GetParameters());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    public void CallsBeforeCreationRemainLazyAndCoalesceIntoOneDeferredScroll(int calls)
        => OnSta(() =>
        {
            using var textBox = new ScrollTextBox { Text = "pending caret" };
            textBox.RejectManagedReads = true;
            // The finite inputs above bound this loop to at most five calls.
            for (int index = 0; index < calls; index++)
            {
                textBox.ScrollToCaret();
            }
            Assert.False(textBox.IsHandleCreated);
            Assert.Equal(0, textBox.CreatedNotifications);
            Assert.Equal(0, textBox.ScrollCount);
            textBox.RejectManagedReads = false;

            _ = textBox.Handle;

            Assert.True(textBox.IsHandleCreated);
            Assert.Equal(1, textBox.CreatedNotifications);
            Assert.Equal(1, textBox.ScrollCount);
            Assert.Equal((EmScrollCaret, (nint)0, (nint)0), Assert.Single(textBox.ScrollRequests));
        });

    [Fact]
    public void DeferredScrollRunsAfterHandleCreatedEventAndRestoredSelectionAndModifiedState()
        => OnSta(() =>
        {
            using var parent = new Control { Size = new Size(350, 180) };
            nint parentHandle = parent.Handle;
            using var textBox = new ScrollTextBox
            {
                Parent = parent, Multiline = true, Text = "0123456789",
                Size = new Size(260, 90), Modified = true
            };
            textBox.Select(2, 3);
            textBox.HandleCreated += (_, _) => textBox.Trace.Add("created-event");
            textBox.ScrollToCaret();

            nint handle = textBox.Handle;

            Assert.Equal(parentHandle, GetParent(handle));
            Assert.Equal(1, textBox.ScrollCount);
            int scroll = textBox.Trace.IndexOf("scroll");
            Assert.True(textBox.Trace.IndexOf("created-event") < scroll);
            Assert.True(textBox.Trace.IndexOf("limit") < scroll);
            Assert.True(textBox.Trace.IndexOf("modified") < scroll);
            Assert.True(textBox.Trace.LastIndexOf("selection") < scroll);
            Assert.Contains((EmSetSel, (nint)2, (nint)5), textBox.SelectionRequests);
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(3, textBox.SelectionLength);
            Assert.True(textBox.Modified);
            Assert.Equal("0123456789", textBox.Text);
        });

    [Fact]
    public void EmptyNativeTextSkipsDeferredScrollAndConsumesItsPendingRequest()
        => OnSta(() =>
        {
            using var textBox = new ScrollTextBox();
            textBox.ScrollToCaret();
            _ = textBox.Handle;
            Assert.Equal(0, textBox.ScrollCount);

            textBox.Text = "added after creation";
            textBox.ReplayHandleCreated();
            Assert.Equal(0, textBox.ScrollCount);
            textBox.ScrollToCaret();
            Assert.Equal(1, textBox.ScrollCount);
        });

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("invented public text")]
    public void EmptyWindowTextIsAuthoritativeEvenWhenPublicTextGetterReportsSomethingElse(string? publicText)
        => OnSta(() =>
        {
            using var textBox = new ScrollTextBox();
            _ = textBox.Handle;
            textBox.ReportedText = () => publicText!;
            textBox.RejectManagedReads = true;

            textBox.ScrollToCaret();

            Assert.Equal(0, textBox.ScrollCount);
            Assert.Equal(0, textBox.ManagedTextReadCount);
            textBox.RejectManagedReads = false;
            textBox.ReportedText = null;
        });

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("different public text")]
    public void NonemptyWindowTextScrollsWithoutReadingVirtualTextOrSelection(string? publicText)
        => OnSta(() =>
        {
            using var textBox = new ScrollTextBox { Text = "real native text" };
            nint handle = textBox.Handle;
            textBox.ManagedTextReadCount = 0;
            textBox.ReportedText = () => publicText!;
            textBox.RejectManagedReads = true;

            textBox.ScrollToCaret();
            textBox.ScrollToCaret();

            Assert.Equal(2, textBox.ScrollCount);
            Assert.Equal(0, textBox.ManagedTextReadCount);
            Assert.Equal(handle, textBox.Handle);
            Assert.All(textBox.ScrollRequests,
                request => Assert.Equal((EmScrollCaret, (nint)0, (nint)0), request));
            textBox.RejectManagedReads = false;
            textBox.ReportedText = null;
        });

    [Fact]
    public void WindowlessHandleCreatedNotificationLeavesDeferredRequestForActualWindow()
        => OnSta(() =>
        {
            using var textBox = new ScrollTextBox { Text = "later" };
            textBox.ScrollToCaret();

            textBox.ReplayHandleCreated();

            Assert.False(textBox.IsHandleCreated);
            Assert.Equal(0, textBox.ScrollCount);
            _ = textBox.Handle;
            Assert.Equal(1, textBox.ScrollCount);
        });

    [Fact]
    public void CreationFailurePreservesLazyRequestAndOriginalExceptionForRetry()
        => OnSta(() =>
        {
            var failure = new InvalidOperationException("caret creation");
            using var textBox = new ScrollTextBox { Text = "retry", CreationFailure = failure };
            textBox.ScrollToCaret();
            Assert.False(textBox.IsHandleCreated);
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => _ = textBox.Handle));
            Assert.False(textBox.IsHandleCreated);
            Assert.Equal(0, textBox.ScrollCount);

            textBox.CreationFailure = null;
            _ = textBox.Handle;
            Assert.Equal(1, textBox.ScrollCount);
        });

    [Fact]
    public void OriginalHandleCreatedEventExceptionDoesNotConsumePendingRequest()
        => OnSta(() =>
        {
            var failure = new InvalidOperationException("caret creation event");
            using var textBox = new ScrollTextBox { Text = "event retry" };
            EventHandler handler = (_, _) => throw failure;
            textBox.HandleCreated += handler;
            textBox.ScrollToCaret();

            // Invoke the protected notification without a native callback so
            // the original exception is observable on this managed call.
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(textBox.ReplayHandleCreated));
            Assert.False(textBox.IsHandleCreated);
            Assert.Equal(0, textBox.ScrollCount);
            textBox.HandleCreated -= handler;

            _ = textBox.Handle;
            Assert.Equal(1, textBox.ScrollCount);
        });

    [Fact]
    public void ExplicitScrollFromCreatedHandlerDoesNotConsumeSeparateDeferredRequest()
        => OnSta(() =>
        {
            using var textBox = new ScrollTextBox { Text = "two requests" };
            textBox.HandleCreated += (_, _) => textBox.ScrollToCaret();
            textBox.ScrollToCaret();

            _ = textBox.Handle;

            Assert.Equal(1, textBox.CreatedNotifications);
            Assert.Equal(2, textBox.ScrollCount);
        });

    [Fact]
    public void DeferredBitRemainsSetDuringScrollAndIsClearedAfterFiniteReentry()
        => OnSta(() =>
        {
            using var textBox = new ScrollTextBox { Text = "reentry" };
            // OnScroll is cleared before invoking this callback, bounding
            // reentry to exactly one additional notification/scroll.
            textBox.OnScroll = textBox.ReplayHandleCreated;
            textBox.ScrollToCaret();

            nint handle = textBox.Handle;

            Assert.Equal(2, textBox.CreatedNotifications);
            Assert.Equal(2, textBox.ScrollCount);
            textBox.ReplayHandleCreated();
            Assert.Equal(2, textBox.ScrollCount);
            Assert.Equal(handle, textBox.Handle);
        });

    [Fact]
    public void ConsumedDeferredRequestIsNotReplayedByLaterRecreation()
        => OnSta(() =>
        {
            using var textBox = new ScrollTextBox { Text = "recreate" };
            textBox.ScrollToCaret();
            _ = textBox.Handle;
            Assert.Equal(1, textBox.ScrollCount);

            textBox.RecreateNow();

            Assert.Equal(2, textBox.CreatedNotifications);
            Assert.Equal(1, textBox.ScrollCount);
            textBox.ScrollToCaret();
            Assert.Equal(2, textBox.ScrollCount);
        });

    [Fact]
    public void NewWindowlessRequestAfterDestroyIsConsumedByNextCreation()
        => OnSta(() =>
        {
            using var textBox = new ScrollTextBox { Text = "destroy and retry" };
            _ = textBox.Handle;
            Assert.Equal(0, textBox.ScrollCount);
            textBox.DestroyNow();
            Assert.False(textBox.IsHandleCreated);

            textBox.ScrollToCaret();
            Assert.False(textBox.IsHandleCreated);
            _ = textBox.Handle;

            Assert.Equal(2, textBox.CreatedNotifications);
            Assert.Equal(1, textBox.ScrollCount);
        });

    [Fact]
    public void RealMultilineEditScrollsCaretIntoViewWithoutEditingOrLosingUndo()
        => OnSta(() =>
        {
            using var parent = new Form
            {
                Size = new Size(340, 180), StartPosition = FormStartPosition.Manual,
                Location = new Point(-30000, -30000)
            };
            parent.Show();
            nint parentHandle = parent.Handle;
            Assert.True(IsWindowVisible(parentHandle));
            string originalText = string.Join("\r\n", Enumerable.Range(0, 40).Select(index => $"line {index:D2}"));
            nint native = CreateWindowExW(0, "EDIT", originalText,
                0x50000000u | 0x00200000u | 0x0004u | 0x0040u | 0x0080u,
                0, 0, 270, 60, parentHandle, 0, 0, 0);
            Assert.NotEqual(0, native);
            try
            {
                Assert.True(IsWindowVisible(native));
                _ = SendMessageW(native, EmSetSel, originalText.Length, originalText.Length);
                _ = SendMessageW(native, 0x00C2, (nint)(-1), "end");
                parent.Hide();
                Assert.False(IsWindowVisible(native));
                _ = SendMessageW(native, EmLineScroll, 0, (nint)(-100));
                _ = SendMessageW(native, EmScrollCaret, 0, 0);
                int hidden = (int)SendMessageW(native, EmGetFirstVisibleLine, 0, 0);
                parent.Show();
                Assert.True(IsWindowVisible(native));
                _ = SetFocus(parentHandle);
                Assert.NotEqual(native, GetFocus());
                _ = SendMessageW(native, EmLineScroll, 0, (nint)(-100));
                _ = SendMessageW(native, EmScrollCaret, 0, 0);
                int withoutCaret = (int)SendMessageW(native, EmGetFirstVisibleLine, 0, 0);
                _ = SetFocus(native);
                Assert.Equal(native, GetFocus());
                _ = SendMessageW(native, EmLineScroll, 0, (nint)(-100));
                _ = SendMessageW(native, EmScrollCaret, 0, 0);
                int withCaret = (int)SendMessageW(native, EmGetFirstVisibleLine, 0, 0);
                _output.WriteLine($"Independent EDIT: hidden first={hidden}; visible unfocused first={withoutCaret}; focused first={withCaret}");
                Assert.InRange(withCaret, 1, 39);
            }
            finally { Assert.True(DestroyWindow(native)); }
            using var textBox = new TextBox
            {
                Parent = parent, Multiline = true, WordWrap = false,
                ScrollBars = ScrollBars.Vertical, Size = new Size(270, 60),
                Text = originalText
            };
            nint handle = textBox.Handle;
            Assert.Equal(parentHandle, GetParent(handle));
            textBox.Select(textBox.TextLength, 0);
            _ = SendMessageW(handle, 0x00C2, (nint)(-1), "end");
            Assert.True(textBox.CanUndo);
            Assert.True(textBox.Modified);
            _ = SetFocus(handle);
            Assert.Equal(handle, GetFocus());
            _ = SendMessageW(handle, EmLineScroll, 0, (nint)(-100));
            Assert.Equal((nint)0, SendMessageW(handle, EmGetFirstVisibleLine, 0, 0));
            string text = textBox.Text;
            int start = textBox.SelectionStart;
            int length = textBox.SelectionLength;
            int changes = 0;
            int modifiedChanges = 0;
            textBox.TextChanged += (_, _) => changes++;
            textBox.ModifiedChanged += (_, _) => modifiedChanges++;

            textBox.ScrollToCaret();

            int firstVisible = (int)SendMessageW(handle, EmGetFirstVisibleLine, 0, 0);
            _output.WriteLine($"Lumina EDIT: visible={IsWindowVisible(handle)}; focused={GetFocus() == handle}; first={firstVisible}");
            Assert.InRange(firstVisible, 1, 39);
            Assert.Equal(handle, textBox.Handle);
            Assert.Equal(text, textBox.Text);
            Assert.Equal(start, textBox.SelectionStart);
            Assert.Equal(length, textBox.SelectionLength);
            Assert.True(textBox.Modified);
            Assert.True(textBox.CanUndo);
            Assert.Equal(0, changes);
            Assert.Equal(0, modifiedChanges);
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox caret scroll test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class ScrollTextBox : TextBox
    {
        internal int ScrollCount { get; private set; }
        internal int CreatedNotifications { get; private set; }
        internal int ManagedTextReadCount { get; set; }
        internal bool RejectManagedReads { get; set; }
        internal Func<string>? ReportedText { get; set; }
        internal Exception? CreationFailure { get; set; }
        internal Action? OnScroll { get; set; }
        internal List<string> Trace { get; } = new();
        internal List<(int Message, nint WParam, nint LParam)> ScrollRequests { get; } = new();
        internal List<(int Message, nint WParam, nint LParam)> SelectionRequests { get; } = new();

        internal void ReplayHandleCreated() => OnHandleCreated(EventArgs.Empty);
        internal void RecreateNow() => RecreateHandle();
        internal void DestroyNow() => DestroyHandle();

        [AllowNull]
        public override string Text
        {
            get
            {
                if (RejectManagedReads)
                {
                    ManagedTextReadCount++;
                    throw new InvalidOperationException("ScrollToCaret must consume WindowText, not Text.");
                }
                return ReportedText is null ? base.Text : ReportedText();
            }
            set => base.Text = value;
        }

        public override int TextLength
            => RejectManagedReads ? throw new InvalidOperationException("Unexpected TextLength read.") : base.TextLength;

        public override int SelectionLength
        {
            get => RejectManagedReads ? throw new InvalidOperationException("Unexpected selection read.") : base.SelectionLength;
            set => base.SelectionLength = value;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                if (CreationFailure is not null) { throw CreationFailure; }
                return base.CreateParams;
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            CreatedNotifications++;
            base.OnHandleCreated(e);
        }

        protected override void WndProc(ref Message message)
        {
            if (message.Msg == EmScrollCaret)
            {
                Trace.Add("scroll");
                ScrollCount++;
                ScrollRequests.Add((message.Msg, message.WParam, message.LParam));
                Action? callback = OnScroll;
                OnScroll = null;
                callback?.Invoke();
                message.Result = (nint)123;
                return;
            }
            if (message.Msg == EmSetSel)
            {
                Trace.Add("selection");
                SelectionRequests.Add((message.Msg, message.WParam, message.LParam));
            }
            if (message.Msg == EmLimitText) { Trace.Add("limit"); }
            if (message.Msg == EmSetModify && message.WParam != 0) { Trace.Add("modified"); }
            base.WndProc(ref message);
        }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetParent(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SetFocus(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetFocus();

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindowVisible(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint CreateWindowExW(uint extendedStyle, string className, string text,
        uint style, int x, int y, int width, int height, nint parent, nint menu, nint instance, nint parameter);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, string lParam);
}
