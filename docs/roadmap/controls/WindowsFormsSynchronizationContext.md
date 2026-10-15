# WindowsFormsSynchronizationContext

⬜ **现状：**无标准同步上下文；批次 003 已修正 Control/Form 的首个真实 HWND 投递范围，但尚未安装标准 context，await 恢复到正确 UI 线程仍待实现。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/WindowsFormsSynchronizationContext.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：同名标准类型尚未实现，无可冒充为完整契约的源码。
- 排序：Application 消息泵、Control/UI 投递窗口或原生主上下文；P0。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P0 / WindowsFormsSynchronizationContext-01 | 实现标准 SynchronizationContext 派生、Post、Send、CreateCopy、AutoInstall、Uninstall、Dispose；接口签名/异常路径以固定基线为准。 |
| ⬜ | P0 / WindowsFormsSynchronizationContext-01a | 批次 006 首范围：标准类型、无参构造及显式 Post/Send/CreateCopy/Dispose，在创建线程保有真实投递目的地，复用库内 dispatcher；先固定参考端检查无现有 Form、状态/异常、复制与释放。AutoInstall/Uninstall、默认安装及线程退出/模态广泛竞态另列，不补空成员。 |
| ⬜ | P0 / WindowsFormsSynchronizationContext-02 | Post 总是异步入正确 UI 队列；Send 同 UI 线程可直接执行，跨线程同步等待并转发异常，不使用 Task.Run。 |
| ⬜ | P0 / WindowsFormsSynchronizationContext-03 | AutoInstall 随首次创建控件/开始消息循环安装，保留已有 context 的标准策略；Uninstall/线程退出还原先前 context。 |
| ⬜ | P1 / WindowsFormsSynchronizationContext-04 | CreateCopy 指向相同 UI 调度目的地而非复制新 UI 线程；Dispose 后 Post/Send、UI 线程已退出和无句柄时行为逐项对照。 |
| ⬜ | P1 / WindowsFormsSynchronizationContext-05 | 验证 async void 事件、await Task、InvokeAsync、多 UI 线程、模态嵌套、取消/异常和关闭竞态；测线程 ID 与队列顺序而非只测最终文本。 |
| ⬜ | P1 / WindowsFormsSynchronizationContext-06 | Windows 使用消息投递 HWND/注册消息与共享 UI 线程队列，native thunk 在 AOT 后仍有 root，托管异常不跨 ABI。 |
| ⬜ | P2 / WindowsFormsSynchronizationContext-07 | Linux 通过目标 GMainContext 的 invoke/source 投递并唤醒；同步 Send 不在 UI 线程阻塞自己，多个 thread-default context 的归属固定。 |
| ⬜ | P3 / WindowsFormsSynchronizationContext-08 | macOS 通过 main run loop/dispatch 到 AppKit 主线程；模态 run-loop mode 下也必须能收到任务，不能只在默认 mode 工作。 |
| ⬜ | 验收 / WindowsFormsSynchronizationContext-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| 消息 HWND/队列复用 Control.Invoke 基础，不另建任务调度框架。 | GLib GMainContext 原生投递；不能假定任意工作线程都能执行 GTK。 | AppKit 主 run loop/主队列；具体投递方式用模态测试决定。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.windowsformssynchronizationcontext?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/glib/method.MainContext.invoke.html)
- [Apple 原生依据](https://developer.apple.com/documentation/foundation/runloop)
