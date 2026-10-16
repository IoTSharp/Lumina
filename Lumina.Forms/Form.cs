using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
namespace Lumina.Forms;

/// <summary>
/// Represents a top-level LuminaForms window backed by a native Win32 overlapped window.
/// </summary>
public class Form : ContainerControl
{
    [ThreadStatic]
    private static FormCreationContext? t_creatingForm;

    private readonly Dictionary<int, Control> _controlsById = [];
    private GCHandle _selfHandle;
    private nint _formDefaultWindowProc;
    private bool _shown;
    private bool _loaded;
    private bool _showing;
    private bool _restoringNativeVisibility;
    private bool _registeredOpenForm;
    private EventArgs? _sizeChangedArguments = EventArgs.Empty;
    private int _nextControlId = 1000;
    private bool _effectExplicitlySet;
    private bool _refreshingMainMenuStrip;
    private bool _suppressMainMenuRefresh;
    private bool _themeExplicitlySet;
    private EffectKind _pendingEffectKind = EffectKind.None;
    private EffectOptions? _pendingEffectOptions;
    private MenuStrip? _mainMenuStrip;
    private ThemeMode? _requestedThemeMode;
    private NativeTheme? _themeOverride;
    private ThemePalette? _paletteOverride;
    private nint _windowBackgroundBrush;
    private bool _ownsWindowBackgroundBrush;
    private nint _surfaceBackgroundBrush;
    private bool _ownsSurfaceBackgroundBrush;
    private nint _controlBackgroundBrush;
    private bool _ownsControlBackgroundBrush;
    private uint _windowForegroundColorRef;
    private uint _surfaceForegroundColorRef;
    private uint _controlForegroundColorRef;
    private uint _surfaceBackgroundColorRef;
    private uint _controlBackgroundColorRef;
    private Icon? _icon;
    private FormWindowState _windowState = FormWindowState.Normal;

    /// <summary>
    /// Initializes a new top-level LuminaForms window.
    /// </summary>
    public Form()
    {
        SetTopLevel(true);
        base.SetVisibleCore(false);
    }

    /// <summary>
    /// Occurs after the native form handle and attached child controls are created.
    /// </summary>
    public new event EventHandler? Created;

    /// <summary>
    /// Occurs before the form is first shown.
    /// </summary>
    public event EventHandler? Load;

    /// <summary>
    /// Occurs after the form becomes visible for the first time.
    /// </summary>
    public event EventHandler? Shown;

    /// <summary>
    /// Occurs when the form is closed.
    /// </summary>
    public event EventHandler? Closed;

    /// <summary>
    /// Occurs before the form closes.
    /// </summary>
    public event EventHandler<FormClosingEventArgs>? FormClosing;

    /// <summary>
    /// Gets or sets the window icon. When unset, Lumina.Forms falls back to its built-in logo icon.
    /// </summary>
    public Icon? Icon
    {
        get => _icon ?? DefaultAppIconProvider.GetIcon();
        set
        {
            if (value is null && _icon is null)
            {
                return;
            }

            if (value is not null
                && _icon is not null
                && ReferenceEquals(_icon, value))
            {
                return;
            }

            ReleaseOwnedIcon();
            _icon = value is null
                ? null
                : OperatingSystem.IsWindows()
                    ? CloneIcon(value)
                    : null;
            ApplyWindowIcon();
        }
    }

    public FormStartPosition StartPosition { get; set; } = FormStartPosition.Manual;

    public FormWindowState WindowState
    {
        get => _windowState;
        set
        {
            _windowState = value;
            if (NativeHandle != 0)
            {
                _ = Win32.ShowWindow(NativeHandle, value switch
                {
                    FormWindowState.Minimized => Win32.SW_MINIMIZE,
                    FormWindowState.Maximized => Win32.SW_MAXIMIZE,
                    _ => Win32.SW_RESTORE,
                });
            }
        }
    }

    /// <summary>
    /// Gets the Win32 window styles used when the form handle is created.
    /// </summary>
    protected virtual uint WindowStyle => Win32.WS_OVERLAPPEDWINDOW | Win32.WS_CLIPCHILDREN;

    /// <summary>
    /// Gets the Win32 extended window styles used when the form handle is created.
    /// </summary>
    protected virtual uint WindowExStyle => Win32.WS_EX_APPWINDOW | Win32.WS_EX_CONTROLPARENT;

    protected override Size DefaultSize => new(300, 300);

    protected override uint Style => WindowStyle;

    protected override uint ExStyle => WindowExStyle;

    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams parameters = base.CreateParams;
            if (StartPosition != FormStartPosition.Manual)
            {
                parameters.X = Win32.CW_USEDEFAULT;
                parameters.Y = Win32.CW_USEDEFAULT;
            }
            return parameters;
        }
    }

    internal WindowThreadDispatcher? ThreadDispatcher => NativeThreadDispatcher;

    internal nint InstanceHandle { get; private set; }


    /// <summary>
    /// Gets or sets the main menu strip associated with the form.
    /// </summary>
    public MenuStrip? MainMenuStrip
    {
        get => _mainMenuStrip;
        set
        {
            if (ReferenceEquals(_mainMenuStrip, value))
            {
                return;
            }

            MenuStrip? previous = _mainMenuStrip;
            _mainMenuStrip = value;
            RefreshMainMenuStrip(previous);
        }
    }

    /// <summary>
    /// Gets the visual style currently resolved for this form after application defaults and form-level overrides are applied.
    /// </summary>
    public new ResolvedVisualStyle CurrentVisualStyle => ResolveCurrentVisualStyle();

    /// <summary>
    /// Creates the native window, applies the current application defaults, and shows the form.
    /// </summary>
    public void Show()
    {
        ThrowIfDisposed();
        if (Visible && NativeHandle != 0)
        {
            return;
        }

        _showing = true;
        try
        {
            base.SetVisibleCore(true);
            PerformLayout();
            if (NativeHandle != 0)
            {
                _ = Win32.UpdateWindow(NativeHandle);
            }
        }
        finally
        {
            _showing = false;
        }
    }

    protected override void OnCreateControl()
    {
        base.OnCreateControl();
        if (_showing && !_loaded)
        {
            _loaded = true;
            OnCreated();
            OnLoad();
            if (!IsDisposed && NativeHandle != 0)
            {
                _ = BeginInvoke((Action)(() =>
                {
                    if (!_shown && !IsDisposed)
                    {
                        _shown = true;
                        OnShown();
                    }
                }));
            }
        }
    }

    protected override void CreateHandle()
    {
        if (!BeginNativeHandleCreation()) { return; }

        try
        {
            CreateFormHandle();
        }
        finally
        {
            EndNativeHandleCreation();
        }
    }

    private unsafe void CreateFormHandle()
    {
        PerformAutoScale();
        InstanceHandle = Win32.GetModuleHandleW(null);

        // Resolve virtual properties before acquiring the per-window strong
        // reference, so a failed getter can be retried without losing a GCHandle.
        CreateParams parameters = CreateParams;
        NativeWindowClassRegistration windowClass = NativeWindowClass.GetOrRegister(
            parameters.ClassName, parameters.ClassStyle,
            (nint)(delegate* unmanaged[Stdcall]<nint, uint, nint, nint, nint>)&WindowProcThunk,
            InstanceHandle, DefaultAppIconProvider.GetIconHandle());
        using NativeCreateParameter nativeParameter = NativeCreateParameter.Create(parameters.Param);
        string? caption = parameters.Caption;
        if (caption?.Length > short.MaxValue) { caption = caption[..short.MaxValue]; }

        _selfHandle = GCHandle.Alloc(this);
        _formDefaultWindowProc = windowClass.OriginalWindowProc;

        Application.NotifyWindowCreated();
        FormCreationContext? previousCreation = t_creatingForm;
        t_creatingForm = new(this);
        nint hwnd;
        int creationError;
        try
        {
            hwnd = Win32.CreateWindowExW(
                unchecked((uint)parameters.ExStyle), windowClass.Name, caption ?? string.Empty,
                unchecked((uint)parameters.Style) & ~Win32.WS_VISIBLE,
                parameters.X, parameters.Y, parameters.Width, parameters.Height,
                parameters.Parent, 0, InstanceHandle, nativeParameter.Pointer);
            creationError = Marshal.GetLastWin32Error();
        }
        catch
        {
            // A failed native call can throw before a window exists. Keep a
            // live window rooted, but release an unused creation reference.
            if (NativeHandle == 0)
            {
                if (_selfHandle.IsAllocated) { _selfHandle.Free(); }
                _formDefaultWindowProc = 0;
            }
            throw;
        }
        finally
        {
            t_creatingForm = previousCreation;
        }

        if (hwnd == 0)
        {
            if (NativeHandle != 0 && !Win32.IsWindow(NativeHandle)) { ClearNativeHandle(); }
            if (_selfHandle.IsAllocated) { _selfHandle.Free(); }
            _formDefaultWindowProc = 0;
            throw new InvalidOperationException($"Failed to create the native form window. Win32 error: {creationError}");
        }

        ApplyFontToHandle();
        UpdateBoundsFromNativeWindow();
        _ = Win32.EnableWindow(hwnd, Enabled);
        ApplyWindowIcon();
        Application.EnsureVisualStylesInitialized();
        ApplyApplicationDefaults();
        RefreshMainMenuStrip();

        if (StartPosition == FormStartPosition.CenterScreen)
        {
            CenterToScreen();
        }

        Application.RegisterOpenForm(this);
        _registeredOpenForm = true;
        NotifyNativeHandleCreated();
        if (Visible && NativeHandle != 0)
        {
            // Creating a replacement HWND restores the existing visible state.
            // It does not run CreateControl or announce a new visibility change.
            _restoringNativeVisibility = true;
            try { _ = Win32.ShowWindow(NativeHandle, Win32.SW_SHOW); }
            finally { _restoringNativeVisibility = false; }
        }
    }

    /// <summary>
    /// Closes the form if its native window has already been created.
    /// </summary>
    public void Close()
    {
        if (NativeHandle != 0)
        {
            _ = Win32.SendMessageW(NativeHandle, Win32.WM_CLOSE, 0, 0);
        }
        else
        {
            Dispose();
        }
    }

    public void Hide()
    {
        base.SetVisibleCore(false);
    }

    protected override void SetVisibleCore(bool value)
    {
        if (value)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    public void Activate()
    {
        if (NativeHandle != 0)
        {
            _ = Win32.SetForegroundWindow(NativeHandle);
        }
    }

    public void BringToFront()
    {
        if (NativeHandle != 0)
        {
            _ = Win32.SetForegroundWindow(NativeHandle);
        }
    }

    public void CenterToScreen()
    {
        if (!OperatingSystem.IsWindows())
        {
            return;
        }

        int screenWidth = Win32.GetSystemMetrics(0);
        int screenHeight = Win32.GetSystemMetrics(1);
        int left = Math.Max(0, (screenWidth - Width) / 2);
        int top = Math.Max(0, (screenHeight - Height) / 2);

        Location = new Point(left, top);
    }

    /// <summary>
    /// Applies a Lumina effect to the form and marks it as explicitly overridden from the application default.
    /// </summary>
    /// <param name="kind">The effect kind to apply.</param>
    /// <param name="options">Optional effect parameters.</param>
    public void SetEffect(EffectKind kind, EffectOptions? options = null)
    {
        _effectExplicitlySet = true;
        _pendingEffectKind = kind;
        _pendingEffectOptions = options;
        ApplyPendingEffect();
    }

    /// <summary>
    /// Overrides the form theme mode independently of the application default.
    /// </summary>
    /// <param name="themeMode">The theme mode to apply to the window.</param>
    public void SetThemeMode(ThemeMode themeMode)
    {
        _themeExplicitlySet = true;
        _requestedThemeMode = themeMode;
        ApplyPendingThemeMode();
    }

    /// <summary>
    /// Clears any form-level theme override and returns to the application default theme mode.
    /// </summary>
    public void ResetThemeMode()
    {
        _themeExplicitlySet = false;
        _requestedThemeMode = null;
        ApplyApplicationDefaults();
    }

    /// <summary>
    /// Applies a form-level theme override that can supply theme mode, visual style, effect defaults, and palette tokens.
    /// </summary>
    /// <param name="theme">The theme to use for this form.</param>
    public void UseTheme(NativeTheme theme)
    {
        ArgumentNullException.ThrowIfNull(theme);
        _themeOverride = theme;
        ApplyApplicationDefaults();
    }

    /// <summary>
    /// Clears the form-level theme override and returns to the application defaults.
    /// </summary>
    public void ResetTheme()
    {
        _themeOverride = null;
        ApplyApplicationDefaults();
    }

    /// <summary>
    /// Applies a form-level semantic palette override without requiring a full theme object.
    /// </summary>
    /// <param name="palette">The palette to use for this form.</param>
    public void SetPalette(ThemePalette palette)
    {
        ArgumentNullException.ThrowIfNull(palette);
        _paletteOverride = palette;
        ApplyApplicationDefaults();
    }

    /// <summary>
    /// Clears the form-level palette override and returns to the application default palette.
    /// </summary>
    public void ResetPalette()
    {
        _paletteOverride = null;
        ApplyApplicationDefaults();
    }

    /// <summary>
    /// Resets the form to the default system-following theme, including the current Windows accent color.
    /// </summary>
    public void UseSystemTheme()
    {
        ResetTheme();
        ResetPalette();
        ResetThemeMode();
    }

    /// <summary>
    /// Applies the built-in light theme while still honoring the current Windows accent color.
    /// </summary>
    public void UseLightTheme()
    {
        ResetTheme();
        ResetPalette();
        SetThemeMode(ThemeMode.Light);
    }

    /// <summary>
    /// Applies the built-in dark theme while still honoring the current Windows accent color.
    /// </summary>
    public void UseDarkTheme()
    {
        ResetTheme();
        ResetPalette();
        SetThemeMode(ThemeMode.Dark);
    }

    /// <summary>
    /// Reapplies the current theme mode using the detected Windows accent color.
    /// </summary>
    public void UseSystemColors()
    {
        ResetTheme();
        ThemeMode themeMode = _themeExplicitlySet ? _requestedThemeMode ?? ThemeMode.System : CurrentVisualStyle.ThemeMode;
        SetPalette(ThemePalette.CreateSystem(themeMode, CurrentVisualStyle.VisualStyleKind));
    }

    /// <summary>
    /// Applies a built-in custom accent palette while preserving the current light or dark mode.
    /// </summary>
    /// <param name="accent">The custom accent color in ARGB format.</param>
    public void UseCustomTheme(uint accent = 0xFF_8B_5C_F6)
    {
        ResetTheme();
        ThemeMode themeMode = _themeExplicitlySet ? _requestedThemeMode ?? ThemeMode.System : CurrentVisualStyle.ThemeMode;
        if (themeMode == ThemeMode.System)
        {
            themeMode = CurrentVisualStyle.ThemeMode;
        }

        SetPalette(ThemePalette.CreateCustom(themeMode, accent, CurrentVisualStyle.VisualStyleKind));
    }

    /// <summary>
    /// Applies the Mica backdrop to the form.
    /// </summary>
    public void SetMica() => SetEffect(EffectKind.Mica);

    /// <summary>
    /// Applies the Mica Alt backdrop to the form.
    /// </summary>
    public void SetMicaAlt() => SetEffect(EffectKind.MicaAlt);

    /// <summary>
    /// Applies the Acrylic backdrop to the form.
    /// </summary>
    /// <param name="blendColor">The ARGB blend color used by the Acrylic backdrop.</param>
    public void SetAcrylic(uint blendColor = 0x80_00_00_00)
        => SetEffect(EffectKind.Acrylic, new EffectOptions { BlendColor = blendColor });

    /// <summary>
    /// Applies the Aero-style backdrop to the form.
    /// </summary>
    public void SetAero() => SetEffect(EffectKind.Aero);

    /// <summary>
    /// Applies a blur effect to the form.
    /// </summary>
    /// <param name="radius">The blur radius to use.</param>
    public void SetBlur(int radius = 20)
        => SetEffect(EffectKind.Blur, new EffectOptions { BlurRadius = radius });

    /// <summary>
    /// Removes the currently applied Lumina effect from the form.
    /// </summary>
    public void ClearLuminaEffect()
    {
        _effectExplicitlySet = true;
        _pendingEffectKind = EffectKind.None;
        _pendingEffectOptions = null;
        if (NativeHandle != 0)
        {
            LuminaWindow.Clear(NativeHandle);
        }
    }

    /// <summary>
    /// Called after the native window and child controls have been created.
    /// </summary>
    protected virtual void OnCreated()
    {
        Created?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Called before the form is shown for the first time.
    /// </summary>
    protected virtual void OnLoad()
    {
        OnLoad(EventArgs.Empty);
    }

    /// <summary>
    /// Called after the form is shown for the first time.
    /// </summary>
    protected virtual void OnShown()
    {
        OnShown(EventArgs.Empty);
    }

    /// <summary>
    /// Raises the <see cref="Load"/> event.
    /// </summary>
    /// <param name="e">The event data.</param>
    protected virtual void OnLoad(EventArgs e)
    {
        Load?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the <see cref="Shown"/> event.
    /// </summary>
    /// <param name="e">The event data.</param>
    protected virtual void OnShown(EventArgs e)
    {
        Shown?.Invoke(this, e);
    }

    /// <inheritdoc />
    protected override void OnLayout(LayoutEventArgs e)
    {
        base.OnLayout(e);
    }

    /// <summary>
    /// Called after a close request has been accepted, before the form is disposed.
    /// </summary>
    protected virtual void OnClosed()
    {
        Closed?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Called before the form is closed.
    /// </summary>
    protected virtual void OnFormClosing(FormClosingEventArgs e)
    {
        FormClosing?.Invoke(this, e);
    }

    /// <summary>
    /// Called when the form size changes.
    /// </summary>
    protected virtual void OnSizeChanged()
    {
        base.OnSizeChanged(_sizeChangedArguments!);
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        // Keep the historical parameterless hook while forwarding the standard
        // argument, including reentrant calls, to the inherited notification.
        EventArgs? previous = _sizeChangedArguments;
        _sizeChangedArguments = e;
        try
        {
            OnSizeChanged();
        }
        finally
        {
            _sizeChangedArguments = previous;
        }
    }

    /// <summary>
    /// Raises the <see cref="BackColorChanged"/> event.
    /// </summary>
    /// <param name="e">The event arguments.</param>
    protected override void OnBackColorChanged(EventArgs e)
    {
        base.OnBackColorChanged(e);
        ApplyApplicationDefaults();
    }

    /// <summary>
    /// Raises the <see cref="ForeColorChanged"/> event.
    /// </summary>
    /// <param name="e">The event arguments.</param>
    protected override void OnForeColorChanged(EventArgs e)
    {
        base.OnForeColorChanged(e);
        ApplyApplicationDefaults();
    }

    /// <summary>
    /// Called after a child control command is dispatched.
    /// </summary>
    /// <param name="controlId">The numeric child control identifier.</param>
    /// <param name="notificationCode">The Win32 notification code.</param>
    /// <param name="sourceHandle">The originating child window handle.</param>
    protected virtual void OnCommand(int controlId, int notificationCode, nint sourceHandle)
    {
    }

    /// <summary>
    /// Tries to read the current client size from the native window.
    /// </summary>
    /// <param name="width">Receives the client width.</param>
    /// <param name="height">Receives the client height.</param>
    /// <returns><see langword="true"/> if the size was retrieved; otherwise, <see langword="false"/>.</returns>
    protected bool TryGetClientSize(out int width, out int height)
    {
        if (NativeHandle != 0 && Win32.GetClientRect(NativeHandle, out var rect))
        {
            width = rect.Width;
            height = rect.Height;
            return true;
        }

        width = 0;
        height = 0;
        return false;
    }

    /// <summary>
    /// Releases the form's native resources and the standard component/control ownership tree.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        try
        {
            base.Dispose(disposing);
        }
        finally
        {
            // A failed or reentrant disposal may leave the HWND alive. Its
            // callback and GDI resources must live at least as long as it does.
            if (NativeHandle == 0 && !IsCreatingHandle)
            {
                ReleaseMainMenuStrip();
                ReleasePaletteResources();
                ReleaseOwnedIcon();
                if (_selfHandle.IsAllocated)
                {
                    _selfHandle.Free();
                }

                UnregisterOpenForm();
            }
        }
    }

    private void UnregisterOpenForm()
    {
        if (!_registeredOpenForm) { return; }
        _registeredOpenForm = false;
        if (Application.UnregisterOpenForm(this))
        {
            Win32.PostQuitMessage(0);
        }
    }

    internal void AttachControl(Control control, Control? parent)
    {
        ArgumentNullException.ThrowIfNull(control);

        if (ReferenceEquals(control.Owner, this))
        {
            return;
        }

        if (control.Owner is not null)
        {
            throw new InvalidOperationException("The control already belongs to a form.");
        }

        control.Attach(this, ++_nextControlId, parent);
        _controlsById[control.Id] = control;

        foreach (Control child in control.ChildControls)
        {
            AttachControl(child, control);
        }
    }

    internal bool DetachControl(Control control)
    {
        ArgumentNullException.ThrowIfNull(control);

        foreach (Control child in control.ChildControls)
        {
            DetachControl(child);
        }

        bool removed = false;
        if (control.Id != 0)
        {
            removed = _controlsById.Remove(control.Id) || removed;
        }

        control.DetachFromOwner();
        return removed;
    }

    internal void RefreshMainMenuStrip()
    {
        RefreshMainMenuStrip(previous: null);
    }

    private void RefreshMainMenuStrip(MenuStrip? previous)
    {
        if (_refreshingMainMenuStrip || _suppressMainMenuRefresh)
        {
            return;
        }

        _refreshingMainMenuStrip = true;
        if (!OperatingSystem.IsWindows() || NativeHandle == 0)
        {
            try
            {
                previous?.ReleaseNativeMenu();
            }
            finally
            {
                _refreshingMainMenuStrip = false;
            }

            return;
        }

        try
        {
            _ = Win32.SetMenu(NativeHandle, 0);
            previous?.ReleaseNativeMenu();

            if (_mainMenuStrip is not null)
            {
                _mainMenuStrip.SynchronizeNativeMenu();
                _ = Win32.SetMenu(NativeHandle, _mainMenuStrip.GetNativeMenuHandle());
            }

            _ = Win32.DrawMenuBar(NativeHandle);
        }
        finally
        {
            _refreshingMainMenuStrip = false;
        }
    }

    private bool UsesNativeMainMenuBar()
        => OperatingSystem.IsWindows()
            && NativeHandle != 0
            && _mainMenuStrip is not null
            && _mainMenuStrip.UsesNativeMenuBar;

    private void ReleaseMainMenuStrip()
    {
        if (NativeHandle != 0 && OperatingSystem.IsWindows())
        {
            _ = Win32.SetMenu(NativeHandle, 0);
            _ = Win32.DrawMenuBar(NativeHandle);
        }

        _mainMenuStrip?.ReleaseNativeMenu();
    }

    private void ApplyApplicationDefaults()
    {
        ResolvedVisualStyle visualStyle = ResolveCurrentVisualStyle();

        if (!_effectExplicitlySet)
        {
            _pendingEffectKind = visualStyle.EffectKind;
            _pendingEffectOptions = visualStyle.EffectOptions;
        }

        if (!_themeExplicitlySet)
        {
            _requestedThemeMode = visualStyle.ThemeMode;
        }

        ApplyPendingThemeMode();
        ApplyPendingEffect();
        ApplyResolvedPalette(visualStyle.Palette);
    }

    internal void RefreshVisualStyles()
    {
        ApplyApplicationDefaults();
    }

    internal nint GetThemeBrush(ThemeColorSlot slot)
    {
        return slot switch
        {
            ThemeColorSlot.Window => _windowBackgroundBrush,
            ThemeColorSlot.Surface => _surfaceBackgroundBrush,
            _ => _controlBackgroundBrush,
        };
    }

    internal uint GetThemeBackgroundColorRef(ThemeColorSlot slot)
    {
        return slot switch
        {
            ThemeColorSlot.Window => Win32.ToColorRef(ToArgb(GetResolvedBackColor(CurrentVisualStyle.Palette))),
            ThemeColorSlot.Surface => _surfaceBackgroundColorRef,
            _ => _controlBackgroundColorRef,
        };
    }

    internal uint GetThemeForegroundColorRef(ThemeColorSlot slot)
    {
        return slot switch
        {
            ThemeColorSlot.Window => Win32.ToColorRef(ToArgb(GetResolvedForeColor(CurrentVisualStyle.Palette))),
            ThemeColorSlot.Surface => _surfaceForegroundColorRef,
            _ => _controlForegroundColorRef,
        };
    }

    private ResolvedVisualStyle ResolveCurrentVisualStyle()
    {
        ThemeMode requestedThemeMode = _themeExplicitlySet
            ? _requestedThemeMode ?? ThemeMode.System
            : Application.VisualStyleSettings.ThemeMode;

        EffectKind? preferredEffect = _effectExplicitlySet
            ? _pendingEffectKind
            : Application.VisualStyleSettings.PreferredEffect;

        EffectOptions? preferredEffectOptions = _effectExplicitlySet
            ? _pendingEffectOptions
            : Application.VisualStyleSettings.PreferredEffectOptions;

        NativeTheme? theme = _themeOverride ?? Application.VisualStyleSettings.Theme;
        ThemePalette? palette = _paletteOverride ?? Application.VisualStyleSettings.Palette;

        return Application.ResolveVisualStyle(
            requestedThemeMode,
            Application.VisualStyleSettings.PreferredVisualStyle,
            Application.VisualStyleSettings.ApplyBackdropEffects,
            preferredEffect,
            preferredEffectOptions,
            theme,
            palette);
    }

    private void ApplyPendingEffect()
    {
        if (NativeHandle == 0)
        {
            return;
        }

        LuminaWindow.SetEffect(NativeHandle, _pendingEffectKind, _pendingEffectOptions);
    }

    private void ApplyPendingThemeMode()
    {
        if (NativeHandle == 0 || !OperatingSystem.IsWindows())
        {
            return;
        }

        ThemeMode resolvedThemeMode = Application.ResolveThemeMode(_requestedThemeMode ?? ThemeMode.System);
        int useDarkMode = resolvedThemeMode == ThemeMode.Dark ? 1 : 0;
        _suppressMainMenuRefresh = true;
        try
        {
            _ = Win32.DwmSetWindowAttribute(NativeHandle, Win32.DWMWA_USE_IMMERSIVE_DARK_MODE, ref useDarkMode, sizeof(int));
            _ = DarkModeNative.AllowWindowDarkMode(NativeHandle, useDarkMode != 0);
            DarkModeNative.RefreshImmersiveState();
            DarkModeNative.RefreshTitleBarTheme(NativeHandle);
        }
        finally
        {
            _suppressMainMenuRefresh = false;
        }
    }

    private void ApplyResolvedPalette(ThemePalette palette)
    {
        SetBrush(ref _windowBackgroundBrush, ref _ownsWindowBackgroundBrush, ToArgb(GetResolvedBackColor(palette)), Win32.COLOR_BTNFACE);
        SetBrush(ref _surfaceBackgroundBrush, ref _ownsSurfaceBackgroundBrush, palette.SurfaceBackground, Win32.COLOR_BTNFACE);
        SetBrush(ref _controlBackgroundBrush, ref _ownsControlBackgroundBrush, palette.ControlBackground, Win32.COLOR_WINDOW);

        _windowForegroundColorRef = Win32.ToColorRef(ToArgb(GetResolvedForeColor(palette)));
        _surfaceForegroundColorRef = Win32.ToColorRef(palette.SurfaceForeground);
        _controlForegroundColorRef = Win32.ToColorRef(palette.ControlForeground);
        _surfaceBackgroundColorRef = Win32.ToColorRef(palette.SurfaceBackground);
        _controlBackgroundColorRef = Win32.ToColorRef(palette.ControlBackground);

        if (NativeHandle != 0)
        {
            RefreshHandles();
        }
    }

    private void RefreshHandles()
    {
        if (NativeHandle != 0)
        {
            DarkModeNative.RefreshTitleBarTheme(NativeHandle);
            RefreshMainMenuStrip();
        }

        _ = Win32.InvalidateRect(NativeHandle, 0, false);

        foreach (Control control in ChildControls)
        {
            RefreshControl(control);
        }
    }

    private static void RefreshControl(Control control)
    {
        control.RefreshTheme();

        foreach (Control child in control.ChildControls)
        {
            RefreshControl(child);
        }
    }

    private Color GetResolvedBackColor(ThemePalette palette)
        => TryGetExplicitBackColor(out Color color) ? color : Color.FromArgb(unchecked((int)palette.WindowBackground));

    private Color GetResolvedForeColor(ThemePalette palette)
        => TryGetExplicitForeColor(out Color color) ? color : Color.FromArgb(unchecked((int)palette.WindowForeground));

    private static void SetBrush(ref nint brush, ref bool ownsBrush, uint argb, int fallbackSysColor)
    {
        ReleaseBrush(ref brush, ref ownsBrush);

        brush = Win32.CreateSolidBrush(Win32.ToColorRef(argb));
        ownsBrush = brush != 0;
        if (brush == 0)
        {
            brush = Win32.GetSysColorBrush(fallbackSysColor);
        }
    }

    private static uint ToArgb(Color color)
        => unchecked((uint)color.ToArgb());

    private void ReleasePaletteResources()
    {
        ReleaseBrush(ref _windowBackgroundBrush, ref _ownsWindowBackgroundBrush);
        ReleaseBrush(ref _surfaceBackgroundBrush, ref _ownsSurfaceBackgroundBrush);
        ReleaseBrush(ref _controlBackgroundBrush, ref _ownsControlBackgroundBrush);
    }

    private static void ReleaseBrush(ref nint brush, ref bool ownsBrush)
    {
        if (brush != 0 && ownsBrush)
        {
            _ = Win32.DeleteObject(brush);
        }

        brush = 0;
        ownsBrush = false;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
    private static nint WindowProcThunk(nint hwnd, uint message, nint wParam, nint lParam)
    {
        Form? self = null;
        try
        {
            _ = TryGetControlByHandle(hwnd, out Control? control);
            self = control as Form;
            if (self is null && t_creatingForm is { WindowHandle: 0 } pending)
            {
                // Associate on the first callback (which can precede NCCREATE)
                // without occupying a system class's GWLP_USERDATA slot. The
                // pending context is consumed once and restored by the caller
                // around nested or reentrant window creation.
                self = pending.Form;
                pending.WindowHandle = hwnd;
                self.RegisterNativeHandle(hwnd);
            }

            try
            {
                return self is null
                    ? Win32.DefWindowProcW(hwnd, message, wParam, lParam)
                    : self.DispatchWindowMessage(hwnd, message, wParam, lParam);
            }
            finally
            {
                // A derived WndProc may consume this message or throw. The
                // native lifetime still ends and must release every root.
                if (message == Win32.WM_NCDESTROY && self is not null)
                {
                    self.ReleaseDestroyedFormHandle(hwnd);
                }
            }
        }
        catch (Exception exception)
        {
            // Include association and final cleanup in the native exception
            // boundary so no managed exception unwinds through USER32.
            try { Application.OnThreadException(exception); }
            catch (Exception reportingException)
            {
                Environment.FailFast("The application UI thread exception handler failed.", reportingException);
            }
            return 0;
        }
    }

    private void ReleaseDestroyedFormHandle(nint hwnd)
    {
        if (NativeHandle != hwnd) { return; }
        try
        {
            try { ReleaseMainMenuStrip(); }
            finally { ReleasePaletteResources(); }
        }
        finally
        {
            try { ClearNativeHandle(); }
            finally
            {
                if (_selfHandle.IsAllocated) { _selfHandle.Free(); }
                _formDefaultWindowProc = 0;
                if (Disposing) { UnregisterOpenForm(); }
            }
        }
    }

    protected override void DefWndProc(ref Message m)
    {
        if (_formDefaultWindowProc != 0)
        {
            m.Result = Win32.CallWindowProcW(_formDefaultWindowProc, m.HWnd,
                unchecked((uint)m.Msg), m.WParam, m.LParam);
        }
        else
        {
            base.DefWndProc(ref m);
        }
    }

    protected override void WndProc(ref Message message)
    {
        message.Result = WindowProc(message.HWnd, unchecked((uint)message.Msg), message.WParam, message.LParam, ref message);
    }

    private nint WindowProc(nint hwnd, uint message, nint wParam, nint lParam, ref Message managedMessage)
    {
        if (ThreadDispatcher?.TryDispatch(message) == true)
        {
            return 0;
        }

        switch (message)
        {
            case Win32.WM_SHOWWINDOW:
            {
                DefWndProc(ref managedMessage);
                if (!_restoringNativeVisibility)
                {
                    NotifyNativeVisibilityChanged(wParam != 0);
                }
                return managedMessage.Result;
            }

            case Win32.WM_MOVE:
                UpdateBoundsFromNativeWindow();
                return 0;

            case Win32.WM_SIZE:
                UpdateBoundsFromNativeWindow();
                return 0;

            case Win32.WM_CLOSE:
            {
                FormClosingEventArgs closingArgs = new(CloseReason.UserClosing);
                OnFormClosing(closingArgs);
                if (closingArgs.Cancel)
                {
                    return 0;
                }

                OnClosed();
                Dispose();
                return 0;
            }

            case Win32.WM_CONTEXTMENU:
                if (HandleContextMenu(wParam, lParam))
                {
                    return 0;
                }

                break;

            case Win32.WM_PARENTNOTIFY:
                if (HandleParentNotify(wParam, lParam))
                {
                    return 0;
                }

                break;

            case Win32.WM_KEYDOWN:
            case Win32.WM_SYSKEYDOWN:
                if (TryHandleMenuShortcut(BuildShortcutKeyData(wParam)))
                {
                    return 0;
                }

                if (!UsesNativeMainMenuBar() && (int)(nuint)wParam == Win32.VK_F10 && TryActivateFirstMenuItem())
                {
                    return 0;
                }

                break;

            case Win32.WM_SYSCHAR:
                if (!UsesNativeMainMenuBar() && TryHandleMenuMnemonic((char)(ushort)(nuint)wParam))
                {
                    return 0;
                }

                break;

            case Win32.WM_SETTINGCHANGE:
                if (DarkModeNative.IsColorSchemeChangeMessage(lParam))
                {
                    DarkModeNative.RefreshImmersiveState();
                }

                RefreshVisualStyles();
                break;

            case Win32.WM_THEMECHANGED:
                DarkModeNative.RefreshImmersiveState();
                RefreshVisualStyles();
                break;

            case Win32.WM_INITMENUPOPUP:
                ToolStripPopupMenu.NotifyMenuDepthChange(+1);
                break;

            case Win32.WM_UNINITMENUPOPUP:
                ToolStripPopupMenu.NotifyMenuDepthChange(-1);
                break;

            case Win32.WM_MENUSELECT:
            {
                int flags = Win32.HighWord(wParam);
                ToolStripPopupMenu.NotifyMenuSelectionChanged((flags & (int)Win32.MF_POPUP) != 0);
                break;
            }

            case Win32.WM_CTLCOLORBTN:
            case Win32.WM_CTLCOLORDLG:
            case Win32.WM_CTLCOLOREDIT:
            case Win32.WM_CTLCOLORLISTBOX:
            case Win32.WM_CTLCOLORSTATIC:
                if (TryHandleControlColor(message, wParam, lParam, out nint brush))
                {
                    return brush;
                }

                break;

            case Win32.WM_MEASUREITEM:
                if (OperatingSystem.IsWindows() && NativeMenuRenderer.TryHandleMeasureItem(lParam))
                {
                    return (nint)1;
                }

                break;

            case Win32.WM_DRAWITEM:
                if (OperatingSystem.IsWindows() && NativeMenuRenderer.TryHandleDrawItem(lParam))
                {
                    return (nint)1;
                }

                break;

            case Win32.WM_NOTIFY:
                if (HandleNotify(lParam))
                {
                    return 0;
                }

                return 0;

            case Win32.WM_COMMAND:
                if (HandleCommand(wParam, lParam))
                {
                    return 0;
                }

                return 0;

            case Win32.WM_DESTROY:
                base.WndProc(ref managedMessage);
                return managedMessage.Result;

            default:
                base.WndProc(ref managedMessage);
                return managedMessage.Result;
        }

        base.WndProc(ref managedMessage);
        return managedMessage.Result;
    }

    private bool HandleCommand(nint wParam, nint lParam)
    {
        int controlId = Win32.LowWord(wParam);
        int notificationCode = Win32.HighWord(wParam);

        if (_mainMenuStrip is not null && _mainMenuStrip.TryHandleNativeCommand(controlId))
        {
            return true;
        }

        if (_controlsById.TryGetValue(controlId, out var control))
        {
            _ = control.HandleCommand(notificationCode);
        }

        OnCommand(controlId, notificationCode, lParam);
        return true;
    }

    private bool HandleNotify(nint lParam)
    {
        if (lParam == 0)
        {
            return false;
        }

        Win32.NMHDR header = Marshal.PtrToStructure<Win32.NMHDR>(lParam);
        int controlId = unchecked((int)header.idFrom);
        int notificationCode = unchecked((int)header.code);

        return _controlsById.TryGetValue(controlId, out var control)
            && control.HandleNotify(notificationCode, lParam);
    }

    internal void RouteCommandFromContainer(Control control, int notificationCode, nint sourceHandle)
    {
        ArgumentNullException.ThrowIfNull(control);

        _ = control.HandleCommand(notificationCode);
        OnCommand(control.Id, notificationCode, sourceHandle);
    }

    internal bool RouteNotifyFromContainer(Control control, int notificationCode, nint lParam)
    {
        ArgumentNullException.ThrowIfNull(control);
        return control.HandleNotify(notificationCode, lParam);
    }

    private bool HandleContextMenu(nint wParam, nint lParam)
    {
        if (wParam != 0
            && Control.TryGetControlByHandle(wParam, out Control? directControl)
            && directControl is not null
            && directControl.TryShowAttachedContextMenu(wParam, lParam))
        {
            return true;
        }

        if (TryResolveControlFromScreenPoint(lParam, out Control? pointControl, out nint controlHandle, out _))
        {
            return pointControl!.TryShowAttachedContextMenu(controlHandle, lParam);
        }

        return false;
    }

    private bool HandleParentNotify(nint wParam, nint lParam)
    {
        if (Win32.LowWord(wParam) != Win32.WM_RBUTTONDOWN)
        {
            return false;
        }

        if (!TryResolveControlFromParentClientPoint(lParam, out Control? control, out nint controlHandle, out Point screenLocation))
        {
            return false;
        }

        return control!.TryShowAttachedContextMenuAtScreenPoint(controlHandle, screenLocation);
    }

    private bool TryResolveControlFromParentClientPoint(nint lParam, out Control? control, out nint controlHandle, out Point screenLocation)
    {
        Point parentClientPoint = Control.ExtractPoint(lParam);
        var screenPoint = new Win32.POINT
        {
            x = parentClientPoint.X,
            y = parentClientPoint.Y,
        };

        if (NativeHandle != 0
            && Win32.ClientToScreen(NativeHandle, ref screenPoint)
            && TryGetControlFromScreenPoint(new Point(screenPoint.x, screenPoint.y), out control, out controlHandle))
        {
            screenLocation = new Point(screenPoint.x, screenPoint.y);
            return true;
        }

        return TryResolveControlFromScreenPoint(lParam, out control, out controlHandle, out screenLocation);
    }

    private static bool TryResolveControlFromScreenPoint(nint lParam, out Control? control, out nint controlHandle, out Point screenLocation)
    {
        bool hasMessagePoint = lParam != 0 && lParam != (nint)(-1);
        Point messagePoint = hasMessagePoint
            ? Control.ExtractPoint(lParam)
            : default;

        if (hasMessagePoint && TryGetControlFromScreenPoint(messagePoint, out control, out controlHandle))
        {
            screenLocation = messagePoint;
            return true;
        }

        if (Win32.GetCursorPos(out var cursor)
            && TryGetControlFromScreenPoint(new Point(cursor.x, cursor.y), out control, out controlHandle))
        {
            screenLocation = new Point(cursor.x, cursor.y);
            return true;
        }

        control = null;
        controlHandle = 0;
        screenLocation = default;
        return false;
    }

    private static bool TryGetControlFromScreenPoint(Point screenLocation, out Control? control, out nint controlHandle)
    {
        nint hwnd = Win32.WindowFromPoint(new Win32.POINT { x = screenLocation.X, y = screenLocation.Y });
        if (hwnd != 0 && Control.TryGetControlByHandle(hwnd, out control) && control is not null)
        {
            controlHandle = hwnd;
            return true;
        }

        control = null;
        controlHandle = 0;
        return false;
    }

    private bool HandleEraseBackground(nint hdc)
    {
        if (hdc == 0 || NativeHandle == 0 || _windowBackgroundBrush == 0)
        {
            return false;
        }

        if (!Win32.GetClientRect(NativeHandle, out var rect))
        {
            return false;
        }

        return Win32.FillRect(hdc, ref rect, _windowBackgroundBrush);
    }

    private bool TryHandleControlColor(uint message, nint wParam, nint lParam, out nint brush)
    {
        brush = 0;
        if (wParam == 0)
        {
            return false;
        }

        _ = Control.TryGetControlByHandle(lParam, out Control? control);

        if (control is not null && control.TryGetThemeColors(out nint controlBrush, out uint backgroundColorRef, out uint foregroundColorRef, out bool transparentBackground))
        {
            if (transparentBackground)
            {
                _ = Win32.SetBkMode(wParam, Win32.TRANSPARENT);
            }
            else
            {
                _ = Win32.SetBkColor(wParam, backgroundColorRef);
            }

            _ = Win32.SetTextColor(wParam, foregroundColorRef);
            brush = controlBrush;
            return true;
        }

        switch (message)
        {
            case Win32.WM_CTLCOLOREDIT:
            case Win32.WM_CTLCOLORLISTBOX:
                if (_controlBackgroundBrush == 0)
                {
                    return false;
                }

                _ = Win32.SetBkColor(wParam, _controlBackgroundColorRef);
                _ = Win32.SetTextColor(wParam, _controlForegroundColorRef);
                brush = _controlBackgroundBrush;
                return true;

            case Win32.WM_CTLCOLORSTATIC:
                if (_surfaceBackgroundBrush == 0)
                {
                    return false;
                }

                _ = Win32.SetBkMode(wParam, Win32.TRANSPARENT);
                _ = Win32.SetBkColor(wParam, _surfaceBackgroundColorRef);
                _ = Win32.SetTextColor(
                    wParam,
                    control is TabPage or Panel or ToolStrip or StatusStrip or GroupBox or CheckBox or RadioButton or Label or LinkLabel
                        ? _surfaceForegroundColorRef
                        : _windowForegroundColorRef);
                brush = _surfaceBackgroundBrush;
                return true;

            case Win32.WM_CTLCOLORBTN:
            case Win32.WM_CTLCOLORDLG:
                if (_surfaceBackgroundBrush == 0)
                {
                    return false;
                }

                _ = Win32.SetBkColor(wParam, _surfaceBackgroundColorRef);
                _ = Win32.SetTextColor(
                    wParam,
                    control is GroupBox or CheckBox or RadioButton or Button
                        ? _surfaceForegroundColorRef
                        : _windowForegroundColorRef);
                brush = _surfaceBackgroundBrush;
                return true;

            default:
                return false;
        }
    }

    private bool TryHandleMenuShortcut(Keys keyData)
    {
        if (MainMenuStrip is null || keyData == Keys.None)
        {
            return false;
        }

        return TryHandleMenuShortcut(MainMenuStrip.Items, keyData);
    }

    private static bool TryHandleMenuShortcut(IEnumerable<ToolStripItem> items, Keys keyData)
    {
        foreach (ToolStripItem item in items)
        {
            if (!item.Visible || !item.Enabled)
            {
                continue;
            }

            if (item is ToolStripMenuItem menuItem && menuItem.MatchesShortcut(keyData))
            {
                menuItem.PerformClick();
                return true;
            }

            if (item is ToolStripDropDownItem dropDownItem && TryHandleMenuShortcut(dropDownItem.DropDownItems, keyData))
            {
                return true;
            }
        }

        return false;
    }

    private bool TryHandleMenuMnemonic(char mnemonic)
    {
        if (MainMenuStrip is null || char.IsControl(mnemonic))
        {
            return false;
        }

        return MainMenuStrip.TryActivateMnemonic(mnemonic);
    }

    private bool TryActivateFirstMenuItem()
    {
        return MainMenuStrip is not null && MainMenuStrip.TryActivateFirstItem();
    }

    private static Keys BuildShortcutKeyData(nint virtualKey)
    {
        Keys keyData = (Keys)(unchecked((int)(nuint)virtualKey) & (int)Keys.KeyCode);

        if ((Win32.GetKeyState(Win32.VK_CONTROL) & 0x8000) != 0)
        {
            keyData |= Keys.Control;
        }

        if ((Win32.GetKeyState(Win32.VK_SHIFT) & 0x8000) != 0)
        {
            keyData |= Keys.Shift;
        }

        if ((Win32.GetKeyState(Win32.VK_MENU) & 0x8000) != 0)
        {
            keyData |= Keys.Alt;
        }

        return keyData;
    }

    private void ApplyWindowIcon()
    {
        if (!OperatingSystem.IsWindows() || NativeHandle == 0)
        {
            return;
        }

        nint iconHandle = ResolveIconHandle();
        if (iconHandle == 0)
        {
            return;
        }

        _ = Win32.SendMessageW(NativeHandle, Win32.WM_SETICON, (nint)Win32.ICON_BIG, iconHandle);
        _ = Win32.SendMessageW(NativeHandle, Win32.WM_SETICON, (nint)Win32.ICON_SMALL, iconHandle);
    }

    private nint ResolveIconHandle()
        => OperatingSystem.IsWindows()
            ? ResolveIconHandleCore()
            : 0;

    private void ReleaseOwnedIcon()
    {
        if (!OperatingSystem.IsWindows())
        {
            _icon = null;
            return;
        }

        ReleaseOwnedIconCore();
    }

    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    private static Icon CloneIcon(Icon icon)
        => (Icon)icon.Clone();

    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    private nint ResolveIconHandleCore()
        => Icon?.Handle ?? 0;

    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    private void ReleaseOwnedIconCore()
    {
        _icon?.Dispose();
        _icon = null;
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(IsDisposed, this);
    }

    private sealed class FormCreationContext(Form form)
    {
        internal Form Form { get; } = form;
        internal nint WindowHandle { get; set; }
    }

}
