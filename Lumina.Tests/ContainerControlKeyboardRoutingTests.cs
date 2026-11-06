using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public sealed class ContainerControlKeyboardRoutingTests
{
    [Fact]
    public void ProcessDialogKey_TabMovesForwardFromActiveControl()
    {
        using var container = new ExposedContainerControl();
        var first = new Control { TabIndex = 0, TabStop = true };
        var second = new Control { TabIndex = 1, TabStop = true };
        container.Controls.AddRange(first, second);
        container.ActiveControl = first;

        Assert.True(container.InvokeProcessDialogKey(Keys.Tab));
        Assert.Same(second, container.ActiveControl);
    }

    [Fact]
    public void ProcessDialogKey_ShiftTabMovesBackwardAndNonTabFallsThrough()
    {
        using var container = new ExposedContainerControl();
        var first = new Control { TabIndex = 0, TabStop = true };
        var second = new Control { TabIndex = 1, TabStop = true };
        container.Controls.AddRange(first, second);
        container.ActiveControl = second;

        Assert.True(container.InvokeProcessDialogKey(Keys.Shift | Keys.Tab));
        Assert.Same(first, container.ActiveControl);
        Assert.False(container.InvokeProcessDialogKey(Keys.F1));
    }

    [Fact]
    public void ProcessMnemonic_VisitsVisibleEnabledDescendantsAndStopsAfterHandled()
    {
        using var container = new ExposedContainerControl();
        var skipped = new MnemonicControl { Visible = false, HandlesMnemonic = true };
        var handled = new MnemonicControl { HandlesMnemonic = true };
        var after = new MnemonicControl { HandlesMnemonic = true };
        container.Controls.AddRange(skipped, handled, after);

        Assert.True(container.InvokeProcessMnemonic('x'));
        Assert.Equal(0, skipped.InvocationCount);
        Assert.Equal(1, handled.InvocationCount);
        Assert.Equal(0, after.InvocationCount);
    }

    private sealed class ExposedContainerControl : ContainerControl
    {
        public bool InvokeProcessDialogKey(Keys keyData) => ProcessDialogKey(keyData);

        public bool InvokeProcessMnemonic(char charCode) => ProcessMnemonic(charCode);
    }

    private sealed class MnemonicControl : Control
    {
        public bool HandlesMnemonic { get; init; }

        public int InvocationCount { get; private set; }

        protected override bool ProcessMnemonic(char charCode)
        {
            InvocationCount++;
            return HandlesMnemonic;
        }
    }
}
