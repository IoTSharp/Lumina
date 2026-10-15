# NativeWindow

⬜ **现状：**没有标准 NativeWindow。现有 Win32.cs 和窗口过程存在，但未暴露标准 subclass/handle 契约。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/NativeWindow.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：同名标准类型尚未实现，无可冒充为完整契约的源码。
- 排序：Win32/Control 句柄与消息基础；Windows 原生扩展兼容。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P1 / NativeWindow-01 | 实现 Handle、AssignHandle/ReleaseHandle/CreateHandle/DestroyHandle、FromHandle、DefWndProc、WndProc(ref Message) 与 CreateParams；借用句柄和拥有句柄明确区分。 |
| ⬜ | P1 / NativeWindow-02 | 支持对已存在 HWND 子类化、链式窗口过程和正确恢复；WM_NCDESTROY、外部窗口销毁和重复 Assign/Release 的顺序/异常对照。 |
| ⬜ | P1 / NativeWindow-03 | 实现 OnHandleChange/OnThreadException 和 DPI 前后通知扩展；native 回调捕获异常并交给 Application 策略，不能全部吞掉。 |
| ⬜ | P1 / NativeWindow-04 | 保持 handle→NativeWindow 映射、同 HWND 多层包装和 GC/终结清理规则；AOT 静态 thunk、GCHandle/委托 root 与释放有实际探针。 |
| ⬜ | P1 / NativeWindow-05 | 自定义继承窗口类、CreateParams 样式/parent/菜单、DefWndProc 修改 Result 的标准消费示例可无改写运行。 |
| ⬜ | P2 / NativeWindow-06 | Windows 用 USER32 RegisterClassEx/CreateWindowEx/SetWindowLongPtr/CallWindowProc/DefWindowProc；DestroyHandle 只销毁拥有的窗口，ReleaseHandle 不误销毁外部 HWND。 |
| ⬜ | P2 / NativeWindow-07 | Linux 🚫 HWND/Win32 Message subclass 无原生等价，不把 GtkWidget 指针塞入 Handle 并宣称可兼容 WndProc；普通控件后端实现不依赖 public NativeWindow。 |
| ⬜ | P3 / NativeWindow-08 | macOS 🚫 NSWindow/NSView 与 Objective-C responder 消息不是 Win32 WndProc；保留 Windows 扩展责任，平台差异不抵扣 Windows 未实现项。 |
| ⬜ | 验收 / NativeWindow-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| USER32 window subclass 与标准消息结构，需完整 AOT-safe 生命周期。 | Windows 专属契约，GTK 原生句柄不能伪装 HWND；在 portable 分母中按逐成员审核记录。 | Windows 专属契约，AppKit 的 target-action/delegate 属内部实现，不是 NativeWindow 等价实现。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.nativewindow?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/class.Widget.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nsresponder)
