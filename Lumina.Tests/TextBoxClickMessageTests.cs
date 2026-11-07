using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;
using Xunit.Abstractions;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxClickMessageTests
{
    private readonly ITestOutputHelper _output;

    public TextBoxClickMessageTests(ITestOutputHelper output) => _output = output;

    private const int LeftDown = 0x0201;
    private const int LeftUp = 0x0202;
    private const int LeftDouble = 0x0203;
    private const int RightUp = 0x0205;
    private const int RightDouble = 0x0206;

    [Theory]
    [InlineData(false, false)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(true, true)]
    public void ConstructorsDisableNativeEditClickStylesWithoutCreatingAHandle(bool multiline, bool readOnly)
        => OnSta(() =>
        {
            using var editor = new ClickTextBox(multiline, readOnly);
            Assert.False(editor.StyleEnabled(ControlStyles.StandardClick));
            Assert.False(editor.StyleEnabled(ControlStyles.StandardDoubleClick));
            Assert.False(editor.StyleEnabled(ControlStyles.UseTextForAccessibility));
            Assert.False(editor.StyleEnabled(ControlStyles.UserPaint));
            Assert.True(editor.StyleEnabled(ControlStyles.Selectable));
            Assert.Equal(!multiline, editor.StyleEnabled(ControlStyles.FixedHeight));
            Assert.False(editor.IsHandleCreated);
        });

    [Fact]
    public void ClickIsDeclaredVisibleAndBothReferenceTypesShareOneEventSlot()
        => OnSta(() =>
        {
            EventInfo click = typeof(TextBoxBase).GetEvent(nameof(Control.Click),
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)!;
            Assert.NotNull(click);
            Assert.True(click.GetCustomAttribute<BrowsableAttribute>()!.Browsable);
            Assert.Equal(EditorBrowsableState.Always,
                click.GetCustomAttribute<EditorBrowsableAttribute>()!.State);

            using var editor = new ClickTextBox();
            TextBoxBase editReference = editor;
            Control controlReference = editor;
            List<string> calls = [];
            var args = new MouseEventArgs(MouseButtons.Left, 1, -9, 17, 0);
            EventHandler first = (sender, observed) =>
            {
                Assert.Same(editor, sender);
                Assert.Same(args, observed);
                calls.Add("first");
            };
            EventHandler second = (_, observed) => { Assert.Same(args, observed); calls.Add("second"); };
            editReference.Click += first;
            controlReference.Click += second;
            editor.RaiseClick(args);
            Assert.Equal(new[] { "first", "second" }, calls);
            calls.Clear();
            controlReference.Click -= first;
            editor.RaiseClick(args);
            Assert.Equal(new[] { "second" }, calls);
            calls.Clear();
            editReference.Click -= second;
            editor.RaiseClick(args);
            Assert.Empty(calls);
            Assert.False(editor.IsHandleCreated);
        });

    [Fact]
    public void ClickUsesComponentEventSuppressionAndPreservesTheOriginalException()
        => OnSta(() =>
        {
            using var editor = new ClickTextBox();
            var failure = new InvalidOperationException("click failure");
            editor.Click += (_, _) => throw failure;
            editor.AllowEvents = false;
            editor.RaiseClick(EventArgs.Empty);
            editor.AllowEvents = true;
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(
                () => editor.RaiseClick(EventArgs.Empty)));
            Assert.False(editor.IsHandleCreated);
        });

    [Theory]
    [InlineData(MouseButtons.None)]
    [InlineData(MouseButtons.Right)]
    [InlineData(MouseButtons.Middle)]
    [InlineData(MouseButtons.XButton1)]
    [InlineData(MouseButtons.XButton2)]
    public void DirectNonLeftMouseUpOnlyForwardsTheOriginalArguments(MouseButtons button)
        => OnSta(() =>
        {
            using var editor = new ClickTextBox();
            var args = new MouseEventArgs(button, 7, -13, 29, -120);
            List<string> calls = [];
            ObserveClicks(editor, calls, args);
            editor.RaiseUp(args);
            Assert.Equal(new[] { "up" }, calls);
            Assert.False(editor.IsHandleCreated);
        });

    [Fact]
    public void DirectNullMouseUpForwardsNullWithoutCreatingAHandle()
        => OnSta(() =>
        {
            using var editor = new ClickTextBox();
            int calls = 0;
            editor.MouseUp += (sender, args) =>
            {
                Assert.Same(editor, sender);
                Assert.Null(args);
                calls++;
            };
            editor.RaiseUp(null!);
            Assert.Equal(1, calls);
            Assert.False(editor.IsHandleCreated);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void CancelledValidationShortCircuitsHitTestingAndForwardsMouseUp(bool cancelledByParent)
        => OnSta(() =>
        {
            using var parent = new Control();
            using var editor = new ClickTextBox();
            if (cancelledByParent) { parent.Controls.Add(editor); }
            SetValidationCancelled(cancelledByParent ? parent : editor, true);
            List<string> calls = [];
            var args = new MouseEventArgs(MouseButtons.Left, 4, 9, 11, 0);
            ObserveClicks(editor, calls, args);
            editor.RaiseUp(args);
            Assert.Equal(new[] { "up" }, calls);
            Assert.False(editor.IsHandleCreated);
            Assert.False(parent.IsHandleCreated);
        });

    [Theory]
    [InlineData(LeftDouble, true)]
    [InlineData(RightDouble, false)]
    public void OnlyLeftDoubleMessageArmsTheTextBoxFlagBeforeNativeDefaultProcessing(int message, bool expected)
        => OnSta(() =>
        {
            using var editor = new ClickTextBox();
            bool? duringDefault = null;
            bool? duringMouseDown = null;
            editor.MouseDefault = msg =>
            {
                if (msg == message) { duringDefault = PendingDouble(editor); }
            };
            editor.MouseDown += (_, args) =>
            {
                Assert.Equal(2, args.Clicks);
                duringMouseDown = PendingDouble(editor);
            };
            editor.Call(message, Pack(-13000, -14000));
            Assert.Equal(expected, duringDefault);
            Assert.Equal(expected, duringMouseDown);
            Assert.Equal(expected, PendingDouble(editor));
            Assert.True(editor.Capture);
            editor.Capture = false;
        });

    [Fact]
    public void NonLeftAndNullDirectMouseUpKeepThePendingLeftDoubleUntilALeftUp()
        => OnSta(() =>
        {
            using var editor = new ClickTextBox();
            editor.Call(LeftDouble, Pack(-13000, -14000));
            Assert.True(PendingDouble(editor));
            editor.RaiseUp(new MouseEventArgs(MouseButtons.Right, 1, -13000, -14000, 0));
            Assert.True(PendingDouble(editor));
            editor.RaiseUp(null!);
            Assert.True(PendingDouble(editor));
            SetValidationCancelled(editor, true);
            editor.RaiseUp(new MouseEventArgs(MouseButtons.Left, 1, 9, 11, 0));
            Assert.False(PendingDouble(editor));
            // Direct OnMouseUp is not the Control message finally; it does
            // not release native capture or clear validation state itself.
            Assert.True(editor.Capture);
            Assert.True(IsValidationCancelled(editor));
            editor.Capture = false;
        });

    [Fact]
    public void LeftMouseUpOutsideTheEditClearsThePendingDoubleBeforeTheMouseUpEvent()
        => OnSta(() =>
        {
            using var editor = new ClickTextBox();
            var args = new MouseEventArgs(MouseButtons.Left, 1, -13000, -14000, 0);
            editor.Call(LeftDouble, Pack(args.X, args.Y));
            Point screen = editor.PointToScreen(args.Location);
            Assert.NotEqual(editor.Handle, WindowFromPoint(screen));
            List<string> calls = [];
            ObserveClicks(editor, calls, args);
            editor.MouseUp += (_, _) => Assert.False(PendingDouble(editor));
            editor.RaiseUp(args);
            Assert.Equal(new[] { "up" }, calls);
            Assert.False(PendingDouble(editor));
            editor.Capture = false;
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void DirectLeftUpUsesIndependentNativeHitAndPreservesTheSameArguments(bool doubleClick)
        => OnSta(() =>
        {
            using var host = new Form { Bounds = new Rectangle(180, 180, 340, 200) };
            using var editor = new ClickTextBox { Bounds = new Rectangle(12, 12, 180, 32) };
            host.Controls.Add(editor);
            host.Show();
            if (doubleClick) { editor.Call(LeftDouble, Pack(9, 8)); }
            var args = new MouseEventArgs(MouseButtons.Left, 5, 9, 8, -120);
            Point screen = editor.PointToScreen(args.Location);
            bool hit = WindowFromPoint(screen) == editor.Handle;
            _output.WriteLine($"Independent WindowFromPoint hit={hit}; doubleClick={doubleClick}; "
                + $"expected branch={(hit ? doubleClick ? "double-click" : "click" : "no-hit")}. "
                + "A no-hit result covers only the negative branch, not positive click acceptance.");
            List<string> calls = [];
            ObserveClicks(editor, calls, args);
            editor.RaiseUp(args);
            string[] expected = hit
                ? doubleClick ? ["double", "mouse-double", "up"] : ["click", "mouse-click", "up"]
                : ["up"];
            Assert.Equal(expected, calls);
            Assert.False(PendingDouble(editor));
            editor.Capture = false;
        });

    [Theory]
    [InlineData(LeftDown)]
    [InlineData(LeftDouble)]
    public void MouseUpMessageFailureStillClearsCaptureValidationAndPendingState(int down)
        => OnSta(() =>
        {
            using var editor = new ClickTextBox();
            var failure = new InvalidOperationException("mouse up failure");
            editor.Call(down, Pack(-13000, -14000));
            SetValidationCancelled(editor, true);
            editor.MouseUp += (_, _) => throw failure;
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(
                () => editor.Call(LeftUp, Pack(-13000, -14000))));
            Assert.False(editor.Capture);
            Assert.False(PendingDouble(editor));
            Assert.False(IsValidationCancelled(editor));
        });

    [Fact]
    public void DefaultMouseUpFailureLeavesTextBoxDoubleStateButControlFinallyReleasesCapture()
        => OnSta(() =>
        {
            using var editor = new ClickTextBox();
            var failure = new InvalidOperationException("native default failure");
            editor.Call(LeftDouble, Pack(-13000, -14000));
            SetValidationCancelled(editor, true);
            editor.MouseDefault = message => { if (message == LeftUp) { throw failure; } };
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(
                () => editor.Call(LeftUp, Pack(-13000, -14000))));
            Assert.False(editor.Capture);
            Assert.False(IsValidationCancelled(editor));
            Assert.True(PendingDouble(editor));
            editor.MouseDefault = null;
            SetValidationCancelled(editor, true);
            editor.RaiseUp(new MouseEventArgs(MouseButtons.Left, 1, 9, 11, 0));
            Assert.False(PendingDouble(editor));
        });

    private static void ObserveClicks(ClickTextBox editor, List<string> calls, MouseEventArgs? expected)
    {
        void Observe(string name, object? sender, EventArgs? args)
        {
            Assert.Same(editor, sender);
            Assert.Same(expected, args);
            calls.Add(name);
        }
        editor.Click += (sender, args) => Observe("click", sender, args);
        editor.MouseClick += (sender, args) => Observe("mouse-click", sender, args);
        editor.DoubleClick += (sender, args) => Observe("double", sender, args);
        editor.MouseDoubleClick += (sender, args) => Observe("mouse-double", sender, args);
        editor.MouseUp += (sender, args) => Observe("up", sender, args);
    }

    private static bool PendingDouble(TextBoxBase editor)
        => (bool)typeof(TextBoxBase).GetField("_doubleClickFired",
            BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(editor)!;

    private static void SetValidationCancelled(Control control, bool value)
        => typeof(Control).GetProperty("ValidationCancelled",
            BindingFlags.Instance | BindingFlags.NonPublic)!.SetValue(control, value);

    private static bool IsValidationCancelled(Control control)
        => (bool)typeof(Control).GetProperty("ValidationCancelled",
            BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(control)!;

    private static nint Pack(int x, int y)
        => unchecked((nint)((uint)(ushort)x | ((uint)(ushort)y << 16)));

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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox click message check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class ClickTextBox : TextBox
    {
        internal ClickTextBox(bool multiline = false, bool readOnly = false) : base(multiline, readOnly) { }
        internal bool AllowEvents { get; set; } = true;
        internal Action<int>? MouseDefault { get; set; }
        internal bool StyleEnabled(ControlStyles style) => GetStyle(style);
        internal void RaiseClick(EventArgs args) => OnClick(args);
        internal void RaiseUp(MouseEventArgs args) => OnMouseUp(args);
        internal void Call(int message, nint lParam)
        {
            var value = Message.Create(IsHandleCreated ? Handle : 0, message, 0, lParam);
            base.WndProc(ref value);
        }
        protected override bool CanRaiseEvents => AllowEvents;
        protected override void DefWndProc(ref Message message)
        {
            if (message.Msg is LeftDown or LeftDouble or LeftUp or RightUp or RightDouble)
            {
                MouseDefault?.Invoke(message.Msg);
                return;
            }
            base.DefWndProc(ref message);
        }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint WindowFromPoint(Point point);
}
