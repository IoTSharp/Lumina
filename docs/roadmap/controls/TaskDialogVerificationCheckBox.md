# TaskDialogVerificationCheckBox

⬜ **现状：**未实现。标准验证勾选元素仅 Text/Checked 与 CheckedChanged，不能添加普通 CheckBox 的三态表面改变 API。

- 基线：固定 .NET10 WinForms 10.0.11 / `b83f534`；评估日期 2026-10-15。
- 类型：`System.Windows.Forms.TaskDialogVerificationCheckBox`。 [全部成员、继承及接口任务](../api/TaskDialogVerificationCheckBox.md) · [98%口径](../coverage.md) · [公共前置](../foundations.md) · [平台条件](../platforms.md)。
- 排序：P2：TaskDialogPage/Control 基础后。
- 现有相关实现：[MessageBox 内部简化调用](../../../Lumina.Forms/MessageBox.cs)、[Win32 声明](../../../Lumina.Forms/Win32.cs)。两者均不是本类型的标准公开实现。

## 顺序任务

| 状态 | 优先级 / ID | 细节与验收 |
| --- | --- | --- |
| ⬜ | P2 / TaskDialogVerificationCheckBox-01 | 实现无参与 Text/isChecked 构造、Text/Checked/CheckedChanged/ToString，BoundPage/Tag 与 page.Verification 归属正确。 |
| ⬜ | P2 / TaskDialogVerificationCheckBox-02 | 初始已选/未选、重复赋值、鼠标/Space 与程序设置的事件次数/时点准确；Checked 变化不能自动关闭对话框。 |
| ⬜ | P2 / TaskDialogVerificationCheckBox-03 | 点击/切页/关闭时 native 返回值与对象 Checked 同步，导航切换验证对象后旧对象解除关联，不漏最后一次勾选。 |
| ⬜ | P2 / TaskDialogVerificationCheckBox-04 | Text 为空、运行时修改、长文本/换行/助记键/RTL/DPI 以及只有 verification 无按钮的交互按官方验收。 |
| ⬜ | P2 / TaskDialogVerificationCheckBox-05 | Windows 使用 pszVerificationText/TDF_VERIFICATION_FLAG_CHECKED、TDM_CLICK_VERIFICATION/TDN_VERIFICATION_CLICKED 和 native 最终返回值。 |
| ⬜ | P2 / TaskDialogVerificationCheckBox-06 | Linux 使用独立 GtkCheckButton，toggled 和 setter 双向去重；不得错误放入 radio group 或伪造 Enabled 公开属性。 |
| ⬜ | P3 / TaskDialogVerificationCheckBox-07 | macOS 使用 NSButton checkbox；若采用 NSAlert suppressionButton 仅在全部 Text/事件/生命周期等价时复用，否则 accessory 原生 checkbox。 |
| ⬜ | 验收 / TaskDialogVerificationCheckBox-V | 对完整 API 表逐成员核对签名、默认、修改限制、异常、状态和事件轨迹；真实 WinForms 与 Lumina 使用同场景。AOT发布后实际运行、资源/回调释放、键盘/可访问性分别留证，Linux/macOS不借Windows结果抵扣。NativeFormsDemo继续双目标；同源静态构造与.resx资源可消费，不将TaskDialog逻辑元素冒称可拖拽的Designer Control。 |

## 原生方案与边界

| 平台 | 实现 |
| --- | --- |
| Windows | TaskDialog 原生 verification checkbox。 |
| Linux | GtkCheckButton 原生复选。 |
| macOS | NSButton checkbox 或经验证的 NSAlert suppression button。 |

普通属性/事件/行为按平台各自验收；Windows消息/句柄等真实专属契约单列🚫，但不抵扣Windows未实现项。Font/Image/Icon/Bitmap/Graphics公共类型的跨平台身份按统一平台计划先解决，不靠排除分母降低目标。

## 修改历史

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

## 原始资料

- [微软固定API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.taskdialogverificationcheckbox?view=windowsdesktop-10.0)
- [Win32 TaskDialogIndirect](https://learn.microsoft.com/en-us/windows/win32/api/commctrl/nf-commctrl-taskdialogindirect)
- [GTK/GIO原生依据](https://docs.gtk.org/gtk4/class.CheckButton.html)
- [AppKit原生依据](https://developer.apple.com/documentation/appkit/nsalert)
