namespace Lumina.Forms;

/// <summary>
/// Provides data for keyboard events raised by a control.
/// </summary>
public class KeyEventArgs : EventArgs
{
    private bool _suppressKeyPress;

    /// <summary>
    /// Initializes keyboard event data for the specified key and modifiers.
    /// </summary>
    /// <param name="keyData">The key code and modifier flags.</param>
    public KeyEventArgs(Keys keyData) => KeyData = keyData;

    /// <summary>Gets a value indicating whether the ALT key was pressed.</summary>
    public bool Alt => (Modifiers & Keys.Alt) != Keys.None;

    /// <summary>Gets a value indicating whether the CTRL key was pressed.</summary>
    public bool Control => (Modifiers & Keys.Control) != Keys.None;

    /// <summary>Gets or sets a value indicating whether the event was handled.</summary>
    public bool Handled { get; set; }

    /// <summary>Gets the key code for the event.</summary>
    public Keys KeyCode => KeyData & Keys.KeyCode;

    /// <summary>Gets the key code and modifier flags for the event.</summary>
    public Keys KeyData { get; }

    /// <summary>Gets the integer value of <see cref="KeyCode"/>.</summary>
    public int KeyValue => (int)KeyCode;

    /// <summary>Gets the modifier flags for the event.</summary>
    public Keys Modifiers => KeyData & Keys.Modifiers;

    /// <summary>Gets a value indicating whether the SHIFT key was pressed.</summary>
    public bool Shift => (Modifiers & Keys.Shift) != Keys.None;

    /// <summary>
    /// Gets or sets a value indicating whether the control should suppress the
    /// corresponding character message and mark the key event as handled.
    /// </summary>
    public bool SuppressKeyPress
    {
        get => _suppressKeyPress;
        set
        {
            _suppressKeyPress = value;
            Handled = value;
        }
    }
}

/// <summary>
/// Provides data for a control's character input event.
/// </summary>
public class KeyPressEventArgs : EventArgs
{
    /// <summary>Initializes character input event data.</summary>
    /// <param name="keyChar">The character represented by the message.</param>
    public KeyPressEventArgs(char keyChar) => KeyChar = keyChar;

    /// <summary>Gets or sets the character represented by the event.</summary>
    public char KeyChar { get; set; }

    /// <summary>Gets or sets a value indicating whether the event was handled.</summary>
    public bool Handled { get; set; }
}

/// <summary>Represents the method that handles a keyboard event.</summary>
/// <param name="sender">The event source.</param>
/// <param name="e">The keyboard event data.</param>
public delegate void KeyEventHandler(object? sender, KeyEventArgs e);

/// <summary>Represents the method that handles a character input event.</summary>
/// <param name="sender">The event source.</param>
/// <param name="e">The character input event data.</param>
public delegate void KeyPressEventHandler(object? sender, KeyPressEventArgs e);
