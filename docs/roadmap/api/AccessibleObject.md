# AccessibleObject：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/AccessibleObject.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.AccessibleObject`；基类：`System.Runtime.InteropServices.StandardOleMarshalObject`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **10**，公开事件 **0**，公开方法 **11**；受保护扩展成员 **2**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-BDB7027D202872A5 | Constructor | `public AccessibleObject()` | `System.Windows.Forms.AccessibleObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CB9198141381E65C | Method | `public virtual System.Void DoDefaultAction()` | `System.Windows.Forms.AccessibleObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D853B0FAA1237829 | Method | `public virtual System.Windows.Forms.AccessibleObject GetChild(System.Int32 index)` | `System.Windows.Forms.AccessibleObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DE443697DD451494 | Method | `public virtual System.Int32 GetChildCount()` | `System.Windows.Forms.AccessibleObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E6B072A121E4C207 | Method | `public virtual System.Windows.Forms.AccessibleObject GetFocused()` | `System.Windows.Forms.AccessibleObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6392994FE75CBB24 | Method | `public virtual System.Int32 GetHelpTopic(out System.String fileName)` | `System.Windows.Forms.AccessibleObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0855FE48499C6A24 | Method | `public virtual System.Windows.Forms.AccessibleObject GetSelected()` | `System.Windows.Forms.AccessibleObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-132BD845369B074B | Method | `public virtual System.Windows.Forms.AccessibleObject HitTest(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.AccessibleObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-014973AD3ABBF42B | Method | `public virtual System.Windows.Forms.AccessibleObject Navigate(System.Windows.Forms.AccessibleNavigation navdir)` | `System.Windows.Forms.AccessibleObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-75D214941F3F7FC8 | Method | `public System.Boolean RaiseAutomationNotification(System.Windows.Forms.Automation.AutomationNotificationKind notificationKind, System.Windows.Forms.Automation.AutomationNotificationProcessing notificationProcessing, System.String notificationText)` | `System.Windows.Forms.AccessibleObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9EB086FB3D01D59E | Method | `public virtual System.Boolean RaiseLiveRegionChanged()` | `System.Windows.Forms.AccessibleObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-455B5FD6B7CB70DE | Method | `public virtual System.Void Select(System.Windows.Forms.AccessibleSelection flags)` | `System.Windows.Forms.AccessibleObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CAD221A38C2AB5FB | Property | `virtual System.Drawing.Rectangle Bounds { public get; }` | `System.Windows.Forms.AccessibleObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AE43D1C6194AB5F5 | Property | `virtual System.String DefaultAction { public get; }` | `System.Windows.Forms.AccessibleObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9BDE8F45F7079C04 | Property | `virtual System.String Description { public get; }` | `System.Windows.Forms.AccessibleObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4BDAF6BBA9B07452 | Property | `virtual System.String Help { public get; }` | `System.Windows.Forms.AccessibleObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-378D7A39C2FE0390 | Property | `virtual System.String KeyboardShortcut { public get; }` | `System.Windows.Forms.AccessibleObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-367C668EA43D1D9D | Property | `virtual System.String Name { public get; public set; }` | `System.Windows.Forms.AccessibleObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-389C38FA3333EC77 | Property | `virtual System.Windows.Forms.AccessibleObject Parent { public get; }` | `System.Windows.Forms.AccessibleObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D69989F9F1243296 | Property | `virtual System.Windows.Forms.AccessibleRole Role { public get; }` | `System.Windows.Forms.AccessibleObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B8F08E308B3B1D95 | Property | `virtual System.Windows.Forms.AccessibleStates State { public get; }` | `System.Windows.Forms.AccessibleObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-30C4DA43BBF558D3 | Property | `virtual System.String Value { public get; public set; }` | `System.Windows.Forms.AccessibleObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-1C963DEA446E6479 | Method | `protected System.Void UseStdAccessibleObjects(System.IntPtr handle)` | `System.Windows.Forms.AccessibleObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C287E9167BF688A1 | Method | `protected System.Void UseStdAccessibleObjects(System.IntPtr handle, System.Int32 objid)` | `System.Windows.Forms.AccessibleObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
