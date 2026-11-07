// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// Portions adapted from dotnet/winforms v10.0.11 for Lumina.Forms.

using System.ComponentModel;
using System.Drawing;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Lumina.Forms;

/// <summary>
/// Represents a multi-line rich text edit control.
/// </summary>
public class RichTextBox : TextBoxBase
{
    private const int WmSetText = 0x000C;
    private const int EmGetLineCount = 0x00BA;
    private const int EmExSetSel = 0x0437;
    private const int EmGetCharFormat = 0x043A;
    private const int EmGetParaFormat = 0x043D;
    private const int EmSelectionType = 0x0442;
    private const int EmExLineFromChar = 0x0436;
    private const int EmSetCharFormat = 0x0444;
    private const int EmSetBackgroundColor = 0x0443;
    private const int EmSetParaFormat = 0x0447;
    private const uint CfmColor = 0x40000000;
    private const uint CfmBackColor = 0x04000000;
    private const uint CfmOffset = 0x10000000;
    private const uint CfmProtected = 0x10;
    private const uint CfmFace = 0x20000000;
    private const uint CfmSize = 0x80000000;
    private const uint CfmCharSet = 0x08000000;
    private const uint CfeProtected = 0x10;
    private const uint CfeAutoBackColor = 0x04000000;
    private const uint PfmStartIndent = 1;
    private const uint PfmRightIndent = 2;
    private const uint PfmOffset = 4;
    private const uint PfmAlignment = 8;
    private const uint PfmTabStops = 0x10;
    private const uint PfmNumbering = 0x20;
    private const ushort PfnBullet = 1;
    private const int MaxTabStops = 32;
    private const int EmStreamIn = 0x0449;
    private const int EmStreamOut = 0x044A;
    private const int EmGetTextEx = 0x045E;
    private const int EmGetTextLengthEx = 0x045F;
    private const int SfTextUnicode = 0x0011;
    private const int SfRtf = 0x0002;
    private const int SffSelection = 0x8000;
    private const int WmClear = 0x0303;
    private const int EmExLimitText = 0x0435;
    private const int EmSetEventMask = 0x0445;
    private const int EmSetOptions = 0x044D;
    private const int EmAutoUrlDetect = 0x045B;
    private const int EcoAutoWordSelection = 1;
    private const int EcoopOr = 2;
    private const int EcoopXor = 4;
    private const int EsDisableNoScroll = 0x2000;
    private const int EnmChange = 1;
    private const int EnmUpdate = 2;
    private const int EnmScroll = 4;
    private const int EnmScrollEvents = 8;
    private const int EnmRequestResize = 0x40000;
    private const int EnmSelChange = 0x80000;
    private const int EnmImeChange = 0x800000;
    private const int EnRequestResize = 0x0701;
    private const int EnSelChange = 0x0702;
    private const int EnHScroll = 0x0601;
    private const int EnVScroll = 0x0602;
    private const int EmGetImeCompMode = 0x047A;
    private static readonly object s_contentsResizedEvent = new();
    private static readonly object s_selectionChangedEvent = new();
    private static readonly object s_hScrollEvent = new();
    private static readonly object s_vScrollEvent = new();
    private static readonly object s_imeChangeEvent = new();

    // The registered superclass retains pointers into Msftedit. Keep its one
    // system-directory load alive for the same process lifetime as that cache.
    private static readonly Lazy<nint> s_richEditModule = new(LoadRichEditModule);

    private string? _textPlain;
    private string? _textRtf;
    private bool _creatingRichHandle;
    private Color _selectionBackColorToSetOnHandleCreated = Color.Empty;
    private bool _autoWordSelection;
    private bool _detectUrls;
    private RichTextBoxScrollBars _scrollBars;
    private int _bulletIndent;
    private bool _suppressContentsResized;
    private int _currentSelectionStart = -1;
    private int _currentSelectionEnd = -1;
    private short _currentSelectionType = -1;

    /// <summary>
    /// Initializes a multi-line rich text box.
    /// </summary>
    public RichTextBox()
    {
        DetectUrls = true;
        ScrollBars = RichTextBoxScrollBars.Both;
        MaxLength = int.MaxValue;
        Multiline = true;
        AutoSize = false;
        _ = DarkModeNative.TryEnableDarkScrollBar();
    }

    public override Color ForeColor
    {
        get => base.ForeColor;
        set
        {
            if (!IsHandleCreated || InternalSetForeColor(value))
            {
                base.ForeColor = value;
            }
        }
    }

    [AllowNull]
    public override Font Font
    {
        get => base.Font;
        set
        {
            if (!IsHandleCreated || Win32.GetWindowTextLengthW(NativeHandle) <= 0)
            {
                base.Font = value;
                return;
            }

            if (value is null)
            {
                base.Font = null;
                SetCharFormatFont(selectionOnly: false, Font);
                return;
            }

            try
            {
                Font? font = GetCharFormatFont(selectionOnly: false);
                if (font is null || !font.Equals(value))
                {
                    SetCharFormatFont(selectionOnly: false, value);
                    // Base sends WM_SETFONT while accepting the font resolved
                    // by RichEdit. Its intermediate size notification is not
                    // part of the document format change's public event.
                    _suppressContentsResized = true;
                    base.Font = GetCharFormatFont(selectionOnly: false);
                }
            }
            finally { _suppressContentsResized = false; }
        }
    }

    [Category("Behavior")]
    public event ContentsResizedEventHandler? ContentsResized
    {
        add => Events.AddHandler(s_contentsResizedEvent, value);
        remove => Events.RemoveHandler(s_contentsResizedEvent, value);
    }

    protected virtual void OnContentsResized(ContentsResizedEventArgs e)
        => ((ContentsResizedEventHandler?)Events[s_contentsResizedEvent])?.Invoke(this, e);

    [Category("Behavior")]
    public event EventHandler? SelectionChanged
    {
        add => Events.AddHandler(s_selectionChangedEvent, value);
        remove => Events.RemoveHandler(s_selectionChangedEvent, value);
    }

    [Category("Behavior")]
    public event EventHandler? HScroll
    {
        add => Events.AddHandler(s_hScrollEvent, value);
        remove => Events.RemoveHandler(s_hScrollEvent, value);
    }

    [Category("Behavior")]
    public event EventHandler? VScroll
    {
        add => Events.AddHandler(s_vScrollEvent, value);
        remove => Events.RemoveHandler(s_vScrollEvent, value);
    }

    [Category("Behavior")]
    public event EventHandler? ImeChange
    {
        add => Events.AddHandler(s_imeChangeEvent, value);
        remove => Events.RemoveHandler(s_imeChangeEvent, value);
    }

    protected virtual void OnSelectionChanged(EventArgs e)
        => ((EventHandler?)Events[s_selectionChangedEvent])?.Invoke(this, e);

    protected virtual void OnHScroll(EventArgs e)
        => ((EventHandler?)Events[s_hScrollEvent])?.Invoke(this, e);

    protected virtual void OnVScroll(EventArgs e)
        => ((EventHandler?)Events[s_vScrollEvent])?.Invoke(this, e);

    protected virtual void OnImeChange(EventArgs e)
        => ((EventHandler?)Events[s_imeChangeEvent])?.Invoke(this, e);

    [Localizable(true)]
    [RefreshProperties(RefreshProperties.All)]
    [AllowNull]
    public override string Text
    {
        get
        {
            if (IsDisposed) { return base.Text; }
            if (RecreatingHandle || GetAnyDisposingInHierarchy()) { return string.Empty; }
            if (!IsHandleCreated && _textRtf is null) { return _textPlain ?? base.Text; }
            if (!IsHandleCreated) { CreateHandle(); }
            return GetRichText();
        }
        set
        {
            using LayoutTransaction transaction = new(AutoSize ? Parent : null, this, nameof(Text));
            _textRtf = null;
            if (!IsHandleCreated)
            {
                _textPlain = value;
            }
            else
            {
                _textPlain = null;
                StreamPlainText(value ?? string.Empty, selection: false);
                _ = Win32.SendMessageW(NativeHandle, Win32.EM_SETMODIFY, 0, 0);
            }
        }
    }

    /// <summary>Gets or sets the document text, including its RTF formatting.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [RefreshProperties(RefreshProperties.All)]
    public string? Rtf
    {
        get
        {
            if (!IsHandleCreated && _textPlain is null) { return _textRtf; }
            if (!IsHandleCreated) { CreateHandle(); }
            return StreamRtfOut(selection: false);
        }
        set
        {
            value ??= string.Empty;
            if (value.Equals(Rtf)) { return; }
            if (!IsHandleCreated) { CreateHandle(); }
            _textRtf = value;
            StreamText(value, SfRtf);
            if (!_creatingRichHandle) { OnTextChanged(EventArgs.Empty); }
        }
    }

    /// <summary>Gets or sets the selected text, including its RTF formatting.</summary>
    [DefaultValue("")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public string SelectedRtf
    {
        get
        {
            if (!IsHandleCreated) { CreateHandle(); }
            return StreamRtfOut(selection: true);
        }
        set
        {
            if (!IsHandleCreated) { CreateHandle(); }
            StreamText(value ?? string.Empty, SfRtf | SffSelection);
        }
    }

    [Browsable(false)]
    public override unsafe int TextLength
    {
        get
        {
            nint handle = Handle;
            GetTextLengthEx parameters = new() { Flags = 8, CodePage = 1200 };
            return (int)Win32.SendMessageW(handle, EmGetTextLengthEx, (nint)(&parameters), 0);
        }
    }

    [DefaultValue("")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public override string SelectedText
    {
        get
        {
            if (!IsHandleCreated) { CreateHandle(); }
            return GetRichText(selection: true);
        }
        set
        {
            if (!IsHandleCreated) { CreateHandle(); }
            StreamPlainText(value ?? string.Empty, selection: true);
        }
    }

    [Category("Appearance")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override int SelectionLength
    {
        get => IsHandleCreated ? SelectedText.Length : base.SelectionLength;
        set => base.SelectionLength = value;
    }

    [Category("Behavior")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RichTextBoxSelectionTypes SelectionType
    {
        get
        {
            if (!IsHandleCreated) { CreateHandle(); }
            return SelectionLength > 0
                ? (RichTextBoxSelectionTypes)(int)Win32.SendMessageW(Handle, EmSelectionType, 0, 0)
                : RichTextBoxSelectionTypes.Empty;
        }
    }

    [DefaultValue(false)]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override bool AutoSize
    {
        get => base.AutoSize;
        set => base.AutoSize = value;
    }

    [DefaultValue(int.MaxValue)]
    public override int MaxLength
    {
        get => base.MaxLength;
        set => base.MaxLength = value;
    }

    [DefaultValue(true)]
    public override bool Multiline
    {
        get => base.Multiline;
        set => base.Multiline = value;
    }

    [Category("Behavior")]
    [DefaultValue(false)]
    public bool AutoWordSelection
    {
        get => _autoWordSelection;
        set
        {
            _autoWordSelection = value;
            if (IsHandleCreated)
            {
                // The fixed WinForms setter sends even for a repeated value.
                _ = Win32.SendMessageW(NativeHandle, EmSetOptions, value ? EcoopOr : EcoopXor, EcoAutoWordSelection);
            }
        }
    }

    [Category("Behavior")]
    [DefaultValue(true)]
    public bool DetectUrls
    {
        get => _detectUrls;
        set
        {
            if (_detectUrls == value) { return; }
            _detectUrls = value;
            if (IsHandleCreated)
            {
                _ = Win32.SendMessageW(NativeHandle, EmAutoUrlDetect, value ? 1 : 0, 0);
                RecreateHandle();
            }
        }
    }

    [Category("Appearance")]
    [DefaultValue(RichTextBoxScrollBars.Both)]
    [Localizable(true)]
    public RichTextBoxScrollBars ScrollBars
    {
        get => _scrollBars;
        set
        {
            if (value is not (RichTextBoxScrollBars.None or RichTextBoxScrollBars.Horizontal
                or RichTextBoxScrollBars.Vertical or RichTextBoxScrollBars.Both
                or RichTextBoxScrollBars.ForcedHorizontal or RichTextBoxScrollBars.ForcedVertical
                or RichTextBoxScrollBars.ForcedBoth))
            {
                throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(RichTextBoxScrollBars));
            }
            if (_scrollBars == value) { return; }
            using LayoutTransaction transaction = new(AutoSize ? Parent : null, this, nameof(ScrollBars));
            _scrollBars = value;
            RecreateHandle();
        }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public unsafe Color SelectionColor
    {
        get
        {
            if (!IsHandleCreated) { CreateHandle(); }
            CharFormat2 format = GetSelectionCharFormat();
            return (format.Mask & CfmColor) != 0
                ? ColorTranslator.FromOle(format.TextColor) : Color.Empty;
        }
        set
        {
            if (!IsHandleCreated) { CreateHandle(); }
            CharFormat2 format = GetSelectionCharFormat();
            format.Mask = CfmColor;
            format.Effects = 0;
            format.TextColor = ColorTranslator.ToWin32(value);
            _ = Win32.SendMessageW(NativeHandle, EmSetCharFormat, 1, (nint)(&format));
        }
    }

    /// <summary>Gets or sets the selection font; a mixed face has no single font.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DisallowNull]
    public Font? SelectionFont
    {
        get => GetCharFormatFont(selectionOnly: true);
        set => SetCharFormatFont(selectionOnly: true, value);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public unsafe Color SelectionBackColor
    {
        get
        {
            if (!IsHandleCreated) { return _selectionBackColorToSetOnHandleCreated; }
            CharFormat2 format = GetSelectionCharFormat();
            if ((format.Effects & CfeAutoBackColor) != 0) { return BackColor; }
            return (format.Mask & CfmBackColor) != 0
                ? ColorTranslator.FromOle(format.BackColor) : Color.Empty;
        }
        set
        {
            // A repeated value can target a different selection. Before handle
            // creation WinForms caches this property without creating a window.
            _selectionBackColorToSetOnHandleCreated = value;
            if (!IsHandleCreated) { return; }
            CharFormat2 format = new() { Size = (uint)sizeof(CharFormat2) };
            if (value == Color.Empty) { format.Effects = CfeAutoBackColor; }
            else
            {
                format.Mask = CfmBackColor;
                format.BackColor = ColorTranslator.ToWin32(value);
            }
            _ = Win32.SendMessageW(NativeHandle, EmSetCharFormat, 1, (nint)(&format));
        }
    }

    [DefaultValue(HorizontalAlignment.Left)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public unsafe HorizontalAlignment SelectionAlignment
    {
        get
        {
            if (!IsHandleCreated) { CreateHandle(); }
            ParaFormat format = new() { Size = (uint)sizeof(ParaFormat) };
            _ = Win32.SendMessageW(NativeHandle, EmGetParaFormat, 0, (nint)(&format));
            if ((format.Mask & PfmAlignment) == 0) { return HorizontalAlignment.Left; }
            return format.Alignment switch
            {
                2 => HorizontalAlignment.Right,
                3 => HorizontalAlignment.Center,
                _ => HorizontalAlignment.Left
            };
        }
        set
        {
            if ((uint)value > 2u)
            {
                throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(HorizontalAlignment));
            }
            if (!IsHandleCreated) { CreateHandle(); }
            ParaFormat format = new()
            {
                Size = (uint)sizeof(ParaFormat),
                Mask = PfmAlignment,
                Alignment = value switch
                {
                    HorizontalAlignment.Right => 2,
                    HorizontalAlignment.Center => 3,
                    _ => 1
                }
            };
            _ = Win32.SendMessageW(NativeHandle, EmSetParaFormat, 0, (nint)(&format));
        }
    }

    [DefaultValue(0)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public unsafe int SelectionCharOffset
    {
        get
        {
            if (!IsHandleCreated) { CreateHandle(); }
            return TwipToPixel(GetSelectionCharFormat().Offset, xDirection: false);
        }
        set
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 2000);
            ArgumentOutOfRangeException.ThrowIfLessThan(value, -2000);
            if (!IsHandleCreated) { CreateHandle(); }
            CharFormat2 format = new()
            {
                Size = (uint)sizeof(CharFormat2),
                Mask = CfmOffset,
                Offset = PixelToTwip(value, xDirection: false)
            };
            _ = Win32.SendMessageW(NativeHandle, EmSetCharFormat, 1, (nint)(&format));
        }
    }

    [DefaultValue(false)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public unsafe bool SelectionProtected
    {
        get
        {
            if (!IsHandleCreated) { CreateHandle(); }
            CharFormat2 format = GetSelectionCharFormat();
            return (format.Mask & CfmProtected) != 0 && (format.Effects & CfeProtected) != 0;
        }
        set
        {
            if (!IsHandleCreated) { CreateHandle(); }
            CharFormat2 format = new()
            {
                Size = (uint)sizeof(CharFormat2),
                Mask = CfmProtected,
                Effects = value ? CfeProtected : 0
            };
            _ = Win32.SendMessageW(NativeHandle, EmSetCharFormat, 1, (nint)(&format));
        }
    }

    [DefaultValue(0)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionIndent
    {
        get => GetSelectionParagraphIndent(PfmStartIndent);
        set => SetSelectionParagraphIndent(PfmStartIndent, value);
    }

    [DefaultValue(0)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionHangingIndent
    {
        get => GetSelectionParagraphIndent(PfmOffset);
        set => SetSelectionParagraphIndent(PfmOffset, value);
    }

    [DefaultValue(0)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionRightIndent
    {
        get => GetSelectionParagraphIndent(PfmRightIndent);
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value);
            SetSelectionParagraphIndent(PfmRightIndent, value);
        }
    }

    [Category("Behavior")]
    [DefaultValue(0)]
    [Localizable(true)]
    public int BulletIndent
    {
        get => _bulletIndent;
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value);
            _bulletIndent = value;
            if (IsHandleCreated && SelectionBullet) { SelectionBullet = true; }
        }
    }

    [DefaultValue(false)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public unsafe bool SelectionBullet
    {
        get
        {
            if (!IsHandleCreated) { CreateHandle(); }
            ParaFormat format = new() { Size = (uint)sizeof(ParaFormat) };
            _ = Win32.SendMessageW(NativeHandle, EmGetParaFormat, 0, (nint)(&format));
            return (format.Mask & PfmNumbering) != 0 && format.Numbering == PfnBullet;
        }
        set
        {
            if (!IsHandleCreated) { CreateHandle(); }
            ParaFormat format = new()
            {
                Size = (uint)sizeof(ParaFormat),
                Mask = PfmNumbering | PfmOffset,
                Numbering = value ? PfnBullet : (ushort)0,
                Offset = value ? PixelToTwip(_bulletIndent, xDirection: true) : 0
            };
            _ = Win32.SendMessageW(NativeHandle, EmSetParaFormat, 0, (nint)(&format));
        }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public unsafe int[] SelectionTabs
    {
        get
        {
            if (!IsHandleCreated) { CreateHandle(); }
            ParaFormat format = new() { Size = (uint)sizeof(ParaFormat) };
            _ = Win32.SendMessageW(NativeHandle, EmGetParaFormat, 0, (nint)(&format));
            if ((format.Mask & PfmTabStops) == 0) { return []; }
            int[] tabs = new int[format.TabCount];
            // RichEdit's PARAFORMAT contract limits cTabCount to 32.
            for (int i = 0; i < tabs.Length; i++)
            {
                tabs[i] = TwipToPixel(format.Tabs[i], xDirection: true);
            }
            return tabs;
        }
        set
        {
            if (value is not null && value.Length > MaxTabStops)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "The number of tab stops must not exceed 32.");
            }
            if (!IsHandleCreated) { CreateHandle(); }
            ParaFormat format = new() { Size = (uint)sizeof(ParaFormat) };
            // The fixed WinForms setter preserves the other returned fields,
            // including tab slots beyond the newly assigned count.
            _ = Win32.SendMessageW(NativeHandle, EmGetParaFormat, 0, (nint)(&format));
            format.TabCount = (short)(value?.Length ?? 0);
            format.Mask = PfmTabStops;
            for (int i = 0; i < format.TabCount; i++)
            {
                format.Tabs[i] = PixelToTwip(value![i], xDirection: true);
            }
            _ = Win32.SendMessageW(NativeHandle, EmSetParaFormat, 0, (nint)(&format));
        }
    }

    private unsafe int GetSelectionParagraphIndent(uint mask)
    {
        if (!IsHandleCreated) { CreateHandle(); }
        ParaFormat format = new() { Size = (uint)sizeof(ParaFormat) };
        _ = Win32.SendMessageW(NativeHandle, EmGetParaFormat, 0, (nint)(&format));
        int twips = (format.Mask & mask) == 0 ? 0 : mask switch
        {
            PfmStartIndent => format.StartIndent,
            PfmRightIndent => format.RightIndent,
            _ => format.Offset
        };
        return TwipToPixel(twips, xDirection: true);
    }

    private unsafe void SetSelectionParagraphIndent(uint mask, int pixels)
    {
        if (!IsHandleCreated) { CreateHandle(); }
        ParaFormat format = new() { Size = (uint)sizeof(ParaFormat), Mask = mask };
        int twips = PixelToTwip(pixels, xDirection: true);
        switch (mask)
        {
            case PfmStartIndent: format.StartIndent = twips; break;
            case PfmRightIndent: format.RightIndent = twips; break;
            default: format.Offset = twips; break;
        }
        _ = Win32.SendMessageW(NativeHandle, EmSetParaFormat, 0, (nint)(&format));
    }

    // WinForms converts using a fresh screen DC, rather than the control DPI.
    // Preserve its floating-point operation order and truncation at the cast.
    private static int PixelToTwip(int pixels, bool xDirection)
        => (int)((((double)pixels) / GetRichTextLogPixels(xDirection)) * 72.0 * 20.0);

    private static int TwipToPixel(int twips, bool xDirection)
        => (int)(((twips / 20.0) / 72.0) * GetRichTextLogPixels(xDirection));

    private static int GetRichTextLogPixels(bool xDirection)
    {
        nint screenDc = Win32.GetDC(0);
        if (screenDc == 0) { throw new Win32Exception(Marshal.GetLastWin32Error()); }
        try
        {
            return Win32.GetDeviceCaps(screenDc, xDirection ? Win32.LOGPIXELSX : Win32.LOGPIXELSY);
        }
        finally { _ = Win32.ReleaseDC(0, screenDc); }
    }

    private unsafe bool InternalSetForeColor(Color value)
    {
        CharFormat2 format = new() { Size = (uint)sizeof(CharFormat2) };
        _ = Win32.SendMessageW(Handle, EmGetCharFormat, 0, (nint)(&format));
        if ((format.Mask & CfmColor) != 0
            && ColorTranslator.ToWin32(value) == format.TextColor)
        {
            return true;
        }

        format.Mask = CfmColor;
        format.Effects = 0;
        format.TextColor = ColorTranslator.ToWin32(value);
        return Win32.SendMessageW(Handle, EmSetCharFormat, 4, (nint)(&format)) != 0;
    }

    private unsafe CharFormat2 GetSelectionCharFormat()
    {
        CharFormat2 format = new() { Size = (uint)sizeof(CharFormat2) };
        _ = Win32.SendMessageW(NativeHandle, EmGetCharFormat, 1, (nint)(&format));
        return format;
    }

    private unsafe Font? GetCharFormatFont(bool selectionOnly)
    {
        if (!IsHandleCreated) { CreateHandle(); }
        CharFormat2 format = new() { Size = (uint)sizeof(CharFormat2) };
        _ = Win32.SendMessageW(NativeHandle, EmGetCharFormat, selectionOnly ? 1 : 0, (nint)(&format));
        if ((format.Mask & CfmFace) == 0) { return null; }

        float size = 13;
        if ((format.Mask & CfmSize) != 0)
        {
            size = format.Height / 20.0f;
            if (size == 0 && format.Height > 0) { size = 1; }
        }
        FontStyle style = FontStyle.Regular;
        if ((format.Mask & format.Effects & 1) != 0) { style |= FontStyle.Bold; }
        if ((format.Mask & format.Effects & 2) != 0) { style |= FontStyle.Italic; }
        if ((format.Mask & format.Effects & 8) != 0) { style |= FontStyle.Strikeout; }
        if ((format.Mask & format.Effects & 4) != 0) { style |= FontStyle.Underline; }

        ReadOnlySpan<char> face = new(format.FaceName, 32);
        int terminator = face.IndexOf('\0');
        string name = new(terminator < 0 ? face : face[..terminator]);
        try
        {
            return new Font(name, size, style, GraphicsUnit.Point, format.CharSet);
        }
        catch
        {
            // The fixed WinForms font conversion returns null when Drawing
            // cannot construct this native format. Handle/message failures
            // occur above and retain their normal exception behavior.
            return null;
        }
    }

    private unsafe void SetCharFormatFont(bool selectionOnly, Font value)
    {
        // Preserve the standard order, including a runtime null assignment:
        // create the window before inspecting the supplied Font.
        if (!IsHandleCreated) { CreateHandle(); }
        uint effects = 0;
        if (value.Bold) { effects |= 1; }
        if (value.Italic) { effects |= 2; }
        if (value.Strikeout) { effects |= 8; }
        if (value.Underline) { effects |= 4; }

        FontLogFont logicalFont = GetLogicalFont(value);
        CharFormat2 format = new()
        {
            Size = (uint)sizeof(CharFormat2),
            Mask = CfmFace | CfmSize | CfmCharSet | 0x0F,
            Effects = effects,
            Height = (int)(value.SizeInPoints * 20),
            CharSet = logicalFont.CharSet,
            PitchAndFamily = logicalFont.PitchAndFamily
        };
        new ReadOnlySpan<char>(logicalFont.FaceName, 32).CopyTo(new Span<char>(format.FaceName, 32));
        _ = Win32.SendMessageW(Handle, EmSetCharFormat, selectionOnly ? 1 : 4, (nint)(&format));
    }

    private static unsafe FontLogFont GetLogicalFont(Font font)
    {
        // Font.ToHfont uses Drawing's typed LOGFONT conversion. Read its native
        // result into a blittable buffer rather than using object marshaling
        // or retaining/disposing the caller's managed Font.
        nint handle = font.ToHfont();
        try
        {
            FontLogFont logicalFont = default;
            if (GetFontObject(handle, sizeof(FontLogFont), &logicalFont) != sizeof(FontLogFont))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            return logicalFont;
        }
        finally { _ = Win32.DeleteObject(handle); }
    }

    [DllImport("gdi32.dll", EntryPoint = "GetObjectW", ExactSpelling = true, SetLastError = true)]
    private static extern unsafe int GetFontObject(nint font, int bytes, FontLogFont* logicalFont);

    /// <summary>Returns the index of the character nearest to the given point.</summary>
    public override unsafe int GetCharIndexFromPosition(Point pt)
    {
        Win32.POINT position = new() { x = pt.X, y = pt.Y };
        int index = (int)Win32.SendMessageW(Handle, Win32.EM_CHARFROMPOS, 0, (nint)(&position));
        string text = Text;
        if (index >= text.Length)
        {
            index = Math.Max(text.Length - 1, 0);
        }
        return index;
    }

    /// <summary>Returns the physical line containing the specified character index.</summary>
    public override int GetLineFromCharIndex(int index)
        => (int)Win32.SendMessageW(Handle, EmExLineFromChar, 0, index);

    /// <summary>Returns the location of the character at the given index, including the end position.</summary>
    public override unsafe Point GetPositionFromCharIndex(int index)
    {
        if (index < 0 || index > Text.Length) { return Point.Empty; }
        // RICHEDIT50W uses a POINTL buffer, unlike the packed EDIT result.
        Win32.POINT position = default;
        _ = Win32.SendMessageW(Handle, Win32.EM_POSFROMCHAR, (nint)(&position), index);
        return new Point(position.x, position.y);
    }

    protected override Size DefaultSize => new(100, 96);

    protected override string ClassName => "RICHEDIT50W";

    protected override CreateParams CreateParams
    {
        get
        {
            _ = s_richEditModule.Value;
            CreateParams parameters = base.CreateParams;
            parameters.ClassName = "RICHEDIT50W";
            if (Multiline)
            {
                if (((int)ScrollBars & 1) != 0 && !WordWrap)
                {
                    parameters.Style |= (int)Win32.WS_HSCROLL;
                    if (((int)ScrollBars & 16) != 0) { parameters.Style |= EsDisableNoScroll; }
                }
                if (((int)ScrollBars & 2) != 0)
                {
                    parameters.Style |= (int)Win32.WS_VSCROLL;
                    if (((int)ScrollBars & 16) != 0) { parameters.Style |= EsDisableNoScroll; }
                }
            }
            if (BorderStyle == BorderStyle.FixedSingle && (parameters.Style & 0x00800000) != 0)
            {
                parameters.Style &= ~0x00800000;
                parameters.ExStyle |= (int)Win32.WS_EX_CLIENTEDGE;
            }
            return parameters;
        }
    }

    internal override void UpdateMaxLength()
    {
        if (IsHandleCreated)
        {
            _ = Win32.SendMessageW(NativeHandle, EmExLimitText, 0, MaxLength);
        }
    }

    protected override void OnBackColorChanged(EventArgs e)
    {
        if (IsHandleCreated)
        {
            int color = ColorTranslator.ToWin32(BackColor);
            _ = Win32.SendMessageW(Handle, EmSetBackgroundColor, 0, color);
        }
        base.OnBackColorChanged(e);
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        _currentSelectionStart = _currentSelectionEnd = _currentSelectionType = -1;
        nint initialModified = Win32.SendMessageW(NativeHandle, Win32.EM_GETMODIFY, 0, 0);
        UpdateMaxLength();
        // Hidden RichEdit controls otherwise omit the notifications consumed here.
        _ = Win32.SendMessageW(NativeHandle, EmSetEventMask, 0,
            EnmChange | EnmUpdate | EnmScroll | EnmScrollEvents | EnmRequestResize | EnmSelChange | EnmImeChange);
        _ = Win32.SendMessageW(NativeHandle, EmAutoUrlDetect, DetectUrls ? 1 : 0, 0);
        if (_selectionBackColorToSetOnHandleCreated != Color.Empty)
        {
            SelectionBackColor = _selectionBackColorToSetOnHandleCreated;
        }
        AutoWordSelection = AutoWordSelection;
        // Set the default colors before loading text/RTF, so automatic text
        // colors use the control foreground while explicit RTF colors survive.
        int background = ColorTranslator.ToWin32(BackColor);
        _ = Win32.SendMessageW(Handle, EmSetBackgroundColor, 0, background);
        _ = InternalSetForeColor(ForeColor);
        // Formatting the new window is initialization, not a document edit.
        // Restore its original bit before the base restores the old true bit.
        _ = Win32.SendMessageW(NativeHandle, Win32.EM_SETMODIFY, initialModified, 0);
        base.OnHandleCreated(e);
        if (_textRtf is not null)
        {
            nint modified = Win32.SendMessageW(NativeHandle, Win32.EM_GETMODIFY, 0, 0);
            string text = _textRtf;
            _textRtf = null;
            Rtf = text;
            _ = Win32.SendMessageW(NativeHandle, Win32.EM_SETMODIFY, modified, 0);
        }
        else if (_textPlain is not null)
        {
            string text = _textPlain;
            _textPlain = null;
            Text = text;
        }
        RestoreSelectionOnHandle();
        ClearUndo();
    }

    protected override void CreateHandle()
    {
        _creatingRichHandle = true;
        try { base.CreateHandle(); }
        finally { _creatingRichHandle = false; }
    }

    protected override void OnHandleDestroyed(EventArgs e)
    {
        base.OnHandleDestroyed(e);
        if (NativeHandle != 0)
        {
            _textRtf = StreamRtfOut(selection: false);
            if (_textRtf.Length == 0) { _textRtf = null; }
        }
    }

    protected override bool OnCommand(int notificationCode)
    {
        if (notificationCode == Win32.EN_CHANGE)
        {
            if (!_creatingRichHandle) { OnTextChanged(EventArgs.Empty); }
            return true;
        }
        if (!_creatingRichHandle)
        {
            if (notificationCode == EnHScroll) { OnHScroll(EventArgs.Empty); return true; }
            if (notificationCode == EnVScroll) { OnVScroll(EventArgs.Empty); return true; }
        }
        return base.OnCommand(notificationCode);
    }

    protected override unsafe bool OnNotify(int notificationCode, nint lParam)
    {
        if (notificationCode == EnSelChange)
        {
            HandleSelectionChange(*(SelectionChange*)lParam);
            return true;
        }
        if (notificationCode != EnRequestResize) { return base.OnNotify(notificationCode, lParam); }
        if (!_suppressContentsResized)
        {
            RequestResize* resize = (RequestResize*)lParam;
            if (BorderStyle == BorderStyle.Fixed3D) { resize->Bounds.Bottom++; }
            Win32.RECT bounds = resize->Bounds;
            OnContentsResized(new ContentsResizedEventArgs(
                Rectangle.FromLTRB(bounds.Left, bounds.Top, bounds.Right, bounds.Bottom)));
        }
        return true;
    }

    private void HandleSelectionChange(SelectionChange change)
    {
        int start = change.Range.Minimum;
        int end = change.Range.Maximum;
        short type = unchecked((short)change.Type);
        // Keep the fixed RichEdit Hangul composition/MaxLength workaround
        // before duplicate suppression, even for an unchanged selection.
        if (ImeMode is ImeMode.Hangul or ImeMode.HangulFull)
        {
            nint compositionMode = Win32.SendMessageW(Handle, EmGetImeCompMode, 0, 0);
            if (compositionMode != 0)
            {
                int length = Win32.GetWindowTextLengthW(Handle);
                if (start == end && length == MaxLength)
                {
                    _ = Win32.SendMessageW(Handle, Win32.WM_KILLFOCUS, 0, 0);
                    _ = Win32.SendMessageW(Handle, Win32.WM_SETFOCUS, 0, 0);
                    _ = Win32.PostMessageW(Handle, Win32.EM_SETSEL, unchecked(end - 1), end);
                }
            }
        }
        if (start != _currentSelectionStart || end != _currentSelectionEnd || type != _currentSelectionType)
        {
            _currentSelectionStart = start;
            _currentSelectionEnd = end;
            _currentSelectionType = type;
            OnSelectionChanged(EventArgs.Empty);
        }
    }

    protected override void WndProc(ref Message m)
    {
        if (m.Msg == Win32.WM_IME_NOTIFY)
        {
            OnImeChange(EventArgs.Empty);
            base.WndProc(ref m);
            return;
        }
        if (m.Msg is Win32.WM_HSCROLL or Win32.WM_VSCROLL)
        {
            bool vertical = m.Msg == Win32.WM_VSCROLL;
            base.WndProc(ref m);
            int command = unchecked((ushort)m.WParam);
            if (command is 4 or 5)
            {
                if (vertical) { OnVScroll(EventArgs.Empty); }
                else { OnHScroll(EventArgs.Empty); }
            }
            return;
        }
        base.WndProc(ref m);
    }

    private unsafe string GetRichText(bool selection = false)
    {
        GetTextLengthEx lengthParameters = new() { CodePage = 1200 };
        int expectedLength = (int)Win32.SendMessageW(NativeHandle, EmGetTextLengthEx,
            (nint)(&lengthParameters), 0);
        if (expectedLength == unchecked((int)0x80070057)) { throw new Win32Exception(expectedLength); }
        int capacity = checked(expectedLength + 1);
        char[] buffer = new char[capacity];
        GetTextEx parameters = new()
        {
            Bytes = checked((uint)capacity * sizeof(char)),
            Flags = selection ? 2u : 0u,
            CodePage = 1200
        };
        fixed (char* pointer = buffer)
        {
            int actualLength = (int)Win32.SendMessageW(NativeHandle, EmGetTextEx,
                (nint)(&parameters), (nint)pointer);
            Span<char> text = buffer.AsSpan(0, actualLength);
            text.Replace('\r', '\n');
            return new string(text);
        }
    }

    private void StreamPlainText(string value, bool selection)
        => StreamText(value, SfTextUnicode | (selection ? SffSelection : 0));

    private unsafe void StreamText(string value, int flags)
    {
        bool selection = (flags & SffSelection) != 0;
        if (value.Length == 0)
        {
            if (selection) { _ = Win32.SendMessageW(NativeHandle, WmClear, 0, 0); }
            else { _ = Win32.SendMessageW(NativeHandle, WmSetText, 0, string.Empty); }
            return;
        }

        int terminator = value.IndexOf('\0');
        if (terminator >= 0) { value = value[..terminator]; }
        var context = new TextStreamContext(((flags & 0x10) != 0
            ? Encoding.Unicode : RichTextEncoding).GetBytes(value));
        GCHandle cookie = GCHandle.Alloc(context);
        try
        {
            if (!selection)
            {
                CharacterRange range = default;
                _ = Win32.SendMessageW(NativeHandle, EmExSetSel, 0, (nint)(&range));
            }

            // Match the fixed WinForms stream contract: even rejected full
            // documents first collapse the selection, but do not change limits.
            if ((flags & SfRtf) != 0)
            {
                if (context.Bytes.Length < 5)
                {
                    throw new ArgumentException("Invalid file format.", new EndOfStreamException());
                }
                if (!context.Bytes.AsSpan().StartsWith("{\\rtf"u8))
                {
                    throw new ArgumentException("Invalid file format.");
                }
            }

            EditStream stream = new()
            {
                Cookie = (nuint)GCHandle.ToIntPtr(cookie),
                Callback = &ReadTextStream
            };
            _ = Win32.SendMessageW(NativeHandle, EmExLimitText, 0, int.MaxValue);
            _ = Win32.SendMessageW(NativeHandle, EmStreamIn,
                flags, (nint)(&stream));
            UpdateMaxLength();
            if (stream.Error != 0 || context.Failure is not null)
            {
                throw new InvalidOperationException("Failed to load text into the rich text box.", context.Failure);
            }
            _ = Win32.SendMessageW(NativeHandle, Win32.EM_SETMODIFY, (nint)(-1), 0);
            _ = Win32.SendMessageW(NativeHandle, EmGetLineCount, 0, 0);
        }
        finally { cookie.Free(); }
    }

    private static Encoding RichTextEncoding
        => CodePagesEncodingProvider.Instance.GetEncoding(0) ?? Encoding.UTF8;

    private unsafe string StreamRtfOut(bool selection)
    {
        using var output = new MemoryStream();
        var context = new OutputStreamContext(output);
        GCHandle cookie = GCHandle.Alloc(context);
        try
        {
            EditStream stream = new()
            {
                Cookie = (nuint)GCHandle.ToIntPtr(cookie),
                Callback = &WriteTextStream
            };
            _ = Win32.SendMessageW(NativeHandle, EmStreamOut,
                SfRtf | (selection ? SffSelection : 0), (nint)(&stream));
            if (stream.Error != 0 || context.Failure is not null)
            {
                throw new InvalidOperationException("Failed to save text from the rich text box.", context.Failure);
            }
            string result = RichTextEncoding.GetString(output.GetBuffer().AsSpan(0, checked((int)output.Length)));
            return result.EndsWith('\0') ? result[..^1] : result;
        }
        finally { cookie.Free(); }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
    private static unsafe uint WriteTextStream(nuint cookie, byte* buffer, int count, int* written)
    {
        OutputStreamContext? context = null;
        try
        {
            *written = 0;
            context = (OutputStreamContext)GCHandle.FromIntPtr((nint)cookie).Target!;
            context.Output.Write(new ReadOnlySpan<byte>(buffer, count));
            *written = count;
            return 0;
        }
        catch (Exception exception)
        {
            if (context is not null) { context.Failure = exception; }
            return 1;
        }
    }

    private sealed class OutputStreamContext(MemoryStream output)
    {
        internal readonly MemoryStream Output = output;
        internal Exception? Failure;
    }

    // RichEdit consumes this callback synchronously. A separate cookie for each
    // invocation keeps the managed bytes rooted across GC and finite reentry.
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
    private static unsafe uint ReadTextStream(nuint cookie, byte* buffer, int count, int* written)
    {
        TextStreamContext? context = null;
        try
        {
            *written = 0;
            context = (TextStreamContext)GCHandle.FromIntPtr((nint)cookie).Target!;
            int length = Math.Min(count, context.Bytes.Length - context.Position);
            context.Bytes.AsSpan(context.Position, length).CopyTo(new Span<byte>(buffer, length));
            context.Position += length;
            *written = length;
            return 0;
        }
        catch (Exception exception)
        {
            if (context is not null) { context.Failure = exception; }
            return 1;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    private struct RequestResize
    {
        internal Win32.NMHDR Header;
        internal Win32.RECT Bounds;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    private struct SelectionChange
    {
        internal Win32.NMHDR Header;
        internal CharacterRange Range;
        internal ushort Type;
    }

    // The LOGFONT buffer contains only native scalar values and an inline name.
    [StructLayout(LayoutKind.Sequential)]
    private unsafe struct FontLogFont
    {
        internal int Height;
        internal int Width;
        internal int Escapement;
        internal int Orientation;
        internal int Weight;
        internal byte Italic;
        internal byte Underline;
        internal byte StrikeOut;
        internal byte CharSet;
        internal byte OutPrecision;
        internal byte ClipPrecision;
        internal byte Quality;
        internal byte PitchAndFamily;
        internal fixed char FaceName[32];
    }

    // Richedit.h packs the character and paragraph formats at four bytes.
    // Inline buffers keep the synchronous payloads blittable on the AOT path.
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    private unsafe struct CharFormat2
    {
        internal uint Size;
        internal uint Mask;
        internal uint Effects;
        internal int Height;
        internal int Offset;
        internal int TextColor;
        internal byte CharSet;
        internal byte PitchAndFamily;
        internal fixed char FaceName[32];
        internal ushort Weight;
        internal short Spacing;
        internal int BackColor;
        internal uint Locale;
        internal uint Reserved;
        internal short Style;
        internal ushort Kerning;
        internal byte UnderlineType;
        internal byte Animation;
        internal byte RevisionAuthor;
        internal byte Reserved1;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    private unsafe struct ParaFormat
    {
        internal uint Size;
        internal uint Mask;
        internal ushort Numbering;
        internal ushort Effects;
        internal int StartIndent;
        internal int RightIndent;
        internal int Offset;
        internal ushort Alignment;
        internal short TabCount;
        internal fixed int Tabs[32];
    }

    private sealed class TextStreamContext(byte[] bytes)
    {
        internal readonly byte[] Bytes = bytes;
        internal int Position;
        internal Exception? Failure;
    }

    // Richedit.h uses pack(4), including on x64: EDITSTREAM is 20 bytes
    // with its callback at offset 12; GETTEXTEX is 28 bytes.
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    private unsafe struct EditStream
    {
        internal nuint Cookie;
        internal uint Error;
        internal delegate* unmanaged[Stdcall]<nuint, byte*, int, int*, uint> Callback;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    private struct GetTextEx
    {
        internal uint Bytes;
        internal uint Flags;
        internal uint CodePage;
        internal nint DefaultCharacter;
        internal nint UsedDefaultCharacter;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct GetTextLengthEx
    {
        internal uint Flags;
        internal uint CodePage;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct CharacterRange
    {
        internal int Minimum;
        internal int Maximum;
    }

    private static nint LoadRichEditModule()
    {
        nint module = Win32.LoadLibraryExW("Msftedit.dll", 0, 0x00000800 /* LOAD_LIBRARY_SEARCH_SYSTEM32 */);
        if (unchecked((nuint)module) < 32)
        {
            throw new Win32Exception(Marshal.GetLastWin32Error(), "Failed to load the system RichEdit library.");
        }
        return module;
    }

    /// <inheritdoc />
    protected override void OnHandleCreated()
    {
        base.OnHandleCreated();
        ApplyTheme();
    }

    /// <inheritdoc />
    protected override void ApplyTheme()
    {
        base.ApplyTheme();

        if (NativeHandle == 0)
        {
            return;
        }

        // Theme refresh also runs during base color notifications and handle
        // creation. Keep the native background consistent with the public value.
        int background = ColorTranslator.ToWin32(BackColor);
        _ = Win32.SendMessageW(Handle, EmSetBackgroundColor, 0, background);
    }
}
