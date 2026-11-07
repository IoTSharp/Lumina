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
public sealed class TextBoxSelectedTextTests
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
    public void SelectedTextHasStandardVirtualBasePropertyAndInternalReplacementHook()
    {
        PropertyInfo property = typeof(TextBox).GetProperty(nameof(TextBoxBase.SelectedText))!;
        Assert.Equal(typeof(TextBoxBase), property.DeclaringType);
        Assert.Equal(typeof(string), property.PropertyType);
        Assert.True(property.GetMethod!.IsPublic && property.GetMethod.IsVirtual);
        Assert.True(property.SetMethod!.IsPublic && property.SetMethod.IsVirtual);
        Assert.Equal("Appearance", property.GetCustomAttribute<CategoryAttribute>()!.Category);
        Assert.False(property.GetCustomAttribute<BrowsableAttribute>()!.Browsable);
        Assert.Equal(DesignerSerializationVisibility.Hidden,
            property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>()!.Visibility);
        Assert.NotNull(property.SetMethod.GetParameters()[0].GetCustomAttribute<AllowNullAttribute>());
        NullabilityInfo nullability = new NullabilityInfoContext().Create(property);
        Assert.Equal(NullabilityState.NotNull, nullability.ReadState);
        Assert.Equal(NullabilityState.Nullable, nullability.WriteState);
        Assert.Null(property.GetCustomAttribute<DefaultValueAttribute>());

        MethodInfo replacement = ReplacementMethod;
        Assert.True(replacement.IsAssembly && replacement.IsVirtual);
        Assert.Equal(typeof(void), replacement.ReturnType);
        Assert.Equal(new[] { typeof(string), typeof(bool) },
            replacement.GetParameters().Select(parameter => parameter.ParameterType));
        Assert.Equal(new[] { "text", "clearUndo" },
            replacement.GetParameters().Select(parameter => parameter.Name));
    }

    [Theory]
    [InlineData("", 0, 0, "")]
    [InlineData("abcdef", 2, 3, "cde")]
    [InlineData("甲\r\n乙", 1, 2, "\r\n")]
    [InlineData("ab\U0001F600cd", 2, 2, "\U0001F600")]
    public void PendingGetterReturnsUtf16RangeWithoutCreatingHandle(string text, int start, int length, string selected)
        => OnSta(() =>
        {
            using var textBox = new TextBox { Text = text };
            textBox.Select(start, length);
            int textChanges = 0;
            textBox.TextChanged += (_, _) => textChanges++;

            Assert.Equal(selected, textBox.SelectedText);

            Assert.False(textBox.IsHandleCreated);
            Assert.Equal(start, textBox.SelectionStart);
            Assert.Equal(length, textBox.SelectionLength);
            Assert.Equal(0, textChanges);
        });

    [Fact]
    public void GetterUsesVirtualTextAndDoesNotCallItsSetter()
        => OnSta(() =>
        {
            using var textBox = new ObservingTextBox { Text = "abcdef" };
            textBox.Select(2, 2);
            textBox.ReportedText = "UVWXYZ";
            textBox.ReportedTextLength = 6;
            textBox.TextReads = 0;
            textBox.TextWrites = 0;

            Assert.Equal("WX", textBox.SelectedText);

            Assert.Equal(1, textBox.TextReads);
            Assert.Equal(0, textBox.TextWrites);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void GetterPreservesOriginalVirtualTextFailureAndPendingSelection()
        => OnSta(() =>
        {
            using var textBox = new ObservingTextBox { Text = "abcdef" };
            textBox.Select(2, 2);
            var failure = new InvalidOperationException("selected text getter");
            textBox.ReportedTextLength = 6;
            textBox.TextFailure = failure;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => _ = textBox.SelectedText));

            textBox.TextFailure = null;
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(2, textBox.SelectionLength);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void PendingDirectedRangePreservesStandardSubstringFailureUntilNativeCreationNormalizesIt()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new TextBox { Text = "abcdef" };
            textBox.Select(5, -2);

            // Fixed v10.0.11 keeps the directed pending range; its getter
            // passes the resulting negative length directly to Substring.
            Assert.Equal("length", Assert.Throws<ArgumentOutOfRangeException>(() => _ = textBox.SelectedText).ParamName);
            Assert.Equal(5, textBox.SelectionStart);
            Assert.Equal(-2, textBox.SelectionLength);
            Assert.False(textBox.IsHandleCreated);

            nint handle = CreateHostedEdit(form, textBox);

            Assert.Equal((3, 5), GetNativeSelection(handle));
            Assert.Equal("de", textBox.SelectedText);
        });

    [Fact]
    public void SetterCreatesNativeHandleAndConsumesPendingSelection()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new TextBox { Text = "abcdef" };
            textBox.Select(2, 2);
            form.Controls.Add(textBox);
            _ = form.Handle;
            Assert.False(textBox.IsHandleCreated);
            int textChanges = 0;
            textBox.TextChanged += (_, _) => textChanges++;

            textBox.SelectedText = "X";

            nint handle = textBox.Handle;
            AssertHostedParent(form, handle);
            Assert.Equal("abXef", textBox.Text);
            Assert.Equal((3, 3), GetNativeSelection(handle));
            Assert.Equal(1, textChanges);
            Assert.Equal(string.Empty, textBox.SelectedText);
        });

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void NullAndEmptySetterDeleteSelectionAndKeepNativeHandle(string? replacement)
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new TextBox { Text = "abcdef" };
            nint handle = CreateHostedEdit(form, textBox);
            textBox.Select(2, 2);

            textBox.SelectedText = replacement;

            Assert.Equal(handle, textBox.Handle);
            Assert.Equal("abef", textBox.Text);
            Assert.Equal((2, 2), GetNativeSelection(handle));
            Assert.Equal((nint)0, SendMessageW(handle, EmGetModify, 0, 0));
            Assert.Equal((nint)0, SendMessageW(handle, EmCanUndo, 0, 0));
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ProgrammaticSetterReplacesTextEvenWhenReadOnly(bool readOnly)
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new TextBox { Text = "abcdef", ReadOnly = readOnly };
            nint handle = CreateHostedEdit(form, textBox);
            textBox.Select(1, 3);

            textBox.SelectedText = "甲";

            Assert.Equal("a甲ef", textBox.Text);
            Assert.Equal(readOnly, textBox.ReadOnly);
            Assert.Equal(handle, textBox.Handle);
            Assert.Equal((2, 2), GetNativeSelection(handle));
        });

    [Theory]
    [InlineData("abcdef", 2, 2, "甲乙", "ab甲乙ef", 4)]
    [InlineData("甲\r\n乙", 1, 2, "\r\n中\r\n", "甲\r\n中\r\n乙", 6)]
    [InlineData("ab\U0001F600cd", 2, 2, "\U0001F642", "ab\U0001F642cd", 4)]
    public void MultilineReplacementAndNativeGetterUseUtf16Selection(string text, int start, int length,
        string replacement, string expectedText, int caret)
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new TextBox { Multiline = true, Text = text };
            nint handle = CreateHostedEdit(form, textBox);
            _ = SendMessageW(handle, EmSetSel, start, start + length);
            Assert.Equal(text.Substring(start, length), textBox.SelectedText);

            textBox.SelectedText = replacement;

            Assert.Equal(expectedText, textBox.Text);
            Assert.Equal((caret, caret), GetNativeSelection(handle));
        });

    [Fact]
    public void SetterInsertsAtCollapsedCaretWithoutUsingVirtualTextSetter()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new ObservingTextBox { Text = "abcd" };
            nint handle = CreateHostedEdit(form, textBox);
            textBox.Select(2, 0);
            textBox.TextWrites = 0;

            textBox.SelectedText = "XY";

            Assert.Equal("abXYcd", textBox.Text);
            Assert.Equal(0, textBox.TextWrites);
            Assert.Equal((4, 4), GetNativeSelection(handle));
        });

    [Fact]
    public void SetterTemporarilyBypassesConfiguredInputLimitAndRestoresIt()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new TextBox { Text = "abcdef", MaxLength = 2 };
            nint handle = CreateHostedEdit(form, textBox);
            textBox.Select(2, 2);

            textBox.SelectedText = "甲乙丙丁";

            Assert.Equal("ab甲乙丙丁ef", textBox.Text);
            Assert.Equal((nint)2, SendMessageW(handle, EmGetLimitText, 0, 0));
            Assert.Equal(2, textBox.MaxLength);
            Assert.Equal((6, 6), GetNativeSelection(handle));
        });

    [Fact]
    public void SetterClearsNativeUndoAndDefersModifiedResetNotificationUntilObserved()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new TextBox { Text = "abcd" };
            nint handle = CreateHostedEdit(form, textBox);
            _ = SendMessageW(handle, EmSetSel, 1, 2);
            _ = SendMessageW(handle, EmReplaceSel, -1, "Q");
            Assert.NotEqual((nint)0, SendMessageW(handle, EmCanUndo, 0, 0));
            Assert.True(textBox.Modified);
            textBox.Select(1, 1);
            var trace = new List<string>();
            textBox.ModifiedChanged += (_, _) => trace.Add($"modified:{textBox.Modified}");
            textBox.TextChanged += (_, _) => trace.Add("text");

            textBox.SelectedText = "X";

            Assert.Equal(new[] { "text" }, trace);
            Assert.Equal("aXcd", textBox.Text);
            Assert.Equal((nint)0, SendMessageW(handle, EmCanUndo, 0, 0));
            Assert.Equal((nint)0, SendMessageW(handle, EmGetModify, 0, 0));
            Assert.False(textBox.Modified);
            Assert.Equal(new[] { "text", "modified:False" }, trace);
        });

    [Fact]
    public void InternalReplacementCanRetainNativeUndoAndModifiedState()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new MessageTextBox
            {
                Text = "abcd", MaxLength = 3, Size = new System.Drawing.Size(150, 40)
            };
            nint handle = CreateHostedEdit(form, textBox);
            CreateParams parameters = textBox.RequestedParameters;
            Assert.Equal("EDIT", parameters.ClassName);
            // Form creation has already activated this STA's visual context.
            // Resolve the independent system EDIT in that same active context
            // and use the same full requested native styles and geometry.
            nint independent = CreateWindowExW((uint)parameters.ExStyle, "EDIT", parameters.Caption,
                (uint)parameters.Style, parameters.X, parameters.Y, parameters.Width, parameters.Height,
                form.Handle, 0, 0, 0);
            Assert.NotEqual((nint)0, independent);
            try
            {
                AssertHostedParent(form, independent);
                Assert.Equal("abcd", GetNativeText(independent));
                _ = SendMessageW(independent, EmLimitText, 3, 0);
                _ = SendMessageW(independent, EmSetSel, 1, 3);
                _ = SendMessageW(independent, EmLimitText, 0, 0);
                _ = SendMessageW(independent, EmReplaceSel, -1, "XYZ");
                _ = SendMessageW(independent, EmLimitText, 3, 0);

                textBox.Select(1, 2);
                InvokeReplacement(textBox, "XYZ", clearUndo: false);

                Assert.Equal("aXYZd", GetNativeText(independent));
                Assert.Equal("aXYZd", textBox.Text);
                Assert.NotEqual((nint)0, SendMessageW(independent, EmGetModify, 0, 0));
                Assert.True(textBox.Modified);
                Assert.NotEqual((nint)0, SendMessageW(independent, EmCanUndo, 0, 0));
                Assert.NotEqual((nint)0, SendMessageW(handle, EmCanUndo, 0, 0));
                Assert.Equal((nint)3, SendMessageW(independent, EmGetLimitText, 0, 0));
                Assert.Equal((nint)3, SendMessageW(handle, EmGetLimitText, 0, 0));
                Assert.NotEqual((nint)0, SendMessageW(independent, EmUndo, 0, 0));
                Assert.NotEqual((nint)0, SendMessageW(handle, EmUndo, 0, 0));
                // Undo must respect the restored limit: only 'b' fits when
                // the original 'bc' is reinserted into the remaining 'ad'.
                Assert.Equal("abd", GetNativeText(independent));
                Assert.Equal("abd", textBox.Text);
                Assert.Equal(GetNativeText(independent), textBox.Text);
            }
            finally { Assert.True(DestroyWindow(independent)); }
        });

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ReplacementUsesStandardNativeMessageSequenceWithoutRecreating(bool clearUndo)
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new MessageTextBox { Text = "abcd", MaxLength = 7 };
            nint handle = CreateHostedEdit(form, textBox);
            textBox.Select(1, 2);
            textBox.Messages.Clear();

            if (clearUndo) { textBox.SelectedText = "X"; }
            else { InvokeReplacement(textBox, "X", clearUndo: false); }

            (int, nint)[] expected = clearUndo
                ? [(EmLimitText, 0), (EmReplaceSel, 0), (EmSetModify, 0), (EmEmptyUndoBuffer, 0), (EmLimitText, 7)]
                : [(EmLimitText, 0), (EmReplaceSel, -1), (EmLimitText, 7)];
            Assert.Equal(expected, textBox.Messages);
            Assert.Equal(handle, textBox.Handle);
            Assert.Equal("aXd", textBox.Text);
        });

    [Fact]
    public void SetterPreservesOriginalHandleCreationFailureBeforeReplacingText()
        => OnSta(() =>
        {
            using var textBox = new FailingCreationTextBox { Text = "abcdef" };
            textBox.Select(2, 2);
            var failure = new InvalidOperationException("selected text handle creation");
            textBox.CreationFailure = failure;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.SelectedText = "X"));

            Assert.False(textBox.IsHandleCreated);
            Assert.Equal("abcdef", textBox.Text);
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(2, textBox.SelectionLength);
        });

    private static MethodInfo ReplacementMethod
        => typeof(TextBoxBase).GetMethod("SetSelectedTextInternal", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static void InvokeReplacement(TextBoxBase textBox, string? text, bool clearUndo)
    {
        try { ReplacementMethod.Invoke(textBox, [text, clearUndo]); }
        catch (TargetInvocationException exception) when (exception.InnerException is not null)
        {
            ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
        }
    }

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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox SelectedText test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class ObservingTextBox : TextBox
    {
        internal string? ReportedText { get; set; }
        internal int? ReportedTextLength { get; set; }
        internal Exception? TextFailure { get; set; }
        internal int TextReads { get; set; }
        internal int TextWrites { get; set; }

        [AllowNull]
        public override string Text
        {
            get
            {
                TextReads++;
                if (TextFailure is not null) { throw TextFailure; }
                return ReportedText ?? base.Text;
            }
            set { TextWrites++; base.Text = value; }
        }

        public override int TextLength => ReportedTextLength ?? base.TextLength;
    }

    private sealed class MessageTextBox : TextBox
    {
        internal List<(int Message, nint WParam)> Messages { get; } = new();
        internal CreateParams RequestedParameters => CreateParams;

        protected override void WndProc(ref Message message)
        {
            if (message.Msg is EmLimitText or EmReplaceSel or EmSetModify or EmEmptyUndoBuffer)
            {
                Messages.Add((message.Msg, message.WParam));
            }
            base.WndProc(ref message);
        }
    }

    private sealed class FailingCreationTextBox : TextBox
    {
        internal Exception? CreationFailure { get; set; }

        protected override void CreateHandle()
        {
            if (CreationFailure is not null) { throw CreationFailure; }
            base.CreateHandle();
        }
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
