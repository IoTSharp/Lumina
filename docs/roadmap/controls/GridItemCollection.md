# GridItemCollection

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/GridItemCollection.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · ⬜

【M4】未发现 Lumina 对应实现。属性树的只读节点集合，公开Empty字段与字符串索引有自己的查找语义。

- 标准继承/契约：`System.Windows.Forms.GridItemCollection : System.Object`。
- 代码证据：[Lumina.Forms 库目录](../../../Lumina.Forms/)（未找到本类型定义）。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `GridItemCollection-01` | 准确构造可见性、ICollection/IEnumerable、公开静态Empty字段形状；不新增Add/Remove或公开构造作为绑定入口。 |
| ⬜ | P1 | `GridItemCollection-02` | Count、整数/字符串label索引器、GetEnumerator及接口CopyTo/IsSynchronized/SyncRoot全量；空集合和缺失label返回规则。 |
| ⬜ | P1 | `GridItemCollection-03` | Root/Category/Property/ArrayValue子项顺序，PropertySort切换、Browsable/属性过滤和同名Label查找的实际规则。 |
| ⬜ | P1 | `GridItemCollection-04` | GridItem.GridItems集合身份及展开前后是否惰性生成，不能每次读取返回随机顺序/完全独立节点。 |
| ⬜ | P2 | `GridItemCollection-05` | null/越界索引、CopyTo数组不足/类型不匹配、枚举中刷新属性树的标准反馈。 |
| ⬜ | P2 | `GridItemCollection-06` | 刷新/换对象/多选交集后的集合和旧节点Parent，SelectedGridItem不得指向无效当前树。 |
| ⬜ | P2 | `GridItemCollection-07` | 只读集合的接口写操作准确不支持，不能静默忽略；标准接口继承亦进入API附件。 |
| ⬜ | P2 | `GridItemCollection-08` | 集合与业务对象/descriptor生命期，PropertyGrid释放后无事件强引用，不擅自释放Tag/Value。 |
| ⬜ | P3 | `GridItemCollection-09` | 三平台属性树顺序、索引、选择与展开一致，空树不以异常吞掉真实属性解析失败。 |

## 三平台实现分析

### Windows

复用PropertyGrid内部托管属性树/descriptor/事务模型；Win32宿主加原生EDIT/COMBOBOX、按钮和系统主题绘制。GridItem与PropertyTab本身没有独立Win32控件，不能因TreeView/ListView有句柄就视为完整属性检查器。

### Linux · GTK 4

GtkColumnView/GtkTreeListModel及GtkEntry/GtkDropDown/GtkCheckButton等原生编辑部件表示属性树，托管GridItem身份与TypeDescriptor协议保留。GTK没有与PropertyGrid/PropertyTab完整等价的单一原生控件。

### macOS · AppKit

NSOutlineView/NSTableColumn与NSTextField/NSButton/NSPopUpButton编辑器组成原生属性检查器；重用视图/field editor不替换公开GridItem身份。AppKit没有现成的完整WinForms PropertyGrid。

## 边界、AOT 与验收

依赖PropertyGrid、Binding/TypeDescriptor、TypeConverter/UITypeEditor和编辑事务。AOT保留标准descriptor/扩展写法，优先静态可发现类型和元数据；未知动态类型说明可达要求。不要另造必需的属性Schema/表单DSL；Windows专属编辑器和任意GDI绘制边界单列。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.griditemcollection?view=windowsdesktop-10.0
- https://docs.gtk.org/gtk4/class.ColumnView.html
- https://developer.apple.com/documentation/appkit/nstableview
