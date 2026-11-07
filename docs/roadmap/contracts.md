# 附属类型与命名空间任务

逐控件页之外，事件参数、枚举、接口、条目/列/单元格/集合、布局与设计器契约仍属于兼容范围。全量成员已经进入 [members.csv](api/members.csv)，接口另见 [interfaces.csv](api/interfaces.csv)。[ownership.csv](api/ownership.csv) 把所有基线类型映射到任务入口；自动归组是责任建议，不是已完成或平台豁免。

| ID | 顺序/状态 | 负责范围与细节 | 验收 |
| --- | --- | --- | --- |
| CNS-001 | P0 🟡 | 018～027既有事件契约保留；040补InputLanguageChanged/Changing两组参数/委托，保留Culture/InputLanguage构造、CharSet/SysCharSet和Cancel。其余准确委托、旧新值/索引/CloseReason及完整键盘契约仍待，动态封送AOT边界单列 | 相同事件处理器源码编译、真实触发参数与顺序正确 |
| CNS-002 | P1 🟡 | 001～027枚举范围保留；035修AutoSizeMode数值身份并保持命名默认，补齐DialogResult缺失五成员、IButtonControl及实际默认/取消消费。旧AutoSizeMode整数0/1和已编译常量需复核；Keys.Return等别名、其余Anchor/Dock/AutoScale/DrawMode和完整契约仍待 | 不因同名枚举值个数大就稀释控件支持率 |
| CNS-003 | P1 🟡 | 批次006～019的ListViewSubItem与ControlCollection/ArrangedElementCollection首范围保留；040补标准只读InputLanguageCollection的索引/Contains/IndexOf/CopyTo和快照。其他TabPage/Object/TreeNode/ListView/ToolStrip集合及完整Owner/并发/native仍缺 | [子项任务](controls/ListViewSubItem.md)、[集合场景](behavior-catalog.md)；局部集合不等于完整父子/原生同步 |
| CNS-004 | P2 ⬜ | DataGridView列/单元格/行/样式/事件参数/编辑接口及共享行：所有文本/按钮/勾选/组合/图像/链接类型，Template/Clone/EditingControl、绘制格式与排序 | 行列、六组cell/column、表头、样式和集合已补独立任务页；[索引](README.md)，精确成员由附件及CSV逐条追踪 |
| CNS-005 | P1/P2 ⬜ | 绑定及ComponentModel：IBindableComponent、BindingMemberInfo、BindingComplete、BindingManagerBase/PropertyManager/RelatedManager、IBindingList等；显式接口也必须支持 | [绑定任务](controls/Binding.md)、[上下文](controls/BindingContext.md)，AOT不靠未知动态代码生成 |
| CNS-006 | P3 🟡 | 053的006a补System.Windows.Forms.TextRenderer六个无DC测量重载及必要枚举/间距依赖；完整IDeviceContext测量、DrawText、ControlPaint、绘制事件参数和System.Windows.Forms.VisualStyles主题类型/句柄仍待。纠正原表把TextRenderer/ControlPaint归入VisualStyles的命名空间描述 | Windows完整基线保留；Linux/macOS逐成员判断，不能整个命名空间先全划平台豁免 |
| CNS-006a | P1 ✅ | 批次053：标准TextRenderer.MeasureText三个string及三个ReadOnlySpan无DC重载、完整TextFormatFlags值和必要Padding.Size；真实DrawTextExW/字体ceil与质量0/字形padding/约束/flags分离，初始系统DPI缓存只在成功SetHighDpiMode后刷新，自有HFONT/DC在finally释放。62例含独立DrawTextW字体/Unicode/布局对照通过。IDeviceContext/DrawText、整个TextRenderer、字体缓存优化、Designer/AOT/多DPI与跨平台不计完成 | 只关闭无DC测量必要子范围；为TextBoxBase-007b服务，不由API/测试数量推兼容率 |
| CNS-007 | P2/P3 ⬜ | System.Windows.Forms.Design运行时导出契约：ComponentEditorForm/Page、PropertyTab、IUIService/IWindowsFormsEditorService、属性特性与编辑站点 | 保留真实VS设计器往返；不承诺重做整个VS设计器宿主 |
| CNS-008 | P2 🟡 | 021新增标准Layout.LayoutEngine、Control默认引擎和基础扩展/事务协作；LayoutSettings、非Control布局对象、Flow/Table完整Settings/样式集合/序列化仍缺 | [布局基础](foundations.md)、[021](batches/021.md)；不新发明公共Layout API |
| CNS-009 | P2/P3 ⬜ | Automation、AccessibleObject派生及相关UIA类型；Com2Interop/ActiveX类型、HtmlDocument/HtmlElement集合、DOM事件；各归所属控件 | 可访问性、OLE/浏览器能力各自验收；没有对应原生能力时公开边界 |
| CNS-010 | P2 ⬜ | Drawing.Printing：PrintDocument/PrintController/PreviewPrintController、PageSettings/PrinterSettings、PrintPage/QueryPage事件参数及所有集合/枚举 | [打印计划](controls/PrintDocument.md)，字体/Graphics身份、AOT与真实打印链路 |
| CNS-011 | P3 ⬜ | 旧DataGrid/ToolBar/StatusBar及样式/条目、ContextMenu/MainMenu/MenuItem；历史格式化/帮助/系统对象 | 历史/Obsolete项在清单保留；先对齐.NET10标准异常占位，[探针](../assessments/2026-10-15/legacy-control-probes.json)；恢复旧版UI另列可选扩展，不稀释活跃控件比例 |
| CNS-012 | P1/P2 🟡 | 批次004～040明确范围保留；041补InputLanguage Current访问的OLE前置与Application.OleRequired线程缓存/显式退出配对。完整OLE宿主context、ShowAsync/ShowDialogAsync、ICommand/相关事件、完整退出竞态和其余资源/屏幕/光标辅助类型仍待 | 不依赖印象筛掉“新版”成员；既有AOT/取消/线程证据限明确首范围 |

## 每个附属类型的实际任务

所有类型都按其 Type+Member ID 单独跟踪：构造/继承/接口 → 公开属性/事件/方法/常量 → 默认值/异常 → 所属控件工作流 → 平台适用性 → AOT与释放。API中同一名字的不同重载不是一个任务。全类型映射中的“通用契约待审”必须人工复核后再实施，不能只完成独立文档页就宣布1095个类型完成。[补漏审计](scope-audit.md) 另核查非Control的交互对象，包括TaskDialog全族、旧Menu和溢出按钮。

不为每个EventArgs/枚举创建新项目或复制一套共性模板；精确签名放机器清单，行为归所属控件，降低维护成本。附属类型若成为独立迁移阻塞，再从控件页拆出独立文档。

## 修改历史

- 2026-11-07 · 批次053：CNS-006从全待办纠正为无DC测量子范围完成/完整父项部分，保留未完Drawing/VisualStyles任务；006a登记六重载及必要TextFormatFlags/Padding.Size/DPI辅助，控件页不重复计数。固定API库存与原ID/分母不改，见[053](batches/053.md)。

- 2026-11-07 · 批次041：CNS-012沿Application-11b/12a补OLE前置及最小owner退出配对；CNS-001/003的040事件/集合保持，未关闭任何完整父项。完整context/模态/输入法及平台仍待，固定源码其他失败HRESULT与MUI有界差异单列，见[041](batches/041.md)。

- 2026-11-07 · 批次040：CNS-001/003/012分别补标准输入语言事件参数/委托、只读集合和HKL服务，行为归Application-11a、Form-12d及Control-12m。所有父任务保持部分，OLE持久前置/完整输入法和平台验收不由新类型存在关闭，见[040](batches/040.md)。

- 2026-11-07 · 批次035：CNS-002同步AutoSizeMode修正和DialogResult/IButtonControl必要消费。原固定API10.0.11引用包缺失，新增面未重导库存，不以旧库存证明新API匹配；Keys.Return别名缺口由首次编译暴露并保留，见[035](batches/035.md)。

- 2026-10-16 · 批次027：CNS-001/002补MouseEventArgs.Location/可继承、HandledMouseEventArgs和XButton1/2及真实事件消费。错误委托强转的命名空间/Native AOT异常文本差异原样保留，完整契约和平台仍未关闭，见[027](batches/027.md)。

- 2026-10-16 · 批次024：CNS-002补ImageLayout与实际背景绘制消费，继承页面/API索引同步；五值存在不等于全部枚举或整个Control完成，见[024](batches/024.md)。

- 2026-10-16 · 批次023：CNS-001/002补标准绘制参数/委托、Message/CreateParams和ControlStyles明确首范围，未关闭所有附属契约；GetLParam(Type)/自定义Param动态封送与完整事件/平台仍保留，见[023](batches/023.md)。

- 2026-10-16 · 批次021：CNS-008由全待办纠正为LayoutEngine基础扩展已存在；LayoutSettings、非Control arranged对象与完整Flow/Table样式/序列化未关闭。标准默认base engine无几何策略，实际Control使用真实DefaultLayout；不将签名存在当完整布局验收。

- 2026-10-16 · 批次020：纠正CNS-001遗漏018 ControlEventArgs/Handler的全待办状态，并加入020 LayoutEventArgs/Handler明确范围；CNS-003补018标准父子集合。CNS-008的LayoutEngine/LayoutSettings仍待实现，不能把布局事件事务当完整布局命名空间；固定分母/历史ID均保留。

2026-10-15：本次新增全类型责任入口和验收要求，未新增上述缺失实现；各已有源文件历史以Git为准。公共类型已有历史名称不重命名，实施时优先标准WinForms契约。

2026-10-16 · 批次016历史回标：CNS-002补001的HighDpiMode与010的BoundsSpecified，CNS-003补006～012子项集合明确首范围，CNS-012补004/005标准InvokeAsync与007默认await恢复；从过时的全待办纠正为部分完成，其他枚举/集合/异步命令/完整所属/native及验收继续保留。仅整理已有实现和证据，不改变历史提交、任务ID或固定分母。
