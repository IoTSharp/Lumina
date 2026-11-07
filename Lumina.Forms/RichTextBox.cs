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
    private const int EmStreamIn = 0x0449;
    private const int EmGetTextEx = 0x045E;
    private const int EmGetTextLengthEx = 0x045F;
    private const int SfTextUnicode = 0x0011;
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

    // The registered superclass retains pointers into Msftedit. Keep its one
    // system-directory load alive for the same process lifetime as that cache.
    private static readonly Lazy<nint> s_richEditModule = new(LoadRichEditModule);

    private string? _textPlain;
    private bool _creatingRichHandle;
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

    [Localizable(true)]
    [RefreshProperties(RefreshProperties.All)]
    [AllowNull]
    public override string Text
    {
        get
        {
            if (IsDisposed) { return base.Text; }
            if (RecreatingHandle || GetAnyDisposingInHierarchy()) { return string.Empty; }
            return IsHandleCreated ? GetRichText() : _textPlain ?? base.Text;
        }
        set
        {
            using LayoutTransaction transaction = new(AutoSize ? Parent : null, this, nameof(Text));
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
        if (_textPlain is not null)
        {
            nint modified = Win32.SendMessageW(NativeHandle, Win32.EM_GETMODIFY, 0, 0);
            string text = _textPlain;
            _textPlain = null;
            Text = text;
            if (modified != 0)
            {
                _ = Win32.SendMessageW(NativeHandle, Win32.EM_SETMODIFY, modified, 0);
            }
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
        // Only plain text is retained in this slice. RTF/format persistence is
        // still tracked separately and must not be inferred from this cache.
        if (NativeHandle != 0) { _textPlain = GetRichText(); }
    }

    protected override bool OnCommand(int notificationCode)
    {
        if (notificationCode == Win32.EN_CHANGE)
        {
            if (!_creatingRichHandle) { OnTextChanged(EventArgs.Empty); }
            return true;
        }
        return base.OnCommand(notificationCode);
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

    private unsafe void StreamPlainText(string value, bool selection)
    {
        if (value.Length == 0)
        {
            if (selection) { _ = Win32.SendMessageW(NativeHandle, WmClear, 0, 0); }
            else { _ = Win32.SendMessageW(NativeHandle, WmSetText, 0, string.Empty); }
            return;
        }

        int terminator = value.IndexOf('\0');
        if (terminator >= 0) { value = value[..terminator]; }
        var context = new TextStreamContext(Encoding.Unicode.GetBytes(value));
        GCHandle cookie = GCHandle.Alloc(context);
        try
        {
            if (!selection)
            {
                CharacterRange range = default;
                _ = Win32.SendMessageW(NativeHandle, EmExSetSel, 0, (nint)(&range));
            }

            EditStream stream = new()
            {
                Cookie = (nuint)GCHandle.ToIntPtr(cookie),
                Callback = &ReadTextStream
            };
            _ = Win32.SendMessageW(NativeHandle, EmExLimitText, 0, int.MaxValue);
            _ = Win32.SendMessageW(NativeHandle, EmStreamIn,
                SfTextUnicode | (selection ? SffSelection : 0), (nint)(&stream));
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

        ThemePalette palette = CurrentVisualStyle.Palette;
        _ = Win32.SendMessageW(NativeHandle, Win32.EM_SETBKGNDCOLOR, 0, unchecked((nint)Win32.ToColorRef(palette.ControlBackground)));
    }
}
