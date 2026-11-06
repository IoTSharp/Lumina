namespace Lumina.Forms;

/// <summary>Allows a control to act as a form's accept or cancel button.</summary>
public interface IButtonControl
{
    /// <summary>Gets or sets the result assigned to the containing dialog.</summary>
    DialogResult DialogResult { get; set; }

    /// <summary>Notifies the control whether it is the current default button.</summary>
    void NotifyDefault(bool value);

    /// <summary>Activates the control's click action.</summary>
    void PerformClick();
}
