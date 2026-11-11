# ApplicationContext

🟡 **现状：**已实现标准 ApplicationContext 与 Run(Context) 接线；MainForm/Tag、退出策略与资源归属的代码已完成，真实 Windows 多窗体/无窗托盘、模态与并发运行验收后置。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/ApplicationContext.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：[ApplicationContext.cs](../../../Lumina.Forms/ApplicationContext.cs)。
- 排序：Application/UI 线程、Form 生命周期；P0 启动契约。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ✅ | P0 / ApplicationContext-01 | 标准两种构造、MainForm、Tag、ThreadExit、IDisposable、finalizer/受保护扩展钩子已实现；保持固定 WinForms 名称及签名。编译与必要托管回归见[验证](../validation.md)，运行验收后置。 |
| ✅ | P0 / ApplicationContext-02 | MainForm 替换时解除旧 HandleDestroyed 订阅并连接新主窗；重建句柄不退出，真正销毁时先解绑再调用 OnMainFormClosed/ExitThreadCore。隐藏与关闭分别处理，退出请求固定发往关联循环线程。 |
| ✅ | P1 / ApplicationContext-03 | ExitThread 调用虚 ExitThreadCore 并以 Context 为 sender 发 ThreadExit；显式重复请求按标准继续发事件，主窗销毁先解绑而不重复退出。派生 override 顺序保留，不把结束进程写入 Context。 |
| ⬜ | P1 / ApplicationContext-04 | 验证无 MainForm 的 Run(Context)、托盘退出、多窗口先后关闭、更换 MainForm、Close 被取消和独立多 UI 线程。 |
| ✅ | P1 / ApplicationContext-05 | Dispose(bool) 只释放当前 MainForm、解除寿命订阅、清空引用；二次 Dispose 不再释放，finalizer 不释放托管主窗。Form 的投递队列随其既有 Dispose 路径关闭，其他 Context 的窗口由各自循环管理。 |
| ✅ | P1 / ApplicationContext-06 | Windows 接入同线程主消息泵及 WM_QUIT；Run()/Run(Form)/Run(Context) 都返回 void，无主窗 Context 持续运行至其退出请求，主循环拒绝非法嵌套。完整平台运行验证继续归04/V。 |
| ⬜ | P2 / ApplicationContext-07 | Linux 映射 GtkApplication 的 hold/release 和窗口登记，允许无窗口 context 继续运行；窗口关闭不能无条件退出整个 GTK application。 |
| ⬜ | P3 / ApplicationContext-08 | macOS NSApplication delegate/termination 由当前 context 策略驱动；窗口关闭与应用退出分离，仍支持托盘 NSStatusItem 无窗口场景。 |
| ⬜ | 验收 / ApplicationContext-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| 纯托管 context 驱动现有原生 UI 线程循环，不需要独立 native 控件。 | GtkApplication 生命周期 hold/release；Context 本身不是 GTK 窗口。 | NSApplication 生命周期与 NSWindow delegate；Context 不是隐藏窗口替代品。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 已完成任务

代码完成范围已在上表标记；变更历史统一见根目录 [CHANGELOG](../../../CHANGELOG.md)。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.applicationcontext?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gio/method.Application.hold.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nsapplication)
