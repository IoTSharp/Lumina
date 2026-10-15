# DataGridViewRowHeaderCell：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/DataGridViewRowHeaderCell.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.DataGridViewRowHeaderCell`；基类：`System.Windows.Forms.DataGridViewHeaderCell`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **33**，公开事件 **0**，公开方法 **20**；受保护扩展成员 **56**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-E6204F60980A9E99 | Constructor | `public DataGridViewRowHeaderCell()` | `System.Windows.Forms.DataGridViewRowHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B2258CA9D1E6B26 | Method | `public virtual System.Windows.Forms.DataGridViewAdvancedBorderStyle AdjustCellBorderStyle(System.Windows.Forms.DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyleInput, System.Windows.Forms.DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder, System.Boolean singleVerticalBorderAdded, System.Boolean singleHorizontalBorderAdded, System.Boolean isFirstDisplayedColumn, System.Boolean isFirstDisplayedRow)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6F20A0CE3B686209 | Method | `public virtual System.Object Clone()` | `System.Windows.Forms.DataGridViewRowHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-96DC13DA8D0F0444 | Method | `public virtual System.Void DetachEditingControl()` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E599B4122AB36A24 | Method | `public System.Void Dispose()` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-63F77A4727751684 | Method | `public System.Drawing.Rectangle GetContentBounds(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CFA668BDA53632CA | Method | `public System.Object GetEditedFormattedValue(System.Int32 rowIndex, System.Windows.Forms.DataGridViewDataErrorContexts context)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3AE0657BE8F3387F | Method | `public virtual System.Windows.Forms.ContextMenuStrip GetInheritedContextMenuStrip(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewRowHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AEC47BAAD5E11C9D | Method | `public virtual System.Windows.Forms.DataGridViewElementStates GetInheritedState(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-57C9295C4D1BF328 | Method | `public virtual System.Windows.Forms.DataGridViewCellStyle GetInheritedStyle(System.Windows.Forms.DataGridViewCellStyle inheritedCellStyle, System.Int32 rowIndex, System.Boolean includeColors)` | `System.Windows.Forms.DataGridViewRowHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E480B058F9455764 | Method | `public virtual System.Void InitializeEditingControl(System.Int32 rowIndex, System.Object initialFormattedValue, System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0EDF11C4420BB533 | Method | `public virtual System.Boolean KeyEntersEditMode(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-53C86B295C4D3C2D | Method | `public static System.Int32 MeasureTextHeight(System.Drawing.Graphics graphics, System.String text, System.Drawing.Font font, System.Int32 maxWidth, System.Windows.Forms.TextFormatFlags flags)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D77A54EECFC6F839 | Method | `public static System.Int32 MeasureTextHeight(System.Drawing.Graphics graphics, System.String text, System.Drawing.Font font, System.Int32 maxWidth, System.Windows.Forms.TextFormatFlags flags, out System.Boolean widthTruncated)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F450E644B927147C | Method | `public static System.Drawing.Size MeasureTextPreferredSize(System.Drawing.Graphics graphics, System.String text, System.Drawing.Font font, System.Single maxRatio, System.Windows.Forms.TextFormatFlags flags)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-940B924602F0D2C4 | Method | `public static System.Drawing.Size MeasureTextSize(System.Drawing.Graphics graphics, System.String text, System.Drawing.Font font, System.Windows.Forms.TextFormatFlags flags)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BEE02F1637EDAA75 | Method | `public static System.Int32 MeasureTextWidth(System.Drawing.Graphics graphics, System.String text, System.Drawing.Font font, System.Int32 maxHeight, System.Windows.Forms.TextFormatFlags flags)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FD52680A8465B9A4 | Method | `public virtual System.Object ParseFormattedValue(System.Object formattedValue, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter formattedValueTypeConverter, System.ComponentModel.TypeConverter valueTypeConverter)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8C14A9E04806A079 | Method | `public virtual System.Void PositionEditingControl(System.Boolean setLocation, System.Boolean setSize, System.Drawing.Rectangle cellBounds, System.Drawing.Rectangle cellClip, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Boolean singleVerticalBorderAdded, System.Boolean singleHorizontalBorderAdded, System.Boolean isFirstDisplayedColumn, System.Boolean isFirstDisplayedRow)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-38A96B41C38E8D16 | Method | `public virtual System.Drawing.Rectangle PositionEditingPanel(System.Drawing.Rectangle cellBounds, System.Drawing.Rectangle cellClip, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Boolean singleVerticalBorderAdded, System.Boolean singleHorizontalBorderAdded, System.Boolean isFirstDisplayedColumn, System.Boolean isFirstDisplayedRow)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50A80090A7538E05 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.DataGridViewRowHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F65D5F7C3D50418A | Property | `System.Windows.Forms.AccessibleObject AccessibilityObject { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50ECC416E0AC69FB | Property | `System.Int32 ColumnIndex { public get; }` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-34E45DAB12F63E35 | Property | `System.Drawing.Rectangle ContentBounds { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-816A7700C8C57CB7 | Property | `virtual System.Windows.Forms.ContextMenuStrip ContextMenuStrip { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-38E7C04CFB2F1152 | Property | `System.Windows.Forms.DataGridView DataGridView { public get; }` | `System.Windows.Forms.DataGridViewElement` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B6FEB8033896690 | Property | `virtual System.Object DefaultNewRowValue { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5D75E90C7DEC4A17 | Property | `virtual System.Boolean Displayed { public get; }` | `System.Windows.Forms.DataGridViewHeaderCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EF5E4FC323C40B2B | Property | `System.Object EditedFormattedValue { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-177067C5992CDC43 | Property | `virtual System.Type EditType { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-656D063B216D3CF9 | Property | `System.Drawing.Rectangle ErrorIconBounds { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DA3F280B8A4CC095 | Property | `System.String ErrorText { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-89B6910F67B04828 | Property | `System.Object FormattedValue { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-236DC645C9DFB0F6 | Property | `virtual System.Type FormattedValueType { public get; }` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9F893DED0DBD2402 | Property | `virtual System.Boolean Frozen { public get; }` | `System.Windows.Forms.DataGridViewHeaderCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-43C871330B84557B | Property | `System.Boolean HasStyle { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-03F5898C177CF997 | Property | `System.Windows.Forms.DataGridViewElementStates InheritedState { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EF968A4F41E9BE75 | Property | `System.Windows.Forms.DataGridViewCellStyle InheritedStyle { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0282F436B1D17993 | Property | `System.Boolean IsInEditMode { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-917587F2CB6D4C07 | Property | `System.Windows.Forms.DataGridViewColumn OwningColumn { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EDB6BAF4E572C84E | Property | `System.Windows.Forms.DataGridViewRow OwningRow { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-57CCFEB839B36B3E | Property | `System.Drawing.Size PreferredSize { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-501C28782E4DC82A | Property | `virtual System.Boolean ReadOnly { public get; public set; }` | `System.Windows.Forms.DataGridViewHeaderCell` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C4F94A43586EA257 | Property | `virtual System.Boolean Resizable { public get; }` | `System.Windows.Forms.DataGridViewHeaderCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F728768BF66CB81 | Property | `System.Int32 RowIndex { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-239730BBDB0B7F37 | Property | `virtual System.Boolean Selected { public get; public set; }` | `System.Windows.Forms.DataGridViewHeaderCell` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D45812AB3AD9D1D0 | Property | `System.Drawing.Size Size { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BD022397318F56DF | Property | `virtual System.Windows.Forms.DataGridViewElementStates State { public get; }` | `System.Windows.Forms.DataGridViewElement` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B4F47C39075746DC | Property | `System.Windows.Forms.DataGridViewCellStyle Style { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FA8F8BD2D790ED48 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E4F3E0D55399F6B0 | Property | `System.String ToolTipText { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CB75D07A707F96E0 | Property | `System.Object Value { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A993FAFB586BA3E | Property | `virtual System.Type ValueType { public get; public set; }` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-54525CD94DD95351 | Property | `virtual System.Boolean Visible { public get; }` | `System.Windows.Forms.DataGridViewHeaderCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-1CEA1D823943D94E | Method | `protected virtual System.Drawing.Rectangle BorderWidths(System.Windows.Forms.DataGridViewAdvancedBorderStyle advancedBorderStyle)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F70DE4035E59D325 | Method | `protected virtual System.Boolean ClickUnsharesRow(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ABD9FBCC41936974 | Method | `protected virtual System.Boolean ContentClickUnsharesRow(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C1FDF0163609643 | Method | `protected virtual System.Boolean ContentDoubleClickUnsharesRow(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BF263EDA035A4598 | Method | `protected virtual System.Windows.Forms.AccessibleObject CreateAccessibilityInstance()` | `System.Windows.Forms.DataGridViewRowHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DCF544B189A171F1 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C66BFEA1AA2CDFF2 | Method | `protected virtual System.Boolean DoubleClickUnsharesRow(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CC20E6BA77081F75 | Method | `protected virtual System.Boolean EnterUnsharesRow(System.Int32 rowIndex, System.Boolean throughMouseClick)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-521DDB2CDB8DA881 | Method | `protected virtual System.Void Finalize()` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E30B6B37BACE3E6 | Method | `protected virtual System.Object GetClipboardContent(System.Int32 rowIndex, System.Boolean firstCell, System.Boolean lastCell, System.Boolean inFirstRow, System.Boolean inLastRow, System.String format)` | `System.Windows.Forms.DataGridViewRowHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CEB4DADE5698AEDB | Method | `protected virtual System.Drawing.Rectangle GetContentBounds(System.Drawing.Graphics graphics, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewRowHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F54FE1931577B708 | Method | `protected virtual System.Drawing.Rectangle GetErrorIconBounds(System.Drawing.Graphics graphics, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewRowHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FA843396F7E2EAEB | Method | `protected internal virtual System.String GetErrorText(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewRowHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5A28006F4BA0DC19 | Method | `protected virtual System.Object GetFormattedValue(System.Object value, System.Int32 rowIndex, ref System.Windows.Forms.DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter valueTypeConverter, System.ComponentModel.TypeConverter formattedValueTypeConverter, System.Windows.Forms.DataGridViewDataErrorContexts context)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FB0D553D5F11E427 | Method | `protected virtual System.Drawing.Size GetPreferredSize(System.Drawing.Graphics graphics, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Int32 rowIndex, System.Drawing.Size constraintSize)` | `System.Windows.Forms.DataGridViewRowHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2C8315C05FFC2131 | Method | `protected virtual System.Drawing.Size GetSize(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-827DD42FC07534CC | Method | `protected virtual System.Object GetValue(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewRowHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4C107EC146B1CCE6 | Method | `protected virtual System.Boolean KeyDownUnsharesRow(System.Windows.Forms.KeyEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CC6908C8BE289E52 | Method | `protected virtual System.Boolean KeyPressUnsharesRow(System.Windows.Forms.KeyPressEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0BD1244D8511AB08 | Method | `protected virtual System.Boolean KeyUpUnsharesRow(System.Windows.Forms.KeyEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-276B5C60EDED0D1A | Method | `protected virtual System.Boolean LeaveUnsharesRow(System.Int32 rowIndex, System.Boolean throughMouseClick)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3F276DC35C0B1080 | Method | `protected virtual System.Boolean MouseClickUnsharesRow(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DFC512E99E820220 | Method | `protected virtual System.Boolean MouseDoubleClickUnsharesRow(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3E31BB156DCCD733 | Method | `protected virtual System.Boolean MouseDownUnsharesRow(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8AD9A3E52D8AB86B | Method | `protected virtual System.Boolean MouseEnterUnsharesRow(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C7FF8022908A0BE1 | Method | `protected virtual System.Boolean MouseLeaveUnsharesRow(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AA8E6B05AFBC8D08 | Method | `protected virtual System.Boolean MouseMoveUnsharesRow(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-852B01A631BC99F8 | Method | `protected virtual System.Boolean MouseUpUnsharesRow(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1F7A7E97C8FA2C94 | Method | `protected virtual System.Void OnClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C1F71B9041579CA9 | Method | `protected virtual System.Void OnContentClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6F5E697CC9878B26 | Method | `protected virtual System.Void OnContentDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2D67490245AD579E | Method | `protected virtual System.Void OnDataGridViewChanged()` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8F785677E8EDEC9C | Method | `protected virtual System.Void OnDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FFA214A4CAA47F24 | Method | `protected virtual System.Void OnEnter(System.Int32 rowIndex, System.Boolean throughMouseClick)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-31B7D8A40D99035E | Method | `protected virtual System.Void OnKeyDown(System.Windows.Forms.KeyEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F7F82A4C0E8AD965 | Method | `protected virtual System.Void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EA39F9D8D3D8CEF3 | Method | `protected virtual System.Void OnKeyUp(System.Windows.Forms.KeyEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1415A3178EA4C220 | Method | `protected virtual System.Void OnLeave(System.Int32 rowIndex, System.Boolean throughMouseClick)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1BA60F922F4DF160 | Method | `protected virtual System.Void OnMouseClick(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-15174741384B070B | Method | `protected virtual System.Void OnMouseDoubleClick(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2A08F6F570FF39BF | Method | `protected virtual System.Void OnMouseDown(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6362CF5A1B3DDE85 | Method | `protected virtual System.Void OnMouseEnter(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C7643079CF0BA081 | Method | `protected virtual System.Void OnMouseLeave(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-17CBC74FCAA9A42B | Method | `protected virtual System.Void OnMouseMove(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1024A55EE7564E0A | Method | `protected virtual System.Void OnMouseUp(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1856831DF96969E5 | Method | `protected virtual System.Void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, System.Int32 rowIndex, System.Windows.Forms.DataGridViewElementStates cellState, System.Object value, System.Object formattedValue, System.String errorText, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Windows.Forms.DataGridViewAdvancedBorderStyle advancedBorderStyle, System.Windows.Forms.DataGridViewPaintParts paintParts)` | `System.Windows.Forms.DataGridViewRowHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FC13697ACB1050B2 | Method | `protected virtual System.Void PaintBorder(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle bounds, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Windows.Forms.DataGridViewAdvancedBorderStyle advancedBorderStyle)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B84BC122C9F697C9 | Method | `protected virtual System.Void PaintErrorIcon(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellValueBounds, System.String errorText)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D0832340DD4347B9 | Method | `protected System.Void RaiseCellClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DBE56884FF2828F3 | Method | `protected System.Void RaiseCellContentClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F6C84BBC86872208 | Method | `protected System.Void RaiseCellContentDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC6E53A3DDE5B290 | Method | `protected System.Void RaiseCellValueChanged(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AAB94CD09EA3ADB3 | Method | `protected System.Void RaiseDataError(System.Windows.Forms.DataGridViewDataErrorEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EA33B6CC93287349 | Method | `protected System.Void RaiseMouseWheel(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F2C67AFA3F0EDE03 | Method | `protected virtual System.Boolean SetValue(System.Int32 rowIndex, System.Object value)` | `System.Windows.Forms.DataGridViewRowHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0267CCF3AF3C671C | Property | `System.Windows.Forms.ButtonState ButtonState { protected get; }` | `System.Windows.Forms.DataGridViewHeaderCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
