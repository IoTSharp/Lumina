# TaskDialogButtonCollection：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/TaskDialogButtonCollection.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.TaskDialogButtonCollection`；基类：`System.Collections.ObjectModel.Collection`1<System.Windows.Forms.TaskDialogButton>`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **2**，公开事件 **0**，公开方法 **10**；受保护扩展成员 **5**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-9DA2916381580855 | Constructor | `public TaskDialogButtonCollection()` | `System.Windows.Forms.TaskDialogButtonCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D558957FBF5D7D8B | Method | `public System.Void Add(System.Windows.Forms.TaskDialogButton item)` | `System.Collections.ObjectModel.Collection&#96;1<System.Windows.Forms.TaskDialogButton>`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-14AEC4C0F590CB59 | Method | `public System.Windows.Forms.TaskDialogButton Add(System.String text, System.Boolean enabled = true, System.Boolean allowCloseDialog = true)` | `System.Windows.Forms.TaskDialogButtonCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D38E561A75E5118A | Method | `public System.Void Clear()` | `System.Collections.ObjectModel.Collection&#96;1<System.Windows.Forms.TaskDialogButton>`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F8CAE8D372E7888F | Method | `public System.Boolean Contains(System.Windows.Forms.TaskDialogButton item)` | `System.Collections.ObjectModel.Collection&#96;1<System.Windows.Forms.TaskDialogButton>`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CCD438A71B82A26D | Method | `public System.Void CopyTo(System.Windows.Forms.TaskDialogButton[] array, System.Int32 index)` | `System.Collections.ObjectModel.Collection&#96;1<System.Windows.Forms.TaskDialogButton>`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-40AEDD1E849E44E9 | Method | `public System.Collections.Generic.IEnumerator&#96;1<System.Windows.Forms.TaskDialogButton> GetEnumerator()` | `System.Collections.ObjectModel.Collection&#96;1<System.Windows.Forms.TaskDialogButton>`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-89F39CDA148242EE | Method | `public System.Int32 IndexOf(System.Windows.Forms.TaskDialogButton item)` | `System.Collections.ObjectModel.Collection&#96;1<System.Windows.Forms.TaskDialogButton>`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-445BCF404C1A88EE | Method | `public System.Void Insert(System.Int32 index, System.Windows.Forms.TaskDialogButton item)` | `System.Collections.ObjectModel.Collection&#96;1<System.Windows.Forms.TaskDialogButton>`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2C194F506F78AB0C | Method | `public System.Boolean Remove(System.Windows.Forms.TaskDialogButton item)` | `System.Collections.ObjectModel.Collection&#96;1<System.Windows.Forms.TaskDialogButton>`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A5889C790393D51A | Method | `public System.Void RemoveAt(System.Int32 index)` | `System.Collections.ObjectModel.Collection&#96;1<System.Windows.Forms.TaskDialogButton>`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C6A5BF2EC283CB9C | Property | `System.Int32 Count { public get; }` | `System.Collections.ObjectModel.Collection&#96;1<System.Windows.Forms.TaskDialogButton>`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-112EB3268E4FBB4E | Property | `System.Windows.Forms.TaskDialogButton Item[System.Int32 index] { public get; public set; }` | `System.Collections.ObjectModel.Collection&#96;1<System.Windows.Forms.TaskDialogButton>`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-3C143009354094FD | Method | `protected virtual System.Void ClearItems()` | `System.Windows.Forms.TaskDialogButtonCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FBA0D9BB8EFC3057 | Method | `protected virtual System.Void InsertItem(System.Int32 index, System.Windows.Forms.TaskDialogButton item)` | `System.Windows.Forms.TaskDialogButtonCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C1061ACC3D969E9C | Method | `protected virtual System.Void RemoveItem(System.Int32 index)` | `System.Windows.Forms.TaskDialogButtonCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B23B747F7D885C6D | Method | `protected virtual System.Void SetItem(System.Int32 index, System.Windows.Forms.TaskDialogButton item)` | `System.Windows.Forms.TaskDialogButtonCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-13D0F988BF27DA6C | Property | `System.Collections.Generic.IList&#96;1<System.Windows.Forms.TaskDialogButton> Items { protected get; }` | `System.Collections.ObjectModel.Collection&#96;1<System.Windows.Forms.TaskDialogButton>`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
