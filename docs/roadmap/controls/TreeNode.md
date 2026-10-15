# TreeNode

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/TreeNode.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · 🟡

当前仅 Text/Name/Nodes 和三个构造；无 Parent/TreeView/Index/Handle、选中/展开/复选/图像/编辑与 Clone。TreeView 原生内容也未连接。

- 标准继承/契约：`TreeNode : MarshalByRefObject, ICloneable, ISerializable`；标准序列化契约按 AOT/安全边界评估。
- 代码证据：[当前源码](../../../Lumina.Forms/TreeNode.cs)。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `TreeNode-01` | 核对构造全部重载及基类/接口；Text=null 的规范化、Name=空串、Tag=null、Checked=false、ImageIndex/ImageKey 等默认值与官方固定样本。 |
| ⬜ | P1 | `TreeNode-02` | Parent/TreeView/Index/Level/FullPath、FirstNode/LastNode/NextNode/PrevNode/NextVisibleNode/PrevVisibleNode；脱离树、移父节点、排序、折叠的实际结果一致。 |
| ⬜ | P1 | `TreeNode-03` | Text/Name/Tag 与节点集合键查找、相同值更新不重建整棵树；节点句柄创建/销毁与 Handle 读取时机，不能返回无来源 native 指针。 |
| ⬜ | P1 | `TreeNode-04` | Expand/Collapse/Toggle/ExpandAll/EnsureVisible、IsExpanded/IsVisible/IsSelected；所有操作走 TreeView Before*/After* 仲裁，取消后状态不变。 |
| ⬜ | P1 | `TreeNode-05` | Checked/StateImageIndex/StateImageKey、ImageIndex/ImageKey/SelectedImageIndex/SelectedImageKey 的互斥和回退；改变 ImageList 后显示同步。 |
| ⬜ | P1 | `TreeNode-06` | BeginEdit/EndEdit/IsEditing、Remove；编辑中移除或改父节点、取消 AfterLabelEdit，原生编辑器和节点状态一致。 |
| ⬜ | P2 | `TreeNode-07` | ForeColor/BackColor/NodeFont/ToolTipText/ContextMenuStrip/Bounds/GetNodeCount，继承值、DPI/RTL/不可见节点的 Bounds 行为。 |
| ⬜ | P2 | `TreeNode-08` | Clone 深拷贝子节点、属性/Tag/ContextMenuStrip 的引用语义，序列化中的已声明属性与 .resx 设计器兼容；不自动启用不安全二进制反序列化。 |
| ⬜ | P2 | `TreeNode-09` | 节点所有权与回调引用清理，移除节点不擅自 Dispose NodeFont/ContextMenuStrip；TreeNode 本身没有 IDisposable，不发明 release API。 |
| ⬜ | P3 | `TreeNode-10` | 三平台稳定节点身份映射，GTK TreeListRow/AppKit item view 回收不会丢状态；Win32 Handle 专属项单列。 |

## 三平台实现分析

### Windows

TreeNode 是托管模型，映射 HTREEITEM，TVM_SETITEM/EXPAND/DELETEITEM 更新；TreeView 的通知解析回同一对象。重建句柄后旧 HTREEITEM 无效，必须刷新映射。

### Linux · GTK 4

托管 TreeNode 映射 GObject/稳定 ID 与 GtkTreeListRow，行 widget 复用不等于节点身份改变；Parent/Index/FullPath 由相同集合语义维护。

### macOS · AppKit

托管节点通过受控 ObjC item identity 交给 NSOutlineView dataSource；视图复用不创建新的 TreeNode，展开/编辑 delegate 进入标准 TreeView 事件。

## 边界、AOT 与验收

依赖 TreeView 与 TreeNodeCollection；核心业务是节点身份、所有权和取消事件，不能把所有状态自动属性存值当完成。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- `dcdc7e9` · 2026-03-31：新增基础 TreeNode 与 TreeNodeCollection；当前实现只覆盖少量模型字段。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.treenode?view=windowsdesktop-10.0
- https://docs.gtk.org/gtk4/class.TreeListRow.html
- https://developer.apple.com/documentation/appkit/nsoutlineview
