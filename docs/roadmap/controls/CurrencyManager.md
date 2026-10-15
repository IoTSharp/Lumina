# CurrencyManager

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/CurrencyManager.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · ⬜

未实现；是列表位置、Current、编辑事务和控件绑定同步的基础，优先于完整列表控件和表格。

- 标准继承/契约：`CurrencyManager : BindingManagerBase`；由 BindingContext 等标准入口取得。
- 代码证据：[Lumina.Forms 库目录](../../../Lumina.Forms/)（未找到本类型定义）。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `CurrencyManager-01` | 按官方可见性补 CurrencyManager/BindingManagerBase：构造可见性、Count/Current/Position/List/Bindings/IsBindingSuspended、派生点；不能新增 public 构造改变生命周期契约。 |
| ⬜ | P1 | `CurrencyManager-02` | Position 在空列表/负数/超过末尾时的边界规则、同值设值；改变 Position、删除当前项、列表 Clear/Reset 后 Current/Count 的过渡。 |
| ⬜ | P1 | `CurrencyManager-03` | PositionChanged/CurrentChanged/CurrentItemChanged/ItemChanged/MetaDataChanged/ListChanged 的先后、次数和索引；列表项属性变化和当前项替换分别录制。 |
| ⬜ | P1 | `CurrencyManager-04` | AddNew/RemoveAt/EndCurrentEdit/CancelCurrentEdit、IEditableObject/ICancelAddNew；Position 变更是否提交、取消新项、验证拒绝后的定位保持。 |
| ⬜ | P1 | `CurrencyManager-05` | SuspendBinding/ResumeBinding、Refresh、PullData/PushData 受保护链和 BindingComplete/DataError 继承反馈；暂停不等同停止所有列表通知。 |
| ⬜ | P1 | `CurrencyManager-06` | IBindingList.ListChanged 的 ItemAdded/Deleted/Moved/Changed/Reset/PropertyDescriptorChanged；不支持通知的 IList 通过标准 Refresh 同步。 |
| ⬜ | P2 | `CurrencyManager-07` | GetItemProperties/GetListName/ITypedList、列表元素 PropertyDescriptor/Count 类型、相关 CurrencyManager 主从定位；DataTable/POCO/空类型源对照。 |
| ⬜ | P2 | `CurrencyManager-08` | 一个上下文多个绑定控件的当前项共享，重入 PositionChanged 中增删/重新定位；保留标准事件语义，避免无限回流。 |
| ⬜ | P2 | `CurrencyManager-09` | 源替换、绑定移除、容器释放，解绑全部 list/item 事件；正常释放后控件与源均可回收，不依赖最终器维持正确性。 |
| ⬜ | P3 | `CurrencyManager-10` | 三平台共用实现，真实双控件和表格 AOT 测试；仅发布成功不计管理器行为通过，事件日志与标准 WinForms 同场景对照。 |

## 三平台实现分析

### Windows

纯托管绑定状态和标准列表接口，无 native 窗口。Windows 控件通知只映射 Position/Current，编辑提交/验证由 manager 和 Binding 共同处理。

### Linux · GTK 4

同实现；GtkSelectionModel 与 manager 同步需有重入标记和明确来源，但不引入公共新状态机 API。模型变更保持 UI 线程规则。

### macOS · AppKit

同实现；NSTableView selection delegate 在 manager 许可后同步，切换行触发 end edit/验证顺序按兼容层实现。

## 边界、AOT 与验收

AOT 重点是 ITypedList/PropertyDescriptor 与当前元素元数据；可静态解析部分生成访问器，动态描述符直接调用既有接口。避免把 CurrencyManager 简化为 int Position 字段。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.currencymanager?view=windowsdesktop-10.0
- https://learn.microsoft.com/dotnet/api/system.windows.forms.bindingmanagerbase?view=windowsdesktop-10.0
- https://learn.microsoft.com/dotnet/api/system.windows.forms.currencymanager.position?view=windowsdesktop-10.0
