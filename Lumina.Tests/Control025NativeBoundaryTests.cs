using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class Control025NativeBoundaryTests
{
    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ExternalNativeDestroyReportsEachFailureAndCompletesCleanup(bool formTarget)
        => OnSta(() =>
        {
            Exception callbackFailure = new InvalidOperationException("control callback failure");
            Exception cleanupFailure = new InvalidOperationException("cleanup failure");
            Exception windowFailure = new InvalidOperationException("window callback failure");
            Control target = formTarget
                ? new DualErrorForm(cleanupFailure, windowFailure)
                : new DualErrorControl(cleanupFailure, windowFailure);

            var reported = new List<Exception>();
            ThreadExceptionEventHandler handler = (_, args) => reported.Add(args.Exception);
            Application.ThreadException += handler;
            try
            {
                nint handle = target.Handle;
                IAsyncResult pending = target.BeginInvoke((Action)(() => throw callbackFailure));

                Assert.True(DestroyWindow(handle));
                Pump();

                Assert.False(IsWindow(handle));
                Assert.False(target.IsHandleCreated);
                Assert.False(target.IsDisposed);
                Assert.True(pending.IsCompleted);
                Assert.IsType<ObjectDisposedException>(Assert.Throws<ObjectDisposedException>(() => target.EndInvoke(pending)));

                Assert.Equal(
                    new[] { "cleanup failure", "window callback failure" },
                    reported.Select(static exception => exception.Message));

                target.Dispose();
                Assert.True(target.IsDisposed);
            }
            finally
            {
                Application.ThreadException -= handler;
                target.Dispose();
            }
        });

    [Fact]
    public void SynchronizationContextCallbackFailureStaysInsideNativeWindowProcedure()
        => OnSta(() =>
        {
            using var context = new WindowsFormsSynchronizationContext();
            Exception expected = new InvalidOperationException("control callback failure");
            Exception? reported = null;
            ThreadExceptionEventHandler handler = (_, args) => reported = args.Exception;
            Application.ThreadException += handler;
            try
            {
                context.Post(_ => throw expected, null);
                PumpUntil(() => reported is not null);
                Assert.Same(expected, reported);
            }
            finally
            {
                Application.ThreadException -= handler;
            }
        });

    private static void OnSta(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            try { action(); }
            catch (Exception exception) { failure = exception; }
            finally { SynchronizationContext.SetSynchronizationContext(null); }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(30)), "The native boundary test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private static void PumpUntil(Func<bool> condition)
    {
        Stopwatch timeout = Stopwatch.StartNew();
        while (!condition())
        {
            Assert.True(timeout.Elapsed < TimeSpan.FromSeconds(10), "The native boundary callback timed out.");
            Pump();
            Thread.Sleep(1);
        }
    }

    private static void Pump()
    {
        while (PeekMessageW(out NativeMessage message, 0, 0, 0, 1))
        {
            _ = TranslateMessage(in message);
            _ = DispatchMessageW(in message);
        }
    }

    private sealed class DualErrorControl(Exception cleanupFailure, Exception windowFailure) : Control
    {
        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            throw cleanupFailure;
        }

        protected override void WndProc(ref Message message)
        {
            if (message.Msg == 0x0082)
            {
                try { base.WndProc(ref message); }
                finally { throw windowFailure; }
            }

            base.WndProc(ref message);
        }
    }

    private sealed class DualErrorForm(Exception cleanupFailure, Exception windowFailure) : Form
    {
        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            throw cleanupFailure;
        }

        protected override void WndProc(ref Message message)
        {
            if (message.Msg == 0x0082)
            {
                try { base.WndProc(ref message); }
                finally { throw windowFailure; }
            }

            base.WndProc(ref message);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct NativeMessage
    {
        internal nint Hwnd;
        internal uint Message;
        internal nuint WParam;
        internal nint LParam;
        internal uint Time;
        internal int X;
        internal int Y;
        internal uint Private;
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool PeekMessageW(out NativeMessage message, nint hwnd, uint minimum, uint maximum, uint remove);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool TranslateMessage(in NativeMessage message);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint DispatchMessageW(in NativeMessage message);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint hwnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint hwnd);
}
