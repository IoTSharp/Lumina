# DataGridViewSelectedColumnCollection：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/DataGridViewSelectedColumnCollection.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.DataGridViewSelectedColumnCollection`；基类：`System.Windows.Forms.BaseCollection`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **5**，公开事件 **0**，公开方法 **6**；受保护扩展成员 **1**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-68E36F0DE68D8D5B | Method | `public System.Void Clear()` | `System.Windows.Forms.DataGridViewSelectedColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-13211B8DA5B7A4E6 | Method | `public System.Boolean Contains(System.Windows.Forms.DataGridViewColumn dataGridViewColumn)` | `System.Windows.Forms.DataGridViewSelectedColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B03185DC20F9A0B3 | Method | `public System.Void CopyTo(System.Array ar, System.Int32 index)` | `System.Windows.Forms.BaseCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-51F6DE63EEEAACA7 | Method | `public System.Void CopyTo(System.Windows.Forms.DataGridViewColumn[] array, System.Int32 index)` | `System.Windows.Forms.DataGridViewSelectedColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-764EEE53B9AF4365 | Method | `public System.Collections.IEnumerator GetEnumerator()` | `System.Windows.Forms.BaseCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E28FCD3653258E89 | Method | `public System.Void Insert(System.Int32 index, System.Windows.Forms.DataGridViewColumn dataGridViewColumn)` | `System.Windows.Forms.DataGridViewSelectedColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8EE739F0A9953ACE | Property | `virtual System.Int32 Count { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DDD058E073758AAF | Property | `System.Boolean IsReadOnly { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B5E1216E5202B095 | Property | `System.Boolean IsSynchronized { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ABEE737E4A99F932 | Property | `System.Windows.Forms.DataGridViewColumn Item[System.Int32 index] { public get; }` | `System.Windows.Forms.DataGridViewSelectedColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-38412B3F223E3B78 | Property | `System.Object SyncRoot { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-001CEC2D668B057A | Property | `virtual System.Collections.ArrayList List { protected get; }` | `System.Windows.Forms.DataGridViewSelectedColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
