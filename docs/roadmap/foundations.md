# 公共基础：先修这些，再铺控件

依赖顺序：`Component/Control → ScrollableControl → ContainerControl → Form/UserControl`；`ButtonBase/TextBoxBase/ListControl/UpDownBase` 与容器/集合各自接入该体系。**不新造公共框架或公开平台接口。** 分平台实现先放现有 Lumina.Forms 内部，等实证表明确有必要再拆项目。

## P0：必须先关闭的确定性阻塞

| ID | 状态 | 工作与验收 | 对应文档 |
| --- | --- | --- | --- |
| FND-001 | 🟡 | 批次 002 完成 Control : Component/IWin32Window 及组件生命周期，批次 003 接入有真实 UI 投递的 ISynchronizeInvoke；Form 标准继承链、完整构造族及 Controls/Parent 仍待实现 | [Control](controls/Control.md)、[Form](controls/Form.md) |
| FND-002 | 🟡 | 批次 003/004 完成同步及 Action/Func<T> 异步投递；批次 005 补标准 ValueTask 两族入口，90 条固定参考/JIT/AOT 观测相同，包含 source 单次消费与取消。标准同步上下文、callback 内 await 后 UI 恢复及完整关闭竞态仍保留；null Delegate 与默认错误 UI 差异见批次记录 | [线程上下文](controls/WindowsFormsSynchronizationContext.md)、[批次 005](batches/005.md) |
| FND-003 | 🟡 | 批次 001/003 完成 ListViewItem 基础构造（41 条观测）；批次 004/005 完成 ListViewSubItem 基础公共构造和五参样式构造（分别 37 条观测）。已挂接资源继承、其余构造及 Items/SubItems/Columns/Nodes 托管与原生同步仍待实现 | [ListViewItem](controls/ListViewItem.md)、[ListViewSubItem](controls/ListViewSubItem.md)、[TreeView](controls/TreeView.md) |
| FND-004 | ⬜ | 完整 Demo 的 AOT 可见主窗、一次操作、模态/关闭/正常退出验收；原隐藏短探针未完成，不直接归咎 AOT | [已有证据](../assessments/2026-10-15/README.md) |
| FND-005 | ✅ | 批次 001 对齐 Run(Form mainForm) 的 void、SetHighDpiMode 的 bool、HighDpiMode 数值；连接真实 DPI 设置/读取及建窗门禁，14 组隔离场景与本机固定基线对照通过；完整消息循环与真机缩放验收仍待后续任务 | [Application](controls/Application.md) |
| FND-006 | 🟡 | 批次 002 完成 Demo 五张 Bitmap 的内建 byte[] 资源/直接解码，三端像素及身份语义对照通过，IL2026 已消除。原 IL3053 展开为 Container.ValidateName 根化 InheritanceAttribute.ToString/Enum.GetValues(Type) 的 IL3050，仍未关闭；Icon/Font/其他 TypeConverter 与完整设计器/主窗另列待办，不用全局 suppress 代替处理 | [跨平台资源](platforms.md)、[批次 002](batches/002.md) |

## P1：所有控件共享的工作

| ID | 状态 | 任务细节与验收 |
| --- | --- | --- |
| FND-010 | ⬜ | ControlCollection：Add/AddRange/Insert/Remove/Clear/SetChildIndex/Find/索引器；ParentChanged、ControlAdded/Removed、重复添加、同名查找、循环父子、重设父级、所有者和销毁后的行为 |
| FND-011 | ⬜ | Handle/CreateControl/CreateHandle/RecreateHandle/DestroyHandle：延迟创建、原生映射、HandleCreated/Destroyed 顺序、重建时恢复状态、窗口过程回调寿命；释放后不保留 GCHandle |
| FND-012 | 🟡 | 批次 002 完成 Component 释放契约、Site/Container、组件容器级联、重复/重入与异常回归（Control-08a）；UI 父子级联、事件解绑及图像/字体/菜单所有权仍待实现，父任务不关闭 |
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
