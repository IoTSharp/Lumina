# GridTableStylesCollection：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/GridTableStylesCollection.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.GridTableStylesCollection`；基类：`System.Windows.Forms.BaseCollection`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **6**，公开事件 **1**，公开方法 **9**；受保护扩展成员 **2**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-E65191B04ABA7095 | Event | `public event System.ComponentModel.CollectionChangeEventHandler CollectionChanged` | `System.Windows.Forms.GridTableStylesCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1C6F2EE018EF74FD | Method | `public virtual System.Int32 Add(System.Windows.Forms.DataGridTableStyle table)` | `System.Windows.Forms.GridTableStylesCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-04E63F734F545EB2 | Method | `public virtual System.Void AddRange(System.Windows.Forms.DataGridTableStyle[] tables)` | `System.Windows.Forms.GridTableStylesCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9D5C60A70D4B8035 | Method | `public System.Void Clear()` | `System.Windows.Forms.GridTableStylesCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E9B4E88FE833AE20 | Method | `public System.Boolean Contains(System.String name)` | `System.Windows.Forms.GridTableStylesCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29364A22D3E184DD | Method | `public System.Boolean Contains(System.Windows.Forms.DataGridTableStyle table)` | `System.Windows.Forms.GridTableStylesCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5DF24B2117ED906D | Method | `public System.Void CopyTo(System.Array ar, System.Int32 index)` | `System.Windows.Forms.BaseCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AE24582E30FBDF8A | Method | `public System.Collections.IEnumerator GetEnumerator()` | `System.Windows.Forms.BaseCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A17B74A29E1C872E | Method | `public System.Void Remove(System.Windows.Forms.DataGridTableStyle table)` | `System.Windows.Forms.GridTableStylesCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A93EE50EE876F271 | Method | `public System.Void RemoveAt(System.Int32 index)` | `System.Windows.Forms.GridTableStylesCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FAD503AE438D3864 | Property | `virtual System.Int32 Count { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1BF25B39272C93F7 | Property | `System.Boolean IsReadOnly { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1F6136C130DB634B | Property | `System.Boolean IsSynchronized { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2C7FE9DF9EA36054 | Property | `System.Windows.Forms.DataGridTableStyle Item[System.Int32 index] { public get; }` | `System.Windows.Forms.GridTableStylesCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7089A34D1ED56E5A | Property | `System.Windows.Forms.DataGridTableStyle Item[System.String tableName] { public get; }` | `System.Windows.Forms.GridTableStylesCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8037AB80FA23E9F8 | Property | `System.Object SyncRoot { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-CB30B80F1562CD6D | Method | `protected System.Void OnCollectionChanged(System.ComponentModel.CollectionChangeEventArgs e)` | `System.Windows.Forms.GridTableStylesCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D8E16FB6EA384E2B | Property | `virtual System.Collections.ArrayList List { protected get; }` | `System.Windows.Forms.BaseCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
