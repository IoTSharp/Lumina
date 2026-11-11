# 逐控件与公共任务索引

更新：2026-11-11，用户已授权恢复 M0 公共基础、Application 与项目收敛代码。**本索引是当前实施状态入口，控件/公共任务页是状态的唯一维护来源**。完成的明确子范围在同页“已完成任务”标记；ID、状态与范围保留，父项不随子项完成。按日期/批次的变更历史统一放在根目录 [CHANGELOG](../../CHANGELOG.md)。实施列和 [tasks.csv](tasks.csv)仍由现有工具生成。

[剩余路线](../../ROADMAP.md) · [依赖顺序](execution.md) · [公共基础](foundations.md) · [附属契约](contracts.md) · [平台闸门](platforms.md) · [仓库任务](project-consolidation.md) · [验证门槛](validation.md) · [变更历史](../../CHANGELOG.md) · [文档导航](../index.md)

✅=明确范围代码完成；🧪=指定平台/范围验收通过；🚧=实施中；🟡=局部实现/未完整验收；⬜=待实现或待验；🔎=待验证；🚫=平台差异，不能算完成。当前无整类型开发完成行，也没有控件达到可宣称的完整 98% 验收。

下表“当前类型”和成员数来自固定 10.0.11 库存快照，存在只代表同名类型；包括 TextBoxBase 在内的后续代码变化以实施列和任务页为准，不手改历史库存。187 独立页覆盖控件及其他 UI/组件对象，不等于已实现 187 个控件。1,095 类型、61,240 成员记录和 2,204 任务 ID 均保留。

表中完成 ID 用于定位已完成范围；完整定义与剩余边界在对应任务页。变更历史统一放在根目录 CHANGELOG。任务数不计算兼容率，Windows/Linux/macOS 验收单列。

## 基础与继承链

| 类型与独立任务 | 实施阶段 | 当前类型 | 公开属性 / 事件 / 方法 | Windows / Linux / macOS验收 | 代码实施与已完成子任务 |
| --- | --- | --- | --- | --- | --- |
| [Control](controls/Control.md) | M0→M1 | 🟡 存在 | 84 / 72 / 74 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ Control-01a、Control-01b、Control-01c、Control-02a、Control-02b、Control-02c、Control-03a、Control-04a、Control-04b、Control-04c、Control-05a、Control-05b、Control-05c、Control-06a、Control-07a、Control-07b、Control-08a、Control-08b、Control-08c、Control-09a、Control-09b、Control-09c、Control-09d、Control-10a、Control-10b、Control-10c、Control-11a、Control-11b、Control-11c、Control-12a、Control-12b、Control-12c、Control-12d、Control-12e、Control-12f、Control-12g、Control-12h、Control-12i、Control-12j、Control-12k、Control-12l、Control-12m、Control-13a、Control-13b、Control-14a、Control-14b、Control-14c、Control-17a、Control-17b、Control-17c、Control-17d、Control-17e、Control-17f、Control-17g；其余任务未完成 |
| [Application](controls/Application.md) | M0→M1 | 🟡 存在 | 24 / 6 / 24 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ Application-01a、Application-02、Application-05、Application-06a、Application-08a、Application-11a、Application-11b、Application-12a；其余任务未完成 |
| [ApplicationConfiguration](controls/ApplicationConfiguration.md) | M1 | 🟡 SDK生成契约 | 见消费项目生成源码 | ⬜ / ⬜ / ⬜ | ⬜ 开发任务待实现；已有类型待逐项核实；暂无独立任务完成记录 |
| [ApplicationContext](controls/ApplicationContext.md) | M1 | ⬜ 缺失 | 2 / 1 / 2 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ ApplicationContext-01、ApplicationContext-02、ApplicationContext-03、ApplicationContext-05、ApplicationContext-06；其余任务未完成 |
| [WindowsFormsSynchronizationContext](controls/WindowsFormsSynchronizationContext.md) | M0 | 🟡 存在 | 2 / 0 / 10 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ WindowsFormsSynchronizationContext-01a、WindowsFormsSynchronizationContext-01b、WindowsFormsSynchronizationContext-06a；其余任务未完成 |
| [ScrollableControl](controls/ScrollableControl.md) | M1 | 🟡 存在 | 91 / 73 / 76 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ ScrollableControl-01a、ScrollableControl-01b、ScrollableControl-02a、ScrollableControl-03a、ScrollableControl-04a、ScrollableControl-05a、ScrollableControl-06a、ScrollableControl-07a、ScrollableControl-08a；其余任务未完成 |
| [ContainerControl](controls/ContainerControl.md) | M1 | 🟡 存在 | 97 / 74 / 81 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ ContainerControl-01、ContainerControl-02a、ContainerControl-02b、ContainerControl-03a、ContainerControl-05a、ContainerControl-06a、ContainerControl-06b、ContainerControl-06c、ContainerControl-06d；其余任务未完成 |
| [UserControl](controls/UserControl.md) | M1 | 🟡 存在 | 99 / 75 / 81 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ UserControl-01a、UserControl-02a、UserControl-02b、UserControl-02c、UserControl-02d、UserControl-03a、UserControl-05a；其余任务未完成 |
| [Form](controls/Form.md) | M0→M1 | 🟡 存在 | 143 / 99 / 95 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ Form-01、Form-06a、Form-07a、Form-08a、Form-10a、Form-10b、Form-12a、Form-12b、Form-12c、Form-12d、Form-16a、Form-16b、Form-16c；其余任务未完成 |
| [ButtonBase](controls/ButtonBase.md) | M1 | ⬜ 缺失 | 99 / 75 / 74 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ ButtonBase-001；其余任务未完成 |
| [TextBoxBase](controls/TextBoxBase.md) | M1 | ⬜ 缺失 | 100 / 78 / 91 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ TextBoxBase-001a、TextBoxBase-002a、TextBoxBase-002b、TextBoxBase-002c、TextBoxBase-002d、TextBoxBase-003a、TextBoxBase-003b、TextBoxBase-003c、TextBoxBase-003d、TextBoxBase-003e、TextBoxBase-003f、TextBoxBase-003g、TextBoxBase-003h、TextBoxBase-005a、TextBoxBase-005b、TextBoxBase-005c、TextBoxBase-005d、TextBoxBase-005e、TextBoxBase-005f、TextBoxBase-007a、TextBoxBase-007b、TextBoxBase-007c、TextBoxBase-007d、TextBoxBase-007e、TextBoxBase-008a、TextBoxBase-008b、TextBoxBase-008c、TextBoxBase-009a；其余任务未完成 |
| [ListControl](controls/ListControl.md) | M1 | ⬜ 缺失 | 92 / 80 / 75 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [UpDownBase](controls/UpDownBase.md) | M1 | ⬜ 缺失 | 103 / 74 / 84 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [ScrollBar](controls/ScrollBar.md) | M1 | ⬜ 缺失 | 90 / 74 / 74 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |

## 输入与显示

| 类型与独立任务 | 实施阶段 | 当前类型 | 公开属性 / 事件 / 方法 | Windows / Linux / macOS验收 | 代码实施与已完成子任务 |
| --- | --- | --- | --- | --- | --- |
| [Button](controls/Button.md) | M2 | 🟡 存在 | 101 / 75 / 76 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ Button-001a、Button-002a、Button-002b、Button-003a、Button-004a、Button-005a；其余任务未完成 |
| [CheckBox](controls/CheckBox.md) | M2 | 🟡 存在 | 105 / 78 / 74 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ CheckBox-004a；其余任务未完成 |
| [RadioButton](controls/RadioButton.md) | M2 | 🟡 存在 | 103 / 77 / 75 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ RadioButton-003a；其余任务未完成 |
| [Label](controls/Label.md) | M2 | 🟡 存在 | 98 / 73 / 74 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [LinkLabel](controls/LinkLabel.md) | M2 | 🟡 存在 | 106 / 74 / 74 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [TextBox](controls/TextBox.md) | M2 | 🟡 存在 | 110 / 79 / 92 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ TextBox-001a、TextBox-003a、TextBox-003b、TextBox-003c、TextBox-004a、TextBox-004b、TextBox-005a、TextBox-007a、TextBox-007b、TextBox-010a；其余任务未完成 |
| [RichTextBox](controls/RichTextBox.md) | M2 | 🟡 存在 | 127 / 85 / 107 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ RichTextBox-001a、RichTextBox-002a、RichTextBox-002b、RichTextBox-002c、RichTextBox-003a、RichTextBox-003b、RichTextBox-003c、RichTextBox-003d、RichTextBox-003e、RichTextBox-003f、RichTextBox-004a、RichTextBox-004b、RichTextBox-004c、RichTextBox-004d、RichTextBox-005a、RichTextBox-005b、RichTextBox-005c、RichTextBox-005d、RichTextBox-007a、RichTextBox-008a、RichTextBox-011a；其余任务未完成 |
| [MaskedTextBox](controls/MaskedTextBox.md) | M2 | 🟡 存在 | 123 / 83 / 92 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ MaskedTextBox-001；其余任务未完成 |
| [NumericUpDown](controls/NumericUpDown.md) | M2 | 🟡 存在 | 111 / 75 / 86 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [DomainUpDown](controls/DomainUpDown.md) | M2 | ⬜ 缺失 | 108 / 75 / 84 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DateTimePicker](controls/DateTimePicker.md) | M2 | 🟡 存在 | 103 / 77 / 74 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [MonthCalendar](controls/MonthCalendar.md) | M2 | 🟡 存在 | 106 / 75 / 90 | ⬜ / ⬜ / ⬜ | ⬜ 开发任务待实现；已有类型待逐项核实；暂无独立任务完成记录 |
| [ProgressBar](controls/ProgressBar.md) | M2 | 🟡 存在 | 91 / 73 / 76 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [TrackBar](controls/TrackBar.md) | M2 | ⬜ 缺失 | 93 / 75 / 77 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [HScrollBar](controls/HScrollBar.md) | M2 | ⬜ 缺失 | 90 / 74 / 74 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [VScrollBar](controls/VScrollBar.md) | M2 | ⬜ 缺失 | 90 / 74 / 74 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [PictureBox](controls/PictureBox.md) | M2 | 🟡 存在 | 91 / 75 / 79 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ PictureBox-008a；其余任务未完成 |

## 容器与布局

| 类型与独立任务 | 实施阶段 | 当前类型 | 公开属性 / 事件 / 方法 | Windows / Linux / macOS验收 | 代码实施与已完成子任务 |
| --- | --- | --- | --- | --- | --- |
| [Panel](controls/Panel.md) | M2 | 🟡 存在 | 93 / 73 / 76 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ Panel-01a、Panel-01b、Panel-02a、Panel-03a；其余任务未完成 |
| [GroupBox](controls/GroupBox.md) | M2 | 🟡 存在 | 87 / 72 / 74 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ GroupBox-07a；其余任务未完成 |
| [FlowLayoutPanel](controls/FlowLayoutPanel.md) | M2 | 🟡 存在 | 95 / 73 / 78 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ FlowLayoutPanel-06a、FlowLayoutPanel-06b；其余任务未完成 |
| [TableLayoutPanel](controls/TableLayoutPanel.md) | M2 | 🟡 存在 | 100 / 74 / 90 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ TableLayoutPanel-06a、TableLayoutPanel-06b；其余任务未完成 |
| [TabControl](controls/TabControl.md) | M2 | 🟡 存在 | 99 / 79 / 82 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ TabControl-05a、TabControl-07a、TabControl-08a；其余任务未完成 |
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
| [TreeView](controls/TreeView.md) | M2b | 🟡 存在 | 113 / 88 / 84 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ TreeView-01；其余任务未完成 |
| [TreeNode](controls/TreeNode.md) | M3 | 🟡 存在 | 33 / 0 / 13 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [TreeNodeCollection](controls/TreeNodeCollection.md) | M3 | 🟡 存在 | 4 / 0 / 26 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ TreeNodeCollection-03、TreeNodeCollection-09；其余任务未完成 |
| [ListViewItem](controls/ListViewItem.md) | M0→M2b | 🟡 存在 | 22 / 0 / 8 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ ListViewItem-01a、ListViewItem-01b、ListViewItem-01c、ListViewItem-04a、ListViewItem-04b、ListViewItem-06a；其余任务未完成 |
| [ColumnHeader](controls/ColumnHeader.md) | M3 | 🟡 存在 | 13 / 1 / 4 | ⬜ / ⬜ / ⬜ | ⬜ 开发任务待实现；已有类型待逐项核实；暂无独立任务完成记录 |
| [ListViewGroup](controls/ListViewGroup.md) | M3 | ⬜ 缺失 | 13 / 0 / 1 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
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
| [ImageList](controls/ImageList.md) | M3 | ⬜ 缺失 | 10 / 2 / 5 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [Timer](controls/Timer.md) | M3 | ⬜ 缺失 | 5 / 2 / 4 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [PropertyGrid](controls/PropertyGrid.md) | M4 | ⬜ 缺失 | 131 / 79 / 85 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGrid](controls/DataGrid.md) | M4 | ⬜ 缺失 | 124 / 86 / 102 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [DataGridTextBox](controls/DataGridTextBox.md) | M4 | ⬜ 缺失 | 111 / 79 / 93 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |

## 菜单、工具条与条目

| 类型与独立任务 | 实施阶段 | 当前类型 | 公开属性 / 事件 / 方法 | Windows / Linux / macOS验收 | 代码实施与已完成子任务 |
| --- | --- | --- | --- | --- | --- |
| [ToolStrip](controls/ToolStrip.md) | M2 | 🟡 存在 | 114 / 82 / 80 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ ToolStrip-05a、ToolStrip-05b、ToolStrip-05c；其余任务未完成 |
| [MenuStrip](controls/MenuStrip.md) | M2 | 🟡 存在 | 115 / 84 / 80 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；✅ MenuStrip-01a、MenuStrip-01b、MenuStrip-04a、MenuStrip-07a；其余任务未完成 |
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
| [ThreadExceptionDialog](controls/ThreadExceptionDialog.md) | M4 | ⬜ 缺失 | 143 / 99 / 95 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
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
| [ListViewGroupCollection](controls/ListViewGroupCollection.md) | M2b→M3 | ⬜ 缺失 | 3 / 0 / 12 | ⬜ / ⬜ / ⬜ | 🟡 部分完成；暂无独立任务完成记录 |
| [ListViewInsertionMark](controls/ListViewInsertionMark.md) | M2b→M3 | ⬜ 缺失 | 4 / 0 / 1 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [GridItem](controls/GridItem.md) | M4 | ⬜ 缺失 | 9 / 0 / 1 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [GridItemCollection](controls/GridItemCollection.md) | M4 | ⬜ 缺失 | 3 / 0 / 1 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |
| [PropertyTab](controls/PropertyTab.md) | M4 | ⬜ 缺失 | 4 / 0 / 6 | ⬜ / ⬜ / ⬜ | ⬜ 待实现；暂无独立任务完成记录 |

## 全量数据与维护

[固定基线/指纹](api/baseline.json) · [全部类型](api/types.json) · [页与完整类型映射](api/page-map.csv) · [成员](api/members.csv) · [接口](api/interfaces.csv) · [责任映射](api/ownership.csv)

维护顺序：复核原任务页中的范围/状态 → 标记已完成任务并更新根目录 CHANGELOG → 执行 `tools/compatibility/Validate-Roadmap.ps1 -ExportTasks` → 普通校验。完成行仍位于原控件/公共页，现有导出读取未完与完成两部分，tasks.csv 保留全量台账。不要手工改生成列，也不要移除未完任务或重新编号。

静态库存、实施状态与验收证据各自独立。[98% 计量](coverage.md)、[行为目录](behavior-catalog.md)、[2026-10-15 范围审计](scope-audit.md)保留完整基线。历史批次与早期状态纠正见[批次索引](batches/README.md)，不再在本页重复复制。
