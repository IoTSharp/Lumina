# Binding

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/Binding.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · ⬜

未实现。只有控件同名属性没有 Binding/DataBindings 标准写法，无法覆盖典型业务窗体。必须保留控件属性名、数据源、数据成员字符串形式与现有格式化事件。

- 标准继承/契约：`Binding` 普通对象；依赖 ControlBindingsCollection、BindingsCollection、BindingManagerBase、BindingMemberInfo 等。
- 代码证据：[Lumina.Forms 库目录](../../../Lumina.Forms/)（未找到本类型定义）。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `Binding-01` | 所有 Binding 构造重载、PropertyName/BindingMemberInfo/DataSource/Control/BindableComponent/BindingManagerBase；Control.DataBindings.Add 的标准入口可编译且建立真实绑定。 |
| ⬜ | P0 | `Binding-02` | 默认 FormattingEnabled=false、DataSourceUpdateMode=OnValidation、ControlUpdateMode=OnPropertyChanged；NullValue/DataSourceNullValue/FormatString/FormatInfo 按构造重载和目标类型固定基线。 |
| ⬜ | P1 | `Binding-03` | ReadValue/WriteValue、IsBinding、绑定建立时初次读取/写回；DataSourceUpdateMode.Never 也需按官方实际初始绑定行为验收，不能直觉理解为从不触碰源。 |
| ⬜ | P1 | `Binding-04` | OnPropertyChanged/OnValidation/Never 两向模式；ControlUpdateMode.Never/OnPropertyChanged、INotifyPropertyChanged、目标属性 Changed 事件、Validating 取消、丢焦点的读写次数与顺序。 |
| ⬜ | P1 | `Binding-05` | Format/Parse、ConvertEventArgs.DesiredType/Value、FormatString/FormatInfo、FormattingEnabled；文化、nullable/enum、null/DBNull/空串、无效类型转换和异常路径。 |
| ⬜ | P1 | `Binding-06` | BindingComplete/BindingCompleteState/BindingCompleteContext、Cancel、ErrorText/Exception；格式化成功、解析失败、数据源 setter 失败各走对应反馈，不吞错。 |
| ⬜ | P1 | `Binding-07` | 成员路径和 BindingMemberInfo.BindingPath/BindingField，CurrencyManager 当前项/主从关系、嵌套对象替换和 null 中间对象；源替换时清理旧订阅。 |
| ⬜ | P2 | `Binding-08` | 目标 PropertyDescriptor、ICustomTypeDescriptor/ITypedList、自定义 TypeConverter、只读属性与不可绑定属性错误；ControlBindingsCollection 重复属性绑定限制。 |
| ⬜ | P2 | `Binding-09` | 生成器识别既有 new Binding/Controls.DataBindings.Add 的常量属性名和静态类型，生成标准访问器/描述符；不使用 Expression.Compile/DynamicMethod，不要求用户改公共语法。 |
| ⬜ | P2 | `Binding-10` | Remove/Clear 绑定、控件 Dispose、源 Dispose、BindingContext 切换、事件回调内重新绑定；不残留 source→control 强引用与重入循环。 |
| ⬜ | P3 | `Binding-11` | Windows/GTK/AppKit 同一绑定样本分别验证 Text/Checked/SelectedValue/Value；原生信号只触发标准属性变化，不插入额外写回。 |

## 三平台实现分析

### Windows

托管绑定引擎；目标 Control 的标准属性变化/验证和 UI 线程是入口，没有单独 HWND。元数据访问和转换必须 AOT 可达，不能依赖运行时 IL。

### Linux · GTK 4

同一引擎，GTK 原生 signal 先更新 Lumina.Forms 属性，再由 Binding 决定写回；不暴露 GTK expression 作为替代绑定 API。

### macOS · AppKit

同一引擎，AppKit target/action/delegate 回调更新属性；不把 Cocoa Bindings 的自动提交顺序直接当 WinForms OnValidation。

## 边界、AOT 与验收

Binding 不是 IDisposable 组件，生命周期由所属绑定集合/控件管理；不得发明新的公开释放模型。未知插件模型可要求明确保留元数据，但已有标准 descriptor 接口应在 AOT 直接工作。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.binding?view=windowsdesktop-10.0
- https://learn.microsoft.com/dotnet/api/system.windows.forms.binding.datasourceupdatemode?view=windowsdesktop-10.0
- https://learn.microsoft.com/dotnet/core/deploying/native-aot/
- https://docs.gtk.org/gtk4/class.Entry.html
- https://developer.apple.com/documentation/appkit/nscontrol
