using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
#if REFERENCE
using System.Windows.Forms;
#else
using Lumina.Forms;
#endif

internal static class Program
{
    private static readonly List<string> Results = [];
    private static readonly AsyncLocal<string?> CallerContext = new();
    private static int s_uiThread;
    private static int s_asyncExceptions;
    private static bool s_asyncExceptionOnUi;

    [STAThread]
    private static void Main()
    {
        s_uiThread = Environment.CurrentManagedThreadId;
        Application.ThreadException += (_, e) =>
        {
            s_asyncExceptions++;
            s_asyncExceptionOnUi = Environment.CurrentManagedThreadId == s_uiThread && e.Exception is DispatchException;
        };
#if REFERENCE
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        string? version = typeof(Control).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        if (version is null || !version.StartsWith("10.0.11+", StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"Expected WinForms 10.0.11, found {version}.");
        }
#endif
        using var uncreated = new Control();
        Record("uncreated-ui-required", uncreated.InvokeRequired);
        Record("uncreated-invoke", Failure(() => uncreated.Invoke((Action)(() => { }))));
        Record("uncreated-begin", Failure(() => uncreated.BeginInvoke((Action)(() => { }))));
        Record("uncreated-null-invoke", Failure(() => uncreated.Invoke((Delegate)null!)));
        Record("uncreated-null-begin", Failure(() => uncreated.BeginInvoke((Delegate)null!)));
        Record("end-null", Failure(() => uncreated.EndInvoke(null!)));
        Record("end-invalid", Failure(() => uncreated.EndInvoke(Task.CompletedTask)));
        RunWorker(() => Record("uncreated-worker-required", uncreated.InvokeRequired), pump: false);

        using var form = new Form { Text = "Lumina dispatch probe", Width = 240, Height = 160 };
        using var panel = new Panel();
        using var control = new Control();
        using var inherited = new HandlelessControl { Visible = false };
        using var ownerFallback = new HandlelessControl { Visible = false };
        panel.Controls.Add(control);
        panel.Controls.Add(inherited);
        form.Controls.Add(panel);
        form.Controls.Add(ownerFallback);
        form.Show();

        Record("control-interface", control is ISynchronizeInvoke);
        Record("control-ui-required", control.InvokeRequired);
        Record("form-ui-required", form.InvokeRequired);
        Record("parent-handleless", !inherited.IsHandleCreated);
        Record("owner-handleless", !ownerFallback.IsHandleCreated);
        int actionThread = 0;
        control.Invoke(() => actionThread = Environment.CurrentManagedThreadId);
        Record("ui-action-thread", actionThread == s_uiThread);
        Record("ui-func-result", control.Invoke((Func<int>)(() => 42)));
        Record("ui-delegate-func-result", control.Invoke((Delegate)(Func<int>)(() => 42)));
        Record("ui-args-result", control.Invoke((Func<int, int, int>)((a, b) => a + b), 20, 22));
        Record("ui-exception", Failure(() => control.Invoke((Action)(() => throw new DispatchException()))));
        Record("ui-func-exception", Failure(() => control.Invoke((Func<int>)(() => throw new DispatchException()))));
        Record("ui-inner-exception", Failure(() => control.Invoke((Delegate)(Action)(() => throw new InvalidOperationException("Outer", new DispatchException())))));
        Record("ui-nested-target-exception", Failure(() => control.Invoke((Delegate)(Func<int>)(() => throw new TargetInvocationException(new TargetInvocationException(new DispatchException()))))));
        bool defaultEventHandler = false;
        inherited.Invoke((Delegate)new EventHandler((sender, e) => defaultEventHandler = ReferenceEquals(sender, inherited) && ReferenceEquals(e, EventArgs.Empty)));
        Record("event-handler-default-caller", defaultEventHandler);
        bool singleArgumentEventHandler = false;
        object? singleArgumentSender = null;
        EventArgs? singleArgumentEventArgs = null;
        inherited.Invoke((Delegate)new EventHandler((sender, e) =>
        {
            singleArgumentSender = sender;
            singleArgumentEventArgs = e;
            singleArgumentEventHandler = ReferenceEquals(sender, inherited) && ReferenceEquals(e, EventArgs.Empty);
        }), "ignored");
        Record("event-handler-single-arg-default", singleArgumentEventHandler);
        Record("event-handler-single-arg-sender", singleArgumentSender is string text ? text : ReferenceEquals(singleArgumentSender, inherited) ? "caller" : singleArgumentSender?.GetType().Name ?? "null");
        Record("event-handler-single-arg-event-empty", ReferenceEquals(singleArgumentEventArgs, EventArgs.Empty));
        bool explicitEventHandler = false;
        var explicitArgs = new EventArgs();
        control.Invoke((Delegate)new EventHandler((sender, e) => explicitEventHandler = sender as string == "sender" && ReferenceEquals(e, explicitArgs)), "sender", explicitArgs);
        Record("event-handler-args", explicitEventHandler);
        bool waitCallback = false;
        WaitCallback wait = state => waitCallback = state is null;
        Record("wait-callback-no-args", Failure(() => control.Invoke((Delegate)wait)));
        Record("wait-callback-empty-args", Failure(() => control.Invoke((Delegate)wait, Array.Empty<object?>())));
        control.Invoke((Delegate)wait, new object?[] { null });
        Record("wait-callback-single-arg", waitCallback);
        bool sendCallback = false;
        SendOrPostCallback send = state => sendCallback = state is null;
        Record("send-callback-no-args", Failure(() => control.Invoke((Delegate)send)));
        Record("send-callback-empty-args", Failure(() => control.Invoke((Delegate)send, Array.Empty<object?>())));
        control.Invoke((Delegate)send, new object?[] { null });
        Record("send-callback-single-arg", sendCallback);
        // WinForms 10.0.11 Invoke((Delegate)null) with an HWND hangs; keep that invalid
        // input out of this shared process. Lumina deliberately guards null delegates.
        IAsyncResult sameThread = control.BeginInvoke((Func<int>)(() => 12));
        Record("begin-deferred", !sameThread.IsCompleted);
        Record("begin-state-null", sameThread.AsyncState is null);
        Record("begin-completed-synchronously", sameThread.CompletedSynchronously);
        Record("ui-end-result", control.EndInvoke(sameThread));
        Record("ui-end-completed", sameThread.IsCompleted);
        Record("ui-end-wait-signaled", sameThread.AsyncWaitHandle.WaitOne(0));
        Record("ui-end-repeat", control.EndInvoke(sameThread));
        List<int> order = [];
        IAsyncResult first = control.BeginInvoke(() => order.Add(1));
        control.Invoke(() => order.Add(2));
        Record("ui-queue-order", string.Join(",", order));
        control.EndInvoke(first);

        RunWorker(() =>
        {
            Record("control-worker-required", control.InvokeRequired);
            Record("form-worker-required", form.InvokeRequired);
            Record("parent-worker-required", inherited.InvokeRequired);
            Record("owner-worker-required", ownerFallback.InvokeRequired);
            CallerContext.Value = "worker-context";
            Record("worker-result", control.Invoke((Func<int, int>)(n => OnUi(n)), 42));
            Record("worker-context", control.Invoke((Func<string?>)(() => CallerContext.Value)));
            Record("worker-action-result", control.Invoke((Delegate)(Action)(() => Record("worker-action-thread", Environment.CurrentManagedThreadId == s_uiThread))) is null);
            Record("worker-exception", Failure(() => control.Invoke((Action)(() => throw new DispatchException()))));
            Record("worker-func-exception", Failure(() => control.Invoke((Func<int>)(() => throw new DispatchException()))));
            Record("parent-result", inherited.Invoke((Func<int>)(() => OnUi(42))));
            Record("owner-result", ownerFallback.Invoke((Func<int>)(() => OnUi(42))));
            Record("form-result", form.Invoke((Func<int, int>)(n => OnUi(n)), 42));
            form.Invoke(() => Record("form-action-thread", Environment.CurrentManagedThreadId == s_uiThread));
            IAsyncResult queued = control.BeginInvoke((Func<int, int>)(n => OnUi(n)), 42);
            Record("worker-end-result", control.EndInvoke(queued));
            Record("worker-end-completed", queued.IsCompleted);
            IAsyncResult otherReceiver = control.BeginInvoke((Func<int>)(() => 43));
            Record("other-receiver-end", inherited.EndInvoke(otherReceiver));
            IAsyncResult formQueued = form.BeginInvoke((Func<int>)(() => OnUi(44)));
            Record("form-end-result", form.EndInvoke(formQueued));
        });

        // EndInvoke on the UI thread drains the queue and observes callback exceptions.
        IAsyncResult asynchronousFailure = control.BeginInvoke((Action)(() => throw new DispatchException()));
        Record("begin-exception", Failure(() => control.EndInvoke(asynchronousFailure)));
        Record("async-exception-event-count", s_asyncExceptions);
        Record("async-exception-event-thread", s_asyncExceptionOnUi);
        IAsyncResult unobservedFailure = control.BeginInvoke(() => throw new DispatchException());
        RunWorker(() =>
        {
            if (!unobservedFailure.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(10)))
            {
                throw new TimeoutException("Asynchronous failure was not dispatched.");
            }
        });
        Record("unobserved-exception-event-count", s_asyncExceptions);

        // A queued callback must be completed when its native marshaling window is disposed.
        using var doomed = new Control();
        panel.Controls.Add(doomed);
        IAsyncResult abandoned = doomed.BeginInvoke((Action)(() => throw new InvalidOperationException("Must not run after disposal.")));
        doomed.Dispose();
        Record("disposed-required", doomed.InvokeRequired);
        Record("disposed-invoke", Failure(() => doomed.Invoke((Action)(() => { }))));
        Record("disposed-begin", Failure(() => doomed.BeginInvoke((Action)(() => { }))));
        Record("disposed-end", Failure(() => doomed.EndInvoke(abandoned)));
        Record("disposed-pending-completed", abandoned.IsCompleted);

        form.Dispose();
        Record("form-disposed-required", form.InvokeRequired);
        Record("form-disposed-invoke", Failure(() => form.Invoke((Action)(() => { }))));
        Console.WriteLine(string.Join(Environment.NewLine, Results));
    }

    private static int OnUi(int value)
    {
        if (Environment.CurrentManagedThreadId != s_uiThread)
        {
            throw new InvalidOperationException("Callback did not execute on the HWND creating thread.");
        }

        return value;
    }

    private static void RunWorker(Action action, bool pump = true)
    {
        Exception? error = null;
        using var completed = new ManualResetEventSlim();
        var worker = new Thread(() =>
        {
            try { action(); }
            catch (Exception exception) { error = exception; }
            finally { completed.Set(); }
        }) { IsBackground = true };
        worker.Start();
        var timeout = Stopwatch.StartNew();
        while (!completed.Wait(1))
        {
            if (timeout.Elapsed > TimeSpan.FromSeconds(15))
            {
                throw new TimeoutException("Thread dispatch probe timed out.");
            }

            if (pump)
            {
                while (PeekMessageW(out NativeMessage message, 0, 0, 0, 1))
                {
                    TranslateMessage(in message);
                    DispatchMessageW(in message);
                }
            }
        }

        worker.Join();
        if (error is not null) { throw new InvalidOperationException("Worker failed.", error); }
    }

    private static string Failure(Action action)
    {
        try { action(); return "none"; }
        catch (Exception exception)
        {
            return exception.GetType().Name + ":" + (exception is ArgumentException argument ? argument.ParamName : string.Empty)
                + ":" + exception.InnerException?.GetType().Name;
        }
    }

    private static void Record(string scenario, object? value)
    {
        string result = scenario + "=" + (value?.ToString() ?? "null");
        Results.Add(result);
        Console.Error.WriteLine(result);
    }

    private sealed class DispatchException : Exception;

    private sealed class HandlelessControl : Control
    {
#if !REFERENCE
        protected override bool ShouldCreateNativeHandle => false;
#endif
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

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool PeekMessageW(out NativeMessage message, nint hwnd, uint min, uint max, uint remove);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool TranslateMessage(in NativeMessage message);
    [DllImport("user32.dll")]
    private static extern nint DispatchMessageW(in NativeMessage message);
}
