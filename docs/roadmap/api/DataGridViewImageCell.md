# DataGridViewImageCell：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/DataGridViewImageCell.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.DataGridViewImageCell`；基类：`System.Windows.Forms.DataGridViewCell`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **36**，公开事件 **0**，公开方法 **20**；受保护扩展成员 **55**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-18048DB22FAE9090 | Constructor | `public DataGridViewImageCell()` | `System.Windows.Forms.DataGridViewImageCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CB8CEDA8441609B6 | Constructor | `public DataGridViewImageCell(System.Boolean valueIsIcon)` | `System.Windows.Forms.DataGridViewImageCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C023D9FB6D2E8377 | Method | `public virtual System.Windows.Forms.DataGridViewAdvancedBorderStyle AdjustCellBorderStyle(System.Windows.Forms.DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyleInput, System.Windows.Forms.DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder, System.Boolean singleVerticalBorderAdded, System.Boolean singleHorizontalBorderAdded, System.Boolean isFirstDisplayedColumn, System.Boolean isFirstDisplayedRow)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C06214C1BB51969C | Method | `public virtual System.Object Clone()` | `System.Windows.Forms.DataGridViewImageCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A0A74D4A67019CB | Method | `public virtual System.Void DetachEditingControl()` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-88B699A7E1BE62C6 | Method | `public System.Void Dispose()` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4FB95C8BF814CDD8 | Method | `public System.Drawing.Rectangle GetContentBounds(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D65FED2DE8501DB8 | Method | `public System.Object GetEditedFormattedValue(System.Int32 rowIndex, System.Windows.Forms.DataGridViewDataErrorContexts context)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9FDA2B1BDE2F615D | Method | `public virtual System.Windows.Forms.ContextMenuStrip GetInheritedContextMenuStrip(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-668677EF3A0C985F | Method | `public virtual System.Windows.Forms.DataGridViewElementStates GetInheritedState(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5E30E66384050045 | Method | `public virtual System.Windows.Forms.DataGridViewCellStyle GetInheritedStyle(System.Windows.Forms.DataGridViewCellStyle inheritedCellStyle, System.Int32 rowIndex, System.Boolean includeColors)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5CD28BFAF17C1230 | Method | `public virtual System.Void InitializeEditingControl(System.Int32 rowIndex, System.Object initialFormattedValue, System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4933A48C7C4AE39C | Method | `public virtual System.Boolean KeyEntersEditMode(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C4A5D167F3630547 | Method | `public static System.Int32 MeasureTextHeight(System.Drawing.Graphics graphics, System.String text, System.Drawing.Font font, System.Int32 maxWidth, System.Windows.Forms.TextFormatFlags flags)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FBB872702AE31CBD | Method | `public static System.Int32 MeasureTextHeight(System.Drawing.Graphics graphics, System.String text, System.Drawing.Font font, System.Int32 maxWidth, System.Windows.Forms.TextFormatFlags flags, out System.Boolean widthTruncated)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A1253753A445633C | Method | `public static System.Drawing.Size MeasureTextPreferredSize(System.Drawing.Graphics graphics, System.String text, System.Drawing.Font font, System.Single maxRatio, System.Windows.Forms.TextFormatFlags flags)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5034BF74AF35E735 | Method | `public static System.Drawing.Size MeasureTextSize(System.Drawing.Graphics graphics, System.String text, System.Drawing.Font font, System.Windows.Forms.TextFormatFlags flags)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3EB4C1438ADB0B3D | Method | `public static System.Int32 MeasureTextWidth(System.Drawing.Graphics graphics, System.String text, System.Drawing.Font font, System.Int32 maxHeight, System.Windows.Forms.TextFormatFlags flags)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D38247CCCC3222D4 | Method | `public virtual System.Object ParseFormattedValue(System.Object formattedValue, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter formattedValueTypeConverter, System.ComponentModel.TypeConverter valueTypeConverter)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5176CC6C22871168 | Method | `public virtual System.Void PositionEditingControl(System.Boolean setLocation, System.Boolean setSize, System.Drawing.Rectangle cellBounds, System.Drawing.Rectangle cellClip, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Boolean singleVerticalBorderAdded, System.Boolean singleHorizontalBorderAdded, System.Boolean isFirstDisplayedColumn, System.Boolean isFirstDisplayedRow)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FA32E45B5B62EA41 | Method | `public virtual System.Drawing.Rectangle PositionEditingPanel(System.Drawing.Rectangle cellBounds, System.Drawing.Rectangle cellClip, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Boolean singleVerticalBorderAdded, System.Boolean singleHorizontalBorderAdded, System.Boolean isFirstDisplayedColumn, System.Boolean isFirstDisplayedRow)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-186217E330D378D0 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.DataGridViewImageCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-12ADCABF469684FD | Property | `System.Windows.Forms.AccessibleObject AccessibilityObject { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E44FB69AF1C2857 | Property | `System.Int32 ColumnIndex { public get; }` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2707B5941101A13D | Property | `System.Drawing.Rectangle ContentBounds { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B5A1F1F47C76B0CF | Property | `virtual System.Windows.Forms.ContextMenuStrip ContextMenuStrip { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-18C7B8D8F7622DCB | Property | `System.Windows.Forms.DataGridView DataGridView { public get; }` | `System.Windows.Forms.DataGridViewElement` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5A059D40E4A59EF1 | Property | `virtual System.Object DefaultNewRowValue { public get; }` | `System.Windows.Forms.DataGridViewImageCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EDE899849D09E7E9 | Property | `System.String Description { public get; public set; }` | `System.Windows.Forms.DataGridViewImageCell` DefaultValueAttribute("") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CBA6F89A7BE6DAAA | Property | `virtual System.Boolean Displayed { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-76078E50E7EE93D2 | Property | `System.Object EditedFormattedValue { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AEB405F30F8B6125 | Property | `virtual System.Type EditType { public get; }` | `System.Windows.Forms.DataGridViewImageCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-99C1C4C8415323B2 | Property | `System.Drawing.Rectangle ErrorIconBounds { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-40ED8B21002D1303 | Property | `System.String ErrorText { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4FB9D6F09BB791FD | Property | `System.Object FormattedValue { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A838BA5080DDAFF7 | Property | `virtual System.Type FormattedValueType { public get; }` | `System.Windows.Forms.DataGridViewImageCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B567E46B5BDED03 | Property | `virtual System.Boolean Frozen { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F67723FEB7515CAA | Property | `System.Boolean HasStyle { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D6DC1B34BEF5B382 | Property | `System.Windows.Forms.DataGridViewImageCellLayout ImageLayout { public get; public set; }` | `System.Windows.Forms.DataGridViewImageCell` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-87D632D4E88A1618 | Property | `System.Windows.Forms.DataGridViewElementStates InheritedState { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-360612EFD9FAE63A | Property | `System.Windows.Forms.DataGridViewCellStyle InheritedStyle { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F16FB4BA6F7EE5F3 | Property | `System.Boolean IsInEditMode { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C5B3AB5E8F48D5A0 | Property | `System.Windows.Forms.DataGridViewColumn OwningColumn { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7E134D37EF95969A | Property | `System.Windows.Forms.DataGridViewRow OwningRow { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D05F3FBC1673A66D | Property | `System.Drawing.Size PreferredSize { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7EC511F85A21AF87 | Property | `virtual System.Boolean ReadOnly { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2700B83D3746DC38 | Property | `virtual System.Boolean Resizable { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C8CE06A52F650D84 | Property | `System.Int32 RowIndex { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ADEF18AAF102D809 | Property | `virtual System.Boolean Selected { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-58CB365CC2815248 | Property | `System.Drawing.Size Size { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-15BE955504FCF980 | Property | `virtual System.Windows.Forms.DataGridViewElementStates State { public get; }` | `System.Windows.Forms.DataGridViewElement` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AFEF27916FC5FBBA | Property | `System.Windows.Forms.DataGridViewCellStyle Style { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-86CEE3A926AD4FE5 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A3F9E5182F5F0937 | Property | `System.String ToolTipText { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-71AE5ED638B060FF | Property | `System.Object Value { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8C4A64696C038D4C | Property | `System.Boolean ValueIsIcon { public get; public set; }` | `System.Windows.Forms.DataGridViewImageCell` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5E29ADDE2BD5E969 | Property | `virtual System.Type ValueType { public get; public set; }` | `System.Windows.Forms.DataGridViewImageCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E02B842B30FB8F48 | Property | `virtual System.Boolean Visible { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-77DCFEDE5F542F37 | Method | `protected virtual System.Drawing.Rectangle BorderWidths(System.Windows.Forms.DataGridViewAdvancedBorderStyle advancedBorderStyle)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-01959D7431CD7A5B | Method | `protected virtual System.Boolean ClickUnsharesRow(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5C76AC02FB854A87 | Method | `protected virtual System.Boolean ContentClickUnsharesRow(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D67B11FDE3673316 | Method | `protected virtual System.Boolean ContentDoubleClickUnsharesRow(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-943299EB692108A1 | Method | `protected virtual System.Windows.Forms.AccessibleObject CreateAccessibilityInstance()` | `System.Windows.Forms.DataGridViewImageCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6313181B4ECEDA94 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-95B77E45BE5C7748 | Method | `protected virtual System.Boolean DoubleClickUnsharesRow(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E731E57A9A524368 | Method | `protected virtual System.Boolean EnterUnsharesRow(System.Int32 rowIndex, System.Boolean throughMouseClick)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C62D0BB74171032 | Method | `protected virtual System.Void Finalize()` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B502985F04F0C7C | Method | `protected virtual System.Object GetClipboardContent(System.Int32 rowIndex, System.Boolean firstCell, System.Boolean lastCell, System.Boolean inFirstRow, System.Boolean inLastRow, System.String format)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6AB11C01268A26F9 | Method | `protected virtual System.Drawing.Rectangle GetContentBounds(System.Drawing.Graphics graphics, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewImageCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-352419A2F94C9053 | Method | `protected virtual System.Drawing.Rectangle GetErrorIconBounds(System.Drawing.Graphics graphics, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewImageCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-25DBC7AC200AE8BC | Method | `protected internal virtual System.String GetErrorText(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8E5DC93159244A00 | Method | `protected virtual System.Object GetFormattedValue(System.Object value, System.Int32 rowIndex, ref System.Windows.Forms.DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter valueTypeConverter, System.ComponentModel.TypeConverter formattedValueTypeConverter, System.Windows.Forms.DataGridViewDataErrorContexts context)` | `System.Windows.Forms.DataGridViewImageCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E3453D096F5B27A0 | Method | `protected virtual System.Drawing.Size GetPreferredSize(System.Drawing.Graphics graphics, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Int32 rowIndex, System.Drawing.Size constraintSize)` | `System.Windows.Forms.DataGridViewImageCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-98458D7763C20759 | Method | `protected virtual System.Drawing.Size GetSize(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F6D7BEB3B4F73901 | Method | `protected virtual System.Object GetValue(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewImageCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-22DEAA039F153EF9 | Method | `protected virtual System.Boolean KeyDownUnsharesRow(System.Windows.Forms.KeyEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F0D770B6FE31C882 | Method | `protected virtual System.Boolean KeyPressUnsharesRow(System.Windows.Forms.KeyPressEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-378F81DF7D0B3C36 | Method | `protected virtual System.Boolean KeyUpUnsharesRow(System.Windows.Forms.KeyEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4CB26679C2D6C837 | Method | `protected virtual System.Boolean LeaveUnsharesRow(System.Int32 rowIndex, System.Boolean throughMouseClick)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-98B3BFBE07247C30 | Method | `protected virtual System.Boolean MouseClickUnsharesRow(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-742B17C927CFE056 | Method | `protected virtual System.Boolean MouseDoubleClickUnsharesRow(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-869A6215EB720129 | Method | `protected virtual System.Boolean MouseDownUnsharesRow(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A842F73256437DEC | Method | `protected virtual System.Boolean MouseEnterUnsharesRow(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DE355E6507999D63 | Method | `protected virtual System.Boolean MouseLeaveUnsharesRow(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5BEF1C9C6BC315DF | Method | `protected virtual System.Boolean MouseMoveUnsharesRow(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5701A29A905F5AA9 | Method | `protected virtual System.Boolean MouseUpUnsharesRow(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C71FC87A9380468F | Method | `protected virtual System.Void OnClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-11A0EE6C9CE47EE5 | Method | `protected virtual System.Void OnContentClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-79690B479284377F | Method | `protected virtual System.Void OnContentDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-515DB1F3D302EB09 | Method | `protected virtual System.Void OnDataGridViewChanged()` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-328D34E9561AFD53 | Method | `protected virtual System.Void OnDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AE1EB5F14CEACF60 | Method | `protected virtual System.Void OnEnter(System.Int32 rowIndex, System.Boolean throughMouseClick)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-406F672ADDF0C4A7 | Method | `protected virtual System.Void OnKeyDown(System.Windows.Forms.KeyEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9D70F9CF885DBA20 | Method | `protected virtual System.Void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EBDA1E4C2B4E0310 | Method | `protected virtual System.Void OnKeyUp(System.Windows.Forms.KeyEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1914215A1782AEF1 | Method | `protected virtual System.Void OnLeave(System.Int32 rowIndex, System.Boolean throughMouseClick)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C10B58E923F2C2B1 | Method | `protected virtual System.Void OnMouseClick(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-334FD48FEFF3990A | Method | `protected virtual System.Void OnMouseDoubleClick(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-85362C814DB02F14 | Method | `protected virtual System.Void OnMouseDown(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-04A0EAD8B180C7AE | Method | `protected virtual System.Void OnMouseEnter(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1F41C278BD3A5837 | Method | `protected virtual System.Void OnMouseLeave(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6914AE47B8690D5B | Method | `protected virtual System.Void OnMouseMove(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E7539B05599BB3E0 | Method | `protected virtual System.Void OnMouseUp(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B88782E43280111B | Method | `protected virtual System.Void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, System.Int32 rowIndex, System.Windows.Forms.DataGridViewElementStates elementState, System.Object value, System.Object formattedValue, System.String errorText, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Windows.Forms.DataGridViewAdvancedBorderStyle advancedBorderStyle, System.Windows.Forms.DataGridViewPaintParts paintParts)` | `System.Windows.Forms.DataGridViewImageCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-66EF4868B74E2619 | Method | `protected virtual System.Void PaintBorder(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle bounds, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Windows.Forms.DataGridViewAdvancedBorderStyle advancedBorderStyle)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E8A0CD8F3F8904E2 | Method | `protected virtual System.Void PaintErrorIcon(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellValueBounds, System.String errorText)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B10DF8E7B21DFF42 | Method | `protected System.Void RaiseCellClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8F1BD0C625D82098 | Method | `protected System.Void RaiseCellContentClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-94EA67B5D9DBCB1A | Method | `protected System.Void RaiseCellContentDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6D19BFFD93521FD7 | Method | `protected System.Void RaiseCellValueChanged(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A8A7747A1C392264 | Method | `protected System.Void RaiseDataError(System.Windows.Forms.DataGridViewDataErrorEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-26780CF5E4FC7987 | Method | `protected System.Void RaiseMouseWheel(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8ABC65E032390AC4 | Method | `protected virtual System.Boolean SetValue(System.Int32 rowIndex, System.Object value)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
