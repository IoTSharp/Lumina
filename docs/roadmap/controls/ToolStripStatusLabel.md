# ToolStripStatusLabel

基线：2026-10-15 · .NET 10.0.11。目标继承：`ToolStripStatusLabel : ToolStripLabel`。本页为逐类型实施与验收任务，没有把规划当作已完成支持。

## 当前证据与依赖

🟡 [ToolStripStatusLabel.cs](../../../Lumina.Forms/ToolStripStatusLabel.cs)直接继承ToolStripItem且Spring为自动属性；[StatusStrip.cs](../../../Lumina.Forms/StatusStrip.cs)确实计算Spring剩余空间并布局Label宿主。基线应继承ToolStripLabel，缺BorderSides/BorderStyle/LiveSetting；Spring修改没有Changed通知。

源码定位：[Lumina.Forms/ToolStripStatusLabel.cs](../../../Lumina.Forms/ToolStripStatusLabel.cs)；条目显示/更新必须同时检查 [ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs)、[StatusStrip.cs](../../../Lumina.Forms/StatusStrip.cs)、[NativeMenu.cs](../../../Lumina.Forms/NativeMenu.cs)、[ToolStripPopupMenu.cs](../../../Lumina.Forms/ToolStripPopupMenu.cs)，不能只凭派生类型文件的长短判断完整度。先决依赖：ToolStripLabel、StatusStrip剩余空间布局、ToolStripItem图文/可访问性。

完整属性、事件、方法/重载（含继承）见 [ToolStripStatusLabel API 清单](../api/ToolStripStatusLabel.md)，共同契约见 [基础任务](../foundations.md)。条目是 WinForms 已有公开概念，原生控件组合留在库内部；本页不要求新增公开UI概念、平台项目或新依赖。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 是继承/阻塞，P1 是主要契约，P2 是边界/高级能力，P3 是平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| ToolStripStatusLabel-001 | P0 | ⬜ | 恢复ToolStripStatusLabel : ToolStripLabel；标准构造重载和继承IsLink/LinkVisited/颜色/CanSelect全部可编译、可运行。 |
| ToolStripStatusLabel-002 | P1 | 🟡 | Spring=false默认；0/1/多个Spring、隐藏项、固定宽度项、宽度不足和奇数剩余像素分配，Spring运行时修改立即触发正确布局。 |
| ToolStripStatusLabel-003 | P1 | ⬜ | BorderSides、BorderStyle默认、四边组合和非法枚举；DPI与RTL下边框/内容矩形不吞文字，主题不覆盖显式设置。 |
| ToolStripStatusLabel-004 | P1 | ⬜ | Alignment、Margin/Padding、AutoSize、Size/Width/GetPreferredSize与Spring优先级；当前Math.Max(测量宽,分配宽)可能超容器需对照基线。 |
| ToolStripStatusLabel-005 | P1 | ⬜ | Text/Font/Image/DisplayStyle/Link属性双向同步；TextChanged等继承事件仅值改变时触发，布局本身不重新设值导致重复事件。 |
| ToolStripStatusLabel-006 | P2 | ⬜ | LiveSetting与UIA live region/GTK announcement/NSAccessibility通知；高频状态更新避免重复同文朗读，保留最终消息。 |
| ToolStripStatusLabel-007 | P2 | ⬜ | Click/助记键/键盘/IsLink的选择行为、ToolTip与StatusStrip.ShowItemToolTips配合；状态文字默认不误抢焦点。 |
| ToolStripStatusLabel-008 | P2 | ⬜ | Owner由StatusStrip换到普通ToolStrip时Spring语义按基线，Remove后旧Label宿主清理，Dispose不误释放外部图像。 |
| ToolStripStatusLabel-009 | P3 | ⬜ | Designer Spring/边框/链接与资源往返、AOT动态状态和三平台布局/无障碍验收。 |
| ToolStripStatusLabel-CLOSE | P3 | ⬜ | 按完整API清单逐个关闭剩余成员；每个访问器/重载、默认值、异常、通知次数/顺序、取消/重入、未挂载/overflow/移除/Dispose分别验证。Windows公开属性、公开事件、行为各≥98%；Linux/macOS可移植三项各≥98%，不可移植明细保留且需证据，未验收不计通过。 |

## 三平台的原生实现评估

**Windows**：继续原生STATIC/Label宿主参与StatusStrip布局；可评估STATUSCLASSNAME分区，但不能丢ToolStripLabel继承与任意条目组合。BorderSides/BorderStyle只有原生能表达的部分可直接映射，其余先确认兼容绘制边界。

**Linux / GTK4**：GtkLabel嵌入底部GtkBox，hexpand分配Spring；边框用平台原生样式/容器组合，须验证四边独立与Border3DStyle不完全映射。不能用系统通知区域组件代替窗体状态文字。

**macOS / AppKit**：窗体底部NSView+NSTextField，Auto Layout或当前托管布局分配Spring，使用原生分隔视图/边框能力验证；NSStatusItem是系统菜单栏功能，不是StatusStrip/StatusLabel对应。

实施顺序是 Windows 标准API/状态/事件→该条目最小原生试验→平台映射与完整验收。三平台分别记录属性/事件/行为的分子分母；继承API不能漏记，签名命中、自动属性、控件可以显示或本页任务已写完都不算通过。原生系统绘制、原生容器承载库绘制、原生控件组合应准确区分；公共业务行为难映射时仍然是待实现需求，不自动从可移植分母剔除。

各平台最少覆盖：代码与用户动作双向同步，Owner/Parent/overflow关系，事件顺序/取消/重入，键盘/助记键/输入法（适用时），DPI/RTL/本地化，无障碍，静态Designer和资源，AOT运行，移除与Dispose。没有完整证据时保持🟡/⬜，不发布已达标百分比。

## 修改历史

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 2026-04-08 · `7d7a67e` · Add initial implementation of Lumina.Forms library。
- 2026-03-31 · `52f5662` · Add core UI components for Lumina.Forms。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

## 官方依据

- [WinForms ToolStripStatusLabel API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripstatuslabel?view=windowsdesktop-10.0)，精确签名以本机10.0.11引用程序集生成的清单为准。
- [ToolStrip 控件概述](https://learn.microsoft.com/dotnet/desktop/winforms/controls/toolstrip-control-overview)。
- [Windows 原生参考](https://learn.microsoft.com/windows/win32/controls/status-bars)。
- [GTK4 Label](https://docs.gtk.org/gtk4/class.Label.html)。
- [AppKit nstextfield](https://developer.apple.com/documentation/appkit/nstextfield)。
