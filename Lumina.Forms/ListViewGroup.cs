using System.ComponentModel;

namespace Lumina.Forms;

public enum ListViewGroupCollapsedState
{
    Default = 0,
    Collapsed = 1,
    Expanded = 2,
}
public class ListViewGroup
{
    private readonly ListView.ListViewItemCollection _items;
    private string _header = string.Empty;
    private string _footer = string.Empty;
    private string _name = string.Empty;
    private string _subtitle = string.Empty;
    private string _taskLink = string.Empty;
    private string _titleImageKey = string.Empty;
    private int _titleImageIndex = -1;
    private HorizontalAlignment _headerAlignment;
    private HorizontalAlignment _footerAlignment;
    private ListViewGroupCollapsedState _collapsedState;
    private ListView? _listView;

    public ListViewGroup()
    {
        _items = new ListView.ListViewItemCollection(this);
    }

    public ListViewGroup(string header) : this() => Header = header;
    public ListViewGroup(string header, HorizontalAlignment headerAlignment) : this()
    {
        Header = header;
        HeaderAlignment = headerAlignment;
    }

    public ListViewGroup(string key, string headerText) : this()
    {
        Name = key;
        Header = headerText;
    }

    [DefaultValue(ListViewGroupCollapsedState.Default)]
    public ListViewGroupCollapsedState CollapsedState
    {
        get => _collapsedState;
        set
        {
            if (!Enum.IsDefined(value)) throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(ListViewGroupCollapsedState));
            _collapsedState = value;
            _listView?.SynchronizeGroups();
        }
    }

    [DefaultValue("")]
    public string Header { get => _header; set { _header = value ?? string.Empty; _listView?.SynchronizeGroups(); } }
    [DefaultValue("")]
    public string Footer { get => _footer; set { _footer = value ?? string.Empty; _listView?.SynchronizeGroups(); } }
    [DefaultValue("")]
    public string Subtitle { get => _subtitle; set { _subtitle = value ?? string.Empty; _listView?.SynchronizeGroups(); } }
    [DefaultValue("")]
    public string TaskLink { get => _taskLink; set { _taskLink = value ?? string.Empty; _listView?.SynchronizeGroups(); } }

    [DefaultValue(HorizontalAlignment.Left)]
    public HorizontalAlignment HeaderAlignment { get => _headerAlignment; set { ValidateAlignment(value); _headerAlignment = value; _listView?.SynchronizeGroups(); } }
    [DefaultValue(HorizontalAlignment.Left)]
    public HorizontalAlignment FooterAlignment { get => _footerAlignment; set { ValidateAlignment(value); _footerAlignment = value; _listView?.SynchronizeGroups(); } }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ListView? ListView => _listView;

    [Browsable(false)]
    public ListView.ListViewItemCollection Items => _items;

    [DefaultValue("")]
    public string Name { get => _name; set { _name = value ?? string.Empty; _listView?.SynchronizeGroups(); } }
    [DefaultValue(null)]
    [Bindable(true)]
    [Localizable(false)]
    [TypeConverter(typeof(StringConverter))]
    public object? Tag { get; set; }
    [DefaultValue(-1)]
    public int TitleImageIndex { get => _titleImageIndex; set { if (value < -1) throw new ArgumentOutOfRangeException(nameof(value)); _titleImageIndex = value; _titleImageKey = string.Empty; _listView?.SynchronizeGroups(); } }
    [DefaultValue("")]
    public string TitleImageKey { get => _titleImageKey; set { _titleImageKey = value ?? string.Empty; _titleImageIndex = -1; _listView?.SynchronizeGroups(); } }

    internal int Index => _listView?.Groups.IndexOf(this) ?? -1;
    internal void AttachToListView(ListView? listView) => _listView = listView;

    public override string ToString() => $"ListViewGroup: {{{Header}}}";

    private static void ValidateAlignment(HorizontalAlignment value)
    {
        if ((uint)value > (uint)HorizontalAlignment.Center)
            throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(HorizontalAlignment));
    }
}
