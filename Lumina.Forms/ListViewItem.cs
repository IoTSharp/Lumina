using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace Lumina.Forms;

/// <summary>
/// Represents an item displayed by a <see cref="ListView"/>.
/// </summary>
public class ListViewItem
{
    // Collection instances are views over the item's data; each view keeps its own key cache.
    private List<ListViewSubItem> _subItemData = [];
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
        get => _subItemData.Count == 0 ? string.Empty : _subItemData[0].Text;
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
                // Restoring the primary subitem starts a new data list. Existing empty
                // enumerators keep observing the old list, while every view sees this one.
                _subItemData = [];
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
    public class ListViewSubItem
    {
        // Keep the unattached default shared and lazy, as WinForms does through Control.DefaultFont.
        // Control-wide font configuration and attached ListView inheritance are separate work.
        private static readonly Lazy<Font> s_defaultFont = new(static () => SystemFonts.MessageBoxFont!);

        private string _text = string.Empty;
        private string _name = string.Empty;
        private Color _foreColor = Color.Empty;
        private Color _backColor = Color.Empty;
        private Font? _font;

        /// <summary>
        /// Initializes a new list view subitem with empty text.
        /// </summary>
        public ListViewSubItem()
        {
        }

        /// <summary>
        /// Initializes a new list view subitem associated with the specified item.
        /// </summary>
        /// <param name="owner">The item associated with this subitem, or <see langword="null"/>.</param>
        /// <param name="text">The displayed subitem text.</param>
        public ListViewSubItem(ListViewItem? owner, string? text)
        {
            Owner = owner;
            Text = text;
        }

        /// <summary>
        /// Initializes a new list view subitem with the specified text and style.
        /// </summary>
        /// <param name="owner">The item associated with this subitem, or <see langword="null"/>.</param>
        /// <param name="text">The displayed subitem text.</param>
        /// <param name="foreColor">The text color, or <see cref="Color.Empty"/> to use the default.</param>
        /// <param name="backColor">The background color, or <see cref="Color.Empty"/> to use the default.</param>
        /// <param name="font">The borrowed text font, or <see langword="null"/> to use the default.</param>
        public ListViewSubItem(ListViewItem? owner, string? text, Color foreColor, Color backColor, Font? font)
            : this(owner, text)
        {
            _foreColor = foreColor;
            _backColor = backColor;
            _font = font;
        }

        internal ListViewItem? Owner { get; set; }

        /// <summary>
        /// Gets or sets the text color of the subitem.
        /// </summary>
        public Color ForeColor
        {
            get => _foreColor.IsEmpty ? SystemColors.WindowText : _foreColor;
            set => _foreColor = value;
        }

        /// <summary>
        /// Gets or sets the background color of the subitem.
        /// </summary>
        public Color BackColor
        {
            get => _backColor.IsEmpty ? SystemColors.Window : _backColor;
            set => _backColor = value;
        }

        /// <summary>
        /// Gets or sets the borrowed font used to display the subitem text.
        /// </summary>
        [AllowNull]
        [Localizable(true)]
        public Font Font
        {
            get => _font ?? s_defaultFont.Value;
            set => _font = value;
        }

        /// <summary>
        /// Clears the subitem's explicit colors and font without disposing the font.
        /// </summary>
        public void ResetStyle()
        {
            _foreColor = Color.Empty;
            _backColor = Color.Empty;
            _font = null;
        }

        /// <summary>
        /// Gets or sets the displayed subitem text.
        /// </summary>
        [AllowNull]
        [Localizable(true)]
        public string Text
        {
            get => _text;
            set => _text = value ?? string.Empty;
        }

        /// <summary>
        /// Gets or sets the name of the subitem.
        /// </summary>
        [AllowNull]
        [Localizable(true)]
        public string Name
        {
            get => _name;
            set => _name = value ?? string.Empty;
        }

        /// <summary>
        /// Gets or sets application data associated with the subitem.
        /// </summary>
        [Localizable(false)]
        [Bindable(true)]
        [DefaultValue(null)]
        [TypeConverter(typeof(StringConverter))]
        public object? Tag { get; set; }

        /// <summary>
        /// Returns a string containing the subitem's displayed text.
        /// </summary>
        public override string ToString() => $"ListViewSubItem: {{{Text}}}";
    }

    /// <summary>
    /// Represents the subitem collection for a <see cref="ListViewItem"/>.
    /// </summary>
    public class ListViewSubItemCollection : IList, IList<ListViewSubItem>
    {
        private readonly ListViewItem _owner;
        private int _lastAccessedIndex = -1;

        private List<ListViewSubItem> _items => _owner._subItemData;

        /// <summary>
        /// Initializes a new view of the subitems belonging to the specified item.
        /// </summary>
        /// <param name="owner">The item whose subitems are exposed by the collection.</param>
        public ListViewSubItemCollection(ListViewItem owner)
        {
            ArgumentNullException.ThrowIfNull(owner);
            _owner = owner;
        }

        /// <summary>
        /// Gets or replaces the subitem at the specified index.
        /// </summary>
        public ListViewSubItem this[int index]
        {
            get => _items[index];
            set
            {
                // WinForms validates the position before checking the replacement value.
                if ((uint)index >= (uint)_items.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                ArgumentNullException.ThrowIfNull(value);
                _items[index] = value;
            }
        }

        /// <summary>
        /// Gets the subitem whose name matches the specified key, or <see langword="null"/>.
        /// </summary>
        public virtual ListViewSubItem? this[string? key]
        {
            get
            {
                int index = IndexOfKey(key);
                return index < 0 ? null : _items[index];
            }
        }

        public int Count => _items.Count;

        public bool IsReadOnly => false;

        bool IList.IsFixedSize => false;

        bool ICollection.IsSynchronized => true;

        object ICollection.SyncRoot => this;

        object? IList.this[int index]
        {
            get => this[index];
            set
            {
                // The object interface checks the value's type before the position.
                if (value is not ListViewSubItem subItem)
                {
                    throw new ArgumentException("The value must be a ListViewSubItem.", nameof(value));
                }

                this[index] = subItem;
            }
        }

        /// <summary>
        /// Adds the specified subitem and returns the same object.
        /// </summary>
        /// <param name="item">The subitem to add.</param>
        /// <returns>The subitem added to the collection.</returns>
        public ListViewSubItem Add(ListViewSubItem item)
        {
            ArgumentNullException.ThrowIfNull(item);
            item.Owner = _owner;
            _items.Add(item);
            return item;
        }

        /// <summary>
        /// Adds a new subitem with the specified text.
        /// </summary>
        /// <param name="text">The displayed subitem text.</param>
        /// <returns>The new subitem added to the collection.</returns>
        public ListViewSubItem Add(string? text)
        {
            return Add(new ListViewSubItem(_owner, text));
        }

        /// <summary>
        /// Adds a new subitem with the specified text and style.
        /// </summary>
        /// <param name="text">The displayed subitem text.</param>
        /// <param name="foreColor">The text color, or <see cref="Color.Empty"/> to use the default.</param>
        /// <param name="backColor">The background color, or <see cref="Color.Empty"/> to use the default.</param>
        /// <param name="font">The borrowed text font, or <see langword="null"/> to use the default.</param>
        /// <returns>The new subitem added to the collection.</returns>
        public ListViewSubItem Add(string? text, Color foreColor, Color backColor, Font? font)
        {
            return Add(new ListViewSubItem(_owner, text, foreColor, backColor, font));
        }

        /// <summary>
        /// Adds the non-null subitems from the specified array in their original order.
        /// </summary>
        /// <param name="items">The subitems to add. Null entries are skipped.</param>
        public void AddRange(ListViewSubItem?[] items)
        {
            ArgumentNullException.ThrowIfNull(items);
            foreach (ListViewSubItem? item in items)
            {
                if (item is not null)
                {
                    Add(item);
                }
            }
        }

        /// <summary>
        /// Adds new subitems for the non-null text entries in the specified array.
        /// </summary>
        /// <param name="items">The text entries to add. Null entries are skipped.</param>
        public void AddRange(string?[] items)
        {
            ArgumentNullException.ThrowIfNull(items);
            foreach (string? text in items)
            {
                // Unlike Add(null), the WinForms bulk overload omits null text entries.
                if (text is not null)
                {
                    Add(text);
                }
            }
        }

        /// <summary>
        /// Adds new subitems with the specified style for non-null text entries.
        /// </summary>
        /// <param name="items">The text entries to add. Null entries are skipped.</param>
        /// <param name="foreColor">The text color, or <see cref="Color.Empty"/> to use the default.</param>
        /// <param name="backColor">The background color, or <see cref="Color.Empty"/> to use the default.</param>
        /// <param name="font">The borrowed text font, or <see langword="null"/> to use the default.</param>
        public void AddRange(string?[] items, Color foreColor, Color backColor, Font? font)
        {
            ArgumentNullException.ThrowIfNull(items);
            foreach (string? text in items)
            {
                if (text is not null)
                {
                    Add(text, foreColor, backColor, font);
                }
            }
        }

        /// <summary>
        /// Removes all subitems without creating a replacement primary subitem.
        /// </summary>
        public void Clear()
        {
            // The standard collection leaves an already empty enumeration valid.
            if (_items.Count > 0)
            {
                _items.Clear();
            }
        }

        /// <summary>
        /// Determines whether the specified subitem occurs by reference in this collection.
        /// </summary>
        public bool Contains(ListViewSubItem? subItem)
        {
            return IndexOf(subItem) >= 0;
        }

        /// <summary>
        /// Determines whether a subitem has the specified name, ignoring case.
        /// </summary>
        public virtual bool ContainsKey(string? key)
        {
            return IndexOfKey(key) >= 0;
        }

        public void CopyTo(ListViewSubItem[] array, int arrayIndex)
        {
            ((ICollection)this).CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns an enumerator over the subitems in their current order.
        /// </summary>
        public IEnumerator GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        /// <summary>
        /// Gets the first position of the specified subitem, using reference identity.
        /// </summary>
        public int IndexOf(ListViewSubItem? subItem)
        {
            for (int index = 0; index < _items.Count; index++)
            {
                if (ReferenceEquals(_items[index], subItem))
                {
                    return index;
                }
            }

            return -1;
        }

        /// <summary>
        /// Gets the index of a subitem with the specified name, or -1 if no name matches.
        /// </summary>
        public virtual int IndexOfKey(string? key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return -1;
            }

            // WinForms reuses the last matching position, even after names or contents change.
            // Preserve that observable duplicate-key behavior without keeping a separate map.
            if ((uint)_lastAccessedIndex < (uint)_items.Count &&
                KeyMatches(_items[_lastAccessedIndex].Name, key))
            {
                return _lastAccessedIndex;
            }

            for (int index = 0; index < _items.Count; index++)
            {
                if (KeyMatches(_items[index].Name, key))
                {
                    _lastAccessedIndex = index;
                    return index;
                }
            }

            _lastAccessedIndex = -1;
            return -1;
        }

        /// <summary>
        /// Inserts the specified subitem at the specified position.
        /// </summary>
        /// <param name="index">The zero-based position, including the end of the collection.</param>
        /// <param name="item">The subitem to insert.</param>
        public void Insert(int index, ListViewSubItem item)
        {
            // WinForms validates the position first, including when item is null.
            if ((uint)index > (uint)_items.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            ArgumentNullException.ThrowIfNull(item);
            item.Owner = _owner;
            _items.Insert(index, item);
        }

        /// <summary>
        /// Removes the first occurrence of the specified subitem, if it exists.
        /// </summary>
        /// <param name="item">The subitem to remove, or <see langword="null"/>.</param>
        public void Remove(ListViewSubItem? item)
        {
            RemoveItem(item);
        }

        /// <summary>
        /// Removes the subitem found by the specified name, if one exists.
        /// </summary>
        public virtual void RemoveByKey(string? key)
        {
            int index = IndexOfKey(key);
            if (index >= 0)
            {
                RemoveAt(index);
            }
        }

        /// <summary>
        /// Removes the subitem at the specified position.
        /// </summary>
        /// <param name="index">The zero-based position of the subitem to remove.</param>
        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
        }

        private bool RemoveItem(ListViewSubItem? item)
        {
            // The typed WinForms collection matches object identity, even when a
            // derived subitem overrides Equals. Removal also preserves each view's
            // last key position and leaves the borrowed font alive.
            int index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }

            return false;
        }

        private static bool KeyMatches(string name, string key)
        {
            // WinForms checks UTF-16 lengths before comparing names with the invariant culture.
            return name.Length == key.Length &&
                string.Equals(name, key, StringComparison.InvariantCultureIgnoreCase);
        }

        void ICollection<ListViewSubItem>.Add(ListViewSubItem item)
        {
            Add(item);
        }

        int IList.Add(object? item)
        {
            if (item is not ListViewSubItem subItem)
            {
                throw new ArgumentException("The value must be a ListViewSubItem.", nameof(item));
            }

            Add(subItem);
            // Re-adding an existing object returns its first position, not the appended one.
            return IndexOf(subItem);
        }

        bool IList.Contains(object? value)
        {
            return value is ListViewSubItem subItem && Contains(subItem);
        }

        bool ICollection<ListViewSubItem>.Contains(ListViewSubItem item)
        {
            return Contains(item);
        }

        int IList.IndexOf(object? value)
        {
            return value is ListViewSubItem subItem ? IndexOf(subItem) : -1;
        }

        int IList<ListViewSubItem>.IndexOf(ListViewSubItem item)
        {
            return IndexOf(item);
        }

        void IList.Insert(int index, object? item)
        {
            if (item is not ListViewSubItem subItem)
            {
                throw new ArgumentException("The value must be a ListViewSubItem.", nameof(item));
            }

            Insert(index, subItem);
        }

        void IList.Remove(object? item)
        {
            if (item is ListViewSubItem subItem)
            {
                Remove(subItem);
            }
        }

        void ICollection.CopyTo(Array dest, int index)
        {
            // Empty WinForms views do not validate the destination or restore a primary item.
            // Array.CopyTo preserves the runtime's rank/lower-bound validation, exception
            // parameters, and partial writes when a derived destination rejects an element.
            if (_items.Count > 0)
            {
                try
                {
                    _items.ToArray().CopyTo(dest, index);
                }
                catch (ArrayTypeMismatchException exception)
                {
                    // WinForms translates incompatible element types after the runtime's
                    // rank, index and capacity checks. InvalidCastException from a derived
                    // destination is distinct and must keep its earlier element writes.
                    throw new ArgumentException("The destination array cannot store list view subitems.", exception);
                }
            }
        }

        void IList<ListViewSubItem>.Insert(int index, ListViewSubItem item)
        {
            Insert(index, item);
        }

        bool ICollection<ListViewSubItem>.Remove(ListViewSubItem item)
        {
            return RemoveItem(item);
        }

        IEnumerator<ListViewSubItem> IEnumerable<ListViewSubItem>.GetEnumerator()
        {
            // Keep the historical generic consumption path alongside the standard
            // non-generic public signature, using the same identity and version rules.
            return _items.GetEnumerator();
        }
    }
}
