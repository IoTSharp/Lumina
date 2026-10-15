# ListControl

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/ListControl.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · ⬜

未实现。ComboBox/ListBox 均直接继承 Control，导致绑定、格式化、SelectedValue 等共用能力缺位；这是两个控件大量差异的共同根因。

- 标准继承/契约：`ListControl : Control`，抽象基类。
- 代码证据：[Lumina.Forms 库目录](../../../Lumina.Forms/)（未找到本类型定义）。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `ListControl-01` | 补齐标准抽象继承契约及 SelectedIndex、RefreshItem、RefreshItems、SetItemsCore、SetItemCore 等派生实现点；自定义派生控件能按 WinForms 方式编译。 |
| ⬜ | P0 | `ListControl-02` | DataSource=null、DisplayMember/ValueMember/FormatString=空串、FormattingEnabled=false、FormatInfo=null 默认值与数据成员错误处理；绑定前后设置顺序均有样本。 |
| ⬜ | P1 | `ListControl-03` | DataSource 接受 IList/IListSource 与合法成员路径；DisplayMember/ValueMember 的 BindingMemberInfo 路径解析、源切换、同值重设；不新增 ListControl.DataMember；释放旧 ListChanged/PropertyChanged 订阅。 |
| ⬜ | P1 | `ListControl-04` | SelectedValue/SelectedIndex 与 CurrencyManager.Position 双向同步，空源/位置 -1/删除当前项/Reset 的选择恢复；不把数值索引当 ValueMember。 |
| ⬜ | P1 | `ListControl-05` | DataSourceChanged、DisplayMemberChanged、ValueMemberChanged、SelectedValueChanged 的先后、重复赋值和异常回滚与标准实测一致。 |
| ⬜ | P1 | `ListControl-06` | GetItemText、FilterItemOnProperty、Format 事件及 ListControlConvertEventArgs；FormattingEnabled/FormatString/FormatInfo 的优先级、null/DBNull、文化转换有对照。 |
| ⬜ | P2 | `ListControl-07` | BindingContextChanged、ParentChanged、句柄重建后重新绑定；IsHandleCreated=false 时保留托管状态，重新挂载不重复订阅。 |
| ⬜ | P2 | `ListControl-08` | DataSource 阻止 Items 非法修改、列表排序/筛选刷新后的 selected item 保留规则，派生 ComboBox 与 ListBox 共跑同组场景。 |
| ⬜ | P2 | `ListControl-09` | 生成器解析既有 DisplayMember/ValueMember/Binding 字符串与静态类型；ITypedList/ICustomTypeDescriptor 作为独立能力验证；AOT 不生成 IL、不使用 DynamicMethod。 |
| ⬜ | P3 | `ListControl-10` | 三平台共用列表状态和格式转换；原生模型只消费结果且通知回流不循环。每平台发布 AOT 绑定样本，并验证源移除后能被 GC 回收。 |

## 三平台实现分析

### Windows

ListControl 无独立 HWND 类；托管实现绑定、格式化和位置管理，派生类连接 COMBOBOX/LISTBOX。优先建立一个正确的公共实现，不能各控件复制绑定规则。

### Linux · GTK 4

无 GtkListControl 对应类。托管列表/绑定状态连接 GListModel，使用 items-changed 及选择模型； GTK 数据模型不能取代 WinForms CurrencyManager 契约。

### macOS · AppKit

无 NSListControl 对应类。托管状态通过 NSComboBoxDataSource/NSTableViewDataSource 提供数据并在主线程刷新；保留 WinForms 事件和转换规则，不将 Cocoa Bindings 作为公开迁移要求。

## 边界、AOT 与验收

这是已有 WinForms 抽象类型的恢复，不是新增公共抽象。AOT 对“仅能运行时获知任意类型的属性路径”有真实边界：列出无法静态发现项及保留元数据的方案，避免静默空白。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.listcontrol?view=windowsdesktop-10.0
- https://docs.gtk.org/gtk4/iface.SelectionModel.html
- https://docs.gtk.org/gtk4/class.SingleSelection.html
- https://developer.apple.com/documentation/appkit/nscomboboxdatasource
