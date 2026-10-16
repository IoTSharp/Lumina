using System.ComponentModel;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks.Sources;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class ControlInvokeAsyncCompletionTests
{
    [Theory]
    [InlineData(false, 0)]
    [InlineData(false, 1)]
    [InlineData(false, 2)]
    [InlineData(true, 0)]
    [InlineData(true, 1)]
    [InlineData(true, 2)]
    public void DestructionCompletesEveryQueuedAsyncOverload(bool form, int closeKind)
        => OnSta(() =>
        {
            using Control control = CreateControl(form);
            nint handle = control.Handle;
            int calls = 0;
            Task[] pending = QueueAll(control, () => calls++);
            Assert.All(pending, task => Assert.False(task.IsCompleted));

            if (closeKind == 0) { control.Dispose(); }
            else if (closeKind == 1) { DestroyHandle(control); }
            else { Assert.True(DestroyWindow(handle)); }

            Assert.False(IsWindow(handle));
            Assert.False(control.IsHandleCreated);
            ObjectDisposedException closed = Failure<ObjectDisposedException>(pending[0]);
            Assert.Equal(control.GetType().Name, closed.ObjectName);
            Assert.All(pending, task => Assert.Same(closed, Failure<ObjectDisposedException>(task)));
            Assert.Equal(0, calls);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void CancellationAndCloseKeepWhicheverCompletedTheTaskFirst(bool cancelFirst)
        => OnSta(() =>
        {
            using var control = new ExposedControl();
            _ = control.Handle;
            using var cancellation = new CancellationTokenSource();
            int calls = 0;
            Task[] pending = QueueAll(control, () => calls++, cancellation.Token);

            if (cancelFirst) { cancellation.Cancel(); }
            control.Dispose();
            if (!cancelFirst) { cancellation.Cancel(); }

            foreach (Task task in pending)
            {
                if (cancelFirst) { AssertCanceled(task, cancellation.Token); }
                else { _ = Failure<ObjectDisposedException>(task); }
            }
            Assert.Equal(0, calls);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void RecreationRetainsQueuedTasksAndRunsThemExactlyOnce(bool form)
        => OnSta(() =>
        {
            using Control control = CreateControl(form);
            _ = control.Handle;
            int calls = 0;
            Task[] pending = QueueAll(control, () => calls++);

            if (control is ExposedForm exposedForm) { exposedForm.RecreateNativeHandle(); }
            else { ((ExposedControl)control).RecreateNativeHandle(); }

            Assert.True(control.IsHandleCreated);
            Assert.All(pending, task => Assert.False(task.IsCompleted));
            Dispatch(control);
            Assert.All(pending, Succeeded);
            Assert.Equal(42, ((Task<int>)pending[2]).GetAwaiter().GetResult());
            Assert.Equal(42, ((Task<int>)pending[5]).GetAwaiter().GetResult());
            Assert.Equal(pending.Length, calls);
            Dispatch(control);
            Assert.Equal(pending.Length, calls);
        });

    [Fact]
    public void ADisposedBeginInvokeWaitDoesNotOrphanFollowingAsyncTasks()
        => OnSta(() =>
        {
            using var control = new ExposedControl();
            _ = control.Handle;
            int calls = 0;
            IAsyncResult broken = control.BeginInvoke((Action)(() => calls++));
            broken.AsyncWaitHandle.Dispose();
            Task[] pending = QueueAll(control, () => calls++);

            ObjectDisposedException signalFailure = Assert.Throws<ObjectDisposedException>(control.DestroyNativeHandle);

            Assert.Contains("Set", signalFailure.StackTrace);
            ObjectDisposedException closed = Assert.Throws<ObjectDisposedException>(() => control.EndInvoke(broken));
            Assert.NotSame(signalFailure, closed);
            Assert.All(pending, task => Assert.Same(closed, Failure<ObjectDisposedException>(task)));
            Assert.False(control.IsHandleCreated);
            Assert.Equal(0, calls);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void FailedPostCompletesAcceptedTasksWithTheOriginalNativeFailure(bool disposeWait)
        => OnSta(() =>
        {
            using var control = new ExposedControl();
            _ = control.Handle;
            int calls = 0;
            Task[] pending = QueueAll(control, () => calls++);
            using var cancellation = new CancellationTokenSource();
            Task[] canceled = QueueAll(control, () => calls++, cancellation.Token);
            cancellation.Cancel();
            IAsyncResult legacy = control.BeginInvoke((Action)(() => calls++));
            using WaitHandle wait = legacy.AsyncWaitHandle;
            if (disposeWait) { wait.Dispose(); }

            // A real destroyed HWND avoids the special meanings of sentinel values.
            using var staleTarget = new ExposedControl();
            nint staleHandle = staleTarget.Handle;
            staleTarget.Dispose();
            Assert.False(IsWindow(staleHandle));
            object dispatcher = Dispatcher(control);
            dispatcher.GetType().GetField("_window", BindingFlags.Instance | BindingFlags.NonPublic)!
                .SetValue(dispatcher, staleHandle);

            Task initiating = control.InvokeAsync((Action)(() => calls++), CancellationToken.None);
            Exception failure = Failure<Exception>(initiating);
            Win32Exception postFailure;
            if (disposeWait)
            {
                AggregateException aggregate = Assert.IsType<AggregateException>(failure);
                Assert.Equal(2, aggregate.InnerExceptions.Count);
                postFailure = Assert.IsType<Win32Exception>(aggregate.InnerExceptions[0]);
                Assert.IsType<ObjectDisposedException>(aggregate.InnerExceptions[1]);
            }
            else
            {
                postFailure = Assert.IsType<Win32Exception>(failure);
                Assert.True(wait.WaitOne(0));
            }

            Assert.NotEqual(0, postFailure.NativeErrorCode);
            Assert.All(pending, task => Assert.Same(postFailure, Failure<Win32Exception>(task)));
            Assert.All(canceled, task => AssertCanceled(task, cancellation.Token));
            Assert.Same(postFailure, Assert.Throws<Win32Exception>(() => control.EndInvoke(legacy)));
            Assert.Equal(0, calls);
            _ = Failure<InvalidOperationException>(control.InvokeAsync((Action)(() => calls++), CancellationToken.None));
        });

    [Fact]
    public void FailedReattachCompletesTheRetainedTasksWithTheQuotaError()
        => OnSta(() =>
        {
            using var control = new ExposedControl();
            nint handle = control.Handle;
            object dispatcher = Dispatcher(control);
            InvokeDispatcher(dispatcher, "SuspendForRecreation");
            int calls = 0;
            Task[] pending = QueueAll(control, () => calls++);
            const uint fillerMessage = 0x8000 + 178;
            try
            {
                int posted = 0;
                while (posted < 100_000 && PostMessageW(handle, fillerMessage, 0, 0)) { posted++; }
                int quotaError = Marshal.GetLastWin32Error();
                Assert.True(posted < 100_000, "The native queue did not reach its configured quota.");
                Assert.Equal(1816, quotaError);

                Win32Exception failure = Assert.Throws<Win32Exception>(() => InvokeDispatcher(dispatcher, "AttachWindow", handle));

                Assert.Equal(quotaError, failure.NativeErrorCode);
                Assert.All(pending, task => Assert.Same(failure, Failure<Win32Exception>(task)));
                Assert.Equal(0, calls);
            }
            finally
            {
                while (PeekMessageW(out _, handle, fillerMessage, fillerMessage, 1)) { }
            }
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void RunningSynchronousCallbackCanDisposeItsControlWithoutLosingItsOutcome(bool fail)
        => OnSta(() =>
        {
            using var control = new ExposedControl();
            _ = control.Handle;
            var original = new TargetInvocationException(new InvalidOperationException("original callback failure"));
            Task<int> running = control.InvokeAsync(() =>
            {
                control.Dispose();
                if (fail) { throw original; }
                return 42;
            });
            Task pending = control.InvokeAsync((Action)(() => throw new InvalidOperationException("must not run")), CancellationToken.None);

            Dispatch(control);

            _ = Failure<ObjectDisposedException>(pending);
            if (fail) { Assert.Same(original, Failure<TargetInvocationException>(running)); }
            else { Succeeded(running); Assert.Equal(42, running.GetAwaiter().GetResult()); }
        });

    [Theory]
    [InlineData("task", 0)]
    [InlineData("task", 1)]
    [InlineData("task", 2)]
    [InlineData("valueTask", 0)]
    [InlineData("valueTask", 1)]
    [InlineData("valueTask", 2)]
    [InlineData("valueTaskResult", 0)]
    [InlineData("valueTaskResult", 1)]
    [InlineData("valueTaskResult", 2)]
    public void StartedAsyncCallbackKeepsItsResultOrOriginalExceptionAfterClose(string kind, int outcome)
        => OnSta(() =>
        {
            using var control = new ExposedControl();
            _ = control.Handle;
            using var callbackCancellation = new CancellationTokenSource();
            var source = new SingleUseSource();
            var taskSource = new TaskCompletionSource<int>(TaskCreationOptions.RunContinuationsAsynchronously);
            int calls = 0;
            Task running = kind switch
            {
                "task" => control.InvokeAsync((Func<CancellationToken, Task>)(_ => { calls++; return taskSource.Task; }), CancellationToken.None),
                "valueTask" => control.InvokeAsync((Func<CancellationToken, ValueTask>)(_ => { calls++; return source.VoidTask; })),
                _ => control.InvokeAsync((Func<CancellationToken, ValueTask<int>>)(_ => { calls++; return source.Task; }))
            };
            Dispatch(control);
            Assert.Equal(1, calls);
            Assert.False(running.IsCompleted);
            Task pending = control.InvokeAsync((Action)(() => calls++), CancellationToken.None);

            control.Dispose();

            _ = Failure<ObjectDisposedException>(pending);
            Assert.False(running.IsCompleted);
            Exception? original = outcome switch
            {
                1 => new TargetInvocationException(new InvalidOperationException("original asynchronous failure")),
                2 => new OperationCanceledException("callback cancellation", callbackCancellation.Token),
                _ => null
            };
            if (original is null)
            {
                if (kind == "task") { taskSource.SetResult(42); }
                else { source.SetResult(42); }
                Succeeded(running);
                if (kind == "valueTaskResult") { Assert.Equal(42, ((Task<int>)running).GetAwaiter().GetResult()); }
            }
            else
            {
                if (kind == "task") { taskSource.SetException(original); }
                else { source.SetException(original); }
                Assert.Same(original, Failure<Exception>(running));
                Assert.Equal(outcome == 2, running.IsCanceled);
                if (original is OperationCanceledException canceled)
                {
                    Assert.Equal(callbackCancellation.Token, canceled.CancellationToken);
                }
            }
            if (kind != "task") { Assert.Equal(1, source.GetResultCount); }
            Assert.Equal(1, calls);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void CancellationAfterCloseStillConsumesAnAlreadyStartedValueTaskOnce(bool generic)
        => OnSta(() =>
        {
            using var control = new ExposedControl();
            _ = control.Handle;
            using var cancellation = new CancellationTokenSource();
            var source = new SingleUseSource();
            Task running = generic
                ? control.InvokeAsync((Func<CancellationToken, ValueTask<int>>)(_ => source.Task), cancellation.Token)
                : control.InvokeAsync((Func<CancellationToken, ValueTask>)(_ => source.VoidTask), cancellation.Token);
            Dispatch(control);

            control.Dispose();
            Assert.False(running.IsCompleted);
            cancellation.Cancel();
            AssertCanceled(running, cancellation.Token);
            Assert.Equal(0, source.GetResultCount);
            source.SetResult(42);

            Assert.True(SpinWait.SpinUntil(() => source.GetResultCount != 0, TimeSpan.FromSeconds(3)), "The started ValueTask was not consumed.");
            Assert.Equal(1, source.GetResultCount);
            AssertCanceled(running, cancellation.Token);
        });

    [Fact]
    public void ClosingTheWindowReleasesAWorkerAwaitingAnAcceptedTask()
        => OnSta(() =>
        {
            using var control = new ExposedControl();
            _ = control.Handle;
            using var accepted = new ManualResetEventSlim();
            Exception? observed = null;
            int calls = 0;
            Thread worker = new(() =>
            {
                Task queued = control.InvokeAsync((Action)(() => calls++), CancellationToken.None);
                accepted.Set();
                try { queued.GetAwaiter().GetResult(); }
                catch (Exception error) { observed = error; }
            }) { IsBackground = true };
            worker.Start();
            Assert.True(accepted.Wait(TimeSpan.FromSeconds(3)));

            control.Dispose();

            Assert.True(worker.Join(TimeSpan.FromSeconds(3)), "Close did not release the waiting worker.");
            Assert.IsType<ObjectDisposedException>(observed);
            Assert.Equal(0, calls);
        });

    private static Task[] QueueAll(Control control, Action callback, CancellationToken token = default)
    {
        List<Task> pending = [];
        if (!token.CanBeCanceled) { pending.Add(control.InvokeAsync(callback)); }
        pending.Add(control.InvokeAsync(callback, token));
        pending.Add(control.InvokeAsync(() => { callback(); return 42; }, token));
        pending.Add(control.InvokeAsync((Func<CancellationToken, Task>)(_ => { callback(); return Task.CompletedTask; }), token));
        pending.Add(control.InvokeAsync((Func<CancellationToken, ValueTask>)(_ => { callback(); return ValueTask.CompletedTask; }), token));
        pending.Add(control.InvokeAsync((Func<CancellationToken, ValueTask<int>>)(_ => { callback(); return ValueTask.FromResult(42); }), token));
        return [.. pending];
    }

    private static TException Failure<TException>(Task task) where TException : Exception
    {
        Completed(task);
        return Assert.ThrowsAny<TException>(() => task.GetAwaiter().GetResult());
    }

    private static void Succeeded(Task task)
    {
        Completed(task);
        task.GetAwaiter().GetResult();
        Assert.True(task.IsCompletedSuccessfully);
    }

    private static void AssertCanceled(Task task, CancellationToken token)
    {
        OperationCanceledException failure = Failure<OperationCanceledException>(task);
        Assert.True(task.IsCanceled);
        Assert.Equal(token, failure.CancellationToken);
    }

    private static void Completed(Task task)
        => Assert.True(SpinWait.SpinUntil(() => task.IsCompleted, TimeSpan.FromSeconds(3)), "The accepted InvokeAsync task did not complete.");

    private static Control CreateControl(bool form) => form ? new ExposedForm() : new ExposedControl();

    private static void DestroyHandle(Control control)
    {
        if (control is ExposedForm form) { form.DestroyNativeHandle(); }
        else { ((ExposedControl)control).DestroyNativeHandle(); }
    }

    private static void Dispatch(Control control)
    {
        uint message = RegisterWindowMessageW("Lumina.Forms.ThreadCallback");
        Assert.NotEqual(0u, message);
        _ = SendMessageW(control.Handle, message, 0, 0);
    }

    private static object Dispatcher(Control control)
        => typeof(Control).GetField("_threadDispatcher", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(control)!;

    private static void InvokeDispatcher(object dispatcher, string method, params object?[] arguments)
    {
        try
        {
            dispatcher.GetType().GetMethod(method, BindingFlags.Instance | BindingFlags.NonPublic)!.Invoke(dispatcher, arguments);
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
            List<Exception> reported = [];
            ThreadExceptionEventHandler handler = (_, args) => reported.Add(args.Exception);
            Application.ThreadException += handler;
            try { action(); Assert.Empty(reported); }
            catch (Exception error) { failure = error; }
            finally { Application.ThreadException -= handler; }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(15)), "The InvokeAsync completion check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class ExposedControl : Control
    {
        internal void DestroyNativeHandle() => DestroyHandle();
        internal void RecreateNativeHandle() => RecreateHandle();
    }

    private sealed class ExposedForm : Form
    {
        internal void DestroyNativeHandle() => DestroyHandle();
        internal void RecreateNativeHandle() => RecreateHandle();
    }

    private sealed class SingleUseSource : IValueTaskSource<int>, IValueTaskSource
    {
        private ManualResetValueTaskSourceCore<int> _core = new() { RunContinuationsAsynchronously = true };
        private int _getResultCount;
        internal int GetResultCount => Volatile.Read(ref _getResultCount);
        internal ValueTask<int> Task => new(this, _core.Version);
        internal ValueTask VoidTask => new(this, _core.Version);
        internal void SetResult(int result) => _core.SetResult(result);
        internal void SetException(Exception error) => _core.SetException(error);
        public int GetResult(short token)
        {
            Assert.Equal(1, Interlocked.Increment(ref _getResultCount));
            return _core.GetResult(token);
        }
        void IValueTaskSource.GetResult(short token) => _ = GetResult(token);
        public ValueTaskSourceStatus GetStatus(short token) => _core.GetStatus(token);
        public void OnCompleted(Action<object?> continuation, object? state, short token, ValueTaskSourceOnCompletedFlags flags)
            => _core.OnCompleted(continuation, state, token, flags);
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

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern uint RegisterWindowMessageW(string name);

    [DllImport("user32.dll")]
    private static extern nint SendMessageW(nint window, uint message, nint wParam, nint lParam);
}
