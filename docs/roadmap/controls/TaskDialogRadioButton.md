# TaskDialogRadioButton

⬜ **现状：**未实现。标准是任务页内部单选元素，继承 TaskDialogControl；不能把全局 RadioButton 控件直接当同一个 public 类型。

- 基线：固定 .NET10 WinForms 10.0.11 / `b83f534`；评估日期 2026-10-15。
- 类型：`System.Windows.Forms.TaskDialogRadioButton`。 [全部成员、继承及接口任务](../api/TaskDialogRadioButton.md) · [98%口径](../coverage.md) · [公共前置](../foundations.md) · [平台条件](../platforms.md)。
- 排序：P2：页绑定/集合/基本按钮完成后。
- 现有相关实现：[MessageBox 内部简化调用](../../../Lumina.Forms/MessageBox.cs)、[Win32 声明](../../../Lumina.Forms/Win32.cs)。两者均不是本类型的标准公开实现。

## 顺序任务

| 状态 | 优先级 / ID | 细节与验收 |
| --- | --- | --- |
| ⬜ | P2 / TaskDialogRadioButton-01 | 实现无参/Text 构造、Text/Enabled/Checked、CheckedChanged/ToString、BoundPage/Tag，固定默认与非法绑定行为。 |
| ⬜ | P2 / TaskDialogRadioButton-02 | 同页单选互斥、初始无选项/多个 Checked、程序设置与用户点击的旧项/新项事件顺序按基线，不凭普通 RadioButton 推测。 |
| ⬜ | P2 / TaskDialogRadioButton-03 | 绑定后 Text/Checked/Enabled 修改、设置已选/未选、禁用当前项、页面导航后选择保留/解除行为逐项验收。 |
| ⬜ | P2 / TaskDialogRadioButton-04 | 方向键/Tab/Space、助记键、焦点/默认按钮不冲突；不会因选中 radio 而关闭 TaskDialog。 |
| ⬜ | P2 / TaskDialogRadioButton-05 | Windows 使用 TASKDIALOGCONFIG radio arrays/default ID、TDM_CLICK_RADIO_BUTTON/TDM_ENABLE_RADIO_BUTTON、TDN_RADIO_BUTTON_CLICKED，映射回同一对象。 |
| ⬜ | P2 / TaskDialogRadioButton-06 | Linux 使用分组 GtkCheckButton（GTK4 的 radio 模式），toggled 与程序写回去重；每页分组独立，不与页面 verification 同组。 |
| ⬜ | P3 / TaskDialogRadioButton-07 | macOS 使用 NSButton radio 类型与显式组/目标状态，保持 native 键盘和可访问性；CheckedChanged 顺序由共享状态协调。 |
| ⬜ | 验收 / TaskDialogRadioButton-V | 对完整 API 表逐成员核对签名、默认、修改限制、异常、状态和事件轨迹；真实 WinForms 与 Lumina 使用同场景。AOT发布后实际运行、资源/回调释放、键盘/可访问性分别留证，Linux/macOS不借Windows结果抵扣。NativeFormsDemo继续双目标；同源静态构造与.resx资源可消费，不将TaskDialog逻辑元素冒称可拖拽的Designer Control。 |

## 原生方案与边界

| 平台 | 实现 |
| --- | --- |
| Windows | TaskDialog 原生 radio buttons。 |
| Linux | GTK4 分组 GtkCheckButton；无须引用已移除 GtkRadioButton。 |
| macOS | NSButton radio 原生控件，page 维度管理互斥。 |

普通属性/事件/行为按平台各自验收；Windows消息/句柄等真实专属契约单列🚫，但不抵扣Windows未实现项。Font/Image/Icon/Bitmap/Graphics公共类型的跨平台身份按统一平台计划先解决，不靠排除分母降低目标。

## 修改历史

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

## 原始资料

- [微软固定API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.taskdialogradiobutton?view=windowsdesktop-10.0)
- [Win32 TaskDialogIndirect](https://learn.microsoft.com/en-us/windows/win32/api/commctrl/nf-commctrl-taskdialogindirect)
- [GTK/GIO原生依据](https://docs.gtk.org/gtk4/class.CheckButton.html)
- [AppKit原生依据](https://developer.apple.com/documentation/appkit/nsbutton)
