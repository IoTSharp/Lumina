using System.ComponentModel;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxModifiedTests
{
    private const int EmGetModify = 0x00B8;
    private const int EmSetModify = 0x00B9;
    private const int WmChar = 0x0102;

    [Fact]
    public void DefaultAndStandardMetadataStayLazy()
        => OnSta(() =>
        {
            using var textBox = new TextBox();
            TextBoxBase editing = textBox;
            Assert.False(editing.Modified);
            PropertyInfo property = typeof(TextBox).GetProperty(nameof(TextBoxBase.Modified))!;
            Assert.Equal(typeof(TextBoxBase), property.DeclaringType);
            Assert.False(property.GetMethod!.IsVirtual);
            Assert.Equal("Behavior", property.GetCustomAttribute<CategoryAttribute>()!.Category);
            Assert.False(property.GetCustomAttribute<BrowsableAttribute>()!.Browsable);
            Assert.Equal(DesignerSerializationVisibility.Hidden,
                property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>()!.Visibility);
            Assert.Null(property.GetCustomAttribute<DefaultValueAttribute>());
            Assert.Equal(typeof(EventHandler), typeof(TextBox).GetEvent(nameof(TextBoxBase.ModifiedChanged))!.EventHandlerType);
            MethodInfo hook = typeof(TextBoxBase).GetMethod("OnModifiedChanged", BindingFlags.Instance | BindingFlags.NonPublic)!;
            Assert.True(hook.IsFamily && hook.IsVirtual);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void PendingSetterOnlyNotifiesActualChangesAndSupportsUnsubscribe()
        => OnSta(() =>
        {
            using var textBox = new TextBox { Text = "alpha" };
            var values = new List<bool>();
            EventHandler handler = (sender, args) =>
            {
                Assert.Same(textBox, sender);
                Assert.Same(EventArgs.Empty, args);
                values.Add(textBox.Modified);
            };
            textBox.ModifiedChanged += handler;
            textBox.Modified = false;
            textBox.Modified = true;
            textBox.Modified = true;
            textBox.Modified = false;
            textBox.ModifiedChanged -= handler;
            textBox.Modified = true;
            Assert.Equal(new[] { true, false }, values);
            Assert.Equal("alpha", textBox.Text);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void HookReceivesCommittedStateAndDirectArguments()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox();
            var trace = new List<string>();
            textBox.BeforeEvent = args => trace.Add($"hook:{textBox.Modified}:{ReferenceEquals(args, EventArgs.Empty)}");
            EventArgs? seen = null;
            textBox.ModifiedChanged += (_, args) =>
            {
                seen = args;
                trace.Add($"event:{textBox.Modified}:{ReferenceEquals(args, EventArgs.Empty)}");
            };
            textBox.Modified = true;
            var supplied = new EventArgs();
            textBox.RaiseModifiedChanged(supplied);
            Assert.Equal(new[] { "hook:True:True", "event:True:True", "hook:True:False", "event:True:False" }, trace);
            Assert.Same(supplied, seen);
        });

    [Fact]
    public void OverrideMaySuppressBaseNotification()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox { CallBase = false };
            int events = 0;
            textBox.ModifiedChanged += (_, _) => events++;
            textBox.Modified = true;
            textBox.Modified = true;
            Assert.True(textBox.Modified);
            Assert.Equal(1, textBox.HookCalls);
            Assert.Equal(0, events);
        });

    [Fact]
    public void ComponentEventSuppressionKeepsHookAndState()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox { SuppressEvents = true };
            int events = 0;
            textBox.ModifiedChanged += (_, _) => events++;
            textBox.Modified = true;
            Assert.Equal(1, textBox.HookCalls);
            Assert.Equal(0, events);
            textBox.SuppressEvents = false;
            textBox.Modified = false;
            Assert.Equal(2, textBox.HookCalls);
            Assert.Equal(1, events);
        });

    [Fact]
    public void PendingSetterPreservesStateWhenHandlerThrows()
        => OnSta(() =>
        {
            using var textBox = new TextBox();
            var expected = new InvalidOperationException("modified handler");
            textBox.ModifiedChanged += (_, _) => throw expected;
            Assert.Same(expected, Assert.Throws<InvalidOperationException>(() => textBox.Modified = true));
            Assert.True(textBox.Modified);
            textBox.Modified = true;
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void PendingReentrancyObservesEachCommittedValueOnce()
        => OnSta(() =>
        {
            using var textBox = new TextBox();
            var values = new List<bool>();
            textBox.ModifiedChanged += (_, _) =>
            {
                values.Add(textBox.Modified);
                if (textBox.Modified) { textBox.Modified = false; }
            };
            textBox.Modified = true;
            Assert.Equal(new[] { true, false }, values);
            Assert.False(textBox.Modified);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void NativeSetterUpdatesActualFlagWithoutRecreating(bool value)
        => OnSta(() =>
        {
            using var textBox = new TextBox { Text = "alpha" };
            nint handle = textBox.Handle;
            textBox.Modified = !value;
            int events = 0;
            textBox.ModifiedChanged += (_, _) =>
            {
                events++;
                Assert.Equal(value, SendMessageW(handle, EmGetModify, 0, 0) != 0);
            };
            textBox.Modified = value;
            textBox.Modified = value;
            Assert.Equal(1, events);
            Assert.Equal(handle, textBox.Handle);
            Assert.Equal(value, textBox.Modified);
            Assert.Equal("alpha", textBox.Text);
        });

    [Fact]
    public void GetterSynchronizesExternalNativeChangesOnce()
        => OnSta(() =>
        {
            using var textBox = new TextBox();
            nint handle = textBox.Handle;
            var values = new List<bool>();
            textBox.ModifiedChanged += (_, _) => values.Add(textBox.Modified);
            _ = SendMessageW(handle, EmSetModify, 1, 0);
            Assert.Empty(values);
            Assert.True(textBox.Modified);
            Assert.True(textBox.Modified);
            _ = SendMessageW(handle, EmSetModify, 0, 0);
            Assert.False(textBox.Modified);
            Assert.Equal(new[] { true, false }, values);
        });

    [Fact]
    public void SetterObservesNativeDifferenceBeforeComparingRequestedValue()
        => OnSta(() =>
        {
            using var textBox = new TextBox();
            nint handle = textBox.Handle;
            var values = new List<bool>();
            textBox.ModifiedChanged += (_, _) => values.Add(textBox.Modified);
            _ = SendMessageW(handle, EmSetModify, 1, 0);
            textBox.Modified = false;
            Assert.Equal(new[] { true, false }, values);
            Assert.Equal((nint)0, SendMessageW(handle, EmGetModify, 0, 0));
        });

    [Fact]
    public void GetterReturnsItsObservedNativeValueAcrossReentrantChange()
        => OnSta(() =>
        {
            using var textBox = new TextBox();
            nint handle = textBox.Handle;
            var values = new List<bool>();
            textBox.ModifiedChanged += (_, _) =>
            {
                values.Add(textBox.Modified);
                if (textBox.Modified) { textBox.Modified = false; }
            };
            _ = SendMessageW(handle, EmSetModify, 1, 0);
            Assert.True(textBox.Modified);
            Assert.False(textBox.Modified);
            Assert.Equal(new[] { true, false }, values);
        });

    [Fact]
    public void GetterCommitsCacheBeforePropagatingOriginalHandlerException()
        => OnSta(() =>
        {
            using var textBox = new TextBox();
            nint handle = textBox.Handle;
            var expected = new InvalidOperationException("native modified handler");
            int events = 0;
            textBox.ModifiedChanged += (_, _) => { events++; throw expected; };
            _ = SendMessageW(handle, EmSetModify, 1, 0);
            Assert.Same(expected, Assert.Throws<InvalidOperationException>(() => _ = textBox.Modified));
            Assert.True(textBox.Modified);
            Assert.Equal(1, events);
        });

    [Fact]
    public void ReflectedUpdateReadsModifiedAndChangeDoesNotInventUpdateNotification()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox { Text = "alpha" };
            nint handle = textBox.Handle;
            int modifiedEvents = 0;
            int textEvents = 0;
            textBox.ModifiedChanged += (_, _) => modifiedEvents++;
            textBox.TextChanged += (_, _) => textEvents++;
            _ = SendMessageW(handle, EmSetModify, 1, 0);
            Assert.False(textBox.NotifyCommand(0x0300));
            Assert.Equal(0, modifiedEvents);
            Assert.True(textBox.NotifyCommand(0x0400));
            Assert.True(textBox.NotifyCommand(0x0400));
            Assert.Equal(1, modifiedEvents);
            Assert.Equal(0, textEvents);
        });

    [Fact]
    public void RealEditUpdateNotifiesModifiedBeforeTextChanged()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new TextBox { Text = "A" };
            form.Controls.Add(textBox);
            nint parentHandle = form.Handle;
            nint handle = textBox.Handle;
            Assert.Equal(parentHandle, GetParent(handle));
            Assert.Same(form, Control.FromHandle(GetParent(handle)));
            var trace = new List<string>();
            textBox.ModifiedChanged += (_, _) => trace.Add("modified");
            textBox.TextChanged += (_, _) => trace.Add("text");
            _ = SendMessageW(handle, WmChar, 'B', 1);
            Assert.Equal(new[] { "modified", "text" }, trace);
            Assert.True(textBox.Modified);
            Assert.Equal(2, textBox.TextLength);
        });

    [Fact]
    public void ProgramTextResetDefersModifiedNotificationUntilItIsObserved()
        => OnSta(() =>
        {
            using var textBox = new TextBox { Text = "old" };
            _ = textBox.Handle;
            textBox.Modified = true;
            var trace = new List<string>();
            textBox.ModifiedChanged += (_, _) => trace.Add($"modified:{textBox.Modified}");
            textBox.TextChanged += (_, _) => trace.Add("text");

            textBox.Text = "new";

            Assert.Equal(new[] { "text" }, trace);
            Assert.Equal((nint)0, SendMessageW(textBox.Handle, EmGetModify, 0, 0));
            Assert.False(textBox.Modified);
            Assert.Equal(new[] { "text", "modified:False" }, trace);
        });

    [Fact]
    public void NativeEditFromTextChangedHandlerIsOutsideCodeUpdateSuppression()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new TextBox { Text = "A" };
            form.Controls.Add(textBox);
            nint parentHandle = form.Handle;
            nint handle = textBox.Handle;
            Assert.Equal(parentHandle, GetParent(handle));
            Assert.Same(form, Control.FromHandle(GetParent(handle)));
            var trace = new List<string>();
            bool edited = false;
            textBox.ModifiedChanged += (_, _) => trace.Add($"modified:{textBox.Modified}");
            textBox.TextChanged += (_, _) =>
            {
                trace.Add("text");
                if (!edited)
                {
                    edited = true;
                    _ = SendMessageW(handle, WmChar, 'B', 1);
                }
            };

            textBox.Text = "C";

            Assert.Equal(new[] { "text", "modified:True", "text" }, trace);
            Assert.False(textBox.Modified);
            Assert.Equal(new[] { "text", "modified:True", "text", "modified:False" }, trace);
        });

    [Fact]
    public void SameTextKeepsModifiedButNullEmptyAssignmentPerformsNativeReset()
        => OnSta(() =>
        {
            using var textBox = new TextBox();
            nint handle = textBox.Handle;
            textBox.Modified = true;
            int textEvents = 0;
            textBox.TextChanged += (_, _) => textEvents++;
            textBox.Text = string.Empty;
            Assert.True(textBox.Modified);
            textBox.Text = null;
            Assert.Equal((nint)0, SendMessageW(handle, EmGetModify, 0, 0));
            Assert.False(textBox.Modified);
            Assert.Equal(0, textEvents);
        });

    [Fact]
    public void PendingModifiedStateSurvivesCreationAndExistingRecreationStyles()
        => OnSta(() =>
        {
            using var textBox = new TextBox { Text = "alpha beta", Modified = true, ReadOnly = true, PasswordChar = '#' };
            textBox.Select(2, 4);
            int events = 0;
            int textEvents = 0;
            textBox.ModifiedChanged += (_, _) => events++;
            textBox.TextChanged += (_, _) => textEvents++;
            _ = textBox.Handle;
            Assert.True(textBox.Modified);
            textBox.HideSelection = false;
            textBox.TextAlign = HorizontalAlignment.Center;
            Assert.True(textBox.Modified);
            Assert.Equal("alpha beta", textBox.Text);
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(4, textBox.SelectionLength);
            Assert.True(textBox.ReadOnly);
            Assert.Equal('#', textBox.PasswordChar);
            Assert.Equal(0, events);
            Assert.Equal(0, textEvents);
        });

    [Fact]
    public void DestroySynchronizesUnobservedNativeFlagBeforeHandleDestroyed()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox();
            nint handle = textBox.Handle;
            var trace = new List<string>();
            textBox.ModifiedChanged += (_, _) => trace.Add($"modified:{textBox.IsHandleCreated}:{textBox.Modified}");
            textBox.HandleDestroyed += (_, _) => trace.Add("destroyed");
            _ = SendMessageW(handle, EmSetModify, 1, 0);
            textBox.RecreateNativeHandle();
            Assert.Equal(new[] { "modified:True:True", "destroyed" }, trace);
            Assert.True(textBox.Modified);
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox Modified lifecycle timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class HookTextBox : TextBox
    {
        internal bool CallBase { get; set; } = true;
        internal bool SuppressEvents { get; set; }
        internal int HookCalls { get; private set; }
        internal Action<EventArgs>? BeforeEvent { get; set; }
        protected override bool CanRaiseEvents => !SuppressEvents;
        internal void RaiseModifiedChanged(EventArgs e) => OnModifiedChanged(e);
        internal bool NotifyCommand(int notificationCode) => OnCommand(notificationCode);
        internal void RecreateNativeHandle() => RecreateHandle();

        protected override void OnModifiedChanged(EventArgs e)
        {
            HookCalls++;
            BeforeEvent?.Invoke(e);
            if (CallBase) { base.OnModifiedChanged(e); }
        }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetParent(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, nint lParam);
}
