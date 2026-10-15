# ListViewGroupCollection

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/ListViewGroupCollection.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · ⬜

【M2】未发现 Lumina 对应实现。ListView.Groups公开类型与Group.Items/Item.Group双向归属需要统一，不能只存列表。

- 标准继承/契约：`System.Windows.Forms.ListViewGroupCollection : System.Object`。
- 代码证据：[Lumina.Forms 库目录](../../../Lumina.Forms/)（未找到本类型定义）。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `ListViewGroupCollection-01` | 真实ListViewGroupCollection类型、构造可见性及IList/ICollection/IEnumerable显式接口；通过ListView.Groups取得稳定实例。 |
| ⬜ | P1 | `ListViewGroupCollection-02` | Add(group)/Add(key,headerText)、AddRange(array/collection)、Insert/Remove/RemoveAt/Clear、IndexOf/Contains全量签名与返回。 |
| ⬜ | P1 | `ListViewGroupCollection-03` | 整数和字符串key索引器get/set，替换组、缺失key、键大小写/重复键/null与无效索引的标准行为。 |
| ⬜ | P1 | `ListViewGroupCollection-04` | Count/CopyTo/GetEnumerator/接口IsFixedSize/IsReadOnly/SyncRoot，枚举中修改和不兼容对象IList操作。 |
| ⬜ | P1 | `ListViewGroupCollection-05` | 加入/移除组时Group.ListView、Group.Items和ListViewItem.Group归属，跨ListView加入及已有条目移动。 |
| ⬜ | P2 | `ListViewGroupCollection-06` | ShowGroups=false、VirtualMode、排序/视图切换、句柄创建前后修改，组集合与可见组头实际同步。 |
| ⬜ | P2 | `ListViewGroupCollection-07` | 插入/删除当前组、默认组、折叠状态/任务链接/子标题等Group属性更新；native ID不直接充当托管集合索引。 |
| ⬜ | P2 | `ListViewGroupCollection-08` | 设计器Groups.AddRange静态初始化、键/Name序列化和AOT；移除/Dispose不释放条目共享ImageList/字体。 |
| ⬜ | P3 | `ListViewGroupCollection-09` | 三平台顺序/键查找/组间移动/空组/禁用分组三套轨迹；原生缺少等价分组的视图单列边界。 |

## 三平台实现分析

### Windows

使用ListView公共原生宿主的LVM_INSERTGROUP/LVM_REMOVEGROUP/LVM_SETGROUPINFO等消息；托管集合是状态来源，建立稳定group ID映射，句柄重建时按当前集合重放。

### Linux · GTK 4

GtkColumnView/GtkListView通过原生section/header支持或原生分组容器呈现；多种ListView视图、组折叠和Item归属不是自动具备，逐后端场景验证，不另造公开分组API。

### macOS · AppKit

NSTableView/NSCollectionView使用原生分组行/section/补充视图表达；须保持WinForms组对象和条目归属，Tile/List等视图与默认分组差异分别验证。

## 边界、AOT 与验收

依赖ListView/ListViewGroup/ListViewItem。分组与VirtualMode等不支持组合按固定标准返回或异常，不用空实现吞掉。共享托管模型，平台仅负责原生呈现和回调；AOT稳定映射与生命周期随ListView释放。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.listviewgroupcollection?view=windowsdesktop-10.0
- https://docs.gtk.org/gtk4/class.ColumnView.html
- https://developer.apple.com/documentation/appkit/nstableview
