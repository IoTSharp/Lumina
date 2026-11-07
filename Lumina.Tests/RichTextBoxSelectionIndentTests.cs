using System.ComponentModel;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class RichTextBoxSelectionIndentTests
{
    private const int EmGetParaFormat = 0x043D;
    private const int EmSetParaFormat = 0x0447;
    private const int EmGetLimitText = 0x00D5;
    private const int ParagraphSize = 156;

    [Theory]
    [InlineData(nameof(RichTextBox.SelectionIndent))]
    [InlineData(nameof(RichTextBox.SelectionHangingIndent))]
    [InlineData(nameof(RichTextBox.SelectionRightIndent))]
    public void PropertiesHaveTheStandardDeclaredContract(string name)
    {
        PropertyInfo property = typeof(RichTextBox).GetProperty(name,
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)!;

        Assert.NotNull(property);
        Assert.Equal(typeof(int), property.PropertyType);
        Assert.False(property.GetMethod!.IsVirtual);
        Assert.False(property.SetMethod!.IsVirtual);
        Assert.Equal(0, property.GetCustomAttribute<DefaultValueAttribute>()!.Value);
        Assert.False(property.GetCustomAttribute<BrowsableAttribute>()!.Browsable);
        Assert.Equal(DesignerSerializationVisibility.Hidden,
            property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>()!.Visibility);
    }

    [Theory]
    [InlineData(nameof(RichTextBox.SelectionIndent))]
    [InlineData(nameof(RichTextBox.SelectionHangingIndent))]
    [InlineData(nameof(RichTextBox.SelectionRightIndent))]
    public void GetterCreatesTheHandleAndReadsTheDefaultParagraph(string name)
        => OnSta(() =>
        {
            using var rich = new IndentRichTextBox();
            Assert.False(rich.IsHandleCreated);

            Assert.Equal(0, GetIndent(rich, name));

            Assert.True(rich.IsHandleCreated);
            Assert.Equal(1, rich.CreatedCount);
            Assert.Equal(0, ReadParagraph(rich.Handle).Value(name));
        });

    [Theory]
    [InlineData(nameof(RichTextBox.SelectionIndent), 48)]
    [InlineData(nameof(RichTextBox.SelectionHangingIndent), 16)]
    [InlineData(nameof(RichTextBox.SelectionRightIndent), 24)]
    public void SetterCreatesTheHandleAndChangesTheRealNativeParagraph(string name, int value)
        => OnSta(() =>
        {
            using var rich = new IndentRichTextBox { Text = "paragraph" };
            Assert.False(rich.IsHandleCreated);

            SetIndent(rich, name, value);

            Assert.True(rich.IsHandleCreated);
            Assert.Equal(1, rich.CreatedCount);
            Assert.Equal(PixelToTwip(value), ReadParagraph(rich.Handle).Value(name));
            Assert.Equal(TwipToPixel(PixelToTwip(value)), GetIndent(rich, name));
        });

    [Theory]
    [InlineData(-1)]
    [InlineData(-2000)]
    [InlineData(int.MinValue)]
    public void NegativeRightIndentIsRejectedBeforeHandleCreation(int value)
        => OnSta(() =>
        {
            using var rich = new IndentRichTextBox { Text = "cached", FailCreation = true };

            ArgumentOutOfRangeException error = Assert.Throws<ArgumentOutOfRangeException>(
                () => rich.SelectionRightIndent = value);

            Assert.Equal("value", error.ParamName);
            Assert.False(rich.IsHandleCreated);
            Assert.Equal(0, rich.CreatedCount);
            Assert.Equal("cached", rich.Text);
        });

    [Theory]
    [InlineData(nameof(RichTextBox.SelectionIndent), -1)]
    [InlineData(nameof(RichTextBox.SelectionIndent), int.MinValue)]
    [InlineData(nameof(RichTextBox.SelectionIndent), int.MaxValue)]
    [InlineData(nameof(RichTextBox.SelectionHangingIndent), -1)]
    [InlineData(nameof(RichTextBox.SelectionHangingIndent), int.MinValue)]
    [InlineData(nameof(RichTextBox.SelectionHangingIndent), int.MaxValue)]
    [InlineData(nameof(RichTextBox.SelectionRightIndent), int.MaxValue)]
    public void UnrestrictedValuesReachNativeWithTheFixedConversion(string name, int value)
        => OnSta(() =>
        {
            using var rich = new IndentRichTextBox();
            _ = rich.Handle;
            rich.InterceptParagraph = true;

            SetIndent(rich, name, value);

            ParagraphMessage call = Assert.Single(rich.ParagraphMessages);
            Assert.Equal(EmSetParaFormat, call.Message);
            Assert.Equal(PixelToTwip(value), BitConverter.ToInt32(call.Bytes, FieldOffset(name)));
        });

    [Theory]
    [InlineData(nameof(RichTextBox.SelectionIndent), 0)]
    [InlineData(nameof(RichTextBox.SelectionIndent), 37)]
    [InlineData(nameof(RichTextBox.SelectionHangingIndent), 0)]
    [InlineData(nameof(RichTextBox.SelectionHangingIndent), -13)]
    [InlineData(nameof(RichTextBox.SelectionRightIndent), 0)]
    [InlineData(nameof(RichTextBox.SelectionRightIndent), 29)]
    public void SetterSendsOnlyItsFieldInACompleteZeroInitializedStructure(string name, int value)
        => OnSta(() =>
        {
            using var rich = new IndentRichTextBox();
            _ = rich.Handle;
            rich.InterceptParagraph = true;

            SetIndent(rich, name, value);

            ParagraphMessage call = Assert.Single(rich.ParagraphMessages);
            Assert.Equal(EmSetParaFormat, call.Message);
            Assert.Equal((nint)0, call.WParam);
            byte[] expected = new byte[ParagraphSize];
            BitConverter.GetBytes(ParagraphSize).CopyTo(expected, 0);
            BitConverter.GetBytes(FieldMask(name)).CopyTo(expected, 4);
            BitConverter.GetBytes(PixelToTwip(value)).CopyTo(expected, FieldOffset(name));
            Assert.Equal(expected, call.Bytes);
        });

    [Theory]
    [InlineData(nameof(RichTextBox.SelectionIndent), 0, 451)]
    [InlineData(nameof(RichTextBox.SelectionIndent), 1, 451)]
    [InlineData(nameof(RichTextBox.SelectionIndent), 2, -451)]
    [InlineData(nameof(RichTextBox.SelectionHangingIndent), 0, -451)]
    [InlineData(nameof(RichTextBox.SelectionHangingIndent), 4, -451)]
    [InlineData(nameof(RichTextBox.SelectionHangingIndent), 1, 451)]
    [InlineData(nameof(RichTextBox.SelectionRightIndent), 0, 451)]
    [InlineData(nameof(RichTextBox.SelectionRightIndent), 2, -451)]
    [InlineData(nameof(RichTextBox.SelectionRightIndent), 4, 451)]
    public void GetterHonorsItsMaskAndTruncatesNativeTwips(string name, int mask, int twips)
        => OnSta(() =>
        {
            using var rich = new IndentRichTextBox();
            _ = rich.Handle;
            rich.InterceptParagraph = true;
            rich.ReturnedMask = mask;
            rich.ReturnedTwips = twips;

            int expected = (mask & FieldMask(name)) == 0 ? 0 : TwipToPixel(twips);
            Assert.Equal(expected, GetIndent(rich, name));

            ParagraphMessage call = Assert.Single(rich.ParagraphMessages);
            Assert.Equal(EmGetParaFormat, call.Message);
            Assert.Equal((nint)0, call.WParam);
            byte[] expectedBytes = new byte[ParagraphSize];
            BitConverter.GetBytes(ParagraphSize).CopyTo(expectedBytes, 0);
            Assert.Equal(expectedBytes, call.Bytes);
        });

    [Theory]
    [InlineData(nameof(RichTextBox.SelectionIndent))]
    [InlineData(nameof(RichTextBox.SelectionHangingIndent))]
    [InlineData(nameof(RichTextBox.SelectionRightIndent))]
    public void RepeatedAssignmentsStillSubmitTheNativeParagraph(string name)
        => OnSta(() =>
        {
            using var rich = new IndentRichTextBox();
            _ = rich.Handle;
            rich.InterceptParagraph = true;

            SetIndent(rich, name, 24);
            SetIndent(rich, name, 24);

            Assert.Equal(2, rich.ParagraphMessages.Count);
            Assert.All(rich.ParagraphMessages, call => Assert.Equal(EmSetParaFormat, call.Message));
            Assert.Equal(rich.ParagraphMessages[0].Bytes, rich.ParagraphMessages[1].Bytes);
        });

    [Theory]
    [InlineData(nameof(RichTextBox.SelectionIndent))]
    [InlineData(nameof(RichTextBox.SelectionHangingIndent))]
    [InlineData(nameof(RichTextBox.SelectionRightIndent))]
    public void SelectedParagraphChangesWithoutChangingTheFollowingParagraph(string name)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new IndentRichTextBox { Text = "first\nsecond\nthird" };
            CreateHostedHandle(form, rich);
            rich.Select(1, 3);

            SetIndent(rich, name, 24);

            Assert.Equal(PixelToTwip(24), ReadParagraph(rich.Handle).Value(name));
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            rich.Select(6, 6);
            Assert.Equal(0, ReadParagraph(rich.Handle).Value(name));
            Assert.Equal("first\nsecond\nthird", rich.Text);
        });

    [Theory]
    [InlineData(nameof(RichTextBox.SelectionIndent))]
    [InlineData(nameof(RichTextBox.SelectionHangingIndent))]
    [InlineData(nameof(RichTextBox.SelectionRightIndent))]
    public void EmptySelectionFormatsItsContainingParagraph(string name)
        => OnSta(() =>
        {
            using var rich = new IndentRichTextBox { Text = "first\nsecond" };
            _ = rich.Handle;
            rich.Select(2, 0);

            SetIndent(rich, name, 24);

            Assert.Equal(2, rich.SelectionStart);
            Assert.Equal(0, rich.SelectionLength);
            rich.Select(0, 5);
            Assert.Equal(PixelToTwip(24), ReadParagraph(rich.Handle).Value(name));
            rich.Select(6, 6);
            Assert.Equal(0, ReadParagraph(rich.Handle).Value(name));
        });

    [Theory]
    [InlineData(nameof(RichTextBox.SelectionIndent))]
    [InlineData(nameof(RichTextBox.SelectionHangingIndent))]
    [InlineData(nameof(RichTextBox.SelectionRightIndent))]
    public void MixedParagraphsUseTheNativeMaskAndReturnZero(string name)
        => OnSta(() =>
        {
            using var rich = new IndentRichTextBox { Text = "first\nsecond" };
            _ = rich.Handle;
            rich.Select(0, 5);
            SetIndent(rich, name, 24);
            rich.Select(6, 6);
            SetIndent(rich, name, 48);
            rich.SelectAll();

            Assert.Equal(0, ReadParagraph(rich.Handle).Mask & FieldMask(name));
            Assert.Equal(0, GetIndent(rich, name));
        });

    [Theory]
    [InlineData(nameof(RichTextBox.SelectionIndent))]
    [InlineData(nameof(RichTextBox.SelectionHangingIndent))]
    [InlineData(nameof(RichTextBox.SelectionRightIndent))]
    public void RepeatingTheValueAfterMovingSelectionFormatsBothParagraphs(string name)
        => OnSta(() =>
        {
            using var rich = new IndentRichTextBox { Text = "first\nsecond" };
            _ = rich.Handle;
            rich.Select(0, 5);
            SetIndent(rich, name, 24);
            rich.Select(6, 6);
            SetIndent(rich, name, 24);
            rich.SelectAll();

            ParagraphState state = ReadParagraph(rich.Handle);
            Assert.NotEqual(0, state.Mask & FieldMask(name));
            Assert.Equal(PixelToTwip(24), state.Value(name));
            Assert.Equal(TwipToPixel(PixelToTwip(24)), GetIndent(rich, name));
        });

    [Theory]
    [InlineData(nameof(RichTextBox.SelectionIndent))]
    [InlineData(nameof(RichTextBox.SelectionHangingIndent))]
    [InlineData(nameof(RichTextBox.SelectionRightIndent))]
    public void GetterReadsIndependentNativeChangesWithoutACache(string name)
        => OnSta(() =>
        {
            using var rich = new IndentRichTextBox { Text = "native paragraph" };
            _ = rich.Handle;
            rich.SelectAll();
            SetIndent(rich, name, 24);

            SetNativeIndent(rich.Handle, name, 451);
            Assert.Equal(TwipToPixel(451), GetIndent(rich, name));
            SetNativeIndent(rich.Handle, name, 907);
            Assert.Equal(TwipToPixel(907), GetIndent(rich, name));
        });

    [Theory]
    [InlineData(nameof(RichTextBox.SelectionIndent))]
    [InlineData(nameof(RichTextBox.SelectionHangingIndent))]
    [InlineData(nameof(RichTextBox.SelectionRightIndent))]
    public void GetterPreservesTheDocumentSelectionUndoLimitAndNotifications(string name)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new IndentRichTextBox { Text = "first\nsecond", MaxLength = 3 };
            CreateHostedHandle(form, rich);
            rich.Select(1, 3);
            SetNativeIndent(rich.Handle, name, 451);
            rich.Modified = true;
            bool canUndo = rich.CanUndo;
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;

            Assert.Equal(TwipToPixel(451), GetIndent(rich, name));
            Assert.Equal(TwipToPixel(451), GetIndent(rich, name));

            Assert.Equal("first\nsecond", rich.Text);
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.True(rich.Modified);
            Assert.Equal(canUndo, rich.CanUndo);
            Assert.Equal((nint)3, SendMessageW(rich.Handle, EmGetLimitText, 0, 0));
            Assert.Equal(0, changes);
        });

    [Theory]
    [InlineData(nameof(RichTextBox.SelectionIndent))]
    [InlineData(nameof(RichTextBox.SelectionHangingIndent))]
    [InlineData(nameof(RichTextBox.SelectionRightIndent))]
    public void AssignmentPreservesAllOtherIndentAlignmentTabAndCharacterFormatting(string name)
        => OnSta(() =>
        {
            using var rich = new IndentRichTextBox();
            rich.Rtf = @"{\rtf1\ansi\pard\qc\li720\ri360\fi-120\tx300\b styled paragraph}";
            rich.SelectAll();
            ParagraphState before = ReadParagraph(rich.Handle);
            Assert.True(before.TabCount > 0);
            int characterEffects = ReadCharacterEffects(rich.Handle);
            Assert.NotEqual(0, characterEffects & 1);

            SetIndent(rich, name, 48);

            ParagraphState after = ReadParagraph(rich.Handle);
            Assert.Equal(PixelToTwip(48), after.Value(name));
            if (name != nameof(RichTextBox.SelectionIndent)) { Assert.Equal(before.StartIndent, after.StartIndent); }
            if (name != nameof(RichTextBox.SelectionRightIndent)) { Assert.Equal(before.RightIndent, after.RightIndent); }
            if (name != nameof(RichTextBox.SelectionHangingIndent)) { Assert.Equal(before.Offset, after.Offset); }
            Assert.Equal(before.Alignment, after.Alignment);
            Assert.Equal(before.TabCount, after.TabCount);
            Assert.Equal(before.FirstTab, after.FirstTab);
            Assert.Equal(characterEffects, ReadCharacterEffects(rich.Handle));
            Assert.Equal("styled paragraph", rich.Text);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void AllThreeIndentPropertiesSurviveRtfHandleRecreationWithDocumentState(bool modified)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new IndentRichTextBox { Text = "first\nsecond", MaxLength = 3 };
            CreateHostedHandle(form, rich);
            rich.Select(1, 3);
            rich.SelectionIndent = 48;
            rich.SelectionHangingIndent = 16;
            rich.SelectionRightIndent = 24;
            rich.SelectionAlignment = HorizontalAlignment.Center;
            ParagraphState before = ReadParagraph(rich.Handle);
            rich.Modified = modified;
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;

            rich.RecreateNow();

            ParagraphState after = ReadParagraph(rich.Handle);
            Assert.Equal(2, rich.CreatedCount);
            Assert.Equal(before.StartIndent, after.StartIndent);
            Assert.Equal(before.RightIndent, after.RightIndent);
            Assert.Equal(before.Offset, after.Offset);
            Assert.Equal(before.Alignment, after.Alignment);
            Assert.Equal(TwipToPixel(before.StartIndent), rich.SelectionIndent);
            Assert.Equal(TwipToPixel(before.Offset), rich.SelectionHangingIndent);
            Assert.Equal(TwipToPixel(before.RightIndent), rich.SelectionRightIndent);
            Assert.Equal("first\nsecond", rich.Text);
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.Equal(modified, rich.Modified);
            Assert.Equal((nint)3, SendMessageW(rich.Handle, EmGetLimitText, 0, 0));
            Assert.Equal(0, changes);
        });

    [Fact]
    public void InvalidRightIndentOnAnExistingHandleLeavesStateUnchanged()
        => OnSta(() =>
        {
            using var rich = new IndentRichTextBox { Text = "paragraph" };
            _ = rich.Handle;
            rich.Select(1, 3);
            rich.SelectionRightIndent = 24;
            ParagraphState before = ReadParagraph(rich.Handle);

            ArgumentOutOfRangeException error = Assert.Throws<ArgumentOutOfRangeException>(
                () => rich.SelectionRightIndent = -1);

            Assert.Equal("value", error.ParamName);
            Assert.Equal(before, ReadParagraph(rich.Handle));
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.Equal("paragraph", rich.Text);
        });

    [Theory]
    [InlineData(nameof(RichTextBox.SelectionIndent), false)]
    [InlineData(nameof(RichTextBox.SelectionIndent), true)]
    [InlineData(nameof(RichTextBox.SelectionHangingIndent), false)]
    [InlineData(nameof(RichTextBox.SelectionHangingIndent), true)]
    [InlineData(nameof(RichTextBox.SelectionRightIndent), false)]
    [InlineData(nameof(RichTextBox.SelectionRightIndent), true)]
    public void OriginalCreationFailurePropagatesAndTheSameInstanceCanRetry(string name, bool setter)
        => OnSta(() =>
        {
            using var rich = new IndentRichTextBox { FailCreation = true };
            Exception error = Assert.Throws<InvalidOperationException>(() =>
            {
                if (setter) { SetIndent(rich, name, 24); }
                else { _ = GetIndent(rich, name); }
            });
            Assert.Same(rich.CreationFailure, error);
            Assert.False(rich.IsHandleCreated);

            rich.FailCreation = false;
            if (setter) { SetIndent(rich, name, 24); }
            else { _ = GetIndent(rich, name); }

            Assert.Equal(1, rich.CreatedCount);
            Assert.Equal(setter ? TwipToPixel(PixelToTwip(24)) : 0, GetIndent(rich, name));
        });

    private static int GetIndent(RichTextBox rich, string name) => name switch
    {
        nameof(RichTextBox.SelectionIndent) => rich.SelectionIndent,
        nameof(RichTextBox.SelectionHangingIndent) => rich.SelectionHangingIndent,
        nameof(RichTextBox.SelectionRightIndent) => rich.SelectionRightIndent,
        _ => throw new ArgumentOutOfRangeException(nameof(name))
    };

    private static void SetIndent(RichTextBox rich, string name, int value)
    {
        switch (name)
        {
            case nameof(RichTextBox.SelectionIndent): rich.SelectionIndent = value; break;
            case nameof(RichTextBox.SelectionHangingIndent): rich.SelectionHangingIndent = value; break;
            case nameof(RichTextBox.SelectionRightIndent): rich.SelectionRightIndent = value; break;
            default: throw new ArgumentOutOfRangeException(nameof(name));
        }
    }

    private static int FieldMask(string name) => name switch
    {
        nameof(RichTextBox.SelectionIndent) => 1,
        nameof(RichTextBox.SelectionHangingIndent) => 4,
        nameof(RichTextBox.SelectionRightIndent) => 2,
        _ => throw new ArgumentOutOfRangeException(nameof(name))
    };

    private static int FieldOffset(string name) => name switch
    {
        nameof(RichTextBox.SelectionIndent) => 12,
        nameof(RichTextBox.SelectionHangingIndent) => 20,
        nameof(RichTextBox.SelectionRightIndent) => 16,
        _ => throw new ArgumentOutOfRangeException(nameof(name))
    };

    private static int PixelToTwip(int value)
        => unchecked((int)((value / (double)GetScreenDpiX()) * 72.0 * 20.0));

    private static int TwipToPixel(int value)
        => unchecked((int)(((value / 20.0) / 72.0) * GetScreenDpiX()));

    private static int GetScreenDpiX()
    {
        nint dc = GetDC(0);
        Assert.NotEqual((nint)0, dc);
        try
        {
            int dpi = GetDeviceCaps(dc, 88);
            Assert.True(dpi > 0);
            return dpi;
        }
        finally { _ = ReleaseDC(0, dc); }
    }

    private static void CreateHostedHandle(Form form, RichTextBox rich)
    {
        form.Controls.Add(rich);
        _ = form.Handle;
        _ = rich.Handle;
    }

    private static ParagraphState ReadParagraph(nint handle)
    {
        nint paragraph = Marshal.AllocHGlobal(ParagraphSize);
        try
        {
            Marshal.Copy(new byte[ParagraphSize], 0, paragraph, ParagraphSize);
            Marshal.WriteInt32(paragraph, ParagraphSize);
            _ = SendMessageW(handle, EmGetParaFormat, 0, paragraph);
            return new ParagraphState(Marshal.ReadInt32(paragraph, 4), Marshal.ReadInt32(paragraph, 12),
                Marshal.ReadInt32(paragraph, 16), Marshal.ReadInt32(paragraph, 20),
                unchecked((ushort)Marshal.ReadInt16(paragraph, 24)), Marshal.ReadInt16(paragraph, 26),
                Marshal.ReadInt32(paragraph, 28));
        }
        finally { Marshal.FreeHGlobal(paragraph); }
    }

    private static void SetNativeIndent(nint handle, string name, int twips)
    {
        nint paragraph = Marshal.AllocHGlobal(ParagraphSize);
        try
        {
            Marshal.Copy(new byte[ParagraphSize], 0, paragraph, ParagraphSize);
            Marshal.WriteInt32(paragraph, ParagraphSize);
            Marshal.WriteInt32(paragraph, 4, FieldMask(name));
            Marshal.WriteInt32(paragraph, FieldOffset(name), twips);
            Assert.NotEqual((nint)0, SendMessageW(handle, EmSetParaFormat, 0, paragraph));
        }
        finally { Marshal.FreeHGlobal(paragraph); }
    }

    private static int ReadCharacterEffects(nint handle)
    {
        nint characterFormat = Marshal.AllocHGlobal(116);
        try
        {
            Marshal.Copy(new byte[116], 0, characterFormat, 116);
            Marshal.WriteInt32(characterFormat, 116);
            _ = SendMessageW(handle, 0x043A, 1, characterFormat);
            return Marshal.ReadInt32(characterFormat, 8);
        }
        finally { Marshal.FreeHGlobal(characterFormat); }
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "RichTextBox paragraph indent test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private readonly record struct ParagraphState(int Mask, int StartIndent, int RightIndent,
        int Offset, ushort Alignment, short TabCount, int FirstTab)
    {
        internal int Value(string name) => name switch
        {
            nameof(RichTextBox.SelectionIndent) => StartIndent,
            nameof(RichTextBox.SelectionHangingIndent) => Offset,
            nameof(RichTextBox.SelectionRightIndent) => RightIndent,
            _ => throw new ArgumentOutOfRangeException(nameof(name))
        };
    }

    private readonly record struct ParagraphMessage(int Message, nint WParam, byte[] Bytes);

    private sealed class IndentRichTextBox : RichTextBox
    {
        internal bool InterceptParagraph { get; set; }
        internal int ReturnedMask { get; set; }
        internal int ReturnedTwips { get; set; }
        internal List<ParagraphMessage> ParagraphMessages { get; } = [];
        internal int CreatedCount { get; private set; }
        internal bool FailCreation { get; set; }
        internal InvalidOperationException CreationFailure { get; } = new("Selection indent creation failure");

        internal void RecreateNow() => RecreateHandle();

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
            if (InterceptParagraph && message.Msg is EmGetParaFormat or EmSetParaFormat)
            {
                byte[] bytes = new byte[ParagraphSize];
                Marshal.Copy(message.LParam, bytes, 0, ParagraphSize);
                ParagraphMessages.Add(new ParagraphMessage(message.Msg, message.WParam, bytes));
                if (message.Msg == EmGetParaFormat)
                {
                    Marshal.WriteInt32(message.LParam, 4, ReturnedMask);
                    Marshal.WriteInt32(message.LParam, 12, ReturnedTwips);
                    Marshal.WriteInt32(message.LParam, 16, ReturnedTwips);
                    Marshal.WriteInt32(message.LParam, 20, ReturnedTwips);
                }
                // Accessors use the filled native structure and setters ignore
                // the return value, including a zero return from RichEdit.
                message.Result = 0;
                return;
            }
            base.WndProc(ref message);
        }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint handle, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetDC(nint handle);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern int ReleaseDC(nint handle, nint dc);

    [DllImport("gdi32.dll", ExactSpelling = true)]
    private static extern int GetDeviceCaps(nint dc, int index);
}
