using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class RichTextBoxTextModelTests
{
    private const int WmClear = 0x0303;
    private const int EmGetSel = 0x00B0;
    private const int EmGetModify = 0x00B8;
    private const int EmSetModify = 0x00B9;
    private const int EmGetLineCount = 0x00BA;
    private const int EmReplaceSel = 0x00C2;
    private const int EmCanUndo = 0x00C6;
    private const int EmGetLimitText = 0x00D5;
    private const int EmExLimitText = 0x0435;
    private const int EmStreamIn = 0x0449;
    private const int EmGetTextEx = 0x045E;
    private const int EmGetTextLengthEx = 0x045F;

    [Fact]
    public void TextLengthIsTheRichOverrideWithTheStandardReadOnlyMetadata()
    {
        PropertyInfo property = typeof(RichTextBox).GetProperty(nameof(RichTextBox.TextLength))!;
        Assert.Equal(typeof(RichTextBox), property.DeclaringType);
        Assert.Equal(typeof(TextBoxBase), property.GetMethod!.GetBaseDefinition().DeclaringType);
        Assert.True(property.GetMethod.IsVirtual);
        Assert.Null(property.SetMethod);
        Assert.False(property.GetCustomAttribute<BrowsableAttribute>()!.Browsable);
    }

    [Fact]
    public void SelectedTextIsTheRichOverrideWithTheStandardNullAndSerializationContract()
    {
        PropertyInfo property = typeof(RichTextBox).GetProperty(nameof(RichTextBox.SelectedText))!;
        Assert.Equal(typeof(RichTextBox), property.DeclaringType);
        Assert.Equal(typeof(TextBoxBase), property.GetMethod!.GetBaseDefinition().DeclaringType);
        Assert.Equal(typeof(TextBoxBase), property.SetMethod!.GetBaseDefinition().DeclaringType);
        Assert.Equal(string.Empty, property.GetCustomAttribute<DefaultValueAttribute>()!.Value);
        Assert.False(property.GetCustomAttribute<BrowsableAttribute>()!.Browsable);
        Assert.Equal(DesignerSerializationVisibility.Hidden,
            property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>()!.Visibility);
        Assert.NotNull(property.SetMethod.GetParameters()[0].GetCustomAttribute<AllowNullAttribute>());
    }

    [Theory]
    [InlineData("", 0)]
    [InlineData("plain", 5)]
    [InlineData("中😀z", 4)]
    [InlineData("a\r\nb", 3)]
    [InlineData("a\nb\r", 4)]
    [InlineData("a\0discarded", 1)]
    public void TextLengthCreatesTheRichEditAndCountsItsLogicalUtf16Text(string input, int expected)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new ModelRichTextBox { Text = input };
            form.Controls.Add(rich);
            Assert.False(rich.IsHandleCreated);

            Assert.Equal(expected, rich.TextLength);

            Assert.True(rich.IsHandleCreated);
            Assert.Equal(1, rich.CreatedCount);
            Assert.Equal(expected, QueryLogicalLength(rich.Handle));
        });

    [Fact]
    public void SelectedTextGetterCreatesAnEmptyRichEdit()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new ModelRichTextBox();
            form.Controls.Add(rich);
            Assert.False(rich.IsHandleCreated);

            Assert.Equal(string.Empty, rich.SelectedText);

            Assert.True(rich.IsHandleCreated);
            Assert.Equal(1, rich.CreatedCount);
        });

    [Theory]
    [InlineData("abcdef", 2, 2, "cd")]
    [InlineData("a\r\nb", 1, 1, "\n")]
    [InlineData("a\rb\nc", 1, 3, "\nb\n")]
    [InlineData("中😀終", 1, 2, "😀")]
    [InlineData("abcdef", 6, 0, "")]
    public void SelectedTextReadsTheNativeRangeUsingLogicalLfText(string input, int start, int length, string expected)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new ModelRichTextBox { Text = input };
            nint handle = CreateHostedHandle(form, rich);
            rich.Select(start, length);
            NativeState before = ReadState(handle);
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;

            Assert.Equal(expected, rich.SelectedText);

            Assert.Equal(before, ReadState(handle));
            Assert.Equal(0, changes);
            Assert.Equal(handle, rich.Handle);
        });

    [Theory]
    [InlineData(0)]
    [InlineData(70001)]
    [InlineData(-1)]
    public void TextLengthSendsNumCharsAndUnicodeWithoutReadingVirtualText(int nativeReply)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new ModelRichTextBox { Text = "initial" };
            nint handle = CreateHostedHandle(form, rich);
            rich.Reads = 0;
            rich.LengthReply = nativeReply;
            rich.Capture = true;

            Assert.Equal(nativeReply, rich.TextLength);

            Assert.Equal(0, rich.Reads);
            Assert.Equal(new[] { new LengthRequest(8, 1200, 0) }, rich.LengthRequests);
            Assert.Empty(rich.TextRequests);
            Assert.Equal(handle, rich.Handle);
        });

    [Fact]
    public void SelectedTextUsesPackedUnicodeSelectionRequestAndNormalizesItsActualResult()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new ModelRichTextBox { Text = "initial" };
            nint handle = CreateHostedHandle(form, rich);
            rich.Reads = 0;
            rich.LengthReply = 7;
            rich.SelectionReply = "pick\rx";
            rich.Capture = true;

            Assert.Equal("pick\nx", rich.SelectedText);

            Assert.Equal(0, rich.Reads);
            Assert.Equal(new[] { new LengthRequest(0, 1200, 0) }, rich.LengthRequests);
            Assert.Equal(new[] { new TextRequest(16, 2, 1200, 0, 0) }, rich.TextRequests);
            Assert.Equal(handle, rich.Handle);
        });

    [Fact]
    public void InvalidNativeLengthForSelectedTextKeepsTheFixedWin32Exception()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new ModelRichTextBox();
            _ = CreateHostedHandle(form, rich);
            rich.LengthReply = unchecked((int)0x80070057);
            rich.Capture = true;

            try
            {
                Win32Exception exception = Assert.Throws<Win32Exception>(() => rich.SelectedText);
                Assert.Equal(unchecked((int)0x80070057), exception.NativeErrorCode);
                Assert.Empty(rich.TextRequests);
            }
            finally
            {
                // End this one-message fault before native destruction also
                // reads text; keep the actual exception contract above intact.
                rich.Capture = false;
                rich.LengthReply = null;
            }
        });

    [Fact]
    public void NativeEditsChangeLogicalLengthAndTheModelGettersKeepEditingState()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new ModelRichTextBox { Text = "ab" };
            nint handle = CreateHostedHandle(form, rich);
            rich.Select(1, 0);
            _ = SendMessageW(handle, EmReplaceSel, (nint)(-1), "中");
            rich.Select(1, 1);
            NativeState before = ReadState(handle);
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;

            Assert.Equal(3, rich.TextLength);
            Assert.Equal("中", rich.SelectedText);

            Assert.Equal(new NativeState(1, 2, true, true), before);
            Assert.Equal(before, ReadState(handle));
            Assert.Equal("a中b", rich.Text);
            Assert.Equal(0, changes);
        });

    [Fact]
    public void LogicalLengthAndSelectedTextKeepTheFullIndexBeyondSixteenBits()
        => OnSta(() =>
        {
            string input = new string('x', 70000) + "中😀\r\n終";
            using var form = new Form();
            using var rich = new ModelRichTextBox { Text = input };
            nint handle = CreateHostedHandle(form, rich);
            rich.Select(70000, 4);

            Assert.Equal(70005, rich.TextLength);
            Assert.Equal(70000, rich.SelectionStart);
            Assert.Equal(4, rich.SelectionLength);
            Assert.Equal("中😀\n", rich.SelectedText);
            Assert.Equal(new NativeState(70000, 70004, false, false), ReadState(handle));
        });

    [Theory]
    [InlineData(null, "")]
    [InlineData("", "")]
    [InlineData("中😀", "中😀")]
    public void SelectedTextSetterCreatesTheHandleForEveryValue(string? input, string expected)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new ModelRichTextBox();
            form.Controls.Add(rich);
            Assert.False(rich.IsHandleCreated);

            rich.SelectedText = input;

            Assert.True(rich.IsHandleCreated);
            Assert.Equal(1, rich.CreatedCount);
            Assert.Equal(expected, rich.Text);
        });

    [Theory]
    [InlineData("XY", "abXYef")]
    [InlineData("中😀", "ab中😀ef")]
    [InlineData("x\r\ny", "abx\nyef")]
    [InlineData("x\0discarded", "abxef")]
    [InlineData("\0discarded", "abef")]
    public void SelectedTextStreamsUnicodeIntoOnlyTheCurrentRange(string replacement, string expected)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new ModelRichTextBox { Text = "abcdef" };
            nint handle = CreateHostedHandle(form, rich);
            rich.Select(2, 2);
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;

            rich.SelectedText = replacement;

            Assert.Equal(expected, rich.Text);
            Assert.True(rich.Modified);
            Assert.True(rich.CanUndo);
            Assert.Equal(1, changes);
            Assert.Equal(handle, rich.Handle);
        });

    [Theory]
    [InlineData(0)]
    [InlineData(3)]
    public void SelectedTextCanExceedTheInputLimitThenRestoresTheConfiguredRichLimit(int limit)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new ModelRichTextBox { MaxLength = limit, Text = "abcd" };
            nint handle = CreateHostedHandle(form, rich);
            rich.Select(1, 2);

            rich.SelectedText = "012345";

            Assert.Equal("a012345d", rich.Text);
            Assert.Equal(limit, rich.MaxLength);
            // RichEdit gives EM_EXLIMITTEXT(0) its documented native default.
            Assert.Equal(limit == 0 ? (nint)65536 : limit, SendMessageW(handle, EmGetLimitText, 0, 0));
            Assert.True(rich.Modified);
            Assert.True(rich.CanUndo);
            Assert.Equal(handle, rich.Handle);
        });

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void EmptySelectedTextClearsOnlyTheSelectionThroughWmClear(string? replacement)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new ModelRichTextBox { Text = "abcdef" };
            nint handle = CreateHostedHandle(form, rich);
            rich.Select(2, 2);
            rich.Capture = true;

            rich.SelectedText = replacement;

            Assert.Equal("abef", rich.Text);
            Assert.Equal(new[] { new EditMessage(WmClear, 0, 0) }, rich.EditMessages);
            Assert.True(rich.Modified);
            Assert.True(rich.CanUndo);
            Assert.Equal(handle, rich.Handle);
        });

    [Theory]
    [InlineData(null, "abcdef")]
    [InlineData("", "abcdef")]
    [InlineData("XY", "abXYef")]
    public void ReadOnlyUsesTheDistinctNativeClearAndProgrammaticStreamRules(string? replacement, string expected)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new ModelRichTextBox { Text = "abcdef", ReadOnly = true };
            nint handle = CreateHostedHandle(form, rich);
            rich.Select(2, 2);

            rich.SelectedText = replacement;

            Assert.Equal(expected, rich.Text);
            Assert.True(rich.ReadOnly);
            Assert.Equal(handle, rich.Handle);
        });

    [Fact]
    public void SelectedTextUsesTheFixedUnicodeStreamAndLimitModifyLineMessageOrder()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new ModelRichTextBox { Text = "abcdef", MaxLength = 3 };
            _ = CreateHostedHandle(form, rich);
            rich.Select(2, 2);
            rich.Capture = true;

            rich.SelectedText = "longer";

            Assert.Equal(5, rich.EditMessages.Count);
            Assert.Equal(new EditMessage(EmExLimitText, 0, int.MaxValue), rich.EditMessages[0]);
            Assert.Equal(EmStreamIn, rich.EditMessages[1].Id);
            Assert.Equal((nint)0x8011, rich.EditMessages[1].WParam);
            Assert.NotEqual((nint)0, rich.EditMessages[1].LParam);
            Assert.Equal(new EditMessage(EmExLimitText, 0, 3), rich.EditMessages[2]);
            Assert.Equal(new EditMessage(EmSetModify, (nint)(-1), 0), rich.EditMessages[3]);
            Assert.Equal(new EditMessage(EmGetLineCount, 0, 0), rich.EditMessages[4]);
            Assert.Equal("ablongeref", rich.Text);
            Assert.True(rich.Modified);
            Assert.True(rich.CanUndo);
        });

    private static nint CreateHostedHandle(Form form, RichTextBox rich)
    {
        form.Controls.Add(rich);
        nint parent = form.Handle;
        nint child = rich.Handle;
        Assert.Equal(parent, GetParent(child));
        return child;
    }

    private static int QueryLogicalLength(nint handle)
    {
        var request = new GetTextLengthEx { Flags = 8, CodePage = 1200 };
        return (int)SendMessageW(handle, EmGetTextLengthEx, ref request, 0);
    }

    private static NativeState ReadState(nint handle)
    {
        _ = SendMessageW(handle, EmGetSel, out int start, out int end);
        return new(start, end, SendMessageW(handle, EmGetModify, 0, 0) != 0,
            SendMessageW(handle, EmCanUndo, 0, 0) != 0);
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "RichTextBox text model test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private readonly record struct NativeState(int Start, int End, bool Modified, bool CanUndo);
    private readonly record struct LengthRequest(uint Flags, uint CodePage, nint LParam);
    private readonly record struct TextRequest(uint Bytes, uint Flags, uint CodePage, nint DefaultChar, nint UsedDefaultChar);
    private readonly record struct EditMessage(int Id, nint WParam, nint LParam);

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    private struct GetTextLengthEx
    {
        internal uint Flags;
        internal uint CodePage;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    private struct GetTextEx
    {
        internal uint Bytes;
        internal uint Flags;
        internal uint CodePage;
        internal nint DefaultChar;
        internal nint UsedDefaultChar;
    }

    private sealed class ModelRichTextBox : RichTextBox
    {
        internal bool Capture { get; set; }
        internal int? LengthReply { get; set; }
        internal string? SelectionReply { get; set; }
        internal int CreatedCount { get; private set; }
        internal int Reads { get; set; }
        internal List<LengthRequest> LengthRequests { get; } = new();
        internal List<TextRequest> TextRequests { get; } = new();
        internal List<EditMessage> EditMessages { get; } = new();

        [AllowNull]
        public override string Text
        {
            get { Reads++; return base.Text; }
            set => base.Text = value;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            CreatedCount++;
        }

        protected override void WndProc(ref Message message)
        {
            if (Capture && message.Msg == EmGetTextLengthEx)
            {
                Assert.InRange(LengthRequests.Count, 0, 15);
                GetTextLengthEx request = Marshal.PtrToStructure<GetTextLengthEx>(message.WParam);
                LengthRequests.Add(new(request.Flags, request.CodePage, message.LParam));
                if (LengthReply is int result)
                {
                    message.Result = result;
                    return;
                }
            }
            if (Capture && message.Msg == EmGetTextEx)
            {
                Assert.InRange(TextRequests.Count, 0, 15);
                GetTextEx request = Marshal.PtrToStructure<GetTextEx>(message.WParam);
                TextRequests.Add(new(request.Bytes, request.Flags, request.CodePage,
                    request.DefaultChar, request.UsedDefaultChar));
                if (SelectionReply is string result)
                {
                    Assert.True(request.Bytes >= (result.Length + 1) * sizeof(char));
                    Marshal.Copy(result.ToCharArray(), 0, message.LParam, result.Length);
                    Marshal.WriteInt16(message.LParam, result.Length * sizeof(char), 0);
                    message.Result = result.Length;
                    return;
                }
            }
            if (Capture && message.Msg is WmClear or EmExLimitText or EmStreamIn or EmSetModify or EmGetLineCount)
            {
                Assert.InRange(EditMessages.Count, 0, 15);
                EditMessages.Add(new(message.Msg, message.WParam, message.LParam));
            }
            base.WndProc(ref message);
        }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetParent(nint handle);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint handle, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint SendMessageW(nint handle, int message, nint wParam, string lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint handle, int message, ref GetTextLengthEx wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint handle, int message, out int wParam, out int lParam);
}
