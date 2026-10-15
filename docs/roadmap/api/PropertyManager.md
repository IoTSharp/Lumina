# PropertyManager：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/PropertyManager.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.PropertyManager`；基类：`System.Windows.Forms.BindingManagerBase`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **5**，公开事件 **5**，公开方法 **7**；受保护扩展成员 **12**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-54ABF5FC4F10AD55 | Constructor | `public PropertyManager()` | `System.Windows.Forms.PropertyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D0D0AC1B8B52433E | Event | `public event System.Windows.Forms.BindingCompleteEventHandler BindingComplete` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-71BED5F300C8CACC | Event | `public event System.EventHandler CurrentChanged` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F572DF94B1D3558D | Event | `public event System.EventHandler CurrentItemChanged` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E82D0DE4219BA1DF | Event | `public event System.Windows.Forms.BindingManagerDataErrorEventHandler DataError` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E4290915F2968FA0 | Event | `public event System.EventHandler PositionChanged` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A73051D23490BEC0 | Method | `public virtual System.Void AddNew()` | `System.Windows.Forms.PropertyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3D041853FDB4A9B1 | Method | `public virtual System.Void CancelCurrentEdit()` | `System.Windows.Forms.PropertyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4CDDCCCDA111813C | Method | `public virtual System.Void EndCurrentEdit()` | `System.Windows.Forms.PropertyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-53E530FFA6881D36 | Method | `public virtual System.ComponentModel.PropertyDescriptorCollection GetItemProperties()` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-42D9A43BC4FF8322 | Method | `public virtual System.Void RemoveAt(System.Int32 index)` | `System.Windows.Forms.PropertyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DF22D886928D0E75 | Method | `public virtual System.Void ResumeBinding()` | `System.Windows.Forms.PropertyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29A60C22EB65021A | Method | `public virtual System.Void SuspendBinding()` | `System.Windows.Forms.PropertyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-18FD37507175EC62 | Property | `System.Windows.Forms.BindingsCollection Bindings { public get; }` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B6475ED8B113F595 | Property | `virtual System.Int32 Count { public get; }` | `System.Windows.Forms.PropertyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-947D45545B9C1023 | Property | `virtual System.Object Current { public get; }` | `System.Windows.Forms.PropertyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4CC09013583099CD | Property | `System.Boolean IsBindingSuspended { public get; }` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EF65BCD9E83CDA5E | Property | `virtual System.Int32 Position { public get; public set; }` | `System.Windows.Forms.PropertyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-7FD4DF42DDAC9103 | Field | `protected System.EventHandler onCurrentChangedHandler` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-85AA8F1197FE59D7 | Field | `protected System.EventHandler onPositionChangedHandler` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9AC8377BDEAC9789 | Method | `protected internal virtual System.ComponentModel.PropertyDescriptorCollection GetItemProperties(System.Collections.ArrayList dataSources, System.Collections.ArrayList listAccessors)` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-95D8DC435A7FC4A5 | Method | `protected virtual System.ComponentModel.PropertyDescriptorCollection GetItemProperties(System.Type listType, System.Int32 offset, System.Collections.ArrayList dataSources, System.Collections.ArrayList listAccessors)` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7F064949A0A0878D | Method | `protected internal virtual System.String GetListName(System.Collections.ArrayList listAccessors)` | `System.Windows.Forms.PropertyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC6532E2D17D13D4 | Method | `protected internal System.Void OnBindingComplete(System.Windows.Forms.BindingCompleteEventArgs args)` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-360B31F98458E009 | Method | `protected internal virtual System.Void OnCurrentChanged(System.EventArgs ea)` | `System.Windows.Forms.PropertyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-280452E7A883F130 | Method | `protected internal virtual System.Void OnCurrentItemChanged(System.EventArgs ea)` | `System.Windows.Forms.PropertyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C11D576FDFDBC17 | Method | `protected internal System.Void OnDataError(System.Exception e)` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C27A786430AA31BB | Method | `protected System.Void PullData()` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B324151F4A508A20 | Method | `protected System.Void PushData()` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-15ECCAB3C517D152 | Method | `protected virtual System.Void UpdateIsBinding()` | `System.Windows.Forms.PropertyManager`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
