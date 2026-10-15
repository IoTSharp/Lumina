# ApplicationContext：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/ApplicationContext.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.ApplicationContext`；基类：`System.Object`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **2**，公开事件 **1**，公开方法 **2**；受保护扩展成员 **4**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-FC64A907D7293ECA | Constructor | `public ApplicationContext()` | `System.Windows.Forms.ApplicationContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E7313B3A1605D002 | Constructor | `public ApplicationContext(System.Windows.Forms.Form mainForm)` | `System.Windows.Forms.ApplicationContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AA1ACB097F65233E | Event | `public event System.EventHandler ThreadExit` | `System.Windows.Forms.ApplicationContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E0C55D649DFE85D | Method | `public System.Void Dispose()` | `System.Windows.Forms.ApplicationContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-17E9F937FE4789C6 | Method | `public System.Void ExitThread()` | `System.Windows.Forms.ApplicationContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C9FD1619E99202A | Property | `System.Windows.Forms.Form MainForm { public get; public set; }` | `System.Windows.Forms.ApplicationContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CE55AEEACA24E695 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.ApplicationContext` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-89DD69DC963FDA60 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.ApplicationContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-326FBEEA9D9C4C8E | Method | `protected virtual System.Void ExitThreadCore()` | `System.Windows.Forms.ApplicationContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B6E157EB877D14E | Method | `protected virtual System.Void Finalize()` | `System.Windows.Forms.ApplicationContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-63F69C569299F3BA | Method | `protected virtual System.Void OnMainFormClosed(System.Object sender, System.EventArgs e)` | `System.Windows.Forms.ApplicationContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
