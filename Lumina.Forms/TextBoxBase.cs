// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// Portions adapted from dotnet/winforms v10.0.11 for Lumina.Forms.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Lumina.Forms;

/// <summary>
/// Provides the shared native editing behavior for text controls.
/// </summary>
public abstract class TextBoxBase : Control
{
    private static readonly object s_acceptsTabChangedEvent = new();
    private static readonly object s_borderStyleChangedEvent = new();
    private static readonly object s_hideSelectionChangedEvent = new();
    private static readonly object s_modifiedChangedEvent = new();
    private static readonly object s_multilineChangedEvent = new();
    private static readonly object s_readOnlyChangedEvent = new();

    private bool _multiline;
    private bool _readOnly;
    private bool _acceptsTab;
    private bool _hideSelection = true;
    private bool _wordWrap = true;
    private BorderStyle _borderStyle = BorderStyle.Fixed3D;
    private int _maxLength = 32767;
    private int _selectionStart;
    private int _selectionLength;
    private bool _restoreSelectionOnHandleCreated;
    private bool _nativeModified;
    private bool _creatingHandle;
    private bool _codeUpdateText;

    internal TextBoxBase()
    {
        SetStyle(ControlStyles.UserPaint, false);
    }

    // Preserve the existing convenience constructor's raw initial state. Using
    // the public setters here would raise notifications and may create an EDIT
    // while the concrete control is still being constructed.
    private protected void InitializeEditState(bool multiline, bool readOnly)
    {
        _multiline = multiline;
        _readOnly = readOnly;
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

    /// <summary>Gets or sets whether a multiline text box accepts TAB characters.</summary>
    [Category("Behavior")]
    [DefaultValue(false)]
    public bool AcceptsTab
    {
        get => _acceptsTab;
        set
        {
            if (_acceptsTab == value) { return; }
            _acceptsTab = value;
            OnAcceptsTabChanged(EventArgs.Empty);
        }
    }

    /// <summary>Occurs when the <see cref="AcceptsTab"/> property changes.</summary>
    [Category("Property Changed")]
    public event EventHandler? AcceptsTabChanged
    {
        add => Events.AddHandler(s_acceptsTabChangedEvent, value);
        remove => Events.RemoveHandler(s_acceptsTabChangedEvent, value);
    }

    /// <summary>Gets or sets the border type of the text box.</summary>
    [Category("Appearance")]
    [DefaultValue(BorderStyle.Fixed3D)]
    [DispId(-504)]
    public BorderStyle BorderStyle
    {
        get => _borderStyle;
        set
        {
            if (_borderStyle == value) { return; }
            if ((uint)value > (uint)BorderStyle.Fixed3D)
            {
                throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(BorderStyle));
            }

            _borderStyle = value;
            UpdateStyles();
            RecreateHandle();
            using LayoutTransaction transaction = new(AutoSize ? Parent : null, this, nameof(BorderStyle));
            OnBorderStyleChanged(EventArgs.Empty);
        }
    }

    /// <summary>Occurs when the <see cref="BorderStyle"/> property changes.</summary>
    [Category("Property Changed")]
    public event EventHandler? BorderStyleChanged
    {
        add => Events.AddHandler(s_borderStyleChangedEvent, value);
        remove => Events.RemoveHandler(s_borderStyleChangedEvent, value);
    }

    /// <summary>Gets or sets whether selected text is hidden when the control loses focus.</summary>
    [Category("Behavior")]
    [DefaultValue(true)]
    public bool HideSelection
    {
        get => _hideSelection;
        set
        {
            if (_hideSelection == value) { return; }
            _hideSelection = value;
            RecreateHandle();
            OnHideSelectionChanged(EventArgs.Empty);
        }
    }

    /// <summary>Occurs when the <see cref="HideSelection"/> property changes.</summary>
    [Category("Property Changed")]
    public event EventHandler? HideSelectionChanged
    {
        add => Events.AddHandler(s_hideSelectionChangedEvent, value);
        remove => Events.RemoveHandler(s_hideSelectionChangedEvent, value);
    }

    /// <summary>Gets or sets whether the text box contents have been modified.</summary>
    [Category("Behavior")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Modified
    {
        get
        {
            if (NativeHandle == 0) { return _nativeModified; }
            bool current = Win32.SendMessageW(NativeHandle, Win32.EM_GETMODIFY, 0, 0) != 0;
            if (_nativeModified != current)
            {
                _nativeModified = current;
                OnModifiedChanged(EventArgs.Empty);
            }
            return current;
        }
        set
        {
            // Reading the native state can itself notify before this setter
            // decides whether another change is required.
            if (Modified == value) { return; }
            if (NativeHandle != 0)
            {
                _ = Win32.SendMessageW(NativeHandle, Win32.EM_SETMODIFY, value ? (nint)1 : 0, 0);
            }
            _nativeModified = value;
            OnModifiedChanged(EventArgs.Empty);
        }
    }

    /// <summary>Occurs when the <see cref="Modified"/> property changes.</summary>
    [Category("Property Changed")]
    public event EventHandler? ModifiedChanged
    {
        add => Events.AddHandler(s_modifiedChangedEvent, value);
        remove => Events.RemoveHandler(s_modifiedChangedEvent, value);
    }

    /// <inheritdoc />
    protected override bool IsInputKey(Keys keyData)
    {
        if ((keyData & Keys.Alt) == 0)
        {
            switch (keyData & Keys.KeyCode)
            {
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
    [DefaultValue(false)]
    [RefreshProperties(RefreshProperties.Repaint)]
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
            OnReadOnlyChanged(EventArgs.Empty);
            VerifyImeRestrictedModeChanged();
        }
    }

    /// <summary>Occurs when the <see cref="ReadOnly"/> property changes.</summary>
    public event EventHandler? ReadOnlyChanged
    {
        add => Events.AddHandler(s_readOnlyChangedEvent, value);
        remove => Events.RemoveHandler(s_readOnlyChangedEvent, value);
    }

    /// <inheritdoc />
    protected override bool CanEnableIme => !(ReadOnly || PasswordProtect) && base.CanEnableIme;

    private protected virtual bool PasswordProtect => false;

    /// <inheritdoc />
    protected override ImeMode ImeModeBase
    {
        get => (DesignMode || CanEnableIme) ? base.ImeModeBase : ImeMode.Disable;
        set => base.ImeModeBase = value;
    }

    /// <summary>Gets or sets whether a multiline text box wraps words to the next line.</summary>
    [Category("Behavior")]
    [Localizable(true)]
    [DefaultValue(true)]
    public bool WordWrap
    {
        get => _wordWrap;
        set
        {
            using LayoutTransaction transaction = new(AutoSize ? Parent : null, this, nameof(WordWrap));
            if (_wordWrap == value) { return; }
            _wordWrap = value;
            RecreateHandle();
        }
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

    /// <inheritdoc />
    [Localizable(true)]
    [AllowNull]
    public override string Text
    {
        get => base.Text;
        set
        {
            if (value != base.Text)
            {
                base.Text = value;
                if (NativeHandle != 0)
                {
                    // The native reset is observed by Modified when it is next
                    // queried; do not replace that observation with a setter.
                    _ = Win32.SendMessageW(NativeHandle, Win32.EM_SETMODIFY, 0, 0);
                }
            }
        }
    }

    internal override string WindowText
    {
        get => base.WindowText;
        set
        {
            value ??= string.Empty;
            if (WindowText.Equals(value, StringComparison.Ordinal)) { return; }
            _codeUpdateText = true;
            try { base.WindowText = value; }
            finally { _codeUpdateText = false; }
        }
    }

    public virtual int TextLength => Text.Length;

    /// <summary>Gets whether the previous editing operation can be undone.</summary>
    [Category("Behavior")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool CanUndo
        => IsHandleCreated && (int)Win32.SendMessageW(NativeHandle, Win32.EM_CANUNDO, 0, 0) != 0;

    /// <summary>Gets or replaces the selected text in the control.</summary>
    [Category("Appearance")]
    [Browsable(false)]
    [AllowNull]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual string SelectedText
    {
        get
        {
            GetSelectionStartAndLength(out int start, out int length);
            return Text.Substring(start, length);
        }
        set => SetSelectedTextInternal(value, true);
    }

    internal virtual void SetSelectedTextInternal(string? text, bool clearUndo)
    {
        if (!IsHandleCreated) { CreateHandle(); }
        text ??= string.Empty;
        _ = Win32.SendMessageW(NativeHandle, Win32.EM_LIMITTEXT, 0, 0);
        if (clearUndo)
        {
            _ = Win32.SendMessageW(NativeHandle, Win32.EM_REPLACESEL, 0, text);
            _ = Win32.SendMessageW(NativeHandle, Win32.EM_SETMODIFY, 0, 0);
            _ = Win32.SendMessageW(NativeHandle, Win32.EM_EMPTYUNDOBUFFER, 0, 0);
        }
        else
        {
            _ = Win32.SendMessageW(NativeHandle, Win32.EM_REPLACESEL, (nint)(-1), text);
        }
        _ = Win32.SendMessageW(NativeHandle, Win32.EM_LIMITTEXT, (nint)_maxLength, 0);
    }

    /// <summary>Gets or sets the lines of text in the text box.</summary>
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [MergableProperty(false)]
    [Localizable(true)]
    [AllowNull]
    public string[] Lines
    {
        get
        {
            string text = Text;
            return text.Length == 0 ? [] : text.Split(["\r\n", "\r", "\n"], StringSplitOptions.None);
        }
        set => Text = value is null ? string.Empty : string.Join(Environment.NewLine, value);
    }

    /// <summary>Gets or sets the maximum number of characters the user can enter.</summary>
    [Category("Behavior")]
    [DefaultValue(32767)]
    [Localizable(true)]
    public virtual int MaxLength
    {
        get => _maxLength;
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value);
            if (_maxLength == value) { return; }
            _maxLength = value;
            UpdateMaxLength();
        }
    }

    internal virtual void UpdateMaxLength()
    {
        if (NativeHandle != 0)
        {
            _ = Win32.SendMessageW(NativeHandle, Win32.EM_LIMITTEXT, (nint)_maxLength, 0);
        }
    }

    public new void Clear()
    {
        Text = string.Empty;
    }

    /// <summary>Clears information about the most recent editing operation.</summary>
    public void ClearUndo()
    {
        if (IsHandleCreated)
        {
            _ = Win32.SendMessageW(NativeHandle, Win32.EM_EMPTYUNDOBUFFER, 0, 0);
        }
    }

    /// <summary>Undoes the last editing operation in the text box.</summary>
    public void Undo()
    {
        _ = Win32.SendMessageW(Handle, Win32.EM_UNDO, 0, 0);
    }

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
            var style = Win32.WS_CHILD | Win32.WS_VISIBLE | Win32.WS_TABSTOP | Win32.ES_LEFT
                | Win32.ES_AUTOHSCROLL | Win32.ES_AUTOVSCROLL;
            if (_multiline)
            {
                style |= Win32.ES_MULTILINE | Win32.ES_WANTRETURN;
                if (_wordWrap) { style &= ~Win32.ES_AUTOHSCROLL; }
            }

            if (_readOnly)
            {
                style |= Win32.ES_READONLY;
            }

            if (!_hideSelection)
            {
                style |= Win32.ES_NOHIDESEL;
            }

            return style;
        }
    }

    // Preserve the existing protected extension; CreateParams below selects
    // the standard BorderStyle after consuming the legacy style projections.
    protected override uint ExStyle => Win32.WS_EX_CLIENTEDGE;

    /// <inheritdoc />
    protected override CreateParams CreateParams
    {
        get
        {
            const int WsBorder = 0x00800000;
            CreateParams parameters = base.CreateParams;
            parameters.Style &= ~WsBorder;
            parameters.ExStyle &= ~(int)Win32.WS_EX_CLIENTEDGE;
            if (_borderStyle == BorderStyle.FixedSingle) { parameters.Style |= WsBorder; }
            if (_borderStyle == BorderStyle.Fixed3D) { parameters.ExStyle |= (int)Win32.WS_EX_CLIENTEDGE; }
            return parameters;
        }
    }

    /// <inheritdoc />
    protected override int GetNativeHeight(int requestedHeight)
        => _multiline ? Math.Max(48, requestedHeight) : Math.Max(24, requestedHeight);

    /// <summary>
    /// Appends text to the current text box contents.
    /// </summary>
    /// <param name="text">The text to append.</param>
    public void AppendText(string? text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return;
        }

        GetSelectionStartAndLength(out int selectionStart, out int selectionLength);
        try
        {
            int endOfText = GetEndPosition();
            SelectInternal(endOfText, endOfText, endOfText);
            SelectedText = text;
        }
        finally
        {
            // Preserve the selection when a docked control has no extent,
            // such as while its parent form is minimized.
            if (Width == 0 || Height == 0)
            {
                Select(selectionStart, selectionLength);
            }
        }
    }

    /// <summary>Gets the append position, including the native edit end marker.</summary>
    internal virtual int GetEndPosition()
        => IsHandleCreated ? TextLength + 1 : TextLength;

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

        SelectInternal(start, length, textLength);
    }

    /// <summary>Performs the selection using an already observed text length.</summary>
    private protected virtual void SelectInternal(int selectionStart, int selectionLength, int textLength)
    {
        if (NativeHandle == 0)
        {
            _selectionStart = selectionStart;
            _selectionLength = selectionLength;
            _restoreSelectionOnHandleCreated = true;
            return;
        }

        AdjustSelection(selectionStart, selectionLength, textLength, out int start, out int end);
        _ = Win32.SendMessageW(NativeHandle, Win32.EM_SETSEL, (nint)start, (nint)end);
        _ = UpdateSelectionFromHandle();
    }

    /// <summary>Selects all text in the text box.</summary>
    public void SelectAll()
    {
        int textLength = TextLength;
        SelectInternal(0, textLength, textLength);
    }

    /// <summary>Sets the selection length to zero.</summary>
    public void DeselectAll()
    {
        SelectionLength = 0;
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
        // Native text submitted by code and initial/recreated window text must
        // not produce another reflected edit notification.
        if (_codeUpdateText || _creatingHandle)
        {
            return notificationCode is Win32.EN_CHANGE or Win32.EN_UPDATE;
        }

        if (notificationCode == Win32.EN_UPDATE)
        {
            _ = Modified;
            return true;
        }

        if (notificationCode != Win32.EN_CHANGE)
        {
            return false;
        }

        return UpdateTextFromHandle();
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
        UpdateMaxLength();
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
            _nativeModified = Modified;
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

    // The standard base has no SelectionChanged event. Concrete controls can
    // retain their existing notification without adding that event to this API.
    internal virtual void OnSelectionChangedInternal(EventArgs e)
    {
    }

    /// <summary>Raises the <see cref="AcceptsTabChanged"/> event.</summary>
    protected virtual void OnAcceptsTabChanged(EventArgs e)
        => (Events[s_acceptsTabChangedEvent] as EventHandler)?.Invoke(this, e);

    /// <summary>Raises the <see cref="BorderStyleChanged"/> event.</summary>
    protected virtual void OnBorderStyleChanged(EventArgs e)
        => (Events[s_borderStyleChangedEvent] as EventHandler)?.Invoke(this, e);

    /// <summary>Raises the <see cref="HideSelectionChanged"/> event.</summary>
    protected virtual void OnHideSelectionChanged(EventArgs e)
        => (Events[s_hideSelectionChangedEvent] as EventHandler)?.Invoke(this, e);

    /// <summary>Raises the <see cref="ModifiedChanged"/> event.</summary>
    protected virtual void OnModifiedChanged(EventArgs e)
        => (Events[s_modifiedChangedEvent] as EventHandler)?.Invoke(this, e);

    /// <summary>Raises the <see cref="MultilineChanged"/> event.</summary>
    protected virtual void OnMultilineChanged(EventArgs e)
        => (Events[s_multilineChangedEvent] as EventHandler)?.Invoke(this, e);

    /// <summary>Raises the <see cref="ReadOnlyChanged"/> event.</summary>
    protected virtual void OnReadOnlyChanged(EventArgs e)
        => (Events[s_readOnlyChangedEvent] as EventHandler)?.Invoke(this, e);

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
        if (raiseEvent) { OnSelectionChangedInternal(EventArgs.Empty); }
        return true;
    }

}
