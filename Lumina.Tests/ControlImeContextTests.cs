using System.ComponentModel;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class ControlImeContextTests
{
    [Fact]
    public void ImeModeInheritsWithoutCreatingHandlesAndResetRestoresTheCurrentParent()
        => OnSta(() =>
        {
            using var first = new Control { ImeMode = ImeMode.Hiragana };
            using var second = new Control { ImeMode = ImeMode.Hangul };
            using var child = new ImeControl();
            Assert.Equal(ImeMode.NoControl, child.ImeMode);
            first.Controls.Add(child);
            Assert.Equal(ImeMode.Hiragana, child.ImeMode);
            child.ImeMode = ImeMode.Alpha;
            second.Controls.Add(child);
            Assert.Equal(ImeMode.Alpha, child.ImeMode);
            child.ResetImeMode();
            Assert.Equal(ImeMode.Hangul, child.ImeMode);
            second.ImeMode = ImeMode.Close;
            Assert.Equal(ImeMode.Close, child.ImeMode);
            Assert.False(first.IsHandleCreated);
            Assert.False(second.IsHandleCreated);
            Assert.False(child.IsHandleCreated);
        });

    [Fact]
    public void ModeEventsUseEffectiveValuesAndNoControlDoesNotRaiseAnEvent()
        => OnSta(() =>
        {
            using var parent = new Control { ImeMode = ImeMode.Hiragana };
            using var control = new ImeControl();
            parent.Controls.Add(control);
            int changes = 0;
            EventHandler handler = (_, e) => { Assert.Same(EventArgs.Empty, e); changes++; };
            control.ImeModeChanged += handler;
            control.ImeMode = ImeMode.Hiragana;
            control.ImeMode = ImeMode.Inherit;
            Assert.Equal(0, changes);
            control.ImeMode = ImeMode.Alpha;
            Assert.Equal(1, changes);
            control.ImeMode = ImeMode.NoControl;
            Assert.Equal(1, changes);
            control.ImeModeChanged -= handler;
            control.ImeMode = ImeMode.Close;
            Assert.Equal(1, changes);
        });

    [Fact]
    public void OnHalfRemainsAvailableToProtectedBaseButPublicModeNormalizesIt()
        => OnSta(() =>
        {
            using var control = new ImeControl();
            control.ImeMode = ImeMode.OnHalf;
            Assert.Equal(12, (int)ImeMode.OnHalf);
            Assert.Equal(ImeMode.OnHalf, control.RawMode);
            Assert.Equal(ImeMode.On, control.ImeMode);
            Assert.Equal(1, control.ModeGets);
            Assert.False(control.IsHandleCreated);
        });

    [Theory]
    [InlineData(-2)]
    [InlineData(13)]
    public void InvalidModesPreserveThePreviousModeAndDoNotNotify(int value)
        => OnSta(() =>
        {
            using var control = new Control { ImeMode = ImeMode.Close };
            int changes = 0;
            control.ImeModeChanged += (_, _) => changes++;
            var exception = Assert.Throws<InvalidEnumArgumentException>(() => control.ImeMode = (ImeMode)value);
            Assert.Equal("value", exception.ParamName);
            Assert.Equal(ImeMode.Close, control.ImeMode);
            Assert.Equal(0, changes);
            Assert.False(control.IsHandleCreated);
        });

    [Fact]
    public void DisabledDefaultSuppressesAutomaticEventsAndDesignerResetUsesTheOverride()
        => OnSta(() =>
        {
            using var control = new ImeDisabledControl();
            int changes = 0;
            control.ImeModeChanged += (_, _) => changes++;
            Assert.False(control.CanEnable);
            Assert.Equal(ImeMode.Disable, control.ImeMode);
            control.ImeMode = ImeMode.On;
            Assert.Equal(ImeMode.On, control.ImeMode);
            control.ResetImeMode();
            Assert.Equal(ImeMode.Disable, control.ImeMode);
            Assert.Equal(0, changes);
        });

    [Fact]
    public void DesignerSerializationTracksTheLocalModeAndEventExceptionsRemainVisible()
        => OnSta(() =>
        {
            using var control = new Control();
            PropertyDescriptor property = TypeDescriptor.GetProperties(control)[nameof(Control.ImeMode)]!;
            Assert.False(property.ShouldSerializeValue(control));
            var expected = new InvalidOperationException("IME event failure");
            EventHandler handler = (_, _) => throw expected;
            control.ImeModeChanged += handler;
            Assert.Same(expected, Assert.Throws<InvalidOperationException>(() => control.ImeMode = ImeMode.Close));
            Assert.Equal(ImeMode.Close, control.ImeMode);
            Assert.True(property.ShouldSerializeValue(control));
            control.ImeModeChanged -= handler;
            property.ResetValue(control);
            Assert.Equal(ImeMode.NoControl, control.ImeMode);
            Assert.False(property.ShouldSerializeValue(control));
        });

    [Fact]
    public void ConversionReadbackMatchesTheFixedChineseJapaneseAndKoreanTables()
    {
        Assert.Equal(ImeMode.On, DecodeMode(1, 0x09));
        Assert.Equal(ImeMode.OnHalf, DecodeMode(1, 0x03));
        Assert.Equal(ImeMode.Off, DecodeMode(1, 0x08));
        Assert.Equal(ImeMode.Hiragana, DecodeMode(2, 0x01));
        Assert.Equal(ImeMode.Katakana, DecodeMode(2, 0x0b));
        Assert.Equal(ImeMode.KatakanaHalf, DecodeMode(2, 0x03));
        Assert.Equal(ImeMode.AlphaFull, DecodeMode(2, 0x08));
        Assert.Equal(ImeMode.HangulFull, DecodeMode(3, 0x0b));
        Assert.Equal(ImeMode.Hangul, DecodeMode(3, 0x03));
        Assert.Equal(ImeMode.Alpha, DecodeMode(3, 0));
        Assert.Equal(ImeMode.Inherit, DecodeMode(0, 0x09));
    }

    [Fact]
    public void NativeContextsCanBeDisabledRestoredAndOwnedContextsAreDetachedOnRelease()
        => OnSta(() =>
        {
            using var control = new ImeControl();
            nint window = control.Handle;
            nint original = ImmAssociateContext(window, 0);
            object owner = Activator.CreateInstance(ContextType, nonPublic: true)!;
            try
            {
                SetNativeMode(owner, ImeMode.On, window);
                nint created = ReadContext(window);
                Assert.NotEqual(0, created);
                SetNativeMode(owner, ImeMode.Disable, window);
                Assert.Equal(0, ReadContext(window));
                SetNativeMode(owner, ImeMode.On, window);
                Assert.Equal(created, ReadContext(window));
                ReleaseNative(owner, window);
                Assert.Equal(0, ReadContext(window));
                Assert.Equal((nint)0, ContextType.GetField("_ownedContext", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(owner));
            }
            finally
            {
                ReleaseNative(owner, window);
                _ = ImmAssociateContext(window, original);
            }
        });

    [Fact]
    public void NativeBorrowedContextIsRestoredWithoutBeingClaimedOrDestroyed()
        => OnSta(() =>
        {
            using var control = new ImeControl();
            nint window = control.Handle;
            nint original = ImmAssociateContext(window, 0);
            nint borrowed = ImmCreateContext();
            Assert.NotEqual(0, borrowed);
            object owner = Activator.CreateInstance(ContextType, nonPublic: true)!;
            try
            {
                _ = ImmAssociateContext(window, borrowed);
                SetNativeMode(owner, ImeMode.Disable, window);
                Assert.Equal(0, ReadContext(window));
                SetNativeMode(owner, ImeMode.On, window);
                Assert.Equal(borrowed, ReadContext(window));
                ReleaseNative(owner, window);
                Assert.Equal(borrowed, ReadContext(window));
            }
            finally
            {
                ReleaseNative(owner, window);
                _ = ImmAssociateContext(window, original);
                Assert.True(ImmDestroyContext(borrowed));
            }
        });

    [Fact]
    public void OwnedContextIsReleasedWhenTheRealHandleIsDestroyedEvenIfTheEventThrows()
        => OnSta(() =>
        {
            using var control = new ImeControl();
            nint window = control.Handle;
            _ = ImmAssociateContext(window, 0);
            object owner = Activator.CreateInstance(ContextType, nonPublic: true)!;
            SetNativeMode(owner, ImeMode.On, window);
            Assert.NotEqual(0, ReadContext(window));
            typeof(Control).GetField("_imeContext", BindingFlags.Instance | BindingFlags.NonPublic)!.SetValue(control, owner);
            var expected = new InvalidOperationException("handle event");
            var observed = new List<Exception>();
            void Report(object sender, ThreadExceptionEventArgs e) => observed.Add(e.Exception);
            Application.ThreadException += Report;
            control.HandleDestroyed += (_, _) => throw expected;
            try
            {
                control.DestroyWindow();
                Assert.False(control.IsHandleCreated);
                Assert.Contains(expected, observed);
                Assert.Equal((nint)0, ContextType.GetField("_ownedContext", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(owner));
                Assert.Null(typeof(Control).GetField("_imeContext", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(control));
            }
            finally { Application.ThreadException -= Report; }
        });

    [Fact]
    public void HandleNotificationAndImeCleanupFailuresAreBothReported()
        => OnSta(() =>
        {
            using var control = new ImeControl();
            _ = control.Handle;
            object owner = Activator.CreateInstance(ContextType, nonPublic: true)!;
            FieldInfo ownedContext = ContextType.GetField("_ownedContext", BindingFlags.Instance | BindingFlags.NonPublic)!;
            // An invalid HIMC makes IMM return an actual cleanup failure without
            // allocating or destroying any other context.
            ownedContext.SetValue(owner, (nint)(-1));
            typeof(Control).GetField("_imeContext", BindingFlags.Instance | BindingFlags.NonPublic)!.SetValue(control, owner);
            var expected = new InvalidOperationException("handle notification failure");
            var observed = new List<Exception>();
            void Report(object sender, ThreadExceptionEventArgs e) => observed.Add(e.Exception);
            Application.ThreadException += Report;
            control.HandleDestroyed += (_, _) => throw expected;
            try
            {
                control.DestroyWindow();
                Assert.False(control.IsHandleCreated);
                AggregateException failure = Assert.IsType<AggregateException>(Assert.Single(observed));
                Assert.Same(expected, failure.InnerExceptions[0]);
                Assert.Equal("The input method context could not be destroyed.", failure.InnerExceptions[1].Message);
                Assert.Equal((nint)(-1), ownedContext.GetValue(owner));
            }
            finally
            {
                Application.ThreadException -= Report;
                ownedContext.SetValue(owner, (nint)0);
                ReleaseNative(owner, 0);
            }
        });

    private static Type ContextType => typeof(Control).Assembly.GetType("Lumina.Forms.NativeImeContext", throwOnError: true)!;
    private static Type LanguageType => typeof(Control).Assembly.GetType("Lumina.Forms.ImeLanguage", throwOnError: true)!;

    private static ImeMode DecodeMode(int language, uint conversion)
        => (ImeMode)ContextType.GetMethod("ModeFromConversion", BindingFlags.Static | BindingFlags.NonPublic)!
            .Invoke(null, [Enum.ToObject(LanguageType, language), conversion])!;

    private static void SetNativeMode(object owner, ImeMode mode, nint window)
        => ContextType.GetMethod("SetImeStatus", BindingFlags.Instance | BindingFlags.NonPublic)!
            .Invoke(owner, [mode, window, Enum.ToObject(LanguageType, 2)]);

    private static void ReleaseNative(object owner, nint window)
        => ContextType.GetMethod("Release", BindingFlags.Instance | BindingFlags.NonPublic)!.Invoke(owner, [window]);

    private static nint ReadContext(nint window)
    {
        nint context = ImmGetContext(window);
        if (context != 0) { Assert.True(ImmReleaseContext(window, context)); }
        return context;
    }

    private class ImeControl : Control
    {
        internal ImeMode RawMode => base.ImeModeBase;
        internal int ModeGets { get; private set; }
        protected override ImeMode ImeModeBase
        {
            get { ModeGets++; return base.ImeModeBase; }
            set => base.ImeModeBase = value;
        }
        internal void DestroyWindow() => DestroyHandle();
    }

    private sealed class ImeDisabledControl : Control
    {
        protected override ImeMode DefaultImeMode => ImeMode.Disable;
        internal bool CanEnable => CanEnableIme;
    }

    [DllImport("imm32.dll")]
    private static extern nint ImmCreateContext();
    [DllImport("imm32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool ImmDestroyContext(nint context);
    [DllImport("imm32.dll")]
    private static extern nint ImmAssociateContext(nint window, nint context);
    [DllImport("imm32.dll")]
    private static extern nint ImmGetContext(nint window);
    [DllImport("imm32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool ImmReleaseContext(nint window, nint context);

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
        Assert.True(thread.Join(TimeSpan.FromSeconds(20)), "The IME context test STA did not complete.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }
}
