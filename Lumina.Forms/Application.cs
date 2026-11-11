using Microsoft.Win32;
using Lumina;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Lumina.Forms;

/// <summary>
/// Provides the LuminaForms application bootstrap surface, including message loop startup and application-wide visual style defaults.
/// </summary>
public static partial class Application
{
    private const string PersonalizeRegistryKey = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
    private const string VisualStylesManifestFileName = "Lumina.Forms.CommonControls.manifest";
    private static readonly string s_visualStylesManifestPath = Path.Combine(Path.GetTempPath(), "Lumina.Forms", VisualStylesManifestFileName);
    private static readonly string s_visualStylesManifestContents =
        """
        <?xml version="1.0" encoding="utf-8" standalone="yes"?>
        <assembly manifestVersion="1.0" xmlns="urn:schemas-microsoft-com:asm.v1">
          <assemblyIdentity version="1.0.0.0" name="Lumina.Forms.VisualStyles" processorArchitecture="*" type="win32" />
          <description>Lumina.Forms common controls v6 activation context</description>
          <dependency>
            <dependentAssembly>
              <assemblyIdentity
                type="win32"
                name="Microsoft.Windows.Common-Controls"
                version="6.0.0.0"
                processorArchitecture="*"
                publicKeyToken="6595b64144ccf1df"
                language="*" />
            </dependentAssembly>
          </dependency>
        </assembly>
        """;
    private static readonly Lock s_syncRoot = new();
    private static readonly List<Form> s_openForms = [];
    private static readonly FormCollection s_openFormsCollection = new();
    private static readonly Dictionary<uint, ThreadLoopContext> s_threadLoops = [];
    private static bool s_commonControlsInitialized;
    private static bool s_visualStylesEnabled;
    private static bool s_compatibleTextRenderingDefault;
    private static bool s_nativeWindowCreated;
    private static bool s_exiting;
    private static UnhandledExceptionMode s_unhandledExceptionMode;

    [ThreadStatic]
    private static nint t_visualStylesActivationContext;

    [ThreadStatic]
    private static nuint t_visualStylesActivationCookie;

    [ThreadStatic]
    private static ThreadExceptionEventHandler? t_threadException;

    [ThreadStatic]
    private static bool t_handlingThreadException;

    [ThreadStatic]
    private static int t_messageLoopDepth;

    [ThreadStatic]
    private static ApplicationContext? t_applicationContext;

    [ThreadStatic]
    private static List<IMessageFilter>? t_messageFilters;

    [ThreadStatic]
    private static IMessageFilter[]? t_messageFilterSnapshot;

    [ThreadStatic]
    private static bool t_filtersChanged;

    [ThreadStatic]
    private static int t_filterDepth;

    [ThreadStatic]
    private static int t_modalDepth;

    [ThreadStatic]
    private static bool t_exitRequested;

    [ThreadStatic]
    private static int? t_pendingQuitExitCode;

    [ThreadStatic]
    private static MessageLoopCallback? t_messageLoopCallback;

    [ThreadStatic]
    private static ThreadLoopContext? t_threadLoop;

    [ThreadStatic]
    private static EventHandler? t_idle;

    [ThreadStatic]
    private static EventHandler? t_enterThreadModal;

    [ThreadStatic]
    private static EventHandler? t_leaveThreadModal;

    [ThreadStatic]
    private static UnhandledExceptionMode? t_unhandledExceptionMode;

    [ThreadStatic]
    private static bool t_nativeWindowCreated;

    /// <summary>Occurs when the current application's message loop exits.</summary>
    public static event EventHandler? ApplicationExit;

    /// <summary>Occurs when the current thread enters a modal loop.</summary>
    public static event EventHandler? EnterThreadModal
    {
        add => t_enterThreadModal += value;
        remove => t_enterThreadModal -= value;
    }

    /// <summary>Occurs when the current thread leaves a modal loop.</summary>
    public static event EventHandler? LeaveThreadModal
    {
        add => t_leaveThreadModal += value;
        remove => t_leaveThreadModal -= value;
    }

    /// <summary>Occurs when the current thread has no queued message to dispatch.</summary>
    public static event EventHandler? Idle
    {
        add => t_idle += value;
        remove => t_idle -= value;
    }

    /// <summary>Occurs when the current thread's message loop exits.</summary>
    public static event EventHandler? ThreadExit;

    /// <summary>Gets the application's currently open forms.</summary>
    public static FormCollection OpenForms => s_openFormsCollection;

    /// <summary>Gets whether the current thread is running a LuminaForms message loop.</summary>
    public static bool MessageLoop => t_messageLoopDepth > 0 || (t_messageLoopCallback?.Invoke() ?? false);

    /// <summary>Gets whether the current thread can continue dispatching messages.</summary>
    public static bool AllowQuit
    {
        get { lock (s_syncRoot) { return s_threadLoops.Count > 0; } }
    }

    /// <summary>Delegate used by a custom message loop registration.</summary>
    public delegate bool MessageLoopCallback();

    /// <summary>
    /// Gets or sets the input language for the current thread.
    /// </summary>
    public static InputLanguage CurrentInputLanguage
    {
        get => InputLanguage.CurrentInputLanguage;
        set => InputLanguage.CurrentInputLanguage = value;
    }

    /// <summary>
    /// Initializes OLE for the current thread and returns its apartment state.
    /// </summary>
    /// <remarks>
    /// Repeated calls share one initialization. The owning thread releases it
    /// when its outer LuminaForms message loop exits, or when it calls
    /// <see cref="ExitThread"/> without a running loop. An existing MTA apartment
    /// remains owned by its caller.
    /// </remarks>
    public static ApartmentState OleRequired() => ApplicationOleState.OleRequired();

    /// <summary>
    /// Occurs on the UI thread when an asynchronous window callback throws an exception.
    /// </summary>
    public static event ThreadExceptionEventHandler ThreadException
    {
        add => t_threadException += value;
        remove => t_threadException -= value;
    }

    /// <summary>
    /// Raises <see cref="ThreadException"/> on the current thread, or displays the exception.
    /// </summary>
    /// <param name="t">The exception to report.</param>
    /// <remarks>
    /// The default Windows prompt currently provides acknowledgement and continuation.
    /// The full WinForms ThreadExceptionDialog and unhandled-exception modes remain unsupported.
    /// </remarks>
    public static void OnThreadException(Exception t)
    {
        ArgumentNullException.ThrowIfNull(t);
        if (t_handlingThreadException)
        {
            return;
        }

        t_handlingThreadException = true;
        try
        {
            if (t_threadException is { } handler)
            {
                handler(Thread.CurrentThread, new ThreadExceptionEventArgs(t));
                return;
            }

            if (!OperatingSystem.IsWindows())
            {
                throw new PlatformNotSupportedException("The default thread exception dialog requires Windows.");
            }

            // Keep error reporting independent of application themes, managed forms and
            // nested framework initialization. Closing this prompt continues the UI loop.
            if (Win32.MessageBoxW(0, t.ToString(), "Unhandled thread exception", Win32.MB_ICONERROR) == 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
        finally
        {
            t_handlingThreadException = false;
        }
    }

    /// <summary>Sets the unhandled-exception policy for the current UI thread.</summary>
    public static void SetUnhandledExceptionMode(UnhandledExceptionMode mode)
        => SetUnhandledExceptionMode(mode, threadScope: true);

    /// <summary>Sets the exception policy before native windows are created.</summary>
    public static void SetUnhandledExceptionMode(UnhandledExceptionMode mode, bool threadScope)
    {
        if ((uint)mode > (uint)UnhandledExceptionMode.CatchException)
        {
            throw new InvalidEnumArgumentException(nameof(mode), (int)mode, typeof(UnhandledExceptionMode));
        }

        lock (s_syncRoot)
        {
            if (threadScope ? t_nativeWindowCreated : s_nativeWindowCreated)
            {
                throw new InvalidOperationException("The unhandled-exception policy must be set before native windows are created.");
            }
            if (threadScope) { t_unhandledExceptionMode = mode; }
            else { s_unhandledExceptionMode = mode; }
        }
    }

    private static bool CatchThreadException
    {
        get
        {
            UnhandledExceptionMode mode = t_unhandledExceptionMode is null or UnhandledExceptionMode.Automatic
                ? s_unhandledExceptionMode
                : t_unhandledExceptionMode.Value;
            return mode switch
            {
                UnhandledExceptionMode.ThrowException => false,
                UnhandledExceptionMode.CatchException => true,
                _ => !Debugger.IsAttached,
            };
        }
    }

    internal static void ReportNativeThreadException(Exception exception)
    {
        if (!CatchThreadException)
        {
            // UnmanagedCallersOnly callbacks cannot propagate a managed
            // exception through USER32. Preserve the terminal policy without
            // attempting an unsupported unwind across the native ABI.
            Environment.FailFast("An unhandled exception occurred in a native UI callback.", exception);
        }

        OnThreadException(exception);
    }

    /// <summary>
    /// Gets the mutable application-wide visual style settings used for new LuminaForms windows.
    /// </summary>
    public static ApplicationVisualStyleSettings VisualStyleSettings { get; } = new();

    /// <summary>
    /// Gets the current resolved visual style after OS detection and application overrides are applied.
    /// </summary>
    public static ResolvedVisualStyle CurrentVisualStyle => GetResolvedVisualStyle();

    /// <summary>
    /// Enables LuminaForms visual styling, activates themed common controls, and resolves the default backdrop/theme from the current operating system.
    /// </summary>
    public static void EnableVisualStyles()
    {
        EnsureCommonControlsInitialized();
        EnsureThemedCommonControlsActivated();
        s_visualStylesEnabled = true;
        NotifyVisualStylesChanged();
    }

    /// <summary>
    /// Matches the WinForms compatibility API. LuminaForms renders using native Win32 text, so this is currently a no-op.
    /// </summary>
    /// <param name="defaultValue">Ignored for compatibility with the WinForms API.</param>
    public static void SetCompatibleTextRenderingDefault(bool defaultValue)
    {
        s_compatibleTextRenderingDefault = defaultValue;
    }

    /// <summary>
    /// Gets the DPI awareness mode currently applied to the calling thread.
    /// </summary>
    public static HighDpiMode HighDpiMode => ApplicationHighDpi.GetCurrentMode();

    /// <summary>
    /// Sets the DPI awareness mode for the process before LuminaForms creates a native window.
    /// </summary>
    /// <param name="highDpiMode">The requested process DPI awareness mode.</param>
    /// <returns>
    /// <see langword="true"/> if the operating system accepted the setting; otherwise,
    /// <see langword="false"/>, including when a manifest or host already set process DPI awareness.
    /// </returns>
    /// <exception cref="InvalidEnumArgumentException">The requested mode is not defined.</exception>
    public static bool SetHighDpiMode(HighDpiMode highDpiMode)
    {
        if ((uint)highDpiMode > (uint)HighDpiMode.DpiUnawareGdiScaled)
        {
            throw new InvalidEnumArgumentException(nameof(highDpiMode), (int)highDpiMode, typeof(HighDpiMode));
        }

        lock (s_syncRoot)
        {
            bool success = !s_nativeWindowCreated && ApplicationHighDpi.SetProcessMode(highDpiMode);
            if (success) { Win32.RefreshInitialSystemDpi(); }
            return success;
        }
    }

    /// <summary>
    /// Requests that the current UI thread exit its LuminaForms message loop.
    /// </summary>
    public static void ExitThread()
    {
        if (t_applicationContext is { } context)
        {
            context.ExitThread();
            return;
        }

        if (t_messageLoopDepth > 0)
        {
            if (!t_exitRequested)
            {
                t_exitRequested = true;
                Win32.PostQuitMessage(0);
            }
            return;
        }

        // Without an application-owned loop there is no thread pump to quit.
        // In particular, do not leave a stale WM_QUIT for a later Run call.
        ApplicationOleState.ReleaseCurrentThread();
    }

    internal static void RequestExitThread()
    {
        t_threadLoop?.RequestExit();
    }

    /// <summary>
    /// Requests that all currently open LuminaForms windows close and the application exit.
    /// </summary>
    public static void Exit()
    {
        Exit(null);
    }

    /// <summary>Closes open forms unless a handler cancels application exit.</summary>
    public static void Exit(CancelEventArgs? e)
    {
        lock (s_syncRoot)
        {
            if (s_exiting)
            {
                if (e is not null) { e.Cancel = false; }
                return;
            }
            s_exiting = true;
        }

        try
        {
            if (!PrepareApplicationExit(e, out Form[] openForms)) { return; }

            if (e is not null) { e.Cancel = false; }
            foreach (Form form in openForms)
            {
                if (!form.IsDisposed)
                {
                    if (form.InvokeRequired)
                    {
                        form.Invoke(new Action(form.CompleteApplicationExit));
                    }
                    else
                    {
                        form.CompleteApplicationExit();
                    }
                }
            }

            ThreadLoopContext[] loops;
            lock (s_syncRoot) { loops = [.. s_threadLoops.Values]; }
            foreach (ThreadLoopContext loop in loops) { loop.Context.ExitThread(); }
        }
        finally
        {
            lock (s_syncRoot) { s_exiting = false; }
        }
    }

    private static bool OnFormThread(Form form, Func<bool> callback)
        => form.InvokeRequired ? (bool)form.Invoke(callback)! : callback();

    private static bool PrepareApplicationExit(CancelEventArgs? e, out Form[] openForms)
    {
        const int maximumPasses = 128;
        const int maximumForms = 4096;
        TimeSpan timeout = TimeSpan.FromSeconds(30);
        long started = Stopwatch.GetTimestamp();
        HashSet<Form> prepared = [];
        openForms = [];

        for (int pass = 0; pass < maximumPasses; pass++)
        {
            int version;
            Form[] snapshot;
            lock (s_syncRoot)
            {
                version = s_openFormsCollection.AddVersion;
                snapshot = [.. s_openForms];
            }

            // Match the reference's reverse registration order. User handlers
            // may open or close forms, so do not hold the collection lock while
            // they run, including when dispatching to another UI thread.
            for (int index = snapshot.Length - 1; index >= 0; index--)
            {
                Form form = snapshot[index];
                if (form.IsDisposed || !prepared.Add(form)) { continue; }
                if (prepared.Count > maximumForms || Stopwatch.GetElapsedTime(started) >= timeout)
                {
                    if (e is not null) { e.Cancel = true; }
                    throw new InvalidOperationException("Application exit could not finish its form-closing queries within the bounded form/time limit.");
                }

                if (!OnFormThread(form, form.PrepareApplicationExit))
                {
                    if (e is not null) { e.Cancel = true; }
                    return false;
                }
                if (Stopwatch.GetElapsedTime(started) >= timeout)
                {
                    if (e is not null) { e.Cancel = true; }
                    throw new InvalidOperationException("Application exit exceeded its form-closing query time limit.");
                }
            }

            lock (s_syncRoot)
            {
                if (version == s_openFormsCollection.AddVersion)
                {
                    // No new registration occurred during this pass. Every
                    // remaining form has agreed before any framework close.
                    openForms = [.. s_openForms];
                    return true;
                }
            }

            if (Stopwatch.GetElapsedTime(started) >= timeout) { break; }
        }

        if (e is not null) { e.Cancel = true; }
        throw new InvalidOperationException("Application exit did not reach a stable form collection within the bounded query passes/time limit.");
    }

    /// <summary>
    /// Starts the application message loop with the provided main form.
    /// </summary>
    /// <param name="mainForm">The main form to show and use as the message loop owner.</param>
    public static void Run(Form mainForm)
    {
        Run(new ApplicationContext(mainForm));
    }

    /// <summary>Starts a message loop without a main form.</summary>
    public static void Run() => Run(new ApplicationContext());

    /// <summary>Starts a message loop owned by the supplied application context.</summary>
    public static void Run(ApplicationContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _ = RunMessageLoopCore(context);
    }

    /// <summary>
    /// Runs the shared Win32 message loop for the current thread.
    /// </summary>
    /// <returns>The exit code returned by the Win32 message loop.</returns>
    public static int RunMessageLoop()
        => RunMessageLoopCore(new ApplicationContext());

    private static int RunMessageLoopCore(ApplicationContext context)
    {
        if (t_messageLoopDepth != 0)
        {
            throw new InvalidOperationException("A LuminaForms message loop cannot be nested on the same thread.");
        }

        t_applicationContext = context;
        t_exitRequested = false;
        t_pendingQuitExitCode = null;
        t_messageLoopDepth++;
        ThreadLoopContext loop = new(context, Win32.GetCurrentThreadId());
        t_threadLoop = loop;
        EventHandler exitHandler = (_, _) => loop.RequestExit();
        context.ThreadExit += exitHandler;
        try
        {
            // PeekMessage creates the native queue before other threads can
            // request an exit through the registered context.
            _ = Win32.PeekMessage(out _, 0, 0, 0, 0);
            lock (s_syncRoot) { s_threadLoops.Add(loop.ThreadId, loop); }
            WindowsFormsSynchronizationContext.InstallIfNeeded();
            EnsureVisualStylesInitialized();
            context.MainForm?.Show();
            int exitCode = 0;
            for (int result = 1; result > 0;)
            {
                if (t_pendingQuitExitCode is int pendingExitCode)
                {
                    exitCode = pendingExitCode;
                    break;
                }
                if (!t_exitRequested && !Win32.PeekMessage(out _, 0, 0, 0, 0))
                {
                    RaiseIdle(EventArgs.Empty);
                }

                if (t_pendingQuitExitCode is int idleExitCode)
                {
                    exitCode = idleExitCode;
                    break;
                }

                result = Win32.GetMessage(out Win32.MSG message, 0, 0, 0);
                if (result == -1)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                if (result > 0)
                {
                    DispatchMessage(ref message);
                }
                else
                {
                    loop.MarkQuitReceived();
                    exitCode = unchecked((int)message.wParam);
                }
            }

            return exitCode;
        }
        finally
        {
            context.ThreadExit -= exitHandler;
            bool lastThread;
            lock (s_syncRoot)
            {
                s_threadLoops.Remove(loop.ThreadId);
                lastThread = s_threadLoops.Count == 0;
            }
            if (--t_messageLoopDepth == 0)
            {
                List<Exception>? cleanupFailures = null;
                void Cleanup(Action action)
                {
                    try { action(); }
                    catch (Exception exception) { (cleanupFailures ??= []).Add(exception); }
                }

                Cleanup(context.Dispose);
                foreach (Form form in GetOpenFormsSnapshot())
                {
                    if (form.NativeHandle != 0
                        && Win32.GetWindowThreadProcessId(form.NativeHandle, out _) == loop.ThreadId)
                    {
                        Cleanup(form.Dispose);
                    }
                }

                Cleanup(() => WindowsFormsSynchronizationContext.Uninstall(turnOffAutoInstall: false));
                Cleanup(WindowsFormsSynchronizationContext.ReleaseCurrentThread);
                // Release the activation cookie only after leaving all HWND
                // callback frames, on the thread that originally activated it.
                Cleanup(DisposeThemedCommonControlsActivation);
                Cleanup(ApplicationOleState.ReleaseCurrentThread);
                t_applicationContext = null;
                t_threadLoop = null;
                t_exitRequested = false;
                t_pendingQuitExitCode = null;
                Cleanup(() => ThreadExit?.Invoke(null, EventArgs.Empty));
                if (lastThread) { Cleanup(() => ApplicationExit?.Invoke(null, EventArgs.Empty)); }
                if (cleanupFailures is { Count: 1 }) { ExceptionDispatchInfo.Capture(cleanupFailures[0]).Throw(); }
                if (cleanupFailures is { Count: > 1 })
                {
                    throw new AggregateException("UI thread cleanup reported multiple failures.", cleanupFailures);
                }
            }
        }
    }

    private static void DispatchMessage(ref Win32.MSG nativeMessage)
    {
        Message managedMessage = Message.Create(nativeMessage.hwnd, unchecked((int)nativeMessage.message),
            unchecked((nint)nativeMessage.wParam), nativeMessage.lParam);
        if (FilterMessage(ref managedMessage))
        {
            return;
        }

        nativeMessage.hwnd = managedMessage.HWnd;
        nativeMessage.message = unchecked((uint)managedMessage.Msg);
        nativeMessage.wParam = unchecked((nuint)managedMessage.WParam);
        nativeMessage.lParam = managedMessage.LParam;
        if (PreProcessKeyboardMessage(ref nativeMessage)) { return; }
        _ = Win32.TranslateMessage(in nativeMessage);
        _ = Win32.DispatchMessage(in nativeMessage);
    }

    /// <summary>Adds a message filter for the current UI thread.</summary>
    public static void AddMessageFilter(IMessageFilter? value)
    {
        if (value is null) { return; }
        (t_messageFilters ??= []).Add(value);
        t_filtersChanged = true;
    }

    /// <summary>Removes a message filter from the current UI thread.</summary>
    public static void RemoveMessageFilter(IMessageFilter? value)
    {
        if (value is null) { return; }
        t_messageFilters?.Remove(value);
        t_filtersChanged = true;
    }

    /// <summary>Passes a message through the current thread's filters.</summary>
    public static bool FilterMessage(ref Message message)
    {
        if (t_filterDepth == 0 && (t_filtersChanged || t_messageFilterSnapshot is null))
        {
            t_messageFilterSnapshot = t_messageFilters is { Count: > 0 } ? [.. t_messageFilters] : [];
            t_filtersChanged = false;
        }

        // Public IMessageFilter receives a working copy. In the reference only
        // its internal IMessageModifyAndFilter can edit the dispatched MSG.
        Message filteredMessage = message;
        t_filterDepth++;
        try
        {
            foreach (IMessageFilter filter in t_messageFilterSnapshot ?? [])
            {
                if (filter.PreFilterMessage(ref filteredMessage)) { return true; }
            }

            return false;
        }
        finally { t_filterDepth--; }
    }

    /// <summary>Dispatches all currently queued messages on the current thread.</summary>
    public static void DoEvents()
    {
        const int maximumMessages = 4096;
        if (t_pendingQuitExitCode.HasValue) { return; }
        if (!OperatingSystem.IsWindows())
        {
            return;
        }

        int processed = 0;
        while (processed++ < maximumMessages
            && Win32.PeekMessage(out Win32.MSG message, 0, 0, 0, 1 /* PM_REMOVE */))
        {
            if (message.message == 0x0012)
            {
                if (t_messageLoopDepth > 0)
                {
                    t_threadLoop?.MarkQuitReceived();
                    t_pendingQuitExitCode = unchecked((int)message.wParam);
                    t_exitRequested = true;
                }
                else
                {
                    Win32.PostQuitMessage(unchecked((int)message.wParam));
                }
                break;
            }

            DispatchMessage(ref message);
        }

        if (processed == 1)
        {
            RaiseIdle(EventArgs.Empty);
        }
    }

    /// <summary>Raises the current thread's Idle event.</summary>
    public static void RaiseIdle(EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e);
        t_idle?.Invoke(Thread.CurrentThread, e);
    }

    /// <summary>Registers a callback that reports whether an external host is pumping messages.</summary>
    public static void RegisterMessageLoop(MessageLoopCallback? callback)
    {
        t_messageLoopCallback = callback;
    }

    /// <summary>Removes the current thread's message-loop callback.</summary>
    public static void UnregisterMessageLoop() => t_messageLoopCallback = null;

    /// <summary>Raises the modal-enter notification for the current thread.</summary>
    internal static void NotifyEnterThreadModal()
    {
        t_modalDepth++;
        if (t_modalDepth == 1) { t_enterThreadModal?.Invoke(Thread.CurrentThread, EventArgs.Empty); }
    }

    /// <summary>Raises the modal-leave notification for the current thread.</summary>
    internal static void NotifyLeaveThreadModal()
    {
        if (t_modalDepth == 0) { return; }
        t_modalDepth--;
        if (t_modalDepth == 0) { t_leaveThreadModal?.Invoke(Thread.CurrentThread, EventArgs.Empty); }
    }

    internal static List<nint> DisableThreadWindowsForModal(Form dialog, nint owner)
    {
        List<nint> disabled = [];
        uint threadId = Win32.GetCurrentThreadId();
        foreach (Form form in GetOpenFormsSnapshot())
        {
            nint window = form.NativeHandle;
            if (window != 0 && !ReferenceEquals(form, dialog)
                && Win32.GetWindowThreadProcessId(window, out _) == threadId
                && Win32.IsWindowEnabled(window))
            {
                _ = Win32.EnableWindow(window, false);
                disabled.Add(window);
            }
        }

        if (owner != 0 && owner != dialog.NativeHandle && !disabled.Contains(owner)
            && Win32.IsWindowEnabled(owner))
        {
            _ = Win32.EnableWindow(owner, false);
            disabled.Add(owner);
        }

        return disabled;
    }

    internal static void EnableThreadWindowsAfterModal(List<nint> windows)
    {
        foreach (nint window in windows)
        {
            if (Win32.IsWindow(window)) { _ = Win32.EnableWindow(window, true); }
        }
    }

    internal static void RunDialog(Form dialog)
    {
        bool outerLoop = t_messageLoopDepth > 0;
        if (!outerLoop)
        {
            t_exitRequested = false;
            t_pendingQuitExitCode = null;
        }
        t_messageLoopDepth++;
        try
        {
            WindowsFormsSynchronizationContext.InstallIfNeeded();
            EnsureVisualStylesInitialized();
            NotifyEnterThreadModal();
            if (dialog.DialogResult == DialogResult.None) { dialog.Show(); }
            for (bool running = !dialog.CheckCloseDialog(); running; running = !dialog.CheckCloseDialog())
            {
                if (t_pendingQuitExitCode.HasValue) { break; }
                if (!Win32.PeekMessage(out _, 0, 0, 0, 0))
                {
                    RaiseIdle(EventArgs.Empty);
                    if (t_pendingQuitExitCode.HasValue || dialog.CheckCloseDialog()) { break; }
                }

                int result = Win32.GetMessage(out Win32.MSG message, 0, 0, 0);
                if (result == -1) { throw new Win32Exception(Marshal.GetLastWin32Error()); }
                if (result == 0)
                {
                    // Forward the consumed quit through the managed pump stack.
                    // Reposting into USER32 here can leave WM_QUIT behind when
                    // a closing dialog or LeaveThreadModal callback pumps again.
                    t_exitRequested = true;
                    t_threadLoop?.MarkQuitReceived();
                    if (outerLoop) { t_pendingQuitExitCode = unchecked((int)message.wParam); }
                    break;
                }

                DispatchMessage(ref message);
            }
        }
        finally
        {
            try { NotifyLeaveThreadModal(); }
            finally
            {
                if (--t_messageLoopDepth == 0)
                {
                    try
                    {
                        try { WindowsFormsSynchronizationContext.Uninstall(turnOffAutoInstall: false); }
                        finally { WindowsFormsSynchronizationContext.ReleaseCurrentThread(); }
                    }
                    finally
                    {
                        try { DisposeThemedCommonControlsActivation(); }
                        finally
                        {
                            try { ApplicationOleState.ReleaseCurrentThread(); }
                            finally
                            {
                                t_exitRequested = false;
                                t_pendingQuitExitCode = null;
                            }
                        }
                    }
                }
            }
        }
    }

    private sealed class ThreadLoopContext(ApplicationContext context, uint threadId)
    {
        private readonly Lock _gate = new();
        private bool _exitRequested;
        internal uint ThreadId { get; } = threadId;
        internal ApplicationContext Context { get; } = context;

        internal void MarkQuitReceived()
        {
            lock (_gate) { _exitRequested = true; }
        }

        internal void RequestExit()
        {
            lock (_gate)
            {
                if (_exitRequested) { return; }
                if (ThreadId == Win32.GetCurrentThreadId())
                {
                    if (t_exitRequested || t_pendingQuitExitCode.HasValue)
                    {
                        _exitRequested = true;
                        return;
                    }
                    t_exitRequested = true;
                    Win32.PostQuitMessage(0);
                }
                else if (!Win32.PostThreadMessageW(ThreadId, 0x0012 /* WM_QUIT */, 0, 0))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                _exitRequested = true;
            }
        }
    }

    private static bool PreProcessKeyboardMessage(ref Win32.MSG nativeMessage)
    {
        // Keep sent messages on their ordinary WndProc path. Only keyboard
        // messages retrieved by this thread's queue participate in preprocessing.
        const uint WmKeyLast = 0x0109;
        if (nativeMessage.message < Win32.WM_KEYDOWN || nativeMessage.message > WmKeyLast
            || Control.FromChildHandle(nativeMessage.hwnd) is not { } control)
        {
            return false;
        }

        Message message = Message.Create(nativeMessage.hwnd, unchecked((int)nativeMessage.message),
            unchecked((nint)nativeMessage.wParam), nativeMessage.lParam);
        bool handled;
        try
        {
            handled = control.PreProcessMessage(ref message);
        }
        catch (Exception exception)
        {
            if (!CatchThreadException) { ExceptionDispatchInfo.Capture(exception).Throw(); }
            OnThreadException(exception);
            return true;
        }

        // A virtual preprocessor may replace the destination or message as well
        // as its parameters. Preserve native timing/position for the same queue
        // entry while passing the edited message to Translate/Dispatch.
        nativeMessage.hwnd = message.HWnd;
        nativeMessage.message = unchecked((uint)message.Msg);
        nativeMessage.wParam = unchecked((nuint)message.WParam);
        nativeMessage.lParam = message.LParam;
        return handled;
    }

    /// <summary>
    /// Applies a batch of visual style changes to the application defaults.
    /// </summary>
    /// <param name="configure">A delegate that updates <see cref="VisualStyleSettings"/>.</param>
    public static void ConfigureVisualStyles(Action<ApplicationVisualStyleSettings> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);
        configure(VisualStyleSettings);
        NotifyVisualStylesChanged();
    }

    /// <summary>
    /// Applies a file-driven theme to future LuminaForms windows.
    /// </summary>
    /// <param name="theme">The theme to activate.</param>
    public static void UseTheme(NativeTheme theme)
    {
        ArgumentNullException.ThrowIfNull(theme);
        VisualStyleSettings.Theme = theme;
        NotifyVisualStylesChanged();
    }

    /// <summary>
    /// Loads a JSON theme file and applies it to future LuminaForms windows.
    /// </summary>
    /// <param name="path">The JSON theme file path.</param>
    public static void LoadTheme(string path)
    {
        UseTheme(NativeTheme.LoadJson(path));
    }

    /// <summary>
    /// Clears the currently active file-driven theme.
    /// </summary>
    public static void ResetTheme()
    {
        VisualStyleSettings.Theme = null;
        NotifyVisualStylesChanged();
    }

    internal static void EnsureVisualStylesInitialized()
    {
        if (!s_visualStylesEnabled)
        {
            EnableVisualStyles();
        }
        else
        {
            EnsureThemedCommonControlsActivated();
        }
    }

    internal static ResolvedVisualStyle GetResolvedVisualStyle()
    {
        return ResolveVisualStyle(
            VisualStyleSettings.ThemeMode,
            VisualStyleSettings.PreferredVisualStyle,
            VisualStyleSettings.ApplyBackdropEffects,
            VisualStyleSettings.PreferredEffect,
            VisualStyleSettings.PreferredEffectOptions,
            VisualStyleSettings.Theme,
            VisualStyleSettings.Palette);
    }

    internal static ThemeMode ResolveThemeMode(ThemeMode requestedThemeMode, NativeTheme? theme = null)
    {
        return requestedThemeMode switch
        {
            ThemeMode.Light => ThemeMode.Light,
            ThemeMode.Dark => ThemeMode.Dark,
            _ => theme?.ThemeMode switch
            {
                ThemeMode.Light => ThemeMode.Light,
                ThemeMode.Dark => ThemeMode.Dark,
                _ => DetectSystemThemeMode(),
            },
        };
    }

    internal static VisualStyleKind ResolveVisualStyleKind(VisualStyleKind requestedVisualStyleKind, NativeTheme? theme = null)
    {
        if (requestedVisualStyleKind != VisualStyleKind.System)
        {
            return requestedVisualStyleKind;
        }

        if (theme?.PreferredVisualStyle is { } themeVisualStyle && themeVisualStyle != VisualStyleKind.System)
        {
            return themeVisualStyle;
        }

        return DetectSystemVisualStyleKind();
    }

    internal static ResolvedVisualStyle ResolveVisualStyle(
        ThemeMode requestedThemeMode,
        VisualStyleKind preferredVisualStyle,
        bool applyBackdropEffects,
        EffectKind? preferredEffect,
        EffectOptions? preferredEffectOptions,
        NativeTheme? theme,
        ThemePalette? palette)
    {
        ThemeMode themeMode = ResolveThemeMode(requestedThemeMode, theme);
        VisualStyleKind visualStyleKind = ResolveVisualStyleKind(preferredVisualStyle, theme);
        EffectKind effectKind = ResolveEffectKind(applyBackdropEffects, preferredEffect, theme, themeMode, visualStyleKind);
        EffectOptions? effectOptions = ResolveEffectOptions(preferredEffectOptions, theme, effectKind, themeMode);
        ThemePalette resolvedPalette = ResolvePalette(palette, theme, themeMode, visualStyleKind);
        return new ResolvedVisualStyle(themeMode, visualStyleKind, effectKind, effectOptions, theme, resolvedPalette);
    }

    private static ThemeMode DetectSystemThemeMode()
    {
        if (!OperatingSystem.IsWindows())
        {
            return ThemeMode.Light;
        }

        try
        {
            object? value = Registry.GetValue($@"HKEY_CURRENT_USER\{PersonalizeRegistryKey}", "AppsUseLightTheme", 1);
            if (value is int intValue)
            {
                return intValue == 0 ? ThemeMode.Dark : ThemeMode.Light;
            }
        }
        catch
        {
        }

        return ThemeMode.Light;
    }

    private static VisualStyleKind DetectSystemVisualStyleKind()
    {
        if (!OperatingSystem.IsWindows())
        {
            return VisualStyleKind.Classic;
        }

        if (OperatingSystem.IsWindowsVersionAtLeast(10, 0, 22000))
        {
            return VisualStyleKind.Mica;
        }

        if (OperatingSystem.IsWindowsVersionAtLeast(10, 0))
        {
            return VisualStyleKind.Fluent;
        }

        if (OperatingSystem.IsWindowsVersionAtLeast(6, 2))
        {
            return VisualStyleKind.Modern;
        }

        if (OperatingSystem.IsWindowsVersionAtLeast(6, 1))
        {
            return VisualStyleKind.AeroGlass;
        }

        return VisualStyleKind.Classic;
    }

    private static void EnsureCommonControlsInitialized()
    {
        lock (s_syncRoot)
        {
            if (s_commonControlsInitialized)
            {
                return;
            }

            var initCommonControls = new Win32.INITCOMMONCONTROLSEX
            {
                dwSize = (uint)Marshal.SizeOf<Win32.INITCOMMONCONTROLSEX>(),
                dwICC = Win32.ICC_WIN95_CLASSES | Win32.ICC_DATE_CLASSES | Win32.ICC_STANDARD_CLASSES | Win32.ICC_USEREX_CLASSES,
            };

            _ = Win32.InitCommonControlsEx(ref initCommonControls);
            s_commonControlsInitialized = true;
        }
    }

    private static void EnsureThemedCommonControlsActivated()
    {
        if (!OperatingSystem.IsWindows())
        {
            return;
        }

        lock (s_syncRoot)
        {
            if (t_visualStylesActivationCookie != 0)
            {
                return;
            }

            try
            {
                EnsureVisualStylesManifestFile();

                var activationContext = new Win32.ACTCTXW
                {
                    cbSize = (uint)Marshal.SizeOf<Win32.ACTCTXW>(),
                    lpSource = s_visualStylesManifestPath,
                };

                nint handle = Win32.CreateActCtxW(ref activationContext);
                if (handle == new nint(-1) || handle == 0)
                {
                    return;
                }

                if (!Win32.ActivateActCtx(handle, out nuint activationCookie))
                {
                    Win32.ReleaseActCtx(handle);
                    return;
                }

                t_visualStylesActivationContext = handle;
                t_visualStylesActivationCookie = activationCookie;
            }
            catch
            {
            }
        }
    }

    private static void EnsureVisualStylesManifestFile()
    {
        string? directory = Path.GetDirectoryName(s_visualStylesManifestPath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        byte[] manifestBytes = Encoding.UTF8.GetBytes(s_visualStylesManifestContents);
        File.WriteAllBytes(s_visualStylesManifestPath, manifestBytes);
    }

    private static void DisposeThemedCommonControlsActivation()
    {
        lock (s_syncRoot)
        {
            if (t_visualStylesActivationCookie != 0)
            {
                if (!Win32.DeactivateActCtx(0, t_visualStylesActivationCookie))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                t_visualStylesActivationCookie = 0;
            }

            if (t_visualStylesActivationContext != 0 && t_visualStylesActivationContext != new nint(-1))
            {
                Win32.ReleaseActCtx(t_visualStylesActivationContext);
                t_visualStylesActivationContext = 0;
            }
        }
    }

    private static EffectKind ResolveEffectKind(
        bool applyBackdropEffects,
        EffectKind? preferredEffect,
        NativeTheme? theme,
        ThemeMode themeMode,
        VisualStyleKind visualStyleKind)
    {
        if (!applyBackdropEffects)
        {
            return EffectKind.None;
        }

        if (preferredEffect is { } preferred)
        {
            return preferred;
        }

        if (theme?.PreferredEffect is { } themedEffect)
        {
            return themedEffect;
        }

        return visualStyleKind switch
        {
            VisualStyleKind.Mica => themeMode == ThemeMode.Dark
                ? EffectKind.Mica
                : EffectKind.MicaAlt,
            VisualStyleKind.Fluent => EffectKind.Blur,
            VisualStyleKind.AeroGlass => EffectKind.Aero,
            _ => EffectKind.None,
        };
    }

    private static EffectOptions? ResolveEffectOptions(
        EffectOptions? preferredEffectOptions,
        NativeTheme? theme,
        EffectKind effectKind,
        ThemeMode themeMode)
    {
        if (preferredEffectOptions is not null)
        {
            return preferredEffectOptions;
        }

        if (theme?.PreferredEffectOptions is not null)
        {
            return theme.PreferredEffectOptions;
        }

        return effectKind switch
        {
            EffectKind.Blur => new EffectOptions { BlurRadius = 20 },
            EffectKind.Acrylic => new EffectOptions
            {
                BlendColor = themeMode == ThemeMode.Dark ? 0xCC_20_20_20 : 0xCC_F2_F2_F2,
                BlurRadius = 18,
                Opacity = 0.9f,
            },
            _ => null,
        };
    }

    private static ThemePalette ResolvePalette(
        ThemePalette? palette,
        NativeTheme? theme,
        ThemeMode themeMode,
        VisualStyleKind visualStyleKind)
    {
        if (palette is not null)
        {
            return palette;
        }

        if (theme?.Palette is not null)
        {
            return theme.Palette;
        }

        return ThemePalette.CreateSystem(themeMode, visualStyleKind);
    }

    internal static void NotifyWindowCreated()
    {
        lock (s_syncRoot)
        {
            s_nativeWindowCreated = true;
            t_nativeWindowCreated = true;
        }
    }

    internal static void RegisterOpenForm(Form form)
    {
        ArgumentNullException.ThrowIfNull(form);

        lock (s_syncRoot)
        {
            s_nativeWindowCreated = true;
            t_nativeWindowCreated = true;
            if (!s_openForms.Contains(form))
            {
                s_openForms.Add(form);
                s_openFormsCollection.Add(form);
            }
        }
    }

    internal static bool UnregisterOpenForm(Form form)
    {
        ArgumentNullException.ThrowIfNull(form);

        lock (s_syncRoot)
        {
            _ = s_openForms.Remove(form);
            s_openFormsCollection.Remove(form);
            return s_openForms.Count == 0;
        }
    }

    internal static nint GetAnyOpenFormHandle()
    {
        lock (s_syncRoot)
        {
            foreach (Form form in s_openForms)
            {
                if (form.NativeHandle != 0)
                {
                    return form.NativeHandle;
                }
            }
        }

        return 0;
    }

    private static Form[] GetOpenFormsSnapshot()
    {
        lock (s_syncRoot)
        {
            return [.. s_openForms];
        }
    }

    private static void NotifyVisualStylesChanged()
    {
        foreach (Form form in GetOpenFormsSnapshot())
        {
            form.RefreshVisualStyles();
        }
    }
}
