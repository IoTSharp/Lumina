# TaskDialogExpander

⬜ **现状：**未实现。标准折叠区有 Text、两种按钮文字、Expanded/Position 和 ExpandedChanged，属于任务页逻辑元素。

- 基线：固定 .NET10 WinForms 10.0.11 / `b83f534`；评估日期 2026-10-15。
- 类型：`System.Windows.Forms.TaskDialogExpander`。 [全部成员、继承及接口任务](../api/TaskDialogExpander.md) · [98%口径](../coverage.md) · [公共前置](../foundations.md) · [平台条件](../platforms.md)。
- 排序：P2：TaskDialogPage 布局/链接/事件模型后。
- 现有相关实现：[MessageBox 内部简化调用](../../../Lumina.Forms/MessageBox.cs)、[Win32 声明](../../../Lumina.Forms/Win32.cs)。两者均不是本类型的标准公开实现。

## 顺序任务

| 状态 | 优先级 / ID | 细节与验收 |
| --- | --- | --- |
| ⬜ | P2 / TaskDialogExpander-01 | 实现无参/Text 构造、Text/CollapsedButtonText/ExpandedButtonText/Expanded/Position/ExpandedChanged/ToString，默认与非法枚举精准。 |
| ⬜ | P2 / TaskDialogExpander-02 | 折叠/展开、程序重复设值与用户点击的事件顺序/次数、页面高度变化、SizeToContent 与最小化/恢复对照。 |
| ⬜ | P2 / TaskDialogExpander-03 | Position.AfterText/AfterFootnote 对原生布局的影响，换页、长文本/换行/链接/RTL/高 DPI 全覆盖；空内容的按钮可见行为按基线。 |
| ⬜ | P2 / TaskDialogExpander-04 | 绑定后允许变更属性的范围和 ExpandedChanged 内再次修改/导航行为明确，旧页 callback 不得误改新页。 |
| ⬜ | P2 / TaskDialogExpander-05 | Windows 使用 TASKDIALOGCONFIG 的 expanded text/control text/flags 与 TDN_EXPANDO_BUTTON_CLICKED，动态文本经对应 TDM element 更新。 |
| ⬜ | P2 / TaskDialogExpander-06 | Linux 用 GtkExpander + 原生 GtkLabel，expanded 通知回写；标签变化/详情位于 footer 后时调整原生布局顺序，不自绘折叠箭头。 |
| ⬜ | P3 / TaskDialogExpander-07 | macOS 用原生 NSButton disclosure 与 NSStackView/文本视图或 NSStackView visibility，事件/重新布局共享；NSAlert 只在 accessory 足以承载时采用。 |
| ⬜ | 验收 / TaskDialogExpander-V | 对完整 API 表逐成员核对签名、默认、修改限制、异常、状态和事件轨迹；真实 WinForms 与 Lumina 使用同场景。AOT发布后实际运行、资源/回调释放、键盘/可访问性分别留证，Linux/macOS不借Windows结果抵扣。NativeFormsDemo继续双目标；同源静态构造与.resx资源可消费，不将TaskDialog逻辑元素冒称可拖拽的Designer Control。 |

## 原生方案与边界

| 平台 | 实现 |
| --- | --- |
| Windows | TaskDialog 系统详情折叠区。 |
| Linux | GtkExpander 原生折叠控件与原生文本子项。 |
| macOS | NSButton disclosure + NSStackView，AppKit 原子原生组合。 |

普通属性/事件/行为按平台各自验收；Windows消息/句柄等真实专属契约单列🚫，但不抵扣Windows未实现项。Font/Image/Icon/Bitmap/Graphics公共类型的跨平台身份按统一平台计划先解决，不靠排除分母降低目标。

## 修改历史

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

## 原始资料

- [微软固定API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.taskdialogexpander?view=windowsdesktop-10.0)
- [Win32 TaskDialogIndirect](https://learn.microsoft.com/en-us/windows/win32/api/commctrl/nf-commctrl-taskdialogindirect)
- [GTK/GIO原生依据](https://docs.gtk.org/gtk4/class.Expander.html)
- [AppKit原生依据](https://developer.apple.com/documentation/appkit/nsstackview)
