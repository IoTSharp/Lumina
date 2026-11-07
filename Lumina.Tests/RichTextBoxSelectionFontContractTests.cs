using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class RichTextBoxSelectionFontContractTests
{
    private const int EmGetCharFormat = 0x043A;
    private const int EmSetCharFormat = 0x0444;
    private const uint CfmFace = 0x20000000;
    private const uint CfmSize = 0x80000000;
    private const uint CfmCharSet = 0x08000000;
    private const uint FontMask = CfmFace | CfmSize | CfmCharSet | 15;

    [Fact]
    public void SelectionFontHasTheStandardDeclaredNullableAndDisallowNullContract()
    {
        PropertyInfo property = typeof(RichTextBox).GetProperty(nameof(RichTextBox.SelectionFont),
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)!;
        Assert.NotNull(property);
        Assert.Equal(typeof(Font), property.PropertyType);
        Assert.True(property.GetMethod!.IsPublic);
        Assert.True(property.SetMethod!.IsPublic);
        Assert.False(property.GetMethod.IsVirtual);
        Assert.False(property.SetMethod.IsVirtual);
        Assert.False(property.GetCustomAttribute<BrowsableAttribute>()!.Browsable);
        Assert.Equal(DesignerSerializationVisibility.Hidden,
            property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>()!.Visibility);
        Assert.NotNull(property.SetMethod.GetParameters()[0].GetCustomAttribute<DisallowNullAttribute>());
        NullabilityInfo nullable = new NullabilityInfoContext().Create(property);
        Assert.Equal(NullabilityState.Nullable, nullable.ReadState);
        Assert.Equal(NullabilityState.NotNull, nullable.WriteState);
    }

    [Fact]
    public void GetterCreatesTheWindowAndMaterializesCachedTextBeforeQueryingTheSelection()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Text = "cached font text" };
            Assert.False(rich.IsHandleCreated);

            using Font font = Assert.IsType<Font>(rich.SelectionFont);

            Assert.True(rich.IsHandleCreated);
            Assert.Equal(1, rich.CreatedCount);
            FormatMessage query = Assert.Single(rich.Messages);
            Assert.Equal(EmGetCharFormat, query.Id);
            Assert.Equal((nint)1, query.WParam);
            Assert.Equal(1, query.CreatedCount);
            Assert.Equal("cached font text", rich.Text);
        });

    [Fact]
    public void GetterPassesANewZeroCharacterFormatBufferAndSelectionFlagOnEveryQuery()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox();
            _ = rich.Handle;
            byte[] expected = new byte[116];
            BitConverter.GetBytes(116).CopyTo(expected, 0);

            using Font first = Assert.IsType<Font>(rich.SelectionFont);
            using Font second = Assert.IsType<Font>(rich.SelectionFont);

            Assert.Equal(2, rich.Messages.Count);
            Assert.All(rich.Messages, message =>
            {
                Assert.Equal(EmGetCharFormat, message.Id);
                Assert.Equal((nint)1, message.WParam);
                Assert.Equal(expected, message.Bytes);
            });
            Assert.NotSame(first, second);
        });

    [Theory]
    [InlineData(0u)]
    [InlineData(CfmSize)]
    [InlineData(CfmCharSet | 15u)]
    public void GetterReturnsNullWhenTheNativeFaceMaskIsAbsent(uint mask)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Reply = MakeReply(mask, 15, 240) };

            Assert.Null(rich.SelectionFont);

            Assert.Single(rich.Messages);
            Assert.True(rich.IsHandleCreated);
        });

    [Fact]
    public void GetterUsesThirteenPointRegularWhenSizeAndStyleMasksAreAbsent()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Reply = MakeReply(CfmFace, 15, -400) };

            using Font font = Assert.IsType<Font>(rich.SelectionFont);

            Assert.Equal(13f, font.Size);
            Assert.Equal(GraphicsUnit.Point, font.Unit);
            Assert.Equal(FontStyle.Regular, font.Style);
            Assert.Equal(FontFamily.GenericSansSerif.Name, font.Name);
        });

    [Theory]
    [InlineData(1, 0.05f)]
    [InlineData(200, 10f)]
    [InlineData(251, 12.55f)]
    [InlineData(720, 36f)]
    public void GetterConvertsTheNativeTwipHeightToPointSize(int twips, float expected)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Reply = MakeReply(CfmFace | CfmSize, 0, twips) };

            using Font font = Assert.IsType<Font>(rich.SelectionFont);

            Assert.Equal(expected, font.Size);
            Assert.Equal(GraphicsUnit.Point, font.Unit);
        });

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void GetterReturnsNullForAnInvalidNativeFontSize(int twips)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Reply = MakeReply(CfmFace | CfmSize, 15, twips) };

            Assert.Null(rich.SelectionFont);

            Assert.Single(rich.Messages);
        });

    [Theory]
    [InlineData(1u, 1u, FontStyle.Bold)]
    [InlineData(2u, 2u, FontStyle.Italic)]
    [InlineData(4u, 4u, FontStyle.Underline)]
    [InlineData(8u, 8u, FontStyle.Strikeout)]
    [InlineData(15u, 15u, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline | FontStyle.Strikeout)]
    [InlineData(1u, 0u, FontStyle.Regular)]
    [InlineData(2u, 0u, FontStyle.Regular)]
    [InlineData(4u, 0u, FontStyle.Regular)]
    [InlineData(8u, 0u, FontStyle.Regular)]
    [InlineData(0u, 1u, FontStyle.Regular)]
    [InlineData(0u, 2u, FontStyle.Regular)]
    [InlineData(0u, 4u, FontStyle.Regular)]
    [InlineData(0u, 8u, FontStyle.Regular)]
    [InlineData(5u, 15u, FontStyle.Bold | FontStyle.Underline)]
    public void GetterRequiresBothTheMaskAndEffectForEachFontStyle(uint mask, uint effects, FontStyle expected)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Reply = MakeReply(CfmFace | CfmSize | mask, effects, 240) };

            using Font font = Assert.IsType<Font>(rich.SelectionFont);

            Assert.Equal(expected, font.Style);
            Assert.Equal(12f, font.Size);
        });

    [Theory]
    [InlineData((byte)0)]
    [InlineData((byte)1)]
    [InlineData((byte)2)]
    [InlineData((byte)128)]
    public void GetterPassesTheNativeCharsetEvenWithoutItsMask(byte charSet)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Reply = MakeReply(CfmFace | CfmSize, 0, 240, charSet) };

            using Font font = Assert.IsType<Font>(rich.SelectionFont);

            Assert.Equal(charSet, font.GdiCharSet);
        });

    [Fact]
    public void GetterReadsTheLatestNativeFontWithoutCachingOrDisposingAnEarlierResult()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Reply = MakeReply(CfmFace | CfmSize | 1, 1, 220) };
            using Font first = Assert.IsType<Font>(rich.SelectionFont);
            rich.Reply = MakeReply(CfmFace | CfmSize | 2, 2, 360);

            using Font second = Assert.IsType<Font>(rich.SelectionFont);

            Assert.Equal(11f, first.Size);
            Assert.Equal(FontStyle.Bold, first.Style);
            Assert.Equal(18f, second.Size);
            Assert.Equal(FontStyle.Italic, second.Style);
            Assert.NotSame(first, second);
            Assert.Equal(2, rich.Messages.Count);
            AssertFontStillUsable(first);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void CreationFailurePrecedesGetterOrSetterWorkAndAllowsRetry(bool setter)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { FailCreation = true };
            using var input = new Font(FontFamily.GenericSansSerif, 12f);
            Action operation = setter ? () => rich.SelectionFont = input : () => _ = rich.SelectionFont;

            Assert.Same(rich.CreationFailure, Assert.Throws<InvalidOperationException>(operation));
            Assert.False(rich.IsHandleCreated);
            Assert.Empty(rich.Messages);

            rich.FailCreation = false;
            if (setter) { rich.SelectionFont = input; }
            else { using Font result = Assert.IsType<Font>(rich.SelectionFont); }
            Assert.True(rich.IsHandleCreated);
            Assert.Single(rich.Messages);
        });

    [Fact]
    public void SetterCreatesTheWindowBeforeDereferencingNullAndDoesNotSubmitAFormat()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox();

            Assert.Throws<NullReferenceException>(() => rich.SelectionFont = null!);

            Assert.True(rich.IsHandleCreated);
            Assert.Equal(1, rich.CreatedCount);
            Assert.Empty(rich.Messages);
        });

    [Fact]
    public void CreationFailureTakesPrecedenceOverTheNullSetterFailure()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { FailCreation = true };

            Assert.Same(rich.CreationFailure,
                Assert.Throws<InvalidOperationException>(() => rich.SelectionFont = null!));

            Assert.False(rich.IsHandleCreated);
            Assert.Empty(rich.Messages);
        });

    [Theory]
    [InlineData(FontStyle.Regular, GraphicsUnit.Point)]
    [InlineData(FontStyle.Bold, GraphicsUnit.Point)]
    [InlineData(FontStyle.Italic, GraphicsUnit.Point)]
    [InlineData(FontStyle.Underline, GraphicsUnit.Point)]
    [InlineData(FontStyle.Strikeout, GraphicsUnit.Point)]
    [InlineData(FontStyle.Bold | FontStyle.Italic | FontStyle.Underline | FontStyle.Strikeout, GraphicsUnit.Point)]
    [InlineData(FontStyle.Regular, GraphicsUnit.Pixel)]
    [InlineData(FontStyle.Italic, GraphicsUnit.Document)]
    public void SetterSubmitsThePreciseFontMaskPointHeightAndNativeLogicalFont(FontStyle style, GraphicsUnit unit)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox();
            using var font = new Font(FontFamily.GenericSansSerif.Name, 12.75f, style, unit, 1);
            byte[] logical = ReadLogicalFont(font);

            rich.SelectionFont = font;

            FormatMessage message = Assert.Single(rich.Messages);
            Assert.Equal(EmSetCharFormat, message.Id);
            Assert.Equal((nint)1, message.WParam);
            Assert.Equal(1, message.CreatedCount);
            byte[] expected = new byte[116];
            BitConverter.GetBytes(116).CopyTo(expected, 0);
            BitConverter.GetBytes(FontMask).CopyTo(expected, 4);
            BitConverter.GetBytes((uint)style).CopyTo(expected, 8);
            BitConverter.GetBytes((int)(font.SizeInPoints * 20)).CopyTo(expected, 12);
            expected[24] = logical[23];
            expected[25] = logical[27];
            Array.Copy(logical, 28, expected, 26, 64);
            Assert.Equal(expected, message.Bytes);
            AssertFontStillUsable(font);
        });

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(-1)]
    public void SetterIgnoresTheNativeReturnAndSubmitsRepeatedAssignmentsWithoutAQuery(int result)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { SetResult = result };
            using var font = new Font(FontFamily.GenericSansSerif, 11.25f, FontStyle.Bold);

            rich.SelectionFont = font;
            rich.SelectionFont = font;

            Assert.Equal(2, rich.Messages.Count);
            Assert.All(rich.Messages, message => Assert.Equal(EmSetCharFormat, message.Id));
            Assert.Equal(rich.Messages[0].Bytes, rich.Messages[1].Bytes);
            AssertFontStillUsable(font);
        });

    [Fact]
    public void NeitherAccessorReadsVirtualTextSelectionLengthOrControlFontOnceTheHandleExists()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox();
            _ = rich.Handle;
            rich.FailUnrelatedGetters = true;
            using var input = new Font(FontFamily.GenericSansSerif, 12f);

            using Font result = Assert.IsType<Font>(rich.SelectionFont);
            rich.SelectionFont = input;

            Assert.Equal(new[] { EmGetCharFormat, EmSetCharFormat }, rich.Messages.Select(message => message.Id));
            rich.FailUnrelatedGetters = false;
        });

    [Fact]
    public void ControlDisposalDoesNotDisposeTheBorrowedSetterFontOrAnOwnedGetterResult()
        => OnSta(() =>
        {
            using var input = new Font(FontFamily.GenericSansSerif, 12f);
            using var rich = new FormatRichTextBox();
            rich.SelectionFont = input;
            using Font result = Assert.IsType<Font>(rich.SelectionFont);

            rich.Dispose();

            AssertFontStillUsable(input);
            AssertFontStillUsable(result);
        });

    private static byte[] MakeReply(uint mask, uint effects, int height, byte charSet = 1)
    {
        byte[] bytes = new byte[116];
        BitConverter.GetBytes(116).CopyTo(bytes, 0);
        BitConverter.GetBytes(mask).CopyTo(bytes, 4);
        BitConverter.GetBytes(effects).CopyTo(bytes, 8);
        BitConverter.GetBytes(height).CopyTo(bytes, 12);
        bytes[24] = charSet;
        byte[] face = Encoding.Unicode.GetBytes(FontFamily.GenericSansSerif.Name);
        Array.Copy(face, 0, bytes, 26, Math.Min(face.Length, 62));
        return bytes;
    }

    private static byte[] ReadLogicalFont(Font font)
    {
        nint nativeFont = font.ToHfont();
        nint buffer = Marshal.AllocHGlobal(92);
        try
        {
            Marshal.Copy(new byte[92], 0, buffer, 92);
            Assert.Equal(92, GetObjectW(nativeFont, 92, buffer));
            byte[] bytes = new byte[92];
            Marshal.Copy(buffer, bytes, 0, bytes.Length);
            return bytes;
        }
        finally
        {
            Marshal.FreeHGlobal(buffer);
            Assert.True(DeleteObject(nativeFont));
        }
    }

    private static void AssertFontStillUsable(Font font)
    {
        nint handle = font.ToHfont();
        Assert.NotEqual((nint)0, handle);
        Assert.True(DeleteObject(handle));
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "RichTextBox selection font contract test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private readonly record struct FormatMessage(int Id, nint WParam, byte[] Bytes, int CreatedCount);

    private sealed class FormatRichTextBox : RichTextBox
    {
        internal byte[] Reply { get; set; } = MakeReply(CfmFace | CfmSize, 0, 240);
        internal List<FormatMessage> Messages { get; } = [];
        internal bool FailCreation { get; set; }
        internal bool FailUnrelatedGetters { get; set; }
        internal int CreatedCount { get; private set; }
        internal nint SetResult { get; set; } = 1;
        internal InvalidOperationException CreationFailure { get; } = new("Selection font creation failure");

        public override string Text
        {
            get => FailUnrelatedGetters ? throw new InvalidOperationException("Text must not be read.") : base.Text;
            set => base.Text = value;
        }

        public override int SelectionLength
        {
            get => FailUnrelatedGetters ? throw new InvalidOperationException("SelectionLength must not be read.") : base.SelectionLength;
            set => base.SelectionLength = value;
        }

        public override Font Font
        {
            get => FailUnrelatedGetters ? throw new InvalidOperationException("Control.Font must not be read.") : base.Font;
            set => base.Font = value;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                if (FailCreation) { throw CreationFailure; }
                return base.CreateParams;
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            CreatedCount++;
        }

        protected override void WndProc(ref Message message)
        {
            if (message.Msg is EmGetCharFormat or EmSetCharFormat)
            {
                byte[] bytes = new byte[116];
                Marshal.Copy(message.LParam, bytes, 0, bytes.Length);
                Messages.Add(new FormatMessage(message.Msg, message.WParam, bytes, CreatedCount));
                if (message.Msg == EmGetCharFormat)
                {
                    Marshal.Copy(Reply, 0, message.LParam, Reply.Length);
                    message.Result = 0;
                }
                else { message.Result = SetResult; }
                return;
            }
            base.WndProc(ref message);
        }
    }

    [DllImport("gdi32.dll", ExactSpelling = true)]
    private static extern int GetObjectW(nint handle, int bytes, nint buffer);

    [DllImport("gdi32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DeleteObject(nint handle);
}
