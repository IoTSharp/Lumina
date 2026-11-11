using System.Runtime.InteropServices;

namespace Lumina.Forms;

internal static partial class Win32
{
    internal const int LVM_GETITEMCOUNT = LVM_FIRST + 4;
    internal const int LVM_DELETEITEM = LVM_FIRST + 8;
    internal const int LVM_DELETEALLITEMS = LVM_FIRST + 9;
    internal const int LVM_DELETECOLUMN = LVM_FIRST + 28;
    internal const int LVM_SETCOLUMNWIDTH = LVM_FIRST + 30;
    internal const int LVM_GETITEMSTATE = LVM_FIRST + 44;
    internal const int LVM_SETITEMW = LVM_FIRST + 76;
    internal const int LVM_INSERTITEMW = LVM_FIRST + 77;
    internal const int LVM_SETCOLUMNW = LVM_FIRST + 96;
    internal const int LVM_INSERTCOLUMNW = LVM_FIRST + 97;
    internal const int HDM_GETITEMCOUNT = 0x1200;
    internal const uint LVIF_TEXT = 0x0001;
    internal const uint LVIF_IMAGE = 0x0002;
    internal const uint LVIF_STATE = 0x0008;

    [StructLayout(LayoutKind.Sequential)]
    internal struct LVITEMW
    {
        public uint mask;
        public int iItem;
        public int iSubItem;
        public uint state;
        public uint stateMask;
        public nint pszText;
        public int cchTextMax;
        public int iImage;
        public nint lParam;
        public int iIndent;
        public int iGroupId;
        public uint cColumns;
        public nint puColumns;
        public nint piColFmt;
        public int iGroup;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct LVCOLUMNW
    {
        public uint mask;
        public int fmt;
        public int cx;
        public nint pszText;
        public int cchTextMax;
        public int iSubItem;
        public int iImage;
        public int iOrder;
        public int cxMin;
        public int cxDefault;
        public int cxIdeal;
    }

    internal const int TVM_INSERTITEMW = TV_FIRST + 50;
    internal const int TVM_DELETEITEM = TV_FIRST + 1;
    internal const int TVM_GETNEXTITEM = TV_FIRST + 10;
    internal const int TVM_GETITEMW = TV_FIRST + 62;
    internal const int TVM_SETITEMW = TV_FIRST + 63;
    internal const int TVM_GETITEMSTATE = TV_FIRST + 39;
    internal const uint TVIF_TEXT = 0x0001;
    internal const uint TVIF_IMAGE = 0x0002;
    internal const uint TVIF_SELECTEDIMAGE = 0x0020;
    internal const uint TVIF_STATE = 0x0008;
    internal const uint TVIS_EXPANDED = 0x0020;
    internal const uint TVIS_STATEIMAGEMASK = 0xF000;
    internal static readonly nint TVI_ROOT = unchecked((nint)(-65536));
    internal static readonly nint TVI_LAST = unchecked((nint)(-65534));

    [StructLayout(LayoutKind.Sequential)]
    internal struct TVITEMW
    {
        public uint mask;
        public nint hItem;
        public uint state;
        public uint stateMask;
        public nint pszText;
        public int cchTextMax;
        public int iImage;
        public int iSelectedImage;
        public int cChildren;
        public nint lParam;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct TVINSERTSTRUCTW
    {
        public nint hParent;
        public nint hInsertAfter;
        public TVITEMW item;
    }
}
