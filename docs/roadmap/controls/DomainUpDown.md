# DomainUpDown

基线：2026-10-15 · .NET 10.0.11。目标继承：`DomainUpDown : UpDownBase`。本页是实施与验收任务，未声称已完成兼容。

## 当前证据与依赖

⬜ 无 Lumina.Forms/DomainUpDown.cs；不能用 ComboBox 的存在替代 DomainUpDown。此控件是可编辑条目步进，属于完整 WinForms 对标范围。

源码定位：`Lumina.Forms/DomainUpDown.cs`（尚不存在）；共同问题见 [Control](../../../Lumina.Forms/Control.cs) 和 [Win32 interop](../../../Lumina.Forms/Win32.cs)。先决依赖：UpDownBase、DomainUpDown.DomainUpDownItemCollection、对象显示文字与标准排序。

公开成员（含继承的全部属性、事件和方法）逐项见 [DomainUpDown API 清单](../api/DomainUpDown.md)；共同行为、AOT、设计器与计数方法见 [基础任务](../foundations.md)。本表列出具体实现/验收动作，API 清单中余下成员也必须逐条关闭，不能把表外成员当作已支持。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 先修层级/阻塞，P1 完成常用契约，P2 补边界/高级场景，P3 完成平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| DomainUpDown-001 | P0 | ⬜ | 实现 DomainUpDown : UpDownBase 与标准 DomainUpDownItemCollection；公开 Items/SelectedIndex/SelectedItem/Sorted/Wrap/SelectedItemChanged 准确签名。 |
| DomainUpDown-002 | P1 | ⬜ | Items 的 Add/Insert/Remove/RemoveAt/Clear/indexer、null/重复/对象ToString、无效索引；选择对象身份在插入/排序/删除后正确。 |
| DomainUpDown-003 | P1 | ⬜ | SelectedIndex 默认 -1、SelectedItem 默认 null；无条目/单条目、无效值赋入、删除已选项及替换已选对象按基线处理。 |
| DomainUpDown-004 | P1 | ⬜ | Sorted=false、Wrap=false；排序文化/大小写、后续插入、重复显示文字、排序开关前后选择不误指向别的对象。 |
| DomainUpDown-005 | P1 | ⬜ | UpButton/DownButton：首尾、未选中态、空集合、Wrap true/false，鼠标/键盘/滚轮/代码调用结果一致，步进方向不被默认原生索引方向倒置。 |
| DomainUpDown-006 | P1 | ⬜ | Text 与用户编辑、ReadOnly、首字符搜索、未匹配文字、Enter/失焦验证；外部修改 Text 是否影响选择按基线分别验收。 |
| DomainUpDown-007 | P1 | ⬜ | SelectedItemChanged、TextChanged、验证和焦点事件顺序；程序同值/点击步进/集合变化/排序不产生重复或漏发。 |
| DomainUpDown-008 | P2 | ⬜ | 条目 ToString 异常、文化切换、双向/CJK/emoji 显示与搜索；字长不决定对象相等，禁止用字符串字典丢掉重复条目。 |
| DomainUpDown-009 | P2 | ⬜ | AutoSize/Font/DPI/UpDownAlign，辅助技术暴露当前条目及上下操作；不可见项不额外创建可见下拉控件。 |
| DomainUpDown-010 | P3 | ⬜ | Designer 静态 Items.AddRange 及 SelectedIndex 初始化顺序、AOT 对象显示/集合序列化和 Dispose 清理；三平台同一条目步进场景记录。 |
| DomainUpDown-CLOSE | P3 | ⬜ | 对照 API 清单逐个关闭剩余属性、事件、方法/重载与受保护兼容点；属性写入后读回、通知顺序/取消/重入、默认值、异常、未建句柄/重建/释放后行为分别有证据。Windows 与两原生平台各出属性/事件/行为分子分母，未验收项不能标🧪 验收通过。 |

## 三平台的原生实现评估

**Windows**：UpDownBase 原生 EDIT+UPDOWN 组合；托管 Items 决定 SelectedIndex/SelectedItem，UDN_DELTAPOS 触发条目切换。原生 UDS_WRAP 仅作辅助，需与托管 Wrap/Sorted/未匹配文本保持一致。

**Linux / GTK4**：GtkSpinButton 用整数索引调整与 input/output 信号呈现任意对象文字，或原生 Entry+上下按钮。不能改用 GtkDropDown 使公开交互变成下拉选择；索引范围、键入首字符搜索与对象身份仍在托管模型。

**macOS / AppKit**：NSTextField+NSStepper 组合；以托管 Items 索引执行步进，键入匹配交给委托；不换成 NSPopUpButton 导致 UX/API 语义变化。Wrap、Sorted、未选中态与原生NSStepper的循环值分别处理。

跨平台工作排在 Windows 行为基线与该类最小原生试验之后。先复用同一公共 WinForms 类型与状态模型，在库内封装平台差异；本路线不要求新增平台项目或公开抽象。每平台至少覆盖：默认值/边界，代码与用户修改双向同步，事件顺序/取消/重入，键盘/输入法（适用时），DPI/RTL/本地化，无障碍，创建/重建/释放，AOT 实际运行；纯编译或仅显示窗口不足以通过。

验收口径：基线固定为 .NET 10.0.11 WinForms。Windows 的公开属性、公开事件、行为场景分别 ≥98%；Linux/macOS 在逐项确认的可移植分母中分别 ≥98%。每个重载、访问器、事件参数与行为场景独立登记，继承成员不遗漏；签名同名、字段存值、任务写完均不能作为通过分子。原生外观差异与 Windows 专属成员保留在清单中，不能用“平台不可等价”自动划掉普通业务需求。当前各项尚无完整对照证据，不发布已达标百分比。

## 修改历史

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 尚无该类型实现文件的提交历史；不将其他控件或本轮规划计为已实现。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

## 官方依据

- [WinForms DomainUpDown API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.domainupdown?view=windowsdesktop-10.0)，精确签名以本机 10.0.11 引用程序集生成的 API 清单为准。
- [Windows 原生控件参考](https://learn.microsoft.com/windows/win32/controls/up-down-controls)。
- [GTK4 SpinButton](https://docs.gtk.org/gtk4/class.SpinButton.html)。
- [AppKit nsstepper](https://developer.apple.com/documentation/appkit/nsstepper)。
