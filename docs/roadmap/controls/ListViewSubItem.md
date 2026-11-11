# ListViewSubItem

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/ListViewSubItem.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · 🟡

标准完整类型名为 `System.Windows.Forms.ListViewItem.ListViewSubItem`（反射名含 `+`），本页使用短文件名。批次 004～013 完成基础构造/样式、集合 key、公共集合构造/共享 owner 视图/整数索引、typed 添加/变更、非泛型 IList/ICollection、引用查询/数组复制、枚举及 ToString 首范围。新字符串 58 条固定参考/JIT 相同、63 项新增回归通过。真实多线程/压力、已挂接样式继承、全局默认字体、Bounds、完整所属与 native 同步仍缺。

- 标准继承/契约：`ListViewItem.ListViewSubItem`，非视觉普通对象；嵌套 ListViewSubItemCollection 管理归属。
- 代码证据：[当前源码](../../../Lumina.Forms/ListViewItem.cs)。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| 🟡 | P0 | `ListViewSubItem-01` | 批次 004/005 已完成三种公共构造、继承、Text/Name/Tag 与独立/未挂接样式（01a/b）；已挂接控件及全局默认字体资源继承仍待后续。 |
| 🟡 | P1 | `ListViewSubItem-02` | 既有集合typed/非泛型契约及共享view保持；本轮补挂接后的文字/集合增删同步到真实LVM单元格。完整所属、样式、并发压力/异常事务仍待，不新增公开Owner。 |
| 🟡 | P1 | `ListViewSubItem-03` | 批次 004～012 已有 Name/Tag、string key 四成员、公共集合/共享视图/整数索引、非泛型查询复制及枚举首范围；保留重复 key/Name 修改/缓存位置语义。真正多线程/压力、完整所属与 native 仍待实现。 |
| 🟡 | P1 | `ListViewSubItem-04` | 批次 005 完成独立/未挂接的颜色字体及空值恢复，016补主项样式首项代理、主/子项共享默认Font及UseItemStyleForSubItems未挂接状态（ListViewItem-06a）；已挂接绘制时UseItemStyle消费、父控件继承优先级及全局默认字体仍待后续。 |
| ⬜ | P1 | `ListViewSubItem-05` | Bounds、文本/图像对齐、列重排、水平滚动、非 Details 模式/未挂载的结果；不能以固定矩形占位。 |
| 🟡 | P2 | `ListViewSubItem-06` | 批次 005 完成独立/未挂接 ResetStyle，批次 013 完成 ToString 首范围；样式序列化/设计器 .resx 及 Clone 由 owner 执行的复制语义、AOT 静态资源路径仍待后续。 |
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

## 已完成任务

✅ 表示该行定义范围代码完成；父项的剩余代码与平台验收仍按当前任务表记录。

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ✅ | P0 | `ListViewSubItem-01a` | 批次 004：公共无参/owner-text、可继承性、Text/Name/Tag 的默认/null/引用身份及首子项同步；37 条两端观测与 13 项新增回归通过，原 21 项构造/41 条观测保留。不新增公开 Owner；样式/完整所属/native 仍待后续。 |
| ✅ | P0 | `ListViewSubItem-01b` | 批次 005：五参样式构造、ForeColor/BackColor/Font、ResetStyle 的独立/未挂接 owner 行为；37 条两端样式观测与 15 项新增测试通过。空颜色/null Font 恢复、引用身份及共享借用 Font 寿命已检；已挂接继承、UseItemStyleForSubItems、全局默认字体和 native 刷新另列。 |
| ✅ | P1 | `ListViewSubItem-02a` | 批次 007：标准公共集合构造、同 owner 多视图共享数据、Count/Text/首项延迟恢复、整数索引先 index 后 value 校验与身份，72 条固定参考/Lumina 观测相同，新增 27 项回归。每集合实例的 key 缓存独立；非泛型 IList、其余集合/完整所属与 native 同步保留。 |
| ✅ | P1 | `ListViewSubItem-02b` | 批次 008：三个标准 typed Add，Add(item) 返回同一对象并拒绝 null（item），接受重复/其他 owner 对象；text/样式重载返回新对象，保留文字、样式/借用 Font 身份与首项共享视图。历史泛型接口使用显式 void 适配；74 条参考/JIT 相同，40 项新增回归通过。非泛型 IList、AddRange/Insert/完整所属与 native 同步另列。 |
| ✅ | P1 | `ListViewSubItem-02c` | 批次009：三个标准void AddRange，null数组抛参数items且不修改状态，三重载均跳过null元素、保留非null空Text对象与空字符串。151条固定参考/JIT相同，121项新增回归；顺序/身份/输入复制/重复/其他owner、借用Font、共享view/主项Text和独立key缓存保留。后续非泛型IList、Insert/Remove及枚举首范围见已完成02d/e/f；真正并发/完整所属/挂接样式/native仍缺。 |
| ✅ | P1 | `ListViewSubItem-02d` | 批次010：typed Insert先校验index后拒绝null item；公开Remove为非虚void，按引用身份只删首项，null/缺失无操作，历史泛型bool用显式适配保留。Clear/RemoveAt接口virtual-final形状不改。203条固定参考/JIT相同，新增110项回归；共享view/首项恢复/独立key缓存、重复/其他owner与借用Font保留。后续非泛型IList/枚举首范围见已完成02e/f，真正并发/完整所属/挂接样式/native仍缺。 |
| ✅ | P1 | `ListViewSubItem-02e` | 批次011：非泛型IList/ICollection的对象索引/添加/插入/移除/查询、flags/SyncRoot与显式数组CopyTo已实现，typed Contains/IndexOf改标准引用身份，历史泛型/typed复制保留。1,406条固定参考/JIT相同，270项新增回归；类型先于位置、重复Add返回首次位置、空集合不验证数组/不恢复首项、非零下界/派生数组部分写入、独立key缓存/借用Font已检。后续枚举首范围见已完成02f，真正并发/完整所属/挂接样式/native仍缺。 |
| ✅ | P1 | `ListViewSubItem-02f` | 批次 012：public IEnumerator GetEnumerator 直接实现 IEnumerable，标准 virtual-final 形状与 Current/MoveNext/Reset、开始前/中途/结束后的双 view 变更共 2,531 条固定参考/JIT 相同，347 项新增回归通过。结构变更使 MoveNext/Reset 失效但保留缓存 Current；空 Clear 不失效，owner.SubItems 恢复首项换数据表，旧空枚举器仍为空，全部 view 共享新数据。历史泛型顺序/身份及 typed 无效 Current=null 保留。真正多线程/压力、完整所属/挂接样式/native 另列。 |
| ✅ | P1 | `ListViewSubItem-03a` | 批次 006：四个标准 virtual key 成员，209 条固定参考/Lumina 观测相同，新增 38 项回归。null/空不匹配且保留缓存，等长+InvariantCultureIgnoreCase、重复 key 的上次位置、Name/集合修改及单项移除、身份与首项延迟恢复已检。完整所属/IList/构造/整数索引及 native 不随此关闭。 |
| ✅ | P2 | `ListViewSubItem-06a` | 批次 013：标准 ToString override 的元数据、原样文本和空/null/Unicode/换行/Text 变更及独立/owner/共享 view/重复/派生分派完成；58 条固定参考/JIT 相同，63 项新增回归通过。读取不恢复首项、不改变集合/枚举/独立 key 缓存，不读取 Tag 或样式、不释放借用 Font。Clone/样式序列化/设计器资源、挂接样式/native 另列，新字符串未计实际 AOT 运行验收。 |

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.listviewitem.listviewsubitem?view=windowsdesktop-10.0
- https://docs.gtk.org/gtk4/class.ColumnView.html
- https://developer.apple.com/documentation/appkit/nstablecellview
