# LuminaForms 实施交接

更新：2026-10-16。先读本文件、[项目需求](PROJECT_REQUIREMENTS.md)，再读 [执行顺序](docs/roadmap/execution.md) 和当前批次；不要重新评估整个项目。

## 当前接续点

- 调度状态：017两项明确范围代码完成，必要验证通过，正在完成路线图/索引及提交推送收尾。用户于2026-10-16直接要求“当然任务没有完成的我们要完成，完成后结束”；本次完成017后结束，不开启下一批、不创建接续聊天、不恢复巡检。本条覆盖下文历史连续接续规则；代码完成不代表完整兼容或真机验收。
- 监督会话：`01a13bd2-e505-79c0-ab49-1d428c85f592`，已确认本轮只完成017后结束，并确认旧主已归档。
- 当前编码主会话：`01a14163-281b-7980-9fa1-558752cecf8b`（批次017，已按用户更新指令实际接管）。接管时工作区干净，HEAD/origin/main/远端refs/heads/main均为交接SHA `2b842071971245bf83a73136bbc269484a45daa7`；已向旧主`01a14140-d125-7750-bce4-b827797c7413`和监督回传证据，旧主保持停止写入。016实现SHA为`10c106002acff0517bcb547f526fef446ec32871`，不得与交接SHA混同。
- 巡检：监督已确认自动化 `luminaforms` 为PAUSED，本轮不恢复；完成017后结束。
- 当前批次：[017 · 控件文本重置与列表项图像选择状态首范围](docs/roadmap/batches/017.md)，两项代码与必要验证完成，提交推送收尾中；001～016历史子范围保留，未完成父项和验收仍单列。两个内部子任务均已完成并停止写入，不存在下一批编码主。
- 最近实现提交：`10c106002acff0517bcb547f526fef446ec32871`（批次016），已成功推送origin/main；推送后HEAD/origin/main/远端refs/heads/main均为该SHA，工作区干净。本记录随后单独提交并成功推送归属交接，新主实际接管时核对并记录其SHA。015实现`21a71fc3646c736baa59132b39d5270a135e8688`/交接`79ccc4a9b4a177beccf6bdb79528906fc16b3f41`、014实现`3894f281a88ff00e806c14440ce5c332af974d6a`/交接`7f1d0709d32ff95340a2a0611a6cc603831f8d39`及013实现`a0e2ec02e15cc92f7f337812bb22b13b17f0a691`/交接`2434d48bbecda25112951cb11f2962c9b7761634`均已验证；原代码基点`b83f534`不是回退要求。
- 全量进度：[tasks.csv](docs/roadmap/tasks.csv)，由控件页及 FND/REP/CNS/XPL 任务表生成；逐 API 分母仍在 [api](docs/roadmap/api/baseline.json)。
- 当前边界：本批明确实现范围已完成。资源严格告警门禁仍有已定位IL3050（标准Container/InheritanceAttribute），FND-006保持部分完成；图像设计器转换器/编辑器和文本AOT的DispId删除差异保留，Windows/Linux/macOS真机后置。

## 批次017完成证据与结束范围

- Control-11a：virtual ResetText消费虚Text setter，保留虚getter判等、通知/原异常/重入、null/Unicode/NUL/引用和释放后托管边界，219项新增回归通过。3,774条固定10.0.11参考/JIT相同；实际AOT只有Text属性列表缺DispId=-517，另3,773条原始记录相同。实际编译参数及CoreLib内嵌RemoveAttributeInstances证实原因，原始差异/首次失败保留，strictMatches=false，不改库AOT策略。
- ListViewItem-04b：未挂接ImageIndex/ImageKey默认、验证/失败状态、key-index互斥及重复/引用身份完成，518条固定英文参考/JIT相同，363项新增回归。首项恢复/三枚举/各view key缓存/样式/借用资源保持；NoneExcludedImageIndexConverter、ImageKeyConverter和ImageIndexEditor仍缺，新范围未计实际AOT运行验收。
- 集成：3,934项测试全部通过，0失败/跳过；29个旧驱动通过；双目标Release 0错误/最终增量301警告。文本AOT EXE2,049,024字节实际运行，无IL2xxx/IL3xxx；完整Demo AOT4,108,288字节仅发布，二者CLR Header 0。Demo既有IL3050、主窗交互/正常关闭、设计器及三平台验收保持待完成。
- 13项目/17目标仅.NET10，Windows官方Drawing10.0.0，原始静态Designer/resx和net10.0依赖满足要求。API固定分母1,095类型/61,240行、运行unverified；405份Markdown/187组类型/API页/2,033项任务及187行实施索引检查0错误。Control-11和ListViewItem-04/FND-003保持部分完成，整类型代码完成0，没有整控件或98%声明。证据artifacts/compatibility-batch017/evidence.json。
- 本次按用户要求在017成功提交推送并核对远端后结束；不创建018、不发送接管指令、不恢复巡检。后续未完成任务保留原ID和优先级，继续工作须遵循后续用户指令。

## 用户已经确定的范围

用户于 2026-10-15 要求：提交推送后，在新会话用多个子智能体逐任务实现；每个子智能体负责一个任务，无文件冲突才并行；每批完成后新建会话接续；每半小时巡检；专注代码，不扩散、不做额外架构。用户授权该实施循环及每批必要的提交、推送、创建接续会话和监督消息；后续用户的暂停/停止指令优先。2026-10-16 补充：实现和交接都必须 commit 并成功 push，记录实现 SHA 和远端分支核对结果；仅本地提交不算批次完成。新会话实际接管后旧主会话立即停止写入并归档，不等待下一批结束。

2026-10-16 新增用户要求：每批任务结束必须检查并重新整理路线图，“把已完成的要标记，不要虚标”；历史已完成和未完成项也须按实际证据统一回标。用户进一步明确 docs/roadmap/README.md 要逐个控件标注。每批实现提交前同步 ROADMAP.md、执行总览、相关控件/公共任务页、修改历史、批次与本文件，用现有Validate-Roadmap.ps1 -ExportTasks同时重生成逐类型README的实施状态/完成子任务列和tasks.csv，再校验一致性。明确子范围全部实现才 ✅，父任务未完保留 🟡/🚧并列完成子范围和剩余项；未开始 ⬜，实际已分配才 🚧。文档/签名/编译/局部探针/基类完成不能关闭整个控件或平台，🧪仅限可复现的对应范围/平台证据；真机/设计器/跨平台继续待验收。发现历史错标要纠正说明，保留ID、提交历史和固定分母，不删除未完项或用计数推出98%。按真实剩余依赖与既有优先级整理下一批，不重做全项目评估、不扩大本批测试、不另建调度/报告框架，随批次提交推送，完整传给接续聊天。

固定 .NET 10.0.11 WinForms 基线。用户于本批进一步明确：所有项目（包括分析器和构建工具）只支持 .NET 10，使用 `net10.0` / `net10.0-windows`，不支持 `netstandard2.0` 或旧 .NET 目标。保留 `Lumina.Forms` 包/命名空间与 NativeFormsDemo 双目标：`net10.0-windows` 真实 WinForms/VS 设计器，`net10.0` Lumina.Forms/Native AOT。不引入替代 UI 栈。

2026-10-16 的 PROJECT_REQUIREMENTS.md 修订已读取（提交 f424523）：Windows 两目标均继续使用官方 System.Drawing.Common；Linux 的自有 Drawing/身份验证按 XPL 任务推进，不阻塞当前 Windows 兼容批次，也不把 net10.0 等同 Linux。

前期 **✅ 代码完成即关闭开发部分**：实现任务明确的范围，并通过必要编译/轻量检查即可。复杂 UI 自动化、真机、压力、设计器往返集中后置；后期先 Windows，再统一 Linux；macOS 尚无真机证据时如实留待验收。不得用空实现、常量成功或吞异常冒充完成。**🧪 验收通过**是另一状态，只有逐成员/场景证据才计入 98%。

## 每批怎样继续

1. 先查当前编码主会话、`git status`、批次任务和文件责任范围。同一工作区同一时刻只有一个编码主会话。
2. 主会话选 2～3 个有边界的任务，用内部子智能体分配，一人一任务；涉及 Control/Form/共享 interop 等同一文件的工作串行。主智能体保留共享文档、集成构建和 Git 操作。
3. 子智能体不提交、不推送、不创建接续会话，不覆盖其他人的修改；需要越过文件责任范围时先向主智能体报依赖。由主智能体统一构建，避免争用 obj/bin。
4. 主智能体集成后按真实实现与必要检查逐项复核本批及受影响路线图，并核实收到的历史回标清单；同步 ROADMAP/执行总览、控件/公共任务页与历史、本批记录/HANDOFF，重新生成 tasks.csv 并校验一致性，再 commit 并成功 push。记录实现 SHA，用远端 refs/heads/main 核对推送结果；仅本地提交不算批次完成。完整实现的子范围才 ✅，父项保留部分及明确剩余，待真机/设计器/跨平台单列；历史错标纠正说明且保留ID/分母。遇到局部阻塞先做其他可实施任务。
5. 每批结束后创建一个新的项目本地会话。先让新会话只读准备，旧会话把本文件的编码主会话 ID、下一批、文件责任、实现 SHA 和远端核对结果更新，commit 并成功 push 交接，再发送“开始接管”。新会话核实归属并实际接管后，旧会话停止所有写入/构建/Git，调用 set_thread_archived 归档（或由监督核对后归档），不能等待下一批全部结束。不要让多个主会话同时领取任务；接续提示保留全部授权、推送成功条件和归档规则。

巡检先读本文件，再查编码主会话状态和最新批次记录。会话正常运行时不另起竞争主会话；空闲但未完成时，按已有授权给该会话发送继续消息。批次完成但尚未接续时才创建下一会话。出现错误先读取必要上下文恢复，不在活跃写入时 reset/clean/换分支。全部开发任务完成后转入集中的 Windows 验收；最终无剩余任务或用户停止时停用巡检。

应用/机器需运行且有可用额度，定时巡检才可执行；该机制不承诺离线永久运行。

## 已有成果与检查

- 批次 001：FND-005、REP-002 及 ListViewItem 构造子范围完成；父任务 FND-003/ListViewItem-01/Application-01 保留部分完成。分析器也统一 net10.0；12 个项目均只声明 .NET 10 目标。
- 批次 001 检查：双目标 Release 构建 0 错误；92 项测试全通过；14 组 DPI 隔离对照与本机 WinForms 10.0.11 一致；两种真实 NuGet 消费各触发 LNF003 且无工具运行依赖；完整 Demo Native AOT 发布通过、EXE 无 CLR Header。复杂 UI/真机/设计器/跨平台验收后置，不能从上述结果推算 98%。
- 批次 002：可实例化 Control : Component/IWin32Window，Component Site/Container/Disposed/Disposing 与派生清理衔接；101 项纯托管测试、1 项基础 HWND 测试、5 项真实端生命周期观测通过。父 FND-001/Control-01/Control-08/FND-012 仍保留未完范围。
- 批次 002 资源：五张真实 Demo Bitmap 的 WinForms/Lumina JIT/Native AOT 像素、文化、身份、重载/GC/错误语义相同，IL2026 消除；资源脚本仍因 IL3050 严格失败。原 IL3053 已展开为 Container.ValidateName 根化 InheritanceAttribute.ToString/Enum.GetValues(Type)，未改标准容器、未全局抑制。原始 Designer/resx 不变，资源转接在库内 Lumina.Forms.Resources，obj 生成副本仅供 net10.0。
- 批次 002 最终主线双目标构建 0 错误；完整 Demo AOT 发布通过（4,085,248 字节），资源探针 AOT 已运行，两者无 CLR Header。13 项目/17 个目标仅 .NET 10；net10.0 Demo deps 无 WinForms/Resources.Extensions；完整主窗、设计器、Icon/Font/其他转换仍未验收。
- 批次 003：Control/Form 接入真实 HWND 投递、ISynchronizeInvoke、标准同步 Action/Delegate/args/Func 与 BeginInvoke/EndInvoke；69 条观测在 WinForms/JIT/Native AOT 三端相同。Application 新增线程局部 ThreadException/OnThreadException，未 EndInvoke 的异步错误也会报告；完整 InvokeAsync、同步上下文/Form 继承及广泛关闭竞态仍待实现。
- 批次 003 构造：ListViewItem 无参/string[]、null/空数组/null 元素、输入复制与延迟首项已完成，41 条差分相同。118 项测试全通过（117 托管 + 1 基础 HWND），主线双目标 Release 0 错误；13 项目/17 目标仅 .NET 10。Demo AOT 发布 4,094,464 字节，实际线程 AOT 探针 2,615,808 字节且运行对照通过，均无 CLR Header；Demo 的 IL3050 保留，线程探针无 IL2xxx/IL3xxx。原始 Designer/resx 无修改。
- 批次 003 明确差异：有 HWND 的 null Delegate 用 ArgumentNullException 防止复制参考端挂起；默认线程错误仅原生 OK 提示，缺标准 Quit/Details/Help/异常模式，错误事件处理器再抛时在 native 边界前 FailFast。完整类型兼容未验收，不能据此算 98%。
- 批次 004 文件责任：Control-05a 子智能体负责 Control.cs/Form.cs/WindowThreadDispatcher.cs 及独立异步检查；ListViewSubItem-01a 子智能体负责 ListViewItem.cs 及独立子项构造/现有构造回归。共享 Application.cs/Win32.cs、全部文档/集成/Git 由主会话协调；子智能体不构建、不提交。
- 批次 004：标准 Action/Func<T> InvokeAsync + CancellationToken 已真实排队，旧单 Action/Task 回调同样排队。59 条固定 WinForms 10.0.11/JIT/AOT 观测相同，旧 Task 专项也在 JIT/AOT 通过。空 callback/无自身 HWND 返回 Faulted Task；预取消成功跳过回调且 Func 返回 default，排队后取消/token 与回调原异常按参考端保留；Task 错误不报告 ThreadException。Dispose 丢弃的排队 Task 等待后续 token 取消，与参考端相同，完整无 token 关闭/重建/退出竞态仍待后续。
- 批次 004 子项：公共无参/owner-text、可继承性、Text/Name/Tag 与属性元数据已完成，37 条对照相同，新增 13 项无 UI 回归；原 21 项 ListViewItem 构造/41 条观测及旧投递 69 条回归继续通过。131 项测试全部通过（130 托管 + 1 既有基础 HWND），主线双目标 Release 0 错误/本次增量 318 警告。
- 批次 004 AOT：新异步探针实际运行三端对照，EXE 2,736,128 字节；完整 Demo 发布通过，EXE 4,094,464 字节，二者 CLR Header 为 0。探针无 IL2xxx/IL3xxx；Demo 详细告警仍为已定位 InheritanceAttribute/Enum.GetValues(Type) 的 IL3050。13 项目/17 目标仍仅 .NET 10，net10.0 deps 无 WinForms/Resources.Extensions/自有 Drawing，原始 Designer/resx 无修改。
- 批次 005 文件责任：Control-05b 子智能体负责 Control.cs/Form.cs/WindowThreadDispatcher.cs 及独立 ValueTask 检查；ListViewSubItem-01b 子智能体负责 ListViewItem.cs 与独立样式构造/未挂接样式检查。共享 Application.cs/Win32.cs、全部文档/API/索引、集成构建与 Git 由主会话协调，子智能体不构建、不提交；先跑固定 WinForms 10.0.11 参考端。ValueTask 初始 UI 调用与单次消费不等于 await 后 UI 恢复；已挂接 ListView 的样式继承/native/完整所属也继续保留。
- 187 组独立类型任务页、固定基线/API/接口/类型责任清单；遗漏审计与三平台逐控件方案已落盘。
- 批次 005：Control/Form 标准 ValueTask 两族在 UI 队列首次调用，固定参考/JIT/实际 AOT 的 90 条观测相同，含 IValueTaskSource 同步/延迟/取消后单次消费及结果/原异常/token；旧 Task 虚方法以 OverloadResolutionPriority(-1) 保留显式消费和派生 override，async token lambda 不歧义。旧 Action/Func 59 条三端、旧 Task 专项与同步投递 69 条回归通过。
- 批次 005 子项：五参样式构造、ForeColor/BackColor/Font 与 ResetStyle 的独立/未挂接 owner 子范围完成，另 37 条样式观测相同，15 项新增测试；全线 146 项通过。默认 WindowText/Window 与惰性共享官方 MessageBoxFont，保留借用 Font 身份/寿命；全局默认字体、已挂接样式/完整所属/native 仍缺。原基础子项 37 条、ListViewItem 41 条回归保留。
- 批次 005 集成：主线双目标 Release 0 错误/本次增量 318 警告；ValueTask AOT 探针实际运行通过，EXE 2,794,496 字节；完整 Demo AOT 发布 4,094,464 字节，二者 CLR Header 为 0。探针无 IL2xxx/IL3xxx；Demo 保留既有 InheritanceAttribute/Enum.GetValues(Type) IL3050。13 项目/17 目标仍仅 .NET 10，Demo 原始 Designer/resx 未改，net10.0 deps 无 WinForms/Resources.Extensions/自有 Drawing。
- 批次 006 文件责任：WindowsFormsSynchronizationContext-01a 子智能体负责标准显式 context 类型/投递辅助、Control/Form/dispatcher 与独立探针；ListViewSubItem-03a 子智能体负责 ListViewItem.cs 与独立 key 检查。Application/Win32、文档/API/索引、集成构建和 Git 由主会话协调。先固定参考端，再实现；AutoInstall/Uninstall/默认 await 恢复与完整线程/关闭/模态竞态留后续，不造空方法。key 子范围不关闭完整集合所属/native。
- 批次 006：显式标准 context 类型、无参构造/Post/Send/CreateCopy/Dispose 已实现；57 条固定参考/JIT/实际 AOT 观测相同，新增 7 项独立 STA 检查。message-only HWND 与既有 dispatcher 复用，创建登记 DPI，销毁释放 GCHandle；普通 Form 释放不影响目的地。同线程共享 Dispose 失效与重新构造仍失效按参考保留；null Post 接受、null Send 明确防挂差异（参考隔离超时，本实现 ArgumentNullException d）。AutoInstall/Uninstall/default await 恢复仍缺。
- 批次 006 子项：四个标准 virtual key 成员与 209 条参考/JIT 观测相同，新增 38 项回归。用一个最后命中索引保留 Name/集合修改后重复 key 选择；null/空保留缓存、非空 miss 清除；先 UTF-16 等长再 InvariantCultureIgnoreCase，含 Unicode/嵌入 NUL。集合已解除 sealed，完整公共构造/IList/整数索引/所属/native 仍缺。
- 批次 006 集成：191 项测试全部通过，主线双目标 Release 0 错误/本次增量 318 警告；旧线程69/Action59/旧Task/ValueTask90、Item41与子项基础/样式各37回归通过。context AOT 实际运行 EXE 2,688,512 字节，Demo AOT 4,094,464 字节，两者无 CLR Header；探针无 IL2xxx/IL3xxx，Demo 既有 IL3050 保留。13 项目/17 目标仍仅 .NET 10，net10.0 deps 与原始 Designer/resx 保持要求，API 分母仍 1,095/61,240 且运行状态 unverified。
- 批次 007 文件责任：WindowsFormsSynchronizationContext-01b 子智能体负责 context/Control/Form/Application/dispatcher 与独立默认安装探针；ListViewSubItem-02a 子智能体负责 ListViewItem.cs 与独立集合构造/整数索引/共享视图检查。Win32 必要衔接、文档/API/索引、集成/Git 由主会话协调；先固定参考。默认安装必须实际接入，不能只存 AutoInstall 或空 Uninstall；集合必须处理真实共享 owner 数据，不能只开放构造可见性。完整多线程/模态/退出与 IList/native 仍保留。
- 批次 007：默认 AutoInstall/Uninstall、构造/现有消息循环安装还原与 callback await 恢复完成，204 条固定参考/JIT/实际 AOT 相同；公共集合构造/共享 owner 视图/整数索引的 72 条对照相同。236 项测试通过（新增 18 STA + 27 集合）；旧显式 context57、线程69、Action59/旧Task、ValueTask90、Item41、子项基础/样式各37、key209 回归保留。
- 批次 007 原生退出：无 Form ExitThread 暴露提前 DeactivateActCtx SEH，改为退出请求与最外层 finally 释放本线程激活资源；两个不同 STA 无 Form 循环通过。Form 仍按进程全局 OpenForms 计数决定关闭后退出，完整多 UI/ApplicationContext/模态/线程退出和竞态未关闭。
- 批次 007 集成：主线双目标 Release 0 错误/本次增量 315 警告；安装探针 AOT 2,750,464 字节、Demo AOT 4,105,216 字节，均无 CLR Header；探针无 IL2xxx/IL3xxx，Demo 保留既有 IL3050。13 项目/17 目标仍仅 .NET 10，原始 Designer/resx 与 net10.0 依赖满足约束；API 分母 1,095/61,240 与运行 unverified 状态不变。
- 批次 008 文件责任：Control-02a 子智能体负责 Control.cs 和独立焦点 API/真实 HWND 检查；ListViewSubItem-02b 子智能体负责 ListViewItem.cs 与三个 typed Add 重载的返回/参数/样式/共享视图检查。Win32 必要衔接、Form 依赖、文档/API/索引、集成/Git 由主会话协调；先固定参考，不扩完整焦点键盘/布局和 IList/AddRange/native。
- 批次 008：标准 bool Focus、virtual Focused、ContainsFocus/CanFocus 与标准元数据完成，157 条固定参考/JIT/实际 AOT 相同；三个 typed Add 的 74 条参考/JIT 相同，历史泛型 void 显式适配保留。真实 HWND 状态/无句柄不创建、重复/其他 owner 对象、共享视图/独立 key 缓存和借用 Font 寿命有对照；完整 Select/键盘/布局、IList/AddRange/所属/native 仍缺。
- 批次 008 集成：主线双目标 Release 0 错误/本次增量 312 警告；293 项全线测试通过，新增 17 焦点 + 40 Add。旧线程69/Action59/旧Task/ValueTask90/context57/默认安装204、Item41/子项基础样式各37/key209/集合72 参考/JIT 回归保留。焦点 AOT 2,552,320 字节、Demo AOT 4,105,216 字节，二者无 CLR Header；焦点实际运行通过且无 IL2xxx/IL3xxx，Demo 既有 IL3050 保留。13 项目/17 目标仅 .NET 10，原始 Designer/resx 与 net10.0 依赖满足约束；API 分母 1,095/61,240 与运行 unverified 状态不变。
- 批次 009 文件责任：Control-02b 子智能体负责 Control.cs 与独立零尺寸/负尺寸归一/真实 HWND 矩形检查；ListViewSubItem-02c 子智能体负责 ListViewItem.cs 与三个 typed AddRange 的 null 数组/元素、顺序/身份、样式/借用 Font 和共享视图/key 缓存检查。Win32 必要衔接、文档/API/索引、集成/Git 由主会话协调；先固定参考，构造默认大小/全部派生约束/完整布局与 IList/Insert/所属/native 仍另列。
- 批次 009 尺寸：无自身 HWND 时保留零/负尺寸且不创建句柄；有 HWND 时负数归零，实际零矩形可恢复。首次创建保留 USER32 原参数解释后 GetWindowRect 回读，HandleCreated 已见归一，int.MinValue 宽度对应 0×0。756 条固定参考/JIT/实际 AOT 相同，33 项新增回归；未改构造默认、全部派生尺寸限制、Form.ScaleSize、Min/Max、完整布局/事件。
- 批次 009 子项：三个 typed void AddRange 完成，null 数组抛 items 且不改状态，三个重载均跳过 null 元素，包括文字数组；非 null 空 Text 对象/空字符串保留。151 条参考/JIT 相同，121 项新增回归；原构造 null 归空、单项 Add、身份/复制/重复/其他 owner、借用 Font、共享视图/主项 Text/各视图 key 缓存均保留，完整 IList/Insert/Remove/所属/挂接样式/native 仍缺，新 AddRange 未提前计实际 AOT 运行验收。
- 批次 009 集成：最终 447 项测试全通过，主线双目标 Release 0 错误/最后成功增量 45 警告；旧组件5/线程69/Action59/旧Task/ValueTask90/context57/默认安装204/焦点157/Item41/子项基础样式各37/key209/集合72/Add74 参考JIT全部保留。尺寸 AOT 实际运行 756 条通过，EXE 2,562,048 字节，Demo AOT 4,105,728 字节，二者无 CLR Header；尺寸无 IL2xxx/IL3xxx，Demo 既有 IL3050 保留。13项目/17目标仅.NET10，原始Designer/resx及net10.0依赖满足约束，API运行状态仍unverified。
- 批次 010 文件责任：Control-02c 子智能体负责 Control.cs、新 BoundsSpecified.cs 和独立指定边界/派生调用探针；ListViewSubItem-02d 子智能体负责 ListViewItem.cs 与独立 typed Insert/void Remove/RemoveAt/Clear 检查。Win32必要衔接、现有测试必要适配、文档/API/索引、集成/Git由主聊天协调；先固定参考，不扩完整布局/事件与非泛型IList/所属/native。
- 批次 010 指定边界：BoundsSpecified 八标准值/Flags、x/y 命名 SetBounds 两族与 protected virtual SetBoundsCore 完成；外层保留未指定分量、未知位原样传递、有效矩形未变不调用，core 接完整矩形及派生调整。2,034 条固定参考/JIT/实际 AOT 相同，60 项新增回归；旧尺寸/首次创建回读/GetNativeHeight/OnBoundsChanged 保留，完整事件/布局/MinMax/派生约束仍缺。
- 批次 010 子项：typed Insert 先 index 后 item；公开非虚 void Remove 用引用身份只删首项，null/缺失无操作，派生 Equals 不影响；历史泛型 bool Remove/Insert 显式保留，Clear/RemoveAt 维持接口 virtual-final。203 条参考/JIT 相同，110 项新增回归；共享视图/Text恢复/独立key缓存/借用Font保留，非泛型IList/Contains/IndexOf/CopyTo完整契约与所属/native仍缺，新范围未提前计实际AOT运行验收。
- 批次 010 集成：617 项测试全通过，主线双目标 Release 0 错误/本次增量 304 警告；组件5/线程69/Action59/旧Task/ValueTask90/context57/默认安装204/焦点157/旧尺寸756/Item41/子项基础样式各37/key209/集合72/Add74/AddRange151参考JIT全部保留。指定边界实际AOT运行2,034条通过，EXE2,576,896字节，Demo AOT4,105,728字节，二者CLR Header为0；新探针无IL2xxx/IL3xxx，Demo既有IL3050保留。13项目/17目标仅.NET10，原始Designer/resx与net10.0依赖满足要求，API运行状态仍unverified。
- 批次 011 文件责任：Control-10a 子智能体负责 Control.cs 与独立位置尺寸通知探针/测试；ListViewSubItem-02e 子智能体负责 ListViewItem.cs 与独立非泛型 IList/ICollection/必要 typed 查询复制探针/测试。Win32 必要衔接、现有测试适配、文档/API/索引、集成/Git由主聊天协调；先固定参考，完整客户区/外部原生自主移动/布局、完整枚举/并发/所属/native另列。
- 批次 011 通知：四个标准事件/protected virtual On* 接入七属性/两族 SetBounds/直接 core，2,280 条固定参考/JIT/实际 AOT 相同，99 项新增回归通过。整矩形先提交、两族变化先快照，顺序 Move→LocationChanged→Resize→SizeChanged；OnSizeChanged 向 OnResize 传 Empty，SizeChanged 保留原参数。无 HWND/直接 protected 调用同步抛原异常，有 HWND setter 转既有 ThreadException；旧 OnBoundsChanged 独立保留，不依赖派生调用 base。
- 批次 011 子项：非泛型 IList/ICollection 与 typed 引用查询/复制首范围完成，1,406 条固定参考/JIT 相同，270 项新增回归。对象变更类型先于位置，重复 Add 返回首次引用索引；CopyTo 为 private 显式 dest/index，空集合跳过验证、不恢复首项，非空保留 rank/索引/容量/类型顺序、非零下界与派生数组部分写入。各 view SyncRoot 为自身，IsSynchronized 按参考 true，不代表完整并发已验收。历史泛型/typed 消费、独立 key 缓存及借用 Font 保持，完整枚举/所属/native仍缺。
- 批次 011 集成：986 项全线测试通过，主线双目标 Release 0 错误/本次增量 301 警告；组件/投递/异步/context/焦点/旧尺寸/指定边界及全部子项旧参考JIT回归通过。通知实际AOT运行2,280条相同，EXE2,618,880字节，Demo AOT4,106,240字节，两者CLR Header为0；新探针无IL2xxx/IL3xxx，Demo既有IL3050保留，新子项接口未计实际AOT运行验收。13项目/17目标仅.NET10，原始Designer/resx和net10.0依赖满足约束，API运行状态仍unverified。
- 批次 012 文件责任：Control-09a 子智能体负责 Control.cs 与独立客户区/ClientSizeChanged 探针/测试；ListViewSubItem-02f 子智能体负责 ListViewItem.cs 与独立枚举消费/修改轨迹探针/测试。Win32必要interop、现有测试适配、文档/API/索引、集成/Git由主聊天协调；先固定参考，完整布局/所有派生约束/外部原生自主通知、多线程并发/压力/完整所属/native另列。
- 批次 012 客户区：ClientSize/ClientRectangle/ClientSizeChanged、SetClientSizeCore/OnClientSizeChanged 完成，5,556 条固定参考/JIT/实际 AOT 全同，80 项新增回归。独立客户区缓存保留原始请求与重复通知；缓存变化使仅位置操作也可触发标准尺寸链，四有限边框/raw负值/重入/原异常有证据，旧边界/GetNativeHeight/OnBoundsChanged 保留。构造默认/SizeFromClientSize/全部派生/外部自主通知/完整布局另列。
- 批次 012 枚举：public IEnumerator/直接 IEnumerable virtual-final 及历史泛型显式消费完成，2,531 条固定参考/JIT 全同，347 项新增回归。结构变化使 MoveNext/Reset 失效但 Current 缓存保留；空 Clear 不失效、owner.SubItems 恢复首项换表，全部 view 共享当前表、旧空枚举器仍为空。历史泛型 typed 无效 Current=null 另记；并发/完整所属/挂接样式/native 未关闭。
- 批次 012 集成：1,413 项全线通过，双目标 Release 0 错误/最终增量298警告，19个既有差分驱动全部通过。客户区实际 AOT5,556条相同，EXE2,592,256字节、无IL2xxx/IL3xxx；Demo AOT4,107,264字节，两者CLR Header0。Demo既有IL3050和新枚举未计实际AOT运行验收的边界保留；13项目/17目标仅.NET10、原始Designer/resx/net10.0依赖满足要求，API1,095类型/61,240行与unverified不变。证据 artifacts/compatibility-batch012。
- 批次 013 文件责任：Control-09b 子智能体负责 Control.cs 与独立 SizeFromClientSize 元数据/调用/换算探针测试；ListViewSubItem-06a 子智能体负责 ListViewItem.cs 与独立 ToString 探针测试。主聊天保留Win32必要interop、现有测试适配、全部共享文档/API/索引、集成与Git；先固定参考、主聊天统一顺序运行。
- 批次 013 换算：标准 protected virtual SizeFromClientSize 完成，10,086 条固定参考/JIT/实际 AOT 全同，257 项新增独立回归。四有限边框/无有 HWND/正零负极值溢出/覆盖/base 绕过/重入/原异常均有证据；ClientSize/SetClientSizeCore 按参考不消费此虚扩展，原路由保留。直接 base 不创建句柄、不改外框/客户区缓存或发通知，009～012 边界/通知及旧 GetNativeHeight/OnBoundsChanged 保留。
- 批次 013 字符串：标准子项 ToString override 完成，58 条固定参考/JIT 全同，63 项新增回归。原样文本/空值/Unicode/换行/嵌入 NUL、共享 view/重复/移除/跨 owner/三枚举与独立 key 缓存、派生分派/隐藏成员/原异常保留；读取不恢复首项/改集合/读 Tag/样式/消费或释放借用 Font。Clone/序列化/完整所属/已挂接样式/native 仍缺，新字符串未记实际 AOT 运行验收。
- 批次 013 集成：1,733 项全线通过，双目标 Release 0 错误/本次增量 302 警告，21 个既有差分驱动全部通过。换算实际 AOT 10,086 条相同，EXE 2,610,176 字节、无 IL2xxx/IL3xxx；Demo AOT 4,107,264 字节，两者 CLR Header 0。Demo 既有分析警告、完整主窗操作/正常关闭及复杂 UI/设备/设计器后置。13 项目/17 目标仅 .NET 10，原始 Designer/resx/net10.0 依赖满足要求；API 1,095 类型/61,240 行与 unverified 不变。证据 artifacts/compatibility-batch013。应用工具已确认批次 012 旧主归档，没有等待本批结束。
- 批次 014 文件责任：Control-09c 子智能体负责 Control.cs 与基础 MinimumSize/MaximumSize/必要默认扩展的独立参考/实现/测试；ListViewItem-04a 子智能体负责 ListViewItem.cs 与 Name/Tag/ToolTipText 及首子项/共享 view 的独立参考/实现/测试。主聊天保留必要 Win32 interop、现有测试适配、全部共享文档/API/索引、集成与 Git；仍先只准备参考，主聊天统一顺序运行后再授权实施，完整边界见批次 014。
- WPF 适配项目及发布入口移除；独立美化/注入/效果展示隔离到 `Lumina.Effects.slnx`。Core、Ext.WinForms 与双目标 Demo 保留。
- 上轮主线 Release 构建 0 错误；效果线 0 错误；77 项现有测试通过；4 个主线 NuGet 包本地打包成功。现有警告和真机缺口见 [验证记录](docs/roadmap/validation.md)。
- 重新生成任务索引：`./tools/compatibility/Validate-Roadmap.ps1 -ExportTasks`。无需每批重新导出全部 61,240 行 API；公共签名有变化后按需更新。

## 批次 015 新增接续证据

- Control-01b：基础 0×0、protected virtual DefaultSize 与构造期默认 Min/Max 的重复虚读取/公共 setter 消费完成，2,677 条固定 10.0.11/JIT/实际 AOT 全同，387 项新增回归。DefaultSize 直接提交宽高、保留重入位置，双轴非零才重算边框客户区；零轴保留重入缓存。context 在默认初始化成功后安装；默认扩展抛原异常时与派生构造体异常的 Current 不同，不能混记。
- ListViewItem-01c：标准 ToString override/Object 槽完成，96 条固定 en-US 参考/JIT 全同，98 项新增回归。读取保留原样 Text、首项恢复/表身份、共享 view/三枚举/独立 key 缓存、Name 代理与独立 Tag/Tooltip；不消费 Tag 或借用 Font。新范围未计实际 AOT 运行验收。
- 集成：2,713 项测试全部通过、0 失败/跳过，25 个旧差分驱动通过，双目标 Release 0 错误/本次增量 300 警告。构造 AOT 实际运行 2,677 条相同，EXE 2,020,864 字节、无 IL2xxx/IL3xxx；Demo AOT 4,108,288 字节，两者 CLR Header 0。Demo 既有 IL3050 与主窗运行、中文 Designer 本地化/AOT 动态属性转换、完整派生默认/构造/布局/Parent/Controls/Form 继承和列表所属/native/资源仍未关闭。
- 13 项目/17 目标仅 .NET 10，原始静态 Designer/resx 与 net10.0 依赖满足要求；API 分母仍 1,095 类型/61,240 行、运行 unverified。404 份 Markdown/2,031 项任务结构校验无错误。证据 artifacts/compatibility-batch015/evidence.json。代码完成不计为 98% 兼容验收。
- 下一批 016：Control-01c 子智能体独占 Control.cs 与独立无 parent 的 text/坐标尺寸构造探针及新测试；ListViewItem-06a 子智能体独占 ListViewItem.cs 与独立未挂接 BackColor/ForeColor/Font/UseItemStyleForSubItems 探针及新测试。先固定参考、主聊天统一顺序运行后授权实现，主聊天保留共享文档/API/索引、必要 interop、现有测试适配、集成与 Git；含 parent 构造与 Form 继承不在本批模拟。

## 批次 016 新增接续证据

- Control-01c：两个无parent文本构造与所需Text虚槽完成，text为string?，默认初始化/context→虚Text→分别Location/Size；4,584条固定10.0.11参考/JIT/实际AOT指定观测相同，311项新增回归。真实空值差异已修正，NullableContext放置与调用约定原始编码保留，精确#ctor/Text保留仅在探针，未改变库裁剪策略。
- ListViewItem-06a：未挂接BackColor/ForeColor/Font为当前首子项代理，空表getter不恢复、Empty/null setter也换表恢复；主/子项共享惰性默认Font，UseItemStyle默认true独立保存。541条固定英文参考/JIT相同，328项新增回归；新范围未计实际AOT运行验收，已挂接/native/全局字体/完整所属和资源仍缺。
- 集成：3,352项全线通过，0失败/跳过，27个旧驱动通过；双目标Release0错误/本次增量301警告。构造实际AOT EXE2,075,648字节，无IL2xxx/IL3xxx；完整Demo AOT4,108,288字节，二者CLR Header0。Demo详细警告仍为既有InheritanceAttribute/Enum.GetValues(Type) IL3050，主窗交互/正常关闭未验收。
- 13项目/17目标仅.NET10、官方Drawing10.0.0、原始静态Designer/resx和net10.0依赖图保持。API1,095类型/61,240行/运行unverified；405份Markdown/2,033项任务/187行逐类型实施索引校验0错误。已合并监督三组历史只读核对清单，修正当前总览/任务页的过时待办与引用，不改历史批次快照或固定分母；没有整控件代码完成/98%声明。
- 下一批017文件责任：Control-11a独占Control.cs及独立无HWND Text/ResetText探针/新测试；ListViewItem-04b独占ListViewItem.cs及独立未挂接ImageIndex/ImageKey探针/新测试。先固定参考，主聊天统一顺序运行后授权实现；共享文档/API/索引、集成/Git仍由主聊天独占。Parent/Controls/Form及图像列表/native/完整所属不模拟关闭。
- 本批完整证据：artifacts/compatibility-batch016/evidence.json。实现已成功推送`10c106002acff0517bcb547f526fef446ec32871`并核对三SHA/干净状态；017只读接续聊天`01a14163-281b-7980-9fa1-558752cecf8b`已创建。本记录另提交推送归属交接，核对成功后才发送开始；新主实际接管后旧主立即归档。

## 接续提示

“继续 D:\source\Lumina 的 LuminaForms 路线图实施。先读 AGENTS.md、PROJECT_REQUIREMENTS.md、HANDOFF.md 和当前批次，收到‘开始接管’并确认归属与 Git 状态后立即分配两个互不冲突的内部子任务。所有项目（含分析器/工具）只支持 .NET 10，保留固定双目标、原始 Designer/resx、Windows 官方 Drawing 与 Native AOT；不扩展架构，复杂真机验收后置且与代码完成分开。用户已授权逐批实施、必要提交推送、新建本地接续会话和每 30 分钟监督，后续暂停/停止优先。每批实现和交接都必须 commit 并成功 push，记录实现 SHA、核对远端 refs/heads/main，仅本地提交不算完成。每批提交前按真实实现/必要检查整理本批与受影响路线图，历史完成/未完也回标；同步ROADMAP/执行总览、控件/公共任务页和历史、批次/HANDOFF，用现有工具同时重生成逐控件README的实施状态/完成ID列和tasks.csv并校验。明确子范围全部实现才✅，父项未完仍🟡/🚧且列剩余，未开始⬜，实际分配才🚧；🧪只限可复现的平台/范围证据。文档/签名/编译/局部探针/基类不能关闭整控件/平台，真机/设计器/跨平台继续待验收；历史错标纠正说明，不删除未完项/重置ID/缩小固定分母，不用数量推98%。按真实依赖与原优先级排下一批，不重做全项目评估/扩大本批测试/另建调度报告框架。先创建下一只读接续会话，再更新归属并提交推送，之后发送‘开始接管’；确认新会话实际接管后旧主会话停止所有写入/构建/Git并调用 set_thread_archived 归档（或由监督核对后归档），不等待下一批结束。所有新接续提示保留这些授权与规则；沿用 luminaforms 监督自动化。”

## 批次 014 新增接续证据

- Control-09c：明确初始化后的 Min/Max 运行期约束与默认虚扩展完成，33,408 条固定10.0.11/JIT/实际 AOT全同，394项新增回归。base getter仍读默认扩展，负值/零轴/冲突/取消按参考，core raw相等先返回，保留009～013边界/客户区/通知。构造期默认约束与DefaultSize统一留015的Control-01b，--constructors已有独立原始状态/调用/异常参考，不能当作已实现。
- ListViewItem-04a：Name首项代理、独立Tag/ToolTipText完成，151条固定英文参考/JIT全同，101项新增回归。读空表不恢复，写null/空也恢复首项并换表；共享view/三枚举/独立key缓存/毒丸/隐藏成员有证据。中文Designer分类Data/Appearance与参考“数据/外观”不同，完整所属/挂接Tooltip/native/Clone/序列化仍缺，新范围未计实际AOT运行验收。
- 集成：2,228项测试全通过，双目标Release 0错误/本次增量300警告，23个既有差分驱动通过。约束AOT实际33,408条相同，EXE2,641,920字节、无IL2xxx/IL3xxx；Demo AOT4,107,776字节，两者CLR Header0，Demo既有IL3050保留。首次AOT动态AmbientValueAttribute.Value抛原异常，最终仅核对构造元数据，该动态转换与中文分类/描述本地化缺口单列，未全局根化或吞异常。
- 13项目/17目标仅.NET10，原始Designer/resx/net10.0依赖满足要求；API分母1,095类型/61,240行、运行状态unverified不变。日志/TRX/PE/项目/依赖及已知差异摘要在artifacts/compatibility-batch014。
- 下一批015：Control-01b子智能体独占Control.cs及独立DefaultSize/构造探针测试（含构造期DefaultMin/Max消费）；ListViewItem-01c子智能体独占ListViewItem.cs及独立ToString探针测试。仍先固定参考、主聊天统一顺序运行后授权实现，主聊天独占共享文档/API/索引、必要interop、集成与Git。
