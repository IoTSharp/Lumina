using System.ComponentModel;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public class ControlDisposeCompletionTests
{
    [Fact]
    public void CompletedNativeDestructionStillReleasesParentChildrenAndSiteBeforeRethrowing()
        => OnSta(() =>
        {
            using var components = new Container();
            using var parent = new Control();
            var control = new FailingControl { Parent = parent };
            var child = new Control { Parent = control };
            var grandchild = new Control { Parent = child };
            components.Add(control);
            components.Add(child);
            parent.CreateControl();
            nint[] handles = [control.Handle, child.Handle, grandchild.Handle];
            Control.ControlCollection retained = control.Controls;
            List<string> events = [];
            var nativeFailure = new InvalidOperationException("after native destruction");
            control.DestructionFailure = nativeFailure;
            bool firstDisposal = true;
            control.ParentChanged += (_, _) =>
            {
                Assert.False(control.IsHandleCreated);
                events.Add("parent");
            };
            parent.ControlRemoved += (_, _) => events.Add("removed");
            control.CleanupAction = () =>
            {
                events.Add("cleanup");
                control.Dispose();
            };
            child.Disposed += (_, _) => events.Add("child");
            control.Disposed += (_, _) =>
            {
                Assert.Equal(!firstDisposal, control.IsDisposed);
                Assert.True(control.Disposing);
                Assert.Null(control.Site);
                events.Add("disposed");
                control.Dispose();
            };

            Assert.Same(nativeFailure, Assert.Throws<InvalidOperationException>(control.Dispose));

            Assert.Contains("DestroyHandle", nativeFailure.StackTrace);
            Assert.Equal(["parent", "removed", "cleanup", "child", "disposed"], events);
            Assert.All(handles, handle => Assert.False(IsWindow(handle)));
            Assert.Null(control.Parent);
            Assert.Empty(parent.Controls);
            Assert.True(control.IsDisposed);
            Assert.False(control.Disposing);
            Assert.True(child.IsDisposed);
            Assert.True(grandchild.IsDisposed);
            Assert.Null(child.Parent);
            Assert.Null(grandchild.Parent);
            Assert.Null(child.Site);
            Assert.Empty(components.Components.Cast<IComponent>());
            Assert.Empty(control.Controls);
            Assert.Same(child, Assert.Single(retained.Cast<Control>()));
            Assert.Equal(1, control.DestructionCalls);
            Assert.Equal(1, control.CleanupCalls);

            // Explicit repeated disposal retains the established Component event
            // behavior; it does not repeat the failed native operation or hook.
            events.Clear();
            firstDisposal = false;
            control.Dispose();
            Assert.Equal(["disposed"], events);
            Assert.Equal(1, control.DestructionCalls);
            Assert.Equal(1, control.CleanupCalls);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void DispatcherCloseFailureCompletesManagedOwnershipForControlAndForm(bool isForm)
        => OnSta(() =>
        {
            using var components = new Container();
            using Control control = isForm ? new Form() : new Control();
            var child = new Control { Parent = control };
            components.Add(control);
            control.CreateControl();
            nint handle = control.Handle;
            nint childHandle = child.Handle;
            int called = 0;
            int disposed = 0;
            control.Disposed += (_, _) => disposed++;
            IAsyncResult pending = control.BeginInvoke((Action)(() => called++));
            pending.AsyncWaitHandle.Dispose();

            ObjectDisposedException failure = Assert.Throws<ObjectDisposedException>(control.Dispose);

            Assert.Contains("Set", failure.StackTrace);
            Assert.False(IsWindow(handle));
            Assert.False(IsWindow(childHandle));
            Assert.True(control.IsDisposed);
            Assert.True(child.IsDisposed);
            Assert.Null(child.Parent);
            Assert.Null(control.Site);
            Assert.Empty(components.Components.Cast<IComponent>());
            Assert.Equal(1, disposed);
            Assert.Equal(0, called);
            Assert.True(pending.IsCompleted);
            Assert.NotSame(failure, Assert.Throws<ObjectDisposedException>(() => control.EndInvoke(pending)));
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ParentNotificationFailureKeepsTheNormalShortCircuitAndAnyNativeFailure(bool nativeFails)
        => OnSta(() =>
        {
            using var components = new Container();
            using var parent = new Control();
            var control = new FailingControl { Parent = parent };
            var child = new Control { Parent = control };
            components.Add(control);
            parent.CreateControl();
            var nativeFailure = new InvalidOperationException("after native destruction");
            var notificationFailure = new ApplicationException("parent notification");
            control.DestructionFailure = nativeFails ? nativeFailure : null;
            EventHandler fail = (_, _) => throw notificationFailure;
            control.ParentChanged += fail;
            int disposed = 0;
            control.Disposed += (_, _) => disposed++;

            try
            {
                AssertFailures(Record.Exception(control.Dispose), nativeFails
                    ? [nativeFailure, notificationFailure] : [notificationFailure]);
                Assert.Null(control.Parent);
                Assert.Empty(parent.Controls);
                Assert.True(control.IsDisposed);
                Assert.False(control.Disposing);
                Assert.Equal(0, control.CleanupCalls);
                Assert.False(child.IsDisposed);
                Assert.Same(control, child.Parent);
                Assert.Same(components, control.Container);
                Assert.Equal(0, disposed);
            }
            finally
            {
                control.ParentChanged -= fail;
                control.Dispose();
            }

            Assert.True(child.IsDisposed);
            Assert.Null(control.Site);
            Assert.Equal(1, disposed);
        });

    [Theory]
    [InlineData(false, false)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(true, true)]
    public void ChildOverrideFailureKeepsLaterSiblingsAndTheParentSiteForRetry(bool nativeFails, bool afterBase)
        => OnSta(() =>
        {
            using var components = new Container();
            var control = new FailingControl();
            var child = new FailingChild { Parent = control, ThrowAfterBase = afterBase };
            var sibling = new Control { Parent = control };
            components.Add(control);
            components.Add(child);
            control.CreateControl();
            Control.ControlCollection retained = control.Controls;
            var nativeFailure = new InvalidOperationException("after native destruction");
            var childFailure = new ApplicationException("child disposal");
            control.DestructionFailure = nativeFails ? nativeFailure : null;
            child.Failure = childFailure;
            int disposed = 0;
            control.Disposed += (_, _) => disposed++;

            try
            {
                AssertFailures(Record.Exception(control.Dispose), nativeFails
                    ? [nativeFailure, childFailure] : [childFailure]);
                Assert.True(control.IsDisposed);
                Assert.Equal(afterBase, child.IsDisposed);
                Assert.Null(child.Parent);
                Assert.False(sibling.IsDisposed);
                Assert.Same(control, sibling.Parent);
                Assert.Same(retained, control.Controls);
                Assert.Equal(2, retained.Count);
                Assert.Same(components, control.Container);
                Assert.Equal(!afterBase, child.Site is not null);
                Assert.Equal(0, disposed);
            }
            finally
            {
                child.Failure = null;
                control.Dispose();
            }

            Assert.True(child.IsDisposed);
            Assert.True(sibling.IsDisposed);
            Assert.Null(control.Site);
            Assert.Equal(1, disposed);
        });

    [Theory]
    [InlineData(false, false)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(true, true)]
    public void ContainerRemovalFailurePreservesComponentRemovalAndEventBoundaries(bool nativeFails, bool afterBase)
        => OnSta(() =>
        {
            using var components = new FailingContainer { ThrowAfterBase = afterBase };
            var control = new FailingControl();
            var child = new Control { Parent = control };
            components.Add(control);
            control.CreateControl();
            var nativeFailure = new InvalidOperationException("after native destruction");
            var removalFailure = new ApplicationException("container removal");
            control.DestructionFailure = nativeFails ? nativeFailure : null;
            components.Failure = removalFailure;
            int disposed = 0;
            control.Disposed += (_, _) => disposed++;

            try
            {
                AssertFailures(Record.Exception(control.Dispose), nativeFails
                    ? [nativeFailure, removalFailure] : [removalFailure]);
                Assert.True(control.IsDisposed);
                Assert.False(control.Disposing);
                Assert.True(child.IsDisposed);
                Assert.Empty(control.Controls);
                Assert.Equal(!afterBase, control.Site is not null);
                Assert.Equal(afterBase ? 0 : 1, components.Components.Count);
                Assert.Equal(0, disposed);
            }
            finally
            {
                components.Failure = null;
                control.Dispose();
            }

            Assert.Null(control.Site);
            Assert.Empty(components.Components.Cast<IComponent>());
            Assert.Equal(1, disposed);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void DisposedFailurePreservesTheNativeErrorWithoutInvokingLaterEventHandlers(bool nativeFails)
        => OnSta(() =>
        {
            using var components = new Container();
            var control = new FailingControl();
            var child = new Control { Parent = control };
            components.Add(control);
            control.CreateControl();
            var nativeFailure = new InvalidOperationException("after native destruction");
            var eventFailure = new ApplicationException("disposed handler");
            control.DestructionFailure = nativeFails ? nativeFailure : null;
            EventHandler fail = (_, _) => throw eventFailure;
            control.Disposed += fail;
            int laterHandlerCalls = 0;
            control.Disposed += (_, _) => laterHandlerCalls++;

            try
            {
                AssertFailures(Record.Exception(control.Dispose), nativeFails
                    ? [nativeFailure, eventFailure] : [eventFailure]);
                Assert.True(control.IsDisposed);
                Assert.False(control.Disposing);
                Assert.True(child.IsDisposed);
                Assert.Null(control.Site);
                Assert.Empty(control.Controls);
                Assert.Equal(0, laterHandlerCalls);
            }
            finally
            {
                control.Disposed -= fail;
                control.Dispose();
            }

            Assert.Equal(1, laterHandlerCalls);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void NativeHookAndComponentFailuresArePreservedWithoutChangingTheHookCleanupBoundary(bool componentFails)
        => OnSta(() =>
        {
            using var components = new Container();
            var control = new FailingControl();
            var child = new Control { Parent = control };
            components.Add(control);
            control.CreateControl();
            var nativeFailure = new InvalidOperationException("after native destruction");
            var hookFailure = new ApplicationException("cleanup hook");
            var eventFailure = new NotSupportedException("disposed handler");
            control.DestructionFailure = nativeFailure;
            control.CleanupAction = () => throw hookFailure;
            int disposed = 0;
            EventHandler disposedHandler = (_, _) =>
            {
                disposed++;
                if (componentFails) { throw eventFailure; }
            };
            control.Disposed += disposedHandler;

            try
            {
                AssertFailures(Record.Exception(control.Dispose), componentFails
                    ? [nativeFailure, hookFailure, eventFailure] : [nativeFailure, hookFailure]);
                Assert.True(control.IsDisposed);
                Assert.False(control.Disposing);
                Assert.False(child.IsDisposed);
                Assert.Same(control, child.Parent);
                Assert.Null(control.Site);
                Assert.Equal(1, disposed);
                Assert.Equal(1, control.CleanupCalls);
            }
            finally
            {
                control.Disposed -= disposedHandler;
                control.Dispose();
            }

            Assert.True(child.IsDisposed);
            Assert.Equal(1, control.CleanupCalls);
        });

    [Fact]
    public void WithoutNativeFailureTheHistoricalHookAndComponentExceptionPrecedenceIsUnchanged()
    {
        using var components = new Container();
        var control = new FailingControl();
        components.Add(control);
        var hookFailure = new ApplicationException("cleanup hook");
        var eventFailure = new NotSupportedException("disposed handler");
        control.CleanupAction = () => throw hookFailure;
        EventHandler fail = (_, _) => throw eventFailure;
        control.Disposed += fail;

        try
        {
            Assert.Same(eventFailure, Assert.Throws<NotSupportedException>(control.Dispose));
            Assert.True(control.IsDisposed);
            Assert.Null(control.Site);
        }
        finally { control.Disposed -= fail; }
    }

    [Fact]
    public void ADestructionFailureThatLeavesTheWindowAlivePreservesOwnershipForRetry()
        => OnSta(() =>
        {
            using var components = new Container();
            using var parent = new Control();
            var control = new FailingControl { Parent = parent, ThrowBeforeDestroy = true };
            var child = new Control { Parent = control };
            components.Add(control);
            parent.CreateControl();
            nint handle = control.Handle;
            var failure = new InvalidOperationException("before native destruction");
            control.DestructionFailure = failure;
            int disposed = 0;
            control.Disposed += (_, _) => disposed++;

            try
            {
                Assert.Same(failure, Assert.Throws<InvalidOperationException>(control.Dispose));
                Assert.True(IsWindow(handle));
                Assert.Same(control, Control.FromHandle(handle));
                Assert.True(control.IsDisposed);
                Assert.False(control.Disposing);
                Assert.Same(parent, control.Parent);
                Assert.Same(control, child.Parent);
                Assert.False(child.IsDisposed);
                Assert.Same(components, control.Container);
                Assert.Equal(0, disposed);
                Assert.Equal(0, control.CleanupCalls);
                Assert.Equal(1, control.DestructionCalls);
            }
            finally
            {
                control.DestructionFailure = null;
                control.Dispose();
            }

            Assert.False(IsWindow(handle));
            Assert.True(child.IsDisposed);
            Assert.Null(control.Site);
            Assert.Null(control.Parent);
            Assert.Equal(1, disposed);
            Assert.Equal(2, control.DestructionCalls);
        });

    [Fact]
    public void DisposeFalseDoesNotRunTheManagedContinuationAfterNativeDestructionFailure()
        => OnSta(() =>
        {
            using var components = new Container();
            using var parent = new Control();
            var control = new FailingControl { Parent = parent };
            var child = new Control { Parent = control };
            components.Add(control);
            parent.CreateControl();
            nint handle = control.Handle;
            var failure = new InvalidOperationException("after native destruction");
            control.DestructionFailure = failure;
            int disposed = 0;
            control.Disposed += (_, _) => disposed++;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(control.ReleaseUnmanaged));

            Assert.False(IsWindow(handle));
            Assert.False(control.IsDisposed);
            Assert.False(control.Disposing);
            Assert.Same(parent, control.Parent);
            Assert.Same(control, child.Parent);
            Assert.False(child.IsDisposed);
            Assert.Same(components, control.Container);
            Assert.Equal(0, disposed);
            Assert.Equal(0, control.CleanupCalls);
            control.Dispose();
            Assert.True(child.IsDisposed);
            Assert.Null(control.Site);
            Assert.Equal(1, disposed);
            Assert.Equal(1, control.CleanupCalls);
        });

    private static void AssertFailures(Exception? actual, Exception[] expected)
    {
        if (expected.Length == 1)
        {
            Assert.Same(expected[0], actual);
            return;
        }

        AggregateException aggregate = Assert.IsType<AggregateException>(actual);
        Assert.Equal(expected.Length, aggregate.InnerExceptions.Count);
        for (int index = 0; index < expected.Length; index++)
        {
            Assert.Same(expected[index], aggregate.InnerExceptions[index]);
        }
    }

    private sealed class FailingControl : Control
    {
        internal Exception? DestructionFailure { get; set; }
        internal bool ThrowBeforeDestroy { get; set; }
        internal int DestructionCalls { get; private set; }
        internal int CleanupCalls { get; private set; }
        internal Action? CleanupAction { get; set; }
        internal void ReleaseUnmanaged() => Dispose(false);

        protected override void DestroyHandle()
        {
            DestructionCalls++;
            if (ThrowBeforeDestroy && DestructionFailure is not null) { throw DestructionFailure; }
            base.DestroyHandle();
            if (DestructionFailure is not null) { throw DestructionFailure; }
        }

        protected override void OnDisposing()
        {
            CleanupCalls++;
            CleanupAction?.Invoke();
            base.OnDisposing();
        }
    }

    private sealed class FailingChild : Control
    {
        internal Exception? Failure { get; set; }
        internal bool ThrowAfterBase { get; set; }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !ThrowAfterBase && Failure is not null) { throw Failure; }
            base.Dispose(disposing);
            if (disposing && ThrowAfterBase && Failure is not null) { throw Failure; }
        }
    }

    private sealed class FailingContainer : Container
    {
        internal Exception? Failure { get; set; }
        internal bool ThrowAfterBase { get; set; }

        public override void Remove(IComponent? component)
        {
            if (!ThrowAfterBase && Failure is not null) { throw Failure; }
            base.Remove(component);
            if (ThrowAfterBase && Failure is not null) { throw Failure; }
        }
    }

    private static void OnSta(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            try { action(); }
            catch (Exception exception) { failure = exception; }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(15)), "The disposal scenario did not finish.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint handle);
}
