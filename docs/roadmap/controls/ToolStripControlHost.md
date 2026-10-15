# ToolStripControlHost

基线：2026-10-15 · .NET 10.0.11。目标继承：`ToolStripControlHost : ToolStripItem`。本页为逐类型实施与验收任务，没有把规划当作已完成支持。

## 当前证据与依赖

⬜ 无 Lumina.Forms/ToolStripControlHost.cs。当前ToolStripTextBox/ComboBox/ProgressBar直接继承ToolStripItem，原生控件由[ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs)临时创建，缺公开Control属性、标准宿主继承与事件桥。

源码定位：`Lumina.Forms/ToolStripControlHost.cs`（尚不存在）；条目显示/更新必须同时检查 [ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs)、[StatusStrip.cs](../../../Lumina.Forms/StatusStrip.cs)、[NativeMenu.cs](../../../Lumina.Forms/NativeMenu.cs)、[ToolStripPopupMenu.cs](../../../Lumina.Forms/ToolStripPopupMenu.cs)，不能只凭派生类型文件的长短判断完整度。先决依赖：ToolStripItem、Control真实Parent/容器、焦点/验证/布局；先实现此标准基类再收敛三种输入宿主条目。

完整属性、事件、方法/重载（含继承）见 [ToolStripControlHost API 清单](../api/ToolStripControlHost.md)，共同契约见 [基础任务](../foundations.md)。条目是 WinForms 已有公开概念，原生控件组合留在库内部；本页不要求新增公开UI概念、平台项目或新依赖。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 是继承/阻塞，P1 是主要契约，P2 是边界/高级能力，P3 是平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| ToolStripControlHost-001 | P0 | ⬜ | 实现ToolStripControlHost(Control)及标准构造重载，Control只读属性、正确继承；ToolStripTextBox/ComboBox/ProgressBar改为从该基类派生。 |
| ToolStripControlHost-002 | P1 | ⬜ | ControlAlign、Size/AutoSize、GetPreferredSize、Bounds变化；宿主Control尺寸改变反向触发布局，最小/最大尺寸、Margin/Padding和DPI不反复震荡。 |
| ToolStripControlHost-003 | P1 | ⬜ | Text、Enabled、Visible、Font、BackColor/ForeColor、RightToLeft、BackgroundImage/Layout的双向透传、继承值和Changed事件一致。 |
| ToolStripControlHost-004 | P1 | ⬜ | Focus()签名为void（区别于Control.Focus的bool）、Focused/Selected/CanSelect、Tab导航和原生编辑子对象；Enter/GotFocus/Leave/LostFocus只按逻辑跨宿主边界触发。 |
| ToolStripControlHost-005 | P1 | ⬜ | CausesValidation、Validating(cancel)/Validated与Form验证规则；验证失败时焦点/菜单关闭/按钮点击不能绕过取消。 |
| ToolStripControlHost-006 | P1 | ⬜ | KeyDown/KeyPress/KeyUp、Click/DoubleClick、鼠标/拖放事件转发；sender为条目或Control按基线记录，不因重复订阅一动作发两次。 |
| ToolStripControlHost-007 | P1 | ⬜ | OnSubscribeControlEvents/OnUnsubscribeControlEvents可覆盖点；派生ControlHost自定义Control可直接挂入，移除/更Owner/Dispose时对称解绑。 |
| ToolStripControlHost-008 | P2 | ⬜ | Control/Site/Component容器生命周期，宿主Dispose是否Dispose Control按基线；Remove与Dispose分开，绝不在每次布局换掉公开Control对象。 |
| ToolStripControlHost-009 | P2 | ⬜ | overflow/DropDown迁移、菜单中的键盘输入与焦点恢复；原生菜单无法放控件的场景作为具体实现任务，不能自动列不支持。 |
| ToolStripControlHost-010 | P2 | ⬜ | 辅助技术宿主与Control关系，避免双重role/Name/焦点；内嵌文本选择/值变化和验证提示可被读到。 |
| ToolStripControlHost-011 | P3 | ⬜ | Designer静态标准宿主与用户派生宿主、AOT无动态代理、三平台输入/关闭/释放真实运行并逐属性事件验收。 |
| ToolStripControlHost-CLOSE | P3 | ⬜ | 按完整API清单逐个关闭剩余成员；每个访问器/重载、默认值、异常、通知次数/顺序、取消/重入、未挂载/overflow/移除/Dispose分别验证。Windows公开属性、公开事件、行为各≥98%；Linux/macOS可移植三项各≥98%，不可移植明细保留且需证据，未验收不计通过。 |

## 三平台的原生实现评估

**Windows**：宿主已有Lumina.Forms.Control的原生HWND并挂到ToolStrip内部，ControlHost拥有托管Control而不是为每次布局重建；WM_COMMAND/NOTIFY、焦点/验证与条目事件转发。在HMENU不能任意嵌入编辑控件的情况下，需要标准ToolStripDropDown原生子控件表面，不能禁用就算支持。

**Linux / GTK4**：GtkWidget挂靠GtkBox或GtkPopover的原生容器；公开仍是ControlHost.Control，不泄漏GtkWidget。g_object引用、父对象销毁与托管Dispose统一；GTK菜单模型不支持任意控件时使用原生GtkPopover子控件而非虚构菜单项。

**macOS / AppKit**：Control对应NSView挂到工具栏自定义NSToolbarItem.view或普通NSView容器；NSMenuItem.view可承载自定义原生view，但键盘/菜单追踪行为需实测。不能将单向设置属性称为完整宿主。

实施顺序是 Windows 标准API/状态/事件→该条目最小原生试验→平台映射与完整验收。三平台分别记录属性/事件/行为的分子分母；继承API不能漏记，签名命中、自动属性、控件可以显示或本页任务已写完都不算通过。原生系统绘制、原生容器承载库绘制、原生控件组合应准确区分；公共业务行为难映射时仍然是待实现需求，不自动从可移植分母剔除。

各平台最少覆盖：代码与用户动作双向同步，Owner/Parent/overflow关系，事件顺序/取消/重入，键盘/助记键/输入法（适用时），DPI/RTL/本地化，无障碍，静态Designer和资源，AOT运行，移除与Dispose。没有完整证据时保持🟡/⬜，不发布已达标百分比。

## 内部输入/显示宿主的任务归属

固定引用程序集包含以下非公开实现类型；它们的行为进入已有公开控件任务，不创建新的公开类型或新的兼容率分母：

| 非公开精确类型 | 责任与阶段 |
|---|---|
| `System.Windows.Forms.ToolStripTextBox+ToolStripTextBoxControl` | 归 ToolStripTextBox 与本页事件/焦点/验证任务，P1/P2。 |
| `System.Windows.Forms.ToolStripComboBox+ToolStripComboBoxControl` | 归 ToolStripComboBox 的选择/编辑/菜单键盘协作，P1/P2。 |
| `System.Windows.Forms.ToolStripProgressBar+ToolStripProgressBarControl` | 归 ToolStripProgressBar 的范围/动画/布局/释放，P1/P2。 |
| `System.Windows.Forms.ToolStripNumericUpDown`、`System.Windows.Forms.ToolStripNumericUpDown+ToolStripNumericUpDownControl` | 标准程序集内部的数值宿主细节，归 NumericUpDown 与本页 ControlHost 的输入/精度/事件/释放，P2；不新增公开 ToolStripNumericUpDown。 |

`Control.ControlAccessibleObject` 等 AccessibleObject 派生辅助类型也不是 Control。公开辅助类型仍通过 [附属类型契约](../contracts.md) CNS-009 和各控件 AccessibilityObject 验收，不能因为不单建控件页就遗漏其成员。

## 修改历史

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 尚无该类型实现文件的提交历史；现有相似宿主行为不能代替标准类型实现。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

## 官方依据

- [WinForms ToolStripControlHost API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripcontrolhost?view=windowsdesktop-10.0)，精确签名以本机10.0.11引用程序集生成的清单为准。
- [ToolStrip 控件概述](https://learn.microsoft.com/dotnet/desktop/winforms/controls/toolstrip-control-overview)。
- [Windows 原生参考](https://learn.microsoft.com/windows/win32/winmsg/windows)。
- [GTK4 Box](https://docs.gtk.org/gtk4/class.Box.html)。
- [AppKit nstoolbaritem](https://developer.apple.com/documentation/appkit/nstoolbaritem)。
