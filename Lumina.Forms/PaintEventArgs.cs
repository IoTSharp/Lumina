using System.Drawing;

namespace Lumina.Forms;

/// <summary>Provides the drawing surface and clipping rectangle for a paint event.</summary>
public class PaintEventArgs : EventArgs, IDisposable, IDeviceContext
{
    public PaintEventArgs(Graphics graphics, Rectangle clipRect)
    {
        ArgumentNullException.ThrowIfNull(graphics);
        Graphics = graphics;
        ClipRectangle = clipRect;
    }

    ~PaintEventArgs() => Dispose(false);

    public Rectangle ClipRectangle { get; }
    public Graphics Graphics { get; }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        // The public constructor borrows its Graphics. The creator, including
        // the native paint scope, owns and disposes that drawing surface.
        // Keep this hook and the finalizer for derived event-argument resources.
    }

    nint IDeviceContext.GetHdc() => Graphics.GetHdc();
    void IDeviceContext.ReleaseHdc() => Graphics.ReleaseHdc();
}
