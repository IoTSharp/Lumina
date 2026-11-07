using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextRendererMeasureTests
{
    [Theory]
    [InlineData(nameof(TextFormatFlags.Bottom), 8)]
    [InlineData(nameof(TextFormatFlags.Default), 0)]
    [InlineData(nameof(TextFormatFlags.EndEllipsis), 32768)]
    [InlineData(nameof(TextFormatFlags.ExpandTabs), 64)]
    [InlineData(nameof(TextFormatFlags.ExternalLeading), 512)]
    [InlineData(nameof(TextFormatFlags.GlyphOverhangPadding), 0)]
    [InlineData(nameof(TextFormatFlags.HidePrefix), 1048576)]
    [InlineData(nameof(TextFormatFlags.HorizontalCenter), 1)]
    [InlineData(nameof(TextFormatFlags.Internal), 4096)]
    [InlineData(nameof(TextFormatFlags.Left), 0)]
    [InlineData(nameof(TextFormatFlags.LeftAndRightPadding), 536870912)]
    [InlineData(nameof(TextFormatFlags.NoClipping), 256)]
    [InlineData(nameof(TextFormatFlags.NoFullWidthCharacterBreak), 524288)]
    [InlineData(nameof(TextFormatFlags.NoPadding), 268435456)]
    [InlineData(nameof(TextFormatFlags.NoPrefix), 2048)]
    [InlineData(nameof(TextFormatFlags.PathEllipsis), 16384)]
    [InlineData(nameof(TextFormatFlags.PrefixOnly), 2097152)]
    [InlineData(nameof(TextFormatFlags.PreserveGraphicsClipping), 16777216)]
    [InlineData(nameof(TextFormatFlags.PreserveGraphicsTranslateTransform), 33554432)]
    [InlineData(nameof(TextFormatFlags.Right), 2)]
    [InlineData(nameof(TextFormatFlags.RightToLeft), 131072)]
    [InlineData(nameof(TextFormatFlags.SingleLine), 32)]
    [InlineData(nameof(TextFormatFlags.TextBoxControl), 8192)]
    [InlineData(nameof(TextFormatFlags.Top), 0)]
    [InlineData(nameof(TextFormatFlags.VerticalCenter), 4)]
    [InlineData(nameof(TextFormatFlags.WordBreak), 16)]
    [InlineData(nameof(TextFormatFlags.WordEllipsis), 262144)]
    public void FormattingFlagsMatchFixedInventory(string name, int expected)
        => Assert.Equal(expected, typeof(TextFormatFlags).GetField(name)!.GetRawConstantValue());

    [Fact]
    public void StandardMeasurementOverloadsKeepTheirParameterContracts()
    {
        Assert.True(typeof(TextRenderer).IsAbstract && typeof(TextRenderer).IsSealed);
        MethodInfo[] methods = typeof(TextRenderer).GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Where(method => method.Name == nameof(TextRenderer.MeasureText)
                && (method.GetParameters()[0].ParameterType == typeof(string)
                    || method.GetParameters()[0].ParameterType == typeof(ReadOnlySpan<char>)))
            .ToArray();
        Assert.Equal(6, methods.Length);
        Assert.All(methods, method =>
        {
            Assert.Equal(nameof(TextRenderer.MeasureText), method.Name);
            Assert.Equal(typeof(Size), method.ReturnType);
            ParameterInfo[] parameters = method.GetParameters();
            Assert.Equal("text", parameters[0].Name);
            Assert.Equal(typeof(Font), parameters[1].ParameterType);
            Assert.Equal("font", parameters[1].Name);
            if (parameters.Length > 2)
            {
                Assert.Equal(typeof(Size), parameters[2].ParameterType);
                Assert.Equal("proposedSize", parameters[2].Name);
            }
            if (parameters.Length > 3)
            {
                Assert.Equal(typeof(TextFormatFlags), parameters[3].ParameterType);
                Assert.Equal("flags", parameters[3].Name);
            }
        });
        Assert.Equal(3, methods.Count(method => method.GetParameters()[0].ParameterType == typeof(string)));
        Assert.Equal(3, methods.Count(method => method.GetParameters()[0].ParameterType == typeof(ReadOnlySpan<char>)));
        Assert.NotNull(typeof(TextFormatFlags).GetCustomAttribute<FlagsAttribute>());
    }

    [Fact]
    public void ModifyStringKeepsTheFixedValueAndObsoleteMetadata()
    {
#pragma warning disable CS0618
        Assert.Equal(65536, (int)TextFormatFlags.ModifyString);
#pragma warning restore CS0618
        ObsoleteAttribute attribute = typeof(TextFormatFlags).GetField("ModifyString")!
            .GetCustomAttribute<ObsoleteAttribute>()!;
        Assert.False(attribute.IsError);
        Assert.Equal("ModifyString mutates strings and should be avoided. It will be blocked in a future release.", attribute.Message);
    }

    [Fact]
    public void EmptyAndNullReturnEmptyForAllOverloadsBeforeConsultingFont()
    {
        using var font = new Font("Arial", 12);
        font.Dispose();
        Assert.Equal(Size.Empty, TextRenderer.MeasureText((string?)null, font));
        Assert.Equal(Size.Empty, TextRenderer.MeasureText(string.Empty, font, new Size(-1, -2)));
        Assert.Equal(Size.Empty, TextRenderer.MeasureText((string?)null, font, Size.Empty, (TextFormatFlags)(-1)));
        Assert.Equal(Size.Empty, TextRenderer.MeasureText(ReadOnlySpan<char>.Empty, font));
        Assert.Equal(Size.Empty, TextRenderer.MeasureText(ReadOnlySpan<char>.Empty, font, Size.Empty));
        Assert.Equal(Size.Empty, TextRenderer.MeasureText(ReadOnlySpan<char>.Empty, font, Size.Empty, TextFormatFlags.NoPadding));
    }

    [Theory]
    [InlineData("")]
    [InlineData("abc")]
    public void SpanRejectsModifyStringBeforeEmptyOrFontHandling(string text)
    {
#pragma warning disable CS0618
        ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => TextRenderer.MeasureText(text.AsSpan(), null, Size.Empty, TextFormatFlags.ModifyString));
#pragma warning restore CS0618
        Assert.Equal("flags", exception.ParamName);
        Assert.Null(exception.ActualValue);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void StringEmptyDoesNotAdoptTheSpanModifyStringGuard(string? text)
    {
#pragma warning disable CS0618
        Assert.Equal(Size.Empty, TextRenderer.MeasureText(text, null, Size.Empty, TextFormatFlags.ModifyString));
#pragma warning restore CS0618
    }

    [Theory]
    [InlineData("Latin text")]
    [InlineData("中文测量")]
    [InlineData("A\uD83D\uDE00Z")]
    [InlineData("a & b")]
    public void NullFontMeasuresTheNativeScreenFont(string text)
    {
        Size expected = NativeNoPadding(text, null, new Size(600, 400), 0x0020 | 0x0800);
        Assert.True(expected.Width > 0 && expected.Height > 0);
        Assert.Equal(expected, TextRenderer.MeasureText(text, null, new Size(600, 400),
            TextFormatFlags.SingleLine | TextFormatFlags.NoPrefix | TextFormatFlags.NoPadding));
    }

    [Theory]
    [InlineData(FontStyle.Regular)]
    [InlineData(FontStyle.Bold)]
    [InlineData(FontStyle.Italic)]
    [InlineData(FontStyle.Bold | FontStyle.Italic)]
    public void SuppliedFontUsesRealGdiGlyphWidths(FontStyle style)
    {
        using var font = new Font("Arial", 12, style);
        const string text = "WWW iii 012345 中文";
        Size expected = NativeNoPadding(text, font, new Size(600, 400), 0x0020 | 0x0800);
        Assert.True(expected.Width > 0 && expected.Height > 0);
        Assert.Equal(expected, TextRenderer.MeasureText(text, font, new Size(600, 400),
            TextFormatFlags.SingleLine | TextFormatFlags.NoPrefix | TextFormatFlags.NoPadding));
    }

    [Theory]
    [InlineData(TextFormatFlags.Default)]
    [InlineData(TextFormatFlags.NoPadding)]
    [InlineData(TextFormatFlags.LeftAndRightPadding)]
    [InlineData(TextFormatFlags.WordBreak)]
    [InlineData(TextFormatFlags.SingleLine | TextFormatFlags.NoPrefix)]
    [InlineData(TextFormatFlags.RightToLeft | TextFormatFlags.WordBreak)]
    public void StringAndSpanMeasureTheSameLayout(TextFormatFlags flags)
    {
        using var font = new Font("Arial", 12);
        const string text = "first &second\r\n中文\tA\uD83D\uDE00Z";
        var proposal = new Size(110, 300);
        Assert.Equal(TextRenderer.MeasureText(text, font, proposal, flags),
            TextRenderer.MeasureText(text.AsSpan(), font, proposal, flags));
    }

    [Fact]
    public void DefaultOverloadsUseTheUnboundedBottomLayout()
    {
        using var font = new Font("Arial", 12);
        const string text = "line one\nline two";
        Size max = new(int.MaxValue, int.MaxValue);
        Size expected = TextRenderer.MeasureText(text, font, max, TextFormatFlags.Bottom);
        Assert.Equal(expected, TextRenderer.MeasureText(text, font));
        Assert.Equal(expected, TextRenderer.MeasureText(text.AsSpan(), font));
        Assert.Equal(expected, TextRenderer.MeasureText(text, font, max));
        Assert.Equal(expected, TextRenderer.MeasureText(text.AsSpan(), font, max));
        Assert.True(expected.Height > font.Height);
    }

    [Fact]
    public void GlyphPaddingAndExtraPaddingAreAppliedAndExtraTakesPrecedence()
    {
        using var font = new Font("Arial", 12);
        const string text = "Italic fW";
        var proposal = new Size(500, 200);
        const TextFormatFlags flags = TextFormatFlags.SingleLine | TextFormatFlags.NoPrefix;
        Size none = TextRenderer.MeasureText(text, font, proposal, flags | TextFormatFlags.NoPadding);
        Size normal = TextRenderer.MeasureText(text, font, proposal, flags);
        Size extra = TextRenderer.MeasureText(text, font, proposal, flags | TextFormatFlags.LeftAndRightPadding);
        Assert.True(normal.Width > none.Width);
        Assert.True(extra.Width > normal.Width);
        Assert.Equal(none.Height, normal.Height);
        Assert.Equal(normal.Height, extra.Height);
        Assert.Equal(extra, TextRenderer.MeasureText(text, font, proposal,
            flags | TextFormatFlags.NoPadding | TextFormatFlags.LeftAndRightPadding));
    }

    [Theory]
    [InlineData(-20, -30)]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    public void NonpositiveConstraintsRetainNarrowNativeWrapping(int width, int height)
    {
        using var font = new Font("Arial", 12);
        const string text = "a b c";
        const TextFormatFlags flags = TextFormatFlags.NoPrefix | TextFormatFlags.NoPadding | TextFormatFlags.WordBreak;
        Size expected = NativeNoPadding(text, font, new Size(1, 1), 0x0010 | 0x0800);
        Size actual = TextRenderer.MeasureText(text, font, new Size(width, height), flags);
        Assert.Equal(expected, actual);
        Assert.True(actual.Height > font.Height);
    }

    [Theory]
    [InlineData(TextFormatFlags.Bottom)]
    [InlineData(TextFormatFlags.VerticalCenter)]
    [InlineData(TextFormatFlags.Bottom | TextFormatFlags.VerticalCenter)]
    public void UnboundedSingleLineRemovesVerticalAlignment(TextFormatFlags alignment)
    {
        using var font = new Font("Arial", 12);
        var max = new Size(int.MaxValue, int.MaxValue);
        const TextFormatFlags flags = TextFormatFlags.SingleLine | TextFormatFlags.NoPadding;
        Assert.Equal(TextRenderer.MeasureText("abc", font, max, flags),
            TextRenderer.MeasureText("abc", font, max, flags | alignment));
    }

    [Fact]
    public void UnboundedWidthDoesNotIntroduceWordWrapping()
    {
        using var font = new Font("Arial", 12);
        var max = new Size(int.MaxValue, int.MaxValue);
        const string text = "several words followed by 中文";
        Assert.Equal(TextRenderer.MeasureText(text, font, max, TextFormatFlags.NoPrefix),
            TextRenderer.MeasureText(text, font, max, TextFormatFlags.NoPrefix | TextFormatFlags.WordBreak));
    }

    [Fact]
    public void NarrowWordBreakProducesMoreLinesThanWideWordBreak()
    {
        using var font = new Font("Arial", 12);
        const string text = "one two three four five six seven";
        const TextFormatFlags flags = TextFormatFlags.NoPrefix | TextFormatFlags.WordBreak;
        Size narrow = TextRenderer.MeasureText(text, font, new Size(75, 300), flags);
        Size wide = TextRenderer.MeasureText(text, font, new Size(600, 300), flags);
        Assert.True(narrow.Height > wide.Height);
        Assert.True(narrow.Width < wide.Width);
    }

    [Fact]
    public void GraphicsPreservationFlagsAreNotForwardedToGdi()
    {
        using var font = new Font("Arial", 12);
        var proposal = new Size(150, 200);
        const TextFormatFlags flags = TextFormatFlags.NoPrefix | TextFormatFlags.WordBreak;
        Assert.Equal(TextRenderer.MeasureText("one two 中文", font, proposal, flags),
            TextRenderer.MeasureText("one two 中文", font, proposal, flags
                | TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PreserveGraphicsTranslateTransform));
    }

    [Theory]
    [InlineData("A&B", false)]
    [InlineData("A\tB", true)]
    [InlineData("one\r\ntwo", false)]
    public void NativeFormattingFlagsKeepTheirOwnSemantics(string text, bool expandTabs)
    {
        using var font = new Font("Arial", 12);
        uint nativeFlags = expandTabs ? 0x0040u : 0u;
        Size expected = NativeNoPadding(text, font, new Size(400, 300), nativeFlags);
        TextFormatFlags flags = TextFormatFlags.NoPadding | (expandTabs ? TextFormatFlags.ExpandTabs : 0);
        Assert.Equal(expected, TextRenderer.MeasureText(text, font, new Size(400, 300), flags));
    }

    // DrawTextW provides an independent native control for layouts without
    // TextRenderer's extra margins; it does not reproduce the DrawTextEx path.
    private static Size NativeNoPadding(string text, Font? font, Size proposal, uint flags)
    {
        nint dc = GetDC(0);
        Assert.NotEqual(0, dc);
        nint handle = 0;
        nint previous = 0;
        try
        {
            if (font is not null)
            {
                // Integer point sizes used above map exactly to screen pixels
                // on the supported test scales, avoiding a second FontCache.
                handle = font.ToHfont();
                Assert.NotEqual(0, handle);
                previous = SelectObject(dc, handle);
            }
            var bounds = new NativeRect { Right = proposal.Width, Bottom = proposal.Height };
            Assert.True(DrawTextW(dc, text, text.Length, ref bounds, flags | 0x0400) > 0);
            return new Size(bounds.Right - bounds.Left, bounds.Bottom - bounds.Top);
        }
        finally
        {
            if (previous != 0) { _ = SelectObject(dc, previous); }
            if (handle != 0) { _ = DeleteObject(handle); }
            _ = ReleaseDC(0, dc);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct NativeRect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    [DllImport("user32.dll")]
    private static extern nint GetDC(nint window);

    [DllImport("user32.dll")]
    private static extern int ReleaseDC(nint window, nint dc);

    [DllImport("gdi32.dll")]
    private static extern nint SelectObject(nint dc, nint item);

    [DllImport("gdi32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DeleteObject(nint item);

    [DllImport("user32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
    private static extern int DrawTextW(nint dc, string text, int count, ref NativeRect bounds, uint flags);
}
