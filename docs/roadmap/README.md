# 逐控件任务索引

更新：2026-10-16。正文按控件族组织，实施按[执行顺序](execution.md)的依赖排序。每页包含独立任务、三平台原生方案、真实修改历史；精确属性/事件/方法和继承成员在API附件。批次[001～026](batches/026.md)的明确子范围代码完成；完整Control专项接续[027坐标、捕获与鼠标输入](batches/027.md)，继续取得Windows实际验收并完成Linux/macOS对应代码。017后停止原实施轮次是历史边界，不能当作当前Control专项已停止；无关控件循环和暂停巡检仍不恢复。父任务、设计器资源和三平台整控件验收保持未完成。每批按真实证据整理受影响路线图并重生成[任务索引](tasks.csv)，不由数量或基类完成推导整控件完成。

[简明路线图](../../ROADMAP.md) · [遗漏审计](scope-audit.md) · [98%口径](coverage.md) · [公共基础](foundations.md) · [行为场景](behavior-catalog.md) · [平台闸门](platforms.md) · [项目整合](project-consolidation.md) · [附属类型](contracts.md) · [全部任务台账](tasks.csv) · [验证](validation.md)

✅=代码完成；🧪=指定范围验收通过；🚧=正在编码；🟡=存在代码/局部实现，未完整验收；⬜=缺失/待验收；🔎=待验证；🚫=平台不等价，不能算完成。下表“存在”只指同名类型。**目前没有控件经完整逐项证据证明达到98%**，两套原生平台也尚未实现。

每个类型另列“代码实施与已完成子任务”：✅ 后的任务 ID 表示该明确子范围代码完成，整行仍为 🟡 时该控件其余任务尚未完成。已有同名类型但没有独立完成记录的控件明确写待实现/待核实；基类任务不会自动算作派生控件完成。Windows/Linux/macOS 验收列独立保留。本列由已复核的控件任务页生成，`Validate-Roadmap.ps1 -ExportTasks` 与 tasks.csv 同步更新；普通校验会报告陈旧行，不由类型存在或测试数量推断完成。

本轮有187份独立类型文档，覆盖全部69个公开Control类型（62个非抽象、7个抽象，含Design命名空间），以及ToolStripItem、CommonDialog、Menu、非可视Component、TaskDialog与表格附属对象。ComponentEditorPage为抽象，不算第63个具体控件。全1095个类型含附属枚举/委托/接口等，由[责任映射](api/ownership.csv)归属；不能把187页当作已实现187个控件。

## 基础与继承链

| 类型与独立任务 | 实施阶段 | 当前类型 | 公开属性 / 事件 / 方法 | Windows / Linux / macOS验收 | 代码实施与已完成子任务 |
| --- | --- | --- | --- | --- | --- |
| [Control](controls/Control.md) | M0→M1 | 🟡 存在 | 84 / 72 / 74 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ Control-01a、Control-01b、Control-01c、Control-02a、Control-02b、Control-02c、Control-03a、Control-04a、Control-04b、Control-04c、Control-05a、Control-05b、Control-05c、Control-06a、Control-07a、Control-07b、Control-08a、Control-08b、Control-08c、Control-09a、Control-09b、Control-09c、Control-09d、Control-10a、Control-10b、Control-10c、Control-11a、Control-11b、Control-14a、Control-14b、Control-17a、Control-17b；其余任务未完成 |
| [Application](controls/Application.md) | M0→M1 | 🟡 存在 | 24 / 6 / 24 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ Application-01a、Application-02、Application-08a；其余任务未完成 |
| [ApplicationConfiguration](controls/ApplicationConfiguration.md) | M1 | 🟡 SDK生成契约 | 见消费项目生成源码 | ⬜ / ⬜ / ⬜ | ⬜ 开发任务待实现；已有类型待逐项核实；暂无独立任务完成记录 |
| [ApplicationContext](controls/ApplicationContext.md) | M1 | ⬜ 缺失 | 2 / 1 / 2 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [WindowsFormsSynchronizationContext](controls/WindowsFormsSynchronizationContext.md) | M0 | 🟡 存在 | 2 / 0 / 10 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ WindowsFormsSynchronizationContext-01a、WindowsFormsSynchronizationContext-01b、WindowsFormsSynchronizationContext-06a；其余任务未完成 |
| [ScrollableControl](controls/ScrollableControl.md) | M1 | 🟡 存在 | 91 / 73 / 76 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ ScrollableControl-01a；其余任务未完成 |
| [ContainerControl](controls/ContainerControl.md) | M1 | 🟡 存在 | 97 / 74 / 81 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ ContainerControl-01；其余任务未完成 |
| [UserControl](controls/UserControl.md) | M1 | 🟡 存在 | 99 / 75 / 81 | ⬜ / ⬜ / ⬜ | ⬜ 开发任务待实现；已有类型待逐项核实；暂无独立任务完成记录 |
| [Form](controls/Form.md) | M0→M1 | 🟡 存在 | 143 / 99 / 95 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ Form-01、Form-06a、Form-10a、Form-10b、Form-16a、Form-16b、Form-16c；其余任务未完成 |
| [ButtonBase](controls/ButtonBase.md) | M1 | ⬜ 缺失 | 99 / 75 / 74 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [TextBoxBase](controls/TextBoxBase.md) | M1 | ⬜ 缺失 | 100 / 78 / 91 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [ListControl](controls/ListControl.md) | M1 | ⬜ 缺失 | 92 / 80 / 75 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [UpDownBase](controls/UpDownBase.md) | M1 | ⬜ 缺失 | 103 / 74 / 84 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [ScrollBar](controls/ScrollBar.md) | M1 | ⬜ 缺失 | 90 / 74 / 74 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |

## 输入与显示

| 类型与独立任务 | 实施阶段 | 当前类型 | 公开属性 / 事件 / 方法 | Windows / Linux / macOS验收 | 代码实施与已完成子任务 |
| --- | --- | --- | --- | --- | --- |
| [Button](controls/Button.md) | M2 | 🟡 存在 | 101 / 75 / 76 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [CheckBox](controls/CheckBox.md) | M2 | 🟡 存在 | 105 / 78 / 74 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [RadioButton](controls/RadioButton.md) | M2 | 🟡 存在 | 103 / 77 / 75 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [Label](controls/Label.md) | M2 | 🟡 存在 | 98 / 73 / 74 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [LinkLabel](controls/LinkLabel.md) | M2 | 🟡 存在 | 106 / 74 / 74 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [TextBox](controls/TextBox.md) | M2 | 🟡 存在 | 110 / 79 / 92 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ TextBox-010a；其余任务未完成 |
| [RichTextBox](controls/RichTextBox.md) | M2 | 🟡 存在 | 127 / 85 / 107 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [MaskedTextBox](controls/MaskedTextBox.md) | M2 | 🟡 存在 | 123 / 83 / 92 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [NumericUpDown](controls/NumericUpDown.md) | M2 | 🟡 存在 | 111 / 75 / 86 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [DomainUpDown](controls/DomainUpDown.md) | M2 | ⬜ 缺失 | 108 / 75 / 84 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DateTimePicker](controls/DateTimePicker.md) | M2 | 🟡 存在 | 103 / 77 / 74 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [MonthCalendar](controls/MonthCalendar.md) | M2 | 🟡 存在 | 106 / 75 / 90 | ⬜ / ⬜ / ⬜ | ⬜ 开发任务待实现；已有类型待逐项核实；暂无独立任务完成记录 |
| [ProgressBar](controls/ProgressBar.md) | M2 | 🟡 存在 | 91 / 73 / 76 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [TrackBar](controls/TrackBar.md) | M2 | ⬜ 缺失 | 93 / 75 / 77 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [HScrollBar](controls/HScrollBar.md) | M2 | ⬜ 缺失 | 90 / 74 / 74 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [VScrollBar](controls/VScrollBar.md) | M2 | ⬜ 缺失 | 90 / 74 / 74 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [PictureBox](controls/PictureBox.md) | M2 | 🟡 存在 | 91 / 75 / 79 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |

## 容器与布局

| 类型与独立任务 | 实施阶段 | 当前类型 | 公开属性 / 事件 / 方法 | Windows / Linux / macOS验收 | 代码实施与已完成子任务 |
| --- | --- | --- | --- | --- | --- |
| [Panel](controls/Panel.md) | M2 | 🟡 存在 | 93 / 73 / 76 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ Panel-03a；其余任务未完成 |
| [GroupBox](controls/GroupBox.md) | M2 | 🟡 存在 | 87 / 72 / 74 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ GroupBox-07a；其余任务未完成 |
| [FlowLayoutPanel](controls/FlowLayoutPanel.md) | M2 | 🟡 存在 | 95 / 73 / 78 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ FlowLayoutPanel-06a、FlowLayoutPanel-06b；其余任务未完成 |
| [TableLayoutPanel](controls/TableLayoutPanel.md) | M2 | 🟡 存在 | 100 / 74 / 90 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ TableLayoutPanel-06a、TableLayoutPanel-06b；其余任务未完成 |
| [TabControl](controls/TabControl.md) | M2 | 🟡 存在 | 99 / 79 / 82 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ TabControl-07a、TabControl-08a；其余任务未完成 |
| [TabPage](controls/TabPage.md) | M2 | 🟡 存在 | 97 / 73 / 77 | ⬜ / ⬜ / ⬜ | ⬜ 开发任务待实现；已有类型待逐项核实；暂无独立任务完成记录 |
| [SplitContainer](controls/SplitContainer.md) | M2 | 🟡 存在 | 111 / 76 / 85 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ SplitContainer-05a、SplitContainer-05b；其余任务未完成 |
| [Splitter](controls/Splitter.md) | M2 | 🟡 存在 | 88 / 74 / 74 | ⬜ / ⬜ / ⬜ | ⬜ 开发任务待实现；已有类型待逐项核实；暂无独立任务完成记录 |
| [SplitterPanel](controls/SplitterPanel.md) | M2 | ⬜ 缺失 | 93 / 73 / 76 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |

## 列表、绑定与业务数据

| 类型与独立任务 | 实施阶段 | 当前类型 | 公开属性 / 事件 / 方法 | Windows / Linux / macOS验收 | 代码实施与已完成子任务 |
| --- | --- | --- | --- | --- | --- |
| [ComboBox](controls/ComboBox.md) | M3 | 🟡 存在 | 112 / 88 / 84 | ⬜ / ⬜ / ⬜ | ⬜ 开发任务待实现；已有类型待逐项核实；暂无独立任务完成记录 |
| [ListBox](controls/ListBox.md) | M3 | 🟡 存在 | 112 / 83 / 88 | ⬜ / ⬜ / ⬜ | ⬜ 开发任务待实现；已有类型待逐项核实；暂无独立任务完成记录 |
| [CheckedListBox](controls/CheckedListBox.md) | M3 | ⬜ 缺失 | 117 / 84 / 92 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [ListView](controls/ListView.md) | M2b | 🟡 存在 | 126 / 95 / 94 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [TreeView](controls/TreeView.md) | M2b | 🟡 存在 | 113 / 88 / 84 | ⬜ / ⬜ / ⬜ | ⬜ 开发任务待实现；已有类型待逐项核实；暂无独立任务完成记录 |
| [TreeNode](controls/TreeNode.md) | M3 | 🟡 存在 | 33 / 0 / 13 | ⬜ / ⬜ / ⬜ | ⬜ 开发任务待实现；已有类型待逐项核实；暂无独立任务完成记录 |
| [TreeNodeCollection](controls/TreeNodeCollection.md) | M3 | 🟡 存在 | 4 / 0 / 26 | ⬜ / ⬜ / ⬜ | ⬜ 开发任务待实现；已有类型待逐项核实；暂无独立任务完成记录 |
| [ListViewItem](controls/ListViewItem.md) | M0→M2b | 🟡 存在 | 22 / 0 / 8 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ ListViewItem-01a、ListViewItem-01b、ListViewItem-01c、ListViewItem-04a、ListViewItem-04b、ListViewItem-06a；其余任务未完成 |
| [ColumnHeader](controls/ColumnHeader.md) | M3 | 🟡 存在 | 13 / 1 / 4 | ⬜ / ⬜ / ⬜ | ⬜ 开发任务待实现；已有类型待逐项核实；暂无独立任务完成记录 |
| [ListViewGroup](controls/ListViewGroup.md) | M3 | ⬜ 缺失 | 13 / 0 / 1 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [ListViewSubItem](controls/ListViewSubItem.md) | M3 | 🟡 存在 | 7 / 0 / 2 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ ListViewSubItem-01a、ListViewSubItem-01b、ListViewSubItem-02a、ListViewSubItem-02b、ListViewSubItem-02c、ListViewSubItem-02d、ListViewSubItem-02e、ListViewSubItem-02f、ListViewSubItem-03a、ListViewSubItem-06a；其余任务未完成 |
| [Binding](controls/Binding.md) | M3 | ⬜ 缺失 | 14 / 3 / 2 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [BindingSource](controls/BindingSource.md) | M3 | ⬜ 缺失 | 30 / 10 / 34 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [BindingContext](controls/BindingContext.md) | M3 | ⬜ 缺失 | 3 / 1 / 3 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [CurrencyManager](controls/CurrencyManager.md) | M3 | ⬜ 缺失 | 6 / 8 / 8 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridView](controls/DataGridView.md) | M3 | ⬜ 缺失 | 157 / 190 / 116 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewComboBoxEditingControl](controls/DataGridViewComboBoxEditingControl.md) | M3 | ⬜ 缺失 | 118 / 88 / 88 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewTextBoxEditingControl](controls/DataGridViewTextBoxEditingControl.md) | M3 | ⬜ 缺失 | 116 / 79 / 96 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [BindingNavigator](controls/BindingNavigator.md) | M3 | ⬜ 缺失 | 124 / 83 / 84 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [ErrorProvider](controls/ErrorProvider.md) | M3 | ⬜ 缺失 | 11 / 2 / 12 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [ImageList](controls/ImageList.md) | M3 | ⬜ 缺失 | 10 / 2 / 5 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [Timer](controls/Timer.md) | M3 | ⬜ 缺失 | 5 / 2 / 4 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [PropertyGrid](controls/PropertyGrid.md) | M4 | ⬜ 缺失 | 131 / 79 / 85 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGrid](controls/DataGrid.md) | M4 | ⬜ 缺失 | 124 / 86 / 102 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridTextBox](controls/DataGridTextBox.md) | M4 | ⬜ 缺失 | 111 / 79 / 93 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |

## 菜单、工具条与条目

| 类型与独立任务 | 实施阶段 | 当前类型 | 公开属性 / 事件 / 方法 | Windows / Linux / macOS验收 | 代码实施与已完成子任务 |
| --- | --- | --- | --- | --- | --- |
| [ToolStrip](controls/ToolStrip.md) | M2 | 🟡 存在 | 114 / 82 / 80 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ ToolStrip-05a、ToolStrip-05b、ToolStrip-05c；其余任务未完成 |
| [MenuStrip](controls/MenuStrip.md) | M2 | 🟡 存在 | 115 / 84 / 80 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ MenuStrip-01a、MenuStrip-01b、MenuStrip-07a；其余任务未完成 |
| [StatusStrip](controls/StatusStrip.md) | M2 | 🟡 存在 | 116 / 82 / 80 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ StatusStrip-06a、StatusStrip-06b；其余任务未完成 |
| [ContextMenuStrip](controls/ContextMenuStrip.md) | M2 | 🟡 存在 | 124 / 86 / 88 | ⬜ / ⬜ / ⬜ | ⬜ 开发任务待实现；已有类型待逐项核实；暂无独立任务完成记录 |
| [ToolStripContainer](controls/ToolStripContainer.md) | M2 | ⬜ 缺失 | 106 / 74 / 81 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [ToolStripContentPanel](controls/ToolStripContentPanel.md) | M2 | ⬜ 缺失 | 95 / 75 / 76 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [ToolStripPanel](controls/ToolStripPanel.md) | M2 | ⬜ 缺失 | 103 / 75 / 88 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [ToolStripDropDown](controls/ToolStripDropDown.md) | M2 | ⬜ 缺失 | 121 / 86 / 88 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [ToolStripDropDownMenu](controls/ToolStripDropDownMenu.md) | M2 | ⬜ 缺失 | 123 / 86 / 88 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [ToolStripOverflow](controls/ToolStripOverflow.md) | M2 | ⬜ 缺失 | 121 / 86 / 88 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [ToolStripItem](controls/ToolStripItem.md) | M2 | 🟡 存在 | 61 / 32 / 19 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [ToolStripItemCollection](controls/ToolStripItemCollection.md) | M2 | 🟡 存在 | 4 / 0 / 22 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [ToolStripButton](controls/ToolStripButton.md) | M2 | 🟡 存在 | 64 / 34 / 19 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [ToolStripLabel](controls/ToolStripLabel.md) | M2 | 🟡 存在 | 67 / 32 / 19 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [ToolStripStatusLabel](controls/ToolStripStatusLabel.md) | M2 | 🟡 存在 | 71 / 32 / 19 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [ToolStripSeparator](controls/ToolStripSeparator.md) | M2 | 🟡 存在 | 61 / 32 / 19 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [ToolStripTextBox](controls/ToolStripTextBox.md) | M2 | 🟡 存在 | 87 / 48 / 37 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [ToolStripComboBox](controls/ToolStripComboBox.md) | M2 | 🟡 存在 | 84 / 46 / 29 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [ToolStripProgressBar](controls/ToolStripProgressBar.md) | M2 | 🟡 存在 | 73 / 42 / 22 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [ToolStripMenuItem](controls/ToolStripMenuItem.md) | M2 | 🟡 存在 | 73 / 38 / 21 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [ToolStripDropDownItem](controls/ToolStripDropDownItem.md) | M2 | 🟡 存在 | 66 / 36 / 21 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [ToolStripDropDownButton](controls/ToolStripDropDownButton.md) | M2 | 🟡 存在 | 67 / 36 / 21 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [ToolStripSplitButton](controls/ToolStripSplitButton.md) | M2 | 🟡 存在 | 75 / 39 / 24 | ⬜ / ⬜ / ⬜ | ⬜ 开发任务待实现；已有类型待逐项核实；暂无独立任务完成记录 |
| [ToolStripControlHost](controls/ToolStripControlHost.md) | M2 | ⬜ 缺失 | 65 / 41 / 20 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [ToolBar](controls/ToolBar.md) | M4 | ⬜ 缺失 | 95 / 74 / 74 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [StatusBar](controls/StatusBar.md) | M4 | ⬜ 缺失 | 87 / 74 / 74 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |

## 对话框与组件

| 类型与独立任务 | 实施阶段 | 当前类型 | 公开属性 / 事件 / 方法 | Windows / Linux / macOS验收 | 代码实施与已完成子任务 |
| --- | --- | --- | --- | --- | --- |
| [MessageBox](controls/MessageBox.md) | M2 | 🟡 存在 | 0 / 0 / 21 | ⬜ / ⬜ / ⬜ | ⬜ 开发任务待实现；已有类型待逐项核实；暂无独立任务完成记录 |
| [CommonDialog](controls/CommonDialog.md) | M2 | ⬜ 缺失 | 3 / 2 / 5 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [FileDialog](controls/FileDialog.md) | M2 | ⬜ 缺失 | 25 / 3 / 5 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [OpenFileDialog](controls/OpenFileDialog.md) | M2 | 🟡 存在 | 32 / 3 / 6 | ⬜ / ⬜ / ⬜ | ⬜ 开发任务待实现；已有类型待逐项核实；暂无独立任务完成记录 |
| [SaveFileDialog](controls/SaveFileDialog.md) | M2 | 🟡 存在 | 29 / 3 / 6 | ⬜ / ⬜ / ⬜ | ⬜ 开发任务待实现；已有类型待逐项核实；暂无独立任务完成记录 |
| [FolderBrowserDialog](controls/FolderBrowserDialog.md) | M2 | 🟡 存在 | 17 / 2 / 5 | ⬜ / ⬜ / ⬜ | ⬜ 开发任务待实现；已有类型待逐项核实；暂无独立任务完成记录 |
| [ColorDialog](controls/ColorDialog.md) | M2 | 🟡 存在 | 10 / 2 / 5 | ⬜ / ⬜ / ⬜ | ⬜ 开发任务待实现；已有类型待逐项核实；暂无独立任务完成记录 |
| [FontDialog](controls/FontDialog.md) | M2 | 🟡 存在 | 18 / 3 / 5 | ⬜ / ⬜ / ⬜ | ⬜ 开发任务待实现；已有类型待逐项核实；暂无独立任务完成记录 |
| [ToolTip](controls/ToolTip.md) | M2 | 🟡 存在 | 18 / 3 / 13 | ⬜ / ⬜ / ⬜ | ⬜ 开发任务待实现；已有类型待逐项核实；暂无独立任务完成记录 |
| [NotifyIcon](controls/NotifyIcon.md) | M2 | 🟡 存在 | 10 / 11 / 4 | ⬜ / ⬜ / ⬜ | ⬜ 开发任务待实现；已有类型待逐项核实；暂无独立任务完成记录 |
| [HelpProvider](controls/HelpProvider.md) | M4 | ⬜ 缺失 | 4 / 1 / 12 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |

## 打印与预览

| 类型与独立任务 | 实施阶段 | 当前类型 | 公开属性 / 事件 / 方法 | Windows / Linux / macOS验收 | 代码实施与已完成子任务 |
| --- | --- | --- | --- | --- | --- |
| [PrintDocument](controls/PrintDocument.md) | M4 | ⬜ 缺失 | 7 / 5 / 3 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [PrintDialog](controls/PrintDialog.md) | M4 | ⬜ 缺失 | 13 / 2 / 5 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [PageSetupDialog](controls/PageSetupDialog.md) | M4 | ⬜ 缺失 | 14 / 2 / 5 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [PrintPreviewControl](controls/PrintPreviewControl.md) | M4 | ⬜ 缺失 | 91 / 73 / 75 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [PrintPreviewDialog](controls/PrintPreviewDialog.md) | M4 | ⬜ 缺失 | 146 / 99 / 95 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |

## 系统、派生、历史与设计契约

| 类型与独立任务 | 实施阶段 | 当前类型 | 公开属性 / 事件 / 方法 | Windows / Linux / macOS验收 | 代码实施与已完成子任务 |
| --- | --- | --- | --- | --- | --- |
| [NativeWindow](controls/NativeWindow.md) | M4 | ⬜ 缺失 | 1 / 0 / 6 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [MdiClient](controls/MdiClient.md) | M4 | ⬜ 缺失 | 85 / 72 / 75 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [WebBrowserBase](controls/WebBrowserBase.md) | M4 | ⬜ 缺失 | 85 / 72 / 74 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [WebBrowser](controls/WebBrowser.md) | M4 | ⬜ 缺失 | 106 / 83 / 94 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [AxHost](controls/AxHost.md) | M4 | ⬜ 缺失 | 88 / 72 / 84 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [ThreadExceptionDialog](controls/ThreadExceptionDialog.md) | M4 | ⬜ 缺失 | 143 / 99 / 95 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [Clipboard](controls/Clipboard.md) | M4 | ⬜ 缺失 | 0 / 0 / 28 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataObject](controls/DataObject.md) | M4 | ⬜ 缺失 | 0 / 0 / 34 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DragEventArgs](controls/DragEventArgs.md) | M4 | ⬜ 缺失 | 9 / 0 / 0 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [AccessibleObject](controls/AccessibleObject.md) | M4 | ⬜ 缺失 | 10 / 0 / 11 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [ComponentEditorForm](controls/ComponentEditorForm.md) | M4 | ⬜ 缺失 | 143 / 99 / 99 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [ComponentEditorPage](controls/ComponentEditorPage.md) | M4 | ⬜ 缺失 | 96 / 73 / 86 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |

## 补齐：绑定与基础契约

| 类型与独立任务 | 实施阶段 | 当前类型 | 公开属性 / 事件 / 方法 | Windows / Linux / macOS验收 | 代码实施与已完成子任务 |
| --- | --- | --- | --- | --- | --- |
| [BindableComponent](controls/BindableComponent.md) | M1 | ⬜ 缺失 | 4 / 2 / 2 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [BindingManagerBase](controls/BindingManagerBase.md) | M1→M3 | ⬜ 缺失 | 5 / 5 / 7 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [BindingsCollection](controls/BindingsCollection.md) | M1→M3 | ⬜ 缺失 | 5 / 2 / 2 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [ControlBindingsCollection](controls/ControlBindingsCollection.md) | M1→M3 | ⬜ 缺失 | 9 / 2 / 12 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [PropertyManager](controls/PropertyManager.md) | M1→M3 | ⬜ 缺失 | 5 / 5 / 7 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |

## 补齐：DataGridView行列、单元格与样式

| 类型与独立任务 | 实施阶段 | 当前类型 | 公开属性 / 事件 / 方法 | Windows / Linux / macOS验收 | 代码实施与已完成子任务 |
| --- | --- | --- | --- | --- | --- |
| [DataGridViewAdvancedBorderStyle](controls/DataGridViewAdvancedBorderStyle.md) | M3 | ⬜ 缺失 | 5 / 0 / 3 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewBand](controls/DataGridViewBand.md) | M3 | ⬜ 缺失 | 15 / 0 / 3 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewButtonCell](controls/DataGridViewButtonCell.md) | M3 | ⬜ 缺失 | 35 / 0 / 20 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewButtonColumn](controls/DataGridViewButtonColumn.md) | M3 | ⬜ 缺失 | 36 / 1 / 4 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewCell](controls/DataGridViewCell.md) | M3 | ⬜ 缺失 | 33 / 0 / 20 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewCellCollection](controls/DataGridViewCellCollection.md) | M3 | ⬜ 缺失 | 6 / 1 / 11 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewCellStyle](controls/DataGridViewCellStyle.md) | M3 | ⬜ 缺失 | 16 / 0 / 5 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewCheckBoxCell](controls/DataGridViewCheckBoxCell.md) | M3 | ⬜ 缺失 | 40 / 0 / 22 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewCheckBoxColumn](controls/DataGridViewCheckBoxColumn.md) | M3 | ⬜ 缺失 | 38 / 1 / 4 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewColumn](controls/DataGridViewColumn.md) | M3 | ⬜ 缺失 | 33 / 1 / 4 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewColumnCollection](controls/DataGridViewColumnCollection.md) | M3 | ⬜ 缺失 | 6 / 1 / 21 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewColumnHeaderCell](controls/DataGridViewColumnHeaderCell.md) | M3 | ⬜ 缺失 | 34 / 0 / 20 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewComboBoxCell](controls/DataGridViewComboBoxCell.md) | M3 | ⬜ 缺失 | 44 / 0 / 20 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewComboBoxColumn](controls/DataGridViewComboBoxColumn.md) | M3 | ⬜ 缺失 | 44 / 1 / 4 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewElement](controls/DataGridViewElement.md) | M3 | ⬜ 缺失 | 2 / 0 / 0 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewHeaderCell](controls/DataGridViewHeaderCell.md) | M3 | ⬜ 缺失 | 33 / 0 / 20 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewImageCell](controls/DataGridViewImageCell.md) | M3 | ⬜ 缺失 | 36 / 0 / 20 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewImageColumn](controls/DataGridViewImageColumn.md) | M3 | ⬜ 缺失 | 38 / 1 / 4 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewLinkCell](controls/DataGridViewLinkCell.md) | M3 | ⬜ 缺失 | 40 / 0 / 20 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewLinkColumn](controls/DataGridViewLinkColumn.md) | M3 | ⬜ 缺失 | 40 / 1 / 4 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewRow](controls/DataGridViewRow.md) | M3 | ⬜ 缺失 | 24 / 0 / 11 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewRowCollection](controls/DataGridViewRowCollection.md) | M3 | ⬜ 缺失 | 2 / 1 / 30 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewRowHeaderCell](controls/DataGridViewRowHeaderCell.md) | M3 | ⬜ 缺失 | 33 / 0 / 20 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewSelectedCellCollection](controls/DataGridViewSelectedCellCollection.md) | M3 | ⬜ 缺失 | 5 / 0 / 6 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewSelectedColumnCollection](controls/DataGridViewSelectedColumnCollection.md) | M3 | ⬜ 缺失 | 5 / 0 / 6 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewSelectedRowCollection](controls/DataGridViewSelectedRowCollection.md) | M3 | ⬜ 缺失 | 5 / 0 / 6 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewTextBoxCell](controls/DataGridViewTextBoxCell.md) | M3 | ⬜ 缺失 | 34 / 0 / 20 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewTextBoxColumn](controls/DataGridViewTextBoxColumn.md) | M3 | ⬜ 缺失 | 34 / 1 / 4 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridViewTopLeftHeaderCell](controls/DataGridViewTopLeftHeaderCell.md) | M3 | ⬜ 缺失 | 34 / 0 / 20 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |

## 补齐：TaskDialog原生任务对话框

| 类型与独立任务 | 实施阶段 | 当前类型 | 公开属性 / 事件 / 方法 | Windows / Linux / macOS验收 | 代码实施与已完成子任务 |
| --- | --- | --- | --- | --- | --- |
| [TaskDialog](controls/TaskDialog.md) | M2 | ⬜ 缺失 | 1 / 0 / 7 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [TaskDialogButton](controls/TaskDialogButton.md) | M2 | ⬜ 缺失 | 18 / 1 / 4 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [TaskDialogButtonCollection](controls/TaskDialogButtonCollection.md) | M2 | ⬜ 缺失 | 2 / 0 / 10 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [TaskDialogCommandLinkButton](controls/TaskDialogCommandLinkButton.md) | M2 | ⬜ 缺失 | 19 / 1 / 4 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [TaskDialogControl](controls/TaskDialogControl.md) | M2 | ⬜ 缺失 | 2 / 0 / 0 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [TaskDialogExpander](controls/TaskDialogExpander.md) | M2 | ⬜ 缺失 | 7 / 1 / 1 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [TaskDialogFootnote](controls/TaskDialogFootnote.md) | M2 | ⬜ 缺失 | 4 / 0 / 1 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [TaskDialogIcon](controls/TaskDialogIcon.md) | M2 | ⬜ 缺失 | 1 / 0 / 1 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [TaskDialogLinkClickedEventArgs](controls/TaskDialogLinkClickedEventArgs.md) | M2 | ⬜ 缺失 | 1 / 0 / 0 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [TaskDialogPage](controls/TaskDialogPage.md) | M2 | ⬜ 缺失 | 17 / 4 / 1 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [TaskDialogProgressBar](controls/TaskDialogProgressBar.md) | M2 | ⬜ 缺失 | 7 / 0 / 0 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [TaskDialogRadioButton](controls/TaskDialogRadioButton.md) | M2 | ⬜ 缺失 | 5 / 1 / 1 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [TaskDialogRadioButtonCollection](controls/TaskDialogRadioButtonCollection.md) | M2 | ⬜ 缺失 | 2 / 0 / 10 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [TaskDialogVerificationCheckBox](controls/TaskDialogVerificationCheckBox.md) | M2 | ⬜ 缺失 | 4 / 1 / 1 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |

## 补齐：旧菜单与历史占位契约

| 类型与独立任务 | 实施阶段 | 当前类型 | 公开属性 / 事件 / 方法 | Windows / Linux / macOS验收 | 代码实施与已完成子任务 |
| --- | --- | --- | --- | --- | --- |
| [ContextMenu](controls/ContextMenu.md) | M4 | ⬜ 缺失 | 10 / 3 / 8 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridBoolColumn](controls/DataGridBoolColumn.md) | M4 | ⬜ 缺失 | 15 / 12 / 3 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridColumnStyle](controls/DataGridColumnStyle.md) | M4 | ⬜ 缺失 | 11 / 9 / 3 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridTableStyle](controls/DataGridTableStyle.md) | M4 | ⬜ 缺失 | 24 / 21 / 15 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridTextBoxColumn](controls/DataGridTextBoxColumn.md) | M4 | ⬜ 缺失 | 14 / 9 / 3 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [GridColumnStylesCollection](controls/GridColumnStylesCollection.md) | M4 | ⬜ 缺失 | 7 / 1 / 12 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [GridTableStylesCollection](controls/GridTableStylesCollection.md) | M4 | ⬜ 缺失 | 6 / 1 / 9 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [MainMenu](controls/MainMenu.md) | M4 | ⬜ 缺失 | 9 / 2 / 8 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [Menu](controls/Menu.md) | M4 | ⬜ 缺失 | 8 / 1 / 6 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [MenuItem](controls/MenuItem.md) | M4 | ⬜ 缺失 | 25 / 6 / 11 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [MenuItemCollection](controls/MenuItemCollection.md) | M4 | ⬜ 缺失 | 4 / 0 / 17 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [StatusBarPanel](controls/StatusBarPanel.md) | M4 | ⬜ 缺失 | 14 / 1 / 4 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [ToolBarButton](controls/ToolBarButton.md) | M4 | ⬜ 缺失 | 16 / 1 / 2 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |

## 补齐：列表、属性窗格与条带附属对象

| 类型与独立任务 | 实施阶段 | 当前类型 | 公开属性 / 事件 / 方法 | Windows / Linux / macOS验收 | 代码实施与已完成子任务 |
| --- | --- | --- | --- | --- | --- |
| [ToolStripOverflowButton](controls/ToolStripOverflowButton.md) | M2 | ⬜ 缺失 | 67 / 36 / 21 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [ToolStripPanelRow](controls/ToolStripPanelRow.md) | M2 | ⬜ 缺失 | 10 / 1 / 3 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [ListViewGroupCollection](controls/ListViewGroupCollection.md) | M2b→M3 | ⬜ 缺失 | 3 / 0 / 12 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [ListViewInsertionMark](controls/ListViewInsertionMark.md) | M2b→M3 | ⬜ 缺失 | 4 / 0 / 1 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [GridItem](controls/GridItem.md) | M4 | ⬜ 缺失 | 9 / 0 / 1 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [GridItemCollection](controls/GridItemCollection.md) | M4 | ⬜ 缺失 | 3 / 0 / 1 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [PropertyTab](controls/PropertyTab.md) | M4 | ⬜ 缺失 | 4 / 0 / 6 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |

## 全量数据与维护

2026-10-16 · 批次026整理：按真实实现新增Control-05c/08c完成子范围，父项保留部分；Form-07按已有Close/取消/Dispose纠正为部分。修正本页引言仍停在023/024的过时接续点，并补验证页缺失的025历史摘要；历史失败、未完ID与固定分母不改，逐控件实施列由既有工具重新生成。

[固定基线/指纹](api/baseline.json)、[全部类型](api/types.json)、[独立页完整类型映射](api/page-map.csv)、[成员](api/members.csv)、[接口契约](api/interfaces.csv)。表中成员数含每类型的继承面，不能相加当全库去重数，更不能当支持率。全部方法重载与字段/构造/受保护扩展见附件。

实施时先更新对应类型文件的状态和修改历史，附实现提交、参考端/实现端场景及AOT证据；任务ID保持稳定。当前公开API、接口和平台适用性仍需逐条验收，不靠勾选大任务自动变绿。所有控件共性默认值/事件/异常场景按[行为目录](behavior-catalog.md)展开。

2026-10-16 · 批次 016 整理：按用户要求为全部 187 行补独立实施状态和已完成子任务列表，历史批次的明确子范围在对应类型行回标；未完成父项/平台/设备/设计器验收继续保留。M0/M1 与公共任务中遗漏的局部完成同步纠正，详细证据仍在原任务页及批次历史，未改变 API 分母或任务 ID。
