# ToolStripLabel

基线：2026-10-15 · .NET 10.0.11。目标继承：`ToolStripLabel : ToolStripItem`。本页为逐类型实施与验收任务，没有把规划当作已完成支持。

## 当前证据与依赖

🟡 [ToolStripLabel.cs](../../../Lumina.Forms/ToolStripLabel.cs)类体为空；[ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs)创建原生Label并单向设置Text，能显示文字。IsLink、LinkBehavior/颜色/Visited与选择/链接激活未接通。

源码定位：[Lumina.Forms/ToolStripLabel.cs](../../../Lumina.Forms/ToolStripLabel.cs)；条目显示/更新必须同时检查 [ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs)、[StatusStrip.cs](../../../Lumina.Forms/StatusStrip.cs)、[NativeMenu.cs](../../../Lumina.Forms/NativeMenu.cs)、[ToolStripPopupMenu.cs](../../../Lumina.Forms/ToolStripPopupMenu.cs)，不能只凭派生类型文件的长短判断完整度。先决依赖：ToolStripItem、Label/LinkLabel测量及链接激活。

完整属性、事件、方法/重载（含继承）见 [ToolStripLabel API 清单](../api/ToolStripLabel.md)，共同契约见 [基础任务](../foundations.md)。条目是 WinForms 已有公开概念，原生控件组合留在库内部；本页不要求新增公开UI概念、平台项目或新依赖。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 是继承/阻塞，P1 是主要契约，P2 是边界/高级能力，P3 是平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| ToolStripLabel-001 | P0 | 🟡 | 保留ToolStripLabel : ToolStripItem、标准构造重载；普通Label宿主与IsLink宿主切换保留逻辑条目/Owner/事件。 |
| ToolStripLabel-002 | P1 | ⬜ | IsLink=false、LinkVisited=false、LinkBehavior及ActiveLinkColor/LinkColor/VisitedLinkColor默认值与运行时变化真实生效。 |
| ToolStripLabel-003 | P1 | ⬜ | CanSelect与IsLink关系、Tab/方向/助记键/Enter激活；普通静态标签不错误占焦点，链接激活发正确条目Click一次。 |
| ToolStripLabel-004 | P1 | ⬜ | Enabled/Available/Visible与链接状态、鼠标悬停/按下/取消、访问态颜色和主题/高对比度，点击后不擅自更新LinkVisited（按基线确认）。 |
| ToolStripLabel-005 | P1 | ⬜ | Text/Font/Image/DisplayStyle、TextAlign/ImageAlign/TextImageRelation/RTL、AutoSize、Padding/Margin测量；文本变化有TextChanged且宿主显示同步。 |
| ToolStripLabel-006 | P2 | ⬜ | 图像/图文路径不把标签误创建成可点击PictureBox按钮，链接辅助角色/名称与普通静态文本分别映射。 |
| ToolStripLabel-007 | P2 | ⬜ | ToolTipText/AutoToolTip、overflow与菜单中标签可选/可点击行为、Owner迁移和布局完成后命中。 |
| ToolStripLabel-008 | P2 | ⬜ | System.Drawing.Image/Font所有权、Links样式资源、移除/Dispose后原生链接回调解绑；不因颜色重建丢事件。 |
| ToolStripLabel-009 | P3 | ⬜ | Designer IsLink/颜色/图片/.resx初始化、AOT链接事件、三平台原生普通标签与链接标签各有验收。 |
| ToolStripLabel-CLOSE | P3 | ⬜ | 按完整API清单逐个关闭剩余成员；每个访问器/重载、默认值、异常、通知次数/顺序、取消/重入、未挂载/overflow/移除/Dispose分别验证。Windows公开属性、公开事件、行为各≥98%；Linux/macOS可移植三项各≥98%，不可移植明细保留且需证据，未验收不计通过。 |

## 三平台的原生实现评估

**Windows**：普通标签用STATIC，IsLink时评估SysLink或已有LinkLabel宿主；ToolStripLabel的Click仍走条目事件，不能改为公开LinkLabelLinkClicked替代。共享图文/布局和ToolStrip键盘导航。

**Linux / GTK4**：GtkLabel普通/markup链接，activate-link统一到条目Click；Image可用原生图像+标签组合。Windows颜色/助记键惯例与GTK链接默认导航分开处理，应用决定实际导航。

**macOS / AppKit**：普通NSTextField标签，IsLink用带链接的NSTextView或原生按钮样式验证；不自动打开URL，不把NSAttributedString的URL当托管Tag。可选与不可选由CanSelect基线决定。

实施顺序是 Windows 标准API/状态/事件→该条目最小原生试验→平台映射与完整验收。三平台分别记录属性/事件/行为的分子分母；继承API不能漏记，签名命中、自动属性、控件可以显示或本页任务已写完都不算通过。原生系统绘制、原生容器承载库绘制、原生控件组合应准确区分；公共业务行为难映射时仍然是待实现需求，不自动从可移植分母剔除。

各平台最少覆盖：代码与用户动作双向同步，Owner/Parent/overflow关系，事件顺序/取消/重入，键盘/助记键/输入法（适用时），DPI/RTL/本地化，无障碍，静态Designer和资源，AOT运行，移除与Dispose。没有完整证据时保持🟡/⬜，不发布已达标百分比。

## 修改历史

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 2026-04-08 · `7149595` · Implement dark mode capabilities and menu rendering enhancements。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

## 官方依据

- [WinForms ToolStripLabel API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstriplabel?view=windowsdesktop-10.0)，精确签名以本机10.0.11引用程序集生成的清单为准。
- [ToolStrip 控件概述](https://learn.microsoft.com/dotnet/desktop/winforms/controls/toolstrip-control-overview)。
- [Windows 原生参考](https://learn.microsoft.com/windows/win32/controls/static-controls)。
- [GTK4 Label](https://docs.gtk.org/gtk4/class.Label.html)。
- [AppKit nstextfield](https://developer.apple.com/documentation/appkit/nstextfield)。
