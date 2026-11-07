# 公共基础：先修这些，再铺控件

2026-11-07 · 批次052完成TextBoxBase-008a/008b六个行/字符位置查询限定范围；102新增/5,221全量及固定双目标Release通过。首93/102九个夹具轨迹错误与原TRX保留，生产未为此改动。普通EDIT程序查询不等于完整测量/default、派生编辑、物理输入或辅助技术验收，完整线程/模态/平台仍待，见[052](batches/052.md)。下述051及以前为历史。

2026-11-07 · 批次051完成TextBoxBase-005f固定词边界辅助、009a启用词删除命令及Control-14c内部redraw依赖。88新增/5,119全量与固定双目标Release通过；首次85/88的夹具轨迹错误与失败TRX保留。真实EDIT可撤销删除不等于完整输入/剪贴板或可见绘制验收，测量/default、真实派生后端、完整线程/模态和平台仍待，见[051](batches/051.md)。下述050及以前为历史。

2026-11-07 · 批次050完成TextBoxBase-005e标准Copy/Cut/Paste命令入口及003d快捷键禁用/只读对齐/系统菜单抑制。77新增、5,031全量及固定双目标Release通过；命令消息截获而不访问系统剪贴板，custom菜单只核受限路由。完整编辑/启用时Ctrl+Backspace词删除、测量/default、派生真实后端及线程/平台门禁保持，见[050](batches/050.md)。下述049及以前为历史。

2026-11-07 · 批次049完成Control-17e原停车接收者寿命/按来源HWND反射及TextBoxBase-005d标准Clear。独立EDIT证明实际父与通知接收者可不同；18新增与8旧用例组合26/26、4,954全量及固定双目标Release通过。原反射父Form寿命/WM_NOTIFY、完整编辑/测量/派生后端与平台门禁继续原任务，见[049](batches/049.md)。下述048及以前为历史。

2026-11-07 · 批次048完成TextBoxBase-005b/005c的AppendText与公开撤销，以及必要Control-17d停车期间WM_COMMAND反射。43新增与3旧行为组合46/46、4,936全量及固定双目标Release通过；重挂后EDIT命令仍未交付的探针/诊断保留且继续原Control-17/TextBoxBase-006，完整编辑/派生后端/线程与平台门禁不关闭，见[048](batches/048.md)。下述047及以前为历史。

2026-11-07 · 批次047完成TextBoxBase-002b/002c的SelectedText及Lines，并补必要003c的MaxLength；65新增/4,893全量及固定双目标Release通过。TextBox消费共享实现，Rich/Masked错误层级的间接消费不关闭其真实后端；完整AppendText/公开撤销/剪贴板、测量、StringArrayEditor/Designer和平台门禁仍待，见[047](batches/047.md)。下述046及以前为历史。

2026-11-07 · 批次046补TextBoxBase边框设置/通知、SelectAll/DeselectAll及源窗口类身份缓存，限定范围为002a/007a/Control-17c。46新增与旧对齐组合74/74、最终4,828全量通过；完整测量/编辑、真实派生后端与平台门禁仍待，见[046](batches/046.md)。下述045及以前为历史。

2026-11-07 · 批次045补TextBoxBase.Modified/ModifiedChanged、EN_UPDATE及程序设文期间的最小内部WindowText守卫，TextBox.CharacterCasing交真实EDIT处理大小写。45新增、892相关和4,782全量通过；完整共同编辑/尺寸、真实派生后端和平台门禁仍待，见[045](batches/045.md)。下述044及以前为历史。

2026-11-07 · 批次035～043范围保留；044补TextBoxBase.HideSelection标准设置/通知和TextBox.TextAlign设置/创建样式，纠正HorizontalAlignment整数身份。完整编辑基类、派生后端、RTL/镜像视觉、OLE宿主context、输入/布局及平台验收仍待，见[044](batches/044.md)。

依赖顺序：`Component/Control → ScrollableControl → ContainerControl → Form/UserControl`；`ButtonBase/TextBoxBase/ListControl/UpDownBase` 与容器/集合各自接入该体系。**不新造公共框架或公开平台接口。** 分平台实现先放现有 Lumina.Forms 内部，等实证表明确有必要再拆项目。

## P0：必须先关闭的确定性阻塞

037新增有限IME字符分流、TextBox派生输入及UserControl原生焦点入口，22项定向与4,536项全量通过，见[037](batches/037.md)；FND-019/021继续部分，完整输入法与平台仍待。

| ID | 状态 | 工作与验收 | 对应文档 |
| --- | --- | --- | --- |
| FND-001 | 🟡 | 002～019基础构造、集合/标准句柄及Form链保留；042新增TextBoxBase共享EDIT首范围与TextBox直接继承。RichTextBox/MaskedTextBox直接层级/原生后端、ButtonBase、IDropTarget和所有派生默认/完整扩展仍缺，不由一个基类或构造关闭父项 | [Control](controls/Control.md)、[Form](controls/Form.md)、[TextBoxBase](controls/TextBoxBase.md)、[批次042](batches/042.md) |
| FND-002 | 🟡 | 003～025投递/context范围保留；026的Control-05c补已接受InvokeAsync任务关闭/Post/Attach失败终止，保留取消先后/重建/已开始异步工作和ValueTask单消费。ApplicationContext/模态、多UI退出、等待期间释放同一wait与完整竞态仍缺；null Delegate/Send、关闭增强和错误UI差异保留 | [线程上下文](controls/WindowsFormsSynchronizationContext.md)、[批次026](batches/026.md) |
| FND-003 | 🟡 | 批次001～017完成列表基础构造/子项样式与key、集合共享view/整数索引/typed增删/非泛型IList/查询复制/枚举、两级字符串、主项元数据/未挂接样式及未挂接ImageIndex/ImageKey状态首范围；017图像选择518条参考/JIT相同。其余构造、ImageList解析/挂接native、图像转换器/编辑器、分组、资源继承、完整所属/并发及Items/SubItems/Columns/Nodes原生同步仍待实现，不把局部状态/集合契约当作完整所属 | [ListViewItem](controls/ListViewItem.md)、[ListViewSubItem](controls/ListViewSubItem.md)、[TreeView](controls/TreeView.md) |
| FND-004 | 🟡 | 018～026 Demo窗口证据保留；027 Native AOT原生窗口可见、程序BM_CLICK使Enable option 0→1、WM_CLOSE正常exit0且原HWND全部销毁。锁屏下物理鼠标命中未验收；模态、完整布局/业务交互、设计器/可访问性及018文字裁切仍未关闭 | [027证据](batches/027.md)、[原评估](../assessments/2026-10-15/README.md) |
| FND-005 | ✅ | 批次 001 对齐 Run(Form mainForm) 的 void、SetHighDpiMode 的 bool、HighDpiMode 数值；连接真实 DPI 设置/读取及建窗门禁，14 组隔离场景与本机固定基线对照通过；完整消息循环与真机缩放验收仍待后续任务 | [Application](controls/Application.md) |
| FND-006 | 🟡 | 批次 002 完成 Demo 五张 Bitmap 的内建 byte[] 资源/直接解码，三端像素及身份语义对照通过，IL2026 已消除。原 IL3053 展开为 Container.ValidateName 根化 InheritanceAttribute.ToString/Enum.GetValues(Type) 的 IL3050，仍未关闭；Icon/Font/其他 TypeConverter 与完整设计器/主窗另列待办，不用全局 suppress 代替处理 | [跨平台资源](platforms.md)、[批次 002](batches/002.md) |

## P1：所有控件共享的工作

| ID | 状态 | 任务细节与验收 |
| --- | --- | --- |
| FND-010 | 🟡 | 018的Control-06a完成集合/Parent及Windows重挂；019修复隐藏重挂的中间VisibleChanged，391行参考/JIT现全部相同。IList.Insert按标准不支持，AOT32条非零下界数组fixture未执行CopyTo仍保留。完整布局/继承通知、派生异常/重入全部组合与跨平台仍缺 |
| FND-011 | 🟡 | 019～046句柄/消息/销毁边界与源窗口类身份缓存范围保留；048的Control-17d补停车期间WM_COMMAND到已登记实际子控件的连接。重挂后EDIT未交付命令的现象已复现，原因未闭环，继续Control-17/TextBoxBase-006；全部派生状态恢复、完整反射、自定义Param AOT、关闭竞态与三平台仍缺，Created隐藏属性差异保留 |
| FND-012 | 🟡 | 002～025资源/释放范围保留；026的Control-08c补HWND已销毁后异常继续正常托管阶段，原错和后续错不丢失；正常Parent/child/Site失败及活HWND失败保留短路重试。其余图像/菜单/派生资源、完整异常事务/事件解绑与竞态仍缺，不由局部收尾关闭父项 |
| FND-013 | 🟡 | 018/022基础外观父链、023 Styles/UserPaint保留；024补BackgroundImage通知传播及基础可见父子透明/alpha；031补RightToLeft托管继承、Win32 RTL扩展样式和重建保持。Application/Site/ActiveX ambient、Cursor、滚动/主题/全部派生透明、DPI和完整Designer默认仍缺；默认palette三条前景差异保留 |
| FND-014 | 🟡 | 009～021基础Bounds/通知/客户区/MinMax/默认布局与测量保留；035修正AutoSizeMode数值，038补UserControl标准AutoSize属性/事件公开并继续复用共享布局。043的WordWrap与046的BorderStyle复用条件父布局/首选缓存失效；完整派生测量/约束/RTL/DPI/重入、继承属性和平台仍缺 |
| FND-015 | 🟡 | 020标准事务10b、021标准引擎/缓存/InitLayout与Resume(false)协作10c完成；512行参考/JIT/实际AOT同，旧394行参考/JIT差异清零。非Control布局对象、复杂缓存重入/异常恢复、完整容器算法/Designer仍缺，不关闭父项 |
| FND-016 | 🟡 | 018已有ScrollableControl无滚动客户区DisplayRectangle；031完成托管AutoScroll/ScrollProperties/Scroll事件及受保护契约，032补Panel迁移和Windows SCROLLINFO/WM_HSCROLL/WM_VSCROLL首范围，033补已有偏移下的后代可见坐标修正。真实物理滚轮/触摸板、Dock/Anchor/Tab/RTL组合、嵌套滚动事务与Linux/macOS滚动仍缺 |
| FND-017 | 🟡 | 历史Form已有AutoScaleMode/Dimensions/PerformAutoScale算法，018迁入ContainerControl并回归；031补AutoScaleFactor首范围，纠正原全部未开始状态遗漏旧局部实现。完整Font/Dpi/None/Inherit、嵌套继承、DpiChanged父子通知与96/144/192往返仍缺 |
| FND-018 | 🟡 | 018～034导航/焦点范围保留；035补最深活动按钮默认通知与队列Enter/Escape首范围。全部派生、物理Tab/完整助记键/默认按钮生命周期和关闭焦点差异仍缺；公开Focus无参，定向入口为Select(bool,bool) |
| FND-019 | 🟡 | 030～041范围保留；042共享已有TextBox编辑键/只读密码IME前置，并核组合/SETCONTEXT/候选通知原生默认路由。043补AcceptsTab标准属性通知，不扩展完整键盘协议。完整Tab/助记键/命令、OLE宿主context/COM对象寿命、真实模态传播/组合串/候选UI、全部派生与设备仍缺，不由程序消息测试或最小线程状态称完整IME兼容 |
| FND-020 | 🟡 | 027完成Control-13a坐标/捕获、13b基础鼠标事件/消息跟踪，必要原生消费者共用Click且避免重复。验证取消、全部派生反射/默认样式/AutoCheck/组同步、RTL/DPI及物理设备仍缺；锁屏造成WindowFromPoint命中失败，不把程序消息结果计作成功设备点击 |
| FND-021 | 🟡 | 029程序验证/关闭前置范围保留；035的Button.PerformClick消费Prevent/Allow/Disable及CausesValidation=false；036补ValidationCancelled父链/鼠标抬起清理，真实HWND按钮Prevent/Allow回归确认原生通知不重复验证。完整物理点击/提交前置、全部派生默认/继承、真实输入及平台行为仍缺 |
| FND-022 | ⬜ | DataBindings、BindingContext、BindingSource：属性写回/通知/错误，保留现有 API；AOT 绑定策略先通过真实绑定样例再推广 |
| FND-023 | 🟡 | 023消息/样式/基础Paint保留；024的14b补背景图像五布局、真实屏幕透明/alpha及父Graphics/DC状态、失效传播，21场景307行JIT/AOT全同（参考关闭差异16行另列）。RTL/mirror、滚动/主题透明、完整owner draw/反射/派生绘制、缓冲失败与资源压力仍缺，父项不关闭 |
| FND-024 | ⬜ | AllowDrop/DragEnter/Over/Leave/Drop、DoDragDrop、GiveFeedback/QueryContinueDrag、Clipboard/DataObject；平台格式与内存所有权有测试 |
| FND-025 | ⬜ | AccessibleName/Description/Role、AccessibilityObject/CreateAccessibilityInstance、键盘焦点与选择通知；UIA/AT-SPI/NSAccessibility 均由对应后端验证 |
| FND-026 | 🟡 | 038的UserControl-02d补AutoSize属性/事件的标准公开与设计器序列化声明，限定元数据不等于Designer往返通过。ISupportInitialize/BeginInit/EndInit、IExtenderProvider、其余DefaultValue/DefaultEvent/DesignerSerializationVisibility、.resx/文化/图像/图标及保存/关闭/重开设计器仍待 |

## 收尾状态整理记录

- 2026-11-07 · 批次048：只新增TextBoxBase-005b/005c与Control-17d，TextBox-002/009按真实继承交叉引用，Rich/Masked不计专有支持。重挂后编辑通知是新发现未完范围，保留诊断并进入原父任务，不因43新增通过或缩小测试范围称完整重挂兼容；原ID/1,095类型/61,240成员行和固定分母保留。README实施列/tasks.csv由既有工具生成，见[048](batches/048.md)。

- 2026-11-07 · 批次047：只新增TextBoxBase-002b/002c/003c，完整002/003/005父项保持部分；TextBox-009因真实继承SelectedText/MaxLength从全待办纠正为部分，002同步Lines范围。Rich/Masked原父项不关闭，API固定成员行/原ID/1,095类型/61,240成员行保持；README实施列/tasks.csv由既有工具生成，见[047](batches/047.md)。

- 2026-11-07 · 批次044：共同HideSelection标准设置/通知与TextBox对齐创建样式完成明确范围；HorizontalAlignment按固定10.0.11修正，旧二进制/整数1/2持久化须复核。FND-001/013/014/019等父项保持部分，Control源码no-op，原生RTL夹具只证既有创建管线的样式消费；完整设备/Designer/AOT/平台/98%不关闭，见[044](batches/044.md)。

- 2026-11-07 · 批次043：TextBoxBase-003a补共同设置/标准AcceptsTabChanged，TextBox-004a使滚动条与折行真实生效；同值WordWrap仍清首选缓存并条件参与父布局。FND-014/019及完整继承/编辑/派生后端父项保留部分；不把原生EDIT样式/行数回归称设备/Designer/AOT/平台验收，见[043](batches/043.md)。

- 2026-11-07 · 批次042：FND-001/019按实际TextBoxBase共享首范围与IME原生路由审计更新；TextBoxBase原001～009及TextBox-001由全待办改部分，完整父项不关闭。历史API类型缺失快照与当前源码分开，原ID/1,095类型/61,240成员行保留，见[042](batches/042.md)。

- 2026-11-07 · 批次041：FND-019增加OLE前置和限定退出配对，Application-12按真实OleRequired实现由全待办改部分，原ID/固定分母保留。没有实现完整ThreadContext/模态或自然线程终止清理；原始夹具失败、其他HRESULT失败策略及MUI有界差异明确保留，见[041](batches/041.md)。

- 2026-11-07 · 批次040：FND-019补HKL服务与标准语言通知/取消；Application-11由全待办改部分。固定源码OleRequired的持久前置尚无完整线程context，明确保留差异，未用空方法或不平衡初始化掩盖。Form/Control/附属契约和所有未完父项/原ID/固定分母保留。

- 2026-11-07 · 批次039：FND-019补TextBox-005a与Control-12l，父项仍部分；TextBox-005由全待办改部分，明确此前已有ReadOnly原生映射及本批必要密码同步，完整复制限制/UIA/AutoComplete仍缺。旧028无句柄焦点用例显式隔离/还原未初始化IME传播缓存，保留原断言，非删除历史差异；固定分母/原ID不变。

- 2026-11-07 · 批次038：FND-019新增Control-12k上下文和TextBox-003c生命周期前置，并纠正父表仍遗漏037的记录；FND-014/026明确UserControl-02d仅标准声明/事件与共享布局消费，不关闭完整布局或Designer。TextBoxBase缺失保留，但其“现有TextBox无句柄Select直接返回”旧描述按003c纠正，固定分母/原ID不变。

- 2026-11-07 · 批次037：FND-019补Control-12j有限IME字符消息与TextBox-003b派生键规则；FND-021由UserControl-05a覆盖嵌套焦点/验证有限组合。补录036原生输入判别、字符队列抑制与验证取消清理，纠正公共父项滞后，未关闭完整IME/验证/平台。

- 2026-11-07 · 批次035：FND-018/019/021吸收实际队列及默认/取消按钮必要验证消费，FND-014基础AutoSize命名行为在枚举数值修正后保留。仅明确子项代码完成，完整IME/原生点击/布局/设备/平台仍待；同步Button原全待办但已有局部实现与索引029停止状态滞后，见[035](batches/035.md)。

- 2026-10-26 · 批次031：FND-013补RightToLeft托管/Win32样式首范围；FND-016补AutoScroll/ScrollProperties/Scroll事件/受保护滚动契约首范围；FND-017补AutoScaleFactor；UserControl接入标准ContainerControl继承。所有父任务仍保持🟡，真实滚动条、Cursor、完整缩放/设计器、派生与平台门禁保留，见[031](batches/031.md)。
- 2026-10-26 · 批次032：FND-016补Panel迁移与Windows SCROLLINFO/WM_HSCROLL/WM_VSCROLL首范围；UserControl补Load一次触发。真实物理设备、完整滚动/Panel属性、Designer/AOT和平台仍待，见[032](batches/032.md)。

- 2026-11-07 · 批次033：FND-019补Control键盘预处理首范围，ContainerControl补Tab/助记键后代路由，FND-016补已有偏移下的后代可见坐标修正；完整输入、物理设备、Designer/AOT和平台仍待，见[033](batches/033.md)。

- 2026-10-26 · 批次030：FND-019回标为部分完成，新增Control-12e基础键消息/事件契约与5项Windows STA回归；完整键盘预处理、IME、真实设备、Designer、AOT及跨平台仍待办，见[030](batches/030.md)。

- 2026-10-17 · 批次029：验证正式归FND-021，纠正准备说明只沿FND-018/019而漏记验证任务；FND-019键盘/IME仍待办。有限程序验证/自动转移完成，关闭LostFocus/WM_QUIT及最终JIT-AOT差异保留，不关闭完整输入/验证，见[029](batches/029.md)。

- 2026-10-16 · 批次028：FND-018补基础选择/事件/活动链范围，FND-019键盘与验证前置仍未完成；纠正非标准Focus(bool)旧表述及Form局部能力漏标。4,343测试/30场景JIT-AOT全同不等于98%；12个参考差异、物理设备/Designer/跨平台边界保留，见[028](batches/028.md)。

- 2026-10-16 · 批次027：FND-020按真实坐标/捕获/鼠标基础实现改部分；CNS补标准参数/枚举，Control-12、CheckBox-004、RadioButton-003及RadioButton父链的过时状态已校正。4,289测试通过、25共同场景保留5项参考/1项AOT差异及锁屏命中限制；不关闭父项/平台，见[027](batches/027.md)。

- 2026-10-16 · 批次026：FND-002/004/012随05c/08c及实际Demo AOT更新；4,243测试、32场景JIT/AOT全同。固定参考25场景有9项关闭/释放增强差异，旧004/005各两行差异保留严格失败，025历史4差异保持。原生失败后后续托管错误仍短路，完整竞态/资源/设计器/平台不关闭；见[026](batches/026.md)。

- 2026-10-16 · 批次025：FND-002/004/011/012按普通Drain和native callback边界实际完成范围更新；保留InvokeAsync关闭、Dispose后续托管事务、完整Control/Form/Designer/资源与GTK4/AppKit缺口。8场景参考/JIT/AOT全部正常退出，4项真实差异保留，不由测试数或局部边界关闭父项，见[025](batches/025.md)。

- 2026-10-16 · 批次024：FND-002/011/012/013/023按04b/14b实际完成范围更新，修正“Close仍中断余队列”“背景图像与可见透明全缺”的过时描述。独立列出普通Drain、InvokeAsync关闭、Dispose后续托管清理和原生双错缺口；本批Windows屏幕证据不扩为Designer/全派生/跨平台验收，ID及固定分母不变，见[024](batches/024.md)。

- 2026-10-16 · 批次023：FND-023更新为标准消息/样式/基础绘制已实现明确子范围，CNS补Message/CreateParams/ControlStyles及Paint/Invalidate参数委托。修正“标准绘制全缺”的当前描述，保留所有未完父任务、ID和固定分母；原生Form消息及销毁边界本批修复，完整Control/平台未关闭，见[023](batches/023.md)。

- 2026-10-16 · 批次022：FND-012/013补基础字体寿命与外观父链子范围，FND-023/Control-14纠正已有Invalidate/内部窗口过程仍全待办；GroupBox标准Controls/部分默认、Form继承缩放/Win32、TabControl与条带原生实现回标部分。全部未完父项、ID/固定分母及真实Styles/theme差异保留，见[022](batches/022.md)。

- 2026-10-16 · 批次021：FND-014/015按真实默认布局/首选尺寸及引擎缓存子范围更新；CNS-008不再全待办。Control-17的既有Win32实现、Flow/Table有限测量从全待办改部分；020七行差异已修但原始历史保留。全部父项/平台/98%未关闭，见[021](batches/021.md)。

- 2026-10-16 · 批次020：FND-015改为已完成标准事务首范围而保留引擎/缓存/几何缺口，FND-014补必要属性通知。纠正Panel对018标准层/集合的遗漏、Form.Layout的过时状态、CNS事件/集合的历史遗漏；只有明确事务子任务代码完成，完整控件/平台和98%均未关闭，详见[020](batches/020.md)。

- 2026-10-16 · 批次019：FND-001/002/010～012补标准句柄和Form必要显示衔接，纠正“Handle仍非延迟/入口不存在”和“018重挂可见消息仍未修复”的当前状态；018历史快照与失败证据保留。FND-015按实际空实现明确下一批依赖，其他完整资源/布局/线程/Designer/平台父项仍部分或待办，不把204条运行对照或基类完成转成整类型/98%声明，见[019](batches/019.md)。

- 2026-10-16 · 批次018：按真实代码补Control-06a/Form-01/ContainerControl-01/ScrollableControl-01a完成子范围，更新原“Parent/Controls/Form链及标准容器不存在”的过时描述。FND-010～018按实际部分实现回标，保留全部剩余任务、原ID/分母；特别纠正FND-017遗漏历史Form局部缩放的状态。完整句柄/布局/滚动/验证、Designer/资源和GTK4/AppKit均未关闭，运行证据见[018](batches/018.md)。

- 2026-10-16 · 批次017：FND-003补未挂接ImageIndex/ImageKey首范围，父任务仍为部分完成。按实际代码核对Control-11/11a及ListViewItem-04/04b，已完成子项单列，ImageList/native/转换器/编辑器与完整所属保留；不把旧“图像全部待办”继续当作当前未挂接状态缺口，也不关闭完整图像功能。保留001～016历史快照、ID及固定分母，无新整控件或平台验收。

- 2026-10-16 · 批次 016：按实际代码与本批必要检查更新 FND-001/003 的已完成子范围，父任务仍 🟡；FND-014 原“待办”遗漏了 009～016 已完成的基础边界/客户区/约束子范围，纠正为 🟡 并保留布局等剩余项。没有把原父任务误标为 ✅，没有删除/重编号未完项，FND-004 真机主窗和 FND-006 既有 IL3050/完整资源仍待完成。此处为状态整理，不新增运行验收或98%声明。

## 三平台共同原则

Windows 以现有 Win32 HWND 与标准控件为起点，复用成熟的系统输入/可访问性能力。GTK4 使用 GtkWidget/GtkWindow/GMainContext，AppKit 使用 NSView/NSWindow/NSApplication；库内部将信号、target-action、delegate 转成现有 WinForms 事件。托管集合、布局和格式规则可共享，公共 Handle/WndProc/Windows 消息不能在其他平台假造为等价。

Linux Wayland 不保证任意窗口位置/全局坐标；macOS 的应用菜单、首响应者、工作表模态和主线程规则不同。逐控件文档记录这些差异，不为 Windows 外观造自绘框架。所有平台原型先过字体/图像公共类型、AOT 回调、主循环、输入法、可访问性和分发验证，参见 [平台计划](platforms.md)。

依据：本地 [Control.cs](../../Lumina.Forms/Control.cs)、[Form.cs](../../Lumina.Forms/Form.cs)、[探针](../assessments/2026-10-15/runtime-probes.json)；[微软 Control](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.control?view=windowsdesktop-10.0)、[事件顺序](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/forms/events)、[GTK Window](https://docs.gtk.org/gtk4/class.Window.html)、[AppKit NSWindow](https://developer.apple.com/documentation/appkit/nswindow)。
