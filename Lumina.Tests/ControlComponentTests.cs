using System.ComponentModel;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public class ControlComponentTests
{
    [Fact]
    public void Control_CanBeConstructedAndConsumedAsAComponentAndWindow()
    {
        using var control = new Control();

        Assert.IsAssignableFrom<Component>(control);
        Assert.IsAssignableFrom<IComponent>(control);
        Assert.IsAssignableFrom<IDisposable>(control);
        IWin32Window window = control;
        Assert.Same(control, window);
        Assert.False(control.IsHandleCreated);
        Assert.False(control.IsDisposed);
        Assert.False(control.Disposing);
        Assert.Null(control.Site);
        Assert.Null(control.Container);

        control.Text = "Managed control";
        Assert.Equal("Managed control", control.Text);
    }

    [Fact]
    public void ComponentDispose_DispatchesToTheDerivedControlBeforeRaisingDisposed()
    {
        var control = new DisposalControl();
        Component component = control;
        List<string> calls = [];
        control.OnDisposingAction = () =>
        {
            calls.Add("OnDisposing");
            Assert.False(control.IsDisposed);
            Assert.True(control.Disposing);
        };
        control.Disposed += (sender, e) =>
        {
            calls.Add("Disposed");
            Assert.Same(control, sender);
            Assert.Same(EventArgs.Empty, e);
            Assert.False(control.IsDisposed);
            Assert.True(control.Disposing);
        };

        component.Dispose();

        Assert.Equal(["OnDisposing", "Disposed"], calls);
        Assert.Equal(1, control.ManagedDisposeCalls);
        Assert.Equal(1, control.OnDisposingCalls);
        Assert.True(control.IsDisposed);
        Assert.False(control.Disposing);
    }

    [Fact]
    public void Dispose_RemovesTheComponentSiteBeforeRaisingDisposed()
    {
        using var components = new Container();
        var control = new Control();
        IComponent component = control;
        components.Add(component, "hostedControl");
        int disposedCalls = 0;

        Assert.Same(components, control.Container);
        Assert.Same(components, component.Site!.Container);
        Assert.Equal("hostedControl", component.Site.Name);
        Assert.Same(control, components.Components["hostedControl"]);
        control.Disposed += (_, _) =>
        {
            disposedCalls++;
            Assert.Null(control.Site);
            Assert.Null(control.Container);
            Assert.Empty(components.Components.Cast<IComponent>());
            Assert.False(control.IsDisposed);
            Assert.True(control.Disposing);
        };

        component.Dispose();

        Assert.Equal(1, disposedCalls);
        Assert.True(control.IsDisposed);
        Assert.False(control.Disposing);
    }

    [Fact]
    public void ContainerDispose_CascadesToControlsAndTheirExistingDisposalHooks()
    {
        var components = new Container();
        var first = new DisposalControl();
        var second = new DisposalControl();
        var textBox = new TextBox();
        int disposedCalls = 0;
        first.Disposed += (_, _) => disposedCalls++;
        second.Disposed += (_, _) => disposedCalls++;
        textBox.Disposed += (_, _) => disposedCalls++;
        components.Add(first, "first");
        components.Add(second, "second");
        components.Add(textBox, "textBox");

        components.Dispose();
        components.Dispose();

        Assert.Equal(3, disposedCalls);
        Assert.Equal(1, first.OnDisposingCalls);
        Assert.Equal(1, second.OnDisposingCalls);
        Assert.True(first.IsDisposed);
        Assert.True(second.IsDisposed);
        Assert.True(textBox.IsDisposed);
        Assert.Null(first.Site);
        Assert.Null(second.Site);
        Assert.Null(textBox.Site);
        Assert.Empty(components.Components.Cast<IComponent>());
    }

    [Fact]
    public void Dispose_RepeatedCallsReleaseResourcesOnceAndRaiseDisposedEachTime()
    {
        var control = new DisposalControl();
        int disposedCalls = 0;
        control.Disposed += (_, _) => disposedCalls++;

        control.Dispose();
        ((IDisposable)control).Dispose();

        Assert.Equal(2, control.ManagedDisposeCalls);
        Assert.Equal(1, control.OnDisposingCalls);
        Assert.Equal(2, disposedCalls);
        Assert.True(control.IsDisposed);
        Assert.False(control.Disposing);
    }

    [Fact]
    public void Dispose_ReentrantCallsDoNotRepeatCleanupOrDisposed()
    {
        var control = new DisposalControl();
        int disposedCalls = 0;
        control.OnDisposingAction = control.Dispose;
        control.Disposed += (_, _) =>
        {
            disposedCalls++;
            control.Dispose();
        };

        control.Dispose();

        Assert.Equal(1, control.OnDisposingCalls);
        Assert.Equal(1, disposedCalls);
        Assert.True(control.IsDisposed);
        Assert.False(control.Disposing);
    }

    [Fact]
    public void DisposeFalse_PreservesManagedOwnershipAndAllowsLaterManagedDisposal()
    {
        using var components = new Container();
        using var parent = new Panel();
        var control = new DisposalControl();
        parent.Controls.Add(control);
        components.Add(control, "hostedControl");
        int disposedCalls = 0;
        control.Disposed += (_, _) => disposedCalls++;

        control.ReleaseUnmanaged();

        Assert.Same(parent, control.Parent);
        Assert.True(parent.Controls.Contains(control));
        Assert.Same(components, control.Container);
        Assert.Same(control, components.Components["hostedControl"]);
        Assert.Equal(0, control.OnDisposingCalls);
        Assert.Equal(0, disposedCalls);
        Assert.False(control.IsDisposed);
        Assert.False(control.Disposing);

        control.Dispose();

        Assert.Null(control.Parent);
        Assert.False(parent.Controls.Contains(control));
        Assert.Null(control.Site);
        Assert.Equal(1, control.OnDisposingCalls);
        Assert.Equal(1, disposedCalls);
        Assert.True(control.IsDisposed);
    }

    [Fact]
    public void Dispose_WhenDisposedHandlerThrowsStillFinishesTheControlState()
    {
        using var components = new Container();
        var control = new DisposalControl();
        components.Add(control);
        var error = new InvalidOperationException("Disposed handler failed.");
        int disposedCalls = 0;
        control.Disposed += (_, _) =>
        {
            if (++disposedCalls == 1)
            {
                throw error;
            }
        };

        Assert.Same(error, Assert.Throws<InvalidOperationException>(control.Dispose));

        Assert.True(control.IsDisposed);
        Assert.False(control.Disposing);
        Assert.Null(control.Site);
        Assert.Equal(1, control.OnDisposingCalls);
        control.Dispose();
        Assert.Equal(2, disposedCalls);
        Assert.Equal(1, control.OnDisposingCalls);
    }

    [Fact]
    public void Dispose_WhenExistingDisposalHookThrowsStillReleasesTheComponentSite()
    {
        using var components = new Container();
        var control = new DisposalControl();
        components.Add(control);
        var error = new InvalidOperationException("Control cleanup failed.");
        control.OnDisposingAction = () => throw error;
        int disposedCalls = 0;
        control.Disposed += (_, _) => disposedCalls++;

        Assert.Same(error, Assert.Throws<InvalidOperationException>(control.Dispose));

        Assert.True(control.IsDisposed);
        Assert.False(control.Disposing);
        Assert.Null(control.Site);
        Assert.Empty(components.Components.Cast<IComponent>());
        Assert.Equal(1, disposedCalls);
        Assert.Equal(1, control.OnDisposingCalls);
    }

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void IWin32Window_ExposesTheCreatedControlHandleToANativeConsumer()
    {
        using var form = new Form();
        using var control = new Control { Text = "Native control" };
        form.Controls.Add(control);
        IWin32Window window = control;

        form.Show();

        nint handle = window.Handle;
        Assert.NotEqual(0, handle);
        Assert.Equal(control.Handle, handle);
        Assert.True(IsWindow(window.Handle));
        Assert.Equal(control.Text.Length, GetWindowTextLength(window.Handle));

        control.Dispose();
        Assert.False(control.IsHandleCreated);
        Assert.False(IsWindow(handle));
        Assert.False(form.Controls.Contains(control));
    }

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint handle);

    [DllImport("user32.dll", EntryPoint = "GetWindowTextLengthW")]
    private static extern int GetWindowTextLength(nint handle);

    private sealed class DisposalControl : Control
    {
        public int ManagedDisposeCalls { get; private set; }

        public int OnDisposingCalls { get; private set; }

        public Action? OnDisposingAction { get; set; }

        public void ReleaseUnmanaged() => Dispose(false);

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ManagedDisposeCalls++;
            }

            base.Dispose(disposing);
        }

        protected override void OnDisposing()
        {
            OnDisposingCalls++;
            OnDisposingAction?.Invoke();
            base.OnDisposing();
        }
    }
}
