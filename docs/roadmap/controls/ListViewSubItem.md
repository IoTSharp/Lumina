# ListViewSubItem

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/ListViewSubItem.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · 🟡

标准完整类型名为 `System.Windows.Forms.ListViewItem.ListViewSubItem`（反射名含 `+`），本页使用短文件名。批次 004～012 完成基础构造/样式、集合 key、公共集合构造/共享 owner 视图/整数索引、typed 添加/变更、非泛型 IList/ICollection、引用查询/数组复制及枚举首范围。新枚举 2,531 条固定参考/JIT 相同、347 项新增回归通过。真实多线程/压力、已挂接样式继承、全局默认字体、Bounds、完整所属与 native 同步仍缺。

- 标准继承/契约：`ListViewItem.ListViewSubItem`，非视觉普通对象；嵌套 ListViewSubItemCollection 管理归属。
- 代码证据：[当前源码](../../../Lumina.Forms/ListViewItem.cs)。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| 🟡 | P0 | `ListViewSubItem-01` | 批次 004/005 已完成三种公共构造、继承、Text/Name/Tag 与独立/未挂接样式（01a/b）；已挂接控件及全局默认字体资源继承仍待后续。 |
| ✅ | P0 | `ListViewSubItem-01a` | 批次 004：公共无参/owner-text、可继承性、Text/Name/Tag 的默认/null/引用身份及首子项同步；37 条两端观测与 13 项新增回归通过，原 21 项构造/41 条观测保留。不新增公开 Owner；样式/完整所属/native 仍待后续。 |
| ✅ | P0 | `ListViewSubItem-01b` | 批次 005：五参样式构造、ForeColor/BackColor/Font、ResetStyle 的独立/未挂接 owner 行为；37 条两端样式观测与 15 项新增测试通过。空颜色/null Font 恢复、引用身份及共享借用 Font 寿命已检；已挂接继承、UseItemStyleForSubItems、全局默认字体和 native 刷新另列。 |
| 🟡 | P1 | `ListViewSubItem-02` | 批次 007～012 完成公共集合构造、共享 owner 数据、整数索引/首项规则、typed Add/AddRange/Insert/void Remove/RemoveAt/Clear、非泛型 IList/ICollection、引用查询/数组复制及枚举首范围（02a～f）；真实多线程/压力、完整所属与原生对应列同步仍待实现，不新增公开 Owner。 |
| ✅ | P1 | `ListViewSubItem-02a` | 批次 007：标准公共集合构造、同 owner 多视图共享数据、Count/Text/首项延迟恢复、整数索引先 index 后 value 校验与身份，72 条固定参考/Lumina 观测相同，新增 27 项回归。每集合实例的 key 缓存独立；非泛型 IList、其余集合/完整所属与 native 同步保留。 |
| ✅ | P1 | `ListViewSubItem-02b` | 批次 008：三个标准 typed Add，Add(item) 返回同一对象并拒绝 null（item），接受重复/其他 owner 对象；text/样式重载返回新对象，保留文字、样式/借用 Font 身份与首项共享视图。历史泛型接口使用显式 void 适配；74 条参考/JIT 相同，40 项新增回归通过。非泛型 IList、AddRange/Insert/完整所属与 native 同步另列。 |
| ✅ | P1 | `ListViewSubItem-02c` | 批次 009：三个标准 void AddRange，null 数组抛参数 items 且不修改状态，三重载均跳过 null 元素、保留非 null 空 Text 对象与空字符串。151 条固定参考/JIT 相同，121 项新增回归通过；顺序/身份/输入复制/重复/其他 owner、借用 Font、外部共享视图/主项 Text 和独立 key 缓存保留。非泛型 IList、Insert/Remove/完整所属/native 另列。 |
| ✅ | P1 | `ListViewSubItem-02d` | 批次 010：typed Insert 先校验 index 后拒绝 null item；公开 Remove 为非虚 void，按引用身份只删首项，null/缺失无操作，历史泛型 bool 用显式适配保留。Clear/RemoveAt 的接口 virtual-final 形状不改。203 条固定参考/JIT 相同，新增 110 项回归通过；共享视图/首项恢复/独立 key 缓存、重复/其他 owner 与借用 Font 保留。非泛型 IList、完整所属/挂接样式/native 另列。 |
| ✅ | P1 | `ListViewSubItem-02e` | 批次 011：非泛型 IList/ICollection 的对象索引/添加/插入/移除/查询、flags/SyncRoot 与显式数组 CopyTo 已实现，typed Contains/IndexOf 改标准引用身份，历史泛型/typed 复制保留。1,406 条固定参考/JIT 相同，270 项新增回归通过；类型先于位置、重复 Add 返回首次位置、空集合不验证数组/不恢复首项、非零下界/派生数组部分写入、独立 key 缓存/借用 Font 已检。完整枚举/并发、所属/挂接样式/native 另列。 |
| ✅ | P1 | `ListViewSubItem-02f` | 批次 012：public IEnumerator GetEnumerator 直接实现 IEnumerable，标准 virtual-final 形状与 Current/MoveNext/Reset、开始前/中途/结束后的双 view 变更共 2,531 条固定参考/JIT 相同，347 项新增回归通过。结构变更使 MoveNext/Reset 失效但保留缓存 Current；空 Clear 不失效，owner.SubItems 恢复首项换数据表，旧空枚举器仍为空，全部 view 共享新数据。历史泛型顺序/身份及 typed 无效 Current=null 保留。真正多线程/压力、完整所属/挂接样式/native 另列。 |
| 🟡 | P1 | `ListViewSubItem-03` | 批次 004～012 已有 Name/Tag、string key 四成员、公共集合/共享视图/整数索引、非泛型查询复制及枚举首范围；保留重复 key/Name 修改/缓存位置语义。真正多线程/压力、完整所属与 native 仍待实现。 |
| ✅ | P1 | `ListViewSubItem-03a` | 批次 006：四个标准 virtual key 成员，209 条固定参考/Lumina 观测相同，新增 38 项回归。null/空不匹配且保留缓存，等长+InvariantCultureIgnoreCase、重复 key 的上次位置、Name/集合修改及单项移除、身份与首项延迟恢复已检。完整所属/IList/构造/整数索引及 native 不随此关闭。 |
| 🟡 | P1 | `ListViewSubItem-04` | 批次 005 完成独立/未挂接的颜色字体及空值恢复；ListViewItem.UseItemStyleForSubItems、父控件继承优先级及全局默认字体仍待后续。 |
| ⬜ | P1 | `ListViewSubItem-05` | Bounds、文本/图像对齐、列重排、水平滚动、非 Details 模式/未挂载的结果；不能以固定矩形占位。 |
| 🟡 | P2 | `ListViewSubItem-06` | 批次 005 完成独立/未挂接 ResetStyle；ToString、样式序列化/设计器 .resx 及 Clone 由 owner 执行的复制语义、AOT 静态资源路径仍待后续。 |
| ⬜ | P2 | `ListViewSubItem-06a` | 批次 013 首范围：固定参考补 ToString override 元数据和空/null/Unicode/换行/Text 变更及独立/owner/共享 view/派生分派字符串轨迹，不触发首项恢复或集合变更。Clone/样式序列化/设计器资源、挂接样式/native 另列。 |
| ⬜ | P2 | `ListViewSubItem-07` | DrawSubItem 与 UseItemStyleForSubItems=false 时样式、DPI/RTL/高对比度；不擅自让每子项可编辑（标准 LabelEdit 只针对标签）。 |
| ⬜ | P2 | `ListViewSubItem-08` | owner/item/column 移除、重建 ListView、native row/cell 回收，子项关联不悬挂；不擅自释放外部共享 Font。 |
| ⬜ | P3 | `ListViewSubItem-09` | GTK 原生 column cell、AppKit NSTableCellView 样式适配；GDI DrawListViewSubItemEventArgs 的绘图边界分项登记，文本显示不算样式全通过。 |

## 三平台实现分析

### Windows

ListViewItem 持有子项模型；通过 LVM_SETITEMTEXT/SETITEM 更新，WM_NOTIFY/NM_CUSTOMDRAW 支持每子项颜色/字体，LVM_GETSUBITEMRECT 获得 Bounds。

### Linux · GTK 4

GtkColumnView 每列 factory 绑定同一 item 的相应子项，GtkLabel/Pango 样式显示；列虚拟重用时解除旧值/旧 CSS，Bounds 需坐标转换到控件坐标。

### macOS · AppKit

NSTableCellView/NSTextField 对应列单元格，按 owner 样式决定字体/颜色，表格可见单元格 rect 转到 ListView 坐标；native view 回收不得改变托管子项。

## 边界、AOT 与验收

依赖 ListViewItem、ListView 与跨平台 Font/Color/Graphics 策略；API 清单使用短文件名，但内容必须保存真实嵌套类型名，避免误把独立顶层 ListViewSubItem 当兼容。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- `b8a98eb` · 2026-04-09：ListViewItem.cs 同时加入基础 Text 子项及集合；未覆盖公开构造和样式契约。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。
- 2026-10-16 · 批次 004：先运行固定 WinForms 10.0.11 的 37 条观测，再补公共无参/owner-text、可继承性和 Text/Name/Tag；构造不会自动插入 owner.SubItems 或替换首项，null 字符串归空且保留非 null 字符串/Tag 身份。增加 13 项无 UI 回归，保留原构造与清空/恢复行为。Owner 只在库内保存，不新增公开属性；颜色字体/完整集合/native 未实现，不从指定场景推算完整兼容。

- 2026-10-16 · 批次 005：先运行固定 WinForms 10.0.11 参考端，再补五参构造、ForeColor/BackColor/Font 与 ResetStyle；37 条两端样式观测相同，新增 15 项测试。默认使用 WindowText/Window 与惰性共享的官方 SystemFonts.MessageBoxFont（本机为 Microsoft YaHei UI 9，不能代以 SystemFonts.DefaultFont）；显式 Font 保留借用身份，不因置空/替换/Reset 而释放。保留 owner/文字/首项语义。未接 Control.DefaultFont/Application 自定义默认字体与 item→ListView 回链，不能据此宣称已挂接继承/native 或完整样式兼容。

- 2026-10-16 · 批次 006：先运行固定参考端的 209 条观测，再补集合 string key 索引器、ContainsKey/IndexOfKey/RemoveByKey，新增 38 项回归。解除历史集合 sealed 以提供四个标准 virtual 成员；不改变现有泛型接口和内部构造形状。只保存一个最后命中索引，按参考端保留 Name 修改、Insert/Remove/Clear 后槽位的可观察选择，非空 miss 清除、null/空查询不清除；RemoveByKey 使用同一次查找结果只移除一项。比较先检 UTF-16 长度，再用不变文化忽略大小写，保留等长软连字符/NUL、Angstrom/Kelvin 等边界；不能以 OrdinalIgnoreCase 代替。完整集合构造、IList、所属和 native 缺口保留。

历史按对应源码路径的相关内容摘要，不把提交标题当成功能验收证明。

- 2026-10-16 · 批次 007：先固定参考，再让 owner 持有唯一子项数据，各公共集合为独立视图而非独立数据；构造 null owner 抛参数 owner，构造本身不生成首项，也不替换 owner.SubItems 的缓存视图。Text 直接读取 owner 数据；Clear 后访问 SubItems 或设置 Text 才恢复首项。整数 setter 先校验 index、后拒绝 null value，失败不改变数据；替换保留对象/Text/Tag 身份和各视图缓存。72 条对照与 27 项回归通过，旧基础/样式/key 回归保留。完整所属、Add/Insert/Remove/IList 和 native 仍缺，见 [批次 007](../batches/007.md)。

- 2026-10-16 · 批次 008：先固定参考端，再补三个 typed Add 标准返回、参数及样式语义。Add(item) 拒绝 null（参数 item），保留派生/Tag/文字/字体身份，接受重复和其他 owner 的同一对象，原集合仍保留该对象；不将此行为当完整所属/native 同步。外部视图首次添加、Clear 后添加、主项 Text、共享数据与各视图最后 key 位置均保留。显式 ICollection<T>.Add 承接历史 void 消费；74 条参考/JIT 观测相同，40 项新增回归通过，原构造/样式/key/集合回归保留。清空、移除、ResetStyle、替换字体或子项不释放借用 Font，见 [批次 008](../batches/008.md)。

- 2026-10-16 · 批次 009：先固定参考端，再补三个标准 typed AddRange。三重载 null 数组均抛 ArgumentNullException(items)，失败/空数组不改变共享数据或初始化首项；三者均跳过 null 元素，包括文本数组的 null 文字，与单项 Add(null) 归空不同。非 null 空 Text 对象与空字符串仍添加；原构造器的 null 元素归空行为保持，不改用 AddRange。顺序、原对象/派生/Tag/字符串身份、输入槽复制、重复/其他 owner、样式与借用 Font 寿命均有对照，外部首次/Clear 后视图、主项 Text 及各视图最后 key 索引保留。151 条固定参考/JIT 相同，121 项新增回归通过；未将本批新 AddRange 计为实际 AOT 运行验收，完整 IList/Insert/Remove/所属/native 仍缺，见 [批次 009](../batches/009.md)。

- 2026-10-16 · 批次 010：先固定参考再实现 typed Insert/Remove 首范围。Insert 先校验 index（允许 Count），再拒绝 null item，原对象/文字/Tag/样式/其他 owner 数据及共享视图保留；公开非虚 void Remove 按 ReferenceEquals 查找首项，null/缺失不操作，派生 Equals 不参与。历史泛型 Insert 和 bool Remove 显式适配保留；Clear/RemoveAt 保持接口 virtual-final，不新增可覆盖 API。203 条固定参考/JIT 观测相同，110 项新增回归通过；首项移除、空集合惰性恢复、各视图独立 key 缓存与借用 Font 寿命均覆盖。未将新集合范围记为实际 AOT 运行验收，非泛型 IList、Contains/IndexOf/CopyTo 全契约、完整所属/挂接样式/native 仍缺，见 [批次 010](../batches/010.md)。

- 2026-10-16 · 批次 011：固定参考后补非泛型 IList/ICollection 消费。对象 setter/Insert/Add 先验证类型，null/错型为 ArgumentException（value/item），typed 索引/Insert 的既有验证顺序保持；重复 Add 返回首次引用索引。Contains/IndexOf 标准 subItem 参数与非 virtual 形状、泛型显式适配均按引用身份，不调用派生 Equals。ICollection.CopyTo 为 private virtual-final 显式槽（dest/index），没有 public Array 重载；历史 typed CopyTo 通过同一路径保留。空集合跳过全部数组校验且不恢复首项，非空保留 rank/index/容量/类型顺序、非零下界和派生数组失败前写入；只把不兼容数组的 ArrayTypeMismatchException 转为标准 ArgumentException，保留 InvalidCastException。各 view 的 SyncRoot 是自身，IsSynchronized 按参考报告 true，不等于完整并发已验收。1,406 条固定参考/JIT 相同、270 项新增回归通过，共享数据/独立 key 缓存、借用 Font 与其他 owner 对象保持；新接口未提前计为实际 AOT 运行验收，完整枚举/并发、所属/挂接样式/native 另列，见 [批次 011](../batches/011.md)。

- 2026-10-16 · 批次 012：固定参考后修正 public GetEnumerator 为 IEnumerator，并以 public virtual-final 直接实现 IEnumerable；历史 IEnumerable<T> 显式消费、typed 无效 Current=null 保留。2,531 条固定参考/JIT 全同，347 项新增回归通过。成功替换（包括同对象）/Add/Insert/Remove/非空 Clear 使 MoveNext/Reset 失效而 Current 保留缓存身份；失败/移除缺失/文字修改不失效。空 Clear 不改变版本，owner.SubItems 在空集合恢复首项时换数据表，所有 view 共享新表而旧空枚举器保持为空；不依赖 Array/List 内部反射或新增公开枚举类型。重复派生身份/双 view/独立 key 缓存与借用 Font 寿命保留。新枚举未记实际 AOT 运行验收；真实多线程/压力、完整所属/挂接样式/native 仍另列，见 [批次 012](../batches/012.md)。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.listviewitem.listviewsubitem?view=windowsdesktop-10.0
- https://docs.gtk.org/gtk4/class.ColumnView.html
- https://developer.apple.com/documentation/appkit/nstablecellview
