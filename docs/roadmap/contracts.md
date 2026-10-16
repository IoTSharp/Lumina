# 附属类型与命名空间任务

逐控件页之外，事件参数、枚举、接口、条目/列/单元格/集合、布局与设计器契约仍属于兼容范围。全量成员已经进入 [members.csv](api/members.csv)，接口另见 [interfaces.csv](api/interfaces.csv)。[ownership.csv](api/ownership.csv) 把所有基线类型映射到任务入口；自动归组是责任建议，不是已完成或平台豁免。

| ID | 顺序/状态 | 负责范围与细节 | 验收 |
| --- | --- | --- | --- |
| CNS-001 | P0 🟡 | 018已有ControlEventArgs/Handler，020补LayoutEventArgs/Handler，023补PaintEventArgs/Handler、InvalidateEventArgs/Handler及Message/CreateParams明确首范围。其他准确委托、Cancel/Handled、旧新值、索引、鼠标键状态/CloseReason等仍待所属控件任务；动态封送AOT边界单列 | 相同事件处理器源码编译、真实触发参数与顺序正确 |
| CNS-002 | P1 🟡 | 批次001已对齐HighDpiMode五值及14组DPI观察（f2d694c），010补BoundsSpecified八值/Flags（Control-02c），023补ControlStyles标准值及组合/未知位状态；其他Keys/DialogResult/Anchor/Dock/AutoScale/DrawMode等名称/数值/别名/组合/默认/无效值仍待逐项核对 | 不因同名枚举值个数大就稀释控件支持率 |
| CNS-003 | P1 🟡 | 批次006～012完成ListViewSubItem key与公共集合构造/共享view/整数索引/typed增删/非泛型IList/查询复制/枚举首范围（03a、02a～02f）；018另完成ControlCollection/ArrangedElementCollection父子集合首范围（Control-06a），019修复隐藏重挂通知；其他TabPage/Object/TreeNode/ListView/ToolStrip集合及完整Owner/并发/native仍缺 | [子项任务](controls/ListViewSubItem.md)、[集合场景](behavior-catalog.md)；局部集合不等于完整父子/原生同步 |
| CNS-004 | P2 ⬜ | DataGridView列/单元格/行/样式/事件参数/编辑接口及共享行：所有文本/按钮/勾选/组合/图像/链接类型，Template/Clone/EditingControl、绘制格式与排序 | 行列、六组cell/column、表头、样式和集合已补独立任务页；[索引](README.md)，精确成员由附件及CSV逐条追踪 |
| CNS-005 | P1/P2 ⬜ | 绑定及ComponentModel：IBindableComponent、BindingMemberInfo、BindingComplete、BindingManagerBase/PropertyManager/RelatedManager、IBindingList等；显式接口也必须支持 | [绑定任务](controls/Binding.md)、[上下文](controls/BindingContext.md)，AOT不靠未知动态代码生成 |
| CNS-006 | P3 ⬜ | System.Windows.Forms.VisualStyles：VisualStyleRenderer/Element/Information、TextRenderer、ControlPaint、绘制事件参数；Windows主题数据/GDI句柄与扩展点 | Windows完整基线保留；Linux/macOS逐成员判断，不能整个命名空间先全划平台豁免 |
| CNS-007 | P2/P3 ⬜ | System.Windows.Forms.Design运行时导出契约：ComponentEditorForm/Page、PropertyTab、IUIService/IWindowsFormsEditorService、属性特性与编辑站点 | 保留真实VS设计器往返；不承诺重做整个VS设计器宿主 |
| CNS-008 | P2 🟡 | 021新增标准Layout.LayoutEngine、Control默认引擎和基础扩展/事务协作；LayoutSettings、非Control布局对象、Flow/Table完整Settings/样式集合/序列化仍缺 | [布局基础](foundations.md)、[021](batches/021.md)；不新发明公共Layout API |
| CNS-009 | P2/P3 ⬜ | Automation、AccessibleObject派生及相关UIA类型；Com2Interop/ActiveX类型、HtmlDocument/HtmlElement集合、DOM事件；各归所属控件 | 可访问性、OLE/浏览器能力各自验收；没有对应原生能力时公开边界 |
| CNS-010 | P2 ⬜ | Drawing.Printing：PrintDocument/PrintController/PreviewPrintController、PageSettings/PrinterSettings、PrintPage/QueryPage事件参数及所有集合/枚举 | [打印计划](controls/PrintDocument.md)，字体/Graphics身份、AOT与真实打印链路 |
| CNS-011 | P3 ⬜ | 旧DataGrid/ToolBar/StatusBar及样式/条目、ContextMenu/MainMenu/MenuItem；历史格式化/帮助/系统对象 | 历史/Obsolete项在清单保留；先对齐.NET10标准异常占位，[探针](../assessments/2026-10-15/legacy-control-probes.json)；恢复旧版UI另列可选扩展，不稀释活跃控件比例 |
| CNS-012 | P1/P2 🟡 | 批次004/005完成标准Action/Func/ValueTask InvokeAsync首范围（cb3692d/b5aad529，Control-05a/b），007补默认callback await恢复（db29dce，context-01b）；ShowAsync/ShowDialogAsync、ICommand/相关事件、完整退出竞态及资源/输入语言/屏幕/光标辅助类型仍待实现 | 不依赖印象筛掉“新版”成员；既有AOT/取消/线程证据限明确首范围 |

## 每个附属类型的实际任务

所有类型都按其 Type+Member ID 单独跟踪：构造/继承/接口 → 公开属性/事件/方法/常量 → 默认值/异常 → 所属控件工作流 → 平台适用性 → AOT与释放。API中同一名字的不同重载不是一个任务。全类型映射中的“通用契约待审”必须人工复核后再实施，不能只完成独立文档页就宣布1095个类型完成。[补漏审计](scope-audit.md) 另核查非Control的交互对象，包括TaskDialog全族、旧Menu和溢出按钮。

不为每个EventArgs/枚举创建新项目或复制一套共性模板；精确签名放机器清单，行为归所属控件，降低维护成本。附属类型若成为独立迁移阻塞，再从控件页拆出独立文档。

## 修改历史

- 2026-10-16 · 批次023：CNS-001/002补标准绘制参数/委托、Message/CreateParams和ControlStyles明确首范围，未关闭所有附属契约；GetLParam(Type)/自定义Param动态封送与完整事件/平台仍保留，见[023](batches/023.md)。

- 2026-10-16 · 批次021：CNS-008由全待办纠正为LayoutEngine基础扩展已存在；LayoutSettings、非Control arranged对象与完整Flow/Table样式/序列化未关闭。标准默认base engine无几何策略，实际Control使用真实DefaultLayout；不将签名存在当完整布局验收。

- 2026-10-16 · 批次020：纠正CNS-001遗漏018 ControlEventArgs/Handler的全待办状态，并加入020 LayoutEventArgs/Handler明确范围；CNS-003补018标准父子集合。CNS-008的LayoutEngine/LayoutSettings仍待实现，不能把布局事件事务当完整布局命名空间；固定分母/历史ID均保留。

2026-10-15：本次新增全类型责任入口和验收要求，未新增上述缺失实现；各已有源文件历史以Git为准。公共类型已有历史名称不重命名，实施时优先标准WinForms契约。

2026-10-16 · 批次016历史回标：CNS-002补001的HighDpiMode与010的BoundsSpecified，CNS-003补006～012子项集合明确首范围，CNS-012补004/005标准InvokeAsync与007默认await恢复；从过时的全待办纠正为部分完成，其他枚举/集合/异步命令/完整所属/native及验收继续保留。仅整理已有实现和证据，不改变历史提交、任务ID或固定分母。
