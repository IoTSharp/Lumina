# ColumnHeader：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/ColumnHeader.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.ColumnHeader`；基类：`System.ComponentModel.Component`。Lumina 同名类型：🟡 存在；基类及基线接口形状：⬜ 未匹配。

公开属性 **13**，公开事件 **1**，公开方法 **4**；受保护扩展成员 **6**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-E4DEAD6F5A9DA5D9 | Constructor | `public ColumnHeader()` | `System.Windows.Forms.ColumnHeader`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7742F4B9B922D405 | Constructor | `public ColumnHeader(System.Int32 imageIndex)` | `System.Windows.Forms.ColumnHeader`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-40E4D6DE3E4DE1FB | Constructor | `public ColumnHeader(System.String imageKey)` | `System.Windows.Forms.ColumnHeader`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-507755009D02A746 | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C40251393E3DAC8A | Method | `public System.Void AutoResize(System.Windows.Forms.ColumnHeaderAutoResizeStyle headerAutoResize)` | `System.Windows.Forms.ColumnHeader`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-758877A9074F8C0D | Method | `public System.Object Clone()` | `System.Windows.Forms.ColumnHeader`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7DA5DDFD2C73BFEE | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EB01FC79E7FA1560 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.ColumnHeader`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-870478E43B6DD7BB | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E186D7391776147A | Property | `System.Int32 DisplayIndex { public get; public set; }` | `System.Windows.Forms.ColumnHeader` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AB466A48E759769C | Property | `System.Int32 ImageIndex { public get; public set; }` | `System.Windows.Forms.ColumnHeader` DefaultValueAttribute(-1); TypeConverterAttribute(System.Windows.Forms.ImageIndexConverter); EditorAttribute("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A84F242CB3E6DF4D | Property | `System.String ImageKey { public get; public set; }` | `System.Windows.Forms.ColumnHeader` DefaultValueAttribute(""); TypeConverterAttribute(System.Windows.Forms.ImageKeyConverter); EditorAttribute("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-959CC51E2B33C0C6 | Property | `System.Windows.Forms.ImageList ImageList { public get; }` | `System.Windows.Forms.ColumnHeader` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6FDC8542AC415A0B | Property | `System.Int32 Index { public get; }` | `System.Windows.Forms.ColumnHeader` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4A8979B80A3A8E44 | Property | `System.Windows.Forms.ListView ListView { public get; }` | `System.Windows.Forms.ColumnHeader` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A7D1C076ADA1B447 | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.ColumnHeader` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7483CB9CB6E27668 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-826BA4A759ACCC0A | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.ColumnHeader` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C3BFE9E330AC4F1C | Property | `System.String Text { public get; public set; }` | `System.Windows.Forms.ColumnHeader` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A138D648A24F9C05 | Property | `System.Windows.Forms.HorizontalAlignment TextAlign { public get; public set; }` | `System.Windows.Forms.ColumnHeader` LocalizableAttribute(true); DefaultValueAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1BED609CF679CA98 | Property | `System.Int32 Width { public get; public set; }` | `System.Windows.Forms.ColumnHeader` LocalizableAttribute(true); DefaultValueAttribute(60) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-D2A4C69EEB92D727 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.ColumnHeader`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2AB89805DEEEE8AC | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8F52BDA193769D2C | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-69F09234816F31B8 | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7C8D78A319D5965B | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-90B7F319589F72BE | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
