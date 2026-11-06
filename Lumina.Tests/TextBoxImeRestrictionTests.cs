using System.ComponentModel;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxImeRestrictionTests
{
    private const int EmGetPasswordChar = 0x00D2;
    private const int EmSetPasswordChar = 0x00CC;
    private const int EmGetModify = 0x00B8;
    private const int EmSetModify = 0x00B9;
    private const int GwlStyle = -16;
    private const int EsPassword = 0x0020;
    private const int EsReadOnly = 0x0800;
    private const int EsMultiline = 0x0004;

    [Fact]
    public void PasswordGetterCreatesEditAndReadsItsActualCharacter()
        => OnSta(() =>
        {
            using var textBox = new ImeTextBox();
            Assert.False(textBox.ReadOnly);
            Assert.False(textBox.UseSystemPasswordChar);
            Assert.False(textBox.IsHandleCreated);

            Assert.Equal('\0', textBox.PasswordChar);
            Assert.True(textBox.IsHandleCreated);
            Assert.True(textBox.CanEnable);
            _ = SendMessageW(textBox.Handle, EmSetPasswordChar, '#', 0);
            Assert.Equal('#', textBox.PasswordChar);
            Assert.False(textBox.CanEnable);
        });

    [Fact]
    public void ReadOnlyShortCircuitsPasswordHandleCreationAndPreservesExplicitMode()
        => OnSta(() =>
        {
            using var textBox = new ImeTextBox { ImeMode = ImeMode.Hiragana };
            int modeChanges = 0;
            textBox.ImeModeChanged += (_, _) => modeChanges++;

            textBox.ReadOnly = true;
            Assert.False(textBox.CanEnable);
            Assert.False(textBox.IsHandleCreated);
            Assert.Equal(ImeMode.Disable, textBox.ImeMode);
            Assert.Equal(1, modeChanges);
            textBox.ReadOnly = false;
            Assert.True(textBox.CanEnable);
            Assert.True(textBox.IsHandleCreated);
            Assert.Equal(ImeMode.Hiragana, textBox.ImeMode);
            Assert.Equal(2, modeChanges);
        });

    [Fact]
    public void ReadOnlyChangedSeesNativeStateBeforeImeNotificationAndSameValueDoesNothing()
        => OnSta(() =>
        {
            using var textBox = new ImeTextBox { ImeMode = ImeMode.Hiragana };
            nint window = textBox.Handle;
            var events = new List<string>();
            textBox.ReadOnlyChanged += (_, e) =>
            {
                Assert.Same(EventArgs.Empty, e);
                Assert.Equal(textBox.ReadOnly, (GetWindowLongW(window, GwlStyle) & EsReadOnly) != 0);
                events.Add("readonly:" + textBox.ReadOnly);
            };
            textBox.ImeModeChanged += (_, _) => events.Add("ime:" + textBox.CanEnable);

            textBox.ReadOnly = true;
            textBox.ReadOnly = true;
            textBox.ReadOnly = false;
            Assert.Equal(new[] { "readonly:True", "ime:False", "readonly:False", "ime:True" }, events);
            Assert.Equal(window, textBox.Handle);
            Assert.Equal(2, textBox.ReadOnlyHooks);
        });

    [Fact]
    public void ReadOnlyChangedUsesEventSlotUnsubscriptionAndPreservesCallbackFailure()
        => OnSta(() =>
        {
            using var textBox = new ImeTextBox();
            var expected = new InvalidOperationException("readonly callback");
            EventHandler handler = (_, _) => throw expected;
            textBox.ReadOnlyChanged += handler;
            Assert.Same(expected, Assert.Throws<InvalidOperationException>(() => textBox.ReadOnly = true));
            Assert.True(textBox.ReadOnly);
            Assert.False(textBox.IsHandleCreated);
            textBox.ReadOnlyChanged -= handler;
            textBox.ReadOnly = false;
            Assert.Equal(2, textBox.ReadOnlyHooks);
        });

    [Fact]
    public void CustomPasswordSetBeforeCreationIsAppliedAfterHandleCreatedNotification()
        => OnSta(() =>
        {
            using var textBox = new ImeTextBox { PasswordChar = '*' };
            Assert.False(textBox.IsHandleCreated);
            char observedAtEvent = '!';
            textBox.HandleCreated += (_, _) => observedAtEvent = (char)SendMessageW(textBox.Handle, EmGetPasswordChar, 0, 0);

            Assert.Equal('*', textBox.PasswordChar);
            Assert.Equal('\0', observedAtEvent);
            Assert.False(textBox.CanEnable);
        });

    [Fact]
    public void RuntimePasswordRestrictionChangesNotifyOnlyEffectiveTransitions()
        => OnSta(() =>
        {
            using var textBox = new ImeTextBox { ImeMode = ImeMode.Hiragana };
            nint window = textBox.Handle;
            var values = new List<bool>();
            textBox.ImeModeChanged += (_, _) => values.Add(textBox.CanEnable);

            textBox.PasswordChar = '*';
            textBox.PasswordChar = '*';
            textBox.PasswordChar = '#';
            textBox.PasswordChar = '\0';

            Assert.Equal(new[] { false, true }, values);
            Assert.Equal(window, textBox.Handle);
            Assert.Equal('\0', textBox.PasswordChar);
            Assert.Equal(ImeMode.Hiragana, textBox.ImeMode);
        });

    [Fact]
    public void ReadOnlyAndPasswordRestrictionsComposeWithoutPrematureEnableNotification()
        => OnSta(() =>
        {
            using var textBox = new ImeTextBox { ImeMode = ImeMode.Hiragana };
            _ = textBox.Handle;
            var values = new List<bool>();
            textBox.ImeModeChanged += (_, _) => values.Add(textBox.CanEnable);

            textBox.PasswordChar = '*';
            textBox.ReadOnly = true;
            textBox.PasswordChar = '\0';
            Assert.False(textBox.CanEnable);
            textBox.ReadOnly = false;
            Assert.Equal(new[] { false, true }, values);
        });

    [Fact]
    public void SettingImeModeWhileRestrictedPreservesTheRequestedModeForLaterEditing()
        => OnSta(() =>
        {
            using var textBox = new ImeTextBox { ReadOnly = true };
            textBox.ImeMode = ImeMode.Hiragana;
            Assert.Equal(ImeMode.Disable, textBox.ImeMode);
            textBox.ImeMode = ImeMode.Hangul;
            Assert.Equal(ImeMode.Disable, textBox.ImeMode);
            Assert.False(textBox.IsHandleCreated);

            textBox.ReadOnly = false;
            Assert.Equal(ImeMode.Hangul, textBox.ImeMode);
            Assert.True(textBox.CanEnable);
        });

    [Fact]
    public void SystemPasswordModeRecreatesEditAndCachesCustomCharacterUntilDisabled()
        => OnSta(() =>
        {
            using var textBox = new ImeTextBox { Text = "secret", PasswordChar = '*' };
            nint first = textBox.Handle;
            textBox.Select(1, 3);
            _ = SendMessageW(first, EmSetModify, 1, 0);
            textBox.UseSystemPasswordChar = true;
            nint second = textBox.Handle;
            Assert.NotEqual(first, second);
            Assert.False(IsWindow(first));
            Assert.NotEqual(0, GetWindowLongW(second, GwlStyle) & EsPassword);
            char systemCharacter = textBox.PasswordChar;
            Assert.NotEqual('\0', systemCharacter);
            Assert.False(textBox.CanEnable);

            textBox.PasswordChar = '#';
            Assert.Equal(systemCharacter, textBox.PasswordChar);
            textBox.UseSystemPasswordChar = true;
            Assert.Equal(second, textBox.Handle);
            textBox.UseSystemPasswordChar = false;
            Assert.False(IsWindow(second));
            Assert.Equal('#', textBox.PasswordChar);
            Assert.False(textBox.CanEnable);
            Assert.Equal("secret", textBox.Text);
            Assert.Equal(1, textBox.SelectionStart);
            Assert.Equal(3, textBox.SelectionLength);
            Assert.NotEqual(0, SendMessageW(textBox.Handle, EmGetModify, 0, 0));
        });

    [Fact]
    public void SystemPasswordBeforeCreationDoesNotCreateHandleAndClearingItRestoresImeEligibility()
        => OnSta(() =>
        {
            using var textBox = new ImeTextBox { ImeMode = ImeMode.Hiragana, UseSystemPasswordChar = true };
            Assert.False(textBox.IsHandleCreated);
            Assert.NotEqual('\0', textBox.PasswordChar);
            Assert.False(textBox.CanEnable);
            int modeChanges = 0;
            textBox.ImeModeChanged += (_, _) => modeChanges++;

            textBox.UseSystemPasswordChar = false;
            Assert.Equal('\0', textBox.PasswordChar);
            Assert.True(textBox.CanEnable);
            Assert.Equal(1, modeChanges);
        });

    [Fact]
    public void PasswordRestrictionTracksTheActualEditModeAcrossMultilineRecreation()
        => OnSta(() =>
        {
            nint nativeEdit = CreateWindowExW(0, "EDIT", "", EsMultiline, 0, 0, 100, 100, 0, 0, 0, 0);
            Assert.NotEqual(0, nativeEdit);
            try
            {
                _ = SendMessageW(nativeEdit, EmSetPasswordChar, '*', 0);
                char nativePassword = (char)SendMessageW(nativeEdit, EmGetPasswordChar, 0, 0);
                using var textBox = new ImeTextBox { PasswordChar = '*' };
                _ = textBox.Handle;
                textBox.Multiline = true;
                Assert.NotEqual(0, GetWindowLongW(textBox.Handle, GwlStyle) & EsMultiline);
                // Display support for multiline passwords does not establish
                // what EM_GETPASSWORDCHAR returns. Compare to a separate
                // native EDIT receiving the same messages as fixed WinForms.
                Assert.Equal(nativePassword, textBox.PasswordChar);
                Assert.Equal(nativePassword == '\0', textBox.CanEnable);
                textBox.Multiline = false;
                Assert.Equal('*', textBox.PasswordChar);
                Assert.False(textBox.CanEnable);
            }
            finally { Assert.True(DestroyWindow(nativeEdit)); }

            using var readOnly = new TextBox(multiline: false, readOnly: true);
            Assert.True(readOnly.ReadOnly);
            _ = readOnly.Handle;
            Assert.NotEqual(0, GetWindowLongW(readOnly.Handle, GwlStyle) & EsReadOnly);
        });

    [Fact]
    public void ReadOnlyAndSystemPasswordPropertiesExposeStandardDefaults()
    {
        foreach (string propertyName in new[] { nameof(TextBox.ReadOnly), nameof(TextBox.UseSystemPasswordChar) })
        {
            PropertyDescriptor property = TypeDescriptor.GetProperties(typeof(TextBox))[propertyName]!;
            Assert.Equal(false, ((DefaultValueAttribute)property.Attributes[typeof(DefaultValueAttribute)]!).Value);
            Assert.Equal(RefreshProperties.Repaint, ((RefreshPropertiesAttribute)property.Attributes[typeof(RefreshPropertiesAttribute)]!).RefreshProperties);
        }
    }

    [Fact]
    public void DesignModeReadsTheConfiguredImeModeWithoutCreatingARestrictedHandle()
    {
        using var textBox = new ImeTextBox();
        textBox.Site = new DesignSite(textBox);
        textBox.ReadOnly = true;
        textBox.ImeMode = ImeMode.Hiragana;
        Assert.Equal(ImeMode.Hiragana, textBox.ImeMode);
        Assert.False(textBox.IsHandleCreated);
    }

    private static void OnSta(Action action)
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox IME restriction check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class ImeTextBox : TextBox
    {
        internal bool CanEnable => CanEnableIme;
        internal int ReadOnlyHooks { get; private set; }
        protected override void OnReadOnlyChanged(EventArgs e)
        {
            ReadOnlyHooks++;
            base.OnReadOnlyChanged(e);
        }
    }

    private sealed class DesignSite(IComponent component) : ISite
    {
        public IComponent Component => component;
        public IContainer? Container => null;
        public bool DesignMode => true;
        public string? Name { get; set; }
        public object? GetService(Type serviceType) => null;
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern int GetWindowLongW(nint hWnd, int index);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint CreateWindowExW(uint exStyle, string className, string windowName, uint style,
        int x, int y, int width, int height, nint parent, nint menu, nint instance, nint parameter);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint hWnd);
}
