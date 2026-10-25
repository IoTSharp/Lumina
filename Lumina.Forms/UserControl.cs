namespace Lumina.Forms;

/// <summary>
/// Represents a reusable composite control hosted inside a form or another container.
/// </summary>
public class UserControl : ContainerControl
{
    private bool _loaded;

    /// <summary>
    /// Occurs the first time the user control and its native child tree are
    /// created successfully.
    /// </summary>
    public event EventHandler? Load;

    /// <inheritdoc />
    protected override string ClassName => "STATIC";

    /// <inheritdoc />
    protected override uint Style => Win32.WS_CHILD | Win32.WS_VISIBLE | Win32.WS_CLIPCHILDREN | Win32.WS_CLIPSIBLINGS;

    /// <inheritdoc />
    private protected override ThemeColorSlot DefaultBackgroundSlot => ThemeColorSlot.Surface;

    /// <inheritdoc />
    private protected override ThemeColorSlot DefaultForegroundSlot => ThemeColorSlot.Surface;

    /// <inheritdoc />
    protected override void OnHandleCreated()
    {
        base.OnHandleCreated();
        ApplyNativeThemeState();
    }

    /// <inheritdoc />
    protected override void OnCreateControl()
    {
        base.OnCreateControl();
        if (!_loaded && !IsDisposed)
        {
            _loaded = true;
            OnLoad(EventArgs.Empty);
        }
    }

    /// <summary>Raises the <see cref="Load"/> event.</summary>
    protected virtual void OnLoad(EventArgs e) => Load?.Invoke(this, e);

    /// <inheritdoc />
    protected override void ApplyTheme()
    {
        ApplyNativeThemeState();
    }

    private void ApplyNativeThemeState()
    {
        if (NativeHandle == 0)
        {
            return;
        }

        DarkModeNative.ApplyThemeToWindow(NativeHandle, CurrentVisualStyle.IsDarkMode);
    }
}
