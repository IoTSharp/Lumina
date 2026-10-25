# 执行顺序与发布门槛

2026-10-15用户确定逐批代码实施与每批新主聊天/多个互斥子智能体机制；根目录[HANDOFF.md](../../HANDOFF.md)是接续入口。2026-10-16另行授权完整Control专项，Windows实际运行/Native AOT及相关Designer/资源验收纳入该专项完成条件，不能继续一概后置。Linux/macOS完成对应代码并单列编译与设备证据；无关全量循环和暂停巡检不恢复。

任务按依赖和真实迁移阻塞排序。P0 先做；P1 是共同基础及常见场景；P2 是业务/高级能力；P3 是历史专属和低频补齐。P3 代表后做，不代表从全量兼容目标删除。跨平台不等待每个历史 Windows 控件完成，但必须先通过资源与基础原型。

| 顺序 | 里程碑 | 优先级/状态 | 具体任务入口 | 完成门槛 |
| --- | --- | --- | --- | --- |
| 00 | 基线可追踪 | P0 🟡 | [计量](coverage.md)、[类型/成员](api/types.json)、[全控件索引](README.md) | 清单已生成；行为场景和证据映射仍需建立 |
| 01 | M0 阻塞 | P0 🟡 | [公共基础 FND-001～006](foundations.md)、[ListViewItem](controls/ListViewItem.md)、[项目任务REP-001/002](project-consolidation.md) | 线程/基础构造/启动签名与Form标准链已实现；完整AOT主窗/模态与警告仍未闭环 |
| 02 | M1 控件/窗体骨架 | P1 🟡 | Component → [Control](controls/Control.md) → 容器/[Form](controls/Form.md)；[BindableComponent](controls/BindableComponent.md) → ToolStripItem；[Application](controls/Application.md) 与 [公共基础](foundations.md) | 基础构造/边界/通知/释放、Parent/Controls/Form标准链首范围完成；标准句柄、事务和基础默认布局/首选尺寸已实现明确范围，完整派生布局/继承属性/键盘/验证/模态/设计器仍缺 |
| 03 | M2a 高频简单控件 | P1 🟡 已有局部代码，开发/验收未完成 | ButtonBase、Button、CheckBox、RadioButton、Label、TextBoxBase/TextBox、Panel/GroupBox、ProgressBar、PictureBox | 每控件附件+行为过门槛；AOT资源/键盘/释放验证 |
| 04 | M2b 已有确定性薄弱控件 | P1 🟡 已有局部代码，开发/验收未完成 | [TreeView](controls/TreeView.md)、[ListView](controls/ListView.md)、[RichTextBox](controls/RichTextBox.md)、MaskedTextBox、NumericUpDown、DateTimePicker/MonthCalendar、LinkLabel、ToolTip | 原生状态与托管集合一致；真实交互及事件顺序通过 |
| 05 | M2c 容器/列表/菜单 | P1 🟡 已有局部代码，开发/验收未完成 | ListControl/ComboBox/ListBox、TabControl、SplitContainer、Flow/TableLayoutPanel、ToolStrip全族（含OverflowButton/PanelRow）、系统对话框/NotifyIcon；随后TaskDialog整族 | 原生与托管双向同步、嵌套布局、所有者/取消/快捷键；TaskDialog页面导航/按钮/进度/关闭取消 |
| 06 | M3 业务链路 | P2 ⬜ | Binding/BindingSource/BindingContext/CurrencyManager → ErrorProvider → DataGridView基础/行列/样式/六种单元格/集合 → 编辑控件 → BindingNavigator；ImageList/Timer等 | 真实对象列表编辑、校验、排序、虚拟模式可用且AOT；列模板/Clone/共享行/编辑提交不遗漏 |
| 07 | M3 补常用缺项 | P2 ⬜ | CheckedListBox、DomainUpDown、TrackBar、ScrollBar/H/VScrollBar、ToolStrip容器和下拉 | 所有控件有独立验收，不只看Demo能编译 |
| 08 | M4 深度兼容 | P2/P3 ⬜ | PropertyGrid及GridItem/PropertyTab、Printing、派生/自绘、UIA、OLE/拖放、WebBrowser、MDI；DataGrid/旧Menu/ToolBar/StatusBar占位契约 | 全量未评估项归零；历史项先匹配固定.NET10标准异常，恢复旧版UI作为可选扩展；剩余项公开 |
| 09 | XPL 平台进入闸门 | P1研究 🔎 | [XPL-001～015](platforms.md) | 可在M1之后并行一个平台原型；资源契约未解决不承诺全量后端 |
| 10 | M5 平台推广 | P2 ⬜ | 各控件Linux/macOS任务，按03～08的相同业务依赖顺序 | 各平台可移植属性/事件/行为逐控件 ≥98%，专属能力清楚披露 |

完整控件列表与逐项任务见 [索引](README.md)。同一里程碑内部先修被多个控件共用的基础；不按控件名称字母顺序分配开发。

控件范围按 [补漏审计](scope-audit.md) 的继承闭包检查：Control含抽象基类，ToolStripItem、CommonDialog、Menu、非可视Component及TaskDialog另外核查。不能只完成62个具体Control就宣称所有交互对象已覆盖。

控件页的 P0 表示“开始该控件前先做的工作”，不是把M4历史控件的P0插到M0线程修复之前。全局按本页里程碑与依赖先后，再按控件内P0→P3执行。[tasks.csv](tasks.csv)按阶段/优先级/类型提供筛选索引，不是自动排程，也不是API支持率分母。

## 当前接续与每批收尾

批次029完成Control-12c/12d自动焦点转移与验证事件、ContainerControl-02b/03a移除候选及程序验证状态机、Form-07a关闭验证前置，并补Panel/ScrollableControl/TabControl必要批量验证消费。4,392项测试（新增49）全通过，双目标Demo Release 0错误/339警告。16场景全部正常退出/无HWND残留；最终固定参考1,832行、JIT1,880行、AOT1,847行：JIT对参考1/16严格相同，其余为15个额外WM_QUIT及11场景关闭LostFocus；JIT/AOT仅5/16严格相同，11场景相差关闭LostFocus的33行，未定位根因，严格失败保留。旧Focus157/父子集合391/生命周期204行参考JIT全同；028仍18/30同参考，保留11个WM_QUIT及SplitContainer投影差异。探针/Demo AOT为3,065,344/4,297,216字节，CLR目录0，Demo实际显示/复选框0→1/WM_CLOSE退出及原HWND失效通过。全部派生、点击提交取消、键盘/IME、物理设备、Designer/资源及GTK4/AppKit仍未完成，不宣称整Control或98%。 当前收尾见[029](batches/029.md)。用户于2026-10-17要求当前批次完成后停止：提交推送和远端核验后结束，不创建030/接续聊天、不恢复luminaforms巡检。未来如获新授权，优先解决关闭焦点与循环归属、派生验证消费，再接FND-019键盘；这只是剩余依赖记录，不自动启动。

2026-10-26新授权恢复实施，批次030完成FND-019/Control-12e基础键消息与事件契约首范围；4,397项全线测试和Forms双目标Release构建通过。完整PreviewKeyDown/命令预处理/KeyPreview/IME/真实设备/AOT/Designer及跨平台仍待办，见[030](batches/030.md)。

2026-10-16：批次024完成Control-04b/14b、Form-16b：关闭/失败投递完成余队列后保留错误，真实销毁HWND；补背景图像五布局、真实父子透明/alpha与DC/Graphics状态，Form取消默认COMPOSITED修复失效区域。4,188测试（新增30）通过，双目标Release 0错误/641警告。21场景307行JIT/实际AOT全同，对固定10.0.11保留16行关闭增强差异及严格失败；6组旧参考/JIT回归全同，外观仍3条palette差异。背景/Demo AOT为3,450,880/4,284,928字节，CLR目录0；Demo实际显示/复选框0→1/exit0/窗口全销毁。既有IL3050和023自定义Param AOT四差异保留；普通Drain、InvokeAsync关闭、native双错/Dispose后续清理、完整输入/绘制/Designer/资源/DPI及GTK4/AppKit仍缺。见[024](batches/024.md)，M0/M1不关闭。

历史批次[026](batches/026.md)完成Control-05c/08c：已接受InvokeAsync任务关闭/投递失败终止、实际HWND已销毁后的异常继续正常托管清理；活HWND失败、普通Parent/child/Site短路与已开始异步工作保留。4,243测试（新增48）通过，双目标Release 0错误/643警告；25标准场景16同/9明确增强差异，另7旧Task仅Lumina，32场景JIT/实际AOT全同。探针/Demo PE均无CLR目录，Demo真实交互/WM_CLOSE/原始HWND销毁通过。旧004/005各两行增强差异、025四差异与警告不隐藏。Form-07依据已有Close/取消/Dispose由全待办纠正为部分；父项/设计器/资源/完整竞态/平台不关闭。当时下一批027已完成下述明确范围；保留026历史证据。

批次[025](batches/025.md)历史快照：普通Drain在完成信号失败后继续FIFO余队列并延后传播完成错误；Control/Form/context的native callback与`WM_NCDESTROY`清理双错边界按有限范围实现。4,195项测试通过，双目标Demo构建0错误，Control探针与Demo Native AOT均为无CLR directory，真实Demo窗口交互/WM_CLOSE/原始HWND销毁通过。4项参考差异保留；当时InvokeAsync关闭桥接与Dispose后续托管收尾缺口由026完成上述有限范围，全部事务/竞态和完整Control仍未关闭。
用户于 2026-10-16 明确要求每批结束检查并重新整理路线图，不得虚标。每批实现提交推送前必须逐项核对本批及受影响条目的实际代码和必要检查，同步 ROADMAP.md、本执行总览、相关控件/公共任务页、修改历史、批次记录和 HANDOFF，重新生成 tasks.csv 并校验一致性。明确范围全部实现后才标 ✅；未完父任务保持 🟡/🚧，在原任务下列出完成子范围与剩余项。文档、签名、编译、局部探针或基类完成不等于完整控件/平台完成。🧪 只对应可复现的平台/范围证据，真机/设计器/跨平台继续待验收，不由测试数/任务数推出98%。发现历史错标要纠正并解释，不删除未完项、不重置ID、不缩小固定基线分母。整理随本批实现 commit/push，不为监督消息做空提交，不另建调度或报告框架；完整要求传给下一聊天。

## 第一个 90 天

按 2～3 名熟悉 C#/.NET/Win32 的全职工程师的工作顺序估计，不是交付整个框架的承诺：

1. 第1～2周：M0；补齐行为场景模板；选择3个真实迁移候选，记录窗体/控件/第三方/Windows互操作。
2. 第3～6周：M1；首个真实配置/设备工具迁移，开始M2a；对照实际API附件修正优先级。
3. 第7～10周：集中M2b集合/输入缺口及M2c容器/菜单；不并行铺所有高级控件。
4. 第11～12周：稳定首批迁移、AOT分发、设计器往返；有真实平台设备和需求时完成一个有时限的平台原型。
5. 第90天：按已关闭缺口、每控件验收率、真实迁移改动量和资源原型结果重估M3～M5。完整DataGridView/PropertyGrid、完整Windows98%以及两个原生后端不承诺90天完成。

## 每个控件的完成定义

开发完成与兼容验收分开：明确范围的代码实现加必要编译/轻量检查后记 ✅ 代码完成，并立即推进下一任务；未完成子项保留，父任务不得跟着自动关闭。各控件页记录真实实现历史与检查结果。复杂真机/UI、设计器和平台运行检查记为后期待验收，不为这些检查先搭大型测试框架。

最终兼容验收仍要求：固定成员清单、继承关系、默认值/异常差分、程序与用户触发事件轨迹、运行时状态回读、平台实现、AOT运行、设计器序列化（适用时）、键盘与辅助技术、释放验证。指定范围附可复现证据后才记 🧪 验收通过并计入98%；规划文档完成只写在修改历史。

每次发布报告每控件属性/事件/方法/行为四项、各平台状态、剩余缺项。运行时未支持功能不得以空方法/吞异常伪装兼容。关键业务与生命周期阻塞必须为零；其余按 [98%规则](coverage.md)。

没有必要在此阶段引入插件后端系统、独立抽象包、每控件项目、分布式验收平台或全量自动UI录制系统。先在现有Lumina.Forms、Lumina.Tests和固定双目标Demo完成最小有效验证。
