namespace Lumina.Forms;

/// <summary>Provides mouse event data that can prevent further native processing.</summary>
public class HandledMouseEventArgs : MouseEventArgs
{
    /// <summary>Initializes mouse event data with further native processing enabled.</summary>
    public HandledMouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta)
        : this(button, clicks, x, y, delta, false)
    {
    }

    /// <summary>Initializes mouse event data with the specified handled state.</summary>
    public HandledMouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta, bool defaultHandledValue)
        : base(button, clicks, x, y, delta)
    {
        Handled = defaultHandledValue;
    }

    /// <summary>Gets or sets whether the event has been handled.</summary>
    public bool Handled { get; set; }
}
