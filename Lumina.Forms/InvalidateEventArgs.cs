using System.Drawing;

namespace Lumina.Forms;

/// <summary>Identifies the rectangle invalidated on a control.</summary>
public class InvalidateEventArgs : EventArgs
{
    public InvalidateEventArgs(Rectangle invalidRect) => InvalidRect = invalidRect;
    public Rectangle InvalidRect { get; }
}
