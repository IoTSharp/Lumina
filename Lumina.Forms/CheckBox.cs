namespace Lumina.Forms;

/// <summary>
/// Represents a standard two-state check box control.
/// </summary>
public class CheckBox : ButtonBase
{
    private bool _checked;

    /// <summary>Initializes a check box drawn by the native button control.</summary>
    public CheckBox() => SetStyle(ControlStyles.StandardDoubleClick, false);

    /// <summary>
    /// Occurs when the <see cref="Checked"/> state changes.
    /// </summary>
    public event EventHandler? CheckedChanged;

    /// <summary>
    /// Gets or sets a value indicating whether the check box is checked.
    /// </summary>
    public bool Checked
    {
        get
        {
            if (NativeHandle != 0)
            {
                _checked = Win32.SendMessageW(NativeHandle, Win32.BM_GETCHECK, 0, 0) == (nint)Win32.BST_CHECKED;
            }

            return _checked;
        }
        set
        {
            if (_checked == value)
            {
                if (NativeHandle != 0)
                {
                    _ = Win32.SendMessageW(NativeHandle, Win32.BM_SETCHECK, (nint)(value ? Win32.BST_CHECKED : Win32.BST_UNCHECKED), 0);
                }

                return;
            }

            _checked = value;
            if (NativeHandle != 0)
            {
                _ = Win32.SendMessageW(NativeHandle, Win32.BM_SETCHECK, (nint)(value ? Win32.BST_CHECKED : Win32.BST_UNCHECKED), 0);
            }

            OnCheckedChanged(EventArgs.Empty);
        }
    }

    public CheckState CheckState
    {
        get => Checked ? CheckState.Checked : CheckState.Unchecked;
        set => Checked = value == CheckState.Checked;
    }

    /// <inheritdoc />
    protected override uint Style => Win32.WS_CHILD | Win32.WS_VISIBLE | Win32.WS_TABSTOP | Win32.BS_AUTOCHECKBOX;

    /// <inheritdoc />
    private protected override ThemeColorSlot DefaultForegroundSlot => ThemeColorSlot.Surface;

    /// <inheritdoc />
    protected override bool UseParentBackgroundForTheming => false;

    /// <inheritdoc />
    protected override void OnHandleCreated()
    {
        base.OnHandleCreated();
        Checked = _checked;
    }

    /// <inheritdoc />
    protected override bool OnCommand(int notificationCode)
    {
        if (notificationCode != Win32.BN_CLICKED)
        {
            return false;
        }

        bool currentValue = Win32.SendMessageW(NativeHandle, Win32.BM_GETCHECK, 0, 0) == (nint)Win32.BST_CHECKED;
        if (_checked != currentValue)
        {
            _checked = currentValue;
            OnCheckedChanged(EventArgs.Empty);
        }

        OnClick(EventArgs.Empty);
        return true;
    }

    /// <summary>
    /// Raises the <see cref="CheckedChanged"/> event.
    /// </summary>
    /// <param name="e">The event arguments.</param>
    protected virtual void OnCheckedChanged(EventArgs e)
    {
        CheckedChanged?.Invoke(this, e);
    }
}
