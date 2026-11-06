// Adapted from dotnet/winforms v10.0.11 InputLanguage.cs.
// Licensed to the .NET Foundation under the MIT license.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.Win32;

namespace Lumina.Forms;

/// <summary>
/// Identifies a Windows keyboard layout and manages the input language of the current thread.
/// </summary>
public sealed class InputLanguage
{
    private const string KeyboardLayoutsRegistryPath = @"SYSTEM\CurrentControlSet\Control\Keyboard Layouts";
    private const string UserProfileRegistryPath = @"Control Panel\International\User Profile";
    private readonly nint _handle;

    internal InputLanguage(nint handle) => _handle = handle;

    /// <summary>Gets the culture associated with this input language.</summary>
    public CultureInfo Culture => new(LanguageTag);

    /// <summary>Gets or sets the input language of the current thread.</summary>
    [AllowNull]
    public static InputLanguage CurrentInputLanguage
    {
        get
        {
            EnsureWindows();
            // This bounded keyboard service reads the native thread HKL. The
            // WinForms OleRequired initialization side effect requires the
            // application thread-context lifetime, which is still a separate gap.
            return new InputLanguage(Win32.GetKeyboardLayout(0));
        }
        set
        {
            EnsureWindows();
            value ??= DefaultInputLanguage;
            if (Win32.ActivateKeyboardLayout(value.Handle, 0) == 0)
            {
                throw new ArgumentException("inputLanguage is not recognized by the system.", nameof(value));
            }
        }
    }

    /// <summary>Gets the default input language for the system.</summary>
    public static InputLanguage DefaultInputLanguage
    {
        get
        {
            EnsureWindows();
            nint handle = 0;
            Win32.SystemParametersInfoW(Win32.SPI_GETDEFAULTINPUTLANG, 0, ref handle, 0);
            return new InputLanguage(handle);
        }
    }

    /// <summary>Gets the native keyboard-layout handle.</summary>
    public nint Handle => _handle;

    /// <summary>Gets a snapshot of the input languages installed on the system.</summary>
    public static unsafe InputLanguageCollection InstalledInputLanguages
    {
        get
        {
            EnsureWindows();
            int count = Win32.GetKeyboardLayoutList(0, null);
            nint[] handles = new nint[count];
            fixed (nint* pointer = handles)
            {
                Win32.GetKeyboardLayoutList(count, pointer);
            }

            var languages = new InputLanguage[count];
            for (int index = 0; index < count; index++)
            {
                languages[index] = new InputLanguage(handles[index]);
            }

            return new InputLanguageCollection(languages);
        }
    }

    /// <summary>Gets the localized name of the keyboard layout.</summary>
    public string LayoutName
    {
        get
        {
            EnsureWindows();
            using RegistryKey? key = Registry.LocalMachine.OpenSubKey($@"{KeyboardLayoutsRegistryPath}\{LayoutId}");
            return GetLayoutDisplayName(key) ?? key?.GetValue("Layout Text") as string ?? "Unknown layout";
        }
    }

    internal string LayoutId
    {
        get
        {
            int device = (int)(((nuint)_handle >> 16) & 0xffff);
            if ((device & 0xf000) == 0xf000)
            {
                int layoutId = device & 0x0fff;
                EnsureWindows();
                using RegistryKey? key = Registry.LocalMachine.OpenSubKey(KeyboardLayoutsRegistryPath);
                if (key is not null)
                {
                    foreach (string name in key.GetSubKeyNames())
                    {
                        using RegistryKey? subKey = key.OpenSubKey(name);
                        if (subKey?.GetValue("Layout Id") is string value
                            && Convert.ToInt32(value, 16) == layoutId)
                        {
                            return name.ToUpperInvariant();
                        }
                    }
                }
            }
            else if (device == 0)
            {
                device = (int)((nuint)_handle & 0xffff);
            }

            return device.ToString("X8", CultureInfo.InvariantCulture);
        }
    }

    private string LanguageTag
    {
        get
        {
            int languageId = (int)((nuint)_handle & 0xffff);
            if (languageId is 0x2000 or 0x2400 or 0x2800 or 0x2c00)
            {
                EnsureWindows();
                using RegistryKey? key = Registry.CurrentUser.OpenSubKey(UserProfileRegistryPath);
                if (key?.GetValue("Languages") is string[] languages)
                {
                    foreach (string language in languages)
                    {
                        using RegistryKey? subKey = key.OpenSubKey(language);
                        if (subKey?.GetValue("TransientLangId") is int transientId && transientId == languageId)
                        {
                            return language;
                        }
                    }
                }
            }

            return CultureInfo.GetCultureInfo(languageId).Name;
        }
    }

    private static unsafe string? GetLayoutDisplayName(RegistryKey? key)
    {
        if (key is null)
        {
            return null;
        }

        const int ErrorMoreData = 234;
        Span<char> buffer = stackalloc char[1024];
        int result;
        uint bytesRequired;
        fixed (char* pointer = buffer)
        {
            result = Win32.RegLoadMUIStringW(key.Handle.DangerousGetHandle(), "Layout Display Name", pointer,
                (uint)(buffer.Length * sizeof(char)), out bytesRequired, 0, null);
        }

        // The native size is in bytes. At most one larger read is needed; a
        // concurrently changed value falls back to the registered plain name.
        if (result == ErrorMoreData && bytesRequired > buffer.Length * sizeof(char) && bytesRequired <= int.MaxValue)
        {
            buffer = new char[checked((int)((bytesRequired + 1) / sizeof(char)))];
            fixed (char* pointer = buffer)
            {
                result = Win32.RegLoadMUIStringW(key.Handle.DangerousGetHandle(), "Layout Display Name", pointer,
                    (uint)(buffer.Length * sizeof(char)), out bytesRequired, 0, null);
            }
        }

        GC.KeepAlive(key);
        if (result != 0)
        {
            return null;
        }

        return new string(buffer[..Math.Max((int)(bytesRequired / sizeof(char)) - 1, 0)]);
    }

    /// <summary>Returns whether another input language has the same native layout handle.</summary>
    public override bool Equals(object? value) => value is InputLanguage other && _handle == other._handle;

    /// <summary>Returns the installed input language for a culture, or null when it is not installed.</summary>
    public static InputLanguage? FromCulture(CultureInfo culture)
    {
        ArgumentNullException.ThrowIfNull(culture);
        foreach (InputLanguage language in InstalledInputLanguages)
        {
            if (culture.Equals(language.Culture))
            {
                return language;
            }
        }

        return null;
    }

    /// <summary>Returns a hash code derived from the keyboard-layout handle.</summary>
    public override int GetHashCode() => unchecked((int)_handle);

    private static void EnsureWindows()
    {
        if (!OperatingSystem.IsWindows())
        {
            throw new PlatformNotSupportedException("Native input languages require Windows.");
        }
    }
}
