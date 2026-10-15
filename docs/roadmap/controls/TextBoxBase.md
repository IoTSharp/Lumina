# TextBoxBase

基线：2026-10-15 · .NET 10.0.11。目标继承：`TextBoxBase : Control`。本页是实施与验收任务，未声称已完成兼容。

## 当前证据与依赖

⬜ 无 Lumina.Forms/TextBoxBase.cs。当前 TextBox 直接继承 Control，MaskedTextBox/RichTextBox 又继承 TextBox，与 WinForms 共同继承 TextBoxBase 的层级不同。现有 Select 在句柄未建立时直接返回，选择状态无法先存。

源码定位：`Lumina.Forms/TextBoxBase.cs`（尚不存在）；共同问题见 [Control](../../../Lumina.Forms/Control.cs) 和 [Win32 interop](../../../Lumina.Forms/Win32.cs)。先决依赖：Control.Text/键盘/IME/焦点与验证、剪贴板、字体和 native handle 重建。

公开成员（含继承的全部属性、事件和方法）逐项见 [TextBoxBase API 清单](../api/TextBoxBase.md)；共同行为、AOT、设计器与计数方法见 [基础任务](../foundations.md)。本表列出具体实现/验收动作，API 清单中余下成员也必须逐条关闭，不能把表外成员当作已支持。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 先修层级/阻塞，P1 完成常用契约，P2 补边界/高级场景，P3 完成平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| TextBoxBase-001 | P0 | ⬜ | 恢复 abstract TextBoxBase : Control，TextBox/RichTextBox/MaskedTextBox 直接继承它；迁移公共成员且不丢已有行为，ISupportInitialize 等按各类型基线放置。 |
| TextBoxBase-002 | P1 | ⬜ | Text、Lines、TextLength、SelectedText、SelectionStart、SelectionLength、Select/SelectAll/DeselectAll；无句柄时也保存选择，负数/超过文本范围、CRLF、emoji 边界按基线验收。 |
| TextBoxBase-003 | P1 | ⬜ | ReadOnly=false、Multiline=false、WordWrap=true、AcceptsTab=false、HideSelection=true、MaxLength=32767、ShortcutsEnabled=true 的类型默认值及派生覆盖逐一以基线确认。 |
| TextBoxBase-004 | P1 | ⬜ | ReadOnlyChanged、MultilineChanged、AcceptsTabChanged、HideSelectionChanged、BorderStyleChanged；同值赋值不重复，重建句柄不伪造用户文本变化。 |
| TextBoxBase-005 | P1 | ⬜ | Modified / ModifiedChanged、CanUndo、Undo、ClearUndo、Copy/Cut/Paste、Clear、AppendText；用户输入与代码赋值的撤销/修改标记分别对照。 |
| TextBoxBase-006 | P1 | ⬜ | TextChanged、KeyDown/KeyPress/KeyUp、输入法提交、粘贴、验证取消和失焦的事件顺序；不额外公开基线没有的 SelectionChanged（该事件属于 RichTextBox）。 |
| TextBoxBase-007 | P1 | ⬜ | BorderStyle、AutoSize、PreferredHeight、GetPreferredSize；单/多行、Font、DPI、滚动条切换后的客户端尺寸和选择/撤销保存。 |
| TextBoxBase-008 | P2 | ⬜ | GetCharFromPosition、GetCharIndexFromPosition、GetPositionFromCharIndex、GetLineFromCharIndex、GetFirstCharIndexFromLine、GetFirstCharIndexOfCurrentLine、ScrollToCaret；换行和端点规则逐场景测试。 |
| TextBoxBase-009 | P2 | ⬜ | Tab/Enter、快捷键关闭、只读仍可复制、密码隐藏、RTL、IME 候选窗口和组合输入不被按键拦截破坏；跨平台输入法原生集成验收。 |
| TextBoxBase-010 | P2 | ⬜ | 控件焦点与原生编辑子对象同步，辅助技术 text/value/selection/read-only 状态；Dispose 解除 field editor/剪贴板和通知回调。 |
| TextBoxBase-011 | P3 | ⬜ | 公开/受保护成员完整清单、Designer 文本/Lines/字体资源往返，AOT 下文本、选择、剪贴板与输入法实际操作。 |
| TextBoxBase-CLOSE | P3 | ⬜ | 对照 API 清单逐个关闭剩余属性、事件、方法/重载与受保护兼容点；属性写入后读回、通知顺序/取消/重入、默认值、异常、未建句柄/重建/释放后行为分别有证据。Windows 与两原生平台各出属性/事件/行为分子分母，未验收项不能标🧪 验收通过。 |

## 三平台的原生实现评估

**Windows**：EDIT 与 RICHEDIT50W 分别由派生类型选择；TextBoxBase 共享 EM_GETSEL/SETSEL、EM_REPLACESEL、EM_GETMODIFY/SETMODIFY、EM_UNDO、剪贴板命令等。不得把仅适用于 RichEdit 的消息发送给 EDIT 并声称支持。

**Linux / GTK4**：单行 GtkEntry、密码 GtkPasswordEntry、多行 GtkTextView/GtkTextBuffer，基类只共享托管文本/选择/事件契约。GtkTextBuffer 偏移是字符，WinForms 索引是 UTF-16，必须建立转换；剪贴板可能异步，公开同步接口的线程循环策略先做小型验证。

**macOS / AppKit**：单行 NSTextField/NSSecureTextField 通过 field editor，多行 NSTextView/NSTextStorage；使用 NSTextInputClient/原生输入法，NSString/NSRange 的 UTF-16 范围与 WinForms 索引对齐，仍需测试代理对和组合字符边界。共享契约落在既有 TextBoxBase，公开层不引入新 Editor API。

跨平台工作排在 Windows 行为基线与该类最小原生试验之后。先复用同一公共 WinForms 类型与状态模型，在库内封装平台差异；本路线不要求新增平台项目或公开抽象。每平台至少覆盖：默认值/边界，代码与用户修改双向同步，事件顺序/取消/重入，键盘/输入法（适用时），DPI/RTL/本地化，无障碍，创建/重建/释放，AOT 实际运行；纯编译或仅显示窗口不足以通过。

验收口径：基线固定为 .NET 10.0.11 WinForms。Windows 的公开属性、公开事件、行为场景分别 ≥98%；Linux/macOS 在逐项确认的可移植分母中分别 ≥98%。每个重载、访问器、事件参数与行为场景独立登记，继承成员不遗漏；签名同名、字段存值、任务写完均不能作为通过分子。原生外观差异与 Windows 专属成员保留在清单中，不能用“平台不可等价”自动划掉普通业务需求。当前各项尚无完整对照证据，不发布已达标百分比。

## 修改历史

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 尚无该类型实现文件的提交历史；不将其他控件或本轮规划计为已实现。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

## 官方依据

- [WinForms TextBoxBase API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.textboxbase?view=windowsdesktop-10.0)，精确签名以本机 10.0.11 引用程序集生成的 API 清单为准。
- [Windows 原生控件参考](https://learn.microsoft.com/windows/win32/controls/edit-controls)。
- [GTK4 TextView](https://docs.gtk.org/gtk4/class.TextView.html)。
- [AppKit nstextview](https://developer.apple.com/documentation/appkit/nstextview)。
