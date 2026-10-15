# PropertyManager

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/PropertyManager.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · ⬜

【M1】未发现 Lumina 对应实现。BindingContext针对单一对象属性的标准管理器；不能把它当只有一行的CurrencyManager别名。

- 标准继承/契约：`System.Windows.Forms.PropertyManager : System.Windows.Forms.BindingManagerBase`。
- 代码证据：[Lumina.Forms 库目录](../../../Lumina.Forms/)（未找到本类型定义）。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `PropertyManager-01` | 精确BindingManagerBase继承、构造可见性；由BindingContext标准索引器取得，不新增公共new PropertyManager模型入口。 |
| ⬜ | P1 | `PropertyManager-02` | Count/Current/Position对非列表对象的实际值和Position设置的合法/非法规则，null/嵌套对象替换。 |
| ⬜ | P1 | `PropertyManager-03` | OnCurrentChanged/OnCurrentItemChanged与PropertyChanged/目标属性通知，多个绑定在同一对象上的同步和事件次数。 |
| ⬜ | P1 | `PropertyManager-04` | EndCurrentEdit/CancelCurrentEdit、IEditableObject和目标Validating取消，提交/回滚的正确触发时机。 |
| ⬜ | P1 | `PropertyManager-05` | AddNew/RemoveAt在对象管理器中不支持的标准异常；不能提供与CurrencyManager相同的伪实现。 |
| ⬜ | P2 | `PropertyManager-06` | SuspendBinding/ResumeBinding/UpdateIsBinding、BindingManagerBase.PullData/PushData失败反馈，暂停不等于从此不跟踪对象。 |
| ⬜ | P2 | `PropertyManager-07` | GetListName和PropertyDescriptor解析、嵌套属性链、ICustomTypeDescriptor、TypeConverter AOT契约。 |
| ⬜ | P2 | `PropertyManager-08` | 对象/BindingContext更换、源事件重入、控件Dispose后解绑；共享对象不被管理器释放。 |
| ⬜ | P3 | `PropertyManager-09` | 三平台单对象Text/Checked/Value绑定样本，与列表绑定分开验收，不将CurrencyManager测试直接标绿。 |

## 三平台实现分析

### Windows

无独立Win32窗口；托管实现标准Binding/PropertyDescriptor/绑定管理器协议。Control和ToolStripItem的属性变更/验证进入同一绑定层，不能在每种控件另造数据绑定。

### Linux · GTK 4

同一托管契约；GTK原生signal只同步标准属性和当前位置，GListModel/SelectionModel不是公开绑定协议的替代。

### macOS · AppKit

同一托管契约；AppKit target/action/delegate更新标准属性，不把Cocoa Bindings/KVC约束强加给原有.NET模型。

## 边界、AOT 与验收

AOT保留既有Binding/DataBindings/PropertyDescriptor/ITypedList/ICustomTypeDescriptor写法；生成器优先解析静态类型及常量路径，不能要求用户换自创Schema/Bind API。未知动态类型明确元数据可达性要求，不吞错显示空值。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.propertymanager?view=windowsdesktop-10.0
- https://docs.gtk.org/gtk4/class.ColumnView.html
- https://developer.apple.com/documentation/appkit/nstableview
