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
    private static int s_threadExceptions;

    [STAThread]
    private static void Main()
    {
        s_uiThread = Environment.CurrentManagedThreadId;
        Application.ThreadException += (_, _) => s_threadExceptions++;
#if REFERENCE
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        string? version = typeof(Control).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        if (version is null || !version.StartsWith("10.0.11+", StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"Expected WinForms 10.0.11, found {version}.");
        }
#endif
        using var canceled = new CancellationTokenSource();
        canceled.Cancel();
        using var uncreated = new Control();
        CheckCall("uncreated-action", () => uncreated.InvokeAsync((Action)(() => { }), CancellationToken.None));
        CheckCall("uncreated-func", () => uncreated.InvokeAsync((Func<int>)(() => 42), CancellationToken.None));
        CheckCall("uncreated-canceled", () => uncreated.InvokeAsync((Action)(() => { }), canceled.Token), canceled.Token);
        CheckCall("uncreated-null-action", () => uncreated.InvokeAsync((Action)null!, CancellationToken.None));
        CheckCall("uncreated-null-func", () => uncreated.InvokeAsync((Func<int>)null!, CancellationToken.None));
        CheckCall("uncreated-null-canceled", () => uncreated.InvokeAsync((Action)null!, canceled.Token), canceled.Token);

        using var form = new Form { Text = "Lumina InvokeAsync probe", Width = 240, Height = 160 };
        using var control = new Control();
        using var inherited = new HandlelessControl { Visible = false };
        form.Controls.Add(control);
        form.Controls.Add(inherited);
        form.Show();
        Record("inherited-no-handle", !inherited.IsHandleCreated);
        CheckCall("inherited-action", () => inherited.InvokeAsync((Action)(() => { }), CancellationToken.None));
        CheckCall("inherited-canceled", () => inherited.InvokeAsync((Action)(() => { }), canceled.Token), canceled.Token);
        CheckCall("created-null-action", () => control.InvokeAsync((Action)null!, CancellationToken.None));
        CheckCall("created-null-func", () => control.InvokeAsync((Func<int>)null!, CancellationToken.None));
        CheckCall("created-null-canceled", () => control.InvokeAsync((Action)null!, canceled.Token), canceled.Token);

        bool called = false;
        Task queued = control.InvokeAsync((Action)(() => { called = true; OnUi(); }), CancellationToken.None);
        Record("ui-action-deferred", !called && !queued.IsCompleted);
        Wait(queued);
        Record("ui-action-completed", called && queued.IsCompletedSuccessfully);
        bool legacyCalled = false;
        Task legacy = control.InvokeAsync((Action)(() => { legacyCalled = true; OnUi(); }));
        Record("single-action-deferred", !legacyCalled && !legacy.IsCompleted);
        Wait(legacy);
        Record("single-action-completed", legacyCalled && legacy.IsCompletedSuccessfully);
        Task<int> result = control.InvokeAsync((Func<int>)(() => { OnUi(); return 42; }), CancellationToken.None);
        Record("ui-func-deferred", !result.IsCompleted);
        Wait(result);
        Record("ui-func-result", result.GetAwaiter().GetResult());
        object value = new();
        Task<object> identity = control.InvokeAsync((Func<object>)(() => value), CancellationToken.None);
        Wait(identity);
        Record("func-result-identity", ReferenceEquals(value, identity.GetAwaiter().GetResult()));
        Task<string?> nullResult = control.InvokeAsync((Func<string?>)(() => null), CancellationToken.None);
        Wait(nullResult);
        Record("func-null-result", nullResult.GetAwaiter().GetResult() is null);

        List<int> order = [];
        IAsyncResult first = control.BeginInvoke(() => order.Add(1));
        Task second = control.InvokeAsync((Action)(() => order.Add(2)), CancellationToken.None);
        IAsyncResult third = control.BeginInvoke(() => order.Add(3));
        Task<int> fourth = control.InvokeAsync((Func<int>)(() => { order.Add(4); return 4; }), CancellationToken.None);
        Record("queue-deferred", order.Count == 0);
        Wait(fourth);
        Record("queue-fifo", string.Join(",", order));
        Record("queue-all-complete", first.IsCompleted && second.IsCompletedSuccessfully && third.IsCompleted);

        RunWorker(() =>
        {
            CallerContext.Value = "worker-context";
            Task workerAction = control.InvokeAsync((Action)OnUi, CancellationToken.None);
            workerAction.GetAwaiter().GetResult();
            Record("worker-action-ui", workerAction.IsCompletedSuccessfully);
            Task<string?> workerContext = control.InvokeAsync((Func<string?>)(() => { OnUi(); return CallerContext.Value; }), CancellationToken.None);
            Record("worker-execution-context", workerContext.GetAwaiter().GetResult());
            Task<int> workerResult = control.InvokeAsync((Func<int>)(() => { OnUi(); return 43; }), CancellationToken.None);
            Record("worker-func-result", workerResult.GetAwaiter().GetResult());
            Task formAction = form.InvokeAsync((Action)OnUi, CancellationToken.None);
            formAction.GetAwaiter().GetResult();
            Record("form-action-ui", formAction.IsCompletedSuccessfully);
            Record("form-func-result", form.InvokeAsync((Func<int>)(() => { OnUi(); return 44; }), CancellationToken.None).GetAwaiter().GetResult());
        });

        CheckCall("action-error", () => control.InvokeAsync((Action)(() => throw new DispatchException()), CancellationToken.None));
        CheckCall("func-error", () => control.InvokeAsync((Func<int>)(() => throw new DispatchException()), CancellationToken.None));
        CheckCall("action-outer-error", () => control.InvokeAsync((Action)(() => throw new InvalidOperationException("outer", new DispatchException())), CancellationToken.None));
        CheckCall("func-target-error", () => control.InvokeAsync((Func<int>)(() => throw new TargetInvocationException(new DispatchException())), CancellationToken.None));
        CheckCall("callback-oce", () => control.InvokeAsync((Action)(() => throw new OperationCanceledException(canceled.Token)), CancellationToken.None), canceled.Token);
        Record("task-errors-thread-exceptions", s_threadExceptions);

        bool preCanceledCalled = false;
        CheckCall("precanceled-action", () => control.InvokeAsync((Action)(() => preCanceledCalled = true), canceled.Token), canceled.Token);
        CheckCall("precanceled-func", () => control.InvokeAsync((Func<int>)(() => { preCanceledCalled = true; return 1; }), canceled.Token), canceled.Token);
        Record("precanceled-func-default", control.InvokeAsync((Func<int>)(() => 1), canceled.Token).GetAwaiter().GetResult());
        Record("precanceled-reference-default", control.InvokeAsync((Func<object>)(() => new object()), canceled.Token).GetAwaiter().GetResult() is null);
        Record("precanceled-callback-skipped", !preCanceledCalled);
        using (var pending = new CancellationTokenSource())
        {
            bool pendingCalled = false;
            Task pendingTask = control.InvokeAsync((Action)(() => pendingCalled = true), pending.Token);
            pending.Cancel();
            CheckTask("pending-cancel-action", pendingTask, pending.Token);
            Pump();
            Record("pending-cancel-skipped", !pendingCalled);
        }
        using (var pending = new CancellationTokenSource())
        {
            bool pendingCalled = false;
            Task<int> pendingTask = control.InvokeAsync((Func<int>)(() => { pendingCalled = true; return 1; }), pending.Token);
            pending.Cancel();
            CheckTask("pending-cancel-func", pendingTask, pending.Token);
            Pump();
            Record("pending-func-cancel-skipped", !pendingCalled);
        }
        using (var inCallback = new CancellationTokenSource())
        {
            bool finished = false;
            Task callbackTask = control.InvokeAsync((Action)(() => { inCallback.Cancel(); finished = true; }), inCallback.Token);
            CheckTask("cancel-inside-action", callbackTask, inCallback.Token);
            Record("cancel-inside-action-finished", finished);
        }
        using (var inCallback = new CancellationTokenSource())
        {
            Task<int> callbackTask = control.InvokeAsync((Func<int>)(() => { inCallback.Cancel(); return 45; }), inCallback.Token);
            CheckTask("cancel-inside-func", callbackTask, inCallback.Token);
            Record("cancel-inside-func-result", callbackTask.IsCompletedSuccessfully ? callbackTask.GetAwaiter().GetResult() : -1);
        }
        using (var after = new CancellationTokenSource())
        {
            Task completed = control.InvokeAsync((Action)(() => { }), after.Token);
            Wait(completed);
            after.Cancel();
            Record("cancel-after-complete-unchanged", completed.IsCompletedSuccessfully);
        }
#if !REFERENCE
        CheckLegacyTaskCallbacks(control);
#endif

        using var doomed = new Control();
        form.Controls.Add(doomed);
        bool doomedCalled = false;
        using var abandonCancellation = new CancellationTokenSource();
        Task abandoned = doomed.InvokeAsync((Action)(() => doomedCalled = true), abandonCancellation.Token);
        doomed.Dispose();
        Pump();
        Record("disposed-pending-status", abandoned.Status);
        Record("disposed-pending-skipped", !doomedCalled);
        abandonCancellation.Cancel();
        CheckTask("disposed-pending-then-cancel", abandoned, abandonCancellation.Token);
        CheckCall("disposed-action", () => doomed.InvokeAsync((Action)(() => { }), CancellationToken.None));
        CheckCall("disposed-func", () => doomed.InvokeAsync((Func<int>)(() => 42), CancellationToken.None));
        CheckCall("disposed-canceled", () => doomed.InvokeAsync((Action)(() => { }), canceled.Token), canceled.Token);
        CheckCall("disposed-null-action", () => doomed.InvokeAsync((Action)null!, CancellationToken.None));
        CheckCall("disposed-null-func", () => doomed.InvokeAsync((Func<int>)null!, CancellationToken.None));
        CheckCall("disposed-null-canceled", () => doomed.InvokeAsync((Action)null!, canceled.Token), canceled.Token);
        form.Dispose();
        CheckCall("form-disposed-action", () => form.InvokeAsync((Action)(() => { }), CancellationToken.None));
        Record("final-thread-exceptions", s_threadExceptions);
        Console.WriteLine(string.Join(Environment.NewLine, Results));
    }

    private static void CheckCall(string name, Func<Task> call, CancellationToken token = default)
    {
        Task task;
        try { task = call(); }
        catch (Exception exception) { Record(name, "throw:" + Describe(exception, token)); return; }
        CheckTask(name, task, token);
    }

#if !REFERENCE
    private static void CheckLegacyTaskCallbacks(Control control)
    {
        using var cancellation = new CancellationTokenSource();
        var inner = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        bool called = false;
        Task task = control.InvokeAsync((Func<CancellationToken, Task>)(token =>
        {
            OnUi();
            Require(token == cancellation.Token, "Task callback token identity");
            called = true;
            return inner.Task;
        }), cancellation.Token);
        Require(!called && !task.IsCompleted, "Task callback is queued");
        Pump();
        Require(called && !task.IsCompleted, "Task callback waits for its returned task");
        inner.SetResult();
        Wait(task);
        task.GetAwaiter().GetResult();

        var expected = new DispatchException();
        CheckFailure(control.InvokeAsync((Func<CancellationToken, Task>)(_ => throw expected), CancellationToken.None), expected);
        CheckFailure(control.InvokeAsync((Func<CancellationToken, Task>)(_ => Task.FromException(expected)), CancellationToken.None), expected);
        using var pending = new CancellationTokenSource();
        bool canceledCalled = false;
        Task pendingTask = control.InvokeAsync((Func<CancellationToken, Task>)(_ => { canceledCalled = true; return Task.CompletedTask; }), pending.Token);
        pending.Cancel();
        Wait(pendingTask);
        Require(pendingTask.IsCanceled, "Queued Task callback cancels");
        Pump();
        Require(!canceledCalled, "Canceled Task callback is skipped");

        bool workerCalled = false;
        RunWorker(() => control.InvokeAsync((Func<CancellationToken, Task>)(_ =>
        {
            OnUi();
            workerCalled = true;
            return Task.CompletedTask;
        }), CancellationToken.None).GetAwaiter().GetResult());
        Require(workerCalled, "Worker Task callback reaches the UI queue");
        Require(s_threadExceptions == 0, "Task callback failures do not raise ThreadException");
        Console.Error.WriteLine("legacy-task-checks=passed");

        static void CheckFailure(Task failed, Exception expected)
        {
            Wait(failed);
            try { failed.GetAwaiter().GetResult(); throw new InvalidOperationException("Expected Task callback failure."); }
            catch (DispatchException actual) { Require(ReferenceEquals(actual, expected), "Task callback preserves exception identity"); }
        }

        static void Require(bool condition, string name)
        {
            if (!condition) { throw new InvalidOperationException("Legacy InvokeAsync check failed: " + name); }
        }
    }
#endif

    private static void CheckTask(string name, Task task, CancellationToken token = default)
    {
        Wait(task);
        try { task.GetAwaiter().GetResult(); Record(name, "task:" + task.Status); }
        catch (Exception exception) { Record(name, "task:" + task.Status + ":" + Describe(exception, token)); }
    }

    private static string Describe(Exception exception, CancellationToken token)
        => exception.GetType().Name + ":" + (exception is ArgumentException argument ? argument.ParamName : string.Empty)
            + ":" + exception.InnerException?.GetType().Name
            + (exception is OperationCanceledException canceled ? ":token=" + (canceled.CancellationToken == token) : string.Empty);

    private static void OnUi()
    {
        if (Environment.CurrentManagedThreadId != s_uiThread) { throw new InvalidOperationException("Callback did not execute on the HWND creating thread."); }
    }

    private static void Wait(Task task)
    {
        var timeout = Stopwatch.StartNew();
        while (!task.IsCompleted)
        {
            if (timeout.Elapsed > TimeSpan.FromSeconds(10)) { throw new TimeoutException("InvokeAsync task did not complete."); }
            Pump();
            Thread.Sleep(1);
        }
    }

    private static void RunWorker(Action action)
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
            if (timeout.Elapsed > TimeSpan.FromSeconds(15)) { throw new TimeoutException("InvokeAsync worker timed out."); }
            Pump();
        }
        worker.Join();
        if (error is not null) { throw new InvalidOperationException("Worker failed.", error); }
    }

    private static void Pump()
    {
        while (PeekMessageW(out NativeMessage message, 0, 0, 0, 1))
        {
            TranslateMessage(in message);
            DispatchMessageW(in message);
        }
    }

    private static void Record(string name, object? value)
    {
        string result = name + "=" + (value?.ToString() ?? "null");
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
