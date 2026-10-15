# Control

🟡 **现状：**批次 002～017 已接入 Component/IWin32Window、组件释放、真实 HWND 投递、显式和默认同步上下文；焦点、旧边界/通知/客户区、SizeFromClientSize 与运行期 Min/Max 的固定参考保留。批次 015 完成基础 0×0 构造、DefaultSize 与构造期默认 Min/Max 的真实虚消费；批次 016 补无 parent 的两个文本构造及所需 Text 虚槽，保留实际文本→Location→Size 路由。批次017完成无父级无自身HWND的Text/ResetText首范围，AOT的DispId删除差异单列。全部具体派生默认值、中文 Designer 分类/描述、AOT 动态属性元数据转换、外部自主通知、Select/CanSelect、完整键盘/布局、Controls/Parent 和关闭竞态仍待后续。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/Control.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：[Control.cs](../../../Lumina.Forms/Control.cs)
- 排序：全库 P0 根基；先于 ScrollableControl/ContainerControl 和所有具体控件。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| 🟡 | P0 / Control-01 | 批次 002 已完成 Component/IWin32Window（Control-01a），批次 003 已实现真实 UI 投递的 ISynchronizeInvoke，批次 015 完成基础 DefaultSize/构造期默认约束（01b），批次 016 完成无 parent 文本构造（01c）。IDropTarget、含 parent 构造、Controls 与全部派生默认/扩展仍待实现，不随子项关闭。 |
| ✅ | P0 / Control-01a | 批次 002：真实无参构造、Component/IComponent/IDisposable/IWin32Window 消费与派生覆盖通过；基础 HWND 由原生 IsWindow/文本长度消费并在释放后失效。完整继承/构造/接口与延迟句柄语义另列待办，兼容验收后置。 |
| ✅ | P0 / Control-01b | 批次 015：基础无参 0×0 与 protected virtual DefaultSize 完成；构造期 DefaultMin/Max 在原始状态按参考重复读取并消费公共虚 setter，随后直接初始化 DefaultSize 和条件客户区，再安装同步上下文。2,677 条固定 10.0.11/JIT/实际 AOT 相同，387 项新增回归，覆盖四边框/正零负极值、冲突/重复读取、不调 base/重入/原异常及构造异常 Current 时序。其余构造、所有具体派生默认/DPI/Form/完整布局与兼容验收另列。 |
| ✅ | P0 / Control-01c | 批次 016：无 parent 的 text 与 text/left/top/width/height 两个标准公共构造完成，text 接受 null，Text 为标准 virtual/AllowNull；默认初始化/context 后消费虚文本，再分别进入 Location 和 Size，保留约束/通知/重入/原异常。4,584 条固定参考/JIT/实际 AOT 指定观测相同，311 项新增回归；构造空值契约与运行时调用约定编码单独保留原始证据，见批次记录。含 parent 构造、完整 Text/native/布局与 Form 继承另列。 |
| 🟡 | P0 / Control-02 | 批次 008～010 完成标准焦点查询（02a）、零尺寸/句柄相关负尺寸（02b）与指定边界/虚 core 入口（02c）；Select/CanSelect、完整键盘/布局仍待后续，不随子范围关闭。 |
| ✅ | P0 / Control-02a | 批次 008：标准 bool Focus() 与 virtual Focused、ContainsFocus/CanFocus 使用真实 GetFocus/IsChild/IsWindowVisible/IsWindowEnabled；无自身 HWND 不创建句柄，隐藏/禁用/释放与外部原生状态有对照。157 条固定参考/JIT/实际 AOT 相同，17 项新增 STA/元数据回归通过。完整选择、键盘/验证及重建竞态另列。 |
| ✅ | P0 / Control-02b | 批次 009：SetBounds 及 Width/Height/Size/Bounds 无自身 HWND 时保留零/负尺寸、不创建句柄；已有 HWND 时负数归零，实际零宽高可恢复。首次创建回读原生矩形，HandleCreated 已见归一结果。756 条固定参考/JIT/实际 AOT 相同，33 项新增回归通过。后续基础构造默认/MinMax/通知见已完成01b/c、09a/c、10a；全部派生约束、完整布局/DPI及外部自主通知仍缺。 |
| ✅ | P0 / Control-02c | 批次 010：标准 BoundsSpecified、四/五参数 SetBounds 的 x/y 名称与 protected virtual SetBoundsCore 实际接入；None/组合/未知位、重复值、属性标志、派生覆盖与直接 core 有 2,034 条固定参考/JIT/实际 AOT 对照，新增 60 项回归通过。外层保留未指定分量，core 接完整矩形，原始 specified 不丢；保留02b尺寸/创建语义。基础Min/Max、通知及客户区见已完成09a/c、10a；完整布局/全部派生约束/外部自主通知仍缺。 |
| 🟡 | P0 / Control-03 | 批次 003 完成现有 HWND/创建线程与父级句柄查找（Control-03a）；标准 Parent/Form 继承链、未启动/已退出消息循环和完整句柄重建竞态仍待对照。 |
| ✅ | P0 / Control-03a | 批次 003：InvokeRequired 查询 HWND 创建线程，沿 Parent 及当前 Owner 找已有投递句柄；无句柄返回 false，Invoke/BeginInvoke 无句柄不创建并抛标准异常；父级/Owner 回退有实际线程对照。 |
| 🟡 | P0 / Control-04 | 批次 003 完成 Action/Delegate/args/Func 同步及 BeginInvoke/EndInvoke 首范围（Control-04a）；完整关闭/重建/线程退出竞态及参数全部边界仍待验收。 |
| ✅ | P0 / Control-04a | 批次 003：真实 PostMessage 队列、同线程 FIFO/EndInvoke、跨线程返回与异常、ExecutionContext、IAsyncResult 和销毁待执行项唤醒；69 条指定观测在固定 WinForms 10.0.11、JIT、AOT 相同。null Delegate 保护与默认异常 UI 的明确差异见修改历史，不等于父任务完整验收。 |
| 🟡 | P0 / Control-05 | 批次 004～007 完成 Action/Func<T>、ValueTask 两族入口及旧路径排队、显式/default context 与 callback await 恢复首范围；完整关闭/重建/退出及多 UI/模态竞态仍保留，不随子范围关闭。 |
| ✅ | P0 / Control-05a | 批次 004：标准 Action/Func<T> 在自身 HWND 线程排队，返回值、原异常及取消保留；旧单 Action/Task 回调也排队。固定 WinForms 10.0.11、JIT、AOT 59 条观测相同，另有旧 Task 路径专项检查；ValueTask/同步上下文与完整关闭竞态未关闭。 |
| ✅ | P0 / Control-05b | 批次 005：标准 Func<CancellationToken,ValueTask> / ValueTask<T> 在 UI 队列首次调用并单次消费，90 条固定参考/JIT/AOT 观测相同；原异常/结果/token、取消后继续消费及 async lambda 选择已检。同步上下文安装、callback 内 await 后 UI 恢复和完整关闭竞态另列，不代表完整异步兼容。 |
| ⬜ | P0 / Control-06 | 实现 Control.ControlCollection、Parent 可设、Controls.Add/AddRange/Remove/Clear/SetChildIndex/Find；Form 直接子项的 Parent 必须是 Form，Owner 不能替代 Parent。 |
| ⬜ | P1 / Control-07 | Handle 延迟创建、CreateControl/CreateHandle/RecreateHandle/DestroyHandle、FromHandle/FromChildHandle 与映射表一致；HandleCreated/HandleDestroyed、重建后属性恢复和 GCHandle 寿命逐项测。 |
| 🟡 | P1 / Control-08 | 批次 002 完成 Component 释放子范围（Control-08a）；UI 父子级联 Dispose、借用 Font/Image/菜单资源所有权和完整事件解绑仍待后续实现。 |
| ✅ | P1 / Control-08a | 批次 002：继承 Component.Dispose 并覆盖 Dispose(bool)，保留派生 OnDisposing；Site 移除、组件容器级联、重复/重入、Dispose(false)、异常传播和状态退出均有回归。5 项生命周期观测与本机 WinForms 10.0.11 相同；不等同完整控件生命周期验收。 |
| 🟡 | P1 / Control-09 | 批次 012～014 完成基础客户区、SizeFromClientSize 及明确初始化后的基础运行期 Min/Max 约束（09a/b/c）；构造期默认约束转 01b。PreferredSize、Dock/Anchor/AutoSize/GetPreferredSize、Padding/Margin、BringToFront/SendToBack 和 z-order、全部派生约束仍待后续，不随子范围关闭。 |
| ✅ | P1 / Control-09a | 批次 012：标准 ClientSize/ClientRectangle、ClientSizeChanged 与 protected virtual SetClientSizeCore/OnClientSizeChanged 完成；明确初始化基础 Control、四有限边框样式、无/有 HWND、零负/恢复、顺序/覆盖/重入/异常共 5,556 条固定参考/JIT/实际 AOT 相同，80 项新增回归通过。保留原始客户区请求缓存与重复通知；客户区缓存变化可使仅位置操作触发标准尺寸通知。旧边界/通知/原生高度扩展保留；基础构造默认与SizeFromClientSize见已完成01b/c、09b，全部派生约束/外部自主通知/完整布局/Form生命周期仍缺。 |
| ✅ | P1 / Control-09b | 批次 013：标准 protected virtual Size SizeFromClientSize(Size clientSize) 与四有限边框、无/有 HWND、正零负极值/unchecked 溢出、覆盖/重入/原异常完成；10,086 条固定参考/JIT/实际 AOT 相同，257 项新增回归通过。直接 base 换算不改状态/客户区缓存/通知/句柄，ClientSize 与 SetClientSizeCore 按参考不消费该虚扩展；原始客户区请求缓存和既有边界/通知保持。完整布局/全部派生约束/DPI 切换/Form 生命周期另列。 |
| ✅ | P1 / Control-09c | 批次 014 运行期首范围：基础 virtual MinimumSize/MaximumSize 与 protected virtual 默认扩展接入真实尺寸 core；四有限边框、无/有 HWND、负零/冲突/重复/取消、12 入口、覆盖/重入/原异常/通知与缓存共 33,408 条固定参考/JIT/实际 AOT 相同，新增 394 项回归通过。构造期默认约束与 DefaultSize 由 01b 一起处理；本批元数据仅固定英文范围，中文 Designer 分类/描述、AOT 动态 AmbientValueAttribute.Value 转换、完整布局/全部派生约束/DPI/Form 生命周期另列。 |
| 🟡 | P1 / Control-10 | 批次 011 完成位置尺寸通知（10a），批次 012 完成基础客户区通知（09a）；嵌套 SuspendLayout、ResumeLayout(false/true)、PerformLayout 重载/LayoutEventArgs 与完整 Layout 顺序仍待后续，不随子范围关闭。 |
| ✅ | P1 / Control-10a | 批次 011：标准 LocationChanged/Move、SizeChanged/Resize 与四个 protected virtual On* 已接入七属性/两族 SetBounds/直接 core；重复、指定/未知位、零负尺寸、覆盖/不调 base、重入/原异常和参数轨迹共 2,280 条固定参考/JIT/实际 AOT 相同，99 项新增回归通过。OnSizeChanged 向 OnResize 传 Empty，SizeChanged 保留原参数；旧OnBoundsChanged独立保留。基础ClientSizeChanged/客户区见已完成09a，外部原生自主移动/尺寸通知、完整Layout及全部派生约束仍缺。 |
| 🟡 | P1 / Control-11 | 批次016完成构造必需的Text虚槽（01c），017完成无父级无自身HWND的Text/ResetText/通知首范围（11a）。Enabled/Visible/Font/BackColor/ForeColor/RightToLeft/Cursor与Text有效值继承、其他Reset/ShouldSerialize/变更事件、已有句柄native及重新Parent仍待实现；不随11a关闭。 |
| ✅ | P1 / Control-11a | 批次017：标准virtual ResetText经虚Text setter，null归空、虚getter判等、重复赋值/身份、事件/覆盖/隐藏/不调base/重入/原异常及释放后托管读写完成；219项新增回归通过。3,774条固定10.0.11参考/JIT全同；实际AOT仅Text的DispId属性被工具链删除，其他3,773条原始记录全同，原差异和运行时策略单列，不能称AOT元数据全同。已有HWND/native、父级继承、完整布局/中文Designer及动态转换另列。 |
| ⬜ | P1 / Control-12 | 对齐 Tab/助记键、Enter/Leave/GotFocus/LostFocus、CausesValidation/Validating/Validated；KeyDown/KeyPress/KeyUp、PreviewKeyDown、ProcessCmdKey/ProcessDialogKey、SuppressKeyPress 与 IME 组合态。 |
| ⬜ | P1 / Control-13 | 实现 MouseDown/Up/Click/DoubleClick/Wheel/Move/Enter/Leave/Hover、Capture/MouseCaptureChanged、坐标换算/PointToClient/PointToScreen；原生通知与反射消息去重。 |
| ⬜ | P1 / Control-14 | 实现 Paint/OnPaint/OnPaintBackground、Invalidate 各重载、Update/Refresh、ControlStyles/DoubleBuffered、CreateParams/WndProc/DefWndProc；派生用户控件可用，不能以原生默认绘制排除这些契约。 |
| ⬜ | P1 / Control-15 | 实现 DataBindings/BindingContext/DataContext 与变化传播、拖放/IDataObject/剪贴板、AccessibleName/Description/Role/AccessibilityObject；各领域引用对应独立任务，不能全部留为存值。 |
| ⬜ | P1 / Control-16 | DesignerSerializationVisibility/DefaultValue/DefaultEvent、ISupportInitialize、.resx Font/Image/Icon/TypeConverter 的消费和裁剪路径逐项验证；真实 VS 设计器与 Native AOT 运行均有证据。 |
| ⬜ | P1 / Control-17 | Windows 用 USER32 子 HWND、COMCTL32 与稳定窗口过程回调；统一 UI 投递、消息反射、布局和释放，不为各具体控件复制第二份 Control。 |
| ⬜ | P2 / Control-18 | Linux 用真实 GtkWidget 和 shared GMainContext；非交互宿主可用 GtkFixed，焦点/输入/可访问性用原生 controller。HWND/WndProc 等 Windows 消息契约单列，普通布局/Font/Image 不任意排除。 |
| ⬜ | P3 / Control-19 | macOS 用 NSView/NSControl 与 AppKit 主线程，target-action/delegate 映射标准事件；翻转坐标与 key-view loop 统一处理，native 指针不能冒称 HWND。 |
| ⬜ | 验收 / Control-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| HWND + 原生窗口过程/消息循环，是现有基础的增量修正；先用双目标探针证明线程、父级、布局、释放。 | GtkWidget/GtkFixed + GMainContext；控件是真 native widget，WinForms 的共享布局/事件规则由库内部对齐。 | NSView/NSControl + NSApplication 主循环；可移植布局与生命周期需保留，Win32 消息扩展作为明确平台契约。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

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
