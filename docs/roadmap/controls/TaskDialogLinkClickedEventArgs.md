# TaskDialogLinkClickedEventArgs

⬜ **现状：**未实现的关联事件参数，属于 TaskDialogPage.LinkClicked 契约；本身不是控件也不创建 native 资源。

- 基线：固定 .NET10 WinForms 10.0.11 / `b83f534`；评估日期 2026-10-15。
- 类型：`System.Windows.Forms.TaskDialogLinkClickedEventArgs`。 [全部成员、继承及接口任务](../api/TaskDialogLinkClickedEventArgs.md) · [98%口径](../coverage.md) · [公共前置](../foundations.md) · [平台条件](../platforms.md)。
- 排序：P2：与 Page/Footnote/Expander 链接路由一起完成。
- 现有相关实现：[MessageBox 内部简化调用](../../../Lumina.Forms/MessageBox.cs)、[Win32 声明](../../../Lumina.Forms/Win32.cs)。两者均不是本类型的标准公开实现。

## 顺序任务

| 状态 | 优先级 / ID | 细节与验收 |
| --- | --- | --- |
| ⬜ | P2 / TaskDialogLinkClickedEventArgs-01 | 实现 string linkHref 构造和只读 LinkHref，保留精确字符串/null 的固定基线行为，不引入 URL 校验/规范化导致 API 不兼容。 |
| ⬜ | P2 / TaskDialogLinkClickedEventArgs-02 | TDN_HYPERLINK_CLICKED 的 Unicode href 正确复制到托管字符串，原生指针失效后参数仍有效，不保留借用指针。 |
| ⬜ | P2 / TaskDialogLinkClickedEventArgs-03 | Heading/文本/脚注/详情中允许链接区域以官方端为准，EnableLinks=false 不误发事件；多链接/相同显示文本不同 href 有测试。 |
| ⬜ | P2 / TaskDialogLinkClickedEventArgs-04 | 事件只报告点击不自动打开浏览器；调用者选择导航/Close/更新页时事件参数不被复用或变更。 |
| ⬜ | P2 / TaskDialogLinkClickedEventArgs-05 | Windows 由 TaskDialog callback 构造，键盘和鼠标激活相同 href；无独立 HWND 或释放模型。 |
| ⬜ | P2 / TaskDialogLinkClickedEventArgs-06 | Linux GtkLabel 原生 activate-link 转同一事件参数，保留字符串并遵守标准默认跳转行为；不直接执行 URI。 |
| ⬜ | P3 / TaskDialogLinkClickedEventArgs-07 | macOS 原生文本 link delegate 转同一参数，防止系统默认外跳与业务处理重复；同样不创建新 UI 类型。 |
| ⬜ | 验收 / TaskDialogLinkClickedEventArgs-V | 对完整 API 表逐成员核对签名、默认、修改限制、异常、状态和事件轨迹；真实 WinForms 与 Lumina 使用同场景。AOT发布后实际运行、资源/回调释放、键盘/可访问性分别留证，Linux/macOS不借Windows结果抵扣。NativeFormsDemo继续双目标；同源静态构造与.resx资源可消费，不将TaskDialog逻辑元素冒称可拖拽的Designer Control。 |

## 原生方案与边界

| 平台 | 实现 |
| --- | --- |
| Windows | 原生 hyperlink callback 的托管值对象。 |
| Linux | GtkLabel 链接信号参数转换。 |
| macOS | 原生文本链接 delegate 参数转换。 |

普通属性/事件/行为按平台各自验收；Windows消息/句柄等真实专属契约单列🚫，但不抵扣Windows未实现项。Font/Image/Icon/Bitmap/Graphics公共类型的跨平台身份按统一平台计划先解决，不靠排除分母降低目标。

## 修改历史

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

## 原始资料

- [微软固定API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.taskdialoglinkclickedeventargs?view=windowsdesktop-10.0)
- [Win32 TaskDialogIndirect](https://learn.microsoft.com/en-us/windows/win32/api/commctrl/nf-commctrl-taskdialogindirect)
- [GTK/GIO原生依据](https://docs.gtk.org/gtk4/class.Label.html)
- [AppKit原生依据](https://developer.apple.com/documentation/appkit/nstextview)
