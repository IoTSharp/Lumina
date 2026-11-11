using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Lumina.Forms;

/// <summary>
/// Represents a node in a <see cref="TreeView"/>.
/// </summary>
public class TreeNode : MarshalByRefObject
{
    private string _text = string.Empty;
    private string _name = string.Empty;
    internal TreeNodeCollection? ContainingCollection { get; set; }
    internal nint NativeHandle { get; set; }

    public TreeNode() : this(string.Empty) { }
    public TreeNode(string? text)
    {
        _text = text ?? string.Empty;
        Nodes = new TreeNodeCollection(this);
    }

    public TreeNode(string? text, TreeNode[] children) : this(text) => Nodes.AddRange(children);

    [AllowNull]
    public string Name
    {
        get => _name;
        set => _name = value ?? string.Empty;
    }

    [AllowNull]
    public string Text
    {
        get => _text;
        set
        {
            _text = value ?? string.Empty;
            TreeView?.UpdateNode(this);
        }
    }

    public object? Tag { get; set; }
    public TreeNodeCollection Nodes { get; }
    [Browsable(false)]
    public TreeNode? Parent => ContainingCollection?.ParentNode;
    [Browsable(false)]
    public TreeView? TreeView => ContainingCollection?.OwnerTree;
    [Browsable(false)]
    public int Index => ContainingCollection?.IndexOf(this) ?? 0;
    [Browsable(false)]
    public int Level
    {
        get
        {
            int level = 0;
            for (TreeNode? node = Parent; node is not null; node = node.Parent) { level++; }
            return level;
        }
    }

    [Browsable(false)]
    public TreeNode? FirstNode => Nodes.Count == 0 ? null : Nodes[0];
    [Browsable(false)]
    public TreeNode? LastNode => Nodes.Count == 0 ? null : Nodes[Nodes.Count - 1];
    [Browsable(false)]
    public TreeNode? NextNode => ContainingCollection is { } collection && Index + 1 < collection.Count ? collection[Index + 1] : null;
    [Browsable(false)]
    public TreeNode? PrevNode => ContainingCollection is { } collection && Index > 0 ? collection[Index - 1] : null;
    [Browsable(false)]
    public string FullPath
    {
        get
        {
            TreeView? tree = TreeView;
            if (tree is null) { throw new InvalidOperationException("The node is not associated with a TreeView."); }
            List<string> segments = [];
            for (TreeNode? node = this; node is not null; node = node.Parent) { segments.Add(node.Text); }
            segments.Reverse();
            return string.Join(tree.PathSeparator, segments);
        }
    }

    [Browsable(false)]
    public nint Handle
    {
        get
        {
            TreeView? tree = TreeView;
            if (tree is not null) { _ = tree.Handle; }
            return NativeHandle;
        }
    }

    public void Remove() => ContainingCollection?.Remove(this);
    public int GetNodeCount(bool includeSubTrees)
    {
        if (!includeSubTrees) { return Nodes.Count; }
        int count = 0;
        Stack<TreeNode> pending = new(Nodes);
        while (pending.TryPop(out TreeNode? node))
        {
            count++;
            foreach (TreeNode child in node.Nodes) { pending.Push(child); }
        }
        return count;
    }

    public static TreeNode? FromHandle(TreeView tree, nint handle)
    {
        ArgumentNullException.ThrowIfNull(tree);
        return tree.NodeFromHandle(handle);
    }

    public override string ToString() => $"TreeNode: {Text}";
}

/// <summary>
/// Represents the nodes belonging to one parent or one tree view.
/// </summary>
public sealed class TreeNodeCollection : IEnumerable<TreeNode>
{
    private readonly List<TreeNode> _items = [];
    private readonly TreeView? _tree;
    internal TreeNode? ParentNode { get; }
    internal TreeView? OwnerTree => ParentNode?.TreeView ?? _tree;

    // Retain the historical standalone collection constructor.
    public TreeNodeCollection() { }
    internal TreeNodeCollection(TreeView tree) => _tree = tree;
    internal TreeNodeCollection(TreeNode parent) => ParentNode = parent;

    public int Count => _items.Count;
    public TreeNode this[int index]
    {
        get => _items[index];
        set
        {
            TreeNode previous = _items[index];
            if (ReferenceEquals(previous, value)) { return; }
            ValidateNode(value);
            TreeView? tree = OwnerTree;
            _items[index] = value;
            value.ContainingCollection = this;
            try
            {
                // Keep the old native subtree until the replacement is fully
                // realized, so a failed child insertion can restore its owner.
                tree?.InsertNativeNode(value);
                tree?.RemoveNativeNode(previous);
                previous.ContainingCollection = null;
            }
            catch (Exception failure)
            {
                Exception? cleanupFailure = null;
                try { tree?.RemoveNativeNode(value); }
                catch (Exception exception) { cleanupFailure = exception; }
                finally
                {
                    tree?.ForgetNativeNode(value);
                    int replacementIndex = IndexOf(value);
                    if (replacementIndex >= 0)
                    {
                        _items[replacementIndex] = previous;
                        previous.ContainingCollection = this;
                    }
                    if (ReferenceEquals(value.ContainingCollection, this)) { value.ContainingCollection = null; }
                }
                if (cleanupFailure is not null)
                {
                    throw new AggregateException("The node replacement and its native rollback both failed.", failure, cleanupFailure);
                }
                throw;
            }
        }
    }

    public TreeNode? this[string? key]
    {
        get { int index = IndexOfKey(key); return index < 0 ? null : _items[index]; }
    }

    public void Add(TreeNode node) => Insert(_items.Count, node);
    public TreeNode Add(string? text)
    {
        var node = new TreeNode(text);
        Add(node);
        return node;
    }

    public TreeNode Add(string? key, string? text)
    {
        var node = new TreeNode(text) { Name = key };
        Add(node);
        return node;
    }

    public void AddRange(IEnumerable<TreeNode> nodes)
    {
        ArgumentNullException.ThrowIfNull(nodes);
        // Materialize first so adding a view of this collection cannot iterate forever.
        TreeNode[] values = nodes.ToArray();
        foreach (TreeNode node in values) { Add(node); }
    }

    public void AddRange(params TreeNode[] nodes) => AddRange((IEnumerable<TreeNode>)nodes);

    public void Insert(int index, TreeNode node)
    {
        if ((uint)index > (uint)_items.Count) { throw new ArgumentOutOfRangeException(nameof(index)); }
        ValidateNode(node);
        _items.Insert(index, node);
        node.ContainingCollection = this;
        TreeView? tree = OwnerTree;
        try { tree?.InsertNativeNode(node); }
        catch (Exception failure)
        {
            Exception? cleanupFailure = null;
            try { tree?.RemoveNativeNode(node); }
            catch (Exception exception) { cleanupFailure = exception; }
            finally
            {
                tree?.ForgetNativeNode(node);
                int currentIndex = IndexOf(node);
                if (currentIndex >= 0) { _items.RemoveAt(currentIndex); }
                if (ReferenceEquals(node.ContainingCollection, this)) { node.ContainingCollection = null; }
            }
            if (cleanupFailure is not null)
            {
                throw new AggregateException("The node insertion and its native rollback both failed.", failure, cleanupFailure);
            }
            throw;
        }
    }

    private void ValidateNode(TreeNode node)
    {
        ArgumentNullException.ThrowIfNull(node);
        if (node.ContainingCollection is not null)
        {
            throw new ArgumentException("The node already belongs to a node collection.", nameof(node));
        }
        for (TreeNode? ancestor = ParentNode; ancestor is not null; ancestor = ancestor.Parent)
        {
            if (ReferenceEquals(ancestor, node))
            {
                throw new ArgumentException("A node cannot be added below itself.", nameof(node));
            }
        }
    }

    public bool Contains(TreeNode? node) => IndexOf(node) >= 0;
    public int IndexOf(TreeNode? node)
    {
        for (int index = 0; index < _items.Count; index++)
        {
            if (ReferenceEquals(_items[index], node)) { return index; }
        }
        return -1;
    }

    public bool ContainsKey(string? key) => IndexOfKey(key) >= 0;
    public int IndexOfKey(string? key)
    {
        if (string.IsNullOrEmpty(key)) { return -1; }
        for (int index = 0; index < _items.Count; index++)
        {
            if (string.Equals(_items[index].Name, key, StringComparison.InvariantCultureIgnoreCase)) { return index; }
        }
        return -1;
    }

    public void Remove(TreeNode? node)
    {
        int index = IndexOf(node);
        if (index >= 0) { RemoveAt(index); }
    }

    public void RemoveByKey(string? key)
    {
        int index = IndexOfKey(key);
        if (index >= 0) { RemoveAt(index); }
    }

    public void RemoveAt(int index)
    {
        TreeNode node = _items[index];
        OwnerTree?.RemoveNativeNode(node);
        _items.RemoveAt(index);
        node.ContainingCollection = null;
    }

    public void Clear()
    {
        for (int index = _items.Count - 1; index >= 0; index--) { RemoveAt(index); }
    }

    public TreeNode[] Find(string key, bool searchAllChildren)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);
        List<TreeNode> results = [];
        Stack<TreeNode> pending = new(_items.AsEnumerable().Reverse());
        while (pending.TryPop(out TreeNode? node))
        {
            if (string.Equals(node.Name, key, StringComparison.InvariantCultureIgnoreCase)) { results.Add(node); }
            if (searchAllChildren)
            {
                for (int index = node.Nodes.Count - 1; index >= 0; index--) { pending.Push(node.Nodes[index]); }
            }
        }
        return results.ToArray();
    }

    public void CopyTo(Array array, int index) => ((ICollection)_items).CopyTo(array, index);
    public IEnumerator<TreeNode> GetEnumerator() => _items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
