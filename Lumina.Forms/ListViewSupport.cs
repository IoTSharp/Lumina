using System.Collections;

namespace Lumina.Forms;

public enum View
{
    LargeIcon = 0,
    Details = 1,
    SmallIcon = 2,
    List = 3,
    Tile = 4
}

public sealed class ColumnHeader
{
    private string _text = string.Empty;
    private int _width = 60;
    private HorizontalAlignment _textAlign;

    public string Text
    {
        get => _text;
        set { _text = value ?? string.Empty; ListView?.SynchronizeColumns(); }
    }

    public int Width
    {
        get => _width;
        set { _width = value; ListView?.SynchronizeColumns(); }
    }

    public HorizontalAlignment TextAlign
    {
        get => _textAlign;
        set
        {
            if ((uint)value > (uint)HorizontalAlignment.Center)
            {
                throw new System.ComponentModel.InvalidEnumArgumentException(nameof(value), (int)value, typeof(HorizontalAlignment));
            }
            _textAlign = value;
            ListView?.SynchronizeColumns();
        }
    }

    public string Name { get; set; } = string.Empty;
    public object? Tag { get; set; }
    public ListView? ListView { get; internal set; }
    public int Index => ListView?.Columns.IndexOf(this) ?? -1;
}

public sealed class ColumnHeaderCollection : IEnumerable<ColumnHeader>
{
    private readonly List<ColumnHeader> _headers = [];
    private readonly ListView? _owner;

    public ColumnHeaderCollection() { }
    internal ColumnHeaderCollection(ListView owner) => _owner = owner;
    public int Count => _headers.Count;
    public ColumnHeader this[int index] => _headers[index];

    public void Clear()
    {
        foreach (ColumnHeader header in _headers) { header.ListView = null; }
        _headers.Clear();
        _owner?.SynchronizeColumns();
    }

    public int Add(ColumnHeader header)
    {
        Insert(_headers.Count, header);
        return _headers.Count - 1;
    }

    public ColumnHeader Add(string text) => Add(text, 60, HorizontalAlignment.Left);
    public ColumnHeader Add(string text, int width) => Add(text, width, HorizontalAlignment.Left);
    public ColumnHeader Add(string text, int width, HorizontalAlignment alignment)
    {
        var header = new ColumnHeader { Text = text, Width = width, TextAlign = alignment };
        Add(header);
        return header;
    }

    public void AddRange(ColumnHeader[] values)
    {
        ArgumentNullException.ThrowIfNull(values);
        foreach (ColumnHeader value in values) { Add(value); }
    }

    public void Insert(int index, ColumnHeader header)
    {
        if ((uint)index > (uint)_headers.Count) { throw new ArgumentOutOfRangeException(nameof(index)); }
        ArgumentNullException.ThrowIfNull(header);
        if (header.ListView is not null || Contains(header))
        {
            throw new ArgumentException("The column already belongs to a ListView.", nameof(header));
        }
        _headers.Insert(index, header);
        header.ListView = _owner;
        _owner?.SynchronizeColumns();
    }

    public bool Contains(ColumnHeader header) => IndexOf(header) >= 0;
    public int IndexOf(ColumnHeader header)
    {
        for (int index = 0; index < _headers.Count; index++)
        {
            if (ReferenceEquals(_headers[index], header)) { return index; }
        }
        return -1;
    }

    public void Remove(ColumnHeader header)
    {
        int index = IndexOf(header);
        if (index >= 0) { RemoveAt(index); }
    }

    public void RemoveAt(int index)
    {
        ColumnHeader header = _headers[index];
        _headers.RemoveAt(index);
        header.ListView = null;
        _owner?.SynchronizeColumns();
    }

    public IEnumerator<ColumnHeader> GetEnumerator() => _headers.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public sealed class ListViewItemCollection : IEnumerable<ListViewItem>
{
    private readonly List<ListViewItem> _items = [];
    private readonly ListView? _owner;

    public ListViewItemCollection() { }
    internal ListViewItemCollection(ListView owner) => _owner = owner;
    public int Count => _items.Count;
    public ListViewItem this[int index]
    {
        get => _items[index];
        set
        {
            ListViewItem old = _items[index];
            if (ReferenceEquals(value, old)) { return; }
            ValidateItem(value);
            old.AttachToListView(null);
            _items[index] = value;
            value.AttachToListView(_owner);
            _owner?.SynchronizeItems();
        }
    }

    public ListViewItem? this[string? key]
    {
        get { int index = IndexOfKey(key); return index < 0 ? null : _items[index]; }
    }

    public void Add(ListViewItem item)
    {
        Insert(_items.Count, item);
    }

    public ListViewItem Add(string? text)
    {
        var item = new ListViewItem(text);
        Add(item);
        return item;
    }
    public void AddRange(ListViewItem[] items)
    {
        ArgumentNullException.ThrowIfNull(items);
        foreach (ListViewItem item in items) { Add(item); }
    }

    public ListViewItem Insert(int index, ListViewItem item)
    {
        if ((uint)index > (uint)_items.Count) { throw new ArgumentOutOfRangeException(nameof(index)); }
        ValidateItem(item);
        _items.Insert(index, item);
        item.AttachToListView(_owner);
        _owner?.SynchronizeItems();
        return item;
    }

    public void Clear()
    {
        foreach (ListViewItem item in _items) { item.AttachToListView(null); }
        _items.Clear();
        _owner?.SynchronizeItems();
    }

    public bool Contains(ListViewItem item) => IndexOf(item) >= 0;
    public int IndexOf(ListViewItem item)
    {
        for (int index = 0; index < _items.Count; index++)
        {
            if (ReferenceEquals(_items[index], item)) { return index; }
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

    public void Remove(ListViewItem item)
    {
        int index = IndexOf(item);
        if (index >= 0) { RemoveAt(index); }
    }

    public void RemoveByKey(string? key)
    {
        int index = IndexOfKey(key);
        if (index >= 0) { RemoveAt(index); }
    }

    public void RemoveAt(int index)
    {
        ListViewItem item = _items[index];
        _items.RemoveAt(index);
        item.AttachToListView(null);
        _owner?.SynchronizeItems();
    }

    private void ValidateItem(ListViewItem item)
    {
        ArgumentNullException.ThrowIfNull(item);
        if (item.ListView is not null || Contains(item))
        {
            throw new ArgumentException("The item already belongs to a ListView.", nameof(item));
        }
    }

    public IEnumerator<ListViewItem> GetEnumerator() => _items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
