# TextBox

基线：2026-10-15 · .NET 10.0.11。目标继承：`TextBox : TextBoxBase`。本页是实施与验收任务，未声称已完成兼容。

## 当前证据与依赖

054源码no-op，继承TextBoxBase-007c/007d/007e的PreferredHeight/100宽默认、独立AutoSize/请求高度和首选缓存；默认AutoSize改为标准true，旧24/48原生下限已取消。通用布局不按文本扩宽，原测量/零尺寸/菜单固定几何夹具明确关闭AutoSize。只在基类登记完成子范围；TextBox滚动条首选测量、完整样式/缩放/元数据与平台仍待，见[054](../batches/054.md)。下述053及以前为历史。

053源码no-op，继承TextBoxBase-007b真实首选尺寸计算与008c普通EDIT延期/空文本滚动，108新增/5,329全量有界回归通过；只在基类登记，不重复计数。AutoSize/PreferredHeight/DefaultSize、24/48旧高度与完整缓存/请求高度生命周期仍待，见[053](../batches/053.md)。下述052及以前为历史。

052源码no-op，继承TextBoxBase-008a/008b六个标准行/字符位置查询；真实EDIT的UTF-16/CRLF/尾空行/软折行及状态保持有102新增/5,221全量证据。仅在基类登记完成范围，不重复计数。AutoSize/PreferredHeight/DefaultSize/GetPreferredSize与完整端点/输入/平台仍待，见[052](../batches/052.md)。下述051及以前为历史。

051源码no-op，继承基类009a启用Ctrl+Backspace/Ctrl+Shift+Backspace可撤销删除及005f固定词边界；必要redraw辅助归Control-14c。88新增/5,119全量通过，不另建重复继承完成项。真实剪贴板/Paste(string)、完整输入/测量/default、运行时RTL和平台仍待，见[051](../batches/051.md)。下述050及以前为历史。

050源码no-op，继承基类005e的Copy/Cut/Paste命令入口及003d的快捷键禁用/只读对齐/系统菜单抑制；77新增及5,031全量通过。入口消息截获测试不证明真实剪贴板数据，custom菜单只核受限路由。启用时Ctrl+Backspace词删除、完整测量/default和平台仍待，见[050](../batches/050.md)。下述049及以前为历史。

049源码no-op，继承TextBoxBase-005d的标准Clear虚Text=null与Control-17e的重挂EDIT命令连接；原HWND与真实撤销/通知保留。Copy/Cut/Paste、ShortcutsEnabled、完整测量/默认及原反射父Form寿命继续原任务，见[049](../batches/049.md)。下述048及以前为历史。

048源码no-op，继承TextBoxBase-005b标准AppendText及005c公开CanUndo/Undo/ClearUndo。空值追加不操作，非空经原生替换并清撤销/modified，零尺寸恢复原选区；公开撤销消费真实EDIT。Copy/Cut/Paste、Clear完整语义、测量/默认和运行时RTL仍待，见[048](../batches/048.md)。

047源码no-op，继承TextBoxBase-002b/002c/003c的SelectedText、Lines与MaxLength；程序替换暂解限制后恢复，真实输入仍受配置限制。完整AppendText/公开撤销/剪贴板、测量/运行时RTL和派生后端仍待，见[047](../batches/047.md)。

046源码no-op，继承基类007a的BorderStyle/标准通知与002a的SelectAll/DeselectAll；44项新增回归通过。初值/重建状态按真实EDIT消费，不将创建请求位等同规范化后的公开样式位；完整尺寸/SelectedText/Lines/撤销/剪贴板和运行时RTL仍待，见[046](../batches/046.md)。

045完成007b：CharacterCasing=Normal、标准枚举Normal0/Upper1/Lower2、非法value异常/同值no-op、变更重建及原生ES_UPPERCASE/ES_LOWERCASE，无自创Changed事件或托管字符串转换。25项新增、全量4,782项通过，见[045](../batches/045.md)。继承005a的Modified/程序设文守卫；完整运行时RTL/镜像视觉、尺寸/编辑和派生后端仍待。

044完成007a：TextAlign标准默认/校验/事件、变更重建及EDIT创建对齐位，初始有效RTL翻译左右，清WS_EX_RIGHT；水平条限逻辑Left。28项新增、42项本批及4,737全量通过，见[044](../batches/044.md)。HorizontalAlignment按固定10.0.11纠正为Left=0/Right=1/Center=2；旧二进制常量及整数1/2持久化需复核。完整运行时RTL/镜像视觉/尺寸及CharacterCasing仍待。

043的004a使ScrollBars按合法枚举/标准默认None更新原生样式并重建，配合基类WordWrap真实切换折行；同值不重建，状态/密码及创建事件顺序保持。24项新增含16组合独立EDIT对照与全量4,695项通过，见[043](../batches/043.md)。完整TextAlign/RTL、滚动条首选尺寸和实际滚动/设备仍待。

037～041有限范围保留；042完成001a的TextBox : TextBoxBase，共享现有文本/选择、多行/只读、Tab/IME前置及EDIT句柄/主题路径，TextBox保留密码、Enter与旧非标准SelectionChanged扩展。8项新增基类回归、相关318项和全量4,654项通过，见[042](../batches/042.md)。完整基类、其余运行期样式、密码安全/辅助技术及另外两个派生直接继承仍缺。

🟡 [TextBox.cs](../../../Lumina.Forms/TextBox.cs) 有 EDIT、EN_CHANGE、ReadOnly 原生映射、AppendText/Select/ScrollToCaret；038修复多行重建/选择，039补PasswordChar真实回读/消息更新和UseSystemPasswordChar样式/重建优先级，ReadOnlyChanged在IME检查前通知。043已接真实ScrollBars/WordWrap样式；PlaceholderText等仍存值，完整Style/布局尚缺。错误公开SelectionChanged（WinForms TextBox未提供该事件）仍待迁移决策；022～038范围保留，完整Tab/键盘/验证/编辑与平台仍缺。

源码定位：[Lumina.Forms/TextBox.cs](../../../Lumina.Forms/TextBox.cs)；共同问题见 [Control](../../../Lumina.Forms/Control.cs) 和 [Win32 interop](../../../Lumina.Forms/Win32.cs)。先决依赖：TextBoxBase、Control.Text 原生同步、Form.AcceptButton/CancelButton、剪贴板和自动完成关联类型。

公开成员（含继承的全部属性、事件和方法）逐项见 [TextBox API 清单](../api/TextBox.md)；共同行为、AOT、设计器与计数方法见 [基础任务](../foundations.md)。本表列出具体实现/验收动作，API 清单中余下成员也必须逐条关闭，不能把表外成员当作已支持。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 先修层级/阻塞，P1 完成常用契约，P2 补边界/高级场景，P3 完成平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| TextBox-001 | P0 | 🟡 | 042的001a已改为TextBox : TextBoxBase并共用旧字体/选择/文本生命周期。完整基类成员与默认/Designer仍缺；现有SelectionChanged非WinForms成员暂留TextBox、不污染标准基类，正式迁移兼容决策仍待，不抵扣基线覆盖。 |
| TextBox-001a | P0 | ✅ | 批次042：TextBox直接继承标准TextBoxBase，密码/系统密码、AcceptsReturn、ScrollBars、PlaceholderText及SelectionChanged留派生；便利构造保持原始字段初始化时序，选择扩展通过内部连接继续派发原虚On*。基类引用/真实重建/事件/密码与旧相关组通过；完整001父项不关闭。 |
| TextBox-002 | P1 | 🟡 | 已有Text/EN_CHANGE与038的003c选择/重建范围保留；047～048继承SelectedText/Lines/AppendText/公开撤销，049继承005d标准Clear及Control-17e重挂命令。完整用户输入/通知重入及派生覆盖仍待，不能由有限编辑子范围关闭父项或重复新增继承任务。 |
| TextBox-003 | P1 | 🟡 | 028焦点、037输入判别、038多行重建保留；054继承基类007c/007d单行高度/默认和多行请求高度生命周期。完整缩放/跨屏DPI、布局、撤销栈、其余样式与完整编辑/IME/设备仍待；不重复计数继承范围。 |
| TextBox-003a | P1 | ✅ | 批次028：移除TextBox独立WM_SETFOCUS/KILLFOCUS→Enter/Leave，改用Control标准GotFocus/LostFocus与ContainerControl逻辑进入离开；真实EDIT程序Focus/Select保留参考顺序且每次通知一次，独立回归通过。未补TextBoxBase继承或完整键盘/验证。 |
| TextBox-003b | P1 | ✅ | 批次037：现有TextBox消费固定TextBox/TextBoxBase输入键规则：多行Enter/AcceptsReturn、Tab/AcceptsTab/Ctrl、多行Escape、可编辑Back及Home/End/Page；AcceptsTab下Ctrl+Tab去除Ctrl后交父对话框链。8项无句柄/真实EDIT/Form默认按钮回归通过。未恢复TextBoxBase、运行期原生样式重建和完整编辑输入。 |
| TextBox-003c | P1 | ✅ | 批次038：Multiline变更真实重建EDIT后发MultilineChanged/虚OnMultilineChanged；同值不重建。Select/SelectionStart/SelectionLength支持创建前缓存、参数与有向范围、创建/重建恢复，保留Text及原生modified位并抑制创建中的重复EN_CHANGE。14项回归通过；仍直接继承Control，virtual Multiline为过渡声明，未完成TextBoxBase、尺寸/布局、撤销栈或完整编辑。 |
| TextBox-004 | P1 | 🟡 | 043的004a使None/Horizontal/Vertical/Both与WordWrap及单双行样式/重建生效；044的007a补对齐创建位、初始有效RTL翻译及逻辑TextAlign.Left水平条限制。完整运行时RTL/镜像视觉、滚动条首选尺寸、实际滚动行为及全部布局协作仍待，父项不关闭。 |
| TextBox-004a | P1 | ✅ | 批次043：ScrollBars.None默认/Localizable、合法枚举校验及value参数异常；同值no-op、变更保存状态后重建。多行按请求加纵条，水平条须WordWrap=false；保留密码与文本/选择/modified/只读和无重复通知。24项回归含16样式组合/独立EDIT通过；不含其他对齐/RTL/PreferredSize/设备。 |
| TextBox-005 | P1 | 🟡 | 原已有ReadOnly原生映射，039的005a补只读/密码受限IME、PasswordChar实际回读与消息更新、UseSystemPasswordChar真实样式/重建优先级及ReadOnlyChanged。默认值、创建/重建/组合限制已有13项回归；完整复制限制、密码辅助技术防泄漏、AutoComplete协作和物理IME仍待。 |
| TextBox-005a | P1 | ✅ | 批次039：CanEnableIme按ReadOnly/实际原生PasswordChar/基类能力判断，ImeModeBase受限时返回Disable但保留设置意图，DesignMode读配置；ReadOnlyChanged/虚On*在原生提交后、IME检查前。PasswordChar getter创建EDIT并回读，setter非系统密码时发EM_SETPASSWORDCHAR；UseSystemPasswordChar重建后优先系统字符，关闭恢复缓存字符，保留文本/选择/modified。13项回归含独立多行原生EDIT对照通过；TextBoxBase、完整密码安全/自动完成/设备仍待。 |
| TextBox-006 | P1 | ⬜ | PlaceholderText=空字符串，空值规范化、聚焦时显示、清空 Text 后显示、多行支持差异和高对比度；占位文字不进入 Text/绑定值。 |
| TextBox-007 | P1 | 🟡 | 044的007a补TextAlign=Left、标准TextAlignChanged/虚通知、重建与初始对齐/RTL样式；045的007b补CharacterCasing=Normal、枚举/校验/重建与原生大小写样式及有限消息消费。真实剪贴板/完整文化与IME、运行时RTL/镜像视觉仍待，父项不关闭。 |
| TextBox-007a | P1 | ✅ | 批次044：TextAlign默认Left/Localizable、合法枚举与value异常、Events槽/虚On*、同值/重入/原异常/CanRaiseEvents；变化保存后重建再通知。创建时左右按有效RTL翻译并清WS_EX_RIGHT，水平条须逻辑Left且不WordWrap。共享枚举纠正Left0/Right1/Center2；28项回归通过。RTL原生对照复现既有Control创建后重提交样式，仅证样式消费，不是固定WinForms完整镜像/物理视觉验收。 |
| TextBox-007b | P1 | ✅ | 批次045：CharacterCasing标准非virtual属性、Behavior/DefaultValue(Normal)，Normal0/Upper1/Lower2；非法value异常不改状态/不建窗，同值no-op、变化保存后重建。创建消费ES_UPPERCASE/ES_LOWERCASE，不做手工转换或新增事件；25项回归含单双行ASCII/中文WM_CHAR硬期望、独立EDIT样式/程序设文/EM_REPLACESEL对照、文本/选择/modified/只读/滚动/密码/原异常保存通过。物理输入/剪贴板/完整文化/IME及007父项不关闭。 |
| TextBox-008 | P2 | ⬜ | AutoCompleteMode、AutoCompleteSource、AutoCompleteCustomSource；None 默认、增删候选、数据变更与密码/多行互斥，Shell 专属源标记 Windows 能力。 |
| TextBox-009 | P2 | 🟡 | 047～050替换/限制/撤销/剪贴板入口/快捷键禁用保留；051继承TextBoxBase-009a启用词删除与005f辅助，不重复新增继承完成任务。Paste(string)、真实剪贴板数据/超长粘贴、完整菜单/输入合并和UTF-16端点仍待。 |
| TextBox-010 | P2 | 🟡 | 022字体/颜色、046边框和053首选计算保留；054继承007c/007d/007e标准单行高度/默认、独立AutoSize/请求高度及首选缓存，旧24/48原生下限已取消。完整缩放/跨屏DPI与字体重缩放、滚动条测量/元数据和真实Designer仍待，完整文本控件不关闭，不重复登记继承完成项。 |
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

- 2026-11-07 · 批次054：源码no-op，继承基类007c/007d/007e，不新增重复完成任务；旧尺寸/default当前缺口按有限实现纠正，完整缩放、样式和平台仍待，见[054](../batches/054.md)。

- 2026-11-07 · 批次053：源码no-op，继承基类007b/008c，010按计算与完整生命周期分别记录；旧高度、AutoSize/default和平台父项保持，见[053](../batches/053.md)。

- 2026-11-07 · 批次052：源码no-op，继承基类008a/008b，未新增重复完成任务；旧24/48最小高度、完整测量/default及平台缺口保持，见[052](../batches/052.md)。

- 2026-11-07 · 批次051：源码no-op，继承009a启用词删除命令；普通EDIT限定回归不关闭完整009/输入/测量/平台，也不重复计数，见[051](../batches/051.md)。

- 2026-11-07 · 批次050：源码no-op，009按真实继承的命令入口及快捷键禁用范围同步，不重复新增任务或关闭父项。15个剪贴板消息用例和62个快捷键用例均通过，未读写系统剪贴板或实际弹出custom菜单；完整编辑/尺寸及平台门禁保持，见[050](../batches/050.md)。

- 2026-11-07 · 批次049：源码no-op，002按真实继承Clear虚调用及重挂命令连接同步，不重复新增继承完成任务；005/009/010及平台父项保持，见[049](../batches/049.md)。

- 2026-11-07 · 批次048：源码no-op，继承标准AppendText与公开撤销入口，002/009按实际范围同步且父项仍部分，不重复计数。AppendText旧value命名实参须改为text；完整剪贴板/测量/输入/运行时RTL仍待，见[048](../batches/048.md)。

- 2026-11-07 · 批次047：源码no-op，002/009按真实继承的SelectedText/Lines/MaxLength更新；009由全待办纠正为部分，不重复计数。程序替换可超过MaxLength，后续用户输入/Undo仍由原生限制；不把内部撤销分支当公开Undo或完整编辑实现，见[047](../batches/047.md)。

- 2026-11-07 · 批次046：源码no-op，消费共同边框和两个标准选择方法；010按实际共享边框子范围纠正当前缺口描述，不重复新增TextBox完成任务。Upper原生初值在重建前明确观测，单线边框公开位按独立EDIT规范化读回核对，完整测量/输入和派生后端仍待，见[046](../batches/046.md)。

- 2026-11-07 · 批次045：007b补CharacterCasing标准属性/枚举与原生消费；不把错误继承链中Rich/Masked间接暴露计为它们的标准成员或真实后端完成。继承TextBoxBase-005a的Modified/程序设文规则，完整Text/选区/撤销/尺寸和运行时RTL仍待；原ID及父任务保持，见[045](../batches/045.md)。

- 2026-11-07 · 批次044：新增007a，007按实际范围由全待办改部分，004交叉引用对齐/水平条范围。HorizontalAlignment原Center/Right整数反置按固定v10.0.11修正，ColumnHeader等按名字重编译保留名字语义，旧二进制/整数存储须复核。首次42项中4例裸native RTL夹具缺少既有Control创建后UpdateStyles步骤而失败；保留原回执，复现该管线后42项通过。生产Control未改，不以样式位对照关闭完整RTL/视觉或007父项，见[044](../batches/044.md)。

- 2026-11-07 · 批次043：新增004a，004由全待办纠正为部分；ScrollBars不再只存值，None纠正旧强制纵条，WordWrap实际影响折行并保留同值布局/缓存副作用。共享基类保留既有Enter规则/高度，默认Left以外对齐、首选尺寸/Designer/AOT/设备和派生后端仍未验收，见[043](../batches/043.md)。

- 2026-11-07 · 批次042：001a迁移直接基类，001改部分；原002/003/005/010范围不重复计数。Multiline、ReadOnly等成员转为继承自TextBoxBase，TextLength为virtual；反射声明位置/基类变化明确记录，二进制/Designer/AOT完整消费仍待。旧SelectionChanged扩展保留在TextBox，不能由其存在计标准API支持，见[042](../batches/042.md)。

- 2026-11-07 · 批次039：005a补只读/密码IME限制及必要原生密码同步，005从全待办纠正为部分，原ReadOnly映射与本批范围分别记账。多行EDIT的显示支持不决定EM_GETPASSWORDCHAR返回值，改用独立原生基线且保留原失败；受限setter缓存配置、设计模式不强制Disable。完整防泄漏/AutoComplete/TextBoxBase仍待，见[039](../batches/039.md)。

- 2026-11-07 · 批次038：003c修复Multiline只存值及无句柄Select丢状态，新增标准MultilineChanged/OnMultilineChanged；选择与modified恢复在base.OnHandleCreated通知后，按固定源码保持顺序。负start/选择属性负值从旧钳制改为标准参数异常，负length有向选择与极值保留。完整层级、尺寸/布局、撤销和IME仍待，见[038](../batches/038.md)。

- 2026-11-07 · 批次037：003b接入派生键判别及对话框路由，8项回归通过；已有AcceptsReturn/AcceptsTab不再仅存值，但完整样式、TextBoxBase/IME/编辑命令和设备仍待，见[037](../batches/037.md)。

- 2026-10-16 · 批次028：完成003a必要消费，真实EDIT程序焦点切换不重复Enter/Leave，保留Selection更新路径。003按实际子范围改部分，全部文本行为/标准继承/键盘/IME/平台未关闭，见[028](../batches/028.md)。

- 2026-10-16 · 批次023：基础Control启用标准UserPaint后，本控件构造显式保留UserPaint=false，使现有系统窗口类过程及原生字体/绘制继续生效。仅属消息与绘制基础的必要衔接，不代表本控件完整Styles、Paint事件或视觉/输入兼容完成，见[023](../batches/023.md)。

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
