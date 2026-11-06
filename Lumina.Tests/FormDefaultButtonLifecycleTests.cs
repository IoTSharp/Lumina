using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class FormDefaultButtonLifecycleTests
{
    [Fact]
    public void DisposeClearsDialogButtonReferencesAndReleasesDefaultNotification()
    {
        using var form = new Form();
        var button = new TrackingButton();

        form.AcceptButton = button;
        form.CancelButton = button;

        Assert.Same(button, form.AcceptButton);
        Assert.Same(button, form.CancelButton);
        Assert.Equal(DialogResult.Cancel, button.DialogResult);
        Assert.Equal(new[] { true }, button.DefaultNotifications);

        form.Dispose();

        Assert.Null(form.AcceptButton);
        Assert.Null(form.CancelButton);
        Assert.Equal(new[] { true, false }, button.DefaultNotifications);
    }

    [Fact]
    public void DisposingWithoutManagedCleanupDoesNotMutateDialogButtonState()
    {
        using var form = new TestForm();
        var button = new TrackingButton();
        form.AcceptButton = button;

        form.DisposeNativeOnly();

        Assert.Same(button, form.AcceptButton);
        Assert.Equal(new[] { true }, button.DefaultNotifications);
    }

    private sealed class TestForm : Form
    {
        internal void DisposeNativeOnly() => base.Dispose(false);
    }

    private sealed class TrackingButton : IButtonControl
    {
        internal List<bool> DefaultNotifications { get; } = [];
        public DialogResult DialogResult { get; set; }
        public void NotifyDefault(bool value) => DefaultNotifications.Add(value);
        public void PerformClick() { }
    }
}
