# ListBox

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/ListBox.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · 🟡

已有 LISTBOX、单选 SelectedIndex/SelectedItem、增项与清空和 LBN_SELCHANGE；Items 为只覆盖部分操作的 ObjectCollection。缺 ListControl、数据绑定、多选、格式化、OwnerDraw 和大多数列表特有属性。

- 标准继承/契约：`ListBox : ListControl : Control`。
- 代码证据：[当前源码](../../../Lumina.Forms/ListBox.cs)。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `ListBox-01` | 接入 ListControl 并比对 ObjectCollection、SelectedIndexCollection、SelectedObjectCollection 及 IList 契约；不让 IEnumerable-only 集合冒充完整兼容。 |
| ⬜ | P0 | `ListBox-02` | 默认 SelectionMode=One、SelectedIndex=-1、IntegralHeight=true、Sorted=false、MultiColumn=false、DrawMode=Normal、HorizontalScrollbar=false；BorderStyle、ItemHeight/DefaultSize 用同机 WinForms 样本固定。 |
| ⬜ | P1 | `ListBox-03` | Items 全操作和 FindString/FindStringExact、GetItemText、GetItemRectangle、IndexFromPoint；句柄前后插入删除、替换、排序、重复值保持一致。 |
| ⬜ | P1 | `ListBox-04` | SelectionMode 的 None/One/MultiSimple/MultiExtended，SelectedIndices/SelectedItems、SetSelected/GetSelected/ClearSelected；Ctrl/Shift、拖动、键盘和程序改变的 SelectedIndexChanged 次数/顺序一致。 |
| ⬜ | P1 | `ListBox-05` | DataSource/DisplayMember/ValueMember/SelectedValue 与 CurrencyManager 接线；源 Reset、当前项删除、显示属性变化，Text/选择集合双向一致。 |
| ⬜ | P1 | `ListBox-06` | SelectedValueChanged、TextChanged、Click/DoubleClick、MouseDoubleClick 与键盘事件的用户/程序差分；异常索引、无匹配项、SelectionMode 切换后的选择规则。 |
| ⬜ | P2 | `ListBox-07` | TopIndex、HorizontalExtent、ScrollAlwaysVisible、MultiColumn、ColumnWidth、IntegralHeight；滚轮、PageUp/Down、Home/End、输入前缀搜索和 RTL。 |
| ⬜ | P2 | `ListBox-08` | DrawMode/DrawItem/MeasureItem、ItemHeight/RefreshItem/RefreshItems、BeginUpdate/EndUpdate；DPI/字体变化后布局与滚动位置不漂移。 |
| ⬜ | P2 | `ListBox-09` | ListControl 的 Format、FormatString、FormatInfo，空项/DBNull/文化格式、动态对象的已声明 AOT 元数据；不把 ToString 当全量格式化。 |
| ⬜ | P3 | `ListBox-10` | 重建原生控件时保留多选、TopIndex、数据绑定；Dispose 释放模型、字体/绘制资源和源订阅。三平台逐项验证多列、owner draw 的移植差异。 |

## 三平台实现分析

### Windows

保持 LISTBOX；LBS_EXTENDEDSEL/MULTIPLESEL/NODATA/OWNERDRAW 等按属性配置；`LB_GETSELITEMS/SETSEL/SETTOPINDEX/SETITEMHEIGHT` 完成状态，WM_DRAWITEM/WM_MEASUREITEM 与 LBN_SELCHANGE 回到托管事件；改样式触发重建时恢复多选。

### Linux · GTK 4

GtkListView + GtkSingleSelection/GtkMultiSelection/无选择模型，置于 GtkScrolledWindow。模型工厂构造行，MultiColumn 需 GtkGridView 或原生布局组合；WinForms 的列方向、IntegralHeight 和像素 ItemHeight 不会自动等价。

### macOS · AppKit

NSScrollView + NSTableView（单列）及 delegate/dataSource；allowsMultipleSelection 和 index set 对应多选。MultiSimple 的点击规则、扩展选择与前缀搜索需适配；MultiColumn 可用 NSCollectionView，必须验证其导航规则差异。

## 边界、AOT 与验收

继承绑定为 P0/P1 前置。OwnerDraw 的 Graphics/EventArgs 受公共绘图跨平台边界约束；不能删除该分母或声称原生行工厂已等价。键盘焦点和当前项必须与选择集合分别建模。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- 2026-10-16 · 批次023：基础Control启用标准UserPaint后，本控件构造显式保留UserPaint=false，使现有系统窗口类过程及原生字体/绘制继续生效。仅属消息与绘制基础的必要衔接，不代表本控件完整Styles、Paint事件或视觉/输入兼容完成，见[023](../batches/023.md)。

- `108ef2b` · 2026-04-08：接入 Win32 深色模式。
- `2e8431f` · 2026-04-01：主题和 Demo UI 改进。
- `ce86ab5` · 2026-03-31：公共主题表面与颜色刷新。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.listbox?view=windowsdesktop-10.0
- https://docs.gtk.org/gtk4/class.ListView.html
- https://docs.gtk.org/gtk4/class.MultiSelection.html
- https://developer.apple.com/documentation/appkit/nstableview
