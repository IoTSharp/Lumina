# TaskDialogRadioButtonCollection

⬜ **现状：**未实现。标准 Collection<TaskDialogRadioButton> 派生，除集合行为还需处理页内单选组绑定。

- 基线：固定 .NET10 WinForms 10.0.11 / `b83f534`；评估日期 2026-10-15。
- 类型：`System.Windows.Forms.TaskDialogRadioButtonCollection`。 [全部成员、继承及接口任务](../api/TaskDialogRadioButtonCollection.md) · [98%口径](../coverage.md) · [公共前置](../foundations.md) · [平台条件](../platforms.md)。
- 排序：P2：TaskDialogPage/RadioButton 后。
- 现有相关实现：[MessageBox 内部简化调用](../../../Lumina.Forms/MessageBox.cs)、[Win32 声明](../../../Lumina.Forms/Win32.cs)。两者均不是本类型的标准公开实现。

## 顺序任务

| 状态 | 优先级 / ID | 细节与验收 |
| --- | --- | --- |
| ⬜ | P2 / TaskDialogRadioButtonCollection-01 | 实现准确 Collection<TaskDialogRadioButton> 继承、无参构造、Add(string) 返回新 radio，全泛型/非泛型集合成员由完整 API 验收。 |
| ⬜ | P2 / TaskDialogRadioButtonCollection-02 | InsertItem/SetItem/RemoveItem/ClearItems 维护 BoundPage/单选组/native ID，null/重复/跨页复用和显示期间修改限制以固定基线为准。 |
| ⬜ | P2 / TaskDialogRadioButtonCollection-03 | 初始多个 Checked 项、没有选中项、移除已选项/替换集合、导航恢复与旧/新 CheckedChanged 顺序完整对照，不自选第一个修饰状态。 |
| ⬜ | P2 / TaskDialogRadioButtonCollection-04 | 集合顺序影响键盘方向键、focus/default radio；禁用项/重复文字仍按独立对象路由，用户选择不能关闭对话框。 |
| ⬜ | P2 / TaskDialogRadioButtonCollection-05 | 索引器/CopyTo/枚举/IList 异常与 Clear 后重用行为，绑定失败回滚不留下旧组 native 信号订阅。 |
| ⬜ | P2 / TaskDialogRadioButtonCollection-06 | Windows 提交 native radio arrays/default ID，并将 TDN_RADIO_BUTTON_CLICKED 仅路由当前页集合。 |
| ⬜ | P2 / TaskDialogRadioButtonCollection-07 | Linux GtkCheckButton group 由集合/页建立和解除，不与其他页或 Verification 共用原生 group。 |
| ⬜ | P3 / TaskDialogRadioButtonCollection-08 | macOS NSButton radio 同组状态由标准集合管理，换页重组不触发额外用户事件，native target-action 正确解绑。 |
| ⬜ | 验收 / TaskDialogRadioButtonCollection-V | 对完整 API 表逐成员核对签名、默认、修改限制、异常、状态和事件轨迹；真实 WinForms 与 Lumina 使用同场景。AOT发布后实际运行、资源/回调释放、键盘/可访问性分别留证，Linux/macOS不借Windows结果抵扣。NativeFormsDemo继续双目标；同源静态构造与.resx资源可消费，不将TaskDialog逻辑元素冒称可拖拽的Designer Control。 |

## 原生方案与边界

| 平台 | 实现 |
| --- | --- |
| Windows | TaskDialog radio ID 数组与托管集合单一映射。 |
| Linux | 分组 GtkCheckButton 原生控件序列。 |
| macOS | NSButton radio 原生组，托管集合保持标准接口。 |

普通属性/事件/行为按平台各自验收；Windows消息/句柄等真实专属契约单列🚫，但不抵扣Windows未实现项。Font/Image/Icon/Bitmap/Graphics公共类型的跨平台身份按统一平台计划先解决，不靠排除分母降低目标。

## 修改历史

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

## 原始资料

- [微软固定API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.taskdialogradiobuttoncollection?view=windowsdesktop-10.0)
- [Win32 TaskDialogIndirect](https://learn.microsoft.com/en-us/windows/win32/api/commctrl/nf-commctrl-taskdialogindirect)
- [GTK/GIO原生依据](https://docs.gtk.org/gtk4/class.CheckButton.html)
- [AppKit原生依据](https://developer.apple.com/documentation/appkit/nsbutton)
