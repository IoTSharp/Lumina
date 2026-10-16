# 公共基础：先修这些，再铺控件

依赖顺序：`Component/Control → ScrollableControl → ContainerControl → Form/UserControl`；`ButtonBase/TextBoxBase/ListControl/UpDownBase` 与容器/集合各自接入该体系。**不新造公共框架或公开平台接口。** 分平台实现先放现有 Lumina.Forms 内部，等实证表明确有必要再拆项目。

## P0：必须先关闭的确定性阻塞

| ID | 状态 | 工作与验收 | 对应文档 |
| --- | --- | --- | --- |
| FND-001 | 🟡 | 002～018基础构造、ControlCollection/Parent与Form标准链保留；019补Control-07a标准句柄入口/状态/事件及Form必要覆盖。IDropTarget、所有派生默认/完整扩展契约仍缺，不由基类或构造关闭父项 | [Control](controls/Control.md)、[Form](controls/Form.md)、[批次019](batches/019.md) |
| FND-002 | 🟡 | 003～025投递/context范围保留；026的Control-05c补已接受InvokeAsync任务关闭/Post/Attach失败终止，保留取消先后/重建/已开始异步工作和ValueTask单消费。ApplicationContext/模态、多UI退出、等待期间释放同一wait与完整竞态仍缺；null Delegate/Send、关闭增强和错误UI差异保留 | [线程上下文](controls/WindowsFormsSynchronizationContext.md)、[批次026](batches/026.md) |
| FND-003 | 🟡 | 批次001～017完成列表基础构造/子项样式与key、集合共享view/整数索引/typed增删/非泛型IList/查询复制/枚举、两级字符串、主项元数据/未挂接样式及未挂接ImageIndex/ImageKey状态首范围；017图像选择518条参考/JIT相同。其余构造、ImageList解析/挂接native、图像转换器/编辑器、分组、资源继承、完整所属/并发及Items/SubItems/Columns/Nodes原生同步仍待实现，不把局部状态/集合契约当作完整所属 | [ListViewItem](controls/ListViewItem.md)、[ListViewSubItem](controls/ListViewSubItem.md)、[TreeView](controls/TreeView.md) |
| FND-004 | 🟡 | 018～026 Demo窗口证据保留；027 Native AOT原生窗口可见、程序BM_CLICK使Enable option 0→1、WM_CLOSE正常exit0且原HWND全部销毁。锁屏下物理鼠标命中未验收；模态、完整布局/业务交互、设计器/可访问性及018文字裁切仍未关闭 | [027证据](batches/027.md)、[原评估](../assessments/2026-10-15/README.md) |
| FND-005 | ✅ | 批次 001 对齐 Run(Form mainForm) 的 void、SetHighDpiMode 的 bool、HighDpiMode 数值；连接真实 DPI 设置/读取及建窗门禁，14 组隔离场景与本机固定基线对照通过；完整消息循环与真机缩放验收仍待后续任务 | [Application](controls/Application.md) |
| FND-006 | 🟡 | 批次 002 完成 Demo 五张 Bitmap 的内建 byte[] 资源/直接解码，三端像素及身份语义对照通过，IL2026 已消除。原 IL3053 展开为 Container.ValidateName 根化 InheritanceAttribute.ToString/Enum.GetValues(Type) 的 IL3050，仍未关闭；Icon/Font/其他 TypeConverter 与完整设计器/主窗另列待办，不用全局 suppress 代替处理 | [跨平台资源](platforms.md)、[批次 002](batches/002.md) |

## P1：所有控件共享的工作

| ID | 状态 | 任务细节与验收 |
| --- | --- | --- |
| FND-010 | 🟡 | 018的Control-06a完成集合/Parent及Windows重挂；019修复隐藏重挂的中间VisibleChanged，391行参考/JIT现全部相同。IList.Insert按标准不支持，AOT32条非零下界数组fixture未执行CopyTo仍保留。完整布局/继承通知、派生异常/重入全部组合与跨平台仍缺 |
| FND-011 | 🟡 | 019句柄、023标准消息/创建参数保留；024的04b保证Close失败后的实际销毁，025的07b/17b与Form-16c完成有限native callback/清理双错边界。全部派生状态恢复、消息反射、自定义Param AOT、关闭竞态与三平台仍缺；Created隐藏属性差异保留 |
| FND-012 | 🟡 | 002～025资源/释放范围保留；026的Control-08c补HWND已销毁后异常继续正常托管阶段，原错和后续错不丢失；正常Parent/child/Site失败及活HWND失败保留短路重试。其余图像/菜单/派生资源、完整异常事务/事件解绑与竞态仍缺，不由局部收尾关闭父项 |
| FND-013 | 🟡 | 018/022基础外观父链、023 Styles/UserPaint保留；024补BackgroundImage通知传播及基础可见父子透明/alpha。Application/Site/ActiveX ambient、RightToLeft/Cursor、滚动/主题/全部派生透明、DPI和完整Designer默认仍缺；默认palette三条前景差异保留 |
| FND-014 | 🟡 | 009～020基础Bounds/通知/客户区/MinMax/构造和事务保留；021完成Control-09d基础默认Dock/Anchor/AutoSize、PreferredSize和必要容器内容测量。完整派生测量/RTL/DPI/布局重入、继承属性和平台仍缺 |
| FND-015 | 🟡 | 020标准事务10b、021标准引擎/缓存/InitLayout与Resume(false)协作10c完成；512行参考/JIT/实际AOT同，旧394行参考/JIT差异清零。非Control布局对象、复杂缓存重入/异常恢复、完整容器算法/Designer仍缺，不关闭父项 |
| FND-016 | 🟡 | 018已有ScrollableControl无滚动客户区DisplayRectangle；AutoScrollPosition/MinSize、横纵滚动、Dock协作、嵌套滚动坐标与平台滚动仍缺 |
| FND-017 | 🟡 | 历史Form已有AutoScaleMode/Dimensions/PerformAutoScale算法，018迁入ContainerControl并回归；原全部未开始状态遗漏旧局部实现，现纠正。完整Font/Dpi/None/Inherit、AutoScaleFactor、DpiChanged父子通知与96/144/192往返仍缺 |
| FND-018 | 🟡 | 018～028基础导航/焦点事件保留；029的Control-12c和ContainerControl-02b补禁用/隐藏、移除候选及程序验证协作。全部派生、真实Tab/助记键、默认/取消按钮与关闭焦点差异仍缺；公开Focus无参，定向入口为Select(bool,bool) |
| FND-019 | ⬜ | KeyDown/KeyPress/KeyUp、PreviewKeyDown、ProcessCmdKey/ProcessDialogKey/PreProcessMessage、KeyPreview、抑制按键；中文 IME 组合态、焦点切换与候选窗位置 |
| FND-020 | 🟡 | 027完成Control-13a坐标/捕获、13b基础鼠标事件/消息跟踪，必要原生消费者共用Click且避免重复。验证取消、全部派生反射/默认样式/AutoCheck/组同步、RTL/DPI及物理设备仍缺；锁屏造成WindowFromPoint命中失败，不把程序消息结果计作成功设备点击 |
| FND-021 | 🟡 | 029完成Control-12d标准验证事件/属性、ContainerControl-03a的AutoValidate/Validate/ValidateChildren程序状态机及Form-07a关闭前置；取消恢复/Allow/Disable/重入/异常与批量后代顺序有证据。点击/提交抑制、全部派生默认/继承、真实输入及完整平台行为仍缺 |
| FND-022 | ⬜ | DataBindings、BindingContext、BindingSource：属性写回/通知/错误，保留现有 API；AOT 绑定策略先通过真实绑定样例再推广 |
| FND-023 | 🟡 | 023消息/样式/基础Paint保留；024的14b补背景图像五布局、真实屏幕透明/alpha及父Graphics/DC状态、失效传播，21场景307行JIT/AOT全同（参考关闭差异16行另列）。RTL/mirror、滚动/主题透明、完整owner draw/反射/派生绘制、缓冲失败与资源压力仍缺，父项不关闭 |
| FND-024 | ⬜ | AllowDrop/DragEnter/Over/Leave/Drop、DoDragDrop、GiveFeedback/QueryContinueDrag、Clipboard/DataObject；平台格式与内存所有权有测试 |
| FND-025 | ⬜ | AccessibleName/Description/Role、AccessibilityObject/CreateAccessibilityInstance、键盘焦点与选择通知；UIA/AT-SPI/NSAccessibility 均由对应后端验证 |
| FND-026 | ⬜ | ISupportInitialize/BeginInit/EndInit、IExtenderProvider、DefaultValue/DefaultEvent/DesignerSerializationVisibility、.resx/文化、图像和图标；保存/关闭/重开设计器并编译双目标 |

## 收尾状态整理记录

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
