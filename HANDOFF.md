# LuminaForms 实施交接

更新：2026-10-16。先读本文件、[项目需求](PROJECT_REQUIREMENTS.md)，再读 [执行顺序](docs/roadmap/execution.md) 和当前批次；不要重新评估整个项目。

## 当前接续点

- 调度状态：批次 009 两个明确子范围代码完成，集成检查通过；当前主聊天更新台账、提交推送并准备批次 010 只读接续。批次 008 实现与归属交接均已成功推送。
- 监督会话：`01a13bd2-e505-79c0-ab49-1d428c85f592`，只负责提交本轮成果、建立巡检与接续。
- 当前编码主会话：`01a140bf-ab23-7d33-a438-241ef1cdf44d`（批次 009，本地 Lumina 项目；已实际接管）。批次 008 主会话 `01a140ae-7c4d-7543-ba97-971ce31c8aec` 已停止全部写入/构建/Git；发送实际接管确认时应用返回该聊天已归档，不等待批次 009 完成。批次 007 主会话 `01a14097-615e-7523-8a15-e70e6a2da8e6` 已通过应用归档列表确认归档，更早主会话均已交接。
- 巡检：每 30 分钟，已启用；自动化 ID `luminaforms`，绑定监督会话并按本文件跟随最新编码主会话。
- 当前批次：[009 · 零尺寸边界与子项批量添加首范围](docs/roadmap/batches/009.md)明确范围代码完成；下一批已划定 [010 · 指定边界与子项插入移除首范围](docs/roadmap/batches/010.md)，交接后领取；[008](docs/roadmap/batches/008.md)至[001](docs/roadmap/batches/001.md)明确范围代码完成。
- 最近实现提交：`d7b1db1c0193460b46a7d5abfc385ee73b619d3a`（批次 008），已成功推送并核对。归属交接提交 `8b31de112a8b4c538aad87cfd2a8dc50ad2bd59e` 已成功推送，批次 009 接管时 HEAD/origin/main/远端 refs/heads/main 核对一致，工作区干净。批次 007 实现 `db29dce02f1af525513e2b8817eb16ae0319f732` 与交接 `017d04d7eeeb9174af2fb73a199948114fe49555` 同样已验证；原代码基点 `b83f534` 不是回退要求。
- 全量进度：[tasks.csv](docs/roadmap/tasks.csv)，由控件页及 FND/REP/CNS/XPL 任务表生成；逐 API 分母仍在 [api](docs/roadmap/api/baseline.json)。
- 当前阻塞：无阻塞下一批编码的事项。资源严格告警门禁仍有已定位 IL3050（标准 Container/InheritanceAttribute），FND-006 保持部分完成；Windows/Linux/macOS 真机验证后置。

## 用户已经确定的范围

用户于 2026-10-15 要求：提交推送后，在新会话用多个子智能体逐任务实现；每个子智能体负责一个任务，无文件冲突才并行；每批完成后新建会话接续；每半小时巡检；专注代码，不扩散、不做额外架构。用户授权该实施循环及每批必要的提交、推送、创建接续会话和监督消息；后续用户的暂停/停止指令优先。2026-10-16 补充：实现和交接都必须 commit 并成功 push，记录实现 SHA 和远端分支核对结果；仅本地提交不算批次完成。新会话实际接管后旧主会话立即停止写入并归档，不等待下一批结束。

固定 .NET 10.0.11 WinForms 基线。用户于本批进一步明确：所有项目（包括分析器和构建工具）只支持 .NET 10，使用 `net10.0` / `net10.0-windows`，不支持 `netstandard2.0` 或旧 .NET 目标。保留 `Lumina.Forms` 包/命名空间与 NativeFormsDemo 双目标：`net10.0-windows` 真实 WinForms/VS 设计器，`net10.0` Lumina.Forms/Native AOT。不引入替代 UI 栈。

2026-10-16 的 PROJECT_REQUIREMENTS.md 修订已读取（提交 f424523）：Windows 两目标均继续使用官方 System.Drawing.Common；Linux 的自有 Drawing/身份验证按 XPL 任务推进，不阻塞当前 Windows 兼容批次，也不把 net10.0 等同 Linux。

前期 **✅ 代码完成即关闭开发部分**：实现任务明确的范围，并通过必要编译/轻量检查即可。复杂 UI 自动化、真机、压力、设计器往返集中后置；后期先 Windows，再统一 Linux；macOS 尚无真机证据时如实留待验收。不得用空实现、常量成功或吞异常冒充完成。**🧪 验收通过**是另一状态，只有逐成员/场景证据才计入 98%。

## 每批怎样继续

1. 先查当前编码主会话、`git status`、批次任务和文件责任范围。同一工作区同一时刻只有一个编码主会话。
2. 主会话选 2～3 个有边界的任务，用内部子智能体分配，一人一任务；涉及 Control/Form/共享 interop 等同一文件的工作串行。主智能体保留共享文档、集成构建和 Git 操作。
3. 子智能体不提交、不推送、不创建接续会话，不覆盖其他人的修改；需要越过文件责任范围时先向主智能体报依赖。由主智能体统一构建，避免争用 obj/bin。
4. 主智能体集成后更新各控件修改历史、任务状态与本批记录，执行适当构建/轻量检查及路线图校验，commit 并成功 push。记录实现 SHA，用远端 refs/heads/main 核对推送结果；仅本地提交不算批次完成。未实现的父任务保持部分完成；延期的真机验收单列。遇到局部阻塞先做其他可实施任务。
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
- WPF 适配项目及发布入口移除；独立美化/注入/效果展示隔离到 `Lumina.Effects.slnx`。Core、Ext.WinForms 与双目标 Demo 保留。
- 上轮主线 Release 构建 0 错误；效果线 0 错误；77 项现有测试通过；4 个主线 NuGet 包本地打包成功。现有警告和真机缺口见 [验证记录](docs/roadmap/validation.md)。
- 重新生成任务索引：`./tools/compatibility/Validate-Roadmap.ps1 -ExportTasks`。无需每批重新导出全部 61,240 行 API；公共签名有变化后按需更新。

## 接续提示

“继续 D:\source\Lumina 的 LuminaForms 路线图实施。先读 AGENTS.md、PROJECT_REQUIREMENTS.md、HANDOFF.md 和当前批次，收到‘开始接管’并确认归属与 Git 状态后立即分配两个互不冲突的内部子任务。所有项目（含分析器/工具）只支持 .NET 10，保留固定双目标、原始 Designer/resx、Windows 官方 Drawing 与 Native AOT；不扩展架构，复杂真机验收后置且与代码完成分开。用户已授权逐批实施、必要提交推送、新建本地接续会话和每 30 分钟监督，后续暂停/停止优先。每批实现和交接都必须 commit 并成功 push，记录实现 SHA、核对远端 refs/heads/main，仅本地提交不算完成。先创建下一只读接续会话，再更新归属并提交推送，之后发送‘开始接管’；确认新会话实际接管后旧主会话停止所有写入/构建/Git并调用 set_thread_archived 归档（或由监督核对后归档），不等待下一批结束。所有新接续提示保留这些授权与规则；沿用 luminaforms 监督自动化。”
