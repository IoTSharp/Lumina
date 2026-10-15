# PropertyGrid

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/PropertyGrid.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · ⬜

未实现。GTK/AppKit 都没有可直接覆盖 WinForms PropertyGrid 公开 API、TypeDescriptor 与编辑器生态的完整原生控件；可用原生树表和编辑器组合，但类型描述、分组、验证仍需库实现。

- 标准继承/契约：`PropertyGrid : ContainerControl`；需要 GridItem/GridItemCollection/GridItemType、PropertyTab、PropertyTabCollection、编辑器服务及相关事件。
- 代码证据：[Lumina.Forms 库目录](../../../Lumina.Forms/)（未找到本类型定义）。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `PropertyGrid-01` | 先完成 ContainerControl、焦点/验证和 AOT PropertyDescriptor/TypeConverter 契约；盘点 GridItem、PropertyTab、UITypeEditor 等类型跨程序集边界，不直接引入 Windows-only 设计程序集到 net10.0。 |
| ⬜ | P1 | `PropertyGrid-02` | SelectedObject/SelectedObjects、SelectedGridItem、PropertySort、BrowsableAttributes；null/空数组/多对象共同属性/不同类型/ReadOnlyAttribute 的项集和选择恢复。 |
| ⬜ | P1 | `PropertyGrid-03` | TypeDescriptor/ICustomTypeDescriptor/ITypedList/TypeDescriptionProvider；动态属性、PropertyDescriptor.GetValue/SetValue、Converter/Editor 检索与过滤，以已知类型源码生成/明确元数据保留实现 AOT。 |
| ⬜ | P1 | `PropertyGrid-04` | 字符串/数字/枚举/bool/日期/颜色/字体/可展开对象/集合：转换、下拉/模态编辑器、只读、默认值、Reset/ShouldSerialize；验证失败不修改目标对象。 |
| ⬜ | P1 | `PropertyGrid-05` | PropertyValueChanged 的 ChangedItem/OldValue、SelectedGridItemChanged、SelectedObjectsChanged、PropertySortChanged；多对象批量修改和某一对象失败时的实际官方行为，不自行发明全事务语义。 |
| ⬜ | P1 | `PropertyGrid-06` | GridItem.Label/Value/PropertyDescriptor/Parent/GridItems/Expandable/Expanded/Select；按类别/字母排序、展开折叠、ExpandAllGridItems/CollapseAllGridItems、键盘左右/Tab/Enter/Esc。 |
| ⬜ | P2 | `PropertyGrid-07` | HelpVisible/ToolbarVisible/CommandsVisibleIfAvailable/CanShowCommands、PropertyTabs/RefreshTabs/SelectedTab/TabStop；工具栏切换时编辑提交/取消与选择保持。 |
| ⬜ | P2 | `PropertyGrid-08` | ViewBackColor/ViewForeColor/LineColor/CategoryForeColor/HelpBackColor/HelpForeColor/SelectedItemWithFocusBackColor 等颜色、字号/DPI/RTL/高对比度、可访问性属性树。 |
| ⬜ | P2 | `PropertyGrid-09` | Refresh、对象属性外部变化、IComponentChangeService/服务查询的兼容范围，默认编辑器和用户继承 UITypeEditor 的 AOT 实例化边界明确。 |
| ⬜ | P2 | `PropertyGrid-10` | 容器切换、对象 Dispose、更换 SelectedObjects、事件处理重入、模态编辑器打开期间关闭窗口；解绑 descriptor/provider/目标对象订阅。 |
| ⬜ | P3 | `PropertyGrid-11` | GTK/AppKit 原生树表与编辑器分别实现上述行为；GDI PaintValue、Win32 编辑器服务/宿主句柄等列专属边界，不能把属性名文本列表称完整属性窗格。 |
| ⬜ | P3 | `PropertyGrid-12` | 设计器双目标静态属性窗格样本、已知 POCO/动态 descriptor/多选/自定义 converter/自定义 editor 的 AOT 用例分别验收；属性族/事件族/编辑行为分别达到门槛。 |

## 三平台实现分析

### Windows

使用托管属性元数据与网格逻辑，Win32 原生宿主/工具栏与 EDIT/COMBOBOX/系统对话框作为编辑器。PropertyGrid 不是现成系统窗口类；可参考微软实现的契约与服务调用，避免照搬依赖动态类型发现/设计时 COM 的路径。

### Linux · GTK 4

GtkColumnView + GtkTreeListModel/GtkTreeExpander，值列使用 GtkEntry/DropDown/CheckButton 等；工具条用原生 GtkBox/Button，帮助区 Label/TextView。编辑器服务与 TypeConverter 不由 GTK 提供，需托管兼容；递归属性对象引用必须按标准展开模型处理。

### macOS · AppKit

NSOutlineView 两列 + 原生 NSTextField/NSPopUpButton/NSButton，工具栏/说明区原生组合。不能把 Cocoa KVC/KVO 属性模型当成 .NET TypeDescriptor 的等价品；多对象值合并和编辑器服务仍由库提供。

## 边界、AOT 与验收

AOT 不应要求用户抛弃 SelectedObject 改用自创 schema API。对编译期已知 SelectedObject 类型生成标准 descriptor/访问器，对 ICustomTypeDescriptor 优先调用接口；无法静态发现的插件 editor 类型需明示 metadata/静态注册约束，而非随意反射后吞错。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.propertygrid?view=windowsdesktop-10.0
- https://learn.microsoft.com/dotnet/api/system.windows.forms.griditem?view=windowsdesktop-10.0
- https://learn.microsoft.com/dotnet/api/system.componentmodel.typeconverter?view=net-10.0
- https://docs.gtk.org/gtk4/class.ColumnView.html
- https://docs.gtk.org/gtk4/class.TreeListModel.html
- https://developer.apple.com/documentation/appkit/nsoutlineview
