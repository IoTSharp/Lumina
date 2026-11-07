# LuminaForms 路线图

更新：2026-11-07。目标是 **.NET 10 WinForms 平滑替换 + Native AOT**；包与命名空间保持 `Lumina.Forms`。

当前批次[058](docs/roadmap/batches/058.md)完成RichTextBox-001a标准直接继承/真实系统RICHEDIT50W及007a必要构造默认、扩展限长与原生选项。最终96新增/5,664全量通过，固定双目标Release0错/2既有NU1510；首构建消息类型错误及首94/96两项夹具失败保留。Rich Text仍沿WM_SETTEXT，其扩限差异与专有StreamIn/长度/选区、RTF/格式留原任务；Masked模型、完整缩放/线程/模态、Designer/AOT/设备/平台/98%仍待。下述057及以前为历史。

当前批次[057](docs/roadmap/batches/057.md)完成TextBoxBase-002d有窗原生TextLength/无窗虚Text分支和003h必要默认事件/绑定、选区元数据与Paint转发。41新增/5,568全量首轮通过，Release固定双目标0错/2既有NU1510；旧测试未改。真实派生层级/模型/后端优先继续，完整基类/绑定/Designer/AOT/可见绘制/物理输入/平台/98%仍待。下述056及以前为历史。

当前批次[056](docs/roadmap/batches/056.md)完成TextBoxBase-003f构造四样式/专用左键点击链和003g八项必要绘图/间距成员声明转发。46新增/5,527全量首轮通过，Release固定双目标0错/2既有NU1510。独立WindowFromPoint均no-hit，只验证负向路由，正向可见点击/物理输入仍待；完整003、其余元数据/缩放、真实Rich/Masked后端与Designer/AOT/平台/98%不关闭。下述055及以前为历史。

历史批次[055](docs/roadmap/batches/055.md)完成TextBox-004b滚动条首选测量及必要线程感知SM2/3度量、TextBoxBase-003e创建请求/前置主题标志。68新增/5,481全量通过，Release固定双目标0错/2既有NU1510。首67/68的一例ReadOnly/IME/PasswordChar建窗夹具错误已纠正并保留回执，生产未为失败改变。完整缩放/点击/样式、真实Rich/Masked后端与Designer/AOT/平台/98%仍待。下述054及以前为历史。

历史批次[054](docs/roadmap/batches/054.md)完成TextBoxBase-007c标准PreferredHeight/100宽DefaultSize及必要独立控件DPI度量、007d独立AutoSize/请求高度生命周期、007e首选缓存与必要Table/Flow内部状态消费。默认AutoSize=true，取消旧24/48原生下限；最终84新增/5,413全量通过，Release固定双目标0错/2既有NU1510。首轮全量6失败含真实Table消费者缺口及5个旧固定夹具默认假设，原证据保留、原硬断言不降低。完整缩放/跨屏DPI/字体重缩放、派生真实后端、Designer/AOT/平台/98%仍待。下述053及以前为历史。

当前批次[053](docs/roadmap/batches/053.md)完成CNS-006a六个无DC标准TextRenderer.MeasureText重载及必要TextFormatFlags/Padding.Size/DPI快照依赖、TextBoxBase-007b真实首选尺寸计算和008c普通EDIT未建窗延期滚动。108新增/5,329全量通过，Release固定双目标0错/2既有NU1510。首构建缺Padding.Size、首104/105与第二107/108夹具/诊断失败均保留；独立EDIT隐藏首行0、可见未聚焦/聚焦均37，Lumina可见聚焦37。生产未为测试失败改变。完整AutoSize/PreferredHeight/DefaultSize/缓存生命周期、Rich/Masked后端与平台/98%仍待。下述052及以前为历史。

当前批次[052](docs/roadmap/batches/052.md)完成TextBoxBase-008a行查询与008b字符位置查询六个标准成员，直接消费真实EDIT消息并保留虚调用/异常/句柄/16位坐标契约。102新增/5,221全量通过，固定双目标Release0错/2既有NU1510；首93/102九个夹具轨迹错误和原TRX保留，生产未为此改动。完整008、AutoSize/PreferredHeight/DefaultSize/GetPreferredSize、真实派生后端和平台验收仍待。下述051及以前为历史。

当前批次[051](docs/roadmap/batches/051.md)完成TextBoxBase-005f固定词边界辅助、009a启用Ctrl+Backspace/Ctrl+Shift+Backspace可撤销词删除，以及必要Control-14c内部redraw嵌套计数。最终88新增/5,119全量通过，固定双目标Release0错/2既有NU1510；首次85/88的三处无窗TextLength调用轨迹夹具错误及原TRX保留，生产未为此改动。未访问系统剪贴板或菜单；实际绘制/物理输入、完整编辑/测量/default、真实派生后端和平台门禁仍待。下述050及以前为历史。

历史批次[050](docs/roadmap/batches/050.md)完成TextBoxBase-005e的Copy/Cut/Paste标准命令入口及003d的ShortcutsEnabled精确禁用表、只读对齐键和原生系统菜单抑制。77新增/5,031全量通过，固定双目标Release0错/2既有NU1510。剪贴板消息全部在测试中截获，未读写系统剪贴板；自定义菜单只核受限路由，实际显示未验。当时启用词删除缺口由051的009a补限定范围，完整编辑/测量/default、派生后端和平台门禁继续原任务。

当前批次[049](docs/roadmap/batches/049.md)完成Control-17e原停车通知接收者寿命/来源HWND反射及TextBoxBase-005d标准Clear虚Text=null。独立原生EDIT证实SetParent后通知仍投创建父；18新增与8旧用例组合26/26、完整4,954/4,954，Release固定双目标0错/2既有NU1510。首跑11/13的两个真实重挂失败保留；048“重挂即销毁停车窗”断言按固定反射寿命纠正，旧事件硬期望保持。原反射父Form寿命/完整反射、剪贴板/快捷键、测量及派生后端和平台门禁仍待；下述048及以前为历史。

当前批次[048](docs/roadmap/batches/048.md)完成TextBoxBase-005b标准AppendText、005c公开CanUndo/Undo/ClearUndo及必要Control-17d停车期间WM_COMMAND转发。新增43与旧行为3组合46/46、全量4,936/4,936，Release固定双目标0错/2既有NU1510。首次夹具37/38、旧行为全量4,930/4,931及停车/重挂诊断失败均保留。重挂后EDIT命令缺口原因未闭环，下一批优先继续Control-17/TextBoxBase-006；剪贴板/Clear完整语义、测量/派生后端及平台门禁仍待，下述047及以前为历史。

当前批次[047](docs/roadmap/batches/047.md)完成TextBoxBase-002b标准SelectedText原生替换、002c的Lines空值/CR/LF/CRLF及必要003c的MaxLength。65新增和4,893全量通过，Release固定双目标0错误/2既有NU1510；首轮62/65的两处AllowNull反射位置和受限Undo夹具错误及独立EDIT对照均保留。完整AppendText/公开撤销/剪贴板、测量、真实派生后端及模态/设备/Designer/AOT/平台/98%仍待；下述046及以前为历史。

当前批次[046](docs/roadmap/batches/046.md)完成TextBoxBase-002a的SelectAll/DeselectAll、007a标准BorderStyle/通知/创建样式，以及Control-17c按当前源类身份缓存superclass。新增46例与旧对齐组合74/74，最终全量4,828/4,828，Release固定双目标0错误/2既有NU1510。首次夹具、源类缓存/activation环境失败和主夹具清理中止均保留。完整SelectedText/Lines/撤销/剪贴板、尺寸、真实派生后端及模态/设备/Designer/AOT/平台/98%仍待；下述045及以前为历史。

当前批次[045](docs/roadmap/batches/045.md)完成TextBoxBase-005a标准Modified状态/通知、EN_UPDATE同步及程序设文的最小WindowText衔接，以及TextBox-007b标准CharacterCasing设置/重建/原生大小写样式。最终45新增、892相关、4,782全量通过，Release固定双目标0错误/2既有NU1510；首次43/45的两例父窗口夹具失败保留。BorderStyle/尺寸、选区编辑/撤销、运行时RTL/镜像、真实Rich/Masked后端及模态/设备/Designer/AOT/平台/98%仍待；下述044及以前为历史。

当前批次[044](docs/roadmap/batches/044.md)完成TextBoxBase-003b标准HideSelection设置/通知/真实样式与TextBox-007a标准TextAlign设置/通知/创建对齐及逻辑Left水平条条件。HorizontalAlignment按固定10.0.11纠正为Left0/Right1/Center2，旧二进制/整数存储需复核。最终42新增、2,356相关、4,737全量通过；首次38/42的4个RTL夹具失败原始回执保留。Release固定双目标0错误/2既有NU1510。完整运行时RTL/镜像视觉、共同编辑/尺寸/派生后端及模态/设备/Designer/AOT/平台/98%仍待；下述043及以前为历史。

当前批次[043](docs/roadmap/batches/043.md)完成TextBoxBase-003a的AcceptsTab标准通知、WordWrap真实折行/重建/条件布局及TextBox-004a的ScrollBars校验/原生样式/重建。新增41/41、相关359/359、全量4,695/4,695通过，Release固定双目标0错误/2既有NU1510。None不再强制纵条、关闭WordWrap实际关闭折行；完整对齐/RTL/首选尺寸、共享编辑及派生后端/模态/设备/Designer/AOT/平台/98%仍待。下述042及以前为历史。

当前批次[042](docs/roadmap/batches/042.md)建立标准TextBoxBase共享EDIT首范围及TextBox直接继承，保留原有编辑/重建/密码规则；IME组合/候选通知经固定源码审计后保持原生默认路由。新增19/19、相关318/318、全量4,654/4,654通过，Release固定双目标0错误/2既有NU1510。完整基类、RichTextBox/MaskedTextBox直接继承与真实后端、模态/设备/Designer/AOT/平台/98%仍待。固定API清单保持原分母，历史类型缺失快照与当前源码分别记录；下述041及以前是历史。

当前批次[041](docs/roadmap/batches/041.md)沿040补Application-11b输入语言OLE前置与Application-12a最小线程初始化/退出配对。完整OLE宿主context、模态/组合串/候选窗、TextBoxBase、AutoSize及全部平台验收仍待；其他失败HRESULT显式抛出、MUI最多一次扩容的差异单列，不以局部线程状态称完整InputLanguage兼容。下述040及以前为历史。

当前批次[040](docs/roadmap/batches/040.md)完成Application-11a真实线程HKL服务、Form-12d标准语言事件/参数及Control-12m后代通知/请求取消。新增27/27、IME/焦点组合96/96、全量4,617/4,617，Release双目标0错误/2既有NU1510。固定源码OLE持久前置副作用未实现；模态/组合输入/候选窗、TextBoxBase、AutoSize及设备/Designer/AOT/平台/98%仍待。下述039及以前为历史。

当前批次[039](docs/roadmap/batches/039.md)完成TextBox-005a只读/密码的受限IME与必要真实密码同步、Control-12l语言消息及非Form宿主传播首范围。22新增/4,590全量通过，既有焦点组合50/50，Release双目标0错误/2既有NU1510。Form语言事件/Application输入语言服务、模态宿主、完整组合输入/候选窗、TextBoxBase、AutoSize及设备/Designer/AOT/平台/98%仍待。下述038及以前为历史。

当前批次[038](docs/roadmap/batches/038.md)完成Control-12k有限ImeMode/IMM上下文、TextBox-003c多行切换与选择恢复、UserControl-02d标准AutoSize声明。32新增/4,568全量通过，Release双目标0错/2既有NU1510。完整IME/派生限制、TextBoxBase、AutoSize、模态、ButtonBase、关闭焦点/退出、设备/Designer/AOT/平台和98%仍未完成。下述037及以前为历史证据。

当前批次[037](docs/roadmap/batches/037.md)完成Control-12j有限IME字符消息、TextBox-003b输入键/对话框路由、UserControl-05a原生焦点入口及嵌套验证导航。新增22/22、全量4,536/4,536、Release双目标0错误/2条既有NU1510。033～036范围保留；完整IME组合态/context/候选窗、TextBoxBase、AutoSize、ButtonBase、模态、AOT、Designer、设备和平台/98%仍待。下述036为历史快照。

当前批次[036](docs/roadmap/batches/036.md)完成Control-12i原生输入键/字符判别与队列字符抑制、Form-12c默认按钮托管释放清理，并完成Button-002b原生点击验证前置源码审计与真实HWND回归；Button.cs无需算法改动。新增9/9、全量4,514/4,514测试通过，Release双目标构建0错误/2条既有NU1510。035范围保留。固定10.0.11 API库存缺引用包仍NOT_RUN；完整IME、全部派生输入、模态、ButtonBase、AutoSize、设备、Designer、AOT及平台仍未关闭，不宣称完整控件或98%。

实施遵循根目录 [项目目标、需求与实施原则](PROJECT_REQUIREMENTS.md)。Drawing 已确定为 **Windows 两个目标都用官方 System.Drawing.Common，Linux 使用自有 Lumina.Drawing.Common 包并保留 System.Drawing 源码消费方式**；优先验证同名程序集 System.Drawing.Common，通过 XPL-002 后冻结完整身份与发布方案。按目标 OS/RID 选择完整依赖图，保留原生控件、Designer 资源和 AOT；先做实际必需范围与 Linux 原型，不承诺先重写全套 Drawing。身份、资源与平台运行须有独立证据，不能把需求定案当作实现完成。

**Windows：每控件属性、事件、行为分别 ≥98%；Linux/macOS：可移植部分分别 ≥98%。** 方法、接口和派生扩展另行验收，Windows 专属项明确列出。当前尚未达到该指标。

[逐控件任务与历史](docs/roadmap/README.md) · [遗漏审计](docs/roadmap/scope-audit.md) · [98%计量](docs/roadmap/coverage.md) · [执行排序](docs/roadmap/execution.md) · [三平台方案](docs/roadmap/platforms.md) · [项目整合](docs/roadmap/project-consolidation.md)

**001～057的明确子范围代码完成，完整父任务保持部分。** [当前记录](HANDOFF.md) · [057结果](docs/roadmap/batches/057.md) · [完整任务索引](docs/roadmap/tasks.csv)。029的停止记录已被2026-10-26新的明确用户授权覆盖；完整Control、Windows全面验收及Linux/macOS仍未完成。✅代码完成与🧪兼容验收分开，未完父项/固定分母/任务ID保留。每批按真实代码/必要检查同步索引，不以签名/基类/局部探针或测试数量推出整控件完成/98%。

| 顺序 | 工作 | 状态 | 任务入口 |
| --- | --- | --- | --- |
| 0 | 固定 .NET 10.0.11 类型、成员、继承与接口清单 | ✅ 清单已生成；运行验收仍待办 | [基线](docs/roadmap/api/baseline.json)、[逐成员](docs/roadmap/api/members.csv)、[接口](docs/roadmap/api/interfaces.csv) |
| 1 · M0 | UI线程、ListViewItem构造、启动签名、完整Demo AOT主窗、裁剪警告 | 🟡 线程/基础构造/签名及Form标准链已实现；完整主窗/模态/警告仍未闭环 | [基础阻塞](docs/roadmap/foundations.md) |
| 2 · M1 | 标准继承、生命周期、父子/集合、布局、焦点、验证、模态、DPI | 🟡 基础Control构造/边界/通知/释放及Parent/Controls/Form链首范围已实现；标准句柄、布局事务及基础默认布局/首选尺寸已实现明确范围，完整派生布局/继承属性/验证/模态仍缺 | [公共基础](docs/roadmap/foundations.md)、[Form](docs/roadmap/controls/Form.md) |
| 3 · M2 | 逐项补齐已有控件、原生双向同步、输入、菜单、TaskDialog与公共对话框 | 🟡 已有局部实现 | [每控件独立任务](docs/roadmap/README.md)、[行为场景](docs/roadmap/behavior-catalog.md) |
| 4 · M3 | 绑定、DataGridView、业务组件、真实应用迁移 | ⬜ | [执行顺序](docs/roadmap/execution.md)、[DataGridView](docs/roadmap/controls/DataGridView.md) |
| 5 · M4 | 高级控件、历史占位契约、Printing、派生绘制、可访问性、COM/MDI边界 | ⬜ | [完整索引](docs/roadmap/README.md)、[附属类型](docs/roadmap/contracts.md) |
| 6 · 平台原型 | 先解Font/Image/Graphics公共契约，再验证GTK4/AppKit原生控件与AOT | 🔎 | [平台进入条件](docs/roadmap/platforms.md) |
| 7 · M5 | 按相同控件顺序扩展Linux/macOS，并逐平台达到门槛 | ⬜ | 各控件Windows/Linux/macOS任务 |
| 仓库整合 | 移除WPF适配器；美化/注入/效果展示隔离到效果解决方案 | ✅ 本轮已执行 | [整合记录](docs/roadmap/project-consolidation.md) |

`NativeFormsDemo` 固定双目标：`net10.0-windows` 用真实WinForms并保留VS设计器；`net10.0` 用Lumina.Forms并保持AOT。静态UI仍由 `.Designer.cs` / `.resx` 管理。

批次029完成Control-12c/12d自动焦点转移与验证事件、ContainerControl-02b/03a移除候选及程序验证状态机、Form-07a关闭验证前置，并补Panel/ScrollableControl/TabControl必要批量验证消费。4,392项测试（新增49）全通过，双目标Demo Release 0错误/339警告。16场景全部正常退出/无HWND残留；最终固定参考1,832行、JIT1,880行、AOT1,847行：JIT对参考1/16严格相同，其余为15个额外WM_QUIT及11场景关闭LostFocus；JIT/AOT仅5/16严格相同，11场景相差关闭LostFocus的33行，未定位根因，严格失败保留。旧Focus157/父子集合391/生命周期204行参考JIT全同；028仍18/30同参考，保留11个WM_QUIT及SplitContainer投影差异。探针/Demo AOT为3,065,344/4,297,216字节，CLR目录0，Demo实际显示/复选框0→1/WM_CLOSE退出及原HWND失效通过。全部派生、点击提交取消、键盘/IME、物理设备、Designer/资源及GTK4/AppKit仍未完成，不宣称整Control或98%。 详情见[029](docs/roadmap/batches/029.md)及[验证记录](docs/roadmap/validation.md)。029后停止，未安排030。

批次030完成Control-12e/FND-019基础键消息与事件契约首范围；批次031完成Control-11c/FND-013 RightToLeft托管与Win32样式、ScrollableControl-02a～06a托管滚动首范围、ContainerControl-05a/06a、UserControl-01a/02a；批次032完成Panel-01b/02a、UserControl-03a和ScrollableControl-08a Windows滚动消息首范围；批次033完成Control-12f/FND-019键盘预处理首范围、ContainerControl-06b Tab/助记键后代路由和ScrollableControl-07a已有偏移下的聚焦滚动修正。4,439项全线测试、Forms双目标Release构建通过。完整命令/对话框协作、KeyPreview、完整Tab/助记键/IME、真实物理滚轮/触摸板、Cursor、完整缩放/设计器、Native AOT和跨平台仍未完成，不宣称完整基础链或98%。详情见[033](docs/roadmap/batches/033.md)。
批次034/035继续补键事件父预览、嵌套导航、实际消息循环和默认取消按钮验证首范围；AutoSizeMode改为GrowAndShrink=0/GrowOnly=1，基础Control显式保持GrowOnly。旧二进制常量或按整数持久化的0/1需复核，按名字源码重编译保留对应名字语义。完整输入、模态、AutoSize和平台仍待。

批次036完成Control-12i原生 `WM_GETDLGCODE` 输入键/字符判别、`WM_SYSCHAR` 对话字符分流、队列字符消息有界移除及验证取消状态递归/鼠标抬起清理；Form-12c在托管Dispose时清理默认/接受/取消按钮引用并撤销默认通知；Button-002b完成固定源码审计和真实HWND点击验证回归，确认Button.cs的BN_CLICKED路径无需重复注入验证。新增9项、全量4,514项通过，Release双目标构建0错误/2条既有NU1510。完整IME、ButtonBase、模态、设备、Designer、Native AOT、平台与98%仍未完成，详情见[036](docs/roadmap/batches/036.md)。

首轮90天先做M0/M1及高频迁移阻塞，再依据真实样本重估；不把完整Windows98%和两个原生后端承诺在90天完成。详细排序与估算在[执行计划](docs/roadmap/execution.md)。

[项目价值与重新评估](docs/zh/luminaforms-reassessment-2026-10.md) · [首轮审计](docs/assessments/2026-10-15/README.md) · [此前路线记录](docs/assessments/2026-10-15/roadmap-before-reassessment.md)
