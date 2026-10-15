# DataGridViewHeaderCell：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/DataGridViewHeaderCell.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.DataGridViewHeaderCell`；基类：`System.Windows.Forms.DataGridViewCell`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **33**，公开事件 **0**，公开方法 **20**；受保护扩展成员 **56**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-CD0BF689A3F5EA2F | Constructor | `public DataGridViewHeaderCell()` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-526BFBEB692B79DB | Method | `public virtual System.Windows.Forms.DataGridViewAdvancedBorderStyle AdjustCellBorderStyle(System.Windows.Forms.DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyleInput, System.Windows.Forms.DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder, System.Boolean singleVerticalBorderAdded, System.Boolean singleHorizontalBorderAdded, System.Boolean isFirstDisplayedColumn, System.Boolean isFirstDisplayedRow)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9ACB04371430582B | Method | `public virtual System.Object Clone()` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1346AB61A98B6111 | Method | `public virtual System.Void DetachEditingControl()` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1E2FE8E2EA18B3F1 | Method | `public System.Void Dispose()` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4690F0D54C0BB439 | Method | `public System.Drawing.Rectangle GetContentBounds(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-57DE54D7DFB3519E | Method | `public System.Object GetEditedFormattedValue(System.Int32 rowIndex, System.Windows.Forms.DataGridViewDataErrorContexts context)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CB5A6EEF6B97E48F | Method | `public virtual System.Windows.Forms.ContextMenuStrip GetInheritedContextMenuStrip(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3883C8EDEB5DE3D3 | Method | `public virtual System.Windows.Forms.DataGridViewElementStates GetInheritedState(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B5766065E6B4035B | Method | `public virtual System.Windows.Forms.DataGridViewCellStyle GetInheritedStyle(System.Windows.Forms.DataGridViewCellStyle inheritedCellStyle, System.Int32 rowIndex, System.Boolean includeColors)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FB82BA32BC54ACDB | Method | `public virtual System.Void InitializeEditingControl(System.Int32 rowIndex, System.Object initialFormattedValue, System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6190B18DAE97F5D2 | Method | `public virtual System.Boolean KeyEntersEditMode(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-360BDDD1A49CE16A | Method | `public static System.Int32 MeasureTextHeight(System.Drawing.Graphics graphics, System.String text, System.Drawing.Font font, System.Int32 maxWidth, System.Windows.Forms.TextFormatFlags flags)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B9B6DE2B1529387B | Method | `public static System.Int32 MeasureTextHeight(System.Drawing.Graphics graphics, System.String text, System.Drawing.Font font, System.Int32 maxWidth, System.Windows.Forms.TextFormatFlags flags, out System.Boolean widthTruncated)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3C4A649A030263DA | Method | `public static System.Drawing.Size MeasureTextPreferredSize(System.Drawing.Graphics graphics, System.String text, System.Drawing.Font font, System.Single maxRatio, System.Windows.Forms.TextFormatFlags flags)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC57C9694C62416D | Method | `public static System.Drawing.Size MeasureTextSize(System.Drawing.Graphics graphics, System.String text, System.Drawing.Font font, System.Windows.Forms.TextFormatFlags flags)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-36F522B0D2743586 | Method | `public static System.Int32 MeasureTextWidth(System.Drawing.Graphics graphics, System.String text, System.Drawing.Font font, System.Int32 maxHeight, System.Windows.Forms.TextFormatFlags flags)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9888DA07227D78E2 | Method | `public virtual System.Object ParseFormattedValue(System.Object formattedValue, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter formattedValueTypeConverter, System.ComponentModel.TypeConverter valueTypeConverter)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9683624A09CB98E0 | Method | `public virtual System.Void PositionEditingControl(System.Boolean setLocation, System.Boolean setSize, System.Drawing.Rectangle cellBounds, System.Drawing.Rectangle cellClip, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Boolean singleVerticalBorderAdded, System.Boolean singleHorizontalBorderAdded, System.Boolean isFirstDisplayedColumn, System.Boolean isFirstDisplayedRow)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5F3AF04EBC6FD37E | Method | `public virtual System.Drawing.Rectangle PositionEditingPanel(System.Drawing.Rectangle cellBounds, System.Drawing.Rectangle cellClip, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Boolean singleVerticalBorderAdded, System.Boolean singleHorizontalBorderAdded, System.Boolean isFirstDisplayedColumn, System.Boolean isFirstDisplayedRow)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3088CC25C3BD5F90 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F5C2B78A3054110C | Property | `System.Windows.Forms.AccessibleObject AccessibilityObject { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C1B2E71D92398B6 | Property | `System.Int32 ColumnIndex { public get; }` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E4A00F5F1C096FC6 | Property | `System.Drawing.Rectangle ContentBounds { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C3A4AFE8630A9321 | Property | `virtual System.Windows.Forms.ContextMenuStrip ContextMenuStrip { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FEDBA80C9E08210E | Property | `System.Windows.Forms.DataGridView DataGridView { public get; }` | `System.Windows.Forms.DataGridViewElement` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-73A66ED3ED7366B0 | Property | `virtual System.Object DefaultNewRowValue { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B22D44144984948 | Property | `virtual System.Boolean Displayed { public get; }` | `System.Windows.Forms.DataGridViewHeaderCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-094BEFDE0DA20E71 | Property | `System.Object EditedFormattedValue { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8D220FB47E610A53 | Property | `virtual System.Type EditType { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-88A15AC2AC974F66 | Property | `System.Drawing.Rectangle ErrorIconBounds { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC31616142B53F11 | Property | `System.String ErrorText { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FFF666D63CA93136 | Property | `System.Object FormattedValue { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-608DC9DCAE0D8A10 | Property | `virtual System.Type FormattedValueType { public get; }` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A9EEFAE0BBDB2E3 | Property | `virtual System.Boolean Frozen { public get; }` | `System.Windows.Forms.DataGridViewHeaderCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-39CC2F6FCE2522C2 | Property | `System.Boolean HasStyle { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-297ABF4E33C2D308 | Property | `System.Windows.Forms.DataGridViewElementStates InheritedState { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2A9B4E23B3D9736B | Property | `System.Windows.Forms.DataGridViewCellStyle InheritedStyle { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A3CCD3A824EF4D53 | Property | `System.Boolean IsInEditMode { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A6B8ED79A3F83417 | Property | `System.Windows.Forms.DataGridViewColumn OwningColumn { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AFCF50DDAA1FA0D0 | Property | `System.Windows.Forms.DataGridViewRow OwningRow { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-344A492C7E931972 | Property | `System.Drawing.Size PreferredSize { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-58727049D1410576 | Property | `virtual System.Boolean ReadOnly { public get; public set; }` | `System.Windows.Forms.DataGridViewHeaderCell` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7AF8571EC22FD3A3 | Property | `virtual System.Boolean Resizable { public get; }` | `System.Windows.Forms.DataGridViewHeaderCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C662512EEF48F69 | Property | `System.Int32 RowIndex { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0359871468B8F77 | Property | `virtual System.Boolean Selected { public get; public set; }` | `System.Windows.Forms.DataGridViewHeaderCell` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-11D99F63956647DE | Property | `System.Drawing.Size Size { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-91E726DC6DA38F8C | Property | `virtual System.Windows.Forms.DataGridViewElementStates State { public get; }` | `System.Windows.Forms.DataGridViewElement` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-17351DFC53481697 | Property | `System.Windows.Forms.DataGridViewCellStyle Style { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-05F4D1B290C8DE43 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-33B1B6EE094DDFA5 | Property | `System.String ToolTipText { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D3E203B37D7E78AF | Property | `System.Object Value { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C6668A9B9AA7950C | Property | `virtual System.Type ValueType { public get; public set; }` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F0326EF331E686A8 | Property | `virtual System.Boolean Visible { public get; }` | `System.Windows.Forms.DataGridViewHeaderCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-52E467FE4C50484D | Method | `protected virtual System.Drawing.Rectangle BorderWidths(System.Windows.Forms.DataGridViewAdvancedBorderStyle advancedBorderStyle)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CDD63BF1A2BC1849 | Method | `protected virtual System.Boolean ClickUnsharesRow(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8C768F3CEE85C4B5 | Method | `protected virtual System.Boolean ContentClickUnsharesRow(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5FF6C968B3E516CB | Method | `protected virtual System.Boolean ContentDoubleClickUnsharesRow(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6CA019FC21413695 | Method | `protected virtual System.Windows.Forms.AccessibleObject CreateAccessibilityInstance()` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FEB151E4571090F4 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0AC771981BE4336D | Method | `protected virtual System.Boolean DoubleClickUnsharesRow(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CC5FB7931FE46FD9 | Method | `protected virtual System.Boolean EnterUnsharesRow(System.Int32 rowIndex, System.Boolean throughMouseClick)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8B63E74A464914DD | Method | `protected virtual System.Void Finalize()` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2D734379ABFAA7CB | Method | `protected virtual System.Object GetClipboardContent(System.Int32 rowIndex, System.Boolean firstCell, System.Boolean lastCell, System.Boolean inFirstRow, System.Boolean inLastRow, System.String format)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-97AB2E2E429A76FA | Method | `protected virtual System.Drawing.Rectangle GetContentBounds(System.Drawing.Graphics graphics, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2764D6BF0E250067 | Method | `protected virtual System.Drawing.Rectangle GetErrorIconBounds(System.Drawing.Graphics graphics, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-12A681DC190DC780 | Method | `protected internal virtual System.String GetErrorText(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AEDC8F22F6348FCD | Method | `protected virtual System.Object GetFormattedValue(System.Object value, System.Int32 rowIndex, ref System.Windows.Forms.DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter valueTypeConverter, System.ComponentModel.TypeConverter formattedValueTypeConverter, System.Windows.Forms.DataGridViewDataErrorContexts context)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A2BC03BC0963304C | Method | `protected virtual System.Drawing.Size GetPreferredSize(System.Drawing.Graphics graphics, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Int32 rowIndex, System.Drawing.Size constraintSize)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0118A106210BAFCE | Method | `protected virtual System.Drawing.Size GetSize(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC5D5FEB3F228727 | Method | `protected virtual System.Object GetValue(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8A9DA9B564C36738 | Method | `protected virtual System.Boolean KeyDownUnsharesRow(System.Windows.Forms.KeyEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C77D23D7D5FA886 | Method | `protected virtual System.Boolean KeyPressUnsharesRow(System.Windows.Forms.KeyPressEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F80394AC9B73C2FB | Method | `protected virtual System.Boolean KeyUpUnsharesRow(System.Windows.Forms.KeyEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6B4708BD7654ADD8 | Method | `protected virtual System.Boolean LeaveUnsharesRow(System.Int32 rowIndex, System.Boolean throughMouseClick)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-67B8B85140306C8E | Method | `protected virtual System.Boolean MouseClickUnsharesRow(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4A62D9BF155A2FFD | Method | `protected virtual System.Boolean MouseDoubleClickUnsharesRow(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-014F4952538DE661 | Method | `protected virtual System.Boolean MouseDownUnsharesRow(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-318E76B8F9D824AE | Method | `protected virtual System.Boolean MouseEnterUnsharesRow(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-46750B131829AA18 | Method | `protected virtual System.Boolean MouseLeaveUnsharesRow(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-57BCD31BD5A6B234 | Method | `protected virtual System.Boolean MouseMoveUnsharesRow(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4A83C3932797C309 | Method | `protected virtual System.Boolean MouseUpUnsharesRow(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-99DD9D44EEA89B71 | Method | `protected virtual System.Void OnClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7114EFCD991C7ABF | Method | `protected virtual System.Void OnContentClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AF4213E402B394A4 | Method | `protected virtual System.Void OnContentDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E779B4D00B9036B5 | Method | `protected virtual System.Void OnDataGridViewChanged()` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4B5892077C3A21C9 | Method | `protected virtual System.Void OnDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D1A161BDAE0DA2FD | Method | `protected virtual System.Void OnEnter(System.Int32 rowIndex, System.Boolean throughMouseClick)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E8E65CF50D2D1643 | Method | `protected virtual System.Void OnKeyDown(System.Windows.Forms.KeyEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-628DC5BCB61D2E9D | Method | `protected virtual System.Void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A022E39F744CA58D | Method | `protected virtual System.Void OnKeyUp(System.Windows.Forms.KeyEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8848323EEDFDCE01 | Method | `protected virtual System.Void OnLeave(System.Int32 rowIndex, System.Boolean throughMouseClick)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D1C08715DAEAD6C5 | Method | `protected virtual System.Void OnMouseClick(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E6E3643FF22EBFF8 | Method | `protected virtual System.Void OnMouseDoubleClick(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E55867645A37E64E | Method | `protected virtual System.Void OnMouseDown(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-39FDD4DD99973E76 | Method | `protected virtual System.Void OnMouseEnter(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-52B493AA7D264ACF | Method | `protected virtual System.Void OnMouseLeave(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F833692A9B330B06 | Method | `protected virtual System.Void OnMouseMove(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FE74684FAF3B6138 | Method | `protected virtual System.Void OnMouseUp(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-301E206B0E3869F9 | Method | `protected virtual System.Void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, System.Int32 rowIndex, System.Windows.Forms.DataGridViewElementStates dataGridViewElementState, System.Object value, System.Object formattedValue, System.String errorText, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Windows.Forms.DataGridViewAdvancedBorderStyle advancedBorderStyle, System.Windows.Forms.DataGridViewPaintParts paintParts)` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-706933165AB64547 | Method | `protected virtual System.Void PaintBorder(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle bounds, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Windows.Forms.DataGridViewAdvancedBorderStyle advancedBorderStyle)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-913227B3192045D7 | Method | `protected virtual System.Void PaintErrorIcon(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellValueBounds, System.String errorText)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-80390D9F18076587 | Method | `protected System.Void RaiseCellClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-21BD39ABAB999D4E | Method | `protected System.Void RaiseCellContentClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A00C9EEC0F102D3 | Method | `protected System.Void RaiseCellContentDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FC2E854E1E7BFCA2 | Method | `protected System.Void RaiseCellValueChanged(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1A9438320253A90E | Method | `protected System.Void RaiseDataError(System.Windows.Forms.DataGridViewDataErrorEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BB3998B58CE557C6 | Method | `protected System.Void RaiseMouseWheel(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1D85D975F2F491E3 | Method | `protected virtual System.Boolean SetValue(System.Int32 rowIndex, System.Object value)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6EA6187B1CCA744A | Property | `System.Windows.Forms.ButtonState ButtonState { protected get; }` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
