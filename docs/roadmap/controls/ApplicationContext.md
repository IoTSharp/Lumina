# ApplicationContext

⬜ **现状：**无同名实现。现有 Application.Run(Form) 不能表达多窗体、无主窗托盘和自定义退出策略。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/ApplicationContext.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：同名标准类型尚未实现，无可冒充为完整契约的源码。
- 排序：Application/UI 线程、Form 生命周期；P0 启动契约。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P0 / ApplicationContext-01 | 实现标准构造 ApplicationContext()/ApplicationContext(Form)、MainForm、Tag、ThreadExit 与 IDisposable；公开/受保护签名见完整清单。 |
| ⬜ | P0 / ApplicationContext-02 | MainForm 设值时解除旧窗体事件、订阅新窗体；主窗关闭通过 OnMainFormClosed/ExitThreadCore 结束所属 UI 线程，隐藏窗口与 Dispose 区分。 |
| ⬜ | P1 / ApplicationContext-03 | ExitThread/ExitThreadCore/ThreadExit 的一次性触发、派生 override 顺序、重复退出与异常对照，不把结束进程写进 Context。 |
| ⬜ | P1 / ApplicationContext-04 | 验证无 MainForm 的 Run(Context)、托盘退出、多窗口先后关闭、更换 MainForm、Close 被取消和独立多 UI 线程。 |
| ⬜ | P1 / ApplicationContext-05 | Dispose(bool) 的 MainForm 资源归属、事件解绑、待投递操作和二次 Dispose 与官方基线一致，不释放其他 Context 的窗口。 |
| ⬜ | P1 / ApplicationContext-06 | Windows 接入共享线程消息泵/WM_QUIT，并保持所有公开 Run 重载 void；不用强制隐藏主窗模拟无窗应用。 |
| ⬜ | P2 / ApplicationContext-07 | Linux 映射 GtkApplication 的 hold/release 和窗口登记，允许无窗口 context 继续运行；窗口关闭不能无条件退出整个 GTK application。 |
| ⬜ | P3 / ApplicationContext-08 | macOS NSApplication delegate/termination 由当前 context 策略驱动；窗口关闭与应用退出分离，仍支持托盘 NSStatusItem 无窗口场景。 |
| ⬜ | 验收 / ApplicationContext-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| 纯托管 context 驱动现有原生 UI 线程循环，不需要独立 native 控件。 | GtkApplication 生命周期 hold/release；Context 本身不是 GTK 窗口。 | NSApplication 生命周期与 NSWindow delegate；Context 不是隐藏窗口替代品。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.applicationcontext?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gio/method.Application.hold.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nsapplication)
