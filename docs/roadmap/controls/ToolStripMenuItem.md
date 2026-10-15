# ToolStripMenuItem

基线：2026-10-15 · .NET 10.0.11。目标继承：`ToolStripMenuItem : ToolStripDropDownItem`。本页为逐类型实施与验收任务，没有把规划当作已完成支持。

## 当前证据与依赖

🟡 [ToolStripMenuItem.cs](../../../Lumina.Forms/ToolStripMenuItem.cs)已有ShortcutKeys/ShortcutKeyDisplayString/ShowShortcutKeys、Checked/CheckOnClick并在OnClick切换；[NativeMenu.cs](../../../Lumina.Forms/NativeMenu.cs)已映射勾选/原生HMENU，[ToolStripPopupMenu.cs](../../../Lumina.Forms/ToolStripPopupMenu.cs)接TrackPopupMenu结果。缺CheckState及CheckedChanged/CheckStateChanged；RadioCheck是当前扩展，不是.NET10标准ToolStripMenuItem成员。

源码定位：[Lumina.Forms/ToolStripMenuItem.cs](../../../Lumina.Forms/ToolStripMenuItem.cs)；条目显示/更新必须同时检查 [ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs)、[StatusStrip.cs](../../../Lumina.Forms/StatusStrip.cs)、[NativeMenu.cs](../../../Lumina.Forms/NativeMenu.cs)、[ToolStripPopupMenu.cs](../../../Lumina.Forms/ToolStripPopupMenu.cs)，不能只凭派生类型文件的长短判断完整度。先决依赖：ToolStripDropDownItem、ToolStripItem命令/绑定、Form快捷键路由、NativeMenu与菜单资源。

完整属性、事件、方法/重载（含继承）见 [ToolStripMenuItem API 清单](../api/ToolStripMenuItem.md)，共同契约见 [基础任务](../foundations.md)。条目是 WinForms 已有公开概念，原生控件组合留在库内部；本页不要求新增公开UI概念、平台项目或新依赖。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 是继承/阻塞，P1 是主要契约，P2 是边界/高级能力，P3 是平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| ToolStripMenuItem-001 | P0 | 🟡 | 保留正确ToolStripMenuItem : ToolStripDropDownItem，标准构造重载、Enabled覆盖；现有RadioCheck单列扩展不计覆盖，不把经典MenuItem API误当此类基线。 |
| ToolStripMenuItem-002 | P1 | ⬜ | CheckState=Unchecked、Checked=false、CheckOnClick=false，混合态与布尔映射；CheckedChanged/CheckStateChanged/Click及命令执行先后、同值/禁用/快捷键路径。 |
| ToolStripMenuItem-003 | P1 | 🟡 | ShortcutKeys=None、ShowShortcutKeys=true、ShortcutKeyDisplayString空；输入路由与显示字符串分开，重复快捷键优先级、隐藏/禁用/未打开菜单、Owner变化和取消按基线。 |
| ToolStripMenuItem-004 | P1 | ⬜ | DropDownItems嵌套、HasDropDownItems、DropDownOpening/Opened/Closed/ItemClicked；动态增删、开菜单前更新Enabled/Checked和递归子菜单。 |
| ToolStripMenuItem-005 | P1 | ⬜ | Text助记键&/&&、大小写/重复助记键、Alt菜单模式、方向导航/Enter/Escape/鼠标悬停与触发；只执行一次且返回正确原始对象。 |
| ToolStripMenuItem-006 | P1 | ⬜ | Image/DisplayStyle/Font/ForeColor、可见/可用/勾选/快捷显示改变后立即更新菜单；owner-draw资源与原生MFS状态一致。 |
| ToolStripMenuItem-007 | P2 | ⬜ | MergeAction/MergeIndex与MDI菜单合并、IsMdiWindowListEntry和MdiWindowListItem协作；Windows专属MDI窗口句柄语义与跨平台文档式窗口列表分别列需求。 |
| ToolStripMenuItem-008 | P2 | ⬜ | Overflow默认、Margin/Padding/Size/DPI/RTL、Submenu工作区翻转和多显示器，菜单打开中Owner关闭/Dispose安全退出并解除hook。 |
| ToolStripMenuItem-009 | P2 | ⬜ | Command/绑定、辅助技术menuitem/check状态/快捷键/子菜单展开，组件容器释放后无残留命令ID、HMENU/图像/brush/callback。 |
| ToolStripMenuItem-010 | P3 | ⬜ | Designer嵌套静态菜单/.resx、AOT快捷键/原生菜单/owner-draw回调，三平台混合态和事件顺序各自验收。 |
| ToolStripMenuItem-CLOSE | P3 | ⬜ | 按完整API清单逐个关闭剩余成员；每个访问器/重载、默认值、异常、通知次数/顺序、取消/重入、未挂载/overflow/移除/Dispose分别验证。Windows公开属性、公开事件、行为各≥98%；Linux/macOS可移植三项各≥98%，不可移植明细保留且需证据，未验收不计通过。 |

## 三平台的原生实现评估

**Windows**：HMENU + MENUITEMINFO、MFS_CHECKED/DISABLED、快捷键路由与TrackPopupMenu；当前弹出菜单含MFT_OWNERDRAW/NativeMenuRenderer，应准确称原生菜单承载而非全部系统绘制。Windows MDI窗口列表与自动合并为额外标准契约。

**Linux / GTK4**：GMenuModel/GMenuItem+GAction与GtkPopoverMenu，stateful action表示勾选，shortcut controller/accelerator路由；Indeterminate、图像/快捷显示文字、动态开菜单期间变化逐项验证，不使用GTK4已移除GtkMenuItem。

**macOS / AppKit**：NSMenuItem/NSMenu，state on/off/mixed、enabled、image、keyEquivalent/modifierMask、target/action；默认自动enable行为需协调，防止覆盖托管Enabled。Ctrl字面保持源API，是否允许平台Command映射需明示策略而非偷偷替换。

实施顺序是 Windows 标准API/状态/事件→该条目最小原生试验→平台映射与完整验收。三平台分别记录属性/事件/行为的分子分母；继承API不能漏记，签名命中、自动属性、控件可以显示或本页任务已写完都不算通过。原生系统绘制、原生容器承载库绘制、原生控件组合应准确区分；公共业务行为难映射时仍然是待实现需求，不自动从可移植分母剔除。

各平台最少覆盖：代码与用户动作双向同步，Owner/Parent/overflow关系，事件顺序/取消/重入，键盘/助记键/输入法（适用时），DPI/RTL/本地化，无障碍，静态Designer和资源，AOT运行，移除与Dispose。没有完整证据时保持🟡/⬜，不发布已达标百分比。

## 修改历史

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 2026-03-31 · `72fe8f6` · Refactor ToolStrip and Menu handling。
- 2026-03-31 · `37a8795` · feat: 添加菜单项单选框支持，增强菜单交互体验。
- 2026-03-31 · `0087cef` · feat: 添加菜单快捷键支持，增强用户交互体验。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

## 官方依据

- [WinForms ToolStripMenuItem API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripmenuitem?view=windowsdesktop-10.0)，精确签名以本机10.0.11引用程序集生成的清单为准。
- [ToolStrip 控件概述](https://learn.microsoft.com/dotnet/desktop/winforms/controls/toolstrip-control-overview)。
- [Windows 原生参考](https://learn.microsoft.com/windows/win32/menurc/menus)。
- [GTK4 PopoverMenu](https://docs.gtk.org/gtk4/class.PopoverMenu.html)。
- [AppKit nsmenuitem](https://developer.apple.com/documentation/appkit/nsmenuitem)。
