# LuminaForms 项目目标、需求与实施原则

确定日期：2026-10-15；评估修订：2026-10-16。本文规定产品方向和实施约束，适用于规划、编码、审查、打包与发布。需求目标与当前已实现、已验收的能力分开记录。

后续工作先读本文，再按 [AGENTS.md](AGENTS.md) 的工作规则和 [HANDOFF.md](HANDOFF.md) 的当前批次继续。具体任务由 [路线图](ROADMAP.md) 和 [逐控件规划](docs/roadmap/README.md) 管理；这些文档应遵循本文。调整本文件中的产品方向时，应明确修订对应需求及迁移影响，不能通过局部实现悄悄改变兼容承诺。

## 产品目标

- LuminaForms 是面向 .NET 10 与 Native AOT 的 WinForms 替代层，现有项目、包及命名空间保持 `Lumina.Forms`。
- 优先让既有应用通过引用和命名空间切换迁移，保留 WinForms 控件名称、成员签名、默认值、事件、布局、线程与释放语义。
- 所有仓库项目，包括分析器和构建工具，只使用 `net10.0` 或 `net10.0-windows`，不增加旧 .NET 或 netstandard 目标。
- 保留 `NativeFormsDemo` 固定双目标：`net10.0-windows` 使用真实 WinForms 和 Visual Studio 设计器；`net10.0` 使用 Lumina.Forms 并保持 Native AOT 能力。
- Windows 先完成兼容实现与集中验收，再推广 Linux；macOS 单列原型与真机验证。跨平台仍以系统原生控件为方向。

## 已确定的 Drawing 平台策略

**Windows 只使用微软官方 System.Drawing.Common；Linux 才引入自有 Drawing 实现。** Windows 的 `net10.0` LuminaForms/AOT 路径同样遵守此规则。

评估结论：这一平台分工适合作为 Lumina.Forms 的绘图兼容配套。先验证真实应用必需的字体、图像、资源与原生控件场景，再扩展覆盖范围；同名程序集和现有小型探针不足以承诺完整二进制替换。

| 应用目标 | 控件层 | Drawing 提供者 | 自有 Drawing 包 |
| --- | --- | --- | --- |
| Windows / `net10.0-windows` | 真实 WinForms，保留 VS 设计器 | 官方 `System.Drawing.Common` | 不引入 |
| Windows / `net10.0`，如 `win-x64`、`win-arm64` | Lumina.Forms，保持 Native AOT | 官方 `System.Drawing.Common` | 不引入，包括额外 wrapper 或 facade |
| Linux / `net10.0` | Lumina.Forms，规划中的 GTK4 原生控件 | 自有兼容实现 | 引入 `Lumina.Drawing.Common` |
| macOS / `net10.0` | 规划中的 AppKit 原生控件 | 后续验证自有兼容实现与系统框架的适配 | 按独立原型确定实际资产，不提前声明已支持 |

Linux 自有实现按以下命名方向推进。包名、程序集名和命名空间是三个独立概念；同名程序集的完整身份与发布方案经过 XPL-002 验证后冻结：

| 项目 | 约定 |
| --- | --- |
| NuGet PackageId | `Lumina.Drawing.Common` |
| AssemblyName / DLL | 优先验证 `System.Drawing.Common` / `System.Drawing.Common.dll`，用于减少既有类型与资源绑定的迁移；不预先宣称完整二进制替换 |
| 公共命名空间 | 保留 `System.Drawing` 及对应的 Drawing2D、Imaging、Text、Printing 等命名空间 |
| API 与行为基线 | 固定 .NET 10.0.11；基线变更须明确记录，不能随 SDK 更新自动漂移 |

这是服务于 Lumina.Forms 的自有绘图兼容配套，不代表微软官方签署、维护或支持其 Linux 实现。不得把官方包文件原样搬到 Linux 后声称已获得跨平台能力。也不能把 Drawing 提供者替换视为 GTK4/AppKit 控件、事件与线程后端已经实现。

## 依赖选择与类型身份

1. 按目标操作系统、RID 和明确的构建配置选择 Drawing 提供者。`net10.0` 同时用于 Windows 和 Linux，不能把它等同于 Linux，也不能用构建宿主的操作系统代替部署目标。无 RID 的还原、设计时构建和项目引用必须有确定策略。
2. 应用、Lumina.Forms、扩展库、资源生成链和发布资产必须选择一致的 Drawing 提供者。同一应用的编译、运行和 AOT 链接结果只能有一个有效的 `System.Drawing.Common` 实现。
3. Linux 必须处理 Lumina.Forms 或第三方库传递引入的官方 Drawing 资产。不能只给应用增加自有包；还需核查 `Microsoft.Win32.SystemEvents` 等子依赖，不能假定排除父包就移除了整个依赖树。
4. 保留既有 Drawing 类型绑定是实施目标。除命名空间和 DLL 文件名外，须验证程序集名称、版本、Culture、公钥标记、BCL 类型转发、基类、接口、公开/受保护签名及既有消费者的 TypeRef。版本、公钥标记和是否采用 public-sign 的具体策略，由真实依赖图与资源/AOT 证据确定，不由身份 stub 代替。强名称、公钥元数据、包签名和发布者来源分别记录；类型绑定成功不代表功能兼容。
5. NuGet 的编译引用与运行实现分别验证。不能假定 `runtimes/{rid}/ref/` 会自动切换 compile 资产，也不能只检查输出目录中的文件名。保存各目标的 compile/runtime/native 依赖证据。
6. 如果改用不同程序集身份导致 Lumina.Forms、第三方库或资源必须重编译，应明确列为迁移变化并修订需求；不能当作无改动的二进制替换。

## Drawing 兼容范围

- 继续复用官方 `System.Drawing.Primitives` 的类型身份，包括 Point、Size、Rectangle、Color 及其浮点变体、KnownColor、ColorTranslator、SystemColors，不复制出另一套同名基础类型。
- 首个交付范围按 Lumina.Forms 的公开面、真实应用、Designer/resx 和兼容测试冻结必需子集，覆盖所需的 Font/FontFamily、Image/Bitmap/Icon、Graphics、画笔/画刷、几何状态、转换器及资源契约。每批明确成员、场景和不支持项，不先实现全部 Drawing 类型再验证原生控件。
- 保留 WinForms 既有公共名称与消费方式。基础兼容不以要求业务代码全面改用 `Lumina.Drawing.*` 或自创绘图 API 为前提。
- 兼容包含默认值、参数与异常、字体度量、中文和 fallback、DPI、图形状态/变换/裁剪、像素格式/stride/alpha、编解码、流生命周期、克隆与 `IDisposable` 释放，不能只覆盖方法声明。
- 完整 API 清单用于记账和披露，Drawing2D、Imaging（含 Effects）、Text 和 Printing 按实际兼容需求进入后续批次。当前 Demo 没有使用的成员仍需登记，未实现不得计为支持；首个版本不承诺替代任意依赖完整官方 Drawing 的第三方二进制。
- 普通字体、图像和绘图是跨平台目标，不能整类标成 Windows 专属以缩小分母。HDC、HWND、HBITMAP、HICON、LOGFONT、DEVMODE 等本机契约逐成员披露；WMF/EMF 的文件处理与 Win32 句柄互操作也分别判断。未实现的功能保持明确缺口，禁止伪句柄、空方法、吞异常或常量成功。

## 原生控件与后端边界

Windows 保持 Win32/真实 WinForms 路线；Linux 规划 GTK4，macOS 规划 AppKit。Drawing 的职责是图形资源和自定义绘制，不将所有系统控件替换成统一 canvas 自绘，也不引入 WPF、MAUI、Avalonia、Uno 或新的公共 UI 抽象来替代兼容工作。

Linux 的首选受限原型是 Cairo/Pango 与系统字体、图像能力的组合；macOS 的候选为 CoreGraphics/CoreText/ImageIO 与 AppKit 适配。SkiaSharp 可以作为受限绘图后端的对照备选。具体库版本、codec、native 分发方式和最低系统版本由原型证据确定，不因写入规划而视为已冻结、已部署或已验收。

Font、Image、Icon 必须能被相应原生控件真实消费；库内负责 Pango/GTK 对象与 NSFont/NSImage/CGImage 的转换、引用持有、像素寿命和释放。native handle、unsafe、marshaling 和平台分支不得泄漏为 Demo 或业务代码的新资源释放模型。

先实现一个有限的 Linux 资源与原生控件场景，再决定是否扩展后端。不得为推进此配套而暂停既有 Windows 基础兼容批次，或未经场景验证复制整个 Drawing 栈。

## Designer 与资源需求

- 保留 Windows 目标的真实 Visual Studio Designer 往返能力；静态布局、文本、图片和初始状态继续由原始 `.Designer.cs` 与 `.resx` 管理。
- 目标是同一静态 Designer 源码与资源能服务两个目标，不把初始化迁到构造函数或 Form.Load 来绕过资源问题。
- 单独验证 assembly-qualified type、历史 `System.Drawing` facade 名称、TypeConverter、ResXFileRef、强类型 Resources.Designer 和 ImageListStreamer。相同命名空间或程序集名不能代替这些验证。
- 必要的资源适配放在库或构建链中，对目标生成中间副本，保留原始设计器资源；不要求 Demo 维护另一套运行时 UI、主题或资源基础设施。
- 最少覆盖 Font、Bitmap、Icon、ImageList、中文文本、字体度量、透明图像、缩放、流和释放，并记录 Windows 设计器重新保存后的结果。

## Native AOT 与验收要求

`net10.0` 路径持续保持裁剪和 Native AOT 兼容。提供者、资源 reader/converter 和 native imports 在发布时确定；不能依赖运行时动态程序集替换、代码生成或不受支持的反射路径。

资源开关或 AOT 发布成功都不能代替运行。应验证启动、实际资源解码、控件消费、绘制和关闭释放，记录分析警告及其处理，不用全局抑制掩盖未解决的路径。Linux/macOS 需要在各自系统发布并运行，Windows 上的 Unix RID 还原或 IL 编译不算目标平台验收。

Windows 每控件属性、事件、行为分别达到 ≥98%，公开方法、构造、接口和受保护扩展按既有 [验收口径](docs/roadmap/coverage.md) 单列；Linux/macOS 对可移植部分分别达标。关键线程、生命周期、数据和资源问题不能藏在剩余 2% 中。Drawing 静态类型/成员数量不等于 WinForms 兼容率。

代码完成、指定场景运行通过、完整兼容验收分别记录。实施阶段允许在明确范围实现并通过必要编译/轻量检查后继续下一任务，复杂 UI、设计器和设备验证集中后置；未验证的功能不得提前计入兼容率。

## 与既有规划的衔接

| 现有任务 | 按本需求推进的范围 |
| --- | --- |
| XPL-001 | 盘点公开类型、Designer、Font/Bitmap/Icon/ImageList 与实际资源；补齐缺少的 fixture |
| XPL-002 | 按 Windows 官方 / Linux 自有策略验证程序集身份、传递依赖、既有消费者及资源绑定 |
| XPL-003 | 同一静态布局和资源在所选 Unix 平台真实运行，覆盖中文、度量、图像、缩放与释放 |
| XPL-010～XPL-014 | 在基础契约和资源路线通过后推进原生控件、UI 投递、交互、各系统 AOT 与分发 |
| XPL-015 | 按现有控件顺序扩展，并逐平台记录兼容率与不支持项 |

本需求确立方向，不自动关闭上述任务，也不改变 [HANDOFF.md](HANDOFF.md) 的编码归属和当前批次。继续使用现有任务页、批次、验证记录和交接机制。

程序集方案与扩面分别使用以下进入条件，不额外建立调度或任务框架：

1. **依赖与身份：** 真实 Lumina.Forms 和扩展消费图在 Windows 两个目标、Linux 目标均选择唯一提供者；核查 BCL 转发、基类/接口、旧资源和至少一个预编译消费库。分别保存编译、运行与发布资产，并明确无 RID 的处理。通过这些检查后，才能冻结程序集完整身份。
2. **资源：** 原始 Designer/resx 保持设计器可用；真实 Font、PNG Bitmap、ICO、ResXFileRef 和 ImageList 资源通过实际 reader/converter 路径，不以 stub 或运行时程序集替换代替。
3. **Linux 运行：** 一个实际原生窗口中的字体/中文度量、图像与 Graphics 状态能够使用，自有资源可被 GTK4 控件消费；在 Linux 本机发布、运行 Native AOT 并检查释放。通过后再扩展兼容范围。

若同身份方案不能形成可维护的依赖、资源与 AOT 闭环，应明确修订程序集策略，采用独立身份并披露相关库重编译和资源迁移，不能靠空实现或全局运行时替换维持“透明兼容”说法。缺少设备时可以继续已授权的代码实施，对应运行门槛保留待验。

截至 2026-10-15，专项探针已有 Windows 官方 Drawing 的少量真实 Native AOT 运行、条件 NuGet 资产选择及同身份 stub 绑定证据；Linux/macOS 的完整自有 Drawing、真实原生资源适配和兼容验收仍待实施。stub 的加载成功不是 Font/Image/Graphics 功能已完成。
