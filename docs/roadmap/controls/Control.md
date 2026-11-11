# Control

本页任务表只列未完成范围；代码完成子项标记在[完成状态](#已完成任务)，任务 ID 与验收状态均不因整理改变。剩余重点是原反射父 Form 销毁/重建寿命、完整线程与模态、派生输入/布局、缩放/DPI、资源和可访问性。

[完整 API 与继承基线](../api/Control.md) · [任务索引](../README.md) · [依赖顺序](../execution.md) · [验证门槛](../validation.md) · [平台要求](../platforms.md)

固定基线为 .NET 10.0.11。Windows、Linux、macOS 分别验收，当前没有完整逐控件 98% 结论。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| 🟡 | P0 / Control-01 | 已完成Component/IWin32Window（01a）、ISynchronizeInvoke、基础DefaultSize/构造约束（01b）、无parent文本构造（01c）；018含parent构造和Controls归06a。IDropTarget、全部派生默认/扩展仍待实现，不随子项关闭。 |
| 🟡 | P0 / Control-02 | 008～010完成02a焦点查询、02b尺寸与02c边界虚入口；028的12a已完成基础Select/CanSelect，029补有限自动转移/验证。纠正此前仍将Select/CanSelect列缺失的滞后；完整键盘/派生布局仍待后续，不随子范围关闭。 |
| 🟡 | P0 / Control-03 | 批次003完成现有HWND/创建线程与父级句柄查找（03a）；018接入真实Parent/Form标准链，019内部查询改用不创建句柄的NativeHandle并保留重建投递。未启动/已退出消息循环和完整句柄重建竞态仍待对照。 |
| 🟡 | P0 / Control-04 | 003首范围04a、019重建保留队列、024关闭收尾04b及025普通Drain收尾04c完成；026的05c补已接受InvokeAsync任务关闭/投递失败终止。等待期间并发释放同一wait、完整关闭/重建/线程退出竞态及参数全部边界仍未关闭。 |
| 🟡 | P0 / Control-05 | 004～007标准异步入口、旧路径与context首范围保留；026的05c补已接受但未执行任务的Close/Post/Attach失败终止。完整关闭/重建/退出、多UI/模态竞态仍未关闭。 |
| 🟡 | P0 / Control-06 | 018完成06a标准父子集合与Windows重挂首范围；Form直接子项Parent为真实Form，共用ControlCollection。019修复隐藏父级重挂时中间VisibleChanged，原391行参考/JIT现全部相同；018原始失败记录保留。020已接基础布局事务；完整布局引擎/继承属性通知、派生重入全部组合与三平台仍缺。 |
| 🟡 | P1 / Control-07 | 019的07a、023的17a及025的07b保留，024的04b保证预先Close失败仍销毁实际HWND。native双错边界已覆盖有限范围；所有派生重建属性/焦点/资源恢复、全部关闭/线程竞态与GTK4/AppKit生命周期仍待原任务及17～19。 029记录正常关闭多余LostFocus，根因尚未定位，不能通过关闭期间抑制事件消差异。 |
| 🟡 | P1 / Control-08 | 002的08a、018/019父子与句柄清理、022的08b、024的14b借用图像保留；026的08c补实际HWND销毁后异常的托管收尾。其余Image/菜单所有权、完整派生清理/事件解绑/异常事务仍缺；普通Parent/child/Site异常及活HWND失败仍按原短路留待重试，不声明全部Dispose事务。 |
| 🟡 | P1 / Control-09 | 012～014客户区/换算/运行期MinMax与015构造默认保留；021完成09d基础默认Dock/Anchor/AutoSize、PreferredSize与指定边界恢复。完整派生测量/布局、DPI/RTL、复杂布局重入/集合修改和BringToFront/SendToBack仍缺；020的Dock/Split七行差异已由021修复，历史原始失败保留。 |
| 🟡 | P1 / Control-10 | 011/012通知和020标准布局事务保留；021完成10c标准LayoutEngine扩展、PreferredSize缓存及InitLayout/ResumeLayout(false)引擎协作。非Control布局对象、DPI锚定恢复、全部派生布局/外部原生自主通知、复杂多子项缓存重入与异常恢复仍待原任务，父项不关闭。 |
| 🟡 | P1 / Control-11 | 016/017 Text/ResetText、018 Enabled/Visible保留；022完成11b基础Font/BackColor/ForeColor局部值及父级继承；023实现基础ControlStyles及透明背景调用首范围；031补11c RightToLeft托管继承、Win32 RTL扩展样式和AutoScrollOffset基础契约。Application默认字体/Site/ActiveX ambient、Cursor、可见透明合成、全部事件与完整native文本同步仍缺；主题默认值、RTL镜像/DPI和WinForms视觉差异保留，不随子范围关闭。 |
| 🟡 | P1 / Control-12 | 028～039范围保留；040的12m补标准Form语言通知/取消，Application-11a提供真实HKL服务。全部派生/ToolStripControlHost、完整原生验证组合、Tab/助记键/命令、OLE/完整输入语言服务、模态宿主/组合串/候选窗和物理输入仍缺。 |
| 🟡 | P1 / Control-13 | 027完成13a坐标/捕获和13b基础鼠标消息/事件；必要消费者共享Click去重。验证取消与点击协作、全部派生反射/默认激活/AutoCheck/组同步、RTL/DPI/非客户区及完整捕获竞态仍待原任务。锁屏下10个参考、20个JIT/AOT命中观察失败，真实命中/物理设备交互未验收，不能由事件入口或消息注入关闭完整输入。 |
| 🟡 | P1 / Control-14 | 023的14a、024的14b完成基础背景图像/真实HWND透明绘制，051的14c补词删除必要内部redraw计数；RTL/mirror、滚动viewport、TabPage主题透明、全部派生owner draw/消息反射/缓冲分配失败与资源压力仍缺。不能由基础Control/消息回归关闭完整绘制，022/023历史原始差异保留。 |
| ⬜ | P1 / Control-15 | 实现 DataBindings/BindingContext/DataContext 与变化传播、拖放/IDataObject/剪贴板、AccessibleName/Description/Role/AccessibilityObject；各领域引用对应独立任务，不能全部留为存值。 |
| ⬜ | P1 / Control-16 | DesignerSerializationVisibility/DefaultValue/DefaultEvent、ISupportInitialize、.resx Font/Image/Icon/TypeConverter 的消费和裁剪路径逐项验证；真实 VS 设计器与 Native AOT 运行均有证据。 |
| 🟡 | P1 / Control-17 | 002～049的USER32/生命周期及17e、067的17f保持；069的17g仅补必要WM_NOTIFY结果回传及Form来源HWND识别。原反射父Form销毁/重建的完整关系、其他WM_NOTIFY结果语义/完整反射、创建参数自定义对象AOT封送、所有派生同步/状态恢复及资源/关闭竞态仍缺，不由限定回归关闭整个Windows后端。 |
| ⬜ | P2 / Control-18 | Linux 用真实 GtkWidget 和 shared GMainContext；非交互宿主可用 GtkFixed，焦点/输入/可访问性用原生 controller。HWND/WndProc 等 Windows 消息契约单列，普通布局/Font/Image 不任意排除。 |
| ⬜ | P3 / Control-19 | macOS 用 NSView/NSControl 与 AppKit 主线程，target-action/delegate 映射标准事件；翻转坐标与 key-view loop 统一处理，native 指针不能冒称 HWND。 |
| ⬜ | 验收 / Control-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| HWND + 原生窗口过程/消息循环，是现有基础的增量修正；先用双目标探针证明线程、父级、布局、释放。 | GtkWidget/GtkFixed + GMainContext；控件是真 native widget，WinForms 的共享布局/事件规则由库内部对齐。 | NSView/NSControl + NSApplication 主循环；可移植布局与生命周期需保留，Win32 消息扩展作为明确平台契约。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 已完成任务

✅ 表示该行定义范围代码完成；父项的剩余代码与平台验收仍按当前任务表记录。

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ✅ | P1 / Control-12m | 批次040：WM_INPUTLANGCHANGE在已有上下文更新/unsupported传播后向FindForm发标准Changed，再默认过程；WM_INPUTLANGCHANGEREQUEST构造标准Changing参数并可取消，取消结果0且不入默认过程。无Form根保留默认转发，不新建句柄；Form14项及IME/焦点组合96项通过。限定共享消息连接，实际物理切换/组合输入与模态/OLE仍待。 |
| ✅ | P1 / Control-12l | 批次039：WM_INPUTLANGCHANGE先更新已有IME上下文、unsupported语言传播Off，再沿默认过程；request维持原生默认转发。非Form根失焦时恢复宿主传播模式后重置为Inherit，WM_IME_NOTIFY忽略守卫finally恢复，保持IME收尾→默认过程→LostFocus顺序。9项回归通过；Form语言事件/取消、Application输入语言服务、模态Form及实际中日韩/组合输入仍待，不关闭完整语言切换/宿主传播。 |
| ✅ | P1 / Control-12k | 批次038：标准ImeMode枚举/属性继承/reset/事件、CanEnableIme/DefaultImeMode/ImeModeBase/PropagatingImeMode扩展；受支持CJK语言的焦点/属性/WM_IME_NOTIFY接真实IMM上下文和转换位，自有HIMC独立清理、系统借用context不销毁，通知/释放双错保留。12项回归通过；TextBox只读/密码限制及属性联动、完整语言切换/宿主模态传播、组合串/候选窗、全部派生与设备仍归父项。 |
| ✅ | P1 / Control-12j | 批次037：WM_IME_CHAR直接进入ProcessKeyEventArgs而不经父KeyPreview，KeyPress消费/字符修改及原WParam保留；计数避免后续WM_CHAR/WM_SYSCHAR重复事件且保留默认过程，START清零/END关闭计数。7项回归通过；Handled后的计数按固定源码保留一次。ImeMode/context/组合串/候选窗及物理设备仍待，不关闭完整IME。 |
| ✅ | P1 / Control-12h | 批次035：Application实际键盘队列在Translate/Dispatch前按FromChildHandle调用PreProcessMessage，消费后停止、未消费保留四字段修改；异常经ThreadException报告。补内部ValidateActiveControl供Button消费；9项实际消息队列回归含默认取消按钮集成通过。直接SendMessage仍走WndProc，原生输入键/字符判别、队列字符抑制、IME和物理输入仍待。 |
| ✅ | P1 / Control-12i | 批次036：按固定10.0.11实现IsInputKey的Alt/Tab/箭头/默认WM_GETDLGCODE判别、IsInputChar的字符/Tab判别及WM_SYSCHAR直达对话字符；队列KeyDown的SuppressKeyPress按消息类型有界移除WM_CHAR/WM_SYSCHAR/WM_IME_CHAR，直接WndProc保留现有单次兼容路径。补ValidationCancelled父链读取、鼠标抬起取消点击守卫和状态清理；5项输入判别、2项真实按钮验证所需共享回归及既有键盘队列通过。完整IME/队列边界、全部派生和物理输入仍归父项。 |
| ✅ | P1 / Control-12g | 批次034：六类普通/system键消息经ProcessKeyMessage→父预览→ProcessKeyEventArgs，事件返回Handled且默认过程只在未消费时执行，KeyChar修改回写WParam；基础ProcessMnemonic默认false终止后代回父递归。7项新增回归通过；完整IME/队列字符抑制/物理输入与全部命令路由仍归12父项。 |
| ✅ | P0 / Control-01a | 批次 002：真实无参构造、Component/IComponent/IDisposable/IWin32Window 消费与派生覆盖通过；基础 HWND 由原生 IsWindow/文本长度消费并在释放后失效。完整继承/构造/接口与延迟句柄语义另列待办，兼容验收后置。 |
| ✅ | P0 / Control-01b | 批次 015：基础无参 0×0 与 protected virtual DefaultSize 完成；构造期 DefaultMin/Max 在原始状态按参考重复读取并消费公共虚 setter，随后直接初始化 DefaultSize 和条件客户区，再安装同步上下文。2,677 条固定 10.0.11/JIT/实际 AOT 相同，387 项新增回归，覆盖四边框/正零负极值、冲突/重复读取、不调 base/重入/原异常及构造异常 Current 时序。其余构造、所有具体派生默认/DPI/Form/完整布局与兼容验收另列。 |
| ✅ | P0 / Control-01c | 批次 016：无 parent 的 text 与 text/left/top/width/height 两个标准公共构造完成，text 接受 null，Text 为标准 virtual/AllowNull；默认初始化/context 后消费虚文本，再分别进入 Location 和 Size，保留约束/通知/重入/原异常。4,584 条固定参考/JIT/实际 AOT 指定观测相同，311 项新增回归；构造空值契约与运行时调用约定编码单独保留原始证据，见批次记录。含 parent 构造、完整 Text/native/布局与 Form 继承另列。 |
| ✅ | P0 / Control-02a | 批次 008：标准 bool Focus() 与 virtual Focused、ContainsFocus/CanFocus 使用真实 GetFocus/IsChild/IsWindowVisible/IsWindowEnabled；无自身 HWND 不创建句柄，隐藏/禁用/释放与外部原生状态有对照。157 条固定参考/JIT/实际 AOT 相同，17 项新增 STA/元数据回归通过。完整选择、键盘/验证及重建竞态另列。 |
| ✅ | P0 / Control-02b | 批次 009：SetBounds 及 Width/Height/Size/Bounds 无自身 HWND 时保留零/负尺寸、不创建句柄；已有 HWND 时负数归零，实际零宽高可恢复。首次创建回读原生矩形，HandleCreated 已见归一结果。756 条固定参考/JIT/实际 AOT 相同，33 项新增回归通过。后续基础构造默认/MinMax/通知见已完成01b/c、09a/c、10a；全部派生约束、完整布局/DPI及外部自主通知仍缺。 |
| ✅ | P0 / Control-02c | 批次 010：标准 BoundsSpecified、四/五参数 SetBounds 的 x/y 名称与 protected virtual SetBoundsCore 实际接入；None/组合/未知位、重复值、属性标志、派生覆盖与直接 core 有 2,034 条固定参考/JIT/实际 AOT 对照，新增 60 项回归通过。外层保留未指定分量，core 接完整矩形，原始 specified 不丢；保留02b尺寸/创建语义。基础Min/Max、通知及客户区见已完成09a/c、10a；完整布局/全部派生约束/外部自主通知仍缺。 |
| ✅ | P0 / Control-03a | 批次 003：InvokeRequired 查询 HWND 创建线程，沿 Parent 及当前 Owner 找已有投递句柄；无句柄返回 false，Invoke/BeginInvoke 无句柄不创建并抛标准异常；父级/Owner 回退有实际线程对照。 |
| ✅ | P0 / Control-04b | 批次024：Close及Enqueue/AttachWindow失败投递先完成全部已接收Invocation，再传播单原错/多错聚合；Post原Win32错误保留第一项及各EndInvoke。DestroyHandle遇Close错误仍真实销毁HWND。10项独立回归及同源JIT/AOT指定关闭场景通过，相对10.0.11的16行收尾差异明确保留。仅队列/实际HWND保证，不承诺Dispose后续托管树/Site/事件事务，普通Drain/InvokeAsync包装及全部竞态归父项。 |
| ✅ | P0 / Control-04c | 批次025：普通 `Drain` 在 `Invocation.Complete.Set` 失败后继续执行 FIFO 余队列，Drain 结束后传播单/多完成信号异常；完成信号成功时仍按标准路径报告未观察 callback 异常，重复 `EndInvoke` 保留原异常。4项独立队列回归通过；InvokeAsync关闭终止、并发释放和完整竞态仍归父项。 |
| ✅ | P0 / Control-04a | 批次 003：真实 PostMessage 队列、同线程 FIFO/EndInvoke、跨线程返回与异常、ExecutionContext、IAsyncResult 和销毁待执行项唤醒；69 条指定观测在固定 WinForms 10.0.11、JIT、AOT 相同。null Delegate 保护与默认异常 UI 的明确差异见修改历史，不等于父任务完整验收。 |
| ✅ | P0 / Control-05c | 批次026：Invocation失败通知桥接标准Action/Func<T>/ValueTask/ValueTask<T>及历史Task，Close/失败Post/Attach先结算已接受未执行任务；取消先完成仍保留token，关闭先完成后取消不覆盖，已开始工作独立完成、ValueTask仅消费一次且不重复ThreadException。28项独立回归及Windows JIT/AOT证据；相对10.0.11的关闭增强、旧004/005各两行差异与严格失败保留，完整退出/竞争仍归父项。 |
| ✅ | P0 / Control-05a | 批次 004：标准 Action/Func<T> 在自身 HWND 线程排队，返回值、原异常及取消保留；旧单 Action/Task 回调也排队。固定 WinForms 10.0.11、JIT、AOT 59 条观测相同，另有旧 Task 路径专项检查；ValueTask/同步上下文与完整关闭竞态未关闭。 |
| ✅ | P0 / Control-05b | 批次 005：标准 Func<CancellationToken,ValueTask> / ValueTask<T> 在 UI 队列首次调用并单次消费，90 条固定参考/JIT/AOT 观测相同；原异常/结果/token、取消后继续消费及 async lambda 选择已检。同步上下文安装、callback 内 await 后 UI 恢复和完整关闭竞态另列，不代表完整异步兼容。 |
| ✅ | P0 / Control-06a | 批次018：ControlCollection/ArrangedElementCollection、Parent可设与含parent构造、Add/AddRange/Remove/Clear/Find/键和索引/复制/枚举/Clone/排序、ParentChanged与ControlAdded/Removed、Contains/FindForm/TopLevelControl完成；重复添加/循环父子/线程限制、直接Form父级、真实HWND移动/停放/重挂保留句柄和Z序有固定参考。精确范围与运行证据见018；完整布局/ambient通知和Handle延迟/重建另列。 |
| ✅ | P1 / Control-07a | 批次019：get-only延迟Handle、独立Created/IsHandleCreated/RecreatingHandle、CreateControl与protected CreateHandle/DestroyHandle/RecreateHandle、OnCreateControl/OnHandleCreated/OnHandleDestroyed及查找映射完成。Windows普通/隐藏/可见树、直接/外部销毁、父子重建、销毁事件中的映射寿命、重挂可见消息、已排队回调保留、创建重入/原异常/Dispose覆盖首范围有204行固定10.0.11/JIT/实际AOT严格对照；完整子范围见019，剩余派生/资源/竞态与平台见父项。 |
| ✅ | P1 / Control-07b | 批次025：外部 `WM_NCDESTROY` 中 callback 与映射移除、dispatcher/句柄清理异常逐项保留并分别报告；Control/Form 的 GCHandle、资源和窗口状态继续完成收尾。固定探针覆盖 Control/Form 双错场景，Lumina 当前销毁待执行 callback 的 `ObjectDisposedException` 差异保留。完整Dispose事务、重建恢复和关闭竞态仍归父项。 |
| ✅ | P1 / Control-08c | 批次026：ReleaseNativeResources抛错且NativeHandle==0时继续原Parent→OnDisposing→children→Component顺序，单错EDI保留、多个错误按发生顺序聚合；继续阶段遇Parent/child/Site错仍按原规则短路，不伪造Disposed。活HWND、无原生故障、重复/重入和Dispose(false)保留既有边界。20项回归含真实Control/Form关闭，固定参考5个native-after场景有明确增强差异；完整资源/事务/并发仍归父项。 |
| ✅ | P1 / Control-08b | 批次022：基础Control及必要消费控件各自持有官方Font.ToHfont生成的HFONT，向HWND设置替代句柄后再释放旧句柄，真实销毁及无窗Dispose释放；托管Font保留借用引用不克隆/Dispose。Form重建失败时停放子窗字体仍存活并可更新；TextBox移除隐藏Font与自有释放路径。完整DPI/UserPaint/派生资源/平台仍归父项。 |
| ✅ | P1 / Control-08a | 批次 002：继承 Component.Dispose 并覆盖 Dispose(bool)，保留派生 OnDisposing；Site 移除、组件容器级联、重复/重入、Dispose(false)、异常传播和状态退出均有回归。5 项生命周期观测与本机 WinForms 10.0.11 相同；不等同完整控件生命周期验收。 |
| ✅ | P1 / Control-09a | 批次 012：标准 ClientSize/ClientRectangle、ClientSizeChanged 与 protected virtual SetClientSizeCore/OnClientSizeChanged 完成；明确初始化基础 Control、四有限边框样式、无/有 HWND、零负/恢复、顺序/覆盖/重入/异常共 5,556 条固定参考/JIT/实际 AOT 相同，80 项新增回归通过。保留原始客户区请求缓存与重复通知；客户区缓存变化可使仅位置操作触发标准尺寸通知。旧边界/通知/原生高度扩展保留；基础构造默认与SizeFromClientSize见已完成01b/c、09b，全部派生约束/外部自主通知/完整布局/Form生命周期仍缺。 |
| ✅ | P1 / Control-09b | 批次 013：标准 protected virtual Size SizeFromClientSize(Size clientSize) 与四有限边框、无/有 HWND、正零负极值/unchecked 溢出、覆盖/重入/原异常完成；10,086 条固定参考/JIT/实际 AOT 相同，257 项新增回归通过。直接 base 换算不改状态/客户区缓存/通知/句柄，ClientSize 与 SetClientSizeCore 按参考不消费该虚扩展；原始客户区请求缓存和既有边界/通知保持。完整布局/全部派生约束/DPI 切换/Form 生命周期另列。 |
| ✅ | P1 / Control-09c | 批次 014 运行期首范围：基础 virtual MinimumSize/MaximumSize 与 protected virtual 默认扩展接入真实尺寸 core；四有限边框、无/有 HWND、负零/冲突/重复/取消、12 入口、覆盖/重入/原异常/通知与缓存共 33,408 条固定参考/JIT/实际 AOT 相同，新增 394 项回归通过。构造期默认约束与 DefaultSize 由 01b 一起处理；本批元数据仅固定英文范围，中文 Designer 分类/描述、AOT 动态 AmbientValueAttribute.Value 转换、完整布局/全部派生约束/DPI/Form 生命周期另列。 |
| ✅ | P1 / Control-09d | 批次021基础默认几何：反序Dock、legacy默认及显式AnchorLayoutV2、AutoSize的GrowOnly/GrowAndShrink、Dock↔Anchor切换/指定边界恢复、PreferredSize/virtual GetPreferredSize、MinMax与必要缓存失效完成；普通Control.DisplayRectangle为未扣Padding的ClientRectangle，容器自行消费Padding。18场景512行固定10.0.11/JIT/实际AOT严格相同，含真实HWND矩形。范围限基础Control及本批列出的必要容器；全部派生测量、DPI/RTL及复杂重入组合保留。 |
| ✅ | P1 / Control-10b | 批次020完成基础非virtual SuspendLayout/ResumeLayout/PerformLayout入口、protected OnLayout及标准参数/委托、嵌套延期/缓存/重入/原异常及必要属性/父子事务。020原394行JIT/AOT同、参考7行差异保留为历史；021同源394行参考/JIT全同。本子项只计事务状态与调用契约，引擎/缓存由10c补齐。 |
| ✅ | P1 / Control-10c | 批次021：标准Layout.LayoutEngine及Control.LayoutEngine扩展真实参与布局；OnLayout先事件再引擎，返回true请求父PreferredSize布局；InitLayout、ResumeLayout(false)、子项边界/属性/集合变更协调初始化及首选缓存。自定义引擎调用/返回/异常、默认引擎、缓存与指定边界有512行对照及13项核心回归。非Control arranged对象、复杂缓存共享重入和完整设计器往返另列，不以标准基类默认无几何策略代替实际DefaultLayout。 |
| ✅ | P1 / Control-10a | 批次 011：标准 LocationChanged/Move、SizeChanged/Resize 与四个 protected virtual On* 已接入七属性/两族 SetBounds/直接 core；重复、指定/未知位、零负尺寸、覆盖/不调 base、重入/原异常和参数轨迹共 2,280 条固定参考/JIT/实际 AOT 相同，99 项新增回归通过。OnSizeChanged 向 OnResize 传 Empty，SizeChanged 保留原参数；旧OnBoundsChanged独立保留。基础ClientSizeChanged/客户区见已完成09a，外部原生自主移动/尺寸通知、完整Layout及全部派生约束仍缺。 |
| ✅ | P1 / Control-11b | 批次022：virtual Font/BackColor/ForeColor、三默认值/Reset、internal virtual ShouldSerialize、FontHeight缓存、标准On*/OnParent*及FontChanged、局部相等/身份、父级重挂、通知顺序/重入/原异常/释放后托管行为和必要布局缓存协作完成。Font沿父内部存储继承，颜色消费父虚getter；主题不向公共属性回写。仅基础托管父链与本批必要消费范围，全部ambient/DPI/UserPaint/Designer另列；212行探针完整原始差分见022。 |
| ✅ | P1 / Control-11a | 批次017：标准virtual ResetText经虚Text setter，null归空、虚getter判等、重复赋值/身份、事件/覆盖/隐藏/不调base/重入/原异常及释放后托管读写完成；219项新增回归通过。3,774条固定10.0.11参考/JIT全同；实际AOT仅Text的DispId属性被工具链删除，其他3,773条原始记录全同，原差异和运行时策略单列，不能称AOT元数据全同。已有HWND/native、父级继承、完整布局/中文Designer及动态转换另列。 |
| ✅ | P1 / Control-11c | 批次031：标准 `RightToLeft` 枚举、属性、Reset/ShouldSerialize、事件槽和父级继承通知；有效值接入 `WS_EX_LAYOUTRTL`/`WS_EX_RTLREADING`，显式 No 使用 `WS_EX_NOINHERITLAYOUT`，句柄创建/重建保持一致；补齐标准 `AutoScrollOffset` 供滚动容器消费。完整 Cursor、RTL镜像/DPI、ambient/Designer及全部派生控件仍归11父项。 |
| ✅ | P1 / Control-12a | 批次028：CanSelect、GetContainerControl、GetNextControl、Select()/protected Select(bool,bool)、SelectNextControl的基础托管导航；按TabIndex/集合顺序遍历、容器边界、方向/TabStop/nested/wrap过滤及虚派发。普通Control/ContainerControl/Form指定场景有固定参考；ToolStripControlHost特殊反向遍历、标准派生容器继承、自动转移/验证另列于12父项。 |
| ✅ | P1 / Control-12b | 批次028：Enter/Leave改用组件事件槽，OnEnter/OnLeave对齐protected internal；补GotFocus/LostFocus、On*/Invoke*及WM_SETFOCUS/WM_KILLFOCUS的默认过程顺序，Focus成功后更新活动链。订退/CanRaiseEvents/原异常、无HWND逻辑选择与实际HWND程序焦点有证据；完整验证、设备及派生焦点处理不在该有限范围。 |
| ✅ | P1 / Control-12c | 批次029：SelectNextIfFocused从父容器使用标准SelectNextControl；Enabled提交状态后、Visible提交前触发导航，无候选不自造清空。TabStop变更同步WS_TABSTOP并经组件事件槽发TabStopChanged；指定当前/祖先、TabStop/Selectable及嵌套候选轨迹有固定参考/JIT/AOT证据。关闭阶段差异归07/Form-07。 |
| ✅ | P1 / Control-12d | 批次029：CausesValidation默认true及Changed、Validating/Validated和protected On*接入组件事件槽；验证取消跳过Validated，CausesValidation=false跳过验证，Validating原异常透传，批量Validated原异常透传、普通路径报ThreadException。只完成标准基础消费，完整输入提交/调试异常策略/派生覆盖仍归12及FND-021。 |
| ✅ | P1 / Control-12e | 批次030：新增KeyEventArgs/KeyPressEventArgs及标准委托，Control事件槽与On*接入WM_KEYDOWN/WM_SYSKEYDOWN/WM_CHAR/WM_KEYUP/WM_SYSKEYUP，保留修饰键、Handled和SuppressKeyPress的基础托管语义。仅完成消息注入首范围；PreviewKeyDown、命令/对话框预处理、KeyPreview、完整Tab/助记键/IME、真实设备和AOT仍归父项与FND-019。 |
| ✅ | P1 / Control-12f | 批次033：增加PreviewKeyDown/PreviewKeyDownEventArgs、IsInputKey/IsInputChar、ProcessCmdKey/ProcessDialogKey/ProcessDialogChar/ProcessKeyPreview/ProcessKeyMessage/ProcessKeyEventArgs/ProcessMnemonic及public PreProcessMessage首范围；键消息先命令路由再预览和输入/对话框分派，字符消息按输入字符或对话字符分派。完整KeyPreview、IME、默认/取消按钮、完整Tab/助记键、派生路由和真实设备仍归12/FND-019。 |
| ✅ | P1 / Control-13a | 批次027：PointToClient/PointToScreen、RectangleToClient/RectangleToScreen按真实MapWindowPoints转换（矩形两点）；Capture查询/获取/转移/释放与MouseCaptureChanged，静态MouseButtons/MousePosition接原生状态。无/有句柄、负坐标、父子、释放与重建指定范围完成；RTL镜像/DPI/Wayland等完整语义与物理设备验收仍归父项。 |
| ✅ | P1 / Control-13b | 批次027：标准Click/DoubleClick及鼠标事件共用Component.Events、对应虚On*、InvokeOnClick/RaiseMouseEvent/ResetMouseEventArgs；MouseEventArgs.Location/可继承、HandledMouseEventArgs与XButton1/2；五按钮/双击/有符号坐标/滚轮Handled及原生Enter/Hover/Leave跟踪。跟踪在虚WndProc之外，保留直接调用区别、重建pending-enter、有限销毁和Up后的捕获清理。范围限基础Control消息路径及上述契约；验证取消、所有派生路由与设备验收不计完成，原异常文本差异保留。 |
| ✅ | P1 / Control-14c | 批次051：内部BeginUpdateInternal及EndUpdateInternal两重载，private short计数；无窗Begin不计数/建窗，首Begin发WM_SETREDRAW(0)，嵌套End返回true且末次先减到0再经Handle发1，可选Invalidate自身，无配对End返回false。10例核消息参数/有限回调计数顺序、默认与false失效行为/原异常、销毁后Handle重建及创建失败后的零计数。测试截获redraw消息，真实可见绘制未验；不新增公开更新模型或关闭完整14/17。 |
| ✅ | P1 / Control-14b | 批次024：virtual BackgroundImage/Layout、ImageLayout五值、两事件/三个On*、通知传播及借用图像寿命；Tile/None/Center/Stretch/Zoom、基础真实父子HWND透明/alpha、父坐标/DC裁剪/Graphics状态、移动失效传播完成。20项回归、同源屏幕及PRINTCLIENT分列观测、非同比例Stretch/Zoom有JIT/实际AOT证据。无HWNDdirect场景仅证不建窗/状态寿命；高对比复用既有系统读取，未作设备切换验收，剩余见父项。 |
| ✅ | P1 / Control-14a | 批次023明确首范围：ControlStyles默认/组合/未知位、GetStyle/SetStyle/UpdateStyles、DoubleBuffered/ResizeRedraw；Paint/Invalidated/StyleChanged及标准On*/InvokePaint/OnPrint、六个Invalidate重载、Update/Refresh/CreateGraphics，真实WM_PAINT/ERASEBKGND/PRINTCLIENT与缓冲/借用Graphics寿命。公共OnPrint借用Graphics并保留背景回调对其状态的修改，内部native绘制隔离背景状态；标准事件/重入/原异常和原生EDIT路径有指定对照。透明无句柄场景不计为可见合成验收，完整边界仍属14父项。 |
| ✅ | P1 / Control-17a | 批次023明确首范围：标准Message/CreateParams、WndProc(ref Message)/DefWndProc、Message.Result回传、原窗口类过程保留、早期创建消息绑定和原生销毁最终清理；CreateParams样式/位置/文本/Parent/Param真实参与Control/Form创建和重建。字符串及显式静态基础值封送已有实现，运行仅覆盖null/string/nint及JIT自定义Payload；自定义Payload/object的AOT明确NotSupportedException。Message.GetLParam(Type)仅验证显式根化Payload，IL3050/任意marshaller、全部其他值与派生类/竞态验收另列。 |
| ✅ | P1 / Control-17b | 批次025：Control/Form 原生窗口过程先捕获 managed callback 异常，再执行 `WM_NCDESTROY` 清理；回调、映射、句柄、资源和 GCHandle 的独立失败分别进入 `ThreadException`，不让异常越过 unmanaged callback。同步上下文同样在 native 边界内处理。完整消息反射/自定义 Param AOT/关闭竞态仍归父项。 |
| ✅ | P1 / Control-17c | 批次046：有源类名时先GetClassInfoExW解析当前activation context中的原生类，缓存键加入源hInstance/lpfnWndProc；同源身份重复复用注册，不按STA额外注册。2项交替hosted/plain STA回归核源过程与_originalWindowProc、反向顺序及注册名复用，修复前2/2失败、修复后与新增/旧RTL共74/74通过。限定系统类缓存身份，不关闭完整activation/OLE/线程生命周期或17父项。 |
| ✅ | P1 / Control-17d | 批次048必要依赖：停车期间WM_COMMAND仅接收实际原生子HWND且映射已登记的Control，经HandleCommand反射；复用NativeWindowClass静态回调，不将停车窗注册为公开Control。其他消息默认转发，异常沿既有native边界报告。5项回归覆盖未挂父EDIT追加/修改/撤销、停车时通知、重挂原HWND/父映射且不重复通知及错误来源不路由；048原测试的提前释放停车窗假设由049纠正，接收者寿命与重挂后编辑通知见17e。WM_NOTIFY/完整反射、共享停车/ThreadContext及17父项不关闭。 |
| ✅ | P1 / Control-17e | 批次049：保留当前原生句柄创建时使用的自有停车通知接收者至该句柄销毁；WM_COMMAND接受真实停车子或仍有映射且原接收者归属匹配的重挂控件，外部销毁/重建均回收旧接收者。Form非零lParam按来源HWND反射，零lParam保留既有菜单/命令路径，不因相同ID误通知其他控件。8项回归含独立原生EDIT目的地、parking→Form编辑/Undo、活Form间重挂、释放/外部销毁/重建、错误来源及ID碰撞通过。048“重挂即销毁停车窗”假设按固定反射寿命纠正，旧TextChanged=2期望保持；完整原反射父Form寿命/WM_NOTIFY/context不关闭。 |
| ✅ | P1 / Control-17f | 批次067必要依赖：自有停车WM_NOTIFY以NMHDR.hwndFrom查映射，只接当前真实子窗或匹配且仍为原通知父的保留接收者，交既有HandleNotify；未映射/错误接收者/未处理走DefWindowProc，异常沿既有native边界报告。32例Rich尺寸组含4项错误来源/同ID/重挂及真实parentless检查，旧停车/重挂EDIT命令组同时回归。仅关闭该连接，原父Form寿命/其他通知结果和17父项仍待。 |
| ✅ | P1 / Control-17g | 批次069必要依赖：内部HandleNotifyWithResult保留旧OnNotify布尔钩子并默认结果0；Form按NMHDR.hwndFrom路由，容器/Owner和停车接收者回传Rich保护通知的精确0/1。49例覆盖四接收路径、旧入口/钩子一次调用、冲突ID/未映射源、仍活原Form重挂/停车归属和既有Resize/Selection零结果；不扩原父Form销毁重建寿命或公共API，任意高位nint可控返回覆盖NOT_RUN，完整17父项不关闭。 |

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.control?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/class.Widget.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nsview)
