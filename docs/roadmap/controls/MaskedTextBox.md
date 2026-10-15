# MaskedTextBox

基线：2026-10-15 · .NET 10.0.11。目标继承：`MaskedTextBox : TextBoxBase`。本页是实施与验收任务，未声称已完成兼容。

## 当前证据与依赖

🟡 [MaskedTextBox.cs](../../../Lumina.Forms/MaskedTextBox.cs) 继承 TextBox，仅 Mask 自动属性；尚无掩码执行、MaskInputRejected、输入状态或类型验证。现有 API 名称出现不代表任何掩码行为已支持。

源码定位：[Lumina.Forms/MaskedTextBox.cs](../../../Lumina.Forms/MaskedTextBox.cs)；共同问题见 [Control](../../../Lumina.Forms/Control.cs) 和 [Win32 interop](../../../Lumina.Forms/Win32.cs)。先决依赖：TextBoxBase、System.ComponentModel.MaskedTextProvider、验证与文化信息；不要另造公开掩码语言。

公开成员（含继承的全部属性、事件和方法）逐项见 [MaskedTextBox API 清单](../api/MaskedTextBox.md)；共同行为、AOT、设计器与计数方法见 [基础任务](../foundations.md)。本表列出具体实现/验收动作，API 清单中余下成员也必须逐条关闭，不能把表外成员当作已支持。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 先修层级/阻塞，P1 完成常用契约，P2 补边界/高级场景，P3 完成平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| MaskedTextBox-001 | P0 | ⬜ | 改为 MaskedTextBox : TextBoxBase；补标准构造重载（Mask/MaskedTextProvider），公开 MaskedTextProvider 只读对象、Culture/FormatProvider 与相关枚举/事件参数类型。 |
| MaskedTextBox-002 | P1 | 🟡 | Mask=空字符串从存值改实际模型；标准掩码字符、必填/可选、文字转义、大小写与区域分隔符逐个用真实 WinForms 输入记录验收。 |
| MaskedTextBox-003 | P1 | ⬜ | Text、TextLength、SelectedText、MaskCompleted、MaskFull、TextMaskFormat、CutCopyMaskFormat；包括提示符/文字的显示、返回与复制内容分开验证。 |
| MaskedTextBox-004 | P1 | ⬜ | PromptChar、PasswordChar、UseSystemPasswordChar、AllowPromptAsInput、HidePromptOnLeave：默认值及不允许字符的异常，聚焦/失焦不误修改数据。 |
| MaskedTextBox-005 | P1 | ⬜ | AsciiOnly、SkipLiterals、ResetOnPrompt、ResetOnSpace、RejectInputOnFirstFailure；插入/删除/覆盖、跳过字面量、部分失败粘贴与替换选区的确切结果。 |
| MaskedTextBox-006 | P1 | ⬜ | InsertKeyMode、IsOverwriteMode、IsOverwriteModeChanged，Insert键和代码赋值联动；光标/选区在非法输入后位置与基线一致。 |
| MaskedTextBox-007 | P1 | ⬜ | MaskChanged、MaskInputRejected、TextChanged：参数 Position/RejectionHint、BeepOnError、多个拒绝字符的事件次数和顺序分别记录。 |
| MaskedTextBox-008 | P1 | ⬜ | ValidatingType、ValidateText、TypeValidationCompleted、取消失焦验证与格式失败；AOT 下已知类型转换不依赖未保留反射，不能把类型验证默认移除。 |
| MaskedTextBox-009 | P2 | ⬜ | ReadOnly、TextAlign/TextAlignChanged，以及 MaskedTextBox 对 Multiline/AcceptsTab/MaxLength/WordWrap/Undo 等成员的限制和隐藏元数据，不能沿用 TextBox 的任意行为。 |
| MaskedTextBox-010 | P2 | ⬜ | 中文输入法、emoji、代理对、双向文字、文化切换、粘贴/剪切/快捷键；原生预编辑期间不替换未提交字符。 |
| MaskedTextBox-011 | P3 | ⬜ | Designer 中 Mask/PromptChar/Culture 初始化往返；AOT 实际输入/类型验证、三平台文本可访问性、Dispose 验证回调与资源释放。 |
| MaskedTextBox-CLOSE | P3 | ⬜ | 对照 API 清单逐个关闭剩余属性、事件、方法/重载与受保护兼容点；属性写入后读回、通知顺序/取消/重入、默认值、异常、未建句柄/重建/释放后行为分别有证据。Windows 与两原生平台各出属性/事件/行为分子分母，未验收项不能标🧪 验收通过。 |

## 三平台的原生实现评估

**Windows**：使用 EDIT 作为输入宿主，托管 MaskedTextProvider 执行与 WinForms 相同掩码规则；WM_CHAR/粘贴/IME 提交与选择替换统一进入掩码模型，更新宿主时避免 EN_CHANGE 递归。不依赖运行时生成验证代理。

**Linux / GTK4**：GtkEntry 负责原生输入、光标、剪贴板和输入法；insert-text/delete-text/changed 与 MaskedTextProvider 绑定，GTK 字符偏移和 .NET UTF-16 索引转换必须单测；不能简单在每次 changed 后整串重写破坏预编辑。

**macOS / AppKit**：NSTextField + delegate/formatter 与相同 MaskedTextProvider 模型；shouldChangeTextInRanges 或编辑验证只处理已提交文本，尊重 marked text。NSFormatter 本身不等价 WinForms 掩码语言，不应直接替代且改变语义。

跨平台工作排在 Windows 行为基线与该类最小原生试验之后。先复用同一公共 WinForms 类型与状态模型，在库内封装平台差异；本路线不要求新增平台项目或公开抽象。每平台至少覆盖：默认值/边界，代码与用户修改双向同步，事件顺序/取消/重入，键盘/输入法（适用时），DPI/RTL/本地化，无障碍，创建/重建/释放，AOT 实际运行；纯编译或仅显示窗口不足以通过。

验收口径：基线固定为 .NET 10.0.11 WinForms。Windows 的公开属性、公开事件、行为场景分别 ≥98%；Linux/macOS 在逐项确认的可移植分母中分别 ≥98%。每个重载、访问器、事件参数与行为场景独立登记，继承成员不遗漏；签名同名、字段存值、任务写完均不能作为通过分子。原生外观差异与 Windows 专属成员保留在清单中，不能用“平台不可等价”自动划掉普通业务需求。当前各项尚无完整对照证据，不发布已达标百分比。

## 修改历史

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 2026-04-08 · `2545995` · feat: 增强深色模式支持，优化多个控件的主题应用逻辑。
- 2026-03-31 · `dcdc7e9` · Add theme and control components to Lumina.Forms。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

## 官方依据

- [WinForms MaskedTextBox API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.maskedtextbox?view=windowsdesktop-10.0)，精确签名以本机 10.0.11 引用程序集生成的 API 清单为准。
- [Windows 原生控件参考](https://learn.microsoft.com/windows/win32/controls/edit-controls)。
- [GTK4 Entry](https://docs.gtk.org/gtk4/class.Entry.html)。
- [AppKit nstextfield](https://developer.apple.com/documentation/appkit/nstextfield)。
