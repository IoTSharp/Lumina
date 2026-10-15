# MdiClient

⬜ **现状：**无 MdiClient。现有 Form 也缺 MdiParent/IsMdiContainer 的完整模型；owned 窗口不构成 WinForms MDI。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/MdiClient.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：同名标准类型尚未实现，无可冒充为完整契约的源码。
- 排序：Form/Control、菜单合并、子窗体活动与消息路由；Windows P2 高级兼容。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P1 / MdiClient-01 | 实现标准 MdiClient : Control、MdiChildren、LayoutMdi(MdiLayout)、标准 ControlCollection 类型限制与父 Form 的 IsMdiContainer 关系。 |
| ⬜ | P1 / MdiClient-02 | Form.MdiParent/ActiveMdiChild/MdiChildren/MdiChildActivate 与 MdiClient 子集合双向一致，修改 Parent/TopLevel 时的异常和解绑遵循基线。 |
| ⬜ | P2 / MdiClient-03 | 支持 Cascade/TileHorizontal/TileVertical/ArrangeIcons、子窗最小化/最大化/还原、激活切换、滚动区域与窗口菜单合并。 |
| ⬜ | P2 / MdiClient-04 | 校准子窗 FormClosing/FormClosed 与父窗关闭的取消链、CloseReason、焦点/键盘导航、Ctrl+F4/Ctrl+Tab，关闭子窗不能结束主消息循环。 |
| ⬜ | P2 / MdiClient-05 | 验证 BackgroundImage/BackgroundImageLayout、BackColor、DPI、RTL、布局/绘制/可访问性和设计器生成 MDI 窗体代码。 |
| ⬜ | P2 / MdiClient-06 | Windows 使用 MDICLIENT 子窗口、DefFrameProc/DefMDIChildProc、WM_MDICREATE/MDIACTIVATE/MDITILE/MDICASCADE 等标准消息，不另造多文档框架。 |
| ⬜ | P2 / MdiClient-07 | Linux 🚫 GTK4 无系统 MDI 子窗口控件；GtkNotebook/owned GtkWindow 只能提供其他交互，不作为本类型全量等价实现，也不以自绘子窗冒充原生 MDI。 |
| ⬜ | P3 / MdiClient-08 | macOS 🚫 AppKit 原生 NSWindow/标签窗口无 WinForms MDICLIENT 等价；是否接受独立窗口/标签需要产品边界，未达成前保持本类型平台不可等价。 |
| ⬜ | 验收 / MdiClient-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| MDICLIENT 原生窗口 + 原生 MDI 子 HWND/菜单合并。 | 无完整 native MDI 对应；普通窗口和页签是不同语义，明确 🚫。 | 无完整 native MDI 对应；AppKit 多窗口不冒称原生 MDI。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.mdiclient?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/class.Window.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nswindow)
