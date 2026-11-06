// Adapted from dotnet/winforms v10.0.11, licensed under the MIT license.
using System.Globalization;

namespace Lumina.Forms;

/// <summary>Provides data for the <see cref="Form.InputLanguageChanged"/> event.</summary>
public class InputLanguageChangedEventArgs : EventArgs
{
    /// <summary>Initializes event data for an installed culture and character set.</summary>
    public InputLanguageChangedEventArgs(CultureInfo culture, byte charSet)
    {
        InputLanguage = InputLanguage.FromCulture(culture)
            ?? throw new ArgumentException($"No installed input language corresponds to culture '{culture}'.", nameof(culture));
        Culture = culture;
        CharSet = charSet;
    }

    /// <summary>Initializes event data for an input language and character set.</summary>
    public InputLanguageChangedEventArgs(InputLanguage inputLanguage, byte charSet)
    {
        ArgumentNullException.ThrowIfNull(inputLanguage);
        InputLanguage = inputLanguage;
        Culture = inputLanguage.Culture;
        CharSet = charSet;
    }

    /// <summary>Gets the new input language.</summary>
    public InputLanguage InputLanguage { get; }

    /// <summary>Gets the culture associated with the new input language.</summary>
    public CultureInfo Culture { get; }

    /// <summary>Gets the character set associated with the new input language.</summary>
    public byte CharSet { get; }
}
