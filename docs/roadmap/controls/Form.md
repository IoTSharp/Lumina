# Form

🟡 **现状：**018标准继承/父子树、019句柄及显示生命周期、020/021标准事务和基础布局保留；022复用Control独占HFONT与有效颜色/theme默认分离。023完成标准消息/绘制及销毁清理首范围，原生类过程和早期创建关联保留。历史Created事件仍隐藏Control.Created属性，FormClosing仍为泛型EventHandler；FormClosed/ShowDialog、完整默认/缩放/显示关闭、多UI退出与Designer仍缺，不能由基础衔接关闭整个Form。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/Form.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：[Form.cs](../../../Lumina.Forms/Form.cs)
- 排序：Control→ScrollableControl→ContainerControl、Application/Context/同步上下文；所有业务窗口的 P0。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ✅ | P0 / Form-01 | 批次018：建立Form : ContainerControl : ScrollableControl : Control : Component，实际共用Text/Name/Bounds/ClientSize/Handle/Controls/颜色/ISynchronizeInvoke/Dispose；直接子项Parent为Form，移除重复状态与集合。真实窗口移动/尺寸回写及父子释放有运行证据，见018。整Form、容器行为与平台兼容未关闭。 |
| 🟡 | P0 / Form-02 | 020已移除历史EventHandler Layout，继承Control标准LayoutEventHandler/LayoutEventArgs及OnLayout(LayoutEventArgs)。FormClosingEventHandler/FormClosedEventHandler、旧Closing/Closed仍待对齐；Created历史自有事件不能抵扣标准属性/HandleCreated。 |
| ⬜ | P0 / Form-03 | 实现 ShowDialog()/ShowDialog(IWin32Window)、Show(owner)、DialogResult、Modal；owner 禁用恢复、嵌套消息循环、Esc/Enter、取消关闭与对话框复用完整验收。 |
| 🟡 | P0 / Form-04 | 003～007的真实投递/context/await恢复已于018合入Control标准继承路径；019保留句柄重建前已排队回调，直接DestroyHandle/外部销毁不等于Close、不退出消息循环。完整关闭/重建/退出与多UI/模态竞态仍缺。 |
| 🟡 | P1 / Form-05 | 018已校准空Text、300×300默认Size、TabStop=true和初始Visible=false，并消费共享客户区。StartPosition、完整ClientSize/AutoScaleMode等默认与派生元数据仍待对照，父任务不关闭。 |
| 🟡 | P1 / Form-06 | 019完成06a与Control标准句柄路径的必要衔接；BindingContextChanged/Activated、完整Visible/WindowState/Handle联动、所有显示入口及标准事件委托仍待对齐。历史Created事件隐藏Control.Created是已知表面差异，不计为标准属性兼容。 |
| ✅ | P1 / Form-06a | 批次019：protected CreateHandle、延迟Handle、首次Show经OnCreateControl发Load并排队Shown，重复Show/隐藏/重建不重复Load/Shown；直接/外部销毁不触发Close语义，已显示窗体重建恢复真实原生可见性并保留Created/子控件状态，字体/菜单/GCHandle随真实窗口寿命清理。指定五个Form场景纳入019的204行参考/JIT/实际AOT对照，14项专项回归含创建参数失败重试后的真实GC回收；完整显示/关闭/模态及所有资源异常仍见父项。 |
| ⬜ | P1 / Form-07 | 实现 Close/FormClosing.Cancel、CloseReason、FormClosed、Dispose；用户关闭、owner/MDI 关闭、Application.Exit、系统关机的事件原因与取消作用域不同，不能统一 UserClosing。 |
| ⬜ | P1 / Form-08 | 补 Owner/OwnedForms/AddOwnedForm/RemoveOwnedForm、TopLevel/TopMost/ShowInTaskbar/ShowIcon、ActiveForm/Focused、BringToFront/Activate；关闭 owner 的从属窗口与焦点恢复对照。 |
| ⬜ | P1 / Form-09 | 实现 FormBorderStyle、ControlBox/MinimizeBox/MaximizeBox/HelpButton、SizeGripStyle、StartPosition/Location/RestoreBounds/DesktopBounds 与屏幕工作区；重建句柄不丢状态。 |
| 🟡 | P1 / Form-10 | 020事务10a、021默认布局10b完成；真实父级Dock替代历史条带专用定位。完整Form AutoSize/AutoSizeMode、客户区/MinMax、AutoScroll、全部嵌套测量/DPI/RTL与Designer仍缺。 |
| ✅ | P1 / Form-10a | 批次020完成共享暂停/恢复/OnLayout入口，移除重复WM_SIZE布局和无条件子树遍历；当时的直属条带专用定位于021被父级DefaultLayout替代。历史事务范围完成保留，几何见10b。 |
| ✅ | P1 / Form-10b | 批次021：Form继承默认布局/ContainerControl测量，菜单/工具/状态条按实际Controls反序Dock；移除旧专用定位和Control原生Y坐标额外菜单偏移。隐藏/显示、嵌套/取消Dock及实际HWND矩形有固定参考，原Demo源码不改；完整Form布局和Designer仍归父项。 |
| 🟡 | P1 / Form-11 | 历史AutoScaleDimensions/PerformAutoScale局部算法已于018迁入ContainerControl，原全待办遗漏继承实现，现更正；022继承基础Font链但未完成字体自动缩放。完整CurrentAutoScaleDimensions、Font/Dpi/Inherit/None、DpiChanged父子通知与96/144/192往返和.resx基准仍待对照。 |
| ⬜ | P1 / Form-12 | 实现 AcceptButton/CancelButton/IButtonControl、KeyPreview、ProcessDialogKey/ProcessCmdKey、输入语言/IME、AutoValidate 和深层 ActiveControl；验证取消时禁止错误关闭对话框。 |
| ⬜ | P1 / Form-13 | 补 MainMenuStrip、IsMdiContainer/MdiParent/MdiChildren/ActiveMdiChild/LayoutMdi/MdiChildActivate；与 MdiClient 一起验收，不能以普通 owned windows 冒充 MDI。 |
| ⬜ | P2 / Form-14 | 实现 Opacity/TransparencyKey/Region/RightToLeftLayout/AllowTransparency 与 .NET10 标题/边框颜色相关 API；平台差异逐条披露，不能删除普通颜色/图像契约。 |
| ⬜ | P1 / Form-15 | NativeFormsDemo .Designer.cs/.resx 静态布局、资源、本地化与继承 Form，可在 VS 双向保存；AOT 主窗可见、业务操作、模态、正常退出为发布验收。 |
| 🟡 | P1 / Form-16 | 已有注册窗口类、USER32过程、真实显示/销毁/重建；022独占HFONT保留，023完成16a标准消息与绘制必要衔接。完整QUERYENDSESSION/DPICHANGED、owner/dialog loop及关闭竞态仍缺，保留原生非客户区/输入。 |
| ✅ | P1 / Form-16a | 批次023：Form.CreateParams及标准WndProc/ref Message.Result接真实创建/重建；默认绘制进入Control标准入口、DefWndProc保留原生类过程；外层销毁finally清映射/GCHandle/画刷，不依赖派生WndProc调用base。创建参数、虚消息覆盖、Paint及截获/抛销毁消息有指定回归；Param的AOT限制同Control-17a，完整Form生命周期/绘制/关闭仍属父项。 |
| ⬜ | P2 / Form-17 | Linux 使用 GtkWindow/GtkApplicationWindow；close-request/activate/state 适配标准生命周期，模态用 transient-for/modal + 同一 GMainContext。Wayland 任意 Location/全局坐标单列限制。 |
| ⬜ | P3 / Form-18 | macOS 使用 NSWindow/NSPanel + delegate，NSApplication 主线程；窗口式模态与 sheet 依调用语义选择，firstResponder/close veto 对齐，AppKit 不能原生完整等价 MDI。 |
| ⬜ | 验收 / Form-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| 已有注册顶层 HWND；优先修标准继承、事件与模态，主题/特效保留为可选，不支配启动/关闭。 | GtkWindow/GtkApplicationWindow 原生窗口；compositor 决定部分位置/装饰能力，移动窗口/DPI 与坐标契约实机验收。 | NSWindow/NSPanel 原生窗口；owner、sheet、全屏和应用级菜单按语义映射，明确 MDI/Windows 消息不可等价。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- 2026-10-16 · 批次023：标准CreateParams/Message/WndProc接入Control共享入口，早期消息绑定和销毁清理移入原生callback异常边界；修复Form绘制绕过Control及派生截获NCDESTROY漏清理。06a历史限定范围保留，16a为本批必要衔接，完整Form/资源/模态/Designer仍未关闭，见[023](../batches/023.md)。

- 2026-10-16 · 批次022：移除Form共享UiFontHandle，创建/销毁复用Control-08b独占HFONT，避免重建失败时停放子窗口引用已释放父字体。有效颜色与局部/theme默认分开，显式默认色与Reset即便不发属性事件也更新palette画刷。补实际资源寿命回归，完整Font/DPI缩放仍缺；纠正Form-11遗漏018继承缩放、Form-16遗漏已有Win32实现的全待办状态。完整资源、模态、Designer及平台验收不关闭，见[022](../batches/022.md)。

- 2026-10-16 · 批次021：移除直属条带专用定位及旧GetClientContentTopInset，由标准父级DefaultLayout统一布局；ContainerControl首选测量供Form消费。更新020历史定位描述以免把旧算法当当前行为；Created事件仍隐藏Control.Created，完整模态/资源/设计器未关闭。

- 2026-10-16 · 批次020：标准Layout移到Control并统一事务入口，保留直接子条带定位、移除WM_SIZE重复布局。历史Form.Layout的EventHandler类型和protected无参OnLayout不再保留，消费者改用LayoutEventHandler和OnLayout(LayoutEventArgs)；仓库无旧覆盖消费者，固定Demo源码未变。Form-02/10按实际部分回标，10a为完成的必要衔接；完整Form/几何/Designer不关闭。

- 2026-10-16 · 批次019：Form覆盖标准protected CreateHandle并复用Control映射/事件/dispatcher。创建恢复真实ShowWindow时仅抑制内部恢复消息的重复托管通知，外部销毁后Handle重读保持Created=false及子窗未创建；直接销毁/重建不再冒充Close。Shown经UI队列触发；历史Created事件为兼容保留并明确其隐藏属性的差异。Form-06由全部未开始纠正为部分完成，06a为本批明确子范围；Form-02/03/07及完整窗口/设计器/资源/跨平台不关闭，见[019](../batches/019.md)。

- 2026-10-16 · 批次018：Form由独立IDisposable类改为标准ContainerControl/ScrollableControl/Control/Component链，删除重复的属性、句柄/投递、集合和释放状态；保留真实顶层HWND、主题、菜单及原始Demo源码。原生移动/尺寸写回共享Bounds/ClientSize；Dispose清活动控件，销毁已显示窗口时静默清可见态。默认Text/Size/TabStop/Visible按参考校准，历史“Form独立状态/不存在标准链”回标。完整Form事件委托/模态/默认/布局/缩放/Designer/平台仍缺，见[018](../batches/018.md)。

- `b83f534`（2026-04-09）：加入 Form.Invoke/BeginInvoke，当前实现仍需修正线程语义。
- `70a08e7`（2026-04-09）：增强布局/释放状态兼容。
- `2fb513a`（2026-04-09）：异步调用和布局改进涉及窗体。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。
- 2026-10-15 · 批次 001：在首个原生窗口创建前通知 Application 的永久 DPI 设置门禁，覆盖原生建窗回调期间的晚调用；未改变继承链、布局或关闭行为。实际建窗/DPI 交互验收后置。
- 2026-10-16 · 批次 003：真实顶层 HWND 创建时建立库内投递队列，WindowProc 分发注册消息、Dispose/WM_NCDESTROY 结束队列；同步/异步 Delegate、args、Action 和同步 Func 重载及接口已接入。指定线程探针在 WinForms/JIT/AOT 三端相同；Form 仍未继承 Control，InvokeAsync、所有关闭竞态和完整生命周期另列待办。
- 2026-10-16 · 批次 004：因标准继承链尚缺，在 Form 增加同名 Action/Func<T> 的 InvokeAsync(CancellationToken) 入口，复用 WindowThreadDispatcher；UI/工作线程及结果/释放后的指定观测纳入 59 条三端对照。没有改变 Form 继承链；ValueTask、标准同步上下文和完整关闭/生命周期仍保留。

- 2026-10-16 · 批次 005：增加标准 Func<CancellationToken,ValueTask> / ValueTask<T> 同名入口，复用 WindowThreadDispatcher；UI 首次调用、返回值与 async token lambda 纳入 90 条参考/JIT/AOT 观测。未改变独立 Form 的继承链；同步上下文与 callback 内 await 后 UI 恢复、全部关闭竞态继续待后续。

- 2026-10-16 · 批次 007：Form 构造接入默认 context，现有 Application.Run 在 Show 前安装并于最外层 finally 还原；默认 await/工作线程投递有对照。Form 仍独立于 Control，最后窗体退出判断仍使用进程全局 OpenForms 计数，多 UI 线程与 ApplicationContext 退出语义继续缺失；不把当前子范围写成完整 Form 生命周期兼容，见 [批次 007](../batches/007.md)。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.form?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/class.Window.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nswindow)
