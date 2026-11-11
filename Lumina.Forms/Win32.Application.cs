using System.Runtime.InteropServices;

namespace Lumina.Forms;

internal static partial class Win32
{
    [DllImport("user32.dll")]
    internal static extern nint GetActiveWindow();

    [DllImport("user32.dll", EntryPoint = "PostThreadMessageW", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool PostThreadMessageW(uint threadId, uint message, nuint wParam, nint lParam);
}
