// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// Portions adapted from dotnet/winforms v10.0.11 for Lumina.Forms.

using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Lumina.Forms;

/// <summary>
/// Represents a multi-line rich text edit control.
/// </summary>
public class RichTextBox : TextBoxBase
{
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

    // The registered superclass retains pointers into Msftedit. Keep its one
    // system-directory load alive for the same process lifetime as that cache.
    private static readonly Lazy<nint> s_richEditModule = new(LoadRichEditModule);

    private Color _selectionColor = Color.Empty;
    private bool _autoWordSelection;
    private bool _detectUrls;
    private RichTextBoxScrollBars _scrollBars;

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

    public Color SelectionColor
    {
        get => _selectionColor;
        set => _selectionColor = value;
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

    protected override void OnHandleCreated(EventArgs e)
    {
        UpdateMaxLength();
        // Enable only the notifications currently handled by TextBoxBase.
        // Hidden RichEdit controls otherwise omit these edit notifications.
        _ = Win32.SendMessageW(NativeHandle, EmSetEventMask, 0, EnmChange | EnmUpdate);
        _ = Win32.SendMessageW(NativeHandle, EmAutoUrlDetect, DetectUrls ? 1 : 0, 0);
        AutoWordSelection = AutoWordSelection;
        base.OnHandleCreated(e);
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

        ThemePalette palette = CurrentVisualStyle.Palette;
        _ = Win32.SendMessageW(NativeHandle, Win32.EM_SETBKGNDCOLOR, 0, unchecked((nint)Win32.ToColorRef(palette.ControlBackground)));
    }
}
