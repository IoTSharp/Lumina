using System.Runtime.InteropServices;

namespace Lumina.Forms;

internal static partial class Win32
{
    // EnumThreadWindows borrows each HWND. The callback runs synchronously and
    // returns a Win32 BOOL; its context must remain alive until enumeration ends.
    [LibraryImport("user32.dll", SetLastError = true)]
    internal static unsafe partial int EnumThreadWindows(
        uint threadId, delegate* unmanaged[Stdcall]<nint, nint, int> callback, nint context);

    [DllImport("user32.dll")]
    internal static extern nint GetActiveWindow();

    [DllImport("user32.dll", EntryPoint = "PostThreadMessageW", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool PostThreadMessageW(uint threadId, uint message, nuint wParam, nint lParam);
}
