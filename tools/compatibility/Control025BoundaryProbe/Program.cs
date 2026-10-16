using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
#if REFERENCE
using System.Windows.Forms;
#else
using Lumina.Forms;
#endif

// Batch 025 Control boundary probe. Each scenario is run in its own process by
// Test-Control025Boundary.ps1 so native callback failures and raw process exits
// remain visible. The source is shared by the fixed WinForms 10.0.11 reference
// and the Lumina target; this file intentionally does not normalize differences.
internal static class Program
{
    private const uint WmUser = 0x0400;
    private const uint WmControl = WmUser + 0x251;
    private const uint WmForm = WmUser + 0x252;
    private const uint WmThrow = WmUser + 0x253;

    private static readonly List<string> Results = [];
    private static readonly List<Exception> ThreadExceptions = [];
    private static readonly AsyncLocal<string?> CallerContext = new();
    private static readonly Exception CallbackFailure = new InvalidOperationException("control callback failure");
    private static readonly Exception CleanupFailure = new InvalidOperationException("cleanup failure");
    private static readonly Exception WindowFailure = new InvalidOperationException("window callback failure");
    private static int s_uiThread;

    [STAThread]
    private static void Main(string[] args)
    {
        s_uiThread = Environment.CurrentManagedThreadId;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Error.WriteLine("runtime=" + System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription);
#if REFERENCE
        string? version = typeof(Control).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        if (version is null || !version.StartsWith("10.0.11+", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Expected WinForms 10.0.11, found " + version);
        }
        Console.Error.WriteLine("reference-assembly=" + version);
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
#endif
        WindowsFormsSynchronizationContext.AutoInstall = false;
        SynchronizationContext.SetSynchronizationContext(null);
        Application.ThreadException += OnThreadException;

        if (args.Length != 1)
        {
            throw new ArgumentException("Specify exactly one Control 025 scenario.", nameof(args));
        }

        Record("scenario", args[0]);
        try
        {
            switch (args[0])
            {
                case "drain-set-failure": DrainSetFailure(); break;
                case "drain-callback-error": DrainCallbackError(); break;
                case "unobserved-threadexception": UnobservedThreadException(); break;
                case "same-thread-invoke": SameThreadInvoke(); break;
                case "message-entry": MessageEntry(); break;
                case "context-entry": ContextEntry(); break;
                case "external-ncdestroy": ExternalNcDestroy(false); break;
                case "form-ncdestroy": ExternalNcDestroy(true); break;
                default: throw new ArgumentException("Unknown Control 025 scenario: " + args[0], nameof(args));
            }
        }
        catch (Exception exception)
        {
            Record("scenario-error", Describe(exception));
            Console.Error.WriteLine("scenario-error=" + Describe(exception));
            Environment.ExitCode = 2;
        }
        finally
        {
            Record("thread-exception-count", ThreadExceptions.Count);
            Record("thread-exception-sequence", string.Join(";", ThreadExceptions.Select(Describe)));
            Application.ThreadException -= OnThreadException;
        }

        Record("complete", Environment.ExitCode == 0);
        Console.WriteLine(string.Join(Environment.NewLine, Results));
    }

    private static void OnThreadException(object? sender, ThreadExceptionEventArgs args)
    {
        ThreadExceptions.Add(args.Exception);
        Console.Error.WriteLine("thread-exception=" + Describe(args.Exception) + "|ui=" + IsUiThread());
    }

    private static void DrainSetFailure()
    {
        using var control = NewControl();
        nint hwnd = control.Handle;
        Record("handle", IsWindow(hwnd));
        List<string> order = [];
        IAsyncResult first = control.BeginInvoke((Action)(() => order.Add("first")));
        IAsyncResult second = control.BeginInvoke((Action)(() => order.Add("second")));
        first.AsyncWaitHandle.Dispose();
        Record("before", QueueState(control, order, first, second));
        Record("first-endinvoke", Call(() => control.EndInvoke(first)));
        Record("after-first", QueueState(control, order, first, second));
        Record("second-endinvoke", Call(() => control.EndInvoke(second)));
        Record("after-second", QueueState(control, order, first, second));
        Record("order", string.Join(",", order));
        DisposeAndRecord(control);
    }

    private static void DrainCallbackError()
    {
        using var control = NewControl();
        nint hwnd = control.Handle;
        Record("handle", IsWindow(hwnd));
        List<string> order = [];
        IAsyncResult first = control.BeginInvoke((Action)(() => { order.Add("first"); throw CallbackFailure; }));
        IAsyncResult second = control.BeginInvoke((Action)(() => order.Add("second")));
        first.AsyncWaitHandle.Dispose();
        Record("before", QueueState(control, order, first, second));
        Record("first-endinvoke", Call(() => control.EndInvoke(first), CallbackFailure));
        Record("after-first", QueueState(control, order, first, second));
        Record("first-repeat", Call(() => control.EndInvoke(first), CallbackFailure));
        Record("second-endinvoke", Call(() => control.EndInvoke(second)));
        Record("after-second", QueueState(control, order, first, second));
        Record("order", string.Join(",", order));
        DisposeAndRecord(control);
    }

    private static void UnobservedThreadException()
    {
        using var control = NewControl();
        nint hwnd = control.Handle;
        Record("handle", IsWindow(hwnd));
        List<string> order = [];
        IAsyncResult first = control.BeginInvoke((Action)(() => { order.Add("first"); throw CallbackFailure; }));
        IAsyncResult second = control.BeginInvoke((Action)(() => order.Add("second")));
        Until(() => second.IsCompleted);
        Record("after-pump", QueueState(control, order, first, second));
        Record("order", string.Join(",", order));
        Record("first-endinvoke", Call(() => control.EndInvoke(first), CallbackFailure));
        Record("second-endinvoke", Call(() => control.EndInvoke(second)));
        Record("thread-exception-identity", ThreadExceptions.Any(e => ReferenceEquals(e, CallbackFailure)));
        DisposeAndRecord(control);
    }

    private static void SameThreadInvoke()
    {
        using var control = NewControl();
        nint hwnd = control.Handle;
        Record("handle", IsWindow(hwnd));
        List<string> order = [];
        IAsyncResult queued = control.BeginInvoke((Action)(() => order.Add("queued")));
        Record("queued-deferred", !queued.IsCompleted && order.Count == 0);
        int result = control.Invoke((Func<int>)(() => { order.Add("invoke"); return 42; }));
        Record("invoke-result", result);
        Record("after-invoke", string.Join(",", order));
        Record("queued-endinvoke", Call(() => control.EndInvoke(queued)));

        order.Clear();
        control.Invoke((Action)(() =>
        {
            order.Add("outer-enter");
            control.Invoke((Action)(() => order.Add("inner-invoke")));
            IAsyncResult nested = control.BeginInvoke((Action)(() => order.Add("nested-queued")));
            Record("nested-deferred", !nested.IsCompleted);
            order.Add("outer-exit");
            control.EndInvoke(nested);
        }));
        Record("reentrant-order", string.Join(",", order));

        Exception? workerFailure = null;
        var worker = new Thread(() =>
        {
            try
            {
                IAsyncResult workerCall = control.BeginInvoke((Func<int>)(() => { order.Add("worker"); return 7; }));
                Record("worker-endinvoke", control.EndInvoke(workerCall));
            }
            catch (Exception exception) { workerFailure = exception; }
        }) { IsBackground = true };
        worker.Start();
        Until(() => !worker.IsAlive);
        worker.Join();
        Record("worker-failure", Describe(workerFailure));
        DisposeAndRecord(control);
    }

    private static void MessageEntry()
    {
        using var control = new MessageControl();
        using var form = new MessageForm();
        nint controlHandle = control.Handle;
        nint formHandle = form.Handle;
        Record("handles", "control:" + IsWindow(controlHandle) + ",form:" + IsWindow(formHandle));
        Record("control-send", SendMessageW(controlHandle, (int)WmControl, 1, 0));
        Record("form-send", SendMessageW(formHandle, (int)WmForm, 1, 0));
        Record("control-post", PostMessageW(controlHandle, WmControl, 2, 0));
        Record("form-post", PostMessageW(formHandle, WmForm, 2, 0));
        Until(() => control.PostSeen && form.PostSeen);
        Record("control-events", string.Join(",", control.TraceEvents));
        Record("form-events", string.Join(",", form.TraceEvents));

        control.EnableReentrant = true;
        Record("control-reentrant-send", SendMessageW(controlHandle, (int)WmControl, 3, 0));
        Until(() => control.TraceEvents.Contains("nested-queued"));
        Record("control-reentrant-events", string.Join(",", control.TraceEvents));

        Record("throw-post", PostMessageW(controlHandle, WmThrow, 0, 0));
        Until(() => ThreadExceptions.Any(e => ReferenceEquals(e, CallbackFailure)));
        Record("throw-threadexception", ThreadExceptions.Any(e => ReferenceEquals(e, CallbackFailure)));
        Record("native-after-throw", IsWindow(controlHandle));
        DisposeAndRecord(control);
        DisposeAndRecord(form);
    }

    private static void ContextEntry()
    {
        var sentinel = new SynchronizationContext();
        SynchronizationContext.SetSynchronizationContext(sentinel);
        using var context = new WindowsFormsSynchronizationContext();
        List<string> order = [];
        object state = new();
        bool posted = false;
        context.Post(value =>
        {
            order.Add("post");
            posted = true;
            Record("post-ui", IsUiThread());
            Record("post-state", ReferenceEquals(value, state));
            Record("post-current", ReferenceEquals(SynchronizationContext.Current, sentinel));
        }, state);
        Record("post-deferred", !posted);
        context.Send(value =>
        {
            order.Add("send");
            Record("send-ui", IsUiThread());
            Record("send-state", ReferenceEquals(value, state));
            Record("send-current", ReferenceEquals(SynchronizationContext.Current, sentinel));
        }, state);
        Record("send-drains-fifo", string.Join(",", order));

        Exception? workerFailure = null;
        var worker = new Thread(() =>
        {
            try
            {
                CallerContext.Value = "worker-context";
                context.Send(_ =>
                {
                    order.Add("worker-send");
                    Record("worker-send-ui", IsUiThread());
                    Record("worker-send-context", CallerContext.Value);
                    Record("worker-send-current", ReferenceEquals(SynchronizationContext.Current, sentinel));
                }, null);
                using var done = new ManualResetEventSlim();
                context.Post(_ =>
                {
                    order.Add("worker-post");
                    Record("worker-post-ui", IsUiThread());
                    Record("worker-post-context", CallerContext.Value);
                    done.Set();
                }, null);
                if (!done.Wait(TimeSpan.FromSeconds(7))) throw new TimeoutException("Worker Post did not complete.");
            }
            catch (Exception exception) { workerFailure = exception; }
        }) { IsBackground = true };
        worker.Start();
        Until(() => !worker.IsAlive);
        worker.Join();
        Record("worker-failure", Describe(workerFailure));
        Record("order", string.Join(",", order));

        Exception direct = new InvalidOperationException("context send failure");
        Record("send-error", Call(() => context.Send(_ => throw direct, null), direct));
        context.Post(_ => throw CallbackFailure, null);
        Until(() => ThreadExceptions.Any(e => ReferenceEquals(e, CallbackFailure)));
        Record("post-error-threadexception", ThreadExceptions.Any(e => ReferenceEquals(e, CallbackFailure)));
        ((IDisposable)context).Dispose();
        SynchronizationContext.SetSynchronizationContext(null);
    }

    private static void ExternalNcDestroy(bool form)
    {
        if (form)
        {
            using var target = new DualErrorForm();
            ExternalDestroy(target, target.Handle, "form");
            return;
        }

        using var control = new DualErrorControl();
        ExternalDestroy(control, control.Handle, "control");
    }

    private static void ExternalDestroy(Control target, nint hwnd, string name)
    {
        IAsyncResult pending = target.BeginInvoke((Action)(() => throw CallbackFailure));
        Record(name + "-before", "managed:" + target.IsHandleCreated + ",native:" + IsWindow(hwnd) + ",pending:" + !pending.IsCompleted);
        Record(name + "-destroy-window", DestroyWindow(hwnd));
        Pump();
        Record(name + "-after", "managed:" + target.IsHandleCreated + ",native:" + IsWindow(hwnd) + ",disposed:" + target.IsDisposed + ",pending:" + pending.IsCompleted);
        Record(name + "-endinvoke", Call(() => target.EndInvoke(pending), CallbackFailure));
        Record(name + "-dispose", Call(target.Dispose));
        Record(name + "-final", "managed:" + target.IsHandleCreated + ",native:" + IsWindow(hwnd) + ",disposed:" + target.IsDisposed);
    }

    private static ProbeControl NewControl() => new() { Name = "control", Bounds = new(0, 0, 64, 48) };

    private static string QueueState(Control control, List<string> order, params IAsyncResult[] results) =>
        "order:" + string.Join(",", order) + "|completed:[" + string.Join(",", results.Select(r => r.IsCompleted)) + "]|managed:" + control.IsHandleCreated + "|native:" + IsWindow(control.Handle);

    private static void DisposeAndRecord(IDisposable target)
    {
        Record("dispose", Call(target.Dispose));
        if (target is Control control)
        {
            Record("final", "managed:" + control.IsHandleCreated + ",disposed:" + control.IsDisposed);
        }
    }

    private static string Call(Action action, Exception? expected = null)
    {
        try { action(); return "success"; }
        catch (Exception exception) { return Describe(exception) + (expected is null ? string.Empty : ":identity=" + ReferenceEquals(exception, expected)); }
    }

    private static bool IsUiThread() => Environment.CurrentManagedThreadId == s_uiThread;

    private static string Describe(Exception? exception)
    {
        if (exception is null) return "none";
        string inner = exception.InnerException is null ? string.Empty : ":inner=" + Describe(exception.InnerException);
        return exception.GetType().Name + ":" + exception.Message + inner;
    }

    private static void Record(string name, object? value) => Results.Add(name + "=" + (value?.ToString() ?? "null"));

    private static void Until(Func<bool> predicate)
    {
        var deadline = Stopwatch.StartNew();
        while (!predicate())
        {
            if (deadline.Elapsed > TimeSpan.FromSeconds(12)) throw new TimeoutException("Control 025 message pump timed out.");
            Pump();
            Thread.Sleep(1);
        }
    }

    private static void Pump()
    {
        int count = 0;
        while (PeekMessageW(out NativeMessage message, 0, 0, 0, 1))
        {
            if (message.Message == 0x0012) return;
            if (++count > 4096) throw new InvalidOperationException("Message pump did not become idle.");
            _ = TranslateMessage(in message);
            _ = DispatchMessageW(in message);
        }
    }

    private class ProbeControl : Control;

    private sealed class MessageControl : Control
    {
        internal List<string> TraceEvents { get; } = [];
        internal bool PostSeen { get; private set; }
        internal bool EnableReentrant;

        protected override void WndProc(ref Message message)
        {
            if (message.Msg == WmControl)
            {
                TraceEvents.Add("enter:" + message.WParam + ":ui=" + IsUiThread());
                if (message.WParam == 2) PostSeen = true;
                if (message.WParam == 3 && EnableReentrant)
                {
                    Invoke((Action)(() => TraceEvents.Add("nested-invoke")));
                    BeginInvoke((Action)(() => TraceEvents.Add("nested-queued")));
                }
                base.WndProc(ref message);
                TraceEvents.Add("exit:" + message.WParam);
                return;
            }

            if (message.Msg == WmThrow)
            {
                throw CallbackFailure;
            }

            base.WndProc(ref message);
        }
    }

    private sealed class MessageForm : Form
    {
        internal List<string> TraceEvents { get; } = [];
        internal bool PostSeen { get; private set; }

        protected override void WndProc(ref Message message)
        {
            if (message.Msg == WmForm)
            {
                TraceEvents.Add("enter:" + message.WParam + ":ui=" + IsUiThread());
                if (message.WParam == 2) PostSeen = true;
                base.WndProc(ref message);
                TraceEvents.Add("exit:" + message.WParam);
                return;
            }

            base.WndProc(ref message);
        }
    }

    private sealed class DualErrorControl : Control
    {
        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            throw CleanupFailure;
        }

        protected override void WndProc(ref Message message)
        {
            if (message.Msg == 0x0082)
            {
                try { base.WndProc(ref message); }
                finally { ExceptionDispatchInfo.Capture(WindowFailure).Throw(); }
                return;
            }

            base.WndProc(ref message);
        }
    }

    private sealed class DualErrorForm : Form
    {
        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            throw CleanupFailure;
        }

        protected override void WndProc(ref Message message)
        {
            if (message.Msg == 0x0082)
            {
                try { base.WndProc(ref message); }
                finally { ExceptionDispatchInfo.Capture(WindowFailure).Throw(); }
                return;
            }

            base.WndProc(ref message);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct NativeMessage
    {
        public nint Hwnd;
        public uint Message;
        public nuint WParam;
        public nint LParam;
        public uint Time;
        public int X;
        public int Y;
        public uint Private;
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool PeekMessageW(out NativeMessage message, nint hwnd, uint min, uint max, uint remove);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool TranslateMessage(in NativeMessage message);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint DispatchMessageW(in NativeMessage message);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool PostMessageW(nint hwnd, uint message, nuint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint hwnd, int message, nuint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint hwnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint hwnd);
}
