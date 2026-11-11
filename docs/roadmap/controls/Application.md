# Application

本页任务表记录当前代码范围与剩余工作；代码完成子项标记在[完成状态](#已完成任务)，任务 ID 与验收状态均不因整理改变。剩余重点是 ApplicationContext、主循环/模态与退出归属、消息过滤、完整 OLE 宿主和异常处理。

[完整 API 与继承基线](../api/Application.md) · [任务索引](../README.md) · [依赖顺序](../execution.md) · [验证门槛](../validation.md) · [平台要求](../platforms.md)

固定基线为 .NET 10.0.11。Windows、Linux、macOS 分别验收，当前没有完整逐控件 98% 结论。

## 2026-11-11 M0 代码状态

- 已补同线程外部宿主窗口快照、模态期间新增窗口禁用、嵌套模态 scope 转移、ThreadExceptionDialog 默认路径及外部宿主消息泵寿命边界。
- 代码证据：`Lumina.Forms/Application.cs`、`ThreadExceptionDialog.cs`、`Win32.Application.cs`；新增 ThreadExceptionDialog 无窗口回归并入本轮 12 项定向测试。
- 完整 COM/OLE 宿主协作、真实外部窗口/模态/重入运行、Designer、完整 AOT 交互仍未关闭；Application 父项保持 🟡。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| 🟡 | P0 / Application-01 | Run()/Run(Form)/Run(ApplicationContext) 的标准入口、主循环归属和模态独立消息泵已实现；本轮补外部宿主窗口 scope。极端重入/并发及历史028/029严格差分的实际重验仍待。 |
| 🟡 | P0 / Application-03 | 默认 context 安装/还原、多 UI 循环、activation/OLE 清理及外部宿主模态保留边界已接入；完整 COM 宿主、关闭竞态和真实多 UI/模态运行仍待。 |
| 🟡 | P1 / Application-04 | Exit()/Exit(CancelEventArgs) 预检 FormClosing.Cancel，取消则不主动销毁窗口，通过后完成关闭；Context/各 UI 循环分别请求退出，ThreadExit/ApplicationExit 在循环清理后触发。极端重入/动态新窗、原生失败和完整多 UI 退出运行验收仍待。 |
| ✅ | P1 / Application-05 | 标准 FormCollection/OpenForms 已实现，保持稳定集合对象、索引/名称查询和集合锁；首次显示登记、隐藏保留、关闭/释放移除。既有 HashSet 不再作为公开集合；Windows 可见/隐藏/多线程实际运行验收后置。 |
| 🟡 | P1 / Application-06 | 已接 AddMessageFilter/RemoveMessageFilter/FilterMessage/IMessageFilter 的线程列表及重入快照；公共 filter 的 ref 修改供后续过滤器观察，不改最终 MSG。GetMessage=-1 抛 Win32Exception，WM_QUIT 传递退出码；原有键盘预处理保留。完整快捷键、IsDialogMessage/外部宿主仍待。 |
| 🟡 | P1 / Application-07 | Idle/RaiseIdle、DoEvents、模态通知、MessageLoop/AllowQuit 及宿主循环回调已实现；本轮补外部窗口快照和动态窗口纳入 scope。实际宿主/模态运行仍待。 |
| 🟡 | P1 / Application-08 | 已接 ThreadExceptionDialog 摘要/详情/Continue/Quit 默认路径、SetUnhandledExceptionMode 和重入保护；完整标准 UI/本地化与 UnmanagedCallersOnly 的托管异常传播仍待。 |
| ⬜ | P1 / Application-09 | EnableVisualStyles、RenderWithVisualStyles/UseVisualStyles/VisualStyleState、SetCompatibleTextRenderingDefault 调用时机和实际控件影响对照；初始化后重复/过晚设置行为一致。 |
| ⬜ | P1 / Application-10 | 实现 SetDefaultFont、SetColorMode、ColorMode/SystemColorMode/IsDarkModeEnabled 的 .NET10 契约，与现有 Lumina theme 的可选覆盖关系清楚；默认字体/系统颜色消息变化实际传播。 |
| 🟡 | P1 / Application-11 | 040的11a线程HKL/集合/消息连接保留；041的11b补Current getter/setter的真实OleRequired前置。CurrentCulture/UseWaitCursor、完整输入法/模态与OLE宿主context仍缺；部分服务不关闭父项。 |
| 🟡 | P2 / Application-12 | OleRequired 线程引用配对、Context/模态退出、RegisterMessageLoop/UnregisterMessageLoop 和外部宿主保留/恢复边界已实现。IMsoComponentManager/完整 COM 宿主、路径/元数据/Restart 等仍待。 |
| ⬜ | P1 / Application-13 | Native AOT 全 Demo 真正可见主窗、输入、模态、关闭/退出后进程正常结束；资源/TypeConverter 的 IL2026/IL3053 追具体路径，测试不能只看 publish exit code。 |
| 🟡 | P1 / Application-14 | activation handle/cookie 继续归属本线程；主循环/独立模态最外层退出后才释放，并汇总清理异常。窗口销毁不再决定全局退出；完整样式策略、外部宿主/多 UI/极端模态与实际运行仍待。 |
| ⬜ | P2 / Application-15 | Linux 用 GtkApplication/GApplication + GMainContext 投递；Run(Context) 保持 application hold/release 与无窗口托盘模式；不能启动第二个互相竞争的 GTK 主循环。 |
| ⬜ | P3 / Application-16 | macOS 用 NSApplication.run 与主 run loop、delegate 生命周期；关闭最后窗口是否退出由 ApplicationContext 决定，Command-Q 走可取消关闭链，所有 AppKit API 在主线程。 |
| ⬜ | 验收 / Application-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| USER32 线程消息循环 + 标准 context/filters，现有循环可修正复用。 | GtkApplication/GApplication/GMainContext，应用生命周期与 Windows 线程模型需兼容调度，原生 callback 使用 AOT-safe 声明。 | NSApplication + 主线程 run loop；退出/模态/菜单路由应共享一套标准 public Application。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 已完成任务

✅ 表示该行定义范围代码完成；父项的剩余代码与平台验收仍按当前任务表记录。

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ✅ | P0 / Application-01a | 批次 001：Run(Form mainForm) 返回 void，支持相同命名参数调用；null 主窗沿消息循环执行，移除不符基线的空值异常。无界面签名检查和双目标编译通过，消息循环生命周期属于父任务待办。 |
| ✅ | P0 / Application-02 | SetHighDpiMode 返回真实系统 BOOL/HRESULT 的成功结果，校验枚举，按 OS 能力回退；HighDpiMode 读取当前线程实际 awareness。首次窗口/托盘窗口创建前永久关闭设置入口，manifest/宿主/重复调用失败不假报成功。14 组隔离进程对照通过；旧系统回退、建窗回调、跨屏及真实缩放验收后置。 |
| ✅ | P1 / Application-06a | 批次035/036：GetMessage检索的键盘消息按FromChildHandle找到托管控件，Translate/Dispatch前实际调用PreProcessMessage；消费停止，未消费保留修改的目标/消息/参数，native时空字段保持。035异常报告一次并停止故障消息，036在Control中补队列字符抑制。11项真实队列/直接发送边界回归通过。过滤器、原生判别、模态及完整退出归父项。 |
| ✅ | P1 / Application-08a | 批次 003：线程局部订阅、当前 Thread sender、原始异常对象、重入保护和处理器异常后的状态恢复；4 项托管回归及实际 UI 投递事件观测通过。无处理器的 Windows 原生 OK 提示保留可见/继续行为，完整 Quit/Details UI 后置；投递事件处理器再抛在 native 边界前显式终止，区别于 public OnThreadException 的直接传播。 |
| ✅ | P1 / Application-11a | 批次040：InputLanguage的当前/默认/安装HKL、线程切换、文化查找、布局名称、身份与只读集合；Application.CurrentInputLanguage同名转发。只读注册表并借用HKL，不卸载布局；13项服务回归及全量通过。限定范围不含固定源码OleRequired持久初始化副作用，MUI扩容最多一次且再变大时fallback；完整OLE线程context寿命归12，见040。 |
| ✅ | P1 / Application-11b | 批次041：CurrentInputLanguage getter/setter先OleRequired，再读取HKL或解析null默认/激活布局；失败setter也先初始化，Application入口仍直接转发。默认/安装布局和名称不额外初始化；HKL借用/只读注册表及MUI有界扩容保持。明确范围与回归见041，完整输入法/模态不关闭。 |
| ✅ | P2 / Application-12a | 批次041：标准OleRequired返回ApartmentState，线程缓存一次真实OleInitialize；S_OK/S_FALSE配对一次，RPC_E_CHANGED_MODE返回MTA且不释放外部引用。无循环ExitThread与最外层循环finally在原线程清理，嵌套/回调退出延后释放。其他失败HRESULT直接抛出而非固定源码缓存STA；自然线程终止不新增finalizer/FLS，完整context/COM对象/宿主生命周期仍待，见041。 |

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.application?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/class.Application.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nsapplication)
