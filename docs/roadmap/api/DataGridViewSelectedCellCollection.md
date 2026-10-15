# DataGridViewSelectedCellCollection：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/DataGridViewSelectedCellCollection.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.DataGridViewSelectedCellCollection`；基类：`System.Windows.Forms.BaseCollection`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **5**，公开事件 **0**，公开方法 **6**；受保护扩展成员 **1**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-00AC6A03BEE7FAC7 | Method | `public System.Void Clear()` | `System.Windows.Forms.DataGridViewSelectedCellCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BA5FB078F58C033B | Method | `public System.Boolean Contains(System.Windows.Forms.DataGridViewCell dataGridViewCell)` | `System.Windows.Forms.DataGridViewSelectedCellCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B16EF2B110407022 | Method | `public System.Void CopyTo(System.Array ar, System.Int32 index)` | `System.Windows.Forms.BaseCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A7B3D0C5FB073201 | Method | `public System.Void CopyTo(System.Windows.Forms.DataGridViewCell[] array, System.Int32 index)` | `System.Windows.Forms.DataGridViewSelectedCellCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5AC7808BFC2900CB | Method | `public System.Collections.IEnumerator GetEnumerator()` | `System.Windows.Forms.BaseCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-412B95E1D4D3EE45 | Method | `public System.Void Insert(System.Int32 index, System.Windows.Forms.DataGridViewCell dataGridViewCell)` | `System.Windows.Forms.DataGridViewSelectedCellCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-355C5B9FB0423B3F | Property | `virtual System.Int32 Count { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DAB1243AC3CB0901 | Property | `System.Boolean IsReadOnly { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-880D9838E1601D72 | Property | `System.Boolean IsSynchronized { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DEF4C99C971F5E83 | Property | `System.Windows.Forms.DataGridViewCell Item[System.Int32 index] { public get; }` | `System.Windows.Forms.DataGridViewSelectedCellCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-817809FB756A1764 | Property | `System.Object SyncRoot { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-21880BD2BE4FD26B | Property | `virtual System.Collections.ArrayList List { protected get; }` | `System.Windows.Forms.DataGridViewSelectedCellCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
