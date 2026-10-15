# DataGridTextBoxColumn：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/DataGridTextBoxColumn.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.DataGridTextBoxColumn`；基类：`System.Windows.Forms.DataGridColumnStyle`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **14**，公开事件 **9**，公开方法 **3**；受保护扩展成员 **36**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-2086F90A4F6E49A4 | Constructor | `public DataGridTextBoxColumn()` | `System.Windows.Forms.DataGridTextBoxColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B1BB37993E62E2A | Constructor | `public DataGridTextBoxColumn(System.ComponentModel.PropertyDescriptor prop)` | `System.Windows.Forms.DataGridTextBoxColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-06339A58FD4C44F2 | Constructor | `public DataGridTextBoxColumn(System.ComponentModel.PropertyDescriptor prop, System.Boolean isDefault)` | `System.Windows.Forms.DataGridTextBoxColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B66DFC6324BF057F | Constructor | `public DataGridTextBoxColumn(System.ComponentModel.PropertyDescriptor prop, System.String format)` | `System.Windows.Forms.DataGridTextBoxColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E5F027693F1813D | Constructor | `public DataGridTextBoxColumn(System.ComponentModel.PropertyDescriptor prop, System.String format, System.Boolean isDefault)` | `System.Windows.Forms.DataGridTextBoxColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-09BF377E99688F92 | Event | `public event System.EventHandler AlignmentChanged` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1F4A57C56F9E841E | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CAE4FC8195C0D1FA | Event | `public event System.EventHandler FontChanged` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B9CF69A6560EC980 | Event | `public event System.EventHandler HeaderTextChanged` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A08BA9B2A5FAAF5D | Event | `public event System.EventHandler MappingNameChanged` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2D4C998195D880BD | Event | `public event System.EventHandler NullTextChanged` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0AF28BF8079942F7 | Event | `public event System.EventHandler PropertyDescriptorChanged` | `System.Windows.Forms.DataGridColumnStyle` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-19BE8ED529F9EA4D | Event | `public event System.EventHandler ReadOnlyChanged` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A751819FB955EB6 | Event | `public event System.EventHandler WidthChanged` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E7D26E58AC10ED27 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A4209DDA03A20F8F | Method | `public System.Void ResetHeaderText()` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-691E14E8CDBCC163 | Method | `public virtual System.String ToString()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3315E6AC4780CDAF | Property | `virtual System.Windows.Forms.HorizontalAlignment Alignment { public get; public set; }` | `System.Windows.Forms.DataGridColumnStyle` LocalizableAttribute(true); DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-14C795DA63DCEB7A | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-75AF88B548B505FA | Property | `virtual System.Windows.Forms.DataGridTableStyle DataGridTableStyle { public get; }` | `System.Windows.Forms.DataGridColumnStyle` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4EF8AFB1700024CE | Property | `System.String Format { public get; public set; }` | `System.Windows.Forms.DataGridTextBoxColumn` DefaultValueAttribute(null); EditorAttribute("System.Windows.Forms.Design.DataGridColumnStyleFormatEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9AF4889605453D17 | Property | `System.IFormatProvider FormatInfo { public get; public set; }` | `System.Windows.Forms.DataGridTextBoxColumn` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6082E5B8CA0AF99C | Property | `System.Windows.Forms.AccessibleObject HeaderAccessibleObject { public get; }` | `System.Windows.Forms.DataGridColumnStyle` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E9232AC3F1B46365 | Property | `virtual System.String HeaderText { public get; public set; }` | `System.Windows.Forms.DataGridColumnStyle` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-65F5DFFFD72D24D2 | Property | `System.String MappingName { public get; public set; }` | `System.Windows.Forms.DataGridColumnStyle` EditorAttribute("System.Windows.Forms.Design.DataGridColumnStyleMappingNameEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); LocalizableAttribute(true); DefaultValueAttribute("") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2AFDFE1035F2D3C3 | Property | `virtual System.String NullText { public get; public set; }` | `System.Windows.Forms.DataGridColumnStyle` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AF79DC880AF26212 | Property | `virtual System.ComponentModel.PropertyDescriptor PropertyDescriptor { public set; }` | `System.Windows.Forms.DataGridTextBoxColumn` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-27FDA07E888181E9 | Property | `virtual System.Boolean ReadOnly { public get; public set; }` | `System.Windows.Forms.DataGridColumnStyle` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E830C5CED6554CD4 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E95E0E9585D76C3 | Property | `virtual System.Windows.Forms.TextBox TextBox { public get; }` | `System.Windows.Forms.DataGridTextBoxColumn` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8CAA9CA0736CF926 | Property | `virtual System.Int32 Width { public get; public set; }` | `System.Windows.Forms.DataGridColumnStyle` LocalizableAttribute(true); DefaultValueAttribute(100) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-ADBED952332F9EB8 | Method | `protected internal virtual System.Void Abort(System.Int32 rowNum)` | `System.Windows.Forms.DataGridTextBoxColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1290200E1597640E | Method | `protected System.Void BeginUpdate()` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2D2FCA7554FE2413 | Method | `protected System.Void CheckValidDataSource(System.Windows.Forms.CurrencyManager value)` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4F0E723DDE130862 | Method | `protected internal virtual System.Void ColumnStartedEditing(System.Windows.Forms.Control editingControl)` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A2E564E4CB2055F | Method | `protected internal virtual System.Boolean Commit(System.Windows.Forms.CurrencyManager dataSource, System.Int32 rowNum)` | `System.Windows.Forms.DataGridTextBoxColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2587E8597ED2E00E | Method | `protected internal virtual System.Void ConcedeFocus()` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C4B499DC67D1912 | Method | `protected virtual System.Windows.Forms.AccessibleObject CreateHeaderAccessibleObject()` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-66A5EE4382EA8096 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-06F4E80CA9BD8D1C | Method | `protected internal virtual System.Void Edit(System.Windows.Forms.CurrencyManager source, System.Int32 rowNum, System.Drawing.Rectangle bounds, System.Boolean readOnly)` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-044F8C7E1988615A | Method | `protected internal virtual System.Void Edit(System.Windows.Forms.CurrencyManager source, System.Int32 rowNum, System.Drawing.Rectangle bounds, System.Boolean readOnly, System.String displayText)` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1EC383653E0CC3B5 | Method | `protected internal virtual System.Void Edit(System.Windows.Forms.CurrencyManager source, System.Int32 rowNum, System.Drawing.Rectangle bounds, System.Boolean readOnly, System.String displayText, System.Boolean cellIsVisible)` | `System.Windows.Forms.DataGridTextBoxColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0F9A409893C6F91 | Method | `protected System.Void EndEdit()` | `System.Windows.Forms.DataGridTextBoxColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F25AC88E73D46305 | Method | `protected System.Void EndUpdate()` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0421580DE21BEE4C | Method | `protected internal virtual System.Void EnterNullValue()` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-39B9CB2B0B44A06C | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-23194774C93C7617 | Method | `protected internal virtual System.Object GetColumnValueAtRow(System.Windows.Forms.CurrencyManager source, System.Int32 rowNum)` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E347D10F6F5051D6 | Method | `protected internal virtual System.Int32 GetMinimumHeight()` | `System.Windows.Forms.DataGridTextBoxColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-281217412DEB3271 | Method | `protected internal virtual System.Int32 GetPreferredHeight(System.Drawing.Graphics g, System.Object value)` | `System.Windows.Forms.DataGridTextBoxColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-20A3777A5BF8DA2E | Method | `protected internal virtual System.Drawing.Size GetPreferredSize(System.Drawing.Graphics g, System.Object value)` | `System.Windows.Forms.DataGridTextBoxColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-684E00E416155982 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-67ECF63278FE9C54 | Method | `protected System.Void HideEditBox()` | `System.Windows.Forms.DataGridTextBoxColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8FF3BDBCA3716919 | Method | `protected virtual System.Void Invalidate()` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0153BA7B54C24D32 | Method | `protected internal virtual System.Void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, System.Windows.Forms.CurrencyManager source, System.Int32 rowNum)` | `System.Windows.Forms.DataGridTextBoxColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FB0F365FD93CEFB5 | Method | `protected internal virtual System.Void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, System.Windows.Forms.CurrencyManager source, System.Int32 rowNum, System.Boolean alignToRight)` | `System.Windows.Forms.DataGridTextBoxColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B4071F2DB29F223D | Method | `protected internal virtual System.Void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, System.Windows.Forms.CurrencyManager source, System.Int32 rowNum, System.Drawing.Brush backBrush, System.Drawing.Brush foreBrush, System.Boolean alignToRight)` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3F172891812E0E05 | Method | `protected System.Void PaintText(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, System.String text, System.Boolean alignToRight)` | `System.Windows.Forms.DataGridTextBoxColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4915FDF753726EA5 | Method | `protected System.Void PaintText(System.Drawing.Graphics g, System.Drawing.Rectangle textBounds, System.String text, System.Drawing.Brush backBrush, System.Drawing.Brush foreBrush, System.Boolean alignToRight)` | `System.Windows.Forms.DataGridTextBoxColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-28E7CF7EB8119E4E | Method | `protected internal virtual System.Void ReleaseHostedControl()` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-295B93352B5DC33C | Method | `protected internal virtual System.Void SetColumnValueAtRow(System.Windows.Forms.CurrencyManager source, System.Int32 rowNum, System.Object value)` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E1E490331A4DB61 | Method | `protected virtual System.Void SetDataGrid(System.Windows.Forms.DataGrid value)` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6D42C713FEF3CE62 | Method | `protected virtual System.Void SetDataGridInColumn(System.Windows.Forms.DataGrid value)` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-97794950CEBDDD65 | Method | `protected internal virtual System.Void UpdateUI(System.Windows.Forms.CurrencyManager source, System.Int32 rowNum, System.String displayText)` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F8AFA7E631BF311D | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-72E81A089E1CD49F | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F930C5A778C8705 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B743E61CE0E1A23 | Property | `System.Int32 FontHeight { protected get; }` | `System.Windows.Forms.DataGridColumnStyle`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
