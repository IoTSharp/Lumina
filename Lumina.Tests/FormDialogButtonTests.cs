using System.ComponentModel;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class FormDialogButtonTests
{
    [Fact]
    public void DefaultsAndCancelAssignmentPreserveExistingResults()
    {
        using var form = new DialogForm();
        using var button = new Button();
        IButtonControl contract = button;

        Assert.Null(form.AcceptButton);
        Assert.Null(form.CancelButton);
        Assert.Equal(DialogResult.None, form.DialogResult);
        Assert.Equal(DialogResult.None, contract.DialogResult);
        form.CancelButton = contract;
        Assert.Equal(DialogResult.Cancel, button.DialogResult);
        button.DialogResult = DialogResult.Retry;
        form.CancelButton = contract;
        Assert.Equal(DialogResult.Retry, button.DialogResult);
        button.DialogResult = DialogResult.None;
        form.CancelButton = contract;
        Assert.Equal(DialogResult.Cancel, button.DialogResult);
        Assert.Equal(DialogResult.None, form.DialogResult);
        Assert.False(form.IsHandleCreated);
        Assert.False(button.IsHandleCreated);
    }

    [Fact]
    public void ChangingAcceptButtonNotifiesOnlyActualDefaultTransitions()
    {
        using var form = new DialogForm();
        using var first = new TrackingButton();
        using var second = new TrackingButton();

        form.AcceptButton = first;
        form.AcceptButton = first;
        form.AcceptButton = second;
        form.AcceptButton = null;

        Assert.Equal(new[] { true, false }, first.DefaultNotifications);
        Assert.Equal(new[] { true, false }, second.DefaultNotifications);
        Assert.False(form.DialogKey(Keys.Enter));
    }

    [Fact]
    public void NestedActiveButtonOverridesAcceptButtonUntilSelectionChanges()
    {
        using var form = new DialogForm();
        using var accept = new TrackingButton { Parent = form };
        using var nested = new ContainerControl { Parent = form };
        using var activeButton = new TrackingButton { Parent = nested };
        using var editor = new Control { Parent = nested };
        form.AcceptButton = accept;

        nested.ActiveControl = activeButton;
        Assert.True(form.DialogKey(Keys.Enter));
        Assert.Equal(1, activeButton.Clicks);
        Assert.Equal(0, accept.Clicks);
        Assert.Equal(new[] { true, false }, accept.DefaultNotifications);
        Assert.Equal(new[] { true }, activeButton.DefaultNotifications);

        nested.ActiveControl = editor;
        Assert.True(form.DialogKey(Keys.Enter));
        Assert.Equal(1, accept.Clicks);
        Assert.Equal(new[] { true, false, true }, accept.DefaultNotifications);
        Assert.Equal(new[] { true, false }, activeButton.DefaultNotifications);
        Assert.Same(editor, nested.ActiveControl);
    }

    [Fact]
    public void EnterAndEscapeRouteWithoutChangingSelectionAndIgnoreAltControl()
    {
        using var form = new DialogForm();
        using var accept = new TrackingButton { Parent = form };
        using var cancel = new TrackingButton { Parent = form };
        using var editor = new Control { Parent = form };
        form.ActiveControl = editor;
        form.AcceptButton = accept;
        form.CancelButton = cancel;

        Assert.True(form.DialogKey(Keys.Enter));
        Assert.True(form.DialogKey(Keys.Shift | Keys.Enter));
        Assert.True(form.DialogKey(Keys.Escape));
        Assert.False(form.DialogKey(Keys.Control | Keys.Enter));
        Assert.False(form.DialogKey(Keys.Alt | Keys.Escape));
        Assert.Equal(2, accept.Clicks);
        Assert.Equal(1, cancel.Clicks);
        Assert.Same(editor, form.ActiveControl);
        Assert.False(form.IsHandleCreated);
    }

    [Fact]
    public void NonControlButtonContractMatchesSeparateEnterAndEscapeRules()
    {
        using var form = new DialogForm();
        var button = new ExternalButton();
        form.AcceptButton = button;
        form.CancelButton = button;

        Assert.True(form.DialogKey(Keys.Enter));
        Assert.Equal(0, button.Clicks);
        Assert.True(form.DialogKey(Keys.Escape));
        Assert.Equal(1, button.Clicks);
    }

    [Theory]
    [InlineData("disabled")]
    [InlineData("hidden")]
    [InlineData("hidden-parent")]
    public void PerformClickDoesNotActivateAnUnselectableButton(string state)
    {
        using var parent = new Control();
        using var button = new Button { Parent = parent };
        int clicks = 0;
        button.Click += (_, _) => clicks++;
        if (state == "disabled") { button.Enabled = false; }
        if (state == "hidden") { button.Visible = false; }
        if (state == "hidden-parent") { parent.Visible = false; }

        button.PerformClick();

        Assert.Equal(0, clicks);
        Assert.False(button.IsHandleCreated);
    }

    [Fact]
    public void ClickingButtonAssignsFormResultBeforeClickAndDoesNotCloseModelessForm()
    {
        using var form = new DialogForm();
        using var nested = new ContainerControl { Parent = form };
        using var button = new Button { Parent = nested, DialogResult = DialogResult.OK };
        DialogResult? observed = null;
        button.Click += (_, _) => observed = form.DialogResult;
        Assert.Equal(DialogResult.None, form.DialogResult);
        form.Show();

        button.PerformClick();

        Assert.Equal(DialogResult.OK, observed);
        Assert.Equal(DialogResult.OK, form.DialogResult);
        Assert.True(form.Visible);
        Assert.False(form.IsDisposed);
    }

    [Theory]
    [InlineData(AutoValidate.EnablePreventFocusChange, 0, 1)]
    [InlineData(AutoValidate.EnableAllowFocusChange, 1, 1)]
    [InlineData(AutoValidate.Disable, 1, 0)]
    public void PerformClickHonorsAutomaticValidationPolicy(AutoValidate mode, int expectedClicks, int expectedValidations)
    {
        using var container = new ContainerControl { AutoValidate = mode };
        using var editor = new Control { Parent = container };
        using var button = new Button { Parent = container };
        int validations = 0;
        int clicks = 0;
        editor.Validating += (_, e) => { validations++; e.Cancel = true; };
        button.Click += (_, _) => clicks++;
        container.ActiveControl = editor;

        button.PerformClick();

        Assert.Equal(expectedClicks, clicks);
        Assert.Equal(expectedValidations, validations);
        Assert.Same(editor, container.ActiveControl);
        Assert.False(container.IsHandleCreated);
    }

    [Fact]
    public void ButtonThatDoesNotCauseValidationCanActivateWithoutValidatingEditor()
    {
        using var container = new ContainerControl();
        using var editor = new Control { Parent = container };
        using var button = new Button { Parent = container, CausesValidation = false };
        int validations = 0;
        int clicks = 0;
        editor.Validating += (_, e) => { validations++; e.Cancel = true; };
        button.Click += (_, _) => clicks++;
        container.ActiveControl = editor;

        button.PerformClick();

        Assert.Equal(1, clicks);
        Assert.Equal(0, validations);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(12)]
    public void InvalidDialogResultsAreRejectedWithoutChangingPreviousValue(int value)
    {
        using var form = new DialogForm { DialogResult = DialogResult.Continue };
        using var button = new Button { DialogResult = DialogResult.TryAgain };

        Assert.Equal("value", Assert.Throws<InvalidEnumArgumentException>(
            () => form.DialogResult = (DialogResult)value).ParamName);
        Assert.Equal("value", Assert.Throws<InvalidEnumArgumentException>(
            () => button.DialogResult = (DialogResult)value).ParamName);
        Assert.Equal(DialogResult.Continue, form.DialogResult);
        Assert.Equal(DialogResult.TryAgain, button.DialogResult);
    }

    [Fact]
    public void DefaultNotificationUpdatesNativeCreationStyleWithoutCreatingAHandle()
    {
        using var button = new StyleButton();
        Assert.Equal(0, button.NativeStyle & 1);
        button.NotifyDefault(true);
        Assert.Equal(1, button.NativeStyle & 1);
        button.NotifyDefault(false);
        Assert.Equal(0, button.NativeStyle & 1);
        Assert.False(button.IsHandleCreated);
    }

    private sealed class DialogForm : Form
    {
        internal bool DialogKey(Keys keyData) => ProcessDialogKey(keyData);
    }

    private sealed class TrackingButton : Control, IButtonControl
    {
        public DialogResult DialogResult { get; set; }
        internal int Clicks { get; private set; }
        internal List<bool> DefaultNotifications { get; } = [];
        public void NotifyDefault(bool value) => DefaultNotifications.Add(value);
        public void PerformClick() => Clicks++;
    }

    private sealed class ExternalButton : IButtonControl
    {
        public DialogResult DialogResult { get; set; }
        internal int Clicks { get; private set; }
        public void NotifyDefault(bool value) { }
        public void PerformClick() => Clicks++;
    }

    private sealed class StyleButton : Button
    {
        internal int NativeStyle => CreateParams.Style;
    }
}
