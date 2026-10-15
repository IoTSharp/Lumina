# ToolStripComboBox

基线：2026-10-15 · .NET 10.0.11。目标继承：`ToolStripComboBox : ToolStripControlHost`。本页为逐类型实施与验收任务，没有把规划当作已完成支持。

## 当前证据与依赖

🟡 [ToolStripComboBox.cs](../../../Lumina.Forms/ToolStripComboBox.cs)有List<object?> Items和SelectedIndex自动属性；[ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs)创建ComboBox并在每次ApplyItemState Clear/AddRange/设选择，未见宿主SelectedIndexChanged回写。缺标准ComboBox属性、事件与集合类型。

源码定位：[Lumina.Forms/ToolStripComboBox.cs](../../../Lumina.Forms/ToolStripComboBox.cs)；条目显示/更新必须同时检查 [ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs)、[StatusStrip.cs](../../../Lumina.Forms/StatusStrip.cs)、[NativeMenu.cs](../../../Lumina.Forms/NativeMenu.cs)、[ToolStripPopupMenu.cs](../../../Lumina.Forms/ToolStripPopupMenu.cs)，不能只凭派生类型文件的长短判断完整度。先决依赖：ToolStripControlHost、ComboBox及标准ComboBox.ObjectCollection，列表/选择双向状态。

完整属性、事件、方法/重载（含继承）见 [ToolStripComboBox API 清单](../api/ToolStripComboBox.md)，共同契约见 [基础任务](../foundations.md)。条目是 WinForms 已有公开概念，原生控件组合留在库内部；本页不要求新增公开UI概念、平台项目或新依赖。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 是继承/阻塞，P1 是主要契约，P2 是边界/高级能力，P3 是平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| ToolStripComboBox-001 | P0 | ⬜ | 改为ToolStripComboBox : ToolStripControlHost；公开ComboBox属性与Items的标准ObjectCollection类型，构造/Name重载签名精确。 |
| ToolStripComboBox-002 | P1 | 🟡 | Items Add/Insert/Remove/Clear/排序双向映射与SelectedIndex/SelectedItem；修复布局中无差别Clear造成的选择/展开态/事件重置。 |
| ToolStripComboBox-003 | P1 | ⬜ | SelectedIndex=-1默认、SelectedItem/SelectedText、Text、SelectionStart/Length；用户选项回写条目，程序设值刷新宿主，重复显示文字不丢对象身份。 |
| ToolStripComboBox-004 | P1 | ⬜ | SelectedIndexChanged、TextUpdate、TextChanged、DropDown、DropDownClosed、DropDownStyleChanged、DoubleClick；程序/用户路径各自事件次序与同值不重复。 |
| ToolStripComboBox-005 | P1 | ⬜ | DropDownStyle、DroppedDown、DropDownHeight/Width、MaxDropDownItems、IntegralHeight、FlatStyle、Sorted；所有默认值/边界/展开中修改真实生效。 |
| ToolStripComboBox-006 | P2 | ⬜ | AutoCompleteMode/Source/CustomSource、MaxLength、FindString/FindStringExact/GetItemHeight/Select等方法与重载；底层关联控件成员透传不遗漏。 |
| ToolStripComboBox-007 | P2 | ⬜ | 键盘上下/Alt+Down/Escape/Enter/Tab、IME输入和候选、验证取消与菜单快捷键；下拉关闭后恢复正确焦点。 |
| ToolStripComboBox-008 | P2 | ⬜ | Items变更通知和属性Changed桥、代码赋无效选择、空集合/删除选中项、数据绑定通过公开ComboBox使用时行为完整。 |
| ToolStripComboBox-009 | P2 | ⬜ | 图像字体资源、测量/DPI/RTL、overflow与DropDown嵌入限制（当前NativeMenu禁用），辅助技术combo role/选项/展开状态。 |
| ToolStripComboBox-010 | P3 | ⬜ | Designer静态Items/选择/风格和AOT操作，Dispose订阅/原生列表资源，三平台可编辑与只选两类原生方案分别验收。 |
| ToolStripComboBox-CLOSE | P3 | ⬜ | 按完整API清单逐个关闭剩余成员；每个访问器/重载、默认值、异常、通知次数/顺序、取消/重入、未挂载/overflow/移除/Dispose分别验证。Windows公开属性、公开事件、行为各≥98%；Linux/macOS可移植三项各≥98%，不可移植明细保留且需证据，未验收不计通过。 |

## 三平台的原生实现评估

**Windows**：唯一COMBOBOX宿主由公开ComboBox属性访问；CBN_SELCHANGE/EDITCHANGE/DROPDOWN/CLOSEUP映射到底层再转发；按集合增量更新而非每次布局Clear，避免选择/事件/展开态被重置。

**Linux / GTK4**：底层ComboBox方案用GtkDropDown（只选）或GtkEntry+GtkPopover/ListView（可编辑）原生组合；包装层不改变DropDownStyle和SelectedItem对象语义。不能仅用GtkDropDown支持不可编辑模式就称三种风格均实现。

**macOS / AppKit**：NSComboBox（可编辑）/NSPopUpButton（只选）映射底层；NSToolbarItem.view承载，列表弹出与工具栏overflow/菜单追踪冲突需实测。SelectedItem保持原对象而非显示字符串。

实施顺序是 Windows 标准API/状态/事件→该条目最小原生试验→平台映射与完整验收。三平台分别记录属性/事件/行为的分子分母；继承API不能漏记，签名命中、自动属性、控件可以显示或本页任务已写完都不算通过。原生系统绘制、原生容器承载库绘制、原生控件组合应准确区分；公共业务行为难映射时仍然是待实现需求，不自动从可移植分母剔除。

各平台最少覆盖：代码与用户动作双向同步，Owner/Parent/overflow关系，事件顺序/取消/重入，键盘/助记键/输入法（适用时），DPI/RTL/本地化，无障碍，静态Designer和资源，AOT运行，移除与Dispose。没有完整证据时保持🟡/⬜，不发布已达标百分比。

## 修改历史

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 2026-03-31 · `52f5662` · Add core UI components for Lumina.Forms。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

## 官方依据

- [WinForms ToolStripComboBox API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripcombobox?view=windowsdesktop-10.0)，精确签名以本机10.0.11引用程序集生成的清单为准。
- [ToolStrip 控件概述](https://learn.microsoft.com/dotnet/desktop/winforms/controls/toolstrip-control-overview)。
- [Windows 原生参考](https://learn.microsoft.com/windows/win32/controls/combo-boxes)。
- [GTK4 DropDown](https://docs.gtk.org/gtk4/class.DropDown.html)。
- [AppKit nscombobox](https://developer.apple.com/documentation/appkit/nscombobox)。
