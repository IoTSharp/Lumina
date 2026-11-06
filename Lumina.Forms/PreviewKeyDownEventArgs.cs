namespace Lumina.Forms;

/// <summary>
/// Provides data for the <see cref="Control.PreviewKeyDown"/> event.
/// </summary>
public sealed class PreviewKeyDownEventArgs : EventArgs
{
    /// <summary>Initializes preview data for the specified key and modifiers.</summary>
    public PreviewKeyDownEventArgs(Keys keyData) => KeyData = keyData;

    /// <summary>Gets or sets whether the key should be treated as input by the control.</summary>
    public bool IsInputKey { get; set; }

    /// <summary>Gets the key code and modifier flags.</summary>
    public Keys KeyData { get; }

    /// <summary>Gets the key code without modifier flags.</summary>
    public Keys KeyCode => KeyData & Keys.KeyCode;

    /// <summary>Gets the modifier flags.</summary>
    public Keys Modifiers => KeyData & Keys.Modifiers;

    /// <summary>Gets whether ALT is pressed.</summary>
    public bool Alt => (Modifiers & Keys.Alt) != Keys.None;

    /// <summary>Gets whether CTRL is pressed.</summary>
    public bool Control => (Modifiers & Keys.Control) != Keys.None;

    /// <summary>Gets whether SHIFT is pressed.</summary>
    public bool Shift => (Modifiers & Keys.Shift) != Keys.None;
}

/// <summary>Represents the method that handles a preview key-down event.</summary>
public delegate void PreviewKeyDownEventHandler(object? sender, PreviewKeyDownEventArgs e);
