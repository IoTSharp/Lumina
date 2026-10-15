# DragEventArgs：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/DragEventArgs.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.DragEventArgs`；基类：`System.EventArgs`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **9**，公开事件 **0**，公开方法 **0**；受保护扩展成员 **0**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-0AE7E96C447C3AEA | Constructor | `public DragEventArgs(System.Windows.Forms.IDataObject data, System.Int32 keyState, System.Int32 x, System.Int32 y, System.Windows.Forms.DragDropEffects allowedEffect, System.Windows.Forms.DragDropEffects effect)` | `System.Windows.Forms.DragEventArgs`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6FE6AA46D857F00C | Constructor | `public DragEventArgs(System.Windows.Forms.IDataObject data, System.Int32 keyState, System.Int32 x, System.Int32 y, System.Windows.Forms.DragDropEffects allowedEffect, System.Windows.Forms.DragDropEffects effect, System.Windows.Forms.DropImageType dropImageType, System.String message, System.String messageReplacementToken)` | `System.Windows.Forms.DragEventArgs`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8168E4A999852E2D | Field | `public static readonly System.EventArgs Empty` | `System.EventArgs`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3A05E452F3125672 | Property | `System.Windows.Forms.DragDropEffects AllowedEffect { public get; }` | `System.Windows.Forms.DragEventArgs`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B5EDFFF7A7BF09A9 | Property | `System.Windows.Forms.IDataObject Data { public get; }` | `System.Windows.Forms.DragEventArgs`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C981820A98F9DA4B | Property | `System.Windows.Forms.DropImageType DropImageType { public get; public set; }` | `System.Windows.Forms.DragEventArgs`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6867B8AE543DAC16 | Property | `System.Windows.Forms.DragDropEffects Effect { public get; public set; }` | `System.Windows.Forms.DragEventArgs`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D39D344D80AB9ABB | Property | `System.Int32 KeyState { public get; }` | `System.Windows.Forms.DragEventArgs`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-10007B26A549BF68 | Property | `System.String Message { public get; public set; }` | `System.Windows.Forms.DragEventArgs`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D8DC738CAD0C4D1 | Property | `System.String MessageReplacementToken { public get; public set; }` | `System.Windows.Forms.DragEventArgs`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9D11A7D941D248EE | Property | `System.Int32 X { public get; }` | `System.Windows.Forms.DragEventArgs`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2EC93061A5123A6B | Property | `System.Int32 Y { public get; }` | `System.Windows.Forms.DragEventArgs`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
