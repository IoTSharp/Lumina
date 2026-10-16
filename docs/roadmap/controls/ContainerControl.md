# ContainerControl

🟡 **当前状态：**018标准ContainerControl : ScrollableControl、活动链及原Form缩放首范围保留；020共用标准事务，021新增布局驱动的首选测量/缓存和边框/Padding消费，Form继承使用。完整焦点/验证/缩放/滚动及全部派生测量与平台未完成。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[ContainerControl API](../api/ContainerControl.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：[ContainerControl.cs](../../../Lumina.Forms/ContainerControl.cs)；历史ContainerControlBase仍保留给既有控件，不作为标准类型替身。
- 前置与排序：Control、ScrollableControl、UI 线程/焦点；Form、UserControl、SplitContainer 的前置。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| ✅ | P0 / ContainerControl-01 | 018已建立ContainerControl : ScrollableControl : Control，共用Control父子集合/状态；Form接入标准链，原Form缩放移到该层。历史公共名称保留，完整容器行为另列。 |
| 🟡 | P0 / ContainerControl-02 | 018补ActiveControl与显式IContainerControl.ActivateControl的不同验证/嵌套活动链、移除候选和释放清理；固定参考覆盖首范围。SelectNextControl、完整焦点/验证与键盘事件仍缺，不能关闭整个活动导航任务。 |
| ⬜ | P0 / ContainerControl-03 | 实现 AutoValidate、Validate、ValidateChildren(ValidationConstraints)；CausesValidation=false、CancelEventArgs.Cancel 与 AutoValidate 两种 enable 策略均对照。 |
| ⬜ | P1 / ContainerControl-04 | 记录 Enter/Leave/GotFocus/LostFocus/Validating/Validated 在鼠标与 Tab 路径的真实顺序；取消验证后焦点与 ActiveControl 保持一致。 |
| 🟡 | P1 / ContainerControl-05 | 018已迁入既有AutoScaleMode/Dimensions/CurrentDimensions/PerformAutoScale，保留实际递归缩放及既有测试。AutoScaleFactor、完整嵌套继承/字体/DPI切换和重复缩放仍缺，不以迁移旧算法宣称完整兼容。 |
| ⬜ | P1 / ContainerControl-06 | 补 ParentForm、BindingContext 传播、ProcessDialogKey/ProcessMnemonic 等派生扩展点；Enter/Escape、助记键和容器内部导航保持标准语义。 |
| ⬜ | P1 / ContainerControl-07 | 验证自定义 UserControl 继承、嵌套 AutoScaleMode 混用、禁用/隐藏当前活动控件、移除/销毁活动控件及异常验证处理。 |
| 🟡 | P2 / ContainerControl-08 | 018已有WS_EX_CONTROLPARENT与焦点活动链连接，Form独立IDisposable问题已由标准继承修正。完整IsDialogMessage/键盘导航/验证与原生焦点生命周期仍待实现。 |
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

- 2026-10-16 · 批次021：补GetPreferredSizeCore经真实LayoutEngine测量内容、边框/Padding和必要缓存，属于Control-09d的容器依赖；未改现有AutoScale算法，未以基础测量关闭完整容器/验证。

- 2026-10-16 · 批次020：随ScrollableControl移除重复布局状态，Form继承统一标准Layout；未修改现有AutoScale算法，也未完成验证/滚动/完整布局。01既有继承完成范围保留，其他父项不随基类完成关闭。

- 2026-10-16 · 批次018：新增标准类与IContainerControl，迁入Form缩放；按固定参考区分公开setter与显式接口、嵌套选择与释放。将历史“同名类不存在”和“Form独立IDisposable”更新为当前事实；02/05/08保持部分，其余验证/键盘/平台未关闭，见[018](../batches/018.md)。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms ContainerControl 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.containercontrol?view=windowsdesktop-10.0)
- [GTK4 EventControllerFocus](https://docs.gtk.org/gtk4/class.EventControllerFocus.html)
- [AppKit nsresponder](https://developer.apple.com/documentation/appkit/nsresponder)
