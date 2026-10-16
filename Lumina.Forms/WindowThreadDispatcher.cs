using System.ComponentModel;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Lumina.Forms;

/// <summary>
/// Marshals callbacks through the message queue of an existing native control window.
/// </summary>
internal sealed class WindowThreadDispatcher
{
    private readonly Lock _gate = new();
    private readonly Queue<Invocation> _callbacks = [];
    private nint _window;
    private readonly uint _threadId;
    private readonly object _owner;
    private readonly int _callbackMessage;
    private bool _closed;

    internal WindowThreadDispatcher(nint window, object owner)
    {
        _window = window;
        _owner = owner;
        _threadId = Win32.GetWindowThreadProcessId(window, out _);
        _callbackMessage = CallbackMessage.Value;
        if (_threadId == 0)
        {
            throw NoMarshalingHandle();
        }
    }

    internal bool InvokeRequired => _threadId != Win32.GetCurrentThreadId();

    internal object? Invoke(object caller, Delegate method, object?[]? args)
    {
        Invocation invocation = Enqueue(caller, method, args, synchronous: true);
        return invocation.GetResult();
    }

    internal IAsyncResult BeginInvoke(object caller, Delegate method, object?[]? args)
        => Enqueue(caller, method, args, synchronous: false);

    internal async Task<T> InvokeAsync<T>(Func<T> callback, CancellationToken cancellationToken)
    {
        var completion = new TaskCompletionSource<T>(TaskCreationOptions.RunContinuationsAsynchronously);
        using CancellationTokenRegistration registration = cancellationToken.Register(() => completion.TrySetCanceled(cancellationToken));
        _ = BeginInvoke(_owner, (Action)(() =>
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            try
            {
                completion.TrySetResult(callback());
            }
            catch (Exception exception)
            {
                // Task callbacks own their failures. The BeginInvoke wrapper must finish
                // successfully so Drain does not also raise Application.ThreadException.
                completion.TrySetException(exception);
            }
        }), args: null);

        // Awaiting the original exception also preserves OperationCanceledException and
        // its own token, rather than replacing it with a newly created canceled task.
        return await completion.Task.ConfigureAwait(false);
    }

    internal async Task InvokeAsync(Func<CancellationToken, Task> callback, CancellationToken cancellationToken)
    {
        var completion = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        using CancellationTokenRegistration registration = cancellationToken.Register(() => completion.TrySetCanceled(cancellationToken));
        _ = BeginInvoke(_owner, (Action)(() => _ = CompleteCallbackAsync()), args: null);
        await completion.Task.ConfigureAwait(false);

        async Task CompleteCallbackAsync()
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            try
            {
                await callback(cancellationToken).ConfigureAwait(false);
                completion.TrySetResult();
            }
            catch (Exception exception)
            {
                completion.TrySetException(exception);
            }
        }
    }

    internal async Task InvokeAsync(Func<CancellationToken, ValueTask> callback, CancellationToken cancellationToken)
    {
        var completion = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        using CancellationTokenRegistration registration = cancellationToken.Register(() => completion.TrySetCanceled(cancellationToken));
        _ = BeginInvoke(_owner, (Action)(() => _ = CompleteCallbackAsync()), args: null);
        await completion.Task.ConfigureAwait(false);

        async Task CompleteCallbackAsync()
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            try
            {
                // Await the returned ValueTask exactly once, even if cancellation has
                // already completed the caller's Task while the callback was running.
                await callback(cancellationToken).ConfigureAwait(false);
                completion.TrySetResult();
            }
            catch (Exception exception)
            {
                completion.TrySetException(exception);
            }
        }
    }

    internal async Task<T> InvokeAsync<T>(Func<CancellationToken, ValueTask<T>> callback, CancellationToken cancellationToken)
    {
        var completion = new TaskCompletionSource<T>(TaskCreationOptions.RunContinuationsAsynchronously);
        using CancellationTokenRegistration registration = cancellationToken.Register(() => completion.TrySetCanceled(cancellationToken));
        _ = BeginInvoke(_owner, (Action)(() => _ = CompleteCallbackAsync()), args: null);
        return await completion.Task.ConfigureAwait(false);

        async Task CompleteCallbackAsync()
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            try
            {
                T result = await callback(cancellationToken).ConfigureAwait(false);
                completion.TrySetResult(result);
            }
            catch (Exception exception)
            {
                // Preserve the callback's original exception, including cancellation
                // tokens and wrapper exceptions, without reporting ThreadException.
                completion.TrySetException(exception);
            }
        }
    }

    internal static object? EndInvoke(IAsyncResult asyncResult)
    {
        ArgumentNullException.ThrowIfNull(asyncResult);
        if (asyncResult is not Invocation invocation)
        {
            throw new ArgumentException("The asynchronous result did not originate from BeginInvoke.", nameof(asyncResult));
        }

        if (!invocation.IsCompleted)
        {
            invocation.Dispatcher.Wait(invocation);
        }

        return invocation.GetResult();
    }

    internal bool TryDispatch(uint message)
    {
        if (message != (uint)_callbackMessage)
        {
            return false;
        }

        Drain();
        return true;
    }

    internal void Close()
    {
        lock (_gate)
        {
            _closed = true;
            _window = 0;
            CompletePendingCallbacks(new ObjectDisposedException(_owner.GetType().Name));
        }
    }

    internal void SuspendForRecreation()
    {
        lock (_gate)
        {
            // The Control still owns these callbacks while only its HWND is replaced.
            _window = 0;
        }
    }

    internal void AttachWindow(nint window)
    {
        uint threadId = Win32.GetWindowThreadProcessId(window, out _);
        if (threadId == 0 || threadId != _threadId)
        {
            throw new InvalidOperationException("A recreated window must belong to the original dispatch thread.");
        }

        lock (_gate)
        {
            if (_closed) { throw NoMarshalingHandle(); }
            _window = window;
            // Messages sent to the old HWND disappear with it. Repost the retained queue.
            if (_callbacks.Count != 0 && !Win32.PostMessageW(window, _callbackMessage, 0, 0))
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _closed = true;
                _window = 0;
                CompletePendingCallbacks(error, initiatingFailure: error);
            }
        }
    }

    internal static InvalidOperationException NoMarshalingHandle()
        => new("Invoke or BeginInvoke cannot be called on a control until an appropriate window handle has been created.");

    private Invocation Enqueue(object caller, Delegate method, object?[]? args, bool synchronous)
    {
        ArgumentNullException.ThrowIfNull(method);
        bool sameThread = !InvokeRequired;
        var invocation = new Invocation(this, caller, method, args, synchronous, synchronous && sameThread ? null : ExecutionContext.Capture());
        lock (_gate)
        {
            if (_closed)
            {
                throw NoMarshalingHandle();
            }

            _callbacks.Enqueue(invocation);
            // Posting under the queue lock makes destruction and a failed post finish every
            // accepted callback before a synchronous caller can begin waiting.
            if (!(synchronous && sameThread) && _window != 0 && !Win32.PostMessageW(_window, _callbackMessage, 0, 0))
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _closed = true;
                _window = 0;
                CompletePendingCallbacks(error, initiatingFailure: error);
            }
        }

        if (synchronous)
        {
            Wait(invocation);
        }

        return invocation;
    }

    private void CompletePendingCallbacks(Exception exception, Exception? initiatingFailure = null)
    {
        // The caller owns _gate and has closed the queue. A caller can dispose
        // an exposed AsyncWaitHandle, causing Complete to throw after setting
        // IsCompleted. Finish every accepted item before propagating these
        // failures so other callers are not orphaned by that disposed handle.
        // This deliberately strengthens WinForms 10.0.11's failure cleanup.
        List<Exception>? failures = initiatingFailure is null ? null : [initiatingFailure];
        while (_callbacks.TryDequeue(out Invocation? pending))
        {
            try
            {
                pending.CompleteException(exception);
            }
            catch (Exception completionFailure)
            {
                (failures ??= []).Add(completionFailure);
            }
        }

        if (failures is { Count: 1 })
        {
            ExceptionDispatchInfo.Capture(failures[0]).Throw();
        }
        if (failures is { Count: > 1 })
        {
            throw new AggregateException("One or more queued callbacks could not signal completion.", failures);
        }
    }

    private void Wait(Invocation invocation)
    {
        if (!InvokeRequired)
        {
            // Like EndInvoke on the UI thread, a nested synchronous Invoke processes older
            // callbacks first instead of blocking the thread that owns their HWND.
            Drain();
        }

        if (!invocation.IsCompleted)
        {
            invocation.AsyncWaitHandle.WaitOne();
        }
    }

    private void Drain()
    {
        List<Exception>? completionFailures = null;
        while (true)
        {
            Invocation? invocation;
            lock (_gate)
            {
                if (!_callbacks.TryDequeue(out invocation))
                {
                    break;
                }
            }

            Exception? completionFailure = null;
            try
            {
                invocation.Execute();
            }
            catch (Exception failure)
            {
                // Invocation.Execute records callback failures before signaling its
                // completion. A caller may dispose the exposed wait handle, causing
                // Complete.Set to throw after IsCompleted is already true. Keep
                // draining accepted callbacks and surface every completion failure
                // after their FIFO execution has finished.
                completionFailure = failure;
                (completionFailures ??= []).Add(failure);
            }

            if (completionFailure is null && !invocation.Synchronous && invocation.Exception is { } exception)
            {
                try
                {
                    Application.OnThreadException(exception);
                }
                catch (Exception reportingException)
                {
                    // A managed exception must never escape a native window procedure.
                    // A failing application error handler cannot be ignored either.
                    Environment.FailFast("The application UI thread exception handler failed.", reportingException);
                }
            }
        }

        if (completionFailures is { Count: 1 })
        {
            ExceptionDispatchInfo.Capture(completionFailures[0]).Throw();
        }
        if (completionFailures is { Count: > 1 })
        {
            throw new AggregateException("One or more queued callbacks could not signal completion.", completionFailures);
        }
    }

    private static class CallbackMessage
    {
        // Register lazily: constructing a managed, handleless Control remains portable.
        internal static readonly int Value = Register();

        private static int Register()
        {
            uint message = Win32.RegisterWindowMessageW("Lumina.Forms.ThreadCallback");
            if (message == 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return checked((int)message);
        }
    }

    private sealed class Invocation : IAsyncResult
    {
        private readonly Lock _completionGate = new();
        private readonly Delegate _method;
        private readonly object _caller;
        private readonly object?[]? _args;
        private readonly ExecutionContext? _executionContext;
        private SynchronizationContext? _synchronizationContext;
        private ManualResetEvent? _completion;
        private object? _result;
        private ExceptionDispatchInfo? _exception;
        private volatile bool _completed;

        internal Invocation(WindowThreadDispatcher dispatcher, object caller, Delegate method, object?[]? args, bool synchronous, ExecutionContext? executionContext)
        {
            Dispatcher = dispatcher;
            _caller = caller;
            _method = method;
            _args = args;
            Synchronous = synchronous;
            _executionContext = executionContext;
        }

        internal WindowThreadDispatcher Dispatcher { get; }
        internal bool Synchronous { get; }
        internal Exception? Exception => _exception?.SourceException;
        public object? AsyncState => null;
        public bool CompletedSynchronously => false;
        public bool IsCompleted => _completed;

        public WaitHandle AsyncWaitHandle
        {
            get
            {
                lock (_completionGate)
                {
                    return _completion ??= new ManualResetEvent(_completed);
                }
            }
        }

        internal object? GetResult()
        {
            _exception?.Throw();
            return _result;
        }

        internal void Execute()
        {
            try
            {
                if (_executionContext is null)
                {
                    InvokeDelegate();
                }
                else
                {
                    if (SynchronizationContext.Current is null)
                    {
                        WindowsFormsSynchronizationContext.InstallIfNeeded();
                    }

                    // Ambient caller state flows, but the native destination owns
                    // the synchronization context used by callback continuations.
                    _synchronizationContext = SynchronizationContext.Current;
                    ExecutionContext.Run(_executionContext, static state => ((Invocation)state!).InvokeWithSynchronizationContext(), this);
                }
            }
            catch (Exception exception)
            {
                // WinForms exposes the base callback exception for both direct delegates
                // and DynamicInvoke, including its original throw location.
                _exception = ExceptionDispatchInfo.Capture(exception.GetBaseException());
            }
            finally
            {
                Complete();
            }
        }

        internal void CompleteException(Exception exception)
        {
            _exception = ExceptionDispatchInfo.Capture(exception);
            Complete();
        }

        private void InvokeWithSynchronizationContext()
        {
            if (_synchronizationContext is null)
            {
                InvokeDelegate();
                return;
            }

            SynchronizationContext? previous = SynchronizationContext.Current;
            try
            {
                SynchronizationContext.SetSynchronizationContext(_synchronizationContext);
                InvokeDelegate();
            }
            finally
            {
                SynchronizationContext.SetSynchronizationContext(previous);
            }
        }

        private void InvokeDelegate()
        {
            if (_method is Action action)
            {
                action();
                _result = null;
            }
            else if (_method is EventHandler handler)
            {
                object? sender = _args is { Length: > 0 } ? _args[0] : _caller;
                EventArgs eventArgs = _args is { Length: > 1 } ? (EventArgs)_args[1]! : EventArgs.Empty;
                handler(sender, eventArgs);
                _result = null;
            }
            else if (_method is WaitCallback waitCallback)
            {
                waitCallback(_args![0]);
                _result = null;
            }
            else if (_method is SendOrPostCallback sendOrPostCallback)
            {
                sendOrPostCallback(_args![0]);
                _result = null;
            }
            else
            {
                _result = _method.DynamicInvoke(_args);
            }
        }

        private void Complete()
        {
            lock (_completionGate)
            {
                _completed = true;
                _completion?.Set();
            }
        }
    }
}
