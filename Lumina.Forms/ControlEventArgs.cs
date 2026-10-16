namespace Lumina.Forms;

/// <summary>
/// Supplies the child control for a collection change notification.
/// </summary>
public class ControlEventArgs : EventArgs
{
    /// <summary>
    /// Initializes the notification with the added or removed control.
    /// </summary>
    public ControlEventArgs(Control? control) => Control = control;

    /// <summary>
    /// Gets the control associated with the notification.
    /// </summary>
    public Control? Control { get; }
}
