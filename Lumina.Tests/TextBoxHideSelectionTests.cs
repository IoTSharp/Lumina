using System.ComponentModel;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxHideSelectionTests
{
    private const int GwlStyle = -16;
    private const int EsNoHideSel = 0x0100;
    private const int EmGetModify = 0x00B8;
    private const int EmSetModify = 0x00B9;
    private const int EmGetPasswordChar = 0x00D2;

    [Fact]
    public void StandardDefaultAndMetadataAreDeclaredByTextBoxBase()
        => OnSta(() =>
        {
            using var textBox = new TextBox();
            TextBoxBase editing = textBox;
            Assert.True(editing.HideSelection);
            PropertyInfo property = typeof(TextBox).GetProperty(nameof(TextBoxBase.HideSelection))!;
            Assert.Equal(typeof(TextBoxBase), property.DeclaringType);
            Assert.Equal(true, property.GetCustomAttribute<DefaultValueAttribute>()!.Value);
            Assert.Equal("Behavior", property.GetCustomAttribute<CategoryAttribute>()!.Category);
            Assert.False(property.GetMethod!.IsVirtual);
            EventInfo notification = typeof(TextBox).GetEvent(nameof(TextBoxBase.HideSelectionChanged))!;
            Assert.Equal(typeof(TextBoxBase), notification.DeclaringType);
            Assert.Equal(typeof(EventHandler), notification.EventHandlerType);
            MethodInfo hook = typeof(TextBoxBase).GetMethod("OnHideSelectionChanged",
                BindingFlags.Instance | BindingFlags.NonPublic)!;
            Assert.True(hook.IsFamily && hook.IsVirtual);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void PendingChangesPreserveSelectionAndStayLazy()
        => OnSta(() =>
        {
            using var textBox = new TextBox { Text = "alpha beta" };
            TextBoxBase editing = textBox;
            textBox.Select(2, 5);
            var values = new List<bool>();
            EventHandler handler = (sender, args) =>
            {
                Assert.Same(textBox, sender);
                Assert.Same(EventArgs.Empty, args);
                values.Add(editing.HideSelection);
            };
            editing.HideSelectionChanged += handler;

            editing.HideSelection = true;
            textBox.HideSelection = false;
            editing.HideSelection = false;
            editing.HideSelection = true;
            textBox.HideSelectionChanged -= handler;
            editing.HideSelection = false;

            Assert.Equal(new[] { false, true }, values);
            Assert.Equal("alpha beta", textBox.Text);
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(5, textBox.SelectionLength);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void VirtualHookSeesCommittedStateAndKeepsDirectArguments()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox();
            var trace = new List<string>();
            textBox.BeforeEvent = args => trace.Add($"hook:{textBox.HideSelection}:{ReferenceEquals(args, EventArgs.Empty)}");
            EventArgs? observed = null;
            textBox.HideSelectionChanged += (_, args) =>
            {
                observed = args;
                trace.Add($"event:{textBox.HideSelection}:{ReferenceEquals(args, EventArgs.Empty)}");
            };

            textBox.HideSelection = false;
            var supplied = new EventArgs();
            textBox.RaiseHideSelectionChanged(supplied);

            Assert.Equal(new[] { "hook:False:True", "event:False:True", "hook:False:False", "event:False:False" }, trace);
            Assert.Same(supplied, observed);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void OverrideCanSuppressBaseEventWithoutSuppressingStateChange()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox { CallBase = false };
            int events = 0;
            textBox.HideSelectionChanged += (_, _) => events++;

            textBox.HideSelection = false;
            textBox.HideSelection = false;

            Assert.False(textBox.HideSelection);
            Assert.Equal(1, textBox.HookCalls);
            Assert.Equal(0, events);
        });

    [Fact]
    public void ComponentEventSuppressionLeavesTheVirtualHookActive()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox { SuppressEvents = true };
            int events = 0;
            textBox.HideSelectionChanged += (_, _) => events++;

            textBox.HideSelection = false;
            Assert.Equal(1, textBox.HookCalls);
            Assert.Equal(0, events);
            textBox.SuppressEvents = false;
            textBox.HideSelection = true;

            Assert.Equal(2, textBox.HookCalls);
            Assert.Equal(1, events);
        });

    [Fact]
    public void ReentrantHandlerObservesBothCommittedValuesOnce()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox();
            var values = new List<bool>();
            textBox.HideSelectionChanged += (_, _) =>
            {
                values.Add(textBox.HideSelection);
                if (!textBox.HideSelection) { textBox.HideSelection = true; }
            };

            textBox.HideSelection = false;

            Assert.Equal(new[] { false, true }, values);
            Assert.True(textBox.HideSelection);
            Assert.Equal(2, textBox.HookCalls);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(false, false)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(true, true)]
    public void RealEditNoHideSelectionStyleMatchesAnIndependentNativeEdit(bool multiline, bool hideSelection)
        => OnSta(() =>
        {
            using var textBox = new HookTextBox
            {
                Multiline = multiline,
                HideSelection = hideSelection,
                Width = 160,
                Height = 80,
                Text = "alpha beta"
            };
            int expected = hideSelection ? 0 : EsNoHideSel;
            Assert.Equal(expected, textBox.RequestedStyle & EsNoHideSel);
            Assert.False(textBox.IsHandleCreated);
            uint nativeStyle = 0x800100C0u | (multiline ? 4u : 0u) | (uint)expected;
            nint nativeEdit = CreateWindowExW(0x00000200, "EDIT", "alpha beta", nativeStyle,
                0, 0, 160, 80, 0, 0, 0, 0);
            Assert.NotEqual(0, nativeEdit);
            try
            {
                Assert.True(IsWindow(textBox.Handle));
                Assert.Equal(GetWindowLongW(nativeEdit, GwlStyle) & EsNoHideSel,
                    GetWindowLongW(textBox.Handle, GwlStyle) & EsNoHideSel);
            }
            finally { Assert.True(DestroyWindow(nativeEdit)); }
        });

    [Theory]
    [InlineData(true, false, false)]
    [InlineData(false, true, false)]
    [InlineData(false, false, true)]
    public void RecreationPrecedesNotificationAndPreservesNativeEditingState(
        bool multiline, bool customPassword, bool systemPassword)
        => OnSta(() =>
        {
            using var textBox = new HookTextBox
            {
                Multiline = multiline,
                ReadOnly = true,
                Text = "alpha beta",
                PasswordChar = customPassword ? '#' : '\0',
                UseSystemPasswordChar = systemPassword
            };
            nint original = textBox.Handle;
            nint originalPassword = SendMessageW(original, EmGetPasswordChar, 0, 0);
            textBox.Select(2, 5);
            _ = SendMessageW(original, EmSetModify, 1, 0);
            var trace = new List<string>();
            int textChanges = 0;
            int selectionChanges = 0;
            textBox.HandleDestroyed += (_, _) => trace.Add($"destroyed:{textBox.HideSelection}");
            textBox.HandleCreated += (_, _) => trace.Add($"created:{textBox.HideSelection}");
            textBox.BeforeEvent = _ => trace.Add($"hook:{textBox.HideSelection}");
            textBox.HideSelectionChanged += (_, _) =>
            {
                Assert.Equal(2, textBox.SelectionStart);
                Assert.Equal(5, textBox.SelectionLength);
                Assert.NotEqual(0, SendMessageW(textBox.Handle, EmGetModify, 0, 0));
                trace.Add($"event:{textBox.HideSelection}");
            };
            textBox.TextChanged += (_, _) => textChanges++;
            textBox.SelectionChanged += (_, _) => selectionChanges++;

            textBox.HideSelection = false;

            Assert.Equal(new[] { "destroyed:False", "created:False", "hook:False", "event:False" }, trace);
            Assert.False(IsWindow(original));
            Assert.Equal(EsNoHideSel, GetWindowLongW(textBox.Handle, GwlStyle) & EsNoHideSel);
            Assert.Equal("alpha beta", textBox.Text);
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(5, textBox.SelectionLength);
            Assert.NotEqual(0, SendMessageW(textBox.Handle, EmGetModify, 0, 0));
            Assert.NotEqual(0, GetWindowLongW(textBox.Handle, GwlStyle) & 0x0800);
            Assert.Equal(originalPassword, SendMessageW(textBox.Handle, EmGetPasswordChar, 0, 0));
            Assert.Equal(0, textChanges);
            Assert.Equal(0, selectionChanges);

            nint replacement = textBox.Handle;
            textBox.HideSelection = false;
            Assert.Equal(replacement, textBox.Handle);
            Assert.Equal(4, trace.Count);
        });

    [Fact]
    public void HandlerExceptionPreservesOriginalFailureAndRecreatedState()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox { Text = "alpha" };
            nint original = textBox.Handle;
            var failure = new InvalidOperationException("HideSelection callback");
            EventHandler handler = (_, _) => throw failure;
            textBox.HideSelectionChanged += handler;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.HideSelection = false));

            Assert.False(textBox.HideSelection);
            Assert.False(IsWindow(original));
            Assert.True(IsWindow(textBox.Handle));
            Assert.Equal(EsNoHideSel, GetWindowLongW(textBox.Handle, GwlStyle) & EsNoHideSel);
            textBox.HideSelection = false;
            Assert.Equal(1, textBox.HookCalls);
            textBox.HideSelectionChanged -= handler;
            textBox.HideSelection = true;
            Assert.Equal(2, textBox.HookCalls);
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox HideSelection lifecycle timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class HookTextBox : TextBox
    {
        internal bool CallBase { get; set; } = true;
        internal bool SuppressEvents { get; set; }
        internal int HookCalls { get; private set; }
        internal Action<EventArgs>? BeforeEvent { get; set; }
        internal int RequestedStyle => CreateParams.Style;
        protected override bool CanRaiseEvents => !SuppressEvents;

        internal void RaiseHideSelectionChanged(EventArgs e) => OnHideSelectionChanged(e);

        protected override void OnHideSelectionChanged(EventArgs e)
        {
            HookCalls++;
            BeforeEvent?.Invoke(e);
            if (CallBase) { base.OnHideSelectionChanged(e); }
        }
    }

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint CreateWindowExW(uint exStyle, string className, string? windowName,
        uint style, int x, int y, int width, int height, nint parent, nint menu, nint instance, nint param);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern int GetWindowLongW(nint hWnd, int index);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint hWnd);
}
