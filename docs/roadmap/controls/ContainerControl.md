# ContainerControl

🟡 **当前状态：**018～034范围保留；035补06d默认按钮活动链更新和必要验证消费。程序选择取消、重入/异常与批量后代顺序已有证据；完整原生点击提交、真实键鼠、嵌套缩放/BindingContext/键盘路由、全部派生和平台仍未完成，见[035](../batches/035.md)。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[ContainerControl API](../api/ContainerControl.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：[ContainerControl.cs](../../../Lumina.Forms/ContainerControl.cs)；历史ContainerControlBase仍保留给既有控件，不作为标准类型替身。
- 前置与排序：Control、ScrollableControl、UI 线程/焦点；Form、UserControl、SplitContainer 的前置。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| ✅ | P1 / ContainerControl-06d | 批次035：活动控件变化经UpdateFocusedControl成功结算后通知所属Form.UpdateDefaultButton，补标准protected virtual扩展点；基类无默认按钮动作与固定参考一致。ValidateInternal供Control内部按钮入口消费，保留Prevent/Allow/Disable结果；嵌套活动按钮与验证策略回归通过。完整原生点击/BindingContext/输入仍待。 |
| ✅ | P1 / ContainerControl-06c | 批次034：ProcessTabKey不在当前容器回绕，Tab/Shift+Tab边界及无候选时回退父链；Ctrl/Alt修饰Tab不走普通导航。9项新增回归覆盖正反边界/嵌套/空容器和普通助记后代终止；完整验证取消/默认按钮/设备仍归父项。 |
| ✅ | P0 / ContainerControl-01 | 018已建立ContainerControl : ScrollableControl : Control，共用Control父子集合/状态；Form接入标准链，原Form缩放移到该层。历史公共名称保留，完整容器行为另列。 |
| 🟡 | P0 / ContainerControl-02 | 028的02a逻辑链/原生恢复保留；029完成02b移除候选和Control-12c禁用/隐藏导航、03a程序验证取消协作。全部派生、ToolStripControlHost、完整原生输入与关闭焦点差异仍缺，不关闭整个活动导航任务。 |
| ✅ | P0 / ContainerControl-02a | 批次028：设置ContainerControl样式，独立逻辑focusedControl；公开ActiveControl与显式ActivateControl按参考分别维护父/深层活动链，Enter/Leave按路径和重入更新，异常经ThreadException保留；定向Select选择首/尾子项，WM_SETFOCUS恢复活动子项并避免多发容器GotFocus。无HWND选择及真实HWND程序焦点对照完成限定范围，验证取消与自动候选仍归02/03。 |
| ✅ | P0 / ContainerControl-02b | 批次029：移除后通过SelectNextControl从剩余首项按TabIndex/CanSelect/TabStop/nested/wrap选取；清理本容器及祖先待验证链，恢复可见树句柄和原生活动焦点。普通/嵌套移除及旧父子集合回归覆盖有限范围，MDI/全部派生另列。 |
| 🟡 | P0 / ContainerControl-03 | 029完成03a基础验证状态机及批量遍历；CausesValidation、取消/重入/异常和两种enable策略有对照。原生点击提交抑制、全部派生默认/标准继承、Form自身声明元数据及完整输入协作仍缺，不以程序探针关闭全验证任务。 |
| ✅ | P0 / ContainerControl-03a | 批次029：标准AutoValidate枚举/继承/AmbientValue/Changed、Validate两重载和ValidateChildren两重载实现。保存最深待验证路径，在Enter前验证至共同祖先；Prevent恢复原活动链，Allow仍如实返回验证失败；Disable/显式验证/重入finally/原异常保留。批量默认Selectable按style筛选、后代先行、取消不中断兄弟；普通容器style及TabControl递归消费完成限定范围。 |
| 🟡 | P1 / ContainerControl-04 | 028程序Select/Focus/原生焦点顺序保留；029补Validating/Validated、取消后活动链与原生焦点、重入/异常对照。物理鼠标/Tab、输入提交及完整派生仍待实现/验收；关闭多余LostFocus单列，不能归一化。 |
| 🟡 | P1 / ContainerControl-05 | 018已迁入既有AutoScaleMode/Dimensions/CurrentDimensions/PerformAutoScale，031补05a受保护AutoScaleFactor。完整嵌套继承/字体/DPI切换和重复缩放仍缺，不以迁移旧算法或比例属性宣称完整兼容。 |
| ✅ | P1 / ContainerControl-05a | 批次031：受保护 `AutoScaleFactor` 按当前/设计尺寸返回双轴比例，设计基线或单轴缺失时返回中性1.0；不关闭完整字体/DPI/嵌套缩放。 |
| 🟡 | P1 / ContainerControl-06 | 031～034的06a～06c保留；035完成06d活动链默认按钮更新与Form Enter/Escape必要验证消费。BindingContext、完整助记键/原生输入和导航验证组合仍缺。 |
| ✅ | P1 / ContainerControl-06a | 批次031：标准 `ParentForm` 按当前父级链动态解析，重新挂载或脱离后立即反映宿主；不冒称BindingContext或键盘路由已完成。 |
| ✅ | P1 / ContainerControl-06b | 批次033：`ProcessDialogKey` 将 Tab/Shift+Tab 交给 `ProcessTabKey`，按活动控件路径执行标准选择；`ProcessDialogChar`/`ProcessMnemonic` 遍历可见启用后代，首个处理成功项终止并按父链回退。完整验证取消/点击提交、BindingContext、IsDialogMessage、Enter/Escape、全部派生和真实设备仍待。 |
| 🟡 | P1 / ContainerControl-07 | 029指定禁用/隐藏、移除/销毁/嵌套候选和异常验证已有程序证据；031已完成UserControl标准继承，035补默认按钮活动链消费，纠正原继承仍缺的滞后描述。完整UserControl组合、混用缩放、全部派生与设备行为仍未完成。 |
| 🟡 | P2 / ContainerControl-08 | 018～028的原生容器入口保留；029接入自动导航和程序验证。IsDialogMessage/键盘、点击取消消费、完整原生焦点生命周期及关闭多余LostFocus仍待实现。 |
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

- 2026-11-07 · 批次035：06d按固定10.0.11调用点通知Form默认按钮，基类空扩展点是标准派生契约，不代表验证成功；实际按钮激活仍消费真实验证结果。嵌套活动链/三种验证策略和队列Enter/Escape有回归，完整容器与点击输入不关闭，见[035](../batches/035.md)。

- 2026-11-07 · 批次034：完成06c嵌套Tab不回绕、修饰键过滤及失败父链回退；纠正06父项遗漏033已完成子范围的描述。普通后代助记键终止依赖Control-12g默认false修复；完整输入/验证/平台仍待，见[034](../batches/034.md)。

- 2026-10-26 · 批次031：补05a AutoScaleFactor和06a ParentForm的明确代码子范围；完整嵌套缩放、绑定、键盘和平台仍待，见[031](../batches/031.md)。

- 2026-10-17 · 批次029：完成02b/03a有限子范围，将03/07由全待办改部分；移除后恢复可见树创建以维持旧父子集合行为。验证状态机按固定10.0.11源码适配，未将有限验证等同全容器或全部派生兼容，见[029](../batches/029.md)。

- 2026-10-16 · 批次028：关闭02a限定逻辑链与程序焦点恢复子范围，04由全待办改部分，08补实际路由证据。公开/接口差异、嵌套重入、错误报告和TextBox去重有26项消费回归；未改AutoScale算法，自动候选、验证/Tab及完整容器仍待办，见[028](../batches/028.md)。

- 2026-10-16 · 批次021：补GetPreferredSizeCore经真实LayoutEngine测量内容、边框/Padding和必要缓存，属于Control-09d的容器依赖；未改现有AutoScale算法，未以基础测量关闭完整容器/验证。

- 2026-10-16 · 批次020：随ScrollableControl移除重复布局状态，Form继承统一标准Layout；未修改现有AutoScale算法，也未完成验证/滚动/完整布局。01既有继承完成范围保留，其他父项不随基类完成关闭。

- 2026-10-16 · 批次018：新增标准类与IContainerControl，迁入Form缩放；按固定参考区分公开setter与显式接口、嵌套选择与释放。将历史“同名类不存在”和“Form独立IDisposable”更新为当前事实；02/05/08保持部分，其余验证/键盘/平台未关闭，见[018](../batches/018.md)。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms ContainerControl 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.containercontrol?view=windowsdesktop-10.0)
- [GTK4 EventControllerFocus](https://docs.gtk.org/gtk4/class.EventControllerFocus.html)
- [AppKit nsresponder](https://developer.apple.com/documentation/appkit/nsresponder)
