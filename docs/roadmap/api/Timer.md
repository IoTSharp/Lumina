# Timer：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/Timer.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.Timer`；基类：`System.ComponentModel.Component`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **5**，公开事件 **2**，公开方法 **4**；受保护扩展成员 **7**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-3706932C584DD972 | Constructor | `public Timer()` | `System.Windows.Forms.Timer`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5A8A844698D64ED6 | Constructor | `public Timer(System.ComponentModel.IContainer container)` | `System.Windows.Forms.Timer`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7BCDEC9AB1285F6F | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C9F4DB9489256765 | Event | `public event System.EventHandler Tick` | `System.Windows.Forms.Timer`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A96F28DB3F8F02A2 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5CD5CF2CCD8E3BA3 | Method | `public System.Void Start()` | `System.Windows.Forms.Timer`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2996200B849CC39B | Method | `public System.Void Stop()` | `System.Windows.Forms.Timer`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3BEE3408C9897013 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.Timer`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B276D0475DF7A3BD | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3041E4367A7C11F6 | Property | `virtual System.Boolean Enabled { public get; public set; }` | `System.Windows.Forms.Timer` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AB5DE6F87292DE6D | Property | `System.Int32 Interval { public get; public set; }` | `System.Windows.Forms.Timer` DefaultValueAttribute(100) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4AC84E32EE7B88FA | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-85D579DA81A1CA2A | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.Timer` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-C1C675F3AA947C43 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.Timer`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1514F861A7D79C75 | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5A8581F9A4625579 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EDCEA06C43F5C269 | Method | `protected virtual System.Void OnTick(System.EventArgs e)` | `System.Windows.Forms.Timer`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C89FFF74245FCD9D | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-32052DF26176E4A0 | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-72C20456B7BD6F74 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
