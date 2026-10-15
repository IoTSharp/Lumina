# Control

🟡 **现状：**当前是 abstract Control : IDisposable；不具备标准 Component/可实例化 Control 契约。Focus() 返回 void，InvokeRequired 恒 false，Invoke 直接 DynamicInvoke，BeginInvoke 用 Task.Run，InvokeAsync 直接执行；SuspendLayout/基础 PerformLayout 为空。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/Control.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：[Control.cs](../../../Lumina.Forms/Control.cs)
- 排序：全库 P0 根基；先于 ScrollableControl/ContainerControl 和所有具体控件。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P0 / Control-01 | 修为可实例化 Control : Component，并对齐 IWin32Window、ISynchronizeInvoke、IDropTarget 等固定基线的公开接口；构造重载、Controls、派生扩展和强制转换消费源码一起验收。 |
| ⬜ | P0 / Control-02 | 把 Focus() 改为标准 bool，补 Select/CanSelect/CanFocus/Focused/ContainsFocus；默认大小允许零尺寸，不再把 Width/Height 永久强制至少 1。 |
| ⬜ | P0 / Control-03 | InvokeRequired 使用创建线程且沿父句柄寻找有效调度器；无句柄与未创建主循环的结果单独对照，不能由恒 false 掩盖跨线程访问。 |
| ⬜ | P0 / Control-04 | 实现 Invoke/BeginInvoke/EndInvoke 全部 Action/Delegate/args 重载，投递到 UI 线程；返回值、异常、同步等待、同线程调用、队列顺序与关闭期间行为与官方一致。 |
| ⬜ | P0 / Control-05 | 实现四类 InvokeAsync 标准 Action、Func<T>、Func<CancellationToken,ValueTask>、带结果 ValueTask<T> 重载和 CancellationToken；区分排队前取消、执行中取消与抛出异常，不把 Task 回调自创为唯一接口。 |
| ⬜ | P0 / Control-06 | 实现 Control.ControlCollection、Parent 可设、Controls.Add/AddRange/Remove/Clear/SetChildIndex/Find；Form 直接子项的 Parent 必须是 Form，Owner 不能替代 Parent。 |
| ⬜ | P1 / Control-07 | Handle 延迟创建、CreateControl/CreateHandle/RecreateHandle/DestroyHandle、FromHandle/FromChildHandle 与映射表一致；HandleCreated/HandleDestroyed、重建后属性恢复和 GCHandle 寿命逐项测。 |
| ⬜ | P1 / Control-08 | 实现 Dispose(bool)/Disposed、Site/Container、Disposing/IsDisposed；父级级联释放、单项移除、重复 Dispose、借用 Font/Image/菜单资源分别对照，杜绝只释放 HWND 不通知组件。 |
| ⬜ | P1 / Control-09 | 校准 Bounds/ClientSize/ClientRectangle/PreferredSize、MinimumSize/MaximumSize、Dock/Anchor/AutoSize/GetPreferredSize、Padding/Margin、BringToFront/SendToBack 和 z-order。 |
| ⬜ | P1 / Control-10 | 实现嵌套 SuspendLayout 计数、ResumeLayout(false/true)、PerformLayout 重载与 LayoutEventArgs；将 SizeChanged/ClientSizeChanged/Resize/LocationChanged 与 Layout 顺序固定为场景证据。 |
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

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.control?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/class.Widget.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nsview)
