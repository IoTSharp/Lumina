using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Lumina.Forms;

public partial class MaskedTextBox
{
    private bool CopyMaskedSelection()
    {
        if (_provider.IsPassword) { return false; }
        string text = SelectedText;
        if (!ClipboardNative.OpenClipboard(NativeHandle))
            throw new Win32Exception(Marshal.GetLastPInvokeError());
        nint memory = 0;
        try
        {
            if (!ClipboardNative.EmptyClipboard())
                throw new Win32Exception(Marshal.GetLastPInvokeError());
            if (text.Length == 0) { return true; }
            memory = ClipboardNative.GlobalAlloc(0x0002, checked((nuint)(text.Length + 1) * 2));
            if (memory == 0) { throw new Win32Exception(Marshal.GetLastPInvokeError()); }
            nint pointer = ClipboardNative.GlobalLock(memory);
            if (pointer == 0) { throw new Win32Exception(Marshal.GetLastPInvokeError()); }
            try
            {
                char[] characters = (text + '\0').ToCharArray();
                Marshal.Copy(characters, 0, pointer, characters.Length);
            }
            finally { _ = ClipboardNative.GlobalUnlock(memory); }
            if (ClipboardNative.SetClipboardData(13, memory) == 0)
                throw new Win32Exception(Marshal.GetLastPInvokeError());
            memory = 0; // Windows now owns this HGLOBAL.
            return true;
        }
        finally
        {
            if (memory != 0) { _ = ClipboardNative.GlobalFree(memory); }
            _ = ClipboardNative.CloseClipboard();
        }
    }

    private string? ReadClipboardText()
    {
        if (!ClipboardNative.OpenClipboard(NativeHandle))
            throw new Win32Exception(Marshal.GetLastPInvokeError());
        try
        {
            nint memory = ClipboardNative.GetClipboardData(13);
            if (memory == 0) { return null; }
            nuint bytes = ClipboardNative.GlobalSize(memory);
            if (bytes > int.MaxValue) { throw new InvalidOperationException("The clipboard text is too large."); }
            nint pointer = ClipboardNative.GlobalLock(memory);
            if (pointer == 0) { throw new Win32Exception(Marshal.GetLastPInvokeError()); }
            try
            {
                // Scan only the allocation returned by Windows.
                string value = Marshal.PtrToStringUni(pointer, checked((int)(bytes / 2))) ?? string.Empty;
                int terminator = value.IndexOf('\0');
                return terminator < 0 ? value : value[..terminator];
            }
            finally { _ = ClipboardNative.GlobalUnlock(memory); }
        }
        finally { _ = ClipboardNative.CloseClipboard(); }
    }

    private static partial class ClipboardNative
    {
        [LibraryImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool OpenClipboard(nint owner);

        [LibraryImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool CloseClipboard();

        [LibraryImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool EmptyClipboard();

        [LibraryImport("user32.dll", SetLastError = true)]
        internal static partial nint GetClipboardData(uint format);

        [LibraryImport("user32.dll", SetLastError = true)]
        internal static partial nint SetClipboardData(uint format, nint memory);

        [LibraryImport("kernel32.dll", SetLastError = true)]
        internal static partial nint GlobalAlloc(uint flags, nuint bytes);

        [LibraryImport("kernel32.dll", SetLastError = true)]
        internal static partial nint GlobalLock(nint memory);

        [LibraryImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool GlobalUnlock(nint memory);

        [LibraryImport("kernel32.dll", SetLastError = true)]
        internal static partial nint GlobalFree(nint memory);

        [LibraryImport("kernel32.dll", SetLastError = true)]
        internal static partial nuint GlobalSize(nint memory);
    }
}
