# Form

本页任务表只列未完成范围；代码完成子项标记在[完成状态](#已完成任务)，任务 ID 与验收状态均不因整理改变。剩余重点是完整关闭事件/原因、关闭焦点与 WM_QUIT 差异、模态/Owner/MDI、多 UI 线程退出及设计器。

[完整 API 与继承基线](../api/Form.md) · [任务索引](../README.md) · [依赖顺序](../execution.md) · [验证门槛](../validation.md) · [平台要求](../platforms.md)

固定基线为 .NET 10.0.11。Windows、Linux、macOS 分别验收，当前没有完整逐控件 98% 结论。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| 🟡 | P0 / Form-02 | 020已移除历史EventHandler Layout，继承Control标准LayoutEventHandler/LayoutEventArgs及OnLayout(LayoutEventArgs)。FormClosingEventHandler/FormClosedEventHandler、旧Closing/Closed仍待对齐；Created历史自有事件不能抵扣标准属性/HandleCreated。 |
| 🟡 | P0 / Form-03 | 已实现ShowDialog()/ShowDialog(IWin32Window)、DialogResult结束模态、Modal、独立嵌套泵、可取消关闭和对话框复用；恢复owner/同线程已登记窗口启用状态。Show(owner)、完整外部宿主与新增窗口模态策略及实际运行仍待。 |
| 🟡 | P0 / Form-04 | 003～007的真实投递/context/await恢复已于018合入Control标准继承路径；019保留句柄重建前已排队回调，直接DestroyHandle/外部销毁不等于Close、不退出消息循环。完整关闭/重建/退出与多UI/模态竞态仍缺。 |
| 🟡 | P1 / Form-05 | 018已校准空Text、300×300默认Size、TabStop=true和初始Visible=false，并消费共享客户区。StartPosition、完整ClientSize/AutoScaleMode等默认与派生元数据仍待对照，父任务不关闭。 |
| 🟡 | P1 / Form-06 | 019完成06a与Control标准句柄路径的必要衔接；BindingContextChanged/Activated、完整Visible/WindowState/Handle联动、所有显示入口及标准事件委托仍待对齐。历史Created事件隐藏Control.Created是已知表面差异，不计为标准属性兼容。 |
| 🟡 | P1 / Form-07 | 已有Close/WM_CLOSE及验证取消，新增标准FormClosed参数/委托、CloseReason 0～6、Application.Exit预检和普通关闭不再全局PostQuit。历史LostFocus及WM_QUIT严格差分未重验；owner/MDI/关机与极端异常/重入事务仍待。旧CloseReason整数及已编译常量需复核。 |
| 🟡 | P1 / Form-08 | 已继承Focused，原Activate/BringToFront调用SetForegroundWindow；原全待办遗漏这些局部实现，028按实际代码纠正并完成08a。Owner/OwnedForms/AddOwnedForm/RemoveOwnedForm、TopLevel/TopMost/ShowInTaskbar/ShowIcon、ActiveForm、Activated/Deactivate、完整激活/Z序/MDI及关闭owner焦点恢复仍缺。 |
| ⬜ | P1 / Form-09 | 实现 FormBorderStyle、ControlBox/MinimizeBox/MaximizeBox/HelpButton、SizeGripStyle、StartPosition/Location/RestoreBounds/DesktopBounds 与屏幕工作区；重建句柄不丢状态。 |
| 🟡 | P1 / Form-10 | 020事务10a、021默认布局10b完成；真实父级Dock替代历史条带专用定位。完整Form AutoSize/AutoSizeMode、客户区/MinMax、AutoScroll、全部嵌套测量/DPI/RTL与Designer仍缺。 |
| 🟡 | P1 / Form-11 | 历史AutoScaleDimensions/PerformAutoScale局部算法已于018迁入ContainerControl，原全待办遗漏继承实现，现更正；022继承基础Font链但未完成字体自动缩放。完整CurrentAutoScaleDimensions、Font/Dpi/Inherit/None、DpiChanged父子通知与96/144/192往返和.resx基准仍待对照。 |
| 🟡 | P1 / Form-12 | 018～036活动链/验证、KeyPreview、默认取消按钮和释放范围保留；040的12d补输入语言事件/请求取消。验证自身override及Browsable元数据、完整命令/模态对话框、IME组合态/候选窗/OLE及全部原生输入与点击提交仍缺。 |
| ⬜ | P1 / Form-13 | 补 MainMenuStrip、IsMdiContainer/MdiParent/MdiChildren/ActiveMdiChild/LayoutMdi/MdiChildActivate；与 MdiClient 一起验收，不能以普通 owned windows 冒充 MDI。 |
| ⬜ | P2 / Form-14 | 实现 Opacity/TransparencyKey/Region/RightToLeftLayout/AllowTransparency 与 .NET10 标题/边框颜色相关 API；平台差异逐条披露，不能删除普通颜色/图像契约。 |
| ⬜ | P1 / Form-15 | NativeFormsDemo .Designer.cs/.resx 静态布局、资源、本地化与继承 Form，可在 VS 双向保存；AOT 主窗可见、业务操作、模态、正常退出为发布验收。 |
| 🟡 | P1 / Form-16 | 已有注册窗口类、USER32过程、真实显示/销毁/重建；022独占HFONT保留，023完成16a标准消息与绘制必要衔接。完整QUERYENDSESSION/DPICHANGED、owner/dialog loop及关闭竞态仍缺，保留原生非客户区/输入。 |
| ⬜ | P2 / Form-17 | Linux 使用 GtkWindow/GtkApplicationWindow；close-request/activate/state 适配标准生命周期，模态用 transient-for/modal + 同一 GMainContext。Wayland 任意 Location/全局坐标单列限制。 |
| ⬜ | P3 / Form-18 | macOS 使用 NSWindow/NSPanel + delegate，NSApplication 主线程；窗口式模态与 sheet 依调用语义选择，firstResponder/close veto 对齐，AppKit 不能原生完整等价 MDI。 |
| ⬜ | 验收 / Form-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| 已有注册顶层 HWND；优先修标准继承、事件与模态，主题/特效保留为可选，不支配启动/关闭。 | GtkWindow/GtkApplicationWindow 原生窗口；compositor 决定部分位置/装饰能力，移动窗口/DPI 与坐标契约实机验收。 | NSWindow/NSPanel 原生窗口；owner、sheet、全屏和应用级菜单按语义映射，明确 MDI/Windows 消息不可等价。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 已完成任务

✅ 表示该行定义范围代码完成；父项的剩余代码与平台验收仍按当前任务表记录。

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ✅ | P1 / Form-12d | 批次040：标准InputLanguageChanging/Changed事件、两组构造参数/委托、组件事件槽/CanRaiseEvents与protected virtual On*；Control后代消息经FindForm转发，变化通知位于IME更新后/默认过程前，请求可取消且返回0。14项事件/消息回归通过，限定公共通知与消息连接，不含完整模态/OLE/实际输入法验收，见040。 |
| ✅ | P1 / Form-12b | 批次035：AcceptButton/CancelButton/IButtonControl、合法DialogResult、最深活动按钮默认通知及无Ctrl/Alt的Enter/Escape路由；Button先写结果再Click并消费CanSelect/验证策略。18项新按钮回归及1项实际队列集成通过。属性保存结果不等于实现ShowDialog/Modal；ButtonBase、全部生命周期/原生点击、IME/设备仍待。 |
| ✅ | P1 / Form-12c | 批次036：托管Dispose(true)先清理_defaultButton、AcceptButton、CancelButton并对原默认按钮发送NotifyDefault(false)；Dispose(false)不改变托管按钮状态，保留AcceptButton允许孤立引用。2项生命周期回归通过；模态、完整关闭事件/原因、多UI和设计器仍待。 |
| ✅ | P1 / Form-12a | 批次034：KeyPreview默认false，启用后在子项之前触发窗体键事件、消费或按父链回退；ProcessTabKey在顶层正反回绕。3项回归覆盖嵌套字符修改、Handled和真实可见窗口程序导航；完整默认/取消按钮、IME/物理输入及消息循环预处理仍归12父项。 |
| ✅ | P0 / Form-01 | 批次018：建立Form : ContainerControl : ScrollableControl : Control : Component，实际共用Text/Name/Bounds/ClientSize/Handle/Controls/颜色/ISynchronizeInvoke/Dispose；直接子项Parent为Form，移除重复状态与集合。真实窗口移动/尺寸回写及父子释放有运行证据，见018。整Form、容器行为与平台兼容未关闭。 |
| ✅ | P1 / Form-06a | 批次019：protected CreateHandle、延迟Handle、首次Show经OnCreateControl发Load并排队Shown，重复Show/隐藏/重建不重复Load/Shown；直接/外部销毁不触发Close语义，已显示窗体重建恢复真实原生可见性并保留Created/子控件状态，字体/菜单/GCHandle随真实窗口寿命清理。指定五个Form场景纳入019的204行参考/JIT/实际AOT对照，14项专项回归含创建参数失败重试后的真实GC回收；完整显示/关闭/模态及所有资源异常仍见父项。 |
| ✅ | P1 / Form-07a | 批次029：WM_CLOSE以Validate(true)结果设置FormClosingEventArgs.Cancel，再调用FormClosing允许处理器更改；无句柄Close直接Dispose，不触发验证。继承ContainerControl-03a基础验证策略；模态/完整CloseReason和关闭焦点事件差异不在此完成范围。 |
| ✅ | P1 / Form-08a | 批次028：Form.CanSelectCore、protected Select(bool,bool)、SetActiveWindow及WM_ACTIVATE必要衔接；激活时选择首可用控件并恢复最深ContainerControl活动项，销毁后清激活状态。外部销毁后读取Handle以SW_SHOWNOACTIVATE恢复可见性，不提前创建子HWND；后续CreateControl/真实激活顺序有固定参考及旧204行回归。完整激活事件/owner/MDI不在此范围。 |
| ✅ | P1 / Form-10a | 批次020完成共享暂停/恢复/OnLayout入口，移除重复WM_SIZE布局和无条件子树遍历；当时的直属条带专用定位于021被父级DefaultLayout替代。历史事务范围完成保留，几何见10b。 |
| ✅ | P1 / Form-10b | 批次021：Form继承默认布局/ContainerControl测量，菜单/工具/状态条按实际Controls反序Dock；移除旧专用定位和Control原生Y坐标额外菜单偏移。隐藏/显示、嵌套/取消Dock及实际HWND矩形有固定参考，原Demo源码不改；完整Form布局和Designer仍归父项。 |
| ✅ | P1 / Form-16a | 批次023：Form.CreateParams及标准WndProc/ref Message.Result接真实创建/重建；默认绘制进入Control标准入口、DefWndProc保留原生类过程；外层销毁finally清映射/GCHandle/画刷，不依赖派生WndProc调用base。创建参数、虚消息覆盖、Paint及截获/抛销毁消息有指定回归；Param的AOT限制同Control-17a，完整Form生命周期/绘制/关闭仍属父项。 |
| ✅ | P1 / Form-16b | 批次024必要背景绘制衔接：默认WindowExStyle移除WS_EX_COMPOSITED，保留虚扩展供派生显式选择。真实父失效矩形12,10,8,6与参考一致，STATIC原生类测试去除旧样式补丁仍通过；Demo实际AOT显示/交互/正常关闭有证据。完整Form绘制/缓冲/关闭异常仍属16父项。 |
| ✅ | P1 / Form-16c | 批次025：Form native窗口过程捕获回调异常后继续逐项释放菜单、palette、句柄、GCHandle和OpenForms注册；清理异常独立报告且不覆盖原回调异常。外部销毁双错场景和实际AOT边界探针通过；完整Close/FormClosing/FormClosed、模态、Dispose事务和多UI退出仍归父项。 |

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.form?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/class.Window.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nswindow)
