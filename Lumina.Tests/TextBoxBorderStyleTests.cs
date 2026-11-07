using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxBorderStyleTests
{
    private const int GwlStyle = -16;
    private const int GwlExStyle = -20;
    private const int WsBorder = 0x00800000;
    private const int WsExClientEdge = 0x00000200;
    private const int EmGetModify = 0x00B8;
    private const int EmSetModify = 0x00B9;
    private const int EmGetPasswordChar = 0x00D2;

    [Fact]
    public void StandardDefaultAndMetadataAreDeclaredByTextBoxBase()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox();
            TextBoxBase editing = textBox;
            Assert.Equal(BorderStyle.Fixed3D, editing.BorderStyle);
            PropertyInfo property = typeof(TextBox).GetProperty(nameof(TextBoxBase.BorderStyle))!;
            Assert.Equal(typeof(TextBoxBase), property.DeclaringType);
            Assert.Equal(BorderStyle.Fixed3D, property.GetCustomAttribute<DefaultValueAttribute>()!.Value);
            Assert.Equal("Appearance", property.GetCustomAttribute<CategoryAttribute>()!.Category);
            Assert.Equal(-504, property.GetCustomAttribute<DispIdAttribute>()!.Value);
            Assert.False(property.GetMethod!.IsVirtual);
            Assert.False(property.SetMethod!.IsVirtual);
            EventInfo notification = typeof(TextBox).GetEvent(nameof(TextBoxBase.BorderStyleChanged))!;
            Assert.Equal(typeof(TextBoxBase), notification.DeclaringType);
            Assert.Equal(typeof(EventHandler), notification.EventHandlerType);
            Assert.Equal("Property Changed", notification.GetCustomAttribute<CategoryAttribute>()!.Category);
            MethodInfo hook = typeof(TextBoxBase).GetMethod("OnBorderStyleChanged",
                BindingFlags.Instance | BindingFlags.NonPublic)!;
            Assert.True(hook.IsFamily && hook.IsVirtual);
            Assert.Equal(0, textBox.RequestedStyle & WsBorder);
            Assert.Equal(WsExClientEdge, textBox.RequestedExStyle & WsExClientEdge);
            Assert.Equal(WsExClientEdge, textBox.LegacyExStyle & WsExClientEdge);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(-1)]
    [InlineData(3)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void InvalidValuesKeepStateEventsAndHandleUntouched(int value)
        => OnSta(() =>
        {
            using var textBox = new HookTextBox();
            int styles = 0;
            textBox.StyleChanged += (_, _) => styles++;

            InvalidEnumArgumentException failure = Assert.Throws<InvalidEnumArgumentException>(
                () => textBox.BorderStyle = (BorderStyle)value);

            Assert.Equal("value", failure.ParamName);
            Assert.Equal(BorderStyle.Fixed3D, textBox.BorderStyle);
            Assert.Equal(0, styles);
            Assert.Equal(0, textBox.HookCalls);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void PendingChangesNotifyAfterStyleAndRemainLazyWithSubscriptionRemoval()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox { Text = "alpha beta" };
            textBox.Select(2, 5);
            var trace = new List<string>();
            textBox.StyleChanged += (_, _) => trace.Add($"style:{textBox.BorderStyle}");
            EventHandler handler = (sender, args) =>
            {
                Assert.Same(textBox, sender);
                Assert.Same(EventArgs.Empty, args);
                trace.Add($"border:{textBox.BorderStyle}");
            };
            textBox.BorderStyleChanged += handler;

            textBox.BorderStyle = BorderStyle.Fixed3D;
            textBox.BorderStyle = BorderStyle.None;
            textBox.BorderStyle = BorderStyle.None;
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.BorderStyleChanged -= handler;
            textBox.BorderStyle = BorderStyle.Fixed3D;

            Assert.Equal(new[] { "style:None", "border:None", "style:FixedSingle",
                "border:FixedSingle", "style:Fixed3D" }, trace);
            Assert.Equal(3, textBox.HookCalls);
            Assert.Equal("alpha beta", textBox.Text);
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(5, textBox.SelectionLength);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void VirtualHookPreservesSuppliedArgumentsAndCanSuppressTheBaseEvent()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox();
            var args = new EventArgs();
            EventArgs? observed = null;
            textBox.BorderStyleChanged += (_, value) => observed = value;

            textBox.RaiseBorderStyleChanged(args);

            Assert.Same(args, observed);
            Assert.Same(args, textBox.LastArguments);
            observed = null;
            textBox.CallBase = false;
            textBox.BorderStyle = BorderStyle.None;
            Assert.Null(observed);
            Assert.Equal(BorderStyle.None, textBox.BorderStyle);
            Assert.Equal(2, textBox.HookCalls);
        });

    [Fact]
    public void ComponentSuppressionLeavesTheVirtualHookAndStateActive()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox { SuppressEvents = true };
            int events = 0;
            textBox.BorderStyleChanged += (_, _) => events++;

            textBox.BorderStyle = BorderStyle.None;

            Assert.Equal(1, textBox.HookCalls);
            Assert.Equal(0, events);
            textBox.SuppressEvents = false;
            textBox.BorderStyle = BorderStyle.FixedSingle;
            Assert.Equal(2, textBox.HookCalls);
            Assert.Equal(1, events);
        });

    [Fact]
    public void ReentrantBorderHandlerObservesCommittedValues()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox();
            var values = new List<BorderStyle>();
            textBox.BorderStyleChanged += (_, _) =>
            {
                values.Add(textBox.BorderStyle);
                if (textBox.BorderStyle == BorderStyle.None) { textBox.BorderStyle = BorderStyle.FixedSingle; }
            };

            textBox.BorderStyle = BorderStyle.None;

            Assert.Equal(new[] { BorderStyle.None, BorderStyle.FixedSingle }, values);
            Assert.Equal(BorderStyle.FixedSingle, textBox.BorderStyle);
            Assert.Equal(2, textBox.HookCalls);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ParentLayoutRunsAfterBorderNotificationOnlyForAutoSize(bool autoSize)
        => OnSta(() =>
        {
            using var parent = new Control { Size = new Size(300, 180) };
            using var textBox = new HookTextBox { AutoSize = autoSize, Size = new Size(100, 30) };
            parent.Controls.Add(textBox);
            var trace = new List<string>();
            parent.Layout += (_, args) =>
            {
                if (args.AffectedProperty != nameof(TextBoxBase.BorderStyle)) { return; }
                Assert.Same(textBox, args.AffectedControl);
                trace.Add($"layout:{textBox.BorderStyle}");
            };
            textBox.StyleChanged += (_, _) => trace.Add($"style:{textBox.BorderStyle}");
            textBox.BorderStyleChanged += (_, _) => trace.Add($"border:{textBox.BorderStyle}");

            textBox.BorderStyle = BorderStyle.None;
            textBox.BorderStyle = BorderStyle.None;

            Assert.Equal(autoSize
                ? new[] { "style:None", "border:None", "layout:None" }
                : new[] { "style:None", "border:None" }, trace);
            Assert.False(textBox.IsHandleCreated);
            Assert.False(parent.IsHandleCreated);
        });

    [Fact]
    public void NonAutoSizeChangeClearsOnlyItsPreferredCacheAndSameValueDoesNotClear()
        => OnSta(() =>
        {
            using var parent = new Panel { Size = new Size(300, 180) };
            using var textBox = new HookTextBox { AutoSize = false, Size = new Size(100, 30) };
            parent.Controls.Add(textBox);
            PropertyInfo cacheEnabled = typeof(Control).GetProperty("CachePreferredSize", BindingFlags.Instance | BindingFlags.NonPublic)!;
            FieldInfo cache = typeof(Control).GetField("_preferredSizeCache", BindingFlags.Instance | BindingFlags.NonPublic)!;
            cacheEnabled.SetValue(textBox, true);
            _ = textBox.PreferredSize;
            _ = parent.PreferredSize;
            Size parentCache = (Size)cache.GetValue(parent)!;
            Assert.NotEqual(Size.Empty, (Size)cache.GetValue(textBox)!);
            Assert.NotEqual(Size.Empty, parentCache);

            textBox.BorderStyle = BorderStyle.None;

            Assert.Equal(Size.Empty, (Size)cache.GetValue(textBox)!);
            Assert.Equal(parentCache, (Size)cache.GetValue(parent)!);
            _ = textBox.PreferredSize;
            Size childCache = (Size)cache.GetValue(textBox)!;
            Assert.NotEqual(Size.Empty, childCache);
            textBox.BorderStyle = BorderStyle.None;
            Assert.Equal(childCache, (Size)cache.GetValue(textBox)!);
            Assert.Equal(parentCache, (Size)cache.GetValue(parent)!);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void ThrowingBorderHandlerPropagatesOriginalFailureAndResumesParentLayout()
        => OnSta(() =>
        {
            using var parent = new Control { Size = new Size(300, 180) };
            using var textBox = new HookTextBox { AutoSize = true, Size = new Size(100, 30) };
            parent.Controls.Add(textBox);
            var failure = new InvalidOperationException("BorderStyle callback");
            var trace = new List<string>();
            parent.Layout += (_, args) =>
            {
                if (args.AffectedProperty == nameof(TextBoxBase.BorderStyle)) { trace.Add("layout"); }
            };
            EventHandler handler = (_, _) => { trace.Add("border"); throw failure; };
            textBox.BorderStyleChanged += handler;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.BorderStyle = BorderStyle.None));

            Assert.Equal(new[] { "border", "layout" }, trace);
            Assert.Equal(BorderStyle.None, textBox.BorderStyle);
            textBox.BorderStyleChanged -= handler;
            textBox.BorderStyle = BorderStyle.FixedSingle;
            Assert.Equal(new[] { "border", "layout", "layout" }, trace);
        });

    [Fact]
    public void ThrowingStyleHandlerStopsBeforeRecreationAndBorderNotification()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new HookTextBox { Size = new Size(150, 40) };
            nint original = CreateHostedHandle(form, textBox);
            var failure = new InvalidOperationException("StyleChanged callback");
            EventHandler handler = (_, _) => throw failure;
            textBox.StyleChanged += handler;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.BorderStyle = BorderStyle.None));

            Assert.Equal(BorderStyle.None, textBox.BorderStyle);
            Assert.True(IsWindow(original));
            Assert.Equal(original, textBox.Handle);
            Assert.Equal(0, GetWindowLongW(original, GwlExStyle) & WsExClientEdge);
            Assert.Equal(0, textBox.HookCalls);
            textBox.StyleChanged -= handler;
        });

    [Theory]
    [InlineData(false, BorderStyle.None)]
    [InlineData(false, BorderStyle.FixedSingle)]
    [InlineData(false, BorderStyle.Fixed3D)]
    [InlineData(true, BorderStyle.None)]
    [InlineData(true, BorderStyle.FixedSingle)]
    [InlineData(true, BorderStyle.Fixed3D)]
    public void RealNativeBorderMatchesRequestedStyleAndAnIndependentEdit(bool multiline, BorderStyle borderStyle)
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new HookTextBox
            {
                Size = new Size(150, 80), Multiline = multiline, BorderStyle = borderStyle, Text = "alpha beta"
            };
            int expectedStyle = borderStyle == BorderStyle.FixedSingle ? WsBorder : 0;
            int expectedExStyle = borderStyle == BorderStyle.Fixed3D ? WsExClientEdge : 0;
            Assert.Equal(expectedStyle, textBox.RequestedStyle & WsBorder);
            Assert.Equal(expectedExStyle, textBox.RequestedExStyle & WsExClientEdge);
            Assert.Equal(WsExClientEdge, textBox.LegacyExStyle & WsExClientEdge);
            Assert.False(textBox.IsHandleCreated);
            nint handle = CreateHostedHandle(form, textBox);
            nint independent = CreateWindowExW((uint)expectedExStyle, "EDIT", "alpha beta",
                0x400100C0u | (multiline ? 4u : 0u) | (uint)expectedStyle,
                0, 0, 150, 80, form.Handle, 0, 0, 0);
            Assert.NotEqual(0, independent);
            try
            {
                Assert.Equal(GetWindowLongW(independent, GwlStyle) & WsBorder,
                    GetWindowLongW(handle, GwlStyle) & WsBorder);
                Assert.Equal(GetWindowLongW(independent, GwlExStyle) & WsExClientEdge,
                    GetWindowLongW(handle, GwlExStyle) & WsExClientEdge);
            }
            finally { Assert.True(DestroyWindow(independent)); }
        });

    [Theory]
    [InlineData(true, false, false)]
    [InlineData(false, true, false)]
    [InlineData(false, false, true)]
    public void NativeRecreationPrecedesBorderEventAndPreservesEditingState(
        bool multiline, bool customPassword, bool systemPassword)
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new HookTextBox
            {
                Size = new Size(180, 80), Multiline = multiline, ReadOnly = true, WordWrap = false,
                HideSelection = false, ScrollBars = ScrollBars.Both, TextAlign = HorizontalAlignment.Right,
                CharacterCasing = CharacterCasing.Upper, Text = "alpha beta",
                PasswordChar = customPassword ? '#' : '\0', UseSystemPasswordChar = systemPassword
            };
            nint original = CreateHostedHandle(form, textBox);
            string originalText = textBox.Text;
            Assert.Equal("ALPHA BETA", originalText);
            nint password = SendMessageW(original, EmGetPasswordChar, 0, 0);
            textBox.Select(2, 5);
            _ = SendMessageW(original, EmSetModify, 1, 0);
            Assert.True(textBox.Modified);
            var trace = new List<string>();
            int textChanges = 0;
            int selectionChanges = 0;
            textBox.StyleChanged += (_, _) => trace.Add($"style:{textBox.BorderStyle}");
            textBox.HandleDestroyed += (_, _) => trace.Add($"destroyed:{textBox.BorderStyle}");
            textBox.HandleCreated += (_, _) => trace.Add($"created:{textBox.BorderStyle}");
            textBox.BorderStyleChanged += (_, _) =>
            {
                Assert.Equal(2, textBox.SelectionStart);
                Assert.Equal(5, textBox.SelectionLength);
                Assert.True(textBox.Modified);
                trace.Add($"border:{textBox.BorderStyle}");
            };
            textBox.TextChanged += (_, _) => textChanges++;
            textBox.SelectionChanged += (_, _) => selectionChanges++;

            textBox.BorderStyle = BorderStyle.FixedSingle;

            Assert.Equal(new[] { "style:FixedSingle", "destroyed:FixedSingle", "created:FixedSingle", "border:FixedSingle" }, trace);
            Assert.False(IsWindow(original));
            Assert.Equal(WsBorder, textBox.RequestedStyle & WsBorder);
            nint independent = CreateWindowExW((uint)textBox.RequestedExStyle, "EDIT", textBox.Text,
                (uint)textBox.RequestedStyle & ~0x10000000u,
                0, 0, 180, 80, form.Handle, 0, 0, 0);
            Assert.NotEqual(0, independent);
            try
            {
                // EDIT consumes the requested single border during creation
                // and removes WS_BORDER from its public window style. Keep
                // the creation contract and the native observation separate.
                int nativeBorder = GetWindowLongW(independent, GwlStyle) & WsBorder;
                Assert.Equal(0, nativeBorder);
                Assert.Equal(nativeBorder, GetWindowLongW(textBox.Handle, GwlStyle) & WsBorder);
            }
            finally { Assert.True(DestroyWindow(independent)); }
            Assert.Equal(0, GetWindowLongW(textBox.Handle, GwlExStyle) & WsExClientEdge);
            Assert.Equal(form.Handle, GetParent(textBox.Handle));
            Assert.Same(form, Control.FromHandle(GetParent(textBox.Handle)));
            Assert.Equal(originalText, textBox.Text);
            Assert.Equal(password, SendMessageW(textBox.Handle, EmGetPasswordChar, 0, 0));
            Assert.Equal(0x0800, GetWindowLongW(textBox.Handle, GwlStyle) & 0x0800);
            Assert.Equal(CharacterCasing.Upper, textBox.CharacterCasing);
            Assert.Equal(HorizontalAlignment.Right, textBox.TextAlign);
            Assert.Equal(ScrollBars.Both, textBox.ScrollBars);
            Assert.False(textBox.WordWrap);
            Assert.False(textBox.HideSelection);
            Assert.Equal(0, textChanges);
            Assert.Equal(0, selectionChanges);
            nint recreated = textBox.Handle;
            textBox.BorderStyle = BorderStyle.FixedSingle;
            Assert.Equal(recreated, textBox.Handle);
            Assert.Equal(4, trace.Count);
        });

    [Fact]
    public void RecreationFailureKeepsCommittedBorderAndOriginalExceptionWithoutSuccessNotification()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new HookTextBox { Text = "alpha beta", Size = new Size(150, 40) };
            nint original = CreateHostedHandle(form, textBox);
            var failure = new InvalidOperationException("BorderStyle recreation");
            textBox.RecreationFailure = failure;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.BorderStyle = BorderStyle.None));

            Assert.Equal(BorderStyle.None, textBox.BorderStyle);
            Assert.False(IsWindow(original));
            Assert.False(textBox.IsHandleCreated);
            Assert.Equal(0, textBox.HookCalls);
            textBox.RecreationFailure = null;
            Assert.True(IsWindow(textBox.Handle));
            Assert.Equal("alpha beta", textBox.Text);
            Assert.Equal(0, GetWindowLongW(textBox.Handle, GwlStyle) & WsBorder);
            Assert.Equal(0, GetWindowLongW(textBox.Handle, GwlExStyle) & WsExClientEdge);
        });

    private static nint CreateHostedHandle(Form form, TextBox textBox)
    {
        form.Controls.Add(textBox);
        nint parent = form.Handle;
        nint child = textBox.Handle;
        Assert.Equal(parent, GetParent(child));
        Assert.Same(form, Control.FromHandle(GetParent(child)));
        return child;
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox BorderStyle lifecycle timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class HookTextBox : TextBox
    {
        internal bool CallBase { get; set; } = true;
        internal bool SuppressEvents { get; set; }
        internal int HookCalls { get; private set; }
        internal EventArgs? LastArguments { get; private set; }
        internal Exception? RecreationFailure { get; set; }
        internal int RequestedStyle => CreateParams.Style;
        internal int RequestedExStyle => CreateParams.ExStyle;
        internal int LegacyExStyle => unchecked((int)base.ExStyle);
        protected override bool CanRaiseEvents => !SuppressEvents;

        protected override CreateParams CreateParams
        {
            get
            {
                if (RecreatingHandle && RecreationFailure is not null) { throw RecreationFailure; }
                return base.CreateParams;
            }
        }

        internal void RaiseBorderStyleChanged(EventArgs e) => OnBorderStyleChanged(e);

        protected override void OnBorderStyleChanged(EventArgs e)
        {
            HookCalls++;
            LastArguments = e;
            if (CallBase) { base.OnBorderStyleChanged(e); }
        }
    }

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint CreateWindowExW(uint exStyle, string className, string? windowName,
        uint style, int x, int y, int width, int height, nint parent, nint menu, nint instance, nint param);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern int GetWindowLongW(nint hWnd, int index);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetParent(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint hWnd);
}
