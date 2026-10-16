# ScrollableControl

🟡 **当前状态：**018新增标准ScrollableControl : Control，ContainerControl/Form已接入；实现客户区DisplayRectangle；020移除独立暂停计数，布局由Control同一事务处理。Panel仍未迁入，AutoScroll状态/滚动条/坐标与平台滚动均未实现，不能把基类存在计为可滚动。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[ScrollableControl API](../api/ScrollableControl.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：[ScrollableControl.cs](../../../Lumina.Forms/ScrollableControl.cs)。
- 前置与排序：Control 的句柄、父子、布局与输入；Panel/ContainerControl 的共同前置。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| 🟡 | P0 / ScrollableControl-01 | 018完成01a标准层次与ContainerControl/Form接入；Panel仍用历史基类，完整Control受保护契约也仍待后续，不关闭父项。 |
| ✅ | P0 / ScrollableControl-01a | 018建立标准ScrollableControl : Control，实际共享父子集合/客户区/释放，ContainerControl/Form复用；020去掉该层独立暂停状态及无条件子树布局，接入Control标准事务。仅此继承与事务衔接范围完成，Panel迁移和任何滚动行为不在此范围。 |
| 🟡 | P0 / ScrollableControl-02 | 018已有无滚动客户区DisplayRectangle。AutoScroll、AutoScrollMargin/MinSize/Position、HorizontalScroll/VerticalScroll和ScrollProperties完整状态模型仍待实现。 |
| ⬜ | P0 / ScrollableControl-03 | 明确 AutoScrollPosition 赋正值、读负偏移的 WinForms 约定；布局、滚动和子控件 Bounds 的逻辑/显示坐标分离，并验证重复滚动不累积误差。 |
| ⬜ | P1 / ScrollableControl-04 | 实现 ScrollControlIntoView、ScrollToControl、SetAutoScrollMargin；隐藏、负坐标、Dock、Anchor、嵌套控件和超大内容的可滚动区域按基线计算。 |
| ⬜ | P1 / ScrollableControl-05 | 实现 Scroll 事件的 ScrollEventType、OldValue/NewValue、ScrollOrientation；区分程序赋值、滚动条拖动、鼠标滚轮和触摸板导致的事件。 |
| ⬜ | P1 / ScrollableControl-06 | 实现 OnScroll、AdjustFormScrollbars、SetDisplayRectLocation 等受保护兼容扩展点；不能只让内置 Panel 可用而阻断用户派生控件。 |
| ⬜ | P1 / ScrollableControl-07 | 验证 Tab 自动滚动聚焦控件、RTL、水平方向、AutoScroll 关闭重置、尺寸变化滚动条互相挤占和布局重入。 |
| ⬜ | P2 / ScrollableControl-08 | Windows 统一 SCROLLINFO 与 WM_HSCROLL/WM_VSCROLL，按行/页/thumb 路径转换；Handle 重建恢复位置，避免将 16 位消息 thumb 值当完整范围。 |
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

- 2026-10-16 · 批次020：移除第二份暂停计数/延期状态与无条件子树遍历，继承Control标准事务，基础恢复不会强制对子孙调用PerformLayout。既有无滚动DisplayRectangle保留；Panel继承、滚动状态/坐标/平台后端仍缺，不能由本改动关闭滚动父项。

- 2026-10-16 · 批次018：新增标准类支持Form真实继承，共用Control状态并提供客户区DisplayRectangle/布局暂停衔接。纠正历史“同名类不存在”为当前部分状态；Panel、所有滚动和GTK4/AppKit仍缺，未新增整类型验收，见[018](../batches/018.md)。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms ScrollableControl 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.scrollablecontrol?view=windowsdesktop-10.0)
- [GTK4 ScrolledWindow](https://docs.gtk.org/gtk4/class.ScrolledWindow.html)
- [AppKit nsscrollview](https://developer.apple.com/documentation/appkit/nsscrollview)
