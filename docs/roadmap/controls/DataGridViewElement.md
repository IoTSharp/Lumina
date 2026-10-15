# DataGridViewElement

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/DataGridViewElement.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · ⬜

【M3】未发现 Lumina 对应实现。它是行、列和单元格共同基类；未完成此层会使所有附属对象的所属表格、状态与事件路由不一致。

- 标准继承/契约：`System.Windows.Forms.DataGridViewElement : System.Object`。
- 代码证据：[Lumina.Forms 库目录](../../../Lumina.Forms/)（未找到本类型定义）。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `DataGridViewElement-01` | 按完整类型核对构造可见性、DataGridView/State 和 protected OnDataGridViewChanged；不能让单元格和行列各自保存互相矛盾的宿主。 |
| ⬜ | P1 | `DataGridViewElement-02` | 元素未挂载→加入→移除→重挂载时 DataGridView/State 的确切过渡与异常；同一元素不得同时属于两个表格。 |
| ⬜ | P1 | `DataGridViewElement-03` | DataGridViewElementStates 的 Displayed/Frozen/ReadOnly/Resizable/Selected/Visible 等标志由派生对象正确组合，默认与运行时变化分别对照。 |
| ⬜ | P1 | `DataGridViewElement-04` | OnDataGridViewChanged 在宿主变更时的次数/先后，派生覆盖调用base的兼容行为；回调内移除元素不留下旧宿主引用。 |
| ⬜ | P1 | `DataGridViewElement-05` | protected RaiseCellClick/RaiseCellContentClick/RaiseCellContentDoubleClick 的参数与目标行列索引；禁止对已移除元素发事件。 |
| ⬜ | P1 | `DataGridViewElement-06` | RaiseCellValueChanged/RaiseDataError/RaiseMouseWheel 路由到标准表格事件；错误上下文和异常不丢失、不重复发送。 |
| ⬜ | P2 | `DataGridViewElement-07` | 列、行、Cell派生扩展共同跑宿主和状态场景；受保护扩展不能只做到类内编译，外部自定义派生也须消费验证。 |
| ⬜ | P2 | `DataGridViewElement-08` | 句柄重建与native行复用不重建业务对象；移除表格、关闭窗口后解绑通知，保留标准IDisposable所在层而不加Release API。 |
| ⬜ | P3 | `DataGridViewElement-09` | 三平台共用元素状态；原生signal/delegate经同一托管路由，事件线程和索引与Windows基线一致。 |

## 三平台实现分析

### Windows

复用 DataGridView 内部托管行列/事务/布局模型；Windows用受控Win32宿主、系统主题/GDI绘制和原生EDIT/COMBOBOX编辑器。不存在与该对象等价的独立Win32窗口类，不另建公开表格框架。

### Linux · GTK 4

对象模型继续使用同一公开WinForms契约；GtkColumnView/GListModel与原生单元格工厂显示状态。行选择模型不能直接代替任意单元格多选/冻结/验证事务，需逐场景补足；GDI自绘扩展明确平台边界。

### macOS · AppKit

对象模型继续共用；NSTableView/NSTableColumn及NSView-based cell承载。View重用与field editor生命周期不能改变托管行列/单元格身份；任意单元格选择、冻结区与绘制扩展没有默认完整等价实现。

## 边界、AOT 与验收

依赖 DataGridView、绑定与公共绘图/无障碍。不要每个类型各建引擎；现有标准类的继承与扩展点需要保留。AOT通过静态可发现的列/单元格类型及标准Clone、EditType、PropertyDescriptor契约，避免运行时生成IL；未知自定义派生类型给出明确裁剪可达性要求。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewelement?view=windowsdesktop-10.0
- https://docs.gtk.org/gtk4/class.ColumnView.html
- https://developer.apple.com/documentation/appkit/nstableview
