# TextBox

本页任务表只列未完成范围；代码完成子项标记在[完成状态](#已完成任务)，任务 ID 与验收状态均不因整理改变。剩余重点是完整编辑/输入、RTL 与 DPI、原生状态和派生覆盖；公共基类完成范围不自动关闭本控件。

[完整 API 与继承基线](../api/TextBox.md) · [任务索引](../README.md) · [依赖顺序](../execution.md) · [验证门槛](../validation.md) · [平台要求](../platforms.md)

固定基线为 .NET 10.0.11。Windows、Linux、macOS 分别验收，当前没有完整逐控件 98% 结论。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 先修层级/阻塞，P1 完成常用契约，P2 补边界/高级场景，P3 完成平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| TextBox-001 | P0 | 🟡 | 042的001a已改为TextBox : TextBoxBase并共用旧字体/选择/文本生命周期。完整基类成员与默认/Designer仍缺；现有SelectionChanged非WinForms成员暂留TextBox、不污染标准基类，正式迁移兼容决策仍待，不抵扣基线覆盖。 |
| TextBox-002 | P1 | 🟡 | 已有Text/EN_CHANGE与038的003c选择/重建范围保留；047～048继承SelectedText/Lines/AppendText/公开撤销，049继承005d标准Clear及Control-17e重挂命令。完整用户输入/通知重入及派生覆盖仍待，不能由有限编辑子范围关闭父项或重复新增继承任务。 |
| TextBox-003 | P1 | 🟡 | 028焦点、037输入判别、038多行重建保留；054继承基类007c/007d单行高度/默认和多行请求高度生命周期。完整缩放/跨屏DPI、布局、撤销栈、其余样式与完整编辑/IME/设备仍待；不重复计数继承范围。 |
| TextBox-004 | P1 | 🟡 | 043的004a使条样式/重建生效，044的007a补对齐/初始RTL与逻辑Left水平条限制；055的004b补滚动条首选尺寸和必要DPI系统度量。完整运行时RTL/镜像视觉、实际滚动行为/设备、复杂布局与跨屏缩放仍待，父项不关闭。 |
| TextBox-005 | P1 | 🟡 | 原已有ReadOnly原生映射，039的005a补只读/密码受限IME、PasswordChar实际回读与消息更新、UseSystemPasswordChar真实样式/重建优先级及ReadOnlyChanged。默认值、创建/重建/组合限制已有13项回归；完整复制限制、密码辅助技术防泄漏、AutoComplete协作和物理IME仍待。 |
| TextBox-006 | P1 | ⬜ | PlaceholderText=空字符串，空值规范化、聚焦时显示、清空 Text 后显示、多行支持差异和高对比度；占位文字不进入 Text/绑定值。 |
| TextBox-007 | P1 | 🟡 | 044的007a补TextAlign=Left、标准TextAlignChanged/虚通知、重建与初始对齐/RTL样式；045的007b补CharacterCasing=Normal、枚举/校验/重建与原生大小写样式及有限消息消费。真实剪贴板/完整文化与IME、运行时RTL/镜像视觉仍待，父项不关闭。 |
| TextBox-008 | P2 | ⬜ | AutoCompleteMode、AutoCompleteSource、AutoCompleteCustomSource；None 默认、增删候选、数据变更与密码/多行互斥，Shell 专属源标记 Windows 能力。 |
| TextBox-009 | P2 | 🟡 | 047～050替换/限制/撤销/剪贴板入口/快捷键禁用保留；051继承TextBoxBase-009a启用词删除与005f辅助，不重复新增继承完成任务。Paste(string)、真实剪贴板数据/超长粘贴、完整菜单/输入合并和UTF-16端点仍待。 |
| TextBox-010 | P2 | 🟡 | 022字体/颜色、046边框、053首选计算与054高度/默认/独立AutoSize及缓存保留；055滚动条测量归004b、创建请求归基类003e，不重复计数。完整缩放/跨屏DPI与字体重缩放、其余样式/元数据和真实Designer仍待，完整文本控件不关闭。 |
| TextBox-011 | P3 | ⬜ | 静态 Designer 与 .resx 文本/字体、公开事件完整清单、AOT 输入与剪贴板、三平台原生可访问性和 Dispose 验收。 |
| TextBox-CLOSE | P3 | ⬜ | 对照 API 清单逐个关闭剩余属性、事件、方法/重载与受保护兼容点；属性写入后读回、通知顺序/取消/重入、默认值、异常、未建句柄/重建/释放后行为分别有证据。Windows 与两原生平台各出属性/事件/行为分子分母，未验收项不能标🧪 验收通过。 |

## 三平台的原生实现评估

**Windows**：EDIT 的 ES_MULTILINE/ES_PASSWORD/ES_UPPERCASE/ES_LOWERCASE、EM_SETPASSWORDCHAR、EM_SETREADONLY、EM_SETCUEBANNER、EM_LIMITTEXT 及 EN_CHANGE；风格变更需要句柄重建或正确消息更新。自动完成涉及 Windows Shell 支持，AOT/COM 路径独立验证。

**Linux / GTK4**：GtkEntry 用于单行、GtkPasswordEntry 或 Entry.visibility 用于密码、GtkTextView 用于多行；placeholder-text、editable、input-purpose、wrap-mode、滚动容器映射。自动完成用原生候选界面与托管源，Windows Shell 路径/历史提供器不可宣称原生等价。

**macOS / AppKit**：NSTextField / NSSecureTextField，Multiline 用 NSTextView+NSScrollView；field editor 的 textDidChange/selection 与托管状态同步。Placeholder、CharacterCasing、PasswordChar 任意字符和 Windows Shell AutoCompleteSource 均需分别验证/列差异。

跨平台工作排在 Windows 行为基线与该类最小原生试验之后。先复用同一公共 WinForms 类型与状态模型，在库内封装平台差异；本路线不要求新增平台项目或公开抽象。每平台至少覆盖：默认值/边界，代码与用户修改双向同步，事件顺序/取消/重入，键盘/输入法（适用时），DPI/RTL/本地化，无障碍，创建/重建/释放，AOT 实际运行；纯编译或仅显示窗口不足以通过。

验收口径：基线固定为 .NET 10.0.11 WinForms。Windows 的公开属性、公开事件、行为场景分别 ≥98%；Linux/macOS 在逐项确认的可移植分母中分别 ≥98%。每个重载、访问器、事件参数与行为场景独立登记，继承成员不遗漏；签名同名、字段存值、任务写完均不能作为通过分子。原生外观差异与 Windows 专属成员保留在清单中，不能用“平台不可等价”自动划掉普通业务需求。当前各项尚无完整对照证据，不发布已达标百分比。

## 已完成任务

✅ 表示该行定义范围代码完成；父项的剩余代码与平台验收仍按当前任务表记录。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| TextBox-001a | P0 | ✅ | 批次042：TextBox直接继承标准TextBoxBase，密码/系统密码、AcceptsReturn、ScrollBars、PlaceholderText及SelectionChanged留派生；便利构造保持原始字段初始化时序，选择扩展通过内部连接继续派发原虚On*。基类引用/真实重建/事件/密码与旧相关组通过；完整001父项不关闭。 |
| TextBox-003a | P1 | ✅ | 批次028：移除TextBox独立WM_SETFOCUS/KILLFOCUS→Enter/Leave，改用Control标准GotFocus/LostFocus与ContainerControl逻辑进入离开；真实EDIT程序Focus/Select保留参考顺序且每次通知一次，独立回归通过。未补TextBoxBase继承或完整键盘/验证。 |
| TextBox-003b | P1 | ✅ | 批次037：现有TextBox消费固定TextBox/TextBoxBase输入键规则：多行Enter/AcceptsReturn、Tab/AcceptsTab/Ctrl、多行Escape、可编辑Back及Home/End/Page；AcceptsTab下Ctrl+Tab去除Ctrl后交父对话框链。8项无句柄/真实EDIT/Form默认按钮回归通过。未恢复TextBoxBase、运行期原生样式重建和完整编辑输入。 |
| TextBox-003c | P1 | ✅ | 批次038：Multiline变更真实重建EDIT后发MultilineChanged/虚OnMultilineChanged；同值不重建。Select/SelectionStart/SelectionLength支持创建前缓存、参数与有向范围、创建/重建恢复，保留Text及原生modified位并抑制创建中的重复EN_CHANGE。14项回归通过；仍直接继承Control，virtual Multiline为过渡声明，未完成TextBoxBase、尺寸/布局、撤销栈或完整编辑。 |
| TextBox-004a | P1 | ✅ | 批次043：ScrollBars.None默认/Localizable、合法枚举校验及value参数异常；同值no-op、变更保存状态后重建。多行按请求加纵条，水平条须WordWrap=false；保留密码与文本/选择/modified/只读和无重复通知。24项回归含16样式组合/独立EDIT通过；不含其他对齐/RTL/PreferredSize/设备。 |
| TextBox-004b | P1 | ✅ | 批次055：GetPreferredSizeCore多行按请求计垂直条宽，多行且不折行按请求计水平条高；先扣约束再基类测量后补回，水平计量不消费TextAlign.Left创建门禁。必要SM2/3仅线程PMv2使用内部控件DPI；37例含独立原生度量、9组context/DPI、约束/MaximumSize/缓存和真实EDIT状态保持。无句柄ScrollBars变更的既有无约束缓存按固定RecreateHandle guard保留；完整004、缩放/平台未关闭。 |
| TextBox-005a | P1 | ✅ | 批次039：CanEnableIme按ReadOnly/实际原生PasswordChar/基类能力判断，ImeModeBase受限时返回Disable但保留设置意图，DesignMode读配置；ReadOnlyChanged/虚On*在原生提交后、IME检查前。PasswordChar getter创建EDIT并回读，setter非系统密码时发EM_SETPASSWORDCHAR；UseSystemPasswordChar重建后优先系统字符，关闭恢复缓存字符，保留文本/选择/modified。13项回归含独立多行原生EDIT对照通过；TextBoxBase、完整密码安全/自动完成/设备仍待。 |
| TextBox-007a | P1 | ✅ | 批次044：TextAlign默认Left/Localizable、合法枚举与value异常、Events槽/虚On*、同值/重入/原异常/CanRaiseEvents；变化保存后重建再通知。创建时左右按有效RTL翻译并清WS_EX_RIGHT，水平条须逻辑Left且不WordWrap。共享枚举纠正Left0/Right1/Center2；28项回归通过。RTL原生对照复现既有Control创建后重提交样式，仅证样式消费，不是固定WinForms完整镜像/物理视觉验收。 |
| TextBox-007b | P1 | ✅ | 批次045：CharacterCasing标准非virtual属性、Behavior/DefaultValue(Normal)，Normal0/Upper1/Lower2；非法value异常不改状态/不建窗，同值no-op、变化保存后重建。创建消费ES_UPPERCASE/ES_LOWERCASE，不做手工转换或新增事件；25项回归含单双行ASCII/中文WM_CHAR硬期望、独立EDIT样式/程序设文/EM_REPLACESEL对照、文本/选择/modified/只读/滚动/密码/原异常保存通过。物理输入/剪贴板/完整文化/IME及007父项不关闭。 |
| TextBox-010a | P2 | ✅ | 批次022移除隐藏Font、克隆和外部Font释放，复用Control借用Font与独占HFONT创建/替换/销毁。无局部颜色时BackColor为Window（只读为Control）、ForeColor为WindowText，不继承父显式颜色；原生仍消费语义palette且明确记录默认前景差异。字体LOGFONT、Reset/重建/释放及派生颜色getter有限场景见022；全部文本/自动缩放/IME另列。 |

## 官方依据

- [WinForms TextBox API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.textbox?view=windowsdesktop-10.0)，精确签名以本机 10.0.11 引用程序集生成的 API 清单为准。
- [Windows 原生控件参考](https://learn.microsoft.com/windows/win32/controls/edit-controls)。
- [GTK4 Entry](https://docs.gtk.org/gtk4/class.Entry.html)。
- [AppKit nstextfield](https://developer.apple.com/documentation/appkit/nstextfield)。
