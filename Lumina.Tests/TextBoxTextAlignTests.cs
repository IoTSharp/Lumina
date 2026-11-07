using System.ComponentModel;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxTextAlignTests
{
    private const int GwlStyle = -16;
    private const int GwlExStyle = -20;
    private const int AlignmentAndScrollMask = 0x00300003;
    private const int WsExRight = 0x00001000;
    private const int EmGetModify = 0x00B8;
    private const int EmSetModify = 0x00B9;
    private const int EmGetPasswordChar = 0x00D2;

    [Fact]
    public void DefaultAlignmentAndStandardMetadataDoNotCreateAHandle()
        => OnSta(() =>
        {
            using var textBox = new TextBox();
            Assert.Equal(0, (int)HorizontalAlignment.Left);
            Assert.Equal(1, (int)HorizontalAlignment.Right);
            Assert.Equal(2, (int)HorizontalAlignment.Center);
            Assert.Equal(HorizontalAlignment.Left, textBox.TextAlign);
            PropertyInfo property = typeof(TextBox).GetProperty(nameof(TextBox.TextAlign))!;
            Assert.Equal(HorizontalAlignment.Left, property.GetCustomAttribute<DefaultValueAttribute>()!.Value);
            Assert.True(property.GetCustomAttribute<LocalizableAttribute>()!.IsLocalizable);
            Assert.Equal("Appearance", property.GetCustomAttribute<CategoryAttribute>()!.Category);
            Assert.False(property.GetMethod!.IsVirtual);
            MethodInfo hook = typeof(TextBox).GetMethod("OnTextAlignChanged", BindingFlags.Instance | BindingFlags.NonPublic)!;
            Assert.True(hook.IsFamily);
            Assert.True(hook.IsVirtual);
            Assert.Equal(typeof(void), hook.ReturnType);
            textBox.TextAlign = HorizontalAlignment.Left;
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(-1)]
    [InlineData(3)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void InvalidAlignmentPreservesPendingStateAndDoesNotCreateAHandle(int value)
        => OnSta(() =>
        {
            using var textBox = new HookTextBox { TextAlign = HorizontalAlignment.Center };
            InvalidEnumArgumentException error = Assert.Throws<InvalidEnumArgumentException>(
                () => textBox.TextAlign = (HorizontalAlignment)value);
            Assert.Equal("value", error.ParamName);
            Assert.Equal(HorizontalAlignment.Center, textBox.TextAlign);
            Assert.Equal(1, textBox.HookCalls);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void AlignmentNotifiesOnlyChangesAndUnsubscribesFromTheComponentEventSlot()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox();
            var values = new List<HorizontalAlignment>();
            EventHandler handler = (sender, args) =>
            {
                Assert.Same(textBox, sender);
                Assert.Same(EventArgs.Empty, args);
                values.Add(textBox.TextAlign);
            };
            textBox.TextAlignChanged += handler;
            textBox.TextAlign = HorizontalAlignment.Left;
            textBox.TextAlign = HorizontalAlignment.Right;
            textBox.TextAlign = HorizontalAlignment.Right;
            textBox.TextAlign = HorizontalAlignment.Center;
            textBox.TextAlignChanged -= handler;
            textBox.TextAlign = HorizontalAlignment.Left;

            Assert.Equal(new[] { HorizontalAlignment.Right, HorizontalAlignment.Center }, values);
            Assert.Equal(3, textBox.HookCalls);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void VirtualHookSeesCommittedStateAndPreservesDirectArguments()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox();
            var trace = new List<string>();
            textBox.BeforeEvent = args => trace.Add($"hook:{textBox.TextAlign}:{ReferenceEquals(args, EventArgs.Empty)}");
            EventArgs? observed = null;
            textBox.TextAlignChanged += (_, args) =>
            {
                observed = args;
                trace.Add($"event:{textBox.TextAlign}:{ReferenceEquals(args, EventArgs.Empty)}");
            };

            textBox.TextAlign = HorizontalAlignment.Right;
            var supplied = new EventArgs();
            textBox.RaiseTextAlignChanged(supplied);

            Assert.Equal(new[] { "hook:Right:True", "event:Right:True", "hook:Right:False", "event:Right:False" }, trace);
            Assert.Same(supplied, observed);
        });

    [Fact]
    public void OverrideCanSuppressTheBaseAlignmentNotification()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox { CallBase = false };
            int notifications = 0;
            textBox.TextAlignChanged += (_, _) => notifications++;
            textBox.TextAlign = HorizontalAlignment.Center;
            Assert.Equal(HorizontalAlignment.Center, textBox.TextAlign);
            Assert.Equal(1, textBox.HookCalls);
            Assert.Equal(0, notifications);
        });

    [Fact]
    public void CanRaiseEventsSuppressesHandlersWithoutSuppressingTheVirtualHook()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox { SuppressEvents = true };
            int notifications = 0;
            textBox.TextAlignChanged += (_, _) => notifications++;
            textBox.TextAlign = HorizontalAlignment.Center;
            Assert.Equal(1, textBox.HookCalls);
            Assert.Equal(0, notifications);
            textBox.SuppressEvents = false;
            textBox.TextAlign = HorizontalAlignment.Right;
            Assert.Equal(2, textBox.HookCalls);
            Assert.Equal(1, notifications);
        });

    [Fact]
    public void HandlerFailurePreservesCommittedAlignmentAndOriginalException()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox();
            var failure = new InvalidOperationException("alignment callback");
            EventHandler handler = (_, _) => throw failure;
            textBox.TextAlignChanged += handler;
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.TextAlign = HorizontalAlignment.Right));
            Assert.Equal(HorizontalAlignment.Right, textBox.TextAlign);
            textBox.TextAlign = HorizontalAlignment.Right;
            Assert.Equal(1, textBox.HookCalls);
            textBox.TextAlignChanged -= handler;
            textBox.TextAlign = HorizontalAlignment.Left;
            Assert.Equal(2, textBox.HookCalls);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void ReentrantAlignmentChangeUsesTheCurrentValue()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox();
            var values = new List<HorizontalAlignment>();
            textBox.TextAlignChanged += (_, _) =>
            {
                values.Add(textBox.TextAlign);
                if (textBox.TextAlign == HorizontalAlignment.Right) { textBox.TextAlign = HorizontalAlignment.Center; }
            };
            textBox.TextAlign = HorizontalAlignment.Right;
            Assert.Equal(new[] { HorizontalAlignment.Right, HorizontalAlignment.Center }, values);
            Assert.Equal(HorizontalAlignment.Center, textBox.TextAlign);
            Assert.Equal(2, textBox.HookCalls);
            Assert.False(textBox.IsHandleCreated);
        });

    // The expected bits are the fixed v10.0.11 logical alignment and horizontal
    // scrollbar contracts. Initial RTL translates Left/Right, while the scroll
    // condition continues to use the configured logical Left value. The native
    // fixture includes the existing Control RTL post-creation style submission;
    // it does not establish full WinForms mirroring or visual alignment parity.
    [Theory]
    [InlineData(false, false, HorizontalAlignment.Left, 0x00000000)]
    [InlineData(false, false, HorizontalAlignment.Center, 0x00000001)]
    [InlineData(false, false, HorizontalAlignment.Right, 0x00000002)]
    [InlineData(false, true, HorizontalAlignment.Left, 0x00000002)]
    [InlineData(false, true, HorizontalAlignment.Center, 0x00000001)]
    [InlineData(false, true, HorizontalAlignment.Right, 0x00000000)]
    [InlineData(true, false, HorizontalAlignment.Left, 0x00300000)]
    [InlineData(true, false, HorizontalAlignment.Center, 0x00200001)]
    [InlineData(true, false, HorizontalAlignment.Right, 0x00200002)]
    [InlineData(true, true, HorizontalAlignment.Left, 0x00300002)]
    [InlineData(true, true, HorizontalAlignment.Center, 0x00200001)]
    [InlineData(true, true, HorizontalAlignment.Right, 0x00200000)]
    public void InitialAlignmentStylesMatchIndependentNativeEdit(
        bool multiline, bool rightToLeft, HorizontalAlignment alignment, int expectedBits)
        => OnSta(() =>
        {
            string text = new string('w', 250) + "\r\nsecond\r\nthird\r\nfourth";
            using var textBox = new StyleTextBox
            {
                Multiline = multiline,
                WordWrap = false,
                ScrollBars = ScrollBars.Both,
                TextAlign = alignment,
                RightToLeft = rightToLeft ? RightToLeft.Yes : RightToLeft.No,
                Text = text,
                Width = 160,
                Height = 56
            };
            CreateParams requested = textBox.RequestedParameters;
            Assert.Equal(expectedBits, requested.Style & AlignmentAndScrollMask);
            Assert.Equal(0, requested.ExStyle & WsExRight);
            Assert.False(textBox.IsHandleCreated);

            uint nativeStyle = 0x800100C0u | (multiline ? 0x00000004u : 0) | (uint)expectedBits;
            uint nativeExStyle = 0x00000200u | (rightToLeft ? 0x00402000u : 0);
            Assert.Equal(unchecked((int)nativeExStyle), requested.ExStyle);
            nint nativeEdit = CreateWindowExW(nativeExStyle, "EDIT", text, nativeStyle,
                0, 0, 160, 56, 0, 0, 0, 0);
            Assert.NotEqual(0, nativeEdit);
            try
            {
                if (rightToLeft)
                {
                    // USER32 normalizes EDIT left/right bits during mirrored
                    // creation. Control.CreateHandle then calls UpdateStyles,
                    // submitting its declared style again before HandleCreated.
                    // Exercise the same lifecycle on this independently created
                    // EDIT; a bare CreateWindowEx tests a different final state.
                    Assert.NotEqual(0, SetWindowLongW(nativeEdit, GwlStyle, unchecked((int)nativeStyle)));
                    Assert.True(SetWindowPos(nativeEdit, 0, 0, 0, 0, 0, 0x0037));
                }
                Assert.True(IsWindow(textBox.Handle));
                Assert.Equal(expectedBits, GetWindowLongW(nativeEdit, GwlStyle) & AlignmentAndScrollMask);
                Assert.Equal(GetWindowLongW(nativeEdit, GwlStyle) & AlignmentAndScrollMask,
                    GetWindowLongW(textBox.Handle, GwlStyle) & AlignmentAndScrollMask);
                Assert.Equal(0, GetWindowLongW(textBox.Handle, GwlExStyle) & WsExRight);
                Assert.Equal(alignment, textBox.TextAlign);
            }
            finally { Assert.True(DestroyWindow(nativeEdit)); }
        });

    [Fact]
    public void InitialInheritedRightToLeftTranslatesAlignmentWithoutCreatingAHandle()
        => OnSta(() =>
        {
            using var parent = new Control { RightToLeft = RightToLeft.Yes };
            using var textBox = new StyleTextBox { Parent = parent, RightToLeft = RightToLeft.Inherit };
            Assert.Equal(2, textBox.RequestedParameters.Style & 3);
            textBox.TextAlign = HorizontalAlignment.Right;
            Assert.Equal(0, textBox.RequestedParameters.Style & 3);
            Assert.False(textBox.IsHandleCreated);
            Assert.False(parent.IsHandleCreated);
        });

    [Fact]
    public void AlignmentChangeRecreatesBeforeNotificationAndPreservesEditingState()
        => OnSta(() =>
        {
            using var textBox = new HookTextBox
            {
                Multiline = true,
                WordWrap = false,
                ScrollBars = ScrollBars.Both,
                ReadOnly = true,
                Text = "alpha beta"
            };
            nint original = textBox.Handle;
            textBox.Select(2, 5);
            _ = SendMessageW(original, EmSetModify, 1, 0);
            var trace = new List<string>();
            int textChanges = 0;
            int selectionChanges = 0;
            textBox.HandleDestroyed += (_, _) => trace.Add("destroyed");
            textBox.HandleCreated += (_, _) =>
            {
                Assert.Equal(HorizontalAlignment.Center, textBox.TextAlign);
                trace.Add("created");
            };
            textBox.BeforeEvent = _ => trace.Add("hook");
            textBox.TextAlignChanged += (_, _) =>
            {
                Assert.True(textBox.IsHandleCreated);
                Assert.Equal(2, textBox.SelectionStart);
                Assert.Equal(5, textBox.SelectionLength);
                Assert.NotEqual(0, SendMessageW(textBox.Handle, EmGetModify, 0, 0));
                trace.Add("event");
            };
            textBox.TextChanged += (_, _) => textChanges++;
            textBox.SelectionChanged += (_, _) => selectionChanges++;

            textBox.TextAlign = HorizontalAlignment.Center;

            Assert.Equal(new[] { "destroyed", "created", "hook", "event" }, trace);
            Assert.False(IsWindow(original));
            Assert.Equal("alpha beta", textBox.Text);
            Assert.NotEqual(0, GetWindowLongW(textBox.Handle, GwlStyle) & 0x0800);
            Assert.Equal(0, GetWindowLongW(textBox.Handle, GwlStyle) & 0x00100000);
            Assert.Equal(0, textChanges);
            Assert.Equal(0, selectionChanges);
            nint current = textBox.Handle;
            textBox.TextAlign = HorizontalAlignment.Center;
            Assert.Equal("value", Assert.Throws<InvalidEnumArgumentException>(
                () => textBox.TextAlign = (HorizontalAlignment)3).ParamName);
            Assert.Equal(current, textBox.Handle);
            Assert.Equal(4, trace.Count);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void AlignmentRecreationPreservesCustomAndSystemPasswordMode(bool systemPassword)
        => OnSta(() =>
        {
            using var textBox = new TextBox { PasswordChar = '#', UseSystemPasswordChar = systemPassword, Text = "secret" };
            nint original = textBox.Handle;
            nint password = SendMessageW(original, EmGetPasswordChar, 0, 0);
            Assert.NotEqual(0, password);
            textBox.Select(1, 3);
            textBox.TextAlign = HorizontalAlignment.Right;
            Assert.False(IsWindow(original));
            Assert.Equal(password, SendMessageW(textBox.Handle, EmGetPasswordChar, 0, 0));
            Assert.Equal(systemPassword, textBox.UseSystemPasswordChar);
            Assert.Equal("secret", textBox.Text);
            Assert.Equal(1, textBox.SelectionStart);
            Assert.Equal(3, textBox.SelectionLength);
        });

    [Fact]
    public void WordWrapAndLogicalAlignmentGateHorizontalScrollIndependently()
        => OnSta(() =>
        {
            using var textBox = new StyleTextBox { Multiline = true, ScrollBars = ScrollBars.Both };
            Assert.Equal(0, textBox.RequestedParameters.Style & 0x00100000);
            textBox.WordWrap = false;
            Assert.NotEqual(0, textBox.RequestedParameters.Style & 0x00100000);
            textBox.TextAlign = HorizontalAlignment.Center;
            Assert.Equal(0, textBox.RequestedParameters.Style & 0x00100000);
            textBox.TextAlign = HorizontalAlignment.Left;
            Assert.NotEqual(0, textBox.RequestedParameters.Style & 0x00100000);
            Assert.Equal(ScrollBars.Both, textBox.ScrollBars);
            Assert.False(textBox.IsHandleCreated);
        });

    private static void OnSta(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            nuint defaultContextCookie = 0;
            try
            {
                // These fixed native style expectations use the original
                // process manifest, independent of the runner's active stack.
                Assert.True(ActivateActCtx(0, out defaultContextCookie));
                action();
            }
            catch (Exception exception) { failure = exception; }
            finally
            {
                Application.ExitThread();
                typeof(Application).GetMethod("DisposeThemedCommonControlsActivation",
                    BindingFlags.Static | BindingFlags.NonPublic)!.Invoke(null, null);
                if (defaultContextCookie != 0) { Assert.True(DeactivateActCtx(0, defaultContextCookie)); }
            }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox alignment test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool ActivateActCtx(nint context, out nuint cookie);

    [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DeactivateActCtx(uint flags, nuint cookie);

    private sealed class HookTextBox : TextBox
    {
        internal int HookCalls { get; private set; }
        internal bool CallBase { get; set; } = true;
        internal bool SuppressEvents { get; set; }
        internal Action<EventArgs>? BeforeEvent { get; set; }
        internal void RaiseTextAlignChanged(EventArgs e) => OnTextAlignChanged(e);
        protected override bool CanRaiseEvents => !SuppressEvents && base.CanRaiseEvents;

        protected override void OnTextAlignChanged(EventArgs e)
        {
            HookCalls++;
            BeforeEvent?.Invoke(e);
            if (CallBase) { base.OnTextAlignChanged(e); }
        }
    }

    private sealed class StyleTextBox : TextBox
    {
        internal CreateParams RequestedParameters => CreateParams;
        protected override uint ExStyle => base.ExStyle | WsExRight;
    }

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint CreateWindowExW(uint exStyle, string className, string? windowName,
        uint style, int x, int y, int width, int height, nint parent, nint menu, nint instance, nint param);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern int GetWindowLongW(nint hWnd, int index);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern int SetWindowLongW(nint hWnd, int index, int value);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetWindowPos(nint hWnd, nint insertAfter, int x, int y, int width, int height, uint flags);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint hWnd);
}
