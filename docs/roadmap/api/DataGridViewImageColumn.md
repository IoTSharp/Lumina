# DataGridViewImageColumn：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/DataGridViewImageColumn.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.DataGridViewImageColumn`；基类：`System.Windows.Forms.DataGridViewColumn`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **38**，公开事件 **1**，公开方法 **4**；受保护扩展成员 **11**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-B2296AC545C72A4D | Constructor | `public DataGridViewImageColumn()` | `System.Windows.Forms.DataGridViewImageColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0FE12555DC9D6B04 | Constructor | `public DataGridViewImageColumn(System.Boolean valuesAreIcons)` | `System.Windows.Forms.DataGridViewImageColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D931C08178B57C65 | Event | `public event System.EventHandler Disposed` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-65FF92F246A2D7F3 | Method | `public virtual System.Object Clone()` | `System.Windows.Forms.DataGridViewImageColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A4D2A5688D24EDED | Method | `public System.Void Dispose()` | `System.Windows.Forms.DataGridViewBand`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CB48A171D91648E3 | Method | `public virtual System.Int32 GetPreferredWidth(System.Windows.Forms.DataGridViewAutoSizeColumnMode autoSizeColumnMode, System.Boolean fixedHeight)` | `System.Windows.Forms.DataGridViewColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CF861DCD5FEE36DA | Method | `public virtual System.String ToString()` | `System.Windows.Forms.DataGridViewImageColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D1F4DDCA10249E78 | Property | `System.Windows.Forms.DataGridViewAutoSizeColumnMode AutoSizeMode { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1598F39713695C93 | Property | `virtual System.Windows.Forms.DataGridViewCell CellTemplate { public get; public set; }` | `System.Windows.Forms.DataGridViewImageColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-37E7B5F24A9FFA1C | Property | `System.Type CellType { public get; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1BE6221B05537F14 | Property | `virtual System.Windows.Forms.ContextMenuStrip ContextMenuStrip { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FA308A92737F0141 | Property | `System.Windows.Forms.DataGridView DataGridView { public get; }` | `System.Windows.Forms.DataGridViewElement` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2148DEA7F80A2E7E | Property | `System.String DataPropertyName { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(true); DefaultValueAttribute(""); TypeConverterAttribute("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"); EditorAttribute("System.Windows.Forms.Design.DataGridViewColumnDataPropertyNameEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3E7AA88B9ED8FC7F | Property | `virtual System.Windows.Forms.DataGridViewCellStyle DefaultCellStyle { public get; public set; }` | `System.Windows.Forms.DataGridViewImageColumn` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5BFD633DFF79A519 | Property | `System.Type DefaultHeaderCellType { public get; public set; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-107C5818D7D423A3 | Property | `System.String Description { public get; public set; }` | `System.Windows.Forms.DataGridViewImageColumn` BrowsableAttribute(true); DefaultValueAttribute("") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-55190630A833CDBC | Property | `virtual System.Boolean Displayed { public get; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B8E801C72AEC73C9 | Property | `System.Int32 DisplayIndex { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F4F5AEFE647C8393 | Property | `System.Int32 DividerWidth { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-64676575B3E297D5 | Property | `System.Single FillWeight { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(100) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AE629622F2AC5BC1 | Property | `virtual System.Boolean Frozen { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DF244FC8E3C81F95 | Property | `System.Boolean HasDefaultCellStyle { public get; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-823A2715699CE014 | Property | `System.Windows.Forms.DataGridViewColumnHeaderCell HeaderCell { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E49A1A1A14917819 | Property | `System.String HeaderText { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3C295036D7C1D97B | Property | `System.Drawing.Icon Icon { public get; public set; }` | `System.Windows.Forms.DataGridViewImageColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-09C54821B4F563F7 | Property | `System.Drawing.Image Image { public get; public set; }` | `System.Windows.Forms.DataGridViewImageColumn` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D89409BF1AE9B98A | Property | `System.Windows.Forms.DataGridViewImageCellLayout ImageLayout { public get; public set; }` | `System.Windows.Forms.DataGridViewImageColumn` DefaultValueAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A91BA3E11C10B2A1 | Property | `System.Int32 Index { public get; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A336C09E8D9B52F7 | Property | `System.Windows.Forms.DataGridViewAutoSizeColumnMode InheritedAutoSizeMode { public get; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C01CF87C15233289 | Property | `virtual System.Windows.Forms.DataGridViewCellStyle InheritedStyle { public get; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8287DD51BE53157E | Property | `System.Boolean IsDataBound { public get; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ED0AF6D2E2CAEEB1 | Property | `System.Int32 MinimumWidth { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(5); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-59F279A4D332914E | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DBBC27C575854087 | Property | `virtual System.Boolean ReadOnly { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B6E16996D9ABD803 | Property | `virtual System.Windows.Forms.DataGridViewTriState Resizable { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C702F10D57436A5C | Property | `virtual System.Boolean Selected { public get; public set; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-794704B35103B84B | Property | `System.ComponentModel.ISite Site { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7901623614FCFB7B | Property | `System.Windows.Forms.DataGridViewColumnSortMode SortMode { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-670C7F30AC5EA0D4 | Property | `virtual System.Windows.Forms.DataGridViewElementStates State { public get; }` | `System.Windows.Forms.DataGridViewElement` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B7962618709D21A8 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B60E5D13D92D07C | Property | `System.String ToolTipText { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(""); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-279E171693890B73 | Property | `System.Boolean ValuesAreIcons { public get; public set; }` | `System.Windows.Forms.DataGridViewImageColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-86F4F421ED244A20 | Property | `System.Type ValueType { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DefaultValueAttribute(null); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-67F68AC53F26CAE1 | Property | `virtual System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(true); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-06FC82793D5867A7 | Property | `System.Int32 Width { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-439F8FEC2EE6169F | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.DataGridViewColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4FD102047F935784 | Method | `protected virtual System.Void Finalize()` | `System.Windows.Forms.DataGridViewBand`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C47E5D7DEF79E93E | Method | `protected virtual System.Void OnDataGridViewChanged()` | `System.Windows.Forms.DataGridViewBand`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A1E6DC3815960C82 | Method | `protected System.Void RaiseCellClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FB16D94A332CBB7D | Method | `protected System.Void RaiseCellContentClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-060FADA2D1C9E942 | Method | `protected System.Void RaiseCellContentDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A6CADEA818331D92 | Method | `protected System.Void RaiseCellValueChanged(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-35D8530D24DDF85E | Method | `protected System.Void RaiseDataError(System.Windows.Forms.DataGridViewDataErrorEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7960CC6B651B4042 | Method | `protected System.Void RaiseMouseWheel(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3E2A9BBD3B8B9624 | Property | `System.Windows.Forms.DataGridViewHeaderCell HeaderCellCore { protected get; protected set; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2A5FC3337962FD34 | Property | `System.Boolean IsRow { protected get; }` | `System.Windows.Forms.DataGridViewBand`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
