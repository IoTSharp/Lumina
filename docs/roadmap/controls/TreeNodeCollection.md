# TreeNodeCollection

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/TreeNodeCollection.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · 🟡

当前只有 List<TreeNode> 存储、Add/AddRange、Count/整数只读索引和枚举；无 owner、插入删除、键查找、IList、原生更新和所有权约束。

- 标准继承/契约：`TreeNodeCollection : IList, ICollection, IEnumerable`；TreeView.Nodes 与 TreeNode.Nodes 的标准集合。
- 代码证据：[当前源码](../../../Lumina.Forms/TreeNode.cs)。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `TreeNodeCollection-01` | IList/ICollection 显式契约、整数可写/字符串只读索引、IsReadOnly/IsFixedSize/SyncRoot/CopyTo；Add(TreeNode) 返回索引等重载签名准确。 |
| ⬜ | P1 | `TreeNodeCollection-02` | Add(text)、Add(key,text)、含 imageIndex/imageKey 重载、AddRange(TreeNode[])、Insert/Remove/RemoveAt/Clear；返回 node/index、异常参数和重复节点规则一致。 |
| ⬜ | P1 | `TreeNodeCollection-03` | owner 连接 TreeView 或父 TreeNode，维护 Parent/TreeView/Index；加入已有宿主节点、自身/祖先、跨树移动不留下两份所属关系。 |
| ⬜ | P1 | `TreeNodeCollection-04` | Contains/IndexOf/ContainsKey/IndexOfKey/RemoveByKey/Find(key,searchAllChildren)，大小写与重复 key、空 key、递归查找顺序按基线固定。 |
| ⬜ | P1 | `TreeNodeCollection-05` | 原生创建前增删、挂载后增删、索引替换、子树删除；托管 Count/顺序与 TVM_GETCOUNT/枚举一致，不只检查托管集合。 |
| ⬜ | P1 | `TreeNodeCollection-06` | 删除选中/正在编辑/展开节点时 TreeView 事件顺序、取消权限和当前节点回退；批量 AddRange 与 BeginUpdate/EndUpdate 不丢最终状态。 |
| ⬜ | P2 | `TreeNodeCollection-07` | Sorted/TreeViewNodeSorter 下插入位置和返回索引、Name 变更后的键缓存失效、枚举中修改集合的实际行为。 |
| ⬜ | P2 | `TreeNodeCollection-08` | 设计器 Serialize/Clone 子树与 .resx 场景、嵌套节点静态布局；AOT 无运行时动态类型创建。 |
| ⬜ | P2 | `TreeNodeCollection-09` | Clear/Remove 释放 native handle 映射/回调根，节点对象按标准可重新插入；不引入 IDisposable 集合或自创销毁流程。 |
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

## 修改历史

- `dcdc7e9` · 2026-03-31：TreeNode.cs 初次加入基础集合；未建立节点所属关系及原生同步。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.treenodecollection?view=windowsdesktop-10.0
- https://docs.gtk.org/gio/iface.ListModel.html
- https://developer.apple.com/documentation/appkit/nsoutlineviewdatasource
