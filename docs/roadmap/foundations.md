# 公共基础：剩余依赖

公共任务状态以本页表格为准；控件子范围见各自历史台账，未完成父项不随子范围关闭。依赖排序见[执行顺序](execution.md)，证据和未解失败见[验证门槛](validation.md)。Windows 官方 Drawing、固定双目标和平台边界遵循[项目需求](../../PROJECT_REQUIREMENTS.md)。

## P0：必须先关闭的确定性阻塞

037新增有限IME字符分流、TextBox派生输入及UserControl原生焦点入口，22项定向与4,536项全量通过，见[037](batches/037.md)；FND-019/021继续部分，完整输入法与平台仍待。

| ID | 状态 | 工作与验收 | 对应文档 |
| --- | --- | --- | --- |
| FND-001 | 🟡 | 既有Control/Form/TextBoxBase/RichTextBox继承保持；新增标准ButtonBase并使Button/CheckBox/RadioButton直接继承、共用原生BUTTON/主题/默认按钮标志。MaskedTextBox真实模型/直接层级、IDropTarget、完整派生共享成员/默认仍缺，不由基础类存在关闭父项 | [ButtonBase](controls/ButtonBase.md)、[TextBoxBase](controls/TextBoxBase.md)、[RichTextBox](controls/RichTextBox.md) |
| FND-002 | 🟡 | 已补标准ApplicationContext、主循环/模态泵、多UI循环登记、退出取消预检与所属线程清理；既有InvokeAsync/队列失败终止保持。完整外部宿主、极端退出/重入/并发、等待期间释放和默认错误UI仍缺，真机验证集中后置 | [Application](controls/Application.md)、[ApplicationContext](controls/ApplicationContext.md)、[线程上下文](controls/WindowsFormsSynchronizationContext.md) |
| FND-003 | 🟡 | 既有列表条目/子项构造、集合与未挂接样式保持；本轮补ListView Items/Columns所属与真实LVM项/列/子项文字同步、Tree节点所属/循环拒绝/TVM插入更新删除及HTREEITEM清理。完整集合API、ImageList解析/转换器、Group/样式/资源继承/复杂重入与平台仍缺 | [ListView](controls/ListView.md)、[ListViewItem](controls/ListViewItem.md)、[TreeView](controls/TreeView.md)、[TreeNodeCollection](controls/TreeNodeCollection.md) |
| FND-004 | 🟡 | 018～026 Demo窗口证据保留；027 Native AOT原生窗口可见、程序BM_CLICK使Enable option 0→1、WM_CLOSE正常exit0且原HWND全部销毁。锁屏下物理鼠标命中未验收；模态、完整布局/业务交互、设计器/可访问性及018文字裁切仍未关闭 | [027证据](batches/027.md)、[原评估](../assessments/2026-10-15/README.md) |
| FND-006 | 🟡 | 既有Bitmap内建byte[]/静态解码保持；本轮补Icon内联/ResXFileRef、Font不变量字符串构建期转版本化primitive记录与运行期直接构造，查找对象独立且保留必要流寿命。Container.ValidateName→InheritanceAttribute.ToString/Enum.GetValues(Type)的旧IL3050、二进制Font/其他类型/ApplyResources与完整Designer/AOT运行仍未关闭，无全局suppress | [资源解码](../../Lumina.Forms/Resources/ResourceManager.cs)、[构建转换](../../NativeFormsDemo/Build/BitmapResources.targets)、[平台依赖](dependencies.md) |

## P1：所有控件共享的工作

| ID | 状态 | 任务细节与验收 |
| --- | --- | --- |
| FND-010 | 🟡 | 018的Control-06a完成集合/Parent及Windows重挂；019修复隐藏重挂的中间VisibleChanged，391行参考/JIT现全部相同。IList.Insert按标准不支持，AOT32条非零下界数组fixture未执行CopyTo仍保留。完整布局/继承通知、派生异常/重入全部组合与跨平台仍缺 |
| FND-011 | 🟡 | 019～049句柄/消息/销毁边界与停车WM_COMMAND连接保留；067的Control-17f另记自有停车WM_NOTIFY来源HWND连接，069的17g仅补必要原生返回值与Form来源识别。原反射父Form销毁/重建寿命、其他WM_NOTIFY结果语义/完整反射、全部派生状态恢复、自定义Param AOT、关闭竞态与三平台仍缺，Created隐藏属性差异保留 |
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

## 已完成任务

完成行和当前表共同构成完整任务台账；按日期/批次的变更与状态纠正记录统一见根目录 [CHANGELOG](../../CHANGELOG.md)。当前开放门槛统一见[验证](validation.md)。

### 已完成任务（历史台账）

以下原任务行从当前任务表迁入历史，ID、状态、明确范围与验证限制原样保留；现有导出工具仍读取这些行。✅ 仅表示定义范围代码完成，未完父项与平台验收仍在当前表。

| ID | 状态 | 工作与验收 | 对应文档 |
| --- | --- | --- | --- |
| FND-005 | ✅ | 批次 001 对齐 Run(Form mainForm) 的 void、SetHighDpiMode 的 bool、HighDpiMode 数值；连接真实 DPI 设置/读取及建窗门禁，14 组隔离场景与本机固定基线对照通过；完整消息循环与真机缩放验收仍待后续任务 | [Application](controls/Application.md) |



## 三平台共同原则

Windows 以现有 Win32 HWND 与标准控件为起点，复用成熟的系统输入/可访问性能力。GTK4 使用 GtkWidget/GtkWindow/GMainContext，AppKit 使用 NSView/NSWindow/NSApplication；库内部将信号、target-action、delegate 转成现有 WinForms 事件。托管集合、布局和格式规则可共享，公共 Handle/WndProc/Windows 消息不能在其他平台假造为等价。

Linux Wayland 不保证任意窗口位置/全局坐标；macOS 的应用菜单、首响应者、工作表模态和主线程规则不同。逐控件文档记录这些差异，不为 Windows 外观造自绘框架。所有平台原型先过字体/图像公共类型、AOT 回调、主循环、输入法、可访问性和分发验证，参见 [平台计划](platforms.md)。

依据：本地 [Control.cs](../../Lumina.Forms/Control.cs)、[Form.cs](../../Lumina.Forms/Form.cs)、[探针](../assessments/2026-10-15/runtime-probes.json)；[微软 Control](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.control?view=windowsdesktop-10.0)、[事件顺序](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/forms/events)、[GTK Window](https://docs.gtk.org/gtk4/class.Window.html)、[AppKit NSWindow](https://developer.apple.com/documentation/appkit/nswindow)。
