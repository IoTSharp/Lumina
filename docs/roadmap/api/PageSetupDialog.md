# PageSetupDialog：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/PageSetupDialog.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.PageSetupDialog`；基类：`System.Windows.Forms.CommonDialog`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **14**，公开事件 **2**，公开方法 **5**；受保护扩展成员 **10**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-11AEE1AA8F548429 | Constructor | `public PageSetupDialog()` | `System.Windows.Forms.PageSetupDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6E7394411D5DD7EA | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C1DAB12B5053AE84 | Event | `public event System.EventHandler HelpRequest` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5CD35886A8BD9639 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C3F750334918640C | Method | `public virtual System.Void Reset()` | `System.Windows.Forms.PageSetupDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4CC0D934F8C9F6E8 | Method | `public System.Windows.Forms.DialogResult ShowDialog()` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F8BAE6569F3EA5C4 | Method | `public System.Windows.Forms.DialogResult ShowDialog(System.Windows.Forms.IWin32Window owner)` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6AB47A85FDC35B8E | Method | `public virtual System.String ToString()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0554691406224477 | Property | `System.Boolean AllowMargins { public get; public set; }` | `System.Windows.Forms.PageSetupDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-87B7C5C0620A6F51 | Property | `System.Boolean AllowOrientation { public get; public set; }` | `System.Windows.Forms.PageSetupDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E59037AA5CE671C | Property | `System.Boolean AllowPaper { public get; public set; }` | `System.Windows.Forms.PageSetupDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B485675CEF40698D | Property | `System.Boolean AllowPrinter { public get; public set; }` | `System.Windows.Forms.PageSetupDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3480C0432B10ACCD | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A216BBA503B1FEE1 | Property | `System.Drawing.Printing.PrintDocument Document { public get; public set; }` | `System.Windows.Forms.PageSetupDialog` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8DF030B642AB7B7B | Property | `System.Boolean EnableMetric { public get; public set; }` | `System.Windows.Forms.PageSetupDialog` DefaultValueAttribute(false); BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B877A39D523D99A | Property | `System.Drawing.Printing.Margins MinMargins { public get; public set; }` | `System.Windows.Forms.PageSetupDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-007F8D201B34DD51 | Property | `System.Drawing.Printing.PageSettings PageSettings { public get; public set; }` | `System.Windows.Forms.PageSetupDialog` DefaultValueAttribute(null); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C78F11CD4627CAB1 | Property | `System.Drawing.Printing.PrinterSettings PrinterSettings { public get; public set; }` | `System.Windows.Forms.PageSetupDialog` DefaultValueAttribute(null); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-53460359450A7B85 | Property | `System.Boolean ShowHelp { public get; public set; }` | `System.Windows.Forms.PageSetupDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D5C5DF7C7EDFBDD7 | Property | `System.Boolean ShowNetwork { public get; public set; }` | `System.Windows.Forms.PageSetupDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AAE231F9F5DDEB90 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-762B6977667D288A | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.CommonDialog` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-00CB269D1A0F32F1 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-635E0F12D696B0D1 | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-99E809DA2DB03387 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5858F48453136D5C | Method | `protected virtual System.IntPtr HookProc(System.IntPtr hWnd, System.Int32 msg, System.IntPtr wparam, System.IntPtr lparam)` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-35D868E9A8AD4E3C | Method | `protected virtual System.Void OnHelpRequest(System.EventArgs e)` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-712478CE358CB909 | Method | `protected virtual System.IntPtr OwnerWndProc(System.IntPtr hWnd, System.Int32 msg, System.IntPtr wparam, System.IntPtr lparam)` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C4101793F9B24B90 | Method | `protected virtual System.Boolean RunDialog(System.IntPtr hwndOwner)` | `System.Windows.Forms.PageSetupDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5A9215FC28451ED1 | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0483F3817D4C383C | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B79B1FBD47FF3C2C | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
