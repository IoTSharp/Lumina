// Button activation portions adapted from dotnet/winforms v10.0.11.
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses those portions under the MIT license.

using System.ComponentModel;

namespace Lumina.Forms;

/// <summary>
/// Represents a standard push button control.
/// </summary>
public class Button : ButtonBase, IButtonControl
{
    private DialogResult _dialogResult;

    /// <summary>
    /// Initializes a button with Windows-style default spacing.
    /// </summary>
    public Button()
    {
        SetStyle(ControlStyles.StandardDoubleClick, false);
        Margin = new Padding(3);
    }

    /// <summary>Gets or sets the result assigned to the containing form when clicked.</summary>
    [DefaultValue(DialogResult.None)]
    public virtual DialogResult DialogResult
    {
        get => _dialogResult;
        set
        {
            if (!Enum.IsDefined(value))
            {
                throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(DialogResult));
            }
            _dialogResult = value;
        }
    }

    /// <summary>Updates whether this button is the form's current default button.</summary>
    public virtual void NotifyDefault(bool value)
    {
        IsDefault = value;
    }

    /// <summary>
    /// Programmatically raises the button click action.
    /// </summary>
    public void PerformClick()
    {
        if (CanSelect)
        {
            bool valid = ValidateActiveControl(out bool allowsFocusChange);
            if (!ValidationCancelled && (valid || allowsFocusChange))
            {
                OnClick(EventArgs.Empty);
            }
        }
    }

    /// <inheritdoc />
    protected override uint Style => Win32.WS_CHILD | Win32.WS_VISIBLE | Win32.WS_TABSTOP
        | Win32.BS_PUSHBUTTON | (IsDefault ? 0x00000001u : 0u); // BS_DEFPUSHBUTTON

    /// <inheritdoc />
    protected override int GetNativeHeight(int requestedHeight)
        => Math.Max(23, requestedHeight);

    /// <inheritdoc />
    protected override bool OnCommand(int notificationCode)
    {
        if (notificationCode != Win32.BN_CLICKED)
        {
            return false;
        }

        if (!ValidationCancelled) { OnClick(EventArgs.Empty); }
        return true;
    }

    /// <summary>
    /// Raises the <see cref="Click"/> event.
    /// </summary>
    /// <param name="e">The event arguments.</param>
    protected override void OnClick(EventArgs e)
    {
        if (FindForm() is { } form) { form.DialogResult = _dialogResult; }
        base.OnClick(e);
    }
}
