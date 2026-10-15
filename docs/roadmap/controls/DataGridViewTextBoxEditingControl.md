# DataGridViewTextBoxEditingControl

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/DataGridViewTextBoxEditingControl.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · ⬜

未实现。现有 TextBox 可作为基础，但表格编辑器必须加入 IDataGridViewEditingControl 与可取消提交、复用和导航。

- 标准继承/契约：`DataGridViewTextBoxEditingControl : TextBox, IDataGridViewEditingControl`。
- 代码证据：[Lumina.Forms 库目录](../../../Lumina.Forms/)（未找到本类型定义）。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `DataGridViewTextBoxEditingControl-01` | 完成 TextBox、DataGridViewTextBoxCell 和标准接口；构造/继承/重载签名与表格编辑器创建契约一致。 |
| ⬜ | P1 | `DataGridViewTextBoxEditingControl-02` | EditingControlDataGridView/RowIndex/ValueChanged、FormattedValue/GetEditingControlFormattedValue、EditingPanelCursor/RepositionEditingControlOnValueChange；复用时所有状态重置。 |
| ⬜ | P1 | `DataGridViewTextBoxEditingControl-03` | ApplyCellStyleToEditingControl、TextAlign/Multiline/WordWrap/MaxLength、字体/前景/背景/RTL；按单元格继承样式设置且布局变化后裁剪正确。 |
| ⬜ | P1 | `DataGridViewTextBoxEditingControl-04` | PrepareEditingControlForEdit(selectAll)、首次字符替换、F2 保留文本、SelectionStart/SelectionLength；加载原值不触发伪脏标记。 |
| ⬜ | P1 | `DataGridViewTextBoxEditingControl-05` | EditingControlWantsInputKey：左右在文本边界才交回表格，Home/End、Ctrl+组合键、Enter/Esc/Tab 按多行模式分支；IME 组合字符不得当导航。 |
| ⬜ | P1 | `DataGridViewTextBoxEditingControl-06` | TextChanged → 编辑器脏标志 → CellParsing/CellValidating → CellValueChanged 的实际路径差分；验证取消保留编辑文本，Esc 回滚数据值。 |
| ⬜ | P2 | `DataGridViewTextBoxEditingControl-07` | NullValue/DataSourceNullValue/Format/FormatProvider、数字/日期解析错误、DataError，空字符串与 null 区别；粘贴和撤销行为一致。 |
| ⬜ | P2 | `DataGridViewTextBoxEditingControl-08` | 行/列隐藏删除、绑定 Reset、表格 Resize/DPI、滚动离开编辑单元格、Dispose；解绑事件且不提交到已移除行。 |
| ⬜ | P3 | `DataGridViewTextBoxEditingControl-09` | EDIT、GtkEntry/GtkTextView、NSTextField/NSTextView 各平台同组编辑器/表格键盘仲裁样本，AOT 发布后验证 IME、剪贴板和多行。 |

## 三平台实现分析

### Windows

复用 EDIT 子窗口，单行/多行样式按 CellStyle 与 WrapMode 决定；WM_GETDLGCODE/键盘消息参与表格仲裁，EN_CHANGE 只标脏，提交由 DataGridView 驱动。

### Linux · GTK 4

单行 GtkEntry、多行 GtkTextView；控制 signal 与 factory unbind 生命周期，文本已改变但验证失败时保留 native buffer，不能因行回收丢编辑事务。

### macOS · AppKit

NSTextField 与共享 field editor，或多行 NSTextView；NSTextInputClient 输入法/第一响应者与兼容导航协调；不要依靠默认 endEditing 自动提交。

## 边界、AOT 与验收

AOT 不依赖动态生成编辑器代理；静态注册/可解析列类型和标准 CreateControlInstance/编辑器类型契约需保留其可支持范围。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewtextboxeditingcontrol?view=windowsdesktop-10.0
- https://learn.microsoft.com/dotnet/api/system.windows.forms.idatagridvieweditingcontrol?view=windowsdesktop-10.0
- https://docs.gtk.org/gtk4/class.Entry.html
- https://developer.apple.com/documentation/appkit/nstextfield
