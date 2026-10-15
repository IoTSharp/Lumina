# LuminaForms 实施交接

更新：2026-10-15。先读本文件，再读 [执行顺序](docs/roadmap/execution.md) 和当前批次；不要重新评估整个项目。

## 当前接续点

- 调度状态：批次 002 明确的组件契约/Bitmap 子范围代码完成，正在更新台账、提交推送及准备批次 003 的只读接续；当前主会话仍独占集成/交接写入。
- 监督会话：`01a13bd2-e505-79c0-ab49-1d428c85f592`，只负责提交本轮成果、建立巡检与接续。
- 当前编码主会话：`01a14025-ea87-7223-b17b-33b40a0276e2`（批次 002，本地 Lumina 项目，已接管）。旧主会话 `01a1400e-db15-7fb1-8747-5f344fec476e` 已交接并停止写入。
- 巡检：每 30 分钟，已启用；自动化 ID `luminaforms`，绑定监督会话并按本文件跟随最新编码主会话。
- 当前批次：[002 · 标准基础与裁剪资源](docs/roadmap/batches/002.md)明确范围代码完成；下一批已划定 [003 · UI 投递与基础项构造](docs/roadmap/batches/003.md)，交接后领取。[001 · 基础修复](docs/roadmap/batches/001.md)已完成。
- 最近实现提交：`f2d694c`，已推送 `origin/main`。接续交接提交以 `git log` 为准，原代码基点 `b83f534` 不是回退要求。
- 全量进度：[tasks.csv](docs/roadmap/tasks.csv)，由控件页及 FND/REP/CNS/XPL 任务表生成；逐 API 分母仍在 [api](docs/roadmap/api/baseline.json)。
- 当前阻塞：无阻塞下一批编码的事项。资源严格告警门禁仍有已定位 IL3050（标准 Container/InheritanceAttribute），FND-006 保持部分完成；Windows/Linux/macOS 真机验证后置。

## 用户已经确定的范围

用户于 2026-10-15 要求：提交推送后，在新会话用多个子智能体逐任务实现；每个子智能体负责一个任务，无文件冲突才并行；每批完成后新建会话接续；每半小时巡检；专注代码，不扩散、不做额外架构。用户授权该实施循环及每批必要的提交、推送、创建接续会话和监督消息；后续用户的暂停/停止指令优先。

固定 .NET 10.0.11 WinForms 基线。用户于本批进一步明确：所有项目（包括分析器和构建工具）只支持 .NET 10，使用 `net10.0` / `net10.0-windows`，不支持 `netstandard2.0` 或旧 .NET 目标。保留 `Lumina.Forms` 包/命名空间与 NativeFormsDemo 双目标：`net10.0-windows` 真实 WinForms/VS 设计器，`net10.0` Lumina.Forms/Native AOT。不引入替代 UI 栈。

前期 **✅ 代码完成即关闭开发部分**：实现任务明确的范围，并通过必要编译/轻量检查即可。复杂 UI 自动化、真机、压力、设计器往返集中后置；后期先 Windows，再统一 Linux；macOS 尚无真机证据时如实留待验收。不得用空实现、常量成功或吞异常冒充完成。**🧪 验收通过**是另一状态，只有逐成员/场景证据才计入 98%。

## 每批怎样继续

1. 先查当前编码主会话、`git status`、批次任务和文件责任范围。同一工作区同一时刻只有一个编码主会话。
2. 主会话选 2～3 个有边界的任务，用内部子智能体分配，一人一任务；涉及 Control/Form/共享 interop 等同一文件的工作串行。主智能体保留共享文档、集成构建和 Git 操作。
3. 子智能体不提交、不推送、不创建接续会话，不覆盖其他人的修改；需要越过文件责任范围时先向主智能体报依赖。由主智能体统一构建，避免争用 obj/bin。
4. 主智能体集成后更新各控件修改历史、任务状态与本批记录，执行适当构建/轻量检查及路线图校验，提交推送。未实现的父任务保持部分完成；延期的真机验收单列。遇到局部阻塞先做其他可实施任务。
5. 每批结束后创建一个新的项目本地会话。先让新会话只读等待，旧会话把本文件的编码主会话 ID、下一批、文件责任和检查结果更新并提交，再发送“开始接管”。旧会话随后停止写代码；新会话继续同一流程。不要让多个主会话同时领取任务。

巡检先读本文件，再查编码主会话状态和最新批次记录。会话正常运行时不另起竞争主会话；空闲但未完成时，按已有授权给该会话发送继续消息。批次完成但尚未接续时才创建下一会话。出现错误先读取必要上下文恢复，不在活跃写入时 reset/clean/换分支。全部开发任务完成后转入集中的 Windows 验收；最终无剩余任务或用户停止时停用巡检。

应用/机器需运行且有可用额度，定时巡检才可执行；该机制不承诺离线永久运行。

## 已有成果与检查

- 批次 001：FND-005、REP-002 及 ListViewItem 构造子范围完成；父任务 FND-003/ListViewItem-01/Application-01 保留部分完成。分析器也统一 net10.0；12 个项目均只声明 .NET 10 目标。
- 批次 001 检查：双目标 Release 构建 0 错误；92 项测试全通过；14 组 DPI 隔离对照与本机 WinForms 10.0.11 一致；两种真实 NuGet 消费各触发 LNF003 且无工具运行依赖；完整 Demo Native AOT 发布通过、EXE 无 CLR Header。复杂 UI/真机/设计器/跨平台验收后置，不能从上述结果推算 98%。
- 批次 002：可实例化 Control : Component/IWin32Window，Component Site/Container/Disposed/Disposing 与派生清理衔接；101 项纯托管测试、1 项基础 HWND 测试、5 项真实端生命周期观测通过。父 FND-001/Control-01/Control-08/FND-012 仍保留未完范围。
- 批次 002 资源：五张真实 Demo Bitmap 的 WinForms/Lumina JIT/Native AOT 像素、文化、身份、重载/GC/错误语义相同，IL2026 消除；资源脚本仍因 IL3050 严格失败。原 IL3053 已展开为 Container.ValidateName 根化 InheritanceAttribute.ToString/Enum.GetValues(Type)，未改标准容器、未全局抑制。原始 Designer/resx 不变，资源转接在库内 Lumina.Forms.Resources，obj 生成副本仅供 net10.0。
- 批次 002 最终主线双目标构建 0 错误；完整 Demo AOT 发布通过（4,085,248 字节），资源探针 AOT 已运行，两者无 CLR Header。13 项目/17 个目标仅 .NET 10；net10.0 Demo deps 无 WinForms/Resources.Extensions；完整主窗、设计器、Icon/Font/其他转换仍未验收。
- 批次 003 文件责任：FND-002 子智能体负责 Control.cs/Form.cs 与必要独立投递辅助文件/线程检查；ListViewItem-01b 子智能体负责 ListViewItem.cs 与该类型独立构造检查。共享 Win32、全部文档/集成/Git 保留主会话协调；子智能体不构建、不提交。
- 187 组独立类型任务页、固定基线/API/接口/类型责任清单；遗漏审计与三平台逐控件方案已落盘。
- WPF 适配项目及发布入口移除；独立美化/注入/效果展示隔离到 `Lumina.Effects.slnx`。Core、Ext.WinForms 与双目标 Demo 保留。
- 上轮主线 Release 构建 0 错误；效果线 0 错误；77 项现有测试通过；4 个主线 NuGet 包本地打包成功。现有警告和真机缺口见 [验证记录](docs/roadmap/validation.md)。
- 重新生成任务索引：`./tools/compatibility/Validate-Roadmap.ps1 -ExportTasks`。无需每批重新导出全部 61,240 行 API；公共签名有变化后按需更新。

## 接续提示

“继续 D:\source\Lumina 的 LuminaForms 路线图实施。先读 AGENTS.md、HANDOFF.md 和当前批次，确认自己是编码主会话后立即分配互不冲突的内部子任务。所有项目（含分析器/工具）只支持 .NET 10，不引入 netstandard 或旧目标。严格逐任务编码、保留双目标和 AOT、不重新扩展架构；真机验收后置；本批完成后更新台账、提交推送并创建下一会话接续。”
