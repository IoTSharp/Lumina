# Application：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/Application.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.Application`；基类：`System.Object`。Lumina 同名类型：🟡 存在；基类及基线接口形状：🟡 初步匹配，待源码消费验证。

公开属性 **24**，公开事件 **6**，公开方法 **24**；受保护扩展成员 **0**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-CBF4922364596088 | Event | `public static event System.EventHandler ApplicationExit` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC0C81056DFD3233 | Event | `public static event System.EventHandler EnterThreadModal` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-93B7F3F6A96F46EF | Event | `public static event System.EventHandler Idle` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EFF9AF35E3A1F084 | Event | `public static event System.EventHandler LeaveThreadModal` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50C6D9F8B4C5C5F2 | Event | `public static event System.Threading.ThreadExceptionEventHandler ThreadException` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5652CC89CE11C6E6 | Event | `public static event System.EventHandler ThreadExit` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F84AD7057C02D8EE | Method | `public static System.Void AddMessageFilter(System.Windows.Forms.IMessageFilter value)` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-613285524458FFCF | Method | `public static System.Void DoEvents()` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EB230D2BF4632753 | Method | `public static System.Void EnableVisualStyles()` | `System.Windows.Forms.Application`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F89CAC82D014E629 | Method | `public static System.Void Exit()` | `System.Windows.Forms.Application`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-20307D5DCA653D68 | Method | `public static System.Void Exit(System.ComponentModel.CancelEventArgs e)` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2DC802E31F60A2C7 | Method | `public static System.Void ExitThread()` | `System.Windows.Forms.Application`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A98B2E95A56F1EAD | Method | `public static System.Boolean FilterMessage(ref System.Windows.Forms.Message message)` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7E6D19022D11292E | Method | `public static System.Threading.ApartmentState OleRequired()` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AB443B553ED0E6D1 | Method | `public static System.Void OnThreadException(System.Exception t)` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-096B337437504840 | Method | `public static System.Void RaiseIdle(System.EventArgs e)` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E8C735E1BEF40A0 | Method | `public static System.Void RegisterMessageLoop(System.Windows.Forms.Application+MessageLoopCallback callback)` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-95FFED7E0CFF30C0 | Method | `public static System.Void RemoveMessageFilter(System.Windows.Forms.IMessageFilter value)` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8D72A7E333903B1A | Method | `public static System.Void Restart()` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-424B747AEBD15173 | Method | `public static System.Void Run()` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7A5E31DFFF1AD11A | Method | `public static System.Void Run(System.Windows.Forms.ApplicationContext context)` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-90A7F4821637043A | Method | `public static System.Void Run(System.Windows.Forms.Form mainForm)` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F1B1F3D31B4DAE24 | Method | `public static System.Void SetColorMode(System.Windows.Forms.SystemColorMode systemColorMode)` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FF428241DACD1A42 | Method | `public static System.Void SetCompatibleTextRenderingDefault(System.Boolean defaultValue)` | `System.Windows.Forms.Application`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-71D32853C81F1CE9 | Method | `public static System.Void SetDefaultFont(System.Drawing.Font font)` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-05A481074DE21DA6 | Method | `public static System.Boolean SetHighDpiMode(System.Windows.Forms.HighDpiMode highDpiMode)` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E767946B1E300D53 | Method | `public static System.Boolean SetSuspendState(System.Windows.Forms.PowerState state, System.Boolean force, System.Boolean disableWakeEvent)` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A6194A344793A3B1 | Method | `public static System.Void SetUnhandledExceptionMode(System.Windows.Forms.UnhandledExceptionMode mode)` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9936842EF0020C1E | Method | `public static System.Void SetUnhandledExceptionMode(System.Windows.Forms.UnhandledExceptionMode mode, System.Boolean threadScope)` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4348B93FD1A912D9 | Method | `public static System.Void UnregisterMessageLoop()` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6B247AF52EEF9637 | Property | `static System.Boolean AllowQuit { public get; }` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E6EFDBD85151EE45 | Property | `static System.Windows.Forms.SystemColorMode ColorMode { public get; }` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-89DC711D465C9B9F | Property | `static System.String CommonAppDataPath { public get; }` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0E9EAFC2B183376 | Property | `static Microsoft.Win32.RegistryKey CommonAppDataRegistry { public get; }` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-00BBA1987B1967E9 | Property | `static System.String CompanyName { public get; }` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CA137592540C90CC | Property | `static System.Globalization.CultureInfo CurrentCulture { public get; public set; }` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E84E62458B7A5C9 | Property | `static System.Windows.Forms.InputLanguage CurrentInputLanguage { public get; public set; }` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-21D72D7EF1B1176D | Property | `static System.String ExecutablePath { public get; }` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-333BE319652550FA | Property | `static System.Windows.Forms.HighDpiMode HighDpiMode { public get; }` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-544BD6933894EBF1 | Property | `static System.Boolean IsDarkModeEnabled { public get; }` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5F166D9B7DA0E073 | Property | `static System.String LocalUserAppDataPath { public get; }` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E4B1D097B77B1A1 | Property | `static System.Boolean MessageLoop { public get; }` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C5C05F33B447418 | Property | `static System.Windows.Forms.FormCollection OpenForms { public get; }` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B6FEA63E47B5AEC2 | Property | `static System.String ProductName { public get; }` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B1BBC057FE002CBE | Property | `static System.String ProductVersion { public get; }` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C83EDF310C7D3DF0 | Property | `static System.Boolean RenderWithVisualStyles { public get; }` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-966A33B3BCBE8418 | Property | `static System.String SafeTopLevelCaptionFormat { public get; public set; }` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-266F292F1C6F29C0 | Property | `static System.String StartupPath { public get; }` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-97867B8381097546 | Property | `static System.Windows.Forms.SystemColorMode SystemColorMode { public get; }` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4C89107E97CA9716 | Property | `static System.String UserAppDataPath { public get; }` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AD17F9CBCCFF88BE | Property | `static Microsoft.Win32.RegistryKey UserAppDataRegistry { public get; }` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CAED2DC1E88933BE | Property | `static System.Boolean UseVisualStyles { public get; }` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9CD1909853748C66 | Property | `static System.Boolean UseWaitCursor { public get; public set; }` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-616467F709A8AA75 | Property | `static System.Windows.Forms.VisualStyles.VisualStyleState VisualStyleState { public get; public set; }` | `System.Windows.Forms.Application`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
