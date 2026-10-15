# ToolStripSplitButton

基线：2026-10-15 · .NET 10.0.11。目标继承：`ToolStripSplitButton : ToolStripDropDownItem`。本页为逐类型实施与验收任务，没有把规划当作已完成支持。

## 当前证据与依赖

🟡 [ToolStripSplitButton.cs](../../../Lumina.Forms/ToolStripSplitButton.cs)类体为空；当前[ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs)把所有DropDownItem同样作为整按钮下拉，未区分主按钮/箭头两区。不能把能弹菜单算已实现SplitButton。

源码定位：[Lumina.Forms/ToolStripSplitButton.cs](../../../Lumina.Forms/ToolStripSplitButton.cs)；条目显示/更新必须同时检查 [ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs)、[StatusStrip.cs](../../../Lumina.Forms/StatusStrip.cs)、[NativeMenu.cs](../../../Lumina.Forms/NativeMenu.cs)、[ToolStripPopupMenu.cs](../../../Lumina.Forms/ToolStripPopupMenu.cs)，不能只凭派生类型文件的长短判断完整度。先决依赖：ToolStripDropDownItem、ToolStripItem输入命中、DefaultItem关系与布局。

完整属性、事件、方法/重载（含继承）见 [ToolStripSplitButton API 清单](../api/ToolStripSplitButton.md)，共同契约见 [基础任务](../foundations.md)。条目是 WinForms 已有公开概念，原生控件组合留在库内部；本页不要求新增公开UI概念、平台项目或新依赖。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 是继承/阻塞，P1 是主要契约，P2 是边界/高级能力，P3 是平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| ToolStripSplitButton-001 | P0 | ⬜ | 保留ToolStripSplitButton : ToolStripDropDownItem，建立真实主区/下拉区/分隔区；已有整按钮下拉不能标该任务完成。 |
| ToolStripSplitButton-002 | P1 | ⬜ | ButtonBounds、DropDownButtonBounds、SplitterBounds、DropDownButtonWidth、ResetDropDownButtonWidth；DPI/RTL/AutoSize/窄尺寸下几何与命中准确。 |
| ToolStripSplitButton-003 | P1 | ⬜ | ButtonPressed/ButtonSelected、DropDownButtonPressed/Selected，悬停跨区/捕获拖离/取消状态；主区动作不误弹菜单，箭头动作不执行主命令。 |
| ToolStripSplitButton-004 | P1 | ⬜ | ButtonClick、ButtonDoubleClick、Click、DoubleClick、PerformButtonClick与PerformClick差异；DoubleClickEnabled和事件顺序按基线逐动作记录。 |
| ToolStripSplitButton-005 | P1 | ⬜ | DefaultItem与DefaultItemChanged，主区调用默认项、默认项禁用/移除/释放/Owner迁移、无默认项时行为；事件订阅替换对称清理。 |
| ToolStripSplitButton-006 | P1 | ⬜ | ShowDropDown/HideDropDown与Opening/Opened/Closed/ItemClicked、DismissWhenClicked；关闭取消、主命令执行与焦点恢复顺序。 |
| ToolStripSplitButton-007 | P2 | ⬜ | 键盘空格/Enter/Alt+Down/方向/助记键、两个区的辅助动作和菜单展开状态；既满足单逻辑条目又暴露可操作两区。 |
| ToolStripSplitButton-008 | P2 | ⬜ | Text/Image/DisplayStyle/AutoToolTip/Overflow/Alignment，图文更新不重建丢失DefaultItem或回调；纯图片宿主同样具有两区。 |
| ToolStripSplitButton-009 | P2 | ⬜ | 句柄重建、菜单打开中禁用/移除/Dispose、共享DropDown和DefaultItem所有权，原生消息/target/action解绑。 |
| ToolStripSplitButton-010 | P3 | ⬜ | Designer静态DefaultItem/DropDownItems/尺寸与资源、AOT两区真实交互，GTK/AppKit原生组合及窗口边缘定位验收。 |
| ToolStripSplitButton-CLOSE | P3 | ⬜ | 按完整API清单逐个关闭剩余成员；每个访问器/重载、默认值、异常、通知次数/顺序、取消/重入、未挂载/overflow/移除/Dispose分别验证。Windows公开属性、公开事件、行为各≥98%；Linux/macOS可移植三项各≥98%，不可移植明细保留且需证据，未验收不计通过。 |

## 三平台的原生实现评估

**Windows**：可评估BUTTON的BS_SPLITBUTTON/BCN_DROPDOWN，或原生Button+下拉Button组合；主区和箭头区必须独立命中/焦点状态/动作。公开仍只暴露ToolStripSplitButton，DefaultItem与菜单为托管语义。

**Linux / GTK4**：GTK4核心用GtkButton+GtkMenuButton在GtkBox中原生组合；不要误称GTK4有GtkSplitButton或隐式增加libadwaita依赖。主区clicked与箭头popover动作独立，布局/键盘/可访问关系共享。

**macOS / AppKit**：NSButton+NSPopUpButton的原生组合承载在NSView/NSToolbarItem.view；AppKit没有必要假设一个完整同义WinFormsSplitButton，公开两区几何和状态由内部布局计算。

实施顺序是 Windows 标准API/状态/事件→该条目最小原生试验→平台映射与完整验收。三平台分别记录属性/事件/行为的分子分母；继承API不能漏记，签名命中、自动属性、控件可以显示或本页任务已写完都不算通过。原生系统绘制、原生容器承载库绘制、原生控件组合应准确区分；公共业务行为难映射时仍然是待实现需求，不自动从可移植分母剔除。

各平台最少覆盖：代码与用户动作双向同步，Owner/Parent/overflow关系，事件顺序/取消/重入，键盘/助记键/输入法（适用时），DPI/RTL/本地化，无障碍，静态Designer和资源，AOT运行，移除与Dispose。没有完整证据时保持🟡/⬜，不发布已达标百分比。

## 内部嵌套条目的任务归属

`System.Windows.Forms.ToolStripSplitButton+ToolStripSplitButtonButton` 是固定程序集中的非公开 ToolStripButton 派生实现。它承载主按钮区域，但不是另外一个消费端公开控件：主区几何/状态/点击分别归 ToolStripSplitButton-002/003/004，DefaultItem 归 -005，释放归 -009，阶段 P2。跨平台可以用原生按钮组合实现同样契约，无需复制此内部类型。

## 修改历史

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 2026-03-31 · `52f5662` · Add core UI components for Lumina.Forms。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

## 官方依据

- [WinForms ToolStripSplitButton API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripsplitbutton?view=windowsdesktop-10.0)，精确签名以本机10.0.11引用程序集生成的清单为准。
- [ToolStrip 控件概述](https://learn.microsoft.com/dotnet/desktop/winforms/controls/toolstrip-control-overview)。
- [Windows 原生参考](https://learn.microsoft.com/windows/win32/controls/buttons)。
- [GTK4 MenuButton](https://docs.gtk.org/gtk4/class.MenuButton.html)。
- [AppKit nspopupbutton](https://developer.apple.com/documentation/appkit/nspopupbutton)。
