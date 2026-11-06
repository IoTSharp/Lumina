// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// Portions adapted from dotnet/winforms v10.0.11 for Lumina.Forms.

using System.ComponentModel;

namespace Lumina.Forms;

/// <summary>
/// Represents a single-line or multi-line edit control.
/// </summary>
public class TextBox : TextBoxBase
{
    private bool _acceptsReturn;
    private string _placeholderText = string.Empty;
    private char _passwordChar;
    private bool _useSystemPasswordChar;
    private ScrollBars _scrollBars;

    /// <summary>
    /// Initializes a single-line editable text box.
    /// </summary>
    public TextBox()
    {
    }

    /// <summary>
    /// Initializes a text box with the requested multi-line and read-only behavior.
    /// </summary>
    /// <param name="multiline">Whether the control should use a multi-line edit window.</param>
    /// <param name="readOnly">Whether the control should reject user edits.</param>
    public TextBox(bool multiline, bool readOnly = false)
    {
        InitializeEditState(multiline, readOnly);
        if (multiline)
        {
            SetBounds(0, 0, 240, 120);
        }
    }

    public bool AcceptsReturn
    {
        get => _acceptsReturn;
        set => _acceptsReturn = value;
    }

    /// <inheritdoc />
    protected override bool IsInputKey(Keys keyData)
    {
        if ((keyData & Keys.Alt) == 0 && (keyData & Keys.KeyCode) == Keys.Enter && Multiline)
        {
            return AcceptsReturn;
        }

        return base.IsInputKey(keyData);
    }

    private protected override bool PasswordProtect => PasswordChar != '\0';

    public string PlaceholderText
    {
        get => _placeholderText;
        set => _placeholderText = value ?? string.Empty;
    }

    /// <summary>Gets or sets the character used to mask the text box's contents.</summary>
    [DefaultValue('\0')]
    [Localizable(true)]
    [RefreshProperties(RefreshProperties.Repaint)]
    public char PasswordChar
    {
        get
        {
            if (!IsHandleCreated) { CreateHandle(); }
            return (char)Win32.SendMessageW(NativeHandle, Win32.EM_GETPASSWORDCHAR, 0, 0);
        }
        set
        {
            _passwordChar = value;
            if (!_useSystemPasswordChar && IsHandleCreated && PasswordChar != value)
            {
                _ = Win32.SendMessageW(NativeHandle, Win32.EM_SETPASSWORDCHAR, (nint)value, 0);
                VerifyImeRestrictedModeChanged();
                Invalidate();
            }
        }
    }

    /// <summary>Gets or sets whether the system password character takes precedence over <see cref="PasswordChar"/>.</summary>
    [DefaultValue(false)]
    [RefreshProperties(RefreshProperties.Repaint)]
    public bool UseSystemPasswordChar
    {
        get => _useSystemPasswordChar;
        set
        {
            if (_useSystemPasswordChar == value) { return; }
            _useSystemPasswordChar = value;
            // Creating the replacement EDIT applies its actual password mode
            // before the IME restriction is checked by OnHandleCreated.
            RecreateHandle();
        }
    }

    /// <summary>Gets or sets the scroll bars displayed by a multiline text box.</summary>
    [Category("Appearance")]
    [Localizable(true)]
    [DefaultValue(ScrollBars.None)]
    public ScrollBars ScrollBars
    {
        get => _scrollBars;
        set
        {
            if (_scrollBars == value) { return; }
            if ((uint)value > (uint)ScrollBars.Both)
            {
                throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(ScrollBars));
            }
            _scrollBars = value;
            RecreateHandle();
        }
    }

    /// <summary>
    /// Occurs when the selection changes.
    /// </summary>
    public event EventHandler? SelectionChanged;

    /// <inheritdoc />
    protected override uint Style
    {
        get
        {
            uint style = base.Style;
            if (Multiline)
            {
                if ((_scrollBars & ScrollBars.Horizontal) != 0 && !WordWrap)
                {
                    style |= Win32.WS_HSCROLL;
                }
                if ((_scrollBars & ScrollBars.Vertical) != 0)
                {
                    style |= Win32.WS_VSCROLL;
                }
            }
            return style | (_useSystemPasswordChar ? Win32.ES_PASSWORD : 0);
        }
    }

    /// <inheritdoc />
    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        if (NativeHandle == 0) { return; }
        if (_passwordChar != '\0' && !_useSystemPasswordChar)
        {
            _ = Win32.SendMessageW(NativeHandle, Win32.EM_SETPASSWORDCHAR, (nint)_passwordChar, 0);
        }
        VerifyImeRestrictedModeChanged();
    }

    internal override void OnSelectionChangedInternal(EventArgs e) => OnSelectionChanged(e);

    /// <summary>
    /// Raises the <see cref="SelectionChanged"/> event.
    /// </summary>
    protected virtual void OnSelectionChanged(EventArgs e)
    {
        SelectionChanged?.Invoke(this, e);
    }
}
