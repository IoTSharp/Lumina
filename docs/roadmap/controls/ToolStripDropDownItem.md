# ToolStripDropDownItem

基线：2026-10-15 · .NET 10.0.11。目标继承：`ToolStripDropDownItem : ToolStripItem`。本页为逐类型实施与验收任务，没有把规划当作已完成支持。

## 当前证据与依赖

🟡 [ToolStripDropDownItem.cs](../../../Lumina.Forms/ToolStripDropDownItem.cs)为具体类且只有DropDownItems；WinForms是abstract。现有[ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs)与[ToolStripPopupMenu.cs](../../../Lumina.Forms/ToolStripPopupMenu.cs)已能用原生popup打开并在相邻菜单间导航，但DropDown对象/标准事件面未实现。

源码定位：[Lumina.Forms/ToolStripDropDownItem.cs](../../../Lumina.Forms/ToolStripDropDownItem.cs)；条目显示/更新必须同时检查 [ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs)、[StatusStrip.cs](../../../Lumina.Forms/StatusStrip.cs)、[NativeMenu.cs](../../../Lumina.Forms/NativeMenu.cs)、[ToolStripPopupMenu.cs](../../../Lumina.Forms/ToolStripPopupMenu.cs)，不能只凭派生类型文件的长短判断完整度。先决依赖：ToolStripItem、ToolStripDropDown/DropDownMenu、ToolStripItemCollection、菜单生命周期。

完整属性、事件、方法/重载（含继承）见 [ToolStripDropDownItem API 清单](../api/ToolStripDropDownItem.md)，共同契约见 [基础任务](../foundations.md)。条目是 WinForms 已有公开概念，原生控件组合留在库内部；本页不要求新增公开UI概念、平台项目或新依赖。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 是继承/阻塞，P1 是主要契约，P2 是边界/高级能力，P3 是平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| ToolStripDropDownItem-001 | P0 | ⬜ | 恢复abstract ToolStripDropDownItem : ToolStripItem；补DropDown、HasDropDown、HasDropDownItems、DropDownItems、OwnerItem和CreateDefaultDropDown标准扩展点。 |
| ToolStripDropDownItem-002 | P1 | 🟡 | ShowDropDown/HideDropDown、DropDownDirection/DropDownLocation、Pressed；代码打开/点击打开/无项/禁用/Owner未建句柄按基线，创建默认DropDown惰性行为。 |
| ToolStripDropDownItem-003 | P1 | ⬜ | DropDownOpening、DropDownOpened、DropDownClosed、DropDownItemClicked；注意DropDownOpening本身是EventHandler，取消在DropDown.Opening/Closing的CancelEventArgs契约实现，不能发明可取消参数。 |
| ToolStripDropDownItem-004 | P1 | ⬜ | DropDownItems动态增删/移Owner/嵌套层级与DropDown属性替换；显式共享DropDown时OwnerItem/事件订阅/释放的基线行为。 |
| ToolStripDropDownItem-005 | P1 | ⬜ | Esc/Alt/方向/Enter、鼠标离开/外部点击/相邻菜单切换；焦点恢复、关闭原因、Click与关闭顺序及取消关闭分开记录。 |
| ToolStripDropDownItem-006 | P1 | ⬜ | DropDownDirection八方向/Default、RTL、工作区边缘、多屏DPI与超长子菜单，原生平台无全局坐标能力列差异但保证可见定位。 |
| ToolStripDropDownItem-007 | P2 | ⬜ | 开放期间修改Text/Enabled/Items/Checked、重入Show/Hide/Dispose、菜单hook清理与标准Closing取消，不能因TrackPopupMenu阻塞忽略生命周期。 |
| ToolStripDropDownItem-008 | P2 | ⬜ | ToolStripControlHost在DropDown内可编辑，当前NativeMenu禁用TextBox/ComboBox/ProgressBar路径必须补原生宿主方案。 |
| ToolStripDropDownItem-009 | P2 | ⬜ | 辅助技术haspopup/expanded、OwnerItem关系、图像/菜单/native引用所有权与Dispose；DropDown替换后旧事件不得继续回调。 |
| ToolStripDropDownItem-010 | P3 | ⬜ | Designer DropDownItems静态序列化、共享DropDown行为和AOT菜单回调，三平台原生菜单/Popover两类内容各验收。 |
| ToolStripDropDownItem-CLOSE | P3 | ⬜ | 按完整API清单逐个关闭剩余成员；每个访问器/重载、默认值、异常、通知次数/顺序、取消/重入、未挂载/overflow/移除/Dispose分别验证。Windows公开属性、公开事件、行为各≥98%；Linux/macOS可移植三项各≥98%，不可移植明细保留且需证据，未验收不计通过。 |

## 三平台的原生实现评估

**Windows**：HMENU/TrackPopupMenu或标准DropDown原生窗口容器；嵌入ControlHost时HMENU不够，需要原生子控件表面。WM_INITMENUPOPUP/UNINITMENUPOPUP/MENUSELECT与托管Opening/Closing事件对齐；现有消息hook保留在库内。

**Linux / GTK4**：GtkMenuButton+GtkPopoverMenu/GMenuModel，需任意ControlHost时GtkPopover原生子控件；popover的显示/隐藏与DropDown对象身份、OwnerItem、取消契约同步。Wayland下绝对屏幕位置能力不能照抄Win32。

**macOS / AppKit**：NSMenu/NSPopUpButton或原生NSPopover承载控件，菜单delegate和target/action转发；菜单追踪期间的重新打开/取消和关闭窗体按托管事件协调。NSMenu不是公开ToolStripDropDown的替代类型。

实施顺序是 Windows 标准API/状态/事件→该条目最小原生试验→平台映射与完整验收。三平台分别记录属性/事件/行为的分子分母；继承API不能漏记，签名命中、自动属性、控件可以显示或本页任务已写完都不算通过。原生系统绘制、原生容器承载库绘制、原生控件组合应准确区分；公共业务行为难映射时仍然是待实现需求，不自动从可移植分母剔除。

各平台最少覆盖：代码与用户动作双向同步，Owner/Parent/overflow关系，事件顺序/取消/重入，键盘/助记键/输入法（适用时），DPI/RTL/本地化，无障碍，静态Designer和资源，AOT运行，移除与Dispose。没有完整证据时保持🟡/⬜，不发布已达标百分比。

## 修改历史

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 2026-03-31 · `72fe8f6` · Refactor ToolStrip and Menu handling。
- 2026-03-31 · `52f5662` · Add core UI components for Lumina.Forms。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

## 官方依据

- [WinForms ToolStripDropDownItem API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripdropdownitem?view=windowsdesktop-10.0)，精确签名以本机10.0.11引用程序集生成的清单为准。
- [ToolStrip 控件概述](https://learn.microsoft.com/dotnet/desktop/winforms/controls/toolstrip-control-overview)。
- [Windows 原生参考](https://learn.microsoft.com/windows/win32/menurc/menus)。
- [GTK4 MenuButton](https://docs.gtk.org/gtk4/class.MenuButton.html)。
- [AppKit nsmenu](https://developer.apple.com/documentation/appkit/nsmenu)。
