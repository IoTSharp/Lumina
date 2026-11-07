using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using Lumina.Forms;
using Xunit;
using Xunit.Abstractions;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class RichTextBoxFontNativeTests
{
    private const int EmGetCharFormat = 0x043A;
    private const int EmSetCharFormat = 0x0444;
    private const uint FontMask = 0xA800000F;
    private readonly ITestOutputHelper _output;

    public RichTextBoxFontNativeTests(ITestOutputHelper output) => _output = output;

    [Fact]
    public void FontAssignedBeforeCreationRemainsBorrowedAndInitializesCachedText()
        => OnSta(() =>
        {
            using var supplied = new Font(FontFamily.GenericMonospace, 17, FontStyle.Bold);
            using var rich = new FontRichTextBox { Text = "cached document" };

            rich.Font = supplied;

            Assert.False(rich.IsHandleCreated);
            Assert.Same(supplied, rich.Font);
            _ = rich.Handle;
            Assert.Equal(1, rich.CreatedCount);
            Assert.Equal("cached document", rich.Text);
            using Font expectedNative = ReadIndependentFontAfterWmSetFont(rich.Handle, supplied);
            using Font actual = ReadFont(rich.Handle, selection: false);
            AssertFont(expectedNative, actual);
            rich.SelectAll();
            using Font selected = Assert.IsType<Font>(rich.SelectionFont);
            AssertFont(expectedNative, selected);
        });

    [Fact]
    public void AnExistingEmptyDocumentUsesTheAssignedFontForSubsequentText()
        => OnSta(() =>
        {
            using var rich = new FontRichTextBox();
            _ = rich.Handle;
            using var supplied = new Font(FontFamily.GenericMonospace, 19, FontStyle.Italic);

            rich.Font = supplied;

            Assert.Same(supplied, rich.Font);
            using Font expectedNative = ReadIndependentFontAfterWmSetFont(rich.Handle, supplied);
            using (Font empty = ReadFont(rich.Handle, selection: false)) { AssertFont(expectedNative, empty); }
            rich.SelectedText = "new document";
            rich.SelectAll();
            using Font actual = Assert.IsType<Font>(rich.SelectionFont);
            AssertFont(expectedNative, actual);
            Assert.Equal("new document", rich.Text);
            Assert.Equal(1, rich.CreatedCount);
        });

    [Theory]
    [InlineData(FontStyle.Regular)]
    [InlineData(FontStyle.Bold)]
    [InlineData(FontStyle.Italic)]
    [InlineData(FontStyle.Underline)]
    [InlineData(FontStyle.Strikeout)]
    [InlineData(FontStyle.Bold | FontStyle.Italic)]
    [InlineData(FontStyle.Underline | FontStyle.Strikeout)]
    [InlineData(FontStyle.Bold | FontStyle.Italic | FontStyle.Underline | FontStyle.Strikeout)]
    public void FontFormatsTheEntireDocumentWithoutChangingTheCurrentSelection(FontStyle style)
        => OnSta(() =>
        {
            using var rich = new FontRichTextBox { Text = "abcdefgh" };
            _ = rich.Handle;
            rich.Select(2, 3);
            using var supplied = new Font(FontFamily.GenericSansSerif, 17.5f, style);

            rich.Font = supplied;

            Assert.Equal("abcdefgh", rich.Text);
            Assert.Equal(2, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            using Font expectedNative = ReadIndependentFontAfterWmSetFont(rich.Handle, supplied);
            using (Font defaults = ReadFont(rich.Handle, selection: false)) { AssertFont(expectedNative, defaults); }
            AssertFont(supplied, rich.Font);
            rich.Select(0, 1);
            using (Font first = Assert.IsType<Font>(rich.SelectionFont)) { AssertFont(expectedNative, first); }
            rich.Select(7, 1);
            using Font last = Assert.IsType<Font>(rich.SelectionFont);
            AssertFont(expectedNative, last);
            NativeFormat native = ReadFormat(rich.Handle, selection: true);
            Assert.Equal(FontMask, native.Mask & FontMask);
            Assert.Equal((uint)ToEffects(style), native.Effects & 15u);
        });

    [Theory]
    [InlineData(GraphicsUnit.Point, 12.75f)]
    [InlineData(GraphicsUnit.Pixel, 18f)]
    [InlineData(GraphicsUnit.Inch, 0.25f)]
    [InlineData(GraphicsUnit.Millimeter, 4.5f)]
    public void FontUsesNativeResolvedPointUnitsRatherThanKeepingTheSuppliedUnit(GraphicsUnit unit, float size)
        => OnSta(() =>
        {
            using var rich = new FontRichTextBox { Text = "units" };
            _ = rich.Handle;
            using var supplied = new Font(FontFamily.GenericMonospace, size, FontStyle.Bold, unit);

            rich.Font = supplied;

            Assert.Equal(GraphicsUnit.Point, rich.Font.Unit);
            AssertFont(supplied, rich.Font);
            using Font defaults = ReadFont(rich.Handle, selection: false);
            AssertFont(supplied, defaults);
            Assert.True(defaults.Equals(rich.Font));
            Assert.Equal("units", rich.Text);
        });

    [Fact]
    public void DocumentFontRestoresAutomaticForegroundAndZeroOffsetWhilePreservingBackgroundParagraphsAndSelection()
        => OnSta(() =>
        {
            using var rich = new FontRichTextBox { Text = "first\nsecond" };
            rich.Select(1, 3);
            rich.SelectionColor = Color.DarkRed;
            rich.SelectionBackColor = Color.Gold;
            rich.SelectionCharOffset = 6;
            rich.SelectionAlignment = HorizontalAlignment.Center;
            rich.SelectionIndent = 24;
            rich.SelectionTabs = [48, 96];
            NativeFormat before = ReadFormat(rich.Handle, selection: true);
            int indent = rich.SelectionIndent;
            using var supplied = new Font(FontFamily.GenericMonospace, 18, FontStyle.Bold | FontStyle.Italic);

            rich.Font = supplied;

            NativeFormat after = ReadFormat(rich.Handle, selection: true);
            Assert.Equal(ColorTranslator.ToWin32(Color.DarkRed), before.TextColor);
            Assert.Equal(0, after.TextColor);
            Assert.Equal(before.BackColor, after.BackColor);
            // SCF_ALL preserves these fields, then the independent full-format
            // diagnostic proves WM_SETFONT restores automatic foreground and
            // clears the character offset while retaining the background.
            Assert.NotEqual(0, before.Offset);
            Assert.Equal(0, after.Offset);
            Assert.Equal((before.Effects & ~15u) | 0x40000000u, after.Effects & ~15u);
            Assert.Equal(HorizontalAlignment.Center, rich.SelectionAlignment);
            Assert.Equal(indent, rich.SelectionIndent);
            Assert.Equal(new[] { 48, 96 }, rich.SelectionTabs);
            Assert.Equal("first\nsecond", rich.Text);
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            using Font selected = Assert.IsType<Font>(rich.SelectionFont);
            AssertFont(supplied, selected);
        });

    [Fact]
    public void MatchingTheNativeDefaultFontLeavesDifferentlyFormattedTextAlone()
        => OnSta(() =>
        {
            using var rich = new FontRichTextBox { Text = "aabb" };
            _ = rich.Handle;
            // Match the fixed getter's named-family identity and use an exact
            // native pixel size. A generic-family Font with the same displayed
            // values is not Equals to the native format's named Font.
            using var defaults = new Font("Microsoft Sans Serif", 18, FontStyle.Regular,
                GraphicsUnit.Point, 1);
            using var different = new Font(FontFamily.GenericMonospace, 20, FontStyle.Bold);
            rich.Font = defaults;
            using (Font nativeBefore = ReadFont(rich.Handle, selection: false))
            {
                Assert.True(nativeBefore.Equals(defaults));
            }
            rich.Select(2, 2);
            rich.SelectionFont = different;
            rich.Modified = false;
            string? before = rich.Rtf;
            int changes = 0;
            rich.FontChanged += (_, _) => changes++;

            rich.Font = defaults;

            Assert.Equal(before, rich.Rtf);
            Assert.Equal(0, changes);
            Assert.False(rich.Modified);
            using Font selected = Assert.IsType<Font>(rich.SelectionFont);
            AssertFont(different, selected);
            using Font nativeDefault = ReadFont(rich.Handle, selection: false);
            AssertFont(defaults, nativeDefault);
            AssertFont(defaults, rich.Font);
        });

    [Fact]
    public void ReassigningTheManagedFontRepairsAChangedNativeDefault()
        => OnSta(() =>
        {
            using var rich = new FontRichTextBox { Text = "native latest" };
            _ = rich.Handle;
            using var original = new Font(FontFamily.GenericSansSerif, 14);
            using var changed = new Font(FontFamily.GenericMonospace, 22, FontStyle.Underline);
            rich.Font = original;
            SetNativeFont(rich.Handle, changed);
            using (Font nativeChanged = ReadFont(rich.Handle, selection: false)) { AssertFont(changed, nativeChanged); }
            AssertFont(original, rich.Font);

            rich.Font = original;

            using Font repaired = ReadFont(rich.Handle, selection: false);
            AssertFont(original, repaired);
            rich.SelectAll();
            using Font selected = Assert.IsType<Font>(rich.SelectionFont);
            AssertFont(original, selected);
            AssertFont(original, rich.Font);
            Assert.Equal("native latest", rich.Text);
        });

    [Fact]
    public void NullFontRestoresTheParentFontAcrossTheExistingDocument()
        => OnSta(() =>
        {
            using var inherited = new Font(FontFamily.GenericSansSerif, 15, FontStyle.Italic);
            using var local = new Font(FontFamily.GenericMonospace, 21, FontStyle.Bold);
            using var form = new Form { Font = inherited };
            using var rich = new FontRichTextBox { Text = "inherited document" };
            CreateHostedHandle(form, rich);
            rich.Font = local;
            rich.Select(2, 4);

            rich.Font = null;

            Assert.Same(inherited, rich.Font);
            using (Font defaults = ReadFont(rich.Handle, selection: false)) { AssertFont(inherited, defaults); }
            using Font selected = Assert.IsType<Font>(rich.SelectionFont);
            AssertFont(inherited, selected);
            Assert.Equal(2, rich.SelectionStart);
            Assert.Equal(4, rich.SelectionLength);
            Assert.Equal("inherited document", rich.Text);
        });

    [Fact]
    public void ResetFontRestoresTheDefaultForAnUnparentedDocument()
        => OnSta(() =>
        {
            using var local = new Font(FontFamily.GenericMonospace, 21, FontStyle.Bold);
            using var rich = new FontRichTextBox { Text = "reset document" };
            _ = rich.Handle;
            rich.Font = local;

            rich.ResetFont();

            Assert.Same(Control.DefaultFont, rich.Font);
            using (Font defaults = ReadFont(rich.Handle, selection: false)) { AssertFont(Control.DefaultFont, defaults); }
            rich.SelectAll();
            using Font selected = Assert.IsType<Font>(rich.SelectionFont);
            AssertFont(Control.DefaultFont, selected);
            Assert.Equal("reset document", rich.Text);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void HandleRecreationPreservesDocumentFontAndRtfEditingState(bool modified)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new FontRichTextBox { Text = "first\nsecond", MaxLength = 7 };
            CreateHostedHandle(form, rich);
            using var supplied = new Font(FontFamily.GenericMonospace, 18,
                FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
            rich.Font = supplied;
            rich.Select(1, 3);
            rich.SelectionColor = Color.DarkRed;
            rich.SelectionBackColor = Color.Gold;
            rich.SelectionAlignment = HorizontalAlignment.Right;
            rich.Modified = modified;
            string text = rich.Text;
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;

            rich.RecreateNow();

            Assert.Equal(2, rich.CreatedCount);
            using Font actual = Assert.IsType<Font>(rich.SelectionFont);
            AssertFont(supplied, actual);
            AssertFont(supplied, rich.Font);
            Assert.Equal(Color.DarkRed.ToArgb(), rich.SelectionColor.ToArgb());
            Assert.Equal(Color.Gold.ToArgb(), rich.SelectionBackColor.ToArgb());
            Assert.Equal(HorizontalAlignment.Right, rich.SelectionAlignment);
            Assert.Equal(text, rich.Text);
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.Equal(modified, rich.Modified);
            Assert.False(rich.CanUndo);
            Assert.Equal((nint)7, SendMessageW(rich.Handle, 0x00D5, 0, 0));
            Assert.Equal(0, changes);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ModificationAndUndoEffectsMatchAnIndependentRichEdit(bool modified)
        => OnSta(() =>
        {
            using var rich = new FontRichTextBox { Text = "abcdef" };
            _ = rich.Handle;
            using var initial = new Font(FontFamily.GenericSansSerif, 12);
            using var supplied = new Font(FontFamily.GenericMonospace, 18, FontStyle.Bold);
            rich.Font = initial;
            nint native = CreateIndependent(rich.Handle, "abcdef");
            nint resolvedHandle = 0;
            try
            {
                rich.Select(1, 3);
                SetNativeSelection(native, 1, 4);
                rich.ClearUndo();
                _ = SendMessageW(native, 0x00CD, 0, 0);
                rich.Modified = modified;
                _ = SendMessageW(native, 0x00B9, modified ? 1 : 0, 0);
                Assert.Equal(ReadState(native), ReadState(rich.Handle));

                rich.Font = supplied;
                resolvedHandle = ApplyIndependentDocumentFont(native, supplied);

                Assert.Equal(ReadState(native), ReadState(rich.Handle));
                using Font independent = ReadFont(native, selection: false);
                using Font actual = ReadFont(rich.Handle, selection: false);
                AssertFont(supplied, independent);
                AssertFont(independent, actual);
                Assert.Equal("abcdef", rich.Text);
                Assert.Equal("abcdef", ReadNativeText(native, 6));
            }
            finally
            {
                try { Assert.True(DestroyWindow(native)); }
                finally
                {
                    if (resolvedHandle != 0) { Assert.True(DeleteObject(resolvedHandle)); }
                }
            }
        });

    [Fact]
    public void BorrowedFontRemainsUsableAfterFormattingAndDisposal()
        => OnSta(() =>
        {
            using var supplied = new Font(FontFamily.GenericMonospace, 18, FontStyle.Bold);
            using (var rich = new FontRichTextBox { Text = "borrowed document" })
            {
                _ = rich.Handle;
                rich.Font = supplied;
                AssertFont(supplied, rich.Font);
                Assert.NotSame(supplied, rich.Font);
            }

            Assert.Equal(18f, supplied.SizeInPoints);
            nint fontHandle = supplied.ToHfont();
            Assert.NotEqual((nint)0, fontHandle);
            Assert.True(DeleteObject(fontHandle));
        });

    [Theory]
    [InlineData(17f, 340, -23, 345)]
    [InlineData(17.5f, 350, -23, 345)]
    [InlineData(19f, 380, -25, 375)]
    public void IndependentRichEditQuantizesPointSizesOnlyWhenTheResolvedFontIsSentAsAnHfont(
        float points, int characterTwips, int logicalHeight, int hfontTwips)
        => OnSta(() =>
        {
            using var loader = new FontRichTextBox();
            using var supplied = new Font(FontFamily.GenericMonospace, points);
            nint native = CreateIndependent(loader.Handle, "independent font");
            nint resolvedHandle = 0;
            nint logicalFont = 0;
            try
            {
                SetNativeFont(native, supplied);
                NativeFormat before = ReadFormat(native, selection: false);
                Assert.Equal(characterTwips, before.Height);
                using Font resolved = ReadFont(native, selection: false);
                Assert.Equal(characterTwips / 20f, resolved.SizeInPoints);
                resolvedHandle = resolved.ToHfont();
                logicalFont = Marshal.AllocHGlobal(92);
                Assert.Equal(92, GetObjectW(resolvedHandle, 92, logicalFont));
                Assert.Equal(logicalHeight, Marshal.ReadInt32(logicalFont));

                _ = SendMessageW(native, 0x0030, resolvedHandle, 1);

                NativeFormat after = ReadFormat(native, selection: false);
                Assert.Equal(hfontTwips, after.Height);
                Assert.Equal(before.FaceName, after.FaceName);
                Assert.NotEqual(before.Height, after.Height);
                Assert.Equal("independent font", ReadNativeText(native, 16));
            }
            finally
            {
                Marshal.FreeHGlobal(logicalFont);
                try { Assert.True(DestroyWindow(native)); }
                finally
                {
                    if (resolvedHandle != 0) { Assert.True(DeleteObject(resolvedHandle)); }
                }
            }
        });

    [Fact]
    public void IndependentRichEditKeepsCharacterOffsetForScfAllButClearsItForWmSetFont()
        => OnSta(() =>
        {
            using var loader = new FontRichTextBox();
            using var supplied = new Font(FontFamily.GenericMonospace, 18, FontStyle.Bold);
            nint native = CreateIndependent(loader.Handle, "abcdef");
            nint resolvedHandle = 0;
            nint format = 0;
            try
            {
                SetNativeSelection(native, 1, 4);
                byte[] bytes = new byte[116];
                BitConverter.GetBytes(116).CopyTo(bytes, 0);
                BitConverter.GetBytes(0x10000000u).CopyTo(bytes, 4);
                BitConverter.GetBytes(139).CopyTo(bytes, 16);
                format = Marshal.AllocHGlobal(bytes.Length);
                Marshal.Copy(bytes, 0, format, bytes.Length);
                Assert.NotEqual((nint)0, SendMessageW(native, EmSetCharFormat, 1, format));
                Assert.Equal(139, ReadFormat(native, selection: true).Offset);

                SetNativeFont(native, supplied);

                Assert.Equal(139, ReadFormat(native, selection: true).Offset);
                using Font resolved = ReadFont(native, selection: false);
                resolvedHandle = resolved.ToHfont();
                _ = SendMessageW(native, 0x0030, resolvedHandle, 1);
                Assert.Equal(0, ReadFormat(native, selection: true).Offset);
                NativeState state = ReadState(native);
                Assert.Equal(1, state.Start);
                Assert.Equal(4, state.End);
                Assert.Equal("abcdef", ReadNativeText(native, 6));
            }
            finally
            {
                Marshal.FreeHGlobal(format);
                try { Assert.True(DestroyWindow(native)); }
                finally
                {
                    if (resolvedHandle != 0) { Assert.True(DeleteObject(resolvedHandle)); }
                }
            }
        });

    [Fact]
    public void IndependentRichEditDecodedNamedFontIsNotEqualToTheGenericFamilyFontWithTheSameDisplayedValues()
        => OnSta(() =>
        {
            using var loader = new FontRichTextBox();
            using var supplied = new Font(FontFamily.GenericSansSerif, 18, FontStyle.Regular,
                GraphicsUnit.Point, 1);
            nint native = CreateIndependent(loader.Handle, "font equality");
            try
            {
                SetNativeFont(native, supplied);
                using Font resolved = ReadFont(native, selection: false);
                using var named = new Font(resolved.Name, resolved.SizeInPoints, resolved.Style,
                    resolved.Unit, resolved.GdiCharSet);

                Assert.Equal(supplied.Name, resolved.Name);
                Assert.Equal(supplied.SizeInPoints, resolved.SizeInPoints);
                Assert.Equal(supplied.Style, resolved.Style);
                Assert.Equal(supplied.Unit, resolved.Unit);
                Assert.Equal(supplied.GdiCharSet, resolved.GdiCharSet);
                Assert.Equal(supplied.GdiVerticalFont, resolved.GdiVerticalFont);
                Assert.False(resolved.Equals(supplied));
                Assert.True(resolved.Equals(named));
                Assert.Equal("font equality", ReadNativeText(native, 13));
            }
            finally { Assert.True(DestroyWindow(native)); }
        });

    [Fact]
    public void IndependentRichEditRecordsFullFormatsAcrossScfAllAndTheLaterWmSetFont()
        => OnSta(() =>
        {
            using var loader = new FontRichTextBox();
            using var supplied = new Font(FontFamily.GenericMonospace, 18, FontStyle.Bold | FontStyle.Italic);
            nint native = CreateIndependent(loader.Handle, "first\nsecond");
            nint resolvedHandle = 0;
            nint characters = 0;
            nint paragraphs = 0;
            try
            {
                SetNativeSelection(native, 1, 4);
                byte[] characterBytes = new byte[116];
                BitConverter.GetBytes(116).CopyTo(characterBytes, 0);
                BitConverter.GetBytes(0x54000000u).CopyTo(characterBytes, 4);
                BitConverter.GetBytes(90).CopyTo(characterBytes, 16);
                BitConverter.GetBytes(139).CopyTo(characterBytes, 20);
                BitConverter.GetBytes(ColorTranslator.ToWin32(Color.Gold)).CopyTo(characterBytes, 96);
                characters = Marshal.AllocHGlobal(characterBytes.Length);
                Marshal.Copy(characterBytes, 0, characters, characterBytes.Length);
                Assert.NotEqual((nint)0, SendMessageW(native, EmSetCharFormat, 1, characters));

                byte[] paragraphBytes = new byte[156];
                BitConverter.GetBytes(156).CopyTo(paragraphBytes, 0);
                BitConverter.GetBytes(0x19u).CopyTo(paragraphBytes, 4);
                BitConverter.GetBytes(360).CopyTo(paragraphBytes, 12);
                BitConverter.GetBytes((short)3).CopyTo(paragraphBytes, 24);
                BitConverter.GetBytes((short)2).CopyTo(paragraphBytes, 26);
                BitConverter.GetBytes(720).CopyTo(paragraphBytes, 28);
                BitConverter.GetBytes(1440).CopyTo(paragraphBytes, 32);
                paragraphs = Marshal.AllocHGlobal(paragraphBytes.Length);
                Marshal.Copy(paragraphBytes, 0, paragraphs, paragraphBytes.Length);
                Assert.NotEqual((nint)0, SendMessageW(native, 0x0447, 0, paragraphs));
                NativeFormat before = ReadFormat(native, selection: true);
                NativeParagraph beforeParagraph = ReadParagraph(native);
                _output.WriteLine("Before: {0}; paragraph={1}", before, beforeParagraph);
                Assert.Equal(139, before.TextColor);
                Assert.Equal(ColorTranslator.ToWin32(Color.Gold), before.BackColor);
                Assert.Equal(90, before.Offset);
                Assert.Equal((short)3, beforeParagraph.Alignment);
                Assert.Equal(360, beforeParagraph.StartIndent);
                Assert.Equal((short)2, beforeParagraph.TabCount);
                Assert.Equal(720, beforeParagraph.FirstTab);
                Assert.Equal(1440, beforeParagraph.SecondTab);

                SetNativeFont(native, supplied);

                NativeFormat afterScfAll = ReadFormat(native, selection: true);
                NativeParagraph afterScfAllParagraph = ReadParagraph(native);
                _output.WriteLine("After SCF_ALL: {0}; paragraph={1}", afterScfAll, afterScfAllParagraph);
                Assert.Equal(before.TextColor, afterScfAll.TextColor);
                Assert.Equal(before.BackColor, afterScfAll.BackColor);
                Assert.Equal(before.Offset, afterScfAll.Offset);
                Assert.Equal(before.Effects & ~15u, afterScfAll.Effects & ~15u);
                Assert.Equal(beforeParagraph, afterScfAllParagraph);
                using Font resolved = ReadFont(native, selection: false);
                resolvedHandle = resolved.ToHfont();
                Assert.NotEqual((nint)0, resolvedHandle);

                _ = SendMessageW(native, 0x0030, resolvedHandle, 1);

                NativeFormat afterWmSetFont = ReadFormat(native, selection: true);
                NativeParagraph afterWmSetFontParagraph = ReadParagraph(native);
                _output.WriteLine("After WM_SETFONT: {0}; paragraph={1}", afterWmSetFont, afterWmSetFontParagraph);
                Assert.Equal(0, afterWmSetFont.TextColor);
                Assert.Equal(0, afterWmSetFont.Offset);
                Assert.Equal(beforeParagraph, afterWmSetFontParagraph);
                NativeState state = ReadState(native);
                Assert.Equal(1, state.Start);
                Assert.Equal(4, state.End);
            }
            finally
            {
                Marshal.FreeHGlobal(paragraphs);
                Marshal.FreeHGlobal(characters);
                try { Assert.True(DestroyWindow(native)); }
                finally
                {
                    if (resolvedHandle != 0) { Assert.True(DeleteObject(resolvedHandle)); }
                }
            }
        });

    private static NativeParagraph ReadParagraph(nint handle)
    {
        nint buffer = Marshal.AllocHGlobal(156);
        try
        {
            Marshal.Copy(new byte[156], 0, buffer, 156);
            Marshal.WriteInt32(buffer, 156);
            _ = SendMessageW(handle, 0x043D, 0, buffer);
            return new NativeParagraph(unchecked((uint)Marshal.ReadInt32(buffer, 4)),
                Marshal.ReadInt32(buffer, 12), Marshal.ReadInt32(buffer, 16), Marshal.ReadInt32(buffer, 20),
                Marshal.ReadInt16(buffer, 24), Marshal.ReadInt16(buffer, 26),
                Marshal.ReadInt32(buffer, 28), Marshal.ReadInt32(buffer, 32));
        }
        finally { Marshal.FreeHGlobal(buffer); }
    }

    private static void AssertFont(Font expected, Font actual)
    {
        Assert.Equal(expected.FontFamily.Name, actual.FontFamily.Name);
        float quantized = (int)(expected.SizeInPoints * 20) / 20f;
        Assert.InRange(actual.SizeInPoints, quantized - 0.01f, quantized + 0.01f);
        Assert.Equal(expected.Style, actual.Style);
        Assert.Equal(GraphicsUnit.Point, actual.Unit);
    }

    private static int ToEffects(FontStyle style)
        => ((style & FontStyle.Bold) != 0 ? 1 : 0)
            | ((style & FontStyle.Italic) != 0 ? 2 : 0)
            | ((style & FontStyle.Underline) != 0 ? 4 : 0)
            | ((style & FontStyle.Strikeout) != 0 ? 8 : 0);

    private static Font ReadFont(nint handle, bool selection)
    {
        NativeFormat format = ReadFormat(handle, selection);
        Assert.NotEqual(0u, format.Mask & 0x20000000u);
        Assert.NotEqual(0u, format.Mask & 0x80000000u);
        return new Font(format.FaceName, format.Height / 20f,
            (FontStyle)(((format.Mask & format.Effects & 1u) != 0 ? FontStyle.Bold : FontStyle.Regular)
                | ((format.Mask & format.Effects & 2u) != 0 ? FontStyle.Italic : FontStyle.Regular)
                | ((format.Mask & format.Effects & 4u) != 0 ? FontStyle.Underline : FontStyle.Regular)
                | ((format.Mask & format.Effects & 8u) != 0 ? FontStyle.Strikeout : FontStyle.Regular)),
            GraphicsUnit.Point, format.CharSet);
    }

    private static NativeFormat ReadFormat(nint handle, bool selection)
    {
        nint buffer = Marshal.AllocHGlobal(116);
        try
        {
            Marshal.Copy(new byte[116], 0, buffer, 116);
            Marshal.WriteInt32(buffer, 116);
            _ = SendMessageW(handle, EmGetCharFormat, selection ? 1 : 0, buffer);
            byte[] bytes = new byte[116];
            Marshal.Copy(buffer, bytes, 0, bytes.Length);
            return new NativeFormat(BitConverter.ToUInt32(bytes, 4), BitConverter.ToUInt32(bytes, 8),
                BitConverter.ToInt32(bytes, 12), BitConverter.ToInt32(bytes, 16),
                BitConverter.ToInt32(bytes, 20), bytes[24],
                Encoding.Unicode.GetString(bytes, 26, 64).TrimEnd('\0'), BitConverter.ToInt32(bytes, 96));
        }
        finally { Marshal.FreeHGlobal(buffer); }
    }

    private static void SetNativeFont(nint handle, Font font)
    {
        nint fontHandle = font.ToHfont();
        Assert.NotEqual((nint)0, fontHandle);
        nint logicalFont = 0;
        nint format = 0;
        try
        {
            logicalFont = Marshal.AllocHGlobal(92);
            format = Marshal.AllocHGlobal(116);
            Assert.Equal(92, GetObjectW(fontHandle, 92, logicalFont));
            byte[] bytes = new byte[116];
            BitConverter.GetBytes(116).CopyTo(bytes, 0);
            BitConverter.GetBytes(FontMask).CopyTo(bytes, 4);
            BitConverter.GetBytes(ToEffects(font.Style)).CopyTo(bytes, 8);
            BitConverter.GetBytes((int)(font.SizeInPoints * 20)).CopyTo(bytes, 12);
            bytes[24] = Marshal.ReadByte(logicalFont, 23);
            bytes[25] = Marshal.ReadByte(logicalFont, 27);
            Marshal.Copy(logicalFont + 28, bytes, 26, 64);
            Marshal.Copy(bytes, 0, format, bytes.Length);
            Assert.NotEqual((nint)0, SendMessageW(handle, EmSetCharFormat, 4, format));
        }
        finally
        {
            Marshal.FreeHGlobal(format);
            Marshal.FreeHGlobal(logicalFont);
            Assert.True(DeleteObject(fontHandle));
        }
    }

    private static Font ReadIndependentFontAfterWmSetFont(nint managedHandle, Font supplied)
    {
        nint native = CreateIndependent(managedHandle, "font reference");
        nint fontHandle = 0;
        try
        {
            // Use the real Drawing HFONT and native control to obtain the
            // independently verified pixel quantization, rather than widening
            // the requested point-size tolerance.
            fontHandle = supplied.ToHfont();
            Assert.NotEqual((nint)0, fontHandle);
            _ = SendMessageW(native, 0x0030, fontHandle, 1);
            return ReadFont(native, selection: false);
        }
        finally
        {
            try { Assert.True(DestroyWindow(native)); }
            finally
            {
                if (fontHandle != 0) { Assert.True(DeleteObject(fontHandle)); }
            }
        }
    }

    private static nint ApplyIndependentDocumentFont(nint handle, Font supplied)
    {
        // Model the fixed reference's native operations, including the later
        // base Font change, without assuming formatting preserves Modified.
        SetNativeFont(handle, supplied);
        using Font resolved = ReadFont(handle, selection: false);
        nint fontHandle = resolved.ToHfont();
        Assert.NotEqual((nint)0, fontHandle);
        _ = SendMessageW(handle, 0x0030, fontHandle, 1);
        return fontHandle;
    }

    private static void CreateHostedHandle(Form form, RichTextBox rich)
    {
        form.Controls.Add(rich);
        _ = form.Handle;
        _ = rich.Handle;
    }

    private static nint CreateIndependent(nint managedHandle, string text)
    {
        nint native = CreateWindowExW(0, "RICHEDIT50W", string.Empty,
            0x80000004, 0, 0, 350, 160, 0, 0, 0, 0);
        Assert.NotEqual((nint)0, native);
        nint value = 0;
        bool transferred = false;
        try
        {
            value = Marshal.StringToHGlobalUni(text);
            _ = SendMessageW(native, 0x0030, SendMessageW(managedHandle, 0x0031, 0, 0), 0);
            _ = SendMessageW(native, 0x000C, 0, value);
            transferred = true;
            return native;
        }
        finally
        {
            Marshal.FreeHGlobal(value);
            if (!transferred) { Assert.True(DestroyWindow(native)); }
        }
    }

    private static void SetNativeSelection(nint handle, int start, int end)
    {
        nint range = Marshal.AllocHGlobal(8);
        try
        {
            Marshal.WriteInt32(range, 0, start);
            Marshal.WriteInt32(range, 4, end);
            _ = SendMessageW(handle, 0x0437, 0, range);
        }
        finally { Marshal.FreeHGlobal(range); }
    }

    private static string ReadNativeText(nint handle, int expectedLength)
    {
        Assert.Equal((nint)expectedLength, SendMessageW(handle, 0x000E, 0, 0));
        nint buffer = Marshal.AllocHGlobal((expectedLength + 1) * sizeof(char));
        try
        {
            Assert.Equal((nint)expectedLength,
                SendMessageW(handle, 0x000D, expectedLength + 1, buffer));
            return Marshal.PtrToStringUni(buffer, expectedLength)!;
        }
        finally { Marshal.FreeHGlobal(buffer); }
    }

    private static NativeState ReadState(nint handle)
    {
        _ = SendMessageW(handle, 0x00B0, out int start, out int end);
        return new NativeState(start, end, SendMessageW(handle, 0x00B8, 0, 0) != 0,
            SendMessageW(handle, 0x00C6, 0, 0) != 0);
    }

    private static void OnSta(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            try { action(); }
            catch (Exception exception) { failure = exception; }
            finally
            {
                try
                {
                    Application.ExitThread();
                    typeof(Application).GetMethod("DisposeThemedCommonControlsActivation",
                        BindingFlags.Static | BindingFlags.NonPublic)!.Invoke(null, null);
                }
                catch (Exception exception)
                {
                    failure = failure is null ? exception : new AggregateException(failure, exception);
                }
            }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "RichTextBox Font native test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private readonly record struct NativeFormat(uint Mask, uint Effects, int Height, int Offset,
        int TextColor, byte CharSet, string FaceName, int BackColor);
    private readonly record struct NativeParagraph(uint Mask, int StartIndent, int RightIndent,
        int Offset, short Alignment, short TabCount, int FirstTab, int SecondTab);
    private readonly record struct NativeState(int Start, int End, bool Modified, bool CanUndo);

    private sealed class FontRichTextBox : RichTextBox
    {
        internal int CreatedCount { get; private set; }
        internal void RecreateNow() => RecreateHandle();

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            CreatedCount++;
        }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint handle, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint handle, int message, out int wParam, out int lParam);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint CreateWindowExW(uint exStyle, string className, string text,
        uint style, int x, int y, int width, int height, nint parent, nint menu, nint instance, nint parameter);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint handle);

    [DllImport("gdi32.dll", ExactSpelling = true)]
    private static extern int GetObjectW(nint handle, int size, nint value);

    [DllImport("gdi32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DeleteObject(nint handle);
}
