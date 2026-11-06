# TextBoxBase

基线：2026-10-15 · .NET 10.0.11。目标继承：`TextBoxBase : Control`。本页是实施与验收任务，未声称已完成兼容。

## 当前证据与依赖

044完成003b：HideSelection=true、标准HideSelectionChanged/虚On*及组件Events槽，变更先重建再通知，false加入ES_NOHIDESEL；14项新增与4,737项全量通过，见[044](../batches/044.md)。RichTextBox既有隐藏存值属性保留，其他默认/编辑/尺寸及派生后端仍待。

043完成003a共同设置首范围：AcceptsTabChanged标准事件/虚hook及WordWrap真实样式/句柄重建、同值缓存失效和AutoSize条件父布局。17项新增及4,695项全量通过，见[043](../batches/043.md)。折行由真实EDIT行数验证；HideSelection/BorderStyle/MaxLength/ShortcutsEnabled、完整测量/编辑和派生后端仍待。

🟡 批次042建立abstract TextBoxBase : Control及TextBox : TextBoxBase，迁入038/039等已有共享EDIT行为。内部无参构造按固定10.0.11保持internal；TextBox保留专属密码/Enter规则和非标准SelectionChanged扩展。MaskedTextBox/RichTextBox仍继承TextBox，直接层级和对应原生后端仍未恢复。新增8项基类回归及全量4,654项通过，只关闭001a限定提取范围，完整基类不关闭，见[042](../batches/042.md)。

源码定位：[TextBoxBase.cs](../../../Lumina.Forms/TextBoxBase.cs)；共同问题见 [Control](../../../Lumina.Forms/Control.cs) 和 [Win32 interop](../../../Lumina.Forms/Win32.cs)。先决依赖：Control.Text/键盘/IME/焦点与验证、剪贴板、字体和 native handle 重建。

公开成员（含继承的全部属性、事件和方法）逐项见 [TextBoxBase API 清单](../api/TextBoxBase.md)；共同行为、AOT、设计器与计数方法见 [基础任务](../foundations.md)。本表列出具体实现/验收动作，API 清单中余下成员也必须逐条关闭，不能把表外成员当作已支持。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 先修层级/阻塞，P1 完成常用契约，P2 补边界/高级场景，P3 完成平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| TextBoxBase-001 | P0 | 🟡 | 042完成001a标准基类及TextBox直接继承/已有行为迁移。RichTextBox/MaskedTextBox直接继承、对应原生实现与共享成员扩面仍待；ISupportInitialize等按各类型基线放置，不由一个派生迁移关闭父项。 |
| TextBoxBase-001a | P0 | ✅ | 批次042：建立abstract TextBoxBase : Control与internal无参构造，TextBox直接继承；迁移现有文本/选择、多行/只读、Tab、颜色/主题和EDIT句柄状态。TextLength为virtual，密码/Enter及既有SelectionChanged扩展保留TextBox。8项新增基类引用/真实重建/事件/派生hook回归与全量通过；不含另外两个派生直接层级或完整编辑能力。 |
| TextBoxBase-002 | P1 | 🟡 | 042迁入现有Text/Lines、virtual TextLength、SelectionStart/Length和Select的无句柄缓存/真实创建恢复。SelectedText、SelectAll/DeselectAll、完整CRLF/emoji/端点及Lines语义仍待，不以已有选择前置关闭父项。 |
| TextBoxBase-003 | P1 | 🟡 | 042迁入ReadOnly/Multiline/AcceptsTab及WordWrap；043的003a补AcceptsTab通知与WordWrap真实折行/重建/条件布局，044的003b补HideSelection标准设置/通知/样式。MaxLength/ShortcutsEnabled、完整默认/元数据及派生覆盖仍待；既有ES_WANTRETURN等完整CreateParams差异保留。 |
| TextBoxBase-003a | P1 | ✅ | 批次043：AcceptsTab=false、标准AcceptsTabChanged/虚On*、Events槽/同值/重入/原异常；WordWrap=true及Localizable，变更重建，所有赋值清本控件首选缓存，AutoSize时含同值参与父布局。默认AUTOH/AUTOV、多行折行清AUTOH，不再强制纵条；17项回归含真实EDIT折行/状态通过。完整003/004/007父项不关闭。 |
| TextBoxBase-003b | P1 | ✅ | 批次044：HideSelection=true、Behavior/DefaultValue元数据、HideSelectionChanged/虚On*与Events槽；同值不操作，合法变化保存后RecreateHandle再通知，false设置ES_NOHIDESEL。14项回归含独立EDIT样式、通知/重入/原异常/CanRaiseEvents及选择/modified/只读/密码保存通过。物理选区显示、RichEdit派生覆盖和完整003/004父项不关闭。 |
| TextBoxBase-004 | P1 | 🟡 | 042迁入ReadOnlyChanged/MultilineChanged，043的003a补AcceptsTabChanged，044的003b补HideSelectionChanged标准事件槽/虚通知及同值/重入/异常。BorderStyleChanged及其余完整通知仍待，不由这四个事件关闭父项。 |
| TextBoxBase-005 | P1 | 🟡 | 042迁入既有Clear/AppendText与重建前后的native modified位保存；未新增公开Modified/ModifiedChanged、CanUndo、Undo/ClearUndo、Copy/Cut/Paste。现有AppendText的空值/选择/撤销语义仍需固定对照，不能把迁移旧代码计为完整编辑。 |
| TextBoxBase-006 | P1 | 🟡 | 042共享EN_CHANGE/TextChanged、Control键事件与现有IME前置，标准基类没有SelectionChanged；TextBox非标准扩展暂留原派生。完整输入法提交、粘贴、验证/失焦和RichEdit选择通知仍待。 |
| TextBoxBase-007 | P1 | 🟡 | 042仅迁入既有颜色/主题与24/48最小原生高度，保留已知差异。BorderStyle、AutoSize、PreferredHeight、GetPreferredSize及Font/DPI/滚动条后的尺寸、选择/撤销保存仍未完整实现。 |
| TextBoxBase-008 | P2 | 🟡 | 042迁入既有有句柄ScrollToCaret；未建句柄延期滚动及GetCharFromPosition/GetCharIndexFromPosition/GetPositionFromCharIndex/GetLineFromCharIndex/GetFirstCharIndexFromLine/GetFirstCharIndexOfCurrentLine仍待，换行/端点逐场景验收。 |
| TextBoxBase-009 | P2 | 🟡 | 042按固定层级拆出共享Tab/编辑键/只读IME限制，Enter与PasswordProtect由TextBox消费；组合/候选通知继续原生默认过程。快捷键关闭、复制/密码完整限制、RTL、物理组合输入/候选窗口与跨平台原生集成仍待。 |
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

- 2026-11-07 · 批次044：003b补标准HideSelection设置/通知/原生样式及状态恢复；004交叉引用003b，不重复创建事件任务。RichTextBox保留独立存值且仅显式new，基类引用与派生引用状态不同的现状明确披露；BorderStyle/其余编辑/尺寸和真实派生后端仍待，见[044](../batches/044.md)。

- 2026-11-07 · 批次043：003a补共同设置标准事件、真实WordWrap与布局事务；移除基类强制纵条，由TextBox.ScrollBars决定实际条样式。旧WordWrap“仍存值”当前描述已纠正；高度/完整CreateParams/测量/编辑及派生后端保持缺口，不将17项回归当整基类或平台验收，见[043](../batches/043.md)。

- 2026-11-07 · 批次042：新增001a，001～009依据实际迁入的旧实现由全待办改部分，保留全部原ID/未完能力。基类存在与TextBox迁移不关闭RichTextBox/MaskedTextBox或任何完整编辑任务；旧存值/高度/文本语义差异保留。固定API库存仍是10.0.11历史快照，未改1,095类型/61,240成员行；构建/程序消息不等于AOT/Designer/设备验收，见[042](../batches/042.md)。

- 2026-11-07 · 批次039：现有TextBox消费CanEnableIme/ImeModeBase、ReadOnlyChanged及必要密码同步，但TextBoxBase类仍不存在，001～011/CLOSE全部原状态和ID保留；标准共享编辑/继承恢复另列，见[039](../batches/039.md)。

- 2026-11-07 · 批次038：TextBox-003c已修复既有TextBox无句柄Select丢状态，并补多行切换的有限句柄/选择/原生modified保存。本页“现有Select直接返回”的当前描述按真实代码纠正；TextBoxBase本身仍缺失，全部原ID/分母/未完任务保留，见[038](../batches/038.md)。

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 2026-11-07 · `bd9f4d2` · 批次042新增TextBoxBase共享EDIT首范围；此前“尚无该类型实现文件的提交历史”随源码存在已失效，本批按实际Git记录纠正。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

## 官方依据

- [WinForms TextBoxBase API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.textboxbase?view=windowsdesktop-10.0)，精确签名以本机 10.0.11 引用程序集生成的 API 清单为准。
- [Windows 原生控件参考](https://learn.microsoft.com/windows/win32/controls/edit-controls)。
- [GTK4 TextView](https://docs.gtk.org/gtk4/class.TextView.html)。
- [AppKit nstextview](https://developer.apple.com/documentation/appkit/nstextview)。
