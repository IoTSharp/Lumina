namespace Lumina.Forms;

/// <summary>
/// Provides access to a native Win32 window handle.
/// </summary>
public interface IWin32Window
{
    /// <summary>
    /// Gets the native window handle.
    /// </summary>
    nint Handle { get; }
}
