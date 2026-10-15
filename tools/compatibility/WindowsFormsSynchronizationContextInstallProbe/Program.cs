using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
#if REFERENCE
using System.Windows.Forms;
#else
using Lumina.Forms;
#endif

internal static class Program
{
    private static readonly List<string> Results = [];

    [STAThread]
    private static void Main()
    {
#if REFERENCE
        string? version = typeof(Control).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        Require(version?.StartsWith("10.0.11+", StringComparison.Ordinal) == true, "Expected WinForms 10.0.11, found " + version);
#endif
        Sta("initial", () =>
        {
            Record("auto", WindowsFormsSynchronizationContext.AutoInstall);
            Record("current", Kind(SynchronizationContext.Current));
            WindowsFormsSynchronizationContext.Uninstall();
            Record("uninstall-auto", WindowsFormsSynchronizationContext.AutoInstall);
            Record("uninstall-current", Kind(SynchronizationContext.Current));
            WindowsFormsSynchronizationContext.Uninstall();
            Record("uninstall-repeat", Kind(SynchronizationContext.Current));
        });

        foreach (string currentKind in new[] { "null", "base", "custom", "forms" })
        {
            foreach (bool enabled in new[] { true, false })
            {
                Sta("control-" + currentKind + "-" + enabled, () => Construction(currentKind, enabled, false));
                Sta("form-" + currentKind + "-" + enabled, () => Construction(currentKind, enabled, true));
            }
        }

        Sta("thread-local", () =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            bool otherDefault = false;
            var worker = new Thread(() => otherDefault = WindowsFormsSynchronizationContext.AutoInstall);
            worker.Start();
            Require(worker.Join(TimeSpan.FromSeconds(5)), "Flag worker timed out.");
            Record("other-default", otherDefault);
            Record("owner-disabled", !WindowsFormsSynchronizationContext.AutoInstall);
        });

        Sta("disabled-uninstall", () =>
        {
            using var control = new Control();
            SynchronizationContext? installed = SynchronizationContext.Current;
            WindowsFormsSynchronizationContext.AutoInstall = false;
            WindowsFormsSynchronizationContext.Uninstall();
            Record("preserves-installed", ReferenceEquals(installed, SynchronizationContext.Current));
            WindowsFormsSynchronizationContext.AutoInstall = true;
            WindowsFormsSynchronizationContext.Uninstall();
            Record("enabled-uninstall-current", Kind(SynchronizationContext.Current));
            Record("enabled-uninstall-auto", WindowsFormsSynchronizationContext.AutoInstall);
        });

        Sta("cached-previous", () =>
        {
            var original = new SynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(original);
            using var first = new Control();
            var replacement = new SynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(replacement);
            using var second = new Control();
            Record("base-replacement-preserved", ReferenceEquals(replacement, SynchronizationContext.Current));
            SynchronizationContext.SetSynchronizationContext(null);
            using var third = new Control();
            Record("null-replacement-installed", Kind(SynchronizationContext.Current));
            WindowsFormsSynchronizationContext.Uninstall();
            Record("original-restored-after-null", ReferenceEquals(original, SynchronizationContext.Current));
            Record("after-null-uninstall-current", Kind(SynchronizationContext.Current));
        });

        Sta("reinstall", () =>
        {
            var original = new SynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(original);
            using var first = new Control();
            SynchronizationContext? installed = SynchronizationContext.Current;
            WindowsFormsSynchronizationContext.Uninstall();
            Record("restores-original", ReferenceEquals(original, SynchronizationContext.Current));
            using var disabled = new Control();
            Record("disabled-preserves-original", ReferenceEquals(original, SynchronizationContext.Current));
            WindowsFormsSynchronizationContext.AutoInstall = true;
            using var enabled = new Control();
            Record("reenabled-installs", Kind(SynchronizationContext.Current));
            Record("reenabled-new-instance", !ReferenceEquals(installed, SynchronizationContext.Current));
            bool sent = false;
            installed!.Send(_ => sent = true, null);
            Record("uninstall-does-not-dispose", sent);
        });

        Sta("failed-construction", () =>
        {
            Record("failure", Failure(() => _ = new ThrowingControl()));
            Record("current", Kind(SynchronizationContext.Current));
            WindowsFormsSynchronizationContext.Uninstall();
            WindowsFormsSynchronizationContext.AutoInstall = true;
            using var next = new Control();
            Record("next-installs", Kind(SynchronizationContext.Current));
        });

        Sta("default-await", DefaultAwait);
        foreach (string currentKind in new[] { "null", "base", "custom", "forms" })
        {
            foreach (bool enabled in new[] { true, false })
            {
                Sta("loop-" + currentKind + "-" + enabled, () => Loop(currentKind, enabled));
            }
        }
        Sta("loop-without-form-first", LoopWithoutForm);
        Sta("loop-without-form-next-thread", LoopWithoutForm);
        Console.WriteLine(string.Join(Environment.NewLine, Results));
    }

    private static void Construction(string currentKind, bool enabled, bool form)
    {
        SynchronizationContext? initial = NewContext(currentKind);
        SynchronizationContext.SetSynchronizationContext(initial);
        WindowsFormsSynchronizationContext.AutoInstall = enabled;
        using IDisposable control = form ? new Form() : new Control();
        SynchronizationContext? installed = SynchronizationContext.Current;
        Record("after-construction", Kind(installed));
        Record("initial-preserved", ReferenceEquals(initial, installed));
        using var second = new Control();
        Record("repeat-preserved", ReferenceEquals(installed, SynchronizationContext.Current));
        WindowsFormsSynchronizationContext.Uninstall();
        Record("after-uninstall", Kind(SynchronizationContext.Current));
        Record("restored-initial", ReferenceEquals(initial, SynchronizationContext.Current));
        Record("after-uninstall-auto", WindowsFormsSynchronizationContext.AutoInstall);
        // An uninstalled context still owns a usable destination.
        if (installed is WindowsFormsSynchronizationContext context)
        {
            bool ran = false;
            context.Send(_ => ran = true, null);
            Record("saved-context-still-runs", ran);
        }
        (initial as IDisposable)?.Dispose();
    }

    private static void DefaultAwait()
    {
        using var form = new Form { Text = "Synchronization context default await", Width = 220, Height = 120 };
        int owner = Environment.CurrentManagedThreadId;
        var installed = SynchronizationContext.Current;
        Record("construction-context", Kind(installed));
        form.Show();
        Task direct = ContinueAsync("direct");
        Until(() => direct.IsCompleted);
        direct.GetAwaiter().GetResult();
        bool posted = false;
        _ = form.BeginInvoke((Action)(async () =>
        {
            Record("begin-start-ui", IsOwner());
            await Task.Delay(15);
            Record("begin-resume-ui", IsOwner());
            Record("begin-current-identity", ReferenceEquals(installed, SynchronizationContext.Current));
            posted = true;
        }));
        Until(() => posted);
        Task valueTask = form.InvokeAsync(async _ =>
        {
            Record("value-start-ui", IsOwner());
            await Task.Delay(15);
            Record("value-resume-ui", IsOwner());
            Record("value-current-identity", ReferenceEquals(installed, SynchronizationContext.Current));
        }, CancellationToken.None);
        Until(() => valueTask.IsCompleted);
        valueTask.GetAwaiter().GetResult();
        Task<int> generic = form.InvokeAsync(async _ =>
        {
            Record("generic-start-ui", IsOwner());
            await Task.Delay(15);
            Record("generic-resume-ui", IsOwner());
            return 37;
        }, CancellationToken.None);
        Until(() => generic.IsCompleted);
        Record("generic-result", generic.GetAwaiter().GetResult());

        // Enqueue from a thread whose ExecutionContext has no UI context. The UI
        // dispatch destination, rather than the caller's context, governs awaits.
        Exception? workerFailure = null;
        var worker = new Thread(() =>
        {
            try
            {
                form.InvokeAsync(async _ =>
                {
                    Record("worker-value-start-ui", IsOwner());
                    Record("worker-value-start-current", ReferenceEquals(installed, SynchronizationContext.Current));
                    await Task.Delay(15);
                    Record("worker-value-resume-ui", IsOwner());
                    Record("worker-value-resume-current", ReferenceEquals(installed, SynchronizationContext.Current));
                }, CancellationToken.None).GetAwaiter().GetResult();
                IAsyncResult callback = form.BeginInvoke((Action)(() =>
                {
                    Record("worker-begin-current", ReferenceEquals(installed, SynchronizationContext.Current));
                }));
                form.EndInvoke(callback);
            }
            catch (Exception exception) { workerFailure = exception; }
        }) { IsBackground = true };
        worker.Start();
        Until(() => !worker.IsAlive);
        if (workerFailure is not null) { ExceptionDispatchInfo.Capture(workerFailure).Throw(); }
        WindowsFormsSynchronizationContext.Uninstall();
        Record("after-uninstall", Kind(SynchronizationContext.Current));
        Record("after-uninstall-auto", WindowsFormsSynchronizationContext.AutoInstall);

        bool IsOwner() => owner == Environment.CurrentManagedThreadId;
        async Task ContinueAsync(string label)
        {
            Record(label + "-start-ui", IsOwner());
            await Task.Delay(15);
            Record(label + "-resume-ui", IsOwner());
            Record(label + "-current-identity", ReferenceEquals(installed, SynchronizationContext.Current));
        }
    }

    private static void Loop(string currentKind, bool enabled)
    {
        // Construct without installing, so Run itself is the trigger under test.
        WindowsFormsSynchronizationContext.AutoInstall = false;
        using var form = new Form { Text = "Synchronization context loop", Width = 220, Height = 120 };
        SynchronizationContext? initial = NewContext(currentKind);
        SynchronizationContext.SetSynchronizationContext(initial);
        WindowsFormsSynchronizationContext.AutoInstall = enabled;
        form.Shown += (_, _) =>
        {
            Record("shown-current", Kind(SynchronizationContext.Current));
            _ = form.BeginInvoke((Action)(() =>
            {
                Record("callback-current", Kind(SynchronizationContext.Current));
                Record("callback-initial-preserved", ReferenceEquals(initial, SynchronizationContext.Current));
                form.Close();
            }));
        };
        Application.Run(form);
        Record("after-current", Kind(SynchronizationContext.Current));
        Record("after-initial-preserved", ReferenceEquals(initial, SynchronizationContext.Current));
        Record("after-auto", WindowsFormsSynchronizationContext.AutoInstall);
        (initial as IDisposable)?.Dispose();
    }

    private static void LoopWithoutForm()
    {
        var original = new SynchronizationContext();
        SynchronizationContext.SetSynchronizationContext(original);
        using var destination = new WindowsFormsSynchronizationContext();
        SynchronizationContext? callbackContext = null;
        Exception? callbackFailure = null;
        ThreadExceptionEventHandler handler = (_, args) =>
        {
            callbackFailure = args.Exception;
            PostQuitMessage(0);
        };
        Application.ThreadException += handler;
        try
        {
            destination.Post(_ =>
            {
                callbackContext = SynchronizationContext.Current;
                Application.ExitThread();
            }, null);
#if REFERENCE
            Application.Run();
#else
            Record("loop-result", Application.RunMessageLoop());
#endif
        }
        finally { Application.ThreadException -= handler; }
        if (callbackFailure is not null) { ExceptionDispatchInfo.Capture(callbackFailure).Throw(); }
#if REFERENCE
        Record("loop-result", 0);
#endif
        Record("callback-current", Kind(callbackContext));
        Record("restored-original", ReferenceEquals(original, SynchronizationContext.Current));
        Record("after-current", Kind(SynchronizationContext.Current));
        Record("after-auto", WindowsFormsSynchronizationContext.AutoInstall);
    }

    private static SynchronizationContext? NewContext(string kind) => kind switch
    {
        "null" => null,
        "base" => new SynchronizationContext(),
        "custom" => new CustomContext(),
        "forms" => new WindowsFormsSynchronizationContext(),
        _ => throw new ArgumentOutOfRangeException(nameof(kind))
    };

    private static string s_scenario = string.Empty;
    private static void Sta(string scenario, Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            s_scenario = scenario;
            try { action(); }
            catch (Exception exception) { failure = exception; }
            finally { SynchronizationContext.SetSynchronizationContext(null); }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Require(thread.Join(TimeSpan.FromSeconds(12)), "STA scenario timed out: " + scenario);
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private static string Kind(SynchronizationContext? context)
        => context is null ? "null" : context.GetType() == typeof(SynchronizationContext) ? "base"
            : context is WindowsFormsSynchronizationContext ? "forms" : "custom";
    private static void Record(string name, object? value) => Results.Add(s_scenario + "/" + name + "=" + (value ?? "null"));
    private static string Failure(Action action)
    {
        try { action(); return "success"; }
        catch (Exception exception) { return exception.GetType().Name; }
    }
    private static void Require(bool condition, string message) { if (!condition) { throw new InvalidOperationException(message); } }
    private static void Until(Func<bool> condition)
    {
        var timeout = Stopwatch.StartNew();
        while (!condition())
        {
            Require(timeout.Elapsed < TimeSpan.FromSeconds(7), "Callback timed out: " + s_scenario);
            while (PeekMessageW(out Message message, 0, 0, 0, 1))
            {
                _ = TranslateMessage(ref message);
                _ = DispatchMessageW(ref message);
            }
            Thread.Sleep(1);
        }
    }

    private sealed class CustomContext : SynchronizationContext;
    private sealed class ConstructionException : Exception;
    private sealed class ThrowingControl : Control
    {
        internal ThrowingControl() => throw new ConstructionException();
    }
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
    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern void PostQuitMessage(int exitCode);
}
