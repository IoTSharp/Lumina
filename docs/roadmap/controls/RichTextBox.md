# RichTextBox

基线：2026-10-15 · .NET 10.0.11。目标继承：`RichTextBox : TextBoxBase`。本页是实施与验收任务，未声称已完成兼容。

## 当前证据与依赖

051源码no-op，错误TextBox层级间接消费普通EDIT的启用词删除与内部redraw辅助；不计RichEdit的词/撤销/格式或专有输入支持，标准直接继承、真实RICHEDIT后端和全部父项保持，见[051](../batches/051.md)。下述050及以前为历史。

050源码no-op，错误TextBox层级间接消费普通EDIT剪贴板命令入口及快捷键禁用/菜单抑制；不计RichEdit格式剪贴板、专有快捷键/输入或真实菜单显示支持，标准直接基类/真实后端和全部父项保持，见[050](../batches/050.md)。

049源码no-op，错误TextBox层级间接共享Clear虚Text=null及普通EDIT重挂命令连接；不实现富文本Clear专有行为、RichEdit通知/真实后端或标准直接继承，全部原父任务保持，见[049](../batches/049.md)。

048源码no-op，错误TextBox层级间接消费普通EDIT的AppendText/CanUndo/Undo/ClearUndo；不实现RichEdit撤销/重做、动作名、富文本追加或标准直接基类。001/002/006等全部原父任务保持，见[048](../batches/048.md)。

047源码no-op，错误TextBox层级间接共享SelectedText/Lines/MaxLength的EDIT实现；本类型需要的RichEdit选区/富文本、MaxLength覆盖及标准直接基类仍未实现，原001/002/006/007父任务不关闭，见[047](../batches/047.md)。

046源码no-op，错误TextBox层级间接消费共同BorderStyle/SelectAll/DeselectAll；标准直接继承、RichEdit、选区格式/撤销及专有默认仍待，001/006/007和全部原父任务不关闭，见[046](../batches/046.md)。

045源码no-op，间接继承标准Modified及错误TextBox层级的CharacterCasing；这不实现RichEdit事件、撤销或本类型标准直接层级。独立HideSelection状态和EDIT后端现状保留，001/006/007等原任务不关闭，见[045](../batches/045.md)。

044仅为既有HideSelection自动属性补显式new，保留独立存值与true默认；通过TextBoxBase引用访问的新标准HideSelection属于另一属性状态，不把隐藏成员声明当RichEdit选区显示已实现。现有错误继承链也会间接暴露TextBox.TextAlign；直接层级、富文本后端和本类型标准成员仍留原任务，见[044](../batches/044.md)。

043未改本类型源码；现有TextBox间接路径随共同基类消费真实WordWrap/ScrollBars，None不再强制纵条。此行为修正不补本类型的标准直接继承、默认覆盖或RichEdit/富文本后端，全部原任务状态保留。

🟡 [RichTextBox.cs](../../../Lumina.Forms/RichTextBox.cs) 当前继承 TextBox，构造开启多行但原生类仍 EDIT；SelectionColor、HideSelection 仅存值。给 EDIT 发送 EM_SETBKGNDCOLOR 不能证明 RichEdit 支持。RTF、选区格式、RichEdit 事件、撤销重做尚缺。

源码定位：[Lumina.Forms/RichTextBox.cs](../../../Lumina.Forms/RichTextBox.cs)；共同问题见 [Control](../../../Lumina.Forms/Control.cs) 和 [Win32 interop](../../../Lumina.Forms/Win32.cs)。先决依赖：TextBoxBase、原生 RichEdit 载入与回调、字体/颜色/图像策略、剪贴板和 RTF 流。

公开成员（含继承的全部属性、事件和方法）逐项见 [RichTextBox API 清单](../api/RichTextBox.md)；共同行为、AOT、设计器与计数方法见 [基础任务](../foundations.md)。本表列出具体实现/验收动作，API 清单中余下成员也必须逐条关闭，不能把表外成员当作已支持。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 先修层级/阻塞，P1 完成常用契约，P2 补边界/高级场景，P3 完成平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| RichTextBox-001 | P0 | ⬜ | 改为 RichTextBox : TextBoxBase 并实际创建 RICHEDIT50W；运行时探针断言类名，阻止 EDIT 继续冒充富文本；修正共享 API 与默认多行状态。 |
| RichTextBox-002 | P1 | ⬜ | Rtf、SelectedRtf、Text、SelectedText、LoadFile/SaveFile 各重载与 RichTextBoxStreamType；空/损坏/非RTF输入、编码、流位置、异常、文件锁及往返保真逐场景验收。 |
| RichTextBox-003 | P1 | 🟡 | SelectionColor 从存值改真实 CHARFORMAT；SelectionBackColor、SelectionFont、SelectionCharOffset、SelectionProtected、SelectionType 和混合选区返回值逐项验收。 |
| RichTextBox-004 | P1 | ⬜ | SelectionAlignment、SelectionBullet、BulletIndent、SelectionIndent、SelectionHangingIndent、SelectionRightIndent、SelectionTabs；空选区与混合段落、RTL 和缩进单位换算。 |
| RichTextBox-005 | P1 | ⬜ | SelectionChanged、ContentsResized、LinkClicked、Protected、HScroll、VScroll、ImeChange 与 TextChanged：原生事件掩码、参数、次数/顺序及受保护文字拒绝修改场景。 |
| RichTextBox-006 | P1 | ⬜ | CanUndo/CanRedo、Undo/Redo、UndoActionName/RedoActionName、ClearUndo；输入合并、粘贴/格式变化、程序赋值、只读和重建句柄的撤销边界。 |
| RichTextBox-007 | P1 | ⬜ | DetectUrls、AutoWordSelection、AutoSize、HideSelection、MaxLength、RightMargin、ScrollBars、ShowSelectionMargin、ZoomFactor 的默认值、上下界、布局/滚动变化逐项对照。 |
| RichTextBox-008 | P2 | ⬜ | Find 的文本/字符数组/范围/选项重载，GetCharIndexFromPosition/GetPositionFromCharIndex/行方法；UTF-16、CRLF、emoji、隐藏文字和选择方向边界。 |
| RichTextBox-009 | P2 | ⬜ | AllowDrop、EnableAutoDragDrop、DragEnter/Over/Leave/Drop、GiveFeedback、QueryContinueDrag、RichTextShortcutsEnabled；OLE 专属格式单列，普通文本/RTF拖放不遗漏。 |
| RichTextBox-010 | P2 | ⬜ | LanguageOption、输入法合成、CJK 字体回退、双向段落、组合字符与缩放后光标命中；原生辅助技术暴露文本、选区和格式。 |
| RichTextBox-011 | P2 | ⬜ | RTF 资源、Font/ForeColor、图像/对象嵌入所有权和 Dispose；所有原生流/通知回调在释放后解除，未完成对象支持明确标缺失。 |
| RichTextBox-012 | P3 | ⬜ | 完整属性/事件/行为分别达到 Windows≥98%；GTK/AppKit可移植分母包含普通RTF与段落格式，无法原生表达的剩余能力有逐项证据；双目标 Designer 与 AOT 真实编辑验收。 |
| RichTextBox-CLOSE | P3 | ⬜ | 对照 API 清单逐个关闭剩余属性、事件、方法/重载与受保护兼容点；属性写入后读回、通知顺序/取消/重入、默认值、异常、未建句柄/重建/释放后行为分别有证据。Windows 与两原生平台各出属性/事件/行为分子分母，未验收项不能标🧪 验收通过。 |

## 三平台的原生实现评估

**Windows**：先加载 Msftedit.dll 并创建 RICHEDIT50W，按 Unicode 结构体映射 EM_SET/GETCHARFORMAT、EM_SET/GETPARAFORMAT、EM_STREAMIN/OUT、EM_SETEVENTMASK 与 EN_SELCHANGE/EN_LINK/EN_PROTECTED/EN_REQUESTRESIZE。托管 Stream 回调需固定函数指针生命周期，禁止动态生成封送。

**Linux / GTK4**：GtkTextView/GtkTextBuffer/GtkTextTag 原生支持富文本视图与标签；GTK 没有原生 Win32 RichEdit/RTF 互操作等价层。RTF 解析/导出可在库内部实现确定的托管转换，但表格、OLE 嵌入对象、精确 RTF 保真不能仅靠 TextTag 宣称完成；逐语法与行为计数。

**macOS / AppKit**：NSTextView/NSTextStorage/NSAttributedString 原生富文本与 RTF 读写可利用，NSLayoutManager 提供文本布局；NSRuler/段落样式/链接委托做映射。Windows OLE 对象与 RichEdit 特定消息为 🚫 平台专属；RTF 往返保真必须实测，不能把支持读取等同完全支持。

跨平台工作排在 Windows 行为基线与该类最小原生试验之后。先复用同一公共 WinForms 类型与状态模型，在库内封装平台差异；本路线不要求新增平台项目或公开抽象。每平台至少覆盖：默认值/边界，代码与用户修改双向同步，事件顺序/取消/重入，键盘/输入法（适用时），DPI/RTL/本地化，无障碍，创建/重建/释放，AOT 实际运行；纯编译或仅显示窗口不足以通过。

验收口径：基线固定为 .NET 10.0.11 WinForms。Windows 的公开属性、公开事件、行为场景分别 ≥98%；Linux/macOS 在逐项确认的可移植分母中分别 ≥98%。每个重载、访问器、事件参数与行为场景独立登记，继承成员不遗漏；签名同名、字段存值、任务写完均不能作为通过分子。原生外观差异与 Windows 专属成员保留在清单中，不能用“平台不可等价”自动划掉普通业务需求。当前各项尚无完整对照证据，不发布已达标百分比。

## 修改历史

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
