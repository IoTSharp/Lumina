using System.Runtime.ExceptionServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class ControlNativeInputClassificationTests
{
    [Fact]
    public void IsInputKey_DoesNotCreateAHandleBeforeQueryingNativeDialogCode()
        => OnStaThread(() =>
        {
            using var control = new NativeDialogCodeControl();

            Assert.False(control.IsHandleCreated);
            Assert.False(control.InputKey(Keys.Tab));
            Assert.False(control.IsHandleCreated);
        });

    [Fact]
    public void IsInputKey_UsesNativeDialogCodeAndRejectsAltKeys()
        => OnStaThread(() =>
        {
            using var control = new NativeDialogCodeControl
            {
                DialogCode = NativeDialogCodeControl.WantAllKeys
                    | NativeDialogCodeControl.WantTab
                    | NativeDialogCodeControl.WantArrows
            };
            _ = control.Handle;

            Assert.True(control.InputKey(Keys.Tab));
            Assert.True(control.InputKey(Keys.Left));
            Assert.True(control.InputKey(Keys.A));
            Assert.False(control.InputKey(Keys.Alt | Keys.Left));
            Assert.Equal(3, control.DialogCodeQueries);
        });

    [Fact]
    public void IsInputChar_UsesNativeDialogCodeAndCreatesTheRequiredHandle()
        => OnStaThread(() =>
        {
            using var control = new NativeDialogCodeControl
            {
                DialogCode = NativeDialogCodeControl.WantChars
                    | NativeDialogCodeControl.WantAllKeys
            };

            Assert.False(control.IsHandleCreated);
            Assert.True(control.InputChar('x'));
            Assert.True(control.IsHandleCreated);
            Assert.True(control.InputChar('\t'));
            Assert.Equal(2, control.DialogCodeQueries);
        });

    [Fact]
    public void PreProcessMessage_SysCharGoesToDialogRouteWithoutInputCharacterQuery()
    {
        using var control = new NativeDialogCodeControl();
        Message message = Message.Create(0, 0x0106, (nint)'x', 0);

        Assert.True(control.PreProcessMessage(ref message));
        Assert.Equal(0, control.InputCharacterQueries);
        Assert.Equal(1, control.DialogCharacterCalls);
        Assert.False(control.IsHandleCreated);
    }

    [Fact]
    public void PreProcessMessage_NormalCharUsesInputCharacterBeforeDialogRoute()
        => OnStaThread(() =>
        {
            using var control = new NativeDialogCodeControl
            {
                DialogCode = NativeDialogCodeControl.WantChars
                    | NativeDialogCodeControl.WantAllKeys
            };
            Message message = Message.Create(0, 0x0102, (nint)'x', 0);

            Assert.False(control.PreProcessMessage(ref message));
            Assert.Equal(1, control.InputCharacterQueries);
            Assert.Equal(0, control.DialogCharacterCalls);
            Assert.True(control.IsHandleCreated);
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "Native input classification timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class NativeDialogCodeControl : Control
    {
        internal const int WantArrows = 0x0001;
        internal const int WantTab = 0x0002;
        internal const int WantAllKeys = 0x0004;
        internal const int WantChars = 0x0080;

        internal int DialogCode { get; init; }
        internal int DialogCodeQueries { get; private set; }
        internal int InputCharacterQueries { get; private set; }
        internal int DialogCharacterCalls { get; private set; }

        internal bool InputKey(Keys keyData) => IsInputKey(keyData);
        internal bool InputChar(char charCode) => IsInputChar(charCode);

        protected override void WndProc(ref Message message)
        {
            if (message.Msg == 0x0087)
            {
                DialogCodeQueries++;
                message.Result = (nint)DialogCode;
                return;
            }

            base.WndProc(ref message);
        }

        protected override bool IsInputChar(char charCode)
        {
            InputCharacterQueries++;
            return base.IsInputChar(charCode);
        }

        protected override bool ProcessDialogChar(char charCode)
        {
            DialogCharacterCalls++;
            return true;
        }
    }
}
