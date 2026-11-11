# ListViewItem

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/ListViewItem.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · 🟡

当前支持无参、string、string[] 基础构造、Text/SubItems 与未挂接 BackColor/ForeColor/Font/UseItemStyleForSubItems。批次 007 补集合公共构造、共享 owner 数据及整数索引；批次 014 补 Name/Tag/ToolTipText，中文 Designer 分类本地化仍有明确差异。批次 015 补标准 ToString override；批次 016 补首子项样式代理和共享默认 Font，541 条固定英文参考/JIT 相同，328 项新增回归通过。读取保留首项恢复时机、共享 view/三枚举/key 缓存。批次017完成未挂接ImageIndex/ImageKey状态首范围，设计器转换器/编辑器仍缺。图像/分组/颜色字体等其余构造、全局字体与已挂接样式/native、全部所属、选择、勾选及完整集合契约仍待实现。

- 标准继承/契约：`ListViewItem : ICloneable, ISerializable`；不是 Control/Component。
- 代码证据：[当前源码](../../../Lumina.Forms/ListViewItem.cs)。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| 🟡 | P0 | `ListViewItem-01` | string、无参和 string[] 基础构造及 Text/首子项一致性已完成；imageIndex/imageKey/group/颜色字体等其余构造不随子项完成而关闭。 |
| 🟡 | P1 | `ListViewItem-02` | ListView/Index/Remove已连接实际Items所属，移除后可重新挂接；Bounds/Position、EnsureVisible/查找/编辑和不同View模式仍待。 |
| ⬜ | P1 | `ListViewItem-03` | Selected/Focused/Checked/StateImageIndex、对应 ListView 选择/复选事件和 native 状态；Items.Clear 后旧对象属性回落规则。 |
| 🟡 | P1 | `ListViewItem-04` | 批次014完成Name/Tag/ToolTipText未挂接首范围（04a），017完成未挂接ImageIndex/ImageKey状态及互斥（04b）。ImageList解析/裁剪、已挂接native/Tooltip、完整所属、IndentCount/Group、组/图像列表重建同步、中文Designer及标准图像转换器/编辑器仍缺，不随04b关闭。 |
| 🟡 | P1 | `ListViewItem-05` | 既有SubItems集合/API保持；本轮补Text/主子项集合变更通知所属ListView，真实LVM_SETITEM写回并清除被删除列的旧文字。完整所属、所有接口/并发压力和样式消费仍待。 |
| 🟡 | P2 | `ListViewItem-06` | 批次 016 完成 BackColor/ForeColor/Font/UseItemStyleForSubItems 未挂接首范围（06a）；父 ListView 继承值、全局默认字体与 native item/subitem 的实时样式更新仍缺。 |
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

本页依赖 ListView、ListViewSubItem、ColumnHeader 和 ImageList。最初的基础构造异常已由批次001修复，003补无参/string[]范围；其余图像/分组/样式构造与完整所属/native仍是未完成工作，不把历史异常继续列为当前基础构造阻塞。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 已完成任务

✅ 表示该行定义范围代码完成；父项的剩余代码与平台验收仍按当前任务表记录。

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ✅ | P0 | `ListViewItem-01a` | 批次 001：先初始化 SubItems，再由 Text setter 创建唯一首项；null 归一，Text/首子项双向更新、清空/移除/替换首项后的托管行为通过 9 项回归。原生同步与完整构造族不在本子项范围，兼容验收后置。 |
| ✅ | P0 | `ListViewItem-01b` | 批次 003：无参/string[] 构造、null/空数组/null 元素、独立文本复制、Text 读取不重建空集合及 SubItems 访问在原集合中恢复首项；固定参考端 41 条观测相同。完整集合所属与原生同步另列，兼容验收后置。 |
| ✅ | P0 | `ListViewItem-01c` | 批次 015：标准 ToString override 与 Object 虚槽、原样主项 Text 和派生分派完成，96 条固定英文参考/JIT 相同，98 项新增回归。空/null/Unicode/换行/NUL、元数据/样式变化、空表不恢复/不换表、共享 view/三枚举/独立 key 缓存、隐藏成员/原异常有证据；不消费 Tag 或借用 Font。完整所属/native/Clone/序列化及新范围实际 AOT 运行验收另列。 |
| ✅ | P1 | `ListViewItem-04a` | 批次 014：Name 为首子项 Name 代理，空表 getter 不恢复，setter 对 null/空也经 SubItems 换表恢复；Tag/ToolTipText 独立保存。151 条固定 10.0.11 英文参考/JIT 相同，101 项独立回归通过，覆盖引用身份、共享 view/三枚举/独立 key 缓存、隐藏成员及毒丸 Tag。中文 Designer 分类、完整所属/已挂接 ToolTip/native、图像/分组/Clone/序列化另列；未计实际 AOT 运行验收。 |
| ✅ | P1 | `ListViewItem-04b` | 批次017：未挂接ImageIndex默认-1，拒绝小于-1且保留原状态/ActualValue；合法赋值（含重复-1）清key，ImageKey的null/空及等值新引用均按参考保存并清index。518条固定英文参考/JIT相同，363项新增回归通过；首项/样式/三枚举/独立key缓存/借用资源不被误消费。NoneExcludedImageIndexConverter、ImageKeyConverter和ImageIndexEditor明确缺失，未声明全部设计器元数据一致；ImageList/构造族/已挂接native、Group/完整所属/资源及此范围实际AOT运行另列。 |
| ✅ | P2 | `ListViewItem-06a` | 批次 016：未挂接 BackColor/ForeColor/Font 代理当前首子项，空表 getter 不恢复、setter 对 Empty/null 也恢复换表；默认颜色和主项/子项共享惰性 Font、UseItemStyleForSubItems 默认 true 的独立状态完成。541 条固定英文参考/JIT 全同，328 项新增回归，含替换/移除/共享 view/三枚举/独立 key 缓存、ResetStyle、借用及已释放 Font 身份；不克隆或释放借用资源。新范围未计实际 AOT 运行验收，完整所属/已挂接继承/native/全局字体与资源另列。 |

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.listviewitem?view=windowsdesktop-10.0
- https://docs.gtk.org/gtk4/class.ListItemFactory.html
- https://developer.apple.com/documentation/appkit/nstableviewdatasource
