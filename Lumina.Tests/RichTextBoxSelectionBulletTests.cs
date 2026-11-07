using System.ComponentModel;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class RichTextBoxSelectionBulletTests
{
    private const int EmGetParaFormat = 0x043D;
    private const int EmSetParaFormat = 0x0447;
    private const int ParagraphSize = 156;

    [Fact]
    public void SelectionBulletHasTheStandardDeclaredContract()
    {
        PropertyInfo property = DeclaredProperty(nameof(RichTextBox.SelectionBullet));
        Assert.Equal(typeof(bool), property.PropertyType);
        Assert.False(property.GetMethod!.IsVirtual);
        Assert.False(property.SetMethod!.IsVirtual);
        Assert.Equal(false, property.GetCustomAttribute<DefaultValueAttribute>()!.Value);
        Assert.False(property.GetCustomAttribute<BrowsableAttribute>()!.Browsable);
        Assert.Equal(DesignerSerializationVisibility.Hidden,
            property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>()!.Visibility);
    }

    [Fact]
    public void BulletIndentHasTheStandardDeclaredContract()
    {
        PropertyInfo property = DeclaredProperty(nameof(RichTextBox.BulletIndent));
        Assert.Equal(typeof(int), property.PropertyType);
        Assert.False(property.GetMethod!.IsVirtual);
        Assert.False(property.SetMethod!.IsVirtual);
        Assert.Equal(0, property.GetCustomAttribute<DefaultValueAttribute>()!.Value);
        Assert.Equal("Behavior", property.GetCustomAttribute<CategoryAttribute>()!.Category);
        Assert.True(property.GetCustomAttribute<LocalizableAttribute>()!.IsLocalizable);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(37)]
    [InlineData(int.MaxValue)]
    public void BulletIndentStoresItsValueWithoutCreatingAHandle(int value)
        => OnSta(() =>
        {
            using var rich = new BulletRichTextBox { FailCreation = true };
            Assert.Equal(0, rich.BulletIndent);

            rich.BulletIndent = value;
            rich.BulletIndent = value;

            Assert.Equal(value, rich.BulletIndent);
            Assert.False(rich.IsHandleCreated);
            Assert.Empty(rich.ParagraphMessages);
        });

    [Theory]
    [InlineData(-1)]
    [InlineData(-2000)]
    [InlineData(int.MinValue)]
    public void NegativeIndentIsRejectedBeforeAnyHandleOrCacheChange(int value)
        => OnSta(() =>
        {
            using var rich = new BulletRichTextBox { BulletIndent = 37, FailCreation = true };

            ArgumentOutOfRangeException error = Assert.Throws<ArgumentOutOfRangeException>(
                () => rich.BulletIndent = value);

            Assert.Equal("value", error.ParamName);
            Assert.Equal(value, error.ActualValue);
            Assert.Equal(37, rich.BulletIndent);
            Assert.False(rich.IsHandleCreated);
            Assert.Empty(rich.ParagraphMessages);
        });

    [Fact]
    public void InvalidIndentOnAnExistingHandleDoesNotQueryOrChangeTheParagraph()
        => OnSta(() =>
        {
            using var rich = new BulletRichTextBox { BulletIndent = 24 };
            _ = rich.Handle;
            rich.InterceptParagraph = true;
            rich.ReturnedMask = 0x20;
            rich.ReturnedNumbering = 1;

            Assert.Throws<ArgumentOutOfRangeException>(() => rich.BulletIndent = -1);

            Assert.Equal(24, rich.BulletIndent);
            Assert.Empty(rich.ParagraphMessages);
        });

    [Fact]
    public void BulletGetterCreatesTheHandleAndReadsTheNativeDefault()
        => OnSta(() =>
        {
            using var rich = new BulletRichTextBox();
            Assert.False(rich.IsHandleCreated);

            Assert.False(rich.SelectionBullet);

            Assert.True(rich.IsHandleCreated);
            Assert.Equal(1, rich.CreatedCount);
            Assert.Equal(0, ReadParagraph(rich.Handle).Numbering);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void BulletSetterCreatesTheHandleAndWritesTheNativeParagraph(bool value)
        => OnSta(() =>
        {
            using var rich = new BulletRichTextBox { Text = "paragraph", BulletIndent = 24 };
            Assert.False(rich.IsHandleCreated);

            rich.SelectionBullet = value;

            Assert.Equal(1, rich.CreatedCount);
            ParagraphState state = ReadParagraph(rich.Handle);
            Assert.Equal(value ? 1 : 0, state.Numbering);
            Assert.Equal(value ? PixelToTwip(24) : 0, state.Offset);
            Assert.Equal(value, rich.SelectionBullet);
        });

    [Theory]
    [InlineData(false, 0)]
    [InlineData(false, 37)]
    [InlineData(true, 0)]
    [InlineData(true, 37)]
    [InlineData(true, int.MaxValue)]
    public void BulletSetterSendsOnlyNumberingAndOffsetInAZeroInitializedPayload(bool value, int indent)
        => OnSta(() =>
        {
            using var rich = new BulletRichTextBox { BulletIndent = indent };
            _ = rich.Handle;
            rich.InterceptParagraph = true;

            rich.SelectionBullet = value;

            ParagraphMessage call = Assert.Single(rich.ParagraphMessages);
            Assert.Equal(EmSetParaFormat, call.Message);
            Assert.Equal((nint)0, call.WParam);
            byte[] expected = EmptyParagraph();
            BitConverter.GetBytes(0x24).CopyTo(expected, 4);
            BitConverter.GetBytes((ushort)(value ? 1 : 0)).CopyTo(expected, 8);
            BitConverter.GetBytes(value ? PixelToTwip(indent) : 0).CopyTo(expected, 20);
            Assert.Equal(expected, call.Bytes);
        });

    [Theory]
    [InlineData(0, 1, false)]
    [InlineData(4, 1, false)]
    [InlineData(0x20, 0, false)]
    [InlineData(0x20, 1, true)]
    [InlineData(0x20, 2, false)]
    [InlineData(0x24, 1, true)]
    [InlineData(0x20, ushort.MaxValue, false)]
    public void BulletGetterRequiresTheNumberingMaskAndBulletValue(int mask, ushort numbering, bool expected)
        => OnSta(() =>
        {
            using var rich = new BulletRichTextBox();
            _ = rich.Handle;
            rich.InterceptParagraph = true;
            rich.ReturnedMask = mask;
            rich.ReturnedNumbering = numbering;

            Assert.Equal(expected, rich.SelectionBullet);

            ParagraphMessage call = Assert.Single(rich.ParagraphMessages);
            Assert.Equal(EmGetParaFormat, call.Message);
            Assert.Equal((nint)0, call.WParam);
            Assert.Equal(EmptyParagraph(), call.Bytes);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void RepeatedBulletAssignmentsEachSubmitWithoutAnInitialGet(bool value)
        => OnSta(() =>
        {
            using var rich = new BulletRichTextBox { BulletIndent = 24 };
            _ = rich.Handle;
            rich.InterceptParagraph = true;

            rich.SelectionBullet = value;
            rich.SelectionBullet = value;

            Assert.Equal(2, rich.ParagraphMessages.Count);
            Assert.All(rich.ParagraphMessages, call => Assert.Equal(EmSetParaFormat, call.Message));
            Assert.Equal(rich.ParagraphMessages[0].Bytes, rich.ParagraphMessages[1].Bytes);
        });

    [Theory]
    [InlineData(0)]
    [InlineData(37)]
    [InlineData(int.MaxValue)]
    public void IndentAssignmentCachesBeforeQueryAndResubmitsWhenBulletIsSet(int indent)
        => OnSta(() =>
        {
            using var rich = new BulletRichTextBox();
            _ = rich.Handle;
            rich.InterceptParagraph = true;
            rich.ReturnedMask = 0x20;
            rich.ReturnedNumbering = 1;

            rich.BulletIndent = indent;
            rich.BulletIndent = indent;

            Assert.Equal(indent, rich.IndentObservedByGet);
            Assert.Equal(indent, rich.BulletIndent);
            Assert.Equal(new[] { EmGetParaFormat, EmSetParaFormat, EmGetParaFormat, EmSetParaFormat },
                rich.ParagraphMessages.Select(call => call.Message));
            Assert.Equal(PixelToTwip(indent), BitConverter.ToInt32(rich.ParagraphMessages[1].Bytes, 20));
            Assert.Equal(rich.ParagraphMessages[1].Bytes, rich.ParagraphMessages[3].Bytes);
        });

    [Theory]
    [InlineData(0, 1)]
    [InlineData(0x20, 0)]
    [InlineData(0x20, 2)]
    public void IndentAssignmentOnlyQueriesWhenCurrentSelectionIsNotUniformlyBulleted(int mask, ushort numbering)
        => OnSta(() =>
        {
            using var rich = new BulletRichTextBox();
            _ = rich.Handle;
            rich.InterceptParagraph = true;
            rich.ReturnedMask = mask;
            rich.ReturnedNumbering = numbering;

            rich.BulletIndent = 37;

            Assert.Equal(37, rich.BulletIndent);
            Assert.Equal(37, rich.IndentObservedByGet);
            Assert.Equal(EmGetParaFormat, Assert.Single(rich.ParagraphMessages).Message);
        });

    [Theory]
    [InlineData("get")]
    [InlineData("setFalse")]
    [InlineData("setTrue")]
    public void OriginalCreationFailurePropagatesAndTheInstanceCanRetry(string operation)
        => OnSta(() =>
        {
            using var rich = new BulletRichTextBox { FailCreation = true, BulletIndent = 24 };
            Exception error = Assert.Throws<InvalidOperationException>(() => InvokeBullet(rich, operation));
            Assert.Same(rich.CreationFailure, error);
            Assert.False(rich.IsHandleCreated);
            Assert.Equal(24, rich.BulletIndent);

            rich.FailCreation = false;
            InvokeBullet(rich, operation);

            Assert.Equal(1, rich.CreatedCount);
            Assert.Equal(operation == "setTrue", rich.SelectionBullet);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void SelectedParagraphChangesWithoutChangingTheFollowingParagraph(bool value)
        => OnSta(() =>
        {
            using var rich = new BulletRichTextBox { Text = "first\nsecond", BulletIndent = 24 };
            _ = rich.Handle;
            rich.Select(1, 3);
            SetNativeBullet(rich.Handle, true, 540);

            rich.SelectionBullet = value;

            ParagraphState selected = ReadParagraph(rich.Handle);
            Assert.Equal(value ? 1 : 0, selected.Numbering);
            Assert.Equal(value ? PixelToTwip(24) : 0, selected.Offset);
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            rich.Select(6, 6);
            Assert.Equal(0, ReadParagraph(rich.Handle).Numbering);
            Assert.Equal("first\nsecond", rich.Text);
        });

    [Fact]
    public void EmptySelectionFormatsTheContainingParagraph()
        => OnSta(() =>
        {
            using var rich = new BulletRichTextBox { Text = "first\nsecond", BulletIndent = 24 };
            _ = rich.Handle;
            rich.Select(2, 0);

            rich.SelectionBullet = true;

            Assert.Equal(2, rich.SelectionStart);
            Assert.Equal(0, rich.SelectionLength);
            rich.Select(0, 5);
            Assert.True(rich.SelectionBullet);
            Assert.Equal(PixelToTwip(24), ReadParagraph(rich.Handle).Offset);
            rich.Select(6, 6);
            Assert.False(rich.SelectionBullet);
        });

    [Fact]
    public void MixedParagraphsReturnFalseWithoutChangingEitherParagraph()
        => OnSta(() =>
        {
            using var rich = new BulletRichTextBox { Text = "first\nsecond" };
            _ = rich.Handle;
            rich.Select(0, 5);
            rich.SelectionBullet = true;
            rich.SelectAll();

            Assert.Equal(0, ReadParagraph(rich.Handle).Mask & 0x20);
            Assert.False(rich.SelectionBullet);

            rich.Select(0, 5);
            Assert.True(rich.SelectionBullet);
            rich.Select(6, 6);
            Assert.False(rich.SelectionBullet);
        });

    [Fact]
    public void RepeatingTheValueAfterMovingSelectionFormatsBothParagraphs()
        => OnSta(() =>
        {
            using var rich = new BulletRichTextBox { Text = "first\nsecond", BulletIndent = 24 };
            _ = rich.Handle;
            rich.Select(0, 5);
            rich.SelectionBullet = true;
            rich.Select(6, 6);

            rich.SelectionBullet = true;
            rich.SelectAll();

            Assert.True(rich.SelectionBullet);
            Assert.NotEqual(0, ReadParagraph(rich.Handle).Mask & 0x20);
            Assert.Equal(PixelToTwip(24), ReadParagraph(rich.Handle).Offset);
        });

    [Fact]
    public void GetterReadsIndependentNativeChangesWithoutCachingBulletOrOffset()
        => OnSta(() =>
        {
            using var rich = new BulletRichTextBox { Text = "native", BulletIndent = 24 };
            _ = rich.Handle;

            SetNativeBullet(rich.Handle, true, 451);
            Assert.True(rich.SelectionBullet);
            Assert.Equal(24, rich.BulletIndent);
            Assert.Equal(451, ReadParagraph(rich.Handle).Offset);
            SetNativeBullet(rich.Handle, false, 0);
            Assert.False(rich.SelectionBullet);
            Assert.Equal(24, rich.BulletIndent);
        });

    [Fact]
    public void IndentUpdatesOnlyTheCurrentlyBulletedParagraphAndRepeatedValueReappliesIt()
        => OnSta(() =>
        {
            using var rich = new BulletRichTextBox { Text = "first\nsecond", BulletIndent = 24 };
            _ = rich.Handle;
            rich.Select(0, 5);
            rich.SelectionBullet = true;
            SetNativeBullet(rich.Handle, true, 451);

            rich.BulletIndent = 24;

            Assert.True(rich.SelectionBullet);
            Assert.Equal(PixelToTwip(24), ReadParagraph(rich.Handle).Offset);
            rich.Select(6, 6);
            rich.BulletIndent = 48;
            Assert.False(rich.SelectionBullet);
            Assert.Equal(0, ReadParagraph(rich.Handle).Offset);
            rich.Select(0, 5);
            Assert.Equal(PixelToTwip(24), ReadParagraph(rich.Handle).Offset);
            Assert.Equal(48, rich.BulletIndent);
        });

    [Fact]
    public void MixedSelectionIndentAssignmentKeepsEachNativeParagraphUnchanged()
        => OnSta(() =>
        {
            using var rich = new BulletRichTextBox { Text = "first\nsecond" };
            _ = rich.Handle;
            rich.Select(0, 5);
            SetNativeBullet(rich.Handle, true, 451);
            rich.SelectAll();

            rich.BulletIndent = 48;

            Assert.False(rich.SelectionBullet);
            Assert.Equal(48, rich.BulletIndent);
            rich.Select(0, 5);
            Assert.Equal(1, ReadParagraph(rich.Handle).Numbering);
            Assert.Equal(451, ReadParagraph(rich.Handle).Offset);
            rich.Select(6, 6);
            Assert.Equal(0, ReadParagraph(rich.Handle).Numbering);
            Assert.Equal(0, ReadParagraph(rich.Handle).Offset);
        });

    [Fact]
    public void GetterPreservesDocumentSelectionUndoLimitModifiedAndNotifications()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new BulletRichTextBox { Text = "first\nsecond", MaxLength = 3 };
            CreateHostedHandle(form, rich);
            rich.Select(1, 3);
            SetNativeBullet(rich.Handle, true, 451);
            rich.Modified = true;
            bool undo = rich.CanUndo;
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;

            Assert.True(rich.SelectionBullet);
            Assert.True(rich.SelectionBullet);
            Assert.Equal(0, rich.BulletIndent);

            Assert.Equal("first\nsecond", rich.Text);
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.True(rich.Modified);
            Assert.Equal(undo, rich.CanUndo);
            Assert.Equal((nint)3, SendMessageW(rich.Handle, 0x00D5, 0, 0));
            Assert.Equal(0, changes);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void AssignmentChangesNumberingAndHangingOffsetWhilePreservingOtherFormatting(bool value)
        => OnSta(() =>
        {
            using var rich = new BulletRichTextBox { BulletIndent = 24 };
            rich.Rtf = @"{\rtf1\ansi\pard\qc\li720\ri360\fi-120\tx300\b styled paragraph}";
            rich.SelectAll();
            ParagraphState before = ReadParagraph(rich.Handle);
            int effects = ReadCharacterEffects(rich.Handle);
            Assert.True(before.TabCount > 0);
            Assert.NotEqual(0, effects & 1);

            rich.SelectionBullet = value;

            ParagraphState after = ReadParagraph(rich.Handle);
            Assert.Equal(value ? 1 : 0, after.Numbering);
            Assert.Equal(value ? PixelToTwip(24) : 0, after.Offset);
            Assert.Equal(before.StartIndent, after.StartIndent);
            Assert.Equal(before.RightIndent, after.RightIndent);
            Assert.Equal(before.Alignment, after.Alignment);
            Assert.Equal(before.TabCount, after.TabCount);
            Assert.Equal(before.FirstTab, after.FirstTab);
            Assert.Equal(effects, ReadCharacterEffects(rich.Handle));
            Assert.Equal("styled paragraph", rich.Text);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void BulletAndCachedIndentSurviveRtfRecreationWithTheDocumentState(bool modified)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new BulletRichTextBox { Text = "first\nsecond", BulletIndent = 24, MaxLength = 3 };
            CreateHostedHandle(form, rich);
            rich.Select(1, 3);
            rich.SelectionIndent = 48;
            rich.SelectionRightIndent = 16;
            rich.SelectionAlignment = HorizontalAlignment.Center;
            rich.SelectionBullet = true;
            ParagraphState before = ReadParagraph(rich.Handle);
            rich.Modified = modified;
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;

            rich.RecreateNow();

            Assert.Equal(2, rich.CreatedCount);
            Assert.True(rich.SelectionBullet);
            Assert.Equal(24, rich.BulletIndent);
            ParagraphState after = ReadParagraph(rich.Handle);
            Assert.Equal(before.Numbering, after.Numbering);
            Assert.Equal(before.Offset, after.Offset);
            Assert.Equal(before.StartIndent, after.StartIndent);
            Assert.Equal(before.RightIndent, after.RightIndent);
            Assert.Equal(before.Alignment, after.Alignment);
            Assert.Equal("first\nsecond", rich.Text);
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.Equal(modified, rich.Modified);
            Assert.Equal((nint)3, SendMessageW(rich.Handle, 0x00D5, 0, 0));
            Assert.Equal(0, changes);
        });

    private static PropertyInfo DeclaredProperty(string name)
        => typeof(RichTextBox).GetProperty(name,
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)!;

    private static void InvokeBullet(RichTextBox rich, string operation)
    {
        if (operation == "get") { _ = rich.SelectionBullet; }
        else { rich.SelectionBullet = operation == "setTrue"; }
    }

    private static byte[] EmptyParagraph()
    {
        byte[] bytes = new byte[ParagraphSize];
        BitConverter.GetBytes(ParagraphSize).CopyTo(bytes, 0);
        return bytes;
    }

    private static ParagraphState ReadParagraph(nint handle)
    {
        nint paragraph = Marshal.AllocHGlobal(ParagraphSize);
        try
        {
            Marshal.Copy(EmptyParagraph(), 0, paragraph, ParagraphSize);
            _ = SendMessageW(handle, EmGetParaFormat, 0, paragraph);
            return new ParagraphState(Marshal.ReadInt32(paragraph, 4),
                unchecked((ushort)Marshal.ReadInt16(paragraph, 8)),
                Marshal.ReadInt32(paragraph, 12), Marshal.ReadInt32(paragraph, 16),
                Marshal.ReadInt32(paragraph, 20), unchecked((ushort)Marshal.ReadInt16(paragraph, 24)),
                Marshal.ReadInt16(paragraph, 26), Marshal.ReadInt32(paragraph, 28));
        }
        finally { Marshal.FreeHGlobal(paragraph); }
    }

    private static void SetNativeBullet(nint handle, bool bullet, int offset)
    {
        nint paragraph = Marshal.AllocHGlobal(ParagraphSize);
        try
        {
            Marshal.Copy(EmptyParagraph(), 0, paragraph, ParagraphSize);
            Marshal.WriteInt32(paragraph, 4, 0x24);
            Marshal.WriteInt16(paragraph, 8, (short)(bullet ? 1 : 0));
            Marshal.WriteInt32(paragraph, 20, offset);
            Assert.NotEqual((nint)0, SendMessageW(handle, EmSetParaFormat, 0, paragraph));
        }
        finally { Marshal.FreeHGlobal(paragraph); }
    }

    private static int ReadCharacterEffects(nint handle)
    {
        nint format = Marshal.AllocHGlobal(116);
        try
        {
            Marshal.Copy(new byte[116], 0, format, 116);
            Marshal.WriteInt32(format, 116);
            _ = SendMessageW(handle, 0x043A, 1, format);
            return Marshal.ReadInt32(format, 8);
        }
        finally { Marshal.FreeHGlobal(format); }
    }

    private static int PixelToTwip(int value)
    {
        nint dc = GetDC(0);
        Assert.NotEqual((nint)0, dc);
        try { return unchecked((int)((value / (double)GetDeviceCaps(dc, 88)) * 72.0 * 20.0)); }
        finally { _ = ReleaseDC(0, dc); }
    }

    private static void CreateHostedHandle(Form form, RichTextBox rich)
    {
        form.Controls.Add(rich);
        _ = form.Handle;
        _ = rich.Handle;
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "RichTextBox bullet test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private readonly record struct ParagraphState(int Mask, ushort Numbering, int StartIndent,
        int RightIndent, int Offset, ushort Alignment, short TabCount, int FirstTab);

    private readonly record struct ParagraphMessage(int Message, nint WParam, byte[] Bytes);

    private sealed class BulletRichTextBox : RichTextBox
    {
        internal bool InterceptParagraph { get; set; }
        internal int ReturnedMask { get; set; }
        internal ushort ReturnedNumbering { get; set; }
        internal int IndentObservedByGet { get; private set; }
        internal List<ParagraphMessage> ParagraphMessages { get; } = [];
        internal int CreatedCount { get; private set; }
        internal bool FailCreation { get; set; }
        internal InvalidOperationException CreationFailure { get; } = new("Bullet creation failure");

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
                    IndentObservedByGet = BulletIndent;
                    Marshal.WriteInt32(message.LParam, 4, ReturnedMask);
                    Marshal.WriteInt16(message.LParam, 8, unchecked((short)ReturnedNumbering));
                }
                // Fixed-source accessors consume the payload and ignore this result.
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
