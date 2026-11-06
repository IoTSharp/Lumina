using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class ControlImeLanguageTests
{
    private const int WmInputLangChangeRequest = 0x0050;
    private const int WmInputLangChange = 0x0051;
    private const int WmKillFocus = 0x0008;
    private const int WmImeNotify = 0x0282;

    [Fact]
    public void LanguageChangeUpdatesPropagationBeforeDefaultProcessingWithoutChangingTheLocalMode()
        => OnSta(() =>
        {
            using var control = new MessageControl { ImeMode = ImeMode.AlphaFull };
            Propagating = ImeMode.Hiragana;
            int changes = 0;
            control.ImeModeChanged += (_, _) => changes++;
            ImeMode expected = CurrentLanguageIsSupported ? ImeMode.Hiragana : ImeMode.Off;
            control.DefaultAction = _ => Assert.Equal(expected, Propagating);

            Message result = control.Dispatch(WmInputLangChange, 0x1234, 0x5678);

            Assert.Equal((nint)73, result.Result);
            Assert.Equal((WmInputLangChange, (nint)0x1234, (nint)0x5678), Assert.Single(control.DefaultMessages));
            Assert.Equal(ImeMode.AlphaFull, control.ImeMode);
            Assert.Equal(0, changes);
            Assert.False(control.IsHandleCreated);
        });

    [Fact]
    public void LanguageChangePreservesTheOriginalDefaultProcedureException()
        => OnSta(() =>
        {
            using var control = new MessageControl { ImeMode = ImeMode.Close };
            Propagating = ImeMode.Hangul;
            var expected = new InvalidOperationException("language default failure");
            control.DefaultAction = _ => throw expected;

            Assert.Same(expected, Assert.Throws<InvalidOperationException>(() => control.Dispatch(WmInputLangChange)));

            Assert.Equal(CurrentLanguageIsSupported ? ImeMode.Hangul : ImeMode.Off, Propagating);
            Assert.Equal(ImeMode.Close, control.ImeMode);
            Assert.False(IgnoringNotify);
        });

    [Fact]
    public void LanguageChangeRequestRetainsTheNativeDefaultRouteAndMessageArguments()
        => OnSta(() =>
        {
            using var control = new MessageControl { ImeMode = ImeMode.Alpha };
            Propagating = ImeMode.Hangul;

            Message result = control.Dispatch(WmInputLangChangeRequest, 1, 0x409);

            Assert.Equal((nint)73, result.Result);
            Assert.Equal((WmInputLangChangeRequest, (nint)1, (nint)0x409), Assert.Single(control.DefaultMessages));
            Assert.Equal(ImeMode.Hangul, Propagating);
            Assert.Equal(ImeMode.Alpha, control.ImeMode);
            Assert.False(control.IsHandleCreated);
        });

    [Fact]
    public void LeavingAnUnmanagedHostRestoresTheRootThenResetsPropagationBeforeLostFocus()
        => OnSta(() =>
        {
            // Only restore the host handle here. Showing it would also create
            // visible descendants, which is a separate lifecycle operation.
            using var root = new Control { Visible = false };
            using var middle = new Control();
            using var control = new MessageControl { ImeMode = ImeMode.Disable };
            root.Controls.Add(middle);
            middle.Controls.Add(control);
            var trace = new List<string>();
            root.HandleCreated += (_, _) =>
            {
                Assert.True(IgnoringNotify);
                Assert.Equal(ImeMode.Alpha, Propagating);
                trace.Add("root");
            };
            control.DefaultAction = _ =>
            {
                Assert.False(IgnoringNotify);
                Assert.Equal(ImeMode.Inherit, Propagating);
                trace.Add("default");
            };
            control.LostFocus += (_, _) =>
            {
                Assert.False(IgnoringNotify);
                Assert.Equal(ImeMode.Inherit, Propagating);
                trace.Add("lost");
            };
            Propagating = ImeMode.Alpha;

            control.Dispatch(WmKillFocus);

            Assert.Equal(["root", "default", "lost"], trace);
            Assert.True(root.IsHandleCreated);
            Assert.False(middle.IsHandleCreated);
            Assert.False(control.IsHandleCreated);
            Assert.Equal(ImeMode.Disable, control.ImeMode);
            Assert.Equal(ImeMode.NoControl, root.ImeMode);
        });

    [Fact]
    public void AnUninitializedPropagationCacheDoesNotCreateTheHostHandleOnFocusLoss()
        => OnSta(() =>
        {
            using var root = new Control();
            using var control = new MessageControl();
            root.Controls.Add(control);
            Propagating = ImeMode.Inherit;
            int lost = 0;
            control.LostFocus += (_, _) => lost++;

            control.Dispatch(WmKillFocus);

            Assert.Equal(ImeMode.Inherit, Propagating);
            Assert.False(IgnoringNotify);
            Assert.False(root.IsHandleCreated);
            Assert.False(control.IsHandleCreated);
            Assert.Equal(1, lost);
            Assert.Single(control.DefaultMessages);
        });

    [Fact]
    public void AnOrdinaryFormRootRetainsPropagationAndDoesNotUseTheUnmanagedHostPath()
        => OnSta(() =>
        {
            using var form = new Form();
            using var container = new Control();
            using var control = new MessageControl { ImeMode = ImeMode.Disable };
            form.Controls.Add(container);
            container.Controls.Add(control);
            Propagating = ImeMode.Hiragana;

            control.Dispatch(WmKillFocus);

            Assert.Equal(ImeMode.Hiragana, Propagating);
            Assert.False(IgnoringNotify);
            Assert.False(form.IsHandleCreated);
            Assert.False(container.IsHandleCreated);
            Assert.False(control.IsHandleCreated);
            Assert.Single(control.DefaultMessages);
        });

    [Fact]
    public void HostRestorationFailureReleasesTheNotificationGuardAndPreservesTheOriginalException()
        => OnSta(() =>
        {
            var expected = new InvalidOperationException("host creation failure");
            using var root = new FailingHost(expected);
            using var control = new MessageControl();
            root.Controls.Add(control);
            Propagating = ImeMode.Alpha;
            int lost = 0;
            control.LostFocus += (_, _) => lost++;

            Assert.Same(expected, Assert.Throws<InvalidOperationException>(() => control.Dispatch(WmKillFocus)));

            Assert.True(root.SawNotificationGuard);
            Assert.False(IgnoringNotify);
            Assert.Equal(ImeMode.Alpha, Propagating);
            Assert.Equal(0, lost);
            Assert.Empty(control.DefaultMessages);
            Assert.False(root.IsHandleCreated);
        });

    [Fact]
    public void HostExitKeepsItsCommittedResetWhenTheDefaultProcedureThrows()
        => OnSta(() =>
        {
            using var root = new Control { Visible = false };
            using var control = new MessageControl();
            root.Controls.Add(control);
            Propagating = ImeMode.Alpha;
            var expected = new InvalidOperationException("focus default failure");
            control.DefaultAction = _ => throw expected;

            Assert.Same(expected, Assert.Throws<InvalidOperationException>(() => control.Dispatch(WmKillFocus)));

            Assert.Equal(ImeMode.Inherit, Propagating);
            Assert.False(IgnoringNotify);
            Assert.True(root.IsHandleCreated);
        });

    [Fact]
    public void HostRestorationSuppressesImeCacheNotificationsButRetainsDefaultProcessing()
        => OnSta(() =>
        {
            using var root = new Control { Visible = false };
            using var control = new MessageControl { ImeMode = ImeMode.Katakana };
            root.Controls.Add(control);
            int changes = 0;
            control.ImeModeChanged += (_, _) => changes++;
            root.HandleCreated += (_, _) =>
            {
                Assert.True(IgnoringNotify);
                control.Dispatch(WmImeNotify, 0x0008);
                Assert.Equal(ImeMode.Katakana, control.ImeMode);
                Assert.Equal(ImeMode.Alpha, Propagating);
            };
            Propagating = ImeMode.Alpha;

            control.Dispatch(WmKillFocus);

            Assert.Equal(0, changes);
            Assert.Equal(2, control.DefaultMessages.Count);
            Assert.Equal(WmImeNotify, control.DefaultMessages[0].Message);
            Assert.Equal(WmKillFocus, control.DefaultMessages[1].Message);
            Assert.False(IgnoringNotify);
        });

    private static FieldInfo PropagatingField => typeof(Control).GetField("s_propagatingImeMode", BindingFlags.Static | BindingFlags.NonPublic)!;
    private static FieldInfo IgnoreNotifyField => typeof(Control).GetField("s_ignoreWmImeNotify", BindingFlags.Static | BindingFlags.NonPublic)!;
    private static ImeMode Propagating
    {
        get => (ImeMode)PropagatingField.GetValue(null)!;
        set => PropagatingField.SetValue(null, value);
    }
    private static bool IgnoringNotify => (bool)IgnoreNotifyField.GetValue(null)!;
    private static bool CurrentLanguageIsSupported => ((nuint)GetKeyboardLayout(0) & 0xffff) is
        0x0404 or 0x0804 or 0x0c04 or 0x1004 or 0x1404 or 0x0411 or 0x0412 or 0x0812;

    private sealed class MessageControl : Control
    {
        internal List<(int Message, nint WParam, nint LParam)> DefaultMessages { get; } = [];
        internal Action<Message>? DefaultAction { get; set; }
        internal Message Dispatch(int message, nint wparam = 0, nint lparam = 0)
        {
            // These direct managed probes use no HWND. Real creation/disposal
            // callbacks retain their nonzero HWND and their native procedure.
            Message value = Message.Create(0, message, wparam, lparam);
            WndProc(ref value);
            return value;
        }
        protected override void DefWndProc(ref Message message)
        {
            if (message.HWnd != 0)
            {
                base.DefWndProc(ref message);
                return;
            }
            DefaultMessages.Add((message.Msg, message.WParam, message.LParam));
            DefaultAction?.Invoke(message);
            message.Result = 73;
        }
        protected override void Dispose(bool disposing)
        {
            DefaultAction = null;
            base.Dispose(disposing);
        }
    }

    private sealed class FailingHost(Exception failure) : Control
    {
        internal bool SawNotificationGuard { get; private set; }
        protected override void CreateHandle()
        {
            SawNotificationGuard = IgnoringNotify;
            throw failure;
        }
    }

    [DllImport("user32.dll")]
    private static extern nint GetKeyboardLayout(uint threadId);

    private static void OnSta(Action action)
    {
        if (!OperatingSystem.IsWindows()) { return; }
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            ImeMode originalPropagating = Propagating;
            bool originalIgnore = IgnoringNotify;
            var nativeFailures = new List<Exception>();
            void Report(object sender, ThreadExceptionEventArgs e) => nativeFailures.Add(e.Exception);
            Application.ThreadException += Report;
            try
            {
                action();
                Assert.Empty(nativeFailures);
            }
            catch (Exception exception) { failure = exception; }
            finally
            {
                Application.ThreadException -= Report;
                Propagating = originalPropagating;
                IgnoreNotifyField.SetValue(null, originalIgnore);
            }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(20)), "The IME language test STA did not complete.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }
}
