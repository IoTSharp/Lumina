using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class ControlMessagePaintTests
{
    [Fact]
    public void StandardMessageAndPaintSurfaceHasTheExpectedVirtualSlots()
    {
        const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        foreach ((string name, Type[] parameters) in new[]
        {
            ("WndProc", new[] { typeof(Message).MakeByRefType() }),
            ("DefWndProc", new[] { typeof(Message).MakeByRefType() }),
            ("OnPaint", new[] { typeof(PaintEventArgs) }),
            ("OnPaintBackground", new[] { typeof(PaintEventArgs) }),
            ("OnInvalidated", new[] { typeof(InvalidateEventArgs) }),
            ("OnStyleChanged", new[] { typeof(EventArgs) }),
        })
        {
            MethodInfo method = typeof(Control).GetMethod(name, flags, parameters)!;
            Assert.True(method.IsFamily);
            Assert.True(method.IsVirtual);
        }

        PropertyInfo createParams = typeof(Control).GetProperty("CreateParams", flags)!;
        PropertyInfo doubleBuffered = typeof(Control).GetProperty("DoubleBuffered", flags)!;
        Assert.True(createParams.GetMethod!.IsFamily && createParams.GetMethod.IsVirtual);
        Assert.True(doubleBuffered.GetMethod!.IsFamily && doubleBuffered.GetMethod.IsVirtual);
        Assert.True(doubleBuffered.SetMethod!.IsFamily && doubleBuffered.SetMethod.IsVirtual);
        Assert.Equal(typeof(PaintEventHandler), typeof(Control).GetEvent(nameof(Control.Paint))!.EventHandlerType);
        Assert.Equal(typeof(InvalidateEventHandler), typeof(Control).GetEvent(nameof(Control.Invalidated))!.EventHandlerType);
    }

    [Fact]
    public void StylesKeepWinFormsDefaultsAndUnknownBitsAreStored()
        => OnSta(() =>
        {
            using var control = new ExposedControl();
            Assert.True(control.ReadStyle(ControlStyles.UserPaint));
            Assert.True(control.ReadStyle(ControlStyles.AllPaintingInWmPaint));
            Assert.True(control.ReadStyle(ControlStyles.StandardClick));
            Assert.True(control.ReadStyle(ControlStyles.StandardDoubleClick));
            Assert.True(control.ReadStyle(ControlStyles.Selectable));
            Assert.False(control.ReadStyle(ControlStyles.Opaque));
            ControlStyles unknown = unchecked((ControlStyles)0x80000000);
            control.WriteStyle(unknown, true);
            Assert.True(control.ReadStyle(unknown));
            Assert.False(control.ReadStyle(ControlStyles.UserPaint | ControlStyles.Opaque));
            control.WriteStyle(ControlStyles.UserPaint | ControlStyles.Opaque, true);
            Assert.True(control.ReadStyle(ControlStyles.UserPaint | ControlStyles.Opaque));
            Assert.False(control.ReadStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw));
        });

    [Fact]
    public void WndProcOverrideCanHandleAndRewriteMessagesWhileDefaultRoutesToNativeWindow()
        => OnSta(() =>
        {
            using var control = new ExposedControl();
            Message custom = Message.Create(0, 0x8000 + 123, 12, 34);
            control.CallWindow(ref custom);
            Assert.Equal((nint)2468, custom.Result);
            Assert.Equal(new[] { "enter:12,34", "handled" }, control.Trace);

            control.Rewrite = true;
            Message rewritten = Message.Create(0, 0x8000 + 123, 0, 0);
            control.CallWindow(ref rewritten);
            Assert.Equal((nint)0, rewritten.Result);
            Assert.Equal("rewrite:0", control.Trace[^1]);
        });

    [Fact]
    public void DirectPaintUsesBorrowedGraphicsAndPreservesCallerOwnership()
        => OnSta(() =>
        {
            using var control = new ExposedControl { PaintColor = Color.Blue };
            using var bitmap = new Bitmap(40, 32);
            using Graphics graphics = Graphics.FromImage(bitmap);
            using var args = new PaintEventArgs(graphics, new Rectangle(2, 3, 20, 15));
            control.RaiseBackground(args);
            control.RaisePaint(args);
            Assert.Equal(Color.Blue.ToArgb(), bitmap.GetPixel(4, 5).ToArgb());
            args.Dispose();
            graphics.Clear(Color.Red);
            Assert.Equal(Color.Red.ToArgb(), bitmap.GetPixel(1, 1).ToArgb());
        });

    [Fact]
    public void InvalidateAndRefreshDoNotMaterializeAnUncreatedWindow()
        => OnSta(() =>
        {
            using var control = new ExposedControl();
            int invalidated = 0;
            control.Invalidated += (_, _) => invalidated++;
            control.Invalidate();
            control.Invalidate(new Rectangle(1, 2, 3, 4));
            control.Refresh();
            Assert.False(control.IsHandleCreated);
            Assert.Equal(0, invalidated);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void TransparentInvalidationAllowsAChildToRemoveOrDisposeItself(bool dispose)
        => OnSta(() =>
        {
            using var parent = new ExposedControl { Size = new Size(100, 80) };
            using var first = new ExposedControl { Size = new Size(20, 20) };
            using var second = new ExposedControl { Bounds = new Rectangle(30, 0, 20, 20) };
            first.WriteStyle(ControlStyles.SupportsTransparentBackColor, true);
            second.WriteStyle(ControlStyles.SupportsTransparentBackColor, true);
            first.BackColor = Color.Transparent;
            second.BackColor = Color.Transparent;
            parent.Controls.AddRange(first, second);
            _ = parent.Handle;
            _ = first.Handle;
            _ = second.Handle;
            List<string> calls = [];
            InvalidateEventHandler? firstHandler = null;
            firstHandler = (_, _) =>
            {
                calls.Add("first");
                first.Invalidated -= firstHandler;
                if (dispose) { first.Dispose(); }
                else { parent.Controls.Remove(first); }
            };
            first.Invalidated += firstHandler;
            second.Invalidated += (_, _) => calls.Add("second");
            parent.Invalidated += (_, _) => calls.Add("parent");

            parent.Invalidate();

            // Removing index zero shifts the second child before the next
            // live-count check, matching the reference's indexed traversal.
            Assert.Equal(new[] { "first", "parent" }, calls);
            Assert.Equal(1, parent.Controls.Count);
            Assert.Same(second, parent.Controls[0]);
            Assert.Null(first.Parent);
            Assert.Equal(dispose, first.IsDisposed);
        });

    [Fact]
    public void PublicOnPrintRetainsBackgroundChangesAndBorrowsTheCallersGraphics()
        => OnSta(() =>
        {
            using var control = new PrintControl { Size = new Size(40, 32), ChangeBackgroundState = true };
            using var bitmap = new Bitmap(40, 32);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.TranslateTransform(2, 3);
            using PaintEventArgs args = new(graphics, new Rectangle(0, 0, 40, 32));

            control.RaisePrint(args);

            Assert.Equal(new PointF(7, 9), control.ForegroundOffset);
            Assert.False(control.ForegroundContainsPixel);
            using (Matrix transform = graphics.Transform)
            {
                Assert.Equal(7, transform.OffsetX);
                Assert.Equal(9, transform.OffsetY);
            }
            args.Dispose();
            graphics.ResetTransform();
            graphics.ResetClip();
            graphics.Clear(Color.Green);
            Assert.Equal(Color.Green.ToArgb(), bitmap.GetPixel(1, 1).ToArgb());
            Assert.False(control.IsHandleCreated);
        });

    [Fact]
    public void NativePrintClientRestoresBackgroundTransformAndClipBeforePaintingForeground()
        => OnSta(() =>
        {
            using var control = new PrintControl { Size = new Size(40, 32), ChangeBackgroundState = true };
            using var bitmap = new Bitmap(40, 32);
            PrintNative(control, bitmap);

            Assert.Equal(PointF.Empty, control.ForegroundOffset);
            Assert.True(control.ForegroundContainsPixel);
            Assert.Equal(Color.Blue.ToArgb(), bitmap.GetPixel(22, 20).ToArgb());
            Assert.Equal(1, control.BackgroundCalls);
            Assert.Equal(1, control.ForegroundCalls);
            Graphics graphics = Assert.IsType<Graphics>(control.LastGraphics);
            Assert.Throws<ArgumentException>(() => _ = graphics.ClipBounds);
        });

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void NativePaintingFailureReportsTheOriginalErrorThenDrawsTheErrorSurface(bool background)
        => OnSta(() =>
        {
            using var control = new PrintControl { Size = new Size(40, 32) };
            using var bitmap = new Bitmap(40, 32);
            Exception failure = new InvalidOperationException("native painting failure");
            control.BackgroundFailure = background ? failure : null;
            control.ForegroundFailure = background ? null : failure;
            List<Exception> reported = [];
            ThreadExceptionEventHandler handler = (_, args) => reported.Add(args.Exception);
            Application.ThreadException += handler;
            try
            {
                PrintNative(control, bitmap);
                Assert.Same(failure, Assert.Single(reported));
                Graphics graphics = Assert.IsType<Graphics>(control.LastGraphics);
                Assert.Throws<ArgumentException>(() => _ = graphics.ClipBounds);

                int backgroundCalls = control.BackgroundCalls;
                int foregroundCalls = control.ForegroundCalls;
                PrintNative(control, bitmap);

                Assert.Single(reported);
                Assert.Equal(backgroundCalls, control.BackgroundCalls);
                Assert.Equal(foregroundCalls, control.ForegroundCalls);
                Assert.Equal(Color.Red.ToArgb(), bitmap.GetPixel(20, 16).ToArgb());
                Assert.Equal(Color.White.ToArgb(), bitmap.GetPixel(10, 16).ToArgb());
            }
            finally { Application.ThreadException -= handler; }
        });

    [Fact]
    public void NativeDestructionReportsCompletionFailureAndStillClearsHandleOwnership()
        => OnSta(() =>
        {
            using var control = new ExposedControl();
            nint handle = control.Handle;
            bool invoked = false;
            IAsyncResult pending = control.BeginInvoke(() => invoked = true);
            pending.AsyncWaitHandle.Dispose();
            List<Exception> reported = [];
            ThreadExceptionEventHandler handler = (_, args) => reported.Add(args.Exception);
            Application.ThreadException += handler;
            try
            {
                Assert.True(DestroyWindow(handle));
                Assert.IsType<ObjectDisposedException>(Assert.Single(reported));
                Assert.False(control.IsHandleCreated);
                Assert.False(invoked);
                Assert.True(pending.IsCompleted);
                Assert.Throws<ObjectDisposedException>(() => control.EndInvoke(pending));

                // A new HWND must get a usable dispatcher after the previous
                // dispatcher's completion failure during native destruction.
                _ = control.Handle;
                IAsyncResult next = control.BeginInvoke(() => invoked = true);
                control.EndInvoke(next);
                Assert.True(invoked);
                Assert.Single(reported);
            }
            finally { Application.ThreadException -= handler; }
        });

    private static void PrintNative(Control control, Bitmap bitmap)
    {
        using Graphics graphics = Graphics.FromImage(bitmap);
        graphics.Clear(Color.Fuchsia);
        nint hdc = graphics.GetHdc();
        try { _ = SendMessageW(control.Handle, 0x0318 /* WM_PRINTCLIENT */, hdc, (nint)0xC); }
        finally { graphics.ReleaseHdc(hdc); }
    }

    [DllImport("user32.dll")]
    private static extern nint SendMessageW(nint hwnd, uint message, nint wParam, nint lParam);

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint hwnd);

    private static void OnSta(Action action)
    {
        Exception? failure = null;
        Thread thread = new(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            try { action(); }
            catch (Exception exception) { failure = exception; }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(15)), "The control message/paint check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class ExposedControl : Control
    {
        internal readonly List<string> Trace = [];
        internal bool Rewrite;
        internal Color PaintColor;

        internal bool ReadStyle(ControlStyles style) => GetStyle(style);
        internal void WriteStyle(ControlStyles style, bool value) => SetStyle(style, value);
        internal void CallWindow(ref Message message) => WndProc(ref message);
        internal void RaisePaint(PaintEventArgs args) => OnPaint(args);
        internal void RaiseBackground(PaintEventArgs args) => OnPaintBackground(args);

        protected override void WndProc(ref Message message)
        {
            if (message.Msg == 0x8000 + 123)
            {
                Trace.Add($"enter:{message.WParam},{message.LParam}");
                if (!Rewrite)
                {
                    message.Result = 2468;
                    Trace.Add("handled");
                    return;
                }
                message.Msg = 0x000E;
                base.WndProc(ref message);
                Trace.Add($"rewrite:{message.Result}");
                return;
            }
            base.WndProc(ref message);
        }

        protected override void OnPaintBackground(PaintEventArgs e) => base.OnPaintBackground(e);

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!PaintColor.IsEmpty)
            {
                using SolidBrush brush = new(PaintColor);
                e.Graphics.FillRectangle(brush, new Rectangle(0, 0, 20, 20));
            }
            base.OnPaint(e);
        }
    }

    private sealed class PrintControl : Control
    {
        internal bool ChangeBackgroundState;
        internal Exception? BackgroundFailure;
        internal Exception? ForegroundFailure;
        internal int BackgroundCalls;
        internal int ForegroundCalls;
        internal Graphics? LastGraphics;
        internal PointF ForegroundOffset;
        internal bool ForegroundContainsPixel;

        internal void RaisePrint(PaintEventArgs args) => OnPrint(args);

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            BackgroundCalls++;
            LastGraphics = e.Graphics;
            if (BackgroundFailure is not null) { throw BackgroundFailure; }
            if (ChangeBackgroundState)
            {
                e.Graphics.TranslateTransform(5, 6);
                e.Graphics.SetClip(new Rectangle(0, 0, 8, 8));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            ForegroundCalls++;
            LastGraphics = e.Graphics;
            if (ForegroundFailure is not null) { throw ForegroundFailure; }
            using Matrix transform = e.Graphics.Transform;
            ForegroundOffset = new PointF(transform.OffsetX, transform.OffsetY);
            ForegroundContainsPixel = e.Graphics.IsVisible(22, 20);
            e.Graphics.FillRectangle(Brushes.Blue, new Rectangle(20, 18, 6, 6));
            base.OnPaint(e);
        }
    }
}
