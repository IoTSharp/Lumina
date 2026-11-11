# ListView

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/ListView.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · 🟡

已有 SysListView32、Items/Columns 所属关系与真实 LVM 项/列插入更新；Text/SubItems/列修改同步到原生，结构重建按项身份保留选择/焦点/状态图像。View默认已改LargeIcon，FullRowSelect/GridLines按属性映射。完整集合API、ImageList/Group、虚拟模式、通知/样式/复杂重入和运行验收仍待。

- 标准继承/契约：`ListView : Control`；需要 ListViewItem、ColumnHeader、ListViewGroup、相关嵌套集合和事件参数类型。
- 代码证据：[当前源码](../../../Lumina.Forms/ListView.cs)。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| 🟡 | P0 | `ListView-01` | 基础条目构造与Text/首子项一致性保留；本轮补Items/Columns的宿主所属、替换移除释放与真实原生同步。其余构造/精确集合接口及ColumnHeader完整继承默认仍待，父项保持部分。 |
| 🟡 | P0 | `ListView-02` | 已修View=LargeIcon、FullRowSelect=false/GridLines=false的实际native样式；其余MultiSelect、HideSelection、LabelEdit、CheckBoxes、VirtualMode默认及完整原生读回仍待。 |
| 🟡 | P1 | `ListView-03` | 已补Items/Columns增删插入/清空、Items索引替换/Name键查找、跨宿主拒绝及LVM_INSERTITEM/SETITEM/列同步；子项文字变更立即同步。完整标准集合类型/重载、列Name键接口及原生通知重入的运行检查仍待。 |
| ⬜ | P1 | `ListView-04` | SelectedItems/SelectedIndices、FocusedItem、TopItem、EnsureVisible/GetItemAt/HitTest/GetItemRect；鼠标、Ctrl/Shift、键盘、程序选择产生 SelectedIndexChanged/ItemSelectionChanged 的顺序和批量次数。 |
| 🟡 | P1 | `ListView-05` | View五值通过LVM_SETVIEW生效，ColumnHeader文字/宽度/对齐更新重新提交；完整Alignment/AutoArrange/TileSize、DisplayIndex及可取消表头通知仍待。 |
| ⬜ | P1 | `ListView-06` | CheckBoxes、CheckedItems/CheckedIndices、StateImageList、ItemCheck（可改 NewValue）、ItemChecked 与 DoubleClick/ItemActivate；区分 checkbox 点击和普通选择。 |
| ⬜ | P1 | `ListView-07` | LabelEdit/BeginEdit、BeforeLabelEdit/AfterLabelEdit（取消规则）、LabelWrap、HotTracking/HoverSelection、ItemDrag/ItemMouseHover；F2、Enter、Esc 和编辑后文本提交。 |
| ⬜ | P2 | `ListView-08` | SmallImageList/LargeImageList/StateImageList、ImageIndex/ImageKey、子项 ForeColor/BackColor/Font、UseItemStyleForSubItems，图像列表替换和 Dispose 不泄漏或双重释放。 |
| ⬜ | P2 | `ListView-09` | Sorting/ListViewItemSorter/Sort、Groups/ShowGroups、ListViewGroup 名称和所属项、分组删除回落；表头排序与集合索引/显示顺序分开验证。 |
| ⬜ | P2 | `ListView-10` | VirtualMode/VirtualListSize/RetrieveVirtualItem/CacheVirtualItems/SearchForVirtualItem；空项、搜索、滚动、选择及虚拟模式禁止的集合操作，与真实 WinForms 对照。 |
| ⬜ | P2 | `ListView-11` | OwnerDraw、DrawColumnHeader/DrawItem/DrawSubItem、BeginUpdate/EndUpdate、双缓冲与重入；ListViewItem.Position/ArrangeIcons 仅在合法模式按标准生效。 |
| ⬜ | P2 | `ListView-12` | 重建句柄、父级切换、字体/DPI/RTL/高对比度、滚动到大数据末尾，选中/焦点/组/编辑状态恢复；每个通知回调均从 UI 线程发出。 |
| ⬜ | P3 | `ListView-13` | GTK/AppKit 各模式原生切换与项身份共用；无法等价的 Win32 排列坐标、组、GDI OwnerDraw 明确登记，不能把 Details 模式覆盖当完整 ListView。 |
| ⬜ | P3 | `ListView-14` | AOT 双目标 Demo 静态列项设计器往返、10万项虚拟列表以及反复创建/释放；记录交互延时和内存趋势而非先引入通用虚拟化框架。 |

## 三平台实现分析

### Windows

SysListView32 + Header：使用 LVM_INSERTITEM/SETITEM/DELETEITEM/INSERTCOLUMN/SETCOLUMN、LVM_SETIMAGELIST/SETVIEW/SETGROUPINFO；LVN_ITEMCHANGING/ITEMCHANGED/BEGINLABELEDIT/ENDLABELEDIT/GETDISPINFO 等反射到事件。ExtendedStyles 必须按实际属性设置，不无条件强开与默认冲突的功能。OwnerData 与普通模式走同一项身份规则。

### Linux · GTK 4

Details 用 GtkColumnView；List 用 GtkListView；图标模式用 GtkGridView + 原生 Image/Label；GListModel 与 SelectionModel 共用身份。GTK 4 的这些部件有真实原生行/单元格，**没有一个组件完整等价 WinForms ListView**：Tile、固定坐标排列、组、表头事件、虚拟回调时机必须逐项补足或明确差异。

### macOS · AppKit

Details/List 用 NSTableView；LargeIcon/SmallIcon/Tile 用 NSCollectionView，置于 NSScrollView；列使用 NSTableColumn，图像用 NSImage。NSCollectionView 的布局不是 Win32 图标坐标模型，跨模式切换需保留 item 对象与选择；分组可用 supplementary view，但不自动等价 ListViewGroup。

## 边界、AOT 与验收

依赖 ImageList、输入/通知路由、可访问性、绘图边界。ListView 本身没有 DataSource，禁止额外发明绑定属性冒充标准。该项达到 98% 的必要条件是所有模式/集合/虚拟化/事件分类均有证据，不能只对展示页计分。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 已完成任务

当前尚无已关闭的代码范围；变更历史统一见根目录 [CHANGELOG](../../../CHANGELOG.md)。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.listview?view=windowsdesktop-10.0
- https://learn.microsoft.com/windows/win32/controls/list-view-controls-overview
- https://docs.gtk.org/gtk4/class.ColumnView.html
- https://docs.gtk.org/gtk4/class.GridView.html
- https://developer.apple.com/documentation/appkit/nstableview
- https://developer.apple.com/documentation/appkit/nscollectionview
