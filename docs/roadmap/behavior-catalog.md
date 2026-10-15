# 行为场景：每个成员怎样验收

本文件给逐成员附件提供统一的场景编号；各控件文档中的专有行为继续单独验收。它避免复制几万行相同文字，不减少分母。场景 ID 使用 `控件名/API-ID/场景ID`，将输入、预期值和事件序列补齐后冻结；未知预期先在真实 .NET 10.0.11 端观察，不能猜测后写测试。

## 属性通用场景

| ID | 适用范围 | 明确的验收内容 |
| --- | --- | --- |
| P01 | 每个公开属性 | 构造后默认值；Designer属性元数据与运行值分别核对；继承值、系统环境相关默认单独记录 |
| P02 | 每个可写属性 | 未挂载/未建句柄赋值后读回，挂载显示后检查原生状态/布局；无句柄时不无故创建句柄 |
| P03 | 每个可写属性 | 已显示后变更，原生状态、事件、布局、可访问性随之更新；重建句柄后值保留 |
| P04 | 每个可写属性 | 再次赋相同值、null/空、上下界、无效枚举；异常类型、状态是否保留、是否触发通知与参考端一致 |
| P05 | 有通知的属性 | 关联Changed事件的次数、先后和线程；程序赋值与原生用户输入分别记录 |
| P06 | 布局/父级/资源相关属性 | 父级默认继承、重设父级、DPI/Font/RTL变化及SuspendLayout期间变更；所有权与释放正确 |
| P07 | 只读/索引/集合属性 | 活对象还是快照、对象身份稳定性、索引边界、枚举与修改、返回值和原生回读；不可只判断非null |
| P08 | 可由设计器序列化的属性 | 保存/重开/Reset/ShouldSerialize或元数据、.resx本地化、初始化顺序；两个目标同源编译 |

只读属性不测试setter，但仍有P01/P03中外部状态变更/P07等适用场景；不适用要记录原因。隐藏/覆盖的继承属性按最派生控件再次确认。

逻辑对象、集合及部分附属类型没有标准Dispose或独立原生窗口。它们的生命周期场景跟随所属宿主释放，不新增Dispose/Handle等公开成员来满足通用检查模板；历史构造即抛异常的类型按其实际可达范围核对。

## 事件通用场景

| ID | 适用范围 | 明确的验收内容 |
| --- | --- | --- |
| E01 | 每个公开事件 | sender、委托类型、参数类型/内容、订阅/退订、事件处理线程 |
| E02 | 有用户动作的事件 | 鼠标/键盘/辅助技术触发时机与次数；禁用、隐藏、不可选择状态不错误触发 |
| E03 | 有程序路径的事件 | 属性赋值、集合操作、PerformClick/Select等API触发规则；无变化赋值不得擅自增加事件 |
| E04 | 每个可取消事件 | Cancel/Handled/Effect等设置后的状态、焦点、集合、模态、后续事件；取消后不偷偷执行操作 |
| E05 | 每个状态链事件 | 与Before/After、选择/文本/绑定/验证/关闭等相关事件顺序对照；不人为设“看起来合理”的顺序 |
| E06 | 会更改状态的处理器 | 处理器里重设属性、移除当前项/控件、Dispose、再次打开/关闭；状态可预测且不重复派发 |
| E07 | 每个事件 | 句柄重建/原生回调解绑/重复Dispose后无幽灵事件；委托root与原生回调寿命正确 |

## 方法、集合和生命周期

| ID | 范围 | 场景 |
| --- | --- | --- |
| M01 | 每个公开方法/重载 | 同名参数调用、可选参数、返回类型/返回值、ref/out、泛型约束及源码重载解析 |
| M02 | 每个改变状态的方法 | 未建句柄、已显示、隐藏、重建、释放后调用；状态/事件/异常与基线一致 |
| M03 | 异步/跨线程方法 | UI与后台调用、完成线程、返回值、异常、取消、线程退出与窗口释放时的结果 |
| C01 | 每个集合 | Add/AddRange/Insert/Remove/RemoveAt/Clear、索引读写、顺序、重复值/null、返回值和异常 |
| C02 | 有所有者的集合 | Owner/Parent/Index、跨集合移动、移除再加、原生回读、句柄重建、批次更新 |
| C03 | 有选择的集合 | 选中对象/索引/多选在插入删除排序后的变化；删除当前项触发事件次序 |
| L01 | 每个控件/组件 | 创建→初始化→挂载→显示→操作→隐藏→移除/重新挂载→释放；每步可观察状态 |
| L02 | 拥有资源的对象 | 父级释放、外部资源是否被误释放、重复Dispose、对象/句柄计数是否恢复；常规重复开关即可，不先造压力框架 |
| D01 | 可序列化控件 | 真实WinForms设计器打开、修改、保存、重开；相同Designer代码在Lumina目标编译运行 |
| A01 | 声明Native AOT支持的功能 | 发布后真正启动、执行动作、查看结果/事件、正常关闭；仅编译/PE检查不足 |
| X01 | 每个平台声明的能力 | 平台本机原生控件、输入法/焦点、键盘、DPI、辅助技术、资源、部署；不可用Windows证据替代 |

## 控件族的专有场景入口

| 族 | 必须额外冻结的具体行为 | 文档入口 |
| --- | --- | --- |
| 窗体/应用 | 首次与重复显示、模态owner、DialogResult、默认/取消按钮、关闭取消、多窗退出、线程投递 | [Form](controls/Form.md)、[Application](controls/Application.md) |
| 按钮/选择 | AutoCheck、三态循环、组内互斥、助记键、默认按钮、验证后激活、命令联动 | [Button](controls/Button.md)、[CheckBox](controls/CheckBox.md)、[RadioButton](controls/RadioButton.md) |
| 文本/掩码/数字 | 光标/选区/Undo、粘贴/IME、RTF选区格式、掩码拒绝、decimal范围/精度、文化解析 | [TextBox](controls/TextBox.md)、[RichTextBox](controls/RichTextBox.md)、[NumericUpDown](controls/NumericUpDown.md) |
| 列表/树 | 项/节点所有者、原生同步、展开取消、勾选取消、选择与排序、虚拟模式、编辑提交/取消 | [TreeView](controls/TreeView.md)、[ListView](controls/ListView.md)、[ComboBox](controls/ComboBox.md) |
| 日期 | 闰日/上下界、CheckBox、格式/文化、用户选择和程序赋值、多月/范围/粗体日期 | [DateTimePicker](controls/DateTimePicker.md)、[MonthCalendar](controls/MonthCalendar.md) |
| 容器/布局 | Dock顺序、Anchor、PreferredSize、自动尺寸、滚动、行列跨度、换行、分割取消、缩放往返 | [TableLayoutPanel](controls/TableLayoutPanel.md)、[FlowLayoutPanel](controls/FlowLayoutPanel.md)、[SplitContainer](controls/SplitContainer.md) |
| 菜单/工具条 | Items双向映射、快捷键/助记键、下拉取消、Owner移动、溢出、宿主输入/布局、移除后清理 | [ToolStrip](controls/ToolStrip.md)、[ContextMenuStrip](controls/ContextMenuStrip.md) |
| 绑定/表格 | Currency位置、Format/Parse、提交/取消、验证错误、通知重入、排序/冻结/虚拟编辑/行新增删除 | [BindingSource](controls/BindingSource.md)、[DataGridView](controls/DataGridView.md) |
| 组件/对话框 | 扩展属性、owner模态、OK提交/Cancel回滚、文件过滤、托盘菜单、提示时序、UI计时器 | [CommonDialog](controls/CommonDialog.md)、[NotifyIcon](controls/NotifyIcon.md)、[Timer](controls/Timer.md) |
| TaskDialog | 页与子元素绑定、Created/Destroyed、导航、按钮返回对象、AllowCloseDialog、取消、进度实时更新、链接、异步模态与回调释放 | [TaskDialog](controls/TaskDialog.md)、[TaskDialogPage](controls/TaskDialogPage.md)、[TaskDialogControl](controls/TaskDialogControl.md) |
| 历史占位 | 基线过时诊断、构造标准异常、不可达成员边界；可选旧版UI恢复单列，不套用可运行控件场景假造通过 | [补漏证据](scope-audit.md)、[Menu](controls/Menu.md)、[DataGrid](controls/DataGrid.md) |

每族表格只给场景定位，不取代控件页具体任务和成员附件。对照完成后保存结果/轨迹，并以实现提交更新历史。当前未冻结全部具体值的行为分母不能生成可靠完成百分比。
