# BindingSource：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/BindingSource.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.BindingSource`；基类：`System.ComponentModel.Component`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **30**，公开事件 **10**，公开方法 **34**；受保护扩展成员 **15**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-F98B08BAA6F91680 | Constructor | `public BindingSource()` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BD4EFE1D79D783F3 | Constructor | `public BindingSource(System.ComponentModel.IContainer container)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-93B01875DFFF8D4F | Constructor | `public BindingSource(System.Object dataSource, System.String dataMember)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-373981841F11F13D | Event | `public event System.ComponentModel.AddingNewEventHandler AddingNew` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E6DDB748C5A579A5 | Event | `public event System.Windows.Forms.BindingCompleteEventHandler BindingComplete` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1860B9E690C8A5EB | Event | `public event System.EventHandler CurrentChanged` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B250CBBCDE4A99B3 | Event | `public event System.EventHandler CurrentItemChanged` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-03F27203E9497FA9 | Event | `public event System.Windows.Forms.BindingManagerDataErrorEventHandler DataError` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E6C1C91D5D14DBC8 | Event | `public event System.EventHandler DataMemberChanged` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-747395777CD930FF | Event | `public event System.EventHandler DataSourceChanged` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FBBCA5DB076D1F98 | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AF93DB03D9A16831 | Event | `public event System.ComponentModel.ListChangedEventHandler ListChanged` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FDDDFAB5B0BA5997 | Event | `public event System.EventHandler PositionChanged` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC3882806393390D | Method | `public virtual System.Int32 Add(System.Object value)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-22E939FF33E4A40B | Method | `public virtual System.Object AddNew()` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3D9F0E3B4CDF98C0 | Method | `public virtual System.Void ApplySort(System.ComponentModel.ListSortDescriptionCollection sorts)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E91473D13CF98230 | Method | `public virtual System.Void ApplySort(System.ComponentModel.PropertyDescriptor property, System.ComponentModel.ListSortDirection sort)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-050BA51AC35E3DC9 | Method | `public System.Void CancelEdit()` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5F9683EB960A14B5 | Method | `public virtual System.Void Clear()` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AB81AE1E064F2370 | Method | `public virtual System.Boolean Contains(System.Object value)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-10EF9ADBD5AF5E8F | Method | `public virtual System.Void CopyTo(System.Array arr, System.Int32 index)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6A6D5A6463D6465C | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4164D38297FF5B72 | Method | `public System.Void EndEdit()` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1396CEEF01777B05 | Method | `public System.Int32 Find(System.String propertyName, System.Object key)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8BFFA5F1AC5927CE | Method | `public virtual System.Int32 Find(System.ComponentModel.PropertyDescriptor prop, System.Object key)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8124610D7DC648F6 | Method | `public virtual System.Collections.IEnumerator GetEnumerator()` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C2E11F36D447732C | Method | `public virtual System.ComponentModel.PropertyDescriptorCollection GetItemProperties(System.ComponentModel.PropertyDescriptor[] listAccessors)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E6D7CCAF9A78727A | Method | `public virtual System.String GetListName(System.ComponentModel.PropertyDescriptor[] listAccessors)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0DB1B72615C06820 | Method | `public virtual System.Windows.Forms.CurrencyManager GetRelatedCurrencyManager(System.String dataMember)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AB410946002EB772 | Method | `public virtual System.Int32 IndexOf(System.Object value)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E7354B601307DC6E | Method | `public virtual System.Void Insert(System.Int32 index, System.Object value)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3946442E12097D00 | Method | `public System.Void MoveFirst()` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5048A2D40AAB4B87 | Method | `public System.Void MoveLast()` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CE251E96D45022AB | Method | `public System.Void MoveNext()` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-91B043672964C291 | Method | `public System.Void MovePrevious()` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8466055302248862 | Method | `public virtual System.Void Remove(System.Object value)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-52DC7320C41469D5 | Method | `public virtual System.Void RemoveAt(System.Int32 index)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-780C9FCBDAED0B64 | Method | `public System.Void RemoveCurrent()` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-02E0DE24DFFDA698 | Method | `public virtual System.Void RemoveFilter()` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B521E324890B6226 | Method | `public virtual System.Void RemoveSort()` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-66A9D7D43064AD9F | Method | `public virtual System.Void ResetAllowNew()` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-738E89E74177A224 | Method | `public System.Void ResetBindings(System.Boolean metadataChanged)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-14733C7E3F9CC9A6 | Method | `public System.Void ResetCurrentItem()` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FD0E263144E74169 | Method | `public System.Void ResetItem(System.Int32 itemIndex)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-12494C3A82A25DFC | Method | `public System.Void ResumeBinding()` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-051BDE73AAA0EA81 | Method | `public System.Void SuspendBinding()` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E20052F697C940FC | Method | `public virtual System.String ToString()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D0FCE87C9DA7DCDE | Property | `virtual System.Boolean AllowEdit { public get; }` | `System.Windows.Forms.BindingSource` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8B4B27CFA1185C42 | Property | `virtual System.Boolean AllowNew { public get; public set; }` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4C2621A0F9E59B2D | Property | `virtual System.Boolean AllowRemove { public get; }` | `System.Windows.Forms.BindingSource` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5908E0D0629EBBD5 | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1E84487EAF017593 | Property | `virtual System.Int32 Count { public get; }` | `System.Windows.Forms.BindingSource` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DC5A3A3CE342FF97 | Property | `virtual System.Windows.Forms.CurrencyManager CurrencyManager { public get; }` | `System.Windows.Forms.BindingSource` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C16413F37E0052BC | Property | `System.Object Current { public get; }` | `System.Windows.Forms.BindingSource` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A612F228B051BD54 | Property | `System.String DataMember { public get; public set; }` | `System.Windows.Forms.BindingSource` DefaultValueAttribute(""); EditorAttribute("System.Windows.Forms.Design.DataMemberListEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FB447CDABA7FCC62 | Property | `System.Object DataSource { public get; public set; }` | `System.Windows.Forms.BindingSource` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-62E46F9899045CFE | Property | `virtual System.String Filter { public get; public set; }` | `System.Windows.Forms.BindingSource` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-66CCDA599942F041 | Property | `System.Boolean IsBindingSuspended { public get; }` | `System.Windows.Forms.BindingSource` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-648D2FDCDF24F99E | Property | `virtual System.Boolean IsFixedSize { public get; }` | `System.Windows.Forms.BindingSource` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2971F7A69328FA25 | Property | `virtual System.Boolean IsReadOnly { public get; }` | `System.Windows.Forms.BindingSource` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1701A57E560CA952 | Property | `virtual System.Boolean IsSorted { public get; }` | `System.Windows.Forms.BindingSource` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-28A057C6638B28FF | Property | `virtual System.Boolean IsSynchronized { public get; }` | `System.Windows.Forms.BindingSource` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-40095B2B0CEA5746 | Property | `virtual System.Object Item[System.Int32 index] { public get; public set; }` | `System.Windows.Forms.BindingSource` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-64BBCA05EEFD1A54 | Property | `System.Collections.IList List { public get; }` | `System.Windows.Forms.BindingSource` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-64CCDD92D7B1A6E2 | Property | `System.Int32 Position { public get; public set; }` | `System.Windows.Forms.BindingSource` DefaultValueAttribute(-1); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5A1AC562A47AA783 | Property | `System.Boolean RaiseListChangedEvents { public get; public set; }` | `System.Windows.Forms.BindingSource` DefaultValueAttribute(true); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FB3B2EFDC4FBC111 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-05DC81318CB52F23 | Property | `System.String Sort { public get; public set; }` | `System.Windows.Forms.BindingSource` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-111DB6C9CB5754B6 | Property | `virtual System.ComponentModel.ListSortDescriptionCollection SortDescriptions { public get; }` | `System.Windows.Forms.BindingSource` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BE59D4E01752AF9C | Property | `virtual System.ComponentModel.ListSortDirection SortDirection { public get; }` | `System.Windows.Forms.BindingSource` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29FFE9B46857717A | Property | `virtual System.ComponentModel.PropertyDescriptor SortProperty { public get; }` | `System.Windows.Forms.BindingSource` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A77CCD1C4EF9E42 | Property | `virtual System.Boolean SupportsAdvancedSorting { public get; }` | `System.Windows.Forms.BindingSource` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6750090E5B0ED205 | Property | `virtual System.Boolean SupportsChangeNotification { public get; }` | `System.Windows.Forms.BindingSource` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-10C61C38D68EFE6B | Property | `virtual System.Boolean SupportsFiltering { public get; }` | `System.Windows.Forms.BindingSource` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3639E8CE2C9DF30F | Property | `virtual System.Boolean SupportsSearching { public get; }` | `System.Windows.Forms.BindingSource` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0218AF83BBD21A43 | Property | `virtual System.Boolean SupportsSorting { public get; }` | `System.Windows.Forms.BindingSource` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-26573E1C80E75E94 | Property | `virtual System.Object SyncRoot { public get; }` | `System.Windows.Forms.BindingSource` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-CFA4F654329524B7 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3BD0C11D4B19B51A | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CC7B72D3C544648C | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B933E122ADF97C3E | Method | `protected virtual System.Void OnAddingNew(System.ComponentModel.AddingNewEventArgs e)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0CE6FE2818A672FC | Method | `protected virtual System.Void OnBindingComplete(System.Windows.Forms.BindingCompleteEventArgs e)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D948E169DAAF02C6 | Method | `protected virtual System.Void OnCurrentChanged(System.EventArgs e)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4CF6D05DC28F24EE | Method | `protected virtual System.Void OnCurrentItemChanged(System.EventArgs e)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F2022A4D4B3D104 | Method | `protected virtual System.Void OnDataError(System.Windows.Forms.BindingManagerDataErrorEventArgs e)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1E3AE3C019C58C81 | Method | `protected virtual System.Void OnDataMemberChanged(System.EventArgs e)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6305E825127BF705 | Method | `protected virtual System.Void OnDataSourceChanged(System.EventArgs e)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F8566035DF02E7ED | Method | `protected virtual System.Void OnListChanged(System.ComponentModel.ListChangedEventArgs e)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CE7CC947DD252E33 | Method | `protected virtual System.Void OnPositionChanged(System.EventArgs e)` | `System.Windows.Forms.BindingSource`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2547681DEC12132B | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-507255628F6398EF | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-61BC9C941DB3AC23 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
