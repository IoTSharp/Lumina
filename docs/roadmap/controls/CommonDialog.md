# CommonDialog

⬜ **现状：**无标准 CommonDialog 基类；现有文件/颜色/字体对话框各自 sealed IDisposable，继承、IWin32Window、帮助与派生扩展不兼容。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/CommonDialog.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：同名标准类型尚未实现，无可冒充为完整契约的源码。
- 排序：Component/IWin32Window、共享 owner/模态/消息基础；所有公共对话框前置。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P0 / CommonDialog-01 | 实现标准 abstract CommonDialog : Component、Reset、ShowDialog() / ShowDialog(IWin32Window)、Tag、HelpRequest，保持所有派生标准关系。 |
| ⬜ | P0 / CommonDialog-02 | 实现 protected RunDialog(IntPtr)、HookProc、OwnerWndProc、OnHelpRequest 的可派生契约，派生用户对话框能直接编译运行。 |
| ⬜ | P1 / CommonDialog-03 | 共享 owner 推断、有效性检查、UI 线程/OLE 初始化、禁用恢复、嵌套 modal 与焦点还原，避免五种对话框各复制一套。 |
| ⬜ | P1 / CommonDialog-04 | 校准 DialogResult.OK/Cancel 与 native error 的区别、异常释放、重复 ShowDialog/Reset、组件容器 Dispose 和 Tag 的序列化。 |
| ⬜ | P1 / CommonDialog-05 | HelpRequest/帮助钩子、Cancel、隐藏 owner 和多 UI 线程/无主窗场景对照；仅允许真正需要的回调在对应 native UI 线程执行。 |
| ⬜ | P1 / CommonDialog-06 | Windows 用系统对话框 API/COM 与共享 native hook 封装，AOT 稳定 ABI/内存/委托寿命；没有必要为 CommonDialog 创建独立视觉控件。 |
| ⬜ | P2 / CommonDialog-07 | Linux 用 GTK 原生 dialog/picker + GMainContext 的受控模态适配；RunDialog 的 HWND 参数和 HookProc Win32 消息按 Windows 专属逐项列明。 |
| ⬜ | P3 / CommonDialog-08 | macOS 用 NSOpenPanel/NSSavePanel/NSAlert/NSColorPanel/NSFontPanel 等原生 UI；公共结果/owner/释放可移植，Win32 hook 扩展不能伪造。 |
| ⬜ | 验收 / CommonDialog-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| 共享系统公共对话框生命周期，派生实现仍使用 native dialogs。 | 共享 GTK 主上下文模态结果适配，不新增 public dialog abstraction。 | 共享 AppKit native panel 会话/主线程管理，保留 Standard API，Win32 hook 明确例外。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.commondialog?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/class.Dialog.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nspanel)
