# 本轮验证与限制

日期：2026-10-15；实现基线：`b83f5340fe7a9893bd0a4857401031b7289e6930`。本轮修改是路线图/审计工具以及用户授权的项目整合，没有补齐缺失控件实现。以下验证范围分别记录。

| 项目 | 结果 | 范围与限制 |
| --- | --- | --- |
| 主线构建 | ✅ | `dotnet build Lumina.slnx -c Release`：0错误，本次314警告；包含固定双目标NativeFormsDemo，未做VS可视设计器往返 |
| 效果线构建 | ✅ | `dotnet build Lumina.Effects.slnx -c Release`：0错误，本次1警告；验证隔离后仍能构建，不代表注入运行验收 |
| 现有测试 | ✅ | `dotnet test Lumina.Tests/Lumina.Tests.csproj -c Release --no-build`：77通过、0失败、0跳过；不是全量WinForms行为认证 |
| 主线包冒烟 | ✅ | Core、WinForms适配器、Forms、Analyzers四包本地pack成功；没有向包源发布，分析器真实消费触发仍待验收 |
| 固定API导出 | ✅ | Microsoft.WindowsDesktop.App.Ref 10.0.11，1095类型、61,240条有效成员行、9,047条接口消费契约；包含继承重复，不能当去重API总数或支持率 |
| 显式/继承接口复核 | ✅ | BindingSource初始化/取消新增、BindingContext集合接口已纳入；公开接口筛选排除WinForms内部接口；接口自身继承也展开 |
| 控件范围复核 | ✅ | 固定基线69个公开Control（62具体/7抽象）、14个ToolStripItem、9个CommonDialog、4个Menu、113个Component并集、13个TaskDialog对象及21个DataGridViewElement均有独立页；含嵌套检查，见[补漏审计](scope-audit.md) |
| 默认值探针 | ✅ 仅参考端 | [46项观测与探针源码](../assessments/2026-10-15/data-control-defaults.json)，JIT真实WinForms参考端；未证明Lumina相应属性正确 |
| 历史占位复核 | ✅ 仅参考端 | [固定10.0.11探针与完整源码](../assessments/2026-10-15/legacy-control-probes.json)：12个具体旧类型构造PNSE，Menu抽象；标准异常与可选Framework历史UI分开规划 |
| 文档结构检查 | ✅ | 187组控件/API页面；1948条控件正文任务加49条FND/REP/CNS/XPL公共任务，共1997条索引；1095类责任映射；389份Markdown检查无本地断链、重复任务ID、损坏表格或NUL；1967条待办、28条局部实现、2条待研究，0条完整兼容验收通过 |
| 同名类型纠错 | ✅ 仅清单/索引 | 完整命名空间/嵌套路径绑定[独立页](api/page-map.csv)，纠正VisualStyleElement.Button等同名类造成的索引误计；附件身份、元数据和索引成员数/候选存在状态均校验，不把这项当作控件实现 |

构建/pack的详细日志在本机忽略目录 artifacts/compatibility-inventory，命令可重复运行；本页保留结果，日志不属于发布物。警告数依赖增量构建情况，不等同于新的警告数量。

文档检查复现：`./tools/compatibility/Validate-Roadmap.ps1 -ExportTasks`；自动排序导出[正文任务台账](tasks.csv)。成员和接口的运行状态全部为unverified；未将资料编写、签名命中或参考端探针计为控件行为支持。

实施接续已补根目录 [HANDOFF.md](../../HANDOFF.md)、首批任务及 AGENTS 接续规则；任务生成器分开输出代码状态与兼容验收状态。用户要求前期以代码完成推进，必要构建/轻量检查照常执行，复杂真机测试后期先 Windows、再统一 Linux，不据此提前宣称98%达标。

## 批次 001 · 2026-10-15 实施结果

本批已实施运行库及分析器修复，完整记录见 [批次 001](batches/001.md)。用户进一步确认所有仓库项目仅支持 .NET 10；12 个项目、15 个目标声明已检查为 net10.0/net10.0-windows，分析器已改 net10.0。以下代码检查不等于完整兼容验收。

| 项目 | 结果 | 范围与限制 |
| --- | --- | --- |
| 主线 Release / Demo 双目标 | ✅ | SDK 10.0.400，0 错误；本次增量 319 警告，包含实际启用后的 LNF003 与既有平台/文档警告；未做设计器往返 |
| GlassMorphDemo 引用集成 | ✅ | 该项目本批也修改 Analyzer 引用，单独 Release 编译 0 错误、1 条 LNF003；不代表效果运行验收 |
| 现有与新增测试 | ✅ | 92 通过，0 失败/跳过；新增 9 项 ListViewItem 托管回归、6 项启动签名/DPI 无副作用检查 |
| DPI 隔离对照 | ✅ 指定轻量范围 | 本机 WinForms 10.0.11，14 组 fresh/宿主已设 × 有效/无效枚举，比较首次/重复调用、回读和异常；首窗回调、跨屏/真实缩放、旧系统回退待设备验收 |
| NuGet 分析器真实消费 | ✅ | Forms 内置和单独 Analyzers 包各触发 LNF003，analyzers/dotnet/cs 唯一 DLL；恢复图/输出/deps 无 Roslyn/MEF/分析器运行资产；IDE codefix 交互后置 |
| 完整 Demo Native AOT 发布 | ✅ 仅发布/PE | net10.0/win-x64 发布通过，EXE 4,084,224 字节，无 CLR Header；应用发布属性不再污染分析器。未启动 UI，IL2026/IL3053 资源警告仍在 FND-006 待办 |
| API 与台账 | ✅ 结构/元数据 | 更新启动/DPI/枚举签名及任务状态，成员平台验收仍 unverified；FND-003、ListViewItem-01、Application-01 父任务保留部分完成 |

复现脚本：`tools/compatibility/Test-ApplicationHighDpi.ps1`；先本地打包 Core/Forms/Analyzers，再运行 `tools/compatibility/Test-AnalyzerPackage.ps1 -PackageDirectory artifacts/compatibility-batch001/packages`。日志和探针结果位于本机忽略的 artifacts/compatibility-batch001 与 artifacts/compatibility-analyzers。未向包源发布。

## 批次 002 · 2026-10-15 实施结果

本批明确子范围为 Component/Control 契约与五张 Bitmap 读取，详见 [批次 002](batches/002.md)。父任务 FND-001/FND-006/Control-01/Control-08 保持部分完成；代码完成与兼容验收分别记录。

| 项目 | 结果 | 范围与限制 |
| --- | --- | --- |
| 主线 Release / Demo 双目标 | ✅ | 最终主线 0 错误、本次增量 324 警告；13 个仓库项目（含新增无 UI 资源探针）、17 个目标声明全部为 net10.0/net10.0-windows。原始 Designer/resx 均无修改，未做设计器往返 |
| 组件与现有回归 | ✅ | 101 项纯托管测试全通过（原 92 加新增 9），基础 HWND 消费/释放测试另 1 项通过；不覆盖复杂 UI 和完整父子级联 |
| 组件生命周期差分 | ✅ 指定轻量范围 | Test-ControlComponent.ps1：相同源码的 WinForms 10.0.11/net10.0 Lumina 两端 5 项观测相同，包括重复 Disposed、事件状态、Site 移除和 Dispose(false) |
| 五图托管/AOT 解码 | ✅ 指定读取范围 | 真实 Demo 的 4×16×16 工具栏位图、1×620×300 SplashScreen；WinForms/Lumina JIT/Native AOT 的像素哈希、强类型属性、文化回退、对象身份、普通 byte[]、重载/GC/错误语义一致；未构造主窗或启动 UI |
| 资源告警严格门禁 | ⬜ 未通过 | IL2026 已消除；原 IL3053 详细展开的 IL3050 仍存在。资源脚本已先执行并对照 AOT EXE，再因该告警非零退出；不能将整套严格检查记为通过 |
| 告警定位 | ✅ 仅根因证据 | 原交接 ba14332 隔离源码和最终产物均用 TrimmerSingleWarn=false；依赖图明确 Container.ValidateName 根化 InheritanceAttribute.Default，再经 ToString 调用 Enum.GetValues(Type)。保留标准组件容器，没有全局 suppress；Icon/Font/其他转换与完整反射路径未关闭 |
| 完整 Demo AOT 发布/PE | ✅ 仅发布/PE | net10.0/win-x64 发布通过，EXE 4,085,248 字节、无 CLR Header；资源探针原生 EXE 4,203,008 字节、无 CLR Header。完整主窗操作和正常退出仍后置 |
| 运行依赖/API | ✅ 结构/元数据 | net10.0 Demo deps 无 WinForms 和 System.Resources.Extensions；因 Control 公共继承/接口改变重新导出固定 API，所有平台验收仍 unverified |
| 路线图/索引 | ✅ 结构 | 391 份 Markdown、187 组控件/API 页、2,001 项任务索引，重新生成后 0 结构错误；不计算兼容验收率 |

复现：`./tools/compatibility/Test-ControlComponent.ps1`、`./tools/compatibility/Test-BitmapResources.ps1`；后者保留剩余 IL3050 的严格失败。日志在本机忽略目录 artifacts/compatibility-batch002。Windows 复杂 UI、设计器往返、完整资源/所有权/线程、设备/压力与 Linux/macOS 仍待后期，未从上述结果推算 98%。

## 批次 003 · 2026-10-16 实施结果

本批完成首个真实 UI 投递与基础构造范围，详见 [批次 003](batches/003.md)。FND-001/002/003、Control-03/04、Form-04、Application-08 和 ListViewItem-01 等父任务继续部分完成；以下不是完整兼容验收。

| 项目 | 结果 | 范围与限制 |
| --- | --- | --- |
| 主线 Release / Demo 双目标 | ✅ | 最终 0 错误、本次增量 322 警告；13 项目/17 个目标仍只声明 .NET 10；原始 Demo Designer/resx 未改 |
| 现有与新增回归 | ✅ | 118 项全通过、0 失败/跳过；117 项纯托管 + 1 项基础 HWND 检查，其中新增 12 项 ListViewItem 基础构造和 4 项线程异常事件回归 |
| UI 投递差分 | ✅ 指定轻量范围 | 固定 WinForms 10.0.11、Lumina JIT、Lumina Native AOT 各 69 条观测一致；含 UI/工作线程、Parent/Owner 句柄回退、Action/Delegate/args/Func、IAsyncResult、EndInvoke/FIFO、ExecutionContext、错误及 Dispose 后等待唤醒 |
| 基础构造差分 | ✅ 指定轻量范围 | 41 条两端观测一致；无参/string/string[]、null/空数组/null 元素、输入复制、Text/首子项同步及 Clear 后延迟恢复；完整集合/native 同步未覆盖 |
| 异步错误报告 | ✅ 首范围 | BeginInvoke 有/无 EndInvoke 都触发 UI 线程 ThreadException；线程局部订阅、对象身份、重入与 handler 失败后状态有回归。标准错误对话框/SetUnhandledExceptionMode 及所有 native 事件仍待后续 |
| 线程探针 Native AOT | ✅ 发布/运行/PE | 实际运行 EXE 并与 JIT 69 条结果相同；2,615,808 字节、无 CLR Header，发布日志无 IL2xxx/IL3xxx 告警 |
| 完整 Demo Native AOT | ✅ 仅发布/PE | 4,094,464 字节、无 CLR Header；仍有批次 002 已定位的 Container/InheritanceAttribute IL3050，资源严格门禁未通过；完整主窗操作与正常退出仍待后期 |
| net10.0 依赖 | ✅ 指定依赖图 | Demo deps 无 System.Windows.Forms 与 System.Resources.Extensions；Windows 仍使用官方 Drawing，未引入自有 Drawing 包 |
| API/任务索引 | ✅ 结构/元数据 | 重新导出公共签名/接口，固定 1,095 类型/61,240 行基线不变、行为仍 unverified；392 份 Markdown、187 组控件/API 页及 2,007 项任务检查 0 结构错误 |

明确差异：固定参考端在已有 HWND 时 Invoke((Delegate)null) 挂起，本实现保留 ArgumentNullException 守卫；无 HWND 时仍按基线抛 InvalidOperationException。默认无人订阅的 ThreadException 仅使用原生 OK 提示后继续，未复刻标准 Quit/Details/Help；投递错误 handler 再抛时 FailFast，防止异常越过 native ABI。完整 InvokeAsync/同步上下文、关闭/重建/退出竞态和全量界面/设备验证仍待后续，不能把指定观测计为整个类型验收。

复现：`./tools/compatibility/Test-ControlThreadDispatch.ps1 -Variant both -PublishAot`、`./tools/compatibility/Test-ListViewItemConstruction.ps1`；结果、逐步诊断和发布/测试日志在忽略目录 artifacts/compatibility-batch003。参考端固定 10.0.11；线程探针有 30 秒外部进程超时，超时/失败不记成功。

## 批次 004 · 2026-10-16 实施结果

本批明确子范围为标准 Action/Func<T> 异步投递及子项基础公共构造，详见 [批次 004](batches/004.md)。FND-002/003、Control-05、Form-04、ListViewSubItem-01/03 等父任务保留部分完成，以下证据不等于完整类型兼容验收。

| 项目 | 结果 | 范围与限制 |
| --- | --- | --- |
| 主线 Release / Demo 双目标 | ✅ | 0 错误，本次增量 318 警告；13 项目/17 个目标仅 .NET 10；原始 Demo Designer/resx 无修改，未做设计器往返 |
| 现有与新增回归 | ✅ | 131 项全通过、0 失败/跳过；130 项纯托管 + 1 项既有基础 HWND；新增 13 项子项构造/继承/Text/Name/Tag 回归 |
| 标准 InvokeAsync 差分 | ✅ 指定轻量范围 | 固定 WinForms 10.0.11、Lumina JIT、实际 Native AOT 各 59 条观测一致；Action/Func<T>、UI/工作线程、FIFO、结果身份、原异常、null/无自身句柄、取消顺序/token 与释放后状态 |
| 旧异步路径 | ✅ 指定回归 | 单 Action 已排队；旧 Func<CancellationToken,Task> 专项在 JIT/AOT 验 UI 初始调用、token、等待内层 Task、异常身份、取消和 worker，不将旧签名计为标准 ValueTask |
| 子项构造差分 | ✅ 指定轻量范围 | 37 条两端观测相同；公共无参/owner-text、派生、Text/Name/Tag 初值/null/身份、构造不插入 owner 集合、首项同步；样式及完整所属/native 未覆盖 |
| 既有差分回归 | ✅ | 同步/BeginInvoke/EndInvoke 的 69 条 WinForms/JIT 观测相同；ListViewItem 基础构造的 41 条两端观测相同，原 21 项构造回归保留 |
| 异步探针 Native AOT | ✅ 发布/运行/PE | 2,736,128 字节、CLR Header 为 0；实际运行并与 JIT 对照 59 条相同，旧 Task 专项通过；发布无 IL2xxx/IL3xxx |
| 完整 Demo Native AOT | ✅ 仅发布/PE | 4,094,464 字节、CLR Header 为 0；首次发布摘要 IL3053，以 TrimmerSingleWarn=false 展开仍是 InheritanceAttribute.ToString→Enum.GetValues(Type) 的 IL3050。资源严格门禁未关闭，未重记完整主窗操作/正常退出 |
| Windows 依赖 | ✅ 指定依赖图 | net10.0 Demo deps 无 WinForms/System.Resources.Extensions/Lumina.Drawing；继续官方 System.Drawing.Common，两目标和平台策略未改 |
| API/任务索引 | ✅ 结构/元数据 | 因公共签名改变重新导出固定基线，1,095 类型/61,240 行不变；393 份 Markdown、187 组控件/API 页、2,009 项任务检查 0 结构错误；运行验收状态仍 unverified，不按任务计数推算 98% |

参考端实测边界：null callback 校验优先，错误返回 Faulted Task；InvokeAsync 要求自身 HWND，不沿用 BeginInvoke 的父级句柄回退；预取消返回成功并跳过回调（Func 返回 default）；排队后/回调内取消保留 token，已进入回调继续执行。Task 错误不报告 ThreadException，也不经 GetBaseException 拆开回调异常。已排队而随 Dispose 丢弃的 Task 仍等待后续 token 取消，与参考端相同；无 token 的关闭等待、所有重建/退出竞态、ValueTask、标准同步上下文和 callback 内 await 恢复仍待后续，不宣称已覆盖。

复现：`./tools/compatibility/Test-ControlInvokeAsync.ps1 -Variant both -PublishAot`、`./tools/compatibility/Test-ListViewSubItemConstruction.ps1`，以及批次 003 的两个现有差分脚本。统一 `dotnet build Lumina.slnx -c Release`、`dotnet test Lumina.Tests/Lumina.Tests.csproj -c Release --no-build`；完整 Demo 发布使用 `-f net10.0 -r win-x64 -p:PublishAot=true -p:TrimmerSingleWarn=false`。本批日志/结果在忽略目录 artifacts/compatibility-batch004；复杂 UI/设备/设计器仍后置，先 Windows 再 Linux，macOS 单列。

## 批次 005 · 2026-10-16 实施结果

本批完成标准 ValueTask 入口和子项样式的明确子范围，详见 [批次 005](batches/005.md)。FND-002/003、Control-05、Form-04、ListViewSubItem-01/03/04/06 保留部分完成；同步上下文与已挂接样式不由本批关闭。

| 项目 | 结果 | 范围与限制 |
| --- | --- | --- |
| 主线 Release / Demo 双目标 | ✅ | 0 错误，本次增量 318 警告；13 项目/17 个目标仅 .NET 10；原始 Designer/resx 无修改，设计器往返后置 |
| 回归测试 | ✅ | 146 项全通过、0 失败/跳过；新增 15 项子项样式/共享 Font 寿命检查，保留既有基础 HWND 测试 |
| ValueTask 差分 | ✅ 指定轻量范围 | 固定 WinForms 10.0.11、Lumina JIT、实际 Native AOT 各 90 条观测相同；初始 UI 调用、两族结果/异常身份/token、同步/延迟和取消后 IValueTaskSource 单次消费、参数/句柄边界及释放后状态 |
| async lambda / 旧路径 | ✅ 指定回归 | 旧 Task 虚方法用 OverloadResolutionPriority(-1) 保留显式消费及派生 override；未强转 async token lambda 选择标准族。Action/Func<T> 59 条三端观测与旧 Task 专项继续通过；同步投递 69 条 WinForms/JIT 观测通过 |
| 子项样式差分 | ✅ 指定轻量范围 | 37 条 WinForms/JIT 观测相同；三种公共构造、独立/未挂接 owner 的默认值/空值恢复/Reset/身份、借用 Font 寿命。全局默认字体与已挂接继承/native 未覆盖 |
| 基础构造回归 | ✅ | 子项 37 条、ListViewItem 41 条两端观测保留 |
| ValueTask 探针 AOT | ✅ 发布/运行/PE | 2,794,496 字节、CLR Header 为 0，90 条实际运行观测与 JIT 相同；发布无 IL2xxx/IL3xxx |
| 完整 Demo AOT | ✅ 仅发布/PE | 4,094,464 字节、CLR Header 为 0；详细告警仍为标准 Container 根化的 InheritanceAttribute/Enum.GetValues(Type) IL3050；完整可见主窗操作/正常关闭未重记通过 |
| Windows 依赖与 API | ✅ 指定图/形状 | net10.0 Demo deps 无 WinForms/Resources.Extensions/自有 Drawing；继续官方 Drawing。固定基线重新导出，1,095 类型/61,240 行不变，运行验收状态继续 unverified |
| 任务索引与文档 | ✅ 结构 | 394 份 Markdown、187 组控件/API 页、2,011 项任务检查 0 结构错误；新增批次 006 的两个待实现子范围，不按任务数计算兼容率 |

默认子项字体在本机为 Microsoft YaHei UI 9；SystemFonts.DefaultFont 为宋体 9，不能互代。库内惰性共享官方 MessageBoxFont，并保留显式 Font 的借用身份；尚无 Control.DefaultFont/Application 全局配置与 item→ListView 回链。ValueTask 首次 UI 调用和底层单次消费不代表 callback 内 await 后恢复 UI；默认同步上下文、完整关闭/重建/退出竞态及设备/设计器/跨平台验收继续待后续。

复现：`./tools/compatibility/Test-ControlInvokeValueTask.ps1 -Variant both -PublishAot`、`./tools/compatibility/Test-ListViewSubItemStyle.ps1`；旧 Action/Func、线程及两组构造驱动继续执行。统一构建/测试、API 导出、完整 Demo 发布及结果记录在 artifacts/compatibility-batch005，现有脚本仍写原批次忽略目录。完整 Demo 使用 `-f net10.0 -r win-x64 -p:PublishAot=true -p:TrimmerSingleWarn=false`。

## 批次 006 · 2026-10-16 实施结果

本批完成显式同步上下文和子项 key 的明确子范围，详见 [批次 006](batches/006.md)。FND-002/003、Control-05、Form-04 和完整 context/集合父任务继续部分完成；默认安装与完整所属/native 未由本批关闭。

| 项目 | 结果 | 范围与限制 |
| --- | --- | --- |
| 主线 Release / Demo 双目标 | ✅ | 0 错误、本次增量 318 警告；13 项目/17 目标仅 .NET 10；原始 Designer/resx 未改 |
| 全线回归测试 | ✅ | 191 项通过，0 失败/跳过；新增 38 项 key 与 7 项独立 STA 原生投递检查 |
| 显式 context 差分 | ✅ 指定轻量范围 | 固定 WinForms 10.0.11、Lumina JIT、实际 Native AOT 的 57 条相同；无 Form 建窗/Post、Send FIFO/跨线程/原异常、state/ExecutionContext、ThreadException、复制目的地、普通 Form 释放、共享 Dispose 与显式 await 恢复 |
| null callback 隔离检查 | ✅ 差异明确 | null Post 两端接受且无回调；参考活跃 null Send 在隔离进程 3 秒超时，Lumina 抛 ArgumentNullException（d）防挂，不把该项算相同 |
| 子项 key 差分 | ✅ 指定轻量范围 | 209 条参考/JIT 观测一致，四个 virtual 成员；上次索引缓存、Name/Insert/Remove/Clear、null/空/重复 key、等长不变文化与 Unicode/NUL、身份及首项延迟恢复 |
| 既有路径回归 | ✅ | 同步投递 69、Action/Func 59 与旧 Task、ValueTask 90、Item 构造 41、子项基础/样式各 37 条参考/JIT 继续相同 |
| context 探针 AOT | ✅ 发布/运行/PE | 2,688,512 字节，CLR Header 为 0；实际运行 57 条与 JIT 相同，发布无 IL2xxx/IL3xxx |
| 完整 Demo AOT | ✅ 仅发布/PE | 4,094,464 字节，CLR Header 为 0；仍有标准 Container/InheritanceAttribute/Enum.GetValues(Type) IL3050，未全局抑制；可见操作与正常退出未重记通过 |
| 依赖与 API | ✅ 指定图/形状 | net10.0 Demo deps 无 WinForms/Resources.Extensions/自有 Drawing；1,095 类型/61,240 行 API 分母不变，运行验收状态继续 unverified |
| 任务索引与文档 | ✅ 结构 | 395 份 Markdown、187 组控件/API 页、2,013 项任务检查 0 结构错误；新增批次 007 两个待实现子范围，不按任务数计算兼容率 |

显式 context 真实持有 message-only HWND，成功创建后登记 DPI 门禁，WM_NCDESTROY 关闭 dispatcher 并释放 GCHandle；不改 Demo 或引入新调度架构。按固定参考保留每线程共享目的地与释放失效：本实例 Dispose 后投递无动作，其他 copy/同线程实例仍指向失效窗口，重新构造也不修复，未执行排队项丢弃。完整线程退出、跨线程释放、多 UI 线程、模态、默认安装与 callback await 恢复仍待后续。

复现：`./tools/compatibility/Test-WindowsFormsSynchronizationContext.ps1 -Variant both -PublishAot`、`./tools/compatibility/Test-ListViewSubItemKeys.ps1`；旧驱动继续保留。日志、TRX、项目目标和 PE 证据写 artifacts/compatibility-batch006，完整 Demo 使用 `-f net10.0 -r win-x64 -p:PublishAot=true -p:TrimmerSingleWarn=false`。下一批范围见 [007](batches/007.md)，代码完成与兼容验收分开，不推算 98%。

## 批次 007 · 2026-10-16 实施结果

本批完成默认同步上下文和集合视图/整数索引的明确子范围，详见 [批次 007](batches/007.md)。FND-002/003、Control-05、Form-04、Application-03/14 和完整 context/集合父任务继续部分完成；以下不是完整兼容验收。

| 项目 | 结果 | 范围与限制 |
| --- | --- | --- |
| 主线 Release / Demo 双目标 | ✅ | 最终 0 错误、本次增量 315 警告；13 项目/17 目标仅 .NET 10；原始 Designer/resx 未改 |
| 全线测试 | ✅ | 236 项通过，0 失败/跳过；新增 18 项独立 STA 与 27 项集合回归 |
| 默认 context 差分 | ✅ 指定轻量范围 | 固定 WinForms 10.0.11、Lumina JIT、实际 AOT 的 204 条相同；AutoInstall/Uninstall、null/base/custom/forms Current、构造/前驱缓存、默认 callback await UI 恢复、Run 还原与两个不同 STA 的无 Form 退出 |
| 原生退出修复 | ✅ 指定场景 | ExitThread 只 PostQuit，离开 HWND 回调后的最外层 finally 才释放本线程 activation handle/cookie；修正新增无 Form 检查定位的 DeactivateActCtx SEH，不吞异常 |
| 集合视图差分 | ✅ 指定轻量范围 | 72 条参考/JIT 相同；公共 owner 构造与 null 参数、共享数据/独立视图、延迟首项、整数 index/value 顺序、替换身份和各视图 key 缓存 |
| 既有回归 | ✅ | 显式 context57、同步投递69、Action59/旧Task、ValueTask90、Item41、子项基础/样式各37和 key209 参考/JIT 均保留 |
| 安装探针 AOT | ✅ 发布/运行/PE | 2,750,464 字节，CLR Header 为 0；实际运行 204 条与 JIT 相同，发布无 IL2xxx/IL3xxx |
| 完整 Demo AOT | ✅ 仅发布/PE | 4,105,216 字节，CLR Header 为 0；既有 InheritanceAttribute/Enum.GetValues(Type) IL3050 保留；完整可见操作与正常关闭未重记通过 |
| 依赖与 API | ✅ 指定图/形状 | Windows 使用官方 Drawing，net10.0 deps 无 WinForms/Resources.Extensions/自有 Drawing；重新导出后固定 1,095 类型/61,240 行不变，运行状态仍 unverified |
| 任务索引与文档 | ✅ 结构 | 396 份 Markdown、187 组控件/API 页、2,015 项任务检查 0 结构错误；新增批次 008 两个待实施子范围，任务数量不作为兼容率 |

全部 ApplicationContext/Run/模态、线程退出/关闭/重建/跨线程 Dispose 竞态仍待后续。当前 Form 最后窗体退出使用进程全局 OpenForms 计数，多 UI 退出语义未完成；安装单元测试显式请求自身 STA ExitThread，以免其他并行测试窗体决定该循环的结束。顺序参考探针仍保留仅 Close 的退出路径。复杂 UI/设备/压力/设计器集中后置，先 Windows 后 Linux，macOS 单列。

复现：`./tools/compatibility/Test-WindowsFormsSynchronizationContextInstall.ps1 -Variant both -PublishAot`、`./tools/compatibility/Test-ListViewSubItemCollection.ps1`，两者可先单独记录固定参考；旧驱动保留。日志、TRX 和 PE/项目/依赖证据在 artifacts/compatibility-batch007。完整 Demo 发布继续使用 `-f net10.0 -r win-x64 -p:PublishAot=true -p:TrimmerSingleWarn=false`。

## 批次 008 · 2026-10-16 实施结果

本批明确范围为标准焦点 API 首范围及子项三个 typed Add，详见 [批次 008](batches/008.md)。Control-02 与 ListViewSubItem-02 等父任务保持部分完成，以下指定证据不等于完整类型兼容验收。

| 项目 | 结果 | 范围与限制 |
| --- | --- | --- |
| 主线 Release / Demo 双目标 | ✅ | 0 错误，本次增量 312 警告；13 项目/17 目标仅 .NET 10；原始 Designer/resx 无修改 |
| 全线回归 | ✅ | 293 项全通过，0 失败/跳过；新增焦点 17 项及添加 40 项，焦点测试使用独立 STA 并隔离其他 collection 的窗口抢焦点 |
| 焦点差分 | ✅ 指定轻量范围 | 标准 bool Focus、virtual Focused、ContainsFocus/CanFocus 与元数据；157 条固定 WinForms 10.0.11/JIT/实际 AOT 相同。无 HWND 不创建、程序/外部原生切换、原生 descendant、隐藏/禁用/释放均覆盖；真实 GetFocus/IsChild/可见与启用状态，不使用托管标志冒充 |
| typed Add 差分 | ✅ 指定轻量范围 | 三重载 74 条参考/JIT 相同，返回与参数/null/重复/其他 owner、字符串/样式/借用 Font、首次外部视图/Clear/主项 Text/独立 key 缓存已检；保留历史泛型 void 显式适配，完整 IList/所属/native 未关闭 |
| 既有差分回归 | ✅ | 同步投递69、Action59/旧Task、ValueTask90、显式context57、默认安装204、Item41、子项基础/样式各37、key209、集合72 参考/JIT 全部保留 |
| 焦点探针 AOT | ✅ 发布/运行/PE | 实际运行 157 条与 JIT 相同；EXE 2,552,320 字节，CLR Header 为 0，无 IL2xxx/IL3xxx |
| 完整 Demo AOT | ✅ 仅发布/PE | EXE 4,105,216 字节，CLR Header 为 0；既有 InheritanceAttribute/Enum.GetValues(Type) IL3050 保留，不将新 Add 的 JIT 对照当实际 AOT 运行验收；完整主窗操作/关闭后置 |
| 依赖与 API | ✅ 指定图/形状 | 官方 Drawing；net10.0 deps 无 WinForms/Resources.Extensions/自有 Drawing；重新导出固定 1,095 类型/61,240 行，运行状态仍 unverified |
| 文档与任务索引 | ✅ 结构 | 397 份 Markdown、187 组控件/API 页、2,017 项任务，0 结构错误；父任务部分完成与两个新子范围待实施分别保留 |

Select/CanSelect、完整键盘/验证/布局/继承与重建竞态，非泛型 IList、AddRange/Insert/Remove 全契约、完整所属/已挂接样式/native 同步继续待后续；复杂 UI/设备/设计器和跨平台运行集中后置，代码完成不计为 98% 验收。

复现：`./tools/compatibility/Test-ControlFocus.ps1 -Variant both -PublishAot`、`./tools/compatibility/Test-ListViewSubItemAdd.ps1`；日志、TRX 与 PE/依赖证据在 artifacts/compatibility-batch008。旧驱动继续保留，完整 Demo 发布仍使用 `-f net10.0 -r win-x64 -p:PublishAot=true -p:TrimmerSingleWarn=false`。

## 批次 009 · 2026-10-16 实施结果

本批为 Control 零尺寸/句柄相关负尺寸和三个 typed AddRange 的明确子范围，详见 [批次 009](batches/009.md)。代码完成不关闭完整类型、布局、集合所属或 native 同步验收。

| 项目 | 结果 | 范围与限制 |
| --- | --- | --- |
| 主线 Release / Demo 双目标 | ✅ | 0 错误，最后成功增量构建 45 警告；13 项目/17 目标仅 .NET 10；原始 Designer/resx 无改动 |
| 全线回归 | ✅ | 447 项通过，0 失败/跳过；新增尺寸 33 项和 AddRange 121 项，原生矩形测试使用独立 STA/禁止并行 collection |
| 尺寸差分 | ✅ 指定轻量范围 | 756 条固定 WinForms 10.0.11/JIT/实际 AOT 相同。无自身 HWND 保留零/负值且不创建；有 HWND 负数归零、实际零矩形可恢复；首次创建回读原生结果、HandleCreated 时机及 int.MinValue/CW_USEDEFAULT 已检 |
| typed AddRange 差分 | ✅ 指定轻量范围 | 三重载 151 条参考/JIT 相同；null 数组拒绝且不改变状态，三者均跳过 null 元素，保留非 null 空 Text 对象。顺序/对象身份/输入复制/重复/其他 owner、样式/借用 Font、共享视图/主项 Text/独立 key 缓存均保留 |
| 既有差分回归 | ✅ | 组件5、线程69、Action59/旧Task、ValueTask90、显式context57、默认安装204、焦点157、Item41、子项基础/样式各37、key209、集合72及Add74参考/JIT通过 |
| 尺寸探针 AOT | ✅ 发布/运行/PE | 实际运行 756 条与 JIT 相同；EXE 2,562,048 字节，CLR Header 为 0，无 IL2xxx/IL3xxx |
| 完整 Demo AOT | ✅ 仅发布/PE | EXE 4,105,728 字节，CLR Header 为 0；既有 InheritanceAttribute/Enum.GetValues(Type) IL3050 保留。完整主窗操作/关闭后置；新 AddRange 未计为实际 AOT 运行验收 |
| 依赖与 API | ✅ 指定图/形状 | 官方 Drawing；net10.0 deps 无 WinForms/Resources.Extensions/自有 Drawing；按固定基线重新导出 1,095 类型/61,240 行，运行状态仍 unverified |
| 文档与任务索引 | ✅ 结构 | 398 份 Markdown、187 组控件/API 页、2,019 项任务，0 结构错误；父任务保持部分完成，下一批两个子范围待实施 |

构造默认大小、全部派生尺寸约束、Form.ScaleSize、Min/Max、指定边界/完整布局及位置尺寸事件仍未关闭。非泛型 IList、Insert/Remove 全契约、完整所属/已挂接样式/native 同步仍缺；ApplicationContext/多 UI/模态/关闭重建与 Form 全局 OpenForms 退出缺口继续保留。复杂 UI/设备/设计器和 Linux/macOS 验收后置，不从任务数量或构建成功推算 98%。

复现：`./tools/compatibility/Test-ControlBounds.ps1 -Variant both -PublishAot`、`./tools/compatibility/Test-ListViewSubItemAddRange.ps1`；两者支持先单独记录固定参考。既有差分驱动保留，完整 Demo 发布继续使用 `-f net10.0 -r win-x64 -p:PublishAot=true -p:TrimmerSingleWarn=false`。日志、TRX 和 PE/项目/依赖证据见 artifacts/compatibility-batch009。

## 批次 010 · 2026-10-16 实施结果

本批为 Control 指定边界/虚 core 入口与子项 typed Insert/void Remove/RemoveAt/Clear 的明确范围，详见 [批次 010](batches/010.md)。代码完成不自动关闭完整布局、事件、集合接口、所属或 native 同步。

| 项目 | 结果 | 范围与限制 |
| --- | --- | --- |
| 主线 Release / Demo 双目标 | ✅ | 0 错误，本次增量 304 警告；13 项目/17 目标仅 .NET 10，原始 Designer/resx 无改动 |
| 全线回归 | ✅ | 617 项通过，0 失败/跳过；新增指定边界 60 项与子项变更 110 项，真实 HWND 测试沿用独立 STA/禁止并行 collection |
| 指定边界差分 | ✅ 指定轻量范围 | BoundsSpecified 八值/Flags、四/五参数 x/y、protected virtual core 与七个属性入口，共 2,034 条固定 WinForms 10.0.11/JIT/实际 AOT 相同；掩码/未知位、重复、直接 core、派生拒绝/调整/None/异常与真实矩形均对照 |
| typed 插入移除差分 | ✅ 指定轻量范围 | 203 条参考/JIT 相同；Insert 先 index 后 item，公开非虚 void Remove 只按引用身份删首项，null/缺失无操作；Clear/RemoveAt 保持接口 virtual-final。共享视图/Text恢复/独立 key 缓存/借用 Font 保留，历史泛型 Insert/bool Remove 显式适配另有回归 |
| 既有差分回归 | ✅ | 组件5、线程69、Action59/旧Task、ValueTask90、显式context57、默认安装204、焦点157、旧尺寸756、Item41、子项基础/样式各37、key209、集合72、Add74和AddRange151参考/JIT通过 |
| 指定边界 AOT | ✅ 发布/运行/PE | 实际运行 2,034 条与 JIT 相同；EXE 2,576,896 字节，CLR Header 为 0，无 IL2xxx/IL3xxx |
| 完整 Demo AOT | ✅ 仅发布/PE | EXE 4,105,728 字节，CLR Header 为 0；既有 InheritanceAttribute/Enum.GetValues(Type) IL3050 保留，完整主窗操作/正常关闭后置；新子项变更未提前计实际 AOT 运行验收 |
| 依赖与 API | ✅ 指定图/形状 | net10.0 deps 无 WinForms/Resources.Extensions/自有 Drawing，仅官方 System.Drawing.Common；固定基线重新导出 1,095 类型/61,240 行，全部运行状态仍 unverified |
| 文档与任务索引 | ✅ 结构 | 399 份 Markdown、187 组控件/API 页、2,021 项任务，0 结构错误；父任务保留部分完成，下一批两个明确子范围待实施 |

保留构造默认、全部派生尺寸约束、Min/Max、位置尺寸事件、完整 Layout/客户区和外部原生自主移动/尺寸通知缺口；非泛型 IList/ICollection、Contains/IndexOf/CopyTo 全契约、完整所属/已挂接样式/native 同步仍另列。ApplicationContext/多 UI/模态/关闭重建、Form 全局 OpenForms 退出、复杂 UI/设备/压力/设计器以及 Linux/macOS 验收继续后置，不从任务或构建数量推算 98%。

复现：`./tools/compatibility/Test-ControlBoundsSpecified.ps1 -Variant both -PublishAot`、`./tools/compatibility/Test-ListViewSubItemMutation.ps1`；均支持先独立取得固定参考。既有差分驱动保持，完整 Demo 使用 `-f net10.0 -r win-x64 -p:PublishAot=true -p:TrimmerSingleWarn=false`。日志、TRX、PE/项目/依赖证据见 artifacts/compatibility-batch010。

## 批次 011 · 2026-10-16 实施结果

本批完成标准位置尺寸通知与非泛型子项接口的两个明确范围，详见 [批次 011](batches/011.md)。Control-09/10、ListViewSubItem-02 等父任务保留未完内容，代码完成与完整兼容验收分开。

| 项目 | 结果 | 范围与限制 |
| --- | --- | --- |
| 主线 Release / Demo 双目标 | ✅ | 0 错误，本次增量 301 警告；13 项目/17 目标仅 .NET 10，原始 Designer/resx 无改动 |
| 全线回归 | ✅ | 986 项通过，0 失败/跳过；新增通知 99 项与子项接口 270 项，原生检查沿用独立 STA/禁止并行 collection |
| 标准通知差分 | ✅ 指定轻量范围 | 四事件/四 protected virtual On*、七属性/两族 SetBounds/直接 core 的 2,280 条固定 WinForms 10.0.11/JIT/实际 AOT 相同；重复/指定/未知位、零负归一、覆盖/不调 base/重入/异常、回调参数与矩形均取证 |
| 参数与异常契约 | ✅ 指定场景 | OnLocationChanged 向 OnMove 透传参数；OnSizeChanged 向 OnResize 传 Empty，SizeChanged 保留原参数。有 HWND setter 转交既有 ThreadException，无 HWND/直接 On* 同步传播；默认错误 UI/完整异常模式未关闭 |
| 非泛型接口差分 | ✅ 指定轻量范围 | 1,406 条参考/JIT 相同；对象类型先于位置、null/错型/失败状态、重复 Add 首次位置、派生 Equals 引用身份、flags/各 view SyncRoot、显式数组 CopyTo 的 rank/类型/索引/容量/非零下界/部分写入、空集合不恢复首项、独立 key 缓存/借用 Font 均覆盖 |
| 既有差分回归 | ✅ | 组件5、线程69、Action59/旧Task、ValueTask90、显式context57、默认安装204、焦点157、旧尺寸756、指定边界2034、Item41、子项基础/样式各37、key209、集合72、Add74、AddRange151、变更203参考/JIT通过 |
| 通知探针 AOT | ✅ 发布/运行/PE | 实际运行 2,280 条与 JIT 相同；EXE 2,618,880 字节，CLR Header 为 0，无 IL2xxx/IL3xxx |
| 完整 Demo AOT | ✅ 仅发布/PE | EXE 4,106,240 字节，CLR Header 为 0；既有 InheritanceAttribute/Enum.GetValues(Type) IL3050 保留。完整主窗操作/正常关闭后置；新子项接口未记实际 AOT 运行验收 |
| 依赖与 API | ✅ 指定图/形状 | net10.0 deps 无 WinForms/Resources.Extensions/自有 Drawing，继续官方 System.Drawing.Common；重新导出固定 1,095 类型/61,240 行，全部运行状态仍 unverified |
| 文档与任务索引 | ✅ 结构 | 400 份 Markdown、187 组控件/API 页、2,023 项任务，0 结构错误；两个子范围代码完成，父任务保留部分完成和下一批待实施 |

标准通知独立于旧 OnBoundsChanged，保留 ApplyBounds 后的派生布局衔接。ClientSize/ClientRectangle/ClientSizeChanged、外部原生自主移动/尺寸通知、完整 Layout/构造默认/所有派生约束与 Form 生命周期另列；完整枚举/并发、完整所属/挂接样式/native 未关闭。ApplicationContext/多 UI/模态/关闭重建、Form 全局 OpenForms 退出以及复杂 UI/设备/压力/设计器和 Linux/macOS 验收仍待后续，不从任务或编译数量推算 98%。

复现：`./tools/compatibility/Test-ControlBoundsNotifications.ps1 -Variant both -PublishAot`、`./tools/compatibility/Test-ListViewSubItemInterfaces.ps1`，均支持先单独取得固定参考。旧驱动保留，完整 Demo 继续 `-f net10.0 -r win-x64 -p:PublishAot=true -p:TrimmerSingleWarn=false`。证据在 artifacts/compatibility-batch011；下一批见 [012](batches/012.md)。

## 批次 012 · 2026-10-16 实施结果

本批完成基础客户区及子项枚举两个明确范围，详见 [批次 012](batches/012.md)。父任务仍保留布局、完整派生约束、所属与 native 等未完内容，代码完成与完整兼容验收分开。

| 项目 | 结果 | 范围与限制 |
| --- | --- | --- |
| 主线 Release / Demo 双目标 | ✅ | 0 错误，本次增量 298 警告；13 项目/17 目标仅 .NET 10，原始 Designer/resx 无改动 |
| 全线回归 | ✅ | 1,413 项通过，0 失败/跳过；新增客户区 80 项、枚举 347 项，原生检查沿用独立 STA/禁止并行 collection |
| 基础客户区差分 | ✅ 指定轻量范围 | ClientSize/ClientRectangle/ClientSizeChanged 与 protected core/On*，四有限边框、11 入口/重复/零负恢复/覆盖/重入/原异常及缓存不一致后仅位置变更，共 5,556 条固定 WinForms 10.0.11/JIT/实际 AOT 相同 |
| 缓存/通知契约 | ✅ 指定场景 | Client core 正常执行至末尾时提交原始请求并通知（不检查是否同值，末尾事件异常同步传播）；native 与托管缓存可因负值/重入不同。客户区缓存变化亦发尺寸链，旧 OnBoundsChanged/GetNativeHeight 保留。GetClientRect/AdjustWindowRectEx 失败真实抛错；完整外部自主通知/异常模式未关闭 |
| 枚举差分 | ✅ 指定轻量范围 | 2,531 条固定参考/JIT 相同；公开 IEnumerator/直接 IEnumerable virtual-final，变更使 MoveNext/Reset 失效但保留 Current 缓存，空 Clear 不失效，首项恢复换表使旧空枚举器保持为空，view 共享当前表/key 缓存独立。历史泛型 typed 无效 Current=null 作为扩展保留 |
| 既有差分回归 | ✅ | 全部 19 个旧驱动通过，覆盖组件/投递/Action旧Task/ValueTask/显式与默认context/焦点/原尺寸/指定边界/通知，以及Item与全部子项基础/样式/key/集合/Add/AddRange/变更/接口 |
| 客户区探针 AOT | ✅ 发布/实际运行/PE | 5,556 条与 JIT 相同；EXE 2,592,256 字节，CLR Header 为 0，无 IL2xxx/IL3xxx。探针对五个实际反射成员加入精确 DynamicDependency，库裁剪策略不变 |
| 完整 Demo AOT | ✅ 仅发布/PE | EXE 4,107,264 字节，CLR Header 为 0；既有 InheritanceAttribute/Enum.GetValues(Type) IL3050 保留。完整主窗操作/正常关闭后置，新枚举未记实际 AOT 运行验收 |
| 依赖与 API | ✅ 指定图/形状 | net10.0 deps 无 WinForms/Resources.Extensions/自有 Drawing，仍用官方 System.Drawing.Common；重新导出固定 1,095 类型/61,240 行，运行状态 unverified 不变 |
| 文档与任务索引 | ✅ 结构 | 401 份 Markdown、187 组控件/API 页、2,025 项任务；两个明确子范围代码完成，父任务及下一批待实施内容独立保留 |

SizeFromClientSize、构造默认、所有派生边框/尺寸约束、DPI 切换、外部原生自主通知、完整布局/滚动/Form 生命周期、真实多线程/压力/完整所属/挂接样式/native 仍另列。ApplicationContext/多 UI/模态/关闭重建、Form 全局 OpenForms 退出、复杂 UI/设备/设计器及 Linux/macOS 仍待后续，不从任务/构建数量推算 98%。

复现：`./tools/compatibility/Test-ControlClientSize.ps1 -Variant both -PublishAot`、`./tools/compatibility/Test-ListViewSubItemEnumeration.ps1`，均可先单独取固定参考；日志/TRX/PE/项目/依赖证据在 artifacts/compatibility-batch012。下一批见 [013](batches/013.md)。

## 批次 013 · 2026-10-16 实施结果

本批按 [013](batches/013.md) 继续 Control-09b 与 ListViewSubItem-06a，两个子智能体先准备固定参考，由主聊天顺序运行后实施并统一集成。实现与验证范围分开记录，父任务保留后续缺口。

| 项目 | 结果 | 范围与限制 |
| --- | --- | --- |
| 主线双目标 Release | ✅ 编译 | 0 错误，本次增量 302 警告；13 项目/17 目标仅 .NET 10，原始静态 Designer/resx 未改 |
| 全线测试 | ✅ 指定范围 | 1,733 通过，0 失败/跳过；新增换算 257、字符串 63，旧 1,413 保留 |
| SizeFromClientSize | ✅ 指定差分 | 10,086 条固定参考/JIT/实际 AOT 全同；四有限边框、无/有 HWND、正零负/int 溢出、覆盖/base 绕过/重入/原异常；ClientSize/SetClientSizeCore 不消费此虚扩展，直接 base 不改缓存/状态/通知或创建 HWND |
| 子项 ToString | ✅ 指定差分 | 58 条参考/JIT 全同；标准 override/Object 槽、原样文本/空值/Unicode/换行/嵌入 NUL、owner/共享 view/重复/派生/原异常；集合/首项/枚举/key 缓存及借用 Font 保持，新字符串未计实际 AOT 运行验收 |
| 既有差分回归 | ✅ 指定范围 | 21 个既有驱动通过，包含前 19 个驱动及批次 012 客户区/枚举 |
| 换算探针 AOT | ✅ 发布/实际运行/PE | EXE 2,610,176 字节，10,086 条与 JIT 全同，CLR Header 0，无 IL2xxx/IL3xxx；仅精确保留实际反射的两个方法 |
| 完整 Demo AOT | ✅ 仅发布/PE | EXE 4,107,264 字节，CLR Header 0；TrimmerSingleWarn=false 展开后仍为既有 InheritanceAttribute/Enum.GetValues(Type) IL3050，完整主窗操作/正常关闭后置 |
| 依赖与 API | ✅ 指定图/形状 | Demo net10.0 deps 无 WinForms/Resources.Extensions/自有 Drawing，官方 System.Drawing.Common 10.0.0；重新导出 1,095 类型/61,240 行，运行状态 unverified |
| 文档与任务索引 | ✅ 结构 | 402 份 Markdown、187 组控件/API 页、2,027 项任务；父任务及批次 014 未实现范围独立保留 |

构造默认、基础及全部派生尺寸约束、DPI/外部原生自主通知、完整布局/滚动/Form 生命周期、真实多线程/压力、完整所属/挂接样式/native/Clone/序列化、复杂 UI/设备/设计器及 Linux/macOS 仍待后续；不从编译或任务数推算 98%。

复现：`./tools/compatibility/Test-ControlSizeFromClientSize.ps1 -Variant both -PublishAot`、`./tools/compatibility/Test-ListViewSubItemToString.ps1`，均可先只跑参考；日志/TRX/PE/项目/依赖证据在 artifacts/compatibility-batch013。下一批见 [014](batches/014.md)。

## 2026-10-16 · 批次 014

本批按 [014](batches/014.md) 完成 Control-09c 的明确初始化后运行期约束，以及 ListViewItem-04a 未挂接元数据首范围。子智能体先准备固定参考，主聊天顺序运行并授权实现，统一集成；只读审查补查构造边界，未以任务数量推算兼容率。

| 项目 | 结果 | 范围与限制 |
| --- | --- | --- |
| 主线双目标 Release | ✅ 编译 | 0 错误，本次集成增量 300 警告；13 项目/17 目标仅 .NET 10，原始静态 Designer/resx 未改 |
| 全线测试 | ✅ 指定范围 | 2,228 通过，0 失败/跳过；旧 1,733 + 新约束 394 + 新列表元数据 101 |
| 基础运行期 Min/Max | ✅ 指定差分 | 33,408 条固定 10.0.11/JIT/实际 AOT 相同；四有限边框、无/有 HWND、负零/冲突/重复/取消、12 入口、派生虚消费/重入/原异常、原始客户区缓存与通知 |
| Name/Tag/ToolTipText | ✅ 英文指定差分 | 151 条固定 en-US 参考/JIT 相同；Name 首项代理与空表读写差异、独立 Tag/Tooltip、引用身份、共享 view/三枚举/key 缓存、隐藏成员及毒丸；未计新范围实际 AOT 运行验收 |
| 既有差分回归 | ✅ 指定范围 | 23 个既有驱动通过，包含批次 013 换算/子项字符串及更早组件/投递/context/焦点/边界/通知/客户区与全部子项消费 |
| 尺寸约束 AOT | ✅ 发布/实际运行/PE | EXE 2,641,920 字节，33,408 条与 JIT/参考全同，CLR Header 0，无 IL2xxx/IL3xxx；六个实际反射 accessor 精确保留 |
| 完整 Demo AOT | ✅ 仅发布/PE | EXE 4,107,776 字节，CLR Header 0；仍有既有 InheritanceAttribute/Enum.GetValues(Type) IL3050，主窗操作/正常关闭后置 |
| 动态 AmbientValue 转换 | ⬜ 已知 AOT 缺口 | 初次 AOT 读取 AmbientValueAttribute.Value 抛原 ArgumentException；最终仅检查 CustomAttributeData 的标准 Size/string 构造元数据，不等同 TypeDescriptor 动态转换可用，未全局根化或抑制裁剪 |
| 中文 Designer 分类/描述 | ⬜ 已知本地化缺口 | 中文列表对照 149/151 相同，Data/Appearance 对“数据/外观”不同；英文 151 全同。尺寸约束描述当前为英文；全语言元数据未验收 |
| 构造期默认约束 | ⬜ 转批次 015 | --constructors 独立参考记录原始 0×0 状态和 default min→max 消费/原异常；候选仍未消费，与 DefaultSize 统一由 Control-01b 实现，不混入运行期全同组 |
| 依赖与 API | ✅ 指定图/形状 | Demo net10.0/win-x64 deps 无 WinForms/Resources.Extensions/自有 Drawing，官方 System.Drawing.Common 10.0.0；重新导出 1,095 类型/61,240 行，运行状态 unverified |
| 文档与任务索引 | ✅ 结构 | 403 份 Markdown、187 组控件/API 页、2,029 项任务，结构校验无错误；父任务、下一批与未验收范围独立保留 |

完整布局/派生约束/DPI/外部自主通知/Form、完整所属/已挂接 Tooltip/native/Clone/序列化与多线程压力及复杂 UI/设备/设计器、Linux/macOS 继续另列。日志/TRX/PE/项目/依赖及差异摘要见 artifacts/compatibility-batch014，复现入口见 tools/compatibility/README.md；下一批见 [015](batches/015.md)。

## 2026-10-16 · 批次 015

本批按 [015](batches/015.md) 完成 Control-01b 基础构造默认尺寸，以及 ListViewItem-01c 字符串首范围。两个实现子任务文件互斥，先准备固定参考、主聊天顺序运行后授权实施；只读辅助审查确认无须修改旧测试或派生构造，旧主已由应用工具确认立即归档。

| 项目 | 结果 | 范围与限制 |
| --- | --- | --- |
| 主线双目标 Release | ✅ 编译 | 0 错误，本次集成增量 300 警告；13 项目/17 目标仅 .NET 10，原始静态 Designer/resx 未改 |
| 全线测试 | ✅ 指定范围 | 2,713 通过，0 失败/跳过；旧 2,228 + 新构造 387 + 新列表字符串 98 |
| DefaultSize/基础无参构造 | ✅ 指定差分 | 2,677 条固定 10.0.11/JIT/实际 AOT 全同；初始 0×0、四有限边框、正零负极值/约束 profile、重复虚读取/公共 setter、默认尺寸直接提交、零轴重入缓存、原异常与 context 安装时序 |
| ListViewItem.ToString | ✅ 英文指定差分 | 96 条固定 en-US 参考/JIT 全同；Object 虚槽、原样 Text、隐藏/override/原异常、元数据/样式、首项/空表/共享 view/三枚举/key 缓存；不消费 Tag/Font，新范围未计实际 AOT 运行验收 |
| 既有差分回归 | ✅ 指定范围 | 25 个驱动通过，含批次 014 的运行期约束/列表元数据和全部旧投递/context/焦点/边界/通知/客户区/子项消费；旧测试和探针未改 |
| 构造 AOT | ✅ 发布/实际运行/PE | EXE 2,020,864 字节，2,677 条与 JIT/参考全同，CLR Header 0，无 IL2xxx/IL3xxx；仅精确保留 get_DefaultSize |
| 完整 Demo AOT | ✅ 仅发布/PE | EXE 4,108,288 字节，CLR Header 0；既有 InheritanceAttribute/Enum.GetValues(Type) IL3050 保留，主窗交互/正常关闭后置 |
| 全部派生默认/完整构造 | ⬜ 未扩面 | 现有派生构造显式尺寸未改；无自有默认尺寸的具体控件继承基础 0×0，专属默认值仍缺。无 parent 文本构造为下一批 01c，含 parent 构造/Controls/Form 继承另列 |
| 依赖与 API | ✅ 指定图/形状 | net10.0 Demo deps 无 WinForms/Resources.Extensions/自有 Drawing，官方 Drawing 10.0.0；公共签名重新导出，1,095 类型/61,240 行、运行 unverified 保持 |
| 文档与任务索引 | ✅ 结构 | 404 份 Markdown、187 组控件/API 页、2,031 项任务，0 结构错误；父任务、具体派生默认及下一批范围独立保留 |

中文 Designer 分类/描述与 AOT 动态 AmbientValue.Value/TypeDescriptor 转换、完整布局/DPI/外部自主通知、完整所属/已挂接样式/native/Clone/序列化、复杂 UI/设备/设计器及 Linux/macOS 保持待实现/待验收，代码完成不计为 98%。复现：`./tools/compatibility/Test-ControlDefaultSize.ps1 -Variant both -PublishAot`、`./tools/compatibility/Test-ListViewItemToString.ps1`；日志与摘要见 artifacts/compatibility-batch015，下一批见 [016](batches/016.md)。

## 2026-10-16 · 批次 016

本批按 [016](batches/016.md) 完成 Control-01c 两个无parent文本构造与 ListViewItem-06a 未挂接样式首范围。两个子智能体文件互斥，先固定10.0.11参考、主聊天顺序运行并授权实现；独立只读审查确认构造/元数据/样式边界。旧主已确认归档，旧测试/探针与原始静态Designer/resx未改。

| 项目 | 结果 | 范围与限制 |
| --- | --- | --- |
| 主线双目标Release | ✅ 编译 | 0错误，本次集成增量301警告；13项目/17目标仅.NET10，固定Demo目标/官方Drawing保持 |
| 全线测试 | ✅ 指定范围 | 3,352通过，0失败/跳过；旧2,713 + 新构造311 + 新样式328，最终测试文件修订后已重跑 |
| 文本构造 | ✅ 指定差分 | 4,584条固定10.0.11参考/JIT/实际AOT指定观测相同；text允许null，默认初始化/context→虚Text→Location→Size，含极值/约束/通知/隐藏/覆盖/重入/原异常和部分状态 |
| 构造元数据 | ✅ 有效契约 | 真实nullable差异修正为string?，仍比较有效参数契约；NullableContext放置和CoreCLR Standard\|HasThis/AOT HasThis原始编码保留stderr，比较实例/HasThis/ExplicitThis/VarArgs/未知位，不补造Standard；初次差异保留 |
| 未挂接列表项样式 | ✅ 英文指定差分 | 541条固定en-US参考/JIT相同，首子项代理/空表恢复换表/三枚举/key缓存/ResetStyle/借用及已释放Font身份，UseItemStyle默认true独立保存；新范围未计实际AOT运行验收 |
| 既有差分 | ✅ 指定范围 | 27个驱动全部通过，含015默认构造/主项字符串和全部旧组件/投递/context/焦点/边界/通知/客户区/约束/子项消费 |
| 构造AOT | ✅ 发布/实际运行/PE | EXE2,075,648字节、CLR Header0，无IL2xxx/IL3xxx；探针初始IL2037修正精确#ctor声明后消除，没有库全局根化 |
| 完整Demo AOT | ✅ 仅发布/PE | EXE4,108,288字节、CLR Header0；本轮展开IL3053确认仍为既有InheritanceAttribute.ToString/Enum.GetValues(Type) IL3050，未抑制；主窗交互/正常关闭后置 |
| 依赖与API | ✅ 指定图/形状 | net10.0 Demo deps无WinForms/Resources.Extensions/自有Drawing，官方Drawing10.0.0；公共签名重新导出，1,095类型/61,240行及运行unverified保持 |
| 路线图收尾/历史回标 | ✅ 文档一致 | 合并三组只读历史核对的具体修正，更新总览/FND/CNS/REP及相关控件；父项部分、未开始/待验收独立。README全部187类型行列实施状态与完成ID，整类型代码完成0；现有工具同步索引/CSV并拒绝陈旧行 |
| 文档/任务校验 | ✅ 结构 | 405份Markdown、187组控件/API页、2,033项任务、0错误；历史批次快照/提交/任务ID和固定分母不变 |

Parent/Controls/Form链、含parent构造、无HWND完整Text/ResetText、native文本/有效值继承、全部具体派生默认/布局/DPI、中文Designer本地化/AOT动态属性转换、已挂接列表样式/native/全局字体/完整所属/Clone/序列化及复杂UI/设备/设计器、Linux/macOS仍待实现或验收。代码完成不计为98%。复现：`./tools/compatibility/Test-ControlTextConstruction.ps1 -Variant both -PublishAot`、`./tools/compatibility/Test-ListViewItemStyle.ps1`；日志/TRX/PE/项目/依赖与初次差异在artifacts/compatibility-batch016，下一批见[017](batches/017.md)。

## 2026-10-16 · 批次017

按[017](batches/017.md)完成Control-11a无父级无自身HWND文本/重置和ListViewItem-04b未挂接图像状态。两个内部子任务文件互斥，先准备固定参考，主聊天顺序运行并授权实现、统一集成。用户更新指令要求完成当前批次后结束；未恢复巡检或开启下一批。

| 检查 | 结果 | 证据及边界 |
| --- | --- | --- |
| 全线测试 | ✅ 指定范围 | 3,934通过，0失败/跳过；新增Text/ResetText 219项、ImageIndex/ImageKey 363项，原3,352项保留 |
| 双目标Release | ✅ 编译 | 0错误/最终增量301警告；静态Designer/resx保持，完整设计器往返后置 |
| Text/ResetText | ✅ 固定英文参考/JIT | 3,774条原始记录相同；虚消费/通知、null/Unicode/NUL/引用身份、覆盖/隐藏/重入/原异常、释放后托管边界 |
| 文本实际AOT | ✅ 行为与其余元数据 | 3,773条原始记录相同，1条Text属性列表仅少DispId=-517；strictMatches=false。实际ilc.rsp与CoreLib内嵌XML证实COM关闭时主动删除，原始差异未改写，不改库AOT配置；无IL2xxx/IL3xxx |
| 图像未挂接状态 | ✅ 固定英文参考/JIT | 518条相同；默认/异常及失败状态、key-index互斥/重复、共享view/三枚举/key缓存/借用资源；未计新范围实际AOT运行 |
| 图像设计器元数据 | ⬜ 未完成 | NoneExcludedImageIndexConverter、ImageKeyConverter和ImageIndexEditor缺失单列；不把缺省当元数据全同 |
| 路线图/逐类型索引 | ✅ 结构与真实状态整理 | 405份Markdown、187组类型/API页、2,033项任务、187行实施状态，校验0错误；只有两个明确子范围新增代码完成，父任务仍部分，整类型完成0 |
| 既有差分 | ✅ 指定范围 | 29个旧驱动全部通过，包含016构造/样式以及001～015已有路径 |
| AOT发布及PE | ✅ 发布/指定实际运行 | 文本EXE 2,049,024字节并实际运行；Demo EXE 4,108,288字节仅发布；两者CLR Header 0 |
| Demo分析警告 | ⬜ 既有缺口 | InheritanceAttribute.ToString/Enum.GetValues(Type) IL3050仍在，无全局抑制；主窗交互/正常关闭仍待验收 |
| 项目/依赖 | ✅ 指定图 | 13项目/17目标仅.NET10，Windows官方Drawing10.0.0；net10.0 deps无WinForms/Resources.Extensions/自有Drawing，原始Designer/resx未改 |

首次全线测试仅有一个新增重入断言预期错误：setter退出回调中的嵌套Reset以exit:reset结束，已按参考加强为完整16步断言；首次失败日志/TRX保留。AOT首次完整元数据比较失败也保留；最终驱动明确只接受实际工具链策略证实的DispId删除，其余原始记录严格比较。

复现：`./tools/compatibility/Test-ControlTextReset.ps1 -Variant both -PublishAot`、`./tools/compatibility/Test-ListViewItemImage.ps1 -Variant both`。日志/TRX/PE/依赖/原始差异与摘要见artifacts/compatibility-batch017/evidence.json。Parent/Controls/Form、已有HWND/native文本、完整布局、ImageList/图像构造/挂接native/完整所属/资源、中文Designer/AOT动态转换及三平台真机继续待实现或验收，不由数量推出98%。公共签名分母仍为1,095类型/61,240行、运行unverified，路线图与逐类型索引同步复核。

## 沿用的前轮运行证据

[首轮审计](../assessments/2026-10-15/README.md)：完整Demo的net10.0/win-x64 Native AOT发布成功，EXE 4,083,712字节、无CLR Header，仍有IL2026/IL3053。短时隐藏启动只看到辅助窗口，没有完成主窗交互与正常退出，后续需可见运行定位。**本轮没有将该项改为通过。**

## 尚未验证

- ⬜ 187组类型的完整属性/事件/方法/行为及公开接口支持率。
- ⬜ 完整Demo的AOT可见主窗、操作与正常关闭；资源警告闭环。
- ⬜ Visual Studio实际设计器打开/保存/重开及所有控件资源往返。
- ⬜ Linux GTK4/macOS AppKit原生后端、各平台Native AOT、真实设备输入法/辅助技术/部署。
- ⬜ System.Drawing.Common公开类型身份的跨平台可行路线。

不会因为规划已细化而宣布以上能力完成；下一项实施按 [M0排序](foundations.md)。

## 2026-10-16 · 批次018

[018](batches/018.md)是Control完整专项的父子/继承依赖批次。Control-06a、Form-01、ContainerControl-01、ScrollableControl-01a明确范围代码完成；整Control、完整容器/滚动与平台未关闭。

| 检查 | 实际结果 | 边界 |
| --- | --- | --- |
| 固定10.0.11/JIT | 391条中390相同，strictMatches=false | 第372行隐藏原生父级重挂缺三次中间VisibleChanged；最终状态正确，019继续 |
| 实际Native AOT探针 | 391条完整运行到complete=True；与JIT有32条差异 | 16个非零下界数组fixture在AOT不支持，明确CopyTo未运行；其余359行相同，不能声称严格通过 |
| 编译/测试 | 固定双目标Release 0错误/400警告；最终3,996项全通过，0失败/跳过 | ToolStrip重入栈溢出原始失败保留；TabControl固定估计值改为实际DisplayRectangle断言，历史提前建窗差异单列 |
| 既有差分 | 首轮29个驱动通过；最终可见/释放修复后16个Control/同步上下文驱动再次通过 | 不是全Control成员覆盖率 |
| 完整Demo实际AOT | 可见主窗，Enable option 0→1，WM_CLOSE后exit0、全部记录HWND失效，无强杀 | 只此交互/关闭范围；截图有文字裁切/布局问题，模态、完整UI和Designer未验收 |
| Native映像 | 新探针3,667,456字节；Demo4,147,712字节；二者CLR目录0 | 探针IL2026/IL3050/IL3053及Demo既有IL3050保留 |
| 项目/依赖/资源 | 13项目17目标仅.NET10；官方Drawing10.0.0；net10.0无WinForms/Resources.Extensions/自有Drawing | 原始Designer/resx未改；Linux/macOS后端及对应资源尚未实现，不记跨平台编译/运行通过 |
| API与路线图 | 固定1,095类型/61,240成员行、187组类型/API页，运行unverified | 同步受影响任务/历史，重新生成逐控件README实施列/tasks.csv；无整类型或98%声明 |

复现：`Test-ControlParentCollection.ps1 -Variant both -PublishAot`（保留严格失败退出）；`Test-NativeFormsDemoWindow.ps1 -ExecutablePath <Demo AOT EXE> -ArtifactsDirectory <证据目录>`；最终测试TRX和日志、原始差异、失败首轮、PE、实际窗口JSON及摘要在artifacts/compatibility-batch018。截图路径为本机`C:/Users/mysti/AppData/Local/Temp/codex-shot-2026-10-16_09-24-08.png`。资源严格门禁FND-006、完整主窗FND-004、Designer和GTK4/AppKit仍未关闭，后续[019](batches/019.md)按同一专项继续。
## 批次 019 · 2026-10-16 实施结果

标准句柄生命周期Control-07a与Form-06a明确范围完成，见[019](batches/019.md)。Control-07/Form-06及FND父项保留部分完成；标准句柄首范围不能代替完整Control或平台验收。下批020按实际依赖继续标准布局事务。

| 检查 | 结果与范围 |
| --- | --- |
| 生命周期固定参考/JIT/实际AOT | 16个隔离STA场景、204行严格相同；含真实窗口可见性、父子树/重建/映射寿命、回调保留、创建重入与Dispose覆盖，以及五个Form场景。最终run为20261016-021319-9490794，sourceHash与参考一致。 |
| 父子集合回归 | 391行参考/JIT全部相同，018隐藏重挂的中间VisibleChanged差异现已修复；原018失败记录保留。 |
| 父子集合AOT | 391行运行至complete；32行非零下界数组fixture为PNSE且CopyTo未执行，其余359行同JIT。严格driver仍失败，不能记为完整通过。 |
| 最终测试/旧驱动 | 4,027测试通过，0失败/跳过；新增Control17/Form14，含失败创建参数重试/Dispose后WeakReference可回收。16个旧Control/context驱动通过。 |
| 双目标Release | 0错误/428警告；13项目/17目标均.NET10，固定双目标、静态Designer/resx未改，Windows官方Drawing10.0.0，net10.0依赖无WinForms/System.Resources.Extensions。 |
| Demo实际AOT | 主窗真实显示、复选框0→1、正常exit0且记录HWND全部失效，无强制清理。EXE4,156,928字节、CLR目录0；仅该交互/退出范围，完整布局/模态/Designer未通过。 |
| 生命周期AOT/警告 | EXE4,115,456字节、CLR目录0，204行完整运行。Container.Add IL2026/TypeConverter IL3053保留；集合另有Array.CreateInstance IL3050；Demo展开的InheritanceAttribute.ToString IL3050仍未关闭。 |
| 固定API清单 | 1,095类型/61,240行/187组类型页不变，运行验收仍unverified；逐控件README实施列与tasks.csv由既有工具再生成。 |

本批初次DoEvents编译失败、旧Handle快照触发延迟建窗、外部可见旧预期、六行Form原生可见差异及Dispose覆盖重复调用的原始证据保留；最终GCHandle强根泄漏补真实GC回归。Form历史Created事件仍隐藏标准Control.Created，完整派生/资源/竞态、输入/绘制/绑定/可访问性、GTK4/AppKit及全Windows验收未关闭。018截图可见的文字裁切不因主窗退出成功而视为修复。

日志、TRX、严格差分、PE、项目与依赖证据在本机忽略目录artifacts/compatibility-batch019/evidence.json；生命周期每次运行保存在唯一子目录。未改写原始失败、未全局抑制警告、未从测试数或任务数计算98%。


## 批次 020 · 2026-10-16 标准布局事务

Control-10b及八个必要容器事务子范围代码完成，完整记录见[020](batches/020.md)。4,083测试（新增25核心+31容器）、16旧Control/context驱动全部通过；双目标Release 0错误/本次454警告。生命周期204行/父子集合391行参考与JIT同。本批未重跑集合AOT，019的32行非零下界数组fixture限制仍保留。

固定10.0.11参考、JIT、实际AOT各24隔离STA进程，均exit0/complete=true/无超时，源码快照hash一致。布局394行JIT/AOT严格同；参考22个完整场景356行同，总394行387同。2行Dock缺真实几何引起的Bounds布局、5行Split比例及传播差异保留，strictMatches=false/driver exit1；Split延期参数panel2/Bounds已修。探针EXE2,872,320字节，CLR目录0，无IL2xxx/IL3xxx；没有事件伪造、输出归一化或宽泛反射根化。

Demo AOT4,159,488字节、CLR目录0，真实主窗显示、复选框0→1、正常exit0、记录HWND全部失效，无强制清理；既有InheritanceAttribute IL3050保留。首次隐藏Form导致MenuStrip偏移fixture失败的原日志/TRX保留，改为Show后核对真实可见菜单。完整UI/文字裁切/模态/Designer不由单一操作宣布修复。

13项目/17目标仅.NET10，原始Demo/Designer/resx未改，Windows官方Drawing10.0.0且net10.0依赖无WinForms/Resources.Extensions/自有Drawing。Export-Inventory按新公共签名重生成，固定1,095类型/61,240行/187页及unverified保持；Validate-Roadmap -ExportTasks生成逐控件实施列/tasks.csv。所有结果和结构计数见artifacts/compatibility-batch020/evidence.json。Control/FND/容器父项及LayoutEngine/PreferredSize/InitLayout/默认布局/平台仍未关闭，不从测试或任务数量宣称98%。


## 批次 021 · 2026-10-16 默认布局与首选尺寸

Control-09d/10c、Panel-03a、Form-10b和六容器明确子范围代码完成，完整边界见[021](batches/021.md)。4,107项测试（新增13核心+11容器）与16旧Control/context驱动全部通过；双目标Release 0错误/464警告。默认布局18独立STA场景512行固定10.0.11参考/JIT/实际AOT严格全同，全部exit0/complete=true/无超时；参考默认AnchorLayoutV2=false，显式true独立进程另测。旧事务394行参考/JIT原七行差异已修，生命周期204行/集合391行参考/JIT同；旧探针AOT本批不重复，集合32行非零下界数组fixture限制继续保留。

默认布局实际AOT3,041,280字节、CLR目录0、无IL2xxx/IL3xxx。Demo实际AOT4,192,256字节、CLR目录0，真实显示/复选框0→1/正常exit0/记录HWND全部失效，无强制清理；既有InheritanceAttribute IL3050保留。最初两个测试失败、探针fixture编译失败及中间9行差异原始记录保留；Splitter旧无Dock构造经独立固定参考确认无效后改为标准Dock/集合顺序，条带旧断言按参考真实几何修正，未降低检查。

13项目/17目标仅.NET10，Windows官方Drawing10.0.0，原始Demo源码/Designer/resx和net10.0依赖边界不变。新增标准API已重生成固定1,095类型/61,240行/187页清单，运行仍unverified；逐控件实施列与tasks.csv重生成。完整路径/进程/PE/警告/计数在artifacts/compatibility-batch021/evidence.json。复杂布局缓存重入、全部派生/DPI/RTL/滚动/FlowTable、完整Windows Control/Designer/资源/模态及Unix未关闭；不由这些数量计算98%。

## 批次 022 · 2026-10-16 字体与颜色继承及必要资源

批次022完成Control-11b基础Font/BackColor/ForeColor局部值、父级继承/Reset/通知及08b借用Font/独占HFONT寿命，必要Form/TextBox/GroupBox/Tab/条带消费接入。16场景212行JIT与实际AOT全同；对固定10.0.11仍有13条真实差异：10条基础Control缺UserPaint路径的WM_GETFONT，3条默认palette前景色，strictMatches=false。4,138项测试（新增31）通过，旧默认布局512/事务394/生命周期204/集合391行参考/JIT均同；双目标Release 0错误/478警告，Demo AOT真实显示、复选框0→1、正常exit0且记录HWND全失效。探针2,971,136字节、Demo4,196,352字节，CLR目录均0；探针无IL2xxx/IL3xxx，Demo既有IL3050保留。完整Styles/UserPaint/消息绘制、其余ambient来源/RightToLeft/Cursor、字体自动缩放/DPI、全部派生布局/输入、Designer/资源/模态与GTK4/AppKit仍缺；父任务保持部分，不宣称整Control或98%。

固定参考run 20261016-082323-2290099；最终JIT/AOT run 20261016-084839-8131815，同源SHA256为21D6E69FD6F219334C05EBEFFF06EA6B419C6B8C6C57C5756C1040047739B96A。48个场景进程均exit0/complete=true/无超时；严格驱动因13条参考差异exit1，不能将进程完成写成参考全通过。元数据只核选定签名/可见性/虚槽及Browsable/Localizable/AmbientValue构造，不含完整COM/中文Designer。

首次fixture两次编译失败及首次4136/4138、资源诊断失败保留。MenuStrip真正修复标题宿主类型误判导致字体布局替换；Form WM_GETFONT=0符合其窗口过程，资源测试改查实际持有HFONT。旧GetObjectType对已删除对象仍回type6，实际本机GetObjectW=0且字段0/Disposed/窗口消失，据此采用联合释放断言，不延时或手动删除目标句柄。完整日志、PE、项目依赖与索引结果见artifacts/compatibility-batch022/evidence.json和[022](batches/022.md)。

按实际代码纠正GroupBox已继承Controls/部分默认/Win32、Form旧缩放/原生窗口、TabControl/ToolStrip/MenuStrip原生路径及Control/FND绘制局部代码的全待办错标；未完父项/ID/固定分母保留。13项目/17目标仅.NET10，原Demo源码/Designer/resx未改，Windows官方Drawing10.0.0且net10.0无WinForms/Resources.Extensions/自有Drawing。API重导出及README实施列/tasks.csv由原工具重生成，所有运行状态仍unverified，不从数量推98%。

## 批次 023 · 2026-10-16 标准消息、样式与基础绘制

Control-14a/17a、Form-16a明确范围代码完成，父项及整Control保持部分。多个内部智能体分别负责核心、契约/Form、参考探针，互斥文件；主统一顺序集成、文档/API/索引和Git。4,158项测试全部通过，新增20项，0失败/跳过；双目标Release 0错误/635警告。默认布局512、事务394、生命周期204、集合391行旧参考/JIT回归全同，旧AOT不重复声称通过。

18独立STA场景558行JIT严格同固定WinForms10.0.11；实际AOT运行558行，554相同，4条custom CreateParams.Param差异保留strictMatches=false/aotMatches=false，精确边界aotInScopeMatches=true。最终run 20261016-101835-1132631，参考run 20261016-091744-1927945，源码SHA256 B9964B910AE3E42AF3D2E988BC0E806C924EA43186B20CF1D78CBDD9B62E18F7；三端54个场景进程均exit0/complete/无超时。Message.GetLParam(Type)的IL3050仍在，显式根化Payload成功不证明任意marshaller。

外观最终run 20261016-101947-4132109由原batch022驱动保存：16场景212行JIT/AOT严格全同，对旧固定参考仍3条默认palette前景差异，driver exit1；原10条字体消息差异由真实UserPaint窗口修复。原022的13条失败记录保留。消息/外观AOT探针分别3,088,896/3,091,968字节，Demo4,265,984字节，三者CLR目录0；Demo实际显示、复选框0→1、正常exit0、记录窗口全销毁/无强制清理，既有InheritanceAttribute/Enum.GetValues(Type) IL3050保留。

公共OnPrint借用Graphics并保留背景回调的状态修改；native打印在前景前恢复背景状态。真实背景/前景绘制异常报告原异常并绘制后续错误表面；Form截获或抛NCDESTROY仍清root/映射/画刷，Control完成失败仍释放句柄/HFONT。透明失效回调移除/Dispose子项的重入已有回归。另确认已释放AsyncWaitHandle使WindowThreadDispatcher.Close中断余下队列、DestroyHandle预先Close可能抛出，保留Control-04/FND-002并优先留024。

证据限于明确场景：透明/alpha探针没有HWND，PRINTCLIENT像素为位图，不证明屏幕透明合成；隐藏窗口失效主要核参数/通知。BackgroundImage/Layout、可见透明/完整owner draw/消息反射/缓冲失败回退/压力、完整输入/ambient/DPI/Designer/资源/模态及GTK4/AppKit仍未完成。13项目/17目标均.NET10，原始Demo/Designer/resx未改，Windows官方Drawing及net10.0依赖边界保持；API与任务索引重生成，固定1,095类型/61,240行/187页/运行unverified不变。全部证据和原失败见artifacts/compatibility-batch023/evidence.json及[023](batches/023.md)，不从任务或测试数推导98%。

## 批次 024 · 2026-10-16 投递关闭与背景绘制

批次024完成Control-04b/14b、Form-16b：关闭/失败投递完成余队列后保留错误，真实销毁HWND；补背景图像五布局、真实父子透明/alpha与DC/Graphics状态，Form取消默认COMPOSITED修复失效区域。4,188测试（新增30）通过，双目标Release 0错误/641警告。21场景307行JIT/实际AOT全同，对固定10.0.11保留16行关闭增强差异及严格失败；6组旧参考/JIT回归全同，外观仍3条palette差异。背景/Demo AOT为3,450,880/4,284,928字节，CLR目录0；Demo实际显示/复选框0→1/exit0/窗口全销毁。既有IL3050和023自定义Param AOT四差异保留；普通Drain、InvokeAsync关闭、native双错/Dispose后续清理、完整输入/绘制/Designer/资源/DPI及GTK4/AppKit仍缺。

固定参考/JIT/AOT的完整run、源码hash、三端63个进程记录和明确未验范围见[024](batches/024.md)及artifacts/compatibility-batch024/evidence.json。屏幕采样与PRINTCLIENT位图分列，direct无HWND不算绘制验收；高对比设备切换、全派生/Designer/跨平台仍待。首轮定向28/30、首轮17行差异与独立去COMPOSITED诊断原始记录保留；修复fixture与正式样式后仅16行有意关闭差异。7组旧驱动仅重跑JIT，消息/外观/集合旧AOT限制不覆盖。13项目/17目标仅.NET10、官方Drawing/固定双目标/原始Designer资源保持；重新导出API与逐控件状态索引，固定分母与unverified不变，不从测试或任务数推98%。

## 批次 025 · 2026-10-16 执行完成与原生异常边界（补录历史）

本页原遗漏025摘要，026收尾按025原始证据补录，不声称全部在026重跑。025完成Control-04c/07b/17b、Form-16c、WindowsFormsSynchronizationContext-06a；4,195测试通过，双目标Release0错误/641警告，8场景JIT/AOT全同，固定参考4同/4差异。探针2,989,056字节、Demo4,287,488字节，CLR目录0；Demo显示/复选框/正常退出/原始HWND销毁有证据。原实现82b9652、文档74372ff、交接f9a3a3e已推送，见[025](batches/025.md)及artifacts/compatibility-batch025/evidence.json。

## 批次 026 · 2026-10-16 异步终止与释放收尾

Control-05c/08c明确范围完成；全部4,243测试通过（新增28+20），双目标Release0错误/643警告。25个固定WinForms10.0.11场景中16严格同、4项queued关闭增强和5项native-after释放增强；另7旧Task仅Lumina，不纳入参考分母。32场景JIT/实际AOT严格同、全部进程exit0/complete=True，但参考strictMatches=false、driver exit1保留。固定参考run20261016-210951-8765335，最终JIT/AOT run20261016-211451-3933342，源码SHA256 CCEA907A90096CF37AEB12372B978D9C95BE4FB7A38B0B2C13FDA43B6216D5F4。750ms pending为有界观察，started中间门控pending不算最终悬挂；Lumina所有最终Task均结算。

6组旧参考/JIT回归：投递69/context57/生命周期204行全同；Action59与ValueTask90各两行关闭增强差异并保留严格失败；025仍四同四差异。旧日志已先保存，旧AOT未重跑。探针AOT3,851,776字节、Demo4,289,024字节且CLR目录0；Demo真实显示/Enable option 0→1/WM_CLOSE exit0/原始HWND全失效、无强制清理。原始Events遮蔽编译失败、Site先命中child的中间诊断与最终target-site参考均保留；Container.Add IL2026、TypeConverter IL3053和Demo既有IL3050等警告不隐藏。

13项目/17目标仅.NET10，Windows官方Drawing10.0.0、固定双目标/原始Designer资源及net10.0无WinForms/Resources.Extensions/自有Drawing依赖保持；公共签名未变，固定1,095类型/61,240行/187页和运行unverified不变。修正Form-07全待办和索引引言过时状态；普通Parent/child/Site再失败仍短路，完整Dispose/并发/输入/绘制/Designer/资源/模态及GTK4/AppKit未完成。详见[026](batches/026.md)与artifacts/compatibility-batch026/evidence.json，不由测试数推98%。
