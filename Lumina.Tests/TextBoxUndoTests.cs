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
public sealed class TextBoxUndoTests
{
    private const int EmGetSel = 0x00B0;
    private const int EmSetSel = 0x00B1;
    private const int EmGetModify = 0x00B8;
    private const int EmReplaceSel = 0x00C2;
    private const int EmLimitText = 0x00C5;
    private const int EmCanUndo = 0x00C6;
    private const int EmUndo = 0x00C7;
    private const int EmEmptyUndoBuffer = 0x00CD;

    [Fact]
    public void UndoMembersHaveStandardBaseSignaturesAndCanUndoMetadata()
    {
        PropertyInfo property = typeof(TextBox).GetProperty(nameof(TextBoxBase.CanUndo))!;
        Assert.Equal(typeof(TextBoxBase), property.DeclaringType);
        Assert.Equal(typeof(bool), property.PropertyType);
        Assert.True(property.GetMethod!.IsPublic);
        Assert.False(property.GetMethod.IsVirtual);
        Assert.Null(property.SetMethod);
        Assert.Equal("Behavior", property.GetCustomAttribute<CategoryAttribute>()!.Category);
        Assert.False(property.GetCustomAttribute<BrowsableAttribute>()!.Browsable);
        Assert.Equal(DesignerSerializationVisibility.Hidden,
            property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>()!.Visibility);
        Assert.Null(property.GetCustomAttribute<DefaultValueAttribute>());

        MethodInfo undo = typeof(TextBox).GetMethod(nameof(TextBoxBase.Undo))!;
        Assert.Equal(typeof(TextBoxBase), undo.DeclaringType);
        Assert.True(undo.IsPublic);
        Assert.False(undo.IsVirtual);
        Assert.Equal(typeof(void), undo.ReturnType);
        Assert.Empty(undo.GetParameters());

        MethodInfo clear = typeof(TextBox).GetMethod(nameof(TextBoxBase.ClearUndo))!;
        Assert.Equal(typeof(TextBoxBase), clear.DeclaringType);
        Assert.True(clear.IsPublic);
        Assert.False(clear.IsVirtual);
        Assert.Equal(typeof(void), clear.ReturnType);
        Assert.Empty(clear.GetParameters());
    }

    [Fact]
    public void CanUndoAndClearUndoDoNotCreatePendingHandleOrChangePendingState()
        => OnSta(() =>
        {
            using var textBox = new MessageTextBox { Text = "abcdef", Modified = true };
            textBox.Select(2, 3);
            int textChanges = 0;
            int modifiedChanges = 0;
            textBox.TextChanged += (_, _) => textChanges++;
            textBox.ModifiedChanged += (_, _) => modifiedChanges++;

            Assert.False(textBox.CanUndo);
            textBox.ClearUndo();

            Assert.False(textBox.IsHandleCreated);
            Assert.Empty(textBox.Messages);
            Assert.Equal("abcdef", textBox.Text);
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(3, textBox.SelectionLength);
            Assert.True(textBox.Modified);
            Assert.Equal(0, textChanges);
            Assert.Equal(0, modifiedChanges);
        });

    [Fact]
    public void UndoCreatesPendingHandleAndPreservesTextAndSelectionWhenBufferIsEmpty()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new MessageTextBox { Text = "abcdef", Modified = true };
            textBox.Select(2, 3);
            form.Controls.Add(textBox);
            _ = form.Handle;
            Assert.False(textBox.IsHandleCreated);
            int textChanges = 0;
            textBox.TextChanged += (_, _) => textChanges++;

            textBox.Undo();

            AssertHostedParent(form, textBox.Handle);
            Assert.Equal(new[] { (EmUndo, (nint)0, (nint)0) }, textBox.Messages);
            Assert.Equal("abcdef", textBox.Text);
            Assert.Equal((2, 5), GetNativeSelection(textBox.Handle));
            Assert.True(textBox.Modified);
            Assert.False(textBox.CanUndo);
            Assert.Equal(0, textChanges);
        });

    [Fact]
    public void UndoPreservesOriginalHandleCreationFailureAndPendingState()
        => OnSta(() =>
        {
            using var textBox = new FailingCreationTextBox { Text = "abcdef" };
            textBox.Select(2, 3);
            var failure = new InvalidOperationException("undo handle creation");
            textBox.CreationFailure = failure;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.Undo()));

            Assert.False(textBox.IsHandleCreated);
            Assert.Equal("abcdef", textBox.Text);
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(3, textBox.SelectionLength);
            Assert.False(textBox.CanUndo);
        });

    [Theory]
    [InlineData(0, false)]
    [InlineData(1, true)]
    [InlineData(-1, true)]
    public void CanUndoReadsCurrentNativeResultWithZeroParameters(int response, bool expected)
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new MessageTextBox { Text = "abcdef" };
            nint handle = CreateHostedEdit(form, textBox);
            textBox.Select(2, 3);
            textBox.Messages.Clear();
            textBox.UndoResponse = response;

            Assert.Equal(expected, textBox.CanUndo);

            Assert.Equal(new[] { (EmCanUndo, (nint)0, (nint)0) }, textBox.Messages);
            Assert.Equal(handle, textBox.Handle);
            Assert.Equal("abcdef", textBox.Text);
            Assert.Equal((2, 5), GetNativeSelection(handle));
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void UndoUsesNativeMessageEvenWhenReadOnlyWithoutRecreatingHandle(bool readOnly)
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new MessageTextBox { Text = "abcdef", ReadOnly = readOnly };
            nint handle = CreateHostedEdit(form, textBox);
            textBox.Select(2, 3);
            textBox.Messages.Clear();
            textBox.SuppressUndo = true;

            textBox.Undo();

            Assert.Equal(new[] { (EmUndo, (nint)0, (nint)0) }, textBox.Messages);
            Assert.Equal(handle, textBox.Handle);
            Assert.Equal(readOnly, textBox.ReadOnly);
            Assert.Equal("abcdef", textBox.Text);
            Assert.Equal((2, 5), GetNativeSelection(handle));
        });

    [Fact]
    public void CanUndoFollowsNativeBufferChangesWithoutCaching()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new TextBox { Text = "abcd" };
            nint handle = CreateHostedEdit(form, textBox);
            Assert.False(textBox.CanUndo);

            ReplaceNativeSelection(handle, "X");

            Assert.True(textBox.CanUndo);
            Assert.Equal("aXd", textBox.Text);
            _ = SendMessageW(handle, EmEmptyUndoBuffer, 0, 0);
            Assert.False(textBox.CanUndo);
            Assert.Equal("aXd", textBox.Text);
        });

    [Fact]
    public void ClearUndoDiscardsNativeBufferWithoutChangingTextSelectionModifiedOrNotifications()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new MessageTextBox { Text = "abcd" };
            nint handle = CreateHostedEdit(form, textBox);
            ReplaceNativeSelection(handle, "X");
            Assert.True(textBox.CanUndo);
            Assert.True(textBox.Modified);
            textBox.Select(0, 2);
            int textChanges = 0;
            int modifiedChanges = 0;
            textBox.TextChanged += (_, _) => textChanges++;
            textBox.ModifiedChanged += (_, _) => modifiedChanges++;
            textBox.Messages.Clear();

            textBox.ClearUndo();

            Assert.Equal(new[] { (EmEmptyUndoBuffer, (nint)0, (nint)0) }, textBox.Messages);
            Assert.False(textBox.CanUndo);
            Assert.Equal(handle, textBox.Handle);
            Assert.Equal("aXd", textBox.Text);
            Assert.Equal((0, 2), GetNativeSelection(handle));
            Assert.NotEqual((nint)0, SendMessageW(handle, EmGetModify, 0, 0));
            Assert.True(textBox.Modified);
            Assert.Equal(0, textChanges);
            Assert.Equal(0, modifiedChanges);
        });

    [Fact]
    public void UndoRestoresNativeEditAndRoutesOneTextNotificationWithoutVirtualTextSetter()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new ObservingTextBox { Text = "abcd" };
            nint handle = CreateHostedEdit(form, textBox);
            ReplaceNativeSelection(handle, "X");
            Assert.Equal("aXd", textBox.Text);
            Assert.True(textBox.CanUndo);
            textBox.TextWrites = 0;
            int changes = 0;
            textBox.TextChanged += (_, _) => changes++;

            textBox.Undo();

            Assert.Equal("abcd", textBox.Text);
            Assert.Equal("abcd", GetNativeText(handle));
            Assert.Equal(0, textBox.TextWrites);
            Assert.Equal(1, changes);
            Assert.Equal(handle, textBox.Handle);
        });

    [Fact]
    public void UndoAfterClearUndoLeavesCurrentTextAndRaisesNoTextNotification()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new TextBox { Text = "abcd" };
            nint handle = CreateHostedEdit(form, textBox);
            ReplaceNativeSelection(handle, "X");
            textBox.ClearUndo();
            Assert.False(textBox.CanUndo);
            (int Start, int End) selection = GetNativeSelection(handle);
            int changes = 0;
            textBox.TextChanged += (_, _) => changes++;

            textBox.Undo();

            Assert.Equal("aXd", textBox.Text);
            Assert.Equal(selection, GetNativeSelection(handle));
            Assert.Equal(0, changes);
            Assert.False(textBox.CanUndo);
        });

    [Fact]
    public void ProgrammaticSelectedTextClearsPublicUndoAvailability()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new TextBox { Text = "abcd" };
            nint handle = CreateHostedEdit(form, textBox);
            ReplaceNativeSelection(handle, "X");
            Assert.True(textBox.CanUndo);
            textBox.Select(1, 1);

            textBox.SelectedText = "Y";

            Assert.Equal("aYd", textBox.Text);
            Assert.False(textBox.CanUndo);
            textBox.Undo();
            Assert.Equal("aYd", textBox.Text);
        });

    [Theory]
    [InlineData(32767, "abcd")]
    [InlineData(3, "abd")]
    public void UndoMatchesIndependentEditAfterTemporaryLimitBypass(int maxLength, string expected)
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new MessageTextBox { Text = "abcd", MaxLength = maxLength };
            nint handle = CreateHostedEdit(form, textBox);
            CreateParams parameters = textBox.RequestedParameters;
            Assert.Equal("EDIT", parameters.ClassName);
            nint independent = CreateWindowExW((uint)parameters.ExStyle, "EDIT", parameters.Caption,
                (uint)parameters.Style, parameters.X, parameters.Y, parameters.Width, parameters.Height,
                form.Handle, 0, 0, 0);
            Assert.NotEqual((nint)0, independent);
            try
            {
                AssertHostedParent(form, independent);
                Assert.Equal("abcd", GetNativeText(independent));
                _ = SendMessageW(independent, EmLimitText, maxLength, 0);
                ReplaceWithLimitBypass(independent, maxLength);
                ReplaceWithLimitBypass(handle, maxLength);
                Assert.Equal("aXYZd", textBox.Text);
                Assert.Equal("aXYZd", GetNativeText(independent));
                Assert.True(textBox.CanUndo);
                Assert.NotEqual((nint)0, SendMessageW(independent, EmCanUndo, 0, 0));

                Assert.NotEqual((nint)0, SendMessageW(independent, EmUndo, 0, 0));
                textBox.Undo();

                Assert.Equal(expected, GetNativeText(independent));
                Assert.Equal(expected, textBox.Text);
                Assert.Equal(GetNativeSelection(independent), GetNativeSelection(handle));
                Assert.Equal(SendMessageW(independent, EmGetModify, 0, 0),
                    SendMessageW(handle, EmGetModify, 0, 0));
                Assert.Equal(SendMessageW(independent, EmCanUndo, 0, 0) != 0, textBox.CanUndo);
            }
            finally { Assert.True(DestroyWindow(independent)); }
        });

    private static void ReplaceNativeSelection(nint handle, string text)
    {
        _ = SendMessageW(handle, EmSetSel, 1, 3);
        _ = SendMessageW(handle, EmReplaceSel, -1, text);
    }

    private static void ReplaceWithLimitBypass(nint handle, int maxLength)
    {
        _ = SendMessageW(handle, EmSetSel, 1, 3);
        _ = SendMessageW(handle, EmLimitText, 0, 0);
        _ = SendMessageW(handle, EmReplaceSel, -1, "XYZ");
        _ = SendMessageW(handle, EmLimitText, maxLength, 0);
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
        var text = new StringBuilder(32);
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox undo test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class MessageTextBox : TextBox
    {
        internal List<(int Message, nint WParam, nint LParam)> Messages { get; } = new();
        internal nint? UndoResponse { get; set; }
        internal bool SuppressUndo { get; set; }
        internal CreateParams RequestedParameters => CreateParams;

        protected override void WndProc(ref Message message)
        {
            if (message.Msg is EmCanUndo or EmUndo or EmEmptyUndoBuffer)
            {
                Messages.Add((message.Msg, message.WParam, message.LParam));
            }
            if (message.Msg == EmCanUndo && UndoResponse is nint response)
            {
                message.Result = response;
                return;
            }
            if (message.Msg == EmUndo && SuppressUndo)
            {
                message.Result = 0;
                return;
            }
            base.WndProc(ref message);
        }
    }

    private sealed class ObservingTextBox : TextBox
    {
        internal int TextWrites { get; set; }

        [AllowNull]
        public override string Text
        {
            get => base.Text;
            set { TextWrites++; base.Text = value; }
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
