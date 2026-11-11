using System.Collections;
using System.ComponentModel;

namespace Lumina.Forms;

public enum View
{
    LargeIcon = 0,
    Details = 1,
    SmallIcon = 2,
    List = 3,
    Tile = 4
}

public class ColumnHeader : Component, ICloneable
{
    private string _text = string.Empty;
    private int _width = 60;
    private HorizontalAlignment _textAlign;
    private int _imageIndex = -1;
    private string _imageKey = string.Empty;
    private int _displayIndex = -1;

    public ColumnHeader() { }
    public ColumnHeader(int imageIndex) => ImageIndex = imageIndex;
    public ColumnHeader(string imageKey) => ImageKey = imageKey;

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

    [DefaultValue(-1)]
    public int ImageIndex
    {
        get => _imageIndex;
        set
        {
            if (value < -1) throw new ArgumentOutOfRangeException(nameof(value));
            _imageIndex = value;
            _imageKey = string.Empty;
            ListView?.SynchronizeColumns();
        }
    }

    [DefaultValue("")]
    public string ImageKey
    {
        get => _imageKey;
        set
        {
            _imageKey = value ?? string.Empty;
            _imageIndex = -1;
            ListView?.SynchronizeColumns();
        }
    }

    [Browsable(false)]
    public ImageList? ImageList => ListView?.SmallImageList;

    public int DisplayIndex
    {
        get => _displayIndex >= 0 ? _displayIndex : Index;
        set
        {
            if (value < -1) throw new ArgumentOutOfRangeException(nameof(value));
            _displayIndex = value;
            ListView?.SynchronizeColumns();
        }
    }

    public string Name { get; set; } = string.Empty;
    public object? Tag { get; set; }
    public ListView? ListView { get; internal set; }
    public int Index => ListView?.Columns.IndexOf(this) ?? -1;

    public object Clone()
    {
        ColumnHeader clone = new() { Text = Text, Width = Width, TextAlign = TextAlign, Name = Name, Tag = Tag };
        clone._imageIndex = _imageIndex;
        clone._imageKey = _imageKey;
        clone._displayIndex = _displayIndex;
        return clone;
    }

    public override string ToString() => $"ColumnHeader: Text: {Text}";

    protected override void Dispose(bool disposing)
    {
        if (disposing) { ListView?.Columns.Remove(this); }
        base.Dispose(disposing);
    }
}

public class ColumnHeaderCollection : IList, IEnumerable<ColumnHeader>
{
    private readonly List<ColumnHeader> _headers = [];
    private readonly ListView? _owner;

    public ColumnHeaderCollection() { }
    internal ColumnHeaderCollection(ListView owner) => _owner = owner;
    public int Count => _headers.Count;
    public bool IsReadOnly => false;
    public ColumnHeader this[int index] => _headers[index];
    public ColumnHeader? this[string key]
    {
        get { int index = IndexOfKey(key); return index < 0 ? null : _headers[index]; }
    }

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

    public ColumnHeader Add(string key, string text) => Add(key, text, 60, HorizontalAlignment.Left, -1);
    public ColumnHeader Add(string key, string text, int width) => Add(key, text, width, HorizontalAlignment.Left, -1);
    public ColumnHeader Add(string key, string text, int width, HorizontalAlignment alignment, int imageIndex)
    {
        ColumnHeader header = new(imageIndex) { Name = key ?? string.Empty, Text = text, Width = width, TextAlign = alignment };
        Add(header);
        return header;
    }
    public ColumnHeader Add(string key, string text, int width, HorizontalAlignment alignment, string imageKey)
    {
        ColumnHeader header = new(imageKey) { Name = key ?? string.Empty, Text = text, Width = width, TextAlign = alignment };
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

    public void Insert(int index, string text) => Insert(index, new ColumnHeader { Text = text });
    public void Insert(int index, string text, int width) => Insert(index, new ColumnHeader { Text = text, Width = width });
    public void Insert(int index, string key, string text) => Insert(index, key, text, 60, HorizontalAlignment.Left, -1);
    public void Insert(int index, string key, string text, int width) => Insert(index, key, text, width, HorizontalAlignment.Left, -1);
    public void Insert(int index, string key, string text, int width, HorizontalAlignment alignment, int imageIndex)
        => Insert(index, new ColumnHeader(imageIndex) { Name = key ?? string.Empty, Text = text, Width = width, TextAlign = alignment });
    public void Insert(int index, string key, string text, int width, HorizontalAlignment alignment, string imageKey)
        => Insert(index, new ColumnHeader(imageKey) { Name = key ?? string.Empty, Text = text, Width = width, TextAlign = alignment });

    public bool Contains(ColumnHeader header) => IndexOf(header) >= 0;
    public int IndexOf(ColumnHeader header)
    {
        for (int index = 0; index < _headers.Count; index++)
        {
            if (ReferenceEquals(_headers[index], header)) { return index; }
        }
        return -1;
    }

    public bool ContainsKey(string key) => IndexOfKey(key) >= 0;
    public int IndexOfKey(string key)
    {
        if (string.IsNullOrEmpty(key)) return -1;
        for (int index = 0; index < _headers.Count; index++)
            if (string.Equals(_headers[index].Name, key, StringComparison.InvariantCultureIgnoreCase)) return index;
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

    public void RemoveByKey(string key)
    {
        int index = IndexOfKey(key);
        if (index >= 0) RemoveAt(index);
    }

    public IEnumerator<ColumnHeader> GetEnumerator() => _headers.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    bool IList.IsFixedSize => false;
    bool ICollection.IsSynchronized => false;
    object ICollection.SyncRoot => this;
    object? IList.this[int index] { get => this[index]; set => throw new NotSupportedException("Column replacement is not supported."); }
    int IList.Add(object? value) => Add(value as ColumnHeader ?? throw new ArgumentException("The value must be a ColumnHeader.", nameof(value)));
    void IList.Insert(int index, object? value) => Insert(index, value as ColumnHeader ?? throw new ArgumentException("The value must be a ColumnHeader.", nameof(value)));
    bool IList.Contains(object? value) => value is ColumnHeader header && Contains(header);
    int IList.IndexOf(object? value) => value is ColumnHeader header ? IndexOf(header) : -1;
    void IList.Remove(object? value) { if (value is ColumnHeader header) Remove(header); }
    void ICollection.CopyTo(Array array, int index) => _headers.ToArray().CopyTo(array, index);
}

public class ListViewItemCollection : IList, IEnumerable<ListViewItem>
{
    private readonly List<ListViewItem> _items = [];
    private readonly ListView? _owner;
    private readonly ListViewGroup? _group;

    public ListViewItemCollection() { }
    internal ListViewItemCollection(ListView owner) => _owner = owner;
    internal ListViewItemCollection(ListViewGroup group) => _group = group;
    public bool IsReadOnly => false;
    public int Count => _items.Count;
    public ListViewItem this[int index]
    {
        get => _items[index];
        set
        {
            ListViewItem old = _items[index];
            if (ReferenceEquals(value, old)) { return; }
            ValidateItem(value);
            if (_group is null) old.AttachToListView(null); else old.AttachToGroup(null);
            _items[index] = value;
            Attach(value);
            SynchronizeOwner();
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
        Attach(item);
        SynchronizeOwner();
        return item;
    }

    public void Clear()
    {
        foreach (ListViewItem item in _items)
        {
            if (_group is null) item.AttachToListView(null); else item.AttachToGroup(null);
        }
        _items.Clear();
        SynchronizeOwner();
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
        if (_group is null) item.AttachToListView(null); else item.AttachToGroup(null);
        SynchronizeOwner();
    }

    private void ValidateItem(ListViewItem item)
    {
        ArgumentNullException.ThrowIfNull(item);
        if (Contains(item))
        {
            throw new ArgumentException("The item already belongs to a ListView.", nameof(item));
        }

        if (_group is null)
        {
            if (item.ListView is not null || (item.Group?.ListView is { } view && !ReferenceEquals(view, _owner)))
                throw new ArgumentException("The item belongs to a different ListView.", nameof(item));
        }
        else if (item.ListView is { } itemView && _group.ListView is { } groupView && !ReferenceEquals(itemView, groupView))
        {
            throw new ArgumentException("The item belongs to a different ListView.", nameof(item));
        }
    }

    private void Attach(ListViewItem item)
    {
        if (_group is null) item.AttachToListView(_owner);
        else { item.Group?.Items.Remove(item); item.AttachToGroup(_group); }
    }

    private void SynchronizeOwner()
    {
        _owner?.SynchronizeItems();
        _group?.ListView?.SynchronizeItems();
    }

    public IEnumerator<ListViewItem> GetEnumerator() => _items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    bool IList.IsFixedSize => false;
    bool ICollection.IsSynchronized => false;
    object ICollection.SyncRoot => this;
    object? IList.this[int index] { get => this[index]; set => this[index] = RequireItem(value); }
    int IList.Add(object? value) { ListViewItem item = RequireItem(value); Add(item); return IndexOf(item); }
    void IList.Insert(int index, object? value) => Insert(index, RequireItem(value));
    bool IList.Contains(object? value) => value is ListViewItem item && Contains(item);
    int IList.IndexOf(object? value) => value is ListViewItem item ? IndexOf(item) : -1;
    void IList.Remove(object? value) { if (value is ListViewItem item) Remove(item); }
    void ICollection.CopyTo(Array array, int index) => _items.ToArray().CopyTo(array, index);

    private static ListViewItem RequireItem(object? value)
        => value as ListViewItem ?? throw new ArgumentException("The value must be a ListViewItem.", nameof(value));
}
