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
    }

    /// <summary>
    /// Represents the subitem collection for a <see cref="ListViewItem"/>.
    /// </summary>
    public class ListViewSubItemCollection : IList<ListViewSubItem>
    {
        private readonly List<ListViewSubItem> _items = [];
        private readonly ListViewItem _owner;
        private int _lastAccessedIndex = -1;

        internal ListViewSubItemCollection(ListViewItem owner)
        {
            _owner = owner;
        }

        public ListViewSubItem this[int index]
        {
            get => _items[index];
            set => _items[index] = value;
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

        public void Add(ListViewSubItem item)
        {
            _items.Add(item);
        }

        public ListViewSubItem Add(string? text)
        {
            var item = new ListViewSubItem(_owner, text);
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

        /// <summary>
        /// Determines whether a subitem has the specified name, ignoring case.
        /// </summary>
        public virtual bool ContainsKey(string? key)
        {
            return IndexOfKey(key) >= 0;
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

        public void Insert(int index, ListViewSubItem item)
        {
            _items.Insert(index, item);
        }

        public bool Remove(ListViewSubItem item)
        {
            return _items.Remove(item);
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

        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
        }

        private static bool KeyMatches(string name, string key)
        {
            // WinForms checks UTF-16 lengths before comparing names with the invariant culture.
            return name.Length == key.Length &&
                string.Equals(name, key, StringComparison.InvariantCultureIgnoreCase);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
