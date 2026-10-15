# WindowsFormsSynchronizationContext：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/WindowsFormsSynchronizationContext.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.WindowsFormsSynchronizationContext`；基类：`System.Threading.SynchronizationContext`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **2**，公开事件 **0**，公开方法 **10**；受保护扩展成员 **2**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-EEAB06CDECC55243 | Constructor | `public WindowsFormsSynchronizationContext()` | `System.Windows.Forms.WindowsFormsSynchronizationContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-40DA79566796D1FD | Method | `public virtual System.Threading.SynchronizationContext CreateCopy()` | `System.Windows.Forms.WindowsFormsSynchronizationContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3DE3A221F3656ED0 | Method | `public System.Void Dispose()` | `System.Windows.Forms.WindowsFormsSynchronizationContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1BDA1FA47C27217D | Method | `public System.Boolean IsWaitNotificationRequired()` | `System.Threading.SynchronizationContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7AD6590DCA0C0361 | Method | `public virtual System.Void OperationCompleted()` | `System.Threading.SynchronizationContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E896AB8E91B6D12 | Method | `public virtual System.Void OperationStarted()` | `System.Threading.SynchronizationContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7B9D9D34C8108F90 | Method | `public virtual System.Void Post(System.Threading.SendOrPostCallback d, System.Object state)` | `System.Windows.Forms.WindowsFormsSynchronizationContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D998A5344A739A91 | Method | `public virtual System.Void Send(System.Threading.SendOrPostCallback d, System.Object state)` | `System.Windows.Forms.WindowsFormsSynchronizationContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8523DCE107634146 | Method | `public static System.Void SetSynchronizationContext(System.Threading.SynchronizationContext syncContext)` | `System.Threading.SynchronizationContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EF04758184118792 | Method | `public static System.Void Uninstall()` | `System.Windows.Forms.WindowsFormsSynchronizationContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CCE4BAF761DC2A80 | Method | `public virtual System.Int32 Wait(System.IntPtr[] waitHandles, System.Boolean waitAll, System.Int32 millisecondsTimeout)` | `System.Threading.SynchronizationContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-70A578CDA56627A6 | Property | `static System.Boolean AutoInstall { public get; public set; }` | `System.Windows.Forms.WindowsFormsSynchronizationContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-26F5D9C9FDAFD08A | Property | `static System.Threading.SynchronizationContext Current { public get; }` | `System.Threading.SynchronizationContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-11693EE5129BDFB1 | Method | `protected System.Void SetWaitNotificationRequired()` | `System.Threading.SynchronizationContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C9F0110CD96E1D39 | Method | `protected static System.Int32 WaitHelper(System.IntPtr[] waitHandles, System.Boolean waitAll, System.Int32 millisecondsTimeout)` | `System.Threading.SynchronizationContext`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
