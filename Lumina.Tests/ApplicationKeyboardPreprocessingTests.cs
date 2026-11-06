using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class ApplicationKeyboardPreprocessingTests
{
    private const int WmKeyDown = 0x0100;
    private const int WmKeyUp = 0x0101;
    private const int WmChar = 0x0102;
    private const int WmQuit = 0x0012;
    private const int ExitMessage = 0x8000 + 153;
    private const int ProbeMessage = ExitMessage + 1;

    [Fact]
    public void ConsumedCommandDoesNotReachKeyEventsOrTranslateIntoACharacter()
        => OnStaThread(() =>
        {
            using var control = new QueueControl { ConsumeCommand = true };
            int preview = 0;
            int keyDown = 0;
            int keyPress = 0;
            control.PreviewKeyDown += (_, _) => preview++;
            control.KeyDown += (_, _) => keyDown++;
            control.KeyPress += (_, _) => keyPress++;

            Post(control.Handle, WmKeyDown, (nint)Keys.A);
            RunThroughExit(control);

            Assert.Equal(1, control.PreprocessCalls);
            Assert.Equal(1, control.CommandCalls);
            Assert.Equal(0, preview);
            Assert.Equal(0, keyDown);
            Assert.Equal(0, keyPress);
        });

    [Fact]
    public void UnhandledPreprocessorCanChangeMessageDestinationAndParameters()
        => OnStaThread(() =>
        {
            using var destination = new QueueControl();
            using var source = new QueueControl
            {
                // A real key-up carries the previous-state and transition bits.
                // Without them TranslateMessage may generate an extra WM_CHAR.
                Rewrite = message => Message.Create(destination.Handle, WmKeyUp, (nint)Keys.B, unchecked((nint)0xC0001234u))
            };
            KeyEventArgs? observed = null;
            destination.KeyUp += (_, args) => observed = args;

            Post(source.Handle, WmChar, (nint)'a');
            RunThroughExit(source);

            Assert.Equal(1, source.PreprocessCalls);
            Assert.True(destination.PreprocessCalls == 0,
                $"Destination preprocessed: {string.Join(", ", destination.PreprocessedMessages)}");
            Assert.NotNull(observed);
            Assert.Equal(Keys.B, observed!.KeyCode);
            Assert.Equal(WmKeyUp, destination.LastKeyboardMessage.Msg);
            Assert.Equal(unchecked((nint)0xC0001234u), destination.LastKeyboardMessage.LParam);
            Assert.Equal(0, source.KeyboardDispatches);
            Assert.Equal(1, destination.KeyboardDispatches);
        });

    [Fact]
    public void QueuedTabUsesContainerRouteBeforeTheFocusedControlsKeyEvent()
        => OnStaThread(() =>
        {
            using var form = new Form();
            using var first = new QueueControl { Parent = form, TabIndex = 0 };
            using var second = new QueueControl { Parent = form, TabIndex = 1 };
            form.Show();
            form.ActiveControl = first;
            int keyDown = 0;
            first.KeyDown += (_, _) => keyDown++;

            Post(first.Handle, WmKeyDown, (nint)Keys.Tab);
            RunThroughExit(first);

            Assert.Same(second, form.ActiveControl);
            Assert.Equal(1, first.PreprocessCalls);
            Assert.Equal(0, keyDown);
            Assert.Equal(0, first.KeyboardDispatches);
        });

    [Fact]
    public void QueuedEnterAndEscapeInvokeTheFormsDefaultAndCancelButtons()
        => OnStaThread(() =>
        {
            using var form = new Form();
            using var source = new QueueControl { Parent = form, TabIndex = 0 };
            using var accept = new Button { Parent = form, DialogResult = DialogResult.OK };
            using var cancel = new Button { Parent = form };
            form.AcceptButton = accept;
            form.CancelButton = cancel;
            var trace = new List<DialogResult>();
            accept.Click += (_, _) => trace.Add(form.DialogResult);
            cancel.Click += (_, _) => trace.Add(form.DialogResult);
            form.Show();
            form.ActiveControl = source;

            Post(source.Handle, WmKeyDown, (nint)Keys.Enter);
            Post(source.Handle, WmKeyDown, (nint)Keys.Escape);
            RunThroughExit(source);

            Assert.Equal(new[] { DialogResult.OK, DialogResult.Cancel }, trace);
            Assert.Equal(DialogResult.Cancel, form.DialogResult);
            Assert.Equal(2, source.PreprocessCalls);
            Assert.Equal(0, source.KeyboardDispatches);
        });

    [Fact]
    public void UnhandledQueuedCharacterRunsFormPreviewAndChildEventExactlyOnce()
        => OnStaThread(() =>
        {
            using var form = new Form { KeyPreview = true };
            using var container = new ContainerControl { Parent = form };
            using var child = new QueueControl { Parent = container };
            var trace = new List<string>();
            form.KeyPress += (_, args) => { trace.Add("form"); args.KeyChar = 'z'; };
            child.KeyPress += (_, args) => { trace.Add($"child:{args.KeyChar}"); args.Handled = true; };
            form.Show();

            Post(child.Handle, WmChar, (nint)'a');
            RunThroughExit(child);

            Assert.Equal(new[] { "form", "child:z" }, trace);
            Assert.Equal(1, child.PreprocessCalls);
            Assert.Equal(1, child.KeyboardDispatches);
        });

    [Fact]
    public void NativeChildWindowUsesNearestManagedAncestorsPreprocessor()
        => OnStaThread(() =>
        {
            using var parent = new QueueControl { ConsumeCommand = true };
            nint nativeChild = CreateWindowExW(0, "STATIC", string.Empty, 0x40000000,
                0, 0, 10, 10, parent.Handle, 0, 0, 0);
            Assert.NotEqual(0, nativeChild);
            try
            {
                Post(nativeChild, WmKeyDown, (nint)Keys.A);
                RunThroughExit(parent);

                Assert.Equal(1, parent.PreprocessCalls);
                Assert.Equal(nativeChild, parent.LastPreprocessedWindow);
                Assert.Equal(1, parent.CommandCalls);
            }
            finally { Assert.True(DestroyWindow(nativeChild)); }
        });

    [Fact]
    public void NonKeyboardMessagesAndUnmappedThreadKeysBypassControlPreprocessing()
        => OnStaThread(() =>
        {
            using var control = new QueueControl();
            _ = control.Handle;
            Assert.True(PostThreadMessageW(GetCurrentThreadId(), WmKeyDown, (nint)Keys.A, 0));
            Post(control.Handle, ProbeMessage, 0);
            RunThroughExit(control);

            Assert.Equal(1, control.ProbeCalls);
            Assert.Equal(0, control.PreprocessCalls);
            Assert.Equal(0, control.KeyboardDispatches);
        });

    [Fact]
    public void PreprocessorExceptionIsReportedOnceAndDoesNotDispatchTheFaultedKey()
        => OnStaThread(() =>
        {
            var expected = new InvalidOperationException("preprocess failure");
            using var control = new QueueControl { PreprocessFailure = expected };
            var reported = new List<Exception>();
            System.Threading.ThreadExceptionEventHandler handler = (_, args) => reported.Add(args.Exception);
            Application.ThreadException += handler;
            try
            {
                Post(control.Handle, WmKeyDown, (nint)Keys.A);
                RunThroughExit(control);
            }
            finally { Application.ThreadException -= handler; }

            Assert.Same(expected, Assert.Single(reported));
            Assert.Equal(1, control.PreprocessCalls);
            Assert.Equal(0, control.KeyboardDispatches);
        });

    [Fact]
    public void SentKeyMessagesRemainOnTheOrdinaryWindowProcedurePath()
        => OnStaThread(() =>
        {
            using var control = new QueueControl { ConsumeCommand = true };
            int keyDown = 0;
            control.KeyDown += (_, args) => { keyDown++; args.Handled = true; };

            _ = SendMessageW(control.Handle, WmKeyDown, (nint)Keys.A, 0);

            Assert.Equal(0, control.PreprocessCalls);
            Assert.Equal(0, control.CommandCalls);
            Assert.Equal(1, keyDown);
            Assert.Equal(1, control.KeyboardDispatches);
        });

    private static void RunThroughExit(QueueControl destination)
    {
        // Each check queues at most two input/probe messages, followed by one
        // exit sentinel. The STA watchdog also posts WM_QUIT after five seconds.
        Post(destination.Handle, ExitMessage, 0);
        Assert.Equal(0, Application.RunMessageLoop());
        Assert.Equal(1, destination.ExitCalls);
    }

    private static void Post(nint window, int message, nint parameter)
        => Assert.True(PostMessageW(window, message, parameter, 0), $"PostMessage failed: {Marshal.GetLastWin32Error()}");

    private static void OnStaThread(Action action)
    {
        Exception? failure = null;
        int deadlineReached = 0;
        var thread = new Thread(() =>
        {
            uint threadId = GetCurrentThreadId();
            using var deadline = new System.Threading.Timer(state =>
            {
                Volatile.Write(ref deadlineReached, 1);
                _ = PostThreadMessageW(threadId, WmQuit, 0, 0);
            }, null, TimeSpan.FromSeconds(5), Timeout.InfiniteTimeSpan);
            try { action(); }
            catch (Exception exception) { failure = exception; }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(10)), "The keyboard queue check failed to exit after its watchdog.");
        Assert.Equal(0, Volatile.Read(ref deadlineReached));
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class QueueControl : Control
    {
        internal bool ConsumeCommand { get; init; }
        internal Func<Message, Message>? Rewrite { get; init; }
        internal Exception? PreprocessFailure { get; init; }
        internal int PreprocessCalls { get; private set; }
        internal List<string> PreprocessedMessages { get; } = [];
        internal int CommandCalls { get; private set; }
        internal int KeyboardDispatches { get; private set; }
        internal int ExitCalls { get; private set; }
        internal int ProbeCalls { get; private set; }
        internal nint LastPreprocessedWindow { get; private set; }
        internal Message LastKeyboardMessage { get; private set; }

        public override bool PreProcessMessage(ref Message message)
        {
            PreprocessCalls++;
            PreprocessedMessages.Add($"{message.Msg:X4}/{message.WParam:X}/{message.LParam:X}");
            LastPreprocessedWindow = message.HWnd;
            if (PreprocessFailure is not null) { throw PreprocessFailure; }
            if (Rewrite is not null) { message = Rewrite(message); return false; }
            return base.PreProcessMessage(ref message);
        }

        protected override bool ProcessCmdKey(ref Message message, Keys keyData)
        {
            CommandCalls++;
            return ConsumeCommand || base.ProcessCmdKey(ref message, keyData);
        }

        protected override void WndProc(ref Message message)
        {
            if (message.Msg == ExitMessage)
            {
                ExitCalls++;
                Application.ExitThread();
                return;
            }
            if (message.Msg == ProbeMessage) { ProbeCalls++; return; }
            if (message.Msg is WmKeyDown or WmKeyUp or WmChar)
            {
                KeyboardDispatches++;
                LastKeyboardMessage = message;
            }
            base.WndProc(ref message);
        }
    }

    [DllImport("kernel32.dll")]
    private static extern uint GetCurrentThreadId();
    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool PostThreadMessageW(uint threadId, int message, nint wParam, nint lParam);
    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool PostMessageW(nint window, int message, nint wParam, nint lParam);
    [DllImport("user32.dll")]
    private static extern nint SendMessageW(nint window, int message, nint wParam, nint lParam);
    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern nint CreateWindowExW(uint exStyle, string className, string text, uint style,
        int x, int y, int width, int height, nint parent, nint menu, nint instance, nint parameter);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint window);
}
