# ToolStripItem

基线：2026-10-15 · .NET 10.0.11。目标继承：`ToolStripItem : BindableComponent`。本页为逐类型实施与验收任务，没有把规划当作已完成支持。

## 当前证据与依赖

🟡 [ToolStripItem.cs](../../../Lumina.Forms/ToolStripItem.cs) 有Component、Text/Image/Visible/Enabled等字段变更通知和PerformClick；但WinForms基线为abstract ToolStripItem : BindableComponent。实际显示在[ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs)的原生子控件宿主，不是所有条目都是空壳；Owner/Parent、完整事件、布局和绑定契约缺失。

源码定位：[Lumina.Forms/ToolStripItem.cs](../../../Lumina.Forms/ToolStripItem.cs)；条目显示/更新必须同时检查 [ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs)、[StatusStrip.cs](../../../Lumina.Forms/StatusStrip.cs)、[NativeMenu.cs](../../../Lumina.Forms/NativeMenu.cs)、[ToolStripPopupMenu.cs](../../../Lumina.Forms/ToolStripPopupMenu.cs)，不能只凭派生类型文件的长短判断完整度。先决依赖：BindableComponent/BindingContext/DataBindings、ToolStrip布局/焦点/overflow、图像字体与标准关联枚举。

完整属性、事件、方法/重载（含继承）见 [ToolStripItem API 清单](../api/ToolStripItem.md)，共同契约见 [基础任务](../foundations.md)。条目是 WinForms 已有公开概念，原生控件组合留在库内部；本页不要求新增公开UI概念、平台项目或新依赖。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 是继承/阻塞，P1 是主要契约，P2 是边界/高级能力，P3 是平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| ToolStripItem-001 | P0 | ⬜ | 恢复abstract ToolStripItem : BindableComponent，正确Component/Site/IsDisposed与绑定生命周期；所有现有派生类型按标准层级迁移。 |
| ToolStripItem-002 | P1 | ⬜ | Owner、Parent、OwnerItem、IsOnDropDown、IsOnOverflow、Placement、Bounds/ContentRectangle；条目迁移/overflow移动不能变更逻辑Owner，OwnerChanged与LocationChanged按基线。 |
| ToolStripItem-003 | P1 | 🟡 | Text/Enabled/Visible/Name/Tag/Image/Size已有存储；补Available与Visible差异及AvailableChanged/VisibleChanged/EnabledChanged/TextChanged，代码设置和宿主用户变化双向同步。 |
| ToolStripItem-004 | P1 | ⬜ | AutoSize、AutoToolTip、ToolTipText、Margin/Padding、Alignment/Anchor/Dock/Overflow、TextDirection/TextAlign/TextImageRelation、ImageAlign/ImageScaling；每项默认值、测量和重排。 |
| ToolStripItem-005 | P1 | ⬜ | Click/DoubleClick、MouseDown/Up/Move/Enter/Hover/Leave、SelectedChanged；Disabled、未挂载、非可选条目、PerformClick、捕获取消与重复点击事件顺序。 |
| ToolStripItem-006 | P1 | ⬜ | Command/CommandParameter与CommandChanged/CommandParameterChanged/CommandCanExecuteChanged；CanExecute、Enabled、Click和执行顺序、换命令解除旧订阅。 |
| ToolStripItem-007 | P1 | ⬜ | Font/BackColor/ForeColor/RightToLeft继承与对应Changed事件、BackgroundImage/Layout、DisplayStyle/DisplayStyleChanged；原生宿主动态刷新和用户显式值优先。 |
| ToolStripItem-008 | P2 | ⬜ | ImageList索引/键映射（ImageIndex/ImageKey）、透明色、自动镜像、缩放与资源所有权；overflow和菜单复制显示不复制逻辑对象身份。 |
| ToolStripItem-009 | P2 | ⬜ | Select/PerformClick/Invalidate/GetCurrentParent/GetPreferredSize及受保护键盘/助记键/布局扩展点；公开和受保护完整签名逐项按清单关闭。 |
| ToolStripItem-010 | P2 | ⬜ | MergeAction/MergeIndex、拖放DragEnter/Over/Leave/Drop/GiveFeedback/QueryContinueDrag；不因先实现点击而把重排/菜单合并需求删出分母。 |
| ToolStripItem-011 | P2 | ⬜ | AccessibilityObject及AccessibleName/Role/Description/DefaultActionDescription、QueryAccessibilityHelp；焦点/选择/启用状态和宿主角色不重复出现在辅助树。 |
| ToolStripItem-012 | P3 | ⬜ | Designer静态条目/.resx、绑定和Component容器Dispose；移除条目清理原生宿主，AOT事件回调、命令/字体/图片释放与三平台实际运行。 |
| ToolStripItem-CLOSE | P3 | ⬜ | 按完整API清单逐个关闭剩余成员；每个访问器/重载、默认值、异常、通知次数/顺序、取消/重入、未挂载/overflow/移除/Dispose分别验证。Windows公开属性、公开事件、行为各≥98%；Linux/macOS可移植三项各≥98%，不可移植明细保留且需证据，未验收不计通过。 |

## 三平台的原生实现评估

**Windows**：保持当前ToolStrip内原生Button/Label/EDIT/COMBOBOX/Progress等宿主路径，菜单用HMENU/原生弹出。WinForms ToolStripItem是逻辑条目，原生宿主是实现手段，需对齐条目事件/布局而不暴露HWND给消费方；Renderer/Paint兼容与纯原生外观存在边界。

**Linux / GTK4**：GtkBox等容器中使用GtkButton/Label/Entry/DropDown/ProgressBar/MenuButton，菜单用GMenuModel/GAction/GtkPopoverMenu；Owner与overflow位置保留托管关系。GTK4不应误用已移除的GtkToolbar/GtkToolItem API。

**macOS / AppKit**：NSToolbarItem可用于窗口工具栏，view承载NSButton/NSTextField等；窗体内任意ToolStrip/StatusStrip用NSView原生子控件组合。不能把底部StatusStrip误映射为系统菜单栏NSStatusItem；布局和overflow需托管兼容。

实施顺序是 Windows 标准API/状态/事件→该条目最小原生试验→平台映射与完整验收。三平台分别记录属性/事件/行为的分子分母；继承API不能漏记，签名命中、自动属性、控件可以显示或本页任务已写完都不算通过。原生系统绘制、原生容器承载库绘制、原生控件组合应准确区分；公共业务行为难映射时仍然是待实现需求，不自动从可移植分母剔除。

各平台最少覆盖：代码与用户动作双向同步，Owner/Parent/overflow关系，事件顺序/取消/重入，键盘/助记键/输入法（适用时），DPI/RTL/本地化，无障碍，静态Designer和资源，AOT运行，移除与Dispose。没有完整证据时保持🟡/⬜，不发布已达标百分比。

## 修改历史

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 2026-04-09 · `9d5165f` · feat: 添加多个控件和功能，增强用户界面和交互体验。
- 2026-03-31 · `72fe8f6` · Refactor ToolStrip and Menu handling。
- 2026-03-31 · `46a9d63` · feat: 添加菜单助记符支持，增强菜单交互体验。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

## 官方依据

- [WinForms ToolStripItem API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripitem?view=windowsdesktop-10.0)，精确签名以本机10.0.11引用程序集生成的清单为准。
- [ToolStrip 控件概述](https://learn.microsoft.com/dotnet/desktop/winforms/controls/toolstrip-control-overview)。
- [Windows 原生参考](https://learn.microsoft.com/windows/win32/controls/window-controls)。
- [GTK4 Box](https://docs.gtk.org/gtk4/class.Box.html)。
- [AppKit nstoolbaritem](https://developer.apple.com/documentation/appkit/nstoolbaritem)。
