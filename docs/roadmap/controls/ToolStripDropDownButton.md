# ToolStripDropDownButton

基线：2026-10-15 · .NET 10.0.11。目标继承：`ToolStripDropDownButton : ToolStripDropDownItem`。本页为逐类型实施与验收任务，没有把规划当作已完成支持。

## 当前证据与依赖

🟡 [ToolStripDropDownButton.cs](../../../Lumina.Forms/ToolStripDropDownButton.cs)类体为空，但[ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs)按DropDownItem创建Button/PictureBox并打开原生菜单；已有基本下拉，缺ShowDropDownArrow与标准外观/键盘事件契约。

源码定位：[Lumina.Forms/ToolStripDropDownButton.cs](../../../Lumina.Forms/ToolStripDropDownButton.cs)；条目显示/更新必须同时检查 [ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs)、[StatusStrip.cs](../../../Lumina.Forms/StatusStrip.cs)、[NativeMenu.cs](../../../Lumina.Forms/NativeMenu.cs)、[ToolStripPopupMenu.cs](../../../Lumina.Forms/ToolStripPopupMenu.cs)，不能只凭派生类型文件的长短判断完整度。先决依赖：ToolStripDropDownItem与菜单、ToolStripItem图文与输入。

完整属性、事件、方法/重载（含继承）见 [ToolStripDropDownButton API 清单](../api/ToolStripDropDownButton.md)，共同契约见 [基础任务](../foundations.md)。条目是 WinForms 已有公开概念，原生控件组合留在库内部；本页不要求新增公开UI概念、平台项目或新依赖。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 是继承/阻塞，P1 是主要契约，P2 是边界/高级能力，P3 是平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| ToolStripDropDownButton-001 | P0 | 🟡 | 保留ToolStripDropDownButton : ToolStripDropDownItem、标准构造/AutoToolTip默认；现有下拉宿主接标准DropDown对象与事件。 |
| ToolStripDropDownButton-002 | P1 | ⬜ | ShowDropDownArrow=true默认、运行时隐藏/显示箭头，命中/测量/高DPI/RTL与纯图片/图文路径均生效。 |
| ToolStripDropDownButton-003 | P1 | ⬜ | 整按钮点击/空格/Enter/Alt+Down/助记键打开菜单，Click与DropDownOpening/Opened顺序；不错误执行SplitButton式DefaultItem。 |
| ToolStripDropDownButton-004 | P1 | ⬜ | Enabled/Available/Visible/Pressed、无条目按钮、取消Opening/Closing、菜单已开时重复点击与键盘关闭。 |
| ToolStripDropDownButton-005 | P1 | ⬜ | Text/Image/DisplayStyle/AutoSize/Padding/Margin/Overflow/ToolTip；图像路径仍有Button角色、焦点和原生菜单展开状态。 |
| ToolStripDropDownButton-006 | P2 | ⬜ | DropDownDirection/RTL/多屏边界/相邻下拉按钮导航，菜单中动态增删/状态变化后对象身份和焦点正确。 |
| ToolStripDropDownButton-007 | P2 | ⬜ | AutoToolTip与ToolTipText、命令/Click是否共存按基线，不让宿主Button.Click与菜单项Click重复冒泡。 |
| ToolStripDropDownButton-008 | P2 | ⬜ | Owner迁移、移除、Dispose菜单打开中退出与消息hook清理；外部Image/共享DropDown所有权。 |
| ToolStripDropDownButton-009 | P3 | ⬜ | Designer静态下拉项/箭头/资源与AOT真实操作，三平台键盘/辅助技术/原生箭头差异分别验收。 |
| ToolStripDropDownButton-CLOSE | P3 | ⬜ | 按完整API清单逐个关闭剩余成员；每个访问器/重载、默认值、异常、通知次数/顺序、取消/重入、未挂载/overflow/移除/Dispose分别验证。Windows公开属性、公开事件、行为各≥98%；Linux/macOS可移植三项各≥98%，不可移植明细保留且需证据，未验收不计通过。 |

## 三平台的原生实现评估

**Windows**：原生BUTTON或toolbar dropdown button结合HMENU，整个按钮触发下拉；箭头是独立呈现区域但不是SplitButton的独立命令区。当前纯图片PictureBox路径需补键盘与无障碍。

**Linux / GTK4**：GtkMenuButton+GtkPopoverMenu，always-show-arrow/direction/custom child提供箭头图文；所用API版本需固定最低GTK版本，不使用最新文档方法而不声明版本。

**macOS / AppKit**：NSPopUpButton的pullsDown模式结合NSMenu，或NSButton+原生menu弹出；图文/箭头/尺寸/首次菜单项显示与WinFormsDropDownButton行为对照。

实施顺序是 Windows 标准API/状态/事件→该条目最小原生试验→平台映射与完整验收。三平台分别记录属性/事件/行为的分子分母；继承API不能漏记，签名命中、自动属性、控件可以显示或本页任务已写完都不算通过。原生系统绘制、原生容器承载库绘制、原生控件组合应准确区分；公共业务行为难映射时仍然是待实现需求，不自动从可移植分母剔除。

各平台最少覆盖：代码与用户动作双向同步，Owner/Parent/overflow关系，事件顺序/取消/重入，键盘/助记键/输入法（适用时），DPI/RTL/本地化，无障碍，静态Designer和资源，AOT运行，移除与Dispose。没有完整证据时保持🟡/⬜，不发布已达标百分比。

## 修改历史

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 2026-03-31 · `52f5662` · Add core UI components for Lumina.Forms。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

## 官方依据

- [WinForms ToolStripDropDownButton API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripdropdownbutton?view=windowsdesktop-10.0)，精确签名以本机10.0.11引用程序集生成的清单为准。
- [ToolStrip 控件概述](https://learn.microsoft.com/dotnet/desktop/winforms/controls/toolstrip-control-overview)。
- [Windows 原生参考](https://learn.microsoft.com/windows/win32/controls/buttons)。
- [GTK4 MenuButton](https://docs.gtk.org/gtk4/class.MenuButton.html)。
- [AppKit nspopupbutton](https://developer.apple.com/documentation/appkit/nspopupbutton)。
