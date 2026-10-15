# HelpProvider：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/HelpProvider.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.HelpProvider`；基类：`System.ComponentModel.Component`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **4**，公开事件 **1**，公开方法 **12**；受保护扩展成员 **6**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-F97E21764C69FEE7 | Constructor | `public HelpProvider()` | `System.Windows.Forms.HelpProvider`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B03ED87181925BB2 | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-46933A66CDC52CDF | Method | `public virtual System.Boolean CanExtend(System.Object target)` | `System.Windows.Forms.HelpProvider`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4A282EBE912D37D3 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7550E6F5028761F7 | Method | `public virtual System.String GetHelpKeyword(System.Windows.Forms.Control ctl)` | `System.Windows.Forms.HelpProvider` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C1C76881EB1A2ADA | Method | `public virtual System.Windows.Forms.HelpNavigator GetHelpNavigator(System.Windows.Forms.Control ctl)` | `System.Windows.Forms.HelpProvider` DefaultValueAttribute(-2147483643); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D19B15ADB8899BF5 | Method | `public virtual System.String GetHelpString(System.Windows.Forms.Control ctl)` | `System.Windows.Forms.HelpProvider` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-858683F2F8B0E5B1 | Method | `public virtual System.Boolean GetShowHelp(System.Windows.Forms.Control ctl)` | `System.Windows.Forms.HelpProvider` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-20382A05B32CAE88 | Method | `public virtual System.Void ResetShowHelp(System.Windows.Forms.Control ctl)` | `System.Windows.Forms.HelpProvider`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9226E7535FC6F1F1 | Method | `public virtual System.Void SetHelpKeyword(System.Windows.Forms.Control ctl, System.String keyword)` | `System.Windows.Forms.HelpProvider`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CB74205442B7F3AA | Method | `public virtual System.Void SetHelpNavigator(System.Windows.Forms.Control ctl, System.Windows.Forms.HelpNavigator navigator)` | `System.Windows.Forms.HelpProvider`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E7DB2761BBC061B3 | Method | `public virtual System.Void SetHelpString(System.Windows.Forms.Control ctl, System.String helpString)` | `System.Windows.Forms.HelpProvider`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B3F080028CD4CADA | Method | `public virtual System.Void SetShowHelp(System.Windows.Forms.Control ctl, System.Boolean value)` | `System.Windows.Forms.HelpProvider`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A6C8C6DB3E0B171 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.HelpProvider`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B28A9D6C07608218 | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0A398E3201ECB95 | Property | `virtual System.String HelpNamespace { public get; public set; }` | `System.Windows.Forms.HelpProvider` LocalizableAttribute(true); DefaultValueAttribute(null); EditorAttribute("System.Windows.Forms.Design.HelpNamespaceEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-178B65448ABCF5ED | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E54CCD2DD117B2CC | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.HelpProvider` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-4E30F5F095505F80 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-58FFCF1EA6CE93B0 | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-56E0B64DF120518B | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-015BCBDC47FD5893 | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-547E2CF780596F96 | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CB150033CF16E1E0 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
