# TaskDialogButton：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/TaskDialogButton.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.TaskDialogButton`；基类：`System.Windows.Forms.TaskDialogControl`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **18**，公开事件 **1**，公开方法 **4**；受保护扩展成员 **0**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-429313D578BD6D07 | Constructor | `public TaskDialogButton()` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-03B0D5234BF10D18 | Constructor | `public TaskDialogButton(System.String text, System.Boolean enabled = true, System.Boolean allowCloseDialog = true)` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E876EC102B678A39 | Event | `public event System.EventHandler Click` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BC9F3B4CBF54CA05 | Method | `public virtual System.Boolean Equals(System.Object obj)` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9F31B0A5E166A5A5 | Method | `public virtual System.Int32 GetHashCode()` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C1AFAEDA1681039F | Method | `public System.Void PerformClick()` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-68AB8DB9004954DB | Method | `public virtual System.String ToString()` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-98D8B9BB1A97D8B9 | Property | `static System.Windows.Forms.TaskDialogButton Abort { public get; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-54A3B8B05F9541C5 | Property | `System.Boolean AllowCloseDialog { public get; public set; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D0F44B80705D83EE | Property | `System.Windows.Forms.TaskDialogPage BoundPage { public get; }` | `System.Windows.Forms.TaskDialogControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9F892CCC846CE97F | Property | `static System.Windows.Forms.TaskDialogButton Cancel { public get; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F745EA96A35E565F | Property | `static System.Windows.Forms.TaskDialogButton Close { public get; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B1766C5AF8C7E249 | Property | `static System.Windows.Forms.TaskDialogButton Continue { public get; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3895E5EE93599D11 | Property | `System.Boolean Enabled { public get; public set; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F45F853E51025DDC | Property | `static System.Windows.Forms.TaskDialogButton Help { public get; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-73AD6A99E17713F8 | Property | `static System.Windows.Forms.TaskDialogButton Ignore { public get; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-15BC53EFB700E632 | Property | `static System.Windows.Forms.TaskDialogButton No { public get; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C4382E1964D9BDF | Property | `static System.Windows.Forms.TaskDialogButton OK { public get; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-52F27ECE57EB87EF | Property | `static System.Windows.Forms.TaskDialogButton Retry { public get; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0369A303D579AE61 | Property | `System.Boolean ShowShieldIcon { public get; public set; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8A07935E18FA346C | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.TaskDialogControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CFE41F534E24E24D | Property | `System.String Text { public get; public set; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-48BD2024ADD7FA5B | Property | `static System.Windows.Forms.TaskDialogButton TryAgain { public get; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-355723BB266B466D | Property | `System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9CAB633B0EA98A83 | Property | `static System.Windows.Forms.TaskDialogButton Yes { public get; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
