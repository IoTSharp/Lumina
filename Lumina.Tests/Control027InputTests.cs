using System.Drawing;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class Control027InputTests
{
    [Fact]
    public void MouseArgumentsPreserveRawValuesAndHandledState()
    {
        var args = new HandledMouseEventArgs((MouseButtons)(-1), -2, int.MaxValue, int.MinValue, -240, true);
        Assert.Equal((MouseButtons)(-1), args.Button);
        Assert.Equal(-2, args.Clicks);
        Assert.Equal(new Point(int.MaxValue, int.MinValue), args.Location);
        Assert.Equal(-240, args.Delta);
        Assert.True(args.Handled);
        args.Handled = false;
        Assert.False(args.Handled);
        Assert.False(new HandledMouseEventArgs(MouseButtons.XButton1, 1, 2, 3, 120).Handled);
        Assert.Equal(0x00800000, (int)MouseButtons.XButton1);
        Assert.Equal(0x01000000, (int)MouseButtons.XButton2);
        Assert.False(typeof(MouseEventArgs).IsSealed);
    }

    [Fact]
    public void DirectEventsPreserveSenderArgumentsRemovalAndExceptionsWithoutCreatingHandles()
        => OnSta(() =>
        {
            using var control = new InputControl();
            var args = new HandledMouseEventArgs(MouseButtons.XButton2, 2, -7, 9, -120, true);
            object? sender = null;
            MouseEventArgs? observed = null;
            MouseEventHandler handler = (s, e) => { sender = s; observed = e; };
            control.MouseDown += handler;
            control.RaiseDown(args);
            Assert.Same(control, sender);
            Assert.Same(args, observed);
            control.RaiseDown(null!);
            Assert.Null(observed);
            control.MouseDown -= handler;
            observed = args;
            control.RaiseDown(null!);
            Assert.Same(args, observed);

            var failure = new InvalidOperationException("original");
            control.MouseDown += (_, _) => throw failure;
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => control.RaiseDown(args)));
            Assert.False(control.IsHandleCreated);
        });

    [Fact]
    public void StandardHelpersUseTargetVirtualClickAndEventListWithoutNativeState()
        => OnSta(() =>
        {
            using var caller = new InputControl();
            using var target = new InputControl();
            var args = new MouseEventArgs(MouseButtons.Right, 2, -1, 4, 0);
            object key = new();
            object? sender = null;
            EventArgs? observed = null;
            target.Click += (s, e) => { sender = s; observed = e; };
            caller.InvokeClick(null, args);
            Assert.Null(sender);
            caller.InvokeClick(target, args);
            Assert.Same(target, sender);
            Assert.Same(args, observed);
            Assert.Equal(1, target.ClickOverrides);
            caller.AddMouseHandler(key, (s, e) => { sender = s; observed = e; });
            caller.RaiseKey(key, args);
            Assert.Same(caller, sender);
            Assert.Same(args, observed);
            caller.ResetTracking();
            Assert.False(caller.IsHandleCreated);
            Assert.False(target.IsHandleCreated);
        });

    [Fact]
    public void CoordinateConversionsCreateAHandleAndKeepRawNativeGeometry()
        => OnSta(() =>
        {
            using var host = new Form { Location = new Point(150, 130), Size = new Size(400, 300) };
            using var parent = new Control { Location = new Point(19, 23), Size = new Size(250, 180) };
            using var child = new Control { Location = new Point(7, 11), Size = new Size(80, 60) };
            host.Controls.Add(parent);
            parent.Controls.Add(child);
            _ = host.Handle;
            _ = parent.Handle;
            Assert.False(child.IsHandleCreated);
            _ = child.PointToScreen(Point.Empty);
            Assert.True(child.IsHandleCreated);

            foreach (Point point in new[] { Point.Empty, new Point(-7, 11), new Point(int.MaxValue, int.MinValue) })
            {
                Point screen = point;
                MapWindowPoints(child.Handle, 0, ref screen, 1);
                Assert.Equal(screen, child.PointToScreen(point));
                Assert.Equal(point, child.PointToClient(screen));
            }
            foreach (Rectangle rectangle in new[] { Rectangle.Empty, new Rectangle(5, 6, -7, -8), new Rectangle(int.MaxValue, int.MinValue, 3, -5) })
            {
                NativeRectangle native = new(rectangle);
                MapWindowPoints(child.Handle, 0, ref native, 2);
                Rectangle screen = child.RectangleToScreen(rectangle);
                Assert.Equal(native.Rectangle, screen);
                Assert.Equal(rectangle, child.RectangleToClient(screen));
            }
        });

    [Fact]
    public void CaptureLazinessTransferAndNonOwnerReleaseFollowNativeOwnership()
        => OnSta(() =>
        {
            using var first = new Control();
            using var second = new Control();
            var trace = new List<string>();
            first.MouseCaptureChanged += (_, _) => trace.Add("first:" + first.Capture);
            second.MouseCaptureChanged += (_, _) => trace.Add("second:" + second.Capture);
            first.Capture = false;
            Assert.False(first.IsHandleCreated);
            first.Capture = true;
            Assert.True(first.IsHandleCreated);
            Assert.True(first.Capture);
            Assert.Empty(trace);
            second.Capture = true;
            Assert.False(first.Capture);
            Assert.True(second.Capture);
            Assert.Equal(new[] { "first:False" }, trace);
            first.Capture = false;
            Assert.True(second.Capture);
            second.Capture = false;
            Assert.Equal(new[] { "first:False", "second:False" }, trace);
            Assert.Equal((nint)0, GetCapture());
        });

    [Theory]
    [InlineData(0x0201, 0x0202, 0, MouseButtons.Left, 1)]
    [InlineData(0x0204, 0x0205, 0, MouseButtons.Right, 1)]
    [InlineData(0x0207, 0x0208, 0, MouseButtons.Middle, 1)]
    [InlineData(0x020B, 0x020C, 1 << 16, MouseButtons.XButton1, 1)]
    [InlineData(0x020B, 0x020C, 2 << 16, MouseButtons.XButton2, 1)]
    [InlineData(0x0203, 0x0202, 0, MouseButtons.Left, 2)]
    [InlineData(0x0206, 0x0205, 0, MouseButtons.Right, 2)]
    [InlineData(0x0209, 0x0208, 0, MouseButtons.Middle, 2)]
    [InlineData(0x020D, 0x020C, 1 << 16, MouseButtons.XButton1, 2)]
    [InlineData(0x020D, 0x020C, 2 << 16, MouseButtons.XButton2, 2)]
    public void ButtonMessagesPreserveSignedCoordinatesCaptureAndClickCounts(int down, int up, int flags, MouseButtons button, int clicks)
        => OnSta(() =>
        {
            using var control = new InputControl { Size = new Size(100, 80) };
            MouseEventArgs? downArgs = null;
            MouseEventArgs? upArgs = null;
            control.MouseDown += (_, e) => { Assert.True(control.Capture); downArgs = e; };
            control.MouseUp += (_, e) => { Assert.True(control.Capture); upArgs = e; };
            control.Call(down, flags, Pack(-7, -9));
            control.Call(up, flags, Pack(-7, -9));
            Assert.Equal(button, downArgs!.Button);
            Assert.Equal(clicks, downArgs.Clicks);
            Assert.Equal(new Point(-7, -9), downArgs.Location);
            Assert.Equal(button, upArgs!.Button);
            Assert.Equal(1, upArgs.Clicks);
            Assert.False(control.Capture);
        });

    [Fact]
    public void MouseUpFailureStillReleasesCaptureAndClearsPendingClick()
        => OnSta(() =>
        {
            using var control = new InputControl { Size = new Size(100, 80) };
            var failure = new InvalidOperationException("up failure");
            MouseEventHandler handler = (_, _) => throw failure;
            control.MouseUp += handler;
            control.Call(0x0201, 0, Pack(-7, -9));
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => control.Call(0x0202, 0, Pack(-7, -9))));
            Assert.False(control.Capture);
            control.MouseUp -= handler;
            control.Call(0x0202, 0, Pack(-7, -9));
            Assert.False(control.Capture);
        });

    [Fact]
    public void MouseUpRecreatesAnUndisposedHandleDestroyedByNativeDefaultProcessing()
        => OnSta(() =>
        {
            using var control = new InputControl { Size = new Size(100, 80), DestroyOnMouseUp = true };
            int created = 0;
            int destroyed = 0;
            bool hadHandleDuringMouseUp = false;
            control.HandleCreated += (_, _) => created++;
            control.HandleDestroyed += (_, _) => destroyed++;
            control.MouseUp += (_, _) => hadHandleDuringMouseUp = control.IsHandleCreated;
            control.Call(0x0201, 0, Pack(-7, -9));
            control.Call(0x0202, 0, Pack(-7, -9));
            // Standard click hit testing reads Handle after DefWndProc. This
            // may recreate an undisposed control even when no click hits it.
            Assert.Equal(2, created);
            Assert.Equal(1, destroyed);
            Assert.True(hadHandleDuringMouseUp);
            Assert.True(control.IsHandleCreated);
            Assert.False(control.IsDisposed);
            Assert.False(control.Capture);
        });

    [Fact]
    public void WheelUsesClientCoordinatesSignedDeltaAndHandledDefaultProcessing()
        => OnSta(() =>
        {
            using var control = new InputControl { Size = new Size(100, 80), CountWheelDefault = true };
            Point screen = control.PointToScreen(new Point(8, 9));
            HandledMouseEventArgs? observed = null;
            control.MouseWheel += (_, e) => observed = Assert.IsType<HandledMouseEventArgs>(e);
            control.Call(0x020A, unchecked(-240 << 16) | 1, Pack(screen.X, screen.Y));
            Assert.Equal(MouseButtons.None, observed!.Button);
            Assert.Equal(0, observed.Clicks);
            Assert.Equal(new Point(8, 9), observed.Location);
            Assert.Equal(-240, observed.Delta);
            Assert.Equal(1, control.WheelDefaultCount);
            control.MouseWheel += (_, e) => ((HandledMouseEventArgs)e).Handled = true;
            Assert.Equal((nint)1, control.Call(0x020A, 120 << 16, Pack(screen.X, screen.Y)));
            Assert.Equal(1, control.WheelDefaultCount);
        });

    [Fact]
    public void MoveUsesNativeButtonStateInsteadOfMessageFlags()
        => OnSta(() =>
        {
            using var control = new InputControl();
            MouseEventArgs? observed = null;
            control.MouseMove += (_, e) => observed = e;
            MouseButtons expected = Control.MouseButtons;
            control.Call(0x0200, 1 | 2 | 16 | 32 | 64, Pack(-7, 9));
            Assert.Equal(expected, observed!.Button);
            Assert.Equal(0, observed.Clicks);
            Assert.Equal(new Point(-7, 9), observed.Location);
        });

    [Fact]
    public void NativeTrackingRunsOutsideVirtualWndProcAndDirectLeaveDoesNotResetIt()
        => OnSta(() =>
        {
            using var control = new InputControl { Size = new Size(100, 80) };
            int entered = 0;
            int moved = 0;
            control.MouseEnter += (_, _) => entered++;
            control.MouseMove += (_, _) => moved++;
            control.Call(0x0200, 0, Pack(8, 9));
            Assert.Equal(0, entered);
            Assert.Equal(1, moved);
            control.ConsumeMove = true;
            SendMessageW(control.Handle, 0x0200, 0, Pack(8, 9));
            Assert.Equal(1, entered);
            Assert.Equal(1, moved);
            control.Call(0x02A3, 0, 0);
            SendMessageW(control.Handle, 0x0200, 0, Pack(9, 10));
            Assert.Equal(1, entered);
            SendMessageW(control.Handle, 0x02A3, 0, 0);
            SendMessageW(control.Handle, 0x0200, 0, Pack(10, 11));
            Assert.Equal(2, entered);
        });

    [Fact]
    public void InjectedCaptureChangedDoesNotChangeNativeCapture()
        => OnSta(() =>
        {
            using var control = new InputControl();
            int notifications = 0;
            control.Capture = true;
            control.MouseCaptureChanged += (_, _) => notifications++;
            control.Call(0x0215, 0, 0);
            Assert.Equal(1, notifications);
            Assert.True(control.Capture);
            control.Capture = false;
            Assert.Equal(2, notifications);
        });

    private sealed class InputControl : Control
    {
        internal bool CountWheelDefault;
        internal int WheelDefaultCount;
        internal bool ConsumeMove;
        internal bool DestroyOnMouseUp;
        internal int ClickOverrides;
        internal void RaiseDown(MouseEventArgs e) => OnMouseDown(e);
        internal void InvokeClick(Control? target, EventArgs e) => InvokeOnClick(target, e);
        internal void AddMouseHandler(object key, MouseEventHandler handler) => Events.AddHandler(key, handler);
        internal void RaiseKey(object key, MouseEventArgs e) => RaiseMouseEvent(key, e);
        internal void ResetTracking() => ResetMouseEventArgs();
        internal nint Call(int message, nint wParam, nint lParam)
        {
            Message value = Message.Create(Handle, message, wParam, lParam);
            WndProc(ref value);
            return value.Result;
        }
        protected override void OnClick(EventArgs e) { ClickOverrides++; base.OnClick(e); }
        protected override bool HandleWindowMessage(uint message, nint wParam, nint lParam, out nint result)
        {
            if (ConsumeMove && message == 0x0200) { result = 0; return true; }
            return base.HandleWindowMessage(message, wParam, lParam, out result);
        }
        protected override void DefWndProc(ref Message m)
        {
            if (DestroyOnMouseUp && m.Msg == 0x0202)
            {
                DestroyOnMouseUp = false;
                DestroyHandle();
                m.Result = 0;
                return;
            }
            if (CountWheelDefault && m.Msg == 0x020A) { WheelDefaultCount++; m.Result = 0; return; }
            base.DefWndProc(ref m);
        }
    }

    private static nint Pack(int x, int y) => unchecked((nint)((x & 0xffff) | ((y & 0xffff) << 16)));

    [StructLayout(LayoutKind.Sequential)]
    private struct NativeRectangle(Rectangle rectangle)
    {
        public int Left = rectangle.Left;
        public int Top = rectangle.Top;
        public int Right = rectangle.Right;
        public int Bottom = rectangle.Bottom;
        internal readonly Rectangle Rectangle => System.Drawing.Rectangle.FromLTRB(Left, Top, Right, Bottom);
    }

    private static void OnSta(Action action)
    {
        if (!OperatingSystem.IsWindows()) { return; }
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            try { action(); }
            catch (Exception exception) { failure = exception; }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(20)), "The input test STA did not complete.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    [DllImport("user32.dll")]
    private static extern int MapWindowPoints(nint from, nint to, ref Point point, uint count);
    [DllImport("user32.dll")]
    private static extern int MapWindowPoints(nint from, nint to, ref NativeRectangle rectangle, uint count);
    [DllImport("user32.dll")]
    private static extern nint GetCapture();
    [DllImport("user32.dll")]
    private static extern nint SendMessageW(nint hwnd, int message, nint wParam, nint lParam);
}
