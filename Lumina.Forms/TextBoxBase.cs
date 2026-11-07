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
[DefaultEvent(nameof(TextChanged))]
[DefaultBindingProperty(nameof(Text))]
public abstract class TextBoxBase : Control
{
    private static readonly object s_acceptsTabChangedEvent = new();
    private static readonly object s_borderStyleChangedEvent = new();
    private static readonly object s_hideSelectionChangedEvent = new();
    private static readonly object s_modifiedChangedEvent = new();
    private static readonly object s_multilineChangedEvent = new();
    private static readonly object s_readOnlyChangedEvent = new();
    private static Keys[]? s_shortcutsToDisable;

    private bool _multiline;
    private bool _readOnly;
    private bool _acceptsTab;
    private bool _hideSelection = true;
    private bool _wordWrap = true;
    private bool _shortcutsEnabled = true;
    private BorderStyle _borderStyle = BorderStyle.Fixed3D;
    private int _maxLength = 32767;
    private int _selectionStart;
    private int _selectionLength;
    private bool _restoreSelectionOnHandleCreated;
    private bool _nativeModified;
    private bool _creatingHandle;
    private bool _codeUpdateText;
    private bool _scrollToCaretOnHandleCreated;
    private bool _autoSize = true;
    private int _requestedHeight;
    private bool _integralHeightAdjust;
    private bool _doubleClickFired;

    internal TextBoxBase()
    {
        SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick
            | ControlStyles.UseTextForAccessibility | ControlStyles.UserPaint, false);
        SetStyle(ControlStyles.FixedHeight, true);
        CachePreferredSize = true;
        _requestedHeight = Height;
    }

    /// <summary>Gets or sets whether a single-line text box follows its font height.</summary>
    [Category("Behavior")]
    [DefaultValue(true)]
    [Localizable(true)]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override bool AutoSize
    {
        get => _autoSize;
        set
        {
            if (_autoSize == value) { return; }
            // Text boxes enforce their legacy height contract themselves. The
            // layout engine's separate AutoSize state must remain unchanged.
            _autoSize = value;
            if (!Multiline)
            {
                SetStyle(ControlStyles.FixedHeight, value);
                AdjustHeight(false);
            }
            OnAutoSizeChanged(EventArgs.Empty);
        }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? AutoSizeChanged
    {
        add => base.AutoSizeChanged += value;
        remove => base.AutoSizeChanged -= value;
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override Image? BackgroundImage
    {
        get => base.BackgroundImage;
        set => base.BackgroundImage = value;
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? BackgroundImageChanged
    {
        add => base.BackgroundImageChanged += value;
        remove => base.BackgroundImageChanged -= value;
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override ImageLayout BackgroundImageLayout
    {
        get => base.BackgroundImageLayout;
        set => base.BackgroundImageLayout = value;
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? BackgroundImageLayoutChanged
    {
        add => base.BackgroundImageLayoutChanged += value;
        remove => base.BackgroundImageLayoutChanged -= value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected override bool DoubleBuffered
    {
        get => base.DoubleBuffered;
        set => base.DoubleBuffered = value;
    }

    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    public new event EventHandler? Click
    {
        add => base.Click += value;
        remove => base.Click -= value;
    }

    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    public new event MouseEventHandler? MouseClick
    {
        add => base.MouseClick += value;
        remove => base.MouseClick -= value;
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event PaintEventHandler? Paint
    {
        add => base.Paint += value;
        remove => base.Paint -= value;
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Padding Padding
    {
        get => base.Padding;
        set => base.Padding = value;
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Layout")]
    public new event EventHandler? PaddingChanged
    {
        add => base.PaddingChanged += value;
        remove => base.PaddingChanged -= value;
    }

    /// <summary>Gets the preferred height for a single line, independently of wrapping.</summary>
    [Category("Layout")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int PreferredHeight => FontHeight + (_borderStyle == BorderStyle.None
        ? 0 : Win32.GetBorderSizeForDpi(DeviceDpiInternal).Height * 4 + 3);

    /// <inheritdoc />
    protected override Size DefaultSize => new(100, PreferredHeight);

    // Preserve the existing convenience constructor's raw initial state. Using
    // the public setters here would raise notifications and may create an EDIT
    // while the concrete control is still being constructed.
    private protected void InitializeEditState(bool multiline, bool readOnly)
    {
        _multiline = multiline;
        _readOnly = readOnly;
        SetStyle(ControlStyles.FixedHeight, !multiline && _autoSize);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the text box is multi-line.
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(false)]
    [Localizable(true)]
    [RefreshProperties(RefreshProperties.All)]
    public virtual bool Multiline
    {
        get => _multiline;
        set
        {
            if (_multiline == value) { return; }
            using LayoutTransaction transaction = new(AutoSize ? Parent : null, this, nameof(Multiline));
            _multiline = value;
            SetStyle(ControlStyles.FixedHeight, !value && AutoSize);
            RecreateHandle();
            AdjustHeight(false);
            OnMultilineChanged(EventArgs.Empty);
        }
    }

    /// <summary>Occurs when the <see cref="Multiline"/> property changes.</summary>
    [Category("Property Changed")]
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

    /// <summary>Gets or sets whether the standard editing shortcuts are enabled.</summary>
    [Category("Behavior")]
    [DefaultValue(true)]
    public virtual bool ShortcutsEnabled
    {
        get => _shortcutsEnabled;
        set
        {
            s_shortcutsToDisable ??=
            [
                Keys.Control | Keys.Z, Keys.Control | Keys.C, Keys.Control | Keys.X,
                Keys.Control | Keys.V, Keys.Control | Keys.A, Keys.Control | Keys.L,
                Keys.Control | Keys.R, Keys.Control | Keys.E, Keys.Control | Keys.Y,
                Keys.Control | Keys.Back, Keys.Control | Keys.Delete,
                Keys.Shift | Keys.Delete, Keys.Shift | Keys.Insert, Keys.Control | Keys.J
            ];
            _shortcutsEnabled = value;
        }
    }

    /// <inheritdoc />
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        bool handled = base.ProcessCmdKey(ref msg, keyData);
        if (!ShortcutsEnabled && s_shortcutsToDisable is not null)
        {
            foreach (Keys shortcut in s_shortcutsToDisable)
            {
                if (keyData == shortcut || keyData == (shortcut | Keys.Shift))
                {
                    return true;
                }
            }
        }

        if (_readOnly && keyData is (Keys.Control | Keys.L) or (Keys.Control | Keys.R)
            or (Keys.Control | Keys.E) or (Keys.Control | Keys.J))
        {
            return true;
        }

        if (!ReadOnly && keyData is (Keys.Control | Keys.Back) or (Keys.Control | Keys.Shift | Keys.Back))
        {
            if (SelectionLength != 0)
            {
                SetSelectedTextInternal(string.Empty, clearUndo: false);
            }
            else if (SelectionStart != 0)
            {
                int boundaryStart = ClientUtils.GetWordBoundaryStart(Text, SelectionStart);
                int length = SelectionStart - boundaryStart;
                BeginUpdateInternal();
                SelectionStart = boundaryStart;
                SelectionLength = length;
                EndUpdateInternal();
                SetSelectedTextInternal(string.Empty, clearUndo: false);
            }

            return true;
        }

        return handled;
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
    [Category("Behavior")]
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
    [Category("Property Changed")]
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
    [Category("Appearance")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
    [Category("Appearance")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

    [Browsable(false)]
    public virtual int TextLength => IsHandleCreated ? Win32.GetWindowTextLengthW(NativeHandle) : Text.Length;

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

    /// <summary>Clears all text from the text box control.</summary>
    public void Clear()
    {
        Text = null;
    }

    /// <summary>Copies the current selection in the text box to the Clipboard.</summary>
    public void Copy()
    {
        _ = Win32.SendMessageW(Handle, Win32.WM_COPY, 0, 0);
    }

    /// <summary>Moves the current selection in the text box to the Clipboard.</summary>
    public void Cut()
    {
        _ = Win32.SendMessageW(Handle, Win32.WM_CUT, 0, 0);
    }

    /// <summary>Replaces the current selection with the contents of the Clipboard.</summary>
    public void Paste()
    {
        _ = Win32.SendMessageW(Handle, Win32.WM_PASTE, 0, 0);
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
                style |= Win32.ES_MULTILINE;
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
            SetStyle(ControlStyles.ApplyThemingImplicitly, true);
            CreateParams parameters = base.CreateParams;
            parameters.Style &= ~WsBorder;
            parameters.ExStyle &= ~(int)Win32.WS_EX_CLIENTEDGE;
            if (_borderStyle == BorderStyle.FixedSingle) { parameters.Style |= WsBorder; }
            if (_borderStyle == BorderStyle.Fixed3D) { parameters.ExStyle |= (int)Win32.WS_EX_CLIENTEDGE; }
            return parameters;
        }
    }

    /// <inheritdoc />
    protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
    {
        if (!_integralHeightAdjust && height != Height) { _requestedHeight = height; }
        if (_autoSize && !_multiline) { height = PreferredHeight; }
        base.SetBoundsCore(x, y, width, height, specified);
    }

    private void AdjustHeight(bool returnIfAnchored)
    {
        if (returnIfAnchored && (Anchor & (AnchorStyles.Top | AnchorStyles.Bottom))
            == (AnchorStyles.Top | AnchorStyles.Bottom)) { return; }

        int savedHeight = _requestedHeight;
        try
        {
            if (_autoSize && !_multiline)
            {
                Height = PreferredHeight;
            }
            else
            {
                if (_multiline) { Height = Math.Max(savedHeight, PreferredHeight + 2); }
                _integralHeightAdjust = true;
                try { Height = savedHeight; }
                finally { _integralHeightAdjust = false; }
            }
        }
        finally { _requestedHeight = savedHeight; }
    }

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

    /// <summary>Returns the physical line containing the specified character index.</summary>
    public virtual int GetLineFromCharIndex(int index)
        => (int)Win32.SendMessageW(Handle, Win32.EM_LINEFROMCHAR, (nint)index, 0);

    /// <summary>Returns the first character index of the specified physical line.</summary>
    public int GetFirstCharIndexFromLine(int lineNumber)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(lineNumber);
        return (int)Win32.SendMessageW(Handle, Win32.EM_LINEINDEX, (nint)lineNumber, 0);
    }

    /// <summary>Returns the first character index of the line containing the caret.</summary>
    public int GetFirstCharIndexOfCurrentLine()
        => (int)Win32.SendMessageW(Handle, Win32.EM_LINEINDEX, (nint)(-1), 0);

    /// <summary>Returns the character nearest to the given point.</summary>
    public virtual char GetCharFromPosition(Point pt)
    {
        string text = Text;
        int index = GetCharIndexFromPosition(pt);
        return index < 0 || index >= text.Length ? '\0' : text[index];
    }

    /// <summary>Returns the index of the character nearest to the given point.</summary>
    public virtual int GetCharIndexFromPosition(Point pt)
    {
        int index = Win32.LowWord(Win32.SendMessageW(Handle, Win32.EM_CHARFROMPOS, 0, Win32.MakeLParam(pt.X, pt.Y)));
        if (index < 0)
        {
            index = 0;
        }
        else
        {
            string text = Text;
            if (index >= text.Length)
            {
                index = Math.Max(text.Length - 1, 0);
            }
        }

        return index;
    }

    /// <summary>Returns the location of the character at the given index.</summary>
    public virtual Point GetPositionFromCharIndex(int index)
    {
        if (index < 0 || index >= Text.Length)
        {
            return Point.Empty;
        }

        nint position = Win32.SendMessageW(Handle, Win32.EM_POSFROMCHAR, index, 0);
        return new Point(unchecked((short)Win32.LowWord(position)), unchecked((short)Win32.HighWord(position)));
    }

    /// <summary>
    /// Scrolls the text box so that the caret is visible.
    /// </summary>
    public void ScrollToCaret()
    {
        if (!IsHandleCreated)
        {
            _scrollToCaretOnHandleCreated = true;
            return;
        }

        if (string.IsNullOrEmpty(WindowText)) { return; }
        _ = Win32.SendMessageW(NativeHandle, Win32.EM_SCROLLCARET, 0, 0);
    }

    // The standard measurement differs from PreferredHeight: multiline text,
    // wrapping, padding and the requested constraints all participate here.
    internal override Size GetPreferredSizeCore(Size proposedConstraints)
    {
        Size bordersAndPadding = SizeFromClientSize(Size.Empty) + Padding.Size;
        if (BorderStyle != BorderStyle.None)
        {
            bordersAndPadding += new Size(0, 3);
        }
        if (BorderStyle == BorderStyle.FixedSingle)
        {
            bordersAndPadding += new Size(2, 2);
        }

        proposedConstraints -= bordersAndPadding;
        TextFormatFlags format = TextFormatFlags.NoPrefix;
        if (!Multiline)
        {
            format |= TextFormatFlags.SingleLine;
        }
        else if (WordWrap)
        {
            format |= TextFormatFlags.WordBreak;
        }

        Size textSize = TextRenderer.MeasureText(Text, Font, proposedConstraints, format);
        textSize.Height = Math.Max(textSize.Height, FontHeight);
        return textSize + bordersAndPadding;
    }

    /// <inheritdoc />
    protected override void OnTextChanged(EventArgs e)
    {
        // Text changes clear measurement without relaying out the parent.
        ClearPreferredSizeCache();
        base.OnTextChanged(e);
    }

    /// <inheritdoc />
    protected override void OnFontChanged(EventArgs e)
    {
        base.OnFontChanged(e);
        AdjustHeight(false);
    }

    /// <inheritdoc />
    protected override void OnMouseUp(MouseEventArgs mevent)
    {
        if (mevent is not null && mevent.Button == MouseButtons.Left)
        {
            if (!ValidationCancelled)
            {
                Point screen = PointToScreen(mevent.Location);
                if (Win32.WindowFromPoint(new Win32.POINT { x = screen.X, y = screen.Y }) == NativeHandle)
                {
                    if (!_doubleClickFired)
                    {
                        OnClick(mevent);
                        OnMouseClick(mevent);
                    }
                    else
                    {
                        _doubleClickFired = false;
                        OnDoubleClick(mevent);
                        OnMouseDoubleClick(mevent);
                    }
                }
            }

            _doubleClickFired = false;
        }

        base.OnMouseUp(mevent!);
    }

    /// <inheritdoc />
    protected override void OnPaddingChanged(EventArgs e)
    {
        base.OnPaddingChanged(e);
        AdjustHeight(false);
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
        if (message == Win32.WM_CONTEXTMENU && !ShortcutsEnabled)
        {
            if (ContextMenuStrip is not null)
            {
                Point client = lParam == (nint)(-1)
                    ? new Point(Width / 2, Height / 2)
                    : PointToClient(new Point(unchecked((short)(long)lParam),
                        unchecked((short)((long)lParam >> 16))));
                if (ClientRectangle.Contains(client))
                {
                    _ = TryShowAttachedContextMenuAtScreenPoint(NativeHandle, PointToScreen(client));
                }
            }
            result = 0;
            return true;
        }

        if (message is Win32.WM_KEYUP or Win32.WM_LBUTTONUP)
        {
            _ = UpdateSelectionFromHandle();
        }

        return base.HandleWindowMessage(message, wParam, lParam, out result);
    }

    /// <inheritdoc />
    protected override void WndProc(ref Message m)
    {
        if (m.Msg == Win32.WM_LBUTTONDBLCLK) { _doubleClickFired = true; }
        base.WndProc(ref m);
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
        ClearPreferredSizeCache();
        AdjustHeight(true);
        UpdateMaxLength();
        if (_nativeModified)
        {
            _ = Win32.SendMessageW(NativeHandle, Win32.EM_SETMODIFY, (nint)1, 0);
        }
        if (_scrollToCaretOnHandleCreated)
        {
            ScrollToCaret();
            _scrollToCaretOnHandleCreated = false;
        }
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

    internal void RestoreSelectionOnHandle()
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
