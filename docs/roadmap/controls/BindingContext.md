# BindingContext

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/BindingContext.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · ⬜

未实现。当前 Form/Control 继承关系不足，也没有每容器共享绑定管理器的标准上下文；仅在各控件各建列表会破坏共享 Current/Position 与主从同步。

- 标准继承/契约：`BindingContext : ICollection, IEnumerable`；普通对象，无 HWND。
- 代码证据：[Lumina.Forms 库目录](../../../Lumina.Forms/)（未找到本类型定义）。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `BindingContext-01` | 公开构造、this[dataSource] / this[dataSource,dataMember]、Contains 两重载、ICollection 显式实现、IsReadOnly 与 protected Add/AddCore/Remove/RemoveCore/Clear/ClearCore；可派生验证。 |
| ⬜ | P0 | `BindingContext-02` | Control.BindingContext 的父级继承与显式覆盖、Form/ContainerControl 默认上下文；同源同成员返回同一 manager，不同容器显式上下文隔离。 |
| ⬜ | P1 | `BindingContext-03` | 对象源返回 PropertyManager、列表源返回 CurrencyManager，数据成员链返回相关 manager；null/非法成员/非列表路径的异常与官方一致。 |
| ⬜ | P1 | `BindingContext-04` | Contains 只查询既有管理器还是创建的实际行为固定样本；同一数据源引用与相等但不同对象分别验证，避免错误按 Equals 合并。 |
| ⬜ | P1 | `BindingContext-05` | UpdateBinding(BindingContext,Binding) 切换旧/新 manager；共享两控件同步 Position，控件移父级和源替换不残留旧订阅。 |
| ⬜ | P1 | `BindingContext-06` | CollectionChanged 与 protected OnCollectionChanged/AddCore 等派生钩子，普通类与自定义派生类的实际通知规则逐项实测，不假设所有操作都公开可调用。 |
| ⬜ | P2 | `BindingContext-07` | 嵌套 DataMember 主从关系、父当前位置变更、子列表替换、空列表/删除当前父项；manager 身份和 Current 过渡状态一致。 |
| ⬜ | P2 | `BindingContext-08` | 弱引用/生命周期策略按标准评估；上下文存活时源是否可回收、绑定移除后的 manager 订阅清理、重入与 UI 线程。 |
| ⬜ | P2 | `BindingContext-09` | 生成器/标准描述符支持成员路径，未知动态路径诊断；不新增公共 ContextFactory/SchemaBuilder 等替代 API。 |
| ⬜ | P3 | `BindingContext-10` | 三平台共享逻辑，双控件同步/容器隔离/主从路径 AOT 差分样本，平台原生选择更新不得新建独立 manager。 |

## 三平台实现分析

### Windows

托管上下文管理，不需要 native 窗口/COM；Control/ContainerControl 的 BindingContextChanged 是重新接线入口。

### Linux · GTK 4

同一上下文管理；各 Gtk 控件模型读取相同 CurrencyManager，GTK SelectionModel 的当前位置不能独自覆盖其他绑定控件状态。

### macOS · AppKit

同一管理器，NSTableView 与 NSComboBox 的选中对象通过同一上下文同步；Cocoa Bindings scope 不是替代实现。

## 边界、AOT 与验收

本类型无 IDisposable 标准要求；生命周期通过绑定引用、父容器和 manager 解绑处理，不新增 ReleaseContext API。AOT 采用已有反射可保留范围与 descriptor，不运行时生成成员访问 IL。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.bindingcontext?view=windowsdesktop-10.0
- https://learn.microsoft.com/dotnet/api/system.windows.forms.control.bindingcontext?view=windowsdesktop-10.0
- https://learn.microsoft.com/dotnet/api/system.windows.forms.bindingmanagerbase?view=windowsdesktop-10.0
