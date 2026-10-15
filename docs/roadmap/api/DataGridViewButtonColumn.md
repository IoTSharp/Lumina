# DataGridViewButtonColumn：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/DataGridViewButtonColumn.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.DataGridViewButtonColumn`；基类：`System.Windows.Forms.DataGridViewColumn`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **36**，公开事件 **1**，公开方法 **4**；受保护扩展成员 **11**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-40939328F502A39E | Constructor | `public DataGridViewButtonColumn()` | `System.Windows.Forms.DataGridViewButtonColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6452F59751E27EFA | Event | `public event System.EventHandler Disposed` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4351EA5BAA60545A | Method | `public virtual System.Object Clone()` | `System.Windows.Forms.DataGridViewButtonColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9547C70027B656F8 | Method | `public System.Void Dispose()` | `System.Windows.Forms.DataGridViewBand`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-79F0332893FD5CC4 | Method | `public virtual System.Int32 GetPreferredWidth(System.Windows.Forms.DataGridViewAutoSizeColumnMode autoSizeColumnMode, System.Boolean fixedHeight)` | `System.Windows.Forms.DataGridViewColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-54ED666C169F4562 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.DataGridViewButtonColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E3C99B718B18E90B | Property | `System.Windows.Forms.DataGridViewAutoSizeColumnMode AutoSizeMode { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5CA10FF1A10C4E84 | Property | `virtual System.Windows.Forms.DataGridViewCell CellTemplate { public get; public set; }` | `System.Windows.Forms.DataGridViewButtonColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C277311A3C7AA27A | Property | `System.Type CellType { public get; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-263D16CA4D005DB3 | Property | `virtual System.Windows.Forms.ContextMenuStrip ContextMenuStrip { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2044FF94F2EDBF46 | Property | `System.Windows.Forms.DataGridView DataGridView { public get; }` | `System.Windows.Forms.DataGridViewElement` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8AD70377FC22D4CB | Property | `System.String DataPropertyName { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(true); DefaultValueAttribute(""); TypeConverterAttribute("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"); EditorAttribute("System.Windows.Forms.Design.DataGridViewColumnDataPropertyNameEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-170FE9CDBEF3F492 | Property | `virtual System.Windows.Forms.DataGridViewCellStyle DefaultCellStyle { public get; public set; }` | `System.Windows.Forms.DataGridViewButtonColumn` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C80913296DCCD6D6 | Property | `System.Type DefaultHeaderCellType { public get; public set; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-06EA6DA86EDEC2A8 | Property | `virtual System.Boolean Displayed { public get; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-138B628B92E2CBC6 | Property | `System.Int32 DisplayIndex { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9804EA6B76F98425 | Property | `System.Int32 DividerWidth { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-884B147F9B30AA46 | Property | `System.Single FillWeight { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(100) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-36B4F4AACA442D01 | Property | `System.Windows.Forms.FlatStyle FlatStyle { public get; public set; }` | `System.Windows.Forms.DataGridViewButtonColumn` DefaultValueAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-327B84091AC6869F | Property | `virtual System.Boolean Frozen { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0329808FEF8E06FA | Property | `System.Boolean HasDefaultCellStyle { public get; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EF9517D20EBDAC74 | Property | `System.Windows.Forms.DataGridViewColumnHeaderCell HeaderCell { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5A91D1F81B5C1414 | Property | `System.String HeaderText { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C195C88783D22C06 | Property | `System.Int32 Index { public get; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-088876CDC5F8F9A3 | Property | `System.Windows.Forms.DataGridViewAutoSizeColumnMode InheritedAutoSizeMode { public get; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E5D54125E73768BA | Property | `virtual System.Windows.Forms.DataGridViewCellStyle InheritedStyle { public get; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-162388722D3B7CF3 | Property | `System.Boolean IsDataBound { public get; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-54ABB7AD4D4563A7 | Property | `System.Int32 MinimumWidth { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(5); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7F53E6468A49CC0C | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5F8DE0BE67DBB721 | Property | `virtual System.Boolean ReadOnly { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-647A864DBF6BFDFB | Property | `virtual System.Windows.Forms.DataGridViewTriState Resizable { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1825FC14C2504BC3 | Property | `virtual System.Boolean Selected { public get; public set; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8470F894420218CC | Property | `System.ComponentModel.ISite Site { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9CC185593EA2976E | Property | `System.Windows.Forms.DataGridViewColumnSortMode SortMode { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-15B9EC509B29473D | Property | `virtual System.Windows.Forms.DataGridViewElementStates State { public get; }` | `System.Windows.Forms.DataGridViewElement` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B3F7577561DBCC47 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E059DA43C31C76A | Property | `System.String Text { public get; public set; }` | `System.Windows.Forms.DataGridViewButtonColumn` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16E305C7E3EB0D34 | Property | `System.String ToolTipText { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(""); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16F82E8B5C1ECFBD | Property | `System.Boolean UseColumnTextForButtonValue { public get; public set; }` | `System.Windows.Forms.DataGridViewButtonColumn` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E8B77829F07E517F | Property | `System.Type ValueType { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DefaultValueAttribute(null); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B45C44ED137F05C | Property | `virtual System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(true); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F1F24937BFCC87D | Property | `System.Int32 Width { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-660D380D48E64DA4 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.DataGridViewColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-325B5D657BBB9B71 | Method | `protected virtual System.Void Finalize()` | `System.Windows.Forms.DataGridViewBand`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9F0EFF1B854CEF11 | Method | `protected virtual System.Void OnDataGridViewChanged()` | `System.Windows.Forms.DataGridViewBand`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3C5AC5EC95182A9A | Method | `protected System.Void RaiseCellClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-55BB37FADE7800EF | Method | `protected System.Void RaiseCellContentClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F066839CDC211FF6 | Method | `protected System.Void RaiseCellContentDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3B254ED1084AC60E | Method | `protected System.Void RaiseCellValueChanged(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9DB204D74921A414 | Method | `protected System.Void RaiseDataError(System.Windows.Forms.DataGridViewDataErrorEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-35E83B595552F9F7 | Method | `protected System.Void RaiseMouseWheel(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4F254244A8A4FDB4 | Property | `System.Windows.Forms.DataGridViewHeaderCell HeaderCellCore { protected get; protected set; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-196D179AF49CC308 | Property | `System.Boolean IsRow { protected get; }` | `System.Windows.Forms.DataGridViewBand`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
