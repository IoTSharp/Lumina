# BindingContext：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/BindingContext.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.BindingContext`；基类：`System.Object`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **3**，公开事件 **1**，公开方法 **3**；受保护扩展成员 **7**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-7CAB7931B95C91B2 | Constructor | `public BindingContext()` | `System.Windows.Forms.BindingContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C4DBB934F31FE2C | Event | `public event System.ComponentModel.CollectionChangeEventHandler CollectionChanged` | `System.Windows.Forms.BindingContext` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-27B4E20295EA47DA | Method | `public System.Boolean Contains(System.Object dataSource)` | `System.Windows.Forms.BindingContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D6F7B687946143FD | Method | `public System.Boolean Contains(System.Object dataSource, System.String dataMember)` | `System.Windows.Forms.BindingContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-313BD00F9A885C5B | Method | `public static System.Void UpdateBinding(System.Windows.Forms.BindingContext newBindingContext, System.Windows.Forms.Binding binding)` | `System.Windows.Forms.BindingContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7440528A5339B414 | Property | `System.Boolean IsReadOnly { public get; }` | `System.Windows.Forms.BindingContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BE070306E8A3282C | Property | `System.Windows.Forms.BindingManagerBase Item[System.Object dataSource, System.String dataMember] { public get; }` | `System.Windows.Forms.BindingContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B31475BF85E8C7F | Property | `System.Windows.Forms.BindingManagerBase Item[System.Object dataSource] { public get; }` | `System.Windows.Forms.BindingContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-29F3CF5EDC4DD1DC | Method | `protected internal System.Void Add(System.Object dataSource, System.Windows.Forms.BindingManagerBase listManager)` | `System.Windows.Forms.BindingContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-12AEBBDCF0CA1A51 | Method | `protected virtual System.Void AddCore(System.Object dataSource, System.Windows.Forms.BindingManagerBase listManager)` | `System.Windows.Forms.BindingContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FDA6F93EC1A5D7C1 | Method | `protected internal System.Void Clear()` | `System.Windows.Forms.BindingContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C16F8EB166BFEC46 | Method | `protected virtual System.Void ClearCore()` | `System.Windows.Forms.BindingContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6FE7EC0C1981F919 | Method | `protected virtual System.Void OnCollectionChanged(System.ComponentModel.CollectionChangeEventArgs ccevent)` | `System.Windows.Forms.BindingContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-659D25E55E025B92 | Method | `protected internal System.Void Remove(System.Object dataSource)` | `System.Windows.Forms.BindingContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D79EAC613CD4DB5B | Method | `protected virtual System.Void RemoveCore(System.Object dataSource)` | `System.Windows.Forms.BindingContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
