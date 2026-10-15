# Form

🟡 **现状：**Form 独立实现 IDisposable，未继承 ContainerControl/Control。已有真实顶层 HWND 与 Show/Close/Load/Shown、部分缩放；批次 003 提供 ISynchronizeInvoke/InvokeRequired/同步投递及 BeginInvoke/EndInvoke，批次 004 共用库内队列实现 Action/Func<T> 的标准 InvokeAsync 与取消。Layout 使用 EventHandler、FormClosing 使用泛型 EventHandler，标准继承、FormClosed/ShowDialog、ValueTask/同步上下文等仍待实现。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/Form.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：[Form.cs](../../../Lumina.Forms/Form.cs)
- 排序：Control→ScrollableControl→ContainerControl、Application/Context/同步上下文；所有业务窗口的 P0。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P0 / Form-01 | 建立标准 Form : ContainerControl 继承，使 Form 可作 Control/IWin32Window/Component 使用；Controls 返回标准 ControlCollection，直接子控件 Parent 正确，删除重复的两套基础状态。 |
| ⬜ | P0 / Form-02 | 修正 FormClosingEventHandler/FormClosedEventHandler、LayoutEventHandler 等准确类型和事件参数；Created 等历史自有事件不能抵扣 HandleCreated；旧 Closing/Closed 仍按基线保留兼容。 |
| ⬜ | P0 / Form-03 | 实现 ShowDialog()/ShowDialog(IWin32Window)、Show(owner)、DialogResult、Modal；owner 禁用恢复、嵌套消息循环、Esc/Enter、取消关闭与对话框复用完整验收。 |
| 🟡 | P0 / Form-04 | 批次 003/004 已在现有独立 Form 复用内部投递实现，同步/BeginInvoke/EndInvoke、ISynchronizeInvoke 与 Action/Func<T> 的 InvokeAsync/取消可消费；标准继承、ValueTask/同步上下文、完整关闭竞态未关闭。 |
| ⬜ | P1 / Form-05 | 校准默认 Text/StartPosition/Size/ClientSize/AutoScaleMode 等；当前 Lumina Native Form/Manual/960×640 不是标准默认，Designer 不应被迫重复赋值抵消库默认。 |
| ⬜ | P1 / Form-06 | 实现显示生命周期：HandleCreated、BindingContextChanged、Load、VisibleChanged、Activated、Shown 真实顺序；Hide→Show 不重复 Load/Shown；Visible/WindowState/Handle 同步。 |
| ⬜ | P1 / Form-07 | 实现 Close/FormClosing.Cancel、CloseReason、FormClosed、Dispose；用户关闭、owner/MDI 关闭、Application.Exit、系统关机的事件原因与取消作用域不同，不能统一 UserClosing。 |
| ⬜ | P1 / Form-08 | 补 Owner/OwnedForms/AddOwnedForm/RemoveOwnedForm、TopLevel/TopMost/ShowInTaskbar/ShowIcon、ActiveForm/Focused、BringToFront/Activate；关闭 owner 的从属窗口与焦点恢复对照。 |
| ⬜ | P1 / Form-09 | 实现 FormBorderStyle、ControlBox/MinimizeBox/MaximizeBox/HelpButton、SizeGripStyle、StartPosition/Location/RestoreBounds/DesktopBounds 与屏幕工作区；重建句柄不丢状态。 |
| ⬜ | P1 / Form-10 | 完整 Dock/Anchor/AutoSize/AutoSizeMode、MinimumSize/MaximumSize、ClientSize/AutoScroll、MenuStrip/StatusStrip 与嵌套容器布局；SuspendLayout 必须实际延迟。 |
| ⬜ | P1 / Form-11 | 实现 AutoScaleDimensions/CurrentAutoScaleDimensions/PerformAutoScale、Font/Dpi/Inherit/None、DpiChanged 及父子前后通知；跨 96/144/192 DPI 往返和 .resx 基准不重复缩放。 |
| ⬜ | P1 / Form-12 | 实现 AcceptButton/CancelButton/IButtonControl、KeyPreview、ProcessDialogKey/ProcessCmdKey、输入语言/IME、AutoValidate 和深层 ActiveControl；验证取消时禁止错误关闭对话框。 |
| ⬜ | P1 / Form-13 | 补 MainMenuStrip、IsMdiContainer/MdiParent/MdiChildren/ActiveMdiChild/LayoutMdi/MdiChildActivate；与 MdiClient 一起验收，不能以普通 owned windows 冒充 MDI。 |
| ⬜ | P2 / Form-14 | 实现 Opacity/TransparencyKey/Region/RightToLeftLayout/AllowTransparency 与 .NET10 标题/边框颜色相关 API；平台差异逐条披露，不能删除普通颜色/图像契约。 |
| ⬜ | P1 / Form-15 | NativeFormsDemo .Designer.cs/.resx 静态布局、资源、本地化与继承 Form，可在 VS 双向保存；AOT 主窗可见、业务操作、模态、正常退出为发布验收。 |
| ⬜ | P1 / Form-16 | Windows 复用现有注册窗口类与 USER32，补 WM_CLOSE/QUERYENDSESSION/SIZE/DPICHANGED、owner 和 dialog loop；保留原生非客户区/输入，不先重做窗口装饰。 |
| ⬜ | P2 / Form-17 | Linux 使用 GtkWindow/GtkApplicationWindow；close-request/activate/state 适配标准生命周期，模态用 transient-for/modal + 同一 GMainContext。Wayland 任意 Location/全局坐标单列限制。 |
| ⬜ | P3 / Form-18 | macOS 使用 NSWindow/NSPanel + delegate，NSApplication 主线程；窗口式模态与 sheet 依调用语义选择，firstResponder/close veto 对齐，AppKit 不能原生完整等价 MDI。 |
| ⬜ | 验收 / Form-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| 已有注册顶层 HWND；优先修标准继承、事件与模态，主题/特效保留为可选，不支配启动/关闭。 | GtkWindow/GtkApplicationWindow 原生窗口；compositor 决定部分位置/装饰能力，移动窗口/DPI 与坐标契约实机验收。 | NSWindow/NSPanel 原生窗口；owner、sheet、全屏和应用级菜单按语义映射，明确 MDI/Windows 消息不可等价。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- `b83f534`（2026-04-09）：加入 Form.Invoke/BeginInvoke，当前实现仍需修正线程语义。
- `70a08e7`（2026-04-09）：增强布局/释放状态兼容。
- `2fb513a`（2026-04-09）：异步调用和布局改进涉及窗体。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。
- 2026-10-15 · 批次 001：在首个原生窗口创建前通知 Application 的永久 DPI 设置门禁，覆盖原生建窗回调期间的晚调用；未改变继承链、布局或关闭行为。实际建窗/DPI 交互验收后置。
- 2026-10-16 · 批次 003：真实顶层 HWND 创建时建立库内投递队列，WindowProc 分发注册消息、Dispose/WM_NCDESTROY 结束队列；同步/异步 Delegate、args、Action 和同步 Func 重载及接口已接入。指定线程探针在 WinForms/JIT/AOT 三端相同；Form 仍未继承 Control，InvokeAsync、所有关闭竞态和完整生命周期另列待办。
- 2026-10-16 · 批次 004：因标准继承链尚缺，在 Form 增加同名 Action/Func<T> 的 InvokeAsync(CancellationToken) 入口，复用 WindowThreadDispatcher；UI/工作线程及结果/释放后的指定观测纳入 59 条三端对照。没有改变 Form 继承链；ValueTask、标准同步上下文和完整关闭/生命周期仍保留。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.form?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/class.Window.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nswindow)
