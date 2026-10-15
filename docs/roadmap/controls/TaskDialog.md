# TaskDialog

⬜ **现状：**标准 TaskDialog 类型尚未实现。MessageBox 内部只调用简化 Win32 TaskDialog；它不能抵扣 TaskDialogIndirect 页模型、导航、子控件、异步重载与返回按钮对象。TaskDialog 实现 IWin32Window，属于根命名空间，既不是 Control 也不是 Form。

- 基线：固定 .NET10 WinForms 10.0.11 / `b83f534`；评估日期 2026-10-15。
- 类型：`System.Windows.Forms.TaskDialog`。 [全部成员、继承及接口任务](../api/TaskDialog.md) · [98%口径](../coverage.md) · [公共前置](../foundations.md) · [平台条件](../platforms.md)。
- 排序：P1：基础模态/同步上下文稳定后，先 TaskDialogControl/Page/按钮，再单页窗口；其余子元素和导航 P2。
- 现有相关实现：[MessageBox 内部简化调用](../../../Lumina.Forms/MessageBox.cs)、[Win32 声明](../../../Lumina.Forms/Win32.cs)。两者均不是本类型的标准公开实现。

## 顺序任务

| 状态 | 优先级 / ID | 细节与验收 |
| --- | --- | --- |
| ⬜ | P1 / TaskDialog-01 | 实现全部 ShowDialog/ShowDialogAsync 的无 owner、IWin32Window、IntPtr 重载、TaskDialogStartupLocation、Handle、Close；返回 TaskDialogButton/Task<TaskDialogButton>，不是 DialogResult。固定基线没有 public/protected 构造，不为补类型新增构造入口。 |
| ⬜ | P1 / TaskDialog-02 | 准备 Common-Controls v6 activation context 与 STA；无 UI context、owner 失效、不同 UI 线程、同时重复显示同一页与非法 startupLocation 的异常依据固定10.0.11验证。 |
| ⬜ | P1 / TaskDialog-03 | 采用 TaskDialogIndirect/TASKDIALOGCONFIG + 统一 callback，创建后 Handle/BoundDialog 可用，销毁后归零/解绑；按钮 native ID 与托管对象一一对应，不能返回另建同文案按钮。 |
| ⬜ | P1 / TaskDialog-04 | Close、Esc、Alt+F4、Cancel 按钮、AllowCancel=false、按钮 AllowCloseDialog=false、点击处理器内 Navigate/Close 的结果与事件顺序分别对照。 |
| ⬜ | P2 / TaskDialog-05 | ShowDialogAsync 保持 UI 响应、owner 模态与异步异常传播，最终回调/Task 完成顺序用官方端轨迹；不能用任意 Task.Run 调 native dialog 导致 STA/owner/线程错乱。 |
| ⬜ | P2 / TaskDialog-06 | 完成 page.Navigate 与 Created/Destroyed/HelpRequest/LinkClicked 生命周期，旧页释放 native 关联、新页接管焦点与默认按钮，导航返回哪个按钮对象有证据。 |
| ⬜ | P1 / TaskDialog-07 | Windows 优先系统 TaskDialogIndirect，AOT callback 静态可达/异常不跨 ABI，字符串、按钮数组、GCHandle、HICON 的寿命覆盖显示与导航；无需重造 Windows 任务窗口。 |
| ⬜ | P2 / TaskDialog-08 | Linux 用 GtkWindow 的 modal/transient-for + GtkBox/GtkLabel/GtkButton/GtkCheckButton/GtkProgressBar/GtkExpander 原生组合；GtkAlertDialog 只覆盖简单子集，不能承载完整任务页。 |
| ⬜ | P3 / TaskDialog-09 | macOS 简单页可用 NSAlert/accessoryView，完整页用 NSPanel + NSStackView/NSTextField/NSButton/NSProgressIndicator 原生组合；共用 page 状态/结果/取消链，不自绘整页。 |
| ⬜ | 验收 / TaskDialog-V | 对完整 API 表逐成员核对签名、默认、修改限制、异常、状态和事件轨迹；真实 WinForms 与 Lumina 使用同场景。AOT发布后实际运行、资源/回调释放、键盘/可访问性分别留证，Linux/macOS不借Windows结果抵扣。NativeFormsDemo继续双目标；同源静态构造与.resx资源可消费，不将TaskDialog逻辑元素冒称可拖拽的Designer Control。 |

## 原生方案与边界

| 平台 | 实现 |
| --- | --- |
| Windows | TaskDialogIndirect/TASKDIALOGCONFIG/TDN_* 与 TDM_*；复用原生键盘、窗口、控件和可访问性。 |
| Linux | GtkWindow 和原子原生控件组合；不存在完整一对一 TaskDialog widget。 |
| macOS | NSPanel/NSAlert 与原子 AppKit 控件组合；Handle 的 HWND 契约 Windows 专属，普通结果/状态/事件仍要求对齐。 |

普通属性/事件/行为按平台各自验收；Windows消息/句柄等真实专属契约单列🚫，但不抵扣Windows未实现项。Font/Image/Icon/Bitmap/Graphics公共类型的跨平台身份按统一平台计划先解决，不靠排除分母降低目标。

## 修改历史

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

## 原始资料

- [微软固定API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.taskdialog?view=windowsdesktop-10.0)
- [Win32 TaskDialogIndirect](https://learn.microsoft.com/en-us/windows/win32/api/commctrl/nf-commctrl-taskdialogindirect)
- [GTK/GIO原生依据](https://docs.gtk.org/gtk4/class.Window.html)
- [AppKit原生依据](https://developer.apple.com/documentation/appkit/nspanel)
