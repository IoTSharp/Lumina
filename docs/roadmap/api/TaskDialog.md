# TaskDialog：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/TaskDialog.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.TaskDialog`；基类：`System.Object`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **1**，公开事件 **0**，公开方法 **7**；受保护扩展成员 **0**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-DD1F8044B3AE8A7D | Method | `public System.Void Close()` | `System.Windows.Forms.TaskDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C10F7FD2E0B17BE9 | Method | `public static System.Windows.Forms.TaskDialogButton ShowDialog(System.IntPtr hwndOwner, System.Windows.Forms.TaskDialogPage page, System.Windows.Forms.TaskDialogStartupLocation startupLocation = 1)` | `System.Windows.Forms.TaskDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C4A1520BA00879E | Method | `public static System.Windows.Forms.TaskDialogButton ShowDialog(System.Windows.Forms.IWin32Window owner, System.Windows.Forms.TaskDialogPage page, System.Windows.Forms.TaskDialogStartupLocation startupLocation = 1)` | `System.Windows.Forms.TaskDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F1DF1166BC3DEC56 | Method | `public static System.Windows.Forms.TaskDialogButton ShowDialog(System.Windows.Forms.TaskDialogPage page, System.Windows.Forms.TaskDialogStartupLocation startupLocation = 1)` | `System.Windows.Forms.TaskDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A2E3DCA1665F66F7 | Method | `public static System.Threading.Tasks.Task&#96;1<System.Windows.Forms.TaskDialogButton> ShowDialogAsync(System.IntPtr hwndOwner, System.Windows.Forms.TaskDialogPage page, System.Windows.Forms.TaskDialogStartupLocation startupLocation = 1)` | `System.Windows.Forms.TaskDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FEB13346DC9AAE29 | Method | `public static System.Threading.Tasks.Task&#96;1<System.Windows.Forms.TaskDialogButton> ShowDialogAsync(System.Windows.Forms.IWin32Window owner, System.Windows.Forms.TaskDialogPage page, System.Windows.Forms.TaskDialogStartupLocation startupLocation = 1)` | `System.Windows.Forms.TaskDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D4EB6C1FEBD88E98 | Method | `public static System.Threading.Tasks.Task&#96;1<System.Windows.Forms.TaskDialogButton> ShowDialogAsync(System.Windows.Forms.TaskDialogPage page, System.Windows.Forms.TaskDialogStartupLocation startupLocation = 0)` | `System.Windows.Forms.TaskDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-594552BB735AFB0F | Property | `System.IntPtr Handle { public get; }` | `System.Windows.Forms.TaskDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
