using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxShortcutsEnabledTests
{
    private const int WmContextMenu = 0x007B;

    [Fact]
    public void ShortcutsEnabledHasStandardVirtualPropertyAndMetadata()
    {
        PropertyInfo property = typeof(TextBox).GetProperty(nameof(TextBoxBase.ShortcutsEnabled))!;
        Assert.Equal(typeof(TextBoxBase), property.DeclaringType);
        Assert.Equal(typeof(bool), property.PropertyType);
        Assert.True(property.GetMethod!.IsPublic);
        Assert.True(property.GetMethod.IsVirtual);
        Assert.True(property.SetMethod!.IsPublic);
        Assert.True(property.SetMethod.IsVirtual);
        Assert.Equal("Behavior", property.GetCustomAttribute<CategoryAttribute>()!.Category);
        Assert.Equal(true, property.GetCustomAttribute<DefaultValueAttribute>()!.Value);
        Assert.Null(typeof(TextBoxBase).GetEvent("ShortcutsEnabledChanged"));
    }

    [Fact]
    public void SettingsDefaultToTrueAndDoNotCreatePendingHandleOrNotify()
        => OnSta(() =>
        {
            using var textBox = new CommandTextBox { Text = "pending" };
            int textChanges = 0;
            int created = 0;
            int destroyed = 0;
            textBox.TextChanged += (_, _) => textChanges++;
            textBox.HandleCreated += (_, _) => created++;
            textBox.HandleDestroyed += (_, _) => destroyed++;

            Assert.True(textBox.ShortcutsEnabled);
            textBox.ShortcutsEnabled = false;
            Assert.False(textBox.ShortcutsEnabled);
            textBox.ShortcutsEnabled = false;
            textBox.ShortcutsEnabled = true;
            textBox.ShortcutsEnabled = true;

            Assert.True(textBox.ShortcutsEnabled);
            Assert.Equal("pending", textBox.Text);
            Assert.False(textBox.IsHandleCreated);
            Assert.Equal(0, created);
            Assert.Equal(0, destroyed);
            Assert.Equal(0, textChanges);
        });

    [Fact]
    public void SettingsPreserveExistingNativeHandleTextAndSelection()
        => OnSta(() =>
        {
            using var textBox = new CommandTextBox { Text = "abcdef" };
            nint handle = textBox.Handle;
            textBox.Select(2, 3);
            int created = 0;
            int destroyed = 0;
            int changed = 0;
            textBox.HandleCreated += (_, _) => created++;
            textBox.HandleDestroyed += (_, _) => destroyed++;
            textBox.TextChanged += (_, _) => changed++;

            textBox.ShortcutsEnabled = false;
            textBox.ShortcutsEnabled = false;
            textBox.ShortcutsEnabled = true;

            Assert.Equal(handle, textBox.Handle);
            Assert.Equal("abcdef", textBox.Text);
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(3, textBox.SelectionLength);
            Assert.Equal(0, created);
            Assert.Equal(0, destroyed);
            Assert.Equal(0, changed);
        });

    [Fact]
    public void SetterDoesNotReadVirtualGetter()
        => OnSta(() =>
        {
            using var textBox = new CommandTextBox
            {
                GetterFailure = new InvalidOperationException("do not read while setting")
            };

            textBox.ShortcutsEnabled = true;
            textBox.ShortcutsEnabled = false;

            Assert.False(textBox.StoredShortcutsEnabled);
            Assert.Equal(0, textBox.GetterCalls);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(Keys.Control | Keys.Z)]
    [InlineData(Keys.Control | Keys.C)]
    [InlineData(Keys.Control | Keys.X)]
    [InlineData(Keys.Control | Keys.V)]
    [InlineData(Keys.Control | Keys.A)]
    [InlineData(Keys.Control | Keys.L)]
    [InlineData(Keys.Control | Keys.R)]
    [InlineData(Keys.Control | Keys.E)]
    [InlineData(Keys.Control | Keys.Y)]
    [InlineData(Keys.Control | Keys.Back)]
    [InlineData(Keys.Control | Keys.Delete)]
    [InlineData(Keys.Shift | Keys.Delete)]
    [InlineData(Keys.Shift | Keys.Insert)]
    [InlineData(Keys.Control | Keys.J)]
    [InlineData(Keys.Control | Keys.Shift | Keys.Z)]
    [InlineData(Keys.Control | Keys.Shift | Keys.C)]
    [InlineData(Keys.Control | Keys.Shift | Keys.X)]
    [InlineData(Keys.Control | Keys.Shift | Keys.V)]
    [InlineData(Keys.Control | Keys.Shift | Keys.A)]
    [InlineData(Keys.Control | Keys.Shift | Keys.L)]
    [InlineData(Keys.Control | Keys.Shift | Keys.R)]
    [InlineData(Keys.Control | Keys.Shift | Keys.E)]
    [InlineData(Keys.Control | Keys.Shift | Keys.Y)]
    [InlineData(Keys.Control | Keys.Shift | Keys.Back)]
    [InlineData(Keys.Control | Keys.Shift | Keys.Delete)]
    [InlineData(Keys.Control | Keys.Shift | Keys.J)]
    public void DisabledSettingConsumesExactShortcutAndShiftVariantsWithoutNativeDispatch(Keys keyData)
        => OnSta(() =>
        {
            using var parent = new RoutingControl();
            using var textBox = new CommandTextBox { Parent = parent, ShortcutsEnabled = false };
            Message message = Message.Create(0, 0x0100, (nint)(keyData & Keys.KeyCode), 0);

            Assert.True(textBox.ProcessCommand(ref message, keyData));

            Assert.Equal(1, parent.Calls);
            Assert.Equal(keyData, parent.LastKeyData);
            Assert.False(textBox.IsHandleCreated);
            Assert.False(parent.IsHandleCreated);
            Assert.Equal(0, textBox.ContextDefaultCalls);
        });

    [Theory]
    [InlineData(Keys.Control | Keys.I)]
    [InlineData(Keys.Control | Keys.Shift | Keys.I)]
    [InlineData(Keys.Control | Keys.B)]
    [InlineData(Keys.Control | Keys.Alt | Keys.C)]
    [InlineData(Keys.Control | Keys.Shift | Keys.Alt | Keys.V)]
    [InlineData(Keys.Control | Keys.Alt | Keys.Back)]
    [InlineData(Keys.Shift | Keys.Back)]
    [InlineData(Keys.Z)]
    public void DisabledSettingDoesNotConsumeUnlistedOrAdditionalAltCombinations(Keys keyData)
        => OnSta(() =>
        {
            using var textBox = new CommandTextBox { ShortcutsEnabled = false };
            Message message = Message.Create(0, 0x0100, (nint)(keyData & Keys.KeyCode), 0);

            Assert.False(textBox.ProcessCommand(ref message, keyData));
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void EnabledSettingPreservesParentResultForOrdinaryClipboardShortcut(bool parentResult)
        => OnSta(() =>
        {
            using var parent = new RoutingControl { ReturnValue = parentResult };
            using var textBox = new CommandTextBox { Parent = parent };
            Message message = Message.Create(0, 0x0100, (nint)Keys.C, 0);

            Assert.Equal(parentResult, textBox.ProcessCommand(ref message, Keys.Control | Keys.C));
            Assert.Equal(1, parent.Calls);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void ParentRunsBeforeDisabledCheckAndMessageChangesRemainVisible()
        => OnSta(() =>
        {
            var trace = new List<string>();
            using var parent = new RoutingControl { Trace = trace, MutateMessage = true };
            using var textBox = new CommandTextBox
            {
                Parent = parent, Trace = trace, ShortcutsEnabled = false
            };
            Message message = Message.Create(0, 0x0100, (nint)Keys.C, 0);

            Assert.True(textBox.ProcessCommand(ref message, Keys.Control | Keys.C));

            Assert.Equal(new[] { "parent", "getter" }, trace);
            Assert.Equal((nint)17, message.HWnd);
            Assert.Equal(0x0101, message.Msg);
            Assert.Equal((nint)19, message.WParam);
            Assert.Equal((nint)23, message.LParam);
            Assert.Equal((nint)29, message.Result);
        });

    [Fact]
    public void ParentHandledResultDoesNotSkipVirtualGetterOrReplaceItsFailure()
        => OnSta(() =>
        {
            var trace = new List<string>();
            var failure = new InvalidOperationException("shortcut getter");
            using var parent = new RoutingControl { Trace = trace, ReturnValue = true };
            using var textBox = new CommandTextBox { Parent = parent, Trace = trace, GetterFailure = failure };
            Message message = Message.Create(0, 0x0100, (nint)Keys.C, 0);

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(
                () => textBox.ProcessCommand(ref message, Keys.Control | Keys.C)));
            Assert.Equal(new[] { "parent", "getter" }, trace);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void ParentFailureStopsBeforeVirtualGetterAndPreservesOriginalException()
        => OnSta(() =>
        {
            var trace = new List<string>();
            var failure = new InvalidOperationException("parent shortcut");
            using var parent = new RoutingControl { Trace = trace, Failure = failure };
            using var textBox = new CommandTextBox { Parent = parent, Trace = trace };
            Message message = Message.Create(0, 0x0100, (nint)Keys.C, 0);

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(
                () => textBox.ProcessCommand(ref message, Keys.Control | Keys.C)));
            Assert.Equal(new[] { "parent" }, trace);
            Assert.Equal(0, textBox.GetterCalls);
        });

    [Theory]
    [InlineData(true, false, true)]
    [InlineData(false, true, false)]
    public void CommandFilteringUsesVirtualGetterRatherThanStoredFlag(
        bool storedSetting, bool virtualSetting, bool expectedHandled)
        => OnSta(() =>
        {
            using var textBox = new CommandTextBox
            {
                ShortcutsEnabled = storedSetting, ForcedSetting = virtualSetting
            };
            Message message = Message.Create(0, 0x0100, (nint)Keys.C, 0);

            Assert.Equal(expectedHandled, textBox.ProcessCommand(ref message, Keys.Control | Keys.C));
            Assert.Equal(storedSetting, textBox.StoredShortcutsEnabled);
            Assert.Equal(1, textBox.GetterCalls);
        });

    [Fact]
    public void SameValueSetterInitializesFilterForFalseOverrideEvenWhenStoredDefaultIsTrue()
        => OnSta(() =>
        {
            // The fixed source initializes the table in the setter, including same-value writes.
            FieldInfo table = typeof(TextBoxBase).GetField("s_shortcutsToDisable",
                BindingFlags.Static | BindingFlags.NonPublic)!;
            object? previous = table.GetValue(null);
            try
            {
                table.SetValue(null, null);
                using var textBox = new CommandTextBox { ForcedSetting = false };
                Message message = Message.Create(0, 0x0100, (nint)Keys.C, 0);
                Assert.False(textBox.ProcessCommand(ref message, Keys.Control | Keys.C));
                Assert.True(textBox.StoredShortcutsEnabled);

                textBox.ShortcutsEnabled = true;

                Assert.True(textBox.ProcessCommand(ref message, Keys.Control | Keys.C));
                Assert.True(textBox.StoredShortcutsEnabled);
                Assert.False(textBox.IsHandleCreated);
            }
            finally { table.SetValue(null, previous); }
        });

    [Theory]
    [InlineData(Keys.Control | Keys.L)]
    [InlineData(Keys.Control | Keys.R)]
    [InlineData(Keys.Control | Keys.E)]
    [InlineData(Keys.Control | Keys.J)]
    public void ReadOnlyConsumesExactAlignmentCommandsEvenWithShortcutsEnabled(Keys keyData)
        => OnSta(() =>
        {
            using var textBox = new CommandTextBox();
            textBox.SetStoredReadOnly(true);
            textBox.ReadOnly = false; // A hidden derived property must not change the base state.
            Message message = Message.Create(0, 0x0100, (nint)(keyData & Keys.KeyCode), 0);

            Assert.True(textBox.ProcessCommand(ref message, keyData));
            Assert.True(textBox.StoredReadOnly);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(Keys.Control | Keys.Shift | Keys.L)]
    [InlineData(Keys.Control | Keys.Shift | Keys.R)]
    [InlineData(Keys.Control | Keys.Shift | Keys.E)]
    [InlineData(Keys.Control | Keys.Shift | Keys.J)]
    [InlineData(Keys.Control | Keys.Alt | Keys.L)]
    public void ReadOnlyDoesNotExpandAlignmentGuardToShiftOrAltVariants(Keys keyData)
        => OnSta(() =>
        {
            using var textBox = new CommandTextBox();
            textBox.SetStoredReadOnly(true);
            Message message = Message.Create(0, 0x0100, (nint)(keyData & Keys.KeyCode), 0);

            Assert.False(textBox.ProcessCommand(ref message, keyData));
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void HiddenDerivedReadOnlyCannotEnableBaseReadOnlyAlignmentGuard()
        => OnSta(() =>
        {
            using var textBox = new CommandTextBox { ReadOnly = true };
            Message message = Message.Create(0, 0x0100, (nint)Keys.L, 0);

            Assert.False(textBox.ProcessCommand(ref message, Keys.Control | Keys.L));
            Assert.False(textBox.StoredReadOnly);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(-1L)]
    [InlineData(0x0014000AL)]
    public void DisabledNativeContextMenuWithoutCustomMenuNeverCallsNativeDefault(long lParam)
        => OnSta(() =>
        {
            using var textBox = new CommandTextBox { ShortcutsEnabled = false, Text = "safe" };
            nint handle = textBox.Handle;

            // DefWndProc is also intercepted, so a regression cannot open the system menu.
            nint result = SendMessageW(handle, WmContextMenu, handle, (nint)lParam);

            Assert.Equal((nint)0, result);
            Assert.Equal(0, textBox.ContextDefaultCalls);
            Assert.Equal(handle, textBox.Handle);
            Assert.Equal("safe", textBox.Text);
        });

    [Fact]
    public void EnabledContextMenuContinuesToDefaultWindowProcedure()
        => OnSta(() =>
        {
            using var textBox = new CommandTextBox();
            nint handle = textBox.Handle;

            Assert.Equal((nint)0x1234, SendMessageW(handle, WmContextMenu, handle, (nint)(-1)));
            Assert.Equal(1, textBox.ContextDefaultCalls);
        });

    [Theory]
    [InlineData(-1L, 50, 20)]
    [InlineData(0x000A0008L, 8, 10)]
    public void DisabledCustomContextMenuUsesCenterOrClientPointBeforeExistingRoute(
        long lParam, int expectedX, int expectedY)
        => OnSta(() =>
        {
            using var menu = new ContextMenuStrip();
            using var textBox = new CommandTextBox
            {
                BorderStyle = BorderStyle.None,
                Size = new Size(100, 40),
                ShortcutsEnabled = false,
                ContextMenuStrip = menu,
                PreventHandleCreation = true
            };

            Message message = textBox.DispatchContextMenu((nint)lParam);

            Assert.Equal((nint)0, message.Result);
            Assert.Equal(new Point(expectedX, expectedY), LastContextMenuLocation(textBox));
            Assert.True(LastContextMenuTick(textBox) > 0);
            Assert.Equal(0, textBox.ContextDefaultCalls);
            Assert.False(textBox.IsHandleCreated);
            Assert.InRange(textBox.CreateHandleAttempts, 1, 2);
            // The test-only creation override leaves owner HWND=0; ShowCore returns before
            // TrackPopupMenu. This proves routing, not real custom-menu display or interaction.
        });

    [Fact]
    public void DisabledCustomContextMenuOutsideClientDoesNotEnterExistingShowRoute()
        => OnSta(() =>
        {
            using var menu = new ContextMenuStrip();
            using var textBox = new CommandTextBox
            {
                BorderStyle = BorderStyle.None,
                Size = new Size(100, 40),
                ShortcutsEnabled = false,
                ContextMenuStrip = menu,
                PreventHandleCreation = true
            };

            Message message = textBox.DispatchContextMenu((nint)0x000AFFFF); // client (-1, 10)

            Assert.Equal((nint)0, message.Result);
            Assert.Equal(long.MinValue, LastContextMenuTick(textBox));
            Assert.Equal(0, textBox.ContextDefaultCalls);
            Assert.False(textBox.IsHandleCreated);
        });

    private static Point LastContextMenuLocation(Control control)
        => (Point)typeof(Control).GetField("_lastContextMenuScreenLocation",
            BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(control)!;

    private static long LastContextMenuTick(Control control)
        => (long)typeof(Control).GetField("_lastContextMenuShownTick",
            BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(control)!;

    private static void OnSta(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            try { action(); }
            catch (Exception exception) { failure = exception; }
            finally
            {
                try
                {
                    Application.ExitThread();
                    typeof(Application).GetMethod("DisposeThemedCommonControlsActivation",
                        BindingFlags.Static | BindingFlags.NonPublic)!.Invoke(null, null);
                }
                catch (Exception exception)
                {
                    failure = failure is null ? exception : new AggregateException(failure, exception);
                }
            }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox shortcut test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class CommandTextBox : TextBox
    {
        internal List<string>? Trace { get; set; }
        internal bool? ForcedSetting { get; set; }
        internal Exception? GetterFailure { get; set; }
        internal int GetterCalls { get; private set; }
        internal int ContextDefaultCalls { get; private set; }
        internal bool PreventHandleCreation { get; set; }
        internal int CreateHandleAttempts { get; private set; }
        internal bool StoredShortcutsEnabled => base.ShortcutsEnabled;
        internal bool StoredReadOnly => base.ReadOnly;
        public new bool ReadOnly { get; set; }

        public override bool ShortcutsEnabled
        {
            get
            {
                GetterCalls++;
                Trace?.Add("getter");
                if (GetterFailure is not null) { throw GetterFailure; }
                return ForcedSetting ?? base.ShortcutsEnabled;
            }
            set => base.ShortcutsEnabled = value;
        }

        internal void SetStoredReadOnly(bool value) => base.ReadOnly = value;

        internal bool ProcessCommand(ref Message message, Keys keyData)
            => base.ProcessCmdKey(ref message, keyData);

        internal Message DispatchContextMenu(nint lParam)
        {
            Message message = Message.Create(0, WmContextMenu, 0, lParam);
            base.WndProc(ref message);
            return message;
        }

        protected override void CreateHandle()
        {
            CreateHandleAttempts++;
            if (!PreventHandleCreation) { base.CreateHandle(); }
        }

        protected override void DefWndProc(ref Message message)
        {
            if (message.Msg == WmContextMenu)
            {
                ContextDefaultCalls++;
                message.Result = (nint)0x1234;
                return;
            }
            base.DefWndProc(ref message);
        }
    }

    private sealed class RoutingControl : Control
    {
        internal List<string>? Trace { get; set; }
        internal bool ReturnValue { get; set; }
        internal bool MutateMessage { get; set; }
        internal Exception? Failure { get; set; }
        internal int Calls { get; private set; }
        internal Keys LastKeyData { get; private set; }

        protected override bool ProcessCmdKey(ref Message message, Keys keyData)
        {
            Calls++;
            LastKeyData = keyData;
            Trace?.Add("parent");
            if (Failure is not null) { throw Failure; }
            if (MutateMessage)
            {
                message.HWnd = (nint)17;
                message.Msg = 0x0101;
                message.WParam = (nint)19;
                message.LParam = (nint)23;
                message.Result = (nint)29;
            }
            return ReturnValue;
        }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, nint lParam);
}
