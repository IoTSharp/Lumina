---
title: "LuminaForms 项目重新评估与实施建议（2026-10）"
permalink: /zh/luminaforms-reassessment-2026-10/
layout: default
---

# LuminaForms 项目重新评估与实施建议

评估日期：2026 年 10 月 15 日。代码基线：`b83f5340fe7a9893bd0a4857401031b7289e6930`。本次只更新评估、证据和规划，不修改运行库和 Demo 的实现。

后续细化已形成 [逐控件路线图](../roadmap/README.md)、[98%验收口径](../roadmap/coverage.md) 与 [三平台进入条件](../roadmap/platforms.md)。用户进一步授权仓库整合后，WPF适配器已移除，美化/注入项目已隔离到效果解决方案，当前去留以 [整合记录](../roadmap/project-consolidation.md) 为准；本报告首轮804类型清点与新清单的命名空间范围不同。

补漏审计已进一步核查全部公开控件继承范围，并补 TaskDialog、ToolStrip 溢出条目、旧菜单、表格行列/单元格和重要附属对象，见 [完整范围](../roadmap/scope-audit.md)。另以真实 .NET10.0.11 探针确认部分旧类型只保留签名、构造即抛异常；这些类型先对齐官方占位契约，恢复 .NET Framework 旧UI作为可选扩展，后续执行优先级以细化路线图为准。

## 1. 结论与继续投入的条件

**建议继续。项目解决的是有实际需求的迁移问题，但目前应定位为已经证明 Windows Native AOT 技术路线可用的早期 WinForms 兼容实现，尚未达到完整替换 WinForms 的成熟度。**

项目最有价值的承诺是：保留已有窗体代码、设计器工作方式和常用交互，以较小的引用与命名空间调整，获得可裁剪、可 Native AOT 发布的应用。能否兑现这项承诺，比新增视觉效果或累计多少个同名类更重要。

本次建议明确保留两个长期目标，并采用不同验收标准：

1. **Windows：完整对标固定版本 WinForms 的运行时 API 和行为，同时支持 Native AOT。** 先以 .NET 10 为基线，不因发布试点版本就放弃剩余兼容工作。
2. **Linux、macOS：以当地平台的原生工具包实现可移植的 WinForms 使用方式。** 保留托管控件模型和业务交互；Windows 消息、COM、ActiveX、HWND 等能力单独标明平台边界。

“任意现有 WinForms 程序、任意第三方二进制控件、任意运行时插件，不作调整就能 AOT 并原生跨平台”，不能作为可交付承诺。这里同时存在 .NET Native AOT 的运行时约束、程序集类型身份差异和操作系统能力差异，不是多补几个控件就能消除。[S1][S2][S7][S8]

可继续投入的判断依据：真实项目能否减少迁移改动，常见桌面工作流能否稳定运行，发布收益是否值得维护成本。当前没有用户迁移样本、付费意愿调查或性能基准，不能把技术可行性当作市场规模证明。

## 2. 为什么有价值，哪些场景不一定需要

### 2.1 价值成立的场景

- 企业已经有大量 WinForms 窗体和相关经验，重写界面的成本高。
- 配置工具、设备管理、串口工具、部署工具等希望减少发布体积和运行时安装要求。
- 业务需要保留 Windows 原生输入、窗口和系统集成体验。
- 希望以后迁移 Linux 或 macOS，但不希望先重写整套窗体和业务交互。

Native AOT 可以在发布时生成本机代码，应用无需目标机器预装 .NET 运行时；一般有启动与内存方面的潜在收益，但项目自己的收益必须实测。[S7]

这不等于普通 WinForms 不能独立部署。WinForms 可以自包含部署；“不需要预装 .NET”“单文件”“裁剪”“Native AOT”是不同能力。LuminaForms 应证明其兼容性与部署收益的组合，而不能把所有部署问题都归因于 WinForms 不支持 AOT。[S7]

### 2.2 价值未必足够的场景

- 现有应用稳定，已接受共享运行时或自包含部署，没有体积、启动或平台迁移诉求。
- 界面主要依赖商业报表、复杂表格、Windows 专属 ActiveX，基础控件替换只覆盖很少一部分应用。
- 应用核心依赖动态加载未知托管插件，无法接受 Native AOT 的闭世界约束。
- 全新项目可以自由选择 UI 模型，保留 WinForms 源码的价值不大。

因此，这是有针对性的基础软件机会；现阶段最合理的商业验证是找到 3～5 个愿意实际迁移的维护者，记录他们的窗体、控件、第三方依赖和阻塞点，而不是推测整个 WinForms 群体都会迁移。

### 2.3 可选技术路线的比较

| 路线 | 保留既有 WinForms 源码 | AOT 状态 | 原生跨平台 | 对本项目的启示 |
| --- | --- | --- | --- | --- |
| 官方 WinForms | 最高 | 标准流程仍有裁剪阻断；存在研究工作 | 官方定位 Windows | 作为行为与设计器基准 |
| 官方 WinForms + 社区 COM/AOT 方案 | 通常更接近原实现 | 有实验性实践，仍须验证具体依赖 | 不解决原生 Linux/macOS | 保留一份对照样例，定期评估是否可复用上游成果 |
| Lumina.Forms | 目标是源码平滑迁移；当前差距明显 | 本次 Demo 已产出原生二进制 | 尚无后端 | 主要投入应转向行为兼容和真实迁移 |
| Eto.Forms 等原生工具包方案 | 需要迁移 UI API | 本次未验证其 AOT 完整性 | 已有不同平台原生工具包实现 | 可参考原生映射经验，不能据此宣称 WinForms 完全兼容 |

社区 `WinFormsComInterop` 的 README 明确给出了 ComWrappers 与 AOT 实验路径，也列有适用边界。Eto.Forms 的 README 明确使用不同平台工具包，但其示例 API 也表明它不是 WinForms 引用替换方案。以上比较用于判断投入方向，不建议替换本仓库既定 UI 路线。[S12][S13]

如果微软未来提供稳定的 WinForms AOT，Windows 部分的差异化会缩小，源码迁移质量、原生跨平台和已有用户仍可能有价值。应定期重评，不必因为这个可能性停止当前验证，也不应把“微软永远不会实现”当作立项前提。

实现策略也应重新评估：WinForms 官方代码是 MIT 开源，布局、集合、绑定和事件规则不必全部凭印象重写。可以选一个具体兼容缺口，对比独立实现与适配上游纯托管逻辑的成本；能减少行为偏差的部分优先复用，按仓库现有政策保留来源说明。另以隔离样例持续比较社区对官方 WinForms 的 AOT 改造进展。这里不建议立即整体分叉，也不把真实 WinForms 运行时依赖引入 `net10.0` 的 Lumina 路径；先用工程结果决定哪些工作值得自己维护。[S1][S12]

## 3. 微软对 WinForms 的态度

### 3.1 仍在维护与现代化

官方仓库将 WinForms 定位为 Windows 桌面应用和业务系统的快速开发工具，强调兼容、设计器、High DPI、无障碍、性能、安全和异步。它没有承诺把 WinForms 演变成通用跨平台 UI。[S1]

.NET 10 的官方说明包含暗色模式、异步窗体与对话框、剪贴板、设计器及屏幕阅读器改进。查阅到的 .NET 11 文档已经介绍 RC 1 阶段的视觉样式、系统视觉设置、窗口显示和批量更新相关能力。这些是持续投入的证据，不能用“微软已经放弃 WinForms”解释项目机会。[S3][S4]

版本边界也应明确：本评估以 2026 年 10 月 15 日为观察时点；本机对照的是 .NET 10.0.11。读取到的 .NET 11 页面标注 RC 1，不能据此认定它是当日最新预发布版本，也不能把预发布能力当成已发布稳定契约。

### 3.2 AOT 有工作推进，但完整官方支持仍不能视为已交付

官方已知裁剪问题文档解释了 WinForms 对内建 COM 封送的依赖。`dotnet/winforms#4649` 仍是未关闭的裁剪兼容总任务；本次 API 查询看到其最近更新时间为 2026-09-14。`#9911` 则展示了开发者和运行时贡献者对 AOT 体积的研究。[S2][S5][S6]

本机还新建了一个只有 `Application.Run(new Form())` 的临时官方 WinForms 项目，设置 `net10.0-windows`、`UseWindowsForms=true`、`PublishAot=true` 后发布；SDK 10.0.400 返回 `NETSDK1175`，提示 Windows Forms 不支持或不推荐在启用裁剪时使用。没有添加压制限制的内部开关。该结果与官方文档一致。

结论是：**标准、稳定、完整的 WinForms Native AOT 支持仍有缺口；不能把它表述为技术上永远无法实现。** 实验能生成二进制，也不等于全框架及其生态已经得到生产支持。

### 3.3 跨平台不属于现有官方方向

官方 README 继续明确 Windows 定位。历史跨平台请求 `#3383` 中，贡献者说明过跨平台不在路线图内；2025 年的 `#4649` 讨论也再次区分了 Windows 与跨平台诉求。历史评论说明当时立场，当前判断以官方定位和已公布能力为准。[S1][S5][S14]

因此，LuminaForms 的机会更接近“为希望保留 WinForms 开发方式的用户提供另一条运行路径”，不宜定位为挽救一个已被弃用的框架。

## 4. 用户对 WinForms 的态度：有需求，分歧也真实存在

本次依据公开问题和使用者自述作定性分析，不提供未经调查的使用率、满意度或市场份额。

| 用户关注点 | 观察到的原始信号 | 对 LuminaForms 的要求 |
| --- | --- | --- |
| 保留成熟业务应用和快速开发效率 | 官方产品定位、既有应用维护者的具体需求 | 设计器、表格、绑定和事件行为优先 |
| 减小部署体积 | `#7149` 的配置工具、`#9911` 的体积研究 | 给同功能、同资源、同部署模式的实测对比 |
| 获得 AOT | `#4649` 在 2026 年仍有追问及参与意愿 | 正常构建、完整发布、运行验收都需要 |
| 对设计器体验、内存和跨平台不满 | `#4649` 的个别开发者自述 | 将这些视作要验证的需求，不能直接视作框架普遍缺陷 |
| 接受 Windows 专用定位 | 贡献者对相关跨平台请求的回应 | Windows 用户和跨平台用户不是完全相同的客户群 |

这些讨论支持“确实有一部分用户需要这个方向”，不能推出“所有用户都希望替换 WinForms”。最终使用者更关心键盘、输入法、窗口、数据和稳定性；AOT 本身不是他们可以直接体验的业务功能。[S1][S5][S6][S15]

## 5. 仓库现状：可复核的检查结果

### 5.1 已经形成的基础

- `Lumina.Forms` 使用 Win32 原生窗口、标准控件和 Common Controls，实际实现了相当一部分窗口、主题、布局和集合工作。
- `NativeFormsDemo` 保持 `net10.0-windows` / `net10.0` 双目标；前者真实使用 WinForms，后者使用 Lumina.Forms，并启用 Native AOT。
- Demo 的主要静态 UI 仍在 `.Designer.cs` 和 `.resx`，这对设计器工作流有价值。
- 已有集合、自动缩放、嵌套容器、菜单和主题测试，具备建立回归基线的起点。
- Native interop 主要封装在库内，消费方不需要直接处理大部分窗口过程和封送。

这已经超出只展示窗口的技术草稿；同时，它还不能被描述为已完成的 WinForms 替代产品。

### 5.2 本次构建与运行检查

| 检查 | 结果 | 能证明的范围 |
| --- | --- | --- |
| `dotnet test Lumina.Tests/Lumina.Tests.csproj -c Release` | 77 通过、0 失败、0 跳过 | 现有测试通过；包含视觉效果、主题和局部兼容用例 |
| `dotnet build NativeFormsDemo/NativeFormsDemo.csproj -c Release` | 两个目标成功，本次输出 178 个警告 | 当前 Demo 源码能分别编译；不等于 VS 设计器往返验收 |
| `dotnet publish NativeFormsDemo/NativeFormsDemo.csproj -f net10.0 -r win-x64 -c Release` | 成功，存在 `IL2026`、`IL3053` 等警告 | 能执行 Native AOT 发布，尚非无警告发布基线 |
| 原生文件结构 | EXE 没有 CLR Header | 输出确为原生程序；不能由此推断全部功能正常 |
| 本次 EXE 大小 | 4,083,712 字节，约 3.89 MiB | 仅此 Demo 的可执行文件；发布目录还包含 PDB/XML |
| AOT Demo 短时隐藏启动探针 | 未完成验收 | 枚举到托盘、GDI+、IME 辅助窗口，未枚举到主窗；测试最终终止该进程 |
| 小型控件运行探针 | 成功创建原生窗体并复现多项缺口 | 使用本机 JIT 运行的库，不能冒充完整 AOT UI 回归 |

AOT 隐藏启动观察时间很短，也没有交互检查，因此这里只记录“主窗启动尚未通过本次探针”，不推断其根因，不宣称所有环境均会失败。可见主窗、图片资源、菜单交互、关闭退出需要单独复核。

`IL2026` 涉及启用了 `CustomResourceTypesSupport` 的自定义资源读取；`IL3053` 涉及 `System.ComponentModel.TypeConverter` 的 AOT 分析。项目文件中的“预序列化资源”注释不构成“运行时完全不需要反射”的证明。需要逐条定位实际使用路径，不建议简单压制整个程序集的警告。

证据目录：[评估证据](../assessments/2026-10-15/README.md)。本次没有验证 Linux/macOS，没有验证 win-arm64，没有完成 Visual Studio 可视设计器操作，也没有完成 AOT 全部控件交互验收。

### 5.3 全量类型摸底：所有控件是否都已实现

**没有。既存在整个控件族缺失，也存在已有同名类没有对应关键行为的情况。**

本次通过反射读取本机 .NET 10.0.11 的 `System.Windows.Forms` 和 `System.Windows.Forms.Primitives`，限定 `System.Windows.Forms` 命名空间，记录导出的类型；包含嵌套类型，不包含完整设计器程序集、Drawing、Printing 或应用第三方依赖。

| 口径 | 数量 | 解释 |
| --- | --- | --- |
| 基线导出类型 | 804 | 含枚举、委托、辅助类型与嵌套类型，不能等同于控件数量 |
| Lumina.Forms 命名空间导出类型 | 110 | 包含 Lumina 自身主题类型 |
| 仅映射命名空间后，基线中有同名对应类型 | 97 | 类型名称命中，不表示类型关系、签名或行为兼容 |
| 基线非抽象、顶层 `Control` 类型 | 61 | 包含基础类、内部用途的公开控件和已过时类型 |
| 其中在 Lumina 有同名类型 | 32 | `Control` 在 Lumina 是抽象的，`ContextMenuStrip` 在 Lumina 是 Component，均不能视为同等视觉控件 |
| Lumina 实际非抽象视觉控件类，计入 Form | 30 | 清单见下一节；均不在本次评估中标记为“全部成员完整验证” |

因此不应把 `32 / 61`、`97 / 804` 或相同名称成员的比值称为“完成度”。它们不反映业务权重，也不衡量事件、输入和生命周期。

完整类型清单在 [API 清点 CSV](../assessments/2026-10-15/winforms-api-inventory.csv)。其中记录的成员数只比较各类型自身声明的 public/protected 成员，不沿继承链归并，也不全面比较 virtual、接口、可选参数和默认值。它是差距摸底，尚不是发布合规工具。

### 5.4 已有的 30 个视觉控件：逐项评估

以下“部分实现”表示已有真实代码，不表示达到完整 WinForms 行为。未对每个成员、每种平台和每种输入方式做穷举运行验证。

| 控件 | 当前判断 | 进一步完善的核心工作 |
| --- | --- | --- |
| `Form` | 原生窗口、事件、主题、部分布局已实现 | 回归 Control/ContainerControl 继承体系；模态、所有者、关闭与退出、常用窗口属性及事件 |
| `Button` | 原生按钮与 Click/PerformClick 已实现 | ButtonBase/IButtonControl、DialogResult、默认按钮、键盘、图像和默认值 |
| `Label` | 原生文本标签及部分样式 | 文本测量、AutoSize、对齐、省略、助记键、图像 |
| `TextBox` | 原生编辑框、文本及部分选择功能 | TextBoxBase；密码、提示、滚动等属性的真实应用；句柄重建、快捷键与输入事件 |
| `CheckBox` | 原生两态及 CheckedChanged | 三态、CheckStateChanged、Appearance、按钮基类、事件顺序 |
| `RadioButton` | 原生按钮及选中状态 | 容器分组、AutoCheck、Tab/方向键、相互取消及事件顺序 |
| `ComboBox` | 原生列表和部分选择/集合 | ListControl、数据绑定、DisplayMember/ValueMember、编辑与选择事件、排序和绘制 |
| `ListBox` | 原生列表和部分选择/集合 | 多选、选中集合、数据绑定、项高度、绘制和变更同步 |
| `GroupBox` | 子控件、消息转发及主题 | 完整父子关系、助记键、焦点导航、自动尺寸与布局 |
| `Panel` | 容器和部分布局 | ScrollableControl、真实滚动、边框、Dock/Anchor、布局事件 |
| `UserControl` | 复合容器基础 | ContainerControl 的焦点/验证/ActiveControl、Load、继承与自动缩放行为 |
| `FlowLayoutPanel` | 方向、换行、Padding/Margin 布局 | FlowBreak、首选尺寸、AutoSize、滚动、RTL、复杂嵌套与重入 |
| `TableLayoutPanel` | 行列、样式、部分自动布局和列跨度 | RowSpan、完整行列集合、定位/扩展属性、跨格占位、增长策略与首选尺寸 |
| `SplitContainer` | 双面板、拖动、折叠、约束已有较多实现 | 面板应对标 SplitterPanel；键盘、布局/DPI 和事件契约 |
| `Splitter` | 相邻区域尺寸与鼠标拖动 | 事件、边界、取消、停靠顺序与键盘可达性 |
| `TabControl` | 原生页签、选择与页布局 | TabPages 专用集合、选择取消事件、图像/绘制、键盘、设计器成员 |
| `TabPage` | 容器及页布局基础 | 页签归属、专用属性、句柄、主题和滚动行为 |
| `PictureBox` | 有实际图像加载、缩放与原生位图工作 | 资源/AOT、异常、图像所有权、异步加载、动画和事件；是跨平台 Drawing 依赖点 |
| `ProgressBar` | 原生控件和 Value | Minimum/Maximum/Step、Increment/PerformStep、Marquee/Style、越界契约 |
| `DateTimePicker` | 原生窗口及主题 | Value 目前仅字段；补日期读写通知、格式、范围、勾选及事件 |
| `MonthCalendar` | 原生日历、尺寸和主题 | 日期/范围读写、选择事件、粗体日期、多月和区域设置 |
| `LinkLabel` | 在 Label 上增加 LinkVisited | 链接范围、点击事件、键盘、访问状态和真实链接行为 |
| `MaskedTextBox` | 在 TextBox 上增加 Mask 属性 | 掩码解析、校验、提示字符、输入文化及事件；当前不执行掩码 |
| `NumericUpDown` | 文本框派生类、Value 及初始化钳制 | UpDownBase、原生增减交互、输入解析、范围、精度、增量与 ValueChanged |
| `RichTextBox` | 多行 TextBox 派生类 | 真正 RichEdit 后端、RTF、选区格式/流与事件；当前原生类是 `Edit` |
| `TreeView` | 原生树窗口与独立托管节点集合 | 节点插入/删除/更新尚未连通原生树；补选择、展开、勾选、图像及事件 |
| `ListView` | 原生列表窗口、主题、独立项/列集合 | 先修复项构造异常，再接通列/项/子项原生同步、视图、选择、虚拟模式等 |
| `ToolStrip` | 条目及部分宿主控件、布局已有实际实现 | 正确继承/集合、溢出、焦点键盘、Renderer、宿主事件双向同步 |
| `MenuStrip` | 原生菜单与主题绘制已有实现 | 助记键、快捷键、合并、MDI、焦点切换、菜单生命周期 |
| `StatusStrip` | 状态条目宿主及部分布局 | Spring、尺寸手柄、溢出、自动尺寸与条目生命周期 |

### 5.5 条目、对话框与非可视组件

| 类型或族 | 当前判断 | 主要差距 |
| --- | --- | --- |
| `ToolStripItem`、`ToolStripButton`、`ToolStripLabel`、`ToolStripSeparator` | 有实现 | 基类成员、事件、绘制、默认值、集合与父子关系 |
| `ToolStripDropDownItem`、`ToolStripDropDownButton`、`ToolStripMenuItem`、`ToolStripSplitButton` | 有部分菜单路径 | 与官方继承体系及 SplitButton 独立按钮/下拉区、事件模型不等价 |
| `ToolStripTextBox`、`ToolStripComboBox`、`ToolStripProgressBar`、`ToolStripStatusLabel` | 有条目和部分宿主映射 | 不能只看小文件判为空壳；仍需验证属性变化、用户操作和宿主状态双向同步 |
| `ContextMenuStrip` | 实际菜单行为存在，但继承 Component | 官方继承 ToolStripDropDownMenu，是 Control；完整成员与事件体系不兼容 |
| `OpenFileDialog`、`SaveFileDialog`、`FolderBrowserDialog`、`ColorDialog`、`FontDialog` | 有实际原生调用 | 当前多为 sealed IDisposable 类，缺 CommonDialog/FileDialog 继承与 Component 契约；owner 类型及默认值也要对照 |
| `MessageBox` | 已有系统对话框调用和部分重载 | IWin32Window、选项、默认按钮、Help、完整重载/枚举 |
| `NotifyIcon` | 有真实托盘窗口与图标操作 | 气球提示、Explorer 重启恢复、事件、句柄与图标生命周期仍需覆盖 |
| `ToolTip` | 目前为控件到文本的字典 | 未见原生提示窗口或显示逻辑；Set/Get 回读不等于能够显示提示 |
| `TreeNode`、`ListViewItem`、`ColumnHeader` 和集合 | 有基本数据结构 | 归属、索引/键、Add 返回值、修改通知、原生映射、序列化和嵌套类型签名 |

这些类型不是次要细节：WinForms 的 `components` 容器、资源、条目集合和 Dispose 链正是大量设计器生成代码依赖的部分。

### 5.6 基线中尚无同名实现的全部 29 个非抽象顶层控件

该清单直接来自本次 61 个类型的对照，不意味着 29 个都应同等优先实现。

| 族 | 缺失类型 |
| --- | --- |
| 基础与容器 | `ContainerControl`、`ScrollableControl`、`SplitterPanel` |
| 常用输入与滚动 | `CheckedListBox`、`DomainUpDown`、`TrackBar`、`HScrollBar`、`VScrollBar` |
| 数据与属性 | `DataGridView`、`DataGridViewComboBoxEditingControl`、`DataGridViewTextBoxEditingControl`、`BindingNavigator`、`PropertyGrid` |
| 工具条容器与弹出 | `ToolStripContainer`、`ToolStripContentPanel`、`ToolStripDropDown`、`ToolStripDropDownMenu`、`ToolStripOverflow`、`ToolStripPanel` |
| 打印 | `PrintPreviewControl`、`PrintPreviewDialog` |
| Windows 集成 | `MdiClient`、`WebBrowser`、`WebBrowserBase`、`ThreadExceptionDialog` |
| 历史控件 | `DataGrid`、`DataGridTextBox`、`StatusBar`、`ToolBar` |

除此之外，`ButtonBase`、`TextBoxBase`、`ListControl`、`UpDownBase`、`CommonDialog`、`FileDialog`、`ToolStripControlHost` 等基础类型尚有缺口；`BindingSource`、`ErrorProvider`、`HelpProvider`、`ImageList`、WinForms `Timer`、`PrintDialog`、`PageSetupDialog` 及大量事件参数/接口/集合尚未齐备。Printing/Drawing 的完整 API 不在上述 804 类型口径内，也需要另外评估。

旧类型中有些已在现代 WinForms 中过时，优先级可以靠后；如果要宣称字面意义的全量兼容，仍须逐项记录处理方式，不能从清单中删除后称为“全部完成”。WebView2、商业表格和报表不是 WinForms 内置控件，也不能把它们混入“内置控件已全部覆盖”的统计。

## 6. 需要优先解决的兼容性问题

### 6.1 先对齐类型关系，避免越补越难迁移

官方常见继承关系是 `Component → Control → ScrollableControl → ContainerControl → Form/UserControl`；不同控件还有自己的标准基类。当前 `Form : IDisposable` 与 `Control : IDisposable` 相互独立，`Control` 还是抽象类，顶层子控件的 `Parent` 会是 null。

直接影响包括：`Control c = form;` 无法编译；共享控件遍历、Component 容器、派生控件和窗口所有者模式难以复用。应该使用 WinForms 已有类型补齐继承关系，保留历史公开类型必要的过渡兼容，不再扩展一套平行的公共控件概念。

还应核对容易被忽略的签名：当前 `Focus()` 返回 void，而 WinForms 返回 bool；`Application.Run(Form)` 当前返回 int，而标准入口返回 void；`SetHighDpiMode` 当前返回 void 且没有实现，标准 API 返回 bool。`Controls` 所在层级、集合嵌套类型、事件委托类型同样属于兼容面。

### 6.2 UI 线程与消息循环是可靠性的基础

当前 `Control.InvokeRequired` 固定为 false；`Invoke` 直接 DynamicInvoke；`BeginInvoke` 通过 Task.Run 执行；部分 InvokeAsync 重载直接在调用线程执行。Form 中也有类似实现。

本次探针记录：UI 线程编号 2，后台线程编号 4，后台调用 Invoke 后回调仍在线程 4，InvokeRequired 仍返回 false。这不是局部 API 缺失，而是会影响网络、数据库、串口和设备更新的运行行为。

应首先实现真实 UI 线程投递、句柄建立/销毁边界、异常传播、取消与关闭竞态；随后对标 ApplicationContext、主窗关闭、模态循环、消息预处理和 WindowsFormsSynchronizationContext。DynamicInvoke 的出现本身不等于 AOT 必然失败，真正需要修复的是线程语义并验证所用委托路径。

### 6.3 把集合与属性真正接到控件行为

本次复现的最短证据：

- `TreeView.Nodes.Add(new TreeNode("node"))` 后 Show：托管 Nodes.Count 为 1，`TVM_GETCOUNT` 为 0。
- `new ListViewItem("row")` 抛 NullReferenceException。构造函数先调用 Text setter，setter 使用尚未初始化的 SubItems。
- `RichTextBox` 的原生窗口类为 `Edit`，不是 RichEdit；SelectionColor 目前只保存字段。
- `MaskedTextBox { Mask = "0000", Text = "abcd" }` 保留 `abcd`；掩码没有执行。
- `NumericUpDown { Maximum = 10, Value = 20 }` 的 Value 保持 20；范围没有在普通赋值路径执行。

掩码程序赋值、数值越界的确切异常和接受规则仍须与 WinForms 差分测试对照，不能用上述单一探针代替全部契约。但这些结果足以否定“已完整实现”。

`Anchor`、`AutoScroll`、`AutoEllipsis` 和基础 `AutoSize` 的声明，以及 `DateTimePicker.Value`、TextBox 的部分属性也不能仅因可以回读就标为实现。应验证句柄前设置、句柄后设置、用户原生输入、句柄重建四个方向。

### 6.4 布局、输入、资源和释放必须与扩控件同步

需要持续补齐：

- Dock/Anchor 与 Z-order、SuspendLayout 计数、Layout 事件、PreferredSize、AutoSize、滚动、动态父子关系。
- Font/DPI 缩放、PerMonitorV2、跨屏、文字缩放、中文与不同字体、RTL；已有自动缩放测试不代表这些已全部覆盖。
- Tab/Shift+Tab、助记键、Enter/Escape、AcceptButton/CancelButton、Validate/Validating、KeyPreview、输入法与文本组合。
- `.resx` 图像、图标、资源文化、多语言、ComponentResourceManager.ApplyResources、设计器重新保存。
- 标准 Paint/OnPaint、WndProc/Message、CreateParams、ControlStyles、owner draw 等派生扩展面；这是自定义 WinForms 控件迁移的基础。
- 父子控件、窗口、菜单、托盘、字体、位图、GDI 对象、回调、事件订阅的所有权和 Dispose 顺序。
- 无障碍语义、键盘可用性、高对比度与屏幕阅读器。原生控件能提供基础能力，但不自动证明框架的组合、命名、状态和事件正确。

### 6.5 企业业务兼容的关键是数据能力

DataGridView、BindingSource、Binding、BindingContext/CurrencyManager、ListControl、DataBindings、ErrorProvider 是一个关联工作包，不能只实现显示表格外观。

具体需要：列表与对象绑定、属性变更通知、当前项、Format/Parse、更新模式、校验/提交/取消、编辑控件、排序、选择、滚动与虚拟模式，并保持习惯用法。初期优先采用能静态分析的现有 ComponentModel 机制；出现可复现 AOT 缺口时，再补生成式描述符或静态注册，不要求使用者改成自创绑定 DSL。

DataGridView 和 PropertyGrid 等也包含大量框架逻辑，不能全部理解为“给一个原生窗口类包一层 API”。这部分很可能是 Windows 完整兼容的主要工程量之一。[S1][S16]

### 6.6 构建链和包消费也属于产品

当前 CI 的 AOT smoke publish 目标是 `Lumina.NotepadDemo`，并不是 `NativeFormsDemo` 的完整控件集合。应增加后者显式的 `-f net10.0 -r win-x64` 发布与启动检查，同时保留双目标编译和设计器验收。

`Lumina.Forms` 还未声明 `IsAotCompatible`，应在核清自身依赖之后启用库级分析。NuGet 消费测试也需要覆盖 buildTransitive、资源和 analyzers；现有 Demo 把分析器项目作为普通 ProjectReference，不能只凭项目在解决方案里就认定 LNF 规则在用户编译时运行。应验证实际 Analyzer 项、包目录及一份会触发诊断的消费样例。

`windows6.1` 平台注解和识别旧 Windows 视觉风格的代码，不等于 .NET 10 应用支持 Windows 7。支持 OS、CPU 和发布工具链需要按实际运行基线独立写清。

## 7. 原生跨平台：可以推进，但要明确“完整”的含义

### 7.1 哪些可以保持，哪些无法原样复制

窗体、按钮、文本、选择、集合、布局、事件、绑定和 IDisposable 等托管契约，可以通过平台实现继续保持。Eto.Forms 等工程证明了使用不同平台原生工具包承载统一控件模型的可行性，但没有证明 WinForms 全行为一一对应。[S13]

HWND、WndProc/WM_*、SendMessage、COM/ActiveX、Windows Shell、GDI HDC 则具有 Windows 语义。`nint` 在不同平台都能存地址，并不意味着 GTK 对象指针或 NSView 指针可以当成 HWND。需要调用 Win32 的既有应用代码仍需平台条件或迁移。

因此：**Windows 可以追求固定 WinForms 基线的完整兼容；跨平台可以追求可移植契约的完整实现；纯原生控件不能同时承诺 Windows 专属互操作也在其他系统上完全等价。**

还有一个容易误判的前提：WinForms 自身也不全是系统标准控件。DataGridView、PropertyGrid 和部分 ToolStrip 逻辑需要框架实现。在 GTK/AppKit 上使用原生表格或容器，并不能消除其中的编辑、绑定、选择、绘制和事件语义工作。

### 7.2 原生工具包映射建议

这是原型候选，不是宣布已经实现或已经选择了额外产品依赖。[S9][S10][S11]

| WinForms 概念 | Windows | Linux 候选 | macOS 候选 | 主要差异 |
| --- | --- | --- | --- | --- |
| Form | Win32 顶层窗口 | GtkWindow / GtkApplicationWindow | NSWindow / NSPanel | 生命周期、模态、窗口管理 |
| Button / CheckBox / RadioButton | BUTTON | GTK 对应按钮控件 | NSButton | 分组、默认动作、键盘习惯 |
| TextBox | EDIT | GtkEntry / GtkTextView | NSTextField / NSTextView | 多行、输入法、文本选择 |
| ComboBox / ListBox | Win32 列表与组合框 | GtkDropDown / GtkListView 等 | NSComboBox / NSTableView 等 | 可编辑组合框通常不是简单一对一 |
| TreeView / ListView | Common Controls | GtkTreeListModel / GtkColumnView 等 | NSOutlineView / NSTableView | 模型、虚拟化、选择与列同步 |
| DataGridView | 框架表格行为 | 原生表格 + 兼容编辑/绑定逻辑 | NSTableView + 兼容编辑/绑定逻辑 | 没有现成的完全等价控件 |
| 菜单与系统对话框 | Windows 菜单、Common Dialog | GTK/GIO，按部署环境验证 portal | NSMenu、NSOpenPanel 等 | 菜单位置、文件权限、异步模式 |
| PictureBox / Font / Graphics | System.Drawing.Common/GDI+ | GTK 图像/文字/绘制能力 | NSImage/NSFont/CoreGraphics 等 | 公共类型身份与绘图 API 是独立问题 |

Linux 并不存在一个覆盖所有桌面的唯一“系统控件库”。GTK 是合理的原生工具包候选，但 GNOME 与 KDE 的外观预期并不相同。首轮应选择一个实际客户发行版和一个工具包版本，验证 Wayland/X11、文件对话框和依赖部署；不宜一开始同时维护 GTK3、GTK4、Qt 多个后端。[S9][S10]

macOS 使用 AppKit 才符合此处“原生 macOS 控件”的方向；它涉及主线程事件循环、Objective-C ABI/回调、对象所有权、菜单和应用打包。互操作可以采用 AOT 友好静态绑定或小型原生桥接，但必须在真实 Mac 上验证。不能因为底层 C# 能编译，就宣称 macOS UI 已得到验证。[S7][S11]

### 7.3 当前跨平台首要阻塞：System.Drawing.Common

`Lumina.Forms.csproj` 当前直接依赖 System.Drawing.Common 10.0.0，公开 API 还使用 Font、Image、Icon 等类型。该包在现代 .NET 中属于 Windows 专属，非 Windows 开关已在 .NET 7 移除。[S8]

Point、Size、Rectangle、Color 等值类型与 Font/Image/Graphics 不是同一类问题。前者可以继续评估复用；后者即使不调用 Win32，也不能靠目标改成 `net10.0` 就跨平台。

必须先完成一个小型验证：保留调用方常见写法时，字体、图片、资源和自绘需要怎样的兼容类型或绑定策略，程序集身份会怎样变化，是否与现有 System.Drawing 引用冲突。**本评估不预先修改公共绘图 API，也不建议在此时从零重写完整 GDI+。** 这个结果会决定原生跨平台能承诺多大范围。

### 7.4 最小实施边界

继续保留 Lumina.Forms 作为消费入口。共享控件状态、集合和兼容行为，在内部拆分平台窗口、事件循环、输入、资源等实现；平台差异留在库内。内部实现可先用平台文件/局部类型和少量接口，不新增公共 UI 语言、场景树或另一套控件模型。

跨平台原型应在基础类型与 UI 线程模型初步稳定后尽早做，避免 Windows 深化后才发现公共 Font/Image 或 Handle 的假设无法成立。原型不代表三套完整控件库同时开工。

Native AOT 仍包含所需的运行时与 GC；Linux 的 GTK 等原生依赖也需要系统提供或随应用部署。“原生 AOT”“使用原生控件”“只有一个无需系统依赖的文件”不应合并成同一项承诺。[S7][S9]

## 8. 路线图：完整目标保留，按证据推进

当前执行路线见仓库根目录 [ROADMAP.md](../../ROADMAP.md)。顺序建议如下：

| 阶段 | 主要结果 | 阶段完成的证据 |
| --- | --- | --- |
| M0：基线与阻塞 | API/控件清单、支持等级；修复项构造、错误线程投递；复核 AOT 主窗启动 | 有可复现失败用例及修复验证，发布产物可启动并正常退出 |
| M1：共同基础 | Component/Control/ScrollableControl/ContainerControl/Form 关系、消息/模态、布局/DPI 与资源链 | 同一场景分别在真实 WinForms 与 Lumina 跑，比较可观察结果 |
| M2：已有控件做实 | 30 个已有视觉控件及菜单/对话框/组件完成常用行为 | 集合与原生状态双向一致；输入、生命周期及 AOT 回归 |
| M3：业务迁移主干 | DataGridView + 绑定体系、常见缺失输入控件、ImageList/ErrorProvider/Timer | 至少 3 个不同类型真实应用迁移并完成实际业务操作 |
| M4：Windows 完整基线 | 完整类型、成员、事件/默认值、打印、绘制扩展、MDI/Windows 集成与历史项逐项闭环 | 基线清单无“未评估”；差异与例外公开，不能把不兼容项算成完成 |
| M5：原生平台扩展 | 根据原型结果交付 Linux、macOS 后端及支持矩阵 | 在对应系统执行 Native AOT、输入法、无障碍、打包和真实业务回归 |

M1 之后安排一个有时间上限的跨平台小型原型，先选有真实需求和验证机器的一种平台。M4 是完整兼容目标，不等于只做 M2 的常用控件后便更换目标名称。

### 首轮 90 天工作建议

以下是 2～3 位熟悉 .NET/Win32 的全职工程师的工作顺序建议，不是已确认人员或发布日期承诺。若只有一位维护者，按相同验收条件顺延。

- 第 1～2 周：建立可持续清点工具；修复首批确定性阻塞；明确 Demo AOT 启动问题与资源警告来源；收集候选迁移应用。
- 第 3～6 周：优先打通基础继承、UI 线程、模态/关闭、父子集合和布局链；保留设计器生成代码，建立针对性差分测试。
- 第 7～12 周：优先让树/列表、文本/掩码/数值、日期和 ToolTip 等现有声明产生真实行为；完成至少一个真实小型应用的 AOT 迁移；基础稳定后开展一个小型原生跨平台验证。
- 第 90 天：依据实际迁移改动量、阻塞类型、每周关闭的兼容项与原型结果，重估 DataGridView/绑定及完整基线的人员与排期。

完整 WinForms 运行时兼容加两套原生后端，应按多版本工程规划；目前没有足够的数据为“全量完成”给出可信固定日期。先把前三个月做出可验收的结果，再用实际速度排剩余工作，比现在给一个精确但没有依据的年月更有用。

### 发布验收建议

1. API 清单必须区分“未实现、仅声明、部分行为、已验证场景”，平台与 AOT 状态单独记录。
2. 每项宣布支持的行为都覆盖句柄前后设置、用户输入、句柄重建和释放；不要求机械地为每个转发属性写重复测试。
3. 同一 .Designer.cs/.resx 经过真实 WinForms 设计器保存后，两目标仍编译；AOT 路径不引用真实 WinForms。
4. UI 线程、关闭、模态、输入法、键盘、DPI、资源与 Dispose 是持续检查项，不推迟到最终版。
5. 自身代码不遗留未解释的 AOT/裁剪警告；外部限制逐项说明、验证和跟踪，不以全局压制换取“零警告”。
6. 冷启动到可操作、工作集/私有内存、峰值、EXE 与完整分发目录分别量测，使用同功能应用作对照。
7. 标准控件兼容、第三方源码控件兼容、第三方预编译程序集兼容分开报告。程序集命名空间不同，后者不能靠全局 using 自动获得。

## 9. 本次建议的取舍

继续保留已有主题与视觉能力，把新增工作首先放在线程、继承、布局、资源和数据行为上。保留固定双目标 Demo 和设计器静态布局。优先修通现有控件，不以大量新增同名类替代行为工作。用真实迁移项目决定缺失控件的先后，但维护完整基线的剩余清单。

微软继续维护 WinForms 与本项目有价值可以同时成立。跨平台原生 UI 也可以作为长期方向成立，但 Windows 专属语义不能隐去。项目下一步最需要的是可以反复验证的兼容承诺，而不是扩大未经验证的支持声明。

## 来源与证据

以下均为原始文档、项目仓库或直接参与者讨论，查询时间为 2026-10-15。历史讨论注明其时间边界，不作为官方新版本交付承诺。正文使用概括，不把个别意见视为用户总体调查。

- [S1：WinForms 官方 README 与产品定位](https://github.com/dotnet/winforms/blob/main/README.md)
- [S2：微软已知裁剪不兼容项](https://learn.microsoft.com/en-us/dotnet/core/deploying/trimming/incompatibilities)
- [S3：WinForms .NET 10 新功能](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/whats-new/net100)
- [S4：WinForms .NET 11 文档，本次读取页面标注 RC 1](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/whats-new/net110)
- [S5：#4649，WinForms 裁剪兼容总任务及用户讨论](https://github.com/dotnet/winforms/issues/4649)
- [S6：#9911，WinForms PublishAot 体积研究](https://github.com/dotnet/winforms/issues/9911)
- [S7：微软 Native AOT 部署、平台与限制](https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/)
- [S8：System.Drawing.Common 非 Windows 开关移除](https://learn.microsoft.com/en-us/dotnet/core/compatibility/core-libraries/7.0/system-drawing)
- [S9：GTK 的 Wayland 后端](https://docs.gtk.org/gtk4/wayland.html)
- [S10：GTK4 迁移与控件窗口/事件模型](https://docs.gtk.org/gtk4/migrating-3to4.html)
- [S11：Apple AppKit](https://developer.apple.com/documentation/appkit)
- [S12：WinFormsComInterop 项目及限制](https://github.com/kant2002/WinFormsComInterop)
- [S13：Eto.Forms 原生工具包路线](https://github.com/picoe/Eto)
- [S14：#3383，历史跨平台请求](https://github.com/dotnet/winforms/issues/3383)
- [S15：#7149，真实小工具的裁剪需求](https://github.com/dotnet/winforms/issues/7149)
- [S16：#6459，WinForms 自定义绘制与 DataGridView 讨论](https://github.com/dotnet/winforms/issues/6459)
- [本地 API 快照、原生运行探针、启动探针与方法边界](../assessments/2026-10-15/README.md)
