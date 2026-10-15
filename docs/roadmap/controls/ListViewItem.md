# ListViewItem

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/ListViewItem.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · 🟡

当前支持无参、string、string[] 基础构造、Text/SubItems 和 ForeColor。批次 007 补集合公共构造、共享 owner 数据及整数索引首范围，72 条固定参考对照相同，原 41 条构造和 209 条 key 对照保留。图像/分组/颜色字体等其余构造、全部所属、选择、勾选及完整集合契约仍待实现。

- 标准继承/契约：`ListViewItem : ICloneable, ISerializable`；不是 Control/Component。
- 代码证据：[当前源码](../../../Lumina.Forms/ListViewItem.cs)。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| 🟡 | P0 | `ListViewItem-01` | string、无参和 string[] 基础构造及 Text/首子项一致性已完成；imageIndex/imageKey/group/颜色字体等其余构造不随子项完成而关闭。 |
| ✅ | P0 | `ListViewItem-01a` | 批次 001：先初始化 SubItems，再由 Text setter 创建唯一首项；null 归一，Text/首子项双向更新、清空/移除/替换首项后的托管行为通过 9 项回归。原生同步与完整构造族不在本子项范围，兼容验收后置。 |
| ✅ | P0 | `ListViewItem-01b` | 批次 003：无参/string[] 构造、null/空数组/null 元素、独立文本复制、Text 读取不重建空集合及 SubItems 访问在原集合中恢复首项；固定参考端 41 条观测相同。完整集合所属与原生同步另列，兼容验收后置。 |
| ⬜ | P1 | `ListViewItem-02` | ListView/Index/Bounds/Position、Remove/EnsureVisible/FindNearestItem/GetBounds/GetSubItemAt/BeginEdit；无宿主/无句柄/不同 View 模式下结果和异常一致。 |
| ⬜ | P1 | `ListViewItem-03` | Selected/Focused/Checked/StateImageIndex、对应 ListView 选择/复选事件和 native 状态；Items.Clear 后旧对象属性回落规则。 |
| ⬜ | P1 | `ListViewItem-04` | ImageIndex/ImageKey/ImageList、IndentCount/Group、Name/Tag/ToolTipText；key/index 互斥、组替换、图像列表重建同步。 |
| 🟡 | P1 | `ListViewItem-05` | 批次 006～010 完成 key 四成员、公共集合构造/共享视图/整数索引、三个 typed Add/AddRange 与 typed Insert/void Remove/RemoveAt/Clear 首范围（ListViewSubItem-03a/02a～d）；非泛型 IList/ICollection、Contains/IndexOf/CopyTo 全契约、完整所属与 native 同步仍缺。 |
| ⬜ | P2 | `ListViewItem-06` | BackColor/ForeColor/Font/UseItemStyleForSubItems，父 ListView 继承值与每子项独立样式，变更实时更新对应 native item/subitem。 |
| ⬜ | P2 | `ListViewItem-07` | Clone 深浅拷贝边界、SubItems 和 Group/Tag/Font 引用规则；序列化与设计器 .resx 常见项资源往返，AOT 不走任意类型反序列化。 |
| ⬜ | P2 | `ListViewItem-08` | VirtualMode 的 RetrieveVirtualItem 返回值及 item 所属关系、重复返回同一 item、模式切换；不把常规 Items 所有权强套到虚拟模式。 |
| ⬜ | P2 | `ListViewItem-09` | 项移除/重插入、源 ListView Dispose、native item delete 通知和回调重入，不残留 handle/id/订阅；Item 无 IDisposable，不新增释放 API。 |
| ⬜ | P3 | `ListViewItem-10` | GTK/AppKit item ID 固定，行 widget/cell 复用与项身份分离；View 切换保留 Selected/Focused/Group，三平台对照。 |

## 三平台实现分析

### Windows

托管项用稳定 ID/lParam 关联 SysListView32；LVM_SETITEM/GETITEMSTATE/SETITEMSTATE/GETITEMRECT 等读写；SubItems 和图像、Group 状态同步，使用 ListView 通知发事件。

### Linux · GTK 4

托管 item 经 GListModel 与 native row factory 显示；多模式切换复用同一 ID，GTK 行选区映射 Selected，Focused 仍需兼容状态管理。

### macOS · AppKit

托管 item 经 NSTableView/NSCollectionView dataSource 显示；native cell/view 生命周期不持有错误 item，不用显示行号作永久对象身份。

## 边界、AOT 与验收

本页依赖 ListView、ListViewSubItem、ColumnHeader 和 ImageList。Windows 构造异常为首要阻塞；先消除缺陷再扩展主题/性能。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- `b8a98eb` · 2026-04-09：加入 ListViewItem 与基础子项模型；当前初始化次序仍需修复。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。
- 2026-10-15 · 批次 001：修复 string 构造空引用、首子项初始化及 null 文本归一；新增 9 项无界面回归且全部通过。父任务及原生集合未完成，完整兼容验收待后期集中进行。
- 2026-10-16 · 批次 003：补无参和 string[] 基础构造；保留输入数组独立性、null 文本归一及 Text/首子项同步。Clear 后读 Text 保持集合为空，重新访问 SubItems 才在同一集合创建新首项；与 WinForms 10.0.11 的 41 条观测相同。父任务其余构造和完整集合/native 同步未关闭。
- 2026-10-16 · 批次 004：嵌套 ListViewSubItem 增加公共基础构造、继承与 Name/Tag；集合 Add(string) 经 owner/text 构造保存库内关联。原 21 项构造回归及 41 条参考观测继续通过；完整集合重归属、样式和 native 同步没有扩面，见 [子项任务](ListViewSubItem.md)。

- 2026-10-16 · 批次 006：嵌套集合增加四个标准 virtual key 成员，209 条固定参考/Lumina 观测相同，新增 38 项回归。保留单索引缓存、等长不变文化忽略大小写、重复 key 与 Name/集合修改及单项移除；原 41 条构造、37 条基础子项及 37 条样式回归保留。完整集合构造/IList/所属/native 仍另列。

历史按对应源码路径的相关内容摘要，不把提交标题当成功能验收证明。

- 2026-10-16 · 批次 007：owner 持有唯一子项数据，公共集合视图共享该数据但保留独立 key 缓存，Text 在外部视图添加/替换/清空后保持一致；整数索引首范围、对象身份与延迟首项恢复有 72 条固定参考对照，原 41 条基础构造回归保留。完整集合/所属/native 不随此关闭，见 [批次 007](../batches/007.md)。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.listviewitem?view=windowsdesktop-10.0
- https://docs.gtk.org/gtk4/class.ListItemFactory.html
- https://developer.apple.com/documentation/appkit/nstableviewdatasource
