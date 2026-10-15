# BindingSource

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/BindingSource.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · ⬜

未实现。它是 WinForms 业务数据层的关键非视觉组件；缺失会连带阻止 ListControl、DataGridView、BindingNavigator 与常见设计器代码平滑迁移。

- 标准继承/契约：`BindingSource : Component`，标准 IList/IBindingList/IBindingListView/ICancelAddNew/ITypedList/ISupportInitialize/ISupportInitializeNotification/ICurrencyManagerProvider 等接口。
- 代码证据：[Lumina.Forms 库目录](../../../Lumina.Forms/)（未找到本类型定义）。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `BindingSource-01` | 先补 Component/Site/Container/Disposed，再按基线逐一实现接口；DataSource=null、DataMember=空串、RaiseListChangedEvents=true、AllowNew 推导规则和初始化阶段固定样本。 |
| ⬜ | P0 | `BindingSource-02` | DataSource 为实例、Type、IList/IListSource、另一个 BindingSource、DataSet 成员等的解析；DataMember 合法/失效、循环绑定、空源初始列表元素类型与标准对照。 |
| ⬜ | P1 | `BindingSource-03` | List/Count/索引器、Add/Insert/Remove/RemoveAt/Clear/Contains/IndexOf/CopyTo；底层 IsReadOnly/IsFixedSize/AllowEdit/AllowRemove 反映，不复制数据造成双列表漂移。 |
| ⬜ | P1 | `BindingSource-04` | Position/Current、MoveFirst/MovePrevious/MoveNext/MoveLast、Find、CurrencyManager/GetRelatedCurrencyManager；空源 Position=-1、越界钳制、删除当前项、同值赋值。 |
| ⬜ | P1 | `BindingSource-05` | CurrentChanged/CurrentItemChanged/PositionChanged/ListChanged/DataSourceChanged/DataMemberChanged 事件先后及次数，源 ListChanged 与 INotifyPropertyChanged 的转发去重和线程归属。 |
| ⬜ | P1 | `BindingSource-06` | AddNew/AddingNew、EndEdit/CancelEdit/EndNew/CancelNew、ICancelAddNew/IEditableObject；新项构造失败、取消加入、已编辑对象移动位置的提交规则。 |
| ⬜ | P1 | `BindingSource-07` | ResetBindings(metadataChanged)/ResetCurrentItem/ResetItem、RaiseListChangedEvents=false 时批处理、ResetAllowNew；metadataChanged 的 PropertyDescriptorChanged/Reset 序列与标准一致。 |
| ⬜ | P2 | `BindingSource-08` | Sort/Filter、SupportsSorting/SupportsAdvancedSorting/SupportsFiltering、ApplySort/RemoveSort、SortDescriptions、Find/AddIndex/RemoveIndex；不支持的源按官方行为反馈，不静默伪排序。 |
| ⬜ | P2 | `BindingSource-09` | ITypedList.GetItemProperties/GetListName、DataError/BindingComplete、Initialized/IsInitialized、BeginInit/EndInit；主从源关系和初始化次序。 |
| ⬜ | P2 | `BindingSource-10` | 静态源类型访问器/构造器可由生成器提供；ITypedList/PropertyDescriptor 维持标准调用，DataSource=typeof(T) 的 AOT 构造可达性纳入诊断，不要求自创 Bind API。 |
| ⬜ | P2 | `BindingSource-11` | SuspendBinding/ResumeBinding/IsBindingSuspended、数据源替换和 Dispose 解绑源及 CurrencyManager；事件处理器重入、控件先释放与源先释放均验收。 |
| ⬜ | P3 | `BindingSource-12` | 三平台共用数据语义；同步源变化回 UI 线程由既有消息循环负责，不能把后台源的错误跨线程更新悄悄隐藏。AOT 真实 DataTable/POCO/主从表格用例。 |

## 三平台实现分析

### Windows

无原生窗口；托管列表与标准 CurrencyManager/PropertyDescriptor 逻辑，消费者控件负责 native 同步。无需为 BindingSource 引入 Win32 COM 绑定。

### Linux · GTK 4

复用相同托管实现，向 GtkListView/ColumnView 的 GListModel 桥接变更；GListModel 不是公开 BindingSource 的替代品。

### macOS · AppKit

复用相同托管实现，NSTableView/NSComboBox dataSource 读取；不用 Cocoa Bindings 强加 ObjC KVC/命名要求给既有 .NET 模型。

## 边界、AOT 与验收

AOT 的主要难点为运行时创建任意元素类型与字符串属性路径。优先编译期发现原有 DataSource 和描述符契约，保留动态 descriptor 扩展；无法推导的路径给出具体诊断和元数据保留指引，不能默默漏项。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.bindingsource?view=windowsdesktop-10.0
- https://learn.microsoft.com/dotnet/desktop/winforms/controls/bindingsource-component-overview
- https://docs.gtk.org/gtk4/class.ColumnView.html
- https://developer.apple.com/documentation/appkit/nstableviewdatasource
