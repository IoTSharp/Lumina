# TaskDialogCommandLinkButton

⬜ **现状：**未实现。标准继承 TaskDialogButton 并增加 DescriptionText；它是任务页命令链接，不是普通网页链接或 LinkLabel。

- 基线：固定 .NET10 WinForms 10.0.11 / `b83f534`；评估日期 2026-10-15。
- 类型：`System.Windows.Forms.TaskDialogCommandLinkButton`。 [全部成员、继承及接口任务](../api/TaskDialogCommandLinkButton.md) · [98%口径](../coverage.md) · [公共前置](../foundations.md) · [平台条件](../platforms.md)。
- 排序：P2：普通 TaskDialogButton 完成后。
- 现有相关实现：[MessageBox 内部简化调用](../../../Lumina.Forms/MessageBox.cs)、[Win32 声明](../../../Lumina.Forms/Win32.cs)。两者均不是本类型的标准公开实现。

## 顺序任务

| 状态 | 优先级 / ID | 细节与验收 |
| --- | --- | --- |
| ⬜ | P2 / TaskDialogCommandLinkButton-01 | 实现准确继承与两种构造、DescriptionText，保留 Text/Enabled/Visible/AllowCloseDialog/ShowShieldIcon/Click/PerformClick 的父类语义。 |
| ⬜ | P2 / TaskDialogCommandLinkButton-02 | 描述为空/多行/Unicode/很长文字、主标题与描述换行、DPI/RTL/SizeToContent 的布局按真实 TaskDialog 对照；不通过串接公共 Text 丢失独立属性。 |
| ⬜ | P2 / TaskDialogCommandLinkButton-03 | CommandLink 与普通按钮混放、默认按钮、返回对象身份、禁用/隐藏、AllowCloseDialog=false 和 Click 内导航路径完整验收。 |
| ⬜ | P2 / TaskDialogCommandLinkButton-04 | 显示后 DescriptionText/Text 等属性允许或禁止修改的标准范围明确，不能为看似实时更新而重开模态会话。 |
| ⬜ | P2 / TaskDialogCommandLinkButton-05 | Windows 使用 TASKDIALOGCONFIG 的 command-link flags 与按钮文本格式，盾牌/默认键处理沿原生任务窗口路径。 |
| ⬜ | P2 / TaskDialogCommandLinkButton-06 | Linux 用 GtkButton 内含原生 GtkBox/GtkLabel 主文字与说明，保留 button 角色和单一点击；GtkLinkButton 会打开 URI，不是等价控件。 |
| ⬜ | P3 / TaskDialogCommandLinkButton-07 | macOS 用 NSButton/原生文本容器组合实现整块命令点击，键盘/辅助技术仍按按钮；NSLinkTextAttributes 文本链接不作为对应。 |
| ⬜ | 验收 / TaskDialogCommandLinkButton-V | 对完整 API 表逐成员核对签名、默认、修改限制、异常、状态和事件轨迹；真实 WinForms 与 Lumina 使用同场景。AOT发布后实际运行、资源/回调释放、键盘/可访问性分别留证，Linux/macOS不借Windows结果抵扣。NativeFormsDemo继续双目标；同源静态构造与.resx资源可消费，不将TaskDialog逻辑元素冒称可拖拽的Designer Control。 |

## 原生方案与边界

| 平台 | 实现 |
| --- | --- |
| Windows | 系统任务窗口 command-link button。 |
| Linux | GtkButton + 原生 label/box 组成，明确没有完全相同外观的单个系统控件。 |
| macOS | NSButton 与原生文本组合，Windows command-link 视觉按平台适配。 |

普通属性/事件/行为按平台各自验收；Windows消息/句柄等真实专属契约单列🚫，但不抵扣Windows未实现项。Font/Image/Icon/Bitmap/Graphics公共类型的跨平台身份按统一平台计划先解决，不靠排除分母降低目标。

## 修改历史

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

## 原始资料

- [微软固定API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.taskdialogcommandlinkbutton?view=windowsdesktop-10.0)
- [Win32 TaskDialogIndirect](https://learn.microsoft.com/en-us/windows/win32/api/commctrl/nf-commctrl-taskdialogindirect)
- [GTK/GIO原生依据](https://docs.gtk.org/gtk4/class.Button.html)
- [AppKit原生依据](https://developer.apple.com/documentation/appkit/nsbutton)
