using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class RichTextBoxSelectionFontNativeTests
{
    private const int EmGetCharFormat = 0x043A;
    private const int EmSetCharFormat = 0x0444;
    private const int EmExSetSel = 0x0437;
    private const uint FontMask = 0xA800000F;

    [Theory]
    [InlineData(FontStyle.Regular)]
    [InlineData(FontStyle.Bold)]
    [InlineData(FontStyle.Italic)]
    [InlineData(FontStyle.Underline)]
    [InlineData(FontStyle.Strikeout)]
    [InlineData(FontStyle.Bold | FontStyle.Italic)]
    [InlineData(FontStyle.Underline | FontStyle.Strikeout)]
    [InlineData(FontStyle.Bold | FontStyle.Italic | FontStyle.Underline | FontStyle.Strikeout)]
    public void UniformSelectedTextRoundTripsEachSupportedFontStyle(FontStyle style)
        => OnSta(() =>
        {
            using var rich = new FontRichTextBox { Text = "abcdefgh" };
            using var supplied = new Font(FontFamily.GenericSansSerif, 15.5f, style, GraphicsUnit.Point);
            rich.Select(1, 5);

            rich.SelectionFont = supplied;

            using Font actual = Assert.IsType<Font>(rich.SelectionFont);
            AssertFont(supplied, actual);
            NativeFormat native = ReadFormat(rich.Handle);
            Assert.Equal(FontMask, native.Mask & FontMask);
            Assert.Equal((int)(supplied.SizeInPoints * 20), native.Height);
            Assert.Equal((uint)ToEffects(style), native.Effects & 15);
            Assert.Equal("abcdefgh", rich.Text);
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(5, rich.SelectionLength);
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
    public void MixedScriptSelectionHasTheSameMissingFaceMaskAsIndependentRichEdit(FontStyle style)
        => OnSta(() =>
        {
            using var rich = new FontRichTextBox { Text = "abc中文def" };
            using var supplied = new Font(FontFamily.GenericSansSerif, 15.5f, style, GraphicsUnit.Point);
            nint native = CreateIndependent(rich.Handle, "abc中文def");
            try
            {
                rich.Select(1, 5);
                SetNativeSelection(native, 1, 6);

                rich.SelectionFont = supplied;
                SetNativeFont(native, supplied);

                NativeFormat independent = ReadFormat(native);
                NativeFormat managed = ReadFormat(rich.Handle);
                // Keep these concrete assertions while investigating the first
                // uniform-font fixture failure; do not branch on observed masks.
                Assert.Equal(0u, independent.Mask & 0x20000000u);
                Assert.Equal(0u, managed.Mask & 0x20000000u);
                Assert.Equal(independent.Mask & FontMask, managed.Mask & FontMask);
                Assert.Equal(independent.Height, managed.Height);
                Assert.Equal(independent.Effects & 15u, managed.Effects & 15u);
                Assert.Equal(independent.FaceName, managed.FaceName);
                Assert.Null(rich.SelectionFont);
                Assert.Equal("abc中文def", rich.Text);
                Assert.Equal("abc中文def", ReadNativeText(native, 8));
                Assert.Equal(1, rich.SelectionStart);
                Assert.Equal(5, rich.SelectionLength);
                NativeState nativeState = ReadState(native);
                Assert.Equal(1, nativeState.Start);
                Assert.Equal(6, nativeState.End);
            }
            finally { Assert.True(DestroyWindow(native)); }
        });

    [Theory]
    [InlineData(GraphicsUnit.Point, 12.75f)]
    [InlineData(GraphicsUnit.Pixel, 18f)]
    [InlineData(GraphicsUnit.Inch, 0.25f)]
    [InlineData(GraphicsUnit.Millimeter, 4.5f)]
    public void SetterUsesPointSizeAndGetterReturnsAPointUnitFont(GraphicsUnit unit, float size)
        => OnSta(() =>
        {
            using var rich = new FontRichTextBox { Text = "units" };
            using var supplied = new Font(FontFamily.GenericSansSerif, size,
                FontStyle.Bold | FontStyle.Italic, unit);
            rich.SelectAll();

            rich.SelectionFont = supplied;

            NativeFormat native = ReadFormat(rich.Handle);
            Assert.Equal((int)(supplied.SizeInPoints * 20), native.Height);
            using Font actual = Assert.IsType<Font>(rich.SelectionFont);
            Assert.Equal(GraphicsUnit.Point, actual.Unit);
            Assert.InRange(actual.SizeInPoints, native.Height / 20f - 0.01f, native.Height / 20f + 0.01f);
            Assert.Equal(supplied.Style, actual.Style);
        });

    [Fact]
    public void ReusingTheSameFontFormatsEachNewSelection()
        => OnSta(() =>
        {
            using var rich = new FontRichTextBox { Text = "abcdef" };
            using var supplied = new Font(FontFamily.GenericMonospace, 18, FontStyle.Bold);
            rich.Select(0, 3);
            rich.SelectionFont = supplied;
            rich.Select(3, 3);
            rich.SelectionFont = supplied;
            rich.SelectAll();

            using Font actual = Assert.IsType<Font>(rich.SelectionFont);
            AssertFont(supplied, actual);
        });

    [Fact]
    public void ApplyingAFontOnlyChangesTheSelectedCharacters()
        => OnSta(() =>
        {
            using var rich = new FontRichTextBox { Text = "abcdef" };
            _ = rich.Handle;
            rich.Select(0, 1);
            NativeFormat untouched = ReadFormat(rich.Handle);
            using var supplied = new Font(FontFamily.GenericMonospace, 24, FontStyle.Bold);
            rich.Select(2, 2);

            rich.SelectionFont = supplied;

            using (Font actual = Assert.IsType<Font>(rich.SelectionFont)) { AssertFont(supplied, actual); }
            rich.Select(0, 1);
            NativeFormat after = ReadFormat(rich.Handle);
            Assert.Equal(untouched.FaceName, after.FaceName);
            Assert.Equal(untouched.Height, after.Height);
            Assert.Equal(untouched.Effects & 15, after.Effects & 15);
            rich.Select(4, 1);
            NativeFormat last = ReadFormat(rich.Handle);
            Assert.Equal(untouched.FaceName, last.FaceName);
            Assert.Equal(untouched.Height, last.Height);
        });

    [Theory]
    [InlineData(FontStyle.Regular)]
    [InlineData(FontStyle.Bold | FontStyle.Italic)]
    [InlineData(FontStyle.Underline | FontStyle.Strikeout)]
    public void IndependentRichEditUsesTheSameFontFormattingProtocol(FontStyle style)
        => OnSta(() =>
        {
            using var rich = new FontRichTextBox { Text = "independent" };
            using var supplied = new Font(FontFamily.GenericMonospace, 17.5f, style,
                GraphicsUnit.Point, 1);
            nint native = CreateIndependent(rich.Handle, "independent");
            try
            {
                rich.Select(1, 4);
                SetNativeSelection(native, 1, 5);

                rich.SelectionFont = supplied;
                SetNativeFont(native, supplied);

                NativeFormat actual = ReadFormat(rich.Handle);
                NativeFormat expected = ReadFormat(native);
                Assert.Equal(expected.FaceName, actual.FaceName);
                Assert.Equal(expected.Height, actual.Height);
                Assert.Equal(expected.CharSet, actual.CharSet);
                Assert.Equal(expected.PitchAndFamily, actual.PitchAndFamily);
                Assert.Equal(expected.Effects & 15, actual.Effects & 15);
                Assert.Equal(expected.Mask & FontMask, actual.Mask & FontMask);
                using Font returned = Assert.IsType<Font>(rich.SelectionFont);
                AssertFont(supplied, returned);
            }
            finally { Assert.True(DestroyWindow(native)); }
        });

    [Fact]
    public void GetterSeesLatestNativeFontAndReturnsIndependentOwnedFontObjects()
        => OnSta(() =>
        {
            using var rich = new FontRichTextBox { Text = "latest" };
            rich.SelectAll();
            using var first = new Font(FontFamily.GenericSansSerif, 11, FontStyle.Italic);
            using var second = new Font(FontFamily.GenericMonospace, 22, FontStyle.Bold);
            SetNativeFont(rich.Handle, first);
            using Font initial = Assert.IsType<Font>(rich.SelectionFont);
            AssertFont(first, initial);

            SetNativeFont(rich.Handle, second);

            using Font latest = Assert.IsType<Font>(rich.SelectionFont);
            using Font again = Assert.IsType<Font>(rich.SelectionFont);
            AssertFont(second, latest);
            AssertFont(second, again);
            Assert.NotSame(latest, again);
            AssertFont(first, initial);
        });

    [Fact]
    public void MixedFaceNamesProduceNullAndSelectingEachRunReturnsItsFont()
        => OnSta(() =>
        {
            using var rich = new FontRichTextBox { Text = "aabb" };
            using var first = new Font(FontFamily.GenericSansSerif, 14);
            using var second = new Font(FontFamily.GenericMonospace, 14);
            Assert.NotEqual(first.Name, second.Name);
            rich.Select(0, 2);
            rich.SelectionFont = first;
            rich.Select(2, 2);
            rich.SelectionFont = second;
            rich.SelectAll();

            Assert.Equal(0u, ReadFormat(rich.Handle).Mask & 0x20000000u);
            Assert.Null(rich.SelectionFont);

            rich.Select(0, 2);
            using (Font actual = Assert.IsType<Font>(rich.SelectionFont)) { AssertFont(first, actual); }
            rich.Select(2, 2);
            using (Font actual = Assert.IsType<Font>(rich.SelectionFont)) { AssertFont(second, actual); }
        });

    [Fact]
    public void MixedSizesKeepTheCommonFaceAndUseTheFixedThirteenPointFallback()
        => OnSta(() =>
        {
            using var rich = new FontRichTextBox { Text = "aabb" };
            using var first = new Font(FontFamily.GenericSansSerif, 10);
            using var second = new Font(FontFamily.GenericSansSerif, 20);
            rich.Select(0, 2);
            rich.SelectionFont = first;
            rich.Select(2, 2);
            rich.SelectionFont = second;
            rich.SelectAll();
            NativeFormat native = ReadFormat(rich.Handle);
            Assert.NotEqual(0u, native.Mask & 0x20000000u);
            Assert.Equal(0u, native.Mask & 0x80000000u);

            using Font actual = Assert.IsType<Font>(rich.SelectionFont);

            Assert.Equal(first.FontFamily.Name, actual.FontFamily.Name);
            Assert.Equal(13f, actual.SizeInPoints);
            Assert.Equal(FontStyle.Regular, actual.Style);
        });

    [Theory]
    [InlineData(FontStyle.Bold, 1u)]
    [InlineData(FontStyle.Italic, 2u)]
    [InlineData(FontStyle.Underline, 4u)]
    [InlineData(FontStyle.Strikeout, 8u)]
    public void MixedStyleBitsDoNotDiscardTheCommonFaceAndSize(FontStyle style, uint nativeStyle)
        => OnSta(() =>
        {
            using var rich = new FontRichTextBox { Text = "aabb" };
            using var first = new Font(FontFamily.GenericSansSerif, 16, FontStyle.Regular);
            using var second = new Font(FontFamily.GenericSansSerif, 16, style);
            rich.Select(0, 2);
            rich.SelectionFont = first;
            rich.Select(2, 2);
            rich.SelectionFont = second;
            rich.SelectAll();
            Assert.Equal(0u, ReadFormat(rich.Handle).Mask & nativeStyle);

            using Font actual = Assert.IsType<Font>(rich.SelectionFont);

            Assert.Equal(first.FontFamily.Name, actual.FontFamily.Name);
            Assert.Equal(16f, actual.SizeInPoints);
            Assert.Equal(FontStyle.Regular, actual.Style);
        });

    [Fact]
    public void AnEmptyDocumentHasAnInsertionFontThatCanBeChanged()
        => OnSta(() =>
        {
            using var rich = new FontRichTextBox();
            using Font initial = Assert.IsType<Font>(rich.SelectionFont);
            Assert.True(rich.IsHandleCreated);
            using var supplied = new Font(FontFamily.GenericMonospace, 19, FontStyle.Bold);

            rich.SelectionFont = supplied;

            using Font actual = Assert.IsType<Font>(rich.SelectionFont);
            AssertFont(supplied, actual);
            Assert.Equal(string.Empty, rich.Text);
            Assert.Equal(0, rich.SelectionStart);
            Assert.Equal(0, rich.SelectionLength);
        });

    [Fact]
    public void EmptySelectionFontBecomesTheFormatOfInsertedText()
        => OnSta(() =>
        {
            using var rich = new FontRichTextBox { Text = "abcd" };
            using var supplied = new Font(FontFamily.GenericMonospace, 21, FontStyle.Bold | FontStyle.Italic);
            rich.Select(2, 0);

            rich.SelectionFont = supplied;
            rich.SelectedText = "XY";
            rich.Select(2, 2);

            using Font actual = Assert.IsType<Font>(rich.SelectionFont);
            AssertFont(supplied, actual);
            Assert.Equal("abXYcd", rich.Text);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void GetterPreservesTextRtfSelectionLimitUndoModifiedAndNotifications(bool modified)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new FontRichTextBox { Text = "abcdef", MaxLength = 3 };
            CreateHostedHandle(form, rich);
            rich.Select(1, 2);
            rich.SelectedText = "xyz";
            rich.Select(2, 1);
            using var supplied = new Font(FontFamily.GenericMonospace, 18, FontStyle.Underline);
            rich.SelectionFont = supplied;
            rich.Modified = modified;
            string text = rich.Text;
            string? rtf = rich.Rtf;
            NativeState before = ReadState(rich.Handle);
            int textChanges = 0;
            int modifiedChanges = 0;
            rich.TextChanged += (_, _) => textChanges++;
            rich.ModifiedChanged += (_, _) => modifiedChanges++;

            using Font first = Assert.IsType<Font>(rich.SelectionFont);
            using Font second = Assert.IsType<Font>(rich.SelectionFont);

            AssertFont(supplied, first);
            AssertFont(supplied, second);
            Assert.Equal(before, ReadState(rich.Handle));
            Assert.Equal(text, rich.Text);
            Assert.Equal(rtf, rich.Rtf);
            Assert.Equal(modified, rich.Modified);
            Assert.Equal((nint)3, SendMessageW(rich.Handle, 0x00D5, 0, 0));
            Assert.Equal(0, textChanges);
            Assert.Equal(0, modifiedChanges);
        });

    [Fact]
    public void ReadingTheFontDoesNotConsumeThePendingNativeUndo()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new FontRichTextBox { Text = "before" };
            CreateHostedHandle(form, rich);
            rich.Select(1, 2);
            rich.SelectedText = "XY";
            Assert.True(rich.CanUndo);

            using Font actual = Assert.IsType<Font>(rich.SelectionFont);
            rich.Undo();

            Assert.Equal("before", rich.Text);
        });

    [Fact]
    public void SetterPreservesUnrelatedCharacterAndParagraphFormatting()
        => OnSta(() =>
        {
            using var rich = new FontRichTextBox { Text = "first\nsecond" };
            rich.Select(1, 3);
            rich.SelectionColor = Color.DarkRed;
            rich.SelectionBackColor = Color.Gold;
            rich.SelectionCharOffset = 12;
            rich.SelectionProtected = true;
            rich.SelectionAlignment = HorizontalAlignment.Center;
            rich.SelectionIndent = 24;
            rich.SelectionBullet = true;
            rich.SelectionTabs = [48, 96];
            NativeFormat before = ReadFormat(rich.Handle);
            int indent = rich.SelectionIndent;
            using var supplied = new Font(FontFamily.GenericMonospace, 18, FontStyle.Bold | FontStyle.Italic);

            rich.SelectionFont = supplied;

            NativeFormat after = ReadFormat(rich.Handle);
            Assert.Equal(before.TextColor, after.TextColor);
            Assert.Equal(before.BackColor, after.BackColor);
            Assert.Equal(before.Offset, after.Offset);
            Assert.Equal(before.Effects & ~15u, after.Effects & ~15u);
            Assert.True(rich.SelectionProtected);
            Assert.True(rich.SelectionBullet);
            Assert.Equal(HorizontalAlignment.Center, rich.SelectionAlignment);
            Assert.Equal(indent, rich.SelectionIndent);
            Assert.Equal(new[] { 48, 96 }, rich.SelectionTabs);
            Assert.Equal("first\nsecond", rich.Text);
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            using Font actual = Assert.IsType<Font>(rich.SelectionFont);
            AssertFont(supplied, actual);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void SetterStateChangesMatchIndependentRichEditRatherThanManagedSuccessFlags(bool modified)
        => OnSta(() =>
        {
            using var rich = new FontRichTextBox { Text = "abcdef" };
            using var supplied = new Font(FontFamily.GenericMonospace, 18, FontStyle.Bold);
            nint native = CreateIndependent(rich.Handle, "abcdef");
            try
            {
                rich.Select(1, 3);
                SetNativeSelection(native, 1, 4);
                rich.ClearUndo();
                _ = SendMessageW(native, 0x00CD, 0, 0);
                rich.Modified = modified;
                _ = SendMessageW(native, 0x00B9, modified ? 1 : 0, 0);
                Assert.Equal(ReadState(native), ReadState(rich.Handle));

                rich.SelectionFont = supplied;
                SetNativeFont(native, supplied);

                Assert.Equal(ReadState(native), ReadState(rich.Handle));
                Assert.Equal("abcdef", rich.Text);
                using Font actual = Assert.IsType<Font>(rich.SelectionFont);
                AssertFont(supplied, actual);
            }
            finally { Assert.True(DestroyWindow(native)); }
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void RtfHandleRecreationPreservesTheSelectedFontAndEditingState(bool modified)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new FontRichTextBox { Text = "first\nsecond", MaxLength = 7 };
            CreateHostedHandle(form, rich);
            rich.Select(1, 3);
            using var supplied = new Font(FontFamily.GenericMonospace, 18,
                FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
            rich.SelectionFont = supplied;
            rich.SelectionColor = Color.DarkRed;
            rich.SelectionBackColor = Color.Gold;
            rich.SelectionAlignment = HorizontalAlignment.Right;
            rich.Modified = modified;
            string before = rich.Text;
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;

            rich.RecreateNow();

            Assert.Equal(2, rich.CreatedCount);
            using Font actual = Assert.IsType<Font>(rich.SelectionFont);
            AssertFont(supplied, actual);
            Assert.Equal(Color.DarkRed.ToArgb(), rich.SelectionColor.ToArgb());
            Assert.Equal(Color.Gold.ToArgb(), rich.SelectionBackColor.ToArgb());
            Assert.Equal(HorizontalAlignment.Right, rich.SelectionAlignment);
            Assert.Equal(before, rich.Text);
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.Equal(modified, rich.Modified);
            Assert.False(rich.CanUndo);
            Assert.Equal((nint)7, SendMessageW(rich.Handle, 0x00D5, 0, 0));
            Assert.Equal(0, changes);
        });

    [Fact]
    public void BorrowedFontRemainsUsableAfterControlDisposal()
        => OnSta(() =>
        {
            using var supplied = new Font(FontFamily.GenericMonospace, 18, FontStyle.Bold);
            using (var rich = new FontRichTextBox { Text = "borrowed" })
            {
                rich.SelectAll();
                rich.SelectionFont = supplied;
                using Font returned = Assert.IsType<Font>(rich.SelectionFont);
                Assert.NotSame(supplied, returned);
                AssertFont(supplied, returned);
            }

            Assert.Equal(18f, supplied.SizeInPoints);
            nint handle = supplied.ToHfont();
            Assert.NotEqual((nint)0, handle);
            Assert.True(DeleteObject(handle));
        });

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

    private static NativeFormat ReadFormat(nint handle)
    {
        nint buffer = Marshal.AllocHGlobal(116);
        try
        {
            Marshal.Copy(new byte[116], 0, buffer, 116);
            Marshal.WriteInt32(buffer, 116);
            _ = SendMessageW(handle, EmGetCharFormat, 1, buffer);
            byte[] bytes = new byte[116];
            Marshal.Copy(buffer, bytes, 0, bytes.Length);
            return new NativeFormat(BitConverter.ToUInt32(bytes, 4), BitConverter.ToUInt32(bytes, 8),
                BitConverter.ToInt32(bytes, 12), BitConverter.ToInt32(bytes, 16),
                BitConverter.ToInt32(bytes, 20), bytes[24], bytes[25],
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
            _ = SendMessageW(handle, EmSetCharFormat, 1, format);
        }
        finally
        {
            Marshal.FreeHGlobal(format);
            Marshal.FreeHGlobal(logicalFont);
            Assert.True(DeleteObject(fontHandle));
        }
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
            _ = SendMessageW(handle, EmExSetSel, 0, range);
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "RichTextBox SelectionFont native test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private readonly record struct NativeFormat(uint Mask, uint Effects, int Height, int Offset,
        int TextColor, byte CharSet, byte PitchAndFamily, string FaceName, int BackColor);
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
