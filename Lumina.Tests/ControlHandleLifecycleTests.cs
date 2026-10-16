using System.Drawing;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public class ControlHandleLifecycleTests
{
    [Fact]
    public void StandardLifecycleSurfaceHasTheExpectedSlots()
    {
        PropertyInfo handle = typeof(Control).GetProperty(nameof(Control.Handle))!;
        Assert.Null(handle.SetMethod);
        Assert.Equal(typeof(nint), handle.PropertyType);
        foreach (string name in new[] { nameof(Control.Created), nameof(Control.IsHandleCreated), nameof(Control.RecreatingHandle) })
        {
            PropertyInfo property = typeof(Control).GetProperty(name)!;
            Assert.Null(property.SetMethod);
            Assert.False(property.GetMethod!.IsVirtual);
        }
        foreach (string name in new[] { "CreateHandle", "DestroyHandle", "OnCreateControl" })
        {
            MethodInfo method = typeof(Control).GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance, Type.EmptyTypes)!;
            Assert.True(method.IsFamily);
            Assert.True(method.IsVirtual);
        }
        MethodInfo recreate = typeof(Control).GetMethod("RecreateHandle", BindingFlags.NonPublic | BindingFlags.Instance)!;
        Assert.True(recreate.IsFamily);
        Assert.False(recreate.IsVirtual);
    }

    [Fact]
    public void ManagedReadsAndMutationsDoNotCreateAWindow() => OnSta(() =>
    {
        using var control = new LifecycleControl { Text = "managed", Bounds = new Rectangle(3, 5, 80, 25) };
        Assert.False(control.IsHandleCreated);
        Assert.False(control.Created);
        Assert.False(control.RecreatingHandle);
        Assert.False(control.Focused);
        Assert.False(control.ContainsFocus);
        Assert.False(control.CanFocus);
        Assert.False(control.InvokeRequired);
        Assert.False(control.Focus());
        Assert.Equal("managed", control.Text);
        control.Refresh();
        control.Invalidate();
        Assert.Empty(control.Calls);
        Assert.False(control.IsHandleCreated);
    });

    [Fact]
    public void HiddenCreateControlDefersTheTreeAndHandleAccessDoesNotCreateAncestors() => OnSta(() =>
    {
        using var parent = new LifecycleControl { Visible = false };
        using var child = new LifecycleControl { Parent = parent, Visible = false };
        using var leaf = new LifecycleControl { Parent = child };
        child.CreateControl();
        Assert.False(child.IsHandleCreated);
        Assert.False(child.Created);
        nint handle = child.Handle;
        Assert.NotEqual(0, handle);
        Assert.True(child.IsHandleCreated);
        Assert.False(child.Created);
        Assert.False(parent.IsHandleCreated);
        Assert.False(leaf.IsHandleCreated);
        Assert.Same(child, Control.FromHandle(handle));
        Assert.Same(child, Control.FromChildHandle(handle));
    });

    [Fact]
    public void LazyVisibleHandleCreatesDescendantsWithoutMaterializingItsParent() => OnSta(() =>
    {
        using var parent = new LifecycleControl();
        using var child = new LifecycleControl { Parent = parent };
        using var leaf = new LifecycleControl { Parent = child };
        bool createdDuringHandleEvent = true;
        child.HandleCreated += (_, _) => createdDuringHandleEvent = child.Created;
        nint handle = child.Handle;
        Assert.False(createdDuringHandleEvent);
        Assert.False(parent.IsHandleCreated);
        Assert.True(child.Created);
        Assert.True(leaf.Created);
        Assert.Equal(handle, GetAncestor(leaf.Handle, 1));
        int count = child.Calls.Count;
        Assert.Equal(handle, child.Handle);
        child.CreateControl();
        Assert.Equal(count, child.Calls.Count);
    });

    [Fact]
    public void ExplicitCreateControlSetsCreatedBeforeHandleAndFinishesChildrenBeforeParent() => OnSta(() =>
    {
        using var root = new LifecycleControl();
        using var child = new LifecycleControl { Parent = root };
        List<string> order = [];
        root.HandleCreated += (_, _) => { Assert.True(root.Created); order.Add("root-handle"); };
        child.HandleCreated += (_, _) => { Assert.True(child.Created); order.Add("child-handle"); };
        child.CreateAction = () => order.Add("child-created");
        root.CreateAction = () => order.Add("root-created");
        root.CreateControl();
        Assert.Equal(["root-handle", "child-handle", "child-created", "root-created"], order);
        root.CreateControl();
        Assert.Equal(4, order.Count);
    });

    [Fact]
    public void RepeatedProtectedCreateHandleRejectsAnExistingWindow() => OnSta(() =>
    {
        using var control = new LifecycleControl();
        control.MakeHandle();
        nint handle = control.Handle;
        Assert.Throws<InvalidOperationException>(control.MakeHandle);
        Assert.Equal(handle, control.Handle);
        Assert.True(IsWindow(handle));
    });

    [Fact]
    public void DestroyCallbacksSeeTheOldHandleAndMappingUntilNativeTeardownCompletes() => OnSta(() =>
    {
        using var root = new LifecycleControl();
        using var child = new LifecycleControl { Parent = root };
        root.CreateControl();
        nint rootHandle = root.Handle;
        nint childHandle = child.Handle;
        int destroyed = 0;
        root.HandleDestroyed += (_, args) =>
        {
            destroyed++;
            Assert.Same(EventArgs.Empty, args);
            Assert.True(root.IsHandleCreated);
            Assert.True(root.Created);
            Assert.Equal(rootHandle, root.Handle);
            Assert.Same(root, Control.FromHandle(rootHandle));
        };
        root.DropHandle();
        Assert.Equal(1, destroyed);
        Assert.False(root.IsHandleCreated);
        Assert.False(root.Created);
        Assert.False(child.IsHandleCreated);
        Assert.False(child.Created);
        Assert.False(root.IsDisposed);
        Assert.False(child.IsDisposed);
        Assert.Null(Control.FromHandle(rootHandle));
        Assert.Null(Control.FromHandle(childHandle));
        root.DropHandle();
        Assert.Equal(1, destroyed);
    });

    [Fact]
    public void ExternalNativeDestructionClearsTheWholeTreeWithoutDisposingManagedControls() => OnSta(() =>
    {
        using var root = new LifecycleControl();
        using var child = new LifecycleControl { Parent = root };
        root.CreateControl();
        nint rootHandle = root.Handle;
        nint childHandle = child.Handle;
        Assert.True(DestroyWindow(rootHandle));
        Assert.False(root.IsHandleCreated);
        Assert.False(child.IsHandleCreated);
        Assert.False(root.IsDisposed);
        Assert.False(child.IsDisposed);
        Assert.Null(Control.FromChildHandle(childHandle));
        root.CreateControl();
        Assert.True(root.Created);
        Assert.True(child.Created);
        Assert.Equal(root.Handle, GetAncestor(child.Handle, 1));
    });

    [Fact]
    public void DisposingAnUncreatedRootDestroysItsParkedNativeDescendants() => OnSta(() =>
    {
        using var root = new LifecycleControl { Visible = false };
        var child = new LifecycleControl { Parent = root };
        var leaf = new LifecycleControl { Parent = child };
        nint childHandle = child.Handle;
        nint leafHandle = leaf.Handle;
        Assert.False(root.IsHandleCreated);
        root.Dispose();
        Assert.True(child.IsDisposed);
        Assert.True(leaf.IsDisposed);
        Assert.False(IsWindow(childHandle));
        Assert.False(IsWindow(leafHandle));
        Assert.Null(Control.FromHandle(childHandle));
        Assert.Null(Control.FromHandle(leafHandle));
    });

    [Fact]
    public void NativeDescendantLookupFindsTheNearestManagedAncestor() => OnSta(() =>
    {
        using var root = new LifecycleControl();
        using var child = new LifecycleControl { Parent = root };
        root.CreateControl();
        nint native = CreateWindowExW(0, "STATIC", "native", 0x40000000, 0, 0, 10, 10,
            child.Handle, 0, 0, 0);
        Assert.NotEqual(0, native);
        try
        {
            Assert.Null(Control.FromHandle(native));
            Assert.Same(child, Control.FromChildHandle(native));
            Assert.Null(Control.FromHandle(0));
            Assert.Null(Control.FromChildHandle(0));
        }
        finally { _ = DestroyWindow(native); }
    });

    [Fact]
    public void RecreationRestoresPropertiesAndAlreadyCreatedHiddenDescendants() => OnSta(() =>
    {
        using var root = new LifecycleControl { Text = "root text", Bounds = new Rectangle(5, 8, 120, 70) };
        using var child = new LifecycleControl { Parent = root, Text = "child", Enabled = false };
        using var hidden = new LifecycleControl { Parent = root, Text = "hidden", Visible = false };
        root.CreateControl();
        nint oldRoot = root.Handle;
        nint oldChild = child.Handle;
        nint oldHidden = hidden.Handle;
        Rectangle bounds = root.Bounds;
        int destroyed = 0;
        EventHandler destroyedHandler = (_, _) => { Assert.True(root.RecreatingHandle); destroyed++; };
        EventHandler createdHandler = (_, _) => { Assert.True(root.RecreatingHandle); Assert.False(root.Created); };
        root.HandleDestroyed += destroyedHandler;
        root.HandleCreated += createdHandler;
        root.RebuildHandle();
        root.HandleDestroyed -= destroyedHandler;
        root.HandleCreated -= createdHandler;
        Assert.Equal(1, destroyed);
        Assert.False(root.RecreatingHandle);
        Assert.True(root.Created);
        Assert.True(child.Created);
        Assert.False(hidden.Created);
        Assert.False(hidden.Visible);
        Assert.False(child.Enabled);
        Assert.Equal("root text", root.Text);
        Assert.Equal("child", child.Text);
        Assert.Equal("hidden", hidden.Text);
        Assert.Equal(bounds, root.Bounds);
        Assert.NotEqual(oldRoot, root.Handle);
        Assert.NotEqual(oldChild, child.Handle);
        Assert.NotEqual(oldHidden, hidden.Handle);
        Assert.Equal(root.Handle, GetAncestor(child.Handle, 1));
        Assert.Equal(root.Handle, GetAncestor(hidden.Handle, 1));
        Assert.Null(Control.FromHandle(oldRoot));
    });

    [Fact]
    public void RecreationRetainsPostedWorkOnBothParentAndChild() => OnSta(() =>
    {
        using var root = new LifecycleControl();
        using var child = new LifecycleControl { Parent = root };
        root.CreateControl();
        int rootCalls = 0;
        int childCalls = 0;
        IAsyncResult rootPending = root.BeginInvoke((Func<int>)(() => ++rootCalls));
        IAsyncResult childPending = child.BeginInvoke((Func<int>)(() => ++childCalls));
        root.RebuildHandle();
        Assert.Equal(1, root.EndInvoke(rootPending));
        Assert.Equal(1, child.EndInvoke(childPending));
        Assert.True(rootPending.IsCompleted);
        Assert.True(childPending.IsCompleted);
        Assert.Equal(1, rootCalls);
        Assert.Equal(1, childCalls);
    });

    [Fact]
    public void FailedCreateControlResetsCreatedAndPreservesTheOriginalException() => OnSta(() =>
    {
        using var control = new LifecycleControl();
        var failure = new ApplicationException("create failure");
        control.CreateHandleAction = () => throw failure;
        Assert.Same(failure, Assert.Throws<ApplicationException>(control.CreateControl));
        Assert.False(control.Created);
        Assert.False(control.IsHandleCreated);
        control.CreateHandleAction = null;
        control.CreateControl();
        Assert.True(control.Created);
        Assert.True(control.IsHandleCreated);
    });

    [Fact]
    public void OnCreateControlFailureKeepsTheAlreadyCreatedNativeTree() => OnSta(() =>
    {
        using var root = new LifecycleControl();
        using var child = new LifecycleControl { Parent = root };
        var failure = new ApplicationException("managed creation failure");
        root.CreateAction = () => throw failure;
        Assert.Same(failure, Assert.Throws<ApplicationException>(root.CreateControl));
        Assert.True(root.Created);
        Assert.True(child.Created);
        root.CreateAction = null;
        root.CreateControl();
        Assert.True(root.IsHandleCreated);
    });

    [Fact]
    public void DirectStandardHooksPreserveArgumentsAndLegacyCreationHook() => OnSta(() =>
    {
        using var control = new LifecycleControl();
        EventArgs createdArgs = new();
        EventArgs destroyedArgs = new();
        EventArgs? seenCreated = null;
        EventArgs? seenDestroyed = null;
        control.HandleCreated += (_, args) => seenCreated = args;
        control.HandleDestroyed += (_, args) => seenDestroyed = args;
        control.RaiseCreated(createdArgs);
        control.RaiseDestroyed(destroyedArgs);
        Assert.Same(createdArgs, seenCreated);
        Assert.Same(destroyedArgs, seenDestroyed);
        Assert.Contains("legacy-created", control.Calls);
        Assert.False(control.IsHandleCreated);
        Assert.False(control.Created);
    });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void DisposeCallsTheDestroyOverrideOnceAndKeepsItsNativeFailureState(bool throws) => OnSta(() =>
    {
        using var container = new Container();
        var root = new LifecycleControl();
        var child = new LifecycleControl { Parent = root };
        container.Add(root);
        container.Add(child);
        root.CreateControl();
        nint handle = root.Handle;
        var failure = new ApplicationException("destroy override failure");
        root.SkipDestroyHandle = !throws;
        root.DestroyHandleAction = throws ? () => throw failure : null;
        try
        {
            if (throws) { Assert.Same(failure, Assert.Throws<ApplicationException>(root.Dispose)); }
            else { root.Dispose(); }
            Assert.Equal(1, root.DestroyHandleCalls);
            Assert.True(root.IsDisposed);
            Assert.True(root.IsHandleCreated);
            Assert.True(root.Created);
            Assert.True(IsWindow(handle));
            Assert.Same(root, Control.FromHandle(handle));
            Assert.Equal(!throws, child.IsDisposed);
            if (throws)
            {
                Assert.Same(container, root.Container);
                Assert.Same(root, child.Parent);
                Assert.True(child.IsHandleCreated);
            }
            else
            {
                Assert.Null(root.Container);
                Assert.Null(child.Parent);
                Assert.False(child.IsHandleCreated);
            }
        }
        finally
        {
            root.SkipDestroyHandle = false;
            root.DestroyHandleAction = null;
            root.DropHandle();
            root.Dispose();
            child.Dispose();
        }
    });

    private sealed class LifecycleControl : Control
    {
        internal List<string> Calls { get; } = [];
        internal Action? CreateAction { get; set; }
        internal Action? CreateHandleAction { get; set; }
        internal Action? DestroyHandleAction { get; set; }
        internal bool SkipDestroyHandle { get; set; }
        internal int DestroyHandleCalls { get; private set; }
        internal void MakeHandle() => CreateHandle();
        internal void DropHandle() => DestroyHandle();
        internal void RebuildHandle() => RecreateHandle();
        internal void RaiseCreated(EventArgs e) => OnHandleCreated(e);
        internal void RaiseDestroyed(EventArgs e) => OnHandleDestroyed(e);
        protected override void CreateHandle()
        {
            CreateHandleAction?.Invoke();
            base.CreateHandle();
        }
        protected override void OnHandleCreated()
        {
            Calls.Add("legacy-created");
            base.OnHandleCreated();
        }
        protected override void DestroyHandle()
        {
            DestroyHandleCalls++;
            DestroyHandleAction?.Invoke();
            if (!SkipDestroyHandle) { base.DestroyHandle(); }
        }
        protected override void OnCreateControl()
        {
            Calls.Add("created");
            base.OnCreateControl();
            CreateAction?.Invoke();
        }
    }

    private static void OnSta(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            try { action(); }
            catch (Exception exception) { failure = exception; }
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(30)), "The lifecycle test did not finish.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint hwnd);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint hwnd);
    [DllImport("user32.dll")]
    private static extern nint GetAncestor(nint hwnd, uint flags);
    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern nint CreateWindowExW(uint exStyle, string className, string text, uint style,
        int x, int y, int width, int height, nint parent, nint menu, nint instance, nint parameter);
}
