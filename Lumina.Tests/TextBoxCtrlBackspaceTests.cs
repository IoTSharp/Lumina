using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxCtrlBackspaceTests
{
    private const int WmSetRedraw = 0x000B;
    private const int EmGetSel = 0x00B0;
    private const int EmReplaceSel = 0x00C2;
    private const int EmEmptyUndoBuffer = 0x00CD;
    private const int EmSetModify = 0x00B9;

    [Theory]
    [InlineData(Keys.Control | Keys.Back)]
    [InlineData(Keys.Control | Keys.Shift | Keys.Back)]
    public void EmptySelectionAtStartQueriesTextLengthAndConsumesCommandWithoutCreatingHandle(Keys keyData)
        => OnSta(() =>
        {
            var trace = new List<string>();
            using var parent = new RoutingControl { Trace = trace };
            using var textBox = new CommandTextBox { Parent = parent, Text = "alpha beta", Trace = trace };
            textBox.Select(0, 0);
            trace.Clear();
            Message message = CommandMessage();

            Assert.True(textBox.ProcessCommand(ref message, keyData));

            Assert.Equal(new[] { "parent", "shortcuts", "length-get", "text-get", "text-get" }, trace);
            Assert.False(textBox.IsHandleCreated);
            Assert.False(parent.IsHandleCreated);
            Assert.Empty(textBox.EditMessages);
        });

    [Theory]
    [InlineData(Keys.Control | Keys.Back)]
    [InlineData(Keys.Control | Keys.Shift | Keys.Back)]
    public void DisabledShortcutsReturnBeforeReadingSelectionOrCreatingHandle(Keys keyData)
        => OnSta(() =>
        {
            var trace = new List<string>();
            using var parent = new RoutingControl { Trace = trace };
            using var textBox = new CommandTextBox
            {
                Parent = parent, Text = "alpha beta", ShortcutsEnabled = false, Trace = trace,
                LengthGetterFailure = new InvalidOperationException("selection must not be read")
            };
            Message message = CommandMessage();

            Assert.True(textBox.ProcessCommand(ref message, keyData));

            Assert.Equal(new[] { "parent", "shortcuts" }, trace);
            Assert.False(textBox.IsHandleCreated);
            Assert.Empty(textBox.EditMessages);
        });

    [Theory]
    [InlineData(Keys.Back)]
    [InlineData(Keys.Shift | Keys.Back)]
    [InlineData(Keys.Alt | Keys.Back)]
    [InlineData(Keys.Control | Keys.Alt | Keys.Back)]
    [InlineData(Keys.Control | Keys.Shift | Keys.Alt | Keys.Back)]
    [InlineData(Keys.Control | Keys.Delete)]
    public void OtherKeyCombinationsPreserveParentResultAndDoNotReadSelection(Keys keyData)
        => OnSta(() =>
        {
            using var parent = new RoutingControl { ReturnValue = true };
            using var textBox = new CommandTextBox
            {
                Parent = parent, Text = "alpha beta",
                LengthGetterFailure = new InvalidOperationException("selection must not be read")
            };
            Message message = CommandMessage();

            Assert.True(textBox.ProcessCommand(ref message, keyData));
            Assert.Equal(1, parent.Calls);
            Assert.Equal(keyData, parent.LastKeyData);
            Assert.False(textBox.IsHandleCreated);
            Assert.Empty(textBox.EditMessages);
        });

    [Theory]
    [InlineData(false, true, true)]
    [InlineData(true, false, false)]
    public void HiddenDerivedReadOnlyDoesNotChangeBaseCommandGuard(
        bool storedReadOnly, bool hiddenReadOnly, bool expectedHandled)
        => OnSta(() =>
        {
            using var textBox = new CommandTextBox { ReadOnly = hiddenReadOnly };
            textBox.SetStoredReadOnly(storedReadOnly);
            Message message = CommandMessage();

            Assert.Equal(expectedHandled, textBox.ProcessCommand(ref message, Keys.Control | Keys.Back));

            Assert.Equal(storedReadOnly, textBox.StoredReadOnly);
            Assert.Equal(hiddenReadOnly, textBox.ReadOnly);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void ParentHandledResultDoesNotSkipSelectionOrReplaceItsOriginalFailure()
        => OnSta(() =>
        {
            var trace = new List<string>();
            var failure = new InvalidOperationException("selection getter");
            using var parent = new RoutingControl { Trace = trace, ReturnValue = true };
            using var textBox = new CommandTextBox
            {
                Parent = parent, Trace = trace, LengthGetterFailure = failure
            };
            Message message = CommandMessage();

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(
                () => textBox.ProcessCommand(ref message, Keys.Control | Keys.Back)));

            Assert.Equal(new[] { "parent", "shortcuts", "length-get" }, trace);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void ParentFailureStopsBeforeShortcutAndSelectionGetters()
        => OnSta(() =>
        {
            var trace = new List<string>();
            var failure = new InvalidOperationException("parent command");
            using var parent = new RoutingControl { Trace = trace, Failure = failure };
            using var textBox = new CommandTextBox { Parent = parent, Trace = trace };
            Message message = CommandMessage();

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(
                () => textBox.ProcessCommand(ref message, Keys.Control | Keys.Back)));
            Assert.Equal(new[] { "parent" }, trace);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void ShortcutGetterFailureStopsBeforeSelectionEvenWhenParentHandled()
        => OnSta(() =>
        {
            var trace = new List<string>();
            var failure = new InvalidOperationException("shortcut getter");
            using var parent = new RoutingControl { Trace = trace, ReturnValue = true };
            using var textBox = new CommandTextBox
            {
                Parent = parent, Trace = trace, ShortcutGetterFailure = failure
            };
            Message message = CommandMessage();

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(
                () => textBox.ProcessCommand(ref message, Keys.Control | Keys.Back)));
            Assert.Equal(new[] { "parent", "shortcuts" }, trace);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void WordBoundaryUsesVirtualTextAndPropagatesOriginalGetterFailureBeforeRedraw()
        => OnSta(() =>
        {
            var trace = new List<string>();
            var failure = new InvalidOperationException("word boundary text");
            using var parent = new RoutingControl { Trace = trace };
            using var textBox = new CommandTextBox { Parent = parent, Text = "alpha beta" };
            _ = parent.Handle;
            nint handle = textBox.Handle;
            Assert.Equal(parent.Handle, GetParent(handle));
            Assert.Same(parent, Control.FromHandle(GetParent(handle)));
            textBox.Select(10, 0);
            textBox.EditMessages.Clear();
            trace.Clear();
            textBox.Trace = trace;
            textBox.TextFailure = failure;
            Message message = CommandMessage();

            try
            {
                Assert.Same(failure, Assert.Throws<InvalidOperationException>(
                    () => textBox.ProcessCommand(ref message, Keys.Control | Keys.Back)));
            }
            finally { textBox.TextFailure = null; }

            Assert.Equal(new[] { "parent", "shortcuts", "length-get", "text-get" }, trace);
            Assert.True(textBox.IsHandleCreated);
            Assert.Equal(handle, textBox.Handle);
            Assert.Equal("alpha beta", GetNativeText(handle));
            Assert.Equal((10, 10), GetNativeSelection(handle));
            Assert.Empty(textBox.EditMessages);
        });

    [Theory]
    [InlineData(Keys.Control | Keys.Back)]
    [InlineData(Keys.Control | Keys.Shift | Keys.Back)]
    public void ExistingSelectionDeletesOnlySelectionAndKeepsNativeUndo(Keys keyData)
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new CommandTextBox { Text = "abcdef" };
            nint handle = CreateHostedEdit(form, textBox);
            textBox.Select(2, 3);
            textBox.EditMessages.Clear();
            textBox.FailPublicSelectedText = true;
            textBox.FailTextSetter = true;
            int changes = 0;
            textBox.TextChanged += (_, _) => changes++;
            Message message = CommandMessage();

            Assert.True(textBox.ProcessCommand(ref message, keyData));

            Assert.Equal("abf", textBox.Text);
            Assert.Equal("abf", GetNativeText(handle));
            Assert.Equal((2, 2), GetNativeSelection(handle));
            Assert.Equal(new[] { (EmReplaceSel, (nint)(-1)) }, textBox.EditMessages);
            Assert.True(textBox.CanUndo);
            Assert.True(textBox.Modified);
            Assert.Equal(1, changes);
            Assert.Equal(handle, textBox.Handle);

            textBox.Undo();
            Assert.Equal("abcdef", textBox.Text);
            Assert.Equal("abcdef", GetNativeText(handle));
            Assert.Equal(2, changes);
        });

    [Theory]
    [InlineData("alpha beta", 10, "alpha ", 6)]
    [InlineData("alpha beta", 8, "alpha ta", 6)]
    [InlineData("alpha", 5, "", 0)]
    [InlineData("alpha beta tail", 10, "alpha  tail", 6)]
    public void EmptySelectionDeletesPreviousWordWithRedrawPairAndNativeUndo(
        string original, int caret, string expected, int expectedCaret)
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new CommandTextBox { Text = original, MaxLength = 120 };
            nint handle = CreateHostedEdit(form, textBox);
            textBox.Select(caret, 0);
            textBox.EditMessages.Clear();
            textBox.FailPublicSelectedText = true;
            textBox.FailTextSetter = true;
            int changes = 0;
            textBox.TextChanged += (_, _) => changes++;
            Message message = CommandMessage();

            Assert.True(textBox.ProcessCommand(ref message, Keys.Control | Keys.Back));

            Assert.Equal(expected, textBox.Text);
            Assert.Equal(expected, GetNativeText(handle));
            Assert.Equal((expectedCaret, expectedCaret), GetNativeSelection(handle));
            Assert.Equal(new[]
            {
                (WmSetRedraw, (nint)0), (WmSetRedraw, (nint)1), (EmReplaceSel, (nint)(-1))
            }, textBox.EditMessages);
            Assert.Equal(120, textBox.MaxLength);
            Assert.Equal(handle, textBox.Handle);
            Assert.True(textBox.CanUndo);
            Assert.True(textBox.Modified);
            Assert.Equal(1, changes);

            textBox.Undo();
            Assert.Equal(original, GetNativeText(handle));
            Assert.Equal(original, textBox.Text);
            Assert.Equal(2, changes);
        });

    [Fact]
    public void ParentHandledCommandStillDeletesWordAndRetainsMessageMutations()
        => OnSta(() =>
        {
            using var parent = new RoutingControl { ReturnValue = true, MutateMessage = true };
            using var textBox = new CommandTextBox { Parent = parent, Text = "alpha beta" };
            _ = parent.Handle;
            nint handle = textBox.Handle;
            Assert.Equal(parent.Handle, GetParent(handle));
            textBox.Select(10, 0);
            Message message = CommandMessage();

            Assert.True(textBox.ProcessCommand(ref message, Keys.Control | Keys.Shift | Keys.Back));

            Assert.Equal("alpha ", GetNativeText(handle));
            Assert.Equal((6, 6), GetNativeSelection(handle));
            Assert.Equal(1, parent.Calls);
            Assert.Equal((nint)17, message.HWnd);
            Assert.Equal(0x0101, message.Msg);
            Assert.Equal((nint)19, message.WParam);
            Assert.Equal((nint)23, message.LParam);
            Assert.Equal((nint)29, message.Result);
        });

    [Fact]
    public void ReadOnlyCommandLeavesNativeSelectionTextAndUndoBufferUnchanged()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new CommandTextBox { Text = "alpha beta" };
            nint handle = CreateHostedEdit(form, textBox);
            textBox.Select(6, 4);
            textBox.SetStoredReadOnly(true);
            textBox.EditMessages.Clear();
            Message message = CommandMessage();

            Assert.False(textBox.ProcessCommand(ref message, Keys.Control | Keys.Back));

            Assert.Equal("alpha beta", GetNativeText(handle));
            Assert.Equal((6, 10), GetNativeSelection(handle));
            Assert.False(textBox.CanUndo);
            Assert.Empty(textBox.EditMessages);
            Assert.Equal(handle, textBox.Handle);
        });

    [Fact]
    public void SelectionLengthSetterFailurePreservesOriginalAndDoesNotPerformReplacement()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new CommandTextBox { Text = "alpha beta" };
            nint handle = CreateHostedEdit(form, textBox);
            textBox.Select(10, 0);
            textBox.EditMessages.Clear();
            var failure = new InvalidOperationException("selection setter");
            textBox.LengthSetterFailure = failure;
            Message message = CommandMessage();

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(
                () => textBox.ProcessCommand(ref message, Keys.Control | Keys.Back)));

            Assert.Equal("alpha beta", GetNativeText(handle));
            Assert.Equal((6, 6), GetNativeSelection(handle));
            // Fixed source has no finally around the two selection setters. This
            // hidden test HWND is disposed below, rather than claiming redraw recovery.
            Assert.Equal(new[] { (WmSetRedraw, (nint)0) }, textBox.EditMessages);
            Assert.False(textBox.CanUndo);
        });

    [Fact]
    public void ShortcutGetterSupportsOneFiniteReentrantCommandAtStartWithoutCreatingHandle()
        => OnSta(() =>
        {
            var trace = new List<string>();
            using var parent = new RoutingControl { Trace = trace };
            using var textBox = new CommandTextBox { Parent = parent, Trace = trace };
            textBox.OnShortcutRead = () =>
            {
                Message nested = CommandMessage();
                Assert.True(textBox.ProcessCommand(ref nested, Keys.Control | Keys.Back));
            };
            Message message = CommandMessage();

            Assert.True(textBox.ProcessCommand(ref message, Keys.Control | Keys.Back));

            Assert.Equal(new[]
            {
                "parent", "shortcuts", "parent", "shortcuts", "length-get", "text-get", "text-get",
                "length-get", "text-get", "text-get"
            }, trace);
            Assert.Equal(2, parent.Calls);
            Assert.False(textBox.IsHandleCreated);
        });

    private static Message CommandMessage() => Message.Create(0, 0x0100, (nint)Keys.Back, 0);

    private static nint CreateHostedEdit(Form form, TextBox textBox)
    {
        form.Controls.Add(textBox);
        _ = form.Handle;
        nint handle = textBox.Handle;
        Assert.Equal(form.Handle, GetParent(handle));
        Assert.Same(form, Control.FromHandle(GetParent(handle)));
        return handle;
    }

    private static (int Start, int End) GetNativeSelection(nint handle)
    {
        _ = SendMessageW(handle, EmGetSel, out int start, out int end);
        return (start, end);
    }

    private static string GetNativeText(nint handle)
    {
        var text = new StringBuilder(128);
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox Ctrl+Backspace test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class CommandTextBox : TextBox
    {
        internal List<string>? Trace { get; set; }
        internal List<(int Message, nint WParam)> EditMessages { get; } = new();
        internal Exception? TextFailure { get; set; }
        internal Exception? ShortcutGetterFailure { get; set; }
        internal Exception? LengthGetterFailure { get; set; }
        internal Exception? LengthSetterFailure { get; set; }
        internal Action? OnShortcutRead { get; set; }
        internal bool FailPublicSelectedText { get; set; }
        internal bool FailTextSetter { get; set; }
        internal bool StoredReadOnly => base.ReadOnly;
        public new bool ReadOnly { get; set; }

        internal void SetStoredReadOnly(bool value) => base.ReadOnly = value;

        internal bool ProcessCommand(ref Message message, Keys keyData)
            => base.ProcessCmdKey(ref message, keyData);

        public override bool ShortcutsEnabled
        {
            get
            {
                Trace?.Add("shortcuts");
                if (ShortcutGetterFailure is not null) { throw ShortcutGetterFailure; }
                Action? callback = OnShortcutRead;
                OnShortcutRead = null;
                callback?.Invoke();
                return base.ShortcutsEnabled;
            }
            set => base.ShortcutsEnabled = value;
        }

        public override int SelectionLength
        {
            get
            {
                Trace?.Add("length-get");
                if (LengthGetterFailure is not null) { throw LengthGetterFailure; }
                return base.SelectionLength;
            }
            set
            {
                Trace?.Add("length-set");
                if (LengthSetterFailure is not null) { throw LengthSetterFailure; }
                base.SelectionLength = value;
            }
        }

        [AllowNull]
        public override string Text
        {
            get
            {
                Trace?.Add("text-get");
                if (TextFailure is not null) { throw TextFailure; }
                return base.Text;
            }
            set
            {
                if (FailTextSetter) { throw new InvalidOperationException("public Text setter must not run"); }
                base.Text = value;
            }
        }

        [AllowNull]
        public override string SelectedText
        {
            get
            {
                if (FailPublicSelectedText) { throw new InvalidOperationException("public SelectedText getter must not run"); }
                return base.SelectedText;
            }
            set
            {
                if (FailPublicSelectedText) { throw new InvalidOperationException("public SelectedText setter must not run"); }
                base.SelectedText = value;
            }
        }

        protected override void WndProc(ref Message message)
        {
            if (message.Msg is WmSetRedraw or EmReplaceSel or EmEmptyUndoBuffer or EmSetModify)
            {
                EditMessages.Add((message.Msg, message.WParam));
            }
            base.WndProc(ref message);
        }
    }

    private sealed class RoutingControl : Control
    {
        internal List<string>? Trace { get; set; }
        internal bool ReturnValue { get; set; }
        internal bool MutateMessage { get; set; }
        internal Exception? Failure { get; set; }
        internal int Calls { get; private set; }
        internal Keys LastKeyData { get; private set; }

        protected override bool ProcessCmdKey(ref Message message, Keys keyData)
        {
            Calls++;
            LastKeyData = keyData;
            Trace?.Add("parent");
            if (Failure is not null) { throw Failure; }
            if (MutateMessage)
            {
                message.HWnd = (nint)17;
                message.Msg = 0x0101;
                message.WParam = (nint)19;
                message.LParam = (nint)23;
                message.Result = (nint)29;
            }
            return ReturnValue;
        }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetParent(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern int GetWindowTextW(nint hWnd, StringBuilder text, int maxCount);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint hWnd, int message, out int wParam, out int lParam);
}
