# ToolBarButton：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/ToolBarButton.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.ToolBarButton`；基类：`System.ComponentModel.Component`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **16**，公开事件 **1**，公开方法 **2**；受保护扩展成员 **6**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-D73CF59362031A4F | Constructor | `public ToolBarButton()` | `System.Windows.Forms.ToolBarButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5A4B74FDF8DBAD3D | Constructor | `public ToolBarButton(System.String text)` | `System.Windows.Forms.ToolBarButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BD00CAA4AA1F4308 | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-89CA56E4973B46A5 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A47637212AE964EC | Method | `public virtual System.String ToString()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1E45FF716F5A1FD5 | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7B8622D04EF97FA0 | Property | `System.Windows.Forms.Menu DropDownMenu { public get; public set; }` | `System.Windows.Forms.ToolBarButton` DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.ReferenceConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-46BA16A6F7C3A0D5 | Property | `System.Boolean Enabled { public get; public set; }` | `System.Windows.Forms.ToolBarButton` DefaultValueAttribute(true); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EFA0693EB864DA6E | Property | `System.Int32 ImageIndex { public get; public set; }` | `System.Windows.Forms.ToolBarButton` TypeConverterAttribute(System.Windows.Forms.ImageIndexConverter); EditorAttribute("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); DefaultValueAttribute(-1); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E470A4508C6E53D7 | Property | `System.String ImageKey { public get; public set; }` | `System.Windows.Forms.ToolBarButton` TypeConverterAttribute(System.Windows.Forms.ImageKeyConverter); EditorAttribute("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); DefaultValueAttribute(""); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0CCD896E0A0C760 | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.ToolBarButton` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DE83F8A5B977A75E | Property | `System.Windows.Forms.ToolBar Parent { public get; }` | `System.Windows.Forms.ToolBarButton` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B6023807B7B6E2DC | Property | `System.Boolean PartialPush { public get; public set; }` | `System.Windows.Forms.ToolBarButton` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4327FF2138EB3B16 | Property | `System.Boolean Pushed { public get; public set; }` | `System.Windows.Forms.ToolBarButton` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1D28C3C01C26E7ED | Property | `System.Drawing.Rectangle Rectangle { public get; }` | `System.Windows.Forms.ToolBarButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3CAF7CDF513FEEE2 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC488236FADBB3D8 | Property | `System.Windows.Forms.ToolBarButtonStyle Style { public get; public set; }` | `System.Windows.Forms.ToolBarButton` DefaultValueAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AE988AC9633E099B | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.ToolBarButton` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-38AF41A7D462F7DC | Property | `System.String Text { public get; public set; }` | `System.Windows.Forms.ToolBarButton` LocalizableAttribute(true); DefaultValueAttribute("") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F936B4C61BA00A9C | Property | `System.String ToolTipText { public get; public set; }` | `System.Windows.Forms.ToolBarButton` LocalizableAttribute(true); DefaultValueAttribute("") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B3172EF036FD1703 | Property | `System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.ToolBarButton` DefaultValueAttribute(true); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-E66E007A69EC497D | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-793910A9D7C17D0B | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8C758B5C41C1E892 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC6BDCA66E0D3901 | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-33E12692A4B3ECBF | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2C55843EB77BA2C5 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
