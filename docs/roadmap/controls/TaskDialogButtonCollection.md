# TaskDialogButtonCollection

⬜ **现状：**未实现。标准 Collection<TaskDialogButton> 派生带文本 Add 重载和 protected 集合钩子，不是简单 List 属性。

- 基线：固定 .NET10 WinForms 10.0.11 / `b83f534`；评估日期 2026-10-15。
- 类型：`System.Windows.Forms.TaskDialogButtonCollection`。 [全部成员、继承及接口任务](../api/TaskDialogButtonCollection.md) · [98%口径](../coverage.md) · [公共前置](../foundations.md) · [平台条件](../platforms.md)。
- 排序：P1：TaskDialogButton/Page 后，与页 Buttons 绑定同时实现。
- 现有相关实现：[MessageBox 内部简化调用](../../../Lumina.Forms/MessageBox.cs)、[Win32 声明](../../../Lumina.Forms/Win32.cs)。两者均不是本类型的标准公开实现。

## 顺序任务

| 状态 | 优先级 / ID | 细节与验收 |
| --- | --- | --- |
| ⬜ | P1 / TaskDialogButtonCollection-01 | 实现准确 Collection<TaskDialogButton> 继承、无参构造、Add(string,bool,bool) 返回刚创建按钮，保留 Count/索引器及全部泛型/非泛型接口形状。 |
| ⬜ | P1 / TaskDialogButtonCollection-02 | InsertItem/SetItem/RemoveItem/ClearItems 管理 BoundPage、native ID 和事件关联，null/重复项/跨页项/替换项行为按固定基线。 |
| ⬜ | P1 / TaskDialogButtonCollection-03 | Page.Buttons 可设置新集合，旧集合解绑；显示期间 Add/Remove/Replace/Clear 的可变/禁止范围明确，禁止私自实时重建 dialog 绕开标准限制。 |
| ⬜ | P2 / TaskDialogButtonCollection-04 | Add 标准按钮与自定义/CommandLink 混合，默认按钮、键盘顺序、隐藏/禁用项、相同文字不同对象与最终返回身份不变。 |
| ⬜ | P2 / TaskDialogButtonCollection-05 | 索引越界、CopyTo、枚举修改检测、IList 类型错误、清空再用等契约对照；无 native 宿主时不提前分配窗口资源。 |
| ⬜ | P1 / TaskDialogButtonCollection-06 | Windows 枚举为 TASKDIALOG_BUTTON 数组/common flags，在可允许时构建/导航提交，不另为集合创建 HWND。 |
| ⬜ | P2 / TaskDialogButtonCollection-07 | Linux 同一托管集合映射 GtkBox 中的原生按钮，item 是唯一数据源，不以 GTK child array 取代 public 集合。 |
| ⬜ | P3 / TaskDialogButtonCollection-08 | macOS 同一托管集合映射原生 NSButton 顺序与默认按钮，native host 变更不改变集合身份/绑定异常。 |
| ⬜ | 验收 / TaskDialogButtonCollection-V | 对完整 API 表逐成员核对签名、默认、修改限制、异常、状态和事件轨迹；真实 WinForms 与 Lumina 使用同场景。AOT发布后实际运行、资源/回调释放、键盘/可访问性分别留证，Linux/macOS不借Windows结果抵扣。NativeFormsDemo继续双目标；同源静态构造与.resx资源可消费，不将TaskDialog逻辑元素冒称可拖拽的Designer Control。 |

## 原生方案与边界

| 平台 | 实现 |
| --- | --- |
| Windows | 托管集合 + native 配置数组，和 TaskDialog 按钮结果 ID 统一。 |
| Linux | 无独立 native collection widget；GtkButton 子控件顺序由集合驱动。 |
| macOS | 无独立 native collection view；NSButton 顺序由集合驱动。 |

普通属性/事件/行为按平台各自验收；Windows消息/句柄等真实专属契约单列🚫，但不抵扣Windows未实现项。Font/Image/Icon/Bitmap/Graphics公共类型的跨平台身份按统一平台计划先解决，不靠排除分母降低目标。

## 修改历史

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

## 原始资料

- [微软固定API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.taskdialogbuttoncollection?view=windowsdesktop-10.0)
- [Win32 TaskDialogIndirect](https://learn.microsoft.com/en-us/windows/win32/api/commctrl/nf-commctrl-taskdialogindirect)
- [GTK/GIO原生依据](https://docs.gtk.org/gtk4/class.Box.html)
- [AppKit原生依据](https://developer.apple.com/documentation/appkit/nsstackview)
