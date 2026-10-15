# ToolStripTextBox

基线：2026-10-15 · .NET 10.0.11。目标继承：`ToolStripTextBox : ToolStripControlHost`。本页为逐类型实施与验收任务，没有把规划当作已完成支持。

## 当前证据与依赖

🟡 [ToolStripTextBox.cs](../../../Lumina.Forms/ToolStripTextBox.cs)类体为空且继承ToolStripItem；[ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs)确实创建TextBox并把item.Text写入宿主，但未见订阅宿主TextChanged回写条目，下次布局可能覆盖用户输入。标准TextBox属性和输入事件桥缺失。

源码定位：[Lumina.Forms/ToolStripTextBox.cs](../../../Lumina.Forms/ToolStripTextBox.cs)；条目显示/更新必须同时检查 [ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs)、[StatusStrip.cs](../../../Lumina.Forms/StatusStrip.cs)、[NativeMenu.cs](../../../Lumina.Forms/NativeMenu.cs)、[ToolStripPopupMenu.cs](../../../Lumina.Forms/ToolStripPopupMenu.cs)，不能只凭派生类型文件的长短判断完整度。先决依赖：ToolStripControlHost、TextBox/TextBoxBase、ToolStrip的双向状态与焦点/overflow。

完整属性、事件、方法/重载（含继承）见 [ToolStripTextBox API 清单](../api/ToolStripTextBox.md)，共同契约见 [基础任务](../foundations.md)。条目是 WinForms 已有公开概念，原生控件组合留在库内部；本页不要求新增公开UI概念、平台项目或新依赖。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 是继承/阻塞，P1 是主要契约，P2 是边界/高级能力，P3 是平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| ToolStripTextBox-001 | P0 | ⬜ | 改为ToolStripTextBox : ToolStripControlHost，公开TextBox只读属性恒定指向实际Control；标准构造/Name重载、受保护事件订阅点。 |
| ToolStripTextBox-002 | P1 | 🟡 | Text双向同步：用户输入→item.Text/TextChanged/绑定；代码赋值→EDIT显示；布局、overflow、主题刷新不恢复旧文本或重置光标。 |
| ToolStripTextBox-003 | P1 | ⬜ | AcceptsReturn/AcceptsTab、Multiline/WordWrap/Lines、ReadOnly/HideSelection、MaxLength/ShortcutsEnabled/CharacterCasing及默认值按底层基线逐项透传。 |
| ToolStripTextBox-004 | P1 | ⬜ | SelectionStart/SelectionLength/SelectedText/TextLength、Select/SelectAll/DeselectAll、AppendText/Clear、ScrollToCaret与各坐标/行方法，创建前选择也保留。 |
| ToolStripTextBox-005 | P1 | ⬜ | Copy/Cut/Paste/Undo/ClearUndo/CanUndo/Modified、ModifiedChanged；只读、剪贴板不可用、用户未提交输入与撤销历史。 |
| ToolStripTextBox-006 | P1 | ⬜ | AcceptsTabChanged/BorderStyleChanged/HideSelectionChanged/MultilineChanged/ReadOnlyChanged/TextBoxTextAlignChanged、TextChanged和继承Key事件的sender/顺序/次数。 |
| ToolStripTextBox-007 | P2 | ⬜ | AutoCompleteMode/Source/CustomSource、TextBoxTextAlign、BorderStyle、字体/DPI/AutoSize；底层TextBox的Shell专属自动完成差异不能在包装层忽略。 |
| ToolStripTextBox-008 | P2 | ⬜ | Enter/Leave/GotFocus/LostFocus、Validating/Validated、CausesValidation、Tab/Enter/Escape与菜单关闭/快捷键；编辑中按Ctrl+C不被菜单快捷键吞。 |
| ToolStripTextBox-009 | P2 | ⬜ | overflow/DropDown中的原生编辑宿主与Accessibility文本/选区，移除、隐藏与Dispose时释放field editor/订阅且不丢用户文本。 |
| ToolStripTextBox-010 | P3 | ⬜ | Designer静态条目和公开TextBox配置、AOT输入/IME/粘贴，三平台同一宿主对象身份及双向数据流验收。 |
| ToolStripTextBox-CLOSE | P3 | ⬜ | 按完整API清单逐个关闭剩余成员；每个访问器/重载、默认值、异常、通知次数/顺序、取消/重入、未挂载/overflow/移除/Dispose分别验证。Windows公开属性、公开事件、行为各≥98%；Linux/macOS可移植三项各≥98%，不可移植明细保留且需证据，未验收不计通过。 |

## 三平台的原生实现评估

**Windows**：使用公开TextBox属性指向的唯一EDIT控件；EN_CHANGE等交给TextBox再转发条目事件，禁止每次Layout重新写旧Text。HMENU原生命令项不支持编辑框，当前NativeMenu将此类禁用，需标准DropDown宿主面实现而非算已支持。

**Linux / GTK4**：GtkEntry/必要时GtkTextView由标准TextBox宿主提供，GtkBox/GtkPopover承载；用户编辑回写条目Text、绑定和事件。GMenuModel不是任意控件容器，须用原生Popover内容而非静态菜单字符串。

**macOS / AppKit**：NSTextField或NSTextView宿主，通过NSToolbarItem.view/普通NSView容器；field editor焦点/IME/剪贴板与ToolStrip菜单键盘分配要实测，不能每次布局重设stringValue。

实施顺序是 Windows 标准API/状态/事件→该条目最小原生试验→平台映射与完整验收。三平台分别记录属性/事件/行为的分子分母；继承API不能漏记，签名命中、自动属性、控件可以显示或本页任务已写完都不算通过。原生系统绘制、原生容器承载库绘制、原生控件组合应准确区分；公共业务行为难映射时仍然是待实现需求，不自动从可移植分母剔除。

各平台最少覆盖：代码与用户动作双向同步，Owner/Parent/overflow关系，事件顺序/取消/重入，键盘/助记键/输入法（适用时），DPI/RTL/本地化，无障碍，静态Designer和资源，AOT运行，移除与Dispose。没有完整证据时保持🟡/⬜，不发布已达标百分比。

## 修改历史

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 2026-03-31 · `52f5662` · Add core UI components for Lumina.Forms。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

## 官方依据

- [WinForms ToolStripTextBox API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstriptextbox?view=windowsdesktop-10.0)，精确签名以本机10.0.11引用程序集生成的清单为准。
- [ToolStrip 控件概述](https://learn.microsoft.com/dotnet/desktop/winforms/controls/toolstrip-control-overview)。
- [Windows 原生参考](https://learn.microsoft.com/windows/win32/controls/edit-controls)。
- [GTK4 Entry](https://docs.gtk.org/gtk4/class.Entry.html)。
- [AppKit nstextfield](https://developer.apple.com/documentation/appkit/nstextfield)。
