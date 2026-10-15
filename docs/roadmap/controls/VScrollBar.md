# VScrollBar

基线：2026-10-15 · .NET 10.0.11。目标继承：`VScrollBar : ScrollBar`。本页是实施与验收任务，未声称已完成兼容。

## 当前证据与依赖

⬜ 无 Lumina.Forms/VScrollBar.cs；RichTextBox/ListBox的内部滚动条不能视为此公开类型已实现。

源码定位：`Lumina.Forms/VScrollBar.cs`（尚不存在）；共同问题见 [Control](../../../Lumina.Forms/Control.cs) 和 [Win32 interop](../../../Lumina.Forms/Win32.cs)。先决依赖：ScrollBar 的范围模型与事件、父窗口 WM_VSCROLL 路由。

公开成员（含继承的全部属性、事件和方法）逐项见 [VScrollBar API 清单](../api/VScrollBar.md)；共同行为、AOT、设计器与计数方法见 [基础任务](../foundations.md)。本表列出具体实现/验收动作，API 清单中余下成员也必须逐条关闭，不能把表外成员当作已支持。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 先修层级/阻塞，P1 完成常用契约，P2 补边界/高级场景，P3 完成平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| VScrollBar-001 | P0 | ⬜ | 实现 VScrollBar : ScrollBar，正确DefaultSize/CreateParams及RightToLeft覆盖；只复用共享范围逻辑，不新增Orientation公开属性。 |
| VScrollBar-002 | P1 | ⬜ | Minimum/Maximum/Value/SmallChange/LargeChange边界、默认末端和min=max；程序值和用户可达末端差异按基线。 |
| VScrollBar-003 | P1 | ⬜ | Up/Down、PageUp/PageDown、Home/End、滚轮、鼠标line/page/thumb逐动作比较；系统滚轮行数/页滚动和累计不足一格。 |
| VScrollBar-004 | P1 | ⬜ | ScrollOrientation=VerticalScroll；Scroll/ValueChanged先后、拖动持续和EndScroll、回调改NewValue的最终值。 |
| VScrollBar-005 | P1 | ⬜ | RightToLeft 与 RightToLeftChanged 的 VScrollBar 特殊行为按真实WinForms记录；RTL父容器不随意反转Value方向。 |
| VScrollBar-006 | P2 | ⬜ | 纵向宽度、Dock.Left/Right、AutoSize/DPI缩放，控件高度小于页按钮所需时的原生布局与范围保持。 |
| VScrollBar-007 | P2 | ⬜ | 32位超大范围的thumb tracking、窗口隐藏/禁用/释放中断、句柄重建和reparent，不能使用16位高字位置。 |
| VScrollBar-008 | P2 | ⬜ | TabStop=false、纵向scrollbar辅助角色和范围/页动作，隐藏事件/属性元数据与三平台原生焦点策略。 |
| VScrollBar-009 | P3 | ⬜ | Designer静态纵向布局与AOT滚轮/拖动；GTK/AppKit纵向反转、page比例、生命周期记录各自验收。 |
| VScrollBar-CLOSE | P3 | ⬜ | 对照 API 清单逐个关闭剩余属性、事件、方法/重载与受保护兼容点；属性写入后读回、通知顺序/取消/重入、默认值、异常、未建句柄/重建/释放后行为分别有证据。Windows 与两原生平台各出属性/事件/行为分子分母，未验收项不能标🧪 验收通过。 |

## 三平台的原生实现评估

**Windows**：SCROLLBAR + SBS_VERT；WM_VSCROLL 路由并通过 GetScrollInfo 取32位值。VScrollBar 对 RightToLeft 的特殊公开行为与横向不同，不能直接复制HScrollBar。

**Linux / GTK4**：GtkScrollbar vertical + GtkAdjustment，原生纵向page/step/drag动作与共享模型对应；GTK upper/page-size到WinForms终点换算显式处理。

**macOS / AppKit**：纵向 NSScroller，view坐标方向与滚动值方向分离；overlay自动隐藏策略要与控件Visible/Enabled区分，原生缺省箭头样式列差异。

跨平台工作排在 Windows 行为基线与该类最小原生试验之后。先复用同一公共 WinForms 类型与状态模型，在库内封装平台差异；本路线不要求新增平台项目或公开抽象。每平台至少覆盖：默认值/边界，代码与用户修改双向同步，事件顺序/取消/重入，键盘/输入法（适用时），DPI/RTL/本地化，无障碍，创建/重建/释放，AOT 实际运行；纯编译或仅显示窗口不足以通过。

验收口径：基线固定为 .NET 10.0.11 WinForms。Windows 的公开属性、公开事件、行为场景分别 ≥98%；Linux/macOS 在逐项确认的可移植分母中分别 ≥98%。每个重载、访问器、事件参数与行为场景独立登记，继承成员不遗漏；签名同名、字段存值、任务写完均不能作为通过分子。原生外观差异与 Windows 专属成员保留在清单中，不能用“平台不可等价”自动划掉普通业务需求。当前各项尚无完整对照证据，不发布已达标百分比。

## 修改历史

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 尚无该类型实现文件的提交历史；不将其他控件或本轮规划计为已实现。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

## 官方依据

- [WinForms VScrollBar API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.vscrollbar?view=windowsdesktop-10.0)，精确签名以本机 10.0.11 引用程序集生成的 API 清单为准。
- [Windows 原生控件参考](https://learn.microsoft.com/windows/win32/controls/scroll-bars)。
- [GTK4 Scrollbar](https://docs.gtk.org/gtk4/class.Scrollbar.html)。
- [AppKit nsscroller](https://developer.apple.com/documentation/appkit/nsscroller)。
