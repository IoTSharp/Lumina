using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class ControlKeyboardPreprocessingTests
{
    [Fact]
    public void PreviewKeyDownCanPromoteDialogKeyToInputKeyWithoutCreatingHandle()
    {
        using var control = new PreProcessControl();
        PreviewKeyDownEventArgs? observed = null;
        control.PreviewKeyDown += (_, e) =>
        {
            observed = e;
            e.IsInputKey = true;
        };

        Message message = Message.Create(0, 0x0100, (nint)Keys.Tab, 0);

        Assert.False(control.IsHandleCreated);
        Assert.False(control.PreProcess(ref message));
        Assert.False(control.IsHandleCreated);
        Assert.NotNull(observed);
        Assert.Equal(Keys.Tab, observed!.KeyCode);
        Assert.False(observed.Control);
        Assert.Equal(0, control.DialogKeyCalls);
    }

    [Fact]
    public void CommandKeyRunsBeforePreviewAndCanConsumeMessage()
    {
        using var control = new PreProcessControl { ConsumeCommandKey = true };
        int previewCalls = 0;
        control.PreviewKeyDown += (_, _) => previewCalls++;
        Message message = Message.Create(0, 0x0100, (nint)Keys.Control | (nint)Keys.C, 0);

        Assert.True(control.PreProcess(ref message));
        Assert.Equal(1, control.CommandKeyCalls);
        Assert.Equal(0, previewCalls);
        Assert.False(control.IsHandleCreated);
    }

    [Fact]
    public void ParentCommandRouteIsUsedWhenChildDoesNotConsume()
    {
        using var parent = new PreProcessControl { ConsumeCommandKey = true };
        using var child = new PreProcessControl(parent);
        Message message = Message.Create(0, 0x0100, (nint)Keys.Control | (nint)Keys.K, 0);

        Assert.True(child.PreProcess(ref message));
        Assert.Equal(1, child.CommandKeyCalls);
        Assert.Equal(1, parent.CommandKeyCalls);
        Assert.False(child.IsHandleCreated);
        Assert.False(parent.IsHandleCreated);
    }

    [Fact]
    public void InputCharacterAndDialogCharacterFollowStandardOrder()
    {
        using var control = new PreProcessControl { InputCharacter = 'x' };
        Message input = Message.Create(0, 0x0102, (nint)'x', 0);
        Assert.False(control.PreProcess(ref input));
        Assert.Equal(0, control.DialogCharacterCalls);

        Message dialog = Message.Create(0, 0x0106, (nint)'y', 0);
        Assert.False(control.PreProcess(ref dialog));
        Assert.Equal(1, control.DialogCharacterCalls);
        Assert.False(control.IsHandleCreated);
    }

    private sealed class PreProcessControl : Control
    {
        internal PreProcessControl() { }

        internal PreProcessControl(Control parent) => Parent = parent;

        internal bool ConsumeCommandKey { get; set; }
        internal char? InputCharacter { get; set; }
        internal int CommandKeyCalls { get; private set; }
        internal int DialogKeyCalls { get; private set; }
        internal int DialogCharacterCalls { get; private set; }

        internal bool PreProcess(ref Message message) => PreProcessMessage(ref message);

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            CommandKeyCalls++;
            return ConsumeCommandKey || base.ProcessCmdKey(ref msg, keyData);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            DialogKeyCalls++;
            return base.ProcessDialogKey(keyData);
        }

        protected override bool IsInputChar(char charCode)
            => InputCharacter == charCode;

        protected override bool ProcessDialogChar(char charCode)
        {
            DialogCharacterCalls++;
            return base.ProcessDialogChar(charCode);
        }
    }
}
