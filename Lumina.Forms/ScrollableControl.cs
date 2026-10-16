using System.Drawing;

namespace Lumina.Forms;

/// <summary>
/// Provides a client-area container for scrollable controls.
/// </summary>
public class ScrollableControl : Control
{
    /// <inheritdoc />
    public override Rectangle DisplayRectangle => new(
        Padding.Left,
        Padding.Top,
        Math.Max(0, ClientSize.Width - Padding.Horizontal),
        Math.Max(0, ClientSize.Height - Padding.Vertical));
}
