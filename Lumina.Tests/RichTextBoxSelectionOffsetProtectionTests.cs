using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class RichTextBoxSelectionOffsetProtectionTests
{
    private const int EmGetCharFormat = 0x043A;
    private const int EmSetCharFormat = 0x0444;
    private const int CharacterFormatSize = 116;
    private const uint OffsetMask = 0x10000000;
    private const uint ProtectedMask = 0x10;

    [Theory]
    [InlineData(nameof(RichTextBox.SelectionCharOffset))]
    [InlineData(nameof(RichTextBox.SelectionProtected))]
    public void PropertiesDeclareTheStandardNonvirtualHiddenContract(string name)
    {
        PropertyInfo property = typeof(RichTextBox).GetProperty(name,
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)!;
        Assert.NotNull(property);
        bool offset = name == nameof(RichTextBox.SelectionCharOffset);
        Assert.Equal(offset ? typeof(int) : typeof(bool), property.PropertyType);
        Assert.False(property.GetMethod!.IsVirtual);
        Assert.False(property.SetMethod!.IsVirtual);
        Assert.Equal(offset ? (object)0 : false, property.GetCustomAttribute<DefaultValueAttribute>()!.Value);
        Assert.False(property.GetCustomAttribute<BrowsableAttribute>()!.Browsable);
        Assert.Equal(DesignerSerializationVisibility.Hidden,
            property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>()!.Visibility);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(true, true)]
    public void ValidAccessCreatesARealHandleIncludingDefaultAssignments(bool offset, bool write)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox();
            Access(rich, offset, write);
            Assert.True(rich.IsHandleCreated);
            Assert.Equal(1, rich.CreatedCount);
            Assert.True(IsWindow(rich.Handle));
            Assert.Equal(0, rich.SelectionCharOffset);
            Assert.False(rich.SelectionProtected);
        });

    [Theory]
    [InlineData(false, false)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(true, true)]
    public void AccessPropagatesTheOriginalCreationFailureAndCanRetry(bool offset, bool write)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { FailCreation = true };
            InvalidOperationException error = Assert.Throws<InvalidOperationException>(
                () => Access(rich, offset, write));
            Assert.Same(rich.CreationFailure, error);
            Assert.False(rich.IsHandleCreated);
            Assert.Equal(0, rich.CreatedCount);
            rich.FailCreation = false;
            Access(rich, offset, write);
            Assert.True(rich.IsHandleCreated);
            Assert.Equal(1, rich.CreatedCount);
        });

    [Theory]
    [InlineData(-2001)]
    [InlineData(2001)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void OffsetValidationPrecedesHandleCreationAndLeavesCachedTextAlone(int value)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Text = "cached text", FailCreation = true };
            ArgumentOutOfRangeException error = Assert.Throws<ArgumentOutOfRangeException>(
                () => rich.SelectionCharOffset = value);
            Assert.Equal("value", error.ParamName);
            Assert.Equal(value, error.ActualValue);
            Assert.False(rich.IsHandleCreated);
            Assert.Equal(0, rich.CreatedCount);
            Assert.Equal("cached text", rich.Text);
        });

    [Theory]
    [InlineData(-2000)]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2000)]
    public void OffsetSetterSendsOnlyOffsetWithScreenVerticalTwips(int value)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox();
            _ = rich.Handle;
            rich.Capture = true;
            rich.SuppressSet = true;
            rich.SelectionCharOffset = value;

            FormatRequest call = Assert.Single(rich.Requests);
            Assert.Equal(EmSetCharFormat, call.Message);
            Assert.Equal((nint)1, call.Range);
            Assert.Equal(MakeFormat(OffsetMask, 0, PixelToTwip(value)), call.Bytes);
        });

    [Theory]
    [InlineData(0, 31)]
    [InlineData(0, -31)]
    [InlineData(0, 1)]
    [InlineData(0, -1)]
    [InlineData(0x10000000, 1440)]
    public void OffsetGetterUsesTheNativeOffsetAndVerticalScreenDpiEvenWithoutItsMask(int mask, int twips)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox();
            _ = rich.Handle;
            rich.SyntheticGet = MakeFormat(unchecked((uint)mask), uint.MaxValue, twips);
            rich.Capture = true;

            Assert.Equal(TwipToPixel(twips), rich.SelectionCharOffset);

            FormatRequest call = Assert.Single(rich.Requests);
            Assert.Equal(EmGetCharFormat, call.Message);
            Assert.Equal((nint)1, call.Range);
            Assert.Equal(MakeFormat(0, 0, 0), call.Bytes);
        });

    [Theory]
    [InlineData(0, 0, false)]
    [InlineData(0, 0x10, false)]
    [InlineData(0x10, 0, false)]
    [InlineData(0x10, 0x10, true)]
    [InlineData(-1, -1, true)]
    public void ProtectionGetterRequiresBothAValidMaskAndTheProtectedEffect(int mask, int effects, bool expected)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox();
            _ = rich.Handle;
            rich.SyntheticGet = MakeFormat(unchecked((uint)mask), unchecked((uint)effects), 12345);
            rich.Capture = true;

            Assert.Equal(expected, rich.SelectionProtected);

            FormatRequest call = Assert.Single(rich.Requests);
            Assert.Equal(EmGetCharFormat, call.Message);
            Assert.Equal((nint)1, call.Range);
            Assert.Equal(MakeFormat(0, 0, 0), call.Bytes);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ProtectionSetterSendsANewZeroedStructureWithoutReadingTheSelection(bool value)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox();
            _ = rich.Handle;
            rich.Capture = true;
            rich.SuppressSet = true;
            rich.SelectionProtected = value;

            FormatRequest call = Assert.Single(rich.Requests);
            Assert.Equal(EmSetCharFormat, call.Message);
            Assert.Equal((nint)1, call.Range);
            Assert.Equal(MakeFormat(ProtectedMask, value ? ProtectedMask : 0, 0), call.Bytes);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void RepeatedAssignmentsStillSubmitTheNativeFormat(bool offset)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox();
            _ = rich.Handle;
            rich.Capture = true;
            rich.SuppressSet = true;
            Access(rich, offset, write: true);
            Access(rich, offset, write: true);

            Assert.Equal(2, rich.Requests.Count);
            Assert.All(rich.Requests, call => Assert.Equal(EmSetCharFormat, call.Message));
            Assert.Equal(rich.Requests[0].Bytes, rich.Requests[1].Bytes);
        });

    [Theory]
    [InlineData(-11)]
    [InlineData(0)]
    [InlineData(17)]
    public void NativeFormattingTargetsTheSelectedTextAndTheSameValueCanTargetAnotherSelection(int offset)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new FormatRichTextBox { Text = "first second third" };
            CreateHostedHandle(form, rich);
            rich.Select(0, 5);
            rich.SelectionCharOffset = offset;
            rich.SelectionProtected = true;
            NativeFormat first = ReadFormat(rich.Handle);
            Assert.Equal(PixelToTwip(offset), first.Offset);
            Assert.NotEqual(0u, first.Effects & ProtectedMask);

            rich.Select(6, 6);
            Assert.Equal(0, rich.SelectionCharOffset);
            Assert.False(rich.SelectionProtected);
            rich.SelectionCharOffset = offset;
            rich.SelectionProtected = true;
            Assert.Equal(PixelToTwip(offset), ReadFormat(rich.Handle).Offset);
            Assert.True(rich.SelectionProtected);

            rich.Select(13, 5);
            Assert.Equal(0, rich.SelectionCharOffset);
            Assert.False(rich.SelectionProtected);
            rich.Select(0, 5);
            rich.SelectionProtected = false;
            Assert.False(rich.SelectionProtected);
            Assert.Equal(PixelToTwip(offset), ReadFormat(rich.Handle).Offset);
        });

    [Fact]
    public void MixedProtectionReadsFalseAndCanBeReplacedAcrossTheWholeSelection()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Text = "abcdef" };
            _ = rich.Handle;
            rich.Select(0, 3);
            rich.SelectionProtected = true;
            rich.Select(0, 6);
            Assert.Equal(0u, ReadFormat(rich.Handle).Mask & ProtectedMask);
            Assert.False(rich.SelectionProtected);
            rich.SelectionProtected = true;
            Assert.True(rich.SelectionProtected);
            rich.SelectionProtected = false;
            Assert.False(rich.SelectionProtected);
        });

    [Fact]
    public void GettersReadTheLatestNativeFormattingRatherThanAnAssignedCache()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Text = "native state" };
            _ = rich.Handle;
            rich.Select(0, 6);
            rich.SelectionCharOffset = 1;
            rich.SelectionProtected = false;
            SetFormat(rich.Handle, OffsetMask | ProtectedMask, ProtectedMask, 720);
            Assert.Equal(TwipToPixel(720), rich.SelectionCharOffset);
            Assert.True(rich.SelectionProtected);
            SetFormat(rich.Handle, OffsetMask | ProtectedMask, 0, -720);
            Assert.Equal(TwipToPixel(-720), rich.SelectionCharOffset);
            Assert.False(rich.SelectionProtected);
        });

    [Fact]
    public void GettersPreserveTextSelectionConfiguredLimitModifiedUndoAndNotifications()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new FormatRichTextBox { Text = "abcdef", MaxLength = 3 };
            CreateHostedHandle(form, rich);
            rich.Select(1, 2);
            rich.SelectedText = "xyz";
            rich.Select(2, 1);
            rich.Modified = true;
            string text = rich.Text;
            bool undo = rich.CanUndo;
            int changes = 0;
            int modifiedChanges = 0;
            rich.TextChanged += (_, _) => changes++;
            rich.ModifiedChanged += (_, _) => modifiedChanges++;

            _ = rich.SelectionCharOffset;
            _ = rich.SelectionProtected;

            Assert.Equal(text, rich.Text);
            Assert.Equal(2, rich.SelectionStart);
            Assert.Equal(1, rich.SelectionLength);
            Assert.True(rich.Modified);
            Assert.Equal(undo, rich.CanUndo);
            Assert.Equal(3, rich.MaxLength);
            Assert.Equal((nint)3, SendMessageW(rich.Handle, 0x00D5, 0, 0));
            Assert.Equal(0, changes);
            Assert.Equal(0, modifiedChanges);
        });

    [Fact]
    public void SettersPreserveOtherCharacterAndParagraphFormattingAndTheSelection()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Rtf = "{\\rtf1\\ansi\\b\\i abcdef}" };
            rich.Select(1, 3);
            rich.SelectionColor = Color.DarkRed;
            rich.SelectionBackColor = Color.Gold;
            rich.SelectionAlignment = HorizontalAlignment.Center;
            NativeFormat before = ReadFormat(rich.Handle);
            string text = rich.Text;
            rich.SelectionCharOffset = 12;
            rich.SelectionProtected = true;
            NativeFormat after = ReadFormat(rich.Handle);

            Assert.Equal(before.Effects & ~ProtectedMask, after.Effects & ~ProtectedMask);
            Assert.Equal(before.Height, after.Height);
            Assert.Equal(before.TextColor, after.TextColor);
            Assert.Equal(before.BackColor, after.BackColor);
            Assert.Equal(HorizontalAlignment.Center, rich.SelectionAlignment);
            Assert.Equal(text, rich.Text);
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.True(rich.SelectionProtected);
            Assert.Equal(PixelToTwip(12), after.Offset);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void RtfHandleRecreationRetainsOffsetProtectionAndTheExistingState(bool modified)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new FormatRichTextBox { Text = "abcdef", MaxLength = 7 };
            CreateHostedHandle(form, rich);
            rich.Select(1, 3);
            // One physical inch is exactly 1440 twips at the current screen DPI,
            // so RTF half-point encoding cannot round this fixture's offset.
            rich.SelectionCharOffset = ReadVerticalDpi();
            rich.SelectionProtected = true;
            rich.Modified = modified;
            NativeFormat before = ReadFormat(rich.Handle);
            string text = rich.Text;

            rich.RecreateNow();

            Assert.Equal(before.Offset, ReadFormat(rich.Handle).Offset);
            Assert.True(rich.SelectionProtected);
            Assert.Equal(text, rich.Text);
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.Equal(modified, rich.Modified);
            Assert.Equal(7, rich.MaxLength);
            Assert.Equal((nint)7, SendMessageW(rich.Handle, 0x00D5, 0, 0));
            Assert.False(rich.CanUndo);
        });

    [Fact]
    public void EmptySelectionUsesTheCurrentInsertionFormat()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Text = "abc" };
            _ = rich.Handle;
            rich.Select(3, 0);
            rich.SelectionCharOffset = -12;
            rich.SelectionProtected = true;
            Assert.Equal(PixelToTwip(-12), ReadFormat(rich.Handle).Offset);
            Assert.True(rich.SelectionProtected);
            Assert.Equal("abc", rich.Text);
            Assert.Equal(3, rich.SelectionStart);
            Assert.Equal(0, rich.SelectionLength);
        });

    private static void Access(RichTextBox rich, bool offset, bool write)
    {
        if (offset)
        {
            if (write) { rich.SelectionCharOffset = 0; }
            else { _ = rich.SelectionCharOffset; }
        }
        else
        {
            if (write) { rich.SelectionProtected = false; }
            else { _ = rich.SelectionProtected; }
        }
    }

    private static void CreateHostedHandle(Form form, RichTextBox rich)
    {
        form.Controls.Add(rich);
        _ = form.Handle;
        _ = rich.Handle;
    }

    private static byte[] MakeFormat(uint mask, uint effects, int offset)
    {
        var bytes = new byte[CharacterFormatSize];
        BitConverter.GetBytes(CharacterFormatSize).CopyTo(bytes, 0);
        BitConverter.GetBytes(mask).CopyTo(bytes, 4);
        BitConverter.GetBytes(effects).CopyTo(bytes, 8);
        BitConverter.GetBytes(offset).CopyTo(bytes, 16);
        return bytes;
    }

    private static NativeFormat ReadFormat(nint handle)
    {
        nint buffer = Marshal.AllocHGlobal(CharacterFormatSize);
        try
        {
            Marshal.Copy(MakeFormat(0, 0, 0), 0, buffer, CharacterFormatSize);
            _ = SendMessageW(handle, EmGetCharFormat, 1, buffer);
            var bytes = new byte[CharacterFormatSize];
            Marshal.Copy(buffer, bytes, 0, bytes.Length);
            return new NativeFormat(bytes);
        }
        finally { Marshal.FreeHGlobal(buffer); }
    }

    private static void SetFormat(nint handle, uint mask, uint effects, int offset)
    {
        nint buffer = Marshal.AllocHGlobal(CharacterFormatSize);
        try
        {
            Marshal.Copy(MakeFormat(mask, effects, offset), 0, buffer, CharacterFormatSize);
            _ = SendMessageW(handle, EmSetCharFormat, 1, buffer);
        }
        finally { Marshal.FreeHGlobal(buffer); }
    }

    private static int ReadVerticalDpi()
    {
        nint dc = GetDC(0);
        Assert.NotEqual((nint)0, dc);
        try
        {
            int dpi = GetDeviceCaps(dc, 90);
            Assert.True(dpi > 0);
            return dpi;
        }
        finally { _ = ReleaseDC(0, dc); }
    }

    private static int PixelToTwip(int value)
        => (int)((((double)value) / ReadVerticalDpi()) * 72.0 * 20.0);

    private static int TwipToPixel(int value)
        => (int)(((value / 20.0) / 72.0) * ReadVerticalDpi());

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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "RichTextBox offset/protection test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private readonly record struct NativeFormat(byte[] Bytes)
    {
        internal uint Mask => BitConverter.ToUInt32(Bytes, 4);
        internal uint Effects => BitConverter.ToUInt32(Bytes, 8);
        internal int Height => BitConverter.ToInt32(Bytes, 12);
        internal int Offset => BitConverter.ToInt32(Bytes, 16);
        internal int TextColor => BitConverter.ToInt32(Bytes, 20);
        internal int BackColor => BitConverter.ToInt32(Bytes, 96);
    }

    private readonly record struct FormatRequest(int Message, nint Range, byte[] Bytes);

    private sealed class FormatRichTextBox : RichTextBox
    {
        internal bool Capture { get; set; }
        internal bool SuppressSet { get; set; }
        internal byte[]? SyntheticGet { get; set; }
        internal List<FormatRequest> Requests { get; } = [];
        internal int CreatedCount { get; private set; }
        internal bool FailCreation { get; set; }
        internal InvalidOperationException CreationFailure { get; } = new("offset/protection creation failure");

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
            if (Capture && message.Msg is EmGetCharFormat or EmSetCharFormat)
            {
                var bytes = new byte[CharacterFormatSize];
                Marshal.Copy(message.LParam, bytes, 0, CharacterFormatSize);
                Requests.Add(new FormatRequest(message.Msg, message.WParam, bytes));
                if (message.Msg == EmGetCharFormat && SyntheticGet is not null)
                {
                    Marshal.Copy(SyntheticGet, 0, message.LParam, SyntheticGet.Length);
                    message.Result = 0;
                    return;
                }
                if (message.Msg == EmSetCharFormat && SuppressSet)
                {
                    message.Result = 0;
                    return;
                }
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

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint handle);
}
