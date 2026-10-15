# PrintDocument：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/PrintDocument.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Drawing.Printing.PrintDocument`；基类：`System.ComponentModel.Component`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **7**，公开事件 **5**，公开方法 **3**；受保护扩展成员 **10**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-D2EE9D41F70D30EC | Constructor | `public PrintDocument()` | `System.Drawing.Printing.PrintDocument`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EB29C84AFAEE070A | Event | `public event System.Drawing.Printing.PrintEventHandler BeginPrint` | `System.Drawing.Printing.PrintDocument`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1C50ED9F98BD3EE8 | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B0A5F99B2B17DE1 | Event | `public event System.Drawing.Printing.PrintEventHandler EndPrint` | `System.Drawing.Printing.PrintDocument`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4AC94514A5F0007A | Event | `public event System.Drawing.Printing.PrintPageEventHandler PrintPage` | `System.Drawing.Printing.PrintDocument`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C32659B6D834724B | Event | `public event System.Drawing.Printing.QueryPageSettingsEventHandler QueryPageSettings` | `System.Drawing.Printing.PrintDocument`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-656007071AA3703E | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E55C6461958B148 | Method | `public System.Void Print()` | `System.Drawing.Printing.PrintDocument`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DCC7E8AD9EADA3AA | Method | `public virtual System.String ToString()` | `System.Drawing.Printing.PrintDocument`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E8ACBA39D7453EBB | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-754B467C23531B6C | Property | `System.Drawing.Printing.PageSettings DefaultPageSettings { public get; public set; }` | `System.Drawing.Printing.PrintDocument` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E4D57449EE75D580 | Property | `System.String DocumentName { public get; public set; }` | `System.Drawing.Printing.PrintDocument` DefaultValueAttribute("document") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-32D39E81C1B8938E | Property | `System.Boolean OriginAtMargins { public get; public set; }` | `System.Drawing.Printing.PrintDocument` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-26AB254D78ADCAB5 | Property | `System.Drawing.Printing.PrintController PrintController { public get; public set; }` | `System.Drawing.Printing.PrintDocument` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CC407302684977AE | Property | `System.Drawing.Printing.PrinterSettings PrinterSettings { public get; public set; }` | `System.Drawing.Printing.PrintDocument` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BA7C8551BA05FBE0 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-C54293F549027363 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-684E46750D2E592B | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1C64D2BE6A36B726 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-34AEA205C8B516A4 | Method | `protected internal virtual System.Void OnBeginPrint(System.Drawing.Printing.PrintEventArgs e)` | `System.Drawing.Printing.PrintDocument`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-86508FEB00701248 | Method | `protected internal virtual System.Void OnEndPrint(System.Drawing.Printing.PrintEventArgs e)` | `System.Drawing.Printing.PrintDocument`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F74501CEACE15261 | Method | `protected internal virtual System.Void OnPrintPage(System.Drawing.Printing.PrintPageEventArgs e)` | `System.Drawing.Printing.PrintDocument`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B3570956128C21E4 | Method | `protected internal virtual System.Void OnQueryPageSettings(System.Drawing.Printing.QueryPageSettingsEventArgs e)` | `System.Drawing.Printing.PrintDocument`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-81C970EBF2BB1918 | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DD5FCC8CE4FDB1C0 | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-021A619908342695 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
