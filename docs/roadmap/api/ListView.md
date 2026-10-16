# ListView：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/ListView.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.ListView`；基类：`System.Windows.Forms.Control`。Lumina 同名类型：🟡 存在；基类及基线接口形状：⬜ 未匹配。

公开属性 **126**，公开事件 **95**，公开方法 **94**；受保护扩展成员 **191**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-E64B8374775B329A | Constructor | `public ListView()` | `System.Windows.Forms.ListView`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3E0B96305447B0C5 | Event | `public event System.Windows.Forms.LabelEditEventHandler AfterLabelEdit` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BEB699114DB0A3A7 | Event | `public event System.EventHandler AutoSizeChanged` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CA5A7857AC14473A | Event | `public event System.EventHandler BackColorChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-44392C3E8078C2A0 | Event | `public event System.EventHandler BackgroundImageChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-565B327FD12715B2 | Event | `public event System.EventHandler BackgroundImageLayoutChanged` | `System.Windows.Forms.ListView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AAA9A2E5D49F30F7 | Event | `public event System.Windows.Forms.LabelEditEventHandler BeforeLabelEdit` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-84935017D4114D9F | Event | `public event System.EventHandler BindingContextChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FE7C29C1AC52877E | Event | `public event System.Windows.Forms.CacheVirtualItemsEventHandler CacheVirtualItems` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B4A35BE8ADAB4491 | Event | `public event System.EventHandler CausesValidationChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8852A49AB3C540A7 | Event | `public event System.Windows.Forms.UICuesEventHandler ChangeUICues` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EAB49B393F9F795B | Event | `public event System.EventHandler Click` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C3C0009201800037 | Event | `public event System.EventHandler ClientSizeChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A87DAA0BF090ED77 | Event | `public event System.Windows.Forms.ColumnClickEventHandler ColumnClick` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5393E56CB6717B61 | Event | `public event System.Windows.Forms.ColumnReorderedEventHandler ColumnReordered` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-69DB215D8FD6C20D | Event | `public event System.Windows.Forms.ColumnWidthChangedEventHandler ColumnWidthChanged` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DC181264BD0C6EC8 | Event | `public event System.Windows.Forms.ColumnWidthChangingEventHandler ColumnWidthChanging` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7A1C51A2E7ECD887 | Event | `public event System.EventHandler ContextMenuChanged` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-22D98887876E2418 | Event | `public event System.EventHandler ContextMenuStripChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7E0BDB66900194EA | Event | `public event System.Windows.Forms.ControlEventHandler ControlAdded` | `System.Windows.Forms.Control` BrowsableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F0D93F95B19E890F | Event | `public event System.Windows.Forms.ControlEventHandler ControlRemoved` | `System.Windows.Forms.Control` BrowsableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-61FACFC6637285D5 | Event | `public event System.EventHandler CursorChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-03534969AFF4EDC6 | Event | `public event System.EventHandler DataContextChanged` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FD7AF743F53D5D06 | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A04AB5039455FFBD | Event | `public event System.EventHandler DockChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-65A324B36606C48F | Event | `public event System.EventHandler DoubleClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-837F4AF4C1B7E47C | Event | `public event System.EventHandler DpiChangedAfterParent` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B1392D231F229D26 | Event | `public event System.EventHandler DpiChangedBeforeParent` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2A4171AE1E482B1D | Event | `public event System.Windows.Forms.DragEventHandler DragDrop` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-96F53B0631F1CF54 | Event | `public event System.Windows.Forms.DragEventHandler DragEnter` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-05E0D61BB08F3497 | Event | `public event System.EventHandler DragLeave` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-70CDE66140774AEA | Event | `public event System.Windows.Forms.DragEventHandler DragOver` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-78EE5407695E476B | Event | `public event System.Windows.Forms.DrawListViewColumnHeaderEventHandler DrawColumnHeader` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C23B7CEFAA1BA897 | Event | `public event System.Windows.Forms.DrawListViewItemEventHandler DrawItem` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16E515BCE66F60A6 | Event | `public event System.Windows.Forms.DrawListViewSubItemEventHandler DrawSubItem` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F455549FBA7796B2 | Event | `public event System.EventHandler EnabledChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-95958A9EA48172BB | Event | `public event System.EventHandler Enter` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4858000027D3C55F | Event | `public event System.EventHandler FontChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F1BDB2DFD1849FAB | Event | `public event System.EventHandler ForeColorChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-DEFAFF4DD511D01B | Event | `public event System.Windows.Forms.GiveFeedbackEventHandler GiveFeedback` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-31ACDE16A5DBCF93 | Event | `public event System.EventHandler GotFocus` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-125731DE6477C2E8 | Event | `public event System.EventHandler&#96;1<System.Windows.Forms.ListViewGroupEventArgs> GroupCollapsedStateChanged` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-36007FEF09D141CC | Event | `public event System.EventHandler&#96;1<System.Windows.Forms.ListViewGroupEventArgs> GroupTaskLinkClick` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7810CA3DA7773F41 | Event | `public event System.EventHandler HandleCreated` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0E610F03DB514CEE | Event | `public event System.EventHandler HandleDestroyed` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0AFFC1D5FB46E7C6 | Event | `public event System.Windows.Forms.HelpEventHandler HelpRequested` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-98370E071CC032DC | Event | `public event System.EventHandler ImeModeChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-43F3C76E8366CDB3 | Event | `public event System.Windows.Forms.InvalidateEventHandler Invalidated` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-35EF76300B463951 | Event | `public event System.EventHandler ItemActivate` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6CE2EFA280FA79E7 | Event | `public event System.Windows.Forms.ItemCheckEventHandler ItemCheck` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1A1B9D6A926A6FBA | Event | `public event System.Windows.Forms.ItemCheckedEventHandler ItemChecked` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D9F59BE85BB37189 | Event | `public event System.Windows.Forms.ItemDragEventHandler ItemDrag` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-99B688654C67FAD2 | Event | `public event System.Windows.Forms.ListViewItemMouseHoverEventHandler ItemMouseHover` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4F7CBDFCEE7C3AE5 | Event | `public event System.Windows.Forms.ListViewItemSelectionChangedEventHandler ItemSelectionChanged` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AA1FC83E8C3370D8 | Event | `public event System.Windows.Forms.KeyEventHandler KeyDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D3236E4455D71FB9 | Event | `public event System.Windows.Forms.KeyPressEventHandler KeyPress` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ECBAA84F5F1BD6E2 | Event | `public event System.Windows.Forms.KeyEventHandler KeyUp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-650EAD4C4227EC4A | Event | `public event System.Windows.Forms.LayoutEventHandler Layout` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-DDD984A882E202E5 | Event | `public event System.EventHandler Leave` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-30DDEEF7F330D7AF | Event | `public event System.EventHandler LocationChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1AAABBAB084BD14A | Event | `public event System.EventHandler LostFocus` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9F795C9DAF005B8E | Event | `public event System.EventHandler MarginChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-EF5CB29127EE95D1 | Event | `public event System.EventHandler MouseCaptureChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-01A5DE24326D8EDB | Event | `public event System.Windows.Forms.MouseEventHandler MouseClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1E6634425601EA62 | Event | `public event System.Windows.Forms.MouseEventHandler MouseDoubleClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8D43B39EF8982D46 | Event | `public event System.Windows.Forms.MouseEventHandler MouseDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7C83A6F5CAECF6D0 | Event | `public event System.EventHandler MouseEnter` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-77CA033D2E8E0F5D | Event | `public event System.EventHandler MouseHover` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-848BEF3BEFEECDBC | Event | `public event System.EventHandler MouseLeave` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7C76E0B2BBDB4F34 | Event | `public event System.Windows.Forms.MouseEventHandler MouseMove` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B175157BD5E0C9A0 | Event | `public event System.Windows.Forms.MouseEventHandler MouseUp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D3137D666C4D39DC | Event | `public event System.Windows.Forms.MouseEventHandler MouseWheel` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B167828300E39903 | Event | `public event System.EventHandler Move` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9730F79207BC7257 | Event | `public event System.EventHandler PaddingChanged` | `System.Windows.Forms.ListView` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-85A8E631CCBB100C | Event | `public event System.Windows.Forms.PaintEventHandler Paint` | `System.Windows.Forms.ListView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A1FF977E4D10FC17 | Event | `public event System.EventHandler ParentChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CC6AFF712BF8509D | Event | `public event System.Windows.Forms.PreviewKeyDownEventHandler PreviewKeyDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E7005D77C40E61EE | Event | `public event System.Windows.Forms.QueryAccessibilityHelpEventHandler QueryAccessibilityHelp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D2328977F5EAB7C4 | Event | `public event System.Windows.Forms.QueryContinueDragEventHandler QueryContinueDrag` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B2A97BB1C655B2A3 | Event | `public event System.EventHandler RegionChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A069D2EC0207B7B | Event | `public event System.EventHandler Resize` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-EA4D87620BE7C765 | Event | `public event System.Windows.Forms.RetrieveVirtualItemEventHandler RetrieveVirtualItem` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D5D0AF5B116282CB | Event | `public event System.EventHandler RightToLeftChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A139680ADF5C807C | Event | `public event System.EventHandler RightToLeftLayoutChanged` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-892E7F76F10CA2BC | Event | `public event System.Windows.Forms.SearchForVirtualItemEventHandler SearchForVirtualItem` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-12001D8479E71202 | Event | `public event System.EventHandler SelectedIndexChanged` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8B852436D4B4FE4C | Event | `public event System.EventHandler SizeChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-855C9BB0402D52FD | Event | `public event System.EventHandler StyleChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BE73050ABFC2477F | Event | `public event System.EventHandler SystemColorsChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50C9A5A43F0A9B73 | Event | `public event System.EventHandler TabIndexChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D9E4CE57B1590B61 | Event | `public event System.EventHandler TabStopChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E4370F8459FA5E55 | Event | `public event System.EventHandler TextChanged` | `System.Windows.Forms.ListView` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7482A2CB20700FFA | Event | `public event System.EventHandler Validated` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B465C320BADA6901 | Event | `public event System.ComponentModel.CancelEventHandler Validating` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-64F4BAAC2E1353B3 | Event | `public event System.Windows.Forms.ListViewVirtualItemsSelectionRangeChangedEventHandler VirtualItemsSelectionRangeChanged` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-17652AE19A0726CE | Event | `public event System.EventHandler VisibleChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A9F689621B0965E2 | Method | `public System.Void ArrangeIcons()` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D948417305FB4A69 | Method | `public System.Void ArrangeIcons(System.Windows.Forms.ListViewAlignment value)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-97DFA1DE2B5F57F3 | Method | `public System.Void AutoResizeColumn(System.Int32 columnIndex, System.Windows.Forms.ColumnHeaderAutoResizeStyle headerAutoResize)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-552FC5C89A384BA2 | Method | `public System.Void AutoResizeColumns(System.Windows.Forms.ColumnHeaderAutoResizeStyle headerAutoResize)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-20F3175D0EFD5370 | Method | `public System.IAsyncResult BeginInvoke(System.Action method)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-354ADECF9D8C544B | Method | `public System.IAsyncResult BeginInvoke(System.Delegate method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-49885879DA99A155 | Method | `public System.IAsyncResult BeginInvoke(System.Delegate method, System.Object[] args)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-29EBDA8A7ABDDB97 | Method | `public System.Void BeginUpdate()` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-54A54921B857445B | Method | `public System.Void BringToFront()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D1C1EC971AC2A78B | Method | `public System.Void Clear()` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7E4362ADD3D92A21 | Method | `public System.Boolean Contains(System.Windows.Forms.Control ctl)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-921B30D585709985 | Method | `public System.Void CreateControl()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-AE3A54A8169C3543 | Method | `public System.Drawing.Graphics CreateGraphics()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-974DE3EBB3F37E96 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3FAFD8CB651A2DB9 | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1AA6B350CF069FA7 | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E2E3B9D9478109B | Method | `public System.Windows.Forms.DragDropEffects DoDragDropAsJson<!!0>(!!0 data, System.Windows.Forms.DragDropEffects allowedEffects) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DAEF5030B31F3BA1 | Method | `public System.Windows.Forms.DragDropEffects DoDragDropAsJson<!!0>(!!0 data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9BAD3909DC214978 | Method | `public System.Void DrawToBitmap(System.Drawing.Bitmap bitmap, System.Drawing.Rectangle targetBounds)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2CAF5AD6F4BBF901 | Method | `public System.Object EndInvoke(System.IAsyncResult asyncResult)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-109B2B000E9A6493 | Method | `public System.Void EndUpdate()` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E35E7BD95DC21B1 | Method | `public System.Void EnsureVisible(System.Int32 index)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8C361E2BAE96DF16 | Method | `public System.Windows.Forms.Form FindForm()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B194B12FD921E77A | Method | `public System.Windows.Forms.ListViewItem FindItemWithText(System.String text)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2592F25EC80E804A | Method | `public System.Windows.Forms.ListViewItem FindItemWithText(System.String text, System.Boolean includeSubItemsInSearch, System.Int32 startIndex)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-873D16D701A909FA | Method | `public System.Windows.Forms.ListViewItem FindItemWithText(System.String text, System.Boolean includeSubItemsInSearch, System.Int32 startIndex, System.Boolean isPrefixSearch)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A58D44BB0F75BB1 | Method | `public System.Windows.Forms.ListViewItem FindNearestItem(System.Windows.Forms.SearchDirectionHint dir, System.Drawing.Point point)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D237724653186096 | Method | `public System.Windows.Forms.ListViewItem FindNearestItem(System.Windows.Forms.SearchDirectionHint searchDirection, System.Int32 x, System.Int32 y)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-290913D6F1AEF2B5 | Method | `public System.Boolean Focus()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A68F36C2B49EA538 | Method | `public static System.Windows.Forms.Control FromChildHandle(System.IntPtr handle)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3024F544363AF231 | Method | `public static System.Windows.Forms.Control FromHandle(System.IntPtr handle)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9FD2E5FD8B655EB1 | Method | `public System.Windows.Forms.Control GetChildAtPoint(System.Drawing.Point pt)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-58294EAB36899DEF | Method | `public System.Windows.Forms.Control GetChildAtPoint(System.Drawing.Point pt, System.Windows.Forms.GetChildAtPointSkip skipValue)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AE333034586108C0 | Method | `public System.Windows.Forms.IContainerControl GetContainerControl()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-62C27B8DB7ABFB38 | Method | `public System.Windows.Forms.ListViewItem GetItemAt(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7E39188F441331B5 | Method | `public System.Drawing.Rectangle GetItemRect(System.Int32 index)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6CAF6888DE28433F | Method | `public System.Drawing.Rectangle GetItemRect(System.Int32 index, System.Windows.Forms.ItemBoundsPortion portion)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9196ED22E2B2D4D9 | Method | `public System.Windows.Forms.Control GetNextControl(System.Windows.Forms.Control ctl, System.Boolean forward)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F6B6AF610A905093 | Method | `public virtual System.Drawing.Size GetPreferredSize(System.Drawing.Size proposedSize)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-78AE3789688376E7 | Method | `public System.Void Hide()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B61AEEDC3316FF54 | Method | `public System.Windows.Forms.ListViewHitTestInfo HitTest(System.Drawing.Point point)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FD0D1F2C54FE175B | Method | `public System.Windows.Forms.ListViewHitTestInfo HitTest(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0EC43DB027CA9E06 | Method | `public System.Void Invalidate()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-21B2578E1B4641A0 | Method | `public System.Void Invalidate(System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AE1A93CD3811169B | Method | `public System.Void Invalidate(System.Drawing.Rectangle rc)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-39DF236CD2544272 | Method | `public System.Void Invalidate(System.Drawing.Rectangle rc, System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3966AB46398BEF66 | Method | `public System.Void Invalidate(System.Drawing.Region region)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E31C282E821FE76 | Method | `public System.Void Invalidate(System.Drawing.Region region, System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BA70C3C88BDA6767 | Method | `public !!0 Invoke<!!0>(System.Func&#96;1<!!0> method) where !!0: None;` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F248D299D25CA7AA | Method | `public System.Object Invoke(System.Delegate method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CED2B5BF976A58E0 | Method | `public System.Object Invoke(System.Delegate method, System.Object[] args)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-506AC466A4E00EE3 | Method | `public System.Void Invoke(System.Action method)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0C14E0B949AFF6DC | Method | `public System.Threading.Tasks.Task InvokeAsync(System.Action callback, System.Threading.CancellationToken cancellationToken = null)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-224F7D3EB5CC56B4 | Method | `public System.Threading.Tasks.Task InvokeAsync(System.Func&#96;2<System.Threading.CancellationToken,System.Threading.Tasks.ValueTask> callback, System.Threading.CancellationToken cancellationToken = null)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D185AA2D00587784 | Method | `public System.Threading.Tasks.Task&#96;1<!!0> InvokeAsync<!!0>(System.Func&#96;1<!!0> callback, System.Threading.CancellationToken cancellationToken = null) where !!0: None;` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-BDECEC3A9FE86E3C | Method | `public System.Threading.Tasks.Task&#96;1<!!0> InvokeAsync<!!0>(System.Func&#96;2<System.Threading.CancellationToken,System.Threading.Tasks.ValueTask&#96;1<!!0>> callback, System.Threading.CancellationToken cancellationToken = null) where !!0: None;` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D5B6A8EC763DEB1D | Method | `public static System.Boolean IsKeyLocked(System.Windows.Forms.Keys keyVal)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EF25B48C21DC4CDF | Method | `public static System.Boolean IsMnemonic(System.Char charCode, System.String text)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FBF1019458D02A0E | Method | `public System.Drawing.Size LogicalToDeviceUnits(System.Drawing.Size value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-037330522DA57AEE | Method | `public System.Int32 LogicalToDeviceUnits(System.Int32 value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1D347A92F162A82D | Method | `public System.Void PerformLayout()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4CD887C6BB3BD8AF | Method | `public System.Void PerformLayout(System.Windows.Forms.Control affectedControl, System.String affectedProperty)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-50F9EA21CA9ABB41 | Method | `public System.Drawing.Point PointToClient(System.Drawing.Point p)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-064ECA1BE06CC9E2 | Method | `public System.Drawing.Point PointToScreen(System.Drawing.Point p)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DB8F2F299C77AF5E | Method | `public System.Windows.Forms.PreProcessControlState PreProcessControlMessage(ref System.Windows.Forms.Message msg)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5186991D5B16108A | Method | `public virtual System.Boolean PreProcessMessage(ref System.Windows.Forms.Message msg)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2CEC1BE9CEACBB1A | Method | `public System.Drawing.Rectangle RectangleToClient(System.Drawing.Rectangle r)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-88A34792499E6A0F | Method | `public System.Drawing.Rectangle RectangleToScreen(System.Drawing.Rectangle r)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-32854FDE63DB15FC | Method | `public System.Void RedrawItems(System.Int32 startIndex, System.Int32 endIndex, System.Boolean invalidateOnly)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DD20752F8376B345 | Method | `public virtual System.Void Refresh()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50D3A1391A252332 | Method | `public virtual System.Void ResetBackColor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-99A9E186BE1C06F9 | Method | `public System.Void ResetBindings()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A981356C1259149D | Method | `public virtual System.Void ResetCursor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C8BDAB8FEA75B1B | Method | `public virtual System.Void ResetFont()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BA36E856F3FECB91 | Method | `public virtual System.Void ResetForeColor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-46B714932D8D547C | Method | `public System.Void ResetImeMode()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FD96869FA8883535 | Method | `public virtual System.Void ResetRightToLeft()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B7D1A804BC412B3D | Method | `public virtual System.Void ResetText()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-96872C6149CD8BB9 | Method | `public System.Void ResumeLayout()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-16998926F0319838 | Method | `public System.Void ResumeLayout(System.Boolean performLayout)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9374BFEE12AB30FE | Method | `public System.Void Scale(System.Drawing.SizeF factor)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4F4D61334014F1E1 | Method | `public System.Void Scale(System.Single dx, System.Single dy)` | `System.Windows.Forms.Control` ObsoleteAttribute("This method has been deprecated. Use the Scale(SizeF ratio) method instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-41C7BF04E6CCF945 | Method | `public System.Void Scale(System.Single ratio)` | `System.Windows.Forms.Control` ObsoleteAttribute("This method has been deprecated. Use the Scale(SizeF ratio) method instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1D1359AB5ADD9E81 | Method | `public System.Void ScaleBitmapLogicalToDevice(ref System.Drawing.Bitmap logicalBitmap)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9EC42F4148EE6D96 | Method | `public System.Void Select()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C79572D6692756BE | Method | `public System.Boolean SelectNextControl(System.Windows.Forms.Control ctl, System.Boolean forward, System.Boolean tabStopOnly, System.Boolean nested, System.Boolean wrap)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BBAD9EBC70178A64 | Method | `public System.Void SendToBack()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2200BD678E30190B | Method | `public System.Void SetBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-834F80277D56BDED | Method | `public System.Void SetBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-841C4451C1CB233D | Method | `public System.Void Show()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9035C8E43B0A7F19 | Method | `public System.Void Sort()` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-59E12E2D1891B563 | Method | `public System.Void SuspendLayout()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C7FB88F54ED3F9C0 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.ListView`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7182D7C9DED9BB48 | Method | `public System.Void Update()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D71958AF614A2434 | Property | `System.Windows.Forms.AccessibleObject AccessibilityObject { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1642915438AACB3F | Property | `System.String AccessibleDefaultActionDescription { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4CEA07E3FFE4F15D | Property | `System.String AccessibleDescription { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A677D69FD9488B36 | Property | `System.String AccessibleName { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0AC4F23D6701AE86 | Property | `System.Windows.Forms.AccessibleRole AccessibleRole { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(-1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F0286E47F00BE485 | Property | `System.Windows.Forms.ItemActivation Activation { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F7471CEE9BEE26F4 | Property | `System.Windows.Forms.ListViewAlignment Alignment { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(2); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EBA936CD1EEE4621 | Property | `System.Boolean AllowColumnReorder { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D96DFAB2E2615F96 | Property | `virtual System.Boolean AllowDrop { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5E6D3DCD92B854DD | Property | `virtual System.Windows.Forms.AnchorStyles Anchor { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true); DefaultValueAttribute(5) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-546E3DB7BF34EB73 | Property | `System.Boolean AutoArrange { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6065F7EFB717C4D2 | Property | `virtual System.Drawing.Point AutoScrollOffset { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DefaultValueAttribute(System.Drawing.Point,"0, 0") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D9C76F7D10CB9009 | Property | `virtual System.Boolean AutoSize { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true); DefaultValueAttribute(false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-DA2DA4C3CBECFAD3 | Property | `virtual System.Drawing.Color BackColor { public get; public set; }` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F5AEA72866E035DE | Property | `virtual System.Drawing.Image BackgroundImage { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B855E144810A22E4 | Property | `virtual System.Windows.Forms.ImageLayout BackgroundImageLayout { public get; public set; }` | `System.Windows.Forms.ListView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9AABB11E996D7835 | Property | `System.Boolean BackgroundImageTiled { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B8DB7D8AB7C5652B | Property | `virtual System.Windows.Forms.BindingContext BindingContext { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EBEB71A3395E52E8 | Property | `System.Windows.Forms.BorderStyle BorderStyle { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D623E8B10FD805A0 | Property | `System.Int32 Bottom { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DBA4016A2DD20617 | Property | `System.Drawing.Rectangle Bounds { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-73857BAEA856C1CF | Property | `System.Boolean CanFocus { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-8336126EEDCA6051 | Property | `System.Boolean CanSelect { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-903F92474CA46F09 | Property | `System.Boolean Capture { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-30EEED3B57224B1B | Property | `System.Boolean CausesValidation { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-76DBD04B5EE40F89 | Property | `System.Boolean CheckBoxes { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-58AD7F04B947AF69 | Property | `System.Windows.Forms.ListView+CheckedIndexCollection CheckedIndices { public get; }` | `System.Windows.Forms.ListView` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4A6CC8C4EE805076 | Property | `System.Windows.Forms.ListView+CheckedListViewItemCollection CheckedItems { public get; }` | `System.Windows.Forms.ListView` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E2926737BD994AD | Property | `static System.Boolean CheckForIllegalCrossThreadCalls { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E086CA4A3D52371A | Property | `System.Drawing.Rectangle ClientRectangle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D3F450E59F46062B | Property | `System.Drawing.Size ClientSize { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B9E1FE341407700D | Property | `System.Windows.Forms.ListView+ColumnHeaderCollection Columns { public get; }` | `System.Windows.Forms.ListView` DesignerSerializationVisibilityAttribute(2); EditorAttribute("System.Windows.Forms.Design.ColumnHeaderCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8B4246FB09BFCF00 | Property | `System.String CompanyName { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-88D074C2AC5C0427 | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6883513F79C262AB | Property | `System.Boolean ContainsFocus { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-EB2FC476795E492C | Property | `virtual System.Windows.Forms.ContextMenu ContextMenu { public get; public set; }` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8B971B60794FE515 | Property | `virtual System.Windows.Forms.ContextMenuStrip ContextMenuStrip { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6933CF553777CF71 | Property | `System.Windows.Forms.Control+ControlCollection Controls { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(2) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9B273A5286483736 | Property | `System.Boolean Created { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-48A7CD9551D644B7 | Property | `virtual System.Windows.Forms.Cursor Cursor { public get; public set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-85C06B87696AB093 | Property | `System.Windows.Forms.ControlBindingsCollection DataBindings { public get; }` | `System.Windows.Forms.Control` DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-34FB092E8B4597AE | Property | `virtual System.Object DataContext { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); BindableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A27E55F41E666029 | Property | `static System.Drawing.Color DefaultBackColor { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6E8EC1507A5BF014 | Property | `static System.Drawing.Font DefaultFont { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-991D540811DFA3F2 | Property | `static System.Drawing.Color DefaultForeColor { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A6AA9189F70B0539 | Property | `System.Int32 DeviceDpi { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0DDE4BB920C90FA6 | Property | `virtual System.Drawing.Rectangle DisplayRectangle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2BF845D75096B15F | Property | `System.Boolean Disposing { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9C85AA5B6C228EC8 | Property | `virtual System.Windows.Forms.DockStyle Dock { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true); DefaultValueAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-09F2C683551D9650 | Property | `System.Boolean Enabled { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7D9A3504B52C555A | Property | `virtual System.Boolean Focused { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1B0BB768D4E4504F | Property | `System.Windows.Forms.ListViewItem FocusedItem { public get; public set; }` | `System.Windows.Forms.ListView` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C6543FE5DF671984 | Property | `virtual System.Drawing.Font Font { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5FF7B0E47DECB547 | Property | `virtual System.Drawing.Color ForeColor { public get; public set; }` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50AA465CF6CA9E49 | Property | `System.Boolean FullRowSelect { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-AF63685F2E4E9C21 | Property | `System.Boolean GridLines { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D85A04BDE069D27E | Property | `System.Windows.Forms.ImageList GroupImageList { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D1D1B05D1FF897E1 | Property | `System.Windows.Forms.ListViewGroupCollection Groups { public get; }` | `System.Windows.Forms.ListView` DesignerSerializationVisibilityAttribute(2); LocalizableAttribute(true); EditorAttribute("System.Windows.Forms.Design.ListViewGroupCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2CE22E55E2F0EAFF | Property | `System.IntPtr Handle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5C747D883EA5FE77 | Property | `System.Boolean HasChildren { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1B7A4170272BA420 | Property | `System.Windows.Forms.ColumnHeaderStyle HeaderStyle { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E05109A811E2B76 | Property | `System.Int32 Height { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B46DB1B8527EA18D | Property | `System.Boolean HideSelection { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-56572CB673AE5E6F | Property | `System.Boolean HotTracking { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A80C001F95C3CC4 | Property | `System.Boolean HoverSelection { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DFD8C4367AE3E5CC | Property | `System.Windows.Forms.ImeMode ImeMode { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6EB7D95BAF47357F | Property | `System.Windows.Forms.ListViewInsertionMark InsertionMark { public get; }` | `System.Windows.Forms.ListView` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D41A0A536CAC65A4 | Property | `System.Boolean InvokeRequired { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A5B866E5663B5775 | Property | `System.Boolean IsAccessible { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-601283F2A4F95D06 | Property | `System.Boolean IsAncestorSiteInDesignMode { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-08E18952DD067729 | Property | `System.Boolean IsDisposed { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-DA8A4CE35019814C | Property | `System.Boolean IsHandleCreated { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-89173CDDE7636C6B | Property | `System.Boolean IsMirrored { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B5009CD28ACC056 | Property | `System.Windows.Forms.ListView+ListViewItemCollection Items { public get; }` | `System.Windows.Forms.ListView` DesignerSerializationVisibilityAttribute(2); LocalizableAttribute(true); EditorAttribute("System.Windows.Forms.Design.ListViewItemCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1668D15CAC417C45 | Property | `System.Boolean LabelEdit { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B455E0F851A4AB4D | Property | `System.Boolean LabelWrap { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(true); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CB6D3F50B67BB620 | Property | `System.Windows.Forms.ImageList LargeImageList { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-13A2B15C86661616 | Property | `virtual System.Windows.Forms.Layout.LayoutEngine LayoutEngine { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C1D46545799EE666 | Property | `System.Int32 Left { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-512B872DB2264077 | Property | `System.Collections.IComparer ListViewItemSorter { public get; public set; }` | `System.Windows.Forms.ListView` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8DC9A306C51A2D68 | Property | `System.Drawing.Point Location { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-366DEBB9D26A5D5B | Property | `System.Windows.Forms.Padding Margin { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F8E787EB49E53BD3 | Property | `virtual System.Drawing.Size MaximumSize { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-EEB50C4675C8260F | Property | `virtual System.Drawing.Size MinimumSize { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-81A84EE89187F2E3 | Property | `static System.Windows.Forms.Keys ModifierKeys { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C438B985E8042377 | Property | `static System.Windows.Forms.MouseButtons MouseButtons { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4998F168FA3BAD84 | Property | `static System.Drawing.Point MousePosition { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-815ACE48577CBE66 | Property | `System.Boolean MultiSelect { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8F136C4D9736F2AF | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2A5B7FEDCA9A5BD4 | Property | `System.Boolean OwnerDraw { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AA06461338E78DC1 | Property | `System.Windows.Forms.Padding Padding { public get; public set; }` | `System.Windows.Forms.ListView` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-41DE8A655307EAC0 | Property | `System.Windows.Forms.Control Parent { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4593037ADFDE20B3 | Property | `System.Drawing.Size PreferredSize { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1B4B71CBCE5FAE88 | Property | `System.String ProductName { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-48BB305AA2F9F3DC | Property | `System.String ProductVersion { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F8AB95CDA0035D1C | Property | `System.Boolean RecreatingHandle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1CCA23E23C881FB4 | Property | `System.Drawing.Region Region { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-32E5950886FF847F | Property | `System.Int32 Right { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CFEBD0C1F5BF5E62 | Property | `virtual System.Windows.Forms.RightToLeft RightToLeft { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6ADE8FF210E8BA7B | Property | `virtual System.Boolean RightToLeftLayout { public get; public set; }` | `System.Windows.Forms.ListView` LocalizableAttribute(true); DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2F87C7EB0C654214 | Property | `System.Boolean Scrollable { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-600514A971A06263 | Property | `System.Windows.Forms.ListView+SelectedIndexCollection SelectedIndices { public get; }` | `System.Windows.Forms.ListView` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FFE84D5CCA7CB1EA | Property | `System.Windows.Forms.ListView+SelectedListViewItemCollection SelectedItems { public get; }` | `System.Windows.Forms.ListView` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AD672A882C779246 | Property | `System.Boolean ShowGroups { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-59E97372CBC3BA56 | Property | `System.Boolean ShowItemToolTips { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F93E92330B09B718 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3C5BDCC47C23496F | Property | `System.Drawing.Size Size { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6988C3102889E1D6 | Property | `System.Windows.Forms.ImageList SmallImageList { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A7AB165150C15265 | Property | `System.Windows.Forms.SortOrder Sorting { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-782A5F378DB90FE3 | Property | `System.Windows.Forms.ImageList StateImageList { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7CA54346617582D3 | Property | `System.Int32 TabIndex { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-61D66C836C96EA9C | Property | `System.Boolean TabStop { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-181B7B360AB6E191 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-018ACB97E2632498 | Property | `virtual System.String Text { public get; public set; }` | `System.Windows.Forms.ListView` BrowsableAttribute(false); BindableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-685AE513DB722B90 | Property | `System.Drawing.Size TileSize { public get; public set; }` | `System.Windows.Forms.ListView` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B4887DD2D3A9F5BE | Property | `System.Int32 Top { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-494DB8FCB7B46CAC | Property | `System.Windows.Forms.ListViewItem TopItem { public get; public set; }` | `System.Windows.Forms.ListView` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B10F6D0971A8030 | Property | `System.Windows.Forms.Control TopLevelControl { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0D5E7A3F223BC353 | Property | `System.Boolean UseCompatibleStateImageBehavior { public get; public set; }` | `System.Windows.Forms.ListView` BrowsableAttribute(false); DefaultValueAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-AC4F2E9E6D5EF296 | Property | `System.Boolean UseWaitCursor { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(false); BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A6F608E64CD486C0 | Property | `System.Windows.Forms.View View { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3C576DDF08AFBF36 | Property | `System.Int32 VirtualListSize { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7626E18D67A42B90 | Property | `System.Boolean VirtualMode { public get; public set; }` | `System.Windows.Forms.ListView` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-76EE35EDF7E19F01 | Property | `System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0234BE380E2F117F | Property | `System.Int32 Width { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C74D83B65A1B8EFA | Property | `System.Windows.Forms.IWindowTarget WindowTarget { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-D3F81FCE21A966F5 | Method | `protected System.Void AccessibilityNotifyClients(System.Windows.Forms.AccessibleEvents accEvent, System.Int32 objectID, System.Int32 childID)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8974DE70363FC9CD | Method | `protected internal System.Void AccessibilityNotifyClients(System.Windows.Forms.AccessibleEvents accEvent, System.Int32 childID)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8F585628C8263B00 | Method | `protected virtual System.Windows.Forms.AccessibleObject CreateAccessibilityInstance()` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-88C76BDAC97A52D1 | Method | `protected virtual System.Windows.Forms.Control+ControlCollection CreateControlsInstance()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5C1D29BE3A2E8A2A | Method | `protected virtual System.Void CreateHandle()` | `System.Windows.Forms.ListView`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-64E1E918A717EB22 | Method | `protected virtual System.Void DefWndProc(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E66D6CDA3E1C40EF | Method | `protected virtual System.Void DestroyHandle()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1584E224608DE554 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.ListView`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C86F30E6F2B1DAFF | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-498BAEDCBBE11940 | Method | `protected virtual System.Windows.Forms.AccessibleObject GetAccessibilityObjectById(System.Int32 objectId)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4BCB6111A6948C21 | Method | `protected System.Windows.Forms.AutoSizeMode GetAutoSizeMode()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-DA92810D2708C57C | Method | `protected virtual System.Drawing.Rectangle GetScaledBounds(System.Drawing.Rectangle bounds, System.Drawing.SizeF factor, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-44E0743CBBC735C3 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-192880E7E2EFC035 | Method | `protected System.Boolean GetStyle(System.Windows.Forms.ControlStyles flag)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-776DD3741FDC67E6 | Method | `protected System.Boolean GetTopLevel()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-78328F56356260B3 | Method | `protected virtual System.Void InitLayout()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9977A2E76DFB867E | Method | `protected System.Void InvokeGotFocus(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-67A231D95EFF1612 | Method | `protected System.Void InvokeLostFocus(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EA1E7A29506BC055 | Method | `protected System.Void InvokeOnClick(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E8ED310625EB109 | Method | `protected System.Void InvokePaint(System.Windows.Forms.Control c, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E2ABB9B1142D82BC | Method | `protected System.Void InvokePaintBackground(System.Windows.Forms.Control c, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A8A7074013526916 | Method | `protected virtual System.Boolean IsInputChar(System.Char charCode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-02B0238F2E200E79 | Method | `protected virtual System.Boolean IsInputKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B9F422C492AE5F01 | Method | `protected virtual System.Void NotifyInvalidate(System.Drawing.Rectangle invalidatedArea)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F086AB25785A1D4F | Method | `protected virtual System.Void OnAfterLabelEdit(System.Windows.Forms.LabelEditEventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-59008B219088A0AD | Method | `protected virtual System.Void OnAutoSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-00F99D45721E90BF | Method | `protected virtual System.Void OnBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-EEF3B35506FEB33B | Method | `protected virtual System.Void OnBackgroundImageChanged(System.EventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-495FA1383B7DCE6F | Method | `protected virtual System.Void OnBackgroundImageLayoutChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C9F0DE6CC076557 | Method | `protected virtual System.Void OnBeforeLabelEdit(System.Windows.Forms.LabelEditEventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-008D646AE8BE0AA6 | Method | `protected virtual System.Void OnBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E7268C41B9C05B2C | Method | `protected virtual System.Void OnCacheVirtualItems(System.Windows.Forms.CacheVirtualItemsEventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FA7C38D8876ADD90 | Method | `protected virtual System.Void OnCausesValidationChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E82676C00BB5B0D9 | Method | `protected virtual System.Void OnChangeUICues(System.Windows.Forms.UICuesEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-33139A80E1A41CE1 | Method | `protected virtual System.Void OnClick(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B175333B63FA56C5 | Method | `protected virtual System.Void OnClientSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-80205EF6A4CFDB06 | Method | `protected virtual System.Void OnColumnClick(System.Windows.Forms.ColumnClickEventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FEBEA627AA3AD3BB | Method | `protected virtual System.Void OnColumnReordered(System.Windows.Forms.ColumnReorderedEventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DC01D8284280B279 | Method | `protected virtual System.Void OnColumnWidthChanged(System.Windows.Forms.ColumnWidthChangedEventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0955FE8506FE242A | Method | `protected virtual System.Void OnColumnWidthChanging(System.Windows.Forms.ColumnWidthChangingEventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-55606777B1D98FEA | Method | `protected virtual System.Void OnContextMenuChanged(System.EventArgs e)` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BFCA8B53B750DB78 | Method | `protected virtual System.Void OnContextMenuStripChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2079DEEAD8B341BA | Method | `protected virtual System.Void OnControlAdded(System.Windows.Forms.ControlEventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-90833E5D7D7018D3 | Method | `protected virtual System.Void OnControlRemoved(System.Windows.Forms.ControlEventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9CE9A30FEB2D27F2 | Method | `protected virtual System.Void OnCreateControl()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FC5BC436676E678D | Method | `protected virtual System.Void OnCursorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-44FCD1F069862C24 | Method | `protected virtual System.Void OnDataContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-30AA1DDA5F2A200F | Method | `protected virtual System.Void OnDockChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A99A049C4E74BC53 | Method | `protected virtual System.Void OnDoubleClick(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-12A76C1EB75AB997 | Method | `protected virtual System.Void OnDpiChangedAfterParent(System.EventArgs e)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F187BA29D9AB5C25 | Method | `protected virtual System.Void OnDpiChangedBeforeParent(System.EventArgs e)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8206F8A67B9F44FD | Method | `protected virtual System.Void OnDragDrop(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FBAC7E7D870912AA | Method | `protected virtual System.Void OnDragEnter(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CDD67C8E67DD5FDC | Method | `protected virtual System.Void OnDragLeave(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7B97A20D0047E842 | Method | `protected virtual System.Void OnDragOver(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-57BCD9ADB2F13C11 | Method | `protected virtual System.Void OnDrawColumnHeader(System.Windows.Forms.DrawListViewColumnHeaderEventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-03BA6F6D1404096C | Method | `protected virtual System.Void OnDrawItem(System.Windows.Forms.DrawListViewItemEventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5B4C44AE73B96775 | Method | `protected virtual System.Void OnDrawSubItem(System.Windows.Forms.DrawListViewSubItemEventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-30728B81945750A0 | Method | `protected virtual System.Void OnEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9B1479F95B57F15B | Method | `protected internal virtual System.Void OnEnter(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E2EE3B46C22E8843 | Method | `protected virtual System.Void OnFontChanged(System.EventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-19A76DA68643B392 | Method | `protected virtual System.Void OnForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E013F21E722158D2 | Method | `protected virtual System.Void OnGiveFeedback(System.Windows.Forms.GiveFeedbackEventArgs gfbevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-47A4156E40C528D5 | Method | `protected virtual System.Void OnGotFocus(System.EventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6B0975B019D8A818 | Method | `protected virtual System.Void OnGroupCollapsedStateChanged(System.Windows.Forms.ListViewGroupEventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2A2202633F2028B9 | Method | `protected virtual System.Void OnGroupTaskLinkClick(System.Windows.Forms.ListViewGroupEventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC0C47AE0D3C3BD8 | Method | `protected virtual System.Void OnHandleCreated(System.EventArgs e)` | `System.Windows.Forms.ListView`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F61182816D3FCCA6 | Method | `protected virtual System.Void OnHandleDestroyed(System.EventArgs e)` | `System.Windows.Forms.ListView`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F73208F3E7C0DD85 | Method | `protected virtual System.Void OnHelpRequested(System.Windows.Forms.HelpEventArgs hevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-31F3534966CD5C71 | Method | `protected virtual System.Void OnImeModeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E97D68AAA7CF51B | Method | `protected virtual System.Void OnInvalidated(System.Windows.Forms.InvalidateEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-739D75F600E3E8DA | Method | `protected virtual System.Void OnItemActivate(System.EventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-768F422C04D862A9 | Method | `protected virtual System.Void OnItemCheck(System.Windows.Forms.ItemCheckEventArgs ice)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-73E2E6B69FEF458B | Method | `protected virtual System.Void OnItemChecked(System.Windows.Forms.ItemCheckedEventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-92A8605544131474 | Method | `protected virtual System.Void OnItemDrag(System.Windows.Forms.ItemDragEventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6EB554805B3C0DBD | Method | `protected virtual System.Void OnItemMouseHover(System.Windows.Forms.ListViewItemMouseHoverEventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F9E7056A04A229E7 | Method | `protected virtual System.Void OnItemSelectionChanged(System.Windows.Forms.ListViewItemSelectionChangedEventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F7FA6ADD82D834D8 | Method | `protected virtual System.Void OnKeyDown(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E5DB998071FF797 | Method | `protected virtual System.Void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-930CC190C8D3A9E0 | Method | `protected virtual System.Void OnKeyUp(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-01759C8574DFFB69 | Method | `protected virtual System.Void OnLayout(System.Windows.Forms.LayoutEventArgs levent)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E0F7B6D6FAF05F99 | Method | `protected internal virtual System.Void OnLeave(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-10C48B9A5C5A6910 | Method | `protected virtual System.Void OnLocationChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7DC6DA3093B5EC67 | Method | `protected virtual System.Void OnLostFocus(System.EventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-38F7F44FCB9BD844 | Method | `protected virtual System.Void OnMarginChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-716C13284D07A6BC | Method | `protected virtual System.Void OnMouseCaptureChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-14E5701DDD6B79E0 | Method | `protected virtual System.Void OnMouseClick(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16D5F942774ABF65 | Method | `protected virtual System.Void OnMouseDoubleClick(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C26CCF060ABDC683 | Method | `protected virtual System.Void OnMouseDown(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C4956DE9CFB89BD1 | Method | `protected virtual System.Void OnMouseEnter(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FA64FD5F12868418 | Method | `protected virtual System.Void OnMouseHover(System.EventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A0026466EAE21E6E | Method | `protected virtual System.Void OnMouseLeave(System.EventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F17FBD5D6BEC36D | Method | `protected virtual System.Void OnMouseMove(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E9B11AE817AF7132 | Method | `protected virtual System.Void OnMouseUp(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1507906B12B93E8E | Method | `protected virtual System.Void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F37C58DED6E3065D | Method | `protected virtual System.Void OnMove(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-8352F7F3468D0037 | Method | `protected virtual System.Void OnNotifyMessage(System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-866E4F28AA2D646A | Method | `protected virtual System.Void OnPaddingChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-8F2BEDFCE337E4AB | Method | `protected virtual System.Void OnPaint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B7478A816CB4E2B0 | Method | `protected virtual System.Void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8E246E0859D00A6A | Method | `protected virtual System.Void OnParentBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-314CFA7F6492BEEC | Method | `protected virtual System.Void OnParentBackgroundImageChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9727C338EE75A853 | Method | `protected virtual System.Void OnParentBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B9634395F71DB330 | Method | `protected virtual System.Void OnParentChanged(System.EventArgs e)` | `System.Windows.Forms.ListView`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-96692AE03875644C | Method | `protected virtual System.Void OnParentCursorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16D1332080C08FDA | Method | `protected virtual System.Void OnParentDataContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C5C1425E07AE1C8E | Method | `protected virtual System.Void OnParentEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-341FFFF996C6BBCD | Method | `protected virtual System.Void OnParentFontChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3EEF31DE197E86B0 | Method | `protected virtual System.Void OnParentForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6BC8286E2CB606B0 | Method | `protected virtual System.Void OnParentRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-207707528D73D28F | Method | `protected virtual System.Void OnParentVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C00FEFED1238BEE9 | Method | `protected virtual System.Void OnPreviewKeyDown(System.Windows.Forms.PreviewKeyDownEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-26AD4B054680F7D9 | Method | `protected virtual System.Void OnPrint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-287D716A038A1F0E | Method | `protected virtual System.Void OnQueryContinueDrag(System.Windows.Forms.QueryContinueDragEventArgs qcdevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-81B64D37D2826786 | Method | `protected virtual System.Void OnRegionChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AB624235FC67AC4B | Method | `protected virtual System.Void OnResize(System.EventArgs e)` | `System.Windows.Forms.ListView`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-36DA63493C7892CE | Method | `protected virtual System.Void OnRetrieveVirtualItem(System.Windows.Forms.RetrieveVirtualItemEventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CB30A32F033FBC2F | Method | `protected virtual System.Void OnRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B43DB71F79B6C617 | Method | `protected virtual System.Void OnRightToLeftLayoutChanged(System.EventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0318EDAC997A7391 | Method | `protected virtual System.Void OnSearchForVirtualItem(System.Windows.Forms.SearchForVirtualItemEventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6B3DD2F808DFD01B | Method | `protected virtual System.Void OnSelectedIndexChanged(System.EventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-608A11EF8ED59686 | Method | `protected virtual System.Void OnSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A94C0524C1ACE2B4 | Method | `protected virtual System.Void OnStyleChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B935BC3462754532 | Method | `protected virtual System.Void OnSystemColorsChanged(System.EventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D715B3EF751814B3 | Method | `protected virtual System.Void OnTabIndexChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-80A7BDD47F09699C | Method | `protected virtual System.Void OnTabStopChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-230B1BF528456565 | Method | `protected virtual System.Void OnTextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7602134A26990187 | Method | `protected virtual System.Void OnValidated(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4F272FDADEF1BFFF | Method | `protected virtual System.Void OnValidating(System.ComponentModel.CancelEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0EA6FE612887558 | Method | `protected virtual System.Void OnVirtualItemsSelectionRangeChanged(System.Windows.Forms.ListViewVirtualItemsSelectionRangeChangedEventArgs e)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8783511C62D7DF09 | Method | `protected virtual System.Void OnVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2BD77E20B7233B2D | Method | `protected virtual System.Boolean ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4F9D80C11028F184 | Method | `protected virtual System.Boolean ProcessDialogChar(System.Char charCode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DAC430ACB37FC13B | Method | `protected virtual System.Boolean ProcessDialogKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-304EEA77EFAA6CCA | Method | `protected virtual System.Boolean ProcessKeyEventArgs(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC2BFAC119EF68BA | Method | `protected internal virtual System.Boolean ProcessKeyMessage(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-78BBE0DB6BC662D2 | Method | `protected virtual System.Boolean ProcessKeyPreview(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CB84928C66599DD3 | Method | `protected internal virtual System.Boolean ProcessMnemonic(System.Char charCode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6D05C3AE6CBC4A2F | Method | `protected System.Void RaiseDragEvent(System.Object key, System.Windows.Forms.DragEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-607424ABF4D1F18E | Method | `protected System.Void RaiseKeyEvent(System.Object key, System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D25C245170ACA9C | Method | `protected System.Void RaiseMouseEvent(System.Object key, System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7BF1B6A1F074BB5E | Method | `protected System.Void RaisePaintEvent(System.Object key, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4127FD477BEB35E9 | Method | `protected System.Void RealizeProperties()` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7A5A604A2801472B | Method | `protected System.Void RecreateHandle()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0793A5F8264F7339 | Method | `protected static System.Boolean ReflectMessage(System.IntPtr hWnd, ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FB90561255E28C46 | Method | `protected virtual System.Void RescaleConstantsForDpi(System.Int32 deviceDpiOld, System.Int32 deviceDpiNew)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5170AA5A7304C76E | Method | `protected System.Void ResetMouseEventArgs()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3A7168024FDE9BD4 | Method | `protected System.Drawing.ContentAlignment RtlTranslateAlignment(System.Drawing.ContentAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-830F8C3E9CCA7BC4 | Method | `protected System.Windows.Forms.HorizontalAlignment RtlTranslateAlignment(System.Windows.Forms.HorizontalAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7A139A94BC4CDDB3 | Method | `protected System.Windows.Forms.LeftRightAlignment RtlTranslateAlignment(System.Windows.Forms.LeftRightAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C808CBB7A5BBB527 | Method | `protected internal System.Drawing.ContentAlignment RtlTranslateContent(System.Drawing.ContentAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-79672F65ED78440B | Method | `protected System.Windows.Forms.HorizontalAlignment RtlTranslateHorizontal(System.Windows.Forms.HorizontalAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2A93E13853BCF56F | Method | `protected System.Windows.Forms.LeftRightAlignment RtlTranslateLeftRight(System.Windows.Forms.LeftRightAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8401CD830FE3B3D3 | Method | `protected virtual System.Void ScaleControl(System.Drawing.SizeF factor, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C60B29F98AE5153A | Method | `protected virtual System.Void ScaleCore(System.Single dx, System.Single dy)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D25600F55ABDB15 | Method | `protected virtual System.Void Select(System.Boolean directed, System.Boolean forward)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-57ECC444E1046D70 | Method | `protected System.Void SetAutoSizeMode(System.Windows.Forms.AutoSizeMode mode)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-527FD9F0B2AADC6D | Method | `protected virtual System.Void SetBoundsCore(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1C0FE1025A6B2361 | Method | `protected virtual System.Void SetClientSizeCore(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C22E360EB2BE6C39 | Method | `protected System.Void SetStyle(System.Windows.Forms.ControlStyles flag, System.Boolean value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8E2E1582B3F2F004 | Method | `protected System.Void SetTopLevel(System.Boolean value)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-83968F12BA47BD0B | Method | `protected virtual System.Void SetVisibleCore(System.Boolean value)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7EE693B31296C6F2 | Method | `protected virtual System.Drawing.Size SizeFromClientSize(System.Drawing.Size clientSize)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D02005F1CD01347C | Method | `protected System.Void UpdateBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9D1042FD0CF12BE3 | Method | `protected System.Void UpdateBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Int32 clientWidth, System.Int32 clientHeight)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D22CCC9850A0D00D | Method | `protected internal System.Void UpdateBounds()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D2A8874BC2D25F54 | Method | `protected System.Void UpdateExtendedStyles()` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6C0809084DFE939B | Method | `protected System.Void UpdateStyles()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-93C5235568F68A3A | Method | `protected System.Void UpdateZOrder()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CF64D824CC5E2F1B | Method | `protected virtual System.Void WndProc(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7F60118364EAA924 | Property | `virtual System.Boolean CanEnableIme { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A01EB1C7DA92990F | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F2BA6AAE1B027161 | Property | `virtual System.Windows.Forms.CreateParams CreateParams { protected get; }` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1C59917E68371979 | Property | `virtual System.Windows.Forms.Cursor DefaultCursor { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B550B3730AF7348 | Property | `virtual System.Windows.Forms.ImeMode DefaultImeMode { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-89634A873EC505E9 | Property | `virtual System.Windows.Forms.Padding DefaultMargin { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-836AD1533020A880 | Property | `virtual System.Drawing.Size DefaultMaximumSize { protected get; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-85DE8E63E5A0F462 | Property | `virtual System.Drawing.Size DefaultMinimumSize { protected get; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D92F5BC9E2FED8E6 | Property | `virtual System.Windows.Forms.Padding DefaultPadding { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E8832521B3B60E47 | Property | `virtual System.Drawing.Size DefaultSize { protected get; }` | `System.Windows.Forms.ListView`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E2C44EDF0F8E7E53 | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C142219E73442864 | Property | `virtual System.Boolean DoubleBuffered { protected get; protected set; }` | `System.Windows.Forms.ListView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5B29B9EB741C4D3F | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3D5F645208805B8A | Property | `System.Int32 FontHeight { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4269ACB3762FDB03 | Property | `virtual System.Windows.Forms.ImeMode ImeModeBase { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C91EE2660A311570 | Property | `static System.Windows.Forms.ImeMode PropagatingImeMode { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-853DA07688420A77 | Property | `System.Boolean RenderRightToLeft { protected internal get; }` | `System.Windows.Forms.Control` ObsoleteAttribute("This property has been deprecated. Please use RightToLeft instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-87E289D1ECF64A98 | Property | `System.Boolean ResizeRedraw { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-745C95A09F18A884 | Property | `virtual System.Boolean ScaleChildren { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-492D0CC78372E099 | Property | `virtual System.Boolean ShowFocusCues { protected internal get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D43A49E2C3C1E9D | Property | `virtual System.Boolean ShowKeyboardCues { protected internal get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
