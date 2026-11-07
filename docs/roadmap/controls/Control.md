# Control

062本类源码no-op；Rich的003b/004b复用现有SendMessage/屏幕DC度量和句柄连接，不新增Control完成任务。110新增/5,959全量首轮通过；完整反射父寿命/WM_NOTIFY、线程/缩放/平台保持待办，见[062](../batches/062.md)。下述061及以前为历史。

061本类源码no-op；Rich字符/段落格式复用现有SendMessage和句柄连接，不新增Control完成任务。完整反射父寿命/WM_NOTIFY、线程/缩放/平台保持待办，见[061](../batches/061.md)。下述060及以前为历史。

060本类源码no-op；Rich RTF流复用既有SendMessage/句柄寿命与选择连接，不增加Control公共API或完成任务。完整WM_NOTIFY/反射父寿命、线程/缩放/平台仍待，见[060](../batches/060.md)。下述059及以前为历史。

059仅将既有GetAnyDisposingInHierarchy改internal供Rich Text getter读取，同一父链判断算法不变；不新增公共API或Control完成范围。Rich静态流回调/逻辑模型归其002a/002b，完整生命周期/反射/线程/缩放及平台仍待，见[059](../batches/059.md)。下述058及以前为历史。

058源码no-op，RichTextBox沿现有NativeWindowClass复制真实系统RichEdit类并保留原过程；不增加Control/interop完成范围。独立模块/原过程与RichEdit特有消息对照归RichTextBox-001a，完整反射父寿命/WM_NOTIFY、DPI/线程/模态及平台门槛保持，见[058](../batches/058.md)。下述057及以前为历史。

057源码no-op，基类002d直接消费现有GetWindowTextLengthW，003h的Paint声明继续转发Control.Paint。既有OnPaint直接调用事件（CanRaiseEvents=false仍调用）保留；不将声明元数据、借用Graphics或普通EDIT长度检查计为完整绘制/Designer/输入完成，不新增Control完成任务，见[057](../batches/057.md)。

056源码no-op，TextBoxBase-003f关闭通用标准点击样式并由专用OnMouseUp生成事件；复用现有Control鼠标finally清捕获/验证状态。必要成员转发归003g，不新增Control完成任务。23项点击契约及23项元数据测试通过，但独立WindowFromPoint均false，正向可见命中/物理输入与完整Control-13仍待，见[056](../batches/056.md)。

055仅复用054的独立控件DPI读取和线程PMv2门禁，Win32新增SM2/3滚动条度量服务TextBox-004b；Control.cs源码no-op，不新增Control完成任务。固定RecreateHandle无句柄直接返回的缓存边界保留；完整缩放/跨屏事件/点击样式等原父项不关闭，见[055](../batches/055.md)。

054为TextBoxBase-007c补必要DPI读取：构造在虚DefaultSize前保存独立DeviceDpiInternal，PMv2窗口创建后按真实HWND读取；公开非virtual DeviceDpi只在线程PMv2时投影控件值，其他线程返回初始系统DPI，元数据按固定源码。边框度量按当前线程感知选择系统API；只作为007c依赖记录，不重复新增完成任务。WmDpiChanged/BeforeParent、字体/常量重缩放、跨屏/Designer与完整DPI仍待，见[054](../batches/054.md)。

051完成14c必要内部redraw更新辅助：short嵌套计数，无窗Begin不积累，首Begin发WM_SETREDRAW(0)，末End先减计数再经Handle发1并按参数Invalidate自身；无配对End返回false。10例含有限回调/异常/销毁重建回归与5,119全量通过，未新增公开API。可见绘制、资源压力、完整14/生命周期及平台仍待，见[051](../batches/051.md)。下述049及以前为历史。

049完成17e的原停车通知接收者寿命及来源HWND反射：独立原生EDIT证实SetParent后EN_UPDATE/EN_CHANGE仍投向创建时父HWND，固定v10.0.11的ReflectParent计数保留该接收者。仅将确曾作为当前原生句柄创建父的停车窗保留至原句柄销毁；回调校验映射和自有原接收者归属，Form非零lParam按来源HWND反射，避免旧owner字典/ID碰撞。8项新增回归及4,954全量通过。原反射父Form销毁/重建的完整关系、WM_NOTIFY及完整context仍待，见[049](../batches/049.md)。下述048及以前为历史。

048完成17d必要停车期间WM_COMMAND转发：原裸STATIC不处理未挂父EDIT通知，标准AppendText建窗后暴露TextChanged缺失。复用NativeWindowClass注册无托管Control映射的停车窗回调，仅验证真实子HWND后进入既有HandleCommand；其他消息走DefWindowProc，异常沿既有native边界报告。停车窗归属/销毁方式保持。新增探针还发现重挂后EDIT未向Form/child交付后续命令，native ID一致但原因未闭环，继续17父项；完整消息反射、WM_NOTIFY及线程context仍待，见[048](../batches/048.md)。

046完成17c：系统窗口类先按当前activation context解析，再以源hInstance/lpfnWndProc身份参与superclass缓存，避免跨STA混用EDIT版本；双向context顺序/重复注册复用和旧RTL组合通过。Control.cs源码no-op，完整窗口后端/输入/生命周期仍待，见[046](../batches/046.md)。

🟡 **当前状态：**002～039明确子范围保留；040补12m输入语言消息向Form通知/请求取消与HKL服务必要连接。关闭多余LostFocus/WM_QUIT、完整命令对话框、OLE/完整输入语言服务、模态宿主/组合串/候选窗及全部派生输入、绘制/资源、Cursor/ambient、DPI/Designer、线程释放竞态及GTK4/AppKit仍缺；整Control保持部分，见[040](../batches/040.md)。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/Control.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：[Control.cs](../../../Lumina.Forms/Control.cs)
- 排序：全库 P0 根基；先于 ScrollableControl/ContainerControl 和所有具体控件。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ✅ | P1 / Control-12m | 批次040：WM_INPUTLANGCHANGE在已有上下文更新/unsupported传播后向FindForm发标准Changed，再默认过程；WM_INPUTLANGCHANGEREQUEST构造标准Changing参数并可取消，取消结果0且不入默认过程。无Form根保留默认转发，不新建句柄；Form14项及IME/焦点组合96项通过。限定共享消息连接，实际物理切换/组合输入与模态/OLE仍待。 |
| ✅ | P1 / Control-12l | 批次039：WM_INPUTLANGCHANGE先更新已有IME上下文、unsupported语言传播Off，再沿默认过程；request维持原生默认转发。非Form根失焦时恢复宿主传播模式后重置为Inherit，WM_IME_NOTIFY忽略守卫finally恢复，保持IME收尾→默认过程→LostFocus顺序。9项回归通过；Form语言事件/取消、Application输入语言服务、模态Form及实际中日韩/组合输入仍待，不关闭完整语言切换/宿主传播。 |
| ✅ | P1 / Control-12k | 批次038：标准ImeMode枚举/属性继承/reset/事件、CanEnableIme/DefaultImeMode/ImeModeBase/PropagatingImeMode扩展；受支持CJK语言的焦点/属性/WM_IME_NOTIFY接真实IMM上下文和转换位，自有HIMC独立清理、系统借用context不销毁，通知/释放双错保留。12项回归通过；TextBox只读/密码限制及属性联动、完整语言切换/宿主模态传播、组合串/候选窗、全部派生与设备仍归父项。 |
| ✅ | P1 / Control-12j | 批次037：WM_IME_CHAR直接进入ProcessKeyEventArgs而不经父KeyPreview，KeyPress消费/字符修改及原WParam保留；计数避免后续WM_CHAR/WM_SYSCHAR重复事件且保留默认过程，START清零/END关闭计数。7项回归通过；Handled后的计数按固定源码保留一次。ImeMode/context/组合串/候选窗及物理设备仍待，不关闭完整IME。 |
| ✅ | P1 / Control-12h | 批次035：Application实际键盘队列在Translate/Dispatch前按FromChildHandle调用PreProcessMessage，消费后停止、未消费保留四字段修改；异常经ThreadException报告。补内部ValidateActiveControl供Button消费；9项实际消息队列回归含默认取消按钮集成通过。直接SendMessage仍走WndProc，原生输入键/字符判别、队列字符抑制、IME和物理输入仍待。 |
| ✅ | P1 / Control-12i | 批次036：按固定10.0.11实现IsInputKey的Alt/Tab/箭头/默认WM_GETDLGCODE判别、IsInputChar的字符/Tab判别及WM_SYSCHAR直达对话字符；队列KeyDown的SuppressKeyPress按消息类型有界移除WM_CHAR/WM_SYSCHAR/WM_IME_CHAR，直接WndProc保留现有单次兼容路径。补ValidationCancelled父链读取、鼠标抬起取消点击守卫和状态清理；5项输入判别、2项真实按钮验证所需共享回归及既有键盘队列通过。完整IME/队列边界、全部派生和物理输入仍归父项。 |
| ✅ | P1 / Control-12g | 批次034：六类普通/system键消息经ProcessKeyMessage→父预览→ProcessKeyEventArgs，事件返回Handled且默认过程只在未消费时执行，KeyChar修改回写WParam；基础ProcessMnemonic默认false终止后代回父递归。7项新增回归通过；完整IME/队列字符抑制/物理输入与全部命令路由仍归12父项。 |
| 🟡 | P0 / Control-01 | 已完成Component/IWin32Window（01a）、ISynchronizeInvoke、基础DefaultSize/构造约束（01b）、无parent文本构造（01c）；018含parent构造和Controls归06a。IDropTarget、全部派生默认/扩展仍待实现，不随子项关闭。 |
| ✅ | P0 / Control-01a | 批次 002：真实无参构造、Component/IComponent/IDisposable/IWin32Window 消费与派生覆盖通过；基础 HWND 由原生 IsWindow/文本长度消费并在释放后失效。完整继承/构造/接口与延迟句柄语义另列待办，兼容验收后置。 |
| ✅ | P0 / Control-01b | 批次 015：基础无参 0×0 与 protected virtual DefaultSize 完成；构造期 DefaultMin/Max 在原始状态按参考重复读取并消费公共虚 setter，随后直接初始化 DefaultSize 和条件客户区，再安装同步上下文。2,677 条固定 10.0.11/JIT/实际 AOT 相同，387 项新增回归，覆盖四边框/正零负极值、冲突/重复读取、不调 base/重入/原异常及构造异常 Current 时序。其余构造、所有具体派生默认/DPI/Form/完整布局与兼容验收另列。 |
| ✅ | P0 / Control-01c | 批次 016：无 parent 的 text 与 text/left/top/width/height 两个标准公共构造完成，text 接受 null，Text 为标准 virtual/AllowNull；默认初始化/context 后消费虚文本，再分别进入 Location 和 Size，保留约束/通知/重入/原异常。4,584 条固定参考/JIT/实际 AOT 指定观测相同，311 项新增回归；构造空值契约与运行时调用约定编码单独保留原始证据，见批次记录。含 parent 构造、完整 Text/native/布局与 Form 继承另列。 |
| 🟡 | P0 / Control-02 | 008～010完成02a焦点查询、02b尺寸与02c边界虚入口；028的12a已完成基础Select/CanSelect，029补有限自动转移/验证。纠正此前仍将Select/CanSelect列缺失的滞后；完整键盘/派生布局仍待后续，不随子范围关闭。 |
| ✅ | P0 / Control-02a | 批次 008：标准 bool Focus() 与 virtual Focused、ContainsFocus/CanFocus 使用真实 GetFocus/IsChild/IsWindowVisible/IsWindowEnabled；无自身 HWND 不创建句柄，隐藏/禁用/释放与外部原生状态有对照。157 条固定参考/JIT/实际 AOT 相同，17 项新增 STA/元数据回归通过。完整选择、键盘/验证及重建竞态另列。 |
| ✅ | P0 / Control-02b | 批次 009：SetBounds 及 Width/Height/Size/Bounds 无自身 HWND 时保留零/负尺寸、不创建句柄；已有 HWND 时负数归零，实际零宽高可恢复。首次创建回读原生矩形，HandleCreated 已见归一结果。756 条固定参考/JIT/实际 AOT 相同，33 项新增回归通过。后续基础构造默认/MinMax/通知见已完成01b/c、09a/c、10a；全部派生约束、完整布局/DPI及外部自主通知仍缺。 |
| ✅ | P0 / Control-02c | 批次 010：标准 BoundsSpecified、四/五参数 SetBounds 的 x/y 名称与 protected virtual SetBoundsCore 实际接入；None/组合/未知位、重复值、属性标志、派生覆盖与直接 core 有 2,034 条固定参考/JIT/实际 AOT 对照，新增 60 项回归通过。外层保留未指定分量，core 接完整矩形，原始 specified 不丢；保留02b尺寸/创建语义。基础Min/Max、通知及客户区见已完成09a/c、10a；完整布局/全部派生约束/外部自主通知仍缺。 |
| 🟡 | P0 / Control-03 | 批次003完成现有HWND/创建线程与父级句柄查找（03a）；018接入真实Parent/Form标准链，019内部查询改用不创建句柄的NativeHandle并保留重建投递。未启动/已退出消息循环和完整句柄重建竞态仍待对照。 |
| ✅ | P0 / Control-03a | 批次 003：InvokeRequired 查询 HWND 创建线程，沿 Parent 及当前 Owner 找已有投递句柄；无句柄返回 false，Invoke/BeginInvoke 无句柄不创建并抛标准异常；父级/Owner 回退有实际线程对照。 |
| 🟡 | P0 / Control-04 | 003首范围04a、019重建保留队列、024关闭收尾04b及025普通Drain收尾04c完成；026的05c补已接受InvokeAsync任务关闭/投递失败终止。等待期间并发释放同一wait、完整关闭/重建/线程退出竞态及参数全部边界仍未关闭。 |
| ✅ | P0 / Control-04b | 批次024：Close及Enqueue/AttachWindow失败投递先完成全部已接收Invocation，再传播单原错/多错聚合；Post原Win32错误保留第一项及各EndInvoke。DestroyHandle遇Close错误仍真实销毁HWND。10项独立回归及同源JIT/AOT指定关闭场景通过，相对10.0.11的16行收尾差异明确保留。仅队列/实际HWND保证，不承诺Dispose后续托管树/Site/事件事务，普通Drain/InvokeAsync包装及全部竞态归父项。 |
| ✅ | P0 / Control-04c | 批次025：普通 `Drain` 在 `Invocation.Complete.Set` 失败后继续执行 FIFO 余队列，Drain 结束后传播单/多完成信号异常；完成信号成功时仍按标准路径报告未观察 callback 异常，重复 `EndInvoke` 保留原异常。4项独立队列回归通过；InvokeAsync关闭终止、并发释放和完整竞态仍归父项。 |
| ✅ | P0 / Control-04a | 批次 003：真实 PostMessage 队列、同线程 FIFO/EndInvoke、跨线程返回与异常、ExecutionContext、IAsyncResult 和销毁待执行项唤醒；69 条指定观测在固定 WinForms 10.0.11、JIT、AOT 相同。null Delegate 保护与默认异常 UI 的明确差异见修改历史，不等于父任务完整验收。 |
| 🟡 | P0 / Control-05 | 004～007标准异步入口、旧路径与context首范围保留；026的05c补已接受但未执行任务的Close/Post/Attach失败终止。完整关闭/重建/退出、多UI/模态竞态仍未关闭。 |
| ✅ | P0 / Control-05c | 批次026：Invocation失败通知桥接标准Action/Func<T>/ValueTask/ValueTask<T>及历史Task，Close/失败Post/Attach先结算已接受未执行任务；取消先完成仍保留token，关闭先完成后取消不覆盖，已开始工作独立完成、ValueTask仅消费一次且不重复ThreadException。28项独立回归及Windows JIT/AOT证据；相对10.0.11的关闭增强、旧004/005各两行差异与严格失败保留，完整退出/竞争仍归父项。 |
| ✅ | P0 / Control-05a | 批次 004：标准 Action/Func<T> 在自身 HWND 线程排队，返回值、原异常及取消保留；旧单 Action/Task 回调也排队。固定 WinForms 10.0.11、JIT、AOT 59 条观测相同，另有旧 Task 路径专项检查；ValueTask/同步上下文与完整关闭竞态未关闭。 |
| ✅ | P0 / Control-05b | 批次 005：标准 Func<CancellationToken,ValueTask> / ValueTask<T> 在 UI 队列首次调用并单次消费，90 条固定参考/JIT/AOT 观测相同；原异常/结果/token、取消后继续消费及 async lambda 选择已检。同步上下文安装、callback 内 await 后 UI 恢复和完整关闭竞态另列，不代表完整异步兼容。 |
| 🟡 | P0 / Control-06 | 018完成06a标准父子集合与Windows重挂首范围；Form直接子项Parent为真实Form，共用ControlCollection。019修复隐藏父级重挂时中间VisibleChanged，原391行参考/JIT现全部相同；018原始失败记录保留。020已接基础布局事务；完整布局引擎/继承属性通知、派生重入全部组合与三平台仍缺。 |
| ✅ | P0 / Control-06a | 批次018：ControlCollection/ArrangedElementCollection、Parent可设与含parent构造、Add/AddRange/Remove/Clear/Find/键和索引/复制/枚举/Clone/排序、ParentChanged与ControlAdded/Removed、Contains/FindForm/TopLevelControl完成；重复添加/循环父子/线程限制、直接Form父级、真实HWND移动/停放/重挂保留句柄和Z序有固定参考。精确范围与运行证据见018；完整布局/ambient通知和Handle延迟/重建另列。 |
| 🟡 | P1 / Control-07 | 019的07a、023的17a及025的07b保留，024的04b保证预先Close失败仍销毁实际HWND。native双错边界已覆盖有限范围；所有派生重建属性/焦点/资源恢复、全部关闭/线程竞态与GTK4/AppKit生命周期仍待原任务及17～19。 029记录正常关闭多余LostFocus，根因尚未定位，不能通过关闭期间抑制事件消差异。 |
| ✅ | P1 / Control-07a | 批次019：get-only延迟Handle、独立Created/IsHandleCreated/RecreatingHandle、CreateControl与protected CreateHandle/DestroyHandle/RecreateHandle、OnCreateControl/OnHandleCreated/OnHandleDestroyed及查找映射完成。Windows普通/隐藏/可见树、直接/外部销毁、父子重建、销毁事件中的映射寿命、重挂可见消息、已排队回调保留、创建重入/原异常/Dispose覆盖首范围有204行固定10.0.11/JIT/实际AOT严格对照；完整子范围见019，剩余派生/资源/竞态与平台见父项。 |
| ✅ | P1 / Control-07b | 批次025：外部 `WM_NCDESTROY` 中 callback 与映射移除、dispatcher/句柄清理异常逐项保留并分别报告；Control/Form 的 GCHandle、资源和窗口状态继续完成收尾。固定探针覆盖 Control/Form 双错场景，Lumina 当前销毁待执行 callback 的 `ObjectDisposedException` 差异保留。完整Dispose事务、重建恢复和关闭竞态仍归父项。 |
| 🟡 | P1 / Control-08 | 002的08a、018/019父子与句柄清理、022的08b、024的14b借用图像保留；026的08c补实际HWND销毁后异常的托管收尾。其余Image/菜单所有权、完整派生清理/事件解绑/异常事务仍缺；普通Parent/child/Site异常及活HWND失败仍按原短路留待重试，不声明全部Dispose事务。 |
| ✅ | P1 / Control-08c | 批次026：ReleaseNativeResources抛错且NativeHandle==0时继续原Parent→OnDisposing→children→Component顺序，单错EDI保留、多个错误按发生顺序聚合；继续阶段遇Parent/child/Site错仍按原规则短路，不伪造Disposed。活HWND、无原生故障、重复/重入和Dispose(false)保留既有边界。20项回归含真实Control/Form关闭，固定参考5个native-after场景有明确增强差异；完整资源/事务/并发仍归父项。 |
| ✅ | P1 / Control-08b | 批次022：基础Control及必要消费控件各自持有官方Font.ToHfont生成的HFONT，向HWND设置替代句柄后再释放旧句柄，真实销毁及无窗Dispose释放；托管Font保留借用引用不克隆/Dispose。Form重建失败时停放子窗字体仍存活并可更新；TextBox移除隐藏Font与自有释放路径。完整DPI/UserPaint/派生资源/平台仍归父项。 |
| ✅ | P1 / Control-08a | 批次 002：继承 Component.Dispose 并覆盖 Dispose(bool)，保留派生 OnDisposing；Site 移除、组件容器级联、重复/重入、Dispose(false)、异常传播和状态退出均有回归。5 项生命周期观测与本机 WinForms 10.0.11 相同；不等同完整控件生命周期验收。 |
| 🟡 | P1 / Control-09 | 012～014客户区/换算/运行期MinMax与015构造默认保留；021完成09d基础默认Dock/Anchor/AutoSize、PreferredSize与指定边界恢复。完整派生测量/布局、DPI/RTL、复杂布局重入/集合修改和BringToFront/SendToBack仍缺；020的Dock/Split七行差异已由021修复，历史原始失败保留。 |
| ✅ | P1 / Control-09a | 批次 012：标准 ClientSize/ClientRectangle、ClientSizeChanged 与 protected virtual SetClientSizeCore/OnClientSizeChanged 完成；明确初始化基础 Control、四有限边框样式、无/有 HWND、零负/恢复、顺序/覆盖/重入/异常共 5,556 条固定参考/JIT/实际 AOT 相同，80 项新增回归通过。保留原始客户区请求缓存与重复通知；客户区缓存变化可使仅位置操作触发标准尺寸通知。旧边界/通知/原生高度扩展保留；基础构造默认与SizeFromClientSize见已完成01b/c、09b，全部派生约束/外部自主通知/完整布局/Form生命周期仍缺。 |
| ✅ | P1 / Control-09b | 批次 013：标准 protected virtual Size SizeFromClientSize(Size clientSize) 与四有限边框、无/有 HWND、正零负极值/unchecked 溢出、覆盖/重入/原异常完成；10,086 条固定参考/JIT/实际 AOT 相同，257 项新增回归通过。直接 base 换算不改状态/客户区缓存/通知/句柄，ClientSize 与 SetClientSizeCore 按参考不消费该虚扩展；原始客户区请求缓存和既有边界/通知保持。完整布局/全部派生约束/DPI 切换/Form 生命周期另列。 |
| ✅ | P1 / Control-09c | 批次 014 运行期首范围：基础 virtual MinimumSize/MaximumSize 与 protected virtual 默认扩展接入真实尺寸 core；四有限边框、无/有 HWND、负零/冲突/重复/取消、12 入口、覆盖/重入/原异常/通知与缓存共 33,408 条固定参考/JIT/实际 AOT 相同，新增 394 项回归通过。构造期默认约束与 DefaultSize 由 01b 一起处理；本批元数据仅固定英文范围，中文 Designer 分类/描述、AOT 动态 AmbientValueAttribute.Value 转换、完整布局/全部派生约束/DPI/Form 生命周期另列。 |
| ✅ | P1 / Control-09d | 批次021基础默认几何：反序Dock、legacy默认及显式AnchorLayoutV2、AutoSize的GrowOnly/GrowAndShrink、Dock↔Anchor切换/指定边界恢复、PreferredSize/virtual GetPreferredSize、MinMax与必要缓存失效完成；普通Control.DisplayRectangle为未扣Padding的ClientRectangle，容器自行消费Padding。18场景512行固定10.0.11/JIT/实际AOT严格相同，含真实HWND矩形。范围限基础Control及本批列出的必要容器；全部派生测量、DPI/RTL及复杂重入组合保留。 |
| 🟡 | P1 / Control-10 | 011/012通知和020标准布局事务保留；021完成10c标准LayoutEngine扩展、PreferredSize缓存及InitLayout/ResumeLayout(false)引擎协作。非Control布局对象、DPI锚定恢复、全部派生布局/外部原生自主通知、复杂多子项缓存重入与异常恢复仍待原任务，父项不关闭。 |
| ✅ | P1 / Control-10b | 批次020完成基础非virtual SuspendLayout/ResumeLayout/PerformLayout入口、protected OnLayout及标准参数/委托、嵌套延期/缓存/重入/原异常及必要属性/父子事务。020原394行JIT/AOT同、参考7行差异保留为历史；021同源394行参考/JIT全同。本子项只计事务状态与调用契约，引擎/缓存由10c补齐。 |
| ✅ | P1 / Control-10c | 批次021：标准Layout.LayoutEngine及Control.LayoutEngine扩展真实参与布局；OnLayout先事件再引擎，返回true请求父PreferredSize布局；InitLayout、ResumeLayout(false)、子项边界/属性/集合变更协调初始化及首选缓存。自定义引擎调用/返回/异常、默认引擎、缓存与指定边界有512行对照及13项核心回归。非Control arranged对象、复杂缓存共享重入和完整设计器往返另列，不以标准基类默认无几何策略代替实际DefaultLayout。 |
| ✅ | P1 / Control-10a | 批次 011：标准 LocationChanged/Move、SizeChanged/Resize 与四个 protected virtual On* 已接入七属性/两族 SetBounds/直接 core；重复、指定/未知位、零负尺寸、覆盖/不调 base、重入/原异常和参数轨迹共 2,280 条固定参考/JIT/实际 AOT 相同，99 项新增回归通过。OnSizeChanged 向 OnResize 传 Empty，SizeChanged 保留原参数；旧OnBoundsChanged独立保留。基础ClientSizeChanged/客户区见已完成09a，外部原生自主移动/尺寸通知、完整Layout及全部派生约束仍缺。 |
| 🟡 | P1 / Control-11 | 016/017 Text/ResetText、018 Enabled/Visible保留；022完成11b基础Font/BackColor/ForeColor局部值及父级继承；023实现基础ControlStyles及透明背景调用首范围；031补11c RightToLeft托管继承、Win32 RTL扩展样式和AutoScrollOffset基础契约。Application默认字体/Site/ActiveX ambient、Cursor、可见透明合成、全部事件与完整native文本同步仍缺；主题默认值、RTL镜像/DPI和WinForms视觉差异保留，不随子范围关闭。 |
| ✅ | P1 / Control-11b | 批次022：virtual Font/BackColor/ForeColor、三默认值/Reset、internal virtual ShouldSerialize、FontHeight缓存、标准On*/OnParent*及FontChanged、局部相等/身份、父级重挂、通知顺序/重入/原异常/释放后托管行为和必要布局缓存协作完成。Font沿父内部存储继承，颜色消费父虚getter；主题不向公共属性回写。仅基础托管父链与本批必要消费范围，全部ambient/DPI/UserPaint/Designer另列；212行探针完整原始差分见022。 |
| ✅ | P1 / Control-11a | 批次017：标准virtual ResetText经虚Text setter，null归空、虚getter判等、重复赋值/身份、事件/覆盖/隐藏/不调base/重入/原异常及释放后托管读写完成；219项新增回归通过。3,774条固定10.0.11参考/JIT全同；实际AOT仅Text的DispId属性被工具链删除，其他3,773条原始记录全同，原差异和运行时策略单列，不能称AOT元数据全同。已有HWND/native、父级继承、完整布局/中文Designer及动态转换另列。 |
| ✅ | P1 / Control-11c | 批次031：标准 `RightToLeft` 枚举、属性、Reset/ShouldSerialize、事件槽和父级继承通知；有效值接入 `WS_EX_LAYOUTRTL`/`WS_EX_RTLREADING`，显式 No 使用 `WS_EX_NOINHERITLAYOUT`，句柄创建/重建保持一致；补齐标准 `AutoScrollOffset` 供滚动容器消费。完整 Cursor、RTL镜像/DPI、ambient/Designer及全部派生控件仍归11父项。 |
| 🟡 | P1 / Control-12 | 028～039范围保留；040的12m补标准Form语言通知/取消，Application-11a提供真实HKL服务。全部派生/ToolStripControlHost、完整原生验证组合、Tab/助记键/命令、OLE/完整输入语言服务、模态宿主/组合串/候选窗和物理输入仍缺。 |
| ✅ | P1 / Control-12a | 批次028：CanSelect、GetContainerControl、GetNextControl、Select()/protected Select(bool,bool)、SelectNextControl的基础托管导航；按TabIndex/集合顺序遍历、容器边界、方向/TabStop/nested/wrap过滤及虚派发。普通Control/ContainerControl/Form指定场景有固定参考；ToolStripControlHost特殊反向遍历、标准派生容器继承、自动转移/验证另列于12父项。 |
| ✅ | P1 / Control-12b | 批次028：Enter/Leave改用组件事件槽，OnEnter/OnLeave对齐protected internal；补GotFocus/LostFocus、On*/Invoke*及WM_SETFOCUS/WM_KILLFOCUS的默认过程顺序，Focus成功后更新活动链。订退/CanRaiseEvents/原异常、无HWND逻辑选择与实际HWND程序焦点有证据；完整验证、设备及派生焦点处理不在该有限范围。 |
| ✅ | P1 / Control-12c | 批次029：SelectNextIfFocused从父容器使用标准SelectNextControl；Enabled提交状态后、Visible提交前触发导航，无候选不自造清空。TabStop变更同步WS_TABSTOP并经组件事件槽发TabStopChanged；指定当前/祖先、TabStop/Selectable及嵌套候选轨迹有固定参考/JIT/AOT证据。关闭阶段差异归07/Form-07。 |
| ✅ | P1 / Control-12d | 批次029：CausesValidation默认true及Changed、Validating/Validated和protected On*接入组件事件槽；验证取消跳过Validated，CausesValidation=false跳过验证，Validating原异常透传，批量Validated原异常透传、普通路径报ThreadException。只完成标准基础消费，完整输入提交/调试异常策略/派生覆盖仍归12及FND-021。 |
| ✅ | P1 / Control-12e | 批次030：新增KeyEventArgs/KeyPressEventArgs及标准委托，Control事件槽与On*接入WM_KEYDOWN/WM_SYSKEYDOWN/WM_CHAR/WM_KEYUP/WM_SYSKEYUP，保留修饰键、Handled和SuppressKeyPress的基础托管语义。仅完成消息注入首范围；PreviewKeyDown、命令/对话框预处理、KeyPreview、完整Tab/助记键/IME、真实设备和AOT仍归父项与FND-019。 |
| ✅ | P1 / Control-12f | 批次033：增加PreviewKeyDown/PreviewKeyDownEventArgs、IsInputKey/IsInputChar、ProcessCmdKey/ProcessDialogKey/ProcessDialogChar/ProcessKeyPreview/ProcessKeyMessage/ProcessKeyEventArgs/ProcessMnemonic及public PreProcessMessage首范围；键消息先命令路由再预览和输入/对话框分派，字符消息按输入字符或对话字符分派。完整KeyPreview、IME、默认/取消按钮、完整Tab/助记键、派生路由和真实设备仍归12/FND-019。 |
| 🟡 | P1 / Control-13 | 027完成13a坐标/捕获和13b基础鼠标消息/事件；必要消费者共享Click去重。验证取消与点击协作、全部派生反射/默认激活/AutoCheck/组同步、RTL/DPI/非客户区及完整捕获竞态仍待原任务。锁屏下10个参考、20个JIT/AOT命中观察失败，真实命中/物理设备交互未验收，不能由事件入口或消息注入关闭完整输入。 |
| ✅ | P1 / Control-13a | 批次027：PointToClient/PointToScreen、RectangleToClient/RectangleToScreen按真实MapWindowPoints转换（矩形两点）；Capture查询/获取/转移/释放与MouseCaptureChanged，静态MouseButtons/MousePosition接原生状态。无/有句柄、负坐标、父子、释放与重建指定范围完成；RTL镜像/DPI/Wayland等完整语义与物理设备验收仍归父项。 |
| ✅ | P1 / Control-13b | 批次027：标准Click/DoubleClick及鼠标事件共用Component.Events、对应虚On*、InvokeOnClick/RaiseMouseEvent/ResetMouseEventArgs；MouseEventArgs.Location/可继承、HandledMouseEventArgs与XButton1/2；五按钮/双击/有符号坐标/滚轮Handled及原生Enter/Hover/Leave跟踪。跟踪在虚WndProc之外，保留直接调用区别、重建pending-enter、有限销毁和Up后的捕获清理。范围限基础Control消息路径及上述契约；验证取消、所有派生路由与设备验收不计完成，原异常文本差异保留。 |
| 🟡 | P1 / Control-14 | 023的14a、024的14b完成基础背景图像/真实HWND透明绘制，051的14c补词删除必要内部redraw计数；RTL/mirror、滚动viewport、TabPage主题透明、全部派生owner draw/消息反射/缓冲分配失败与资源压力仍缺。不能由基础Control/消息回归关闭完整绘制，022/023历史原始差异保留。 |
| ✅ | P1 / Control-14c | 批次051：内部BeginUpdateInternal及EndUpdateInternal两重载，private short计数；无窗Begin不计数/建窗，首Begin发WM_SETREDRAW(0)，嵌套End返回true且末次先减到0再经Handle发1，可选Invalidate自身，无配对End返回false。10例核消息参数/有限回调计数顺序、默认与false失效行为/原异常、销毁后Handle重建及创建失败后的零计数。测试截获redraw消息，真实可见绘制未验；不新增公开更新模型或关闭完整14/17。 |
| ✅ | P1 / Control-14b | 批次024：virtual BackgroundImage/Layout、ImageLayout五值、两事件/三个On*、通知传播及借用图像寿命；Tile/None/Center/Stretch/Zoom、基础真实父子HWND透明/alpha、父坐标/DC裁剪/Graphics状态、移动失效传播完成。20项回归、同源屏幕及PRINTCLIENT分列观测、非同比例Stretch/Zoom有JIT/实际AOT证据。无HWNDdirect场景仅证不建窗/状态寿命；高对比复用既有系统读取，未作设备切换验收，剩余见父项。 |
| ✅ | P1 / Control-14a | 批次023明确首范围：ControlStyles默认/组合/未知位、GetStyle/SetStyle/UpdateStyles、DoubleBuffered/ResizeRedraw；Paint/Invalidated/StyleChanged及标准On*/InvokePaint/OnPrint、六个Invalidate重载、Update/Refresh/CreateGraphics，真实WM_PAINT/ERASEBKGND/PRINTCLIENT与缓冲/借用Graphics寿命。公共OnPrint借用Graphics并保留背景回调对其状态的修改，内部native绘制隔离背景状态；标准事件/重入/原异常和原生EDIT路径有指定对照。透明无句柄场景不计为可见合成验收，完整边界仍属14父项。 |
| ⬜ | P1 / Control-15 | 实现 DataBindings/BindingContext/DataContext 与变化传播、拖放/IDataObject/剪贴板、AccessibleName/Description/Role/AccessibilityObject；各领域引用对应独立任务，不能全部留为存值。 |
| ⬜ | P1 / Control-16 | DesignerSerializationVisibility/DefaultValue/DefaultEvent、ISupportInitialize、.resx Font/Image/Icon/TypeConverter 的消费和裁剪路径逐项验证；真实 VS 设计器与 Native AOT 运行均有证据。 |
| 🟡 | P1 / Control-17 | 002～048的USER32/生命周期保留；049的17e补原停车接收者寿命及Form控件命令按来源HWND反射。原反射父Form销毁/重建的完整关系、WM_NOTIFY/完整反射、创建参数自定义对象AOT封送、所有派生同步/状态恢复及资源/关闭竞态仍缺，不由有限EDIT回归关闭整个Windows后端。 |
| ✅ | P1 / Control-17a | 批次023明确首范围：标准Message/CreateParams、WndProc(ref Message)/DefWndProc、Message.Result回传、原窗口类过程保留、早期创建消息绑定和原生销毁最终清理；CreateParams样式/位置/文本/Parent/Param真实参与Control/Form创建和重建。字符串及显式静态基础值封送已有实现，运行仅覆盖null/string/nint及JIT自定义Payload；自定义Payload/object的AOT明确NotSupportedException。Message.GetLParam(Type)仅验证显式根化Payload，IL3050/任意marshaller、全部其他值与派生类/竞态验收另列。 |
| ✅ | P1 / Control-17b | 批次025：Control/Form 原生窗口过程先捕获 managed callback 异常，再执行 `WM_NCDESTROY` 清理；回调、映射、句柄、资源和 GCHandle 的独立失败分别进入 `ThreadException`，不让异常越过 unmanaged callback。同步上下文同样在 native 边界内处理。完整消息反射/自定义 Param AOT/关闭竞态仍归父项。 |
| ✅ | P1 / Control-17c | 批次046：有源类名时先GetClassInfoExW解析当前activation context中的原生类，缓存键加入源hInstance/lpfnWndProc；同源身份重复复用注册，不按STA额外注册。2项交替hosted/plain STA回归核源过程与_originalWindowProc、反向顺序及注册名复用，修复前2/2失败、修复后与新增/旧RTL共74/74通过。限定系统类缓存身份，不关闭完整activation/OLE/线程生命周期或17父项。 |
| ✅ | P1 / Control-17d | 批次048必要依赖：停车期间WM_COMMAND仅接收实际原生子HWND且映射已登记的Control，经HandleCommand反射；复用NativeWindowClass静态回调，不将停车窗注册为公开Control。其他消息默认转发，异常沿既有native边界报告。5项回归覆盖未挂父EDIT追加/修改/撤销、停车时通知、重挂原HWND/父映射且不重复通知及错误来源不路由；048原测试的提前释放停车窗假设由049纠正，接收者寿命与重挂后编辑通知见17e。WM_NOTIFY/完整反射、共享停车/ThreadContext及17父项不关闭。 |
| ✅ | P1 / Control-17e | 批次049：保留当前原生句柄创建时使用的自有停车通知接收者至该句柄销毁；WM_COMMAND接受真实停车子或仍有映射且原接收者归属匹配的重挂控件，外部销毁/重建均回收旧接收者。Form非零lParam按来源HWND反射，零lParam保留既有菜单/命令路径，不因相同ID误通知其他控件。8项回归含独立原生EDIT目的地、parking→Form编辑/Undo、活Form间重挂、释放/外部销毁/重建、错误来源及ID碰撞通过。048“重挂即销毁停车窗”假设按固定反射寿命纠正，旧TextChanged=2期望保持；完整原反射父Form寿命/WM_NOTIFY/context不关闭。 |
| ⬜ | P2 / Control-18 | Linux 用真实 GtkWidget 和 shared GMainContext；非交互宿主可用 GtkFixed，焦点/输入/可访问性用原生 controller。HWND/WndProc 等 Windows 消息契约单列，普通布局/Font/Image 不任意排除。 |
| ⬜ | P3 / Control-19 | macOS 用 NSView/NSControl 与 AppKit 主线程，target-action/delegate 映射标准事件；翻转坐标与 key-view loop 统一处理，native 指针不能冒称 HWND。 |
| ⬜ | 验收 / Control-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| HWND + 原生窗口过程/消息循环，是现有基础的增量修正；先用双目标探针证明线程、父级、布局、释放。 | GtkWidget/GtkFixed + GMainContext；控件是真 native widget，WinForms 的共享布局/事件规则由库内部对齐。 | NSView/NSControl + NSApplication 主循环；可移植布局与生命周期需保留，Win32 消息扩展作为明确平台契约。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- 2026-11-07 · 批次062：源码no-op，Rich偏移/保护标记/段落缩进沿既有interop，不新增公共API或Control完成范围；完整父项与平台门槛保持，见[062](../batches/062.md)。

- 2026-11-07 · 批次061：源码/任务no-op，Rich-003a/004a的私有blittable格式结构与标准属性不扩写Control完成范围，原父项/ID/分母保持，见[061](../batches/061.md)。

- 2026-11-07 · 批次060：源码no-op，Rich静态RTF流回调/缓存归002c/011a，现有生命周期连接保持；不由隐藏窗口格式往返关闭完整反射/线程/资源或平台父项，见[060](../batches/060.md)。

- 2026-11-07 · 批次059：既有父链Disposing查询仅放宽同库可见性，无算法/公共面扩写，64新增/5,728全量通过；不新增Control完成项，见[059](../batches/059.md)。

- 2026-11-07 · 批次058：源码与原父项no-op，复用现有系统类注册/消息/生命周期；真实Rich后端不关闭完整Control-17或其他Control任务，见[058](../batches/058.md)。

- 2026-11-07 · 批次057：源码no-op，必要TextLength原生读取/编辑元数据归TextBoxBase-002d/003h；不新增Control完成项或改变现有Paint调用语义，完整父任务保持，见[057](../batches/057.md)。

- 2026-11-07 · 批次056：生产no-op，专用文本框点击链保留通用鼠标异常finally边界，不将有限负向命中/元数据检查计为完整Control输入或Designer完成，见[056](../batches/056.md)。

- 2026-11-07 · 批次055：源码no-op，固定Control重建guard与DPI内部读取只为TextBox-004b提供依赖；不把新增两个系统度量helper计作完整DPI或Control完成，见[055](../batches/055.md)。

- 2026-11-07 · 批次054：补标准DeviceDpi/独立内部缓存和真实窗口创建读取，为TextBoxBase-007c服务；保持完整Control/DPI父任务部分，不以初值/窗口读取代替缩放及跨屏事件验收，见[054](../batches/054.md)。

- 2026-11-07 · 批次051：14c仅补TextBox词删除必要内部redraw依赖，默认Invalidate不强制子树；10例新增及5,119全量通过。Handle销毁后End必要重建和原异常按固定10.0.11保持，完整绘制/生命周期/设备及平台仍待，见[051](../batches/051.md)。

- 2026-11-07 · 批次049：新增17e，原因由固定Control.ReflectParent/UpdateReflectParent与独立原生EDIT对照闭环，不再把实际GetParent等同通知接收者。首跑13例11通过，两项重挂通知失败保留；修复后新18与旧停车/行为8组合26/26、完整4,954/4,954。048最终停车测试“重挂即释放”的断言是历史实现假设，现改为原EDIT销毁时释放；048原失败/诊断及17d限定历史保留，旧2次事件硬期望未改。完整17父项仍部分，见[049](../batches/049.md)。

- 2026-11-07 · 批次048：全量首轮4,930/4,931暴露旧ControlBehaviorTests的TextChanged=2变1；保留旧硬断言与失败回执，根因是裸STATIC停车窗吞WM_COMMAND。只补17d通知连接，不在AppendText手工补发事件、不改固定基类算法，也不新建完整context；标准ParkingWindow固定源码依据及最终证据见[048](../batches/048.md)。

- 2026-11-07 · 批次048未完成范围：新重挂探针在Form.Show后实际父映射/同HWND/停车窗销毁及native ID=1001均通过，但后续EDIT修改文本不触发Form/child命令和TextChanged。两份有界诊断JSON/TRX保留048；原因未闭环，不以本批停车期间通知测试称重挂后兼容，继续Control-17/TextBoxBase-006，下一批优先核此依赖。

- 2026-11-07 · 批次046：新宿主测试暴露旧superclass缓存跨activation context混用EDIT的潜在缺陷。首轮全量6个旧RTL失败，隔离28项通过、与新测试组合再次6失败；直接回归读到源过程与缓存原过程不一致，修复前两例全失败。仅在NativeWindowClass当前源类解析与缓存键加入真实源身份，保留旧RTL硬断言；Control.cs不改，完整17及平台/线程/模态仍待，见[046](../batches/046.md)。

- 2026-11-07 · 批次045：为TextBoxBase-005a增加标准内部WindowText扩展点，将现有Text setter的native提交与公开OnTextChanged分开；仅EDIT在native提交期间抑制反射通知，处理器内native编辑仍正常通知。Control公开Text判等/提交/通知规则保持；相关ControlText/编辑等892项与全量4,782项通过。首次两例夹具父Form未建窗被停车窗承载，补真实父HWND和托管映射断言后通过；不改变生产Parent/创建/反射管线，不重复新增Control完成任务，完整11/17与生命周期仍待，见[045](../batches/045.md)。

- 2026-11-07 · 批次044：Control.cs源码no-op。TextBox对齐夹具定位既有RTL创建后UpdateStyles会重新提交USER32规范化过的样式；独立EDIT复现此管线，仅验证TextAlign创建样式消费。原38/42失败保留，完整运行时RTL/镜像视觉及11/17父项不关闭，见[044](../batches/044.md)。

- 2026-11-07 · 批次042：生产Control及Control.Ime均no-op；核固定源码确认组合/SETCONTEXT/候选通知交原生默认过程，新增11项消息参数/结果、去重状态/默认异常及真实隐藏EDIT回传回归。沿12父项记录审计，不新增重复实现子项；物理IME/候选UI、模态与平台仍待，见[042](../batches/042.md)。

- 2026-11-07 · 批次040：12m将039语言变化消息接入标准Form通知，并补请求取消与原始参数/默认结果保留；上下文更新、unsupported传播和默认过程先后遵循固定源码。只关闭明确共享消息子范围，模态/OLE/组合串/设备与平台仍待，见[040](../batches/040.md)。

- 2026-11-07 · 批次039：12l接语言消息和非Form宿主传播，普通Form不冒充模态宿主。9项新增、50项组合及4,590全量通过；原始夹具失败保留，旧028无句柄焦点用例隔离并还原未初始化传播缓存，原默认/LostFocus/不建句柄断言保留。完整IME与设备仍待，见[039](../batches/039.md)。

- 2026-11-07 · 批次038：12k按固定10.0.11补有效ImeMode与CJK IMM上下文首范围；保留037字符计数语义。新interop用LibraryImport，HIMC借用与自有寿命分开；完整输入法/派生/平台仍未关闭，见[038](../batches/038.md)。

- 2026-11-07 · 批次037：按固定10.0.11补12j，7项IME消息回归及4,536全量通过。纠正12父项遗漏036已完成子范围的滞后；完整IME/context/候选窗/派生与平台不关闭，详见[037](../batches/037.md)。

- 2026-11-07 · 批次035：12h接入真实消息循环，默认按钮消费复用内部验证结果；AutoSizeMode修正数值身份时将基础字段显式初始化GrowOnly，保留原命名默认。旧整数0/1及已编译常量须复核，不以新增队列用例关闭完整键盘/Control，见[035](../batches/035.md)。

- 2026-11-07 · 批次036：12i按固定WinForms 10.0.11源码补原生WM_GETDLGCODE判别和WM_SYSCHAR分流；SuppressKeyPress在真实队列中有界移除字符消息，直接消息测试仍保持兼容；验证取消状态按父链读取，并在鼠标抬起后清理。新增5项输入测试、真实HWND按钮验证2项及既有键盘14项通过；完整IME、派生路由、设备和平台仍未验收，见[036](../batches/036.md)。

- 2026-11-07 · 批次034：完成12g键事件实际消息管线首范围，修复033中WndProc绕过预览、Handled返回误判及默认助记键回父递归；保留033已完成预处理范围，完整IME/队列字符抑制/设备及命令对话框仍待，见[034](../batches/034.md)。

- 2026-10-26 · 批次030：补Control-12e基础键消息/事件契约和5项Windows STA回归；4,397项全线测试通过，完整键盘预处理、IME、物理设备、Designer/AOT和跨平台仍未关闭，见[030](../batches/030.md)。

- 2026-10-17 · 批次029：新增49项回归与有限12c/12d实现；最终16场景仅5场景JIT/AOT严格一致，11场景相差关闭LostFocus的33行；对参考保留关闭LostFocus和既有WM_QUIT差异。父子集合初次回归发现可见兄弟句柄未创建，恢复既有创建路径后重新检查；原始失败保留。修正Control-02遗漏028基础Select/CanSelect的历史表述；整Control/98%不关闭，见[029](../batches/029.md)。

- 2026-10-16 · 批次028：补12a/12b及必要容器合作，54项新增核心/消费回归与全部4,343测试通过。固定参考30场景18同，JIT/AOT全部30同；11个额外WM_QUIT和1个SplitContainer继承投影差异原样保留。修复Form句柄恢复误激活，旧生命周期204行恢复严格全同。基础导航不等于自动转移、验证/键盘或物理设备验收；父项/完整Control不关闭，见[028](../batches/028.md)。

- 2026-10-16 · 批次027：Control-13由全待办改部分，只关闭13a/13b明确代码范围；核心21例、消费25例通过。固定参考25场景20同/5差异，JIT/AOT24同/1异常文本差异；LockScreenBackstopFrame阻止10/20次命中，成功Click命中与物理输入待解锁后验证。Control-12已有Enter/Leave/On*和局部焦点接入，纠正全待办但不关闭完整焦点/键盘。生命周期204、消息绘制558行参考/JIT回归同；旧AOT边界保留，见[027](../batches/027.md)。

- 2026-10-16 · 批次026：05c/08c有限范围实现，48项新回归与全部4,243测试通过。25标准场景16同/9增强差异，另7旧Task仅Lumina；32场景JIT/实际AOT全同。旧Action59行/ValueTask90行各两条关闭差异首次出现并保留strict失败，其余观测不变；历史004/005原全同证据不删除。修正当前“所有InvokeAsync关闭均悬挂”“原生错后托管清理必需第二次Dispose”的过时描述，明确Parent/child/Site再失败仍需重试，未关闭完整父项/平台或98%。见[026](../batches/026.md)。

- 2026-10-16 · 批次025：完成普通Drain的完成信号失败收尾，以及Control/Form/同步上下文的native callback与`WM_NCDESTROY`双错边界。固定参考/JIT/AOT各8场景全部正常退出；4项真实差异保留，未将增强行为归一化。完整Control、InvokeAsync关闭、Dispose后续托管事务、输入/Designer/资源、Linux/macOS仍未关闭，见[025](../batches/025.md)。

- 2026-10-16 · 批次024：完成04b/14b，关闭失败仍遍历余队列并销毁实际HWND，明确偏离10.0.11的收尾行为。补基础背景图像和真实屏幕透明/alpha证据，Form取消默认COMPOSITED后修复唯一意外失效矩形差异。更新“背景/透明全缺”及“Close必被首个wait打断”的当前描述；普通Drain、Dispose后续清理和native回调/清理双错仍有缺口，父项/历史失败/固定分母不变，见[024](../batches/024.md)。

- 2026-10-16 · 批次023：14a/17a补标准消息、样式和基础绘制，内部HandleWindowMessage收进标准虚入口，系统原生控件显式保留原生绘制。修复UpdateStyles后的客户区缓存、重建文本和公共OnPrint状态；Form默认绘制接共享入口，最外层销毁清理不依赖派生调用base。022基础Control字体差异由实际UserPaint类解决，保留默认palette差异与历史失败。AOT自定义Param/GetLParam动态封送、透明可见合成、完整绘制/输入/Designer和平台仍缺；仅明确子范围代码完成，详见[023](../batches/023.md)。

- 2026-10-16 · 批次022：完成11b基础外观父链与08b借用Font/独占HFONT明确范围，移除TextBox隐藏Font所有权；父重挂/Reset/通知/FontHeight/布局缓存与内部theme局部值分离。固定参考仍记录基础Control原生字体和默认palette前景色差异，完整Styles/UserPaint由14接续；不以托管一致关闭原生绘制。历史Control-14从全待办纠正为已有Invalidate/内部窗口过程的部分状态，全部未完成员与原ID保留。详细运行、首次失败和剩余边界见[022](../batches/022.md)。

- 2026-10-16 · 批次021：09d/10c完成有限基础几何/引擎/缓存。固定10.0.11默认AnchorLayoutV2=false，另进程显式true覆盖；不把V2误当默认。Dock↔Anchor内部切换避免多余DockChanged，真实几何提交保留标准事件；移除旧Form菜单高度二次偏移，native矩形与托管一致。13核心+11容器新测试及全部4,107项通过，512行JIT/实际AOT同参考；020七行差异已修，旧失败不覆盖。Control-17从全待办纠正为已有Win32部分实现，未把它或09/10父项关闭；完整边界见021。

- 2026-10-16 · 批次020：新增标准Layout参数/委托和非virtual公共布局入口，属性/集合/尺寸变更进入同一事务；OnLayout为标准派生扩展。历史Lumina覆盖virtual SuspendLayout/ResumeLayout/PerformLayout的消费者须按标准改为OnLayout(LayoutEventArgs)，这是对WinForms表面的兼容修正。byte暂停计数、缓存消费/重入/原异常有对照，不制造Dock缺失的Bounds事件。10父项及09/LayoutEngine/PreferredSize/InitLayout完整语义继续部分；56项新增回归与全部4,083项通过，394行JIT/AOT严格相同，参考7行几何差异保留，不宣称完整Control或98%。

- 2026-10-16 · 批次019：补07a标准句柄入口、事件、状态与映射。历史内部读Handle的调用改用NativeHandle，避免普通属性/销毁路径意外建窗；保留旧无参OnHandleCreated扩展。WM_SHOWWINDOW接入标准可见树与通知，018的391行父子集合参考/JIT差异已清零，历史失败不改写。重建期间保留dispatcher、子窗停放和重挂，普通销毁仍终止待执行项；Dispose覆盖异常/跳过base按固定参考保留对象/窗口状态。07父项保留全部派生恢复、资源与竞态；Form历史Created事件隐藏标准属性仍明确披露。全Control、Windows完整验收和GTK4/AppKit未关闭，详见[019](../batches/019.md)。

- 2026-10-16 · 批次018：新增标准嵌套集合和Parent setter/含parent构造，共用所有控件父子树；Form进入标准继承后复用同一状态。Windows通过SetParent/停放窗口保留重挂HWND、恢复Z序；级联Dispose先销毁原生树再解除Parent，释放后重挂再次Dispose仍清理当前树。数组复制保留接口存储的逐元素转换与标准异常；对Find及释放通知失败补最小固定参考。同步集合布局暴露的ToolStrip重入由库内修复。历史“Parent/Controls/Form完全未实现”已改为明确首范围，Control-01/03/06/08/09/11及FND父项仍有剩余，不新增整控件或98%声明。最终记录与已知差异见[018](../batches/018.md)。

- 2026-10-16 · 批次017：补ResetText标准虚槽、Text/TextChanged英文属性及OnTextChanged的Advanced，移除Text setter的释放后拒绝；维持虚getter判等、存值后通知和016构造顺序。固定参考/JIT 3,774条相同，实际AOT行为及其余元数据相同；内置COM关闭时CoreLib的RemoveAttributeInstances主动删除DispId=-517，保留原始差异，不改库AOT策略或全局根化。首轮一项新测试把setter退出回调中的嵌套Reset尾记录预期写错，已按参考改为完整16步断言并通过；219项新测试全部通过。父任务11保持部分完成；已有HWND/native、Parent/Form、布局、中文Designer和动态属性转换未关闭，见[017](../batches/017.md)。

- 2026-10-16 · 批次016历史回标：任务11补构造必需的Text子范围为部分完成并引用01c，重置/完整运行期与native/继承仍缺；02b/c、09a、10a的后续基础构造/MinMax/客户区/通知/换算改为交叉引用已完成子任务，保留真实布局/派生/外部通知缺口。原批次快照和ID不变，不新增验收。

- 2026-10-16 · 批次 016：新增 Control(string? text) 与五参文本/几何构造，默认初始化和 context 安装后经虚 Text，再分别设置 Location、Size；约束可在 Location 阶段改变尺寸并发通知。Text 允许 null 且为标准 virtual；虚 getter 判等时保留原存储，不被空写覆盖。4,584 条固定参考/JIT/实际 AOT 指定观测相同，311 项新增回归，覆盖 null/Unicode/NUL、极值几何、隐藏/覆盖/不调 base/重入/原异常和 context 时序。初次有效空值差异已按参考修正；构造 NullableContext 放置按有效契约比较，CoreCLR 的 Standard|HasThis 与 Native AOT 的 HasThis 原始编码单列 stderr，只比较实例/变参等实际契约。精确 #ctor/Text 根化仅在探针，AOT EXE 2,075,648 字节、无 CLR Header/IL2xxx/IL3xxx。含 parent 构造、完整 Text/native/重置/继承、布局与 Form 仍缺，见 [016](../batches/016.md)。

- 2026-10-16 · 批次 015：基础无参构造由固定 120×24 改为标准初始 0×0，新增 protected virtual DefaultSize。构造按参考重复读取非空默认 Min/Max 并调用公共虚 setter，再直接提交 DefaultSize 的宽高；双轴非零才计算边框客户区，否则保留构造重入留下的缓存，位置不被默认尺寸覆盖。默认扩展原异常发生时 context 尚未安装，成功后才安装、派生构造体可见。2,677 条固定 10.0.11/JIT/实际 AOT 全同，387 项新增回归；25 个旧差分驱动和全线 2,713 项测试通过，无旧测试或派生构造修改。AOT EXE 2,020,864 字节，无 CLR Header/IL2xxx/IL3xxx。已有派生构造显式尺寸保留；尚未自设默认尺寸的具体控件现在继承基础 0×0，其专属默认值仍待后续。其余公共构造、Parent/Controls、完整布局/DPI/Form 与全语言元数据另列，见 [015](../batches/015.md)。

- `70a08e7`（2026-04-09）：补 ResumeLayout/IsDisposed 等设计器兼容表面。
- `2fb513a`（2026-04-09）：加入异步调用相关实现。
- `9d5165f`（2026-04-09）：扩展基础属性与控件功能。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。
- 2026-10-15 · 批次 002：Control 接入 Component/IWin32Window 并可实例化；新增标准 Disposing，资源清理幂等且重复 Dispose 继续触发 Disposed；终结器分支不遍历托管子树。新增 9 项纯托管测试及 1 项基础 HWND 测试通过，`Test-ControlComponent.ps1` 的 5 项观测与真实 WinForms 10.0.11 一致。Form 继承链、UI 子控件级联、构造族、UI 投递及复杂设备/设计器验收保留。
- 2026-10-16 · 批次 003：接入真实窗口队列、ISynchronizeInvoke 和 Action/Delegate/args/Func 的同步调用、BeginInvoke/EndInvoke。对照无句柄、UI/工作线程、父级回退、FIFO、ExecutionContext、特殊委托参数及释放唤醒；69 条三端观测相同。已有 HWND 的 null Delegate 在参考端会挂起，本实现以 ArgumentNullException 明确拒绝；无 HWND 的 null 仍按参考端抛 InvalidOperationException。异步异常报 ThreadException；无订阅者仅显示原生 OK 提示继续，完整标准异常对话框/模式和广泛关闭竞态待后续。
- 2026-10-16 · 批次 004：Action/Func<T> 的标准 InvokeAsync 与取消及既有两条直接调用路径统一复用真实队列；59 条固定参考/JIT/AOT 观测相同，旧 Task 路径另检初始 UI 线程、等待内层 Task、原异常身份与取消。按参考端保留 null 校验先于句柄、无自身句柄返回 Faulted Task、预取消成功并跳过回调（Func 返回 default）、排队后/回调内取消及原 token；任务异常不报告 ThreadException。Dispose 前已排队而未执行的 Task 等待后续 token 取消，与参考端相同；无 token 的关闭/重建/退出竞态、ValueTask 与标准同步上下文仍待后续。

- 2026-10-16 · 批次 005：Control/Form 补标准 ValueTask 两族并共用真实 HWND 队列；IValueTaskSource 同步/延迟/取消后单次消费，原异常（含 wrapper/OCE 身份）与结果/token 保留。固定 WinForms 10.0.11、JIT、实际 Native AOT 的 90 条观测相同；旧 Task 虚方法保留，用 OverloadResolutionPriority(-1) 使未强转 async token lambda 选择标准 ValueTask，显式 Task 委托和派生 override 专项通过。预取消/无自身句柄/释放后边界按独立参考观测保留；同步上下文/await 后 UI 恢复及关闭竞态未关闭。

- 2026-10-16 · 批次 007：Control 构造接入默认 context 安装，保留已有 custom Current 与禁用策略；dispatcher 将目的 UI Current 用于回调的 ExecutionContext 并 finally 还原。默认直接/BeginInvoke/ValueTask/带结果/工作线程投递 await UI 恢复有固定三端对照，旧 Task 专项保留；完整构造、继承、Controls/Parent 与竞态不关闭，见 [批次 007](../batches/007.md)。

- 2026-10-16 · 批次 008：先固定 WinForms 10.0.11 参考，补标准 Focus 返回及三个查询属性和 Advanced/Browsable/Hidden 元数据。真实 HWND 程序切换、重复请求、外部原生切换/清空、原生 descendant、隐藏/禁用及释放后均按原生状态返回；不因查询或无自身句柄 Focus 创建窗口，也不回退 owner HWND。157 条参考/JIT/实际 AOT 相同，新增 17 项测试；未修改同步上下文/投递与现有 setter。禁用原生父窗口时 CanFocus 为 true 而实际 Focus 返回 false 的差异也按固定参考保留。Select、键盘/验证、Form 继承与广泛重建竞态仍待后续，见 [批次 008](../batches/008.md)。

- 2026-10-16 · 批次 009：先固定参考再改尺寸链。未创建自身 HWND 时 WinForms 保留负尺寸，不能统一钳制到零；已有 HWND 时负数归零且零宽高真实应用到 MoveWindow。首次创建保留原始参数，负尺寸通过 GetWindowRect 回读；int.MinValue 宽度按 USER32 的 CW_USEDEFAULT 结果得到 0×0，HandleCreated 前完成缓存更新。已有 GetNativeHeight 的正逻辑高度扩展不写回为下拉区高度。756 条固定参考/JIT/实际 AOT 相同，新增 33 项独立 STA/原生矩形回归，AOT EXE 2,562,048 字节、无 CLR Header 和 IL2xxx/IL3xxx。构造默认、全部派生限制、Form.ScaleSize、完整布局/事件与 Min/Max 未关闭，见 [批次 009](../batches/009.md)。

- 2026-10-16 · 批次 010：先固定 WinForms 10.0.11，再实现 BoundsSpecified 的八个标准值/Flags、x/y 命名的两族 SetBounds 和 protected virtual SetBoundsCore。外层按已知位保留其他分量，未知位原样交给 core，有效矩形未变不调用；core 本身接受完整矩形，包括派生调整的未指定分量和 specified=None，重复直接调用仍可被覆盖观察。七个位置/尺寸属性按对应标志进入同一路径，派生可拒绝、调整或抛异常。2,034 条固定参考/JIT/实际 AOT 完全相同，60 项新增回归通过；AOT EXE 2,576,896 字节，无 IL2xxx/IL3xxx。保留旧零/负尺寸、首次创建回读、GetNativeHeight 与 OnBoundsChanged，完整布局/Min/Max/事件和派生约束继续另列，见 [批次 010](../batches/010.md)。

- 2026-10-16 · 批次 011：先运行固定 WinForms 10.0.11 参考，再接入 LocationChanged/Move、SizeChanged/Resize 与四个标准 protected virtual On*。整矩形先提交，两族变化先快照，保持 Move→LocationChanged→Resize→SizeChanged 的顺序及重入后外层待发通知。OnLocationChanged 向 OnMove 透传参数，OnSizeChanged 仅向 OnResize 传 EventArgs.Empty，SizeChanged 仍用原参数；覆盖不调 base 按标准抑制对应通知链。无自身 HWND setter 和直接 On* 传播原异常；有 HWND setter 转交既有 Application.ThreadException，默认异常 UI/模式仍按既有缺口披露。旧 ApplyBounds→OnBoundsChanged 布局衔接保留，标准通知独立于该旧 hook。2,280 条固定参考/JIT/实际 AOT 相同，99 项新增测试；通知 AOT EXE 2,618,880 字节，无 CLR Header 或 IL2xxx/IL3xxx。客户区、外部原生自主通知、完整布局、构造默认/全部派生约束与 Form 生命周期未关闭，见 [批次 011](../batches/011.md)。

- 2026-10-16 · 批次 012：先固定参考，再补基础客户区与标准元数据。四有限边框/无有 HWND/11 入口/重复/零负恢复/覆盖重入异常及缓存异常态后仅位置更新共 5,556 条固定 WinForms 10.0.11/JIT/实际 AOT 全同，80 项新增回归通过。外框先提交，随后同步客户区缓存，位置/尺寸通知与客户区通知按参考顺序触发；缓存变化也算尺寸变化。SetClientSizeCore 正常执行至末尾时提交原始客户区请求并调用 OnClientSizeChanged（不检查是否同值，事件异常同步传播），故同值仍通知，raw 负值/重入可使缓存不同于原生客户区；不能简单实时 native getter 替换。GetClientRect/AdjustWindowRectEx 失败真实抛错，旧 OnBoundsChanged/GetNativeHeight 保留。客户区 AOT EXE 2,592,256 字节，无 CLR Header 或 IL2xxx/IL3xxx；探针的五个反射成员精确保留不改变库裁剪策略。SizeFromClientSize、构造默认、全部派生约束/外部自主通知/完整布局/Form 生命周期继续另列，见 [批次 012](../batches/012.md)。
- 2026-10-16 · 批次 013：先取得固定参考，再新增标准 SizeFromClientSize 虚方法，复用真实非客户区计算并保留 unchecked 溢出。四有限边框/无有 HWND/极值、直接/base 绕过/覆盖不调 base/返回调整、重入和原异常共 10,086 条固定参考/JIT/实际 AOT 全同，257 项新增独立 STA/元数据/原生矩形回归通过。基础 ClientSize/SetClientSizeCore 不调用该虚扩展，原路由保留；直接 base 换算不创建 HWND，不改变外框/客户区缓存或发通知，负值和重入形成的缓存差异也不被读取操作刷新。探针仅精确保留实际反射的两个方法；构造默认、尺寸约束、全部派生/外部自主通知/完整布局/DPI/Form 生命周期仍另列，见 [批次 013](../batches/013.md)。

- 2026-10-16 · 批次 014：明确初始化后的基础 Min/Max 运行期约束及默认虚扩展完成，33,408 条固定 10.0.11/JIT/实际 AOT 相同，新增 394 项回归；冲突保留属性值、负值/零轴、Empty 取消差异、getter/default/core 调用顺序与缓存/通知均按参考。core 原始相等先返回，约束后无 HWND 的重复矩形仍可能刷新客户区。AOT 仅精确保留六个 accessor，AmbientValue 构造元数据用 CustomAttributeData 核对，动态 Value/TypeDescriptor 转换初次实测失败仍单列；英文元数据相同不代表中文分类/描述本地化。构造期 DefaultMin/Max 在 DefaultSize 之前的状态已有独立参考，但实现统一留批次 015 的 01b。全部派生/完整布局/DPI/外部自主通知/Form 保持缺口，见 [014](../batches/014.md)。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.control?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/class.Widget.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nsview)
