using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class RichTextBoxSelectionColorTests
{
    private const int EmGetCharFormat = 0x043A;
    private const int EmSetCharFormat = 0x0444;
    private const int EmStreamIn = 0x0449;
    private const int CharacterFormatSize = 116;
    private const uint ColorMask = 0x40000000;
    private const uint BackColorMask = 0x04000000;
    private const int HandleCreatedMarker = -100;

    [Theory]
    [InlineData(nameof(RichTextBox.SelectionColor))]
    [InlineData(nameof(RichTextBox.SelectionBackColor))]
    public void ColorsDeclareStandardNonvirtualHiddenProperties(string name)
    {
        PropertyInfo property = typeof(RichTextBox).GetProperty(name,
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)!;
        Assert.NotNull(property);
        Assert.Equal(typeof(Color), property.PropertyType);
        Assert.False(property.GetMethod!.IsVirtual);
        Assert.False(property.SetMethod!.IsVirtual);
        Assert.False(property.GetCustomAttribute<BrowsableAttribute>()!.Browsable);
        Assert.Equal(DesignerSerializationVisibility.Hidden,
            property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>()!.Visibility);
        Assert.Null(property.GetCustomAttribute<DefaultValueAttribute>());
    }

    [Fact]
    public void DefaultBackgroundIsEmptyWithoutCreatingAWindow()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox();
            Assert.Equal(Color.Empty, rich.SelectionBackColor);
            Assert.False(rich.IsHandleCreated);
            Assert.Equal(0, rich.CreatedCount);
        });

    [Theory]
    [InlineData(0x00112233)]
    [InlineData(-16711165)]
    public void BackgroundCachesTheOriginalColorWithoutCreatingAWindow(int argb)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox();
            Color color = Color.FromArgb(argb);
            rich.SelectionBackColor = color;
            Assert.Equal(color, rich.SelectionBackColor);
            Assert.False(rich.IsHandleCreated);
            rich.SelectionBackColor = Color.Empty;
            Assert.Equal(Color.Empty, rich.SelectionBackColor);
            Assert.False(rich.IsHandleCreated);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ForegroundAccessCreatesARealRichEdit(bool write)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox();
            if (write) { rich.SelectionColor = Color.Crimson; }
            else { _ = rich.SelectionColor; }
            Assert.True(rich.IsHandleCreated);
            Assert.Equal(1, rich.CreatedCount);
            Assert.True(IsWindow(rich.Handle));
            Assert.Equal(CharacterFormatSize, ReadFormat(rich.Handle).Bytes.Length);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ForegroundAccessRetainsTheOriginalCreationExceptionAndCanRetry(bool write)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { FailCreation = true };
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
            {
                if (write) { rich.SelectionColor = Color.Red; }
                else { _ = rich.SelectionColor; }
            });
            Assert.Same(rich.CreationFailure, exception);
            Assert.False(rich.IsHandleCreated);
            rich.FailCreation = false;
            rich.SelectionColor = Color.Red;
            AssertRgb(Color.Red, rich.SelectionColor);
        });

    [Fact]
    public void ForegroundGetterUsesTheColorMaskAndNotAnAutomaticColorEffect()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox();
            _ = rich.Handle;
            rich.SyntheticGet = MakeFormat(0, 0x40000000, 0x00332211, 0);
            rich.Capture = true;
            Assert.Equal(Color.Empty, rich.SelectionColor);
            FormatRequest request = Assert.Single(rich.Requests);
            Assert.Equal(EmGetCharFormat, request.Message);
            Assert.Equal((nint)1, request.Range);
            Assert.Equal(CharacterFormatSize, BitConverter.ToInt32(request.Bytes, 0));
        });

    [Fact]
    public void ForegroundGetterConvertsOleSystemColors()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox();
            _ = rich.Handle;
            rich.SyntheticGet = MakeFormat(ColorMask, 0, unchecked((int)0x80000005), 0);
            rich.Capture = true;
            Assert.Equal(SystemColors.Window, rich.SelectionColor);
        });

    [Theory]
    [InlineData(true, 0)]
    [InlineData(false, 0)]
    [InlineData(false, 0x00112233)]
    [InlineData(false, -16711165)]
    public void ForegroundSetterQueriesAndPreservesTheNativeStructureExceptItsColorFields(
        bool empty, int argb)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox();
            _ = rich.Handle;
            byte[] initial = MakeFormat(0xFFFFFFFF, 0x7FFFFFFF, 0x00101010, 0x00202020);
            BitConverter.GetBytes(280).CopyTo(initial, 12);
            BitConverter.GetBytes((short)711).CopyTo(initial, 90);
            rich.SyntheticGet = initial;
            rich.SuppressSet = true;
            rich.Capture = true;
            Color color = empty ? Color.Empty : Color.FromArgb(argb);

            rich.SelectionColor = color;

            Assert.Equal(2, rich.Requests.Count);
            Assert.Equal(EmGetCharFormat, rich.Requests[0].Message);
            FormatRequest submission = rich.Requests[1];
            Assert.Equal(EmSetCharFormat, submission.Message);
            Assert.Equal((nint)1, submission.Range);
            byte[] expected = (byte[])initial.Clone();
            BitConverter.GetBytes(ColorMask).CopyTo(expected, 4);
            BitConverter.GetBytes(0).CopyTo(expected, 8);
            BitConverter.GetBytes(ColorTranslator.ToWin32(color)).CopyTo(expected, 20);
            Assert.Equal(expected, submission.Bytes);
        });

    [Fact]
    public void ForegroundSameValueStillFormatsTheNewSelection()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Text = "firstsecond" };
            _ = rich.Handle;
            rich.Select(0, 5);
            rich.SelectionColor = Color.Red;
            rich.Select(5, 6);
            rich.SelectionColor = Color.Red;
            AssertRgb(Color.Red, rich.SelectionColor);
            rich.Select(0, 11);
            AssertRgb(Color.Red, rich.SelectionColor);
        });

    [Fact]
    public void ForegroundReadsTheLatestNativeColorAndReportsMixedSelections()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Text = "abcdef" };
            _ = rich.Handle;
            rich.Select(0, 3);
            SetFormat(rich.Handle, ColorMask, 0, 0x000000FF, 0);
            AssertRgb(Color.Red, rich.SelectionColor);
            rich.Select(3, 3);
            SetFormat(rich.Handle, ColorMask, 0, 0x00FF0000, 0);
            AssertRgb(Color.Blue, rich.SelectionColor);
            rich.Select(0, 6);
            Assert.Equal(Color.Empty, rich.SelectionColor);
            rich.Select(0, 3);
            AssertRgb(Color.Red, rich.SelectionColor);
        });

    [Fact]
    public void ForegroundChangesOnlyTheSelectedCharactersAndPreservesOtherFormatting()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox();
            rich.Rtf = @"{\rtf1\ansi\f0\fs28\b\i abcdef}";
            rich.Select(1, 3);
            NativeFormat before = ReadFormat(rich.Handle);
            rich.SelectionColor = Color.FromArgb(17, 34, 51);
            NativeFormat after = ReadFormat(rich.Handle);
            Assert.Equal(before.Effects & 3u, after.Effects & 3u);
            Assert.Equal(before.Height, after.Height);
            Assert.Equal(0x00332211, after.TextColor);
            Assert.Equal("abcdef", rich.Text);
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            rich.Select(0, 1);
            Assert.NotEqual(0x00332211, ReadFormat(rich.Handle).TextColor);
        });

    [Theory]
    [InlineData(0u, 0u, 0, true)]
    [InlineData(0u, BackColorMask, 0x00332211, false)]
    [InlineData(BackColorMask, BackColorMask, 0x00332211, false)]
    public void BackgroundGetterPrioritizesAutomaticColorBeforeTheValidityMask(
        uint mask, uint effects, int background, bool expectedEmpty)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { BackColor = Color.Aquamarine };
            _ = rich.Handle;
            rich.SyntheticGet = MakeFormat(mask, effects, 0, background);
            rich.Capture = true;
            Color expected = expectedEmpty ? Color.Empty : rich.BackColor;
            Assert.Equal(expected, rich.SelectionBackColor);
            FormatRequest request = Assert.Single(rich.Requests);
            Assert.Equal(EmGetCharFormat, request.Message);
            Assert.Equal((nint)1, request.Range);
        });

    [Fact]
    public void BackgroundGetterConvertsOleSystemColorsWhenTheMaskIsValid()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox();
            _ = rich.Handle;
            rich.SyntheticGet = MakeFormat(BackColorMask, 0, 0, unchecked((int)0x80000008));
            rich.Capture = true;
            Assert.Equal(SystemColors.WindowText, rich.SelectionBackColor);
        });

    [Theory]
    [InlineData(true, 0)]
    [InlineData(false, 0x00112233)]
    [InlineData(false, -16711165)]
    public void BackgroundSetterSubmitsAFreshStructureWithoutAReadOrSameValueShortcut(
        bool empty, int argb)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox();
            _ = rich.Handle;
            rich.SuppressSet = true;
            rich.Capture = true;
            Color color = empty ? Color.Empty : Color.FromArgb(argb);
            rich.SelectionBackColor = color;
            rich.SelectionBackColor = color;
            Assert.Equal(2, rich.Requests.Count);
            byte[] expected = MakeFormat(empty ? 0 : BackColorMask,
                empty ? BackColorMask : 0, 0, empty ? 0 : ColorTranslator.ToWin32(color));
            Assert.All(rich.Requests, request =>
            {
                Assert.Equal(EmSetCharFormat, request.Message);
                Assert.Equal((nint)1, request.Range);
                Assert.Equal(expected, request.Bytes);
            });
        });

    [Fact]
    public void BackgroundSameCachedValueIsAppliedToAnotherSelection()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Text = "abcdef" };
            _ = rich.Handle;
            rich.Select(0, 3);
            rich.SelectionBackColor = Color.Yellow;
            rich.Select(3, 3);
            rich.SelectionBackColor = Color.Yellow;
            rich.Select(0, 6);
            AssertRgb(Color.Yellow, rich.SelectionBackColor);
            Assert.Equal(ColorTranslator.ToWin32(Color.Yellow), ReadFormat(rich.Handle).BackColor);
        });

    [Fact]
    public void BackgroundReadsTheLatestNativeColorAndReportsMixedSelections()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Text = "abcdef" };
            _ = rich.Handle;
            rich.Select(0, 3);
            SetFormat(rich.Handle, BackColorMask, 0, 0, 0x0000FFFF);
            AssertRgb(Color.Yellow, rich.SelectionBackColor);
            rich.Select(3, 3);
            SetFormat(rich.Handle, BackColorMask, 0, 0, 0x0000FF00);
            AssertRgb(Color.Lime, rich.SelectionBackColor);
            rich.Select(0, 6);
            Assert.Equal(Color.Empty, rich.SelectionBackColor);
        });

    [Fact]
    public void BackgroundSetterPreservesForegroundBoldAndFontSize()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox();
            rich.Rtf = @"{\rtf1\ansi{\colortbl ;\red255\green0\blue0;}\fs30\b\cf1 abcdef}";
            rich.Select(1, 3);
            NativeFormat before = ReadFormat(rich.Handle);
            rich.SelectionBackColor = Color.Yellow;
            NativeFormat after = ReadFormat(rich.Handle);
            Assert.Equal(before.TextColor, after.TextColor);
            Assert.Equal(before.Height, after.Height);
            Assert.Equal(before.Effects & 1u, after.Effects & 1u);
            Assert.Equal(0x0000FFFF, after.BackColor);
            Assert.Equal("abcdef", rich.Text);
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
        });

    [Fact]
    public void CachedBackgroundIsReplayedBeforeHandleCreatedAndPlainTextStreaming()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Text = "cached", SelectionBackColor = Color.Yellow };
            Assert.False(rich.IsHandleCreated);
            rich.Capture = true;
            rich.HandleCreated += (_, _) => rich.Messages.Add(HandleCreatedMarker);
            _ = rich.Handle;

            int submission = rich.Messages.IndexOf(EmSetCharFormat);
            Assert.True(submission >= 0);
            Assert.True(submission < rich.Messages.IndexOf(HandleCreatedMarker));
            Assert.True(submission < rich.Messages.IndexOf(EmStreamIn));
            Assert.Contains(rich.Requests, request => request.Message == EmSetCharFormat
                && request.Range == 1 && BitConverter.ToUInt32(request.Bytes, 4) == BackColorMask
                && BitConverter.ToInt32(request.Bytes, 96) == 0x0000FFFF);
            Assert.Equal("cached", rich.Text);
        });

    [Fact]
    public void LoadingRtfTakesPrecedenceOverTheCachedBackgroundColor()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { SelectionBackColor = Color.Yellow };
            rich.Rtf = @"{\rtf1\ansi{\colortbl ;\red0\green255\blue0;}\highlight1 formatted}";
            rich.SelectAll();
            Assert.Equal("formatted", rich.Text);
            AssertRgb(Color.Lime, rich.SelectionBackColor);
        });

    [Fact]
    public void RecreatingTheHandleRetainsRealForegroundAndBackgroundFormatting()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Text = "abcdef" };
            _ = rich.Handle;
            rich.Select(1, 3);
            rich.SelectionColor = Color.Crimson;
            rich.SelectionBackColor = Color.Yellow;
            NativeFormat expected = ReadFormat(rich.Handle);
            rich.RecreateNow();
            Assert.Equal(2, rich.CreatedCount);
            Assert.Equal("abcdef", rich.Text);
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            NativeFormat actual = ReadFormat(rich.Handle);
            Assert.Equal(expected.TextColor, actual.TextColor);
            Assert.Equal(expected.BackColor, actual.BackColor);
            AssertRgb(Color.Crimson, rich.SelectionColor);
            AssertRgb(Color.Yellow, rich.SelectionBackColor);
        });

    [Fact]
    public void EmptyBackgroundRequestMatchesAnIndependentRichEditWindow()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Text = "abcdef" };
            _ = rich.Handle;
            nint native = CreateWindowExW(0, "RICHEDIT50W", "abcdef", unchecked((int)0x80000004),
                0, 0, 100, 96, 0, 0, 0, 0);
            Assert.NotEqual((nint)0, native);
            try
            {
                rich.Select(0, 3);
                SelectNative(native, 0, 3);
                SetFormat(native, BackColorMask, 0, 0, 0x0000FFFF);
                rich.SelectionBackColor = Color.Yellow;
                SetFormat(native, 0, BackColorMask, 0, 0);
                rich.SelectionBackColor = Color.Empty;
                NativeFormat expected = ReadFormat(native);
                NativeFormat actual = ReadFormat(rich.Handle);
                Assert.Equal(expected.BackColor, actual.BackColor);
                Assert.Equal(expected.Effects & BackColorMask, actual.Effects & BackColorMask);
                Assert.Equal(expected.Mask & BackColorMask, actual.Mask & BackColorMask);
            }
            finally { Assert.True(DestroyWindow(native)); }
        });

    [Fact]
    public void ColorGettersLeaveTextSelectionModifiedUndoAndNotificationsUnchanged()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new FormatRichTextBox { Text = "abcdef", MaxLength = 3 };
            form.Controls.Add(rich);
            _ = form.Handle;
            _ = rich.Handle;
            rich.Select(1, 2);
            rich.SelectedText = "xyz";
            rich.Select(2, 1);
            rich.Modified = true;
            string text = rich.Text;
            bool undo = rich.CanUndo;
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;

            _ = rich.SelectionColor;
            _ = rich.SelectionBackColor;

            Assert.Equal(text, rich.Text);
            Assert.Equal(2, rich.SelectionStart);
            Assert.Equal(1, rich.SelectionLength);
            Assert.True(rich.Modified);
            Assert.Equal(undo, rich.CanUndo);
            Assert.Equal(3, rich.MaxLength);
            Assert.Equal((nint)3, SendMessageW(rich.Handle, 0x00D5, 0, 0));
            Assert.Equal(0, changes);
        });

    private static byte[] MakeFormat(uint mask, uint effects, int foreground, int background)
    {
        var bytes = new byte[CharacterFormatSize];
        BitConverter.GetBytes(CharacterFormatSize).CopyTo(bytes, 0);
        BitConverter.GetBytes(mask).CopyTo(bytes, 4);
        BitConverter.GetBytes(effects).CopyTo(bytes, 8);
        BitConverter.GetBytes(foreground).CopyTo(bytes, 20);
        BitConverter.GetBytes(background).CopyTo(bytes, 96);
        return bytes;
    }

    private static NativeFormat ReadFormat(nint handle)
    {
        nint buffer = Marshal.AllocHGlobal(CharacterFormatSize);
        try
        {
            Marshal.Copy(MakeFormat(0, 0, 0, 0), 0, buffer, CharacterFormatSize);
            _ = SendMessageW(handle, EmGetCharFormat, 1, buffer);
            var bytes = new byte[CharacterFormatSize];
            Marshal.Copy(buffer, bytes, 0, bytes.Length);
            return new NativeFormat(bytes);
        }
        finally { Marshal.FreeHGlobal(buffer); }
    }

    private static void SetFormat(nint handle, uint mask, uint effects, int foreground, int background)
    {
        nint buffer = Marshal.AllocHGlobal(CharacterFormatSize);
        try
        {
            Marshal.Copy(MakeFormat(mask, effects, foreground, background), 0, buffer, CharacterFormatSize);
            _ = SendMessageW(handle, EmSetCharFormat, 1, buffer);
        }
        finally { Marshal.FreeHGlobal(buffer); }
    }

    private static void SelectNative(nint handle, int start, int end)
    {
        nint buffer = Marshal.AllocHGlobal(8);
        try
        {
            Marshal.WriteInt32(buffer, start);
            Marshal.WriteInt32(buffer, 4, end);
            _ = SendMessageW(handle, 0x0437, 0, buffer);
        }
        finally { Marshal.FreeHGlobal(buffer); }
    }

    private static void AssertRgb(Color expected, Color actual)
        => Assert.Equal(Color.FromArgb(expected.R, expected.G, expected.B).ToArgb(), actual.ToArgb());

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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "RichTextBox selection color test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private readonly record struct NativeFormat(byte[] Bytes)
    {
        internal uint Mask => BitConverter.ToUInt32(Bytes, 4);
        internal uint Effects => BitConverter.ToUInt32(Bytes, 8);
        internal int Height => BitConverter.ToInt32(Bytes, 12);
        internal int TextColor => BitConverter.ToInt32(Bytes, 20);
        internal int BackColor => BitConverter.ToInt32(Bytes, 96);
    }

    private readonly record struct FormatRequest(int Message, nint Range, byte[] Bytes);

    private sealed class FormatRichTextBox : RichTextBox
    {
        internal bool Capture { get; set; }
        internal byte[]? SyntheticGet { get; set; }
        internal bool SuppressSet { get; set; }
        internal List<FormatRequest> Requests { get; } = [];
        internal List<int> Messages { get; } = [];
        internal int CreatedCount { get; private set; }
        internal bool FailCreation { get; set; }
        internal InvalidOperationException CreationFailure { get; } = new("selection color creation failure");

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
            if (Capture && message.Msg is EmGetCharFormat or EmSetCharFormat or EmStreamIn)
            {
                Messages.Add(message.Msg);
                if (message.Msg is EmGetCharFormat or EmSetCharFormat)
                {
                    int size = Math.Clamp(Marshal.ReadInt32(message.LParam), 0, CharacterFormatSize);
                    var bytes = new byte[size];
                    Marshal.Copy(message.LParam, bytes, 0, size);
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
            }
            base.WndProc(ref message);
        }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint handle, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint CreateWindowExW(int extendedStyle, string className, string title,
        int style, int x, int y, int width, int height, nint parent, nint menu, nint instance, nint parameter);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint handle);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint handle);
}
