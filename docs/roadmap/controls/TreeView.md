# TreeView

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/TreeView.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · 🟡

已有 SysTreeView32、深色样式与 TreeNode/Nodes 托管结构；没有 TVM_INSERTITEM 及节点通知接线。审计中托管 Nodes.Count=1，TVM_GETCOUNT=0；显示宿主存在不能算树内容已实现。

- 标准继承/契约：`TreeView : Control`；TreeNode、TreeNodeCollection、TreeViewEventArgs/TreeViewCancelEventArgs 等为独立契约。
- 代码证据：[当前源码](../../../Lumina.Forms/TreeView.cs)。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `TreeView-01` | TreeNode/Nodes 建立 owner/parent/handle/index 关系；Add/Insert/Remove/Clear 同步原生，禁止同节点多宿主和循环父子结构；构造前后数据可见。 |
| ⬜ | P0 | `TreeView-02` | 默认 ShowLines=true、ShowRootLines=true、ShowPlusMinus=true、HideSelection=true、HotTracking=false、CheckBoxes=false、LabelEdit=false、Sorted=false；当前强开样式逐项对齐。 |
| ⬜ | P1 | `TreeView-03` | SelectedNode、TopNode、VisibleCount、GetNodeAt/HitTest、GetNodeCount、EnsureVisible；选择前 BeforeSelect 可取消，成功后 AfterSelect；Action 区分鼠标/键盘/程序。 |
| ⬜ | P1 | `TreeView-04` | ExpandAll/CollapseAll、TreeNode.Expand/Collapse/Toggle/ExpandAll；BeforeExpand/AfterExpand、BeforeCollapse/AfterCollapse 的取消、重复调用、懒加载插节点和子孙状态。 |
| ⬜ | P1 | `TreeView-05` | CheckBoxes、TreeNode.Checked/StateImageIndex、BeforeCheck/AfterCheck；取消不改原生勾选，父子节点不擅自联动；空格键/鼠标/程序入口一致。 |
| ⬜ | P1 | `TreeView-06` | LabelEdit、BeginEdit/EndEdit、BeforeLabelEdit/AfterLabelEdit、CancelEdit；F2/Esc/Enter、null Label 表示取消与空字符串区别，提交前后 Text 同步。 |
| ⬜ | P2 | `TreeView-07` | ImageList/StateImageList、ImageIndex/ImageKey/SelectedImageIndex/SelectedImageKey、Indent/ItemHeight、FullRowSelect/LineColor/ShowNodeToolTips；运行时变化与 DPI 兼容。 |
| ⬜ | P2 | `TreeView-08` | Sorted/TreeViewNodeSorter/Sort；TreeNode.Name/FullPath/Tag/NodeFont/ForeColor/BackColor、节点查找/Clone；排序后选择与路径正确，重复 key 按官方集合规则。 |
| ⬜ | P2 | `TreeView-09` | NodeMouseClick/NodeMouseDoubleClick/NodeMouseHover、ItemDrag、键盘 Left/Right/Home/End/PageUp/Down/增量搜索；节点切换时焦点和事件顺序可对照。 |
| ⬜ | P2 | `TreeView-10` | DrawMode/DrawNode、BeginUpdate/EndUpdate、节点移除时句柄映射清理，重建原生树保留展开、选中、滚动；Dispose 解绑图像列表和所有 native 回调。 |
| ⬜ | P3 | `TreeView-11` | GTK TreeListModel 的折叠前仲裁与 AppKit delegate 的允许展开/选择仲裁，各运行取消/懒加载/大树场景；按行为分母计分，不因系统默认动画/外观不同判为API失败。 |

## 三平台实现分析

### Windows

SysTreeView32：TVM_INSERTITEM/SETITEM/DELETEITEM/EXPAND/SELECTITEM/GETNEXTITEM/SETIMAGELIST；TVN_SELCHANGING/SELCHANGED、ITEMEXPANDING/ITEMEXPANDED、BEGINLABELEDIT/ENDLABELEDIT、STATEIMAGECHANGING 等进入兼容事件。GCHandle 或受控 ID 表关联节点，释放必须与 TVM 删除及句柄重建一致。

### Linux · GTK 4

GtkListView 或 GtkColumnView + GtkTreeListModel + GtkTreeExpander + GtkSingleSelection。TreeListModel 按需创建子模型，可保留懒加载；GTK 展开属性通知不等于 WinForms 可取消 BeforeExpand，需要由库控制展开入口并阻止默认通路绕过仲裁。复选/图像是原生行组合。

### macOS · AppKit

NSOutlineView + NSScrollView，dataSource 返回 child/count/expandable，delegate 协调 shouldExpandItem/shouldCollapseItem/shouldSelectItem。行内 NSTableCellView/NSImageView/NSButton 支持图标/复选。FullPath、Name、TreeNodeCollection 是托管语义；节点身份用稳定托管映射而非显示索引。

## 边界、AOT 与验收

TreeView 没有标准 DataSource，不新增公开绑定概念。Before* 取消和回调重入为 P1；OwnerDraw 依赖跨平台 System.Drawing 处理决定。Win32 HTREEITEM/HWND 专属另列，不能让 GTK/AppKit 返回伪造 HWND。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- 2026-10-16 · 批次023：基础Control启用标准UserPaint后，本控件构造显式保留UserPaint=false，使现有系统窗口类过程及原生字体/绘制继续生效。仅属消息与绘制基础的必要衔接，不代表本控件完整Styles、Paint事件或视觉/输入兼容完成，见[023](../batches/023.md)。

- `108ef2b` · 2026-04-08：原生深色模式。
- `7149595` · 2026-04-08：深色模式与菜单绘制相关增强。
- `1c89c0a` · 2026-04-01：主题、控件初始化与样式逻辑。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.treeview?view=windowsdesktop-10.0
- https://learn.microsoft.com/windows/win32/controls/tree-view-controls
- https://docs.gtk.org/gtk4/class.TreeListModel.html
- https://docs.gtk.org/gtk4/class.TreeExpander.html
- https://developer.apple.com/documentation/appkit/nsoutlineview
