# ToolStripDropDownMenu

⬜ **当前状态：**标准同名类型缺失；ContextMenuStrip 与内部 NativeMenu 有菜单能力，但未形成正确的标准继承/布局表面。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[ToolStripDropDownMenu API](../api/ToolStripDropDownMenu.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：同名标准类型尚未实现；内部近似功能不抵扣其公开契约。
- 前置与排序：ToolStripDropDown、ToolStripMenuItem、菜单绘图/键盘。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| ⬜ | P0 / ToolStripDropDownMenu-01 | 实现标准 ToolStripDropDownMenu : ToolStripDropDown，ContextMenuStrip 以其为基类；保持标准构造与 item 所有权。 |
| ⬜ | P1 / ToolStripDropDownMenu-02 | 实现 ShowCheckMargin/ShowImageMargin、MaxItemSize、LayoutStyle 等实际菜单表面，记录默认值与动态切换；无需自创菜单专属公开属性。 |
| ⬜ | P1 / ToolStripDropDownMenu-03 | 菜单列布局分配勾选区、图像区、文字、快捷键、子菜单箭头，动态项目宽度/高度、RTL 和 DPI 统一测量。 |
| ⬜ | P1 / ToolStripDropDownMenu-04 | 实现超过屏幕菜单的滚动、鼠标滚轮、键盘 Home/End/Up/Down/Page 导航；隐藏/禁用/分隔项跳过。 |
| ⬜ | P1 / ToolStripDropDownMenu-05 | 实现级联子菜单打开/关闭、悬停延迟、同级切换、Esc 层级和自动关闭；Opening/Closing Cancel 与 owner.DropDownOpening/Closed 之间顺序精确对照。 |
| ⬜ | P1 / ToolStripDropDownMenu-06 | Checked/CheckState/Image/ShortcutKeyDisplayString 的变化双向同步；同一 item 在菜单与 overflow 间迁移后状态不丢失。 |
| ⬜ | P1 / ToolStripDropDownMenu-07 | 暴露正确可访问性菜单/菜单项状态与快捷键信息，ToolStripTextBox/ComboBox 等混合 item 仍可输入而非只画成菜单文字。 |
| ⬜ | P2 / ToolStripDropDownMenu-08 | Windows 纯菜单用 HMENU 与 owner-draw 测量/绘制扩展，任意托管控件宿主走 popup HWND；共享事件/集合实现。 |
| ⬜ | P2 / ToolStripDropDownMenu-09 | Linux 用 GtkPopoverMenu + GMenuModel/GAction；image/check margin 是平台主题约定，无法精确控制的视觉项单列，功能勾选与快捷键仍必须实现。 |
| ⬜ | P3 / ToolStripDropDownMenu-10 | macOS 用 NSMenu/NSMenuItem；系统控制勾选/图像列与菜单滚动，ShowImageMargin/ShowCheckMargin 的像素布局不可直接等价。 |
| ⬜ | P1 / ToolStripDropDownMenu-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / ToolStripDropDownMenu-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI/非视觉组件写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / ToolStripDropDownMenu-V3 | 发布 win-x64 Native AOT，检查实际窗口/原生组件与本类型交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | HMENU/native popup 组合，共享标准 ToolStripDropDown 生命周期。 |
| Linux | GtkPopoverMenu，混合编辑控件可用 Popover+原生 widgets；同一 logical item 状态保持单一来源。 |
| macOS | NSMenu 原生菜单；自定义视图项只在确有 API 需要且可访问性可验收时采用，不强行统一所有视觉列距。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- 无同名类型的实现修改历史；不得将规划文档创建计入运行库实现完成。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms ToolStripDropDownMenu 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.toolstripdropdownmenu?view=windowsdesktop-10.0)
- [GTK4 PopoverMenu](https://docs.gtk.org/gtk4/class.PopoverMenu.html)
- [AppKit nsmenu](https://developer.apple.com/documentation/appkit/nsmenu)
