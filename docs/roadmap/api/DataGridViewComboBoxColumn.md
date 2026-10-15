# DataGridViewComboBoxColumn：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/DataGridViewComboBoxColumn.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.DataGridViewComboBoxColumn`；基类：`System.Windows.Forms.DataGridViewColumn`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **44**，公开事件 **1**，公开方法 **4**；受保护扩展成员 **11**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-8FC955576BA41A56 | Constructor | `public DataGridViewComboBoxColumn()` | `System.Windows.Forms.DataGridViewComboBoxColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CA77406AD7BE4367 | Event | `public event System.EventHandler Disposed` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E8CF4C496DB4BFBA | Method | `public virtual System.Object Clone()` | `System.Windows.Forms.DataGridViewComboBoxColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3B1A3DDDAF1324FC | Method | `public System.Void Dispose()` | `System.Windows.Forms.DataGridViewBand`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A37BD61BB0CA648B | Method | `public virtual System.Int32 GetPreferredWidth(System.Windows.Forms.DataGridViewAutoSizeColumnMode autoSizeColumnMode, System.Boolean fixedHeight)` | `System.Windows.Forms.DataGridViewColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-90C3B5815A84376F | Method | `public virtual System.String ToString()` | `System.Windows.Forms.DataGridViewComboBoxColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5C75B8DD64C7C5DC | Property | `System.Boolean AutoComplete { public get; public set; }` | `System.Windows.Forms.DataGridViewComboBoxColumn` BrowsableAttribute(true); DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5EC28097F61636CF | Property | `System.Windows.Forms.DataGridViewAutoSizeColumnMode AutoSizeMode { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1754D984649469F5 | Property | `virtual System.Windows.Forms.DataGridViewCell CellTemplate { public get; public set; }` | `System.Windows.Forms.DataGridViewComboBoxColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-77DC316F9DAF4EC8 | Property | `System.Type CellType { public get; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6427759920E106D9 | Property | `virtual System.Windows.Forms.ContextMenuStrip ContextMenuStrip { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BED09E11200D7155 | Property | `System.Windows.Forms.DataGridView DataGridView { public get; }` | `System.Windows.Forms.DataGridViewElement` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-900B733C3379E177 | Property | `System.String DataPropertyName { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(true); DefaultValueAttribute(""); TypeConverterAttribute("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"); EditorAttribute("System.Windows.Forms.Design.DataGridViewColumnDataPropertyNameEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3CD19B9C077C69B9 | Property | `System.Object DataSource { public get; public set; }` | `System.Windows.Forms.DataGridViewComboBoxColumn` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5CD149DDDFD05FB2 | Property | `virtual System.Windows.Forms.DataGridViewCellStyle DefaultCellStyle { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B70CB00F794B8519 | Property | `System.Type DefaultHeaderCellType { public get; public set; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5F0885A647DEF271 | Property | `virtual System.Boolean Displayed { public get; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-98E0E7A128A76D8A | Property | `System.Int32 DisplayIndex { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2A0A46DAC04C3992 | Property | `System.String DisplayMember { public get; public set; }` | `System.Windows.Forms.DataGridViewComboBoxColumn` DefaultValueAttribute(""); TypeConverterAttribute("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"); EditorAttribute("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-57243B5DD2E2248F | Property | `System.Windows.Forms.DataGridViewComboBoxDisplayStyle DisplayStyle { public get; public set; }` | `System.Windows.Forms.DataGridViewComboBoxColumn` DefaultValueAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A137F3EE46BD2FF9 | Property | `System.Boolean DisplayStyleForCurrentCellOnly { public get; public set; }` | `System.Windows.Forms.DataGridViewComboBoxColumn` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-997149CF3859EE71 | Property | `System.Int32 DividerWidth { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F121874181785454 | Property | `System.Int32 DropDownWidth { public get; public set; }` | `System.Windows.Forms.DataGridViewComboBoxColumn` DefaultValueAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B636B9EC0227AE7 | Property | `System.Single FillWeight { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(100) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D090058D9269207 | Property | `System.Windows.Forms.FlatStyle FlatStyle { public get; public set; }` | `System.Windows.Forms.DataGridViewComboBoxColumn` DefaultValueAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-130CB09108562520 | Property | `virtual System.Boolean Frozen { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-658341A1FFEC7BD2 | Property | `System.Boolean HasDefaultCellStyle { public get; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BB5B9F06C852C524 | Property | `System.Windows.Forms.DataGridViewColumnHeaderCell HeaderCell { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5E5ABC41386C590E | Property | `System.String HeaderText { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-34F2C64AF12BBBAF | Property | `System.Int32 Index { public get; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16DB804D729FF6BF | Property | `System.Windows.Forms.DataGridViewAutoSizeColumnMode InheritedAutoSizeMode { public get; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3A842752A65F9695 | Property | `virtual System.Windows.Forms.DataGridViewCellStyle InheritedStyle { public get; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E3983C5FB310B0A3 | Property | `System.Boolean IsDataBound { public get; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DF3E99A5CA68105F | Property | `System.Windows.Forms.DataGridViewComboBoxCell+ObjectCollection Items { public get; }` | `System.Windows.Forms.DataGridViewComboBoxColumn` EditorAttribute("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FC298907E3E3D121 | Property | `System.Int32 MaxDropDownItems { public get; public set; }` | `System.Windows.Forms.DataGridViewComboBoxColumn` DefaultValueAttribute(8) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-33D1FEB893C4E261 | Property | `System.Int32 MinimumWidth { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(5); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2F4958260F71B866 | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6FF58CB10F37A620 | Property | `virtual System.Boolean ReadOnly { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9FBF9DB3E656C6B8 | Property | `virtual System.Windows.Forms.DataGridViewTriState Resizable { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AB6FC4F902066F16 | Property | `virtual System.Boolean Selected { public get; public set; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-685556D3F2B72F8B | Property | `System.ComponentModel.ISite Site { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8AE81E6C6A8F1D47 | Property | `System.Boolean Sorted { public get; public set; }` | `System.Windows.Forms.DataGridViewComboBoxColumn` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-47202EFFD33C09CC | Property | `System.Windows.Forms.DataGridViewColumnSortMode SortMode { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1E01B4F52D930A6D | Property | `virtual System.Windows.Forms.DataGridViewElementStates State { public get; }` | `System.Windows.Forms.DataGridViewElement` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-65EA5176EAD5FF52 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C9D812E03D783AD6 | Property | `System.String ToolTipText { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(""); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-167A4A20F1BA7E84 | Property | `System.String ValueMember { public get; public set; }` | `System.Windows.Forms.DataGridViewComboBoxColumn` DefaultValueAttribute(""); TypeConverterAttribute("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"); EditorAttribute("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F38EB61A5192F1D | Property | `System.Type ValueType { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` BrowsableAttribute(false); DefaultValueAttribute(null); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DD2AF154837E5C37 | Property | `virtual System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` DefaultValueAttribute(true); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0D3F09F109FA493C | Property | `System.Int32 Width { public get; public set; }` | `System.Windows.Forms.DataGridViewColumn` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-171951885E902EFD | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.DataGridViewColumn`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6664DA5F29532F3A | Method | `protected virtual System.Void Finalize()` | `System.Windows.Forms.DataGridViewBand`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-447A551163314A5B | Method | `protected virtual System.Void OnDataGridViewChanged()` | `System.Windows.Forms.DataGridViewBand`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-37110C1C3C0D124D | Method | `protected System.Void RaiseCellClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2689F8514F115956 | Method | `protected System.Void RaiseCellContentClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3AED094117B0FEA4 | Method | `protected System.Void RaiseCellContentDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AEA3C274CDABEA93 | Method | `protected System.Void RaiseCellValueChanged(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1DA33BA2252668EE | Method | `protected System.Void RaiseDataError(System.Windows.Forms.DataGridViewDataErrorEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-37BC043F947D1E05 | Method | `protected System.Void RaiseMouseWheel(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-635CA41CB985E9BC | Property | `System.Windows.Forms.DataGridViewHeaderCell HeaderCellCore { protected get; protected set; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-80B1F9C777ECBD1E | Property | `System.Boolean IsRow { protected get; }` | `System.Windows.Forms.DataGridViewBand`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
