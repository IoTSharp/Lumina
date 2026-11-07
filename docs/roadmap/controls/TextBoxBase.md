# TextBoxBase

基线：2026-10-15 · .NET 10.0.11。目标继承：`TextBoxBase : Control`。本页是实施与验收任务，未声称已完成兼容。

## 当前证据与依赖

053完成007b真实首选尺寸计算及008c普通EDIT延期滚动；标准无DC TextRenderer六重载、TextFormatFlags、Padding.Size与初始系统DPI快照为必要依赖，归CNS-006a不重复计数。108新增/5,329全量通过；原生隐藏/可见宿主对照与失败回执见[053](../batches/053.md)。仅实现尺寸计算，不提前启用首选缓存；AutoSize独立默认、PreferredHeight/DefaultSize、控件DPI/请求高度和完整生命周期继续007，RichEdit COM/完整滚动继续008。下述052及以前为历史。

052完成008a三个行查询和008b三个字符位置查询，直接消费真实EDIT消息，保留标准virtual/nonvirtual、参数校验、句柄创建、虚Text快照及16位坐标契约。新增102/102、全量5,221/5,221通过；首跑93/102的9个夹具轨迹错误保留，生产未为此改动。完整008仍含ScrollToCaret未建窗延期与完整端点/派生验收，完整测量/default仍待；固定源码审计确认控件DPI、文本测量、AutoSize独立状态/请求高度与生命周期依赖，未用局部默认或桩关闭。见[052](../batches/052.md)，下述051及以前为历史。

051完成005f固定ClientUtils词边界辅助与009a启用Ctrl+Backspace/Ctrl+Shift+Backspace命令；依赖Control-14c内部Begin/EndUpdate计数，经选择和SetSelectedTextInternal(empty,false)保留native Undo/Modified/通知，不整串重写Text。最终88新增及5,119全量通过；首次三处夹具预期错误及原TRX保留。无窗选择查询仍经虚TextLength读取Text，选择setter异常按固定源码不增加finally；完整编辑/测量/default及派生后端仍待，见[051](../batches/051.md)。下述050及以前为历史。

050完成005e标准Copy/Cut/Paste命令入口和003d的ShortcutsEnabled禁用/只读对齐/系统菜单抑制。77新增及5,031全量通过；剪贴板消息全部被测试WndProc截获，custom菜单只核owner0受限路由，实际剪贴板数据/菜单显示未验。启用时Ctrl+Backspace词删除、完整测量/default和派生真实后端仍待，见[050](../batches/050.md)。下述049及以前为历史。

049完成005d：Clear非virtual且直接调用虚Text setter传null，不预读Text，保留原异常与重入；10项回归通过。继承Control-17e修复parking→Form后的真实编辑/Undo通知及原停车接收者释放，未手工补发事件。完整剪贴板/快捷键、测量和派生后端仍待，见[049](../batches/049.md)。下述048及以前为历史。

048完成005b标准AppendText与005c公开CanUndo/Undo/ClearUndo。非空追加经GetEndPosition/SelectInternal和虚SelectedText，零宽/零高在finally恢复原选择；null/空串不操作。CanUndo不建窗且每次读取native，Undo必要建窗，ClearUndo无窗不操作。普通EDIT范围不实现RichEdit或掩码限制；Copy/Cut/Paste、ShortcutsEnabled、完整测量/default及平台仍待，见[048](../batches/048.md)。

047完成002b的SelectedText标准读取/原生替换、002c的Lines空值/换行契约及必要003c的MaxLength输入限制。SelectedText暂解限制后原生替换，按clearUndo决定修改标志/撤销清理，再恢复私有配置；Lines空文本改为空数组并识别CR/LF/CRLF。完整AppendText/撤销/剪贴板、测量、StringArrayEditor/Designer和派生真实后端仍待，见[047](../batches/047.md)。

046完成007a边框设置/通知/原生创建样式与002a标准SelectAll/DeselectAll：BorderStyle默认Fixed3D，按固定顺序提交状态/UpdateStyles/重建/条件布局/通知；SelectAll只观察一次TextLength，DeselectAll经过虚SelectionLength setter。44项新增回归通过，见[046](../batches/046.md)。完整尺寸、SelectedText/Lines/撤销/剪贴板及真实派生后端仍待。

045完成005a：标准Modified/ModifiedChanged、native getter观测通知、setter先getter及EN_UPDATE同步；Text/WindowText按固定源码仅在程序native提交期间抑制反射通知，最后清native modified位，由后续getter观测通知。20项新增和4,782项全量通过，见[045](../batches/045.md)。完整撤销/选区编辑/Lines/AppendText、BorderStyle/尺寸及派生真实后端仍待。

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
| TextBoxBase-002 | P1 | 🟡 | 042迁入共享Text/选择，046的002a补标准选择方法，047的002b/002c补SelectedText原生替换与Lines空值/CR/LF/CRLF。完整端点/IME/辅助技术、派生覆盖及StringArrayEditor/Designer仍待，不以有限UTF-16案例关闭父项。 |
| TextBoxBase-002a | P1 | ✅ | 批次046：标准非virtual SelectAll/DeselectAll；SelectAll只读取一次虚TextLength并直接进入private protected virtual SelectInternal，DeselectAll调用虚SelectionLength setter。已有Select校验和无窗缓存/EM_SETSEL/创建重建恢复共用入口。20项回归含虚调用/原异常、CRLF/emoji、真实父映射及选区/只读/Modified保持通过；不含SelectedText、Lines、完整端点/输入或UIA。 |
| TextBoxBase-002b | P1 | ✅ | 批次047：标准virtual SelectedText及内部virtual SetSelectedTextInternal；getter消费私有选择查询和虚Text.Substring，无窗不建句柄，保留固定源码有向负长度异常。setter创建真实EDIT，null归空，暂解EM_LIMITTEXT后EM_REPLACESEL；clearUndo时清native modified/undo，再恢复私有_maxLength，false保留可撤销编辑。必要长度依赖见003c；原生通知、只读程序替换、UTF-16、精确消息序列及独立EDIT受限Undo对照见047，不关闭完整002/005。 |
| TextBoxBase-002c | P1 | ✅ | 批次047：Lines getter只读取一次虚Text，空文本返回空数组，CR/LF/CRLF分行且保留尾部空行；setter null/空数组/null元素经虚Text及Environment.NewLine处理。补Appearance/Hidden/Mergable(false)/Localizable/AllowNull声明，虚调用/原异常、真实父映射/原生文本/通知/重建有限场景见047；StringArrayEditor和Designer往返继续011。 |
| TextBoxBase-003 | P1 | 🟡 | 042～047共享设置/通知和MaxLength范围保留；050的003d补ShortcutsEnabled禁用/系统菜单抑制限定契约，051的009a补启用词删除命令。其余默认/元数据、完整菜单/快捷键输入及派生覆盖仍待；ES_WANTRETURN等完整CreateParams差异保留，交叉引用不重复计数。 |
| TextBoxBase-003a | P1 | ✅ | 批次043：AcceptsTab=false、标准AcceptsTabChanged/虚On*、Events槽/同值/重入/原异常；WordWrap=true及Localizable，变更重建，所有赋值清本控件首选缓存，AutoSize时含同值参与父布局。默认AUTOH/AUTOV、多行折行清AUTOH，不再强制纵条；17项回归含真实EDIT折行/状态通过。完整003/004/007父项不关闭。 |
| TextBoxBase-003b | P1 | ✅ | 批次044：HideSelection=true、Behavior/DefaultValue元数据、HideSelectionChanged/虚On*与Events槽；同值不操作，合法变化保存后RecreateHandle再通知，false设置ES_NOHIDESEL。14项回归含独立EDIT样式、通知/重入/原异常/CanRaiseEvents及选择/modified/只读/密码保存通过。物理选区显示、RichEdit派生覆盖和完整003/004父项不关闭。 |
| TextBoxBase-003c | P1 | ✅ | 批次047：SelectedText必要前置MaxLength标准virtual属性，默认32767/Behavior/Localizable，负值抛value异常；同值no-op、无窗缓存，有窗和创建后经internal virtual UpdateMaxLength提交EM_LIMITTEXT。0解除限制，现有/程序Text不截断、不重建句柄；真实字符输入、消息次数/状态和重建限制回读见047。不含派生专有限制、剪贴板/完整输入或003父项。 |
| TextBoxBase-003d | P1 | ✅ | 批次050：标准virtual ShortcutsEnabled默认true及Behavior/DefaultValue；setter含同值赋值惰性初始化固定14项表。ProcessCmdKey先父路由并保消息修改/结果，再消费虚getter；禁用精确表及附加Shift，不扩展Alt/CtrlI，私有只读态另拦精确CtrlL/R/E/J。WM_CONTEXTMENU禁用时跳过native系统菜单，客户区内保留既有custom菜单路由/键盘中心。62例通过；自定义菜单仅owner0受限路由，真实显示/物理输入和启用时Ctrl+Backspace词删除不在本子范围，不关闭003/005/009。 |
| TextBoxBase-004 | P1 | 🟡 | 042迁入ReadOnlyChanged/MultilineChanged，043的003a补AcceptsTabChanged，044的003b补HideSelectionChanged，045的005a补ModifiedChanged，046的007a补BorderStyleChanged标准事件槽/虚通知。完整通知时序/元数据及派生覆盖仍待，事件子范围交叉引用不重复计数、不关闭父项。 |
| TextBoxBase-005 | P1 | 🟡 | 045～050修改标志/替换/追加/撤销/Clear/剪贴板入口保留；051的005f补固定词边界辅助，009a另记使用该辅助的可撤销词删除命令。真实剪贴板数据/限制、完整快捷键/输入合并、派生覆盖与全部编辑重入仍待；有限普通EDIT范围不关闭父项。 |
| TextBoxBase-005a | P1 | ✅ | 批次045：Modified默认false、非virtual及Behavior/Browsable(false)/隐藏序列化元数据，Events槽/虚OnModifiedChanged；getter读取真实EDIT状态，差异时保存后通知，setter先消费getter再提交native/缓存/通知。EN_UPDATE同步，程序Text的native提交期间抑制反射通知，TextChanged处理器内native编辑仍可通知；最后直接清native位并延迟至getter通知，创建/重建和销毁观测保存状态。20项回归与全量通过；不含撤销/剪贴板/完整编辑或真实派生后端。 |
| TextBoxBase-005b | P1 | ✅ | 批次048：标准非virtual AppendText(string? text)，null/空串直接返回；保存私有选择，GetEndPosition消费虚TextLength（已有句柄加1），SelectInternal(end,end,end)后经虚SelectedText进入047替换，不整串重写Text。finally在Width或Height为0时Select恢复原范围，虚调用原异常保留。22项回归覆盖无窗/有窗、UTF-16/CRLF、只读程序追加、输入限制、modified/undo及零尺寸；不含派生真实后端、物理输入或完整005。 |
| TextBoxBase-005c | P1 | ✅ | 批次048：标准非virtual CanUndo及Behavior/Browsable(false)/Hidden，短路无窗且每次EM_CANUNDO原生查询；Undo经Handle必要建窗后发EM_UNDO，ClearUndo只在有窗时EM_EMPTYUNDOBUFFER。16项回归含消息参数、原建窗异常、真实缓冲/通知/虚Text setter不调用及同配置独立EDIT的MaxLength受限Undo；不实现剪贴板、RichEdit重做/动作名或Masked专有限制，不关闭005。 |
| TextBoxBase-005d | P1 | ✅ | 批次049：标准public非virtual Clear直接执行Text=null，经虚Text setter且不先读getter；重复调用仍交虚setter，原异常/有限重入保留。10项回归含未建窗空/Unicode、TextChanged重入/原异常，以及有真实父的只读/可编辑EDIT清空全文/单次通知/清撤销且保持句柄和MaxLength通过。掩码/富文本派生专有行为、剪贴板及完整005仍待。 |
| TextBoxBase-005e | P1 | ✅ | 批次050：标准public非virtual无参void Copy/Cut/Paste，经Handle必要建窗并分别发送WM_COPY/WM_CUT/WM_PASTE，wParam/lParam0、忽略返回值，不读写虚Text/SelectedText等托管成员，不因ReadOnly或ShortcutsEnabled而在托管入口短路。15例覆盖签名/句柄/零参数/原建窗异常重试/有限重入；全部消息在测试WndProc截获，不访问系统剪贴板，只关闭命令入口契约。真实数据/格式/限制、派生专有语义和完整005仍待。 |
| TextBoxBase-005f | P1 | ✅ | 批次051：仅内部ClientUtils.GetWordBoundaryStart固定v10.0.11词边界辅助；字母/数字/NonSpacingMark为word，连接标点为nonword，末尾非word及首位非word按固定规则，UTF-16 surrogate终止扫描。52例硬预期覆盖标点/空白/CRLF/CJK/Unicode分类/成对与不成对代理码元、负值/越界及原异常；无公共API扩面。命令整合另归009a，不关闭完整005或派生输入。 |
| TextBoxBase-006 | P1 | 🟡 | 042～048的EN_CHANGE/EN_UPDATE、程序设文守卫及停车期间通知保留；049通过Control-17e保留原停车接收者并按来源HWND反射，闭环048的parking→Form编辑/Undo通知缺口。原反射父Form销毁/重建的完整关系仍归Control-17；标准基类没有SelectionChanged，旧TextBox扩展保留，完整IME/粘贴/验证/失焦和RichEdit通知仍待。 |
| TextBoxBase-007 | P1 | 🟡 | 042颜色/主题与24/48最小原生高度、046的007a边框范围保留；053的007b补标准首选尺寸计算和文字失效。AutoSize独立默认、PreferredHeight/DefaultSize、首选缓存启用、控件DPI/请求高度及Font/Multiline/SetBoundsCore完整尺寸生命周期仍待，旧高度差异保留；计算完成不关闭父项。 |
| TextBoxBase-007a | P1 | ✅ | 批次046：BorderStyle默认Fixed3D、Appearance/DefaultValue/DispId(-504)及合法枚举异常，变化保存后UpdateStyles、RecreateHandle，再在AutoSize条件父布局事务内通知；组件Events槽与虚OnBorderStyleChanged，同值no-op。CreateParams清WS_BORDER/CLIENTEDGE后按None/FixedSingle/Fixed3D选择真实请求。24项回归含缓存/布局/异常/重入、独立EDIT边框与状态恢复通过；原生EDIT规范化读回与创建请求分开，不含完整尺寸或视觉验收。 |
| TextBoxBase-007b | P1 | ✅ | 批次053：GetPreferredSizeCore按SizeFromClientSize/四侧Padding、非None高3及FixedSingle宽高2兼容补偿扣减约束，经NoPrefix及单行/多行WordWrap的标准GDI测量，再保留FontHeight下界；OnTextChanged先清自身缓存再通知，不重排父。26例含虚调用/原异常、Unicode/CRLF/字面&、边框/约束/字体和真实EDIT状态保持通过。只关闭计算，不提前启用首选缓存或改变AutoSize/PreferredHeight/DefaultSize与24/48生命周期。 |
| TextBoxBase-008 | P2 | 🟡 | 052的008a/008b六个标准行/位置查询保留；053的008c补普通EDIT未建窗延期及空WindowText短路。RichEdit OLE/文档滚动分支、完整滚动/RTL/DPI端点、真实派生后端与辅助技术仍待，不以普通EDIT限定范围关闭父项。 |
| TextBoxBase-008a | P2 | ✅ | 批次052：GetLineFromCharIndex为virtual且不加索引校验，GetFirstCharIndexFromLine非virtual且先拒绝负lineNumber，CurrentLine非virtual发送原生-1哨兵；经Handle必要建窗，不读取托管Text/Selection替代原生查询。53例含签名/消息参数/原结果、创建失败重试、UTF-16/CRLF/尾空行、caret行、真实软折行、状态通知保持及有限重入通过。完整008/派生/物理输入和平台未验。 |
| TextBoxBase-008b | P2 | ✅ | 批次052：GetCharFromPosition先虚Text快照再虚索引；GetCharIndexFromPosition先Handle/EM_CHARFROMPOS、取无符号低16位，再按虚Text长度夹值；GetPositionFromCharIndex负值直接Empty，不负先虚Text.Length校验，合法才建窗/EM_POSFROMCHAR并有符号16位解坐标。49例含精确packing/原异常/null/快照/有限重入与真实EDIT字符回读、状态保持通过。固定16位原生索引限制保留，不计完整命中/DPI/派生或辅助技术验收。 |
| TextBoxBase-008c | P2 | ✅ | 批次053：普通EDIT无句柄ScrollToCaret只记延期位，不建窗；有窗以WindowText判空后发EM_SCROLLCARET，创建通知/限制/修改位/选区恢复后消费延期，调用返回才清位，保留失败/有限重入。20例含原异常/重试/重建/原生文本权威及真实40行EDIT滚动/Undo/Modified/通知保持。独立EDIT隐藏首行0、可见未聚焦/聚焦均37，受控有效顶层宿主下Lumina为37；RichEdit接口探测/COM分支、可见命中/物理设备与完整008未验。 |
| TextBoxBase-009 | P2 | 🟡 | 042共享Tab/编辑键/只读IME限制和050的003d/005e保留；051的009a补启用词删除限定命令。真实剪贴板/密码完整限制、全部快捷键/菜单、RTL、物理组合输入/候选窗口与跨平台原生集成仍待，不重复计数或关闭父项。 |
| TextBoxBase-009a | P2 | ✅ | 批次051：父命令路由及050守卫后，仅非只读的精确Ctrl+Back/ Ctrl+Shift+Back执行；有选区经SetSelectedTextInternal(empty,false)，无选区非起点经005f词边界和Control-14c成对redraw调整选择，再内部替换保留Undo。父返回true仍执行并保留消息修改；起点消费不建窗，私有选择查询仍消费虚TextLength。26例覆盖修饰键/禁用/隐藏ReadOnly、原异常/有限重入、真实EDIT文本/选择/一次通知/Modified/限制/Undo及消息顺序。选择setter抛错按固定源码没有finally恢复；物理输入/可见绘制、完整009和Rich/Masked专有语义未验。 |
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

- 2026-11-07 · 批次053：007b只补真实首选尺寸计算，008c只补普通EDIT延期/空文本滚动，完整007/008保持部分；缺少的标准Padding.Size在首次编译后补齐，首构建、首104/105和第二107/108原回执保留。独立原生EDIT对照推翻“必须聚焦”假设，改有效可见顶层夹具而未改生产/原滚动状态硬期望；最终108新增/5,329全量通过，见[053](../batches/053.md)。

- 2026-11-07 · 批次052：只新增008a/008b，纠正008中六个查询仍全待办的当前描述，完整父项保持部分。102新增/5,221全量及固定双目标Release通过；首93/102九例轨迹错误来自初始化Text setter及回调setter的合法虚getter读取，清初始化轨迹并保留回调末次读取，原字符/异常硬预期不改、生产不改。完整测量/default的真实依赖已核，不将PreferredHeight/DefaultSize或普通EDIT查询当完整布局/派生实现，见[052](../batches/052.md)。

- 2026-11-07 · 批次051：005f只计固定词边界辅助，009a只计使用它的启用词删除命令，必要redraw辅助另归Control-14c。88新增/5,119全量及固定双目标Release通过；首轮85/88三个夹具预期错误已按固定无窗TextLength调用链纠正，原回执不改。完整003/005/009等父项保持部分，见[051](../batches/051.md)。

- 2026-11-07 · 批次050：仅新增003d/005e，完整003/005/009保持部分。Copy/Cut/Paste直接转发真实HWND消息，测试截获命令以保护用户剪贴板；ShortcutsEnabled按实际固定14键表（CtrlJ而非摘要CtrlI）、父先调用/虚getter及只读精确对齐处理。custom菜单受限夹具仅证路由，完整启用编辑/菜单显示/派生/平台未验；77新增与5,031全量通过，见[050](../batches/050.md)。

- 2026-11-07 · 批次049：只新增005d，Clear由Text=empty改为固定标准的虚Text=null，去掉无必要new（CLR签名/非virtual保持）。10项新增通过；006交叉引用Control-17e，不重复计数或关闭完整父项。首跑11/13中的两个失败来自重挂通知，不是Clear夹具；原回执及最终证据见[049](../batches/049.md)。

- 2026-11-07 · 批次048：只新增005b/005c，完整005继续部分；AppendText参数名由value对齐标准text（旧value命名实参需改），nullable标记不改变CLR参数类型，null由异常改为no-op。首次38例37通过，一例误以为空撤销缓冲时EM_UNDO必返回0；独立原生对照与最后结果见[048](../batches/048.md)，生产未为夹具改变。原任务/固定分母不变，剪贴板/完整编辑/测量和派生后端仍待。

- 2026-11-07 · 批次047：新增002b/002c/003c，完整002/003/005父项保持部分；原Lines空Text返回单个空行及不识别独立CR的行为按固定源码修正。新增SelectedText及必要MaxLength，无新公共概念。首次65例有3个夹具失败：AllowNull反射位置两例、较小输入限制下Undo期望一例，独立原生对照和最终结果见[047](../batches/047.md)。StringArrayEditor元数据/Designer、完整编辑/测量和派生后端仍待；原ID/固定分母保留。

- 2026-11-07 · 批次046：新增002a/007a，002/004/007按真实子范围交叉引用，完整父项保持部分。首次40/44中三例错误要求原生EDIT保留公开WS_BORDER，另一例错误比较不同ReflectedType的MethodInfo对象；补匹配配置的独立EDIT，保留请求位硬断言并确认原生规范化后读回0，反射改核DeclaringType/Module/MetadataToken/签名。第二轮41/44暴露同三例启用Upper后误期待小写初值，改在重建前硬断言原生大写并核重建保持。原失败JSON/TRX保留，生产实现未为夹具改变；最终44/44。完整测量/编辑/派生后端仍待，见[046](../batches/046.md)。

- 2026-11-07 · 批次045：005a补Modified/ModifiedChanged、EN_UPDATE和程序设文/销毁观测衔接，004/006交叉引用不重复新增事件任务。核固定源码纠正最初“EN_CHANGE后同步”理解：实际为EN_UPDATE分支；程序Text清native位后缓存由getter观测，不用setter提前通知。首次两例父Form无HWND而进入停车窗，补真实父句柄/映射硬断言后原期望通过，生产不为夹具改写。完整005/编辑/尺寸/派生后端仍部分，见[045](../batches/045.md)。

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
