using System.Globalization;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class FormInputLanguageTests
{
    private const int WmInputLanguageChanging = 0x0050;
    private const int WmInputLanguageChanged = 0x0051;

    [Fact]
    public void LanguageConstructorsPreserveIdentityCharacterSetAndCancellation()
        => OnSta(() =>
        {
            InputLanguage language = InputLanguage.CurrentInputLanguage;
            var changed = new InputLanguageChangedEventArgs(language, 0xff);
            var changing = new InputLanguageChangingEventArgs(language, true);

            Assert.Same(language, changed.InputLanguage);
            Assert.Equal(language.Culture, changed.Culture);
            Assert.Equal((byte)0xff, changed.CharSet);
            Assert.Same(language, changing.InputLanguage);
            Assert.Equal(language.Culture, changing.Culture);
            Assert.True(changing.SysCharSet);
            Assert.False(changing.Cancel);
            changing.Cancel = true;
            Assert.True(changing.Cancel);
        });

    [Fact]
    public void CultureConstructorsPreserveTheSuppliedCultureInstance()
        => OnSta(() =>
        {
            CultureInfo culture = new(InputLanguage.CurrentInputLanguage.Culture.Name);
            InputLanguage? expected = InputLanguage.FromCulture(culture);
            Assert.NotNull(expected);
            var changed = new InputLanguageChangedEventArgs(culture, 17);
            var changing = new InputLanguageChangingEventArgs(culture, false);

            Assert.Equal(expected, changed.InputLanguage);
            Assert.Equal(expected, changing.InputLanguage);
            Assert.Same(culture, changed.Culture);
            Assert.Same(culture, changing.Culture);
            Assert.Equal((byte)17, changed.CharSet);
            Assert.False(changing.SysCharSet);
        });

    [Fact]
    public void ConstructorsRejectNullAndAnUnavailableCultureWithStandardParameterNames()
        => OnSta(() =>
        {
            Assert.Equal("inputLanguage", Assert.Throws<ArgumentNullException>(() =>
                new InputLanguageChangedEventArgs((InputLanguage)null!, 0)).ParamName);
            Assert.Equal("inputLanguage", Assert.Throws<ArgumentNullException>(() =>
                new InputLanguageChangingEventArgs((InputLanguage)null!, false)).ParamName);
            Assert.Equal("culture", Assert.Throws<ArgumentNullException>(() =>
                new InputLanguageChangedEventArgs((CultureInfo)null!, 0)).ParamName);
            Assert.Equal("culture", Assert.Throws<ArgumentNullException>(() =>
                new InputLanguageChangingEventArgs((CultureInfo)null!, false)).ParamName);
            Assert.Null(InputLanguage.FromCulture(CultureInfo.InvariantCulture));
            Assert.Equal("culture", Assert.Throws<ArgumentException>(() =>
                new InputLanguageChangedEventArgs(CultureInfo.InvariantCulture, 0)).ParamName);
            Assert.Equal("culture", Assert.Throws<ArgumentException>(() =>
                new InputLanguageChangingEventArgs(CultureInfo.InvariantCulture, false)).ParamName);
        });

    [Fact]
    public void ProtectedHooksUseComponentEventSlotsAndPreserveTheEventArguments()
        => OnSta(() =>
        {
            using var form = new LanguageForm();
            var language = InputLanguage.CurrentInputLanguage;
            var changed = new InputLanguageChangedEventArgs(language, 5);
            var changing = new InputLanguageChangingEventArgs(language, true);
            var trace = new List<string>();
            InputLanguageChangedEventHandler changedHandler = (sender, e) =>
            {
                Assert.Same(form, sender);
                Assert.Same(changed, e);
                trace.Add("changed");
            };
            InputLanguageChangingEventHandler changingHandler = (sender, e) =>
            {
                Assert.Same(form, sender);
                Assert.Same(changing, e);
                e.Cancel = true;
                trace.Add("changing");
            };
            form.InputLanguageChanged += changedHandler;
            form.InputLanguageChanging += changingHandler;

            form.RaiseChanged(changed);
            form.RaiseChanging(changing);
            Assert.True(changing.Cancel);
            form.InputLanguageChanged -= changedHandler;
            form.InputLanguageChanging -= changingHandler;
            form.RaiseChanged(changed);
            form.RaiseChanging(changing);

            Assert.Equal(new[] { "changed", "changing" }, trace);
            Assert.False(form.IsHandleCreated);
        });

    [Fact]
    public void CanRaiseEventsSuppressesSubscribersWhileRetainingVirtualDispatch()
        => OnSta(() =>
        {
            using var form = new LanguageForm { AllowEvents = false };
            int notifications = 0;
            form.InputLanguageChanged += (_, _) => notifications++;
            form.InputLanguageChanging += (_, _) => notifications++;
            InputLanguage language = InputLanguage.CurrentInputLanguage;

            form.RaiseChanged(new InputLanguageChangedEventArgs(language, 0));
            form.RaiseChanging(new InputLanguageChangingEventArgs(language, false));

            Assert.Equal(new[] { "changed-hook", "changing-hook" }, form.Trace);
            Assert.Equal(0, notifications);
            Assert.False(form.IsHandleCreated);
        });

    [Fact]
    public void DescendantChangeNotifiesItsFormAfterImeUpdateAndBeforeDefaultProcessing()
        => OnSta(() =>
        {
            using var form = new LanguageForm();
            using var middle = new Control { Parent = form };
            using var child = new LanguageControl { Parent = middle };
            var language = InputLanguage.CurrentInputLanguage;
            Propagating = ImeMode.Hiragana;
            ImeMode expected = CurrentLanguageIsSupported ? ImeMode.Hiragana : ImeMode.Off;
            form.InputLanguageChanged += (sender, e) =>
            {
                Assert.Same(form, sender);
                Assert.Equal(language.Handle, e.InputLanguage.Handle);
                Assert.Equal(language.Culture, e.Culture);
                Assert.Equal((byte)0x34, e.CharSet);
                Assert.Equal(expected, Propagating);
                form.Trace.Add("changed-event");
            };
            child.DefaultAction = _ => form.Trace.Add("default");

            Message result = child.Dispatch(WmInputLanguageChanged, 0x1234, language.Handle);

            Assert.Equal(new[] { "changed-hook", "changed-event", "default" }, form.Trace);
            Assert.Equal((nint)73, result.Result);
            Assert.Equal((WmInputLanguageChanged, (nint)0x1234, language.Handle), Assert.Single(child.DefaultMessages));
            Assert.False(form.IsHandleCreated);
            Assert.False(middle.IsHandleCreated);
            Assert.False(child.IsHandleCreated);
        });

    [Theory]
    [InlineData(0, false)]
    [InlineData(1, true)]
    [InlineData(256, true)]
    public void AcceptedChangeRequestPreservesArgumentsAndTheDefaultProcedureResult(int wparam, bool sysCharSet)
        => OnSta(() =>
        {
            using var form = new LanguageForm();
            using var child = new LanguageControl { Parent = form };
            var language = InputLanguage.CurrentInputLanguage;
            Propagating = ImeMode.Katakana;
            form.InputLanguageChanging += (_, e) =>
            {
                Assert.Equal(language.Handle, e.InputLanguage.Handle);
                Assert.Equal(sysCharSet, e.SysCharSet);
                Assert.False(e.Cancel);
                Assert.Equal(ImeMode.Katakana, Propagating);
                form.Trace.Add("changing-event");
            };
            form.InputLanguageChanged += (_, _) => Assert.Fail("A request must not synthesize a changed notification.");
            child.DefaultAction = _ => form.Trace.Add("default");

            Message result = child.Dispatch(WmInputLanguageChanging, wparam, language.Handle);

            Assert.Equal(new[] { "changing-hook", "changing-event", "default" }, form.Trace);
            Assert.Equal((nint)73, result.Result);
            Assert.Equal((WmInputLanguageChanging, (nint)wparam, language.Handle), Assert.Single(child.DefaultMessages));
            Assert.False(form.IsHandleCreated);
            Assert.False(child.IsHandleCreated);
        });

    [Fact]
    public void CancelledDescendantRequestSkipsTheDefaultProcedureAndReturnsZero()
        => OnSta(() =>
        {
            using var form = new LanguageForm();
            using var child = new LanguageControl { Parent = form };
            form.InputLanguageChanging += (_, e) => e.Cancel = true;
            form.InputLanguageChanged += (_, _) => Assert.Fail("A cancelled request must not notify a change.");

            Message result = child.Dispatch(WmInputLanguageChanging, 1, InputLanguage.CurrentInputLanguage.Handle);

            Assert.Equal((nint)0, result.Result);
            Assert.Equal(new[] { "changing-hook" }, form.Trace);
            Assert.Empty(child.DefaultMessages);
            Assert.False(form.IsHandleCreated);
            Assert.False(child.IsHandleCreated);
        });

    [Theory]
    [InlineData(WmInputLanguageChanged)]
    [InlineData(WmInputLanguageChanging)]
    public void AFormHandlerExceptionRemainsTheOriginalExceptionAndStopsDefaultProcessing(int message)
        => OnSta(() =>
        {
            using var form = new LanguageForm();
            using var child = new LanguageControl { Parent = form };
            var expected = new InvalidOperationException("input-language handler failure");
            form.InputLanguageChanged += (_, _) => throw expected;
            form.InputLanguageChanging += (_, _) => throw expected;

            Assert.Same(expected, Assert.Throws<InvalidOperationException>(() =>
                child.Dispatch(message, 0, InputLanguage.CurrentInputLanguage.Handle)));

            Assert.Empty(child.DefaultMessages);
            Assert.False(form.IsHandleCreated);
            Assert.False(child.IsHandleCreated);
        });

    [Fact]
    public void NativeFormChangeReachesTheVirtualHookAndSubscriberWithTheCurrentLayout()
        => OnSta(() =>
        {
            using var form = new LanguageForm();
            nint handle = form.Handle;
            Assert.True(IsWindow(handle));
            var language = InputLanguage.CurrentInputLanguage;
            form.Trace.Clear();
            int notifications = 0;
            form.InputLanguageChanged += (sender, e) =>
            {
                Assert.Same(form, sender);
                Assert.Equal(language.Handle, e.InputLanguage.Handle);
                Assert.Equal((byte)128, e.CharSet);
                form.Trace.Add("native-event");
                notifications++;
            };

            SendMessageW(handle, WmInputLanguageChanged, 128, language.Handle);

            Assert.Equal(1, notifications);
            Assert.Equal(new[] { "changed-hook", "native-event" }, form.Trace);
            Assert.True(IsWindow(handle));
        });

    [Fact]
    public void NativeChildRequestCanBeCancelledByItsFormWithoutActivatingAnotherLayout()
        => OnSta(() =>
        {
            using var form = new LanguageForm();
            using var child = new Control { Parent = form };
            nint handle = child.Handle;
            var original = InputLanguage.CurrentInputLanguage;
            int requests = 0;
            form.InputLanguageChanging += (_, e) =>
            {
                Assert.Equal(original.Handle, e.InputLanguage.Handle);
                e.Cancel = true;
                requests++;
            };

            nint result = SendMessageW(handle, WmInputLanguageChanging, 1, original.Handle);

            Assert.Equal((nint)0, result);
            Assert.Equal(1, requests);
            Assert.Equal(original, InputLanguage.CurrentInputLanguage);
            Assert.True(IsWindow(handle));
        });

    private sealed class LanguageForm : Form
    {
        internal bool AllowEvents { get; set; } = true;
        internal List<string> Trace { get; } = [];
        protected override bool CanRaiseEvents => AllowEvents;
        internal void RaiseChanged(InputLanguageChangedEventArgs e) => OnInputLanguageChanged(e);
        internal void RaiseChanging(InputLanguageChangingEventArgs e) => OnInputLanguageChanging(e);
        protected override void OnInputLanguageChanged(InputLanguageChangedEventArgs e)
        {
            Trace.Add("changed-hook");
            base.OnInputLanguageChanged(e);
        }
        protected override void OnInputLanguageChanging(InputLanguageChangingEventArgs e)
        {
            Trace.Add("changing-hook");
            base.OnInputLanguageChanging(e);
        }
    }

    private sealed class LanguageControl : Control
    {
        internal List<(int Message, nint WParam, nint LParam)> DefaultMessages { get; } = [];
        internal Action<Message>? DefaultAction { get; set; }
        internal Message Dispatch(int message, nint wparam, nint lparam)
        {
            Message result = Message.Create(0, message, wparam, lparam);
            WndProc(ref result);
            return result;
        }
        protected override void DefWndProc(ref Message m)
        {
            if (m.HWnd != 0)
            {
                base.DefWndProc(ref m);
                return;
            }
            DefaultMessages.Add((m.Msg, m.WParam, m.LParam));
            DefaultAction?.Invoke(m);
            m.Result = 73;
        }
        protected override void Dispose(bool disposing)
        {
            DefaultAction = null;
            base.Dispose(disposing);
        }
    }

    private static FieldInfo PropagatingField => typeof(Control).GetField("s_propagatingImeMode", BindingFlags.Static | BindingFlags.NonPublic)!;
    private static ImeMode Propagating
    {
        get => (ImeMode)PropagatingField.GetValue(null)!;
        set => PropagatingField.SetValue(null, value);
    }
    private static bool CurrentLanguageIsSupported => ((nuint)GetKeyboardLayout(0) & 0xffff) is
        0x0404 or 0x0804 or 0x0c04 or 0x1004 or 0x1404 or 0x0411 or 0x0412 or 0x0812;

    [DllImport("user32.dll")]
    private static extern nint GetKeyboardLayout(uint threadId);
    [DllImport("user32.dll")]
    private static extern nint SendMessageW(nint hwnd, int message, nint wparam, nint lparam);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint hwnd);

    private static void OnSta(Action action)
    {
        if (!OperatingSystem.IsWindows()) { return; }
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            ImeMode original = Propagating;
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
                Propagating = original;
            }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(20)), "The input-language test STA did not complete.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }
}
