using System.Runtime.InteropServices;

namespace Lumina.Forms;

public partial class ListView
{
    private const int LVM_SETIMAGELIST = Win32.LVM_FIRST + 3;
    private const int LVSIL_NORMAL = 0;
    private const int LVSIL_SMALL = 1;
    private const int LVSIL_STATE = 2;
    private const int LVM_ENABLEGROUPVIEW = Win32.LVM_FIRST + 157;
    private const int LVM_INSERTGROUP = Win32.LVM_FIRST + 145;
    private const int LVM_REMOVEALLGROUPS = Win32.LVM_FIRST + 160;
    private const uint LVGF_HEADER = 0x00000001;
    private const uint LVGF_ALIGN = 0x00000008;
    private const uint LVGF_GROUPID = 0x00000010;
    private const uint LVGF_STATE = 0x00000004;
    private const uint LVGS_COLLAPSED = 0x00000001;
    private const uint LVGS_COLLAPSIBLE = 0x00000008;
    private bool _synchronizingGroups;
    private bool _groupsSynchronizationPending;

    private void SetImageList(ref ImageList? field, ImageList? value)
    {
        if (ReferenceEquals(field, value)) { return; }
        bool alreadyUsed = value is not null && UsesImageList(value);
        ImageList? previous = field;
        field = value;
        if (previous is not null && !UsesImageList(previous)) UnsubscribeImageList(previous);
        if (value is not null && !alreadyUsed)
        {
            value.RecreateHandle += OnImageListChanged;
            value.ImagesChanged += OnImageListChanged;
            value.Disposed += OnImageListDisposed;
        }
        OnImageListChanged(this, EventArgs.Empty);
    }

    private bool UsesImageList(ImageList list)
        => ReferenceEquals(_smallImageList, list) || ReferenceEquals(_largeImageList, list) || ReferenceEquals(_stateImageList, list);

    private void UnsubscribeImageList(ImageList list)
    {
        list.RecreateHandle -= OnImageListChanged;
        list.ImagesChanged -= OnImageListChanged;
        list.Disposed -= OnImageListDisposed;
    }

    private void OnImageListChanged(object? sender, EventArgs e)
    {
        RefreshImageLists();
        SynchronizeColumns();
        SynchronizeGroups();
        SynchronizeItems();
    }

    private void OnImageListDisposed(object? sender, EventArgs e)
    {
        if (sender is not ImageList list) return;
        if (ReferenceEquals(_smallImageList, list)) _smallImageList = null;
        if (ReferenceEquals(_largeImageList, list)) _largeImageList = null;
        if (ReferenceEquals(_stateImageList, list)) _stateImageList = null;
        UnsubscribeImageList(list);
        OnImageListChanged(this, EventArgs.Empty);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (NativeHandle != 0)
            {
                _ = Win32.SendMessageW(NativeHandle, LVM_SETIMAGELIST, LVSIL_NORMAL, 0);
                _ = Win32.SendMessageW(NativeHandle, LVM_SETIMAGELIST, LVSIL_SMALL, 0);
                _ = Win32.SendMessageW(NativeHandle, LVM_SETIMAGELIST, LVSIL_STATE, 0);
            }
            if (_smallImageList is not null) UnsubscribeImageList(_smallImageList);
            if (_largeImageList is not null) UnsubscribeImageList(_largeImageList);
            if (_stateImageList is not null) UnsubscribeImageList(_stateImageList);
        }
        base.Dispose(disposing);
    }

    internal void RefreshImageLists()
    {
        if (NativeHandle == 0) { return; }
        _ = Win32.SendMessageW(NativeHandle, LVM_SETIMAGELIST, LVSIL_NORMAL, _largeImageList?.Handle ?? 0);
        _ = Win32.SendMessageW(NativeHandle, LVM_SETIMAGELIST, LVSIL_SMALL, _smallImageList?.Handle ?? 0);
        _ = Win32.SendMessageW(NativeHandle, LVM_SETIMAGELIST, LVSIL_STATE, _stateImageList?.Handle ?? 0);
    }

    internal int ResolveImageIndex(ListViewItem item)
    {
        ImageList? images = _view == View.LargeIcon || _view == View.Tile ? _largeImageList : _smallImageList;
        if (images is null) { return -1; }
        if (!string.IsNullOrEmpty(item.ImageKey)) { return images.Images.IndexOfKey(item.ImageKey); }
        return item.ImageIndex >= 0 && item.ImageIndex < images.Images.Count ? item.ImageIndex : -1;
    }

    internal int ResolveColumnImageIndex(ColumnHeader column)
    {
        if (_smallImageList is null) { return -1; }
        if (!string.IsNullOrEmpty(column.ImageKey)) return _smallImageList.Images.IndexOfKey(column.ImageKey);
        return column.ImageIndex >= 0 && column.ImageIndex < _smallImageList.Images.Count ? column.ImageIndex : -1;
    }

    internal unsafe void SynchronizeGroups()
    {
        nint handle = NativeHandle;
        if (handle == 0 || IsDisposed) return;
        if (_synchronizingGroups || _synchronizingItems || _synchronizingColumns)
        {
            _groupsSynchronizationPending = true;
            return;
        }
        _synchronizingGroups = true;
        try
        {
            ListViewGroup[] groups = _groups.ToArray();
            _ = Win32.SendMessageW(handle, LVM_ENABLEGROUPVIEW, 0, 0);
            _ = Win32.SendMessageW(handle, LVM_REMOVEALLGROUPS, 0, 0);
            if (NativeHandle != handle || IsDisposed) return;
            if (!_showGroups || groups.Length == 0) return;

            // Unassigned items and items whose group was removed remain visible.
            InsertNativeGroup(handle, null, 0);
            for (int index = 0; index < groups.Length; index++)
            {
                if (NativeHandle != handle || IsDisposed) return;
                InsertNativeGroup(handle, groups[index], index + 1);
            }
            if (Win32.SendMessageW(handle, LVM_ENABLEGROUPVIEW, 1, 0) == -1)
                throw new InvalidOperationException("The native list view does not support group view.");
        }
        finally
        {
            _synchronizingGroups = false;
            _itemsSynchronizationPending = true;
            SchedulePendingSynchronization();
        }
    }

    private unsafe void InsertNativeGroup(nint handle, ListViewGroup? group, int id)
    {
        string header = group?.Header ?? "Default";
        string footer = group?.Footer ?? string.Empty;
        string subtitle = group?.Subtitle ?? string.Empty;
        string task = group?.TaskLink ?? string.Empty;
        fixed (char* headerPointer = header)
        fixed (char* footerPointer = footer)
        fixed (char* subtitlePointer = subtitle)
        fixed (char* taskPointer = task)
        {
            int titleImage = group?.TitleImageIndex ?? -1;
            if (group is not null && !string.IsNullOrEmpty(group.TitleImageKey))
                titleImage = _largeImageList?.Images.IndexOfKey(group.TitleImageKey) ?? -1;
            LVGROUP native = new()
            {
                cbSize = (uint)sizeof(LVGROUP),
                mask = LVGF_HEADER | LVGF_GROUPID | LVGF_ALIGN | LVGF_STATE | 0x2 | 0x100 | 0x200 | 0x1000,
                pszHeader = (nint)headerPointer, cchHeader = header.Length,
                pszFooter = (nint)footerPointer, cchFooter = footer.Length,
                pszSubtitle = (nint)subtitlePointer, cchSubtitle = (uint)subtitle.Length,
                pszTask = (nint)taskPointer, cchTask = (uint)task.Length,
                iGroupId = id,
                uAlign = AlignmentFlags(group?.HeaderAlignment ?? HorizontalAlignment.Left)
                    | (AlignmentFlags(group?.FooterAlignment ?? HorizontalAlignment.Left) << 3),
                state = group?.CollapsedState switch
                {
                    ListViewGroupCollapsedState.Collapsed => LVGS_COLLAPSIBLE | LVGS_COLLAPSED,
                    ListViewGroupCollapsedState.Expanded => LVGS_COLLAPSIBLE,
                    _ => 0,
                },
                stateMask = LVGS_COLLAPSED | LVGS_COLLAPSIBLE,
                iTitleImage = titleImage,
            };
            if (Win32.SendMessageW(handle, LVM_INSERTGROUP, -1, (nint)(&native)) == -1)
                throw new InvalidOperationException("The native list view could not insert a group.");
        }
    }

    private static uint AlignmentFlags(HorizontalAlignment value)
        => value switch { HorizontalAlignment.Center => 2u, HorizontalAlignment.Right => 4u, _ => 1u };

    internal int ResolveGroupId(ListViewItem item)
        => item.Group?.ListView == this ? item.Group.Index + 1 : 0;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct LVGROUP
    {
        public uint cbSize;
        public uint mask;
        public nint pszHeader;
        public int cchHeader;
        public nint pszFooter;
        public int cchFooter;
        public int iGroupId;
        public uint stateMask;
        public uint state;
        public uint uAlign;
        public nint pszSubtitle;
        public uint cchSubtitle;
        public nint pszTask;
        public uint cchTask;
        public nint pszDescriptionTop;
        public uint cchDescriptionTop;
        public nint pszDescriptionBottom;
        public uint cchDescriptionBottom;
        public int iTitleImage;
        public int iExtendedImage;
        public int iFirstItem;
        public uint cItems;
        public nint pszSubsetTitle;
        public uint cchSubsetTitle;
    }
}
