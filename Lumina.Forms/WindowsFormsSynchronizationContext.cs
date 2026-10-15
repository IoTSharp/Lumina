using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Lumina.Forms;

/// <summary>
/// Sends and posts callbacks to the native message queue of its creating thread.
/// </summary>
/// <remarks>
/// Constructing this context does not install it as <see cref="SynchronizationContext.Current"/>.
/// Automatic installation and restoration are not implemented yet.
/// </remarks>
public sealed class WindowsFormsSynchronizationContext : SynchronizationContext, IDisposable
{
    [ThreadStatic]
    private static SynchronizationContextWindow? t_window;

    private readonly WeakReference<Thread> _destinationThread;
    private volatile SynchronizationContextWindow? _window;

    /// <summary>
    /// Creates a context that dispatches callbacks on the current thread.
    /// </summary>
    public WindowsFormsSynchronizationContext()
    {
        _destinationThread = new WeakReference<Thread>(Thread.CurrentThread);
        // WinForms shares its marshalling control across contexts created on one thread.
        // In the fixed reference, disposing it does not recreate that thread's control.
        _window = t_window ??= new SynchronizationContextWindow();
    }

    private WindowsFormsSynchronizationContext(SynchronizationContextWindow? window, WeakReference<Thread> destinationThread)
    {
        _window = window;
        _destinationThread = destinationThread;
    }

    /// <summary>
    /// Sends a callback synchronously to the context's creating thread.
    /// </summary>
    public override void Send(SendOrPostCallback d, object? state)
    {
        if (!_destinationThread.TryGetTarget(out Thread? thread) || !thread.IsAlive)
        {
            throw new InvalidAsynchronousStateException("The destination thread is no longer valid.");
        }

        _window?.Send(d, state);
    }

    /// <summary>
    /// Queues a callback for the context's creating thread.
    /// </summary>
    public override void Post(SendOrPostCallback d, object? state)
        => _window?.Post(d, state);

    /// <summary>
    /// Creates another context that shares the same native dispatch destination.
    /// </summary>
    public override SynchronizationContext CreateCopy()
        => new WindowsFormsSynchronizationContext(_window, _destinationThread);

    /// <summary>
    /// Disposes the native dispatch destination shared by this thread's contexts.
    /// </summary>
    public void Dispose()
    {
        _window?.Dispose();
        _window = null;
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Owns only the message HWND; all callback queuing remains in WindowThreadDispatcher.
    /// </summary>
    private sealed class SynchronizationContextWindow : IDisposable
    {
        private const string WindowClassName = "Lumina.Forms.SynchronizationContextWindow";
        private static readonly Lock RegistrationGate = new();
        private static bool s_registered;

        private volatile nint _handle;
        private GCHandle _selfHandle;
        private WindowThreadDispatcher? _dispatcher;

        internal SynchronizationContextWindow()
        {
            if (!OperatingSystem.IsWindows())
            {
                throw new PlatformNotSupportedException("The native synchronization context currently requires Windows.");
            }

            nint instance = Win32.GetModuleHandleW(null);
            EnsureClassRegistered(instance);
            _selfHandle = GCHandle.Alloc(this);
            try
            {
                _handle = Win32.CreateWindowExW(0, WindowClassName, string.Empty, 0,
                    0, 0, 0, 0, (nint)(-3) /* HWND_MESSAGE */, 0, instance, GCHandle.ToIntPtr(_selfHandle));
                if (_handle == 0)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                Application.NotifyWindowCreated();
                _dispatcher = new WindowThreadDispatcher(_handle, this);
            }
            catch
            {
                if (_handle != 0) { _ = Win32.DestroyWindow(_handle); }
                if (_selfHandle.IsAllocated) { _selfHandle.Free(); }
                throw;
            }
        }

        internal void Send(SendOrPostCallback callback, object? state)
        {
            WindowThreadDispatcher dispatcher = GetDispatcher();
            // The reference hangs for a live destination and null Send callback.
            // Keep the existing Control.Invoke guard instead of blocking the UI forever.
            ArgumentNullException.ThrowIfNull(callback, "d");
            _ = dispatcher.Invoke(this, callback, [state]);
        }

        internal void Post(SendOrPostCallback callback, object? state)
        {
            WindowThreadDispatcher dispatcher = GetDispatcher();
            // A null Post is accepted without a callback by WinForms 10.0.11.
            if (callback is not null)
            {
                _ = dispatcher.BeginInvoke(this, callback, [state]);
            }
        }

        public void Dispose()
        {
            nint handle = _handle;
            if (handle == 0) { return; }

            if (_dispatcher?.InvokeRequired == true)
            {
                // Destruction must occur on the native window's owner thread. A sent
                // window message does not drain already-posted managed callbacks first.
                _ = Win32.SendMessageW(handle, Win32.WM_CLOSE, 0, 0);
                if (_handle != 0)
                {
                    throw new InvalidOperationException("The synchronization context window could not be destroyed on its owning thread.");
                }
            }
            else if (!Win32.DestroyWindow(handle))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        private WindowThreadDispatcher GetDispatcher()
            => _handle != 0 && _dispatcher is { } dispatcher
                ? dispatcher
                : throw WindowThreadDispatcher.NoMarshalingHandle();

        private static unsafe void EnsureClassRegistered(nint instance)
        {
            lock (RegistrationGate)
            {
                if (s_registered) { return; }
                var windowClass = new Win32.WNDCLASSEXW
                {
                    cbSize = (uint)Marshal.SizeOf<Win32.WNDCLASSEXW>(),
                    hInstance = instance,
                    lpszClassName = WindowClassName,
                    lpfnWndProc = (nint)(delegate* unmanaged[Stdcall]<nint, uint, nint, nint, nint>)&WindowProc
                };
                if (Win32.RegisterClassExW(ref windowClass) == 0)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                s_registered = true;
            }
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        private static unsafe nint WindowProc(nint window, uint message, nint wParam, nint lParam)
        {
            if (message == Win32.WM_NCCREATE)
            {
                _ = Win32.SetWindowLongPtrW(window, Win32.GWLP_USERDATA, ((Win32.CREATESTRUCTW*)lParam)->lpCreateParams);
            }

            nint userData = Win32.GetWindowLongPtrW(window, Win32.GWLP_USERDATA);
            if (userData != 0 && GCHandle.FromIntPtr(userData).Target is SynchronizationContextWindow owner)
            {
                if (owner._dispatcher?.TryDispatch(message) == true) { return 0; }
                if (message == Win32.WM_CLOSE)
                {
                    _ = Win32.DestroyWindow(window);
                    return 0;
                }

                if (message == Win32.WM_NCDESTROY)
                {
                    owner._dispatcher?.Close();
                    owner._handle = 0;
                    _ = Win32.SetWindowLongPtrW(window, Win32.GWLP_USERDATA, 0);
                    if (owner._selfHandle.IsAllocated) { owner._selfHandle.Free(); }
                }
            }

            return Win32.DefWindowProcW(window, message, wParam, lParam);
        }
    }
}
