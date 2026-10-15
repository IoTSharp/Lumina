# ImageList：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/ImageList.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.ImageList`；基类：`System.ComponentModel.Component`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **10**，公开事件 **2**，公开方法 **5**；受保护扩展成员 **6**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-11C33C069693C144 | Constructor | `public ImageList()` | `System.Windows.Forms.ImageList`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AD093481C524CBFD | Constructor | `public ImageList(System.ComponentModel.IContainer container)` | `System.Windows.Forms.ImageList`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ADEC7664866C4CEF | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F200AD4786FD61C2 | Event | `public event System.EventHandler RecreateHandle` | `System.Windows.Forms.ImageList` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6E79C816AC063A7C | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CF4B610CF9644D15 | Method | `public System.Void Draw(System.Drawing.Graphics g, System.Drawing.Point pt, System.Int32 index)` | `System.Windows.Forms.ImageList`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4AA1A5AAE650FB25 | Method | `public System.Void Draw(System.Drawing.Graphics g, System.Int32 x, System.Int32 y, System.Int32 index)` | `System.Windows.Forms.ImageList`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-79A15D60471FBCD1 | Method | `public System.Void Draw(System.Drawing.Graphics g, System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Int32 index)` | `System.Windows.Forms.ImageList`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A088A272A598B4D4 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.ImageList`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CECB1E3EBA4467A4 | Property | `System.Windows.Forms.ColorDepth ColorDepth { public get; public set; }` | `System.Windows.Forms.ImageList`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F08413509437920C | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC8C43F44516C6DE | Property | `System.IntPtr Handle { public get; }` | `System.Windows.Forms.ImageList` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8B2DC41DF19F8EAD | Property | `System.Boolean HandleCreated { public get; }` | `System.Windows.Forms.ImageList` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BB6A782B2E458344 | Property | `System.Windows.Forms.ImageList+ImageCollection Images { public get; }` | `System.Windows.Forms.ImageList` DefaultValueAttribute(null); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F8FDD1C216687D08 | Property | `System.Drawing.Size ImageSize { public get; public set; }` | `System.Windows.Forms.ImageList` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B5D2ECEA6EEDD2BE | Property | `System.Windows.Forms.ImageListStreamer ImageStream { public get; public set; }` | `System.Windows.Forms.ImageList` BrowsableAttribute(false); DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B22C7494C607FCA | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2CE1053A8D3767D0 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.ImageList` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D9A16B278D07A58E | Property | `System.Drawing.Color TransparentColor { public get; public set; }` | `System.Windows.Forms.ImageList`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-C46ECB91BC5810F9 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.ImageList`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8C4537570293FACB | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B3C5A2D468234B82 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B52132FD0293AA8 | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A5558B720BF5BCBA | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-075ED5DCCEBF28D3 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
