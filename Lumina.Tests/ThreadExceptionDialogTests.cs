using System.ComponentModel;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public sealed class ThreadExceptionDialogTests
{
    [Fact]
    public void ConstructionKeepsStandardContinueResultAndActualExceptionDetailsWithoutShowingAWindow()
    {
        bool autoInstall = WindowsFormsSynchronizationContext.AutoInstall;
        WindowsFormsSynchronizationContext.AutoInstall = false;
        try
        {
            var exception = new InvalidOperationException("outer failure", new ArgumentException("inner failure"));
            using var dialog = new ThreadExceptionDialog(t: exception);

            Assert.False(dialog.IsHandleCreated);
            Assert.Same(dialog.AcceptButton, dialog.CancelButton);
            Assert.Equal(DialogResult.Cancel, Assert.IsType<Button>(dialog.AcceptButton).DialogResult);
            TextBox details = Assert.Single(dialog.Controls.Cast<Control>().OfType<TextBox>());
            Assert.Contains(exception.ToString(), details.Text);
            Assert.True(details.ReadOnly);
            Assert.False(details.IsHandleCreated);
            Assert.False(details.TabStop);
        }
        finally { WindowsFormsSynchronizationContext.AutoInstall = autoInstall; }
    }

    [Fact]
    public void WarningWithoutHelpHasOnlyTheContinueAction()
    {
        bool autoInstall = WindowsFormsSynchronizationContext.AutoInstall;
        WindowsFormsSynchronizationContext.AutoInstall = false;
        try
        {
            using var dialog = new ThreadExceptionDialog(new WarningException("warning"));
            Button button = Assert.Single(dialog.Controls.Cast<Control>().OfType<Button>());
            Assert.Same(dialog.AcceptButton, button);
            Assert.Equal(DialogResult.Cancel, button.DialogResult);
        }
        finally { WindowsFormsSynchronizationContext.AutoInstall = autoInstall; }
    }
}
