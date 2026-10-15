# TaskDialogPage

⬜ **现状：**未实现。标准为独立页数据/生命周期模型，具有17个公开属性和4个事件；不能继承普通 Form 以近似代替。

- 基线：固定 .NET10 WinForms 10.0.11 / `b83f534`；评估日期 2026-10-15。
- 类型：`System.Windows.Forms.TaskDialogPage`。 [全部成员、继承及接口任务](../api/TaskDialogPage.md) · [98%口径](../coverage.md) · [公共前置](../foundations.md) · [平台条件](../platforms.md)。
- 排序：P1：先确定页与宿主绑定；P2：导航、链接和全部子元素。
- 现有相关实现：[MessageBox 内部简化调用](../../../Lumina.Forms/MessageBox.cs)、[Win32 声明](../../../Lumina.Forms/Win32.cs)。两者均不是本类型的标准公开实现。

## 顺序任务

| 状态 | 优先级 / ID | 细节与验收 |
| --- | --- | --- |
| ⬜ | P1 / TaskDialogPage-01 | 实现 Caption/Heading/Text/Icon、Buttons/RadioButtons、DefaultButton、ProgressBar/Verification/Expander/Footnote、AllowCancel/AllowMinimize/EnableLinks/RightToLeftLayout/SizeToContent/BoundDialog 的确切类型和默认。 |
| ⬜ | P1 / TaskDialogPage-02 | 实现 Created/Destroyed/HelpRequest/LinkClicked 的标准事件和 On* 扩展；Created 时可访问 BoundDialog，Destroyed/导航后旧页解绑时机与原生消息有轨迹。 |
| ⬜ | P1 / TaskDialogPage-03 | 明确每个属性在未显示、已绑定、导航中、关闭后的可写性；系统不支持动态变更时保持标准异常/限制，不偷偷重建窗口改变生命周期。 |
| ⬜ | P2 / TaskDialogPage-04 | Navigate(TaskDialogPage) 的目标重复绑定、同一页导航、旧页/新页事件顺序、按钮/单选/验证值恢复、focus/default/links 状态完整对照。 |
| ⬜ | P2 / TaskDialogPage-05 | DefaultButton 必须与 Buttons/标准共用按钮的归属兼容；无按钮、没有默认、全部禁用、radio初始全未选、取消关闭等边界按基线而非新增方便规则。 |
| ⬜ | P2 / TaskDialogPage-06 | Text/Footnote/Expander 中链接只在 EnableLinks 允许时产生 LinkClicked；Heading 等区域是否支持按基线记录，不假设所有文字均接受 anchor。保留原 LinkHref，不自动用 shell 打开目标。 |
| ⬜ | P1 / TaskDialogPage-07 | Windows 用 TASKDIALOGCONFIG 对应字段和 TDM_SET/UPDATE_ELEMENT_TEXT、TDM_NAVIGATE_PAGE；回调把 native ID/事件定位到当前 page。 |
| ⬜ | P2 / TaskDialogPage-08 | Linux 用同一 GtkWindow 内的原生控件组合替换页内容；页是逻辑对象，不能每次 Navigate 新开一个模态窗导致 owner/Task 结果断裂。 |
| ⬜ | P3 / TaskDialogPage-09 | macOS 用 NSPanel content/accessory native views 更新页，保持同一模态会话；平台窗口最小化、RTL排版和 SizeToContent 差异独立验收。 |
| ⬜ | 验收 / TaskDialogPage-V | 对完整 API 表逐成员核对签名、默认、修改限制、异常、状态和事件轨迹；真实 WinForms 与 Lumina 使用同场景。AOT发布后实际运行、资源/回调释放、键盘/可访问性分别留证，Linux/macOS不借Windows结果抵扣。NativeFormsDemo继续双目标；同源静态构造与.resx资源可消费，不将TaskDialog逻辑元素冒称可拖拽的Designer Control。 |

## 原生方案与边界

| 平台 | 实现 |
| --- | --- |
| Windows | 系统 TaskDialogIndirect page 配置与导航消息。 |
| Linux | 原生 widgets 页布局；共享托管绑定/导航模型，GTK 信号不能代替完整页生命周期。 |
| macOS | 原生 content/accessoryView 页布局；普通布局与资源身份不从 portable 分母排除。 |

普通属性/事件/行为按平台各自验收；Windows消息/句柄等真实专属契约单列🚫，但不抵扣Windows未实现项。Font/Image/Icon/Bitmap/Graphics公共类型的跨平台身份按统一平台计划先解决，不靠排除分母降低目标。

## 修改历史

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

## 原始资料

- [微软固定API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.taskdialogpage?view=windowsdesktop-10.0)
- [Win32 TaskDialogIndirect](https://learn.microsoft.com/en-us/windows/win32/api/commctrl/nf-commctrl-taskdialogindirect)
- [GTK/GIO原生依据](https://docs.gtk.org/gtk4/class.Box.html)
- [AppKit原生依据](https://developer.apple.com/documentation/appkit/nsstackview)
