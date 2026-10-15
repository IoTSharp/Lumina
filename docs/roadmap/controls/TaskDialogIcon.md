# TaskDialogIcon

⬜ **现状：**未实现。标准是 IDisposable 图标包装类，支持 Bitmap/Icon/IntPtr 构造及标准/盾牌图标静态字段；不是简单枚举。

- 基线：固定 .NET10 WinForms 10.0.11 / `b83f534`；评估日期 2026-10-15。
- 类型：`System.Windows.Forms.TaskDialogIcon`。 [全部成员、继承及接口任务](../api/TaskDialogIcon.md) · [98%口径](../coverage.md) · [公共前置](../foundations.md) · [平台条件](../platforms.md)。
- 排序：P1：系统图标和借用句柄；P2：Bitmap/资源、动态图标；跨平台受 Drawing 类型身份前置。
- 现有相关实现：[MessageBox 内部简化调用](../../../Lumina.Forms/MessageBox.cs)、[Win32 声明](../../../Lumina.Forms/Win32.cs)。两者均不是本类型的标准公开实现。

## 顺序任务

| 状态 | 优先级 / ID | 细节与验收 |
| --- | --- | --- |
| ⬜ | P1 / TaskDialogIcon-01 | 实现 Bitmap/Icon/IntPtr 三构造、只读 IconHandle、Dispose，逐一核对复制/借用/拥有语义；不能为方便一律 DestroyIcon 调用方传入句柄。 |
| ⬜ | P1 / TaskDialogIcon-02 | 提供 None/Information/Warning/Error/Shield 与各色 Shield*Bar 静态 readonly 字段，保留预定义资源标识与 HICON 的区别和可重复使用规则。 |
| ⬜ | P2 / TaskDialogIcon-03 | TaskDialogPage.Icon/Footnote.Icon 的绑定、动态图标切换、同一图标用于多页、图标先释放/宿主先关闭、重复 Dispose 按固定基线验收。 |
| ⬜ | P2 / TaskDialogIcon-04 | Bitmap alpha/透明像素、Icon 多尺寸、高 DPI 与 .resx 发布裁剪可用；System.Drawing.Common 平台限制不得靠内部转图片就宣称解决。 |
| ⬜ | P1 / TaskDialogIcon-05 | Windows 使用 taskdialog stock icon 标识或真实 HICON，TDF_USE_HICON_* 和 TDM_UPDATE_ICON 一致；构造中临时 HBITMAP/HICON 资源 finally 清理。 |
| ⬜ | P2 / TaskDialogIcon-06 | Linux stock icons 映射原生主题 GtkImage/GIcon，自定义图片用可支持的 GdkTexture；原生 IconHandle 不能伪装 HWND/HICON，IntPtr 专属契约单列。 |
| ⬜ | P3 / TaskDialogIcon-07 | macOS stock icons 映射系统 NSImage/适当 symbol，自定义图片由已冻结资源路线转换；彩色盾牌条的 Windows 特定装饰无精确原生等价。 |
| ⬜ | 验收 / TaskDialogIcon-V | 对完整 API 表逐成员核对签名、默认、修改限制、异常、状态和事件轨迹；真实 WinForms 与 Lumina 使用同场景。AOT发布后实际运行、资源/回调释放、键盘/可访问性分别留证，Linux/macOS不借Windows结果抵扣。NativeFormsDemo继续双目标；同源静态构造与.resx资源可消费，不将TaskDialog逻辑元素冒称可拖拽的Designer Control。 |

## 原生方案与边界

| 平台 | 实现 |
| --- | --- |
| Windows | 系统 stock task icon/HICON 包装，尊重预定义与拥有资源的区别。 |
| Linux | GtkImage/GIcon/GdkTexture，Windows HICON 不跨平台伪造。 |
| macOS | NSImage 原生图像，System.Drawing 构造可用性是共同阻塞。 |

普通属性/事件/行为按平台各自验收；Windows消息/句柄等真实专属契约单列🚫，但不抵扣Windows未实现项。Font/Image/Icon/Bitmap/Graphics公共类型的跨平台身份按统一平台计划先解决，不靠排除分母降低目标。

## 修改历史

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

## 原始资料

- [微软固定API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.taskdialogicon?view=windowsdesktop-10.0)
- [Win32 TaskDialogIndirect](https://learn.microsoft.com/en-us/windows/win32/api/commctrl/nf-commctrl-taskdialogindirect)
- [GTK/GIO原生依据](https://docs.gtk.org/gtk4/class.Image.html)
- [AppKit原生依据](https://developer.apple.com/documentation/appkit/nsimage)
