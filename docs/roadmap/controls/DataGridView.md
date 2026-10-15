# DataGridView

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/DataGridView.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · ⬜

未实现；这是业务型 WinForms 平滑替换的最大工作项之一。Windows 的 DataGridView 本身是托管表格控件，不存在一个同名 Win32 公共控件可直接包装；GTK ColumnView 与 AppKit NSTableView 也不是其完整等价实现。

- 标准继承/契约：`DataGridView : Control`；依赖 DataGridViewRow/Column/Cell/HeaderCell/CellStyle/各派生单元格、集合、IDataGridViewEditingControl、事件参数及枚举。
- 代码证据：[Lumina.Forms 库目录](../../../Lumina.Forms/)（未找到本类型定义）。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `DataGridView-01` | 固定完整类型族清单：TextBox/CheckBox/ComboBox/Button/Image/Link 列与单元格、行列头、Selected* 集合、EditingControl；每类和每重载进入 API 差分，不能仅创建 DataGridView 空宿主。 |
| ⬜ | P0 | `DataGridView-02` | 先打通 BindingSource/BindingContext/CurrencyManager/Binding 的静态 AOT 契约、Control 消息/焦点/布局、公共图形与可访问性；发布最小只读表格样本确认平台方案可成立。 |
| ⬜ | P1 | `DataGridView-03` | Rows/Columns 的 Add/Insert/Remove/Clear/索引/Name、Owner、Clone、RowTemplate、SharedRow、行共享限制；未绑定和绑定模式下合法/非法变更与标准一致。 |
| ⬜ | P1 | `DataGridView-04` | 默认 AutoGenerateColumns=true、AllowUserToAddRows=true、AllowUserToDeleteRows=true、AllowUserToResizeColumns=true、AllowUserToResizeRows=true、ReadOnly=false、MultiSelect=true、SelectionMode=RowHeaderSelect、EditMode=EditOnKeystrokeOrF2；NewRowIndex/IsNewRow/空源场景。 |
| ⬜ | P1 | `DataGridView-05` | DataSource/DataMember、DataPropertyName、DataBindingComplete、自动列生成；IList/IListSource/IBindingList/ITypedList 与 DataTable/DataView，Reset/新增/删除/属性通知时不丢列身份和当前项。 |
| ⬜ | P1 | `DataGridView-06` | CurrentCell/CurrentRow、SelectedCells/Rows/Columns、ClearSelection/SelectAll；CellEnter/CellLeave、RowEnter/RowLeave、CurrentCellChanged、SelectionChanged 的顺序；空单元格、隐藏列、冻结列、删除当前行均有对照。 |
| ⬜ | P1 | `DataGridView-07` | BeginEdit/EndEdit/CancelEdit/CommitEdit/IsCurrentCellDirty/IsCurrentCellInEditMode；CellBeginEdit（可取消）→ EditingControlShowing → CellParsing/CellValidating（可取消）→ CellEndEdit 等具体分支逐路径录制，不预设单一全局事件序列。 |
| ⬜ | P1 | `DataGridView-08` | CellValidating/CellValidated、RowValidating/RowValidated、DataError、CellErrorTextNeeded/RowErrorTextNeeded、ShowCellErrors/ShowRowErrors；取消验证后焦点/编辑值/数据源值一致，不吞异常冒充成功。 |
| ⬜ | P1 | `DataGridView-09` | 新增/删除事务：DefaultValuesNeeded、UserAddedRow、UserDeletingRow（可取消）、UserDeletedRow、RowValidating（可取消）；IEditableObject 与 ICancelAddNew、DataBoundItem、Esc 取消新行，删除当前项前后位置一致。 |
| ⬜ | P1 | `DataGridView-10` | 六种标准列/单元格的 Value/FormattedValue/ValueType/ReadOnly/Style/InheritedStyle；CheckBox 二/三态、ComboBox DisplayMember/ValueMember/DataSource、Button/Link 激活、Image 空值与错误图标；不把所有单元格退化成文本。 |
| ⬜ | P1 | `DataGridView-11` | 键盘 Tab/Shift+Tab/Enter/Esc/F2/Ctrl+C/方向/Page/Home/End、ProcessDataGridViewKey/ProcessDialogKey 和 EditingControlWantsInputKey；编辑器 IME、选区、焦点与表格导航互不抢键。 |
| ⬜ | P2 | `DataGridView-12` | AutoSizeColumnsMode/AutoSizeRowsMode、AutoSizeMode、FillWeight/MinimumWidth、AutoResizeColumn/AutoResizeColumns/AutoResizeRow/AutoResizeRows、列头/行头、Frozen/Visible/DisplayIndex、滚动条与 FirstDisplayedScrollingRowIndex/FirstDisplayedScrollingColumnIndex；DPI/RTL/大字体/可变行高。 |
| ⬜ | P2 | `DataGridView-13` | ColumnHeaderMouseClick/CellClick/CellContentClick/CellDoubleClick、CellMouseClick/CellMouseDoubleClick/CellMouseDown/CellMouseUp/CellMouseMove/CellMouseEnter/CellMouseLeave、ColumnWidthChanged/ColumnDisplayIndexChanged/RowHeightChanged、Scroll；坐标/HitTest/内容区命中与取消列重排有明确场景。 |
| ⬜ | P2 | `DataGridView-14` | Sort/SortCompare/Sorted、SortMode/SortGlyphDirection、IBindingList 排序；视觉顺序和 Rows 索引/数据源顺序差异、当前项保留、null/DBNull/文化比较。 |
| ⬜ | P2 | `DataGridView-15` | VirtualMode、RowCount/ColumnCount、CellValueNeeded/CellValuePushed、NewRowNeeded、RowDirtyStateNeeded、CancelRowEdit、RowValidated；10万行滚动、排序、编辑、取消不把全部数据实体化。 |
| ⬜ | P2 | `DataGridView-16` | DefaultCellStyle/AlternatingRowsDefaultCellStyle/RowsDefaultCellStyle/ColumnHeadersDefaultCellStyle、CellFormatting、CellPainting、RowPrePaint/RowPostPaint；样式继承优先级、格式/NullValue/DataSourceNullValue 与颜色/字体资源生命周期。 |
| ⬜ | P2 | `DataGridView-17` | ClipboardCopyMode/GetClipboardContent、CellToolTipTextNeeded、CellContextMenuStripNeeded/RowContextMenuStripNeeded、拖放、可访问性网格/表/单元格模式与读屏；新行/错误/只读状态要可获知。 |
| ⬜ | P2 | `DataGridView-18` | SuspendLayout/BeginInit/EndInit、列设计器序列化、RowTemplate 与 Clone 扩展点、句柄重建、绑定源替换及 Dispose；不得让通用反射克隆绕过 AOT。 |
| ⬜ | P3 | `DataGridView-19` | GTK/AppKit 各做“只读/编辑/验证取消/虚拟/冻结/多单元格选区”六个实证原型；缺其中核心能力时不能宣布该平台完整表格可行；以明确缺口决定投入，不先建通用渲染框架。 |
| ⬜ | P3 | `DataGridView-20` | 三平台按公开API与行为分类分别计算 ≥98%；编辑、绑定、选择、虚拟化、生命周期等核心族不能靠大量简单属性稀释。Windows 全量承诺必须给自绘事件与自定义单元格扩展验收。 |

## 三平台实现分析

### Windows

需要在 Lumina.Forms 内实现 WinForms 表格的托管行列/编辑/绑定/事件逻辑，Windows 宿主可使用自有 Win32 窗口和 GDI/系统主题；编辑单元格嵌入 EDIT、COMBOBOX 等原生子窗。SysListView32 无任意单元格编辑、完整验证事务、行共享/虚拟推送等契约，不能拿它包装后称为完整 DataGridView。优先参考微软 MIT 源码拆解行为，逐模块消除 AOT 不支持的动态路径。

### Linux · GTK 4

GtkColumnView + GListModel/SelectionModel + GtkEntry/GtkCheckButton/GtkDropDown/GtkButton/GtkImage 等工厂产生的真实原生单元格；原生行虚拟化可复用。GtkColumnView 的选择以行模型为核心，不能直接提供 DataGridView 的任意单元格矩形选区、冻结行列、行头、新行事务或 CellPainting。需要可控原生组合与托管状态；若要满足所有绘图扩展，纯原生控件要求与完全 WinForms 行为存在边界，必须列为 🚫/未达，不能隐藏。

### macOS · AppKit

NSTableView + NSTableColumn + NSView-based cells（NSTextField、NSButton、NSPopUpButton、NSImageView）可提供原生只读与编辑表格。WinForms 的单元格矩形多选、行头、新行、虚拟推送、冻结区域、合并式自定义绘制事件并非 NSTableView 默认能力；通常需原生子视图组合加托管状态，并研究 table view 重用、field editor 生命周期。不得声称换成 NSTableView 就完成 DataGridView。

## 边界、AOT 与验收

最大风险是“完全原生控件 + 任意 WinForms 自绘/扩展完全等价”三者不能无条件同时满足。Windows 98% 与两平台可移植 98% 的分母需公开，核心表格行为不得事后划出可移植范围来刷比例。AOT 绑定通过既有 PropertyDescriptor/IBindingList/ITypedList 接口及编译期已知类型生成访问器，保持 DataSource/DataPropertyName 等公开写法。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridview?view=windowsdesktop-10.0
- https://learn.microsoft.com/dotnet/desktop/winforms/controls/datagridview-control-overview-windows-forms
- https://github.com/dotnet/winforms/tree/v10.0.11/src/System.Windows.Forms/System/Windows/Forms/Controls/DataGridView
- https://docs.gtk.org/gtk4/class.ColumnView.html
- https://developer.apple.com/documentation/appkit/nstableview
