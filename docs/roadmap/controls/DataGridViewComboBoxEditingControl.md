# DataGridViewComboBoxEditingControl

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/DataGridViewComboBoxEditingControl.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · ⬜

未实现。其兼容目标是 DataGridView 的可重用编辑器，而非独立 ComboBox 外观；需要实现标准编辑器接口和表格事务。

- 标准继承/契约：`DataGridViewComboBoxEditingControl : ComboBox, IDataGridViewEditingControl`。
- 代码证据：[Lumina.Forms 库目录](../../../Lumina.Forms/)（未找到本类型定义）。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `DataGridViewComboBoxEditingControl-01` | 完成 ComboBox、DataGridViewComboBoxCell 和 IDataGridViewEditingControl 前置；公开构造、基类、接口成员签名与嵌入方式一致。 |
| ⬜ | P1 | `DataGridViewComboBoxEditingControl-02` | EditingControlDataGridView/EditingControlRowIndex/EditingControlValueChanged/EditingPanelCursor/RepositionEditingControlOnValueChange；复用一实例跨行编辑时旧宿主/旧行不残留。 |
| ⬜ | P1 | `DataGridViewComboBoxEditingControl-03` | EditingControlFormattedValue/GetEditingControlFormattedValue、DisplayMember/ValueMember、Value/FormattedValue 转换；绑定项不存在、null/DBNull 与 DataError 按标准反馈。 |
| ⬜ | P1 | `DataGridViewComboBoxEditingControl-04` | ApplyCellStyleToEditingControl(DataGridViewCellStyle)，字体/颜色/格式/RTL 及选区；样式只在编辑期生效，不改列默认值。 |
| ⬜ | P1 | `DataGridViewComboBoxEditingControl-05` | PrepareEditingControlForEdit(selectAll)；首次键入、F2、鼠标打开下拉，DropDownStyle 和当前项恢复，选中后 CurrentCellDirtyStateChanged/提交事件次数准确。 |
| ⬜ | P1 | `DataGridViewComboBoxEditingControl-06` | EditingControlWantsInputKey 与继承的 ProcessCmdKey/ProcessDialogKey：方向、Home/End、PageUp/PageDown、Enter/Esc、Tab；弹层打开时 ComboBox 接键，关闭后表格导航一致。 |
| ⬜ | P2 | `DataGridViewComboBoxEditingControl-07` | CellValidating 取消和 Esc 后关闭弹层、恢复旧选择/文本/数据值；重复选择同值不标脏，程序加载初值不标脏。 |
| ⬜ | P2 | `DataGridViewComboBoxEditingControl-08` | 绑定源 Reset/编辑行删除、列宽/DPI 改变、表格 Dispose 时的弹层/回调释放；同一编辑器复用不重复订阅。 |
| ⬜ | P3 | `DataGridViewComboBoxEditingControl-09` | 三平台嵌入原生编辑器，验证 popup/field editor 与表格焦点协调、IME 和辅助技术；两目标静态列设计器样本及 AOT。 |

## 三平台实现分析

### Windows

复用标准 COMBOBOX 后端，在 DataGridView 编辑 panel 内创建/重新定位；通知先更新编辑器脏值状态，由表格决定何时解析、验证和提交。

### Linux · GTK 4

复用 GtkDropDown 或 GtkEntry/Popover/ListView 组合；行工厂 rebind/unbind 时撤销旧 cell 订阅，不能因回收行 widget 把未验证值提交。

### macOS · AppKit

使用 NSComboBox 或 NSPopUpButton 编辑视图；NSTableView field editor/第一响应者切换需交给兼容表格事务，避免 Cocoa 自动写回绕过 CellValidating。

## 边界、AOT 与验收

事件状态机依赖 DataGridView；不能只实现 IDataGridViewEditingControl 属性存值而没有表格调用链。跨平台 GDI 单元格绘制边界沿用表格文档。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewcomboboxeditingcontrol?view=windowsdesktop-10.0
- https://learn.microsoft.com/dotnet/api/system.windows.forms.idatagridvieweditingcontrol?view=windowsdesktop-10.0
- https://docs.gtk.org/gtk4/class.DropDown.html
- https://developer.apple.com/documentation/appkit/nscombobox
