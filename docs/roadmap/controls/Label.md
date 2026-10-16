# Label

基线：2026-10-15 · .NET 10.0.11。目标继承：`Label : Control`。本页是实施与验收任务，未声称已完成兼容。

## 当前证据与依赖

🟡 [Label.cs](../../../Lumina.Forms/Label.cs) 为 STATIC 原生窗口，TextAlign 默认 MiddleLeft 且仅自动属性；WinForms Label 默认 TextAlign 是 TopLeft。没有 TextAlignChanged、图片和文本度量的完整实现。

源码定位：[Lumina.Forms/Label.cs](../../../Lumina.Forms/Label.cs)；共同问题见 [Control](../../../Lumina.Forms/Control.cs) 和 [Win32 interop](../../../Lumina.Forms/Win32.cs)。先决依赖：Control 测量/布局、字体/图像资源策略、助记键焦点遍历。

公开成员（含继承的全部属性、事件和方法）逐项见 [Label API 清单](../api/Label.md)；共同行为、AOT、设计器与计数方法见 [基础任务](../foundations.md)。本表列出具体实现/验收动作，API 清单中余下成员也必须逐条关闭，不能把表外成员当作已支持。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 先修层级/阻塞，P1 完成常用契约，P2 补边界/高级场景，P3 完成平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| Label-001 | P0 | 🟡 | 纠正 TextAlign 默认 TopLeft 并让九种 ContentAlignment 真正更新显示；TextAlignChanged 只在值变化时触发，非法枚举按基线抛错。 |
| Label-002 | P1 | ⬜ | AutoSize=false、AutoEllipsis=false、UseMnemonic=true、TabStop=false 默认值；属性变化前后有无句柄、禁用状态的显示与事件逐项验收。 |
| Label-003 | P1 | ⬜ | GetPreferredSize、PreferredWidth、PreferredHeight：空文本、换行、长单词、Padding、Font、图片、DPI、父约束、RTL 的测量与 AutoSize 重排。 |
| Label-004 | P1 | ⬜ | Text / TextChanged、& 与 && 助记键以及下一个可聚焦控件激活；不让 Label 错误进入正常 Tab 焦点链。 |
| Label-005 | P1 | ⬜ | BorderStyle、FlatStyle、UseCompatibleTextRendering：默认值和句柄重建对照；原生可表达样式与 WinForms 绘制特有能力分别列差异。 |
| Label-006 | P1 | ⬜ | Image、ImageList、ImageIndex、ImageKey、ImageAlign；替换/移除/被释放图片和 ImageList 重建后重排，外部 Image 所有权不被错误接管。 |
| Label-007 | P2 | ⬜ | BackColor=Transparent 等父背景语义、文字颜色、BackgroundImage / BackgroundImageLayout 的隐藏与实际行为，主题切换不能覆盖用户显式设置。 |
| Label-008 | P2 | ⬜ | AutoSizeChanged、TextAlignChanged、TabStopChanged、继承鼠标/Click 事件与 Label 隐藏键盘事件，逐条对照元数据和实际触发方式。 |
| Label-009 | P2 | ⬜ | LiveSetting 与 AccessibilityObject 的文本名称、live region 更新；屏幕阅读器读取静态文本及助记键关联的输入控件。 |
| Label-010 | P3 | ⬜ | Designer 中 AutoSize 静态初始化顺序与 .resx 本地化，三平台字体替代后布局不截断；AOT 资源读取及 Dispose 图像/字体/回调释放。 |
| Label-CLOSE | P3 | ⬜ | 对照 API 清单逐个关闭剩余属性、事件、方法/重载与受保护兼容点；属性写入后读回、通知顺序/取消/重入、默认值、异常、未建句柄/重建/释放后行为分别有证据。Windows 与两原生平台各出属性/事件/行为分子分母，未验收项不能标🧪 验收通过。 |

## 三平台的原生实现评估

**Windows**：STATIC 可覆盖基本 SS_LEFT/CENTER/RIGHT、SS_NOPREFIX、SS_*ELLIPSIS；WinForms Label 的九宫格对齐、透明背景、图像组合和文本渲染存在托管绘制语义，需明确哪些原生 STATIC 风格能等价，哪些为兼容绘制而非纯原生控件能力。

**Linux / GTK4**：GtkLabel 支持文字、换行、ellipsize、mnemonic、xalign/yalign；图片用原生 GtkImage 和容器组合。Windows GDI 与 Pango 的逐像素度量、UseCompatibleTextRendering 不是可移植同义行为，公共文本/布局场景与专属渲染选择分开计数。

**macOS / AppKit**：不可编辑、无边框 NSTextField 用作标签；alignment、lineBreakMode、maximumNumberOfLines 做映射。NSImageView 可组合图片；AppKit 没有 Windows Alt 助记键惯例，需要在窗口键盘层保留兼容动作，外观和字距差异不伪装完全一致。

跨平台工作排在 Windows 行为基线与该类最小原生试验之后。先复用同一公共 WinForms 类型与状态模型，在库内封装平台差异；本路线不要求新增平台项目或公开抽象。每平台至少覆盖：默认值/边界，代码与用户修改双向同步，事件顺序/取消/重入，键盘/输入法（适用时），DPI/RTL/本地化，无障碍，创建/重建/释放，AOT 实际运行；纯编译或仅显示窗口不足以通过。

验收口径：基线固定为 .NET 10.0.11 WinForms。Windows 的公开属性、公开事件、行为场景分别 ≥98%；Linux/macOS 在逐项确认的可移植分母中分别 ≥98%。每个重载、访问器、事件参数与行为场景独立登记，继承成员不遗漏；签名同名、字段存值、任务写完均不能作为通过分子。原生外观差异与 Windows 专属成员保留在清单中，不能用“平台不可等价”自动划掉普通业务需求。当前各项尚无完整对照证据，不发布已达标百分比。

## 修改历史

- 2026-10-16 · 批次023：基础Control启用标准UserPaint后，本控件构造显式保留UserPaint=false，使现有系统窗口类过程及原生字体/绘制继续生效。仅属消息与绘制基础的必要衔接，不代表本控件完整Styles、Paint事件或视觉/输入兼容完成，见[023](../batches/023.md)。

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 2026-04-09 · `9d5165f` · feat: 添加多个控件和功能，增强用户界面和交互体验。
- 2026-04-08 · `7d38999` · feat: 增强深色模式支持，优化多个控件的主题应用逻辑。
- 2026-03-31 · `ce86ab5` · 已经把 Lumina.Forms 的主题能力接到更像 WinForms 的核心表面上了。现在 Control.cs 和 Form.cs 提供了 BackColor / ForeColor、对应变更事件、主题槽位解析、父容器背景继承，以及打开窗口后的实时主题刷新；高频控件也统一改成走 ApplyExp…（原提交标题节录）。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

## 官方依据

- [WinForms Label API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.label?view=windowsdesktop-10.0)，精确签名以本机 10.0.11 引用程序集生成的 API 清单为准。
- [Windows 原生控件参考](https://learn.microsoft.com/windows/win32/controls/static-controls)。
- [GTK4 Label](https://docs.gtk.org/gtk4/class.Label.html)。
- [AppKit nstextfield](https://developer.apple.com/documentation/appkit/nstextfield)。
