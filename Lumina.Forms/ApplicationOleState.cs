// Adapted from dotnet/winforms v10.0.11 Application.ThreadContext.cs.
// Licensed to the .NET Foundation under the MIT license.

using System.Runtime.InteropServices;

namespace Lumina.Forms;

/// <summary>
/// Keeps the OLE reference acquired by this library on the calling thread.
/// </summary>
internal static class ApplicationOleState
{
    private const int RpcEChangedMode = unchecked((int)0x80010106);

    [ThreadStatic]
    private static bool t_initialized;

    [ThreadStatic]
    private static bool t_externalMta;

    internal static ApartmentState OleRequired()
    {
        if (!OperatingSystem.IsWindows())
        {
            throw new PlatformNotSupportedException("OLE initialization requires Windows.");
        }

        if (!t_initialized)
        {
            int result = Win32.OleInitialize(0);
            if (result == RpcEChangedMode)
            {
                // No reference was acquired when an existing MTA prevented STA
                // initialization. Preserve that cached result until thread cleanup.
                t_externalMta = true;
            }
            else
            {
                // S_OK and S_FALSE both acquire one reference. Other failures
                // acquire none and must not be cached as a successful STA.
                Marshal.ThrowExceptionForHR(result);
                t_externalMta = false;
            }

            t_initialized = true;
        }

        return t_externalMta ? ApartmentState.MTA : ApartmentState.STA;
    }

    internal static void ReleaseCurrentThread()
    {
        if (!t_initialized)
        {
            return;
        }

        bool releaseReference = !t_externalMta;
        t_initialized = false;
        t_externalMta = false;

        if (releaseReference)
        {
            // OleUninitialize may pump messages. Clear our state before entering
            // it, and only release the reference owned by this calling thread.
            Win32.OleUninitialize();
        }
    }
}
