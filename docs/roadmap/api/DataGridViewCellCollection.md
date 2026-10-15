# DataGridViewCellCollection：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/DataGridViewCellCollection.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.DataGridViewCellCollection`；基类：`System.Windows.Forms.BaseCollection`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **6**，公开事件 **1**，公开方法 **11**；受保护扩展成员 **2**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-FB78F93FC513415C | Constructor | `public DataGridViewCellCollection(System.Windows.Forms.DataGridViewRow dataGridViewRow)` | `System.Windows.Forms.DataGridViewCellCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-091C06C5EA345626 | Event | `public event System.ComponentModel.CollectionChangeEventHandler CollectionChanged` | `System.Windows.Forms.DataGridViewCellCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5F20A0C4DF5F5327 | Method | `public virtual System.Int32 Add(System.Windows.Forms.DataGridViewCell dataGridViewCell)` | `System.Windows.Forms.DataGridViewCellCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D826115A47DDA4C5 | Method | `public virtual System.Void AddRange(System.Windows.Forms.DataGridViewCell[] dataGridViewCells)` | `System.Windows.Forms.DataGridViewCellCollection` DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FDA8E67A8216AB3E | Method | `public virtual System.Void Clear()` | `System.Windows.Forms.DataGridViewCellCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-085BD4AC77DB5E0C | Method | `public virtual System.Boolean Contains(System.Windows.Forms.DataGridViewCell dataGridViewCell)` | `System.Windows.Forms.DataGridViewCellCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C4472DE741300BA0 | Method | `public System.Void CopyTo(System.Array ar, System.Int32 index)` | `System.Windows.Forms.BaseCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5F805640EF61EE82 | Method | `public System.Void CopyTo(System.Windows.Forms.DataGridViewCell[] array, System.Int32 index)` | `System.Windows.Forms.DataGridViewCellCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B64E2BA13B7B998A | Method | `public System.Collections.IEnumerator GetEnumerator()` | `System.Windows.Forms.BaseCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C5F5ABE6E27D3469 | Method | `public System.Int32 IndexOf(System.Windows.Forms.DataGridViewCell dataGridViewCell)` | `System.Windows.Forms.DataGridViewCellCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-088E25FC8B43B078 | Method | `public virtual System.Void Insert(System.Int32 index, System.Windows.Forms.DataGridViewCell dataGridViewCell)` | `System.Windows.Forms.DataGridViewCellCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EE8647082C506356 | Method | `public virtual System.Void Remove(System.Windows.Forms.DataGridViewCell cell)` | `System.Windows.Forms.DataGridViewCellCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F285780ABF65F4B2 | Method | `public virtual System.Void RemoveAt(System.Int32 index)` | `System.Windows.Forms.DataGridViewCellCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DFD0EBEB7533C85C | Property | `virtual System.Int32 Count { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5C526F0ED2A2DA48 | Property | `System.Boolean IsReadOnly { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1D22AE6797B23AC6 | Property | `System.Boolean IsSynchronized { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-982860A64064025D | Property | `System.Windows.Forms.DataGridViewCell Item[System.Int32 index] { public get; public set; }` | `System.Windows.Forms.DataGridViewCellCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D09881DFF1E9610F | Property | `System.Windows.Forms.DataGridViewCell Item[System.String columnName] { public get; public set; }` | `System.Windows.Forms.DataGridViewCellCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC783CCFBA9DB228 | Property | `System.Object SyncRoot { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-0B8C3759B636C364 | Method | `protected System.Void OnCollectionChanged(System.ComponentModel.CollectionChangeEventArgs e)` | `System.Windows.Forms.DataGridViewCellCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-98477CC96C127F3D | Property | `virtual System.Collections.ArrayList List { protected get; }` | `System.Windows.Forms.DataGridViewCellCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
