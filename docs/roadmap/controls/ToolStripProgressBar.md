# ToolStripProgressBar

基线：2026-10-15 · .NET 10.0.11。目标继承：`ToolStripProgressBar : ToolStripControlHost`。本页为逐类型实施与验收任务，没有把规划当作已完成支持。

## 当前证据与依赖

🟡 [ToolStripProgressBar.cs](../../../Lumina.Forms/ToolStripProgressBar.cs)仅Value自动属性；[ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs)创建ProgressBar并在布局时赋Value，Value修改本身未NotifyChanged，可能直到后续布局才显示。缺公开ProgressBar、范围/动画和标准继承层级。

源码定位：[Lumina.Forms/ToolStripProgressBar.cs](../../../Lumina.Forms/ToolStripProgressBar.cs)；条目显示/更新必须同时检查 [ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs)、[StatusStrip.cs](../../../Lumina.Forms/StatusStrip.cs)、[NativeMenu.cs](../../../Lumina.Forms/NativeMenu.cs)、[ToolStripPopupMenu.cs](../../../Lumina.Forms/ToolStripPopupMenu.cs)，不能只凭派生类型文件的长短判断完整度。先决依赖：ToolStripControlHost、ProgressBar完整范围/动画、ToolStrip状态更新。

完整属性、事件、方法/重载（含继承）见 [ToolStripProgressBar API 清单](../api/ToolStripProgressBar.md)，共同契约见 [基础任务](../foundations.md)。条目是 WinForms 已有公开概念，原生控件组合留在库内部；本页不要求新增公开UI概念、平台项目或新依赖。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 是继承/阻塞，P1 是主要契约，P2 是边界/高级能力，P3 是平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| ToolStripProgressBar-001 | P0 | ⬜ | 改为ToolStripProgressBar : ToolStripControlHost，公开ProgressBar只读属性恒定，标准构造/Name重载。 |
| ToolStripProgressBar-002 | P1 | 🟡 | Value默认0、Minimum=0、Maximum=100、Step=10与Increment/PerformStep；边界/异常/范围钳制完全透传底层，Value改变立即显示。 |
| ToolStripProgressBar-003 | P1 | ⬜ | Style=Blocks、MarqueeAnimationSpeed默认100、确定/不确定切换、0停动画和运行时更新；隐藏/移除/Dispose停止计时。 |
| ToolStripProgressBar-004 | P1 | ⬜ | RightToLeftLayout/RightToLeftLayoutChanged、Text/TextChanged、OwnerChanged/LocationChanged与继承键盘/验证事件的隐藏元数据和实际触发。 |
| ToolStripProgressBar-005 | P1 | ⬜ | 包装层不新增ProgressBar基线没有的ValueChanged事件；用户经ProgressBar属性设置范围/值后条目属性读回一致。 |
| ToolStripProgressBar-006 | P2 | ⬜ | AutoSize/GetPreferredSize、Size、Margin/Padding、DPI、StatusStrip与ToolStrip的默认高度/对齐、overflow空间分配。 |
| ToolStripProgressBar-007 | P2 | ⬜ | ControlHost Focus/Validation对不可交互进度条的语义、TabStop默认、AccessibleName/role/value/indeterminate状态。 |
| ToolStripProgressBar-008 | P2 | ⬜ | 当前NativeMenu不支持进度条的DropDown路径写成实施任务；原生Popover/NSMenu自定义view可行性有实际样例。 |
| ToolStripProgressBar-009 | P3 | ⬜ | Designer范围/Style初始化、AOT动画与动态更新，三平台实际释放及辅助技术范围一致。 |
| ToolStripProgressBar-CLOSE | P3 | ⬜ | 按完整API清单逐个关闭剩余成员；每个访问器/重载、默认值、异常、通知次数/顺序、取消/重入、未挂载/overflow/移除/Dispose分别验证。Windows公开属性、公开事件、行为各≥98%；Linux/macOS可移植三项各≥98%，不可移植明细保留且需证据，未验收不计通过。 |

## 三平台的原生实现评估

**Windows**：唯一msctls_progress32由ProgressBar属性访问，PBM_*与底层ProgressBar共用；值变化应立即更新而非靠Layout触发，菜单内当前禁用路径需标准ControlHost DropDown支持。

**Linux / GTK4**：GtkProgressBar嵌GtkBox/GtkPopover，progress fraction与pulse共用底层实现，状态栏/工具栏不新增独立动画逻辑；宿主Dispose停止原生loop timer。

**macOS / AppKit**：NSProgressIndicator嵌NSToolbarItem.view或NSView，确定/不确定模式与范围透传，工具栏overflow展示能力以小型原生样例验证。

实施顺序是 Windows 标准API/状态/事件→该条目最小原生试验→平台映射与完整验收。三平台分别记录属性/事件/行为的分子分母；继承API不能漏记，签名命中、自动属性、控件可以显示或本页任务已写完都不算通过。原生系统绘制、原生容器承载库绘制、原生控件组合应准确区分；公共业务行为难映射时仍然是待实现需求，不自动从可移植分母剔除。

各平台最少覆盖：代码与用户动作双向同步，Owner/Parent/overflow关系，事件顺序/取消/重入，键盘/助记键/输入法（适用时），DPI/RTL/本地化，无障碍，静态Designer和资源，AOT运行，移除与Dispose。没有完整证据时保持🟡/⬜，不发布已达标百分比。

## 修改历史

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 2026-03-31 · `52f5662` · Add core UI components for Lumina.Forms。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

## 官方依据

- [WinForms ToolStripProgressBar API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripprogressbar?view=windowsdesktop-10.0)，精确签名以本机10.0.11引用程序集生成的清单为准。
- [ToolStrip 控件概述](https://learn.microsoft.com/dotnet/desktop/winforms/controls/toolstrip-control-overview)。
- [Windows 原生参考](https://learn.microsoft.com/windows/win32/controls/progress-bar-control-reference)。
- [GTK4 ProgressBar](https://docs.gtk.org/gtk4/class.ProgressBar.html)。
- [AppKit nsprogressindicator](https://developer.apple.com/documentation/appkit/nsprogressindicator)。
