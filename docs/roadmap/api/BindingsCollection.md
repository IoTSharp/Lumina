# BindingsCollection：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/BindingsCollection.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.BindingsCollection`；基类：`System.Windows.Forms.BaseCollection`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **5**，公开事件 **2**，公开方法 **2**；受保护扩展成员 **11**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-A7EF68B1FBBE150A | Event | `public event System.ComponentModel.CollectionChangeEventHandler CollectionChanged` | `System.Windows.Forms.BindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3641F268624F2718 | Event | `public event System.ComponentModel.CollectionChangeEventHandler CollectionChanging` | `System.Windows.Forms.BindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E7E23AE69E54DA6 | Method | `public System.Void CopyTo(System.Array ar, System.Int32 index)` | `System.Windows.Forms.BaseCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-98C2DF1B9C341497 | Method | `public System.Collections.IEnumerator GetEnumerator()` | `System.Windows.Forms.BaseCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E664D03D071588C | Property | `virtual System.Int32 Count { public get; }` | `System.Windows.Forms.BindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-81ED95F03919D1DD | Property | `System.Boolean IsReadOnly { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F1006796A1877EFA | Property | `System.Boolean IsSynchronized { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4B3C77FC0E8425AB | Property | `System.Windows.Forms.Binding Item[System.Int32 index] { public get; }` | `System.Windows.Forms.BindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6B7D7D3ED613DF16 | Property | `System.Object SyncRoot { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-2B0A39EB259573A6 | Method | `protected internal System.Void Add(System.Windows.Forms.Binding binding)` | `System.Windows.Forms.BindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FC97332AFAB782E1 | Method | `protected virtual System.Void AddCore(System.Windows.Forms.Binding dataBinding)` | `System.Windows.Forms.BindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7803CCC39B528057 | Method | `protected internal System.Void Clear()` | `System.Windows.Forms.BindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-456DE4D0A81248D5 | Method | `protected virtual System.Void ClearCore()` | `System.Windows.Forms.BindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-440FF0D31B285243 | Method | `protected virtual System.Void OnCollectionChanged(System.ComponentModel.CollectionChangeEventArgs ccevent)` | `System.Windows.Forms.BindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-68EEA1AAED4B567C | Method | `protected virtual System.Void OnCollectionChanging(System.ComponentModel.CollectionChangeEventArgs e)` | `System.Windows.Forms.BindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5C1060063B854802 | Method | `protected internal System.Void Remove(System.Windows.Forms.Binding binding)` | `System.Windows.Forms.BindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4877275ECC947BF4 | Method | `protected internal System.Void RemoveAt(System.Int32 index)` | `System.Windows.Forms.BindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FA5125662BE84556 | Method | `protected virtual System.Void RemoveCore(System.Windows.Forms.Binding dataBinding)` | `System.Windows.Forms.BindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-94D82DE5ABEE434A | Method | `protected internal System.Boolean ShouldSerializeMyAll()` | `System.Windows.Forms.BindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CB4F272C3ED47B6E | Property | `virtual System.Collections.ArrayList List { protected get; }` | `System.Windows.Forms.BindingsCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
