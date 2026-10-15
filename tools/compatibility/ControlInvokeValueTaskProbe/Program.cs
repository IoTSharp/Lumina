using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks.Sources;
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
        CheckBoundaries("uncreated", uncreated, canceled.Token);
        using var form = new Form { Text = "Lumina ValueTask probe", Width = 240, Height = 160 };
        using var control = new Control();
        using var inherited = new HandlelessControl { Visible = false };
        form.Controls.Add(control);
        form.Controls.Add(inherited);
        form.Show();
        Record("inherited-no-handle", !inherited.IsHandleCreated);
        CheckBoundaries("inherited", inherited, canceled.Token);
        CheckCall("created-null-void", () => control.InvokeAsync((Func<CancellationToken, ValueTask>)null!));
        CheckCall("created-null-result", () => control.InvokeAsync((Func<CancellationToken, ValueTask<int>>)null!));
        CheckCall("created-null-canceled", () => control.InvokeAsync((Func<CancellationToken, ValueTask>)null!, canceled.Token), canceled.Token);

        bool called = false;
        Task queued = control.InvokeAsync((Func<CancellationToken, ValueTask>)(token =>
        {
            OnUi();
            called = true;
            Record("ui-token-none", token == CancellationToken.None);
            return ValueTask.CompletedTask;
        }));
        Record("ui-void-deferred", !called && !queued.IsCompleted);
        CheckTask("ui-void-completed", queued);
        Task<int> result = control.InvokeAsync((Func<CancellationToken, ValueTask<int>>)(_ => { OnUi(); return ValueTask.FromResult(42); }));
        Record("ui-result-deferred", !result.IsCompleted);
        Wait(result);
        Record("ui-result", result.GetAwaiter().GetResult());
        object value = new();
        Task<object> identity = control.InvokeAsync((Func<CancellationToken, ValueTask<object>>)(_ => ValueTask.FromResult(value)));
        Wait(identity);
        Record("result-identity", ReferenceEquals(value, identity.GetAwaiter().GetResult()));
        Task<string?> nullResult = control.InvokeAsync((Func<CancellationToken, ValueTask<string?>>)(_ => ValueTask.FromResult<string?>(null)));
        Wait(nullResult);
        Record("null-result", nullResult.GetAwaiter().GetResult() is null);

        RunSources(control);
        RunErrors(control, canceled.Token);
        RunCancellation(control, canceled.Token);

        RunWorker(() =>
        {
            using var workerCancellation = new CancellationTokenSource();
            CallerContext.Value = "worker-context";
            Task worker = control.InvokeAsync((Func<CancellationToken, ValueTask>)(token =>
            {
                OnUi();
                Record("worker-token", token == workerCancellation.Token);
                Record("worker-execution-context", CallerContext.Value);
                return ValueTask.CompletedTask;
            }), workerCancellation.Token);
            worker.GetAwaiter().GetResult();
            Record("worker-void-ui", worker.IsCompletedSuccessfully);
            Task<int> workerResult = control.InvokeAsync((Func<CancellationToken, ValueTask<int>>)(_ => { OnUi(); return ValueTask.FromResult(43); }));
            Record("worker-result", workerResult.GetAwaiter().GetResult());
            form.InvokeAsync((Func<CancellationToken, ValueTask>)(_ => { OnUi(); return ValueTask.CompletedTask; })).GetAwaiter().GetResult();
            Record("form-void-ui", true);
            Record("form-result", form.InvokeAsync((Func<CancellationToken, ValueTask<int>>)(_ => { OnUi(); return ValueTask.FromResult(44); })).GetAwaiter().GetResult());
        });

        Task asyncLambda = control.InvokeAsync(async token => { OnUi(); await Task.Delay(1).ConfigureAwait(false); });
        CheckTask("async-lambda-void", asyncLambda);
        Task<int> asyncResultLambda = control.InvokeAsync(async token => { OnUi(); await Task.Delay(1).ConfigureAwait(false); return 45; });
        Wait(asyncResultLambda);
        Record("async-lambda-result", asyncResultLambda.GetAwaiter().GetResult());
        Task formAsyncLambda = form.InvokeAsync(async token => { OnUi(); await Task.Delay(1).ConfigureAwait(false); });
        CheckTask("form-async-lambda", formAsyncLambda);
#if !REFERENCE
        CheckLegacyOverloadSelection(form);
#endif

        using var doomed = new Control();
        form.Controls.Add(doomed);
        bool doomedCalled = false;
        using var abandonCancellation = new CancellationTokenSource();
        Task abandoned = doomed.InvokeAsync((Func<CancellationToken, ValueTask>)(_ => { doomedCalled = true; return ValueTask.CompletedTask; }), abandonCancellation.Token);
        doomed.Dispose();
        Pump();
        Record("disposed-pending-status", abandoned.Status);
        Record("disposed-pending-skipped", !doomedCalled);
        abandonCancellation.Cancel();
        CheckTask("disposed-pending-then-cancel", abandoned, abandonCancellation.Token);
        CheckBoundaries("disposed", doomed, canceled.Token);
        form.Dispose();
        CheckCall("form-disposed-void", () => form.InvokeAsync((Func<CancellationToken, ValueTask>)(_ => ValueTask.CompletedTask)));
        Record("thread-exceptions", s_threadExceptions);
        Console.WriteLine(string.Join(Environment.NewLine, Results));
    }

    private static void CheckBoundaries(string prefix, Control control, CancellationToken canceled)
    {
        CheckCall(prefix + "-void", () => control.InvokeAsync((Func<CancellationToken, ValueTask>)(_ => ValueTask.CompletedTask)));
        CheckCall(prefix + "-result", () => control.InvokeAsync((Func<CancellationToken, ValueTask<int>>)(_ => ValueTask.FromResult(1))));
        CheckCall(prefix + "-canceled-void", () => control.InvokeAsync((Func<CancellationToken, ValueTask>)(_ => ValueTask.CompletedTask), canceled), canceled);
        CheckCall(prefix + "-canceled-result", () => control.InvokeAsync((Func<CancellationToken, ValueTask<int>>)(_ => ValueTask.FromResult(1)), canceled), canceled);
        CheckCall(prefix + "-null-void", () => control.InvokeAsync((Func<CancellationToken, ValueTask>)null!));
        CheckCall(prefix + "-null-result", () => control.InvokeAsync((Func<CancellationToken, ValueTask<int>>)null!));
        CheckCall(prefix + "-null-canceled", () => control.InvokeAsync((Func<CancellationToken, ValueTask>)null!, canceled), canceled);
    }

    private static void RunSources(Control control)
    {
        foreach (bool delayed in new[] { false, true })
        {
            string prefix = delayed ? "source-delayed" : "source-sync";
            var source = new SingleUseSource<object>();
            object value = new();
            if (!delayed) { source.SetResult(value); }
            Task task = control.InvokeAsync((Func<CancellationToken, ValueTask>)(_ => { OnUi(); return source.VoidTask; }));
            Pump();
            if (delayed)
            {
                Record(prefix + "-void-waits", !task.IsCompleted && source.GetResultCount == 0);
                source.SetResult(value);
            }
            CheckTask(prefix + "-void", task);
            Record(prefix + "-void-consumed", source.GetResultCount);

            source = new SingleUseSource<object>();
            if (!delayed) { source.SetResult(value); }
            SingleUseSource<object> resultSource = source;
            Task<object> result = control.InvokeAsync((Func<CancellationToken, ValueTask<object>>)(_ => { OnUi(); return resultSource.Task; }));
            Pump();
            if (delayed)
            {
                Record(prefix + "-result-waits", !result.IsCompleted && source.GetResultCount == 0);
                source.SetResult(value);
            }
            Wait(result);
            Record(prefix + "-result-identity", ReferenceEquals(value, result.GetAwaiter().GetResult()));
            Record(prefix + "-result-consumed", source.GetResultCount);
        }
        var delayedSource = new SingleUseSource<int>();
        var expected = new DispatchException();
        Task<int> fault = control.InvokeAsync((Func<CancellationToken, ValueTask<int>>)(_ => delayedSource.Task));
        Pump();
        delayedSource.SetException(expected);
        CheckIdentity("source-error", fault, expected);
        Record("source-error-consumed", delayedSource.GetResultCount);
    }

    private static void RunErrors(Control control, CancellationToken canceled)
    {
        var expected = new DispatchException();
        CheckIdentity("void-sync-error", control.InvokeAsync((Func<CancellationToken, ValueTask>)(_ => throw expected)), expected);
        CheckIdentity("result-sync-error", control.InvokeAsync((Func<CancellationToken, ValueTask<int>>)(_ => throw expected)), expected);
        CheckIdentity("void-valuetask-error", control.InvokeAsync((Func<CancellationToken, ValueTask>)(_ => ValueTask.FromException(expected))), expected);
        CheckIdentity("result-valuetask-error", control.InvokeAsync((Func<CancellationToken, ValueTask<int>>)(_ => ValueTask.FromException<int>(expected))), expected);
        var outer = new InvalidOperationException("outer", expected);
        CheckIdentity("void-outer-error", control.InvokeAsync((Func<CancellationToken, ValueTask>)(_ => ValueTask.FromException(outer))), outer);
        var target = new TargetInvocationException(expected);
        CheckIdentity("result-target-error", control.InvokeAsync((Func<CancellationToken, ValueTask<int>>)(_ => ValueTask.FromException<int>(target))), target);
        var oce = new OperationCanceledException(canceled);
        CheckIdentity("void-callback-oce", control.InvokeAsync((Func<CancellationToken, ValueTask>)(_ => ValueTask.FromException(oce))), oce, canceled);
        CheckIdentity("result-callback-oce", control.InvokeAsync((Func<CancellationToken, ValueTask<int>>)(_ => ValueTask.FromException<int>(oce))), oce, canceled);
        CheckCall("void-canceled-valuetask", () => control.InvokeAsync((Func<CancellationToken, ValueTask>)(_ => ValueTask.FromCanceled(canceled))), canceled);
        CheckCall("result-canceled-valuetask", () => control.InvokeAsync((Func<CancellationToken, ValueTask<int>>)(_ => ValueTask.FromCanceled<int>(canceled))), canceled);
    }

    private static void RunCancellation(Control control, CancellationToken canceled)
    {
        bool called = false;
        CheckCall("precanceled-void", () => control.InvokeAsync((Func<CancellationToken, ValueTask>)(_ => { called = true; return ValueTask.CompletedTask; }), canceled), canceled);
        Task<int> preResult = control.InvokeAsync((Func<CancellationToken, ValueTask<int>>)(_ => { called = true; return ValueTask.FromResult(1); }), canceled);
        CheckTask("precanceled-result", preResult, canceled);
        Record("precanceled-result-value", preResult.IsCompletedSuccessfully ? preResult.Result : -1);
        Record("precanceled-skipped", !called);
        using (var pending = new CancellationTokenSource())
        {
            bool pendingCalled = false;
            Task task = control.InvokeAsync((Func<CancellationToken, ValueTask>)(_ => { pendingCalled = true; return ValueTask.CompletedTask; }), pending.Token);
            pending.Cancel();
            CheckTask("pending-cancel-void", task, pending.Token);
            Pump();
            Record("pending-cancel-void-skipped", !pendingCalled);
        }
        using (var pending = new CancellationTokenSource())
        {
            bool pendingCalled = false;
            Task<int> task = control.InvokeAsync((Func<CancellationToken, ValueTask<int>>)(_ => { pendingCalled = true; return ValueTask.FromResult(1); }), pending.Token);
            pending.Cancel();
            CheckTask("pending-cancel-result", task, pending.Token);
            Pump();
            Record("pending-cancel-result-skipped", !pendingCalled);
        }
        using (var inside = new CancellationTokenSource())
        {
            bool finished = false;
            Task task = control.InvokeAsync((Func<CancellationToken, ValueTask>)(token => { Record("inside-token", token == inside.Token); inside.Cancel(); finished = true; return ValueTask.CompletedTask; }), inside.Token);
            CheckTask("cancel-inside-void", task, inside.Token);
            Record("cancel-inside-void-finished", finished);
        }
        using (var inside = new CancellationTokenSource())
        {
            Task<int> task = control.InvokeAsync((Func<CancellationToken, ValueTask<int>>)(_ => { inside.Cancel(); return ValueTask.FromResult(46); }), inside.Token);
            CheckTask("cancel-inside-result", task, inside.Token);
            Record("cancel-inside-result-value", task.IsCompletedSuccessfully ? task.Result : -1);
        }
        using (var pending = new CancellationTokenSource())
        {
            var source = new SingleUseSource<int>();
            Task task = control.InvokeAsync((Func<CancellationToken, ValueTask>)(_ => source.VoidTask), pending.Token);
            Pump();
            pending.Cancel();
            Record("cancel-awaiting-void-completes-before-source", CompletesBriefly(task));
            source.SetResult(1);
            CheckTask("cancel-awaiting-void", task, pending.Token);
            Until(() => source.GetResultCount == 1);
            Record("cancel-awaiting-void-consumed", source.GetResultCount);
            Record("cancel-awaiting-void-final-status", task.Status);
        }
        using (var pending = new CancellationTokenSource())
        {
            var source = new SingleUseSource<int>();
            Task<int> task = control.InvokeAsync((Func<CancellationToken, ValueTask<int>>)(_ => source.Task), pending.Token);
            Pump();
            pending.Cancel();
            Record("cancel-awaiting-result-completes-before-source", CompletesBriefly(task));
            source.SetResult(47);
            CheckTask("cancel-awaiting-result", task, pending.Token);
            Until(() => source.GetResultCount == 1);
            Record("cancel-awaiting-result-consumed", source.GetResultCount);
            Record("cancel-awaiting-result-final-status", task.Status);
        }
        using (var after = new CancellationTokenSource())
        {
            Task completed = control.InvokeAsync((Func<CancellationToken, ValueTask>)(_ => ValueTask.CompletedTask), after.Token);
            Wait(completed);
            after.Cancel();
            Record("cancel-after-complete", completed.IsCompletedSuccessfully);
        }
    }

#if !REFERENCE
    private static void CheckLegacyOverloadSelection(Form form)
    {
        using var derived = new LegacyOverrideControl();
        form.Controls.Add(derived);
        Task lambda = derived.InvokeAsync(async token => { OnUi(); await Task.Delay(1).ConfigureAwait(false); }, CancellationToken.None);
        Wait(lambda);
        lambda.GetAwaiter().GetResult();
        Require(!derived.LegacyCalled, "async token lambda prefers the standard ValueTask overload on derived controls");
        Task legacy = derived.InvokeAsync((Func<CancellationToken, Task>)(_ => { OnUi(); return Task.CompletedTask; }), CancellationToken.None);
        Wait(legacy);
        legacy.GetAwaiter().GetResult();
        Require(derived.LegacyCalled, "explicit Task delegates preserve virtual override dispatch");
        Console.Error.WriteLine("legacy-overload-checks=passed");
    }

    private sealed class LegacyOverrideControl : Control
    {
        internal bool LegacyCalled;
        public override Task InvokeAsync(Func<CancellationToken, Task> action, CancellationToken cancellationToken)
        {
            LegacyCalled = true;
            return base.InvokeAsync(action, cancellationToken);
        }
    }
#endif

    private sealed class SingleUseSource<T> : IValueTaskSource<T>, IValueTaskSource
    {
        private ManualResetValueTaskSourceCore<T> _core = new() { RunContinuationsAsynchronously = true };
        private int _getResultCount;
        internal int GetResultCount => Volatile.Read(ref _getResultCount);
        internal ValueTask<T> Task => new(this, _core.Version);
        internal ValueTask VoidTask => new(this, _core.Version);
        internal void SetResult(T value) => _core.SetResult(value);
        internal void SetException(Exception error) => _core.SetException(error);
        public T GetResult(short token)
        {
            Require(Interlocked.Increment(ref _getResultCount) == 1, "ValueTask source must only be consumed once");
            return _core.GetResult(token);
        }
        void IValueTaskSource.GetResult(short token) => _ = GetResult(token);
        public ValueTaskSourceStatus GetStatus(short token) => _core.GetStatus(token);
        public void OnCompleted(Action<object?> continuation, object? state, short token, ValueTaskSourceOnCompletedFlags flags)
            => _core.OnCompleted(continuation, state, token, flags);
    }

    private static void CheckIdentity(string name, Task task, Exception expected, CancellationToken token = default)
    {
        Wait(task);
        try { task.GetAwaiter().GetResult(); Record(name, "unexpected-success"); }
        catch (Exception actual) { Record(name, "task:" + task.Status + ":" + Describe(actual, token) + ":identity=" + ReferenceEquals(actual, expected)); }
    }

    private static void CheckCall(string name, Func<Task> call, CancellationToken token = default)
    {
        Task task;
        try { task = call(); }
        catch (Exception exception) { Record(name, "throw:" + Describe(exception, token)); return; }
        CheckTask(name, task, token);
    }

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

    private static void OnUi() => Require(Environment.CurrentManagedThreadId == s_uiThread, "callback starts on the HWND creating thread");
    private static void Require(bool condition, string name)
    {
        if (!condition) { throw new InvalidOperationException("ValueTask probe failed: " + name); }
    }
    private static void Wait(Task task) => Until(() => task.IsCompleted);
    private static bool CompletesBriefly(Task task)
    {
        var timeout = Stopwatch.StartNew();
        while (!task.IsCompleted && timeout.Elapsed < TimeSpan.FromMilliseconds(200))
        {
            Pump();
            Thread.Sleep(1);
        }
        return task.IsCompleted;
    }
    private static void Until(Func<bool> condition)
    {
        var timeout = Stopwatch.StartNew();
        while (!condition())
        {
            if (timeout.Elapsed > TimeSpan.FromSeconds(10)) { throw new TimeoutException("ValueTask probe timed out."); }
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
        Until(() => completed.Wait(1));
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
