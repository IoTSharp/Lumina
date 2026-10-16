using System.ComponentModel;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public class ControlDispatcherCloseTests
{
    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void DestructionCompletesEveryPendingCallBeforePropagatingSignalFailures(bool dispose)
        => OnSta(() =>
        {
            using var control = new ExposedControl();
            nint handle = control.Handle;
            int called = 0;
            IAsyncResult[] pending = Enumerable.Range(0, 4)
                .Select(_ => control.BeginInvoke((Action)(() => called++))).ToArray();
            pending[0].AsyncWaitHandle.Dispose();
            pending[2].AsyncWaitHandle.Dispose();
            using WaitHandle secondWait = pending[1].AsyncWaitHandle;
            using WaitHandle fourthWait = pending[3].AsyncWaitHandle;

            // Unlike the fixed reference, cleanup finishes the queue and HWND
            // before propagating the failures of caller-disposed wait handles.
            AggregateException failure = Assert.Throws<AggregateException>(() =>
            {
                if (dispose) { control.Dispose(); }
                else { control.DestroyNativeHandle(); }
            });

            Assert.Equal(2, failure.InnerExceptions.Count);
            Assert.All(failure.InnerExceptions, error => Assert.IsType<ObjectDisposedException>(error));
            Assert.False(IsWindow(handle));
            Assert.False(control.IsHandleCreated);
            Assert.Equal(dispose, control.IsDisposed);
            Assert.Equal(0, called);
            Assert.All(pending, result => Assert.True(result.IsCompleted));
            Assert.True(secondWait.WaitOne(0));
            Assert.True(fourthWait.WaitOne(0));
            ObjectDisposedException closed = Assert.Throws<ObjectDisposedException>(() => control.EndInvoke(pending[0]));
            Assert.Equal(nameof(ExposedControl), closed.ObjectName);
            Assert.All(pending, result => Assert.Same(closed,
                Assert.Throws<ObjectDisposedException>(() => control.EndInvoke(result))));
            Assert.Throws<ObjectDisposedException>(() => pending[0].AsyncWaitHandle.WaitOne(0));
            control.DestroyNativeHandle();
        });

    [Fact]
    public void SingleSignalFailureKeepsItsThrowSiteAndAllowsANewDispatcher()
        => OnSta(() =>
        {
            using var control = new ExposedControl();
            nint oldHandle = control.Handle;
            int called = 0;
            IAsyncResult first = control.BeginInvoke((Action)(() => called++));
            IAsyncResult second = control.BeginInvoke((Action)(() => called++));
            WaitHandle disposedWait = first.AsyncWaitHandle;
            disposedWait.Dispose();

            ObjectDisposedException signalFailure = Assert.Throws<ObjectDisposedException>(control.DestroyNativeHandle);

            Assert.Contains("Set", signalFailure.StackTrace);
            Assert.False(IsWindow(oldHandle));
            Assert.True(first.IsCompleted);
            Assert.True(second.IsCompleted);
            ObjectDisposedException closed = Assert.Throws<ObjectDisposedException>(() => control.EndInvoke(first));
            Assert.NotSame(signalFailure, closed);
            Assert.Same(closed, Assert.Throws<ObjectDisposedException>(() => control.EndInvoke(second)));
            Assert.Same(disposedWait, first.AsyncWaitHandle);
            Assert.Equal(0, called);

            _ = control.Handle;
            IAsyncResult next = control.BeginInvoke((Func<int>)(() => ++called));
            Assert.Equal(1, control.EndInvoke(next));
        });

    [Fact]
    public void CloseWakesAWaitingCallerAfterAnEarlierSignalFailure()
        => OnSta(() =>
        {
            using var control = new ExposedControl();
            _ = control.Handle;
            IAsyncResult first = control.BeginInvoke((Action)(() => throw new InvalidOperationException("must not run")));
            first.AsyncWaitHandle.Dispose();
            IAsyncResult second = control.BeginInvoke((Action)(() => throw new InvalidOperationException("must not run")));
            using WaitHandle secondWait = second.AsyncWaitHandle;
            using var started = new ManualResetEventSlim();
            Exception? observed = null;
            Thread waiter = new(() =>
            {
                started.Set();
                try { control.EndInvoke(second); }
                catch (Exception error) { observed = error; }
            }) { IsBackground = true };
            waiter.Start();
            Assert.True(started.Wait(TimeSpan.FromSeconds(3)));

            Assert.Throws<ObjectDisposedException>(control.DestroyNativeHandle);

            Assert.True(waiter.Join(TimeSpan.FromSeconds(3)), "The queued caller was not released by close.");
            Assert.IsType<ObjectDisposedException>(observed);
            Assert.Same(observed, Assert.Throws<ObjectDisposedException>(() => control.EndInvoke(second)));
            Assert.True(secondWait.WaitOne(0));
        });

    [Fact]
    public void NativeDestructionReportsEverySignalFailureAfterCompletingTheQueue()
        => OnSta(() =>
        {
            using var control = new ExposedControl();
            nint handle = control.Handle;
            int called = 0;
            IAsyncResult[] pending = Enumerable.Range(0, 3)
                .Select(_ => control.BeginInvoke((Action)(() => called++))).ToArray();
            pending[0].AsyncWaitHandle.Dispose();
            pending[1].AsyncWaitHandle.Dispose();
            using WaitHandle thirdWait = pending[2].AsyncWaitHandle;
            List<Exception> reported = [];
            ThreadExceptionEventHandler handler = (_, args) => reported.Add(args.Exception);
            Application.ThreadException += handler;
            try
            {
                Assert.True(DestroyWindow(handle));

                AggregateException failure = Assert.IsType<AggregateException>(Assert.Single(reported));
                Assert.Equal(2, failure.InnerExceptions.Count);
                Assert.All(failure.InnerExceptions, error => Assert.IsType<ObjectDisposedException>(error));
                Assert.All(pending, result => Assert.True(result.IsCompleted));
                Assert.All(pending, result => Assert.Throws<ObjectDisposedException>(() => control.EndInvoke(result)));
                Assert.True(thirdWait.WaitOne(0));
                Assert.False(IsWindow(handle));
                Assert.False(control.IsHandleCreated);
                Assert.Equal(0, called);
            }
            finally { Application.ThreadException -= handler; }
        });

    [Fact]
    public void RecreatedWindowRetainsPendingCallsUntilCloseAndCloseIsRepeatable()
        => OnSta(() =>
        {
            using var control = new ExposedControl();
            nint oldHandle = control.Handle;
            object dispatcher = Dispatcher(control);
            int called = 0;
            IAsyncResult first = control.BeginInvoke((Action)(() => called++));
            IAsyncResult second = control.BeginInvoke((Action)(() => called++));
            first.AsyncWaitHandle.Dispose();
            using WaitHandle secondWait = second.AsyncWaitHandle;

            control.RecreateNativeHandle();

            Assert.Same(dispatcher, Dispatcher(control));
            Assert.False(IsWindow(oldHandle));
            Assert.True(control.IsHandleCreated);
            Assert.False(first.IsCompleted);
            Assert.False(second.IsCompleted);
            Assert.Throws<ObjectDisposedException>(() => InvokeDispatcher(dispatcher, "Close"));
            Assert.True(first.IsCompleted);
            Assert.True(second.IsCompleted);
            Assert.True(secondWait.WaitOne(0));
            InvokeDispatcher(dispatcher, "Close");
            Assert.Throws<InvalidOperationException>(() => control.BeginInvoke((Action)(() => called++)));
            Assert.Equal(0, called);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void FailedPostRetainsTheNativeErrorAndCompletesEveryAcceptedCall(bool disposeWait)
        => OnSta(() =>
        {
            using var control = new ExposedControl();
            _ = control.Handle;
            object dispatcher = Dispatcher(control);
            int called = 0;
            IAsyncResult first = control.BeginInvoke((Action)(() => called++));
            IAsyncResult second = control.BeginInvoke((Action)(() => called++));
            WaitHandle firstWait = first.AsyncWaitHandle;
            WaitHandle secondWait = second.AsyncWaitHandle;
            if (disposeWait) { firstWait.Dispose(); }
            Exception? assertionFailure = null;
            try
            {
                // Use an actual destroyed HWND: sentinel HWND values can have
                // special PostMessage behavior. Do not create another window
                // between destroying this target and attempting the post.
                using var staleTarget = new ExposedControl();
                nint staleHandle = staleTarget.Handle;
                staleTarget.Dispose();
                Assert.False(IsWindow(staleHandle));
                dispatcher.GetType().GetField("_window", BindingFlags.Instance | BindingFlags.NonPublic)!
                    .SetValue(dispatcher, staleHandle);
                Exception failure = Assert.ThrowsAny<Exception>(() => control.BeginInvoke((Action)(() => called++)));
                Win32Exception postError;
                if (disposeWait)
                {
                    AggregateException aggregate = Assert.IsType<AggregateException>(failure);
                    Assert.Equal(2, aggregate.InnerExceptions.Count);
                    postError = Assert.IsType<Win32Exception>(aggregate.InnerExceptions[0]);
                    Assert.IsType<ObjectDisposedException>(aggregate.InnerExceptions[1]);
                }
                else
                {
                    postError = Assert.IsType<Win32Exception>(failure);
                    Assert.True(firstWait.WaitOne(0));
                }

                Assert.NotEqual(0, postError.NativeErrorCode);
                Assert.True(first.IsCompleted);
                Assert.True(second.IsCompleted);
                Assert.True(secondWait.WaitOne(0));
                Assert.Same(postError, Assert.Throws<Win32Exception>(() => control.EndInvoke(first)));
                Assert.Same(postError, Assert.Throws<Win32Exception>(() => control.EndInvoke(second)));
                Assert.Throws<InvalidOperationException>(() => control.BeginInvoke((Action)(() => called++)));
                InvokeDispatcher(dispatcher, "Close");
                Assert.Equal(0, called);
            }
            catch (Exception failure)
            {
                assertionFailure = failure;
                throw;
            }
            finally
            {
                try
                {
                    // Keep live wait handles available until queue cleanup.
                    // If setup/assertion and cleanup both fail, retain both.
                    try { InvokeDispatcher(dispatcher, "Close"); }
                    catch (Exception cleanupFailure) when (assertionFailure is not null)
                    {
                        throw new AggregateException(assertionFailure, cleanupFailure);
                    }
                }
                finally
                {
                    firstWait.Dispose();
                    secondWait.Dispose();
                }
            }
        });

    [Fact]
    public void FailedReattachCompletesTheRetainedQueueAndPreservesAllFailures()
        => OnSta(() =>
        {
            using var control = new ExposedControl();
            nint handle = control.Handle;
            object dispatcher = Dispatcher(control);
            InvokeDispatcher(dispatcher, "SuspendForRecreation");
            int called = 0;
            IAsyncResult[] pending = Enumerable.Range(0, 3)
                .Select(_ => control.BeginInvoke((Action)(() => called++))).ToArray();
            pending[0].AsyncWaitHandle.Dispose();
            pending[1].AsyncWaitHandle.Dispose();
            using WaitHandle thirdWait = pending[2].AsyncWaitHandle;
            const uint fillerMessage = 0x8000 + 177;
            try
            {
                // GetWindowThreadProcessId must still accept this HWND. Fill
                // only this dedicated STA's native queue to make its repost
                // fail with the actual Windows quota error.
                int posted = 0;
                while (posted < 100_000 && PostMessageW(handle, fillerMessage, 0, 0)) { posted++; }
                int quotaError = Marshal.GetLastWin32Error();
                Assert.True(posted < 100_000, "The native message queue did not reach its configured quota.");
                Assert.Equal(1816, quotaError); // ERROR_NOT_ENOUGH_QUOTA

                AggregateException failure = Assert.Throws<AggregateException>(() =>
                    InvokeDispatcher(dispatcher, "AttachWindow", handle));

                Assert.Equal(3, failure.InnerExceptions.Count);
                Win32Exception postError = Assert.IsType<Win32Exception>(failure.InnerExceptions[0]);
                Assert.Equal(quotaError, postError.NativeErrorCode);
                Assert.All(failure.InnerExceptions.Skip(1), error => Assert.IsType<ObjectDisposedException>(error));
                Assert.All(pending, result => Assert.True(result.IsCompleted));
                Assert.All(pending, result => Assert.Same(postError,
                    Assert.Throws<Win32Exception>(() => control.EndInvoke(result))));
                Assert.True(thirdWait.WaitOne(0));
                InvokeDispatcher(dispatcher, "Close");
                Assert.Throws<InvalidOperationException>(() => InvokeDispatcher(dispatcher, "AttachWindow", handle));
                Assert.Equal(0, called);
            }
            finally
            {
                while (PeekMessageW(out _, handle, fillerMessage, fillerMessage, 1)) { }
            }
        });

    [Fact]
    public void CloseDoesNotReplaceAnAlreadyCompletedCallbacksOriginalFailure()
        => OnSta(() =>
        {
            using var control = new ExposedControl();
            _ = control.Handle;
            var original = new InvalidOperationException("original callback failure");
            IAsyncResult completed = control.BeginInvoke((Action)(() => throw original));
            completed.AsyncWaitHandle.Dispose();
            Assert.Throws<ObjectDisposedException>(() => control.EndInvoke(completed));
            Assert.Same(original, Assert.Throws<InvalidOperationException>(() => control.EndInvoke(completed)));
            IAsyncResult pending = control.BeginInvoke((Action)(() => throw new InvalidOperationException("must not run")));
            using WaitHandle pendingWait = pending.AsyncWaitHandle;

            control.DestroyNativeHandle();

            Assert.True(pending.IsCompleted);
            Assert.True(pendingWait.WaitOne(0));
            Assert.Throws<ObjectDisposedException>(() => control.EndInvoke(pending));
            Assert.Same(original, Assert.Throws<InvalidOperationException>(() => control.EndInvoke(completed)));
        });

    private static object Dispatcher(Control control)
        => typeof(Control).GetField("_threadDispatcher", BindingFlags.Instance | BindingFlags.NonPublic)!
            .GetValue(control)!;

    private static void InvokeDispatcher(object dispatcher, string method, params object?[] arguments)
    {
        try
        {
            dispatcher.GetType().GetMethod(method, BindingFlags.Instance | BindingFlags.NonPublic)!
                .Invoke(dispatcher, arguments);
        }
        catch (TargetInvocationException error) when (error.InnerException is { } original)
        {
            ExceptionDispatchInfo.Capture(original).Throw();
        }
    }

    private static void OnSta(Action action)
    {
        Exception? failure = null;
        Thread thread = new(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            try { action(); }
            catch (Exception error) { failure = error; }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(15)), "The dispatcher close check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class ExposedControl : Control
    {
        internal void DestroyNativeHandle() => DestroyHandle();
        internal void RecreateNativeHandle() => RecreateHandle();
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct NativeMessage
    {
        public nint Window;
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
    private static extern bool IsWindow(nint window);

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint window);

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool PostMessageW(nint window, uint message, nint wParam, nint lParam);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool PeekMessageW(out NativeMessage message, nint window, uint minimum, uint maximum, uint remove);
}
