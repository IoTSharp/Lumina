# ErrorProvider：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/ErrorProvider.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.ErrorProvider`；基类：`System.ComponentModel.Component`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **11**，公开事件 **2**，公开方法 **12**；受保护扩展成员 **7**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-D59ADA4035156B3B | Constructor | `public ErrorProvider()` | `System.Windows.Forms.ErrorProvider`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E7712045E69F466 | Constructor | `public ErrorProvider(System.ComponentModel.IContainer container)` | `System.Windows.Forms.ErrorProvider`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0B85DA6AFE8383E | Constructor | `public ErrorProvider(System.Windows.Forms.ContainerControl parentControl)` | `System.Windows.Forms.ErrorProvider`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E1F12F36ABC5A99C | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DC6EEABAE02231A2 | Event | `public event System.EventHandler RightToLeftChanged` | `System.Windows.Forms.ErrorProvider`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E9E7D4B3E65C8829 | Method | `public System.Void BindToDataAndErrors(System.Object newDataSource, System.String newDataMember)` | `System.Windows.Forms.ErrorProvider`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-199C8533962F46B6 | Method | `public System.Boolean CanExtend(System.Object extendee)` | `System.Windows.Forms.ErrorProvider`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-67EAAB2C92F7E93D | Method | `public System.Void Clear()` | `System.Windows.Forms.ErrorProvider`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B70F866ED0EEEC4D | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C5AAA0A6904A8010 | Method | `public System.String GetError(System.Windows.Forms.Control control)` | `System.Windows.Forms.ErrorProvider` DefaultValueAttribute(""); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B6E1EBE3991953B6 | Method | `public System.Windows.Forms.ErrorIconAlignment GetIconAlignment(System.Windows.Forms.Control control)` | `System.Windows.Forms.ErrorProvider` DefaultValueAttribute(3); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A58B8F7A1EB677B1 | Method | `public System.Int32 GetIconPadding(System.Windows.Forms.Control control)` | `System.Windows.Forms.ErrorProvider` DefaultValueAttribute(0); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EACC7AC39AEB1BC0 | Method | `public System.Void SetError(System.Windows.Forms.Control control, System.String value)` | `System.Windows.Forms.ErrorProvider`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A44859E8F01B4648 | Method | `public System.Void SetIconAlignment(System.Windows.Forms.Control control, System.Windows.Forms.ErrorIconAlignment value)` | `System.Windows.Forms.ErrorProvider`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8E1C6AEF5974B5F3 | Method | `public System.Void SetIconPadding(System.Windows.Forms.Control control, System.Int32 padding)` | `System.Windows.Forms.ErrorProvider`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-07498ABEAB0D7701 | Method | `public virtual System.String ToString()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B545F7A573A24D3A | Method | `public System.Void UpdateBinding()` | `System.Windows.Forms.ErrorProvider`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9F02028DBBB36A97 | Property | `System.Int32 BlinkRate { public get; public set; }` | `System.Windows.Forms.ErrorProvider` DefaultValueAttribute(250) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-800DE102C1FCA886 | Property | `System.Windows.Forms.ErrorBlinkStyle BlinkStyle { public get; public set; }` | `System.Windows.Forms.ErrorProvider` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3968F6965D4B7C99 | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-802CCFA749E1C723 | Property | `System.Windows.Forms.ContainerControl ContainerControl { public get; public set; }` | `System.Windows.Forms.ErrorProvider` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F7182B2B37A05486 | Property | `System.String DataMember { public get; public set; }` | `System.Windows.Forms.ErrorProvider` DefaultValueAttribute(null); EditorAttribute("System.Windows.Forms.Design.DataMemberListEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C5C2FE0654516A5F | Property | `System.Object DataSource { public get; public set; }` | `System.Windows.Forms.ErrorProvider` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-384C4E65D0829A0C | Property | `System.Boolean HasErrors { public get; }` | `System.Windows.Forms.ErrorProvider`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C8930840DD9FAD6 | Property | `System.Drawing.Icon Icon { public get; public set; }` | `System.Windows.Forms.ErrorProvider` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C2D5D0D4BFC053E3 | Property | `virtual System.Boolean RightToLeft { public get; public set; }` | `System.Windows.Forms.ErrorProvider` LocalizableAttribute(true); DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-209F392FAD614071 | Property | `virtual System.ComponentModel.ISite Site { public set; }` | `System.Windows.Forms.ErrorProvider`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-843A5C650ADAA414 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.ErrorProvider` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-72392F7CE4C249DA | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.ErrorProvider`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9811D472C09E329D | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FCBE4D280E8EB9C8 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9843738AFC18E9D5 | Method | `protected virtual System.Void OnRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.ErrorProvider`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BE904A83CD352FD0 | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E27D4A0F97FD7B3 | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D5E65B48627E0D9 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
