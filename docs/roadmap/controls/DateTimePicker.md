# DateTimePicker

基线：2026-10-15 · .NET 10.0.11。目标继承：`DateTimePicker : Control`。本页是实施与验收任务，未声称已完成兼容。

## 当前证据与依赖

🟡 [DateTimePicker.cs](../../../Lumina.Forms/DateTimePicker.cs) 已创建 SysDateTimePick32 并设置日历主题颜色，但 Value 仅 DateTime 字段，未发 DTM_SETSYSTEMTIME、未接 DTN_DATETIMECHANGE；Format/CustomFormat/范围/事件缺失。

源码定位：[Lumina.Forms/DateTimePicker.cs](../../../Lumina.Forms/DateTimePicker.cs)；共同问题见 [Control](../../../Lumina.Forms/Control.cs) 和 [Win32 interop](../../../Lumina.Forms/Win32.cs)。先决依赖：Control 通知路由、SYSTEMTIME/原生通知结构、日期文化和时间边界。

公开成员（含继承的全部属性、事件和方法）逐项见 [DateTimePicker API 清单](../api/DateTimePicker.md)；共同行为、AOT、设计器与计数方法见 [基础任务](../foundations.md)。本表列出具体实现/验收动作，API 清单中余下成员也必须逐条关闭，不能把表外成员当作已支持。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 先修层级/阻塞，P1 完成常用契约，P2 补边界/高级场景，P3 完成平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| DateTimePicker-001 | P0 | 🟡 | Value 建立托管↔原生双向同步；创建前赋值、创建后赋值、用户选日/改时间、句柄重建都保持 DateTime，原生修改不能只停留在界面。 |
| DateTimePicker-002 | P1 | ⬜ | ValueChanged、TextChanged、DropDown、CloseUp、FormatChanged、RightToLeftLayoutChanged 的次数/顺序；程序同值赋入和弹出后取消不得伪造日期变更。 |
| DateTimePicker-003 | P1 | ⬜ | MinDate/MaxDate、MinimumDateTime/MaximumDateTime 静态范围；以真实 .NET10 与当前文化捕获默认值，跨界/颠倒边界/DateTime.Kind/毫秒精度与异常逐项验收。 |
| DateTimePicker-004 | P1 | ⬜ | Format 默认 Long、CustomFormat、Text 解析/格式化；Short/Long/Time/Custom、引号转义、文化切换、无效文本和 AM/PM 与24小时。 |
| DateTimePicker-005 | P1 | ⬜ | ShowCheckBox=false、Checked=true 默认及未选中态；Value 与 Text/Checked 互相影响，键盘切换与代码设置按基线，绑定 null 不自创新 API。 |
| DateTimePicker-006 | P1 | ⬜ | ShowUpDown=false、DropDownAlign=Left、PreferredHeight；切换模式与句柄重建保存值/范围/选择，键盘方向/滚轮/Enter/Escape 不重复提交。 |
| DateTimePicker-007 | P2 | ⬜ | CalendarFont、CalendarForeColor、CalendarMonthBackground、CalendarTitleBackColor/ForeColor、CalendarTrailingForeColor；系统主题限制逐项记录，不把不可见存值算实现。 |
| DateTimePicker-008 | P2 | ⬜ | RightToLeftLayout、RTL、自定义历法和本地化；Linux/macOS日期/时间存储转换避免时区/DST导致选中日期漂移。 |
| DateTimePicker-009 | P2 | ⬜ | WinForms 隐藏 BackColor/ForeColor/Click/Paint 等成员元数据与实际行为；禁用、只读缺少的成员不自创，辅助技术 date/time各字段可编辑和取值。 |
| DateTimePicker-010 | P3 | ⬜ | Designer Value/Format/MinDate/MaxDate/.resx 初始化、AOT 原生通知结构与回调、弹出窗口销毁及 Dispose；三平台按可移植场景独立验收。 |
| DateTimePicker-CLOSE | P3 | ⬜ | 对照 API 清单逐个关闭剩余属性、事件、方法/重载与受保护兼容点；属性写入后读回、通知顺序/取消/重入、默认值、异常、未建句柄/重建/释放后行为分别有证据。Windows 与两原生平台各出属性/事件/行为分子分母，未验收项不能标🧪 验收通过。 |

## 三平台的原生实现评估

**Windows**：SysDateTimePick32；DTM_SET/GETSYSTEMTIME、DTM_SET/GETRANGE、DTM_SETFORMAT、DTM_SETMCFONT 与 DTN_DATETIMECHANGE/DROPDOWN/CLOSEUP/USERSTRING。ShowCheckBox/ShowUpDown/RightToLeftLayout 涉及风格和重建；原生弹出日历颜色/字体需句柄后更新。

**Linux / GTK4**：GTK4 没有与 WinForms DateTimePicker 全部语义一一对应的单个控件；可用 GtkEntry+GtkPopover+GtkCalendar，时间部分配 GtkSpinButton，保持全部为平台原生控件。空选择复选框、上下模式、自定义格式、历法和 Min/MaxDate 需托管协调，不称之为原生单控件等价。

**macOS / AppKit**：NSDatePicker 提供原生 date/time、calendar/text-field/stepper 模式，dateValue/minDate/maxDate/locale/calendar 与委托映射。DateTime.Kind/无时区日期和 NSDate 绝对时间需明确转换；ShowCheckBox 要原生 NSButton 组合，CustomFormat 任意.NET格式不等于 datePickerElements。

跨平台工作排在 Windows 行为基线与该类最小原生试验之后。先复用同一公共 WinForms 类型与状态模型，在库内封装平台差异；本路线不要求新增平台项目或公开抽象。每平台至少覆盖：默认值/边界，代码与用户修改双向同步，事件顺序/取消/重入，键盘/输入法（适用时），DPI/RTL/本地化，无障碍，创建/重建/释放，AOT 实际运行；纯编译或仅显示窗口不足以通过。

验收口径：基线固定为 .NET 10.0.11 WinForms。Windows 的公开属性、公开事件、行为场景分别 ≥98%；Linux/macOS 在逐项确认的可移植分母中分别 ≥98%。每个重载、访问器、事件参数与行为场景独立登记，继承成员不遗漏；签名同名、字段存值、任务写完均不能作为通过分子。原生外观差异与 Windows 专属成员保留在清单中，不能用“平台不可等价”自动划掉普通业务需求。当前各项尚无完整对照证据，不发布已达标百分比。

## 修改历史

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 2026-04-08 · `2545995` · feat: 增强深色模式支持，优化多个控件的主题应用逻辑。
- 2026-04-08 · `108ef2b` · feat: 添加对 Win32 黑暗模式的支持，重构多个控件以优化主题应用逻辑。
- 2026-04-08 · `7149595` · Implement dark mode capabilities and menu rendering enhancements。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

## 官方依据

- [WinForms DateTimePicker API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.datetimepicker?view=windowsdesktop-10.0)，精确签名以本机 10.0.11 引用程序集生成的 API 清单为准。
- [Windows 原生控件参考](https://learn.microsoft.com/windows/win32/controls/date-and-time-picker-controls)。
- [GTK4 Calendar](https://docs.gtk.org/gtk4/class.Calendar.html)。
- [AppKit nsdatepicker](https://developer.apple.com/documentation/appkit/nsdatepicker)。
