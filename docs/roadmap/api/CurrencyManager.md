# CurrencyManager：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/CurrencyManager.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.CurrencyManager`；基类：`System.Windows.Forms.BindingManagerBase`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **6**，公开事件 **8**，公开方法 **8**；受保护扩展成员 **18**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-6C0E474C05D53124 | Event | `public event System.Windows.Forms.BindingCompleteEventHandler BindingComplete` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-05E88B0A0E9B0C8F | Event | `public event System.EventHandler CurrentChanged` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8BAB0886BE1B361C | Event | `public event System.EventHandler CurrentItemChanged` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CC4A7F95488F18C3 | Event | `public event System.Windows.Forms.BindingManagerDataErrorEventHandler DataError` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5BE3987999B03C41 | Event | `public event System.Windows.Forms.ItemChangedEventHandler ItemChanged` | `System.Windows.Forms.CurrencyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-85A4173044296A20 | Event | `public event System.ComponentModel.ListChangedEventHandler ListChanged` | `System.Windows.Forms.CurrencyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C5F3255CF31CFA2E | Event | `public event System.EventHandler MetaDataChanged` | `System.Windows.Forms.CurrencyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-53D098D0C1CDE45C | Event | `public event System.EventHandler PositionChanged` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-65F4CB00D5C10EE9 | Method | `public virtual System.Void AddNew()` | `System.Windows.Forms.CurrencyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F1C2FD5B44B934D2 | Method | `public virtual System.Void CancelCurrentEdit()` | `System.Windows.Forms.CurrencyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5F0C79E908E9F889 | Method | `public virtual System.Void EndCurrentEdit()` | `System.Windows.Forms.CurrencyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BA3D02532D2D1232 | Method | `public virtual System.ComponentModel.PropertyDescriptorCollection GetItemProperties()` | `System.Windows.Forms.CurrencyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-31343F4FC6B63275 | Method | `public System.Void Refresh()` | `System.Windows.Forms.CurrencyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3DE77015AC07CF22 | Method | `public virtual System.Void RemoveAt(System.Int32 index)` | `System.Windows.Forms.CurrencyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F79BED032E105CCA | Method | `public virtual System.Void ResumeBinding()` | `System.Windows.Forms.CurrencyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-49F9890748681257 | Method | `public virtual System.Void SuspendBinding()` | `System.Windows.Forms.CurrencyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-983EE4A07B711D8D | Property | `System.Windows.Forms.BindingsCollection Bindings { public get; }` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-03FC027887B9FA23 | Property | `virtual System.Int32 Count { public get; }` | `System.Windows.Forms.CurrencyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-032C3CBFF0FA20ED | Property | `virtual System.Object Current { public get; }` | `System.Windows.Forms.CurrencyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F5A7F46B4E667625 | Property | `System.Boolean IsBindingSuspended { public get; }` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8AB0FFD4E50822C7 | Property | `System.Collections.IList List { public get; }` | `System.Windows.Forms.CurrencyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D35685A090DA9857 | Property | `virtual System.Int32 Position { public get; public set; }` | `System.Windows.Forms.CurrencyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-CD61E363F1A42895 | Field | `protected System.Type finalType` | `System.Windows.Forms.CurrencyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DF3AF4B51763DC35 | Field | `protected System.Int32 listposition` | `System.Windows.Forms.CurrencyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D9C0E1BF5D4FB855 | Field | `protected System.EventHandler onCurrentChangedHandler` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AAC6946CB5F3CA67 | Field | `protected System.EventHandler onPositionChangedHandler` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A30B1A98D8ED0E41 | Method | `protected System.Void CheckEmpty()` | `System.Windows.Forms.CurrencyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D54304DC89901BB0 | Method | `protected internal virtual System.ComponentModel.PropertyDescriptorCollection GetItemProperties(System.Collections.ArrayList dataSources, System.Collections.ArrayList listAccessors)` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-791D60C7F27963FC | Method | `protected virtual System.ComponentModel.PropertyDescriptorCollection GetItemProperties(System.Type listType, System.Int32 offset, System.Collections.ArrayList dataSources, System.Collections.ArrayList listAccessors)` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E9BF4BFA0DD6FF64 | Method | `protected internal virtual System.String GetListName(System.Collections.ArrayList listAccessors)` | `System.Windows.Forms.CurrencyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F8A26215D42936DD | Method | `protected internal System.Void OnBindingComplete(System.Windows.Forms.BindingCompleteEventArgs args)` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4296213EEC782829 | Method | `protected internal virtual System.Void OnCurrentChanged(System.EventArgs e)` | `System.Windows.Forms.CurrencyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B73C7C402F8D96CC | Method | `protected internal virtual System.Void OnCurrentItemChanged(System.EventArgs e)` | `System.Windows.Forms.CurrencyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-05E5EF8247DD80D7 | Method | `protected internal System.Void OnDataError(System.Exception e)` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-78AEBA16EC9932F5 | Method | `protected virtual System.Void OnItemChanged(System.Windows.Forms.ItemChangedEventArgs e)` | `System.Windows.Forms.CurrencyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B5B9C63FC596260 | Method | `protected internal System.Void OnMetaDataChanged(System.EventArgs e)` | `System.Windows.Forms.CurrencyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F1BF0E8E3668AC37 | Method | `protected virtual System.Void OnPositionChanged(System.EventArgs e)` | `System.Windows.Forms.CurrencyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0ABEDD39DF9C6E0B | Method | `protected System.Void PullData()` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-73EBD099A18EEBCE | Method | `protected System.Void PushData()` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6F9F7D17DDD017CA | Method | `protected virtual System.Void UpdateIsBinding()` | `System.Windows.Forms.CurrencyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
