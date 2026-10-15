# DataGridTableStyle

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/DataGridTableStyle.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · ⬜

【M4】未发现 Lumina 对应实现。参考端.NET 10.0.11已实测该族多个具体类型构造抛PlatformNotSupportedException，Obsolete用于Framework二进制兼容；先逐签名/异常对齐，不能假定现代.NET还提供可工作的旧UI。旧表样式对象关联列集合和关系导航，现代构造占位不应触发造第二套表格。

- 标准继承/契约：`System.Windows.Forms.DataGridTableStyle : System.ComponentModel.Component`。
- 代码证据：[Lumina.Forms 库目录](../../../Lumina.Forms/)（未找到本类型定义）。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `DataGridTableStyle-01` | 无参构造PNSE已实测；补bool及CurrencyManager构造、Component继承/Obsolete与DefaultTableStyle静态字段的类型初始化/访问结果。 |
| ⬜ | P1 | `DataGridTableStyle-02` | MappingName/DataGrid/GridColumnStyles、ReadOnly/AllowSorting、BeginEdit/EndEdit与全部事件准确签名，不能以DataGridViewCellStyle替代。 |
| ⬜ | P1 | `DataGridTableStyle-03` | AlternatingBackColor/BackColor/ForeColor/GridLineColor/GridLineStyle、HeaderBackColor/HeaderForeColor/HeaderFont/LinkColor/LinkHoverColor/Selection*形状与Reset*/ShouldSerialize*入口。 |
| ⬜ | P1 | `DataGridTableStyle-04` | ColumnHeadersVisible/RowHeadersVisible/RowHeaderWidth/PreferredColumnWidth/PreferredRowHeight及对应Changed/On*Changed派生点，逐成员核验异常。 |
| ⬜ | P2 | `DataGridTableStyle-05` | CreateGridColumn(PropertyDescriptor[,isDefault])扩展、默认列选择与可见性；AOT不以Activator盲建未知列类型。 |
| ⬜ | P2 | `DataGridTableStyle-06` | 可选Framework扩展：MappingName表/关系匹配、列集合所属和重复映射，源schema改变后样式选择和事件次序。 |
| ⬜ | P2 | `DataGridTableStyle-07` | 可选Framework扩展：BeginEdit/EndEdit(shouldAbort)、只读/验证失败及CurrencyManager提交，表样式移除后退出编辑。 |
| ⬜ | P2 | `DataGridTableStyle-08` | 可选Framework扩展：默认样式/本地值优先级、颜色/行高影响量测和绘制，资源与Site/Dispose所有权。 |
| ⬜ | P3 | `DataGridTableStyle-09` | 三平台占位及可选视觉分开验收，不将属性存值直接算成表样式生效。 |

## 三平台实现分析

### Windows

M4先实现现代.NET公开形状、Obsolete和实际异常，不为构造即PNSE的基线新建原生窗口。仅在另行接受Framework历史视觉扩展时，复用现有DataGridView/ToolStrip/StatusStrip内部能力加Win32原生编辑器，保留旧公开契约，不新增第二套公开框架。

### Linux · GTK 4

现代.NET占位契约可由同一托管实现表达，无需Gtk控件。可工作的Framework旧UI属于可选扩展：表格以GtkColumnView和原生编辑器组合，工具条/状态栏以GtkButton/GtkLabel等原生部件组合；GDI扩展不自动等价。

### macOS · AppKit

现代.NET占位契约无独立AppKit视图。可选Framework视觉扩展复用NSTableView、NSTextField/NSButton及原生工具条/状态内容视图，旧式逐像素绘制与Cocoa原生行为差异须单列，不能直接计成.NET 10兼容。

## 边界、AOT 与验收

依据[完整参考端探针](../../assessments/2026-10-15/legacy-control-probes.json)。仅无参构造已实测PNSE，不把其他重载/受保护成员猜成同一异常；M0/M4逐一核查后补证据。Framework可工作视觉行为明确为可选扩展，排在现代DataGridView、绑定、ToolStrip/StatusStrip之后，不是.NET 10首发98%的额外强制UI成本；占位通过项单独报告，不能稀释常用控件指标。AOT不通过未初始化对象/运行时发射绕过构造。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridtablestyle?view=windowsdesktop-10.0
- https://docs.gtk.org/gtk4/class.ColumnView.html
- https://developer.apple.com/documentation/appkit/nstableview
