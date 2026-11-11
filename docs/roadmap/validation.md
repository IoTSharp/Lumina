# 验证状态与未关闭门槛

更新：2026-11-11。当前 M0 实现从 `313fa0fb685c2579919e2e3d7256ae2f3b3b7021` 继续；只运行必要编译、少量轻量回归及文档检查，Windows 真机/设计器/完整 AOT 交互集中后置。069 及此前的原始结果保留，不由本轮局部检查覆盖。

本页是当前验证摘要；[001～069 完整验证档案](batches/validation-through-069.md)保留原始首失败、纠正、诊断、历史 AOT/窗口证据及资源限制。逐批事实见[批次索引](batches/README.md)，任务实施状态见[任务索引](README.md)。历史 PASS 只覆盖当时提交和明确场景。

## 最近实际结果

M0 本轮的编译、回归与任务校验回执保存在 `artifacts/m0-20261111`；资源构建期转换的独立回执在 `artifacts/m0-resource-conversion/receipt.json`。Git 提交/推送及远端回读以最终交付回执为准。

- 接续实现集成 Release 编译 PASS：完整 `Lumina.slnx` 双目标，0 错误、1332 警告，22.58 秒；警告未通过全局抑制隐藏。旧 1040 警告结果仍保留为接续前基点。
- 接续新增无窗口回归 PASS：MaskedTextBox、ListView/ImageList/Group、ThreadExceptionDialog 共 12/12；既有 `ListViewItemImageTests` 363/363。真实窗口、Designer、设备和完整交互仍 NOT_RUN。
- 接续资源探针 PASS：NRBF 四字段 Font 转换生成 LFR1 primitive 记录；Designer 转换副本将标准组件容器替换为 AOT 安全的本地组件容器，保留原始 Designer/resx。回执在 `artifacts/m0-followup-20261111/font-conversion.json`、`designer-conversion.json`。
- 接续 Native AOT 发布 FAIL/环境门槛：`NativeFormsDemo net10.0 win-x64 PublishAot` 被宿主拒绝并报 `Cross-OS native compilation is not supported`；当前 AOT 交互为 NOT_RUN，回执在 `artifacts/m0-followup-20261111/aot-publish.json`，不把该失败归因于源码兼容性。
- 最终 Release/固定 Demo 双目标编译 PASS，0 错误、1040 警告，30.359秒；未隐藏告警，详细输出保留在 `build-final.log`。首个完整集成编译同为0错/1040警告，47.328秒；两次均由Cordonix确认进程树已清理。
- 必要轻量回归 PASS，18/18、0失败/跳过，测试执行93ms、命令2.531秒；`lightweight.trx`。仅覆盖9个新增Context/过滤器/集合归属回归及9个既有按钮路由/校验用例，不创建可见UI，不扩为消息泵/模态/真机验收。
- 资源转换 PASS：实际 Roslyn MSBuild 任务检查 Font 字段/单位/样式、Icon 内联与文件引用及普通 byte[]；没有创建 UI 或运行期 Drawing 对象。首轮负例夹具的 WarnAndContinue 判断错误保留在 `conversion-initial.log`，修正后通过；生产转换未为夹具改成接受非法输入。
- 完成状态更新后的最终文档导出PASS，检查462个Markdown、187控件/API页、2204任务及187实施行，0错误；固定1095类型/61240成员行不变。任务页保留完成标记，日期/批次历史统一在根目录CHANGELOG；CHANGELOG已纳入现有工具的链接检查。
- 初次 --no-restore 编译因缺少 project.assets.json 以 NETSDK1004 失败，补必要还原后通过。其后一轮编译因大量原有警告触发捕获输出上限而被清理，改用文件日志后取得完整退出结果；这些初始结果没有计为 PASS。

下表是 069 及此前的历史基点：

| 门槛 | 状态 | 证据与实际边界 |
| --- | --- | --- |
| 069 Release 方案/固定 Demo 双目标 | PASS（该次构建） | 0 错误、2 条既有 NU1510，27.766 秒；`artifacts/compatibility-batch069/release-build-final.json`。使用 WarningLevel=0，不代表分析告警清零 |
| 069 相关测试 | PASS（限定范围） | 1048/1048，84.531 秒；96 新增（19 契约/28 原生/49 结果）+ 旧 952；`rich-tests-final.json`/TRX |
| 069 独立 Protected 诊断 | PASS（限定范围） | 23/23；关闭/启用通知掩码的格式变化与拒绝对照，`protected-diagnostic.json`/TRX |
| 069 首相关 | FAIL（历史原始结果保留） | 1027/1046、19 失败：16 个构造/建窗守卫假设、2 个 raw 消息预期、1 个旧格式保持夹具遇新保护守卫；纠正及原断言保留详见[069](batches/069.md) |
| 064 全量 | **FAIL / 未解决** | 完整一次 6145/6146，既有同步上下文 UI callback 超时；另两次超时，末次另记录既有点击失败。独立 18/18、23/23 不清除全量失败，原因未定 |
| 065 全量 | **超时 / 未完成** | 一次 180 秒，无完整 TRX/总数；不记 PASS，见[065](batches/065.md) |
| 066～069 全量 | **NOT_RUN** | 相关测试结果不替代全量检查；本次M0也不重跑全量 |
| 069 文档导出/独立收尾 | PASS（文档与审计范围） | 整理前 457 MD、2204 任务（2154 控件+50 共享）、187 实施行、整类型完成 0、0 错误；不等于产品全量或 98% |

069 两个旧测试变动仅为精确事件掩码加入 PROTECTED，以及格式合并夹具临时关闭新增保护通知并 finally 恢复；后者完整原方法体、13 处 Assert 和 AssertFont 保留。原失败、独立 raw 证据与最终逐结果审计均保存，不从最终 PASS 抹掉先前记录。

## 继续开放的验收

| 范围 | 当前边界 | 追踪入口 |
| --- | --- | --- |
| 完整线程/关闭/焦点差分 | 本轮已修窗口/Context/循环退出归属及模态泵，但028/029历史WM_QUIT、关闭LostFocus与JIT/AOT严格差分未整体重验；外部宿主/极端重入仍待 | [Application](controls/Application.md)、[Form](controls/Form.md)、[历史验证](batches/validation-through-069.md) |
| 固定 API 库存刷新 | NOT_RUN；现有 10.0.11 静态库存是历史快照，不能据同名类型判定当前支持 | [基线](api/baseline.json)、[计量](coverage.md) |
| 包 smoke / Notepad publish / hosted CI | 最近批次本地 NOT_RUN；hosted CI 未独立读回（UNKNOWN） | [REP-001/002](project-consolidation.md)、[实际 CI](../../.github/workflows/ci.yml) |
| Designer / resx 往返 | 完整范围待验；元数据、资源 stub 或双目标编译不等于设计器往返 | [FND-026](foundations.md)、[需求](../../PROJECT_REQUIREMENTS.md) |
| Native AOT 实际运行 | 早期限定探针/Demo有历史运行证据；本轮仅接CI发布链，完整当前产品的发布与实际交互未在本机重跑，集中Windows真机验证后置 | [历史验证](batches/validation-through-069.md)、[完整 Control](controls/Control.md) |
| 正向可见点击、真实剪贴板/菜单、物理键盘/滚轮/IME、DPI | 056 两次 WindowFromPoint no-hit 仅证负向；程序消息与截获命令不等于设备验收 | [TextBoxBase](controls/TextBoxBase.md)、[行为目录](behavior-catalog.md) |
| Linux / macOS | 原生资源、控件、线程、AOT 与设备验收待完成；不能以 Windows 检查代替 | [平台闸门](platforms.md) |
| 每控件 ≥98% | 未达成可宣称的完整逐项证据；属性/事件/行为按各平台单列 | [验收口径](coverage.md) |

## 回执与资源边界

069 回执位于 `artifacts/compatibility-batch069`：实现/交接远端核验、独立收尾、`resource-closeout.json`、`execution-requests.json`及 JSON/TRX 保留。任务创建的临时 request 已精确核验后回收；既有 bin/obj、共享缓存与应用管理 MCP 保留。

069 首构建、首/最终相关和普通文档校验有存活进程身份；27.766 秒最终构建在独立快照前退出，832ms 诊断 host、ExportTasks validator 及部分短调用未捕获完整存活身份。执行回执记录 cleanup_complete=true/after0；这不补足缺失的身份快照，也不授权清理外部进程。

PASS、FAIL、UNKNOWN、NOT_RUN 各自保存。文档重排、审计通过、完成任务数或历史某一探针通过，均不自动改变上述门槛。
