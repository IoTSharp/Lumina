# TaskDialogFootnote

⬜ **现状：**未实现。标准任务页脚注包含 Text/Icon，配合 page.EnableLinks 的点击，不是新增独立窗口。

- 基线：固定 .NET10 WinForms 10.0.11 / `b83f534`；评估日期 2026-10-15。
- 类型：`System.Windows.Forms.TaskDialogFootnote`。 [全部成员、继承及接口任务](../api/TaskDialogFootnote.md) · [98%口径](../coverage.md) · [公共前置](../foundations.md) · [平台条件](../platforms.md)。
- 排序：P2：TaskDialogPage、TaskDialogIcon/LinkClicked 后。
- 现有相关实现：[MessageBox 内部简化调用](../../../Lumina.Forms/MessageBox.cs)、[Win32 声明](../../../Lumina.Forms/Win32.cs)。两者均不是本类型的标准公开实现。

## 顺序任务

| 状态 | 优先级 / ID | 细节与验收 |
| --- | --- | --- |
| ⬜ | P2 / TaskDialogFootnote-01 | 实现无参/Text 构造、Text/Icon/ToString 与继承 BoundPage/Tag，准确默认/null/空文字与资源归属。 |
| ⬜ | P2 / TaskDialogFootnote-02 | Text 的运行时更新、图标在绑定前后允许的变化、页导航时解绑/恢复与脚注隐藏规则按官方逐项验证。 |
| ⬜ | P2 / TaskDialogFootnote-03 | EnableLinks 开/关、脚注中的多个 anchor、键盘进入链接、LinkClicked.LinkHref 和标准事件顺序；不自动打开链接代替事件。 |
| ⬜ | P2 / TaskDialogFootnote-04 | 脚注前后折叠区 Position、图像尺寸/对齐、多行/RTL/DPI/可访问性朗读与页面 SizeToContent 对照。 |
| ⬜ | P2 / TaskDialogFootnote-05 | Windows 使用 pszFooter/hFooterIcon 与 TDM_SET/UPDATE_ELEMENT_TEXT(TDE_FOOTER)、TDM_UPDATE_ICON(TDIE_ICON_FOOTER)，遵守实际可变范围。 |
| ⬜ | P2 / TaskDialogFootnote-06 | Linux 使用 GtkBox/GtkImage/GtkLabel（markup/link）原生组合，activate-link 拦截发标准事件；脚注仅布局区域不造新公共组件。 |
| ⬜ | P3 / TaskDialogFootnote-07 | macOS 使用 NSImageView 与原生可交互文本组件，link delegate 转事件；内建超链接默认跳转需按 WinForms 契约阻止。 |
| ⬜ | 验收 / TaskDialogFootnote-V | 对完整 API 表逐成员核对签名、默认、修改限制、异常、状态和事件轨迹；真实 WinForms 与 Lumina 使用同场景。AOT发布后实际运行、资源/回调释放、键盘/可访问性分别留证，Linux/macOS不借Windows结果抵扣。NativeFormsDemo继续双目标；同源静态构造与.resx资源可消费，不将TaskDialog逻辑元素冒称可拖拽的Designer Control。 |

## 原生方案与边界

| 平台 | 实现 |
| --- | --- |
| Windows | TaskDialog 原生 footer 元素。 |
| Linux | GtkImage/Label 原生组合。 |
| macOS | NSImageView/原生文本组合；资源类型身份前置不回避。 |

普通属性/事件/行为按平台各自验收；Windows消息/句柄等真实专属契约单列🚫，但不抵扣Windows未实现项。Font/Image/Icon/Bitmap/Graphics公共类型的跨平台身份按统一平台计划先解决，不靠排除分母降低目标。

## 修改历史

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

## 原始资料

- [微软固定API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.taskdialogfootnote?view=windowsdesktop-10.0)
- [Win32 TaskDialogIndirect](https://learn.microsoft.com/en-us/windows/win32/api/commctrl/nf-commctrl-taskdialogindirect)
- [GTK/GIO原生依据](https://docs.gtk.org/gtk4/class.Label.html)
- [AppKit原生依据](https://developer.apple.com/documentation/appkit/nsimageview)
