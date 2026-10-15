# TaskDialogCommandLinkButton：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/TaskDialogCommandLinkButton.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.TaskDialogCommandLinkButton`；基类：`System.Windows.Forms.TaskDialogButton`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **19**，公开事件 **1**，公开方法 **4**；受保护扩展成员 **0**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-6532E8DF0736712E | Constructor | `public TaskDialogCommandLinkButton()` | `System.Windows.Forms.TaskDialogCommandLinkButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-86F8EFEC553DECD2 | Constructor | `public TaskDialogCommandLinkButton(System.String text, System.String descriptionText = null, System.Boolean enabled = true, System.Boolean allowCloseDialog = true)` | `System.Windows.Forms.TaskDialogCommandLinkButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-05A99320992D9D99 | Event | `public event System.EventHandler Click` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AB91AAA4D955EAE7 | Method | `public virtual System.Boolean Equals(System.Object obj)` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D3E74D1F7C406F0C | Method | `public virtual System.Int32 GetHashCode()` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F6CFF044672644D8 | Method | `public System.Void PerformClick()` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E3CACC872C00F26E | Method | `public virtual System.String ToString()` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CB0154DA6BDCF425 | Property | `static System.Windows.Forms.TaskDialogButton Abort { public get; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B5FA77752BF00800 | Property | `System.Boolean AllowCloseDialog { public get; public set; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CDED72D3EFDEC07F | Property | `System.Windows.Forms.TaskDialogPage BoundPage { public get; }` | `System.Windows.Forms.TaskDialogControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-874E4C041D3EB95B | Property | `static System.Windows.Forms.TaskDialogButton Cancel { public get; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3161DA28938370FB | Property | `static System.Windows.Forms.TaskDialogButton Close { public get; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2419664044764202 | Property | `static System.Windows.Forms.TaskDialogButton Continue { public get; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-89A91D60EC2F18C5 | Property | `System.String DescriptionText { public get; public set; }` | `System.Windows.Forms.TaskDialogCommandLinkButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4358F73201DD7B7F | Property | `System.Boolean Enabled { public get; public set; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A982380A98B93D7D | Property | `static System.Windows.Forms.TaskDialogButton Help { public get; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6E54FC46E5831EEE | Property | `static System.Windows.Forms.TaskDialogButton Ignore { public get; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5B353BBBC691F252 | Property | `static System.Windows.Forms.TaskDialogButton No { public get; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-657AD437E188859A | Property | `static System.Windows.Forms.TaskDialogButton OK { public get; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D839FD8CE46366BC | Property | `static System.Windows.Forms.TaskDialogButton Retry { public get; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8D6AD0A0278F6556 | Property | `System.Boolean ShowShieldIcon { public get; public set; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B9797812B2311B13 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.TaskDialogControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DE8B7067F38F1811 | Property | `System.String Text { public get; public set; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C3FD8801986F4BEB | Property | `static System.Windows.Forms.TaskDialogButton TryAgain { public get; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-674D30C00A586099 | Property | `System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-719B46CEF68AF59E | Property | `static System.Windows.Forms.TaskDialogButton Yes { public get; }` | `System.Windows.Forms.TaskDialogButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
