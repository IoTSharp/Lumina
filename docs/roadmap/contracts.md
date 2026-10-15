# 附属类型与命名空间任务

逐控件页之外，事件参数、枚举、接口、条目/列/单元格/集合、布局与设计器契约仍属于兼容范围。全量成员已经进入 [members.csv](api/members.csv)，接口另见 [interfaces.csv](api/interfaces.csv)。[ownership.csv](api/ownership.csv) 把所有基线类型映射到任务入口；自动归组是责任建议，不是已完成或平台豁免。

| ID | 顺序/状态 | 负责范围与细节 | 验收 |
| --- | --- | --- | --- |
| CNS-001 | P0 ⬜ | 事件参数/委托：准确EventHandler类型、泛型与命名委托区别；Cancel/Handled、旧新值、索引、鼠标键状态、CloseReason等；每个参数类型跟随所属控件任务 | 相同事件处理器源码编译、真实触发参数与顺序正确 |
| CNS-002 | P1 ⬜ | 枚举/Flags/常量：所有名称、数值、别名、组合、默认值、无效值异常；Keys/DialogResult/Anchor/Dock/AutoScale/DrawMode等逐项 | 不因同名枚举值个数大就稀释控件支持率 |
| CNS-003 | P1 ⬜ | 条目/集合：ControlCollection、TabPageCollection、ObjectCollection、TreeNodeCollection、ListView各集合、ToolStripItemCollection；构造/Owner/索引/排序/Clone/枚举/异常 | 见 [集合场景](behavior-catalog.md)，父子关系与原生状态同步 |
| CNS-004 | P2 ⬜ | DataGridView列/单元格/行/样式/事件参数/编辑接口及共享行：所有文本/按钮/勾选/组合/图像/链接类型，Template/Clone/EditingControl、绘制格式与排序 | 行列、六组cell/column、表头、样式和集合已补独立任务页；[索引](README.md)，精确成员由附件及CSV逐条追踪 |
| CNS-005 | P1/P2 ⬜ | 绑定及ComponentModel：IBindableComponent、BindingMemberInfo、BindingComplete、BindingManagerBase/PropertyManager/RelatedManager、IBindingList等；显式接口也必须支持 | [绑定任务](controls/Binding.md)、[上下文](controls/BindingContext.md)，AOT不靠未知动态代码生成 |
| CNS-006 | P3 ⬜ | System.Windows.Forms.VisualStyles：VisualStyleRenderer/Element/Information、TextRenderer、ControlPaint、绘制事件参数；Windows主题数据/GDI句柄与扩展点 | Windows完整基线保留；Linux/macOS逐成员判断，不能整个命名空间先全划平台豁免 |
| CNS-007 | P2/P3 ⬜ | System.Windows.Forms.Design运行时导出契约：ComponentEditorForm/Page、PropertyTab、IUIService/IWindowsFormsEditorService、属性特性与编辑站点 | 保留真实VS设计器往返；不承诺重做整个VS设计器宿主 |
| CNS-008 | P2 ⬜ | System.Windows.Forms.Layout：LayoutEngine/LayoutSettings和表格行列样式、Settings序列化；标准protected扩展与布局事务 | [布局基础](foundations.md)、Flow/TableLayout对照；不新发明公共Layout API |
| CNS-009 | P2/P3 ⬜ | Automation、AccessibleObject派生及相关UIA类型；Com2Interop/ActiveX类型、HtmlDocument/HtmlElement集合、DOM事件；各归所属控件 | 可访问性、OLE/浏览器能力各自验收；没有对应原生能力时公开边界 |
| CNS-010 | P2 ⬜ | Drawing.Printing：PrintDocument/PrintController/PreviewPrintController、PageSettings/PrinterSettings、PrintPage/QueryPage事件参数及所有集合/枚举 | [打印计划](controls/PrintDocument.md)，字体/Graphics身份、AOT与真实打印链路 |
| CNS-011 | P3 ⬜ | 旧DataGrid/ToolBar/StatusBar及样式/条目、ContextMenu/MainMenu/MenuItem；历史格式化/帮助/系统对象 | 历史/Obsolete项在清单保留；先对齐.NET10标准异常占位，[探针](../assessments/2026-10-15/legacy-control-probes.json)；恢复旧版UI另列可选扩展，不稀释活跃控件比例 |
| CNS-012 | P1/P2 ⬜ | 新版标准异步/命令等API：InvokeAsync/ShowAsync/ShowDialogAsync、ICommand及相关事件（以固定10.0.11实际清单为准）；资源/输入语言/屏幕/光标等辅助类型 | 不依赖印象筛掉“新版”成员；AOT、取消、线程和标准命名共同验证 |

## 每个附属类型的实际任务

所有类型都按其 Type+Member ID 单独跟踪：构造/继承/接口 → 公开属性/事件/方法/常量 → 默认值/异常 → 所属控件工作流 → 平台适用性 → AOT与释放。API中同一名字的不同重载不是一个任务。全类型映射中的“通用契约待审”必须人工复核后再实施，不能只完成独立文档页就宣布1095个类型完成。[补漏审计](scope-audit.md) 另核查非Control的交互对象，包括TaskDialog全族、旧Menu和溢出按钮。

不为每个EventArgs/枚举创建新项目或复制一套共性模板；精确签名放机器清单，行为归所属控件，降低维护成本。附属类型若成为独立迁移阻塞，再从控件页拆出独立文档。

## 修改历史

2026-10-15：本次新增全类型责任入口和验收要求，未新增上述缺失实现；各已有源文件历史以Git为准。公共类型已有历史名称不重命名，实施时优先标准WinForms契约。
