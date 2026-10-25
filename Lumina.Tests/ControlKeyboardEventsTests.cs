using System.Runtime.ExceptionServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class ControlKeyboardEventsTests
{
    [Fact]
    public void KeyArgumentsExposeModifiersAndSuppressKeyPressMarksHandled()
    {
        var key = new KeyEventArgs(Keys.Control | Keys.Shift | Keys.A);

        Assert.True(key.Control);
        Assert.True(key.Shift);
        Assert.False(key.Alt);
        Assert.Equal(Keys.A, key.KeyCode);
        Assert.Equal(Keys.Control | Keys.Shift, key.Modifiers);
        Assert.Equal((int)Keys.A, key.KeyValue);
        Assert.False(key.Handled);

        key.SuppressKeyPress = true;
        Assert.True(key.SuppressKeyPress);
        Assert.True(key.Handled);
        key.SuppressKeyPress = false;
        Assert.False(key.SuppressKeyPress);
        Assert.False(key.Handled);

        var character = new KeyPressEventArgs('a');
        Assert.Equal('a', character.KeyChar);
        character.KeyChar = 'A';
        character.Handled = true;
        Assert.Equal('A', character.KeyChar);
        Assert.True(character.Handled);
    }

    [Fact]
    public void KeyMessagesRaiseDownCharAndUpInOrder()
        => OnSta(() =>
        {
            using var control = new KeyboardControl();
            var events = new List<string>();
            Keys? keyDown = null;
            char? keyChar = null;
            Keys? keyUp = null;
            control.KeyDown += (_, e) => { events.Add("down"); keyDown = e.KeyCode; };
            control.KeyPress += (_, e) => { events.Add("press"); keyChar = e.KeyChar; };
            control.KeyUp += (_, e) => { events.Add("up"); keyUp = e.KeyCode; };

            control.Call(0x0100, 0x41, 0);
            control.Call(0x0102, 0x61, 0);
            control.Call(0x0101, 0x41, 0);

            Assert.Equal(new[] { "down", "press", "up" }, events);
            Assert.Equal(Keys.A, keyDown);
            Assert.Equal('a', keyChar);
            Assert.Equal(Keys.A, keyUp);
            Assert.Equal(3, control.DefaultKeyMessages);
        });

    [Fact]
    public void SuppressKeyPressSkipsCharacterButStillRaisesKeyUp()
        => OnSta(() =>
        {
            using var control = new KeyboardControl();
            var events = new List<string>();
            control.KeyDown += (_, e) =>
            {
                events.Add("down");
                e.SuppressKeyPress = true;
            };
            control.KeyPress += (_, _) => events.Add("press");
            control.KeyUp += (_, _) => events.Add("up");

            control.Call(0x0100, 0x41, 0);
            control.Call(0x0102, 0x61, 0);
            control.Call(0x0101, 0x41, 0);

            Assert.Equal(new[] { "down", "up" }, events);
            Assert.Equal(1, control.DefaultKeyMessages);
        });

    [Fact]
    public void HandledKeyDownDoesNotSuppressFollowingCharacterMessage()
        => OnSta(() =>
        {
            using var control = new KeyboardControl();
            var events = new List<string>();
            control.KeyDown += (_, e) =>
            {
                events.Add("down");
                e.Handled = true;
            };
            control.KeyPress += (_, _) => events.Add("press");
            control.KeyUp += (_, _) => events.Add("up");

            control.Call(0x0100, 0x41, 0);
            control.Call(0x0102, 0x61, 0);
            control.Call(0x0101, 0x41, 0);

            Assert.Equal(new[] { "down", "press", "up" }, events);
            Assert.Equal(2, control.DefaultKeyMessages);
        });

    [Fact]
    public void SystemKeyDownUsesAltContextBit()
        => OnSta(() =>
        {
            using var control = new KeyboardControl();
            KeyEventArgs? observed = null;
            control.KeyDown += (_, e) => observed = e;

            control.Call(0x0104, 0x79, (nint)(1L << 29));

            Assert.NotNull(observed);
            Assert.Equal(Keys.F10, observed!.KeyCode);
            Assert.True(observed.Alt);
        });

    private sealed class KeyboardControl : Control
    {
        internal int DefaultKeyMessages { get; private set; }

        internal void Call(int message, nint wParam, nint lParam)
        {
            Message value = Message.Create(Handle, message, wParam, lParam);
            WndProc(ref value);
        }

        protected override void DefWndProc(ref Message message)
        {
            if (message.Msg is 0x0100 or 0x0101 or 0x0102 or 0x0104 or 0x0105)
            {
                DefaultKeyMessages++;
                message.Result = 1;
                return;
            }

            base.DefWndProc(ref message);
        }
    }

    private static void OnSta(Action action)
    {
        if (!OperatingSystem.IsWindows()) { return; }

        Exception? failure = null;
        var thread = new Thread(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            try { action(); }
            catch (Exception exception) { failure = exception; }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(20)), "The keyboard test STA did not complete.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }
}
