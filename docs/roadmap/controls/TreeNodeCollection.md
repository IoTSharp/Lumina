# TreeNodeCollection

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/TreeNodeCollection.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · 🟡

已建立TreeView/父TreeNode所属、可写索引、增删插入替换、键查找/Find及TVM原生更新；完整IList/ICollection、图像重载、排序、编辑事件和平台验收仍待。

- 标准继承/契约：`TreeNodeCollection : IList, ICollection, IEnumerable`；TreeView.Nodes 与 TreeNode.Nodes 的标准集合。
- 代码证据：[当前源码](../../../Lumina.Forms/TreeNode.cs)。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| 🟡 | P0 | `TreeNodeCollection-01` | 已补整数可写/字符串只读索引与Add(TreeNode)索引；完整IList/ICollection显式接口、SyncRoot/CopyTo等仍待。 |
| 🟡 | P1 | `TreeNodeCollection-02` | 已补Add(text)/Add(key,text)、AddRange、Insert/Remove/RemoveAt/Clear与基础返回值；含图像重载和完整异常参数对照仍待。 |
| ✅ | P1 | `TreeNodeCollection-03` | owner连接TreeView或父TreeNode并维护Parent/TreeView/Index；重复宿主、自身及祖先循环先拒绝，移除后可重挂。插入/替换原生失败会恢复托管所属及句柄映射。 |
| 🟡 | P1 | `TreeNodeCollection-04` | 已补Contains/IndexOf及按key查询、RemoveByKey/Find；完整重复key/大小写/空key及深层查找顺序的固定基线运行对照仍待。 |
| 🟡 | P1 | `TreeNodeCollection-05` | 建窗前后增删、索引替换、子树移除已连接真实TVM消息；Windows原生Count/顺序与回调重入的实际验证后置。 |
| ⬜ | P1 | `TreeNodeCollection-06` | 删除选中/正在编辑/展开节点时 TreeView 事件顺序、取消权限和当前节点回退；批量 AddRange 与 BeginUpdate/EndUpdate 不丢最终状态。 |
| ⬜ | P2 | `TreeNodeCollection-07` | Sorted/TreeViewNodeSorter 下插入位置和返回索引、Name 变更后的键缓存失效、枚举中修改集合的实际行为。 |
| ⬜ | P2 | `TreeNodeCollection-08` | 设计器 Serialize/Clone 子树与 .resx 场景、嵌套节点静态布局；AOT 无运行时动态类型创建。 |
| ✅ | P2 | `TreeNodeCollection-09` | Clear/Remove与句柄销毁清理全部子树HTREEITEM映射；托管节点可重新插入，子节点保留其父节点关系；不释放外部共享资源、不新增IDisposable或自创销毁API。 |
| ⬜ | P3 | `TreeNodeCollection-10` | GTK GListModel items-changed 与 AppKit reloadItem/insert/remove 原生更新，插入删除后的展开/选中保持，同一任务三平台分别验收。 |

## 三平台实现分析

### Windows

集合变更先执行托管合法性检查，再使用 TVM_INSERTITEM/DELETEITEM/SETITEM，更新 HTREEITEM 字典；一次删除子树同步清理所有后代映射。

### Linux · GTK 4

每父节点子列表以 GListModel 提供，集合变化发 items-changed；GtkTreeListModel 自动展平，不能借展平索引改写原始 Parent/Index。

### macOS · AppKit

NSOutlineViewDataSource 从同一集合读取，通过 insertItems/removeItems/reloadItem 同步；数组索引为兄弟索引，避免把展开视图行号混为 TreeNode.Index。

## 边界、AOT 与验收

没有独立窗体或可见原生控件，本页是树控件关键组成部分。所有“完成”必须包含真实 native 可见状态和节点归属证据。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 已完成任务

代码完成范围已在上表标记；变更历史统一见根目录 [CHANGELOG](../../../CHANGELOG.md)。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.treenodecollection?view=windowsdesktop-10.0
- https://docs.gtk.org/gio/iface.ListModel.html
- https://developer.apple.com/documentation/appkit/nsoutlineviewdatasource
