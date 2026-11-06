using System.Runtime.ExceptionServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class ControlImeCharacterTests
{
    private const int Char = 0x0102;
    private const int SysChar = 0x0106;
    private const int ImeStart = 0x010D;
    private const int ImeEnd = 0x010E;
    private const int ImeChar = 0x0286;

    [Fact]
    public void ImeCharactersRaiseOnceAndDuplicateMessagesStillReachDefaultProcessing()
        => OnSta(() =>
        {
            using var control = new ImeControl();
            var characters = new List<char>();
            control.KeyPress += (_, e) => characters.Add(e.KeyChar);

            control.Call(ImeStart);
            control.Call(ImeChar, '中');
            control.Call(ImeChar, '文');
            control.Call(Char, '中');
            control.Call(SysChar, '文');
            control.Call(Char, 'a');

            Assert.Equal(new[] { '中', '文', 'a' }, characters);
            Assert.Equal(new[] { ImeStart, ImeChar, ImeChar, Char, SysChar, Char }, control.DefaultMessages);
        });

    [Fact]
    public void HandledImeCharacterSkipsDefaultAndStillDeduplicatesFollowingCharacter()
        => OnSta(() =>
        {
            using var control = new ImeControl();
            int presses = 0;
            control.KeyPress += (_, e) => { presses++; e.Handled = true; };

            control.Call(ImeChar, '中');
            control.Call(Char, '中');

            Assert.Equal(1, presses);
            Assert.Equal(new[] { Char }, control.DefaultMessages);

            // Fixed WinForms 10.0.11 retains the duplicate count even when
            // the IME KeyPress is handled. It consumes exactly one character.
            control.Call(Char, 'a');
            Assert.Equal(2, presses);
            Assert.Equal(new[] { Char }, control.DefaultMessages);
        });

    [Fact]
    public void ImeCharacterChangeWritesBackButUnchangedCharacterPreservesFullWParam()
        => OnSta(() =>
        {
            using var control = new ImeControl();
            nint original = (nint)0x12344E2D;
            Message unchanged = control.Call(ImeChar, original);
            Assert.Equal(original, unchanged.WParam);
            Assert.Equal(original, control.DefaultWParams[^1]);

            control.KeyPress += (_, e) => e.KeyChar = '新';
            Message changed = control.Call(ImeChar, original);
            Assert.Equal((nint)'新', changed.WParam);
            Assert.Equal((nint)'新', control.DefaultWParams[^1]);
        });

    [Fact]
    public void EndCompositionDisablesCountingUntilStartAndDoesNotEatOrdinaryInput()
        => OnSta(() =>
        {
            using var control = new ImeControl();
            var characters = new List<char>();
            control.KeyPress += (_, e) => characters.Add(e.KeyChar);

            control.Call(ImeChar, '中');
            control.Call(ImeEnd);
            control.Call(ImeChar, '文');
            control.Call(Char, 'a');
            control.Call(Char, 'b');
            control.Call(ImeStart);
            control.Call(ImeChar, '新');
            control.Call(Char, '新');
            control.Call(Char, 'c');

            Assert.Equal(new[] { '中', '文', 'a', 'b', '新', 'c' }, characters);
            Assert.Contains(ImeEnd, control.DefaultMessages);
            Assert.Contains(ImeStart, control.DefaultMessages);
            Assert.Equal(9, control.DefaultMessages.Count);
        });

    [Fact]
    public void StartCompositionResetsUnmatchedCharactersFromPreviousComposition()
        => OnSta(() =>
        {
            using var control = new ImeControl();
            var characters = new List<char>();
            control.KeyPress += (_, e) => characters.Add(e.KeyChar);

            control.Call(ImeChar, '中');
            control.Call(ImeStart);
            control.Call(Char, 'a');

            Assert.Equal(new[] { '中', 'a' }, characters);
            Assert.Equal(new[] { ImeChar, ImeStart, Char }, control.DefaultMessages);
        });

    [Fact]
    public void ImeCharUsesEventArgsOverrideButBypassesOrdinaryKeyMessageAndParentPreview()
        => OnSta(() =>
        {
            using var parent = new ImeControl { ConsumePreview = true };
            using var child = new ImeControl();
            parent.Controls.Add(child);
            int presses = 0;
            child.KeyPress += (_, _) => presses++;

            child.Call(ImeChar, '中');
            Assert.Equal(1, presses);
            Assert.Equal(1, child.EventArgsCalls);
            Assert.Equal(0, child.KeyMessageCalls);
            Assert.Equal(0, parent.PreviewCalls);

            child.Call(Char, 'a');
            Assert.Equal(1, parent.PreviewCalls);
            Assert.Equal(1, child.KeyMessageCalls);
            Assert.Equal(1, child.EventArgsCalls);
        });

    [Fact]
    public void EventArgsOverrideCanConsumeImeMessageBeforeKeyPressAndDefaultProcessing()
        => OnSta(() =>
        {
            using var control = new ImeControl { ConsumeImeEventArgs = true };
            int presses = 0;
            control.KeyPress += (_, _) => presses++;

            control.Call(ImeChar, '中');
            control.Call(Char, 'a');

            Assert.Equal(1, presses);
            Assert.Equal(new[] { Char }, control.DefaultMessages);
        });

    private sealed class ImeControl : Control
    {
        internal List<int> DefaultMessages { get; } = [];
        internal List<nint> DefaultWParams { get; } = [];
        internal int EventArgsCalls { get; private set; }
        internal int KeyMessageCalls { get; private set; }
        internal int PreviewCalls { get; private set; }
        internal bool ConsumePreview { get; init; }
        internal bool ConsumeImeEventArgs { get; init; }

        internal Message Call(int message, nint wParam = 0)
        {
            Message value = Message.Create(Handle, message, wParam, 0);
            WndProc(ref value);
            return value;
        }

        protected override bool ProcessKeyMessage(ref Message message)
        {
            KeyMessageCalls++;
            return base.ProcessKeyMessage(ref message);
        }

        protected override bool ProcessKeyPreview(ref Message message)
        {
            PreviewCalls++;
            return ConsumePreview || base.ProcessKeyPreview(ref message);
        }

        protected override bool ProcessKeyEventArgs(ref Message message)
        {
            EventArgsCalls++;
            return (ConsumeImeEventArgs && message.Msg == ImeChar) || base.ProcessKeyEventArgs(ref message);
        }

        protected override void DefWndProc(ref Message message)
        {
            if (message.Msg is Char or SysChar or ImeChar or ImeStart or ImeEnd)
            {
                DefaultMessages.Add(message.Msg);
                DefaultWParams.Add(message.WParam);
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(20)), "The IME character test STA did not complete.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }
}
