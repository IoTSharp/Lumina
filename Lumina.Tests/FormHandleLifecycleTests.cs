using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public class FormHandleLifecycleTests
{
    [Fact]
    public void Form_PropertiesAndHandleQueriesDoNotCreateItsWindow()
        => OnStaThread(() =>
        {
            using var form = new LifecycleForm();
            form.Text = "Deferred form";
            form.Size = new Size(420, 260);
            form.WindowState = FormWindowState.Maximized;
            form.MainMenuStrip = new MenuStrip();
            form.ClearLuminaEffect();
            form.Activate();
            form.BringToFront();
            form.Hide();

            Assert.False(form.IsHandleCreated);
            Assert.False(((Control)form).Created);
            Assert.False(form.Visible);
            Assert.Equal(0, form.CreateCalls);
            form.MainMenuStrip.Dispose();
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void Form_HiddenCreateControlAndLazyHandleKeepTheChildTreeUncreated()
        => OnStaThread(() =>
        {
            using var form = new LifecycleForm();
            var child = new Control();
            form.Controls.Add(child);
            int loads = 0;
            int shown = 0;
            form.Load += (_, _) => loads++;
            form.Shown += (_, _) => shown++;

            form.CreateControl();
            Assert.False(form.IsHandleCreated);
            nint handle = form.Handle;
            form.CreateControl();

            Assert.True(IsWindow(handle));
            Assert.Same(form, Control.FromHandle(handle));
            Assert.Equal(handle, ((IWin32Window)form).Handle);
            Assert.False(((Control)form).Created);
            Assert.False(child.IsHandleCreated);
            Assert.False(form.Visible);
            Assert.Equal(1, form.CreateCalls);
            Assert.Equal(0, form.CreateControlCalls);
            Assert.Equal(0, loads);
            Assert.Equal(0, shown);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void Form_ShowCreatesTheVisibleTreeAndQueuesShownAfterLoad()
        => OnStaThread(() =>
        {
            using var form = new LifecycleForm();
            var visible = new Control();
            var hidden = new Control { Visible = false };
            form.Controls.AddRange(visible, hidden);
            List<string> events = [];
            form.Load += (_, _) => events.Add($"load:{((Control)form).Created}/{visible.IsHandleCreated}/{form.Visible}");
            form.Shown += (_, _) => events.Add("shown");

            form.Show();
            Assert.Equal(["load:True/True/True"], events);
            Assert.True(((Control)form).Created);
            Assert.Equal(form.Handle, GetParent(visible.Handle));
            Assert.False(hidden.IsHandleCreated);
            Assert.False(PumpMessages());
            Assert.Equal(["load:True/True/True", "shown"], events);

            form.Hide();
            form.Show();
            Assert.False(PumpMessages());
            Assert.Equal(["load:True/True/True", "shown"], events);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void Form_DestroyHandlePreservesItsLifetimeAndDoesNotQuitTheThread()
        => OnStaThread(() =>
        {
            using var form = new LifecycleForm();
            var child = new Control();
            form.Controls.Add(child);
            int closed = 0;
            form.Closed += (_, _) => closed++;
            form.Show();
            _ = PumpMessages();
            nint oldHandle = form.Handle;
            nint oldChild = child.Handle;
            string? duringDestroy = null;
            form.HandleDestroyed += (_, _) => duringDestroy = $"{form.IsHandleCreated}/{((Control)form).Created}/{form.Visible}/{ReferenceEquals(form, Control.FromHandle(oldHandle))}";

            form.DropHandle();

            Assert.Equal("True/True/True/True", duringDestroy);
            Assert.False(IsWindow(oldHandle));
            Assert.False(IsWindow(oldChild));
            Assert.Null(Control.FromHandle(oldHandle));
            Assert.False(form.IsHandleCreated);
            Assert.False(((Control)form).Created);
            Assert.True(form.Visible);
            Assert.False(form.IsDisposed);
            Assert.False(child.IsDisposed);
            Assert.Equal(0, closed);
            Assert.False(PumpMessages());

            string? duringDispose = null;
            form.Disposed += (_, _) => duringDispose = $"{form.Visible}/{form.IsDisposed}";
            form.Dispose();
            Assert.Equal("True/False", duringDispose);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void Form_RecreateRestoresTheNativeTreeAndPendingCallbacksWithoutClosing()
        => OnStaThread(() =>
        {
            using var form = new LifecycleForm();
            var child = new Control();
            form.Controls.Add(child);
            int loads = 0;
            int shown = 0;
            int closed = 0;
            form.Load += (_, _) => loads++;
            form.Shown += (_, _) => shown++;
            form.Closed += (_, _) => closed++;
            form.Show();
            Assert.False(PumpMessages());
            nint oldHandle = form.Handle;
            nint oldChild = child.Handle;
            int callbacks = 0;
            int visibilityChanges = 0;
            form.VisibleChanged += (_, _) => visibilityChanges++;
            IAsyncResult result = form.BeginInvoke((Action)(() => callbacks++));
            string? duringCreate = null;
            form.HandleCreated += (_, _) => duringCreate = $"{((Control)form).Created}/{form.RecreatingHandle}/{form.Visible}";

            form.RebuildHandle();

            Assert.Equal("False/True/True", duringCreate);
            Assert.False(IsWindow(oldHandle));
            Assert.False(IsWindow(oldChild));
            Assert.True(IsWindowVisible(form.Handle));
            Assert.Equal(form.Handle, GetParent(child.Handle));
            Assert.True(((Control)form).Created);
            Assert.False(form.RecreatingHandle);
            Assert.False(form.IsDisposed);
            Assert.False(PumpMessages());
            Assert.True(result.IsCompleted);
            _ = form.EndInvoke(result);
            Assert.Equal(1, callbacks);
            Assert.Equal(1, loads);
            Assert.Equal(1, shown);
            Assert.Equal(0, closed);
            Assert.Equal(0, visibilityChanges);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void Form_RecreatingAHiddenLazyHandleDoesNotLoadOrCreateChildren()
        => OnStaThread(() =>
        {
            using var form = new LifecycleForm();
            var child = new Control();
            form.Controls.Add(child);
            int loads = 0;
            form.Load += (_, _) => loads++;
            nint oldHandle = form.Handle;

            form.RebuildHandle();

            Assert.False(IsWindow(oldHandle));
            Assert.True(form.IsHandleCreated);
            Assert.False(IsWindowVisible(form.Handle));
            Assert.False(((Control)form).Created);
            Assert.False(child.IsHandleCreated);
            Assert.False(form.Visible);
            Assert.Equal(0, loads);
            Assert.False(PumpMessages());
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void Form_ExternalDestroyClearsMappingsAndLazyRecreationOnlyCreatesTheForm()
        => OnStaThread(() =>
        {
            using var form = new LifecycleForm();
            var child = new Control();
            form.Controls.Add(child);
            int closed = 0;
            form.Closed += (_, _) => closed++;
            form.Show();
            Assert.False(PumpMessages());
            nint oldHandle = form.Handle;
            nint oldChild = child.Handle;
            int visibilityChanges = 0;
            form.VisibleChanged += (_, _) => visibilityChanges++;

            Assert.True(DestroyWindow(oldHandle));

            Assert.Null(Control.FromHandle(oldHandle));
            Assert.Null(Control.FromHandle(oldChild));
            Assert.False(form.IsHandleCreated);
            Assert.False(child.IsHandleCreated);
            Assert.True(form.Visible);
            Assert.False(form.IsDisposed);
            Assert.False(PumpMessages());
            bool? visibleDuringHandleCreated = null;
            form.HandleCreated += (_, _) => visibleDuringHandleCreated = IsWindowVisible(form.Handle);
            _ = form.Handle;
            Assert.True(form.IsHandleCreated);
            Assert.False(visibleDuringHandleCreated);
            Assert.True(IsWindowVisible(form.Handle));
            Assert.False(((Control)form).Created);
            Assert.False(child.IsHandleCreated);
            Assert.Equal(1, form.CreateControlCalls);
            Assert.Equal(0, visibilityChanges);
            Assert.Equal(0, closed);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void Form_CreateControlAfterDestroyRestoresNativeVisibilityWithoutRepeatingLoad()
        => OnStaThread(() =>
        {
            using var form = new LifecycleForm();
            var child = new Control();
            form.Controls.Add(child);
            int loads = 0;
            int shown = 0;
            form.Load += (_, _) => loads++;
            form.Shown += (_, _) => shown++;
            form.Show();
            Assert.False(PumpMessages());
            int visibilityChanges = 0;
            form.VisibleChanged += (_, _) => visibilityChanges++;
            form.DropHandle();

            form.CreateControl();

            Assert.True(((Control)form).Created);
            Assert.True(child.Created);
            Assert.True(IsWindowVisible(form.Handle));
            Assert.True(IsWindowVisible(child.Handle));
            Assert.Equal(form.Handle, GetParent(child.Handle));
            Assert.Equal(0, visibilityChanges);
            Assert.False(PumpMessages());
            Assert.Equal(1, loads);
            Assert.Equal(1, shown);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void Form_CloseRaisesClosedBeforeHandleDestroyedAndDisposal()
        => OnStaThread(() =>
        {
            using var form = new LifecycleForm();
            form.Show();
            _ = PumpMessages();
            List<string> events = [];
            form.FormClosing += (_, _) => events.Add("closing");
            form.Closed += (_, _) => events.Add($"closed:{form.Visible}/{form.Disposing}");
            form.HandleDestroyed += (_, _) => events.Add($"destroyed:{form.Visible}/{form.Disposing}");
            form.Disposed += (_, _) => events.Add($"disposed:{form.Visible}/{form.IsDisposed}");

            form.Close();

            Assert.Equal(["closing", "closed:True/False", "destroyed:True/True", "disposed:False/False"], events);
            Assert.True(form.IsDisposed);
            Assert.False(form.IsHandleCreated);
        });

    [Fact]
    public void Form_CloseWithoutAHandleDisposesWithoutCreatingOrClosingEvents()
        => OnStaThread(() =>
        {
            using var form = new LifecycleForm();
            var child = new Control();
            form.Controls.Add(child);
            int closing = 0;
            int closed = 0;
            form.FormClosing += (_, _) => closing++;
            form.Closed += (_, _) => closed++;

            form.Close();

            Assert.Equal(0, form.CreateCalls);
            Assert.Equal(0, closing);
            Assert.Equal(0, closed);
            Assert.True(form.IsDisposed);
            Assert.True(child.IsDisposed);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void Form_DisposeDuringHandleCreationDoesNotReleaseTheLiveWindowCallback()
        => OnStaThread(() =>
        {
            using var form = new LifecycleForm();
            Exception? failure = null;
            form.HandleCreated += (_, _) =>
            {
                try { form.Dispose(); }
                catch (Exception exception) { failure = exception; }
            };

            nint handle = form.Handle;

            Assert.IsType<InvalidOperationException>(failure);
            Assert.False(form.IsDisposed);
            Assert.True(IsWindow(handle));
            Assert.Same(form, Control.FromHandle(handle));
            form.Text = "Still alive";
            form.Dispose();
            Assert.False(IsWindow(handle));
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void Form_HandleDestroyedFailureUsesTheUiExceptionRouteAndStillClearsNativeState()
        => OnStaThread(() =>
        {
            using var form = new LifecycleForm();
            nint handle = form.Handle;
            var expected = new InvalidOperationException("HandleDestroyed callback");
            Exception? reported = null;
            ThreadExceptionEventHandler handler = (_, e) => reported = e.Exception;
            Application.ThreadException += handler;
            try
            {
                form.HandleDestroyed += (_, _) => throw expected;

                form.DropHandle();

                Assert.Same(expected, reported);
                Assert.False(IsWindow(handle));
                Assert.False(form.IsHandleCreated);
                Assert.Null(Control.FromHandle(handle));
            }
            finally { Application.ThreadException -= handler; }
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void Form_HandleCreatedFailureUsesTheUiExceptionRouteAndLeavesTheWindowUsable()
        => OnStaThread(() =>
        {
            using var form = new LifecycleForm();
            var expected = new InvalidOperationException("HandleCreated callback");
            Exception? reported = null;
            ThreadExceptionEventHandler handler = (_, e) => reported = e.Exception;
            Application.ThreadException += handler;
            try
            {
                form.HandleCreated += (_, _) => throw expected;

                nint handle = form.Handle;

                Assert.Same(expected, reported);
                Assert.True(IsWindow(handle));
                Assert.Same(form, Control.FromHandle(handle));
                Assert.False(form.IsDisposed);
                Assert.False(((Control)form).Created);
                form.Show();
                Assert.True(((Control)form).Created);
                Assert.True(IsWindowVisible(handle));
            }
            finally { Application.ThreadException -= handler; }
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void Form_ThrowingCreationPropertyCanRetryWithoutLeakingAStrongGcHandle()
        => OnStaThread(() =>
        {
            WeakReference reference = CreateRetryAndDisposeForm();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            Assert.False(reference.IsAlive);
        });

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static WeakReference CreateRetryAndDisposeForm()
    {
        var form = new ThrowingCreationForm();
        form.ThrowOnNextStyleRead = true;
        Assert.Throws<InvalidOperationException>(() => _ = form.Handle);
        Assert.False(form.IsHandleCreated);
        nint handle = form.Handle;
        Assert.True(IsWindow(handle));
        Assert.Same(form, Control.FromHandle(handle));
        form.Dispose();
        Assert.False(IsWindow(handle));
        return new WeakReference(form);
    }

    private sealed class ThrowingCreationForm : Form
    {
        internal bool ThrowOnNextStyleRead { get; set; }

        protected override uint WindowExStyle
        {
            get
            {
                if (ThrowOnNextStyleRead)
                {
                    ThrowOnNextStyleRead = false;
                    throw new InvalidOperationException("Creation style getter");
                }
                return base.WindowExStyle;
            }
        }
    }

    private sealed class LifecycleForm : Form
    {
        internal int CreateCalls { get; private set; }
        internal int CreateControlCalls { get; private set; }
        internal void DropHandle() => DestroyHandle();
        internal void RebuildHandle() => RecreateHandle();
        protected override void CreateHandle()
        {
            CreateCalls++;
            base.CreateHandle();
        }
        protected override void OnCreateControl()
        {
            CreateControlCalls++;
            base.OnCreateControl();
        }
    }

    private static bool PumpMessages()
    {
        bool quit = false;
        int count = 0;
        while (PeekMessageW(out NativeMessage message, 0, 0, 0, 1))
        {
            Assert.True(++count < 10_000, "The form message queue did not become idle.");
            if (message.Value == 0x0012) { quit = true; continue; }
            _ = TranslateMessage(ref message);
            _ = DispatchMessageW(ref message);
        }
        return quit;
    }

    private static void OnStaThread(Action action)
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(15)), "The form handle lifecycle check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct NativeMessage
    {
        internal nint Hwnd;
        internal uint Value;
        internal nuint WParam;
        internal nint LParam;
        internal uint Time;
        internal int X;
        internal int Y;
        internal uint Private;
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool PeekMessageW(out NativeMessage message, nint window, uint minimum, uint maximum, uint remove);
    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool TranslateMessage(ref NativeMessage message);
    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint DispatchMessageW(ref NativeMessage message);
    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetParent(nint window);
    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint window);
    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindowVisible(nint window);
    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint window);
}
