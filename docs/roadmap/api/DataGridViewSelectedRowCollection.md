# DataGridViewSelectedRowCollection：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/DataGridViewSelectedRowCollection.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.DataGridViewSelectedRowCollection`；基类：`System.Windows.Forms.BaseCollection`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **5**，公开事件 **0**，公开方法 **6**；受保护扩展成员 **1**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-474085AA0E6C3D1A | Method | `public System.Void Clear()` | `System.Windows.Forms.DataGridViewSelectedRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D2C174E7CE8DC1C | Method | `public System.Boolean Contains(System.Windows.Forms.DataGridViewRow dataGridViewRow)` | `System.Windows.Forms.DataGridViewSelectedRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A1917CF4B74E605 | Method | `public System.Void CopyTo(System.Array ar, System.Int32 index)` | `System.Windows.Forms.BaseCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5B2A1776409FB801 | Method | `public System.Void CopyTo(System.Windows.Forms.DataGridViewRow[] array, System.Int32 index)` | `System.Windows.Forms.DataGridViewSelectedRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6E77D38F11B61616 | Method | `public System.Collections.IEnumerator GetEnumerator()` | `System.Windows.Forms.BaseCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DA2D91F59115D254 | Method | `public System.Void Insert(System.Int32 index, System.Windows.Forms.DataGridViewRow dataGridViewRow)` | `System.Windows.Forms.DataGridViewSelectedRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-56524C6CD0F3E775 | Property | `virtual System.Int32 Count { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A4DCB7136ADBA75B | Property | `System.Boolean IsReadOnly { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CCAFFC52E24D8081 | Property | `System.Boolean IsSynchronized { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6194F7BDFF06048A | Property | `System.Windows.Forms.DataGridViewRow Item[System.Int32 index] { public get; }` | `System.Windows.Forms.DataGridViewSelectedRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F52A86A2899FD6A9 | Property | `System.Object SyncRoot { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-E77D7A9AD01552FE | Property | `virtual System.Collections.ArrayList List { protected get; }` | `System.Windows.Forms.DataGridViewSelectedRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
