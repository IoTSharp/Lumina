# DataGridViewSelectedColumnCollection

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/DataGridViewSelectedColumnCollection.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · ⬜

【M3】未发现 Lumina 对应实现。选择结果的只读列视图。

- 标准继承/契约：`System.Windows.Forms.DataGridViewSelectedColumnCollection : System.Windows.Forms.BaseCollection`。
- 代码证据：[Lumina.Forms 库目录](../../../Lumina.Forms/)（未找到本类型定义）。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `DataGridViewSelectedColumnCollection-01` | 核对构造可见性、基类、IList/ICollection/IEnumerable显式接口、IsReadOnly/IsFixedSize/SyncRoot；不能为只读结果集合新增公开构造或写方法。 |
| ⬜ | P1 | `DataGridViewSelectedColumnCollection-02` | Count/整数索引/Contains/CopyTo/IndexOf与IList；写操作是否允许严格按标准，不发明增删入口。 |
| ⬜ | P1 | `DataGridViewSelectedColumnCollection-03` | FullColumnSelect/ColumnHeaderSelect与其他SelectionMode的差异、DisplayIndex与结果顺序、隐藏/冻结列。 |
| ⬜ | P1 | `DataGridViewSelectedColumnCollection-04` | 列重排/删除/清选择后的SelectionChanged序列和CurrentCell保留规则。 |
| ⬜ | P1 | `DataGridViewSelectedColumnCollection-05` | 句柄前后增删或改变选择、未挂载/已挂载状态、null/无效索引/重复对象/复制数组不足的准确异常。 |
| ⬜ | P2 | `DataGridViewSelectedColumnCollection-06` | 枚举期间修改、批量更新/BeginInit-EndInit、排序/过滤/绑定Reset、处理器重入，托管集合和native实际视图一致。 |
| ⬜ | P2 | `DataGridViewSelectedColumnCollection-07` | 所有权、移除与Dispose区别；表格/行列释放后旧集合引用行为，不擅自释放用户共享菜单/字体/图像。 |
| ⬜ | P2 | `DataGridViewSelectedColumnCollection-08` | 设计器静态初始化与AOT，集合元素派生类型/Clone可达；不以运行时反射自动补全缺失类型。 |
| ⬜ | P3 | `DataGridViewSelectedColumnCollection-09` | GTK GListModel/NSTableView更新读取同一集合，原生可见索引不是永久模型索引；按三平台同一操作序列验收。 |

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

- https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewselectedcolumncollection?view=windowsdesktop-10.0
- https://docs.gtk.org/gtk4/class.ColumnView.html
- https://developer.apple.com/documentation/appkit/nstableview
