using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Trait("Category", "WindowsHandle")]
public class WindowsFormsSynchronizationContextTests
{
    [Fact]
    public void Constructor_PreservesCurrentAndRegistersItsNativeWindowForDpi()
        => OnStaThread(() =>
        {
            var sentinel = new SynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(sentinel);
            using var context = new WindowsFormsSynchronizationContext();

            Assert.IsAssignableFrom<SynchronizationContext>(context);
            Assert.IsAssignableFrom<IDisposable>(context);
            Assert.Same(sentinel, SynchronizationContext.Current);
            Assert.False(context.IsWaitNotificationRequired());
            Assert.False(Application.SetHighDpiMode(HighDpiMode.PerMonitorV2));
        });

    [Fact]
    public void Post_WithoutForm_DefersAndFlowsWorkerStateAndExecutionContext()
        => OnStaThread(() =>
        {
            using var context = new WindowsFormsSynchronizationContext();
            int owner = Environment.CurrentManagedThreadId;
            var ambient = new AsyncLocal<string?> { Value = "owner" };
            object state = new();
            bool called = false;
            context.Post(_ => called = true, null);
            Assert.False(called);
            PumpUntil(() => called);
            called = false;
            Exception? callbackFailure = null;
            var worker = new Thread(() =>
            {
                ambient.Value = "worker";
                context.Post(value =>
                {
                    try
                    {
                        Assert.Equal(owner, Environment.CurrentManagedThreadId);
                        Assert.Same(state, value);
                        Assert.Equal("worker", ambient.Value);
                    }
                    catch (Exception exception) { callbackFailure = exception; }
                    finally { called = true; }
                }, state);
            }) { IsBackground = true };
            worker.Start();
            PumpUntil(() => called && !worker.IsAlive);
            worker.Join();
            Assert.Null(callbackFailure);
            Assert.Equal("owner", ambient.Value);
        });

    [Fact]
    public void Send_DrainsOlderPostsAndPreservesTheCallbackException()
        => OnStaThread(() =>
        {
            using var context = new WindowsFormsSynchronizationContext();
            List<int> order = [];
            context.Post(_ => order.Add(1), null);
            context.Send(_ => order.Add(2), null);
            Assert.Equal([1, 2], order);
            var expected = new InvalidOperationException("original failure");
            Assert.Same(expected, Assert.Throws<InvalidOperationException>(() => context.Send(_ => throw expected, null)));

            int owner = Environment.CurrentManagedThreadId;
            Exception? actual = null;
            bool onOwner = false;
            var worker = new Thread(() =>
            {
                try { context.Send(_ => { onOwner = Environment.CurrentManagedThreadId == owner; throw expected; }, null); }
                catch (Exception exception) { actual = exception; }
            }) { IsBackground = true };
            worker.Start();
            PumpUntil(() => !worker.IsAlive);
            worker.Join();
            Assert.True(onOwner);
            Assert.Same(expected, actual);
        });

    [Fact]
    public void Copy_DisposalSharesDestinationAndDiscardsAlreadyQueuedCallbacks()
        => OnStaThread(() =>
        {
            using var context = new WindowsFormsSynchronizationContext();
            using var another = new WindowsFormsSynchronizationContext();
            var copy = Assert.IsType<WindowsFormsSynchronizationContext>(context.CreateCopy());
            Assert.NotSame(context, copy);
            bool called = false;
            context.Post(_ => called = true, null);
            copy.Dispose();
            Pump();
            Assert.False(called);
            Assert.Throws<InvalidOperationException>(() => context.Post(_ => called = true, null));
            Assert.Throws<InvalidOperationException>(() => another.Send(_ => called = true, null));
            using var later = new WindowsFormsSynchronizationContext();
            Assert.Throws<InvalidOperationException>(() => later.Post(_ => called = true, null));

            copy.Post(_ => called = true, null);
            copy.Send(_ => called = true, null);
            using var disposedCopy = Assert.IsType<WindowsFormsSynchronizationContext>(copy.CreateCopy());
            disposedCopy.Send(_ => called = true, null);
            Assert.False(called);
            copy.Dispose();
        });

    [Fact]
    public void ExplicitInstallation_ResumesAwaitOnTheCreatingThread()
        => OnStaThread(() =>
        {
            using var context = new WindowsFormsSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(context);
            int owner = Environment.CurrentManagedThreadId;
            Task operation = ContinueAsync();
            PumpUntil(() => operation.IsCompleted);
            operation.GetAwaiter().GetResult();

            async Task ContinueAsync()
            {
                await Task.Run(() => Thread.Sleep(15));
                Assert.Equal(owner, Environment.CurrentManagedThreadId);
                Assert.Same(context, SynchronizationContext.Current);
            }
        });

    [Fact]
    public void Destination_RemainsAvailableAfterAnUnrelatedFormIsDisposed()
        => OnStaThread(() =>
        {
            using var context = new WindowsFormsSynchronizationContext();
            using (var form = new Form { Width = 200, Height = 120 }) { form.Show(); }
            bool called = false;
            context.Post(_ => called = true, null);
            PumpUntil(() => called);
            Assert.True(called);
        });

    [Fact]
    public void NullCallbacks_PostIsAcceptedAndSendHasTheExplicitHangGuard()
        => OnStaThread(() =>
        {
            using var context = new WindowsFormsSynchronizationContext();
            context.Post(null!, null);
            Pump();
            Assert.Equal("d", Assert.Throws<ArgumentNullException>(() => context.Send(null!, null)).ParamName);
            context.Dispose();
            context.Post(null!, null);
            context.Send(null!, null);
        });

    private static void OnStaThread(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            try { action(); }
            catch (Exception exception) { failure = exception; }
            finally { SynchronizationContext.SetSynchronizationContext(null); }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(10)), "The STA context check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private static void PumpUntil(Func<bool> condition)
    {
        var timeout = Stopwatch.StartNew();
        while (!condition())
        {
            Assert.True(timeout.Elapsed < TimeSpan.FromSeconds(5), "The native context callback timed out.");
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
