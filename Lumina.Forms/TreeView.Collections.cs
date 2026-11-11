using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Lumina.Forms;

public partial class TreeView
{
    private readonly Dictionary<nint, TreeNode> _nodesByHandle = [];
    private string _pathSeparator = "\\";
    private bool _showLines = true;
    private bool _showRootLines = true;
    private bool _showPlusMinus = true;
    private bool _hideSelection = true;
    private bool _hotTracking;

    [AllowNull]
    [DefaultValue("\\")]
    public string PathSeparator
    {
        get => _pathSeparator;
        set => _pathSeparator = value ?? string.Empty;
    }

    [DefaultValue(true)]
    public bool ShowLines
    {
        get => _showLines;
        set { _showLines = value; UpdateStyles(); }
    }

    [DefaultValue(true)]
    public bool ShowRootLines
    {
        get => _showRootLines;
        set { _showRootLines = value; UpdateStyles(); }
    }

    [DefaultValue(true)]
    public bool ShowPlusMinus
    {
        get => _showPlusMinus;
        set { _showPlusMinus = value; UpdateStyles(); }
    }

    [DefaultValue(true)]
    public bool HideSelection
    {
        get => _hideSelection;
        set { _hideSelection = value; UpdateStyles(); }
    }

    [DefaultValue(false)]
    public bool HotTracking
    {
        get => _hotTracking;
        set { _hotTracking = value; UpdateStyles(); }
    }

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

    protected override void OnHandleDestroyed(EventArgs e)
    {
        foreach (TreeNode node in _nodesByHandle.Values) { node.NativeHandle = 0; }
        _nodesByHandle.Clear();
        base.OnHandleDestroyed(e);
    }

    internal TreeNode? NodeFromHandle(nint handle)
        => _nodesByHandle.GetValueOrDefault(handle);

    private void RealizeNodes()
    {
        for (int index = 0; index < Nodes.Count; index++) { InsertNativeNode(Nodes[index]); }
    }

    internal unsafe void InsertNativeNode(TreeNode node)
    {
        nint handle = NativeHandle;
        if (handle == 0) { return; }
        Stack<TreeNode> pending = new();
        pending.Push(node);
        while (pending.TryPop(out TreeNode? current))
        {
            if (current.NativeHandle == 0)
            {
                string text = current.Text;
                fixed (char* pointer = text)
                {
                    Win32.TVINSERTSTRUCTW native = new()
                    {
                        hParent = current.Parent?.NativeHandle ?? Win32.TVI_ROOT,
                        hInsertAfter = current.PrevNode?.NativeHandle ?? unchecked((nint)(-65535)),
                        item = new Win32.TVITEMW { mask = Win32.TVIF_TEXT, pszText = (nint)pointer },
                    };
                    nint result = Win32.SendMessageW(handle, Win32.TVM_INSERTITEMW, 0, (nint)(&native));
                    if (NativeHandle != handle || IsDisposed) { return; }
                    if (result == 0)
                    {
                        throw new InvalidOperationException("The native tree view could not insert a node.");
                    }
                    if (!ReferenceEquals(current.TreeView, this))
                    {
                        if (Win32.SendMessageW(handle, Win32.TVM_DELETEITEM, 0, result) == 0)
                        {
                            throw new InvalidOperationException("The native tree view could not remove a node detached during insertion.");
                        }
                        continue;
                    }
                    current.NativeHandle = result;
                    _nodesByHandle.Add(result, current);
                }
            }
            for (int index = current.Nodes.Count - 1; index >= 0; index--) { pending.Push(current.Nodes[index]); }
        }
    }

    internal unsafe void UpdateNode(TreeNode node)
    {
        if (NativeHandle == 0 || node.NativeHandle == 0) { return; }
        string text = node.Text;
        fixed (char* pointer = text)
        {
            Win32.TVITEMW native = new()
            {
                mask = Win32.TVIF_TEXT,
                hItem = node.NativeHandle,
                pszText = (nint)pointer,
            };
            if (Win32.SendMessageW(NativeHandle, Win32.TVM_SETITEMW, 0, (nint)(&native)) == 0)
            {
                throw new InvalidOperationException("The native tree view could not update a node.");
            }
        }
    }

    internal void RemoveNativeNode(TreeNode node)
    {
        if (NativeHandle != 0 && node.NativeHandle != 0
            && Win32.SendMessageW(NativeHandle, Win32.TVM_DELETEITEM, 0, node.NativeHandle) == 0)
        {
            throw new InvalidOperationException("The native tree view could not remove a node.");
        }
        ForgetNativeNode(node);
    }

    internal void ForgetNativeNode(TreeNode node)
    {
        Stack<TreeNode> pending = new();
        pending.Push(node);
        while (pending.TryPop(out TreeNode? current))
        {
            _nodesByHandle.Remove(current.NativeHandle);
            current.NativeHandle = 0;
            foreach (TreeNode child in current.Nodes) { pending.Push(child); }
        }
    }
}
