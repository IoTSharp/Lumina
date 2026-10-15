# BindingManagerBase：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/BindingManagerBase.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.BindingManagerBase`；基类：`System.Object`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **5**，公开事件 **5**，公开方法 **7**；受保护扩展成员 **12**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-EE907EC1A77E4B2C | Constructor | `public BindingManagerBase()` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EE754726553DE32E | Event | `public event System.Windows.Forms.BindingCompleteEventHandler BindingComplete` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4457861074C6439F | Event | `public event System.EventHandler CurrentChanged` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-107D363A8F2C15E9 | Event | `public event System.EventHandler CurrentItemChanged` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D1323EEC7A35CF73 | Event | `public event System.Windows.Forms.BindingManagerDataErrorEventHandler DataError` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7201392FA0E9FA75 | Event | `public event System.EventHandler PositionChanged` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A9CD2D7B56C86A18 | Method | `public abstract System.Void AddNew()` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DB386E7DF4F9DD48 | Method | `public abstract System.Void CancelCurrentEdit()` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FCEF2B9C4BF76440 | Method | `public abstract System.Void EndCurrentEdit()` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4696925C6C45FDC5 | Method | `public virtual System.ComponentModel.PropertyDescriptorCollection GetItemProperties()` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8F51A9884A59E158 | Method | `public abstract System.Void RemoveAt(System.Int32 index)` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A4E5D1B2BF54461 | Method | `public abstract System.Void ResumeBinding()` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1DF96D67DE311757 | Method | `public abstract System.Void SuspendBinding()` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E4C3638744A33EFF | Property | `System.Windows.Forms.BindingsCollection Bindings { public get; }` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EB7BF81137D128A0 | Property | `abstract System.Int32 Count { public get; }` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5775EDF7AB055424 | Property | `abstract System.Object Current { public get; }` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EEE99A3F59E45E3C | Property | `System.Boolean IsBindingSuspended { public get; }` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-401712D370C68770 | Property | `abstract System.Int32 Position { public get; public set; }` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-A29B201900768AC4 | Field | `protected System.EventHandler onCurrentChangedHandler` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C123A1AE0D7F9A45 | Field | `protected System.EventHandler onPositionChangedHandler` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16BE8808387C393C | Method | `protected internal virtual System.ComponentModel.PropertyDescriptorCollection GetItemProperties(System.Collections.ArrayList dataSources, System.Collections.ArrayList listAccessors)` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A27922657F2E917C | Method | `protected virtual System.ComponentModel.PropertyDescriptorCollection GetItemProperties(System.Type listType, System.Int32 offset, System.Collections.ArrayList dataSources, System.Collections.ArrayList listAccessors)` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC3632DA10745154 | Method | `protected internal abstract System.String GetListName(System.Collections.ArrayList listAccessors)` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-45D57B101149CC8C | Method | `protected internal System.Void OnBindingComplete(System.Windows.Forms.BindingCompleteEventArgs args)` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-11C5641F3CF9EE8E | Method | `protected internal abstract System.Void OnCurrentChanged(System.EventArgs e)` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4C1FEB7255A68FEC | Method | `protected internal abstract System.Void OnCurrentItemChanged(System.EventArgs e)` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4664D3784A21095A | Method | `protected internal System.Void OnDataError(System.Exception e)` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3013B6B17A8B1A06 | Method | `protected System.Void PullData()` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B3D252D4F4ADD11 | Method | `protected System.Void PushData()` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0D94E5F100C96634 | Method | `protected abstract System.Void UpdateIsBinding()` | `System.Windows.Forms.BindingManagerBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
