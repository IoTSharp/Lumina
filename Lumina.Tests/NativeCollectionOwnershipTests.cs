using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public class NativeCollectionOwnershipTests
{
    [Fact]
    public void ListItem_CannotHaveTwoHosts_AndRemoveReleasesOwner()
    {
        using var first = new ListView();
        using var second = new ListView();
        var item = new ListViewItem("row");
        first.Items.Add(item);

        Assert.Same(first, item.ListView);
        Assert.Equal(0, item.Index);
        Assert.Throws<ArgumentException>(() => second.Items.Add(item));
        Assert.Single(first.Items);
        Assert.Empty(second.Items);

        item.Remove();
        second.Items.Add(item);
        Assert.Same(second, item.ListView);
        Assert.Empty(first.Items);
        Assert.False(first.IsHandleCreated);
        Assert.False(second.IsHandleCreated);
    }

    [Fact]
    public void ListItem_ReplacementAndClearReleaseOwnership()
    {
        using var view = new ListView();
        var old = view.Items.Add("old");
        var replacement = new ListViewItem("new");
        view.Items[0] = replacement;

        Assert.Null(old.ListView);
        Assert.Equal(-1, old.Index);
        Assert.Same(view, replacement.ListView);
        view.Items.Clear();
        Assert.Null(replacement.ListView);
    }

    [Fact]
    public void ColumnHeader_CannotHaveTwoHosts_AndClearReleasesOwner()
    {
        using var first = new ListView();
        using var second = new ListView();
        ColumnHeader header = first.Columns.Add("name", 120);
        Assert.Same(first, header.ListView);
        Assert.Throws<ArgumentException>(() => second.Columns.Add(header));
        first.Columns.Clear();
        Assert.Null(header.ListView);
        second.Columns.Add(header);
        Assert.Same(second, header.ListView);
    }

    [Fact]
    public void TreeSubtree_ParentPathAndOwnerFollowAttachment()
    {
        using var tree = new TreeView();
        var child = new TreeNode("child") { Name = "child-key" };
        var root = new TreeNode("root", [child]);
        tree.Nodes.Add(root);

        Assert.Null(root.Parent);
        Assert.Same(root, child.Parent);
        Assert.Same(tree, child.TreeView);
        Assert.Equal(1, child.Level);
        Assert.Equal("root\\child", child.FullPath);
        Assert.Equal(2, tree.GetNodeCount(true));
        Assert.Same(child, Assert.Single(tree.Nodes.Find("CHILD-KEY", true)));

        root.Remove();
        Assert.Null(root.TreeView);
        Assert.Null(child.TreeView);
        Assert.Same(root, child.Parent);
        Assert.False(tree.IsHandleCreated);
    }

    [Fact]
    public void TreeNode_RejectsDuplicateHostAndCyclesWithoutChangingParents()
    {
        var child = new TreeNode("child");
        var parent = new TreeNode("parent", [child]);
        var other = new TreeNode("other");

        Assert.Throws<ArgumentException>(() => other.Nodes.Add(child));
        Assert.Throws<ArgumentException>(() => child.Nodes.Add(parent));
        Assert.Throws<ArgumentException>(() => parent.Nodes.Add(parent));
        Assert.Same(parent, child.Parent);
        Assert.Single(parent.Nodes);
        Assert.Empty(child.Nodes);
        Assert.Empty(other.Nodes);
    }
}
