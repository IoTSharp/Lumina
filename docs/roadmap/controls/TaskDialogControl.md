# TaskDialogControl

⬜ **现状：**未实现。它是 TaskDialog 子元素的抽象逻辑基类，仅有 BoundPage/Tag，不是标准 Control，也没有每个元素独立 HWND 的公共承诺。

- 基线：固定 .NET10 WinForms 10.0.11 / `b83f534`；评估日期 2026-10-15。
- 类型：`System.Windows.Forms.TaskDialogControl`。 [全部成员、继承及接口任务](../api/TaskDialogControl.md) · [98%口径](../coverage.md) · [公共前置](../foundations.md) · [平台条件](../platforms.md)。
- 排序：P1：先于 TaskDialogButton/Radio/Verification/Progress/Expander/Footnote。
- 现有相关实现：[MessageBox 内部简化调用](../../../Lumina.Forms/MessageBox.cs)、[Win32 声明](../../../Lumina.Forms/Win32.cs)。两者均不是本类型的标准公开实现。

## 顺序任务

| 状态 | 优先级 / ID | 细节与验收 |
| --- | --- | --- |
| ⬜ | P1 / TaskDialogControl-01 | 实现准确抽象性、BoundPage 只读与 Tag 可读写；固定基线构造为 private protected，调用方不能在库外派生，不新增 public/protected 构造。它不派生 Control/Component，不新增 Handle/Controls/Dispose 的仓库替代协议。 |
| ⬜ | P1 / TaskDialogControl-02 | 定义进入/离开 TaskDialogPage 的绑定状态与标准异常；一个元素跨页共享、重复加入、显示期间转移以10.0.11探针为准。 |
| ⬜ | P1 / TaskDialogControl-03 | BoundPage 与 page.BoundDialog 在 Created/Destroyed/Navigate/失败退出时一致；仅改变 Tag 不触发无关 native 重建或事件。 |
| ⬜ | P1 / TaskDialogControl-04 | 共享 native ID/逻辑对象映射与状态传播，子类只实现自己的契约；不为每个元素另建线程/窗口/资源管理器。 |
| ⬜ | P2 / TaskDialogControl-05 | 构造、派生、Tag 任意对象与资源裁剪、绑定失败回滚有实际 AOT 测试；逻辑元素生命周期不引入新公开释放模型。 |
| ⬜ | P1 / TaskDialogControl-06 | Windows 由宿主 TASKDIALOGCONFIG/TDN callback 操作对应原生元素；基类自身不创建新系统控件。 |
| ⬜ | P2 / TaskDialogControl-07 | Linux 基类是共享托管对象，其子类分别绑定 GtkButton/GtkCheckButton 等真实 widgets，GTK wrapper 不泄漏进 public API。 |
| ⬜ | P3 / TaskDialogControl-08 | macOS 同样只保存逻辑绑定，子类绑定 NSButton/文本/进度等 AppKit 原生对象，不伪装 HWND。 |
| ⬜ | 验收 / TaskDialogControl-V | 对完整 API 表逐成员核对签名、默认、修改限制、异常、状态和事件轨迹；真实 WinForms 与 Lumina 使用同场景。AOT发布后实际运行、资源/回调释放、键盘/可访问性分别留证，Linux/macOS不借Windows结果抵扣。NativeFormsDemo继续双目标；同源静态构造与.resx资源可消费，不将TaskDialog逻辑元素冒称可拖拽的Designer Control。 |

## 原生方案与边界

| 平台 | 实现 |
| --- | --- |
| Windows | TaskDialog 原生元素的逻辑归属基类，不是窗口。 |
| Linux | 无对应 widget 是正常的逻辑类型；子元素使用原生 widgets。 |
| macOS | 无对应 view 是正常的逻辑类型；子元素使用原生 AppKit views。 |

普通属性/事件/行为按平台各自验收；Windows消息/句柄等真实专属契约单列🚫，但不抵扣Windows未实现项。Font/Image/Icon/Bitmap/Graphics公共类型的跨平台身份按统一平台计划先解决，不靠排除分母降低目标。

## 修改历史

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

## 原始资料

- [微软固定API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.taskdialogcontrol?view=windowsdesktop-10.0)
- [Win32 TaskDialogIndirect](https://learn.microsoft.com/en-us/windows/win32/api/commctrl/nf-commctrl-taskdialogindirect)
- [GTK/GIO原生依据](https://docs.gtk.org/gtk4/class.Widget.html)
- [AppKit原生依据](https://developer.apple.com/documentation/appkit/nsview)
