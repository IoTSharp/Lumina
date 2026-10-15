# ToolStripDropDown

⬜ **当前状态：**有 ToolStripPopupMenu/NativeMenu 历史内部实现，但无标准同名公开类型。内部能弹菜单不足以覆盖标准可派生下拉窗口。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[ToolStripDropDown API](../api/ToolStripDropDown.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：同名标准类型尚未实现；内部近似功能不抵扣其公开契约。
- 前置与排序：ToolStrip、UI 消息循环/焦点、ToolStripDropDownItem/OwnerItem。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| ⬜ | P0 / ToolStripDropDown-01 | 实现标准 ToolStripDropDown : ToolStrip 及 OwnerItem/Owner 等准确层次；将内部 popup 能力接入，而非新增第二套公共 Show API。 |
| ⬜ | P0 / ToolStripDropDown-02 | 实现 AutoClose、DropShadowEnabled、AllowTransparency、Opacity、TopLevel 等标准可用表面；默认/范围/句柄前后设值依固定基线逐项记录。 |
| ⬜ | P1 / ToolStripDropDown-03 | 实现全部 Show/Close 重载、ToolStripDropDownDirection、相对/绝对定位；边缘避让、RTL 和多显示器坐标覆盖。 |
| ⬜ | P1 / ToolStripDropDown-04 | 实现 Opening/Opened/Closing/Closed、ToolStripDropDownCloseReason 和 Cancel；关闭取消后仍保持有效焦点/owner，禁止仅发事件但 native 已关闭。 |
| ⬜ | P1 / ToolStripDropDown-05 | 承载任意 ToolStripItem 包括 TextBox/ComboBox/ProgressBar；子菜单与编辑控件输入、Tab/方向键/Esc 的作用域正确。 |
| ⬜ | P1 / ToolStripDropDown-06 | 子菜单级联、点击外部、应用失活、owner 被销毁、再次 Show 等路径事件去重；焦点退出时回到原来源。 |
| ⬜ | P1 / ToolStripDropDown-07 | 允许继承覆盖受保护布局/绘制/消息处理扩展；可访问性弹出层/菜单层级、DPI 和 .resx 图像可验收。 |
| ⬜ | P2 / ToolStripDropDown-08 | Windows 用无任务栏 popup HWND 承载真实子控件；纯菜单子集可以 HMENU，不能以 TrackPopupMenu 代替任意控件宿主。 |
| ⬜ | P2 / ToolStripDropDown-09 | Linux 用 GtkPopover 承载 native widgets；普通菜单用 GtkPopoverMenu。Wayland 下绝对屏幕定位、独立 popup 生命周期与 WinForms TopLevel 有不可等价项。 |
| ⬜ | P3 / ToolStripDropDown-10 | macOS 普通菜单用 NSMenu，含编辑控件/任意 item 的下拉用 native NSPanel/NSPopover 组合；系统弹出层激活/透明度限制需要逐项验证。 |
| ⬜ | P1 / ToolStripDropDown-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / ToolStripDropDown-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI/非视觉组件写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / ToolStripDropDown-V3 | 发布 win-x64 Native AOT，检查实际窗口/原生组件与本类型交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | 标准 popup HWND + 原生子控件；复用已有菜单内部逻辑但补标准类型/事件。 |
| Linux | GtkPopover/PopoverMenu；透明度、阴影和全局坐标受 GTK/compositor 限制，标为 🔎/🚫 而非完成。 |
| macOS | NSPopover/NSPanel 或 NSMenu 子集；NSMenu 的追踪循环与任意互动视图能力不是通用 ToolStripDropDown 等价物。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- 无同名类型的实现修改历史；不得将规划文档创建计入运行库实现完成。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms ToolStripDropDown 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.toolstripdropdown?view=windowsdesktop-10.0)
- [GTK4 Popover](https://docs.gtk.org/gtk4/class.Popover.html)
- [AppKit nspopover](https://developer.apple.com/documentation/appkit/nspopover)
