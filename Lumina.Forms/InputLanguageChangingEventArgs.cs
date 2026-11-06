// Adapted from dotnet/winforms v10.0.11, licensed under the MIT license.
using System.ComponentModel;
using System.Globalization;

namespace Lumina.Forms;

/// <summary>Provides data for the <see cref="Form.InputLanguageChanging"/> event.</summary>
public class InputLanguageChangingEventArgs : CancelEventArgs
{
    /// <summary>Initializes event data for an installed culture and system character-set support.</summary>
    public InputLanguageChangingEventArgs(CultureInfo culture, bool sysCharSet)
    {
        InputLanguage = InputLanguage.FromCulture(culture)
            ?? throw new ArgumentException($"No installed input language corresponds to culture '{culture}'.", nameof(culture));
        Culture = culture;
        SysCharSet = sysCharSet;
    }

    /// <summary>Initializes event data for an input language and system character-set support.</summary>
    public InputLanguageChangingEventArgs(InputLanguage inputLanguage, bool sysCharSet)
    {
        ArgumentNullException.ThrowIfNull(inputLanguage);
        InputLanguage = inputLanguage;
        Culture = inputLanguage.Culture;
        SysCharSet = sysCharSet;
    }

    /// <summary>Gets the requested input language.</summary>
    public InputLanguage InputLanguage { get; }

    /// <summary>Gets the culture associated with the requested input language.</summary>
    public CultureInfo Culture { get; }

    /// <summary>Gets whether the system default font supports the requested character set.</summary>
    public bool SysCharSet { get; }
}
