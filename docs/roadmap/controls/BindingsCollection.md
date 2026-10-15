# BindingsCollection

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/BindingsCollection.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · ⬜

【M1】未发现 Lumina 对应实现。管理器与控件绑定集合的共同标准基础，包含变更前/后事件与派生钩子。

- 标准继承/契约：`System.Windows.Forms.BindingsCollection : System.Windows.Forms.BaseCollection`。
- 代码证据：[Lumina.Forms 库目录](../../../Lumina.Forms/)（未找到本类型定义）。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `BindingsCollection-01` | BaseCollection继承、构造/Count/整数索引及受保护List；Add/AddCore/RemoveCore等实际可见性，不把受保护方法误作新的公共API。 |
| ⬜ | P1 | `BindingsCollection-02` | Add/Remove/RemoveAt/Clear通过对应Core钩子维护Binding所属manager/组件；相同Binding重复加入/跨宿主的标准异常。 |
| ⬜ | P1 | `BindingsCollection-03` | CollectionChanging在变更前、CollectionChanged在变更后；Action/Element、Clear和批量路径及事件处理重入顺序。 |
| ⬜ | P1 | `BindingsCollection-04` | 无效索引、null、非所属Binding、枚举中修改和CopyTo参数的正确异常，不静默忽略错误。 |
| ⬜ | P1 | `BindingsCollection-05` | 增删后Binding初始ReadValue/WriteValue及manager订阅/解绑、Count和控件实际属性同步。 |
| ⬜ | P2 | `BindingsCollection-06` | OnCollectionChanging/OnCollectionChanged/Core派生扩展，可外部继承验证；ShouldSerializeMyAll受保护序列化规则。 |
| ⬜ | P2 | `BindingsCollection-07` | 源Reset/BindingContext改变与Clear竞合，解除强引用且不擅自Dispose模型或外部共享数据。 |
| ⬜ | P2 | `BindingsCollection-08` | 设计器DataBindings初始化/序列化、AOT静态访问器可达性，接口继承ICollection/IEnumerable按附件逐一消费。 |
| ⬜ | P3 | `BindingsCollection-09` | 三平台共用集合，重复挂载/释放和组件条目绑定样本；集合存在不等于绑定生效。 |

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

- https://learn.microsoft.com/dotnet/api/system.windows.forms.bindingscollection?view=windowsdesktop-10.0
- https://docs.gtk.org/gtk4/class.ColumnView.html
- https://developer.apple.com/documentation/appkit/nstableview
