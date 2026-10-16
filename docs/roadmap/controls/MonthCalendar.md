# MonthCalendar

基线：2026-10-15 · .NET 10.0.11。目标继承：`MonthCalendar : Control`。本页是实施与验收任务，未声称已完成兼容。

## 当前证据与依赖

🟡 [MonthCalendar.cs](../../../Lumina.Forms/MonthCalendar.cs) 已创建 SysMonthCal32、MCM_GETMINREQRECT 调整最小尺寸并设置颜色；尚无 SelectionStart/End/Range、DateChanged/DateSelected、范围和粗体日期数据模型。

源码定位：[Lumina.Forms/MonthCalendar.cs](../../../Lumina.Forms/MonthCalendar.cs)；共同问题见 [Control](../../../Lumina.Forms/Control.cs) 和 [Win32 interop](../../../Lumina.Forms/Win32.cs)。先决依赖：Control 通知和布局、SelectionRange/DateRangeEventArgs/Day 等标准关联类型。

公开成员（含继承的全部属性、事件和方法）逐项见 [MonthCalendar API 清单](../api/MonthCalendar.md)；共同行为、AOT、设计器与计数方法见 [基础任务](../foundations.md)。本表列出具体实现/验收动作，API 清单中余下成员也必须逐条关闭，不能把表外成员当作已支持。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 先修层级/阻塞，P1 完成常用契约，P2 补边界/高级场景，P3 完成平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| MonthCalendar-001 | P0 | ⬜ | 补 SelectionStart、SelectionEnd、SelectionRange、SelectionRange 类型与 DateRangeEventArgs；托管↔原生日历同步，创建前赋值和创建后用户选择都能读回。 |
| MonthCalendar-002 | P1 | ⬜ | MinDate/MaxDate、MaxSelectionCount 默认7与 SelectionRange；跨月/跨年、单日/连续多日、反向拖选、范围越界与边界改变后的钳制/异常对照基线。 |
| MonthCalendar-003 | P1 | ⬜ | DateChanged 与 DateSelected：键盘移动、鼠标完成选择、程序 SetDate/SetSelectionRange、选择同一天的通知顺序/次数及事件参数 Start/End。 |
| MonthCalendar-004 | P1 | ⬜ | BoldedDates、AnnuallyBoldedDates、MonthlyBoldedDates；Add/Remove/RemoveAll 各系列方法与 UpdateBoldedDates；闰年、重复日、移除和可视月份变化正确刷新。 |
| MonthCalendar-005 | P1 | ⬜ | TodayDate/TodayDateSet、ShowToday=true、ShowTodayCircle=true、ShowWeekNumbers=false、FirstDayOfWeek=Default；今天变化、文化周起点和周数规则。 |
| MonthCalendar-006 | P1 | ⬜ | CalendarDimensions、SingleMonthSize、GetPreferredSize、GetDisplayRange(bool)、ScrollChange；多月排列、最大尺寸/月份数限制、DPI和父约束分别捕获基线。 |
| MonthCalendar-007 | P2 | ⬜ | HitTest 各重载和 HitTestInfo/HitArea、Title/Trailing日期、周号、空白/导航按钮；坐标、日期与区域不能只返回当前选择。 |
| MonthCalendar-008 | P2 | ⬜ | TitleBackColor/ForeColor、TrailingForeColor、BackColor/ForeColor、RightToLeftLayout/RightToLeftLayoutChanged；当前主题覆盖用户颜色的行为需对照，原生主题忽略颜色时明确差异。 |
| MonthCalendar-009 | P2 | ⬜ | 键盘方向/PageUp/Down/Shift范围、滚轮、禁用/焦点、鼠标双击及隐藏成员的实际触发；辅助技术每个日期和范围状态。 |
| MonthCalendar-010 | P2 | ⬜ | Linux/macOS 原生可行性先验收：多日范围、多月、粗体、周数分别标可映射/需原生组合/无法等价；这些仍是需求，不因困难自动从可移植分母删除。 |
| MonthCalendar-011 | P3 | ⬜ | Designer 日期数组/SelectionRange/.resx、AOT DAYSTATE内存与通知生命周期，Dispose 弹出/定时刷新和原生回调资源。 |
| MonthCalendar-CLOSE | P3 | ⬜ | 对照 API 清单逐个关闭剩余属性、事件、方法/重载与受保护兼容点；属性写入后读回、通知顺序/取消/重入、默认值、异常、未建句柄/重建/释放后行为分别有证据。Windows 与两原生平台各出属性/事件/行为分子分母，未验收项不能标🧪 验收通过。 |

## 三平台的原生实现评估

**Windows**：SysMonthCal32，MCM_SET/GETSELRANGE、MCM_SET/GETCURSEL、MCM_SET/GETRANGE、MCM_GETMONTHRANGE、MCM_SETDAYSTATE、MCM_GETMINREQRECT；MCN_SELCHANGE/SELECT/GETDAYSTATE 映射事件/粗体，需区分用户选中与日期范围改变。

**Linux / GTK4**：GtkCalendar 原生月历支持单日选择与 mark_day 等；WinForms 连续多日选择、多月布局、最多选择数和年度/月度粗体规则没有完整单控件同义映射。可原生多 GtkCalendar 组合并协调状态，但原生控件不支持的范围高亮/日粒度外观列真实缺口，不能用自绘格子记为原生完成。

**macOS / AppKit**：NSDatePicker 的 clockAndCalendar/图形日期样式可承载单月选择；是否支持所需范围选择需在目标 macOS 版本实测。任意多月、粗体指定日、周数/今日圈/尾部日期颜色没有完整 AppKit 公共接口对应；作为原生目标的关键可行性门槛，不能承诺全部可移植能力已达标。

跨平台工作排在 Windows 行为基线与该类最小原生试验之后。先复用同一公共 WinForms 类型与状态模型，在库内封装平台差异；本路线不要求新增平台项目或公开抽象。每平台至少覆盖：默认值/边界，代码与用户修改双向同步，事件顺序/取消/重入，键盘/输入法（适用时），DPI/RTL/本地化，无障碍，创建/重建/释放，AOT 实际运行；纯编译或仅显示窗口不足以通过。

验收口径：基线固定为 .NET 10.0.11 WinForms。Windows 的公开属性、公开事件、行为场景分别 ≥98%；Linux/macOS 在逐项确认的可移植分母中分别 ≥98%。每个重载、访问器、事件参数与行为场景独立登记，继承成员不遗漏；签名同名、字段存值、任务写完均不能作为通过分子。原生外观差异与 Windows 专属成员保留在清单中，不能用“平台不可等价”自动划掉普通业务需求。当前各项尚无完整对照证据，不发布已达标百分比。

## 修改历史

- 2026-10-16 · 批次023：基础Control启用标准UserPaint后，本控件构造显式保留UserPaint=false，使现有系统窗口类过程及原生字体/绘制继续生效。仅属消息与绘制基础的必要衔接，不代表本控件完整Styles、Paint事件或视觉/输入兼容完成，见[023](../batches/023.md)。

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 2026-04-08 · `7d7a67e` · Add initial implementation of Lumina.Forms library。
- 2026-04-08 · `2545995` · feat: 增强深色模式支持，优化多个控件的主题应用逻辑。
- 2026-04-08 · `108ef2b` · feat: 添加对 Win32 黑暗模式的支持，重构多个控件以优化主题应用逻辑。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

## 官方依据

- [WinForms MonthCalendar API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.monthcalendar?view=windowsdesktop-10.0)，精确签名以本机 10.0.11 引用程序集生成的 API 清单为准。
- [Windows 原生控件参考](https://learn.microsoft.com/windows/win32/controls/month-calendar-controls)。
- [GTK4 Calendar](https://docs.gtk.org/gtk4/class.Calendar.html)。
- [AppKit nsdatepicker](https://developer.apple.com/documentation/appkit/nsdatepicker)。
