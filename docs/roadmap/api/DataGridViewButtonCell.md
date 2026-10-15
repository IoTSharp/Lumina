# DataGridViewButtonCell：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/DataGridViewButtonCell.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.DataGridViewButtonCell`；基类：`System.Windows.Forms.DataGridViewCell`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **35**，公开事件 **0**，公开方法 **20**；受保护扩展成员 **55**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-4FC46B520EBD1CB6 | Constructor | `public DataGridViewButtonCell()` | `System.Windows.Forms.DataGridViewButtonCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5C56E9CC80DF0968 | Method | `public virtual System.Windows.Forms.DataGridViewAdvancedBorderStyle AdjustCellBorderStyle(System.Windows.Forms.DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyleInput, System.Windows.Forms.DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder, System.Boolean singleVerticalBorderAdded, System.Boolean singleHorizontalBorderAdded, System.Boolean isFirstDisplayedColumn, System.Boolean isFirstDisplayedRow)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C5D551F7FC2302FD | Method | `public virtual System.Object Clone()` | `System.Windows.Forms.DataGridViewButtonCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1093335342417ABC | Method | `public virtual System.Void DetachEditingControl()` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C6F4B09E3D35E37D | Method | `public System.Void Dispose()` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-551AC26271E95A05 | Method | `public System.Drawing.Rectangle GetContentBounds(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2C7E0302E18F93DC | Method | `public System.Object GetEditedFormattedValue(System.Int32 rowIndex, System.Windows.Forms.DataGridViewDataErrorContexts context)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F01BD53044464579 | Method | `public virtual System.Windows.Forms.ContextMenuStrip GetInheritedContextMenuStrip(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A3896E895F1D41F6 | Method | `public virtual System.Windows.Forms.DataGridViewElementStates GetInheritedState(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8A59453E1CE14C7A | Method | `public virtual System.Windows.Forms.DataGridViewCellStyle GetInheritedStyle(System.Windows.Forms.DataGridViewCellStyle inheritedCellStyle, System.Int32 rowIndex, System.Boolean includeColors)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-100FC45E8C3AC4FE | Method | `public virtual System.Void InitializeEditingControl(System.Int32 rowIndex, System.Object initialFormattedValue, System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-731A80849BFD6C9B | Method | `public virtual System.Boolean KeyEntersEditMode(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8E4BDFFB235EC856 | Method | `public static System.Int32 MeasureTextHeight(System.Drawing.Graphics graphics, System.String text, System.Drawing.Font font, System.Int32 maxWidth, System.Windows.Forms.TextFormatFlags flags)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-73767D0B3E71DCBF | Method | `public static System.Int32 MeasureTextHeight(System.Drawing.Graphics graphics, System.String text, System.Drawing.Font font, System.Int32 maxWidth, System.Windows.Forms.TextFormatFlags flags, out System.Boolean widthTruncated)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FD20764F4FF8A6AB | Method | `public static System.Drawing.Size MeasureTextPreferredSize(System.Drawing.Graphics graphics, System.String text, System.Drawing.Font font, System.Single maxRatio, System.Windows.Forms.TextFormatFlags flags)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E998A80AEA9A7B31 | Method | `public static System.Drawing.Size MeasureTextSize(System.Drawing.Graphics graphics, System.String text, System.Drawing.Font font, System.Windows.Forms.TextFormatFlags flags)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F83F5D0451F23804 | Method | `public static System.Int32 MeasureTextWidth(System.Drawing.Graphics graphics, System.String text, System.Drawing.Font font, System.Int32 maxHeight, System.Windows.Forms.TextFormatFlags flags)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3A72E773C7595045 | Method | `public virtual System.Object ParseFormattedValue(System.Object formattedValue, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter formattedValueTypeConverter, System.ComponentModel.TypeConverter valueTypeConverter)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4F903D67420F9EBE | Method | `public virtual System.Void PositionEditingControl(System.Boolean setLocation, System.Boolean setSize, System.Drawing.Rectangle cellBounds, System.Drawing.Rectangle cellClip, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Boolean singleVerticalBorderAdded, System.Boolean singleHorizontalBorderAdded, System.Boolean isFirstDisplayedColumn, System.Boolean isFirstDisplayedRow)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FF26509BBC7A3920 | Method | `public virtual System.Drawing.Rectangle PositionEditingPanel(System.Drawing.Rectangle cellBounds, System.Drawing.Rectangle cellClip, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Boolean singleVerticalBorderAdded, System.Boolean singleHorizontalBorderAdded, System.Boolean isFirstDisplayedColumn, System.Boolean isFirstDisplayedRow)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C5751C84CCA4C214 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.DataGridViewButtonCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A9CF1AC2C042DC5 | Property | `System.Windows.Forms.AccessibleObject AccessibilityObject { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-673F393EC8567CD4 | Property | `System.Int32 ColumnIndex { public get; }` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6A23DEB488A310D5 | Property | `System.Drawing.Rectangle ContentBounds { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C37673138B3C9F2 | Property | `virtual System.Windows.Forms.ContextMenuStrip ContextMenuStrip { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-53214114DDAECE4A | Property | `System.Windows.Forms.DataGridView DataGridView { public get; }` | `System.Windows.Forms.DataGridViewElement` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5AEE29DCC94B2097 | Property | `virtual System.Object DefaultNewRowValue { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4CB3410B4C771DD4 | Property | `virtual System.Boolean Displayed { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3694230988AE4798 | Property | `System.Object EditedFormattedValue { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FD4BF2F0F9BB14F6 | Property | `virtual System.Type EditType { public get; }` | `System.Windows.Forms.DataGridViewButtonCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-40C4146A2193DB11 | Property | `System.Drawing.Rectangle ErrorIconBounds { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A9D51652680D1308 | Property | `System.String ErrorText { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-017D0177144F8C07 | Property | `System.Windows.Forms.FlatStyle FlatStyle { public get; public set; }` | `System.Windows.Forms.DataGridViewButtonCell` DefaultValueAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B14A120E038CB17 | Property | `System.Object FormattedValue { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-33891180F500A599 | Property | `virtual System.Type FormattedValueType { public get; }` | `System.Windows.Forms.DataGridViewButtonCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-09F50BFA576288F5 | Property | `virtual System.Boolean Frozen { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ECB7C24ED1D7318A | Property | `System.Boolean HasStyle { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-512FF0B656ADC8EC | Property | `System.Windows.Forms.DataGridViewElementStates InheritedState { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8D8C2AE25BC0F5F5 | Property | `System.Windows.Forms.DataGridViewCellStyle InheritedStyle { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EAA81604EA952802 | Property | `System.Boolean IsInEditMode { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C12F8B21E0D99C3A | Property | `System.Windows.Forms.DataGridViewColumn OwningColumn { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6CF068C3AA6D01F6 | Property | `System.Windows.Forms.DataGridViewRow OwningRow { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B2CA6C14622732B | Property | `System.Drawing.Size PreferredSize { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4AC089C450773E6F | Property | `virtual System.Boolean ReadOnly { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-007B6D072A396B64 | Property | `virtual System.Boolean Resizable { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7F77133075226198 | Property | `System.Int32 RowIndex { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-87AA9BF36DD9D2C6 | Property | `virtual System.Boolean Selected { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D5FBAE2804CE14C | Property | `System.Drawing.Size Size { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AFB0E830DF026E7A | Property | `virtual System.Windows.Forms.DataGridViewElementStates State { public get; }` | `System.Windows.Forms.DataGridViewElement` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AB24AE8EB82DC7F0 | Property | `System.Windows.Forms.DataGridViewCellStyle Style { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0338B1C783F92CD2 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-43A3956074875DBC | Property | `System.String ToolTipText { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-617022D52ADC89D8 | Property | `System.Boolean UseColumnTextForButtonValue { public get; public set; }` | `System.Windows.Forms.DataGridViewButtonCell` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-500D51F6132C9524 | Property | `System.Object Value { public get; public set; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-07CC9DBBC003294A | Property | `virtual System.Type ValueType { public get; }` | `System.Windows.Forms.DataGridViewButtonCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8EADFD1F7D1C9B02 | Property | `virtual System.Boolean Visible { public get; }` | `System.Windows.Forms.DataGridViewCell` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-E5E1117FA430656D | Method | `protected virtual System.Drawing.Rectangle BorderWidths(System.Windows.Forms.DataGridViewAdvancedBorderStyle advancedBorderStyle)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FC1472F01D329FBB | Method | `protected virtual System.Boolean ClickUnsharesRow(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A7B1924A8B67C30 | Method | `protected virtual System.Boolean ContentClickUnsharesRow(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F75ABDDB94836DFD | Method | `protected virtual System.Boolean ContentDoubleClickUnsharesRow(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0F98367A6650B1C | Method | `protected virtual System.Windows.Forms.AccessibleObject CreateAccessibilityInstance()` | `System.Windows.Forms.DataGridViewButtonCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EB7FC2DE3F61BBCD | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5B9A98B059F8EE93 | Method | `protected virtual System.Boolean DoubleClickUnsharesRow(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C7187654391B1AA | Method | `protected virtual System.Boolean EnterUnsharesRow(System.Int32 rowIndex, System.Boolean throughMouseClick)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-907B1AB960FD063D | Method | `protected virtual System.Void Finalize()` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E7B9140AE6AAF9B | Method | `protected virtual System.Object GetClipboardContent(System.Int32 rowIndex, System.Boolean firstCell, System.Boolean lastCell, System.Boolean inFirstRow, System.Boolean inLastRow, System.String format)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AAD5DEFB007CFD18 | Method | `protected virtual System.Drawing.Rectangle GetContentBounds(System.Drawing.Graphics graphics, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewButtonCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FB9C930E355DF1D6 | Method | `protected virtual System.Drawing.Rectangle GetErrorIconBounds(System.Drawing.Graphics graphics, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewButtonCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C1372070DC8EC907 | Method | `protected internal virtual System.String GetErrorText(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AF560D328F004D32 | Method | `protected virtual System.Object GetFormattedValue(System.Object value, System.Int32 rowIndex, ref System.Windows.Forms.DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter valueTypeConverter, System.ComponentModel.TypeConverter formattedValueTypeConverter, System.Windows.Forms.DataGridViewDataErrorContexts context)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E804C5542D067094 | Method | `protected virtual System.Drawing.Size GetPreferredSize(System.Drawing.Graphics graphics, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Int32 rowIndex, System.Drawing.Size constraintSize)` | `System.Windows.Forms.DataGridViewButtonCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-96AFB0E6B79A9062 | Method | `protected virtual System.Drawing.Size GetSize(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A40D8D6644AA9123 | Method | `protected virtual System.Object GetValue(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewButtonCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EA95956464FA6415 | Method | `protected virtual System.Boolean KeyDownUnsharesRow(System.Windows.Forms.KeyEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewButtonCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0635355EA68D4FA9 | Method | `protected virtual System.Boolean KeyPressUnsharesRow(System.Windows.Forms.KeyPressEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BED86FC30BD5A150 | Method | `protected virtual System.Boolean KeyUpUnsharesRow(System.Windows.Forms.KeyEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewButtonCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A86F9813C9588A7F | Method | `protected virtual System.Boolean LeaveUnsharesRow(System.Int32 rowIndex, System.Boolean throughMouseClick)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3C691AD94E79BAF8 | Method | `protected virtual System.Boolean MouseClickUnsharesRow(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0EE1309E2A33BADB | Method | `protected virtual System.Boolean MouseDoubleClickUnsharesRow(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E079D5F301037378 | Method | `protected virtual System.Boolean MouseDownUnsharesRow(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewButtonCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AF0008C0190A2DFC | Method | `protected virtual System.Boolean MouseEnterUnsharesRow(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewButtonCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-707167762BBCD836 | Method | `protected virtual System.Boolean MouseLeaveUnsharesRow(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewButtonCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5D3031DA53DA4C8C | Method | `protected virtual System.Boolean MouseMoveUnsharesRow(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CF40D18737956830 | Method | `protected virtual System.Boolean MouseUpUnsharesRow(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewButtonCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-798378451CFE1500 | Method | `protected virtual System.Void OnClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0D83B3FEC4A11C30 | Method | `protected virtual System.Void OnContentClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-107C8FFD9201C946 | Method | `protected virtual System.Void OnContentDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4DB2BBBEFBDA45B0 | Method | `protected virtual System.Void OnDataGridViewChanged()` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-06CF005215271DAF | Method | `protected virtual System.Void OnDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-22DBEE737D436624 | Method | `protected virtual System.Void OnEnter(System.Int32 rowIndex, System.Boolean throughMouseClick)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0FBC8F77AEB2D8B1 | Method | `protected virtual System.Void OnKeyDown(System.Windows.Forms.KeyEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewButtonCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3214CA7819F939D4 | Method | `protected virtual System.Void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-23F7F7A27243C891 | Method | `protected virtual System.Void OnKeyUp(System.Windows.Forms.KeyEventArgs e, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewButtonCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4117DD6BBCF230A7 | Method | `protected virtual System.Void OnLeave(System.Int32 rowIndex, System.Boolean throughMouseClick)` | `System.Windows.Forms.DataGridViewButtonCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4963D7D37EACB547 | Method | `protected virtual System.Void OnMouseClick(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F310864C735E9CDC | Method | `protected virtual System.Void OnMouseDoubleClick(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1C10FE79A0A819FB | Method | `protected virtual System.Void OnMouseDown(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewButtonCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-91B25BCFC63EEA44 | Method | `protected virtual System.Void OnMouseEnter(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CBEB47881D93C507 | Method | `protected virtual System.Void OnMouseLeave(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewButtonCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C8F0C858593BF8A1 | Method | `protected virtual System.Void OnMouseMove(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewButtonCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-55B55AE7E58FFCC4 | Method | `protected virtual System.Void OnMouseUp(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridViewButtonCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4828EDC05634BCB2 | Method | `protected virtual System.Void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, System.Int32 rowIndex, System.Windows.Forms.DataGridViewElementStates elementState, System.Object value, System.Object formattedValue, System.String errorText, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Windows.Forms.DataGridViewAdvancedBorderStyle advancedBorderStyle, System.Windows.Forms.DataGridViewPaintParts paintParts)` | `System.Windows.Forms.DataGridViewButtonCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F375AC896BD7C9A0 | Method | `protected virtual System.Void PaintBorder(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle bounds, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Windows.Forms.DataGridViewAdvancedBorderStyle advancedBorderStyle)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-957D35252D3E0E53 | Method | `protected virtual System.Void PaintErrorIcon(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellValueBounds, System.String errorText)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A7DC8D9D3B69230F | Method | `protected System.Void RaiseCellClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D42153716E47A897 | Method | `protected System.Void RaiseCellContentClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EED28DEAB82B43A2 | Method | `protected System.Void RaiseCellContentDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-01DE3DEFE0E282FB | Method | `protected System.Void RaiseCellValueChanged(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-769FC7C764F38172 | Method | `protected System.Void RaiseDataError(System.Windows.Forms.DataGridViewDataErrorEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8CF90D339F3328F9 | Method | `protected System.Void RaiseMouseWheel(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E4BC93FC44AF6CAF | Method | `protected virtual System.Boolean SetValue(System.Int32 rowIndex, System.Object value)` | `System.Windows.Forms.DataGridViewCell`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
