using System.ComponentModel;

namespace Lumina.Forms;

/// <summary>
/// Provides the common native button behavior for push buttons, check boxes and radio buttons.
/// </summary>
public abstract class ButtonBase : Control
{
    private bool _isDefault;
    private bool _useVisualStyleBackColor = true;

    /// <summary>
    /// Initializes a button backed by the system BUTTON window class.
    /// </summary>
    protected ButtonBase()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick, false);
    }

    /// <summary>
    /// Gets or sets whether a themed push button uses its parent's background.
    /// </summary>
    [DefaultValue(true)]
    public bool UseVisualStyleBackColor
    {
        get => _useVisualStyleBackColor;
        set
        {
            if (_useVisualStyleBackColor == value) { return; }
            _useVisualStyleBackColor = value;
            if (IsHandleCreated) { Invalidate(); }
        }
    }

    /// <summary>
    /// Gets or sets whether the button is the current default button.
    /// </summary>
    protected internal bool IsDefault
    {
        get => _isDefault;
        set
        {
            if (_isDefault == value) { return; }
            _isDefault = value;
            UpdateStyles();
        }
    }

    /// <inheritdoc />
    protected override string ClassName => "BUTTON";

    /// <inheritdoc />
    protected override bool UseParentBackgroundForTheming => UseVisualStyleBackColor;

    /// <inheritdoc />
    protected override void ApplyTheme()
    {
        ApplyNativeThemeState();
        ApplyExplorerTheme();
    }

    /// <inheritdoc />
    protected override void OnHandleCreated()
    {
        base.OnHandleCreated();
        ApplyNativeThemeState();
    }

    private void ApplyNativeThemeState()
    {
        if (NativeHandle == 0) { return; }
        DarkModeNative.ApplyThemeToWindow(NativeHandle, CurrentVisualStyle.IsDarkMode);
    }
}
