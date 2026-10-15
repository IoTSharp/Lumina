# LinkLabel

基线：2026-10-15 · .NET 10.0.11。目标继承：`LinkLabel : Label`。本页是实施与验收任务，未声称已完成兼容。

## 当前证据与依赖

🟡 [LinkLabel.cs](../../../Lumina.Forms/LinkLabel.cs) 只有继承 Label 与 LinkVisited 自动属性，没有 LinkClicked、Links、命中检测和键盘激活；当前不能作为可操作链接。

源码定位：[Lumina.Forms/LinkLabel.cs](../../../Lumina.Forms/LinkLabel.cs)；共同问题见 [Control](../../../Lumina.Forms/Control.cs) 和 [Win32 interop](../../../Lumina.Forms/Win32.cs)。先决依赖：Label 的文本测量、Control 焦点/鼠标命中、LinkArea/LinkLabel.Link/LinkCollection/LinkLabelLinkClickedEventArgs 等标准关联类型。

公开成员（含继承的全部属性、事件和方法）逐项见 [LinkLabel API 清单](../api/LinkLabel.md)；共同行为、AOT、设计器与计数方法见 [基础任务](../foundations.md)。本表列出具体实现/验收动作，API 清单中余下成员也必须逐条关闭，不能把表外成员当作已支持。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 先修层级/阻塞，P1 完成常用契约，P2 补边界/高级场景，P3 完成平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| LinkLabel-001 | P0 | ⬜ | 保留 LinkLabel : Label，补 LinkArea、Links、LinkLabel.Link/LinkCollection 与 LinkLabelLinkClickedEventArgs/委托；集合签名和对象身份与基线一致。 |
| LinkLabel-002 | P1 | ⬜ | LinkArea 默认范围按真实 WinForms 确认；零长度、越界、重叠链接、文本变化缩短、surrogate pair/组合字符和换行的范围规则逐项验收。 |
| LinkLabel-003 | P1 | ⬜ | Links 的 Add/Clear/Remove/indexer、LinkData、Enabled、Visited、Start、Length；集合变更立即影响命中、Tab 和辅助树，布局更新不复制掉 Link 对象身份。 |
| LinkLabel-004 | P1 | ⬜ | LinkClicked：鼠标、Enter/空格、辅助动作、IButtonControl.PerformClick（显式接口按基线）仅报告正确链接；禁用链接不激活，事件处理器决定是否导航。 |
| LinkLabel-005 | P1 | 🟡 | LinkVisited 与每条 Link.Visited 的关联；LinkColor、ActiveLinkColor、VisitedLinkColor、DisabledLinkColor、LinkBehavior 变化真实生效且不因 repaint 重置访问状态。 |
| LinkLabel-006 | P1 | ⬜ | TabStop、每条链接内部键盘遍历、焦点框、光标悬停和捕获取消；文本空白区域不触发 LinkClicked。 |
| LinkLabel-007 | P2 | ⬜ | Text/Font/AutoSize/TextAlign/Padding/RTL/UseCompatibleTextRendering 变化后重新计算链接矩形；多行链接点击区域与文字一致。 |
| LinkLabel-008 | P2 | ⬜ | IButtonControl.DialogResult/NotifyDefault 等显式接口存在性及行为按完整 API 清单验证，不能因 Label 基类而遗漏。 |
| LinkLabel-009 | P2 | ⬜ | 原生辅助树为每条链接提供名称、启用状态、访问状态、焦点和默认动作；不把整个标签暴露为一块不可点击静态文字。 |
| LinkLabel-010 | P3 | ⬜ | Designer 静态 LinkArea/Links 初始化与资源本地化，AOT 回调、控件释放和事件解绑；三平台单/多链接示例分别验收。 |
| LinkLabel-CLOSE | P3 | ⬜ | 对照 API 清单逐个关闭剩余属性、事件、方法/重载与受保护兼容点；属性写入后读回、通知顺序/取消/重入、默认值、异常、未建句柄/重建/释放后行为分别有证据。Windows 与两原生平台各出属性/事件/行为分子分母，未验收项不能标🧪 验收通过。 |

## 三平台的原生实现评估

**Windows**：单链接/富文本链接可评估原生 SysLink（WC_LINK）与 NM_CLICK/NM_RETURN，但 WinForms LinkLabel 支持任意 LinkArea、LinkCollection 与 Label 图文布局，不等于 SysLink 全部语义。先做映射样例；无法由原生类表达的能力明确写为兼容逻辑或原生目标差异。

**Linux / GTK4**：GtkLabel 的 markup <a> 和 activate-link、或单链接 GtkLinkButton；将 Unicode/UTF-16 LinkArea 索引转换，不把 byte 索引当字符索引。访问状态、颜色、任意部分文本与多链接可访问性逐项验收；链接激活只发事件，不擅自替应用启动 URL。

**macOS / AppKit**：带 NSAttributedString 链接属性、不可编辑且可选择的 NSTextView 结合委托处理链接；单链接可评估 NSButton 链接样式能力。多链接键盘遍历和任意颜色/下划线与 AppKit 默认行为不完全等价，须验证并登记差异。

跨平台工作排在 Windows 行为基线与该类最小原生试验之后。先复用同一公共 WinForms 类型与状态模型，在库内封装平台差异；本路线不要求新增平台项目或公开抽象。每平台至少覆盖：默认值/边界，代码与用户修改双向同步，事件顺序/取消/重入，键盘/输入法（适用时），DPI/RTL/本地化，无障碍，创建/重建/释放，AOT 实际运行；纯编译或仅显示窗口不足以通过。

验收口径：基线固定为 .NET 10.0.11 WinForms。Windows 的公开属性、公开事件、行为场景分别 ≥98%；Linux/macOS 在逐项确认的可移植分母中分别 ≥98%。每个重载、访问器、事件参数与行为场景独立登记，继承成员不遗漏；签名同名、字段存值、任务写完均不能作为通过分子。原生外观差异与 Windows 专属成员保留在清单中，不能用“平台不可等价”自动划掉普通业务需求。当前各项尚无完整对照证据，不发布已达标百分比。

## 修改历史

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 2026-03-31 · `dcdc7e9` · Add theme and control components to Lumina.Forms。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

## 官方依据

- [WinForms LinkLabel API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.linklabel?view=windowsdesktop-10.0)，精确签名以本机 10.0.11 引用程序集生成的 API 清单为准。
- [Windows 原生控件参考](https://learn.microsoft.com/windows/win32/controls/syslink-overview)。
- [GTK4 Label](https://docs.gtk.org/gtk4/class.Label.html)。
- [AppKit nstextview](https://developer.apple.com/documentation/appkit/nstextview)。
