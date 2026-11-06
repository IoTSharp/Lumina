using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class ApplicationOleLifetimeTests
{
    private const int ProbeMessage = 0x8000 + 161;

    [Fact]
    public void ExitThreadDefersOleReleaseUntilTheOuterMessageLoopReturns()
        => OnStaThread(() =>
        {
            int baseline = ProbeOleInitialization();
            Assert.Equal(ApartmentState.STA, Application.OleRequired());
            bool called = false;
            using var control = new ProbeControl();
            control.Callback = () =>
            {
                called = true;
                Application.ExitThread();
                Assert.Equal(1, ProbeOleInitialization());
            };

            Assert.True(PostMessageW(control.Handle, ProbeMessage, 0, 0));
            Assert.Equal(0, Application.RunMessageLoop());
            Assert.True(called);
            Assert.Equal(baseline, ProbeOleInitialization());
        });

    [Fact]
    public void ReturningFromAnInnerMessageLoopKeepsTheOuterOleInitialization()
        => OnStaThread(() =>
        {
            int baseline = ProbeOleInitialization();
            Assert.Equal(ApartmentState.STA, Application.OleRequired());
            bool called = false;
            using var control = new ProbeControl();
            control.Callback = () =>
            {
                called = true;
                PostQuitMessage(0);
                Assert.Equal(0, Application.RunMessageLoop());
                Assert.Equal(1, ProbeOleInitialization());
                Application.ExitThread();
                Assert.Equal(1, ProbeOleInitialization());
            };

            Assert.True(PostMessageW(control.Handle, ProbeMessage, 0, 0));
            Assert.Equal(0, Application.RunMessageLoop());
            Assert.True(called);
            Assert.Equal(baseline, ProbeOleInitialization());
        });

    private static int ProbeOleInitialization()
    {
        int result = OleInitialize(0);
        Assert.True(result >= 0, $"OleInitialize failed: 0x{result:X8}");
        OleUninitialize();
        return result;
    }

    private static void OnStaThread(Action action)
    {
        Exception? failure = null;
        uint threadId = 0;
        var thread = new Thread(() =>
        {
            threadId = GetCurrentThreadId();
            ThreadExceptionEventHandler handler = (_, args) =>
            {
                failure ??= args.Exception;
                Application.ExitThread();
            };
            Application.ThreadException += handler;
            try
            {
                action();
            }
            catch (Exception exception)
            {
                failure ??= exception;
            }
            finally
            {
                Application.ThreadException -= handler;
                Application.ExitThread();
            }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        bool completed = thread.Join(TimeSpan.FromSeconds(10));
        if (!completed)
        {
            _ = PostThreadMessageW(threadId, 0x0012, 0, 0);
            Assert.True(thread.Join(TimeSpan.FromSeconds(2)), "STA did not stop after its bounded quit request.");
        }

        Assert.True(completed, "OLE lifetime scenario exceeded ten seconds.");
        if (failure is not null)
        {
            ExceptionDispatchInfo.Capture(failure).Throw();
        }
    }

    private sealed class ProbeControl : Control
    {
        internal Action? Callback { get; set; }

        protected override void WndProc(ref Message message)
        {
            if (message.Msg == ProbeMessage)
            {
                Callback?.Invoke();
                return;
            }

            base.WndProc(ref message);
        }
    }

    [DllImport("ole32.dll")]
    private static extern int OleInitialize(nint reserved);

    [DllImport("ole32.dll")]
    private static extern void OleUninitialize();

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool PostMessageW(nint window, int message, nint wParam, nint lParam);

    [DllImport("user32.dll")]
    private static extern void PostQuitMessage(int exitCode);

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool PostThreadMessageW(uint threadId, int message, nint wParam, nint lParam);

    [DllImport("kernel32.dll")]
    private static extern uint GetCurrentThreadId();
}
