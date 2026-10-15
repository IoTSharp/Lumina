# TrackBar

基线：2026-10-15 · .NET 10.0.11。目标继承：`TrackBar : Control`。本页是实施与验收任务，未声称已完成兼容。

## 当前证据与依赖

⬜ 无 Lumina.Forms/TrackBar.cs；音量/范围等常见 WinForms 滑块场景尚缺，不能由 ProgressBar 或 Splitter 替代。

源码定位：`Lumina.Forms/TrackBar.cs`（尚不存在）；共同问题见 [Control](../../../Lumina.Forms/Control.cs) 和 [Win32 interop](../../../Lumina.Forms/Win32.cs)。先决依赖：Control WM_HSCROLL/WM_VSCROLL 路由、Orientation/TickStyle、ISupportInitialize。

公开成员（含继承的全部属性、事件和方法）逐项见 [TrackBar API 清单](../api/TrackBar.md)；共同行为、AOT、设计器与计数方法见 [基础任务](../foundations.md)。本表列出具体实现/验收动作，API 清单中余下成员也必须逐条关闭，不能把表外成员当作已支持。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 先修层级/阻塞，P1 完成常用契约，P2 补边界/高级场景，P3 完成平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| TrackBar-001 | P0 | ⬜ | 实现 TrackBar : Control, ISupportInitialize；标准 Orientation/TickStyle、Value/Range/Step成员和 Scroll/ValueChanged，不暴露自定义 slider API。 |
| TrackBar-002 | P1 | ⬜ | Minimum=0、Maximum=10、Value=0、SetRange；修改边界与Value越界、负数范围、min=max、BeginInit期间初始化和EndInit收敛。 |
| TrackBar-003 | P1 | ⬜ | SmallChange=1、LargeChange=5；方向/PageUp/PageDown/Home/End/滚轮/点击轨道/拖动拇指与程序赋值逐种对照，不能把所有输入当固定1。 |
| TrackBar-004 | P1 | ⬜ | Scroll 与 ValueChanged：拖动连续/结束、键盘、鼠标滚轮、Value赋同值、范围钳制分别记录事件顺序；程序赋值是否触发Scroll以基线为准。 |
| TrackBar-005 | P1 | ⬜ | TickFrequency=1、TickStyle=BottomRight，None/TopLeft/BottomRight/Both；频率非法值、极大范围密集刻度、端点刻度与样式切换。 |
| TrackBar-006 | P1 | ⬜ | Orientation=Horizontal、AutoSize=true、RightToLeftLayout 与 RightToLeftLayoutChanged；垂直尺寸交换、方向反转、字体/DPI和布局后的有效轨道。 |
| TrackBar-007 | P2 | ⬜ | Enabled/Visible、键盘焦点、鼠标捕获拖出/取消、窗体隐藏/Dispose中断；最后Value与事件不丢失。 |
| TrackBar-008 | P2 | ⬜ | WinForms 隐藏 Text/Font/Paint/Click 等成员和可访问 range 值/增减动作；不要因继承Control而开放基线没有的行为。 |
| TrackBar-009 | P2 | ⬜ | 句柄重建保存范围/值/刻度/方向，父容器 reparent 后原生通知仍返回正确对象，Dispose 取消原生回调。 |
| TrackBar-010 | P3 | ⬜ | Designer BeginInit/EndInit、静态范围/刻度/RTL与AOT交互；Linux/macOS 原生刻度映射各自有截图和事件记录。 |
| TrackBar-CLOSE | P3 | ⬜ | 对照 API 清单逐个关闭剩余属性、事件、方法/重载与受保护兼容点；属性写入后读回、通知顺序/取消/重入、默认值、异常、未建句柄/重建/释放后行为分别有证据。Windows 与两原生平台各出属性/事件/行为分子分母，未验收项不能标🧪 验收通过。 |

## 三平台的原生实现评估

**Windows**：msctls_trackbar32，TBM_SET/GETRANGE、SETPOS、SETLINESIZE/SETPAGESIZE、SETTICFREQ、GETTHUMBRECT；WM_HSCROLL/WM_VSCROLL 按 TB_*通知区分鼠标拖动、键盘/滚轮与程序赋值，32位范围不从16位通知高字取真值。

**Linux / GTK4**：GtkScale+GtkAdjustment 原生滑块，add_mark 对应刻度、orientation/inverted/step/page_increment 映射；WinForms TickStyle 双侧、TickFrequency 整数与原生标记密度需验证，不同平台主题最小尺寸单列。

**macOS / AppKit**：NSSlider 原生线性滑块，min/max/integerValue、numberOfTickMarks、tickMarkPosition、continuous action；原生双侧刻度与 Windows 方向/RTL不完全同义，无法匹配的外观列差异，整数托管模型避免double取整漂移。

跨平台工作排在 Windows 行为基线与该类最小原生试验之后。先复用同一公共 WinForms 类型与状态模型，在库内封装平台差异；本路线不要求新增平台项目或公开抽象。每平台至少覆盖：默认值/边界，代码与用户修改双向同步，事件顺序/取消/重入，键盘/输入法（适用时），DPI/RTL/本地化，无障碍，创建/重建/释放，AOT 实际运行；纯编译或仅显示窗口不足以通过。

验收口径：基线固定为 .NET 10.0.11 WinForms。Windows 的公开属性、公开事件、行为场景分别 ≥98%；Linux/macOS 在逐项确认的可移植分母中分别 ≥98%。每个重载、访问器、事件参数与行为场景独立登记，继承成员不遗漏；签名同名、字段存值、任务写完均不能作为通过分子。原生外观差异与 Windows 专属成员保留在清单中，不能用“平台不可等价”自动划掉普通业务需求。当前各项尚无完整对照证据，不发布已达标百分比。

## 修改历史

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 尚无该类型实现文件的提交历史；不将其他控件或本轮规划计为已实现。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

## 官方依据

- [WinForms TrackBar API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.trackbar?view=windowsdesktop-10.0)，精确签名以本机 10.0.11 引用程序集生成的 API 清单为准。
- [Windows 原生控件参考](https://learn.microsoft.com/windows/win32/controls/trackbar-controls)。
- [GTK4 Scale](https://docs.gtk.org/gtk4/class.Scale.html)。
- [AppKit nsslider](https://developer.apple.com/documentation/appkit/nsslider)。
