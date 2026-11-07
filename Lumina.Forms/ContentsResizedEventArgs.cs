using System.Drawing;

namespace Lumina.Forms;

/// <summary>Provides the rectangle required to display a rich text control's contents.</summary>
public class ContentsResizedEventArgs : EventArgs
{
    public ContentsResizedEventArgs(Rectangle newRectangle) => NewRectangle = newRectangle;

    public Rectangle NewRectangle { get; }
}
