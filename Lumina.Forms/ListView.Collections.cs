using System.ComponentModel;

namespace Lumina.Forms;

public partial class ListView
{
    private readonly List<ListViewItem> _nativeItems = [];
    private readonly Dictionary<ListViewItem, int> _nativeSubItemCounts = new(ReferenceEqualityComparer.Instance);
    private bool _synchronizingItems;
    private bool _synchronizingColumns;
    private bool _itemsSynchronizationPending;
    private bool _columnsSynchronizationPending;
    private bool _synchronizationPosted;

    protected override void OnHandleDestroyed(EventArgs e)
    {
        _nativeItems.Clear();
        _nativeSubItemCounts.Clear();
        _itemsSynchronizationPending = false;
        _columnsSynchronizationPending = false;
        _synchronizationPosted = false;
        _groupsSynchronizationPending = false;
        base.OnHandleDestroyed(e);
    }

    internal unsafe void SynchronizeColumns()
    {
        nint handle = NativeHandle;
        if (handle == 0) { return; }
        if (_synchronizingColumns || _synchronizingItems || _synchronizingGroups)
        {
            _columnsSynchronizationPending = true;
            return;
        }
        _synchronizingColumns = true;
        try
        {
            ColumnHeader[] columns = _columns.ToArray();
            nint header = Win32.SendMessageW(handle, Win32.LVM_GETHEADER, 0, 0);
            int oldCount = header == 0 ? 0 : (int)Win32.SendMessageW(header, Win32.HDM_GETITEMCOUNT, 0, 0);
            for (int index = oldCount - 1; index >= 0; index--)
            {
                if (Win32.SendMessageW(handle, Win32.LVM_DELETECOLUMN, index, 0) == 0)
                {
                    throw new InvalidOperationException("The native list view could not delete a column.");
                }
            }

            for (int index = 0; index < columns.Length; index++)
            {
                ColumnHeader column = columns[index];
                string text = column.Text;
                int imageIndex = ResolveColumnImageIndex(column);
                fixed (char* pointer = text)
                {
                    Win32.LVCOLUMNW native = new()
                    {
                        mask = 0x0001 | 0x0002 | 0x0004 | 0x0008 | (imageIndex >= 0 ? 0x0010u : 0u),
                        fmt = (column.TextAlign switch { HorizontalAlignment.Right => 1, HorizontalAlignment.Center => 2, _ => 0 }) | (imageIndex >= 0 ? 0x0800 : 0),
                        cx = Math.Max(0, column.Width),
                        pszText = (nint)pointer,
                        iSubItem = index,
                        iImage = imageIndex,
                    };
                    if (Win32.SendMessageW(handle, Win32.LVM_INSERTCOLUMNW, index, (nint)(&native)) == -1)
                    {
                        throw new InvalidOperationException("The native list view could not insert a column.");
                    }
                }
            }
            for (int index = 0; index < columns.Length; index++)
            {
                if (columns[index].Width < 0)
                {
                    _ = Win32.SendMessageW(handle, Win32.LVM_SETCOLUMNWIDTH, index, columns[index].Width);
                }
            }
            // Deleting native columns also removes their subitem text.
            SynchronizeItems();
        }
        finally
        {
            _synchronizingColumns = false;
            SchedulePendingSynchronization();
        }
    }

    internal unsafe void SynchronizeItems()
    {
        nint handle = NativeHandle;
        if (handle == 0) { return; }
        if (_synchronizingItems || _synchronizingGroups)
        {
            _itemsSynchronizationPending = true;
            return;
        }
        _synchronizingItems = true;
        try
        {
            ListViewItem[] items = _items.ToArray();
            Dictionary<ListViewItem, uint> states = new(ReferenceEqualityComparer.Instance);
            for (int index = 0; index < _nativeItems.Count; index++)
            {
                states[_nativeItems[index]] = unchecked((uint)Win32.SendMessageW(handle, Win32.LVM_GETITEMSTATE, index, 0xF003));
            }
            nint cleared = Win32.SendMessageW(handle, Win32.LVM_DELETEALLITEMS, 0, 0);
            if (NativeHandle != handle || IsDisposed) { return; }
            if (cleared == 0)
            {
                throw new InvalidOperationException("The native list view could not clear its items.");
            }
            _nativeItems.Clear();
            _nativeSubItemCounts.Clear();

            for (int index = 0; index < items.Length; index++)
            {
                ListViewItem item = items[index];
                string text = item.Text;
                fixed (char* pointer = text)
                {
                    Win32.LVITEMW native = new()
                    {
                        mask = Win32.LVIF_TEXT | Win32.LVIF_IMAGE | Win32.LVIF_STATE | (_showGroups && _groups.Count > 0 ? 0x100u : 0u),
                        iItem = index,
                        pszText = (nint)pointer,
                        iImage = ResolveImageIndex(item),
                        iGroupId = ResolveGroupId(item),
                        state = states.GetValueOrDefault(item),
                        stateMask = 0xF003,
                    };
                    nint inserted = Win32.SendMessageW(handle, Win32.LVM_INSERTITEMW, 0, (nint)(&native));
                    if (NativeHandle != handle || IsDisposed) { return; }
                    if (inserted == -1)
                    {
                        throw new InvalidOperationException("The native list view could not insert an item.");
                    }
                }
                _nativeItems.Add(item);
                WriteSubItems(item, index);
                if (NativeHandle != handle || IsDisposed) { return; }
            }
        }
        finally
        {
            _synchronizingItems = false;
            SchedulePendingSynchronization();
        }
    }

    internal void SynchronizeItem(ListViewItem item)
    {
        if (NativeHandle == 0) { return; }
        if (_synchronizingItems || _synchronizingGroups)
        {
            _itemsSynchronizationPending = true;
            return;
        }
        _synchronizingItems = true;
        try
        {
            int index = _nativeItems.FindIndex(candidate => ReferenceEquals(candidate, item));
            if (index >= 0) { WriteSubItems(item, index); }
        }
        finally
        {
            _synchronizingItems = false;
            SchedulePendingSynchronization();
        }
    }

    private unsafe void WriteSubItems(ListViewItem item, int index)
    {
        nint handle = NativeHandle;
        if (handle == 0 || IsDisposed) { return; }
        int count = Math.Max(1, item.SubItemCount);
        string[] texts = new string[count];
        for (int position = 0; position < count; position++)
        {
            texts[position] = position < item.SubItemCount ? item.GetSubItem(position).Text : string.Empty;
        }
        int imageIndex = ResolveImageIndex(item);
        int previousCount = _nativeSubItemCounts.GetValueOrDefault(item);
        for (int subItem = 0; subItem < Math.Max(count, previousCount); subItem++)
        {
            string text = subItem < texts.Length ? texts[subItem] : string.Empty;
            fixed (char* pointer = text)
            {
                Win32.LVITEMW native = new()
                {
                    mask = Win32.LVIF_TEXT | (subItem == 0 ? Win32.LVIF_IMAGE | (_showGroups && _groups.Count > 0 ? 0x100u : 0u) : 0),
                    iItem = index,
                    iSubItem = subItem,
                    pszText = (nint)pointer,
                    iImage = imageIndex,
                    iGroupId = ResolveGroupId(item),
                };
                nint updated = Win32.SendMessageW(handle, Win32.LVM_SETITEMW, 0, (nint)(&native));
                if (NativeHandle != handle || IsDisposed) { return; }
                if (updated == 0)
                {
                    throw new InvalidOperationException("The native list view could not update an item.");
                }
            }
        }
        _nativeSubItemCounts[item] = count;
    }

    private void SchedulePendingSynchronization()
    {
        if (_synchronizingItems || _synchronizingColumns || _synchronizingGroups || _synchronizationPosted
            || (!_itemsSynchronizationPending && !_columnsSynchronizationPending && !_groupsSynchronizationPending)
            || NativeHandle == 0 || IsDisposed)
        {
            return;
        }

        _synchronizationPosted = true;
        try
        {
            // Native notifications can synchronously mutate a collection. Finish
            // that native transaction before applying the latest managed snapshot.
            BeginInvoke((Action)(() =>
            {
                _synchronizationPosted = false;
                bool columns = _columnsSynchronizationPending;
                bool items = _itemsSynchronizationPending;
                bool groups = _groupsSynchronizationPending;
                _columnsSynchronizationPending = false;
                _itemsSynchronizationPending = false;
                _groupsSynchronizationPending = false;
                if (NativeHandle == 0 || IsDisposed) { return; }
                if (groups) { SynchronizeGroups(); }
                if (columns) { SynchronizeColumns(); }
                else if (items) { SynchronizeItems(); }
            }));
        }
        catch
        {
            _synchronizationPosted = false;
            throw;
        }
    }
}
