# 公共基础：先修这些，再铺控件

依赖顺序：`Component/Control → ScrollableControl → ContainerControl → Form/UserControl`；`ButtonBase/TextBoxBase/ListControl/UpDownBase` 与容器/集合各自接入该体系。**不新造公共框架或公开平台接口。** 分平台实现先放现有 Lumina.Forms 内部，等实证表明确有必要再拆项目。

## P0：必须先关闭的确定性阻塞

| ID | 状态 | 工作与验收 | 对应文档 |
| --- | --- | --- | --- |
| FND-001 | ⬜ | 修正 Control 继承 Component、可实例化性、IWin32Window/ISynchronizeInvoke 等标准契约；Form 进入标准继承链；对照编译强制转换/派生/容器消费源码 | [Control](controls/Control.md)、[Form](controls/Form.md) |
| FND-002 | ⬜ | InvokeRequired 判断创建线程并沿父句柄查找；Invoke/BeginInvoke/EndInvoke/InvokeAsync 投递到 UI 队列；返回、异常、取消、无句柄、释放行为与真实端对照 | [线程上下文](controls/WindowsFormsSynchronizationContext.md) |
| FND-003 | 🟡 | 批次 001 已完成 string 构造空引用、唯一首子项和 Text/null 一致性（ListViewItem-01a）；完整构造族及 Items/SubItems/Columns/Nodes 托管与原生同步仍待实现 | [ListViewItem](controls/ListViewItem.md)、[TreeView](controls/TreeView.md) |
| FND-004 | ⬜ | 完整 Demo 的 AOT 可见主窗、一次操作、模态/关闭/正常退出验收；原隐藏短探针未完成，不直接归咎 AOT | [已有证据](../assessments/2026-10-15/README.md) |
| FND-005 | ✅ | 批次 001 对齐 Run(Form mainForm) 的 void、SetHighDpiMode 的 bool、HighDpiMode 数值；连接真实 DPI 设置/读取及建窗门禁，14 组隔离场景与本机固定基线对照通过；完整消息循环与真机缩放验收仍待后续任务 | [Application](controls/Application.md) |
| FND-006 | ⬜ | IL2026/IL3053 追到具体资源/转换路径；图片、图标、Font、TypeConverter 在裁剪后的实际运行验证，不用全局 suppress 代替处理 | [跨平台资源](platforms.md) |

## P1：所有控件共享的工作

| ID | 状态 | 任务细节与验收 |
| --- | --- | --- |
| FND-010 | ⬜ | ControlCollection：Add/AddRange/Insert/Remove/Clear/SetChildIndex/Find/索引器；ParentChanged、ControlAdded/Removed、重复添加、同名查找、循环父子、重设父级、所有者和销毁后的行为 |
| FND-011 | ⬜ | Handle/CreateControl/CreateHandle/RecreateHandle/DestroyHandle：延迟创建、原生映射、HandleCreated/Destroyed 顺序、重建时恢复状态、窗口过程回调寿命；释放后不保留 GCHandle |
| FND-012 | ⬜ | Dispose(bool)、Disposed、Site/Container、组件容器级联释放、事件解绑、图像/字体/菜单所有权；重复 Dispose 不重复销毁原生资源 |
| FND-013 | ⬜ | Enabled/Visible/Font/BackColor/ForeColor/RightToLeft 的继承和变更事件、重设父级后有效值；设计器不序列化继承默认值 |
| FND-014 | ⬜ | Bounds/ClientSize/ClientRectangle/MinimumSize/MaximumSize、Dock/Anchor/AutoSize/GetPreferredSize、Padding/Margin、Z-order；布局之前/之后可回读正确状态 |
| FND-015 | ⬜ | SuspendLayout 计数、ResumeLayout(bool)、PerformLayout 重载和 LayoutEventArgs；嵌套暂停、请求合并、重入后收敛；禁止用空实现通过 Designer 编译 |
| FND-016 | ⬜ | AutoScrollPosition/AutoScrollMinSize/DisplayRectangle、水平/垂直滚动及 Dock 的协作；容器内嵌套布局和滚动坐标转换 |
| FND-017 | ⬜ | AutoScaleMode Font/Dpi/None/Inherit、AutoScaleDimensions、PerformAutoScale、DpiChanged/BeforeParent/AfterParent；96/144/192 DPI 往返不重复放大 |
| FND-018 | ⬜ | TabIndex/TabStop/Select/Focus(bool)、Enter/Leave/GotFocus/LostFocus、嵌套 ActiveControl；助记键、默认/取消按钮；不能仅靠原生 Tab 行为猜测事件顺序 |
| FND-019 | ⬜ | KeyDown/KeyPress/KeyUp、PreviewKeyDown、ProcessCmdKey/ProcessDialogKey/PreProcessMessage、KeyPreview、抑制按键；中文 IME 组合态、焦点切换与候选窗位置 |
| FND-020 | ⬜ | MouseDown/Up/Click/DoubleClick/Wheel/Move/Enter/Leave/Hover、Capture、HitTest；坐标、按键、双击和原生消息不重复派发 |
| FND-021 | ⬜ | CausesValidation、AutoValidate、Validating(Cancel)/Validated、Validate/ValidateChildren；取消验证后焦点与提交行为 |
| FND-022 | ⬜ | DataBindings、BindingContext、BindingSource：属性写回/通知/错误，保留现有 API；AOT 绑定策略先通过真实绑定样例再推广 |
| FND-023 | ⬜ | Paint/OnPaint/OnPaintBackground/Invalidate/Update/Refresh、ControlStyles、DoubleBuffered、WndProc/Message/CreateParams、owner draw；派生控件对照；Windows 原生与派生绘制是独立能力 |
| FND-024 | ⬜ | AllowDrop/DragEnter/Over/Leave/Drop、DoDragDrop、GiveFeedback/QueryContinueDrag、Clipboard/DataObject；平台格式与内存所有权有测试 |
| FND-025 | ⬜ | AccessibleName/Description/Role、AccessibilityObject/CreateAccessibilityInstance、键盘焦点与选择通知；UIA/AT-SPI/NSAccessibility 均由对应后端验证 |
| FND-026 | ⬜ | ISupportInitialize/BeginInit/EndInit、IExtenderProvider、DefaultValue/DefaultEvent/DesignerSerializationVisibility、.resx/文化、图像和图标；保存/关闭/重开设计器并编译双目标 |

## 三平台共同原则

Windows 以现有 Win32 HWND 与标准控件为起点，复用成熟的系统输入/可访问性能力。GTK4 使用 GtkWidget/GtkWindow/GMainContext，AppKit 使用 NSView/NSWindow/NSApplication；库内部将信号、target-action、delegate 转成现有 WinForms 事件。托管集合、布局和格式规则可共享，公共 Handle/WndProc/Windows 消息不能在其他平台假造为等价。

Linux Wayland 不保证任意窗口位置/全局坐标；macOS 的应用菜单、首响应者、工作表模态和主线程规则不同。逐控件文档记录这些差异，不为 Windows 外观造自绘框架。所有平台原型先过字体/图像公共类型、AOT 回调、主循环、输入法、可访问性和分发验证，参见 [平台计划](platforms.md)。

依据：本地 [Control.cs](../../Lumina.Forms/Control.cs)、[Form.cs](../../Lumina.Forms/Form.cs)、[探针](../assessments/2026-10-15/runtime-probes.json)；[微软 Control](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.control?view=windowsdesktop-10.0)、[事件顺序](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/forms/events)、[GTK Window](https://docs.gtk.org/gtk4/class.Window.html)、[AppKit NSWindow](https://developer.apple.com/documentation/appkit/nswindow)。
