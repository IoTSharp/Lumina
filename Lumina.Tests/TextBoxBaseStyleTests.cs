using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxBaseStyleTests
{
    private const int GwlStyle = -16;
    private const int EsAutoVScroll = 0x0040;
    private const int EsAutoHScroll = 0x0080;
    private const int WsVScroll = 0x00200000;
    private const int EmGetLineCount = 0x00BA;
    private const int EmGetModify = 0x00B8;
    private const int EmSetModify = 0x00B9;

    [Fact]
    public void StandardDefaultsAndPropertyMetadataDoNotCreateHandle()
        => OnSta(() =>
        {
            using var textBox = new TextBox();
            Assert.False(textBox.AcceptsTab);
            Assert.True(textBox.WordWrap);
            PropertyInfo acceptsTab = typeof(TextBoxBase).GetProperty(nameof(TextBoxBase.AcceptsTab))!;
            PropertyInfo wordWrap = typeof(TextBoxBase).GetProperty(nameof(TextBoxBase.WordWrap))!;
            Assert.Equal(false, acceptsTab.GetCustomAttribute<DefaultValueAttribute>()!.Value);
            Assert.Equal(true, wordWrap.GetCustomAttribute<DefaultValueAttribute>()!.Value);
            Assert.True(wordWrap.GetCustomAttribute<LocalizableAttribute>()!.IsLocalizable);
            Assert.Equal(typeof(TextBoxBase), typeof(TextBox).GetEvent(nameof(TextBoxBase.AcceptsTabChanged))!.DeclaringType);
            Assert.Null(typeof(TextBoxBase).GetEvent("WordWrapChanged"));
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void AcceptsTabNotifiesOnlyChangesThroughTheSharedEventSlot()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox();
            TextBoxBase editing = textBox;
            var values = new List<bool>();
            EventHandler handler = (sender, args) =>
            {
                Assert.Same(textBox, sender);
                Assert.Same(EventArgs.Empty, args);
                values.Add(editing.AcceptsTab);
            };
            editing.AcceptsTabChanged += handler;

            editing.AcceptsTab = false;
            editing.AcceptsTab = true;
            textBox.AcceptsTab = true;
            textBox.AcceptsTab = false;
            textBox.AcceptsTabChanged -= handler;
            editing.AcceptsTab = true;

            Assert.Equal(new[] { true, false }, values);
            Assert.Equal(3, textBox.HookCalls);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void AcceptsTabVirtualHookSeesCommittedStateAndPreservesDirectArguments()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox();
            var trace = new List<string>();
            textBox.BeforeEvent = args => trace.Add($"hook:{textBox.AcceptsTab}:{ReferenceEquals(args, EventArgs.Empty)}");
            textBox.AcceptsTabChanged += (_, args) => trace.Add($"event:{textBox.AcceptsTab}:{ReferenceEquals(args, EventArgs.Empty)}");

            textBox.AcceptsTab = true;
            var supplied = new EventArgs();
            EventArgs? observed = null;
            textBox.AcceptsTabChanged += (_, args) => observed = args;
            textBox.RaiseAcceptsTabChanged(supplied);

            Assert.Equal(new[] { "hook:True:True", "event:True:True", "hook:True:False", "event:True:False" }, trace);
            Assert.Same(supplied, observed);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void AcceptsTabOverrideCanSuppressTheBaseNotification()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox { CallBase = false };
            int events = 0;
            textBox.AcceptsTabChanged += (_, _) => events++;

            textBox.AcceptsTab = true;

            Assert.True(textBox.AcceptsTab);
            Assert.Equal(1, textBox.HookCalls);
            Assert.Equal(0, events);
        });

    [Fact]
    public void AcceptsTabHandlerFailureKeepsCommittedStateAndOriginalException()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox();
            var failure = new InvalidOperationException("AcceptsTab callback");
            EventHandler handler = (_, _) => throw failure;
            textBox.AcceptsTabChanged += handler;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.AcceptsTab = true));
            Assert.True(textBox.AcceptsTab);
            textBox.AcceptsTab = true;
            Assert.Equal(1, textBox.HookCalls);
            textBox.AcceptsTabChanged -= handler;
            textBox.AcceptsTab = false;

            Assert.Equal(2, textBox.HookCalls);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void AcceptsTabReentryUsesTheCurrentValueWithoutDuplicateOuterNotification()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox();
            var values = new List<bool>();
            textBox.AcceptsTabChanged += (_, _) =>
            {
                values.Add(textBox.AcceptsTab);
                if (textBox.AcceptsTab) { textBox.AcceptsTab = false; }
            };

            textBox.AcceptsTab = true;

            Assert.Equal(new[] { true, false }, values);
            Assert.False(textBox.AcceptsTab);
            Assert.Equal(2, textBox.HookCalls);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void AcceptsTabUsesComponentEventSuppressionWithoutSuppressingTheVirtualHook()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox { SuppressEvents = true };
            int events = 0;
            textBox.AcceptsTabChanged += (_, _) => events++;

            textBox.AcceptsTab = true;
            Assert.Equal(0, events);
            Assert.Equal(1, textBox.HookCalls);
            textBox.SuppressEvents = false;
            textBox.AcceptsTab = false;

            Assert.Equal(1, events);
            Assert.Equal(2, textBox.HookCalls);
        });

    [Fact]
    public void WordWrapChangesBeforeCreationRemainLazy()
        => OnSta(() =>
        {
            using var textBox = new TextBox { Multiline = true, Text = "alpha beta" };
            textBox.Select(2, 5);
            int created = 0;
            textBox.HandleCreated += (_, _) => created++;

            textBox.WordWrap = false;
            textBox.WordWrap = false;
            textBox.WordWrap = true;

            Assert.True(textBox.WordWrap);
            Assert.Equal("alpha beta", textBox.Text);
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(5, textBox.SelectionLength);
            Assert.Equal(0, created);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(false, false, true)]
    [InlineData(false, true, true)]
    [InlineData(true, false, true)]
    [InlineData(true, true, false)]
    public void NativeAutoScrollStylesUseMultilineAndWordWrap(bool multiline, bool wordWrap, bool autoHScroll)
        => OnSta(() =>
        {
            using var textBox = new TextBox { Multiline = multiline, WordWrap = wordWrap };
            int style = GetWindowLongW(textBox.Handle, GwlStyle);

            Assert.Equal(autoHScroll, (style & EsAutoHScroll) != 0);
            Assert.NotEqual(0, style & EsAutoVScroll);
            Assert.Equal(0, style & WsVScroll);
        });

    [Fact]
    public void WordWrapRecreationKeepsTextSelectionAndModifiedStateWithoutTextChanged()
        => OnSta(() =>
        {
            using var textBox = new TextBox { Multiline = true, Text = "alpha beta", ReadOnly = true };
            nint first = textBox.Handle;
            textBox.Select(2, 5);
            _ = SendMessageW(first, EmSetModify, 1, 0);
            var events = new List<string>();
            textBox.HandleDestroyed += (_, _) => events.Add("destroyed");
            textBox.HandleCreated += (_, _) =>
            {
                Assert.False(textBox.WordWrap);
                Assert.NotEqual(0, GetWindowLongW(textBox.Handle, GwlStyle) & EsAutoHScroll);
                events.Add("created");
            };
            int textChanges = 0;
            textBox.TextChanged += (_, _) => textChanges++;

            textBox.WordWrap = false;

            Assert.False(IsWindow(first));
            Assert.NotEqual(first, textBox.Handle);
            Assert.Equal(new[] { "destroyed", "created" }, events);
            Assert.Equal("alpha beta", textBox.Text);
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(5, textBox.SelectionLength);
            Assert.True(textBox.ReadOnly);
            Assert.NotEqual(0, SendMessageW(textBox.Handle, EmGetModify, 0, 0));
            Assert.Equal(0, textChanges);
            nint second = textBox.Handle;
            textBox.WordWrap = false;
            Assert.Equal(second, textBox.Handle);
            Assert.Equal(2, events.Count);
        });

    [Fact]
    public void SingleLineWordWrapChangeRecreatesEvenThoughNativeHorizontalScrollingRemains()
        => OnSta(() =>
        {
            using var textBox = new TextBox { Text = "alpha beta" };
            nint first = textBox.Handle;
            int created = 0;
            textBox.HandleCreated += (_, _) => created++;

            textBox.WordWrap = false;

            Assert.NotEqual(first, textBox.Handle);
            Assert.False(IsWindow(first));
            Assert.Equal(1, created);
            Assert.NotEqual(0, GetWindowLongW(textBox.Handle, GwlStyle) & EsAutoHScroll);
            textBox.WordWrap = false;
            Assert.Equal(1, created);
        });

    [Fact]
    public void AutoSizeWordWrapAssignmentRequestsParentLayoutIncludingSameValues()
        => OnSta(() =>
        {
            using var parent = new Control();
            using var textBox = new TextBox { AutoSize = true };
            parent.Controls.Add(textBox);
            var values = new List<bool>();
            parent.Layout += (_, args) =>
            {
                if (args.AffectedProperty != nameof(TextBoxBase.WordWrap)) { return; }
                Assert.Same(textBox, args.AffectedControl);
                values.Add(textBox.WordWrap);
            };

            textBox.WordWrap = true;
            textBox.WordWrap = false;
            textBox.WordWrap = false;

            Assert.Equal(new[] { true, false, false }, values);
            Assert.False(textBox.IsHandleCreated);
            textBox.AutoSize = false;
            textBox.WordWrap = true;
            Assert.Equal(3, values.Count);
        });

    [Fact]
    public void NonAutoSizeSameValueWordWrapAssignmentInvalidatesOnlyTheControlPreferredSizeCache()
        => OnSta(() =>
        {
            using var parent = new Panel { Size = new Size(300, 200) };
            using var textBox = new TextBox { Size = new Size(90, 30), AutoSize = false };
            parent.Controls.Add(textBox);
            PropertyInfo cacheEnabled = typeof(Control).GetProperty("CachePreferredSize", BindingFlags.Instance | BindingFlags.NonPublic)!;
            FieldInfo cache = typeof(Control).GetField("_preferredSizeCache", BindingFlags.Instance | BindingFlags.NonPublic)!;
            cacheEnabled.SetValue(textBox, true);
            _ = textBox.PreferredSize;
            _ = parent.PreferredSize;
            Assert.NotEqual(Size.Empty, (Size)cache.GetValue(textBox)!);
            Size parentCache = (Size)cache.GetValue(parent)!;
            Assert.NotEqual(Size.Empty, parentCache);
            int layouts = 0;
            parent.Layout += (_, _) => layouts++;

            textBox.WordWrap = true;

            Assert.Equal(Size.Empty, (Size)cache.GetValue(textBox)!);
            Assert.Equal(parentCache, (Size)cache.GetValue(parent)!);
            Assert.Equal(0, layouts);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void NativeWordWrappingActuallyChangesTheEditLineCount()
        => OnSta(() =>
        {
            using var textBox = new TextBox
            {
                Multiline = true,
                Size = new Size(90, 80),
                Text = new string('W', 120)
            };
            Assert.True(SendMessageW(textBox.Handle, EmGetLineCount, 0, 0) > 1);

            textBox.WordWrap = false;
            Assert.Equal((nint)1, SendMessageW(textBox.Handle, EmGetLineCount, 0, 0));

            textBox.WordWrap = true;
            Assert.True(SendMessageW(textBox.Handle, EmGetLineCount, 0, 0) > 1);
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBoxBase style test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class HookTextBox : TextBox
    {
        internal int HookCalls { get; private set; }
        internal bool CallBase { get; set; } = true;
        internal bool SuppressEvents { get; set; }
        internal Action<EventArgs>? BeforeEvent { get; set; }
        internal void RaiseAcceptsTabChanged(EventArgs e) => OnAcceptsTabChanged(e);
        protected override bool CanRaiseEvents => !SuppressEvents && base.CanRaiseEvents;

        protected override void OnAcceptsTabChanged(EventArgs e)
        {
            HookCalls++;
            BeforeEvent?.Invoke(e);
            if (CallBase) { base.OnAcceptsTabChanged(e); }
        }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern int GetWindowLongW(nint hWnd, int index);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint hWnd);
}
