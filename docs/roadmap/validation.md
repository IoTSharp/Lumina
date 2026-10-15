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

## 沿用的前轮运行证据

[首轮审计](../assessments/2026-10-15/README.md)：完整Demo的net10.0/win-x64 Native AOT发布成功，EXE 4,083,712字节、无CLR Header，仍有IL2026/IL3053。短时隐藏启动只看到辅助窗口，没有完成主窗交互与正常退出，后续需可见运行定位。**本轮没有将该项改为通过。**

## 尚未验证

- ⬜ 187组类型的完整属性/事件/方法/行为及公开接口支持率。
- ⬜ 完整Demo的AOT可见主窗、操作与正常关闭；资源警告闭环。
- ⬜ Visual Studio实际设计器打开/保存/重开及所有控件资源往返。
- ⬜ Linux GTK4/macOS AppKit原生后端、各平台Native AOT、真实设备输入法/辅助技术/部署。
- ⬜ System.Drawing.Common公开类型身份的跨平台可行路线。

不会因为规划已细化而宣布以上能力完成；下一项实施按 [M0排序](foundations.md)。
