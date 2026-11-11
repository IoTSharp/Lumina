# Forms/Core 平台依赖

本页记录 REP-003 的实际依赖边界，依据当前工程声明和原生入口维护。任务状态仍以[项目整合任务](project-consolidation.md)为准；[项目需求](../../PROJECT_REQUIREMENTS.md)规定提供者选择和类型身份。依赖盘点与 Windows 入口隔离不等于 Linux/macOS 后端或平台验收已完成。

## 托管项目与 Drawing

| 项目/路径 | 当前依赖 | 平台边界 |
| --- | --- | --- |
| `Lumina.Forms` | `net10.0-windows;net10.0`；引用 `Lumina.Core`；两个目标均直接引用官方 `System.Drawing.Common 10.0.0` | 当前实际实现为 Windows 原生控件。Windows 两个目标都使用官方 Drawing；无 RID 的工程构建继续保留这一现状。尚未接入 Linux 自有 Drawing 资产，不把无 RID 或 `net10.0` 自动解释成 Linux。 |
| `Lumina.Core` | `net10.0-windows;net10.0`；无直接 Drawing/NuGet 运行时依赖 | `EffectKind`、`EffectOptions` 等类型身份保留；`LuminaWindow` 的真实 DWM 入口仅允许 Windows。 |
| `Lumina.Ext.WinForms` | `net10.0-windows`、`UseWindowsForms=true`、引用 `Lumina.Core` | `System.Windows.Forms` 命名空间提供原有扩展方法；属于真实 WinForms 分支。 |
| `NativeFormsDemo/net10.0-windows` | 真实 WinForms、`Lumina.Ext.WinForms`、`System.Resources.Extensions 10.0.5` | 保留设计器、原始 `.Designer.cs` / `.resx`；不是 Lumina.Forms 实现路径。 |
| `NativeFormsDemo/net10.0` | `Lumina.Forms`、`Lumina.Core`、`PublishAot=true` | `BitmapResources.targets` 在中间目录转换五张 Bitmap 为内建 `byte[]` 资源及相应资源管理器引用，保留原始设计器文件；现已支持 Bitmap/Icon 字节与 Font 不变量字符串静态转换；二进制 Font、完整 ApplyResources/设计器往返及资源实际运行仍待。 |
| `Lumina.Forms.Analyzers` | `net10.0`、Roslyn/Workspaces/Composition 构建依赖 | 工程按 `OutputItemType=Analyzer`、`ReferenceOutputAssembly=false` 引用，隔离应用发布属性；Forms 包把分析器放入 `analyzers/dotnet/cs`，不作为运行时 UI 依赖。 |

Drawing 公共签名仍含 `System.Drawing.Font`、`Image`、`Bitmap`、`Icon`、`Graphics`。这些类型不能靠把 TFM 改成 `net10.0` 获得 Unix 实现。Windows 不引入 Drawing wrapper、facade 或自有提供者；Linux 的 `Lumina.Drawing.Common`、程序集完整身份、资源绑定、传递依赖和 AOT 选择继续按 XPL-002 推进，当前没有新增假定可用的包入口。

## Windows 原生依赖

| 原生库 | 源码入口与用途 | 当前隔离范围 |
| --- | --- | --- |
| `dwmapi.dll` | `Lumina.Core/LuminaWindow.cs` 的背景效果；`Lumina.Forms/Win32.cs` 的标题栏属性及系统强调色；`Lumina.Ext.WinForms/LuminaFormThemeExtensions.cs` 的真实 WinForms 主题扩展 | Core 的 `SetEffect` / 非零句柄 `Clear` 在 P/Invoke 前检查 Windows，非 Windows 抛 `PlatformNotSupportedException`。`SetEffect(0,...)` 保留原参数异常，`Clear(0)` 保留原无操作语义。Forms 的系统强调色、标题栏和通用对话框 DWM 分支有 Windows/版本检查；效果请求复用 Core 的检查。WinForms 扩展本身为 Windows 专用目标。 |
| `user32.dll` | `Win32.cs`、`ApplicationHighDpi.cs`、应用消息入口 | HWND、消息泵、输入、窗口状态、DC 和 DPI。不是 GTK/AppKit 的可移植实现。 |
| `gdi32.dll`、`gdiplus.dll` | `Win32.cs`、`RichTextBox.cs`、`PictureBox.cs`，以及官方 Drawing 的 Windows 图形实现 | 字体、DC、度量、绘图和图像；资源必须继续按各后端的真实寿命处理。 |
| `comctl32.dll`、`uxtheme.dll` | `Win32.cs` 与公共控件/菜单/主题消费者 | Windows 公共控件、子类化、主题与暗色适配。 |
| `imm32.dll`、`ole32.dll` | `Win32.cs`、`NativeImeContext`、`ApplicationOleState` | Windows IME 与 OLE apartment 生命周期；不能在 Unix 伪造相同句柄或初始化成功。 |
| `comdlg32.dll`、`shell32.dll` | `Win32.cs`、`NotifyIcon.cs` | Windows 公共对话框、Shell 与托盘。 |
| `advapi32.dll`、`kernel32.dll` | `Win32.cs` | 注册表语言名称、模块、activation context、线程及进程服务。 |
| `shcore.dll`、`ntdll.dll` | `ApplicationHighDpi.cs`、`DarkModeCapabilities.cs` | 按 Windows 能力查询 DPI 与系统版本；不是 Unix 平台检测替代品。 |

## 仍待实现与验收

- Linux 按部署 OS/RID 选自有 Drawing，排除官方 Windows 实现及不适用的传递运行资产；验证固定 API 身份、Font/Image/Icon 真实资源、资源 reader/converter 和 GTK4 控件消费。macOS 另列 AppKit 与图形资源原型。
- Unix 控件、消息投递、IME、可访问性、窗口/菜单/托盘、对话框和图形后端分别实现；Core 的 Windows guard 只建立失败边界，不提供对应 Unix 功能。
- Windows 主窗 AOT 的可见启动、交互、关闭退出、设计器往返和资源释放集中真机验证；CI 的 AOT publish 和产物上传只证明对应发布链执行结果。
- FND-006 的现存分析警告、完整资源类型及 Designer/AOT 运行门槛继续登记在[验证记录](validation.md)；依赖表不抹除历史失败、超时、UNKNOWN 或 NOT_RUN。
