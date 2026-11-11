namespace Lumina.Forms;

/// <summary>Provides the reason a form was closed.</summary>
public class FormClosedEventArgs(CloseReason closeReason) : EventArgs
{
    /// <summary>Gets the reason for closing the form.</summary>
    public CloseReason CloseReason { get; } = closeReason;
}

/// <summary>Handles a form's completed close notification.</summary>
public delegate void FormClosedEventHandler(object? sender, FormClosedEventArgs e);
