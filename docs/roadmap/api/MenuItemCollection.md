# MenuItemCollection：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/MenuItemCollection.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.Menu+MenuItemCollection`；基类：`System.Object`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **4**，公开事件 **0**，公开方法 **17**；受保护扩展成员 **0**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-DDBCE2FE872EA024 | Constructor | `public MenuItemCollection(System.Windows.Forms.Menu owner)` | `System.Windows.Forms.Menu+MenuItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-28931ABE54F43048 | Method | `public virtual System.Int32 Add(System.Int32 index, System.Windows.Forms.MenuItem item)` | `System.Windows.Forms.Menu+MenuItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-67C04AE4741320AA | Method | `public virtual System.Int32 Add(System.Windows.Forms.MenuItem item)` | `System.Windows.Forms.Menu+MenuItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6114A21F8A7F80E4 | Method | `public virtual System.Windows.Forms.MenuItem Add(System.String caption)` | `System.Windows.Forms.Menu+MenuItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C7DDA6D17F7E9257 | Method | `public virtual System.Windows.Forms.MenuItem Add(System.String caption, System.EventHandler onClick)` | `System.Windows.Forms.Menu+MenuItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6D075DAC198007C1 | Method | `public virtual System.Windows.Forms.MenuItem Add(System.String caption, System.Windows.Forms.MenuItem[] items)` | `System.Windows.Forms.Menu+MenuItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C3F864FDF255708 | Method | `public virtual System.Void AddRange(System.Windows.Forms.MenuItem[] items)` | `System.Windows.Forms.Menu+MenuItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC59B974A9ABCF12 | Method | `public virtual System.Void Clear()` | `System.Windows.Forms.Menu+MenuItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-973E8CECC677001A | Method | `public System.Boolean Contains(System.Windows.Forms.MenuItem value)` | `System.Windows.Forms.Menu+MenuItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6501CBA01F4B5B64 | Method | `public virtual System.Boolean ContainsKey(System.String key)` | `System.Windows.Forms.Menu+MenuItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-75E793E8E47852DE | Method | `public System.Void CopyTo(System.Array dest, System.Int32 index)` | `System.Windows.Forms.Menu+MenuItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-98E2A72881556F08 | Method | `public System.Windows.Forms.MenuItem[] Find(System.String key, System.Boolean searchAllChildren)` | `System.Windows.Forms.Menu+MenuItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A91F913434E797F9 | Method | `public System.Collections.IEnumerator GetEnumerator()` | `System.Windows.Forms.Menu+MenuItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0886AE47D9A27824 | Method | `public System.Int32 IndexOf(System.Windows.Forms.MenuItem value)` | `System.Windows.Forms.Menu+MenuItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A47ADBC8946CFE3D | Method | `public virtual System.Int32 IndexOfKey(System.String key)` | `System.Windows.Forms.Menu+MenuItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1CBEE0327A644257 | Method | `public virtual System.Void Remove(System.Windows.Forms.MenuItem item)` | `System.Windows.Forms.Menu+MenuItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1AC5324DDB44DAA8 | Method | `public virtual System.Void RemoveAt(System.Int32 index)` | `System.Windows.Forms.Menu+MenuItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6646E4F422961EE1 | Method | `public virtual System.Void RemoveByKey(System.String key)` | `System.Windows.Forms.Menu+MenuItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5990C0B4C52733AF | Property | `System.Int32 Count { public get; }` | `System.Windows.Forms.Menu+MenuItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0CF663F5216FAB38 | Property | `System.Boolean IsReadOnly { public get; }` | `System.Windows.Forms.Menu+MenuItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B1D495471B6D1E2C | Property | `virtual System.Windows.Forms.MenuItem Item[System.Int32 index] { public get; }` | `System.Windows.Forms.Menu+MenuItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-60265E72A7CA03FC | Property | `virtual System.Windows.Forms.MenuItem Item[System.String key] { public get; }` | `System.Windows.Forms.Menu+MenuItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
