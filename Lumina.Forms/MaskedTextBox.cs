// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// Mask editing algorithms adapted from dotnet/winforms v10.0.11.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using MaskProvider = System.ComponentModel.MaskedTextProvider;

namespace Lumina.Forms;

/// <summary>Provides single-line native editing backed by the standard mask language provider.</summary>
[DefaultEvent(nameof(MaskInputRejected))]
[DefaultBindingProperty(nameof(Text))]
[DefaultProperty(nameof(Mask))]
public partial class MaskedTextBox : TextBoxBase
{
    // The BCL provider rejects an empty mask. This private property backend is
    // the same non-editing mask used by WinForms; it is never publicly exposed.
    private const string NullMask = "<>";
    private MaskProvider _provider = null!;
    private string _mask = string.Empty;
    private IFormatProvider? _formatProvider;
    private MaskFormat _textMaskFormat = MaskFormat.IncludeLiterals;
    private MaskFormat _cutCopyMaskFormat = MaskFormat.IncludeLiterals;
    private InsertKeyMode _insertKeyMode;
    private bool _insertToggled;
    private bool _beepOnError;
    private bool _hidePromptOnLeave;
    private bool _rejectInputOnFirstFailure;
    private bool _composing;
    private char _passwordChar;
    private bool _useSystemPasswordChar;
    private char _systemPasswordChar;
    private HorizontalAlignment _textAlign;
    private Type? _validatingType;

    /// <summary>Initializes an unrestricted single-line text box.</summary>
    public MaskedTextBox() : this(new MaskProvider(NullMask, CultureInfo.CurrentCulture), string.Empty) { }

    /// <summary>Initializes a text box using a nonempty input mask.</summary>
    public MaskedTextBox(string mask) : this(new MaskProvider(mask, CultureInfo.CurrentCulture), mask) { }

    /// <summary>Initializes the text box with the supplied mask language provider.</summary>
    public MaskedTextBox(MaskProvider maskedTextProvider)
        : this(maskedTextProvider ?? throw new ArgumentNullException(nameof(maskedTextProvider)), maskedTextProvider.Mask) { }

    private MaskedTextBox(MaskProvider provider, string mask)
    {
        _provider = provider;
        _mask = mask;
        _passwordChar = provider.PasswordChar;
        _textMaskFormat = (provider.IncludePrompt ? MaskFormat.IncludePrompt : 0)
            | (provider.IncludeLiterals ? MaskFormat.IncludeLiterals : 0);
        _cutCopyMaskFormat = _textMaskFormat;
        InitializeEditState(multiline: false, readOnly: false);
        RefreshDisplay();
    }

    /// <summary>Gets or sets whether prompt characters can be entered as data.</summary>
    [DefaultValue(true)]
    public bool AllowPromptAsInput
    {
        get => _provider.AllowPromptAsInput;
        set
        {
            if (value != AllowPromptAsInput)
            {
                ReplaceProvider(new MaskProvider(_provider.Mask, Culture, value, PromptChar, EffectivePasswordChar, AsciiOnly), _mask);
            }
        }
    }

    /// <summary>Gets or sets whether input must be ASCII.</summary>
    [DefaultValue(false)]
    public bool AsciiOnly
    {
        get => _provider.AsciiOnly;
        set
        {
            if (value != AsciiOnly)
            {
                ReplaceProvider(new MaskProvider(_provider.Mask, Culture, AllowPromptAsInput, PromptChar, EffectivePasswordChar, value), _mask);
            }
        }
    }

    /// <summary>Gets or sets whether rejecting input plays a system beep.</summary>
    [DefaultValue(false)]
    public bool BeepOnError { get => _beepOnError; set => _beepOnError = value; }

    /// <summary>Masked editing does not retain an undo history.</summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool CanUndo => false;

    /// <summary>Gets or sets the culture used by mask separators and case conversion.</summary>
    public CultureInfo Culture
    {
        get => _provider.Culture;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            if (!Culture.Equals(value))
            {
                ReplaceProvider(new MaskProvider(_provider.Mask, value, AllowPromptAsInput, PromptChar, EffectivePasswordChar, AsciiOnly), _mask);
            }
        }
    }

    /// <summary>Gets or sets formatting used by selected text and clipboard operations.</summary>
    [DefaultValue(MaskFormat.IncludeLiterals)]
    public MaskFormat CutCopyMaskFormat
    {
        get => _cutCopyMaskFormat;
        set => _cutCopyMaskFormat = ValidateMaskFormat(value);
    }

    /// <summary>Gets or sets the provider used when validating the value's type.</summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IFormatProvider? FormatProvider { get => _formatProvider; set => _formatProvider = value; }

    /// <summary>Gets or sets whether an unfocused control hides unassigned prompts.</summary>
    [DefaultValue(false)]
    public bool HidePromptOnLeave
    {
        get => _hidePromptOnLeave;
        set { if (_hidePromptOnLeave != value) { _hidePromptOnLeave = value; RefreshDisplay(); } }
    }

    /// <summary>Gets or sets the keyboard insertion policy.</summary>
    [DefaultValue(InsertKeyMode.Default)]
    public InsertKeyMode InsertKeyMode
    {
        get => _insertKeyMode;
        set
        {
            if ((uint)value > (uint)InsertKeyMode.Overwrite)
                throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(InsertKeyMode));
            bool previous = IsOverwriteMode;
            _insertKeyMode = value;
            if (previous != IsOverwriteMode) { OnIsOverwriteModeChanged(EventArgs.Empty); }
        }
    }

    /// <summary>Gets the effective insertion mode.</summary>
    [Browsable(false)]
    public bool IsOverwriteMode => _mask.Length != 0 && (_insertKeyMode == InsertKeyMode.Overwrite
        || (_insertKeyMode == InsertKeyMode.Default && _insertToggled));

    /// <summary>Gets or sets the input mask. Null or empty restores unrestricted editing.</summary>
    [AllowNull, DefaultValue(""), Localizable(true)]
    public string Mask
    {
        get => _mask;
        set
        {
            value ??= string.Empty;
            if (value == _mask) { return; }
            ReplaceProvider(new MaskProvider(value.Length == 0 ? NullMask : value, Culture,
                AllowPromptAsInput, PromptChar, EffectivePasswordChar, AsciiOnly), value);
        }
    }

    /// <summary>Gets an independent snapshot of the active provider, or null without a mask.</summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public MaskProvider? MaskedTextProvider => _mask.Length == 0 ? null : (MaskProvider)_provider.Clone();

    /// <summary>Gets whether all required positions contain input.</summary>
    [Browsable(false)]
    public bool MaskCompleted => _provider.MaskCompleted;

    /// <summary>Gets whether all editable positions contain input.</summary>
    [Browsable(false)]
    public bool MaskFull => _provider.MaskFull;

    /// <summary>Gets or sets the display character used to conceal entered data.</summary>
    [DefaultValue('\0'), Localizable(true)]
    public char PasswordChar
    {
        get => _passwordChar;
        set
        {
            if (!MaskProvider.IsValidPasswordChar(value))
                throw new ArgumentException("The password character is not valid.", nameof(value));
            if (value == PromptChar)
                throw new InvalidOperationException("PasswordChar and PromptChar must be different.");
            if (_passwordChar == value) { return; }
            if (!_useSystemPasswordChar) { _provider.PasswordChar = value; }
            _passwordChar = value;
            ApplyNativePassword();
            RefreshDisplay();
            VerifyImeRestrictedModeChanged();
        }
    }

    /// <summary>Gets or sets the character displayed in unassigned positions.</summary>
    [DefaultValue('_'), Localizable(true)]
    public char PromptChar
    {
        get => _provider.PromptChar;
        set
        {
            if (!MaskProvider.IsValidInputChar(value))
                throw new ArgumentException("The prompt character is not valid.", nameof(value));
            if (value == PasswordChar)
                throw new InvalidOperationException("PromptChar and PasswordChar must be different.");
            string previous = Text;
            _provider.PromptChar = value;
            RefreshDisplay();
            RaiseTextChangedIfNeeded(previous);
        }
    }

    /// <summary>Gets or sets whether a failing character rejects an entire text assignment or paste.</summary>
    [DefaultValue(false)]
    public bool RejectInputOnFirstFailure { get => _rejectInputOnFirstFailure; set => _rejectInputOnFirstFailure = value; }

    /// <summary>Gets or sets whether entering a prompt resets its edit position.</summary>
    [DefaultValue(true)]
    public bool ResetOnPrompt { get => _provider.ResetOnPrompt; set => _provider.ResetOnPrompt = value; }

    /// <summary>Gets or sets whether entering a space resets its edit position.</summary>
    [DefaultValue(true)]
    public bool ResetOnSpace { get => _provider.ResetOnSpace; set => _provider.ResetOnSpace = value; }

    /// <summary>Gets or sets whether reentered literal characters are skipped.</summary>
    [DefaultValue(true)]
    public bool SkipLiterals { get => _provider.SkipLiterals; set => _provider.SkipLiterals = value; }

    /// <summary>Gets or sets the formatting of the programmatic Text value.</summary>
    [DefaultValue(MaskFormat.IncludeLiterals)]
    public MaskFormat TextMaskFormat
    {
        get => _textMaskFormat;
        set
        {
            value = ValidateMaskFormat(value);
            if (_textMaskFormat == value) { return; }
            string previous = Text;
            _textMaskFormat = value;
            _provider.IncludePrompt = (value & MaskFormat.IncludePrompt) != 0;
            _provider.IncludeLiterals = (value & MaskFormat.IncludeLiterals) != 0;
            RaiseTextChangedIfNeeded(previous);
        }
    }

    /// <summary>Gets or sets unhidden data formatted according to TextMaskFormat.</summary>
    [AllowNull, Localizable(true), DefaultValue("")]
    public override string Text
    {
        get => _mask.Length == 0 ? base.Text : _provider.ToString();
        set
        {
            if (_mask.Length == 0) { base.Text = value; return; }
            value ??= string.Empty;
            if (value.Length == 0) { DeleteInput(Keys.Delete, 0, _provider.Length); }
            else if (RejectInputOnFirstFailure)
            {
                MaskProvider source = _provider;
                MaskProvider candidate = (MaskProvider)source.Clone();
                if (!candidate.Set(value, out int position, out MaskedTextResultHint hint))
                {
                    Reject(position, hint);
                    return;
                }
                CommitProvider(source, candidate, position + 1);
            }
            else { ReplaceInput(value, 0, _provider.Length); }
        }
    }

    /// <summary>Gets the number of displayed characters, including visible prompts and literals.</summary>
    [Browsable(false)]
    public override int TextLength => _mask.Length == 0 ? base.TextLength : GetDisplayText().Length;

    /// <summary>Gets or sets horizontal text alignment.</summary>
    [DefaultValue(HorizontalAlignment.Left), Localizable(true)]
    public HorizontalAlignment TextAlign
    {
        get => _textAlign;
        set
        {
            if ((uint)value > (uint)HorizontalAlignment.Center)
                throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(HorizontalAlignment));
            if (_textAlign == value) { return; }
            _textAlign = value;
            RecreateHandle();
            OnTextAlignChanged(EventArgs.Empty);
        }
    }

    /// <summary>Gets or sets the framework type used for input validation.</summary>
    /// <remarks>Custom converters and custom Parse methods are not yet supported by this AOT-safe implementation.</remarks>
    [Browsable(false), DefaultValue(null)]
    public Type? ValidatingType { get => _validatingType; set => _validatingType = value; }

    /// <summary>Gets or sets whether Windows supplies the password display character.</summary>
    [DefaultValue(false)]
    public bool UseSystemPasswordChar
    {
        get => _useSystemPasswordChar;
        set
        {
            if (_useSystemPasswordChar == value) { return; }
            char character = value ? SystemPasswordChar : _passwordChar;
            _provider.PasswordChar = character; // Validate before updating the property.
            _useSystemPasswordChar = value;
            ApplyNativePassword();
            RefreshDisplay();
            VerifyImeRestrictedModeChanged();
        }
    }

    /// <summary>Occurs when the effective overwrite mode changes.</summary>
    public event EventHandler? IsOverwriteModeChanged;
    /// <summary>Occurs after the mask has changed.</summary>
    public event EventHandler? MaskChanged;
    /// <summary>Occurs after the text alignment has changed.</summary>
    public event EventHandler? TextAlignChanged;
    /// <summary>Occurs for each rejected input position.</summary>
    public event MaskInputRejectedEventHandler? MaskInputRejected;
    /// <summary>Occurs after type validation, before the Validating event.</summary>
    public event TypeValidationEventHandler? TypeValidationCompleted;

    // These no-ops are the documented WinForms MaskedTextBox contracts.
    /// <summary>Multiline is unsupported and always false.</summary>
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool Multiline { get => false; set { } }
    /// <summary>TAB insertion is unsupported and always false.</summary>
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool AcceptsTab { get => false; set { } }
    /// <summary>Word wrap is unsupported and always false.</summary>
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool WordWrap { get => false; set { } }
    /// <summary>Gets or sets the hidden lines projection.</summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public new string[] Lines { get => base.Lines; set => base.Lines = value; }
    /// <summary>The mask determines the maximum input length.</summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override int MaxLength { get => 0; set { } }
    /// <summary>Masked controls do not raise AcceptsTabChanged.</summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new event EventHandler? AcceptsTabChanged { add { } remove { } }
    /// <summary>Masked controls do not raise MultilineChanged.</summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new event EventHandler? MultilineChanged { add { } remove { } }
    /// <summary>Undo history is not supported.</summary>
    public new void ClearUndo() { }
    /// <summary>Undo is not supported.</summary>
    public new void Undo() { }
    /// <summary>Explicit caret scrolling is not supported.</summary>
    public new void ScrollToCaret() { }
    /// <summary>The only physical line starts at zero.</summary>
    public new int GetFirstCharIndexFromLine(int lineNumber) => 0;
    /// <summary>The only physical line starts at zero.</summary>
    public new int GetFirstCharIndexOfCurrentLine() => 0;
    /// <inheritdoc/>
    public override int GetLineFromCharIndex(int index) => 0;

    /// <summary>Gets the selected data using CutCopyMaskFormat, or replaces selected display positions.</summary>
    [AllowNull]
    public override string SelectedText
    {
        get => _mask.Length == 0 ? base.SelectedText : _provider.ToString(true,
            (_cutCopyMaskFormat & MaskFormat.IncludePrompt) != 0,
            (_cutCopyMaskFormat & MaskFormat.IncludeLiterals) != 0, SelectionStart, SelectionLength);
        set => SetSelectedTextInternal(value, true);
    }

    internal override void SetSelectedTextInternal(string? text, bool clearUndo)
    {
        if (_mask.Length == 0) { base.SetSelectedTextInternal(text, clearUndo); return; }
        int start = SelectionStart;
        int length = SelectionLength;
        if (string.IsNullOrEmpty(text)) { if (length > 0) { DeleteInput(Keys.Delete, start, length); } }
        else { ReplaceInput(text, start, length); }
    }

    private protected override bool PasswordProtect => EffectivePasswordChar != '\0';

    /// <inheritdoc/>
    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams parameters = base.CreateParams;
            HorizontalAlignment alignment = RenderRightToLeft ? _textAlign switch
            {
                HorizontalAlignment.Left => HorizontalAlignment.Right,
                HorizontalAlignment.Right => HorizontalAlignment.Left,
                _ => _textAlign
            } : _textAlign;
            parameters.ExStyle &= ~(int)Win32.WS_EX_RIGHT;
            parameters.Style &= ~(int)(Win32.ES_CENTER | Win32.ES_RIGHT);
            parameters.Style |= (int)(alignment switch
            {
                HorizontalAlignment.Center => Win32.ES_CENTER,
                HorizontalAlignment.Right => Win32.ES_RIGHT,
                _ => Win32.ES_LEFT
            });
            return parameters;
        }
    }

    /// <inheritdoc/>
    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        ApplyNativePassword();
        RefreshDisplay();
    }

    /// <inheritdoc/>
    protected override void OnGotFocus(EventArgs e) { RefreshDisplay(); base.OnGotFocus(e); }
    /// <inheritdoc/>
    protected override void OnLostFocus(EventArgs e) { base.OnLostFocus(e); RefreshDisplay(); }
    /// <inheritdoc/>
    protected override void OnReadOnlyChanged(EventArgs e) { RefreshDisplay(); base.OnReadOnlyChanged(e); }

    /// <inheritdoc/>
    protected override bool OnCommand(int notificationCode)
    {
        if (_mask.Length == 0) { return base.OnCommand(notificationCode); }
        // Character, selection and clipboard operations go through the provider.
        // EN_CHANGE also occurs during IME pre-edit and our silent WindowText writes.
        if (notificationCode == Win32.EN_CHANGE) { return true; }
        return base.OnCommand(notificationCode);
    }

    /// <inheritdoc/>
    protected override void OnKeyPress(KeyPressEventArgs e)
    {
        base.OnKeyPress(e);
        if (_mask.Length == 0 || e.Handled) { return; }
        if (char.IsControl(e.KeyChar) && e.KeyChar != '\b') { return; }
        e.Handled = true;
        if (ReadOnly) { return; }
        if (e.KeyChar == '\b') { DeleteInput(Keys.Back, SelectionStart, SelectionLength); return; }

        MaskProvider source = _provider;
        MaskProvider candidate = (MaskProvider)source.Clone();
        if (PlaceCharacter(candidate, e.KeyChar, SelectionStart, SelectionLength, IsOverwriteMode,
            out int position, out MaskedTextResultHint hint))
        {
            CommitProvider(source, candidate, position + 1);
        }
        else { Reject(position, hint); }
    }

    /// <inheritdoc/>
    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        if (_mask.Length == 0 || e.Handled) { return; }
        if (e.KeyCode == Keys.Insert && e.Modifiers == Keys.None && _insertKeyMode == InsertKeyMode.Default)
        {
            _insertToggled = !_insertToggled;
            OnIsOverwriteModeChanged(EventArgs.Empty);
            e.Handled = true;
        }
        else if (!ReadOnly && e.KeyCode is Keys.Back or Keys.Delete)
        {
            // Shift+Delete is the native Cut shortcut and reaches WM_CUT.
            if (e.KeyCode == Keys.Delete && e.Shift && ShortcutsEnabled) { return; }
            int start = SelectionStart;
            int length = SelectionLength;
            if (e.Control && length == 0)
            {
                if (e.KeyCode == Keys.Delete) { length = _provider.Length - start; }
                else { length = Math.Min(_provider.Length, start + 1); start = 0; }
            }
            DeleteInput(e.KeyCode, start, length);
            e.SuppressKeyPress = true;
        }
    }

    /// <inheritdoc/>
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (_mask.Length != 0 && keyData is (Keys.Control | Keys.Back) or (Keys.Control | Keys.Shift | Keys.Back))
        {
            if (!ShortcutsEnabled) { return true; }
            if (!ReadOnly)
            {
                int length = SelectionLength;
                int start = SelectionStart;
                if (length == 0) { length = Math.Min(_provider.Length, start + 1); start = 0; }
                DeleteInput(Keys.Back, start, length);
            }
            return true;
        }
        if (base.ProcessCmdKey(ref msg, keyData)) { return true; }
        if (keyData == (Keys.Control | Keys.A) && ShortcutsEnabled) { SelectAll(); return true; }
        return false;
    }

    /// <inheritdoc/>
    protected override void WndProc(ref Message m)
    {
        if (m.Msg is Win32.EM_CANUNDO or Win32.EM_UNDO or 0x0304 /* WM_UNDO */
            or Win32.EM_SCROLLCARET or Win32.EM_LIMITTEXT)
        {
            m.Result = 0;
            return;
        }
        if (_mask.Length != 0)
        {
            switch (m.Msg)
            {
                case 0x010D: // WM_IME_STARTCOMPOSITION
                    _composing = true;
                    break;
                case 0x010E: // WM_IME_ENDCOMPOSITION
                    _composing = false;
                    break;
                case Win32.WM_COPY:
                    CopyMaskedSelection();
                    m.Result = 0;
                    return;
                case Win32.WM_CUT:
                    if (!ReadOnly && CopyMaskedSelection()) { DeleteInput(Keys.Delete, SelectionStart, SelectionLength); }
                    m.Result = 0;
                    return;
                case Win32.WM_PASTE:
                    if (!ReadOnly) { SetSelectedTextInternal(ReadClipboardText(), true); }
                    m.Result = 0;
                    return;
                case 0x0303: // WM_CLEAR
                    if (!ReadOnly && SelectionLength > 0) { DeleteInput(Keys.Delete, SelectionStart, SelectionLength); }
                    m.Result = 0;
                    return;
                case Win32.EM_REPLACESEL:
                    SetSelectedTextInternal(Marshal.PtrToStringUni(m.LParam), true);
                    m.Result = 1;
                    return;
            }
        }
        base.WndProc(ref m);
        if (m.Msg == 0x010E && _mask.Length != 0) { RefreshDisplay(); }
    }

    /// <inheritdoc/>
    public override char GetCharFromPosition(Point pt)
    {
        string display = _mask.Length == 0 ? base.Text : GetDisplayText();
        int index = GetCharIndexFromPosition(pt);
        return index >= 0 && index < display.Length ? display[index] : '\0';
    }

    /// <inheritdoc/>
    public override int GetCharIndexFromPosition(Point pt)
    {
        int index = Win32.LowWord(Win32.SendMessageW(Handle, Win32.EM_CHARFROMPOS, 0, Win32.MakeLParam(pt.X, pt.Y)));
        return Math.Clamp(index, 0, Math.Max(0, TextLength - 1));
    }

    /// <inheritdoc/>
    public override Point GetPositionFromCharIndex(int index)
    {
        if (index < 0 || index >= TextLength) { return Point.Empty; }
        nint position = Win32.SendMessageW(Handle, Win32.EM_POSFROMCHAR, index, 0);
        return new Point(unchecked((short)Win32.LowWord(position)), unchecked((short)Win32.HighWord(position)));
    }

    /// <inheritdoc/>
    protected override void OnValidating(CancelEventArgs e)
    {
        if (_validatingType is not null)
        {
            TypeValidationEventArgs result = ValidateTextCore(_validatingType);
            TypeValidationCompleted?.Invoke(this, result);
            e.Cancel = result.Cancel;
        }
        base.OnValidating(e);
    }

    /// <summary>Converts the formatted value to the requested framework type.</summary>
    public object? ValidateText()
    {
        if (_validatingType is null) { return null; }
        TypeValidationEventArgs result = ValidateTextCore(_validatingType);
        TypeValidationCompleted?.Invoke(this, result);
        // Cancel affects focus validation, not the explicit return value.
        return result.ReturnValue;
    }

    /// <summary>Raises IsOverwriteModeChanged.</summary>
    protected virtual void OnIsOverwriteModeChanged(EventArgs e) => IsOverwriteModeChanged?.Invoke(this, e);
    /// <summary>Raises MaskChanged.</summary>
    protected virtual void OnMaskChanged(EventArgs e) => MaskChanged?.Invoke(this, e);
    /// <summary>Raises TextAlignChanged.</summary>
    protected virtual void OnTextAlignChanged(EventArgs e) => TextAlignChanged?.Invoke(this, e);

    private void Reject(int position, MaskedTextResultHint hint)
    {
        if (BeepOnError) { _ = Win32.MessageBeep(0); }
        MaskInputRejected?.Invoke(this, new MaskInputRejectedEventArgs(position, hint));
    }

    private static MaskFormat ValidateMaskFormat(MaskFormat value)
    {
        if ((uint)value > (uint)MaskFormat.IncludePromptAndLiterals)
            throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(MaskFormat));
        return value;
    }

    private char EffectivePasswordChar => _provider is null ? '\0' : _provider.PasswordChar;

    private char SystemPasswordChar
    {
        get
        {
            if (_systemPasswordChar == '\0')
            {
                using var edit = new TextBox { UseSystemPasswordChar = true };
                _systemPasswordChar = edit.PasswordChar;
            }
            return _systemPasswordChar;
        }
    }

    private void ApplyNativePassword()
    {
        if (NativeHandle != 0)
            _ = Win32.SendMessageW(NativeHandle, Win32.EM_SETPASSWORDCHAR,
                _mask.Length == 0 ? EffectivePasswordChar : 0, 0);
    }

    private string GetDisplayText()
    {
        if (_mask.Length == 0) { return base.Text; }
        bool prompt = !ReadOnly && (DesignMode || !HidePromptOnLeave || Focused);
        return _provider.ToString(false, prompt, true, 0, _provider.Length);
    }

    private void RefreshDisplay()
    {
        if (_mask.Length == 0 || _composing) { return; }
        int start = SelectionStart;
        int length = SelectionLength;
        base.WindowText = GetDisplayText();
        base.SelectInternal(start, length, _provider.Length);
    }

    private void RaiseTextChangedIfNeeded(string previous)
    {
        if (!string.Equals(previous, Text, StringComparison.Ordinal)) { OnTextChanged(EventArgs.Empty); }
    }

    private void CommitProvider(MaskProvider original, MaskProvider candidate, int caret)
    {
        // A rejection event may reenter and replace the model. Do not overwrite
        // a newer model with the transaction that raised that event.
        if (!ReferenceEquals(_provider, original)) { return; }
        string previous = Text;
        _provider = candidate;
        RefreshDisplay();
        RaiseTextChangedIfNeeded(previous);
        if (ReferenceEquals(_provider, candidate))
            base.SelectInternal(Math.Clamp(caret, 0, candidate.Length), 0, candidate.Length);
    }

    private static bool PlaceCharacter(MaskProvider provider, char character, int start, int length,
        bool overwrite, out int position, out MaskedTextResultHint hint)
    {
        position = start;
        if (start >= provider.Length)
        {
            hint = MaskedTextResultHint.UnavailableEditPosition;
            return false;
        }
        if (length > 0) { return provider.Replace(character, start, start + length - 1, out position, out hint); }
        return overwrite ? provider.Replace(character, start, out position, out hint)
            : provider.InsertAt(character, start, out position, out hint);
    }

    private void ReplaceInput(string text, int start, int length)
    {
        MaskProvider source = _provider;
        MaskProvider candidate = (MaskProvider)source.Clone();
        int end = start + length - 1;
        int caret = start;
        if (RejectInputOnFirstFailure)
        {
            bool success = length > 0
                ? candidate.Replace(text, start, end, out caret, out MaskedTextResultHint hint)
                : candidate.InsertAt(text, start, out caret, out hint);
            if (!success) { Reject(caret, hint); return; }
            caret++;
        }
        else
        {
            // Input length bounds the operation. Failed characters leave the
            // insertion position unchanged so the next character can satisfy it.
            for (int offset = 0; offset < text.Length; offset++)
            {
                char character = text[offset];
                if (!candidate.VerifyEscapeChar(character, start))
                {
                    int editable = candidate.FindEditPositionFrom(start, true);
                    if (editable == MaskProvider.InvalidIndex)
                    {
                        Reject(start, MaskedTextResultHint.UnavailableEditPosition);
                        if (!ReferenceEquals(_provider, source)) { return; }
                        continue;
                    }
                    start = editable;
                }
                int replaceLength = start <= end ? 1 : 0;
                if (PlaceCharacter(candidate, character, start, replaceLength, replaceLength != 0,
                    out int position, out MaskedTextResultHint hint))
                {
                    start = position + 1;
                }
                else
                {
                    Reject(start, hint);
                    if (!ReferenceEquals(_provider, source)) { return; }
                }
            }
            if (length > 0 && start <= end && !candidate.RemoveAt(start, end, out int failed, out MaskedTextResultHint removalHint))
            {
                Reject(failed, removalHint);
            }
            caret = start;
        }
        CommitProvider(source, candidate, caret);
    }

    private void DeleteInput(Keys key, int start, int length)
    {
        if (length == 0)
        {
            if (key == Keys.Back) { if (start == 0) { return; } start--; }
            else if (start == _provider.Length) { return; }
        }
        MaskProvider source = _provider;
        MaskProvider candidate = (MaskProvider)source.Clone();
        int end = length > 0 ? start + length - 1 : start;
        if (!candidate.RemoveAt(start, end, out int position, out MaskedTextResultHint hint))
        {
            Reject(position, hint);
            return;
        }
        CommitProvider(source, candidate, start);
    }

    private void ReplaceProvider(MaskProvider replacement, string mask)
    {
        MaskProvider previousProvider = _provider;
        string previousText = Text;
        string previousMask = _mask;
        replacement.ResetOnPrompt = previousProvider.ResetOnPrompt;
        replacement.ResetOnSpace = previousProvider.ResetOnSpace;
        replacement.SkipLiterals = previousProvider.SkipLiterals;
        replacement.IncludePrompt = previousProvider.IncludePrompt;
        replacement.IncludeLiterals = previousProvider.IncludeLiterals;
        MaskedTextResultHint hint = MaskedTextResultHint.Success;
        int position = 0;
        string? unrestricted = null;

        if (mask.Length == 0)
        {
            unrestricted = previousMask.Length == 0 ? base.Text : previousProvider.ToString(false, false);
        }
        else if (previousMask.Length == 0)
        {
            replacement.Set(base.Text, out position, out hint);
        }
        else
        {
            int destination = 0;
            bool preservePositions = previousProvider.Mask == replacement.Mask;
            for (int source = 0; source < previousProvider.Length; source++)
            {
                if (!previousProvider.IsEditPosition(source) || previousProvider.IsAvailablePosition(source)) { continue; }
                destination = preservePositions ? source : replacement.FindEditPositionFrom(destination, true);
                if (destination == MaskProvider.InvalidIndex
                    || !replacement.Replace(previousProvider[source], destination, out position, out hint))
                {
                    if (destination == MaskProvider.InvalidIndex) { position = replacement.Length; hint = MaskedTextResultHint.UnavailableEditPosition; }
                    replacement.Clear();
                    break;
                }
                destination++;
            }
        }

        _provider = replacement;
        _mask = mask;
        if (unrestricted is not null) { base.WindowText = unrestricted; }
        ApplyNativePassword();
        RefreshDisplay();
        if (!MaskProvider.GetOperationResultFromHint(hint)) { Reject(position, hint); }
        if (!ReferenceEquals(_provider, replacement)) { return; }
        if (previousMask != mask) { OnMaskChanged(EventArgs.Empty); }
        RaiseTextChangedIfNeeded(previousText);
    }

    private TypeValidationEventArgs ValidateTextCore(Type validatingType)
    {
        if (_mask.Length != 0 && !_provider.MaskCompleted)
            return new TypeValidationEventArgs(validatingType, false, null, "The required mask positions have not been filled.");
        string value = _mask.Length == 0 ? base.Text : _provider.ToString(false, _provider.IncludeLiterals);
        if (TryConvertKnownType(validatingType, value, out object? converted, out string? message))
            return new TypeValidationEventArgs(validatingType, true, converted, "Type validation succeeded.");
        return new TypeValidationEventArgs(validatingType, false, null, message);
    }

    private bool TryConvertKnownType(Type validatingType, string value, out object? converted, out string? message)
    {
        converted = null;
        message = null;
        Type targetType = Nullable.GetUnderlyingType(validatingType) ?? validatingType;
        IFormatProvider culture = _formatProvider ?? CultureInfo.CurrentCulture;

        if (targetType == typeof(string))
        {
            converted = value;
            return true;
        }

        if (targetType == typeof(bool) && bool.TryParse(value, out bool boolean)) { converted = boolean; return true; }
        if (targetType == typeof(char) && value.Length == 1) { converted = value[0]; return true; }
        if (targetType == typeof(byte) && byte.TryParse(value, NumberStyles.Integer, culture, out byte u8)) { converted = u8; return true; }
        if (targetType == typeof(sbyte) && sbyte.TryParse(value, NumberStyles.Integer, culture, out sbyte i8)) { converted = i8; return true; }
        if (targetType == typeof(short) && short.TryParse(value, NumberStyles.Integer, culture, out short i16)) { converted = i16; return true; }
        if (targetType == typeof(ushort) && ushort.TryParse(value, NumberStyles.Integer, culture, out ushort u16)) { converted = u16; return true; }
        if (targetType == typeof(int) && int.TryParse(value, NumberStyles.Integer, culture, out int i32)) { converted = i32; return true; }
        if (targetType == typeof(uint) && uint.TryParse(value, NumberStyles.Integer, culture, out uint u32)) { converted = u32; return true; }
        if (targetType == typeof(long) && long.TryParse(value, NumberStyles.Integer, culture, out long i64)) { converted = i64; return true; }
        if (targetType == typeof(ulong) && ulong.TryParse(value, NumberStyles.Integer, culture, out ulong u64)) { converted = u64; return true; }
        if (targetType == typeof(nint) && long.TryParse(value, NumberStyles.Integer, culture, out long nativeInt)
            && (IntPtr.Size == 8 || (nativeInt >= int.MinValue && nativeInt <= int.MaxValue)))
        {
            converted = (nint)nativeInt;
            return true;
        }
        if (targetType == typeof(nuint) && ulong.TryParse(value, NumberStyles.Integer, culture, out ulong nativeUInt)
            && (UIntPtr.Size == 8 || nativeUInt <= uint.MaxValue))
        {
            converted = (nuint)nativeUInt;
            return true;
        }
        if (targetType == typeof(float) && float.TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, culture, out float single)) { converted = single; return true; }
        if (targetType == typeof(double) && double.TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, culture, out double real)) { converted = real; return true; }
        if (targetType == typeof(decimal) && decimal.TryParse(value, NumberStyles.Number, culture, out decimal money)) { converted = money; return true; }
        if (targetType == typeof(DateTime) && DateTime.TryParse(value, culture, DateTimeStyles.AllowWhiteSpaces, out DateTime dateTime)) { converted = dateTime; return true; }
        if (targetType == typeof(DateTimeOffset) && DateTimeOffset.TryParse(value, culture, DateTimeStyles.AllowWhiteSpaces, out DateTimeOffset dateTimeOffset)) { converted = dateTimeOffset; return true; }
        if (targetType == typeof(DateOnly) && DateOnly.TryParse(value, culture, DateTimeStyles.AllowWhiteSpaces, out DateOnly dateOnly)) { converted = dateOnly; return true; }
        if (targetType == typeof(TimeOnly) && TimeOnly.TryParse(value, culture, DateTimeStyles.AllowWhiteSpaces, out TimeOnly timeOnly)) { converted = timeOnly; return true; }
        if (targetType == typeof(TimeSpan) && TimeSpan.TryParse(value, culture, out TimeSpan timeSpan)) { converted = timeSpan; return true; }
        if (targetType == typeof(Guid) && Guid.TryParse(value, out Guid guid)) { converted = guid; return true; }

        if (targetType.IsEnum)
        {
            try
            {
                converted = Enum.Parse(targetType, value, ignoreCase: false);
                return true;
            }
            catch (ArgumentException)
            {
                // Fall through to the same deterministic failure message used by other unsupported values.
            }
        }

        message = $"The value could not be converted to {validatingType.FullName}; only AOT-safe primitive and framework conversions are supported.";
        return false;
    }
}
