# LuminaForms 当前交接

更新：2026-11-11（Asia/Shanghai）。先读[项目需求](PROJECT_REQUIREMENTS.md)、[工作规则](AGENTS.md)和[任务索引](docs/roadmap/README.md)。

## 当前授权与归属

用户已重新授权：将路线图变更历史放到根目录 CHANGELOG，完成范围直接标记；多内部智能体并行实现 M0 公共基础、Application 和项目收敛；尽量少写测试，只做必要检查。Windows 真机验证在代码完成后集中执行。用户随后明确要求“在新会话里开始多个智能体并行实现剩余代码”，本次因此创建一个接续主聊天；不恢复自动无限接续或监督自动化，后续暂停/停止优先。

接续主聊天：`01a1ca08-6ac2-7872-a03f-82ec0713c8e9`（Lumina M0 剩余代码并行实现），仍使用 `D:\GitHub\Lumina` 的本地 main，不创建 worktree 或源码副本。已按只读准备创建；本次交接提交并成功推送、独立核三 SHA 后，旧主发送“开始接管”，由新主持有唯一编码/集成/Git归属，旧主停止工作区写入并归档。前轮三个内部子智能体均已完成分配范围，不继续写入。

新主按原任务的真实剩余依赖启动三个内部子智能体，先明确互不重叠的文件归属：MaskedTextBox真实模型/标准层级、列表集合与ImageList资源/原生同步、Application宿主/模态及异常/OLE缺口。主负责共用接口、资源/AOT告警、集成、文档与Git；不得重做已完成范围或扩大到M1。每个子智能体均须继承有界执行、工具发现、PID/父链归属和自有资源回收规则。

| 基点 | 当前状态 |
| --- | --- |
| 实施起点 | `313fa0fb685c2579919e2e3d7256ae2f3b3b7021`，main，开始时工作区干净 |
| 最新实现（已推送） | `e3ebfd87f766a69295156fab02f89894bce8e2f9`，接续前HEAD / origin/main /实际远端main一致，工作区干净 |
| 本次交接 | 仅更新AGENTS/HANDOFF；交接提交SHA以Git历史及独立远端回读为准，不在同一提交中预填自身SHA |
| 工作区 | `D:\GitHub\Lumina` |
| 交付 | 按既有约定提交、推送并核对 HEAD / origin/main / 实际远端；最终 SHA 和回读见 Git 历史及 `artifacts/m0-20261111`，不在同一提交中预填自身 SHA |

## 已交付实现

- 标准 ApplicationContext、Run 三重载、FormCollection/OpenForms、线程循环归属、取消退出预检、消息过滤及线程/应用退出通知。
- ShowDialog 两重载、Modal/FormClosed、嵌套消息泵、取消关闭、owner/窗口启用恢复、对话框复用与 WM_QUIT 向外传递；标准异常模式接线及线程资源清理。
- 标准 ButtonBase 及三个按钮类继承、既有原生 BUTTON/主题/默认标志迁移。
- ListView 项/列所属及真实 LVM 同步；Tree 节点/子树所属、循环拒绝、TVM 插入更新删除和 HTREEITEM 映射清理/失败回滚。
- Icon/Font 资源静态转换与解码，保留原始 Designer/resx；完整 Demo 的 CI win-x64 AOT 发布/产物上传，Core DWM Windows 入口与依赖清单。
- 所有控件任务页的日期/批次/提交历史归入根目录 CHANGELOG；任务页保留 ID 与完成状态，既有工具生成实施列与 tasks.csv。

## 本次接续已完成的代码范围

- `MaskedTextBox` 已直接继承 `TextBoxBase`，使用私有空掩码 provider 和真实 `MaskedTextProvider` 完成确定范围的构造、格式、选区、替换/插入/删除、拒绝、剪贴板和 AOT 安全已知类型验证。
- `ListView` 已补嵌套集合兼容入口、ImageList SafeHandle/键映射/重建与消费释放、ListViewGroup/GroupCollection 所属和 LVGROUP 原生同步；默认 View 为 LargeIcon。
- `Application` 已补外部宿主顶层窗口快照、模态期间动态窗口 scope、嵌套 scope 转移、宿主消息泵寿命边界和 ThreadExceptionDialog 默认路径。
- 资源构建链已补 NRBF 四字段 Font → LFR1 primitive 记录，并在 Lumina Designer 副本中使用 AOT 安全组件容器；原始 Designer/resx 不改。

上述范围均为父任务内的部分代码完成，不能关闭完整控件、Application、资源/AOT 或平台验收任务。完整 OLE/COM 宿主、ImageStream/Designer streamer、通用 ApplyResources、VirtualMode/OwnerDraw、真实窗口/Designer/完整 AOT 交互和 Linux/macOS 仍开放。

## 当前检查与剩余门槛

- PASS：最终 Release 方案及固定 Demo 双目标编译，0错误/1040警告，30.359秒；未隐藏警告，日志为 `build-final.log`。
- PASS：必要轻量回归18/18（9个新增、9个既有），无可见UI/设备测试；`lightweight.trx`。
- PASS：无窗口的实际资源构建期转换检查；`artifacts/m0-resource-conversion/receipt.json`。
- PASS：最终文档导出及普通校验均0错误，462个Markdown、187控件/API页、2204任务及固定API基线保持；Git空白/链接检查通过，结果统一见[validation](docs/roadmap/validation.md)。
- NOT_RUN：全量、Windows真机/设计器/完整AOT交互、Linux/macOS、本次Hosted CI运行。064全量FAIL、065超时与历史严格差分仍未整体重验或清除。
- 本次接续：PASS，完整 Release 方案编译 0 错误/1332 警告；PASS，新增 M0 无窗口回归 12/12 及既有 ListViewItemImage 363/363；PASS，NRBF Font/Designer 容器转换探针。FAIL/环境门槛，当前 NativeFormsDemo win-x64 AOT 发布报 `Cross-OS native compilation is not supported`；真实 AOT/窗口运行仍 NOT_RUN。回执位于 `artifacts/m0-followup-20261111`。

M0 尚不能整体标为完成。剩余真实代码包括 MaskedTextBox 模型/标准直接层级、完整列表集合API/ImageList/分组/样式、外部宿主模态/新增窗口策略、完整错误对话框/OLE宿主，以及资源二进制Font/ApplyResources和旧Container IL3050。各范围继续使用原任务ID，完成基础类不关闭整个控件或父项。真机验收依用户要求集中后置，不由本轮编译或18项回归推导98%。

## 工作与资源

PowerShell仅7；外部命令用有界typed argv及Cordonix Job清理。初次缺还原资产、输出上限中止和资源首轮负例夹具错误均留回执；只将完整退出的实际结果计PASS。

未修改原始Designer/resx、固定API库存或历史批次档案。保留现有bin/obj、缓存及历史证据。资源探针临时脚本/fixture已回收，仅保留三份忽略证据；子智能体报告无持久进程或未提交内存文档。主调用回执均记录清理状态；不按进程名清理共享MCP/用户进程。

新主收到“开始接管”后，先核对本次交接HEAD/tracking/实际远端及工作区，确认接管并实际启动多个内部子智能体。旧主确认接管已发生后停止写入、归档；后续M0代码按现有授权继续，少测、真机后置、按实际完成状态更新并提交推送。除非用户再次明确要求，不再创建下一接续聊天，不恢复旧的无限批次或监督机制。
