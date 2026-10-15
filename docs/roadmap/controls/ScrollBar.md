# ScrollBar

基线：2026-10-15 · .NET 10.0.11。目标继承：`ScrollBar : Control`。本页是实施与验收任务，未声称已完成兼容。

## 当前证据与依赖

⬜ 无 Lumina.Forms/ScrollBar.cs。容器内部滚动条或 TextBox 原生滚动条不等于公开 ScrollBar 控件；HScrollBar/VScrollBar 也不存在。

源码定位：`Lumina.Forms/ScrollBar.cs`（尚不存在）；共同问题见 [Control](../../../Lumina.Forms/Control.cs) 和 [Win32 interop](../../../Lumina.Forms/Win32.cs)。先决依赖：Control 滚动消息路由、ScrollEventArgs/ScrollEventType/ScrollOrientation、方向派生类。

公开成员（含继承的全部属性、事件和方法）逐项见 [ScrollBar API 清单](../api/ScrollBar.md)；共同行为、AOT、设计器与计数方法见 [基础任务](../foundations.md)。本表列出具体实现/验收动作，API 清单中余下成员也必须逐条关闭，不能把表外成员当作已支持。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 先修层级/阻塞，P1 完成常用契约，P2 补边界/高级场景，P3 完成平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| ScrollBar-001 | P0 | ⬜ | 实现 abstract ScrollBar : Control，HScrollBar/VScrollBar 派生；ScrollEventArgs、ScrollEventType、ScrollOrientation 及受保护 OnScroll/OnValueChanged 签名准确。 |
| ScrollBar-002 | P1 | ⬜ | Minimum=0、Maximum=100、Value=0、SmallChange=1、LargeChange=10 默认；范围变更、负数、min=max、非法增量与溢出按基线。 |
| ScrollBar-003 | P1 | ⬜ | 区分 Value 可赋上限与用户可滚上限 Maximum-LargeChange+1；默认范围拖动终点91，程序Value=100的结果和随后的原生读回按WinForms实测。 |
| ScrollBar-004 | P1 | ⬜ | ScrollEventArgs.OldValue/NewValue/Type/ScrollOrientation；处理器改NewValue是否影响最终位置，line/page/thumb/first/last/EndScroll逐事件验收。 |
| ScrollBar-005 | P1 | ⬜ | Scroll 与 ValueChanged 的顺序/次数：程序写入、同值、鼠标拖动、键盘、滚轮、边界钳制；32位大范围不截断到16位。 |
| ScrollBar-006 | P1 | ⬜ | TabStop=false、键盘/鼠标捕获、禁用/隐藏、滚轮积累和系统滚动设置；非聚焦滚轮行为按各基线记录。 |
| ScrollBar-007 | P2 | ⬜ | AutoSize、ScaleScrollBarForDpiChange、方向默认厚度、Font/BackColor/ForeColor/Text 等隐藏元数据，DPI下宽高和布局不混乱。 |
| ScrollBar-008 | P2 | ⬜ | RightToLeft 行为交由派生类验证；范围、page比例、事件值在三平台换算往返无一位偏差。 |
| ScrollBar-009 | P2 | ⬜ | 原生辅助技术 scrollbar 角色、方向、min/max/current、line/page动作；Dispose释放通知/捕获，重建句柄保持范围。 |
| ScrollBar-010 | P3 | ⬜ | Designer 初始化顺序与动态范围、AOT 32位滚动值和事件参数、两方向各完成原生平台验收。 |
| ScrollBar-CLOSE | P3 | ⬜ | 对照 API 清单逐个关闭剩余属性、事件、方法/重载与受保护兼容点；属性写入后读回、通知顺序/取消/重入、默认值、异常、未建句柄/重建/释放后行为分别有证据。Windows 与两原生平台各出属性/事件/行为分子分母，未验收项不能标🧪 验收通过。 |

## 三平台的原生实现评估

**Windows**：SCROLLBAR 原生窗口，SetScrollInfo/GetScrollInfo（SIF_RANGE/PAGE/POS/TRACKPOS），WM_HSCROLL/WM_VSCROLL、SB_LINE*/PAGE*/THUMBTRACK/THUMBPOSITION/ENDSCROLL。拖动位置必须用 nTrackPos 获取32位，不能依靠16位消息高字。

**Linux / GTK4**：GtkScrollbar + GtkAdjustment（lower/upper/value/step-increment/page-increment/page-size）；WinForms 的可达末端 Maximum-LargeChange+1 与 GTK upper-page_size 有一位差，需显式换算，输入类型用原生控制器/调整动作记录。

**macOS / AppKit**：NSScroller 的 doubleValue、knobProportion、hitPart、target/action；托管范围换算为0..1，line/page/knob分辨为ScrollEventType。macOS overlay与箭头缺省风格影响原生交互，不能虚构传统Windows箭头一直存在。

跨平台工作排在 Windows 行为基线与该类最小原生试验之后。先复用同一公共 WinForms 类型与状态模型，在库内封装平台差异；本路线不要求新增平台项目或公开抽象。每平台至少覆盖：默认值/边界，代码与用户修改双向同步，事件顺序/取消/重入，键盘/输入法（适用时），DPI/RTL/本地化，无障碍，创建/重建/释放，AOT 实际运行；纯编译或仅显示窗口不足以通过。

验收口径：基线固定为 .NET 10.0.11 WinForms。Windows 的公开属性、公开事件、行为场景分别 ≥98%；Linux/macOS 在逐项确认的可移植分母中分别 ≥98%。每个重载、访问器、事件参数与行为场景独立登记，继承成员不遗漏；签名同名、字段存值、任务写完均不能作为通过分子。原生外观差异与 Windows 专属成员保留在清单中，不能用“平台不可等价”自动划掉普通业务需求。当前各项尚无完整对照证据，不发布已达标百分比。

## 修改历史

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 尚无该类型实现文件的提交历史；不将其他控件或本轮规划计为已实现。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

## 官方依据

- [WinForms ScrollBar API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.scrollbar?view=windowsdesktop-10.0)，精确签名以本机 10.0.11 引用程序集生成的 API 清单为准。
- [Windows 原生控件参考](https://learn.microsoft.com/windows/win32/controls/scroll-bars)。
- [GTK4 Scrollbar](https://docs.gtk.org/gtk4/class.Scrollbar.html)。
- [AppKit nsscroller](https://developer.apple.com/documentation/appkit/nsscroller)。
