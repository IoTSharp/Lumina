# ListViewGroup

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/ListViewGroup.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · 🟡

已实现 Windows 组对象、Groups 集合、Group.Items/Item.Group 双向所有权、默认未分组回落、完整 LVGROUP 文本/标题图像/对齐/折叠字段和原生同步。事件、设计器、跨平台和完整成员验收仍待；GTK/AppKit 的分段标题仅提供视觉材料，不能据此声明完整组契约完成。

2026-11-11 M0 检查：ListView/ImageList/Group 无窗口回归 5/5，Release 集成编译 0 错误；真实组交互、Designer、AOT 和平台验收为 NOT_RUN。

- 标准继承/契约：`ListViewGroup` 普通对象；包含序列化契约，Items 为 ListView.ListViewItemCollection。
- 代码证据：[ListViewGroup.cs](../../../Lumina.Forms/ListViewGroup.cs)、[ListViewGroupCollection.cs](../../../Lumina.Forms/ListViewGroupCollection.cs)、[ListView.ImagesAndGroups.cs](../../../Lumina.Forms/ListView.ImagesAndGroups.cs)。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| 🟡 | P0 | `ListViewGroup-01` | 已补构造、Name/Header/HeaderAlignment、Items/ListView/Tag、Footer/Subtitle/TaskLink/TitleImage 等确定范围；默认值和完整基线对照仍待。 |
| 🟡 | P1 | `ListViewGroup-02` | 已补 ListView.Groups Add/Insert/Remove/Clear/索引和 key 操作、单宿主与重复组拒绝；完整异常/回退规则仍待。 |
| 🟡 | P1 | `ListViewGroup-03` | 已接 item.Group 与 group.Items 双向同步、移组/删组/跨 ListView 拒绝；VirtualMode 和完整索引差分仍待。 |
| 🟡 | P1 | `ListViewGroup-04` | 已同步 ShowGroups、Header/Subtitle/Footer/TaskLink/TitleImage、1/2/4 对齐和折叠状态到 LVGROUP；真实运行事件仍待。 |
| ⬜ | P1 | `ListViewGroup-05` | 组折叠/展开、任务链接点击，ListView.GroupCollapsedStateChanged/GroupTaskLinkClick 等实际公开事件核对并连接；键盘/鼠标/程序路径区别。 |
| ⬜ | P2 | `ListViewGroup-06` | Groups 顺序、项排序、空组、默认未分组项、大数据滚动，View 模式/系统版本限制与标准实际行为。 |
| ⬜ | P2 | `ListViewGroup-07` | 标题图像与 ImageList 更换/释放、DPI/RTL/高对比度/辅助技术 group role 和折叠状态。 |
| ⬜ | P2 | `ListViewGroup-08` | 序列化、设计器静态 Groups/AddRange 与 item.Group 的初始化次序；AOT 资源读取不靠任意程序集动态加载。 |
| ⬜ | P2 | `ListViewGroup-09` | 移除组后重插、ListView Dispose、native group ID 复用、回调中删除组；组本身不擅自 Dispose 包含项。 |
| ⬜ | P3 | `ListViewGroup-10` | GTK 分组模型/标题行与 AppKit NSCollectionView section/NSTableView group row 分别论证；折叠/TaskLink/额外文本不足项明确列缺口。 |

## 三平台实现分析

### Windows

SysListView32 分组 API：LVM_INSERTGROUP/SETGROUPINFO/REMOVEGROUP/ENABLEGROUPVIEW 与 LVGROUP；group ID 独立于集合索引。操作系统/视图限制沿用标准，不伪装总可用。

### Linux · GTK 4

GtkListView/ColumnView 可用 section/header factory（最低 GTK 版本需固定），或模型内原生标题行 + 内容行；折叠和 TaskLink 用原生按钮/标签组合。没有完整 ListViewGroup 原生对象，组位置与事件需托管实现。

### macOS · AppKit

NSCollectionView section + supplementary view 对图标视图，NSTableView 的 group row 方案用于表格；collapsed 与 TaskLink 要显式管理，不将 section 数量当 Groups 兼容完成度。

## 边界、AOT 与验收

依赖 ListView 稳定项身份和集合。跨平台哪些组能力可移植需逐成员裁定；不可只把高级组属性存值。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 已完成任务

当前尚无已关闭的代码范围；变更历史统一见根目录 [CHANGELOG](../../../CHANGELOG.md)。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.listviewgroup?view=windowsdesktop-10.0
- https://learn.microsoft.com/windows/win32/controls/lvm-insertgroup
- https://docs.gtk.org/gtk4/class.ListView.html
- https://developer.apple.com/documentation/appkit/nscollectionview
