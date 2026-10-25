# LuminaForms 实施交接

更新：2026-10-26（Asia/Shanghai）。先读本文件、[项目需求](PROJECT_REQUIREMENTS.md)，再读 [执行顺序](docs/roadmap/execution.md) 和当前批次；不要重新评估整个项目。

## 当前接续点（2026-10-26 新授权）

- 用户本轮明确授权在 Codex 桌面应用的 Lumina 项目继续工作，并要求使用多个智能体监督与实现。主会话独占共享文档、集成验证和 Git；路线图审阅、构建基线和 Control 键盘切片由互斥子智能体完成，均已停止写入后由主会话复核。
- 工作区是 `D:\GitHub\Lumina`，分支 `main`。本轮未提交改动属于批次031：Control/ScrollableControl/ContainerControl/UserControl基础与继承链代码、六组新增回归测试及本批路线图、验证、交接和变更记录；忽略的 `bin/`、`obj/` 仅为有界构建/测试产物。
- 当前实施批次：[031](docs/roadmap/batches/031.md)。本批完成Control-11c/FND-013 RightToLeft托管继承与Win32 RTL样式、ScrollableControl-02a～06a托管滚动与受保护契约、ContainerControl-05a/06a AutoScaleFactor/ParentForm、UserControl-01a/02a标准ContainerControl继承；4,419项全线测试、Forms双目标Release构建和路线图校验通过。完整Cursor、真实滚动条消息、BindingContext/键盘路由、Load/Designer、Native AOT、设备和跨平台仍为UNKNOWN/NOT_RUN；批次030的FND-019/Control-12e基础键消息首范围保留。
- 忽略的 `bin/`、`obj/` 是本批有界还原/构建生成的验证产物，保留供继续复核，不纳入 Git。实现提交 `8211ec6161cc32f740a8ccd4c7d37951ad4fdbeb` 与交接提交 `33b1c2c6bc094c285fc926743389f2f7e53ff30b` 均已推送；最终 HEAD、tracking 分支和实际远端 ref 三者一致，工作区干净。
- 仓库已克隆并在 Codex 面板打开 `README.zh-CN.md`、`ROADMAP.md`、`HANDOFF.md`；当前 Codex App 工具可列出已有项目但没有创建新本地项目登记接口，侧栏的 `Lumina` 项目注册仍需用户在桌面界面添加 `D:\GitHub\Lumina` 路径，不能把 projectless 会话误记为已注册项目。

## 批次031 新增接续记录

- 并行子任务已停止写入：`control_inheritance`完成Control-11c/FND-013托管与Win32 RTL样式；`scrollable_foundation`完成ScrollableControl-02a～05a托管滚动；`container_chain`完成ContainerControl-05a/06a；第二轮完成ScrollableControl-06a受保护契约、`usercontrol_chain`完成UserControl-01a/02a、`rtl_native_bridge`完成RightToLeft原生桥接。主会话复核并修正 `AutoScroll` virtual/`AutoScrollOffset` 标准契约和滚动内容范围逻辑。
- 验证：Release方案构建0错误；`Lumina.Tests`全量4,419/4,419通过；本批六组新增测试22/22通过；`Validate-Roadmap.ps1 -ExportTasks`返回0结构错误；`git -c core.whitespace=cr-at-eol diff --check`通过。既有警告保留。
- 未运行：Native AOT发布/运行、真实Designer往返、物理设备、完整滚动条消息、Cursor/完整ambient、Linux/macOS、生产/现场。所有父任务和跨平台门禁保持部分或待办，不宣称98%。
- 待提交状态：实现与共享文档尚未提交/推送；主会话负责只暂存本批拥有路径，完成 commit/push 后回读 HEAD、tracking 分支与实际远端 ref，并更新本段 SHA。
## 历史029停止记录

- 调度状态：用户于2026-10-17要求“当前这批任务完成后，停止任务”。029实现、必要验证、文档提交推送并核验远端后停止；不创建030/下一聊天、不继续完整Control专项、不恢复luminaforms巡检。此指示覆盖此前持续接续授权；完整Control/Windows全面验收/Linux/macOS仍未完成，保留任务等待新指示。
- 当前编码归属：029主聊天`01a14565-97a2-76f0-8645-317df65705a7`（local）已核实交接`9ff6f26000533469d2b07340bec5ce345b764697`、三SHA一致及工作区干净后实际接管；029实现已成功推送并核验三SHA一致，主及三个内部子任务停止实施；没有新编码主，不发送开始接管。028旧主及更早归档记录作为历史保留。
- 028历史接管：`01a14527-ef1d-7440-8ca2-fc11b53f515f`于2026-10-16核对独立交接`ab5a23866774d0adc9bae17b20fd5e5b9ab63939`的三SHA/干净状态后接管，027旧主已核实归档。028实现`496e632d13e7e4f26dcb649093e69d589c9128d0`已成功推送且核对三SHA/干净状态，三个内部任务全部冻结。
- 027历史接管：027主聊天`01a144ef-726b-78a3-8b32-d9bd4e73eb2d`（host `local`）于2026-10-16收到单独“开始接管”后，已核实工作区干净、HEAD/origin/main/实际远端refs/heads/main均为交接SHA `32f65b9e411d9838ebcc5c1f0a1af7f61add972b`并实际接管。已只读核实026旧主`01a144c0-6427-79a2-a2cd-55d0dd562990`归档。
- 026历史接管：01a144c0-6427-79a2-a2cd-55d0dd562990于2026-10-16收到单独开始消息后核实干净状态与三SHA同为f9a3a3e9d737309d45e76a0b263c7126c2b3c64c并实际接管，已核实025旧主归档。026实现与路线图收尾c0f4c3d8bc12e2684cb9ddb6bca765373f8c4c60已成功推送，三个内部任务全部冻结，无后续写入/构建/Git。
- 024历史接管：01a14443-9c60-7a71-910f-29f6e9368dcd于2026-10-16核实干净状态、HEAD/origin/main/实际远端同为db602797a3b575edca04ca64481d6fa65fbd4973后实际接管，已核实023旧主归档。024实现5784c22610f768e05dc44ccf496bb275df11634e已成功推送并核对三SHA/干净状态；原三个内部任务已完成冻结，不再写入/构建/Git。
- 023历史接管归属：01a143ef-9e5b-76f2-9cfc-bb811bf4a0e2于2026-10-16接管时已核实干净状态，HEAD/origin/main/实际远端均为交接17ede2ed1e5dbad89981411eb1adb9cb370de0d9；已核实022旧主归档。023实现`7225e947495a7007f94377b5a0972ac91288c3cd`已成功推送，独立归属交接SHA由开始消息及artifacts/compatibility-batch023/handoff-push.json记录。
- 022历史接管归属：01a143be-8230-74b0-aa82-837bcf0b8563于2026-10-16收到开始消息后核实干净状态和HEAD/origin/main/实际远端同为d6644d771521127ee3391133afbcfa4ec0322efc并实际接管，已只读确认021旧主归档。022实现98cde66ca344ccbf8755faaca314e6936788df73已推送，独立交接SHA由本次开始消息及artifacts/compatibility-batch022/handoff-push.json记录。
- 021历史接管归属：`01a142ba-a2e6-75e1-a249-96cbb7a07e75`（Control默认布局与首选尺寸批次021，本地Lumina项目）。2026-10-16收到独立“开始接管”后已实际接管；接管时工作区干净，HEAD、origin/main与实际远端refs/heads/main均为已推送交接SHA `c9f4f2880429e648f81e7fbbf487a611547c82d3`。旧主`01a14286-0053-71b1-9348-6dda086ffc1e`及020三个内部子任务已停止写入/构建/Git；旧主只读确认实际接管后立即归档，不等待021完成。020接管SHA为`10588cf8086e4083c5b19cbe19550d07d7b382a2`；019旧主`01a14257-6797-7f83-963a-5c5022103f53`已确认停止且已核实归档，更早主归档记录保留。
- 历史监督会话：`01a13bd2-e505-79c0-ab49-1d428c85f592`。自动化`luminaforms`保持PAUSED；本次新授权不恢复30分钟巡检，后续用户暂停/停止优先。
- 当前范围：029有限自动焦点转移/程序验证、必要消费者及全部文档收尾；不是整Control完成。实现、测试与AOT结果见[029](docs/roadmap/batches/029.md)，按用户指示本批后停止。
- 当前实施批次：[029](docs/roadmap/batches/029.md)限定范围代码完成，4,392测试通过；按用户指示本批后停止，无下一批归属交接。
- 028历史完成批次：028实现与路线图收尾`496e632d13e7e4f26dcb649093e69d589c9128d0`已成功push，HEAD/origin/main/实际远端refs/heads/main一致且干净，见artifacts/compatibility-batch028/implementation-push.json。本独立交接另行commit/push并记录handoff-push.json，029按开始消息的交接SHA核验后接管，本地commit不算完成。027实现c87c52849e19098c02dc00898c9960424abf21da及独立交接ab5a23866774d0adc9bae17b20fd5e5b9ab63939保留。
- 018证据：3,996测试全部通过、双目标Release 0错误/400警告；固定参考/JIT391行中390相同，隐藏父级原生重挂的中间VisibleChanged缺失保持strictMatches=false。AOT实际运行全部391行，32条非零下界数组fixture因运行时不支持而明确CopyTo未运行，其余359同JIT；严格driver仍失败，原始记录不得归一化或删除。完整Demo AOT已真实显示、复选框0→1、正常exit0且记录HWND全部失效；截图仍有文字裁切/布局问题，模态/完整UI/Designer未验收。探针3,667,456字节、Demo4,147,712字节，均CLR目录0；IL2026/IL3050/IL3053与Demo既有IL3050保留。首轮29旧驱动通过，最终16个Control/上下文驱动复核结果在artifacts/compatibility-batch018/final-regressions/results.json；完整证据见evidence.json/018。
- 019证据：最终4,027测试全部通过、双目标Release 0错误/428警告；生命周期16场景/204行固定10.0.11参考/JIT/实际AOT严格全同，最终run为`20261016-021319-9490794`。父子集合391行参考/JIT全同，018隐藏重挂的中间VisibleChanged缺口已修复；集合AOT仍有32行非零下界数组fixture明确CopyTo未运行，其余359行同JIT，严格失败保留。16个旧Control/context驱动全通过。Demo AOT真实显示、复选框0→1、正常exit0且记录HWND全部失效；生命周期EXE4,115,456字节、Demo4,156,928字节，CLR目录均0。原始失败/警告/路径见`artifacts/compatibility-batch019/evidence.json`，不由这些记录宣称整Control/98%。
- 019收尾：Control-07/Form-06标部分并登记07a/06a完成子范围，FND及总览/历史同步；修正“标准句柄入口不存在”和“018隐藏重挂可见差异仍未修”的当前表述，保留原始历史失败。API固定1,095类型/61,240行/187页，成员运行仍unverified；408份Markdown、2,037项任务、187行逐控件实施索引0错误，整类型代码完成0。13项目/17目标均.NET10，原始Demo源码/Designer/resx不改，Windows官方Drawing10.0.0且net10.0依赖无WinForms/System.Resources.Extensions。
- 020责任：layout_core完成Control.cs/标准布局参数委托/25项回归，layout_containers完成10个必要容器文件/31项回归，layout_probe完成固定24场景探针/驱动；三者已冻结。主聊天独占共享文档/API/索引、顺序集成与Git。基础标准事务已完成，下一批沿Control-09/10补LayoutEngine/首选尺寸/完整几何；不要重复旧空入口评估。
- 历史完成批次：[017](docs/roadmap/batches/017.md)，两项代码、必要验证、路线图整理及推送完成；原内部子任务已结束。实现提交`2df2cca120f38b94d5c469b3687cd5cc5a800f15`，结束记录提交`77955c4066b3b8933133742ff7c3c9200b9f0fc6`均已成功推送。创建Control聊天前HEAD/origin/main/实际远端refs/heads/main三者均为77955c4，工作区干净。本次归属交接另行commit/push，成功核对后在开始消息中给出交接SHA，新主不得把017实现或结束记录SHA误作本次交接SHA。
- 历史实现提交：`10c106002acff0517bcb547f526fef446ec32871`（批次016），已成功推送origin/main；推送后HEAD/origin/main/远端refs/heads/main均为该SHA，工作区干净。本记录随后单独提交并成功推送归属交接，新主实际接管时核对并记录其SHA。015实现`21a71fc3646c736baa59132b39d5270a135e8688`/交接`79ccc4a9b4a177beccf6bdb79528906fc16b3f41`、014实现`3894f281a88ff00e806c14440ce5c332af974d6a`/交接`7f1d0709d32ff95340a2a0611a6cc603831f8d39`及013实现`a0e2ec02e15cc92f7f337812bb22b13b17f0a691`/交接`2434d48bbecda25112951cb11f2962c9b7761634`均已验证；原代码基点`b83f534`不是回退要求。
- 全量进度：[tasks.csv](docs/roadmap/tasks.csv)，由控件页及 FND/REP/CNS/XPL 任务表生成；逐 API 分母仍在 [api](docs/roadmap/api/baseline.json)。
- 当前边界：029有限自动焦点转移、移除候选及程序验证完成；点击提交取消、键盘/IME、全部派生仍缺，关闭多余LostFocus/WM_QUIT与SplitContainer继承投影差异真实保留。027基础鼠标范围完成，LockScreenBackstopFrame导致目标命中失败，物理点击/成功Click命中未验收；四消费者默认样式/事件顺序、AutoCheck/组同步和完整键盘验证仍缺。026有限05c/08c收尾保留；普通Parent/child/Site错误及活HWND失败仍短路，全部Dispose事务/并发wait释放未完。RTL/mirror/滚动/主题透明、owner draw/反射/缓冲、ambient/Cursor、DPI/派生布局/Designer/资源/模态和GTK4/AppKit未完成。旧004/005各两行、025四项、026九项增强、自定义Param AOT四差异/GetLParam IL3050、palette三差异、Form.Created、Text DispId AOT、集合32行fixture/中文Designer/Container.Add IL2026/TypeConverter IL3053及Demo既有IL3050全部保留，不宣称整Control/98%。

## 批次029完成与停止记录

批次029完成Control-12c/12d自动焦点转移与验证事件、ContainerControl-02b/03a移除候选及程序验证状态机、Form-07a关闭验证前置，并补Panel/ScrollableControl/TabControl必要批量验证消费。4,392项测试（新增49）全通过，双目标Demo Release 0错误/339警告。16场景全部正常退出/无HWND残留；最终固定参考1,832行、JIT1,880行、AOT1,847行：JIT对参考1/16严格相同，其余为15个额外WM_QUIT及11场景关闭LostFocus；JIT/AOT仅5/16严格相同，11场景相差关闭LostFocus的33行，未定位根因，严格失败保留。旧Focus157/父子集合391/生命周期204行参考JIT全同；028仍18/30同参考，保留11个WM_QUIT及SplitContainer投影差异。探针/Demo AOT为3,065,344/4,297,216字节，CLR目录0，Demo实际显示/复选框0→1/WM_CLOSE退出及原HWND失效通过。全部派生、点击提交取消、键盘/IME、物理设备、Designer/资源及GTK4/AppKit仍未完成，不宣称整Control或98%。

- 接管SHA：`9ff6f26000533469d2b07340bec5ce345b764697`；实现SHA：`4dd229ee106eec28eed33571752e1e9139fee0b6`，2026-10-17 01:47（Asia/Shanghai）已成功push，HEAD/origin/main/实际远端均一致且干净；见implementation-push.json。本停止记录另行提交推送，最终核验记stop-push.json。完整证据：`artifacts/compatibility-batch029/evidence.json`；固定参考run `20261017-001742-0376196`，最终run `20261017-013511-9230398`。
- 原始早期成功、fixture/PowerShell失败、父子集合7行回归及最终修复全部保留；最终JIT-AOT关闭LostFocus差异不能用早期16/16一致替代。关闭验证取消场景后续由fixture显式Dispose清理，不称Close已接受关闭。
- 收尾纠正FND-021验证任务漏记、Control-02遗漏028 Select/CanSelect、TabControl既有局部实现状态；未完父项/任务ID/固定分母保留。关闭/循环归属、全部派生验证消费、键盘/设备/Designer/资源/GTK4/AppKit继续待办。
- 用户新指示覆盖历史接续提示：029推送核验完成后停止；不创建下一聊天/030、不发送“开始接管”、不恢复巡检。只有新的明确用户指示才可恢复未完任务。

## 批次028完成证据与下一依赖（历史）

- 实现已成功推送`496e632d13e7e4f26dcb649093e69d589c9128d0`并核对三SHA/干净状态；029本地只读聊天`01a14565-97a2-76f0-8645-317df65705a7`已创建。本交接独立提交/推送并核对远端后才发送开始，确认新主实际接管即归档028。

- 批次028完成Control-12a基础选择导航、12b标准焦点通知、ContainerControl-02a逻辑活动链、Form-08a必要激活及TextBox-003a通知去重。4,343测试（新增28+26）全通过，双目标Release 0错误/646警告。30场景JIT/实际AOT全同，对固定10.0.11参考18同/12差异：11场景关闭后多出WM_QUIT，另1场景为SplitContainer继承链导致的活动容器投影差异；严格driver exit1保留。旧Focus157/父子集合391/句柄生命周期204行参考JIT全同。探针/Demo AOT为3,119,616/4,305,920字节，CLR目录0；Demo窗口可见、BM_CLICK复选框0→1、WM_CLOSE正常exit0且原HWND全部销毁。13项目/17目标仅.NET10，官方Drawing10.0.0、固定双目标及原始Designer/resx保持；API重新导出且1,095类型/61,240行/187页与运行unverified不变。自动焦点转移/完整派生导航、验证/键盘/IME、物理输入、Designer/资源与GTK4/AppKit仍未完成。
- 固定参考run `20261016-231510-4119028`；最终JIT/AOT `20261016-232925-3027818`；源码SHA256 `80527FBB6C3C20353A58CC4D105605A67B163CE05BEBA171F332729D4C064555`。参考6,770行/Lumina6,781行，全部exit0、complete标记和原HWND释放齐全；strictMatches=false/aotMatches=true。
- 11项差异仅增加pump-quit=True，属于既有Form/OpenForms消息循环退出缺口；container-lookup另1行少split:null，属于SplitContainer继承缺口。不得归一化、标成strict全同或把原生程序消息当物理输入。
- 首轮测试两失败及旧生命周期4行差异保留；Form句柄可见性恢复改SW_SHOWNOACTIVATE，避免早激活创建子HWND。最终4,343测试与旧157/391/204行回归通过，不修改旧期望。焦点探针IL3050、Demo既有IL3050/IL3053及全部历史边界保留。
- 已同步五个完成子项/未完父项、Form-08/12历史漏标与Focus(bool)误述，并纠正ROADMAP总标题026滞后；417份Markdown、2,088任务、187行逐类型实施索引校验0错误，整类型代码完成0。完整证据artifacts/compatibility-batch028/evidence.json。下一批[029](docs/roadmap/batches/029.md)先固定自动焦点转移/验证前置，不重做全项目评估。

## 批次027完成证据与下一依赖（历史快照）

- 实现已成功推送c87c52849e19098c02dc00898c9960424abf21da，三SHA/干净状态已核；028只读准备聊天01a14527-ef1d-7440-8ca2-fc11b53f515f已创建。本交接另行commit/push/核远端后才发开始，确认实际接管即归档027。

- 批次027完成Control-13a坐标/捕获、13b基础鼠标事件/消息跟踪及五处必要消费者共享Click子范围。4,289测试（新增21+25）通过，双目标Release 0错误/645警告；25共同场景549行、固定参考20同/5差异，JIT/实际AOT24同/1异常文本差异，所有进程exit0。锁屏导致参考10、JIT/AOT20次WindowFromPoint命中失败，成功命中与物理点击未验收。生命周期204及消息绘制558行参考/JIT回归全同。输入探针/Demo AOT为3,107,840/4,298,752字节且CLR目录0；Demo原生窗口可见、BM_CLICK复选框0→1、WM_CLOSE正常exit0/原HWND全失效，无强制清理。13项目/17目标仅.NET10、官方Drawing10.0.0、固定双目标及原始Designer/resx保持；API已重导出但固定1,095类型/61,240行/187页及运行unverified不变。完整Control/派生输入/键盘验证/资源Designer/平台和旧差异仍未关闭。
- 027主为`01a144ef-726b-78a3-8b32-d9bd4e73eb2d`（local）；接管32f65b9e411d9838ebcc5c1f0a1af7f61add972b时三SHA相同且干净，026旧主已归档。control_input_core/input_consumers/reference_probe均冻结，无后续写入/构建/Git；主统一完成共享interop、API和收尾。
- 参考run `20261016-220702-8990064`；最终JIT/AOT run `20261016-222106-7575539`；源SHA256 `A47B7CDA7FF8E64917279641FC0C3689E0DE644B82DB6DA265449E1C0743D407`。FlatStyle.System另3场景仅参考，不进共同分母。direct-helpers保留命名空间/Native AOT原生异常文本，四消费者保留实际差异；strict driver exit1不隐藏。
- 锁屏诊断为LockScreenBackstopFrame，visible=true、enabled=false、rect0,0,1920,1080；尚无用户解锁回复。原生Capture/坐标/消息和程序BM_CLICK有证据，成功WindowFromPoint点击/物理设备仍待验。下一主不得用程序消息或修改期望冒充解锁验收。
- FND-020/CNS和七个明确子项同步；纠正Control-12已有Enter/Leave、CheckBox/RadioButton既有CheckedChanged及RadioButton Parent旧阻塞遗漏。父项、平台、原ID/未完项/固定分母保留，README实施完成ID列和tasks.csv由原工具生成；本机证据artifacts/compatibility-batch027/evidence.json。
- 收尾校验416份Markdown、2,083项任务、187行逐控件实施索引0错误，整类型代码完成0。已重导出API并保持固定分母及运行unverified；原始Demo/Designer/resx未改。
- 下一批[028](docs/roadmap/batches/028.md)先固定焦点/选择导航与必要ContainerControl/Form协作参考；完整键盘/验证/IME后续按依赖继续，完整Control目标、Linux/macOS代码和Windows验收不因本批结束而停止。

## 批次026完成证据与下一依赖

- Control-05c：失败通知结算全部已接受未执行InvokeAsync任务；保留取消先后/原错token/重建/已开始工作/ValueTask单消费，不重复ThreadException。Control-08c：实际HWND已销毁后的原生错误继续正常托管步骤，后续托管失败仍短路，单错EDI/多错按序聚合，未改Form.cs或公共API。28+20新增回归与全部4,243测试通过。
- 参考run `20261016-210951-8765335`，JIT/AOT run `20261016-211451-3933342`，源码SHA256 `CCEA907A90096CF37AEB12372B978D9C95BE4FB7A38B0B2C13FDA43B6216D5F4`。25标准场景16同/9增强差异，另7旧Task只Lumina；32场景JIT/AOT全同、全部exit0，严格参考driver exit1保留。pendingTaskObservations包含before-source门控中间值，不是最终悬挂任务数。
- 双目标Release0错误/643警告；探针AOT3,851,776字节、Demo4,289,024字节，CLR目录均0。Demo真实显示/复选框0→1/WM_CLOSE exit0/原始HWND全失效，无强制清理。旧投递69/context57/生命周期204行参考JIT同；旧004/005各两行关闭增强差异，025仍四同四差异，原始旧日志/首轮fixture失败均保存。本批旧AOT未重跑，证据artifacts/compatibility-batch026/evidence.json。
- 13项目/17目标仅.NET10，原始Demo/Designer/resx未改，Windows官方Drawing10.0.0且net10.0两依赖图无WinForms/Resources.Extensions/自有Drawing。Control/FND当前缺口按实际范围修正；Form-07已有Close/WM_CLOSE/取消/Dispose却仍全待办，纠正为部分并保留全部剩余。仅05c/08c代码完成，父项/平台/固定API运行unverified保留。
- 下一批027先固定Control-13的坐标、Capture和鼠标输入参考，核心Control、必要Form/消费、参考三个互斥内部任务；共享interop/API/文档/顺序构建/Git由主独占。按已有基础继续真实交互，未完竞态/资源/Designer/平台不删除，不恢复无关循环或暂停巡检。
- 收尾校验：415份Markdown、2,076项任务、187行逐控件实施索引0错误，整类型代码完成0；README引言和验证页025遗漏历史已纠正。实现已成功推送c0f4c3d8bc12e2684cb9ddb6bca765373f8c4c60；027只读准备聊天01a144ef-726b-78a3-8b32-d9bd4e73eb2d已创建，本次独立交接成功核对后才发送开始，确认实际接管即归档026。

## 批次025完成证据与下一依赖

- 025完成Control-04c、Control-07b、Control-17b、Form-16c、WindowsFormsSynchronizationContext-06a。`Lumina.Tests` 4,195/4,195 通过；`Lumina.slnx` Release 0错误/641警告；固定WinForms 10.0.11参考、Lumina JIT、Lumina Native AOT各8场景均 `exit=0` 且 `complete=True`，4场景严格相同，4项真实差异保留在`artifacts/compatibility-batch025/control-boundary/comparison.json`。
- NativeFormsDemo `net10.0` AOT 实际显示、Enable option 复选框 0→1、WM_CLOSE 正常退出，原始 HWND 全部失效；Demo PE 4,287,488 字节、Control 探针 PE 2,989,056 字节，CLR directory RVA/size 均为0。现有IL3050/IL3053/NU1510/CA1416和XML/分析器警告按原始日志保留，未全局 suppress。
- 下一批先按只读准备聊天`01a144c0-6427-79a2-a2cd-55d0dd562990`核对025实现/文档/远端状态，再由当前主更新本节、提交并推送交接SHA，发送“开始接管”并确认新主实际接管；确认后当前主停止写入/构建/Git并立即归档，不等待下一批完成。

## 批次024完成证据与下一依赖

- 批次024完成Control-04b/14b、Form-16b：关闭/失败投递完成余队列后保留错误，真实销毁HWND；补背景图像五布局、真实父子透明/alpha与DC/Graphics状态，Form取消默认COMPOSITED修复失效区域。4,188测试（新增30）通过，双目标Release 0错误/641警告。21场景307行JIT/实际AOT全同，对固定10.0.11保留16行关闭增强差异及严格失败；6组旧参考/JIT回归全同，外观仍3条palette差异。背景/Demo AOT为3,450,880/4,284,928字节，CLR目录0；Demo实际显示/复选框0→1/exit0/窗口全销毁。既有IL3050和023自定义Param AOT四差异保留；普通Drain、InvokeAsync关闭、native双错/Dispose后续清理、完整输入/绘制/Designer/资源/DPI及GTK4/AppKit仍缺。

- 本批固定参考run 20261016-104912-4009835，最终JIT/AOT run 20261016-110417-5852973，源码SHA256 C796161745EA4AB770586AAACC8B7EB2600A78ADEAB1C8F8472B1010D7C416A5。原始差异与进程/TRX/PE/依赖/索引汇总见artifacts/compatibility-batch024/evidence.json；仅三个明确子范围✅，父项、187类型索引及运行unverified保留。

- 下一批025的执行收尾独占WindowThreadDispatcher，native边界独占Control/Form/context，参考任务独占新探针；统一主负责文档/顺序集成/Git。先修普通Drain与回调/清理双错，InvokeAsync包装终止按共享文件依赖后续串行，不扩为所有竞态已完成。

## 批次023历史证据与当时下一依赖

- 明确范围：Control-14a/17a、Form-16a；标准Message/CreateParams/WndProc/DefWndProc、样式与基础真实绘制/失效/更新/打印，原生控件保留原过程。原生回调外层无条件释放映射/句柄/root/HFONT，公共OnPrint保留背景回调状态修改，native打印隔离背景状态；透明子项Invalidated移除/释放重入已修。
- 4,158项测试全部通过，新增20项；双目标Release 0错误/635警告。四组旧差分：布局512、事务394、生命周期204、父子集合391行参考/JIT全同，旧AOT未重跑；集合旧32行fixture边界保留。
- 消息绘制18场景558行JIT严格同固定10.0.11；实际AOT完成558行，554同、4条自定义CreateParams.Param差异，strictMatches=false/aotInScopeMatches=true，原始输出不变。最终run 20261016-101835-1132631；原参考20261016-091744-1927945，源码SHA256 B9964B910AE3E42AF3D2E988BC0E806C924EA43186B20CF1D78CBDD9B62E18F7。
- 外观最终run 20261016-101947-4132109位于既有batch022驱动目录：212行JIT/AOT全同，对原参考仍3条palette前景差异/driver exit1，原10条WM_GETFONT差异由真实UserPaint修复。保留022历史13条失败快照。消息/外观探针与Demo实际AOT分别3,088,896/3,091,968/4,265,984字节，CLR目录均0；Demo显示/复选框0→1/exit0/记录窗口全销毁，无强制清理。
- Message.GetLParam(Type)存在IL3050，AOT仅显式根化Payload有证据；任意marshaller和custom Param不宣称支持。透明/alpha探针没有HWND、PRINTCLIENT像素为位图，不证明屏幕透明合成。全证据artifacts/compatibility-batch023/evidence.json；原失败、TRX、进程/PE/警告保存。
- 13项目/17目标均.NET10，原Demo源码/Designer/resx未改，Windows官方Drawing10.0.0且net10.0无WinForms/Resources.Extensions/自有Drawing；API固定1,095类型/61,240行/187页、运行unverified。仅三个明确子范围✅，父项仍部分，历史状态/任务索引按实际代码重整，不由测试数推98%。
- 下一批024先处理已释放AsyncWaitHandle使WindowThreadDispatcher.Close中断剩余回调，以及DestroyHandle预先Close阻止销毁；随后补BackgroundImage/Layout与有HWND透明绘制。投递关闭/背景绘制/参考子任务互斥；主统一构建、共享文档/API/索引、Git。完整授权与收尾/推送/归档规则见024及下方接续提示。

## 批次022历史证据与当时下一依赖

- 实现98cde66ca344ccbf8755faaca314e6936788df73已成功推送并核对三SHA/干净状态；023只读准备聊天01a143ef-9e5b-76f2-9cfc-bb811bf4a0e2已创建。本归属交接独立提交推送，确认新主实际接管后旧主立即归档。
- 批次022完成Control-11b基础Font/BackColor/ForeColor局部值、父级继承/Reset/通知及08b借用Font/独占HFONT寿命，必要Form/TextBox/GroupBox/Tab/条带消费接入。16场景212行JIT与实际AOT全同；对固定10.0.11仍有13条真实差异：10条基础Control缺UserPaint路径的WM_GETFONT，3条默认palette前景色，strictMatches=false。4,138项测试（新增31）通过，旧默认布局512/事务394/生命周期204/集合391行参考/JIT均同；双目标Release 0错误/478警告，Demo AOT真实显示、复选框0→1、正常exit0且记录HWND全失效。探针2,971,136字节、Demo4,196,352字节，CLR目录均0；探针无IL2xxx/IL3xxx，Demo既有IL3050保留。
- 完整Styles/UserPaint/消息绘制、其余ambient来源/RightToLeft/Cursor、字体自动缩放/DPI、全部派生布局/输入、Designer/资源/模态与GTK4/AppKit仍缺；父任务保持部分，不宣称整Control或98%。
- 固定参考run 20261016-082323-2290099，最终JIT/AOT 20261016-084839-8131815；旧事务20261016-084716-3291017、生命周期20261016-084724-2175337、默认布局20261016-084735-6315609，父子集合在022目录。完整证据artifacts/compatibility-batch022/evidence.json。仅4组旧差分在本批重跑，不能写成16驱动。
- 20核心+11资源新测试，首次编译/运行失败和GetObjectType假阳性诊断保留；MenuStrip宿主错误替换已真实修复。13项目/17目标、原始Demo与官方Drawing约束不变，API固定分母和187页运行unverified不变。
- 已核对本批与受影响历史状态：GroupBox标准集合/局部默认/原生、Form继承缩放/原生、Tab/条带原生、Control/FND已有Invalidate/内部窗口过程改为部分；仅明确子范围✅，不删除任务或关闭整类型。
- 下一批023核心/必要契约原生衔接/固定参考三个互斥内部任务，主独占共享interop/文档/顺序集成/Git。先对照Message/CreateParams/Styles及真实UserPaint，不伪装WM_GETFONT；完整Control目标与所有授权/验收/推送交接归档规则不变。

## 批次021完成证据与剩余依赖（历史快照）

- 实现已推送accc4f92ab9ea1e334a9419872542cb8f3227bf3并核对三SHA/干净状态；022只读接续聊天01a143be-8230-74b0-aa82-837bcf0b8563已创建。本次归属交接独立提交推送，确认实际接管后旧主立即归档。

- 021完成Control-09d/10c基础默认布局、首选尺寸/缓存和标准LayoutEngine及必要容器衔接。18场景512行固定10.0.11/JIT/实际AOT严格相同；旧020事务394行参考/JIT七行差异清零，019生命周期204行和父子集合391行参考/JIT同。4,107项测试及16个旧Control/context驱动全部通过，双目标Release 0错误/464警告；Demo AOT真实显示、复选框0→1、正常exit0且记录HWND全失效。布局探针3,041,280字节、Demo4,192,256字节，CLR目录均0；探针无IL2xxx/IL3xxx，Demo既有IL3050保留。
- 完整派生测量/滚动/Flow/Table/条带/Split边界、DPI/RTL及复杂布局缓存重入、非Control布局对象仍缺；Control-09/10、FND-014/015和容器父项继续部分。Control-11字体/颜色继承及必要主题/原生资源消费是下一依赖；完整Control Windows/Designer/资源/模态与GTK4/AppKit未关闭，不宣称98%。
- 默认布局最终run为20261016-074614-3648884，固定参考为20261016-073734-9109514；旧事务run为20261016-074817-1254094，生命周期run为20261016-074822-9151166。完整证据artifacts/compatibility-batch021/evidence.json。核心13项+容器11项新增回归；首轮两个失败、fixture编译失败和中间9行差异保留，最终不做输出归一化。
- 收尾更正Control-17已存在Win32代码仍全标待办、Flow/Table有限测量及CNS-008引擎仍全待办；父项/平台/98%不关闭，固定分母和187页运行unverified不变。README逐控件实施列及tasks.csv用既有工具重生成，13项目/17目标均.NET10，原Demo源码/Designer/resx未改。
- 下一批022沿Font/BackColor/ForeColor继承和实际资源/主题消费；Control核心、必要容器资源、独立固定参考三个内部任务，先约定接口；共享interop/文档/API/顺序构建/Git由主独占。完整授权、验收、提交推送/远端核对、只读准备→归属交接→开始接管→确认后归档规则不变。

## 批次020完成证据与剩余依赖（历史快照）

- 020完成Control-10b标准布局事务及Form/Flow/Table/Split/Tab/条带必要衔接；4,083测试、16个旧Control/context驱动、204行生命周期及391行父子集合JIT回归通过。24场景394行JIT/实际AOT严格同；参考有7行Dock/Split几何差异，strictMatches=false，原始失败保留。双目标Release 0错误/454警告；Demo AOT真实显示、复选框0→1、正常exit0且全部记录HWND失效。布局探针2,872,320字节、Demo4,159,488字节，CLR目录均0；探针无IL2xxx/IL3xxx，Demo既有IL3050保留。
- 完整LayoutEngine/DefaultLayout、PreferredSize缓存、InitLayout/ResumeLayout(false)引擎初始化、Dock/Anchor/AutoSize及Split比例仍未完成；Control-09/10、FND-014/015和容器父项保持部分。Form.Created、完整Windows Control/Designer/资源/模态及GTK4/AppKit继续待办，不宣称整Control或98%。
- 固定参考run为20261016-024318-6186215，最终JIT/AOT为20261016-025123-8344158；完整日志、原始差异、PE、约束与索引摘要在artifacts/compatibility-batch020/evidence.json。25项核心+31项容器新增回归；首次fixture失败保留。参考22个完整场景356行同，394总行387同，7行几何差异不归一化。
- 收尾已同步Control/Form/ScrollableControl/ContainerControl/Panel与七容器、FND/CNS及总览历史；纠正Panel对018标准层/集合的遗漏、CNS对018事件/集合的遗漏及布局空入口过时描述。明确子范围才✅，完整父项继续🟡；固定1,095类型/61,240行/187页及unverified不变；409份Markdown、2,046项任务与187行逐控件实施索引校验0错误，整类型代码完成仍为0。
- 收尾源码复核另发现Dock↔Anchor切换事件边界：当前Anchor setter通过公共Dock=None可能额外触发DockChanged，固定参考走内部SetDock；394行探针未覆盖该组合。后续默认布局需修正并取得独立证据，不将7行已观测几何差异泛化为全部剩余差异。
- 021先定义实际默认布局/LayoutEngine/首选尺寸范围，再分配核心、必要容器和固定参考三个互斥内部任务；Control/Form/interop重叠串行，主统一顺序构建与Git，完整Control目标和全部交接规则不变。
- 021内部责任已完成并冻结：default_layout_core负责Control/默认引擎/13测试，default_layout_containers负责10库文件/11测试，default_layout_probe负责18场景固定探针/驱动及只读审查；主独占共享文档/API/索引、旧fixture、集成/Git。旧020聊天已核实归档。

## 批次017完成证据与结束范围

- Control-11a：virtual ResetText消费虚Text setter，保留虚getter判等、通知/原异常/重入、null/Unicode/NUL/引用和释放后托管边界，219项新增回归通过。3,774条固定10.0.11参考/JIT相同；实际AOT只有Text属性列表缺DispId=-517，另3,773条原始记录相同。实际编译参数及CoreLib内嵌RemoveAttributeInstances证实原因，原始差异/首次失败保留，strictMatches=false，不改库AOT策略。
- ListViewItem-04b：未挂接ImageIndex/ImageKey默认、验证/失败状态、key-index互斥及重复/引用身份完成，518条固定英文参考/JIT相同，363项新增回归。首项恢复/三枚举/各view key缓存/样式/借用资源保持；NoneExcludedImageIndexConverter、ImageKeyConverter和ImageIndexEditor仍缺，新范围未计实际AOT运行验收。
- 集成：3,934项测试全部通过，0失败/跳过；29个旧驱动通过；双目标Release 0错误/最终增量301警告。文本AOT EXE2,049,024字节实际运行，无IL2xxx/IL3xxx；完整Demo AOT4,108,288字节仅发布，二者CLR Header 0。Demo既有IL3050、主窗交互/正常关闭、设计器及三平台验收保持待完成。
- 13项目/17目标仅.NET10，Windows官方Drawing10.0.0，原始静态Designer/resx和net10.0依赖满足要求。API固定分母1,095类型/61,240行、运行unverified；405份Markdown/187组类型/API页/2,033项任务及187行实施索引检查0错误。Control-11和ListViewItem-04/FND-003保持部分完成，整类型代码完成0，没有整控件或98%声明。证据artifacts/compatibility-batch017/evidence.json。
- 017结束时按当时用户要求停止接续，完成记录为77955c4；本节保留该历史边界。后续用户已另行授权上述Control专项，原ID/优先级和全部017证据继续保留，巡检仍不恢复。

## 用户已经确定的范围

2026-10-16最新授权：在新本地聊天完整实现Control，至少完成Windows验收，其他系统完成对应代码；本专项目标优先于旧轮次“017后停止接续”以及实施阶段统一后置真机验证的安排。使用多个互斥内部子智能体，保留单一编码主；Control.cs、Form及共享interop的写入责任需串行协调，主聊天统一集成/验证/共享文档/Git。按既有批次机制推进到整个目标完成，必要父类/容器/Application/平台资源依赖纳入，但不扩展到所有无关控件。代码完成、平台编译与真实运行验收分开记录；按既有coverage口径核对完整Control清单，不按测试数量计算98%。新授权不恢复已暂停的luminaforms自动化或无关路线图循环。

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

以下为历史巡检恢复时的协调规则，当前luminaforms仍暂停，不能凭本段自行恢复：巡检先读本文件，再查编码主会话状态和最新批次记录。会话正常运行时不另起竞争主会话；空闲但未完成时，按当时有效授权给该会话发送继续消息。批次完成但尚未接续时才创建下一会话。出现错误先读取必要上下文恢复，不在活跃写入时reset/clean/换分支。当前Control专项必须在结束前完成其Windows验收，无需等全部控件开发完成；最终专项目标完成或用户停止时结束。

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

## 历史接续提示（2026-10-17停止指示优先，不可自动执行）

“继续D:\source\Lumina的Control专项。用户要求在新会话把Control完整闭环，Windows取得实际验收证据，Linux/macOS完成对应代码；完整专项目标完成后结束，不仅完成一个小子范围，不恢复无关路线图循环。先只读AGENTS.md、PROJECT_REQUIREMENTS.md、HANDOFF.md、当前批次、Control任务/API页及coverage/platforms；收到旧主单独发送‘开始接管’和已成功推送的交接SHA、核实归属与Git状态后明确实际接管并开始实施。使用多个内部子智能体，一人一任务、互斥文件责任；Control.cs/Form/共享interop重叠串行协调，单一主聊天负责共享文档、集成、统一顺序构建/探针、提交推送。按真实依赖完成Parent/Controls/Form标准链及完整Control剩余清单，必要依赖纳入，不扩散到无关控件。所有项目含分析器/工具仅.NET10；保留Lumina.Forms标准API、固定双目标、原始Designer/resx、Windows官方Drawing及Native AOT；Linux GTK4/macOS AppKit与目标OS/RID Drawing策略遵循需求，不换UI栈或新公共抽象。Windows真实窗口/生命周期/布局/输入/绘制/投递、实际AOT运行正常关闭及相关设计器/资源验证是本次完成条件，不能沿用统一后置规则。其他平台实现、编译与真机验证分开，不以stub/空方法/吞异常/常量成功冒充实现。用户已授权必要实施/修复/验证、提交推送及按批本地接续；luminaforms自动化保持PAUSED，不恢复30分钟巡检，后续暂停/停止优先。每批实现和交接都必须commit并成功push，记录实现SHA、核对实际远端refs/heads/main，本地commit不算完成。每批提交前按真实代码/必要检查整理本批及受影响历史任务，同步ROADMAP/执行总览、相关控件/公共/平台任务页和历史、批次/HANDOFF，用现有Validate-Roadmap.ps1 -ExportTasks重生成逐控件README实施状态/完成ID列与tasks.csv并校验。明确子范围全部实现才✅，父项未完仍🟡/🚧且列剩余；🧪只限可复现的平台/范围证据。文档/签名/编译/局部探针/基类不能关闭整控件/平台；纠正历史错标并说明，保留ID/未完项/固定分母，不用测试或任务数量推98%，不另建调度报告框架或重启全项目评估。需要下一批接续时先创建下一本地只读准备聊天，再更新归属并commit/push/核对远端，之后发送‘开始接管’；确认新会话实际接管后旧主停止所有写入/构建/Git并set_thread_archived归档，不等待下一批结束。每份接续提示保留完整Control目标、用户授权、验收边界、commit/push/远端核对及归档规则。”

## 批次 014 新增接续证据

- Control-09c：明确初始化后的 Min/Max 运行期约束与默认虚扩展完成，33,408 条固定10.0.11/JIT/实际 AOT全同，394项新增回归。base getter仍读默认扩展，负值/零轴/冲突/取消按参考，core raw相等先返回，保留009～013边界/客户区/通知。构造期默认约束与DefaultSize统一留015的Control-01b，--constructors已有独立原始状态/调用/异常参考，不能当作已实现。
- ListViewItem-04a：Name首项代理、独立Tag/ToolTipText完成，151条固定英文参考/JIT全同，101项新增回归。读空表不恢复，写null/空也恢复首项并换表；共享view/三枚举/独立key缓存/毒丸/隐藏成员有证据。中文Designer分类Data/Appearance与参考“数据/外观”不同，完整所属/挂接Tooltip/native/Clone/序列化仍缺，新范围未计实际AOT运行验收。
- 集成：2,228项测试全通过，双目标Release 0错误/本次增量300警告，23个既有差分驱动通过。约束AOT实际33,408条相同，EXE2,641,920字节、无IL2xxx/IL3xxx；Demo AOT4,107,776字节，两者CLR Header0，Demo既有IL3050保留。首次AOT动态AmbientValueAttribute.Value抛原异常，最终仅核对构造元数据，该动态转换与中文分类/描述本地化缺口单列，未全局根化或吞异常。
- 13项目/17目标仅.NET10，原始Designer/resx/net10.0依赖满足要求；API分母1,095类型/61,240行、运行状态unverified不变。日志/TRX/PE/项目/依赖及已知差异摘要在artifacts/compatibility-batch014。
- 下一批015：Control-01b子智能体独占Control.cs及独立DefaultSize/构造探针测试（含构造期DefaultMin/Max消费）；ListViewItem-01c子智能体独占ListViewItem.cs及独立ToString探针测试。仍先固定参考、主聊天统一顺序运行后授权实现，主聊天独占共享文档/API/索引、必要interop、集成与Git。
