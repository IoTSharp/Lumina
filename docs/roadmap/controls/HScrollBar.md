# HScrollBar

基线：2026-10-15 · .NET 10.0.11。目标继承：`HScrollBar : ScrollBar`。本页是实施与验收任务，未声称已完成兼容。

## 当前证据与依赖

⬜ 无 Lumina.Forms/HScrollBar.cs；完整 WinForms 对标需独立横向滚动条，不能只依靠 Panel.AutoScroll。

源码定位：`Lumina.Forms/HScrollBar.cs`（尚不存在）；共同问题见 [Control](../../../Lumina.Forms/Control.cs) 和 [Win32 interop](../../../Lumina.Forms/Win32.cs)。先决依赖：ScrollBar 的范围模型与事件、父窗口 WM_HSCROLL 路由。

公开成员（含继承的全部属性、事件和方法）逐项见 [HScrollBar API 清单](../api/HScrollBar.md)；共同行为、AOT、设计器与计数方法见 [基础任务](../foundations.md)。本表列出具体实现/验收动作，API 清单中余下成员也必须逐条关闭，不能把表外成员当作已支持。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 先修层级/阻塞，P1 完成常用契约，P2 补边界/高级场景，P3 完成平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| HScrollBar-001 | P0 | ⬜ | 实现 HScrollBar : ScrollBar，继承完整范围/事件/方法；核对 DefaultSize/CreateParams 受保护成员，不复制另一套范围算法。 |
| HScrollBar-002 | P1 | ⬜ | Minimum/Maximum/Value/SmallChange/LargeChange 默认与共享基类相同；横向默认可达末端、min=max和大范围32位。 |
| HScrollBar-003 | P1 | ⬜ | Left/Right、PageUp/PageDown、Home/End、鼠标line/page/thumb、水平滚轮（按基线）结果与 ScrollEventType 精确对应。 |
| HScrollBar-004 | P1 | ⬜ | ScrollEventArgs.ScrollOrientation=HorizontalScroll；程序Value/用户输入的Scroll/ValueChanged顺序和次数、NewValue改写。 |
| HScrollBar-005 | P1 | ⬜ | RightToLeft、父容器RightToLeftLayout、系统镜像样式；显示方向、按键方向与Value增长方向逐组合验收。 |
| HScrollBar-006 | P2 | ⬜ | 横向布局厚度、Dock.Top/Bottom、AutoSize、DPI缩放、MinimumSize/MaximumSize，宽度极小时按钮/拇指行为。 |
| HScrollBar-007 | P2 | ⬜ | 拖动时窗口隐藏/禁用/reparent/Dispose、鼠标捕获释放与EndScroll，句柄重建不丢Value/范围。 |
| HScrollBar-008 | P2 | ⬜ | TabStop默认false，辅助技术横向scrollbar角色及range、页大小与动作；隐藏键盘/鼠标事件元数据按基线。 |
| HScrollBar-009 | P3 | ⬜ | Designer静态横向控件布局与AOT实际拖动；GTK/AppKit比例映射、RTL和默认可达末端保留独立记录。 |
| HScrollBar-CLOSE | P3 | ⬜ | 对照 API 清单逐个关闭剩余属性、事件、方法/重载与受保护兼容点；属性写入后读回、通知顺序/取消/重入、默认值、异常、未建句柄/重建/释放后行为分别有证据。Windows 与两原生平台各出属性/事件/行为分子分母，未验收项不能标🧪 验收通过。 |

## 三平台的原生实现评估

**Windows**：SCROLLBAR + SBS_HORZ；父窗口 WM_HSCROLL 路由到此对象，GetScrollInfo 取完整位置。RTL 布局与系统滚动条风格分开验证。

**Linux / GTK4**：GtkScrollbar horizontal + 共享 GtkAdjustment；反转和RTL行为按托管WinForms定义映射；主题原生厚度不同，不能固定Windows像素高度。

**macOS / AppKit**：横向 NSScroller，0..1位置与knob比例换算；原生overlay/line/page hitPart行为影响交互，逐输入记录。

跨平台工作排在 Windows 行为基线与该类最小原生试验之后。先复用同一公共 WinForms 类型与状态模型，在库内封装平台差异；本路线不要求新增平台项目或公开抽象。每平台至少覆盖：默认值/边界，代码与用户修改双向同步，事件顺序/取消/重入，键盘/输入法（适用时），DPI/RTL/本地化，无障碍，创建/重建/释放，AOT 实际运行；纯编译或仅显示窗口不足以通过。

验收口径：基线固定为 .NET 10.0.11 WinForms。Windows 的公开属性、公开事件、行为场景分别 ≥98%；Linux/macOS 在逐项确认的可移植分母中分别 ≥98%。每个重载、访问器、事件参数与行为场景独立登记，继承成员不遗漏；签名同名、字段存值、任务写完均不能作为通过分子。原生外观差异与 Windows 专属成员保留在清单中，不能用“平台不可等价”自动划掉普通业务需求。当前各项尚无完整对照证据，不发布已达标百分比。

## 修改历史

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 尚无该类型实现文件的提交历史；不将其他控件或本轮规划计为已实现。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

## 官方依据

- [WinForms HScrollBar API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.hscrollbar?view=windowsdesktop-10.0)，精确签名以本机 10.0.11 引用程序集生成的 API 清单为准。
- [Windows 原生控件参考](https://learn.microsoft.com/windows/win32/controls/scroll-bars)。
- [GTK4 Scrollbar](https://docs.gtk.org/gtk4/class.Scrollbar.html)。
- [AppKit nsscroller](https://developer.apple.com/documentation/appkit/nsscroller)。
