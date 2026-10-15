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
| ✅ | P0 | `ListViewItem-01a` | 批次 001：先初始化 SubItems，再由 Text setter 创建唯一首项；null 归一，Text/首子项双向更新、清空/移除/替换首项后的托管行为通过 9 项回归。原生同步与完整构造族不在本子项范围，兼容验收后置。 |
| ✅ | P0 | `ListViewItem-01b` | 批次 003：无参/string[] 构造、null/空数组/null 元素、独立文本复制、Text 读取不重建空集合及 SubItems 访问在原集合中恢复首项；固定参考端 41 条观测相同。完整集合所属与原生同步另列，兼容验收后置。 |
| ✅ | P0 | `ListViewItem-01c` | 批次 015：标准 ToString override 与 Object 虚槽、原样主项 Text 和派生分派完成，96 条固定英文参考/JIT 相同，98 项新增回归。空/null/Unicode/换行/NUL、元数据/样式变化、空表不恢复/不换表、共享 view/三枚举/独立 key 缓存、隐藏成员/原异常有证据；不消费 Tag 或借用 Font。完整所属/native/Clone/序列化及新范围实际 AOT 运行验收另列。 |
| ⬜ | P1 | `ListViewItem-02` | ListView/Index/Bounds/Position、Remove/EnsureVisible/FindNearestItem/GetBounds/GetSubItemAt/BeginEdit；无宿主/无句柄/不同 View 模式下结果和异常一致。 |
| ⬜ | P1 | `ListViewItem-03` | Selected/Focused/Checked/StateImageIndex、对应 ListView 选择/复选事件和 native 状态；Items.Clear 后旧对象属性回落规则。 |
| 🟡 | P1 | `ListViewItem-04` | 批次014完成Name/Tag/ToolTipText未挂接首范围（04a），017完成未挂接ImageIndex/ImageKey状态及互斥（04b）。ImageList解析/裁剪、已挂接native/Tooltip、完整所属、IndentCount/Group、组/图像列表重建同步、中文Designer及标准图像转换器/编辑器仍缺，不随04b关闭。 |
| ✅ | P1 | `ListViewItem-04a` | 批次 014：Name 为首子项 Name 代理，空表 getter 不恢复，setter 对 null/空也经 SubItems 换表恢复；Tag/ToolTipText 独立保存。151 条固定 10.0.11 英文参考/JIT 相同，101 项独立回归通过，覆盖引用身份、共享 view/三枚举/独立 key 缓存、隐藏成员及毒丸 Tag。中文 Designer 分类、完整所属/已挂接 ToolTip/native、图像/分组/Clone/序列化另列；未计实际 AOT 运行验收。 |
| ✅ | P1 | `ListViewItem-04b` | 批次017：未挂接ImageIndex默认-1，拒绝小于-1且保留原状态/ActualValue；合法赋值（含重复-1）清key，ImageKey的null/空及等值新引用均按参考保存并清index。518条固定英文参考/JIT相同，363项新增回归通过；首项/样式/三枚举/独立key缓存/借用资源不被误消费。NoneExcludedImageIndexConverter、ImageKeyConverter和ImageIndexEditor明确缺失，未声明全部设计器元数据一致；ImageList/构造族/已挂接native、Group/完整所属/资源及此范围实际AOT运行另列。 |
| 🟡 | P1 | `ListViewItem-05` | 批次 006～012 完成 key 四成员、公共集合/共享视图/整数索引、typed 添加/变更、非泛型 IList/ICollection、引用查询/数组复制及枚举首范围（ListViewSubItem-03a/02a～f）；真正多线程/压力、完整所属与 native 同步仍缺。批次 013 的子项字符串读取保留上述集合/缓存行为。 |
| 🟡 | P2 | `ListViewItem-06` | 批次 016 完成 BackColor/ForeColor/Font/UseItemStyleForSubItems 未挂接首范围（06a）；父 ListView 继承值、全局默认字体与 native item/subitem 的实时样式更新仍缺。 |
| ✅ | P2 | `ListViewItem-06a` | 批次 016：未挂接 BackColor/ForeColor/Font 代理当前首子项，空表 getter 不恢复、setter 对 Empty/null 也恢复换表；默认颜色和主项/子项共享惰性 Font、UseItemStyleForSubItems 默认 true 的独立状态完成。541 条固定英文参考/JIT 全同，328 项新增回归，含替换/移除/共享 view/三枚举/独立 key 缓存、ResetStyle、借用及已释放 Font 身份；不克隆或释放借用资源。新范围未计实际 AOT 运行验收，完整所属/已挂接继承/native/全局字体与资源另列。 |
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

## 修改历史

- 2026-10-16 · 批次017：新增标准非虚ImageIndex/ImageKey及范围内英文元数据，独立保存未挂接图像选择，严格先验证再改状态；合法索引与key互斥，null key归空，等值字符串保留新引用。518条固定10.0.11参考/JIT相同、363项新测试通过；覆盖新建/空表/清空/替换/共享首项、三枚举/各view key缓存、Name/Tag/Tooltip/ToString、隐藏/毒丸与借用Font。原始设计器元数据单独记录：NoneExcludedImageIndexConverter、ImageKeyConverter和ImageIndexEditor未实现；参考这两属性没有RelatedImageListAttribute，不能把它记成当前不匹配。未计新范围实际AOT运行验收。父任务04/FND-003保持部分，图像构造、ImageList/native/Group/完整所属/Clone/序列化与资源仍缺，见[017](../batches/017.md)。

- 2026-10-16 · 批次 016：BackColor/ForeColor/Font 改为当前首子项代理，空表 getter 不恢复、setter 对 Empty/null 也经既有 SubItems 换表恢复；移除/替换/ResetStyle 真实改变 item 的读取。默认 Window/WindowText，主项和子项共享同一惰性 MessageBoxFont；借用或已释放 Font 保持身份，不克隆/释放。UseItemStyleForSubItems 默认 true，未挂接时独立保存且不修改直接子项样式。541 条固定 en-US 参考/JIT 全同，328 项新增回归，三枚举/共享 view/独立 key 缓存、Name/Tag/Tooltip/两级 ToString 与既有样式构造保持；全线 3,352 项及 27 个旧驱动通过。标准非虚属性及英文元数据有证据；新范围未计实际 AOT 运行验收，已挂接继承/native/全局字体/完整所属/Clone/序列化/Designer 资源仍缺，见 [016](../batches/016.md)。

- 2026-10-16 · 批次 015：新增标准 public override string ToString()，覆盖 Object 槽并使用主项原样 Text，格式为 ListViewItem: {Text}。96 条固定 en-US 参考/JIT 全同，98 项新增回归，覆盖空/null/Unicode/换行/NUL、元数据/样式变化、派生隐藏/override/原异常、首项清空/移除/替换/重复/跨 owner、三枚举与各 view 独立 key 缓存。读取不恢复首项/换表，不调用 Tag.ToString 或消费/释放借用 Font；Name 代理与独立 Tag/Tooltip 保持。全线 2,713 项及 25 个旧差分驱动通过，新范围未计实际 AOT 运行验收；未挂接完整样式、所属/native/Clone/序列化/设计器和压力另列，见 [015](../batches/015.md)。

- `b8a98eb` · 2026-04-09：加入 ListViewItem 与基础子项模型；当时的初始化次序缺陷已由批次001修复基础范围，原提交历史保留，其余构造族仍待实现。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。
- 2026-10-15 · 批次 001：修复 string 构造空引用、首子项初始化及 null 文本归一；新增 9 项无界面回归且全部通过。父任务及原生集合未完成，完整兼容验收待后期集中进行。
- 2026-10-16 · 批次 003：补无参和 string[] 基础构造；保留输入数组独立性、null 文本归一及 Text/首子项同步。Clear 后读 Text 保持集合为空，重新访问 SubItems 才在同一集合创建新首项；与 WinForms 10.0.11 的 41 条观测相同。父任务其余构造和完整集合/native 同步未关闭。
- 2026-10-16 · 批次 004：嵌套 ListViewSubItem 增加公共基础构造、继承与 Name/Tag；集合 Add(string) 经 owner/text 构造保存库内关联。原 21 项构造回归及 41 条参考观测继续通过；完整集合重归属、样式和 native 同步没有扩面，见 [子项任务](ListViewSubItem.md)。

- 2026-10-16 · 批次 006：嵌套集合增加四个标准 virtual key 成员，209 条固定参考/Lumina 观测相同，新增 38 项回归。保留单索引缓存、等长不变文化忽略大小写、重复 key 与 Name/集合修改及单项移除；原 41 条构造、37 条基础子项及 37 条样式回归保留。完整集合构造/IList/所属/native 仍另列。

历史按对应源码路径的相关内容摘要，不把提交标题当成功能验收证明。

- 2026-10-16 · 批次 007：owner 持有唯一子项数据，公共集合视图共享该数据但保留独立 key 缓存，Text 在外部视图添加/替换/清空后保持一致；整数索引首范围、对象身份与延迟首项恢复有 72 条固定参考对照，原 41 条基础构造回归保留。完整集合/所属/native 不随此关闭，见 [批次 007](../batches/007.md)。

- 2026-10-16 · 批次 014：补 Name/Tag/ToolTipText 标准非虚属性。Name 不独立存储；读空表不恢复，写 null/空也恢复首项且旧空枚举器继续观察旧表。Tag 与 ToolTipText 独立，引用/文本原样保存，均不消费或释放外部资源。151 条固定英文参考/JIT 全同，新增 101 项测试；三种枚举/共享 view/独立 key 缓存及全部旧差分保持。中文 Designer 分类实测仍为 Data/Appearance，对参考“数据/外观”有差异；完整所属/已挂接 Tooltip/native、图像/分组/Clone/序列化和压力另列，新范围未记实际 AOT 运行验收，父任务 04 仅部分完成。见 [014](../batches/014.md)。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.listviewitem?view=windowsdesktop-10.0
- https://docs.gtk.org/gtk4/class.ListItemFactory.html
- https://developer.apple.com/documentation/appkit/nstableviewdatasource
