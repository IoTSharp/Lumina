using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Trait("Category", "WindowsHandle")]
public class WindowsFormsSynchronizationContextInstallTests
{
    [Fact]
    public void AutoInstall_DefaultsToTrueAndBelongsToTheCurrentThread()
        => OnStaThread(() =>
        {
            Assert.True(WindowsFormsSynchronizationContext.AutoInstall);
            WindowsFormsSynchronizationContext.AutoInstall = false;
            OnStaThread(() => Assert.True(WindowsFormsSynchronizationContext.AutoInstall));
            Assert.False(WindowsFormsSynchronizationContext.AutoInstall);
        });

    [Theory]
    [InlineData(false, false)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(true, true)]
    public void Construction_InstallsForNullOrBaseContextAndRestoresItsPredecessor(bool form, bool baseContext)
        => OnStaThread(() =>
        {
            SynchronizationContext? previous = baseContext ? new SynchronizationContext() : null;
            SynchronizationContext.SetSynchronizationContext(previous);
            using IDisposable first = form ? new Form() : new Control();
            var installed = Assert.IsType<WindowsFormsSynchronizationContext>(SynchronizationContext.Current);
            using var second = new Control();
            Assert.Same(installed, SynchronizationContext.Current);

            WindowsFormsSynchronizationContext.Uninstall();
            Assert.False(WindowsFormsSynchronizationContext.AutoInstall);
            Assert.IsType<SynchronizationContext>(SynchronizationContext.Current);
            if (previous is not null) { Assert.Same(previous, SynchronizationContext.Current); }
            bool sent = false;
            installed.Send(_ => sent = true, null);
            Assert.True(sent);
        });

    [Fact]
    public void Construction_PreservesCustomContextAndDisabledInstallation()
        => OnStaThread(() =>
        {
            var custom = new CustomContext();
            SynchronizationContext.SetSynchronizationContext(custom);
            using var first = new Control();
            using var form = new Form();
            Assert.Same(custom, SynchronizationContext.Current);

            WindowsFormsSynchronizationContext.AutoInstall = false;
            SynchronizationContext.SetSynchronizationContext(null);
            using var disabledControl = new Control();
            using var disabledForm = new Form();
            Assert.Null(SynchronizationContext.Current);
        });

    [Fact]
    public void Uninstall_FreshThreadCreatesBaseContextAndRepeatedCallsPreserveIt()
        => OnStaThread(() =>
        {
            Assert.Null(SynchronizationContext.Current);
            WindowsFormsSynchronizationContext.Uninstall();
            var current = Assert.IsType<SynchronizationContext>(SynchronizationContext.Current);
            Assert.False(WindowsFormsSynchronizationContext.AutoInstall);
            WindowsFormsSynchronizationContext.Uninstall();
            Assert.Same(current, SynchronizationContext.Current);
        });

    [Fact]
    public void Uninstall_WhenAutoInstallIsDisabledPreservesAnInstalledContext()
        => OnStaThread(() =>
        {
            using var control = new Control();
            SynchronizationContext? installed = SynchronizationContext.Current;
            WindowsFormsSynchronizationContext.AutoInstall = false;
            WindowsFormsSynchronizationContext.Uninstall();
            Assert.Same(installed, SynchronizationContext.Current);
            WindowsFormsSynchronizationContext.AutoInstall = true;
            WindowsFormsSynchronizationContext.Uninstall();
            Assert.IsType<SynchronizationContext>(SynchronizationContext.Current);
            Assert.False(WindowsFormsSynchronizationContext.AutoInstall);
        });

    [Fact]
    public void Construction_RespectsSavedBaseReplacementAndStartsAgainWhenCurrentIsNull()
        => OnStaThread(() =>
        {
            var original = new SynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(original);
            using var first = new Control();
            var replacement = new SynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(replacement);
            using var second = new Control();
            Assert.Same(replacement, SynchronizationContext.Current);

            SynchronizationContext.SetSynchronizationContext(null);
            using var third = new Control();
            Assert.IsType<WindowsFormsSynchronizationContext>(SynchronizationContext.Current);
            WindowsFormsSynchronizationContext.Uninstall();
            Assert.IsType<SynchronizationContext>(SynchronizationContext.Current);
            Assert.NotSame(original, SynchronizationContext.Current);
        });

    [Fact]
    public void Uninstall_ReenablingCreatesANewContextWithoutDisposingTheOldDestination()
        => OnStaThread(() =>
        {
            using var first = new Control();
            var installed = Assert.IsType<WindowsFormsSynchronizationContext>(SynchronizationContext.Current);
            WindowsFormsSynchronizationContext.Uninstall();
            using var disabled = new Control();
            Assert.IsType<SynchronizationContext>(SynchronizationContext.Current);
            WindowsFormsSynchronizationContext.AutoInstall = true;
            using var enabled = new Control();
            Assert.NotSame(installed, Assert.IsType<WindowsFormsSynchronizationContext>(SynchronizationContext.Current));
            bool posted = false;
            installed.Post(_ => posted = true, null);
            PumpUntil(() => posted);
        });

    [Fact]
    public void DerivedConstructorFailure_LeavesInstalledContextUsableAndCanReinstall()
        => OnStaThread(() =>
        {
            Assert.Throws<ConstructionException>(() => new ThrowingControl());
            var installed = Assert.IsType<WindowsFormsSynchronizationContext>(SynchronizationContext.Current);
            bool sent = false;
            installed.Send(_ => sent = true, null);
            Assert.True(sent);
            WindowsFormsSynchronizationContext.Uninstall();
            WindowsFormsSynchronizationContext.AutoInstall = true;
            using var next = new Control();
            Assert.IsType<WindowsFormsSynchronizationContext>(SynchronizationContext.Current);
        });

    [Fact]
    public void DefaultInstallation_ResumesDirectAndWorkerValueTaskAwaitsOnTheUiThread()
        => OnStaThread(() =>
        {
            using var form = new Form { Width = 220, Height = 120 };
            var installed = Assert.IsType<WindowsFormsSynchronizationContext>(SynchronizationContext.Current);
            int owner = Environment.CurrentManagedThreadId;
            form.Show();
            Task direct = ContinueAsync();
            PumpUntil(() => direct.IsCompleted);
            direct.GetAwaiter().GetResult();

            Exception? failure = null;
            var worker = new Thread(() =>
            {
                try
                {
                    Assert.Null(SynchronizationContext.Current);
                    form.InvokeAsync(async _ =>
                    {
                        AssertUi();
                        await Task.Delay(15);
                        AssertUi();
                    }, CancellationToken.None).GetAwaiter().GetResult();
                }
                catch (Exception exception) { failure = exception; }
            }) { IsBackground = true };
            worker.Start();
            PumpUntil(() => !worker.IsAlive);
            if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }

            async Task ContinueAsync() { await Task.Delay(15); AssertUi(); }
            void AssertUi()
            {
                Assert.Equal(owner, Environment.CurrentManagedThreadId);
                Assert.Same(installed, SynchronizationContext.Current);
            }
        });

    [Fact]
    public void LegacyTaskCallback_FromWorkerAlsoResumesOnTheUiThread()
        => OnStaThread(() =>
        {
            using var form = new Form { Width = 220, Height = 120 };
            var control = new Button();
            form.Controls.Add(control);
            form.Show();
            var installed = Assert.IsType<WindowsFormsSynchronizationContext>(SynchronizationContext.Current);
            int owner = Environment.CurrentManagedThreadId;
            Func<CancellationToken, Task> callback = async _ =>
            {
                Assert.Same(installed, SynchronizationContext.Current);
                await Task.Delay(15);
                Assert.Equal(owner, Environment.CurrentManagedThreadId);
                Assert.Same(installed, SynchronizationContext.Current);
            };
            Exception? failure = null;
            var worker = new Thread(() =>
            {
                try { control.InvokeAsync(callback, CancellationToken.None).GetAwaiter().GetResult(); }
                catch (Exception exception) { failure = exception; }
            }) { IsBackground = true };
            worker.Start();
            PumpUntil(() => !worker.IsAlive);
            if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
        });

    [Fact]
    public void CallbackFailure_RestoresTheDestinationContextBeforeReportingTheError()
        => OnStaThread(() =>
        {
            using var form = new Form { Width = 220, Height = 120 };
            form.Show();
            SynchronizationContext? installed = SynchronizationContext.Current;
            var expected = new InvalidOperationException("callback failed");
            Exception? actual = null;
            SynchronizationContext? reportingContext = null;
            System.Threading.ThreadExceptionEventHandler handler = (_, args) =>
            {
                actual = args.Exception;
                reportingContext = SynchronizationContext.Current;
            };
            Application.ThreadException += handler;
            try
            {
                form.BeginInvoke((Action)(() =>
                {
                    SynchronizationContext.SetSynchronizationContext(new CustomContext());
                    throw expected;
                }));
                PumpUntil(() => actual is not null);
                Assert.Same(expected, actual);
                Assert.Same(installed, reportingContext);
                Assert.Same(installed, SynchronizationContext.Current);
            }
            finally { Application.ThreadException -= handler; }
        });

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Run_InstallsBeforeShownAndRestoresContextWithoutChangingAutoInstall(bool enabled)
        => OnStaThread(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            using var form = new Form { Width = 220, Height = 120 };
            var original = new SynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(original);
            WindowsFormsSynchronizationContext.AutoInstall = enabled;
            SynchronizationContext? shownContext = null;
            SynchronizationContext? callbackContext = null;
            Exception? callbackFailure = null;
            System.Threading.ThreadExceptionEventHandler handler = (_, args) =>
            {
                callbackFailure = args.Exception;
                PostQuitMessage(0);
            };
            form.Shown += (_, _) =>
            {
                shownContext = SynchronizationContext.Current;
                form.BeginInvoke((Action)(() =>
                {
                    callbackContext = SynchronizationContext.Current;
                    form.Close();
                    // This check owns this loop. Forms on parallel test STAs must
                    // not decide whether its installation/restore check can exit.
                    Application.ExitThread();
                }));
            };
            Application.ThreadException += handler;
            try
            {
                // OnStaThread owns the STA; this check deliberately tests Run's initialization.
#pragma warning disable LNF001, LNF002
                Application.Run(form);
#pragma warning restore LNF001, LNF002
            }
            finally { Application.ThreadException -= handler; }
            if (callbackFailure is not null) { ExceptionDispatchInfo.Capture(callbackFailure).Throw(); }
            Assert.Equal(enabled, WindowsFormsSynchronizationContext.AutoInstall);
            Assert.Same(original, SynchronizationContext.Current);
            if (enabled) { Assert.IsType<WindowsFormsSynchronizationContext>(shownContext); }
            else { Assert.Same(original, shownContext); }
            Assert.Same(shownContext, callbackContext);
        });

    [Fact]
    public void RunMessageLoop_InstallsAndRestoresWithoutRequiringAForm()
        => OnStaThread(() =>
        {
            var original = new SynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(original);
            using var destination = new WindowsFormsSynchronizationContext();
            SynchronizationContext? callbackContext = null;
            Exception? callbackFailure = null;
            System.Threading.ThreadExceptionEventHandler handler = (_, args) =>
            {
                callbackFailure = args.Exception;
                // Expose an exit callback failure as its original test exception,
                // instead of opening the default UI exception prompt indefinitely.
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
                Assert.Equal(0, Application.RunMessageLoop());
            }
            finally { Application.ThreadException -= handler; }
            if (callbackFailure is not null) { ExceptionDispatchInfo.Capture(callbackFailure).Throw(); }
            Assert.IsType<WindowsFormsSynchronizationContext>(callbackContext);
            Assert.Same(original, SynchronizationContext.Current);
            Assert.True(WindowsFormsSynchronizationContext.AutoInstall);
        });

    [Fact]
    public void Run_ShowFailureRestoresContextAndAllowsLaterConstruction()
        => OnStaThread(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            var disposed = new Form();
            disposed.Dispose();
            var original = new SynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(original);
            WindowsFormsSynchronizationContext.AutoInstall = true;
            // OnStaThread owns the STA; Run's own failure/initialization cleanup is under test.
#pragma warning disable LNF001, LNF002
            Assert.Throws<ObjectDisposedException>(() => Application.Run(disposed));
#pragma warning restore LNF001, LNF002
            Assert.Same(original, SynchronizationContext.Current);
            Assert.True(WindowsFormsSynchronizationContext.AutoInstall);
            using var next = new Control();
            Assert.IsType<WindowsFormsSynchronizationContext>(SynchronizationContext.Current);
        });

    private static void OnStaThread(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            try { action(); }
            catch (Exception exception) { failure = exception; }
            finally
            {
                // Each check owns its STA and its shared message destination.
                // Dispose only after the check; Uninstall itself must not do so.
                if (SynchronizationContext.Current is WindowsFormsSynchronizationContext context) { context.Dispose(); }
                SynchronizationContext.SetSynchronizationContext(null);
            }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "The STA installation check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private static void PumpUntil(Func<bool> condition)
    {
        var timeout = Stopwatch.StartNew();
        while (!condition())
        {
            Assert.True(timeout.Elapsed < TimeSpan.FromSeconds(6), "The UI callback timed out.");
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
