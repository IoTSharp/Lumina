# TaskDialogButton

⬜ **现状：**未实现。标准按钮包含常用结果按钮静态属性、Click、AllowCloseDialog 等语义，返回按钮对象身份不能被 DialogResult 简化。

- 基线：固定 .NET10 WinForms 10.0.11 / `b83f534`；评估日期 2026-10-15。
- 类型：`System.Windows.Forms.TaskDialogButton`。 [全部成员、继承及接口任务](../api/TaskDialogButton.md) · [98%口径](../coverage.md) · [公共前置](../foundations.md) · [平台条件](../platforms.md)。
- 排序：P1：TaskDialogControl/Page 后，最小可用 TaskDialog 的必要部分。
- 现有相关实现：[MessageBox 内部简化调用](../../../Lumina.Forms/MessageBox.cs)、[Win32 声明](../../../Lumina.Forms/Win32.cs)。两者均不是本类型的标准公开实现。

## 顺序任务

| 状态 | 优先级 / ID | 细节与验收 |
| --- | --- | --- |
| ⬜ | P1 / TaskDialogButton-01 | 补无参/Text-enabled-allowCloseDialog 构造、Text/Enabled/Visible/AllowCloseDialog/ShowShieldIcon、Click/PerformClick，继承 BoundPage/Tag，不误用普通 Button。 |
| ⬜ | P1 / TaskDialogButton-02 | 实现 OK/Cancel/Yes/No/Abort/Retry/Ignore/Close/Help/TryAgain/Continue 静态按钮，核对每次获取、Equals/GetHashCode 与 common button ID 的真实语义。 |
| ⬜ | P1 / TaskDialogButton-03 | 点击先产生准确事件再依据 AllowCloseDialog 决定是否结束；Click handler 中修改允许关闭、Navigate/Close、抛异常和 PerformClick 递归分别对照。 |
| ⬜ | P1 / TaskDialogButton-04 | Enabled/Visible/Text/ShowShieldIcon 逐项验证绑定后可变范围；默认按钮、Esc/Enter、禁用/隐藏按钮不可被错误激活。 |
| ⬜ | P2 / TaskDialogButton-05 | 标准共用按钮本地化文本、用户文字/&助记键、同文案多个按钮、集合移除/重排与结果对象关联有测试。 |
| ⬜ | P1 / TaskDialogButton-06 | Windows 使用 TASKDIALOG_BUTTON/common buttons、TDM_ENABLE_BUTTON/TDM_CLICK_BUTTON/TDM_SET_BUTTON_ELEVATION_REQUIRED_STATE、TDN_BUTTON_CLICKED；不为每个逻辑按钮创建另一层普通 HWND。 |
| ⬜ | P2 / TaskDialogButton-07 | Linux 用 GtkButton，默认/助记键/敏感状态和 clicked 映射；盾牌只表示状态/装饰，不能自行启动提权改变标准行为。 |
| ⬜ | P3 / TaskDialogButton-08 | macOS 用 NSButton 与 keyEquivalent/default/cancel，accessibility状态同步；ShowShieldIcon 的 Windows 提权视觉无精确系统等价，单列视觉差异。 |
| ⬜ | 验收 / TaskDialogButton-V | 对完整 API 表逐成员核对签名、默认、修改限制、异常、状态和事件轨迹；真实 WinForms 与 Lumina 使用同场景。AOT发布后实际运行、资源/回调释放、键盘/可访问性分别留证，Linux/macOS不借Windows结果抵扣。NativeFormsDemo继续双目标；同源静态构造与.resx资源可消费，不将TaskDialog逻辑元素冒称可拖拽的Designer Control。 |

## 原生方案与边界

| 平台 | 实现 |
| --- | --- |
| Windows | TaskDialog 原生普通/common buttons。 |
| Linux | GtkButton，托管映射同一 TaskDialogButton 对象与关闭策略。 |
| macOS | NSButton，结果身份与 AllowCloseDialog 仍精确实现。 |

普通属性/事件/行为按平台各自验收；Windows消息/句柄等真实专属契约单列🚫，但不抵扣Windows未实现项。Font/Image/Icon/Bitmap/Graphics公共类型的跨平台身份按统一平台计划先解决，不靠排除分母降低目标。

## 修改历史

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

## 原始资料

- [微软固定API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.taskdialogbutton?view=windowsdesktop-10.0)
- [Win32 TaskDialogIndirect](https://learn.microsoft.com/en-us/windows/win32/api/commctrl/nf-commctrl-taskdialogindirect)
- [GTK/GIO原生依据](https://docs.gtk.org/gtk4/class.Button.html)
- [AppKit原生依据](https://developer.apple.com/documentation/appkit/nsbutton)
