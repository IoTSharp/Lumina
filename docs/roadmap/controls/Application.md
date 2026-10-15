# Application

🟡 **现状：**已有 GetMessage/TranslateMessage/DispatchMessage、样式 activation context 与 open form 跟踪；Run(Form) 返回 int、SetHighDpiMode 返回 void 且空实现，标准均不匹配。缺 Run()/Run(ApplicationContext)、完整消息过滤/退出与异常事件。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/Application.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：[Application.cs](../../../Lumina.Forms/Application.cs)
- 排序：内部窗口/UI 调度、Form、ApplicationContext/WindowsFormsSynchronizationContext；启动 P0。内部投递窗口不要求先完成 M4 的 NativeWindow 全量公共扩展。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P0 / Application-01 | Run(Form) 改为 void，补 Run()/Run(ApplicationContext)；拒绝同线程非法嵌套 Run，模态循环与主循环分别管理，主窗结束与最后窗口结束不能混淆。 |
| ⬜ | P0 / Application-02 | SetHighDpiMode(HighDpiMode) 返回 bool 并真正设置/读取进程 DPI awareness；首次建 HWND 前调用，manifest/宿主已设置或失败时返回值与官方一致。 |
| ⬜ | P0 / Application-03 | 安装并卸载 WindowsFormsSynchronizationContext，确定 UI 创建线程与投递窗口；处理 InvokeAsync/await 回到 UI、退出期间取消和线程存活。 |
| ⬜ | P1 / Application-04 | 实现 Exit()/Exit(CancelEventArgs)/ExitThread、ApplicationExit/ThreadExit；各 FormClosing 取消必须阻止不应发生的全局退出，多个 UI 线程和无主窗 Context 分别验收。 |
| ⬜ | P1 / Application-05 | 实现 OpenForms 的标准 FormCollection、可见/隐藏/释放窗口登记、索引与名称查询；不得直接暴露内部 HashSet 改变枚举契约。 |
| ⬜ | P1 / Application-06 | 实现 AddMessageFilter/RemoveMessageFilter/FilterMessage/IMessageFilter、消息预处理/快捷键/IsDialogMessage；WM_QUIT/GetMessage=-1 处理明确，不能只做 Translate/Dispatch。 |
| ⬜ | P1 / Application-07 | 实现 Idle/RaiseIdle、DoEvents、EnterThreadModal/LeaveThreadModal、MessageLoop/AllowQuit；DoEvents 的重入遵循标准，不用 Task.Run 代替消息泵。 |
| ⬜ | P1 / Application-08 | 实现 SetUnhandledExceptionMode/ThreadException/OnThreadException，与 AppDomain 未处理异常分工明确；异常不穿越 native callback，也不能全部吞掉假装成功。 |
| ⬜ | P1 / Application-09 | EnableVisualStyles、RenderWithVisualStyles/UseVisualStyles/VisualStyleState、SetCompatibleTextRenderingDefault 调用时机和实际控件影响对照；初始化后重复/过晚设置行为一致。 |
| ⬜ | P1 / Application-10 | 实现 SetDefaultFont、SetColorMode、ColorMode/SystemColorMode/IsDarkModeEnabled 的 .NET10 契约，与现有 Lumina theme 的可选覆盖关系清楚；默认字体/系统颜色消息变化实际传播。 |
| ⬜ | P1 / Application-11 | 实现 CurrentCulture/CurrentInputLanguage、UseWaitCursor，影响现有窗口和新窗口；输入语言事件与控件 IME 路径联动。 |
| ⬜ | P2 / Application-12 | 补 StartupPath/ExecutablePath、CompanyName/ProductName/ProductVersion、各 AppDataPath/Registry、Restart/OleRequired/SetSuspendState/消息循环注册等全部剩余 API；Windows Registry/电源/OLE 专属部分单列，但 Windows 分母保留。 |
| ⬜ | P1 / Application-13 | Native AOT 全 Demo 真正可见主窗、输入、模态、关闭/退出后进程正常结束；资源/TypeConverter 的 IL2026/IL3053 追具体路径，测试不能只看 publish exit code。 |
| ⬜ | P1 / Application-14 | Windows 共享现有 USER32 消息泵与 common-controls activation context；context 是 UI 线程资源，不能一次 ExitThread 释放其他 UI 线程仍需的状态。 |
| ⬜ | P2 / Application-15 | Linux 用 GtkApplication/GApplication + GMainContext 投递；Run(Context) 保持 application hold/release 与无窗口托盘模式；不能启动第二个互相竞争的 GTK 主循环。 |
| ⬜ | P3 / Application-16 | macOS 用 NSApplication.run 与主 run loop、delegate 生命周期；关闭最后窗口是否退出由 ApplicationContext 决定，Command-Q 走可取消关闭链，所有 AppKit API 在主线程。 |
| ⬜ | 验收 / Application-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| USER32 线程消息循环 + 标准 context/filters，现有循环可修正复用。 | GtkApplication/GApplication/GMainContext，应用生命周期与 Windows 线程模型需兼容调度，原生 callback 使用 AOT-safe 声明。 | NSApplication + 主线程 run loop；退出/模态/菜单路由应共享一套标准 public Application。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- `9d5165f`（2026-04-09）：新增启动兼容功能，含待修正 DPI 表面。
- `1c89c0a`（2026-04-01）：增强主题与控件初始化。
- `2e8431f`（2026-04-01）：主题和 Demo UI 改进涉及启动层。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.application?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/class.Application.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nsapplication)
