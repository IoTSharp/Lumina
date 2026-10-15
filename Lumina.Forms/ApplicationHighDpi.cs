using System.Runtime.InteropServices;

namespace Lumina.Forms;

/// <summary>
/// Keeps process DPI interop separate from the window and control implementations.
/// </summary>
internal static partial class ApplicationHighDpi
{
    private static readonly nint s_unawareContext = -1;
    private static readonly nint s_systemAwareContext = -2;
    private static readonly nint s_perMonitorContext = -3;
    private static readonly nint s_perMonitorV2Context = -4;
    private static readonly nint s_unawareGdiScaledContext = -5;

    internal static HighDpiMode GetCurrentMode()
    {
        if (!OperatingSystem.IsWindows())
        {
            return HighDpiMode.DpiUnaware;
        }

        if (OperatingSystem.IsWindowsVersionAtLeast(10, 0, 14393))
        {
            nint context = GetThreadDpiAwarenessContext();
            if (AreDpiAwarenessContextsEqual(context, s_perMonitorV2Context) != 0)
            {
                return HighDpiMode.PerMonitorV2;
            }

            if (AreDpiAwarenessContextsEqual(context, s_perMonitorContext) != 0)
            {
                return HighDpiMode.PerMonitor;
            }

            if (AreDpiAwarenessContextsEqual(context, s_systemAwareContext) != 0)
            {
                return HighDpiMode.SystemAware;
            }

            return AreDpiAwarenessContextsEqual(context, s_unawareGdiScaledContext) != 0
                ? HighDpiMode.DpiUnawareGdiScaled
                : HighDpiMode.DpiUnaware;
        }

        if (OperatingSystem.IsWindowsVersionAtLeast(6, 3))
        {
            if (GetProcessDpiAwareness(0, out int awareness) < 0)
            {
                return HighDpiMode.DpiUnaware;
            }

            return awareness switch
            {
                1 => HighDpiMode.SystemAware,
                2 => HighDpiMode.PerMonitor,
                _ => HighDpiMode.DpiUnaware,
            };
        }

        return IsProcessDPIAware() != 0 ? HighDpiMode.SystemAware : HighDpiMode.DpiUnaware;
    }

    internal static bool SetProcessMode(HighDpiMode mode)
    {
        if (!OperatingSystem.IsWindows())
        {
            return false;
        }

        // Match the .NET 10 WinForms fallback policy when a Windows release cannot
        // represent the requested awareness mode. Always return the native result.
        if (OperatingSystem.IsWindowsVersionAtLeast(10, 0, 15063))
        {
            nint context = mode switch
            {
                HighDpiMode.SystemAware => s_systemAwareContext,
                HighDpiMode.PerMonitor => s_perMonitorContext,
                HighDpiMode.PerMonitorV2 => IsValidDpiAwarenessContext(s_perMonitorV2Context) != 0
                    ? s_perMonitorV2Context
                    : s_perMonitorContext,
                HighDpiMode.DpiUnawareGdiScaled when OperatingSystem.IsWindowsVersionAtLeast(10, 0, 17763)
                    => s_unawareGdiScaledContext,
                _ => s_unawareContext,
            };

            // ERROR_ACCESS_DENIED (including manifest/host initialization and repeated
            // calls) is a failure even when the current mode matches the requested mode.
            return SetProcessDpiAwarenessContext(context) != 0;
        }

        if (OperatingSystem.IsWindowsVersionAtLeast(6, 3))
        {
            int awareness = mode switch
            {
                HighDpiMode.SystemAware => 1,
                HighDpiMode.PerMonitor or HighDpiMode.PerMonitorV2 => 2,
                _ => 0,
            };

            return SetProcessDpiAwareness(awareness) >= 0;
        }

        // On older Windows the only setter enables system awareness. Like WinForms,
        // requests for either unaware mode require no native change on those releases.
        return mode is HighDpiMode.DpiUnaware or HighDpiMode.DpiUnawareGdiScaled
            || SetProcessDPIAware() != 0;
    }

    [LibraryImport("user32.dll")]
    private static partial nint GetThreadDpiAwarenessContext();

    [LibraryImport("user32.dll")]
    private static partial int AreDpiAwarenessContextsEqual(nint firstContext, nint secondContext);

    [LibraryImport("user32.dll")]
    private static partial int IsValidDpiAwarenessContext(nint context);

    [LibraryImport("user32.dll", SetLastError = true)]
    private static partial int SetProcessDpiAwarenessContext(nint context);

    [LibraryImport("shcore.dll")]
    private static partial int GetProcessDpiAwareness(nint process, out int awareness);

    [LibraryImport("shcore.dll")]
    private static partial int SetProcessDpiAwareness(int awareness);

    [LibraryImport("user32.dll")]
    private static partial int IsProcessDPIAware();

    [LibraryImport("user32.dll", SetLastError = true)]
    private static partial int SetProcessDPIAware();
}
