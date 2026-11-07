# 验证状态与未关闭门槛

更新：2026-11-07。代码基点为批次 069 实现 `9675879bf6f65bb386429b554f6705ca978f61ce`，文档交接为 `6c05aa0634bc543cebda55418c6ee6e6daf2d00d`。本次只整理文档，没有重新执行以下产品检查。

本页是当前验证摘要；[001～069 完整验证档案](batches/validation-through-069.md)保留原始首失败、纠正、诊断、历史 AOT/窗口证据及资源限制。逐批事实见[批次索引](batches/README.md)，任务实施状态见[任务索引](README.md)。历史 PASS 只覆盖当时提交和明确场景。

## 最近实际结果

| 门槛 | 状态 | 证据与实际边界 |
| --- | --- | --- |
| 069 Release 方案/固定 Demo 双目标 | PASS（该次构建） | 0 错误、2 条既有 NU1510，27.766 秒；`artifacts/compatibility-batch069/release-build-final.json`。使用 WarningLevel=0，不代表分析告警清零 |
| 069 相关测试 | PASS（限定范围） | 1048/1048，84.531 秒；96 新增（19 契约/28 原生/49 结果）+ 旧 952；`rich-tests-final.json`/TRX |
| 069 独立 Protected 诊断 | PASS（限定范围） | 23/23；关闭/启用通知掩码的格式变化与拒绝对照，`protected-diagnostic.json`/TRX |
| 069 首相关 | FAIL（历史原始结果保留） | 1027/1046、19 失败：16 个构造/建窗守卫假设、2 个 raw 消息预期、1 个旧格式保持夹具遇新保护守卫；纠正及原断言保留详见[069](batches/069.md) |
| 064 全量 | **FAIL / 未解决** | 完整一次 6145/6146，既有同步上下文 UI callback 超时；另两次超时，末次另记录既有点击失败。独立 18/18、23/23 不清除全量失败，原因未定 |
| 065 全量 | **超时 / 未完成** | 一次 180 秒，无完整 TRX/总数；不记 PASS，见[065](batches/065.md) |
| 066～069 全量 | **NOT_RUN** | 相关测试结果不替代全量检查；本次文档整理也不重试 |
| 069 文档导出/独立收尾 | PASS（文档与审计范围） | 整理前 457 MD、2204 任务（2154 控件+50 共享）、187 实施行、整类型完成 0、0 错误；不等于产品全量或 98% |

069 两个旧测试变动仅为精确事件掩码加入 PROTECTED，以及格式合并夹具临时关闭新增保护通知并 finally 恢复；后者完整原方法体、13 处 Assert 和 AssertFont 保留。原失败、独立 raw 证据与最终逐结果审计均保存，不从最终 PASS 抹掉先前记录。

## 继续开放的验收

| 范围 | 当前边界 | 追踪入口 |
| --- | --- | --- |
| 完整线程/关闭/焦点差分 | 028/029 的额外 WM_QUIT、关闭 LostFocus 与 JIT/AOT 严格差异未整体闭环；其他明确增强/差异保留 | [Application](controls/Application.md)、[Form](controls/Form.md)、[历史验证](batches/validation-through-069.md) |
| 固定 API 库存刷新 | NOT_RUN；现有 10.0.11 静态库存是历史快照，不能据同名类型判定当前支持 | [基线](api/baseline.json)、[计量](coverage.md) |
| 包 smoke / Notepad publish / hosted CI | 最近批次本地 NOT_RUN；hosted CI 未独立读回（UNKNOWN） | [REP-001/002](project-consolidation.md)、[实际 CI](../../.github/workflows/ci.yml) |
| Designer / resx 往返 | 完整范围待验；元数据、资源 stub 或双目标编译不等于设计器往返 | [FND-026](foundations.md)、[需求](../../PROJECT_REQUIREMENTS.md) |
| Native AOT 实际运行 | 早期限定探针/Demo 有历史运行证据；069 范围及完整当前产品没有新的运行验收 | [历史验证](batches/validation-through-069.md)、[完整 Control](controls/Control.md) |
| 正向可见点击、真实剪贴板/菜单、物理键盘/滚轮/IME、DPI | 056 两次 WindowFromPoint no-hit 仅证负向；程序消息与截获命令不等于设备验收 | [TextBoxBase](controls/TextBoxBase.md)、[行为目录](behavior-catalog.md) |
| Linux / macOS | 原生资源、控件、线程、AOT 与设备验收待完成；不能以 Windows 检查代替 | [平台闸门](platforms.md) |
| 每控件 ≥98% | 未达成可宣称的完整逐项证据；属性/事件/行为按各平台单列 | [验收口径](coverage.md) |

## 回执与资源边界

069 回执位于 `artifacts/compatibility-batch069`：实现/交接远端核验、独立收尾、`resource-closeout.json`、`execution-requests.json`及 JSON/TRX 保留。任务创建的临时 request 已精确核验后回收；既有 bin/obj、共享缓存与应用管理 MCP 保留。

069 首构建、首/最终相关和普通文档校验有存活进程身份；27.766 秒最终构建在独立快照前退出，832ms 诊断 host、ExportTasks validator 及部分短调用未捕获完整存活身份。执行回执记录 cleanup_complete=true/after0；这不补足缺失的身份快照，也不授权清理外部进程。

PASS、FAIL、UNKNOWN、NOT_RUN 各自保存。文档重排、审计通过、完成任务数或历史某一探针通过，均不自动改变上述门槛。
