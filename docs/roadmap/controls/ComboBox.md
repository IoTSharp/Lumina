# ComboBox

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/ComboBox.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · 🟡

已有真实 `COMBOBOX`、Items 添加/清空、SelectedIndex/SelectedItem 和 `CBN_SELCHANGE` 接线；仍直接继承 Control，缺 ListControl 的绑定、格式化契约。`DropDownStyle=DropDownList`、`DropDownHeight=240` 与 WinForms 默认不符；Items 类型也采用了非标准 `ItemCollection`，且缺删除/插入/排序。

- 标准继承/契约：`ComboBox : ListControl : Control`；标准条目集合为 `ComboBox.ObjectCollection`。
- 代码证据：[当前源码](../../../Lumina.Forms/ComboBox.cs)。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `ComboBox-01` | 恢复 ListControl 继承及 ComboBox.ObjectCollection；核对所有构造、可见性、重载、异常类型；已有 AddItem/ClearItems 不再作为迁移入口，标准 Items 操作全部可用。 |
| ⬜ | P0 | `ComboBox-02` | 默认值对照：DropDownStyle=DropDown、SelectedIndex=-1、MaxDropDownItems=8、DropDownHeight=106、IntegralHeight=true、Sorted=false、DrawMode=Normal；构造和 Reset 后一致。 |
| ⬜ | P1 | `ComboBox-03` | Items.Add/AddRange/Insert/Remove/RemoveAt/Clear、索引写入、Contains/IndexOf、重复值与 null 参数规则；在句柄前后执行，原生计数、字符串、选择均一致；绑定后修改 Items 的限制一致。 |
| ⬜ | P1 | `ComboBox-04` | SelectedIndex/SelectedItem/SelectedValue/Text/SelectedText/SelectionStart/SelectionLength 双向同步；记录程序设值、鼠标、方向键三条路径的 SelectedIndexChanged、SelectedValueChanged、TextChanged、SelectionChangeCommitted 顺序；相同值不重复发出。 |
| ⬜ | P1 | `ComboBox-05` | DropDownStyle 三模式、DroppedDown、DropDown/DropDownClosed、DropDownWidth/Height、MaxDropDownItems、IntegralHeight 与运行时切换；Esc 取消、Enter 提交、Alt+Down 开启，焦点不丢失。 |
| ⬜ | P1 | `ComboBox-06` | 绑定继承任务：DataSource/DisplayMember/ValueMember/FormattingEnabled/FormatString/FormatInfo 与 Format；源列表增删、INotifyPropertyChanged、CurrencyManager 定位同步，不靠 ToString 代替成员解析。 |
| ⬜ | P2 | `ComboBox-07` | AutoCompleteMode/AutoCompleteSource/AutoCompleteCustomSource、FindString/FindStringExact、GetItemText、MaxLength；核对大小写、文化、输入法组合输入与不存在的 SelectedItem 行为。 |
| ⬜ | P2 | `ComboBox-08` | DrawMode、DrawItem、MeasureItem、ItemHeight、GetItemHeight、BeginUpdate/EndUpdate；变高项、DPI、RTL、字体变化、空列表和滚动位置均有差分场景。 |
| ⬜ | P2 | `ComboBox-09` | 绑定/自动完成/原生通知触发重入、Items 排序后的索引、句柄重建保留 Text/selection；Dispose 释放编辑子窗、列表子窗回调和绑定订阅。 |
| ⬜ | P3 | `ComboBox-10` | GTK 与 AppKit 分别补齐三种样式与选择事件顺序；OwnerDraw 的 GDI DrawItemEventArgs 单列图形边界，严禁无行为空实现记为通过。 |

## 三平台实现分析

### Windows

继续使用 COMBOBOX。`CB_ADDSTRING/INSERTSTRING/DELETESTRING/RESETCONTENT/SETCURSEL/GETCURSEL` 管理集合；`CBN_SELCHANGE/EDITCHANGE/SELENDOK/DROPDOWN/CLOSEUP` 进入统一状态和事件逻辑；编辑区域通过 COMBOBOXINFO 取得子 HWND，维护选择、输入法与自动完成。属性变化必要时重建句柄并还原状态，不能仅写自动属性。

### Linux · GTK 4

DropDownList 可用 GtkDropDown + GListModel + GtkSingleSelection；可编辑 DropDown 需要 GtkEntry、GtkPopover 与 GtkListView 原生组合，Simple 需要常显列表。GtkComboBox/ComboBoxText 自 4.10 废弃，不作为新后端主方案。GTK 的搜索弹窗不是 WinForms 自动完成，需适配输入与承诺选择语义；不能把 GtkDropDown 搜索当作可编辑 Text。

### macOS · AppKit

可编辑模式使用 NSComboBox 的数据源与 delegate，DropDownList 使用 NSPopUpButton；Simple 组合 NSTextField + NSScrollView/NSTableView。NSComboBox 的完成和 selection 通知需转为 WinForms 事件，保持用户提交与程序设值区别。运行时换模式需保持焦点、Text 和绑定位置。

## 边界、AOT 与验收

依赖 ListControl、BindingContext/CurrencyManager 和输入法基础。DisplayMember/ValueMember 为字符串的标准契约必须保留：优先从静态可解析的 DataSource 类型生成访问器，动态模型允许显式保留的 PropertyDescriptor；不能强迫用户改用仓库自创绑定 API。Windows 的 HWND 子窗和 GDI owner draw 不能原样映射，另列为专属或适配限制。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- `108ef2b` · 2026-04-08：接入 Win32 深色模式。
- `2e8431f` · 2026-04-01：主题和 Demo UI 改进；不代表全部列表行为已完成。
- `ce86ab5` · 2026-03-31：接入公共主题、颜色和窗口实时刷新能力。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.combobox?view=windowsdesktop-10.0
- https://docs.gtk.org/gtk4/class.DropDown.html
- https://docs.gtk.org/gtk4/class.ComboBoxText.html
- https://developer.apple.com/documentation/appkit/nscombobox
- https://developer.apple.com/documentation/appkit/nspopupbutton
