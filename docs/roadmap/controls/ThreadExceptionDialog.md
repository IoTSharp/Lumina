# ThreadExceptionDialog

🟡 **现状：**已补 Windows/Lumina.Forms 的标准 Form 继承异常对话框确定范围并接入 Application.ThreadException；完整按钮、系统资源、本地化、平台和逐成员验收仍开放。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/ThreadExceptionDialog.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：[ThreadExceptionDialog.cs](../../../Lumina.Forms/ThreadExceptionDialog.cs)、[Application.cs](../../../Lumina.Forms/Application.cs)。
- 2026-11-11 代码证据：`Lumina.Forms/ThreadExceptionDialog.cs`、`Application.cs`；无窗口回归验证构造、Continue/Cancel、详情文本和 Warning 分支。
- 排序：Form/MessageBox、Application 异常策略、文本/按钮与系统信息。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| 🟡 | P1 / ThreadExceptionDialog-01 | 已实现 `ThreadExceptionDialog(Exception) : Form`，保持可派生公开类型和普通 Lumina 控件组合；继承成员逐项基线仍待。 |
| 🟡 | P2 / ThreadExceptionDialog-02 | 已实现摘要、详情展开/收起、异常/程序集文本、Continue/条件 Quit；本地化、图标和完整按钮差分仍待。 |
| 🟡 | P2 / ThreadExceptionDialog-03 | 已接 Application.ThreadException/默认错误路径、重入保护和 Quit→Application.Exit；完整 SetUnhandledExceptionMode/UI/异常分支仍待。 |
| ⬜ | P2 / ThreadExceptionDialog-04 | ShowDialog owner 模态/焦点、Enter/Esc/Tab、复制文本、本地化长消息、高 DPI 和可访问性按真实端比较。 |
| 🟡 | P2 / ThreadExceptionDialog-05 | 已限制程序集摘要遍历并保持 AOT 可用的普通文本路径；完整裁剪、堆栈、Dispose 压力和重复显示仍待。 |
| 🟡 | P2 / ThreadExceptionDialog-06 | 已使用标准 Form/Windows 原生 Button/TextBox/Label 组合；系统 error icon/Designer 静态资源仍待。 |
| ⬜ | P2 / ThreadExceptionDialog-07 | Linux 用 GtkWindow/GtkAlertDialog + native text/button widgets，继续/退出结果路由标准 Application，不调用另一个进程弹窗代替。 |
| ⬜ | P3 / ThreadExceptionDialog-08 | macOS 用 NSAlert 或 NSPanel+原生控件组合，详细文本/结果/主线程行为一致，系统按钮顺序差异明确。 |
| ⬜ | 验收 / ThreadExceptionDialog-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| 原生 Form/标准子控件组合，不另设错误处理框架。 | GTK 原生窗口与文本按钮，异常策略共用标准 Application。 | NSAlert/NSPanel 原生组合，Native AOT 堆栈实际结果作为证据。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 已完成任务

当前尚无已关闭的代码范围；变更历史统一见根目录 [CHANGELOG](../../../CHANGELOG.md)。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.threadexceptiondialog?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/class.AlertDialog.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nsalert)
