# ToolStripButton

基线：2026-10-15 · .NET 10.0.11。目标继承：`ToolStripButton : ToolStripItem`。本页为逐类型实施与验收任务，没有把规划当作已完成支持。

## 当前证据与依赖

🟡 [ToolStripButton.cs](../../../Lumina.Forms/ToolStripButton.cs) 类体为空，但[ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs)会创建Button或纯图片PictureBox并把Click接到PerformClick；实际可点击，缺Checked/CheckState/CheckOnClick及相应事件。

源码定位：[Lumina.Forms/ToolStripButton.cs](../../../Lumina.Forms/ToolStripButton.cs)；条目显示/更新必须同时检查 [ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs)、[StatusStrip.cs](../../../Lumina.Forms/StatusStrip.cs)、[NativeMenu.cs](../../../Lumina.Forms/NativeMenu.cs)、[ToolStripPopupMenu.cs](../../../Lumina.Forms/ToolStripPopupMenu.cs)，不能只凭派生类型文件的长短判断完整度。先决依赖：ToolStripItem选择/命令/图文、ToolStrip宿主同步。

完整属性、事件、方法/重载（含继承）见 [ToolStripButton API 清单](../api/ToolStripButton.md)，共同契约见 [基础任务](../foundations.md)。条目是 WinForms 已有公开概念，原生控件组合留在库内部；本页不要求新增公开UI概念、平台项目或新依赖。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 是继承/阻塞，P1 是主要契约，P2 是边界/高级能力，P3 是平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| ToolStripButton-001 | P0 | 🟡 | 保留ToolStripButton : ToolStripItem，补标准构造重载及AutoToolTip默认，现有原生Button映射成为统一条目状态/动作管线。 |
| ToolStripButton-002 | P1 | ⬜ | Checked=false、CheckOnClick=false、CheckState=Unchecked；Checked与三态CheckState关联，非法枚举和同值赋入按基线。 |
| ToolStripButton-003 | P1 | ⬜ | CheckOnClick点击切换及CheckedChanged/CheckStateChanged/Click的顺序；程序赋值、键盘/鼠标/PerformClick、Command失败/禁用分别验收。 |
| ToolStripButton-004 | P1 | ⬜ | Text/Image/DisplayStyle/ImageScaling/TextImageRelation，ImageAndText与纯图标路径一致；纯图片按钮不能丢助记键、焦点和可访问默认动作。 |
| ToolStripButton-005 | P1 | ⬜ | Selected/Pressed/Enabled/Available/Visible、鼠标捕获取消与disabled点击，不把selected悬停和checked锁定混为一态。 |
| ToolStripButton-006 | P2 | ⬜ | AutoToolTip、ToolTipText、AutoSize/GetPreferredSize、Margin/Padding、Overflow/Alignment，在状态切换与主题变化后立即重排/重画。 |
| ToolStripButton-007 | P2 | ⬜ | 绑定Checked/CheckState与Command，CanExecute联动，回调中再次改值不多次激活；外部ICommand订阅随Dispose释放。 |
| ToolStripButton-008 | P2 | ⬜ | 辅助技术toggle/button角色和checked/mixed、键盘快捷与名称；overflow中仍然是同一逻辑按钮状态。 |
| ToolStripButton-009 | P3 | ⬜ | Designer静态Checked/CheckOnClick/图片资源，AOT真实点击/切换，三平台原生按钮和Dispose事件解绑。 |
| ToolStripButton-CLOSE | P3 | ⬜ | 按完整API清单逐个关闭剩余成员；每个访问器/重载、默认值、异常、通知次数/顺序、取消/重入、未挂载/overflow/移除/Dispose分别验证。Windows公开属性、公开事件、行为各≥98%；Linux/macOS可移植三项各≥98%，不可移植明细保留且需证据，未验收不计通过。 |

## 三平台的原生实现评估

**Windows**：当前原生Button宿主可继续使用；CheckOnClick状态可用BUTTON toggle样式或toolbar按钮态映射，但真实托管模型、事件先后必须统一。纯图片条目用PictureBox时需补按钮键盘/无障碍语义，不能只支持鼠标。

**Linux / GTK4**：GtkButton/GtkToggleButton，clicked/toggled对接；未选中/选中/Indeterminate应先验证原生表示能力，mixed外观不支持时单列；GTK active与托管CheckState去重同步。

**macOS / AppKit**：NSButton的momentary/toggle类型及state映射，target/action统一Click/Check状态/Command顺序；混合状态用allowsMixedState验证；NSToolbarItem的view承载按钮。

实施顺序是 Windows 标准API/状态/事件→该条目最小原生试验→平台映射与完整验收。三平台分别记录属性/事件/行为的分子分母；继承API不能漏记，签名命中、自动属性、控件可以显示或本页任务已写完都不算通过。原生系统绘制、原生容器承载库绘制、原生控件组合应准确区分；公共业务行为难映射时仍然是待实现需求，不自动从可移植分母剔除。

各平台最少覆盖：代码与用户动作双向同步，Owner/Parent/overflow关系，事件顺序/取消/重入，键盘/助记键/输入法（适用时），DPI/RTL/本地化，无障碍，静态Designer和资源，AOT运行，移除与Dispose。没有完整证据时保持🟡/⬜，不发布已达标百分比。

## 修改历史

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 2026-04-08 · `7149595` · Implement dark mode capabilities and menu rendering enhancements。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

## 官方依据

- [WinForms ToolStripButton API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripbutton?view=windowsdesktop-10.0)，精确签名以本机10.0.11引用程序集生成的清单为准。
- [ToolStrip 控件概述](https://learn.microsoft.com/dotnet/desktop/winforms/controls/toolstrip-control-overview)。
- [Windows 原生参考](https://learn.microsoft.com/windows/win32/controls/buttons)。
- [GTK4 ToggleButton](https://docs.gtk.org/gtk4/class.ToggleButton.html)。
- [AppKit nsbutton](https://developer.apple.com/documentation/appkit/nsbutton)。
