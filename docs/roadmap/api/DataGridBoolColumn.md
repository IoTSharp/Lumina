# DataGridBoolColumn：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/DataGridBoolColumn.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.DataGridBoolColumn`；基类：`System.Windows.Forms.DataGridColumnStyle`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **15**，公开事件 **12**，公开方法 **3**；受保护扩展成员 **32**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-AE7D344DAE21ACDF | Constructor | `public DataGridBoolColumn()` | `System.Windows.Forms.DataGridBoolColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6FB4BF10AC5D3D0A | Constructor | `public DataGridBoolColumn(System.ComponentModel.PropertyDescriptor prop)` | `System.Windows.Forms.DataGridBoolColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-84909DF4F91538A5 | Constructor | `public DataGridBoolColumn(System.ComponentModel.PropertyDescriptor prop, System.Boolean isDefault)` | `System.Windows.Forms.DataGridBoolColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16954C2D47A55D01 | Event | `public event System.EventHandler AlignmentChanged` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BC93EA0F6F336831 | Event | `public event System.EventHandler AllowNullChanged` | `System.Windows.Forms.DataGridBoolColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29AD2C638520EB35 | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FEF5A9D54852AF63 | Event | `public event System.EventHandler FalseValueChanged` | `System.Windows.Forms.DataGridBoolColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B0735DEEE8403AA9 | Event | `public event System.EventHandler FontChanged` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-22E8F974C7174A29 | Event | `public event System.EventHandler HeaderTextChanged` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-27A59502B5111696 | Event | `public event System.EventHandler MappingNameChanged` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1169677C9250CD03 | Event | `public event System.EventHandler NullTextChanged` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4B33D6B96FC4E130 | Event | `public event System.EventHandler PropertyDescriptorChanged` | `System.Windows.Forms.DataGridColumnStyle` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A1960DFA90882FB3 | Event | `public event System.EventHandler ReadOnlyChanged` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9503B02C856C3321 | Event | `public event System.EventHandler TrueValueChanged` | `System.Windows.Forms.DataGridBoolColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-64EB083FAED8D28D | Event | `public event System.EventHandler WidthChanged` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DD300B018519D48A | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D7903489D30EC663 | Method | `public System.Void ResetHeaderText()` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E200D1C40449411D | Method | `public virtual System.String ToString()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A6ABF183D1CC7599 | Property | `virtual System.Windows.Forms.HorizontalAlignment Alignment { public get; public set; }` | `System.Windows.Forms.DataGridColumnStyle` LocalizableAttribute(true); DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B9CD1F7951CC85C | Property | `System.Boolean AllowNull { public get; public set; }` | `System.Windows.Forms.DataGridBoolColumn` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2981196C510AF66A | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A74112C80CC79E9 | Property | `virtual System.Windows.Forms.DataGridTableStyle DataGridTableStyle { public get; }` | `System.Windows.Forms.DataGridColumnStyle` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CB4D45EBA1011232 | Property | `System.Object FalseValue { public get; public set; }` | `System.Windows.Forms.DataGridBoolColumn` TypeConverterAttribute(System.ComponentModel.StringConverter); DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1F83E2CA0F40081E | Property | `System.Windows.Forms.AccessibleObject HeaderAccessibleObject { public get; }` | `System.Windows.Forms.DataGridColumnStyle` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-54CA66E1B97C92F7 | Property | `virtual System.String HeaderText { public get; public set; }` | `System.Windows.Forms.DataGridColumnStyle` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DC78CD30BC42B2FE | Property | `System.String MappingName { public get; public set; }` | `System.Windows.Forms.DataGridColumnStyle` EditorAttribute("System.Windows.Forms.Design.DataGridColumnStyleMappingNameEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); LocalizableAttribute(true); DefaultValueAttribute("") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-55985737C230FC51 | Property | `virtual System.String NullText { public get; public set; }` | `System.Windows.Forms.DataGridColumnStyle` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8C659E5E3EA22C1E | Property | `System.Object NullValue { public get; public set; }` | `System.Windows.Forms.DataGridBoolColumn` TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AB19D43F9549F580 | Property | `virtual System.ComponentModel.PropertyDescriptor PropertyDescriptor { public get; public set; }` | `System.Windows.Forms.DataGridColumnStyle` DefaultValueAttribute(null); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-76B2C398D188CB1C | Property | `virtual System.Boolean ReadOnly { public get; public set; }` | `System.Windows.Forms.DataGridColumnStyle` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-20861D67268A050E | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5626E42D6768F443 | Property | `System.Object TrueValue { public get; public set; }` | `System.Windows.Forms.DataGridBoolColumn` TypeConverterAttribute(System.ComponentModel.StringConverter); DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-323E43CA633F3F6B | Property | `virtual System.Int32 Width { public get; public set; }` | `System.Windows.Forms.DataGridColumnStyle` LocalizableAttribute(true); DefaultValueAttribute(100) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-718D112C4E65FF61 | Method | `protected internal virtual System.Void Abort(System.Int32 rowNum)` | `System.Windows.Forms.DataGridBoolColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BF8335E8EE75392B | Method | `protected System.Void BeginUpdate()` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A3BA0363899EE17B | Method | `protected System.Void CheckValidDataSource(System.Windows.Forms.CurrencyManager value)` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6982FD741B15D0E3 | Method | `protected internal virtual System.Void ColumnStartedEditing(System.Windows.Forms.Control editingControl)` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C2E94CE9F950106C | Method | `protected internal virtual System.Boolean Commit(System.Windows.Forms.CurrencyManager dataSource, System.Int32 rowNum)` | `System.Windows.Forms.DataGridBoolColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EE5E67BB1DCE578F | Method | `protected internal virtual System.Void ConcedeFocus()` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-26FE90469D87F976 | Method | `protected virtual System.Windows.Forms.AccessibleObject CreateHeaderAccessibleObject()` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-80C2329064B88040 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-94403F6ED4A13161 | Method | `protected internal virtual System.Void Edit(System.Windows.Forms.CurrencyManager source, System.Int32 rowNum, System.Drawing.Rectangle bounds, System.Boolean readOnly)` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-027743C1BA8D5E6C | Method | `protected internal virtual System.Void Edit(System.Windows.Forms.CurrencyManager source, System.Int32 rowNum, System.Drawing.Rectangle bounds, System.Boolean readOnly, System.String displayText)` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-70D77CBBE0365C5E | Method | `protected internal virtual System.Void Edit(System.Windows.Forms.CurrencyManager source, System.Int32 rowNum, System.Drawing.Rectangle bounds, System.Boolean readOnly, System.String displayText, System.Boolean cellIsVisible)` | `System.Windows.Forms.DataGridBoolColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3E94DA388106C173 | Method | `protected System.Void EndUpdate()` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5FA2A9060F7F42BD | Method | `protected internal virtual System.Void EnterNullValue()` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FCC804D5FF9C1985 | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-45D712AB87A98C85 | Method | `protected internal virtual System.Object GetColumnValueAtRow(System.Windows.Forms.CurrencyManager source, System.Int32 rowNum)` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6246DEF15687DFD8 | Method | `protected internal virtual System.Int32 GetMinimumHeight()` | `System.Windows.Forms.DataGridBoolColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5BC91F7F46D3D569 | Method | `protected internal virtual System.Int32 GetPreferredHeight(System.Drawing.Graphics g, System.Object value)` | `System.Windows.Forms.DataGridBoolColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-413F4C0AB56301E5 | Method | `protected internal virtual System.Drawing.Size GetPreferredSize(System.Drawing.Graphics g, System.Object value)` | `System.Windows.Forms.DataGridBoolColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29691A1897CA6110 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AD8744880065DE23 | Method | `protected virtual System.Void Invalidate()` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F42D268BE59EFD38 | Method | `protected internal virtual System.Void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, System.Windows.Forms.CurrencyManager source, System.Int32 rowNum)` | `System.Windows.Forms.DataGridBoolColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-825C41C5A8BD8505 | Method | `protected internal virtual System.Void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, System.Windows.Forms.CurrencyManager source, System.Int32 rowNum, System.Boolean alignToRight)` | `System.Windows.Forms.DataGridBoolColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C52F8B8F0807D709 | Method | `protected internal virtual System.Void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, System.Windows.Forms.CurrencyManager source, System.Int32 rowNum, System.Drawing.Brush backBrush, System.Drawing.Brush foreBrush, System.Boolean alignToRight)` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8B76964977F551FA | Method | `protected internal virtual System.Void ReleaseHostedControl()` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B7E2A82FD9A880CF | Method | `protected internal virtual System.Void SetColumnValueAtRow(System.Windows.Forms.CurrencyManager source, System.Int32 rowNum, System.Object value)` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B347259A23E3947A | Method | `protected virtual System.Void SetDataGrid(System.Windows.Forms.DataGrid value)` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-60E706737B1F5C16 | Method | `protected virtual System.Void SetDataGridInColumn(System.Windows.Forms.DataGrid value)` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B7F539C41E6E195 | Method | `protected internal virtual System.Void UpdateUI(System.Windows.Forms.CurrencyManager source, System.Int32 rowNum, System.String displayText)` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A15E3F135E087609 | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F0FBE84F85DD919 | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1F5BA6C99187AC41 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D6C7D31BFCED582A | Property | `System.Int32 FontHeight { protected get; }` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
