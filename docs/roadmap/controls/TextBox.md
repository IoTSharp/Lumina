# TextBox

基线：2026-10-15 · .NET 10.0.11。目标继承：`TextBox : TextBoxBase`。本页是实施与验收任务，未声称已完成兼容。

## 当前证据与依赖

🟡 [TextBox.cs](../../../Lumina.Forms/TextBox.cs) 有 EDIT、EN_CHANGE、ReadOnly 原生映射、AppendText/Select/ScrollToCaret；Multiline/AcceptsReturn/AcceptsTab/PasswordChar/ScrollBars/WordWrap/PlaceholderText 多为存值，Style 未采用全部值，Select 未建句柄直接返回。错误公开 SelectionChanged（WinForms TextBox 未提供该事件）仍待迁移决策；022已移除隐藏Font及克隆/释放，复用基础继承和借用字体，并补标准颜色默认，详见010a。

源码定位：[Lumina.Forms/TextBox.cs](../../../Lumina.Forms/TextBox.cs)；共同问题见 [Control](../../../Lumina.Forms/Control.cs) 和 [Win32 interop](../../../Lumina.Forms/Win32.cs)。先决依赖：TextBoxBase、Control.Text 原生同步、Form.AcceptButton/CancelButton、剪贴板和自动完成关联类型。

公开成员（含继承的全部属性、事件和方法）逐项见 [TextBox API 清单](../api/TextBox.md)；共同行为、AOT、设计器与计数方法见 [基础任务](../foundations.md)。本表列出具体实现/验收动作，API 清单中余下成员也必须逐条关闭，不能把表外成员当作已支持。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 先修层级/阻塞，P1 完成常用契约，P2 补边界/高级场景，P3 完成平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| TextBox-001 | P0 | ⬜ | 改为 TextBox : TextBoxBase，共用字体/选择/文本生命周期；现有 SelectionChanged 非 WinForms 成员列迁移兼容决策，避免以扩展成员算基线覆盖。 |
| TextBox-002 | P1 | 🟡 | Text/Lines、EN_CHANGE→TextChanged 双向同步；程序同值、用户输入、AppendText、Clear、句柄重建各自的通知次数按基线，Select 在建句柄前保留状态。 |
| TextBox-003 | P1 | ⬜ | Multiline=false、AcceptsReturn=false、AcceptsTab=false：Enter 与 Form.AcceptButton、Tab 与焦点导航配合；多行切换保持文字、选择、修改标志。 |
| TextBox-004 | P1 | ⬜ | WordWrap=true、ScrollBars=None；四种滚动条与换行组合真实生效，水平滚动条在 WordWrap 开启时的表现按基线。 |
| TextBox-005 | P1 | ⬜ | PasswordChar='\0'、UseSystemPasswordChar=false、ReadOnly=false；运行时更新、系统密码字符优先级、复制限制、辅助技术不泄漏密码。 |
| TextBox-006 | P1 | ⬜ | PlaceholderText=空字符串，空值规范化、聚焦时显示、清空 Text 后显示、多行支持差异和高对比度；占位文字不进入 Text/绑定值。 |
| TextBox-007 | P1 | ⬜ | CharacterCasing=Normal、TextAlign=Left、TextAlignChanged；输入/粘贴/程序赋值的大小写转换分别验收，RTL 与对齐不混为一个开关。 |
| TextBox-008 | P2 | ⬜ | AutoCompleteMode、AutoCompleteSource、AutoCompleteCustomSource；None 默认、增删候选、数据变更与密码/多行互斥，Shell 专属源标记 Windows 能力。 |
| TextBox-009 | P2 | ⬜ | Paste(string)、继承 Paste/Copy/Cut/Undo、MaxLength、SelectedText、ShortcutsEnabled；异常、只读、超长粘贴和 UTF-16 端点完整对照。 |
| TextBox-010 | P2 | 🟡 | 022完成010a基础Font继承/借用寿命与必要颜色默认；单行PreferredHeight/AutoSize、BorderStyle、DPI/IME及固定最小24高度差异仍待实现，完整文本控件不关闭。 |
| TextBox-010a | P2 | ✅ | 批次022移除隐藏Font、克隆和外部Font释放，复用Control借用Font与独占HFONT创建/替换/销毁。无局部颜色时BackColor为Window（只读为Control）、ForeColor为WindowText，不继承父显式颜色；原生仍消费语义palette且明确记录默认前景差异。字体LOGFONT、Reset/重建/释放及派生颜色getter有限场景见022；全部文本/自动缩放/IME另列。 |
| TextBox-011 | P3 | ⬜ | 静态 Designer 与 .resx 文本/字体、公开事件完整清单、AOT 输入与剪贴板、三平台原生可访问性和 Dispose 验收。 |
| TextBox-CLOSE | P3 | ⬜ | 对照 API 清单逐个关闭剩余属性、事件、方法/重载与受保护兼容点；属性写入后读回、通知顺序/取消/重入、默认值、异常、未建句柄/重建/释放后行为分别有证据。Windows 与两原生平台各出属性/事件/行为分子分母，未验收项不能标🧪 验收通过。 |

## 三平台的原生实现评估

**Windows**：EDIT 的 ES_MULTILINE/ES_PASSWORD/ES_UPPERCASE/ES_LOWERCASE、EM_SETPASSWORDCHAR、EM_SETREADONLY、EM_SETCUEBANNER、EM_LIMITTEXT 及 EN_CHANGE；风格变更需要句柄重建或正确消息更新。自动完成涉及 Windows Shell 支持，AOT/COM 路径独立验证。

**Linux / GTK4**：GtkEntry 用于单行、GtkPasswordEntry 或 Entry.visibility 用于密码、GtkTextView 用于多行；placeholder-text、editable、input-purpose、wrap-mode、滚动容器映射。自动完成用原生候选界面与托管源，Windows Shell 路径/历史提供器不可宣称原生等价。

**macOS / AppKit**：NSTextField / NSSecureTextField，Multiline 用 NSTextView+NSScrollView；field editor 的 textDidChange/selection 与托管状态同步。Placeholder、CharacterCasing、PasswordChar 任意字符和 Windows Shell AutoCompleteSource 均需分别验证/列差异。

跨平台工作排在 Windows 行为基线与该类最小原生试验之后。先复用同一公共 WinForms 类型与状态模型，在库内封装平台差异；本路线不要求新增平台项目或公开抽象。每平台至少覆盖：默认值/边界，代码与用户修改双向同步，事件顺序/取消/重入，键盘/输入法（适用时），DPI/RTL/本地化，无障碍，创建/重建/释放，AOT 实际运行；纯编译或仅显示窗口不足以通过。

验收口径：基线固定为 .NET 10.0.11 WinForms。Windows 的公开属性、公开事件、行为场景分别 ≥98%；Linux/macOS 在逐项确认的可移植分母中分别 ≥98%。每个重载、访问器、事件参数与行为场景独立登记，继承成员不遗漏；签名同名、字段存值、任务写完均不能作为通过分子。原生外观差异与 Windows 专属成员保留在清单中，不能用“平台不可等价”自动划掉普通业务需求。当前各项尚无完整对照证据，不发布已达标百分比。

## 修改历史

- 2026-10-16 · 批次022：010由全待办改部分，010a明确关闭基础字体所有权/继承及必要颜色默认；不再将外部Font会被Dispose写成当前问题。公开属性准确默认与native palette消费分离，3条默认前景色差异保留；TextBoxBase标准继承、ReadOnlyChanged/完整事件、PreferredHeight/DPI/IME和Designer仍缺。固定TextBoxBase v10.0.11源码及hash保存在022证据目录。

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 2026-04-09 · `9d5165f` · feat: 添加多个控件和功能，增强用户界面和交互体验。
- 2026-04-08 · `cb090bc` · Add Lumina Notepad demo application with basic text editing features。
- 2026-04-08 · `108ef2b` · feat: 添加对 Win32 黑暗模式的支持，重构多个控件以优化主题应用逻辑。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

## 官方依据

- [WinForms TextBox API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.textbox?view=windowsdesktop-10.0)，精确签名以本机 10.0.11 引用程序集生成的 API 清单为准。
- [Windows 原生控件参考](https://learn.microsoft.com/windows/win32/controls/edit-controls)。
- [GTK4 Entry](https://docs.gtk.org/gtk4/class.Entry.html)。
- [AppKit nstextfield](https://developer.apple.com/documentation/appkit/nstextfield)。
