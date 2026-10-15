# TaskDialogPage：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/TaskDialogPage.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.TaskDialogPage`；基类：`System.Object`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **17**，公开事件 **4**，公开方法 **1**；受保护扩展成员 **4**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-0D335096B6BCA4DB | Constructor | `public TaskDialogPage()` | `System.Windows.Forms.TaskDialogPage`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E2500A7266D79526 | Event | `public event System.EventHandler Created` | `System.Windows.Forms.TaskDialogPage`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-83D4B34C47D3ADEB | Event | `public event System.EventHandler Destroyed` | `System.Windows.Forms.TaskDialogPage`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0259DABDDD23F26 | Event | `public event System.EventHandler HelpRequest` | `System.Windows.Forms.TaskDialogPage`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-40640074EA4A39AC | Event | `public event System.EventHandler&#96;1<System.Windows.Forms.TaskDialogLinkClickedEventArgs> LinkClicked` | `System.Windows.Forms.TaskDialogPage`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2622CF56049BCFB0 | Method | `public System.Void Navigate(System.Windows.Forms.TaskDialogPage page)` | `System.Windows.Forms.TaskDialogPage`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4568EB87225D8016 | Property | `System.Boolean AllowCancel { public get; public set; }` | `System.Windows.Forms.TaskDialogPage`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7BD11DBA765A2C42 | Property | `System.Boolean AllowMinimize { public get; public set; }` | `System.Windows.Forms.TaskDialogPage`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-66664F4101F6B3FF | Property | `System.Windows.Forms.TaskDialog BoundDialog { public get; }` | `System.Windows.Forms.TaskDialogPage`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AFD023F7D5A5C723 | Property | `System.Windows.Forms.TaskDialogButtonCollection Buttons { public get; public set; }` | `System.Windows.Forms.TaskDialogPage`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-69FFEFF13D399911 | Property | `System.String Caption { public get; public set; }` | `System.Windows.Forms.TaskDialogPage`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B45B75D7C3BBFCD | Property | `System.Windows.Forms.TaskDialogButton DefaultButton { public get; public set; }` | `System.Windows.Forms.TaskDialogPage`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-10CEB7DA5E365BB0 | Property | `System.Boolean EnableLinks { public get; public set; }` | `System.Windows.Forms.TaskDialogPage`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B6927D4E576B0D38 | Property | `System.Windows.Forms.TaskDialogExpander Expander { public get; public set; }` | `System.Windows.Forms.TaskDialogPage`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E1C5D7213874B01E | Property | `System.Windows.Forms.TaskDialogFootnote Footnote { public get; public set; }` | `System.Windows.Forms.TaskDialogPage`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EDDCA3E6EB6A716C | Property | `System.String Heading { public get; public set; }` | `System.Windows.Forms.TaskDialogPage`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-084B1DE19751B370 | Property | `System.Windows.Forms.TaskDialogIcon Icon { public get; public set; }` | `System.Windows.Forms.TaskDialogPage`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2530473BF3984E7D | Property | `System.Windows.Forms.TaskDialogProgressBar ProgressBar { public get; public set; }` | `System.Windows.Forms.TaskDialogPage`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C3ACFDF06CCDCC84 | Property | `System.Windows.Forms.TaskDialogRadioButtonCollection RadioButtons { public get; public set; }` | `System.Windows.Forms.TaskDialogPage`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0BDA98EED6392D40 | Property | `System.Boolean RightToLeftLayout { public get; public set; }` | `System.Windows.Forms.TaskDialogPage`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-55C542D1EB56E30B | Property | `System.Boolean SizeToContent { public get; public set; }` | `System.Windows.Forms.TaskDialogPage`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2BCD845896669594 | Property | `System.String Text { public get; public set; }` | `System.Windows.Forms.TaskDialogPage`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2A0ABD9528269BC0 | Property | `System.Windows.Forms.TaskDialogVerificationCheckBox Verification { public get; public set; }` | `System.Windows.Forms.TaskDialogPage`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-613E8C89E8A4B526 | Method | `protected internal System.Void OnCreated(System.EventArgs e)` | `System.Windows.Forms.TaskDialogPage`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-23A4ABA3BF3F9797 | Method | `protected internal System.Void OnDestroyed(System.EventArgs e)` | `System.Windows.Forms.TaskDialogPage`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DFBE3099321A4582 | Method | `protected internal System.Void OnHelpRequest(System.EventArgs e)` | `System.Windows.Forms.TaskDialogPage`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C230CC3BAD8E02A2 | Method | `protected internal System.Void OnLinkClicked(System.Windows.Forms.TaskDialogLinkClickedEventArgs e)` | `System.Windows.Forms.TaskDialogPage`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
