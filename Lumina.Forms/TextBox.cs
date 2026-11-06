// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// Portions adapted from dotnet/winforms v10.0.11 for Lumina.Forms.

using System.ComponentModel;
using System.Drawing;

namespace Lumina.Forms;

/// <summary>
/// Represents a single-line or multi-line edit control.
/// </summary>
public class TextBox : Control
{
    private static readonly object s_multilineChangedEvent = new();

    private bool _multiline;
    private bool _readOnly;
    private bool _acceptsReturn;
    private bool _acceptsTab;
    private bool _wordWrap = true;
    private string _placeholderText = string.Empty;
    private char _passwordChar;
    private ScrollBars _scrollBars;
    private int _selectionStart;
    private int _selectionLength;
    private bool _restoreSelectionOnHandleCreated;
    private bool _nativeModified;
    private bool _creatingHandle;

    /// <summary>
    /// Initializes a single-line editable text box.
    /// </summary>
    public TextBox()
    {
        SetStyle(ControlStyles.UserPaint, false);
    }

    /// <summary>
    /// Initializes a text box with the requested multi-line and read-only behavior.
    /// </summary>
    /// <param name="multiline">Whether the control should use a multi-line edit window.</param>
    /// <param name="readOnly">Whether the control should reject user edits.</param>
    public TextBox(bool multiline, bool readOnly = false)
    {
        SetStyle(ControlStyles.UserPaint, false);
        _multiline = multiline;
        _readOnly = readOnly;

        if (multiline)
        {
            SetBounds(0, 0, 240, 120);
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the text box is multi-line.
    /// </summary>
    [DefaultValue(false)]
    [Localizable(true)]
    [RefreshProperties(RefreshProperties.All)]
    public virtual bool Multiline
    {
        get => _multiline;
        set
        {
            if (_multiline == value) { return; }
            _multiline = value;
            RecreateHandle();
            OnMultilineChanged(EventArgs.Empty);
        }
    }

    /// <summary>Occurs when the <see cref="Multiline"/> property changes.</summary>
    public event EventHandler? MultilineChanged
    {
        add => Events.AddHandler(s_multilineChangedEvent, value);
        remove => Events.RemoveHandler(s_multilineChangedEvent, value);
    }

    public bool AcceptsReturn
    {
        get => _acceptsReturn;
        set => _acceptsReturn = value;
    }

    public bool AcceptsTab
    {
        get => _acceptsTab;
        set => _acceptsTab = value;
    }

    /// <inheritdoc />
    protected override bool IsInputKey(Keys keyData)
    {
        // Preserve the TextBox and TextBoxBase input-key rules from WinForms
        // v10.0.11 while the standard TextBoxBase hierarchy remains pending.
        if ((keyData & Keys.Alt) == 0)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Enter when Multiline:
                    return AcceptsReturn;
                case Keys.Tab:
                    return Multiline && AcceptsTab && (keyData & Keys.Control) == 0;
                case Keys.Escape when Multiline:
                    return false;
                case Keys.Back when !ReadOnly:
                case Keys.PageUp:
                case Keys.PageDown:
                case Keys.Home:
                case Keys.End:
                    return true;
            }
        }

        return base.IsInputKey(keyData);
    }

    /// <inheritdoc />
    protected override bool ProcessDialogKey(Keys keyData)
    {
        if ((keyData & Keys.KeyCode) == Keys.Tab && AcceptsTab && (keyData & Keys.Control) != 0)
        {
            keyData &= ~Keys.Control;
        }

        return base.ProcessDialogKey(keyData);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the text box is read-only.
    /// </summary>
    public bool ReadOnly
    {
        get => _readOnly;
        set
        {
            if (_readOnly == value) { return; }
            _readOnly = value;
            if (NativeHandle != 0)
            {
                _ = Win32.SendMessageW(NativeHandle, Win32.EM_SETREADONLY, value ? (nint)1 : 0, 0);
            }
            RefreshTheme();
        }
    }

    public string PlaceholderText
    {
        get => _placeholderText;
        set => _placeholderText = value ?? string.Empty;
    }

    public char PasswordChar
    {
        get => _passwordChar;
        set => _passwordChar = value;
    }

    public ScrollBars ScrollBars
    {
        get => _scrollBars;
        set => _scrollBars = value;
    }

    public bool WordWrap
    {
        get => _wordWrap;
        set => _wordWrap = value;
    }

    /// <summary>
    /// Gets the starting position of text selected in the text box.
    /// </summary>
    public int SelectionStart
    {
        get
        {
            GetSelectionStartAndLength(out int start, out _);
            return start;
        }
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value);
            Select(value, SelectionLength);
        }
    }

    /// <summary>
    /// Gets the number of characters selected in the text box.
    /// </summary>
    public virtual int SelectionLength
    {
        get
        {
            GetSelectionStartAndLength(out _, out int length);
            return length;
        }
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value);
            GetSelectionStartAndLength(out int start, out int length);
            if (value != length) { Select(start, value); }
        }
    }

    public int TextLength => Text.Length;

    public string[] Lines
    {
        get => Text.Replace("\r\n", "\n", StringComparison.Ordinal).Split('\n');
        set => Text = value is null ? string.Empty : string.Join(Environment.NewLine, value);
    }

    public new void Clear()
    {
        Text = string.Empty;
    }

    /// <summary>
    /// Occurs when the selection changes.
    /// </summary>
    public event EventHandler? SelectionChanged;

    /// <summary>
    /// Gets or sets the background color of the text box.
    /// </summary>
    public override Color BackColor
    {
        get => ShouldSerializeBackColor() ? base.BackColor : ReadOnly ? SystemColors.Control : SystemColors.Window;
        set => base.BackColor = value;
    }

    /// <summary>
    /// Gets or sets the foreground color of the text box.
    /// </summary>
    public override Color ForeColor
    {
        get => ShouldSerializeForeColor() ? base.ForeColor : SystemColors.WindowText;
        set => base.ForeColor = value;
    }

    internal override bool TryGetExplicitBackColor(out Color color)
    {
        // EDIT uses its own color defaults. Keep those defaults on the semantic
        // palette while honoring explicit values and further derived getters.
        color = BackColor;
        return !LocalBackColor.IsEmpty || color != (ReadOnly ? SystemColors.Control : SystemColors.Window);
    }

    internal override bool TryGetExplicitForeColor(out Color color)
    {
        color = ForeColor;
        return !LocalForeColor.IsEmpty || color != SystemColors.WindowText;
    }

    private protected override ThemeColorSlot DefaultBackgroundSlot
        => ReadOnly ? ThemeColorSlot.Surface : ThemeColorSlot.Control;

    /// <inheritdoc />
    protected override string ClassName => "EDIT";

    /// <inheritdoc />
    protected override uint Style
    {
        get
        {
            var style = Win32.WS_CHILD | Win32.WS_VISIBLE | Win32.WS_TABSTOP | Win32.ES_LEFT;
            if (_multiline)
            {
                style |= Win32.WS_VSCROLL | Win32.ES_MULTILINE | Win32.ES_AUTOVSCROLL | Win32.ES_WANTRETURN;
            }
            else
            {
                style |= Win32.ES_AUTOHSCROLL;
            }

            if (_readOnly)
            {
                style |= Win32.ES_READONLY;
            }

            return style;
        }
    }

    /// <inheritdoc />
    protected override uint ExStyle => Win32.WS_EX_CLIENTEDGE;

    /// <inheritdoc />
    protected override int GetNativeHeight(int requestedHeight)
        => _multiline ? Math.Max(48, requestedHeight) : Math.Max(24, requestedHeight);

    /// <summary>
    /// Appends text to the current text box contents.
    /// </summary>
    /// <param name="value">The text to append.</param>
    public void AppendText(string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (NativeHandle == 0)
        {
            Text += value;
            return;
        }

        _ = Win32.SendMessageW(NativeHandle, Win32.EM_SETSEL, (nint)int.MaxValue, (nint)int.MaxValue);
        _ = Win32.SendMessageW(NativeHandle, Win32.EM_REPLACESEL, 0, value);
        _ = UpdateTextFromHandle();
    }

    /// <summary>
    /// Selects a range of text in the text box.
    /// </summary>
    /// <param name="start">The starting position.</param>
    /// <param name="length">The number of characters to select.</param>
    public void Select(int start, int length)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(start);
        int textLength = TextLength;
        if (start > textLength)
        {
            // A directed selection may extend backwards from beyond the text.
            length = (int)Math.Max(int.MinValue, Math.Min(0L, (long)length + start - textLength));
            start = textLength;
        }

        if (NativeHandle == 0)
        {
            _selectionStart = start;
            _selectionLength = length;
            _restoreSelectionOnHandleCreated = true;
            return;
        }

        AdjustSelection(start, length, textLength, out int selectionStart, out int selectionEnd);
        _ = Win32.SendMessageW(NativeHandle, Win32.EM_SETSEL, (nint)selectionStart, (nint)selectionEnd);
        _ = UpdateSelectionFromHandle();
    }

    /// <summary>
    /// Scrolls the text box so that the caret is visible.
    /// </summary>
    public void ScrollToCaret()
    {
        if (NativeHandle != 0)
        {
            _ = Win32.SendMessageW(NativeHandle, Win32.EM_SCROLLCARET, 0, 0);
        }
    }

    /// <inheritdoc />
    protected override bool OnCommand(int notificationCode)
    {
        if (notificationCode != Win32.EN_CHANGE)
        {
            return false;
        }

        // Initial/recreated window text is not a user edit and must not raise
        // another TextChanged notification while the handle is being created.
        return _creatingHandle || UpdateTextFromHandle();
    }

    /// <inheritdoc />
    protected override bool HandleWindowMessage(uint message, nint wParam, nint lParam, out nint result)
    {
        if (message is Win32.WM_KEYUP or Win32.WM_LBUTTONUP)
        {
            _ = UpdateSelectionFromHandle();
        }

        return base.HandleWindowMessage(message, wParam, lParam, out result);
    }

    /// <inheritdoc />
    protected override void ApplyTheme()
    {
        ApplyNativeThemeState();
        ApplyExplorerTheme();
    }

    /// <inheritdoc />
    protected override void OnHandleCreated()
    {
        base.OnHandleCreated();
        ApplyNativeThemeState();
    }

    /// <inheritdoc />
    protected override void CreateHandle()
    {
        _creatingHandle = true;
        try
        {
            base.CreateHandle();
            RestoreSelectionOnHandle();
        }
        finally { _creatingHandle = false; }
    }

    /// <inheritdoc />
    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        if (NativeHandle == 0) { return; }
        if (_nativeModified)
        {
            _ = Win32.SendMessageW(NativeHandle, Win32.EM_SETMODIFY, (nint)1, 0);
        }
        RestoreSelectionOnHandle();
    }

    /// <inheritdoc />
    protected override void OnHandleDestroyed(EventArgs e)
    {
        if (NativeHandle != 0)
        {
            _nativeModified = Win32.SendMessageW(NativeHandle, Win32.EM_GETMODIFY, 0, 0) != 0;
            _ = UpdateSelectionFromHandle(raiseEvent: false);
            _restoreSelectionOnHandleCreated = true;
        }
        base.OnHandleDestroyed(e);
    }

    private void ApplyNativeThemeState()
    {
        if (NativeHandle == 0)
        {
            return;
        }

        DarkModeNative.ApplyThemeToWindow(NativeHandle, CurrentVisualStyle.IsDarkMode);
    }

    /// <inheritdoc />
    protected override string GetPreferredThemeClass(ResolvedVisualStyle visualStyle)
        => visualStyle.IsDarkMode && OperatingSystem.IsWindowsVersionAtLeast(10, 0, 17763)
            ? "DarkMode_CFD"
            : "CFD";

    /// <inheritdoc />
    protected override string GetFallbackThemeClass(ResolvedVisualStyle visualStyle)
        => base.GetPreferredThemeClass(visualStyle);

    /// <summary>
    /// Raises the <see cref="SelectionChanged"/> event.
    /// </summary>
    protected virtual void OnSelectionChanged(EventArgs e)
    {
        SelectionChanged?.Invoke(this, e);
    }

    /// <summary>Raises the <see cref="MultilineChanged"/> event.</summary>
    protected virtual void OnMultilineChanged(EventArgs e)
        => (Events[s_multilineChangedEvent] as EventHandler)?.Invoke(this, e);

    private void GetSelectionStartAndLength(out int start, out int length)
    {
        if (NativeHandle == 0)
        {
            // Keep the original pending range: changing Text back before
            // creation can make a temporarily out-of-range selection valid.
            AdjustSelection(_selectionStart, _selectionLength, TextLength, out start, out int end);
            length = end - start;
            return;
        }

        _ = UpdateSelectionFromHandle();
        start = _selectionStart;
        length = _selectionLength;
    }

    private static void AdjustSelection(int start, int length, int textLength, out int selectionStart, out int selectionEnd)
    {
        selectionStart = Math.Min(start, textLength);
        selectionEnd = (int)Math.Clamp((long)selectionStart + length, 0L, textLength);
    }

    private void RestoreSelectionOnHandle()
    {
        if (NativeHandle == 0 || !_restoreSelectionOnHandleCreated) { return; }
        _restoreSelectionOnHandleCreated = false;
        AdjustSelection(_selectionStart, _selectionLength, TextLength, out int start, out int end);
        _ = Win32.SendMessageW(NativeHandle, Win32.EM_SETSEL, (nint)start, (nint)end);
        _ = UpdateSelectionFromHandle(raiseEvent: false);
    }

    private bool UpdateSelectionFromHandle(bool raiseEvent = true)
    {
        if (NativeHandle == 0)
        {
            return false;
        }

        _ = Win32.SendMessageW(NativeHandle, Win32.EM_GETSEL, out int start, out int end);
        int normalizedStart = Math.Max(0, start);
        int normalizedLength = Math.Max(0, end - start);

        if (_selectionStart == normalizedStart && _selectionLength == normalizedLength)
        {
            return false;
        }

        _selectionStart = normalizedStart;
        _selectionLength = normalizedLength;
        if (raiseEvent) { OnSelectionChanged(EventArgs.Empty); }
        return true;
    }

}
