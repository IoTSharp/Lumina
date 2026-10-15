# PropertyTab

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/PropertyTab.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · ⬜

【M4】未发现 Lumina 对应实现。这是System.Windows.Forms.Design运行时导出的抽象页签，类型命名空间不能移到Forms根。

- 标准继承/契约：`System.Windows.Forms.Design.PropertyTab : System.Object`；抽象类型。
- 代码证据：[Lumina.Forms 库目录](../../../Lumina.Forms/)（未找到本类型定义）。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `PropertyTab-01` | protected构造、抽象TabName/GetProperties(object,Attribute[])、IDisposable与完整命名空间，外部自定义页签可编译。 |
| ⬜ | P1 | `PropertyTab-02` | Bitmap/HelpKeyword/Components默认与set、引用/复制/空值和多对象范围；不把Bitmap无故当成永久共享原生句柄。 |
| ⬜ | P1 | `PropertyTab-03` | GetProperties全部重载、ITypeDescriptorContext/Attribute[]过滤和GetDefaultProperty，默认派生委托路径与返回顺序。 |
| ⬜ | P1 | `PropertyTab-04` | CanExtend对单对象/多选/空对象的规则，不能无条件把所有tab附加到所有组件。 |
| ⬜ | P1 | `PropertyTab-05` | PropertyGrid.PropertyTabs/AddTabType/RemoveTabType及PropertyTabScope关系；换选中对象后Components和当前tab更新。 |
| ⬜ | P2 | `PropertyTab-06` | PropertyTabChanged与PropertyTabCollection、刷新/排序/过滤/编辑事务，tab切换不误提交验证失败的属性。 |
| ⬜ | P2 | `PropertyTab-07` | 自定义PropertyDescriptor/TypeConverter/ICustomTypeDescriptor静态可达；AOT无需用户改自创schema，派生tab构造/注册不依赖动态发射。 |
| ⬜ | P2 | `PropertyTab-08` | Dispose()/Dispose(bool)/终结器的覆盖和幂等，Bitmap/Components/服务引用所有权，PropertyGrid释放或移除tab后退订。 |
| ⬜ | P3 | `PropertyTab-09` | 三平台内置属性页与自定义tab、图标/名称/帮助入口和只读属性样本；Windows专属设计服务另标边界。 |

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

- https://learn.microsoft.com/dotnet/api/system.windows.forms.design.propertytab?view=windowsdesktop-10.0
- https://docs.gtk.org/gtk4/class.ColumnView.html
- https://developer.apple.com/documentation/appkit/nstableview
