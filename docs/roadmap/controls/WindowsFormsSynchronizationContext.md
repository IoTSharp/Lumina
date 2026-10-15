# WindowsFormsSynchronizationContext

🟡 **现状：**批次 006 已实现标准类型、无参构造与显式 Post/Send/CreateCopy/Dispose，复用既有 dispatcher 并持有独立于应用窗体的真实消息 HWND。57 条固定 WinForms 10.0.11、Lumina JIT 与实际 Native AOT 观测相同，显式安装后的 await 恢复已检；AutoInstall/Uninstall 与默认安装/还原仍未实现。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/WindowsFormsSynchronizationContext.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：[WindowsFormsSynchronizationContext.cs](../../../Lumina.Forms/WindowsFormsSynchronizationContext.cs)。
- 排序：Application 消息泵、Control/UI 投递窗口或原生主上下文；P0。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| 🟡 | P0 / WindowsFormsSynchronizationContext-01 | 批次 006 完成标准派生、IDisposable、无参构造及显式 Post/Send/CreateCopy/Dispose；AutoInstall/Uninstall 与默认安装/还原仍缺，完整异常/退出范围未关闭。 |
| ✅ | P0 / WindowsFormsSynchronizationContext-01a | 批次 006：真实 message-only HWND 复用 dispatcher，57 条参考/JIT/实际 AOT 观测相同，7 项独立 STA 回归通过；无 Form 投递、state/ExecutionContext/原异常、复制目的地、共享释放与显式 await 恢复已检。Send(null) 防挂差异见下文；默认安装及完整退出/模态竞态另列。 |
| ⬜ | P0 / WindowsFormsSynchronizationContext-01b | 批次 007 首范围：标准 AutoInstall/Uninstall 与控件/窗体构造及消息循环的默认安装/还原，固定参考端比较线程局部开关、保留自定义 Current、base context 替换、重复调用与默认 callback await 恢复。不能只增加属性存值或空 Uninstall；完整线程退出/关闭/多 UI/模态竞态继续保留。 |
| 🟡 | P0 / WindowsFormsSynchronizationContext-02 | 批次 006 完成显式 Post 异步队列、同线程 Send 排空较早 Post 后同步执行、跨线程 Send 等待并保留异常。默认安装与线程退出等未覆盖路径仍待验。 |
| ⬜ | P0 / WindowsFormsSynchronizationContext-03 | AutoInstall 随首次创建控件/开始消息循环安装，保留已有 context 的标准策略；Uninstall/线程退出还原先前 context。 |
| 🟡 | P1 / WindowsFormsSynchronizationContext-04 | 批次 006 完成相同目的地的 CreateCopy（含工作线程调用）、本实例 Dispose 后投递无动作、共享实例失效及排队丢弃对照。UI 线程已退出、跨线程释放和全部无句柄/关闭竞态仍待验。 |
| 🟡 | P1 / WindowsFormsSynchronizationContext-05 | 批次 006 仅验证显式 SetSynchronizationContext 后 Task await 与 Post async 回调恢复到创建线程；默认安装下 async void/InvokeAsync、多 UI 线程、模态、取消/关闭组合仍待验。 |
| 🟡 | P1 / WindowsFormsSynchronizationContext-06 | 批次 006 显式类型复用既有注册消息/dispatcher，静态 unmanaged thunk 实际 AOT 运行通过，异步异常仍在库内转 ThreadException。完整线程退出资源与默认生命周期待后续。 |
| ⬜ | P2 / WindowsFormsSynchronizationContext-07 | Linux 通过目标 GMainContext 的 invoke/source 投递并唤醒；同步 Send 不在 UI 线程阻塞自己，多个 thread-default context 的归属固定。 |
| ⬜ | P3 / WindowsFormsSynchronizationContext-08 | macOS 通过 main run loop/dispatch 到 AppKit 主线程；模态 run-loop mode 下也必须能收到任务，不能只在默认 mode 工作。 |
| ⬜ | 验收 / WindowsFormsSynchronizationContext-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| 消息 HWND/队列复用 Control.Invoke 基础，不另建任务调度框架。 | GLib GMainContext 原生投递；不能假定任意工作线程都能执行 GTK。 | AppKit 主 run loop/主队列；具体投递方式用模态测试决定。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- 2026-10-15：增加评估和待办；本轮未实现运行库功能。
- 2026-10-16 · 批次 006：先运行固定 WinForms 10.0.11，再实现显式标准类型；57 条参考/JIT/实际 Native AOT 观测一致，新增 7 项独立 STA 轻量回归。每线程共用 message-only HWND，通过现有 dispatcher 保留 FIFO、ExecutionContext、state、原异常与 ThreadException；成功建窗登记 DPI 门禁，WM_NCDESTROY 关闭队列并释放 GCHandle。普通 Form 关闭不销毁此目的地。按参考端保留共享释放：本实例 Dispose 后投递无动作，其他 copy/同线程构造实例仍持失效目的地并抛 InvalidOperationException；新构造不会修复失效 slot，已接受未执行回调丢弃。默认安装/还原不随显式安装检查关闭。

明确差异：参考端活跃 context 的 Send(null) 在独立进程 3 秒超时，本实现沿用防挂守卫抛 ArgumentNullException（参数 d）；Post(null) 两端均接受且不调用回调。此差异单列，不把挂起复制为支持；完整默认错误 UI、线程退出、跨线程释放、多 UI 线程和模态竞态仍待验。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.windowsformssynchronizationcontext?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/glib/method.MainContext.invoke.html)
- [Apple 原生依据](https://developer.apple.com/documentation/foundation/runloop)
