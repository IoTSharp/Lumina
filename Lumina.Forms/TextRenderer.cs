// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// Portions adapted from dotnet/winforms v10.0.11 for Lumina.Forms.

using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Lumina.Forms;

/// <summary>Provides GDI text measurement.</summary>
public static class TextRenderer
{
    private static readonly Size s_maxSize = new(int.MaxValue, int.MaxValue);

    /// <summary>Measures text using the primary display's device context.</summary>
    public static Size MeasureText(string? text, Font? font)
        => MeasureTextInternal(text, font, s_maxSize);

    /// <summary>Measures text using the primary display's device context.</summary>
    public static Size MeasureText(ReadOnlySpan<char> text, Font? font)
        => MeasureTextInternal(text, font, s_maxSize);

    /// <summary>Measures text within the proposed size.</summary>
    public static Size MeasureText(string? text, Font? font, Size proposedSize)
        => MeasureTextInternal(text, font, proposedSize);

    /// <summary>Measures text within the proposed size.</summary>
    public static Size MeasureText(ReadOnlySpan<char> text, Font? font, Size proposedSize)
        => MeasureTextInternal(text, font, proposedSize);

    /// <summary>Measures text using the proposed size and formatting options.</summary>
    public static Size MeasureText(string? text, Font? font, Size proposedSize, TextFormatFlags flags)
        => MeasureTextInternal(text, font, proposedSize, flags);

    /// <summary>Measures text using the proposed size and formatting options.</summary>
    /// <exception cref="ArgumentOutOfRangeException">The flags include ModifyString.</exception>
    public static Size MeasureText(ReadOnlySpan<char> text, Font? font, Size proposedSize, TextFormatFlags flags)
        => MeasureTextInternal(text, font, proposedSize, BlockModifyString(flags));

    private static TextFormatFlags BlockModifyString(TextFormatFlags flags)
    {
#pragma warning disable CS0618 // The fixed .NET 10 contract rejects this flag only for spans.
        if ((flags & TextFormatFlags.ModifyString) != 0)
        {
            throw new ArgumentOutOfRangeException(nameof(flags), "ModifyString is not allowed when formatting a span.");
        }
#pragma warning restore CS0618
        return flags;
    }

    private static unsafe Size MeasureTextInternal(ReadOnlySpan<char> text, Font? font,
        Size proposedSize, TextFormatFlags flags = TextFormatFlags.Bottom)
    {
        if (text.IsEmpty) { return Size.Empty; }

        nint screenDc = Win32.GetDC(0);
        if (screenDc == 0) { throw new Win32Exception(Marshal.GetLastWin32Error()); }
        nint nativeFont = 0;
        nint previousFont = 0;
        try
        {
            if (font is not null)
            {
                nativeFont = CreateMeasurementFont(font);
                previousFont = Win32.SelectObject(screenDc, nativeFont);
            }

            // TextRenderer-specific options are never passed as DrawTextEx flags.
            uint nativeFlags = (uint)flags & 0x00FF_FFFF;
            int leftMargin = 0;
            int rightMargin = 0;
            bool extraPadding = (flags & TextFormatFlags.LeftAndRightPadding) != 0;
            if (extraPadding || (flags & TextFormatFlags.NoPadding) == 0)
            {
                _ = Win32.GetTextMetricsW(screenDc, out Win32.TEXTMETRICW metrics);
                float overhangPadding = metrics.tmHeight / 6f;
                leftMargin = (int)Math.Ceiling(overhangPadding * (extraPadding ? 2 : 1));
                rightMargin = (int)Math.Ceiling(overhangPadding * (extraPadding ? 2.5f : 1.5f));
            }

            var parameters = new Win32.DRAWTEXTPARAMS
            {
                cbSize = (uint)sizeof(Win32.DRAWTEXTPARAMS),
                iLeftMargin = leftMargin,
                iRightMargin = rightMargin
            };

            int minimumWidth = 1 + leftMargin + rightMargin;
            if (proposedSize.Width <= minimumWidth) { proposedSize.Width = minimumWidth; }
            if (proposedSize.Height <= 0) { proposedSize.Height = 1; }

            if (proposedSize.Height == int.MaxValue && (nativeFlags & (uint)TextFormatFlags.SingleLine) != 0)
            {
                nativeFlags &= ~((uint)TextFormatFlags.Bottom | (uint)TextFormatFlags.VerticalCenter);
            }
            if (proposedSize.Width == int.MaxValue)
            {
                nativeFlags &= ~(uint)TextFormatFlags.WordBreak;
            }

            var bounds = new Win32.RECT { Right = proposedSize.Width, Bottom = proposedSize.Height };
            fixed (char* characters = text)
            {
                _ = Win32.DrawTextExW(screenDc, characters, text.Length, ref bounds,
                    nativeFlags | 0x0000_0400 /* DT_CALCRECT */, ref parameters);
            }
            return new Size(bounds.Width, bounds.Height);
        }
        finally
        {
            if (previousFont != 0) { _ = Win32.SelectObject(screenDc, previousFont); }
            if (nativeFont != 0) { _ = Win32.DeleteObject(nativeFont); }
            _ = Win32.ReleaseDC(0, screenDc);
        }
    }

    private static nint CreateMeasurementFont(Font font)
    {
        string familyName = font.FontFamily.Name;
        if (familyName.Length > 1 && familyName[0] == '@') { familyName = familyName[1..]; }
        if (familyName.Length == 0) { familyName = "Microsoft Sans Serif"; }

        // FontCache uses an initial system-DPI snapshot and rounds upward. The
        // control-font helper uses different rounding and rendering quality.
        int height = -(int)Math.Ceiling(Win32.InitialSystemDpi * font.SizeInPoints / 72);
        nint handle = CreateFont(familyName, height, font, outputPrecision: 4 /* OUT_TT_PRECIS */);
        if (handle == 0)
        {
            handle = CreateFont("Microsoft Sans Serif", height, font, outputPrecision: 7 /* OUT_TT_ONLY_PRECIS */);
        }
        if (handle == 0) { throw new Win32Exception(Marshal.GetLastWin32Error()); }
        return handle;
    }

    private static nint CreateFont(string familyName, int height, Font font, uint outputPrecision)
        => Win32.CreateFontW(height, 0, 0, 0, font.Bold ? 700 : 400,
            font.Italic ? 1u : 0u, font.Underline ? 1u : 0u, font.Strikeout ? 1u : 0u,
            font.GdiCharSet, outputPrecision, 0, 0 /* DEFAULT_QUALITY */, 0, familyName);
}
