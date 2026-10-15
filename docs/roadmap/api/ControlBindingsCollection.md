# ControlBindingsCollection：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/ControlBindingsCollection.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.ControlBindingsCollection`；基类：`System.Windows.Forms.BindingsCollection`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **9**，公开事件 **2**，公开方法 **12**；受保护扩展成员 **7**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-068AA62F978E8C8B | Constructor | `public ControlBindingsCollection(System.Windows.Forms.IBindableComponent control)` | `System.Windows.Forms.ControlBindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-383134DC509916A9 | Event | `public event System.ComponentModel.CollectionChangeEventHandler CollectionChanged` | `System.Windows.Forms.BindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-35DAC0793A20B183 | Event | `public event System.ComponentModel.CollectionChangeEventHandler CollectionChanging` | `System.Windows.Forms.BindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EA1FCDA009ED4832 | Method | `public System.Void Add(System.Windows.Forms.Binding binding)` | `System.Windows.Forms.ControlBindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8959A93F769446D6 | Method | `public System.Windows.Forms.Binding Add(System.String propertyName, System.Object dataSource, System.String dataMember)` | `System.Windows.Forms.ControlBindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-328893F9B98B1FE0 | Method | `public System.Windows.Forms.Binding Add(System.String propertyName, System.Object dataSource, System.String dataMember, System.Boolean formattingEnabled)` | `System.Windows.Forms.ControlBindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BB61AFABA7B90C27 | Method | `public System.Windows.Forms.Binding Add(System.String propertyName, System.Object dataSource, System.String dataMember, System.Boolean formattingEnabled, System.Windows.Forms.DataSourceUpdateMode updateMode)` | `System.Windows.Forms.ControlBindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C97C097DFCA8A633 | Method | `public System.Windows.Forms.Binding Add(System.String propertyName, System.Object dataSource, System.String dataMember, System.Boolean formattingEnabled, System.Windows.Forms.DataSourceUpdateMode updateMode, System.Object nullValue)` | `System.Windows.Forms.ControlBindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6D1C236D02A90A0B | Method | `public System.Windows.Forms.Binding Add(System.String propertyName, System.Object dataSource, System.String dataMember, System.Boolean formattingEnabled, System.Windows.Forms.DataSourceUpdateMode updateMode, System.Object nullValue, System.String formatString)` | `System.Windows.Forms.ControlBindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D097AE586F3451A | Method | `public System.Windows.Forms.Binding Add(System.String propertyName, System.Object dataSource, System.String dataMember, System.Boolean formattingEnabled, System.Windows.Forms.DataSourceUpdateMode updateMode, System.Object nullValue, System.String formatString, System.IFormatProvider formatInfo)` | `System.Windows.Forms.ControlBindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-64A89C668CB04F45 | Method | `public System.Void Clear()` | `System.Windows.Forms.ControlBindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E94A9A4BCFA5C6C | Method | `public System.Void CopyTo(System.Array ar, System.Int32 index)` | `System.Windows.Forms.BaseCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B12FAA05EBE2924F | Method | `public System.Collections.IEnumerator GetEnumerator()` | `System.Windows.Forms.BaseCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D5439ACA442FBCA0 | Method | `public System.Void Remove(System.Windows.Forms.Binding binding)` | `System.Windows.Forms.ControlBindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-85EE15057EEBC7B7 | Method | `public System.Void RemoveAt(System.Int32 index)` | `System.Windows.Forms.ControlBindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ACC1814ABFEBEA9C | Property | `System.Windows.Forms.IBindableComponent BindableComponent { public get; }` | `System.Windows.Forms.ControlBindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-07FC2AFE680C5A2C | Property | `System.Windows.Forms.Control Control { public get; }` | `System.Windows.Forms.ControlBindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-01AEEBC446659DBB | Property | `virtual System.Int32 Count { public get; }` | `System.Windows.Forms.BindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9378D040E8EFDC68 | Property | `System.Windows.Forms.DataSourceUpdateMode DefaultDataSourceUpdateMode { public get; public set; }` | `System.Windows.Forms.ControlBindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1891E4C6320275B8 | Property | `System.Boolean IsReadOnly { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7F9F5ACEE5F7AC46 | Property | `System.Boolean IsSynchronized { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2DB9FBA45F16A51E | Property | `System.Windows.Forms.Binding Item[System.Int32 index] { public get; }` | `System.Windows.Forms.BindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B1B11FD856B7C401 | Property | `System.Windows.Forms.Binding Item[System.String propertyName] { public get; }` | `System.Windows.Forms.ControlBindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E7C4E299B623CA1 | Property | `System.Object SyncRoot { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-B81553AB147D121E | Method | `protected virtual System.Void AddCore(System.Windows.Forms.Binding dataBinding)` | `System.Windows.Forms.ControlBindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F580D08D52C4F63 | Method | `protected virtual System.Void ClearCore()` | `System.Windows.Forms.ControlBindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CC14281EB0EC8B7E | Method | `protected virtual System.Void OnCollectionChanged(System.ComponentModel.CollectionChangeEventArgs ccevent)` | `System.Windows.Forms.BindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5C6B070387207EB5 | Method | `protected virtual System.Void OnCollectionChanging(System.ComponentModel.CollectionChangeEventArgs e)` | `System.Windows.Forms.BindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1738B46CE76B97C0 | Method | `protected virtual System.Void RemoveCore(System.Windows.Forms.Binding dataBinding)` | `System.Windows.Forms.ControlBindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3DD8E579561BFCC4 | Method | `protected internal System.Boolean ShouldSerializeMyAll()` | `System.Windows.Forms.BindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D6AB0C80BA1ADF5A | Property | `virtual System.Collections.ArrayList List { protected get; }` | `System.Windows.Forms.BindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
