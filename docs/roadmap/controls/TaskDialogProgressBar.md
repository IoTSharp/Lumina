# TaskDialogProgressBar

⬜ **现状：**未实现。标准任务页进度模型包含范围、值、MarqueeSpeed 和六种状态（含隐藏 None），不等于普通 ProgressBar 默认值。

- 基线：固定 .NET10 WinForms 10.0.11 / `b83f534`；评估日期 2026-10-15。
- 类型：`System.Windows.Forms.TaskDialogProgressBar`。 [全部成员、继承及接口任务](../api/TaskDialogProgressBar.md) · [98%口径](../coverage.md) · [公共前置](../foundations.md) · [平台条件](../platforms.md)。
- 排序：P2：页绑定与原生消息路由后。
- 现有相关实现：[MessageBox 内部简化调用](../../../Lumina.Forms/MessageBox.cs)、[Win32 声明](../../../Lumina.Forms/Win32.cs)。两者均不是本类型的标准公开实现。

## 顺序任务

| 状态 | 优先级 / ID | 细节与验收 |
| --- | --- | --- |
| ⬜ | P2 / TaskDialogProgressBar-01 | 实现两种构造、Minimum/Maximum/Value/MarqueeSpeed/State 与准确默认/范围，继承 BoundPage/Tag；无标准 ValueChanged 就不自造事件。 |
| ⬜ | P2 / TaskDialogProgressBar-02 | 逐项覆盖 Normal/Paused/Error/Marquee/MarqueePaused/None，范围/值的赋值顺序、越界异常、切换状态后值保留与可见性以固定基线为准。 |
| ⬜ | P2 / TaskDialogProgressBar-03 | 绑定前/后动态改变范围、Value/速度/State，后台任务必须经 UI 调度更新；关闭/导航后更新不得命中失效 native hwnd。 |
| ⬜ | P2 / TaskDialogProgressBar-04 | Marquee 暂停/恢复、None 隐藏、DPI/RTL/可访问性进度范围与繁忙状态有真实探针，不能只变颜色/字段。 |
| ⬜ | P2 / TaskDialogProgressBar-05 | Windows 使用 TDM_SET_PROGRESS_BAR_RANGE/POS/STATE、TDM_SET_MARQUEE_PROGRESS_BAR/SET_PROGRESS_BAR_MARQUEE 与 task flags。 |
| ⬜ | P2 / TaskDialogProgressBar-06 | Linux 使用 GtkProgressBar fraction/pulse 与原生状态样式，None 显示控制/MarqueePaused 停止 pulse 保留状态；Paused/Error 颜色由主题能力决定并单列差异。 |
| ⬜ | P3 / TaskDialogProgressBar-07 | macOS 使用 NSProgressIndicator 确定/不确定模式与 start/stopAnimation，范围/值同步；原生没有 Win32 三种颜色状态的精确等价，保留语义并记录视觉差异。 |
| ⬜ | 验收 / TaskDialogProgressBar-V | 对完整 API 表逐成员核对签名、默认、修改限制、异常、状态和事件轨迹；真实 WinForms 与 Lumina 使用同场景。AOT发布后实际运行、资源/回调释放、键盘/可访问性分别留证，Linux/macOS不借Windows结果抵扣。NativeFormsDemo继续双目标；同源静态构造与.resx资源可消费，不将TaskDialog逻辑元素冒称可拖拽的Designer Control。 |

## 原生方案与边界

| 平台 | 实现 |
| --- | --- |
| Windows | TaskDialog 原生进度条与 TDM_*。 |
| Linux | GtkProgressBar 原生进度与 pulse，不新造绘制进度控件。 |
| macOS | NSProgressIndicator，原生 busy/确定进度；状态颜色不假称逐像素等价。 |

普通属性/事件/行为按平台各自验收；Windows消息/句柄等真实专属契约单列🚫，但不抵扣Windows未实现项。Font/Image/Icon/Bitmap/Graphics公共类型的跨平台身份按统一平台计划先解决，不靠排除分母降低目标。

## 修改历史

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

## 原始资料

- [微软固定API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.taskdialogprogressbar?view=windowsdesktop-10.0)
- [Win32 TaskDialogIndirect](https://learn.microsoft.com/en-us/windows/win32/api/commctrl/nf-commctrl-taskdialogindirect)
- [GTK/GIO原生依据](https://docs.gtk.org/gtk4/class.ProgressBar.html)
- [AppKit原生依据](https://developer.apple.com/documentation/appkit/nsprogressindicator)
