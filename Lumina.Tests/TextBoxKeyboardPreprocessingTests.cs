using System.Runtime.ExceptionServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxKeyboardPreprocessingTests
{
    [Fact]
    public void MultilineEnterUsesAcceptsReturnWithoutCreatingHandle()
        => OnStaThread(() =>
        {
            using var textBox = new InputTextBox { Multiline = true };

            Assert.False(textBox.InputKey(Keys.Enter));
            textBox.AcceptsReturn = true;
            Assert.True(textBox.InputKey(Keys.Enter));
            Assert.True(textBox.InputKey(Keys.Control | Keys.Enter));
            Assert.False(textBox.InputKey(Keys.Alt | Keys.Enter));
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void TabRequiresMultilineAcceptsTabAndNoControlModifier()
        => OnStaThread(() =>
        {
            using var textBox = new InputTextBox { AcceptsTab = true };

            Assert.False(textBox.InputKey(Keys.Tab));
            textBox.Multiline = true;
            Assert.True(textBox.InputKey(Keys.Tab));
            Assert.True(textBox.InputKey(Keys.Shift | Keys.Tab));
            Assert.False(textBox.InputKey(Keys.Control | Keys.Tab));
            Assert.False(textBox.InputKey(Keys.Alt | Keys.Tab));
            textBox.AcceptsTab = false;
            Assert.False(textBox.InputKey(Keys.Tab));
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void EditingNavigationKeysAreInputKeysWithoutCreatingHandle()
        => OnStaThread(() =>
        {
            using var textBox = new InputTextBox();

            Assert.True(textBox.InputKey(Keys.Back));
            Assert.True(textBox.InputKey(Keys.Home));
            Assert.True(textBox.InputKey(Keys.End));
            Assert.True(textBox.InputKey(Keys.PageUp));
            Assert.True(textBox.InputKey(Keys.PageDown));
            Assert.True(textBox.InputKey(Keys.Control | Keys.Home));
            Assert.False(textBox.InputKey(Keys.Alt | Keys.Home));
            textBox.ReadOnly = true;
            Assert.False(textBox.InputKey(Keys.Back));
            Assert.True(textBox.InputKey(Keys.Home));
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void MultilineEscapeRemainsADialogKey()
        => OnStaThread(() =>
        {
            using var textBox = new InputTextBox { Multiline = true, AcceptsReturn = true, AcceptsTab = true };

            Assert.False(textBox.InputKey(Keys.Escape));
            Assert.False(textBox.InputKey(Keys.Control | Keys.Escape));
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void CtrlTabForAcceptsTabRemovesOnlyControlBeforeParentDialogRoute()
        => OnStaThread(() =>
        {
            using var parent = new DialogParent();
            using var textBox = new InputTextBox { Parent = parent, AcceptsTab = true };

            Assert.True(textBox.DialogKey(Keys.Control | Keys.Shift | Keys.Tab));
            Assert.Equal(Keys.Shift | Keys.Tab, parent.LastDialogKey);
            textBox.AcceptsTab = false;
            Assert.True(textBox.DialogKey(Keys.Control | Keys.Tab));
            Assert.Equal(Keys.Control | Keys.Tab, parent.LastDialogKey);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void PreprocessingLeavesAcceptedEnterForDispatchAndRoutesRejectedEnter()
        => OnStaThread(() =>
        {
            using var parent = new DialogParent();
            using var textBox = new InputTextBox { Parent = parent, Multiline = true, AcceptsReturn = true };
            Message message = Message.Create(0, 0x0100, (nint)Keys.Enter, 0);

            Assert.False(textBox.PreProcessMessage(ref message));
            Assert.Equal(0, parent.DialogCalls);
            textBox.AcceptsReturn = false;
            Assert.True(textBox.PreProcessMessage(ref message));
            Assert.Equal(Keys.Enter, parent.LastDialogKey);
            Assert.Equal(1, parent.DialogCalls);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void MultilineReturnAndTabRulesOverrideNativeEditDialogCode()
        => OnStaThread(() =>
        {
            using var textBox = new InputTextBox { Multiline = true };
            _ = textBox.Handle;

            Assert.False(textBox.InputKey(Keys.Enter));
            Assert.False(textBox.InputKey(Keys.Tab));
            Assert.False(textBox.InputKey(Keys.Escape));
            textBox.AcceptsReturn = true;
            textBox.AcceptsTab = true;
            Assert.True(textBox.InputKey(Keys.Enter));
            Assert.True(textBox.InputKey(Keys.Tab));
            Assert.False(textBox.InputKey(Keys.Control | Keys.Tab));
            Assert.False(textBox.InputKey(Keys.Alt | Keys.Enter));
        });

    [Fact]
    public void MultilineAcceptsReturnPreventsFormAcceptButtonActivation()
        => OnStaThread(() =>
        {
            using var form = new Form();
            using var accept = new TrackingButton();
            using var textBox = new InputTextBox { Parent = form, Multiline = true, AcceptsReturn = true };
            form.AcceptButton = accept;
            Message message = Message.Create(0, 0x0100, (nint)Keys.Enter, 0);

            Assert.False(textBox.PreProcessMessage(ref message));
            Assert.Equal(0, accept.Clicks);
            textBox.AcceptsReturn = false;
            Assert.True(textBox.PreProcessMessage(ref message));
            Assert.Equal(1, accept.Clicks);
            Assert.False(textBox.IsHandleCreated);
        });

    private static void OnStaThread(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            try { action(); }
            catch (Exception exception) { failure = exception; }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox keyboard preprocessing timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class InputTextBox : TextBox
    {
        internal bool InputKey(Keys keyData) => IsInputKey(keyData);
        internal bool DialogKey(Keys keyData) => ProcessDialogKey(keyData);
    }

    private sealed class TrackingButton : Control, IButtonControl
    {
        public DialogResult DialogResult { get; set; }
        internal int Clicks { get; private set; }
        internal bool IsDefault { get; private set; }
        public void NotifyDefault(bool value) => IsDefault = value;
        public void PerformClick() => Clicks++;
    }

    private sealed class DialogParent : Control
    {
        internal Keys LastDialogKey { get; private set; }
        internal int DialogCalls { get; private set; }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            LastDialogKey = keyData;
            DialogCalls++;
            return true;
        }
    }
}
