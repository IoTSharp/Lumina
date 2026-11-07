using System.ComponentModel;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class RichTextBoxSelectionAlignmentTests
{
    private const int EmGetParaFormat = 0x043D;
    private const int EmSetParaFormat = 0x0447;
    private const int EmGetLimitText = 0x00D5;
    private const int ParagraphSize = 156;
    private const int AlignmentMask = 8;

    [Fact]
    public void PropertyHasTheStandardDeclaredContract()
    {
        PropertyInfo property = typeof(RichTextBox).GetProperty(nameof(RichTextBox.SelectionAlignment),
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)!;

        Assert.NotNull(property);
        Assert.Equal(typeof(HorizontalAlignment), property.PropertyType);
        Assert.False(property.GetMethod!.IsVirtual);
        Assert.False(property.SetMethod!.IsVirtual);
        Assert.Equal(HorizontalAlignment.Left, property.GetCustomAttribute<DefaultValueAttribute>()!.Value);
        Assert.False(property.GetCustomAttribute<BrowsableAttribute>()!.Browsable);
        Assert.Equal(DesignerSerializationVisibility.Hidden,
            property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>()!.Visibility);
    }

    [Fact]
    public void GetterCreatesTheNativeHandleAndReadsTheDefaultParagraph()
        => OnSta(() =>
        {
            using var rich = new AlignmentRichTextBox();
            Assert.False(rich.IsHandleCreated);

            Assert.Equal(HorizontalAlignment.Left, rich.SelectionAlignment);

            Assert.True(rich.IsHandleCreated);
            Assert.Equal(1, rich.CreatedCount);
            Assert.Equal((ushort)1, ReadParagraph(rich.Handle).Alignment);
        });

    [Theory]
    [InlineData(HorizontalAlignment.Left, 1)]
    [InlineData(HorizontalAlignment.Right, 2)]
    [InlineData(HorizontalAlignment.Center, 3)]
    public void ValidSetterCreatesTheHandleAndSetsTheNativeParagraph(HorizontalAlignment value, int nativeValue)
        => OnSta(() =>
        {
            using var rich = new AlignmentRichTextBox();
            Assert.False(rich.IsHandleCreated);

            rich.SelectionAlignment = value;

            Assert.True(rich.IsHandleCreated);
            Assert.Equal(1, rich.CreatedCount);
            Assert.Equal((ushort)nativeValue, ReadParagraph(rich.Handle).Alignment);
            Assert.Equal(value, rich.SelectionAlignment);
        });

    [Theory]
    [InlineData(-1)]
    [InlineData(3)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void InvalidAlignmentIsRejectedBeforeHandleCreation(int value)
        => OnSta(() =>
        {
            using var rich = new AlignmentRichTextBox { Text = "cached text", FailCreation = true };

            InvalidEnumArgumentException error = Assert.Throws<InvalidEnumArgumentException>(
                () => rich.SelectionAlignment = (HorizontalAlignment)value);

            Assert.Equal("value", error.ParamName);
            Assert.False(rich.IsHandleCreated);
            Assert.Equal(0, rich.CreatedCount);
            Assert.Equal("cached text", rich.Text);
        });

    [Theory]
    [InlineData(0, 3, HorizontalAlignment.Left)]
    [InlineData(8, 1, HorizontalAlignment.Left)]
    [InlineData(8, 2, HorizontalAlignment.Right)]
    [InlineData(8, 3, HorizontalAlignment.Center)]
    [InlineData(8, 0, HorizontalAlignment.Left)]
    [InlineData(8, 4, HorizontalAlignment.Left)]
    [InlineData(8, ushort.MaxValue, HorizontalAlignment.Left)]
    public void GetterUsesTheReturnedMaskAndNativeAlignment(int mask, int nativeValue, HorizontalAlignment expected)
        => OnSta(() =>
        {
            using var rich = new AlignmentRichTextBox();
            _ = rich.Handle;
            rich.InterceptParagraph = true;
            rich.ReturnedMask = mask;
            rich.ReturnedAlignment = (ushort)nativeValue;

            Assert.Equal(expected, rich.SelectionAlignment);

            ParagraphMessage call = Assert.Single(rich.ParagraphMessages);
            Assert.Equal(EmGetParaFormat, call.Message);
            Assert.Equal((nint)0, call.WParam);
            byte[] expectedBytes = new byte[ParagraphSize];
            expectedBytes[0] = ParagraphSize;
            Assert.Equal(expectedBytes, call.Bytes);
        });

    [Theory]
    [InlineData(HorizontalAlignment.Left, 1)]
    [InlineData(HorizontalAlignment.Right, 2)]
    [InlineData(HorizontalAlignment.Center, 3)]
    public void SetterSendsOnlyTheAlignmentInACompleteNativeStructure(HorizontalAlignment value, int nativeValue)
        => OnSta(() =>
        {
            using var rich = new AlignmentRichTextBox();
            _ = rich.Handle;
            rich.InterceptParagraph = true;

            rich.SelectionAlignment = value;

            ParagraphMessage call = Assert.Single(rich.ParagraphMessages);
            Assert.Equal(EmSetParaFormat, call.Message);
            Assert.Equal((nint)0, call.WParam);
            byte[] expectedBytes = new byte[ParagraphSize];
            expectedBytes[0] = ParagraphSize;
            expectedBytes[4] = AlignmentMask;
            expectedBytes[24] = (byte)nativeValue;
            Assert.Equal(expectedBytes, call.Bytes);
        });

    [Fact]
    public void RepeatedAssignmentsStillSendNativeParagraphChanges()
        => OnSta(() =>
        {
            using var rich = new AlignmentRichTextBox();
            _ = rich.Handle;
            rich.InterceptParagraph = true;

            rich.SelectionAlignment = HorizontalAlignment.Center;
            rich.SelectionAlignment = HorizontalAlignment.Center;

            Assert.Equal(2, rich.ParagraphMessages.Count);
            Assert.All(rich.ParagraphMessages, call => Assert.Equal(EmSetParaFormat, call.Message));
            Assert.Equal(rich.ParagraphMessages[0].Bytes, rich.ParagraphMessages[1].Bytes);
        });

    [Theory]
    [InlineData(HorizontalAlignment.Left, 1)]
    [InlineData(HorizontalAlignment.Right, 2)]
    [InlineData(HorizontalAlignment.Center, 3)]
    public void SelectedParagraphChangesWithoutChangingTheFollowingParagraph(HorizontalAlignment value, int nativeValue)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new AlignmentRichTextBox { Text = "first\nsecond\nthird" };
            CreateHostedHandle(form, rich);
            rich.Select(1, 3);

            rich.SelectionAlignment = value;

            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.Equal((ushort)nativeValue, ReadParagraph(rich.Handle).Alignment);
            Assert.Equal(value, rich.SelectionAlignment);
            rich.Select(6, 6);
            Assert.Equal((ushort)1, ReadParagraph(rich.Handle).Alignment);
            Assert.Equal("first\nsecond\nthird", rich.Text);
        });

    [Fact]
    public void EmptySelectionChangesItsContainingParagraph()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new AlignmentRichTextBox { Text = "first\nsecond" };
            CreateHostedHandle(form, rich);
            rich.Select(2, 0);

            rich.SelectionAlignment = HorizontalAlignment.Center;

            Assert.Equal(2, rich.SelectionStart);
            Assert.Equal(0, rich.SelectionLength);
            rich.Select(0, 5);
            Assert.Equal((ushort)3, ReadParagraph(rich.Handle).Alignment);
            rich.Select(6, 6);
            Assert.Equal((ushort)1, ReadParagraph(rich.Handle).Alignment);
        });

    [Fact]
    public void MixedParagraphAlignmentUsesTheNativeMaskAndReturnsLeft()
        => OnSta(() =>
        {
            using var rich = new AlignmentRichTextBox { Text = "first\nsecond" };
            _ = rich.Handle;
            rich.Select(0, 5);
            rich.SelectionAlignment = HorizontalAlignment.Right;
            rich.Select(6, 6);
            rich.SelectionAlignment = HorizontalAlignment.Center;
            rich.SelectAll();

            Assert.Equal(0, ReadParagraph(rich.Handle).Mask & AlignmentMask);
            Assert.Equal(HorizontalAlignment.Left, rich.SelectionAlignment);
        });

    [Fact]
    public void RepeatingAValueAfterMovingTheSelectionFormatsBothParagraphs()
        => OnSta(() =>
        {
            using var rich = new AlignmentRichTextBox { Text = "first\nsecond" };
            _ = rich.Handle;
            rich.Select(0, 5);
            rich.SelectionAlignment = HorizontalAlignment.Right;
            rich.Select(6, 6);
            rich.SelectionAlignment = HorizontalAlignment.Right;

            rich.SelectAll();
            ParagraphState state = ReadParagraph(rich.Handle);
            Assert.NotEqual(0, state.Mask & AlignmentMask);
            Assert.Equal((ushort)2, state.Alignment);
            Assert.Equal(HorizontalAlignment.Right, rich.SelectionAlignment);
        });

    [Fact]
    public void GetterReadsTheLatestIndependentNativeParagraphChange()
        => OnSta(() =>
        {
            using var rich = new AlignmentRichTextBox { Text = "native paragraph" };
            _ = rich.Handle;
            rich.SelectAll();
            rich.SelectionAlignment = HorizontalAlignment.Right;

            SetNativeAlignment(rich.Handle, 3);

            Assert.Equal(HorizontalAlignment.Center, rich.SelectionAlignment);
            SetNativeAlignment(rich.Handle, 1);
            Assert.Equal(HorizontalAlignment.Left, rich.SelectionAlignment);
        });

    [Fact]
    public void GetterPreservesTextSelectionLimitModifiedAndNotifications()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new AlignmentRichTextBox { Text = "first\nsecond", MaxLength = 3 };
            CreateHostedHandle(form, rich);
            rich.Select(1, 3);
            SetNativeAlignment(rich.Handle, 2);
            rich.Modified = true;
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;

            Assert.Equal(HorizontalAlignment.Right, rich.SelectionAlignment);
            Assert.Equal(HorizontalAlignment.Right, rich.SelectionAlignment);

            Assert.Equal("first\nsecond", rich.Text);
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.True(rich.Modified);
            Assert.Equal((nint)3, SendMessageW(rich.Handle, EmGetLimitText, 0, 0));
            Assert.Equal(0, changes);
        });

    [Fact]
    public void AlignmentAssignmentPreservesExistingIndentAndTabFormatting()
        => OnSta(() =>
        {
            using var rich = new AlignmentRichTextBox();
            rich.Rtf = @"{\rtf1\ansi\pard\qc\li720\ri360\fi-120\tx300\b styled paragraph}";
            rich.SelectAll();
            ParagraphState before = ReadParagraph(rich.Handle);
            Assert.NotEqual(0, before.StartIndent);
            Assert.NotEqual(0, before.RightIndent);
            Assert.NotEqual(0, before.Offset);
            Assert.True(before.TabCount > 0);

            rich.SelectionAlignment = HorizontalAlignment.Right;

            ParagraphState after = ReadParagraph(rich.Handle);
            Assert.Equal((ushort)2, after.Alignment);
            Assert.Equal(before.StartIndent, after.StartIndent);
            Assert.Equal(before.RightIndent, after.RightIndent);
            Assert.Equal(before.Offset, after.Offset);
            Assert.Equal(before.TabCount, after.TabCount);
            Assert.Equal(before.FirstTab, after.FirstTab);
            Assert.Equal("styled paragraph", rich.Text);
            Assert.Contains(@"\b", rich.Rtf!);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void PublicAlignmentSurvivesHandleRecreationWithTheDocumentState(bool modified)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new AlignmentRichTextBox { Text = "first\nsecond", MaxLength = 3 };
            CreateHostedHandle(form, rich);
            rich.Select(1, 3);
            rich.SelectionAlignment = HorizontalAlignment.Center;
            rich.Modified = modified;
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;

            rich.RecreateNow();

            Assert.Equal(2, rich.CreatedCount);
            Assert.Equal("first\nsecond", rich.Text);
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.Equal(HorizontalAlignment.Center, rich.SelectionAlignment);
            Assert.Equal((ushort)3, ReadParagraph(rich.Handle).Alignment);
            Assert.Equal(modified, rich.Modified);
            Assert.Equal((nint)3, SendMessageW(rich.Handle, EmGetLimitText, 0, 0));
            Assert.Equal(0, changes);
        });

    [Fact]
    public void InvalidValueOnAnExistingHandleLeavesTheParagraphUnchanged()
        => OnSta(() =>
        {
            using var rich = new AlignmentRichTextBox { Text = "paragraph" };
            _ = rich.Handle;
            rich.Select(1, 3);
            rich.SelectionAlignment = HorizontalAlignment.Right;
            ParagraphState before = ReadParagraph(rich.Handle);

            Assert.Throws<InvalidEnumArgumentException>(() => rich.SelectionAlignment = (HorizontalAlignment)3);

            Assert.Equal(before, ReadParagraph(rich.Handle));
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.Equal("paragraph", rich.Text);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void CreationFailurePropagatesAndTheSameInstanceCanRetry(bool setter)
        => OnSta(() =>
        {
            using var rich = new AlignmentRichTextBox { FailCreation = true };
            Exception error = Assert.Throws<InvalidOperationException>(() =>
            {
                if (setter) { rich.SelectionAlignment = HorizontalAlignment.Right; }
                else { _ = rich.SelectionAlignment; }
            });
            Assert.Same(rich.CreationFailure, error);
            Assert.False(rich.IsHandleCreated);

            rich.FailCreation = false;
            if (setter) { rich.SelectionAlignment = HorizontalAlignment.Right; }
            else { _ = rich.SelectionAlignment; }

            Assert.Equal(1, rich.CreatedCount);
            Assert.Equal(setter ? HorizontalAlignment.Right : HorizontalAlignment.Left, rich.SelectionAlignment);
        });

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
            return new ParagraphState(Marshal.ReadInt32(paragraph, 4),
                unchecked((ushort)Marshal.ReadInt16(paragraph, 24)),
                Marshal.ReadInt32(paragraph, 12), Marshal.ReadInt32(paragraph, 16),
                Marshal.ReadInt32(paragraph, 20), Marshal.ReadInt16(paragraph, 26),
                Marshal.ReadInt32(paragraph, 28));
        }
        finally { Marshal.FreeHGlobal(paragraph); }
    }

    private static void SetNativeAlignment(nint handle, short alignment)
    {
        nint paragraph = Marshal.AllocHGlobal(ParagraphSize);
        try
        {
            Marshal.Copy(new byte[ParagraphSize], 0, paragraph, ParagraphSize);
            Marshal.WriteInt32(paragraph, ParagraphSize);
            Marshal.WriteInt32(paragraph, 4, AlignmentMask);
            Marshal.WriteInt16(paragraph, 24, alignment);
            Assert.NotEqual((nint)0, SendMessageW(handle, EmSetParaFormat, 0, paragraph));
        }
        finally { Marshal.FreeHGlobal(paragraph); }
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "RichTextBox SelectionAlignment test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private readonly record struct ParagraphState(int Mask, ushort Alignment, int StartIndent,
        int RightIndent, int Offset, short TabCount, int FirstTab);

    private readonly record struct ParagraphMessage(int Message, nint WParam, byte[] Bytes);

    private sealed class AlignmentRichTextBox : RichTextBox
    {
        internal bool InterceptParagraph { get; set; }
        internal int ReturnedMask { get; set; }
        internal ushort ReturnedAlignment { get; set; }
        internal List<ParagraphMessage> ParagraphMessages { get; } = [];
        internal int CreatedCount { get; private set; }
        internal bool FailCreation { get; set; }
        internal InvalidOperationException CreationFailure { get; } = new("SelectionAlignment creation failure");

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
                    Marshal.WriteInt16(message.LParam, 24, unchecked((short)ReturnedAlignment));
                }
                // A zero return deliberately proves that the public accessors
                // use the filled mask and preserve the native setter contract.
                message.Result = 0;
                return;
            }
            base.WndProc(ref message);
        }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint handle, int message, nint wParam, nint lParam);
}
