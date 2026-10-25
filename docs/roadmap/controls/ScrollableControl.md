# ScrollableControl

🟡 **当前状态：**018新增标准ScrollableControl : Control，ContainerControl/Form已接入；029补01b验证递归所需的标准容器style；031完成02a～06a托管首范围；032完成Panel迁移、UserControl消费和08a Windows滚动消息/SCROLLINFO接线。真实物理滚轮/触摸板、Tab/RTL组合、平台滚动和完整API/验收仍未完成，不能把代码子范围计为完整滚动。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[ScrollableControl API](../api/ScrollableControl.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：[ScrollableControl.cs](../../../Lumina.Forms/ScrollableControl.cs)。
- 前置与排序：Control 的句柄、父子、布局与输入；Panel/ContainerControl 的共同前置。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| 🟡 | P0 / ScrollableControl-01 | 018完成01a标准层次与ContainerControl/Form接入；Panel仍用历史基类，完整Control受保护契约也仍待后续，不关闭父项。 |
| ✅ | P0 / ScrollableControl-01a | 018建立标准ScrollableControl : Control，共享父子集合/客户区/释放，ContainerControl/Form复用；020统一事务，021补Visible时请求自身布局并清首选缓存，再调用base通知。仅继承/事务衔接完成，Panel迁移与任何完整滚动行为不在此范围。 |
| ✅ | P0 / ScrollableControl-01b | 批次029：按固定ScrollableControl源码在构造设置ContainerControl style，使批量验证遍历其子孙；无HWND后代先行回归通过。仅该必要style及消费，其他默认style、滚动/全部派生仍归父项。 |
| 🟡 | P0 / ScrollableControl-02 | 018已有无滚动客户区DisplayRectangle；031完成02a的AutoScroll/AutoScrollMargin/MinSize/Position、HorizontalScroll/VerticalScroll和ScrollProperties托管范围模型，真实滚动条与完整状态仍待父项。 |
| ✅ | P0 / ScrollableControl-02a | 批次031：AutoScroll、AutoScrollMargin/MinSize/Position、DisplayRectangle内容范围、H/V ScrollProperties、子控件/尺寸/布局刷新和关闭重置完成；覆盖正值设置、负值读取和范围钳制。真实SCROLLINFO/WM_HSCROLL/WM_VSCROLL仍归02父项。 |
| 🟡 | P0 / ScrollableControl-03 | 031完成03a正设负读和逻辑/显示坐标分离首范围；嵌套、Dock/Anchor、重复滚动、真实消息和全部派生组合仍待基线对照。 |
| ✅ | P0 / ScrollableControl-03a | 批次031：AutoScrollPosition 正值设置/负值读取、DisplayRectangle负偏移、范围钳制与重复刷新首范围完成。 |
| 🟡 | P1 / ScrollableControl-04 | 031完成04a的ScrollControlIntoView、ScrollToControl和SetAutoScrollMargin基础路径；隐藏、负坐标、Dock/Anchor、嵌套控件和完整大内容基线仍待。 |
| ✅ | P1 / ScrollableControl-04a | 批次031：ScrollControlIntoView/ScrollToControl使用真实子树边界和AutoScrollOffset，SetAutoScrollMargin校验负值并刷新范围；复杂布局和真实滚动条仍归父项。 |
| 🟡 | P1 / ScrollableControl-05 | 031完成05a程序设置的ScrollEventType/OldValue/NewValue/ScrollOrientation首范围；鼠标滚轮、触摸板、真实滚动条消息和事件聚合仍待。 |
| ✅ | P1 / ScrollableControl-05a | 批次031：ScrollEventArgs/ScrollEventHandler及双轴程序滚动通知完成，记录旧值/新值和方向；设备消息路径未完成。 |
| 🟡 | P1 / ScrollableControl-06 | 031完成06a OnScroll、AdjustFormScrollbars、SetDisplayRectLocation、状态位和H/V可见状态首范围；完整派生扩展与原生滚动条仍待。 |
| ✅ | P1 / ScrollableControl-06a | 批次031：保护状态常量、Get/SetScrollState、HScroll/VScroll、AdjustFormScrollbars、SetDisplayRectLocation和范围刷新接线完成；不代表完整 ScrollableControl。 |
| ⬜ | P1 / ScrollableControl-07 | 验证 Tab 自动滚动聚焦控件、RTL、水平方向、AutoScroll 关闭重置、尺寸变化滚动条互相挤占和布局重入。 |
| 🟡 | P2 / ScrollableControl-08 | 032完成08a Windows `WM_HSCROLL`/`WM_VSCROLL`、SCROLLINFO、line/page/thumb/first/last/end和句柄重建同步首范围；真实物理滚轮/触摸板、完整消息/设备组合和平台仍待。 |
| ✅ | P2 / ScrollableControl-08a | 批次032：补WS_HSCROLL/WS_VSCROLL、SCROLLINFO、Get/SetScrollInfo、ScrollWindowEx及WndProc命令映射；复用托管范围/Scroll事件，Dispose后忽略注入消息。 |
| ⬜ | P2 / ScrollableControl-09 | Linux 使用 GtkScrolledWindow 的 adjustments 与原生滚动条；将调整值映射负 AutoScrollPosition，拦截平滑滚动时保留 WinForms 事件聚合规则。 |
| ⬜ | P3 / ScrollableControl-10 | macOS 使用 NSScrollView/NSClipView，观察 bounds 变化而非只监听滚轮；弹性滚动临时越界不能直接写回 WinForms 合法滚动值。 |
| ⬜ | P1 / ScrollableControl-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / ScrollableControl-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI 写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / ScrollableControl-V3 | 发布 win-x64 Native AOT，检查实际窗口与本控件交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | 滚动是共享窗口行为：WS_HSCROLL/WS_VSCROLL、SCROLLINFO、ScrollWindowEx/重定位原生子窗口。 |
| Linux | GtkScrolledWindow + GtkAdjustment；像素/惯性滚动与 WinForms 行/页通知需显式转换。 |
| macOS | NSScrollView + NSClipView + NSScroller；overlay 滚动条与传统占空间滚动条是视觉差异，需要分别验证布局。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- 2026-10-26 · 批次032：完成08a Windows滚动消息与SCROLLINFO代码首范围，Panel迁移并消费共享滚动层；真实物理输入、平台后端和完整验收仍待，见[032](../batches/032.md)。

- 2026-10-26 · 批次031：完成02a～06a托管滚动首范围，补正设负读、内容范围、ScrollProperties/事件、受保护状态位和滚动路径；真实Win32滚动条消息、Panel迁移、平台和完整验收仍待，见[031](../batches/031.md)。

- 2026-10-17 · 批次029：完成01b必要验证容器style；标准构造位有固定源码依据，未扩大为完整滚动或默认样式验收，见[029](../batches/029.md)。

- 2026-10-16 · 批次021：按官方OnVisibleChanged补Visible时真正请求本体布局/缓存失效，ContainerControl/Form继承消费。原无滚动DisplayRectangle保留，未实现AutoScroll/ScrollProperties，也未迁移Panel；父项继续部分。

- 2026-10-16 · 批次020：移除第二份暂停计数/延期状态与无条件子树遍历，继承Control标准事务，基础恢复不会强制对子孙调用PerformLayout。既有无滚动DisplayRectangle保留；Panel继承、滚动状态/坐标/平台后端仍缺，不能由本改动关闭滚动父项。

- 2026-10-16 · 批次018：新增标准类支持Form真实继承，共用Control状态并提供客户区DisplayRectangle/布局暂停衔接。纠正历史“同名类不存在”为当前部分状态；Panel、所有滚动和GTK4/AppKit仍缺，未新增整类型验收，见[018](../batches/018.md)。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms ScrollableControl 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.scrollablecontrol?view=windowsdesktop-10.0)
- [GTK4 ScrolledWindow](https://docs.gtk.org/gtk4/class.ScrolledWindow.html)
- [AppKit nsscrollview](https://developer.apple.com/documentation/appkit/nsscrollview)
