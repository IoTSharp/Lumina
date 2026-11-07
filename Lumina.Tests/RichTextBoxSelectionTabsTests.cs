using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class RichTextBoxSelectionTabsTests
{
    private const int EmGetParaFormat = 0x043D;
    private const int EmSetParaFormat = 0x0447;
    private const int EmGetLimitText = 0x00D5;
    private const int ParagraphSize = 156;
    private const int TabMask = 0x10;

    [Fact]
    public void PropertyHasTheStandardDeclaredContract()
    {
        PropertyInfo property = typeof(RichTextBox).GetProperty(nameof(RichTextBox.SelectionTabs),
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)!;

        Assert.NotNull(property);
        Assert.Equal(typeof(int[]), property.PropertyType);
        Assert.False(property.GetMethod!.IsVirtual);
        Assert.False(property.SetMethod!.IsVirtual);
        Assert.False(property.GetCustomAttribute<BrowsableAttribute>()!.Browsable);
        Assert.Equal(DesignerSerializationVisibility.Hidden,
            property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>()!.Visibility);
        Assert.NotNull(Assert.Single(property.SetMethod.GetParameters()).GetCustomAttribute<AllowNullAttribute>());
    }

    [Fact]
    public void GetterCreatesTheHandleAndReadsTheEmptyDefault()
        => OnSta(() =>
        {
            using var rich = new TabsRichTextBox();
            Assert.False(rich.IsHandleCreated);

            Assert.Empty(rich.SelectionTabs);

            Assert.True(rich.IsHandleCreated);
            Assert.Equal(1, rich.CreatedCount);
            Assert.Empty(ReadNativeTabs(rich.Handle));
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void SetterCreatesTheHandleIncludingNullAndEmptyValues(bool nullValue)
        => OnSta(() =>
        {
            using var rich = new TabsRichTextBox();
            Assert.False(rich.IsHandleCreated);

            rich.SelectionTabs = nullValue ? null : [];

            Assert.True(rich.IsHandleCreated);
            Assert.Equal(1, rich.CreatedCount);
            Assert.Empty(ReadNativeTabs(rich.Handle));
        });

    [Theory]
    [InlineData(33)]
    [InlineData(64)]
    public void TooManyTabsAreRejectedBeforeHandleCreation(int count)
        => OnSta(() =>
        {
            using var rich = new TabsRichTextBox { Text = "cached text", FailCreation = true };

            ArgumentOutOfRangeException error = Assert.Throws<ArgumentOutOfRangeException>(
                () => rich.SelectionTabs = new int[count]);

            Assert.Equal("value", error.ParamName);
            Assert.Null(error.ActualValue);
            Assert.False(rich.IsHandleCreated);
            Assert.Equal(0, rich.CreatedCount);
            Assert.Equal("cached text", rich.Text);
        });

    [Fact]
    public void TooManyTabsOnAnExistingHandleLeaveTheNativeStateAndSelectionAlone()
        => OnSta(() =>
        {
            using var rich = new TabsRichTextBox { Text = "paragraph" };
            rich.SelectionTabs = [24, 48];
            rich.Select(1, 3);
            int[] before = ReadNativeTabs(rich.Handle);
            rich.InterceptParagraph = true;

            ArgumentOutOfRangeException error = Assert.Throws<ArgumentOutOfRangeException>(
                () => rich.SelectionTabs = new int[33]);

            Assert.Equal("value", error.ParamName);
            Assert.Null(error.ActualValue);
            Assert.Empty(rich.ParagraphMessages);
            rich.InterceptParagraph = false;
            Assert.Equal(before, ReadNativeTabs(rich.Handle));
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.Equal("paragraph", rich.Text);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void OriginalCreationFailurePropagatesAndTheSameInstanceCanRetry(bool setter)
        => OnSta(() =>
        {
            using var rich = new TabsRichTextBox { FailCreation = true };

            Exception error = Assert.Throws<InvalidOperationException>(() =>
            {
                if (setter) { rich.SelectionTabs = [24, 48]; }
                else { _ = rich.SelectionTabs; }
            });

            Assert.Same(rich.CreationFailure, error);
            Assert.False(rich.IsHandleCreated);
            rich.FailCreation = false;
            if (setter) { rich.SelectionTabs = [24, 48]; }
            else { _ = rich.SelectionTabs; }
            Assert.Equal(1, rich.CreatedCount);
            Assert.Equal(setter ? new[] { PixelToTwip(24), PixelToTwip(48) } : [],
                ReadNativeTabs(rich.Handle));
        });

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(TabMask)]
    [InlineData(TabMask | 8)]
    public void GetterUsesTheTabMaskAndSendsOnlyTheSizeInItsRequest(int mask)
        => OnSta(() =>
        {
            using var rich = new TabsRichTextBox();
            _ = rich.Handle;
            rich.ReturnedParagraph = ParagraphWithTabs(mask, [-451, 0, 907]);
            rich.InterceptParagraph = true;

            int[] actual = rich.SelectionTabs;

            Assert.Equal((mask & TabMask) == 0 ? [] : new[] { TwipToPixel(-451), 0, TwipToPixel(907) }, actual);
            ParagraphMessage call = Assert.Single(rich.ParagraphMessages);
            Assert.Equal(EmGetParaFormat, call.Message);
            Assert.Equal((nint)0, call.WParam);
            Assert.Equal(EmptyParagraph(), call.Bytes);
        });

    [Fact]
    public void GetterWithAValidZeroTabCountReturnsAnEmptyArray()
        => OnSta(() =>
        {
            using var rich = new TabsRichTextBox();
            _ = rich.Handle;
            rich.ReturnedParagraph = ParagraphWithTabs(TabMask, []);
            rich.InterceptParagraph = true;

            Assert.Empty(rich.SelectionTabs);
        });

    [Fact]
    public void GetterReadsAllThirtyTwoNativeSlotsAndReturnsIndependentArrays()
        => OnSta(() =>
        {
            using var rich = new TabsRichTextBox();
            _ = rich.Handle;
            int[] twips = new int[32];
            int[] pixels = new int[32];
            for (int index = 0; index < 32; index++)
            {
                twips[index] = (index - 16) * 451;
                pixels[index] = TwipToPixel(twips[index]);
            }
            rich.ReturnedParagraph = ParagraphWithTabs(TabMask, twips);
            rich.InterceptParagraph = true;

            int[] first = rich.SelectionTabs;
            Assert.Equal(pixels, first);
            first[0] = 123456;
            int[] second = rich.SelectionTabs;

            Assert.NotSame(first, second);
            Assert.Equal(pixels, second);
            Assert.Equal(2, rich.ParagraphMessages.Count);
        });

    [Theory]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void SetterAcceptsFullIntegerValuesWithoutElementValidation(int value)
        => OnSta(() =>
        {
            using var rich = new TabsRichTextBox();
            _ = rich.Handle;
            rich.InterceptParagraph = true;

            rich.SelectionTabs = [value];

            Assert.Equal(2, rich.ParagraphMessages.Count);
            Assert.Equal(EmGetParaFormat, rich.ParagraphMessages[0].Message);
            Assert.Equal(EmSetParaFormat, rich.ParagraphMessages[1].Message);
            Assert.Equal(PixelToTwip(value), BitConverter.ToInt32(rich.ParagraphMessages[1].Bytes, 28));
        });

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(32)]
    public void SetterReadsThenPreservesTheWholeNativeStructureExceptItsMaskCountAndUsedTabs(int count)
        => OnSta(() =>
        {
            using var rich = new TabsRichTextBox();
            _ = rich.Handle;
            byte[] response = new byte[ParagraphSize];
            for (int index = 0; index < ParagraphSize; index++) { response[index] = (byte)(index + 1); }
            WriteInt32(response, 0, ParagraphSize);
            WriteInt32(response, 4, 0x3F);
            WriteInt16(response, 26, 32);
            rich.ReturnedParagraph = response;
            rich.InterceptParagraph = true;
            int[] input = new int[count];
            for (int index = 0; index < count; index++) { input[index] = (count - index) * 17; }

            rich.SelectionTabs = input;

            Assert.Equal(2, rich.ParagraphMessages.Count);
            ParagraphMessage get = rich.ParagraphMessages[0];
            ParagraphMessage set = rich.ParagraphMessages[1];
            Assert.Equal(EmGetParaFormat, get.Message);
            Assert.Equal((nint)0, get.WParam);
            Assert.Equal(EmptyParagraph(), get.Bytes);
            Assert.Equal(EmSetParaFormat, set.Message);
            Assert.Equal((nint)0, set.WParam);
            byte[] expected = (byte[])response.Clone();
            WriteInt32(expected, 4, TabMask);
            WriteInt16(expected, 26, (short)count);
            for (int index = 0; index < count; index++) { WriteInt32(expected, 28 + 4 * index, PixelToTwip(input[index])); }
            Assert.Equal(expected, set.Bytes);
            Assert.Equal(response, rich.ReturnedParagraph);
        });

    [Fact]
    public void NullSetterStillReadsTheCurrentParagraphAndPreservesItsUnusedSlots()
        => OnSta(() =>
        {
            using var rich = new TabsRichTextBox();
            _ = rich.Handle;
            byte[] response = ParagraphWithTabs(0x3F, [300, 600, 900]);
            WriteInt32(response, 12, 721);
            rich.ReturnedParagraph = response;
            rich.InterceptParagraph = true;

            rich.SelectionTabs = null;

            Assert.Equal(2, rich.ParagraphMessages.Count);
            Assert.Equal(EmGetParaFormat, rich.ParagraphMessages[0].Message);
            Assert.Equal(EmSetParaFormat, rich.ParagraphMessages[1].Message);
            byte[] expected = (byte[])response.Clone();
            WriteInt32(expected, 4, TabMask);
            WriteInt16(expected, 26, 0);
            Assert.Equal(expected, rich.ParagraphMessages[1].Bytes);
        });

    [Fact]
    public void RepeatedAssignmentsAlwaysReadAndSubmitTheCurrentParagraph()
        => OnSta(() =>
        {
            using var rich = new TabsRichTextBox();
            _ = rich.Handle;
            rich.InterceptParagraph = true;

            rich.SelectionTabs = [24, 48];
            rich.SelectionTabs = [24, 48];

            Assert.Equal(new[] { EmGetParaFormat, EmSetParaFormat, EmGetParaFormat, EmSetParaFormat },
                rich.ParagraphMessages.Select(call => call.Message));
            Assert.Equal(rich.ParagraphMessages[1].Bytes, rich.ParagraphMessages[3].Bytes);
        });

    [Fact]
    public void NativeStateDoesNotRetainTheAssignedArray()
        => OnSta(() =>
        {
            using var rich = new TabsRichTextBox { Text = "paragraph" };
            int[] input = [24, 48];

            rich.SelectionTabs = input;
            input[0] = 96;

            Assert.Equal(new[] { PixelToTwip(24), PixelToTwip(48) }, ReadNativeTabs(rich.Handle));
            Assert.Equal(new[] { TwipToPixel(PixelToTwip(24)), TwipToPixel(PixelToTwip(48)) }, rich.SelectionTabs);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void NullAndEmptyAssignmentsClearExistingNativeTabs(bool nullValue)
        => OnSta(() =>
        {
            using var rich = new TabsRichTextBox { Text = "paragraph" };
            rich.SelectionTabs = [24, 48];
            Assert.Equal(2, ReadNativeTabs(rich.Handle).Length);

            rich.SelectionTabs = nullValue ? null : [];

            Assert.Empty(ReadNativeTabs(rich.Handle));
            Assert.Empty(rich.SelectionTabs);
        });

    [Fact]
    public void SelectedParagraphChangesWithoutChangingTheFollowingParagraph()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new TabsRichTextBox { Text = "first\nsecond\nthird" };
            CreateHostedHandle(form, rich);
            rich.Select(1, 3);

            rich.SelectionTabs = [24, 64];

            Assert.Equal(new[] { PixelToTwip(24), PixelToTwip(64) }, ReadNativeTabs(rich.Handle));
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            rich.Select(6, 6);
            Assert.Empty(ReadNativeTabs(rich.Handle));
            Assert.Equal("first\nsecond\nthird", rich.Text);
        });

    [Fact]
    public void EmptySelectionFormatsItsContainingParagraph()
        => OnSta(() =>
        {
            using var rich = new TabsRichTextBox { Text = "first\nsecond" };
            _ = rich.Handle;
            rich.Select(2, 0);

            rich.SelectionTabs = [24, 64];

            Assert.Equal(2, rich.SelectionStart);
            Assert.Equal(0, rich.SelectionLength);
            rich.Select(0, 5);
            Assert.Equal(new[] { PixelToTwip(24), PixelToTwip(64) }, ReadNativeTabs(rich.Handle));
            rich.Select(6, 6);
            Assert.Empty(ReadNativeTabs(rich.Handle));
        });

    [Fact]
    public void MixedParagraphsUseTheNativeMaskAndReturnNoTabs()
        => OnSta(() =>
        {
            using var rich = new TabsRichTextBox { Text = "first\nsecond" };
            _ = rich.Handle;
            rich.Select(0, 5);
            rich.SelectionTabs = [24];
            rich.Select(6, 6);
            rich.SelectionTabs = [48, 96];
            rich.SelectAll();

            Assert.Equal(0, BitConverter.ToInt32(ReadParagraph(rich.Handle), 4) & TabMask);
            Assert.Empty(rich.SelectionTabs);
        });

    [Fact]
    public void RepeatingTheValueAfterMovingSelectionFormatsBothParagraphs()
        => OnSta(() =>
        {
            using var rich = new TabsRichTextBox { Text = "first\nsecond" };
            _ = rich.Handle;
            rich.Select(0, 5);
            rich.SelectionTabs = [24, 64];
            rich.Select(6, 6);
            rich.SelectionTabs = [24, 64];
            rich.SelectAll();

            Assert.NotEqual(0, BitConverter.ToInt32(ReadParagraph(rich.Handle), 4) & TabMask);
            Assert.Equal(new[] { PixelToTwip(24), PixelToTwip(64) }, ReadNativeTabs(rich.Handle));
        });

    [Fact]
    public void GetterReadsIndependentNativeChangesWithoutACache()
        => OnSta(() =>
        {
            using var rich = new TabsRichTextBox { Text = "native paragraph" };
            _ = rich.Handle;
            rich.SelectionTabs = [24];

            SetNativeParagraph(rich.Handle, ParagraphWithTabs(TabMask, [451, 907]));
            Assert.Equal(new[] { TwipToPixel(451), TwipToPixel(907) }, rich.SelectionTabs);
            SetNativeParagraph(rich.Handle, ParagraphWithTabs(TabMask, [1501]));
            Assert.Equal(new[] { TwipToPixel(1501) }, rich.SelectionTabs);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void GetterPreservesTextSelectionModifiedUndoLimitAndNotifications(bool modified)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new TabsRichTextBox { Text = "first\nsecond", MaxLength = 3 };
            CreateHostedHandle(form, rich);
            rich.Select(1, 3);
            SetNativeParagraph(rich.Handle, ParagraphWithTabs(TabMask, [451, 907]));
            rich.Modified = modified;
            bool canUndo = rich.CanUndo;
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;

            Assert.Equal(new[] { TwipToPixel(451), TwipToPixel(907) }, rich.SelectionTabs);
            Assert.Equal(new[] { TwipToPixel(451), TwipToPixel(907) }, rich.SelectionTabs);

            Assert.Equal("first\nsecond", rich.Text);
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.Equal(modified, rich.Modified);
            Assert.Equal(canUndo, rich.CanUndo);
            Assert.Equal((nint)3, SendMessageW(rich.Handle, EmGetLimitText, 0, 0));
            Assert.Equal(0, changes);
        });

    [Fact]
    public void AssignmentPreservesOtherParagraphAndRealCharacterFormatting()
        => OnSta(() =>
        {
            using var rich = new TabsRichTextBox();
            rich.Rtf = @"{\rtf1\ansi\pard\qc\li720\ri360\fi-120\tx300\b styled paragraph}";
            rich.SelectAll();
            byte[] bullet = EmptyParagraph();
            WriteInt32(bullet, 4, 0x20);
            WriteInt16(bullet, 8, 1);
            SetNativeParagraph(rich.Handle, bullet);
            byte[] before = ReadParagraph(rich.Handle);
            int characterEffects = ReadCharacterEffects(rich.Handle);
            Assert.NotEqual(0, characterEffects & 1);
            Assert.Equal(1, BitConverter.ToInt16(before, 8));

            rich.SelectionTabs = [24, 64];

            byte[] after = ReadParagraph(rich.Handle);
            Assert.Equal(before.AsSpan(8, 18).ToArray(), after.AsSpan(8, 18).ToArray());
            Assert.Equal(new[] { PixelToTwip(24), PixelToTwip(64) }, ReadNativeTabs(rich.Handle));
            Assert.Equal(characterEffects, ReadCharacterEffects(rich.Handle));
            Assert.Equal("styled paragraph", rich.Text);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void TabsSurviveRtfHandleRecreationWithDocumentState(bool modified)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new TabsRichTextBox { Text = "first\nsecond", MaxLength = 3 };
            CreateHostedHandle(form, rich);
            rich.Select(1, 3);
            rich.SelectionIndent = 48;
            rich.SelectionHangingIndent = 16;
            rich.SelectionRightIndent = 24;
            rich.SelectionAlignment = HorizontalAlignment.Center;
            rich.SelectionTabs = [24, 64, 96];
            byte[] before = ReadParagraph(rich.Handle);
            rich.Modified = modified;
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;

            rich.RecreateNow();

            byte[] after = ReadParagraph(rich.Handle);
            Assert.Equal(2, rich.CreatedCount);
            Assert.Equal(before.AsSpan(12, 16).ToArray(), after.AsSpan(12, 16).ToArray());
            Assert.Equal(new[] { PixelToTwip(24), PixelToTwip(64), PixelToTwip(96) }, ReadNativeTabs(rich.Handle));
            Assert.Equal(new[] { TwipToPixel(PixelToTwip(24)), TwipToPixel(PixelToTwip(64)), TwipToPixel(PixelToTwip(96)) }, rich.SelectionTabs);
            Assert.Equal("first\nsecond", rich.Text);
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.Equal(modified, rich.Modified);
            Assert.False(rich.CanUndo);
            Assert.Equal((nint)3, SendMessageW(rich.Handle, EmGetLimitText, 0, 0));
            Assert.Equal(0, changes);
        });

    [Fact]
    public void IndependentRichEditWindowReceivesTheSameRealTabStops()
        => OnSta(() =>
        {
            using var rich = new TabsRichTextBox();
            _ = rich.Handle;
            nint native = CreateWindowExW(0, "RICHEDIT50W", string.Empty,
                0x80000004, 0, 0, 400, 200, 0, 0, 0, 0);
            Assert.NotEqual((nint)0, native);
            try
            {
                rich.SelectionTabs = [24, 64, 96];
                SetNativeParagraph(native, ParagraphWithTabs(TabMask,
                    [PixelToTwip(24), PixelToTwip(64), PixelToTwip(96)]));

                Assert.Equal(ReadNativeTabs(native), ReadNativeTabs(rich.Handle));
                Assert.Equal(new[] { PixelToTwip(24), PixelToTwip(64), PixelToTwip(96) }, ReadNativeTabs(native));
            }
            finally { Assert.True(DestroyWindow(native)); }
        });

    private static byte[] EmptyParagraph()
    {
        byte[] bytes = new byte[ParagraphSize];
        WriteInt32(bytes, 0, ParagraphSize);
        return bytes;
    }

    private static byte[] ParagraphWithTabs(int mask, int[] tabs)
    {
        Assert.InRange(tabs.Length, 0, 32);
        byte[] bytes = EmptyParagraph();
        WriteInt32(bytes, 4, mask);
        WriteInt16(bytes, 26, (short)tabs.Length);
        for (int index = 0; index < tabs.Length; index++) { WriteInt32(bytes, 28 + 4 * index, tabs[index]); }
        return bytes;
    }

    private static void WriteInt32(byte[] bytes, int offset, int value)
        => BitConverter.GetBytes(value).CopyTo(bytes, offset);

    private static void WriteInt16(byte[] bytes, int offset, short value)
        => BitConverter.GetBytes(value).CopyTo(bytes, offset);

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

    private static byte[] ReadParagraph(nint handle)
    {
        nint paragraph = Marshal.AllocHGlobal(ParagraphSize);
        try
        {
            byte[] bytes = EmptyParagraph();
            Marshal.Copy(bytes, 0, paragraph, ParagraphSize);
            _ = SendMessageW(handle, EmGetParaFormat, 0, paragraph);
            Marshal.Copy(paragraph, bytes, 0, ParagraphSize);
            return bytes;
        }
        finally { Marshal.FreeHGlobal(paragraph); }
    }

    private static int[] ReadNativeTabs(nint handle)
    {
        byte[] bytes = ReadParagraph(handle);
        short count = BitConverter.ToInt16(bytes, 26);
        Assert.InRange(count, (short)0, (short)32);
        int[] tabs = new int[count];
        for (int index = 0; index < count; index++) { tabs[index] = BitConverter.ToInt32(bytes, 28 + 4 * index); }
        return tabs;
    }

    private static void SetNativeParagraph(nint handle, byte[] bytes)
    {
        Assert.Equal(ParagraphSize, bytes.Length);
        nint paragraph = Marshal.AllocHGlobal(ParagraphSize);
        try
        {
            Marshal.Copy(bytes, 0, paragraph, ParagraphSize);
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "RichTextBox paragraph tabs test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private readonly record struct ParagraphMessage(int Message, nint WParam, byte[] Bytes);

    private sealed class TabsRichTextBox : RichTextBox
    {
        internal bool InterceptParagraph { get; set; }
        internal byte[] ReturnedParagraph { get; set; } = EmptyParagraph();
        internal List<ParagraphMessage> ParagraphMessages { get; } = [];
        internal int CreatedCount { get; private set; }
        internal bool FailCreation { get; set; }
        internal InvalidOperationException CreationFailure { get; } = new("Selection tabs creation failure");

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
                    Marshal.Copy(ReturnedParagraph, 0, message.LParam, ParagraphSize);
                }
                // The public accessors use the filled structure and ignore the
                // message return, including an unsuccessful native SET result.
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

    [DllImport("user32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
    private static extern nint CreateWindowExW(uint extendedStyle, string className, string text,
        uint style, int x, int y, int width, int height, nint parent, nint menu, nint instance, nint parameter);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint handle);
}
