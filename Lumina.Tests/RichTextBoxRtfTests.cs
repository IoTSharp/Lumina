using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class RichTextBoxRtfTests
{
    private const int WmSetText = 0x000C;
    private const int WmClear = 0x0303;
    private const int EmSetModify = 0x00B9;
    private const int EmGetLineCount = 0x00BA;
    private const int EmGetLimitText = 0x00D5;
    private const int EmExLimitText = 0x0435;
    private const int EmExSetSel = 0x0437;
    private const int EmGetCharFormat = 0x043A;
    private const int EmStreamIn = 0x0449;
    private const int EmStreamOut = 0x044A;
    private const string BoldDocument = @"{\rtf1\ansi\b bold\b0  plain}";

    [Fact]
    public void RtfDeclaresTheStandardNullableNonvirtualHiddenProperty()
    {
        PropertyInfo property = typeof(RichTextBox).GetProperty(nameof(RichTextBox.Rtf),
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)!;
        Assert.NotNull(property);
        Assert.Equal(typeof(string), property.PropertyType);
        Assert.False(property.GetMethod!.IsVirtual);
        Assert.False(property.SetMethod!.IsVirtual);
        Assert.False(property.GetCustomAttribute<BrowsableAttribute>()!.Browsable);
        Assert.Equal(DesignerSerializationVisibility.Hidden,
            property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>()!.Visibility);
        Assert.Equal(RefreshProperties.All,
            property.GetCustomAttribute<RefreshPropertiesAttribute>()!.RefreshProperties);
        var nullability = new NullabilityInfoContext().Create(property);
        Assert.Equal(NullabilityState.Nullable, nullability.ReadState);
        Assert.Equal(NullabilityState.Nullable, nullability.WriteState);
    }

    [Fact]
    public void SelectedRtfDeclaresTheStandardNullAndSerializationContract()
    {
        PropertyInfo property = typeof(RichTextBox).GetProperty(nameof(RichTextBox.SelectedRtf),
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)!;
        Assert.NotNull(property);
        Assert.Equal(typeof(string), property.PropertyType);
        Assert.False(property.GetMethod!.IsVirtual);
        Assert.False(property.SetMethod!.IsVirtual);
        Assert.Equal(string.Empty, property.GetCustomAttribute<DefaultValueAttribute>()!.Value);
        Assert.False(property.GetCustomAttribute<BrowsableAttribute>()!.Browsable);
        Assert.Equal(DesignerSerializationVisibility.Hidden,
            property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>()!.Visibility);
        Assert.NotNull(property.SetMethod.GetParameters()[0].GetCustomAttribute<AllowNullAttribute>());
    }

    [Fact]
    public void DefaultRtfIsNullWithoutCreatingAHandleOrNotifying()
        => OnSta(() =>
        {
            using var rich = new RtfRichTextBox();
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;
            Assert.Null(rich.Rtf);
            Assert.False(rich.IsHandleCreated);
            Assert.Equal(0, changes);
            Assert.Empty(rich.Submissions);
        });

    [Fact]
    public void ReadingRtfConvertsCachedPlainTextThroughTheRichEdit()
        => OnSta(() =>
        {
            using var rich = new RtfRichTextBox { Text = "plain 中😀" };
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;
            Assert.False(rich.IsHandleCreated);

            string rtf = rich.Rtf!;

            Assert.True(rich.IsHandleCreated);
            Assert.StartsWith(@"{\rtf", rtf);
            Assert.False(rtf.EndsWith('\0'));
            Assert.Equal("plain 中😀", rich.Text);
            using var copy = new RichTextBox { Rtf = rtf };
            Assert.Equal("plain 中😀", copy.Text);
            Assert.Equal(0, changes);
        });

    [Fact]
    public void RtfRoundTripRetainsRealNativeBoldAndPlainRuns()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new RtfRichTextBox();
            nint handle = CreateHostedHandle(form, rich);
            rich.Rtf = BoldDocument;
            Assert.Equal("bold plain", rich.Text);
            rich.Select(0, 4);
            Assert.True(IsSelectionBold(handle));
            rich.Select(5, 5);
            Assert.False(IsSelectionBold(handle));
            string saved = rich.Rtf!;
            Assert.Equal(5, rich.SelectionStart);
            Assert.Equal(5, rich.SelectionLength);

            using var restored = new RichTextBox { Rtf = saved };
            Assert.Equal("bold plain", restored.Text);
            restored.Select(0, 4);
            Assert.True(IsSelectionBold(restored.Handle));
            restored.Select(5, 5);
            Assert.False(IsSelectionBold(restored.Handle));
        });

    [Fact]
    public void RtfReadsUnicodeEscapesSurrogatePairsAndParagraphsAsLogicalText()
        => OnSta(() =>
        {
            using var rich = new RichTextBox
            {
                Rtf = @"{\rtf1\ansi\uc1 A\u20013?\u-10179?\u-8704?\par B}"
            };
            Assert.Equal("A中😀\nB", rich.Text);
            using var restored = new RichTextBox { Rtf = rich.Rtf };
            Assert.Equal("A中😀\nB", restored.Text);
        });

    [Fact]
    public void AssigningTheCurrentSerializedRtfDoesNotStreamOrNotify()
        => OnSta(() =>
        {
            using var rich = new RtfRichTextBox { Rtf = BoldDocument };
            string serialized = rich.Rtf!;
            rich.Select(1, 2);
            rich.Modified = false;
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;
            rich.Capture = true;

            rich.Rtf = serialized;

            Assert.DoesNotContain(rich.Submissions, item => item.Message == EmStreamIn);
            Assert.Equal(new[] { new Submission(EmStreamOut, 2, 0, 0) }, rich.Submissions);
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(2, rich.SelectionLength);
            Assert.False(rich.Modified);
            Assert.Equal(0, changes);
        });

    [Fact]
    public void RtfAssignmentReportsTheNativeChangeAndTheExplicitSetterNotification()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new RtfRichTextBox();
            _ = CreateHostedHandle(form, rich);
            var texts = new List<string>();
            rich.TextChanged += (_, _) => texts.Add(rich.Text);

            rich.Rtf = BoldDocument;

            Assert.Equal(new[] { "bold plain", "bold plain" }, texts);
        });

    [Fact]
    public void FullRtfUsesAnsiRichTextFlagsAndRestoresTheConfiguredInputLimit()
        => OnSta(() =>
        {
            using var rich = new RtfRichTextBox { MaxLength = 3 };
            nint handle = rich.Handle;
            rich.Capture = true;

            rich.Rtf = BoldDocument;

            Assert.Equal(new[]
            {
                new Submission(EmStreamOut, 2, 0, 0),
                new Submission(EmExSetSel, 0, 0, 0),
                new Submission(EmExLimitText, 0, int.MaxValue, 0),
                new Submission(EmStreamIn, 2, 0, 0),
                new Submission(EmExLimitText, 0, 3, 0),
                new Submission(EmSetModify, -1, 0, 0),
                new Submission(EmGetLineCount, 0, 0, 0)
            }, rich.Submissions);
            Assert.Equal("bold plain", rich.Text);
            Assert.Equal((nint)3, SendMessageW(handle, EmGetLimitText, 0, 0));
            Assert.True(rich.Modified);
            Assert.Equal(handle, rich.Handle);
        });

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void EmptyFullRtfUsesSetTextAndClearsTheDocument(string? value)
        => OnSta(() =>
        {
            using var rich = new RtfRichTextBox { Rtf = BoldDocument };
            rich.Capture = true;

            rich.Rtf = value;

            Assert.Equal(string.Empty, rich.Text);
            Assert.Equal(new[]
            {
                new Submission(EmStreamOut, 2, 0, 0),
                new Submission(WmSetText, 0, 0, 0)
            }, rich.Submissions);
        });

    [Theory]
    [InlineData("x", true)]
    [InlineData(@"{\rt", true)]
    [InlineData(@"{\RTF1 bad}", false)]
    [InlineData(@" {\rtf1 bad}", false)]
    [InlineData("\0ignored", true)]
    public void InvalidFullRtfClearsSelectionBeforeRejectingWithoutStreaming(string invalid, bool shortPrefix)
        => OnSta(() =>
        {
            using var rich = new RtfRichTextBox { Text = "abcdef", MaxLength = 3 };
            _ = rich.Handle;
            rich.Select(2, 3);
            rich.Capture = true;

            ArgumentException failure = Assert.Throws<ArgumentException>(() => rich.Rtf = invalid);
            if (shortPrefix) { Assert.IsType<EndOfStreamException>(failure.InnerException); }
            else { Assert.Null(failure.InnerException); }

            Assert.Equal("abcdef", rich.Text);
            Assert.Equal(0, rich.SelectionStart);
            Assert.Equal(0, rich.SelectionLength);
            Assert.Equal(new[]
            {
                new Submission(EmStreamOut, 2, 0, 0),
                new Submission(EmExSetSel, 0, 0, 0)
            }, rich.Submissions);
            Assert.Equal((nint)3, SendMessageW(rich.Handle, EmGetLimitText, 0, 0));
        });

    [Fact]
    public void RtfIgnoresTextFollowingTheFirstNullAfterACompleteDocument()
        => OnSta(() =>
        {
            using var rich = new RichTextBox { Rtf = BoldDocument + "\0not RTF" };
            Assert.Equal("bold plain", rich.Text);
            rich.Select(0, 4);
            Assert.True(IsSelectionBold(rich.Handle));
        });

    [Fact]
    public void SelectedRtfGetterCreatesAHandleAndReturnsTheNativeEmptySelection()
        => OnSta(() =>
        {
            using var rich = new RtfRichTextBox();
            Assert.False(rich.IsHandleCreated);
            rich.Capture = true;

            string selected = rich.SelectedRtf;

            Assert.True(rich.IsHandleCreated);
            Assert.Equal(new[] { new Submission(EmStreamOut, 0x8002, 0, 0) },
                rich.Submissions.Where(item => item.Message == EmStreamOut));
            // Verify the same real RichEdit can emit a full RTF document,
            // then independently establish that its empty selection emits no bytes.
            Assert.True(ReadNativeRtfByteCount(rich.Handle, 2) > 0);
            Assert.Equal(0, ReadNativeRtfByteCount(rich.Handle, 0x8002));
            Assert.Equal(string.Empty, selected);
        });

    [Fact]
    public void SelectedRtfExportsOnlyTheSelectedRunAndItsNativeFormat()
        => OnSta(() =>
        {
            using var rich = new RtfRichTextBox { Rtf = BoldDocument };
            rich.Select(0, 4);
            rich.Modified = false;
            rich.Capture = true;

            string selected = rich.SelectedRtf;

            Assert.Equal(new[] { new Submission(EmStreamOut, 0x8002, 0, 0) }, rich.Submissions);
            Assert.Equal(0, rich.SelectionStart);
            Assert.Equal(4, rich.SelectionLength);
            Assert.False(rich.Modified);
            using var restored = new RichTextBox { Rtf = selected };
            Assert.Equal("bold", restored.Text);
            restored.SelectAll();
            Assert.True(IsSelectionBold(restored.Handle));
        });

    [Fact]
    public void SelectedRtfReplacesOnlyTheRangeWithFormattingAndBypassesTheInputLimit()
        => OnSta(() =>
        {
            using var rich = new RtfRichTextBox { Text = "abcdef", MaxLength = 3 };
            nint handle = rich.Handle;
            rich.Select(2, 2);
            rich.Capture = true;

            rich.SelectedRtf = @"{\rtf1\ansi\b XYZZY}";

            Assert.Equal("abXYZZYef", rich.Text);
            Assert.Equal(new[]
            {
                new Submission(EmExLimitText, 0, int.MaxValue, 0),
                new Submission(EmStreamIn, 0x8002, 0, 0),
                new Submission(EmExLimitText, 0, 3, 0),
                new Submission(EmSetModify, -1, 0, 0),
                new Submission(EmGetLineCount, 0, 0, 0)
            }, rich.Submissions);
            Assert.Equal((nint)3, SendMessageW(handle, EmGetLimitText, 0, 0));
            Assert.True(rich.Modified);
            rich.Select(2, 5);
            Assert.True(IsSelectionBold(handle));
            Assert.Equal(handle, rich.Handle);
        });

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void EmptySelectedRtfUsesClearAndDeletesTheSelection(string? value)
        => OnSta(() =>
        {
            using var rich = new RtfRichTextBox { Text = "abcdef" };
            _ = rich.Handle;
            rich.Select(2, 2);
            rich.Capture = true;

            rich.SelectedRtf = value;

            Assert.Equal("abef", rich.Text);
            Assert.Equal(new[] { new Submission(WmClear, 0, 0, 0) }, rich.Submissions);
        });

    [Fact]
    public void ReadOnlyRejectsEmptySelectedRtfButAllowsProgrammaticRichReplacement()
        => OnSta(() =>
        {
            using var rich = new RichTextBox { Text = "abcdef", ReadOnly = true };
            _ = rich.Handle;
            rich.Select(2, 2);
            rich.SelectedRtf = string.Empty;
            Assert.Equal("abcdef", rich.Text);
            rich.SelectedRtf = @"{\rtf1\ansi\b XY}";
            Assert.Equal("abXYef", rich.Text);
            rich.Select(2, 2);
            Assert.True(IsSelectionBold(rich.Handle));
        });

    [Fact]
    public void InvalidSelectedRtfPreservesTheRangeAndDoesNotTemporarilyExpandItsLimit()
        => OnSta(() =>
        {
            using var rich = new RtfRichTextBox { Text = "abcdef", MaxLength = 3 };
            _ = rich.Handle;
            rich.Select(2, 2);
            rich.Capture = true;

            Assert.Throws<ArgumentException>(() => rich.SelectedRtf = "invalid");

            Assert.Equal("abcdef", rich.Text);
            Assert.Equal(2, rich.SelectionStart);
            Assert.Equal(2, rich.SelectionLength);
            Assert.Empty(rich.Submissions);
            Assert.Equal((nint)3, SendMessageW(rich.Handle, EmGetLimitText, 0, 0));
        });

    [Theory]
    [InlineData("", "")]
    [InlineData("one\0", "one")]
    [InlineData("two\0\0", "two\0")]
    public void RtfOutputDecodesTheTransferredBytesAndRemovesOnlyOneTrailingNull(string nativeBytes, string expected)
        => OnSta(() =>
        {
            using var rich = new RtfRichTextBox();
            _ = rich.Handle;
            rich.OutputBytes = Encoding.ASCII.GetBytes(nativeBytes);
            try { Assert.Equal(expected, rich.Rtf); }
            finally { rich.OutputBytes = null; }
        });

    [Fact]
    public void OutputCallbackKeepsItsCookieRootedAcrossGcAndSeveralChunks()
        => OnSta(() =>
        {
            using var rich = new RtfRichTextBox();
            _ = rich.Handle;
            rich.OutputBytes = Encoding.ASCII.GetBytes(@"{\rtf1 callback chunks}");
            rich.SplitOutputAndCollect = true;
            try
            {
                Assert.Equal(@"{\rtf1 callback chunks}", rich.SelectedRtf);
                Assert.Equal(3, rich.OutputCallbackCount);
                Assert.Equal(rich.OutputBytes!.Length, rich.OutputTransferredBytes);
                Assert.Equal(0u, rich.OutputCallbackError);
            }
            finally { rich.OutputBytes = null; rich.SplitOutputAndCollect = false; }
        });

    [Fact]
    public void OutputNativeErrorThrowsWithoutChangingTheDocumentAndCanBeRetried()
        => OnSta(() =>
        {
            using var rich = new RtfRichTextBox { Rtf = BoldDocument };
            rich.Select(0, 4);
            rich.RejectOutput = true;
            try { Assert.Throws<InvalidOperationException>(() => rich.SelectedRtf); }
            finally { rich.RejectOutput = false; }
            Assert.Equal("bold plain", rich.Text);
            Assert.Equal(0, rich.SelectionStart);
            Assert.Equal(4, rich.SelectionLength);
            Assert.StartsWith(@"{\rtf", rich.SelectedRtf);
            Assert.True(IsSelectionBold(rich.Handle));
        });

    [Fact]
    public void OutputCallbackFailureIsReportedInsideTheNativeBoundaryAndTheNextCallWorks()
        => OnSta(() =>
        {
            using var rich = new RtfRichTextBox { Rtf = BoldDocument };
            rich.InvalidOutputCount = true;
            try
            {
                Assert.Throws<InvalidOperationException>(() => rich.Rtf);
                Assert.Equal(1u, rich.OutputCallbackError);
                Assert.Equal(0, rich.OutputTransferredBytes);
            }
            finally { rich.InvalidOutputCount = false; }
            Assert.StartsWith(@"{\rtf", rich.Rtf!);
            Assert.Equal("bold plain", rich.Text);
        });

    [Fact]
    public void InputNativeErrorRestoresTheConfiguredLimitAndAllowsRetry()
        => OnSta(() =>
        {
            using var rich = new RtfRichTextBox { Text = "before", MaxLength = 3 };
            _ = rich.Handle;
            rich.RejectInput = true;
            rich.Capture = true;
            try { Assert.Throws<InvalidOperationException>(() => rich.SelectedRtf = BoldDocument); }
            finally { rich.RejectInput = false; }
            Assert.Equal("before", rich.Text);
            Assert.Equal((nint)3, SendMessageW(rich.Handle, EmGetLimitText, 0, 0));
            Assert.DoesNotContain(rich.Submissions, item => item.Message == EmSetModify && item.WParam == -1);
            rich.SelectedRtf = @"{\rtf1 retry}";
            Assert.Equal("retrybefore", rich.Text);
        });

    private static nint CreateHostedHandle(Form form, RichTextBox rich)
    {
        form.Controls.Add(rich);
        _ = form.Handle;
        return rich.Handle;
    }

    private static int ReadNativeRtfByteCount(nint handle, int flags)
    {
        using var bytes = new MemoryStream();
        Exception? failure = null;
        EditStreamCallback callback = (nuint cookie, nint buffer, int count, out int transferred) =>
        {
            transferred = 0;
            try
            {
                byte[] block = new byte[count];
                Marshal.Copy(buffer, block, 0, count);
                bytes.Write(block, 0, count);
                transferred = count;
                return 0;
            }
            catch (Exception exception)
            {
                failure = exception;
                return 1;
            }
        };
        int streamSize = 2 * IntPtr.Size + sizeof(uint);
        nint stream = Marshal.AllocHGlobal(streamSize);
        try
        {
            Marshal.Copy(new byte[streamSize], 0, stream, streamSize);
            Marshal.WriteIntPtr(stream, IntPtr.Size + sizeof(uint),
                Marshal.GetFunctionPointerForDelegate(callback));
            _ = SendMessageW(handle, EmStreamOut, flags, stream);
            Assert.Null(failure);
            Assert.Equal(0, Marshal.ReadInt32(stream, IntPtr.Size));
            return checked((int)bytes.Length);
        }
        finally
        {
            GC.KeepAlive(callback);
            Marshal.FreeHGlobal(stream);
        }
    }

    private static bool IsSelectionBold(nint handle)
    {
        // CHARFORMATW from Richedit.h, pack(4): dwMask at4, dwEffects at8.
        nint format = Marshal.AllocHGlobal(92);
        try
        {
            Marshal.Copy(new byte[92], 0, format, 92);
            Marshal.WriteInt32(format, 92);
            _ = SendMessageW(handle, EmGetCharFormat, 1, format);
            Assert.NotEqual(0, Marshal.ReadInt32(format, 4) & 1);
            return (Marshal.ReadInt32(format, 8) & 1) != 0;
        }
        finally { Marshal.FreeHGlobal(format); }
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "RichTextBox RTF test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private readonly record struct Submission(int Message, nint WParam, nint LParam, int RangeEnd);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate uint EditStreamCallback(nuint cookie, nint buffer, int count, out int transferred);

    private sealed class RtfRichTextBox : RichTextBox
    {
        internal bool Capture { get; set; }
        internal bool RejectInput { get; set; }
        internal bool RejectOutput { get; set; }
        internal bool InvalidOutputCount { get; set; }
        internal bool SplitOutputAndCollect { get; set; }
        internal byte[]? OutputBytes { get; set; }
        internal int OutputCallbackCount { get; private set; }
        internal int OutputTransferredBytes { get; private set; }
        internal uint OutputCallbackError { get; private set; }
        internal List<Submission> Submissions { get; } = new();

        protected override void WndProc(ref Message message)
        {
            if (Capture && Submissions.Count < 100 && message.Msg is WmSetText or WmClear
                or EmExSetSel or EmExLimitText or EmStreamIn or EmStreamOut or EmSetModify or EmGetLineCount)
            {
                nint parameter = message.Msg == EmExSetSel ? Marshal.ReadInt32(message.LParam)
                    : message.Msg is WmSetText or EmStreamIn or EmStreamOut ? 0 : message.LParam;
                int rangeEnd = message.Msg == EmExSetSel ? Marshal.ReadInt32(message.LParam, 4) : 0;
                Submissions.Add(new Submission(message.Msg, message.WParam, parameter, rangeEnd));
            }
            if ((RejectInput && message.Msg == EmStreamIn) || (RejectOutput && message.Msg == EmStreamOut))
            {
                Marshal.WriteInt32(message.LParam, IntPtr.Size, 5);
                message.Result = 0;
                return;
            }
            if (message.Msg == EmStreamOut && (OutputBytes is not null || InvalidOutputCount))
            {
                // EDITSTREAM is pack(4); the callback follows DWORD dwError.
                nuint cookie = (nuint)Marshal.ReadIntPtr(message.LParam);
                EditStreamCallback callback = Marshal.GetDelegateForFunctionPointer<EditStreamCallback>(
                    Marshal.ReadIntPtr(message.LParam, IntPtr.Size + sizeof(uint)));
                byte[] bytes = OutputBytes ?? [0];
                nint buffer = Marshal.AllocHGlobal(Math.Max(1, bytes.Length));
                try
                {
                    Marshal.Copy(bytes, 0, buffer, bytes.Length);
                    if (InvalidOutputCount)
                    {
                        OutputCallbackError = callback(cookie, buffer, -1, out int written);
                        OutputTransferredBytes = written;
                        Marshal.WriteInt32(message.LParam, IntPtr.Size, unchecked((int)OutputCallbackError));
                    }
                    else if (SplitOutputAndCollect)
                    {
                        SendChunk(callback, cookie, buffer, 1);
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        SendChunk(callback, cookie, buffer + 1, 2);
                        SendChunk(callback, cookie, buffer + 3, bytes.Length - 3);
                    }
                    else { SendChunk(callback, cookie, buffer, bytes.Length); }
                }
                finally { Marshal.FreeHGlobal(buffer); }
                if (OutputCallbackError != 0)
                {
                    Marshal.WriteInt32(message.LParam, IntPtr.Size, unchecked((int)OutputCallbackError));
                }
                message.Result = 0;
                return;
            }
            base.WndProc(ref message);
        }

        private void SendChunk(EditStreamCallback callback, nuint cookie, nint buffer, int length)
        {
            uint error = callback(cookie, buffer, length, out int written);
            OutputCallbackError = error;
            OutputTransferredBytes += written;
            OutputCallbackCount++;
        }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint handle, int message, nint wParam, nint lParam);
}
