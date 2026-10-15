# ThreadExceptionDialog

⬜ **现状：**无标准异常对话框；Application.ThreadException 也未形成完整处理链。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/ThreadExceptionDialog.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：同名标准类型尚未实现，无可冒充为完整契约的源码。
- 排序：Form/MessageBox、Application 异常策略、文本/按钮与系统信息。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P1 / ThreadExceptionDialog-01 | 实现 ThreadExceptionDialog(Exception) : Form，继承属性/事件按固定基线；不要另造公开异常报告 API 或自动发送异常。 |
| ⬜ | P2 / ThreadExceptionDialog-02 | 对照普通异常/嵌套异常/不同消息的按钮、DialogResult、详细信息展开/收起、堆栈/程序集文本；按钮条件和默认动作按真实端捕获，不猜。 |
| ⬜ | P2 / ThreadExceptionDialog-03 | 接入 Application.ThreadException/SetUnhandledExceptionMode；用户关闭、继续/退出、对话框自身异常路径不能递归无限弹窗。 |
| ⬜ | P2 / ThreadExceptionDialog-04 | ShowDialog owner 模态/焦点、Enter/Esc/Tab、复制文本、本地化长消息、高 DPI 和可访问性按真实端比较。 |
| ⬜ | P2 / ThreadExceptionDialog-05 | AOT 堆栈可用性/裁剪的程序集信息差异清楚；只展示实际可取得数据，不把 JIT 特有细节伪造成相同。Dispose 与重复显示释放全部控件资源。 |
| ⬜ | P2 / ThreadExceptionDialog-06 | Windows 用标准 Form HWND 与原生 Button/TextBox/Label 等组合；系统 error icon/资源静态读取，保留 Designer 风格布局。 |
| ⬜ | P2 / ThreadExceptionDialog-07 | Linux 用 GtkWindow/GtkAlertDialog + native text/button widgets，继续/退出结果路由标准 Application，不调用另一个进程弹窗代替。 |
| ⬜ | P3 / ThreadExceptionDialog-08 | macOS 用 NSAlert 或 NSPanel+原生控件组合，详细文本/结果/主线程行为一致，系统按钮顺序差异明确。 |
| ⬜ | 验收 / ThreadExceptionDialog-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| 原生 Form/标准子控件组合，不另设错误处理框架。 | GTK 原生窗口与文本按钮，异常策略共用标准 Application。 | NSAlert/NSPanel 原生组合，Native AOT 堆栈实际结果作为证据。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.threadexceptiondialog?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/class.AlertDialog.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nsalert)
