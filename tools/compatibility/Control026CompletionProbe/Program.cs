using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks.Sources;
#if REFERENCE
using System.Windows.Forms;
#else
using Lumina.Forms;
#endif

// Each scenario runs in a separate STA process. Pending tasks, exception routes,
// and native/managed lifetime differences are observations, never normalized.
internal static class Program
{
    private static readonly List<string> Events = [];
    private static readonly List<Exception> ThreadErrors = [];
    private static readonly Exception NativeFailure = new InvalidOperationException("native-close-failure");
    private static readonly Exception ManagedFailure = new InvalidOperationException("managed-cleanup-failure");
    private static readonly Exception CallbackFailure = new InvalidOperationException("callback-failure");
    private static int s_uiThread;

    [STAThread]
    private static void Main(string[] args)
    {
        Console.OutputEncoding = new System.Text.UTF8Encoding(false);
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
        s_uiThread = Environment.CurrentManagedThreadId;
        Console.Error.WriteLine("runtime=" + RuntimeInformation.FrameworkDescription);
#if REFERENCE
        string? version = typeof(Control).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        if (version is null || !version.StartsWith("10.0.11+", StringComparison.Ordinal))
            throw new InvalidOperationException("Expected WinForms 10.0.11, found " + version);
        Console.Error.WriteLine("reference-assembly=" + version);
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
#endif
        WindowsFormsSynchronizationContext.AutoInstall = false;
        SynchronizationContext.SetSynchronizationContext(null);
        Application.ThreadException += OnThreadException;
        if (args.Length != 1) throw new ArgumentException("Specify one Control 026 scenario.");
        string scenario = args[0];
        Record("scenario", scenario);
        try
        {
            if (scenario.StartsWith("queued-", StringComparison.Ordinal)) Queued(scenario[7..], false);
            else if (scenario.StartsWith("started-", StringComparison.Ordinal)) Started(scenario[8..], false);
            else if (scenario.StartsWith("sync-", StringComparison.Ordinal)) Synchronous(scenario[5..]);
            else if (scenario.StartsWith("dispose-", StringComparison.Ordinal)) DisposeTree(scenario[8..]);
#if !REFERENCE
            else if (scenario.StartsWith("legacy-queued-", StringComparison.Ordinal)) Queued(scenario[14..], true);
            else if (scenario.StartsWith("legacy-started-", StringComparison.Ordinal)) Started(scenario[15..], true);
#endif
            else throw new ArgumentException("Unknown Control 026 scenario: " + scenario);
        }
        catch (Exception error)
        {
            Record("scenario-error", Describe(error));
            Environment.ExitCode = 2;
        }
        finally
        {
            Record("thread-exception-count", ThreadErrors.Count);
            Record("thread-exception-sequence", string.Join(";", ThreadErrors.Select(Describe)));
            Application.ThreadException -= OnThreadException;
        }
        Record("complete", Environment.ExitCode == 0);
    }

    private static void Queued(string mode, bool legacy)
    {
        using var control = NewControl("target");
        using var tokenSource = new CancellationTokenSource();
        CancellationToken token = mode.Contains("cancel", StringComparison.Ordinal) ? tokenSource.Token : default;
        nint oldHandle = control.Handle;
        List<string> callbacks = [];
        void Called(string kind) => callbacks.Add(kind + ":ui=" + OnUi());
        var tasks = new List<(string Name, Task Task)>();
#if !REFERENCE
        if (legacy)
        {
            tasks.Add(("task", control.InvokeAsync((Func<CancellationToken, Task>)(received =>
            {
                Called("task:token=" + (received == token));
                return Task.CompletedTask;
            }), token)));
        }
        else
#endif
        {
            tasks.Add(("action", control.InvokeAsync((Action)(() => Called("action")), token)));
            tasks.Add(("function", control.InvokeAsync((Func<int>)(() => { Called("function"); return 42; }), token)));
            tasks.Add(("valuetask", control.InvokeAsync((Func<CancellationToken, ValueTask>)(received =>
            {
                Called("valuetask:token=" + (received == token));
                return ValueTask.CompletedTask;
            }), token)));
            tasks.Add(("valuetask-result", control.InvokeAsync((Func<CancellationToken, ValueTask<int>>)(received =>
            {
                Called("valuetask-result:token=" + (received == token));
                return ValueTask.FromResult(43);
            }), token)));
        }
        Record("before-native", IsWindow(oldHandle));
        Record("accepted-deferred", tasks.All(item => !item.Task.IsCompleted) && callbacks.Count == 0);
        if (mode == "cancel-dispose") tokenSource.Cancel();
        Record("close-call", Call(() =>
        {
            switch (mode)
            {
                case "dispose": case "cancel-dispose": case "dispose-cancel": control.Dispose(); break;
                case "destroy": control.DirectDestroy(); break;
                case "external": Record("destroy-window", DestroyWindow(oldHandle)); break;
                case "recreate": control.Rebuild(); break;
                default: throw new ArgumentException("Unknown queue mode " + mode);
            }
        }));
        Record("after-close-native", IsWindow(oldHandle));
        Record("after-close-managed", State(control));
        if (mode == "dispose-cancel")
        {
            ObserveTasks("before-cancel", tasks, token);
            Record("cancel-call", Call(tokenSource.Cancel));
        }
        ObserveTasks("after", tasks, token);
        Record("callbacks", string.Join(";", callbacks));
        if (mode == "recreate") Record("new-native", control.IsHandleCreated && IsWindow(control.Handle));
        Record("final-dispose", Call(control.Dispose));
        Record("final-native", IsWindow(oldHandle));
    }

    private static void Started(string mode, bool legacy)
    {
        using var control = NewControl("target");
        using var tokenSource = new CancellationTokenSource();
        using var callbackTokenSource = new CancellationTokenSource();
        callbackTokenSource.Cancel();
        var callbackCancellation = new OperationCanceledException("callback-cancellation", callbackTokenSource.Token);
        nint oldHandle = control.Handle;
        var first = new SingleUseSource<int>();
        var second = new SingleUseSource<int>();
        var legacySource = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        int started = 0;
        List<string> callbacks = [];
        void Called(string name, CancellationToken token)
        {
            started++;
            callbacks.Add(name + ":ui=" + OnUi() + ":token=" + (token == tokenSource.Token));
        }
        var tasks = new List<(string Name, Task Task)>();
#if !REFERENCE
        if (legacy)
        {
            tasks.Add(("task", control.InvokeAsync((Func<CancellationToken, Task>)(token =>
            {
                Called("task", token);
                return legacySource.Task;
            }), tokenSource.Token)));
        }
        else
#endif
        {
            tasks.Add(("valuetask", control.InvokeAsync((Func<CancellationToken, ValueTask>)(token =>
            {
                Called("valuetask", token);
                return first.VoidTask;
            }), tokenSource.Token)));
            tasks.Add(("valuetask-result", control.InvokeAsync((Func<CancellationToken, ValueTask<int>>)(token =>
            {
                Called("valuetask-result", token);
                return second.Task;
            }), tokenSource.Token)));
        }
        RequireUntil(() => started == tasks.Count, "callbacks did not start");
        Record("callbacks", string.Join(";", callbacks));
        Record("close-call", Call(control.Dispose));
        Record("after-close-native", IsWindow(oldHandle));
        ObserveTasks("before-source", tasks, tokenSource.Token);
        if (mode == "cancel") tokenSource.Cancel();
        Exception? sourceError = mode == "error" ? CallbackFailure : mode == "oce" ? callbackCancellation : null;
        if (sourceError is null)
        {
            first.SetResult(51);
            second.SetResult(52);
            legacySource.SetResult();
        }
        else
        {
            first.SetException(sourceError);
            second.SetException(sourceError);
            legacySource.SetException(sourceError);
        }
        if (!legacy) RequireUntil(() => first.GetResultCount > 0 && second.GetResultCount > 0, "ValueTask source not consumed");
        ObserveTasks("after-source", tasks, mode == "oce" ? callbackTokenSource.Token : tokenSource.Token, sourceError);
        if (!legacy) Record("source-consumption", first.GetResultCount + "," + second.GetResultCount);
        Record("final-managed", State(control));
    }

    private static void Synchronous(string mode)
    {
        foreach (bool result in new[] { false, true })
        {
            using var control = NewControl("target");
            nint handle = control.Handle;
            int calls = 0;
            int Callback()
            {
                calls++;
                Record((result ? "function" : "action") + "-ui", OnUi());
                control.Dispose();
                if (mode == "error") throw CallbackFailure;
                return 63;
            }
            Task task = result
                ? control.InvokeAsync((Func<int>)Callback, CancellationToken.None)
                : control.InvokeAsync((Action)(() => _ = Callback()), CancellationToken.None);
            string prefix = result ? "function" : "action";
            ObserveTasks(prefix, [(prefix, task)], default, mode == "error" ? CallbackFailure : null);
            Record(prefix + "-calls", calls);
            Record(prefix + "-native", IsWindow(handle));
        }
    }

    private static void DisposeTree(string mode)
    {
        var parent = NewControl("parent");
        var target = NewControl("target");
        var first = NewControl("first");
        var second = NewControl("second");
        var container = new ProbeContainer();
        parent.Controls.Add(target);
        target.Controls.AddRange([first, second]);
        container.Add(target, "target-site");
        container.Add(first, "first-site");
        container.Add(second, "second-site");
        Control.ControlCollection retained = target.Controls;
        ProbeControl[] controls = [parent, target, first, second];
        nint[] handles = controls.Select(control => control.Handle).ToArray();
        bool injectDisposedFailure = true;
        foreach (ProbeControl control in controls)
        {
            control.Disposed += (_, _) =>
            {
                Events.Add(control.Name + ":disposed:" + State(control));
                if (injectDisposedFailure && control == target && mode.EndsWith("disposed", StringComparison.Ordinal)) throw ManagedFailure;
            };
        }
        target.DestroyFailure = mode == "native-before" ? "before" : mode.StartsWith("native-after", StringComparison.Ordinal) ? "after" : "";
        target.HandleEventFailure = mode == "native-event";
        target.ParentFailure = mode.EndsWith("parent", StringComparison.Ordinal);
        first.DisposeFailure = mode.EndsWith("child-before", StringComparison.Ordinal) ? "before" : mode.EndsWith("child-after", StringComparison.Ordinal) ? "after" : "";
        container.ThrowRemove = mode.EndsWith("site", StringComparison.Ordinal);
        Events.Clear();
        Record("dispose-call", Call(target.Dispose));
        Record("dispose-events", string.Join(";", Events));
        RecordTree("after", controls, handles, retained, container);
        Events.Clear();
        Record("repeat-call", Call(target.Dispose));
        Record("repeat-events", string.Join(";", Events));
        RecordTree("repeat", controls, handles, retained, container);

        // Restore only fixture fault injection. Cleanup is recorded separately;
        // it must not rewrite the state observed after the failing Dispose call.
        foreach (ProbeControl control in controls)
        {
            control.DestroyFailure = "";
            control.HandleEventFailure = false;
            control.ParentFailure = false;
            control.DisposeFailure = "";
        }
        container.ThrowRemove = false;
        injectDisposedFailure = false;
        Events.Clear();
        foreach (ProbeControl control in controls.Reverse())
        {
            Record("cleanup-destroy-" + control.Name, Call(control.DirectDestroy));
            Record("cleanup-dispose-" + control.Name, Call(control.Dispose));
        }
        Record("cleanup-container", Call(container.Dispose));
        Record("cleanup-native", string.Join(",", handles.Select(IsWindow)));
        if (handles.Any(IsWindow)) throw new InvalidOperationException("Probe cleanup left a real HWND alive.");
    }

    private static void RecordTree(string prefix, ProbeControl[] controls, nint[] handles, Control.ControlCollection retained, ProbeContainer container)
    {
        for (int index = 0; index < controls.Length; index++)
            Record(prefix + "-" + controls[index].Name, State(controls[index]) + ":native=" + IsWindow(handles[index])
                + ":mapped=" + ReferenceEquals(Control.FromHandle(handles[index]), controls[index]));
        Record(prefix + "-retained-count", retained.Count);
        Record(prefix + "-container-count", container.Components.Count);
    }

    private static void ObserveTasks(string prefix, List<(string Name, Task Task)> tasks, CancellationToken token, Exception? expected = null)
    {
        bool settled = Until(() => tasks.All(item => item.Task.IsCompleted), 750);
        Record(prefix + "-all-settled", settled);
        foreach ((string name, Task task) in tasks)
        {
            if (!task.IsCompleted) { Record(prefix + "-" + name, "pending-after-750ms:" + task.Status); continue; }
            try
            {
                task.GetAwaiter().GetResult();
                Record(prefix + "-" + name, task.Status + (task is Task<int> result ? ":result=" + result.Result : ""));
            }
            catch (Exception error)
            {
                Record(prefix + "-" + name, task.Status + ":" + Describe(error)
                    + (error is OperationCanceledException canceled ? ":token=" + (canceled.CancellationToken == token) : "")
                    + (expected is not null ? ":identity=" + ReferenceEquals(error, expected) : ""));
            }
        }
    }

    private static string State(Control control) => "disposed=" + control.IsDisposed + ":disposing=" + control.Disposing
        + ":handle=" + control.IsHandleCreated + ":parent=" + (control.Parent?.Name ?? "null")
        + ":children=" + control.Controls.Count + ":site=" + (control.Site?.Name ?? "null");

    private static string Describe(Exception error) => error.GetType().Name + ":" + error.Message
        + ":native-identity=" + ReferenceEquals(error, NativeFailure)
        + ":managed-identity=" + ReferenceEquals(error, ManagedFailure)
        + ":callback-identity=" + ReferenceEquals(error, CallbackFailure)
        + (error is AggregateException aggregate
            ? ":inners=[" + string.Join(";", aggregate.InnerExceptions.Select(Describe)) + "]"
            : error.InnerException is not null ? ":inner=" + Describe(error.InnerException) : "");

    private static string Call(Action action)
    {
        try { action(); return "success"; }
        catch (Exception error) { return Describe(error); }
    }

    private static void OnThreadException(object? sender, ThreadExceptionEventArgs args)
    {
        ThreadErrors.Add(args.Exception);
        Events.Add("thread-exception:" + Describe(args.Exception) + ":ui=" + OnUi());
        Console.Error.WriteLine("thread-exception=" + Describe(args.Exception));
    }

    private static ProbeControl NewControl(string name) => new() { Name = name, Bounds = new(0, 0, 64, 48) };
    private static bool OnUi() => Environment.CurrentManagedThreadId == s_uiThread;
    private static void Record(string name, object? value) => Console.WriteLine(name + "=" + (value?.ToString() ?? "null"));
    private static void RequireUntil(Func<bool> condition, string failure)
    {
        if (!Until(condition, 5000)) throw new TimeoutException(failure);
    }
    private static bool Until(Func<bool> condition, int milliseconds)
    {
        var timer = Stopwatch.StartNew();
        while (!condition() && timer.ElapsedMilliseconds < milliseconds) { Pump(); Thread.Sleep(1); }
        return condition();
    }
    private static void Pump()
    {
        int count = 0;
        while (PeekMessageW(out NativeMessage message, 0, 0, 0, 1))
        {
            if (message.Message == 0x0012) continue;
            if (++count > 4096) throw new InvalidOperationException("Message pump did not become idle.");
            _ = TranslateMessage(in message);
            _ = DispatchMessageW(in message);
        }
    }

    private sealed class ProbeControl : Control
    {
        internal string DestroyFailure = "";
        internal string DisposeFailure = "";
        internal bool ParentFailure;
        internal bool HandleEventFailure;
        internal void DirectDestroy() => DestroyHandle();
        internal void Rebuild() => RecreateHandle();
        protected override void DestroyHandle()
        {
            Program.Events.Add(Name + ":destroy-enter");
            if (DestroyFailure == "before") throw NativeFailure;
            base.DestroyHandle();
            Program.Events.Add(Name + ":destroy-exit");
            if (DestroyFailure == "after") throw NativeFailure;
        }
        protected override void OnHandleDestroyed(EventArgs e)
        {
            Program.Events.Add(Name + ":handle-destroyed");
            base.OnHandleDestroyed(e);
            if (HandleEventFailure) throw NativeFailure;
        }
        protected override void OnParentChanged(EventArgs e)
        {
            Program.Events.Add(Name + ":parent-changed:" + (Parent?.Name ?? "null"));
            base.OnParentChanged(e);
            if (ParentFailure && Parent is null) throw ManagedFailure;
        }
        protected override void Dispose(bool disposing)
        {
            Program.Events.Add(Name + ":dispose-enter:" + disposing);
            if (DisposeFailure == "before") throw ManagedFailure;
            base.Dispose(disposing);
            Program.Events.Add(Name + ":dispose-exit:" + disposing);
            if (DisposeFailure == "after") throw ManagedFailure;
        }
    }

    private sealed class ProbeContainer : Container
    {
        internal bool ThrowRemove;
        public override void Remove(IComponent? component)
        {
            Program.Events.Add("container-remove:" + component?.Site?.Name);
            if (ThrowRemove && component?.Site?.Name == "target-site") throw ManagedFailure;
            base.Remove(component);
        }
    }

    private sealed class SingleUseSource<T> : IValueTaskSource<T>, IValueTaskSource
    {
        private ManualResetValueTaskSourceCore<T> _source = new() { RunContinuationsAsynchronously = true };
        private int _getResultCount;
        internal int GetResultCount => Volatile.Read(ref _getResultCount);
        internal ValueTask<T> Task => new(this, _source.Version);
        internal ValueTask VoidTask => new(this, _source.Version);
        internal void SetResult(T result) => _source.SetResult(result);
        internal void SetException(Exception error) => _source.SetException(error);
        public T GetResult(short token)
        {
            if (Interlocked.Increment(ref _getResultCount) != 1) throw new InvalidOperationException("ValueTask consumed more than once.");
            return _source.GetResult(token);
        }
        void IValueTaskSource.GetResult(short token) => _ = GetResult(token);
        public ValueTaskSourceStatus GetStatus(short token) => _source.GetStatus(token);
        public void OnCompleted(Action<object?> continuation, object? state, short token, ValueTaskSourceOnCompletedFlags flags)
            => _source.OnCompleted(continuation, state, token, flags);
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
    private static extern bool IsWindow(nint hwnd);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint hwnd);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool PeekMessageW(out NativeMessage message, nint hwnd, uint min, uint max, uint remove);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool TranslateMessage(in NativeMessage message);
    [DllImport("user32.dll")]
    private static extern nint DispatchMessageW(in NativeMessage message);
}
