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
    private static Exception? s_lastThreadException;
    private static bool s_exceptionOnUi;

    [STAThread]
    private static void Main(string[] args)
    {
        s_uiThread = Environment.CurrentManagedThreadId;
        Application.ThreadException += (_, e) =>
        {
            s_threadExceptions++;
            s_lastThreadException = e.Exception;
            s_exceptionOnUi = Environment.CurrentManagedThreadId == s_uiThread;
        };
#if REFERENCE
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        string? version = typeof(Control).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        if (version is null || !version.StartsWith("10.0.11+", StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"Expected WinForms 10.0.11, found {version}.");
        }
#endif
        if (args.Length != 0)
        {
            RunIsolated(args[0]);
            return;
        }

        var sentinel = new SynchronizationContext();
        SynchronizationContext.SetSynchronizationContext(sentinel);
        var context = new WindowsFormsSynchronizationContext();
        var sameThreadContext = new WindowsFormsSynchronizationContext();
        Record("base-type", typeof(WindowsFormsSynchronizationContext).BaseType == typeof(SynchronizationContext));
        Record("disposable", context is IDisposable);
        Record("constructor-preserves-current", ReferenceEquals(SynchronizationContext.Current, sentinel));
#if REFERENCE
        Require(Application.OpenForms.Count == 0, "The constructor check must run without an existing Form.");
#endif
        Record("wait-notification", context.IsWaitNotificationRequired());

        object state = new();
        bool posted = false;
        context.Post(value =>
        {
            Record("post-ui-thread", OnUi());
            Record("post-state-identity", ReferenceEquals(value, state));
            Record("post-current-preserved", ReferenceEquals(SynchronizationContext.Current, sentinel));
            posted = true;
        }, state);
        Record("post-deferred", !posted);
        Until(() => posted);
        bool nullState = false;
        context.Post(value => nullState = value is null, null);
        Until(() => nullState);
        Record("post-null-state", nullState);

        List<int> order = [];
        context.Post(_ => order.Add(1), null);
        context.Send(value =>
        {
            order.Add(2);
            Record("send-ui-thread", OnUi());
            Record("send-state-identity", ReferenceEquals(value, state));
        }, state);
        Record("send-drains-older-post", string.Join(",", order));
        CallerContext.Value = "ui-context";
        context.Send(_ => Record("send-ui-execution-context", CallerContext.Value), null);
        var directError = new DispatchException();
        Record("send-ui-error", Failure(() => context.Send(_ => throw directError, null), directError));
        var innerError = new DispatchException();
        Record("send-ui-inner-error", Failure(() => context.Send(_ => throw new InvalidOperationException("outer", innerError), null), innerError));

        RunWorker(() =>
        {
            CallerContext.Value = "worker-context";
            context.Send(value =>
            {
                Record("send-worker-ui-thread", OnUi());
                Record("send-worker-state-identity", ReferenceEquals(value, state));
                Record("send-worker-execution-context", CallerContext.Value);
                Record("send-worker-current-preserved", ReferenceEquals(SynchronizationContext.Current, sentinel));
            }, state);
            var workerError = new DispatchException();
            Record("send-worker-error", Failure(() => context.Send(_ => throw workerError, null), workerError));
            using var finished = new ManualResetEventSlim();
            context.Post(value =>
            {
                Record("post-worker-ui-thread", OnUi());
                Record("post-worker-state-identity", ReferenceEquals(value, state));
                Record("post-worker-execution-context", CallerContext.Value);
                finished.Set();
            }, state);
            Require(finished.Wait(TimeSpan.FromSeconds(5)), "Worker Post did not finish.");
        });
        Record("ui-execution-context-restored", CallerContext.Value);

        var postError = new DispatchException();
        context.Post(_ => throw postError, null);
        Until(() => s_threadExceptions != 0);
        Record("post-error-event-count", s_threadExceptions);
        Record("post-error-identity", ReferenceEquals(s_lastThreadException, postError));
        Record("post-error-ui-thread", s_exceptionOnUi);

        SynchronizationContext copy = context.CreateCopy();
        Record("copy-new-instance", !ReferenceEquals(copy, context));
        Record("copy-runtime-type", copy.GetType() == typeof(WindowsFormsSynchronizationContext));
        Record("copy-preserves-current", ReferenceEquals(SynchronizationContext.Current, sentinel));
        SynchronizationContext? workerCreatedCopy = null;
        RunWorker(() =>
        {
            workerCreatedCopy = context.CreateCopy();
            workerCreatedCopy.Send(_ => Record("worker-copy-original-destination", OnUi()), null);
        });

        using (var form = new Form { Text = "Lumina context lifetime probe", Width = 200, Height = 120 })
        {
            form.Show();
            Pump();
        }
        bool afterForm = false;
        context.Post(_ => { Record("post-after-form-dispose-ui", OnUi()); afterForm = true; }, null);
        Until(() => afterForm);

        SynchronizationContext.SetSynchronizationContext(context);
        Task recovery = CheckExplicitAwaitAsync(context);
        Until(() => recovery.IsCompleted);
        recovery.GetAwaiter().GetResult();
        SynchronizationContext.SetSynchronizationContext(sentinel);

        bool acceptedBeforeDispose = false;
        context.Post(_ => acceptedBeforeDispose = true, null);
        context.Dispose();
        Record("dispose-preserves-current", ReferenceEquals(SynchronizationContext.Current, sentinel));
        bool postAfterDispose = false;
        Record("disposed-post-call", Failure(() => context.Post(_ => postAfterDispose = true, null)));
        bool sendAfterDispose = false;
        Record("disposed-send-call", Failure(() => context.Send(_ => sendAfterDispose = true, null)));
        Pump();
        Record("post-before-dispose-runs", acceptedBeforeDispose);
        Record("post-after-dispose-runs", postAfterDispose);
        Record("send-after-dispose-runs", sendAfterDispose);
        RunWorker(() => Record("disposed-worker-send", Failure(() => context.Send(_ => { }, null))));
        bool survivingCopy = false;
        Record("copy-after-original-dispose-post", Failure(() => copy.Post(_ => { Record("copy-after-original-dispose-ui", OnUi()); survivingCopy = true; }, null)));
        Pump();
        Record("copy-after-original-dispose-runs", survivingCopy);
        Record("same-thread-context-after-dispose-send", Failure(() => sameThreadContext.Send(_ => { }, null)));
        sameThreadContext.Dispose();
        SynchronizationContext disposedCopy = context.CreateCopy();
        bool disposedCopyCalled = false;
        Record("disposed-create-copy-type", disposedCopy.GetType() == typeof(WindowsFormsSynchronizationContext));
        Record("disposed-copy-send-call", Failure(() => disposedCopy.Send(_ => disposedCopyCalled = true, null)));
        Record("disposed-copy-send-runs", disposedCopyCalled);
        ((IDisposable)disposedCopy).Dispose();
        ((IDisposable)workerCreatedCopy!).Dispose();
        ((IDisposable)copy).Dispose();
        Record("dispose-repeat", Failure(context.Dispose));

        using var another = new WindowsFormsSynchronizationContext();
        bool freshConstructorCalled = false;
        Record("fresh-constructor-post", Failure(() => another.Post(_ => freshConstructorCalled = true, null)));
        Pump();
        Record("fresh-constructor-post-runs", freshConstructorCalled);
        SynchronizationContext anotherCopy = another.CreateCopy();
        ((IDisposable)anotherCopy).Dispose();
        bool originalAfterCopy = false;
        Record("original-after-copy-dispose-post", Failure(() => another.Post(_ => originalAfterCopy = true, null)));
        Pump();
        Record("original-survives-copy-dispose", originalAfterCopy);
        Record("thread-exceptions-final", s_threadExceptions);
        SynchronizationContext.SetSynchronizationContext(null);
        Console.WriteLine(string.Join(Environment.NewLine, Results));
    }

    private static async Task CheckExplicitAwaitAsync(WindowsFormsSynchronizationContext context)
    {
        Record("explicit-await-start-ui", OnUi());
        await Task.Run(() => Thread.Sleep(20));
        Record("explicit-await-resumes-ui", OnUi());
        Record("explicit-await-current-identity", ReferenceEquals(SynchronizationContext.Current, context));
        bool continued = false;
        context.Post(async _ =>
        {
            Record("explicit-post-await-start-ui", OnUi());
            await Task.Run(() => Thread.Sleep(20));
            Record("explicit-post-await-resumes-ui", OnUi());
            continued = true;
        }, null);
        while (!continued) { await Task.Delay(1); }
    }

    private static void RunIsolated(string scenario)
    {
        using var context = new WindowsFormsSynchronizationContext();
        Console.WriteLine("isolated-start=" + scenario);
        try
        {
            if (scenario == "null-send") { context.Send(null!, null); }
            else if (scenario == "null-post") { context.Post(null!, null); Pump(); }
            else { throw new ArgumentException("Unknown isolated scenario.", nameof(scenario)); }
            Console.WriteLine("isolated-result=success");
        }
        catch (Exception exception) { Console.WriteLine("isolated-result=" + exception.GetType().Name + ":" + (exception as ArgumentException)?.ParamName); }
        Console.WriteLine("isolated-thread-exceptions=" + s_threadExceptions);
    }

    private static string Failure(Action action, Exception? expected = null)
    {
        try { action(); return "success"; }
        catch (Exception exception)
        {
            return exception.GetType().Name + ":" + (exception as ArgumentException)?.ParamName
                + (expected is null ? string.Empty : ":identity=" + ReferenceEquals(exception, expected));
        }
    }

    private static void RunWorker(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() => { try { action(); } catch (Exception exception) { failure = exception; } });
        thread.IsBackground = true;
        thread.Start();
        Until(() => !thread.IsAlive);
        if (failure is not null) { throw new InvalidOperationException("Worker probe failed.", failure); }
    }

    private static bool OnUi() => Environment.CurrentManagedThreadId == s_uiThread;
    private static void Record(string name, object? value) => Results.Add(name + "=" + (value ?? "null"));
    private static void Require(bool condition, string message) { if (!condition) { throw new InvalidOperationException(message); } }
    private static void Until(Func<bool> predicate)
    {
        var timeout = Stopwatch.StartNew();
        while (!predicate())
        {
            Require(timeout.Elapsed < TimeSpan.FromSeconds(7), "Synchronization context probe timed out.");
            Pump();
            Thread.Sleep(1);
        }
    }

    private static void Pump()
    {
        while (PeekMessageW(out Message message, 0, 0, 0, 1))
        {
            _ = TranslateMessage(ref message);
            _ = DispatchMessageW(ref message);
        }
    }

    private sealed class DispatchException : Exception;
    [StructLayout(LayoutKind.Sequential)]
    private struct Message
    {
        internal nint Hwnd;
        internal uint Value;
        internal nuint WParam;
        internal nint LParam;
        internal uint Time;
        internal int X;
        internal int Y;
        internal uint Private;
    }
    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool PeekMessageW(out Message message, nint window, uint minimum, uint maximum, uint remove);
    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool TranslateMessage(ref Message message);
    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint DispatchMessageW(ref Message message);
}
