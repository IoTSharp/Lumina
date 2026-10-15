# ContextMenuStrip

🟡 **当前状态：**已有 Component + Items、Show(Control, Point) 和原生 popup；标准应派生 ToolStripDropDownMenu，当前类型层次、SourceControl、Opening/Closing 等缺口明显。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[ContextMenuStrip API](../api/ContextMenuStrip.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：[ContextMenuStrip.cs](../../../Lumina.Forms/ContextMenuStrip.cs)
- 前置与排序：ToolStripDropDownMenu、ToolStripItem/Owner、Control.ContextMenuStrip 与键盘命令。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| ⬜ | P0 / ContextMenuStrip-01 | 校正 ContextMenuStrip : ToolStripDropDownMenu : ToolStripDropDown : ToolStrip 的标准层次，保留 IContainer 构造与 Component 生命周期；移除依赖错误继承的内部假设。 |
| ⬜ | P0 / ContextMenuStrip-02 | 补 SourceControl、OwnerItem、ShowCheckMargin/ShowImageMargin、AutoClose 等标准 API；各 Show 重载含 Point、x/y、ToolStripDropDownDirection 和绝对/相对坐标语义。 |
| ⬜ | P1 / ContextMenuStrip-03 | 实现 Opening/Opened/Closing/Closed 的取消、CloseReason；空菜单、Opening 动态填充、Cancel=true 和点击外部关闭都对照 WinForms。 |
| ⬜ | P1 / ContextMenuStrip-04 | 记录 item.Click、strip.ItemClicked、Closing/Closed 顺序；命令禁用/隐藏、分隔项、嵌套子菜单、快捷键与 Checked 状态准确路由。 |
| ⬜ | P1 / ContextMenuStrip-05 | 支持鼠标右键、Shift+F10/Menu 键、NotifyIcon 弹出、无活动 Form owner；关闭后焦点回到正确来源控件。 |
| ⬜ | P1 / ContextMenuStrip-06 | 运行中增删 Items/修改 Text/Image/Enabled、重用同一菜单供多个 Control 时更新 SourceControl；重入 Show 不产生泄漏或重复回调。 |
| ⬜ | P1 / ContextMenuStrip-07 | 验证屏幕边缘、多个 DPI 显示器、RTL、长菜单滚动、可访问性、.resx 图片；Windows Screen 坐标在其他平台的限制逐项记录。 |
| ⬜ | P2 / ContextMenuStrip-08 | Windows 复用现有 NativeMenu/TrackPopupMenuEx，但将 native 生命周期与正确的 ToolStripDropDown 事件层接通，安全处理嵌套消息循环。 |
| ⬜ | P2 / ContextMenuStrip-09 | Linux 用 GtkPopoverMenu/GMenuModel + 相对 widget 的 pointing rectangle；Wayland 不提供全局窗口坐标，Show(screen point) 精确定位不能承诺等价。 |
| ⬜ | P3 / ContextMenuStrip-10 | macOS 用 NSMenu.popUp/contextMenu 对原生 NSView 锚定；NSMenu 项的任意 ToolStrip 控件宿主和非模态交互与 Windows 不同，单列验证或例外。 |
| ⬜ | P1 / ContextMenuStrip-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / ContextMenuStrip-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI/非视觉组件写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / ContextMenuStrip-V3 | 发布 win-x64 Native AOT，检查实际窗口/原生组件与本类型交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | HMENU/TrackPopupMenuEx 已有基础；重点是标准继承、事件/取消、来源与资源归属。 |
| Linux | GtkPopoverMenu 原生菜单；鼠标/键盘弹出使用 native widget 锚点，屏幕绝对坐标受 Wayland 约束。 |
| macOS | NSMenu/NSMenuItem，保留 native command/state。NSMenu 的菜单追踪与托管同步 Show 调用时序需验证。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- `108ef2b`（2026-04-08）：接入 Win32 深色主题。
- `52f5662`（2026-03-31）：加入 ContextMenuStrip 初始能力。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms ContextMenuStrip 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.contextmenustrip?view=windowsdesktop-10.0)
- [GTK4 PopoverMenu](https://docs.gtk.org/gtk4/class.PopoverMenu.html)
- [AppKit nsmenu](https://developer.apple.com/documentation/appkit/nsmenu)
