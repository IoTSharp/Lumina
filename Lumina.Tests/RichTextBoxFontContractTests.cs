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
public sealed class RichTextBoxFontContractTests
{
    private const int EmGetCharFormat = 0x043A;
    private const int EmSetCharFormat = 0x0444;
    private const uint FaceMask = 0x20000000;
    private const uint SizeMask = 0x80000000;
    private const uint CharsetMask = 0x08000000;
    private const uint FontMask = FaceMask | SizeMask | CharsetMask | 15;

    [Fact]
    public void FontDeclaresTheStandardAllowNullOverride()
    {
        PropertyInfo property = typeof(RichTextBox).GetProperty(nameof(RichTextBox.Font),
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)!;
        Assert.NotNull(property);
        Assert.Equal(typeof(Font), property.PropertyType);
        Assert.True(property.GetMethod!.IsPublic);
        Assert.True(property.SetMethod!.IsPublic);
        Assert.True(property.GetMethod.IsVirtual);
        Assert.True(property.SetMethod.IsVirtual);
        Assert.Equal(typeof(Control), property.GetMethod.GetBaseDefinition().DeclaringType);
        Assert.Equal(typeof(Control), property.SetMethod.GetBaseDefinition().DeclaringType);
        Assert.NotNull(property.SetMethod.GetParameters()[0].GetCustomAttribute<AllowNullAttribute>());
        NullabilityInfo nullable = new NullabilityInfoContext().Create(property);
        Assert.Equal(NullabilityState.NotNull, nullable.ReadState);
        Assert.Equal(NullabilityState.Nullable, nullable.WriteState);
    }

    [Fact]
    public void GetterUsesTheManagedFontWithoutCreatingOrQueryingTheWindow()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Capture = true, FailCreation = true };

            Assert.Same(Control.DefaultFont, rich.Font);

            Assert.False(rich.IsHandleCreated);
            Assert.Empty(rich.Messages);
            Assert.Equal(0, rich.CreatedCount);
        });

    [Theory]
    [InlineData(FontStyle.Regular)]
    [InlineData(FontStyle.Bold)]
    [InlineData(FontStyle.Italic)]
    public void GenericFamilyAndNamedFamilyHaveDistinctDrawingIdentityDespiteMatchingVisibleFontProperties(FontStyle style)
        => OnSta(() =>
        {
            // Fixed Drawing 10.0.11 FontFamily.Equals compares the native
            // family identity. The RichEdit conversion creates by face name,
            // whereas a generic-family Font can carry another native identity.
            using var generic = new Font(FontFamily.GenericSansSerif, 18f, style);
            using var named = new Font(generic.Name, 18f, style, GraphicsUnit.Point, 1);
            using var namedAgain = new Font(generic.Name, 18f, style, GraphicsUnit.Point, 1);

            Assert.Equal(generic.Name, named.Name);
            Assert.Equal(generic.Size, named.Size);
            Assert.Equal(generic.Style, named.Style);
            Assert.Equal(generic.Unit, named.Unit);
            Assert.Equal(generic.GdiCharSet, named.GdiCharSet);
            Assert.Equal(generic.GdiVerticalFont, named.GdiVerticalFont);
            Assert.Equal(generic.ToString(), named.ToString());
            Assert.False(generic.FontFamily.Equals(named.FontFamily));
            Assert.False(generic.Equals(named));
            Assert.True(named.FontFamily.Equals(namedAgain.FontFamily));
            Assert.Equal(named, namedAgain);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void SetterWithoutAWindowUsesTheBaseFontWithoutReadingCreateParams(bool reset)
        => OnSta(() =>
        {
            using var first = new Font(FontFamily.GenericSansSerif, 11f);
            using var next = new Font(FontFamily.GenericSansSerif, 17f, FontStyle.Bold);
            using var rich = new FormatRichTextBox { Font = first, Text = "cached text" };
            rich.FailCreation = true;
            rich.Capture = true;
            int changed = 0;
            rich.FontChanged += (_, _) => changed++;

            rich.Font = reset ? null! : next;

            Assert.Same(reset ? Control.DefaultFont : next, rich.Font);
            Assert.False(rich.IsHandleCreated);
            Assert.Empty(rich.Messages);
            Assert.Equal(1, changed);
            Assert.Equal("cached text", rich.Text);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void EmptyNativeTextUsesOnlyTheBaseFontAndDoesNotQueryOrSubmitCharacterFormats(bool reset)
        => OnSta(() =>
        {
            using var first = new Font(FontFamily.GenericSansSerif, 11f);
            using var next = new Font(FontFamily.GenericSansSerif, 17f, FontStyle.Bold);
            using var rich = new FormatRichTextBox { Font = first };
            rich.BeginCapture();
            int changed = 0;
            rich.FontChanged += (_, _) => changed++;

            rich.Font = reset ? null! : next;

            Assert.Same(reset ? Control.DefaultFont : next, rich.Font);
            Assert.Empty(rich.Messages);
            Assert.Equal(1, changed);
            Assert.Equal(1, rich.CreatedCount);
        });

    [Fact]
    public void EmptyNativeTextDoesNotBecomeNonemptyBecauseAVirtualTextGetterSaysSo()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox();
            rich.BeginCapture();
            rich.VirtualText = "virtual text does not exist in RichEdit";
            using var input = new Font(FontFamily.GenericSansSerif, 16f);

            rich.Font = input;

            Assert.Same(input, rich.BaseFont);
            Assert.Empty(rich.Messages);
            Assert.Equal((nint)0, SendMessageW(rich.Handle, 0x000E, 0, 0));
        });

    [Fact]
    public void NativeNonemptyTextDoesNotReadVirtualTextOrSelectionAccessors()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Text = "abcdefgh" };
            rich.BeginCapture();
            rich.FailUnrelatedGetters = true;
            rich.EnqueueReply(Reply(12));
            rich.EnqueueReply(Reply(18));
            using var input = new Font(FontFamily.GenericSansSerif, 18f);

            rich.Font = input;

            Assert.Equal(new[] { EmGetCharFormat, EmSetCharFormat, EmGetCharFormat },
                rich.Messages.Select(message => message.Id));
            Assert.Equal(18f, rich.BaseFont.SizeInPoints);
            rich.FailUnrelatedGetters = false;
        });

    [Fact]
    public void MatchingNativeDefaultFontSkipsTheSetEvenWhenTheManagedFontDiffers()
        => OnSta(() =>
        {
            using var managed = new Font(FontFamily.GenericSansSerif, 10f);
            using var input = new Font(FontFamily.GenericSansSerif.Name, 18f, FontStyle.Italic);
            using var rich = new FormatRichTextBox { Font = managed, Text = "native default wins" };
            rich.BeginCapture();
            rich.EnqueueReply(Reply(input.SizeInPoints, input.Style));
            int changed = 0;
            rich.FontChanged += (_, _) => changed++;

            rich.Font = input;

            AssertDefaultQuery(Assert.Single(rich.Messages));
            Assert.Same(managed, rich.BaseFont);
            Assert.Equal(0, changed);
        });

    [Fact]
    public void MatchingManagedFontStillSubmitsWhenTheNativeDefaultDiffers()
        => OnSta(() =>
        {
            using var input = new Font(FontFamily.GenericSansSerif.Name, 18f, FontStyle.Bold);
            using var rich = new FormatRichTextBox { Font = input, Text = "native default differs" };
            rich.BeginCapture();
            rich.EnqueueReply(Reply(10));
            rich.EnqueueReply(Reply(input.SizeInPoints, input.Style));

            rich.Font = input;

            AssertThreeMessageSequence(rich);
            AssertFontPayload(input, rich.Messages[1]);
            Assert.Equal(input, rich.BaseFont);
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
    public void SetterSubmitsAZeroBasedAllTextFontPayloadAndFreshDefaultQueries(FontStyle style, GraphicsUnit unit)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Text = "abcdefgh" };
            using var input = new Font(FontFamily.GenericSansSerif.Name, 12.75f, style, unit, 1);
            rich.BeginCapture();
            rich.EnqueueReply(Reply(30));
            rich.EnqueueReply(Reply(17));

            rich.Font = input;

            AssertThreeMessageSequence(rich);
            AssertFontPayload(input, rich.Messages[1]);
            Assert.Equal(17f, rich.BaseFont.SizeInPoints);
            AssertFontStillUsable(input);
        });

    [Theory]
    [InlineData(0u)]
    [InlineData(SizeMask)]
    [InlineData(CharsetMask | 15u)]
    public void MissingFaceInTheFirstDefaultReplyDoesNotShortCircuitTheAssignment(uint mask)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Text = "abcdefgh" };
            using var input = new Font(FontFamily.GenericSansSerif, 18f);
            rich.BeginCapture();
            rich.EnqueueReply(Reply(input.SizeInPoints, mask: mask));
            rich.EnqueueReply(Reply(20));

            rich.Font = input;

            AssertThreeMessageSequence(rich);
            AssertFontPayload(input, rich.Messages[1]);
            Assert.Equal(20f, rich.BaseFont.SizeInPoints);
        });

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(-1)]
    public void SetterIgnoresTheNativeSetResultAndStillReadsTheResolvedDefault(int result)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Text = "abcdefgh", SetResult = result };
            using var input = new Font(FontFamily.GenericSansSerif, 18f, FontStyle.Bold);
            rich.BeginCapture();
            rich.EnqueueReply(Reply(10));
            rich.EnqueueReply(Reply(22, FontStyle.Italic));

            rich.Font = input;

            AssertThreeMessageSequence(rich);
            Assert.Equal(22f, rich.BaseFont.SizeInPoints);
            Assert.Equal(FontStyle.Italic, rich.BaseFont.Style);
            Assert.NotSame(input, rich.BaseFont);
        });

    [Fact]
    public void ResolvedDefaultReplacesTheManagedFontBeforeFontChanged()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Text = "abcdefgh" };
            using var input = new Font(FontFamily.GenericSansSerif, 18f, FontStyle.Bold);
            rich.BeginCapture();
            rich.EnqueueReply(Reply(10));
            rich.EnqueueReply(Reply(22, FontStyle.Italic));
            Font? seen = null;
            int messageCountAtNotification = -1;
            rich.FontChanged += (_, _) =>
            {
                seen = rich.BaseFont;
                messageCountAtNotification = rich.Messages.Count;
            };

            rich.Font = input;

            Assert.NotNull(seen);
            Assert.Same(rich.BaseFont, seen);
            Assert.Equal(22f, seen.SizeInPoints);
            Assert.Equal(FontStyle.Italic, seen.Style);
            Assert.Equal(3, messageCountAtNotification);
        });

    [Theory]
    [InlineData(0u, 240)]
    [InlineData(FaceMask | SizeMask, 0)]
    [InlineData(FaceMask | SizeMask, -1)]
    public void UnusableResolvedDefaultClearsTheLocalManagedFont(uint mask, int height)
        => OnSta(() =>
        {
            using var inherited = new Font(FontFamily.GenericSansSerif, 15f);
            using var initial = new Font(FontFamily.GenericSansSerif, 11f);
            using var input = new Font(FontFamily.GenericSansSerif, 18f);
            using var parent = new Control { Font = inherited };
            using var rich = new FormatRichTextBox { Font = initial, Text = "abcdefgh" };
            parent.Controls.Add(rich);
            rich.BeginCapture();
            rich.EnqueueReply(Reply(10));
            rich.EnqueueReply(Reply(12, mask: mask, height: height));

            rich.Font = input;

            AssertThreeMessageSequence(rich);
            Assert.Same(inherited, rich.BaseFont);
            AssertFontStillUsable(input);
            AssertFontStillUsable(initial);
        });

    [Fact]
    public void EachRepeatedAssignmentQueriesTheNativeDefaultAgain()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Text = "abcdefgh" };
            using var input = new Font(FontFamily.GenericSansSerif.Name, 18f);
            rich.BeginCapture();
            rich.EnqueueReply(Reply(18));
            rich.EnqueueReply(Reply(18));

            rich.Font = input;
            rich.Font = input;

            Assert.Equal(2, rich.Messages.Count);
            Assert.All(rich.Messages, AssertDefaultQuery);
        });

    [Fact]
    public void NullWithNativeTextResetsTheBaseBeforeSubmittingTheEffectiveInheritedFont()
        => OnSta(() =>
        {
            using var inherited = new Font(FontFamily.GenericSansSerif, 16f, FontStyle.Italic);
            using var explicitFont = new Font(FontFamily.GenericSansSerif, 11f, FontStyle.Bold);
            using var parent = new Control { Font = inherited };
            using var rich = new FormatRichTextBox { Font = explicitFont, Text = "abcdefgh" };
            parent.Controls.Add(rich);
            rich.BeginCapture();
            Font? seen = null;
            int messagesAtNotification = -1;
            rich.FontChanged += (_, _) =>
            {
                seen = rich.BaseFont;
                messagesAtNotification = rich.Messages.Count;
            };

            rich.Font = null!;

            FormatMessage message = Assert.Single(rich.Messages);
            AssertFontPayload(inherited, message);
            Assert.Same(inherited, seen);
            Assert.Same(inherited, rich.BaseFont);
            Assert.Equal(0, messagesAtNotification);
        });

    [Fact]
    public void NullWithNativeTextReadsTheVirtualFontAfterResettingTheBase()
        => OnSta(() =>
        {
            using var original = new Font(FontFamily.GenericSansSerif, 11f);
            using var virtualFont = new Font(FontFamily.GenericSansSerif, 23f, FontStyle.Underline);
            using var rich = new FormatRichTextBox { Font = original, Text = "abcdefgh" };
            rich.BeginCapture();
            rich.VirtualFont = virtualFont;

            rich.Font = null!;

            AssertFontPayload(virtualFont, Assert.Single(rich.Messages));
            Assert.Same(Control.DefaultFont, rich.BaseFont);
            Assert.Same(virtualFont, rich.Font);
            rich.VirtualFont = null;
        });

    [Fact]
    public void NullAssignmentPropagatesTheBaseFontChangedExceptionBeforeAnyCharacterFormat()
        => OnSta(() =>
        {
            using var original = new Font(FontFamily.GenericSansSerif, 11f);
            using var rich = new FormatRichTextBox { Font = original, Text = "abcdefgh" };
            rich.BeginCapture();
            var failure = new InvalidOperationException("base font notification failure");
            rich.FontChangedFailure = failure;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => rich.Font = null!));

            Assert.Empty(rich.Messages);
            Assert.Same(Control.DefaultFont, rich.BaseFont);
            rich.FontChangedFailure = null;
            rich.Font = null!;
            AssertFontPayload(Control.DefaultFont, Assert.Single(rich.Messages));
        });

    [Fact]
    public void NullAssignmentPropagatesTheEffectiveVirtualGetterExceptionAfterTheBaseReset()
        => OnSta(() =>
        {
            using var original = new Font(FontFamily.GenericSansSerif, 11f);
            using var rich = new FormatRichTextBox { Font = original, Text = "abcdefgh" };
            rich.BeginCapture();
            var failure = new InvalidOperationException("effective font getter failure");
            rich.AfterFontChanged = () => rich.FontGetterFailure = failure;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => rich.Font = null!));

            Assert.Empty(rich.Messages);
            Assert.Same(Control.DefaultFont, rich.BaseFont);
            rich.FontGetterFailure = null;
            rich.AfterFontChanged = null;
            rich.Font = null!;
            AssertFontPayload(Control.DefaultFont, Assert.Single(rich.Messages));
        });

    [Fact]
    public void ResolvedFontNotificationExceptionRetainsTheOriginalErrorAndAllowsANewNativeAssignment()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Text = "abcdefgh" };
            using var input = new Font(FontFamily.GenericSansSerif, 18f);
            rich.BeginCapture();
            rich.EnqueueReply(Reply(10));
            rich.EnqueueReply(Reply(20));
            var failure = new InvalidOperationException("resolved font notification failure");
            rich.FontChangedFailure = failure;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => rich.Font = input));

            AssertThreeMessageSequence(rich);
            Assert.Equal(20f, rich.BaseFont.SizeInPoints);
            rich.FontChangedFailure = null;
            rich.Messages.Clear();
            rich.EnqueueReply(Reply(20));
            rich.EnqueueReply(Reply(22));
            rich.Font = input;
            AssertThreeMessageSequence(rich);
            Assert.Equal(22f, rich.BaseFont.SizeInPoints);
        });

    [Fact]
    public void DisposingTheControlDoesNotConsumeTheBorrowedInputFont()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Text = "abcdefgh" };
            using var input = new Font(FontFamily.GenericSansSerif, 18f);
            rich.BeginCapture();
            rich.EnqueueReply(Reply(10));
            rich.EnqueueReply(Reply(20));
            rich.Font = input;

            rich.Dispose();

            AssertFontStillUsable(input);
        });

    private static void AssertThreeMessageSequence(FormatRichTextBox rich)
    {
        Assert.Equal(3, rich.Messages.Count);
        AssertDefaultQuery(rich.Messages[0]);
        Assert.Equal(EmSetCharFormat, rich.Messages[1].Id);
        Assert.Equal((nint)4, rich.Messages[1].Range);
        AssertDefaultQuery(rich.Messages[2]);
    }

    private static void AssertDefaultQuery(FormatMessage message)
    {
        Assert.Equal(EmGetCharFormat, message.Id);
        Assert.Equal((nint)0, message.Range);
        byte[] expected = new byte[116];
        BitConverter.GetBytes(116).CopyTo(expected, 0);
        Assert.Equal(expected, message.Bytes);
    }

    private static void AssertFontPayload(Font font, FormatMessage message)
    {
        Assert.Equal(EmSetCharFormat, message.Id);
        Assert.Equal((nint)4, message.Range);
        byte[] logical = ReadLogicalFont(font);
        byte[] expected = new byte[116];
        BitConverter.GetBytes(116).CopyTo(expected, 0);
        BitConverter.GetBytes(FontMask).CopyTo(expected, 4);
        BitConverter.GetBytes((uint)font.Style).CopyTo(expected, 8);
        BitConverter.GetBytes((int)(font.SizeInPoints * 20)).CopyTo(expected, 12);
        expected[24] = logical[23];
        expected[25] = logical[27];
        Array.Copy(logical, 28, expected, 26, 64);
        Assert.Equal(expected, message.Bytes);
    }

    private static byte[] Reply(float size, FontStyle style = FontStyle.Regular,
        uint mask = FontMask, int? height = null)
    {
        byte[] bytes = new byte[116];
        BitConverter.GetBytes(116).CopyTo(bytes, 0);
        BitConverter.GetBytes(mask).CopyTo(bytes, 4);
        BitConverter.GetBytes((uint)style).CopyTo(bytes, 8);
        BitConverter.GetBytes(height ?? (int)(size * 20)).CopyTo(bytes, 12);
        bytes[24] = 1;
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "RichTextBox Font contract test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private readonly record struct FormatMessage(int Id, nint Range, byte[] Bytes);

    private sealed class FormatRichTextBox : RichTextBox
    {
        private readonly Queue<byte[]> _replies = new();
        internal bool Capture { get; set; }
        internal bool FailCreation { get; set; }
        internal bool FailUnrelatedGetters { get; set; }
        internal string? VirtualText { get; set; }
        internal Font? VirtualFont { get; set; }
        internal Exception? FontGetterFailure { get; set; }
        internal Exception? FontChangedFailure { get; set; }
        internal Action? AfterFontChanged { get; set; }
        internal nint SetResult { get; set; } = 1;
        internal List<FormatMessage> Messages { get; } = [];
        internal int CreatedCount { get; private set; }
        internal Font BaseFont => base.Font;

        internal void BeginCapture()
        {
            _ = Handle;
            Capture = true;
            Messages.Clear();
        }

        internal void EnqueueReply(byte[] reply) => _replies.Enqueue(reply);

        public override Font Font
        {
            get
            {
                if (FontGetterFailure is not null) { throw FontGetterFailure; }
                return VirtualFont ?? base.Font;
            }
            set => base.Font = value;
        }

        public override string Text
        {
            get => FailUnrelatedGetters ? throw new InvalidOperationException("Font must not read Text.")
                : VirtualText ?? base.Text;
            set => base.Text = value;
        }

        public override int SelectionLength
        {
            get => FailUnrelatedGetters ? throw new InvalidOperationException("Font must not read SelectionLength.")
                : base.SelectionLength;
            set => base.SelectionLength = value;
        }

        public override string SelectedText
        {
            get => FailUnrelatedGetters ? throw new InvalidOperationException("Font must not read SelectedText.")
                : base.SelectedText;
            set => base.SelectedText = value;
        }

        protected override CreateParams CreateParams
            => FailCreation ? throw new InvalidOperationException("Font must not create the window.") : base.CreateParams;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            CreatedCount++;
        }

        protected override void OnFontChanged(EventArgs e)
        {
            if (FontChangedFailure is not null) { throw FontChangedFailure; }
            base.OnFontChanged(e);
            AfterFontChanged?.Invoke();
        }

        protected override void WndProc(ref Message message)
        {
            if (Capture && message.Msg is EmGetCharFormat or EmSetCharFormat)
            {
                byte[] bytes = new byte[116];
                Marshal.Copy(message.LParam, bytes, 0, bytes.Length);
                Messages.Add(new FormatMessage(message.Msg, message.WParam, bytes));
                if (message.Msg == EmGetCharFormat)
                {
                    byte[] reply = _replies.Count == 0 ? Reply(12) : _replies.Dequeue();
                    Marshal.Copy(reply, 0, message.LParam, reply.Length);
                    message.Result = 0;
                }
                else { message.Result = SetResult; }
                return;
            }
            base.WndProc(ref message);
        }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint handle, int message, nint wParam, nint lParam);

    [DllImport("gdi32.dll", ExactSpelling = true)]
    private static extern int GetObjectW(nint handle, int bytes, nint buffer);

    [DllImport("gdi32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DeleteObject(nint handle);
}
