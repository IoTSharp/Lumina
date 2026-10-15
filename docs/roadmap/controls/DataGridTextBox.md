# DataGridTextBox

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/DataGridTextBox.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · ⬜

【M4】参考端.NET 10.0.11已实测该族多个具体类型构造抛PlatformNotSupportedException，Obsolete用于Framework二进制兼容；先逐签名/异常对齐，不能假定现代.NET还提供可工作的旧UI。旧DataGridTextBoxColumn的编辑器与DGV编辑器不同；现代构造占位先对齐。

- 标准继承/契约：`DataGridTextBox : TextBox`。
- 代码证据：[Lumina.Forms 库目录](../../../Lumina.Forms/)（未找到本类型定义）。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `DataGridTextBox-01` | 无参构造已实测PlatformNotSupportedException；TextBox继承、Obsolete、公开/受保护完整签名及准确异常时机。 |
| ⬜ | P1 | `DataGridTextBox-02` | IsInEditOrNavigateMode、SetDataGrid(DataGrid)和继承TextBox成员，逐一核查实际可达性/异常，不能返回无宿主的编辑器。 |
| ⬜ | P1 | `DataGridTextBox-03` | 键盘/焦点/消息预处理覆盖点与DataGridTextBoxColumn所需类型身份保持；静态外部派生编译/AOT可达。 |
| ⬜ | P1 | `DataGridTextBox-04` | 设计器消费和运行时PNSE分开记录，不通过未初始化对象/运行时代码生成来伪造支持。 |
| ⬜ | P2 | `DataGridTextBox-05` | 可选Framework扩展：IsInEditOrNavigateMode两种模式、F2/Tab/Enter/Esc/方向键与旧DataGrid导航，不直接沿用DGV事务。 |
| ⬜ | P2 | `DataGridTextBox-06` | 可选Framework扩展：SetDataGrid重复换宿主、选区/IME/ReadOnly/多行，KeyPress/TextChanged/Validating/Validated与CurrentCellChanged事件链。 |
| ⬜ | P2 | `DataGridTextBox-07` | 可选Framework扩展：格式解析失败/取消验证保留文本、DataGridTextBoxColumn复用、移除/重挂载/句柄重建和Dispose解绑。 |
| ⬜ | P3 | `DataGridTextBox-08` | 三平台复用TextBox原生后端的历史视觉样本独立取证；现代占位结果不能当作实际编辑通过。 |

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

- https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridtextbox?view=windowsdesktop-10.0
- https://learn.microsoft.com/dotnet/core/compatibility/winforms/5.0/removed-controls
