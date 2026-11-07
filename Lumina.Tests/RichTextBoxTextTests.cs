using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class RichTextBoxTextTests
{
    private const int WmSetText = 0x000C;
    private const int WmChar = 0x0102;
    private const int EmGetModify = 0x00B8;
    private const int EmSetModify = 0x00B9;
    private const int EmGetLineCount = 0x00BA;
    private const int EmCanUndo = 0x00C6;
    private const int EmGetLimitText = 0x00D5;
    private const int EmExSetSel = 0x0437;
    private const int EmExLimitText = 0x0435;
    private const int EmStreamIn = 0x0449;

    [Fact]
    public void TextIsTheStandardOverrideWithLocalizableRefreshAndNullMetadata()
    {
        PropertyInfo property = typeof(RichTextBox).GetProperty(nameof(RichTextBox.Text),
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)!;
        Assert.NotNull(property);
        Assert.Equal(typeof(string), property.PropertyType);
        Assert.True(property.GetMethod!.IsVirtual);
        Assert.True(property.SetMethod!.IsVirtual);
        Assert.Equal(typeof(Control), property.GetMethod.GetBaseDefinition().DeclaringType);
        Assert.True(property.GetCustomAttribute<LocalizableAttribute>()!.IsLocalizable);
        Assert.Equal(RefreshProperties.All,
            property.GetCustomAttribute<RefreshPropertiesAttribute>()!.RefreshProperties);
        Assert.NotNull(property.SetMethod.GetParameters()[0].GetCustomAttribute<AllowNullAttribute>());
    }

    [Theory]
    [InlineData(null, "")]
    [InlineData("", "")]
    [InlineData("plain", "plain")]
    [InlineData("中😀z", "中😀z")]
    [InlineData("a\r\nb", "a\r\nb")]
    [InlineData("a\0b", "a\0b")]
    public void BeforeCreationTextIsOnlyCachedWithoutNormalizationOrNotifications(string? value, string expected)
        => OnSta(() =>
        {
            using var rich = new TrackingRichTextBox();
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;

            rich.Text = value;

            Assert.Equal(expected, rich.Text);
            Assert.False(rich.IsHandleCreated);
            Assert.Equal(0, changes);
            Assert.Empty(rich.Submissions);
        });

    [Fact]
    public void CacheAssignmentDoesNotReadTheVirtualTextGetter()
        => OnSta(() =>
        {
            using var rich = new ThrowingTextGetterRichTextBox();
            rich.Text = "cached";
            rich.Text = null;
            Assert.False(rich.IsHandleCreated);
            Assert.Equal(0, rich.GetterCalls);
        });

    [Fact]
    public void LatestCachedValueIsInitializedWithoutAChangeEvent()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new TrackingRichTextBox();
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;
            rich.Text = "discarded";
            rich.Text = "latest 中😀";
            rich.Text = "latest 中😀";
            Assert.False(rich.IsHandleCreated);

            nint handle = CreateHostedHandle(form, rich);

            Assert.Equal("latest 中😀", rich.Text);
            Assert.Equal("latest 中😀", ReadNativeText(handle));
            Assert.Equal(0, changes);
            Assert.False(rich.Modified);
            Assert.False(rich.CanUndo);
        });

    [Theory]
    [InlineData("plain", "plain")]
    [InlineData("中😀z", "中😀z")]
    [InlineData("a\r\nb", "a\nb")]
    [InlineData("a\nb", "a\nb")]
    [InlineData("a\rb", "a\nb")]
    [InlineData("a\r\nb\r\nc\n", "a\nb\nc\n")]
    [InlineData("a\0ignored", "a")]
    [InlineData("\0ignored", "")]
    public void CreatedTextUsesRichEditUnicodeStreamingAndLogicalLineEndings(string value, string expected)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new TrackingRichTextBox();
            nint handle = CreateHostedHandle(form, rich);
            rich.CaptureSubmissions = true;

            rich.Text = value;

            Assert.Equal(expected, rich.Text);
            Assert.Single(rich.Submissions.Where(item => item.Message == EmStreamIn));
            Assert.Equal(handle, rich.Handle);
            Assert.False(rich.Modified);
            Assert.False(rich.CanUndo);
        });

    [Fact]
    public void UnicodeInputLargerThanTheNativeChunkAndOldLimitIsFullyConsumed()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new TrackingRichTextBox { MaxLength = 3 };
            nint handle = CreateHostedHandle(form, rich);
            string text = new string('中', 50000) + "😀end";

            rich.Text = text;

            Assert.Equal(text, rich.Text);
            Assert.Equal(50005, rich.Text.Length);
            Assert.Equal((nint)3, SendMessageW(handle, EmGetLimitText, 0, 0));
            Assert.Equal(3, rich.MaxLength);
            Assert.False(rich.Modified);
            Assert.False(rich.CanUndo);
        });

    [Fact]
    public void NonemptyTextSubmitsTheFixedUnicodeStreamAndRestoresTheConfiguredLimit()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new TrackingRichTextBox { MaxLength = 3 };
            nint handle = CreateHostedHandle(form, rich);
            rich.CaptureSubmissions = true;

            rich.Text = "longer than three";

            Assert.Equal(new[]
            {
                new Submission(EmExSetSel, 0, 0, 0),
                new Submission(EmExLimitText, 0, int.MaxValue, 0),
                new Submission(EmStreamIn, 0x0011, 0, 0),
                new Submission(EmExLimitText, 0, 3, 0),
                new Submission(EmSetModify, -1, 0, 0),
                new Submission(EmGetLineCount, 0, 0, 0),
                new Submission(EmSetModify, 0, 0, 0)
            }, rich.Submissions);
            Assert.Equal("longer than three", rich.Text);
            Assert.Equal((nint)3, SendMessageW(handle, EmGetLimitText, 0, 0));
            Assert.False(rich.Modified);
        });

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void EmptyTextUsesSetTextWithoutTheStreamingOrTemporaryLimitPath(string? value)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new TrackingRichTextBox { Text = "before", MaxLength = 3 };
            nint handle = CreateHostedHandle(form, rich);
            rich.Modified = true;
            rich.CaptureSubmissions = true;

            rich.Text = value;

            Assert.Equal(new[]
            {
                new Submission(WmSetText, 0, 0, 0),
                new Submission(EmSetModify, 0, 0, 0)
            }, rich.Submissions);
            Assert.Equal("", rich.Text);
            Assert.Equal((nint)3, SendMessageW(handle, EmGetLimitText, 0, 0));
            Assert.Equal((nint)0, SendMessageW(handle, EmGetModify, 0, 0));
            Assert.Equal((nint)0, SendMessageW(handle, EmCanUndo, 0, 0));
        });

    [Fact]
    public void SameValueAssignmentStillStreamsAndResetsTheNativeSelectionAndModifyFlag()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new TrackingRichTextBox { Text = "abcdef" };
            nint handle = CreateHostedHandle(form, rich);
            rich.Select(2, 3);
            rich.Modified = true;
            rich.CaptureSubmissions = true;

            rich.Text = "abcdef";

            Assert.Single(rich.Submissions.Where(item => item.Message == EmStreamIn));
            Assert.Equal("abcdef", rich.Text);
            Assert.Equal(0, rich.SelectionStart);
            Assert.Equal(0, rich.SelectionLength);
            Assert.False(rich.Modified);
            Assert.False(rich.CanUndo);
            Assert.Equal(handle, rich.Handle);
        });

    [Fact]
    public void TextChangeIsReportedOnceWithTheNewLogicalTextAndClearsNativeUndo()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new TrackingRichTextBox();
            nint handle = CreateHostedHandle(form, rich);
            _ = SendMessageW(handle, WmChar, 'x', 0);
            Assert.True(rich.CanUndo);
            var values = new List<string>();
            rich.TextChanged += (_, _) => values.Add(rich.Text);

            rich.Text = "new\r\n中😀";

            Assert.Equal(new[] { "new\n中😀" }, values);
            Assert.Equal("new\n中😀", rich.Text);
            Assert.False(rich.Modified);
            Assert.False(rich.CanUndo);
            Assert.Equal(handle, rich.Handle);
        });

    [Fact]
    public void LaterNativeTypingRemainsRestrictedAfterAnOversizedProgrammaticTextAssignment()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new TrackingRichTextBox { MaxLength = 3 };
            nint handle = CreateHostedHandle(form, rich);
            rich.Text = "longer than three";
            rich.Select(rich.TextLength, 0);

            _ = SendMessageW(handle, WmChar, 'x', 0);

            Assert.Equal("longer than three", rich.Text);
            Assert.Equal((nint)3, SendMessageW(handle, EmGetLimitText, 0, 0));
            rich.Text = "a";
            rich.Select(1, 0);
            _ = SendMessageW(handle, WmChar, 'b', 0);
            _ = SendMessageW(handle, WmChar, 'c', 0);
            _ = SendMessageW(handle, WmChar, 'd', 0);
            Assert.Equal("abc", rich.Text);
        });

    [Fact]
    public void NativeStreamErrorIsReportedAndTheConfiguredLimitIsRestored()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new TrackingRichTextBox { Text = "before", MaxLength = 3 };
            nint handle = CreateHostedHandle(form, rich);
            rich.RejectStream = true;
            rich.CaptureSubmissions = true;

            Assert.Throws<InvalidOperationException>(() => rich.Text = "rejected");

            Assert.Equal("before", rich.Text);
            Assert.Equal((nint)3, SendMessageW(handle, EmGetLimitText, 0, 0));
            Assert.Equal(3, rich.MaxLength);
            Assert.DoesNotContain(rich.Submissions,
                item => item.Message == EmSetModify && item.WParam == -1);
            rich.RejectStream = false;
            rich.Text = "retry";
            Assert.Equal("retry", rich.Text);
            Assert.Equal((nint)3, SendMessageW(handle, EmGetLimitText, 0, 0));
        });

    [Fact]
    public void CreationFailurePreservesThePendingPlainTextForRetry()
        => OnSta(() =>
        {
            using var rich = new CreationFailureRichTextBox { Text = "pending 中😀" };
            rich.FailCreation = true;
            var failure = Assert.Throws<InvalidOperationException>(() => _ = rich.Handle);
            Assert.Same(rich.Failure, failure);
            Assert.False(rich.IsHandleCreated);
            Assert.Equal("pending 中😀", rich.Text);

            rich.FailCreation = false;
            Assert.NotEqual((nint)0, rich.Handle);
            Assert.Equal("pending 中😀", rich.Text);
        });

    [Fact]
    public void RecreationKeepsPlainTextSelectionModifiedAndTheInputLimitWithoutExtraChanges()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new TrackingRichTextBox { Text = "plain 中😀 text", MaxLength = 3 };
            _ = CreateHostedHandle(form, rich);
            rich.Select(2, 3);
            rich.Modified = true;
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;

            rich.RecreateNow();

            Assert.Equal("plain 中😀 text", rich.Text);
            Assert.Equal(2, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.True(rich.Modified);
            Assert.False(rich.CanUndo);
            Assert.Equal((nint)3, SendMessageW(rich.Handle, EmGetLimitText, 0, 0));
            Assert.Equal(0, changes);
            Assert.Equal(2, rich.CreatedCount);
        });

    private static nint CreateHostedHandle(Form form, RichTextBox rich)
    {
        form.Controls.Add(rich);
        _ = form.Handle;
        return rich.Handle;
    }

    private static string ReadNativeText(nint handle)
    {
        var buffer = new char[256];
        int length = GetWindowTextW(handle, buffer, buffer.Length);
        Assert.InRange(length, 0, buffer.Length - 2);
        return new string(buffer, 0, length);
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "RichTextBox Text test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private readonly record struct Submission(int Message, nint WParam, nint LParam, int RangeEnd);

    private sealed class TrackingRichTextBox : RichTextBox
    {
        internal bool CaptureSubmissions { get; set; }
        internal bool RejectStream { get; set; }
        internal int CreatedCount { get; private set; }
        internal List<Submission> Submissions { get; } = new();

        internal void RecreateNow() => RecreateHandle();

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            CreatedCount++;
        }

        protected override void WndProc(ref Message message)
        {
            if (CaptureSubmissions && Submissions.Count < 100 && message.Msg is
                WmSetText or EmExSetSel or EmExLimitText or EmStreamIn or EmSetModify or EmGetLineCount)
            {
                int rangeEnd = message.Msg == EmExSetSel ? Marshal.ReadInt32(message.LParam, 4) : 0;
                nint parameter = message.Msg == EmExSetSel ? Marshal.ReadInt32(message.LParam)
                    : message.Msg is WmSetText or EmStreamIn ? 0 : message.LParam;
                Submissions.Add(new Submission(message.Msg, message.WParam, parameter, rangeEnd));
            }
            if (RejectStream && message.Msg == EmStreamIn)
            {
                Marshal.WriteInt32(message.LParam, IntPtr.Size, 5);
                message.Result = 0;
                return;
            }
            base.WndProc(ref message);
        }
    }

    private sealed class ThrowingTextGetterRichTextBox : RichTextBox
    {
        internal int GetterCalls { get; private set; }

        [AllowNull]
        public override string Text
        {
            get { GetterCalls++; throw new InvalidOperationException("The setter must not call this getter."); }
            set => base.Text = value;
        }
    }

    private sealed class CreationFailureRichTextBox : RichTextBox
    {
        internal bool FailCreation { get; set; }
        internal InvalidOperationException Failure { get; } = new("creation failure");

        protected override CreateParams CreateParams
        {
            get
            {
                if (FailCreation) { throw Failure; }
                return base.CreateParams;
            }
        }
    }

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern int GetWindowTextW(nint handle, [Out] char[] text, int count);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint handle, int message, nint wParam, nint lParam);
}
