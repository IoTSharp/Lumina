using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class ControlImeNativeDispatchTests
{
    private const int WmChar = 0x0102;
    private const int WmImeStartComposition = 0x010D;
    private const int WmImeEndComposition = 0x010E;
    private const int WmImeComposition = 0x010F;
    private const int WmImeSetContext = 0x0281;
    private const int WmImeNotify = 0x0282;
    private const int WmImeChar = 0x0286;
    private const int ImnChangeCandidate = 3;
    private const int ImnCloseCandidate = 4;
    private const int ImnOpenCandidate = 5;

    // Fixed WinForms v10.0.11 TextBoxBase.WndProc delegates these messages
    // to Control. Composition/context messages and candidate notifications
    // retain the native default route; they do not define a managed editor.
    [Theory]
    [InlineData(WmImeComposition, 0x12344E2D, 0x40000808)]
    [InlineData(WmImeSetContext, 1, 0x40000001)]
    [InlineData(WmImeNotify, ImnChangeCandidate, 5)]
    [InlineData(WmImeNotify, ImnCloseCandidate, 5)]
    [InlineData(WmImeNotify, ImnOpenCandidate, 5)]
    public void NativeImeMessagesPreserveArgumentsAndDefaultResultWithoutCreatingAHandle(int message, int wParam, int lParam)
        => OnSta(() =>
        {
            using var control = new RecordingControl { ImeMode = ImeMode.AlphaFull };
            int keyCalls = 0;
            int modeChanges = 0;
            control.KeyDown += (_, _) => keyCalls++;
            control.KeyPress += (_, _) => keyCalls++;
            control.KeyUp += (_, _) => keyCalls++;
            control.ImeModeChanged += (_, _) => modeChanges++;

            Message result = control.Dispatch(message, (nint)wParam, (nint)lParam);

            Assert.Equal((nint)73, result.Result);
            Assert.Equal((nint)wParam, result.WParam);
            Assert.Equal((nint)lParam, result.LParam);
            Assert.Equal((message, (nint)wParam, (nint)lParam), Assert.Single(control.DefaultMessages));
            Assert.Equal(ImeMode.AlphaFull, control.ImeMode);
            Assert.Equal(0, keyCalls);
            Assert.Equal(0, modeChanges);
            Assert.False(control.IsHandleCreated);
        });

    [Fact]
    public void CompositionContextAndCandidateMessagesDoNotResetPendingDuplicateCharacters()
        => OnSta(() =>
        {
            using var control = new RecordingControl();
            var characters = new List<char>();
            control.KeyPress += (_, e) => characters.Add(e.KeyChar);

            control.Dispatch(WmImeStartComposition);
            control.Dispatch(WmImeChar, '中');
            control.Dispatch(WmImeComposition, '中', 0x0800);
            control.Dispatch(WmImeSetContext, 1, 0);
            control.Dispatch(WmImeNotify, ImnChangeCandidate, 1);
            control.Dispatch(WmChar, '中');
            control.Dispatch(WmChar, 'a');

            Assert.Equal(new[] { '中', 'a' }, characters);
            Assert.Equal(new[]
            {
                WmImeStartComposition, WmImeChar, WmImeComposition,
                WmImeSetContext, WmImeNotify, WmChar, WmChar
            }, control.DefaultMessages.Select(value => value.Message));
            Assert.False(control.IsHandleCreated);
        });

    [Fact]
    public void NativeCompositionDefaultExceptionRetainsPendingDuplicateState()
        => OnSta(() =>
        {
            using var control = new RecordingControl();
            var characters = new List<char>();
            var expected = new InvalidOperationException("native composition default failure");
            control.KeyPress += (_, e) => characters.Add(e.KeyChar);
            control.Dispatch(WmImeChar, '中');
            control.DefaultAction = message =>
            {
                if (message.Msg == WmImeComposition) { throw expected; }
            };

            Assert.Same(expected, Assert.Throws<InvalidOperationException>(
                () => control.Dispatch(WmImeComposition, '中', 0x0800)));

            control.DefaultAction = null;
            control.Dispatch(WmChar, '中');
            control.Dispatch(WmChar, 'a');
            Assert.Equal(new[] { '中', 'a' }, characters);
            Assert.Equal(4, control.DefaultMessages.Count);
            Assert.False(control.IsHandleCreated);
        });

    [Fact]
    public void CompositionBoundariesCommitCharacterStateBeforeCallingTheDefaultProcedure()
        => OnSta(() =>
        {
            using var control = new RecordingControl();
            var characters = new List<char>();
            control.KeyPress += (_, e) => characters.Add(e.KeyChar);
            control.Dispatch(WmImeChar, '中');
            control.DefaultAction = message =>
            {
                if (message.Msg == WmImeStartComposition)
                {
                    // START clears the previous unmatched duplicate before
                    // the native procedure can synchronously send WM_CHAR.
                    control.Dispatch(WmChar, 'a');
                }
                else if (message.Msg == WmImeEndComposition)
                {
                    // END disables counting before the native procedure can
                    // emit a final IME character without a duplicate WM_CHAR.
                    control.Dispatch(WmImeChar, '文');
                    control.Dispatch(WmChar, 'b');
                }
            };

            control.Dispatch(WmImeStartComposition);
            control.Dispatch(WmImeEndComposition);

            Assert.Equal(new[] { '中', 'a', '文', 'b' }, characters);
            Assert.Equal(new[]
            {
                WmImeChar, WmImeStartComposition, WmChar,
                WmImeEndComposition, WmImeChar, WmChar
            }, control.DefaultMessages.Select(value => value.Message));
            Assert.False(control.IsHandleCreated);
        });

    [Theory]
    [InlineData(WmImeComposition, 0, 0)]
    [InlineData(WmImeSetContext, 0, 0)]
    [InlineData(WmImeNotify, ImnChangeCandidate, 1)]
    public void HiddenNativeEditReceivesImeMessagesThroughItsOriginalDefaultProcedure(int message, int wParam, int lParam)
        => OnSta(() =>
        {
            using var control = new NativeEdit { Visible = false, Text = "unchanged" };
            nint handle = control.Handle;
            Assert.True(IsWindow(handle));
            int presses = 0;
            int modeChanges = 0;
            control.KeyPress += (_, _) => presses++;
            control.ImeModeChanged += (_, _) => modeChanges++;
            control.ProbeMessage = message;
            control.ProbeEnabled = true;

            nint result = SendMessageW(handle, message, (nint)wParam, (nint)lParam);

            NativeCall call = Assert.Single(control.Calls);
            Assert.Equal(handle, call.Handle);
            Assert.Equal(message, call.Message);
            Assert.Equal((nint)wParam, call.WParam);
            Assert.Equal((nint)lParam, call.LParam);
            Assert.Equal(call.NativeResult, result);
            Assert.Equal("unchanged", control.Text);
            Assert.Equal(0, presses);
            Assert.Equal(0, modeChanges);
            Assert.True(IsWindow(handle));
            // Sending an IME message to an unfocused hidden EDIT verifies its
            // dispatch and return path, not composition or a candidate UI.
        });

    private sealed class RecordingControl : Control
    {
        internal List<(int Message, nint WParam, nint LParam)> DefaultMessages { get; } = [];
        internal Action<Message>? DefaultAction { get; set; }

        internal Message Dispatch(int message, nint wParam = 0, nint lParam = 0)
        {
            Message value = Message.Create(0, message, wParam, lParam);
            WndProc(ref value);
            return value;
        }

        protected override void DefWndProc(ref Message message)
        {
            if (message.Msg is WmChar or WmImeStartComposition or WmImeEndComposition
                or WmImeComposition or WmImeSetContext or WmImeNotify or WmImeChar)
            {
                DefaultMessages.Add((message.Msg, message.WParam, message.LParam));
                DefaultAction?.Invoke(message);
                message.Result = 73;
                return;
            }
            base.DefWndProc(ref message);
        }
    }

    private readonly record struct NativeCall(nint Handle, int Message, nint WParam, nint LParam, nint NativeResult);

    private sealed class NativeEdit : TextBox
    {
        internal bool ProbeEnabled { get; set; }
        internal int ProbeMessage { get; set; }
        internal List<NativeCall> Calls { get; } = [];

        protected override void DefWndProc(ref Message message)
        {
            bool record = ProbeEnabled && message.Msg == ProbeMessage;
            nint handle = message.HWnd;
            int messageId = message.Msg;
            nint wParam = message.WParam;
            nint lParam = message.LParam;
            base.DefWndProc(ref message);
            if (record) { Calls.Add(new(handle, messageId, wParam, lParam, message.Result)); }
        }
    }

    private static void OnSta(Action action)
    {
        if (!OperatingSystem.IsWindows()) { return; }
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            bool autoInstall = WindowsFormsSynchronizationContext.AutoInstall;
            WindowsFormsSynchronizationContext.AutoInstall = false;
            try { action(); }
            catch (Exception exception) { failure = exception; }
            finally { WindowsFormsSynchronizationContext.AutoInstall = autoInstall; }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(20)), "The IME native-dispatch test STA did not complete.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint hWnd);
}
