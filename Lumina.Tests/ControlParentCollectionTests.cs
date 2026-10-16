using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public class ControlParentCollectionTests
{
    [Fact]
    public void ParentAssignmentUpdatesBothCollectionsAndRaisesOrderedNotifications()
    {
        using var first = new Control { Name = "first" };
        using var second = new Control { Name = "second" };
        using var child = new Control();
        List<string> calls = [];
        child.ParentChanged += (_, _) => calls.Add($"parent:{child.Parent?.Name ?? "null"}");
        first.ControlAdded += (_, e) => { Assert.Same(child, e.Control); calls.Add("first:add"); };
        first.ControlRemoved += (_, e) => { Assert.Same(child, e.Control); calls.Add("first:remove"); };
        second.ControlAdded += (_, _) => calls.Add("second:add");

        child.Parent = first;
        child.Parent = second;
        child.Parent = second;

        Assert.Equal(["parent:first", "first:add", "parent:null", "first:remove", "parent:second", "second:add"], calls);
        Assert.Empty(first.Controls);
        Assert.Same(child, Assert.Single(second.Controls));
        Assert.Same(second, child.Parent);
    }

    [Fact]
    public void EveryControlCanOwnDescendantsAndFindItsForm()
    {
        using var form = new Form();
        var outer = new Control();
        var inner = new Control();
        form.Controls.Add(outer);
        outer.Controls.Add(inner);

        Assert.Same(form, outer.Parent);
        Assert.Same(outer, inner.Parent);
        Assert.True(form.Contains(inner));
        Assert.True(outer.Contains(inner));
        Assert.False(inner.Contains(outer));
        Assert.False(outer.Contains(outer));
        Assert.False(outer.Contains(null));
        Assert.Same(form, inner.FindForm());
        Assert.Same(form, form.FindForm());
        Assert.Same(form, inner.TopLevelControl);
        Assert.Same(form, form.TopLevelControl);
        Assert.True(form.HasChildren);
        Assert.False(inner.HasChildren);

        outer.Parent = null;
        Assert.Null(inner.FindForm());
        Assert.Null(inner.TopLevelControl);
        Assert.Same(outer, inner.Parent);
        outer.Dispose();
    }

    [Fact]
    public void CyclesAndTopLevelControlsAreRejectedBeforeMutation()
    {
        using var root = new Control();
        using var child = new Control();
        using var form = new Form();
        root.Controls.Add(child);

        Assert.Throws<ArgumentException>(() => child.Controls.Add(root));
        Assert.Throws<ArgumentException>(() => root.Controls.Add(root));
        Assert.Throws<ArgumentException>(() => root.Controls.Add(form));
        Assert.Same(root, child.Parent);
        Assert.Same(child, Assert.Single(root.Controls));
        Assert.Null(root.Parent);
        Assert.Null(form.Parent);
    }

    [Fact]
    public void DuplicateAddMovesChildToBackWhileSameParentAssignmentDoesNot()
    {
        using var owner = new Control();
        var first = new Control();
        var second = new Control();
        owner.Controls.AddRange(first, second);
        int events = 0;
        owner.ControlAdded += (_, _) => events++;
        first.ParentChanged += (_, _) => events++;

        first.Parent = owner;
        Assert.Same(first, owner.Controls[0]);
        owner.Controls.Add(first);

        Assert.Same(second, owner.Controls[0]);
        Assert.Same(first, owner.Controls[1]);
        Assert.Equal(0, events);
    }

    [Fact]
    public void AddRangeKeepsSuccessfullyAddedPrefixWhenALaterItemFails()
    {
        using var owner = new Control();
        using var invalid = new Form();
        var first = new Control();
        using var last = new Control();

        Assert.Throws<ArgumentException>(() => owner.Controls.AddRange(first, null!, invalid, last));

        Assert.Same(first, Assert.Single(owner.Controls));
        Assert.Same(owner, first.Parent);
        Assert.Null(last.Parent);
        Assert.Null(invalid.Parent);
    }

    [Fact]
    public void FindReturnsDirectMatchesBeforeEachNestedBranch()
    {
        using var owner = new Control();
        var first = new Control { Name = "match" };
        var second = new Control { Name = "MATCH" };
        var nested = new Control { Name = "match" };
        var deep = new Control { Name = "match" };
        first.Controls.Add(nested);
        nested.Controls.Add(deep);
        owner.Controls.AddRange(first, second);

        Assert.Equal([first, second], owner.Controls.Find("MATCH", false));
        Assert.Equal([first, second, nested, deep], owner.Controls.Find("MATCH", true));
        Assert.Empty(owner.Controls.Find("missing", true));
    }

    [Fact]
    public void FindRetainsTheCollectedPrefixWhenADerivedIndexerThrowsAnOrdinaryException()
    {
        using var owner = new IndexedFindControl();
        var first = new Control { Name = "match" };
        owner.Controls.AddRange(first, new Control { Name = "match" });
        owner.IndexReads.Clear();
        owner.IndexFailure = new InvalidOperationException("Derived indexer failed.");

        try
        {
            Assert.Equal([first], owner.Controls.Find("match", true));
            Assert.Equal([0, 0, 0, 1], owner.IndexReads);
        }
        finally { owner.IndexFailure = null; }
    }

    [Fact]
    public void FindPropagatesTheOriginalCriticalExceptionFromADerivedIndexer()
    {
        using var owner = new IndexedFindControl();
        owner.Controls.AddRange(new Control { Name = "match" }, new Control { Name = "match" });
        owner.IndexReads.Clear();
        var error = new IndexOutOfRangeException("Critical derived indexer failure.");
        owner.IndexFailure = error;

        try
        {
            Assert.Same(error, Assert.Throws<IndexOutOfRangeException>(() => owner.Controls.Find("match", true)));
            Assert.Equal([0, 0, 0, 1], owner.IndexReads);
        }
        finally { owner.IndexFailure = null; }
    }

    [Fact]
    public void KeyCacheRetainsLastMatchingIndexUntilANonemptyMiss()
    {
        using var owner = new Control();
        var first = new Control { Name = "first" };
        var second = new Control { Name = "second" };
        owner.Controls.AddRange(first, second);
        Assert.Equal(1, owner.Controls.IndexOfKey("SECOND"));
        first.Name = "second";

        Assert.Equal(1, owner.Controls.IndexOfKey("second"));
        Assert.Equal(-1, owner.Controls.IndexOfKey(null));
        Assert.Equal(1, owner.Controls.IndexOfKey("second"));
        Assert.Equal(-1, owner.Controls.IndexOfKey("missing"));
        Assert.Equal(0, owner.Controls.IndexOfKey("second"));
        Assert.Same(first, owner.Controls["SECOND"]);
        Assert.Null(owner.Controls[string.Empty]);
    }

    [Fact]
    public void ListMutationsUseControlOwnershipAndRejectUnsupportedShapes()
    {
        using var owner = new Control();
        using var child = new Control();
        IList list = owner.Controls;

        Assert.Equal(0, list.Add(child));
        Assert.Same(owner, child.Parent);
        Assert.Throws<ArgumentException>(() => list.Add(null));
        Assert.Throws<ArgumentException>(() => list.Add("not a control"));
        Assert.Throws<NotSupportedException>(() => list.Insert(0, child));
        Assert.Throws<NotSupportedException>(() => list[0] = child);
        list.Remove("missing");
        list.RemoveAt(0);

        Assert.Null(child.Parent);
        Assert.Empty(list);
        Assert.False(list.IsReadOnly);
        Assert.False(list.IsFixedSize);
    }

    [Fact]
    public void ClearRemovesChildrenInReverseOrderWithoutDisposingThem()
    {
        using var owner = new Control();
        using var first = new Control { Name = "first" };
        using var second = new Control { Name = "second" };
        owner.Controls.AddRange(first, second);
        List<string> removed = [];
        owner.ControlRemoved += (_, e) => removed.Add(e.Control!.Name);

        owner.Controls.Clear();

        Assert.Equal(["second", "first"], removed);
        Assert.Null(first.Parent);
        Assert.Null(second.Parent);
        Assert.False(first.IsDisposed);
        Assert.False(second.IsDisposed);
        Assert.False(owner.HasChildren);
    }

    [Fact]
    public void SetChildIndexClampsHighIndicesAndKeepsParent()
    {
        using var owner = new Control();
        var first = new Control();
        var second = new Control();
        var third = new Control();
        owner.Controls.AddRange(first, second, third);

        owner.Controls.SetChildIndex(third, 0);
        Assert.Equal([third, first, second], owner.Controls.ToArray());
        owner.Controls.SetChildIndex(third, int.MaxValue);
        Assert.Equal([first, second, third], owner.Controls.ToArray());
        owner.Controls.SetChildIndex(first, -1);
        Assert.Equal([second, third, first], owner.Controls.ToArray());
        Assert.Throws<ArgumentOutOfRangeException>(() => owner.Controls.SetChildIndex(first, -2));
        Assert.Equal(2, owner.Controls.GetChildIndex(first));
        Assert.Same(owner, first.Parent);
    }

    [Fact]
    public void EnumeratorToleratesMutationAndStopsAtItsOriginalCount()
    {
        using var owner = new Control();
        var first = new Control();
        var second = new Control();
        var third = new Control();
        owner.Controls.AddRange(first, second);
        IEnumerator enumerator = owner.Controls.GetEnumerator();
        Assert.Null(enumerator.Current);
        Assert.True(enumerator.MoveNext());
        Assert.Same(first, enumerator.Current);
        owner.Controls.Add(third);
        Assert.True(enumerator.MoveNext());
        Assert.Same(second, enumerator.Current);
        Assert.False(enumerator.MoveNext());
        Assert.Same(second, enumerator.Current);
        enumerator.Reset();
        Assert.Null(enumerator.Current);
    }

    [Fact]
    public void CloneUsesTheFactoryAndCopiesReferencesWithoutReparenting()
    {
        using var owner = new FactoryControl();
        var child = new Control();
        owner.Controls.Add(child);
        Assert.Equal(1, owner.FactoryCalls);

        var clone = Assert.IsAssignableFrom<Control.ControlCollection>(((ICloneable)owner.Controls).Clone());

        Assert.Equal(2, owner.FactoryCalls);
        Assert.NotSame(owner.Controls, clone);
        Assert.Same(owner, clone.Owner);
        Assert.Same(child, clone[0]);
        Assert.Same(owner, child.Parent);
        Assert.True(owner.Controls.Equals(clone));
        Assert.Equal(owner.Controls.GetHashCode(), clone.GetHashCode());
    }

    [Fact]
    public void ParentConstructorAssignsParentBeforeTextAndBounds()
    {
        using var owner = new Control();
        List<(string Text, Rectangle Bounds)> observed = [];
        owner.ControlAdded += (_, e) => observed.Add((e.Control!.Text, e.Control.Bounds));

        using var child = new Control(owner, "text", 3, 4, 50, 60);

        Assert.Equal([(string.Empty, Rectangle.Empty)], observed);
        Assert.Same(owner, child.Parent);
        Assert.Equal("text", child.Text);
        Assert.Equal(new Rectangle(3, 4, 50, 60), child.Bounds);
    }

    [Fact]
    public void DisposeCascadesAndDetachesChildrenWithoutParentChangedNotifications()
    {
        var owner = new Control();
        var child = new Control();
        var grandchild = new Control();
        owner.Controls.Add(child);
        child.Controls.Add(grandchild);
        Control.ControlCollection retained = owner.Controls;
        int parentChanges = 0;
        child.ParentChanged += (_, _) => parentChanges++;
        grandchild.ParentChanged += (_, _) => parentChanges++;

        owner.Dispose();

        Assert.True(owner.IsDisposed);
        Assert.True(child.IsDisposed);
        Assert.True(grandchild.IsDisposed);
        Assert.Null(child.Parent);
        Assert.Null(grandchild.Parent);
        Assert.Equal(0, parentChanges);
        Assert.Empty(owner.Controls);
        Assert.Same(child, retained[0]);
    }

    [Fact]
    public void RepeatedDisposeRemovesAControlThatWasReparentedAfterDisposal()
    {
        using var owner = new Control();
        var child = new LifetimeControl();
        int cleanupCalls = 0;
        int disposedCalls = 0;
        child.DisposingAction = () => cleanupCalls++;
        child.Disposed += (_, _) => disposedCalls++;
        child.Dispose();
        owner.Controls.Add(child);

        child.Dispose();

        Assert.Null(child.Parent);
        Assert.Empty(owner.Controls);
        Assert.Equal(1, cleanupCalls);
        Assert.Equal(2, disposedCalls);
    }

    [Fact]
    public void RepeatedDisposeReleasesChildrenAddedAfterThePreviousDisposal()
    {
        var owner = new LifetimeControl();
        int cleanupCalls = 0;
        owner.DisposingAction = () => cleanupCalls++;
        owner.Dispose();
        var child = new Control();
        owner.Controls.Add(child);

        owner.Dispose();

        Assert.True(child.IsDisposed);
        Assert.Null(child.Parent);
        Assert.Empty(owner.Controls);
        Assert.Equal(1, cleanupCalls);
    }

    [Fact]
    public void DisposeParentNotificationFailurePreservesTheComponentSiteAndSkipsDisposed()
    {
        using var components = new Container();
        using var owner = new Control();
        var child = new Control();
        owner.Controls.Add(child);
        components.Add(child, "child");
        ISite? originalSite = child.Site;
        var error = new InvalidOperationException("Parent notification failed.");
        EventHandler handler = (_, _) => throw error;
        child.ParentChanged += handler;
        int disposedCalls = 0;
        child.Disposed += (_, _) => disposedCalls++;

        try
        {
            Assert.Same(error, Assert.Throws<InvalidOperationException>(child.Dispose));
        }
        finally { child.ParentChanged -= handler; }

        Assert.Null(child.Parent);
        Assert.Empty(owner.Controls);
        Assert.True(child.IsDisposed);
        Assert.False(child.Disposing);
        Assert.Same(originalSite, child.Site);
        Assert.Same(child, components.Components["child"]);
        Assert.Equal(0, disposedCalls);

        child.Dispose();

        Assert.Null(child.Site);
        Assert.Empty(components.Components.Cast<IComponent>());
        Assert.Equal(1, disposedCalls);
    }

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void NativeDisposeDestroysWindowBeforeParentNotificationsAndCleanupHook()
    {
        using var form = new Form();
        var child = new LifetimeControl();
        form.Controls.Add(child);
        form.Show();
        nint handle = child.Handle;
        List<string> notifications = [];
        child.ParentChanged += (_, _) =>
        {
            Assert.Null(child.Parent);
            Assert.False(child.IsHandleCreated);
            Assert.False(IsWindow(handle));
            notifications.Add("parent");
        };
        form.ControlRemoved += (_, _) =>
        {
            Assert.False(child.IsHandleCreated);
            notifications.Add("removed");
        };
        child.DisposingAction = () =>
        {
            Assert.False(child.IsHandleCreated);
            Assert.True(child.Disposing);
            notifications.Add("cleanup");
        };

        child.Dispose();

        Assert.Equal(["parent", "removed", "cleanup"], notifications);
        Assert.True(child.IsDisposed);
    }

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void ParkedWindowIsReleasedWhenTheNewParentCreatesItsWindow()
    {
        using var original = new Form();
        using var destination = new Form();
        var child = new Control();
        var newParent = new Control();
        original.Controls.Add(child);
        destination.Controls.Add(newParent);
        original.Show();
        nint childHandle = child.Handle;

        child.Parent = null;
        nint parkingHandle = GetParent(childHandle);
        Assert.NotEqual(0, parkingHandle);
        Assert.True(IsWindow(parkingHandle));
        child.Parent = newParent;
        Assert.Same(newParent, child.Parent);
        Assert.Equal(parkingHandle, GetParent(childHandle));

        destination.Show();

        Assert.Equal(childHandle, child.Handle);
        Assert.Equal(newParent.Handle, GetParent(childHandle));
        Assert.False(IsWindow(parkingHandle));
    }

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void MovingAWindowFromAHiddenParentRestoresItsEffectiveNativeVisibility()
    {
        using var original = new Form();
        using var destination = new Form();
        var hiddenParent = new Control();
        var visibleParent = new Control();
        var child = new Control();
        original.Controls.Add(hiddenParent);
        destination.Controls.Add(visibleParent);
        hiddenParent.Controls.Add(child);
        original.Show();
        destination.Show();
        nint childHandle = child.Handle;
        int visibleEvents = 0;
        child.VisibleChanged += (_, _) => visibleEvents++;

        hiddenParent.Visible = false;

        Assert.False(child.Visible);
        Assert.False(IsWindowVisible(childHandle));
        Assert.Equal(0, visibleEvents);

        child.Parent = visibleParent;

        Assert.Same(visibleParent, child.Parent);
        Assert.Equal(childHandle, child.Handle);
        Assert.Equal(visibleParent.Handle, GetParent(childHandle));
        Assert.True(child.Visible);
        Assert.True(IsWindowVisible(childHandle));
    }

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void DisposeFalseReleasesNativeTreeWithoutDisposingManagedChildren()
    {
        using var form = new Form();
        var parent = new LifetimeControl();
        var child = new LifetimeControl();
        parent.Controls.Add(child);
        form.Controls.Add(parent);
        form.Show();
        nint parentHandle = parent.Handle;
        nint childHandle = child.Handle;
        int callbacks = 0;
        parent.Disposed += (_, _) => callbacks++;
        child.Disposed += (_, _) => callbacks++;
        parent.DisposingAction = () => callbacks++;
        child.DisposingAction = () => callbacks++;

        parent.ReleaseUnmanaged();

        Assert.False(IsWindow(parentHandle));
        Assert.False(IsWindow(childHandle));
        Assert.False(parent.IsHandleCreated);
        Assert.False(child.IsHandleCreated);
        Assert.False(parent.IsDisposed);
        Assert.False(child.IsDisposed);
        Assert.Same(form, parent.Parent);
        Assert.Same(parent, child.Parent);
        Assert.Same(child, Assert.Single(parent.Controls));
        Assert.Equal(0, callbacks);
    }

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint handle);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindowVisible(nint handle);

    [DllImport("user32.dll")]
    private static extern nint GetParent(nint handle);

    private sealed class LifetimeControl : Control
    {
        public Action? DisposingAction { get; set; }
        public void ReleaseUnmanaged() => Dispose(false);

        protected override void OnDisposing()
        {
            DisposingAction?.Invoke();
            base.OnDisposing();
        }
    }

    private sealed class FactoryControl : Control
    {
        public int FactoryCalls { get; private set; }

        protected override ControlCollection CreateControlsInstance()
        {
            FactoryCalls++;
            return new ControlCollection(this);
        }
    }

    private sealed class IndexedFindControl : Control
    {
        public Exception? IndexFailure { get; set; }
        public List<int> IndexReads { get; } = [];

        protected override ControlCollection CreateControlsInstance() => new IndexedFindCollection(this);

        private sealed class IndexedFindCollection(IndexedFindControl owner) : ControlCollection(owner)
        {
            public override Control this[int index]
            {
                get
                {
                    owner.IndexReads.Add(index);
                    if (index == 1 && owner.IndexFailure is { } error) { throw error; }
                    return base[index];
                }
            }
        }
    }
}
