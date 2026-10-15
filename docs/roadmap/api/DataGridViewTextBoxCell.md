# DataGridViewTextBoxCell：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/DataGridViewTextBoxCell.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.DataGridViewTextBoxCell`；基类：`System.Windows.Forms.DataGridViewCell`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **34**，公开事件 **0**，公开方法 **20**；受保护扩展成员 **55**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-A71BC00BB9F5C12D | Constructor | `public DataGridViewTextBoxCell()` | `System.Windows.Forms.DataGridViewTextBoxCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0700221FF12771BE | Method | `public virtual System.Windows.Forms.DataGridViewAdvancedBorderStyle AdjustCellBorderStyle(System.Windows.Forms.DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyleInput, System.Windows.Forms.DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder, System.Boolean singleVerticalBorderAdded, System.Boolean singleHorizontalBorderAdded, System.Boolean isFirstDisplayedColumn, System.Boolean isFirstDisplayedRow)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3C9AF9FBA163BECC | Method | `public virtual System.Object Clone()` | `System.Windows.Forms.DataGridViewTextBoxCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A585C4B45E483D73 | Method | `public virtual System.Void DetachEditingControl()` | `System.Windows.Forms.DataGridViewTextBoxCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ABBE6D913C116EB2 | Method | `public System.Void Dispose()` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-09DA1C3DA946CAA9 | Method | `public System.Drawing.Rectangle GetContentBounds(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A85E4F37779EE184 | Method | `public System.Object GetEditedFormattedValue(System.Int32 rowIndex, System.Windows.Forms.DataGridViewDataErrorContexts context)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C3669B12304EE23 | Method | `public virtual System.Windows.Forms.ContextMenuStrip GetInheritedContextMenuStrip(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9D1E904AEE0B167F | Method | `public virtual System.Windows.Forms.DataGridViewElementStates GetInheritedState(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0BB90BD3E5A57207 | Method | `public virtual System.Windows.Forms.DataGridViewCellStyle GetInheritedStyle(System.Windows.Forms.DataGridViewCellStyle inheritedCellStyle, System.Int32 rowIndex, System.Boolean includeColors)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E693C0DCDB3E6685 | Method | `public virtual System.Void InitializeEditingControl(System.Int32 rowIndex, System.Object initialFormattedValue, System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle)` | `System.Windows.Forms.DataGridViewTextBoxCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-68424C6B5C80E284 | Method | `public virtual System.Boolean KeyEntersEditMode(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.DataGridViewTextBoxCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3D247ED66BD83A8A | Method | `public static System.Int32 MeasureTextHeight(System.Drawing.Graphics graphics, System.String text, System.Drawing.Font font, System.Int32 maxWidth, System.Windows.Forms.TextFormatFlags flags)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D03BABB7126FA77B | Method | `public static System.Int32 MeasureTextHeight(System.Drawing.Graphics graphics, System.String text, System.Drawing.Font font, System.Int32 maxWidth, System.Windows.Forms.TextFormatFlags flags, out System.Boolean widthTruncated)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-633269060BE8BA89 | Method | `public static System.Drawing.Size MeasureTextPreferredSize(System.Drawing.Graphics graphics, System.String text, System.Drawing.Font font, System.Single maxRatio, System.Windows.Forms.TextFormatFlags flags)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C6986A98B1EC102E | Method | `public static System.Drawing.Size MeasureTextSize(System.Drawing.Graphics graphics, System.String text, System.Drawing.Font font, System.Windows.Forms.TextFormatFlags flags)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0D19CE6A3DE484B3 | Method | `public static System.Int32 MeasureTextWidth(System.Drawing.Graphics graphics, System.String text, System.Drawing.Font font, System.Int32 maxHeight, System.Windows.Forms.TextFormatFlags flags)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-20E992233B9BDBD2 | Method | `public virtual System.Object ParseFormattedValue(System.Object formattedValue, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter formattedValueTypeConverter, System.ComponentModel.TypeConverter valueTypeConverter)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F78EAFA24C32EC40 | Method | `public virtual System.Void PositionEditingControl(System.Boolean setLocation, System.Boolean setSize, System.Drawing.Rectangle cellBounds, System.Drawing.Rectangle cellClip, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Boolean singleVerticalBorderAdded, System.Boolean singleHorizontalBorderAdded, System.Boolean isFirstDisplayedColumn, System.Boolean isFirstDisplayedRow)` | `System.Windows.Forms.DataGridViewTextBoxCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D8BEC7E4A5FCD8D8 | Method | `public virtual System.Drawing.Rectangle PositionEditingPanel(System.Drawing.Rectangle cellBounds, System.Drawing.Rectangle cellClip, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Boolean singleVerticalBorderAdded, System.Boolean singleHorizontalBorderAdded, System.Boolean isFirstDisplayedColumn, System.Boolean isFirstDisplayedRow)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9DB0485B1C50FD85 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.DataGridViewTextBoxCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CA1DE42A2A24B702 | Property | `System.Windows.Forms.AccessibleObject AccessibilityObject { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2F2E8C2FB78F1EB8 | Property | `System.Int32 ColumnIndex { public get; }` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-10BB08E7083AD573 | Property | `System.Drawing.Rectangle ContentBounds { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-43AECBD1B251B9EF | Property | `virtual System.Windows.Forms.ContextMenuStrip ContextMenuStrip { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7AA20E58B63E7C9B | Property | `System.Windows.Forms.DataGridView DataGridView { public get; }` | `System.Windows.Forms.DataGridViewElement` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-279D4D548A65D9F6 | Property | `virtual System.Object DefaultNewRowValue { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7B0EAE6F6F38B0CB | Property | `virtual System.Boolean Displayed { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B128C048377AC6E | Property | `System.Object EditedFormattedValue { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B0752E43699533F7 | Property | `virtual System.Type EditType { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F3D78E0D0530EA20 | Property | `System.Drawing.Rectangle ErrorIconBounds { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F245F104942AD08E | Property | `System.String ErrorText { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B387CDD95288991 | Property | `System.Object FormattedValue { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-239ABBEA2A96C799 | Property | `virtual System.Type FormattedValueType { public get; }` | `System.Windows.Forms.DataGridViewTextBoxCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E1D566B7F0D1C256 | Property | `virtual System.Boolean Frozen { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C9C0BE432ACF8B52 | Property | `System.Boolean HasStyle { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-32B04DC4E795EFA4 | Property | `System.Windows.Forms.DataGridViewElementStates InheritedState { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4F99621AE397D45E | Property | `System.Windows.Forms.DataGridViewCellStyle InheritedStyle { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-24DD5D6B2F56DD9D | Property | `System.Boolean IsInEditMode { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DB8730C39B4522AE | Property | `virtual System.Int32 MaxInputLength { public get; public set; }` | `System.Windows.Forms.DataGridViewTextBoxCell` DefaultValueAttribute(32767) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C72EC1C35228556A | Property | `System.Windows.Forms.DataGridViewColumn OwningColumn { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2432F7BE30CA0F69 | Property | `System.Windows.Forms.DataGridViewRow OwningRow { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4816F99766C44AA9 | Property | `System.Drawing.Size PreferredSize { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3178E4DE7A4941D6 | Property | `virtual System.Boolean ReadOnly { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-43C50456B93307F5 | Property | `virtual System.Boolean Resizable { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-12F8A14E0DC2FEB5 | Property | `System.Int32 RowIndex { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-528CE363E3442753 | Property | `virtual System.Boolean Selected { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BCE448CF4A92C9A2 | Property | `System.Drawing.Size Size { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3877E2650144F025 | Property | `virtual System.Windows.Forms.DataGridViewElementStates State { public get; }` | `System.Windows.Forms.DataGridViewElement` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-52CE465241A9EB41 | Property | `System.Windows.Forms.DataGridViewCellStyle Style { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A71AEEF97CD2878F | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B85CB5BEA8E38B9 | Property | `System.String ToolTipText { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-364942AA63965CAD | Property | `System.Object Value { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C134D98B430679F | Property | `virtual System.Type ValueType { public get; }` | `System.Windows.Forms.DataGridViewTextBoxCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B56D7B92B883E2E6 | Property | `virtual System.Boolean Visible { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-986CD090EDA38522 | Method | `protected virtual System.Drawing.Rectangle BorderWidths(System.Windows.Forms.DataGridViewAdvancedBorderStyle advancedBorderStyle)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3CA5E1B213A4DFC2 | Method | `protected virtual System.Boolean ClickUnsharesRow(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B4635F04AE43C4D | Method | `protected virtual System.Boolean ContentClickUnsharesRow(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5DECF9119518E6FB | Method | `protected virtual System.Boolean ContentDoubleClickUnsharesRow(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FE0FE221DABFAC75 | Method | `protected virtual System.Windows.Forms.AccessibleObject CreateAccessibilityInstance()` | `System.Windows.Forms.DataGridViewTextBoxCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B0CF4F8108F254F | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-12422AE7D233AB91 | Method | `protected virtual System.Boolean DoubleClickUnsharesRow(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B6608975E4FCE2B9 | Method | `protected virtual System.Boolean EnterUnsharesRow(System.Int32 rowIndex, System.Boolean throughMouseClick)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-19932D49ABDAF928 | Method | `protected virtual System.Void Finalize()` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-676EE89979E8BFCE | Method | `protected virtual System.Object GetClipboardContent(System.Int32 rowIndex, System.Boolean firstCell, System.Boolean lastCell, System.Boolean inFirstRow, System.Boolean inLastRow, System.String format)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5FA0CFF1C141B9AF | Method | `protected virtual System.Drawing.Rectangle GetContentBounds(System.Drawing.Graphics graphics, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewTextBoxCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D27372B92F6A83E1 | Method | `protected virtual System.Drawing.Rectangle GetErrorIconBounds(System.Drawing.Graphics graphics, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewTextBoxCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-36CC9C653698076B | Method | `protected internal virtual System.String GetErrorText(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-889D7F7F04F4AD60 | Method | `protected virtual System.Object GetFormattedValue(System.Object value, System.Int32 rowIndex, ref System.Windows.Forms.DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter valueTypeConverter, System.ComponentModel.TypeConverter formattedValueTypeConverter, System.Windows.Forms.DataGridViewDataErrorContexts context)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6463EB11150DB439 | Method | `protected virtual System.Drawing.Size GetPreferredSize(System.Drawing.Graphics graphics, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Int32 rowIndex, System.Drawing.Size constraintSize)` | `System.Windows.Forms.DataGridViewTextBoxCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3831D663BA4A15DE | Method | `protected virtual System.Drawing.Size GetSize(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-348AD8DB3E8BCB7B | Method | `protected virtual System.Object GetValue(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B7501F83CFFF098C | Method | `protected virtual System.Boolean KeyDownUnsharesRow(System.Windows.Forms.KeyEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-12F8A31731989D90 | Method | `protected virtual System.Boolean KeyPressUnsharesRow(System.Windows.Forms.KeyPressEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BC581C5D9B8BFAC0 | Method | `protected virtual System.Boolean KeyUpUnsharesRow(System.Windows.Forms.KeyEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2981CE2F02CBB0A3 | Method | `protected virtual System.Boolean LeaveUnsharesRow(System.Int32 rowIndex, System.Boolean throughMouseClick)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2186B987CB9DF83F | Method | `protected virtual System.Boolean MouseClickUnsharesRow(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A7841F6EB418CF2C | Method | `protected virtual System.Boolean MouseDoubleClickUnsharesRow(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-69AC62527B7F2D41 | Method | `protected virtual System.Boolean MouseDownUnsharesRow(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C31B9F9807B598F | Method | `protected virtual System.Boolean MouseEnterUnsharesRow(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1019748E330B7A9F | Method | `protected virtual System.Boolean MouseLeaveUnsharesRow(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BA51CCAE1828D09A | Method | `protected virtual System.Boolean MouseMoveUnsharesRow(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F7972D3DCCB9CE44 | Method | `protected virtual System.Boolean MouseUpUnsharesRow(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AD24EF95B1052EE3 | Method | `protected virtual System.Void OnClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-268B131243906944 | Method | `protected virtual System.Void OnContentClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-11A72A073FEE51FE | Method | `protected virtual System.Void OnContentDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-041E03D5AEE9CE8C | Method | `protected virtual System.Void OnDataGridViewChanged()` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D813CFB43FBC115C | Method | `protected virtual System.Void OnDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FCED08CC9664B037 | Method | `protected virtual System.Void OnEnter(System.Int32 rowIndex, System.Boolean throughMouseClick)` | `System.Windows.Forms.DataGridViewTextBoxCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E74BB8D5EE366E5 | Method | `protected virtual System.Void OnKeyDown(System.Windows.Forms.KeyEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1C1E7EB1B1A8BAC0 | Method | `protected virtual System.Void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1901794B0543F33E | Method | `protected virtual System.Void OnKeyUp(System.Windows.Forms.KeyEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-370DAED0C5A1AB9B | Method | `protected virtual System.Void OnLeave(System.Int32 rowIndex, System.Boolean throughMouseClick)` | `System.Windows.Forms.DataGridViewTextBoxCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7E83303F8FE37CFF | Method | `protected virtual System.Void OnMouseClick(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewTextBoxCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-97F5ED076AA0A2EA | Method | `protected virtual System.Void OnMouseDoubleClick(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-386F420CDB2BA608 | Method | `protected virtual System.Void OnMouseDown(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A796D36F08F384D2 | Method | `protected virtual System.Void OnMouseEnter(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4F85D8758FAC3A98 | Method | `protected virtual System.Void OnMouseLeave(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-75BB59B4B498C36C | Method | `protected virtual System.Void OnMouseMove(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F35CB9C3D346285 | Method | `protected virtual System.Void OnMouseUp(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-01BD981207E62390 | Method | `protected virtual System.Void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, System.Int32 rowIndex, System.Windows.Forms.DataGridViewElementStates cellState, System.Object value, System.Object formattedValue, System.String errorText, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Windows.Forms.DataGridViewAdvancedBorderStyle advancedBorderStyle, System.Windows.Forms.DataGridViewPaintParts paintParts)` | `System.Windows.Forms.DataGridViewTextBoxCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E41EFEDBAB56EF89 | Method | `protected virtual System.Void PaintBorder(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle bounds, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Windows.Forms.DataGridViewAdvancedBorderStyle advancedBorderStyle)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3CE7A21002432A6B | Method | `protected virtual System.Void PaintErrorIcon(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellValueBounds, System.String errorText)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AD27D4395F9956CA | Method | `protected System.Void RaiseCellClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6C64C5EF009D18EA | Method | `protected System.Void RaiseCellContentClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D718E35DAFC7F6B4 | Method | `protected System.Void RaiseCellContentDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-96F8193E434E7587 | Method | `protected System.Void RaiseCellValueChanged(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EEFA064EF66FC494 | Method | `protected System.Void RaiseDataError(System.Windows.Forms.DataGridViewDataErrorEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-63C3786B06E53B69 | Method | `protected System.Void RaiseMouseWheel(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C9D3E0FEAA16E04B | Method | `protected virtual System.Boolean SetValue(System.Int32 rowIndex, System.Object value)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
