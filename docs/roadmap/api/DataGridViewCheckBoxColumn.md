# DataGridViewCheckBoxColumn：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/DataGridViewCheckBoxColumn.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.DataGridViewCheckBoxColumn`；基类：`System.Windows.Forms.DataGridViewColumn`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **38**，公开事件 **1**，公开方法 **4**；受保护扩展成员 **11**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-8F47A7DB8B1CE623 | Constructor | `public DataGridViewCheckBoxColumn()` | `System.Windows.Forms.DataGridViewCheckBoxColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-80A44F7F1B17DF8D | Constructor | `public DataGridViewCheckBoxColumn(System.Boolean threeState)` | `System.Windows.Forms.DataGridViewCheckBoxColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C9DDC1BDBBBBDC1D | Event | `public event System.EventHandler Disposed` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B79CAEB3C6A7F0EE | Method | `public virtual System.Object Clone()` | `System.Windows.Forms.DataGridViewColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7E94DE5A2DCB54D8 | Method | `public System.Void Dispose()` | `System.Windows.Forms.DataGridViewBand`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A395BF577086CAB | Method | `public virtual System.Int32 GetPreferredWidth(System.Windows.Forms.DataGridViewAutoSizeColumnMode autoSizeColumnMode, System.Boolean fixedHeight)` | `System.Windows.Forms.DataGridViewColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9110C1C0F6C04784 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.DataGridViewCheckBoxColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9F3567860A5CE726 | Property | `System.Windows.Forms.DataGridViewAutoSizeColumnMode AutoSizeMode { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DDCF52A15D2D9E80 | Property | `virtual System.Windows.Forms.DataGridViewCell CellTemplate { public get; public set; }` | `System.Windows.Forms.DataGridViewCheckBoxColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CB4DB663153E7EAC | Property | `System.Type CellType { public get; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5CB2220CA9BA9BB9 | Property | `virtual System.Windows.Forms.ContextMenuStrip ContextMenuStrip { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E40C52BEF5E0D9F5 | Property | `System.Windows.Forms.DataGridView DataGridView { public get; }` | `System.Windows.Forms.DataGridViewElement` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-12B7BC5AA37774B2 | Property | `System.String DataPropertyName { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(true); DefaultValueAttribute(""); TypeConverterAttribute("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"); EditorAttribute("System.Windows.Forms.Design.DataGridViewColumnDataPropertyNameEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8507D77CFC0E18F5 | Property | `virtual System.Windows.Forms.DataGridViewCellStyle DefaultCellStyle { public get; public set; }` | `System.Windows.Forms.DataGridViewCheckBoxColumn` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C6ED76D54C1ADF2C | Property | `System.Type DefaultHeaderCellType { public get; public set; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-82A40198D2A44A22 | Property | `virtual System.Boolean Displayed { public get; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D84DA1249D027A03 | Property | `System.Int32 DisplayIndex { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-774F5362E679EE72 | Property | `System.Int32 DividerWidth { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A8671B52CAF18EC4 | Property | `System.Object FalseValue { public get; public set; }` | `System.Windows.Forms.DataGridViewCheckBoxColumn` DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3107B686F4E81455 | Property | `System.Single FillWeight { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(100) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D1F8F38740326D7C | Property | `System.Windows.Forms.FlatStyle FlatStyle { public get; public set; }` | `System.Windows.Forms.DataGridViewCheckBoxColumn` DefaultValueAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-690F786161082938 | Property | `virtual System.Boolean Frozen { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0F9BE41D2C7AD6E | Property | `System.Boolean HasDefaultCellStyle { public get; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C0621FC7A882918A | Property | `System.Windows.Forms.DataGridViewColumnHeaderCell HeaderCell { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B7A5E6E54104012 | Property | `System.String HeaderText { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3FBDF4923D3AADAB | Property | `System.Object IndeterminateValue { public get; public set; }` | `System.Windows.Forms.DataGridViewCheckBoxColumn` DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5FA62E567BBD4A27 | Property | `System.Int32 Index { public get; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DEE176A41A515747 | Property | `System.Windows.Forms.DataGridViewAutoSizeColumnMode InheritedAutoSizeMode { public get; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-33BDD6F2B0C713C8 | Property | `virtual System.Windows.Forms.DataGridViewCellStyle InheritedStyle { public get; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BD0DCC20336FAAA5 | Property | `System.Boolean IsDataBound { public get; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D247DD08468D1A41 | Property | `System.Int32 MinimumWidth { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(5); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FB13A0EEC3D891E6 | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1115DB5D35625D8E | Property | `virtual System.Boolean ReadOnly { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-90409161DDCD456F | Property | `virtual System.Windows.Forms.DataGridViewTriState Resizable { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B5B093DC93A013BD | Property | `virtual System.Boolean Selected { public get; public set; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A1DC4E564440EC1E | Property | `System.ComponentModel.ISite Site { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A09966886E4684B3 | Property | `System.Windows.Forms.DataGridViewColumnSortMode SortMode { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2DC0C4AB7F863764 | Property | `virtual System.Windows.Forms.DataGridViewElementStates State { public get; }` | `System.Windows.Forms.DataGridViewElement` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-928791CEACD42CAB | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-85F8F961E3B2A897 | Property | `System.Boolean ThreeState { public get; public set; }` | `System.Windows.Forms.DataGridViewCheckBoxColumn` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC66DB7E099475F1 | Property | `System.String ToolTipText { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(""); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F9528CD6398EB79E | Property | `System.Object TrueValue { public get; public set; }` | `System.Windows.Forms.DataGridViewCheckBoxColumn` DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-87F5B9EAE8C3B723 | Property | `System.Type ValueType { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DefaultValueAttribute(null); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1C109B3D010AFA88 | Property | `virtual System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(true); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B55A1A12664F1089 | Property | `System.Int32 Width { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-F43C0274D341D9C7 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.DataGridViewColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-01123E4707436248 | Method | `protected virtual System.Void Finalize()` | `System.Windows.Forms.DataGridViewBand`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E8C92A6C5180C738 | Method | `protected virtual System.Void OnDataGridViewChanged()` | `System.Windows.Forms.DataGridViewBand`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-98D92906DDCB2D17 | Method | `protected System.Void RaiseCellClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-66830DD79FD07BD4 | Method | `protected System.Void RaiseCellContentClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0D66A888A6D37144 | Method | `protected System.Void RaiseCellContentDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-332A76773D939039 | Method | `protected System.Void RaiseCellValueChanged(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ABE8A62639DB09C5 | Method | `protected System.Void RaiseDataError(System.Windows.Forms.DataGridViewDataErrorEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-68CF49935337D44F | Method | `protected System.Void RaiseMouseWheel(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6F2E05A41AAEF4EC | Property | `System.Windows.Forms.DataGridViewHeaderCell HeaderCellCore { protected get; protected set; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EEA24820BC46FEDC | Property | `System.Boolean IsRow { protected get; }` | `System.Windows.Forms.DataGridViewBand`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
