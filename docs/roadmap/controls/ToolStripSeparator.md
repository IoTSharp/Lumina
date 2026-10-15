# ToolStripSeparator

基线：2026-10-15 · .NET 10.0.11。目标继承：`ToolStripSeparator : ToolStripItem`。本页为逐类型实施与验收任务，没有把规划当作已完成支持。

## 当前证据与依赖

🟡 [ToolStripSeparator.cs](../../../Lumina.Forms/ToolStripSeparator.cs)类体为空；[ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs)用Panel宿主，[NativeMenu.cs](../../../Lumina.Forms/NativeMenu.cs)用MFT_SEPARATOR或owner-draw分隔。并非没有显示实现，但不可选/尺寸/隐藏成员元数据未对齐。

源码定位：[Lumina.Forms/ToolStripSeparator.cs](../../../Lumina.Forms/ToolStripSeparator.cs)；条目显示/更新必须同时检查 [ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs)、[StatusStrip.cs](../../../Lumina.Forms/StatusStrip.cs)、[NativeMenu.cs](../../../Lumina.Forms/NativeMenu.cs)、[ToolStripPopupMenu.cs](../../../Lumina.Forms/ToolStripPopupMenu.cs)，不能只凭派生类型文件的长短判断完整度。先决依赖：ToolStripItem的CanSelect/布局、ToolStrip与原生菜单宿主。

完整属性、事件、方法/重载（含继承）见 [ToolStripSeparator API 清单](../api/ToolStripSeparator.md)，共同契约见 [基础任务](../foundations.md)。条目是 WinForms 已有公开概念，原生控件组合留在库内部；本页不要求新增公开UI概念、平台项目或新依赖。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 是继承/阻塞，P1 是主要契约，P2 是边界/高级能力，P3 是平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| ToolStripSeparator-001 | P0 | 🟡 | 保留ToolStripSeparator : ToolStripItem、标准构造；验证当前Panel显示是否真有分隔线，未观察到线条不把存在Panel算支持。 |
| ToolStripSeparator-002 | P1 | ⬜ | CanSelect=false、AutoToolTip默认、Enabled/Text/Image/DisplayStyle等隐藏/重载成员元数据和实际行为；分隔符不能被PerformClick/助记键/方向导航误激活。 |
| ToolStripSeparator-003 | P1 | ⬜ | 横向ToolStrip显示纵线、纵向布局显示横线，DefaultSize/Margin/Padding/AutoSize与DPI；菜单行高度按原生主题。 |
| ToolStripSeparator-004 | P1 | ⬜ | Available/Visible改变立即参与布局和菜单重建；首项/末项/相邻多个分隔符在普通/overflow/dropdown各按WinForms行为。 |
| ToolStripSeparator-005 | P1 | ⬜ | TextChanged/EnabledChanged/DisplayStyleChanged等隐藏事件，赋同值/非法值的行为按公开清单；不新增Separator专用事件。 |
| ToolStripSeparator-006 | P2 | ⬜ | 菜单MFT_SEPARATOR/owner-draw两条路径的不可选、命中、键盘跳过与辅助技术separator角色。 |
| ToolStripSeparator-007 | P2 | ⬜ | Owner迁移、Items.Remove/Clear、句柄重建后旧Panel/HMENU条目清理，布局不残留空白宽度。 |
| ToolStripSeparator-008 | P2 | ⬜ | 主题/高对比度/RTL/显示缩放下原生线条可见，原生平台无法控制的线宽颜色作为差异记录。 |
| ToolStripSeparator-009 | P3 | ⬜ | Designer静态分隔条目、AOT普通/菜单/overflow三种宿主和Dispose实际验收。 |
| ToolStripSeparator-CLOSE | P3 | ⬜ | 按完整API清单逐个关闭剩余成员；每个访问器/重载、默认值、异常、通知次数/顺序、取消/重入、未挂载/overflow/移除/Dispose分别验证。Windows公开属性、公开事件、行为各≥98%；Linux/macOS可移植三项各≥98%，不可移植明细保留且需证据，未验收不计通过。 |

## 三平台的原生实现评估

**Windows**：ToolStrip内原生STATIC分隔线（SS_ETCHEDHORZ/VERT）或现有Panel宿主需先确认视觉，HMENU内用MFT_SEPARATOR；当前owner-draw菜单路径要标为原生菜单承载+库绘制，不称全为系统绘制。

**Linux / GTK4**：GtkSeparator横/纵向原生分隔，GMenuModel按section创建菜单分组；连续/首尾分隔的自动隐藏规则与WinForms对照，不能简单一项换一条线。

**macOS / AppKit**：工具栏用NSToolbarItem分隔/空间能力或NSBox.separator，NSMenuItem.separator菜单分隔；Toolbar系统间距策略与显式Size存在差异需记录。

实施顺序是 Windows 标准API/状态/事件→该条目最小原生试验→平台映射与完整验收。三平台分别记录属性/事件/行为的分子分母；继承API不能漏记，签名命中、自动属性、控件可以显示或本页任务已写完都不算通过。原生系统绘制、原生容器承载库绘制、原生控件组合应准确区分；公共业务行为难映射时仍然是待实现需求，不自动从可移植分母剔除。

各平台最少覆盖：代码与用户动作双向同步，Owner/Parent/overflow关系，事件顺序/取消/重入，键盘/助记键/输入法（适用时），DPI/RTL/本地化，无障碍，静态Designer和资源，AOT运行，移除与Dispose。没有完整证据时保持🟡/⬜，不发布已达标百分比。

## 修改历史

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 2026-03-31 · `52f5662` · Add core UI components for Lumina.Forms。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

## 官方依据

- [WinForms ToolStripSeparator API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripseparator?view=windowsdesktop-10.0)，精确签名以本机10.0.11引用程序集生成的清单为准。
- [ToolStrip 控件概述](https://learn.microsoft.com/dotnet/desktop/winforms/controls/toolstrip-control-overview)。
- [Windows 原生参考](https://learn.microsoft.com/windows/win32/menurc/menus)。
- [GTK4 Separator](https://docs.gtk.org/gtk4/class.Separator.html)。
- [AppKit nsbox](https://developer.apple.com/documentation/appkit/nsbox)。
