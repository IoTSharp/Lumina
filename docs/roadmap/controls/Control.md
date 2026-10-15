# Control

🟡 **现状：**批次 002～012 已接入 Component/IWin32Window、组件释放、真实 HWND 投递、显式和默认同步上下文；焦点 157 条、零/负尺寸与首次创建 756 条、指定边界 2,034 条及位置尺寸通知 2,280 条既有固定参考/JIT/实际 AOT 观测保留。批次 012 完成基础客户区与 ClientSizeChanged 首范围，5,556 条固定参考/JIT/实际 AOT 相同，80 项新增回归通过。SizeFromClientSize 扩展、外部原生自主通知、Select/CanSelect、完整键盘/布局、构造、Controls/Parent 和关闭竞态仍待后续。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/Control.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：[Control.cs](../../../Lumina.Forms/Control.cs)
- 排序：全库 P0 根基；先于 ScrollableControl/ContainerControl 和所有具体控件。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| 🟡 | P0 / Control-01 | 批次 002 已完成 Component/IWin32Window（Control-01a），批次 003 已实现真实 UI 投递的 ISynchronizeInvoke；IDropTarget、完整构造、Controls 与派生扩展仍待实现，不随子项关闭。 |
| ✅ | P0 / Control-01a | 批次 002：真实无参构造、Component/IComponent/IDisposable/IWin32Window 消费与派生覆盖通过；基础 HWND 由原生 IsWindow/文本长度消费并在释放后失效。完整继承/构造/接口与延迟句柄语义另列待办，兼容验收后置。 |
| 🟡 | P0 / Control-02 | 批次 008～010 完成标准焦点查询（02a）、零尺寸/句柄相关负尺寸（02b）与指定边界/虚 core 入口（02c）；Select/CanSelect、完整键盘/布局仍待后续，不随子范围关闭。 |
| ✅ | P0 / Control-02a | 批次 008：标准 bool Focus() 与 virtual Focused、ContainsFocus/CanFocus 使用真实 GetFocus/IsChild/IsWindowVisible/IsWindowEnabled；无自身 HWND 不创建句柄，隐藏/禁用/释放与外部原生状态有对照。157 条固定参考/JIT/实际 AOT 相同，17 项新增 STA/元数据回归通过。完整选择、键盘/验证及重建竞态另列。 |
| ✅ | P0 / Control-02b | 批次 009：SetBounds 及 Width/Height/Size/Bounds 无自身 HWND 时保留零/负尺寸、不创建句柄；已有 HWND 时负数归零，实际零宽高可恢复。首次创建回读原生矩形，HandleCreated 已见归一结果。756 条固定参考/JIT/实际 AOT 相同，33 项新增回归通过。构造默认大小、全部派生约束、完整布局/Min/Max/事件另列。 |
| ✅ | P0 / Control-02c | 批次 010：标准 BoundsSpecified、四/五参数 SetBounds 的 x/y 名称与 protected virtual SetBoundsCore 实际接入；None/组合/未知位、重复值、属性标志、派生覆盖与直接 core 有 2,034 条固定参考/JIT/实际 AOT 对照，新增 60 项回归通过。外层保留未指定分量，core 接完整矩形，原始 specified 不丢；保留 02b 尺寸/创建语义。完整布局/Min/Max/事件与全部派生约束另列。 |
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
| 🟡 | P1 / Control-09 | 批次 012 完成基础客户区（09a）；SizeFromClientSize、PreferredSize、MinimumSize/MaximumSize、Dock/Anchor/AutoSize/GetPreferredSize、Padding/Margin、BringToFront/SendToBack 和 z-order 及全部派生约束仍待后续，不随子范围关闭。 |
| ✅ | P1 / Control-09a | 批次 012：标准 ClientSize/ClientRectangle、ClientSizeChanged 与 protected virtual SetClientSizeCore/OnClientSizeChanged 完成；明确初始化基础 Control、四有限边框样式、无/有 HWND、零负/恢复、顺序/覆盖/重入/异常共 5,556 条固定参考/JIT/实际 AOT 相同，80 项新增回归通过。保留原始客户区请求缓存与重复通知；客户区缓存变化可使仅位置操作触发标准尺寸通知。旧边界/通知/原生高度扩展保留；构造默认、SizeFromClientSize、全部派生约束、外部自主通知、完整布局和 Form 生命周期另列。 |
| ⬜ | P1 / Control-09b | 批次 013 首范围：固定参考补 protected SizeFromClientSize 的真实形状/输入输出、是否改变状态/创建句柄与标准入口的实际虚调用轨迹；基础 Control/四有限边框、正零负极值、覆盖/重入/异常。完整布局/全部派生约束/DPI 切换/Form 生命周期另列。 |
| 🟡 | P1 / Control-10 | 批次 011 完成位置尺寸通知（10a），批次 012 完成基础客户区通知（09a）；嵌套 SuspendLayout、ResumeLayout(false/true)、PerformLayout 重载/LayoutEventArgs 与完整 Layout 顺序仍待后续，不随子范围关闭。 |
| ✅ | P1 / Control-10a | 批次 011：标准 LocationChanged/Move、SizeChanged/Resize 与四个 protected virtual On* 已接入七属性/两族 SetBounds/直接 core；重复、指定/未知位、零负尺寸、覆盖/不调 base、重入/原异常和参数轨迹共 2,280 条固定参考/JIT/实际 AOT 相同，99 项新增回归通过。OnSizeChanged 向 OnResize 传 Empty，SizeChanged 保留原参数；旧 OnBoundsChanged 独立保留。ClientSizeChanged/客户区、外部原生自主移动/尺寸通知、完整 Layout 与全部派生约束另列。 |
| ⬜ | P1 / Control-11 | 实现 Enabled/Visible/Text/Font/BackColor/ForeColor/RightToLeft/Cursor 的有效值继承、Reset/ShouldSerialize 和变更事件；句柄前后赋值与重新 Parent 结果一致。 |
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

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.control?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/class.Widget.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nsview)
