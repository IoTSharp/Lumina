# UpDownBase

基线：2026-10-15 · .NET 10.0.11。目标继承：`UpDownBase : ContainerControl`。本页是实施与验收任务，未声称已完成兼容。

## 当前证据与依赖

⬜ 无 Lumina.Forms/UpDownBase.cs。NumericUpDown 目前直接继承 TextBox，缺少增减按钮宿主与 UpDownBase 既有扩展点；DomainUpDown 也不存在。

源码定位：`Lumina.Forms/UpDownBase.cs`（尚不存在）；共同问题见 [Control](../../../Lumina.Forms/Control.cs) 和 [Win32 interop](../../../Lumina.Forms/Win32.cs)。先决依赖：ContainerControl、Control 的真实父子结构、焦点/验证、复合原生子控件及布局。

公开成员（含继承的全部属性、事件和方法）逐项见 [UpDownBase API 清单](../api/UpDownBase.md)；共同行为、AOT、设计器与计数方法见 [基础任务](../foundations.md)。本表列出具体实现/验收动作，API 清单中余下成员也必须逐条关闭，不能把表外成员当作已支持。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 先修层级/阻塞，P1 完成常用契约，P2 补边界/高级场景，P3 完成平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| UpDownBase-001 | P0 | ⬜ | 恢复 abstract UpDownBase : ContainerControl；标准 UpButton/DownButton 抽象方法及 UpdateEditText/ValidateEditText 等受保护契约，NumericUpDown/DomainUpDown 按基线派生。 |
| UpDownBase-002 | P1 | ⬜ | Text、ReadOnly=false、InterceptArrowKeys=true、TextAlign=Left、UpDownAlign=Right、BorderStyle=Fixed3D；默认值在真实 WinForms 捕获，创建后设置均真实生效。 |
| UpDownBase-003 | P1 | ⬜ | UpButton/DownButton 的鼠标、长按、方向键、滚轮、公开调用统一语义；ReadOnly 限制编辑但不误禁用步进，禁用整体控件才拒绝动作。 |
| UpDownBase-004 | P1 | ⬜ | Select(start,length)、Focused、Tab导航、内部文本与按钮切换；托管只表现一个复合控件，Enter/Leave/Validating/Validated 不因内部焦点移动重复。 |
| UpDownBase-005 | P1 | ⬜ | ChangingText、UserEdit、ValidateEditText、UpdateEditText：区分用户输入和格式化回写，避免 TextChanged 递归，失焦前按基线提交或恢复文本。 |
| UpDownBase-006 | P1 | ⬜ | TextChanged、KeyDown/KeyPress/KeyUp、MouseDown/Up 与复合 MouseEnter/Hover/Leave/Move 的 sender、参数和一次触发原则，取消键和验证失败的结果逐项比较。 |
| UpDownBase-007 | P1 | ⬜ | AutoSize、PreferredHeight、MinimumSize/MaximumSize、Font、DPI、RTL 与上下按钮宽度；复合客户端区域不把编辑文字挡住。 |
| UpDownBase-008 | P2 | ⬜ | ContextMenuStrip、BackColor/ForeColor、边框/主题动态变更；标准隐藏 AutoScroll/DockPadding 等继承面按元数据保留，不自创滚动语义。 |
| UpDownBase-009 | P2 | ⬜ | 反复建/销毁句柄时清理 buddy关系、定时重复、field editor 与事件回调；Dispose 子控件一次，外部 Font/菜单所有权按基线。 |
| UpDownBase-010 | P3 | ⬜ | Designer 初始化复合控件无需消费端 interop；AOT 下子控件、长按回调、输入法和三平台 spin-button 可访问性验证。 |
| UpDownBase-CLOSE | P3 | ⬜ | 对照 API 清单逐个关闭剩余属性、事件、方法/重载与受保护兼容点；属性写入后读回、通知顺序/取消/重入、默认值、异常、未建句柄/重建/释放后行为分别有证据。Windows 与两原生平台各出属性/事件/行为分子分母，未验收项不能标🧪 验收通过。 |

## 三平台的原生实现评估

**Windows**：原生 EDIT 配合 msctls_updown32、UDM_SETBUDDY 与 UDN_DELTAPOS；UpDownBase 本身为复合控件，托管模型决定数值或条目，不能仅用原生32位整数数值覆盖 NumericUpDown.decimal。长按重复、方向键、滚轮在同一动作路径执行。

**Linux / GTK4**：数值型优先 GtkSpinButton，领域条目可用 SpinButton 的 input/output 映射或原生 GtkEntry+按钮组合；上下按钮不是公开的新控件概念。默认 double 调整器不是 decimal 真值，派生类型自行保留精确托管状态。

**macOS / AppKit**：NSTextField+NSStepper 为原生组合，按 UpDownAlign 排列；target/action、field editor 与复合 Focused 同步。NSStepper 的 double 值不能替代 decimal 状态，步进动作交由派生 UpButton/DownButton。

跨平台工作排在 Windows 行为基线与该类最小原生试验之后。先复用同一公共 WinForms 类型与状态模型，在库内封装平台差异；本路线不要求新增平台项目或公开抽象。每平台至少覆盖：默认值/边界，代码与用户修改双向同步，事件顺序/取消/重入，键盘/输入法（适用时），DPI/RTL/本地化，无障碍，创建/重建/释放，AOT 实际运行；纯编译或仅显示窗口不足以通过。

验收口径：基线固定为 .NET 10.0.11 WinForms。Windows 的公开属性、公开事件、行为场景分别 ≥98%；Linux/macOS 在逐项确认的可移植分母中分别 ≥98%。每个重载、访问器、事件参数与行为场景独立登记，继承成员不遗漏；签名同名、字段存值、任务写完均不能作为通过分子。原生外观差异与 Windows 专属成员保留在清单中，不能用“平台不可等价”自动划掉普通业务需求。当前各项尚无完整对照证据，不发布已达标百分比。

## 内部嵌套宿主的任务归属

固定引用程序集还包含非公开 `System.Windows.Forms.UpDownBase+UpDownEdit : TextBox` 和 `System.Windows.Forms.UpDownBase+UpDownButtons : Control`。它们不是额外公开控件，不增加 API 支持率分母，也不要求照抄其私有类型结构。前者的输入/选择/验证归 UpDownBase-002/004/005/006，后者的步进/长按/取消/释放归 UpDownBase-003/009；Windows 原生 EDIT+UPDOWN、GTK/AppKit 原生组合均须通过同样行为。阶段为 P1，依赖本页 P0 继承修正。

## 修改历史

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 尚无该类型实现文件的提交历史；不将其他控件或本轮规划计为已实现。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

## 官方依据

- [WinForms UpDownBase API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.updownbase?view=windowsdesktop-10.0)，精确签名以本机 10.0.11 引用程序集生成的 API 清单为准。
- [Windows 原生控件参考](https://learn.microsoft.com/windows/win32/controls/up-down-controls)。
- [GTK4 SpinButton](https://docs.gtk.org/gtk4/class.SpinButton.html)。
- [AppKit nsstepper](https://developer.apple.com/documentation/appkit/nsstepper)。
