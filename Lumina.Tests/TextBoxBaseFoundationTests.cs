using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxBaseFoundationTests
{
    private const int EmGetModify = 0x00B8;
    private const int EmSetModify = 0x00B9;
    private const int EmGetPasswordChar = 0x00D2;

    [Fact]
    public void StandardBaseIsAbstractWithInternalConstructionAndNoSelectionExtension()
    {
        Assert.True(typeof(TextBoxBase).IsPublic);
        Assert.True(typeof(TextBoxBase).IsAbstract);
        Assert.Equal(typeof(Control), typeof(TextBoxBase).BaseType);
        Assert.Equal(typeof(TextBoxBase), typeof(TextBox).BaseType);
        ConstructorInfo constructor = Assert.Single(typeof(TextBoxBase).GetConstructors(
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic));
        Assert.True(constructor.IsAssembly);
        Assert.Empty(constructor.GetParameters());
        Assert.Null(typeof(TextBoxBase).GetEvent(nameof(TextBox.SelectionChanged)));
        Assert.Equal(typeof(TextBox), typeof(TextBox).GetEvent(nameof(TextBox.SelectionChanged))!.DeclaringType);
        Assert.Equal(typeof(TextBoxBase), typeof(TextBox).GetProperty(nameof(TextBox.SelectionStart))!.DeclaringType);
        Assert.True(typeof(TextBoxBase).GetProperty(nameof(TextBoxBase.TextLength))!.GetMethod!.IsVirtual);
    }

    [Fact]
    public void BaseReferenceSharesPendingTextSelectionAndEditingSettings()
        => OnSta(() =>
        {
            using var textBox = new TextBox();
            TextBoxBase editing = textBox;
            Assert.False(editing.ReadOnly);
            Assert.False(editing.Multiline);
            Assert.False(editing.AcceptsTab);
            Assert.True(editing.WordWrap);
            editing.Text = "ab\U0001F600cdef";
            editing.Select(2, 3);
            editing.ReadOnly = true;
            editing.Multiline = true;
            editing.AcceptsTab = true;
            editing.WordWrap = false;

            Assert.Equal(textBox.Text, editing.Text);
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(3, textBox.SelectionLength);
            Assert.True(textBox.ReadOnly);
            Assert.True(textBox.Multiline);
            Assert.True(textBox.AcceptsTab);
            Assert.False(textBox.WordWrap);
            Assert.False(textBox.IsHandleCreated);

            textBox.Text = "ab";
            Assert.Equal(2, editing.SelectionStart);
            Assert.Equal(0, editing.SelectionLength);
            editing.Text = "ab\U0001F600cdef";
            Assert.Equal(2, editing.SelectionStart);
            Assert.Equal(3, editing.SelectionLength);
            Assert.False(editing.IsHandleCreated);
        });

    [Fact]
    public void NativeRecreationThroughBaseKeepsTextSelectionModifiedBitAndEventIdentity()
        => OnSta(() =>
        {
            using var textBox = new TextBox { Text = "alpha beta" };
            TextBoxBase editing = textBox;
            editing.Select(2, 5);
            nint original = editing.Handle;
            _ = SendMessageW(original, EmSetModify, 1, 0);
            var events = new List<string>();
            editing.HandleDestroyed += (sender, _) =>
            {
                Assert.Same(textBox, sender);
                events.Add("destroyed");
            };
            textBox.HandleCreated += (sender, _) =>
            {
                Assert.Same(editing, sender);
                events.Add("created");
            };
            editing.MultilineChanged += (sender, _) =>
            {
                Assert.Same(textBox, sender);
                events.Add("multiline");
            };
            int textChanges = 0;
            editing.TextChanged += (_, _) => textChanges++;

            editing.Multiline = true;

            Assert.False(IsWindow(original));
            Assert.NotEqual(original, textBox.Handle);
            Assert.Equal("alpha beta", editing.Text);
            Assert.Equal(2, editing.SelectionStart);
            Assert.Equal(5, editing.SelectionLength);
            Assert.NotEqual(0, SendMessageW(editing.Handle, EmGetModify, 0, 0));
            Assert.Equal(new[] { "destroyed", "created", "multiline" }, events);
            Assert.Equal(0, textChanges);
        });

    [Fact]
    public void BaseAndConcreteReadOnlySubscriptionsUseOneVirtualNotification()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox();
            TextBoxBase editing = textBox;
            int events = 0;
            EventHandler handler = (sender, args) =>
            {
                Assert.Same(textBox, sender);
                Assert.Same(EventArgs.Empty, args);
                Assert.Equal(editing.ReadOnly, textBox.ReadOnly);
                events++;
            };
            editing.ReadOnlyChanged += handler;
            editing.ReadOnly = true;
            textBox.ReadOnly = true;
            Assert.Equal(1, events);
            Assert.Equal(1, textBox.ReadOnlyHooks);
            Assert.Equal(SystemColors.Control, editing.BackColor);
            textBox.ReadOnlyChanged -= handler;

            editing.ReadOnly = false;

            Assert.Equal(1, events);
            Assert.Equal(2, textBox.ReadOnlyHooks);
            Assert.Equal(SystemColors.Window, editing.BackColor);
        });

    [Fact]
    public void ConcreteSelectionExtensionStillRunsForChangesMadeThroughBase()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox { Text = "abcdef" };
            TextBoxBase editing = textBox;
            _ = editing.Handle;
            int events = 0;
            textBox.SelectionChanged += (sender, args) =>
            {
                Assert.Same(editing, sender);
                Assert.Same(EventArgs.Empty, args);
                events++;
            };

            editing.Select(1, 2);
            editing.Select(1, 2);
            editing.SelectionStart = 2;

            Assert.Equal(2, events);
            Assert.Equal(2, textBox.SelectionHooks);
            Assert.Equal(2, editing.SelectionStart);
            Assert.Equal(2, editing.SelectionLength);
        });

    [Fact]
    public void PasswordStateIsAppliedAfterSharedHandleNotificationAndSelectionRestore()
        => OnSta(() =>
        {
            using var textBox = new TextBox { Text = "secret", PasswordChar = '#' };
            TextBoxBase editing = textBox;
            editing.Select(1, 3);
            char atHandleCreated = '!';
            editing.HandleCreated += (_, _) =>
                atHandleCreated = (char)SendMessageW(editing.Handle, EmGetPasswordChar, 0, 0);

            Assert.Equal('#', textBox.PasswordChar);
            Assert.Equal('\0', atHandleCreated);
            Assert.Equal(1, editing.SelectionStart);
            Assert.Equal(3, editing.SelectionLength);

            textBox.UseSystemPasswordChar = true;

            Assert.NotEqual('\0', textBox.PasswordChar);
            Assert.Equal(1, editing.SelectionStart);
            Assert.Equal(3, editing.SelectionLength);
        });

    [Fact]
    public void LegacyConvenienceConstructorDoesNotCallEditingSettersOrCreateHandle()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox(multiline: true, readOnly: true);
            TextBoxBase editing = textBox;
            Assert.True(editing.Multiline);
            Assert.True(editing.ReadOnly);
            Assert.Equal(new Size(240, 120), editing.Size);
            Assert.Equal(0, textBox.MultilineHooks);
            Assert.Equal(0, textBox.ReadOnlyHooks);
            Assert.False(editing.IsHandleCreated);
        });

    [Fact]
    public void SharedKeyboardRulesAndDerivedReturnSettingUseTheSameEditingState()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox();
            TextBoxBase editing = textBox;
            editing.Multiline = true;
            editing.AcceptsTab = true;
            Assert.True(textBox.InputKey(Keys.Tab));
            Assert.False(textBox.InputKey(Keys.Control | Keys.Tab));
            Assert.False(textBox.InputKey(Keys.Enter));
            textBox.AcceptsReturn = true;
            Assert.True(textBox.InputKey(Keys.Enter));
            Assert.False(textBox.InputKey(Keys.Alt | Keys.Enter));
            editing.ReadOnly = true;
            Assert.False(textBox.InputKey(Keys.Back));
            Assert.True(textBox.InputKey(Keys.Home));
            Assert.False(editing.IsHandleCreated);
        });

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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBoxBase foundation timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class HookTextBox : TextBox
    {
        internal HookTextBox()
        {
        }

        internal HookTextBox(bool multiline, bool readOnly) : base(multiline, readOnly)
        {
        }

        internal int ReadOnlyHooks { get; private set; }
        internal int MultilineHooks { get; private set; }
        internal int SelectionHooks { get; private set; }
        internal bool InputKey(Keys keyData) => IsInputKey(keyData);

        protected override void OnReadOnlyChanged(EventArgs e)
        {
            ReadOnlyHooks++;
            base.OnReadOnlyChanged(e);
        }

        protected override void OnMultilineChanged(EventArgs e)
        {
            MultilineHooks++;
            base.OnMultilineChanged(e);
        }

        protected override void OnSelectionChanged(EventArgs e)
        {
            SelectionHooks++;
            base.OnSelectionChanged(e);
        }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint hWnd);
}
