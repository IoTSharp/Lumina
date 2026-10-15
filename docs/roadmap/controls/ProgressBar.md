# ProgressBar

基线：2026-10-15 · .NET 10.0.11。目标继承：`ProgressBar : Control`。本页是实施与验收任务，未声称已完成兼容。

## 当前证据与依赖

🟡 [ProgressBar.cs](../../../Lumina.Forms/ProgressBar.cs) 有 msctls_progress32 和 Value→PBM_SETPOS，但只下界 Math.Max(0,value)，缺 Minimum/Maximum/Step/Style 等；不能验证范围、动画或最大值行为。

源码定位：[Lumina.Forms/ProgressBar.cs](../../../Lumina.Forms/ProgressBar.cs)；共同问题见 [Control](../../../Lumina.Forms/Control.cs) 和 [Win32 interop](../../../Lumina.Forms/Win32.cs)。先决依赖：Control 原生创建/样式、ProgressBarStyle 与系统主题；不依赖数据绑定即可完成首轮。

公开成员（含继承的全部属性、事件和方法）逐项见 [ProgressBar API 清单](../api/ProgressBar.md)；共同行为、AOT、设计器与计数方法见 [基础任务](../foundations.md)。本表列出具体实现/验收动作，API 清单中余下成员也必须逐条关闭，不能把表外成员当作已支持。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 先修层级/阻塞，P1 完成常用契约，P2 补边界/高级场景，P3 完成平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| ProgressBar-001 | P0 | 🟡 | Minimum=0、Maximum=100、Value=0；范围颠倒、值越界和范围变更的异常/钳制对照 WinForms，修复当前仅下界截断的不同语义。 |
| ProgressBar-002 | P1 | ⬜ | Step=10、Increment(int)、PerformStep；正/负增量、跨端点、极端整数、同值操作和 Marquee 下调用限制逐项验收。 |
| ProgressBar-003 | P1 | ⬜ | Style 默认 Blocks，Blocks/Continuous/Marquee 切换真实改变宿主，重建句柄保存范围和值；主题下 Continuous 是否可见以基线为准。 |
| ProgressBar-004 | P1 | ⬜ | MarqueeAnimationSpeed 默认100、0停止/恢复、禁用/隐藏/移除父容器与 Dispose 后不残留动画定时器，UI线程更新。 |
| ProgressBar-005 | P1 | ⬜ | RightToLeftLayout/RightToLeftLayoutChanged、RTL、垂直并非公开 Orientation 不自创；进度方向与当前系统主题匹配基线。 |
| ProgressBar-006 | P2 | ⬜ | BackColor/ForeColor/Font/Text、隐藏事件和属性元数据；原生主题忽略颜色时不把字段存储算视觉支持，不增加基线不存在的 ValueChanged 事件。 |
| ProgressBar-007 | P2 | ⬜ | TabStop 默认 false、键盘/Enter/Leave/Click/Paint 等隐藏成员的行为；进度条不意外占用输入焦点或触发验证。 |
| ProgressBar-008 | P2 | ⬜ | AutoSize/GetPreferredSize/默认尺寸、DPI、容器缩放、100%边界和 min=max 范围，三平台进度分数与可访问值一致。 |
| ProgressBar-009 | P2 | ⬜ | UIA/AT-SPI/NSAccessibility range/progress 角色、Indeterminate 状态与 Value 更新；外部模型高频更新合并不丢最终值。 |
| ProgressBar-010 | P3 | ⬜ | Designer 初值/Style/Marquee 顺序、AOT 动画回调和句柄重建、三平台原生动画/资源释放真实运行。 |
| ProgressBar-CLOSE | P3 | ⬜ | 对照 API 清单逐个关闭剩余属性、事件、方法/重载与受保护兼容点；属性写入后读回、通知顺序/取消/重入、默认值、异常、未建句柄/重建/释放后行为分别有证据。Windows 与两原生平台各出属性/事件/行为分子分母，未验收项不能标🧪 验收通过。 |

## 三平台的原生实现评估

**Windows**：msctls_progress32；PBM_SETRANGE32/SETPOS/SETSTEP/STEPIT/SETMARQUEE，PBS_SMOOTH/PBS_MARQUEE/RTL样式。主题可能忽略颜色与连续样式，应把 WinForms 在同主题下的实际行为作为基线。

**Linux / GTK4**：GtkProgressBar 的 fraction 对应 (Value-Minimum)/(Maximum-Minimum)，pulse 对应 Marquee，GLib主循环定时更新；Blocks 与 Continuous 在原生 GTK 没有相同分块像素语义，公共进度/动画与 Windows专属外观分别记录。

**macOS / AppKit**：NSProgressIndicator 的 minValue/maxValue/doubleValue、indeterminate、startAnimation/stopAnimation；样式限定 bar。MarqueeAnimationSpeed 与系统动画速度没有一一对应控制，需记录可实现程度，不能用重绘替代原生动画而不说明。

跨平台工作排在 Windows 行为基线与该类最小原生试验之后。先复用同一公共 WinForms 类型与状态模型，在库内封装平台差异；本路线不要求新增平台项目或公开抽象。每平台至少覆盖：默认值/边界，代码与用户修改双向同步，事件顺序/取消/重入，键盘/输入法（适用时），DPI/RTL/本地化，无障碍，创建/重建/释放，AOT 实际运行；纯编译或仅显示窗口不足以通过。

验收口径：基线固定为 .NET 10.0.11 WinForms。Windows 的公开属性、公开事件、行为场景分别 ≥98%；Linux/macOS 在逐项确认的可移植分母中分别 ≥98%。每个重载、访问器、事件参数与行为场景独立登记，继承成员不遗漏；签名同名、字段存值、任务写完均不能作为通过分子。原生外观差异与 Windows 专属成员保留在清单中，不能用“平台不可等价”自动划掉普通业务需求。当前各项尚无完整对照证据，不发布已达标百分比。

## 修改历史

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 2026-04-08 · `108ef2b` · feat: 添加对 Win32 黑暗模式的支持，重构多个控件以优化主题应用逻辑。
- 2026-03-31 · `ce86ab5` · 已经把 Lumina.Forms 的主题能力接到更像 WinForms 的核心表面上了。现在 Control.cs 和 Form.cs 提供了 BackColor / ForeColor、对应变更事件、主题槽位解析、父容器背景继承，以及打开窗口后的实时主题刷新；高频控件也统一改成走 ApplyExp…（原提交标题节录）。
- 2026-03-31 · `dcdc7e9` · Add theme and control components to Lumina.Forms。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

## 官方依据

- [WinForms ProgressBar API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.progressbar?view=windowsdesktop-10.0)，精确签名以本机 10.0.11 引用程序集生成的 API 清单为准。
- [Windows 原生控件参考](https://learn.microsoft.com/windows/win32/controls/progress-bar-control-reference)。
- [GTK4 ProgressBar](https://docs.gtk.org/gtk4/class.ProgressBar.html)。
- [AppKit nsprogressindicator](https://developer.apple.com/documentation/appkit/nsprogressindicator)。
