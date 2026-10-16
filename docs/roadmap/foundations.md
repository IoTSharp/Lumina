# 公共基础：先修这些，再铺控件

依赖顺序：`Component/Control → ScrollableControl → ContainerControl → Form/UserControl`；`ButtonBase/TextBoxBase/ListControl/UpDownBase` 与容器/集合各自接入该体系。**不新造公共框架或公开平台接口。** 分平台实现先放现有 Lumina.Forms 内部，等实证表明确有必要再拆项目。

## P0：必须先关闭的确定性阻塞

| ID | 状态 | 工作与验收 | 对应文档 |
| --- | --- | --- | --- |
| FND-001 | 🟡 | 002～018基础构造、ControlCollection/Parent与Form标准链保留；019补Control-07a标准句柄入口/状态/事件及Form必要覆盖。IDropTarget、所有派生默认/完整扩展契约仍缺，不由基类或构造关闭父项 | [Control](controls/Control.md)、[Form](controls/Form.md)、[批次019](batches/019.md) |
| FND-002 | 🟡 | 003～007投递/context及019重建保留回调证据保留；023修原生销毁完成异常的最外层捕获和最终释放，但已释放AsyncWaitHandle仍使Close中断剩余队列，优先留024。完整ApplicationContext/模态、多UI退出与关闭/重建竞态仍缺；null Delegate/Send和默认错误UI差异保留 | [线程上下文](controls/WindowsFormsSynchronizationContext.md)、[批次024](batches/024.md) |
| FND-003 | 🟡 | 批次001～017完成列表基础构造/子项样式与key、集合共享view/整数索引/typed增删/非泛型IList/查询复制/枚举、两级字符串、主项元数据/未挂接样式及未挂接ImageIndex/ImageKey状态首范围；017图像选择518条参考/JIT相同。其余构造、ImageList解析/挂接native、图像转换器/编辑器、分组、资源继承、完整所属/并发及Items/SubItems/Columns/Nodes原生同步仍待实现，不把局部状态/集合契约当作完整所属 | [ListViewItem](controls/ListViewItem.md)、[ListViewSubItem](controls/ListViewSubItem.md)、[TreeView](controls/TreeView.md) |
| FND-004 | 🟡 | 018～023完整Demo AOT已真实显示，Enable option复选框0→1，WM_CLOSE正常exit0且记录HWND全部销毁。模态、完整布局/业务交互、设计器和可访问性仍未验收；018截图的文字裁切未据此宣称修复，不关闭父项 | [023证据](batches/023.md)、[原评估](../assessments/2026-10-15/README.md) |
| FND-005 | ✅ | 批次 001 对齐 Run(Form mainForm) 的 void、SetHighDpiMode 的 bool、HighDpiMode 数值；连接真实 DPI 设置/读取及建窗门禁，14 组隔离场景与本机固定基线对照通过；完整消息循环与真机缩放验收仍待后续任务 | [Application](controls/Application.md) |
| FND-006 | 🟡 | 批次 002 完成 Demo 五张 Bitmap 的内建 byte[] 资源/直接解码，三端像素及身份语义对照通过，IL2026 已消除。原 IL3053 展开为 Container.ValidateName 根化 InheritanceAttribute.ToString/Enum.GetValues(Type) 的 IL3050，仍未关闭；Icon/Font/其他 TypeConverter 与完整设计器/主窗另列待办，不用全局 suppress 代替处理 | [跨平台资源](platforms.md)、[批次 002](batches/002.md) |

## P1：所有控件共享的工作

| ID | 状态 | 任务细节与验收 |
| --- | --- | --- |
| FND-010 | 🟡 | 018的Control-06a完成集合/Parent及Windows重挂；019修复隐藏重挂的中间VisibleChanged，391行参考/JIT现全部相同。IList.Insert按标准不支持，AOT32条非零下界数组fixture未执行CopyTo仍保留。完整布局/继承通知、派生异常/重入全部组合与跨平台仍缺 |
| FND-011 | 🟡 | 019完成Control-07a与Form-06a：延迟Handle、创建销毁重建、事件/映射、基础可见树与回调队列；023补标准Message/CreateParams/WndProc及Form销毁最终清理。全部派生属性/焦点/资源恢复、完整消息反射、自定义Param的AOT封送、关闭竞态与三平台后端仍缺；Form历史Created事件隐藏属性差异保留 |
| FND-012 | 🟡 | 002/018/019释放与句柄异常首范围保留；022 Control-08b补借用托管Font/每控件HFONT及失败重建停放子窗寿命。全部异常/关闭竞态、事件解绑、图像/菜单和完整派生资源仍缺 |
| FND-013 | 🟡 | 018 Enabled/Visible与022 Control-11b基础Font/BackColor/ForeColor父链、默认/Reset/internal ShouldSerialize/通知完成；023实现基础Styles/UserPaint，原10条WM_GETFONT差异由真实绘制路径修复。Application/Site/ActiveX ambient、RightToLeft/Cursor、可见透明合成、DPI和完整Designer默认仍缺；默认palette三条前景差异保留 |
| FND-014 | 🟡 | 009～020基础Bounds/通知/客户区/MinMax/构造和事务保留；021完成Control-09d基础默认Dock/Anchor/AutoSize、PreferredSize和必要容器内容测量。完整派生测量/RTL/DPI/布局重入、继承属性和平台仍缺 |
| FND-015 | 🟡 | 020标准事务10b、021标准引擎/缓存/InitLayout与Resume(false)协作10c完成；512行参考/JIT/实际AOT同，旧394行参考/JIT差异清零。非Control布局对象、复杂缓存重入/异常恢复、完整容器算法/Designer仍缺，不关闭父项 |
| FND-016 | 🟡 | 018已有ScrollableControl无滚动客户区DisplayRectangle；AutoScrollPosition/MinSize、横纵滚动、Dock协作、嵌套滚动坐标与平台滚动仍缺 |
| FND-017 | 🟡 | 历史Form已有AutoScaleMode/Dimensions/PerformAutoScale算法，018迁入ContainerControl并回归；原全部未开始状态遗漏旧局部实现，现纠正。完整Font/Dpi/None/Inherit、AutoScaleFactor、DpiChanged父子通知与96/144/192往返仍缺 |
| FND-018 | 🟡 | 018补默认TabStop、添加时TabIndex与嵌套ActiveControl/显式ActivateControl首范围；Select/Focus(bool)、Enter/Leave/验证完整事件、Tab/助记键和默认/取消按钮仍缺 |
| FND-019 | ⬜ | KeyDown/KeyPress/KeyUp、PreviewKeyDown、ProcessCmdKey/ProcessDialogKey/PreProcessMessage、KeyPreview、抑制按键；中文 IME 组合态、焦点切换与候选窗位置 |
| FND-020 | ⬜ | MouseDown/Up/Click/DoubleClick/Wheel/Move/Enter/Leave/Hover、Capture、HitTest；坐标、按键、双击和原生消息不重复派发 |
| FND-021 | ⬜ | CausesValidation、AutoValidate、Validating(Cancel)/Validated、Validate/ValidateChildren；取消验证后焦点与提交行为 |
| FND-022 | ⬜ | DataBindings、BindingContext、BindingSource：属性写回/通知/错误，保留现有 API；AOT 绑定策略先通过真实绑定样例再推广 |
| FND-023 | 🟡 | 023完成Control-14a/17a标准Message/CreateParams/WndProc/Styles、基础Paint/失效/更新/打印与必要Form衔接。18场景558行JIT严格同参考，AOT自定义Param四差异保留；BackgroundImage、可见透明合成、完整owner draw/反射/派生绘制与资源压力仍缺。Windows原生与用户绘制分开验收，不关闭父项 |
| FND-024 | ⬜ | AllowDrop/DragEnter/Over/Leave/Drop、DoDragDrop、GiveFeedback/QueryContinueDrag、Clipboard/DataObject；平台格式与内存所有权有测试 |
| FND-025 | ⬜ | AccessibleName/Description/Role、AccessibilityObject/CreateAccessibilityInstance、键盘焦点与选择通知；UIA/AT-SPI/NSAccessibility 均由对应后端验证 |
| FND-026 | ⬜ | ISupportInitialize/BeginInit/EndInit、IExtenderProvider、DefaultValue/DefaultEvent/DesignerSerializationVisibility、.resx/文化、图像和图标；保存/关闭/重开设计器并编译双目标 |

## 收尾状态整理记录

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
