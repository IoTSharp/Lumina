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
public sealed class TextBoxAppendTextTests
{
    private const int EmGetSel = 0x00B0;
    private const int EmSetSel = 0x00B1;
    private const int EmGetModify = 0x00B8;
    private const int EmSetModify = 0x00B9;
    private const int EmReplaceSel = 0x00C2;
    private const int EmLimitText = 0x00C5;
    private const int EmCanUndo = 0x00C6;
    private const int EmUndo = 0x00C7;
    private const int EmEmptyUndoBuffer = 0x00CD;
    private const int EmGetLimitText = 0x00D5;

    [Fact]
    public void AppendTextHasStandardNullableNonvirtualSignatureAndInternalEndHook()
    {
        MethodInfo method = typeof(TextBox).GetMethod(nameof(TextBoxBase.AppendText))!;
        Assert.Equal(typeof(TextBoxBase), method.DeclaringType);
        Assert.True(method.IsPublic);
        Assert.False(method.IsVirtual);
        Assert.Equal(typeof(void), method.ReturnType);
        ParameterInfo parameter = Assert.Single(method.GetParameters());
        Assert.Equal("text", parameter.Name);
        Assert.Equal(typeof(string), parameter.ParameterType);
        Assert.Equal(NullabilityState.Nullable, new NullabilityInfoContext().Create(parameter).ReadState);

        MethodInfo endPosition = EndPositionMethod;
        Assert.True(endPosition.IsAssembly && endPosition.IsVirtual);
        Assert.Equal(typeof(int), endPosition.ReturnType);
        Assert.Empty(endPosition.GetParameters());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void NullAndEmptyAreNoOpsWithoutReadingTextOrChangingPendingSelection(string? text)
        => OnSta(() =>
        {
            using var textBox = new ObservingTextBox { Text = "abcdef", Size = new Size(120, 30) };
            textBox.Select(2, 3);
            textBox.TextLengthReads = 0;
            textBox.TextLengthFailure = new InvalidOperationException("must not inspect empty append");

            textBox.AppendText(text: text);

            Assert.Equal(0, textBox.TextLengthReads);
            Assert.Empty(textBox.SelectedTextWrites);
            Assert.False(textBox.IsHandleCreated);
            textBox.TextLengthFailure = null;
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(3, textBox.SelectionLength);
            Assert.Equal("abcdef", textBox.Text);
        });

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void NullAndEmptyKeepExistingNativeSelectionModifiedAndUndo(string? text)
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new TextBox { Text = "abcdef", Size = new Size(120, 30) };
            nint handle = CreateHostedEdit(form, textBox);
            _ = SendMessageW(handle, EmSetSel, 2, 4);
            _ = SendMessageW(handle, EmReplaceSel, (nint)(-1), "X");
            _ = SendMessageW(handle, EmSetSel, 1, 3);
            Assert.NotEqual((nint)0, SendMessageW(handle, EmCanUndo, 0, 0));
            _ = SendMessageW(handle, EmSetModify, (nint)1, 0);
            int changes = 0;
            textBox.TextChanged += (_, _) => changes++;

            textBox.AppendText(text);

            Assert.Equal(handle, textBox.Handle);
            Assert.Equal("abXef", textBox.Text);
            Assert.Equal((1, 3), GetNativeSelection(handle));
            Assert.NotEqual((nint)0, SendMessageW(handle, EmGetModify, 0, 0));
            Assert.NotEqual((nint)0, SendMessageW(handle, EmCanUndo, 0, 0));
            Assert.Equal(0, changes);
        });

    [Fact]
    public void AppendUsesVirtualSelectedTextAfterCapturingLengthWithoutCallingVirtualTextSetter()
        => OnSta(() =>
        {
            using var textBox = new ObservingTextBox
            {
                Text = "abcdef", Size = new Size(120, 30), BypassSelectedText = true
            };
            textBox.Select(1, 2);
            textBox.TextLengthReads = 0;
            textBox.TextWrites = 0;
            string appended = new(new[] { '\u7532', '\u4e59' });

            textBox.AppendText(appended);

            Assert.Same(appended, Assert.Single(textBox.SelectedTextWrites));
            Assert.Equal(2, textBox.LengthReadsAtSelectedText);
            Assert.Equal(0, textBox.TextWrites);
            Assert.Equal("abcdef", textBox.Text);
            Assert.Equal(6, textBox.SelectionStart);
            Assert.Equal(0, textBox.SelectionLength);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void InitialSelectionLengthFailurePropagatesBeforeCallingSelectedText()
        => OnSta(() =>
        {
            using var textBox = new ObservingTextBox { Text = "abcdef", Size = new Size(120, 30) };
            textBox.Select(1, 2);
            var failure = new InvalidOperationException("selection capture");
            textBox.TextLengthReads = 0;
            textBox.TextLengthFailure = failure;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.AppendText("X")));

            Assert.Equal(1, textBox.TextLengthReads);
            Assert.Empty(textBox.SelectedTextWrites);
            textBox.TextLengthFailure = null;
            Assert.Equal(1, textBox.SelectionStart);
            Assert.Equal(2, textBox.SelectionLength);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void EndPositionLengthFailurePreservesOriginalExceptionAndSelection()
        => OnSta(() =>
        {
            using var textBox = new ObservingTextBox { Text = "abcdef", Size = new Size(120, 30) };
            textBox.Select(1, 2);
            var failure = new InvalidOperationException("append end position");
            textBox.TextLengthReads = 0;
            textBox.TextLengthFailure = failure;
            textBox.FailOnLengthRead = 2;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.AppendText("X")));

            Assert.Equal(2, textBox.TextLengthReads);
            Assert.Empty(textBox.SelectedTextWrites);
            textBox.TextLengthFailure = null;
            Assert.Equal(1, textBox.SelectionStart);
            Assert.Equal(2, textBox.SelectionLength);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(120, 30, 6, 0)]
    [InlineData(0, 30, 1, 2)]
    [InlineData(120, 0, 1, 2)]
    public void VirtualSelectedTextFailureRestoresSelectionOnlyForZeroExtent(int width, int height,
        int expectedStart, int expectedLength)
        => OnSta(() =>
        {
            using var textBox = new ObservingTextBox { Text = "abcdef", Size = new Size(width, height) };
            textBox.Select(1, 2);
            var failure = new InvalidOperationException("virtual SelectedText");
            textBox.SelectedTextFailure = failure;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.AppendText("X")));

            Assert.Equal("X", Assert.Single(textBox.SelectedTextWrites));
            Assert.Equal(expectedStart, textBox.SelectionStart);
            Assert.Equal(expectedLength, textBox.SelectionLength);
            Assert.Equal("abcdef", textBox.Text);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(false, "", "abc", "abc")]
    [InlineData(false, "abcdef", "\u7532", "abcdef\u7532")]
    [InlineData(true, "\u7532\r\n\u4e59", "\r\n\u4e2d", "\u7532\r\n\u4e59\r\n\u4e2d")]
    [InlineData(true, "ab\U0001F600", "\U0001F642", "ab\U0001F600\U0001F642")]
    public void NonEmptyAppendCreatesHostedEditAndAppendsAfterPendingSelection(bool multiline,
        string original, string appended, string expected)
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new ObservingTextBox
            {
                Multiline = multiline, Text = original, Size = new Size(160, 60)
            };
            textBox.Select(0, original.Length);
            form.Controls.Add(textBox);
            _ = form.Handle;
            Assert.False(textBox.IsHandleCreated);
            textBox.TextWrites = 0;
            int changes = 0;
            textBox.TextChanged += (_, _) => changes++;

            textBox.AppendText(appended);

            AssertHostedParent(form, textBox.Handle);
            Assert.Equal(expected, textBox.Text);
            Assert.Equal((expected.Length, expected.Length), GetNativeSelection(textBox.Handle));
            Assert.Equal(0, textBox.TextWrites);
            Assert.Equal(appended, Assert.Single(textBox.SelectedTextWrites));
            Assert.Equal(1, changes);
            Assert.Equal((nint)0, SendMessageW(textBox.Handle, EmGetModify, 0, 0));
            Assert.Equal((nint)0, SendMessageW(textBox.Handle, EmCanUndo, 0, 0));
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ExistingEditAppendsProgrammaticallyAndRestoresConfiguredInputLimit(bool readOnly)
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new TextBox
            {
                Text = "abcd", ReadOnly = readOnly, MaxLength = 2, Size = new Size(120, 30)
            };
            nint handle = CreateHostedEdit(form, textBox);
            textBox.Select(1, 2);

            textBox.AppendText("\u7532\u4e59\u4e19");

            Assert.Equal(handle, textBox.Handle);
            Assert.Equal("abcd\u7532\u4e59\u4e19", textBox.Text);
            Assert.Equal((7, 7), GetNativeSelection(handle));
            Assert.Equal((nint)2, SendMessageW(handle, EmGetLimitText, 0, 0));
            Assert.Equal(readOnly, textBox.ReadOnly);
        });

    [Fact]
    public void AppendClearsExistingUndoAndModifiedThroughStandardSelectedTextPath()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new NativeParametersTextBox { Text = "abcd", Size = new Size(120, 30) };
            nint handle = CreateHostedEdit(form, textBox);
            CreateParams parameters = textBox.RequestedParameters;
            Assert.Equal("EDIT", parameters.ClassName);
            Assert.Equal(form.Handle, parameters.Parent);
            // Resolve the independent EDIT under this STA's current activation
            // context with the same complete creation request and native parent.
            nint independent = CreateWindowExW((uint)parameters.ExStyle, "EDIT", parameters.Caption,
                (uint)parameters.Style, parameters.X, parameters.Y, parameters.Width, parameters.Height,
                parameters.Parent, 0, 0, 0);
            Assert.NotEqual((nint)0, independent);
            try
            {
                AssertHostedParent(form, independent);
                Assert.Equal("abcd", GetNativeText(independent));
                _ = SendMessageW(independent, EmLimitText, textBox.MaxLength, 0);
                _ = SendMessageW(independent, EmSetSel, 1, 3);
                _ = SendMessageW(independent, EmReplaceSel, (nint)(-1), "X");
                _ = SendMessageW(independent, EmSetModify, (nint)1, 0);
                _ = SendMessageW(handle, EmSetSel, 1, 3);
                _ = SendMessageW(handle, EmReplaceSel, (nint)(-1), "X");
                _ = SendMessageW(handle, EmSetModify, (nint)1, 0);
                Assert.Equal("aXd", GetNativeText(independent));
                Assert.Equal("aXd", textBox.Text);
                Assert.NotEqual((nint)0, SendMessageW(independent, EmCanUndo, 0, 0));
                Assert.NotEqual((nint)0, SendMessageW(handle, EmCanUndo, 0, 0));
                Assert.NotEqual((nint)0, SendMessageW(independent, EmGetModify, 0, 0));
                Assert.True(textBox.Modified);

                // AppendText selects GetEndPosition (TextLength + 1) before
                // the existing SelectedText clearUndo replacement sequence.
                _ = SendMessageW(independent, EmSetSel, 4, 4);
                _ = SendMessageW(independent, EmLimitText, 0, 0);
                _ = SendMessageW(independent, EmReplaceSel, 0, "Y");
                _ = SendMessageW(independent, EmSetModify, 0, 0);
                _ = SendMessageW(independent, EmEmptyUndoBuffer, 0, 0);
                _ = SendMessageW(independent, EmLimitText, textBox.MaxLength, 0);
                textBox.AppendText("Y");

                Assert.Equal("aXdY", GetNativeText(independent));
                Assert.Equal("aXdY", textBox.Text);
                Assert.Equal((4, 4), GetNativeSelection(independent));
                Assert.Equal((4, 4), GetNativeSelection(handle));
                Assert.Equal((nint)0, SendMessageW(independent, EmCanUndo, 0, 0));
                Assert.Equal((nint)0, SendMessageW(handle, EmCanUndo, 0, 0));
                Assert.Equal((nint)0, SendMessageW(independent, EmGetModify, 0, 0));
                Assert.Equal((nint)0, SendMessageW(handle, EmGetModify, 0, 0));
                Assert.False(textBox.Modified);
                Assert.Equal((nint)32767, SendMessageW(independent, EmGetLimitText, 0, 0));
                Assert.Equal((nint)32767, SendMessageW(handle, EmGetLimitText, 0, 0));

                // An empty buffer is established by EM_CANUNDO and unchanged
                // state; the native EM_UNDO message still returns one here.
                nint nativeUndoResult = SendMessageW(independent, EmUndo, 0, 0);
                Assert.Equal((nint)1, nativeUndoResult);
                nint luminaUndoResult = SendMessageW(handle, EmUndo, 0, 0);
                Assert.Equal((nint)1, luminaUndoResult);
                Assert.Equal(nativeUndoResult, luminaUndoResult);
                Assert.Equal("aXdY", GetNativeText(independent));
                Assert.Equal("aXdY", textBox.Text);
                Assert.Equal(GetNativeSelection(independent), GetNativeSelection(handle));
            }
            finally { Assert.True(DestroyWindow(independent)); }
        });

    [Theory]
    [InlineData(0, 30)]
    [InlineData(120, 0)]
    public void ZeroExtentRestoresOriginalNativeSelectionAfterAppending(int width, int height)
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new UnconstrainedHeightTextBox
            {
                Text = "abcdef", Size = new Size(width, height)
            };
            nint handle = CreateHostedEdit(form, textBox);
            Assert.Equal(width, textBox.Width);
            Assert.Equal(height, textBox.Height);
            textBox.Select(1, 2);

            textBox.AppendText("XY");

            Assert.Equal("abcdefXY", textBox.Text);
            Assert.Equal((1, 3), GetNativeSelection(handle));
            Assert.Equal(1, textBox.SelectionStart);
            Assert.Equal(2, textBox.SelectionLength);
        });

    [Fact]
    public void ExistingEditUsesVirtualSelectedTextAndLengthWithNativeEndMarker()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new ObservingTextBox { Text = "abcdef", Size = new Size(120, 30) };
            nint handle = CreateHostedEdit(form, textBox);
            textBox.Select(1, 2);
            textBox.BypassSelectedText = true;
            textBox.TextLengthReads = 0;

            textBox.AppendText("X");

            Assert.Equal("X", Assert.Single(textBox.SelectedTextWrites));
            Assert.Equal(1, textBox.LengthReadsAtSelectedText);
            Assert.Equal((6, 6), GetNativeSelection(handle));
            Assert.Equal("abcdef", textBox.Text);
            Assert.Equal(7, InvokeEndPosition(textBox));
        });

    [Fact]
    public void EndHookUsesVirtualLengthAndOnlyAddsNativeEndMarkerAfterCreation()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new ObservingTextBox
            {
                Text = "abcd", Size = new Size(120, 30), ReportedTextLength = 4
            };
            Assert.Equal(4, InvokeEndPosition(textBox));
            Assert.False(textBox.IsHandleCreated);
            _ = CreateHostedEdit(form, textBox);
            textBox.ReportedTextLength = 10;
            textBox.TextLengthReads = 0;

            Assert.Equal(11, InvokeEndPosition(textBox));

            Assert.Equal(1, textBox.TextLengthReads);
            Assert.Equal("abcd", textBox.Text);
        });

    private static MethodInfo EndPositionMethod
        => typeof(TextBoxBase).GetMethod("GetEndPosition", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static int InvokeEndPosition(TextBoxBase textBox) => (int)EndPositionMethod.Invoke(textBox, null)!;

    private static nint CreateHostedEdit(Form form, TextBox textBox)
    {
        form.Controls.Add(textBox);
        _ = form.Handle;
        nint handle = textBox.Handle;
        AssertHostedParent(form, handle);
        return handle;
    }

    private static void AssertHostedParent(Form form, nint handle)
    {
        nint parent = GetParent(handle);
        Assert.Equal(form.Handle, parent);
        Assert.Same(form, Control.FromHandle(parent));
    }

    private static (int Start, int End) GetNativeSelection(nint handle)
    {
        _ = SendMessageW(handle, EmGetSel, out int start, out int end);
        return (start, end);
    }

    private static string GetNativeText(nint handle)
    {
        var text = new StringBuilder(16);
        int length = GetWindowTextW(handle, text, text.Capacity);
        Assert.InRange(length, 0, text.Capacity - 2);
        return text.ToString();
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox AppendText test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class ObservingTextBox : TextBox
    {
        internal int TextLengthReads { get; set; }
        internal int TextWrites { get; set; }
        internal int LengthReadsAtSelectedText { get; private set; }
        internal int? ReportedTextLength { get; set; }
        internal int? FailOnLengthRead { get; set; }
        internal Exception? TextLengthFailure { get; set; }
        internal Exception? SelectedTextFailure { get; set; }
        internal bool BypassSelectedText { get; set; }
        internal List<string?> SelectedTextWrites { get; } = new();

        public override int TextLength
        {
            get
            {
                TextLengthReads++;
                if (TextLengthFailure is not null && (FailOnLengthRead is null || TextLengthReads == FailOnLengthRead))
                {
                    throw TextLengthFailure;
                }
                return ReportedTextLength ?? base.TextLength;
            }
        }

        [AllowNull]
        public override string Text
        {
            get => base.Text;
            set { TextWrites++; base.Text = value; }
        }

        [AllowNull]
        public override string SelectedText
        {
            get => base.SelectedText;
            set
            {
                SelectedTextWrites.Add(value);
                LengthReadsAtSelectedText = TextLengthReads;
                if (SelectedTextFailure is not null) { throw SelectedTextFailure; }
                if (!BypassSelectedText) { base.SelectedText = value; }
            }
        }
    }

    private sealed class UnconstrainedHeightTextBox : TextBox
    {
        // The product still has a legacy minimum native height. This fixture
        // keeps a real zero-height HWND to exercise AppendText's existing guard.
        protected override int GetNativeHeight(int requestedHeight) => requestedHeight;
    }

    private sealed class NativeParametersTextBox : TextBox
    {
        internal CreateParams RequestedParameters => CreateParams;
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetParent(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint CreateWindowExW(uint exStyle, string className, string? windowName,
        uint style, int x, int y, int width, int height, nint parent, nint menu, nint instance, nint param);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern int GetWindowTextW(nint hWnd, StringBuilder text, int maxCount);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, string lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint hWnd, int message, out int wParam, out int lParam);
}
