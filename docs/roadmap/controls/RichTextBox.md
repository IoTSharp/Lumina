# RichTextBox

本页任务表只列未完成范围；代码完成子项保留在[修改历史](#修改历史)，任务 ID 与验收状态均不因整理改变。剩余重点是 LinkClicked/完整通知、文件与编码、Undo/Redo/OLE、Find/选择测量、ambient/主题/DPI 和平台验收。

[完整 API 与继承基线](../api/RichTextBox.md) · [任务索引](../README.md) · [依赖顺序](../execution.md) · [验证门槛](../validation.md) · [平台要求](../platforms.md)

固定基线为 .NET 10.0.11。Windows、Linux、macOS 分别验收，当前没有完整逐控件 98% 结论。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 先修层级/阻塞，P1 完成常用契约，P2 补边界/高级场景，P3 完成平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| RichTextBox-001 | P0 | 🟡 | 058的001a完成直接TextBoxBase、系统RICHEDIT50W与基础通知；059的002a/002b和060的002c/011a另记文本/RTF模型与句柄格式缓存，007a另记必要默认。完整Rich选择/测量、共享成员覆盖、所有格式/原生状态与资源恢复仍待，不重复计数或关闭父项。 |
| RichTextBox-002 | P1 | 🟡 | 059的002a/002b完成纯文本Text/SelectedText流与逻辑读取/长度首范围；060的002c限定Rtf/SelectedRtf字符串流另列。069的005d另记选区流保护拒改；LoadFile/SaveFile各重载/StreamType、完整编码/文件锁/RTF保真及OLE仍待；不能由字符串流关闭完整父项。 |
| RichTextBox-003 | P1 | 🟡 | 061～066的003a～003e明确范围保持；067的003f另记Font override与005a必要尺寸通知依赖。005d另记保护通知/拒改；字符格式/完整输入、全部ambient/主题/字体DPI交互与平台验收仍待，父项不关闭；HFONT量化/自动前景及偏移复位沿固定源，不承诺全篇设置保留所有字符格式。 |
| RichTextBox-004 | P1 | 🟡 | 061的004a对齐、062的004b三项缩进、063的004c项目符号/缓存缩进和004d制表位限定实现完成。完整混合段落/RTL/布局单位边界、全部格式交互及平台/Designer/AOT/设备验收仍待，不以局部属性关闭父项。 |
| RichTextBox-005 | P1 | 🟡 | 067～068的005a/005b/005c保持，069的005d另记Protected事件/真实拒改及流保护错误消费。LinkClicked、完整TextChanged掩码、其余通知次数/顺序/全部输入交互及平台仍待，不由已完成通知关闭父项。 |
| RichTextBox-006 | P1 | ⬜ | CanUndo/CanRedo、Undo/Redo、UndoActionName/RedoActionName、ClearUndo；输入合并、粘贴/格式变化、程序赋值、只读和重建句柄的撤销边界。 |
| RichTextBox-007 | P1 | 🟡 | 058的007a完成AutoSize/Multiline/MaxLength覆盖与必要初值、共享HideSelection、DetectUrls/AutoWordSelection及ScrollBars真实原生设置。RightMargin、ShowSelectionMargin、ZoomFactor、完整富文本测量/布局/滚动、格式/撤销的重建恢复与物理交互仍待。 |
| RichTextBox-008 | P2 | 🟡 | 064的008a完成GetCharIndexFromPosition/GetPositionFromCharIndex/GetLineFromCharIndex三项RichEdit专有查询。Find文本/字符数组/范围/选项重载、完整行/选择方向/隐藏文字/RTL/DPI边界与测量仍待；只由全待办整理为部分，不关闭父项。 |
| RichTextBox-009 | P2 | ⬜ | AllowDrop、EnableAutoDragDrop、DragEnter/Over/Leave/Drop、GiveFeedback、QueryContinueDrag、RichTextShortcutsEnabled；OLE 专属格式单列，普通文本/RTF拖放不遗漏。 |
| RichTextBox-010 | P2 | ⬜ | LanguageOption、输入法合成、CJK 字体回退、双向段落、组合字符与缩放后光标命中；原生辅助技术暴露文本、选区和格式。 |
| RichTextBox-011 | P2 | 🟡 | 060的011a限定真实RTF句柄缓存恢复另列；066的003e颜色同步及067的003f Font override按实际实现引用，不重复计数。RTF资源、全部ambient/字体DPI、图像/对象嵌入所有权和Dispose、持久通知/OLE回调解除仍待，未完成对象支持明确标缺失。 |
| RichTextBox-012 | P3 | ⬜ | 完整属性/事件/行为分别达到 Windows≥98%；GTK/AppKit可移植分母包含普通RTF与段落格式，无法原生表达的剩余能力有逐项证据；双目标 Designer 与 AOT 真实编辑验收。 |
| RichTextBox-CLOSE | P3 | ⬜ | 对照 API 清单逐个关闭剩余属性、事件、方法/重载与受保护兼容点；属性写入后读回、通知顺序/取消/重入、默认值、异常、未建句柄/重建/释放后行为分别有证据。Windows 与两原生平台各出属性/事件/行为分子分母，未验收项不能标🧪 验收通过。 |

## 三平台的原生实现评估

**Windows**：先加载 Msftedit.dll 并创建 RICHEDIT50W，按 Unicode 结构体映射 EM_SET/GETCHARFORMAT、EM_SET/GETPARAFORMAT、EM_STREAMIN/OUT、EM_SETEVENTMASK 与 EN_SELCHANGE/EN_LINK/EN_PROTECTED/EN_REQUESTRESIZE。托管 Stream 回调需固定函数指针生命周期，禁止动态生成封送。

**Linux / GTK4**：GtkTextView/GtkTextBuffer/GtkTextTag 原生支持富文本视图与标签；GTK 没有原生 Win32 RichEdit/RTF 互操作等价层。RTF 解析/导出可在库内部实现确定的托管转换，但表格、OLE 嵌入对象、精确 RTF 保真不能仅靠 TextTag 宣称完成；逐语法与行为计数。

**macOS / AppKit**：NSTextView/NSTextStorage/NSAttributedString 原生富文本与 RTF 读写可利用，NSLayoutManager 提供文本布局；NSRuler/段落样式/链接委托做映射。Windows OLE 对象与 RichEdit 特定消息为 🚫 平台专属；RTF 往返保真必须实测，不能把支持读取等同完全支持。

跨平台工作排在 Windows 行为基线与该类最小原生试验之后。先复用同一公共 WinForms 类型与状态模型，在库内封装平台差异；本路线不要求新增平台项目或公开抽象。每平台至少覆盖：默认值/边界，代码与用户修改双向同步，事件顺序/取消/重入，键盘/输入法（适用时），DPI/RTL/本地化，无障碍，创建/重建/释放，AOT 实际运行；纯编译或仅显示窗口不足以通过。

验收口径：基线固定为 .NET 10.0.11 WinForms。Windows 的公开属性、公开事件、行为场景分别 ≥98%；Linux/macOS 在逐项确认的可移植分母中分别 ≥98%。每个重载、访问器、事件参数与行为场景独立登记，继承成员不遗漏；签名同名、字段存值、任务写完均不能作为通过分子。原生外观差异与 Windows 专属成员保留在清单中，不能用“平台不可等价”自动划掉普通业务需求。当前各项尚无完整对照证据，不发布已达标百分比。

## 修改历史

### 已完成任务（历史台账）

以下原任务行从当前任务表迁入历史，ID、状态、明确范围与验证限制原样保留；现有导出工具仍读取这些行。✅ 仅表示定义范围代码完成，未完父项与平台验收仍在当前表。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| RichTextBox-001a | P0 | ✅ | 批次058：直接TextBoxBase，建窗前以System32专用路径惰性加载Msftedit并保留与superclass缓存同寿命的进程引用，实际窗口复制系统RICHEDIT50W类/原过程。base.OnHandleCreated前启用现已消费的ENM_CHANGE/UPDATE；15例核模块身份/特有EM_GETTEXTLENGTHEX/普通EDIT反例、隐藏原生编辑/通知/状态、独立窗口及重建/销毁/多实例。完整RTF/格式、Rich专有Text/长度/选择与通知仍待，其他必要默认归007a不重复计数。 |
| RichTextBox-002a | P1 | ✅ | 批次059：Text无窗仅缓存原始值且不通知/不读虚getter，有窗Unicode全文流入、同值重流、原始空值WM_SETTEXT/非空NUL截断、临时扩限后恢复配置MaxLength及固定修改位/行索引顺序；GETTEXTEX的CR→LF读取、EN_CHANGE与纯文本创建/重建恢复落地。27例含50kUnicode、错误回传/重试、原异常和真实输入限制；首Modified重建缺口已修，纯文/选择/Modified/通知硬要求保持。RTF/Protected/OLE/格式恢复及完整002未计完成。 |
| RichTextBox-002b | P1 | ✅ | 批次059：TextLength强制Handle、GTL_NUMCHARS/CP1200；SelectedText强制建窗、GT_SELECTION读取与CR→LF、SFF_SELECTION Unicode流/原始空值WM_CLEAR；SelectionLength有窗取SelectedText.Length排除EOF。37例含Pack4结构/参数/原生返回、70k范围、只读双分支、MaxLength0/3、状态/Undo/通知与精确消息；静态Stdcall回调/逐调用cookie为必要依赖。完整专有选择方向/行坐标/格式/008及设备验收仍待。 |
| RichTextBox-002c | P1 | ✅ | 批次060：标准非virtual Rtf/SelectedRtf属性与SF_RTF全文/选区流入流出，ACP编码、NUL/五字节前缀与异常、空值分支、同值短路、显式/原生通知及配置限制恢复；静态Stdcall流出回调/独立cookie。31例含真实粗体/Unicode往返、错误/重试/GC、只读/限长/精确消息；独立native核空选区导出0字节，保全文正字节对照。完整文件重载/Protected/OLE及002父项不关闭。 |
| RichTextBox-003a | P1 | ✅ | 批次061：标准非virtual SelectionColor/SelectionBackColor与隐藏浏览/序列化；前景强制建窗并GET→SET仅COLOR/effects0，背景无窗缓存/有窗新零结构、auto effect优先、同值仍原生提交，非空缓存在base创建通知与文本/RTF加载前重放。33例覆盖116字节Pack4/精确mask与颜色转换、混合/最新native状态、其他格式/选区保持、RTF优先/重建；Empty背景mask0/effect按固定源及独立RichEdit对照保留。完整003、Font/ForeColor/BackColor同步及平台不计完成。 |
| RichTextBox-003b | P1 | ✅ | 批次062：标准非virtual SelectionCharOffset/SelectionProtected、DefaultValue及隐藏浏览/序列化；偏移先拒绝超出[-2000,2000]再建窗，以新CHARFORMAT2W仅提交OFFSET，getter直接转换原生Offset不检查mask。保护getter需mask/effect均PROTECTED，setter新零结构仅mask/effect且每次提交，不补完整保护通知/拒改。43例含116字节精确消息/屏幕Y DPI、原异常重试/混合/最新native、其他格式/状态和RTF重建Modified真/假。完整003仍待。 |
| RichTextBox-003c | P1 | ✅ | 批次064：标准public非virtual只读SelectionType及Behavior/Browsable(false)/DSV.Hidden；先强制建窗，再读虚SelectionLength，仅正值发送EM_SELECTIONTYPE且直接cast原生int，保留未知/负值与重复实时查询。标准RichTextBoxSelectionTypes为五值int Flags，Object/MultiObject常量不代表OLE对象能力完成。39例覆盖句柄/虚getter顺序、原异常/重试与getter销毁后的替代句柄、真实空/单/多/混合格式文字及独立RichEdit、原生最新选区、文档/格式/Undo/Modified/通知和RTF重建。完整003/对象选择/平台未关闭。 |
| RichTextBox-003d | P1 | ✅ | 批次065：标准非virtual Font? SelectionFont/Browsable(false)/Hidden/DisallowNull；先强制建窗，getter每次GET selection格式，FACE缺位返回null、SIZE缺位默认13point，四style分别需mask/effect，保原生charset及Drawing构造失败的固定null结果。setter先建窗再访问字体，新零116字节CHARFORMAT2W只提交七项font mask0xA800000F，点大小乘20截断、真实LOGFONT的face/charset/pitch，每次SET且忽略native返回，不消费或释放借用Font。私有92字节blittable LOGFONT经ToHfont/GetObjectW读取、finally释放临时HFONT，资源/封送不外泄。93例含精确消息/空值与异常顺序、真实字体/混合状态/独立RichEdit、其他格式/Undo/Modified/通知与RTF重建；首中英夹具失败/8项独立FACE缺位诊断保留，完整003/Font同步/平台未关闭。 |
| RichTextBox-003e | P1 | ✅ | 批次066：标准ForeColor override，无窗交基类；有窗每次GET默认CHARFORMAT2W，COLOR有效且同Win32颜色时不SET，否则保留其他字段只改COLOR/effects0并以SCF_ALL提交，原生非零才更新基类/通知。OnBackColorChanged有窗先提交有效背景再基类，创建在base事件及Text/RTF前初始化背景与前景，保RTF显式颜色；旧Rich ApplyTheme改用有效BackColor防覆盖。新契约/真实RichEdit检查见批次页；完整Font、全部ambient/主题/格式交互及平台未关闭。 |
| RichTextBox-003f | P1 | ✅ | 批次067：标准AllowNull Font override；无窗或真实WindowText长度非正直接base，有文null先清base再读虚Font全篇提交。非null每次GET默认格式，null或不Equals才SET SCF_ALL，再以解析后的默认Font回写base；原生返回忽略，回写阶段暂抑ContentsResized且finally恢复。复用SelectionFont私有116字节/LOGFONT转换，不消费借用Font；generic-family身份、HFONT量化及自动前景/偏移复位由独立原生对照确认，全部ambient/字体DPI与003父项未关闭。 |
| RichTextBox-004a | P1 | ✅ | 批次061：标准SelectionAlignment/DefaultValue(Left)/隐藏序列化；非法枚举value先拒绝而不建窗，getter强制建窗并按PARAFORMAT mask8/native1/2/3映射，无mask/未知值回Left。setter每次以156字节Pack4零结构仅设对齐发送，忽略原生返回值如固定源。34例覆盖精确布局/消息、原异常重试、真实选中/空选区/混合段落、最新native读回及缩进/Tab/粗体/RTF重建/Modified真假的状态保持。Bullet/缩进/Tabs及完整004仍待。 |
| RichTextBox-004b | P1 | ✅ | 批次062：三项标准非virtual缩进属性及DefaultValue(0)/隐藏序列化；getter强制建窗，以PARAFORMAT对应mask有效字段读取，否则0；setter新零结构只提交STARTINDENT/RIGHTINDENT/OFFSET一个字段且忽略native返回，右缩进先拒负，其余接受完整int。屏幕X DPI像素/twip沿固定浮点顺序截断，每次DC在finally释放。67例核156字节精确结构/无GET/零返回、极值/原异常、真实空/混合/独立native状态、无关格式和RTF重建Modified真/假；Bullet/Tabs及完整004仍待。 |
| RichTextBox-004c | P1 | ✅ | 批次063：004c：SelectionBullet为标准非virtual/默认false/隐藏属性，getter强制建窗后需NUMBERING mask且值1才真；setter新零156字节PARAFORMAT仅mask0x24/Numbering和Offset，true为bullet1与屏幕X像素转twip，false清两字段，每次SET无GET。BulletIndent标准非virtual/默认0/Behavior/Localizable，负值前置拒绝，先写缓存且无窗不建；有窗仅当前真bullet再提交，同值也查询。48例含精确载荷/混合/原生最新值/缓存时序/原异常/其他格式/RTF重建Modified真假的状态保持。完整004仍待。 |
| RichTextBox-004d | P1 | ✅ | 批次063：004d：SelectionTabs为标准非virtual int[]/隐藏序列化/AllowNull，超过32在建窗前拒绝且ActualValue为空；getter按TABSTOPS有效mask逐槽转像素，无mask为空。setter先GET当前156字节结构，仅改mask/数量/前N槽，保留非目标字段及尾槽；null/空清数量，不排序/不校验元素，重复仍GET→SET并忽略返回。38例含32边界/全int/数组寿命/整结构/独立RichEdit/混合段落/其他格式及RTF重建通过。首AllowNull夹具只修setter参数反射，原失败保留。完整004未关闭。 |
| RichTextBox-005a | P1 | ✅ | 批次067：标准ContentsResizedEventArgs/Handler、Events槽事件与protected virtual OnContentsResized；创建/重建启用ENM_REQUESTRESIZE，EN_REQUESTRESIZE读取blittable REQRESIZE，Fixed3D原地bottom++后交标准Rectangle。Font非null解析回写base期间抑制，finally恢复；空文/null分支不抑制。真实Form/parentless、来源拒绝/同ID与重挂停车连接见32例；完整005/反射父寿命/平台仍待。 |
| RichTextBox-005b | P1 | ✅ | 批次068：标准SelectionChanged/Events槽及protected virtual OnSelectionChanged；EN_SELCHANGE读取36字节x64 Pack4 SELCHANGE，起点/终点/short类型三项去重，构造/创建重置-1、事件前更新缓存，异常/有限重入/CanRaiseEvents不回退。保留Hangul/HangulFull下原生组合模式、WindowText长度与MaxLength门禁、焦点消息和PostMessage末字符选择，先处理再去重；不由基类选择方法合成重复事件。36契约及29原生案例（含独立raw对照）通过，完整物理IME/全部通知与005父项仍待。 |
| RichTextBox-005c | P1 | ✅ | 批次068：标准HScroll/VScroll/ImeChange三个Events槽及protected virtual On*；EN_HSCROLL/EN_VSCROLL创建期交基类，运行期通知；WM_IME_NOTIFY先事件后base，WM_HSCROLL/WM_VSCROLL先base后按返回消息低16位只对4/5触发。创建/重建加入ENM_SCROLL/SCROLLEVENTS/IMECHANGE；42例含参数/事件隔离、精确前后顺序、原异常、改写消息及自有停车通知来源。仅程序通知契约，物理滚动/真实输入法及完整005未验。 |
| RichTextBox-005d | P1 | ✅ | 批次069：标准EventHandler Protected/Behavior/Events槽与protected virtual OnProtected，先置保护错误再派发；私有Pack4 ENPROTECTED(x64 52字节)，创建启用ENM_PROTECTED。保护格式mask/全文流/COPY/SETTEXT/EXLIMITTEXT允许，其他字符/段落格式与选区替换按固定源拒绝，默认消息先MessageBeep。内部结果经Form/容器/停车返回0/1，空选区清标志、非空流恢复限制后消耗保护错误，不抛拒改原生错误/不追加修改位或行计数。契约19/真实原生28/共享结果49与旧相关通过；LinkClicked/完整005、文件/OLE、物理输入及平台不计完成。 |
| RichTextBox-007a | P1 | ✅ | 批次058：100×96默认、MaxLength→Multiline→AutoSize虚setter构造顺序及声明，HideSelection直接继承唯一基类状态；扩展EM_EXLIMITTEXT、标准RichTextBoxScrollBars七值/请求/FixedSingle CLIENTEDGE与变化重建、DetectUrls旧窗发送后重建、AutoWordSelection每次单条OR2/XOR4（保留重复false的native toggle）。43默认+38原生选项回归、7条值×多行×折行28组请求及独立窗口对照通过；完整007及Rich专有模型不关闭。 |
| RichTextBox-008a | P2 | ✅ | 批次064：三项标准override，字符索引用完整8字节POINTL/EM_CHARFROMPOS，消息后读虚Text，保留负值/完整int并仅夹越界尾部；物理行无校验地使用EM_EXLINEFROMCHAR有符号lParam。位置查询负值先短路，其余按虚Text校验且EOF合法，新零POINTL作wParam输出缓冲，忽略消息返回并保留32位坐标；固定RICHEDIT50W后端不新增RichEdit2路径。62例含精确消息/虚调用/原异常/70k索引、真实UTF16/尾换行/折行/最新native/独立RichEdit及状态/RTF重建；首两轮物理行夹具错误已按直接原生证据修正，70001硬索引不降。Find/完整008及设备平台未完成。 |
| RichTextBox-011a | P1 | ✅ | 批次060：销毁先base再缓存最新真实RTF；创建先base、优先恢复RTF再选择/ClearUndo，保持原生Modified真/假与配置MaxLength。无窗Text覆盖清RTF缓存并按新纯文赋值清修改位，缓存RTF的Text读取经RichEdit翻译。23例含七重建入口/真实字符段落格式、最新native编辑、选择/限长/Undo/通知、失败重试/实例/释放；首新纯文恢复旧Modified缺口修生产，原false硬要求保持。字符/段落公共格式API、OLE/资源完整生命周期与011父项未完成。 |

### 变更记录


- 2026-11-07 · 批次069：069限定完成RichTextBox-005d标准Protected通知及真实保护拒改，并以Control-17g补必要WM_NOTIFY原生结果回传。Form改按来源HWND路由，容器/停车接收者保持0/1结果；旧OnNotify钩子保留。保护样式本身与全文替换允许，受保护选区替换/格式修改拒绝；流拒改消耗保护标志，不伪造成功修改。96新增及1048相关通过，Release固定双目标0错/2既有NU1510。完整LinkClicked/TextChanged/其他通知、文件/OLE/撤销、Find/完整选择测量、Masked模型与平台/98%仍待；064全量FAIL/未解决、065超时及066～069全量NOT_RUN保持。用户2026-11-07明确要求本批完成后停止连续代码实施；下一新会话只整理路线图/文档，将完成范围迁入变更记录并合并重复内容，不启动070。 005及其他父项保持部分，见[069](../batches/069.md)；下述068及以前为历史。

- 2026-11-07 · 批次068：005b/005c只关闭对应选择/滚动/IME程序通知范围；原ID/未完父项/固定分母及064FAIL/065超时保持，见[068](../batches/068.md)。

- 2026-11-07 · 批次067：完成003f/005a限定范围，005从待办整理为部分；纠正当前总述/011仍把已完成颜色同步列全待办的滞后。保留原父项/ID/固定分母与064FAIL/065超时，原失败与独立原生诊断见[067](../batches/067.md)。

- 2026-11-07 · 批次066：限定RichTextBox-003e颜色同步，原ID/未完父项/固定分母保持；064全量FAIL及065超时不由本批检查清除，见[066](../batches/066.md)。

- 2026-11-07 · 批次065：限定完成003d SelectionFont，93新增通过；首77/85与独立中英FACE缺位8/8保持，生产行为/旧断言未为失败改变，完整Font同步与003父项仍待。 本批全量180秒超时未完成，064全量FAIL/超时未解门槛保留，见[065](../batches/065.md)。

- 2026-11-07 · 批次064：限定完成003c/008a，101新增通过，完整父项保留；首两轮100/101的长段落物理行/跨窗口布局假设失败保留，生产未为失败改变。只修新夹具并保70001硬索引、各窗口原生往返与本HWND精确行号对照；最终全量见[064](../batches/064.md)。

- 2026-11-07 · 批次063：限定完成Rich-004c/004d；86新增/6,045全量及固定双目标Release通过，首85/86的新AllowNull反射夹具失败回执保留。原ID/未完父项/固定分母保持，见[063](../batches/063.md)。

- 2026-11-07 · 批次062：003b字符偏移/选区保护属性与004b三项段落缩进限定范围完成；110新增/5,959全量首轮及固定双目标Release通过。屏幕DC/twip、原生mask/精确结构/混合读回和RTF重建保持；未完父项/平台/98%及固定分母保留，见[062](../batches/062.md)。

- 2026-11-07 · 批次061：新增003a/004a，004由全待办纠正为部分；SelectionColor仅存值的当前描述按真实native实现更新，003/004及其他父项保持未完。67新增/5,849全量首轮及固定双目标Release通过；旧测试/项目/Demo未改，原ID/固定分母保持，见[061](../batches/061.md)。

- 2026-11-07 · 批次060：新增002c/011a，011从全待办纠正为部分；002/001保留未完父项，当前RTF/格式缓存描述按实际代码更新。54新增/5,782全量及固定双目标Release通过；首测试unsafe编译失败与首50/54保留，修真实新纯文Modified恢复缺口及空选区新夹具。原ID/未完任务/分母保持，见[060](../batches/060.md)。

- 2026-11-07 · 批次059：新增002a/002b，002由全待办改部分；当前Text/限长/逻辑读取描述按真实代码修正，058与此前失败证据保持。64新增/5,728全量通过，首61/64含真实Modified重建缺口及两项新夹具问题，058旧反例明确改为native17/流恢复3。只关闭纯文限定范围，原ID/未完父项/固定分母保持，见[059](../batches/059.md)。

- 2026-11-07 · 批次058：新增001a/007a；001/007从全待办改部分，纠正当前TextBox层级/EDIT后端及HideSelection独立存值描述，旧042～057快照保持。最终96新增/5,664全量通过；首94/96中程序设文native扩限和BOOL返回-1经独立RichEdit核实，只修新夹具，生产/旧测试未为失败改动。真实Text/StreamIn缺口明确留002；全部原ID/未完项/分母保持，见[058](../batches/058.md)。

- 2026-11-07 · 批次057：普通EDIT的基类002d/003h不关闭本类型001/002或其他父项，源码no-op、不重复计数；真实RICHEDIT50W、专有默认与格式行为仍待，见[057](../batches/057.md)。

- 2026-11-07 · 批次056：普通EDIT的基类003f/003g不关闭本类型直接继承/真实后端或其他父项，源码no-op、不重复计数，见[056](../batches/056.md)。

- 2026-11-07 · 批次055：普通EDIT的TextBox-004b/TextBoxBase-003e不关闭本类型001/007/008或其他父项；源码no-op，不重复计数，见[055](../batches/055.md)。

- 2026-11-07 · 批次054：普通EDIT的007c/007d/007e不关闭RichTextBox-001/007或其他父项；源码与未完成真实后端保持，见[054](../batches/054.md)。

- 2026-11-07 · 批次053：普通EDIT的007b/008c不关闭RichTextBox-001/007/008或其他父项，不重复计数，见[053](../batches/053.md)。

- 2026-11-07 · 批次052：基类008a/008b只验普通EDIT，不关闭RichTextBox-001/008或其他父项，不重复计数；本类型源码保持，见[052](../batches/052.md)。

- 2026-11-07 · 批次051：普通EDIT词删除的间接消费不补本类型真实后端/标准层级，源码no-op，所有原父任务保持，见[051](../batches/051.md)。

- 2026-11-07 · 批次050：源码no-op，共享005e入口和003d禁用行为不关闭RichEdit的剪贴板格式、快捷键/富文本编辑或标准直接层级，原任务和固定分母保持，见[050](../batches/050.md)。

- 2026-11-07 · 批次049：源码no-op，普通EDIT共享Clear/重挂命令不计RichEdit专有支持，原任务及固定分母保持，见[049](../batches/049.md)。

- 2026-11-07 · 批次048：源码no-op，共享普通EDIT追加/撤销不计RichEdit专有支持，原父任务及真实后端继续待办，见[048](../batches/048.md)。

- 2026-11-07 · 批次047：源码no-op，共享普通EDIT替换/分行/输入限制不实现RichEdit专有语义或标准直接层级；全部原父任务保留，见[047](../batches/047.md)。

- 2026-11-07 · 批次046：源码no-op，共同边框与选择方法的间接消费不实现RichEdit选区、格式、撤销或标准直接基类；仍由原任务管理真实后端，见[046](../batches/046.md)。

- 2026-11-07 · 批次045：源码no-op，间接共享Modified/程序设文规则不等于RichEdit修改/撤销完成；错误层级间接暴露CharacterCasing不计本类型标准支持。完整直接基类和真实后端保留原任务，见[045](../batches/045.md)。

- 2026-11-07 · 批次044：HideSelection仅补显式new避免隐藏声明告警，不转发到标准基类，不改变派生存值语义。完整001/007和真实富文本后端仍未完成；通过错误TextBox层级暴露的TextAlign不计本类型标准支持。

- 2026-11-07 · 批次043：源码no-op，间接共享真实折行/滚动样式及重建路径，不能据此关闭001或RichEdit/富文本功能；标准直接基类/派生默认仍待，见[043](../batches/043.md)。

- 2026-11-07 · 批次042：源码no-op，随TextBox间接消费新TextBoxBase的共享EDIT首范围；仍直接继承TextBox且原生仍EDIT，RichTextBox-001及富文本功能不关闭。不能把共同基类存在或EM_SETBKGNDCOLOR消息当RichEdit支持，见[042](../batches/042.md)。

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 2026-04-09 · `2fb513a` · feat: 添加异步调用支持，增强 ListView 和 RichTextBox 控件功能，优化 TableLayoutPanel 布局逻辑。
- 2026-04-08 · `2545995` · feat: 增强深色模式支持，优化多个控件的主题应用逻辑。
- 2026-04-08 · `7149595` · Implement dark mode capabilities and menu rendering enhancements。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

## 官方依据

- [WinForms RichTextBox API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.richtextbox?view=windowsdesktop-10.0)，精确签名以本机 10.0.11 引用程序集生成的 API 清单为准。
- [Windows 原生控件参考](https://learn.microsoft.com/windows/win32/controls/about-rich-edit-controls)。
- [GTK4 TextView](https://docs.gtk.org/gtk4/class.TextView.html)。
- [AppKit nstextview](https://developer.apple.com/documentation/appkit/nstextview)。
