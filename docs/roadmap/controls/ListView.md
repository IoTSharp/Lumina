# ListView

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/ListView.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · 🟡

已有 SysListView32 与深色主题、Items/Columns 托管集合；没有插入原生项/列的实现。`ListViewItem(string)` 在初始化 SubItems 前设置 Text 导致 NullReferenceException，已由审计探针复现。View 默认 Details（标准 LargeIcon），FullRowSelect 属性默认 false 却无条件设置原生 FULLROWSELECT 样式。

- 标准继承/契约：`ListView : Control`；需要 ListViewItem、ColumnHeader、ListViewGroup、相关嵌套集合和事件参数类型。
- 代码证据：[当前源码](../../../Lumina.Forms/ListView.cs)。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `ListView-01` | 修复 ListViewItem 构造和 Text/SubItems[0] 同步；Items.Add 返回值/重载、集合所有权和 ColumnHeader 继承/默认值匹配；不能靠 Demo 避开构造缺陷。 |
| ⬜ | P0 | `ListView-02` | 默认 View=LargeIcon、MultiSelect=true、FullRowSelect=false、GridLines=false、HideSelection=false、LabelEdit=false、CheckBoxes=false、VirtualMode=false；原生样式读回与托管一致；HideSelection 按 .NET 10 实测，不能沿用旧版默认。 |
| ⬜ | P1 | `ListView-03` | Items/Columns 的 Add/Insert/Remove/Clear/索引写入、Name 键查找、跨宿主归属；映射到 LVM_INSERTITEM/INSERTCOLUMN 等，分别检查句柄前后实际项列数量与文本。 |
| ⬜ | P1 | `ListView-04` | SelectedItems/SelectedIndices、FocusedItem、TopItem、EnsureVisible/GetItemAt/HitTest/GetItemRect；鼠标、Ctrl/Shift、键盘、程序选择产生 SelectedIndexChanged/ItemSelectionChanged 的顺序和批量次数。 |
| ⬜ | P1 | `ListView-05` | View 五种模式、Alignment/AutoArrange/TileSize、HeaderStyle、ColumnHeader.Width/DisplayIndex、ColumnClick/ColumnWidthChanging（可取消）/ColumnWidthChanged/ColumnReordered（可取消）；动态切换后项身份不变。 |
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

## 修改历史

- `2fb513a` · 2026-04-09：提交包含 ListView 功能和异步调用增强；现有集合仍未与原生内容同步。
- `2545995` · 2026-04-08：主题应用优化。
- `10dc1f4` · 2026-04-08：Win32 深色模式与窗口主题重构。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.listview?view=windowsdesktop-10.0
- https://learn.microsoft.com/windows/win32/controls/list-view-controls-overview
- https://docs.gtk.org/gtk4/class.ColumnView.html
- https://docs.gtk.org/gtk4/class.GridView.html
- https://developer.apple.com/documentation/appkit/nstableview
- https://developer.apple.com/documentation/appkit/nscollectionview
