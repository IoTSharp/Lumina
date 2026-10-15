# ContainerControl

⬜ **当前状态：**尚无标准 ContainerControl；现有 ContainerControlBase 负责子集合与消息转发，但不是 WinForms ContainerControl 的兼容替身。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[ContainerControl API](../api/ContainerControl.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：同名标准类型尚未实现；不可用历史内部容器名称抵扣此类型。
- 前置与排序：Control、ScrollableControl、UI 线程/焦点；Form、UserControl、SplitContainer 的前置。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| ⬜ | P0 / ContainerControl-01 | 建立 ContainerControl : ScrollableControl : Control 标准继承关系；将 ContainerControlBase 的可复用内部实现逐步迁入，不继续扩大历史公共基类依赖。 |
| ⬜ | P0 / ContainerControl-02 | 实现 ActiveControl、IContainerControl.ActivateControl、SelectNextControl 与嵌套活动链；显式接口不擅自变为新 public 方法，焦点进入深层子控件时每级容器同步 ActiveControl。 |
| ⬜ | P0 / ContainerControl-03 | 实现 AutoValidate、Validate、ValidateChildren(ValidationConstraints)；CausesValidation=false、CancelEventArgs.Cancel 与 AutoValidate 两种 enable 策略均对照。 |
| ⬜ | P1 / ContainerControl-04 | 记录 Enter/Leave/GotFocus/LostFocus/Validating/Validated 在鼠标与 Tab 路径的真实顺序；取消验证后焦点与 ActiveControl 保持一致。 |
| ⬜ | P1 / ContainerControl-05 | 实现 AutoScaleMode、AutoScaleDimensions、CurrentAutoScaleDimensions、AutoScaleFactor、PerformAutoScale；字体/显示器 DPI 切换采用增量缩放而非累计重复缩放。 |
| ⬜ | P1 / ContainerControl-06 | 补 ParentForm、BindingContext 传播、ProcessDialogKey/ProcessMnemonic 等派生扩展点；Enter/Escape、助记键和容器内部导航保持标准语义。 |
| ⬜ | P1 / ContainerControl-07 | 验证自定义 UserControl 继承、嵌套 AutoScaleMode 混用、禁用/隐藏当前活动控件、移除/销毁活动控件及异常验证处理。 |
| ⬜ | P2 / ContainerControl-08 | Windows 用 WS_EX_CONTROLPARENT 与 UI 线程消息循环，焦点通知接入托管活动链；Form 的独立 IDisposable 类型必须先修正才可验收。 |
| ⬜ | P2 / ContainerControl-09 | Linux 用 GtkFixed/GtkBox 原生容器与 GtkEventControllerFocus；兼容层决定验证是否允许转移，再映射 GtkWidget focus；Gtk 自身 focus chain 不等于 ActiveControl。 |
| ⬜ | P3 / ContainerControl-10 | macOS 用 NSView 与 NSWindow firstResponder/key-view loop；在 responder 切换提交前处理可取消验证，原生控件编辑结束与 WinForms 验证事件去重。 |
| ⬜ | P1 / ContainerControl-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / ContainerControl-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI 写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / ContainerControl-V3 | 发布 win-x64 Native AOT，检查实际窗口与本控件交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | 不是单一 Win32 控件；由真实宿主 HWND、WS_EX_CONTROLPARENT、IsDialogMessage/焦点处理组成。 |
| Linux | 原生容器 widget + focus controller，托管实现验证和缩放契约。GTK 样式缩放和 WinForms AutoScale 的分工必须固定。 |
| macOS | NSView + firstResponder/nextKeyView 链；AppKit responder 体系不能替代 WinForms 验证与活动容器状态。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- 无同名类型的实现修改历史；不得将规划文档创建计入运行库实现完成。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms ContainerControl 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.containercontrol?view=windowsdesktop-10.0)
- [GTK4 EventControllerFocus](https://docs.gtk.org/gtk4/class.EventControllerFocus.html)
- [AppKit nsresponder](https://developer.apple.com/documentation/appkit/nsresponder)
