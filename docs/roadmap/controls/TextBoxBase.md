# TextBoxBase

基线：2026-10-15 · .NET 10.0.11。目标继承：`TextBoxBase : Control`。本页是实施与验收任务，未声称已完成兼容。

## 当前证据与依赖

062本类源码no-op；Rich的003b/004b沿现有句柄/选择/RTF恢复和原生消息连接，不新增共享基类完成任务。110新增/5,959全量首轮与固定双目标Release通过；完整基类/Masked模型与平台父项保持。见[062](../batches/062.md)。下述061及以前为历史。

061本类源码no-op；Rich的003a/004a复用现有句柄/选择/RTF恢复连接，不新增共享基类完成任务。67新增/5,849全量首轮通过；完整基类、真实Masked模型与平台父项保持，见[061](../batches/061.md)。下述060及以前为历史。

060本类源码no-op；Rich的002c/011a复用现有选择/Modified/句柄连接，增加RTF字符串流与格式缓存，不新增共享基类完成任务。销毁后新plain缓存的修改位修复在Rich生产中，完整共享成员/派生模型与父项保持，见[060](../batches/060.md)。下述059及以前为历史。

059为Rich纯文本恢复提供必要内部连接：去掉OnHandleCreated额外选择恢复，保留CreateHandle收尾，并向同库Rich开放原恢复函数使其加载文本后恢复。该顺序与固定053参考一致，普通EDIT全部回归通过，不新增基类完成任务；Rich专有流/模型归其002a/002b，完整基类与Masked仍待，见[059](../batches/059.md)。下述058及以前为历史。

058基类源码no-op；RichTextBox已按001a直接继承并使用真实系统RichEdit，必要专有默认见其007a。MaskedTextBox仍错误继承TextBox，Rich的专有Text/逻辑长度/选区/测量及完整基类仍待；不能由一个真实派生原生基础关闭001父项。96新增/5,664全量与固定双目标Release通过，见[058](../batches/058.md)。下述057及以前为历史。

057完成002d有句柄原生TextLength/无句柄虚Text和003h必要编辑元数据。41新增/5,568全量首轮及固定双目标Release通过，旧测试未改；原生长度不读虚Text、不缓存，保持编辑状态。类默认事件/绑定、隐藏Paint/选区声明与两组分类只恢复限定设计契约，不补完整绑定/Designer/可见绘制。DefaultCursor、ToString、其余元数据、真实派生及平台仍待，见[057](../batches/057.md)。下述056及以前为历史。

056完成003f构造点击样式/专用左键消息链及003g必要绘图/间距成员元数据。构造关闭StandardClick/StandardDoubleClick/UseTextForAccessibility/UserPaint，左双击消息先置私有位；OnMouseUp按验证/原生命中生成点击或双击并转发原参数，保留固定异常/清位顺序。隐藏成员继续直转Control状态/事件槽。46新增/5,527全量及Release固定双目标通过；两次独立WindowFromPoint检查均no-hit，只证明负向路由，不计正向可见点击/物理输入验收。完整003与Designer/AOT/平台保持未完成，见[056](../batches/056.md)。下述055及以前为历史。

055完成003e创建参数限定范围：在消费base.CreateParams前设置ApplyThemingImplicitly，多行不再额外强加ES_WANTRETURN；保留Style/ExStyle既有受保护扩展。31例核请求组合/缓存复用、前置顺序/原异常、真实EDIT/独立native对照和AcceptsReturn/直接CR输入通过。首轮一例句柄预期错误按既有ReadOnly→IME→PasswordChar建窗链纠正，原样式硬断言不改，生产未为此改动；完整主题、点击/双击、其余样式/元数据与平台仍待，见[055](../batches/055.md)。下述054及以前为历史。

054完成007c的标准PreferredHeight/DefaultSize与必要控件DPI度量、007d的独立AutoSize/请求高度生命周期、007e的首选缓存启用及失效。单行默认AutoSize=true，默认100×PreferredHeight；取消旧24/48原生高度下限，保存用户请求高度供关闭AutoSize/切换多行时恢复。通用布局状态保持false，Table/Flow消费内部状态；84个新增用例通过。完整缩放/跨屏DPI消息、其余元数据/样式、Rich/Masked真实后端与平台验收仍待，见[054](../batches/054.md)。下述053及以前为历史。

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

🟡 批次042建立abstract TextBoxBase : Control及TextBox : TextBoxBase，迁入038/039等已有共享EDIT行为。内部无参构造按固定10.0.11保持internal；TextBox保留专属密码/Enter规则和非标准SelectionChanged扩展。该批历史时两个派生仍经TextBox；058已恢复Rich直接层级与系统RichEdit后端，Masked直接层级/真实模型仍待。062收尾纠正此处当前叙述滞后，不改变042完成范围。新增8项基类回归及全量4,654项通过，只关闭001a限定提取范围，完整基类不关闭，见[042](../batches/042.md)。

源码定位：[TextBoxBase.cs](../../../Lumina.Forms/TextBoxBase.cs)；共同问题见 [Control](../../../Lumina.Forms/Control.cs) 和 [Win32 interop](../../../Lumina.Forms/Win32.cs)。先决依赖：Control.Text/键盘/IME/焦点与验证、剪贴板、字体和 native handle 重建。

公开成员（含继承的全部属性、事件和方法）逐项见 [TextBoxBase API 清单](../api/TextBoxBase.md)；共同行为、AOT、设计器与计数方法见 [基础任务](../foundations.md)。本表列出具体实现/验收动作，API 清单中余下成员也必须逐条关闭，不能把表外成员当作已支持。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 先修层级/阻塞，P1 完成常用契约，P2 补边界/高级场景，P3 完成平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| TextBoxBase-001 | P0 | 🟡 | 042完成001a标准基类/TextBox直接继承；058的RichTextBox-001a/007a另记Rich直接继承、真实RichEdit及必要默认，不重复计数。MaskedTextBox标准直接层级/真实模型、Rich专有Text/选择/测量覆盖及完整共享成员仍待；ISupportInitialize按各类型基线放置，不由派生首范围关闭父项。 |
| TextBoxBase-001a | P0 | ✅ | 批次042：建立abstract TextBoxBase : Control与internal无参构造，TextBox直接继承；迁移现有文本/选择、多行/只读、Tab、颜色/主题和EDIT句柄状态。TextLength为virtual，密码/Enter及既有SelectionChanged扩展保留TextBox。8项新增基类引用/真实重建/事件/派生hook回归与全量通过；不含另外两个派生直接层级或完整编辑能力。 |
| TextBoxBase-002 | P1 | 🟡 | 042～047共享Text/选择、SelectedText/Lines保留；057的002d补有窗原生TextLength及无窗虚Text分支，003h另补必要选区元数据。完整端点/IME/辅助技术、派生覆盖及StringArrayEditor/Designer仍待，不以有限UTF-16案例关闭父项或重复计数。 |
| TextBoxBase-002a | P1 | ✅ | 批次046：标准非virtual SelectAll/DeselectAll；SelectAll只读取一次虚TextLength并直接进入private protected virtual SelectInternal，DeselectAll调用虚SelectionLength setter。已有Select校验和无窗缓存/EM_SETSEL/创建重建恢复共用入口。20项回归含虚调用/原异常、CRLF/emoji、真实父映射及选区/只读/Modified保持通过；不含SelectedText、Lines、完整端点/输入或UIA。 |
| TextBoxBase-002b | P1 | ✅ | 批次047：标准virtual SelectedText及内部virtual SetSelectedTextInternal；getter消费私有选择查询和虚Text.Substring，无窗不建句柄，保留固定源码有向负长度异常。setter创建真实EDIT，null归空，暂解EM_LIMITTEXT后EM_REPLACESEL；clearUndo时清native modified/undo，再恢复私有_maxLength，false保留可撤销编辑。必要长度依赖见003c；原生通知、只读程序替换、UTF-16、精确消息序列及独立EDIT受限Undo对照见047，不关闭完整002/005。 |
| TextBoxBase-002c | P1 | ✅ | 批次047：Lines getter只读取一次虚Text，空文本返回空数组，CR/LF/CRLF分行且保留尾部空行；setter null/空数组/null元素经虚Text及Environment.NewLine处理。补Appearance/Hidden/Mergable(false)/Localizable/AllowNull声明，虚调用/原异常、真实父映射/原生文本/通知/重建有限场景见047；StringArrayEditor和Designer往返继续011。 |
| TextBoxBase-002d | P1 | ✅ | 批次057：TextLength保持public virtual只读/Browsable(false)，有句柄直接GetWindowTextLengthW而不读虚Text，无窗仍Text.Length且不建窗。21例含原异常/非法null、UTF-16/CRLF/native NUL截断、实时原生编辑长度、选区/Modified/Undo/通知保持、重建/销毁及一次有限重入通过；不计Rich/Masked真实模型、完整文本端点或002父项。 |
| TextBoxBase-003 | P1 | 🟡 | 042～056设置/通知、快捷键、创建请求、构造点击与八项绘图/间距声明保留；057的003h补类默认事件/绑定、Paint转发、选区隐藏元数据及Multiline/ReadOnly分类。其余默认/元数据、DefaultCursor/ToString、Designer/editor、完整菜单/快捷键输入、正向可见点击/物理设备与派生覆盖仍待；完整CreateParams/主题与父项不由有限范围关闭，交叉引用不重复计数。 |
| TextBoxBase-003a | P1 | ✅ | 批次043：AcceptsTab=false、标准AcceptsTabChanged/虚On*、Events槽/同值/重入/原异常；WordWrap=true及Localizable，变更重建，所有赋值清本控件首选缓存，AutoSize时含同值参与父布局。默认AUTOH/AUTOV、多行折行清AUTOH，不再强制纵条；17项回归含真实EDIT折行/状态通过。完整003/004/007父项不关闭。 |
| TextBoxBase-003b | P1 | ✅ | 批次044：HideSelection=true、Behavior/DefaultValue元数据、HideSelectionChanged/虚On*与Events槽；同值不操作，合法变化保存后RecreateHandle再通知，false设置ES_NOHIDESEL。14项回归含独立EDIT样式、通知/重入/原异常/CanRaiseEvents及选择/modified/只读/密码保存通过。物理选区显示、RichEdit派生覆盖和完整003/004父项不关闭。 |
| TextBoxBase-003c | P1 | ✅ | 批次047：SelectedText必要前置MaxLength标准virtual属性，默认32767/Behavior/Localizable，负值抛value异常；同值no-op、无窗缓存，有窗和创建后经internal virtual UpdateMaxLength提交EM_LIMITTEXT。0解除限制，现有/程序Text不截断、不重建句柄；真实字符输入、消息次数/状态和重建限制回读见047。不含派生专有限制、剪贴板/完整输入或003父项。 |
| TextBoxBase-003d | P1 | ✅ | 批次050：标准virtual ShortcutsEnabled默认true及Behavior/DefaultValue；setter含同值赋值惰性初始化固定14项表。ProcessCmdKey先父路由并保消息修改/结果，再消费虚getter；禁用精确表及附加Shift，不扩展Alt/CtrlI，私有只读态另拦精确CtrlL/R/E/J。WM_CONTEXTMENU禁用时跳过native系统菜单，客户区内保留既有custom菜单路由/键盘中心。62例通过；自定义菜单仅owner0受限路由，真实显示/物理输入和启用时Ctrl+Backspace词删除不在本子范围，不关闭003/005/009。 |
| TextBoxBase-003e | P1 | ✅ | 批次055：CreateParams在base前设置ApplyThemingImplicitly，多行仅加ES_MULTILINE而不强加ES_WANTRETURN；保留Style/ExStyle历史扩展和现有其他请求位。31例覆盖组合、参数复用/状态切换、投影前标志/原异常、真实与独立EDIT、AcceptsReturn和直接CR输入；首轮一例ReadOnly=false经IME/PasswordChar建窗的夹具假设已纠正，原样式硬断言保留。构造点击代码另见056的003f；正向可见点击、主题视觉和完整003仍未验，055原批次历史保持。 |
| TextBoxBase-003f | P1 | ✅ | 批次056：构造关闭StandardClick/StandardDoubleClick/UseTextForAccessibility/UserPaint，保留单行FixedHeight/Selectable；左双击在base.WndProc前置私有标记，OnMouseUp仅左键经验证取消/WindowFromPoint守卫生成Click→MouseClick或先清位再DoubleClick→MouseDoubleClick，随后清位并base转发原参数；非左/null与原异常时序保持。Click显式new/true/Always并共用基类事件槽。23例含标记前置/取消/负命中/同槽订退及Control finally清捕获通过；两次原生命中均false，正向可见点击、物理输入及完整003未验。 |
| TextBoxBase-003g | P1 | ✅ | 批次056：BackgroundImage/BackgroundImageLayout override及两Changed new事件均Browsable(false)/EditorBrowsable(Never)，Padding及PaddingChanged另DSV.Hidden/事件Layout分类；MouseClick为new/true/Always，DoubleBuffered为protected override/Never。八项均直转Control，不新增状态/事件槽。23例声明/TypeDescriptor、跨引用订退/共享状态、有限重入/原异常、间距钳值/借用图片寿命通过；其余元数据、Designer序列化往返/主题视觉/AOT和完整003未验。 |
| TextBoxBase-003h | P1 | ✅ | 批次057：DefaultEvent(TextChanged)/DefaultBindingProperty(Text)，Paint new事件隐藏并直转base.Paint；SelectionStart/Length为Appearance/Browsable(false)/DSV.Hidden，Multiline/ReadOnly及两Changed补Behavior/Property Changed分类。20例反射/TypeDescriptor默认事件真实订退、选区描述符读写、Paint共槽/跨引用退订/原参数异常/一次重入/借用Graphics通过。保留Control.OnPaint既有直接调用，不新增绑定引擎或Designer；完整003/可见绘制/设计器仍未验。 |
| TextBoxBase-004 | P1 | 🟡 | 042～046标准Changed事件槽/虚通知保留，057的003h补MultilineChanged/ReadOnlyChanged分类及默认TextChanged/Paint声明。完整通知时序/其余元数据及派生覆盖仍待，事件子范围交叉引用不重复计数、不关闭父项。 |
| TextBoxBase-005 | P1 | 🟡 | 045～050修改标志/替换/追加/撤销/Clear/剪贴板入口保留；051的005f补固定词边界辅助，009a另记使用该辅助的可撤销词删除命令。真实剪贴板数据/限制、完整快捷键/输入合并、派生覆盖与全部编辑重入仍待；有限普通EDIT范围不关闭父项。 |
| TextBoxBase-005a | P1 | ✅ | 批次045：Modified默认false、非virtual及Behavior/Browsable(false)/隐藏序列化元数据，Events槽/虚OnModifiedChanged；getter读取真实EDIT状态，差异时保存后通知，setter先消费getter再提交native/缓存/通知。EN_UPDATE同步，程序Text的native提交期间抑制反射通知，TextChanged处理器内native编辑仍可通知；最后直接清native位并延迟至getter通知，创建/重建和销毁观测保存状态。20项回归与全量通过；不含撤销/剪贴板/完整编辑或真实派生后端。 |
| TextBoxBase-005b | P1 | ✅ | 批次048：标准非virtual AppendText(string? text)，null/空串直接返回；保存私有选择，GetEndPosition消费虚TextLength（已有句柄加1），SelectInternal(end,end,end)后经虚SelectedText进入047替换，不整串重写Text。finally在Width或Height为0时Select恢复原范围，虚调用原异常保留。22项回归覆盖无窗/有窗、UTF-16/CRLF、只读程序追加、输入限制、modified/undo及零尺寸；不含派生真实后端、物理输入或完整005。 |
| TextBoxBase-005c | P1 | ✅ | 批次048：标准非virtual CanUndo及Behavior/Browsable(false)/Hidden，短路无窗且每次EM_CANUNDO原生查询；Undo经Handle必要建窗后发EM_UNDO，ClearUndo只在有窗时EM_EMPTYUNDOBUFFER。16项回归含消息参数、原建窗异常、真实缓冲/通知/虚Text setter不调用及同配置独立EDIT的MaxLength受限Undo；不实现剪贴板、RichEdit重做/动作名或Masked专有限制，不关闭005。 |
| TextBoxBase-005d | P1 | ✅ | 批次049：标准public非virtual Clear直接执行Text=null，经虚Text setter且不先读getter；重复调用仍交虚setter，原异常/有限重入保留。10项回归含未建窗空/Unicode、TextChanged重入/原异常，以及有真实父的只读/可编辑EDIT清空全文/单次通知/清撤销且保持句柄和MaxLength通过。掩码/富文本派生专有行为、剪贴板及完整005仍待。 |
| TextBoxBase-005e | P1 | ✅ | 批次050：标准public非virtual无参void Copy/Cut/Paste，经Handle必要建窗并分别发送WM_COPY/WM_CUT/WM_PASTE，wParam/lParam0、忽略返回值，不读写虚Text/SelectedText等托管成员，不因ReadOnly或ShortcutsEnabled而在托管入口短路。15例覆盖签名/句柄/零参数/原建窗异常重试/有限重入；全部消息在测试WndProc截获，不访问系统剪贴板，只关闭命令入口契约。真实数据/格式/限制、派生专有语义和完整005仍待。 |
| TextBoxBase-005f | P1 | ✅ | 批次051：仅内部ClientUtils.GetWordBoundaryStart固定v10.0.11词边界辅助；字母/数字/NonSpacingMark为word，连接标点为nonword，末尾非word及首位非word按固定规则，UTF-16 surrogate终止扫描。52例硬预期覆盖标点/空白/CRLF/CJK/Unicode分类/成对与不成对代理码元、负值/越界及原异常；无公共API扩面。命令整合另归009a，不关闭完整005或派生输入。 |
| TextBoxBase-006 | P1 | 🟡 | 042～048的EN_CHANGE/EN_UPDATE、程序设文守卫及停车期间通知保留；049通过Control-17e保留原停车接收者并按来源HWND反射，闭环048的parking→Form编辑/Undo通知缺口。原反射父Form销毁/重建的完整关系仍归Control-17；标准基类没有SelectionChanged，旧TextBox扩展保留，完整IME/粘贴/验证/失焦和RichEdit通知仍待。 |
| TextBoxBase-007 | P1 | 🟡 | 046边框与053首选计算保留；054的007c/007d/007e补标准单行高度/默认、独立AutoSize与请求高度、缓存启用和必要失效，取消旧24/48原生下限。完整ScaleControl/缩放、跨屏DPI消息/字体重缩放、剩余元数据/样式和真实派生默认仍待，不能由有限尺寸范围关闭父项。 |
| TextBoxBase-007a | P1 | ✅ | 批次046：BorderStyle默认Fixed3D、Appearance/DefaultValue/DispId(-504)及合法枚举异常，变化保存后UpdateStyles、RecreateHandle，再在AutoSize条件父布局事务内通知；组件Events槽与虚OnBorderStyleChanged，同值no-op。CreateParams清WS_BORDER/CLIENTEDGE后按None/FixedSingle/Fixed3D选择真实请求。24项回归含缓存/布局/异常/重入、独立EDIT边框与状态恢复通过；原生EDIT规范化读回与创建请求分开，不含完整尺寸或视觉验收。 |
| TextBoxBase-007b | P1 | ✅ | 批次053：GetPreferredSizeCore按SizeFromClientSize/四侧Padding、非None高3及FixedSingle宽高2兼容补偿扣减约束，经NoPrefix及单行/多行WordWrap的标准GDI测量，再保留FontHeight下界；OnTextChanged先清自身缓存再通知，不重排父。26例含虚调用/原异常、Unicode/CRLF/字面&、边框/约束/字体和真实EDIT状态保持通过。只关闭计算，不提前启用首选缓存或改变AutoSize/PreferredHeight/DefaultSize与24/48生命周期。 |
| TextBoxBase-007c | P1 | ✅ | 批次054：标准只读非virtual PreferredHeight=FontHeight，非None另加真实DPI边框高×4+3，DefaultSize=100×PreferredHeight；不测Text/多行/换行/间距。必要Control独立DPI初值/真实PMv2 HWND读取、公开DeviceDpi线程投影及线程感知边框度量封装在库内。33例覆盖签名/元数据、3边框、3线程context×3内部DPI、字体/无窗和native编辑状态；只关闭该度量与读取范围，不计完整DPI缩放/跨屏事件链。 |
| TextBoxBase-007d | P1 | ✅ | 批次054：独立AutoSize默认true且不调用Control.AutoSize，标准隐藏事件转发；FixedHeight、SetBoundsCore请求高度保存和AdjustHeight的两层finally、Multiline事务、Font/Padding先base通知后调整、创建时Top+Bottom anchor守卫完成。取消旧24/48 native夹值，保留便利构造；30例含原异常/有限重入/真实小高度和状态保持。Table/Flow必要消费者改用内部LayoutAutoSize，完整缩放、全部布局/派生默认及Designer仍待。 |
| TextBoxBase-007e | P1 | ✅ | 批次054：构造启用已有首选尺寸缓存，复用Text清缓存→通知及WordWrap含同值、Multiline、BorderStyle、Font/Padding/继承字体的既有或必要事务；无约束复用、带约束每次测量且不驱逐无约束缓存，异常不缓存。21例核事件内新度量、原异常、不父布局及Default/Flow/Table不按文本扩展宽度；完整复杂布局重入/缩放与平台仍待。 |
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

- 2026-11-07 · 批次062：062本类源码no-op；Rich的003b/004b沿现有句柄/选择/RTF恢复和原生消息连接，不新增共享基类完成任务。110新增/5,959全量首轮与固定双目标Release通过；完整基类/Masked模型与平台父项保持。见[062](../batches/062.md)。

- 2026-11-07 · 批次061：源码/任务no-op，Rich字符颜色与段落对齐归003a/004a，不重复计共享基类完成项；所有未完父项/ID/分母保持，见[061](../batches/061.md)。

- 2026-11-07 · 批次060：源码no-op，Rich专有RTF流与格式缓存归其002c/011a；不由派生限定范围关闭共享基类或Masked依赖，原ID/父项/分母保持，见[060](../batches/060.md)。

- 2026-11-07 · 批次059：原选择恢复函数改internal供Rich消费，OnHandleCreated移除提前恢复、CreateHandle finally边界保持；仅必要依赖，不新增基类完成项。64新增/5,728全量通过，完整基类/缩放/平台门槛保持，见[059](../batches/059.md)。

- 2026-11-07 · 批次058：源码no-op，001当前“Rich仍待直接继承”的描述按真实代码纠正并交叉引用RichTextBox-001a/007a；不新增基类完成任务，完整父项与Masked/Rich专有模型缺口保持，见[058](../batches/058.md)。

- 2026-11-07 · 批次057：新增002d/003h，纠正有窗TextLength仍取虚Text以及必要默认事件/绑定、Paint/选区/两组分类缺口。003e当前行中仍全待办的构造点击描述改为交叉引用已实现003f，原055历史/正向门槛保持。无窗异常/虚调用及Control既有直接Paint行为保留，全部原父项和固定分母保持；41新增/5,568全量首轮及固定双目标Release通过、旧测试未改，见[057](../batches/057.md)。不把设计声明/普通EDIT长度计为完整Designer、绑定或真实派生后端。

- 2026-11-07 · 批次056：新增003f/003g，仅关闭上述代码范围；纠正当前构造点击样式/这八项元数据仍全待办的描述，旧批历史不回写。46新增/5,527全量和固定双目标Release通过，未改旧测试。两次WindowFromPoint均no-hit，不将负向通过冒充正向点击验收，原始TRX保留；完整003/平台与固定分母保持，见[056](../batches/056.md)。

- 2026-11-07 · 批次055：新增003e，只关闭创建请求位/前置标志有限范围；当前ES_WANTRETURN缺口按真实代码纠正，完整构造点击/主题/元数据父项仍待。首67/68的一例新夹具错误和最终68新增/5,481全量证据见[055](../batches/055.md)，未为失败修改生产。

- 2026-11-07 · 批次054：新增007c/007d/007e限定范围，007仍部分；当前旧24/48与AutoSize/default/缓存待办描述按真实代码纠正，历史记录保留。全量首6失败中Table公开AutoSize消费为真实生产缺口，Flow同源消费者一并修复；其余5失败明确固定夹具AutoSize=false并保原断言。最终回执与未验范围见[054](../batches/054.md)。

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
