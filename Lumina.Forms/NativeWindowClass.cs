using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Lumina.Forms;

internal readonly record struct NativeWindowClassRegistration(string Name, nint OriginalWindowProc);

/// <summary>Registers window classes while retaining the selected system class procedure.</summary>
internal static class NativeWindowClass
{
    private static readonly Lock s_sync = new();
    private static readonly Dictionary<(string? Name, int Style, nint Callback, nint Instance, nint Icon, nint Brush), NativeWindowClassRegistration> s_classes = [];

    internal static unsafe NativeWindowClassRegistration GetOrRegister(
        string? className, int classStyle, nint windowProc, nint instanceHandle,
        nint iconHandle = 0, nint backgroundBrush = 0)
    {
        // ClassStyle applies only to a newly defined class, as in CreateParams.
        var key = (className, className is null ? classStyle : 0, windowProc, instanceHandle, iconHandle, backgroundBrush);
        lock (s_sync)
        {
            if (s_classes.TryGetValue(key, out NativeWindowClassRegistration registration)) { return registration; }

            Win32.WNDCLASSEXW_NATIVE windowClass = new() { cbSize = (uint)Marshal.SizeOf<Win32.WNDCLASSEXW_NATIVE>() };
            nint originalWindowProc = 0;
            if (className is null)
            {
                windowClass.style = unchecked((uint)classStyle);
                windowClass.hIcon = iconHandle;
                windowClass.hIconSm = iconHandle;
                windowClass.hCursor = Win32.LoadCursorW(0, (nint)Win32.IDC_ARROW);
                windowClass.hbrBackground = backgroundBrush == 0 ? Win32.GetStockObject(5 /* NULL_BRUSH */) : backgroundBrush;
            }
            else
            {
                if (!Win32.GetClassInfoExW(instanceHandle, className, ref windowClass)
                    && !Win32.GetClassInfoExW(0, className, ref windowClass))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error(), $"The native window class '{className}' is not registered.");
                }
                originalWindowProc = windowClass.lpfnWndProc;
            }

            // Keep the source class recognizable to native diagnostics while
            // the unique suffix separates callback/style registrations.
            string registeredName = $"LuminaFormsWindow.{className ?? "Control"}.{Environment.ProcessId:x}.{s_classes.Count:x}";
            windowClass.lpfnWndProc = windowProc;
            windowClass.hInstance = instanceHandle;
            fixed (char* name = registeredName)
            {
                windowClass.lpszClassName = (nint)name;
                if (Win32.RegisterClassExW(ref windowClass) == 0)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error(), "Failed to register the native window class.");
                }
            }

            registration = new(registeredName, originalWindowProc);
            s_classes.Add(key, registration);
            return registration;
        }
    }
}
