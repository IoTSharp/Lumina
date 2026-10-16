using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class FormInputRoutingTests
{
    [Theory]
    [InlineData("button", false, false)]
    [InlineData("checkbox", false, false)]
    [InlineData("radio", false, true)]
    [InlineData("picture", true, true)]
    public void NativeConsumersUseTheReferenceClickStyles(string kind, bool click, bool doubleClick)
        => OnSta(() =>
        {
            using Control control = CreateConsumer(kind);
            var probe = (IInputProbe)control;
            Assert.Equal(click, probe.ReadStyle(ControlStyles.StandardClick));
            Assert.Equal(doubleClick, probe.ReadStyle(ControlStyles.StandardDoubleClick));
            Assert.False(probe.ReadStyle(ControlStyles.UserPaint));
        });

    [Theory]
    [InlineData("button")]
    [InlineData("checkbox")]
    [InlineData("radio")]
    [InlineData("picture")]
    public void TypedAndControlClickSubscriptionsShareOneVirtualEventChain(string kind)
        => OnSta(() =>
        {
            using Control control = CreateConsumer(kind);
            var args = new EventArgs();
            List<string> calls = [];
            control.Click += (sender, e) =>
            {
                Assert.Same(control, sender);
                Assert.Same(args, e);
                calls.Add("control");
            };
            AddTypedClick(control, (sender, e) =>
            {
                Assert.Same(control, sender);
                Assert.Same(args, e);
                calls.Add("typed");
            });

            ((IInputProbe)control).RaiseClick(args);

            Assert.Equal(new[] { "control", "typed" }, calls);
            Assert.False(control.IsHandleCreated);
            Assert.Null(control.GetType().BaseType!.GetEvent(nameof(Control.Click),
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly));
        });

    [Theory]
    [InlineData("button")]
    [InlineData("checkbox")]
    [InlineData("radio")]
    [InlineData("picture")]
    public void SharedClickChainPropagatesTheOriginalSubscriberFailure(string kind)
        => OnSta(() =>
        {
            using Control control = CreateConsumer(kind);
            var expected = new InvalidOperationException("consumer click failure");
            int laterCalls = 0;
            control.Click += (_, _) => throw expected;
            AddTypedClick(control, (_, _) => laterCalls++);

            Exception actual = Assert.Throws<InvalidOperationException>(
                () => ((IInputProbe)control).RaiseClick(EventArgs.Empty));

            Assert.Same(expected, actual);
            Assert.Equal(0, laterCalls);
            Assert.False(control.IsHandleCreated);
        });

    [Fact]
    public void ButtonPerformClickUsesTheControlEventSlotWithoutMouseEvents()
        => OnSta(() =>
        {
            using var button = new Button();
            Control control = button;
            List<string> calls = [];
            control.Click += (_, e) =>
            {
                Assert.Same(EventArgs.Empty, e);
                calls.Add("control");
            };
            button.Click += (_, _) => calls.Add("typed");
            button.MouseDown += (_, _) => calls.Add("down");
            button.MouseClick += (_, _) => calls.Add("mouse-click");
            button.MouseUp += (_, _) => calls.Add("up");

            button.PerformClick();

            Assert.Equal(new[] { "control", "typed" }, calls);
            Assert.False(button.IsHandleCreated);
        });

    [Theory]
    [InlineData("button", false)]
    [InlineData("button", true)]
    [InlineData("checkbox", false)]
    [InlineData("checkbox", true)]
    [InlineData("radio", false)]
    [InlineData("radio", true)]
    [InlineData("picture", false)]
    [InlineData("picture", true)]
    [Trait("Category", "WindowsHandle")]
    public void ProgrammaticNativeCommandRoutesOnceThroughFormOrContainer(string kind, bool nested)
        => OnSta(() =>
        {
            using var form = new Form();
            Control parent = nested ? new Control { Size = new Size(180, 100) } : form;
            if (nested) { form.Controls.Add(parent); }
            Control control = CreateConsumer(kind);
            control.Bounds = new Rectangle(8, 9, 100, 32);
            parent.Controls.Add(control);
            nint childHandle = control.Handle;
            nint parentHandle = parent.Handle;
            List<string> calls = [];
            if (control is CheckBox checkBox)
            {
                checkBox.CheckedChanged += (_, _) => calls.Add("checked:" + checkBox.Checked);
                _ = SendMessageW(childHandle, 0x00F1, 1, 0);
            }
            else if (control is RadioButton radioButton)
            {
                radioButton.CheckedChanged += (_, _) => calls.Add("checked:" + radioButton.Checked);
                _ = SendMessageW(childHandle, 0x00F1, 1, 0);
            }
            control.Click += (sender, e) =>
            {
                Assert.Same(control, sender);
                Assert.Same(EventArgs.Empty, e);
                calls.Add("control-click");
            };
            AddTypedClick(control, (_, _) => calls.Add("typed-click"));
            control.MouseClick += (_, _) => calls.Add("mouse-click");
            control.MouseDown += (_, _) => calls.Add("down");
            control.MouseUp += (_, _) => calls.Add("up");

            // A real HWND notification is a programmatic routing check. It is
            // deliberately separate from a physical mouse click acceptance.
            _ = SendMessageW(parentHandle, 0x0111, (nint)(ushort)GetDlgCtrlID(childHandle), childHandle);

            string[] expected = kind switch
            {
                "picture" => [],
                "checkbox" or "radio" => ["checked:True", "control-click", "typed-click"],
                _ => ["control-click", "typed-click"],
            };
            Assert.Equal(expected, calls);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void PictureBoxNativeButtonDownDoesNotRaiseAnEarlyStaticControlClick()
        => OnSta(() =>
        {
            using var form = new Form();
            var picture = new PictureBox { Bounds = new Rectangle(8, 9, 100, 70) };
            form.Controls.Add(picture);
            List<string> calls = [];
            picture.Click += (_, _) => calls.Add("click");
            picture.MouseClick += (_, _) => calls.Add("mouse-click");
            picture.MouseDown += (_, e) => calls.Add($"down:{e.Button}:{e.X},{e.Y}");

            _ = SendMessageW(picture.Handle, 0x0201, 1, Pack(12, 13));

            Assert.Equal(new[] { "down:Left:12,13" }, calls);
            picture.Capture = false;
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void FormPassesMouseMessagesToTheInheritedControlEvents()
        => OnSta(() =>
        {
            using var form = new Form();
            Control control = form;
            List<string> calls = [];
            control.MouseMove += (sender, e) =>
            {
                Assert.Same(form, sender);
                calls.Add($"move:{e.X},{e.Y}");
            };
            form.MouseHover += (_, _) => calls.Add("hover");
            form.MouseLeave += (_, _) => calls.Add("leave");
            nint handle = form.Handle;

            _ = SendMessageW(handle, 0x0200, 0, Pack(-7, 11));
            _ = SendMessageW(handle, 0x02A1, 0, 0);
            _ = SendMessageW(handle, 0x02A3, 0, 0);

            Assert.Equal(new[] { "move:-7,11", "hover", "leave" }, calls);
            Assert.True(IsWindow(handle));
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void FormPassesRealCaptureReleaseToTheInheritedControlEvent()
        => OnSta(() =>
        {
            using var form = new Form();
            Control control = form;
            int changed = 0;
            control.MouseCaptureChanged += (sender, e) =>
            {
                Assert.Same(form, sender);
                Assert.Same(EventArgs.Empty, e);
                changed++;
            };

            form.Capture = true;
            Assert.True(control.Capture);
            Assert.Equal(0, changed);
            control.Capture = false;

            Assert.False(form.Capture);
            Assert.Equal(1, changed);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void MenuCaptionNativeReleaseUsesTheSharedClickSlotExactlyOnce()
        => OnSta(() =>
        {
            using var form = new Form();
            var menu = new MenuStrip();
            var item = new ToolStripButton { Text = "Run" };
            menu.Items.Add(item);
            form.Controls.Add(menu);
            Control host = Assert.Single(menu.Controls.Cast<Control>());
            List<string> calls = [];
            item.Click += (_, _) => calls.Add("item");
            host.Click += (sender, e) =>
            {
                Assert.Same(host, sender);
                Assert.Same(EventArgs.Empty, e);
                calls.Add("host");
            };
            nint handle = host.Handle;

            _ = SendMessageW(handle, 0x0201, 1, Pack(3, 4));
            Assert.Empty(calls);
            _ = SendMessageW(handle, 0x0202, 0, Pack(3, 4));

            Assert.Equal(new[] { "item", "host" }, calls);
            Assert.Null(host.GetType().GetEvent(nameof(Control.Click),
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            host.Capture = false;
        });

    private static Control CreateConsumer(string kind) => kind switch
    {
        "button" => new InputButton(),
        "checkbox" => new InputCheckBox(),
        "radio" => new InputRadioButton(),
        "picture" => new InputPictureBox(),
        _ => throw new ArgumentOutOfRangeException(nameof(kind)),
    };

    private static void AddTypedClick(Control control, EventHandler handler)
    {
        switch (control)
        {
            case Button button: button.Click += handler; break;
            case CheckBox checkBox: checkBox.Click += handler; break;
            case RadioButton radioButton: radioButton.Click += handler; break;
            case PictureBox picture: picture.Click += handler; break;
            default: throw new ArgumentException("Unsupported input consumer.", nameof(control));
        }
    }

    private static nint Pack(int x, int y) => unchecked((nint)((uint)(ushort)x | ((uint)(ushort)y << 16)));

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
        Assert.True(thread.Join(TimeSpan.FromSeconds(15)), "The native input consumer check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private interface IInputProbe
    {
        bool ReadStyle(ControlStyles style);
        void RaiseClick(EventArgs args);
    }

    private sealed class InputButton : Button, IInputProbe
    {
        public bool ReadStyle(ControlStyles style) => GetStyle(style);
        public void RaiseClick(EventArgs args) => OnClick(args);
    }

    private sealed class InputCheckBox : CheckBox, IInputProbe
    {
        public bool ReadStyle(ControlStyles style) => GetStyle(style);
        public void RaiseClick(EventArgs args) => OnClick(args);
    }

    private sealed class InputRadioButton : RadioButton, IInputProbe
    {
        public bool ReadStyle(ControlStyles style) => GetStyle(style);
        public void RaiseClick(EventArgs args) => OnClick(args);
    }

    private sealed class InputPictureBox : PictureBox, IInputProbe
    {
        public bool ReadStyle(ControlStyles style) => GetStyle(style);
        public void RaiseClick(EventArgs args) => OnClick(args);
    }

    [DllImport("user32.dll")]
    private static extern nint SendMessageW(nint handle, uint message, nint wParam, nint lParam);

    [DllImport("user32.dll")]
    private static extern int GetDlgCtrlID(nint handle);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint handle);
}
