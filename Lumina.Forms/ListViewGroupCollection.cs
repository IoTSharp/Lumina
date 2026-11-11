using System.Collections;

namespace Lumina.Forms;

public class ListViewGroupCollection : IList, IEnumerable<ListViewGroup>
{
    private readonly List<ListViewGroup> _groups = [];
    private readonly ListView? _owner;

    public ListViewGroupCollection() { }
    internal ListViewGroupCollection(ListView owner) => _owner = owner;

    public int Count => _groups.Count;
    public ListViewGroup this[int index]
    {
        get => _groups[index];
        set
        {
            ListViewGroup old = _groups[index];
            if (ReferenceEquals(old, value)) return;
            ArgumentNullException.ThrowIfNull(value);
            Validate(value);
            old.AttachToListView(null);
            _groups[index] = value;
            value.AttachToListView(_owner);
            _owner?.SynchronizeGroups();
            _owner?.SynchronizeItems();
        }
    }

    public ListViewGroup? this[string key]
    {
        get
        {
            int index = IndexOfKey(key);
            return index < 0 ? null : _groups[index];
        }
        set
        {
            int index = IndexOfKey(key);
            if (index < 0) throw new KeyNotFoundException(key);
            this[index] = value ?? throw new ArgumentNullException(nameof(value));
        }
    }

    public int Add(ListViewGroup group)
    {
        int existing = IndexOf(group);
        if (existing >= 0) return existing;
        Insert(_groups.Count, group);
        return _groups.Count - 1;
    }

    public ListViewGroup Add(string key, string headerText)
    {
        ListViewGroup group = new(key, headerText);
        Add(group);
        return group;
    }

    public void AddRange(ListViewGroup[] groups)
    {
        ArgumentNullException.ThrowIfNull(groups);
        for (int index = 0; index < groups.Length; index++) Add(groups[index]);
    }

    public void AddRange(ListViewGroupCollection groups)
    {
        ArgumentNullException.ThrowIfNull(groups);
        foreach (ListViewGroup group in groups.ToArray()) Add(group);
    }

    public void Insert(int index, ListViewGroup group)
    {
        if ((uint)index > (uint)_groups.Count) throw new ArgumentOutOfRangeException(nameof(index));
        ArgumentNullException.ThrowIfNull(group);
        Validate(group);
        _groups.Insert(index, group);
        group.AttachToListView(_owner);
        _owner?.SynchronizeGroups();
        _owner?.SynchronizeItems();
    }

    public void Clear()
    {
        for (int index = 0; index < _groups.Count; index++) _groups[index].AttachToListView(null);
        _groups.Clear();
        _owner?.SynchronizeGroups();
        _owner?.SynchronizeItems();
    }

    public bool Contains(ListViewGroup value) => IndexOf(value) >= 0;
    public int IndexOf(ListViewGroup value)
    {
        for (int index = 0; index < _groups.Count; index++) if (ReferenceEquals(_groups[index], value)) return index;
        return -1;
    }

    public int IndexOfKey(string key)
    {
        if (string.IsNullOrEmpty(key)) return -1;
        for (int index = 0; index < _groups.Count; index++)
            if (string.Equals(_groups[index].Name, key, StringComparison.InvariantCultureIgnoreCase)) return index;
        return -1;
    }

    public void Remove(ListViewGroup group)
    {
        int index = IndexOf(group);
        if (index >= 0) RemoveAt(index);
    }

    public void RemoveAt(int index)
    {
        ListViewGroup group = _groups[index];
        _groups.RemoveAt(index);
        group.AttachToListView(null);
        _owner?.SynchronizeGroups();
        _owner?.SynchronizeItems();
    }

    public void CopyTo(Array array, int index) => _groups.ToArray().CopyTo(array, index);
    public IEnumerator GetEnumerator() => _groups.GetEnumerator();
    IEnumerator<ListViewGroup> IEnumerable<ListViewGroup>.GetEnumerator() => _groups.GetEnumerator();
    bool IList.IsReadOnly => false;
    bool IList.IsFixedSize => false;
    bool ICollection.IsSynchronized => false;
    object ICollection.SyncRoot => this;
    object? IList.this[int index] { get => this[index]; set => this[index] = RequireGroup(value); }
    int IList.Add(object? value) => Add(RequireGroup(value));
    void IList.Insert(int index, object? value) => Insert(index, RequireGroup(value));
    bool IList.Contains(object? value) => value is ListViewGroup group && Contains(group);
    int IList.IndexOf(object? value) => value is ListViewGroup group ? IndexOf(group) : -1;
    void IList.Remove(object? value) { if (value is ListViewGroup group) Remove(group); }

    private static ListViewGroup RequireGroup(object? value)
        => value as ListViewGroup ?? throw new ArgumentException("The value must be a ListViewGroup.", nameof(value));

    private void Validate(ListViewGroup group)
    {
        if (group.ListView is not null || Contains(group)) throw new ArgumentException("The group already belongs to a ListView.", nameof(group));
        foreach (ListViewItem item in group.Items)
        {
            if (item.ListView is { } owner && !ReferenceEquals(owner, _owner))
                throw new ArgumentException("An item in the group belongs to another ListView.", nameof(group));
        }
    }
}
