# NativeWindow：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/NativeWindow.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.NativeWindow`；基类：`System.MarshalByRefObject`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **1**，公开事件 **0**，公开方法 **6**；受保护扩展成员 **6**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-B39ECABA85D47AD6 | Constructor | `public NativeWindow()` | `System.Windows.Forms.NativeWindow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FC53D1AC9AE83534 | Method | `public System.Void AssignHandle(System.IntPtr handle)` | `System.Windows.Forms.NativeWindow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6DD7B2D3B599A404 | Method | `public virtual System.Void CreateHandle(System.Windows.Forms.CreateParams cp)` | `System.Windows.Forms.NativeWindow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-63EDFE3A8E1B6354 | Method | `public System.Void DefWndProc(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.NativeWindow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DA459682C253F664 | Method | `public virtual System.Void DestroyHandle()` | `System.Windows.Forms.NativeWindow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E85B671699D40899 | Method | `public static System.Windows.Forms.NativeWindow FromHandle(System.IntPtr handle)` | `System.Windows.Forms.NativeWindow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DB07D43BD81D493F | Method | `public virtual System.Void ReleaseHandle()` | `System.Windows.Forms.NativeWindow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6FA1B066437899D9 | Property | `System.IntPtr Handle { public get; }` | `System.Windows.Forms.NativeWindow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-EF32B74CED13F031 | Method | `protected virtual System.Void Finalize()` | `System.Windows.Forms.NativeWindow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1014DCB74F818098 | Method | `protected virtual System.Void OnHandleChange()` | `System.Windows.Forms.NativeWindow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5C059B676BC17EFD | Method | `protected virtual System.Void OnThreadException(System.Exception e)` | `System.Windows.Forms.NativeWindow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-404D526694E7D245 | Method | `protected virtual System.Void WmDpiChangedAfterParent(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.NativeWindow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D22140491A96620B | Method | `protected virtual System.Void WmDpiChangedBeforeParent(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.NativeWindow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-558D98B6A19E9BDB | Method | `protected virtual System.Void WndProc(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.NativeWindow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
