# Application

053必要依赖：成功SetHighDpiMode后刷新无DC标准文本测量的初始系统DPI快照；失败、非法值和已有窗口拒绝路径保持原结果。该内部连接归CNS-006a，不新增Application完成任务；全量5,329回归通过，完整DPI/线程/平台验收仍待，见[053](../batches/053.md)。

🟡 **现状：**已有消息循环、样式activation context和open form跟踪；040线程HKL服务保留，041补OleRequired线程缓存、输入语言OLE前置与显式/最外层退出配对。Run()/Run(ApplicationContext)、完整消息过滤/退出与OLE宿主context、CurrentCulture/UseWaitCursor、标准异常对话框与异常模式仍缺。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/Application.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：[Application.cs](../../../Lumina.Forms/Application.cs)
- 排序：内部窗口/UI 调度、Form、ApplicationContext/WindowsFormsSynchronizationContext；启动 P0。内部投递窗口不要求先完成 M4 的 NativeWindow 全量公共扩展。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| 🟡 | P0 / Application-01 | Run(Form mainForm) 的 void/命名参数/null 主窗路径已修正；Run()/Run(ApplicationContext)、拒绝非法嵌套 Run、模态与主循环分别管理、主窗与最后窗口退出区分仍待实现。028同源探针发现Run外最后窗体Close也触发PostQuitMessage，11个场景多出WM_QUIT；029又有15场景记录该差异；必须按循环/线程/主窗所有权修正，不能过滤输出或视为无害噪声。 |
| ✅ | P0 / Application-01a | 批次 001：Run(Form mainForm) 返回 void，支持相同命名参数调用；null 主窗沿消息循环执行，移除不符基线的空值异常。无界面签名检查和双目标编译通过，消息循环生命周期属于父任务待办。 |
| ✅ | P0 / Application-02 | SetHighDpiMode 返回真实系统 BOOL/HRESULT 的成功结果，校验枚举，按 OS 能力回退；HighDpiMode 读取当前线程实际 awareness。首次窗口/托盘窗口创建前永久关闭设置入口，manifest/宿主/重复调用失败不假报成功。14 组隔离进程对照通过；旧系统回退、建窗回调、跨屏及真实缩放验收后置。 |
| 🟡 | P0 / Application-03 | 批次 007 完成现有 Run/RunMessageLoop 与控件/窗体构造的默认 context 安装/还原、默认 callback await UI 恢复；完整 ApplicationContext/模态、多 UI 线程与退出取消/竞态仍缺。 |
| 🟡 | P1 / Application-04 | 批次007完成无Form循环的ExitThread请求/最外层activation释放；041的12a复用现有退出路径配对本库OLE引用。Exit()/Exit(CancelEventArgs)、ApplicationExit/ThreadExit、FormClosing取消、ApplicationContext/多个UI线程的完整退出与竞态仍待，原007两个STA固定参考/JIT/AOT证据保留。 |
| ⬜ | P1 / Application-05 | 实现 OpenForms 的标准 FormCollection、可见/隐藏/释放窗口登记、索引与名称查询；不得直接暴露内部 HashSet 改变枚举契约。 |
| 🟡 | P1 / Application-06 | 035的06a完成键盘队列Control预处理首范围；AddMessageFilter/RemoveMessageFilter/FilterMessage/IMessageFilter、完整快捷键/IsDialogMessage及WM_QUIT/GetMessage=-1处理仍待，不能由局部接入关闭父任务。 |
| ✅ | P1 / Application-06a | 批次035/036：GetMessage检索的键盘消息按FromChildHandle找到托管控件，Translate/Dispatch前实际调用PreProcessMessage；消费停止，未消费保留修改的目标/消息/参数，native时空字段保持。035异常报告一次并停止故障消息，036在Control中补队列字符抑制。11项真实队列/直接发送边界回归通过。过滤器、原生判别、模态及完整退出归父项。 |
| ⬜ | P1 / Application-07 | 实现 Idle/RaiseIdle、DoEvents、EnterThreadModal/LeaveThreadModal、MessageLoop/AllowQuit；DoEvents 的重入遵循标准，不用 Task.Run 代替消息泵。 |
| 🟡 | P1 / Application-08 | 批次 003 完成线程局部 ThreadException/OnThreadException 与异步投递错误报告（Application-08a）；SetUnhandledExceptionMode、标准 ThreadExceptionDialog、其余消息回调的完整异常策略仍待实现。 |
| ✅ | P1 / Application-08a | 批次 003：线程局部订阅、当前 Thread sender、原始异常对象、重入保护和处理器异常后的状态恢复；4 项托管回归及实际 UI 投递事件观测通过。无处理器的 Windows 原生 OK 提示保留可见/继续行为，完整 Quit/Details UI 后置；投递事件处理器再抛在 native 边界前显式终止，区别于 public OnThreadException 的直接传播。 |
| ⬜ | P1 / Application-09 | EnableVisualStyles、RenderWithVisualStyles/UseVisualStyles/VisualStyleState、SetCompatibleTextRenderingDefault 调用时机和实际控件影响对照；初始化后重复/过晚设置行为一致。 |
| ⬜ | P1 / Application-10 | 实现 SetDefaultFont、SetColorMode、ColorMode/SystemColorMode/IsDarkModeEnabled 的 .NET10 契约，与现有 Lumina theme 的可选覆盖关系清楚；默认字体/系统颜色消息变化实际传播。 |
| 🟡 | P1 / Application-11 | 040的11a线程HKL/集合/消息连接保留；041的11b补Current getter/setter的真实OleRequired前置。CurrentCulture/UseWaitCursor、完整输入法/模态与OLE宿主context仍缺；部分服务不关闭父项。 |
| ✅ | P1 / Application-11a | 批次040：InputLanguage的当前/默认/安装HKL、线程切换、文化查找、布局名称、身份与只读集合；Application.CurrentInputLanguage同名转发。只读注册表并借用HKL，不卸载布局；13项服务回归及全量通过。限定范围不含固定源码OleRequired持久初始化副作用，MUI扩容最多一次且再变大时fallback；完整OLE线程context寿命归12，见040。 |
| ✅ | P1 / Application-11b | 批次041：CurrentInputLanguage getter/setter先OleRequired，再读取HKL或解析null默认/激活布局；失败setter也先初始化，Application入口仍直接转发。默认/安装布局和名称不额外初始化；HKL借用/只读注册表及MUI有界扩容保持。明确范围与回归见041，完整输入法/模态不关闭。 |
| 🟡 | P2 / Application-12 | 041的12a完成OleRequired及现有owner退出配对。StartupPath/ExecutablePath、CompanyName/ProductName/ProductVersion、各AppDataPath/Registry、Restart/SetSuspendState/消息循环注册，以及完整OLE宿主/ThreadContext仍缺；Windows专属部分单列但分母保留。 |
| ✅ | P2 / Application-12a | 批次041：标准OleRequired返回ApartmentState，线程缓存一次真实OleInitialize；S_OK/S_FALSE配对一次，RPC_E_CHANGED_MODE返回MTA且不释放外部引用。无循环ExitThread与最外层循环finally在原线程清理，嵌套/回调退出延后释放。其他失败HRESULT直接抛出而非固定源码缓存STA；自然线程终止不新增finalizer/FLS，完整context/COM对象/宿主生命周期仍待，见041。 |
| ⬜ | P1 / Application-13 | Native AOT 全 Demo 真正可见主窗、输入、模态、关闭/退出后进程正常结束；资源/TypeConverter 的 IL2026/IL3053 追具体路径，测试不能只看 publish exit code。 |
| 🟡 | P1 / Application-14 | 批次 007 将 activation handle/cookie 归属本线程，ExitThread 只请求退出、最外层循环离开 native 回调后释放，修正无 Form 退出的 SEH。完整样式策略/线程退出/多 UI 生命周期与模态仍待后续。 |
| ⬜ | P2 / Application-15 | Linux 用 GtkApplication/GApplication + GMainContext 投递；Run(Context) 保持 application hold/release 与无窗口托盘模式；不能启动第二个互相竞争的 GTK 主循环。 |
| ⬜ | P3 / Application-16 | macOS 用 NSApplication.run 与主 run loop、delegate 生命周期；关闭最后窗口是否退出由 ApplicationContext 决定，Command-Q 走可取消关闭链，所有 AppKit API 在主线程。 |
| ⬜ | 验收 / Application-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| USER32 线程消息循环 + 标准 context/filters，现有循环可修正复用。 | GtkApplication/GApplication/GMainContext，应用生命周期与 Windows 线程模型需兼容调度，原生 callback 使用 AOT-safe 声明。 | NSApplication + 主线程 run loop；退出/模态/菜单路由应共享一套标准 public Application。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- 2026-11-07 · 批次041：新增11b与12a，12由全待办改部分，保留全部原ID/未完API。最小ThreadStatic OLE状态复用现有退出路径，不引入完整ThreadContext；其他失败HRESULT显式传播是相对固定v10.0.11的明确差异。首轮9个内部服务测试在调用库前误假定CLR未初始化COM，保留失败并修正原生夹具；生产代码未为夹具改变，见[041](../batches/041.md)。

- 2026-11-07 · 批次040：新增11a的真实HKL服务与Application入口，11由全待办改部分。明确保留固定v10.0.11的OleRequired初始化副作用差异与完整线程寿命，未造空OleRequired或假成功；完整11/12、输入法/模态和平台不关闭，见[040](../batches/040.md)。

- 2026-11-07 · 批次039：Application.cs未改，Control-12l语言消息不关闭11或完整应用级语言服务；Form事件/取消与InputLanguage公共类型仍待，见[039](../batches/039.md)。

- 2026-11-07 · 批次035：06a接入实际键盘队列预处理，补Tab、KeyPreview、Enter/Escape、异常及原生子窗映射限定证据；首轮改写KeyUp测试缺transition位导致真实Translate额外WM_CHAR，修正有效消息后保持原断言通过。既有WM_QUIT/多UI退出差异不因本批关闭，见[035](../batches/035.md)。

- 2026-11-07 · 批次036：06a的队列消费继续由Application保持原路径，字符抑制由Control按固定WM_CHAR/WM_SYSCHAR/WM_IME_CHAR消息类型有界移除；未扩展消息过滤器、DoEvents、模态或完整退出语义，见[036](../batches/036.md)。

- 2026-10-17 · 批次029：程序验证接入未修改Application退出实现；15个场景仍多出WM_QUIT，继续归01/04与Form-07。另有关闭LostFocus差异，不混为同一根因；用户要求本批后停止，见[029](../batches/029.md)。

- 2026-10-16 · 批次028：未改Application代码；焦点探针的11个原生窗体场景关闭后出现参考没有的WM_QUIT，定位既有Form按全局OpenForms退出行为，归01/04保留待修。所有进程exit0不等于循环语义兼容；后续需覆盖Run外关闭、主窗与最后窗口、多UI线程及取消，见[028](../batches/028.md)。

- 2026-10-16 · 批次016历史回标：任务04从全待办纠正为部分完成，只补007已有的无Form ExitThread请求/最外层线程activation释放和两个STA证据；Exit取消/事件/ApplicationContext/完整多UI与竞态仍待完成，未改源码或历史批次记录。

- `9d5165f`（2026-04-09）：新增启动兼容功能，含待修正 DPI 表面。
- `1c89c0a`（2026-04-01）：增强主题与控件初始化。
- `2e8431f`（2026-04-01）：主题和 Demo UI 改进涉及启动层。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。
- 2026-10-15 · 批次 001：修复启动签名、命名参数/null 主窗路径；新增 AOT 友好的 DPI interop、真实状态回读及建窗门禁；6 项无副作用回归、14 组独立进程 DPI 对照通过。Run 的其他重载及完整生命周期未关闭，Windows/Linux/macOS 的完整兼容验收仍待后续。
- 2026-10-16 · 批次 003：新增标准 ThreadException/OnThreadException 的首个可观察范围；4 项托管测试覆盖对象身份/线程隔离/重入/异常恢复，线程探针证明未调用 EndInvoke 的错误也在 UI 线程上报。默认原生 OK 提示不等同标准 ThreadExceptionDialog，SetUnhandledExceptionMode 及原生回调全族仍未完成；错误事件处理器再抛时显式 FailFast 防止越过 native ABI。

- 2026-10-16 · 批次 007：现有 Run/RunMessageLoop 共享安装与外层 finally 还原；204 条参考/JIT/实际 AOT 观测和新增 STA 检查覆盖标准 AutoInstall/Uninstall、默认 await 与两个不同 STA 的无 Form 循环。ExitThread 不在 HWND 回调内提前 DeactivateActCtx，实际释放移至外层循环退出后，激活资源改为线程归属并允许下次进入重新激活。全局 OpenForms 的最后窗体退出判断、ApplicationContext/完整 Run/模态与并行 UI 退出仍保留，见 [批次 007](../batches/007.md)。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.application?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/class.Application.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nsapplication)
