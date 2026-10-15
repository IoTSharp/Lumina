using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace Lumina.Forms;

/// <summary>
/// Represents an item displayed by a <see cref="ListView"/>.
/// </summary>
public class ListViewItem
{
    private ListViewSubItemCollection? _subItems;

    /// <summary>
    /// Initializes a new list view item with empty primary text.
    /// </summary>
    public ListViewItem()
    {
    }

    /// <summary>
    /// Initializes a new list view item with the specified main text.
    /// </summary>
    /// <param name="text">The primary item text.</param>
    public ListViewItem(string? text)
    {
        Text = text;
    }

    /// <summary>
    /// Initializes a new list view item with the specified subitem text.
    /// </summary>
    /// <param name="items">The primary item text followed by any additional subitem text.</param>
    public ListViewItem(string?[]? items)
    {
        if (items is { Length: > 0 })
        {
            _subItems = new ListViewSubItemCollection(this);
            foreach (string? text in items)
            {
                _subItems.Add(text);
            }
        }
    }

    /// <summary>
    /// Gets or sets the primary text of the item.
    /// </summary>
    [AllowNull]
    public string Text
    {
        get => _subItems is null || _subItems.Count == 0 ? string.Empty : _subItems[0].Text;
        set
        {
            SubItems[0].Text = value;
        }
    }

    /// <summary>
    /// Gets the subitems belonging to the item.
    /// </summary>
    public ListViewSubItemCollection SubItems
    {
        get
        {
            _subItems ??= new ListViewSubItemCollection(this);
            if (_subItems.Count == 0)
            {
                _subItems.Add(string.Empty);
            }

            return _subItems;
        }
    }

    /// <summary>
    /// Gets or sets the item foreground color.
    /// </summary>
    public Color ForeColor { get; set; } = Color.Empty;

    /// <summary>
    /// Represents a single list view subitem.
    /// </summary>
    public sealed class ListViewSubItem
    {
        private string _text = string.Empty;

        internal ListViewSubItem(string? text)
        {
            Text = text;
        }

        /// <summary>
        /// Gets or sets the displayed subitem text.
        /// </summary>
        [AllowNull]
        public string Text
        {
            get => _text;
            set => _text = value ?? string.Empty;
        }
    }

    /// <summary>
    /// Represents the subitem collection for a <see cref="ListViewItem"/>.
    /// </summary>
    public sealed class ListViewSubItemCollection : IList<ListViewSubItem>
    {
        private readonly List<ListViewSubItem> _items = [];
        private readonly ListViewItem _owner;

        internal ListViewSubItemCollection(ListViewItem owner)
        {
            _owner = owner;
        }

        public ListViewSubItem this[int index]
        {
            get => _items[index];
            set => _items[index] = value;
        }

        public int Count => _items.Count;

        public bool IsReadOnly => false;

        public void Add(ListViewSubItem item)
        {
            _items.Add(item);
        }

        public ListViewSubItem Add(string? text)
        {
            var item = new ListViewSubItem(text);
            _items.Add(item);
            return item;
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(ListViewSubItem item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(ListViewSubItem[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<ListViewSubItem> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public int IndexOf(ListViewSubItem item)
        {
            return _items.IndexOf(item);
        }

        public void Insert(int index, ListViewSubItem item)
        {
            _items.Insert(index, item);
        }

        public bool Remove(ListViewSubItem item)
        {
            return _items.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
