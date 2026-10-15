# DataGridViewTextBoxColumn：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/DataGridViewTextBoxColumn.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.DataGridViewTextBoxColumn`；基类：`System.Windows.Forms.DataGridViewColumn`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **34**，公开事件 **1**，公开方法 **4**；受保护扩展成员 **11**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-EEB99258CAECDA4E | Constructor | `public DataGridViewTextBoxColumn()` | `System.Windows.Forms.DataGridViewTextBoxColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3365A22F287FE16D | Event | `public event System.EventHandler Disposed` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29C587826D4778FC | Method | `public virtual System.Object Clone()` | `System.Windows.Forms.DataGridViewColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D35F964646AE70AC | Method | `public System.Void Dispose()` | `System.Windows.Forms.DataGridViewBand`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-559535CB4B328F08 | Method | `public virtual System.Int32 GetPreferredWidth(System.Windows.Forms.DataGridViewAutoSizeColumnMode autoSizeColumnMode, System.Boolean fixedHeight)` | `System.Windows.Forms.DataGridViewColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3BC87D1CBA5532AB | Method | `public virtual System.String ToString()` | `System.Windows.Forms.DataGridViewTextBoxColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-39A075B6BC103A8E | Property | `System.Windows.Forms.DataGridViewAutoSizeColumnMode AutoSizeMode { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B8CD01D0E2EC68A | Property | `virtual System.Windows.Forms.DataGridViewCell CellTemplate { public get; public set; }` | `System.Windows.Forms.DataGridViewTextBoxColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9CD192F487964C0C | Property | `System.Type CellType { public get; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-715F9E9E73FC9AE6 | Property | `virtual System.Windows.Forms.ContextMenuStrip ContextMenuStrip { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A2945AFB3C577DFA | Property | `System.Windows.Forms.DataGridView DataGridView { public get; }` | `System.Windows.Forms.DataGridViewElement` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-14FA7B0B187E203E | Property | `System.String DataPropertyName { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(true); DefaultValueAttribute(""); TypeConverterAttribute("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"); EditorAttribute("System.Windows.Forms.Design.DataGridViewColumnDataPropertyNameEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-25EDE3980B4BF0D4 | Property | `virtual System.Windows.Forms.DataGridViewCellStyle DefaultCellStyle { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A1D0507C78592EE6 | Property | `System.Type DefaultHeaderCellType { public get; public set; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6D6BF80DD57B1AEC | Property | `virtual System.Boolean Displayed { public get; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B2242368D5EB5CE | Property | `System.Int32 DisplayIndex { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D1C8B700286265FF | Property | `System.Int32 DividerWidth { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CF4A95E52189303D | Property | `System.Single FillWeight { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(100) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-573879532C09C888 | Property | `virtual System.Boolean Frozen { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2AE882A2C6860FA3 | Property | `System.Boolean HasDefaultCellStyle { public get; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-30B0F9A2079312F4 | Property | `System.Windows.Forms.DataGridViewColumnHeaderCell HeaderCell { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-34CCE64A80E0BEFF | Property | `System.String HeaderText { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-07EBDE6AB3FFFB3E | Property | `System.Int32 Index { public get; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-79EBA649C0062C41 | Property | `System.Windows.Forms.DataGridViewAutoSizeColumnMode InheritedAutoSizeMode { public get; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F7EB7CF672520C15 | Property | `virtual System.Windows.Forms.DataGridViewCellStyle InheritedStyle { public get; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F5C0DF9D281CC6F2 | Property | `System.Boolean IsDataBound { public get; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-41A738E272E95709 | Property | `System.Int32 MaxInputLength { public get; public set; }` | `System.Windows.Forms.DataGridViewTextBoxColumn` DefaultValueAttribute(32767) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CC370181E5710D79 | Property | `System.Int32 MinimumWidth { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(5); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2DF2A4AB70165CF1 | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-338F2624DAFB154D | Property | `virtual System.Boolean ReadOnly { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-57B49C29F760A5A0 | Property | `virtual System.Windows.Forms.DataGridViewTriState Resizable { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5E8314EB4B3E32C6 | Property | `virtual System.Boolean Selected { public get; public set; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E6773106C3CE7933 | Property | `System.ComponentModel.ISite Site { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C086A6E7BE8DB9D5 | Property | `System.Windows.Forms.DataGridViewColumnSortMode SortMode { public get; public set; }` | `System.Windows.Forms.DataGridViewTextBoxColumn` DefaultValueAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5445DE1E0DD130B1 | Property | `virtual System.Windows.Forms.DataGridViewElementStates State { public get; }` | `System.Windows.Forms.DataGridViewElement` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FAE2CAAFB042C955 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-22B45851B0DAE2E7 | Property | `System.String ToolTipText { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(""); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4A5AAF6450BD20E3 | Property | `System.Type ValueType { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DefaultValueAttribute(null); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BDAA8894720865F8 | Property | `virtual System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(true); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A1EABD1BA80F1E3 | Property | `System.Int32 Width { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-30ADBB4D1485D5BB | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.DataGridViewColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D6CC8D0F1D1497F4 | Method | `protected virtual System.Void Finalize()` | `System.Windows.Forms.DataGridViewBand`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-222AC7F73564F893 | Method | `protected virtual System.Void OnDataGridViewChanged()` | `System.Windows.Forms.DataGridViewBand`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4814A793D1A4E6FA | Method | `protected System.Void RaiseCellClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A2F11140F148634B | Method | `protected System.Void RaiseCellContentClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6A76AE109205AB89 | Method | `protected System.Void RaiseCellContentDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-367410A30066B5AB | Method | `protected System.Void RaiseCellValueChanged(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F409D81EF43FAFB8 | Method | `protected System.Void RaiseDataError(System.Windows.Forms.DataGridViewDataErrorEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6817AE800BC246F9 | Method | `protected System.Void RaiseMouseWheel(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D71A9A048E6C1448 | Property | `System.Windows.Forms.DataGridViewHeaderCell HeaderCellCore { protected get; protected set; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BDCAAAA7BF5A6B6F | Property | `System.Boolean IsRow { protected get; }` | `System.Windows.Forms.DataGridViewBand`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
