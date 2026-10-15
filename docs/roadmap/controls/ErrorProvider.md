# ErrorProvider

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/ErrorProvider.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · ⬜

未实现。需要标准 extender provider 属性、可见错误图标/提示及数据错误绑定，不能只有字典存储 SetError/GetError。

- 标准继承/契约：`ErrorProvider : Component, IExtenderProvider, ISupportInitialize`。
- 代码证据：[Lumina.Forms 库目录](../../../Lumina.Forms/)（未找到本类型定义）。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `ErrorProvider-01` | 构造重载、Component/ContainerControl、IExtenderProvider.CanExtend、Site、BeginInit/EndInit；设计器给各 Control 序列化 SetError/SetIconAlignment/SetIconPadding 的标准语法。 |
| ⬜ | P0 | `ErrorProvider-02` | 本机 .NET 10 默认 BlinkRate=250、BlinkStyle=BlinkIfDifferentError、RightToLeft=false、HasErrors=false；Icon/ContainerControl/DataSource/DataMember/Tag 的默认与重设规则固定样本。 |
| ⬜ | P1 | `ErrorProvider-03` | SetError/GetError/Clear/HasErrors；null/空串清除、重复相同错误、多个控件互不干扰，图标与 tooltip 真正显示且跟随窗口前后台。 |
| ⬜ | P1 | `ErrorProvider-04` | GetIconAlignment/SetIconAlignment 六位置、GetIconPadding/SetIconPadding、DPI/RTL/容器滚动/控件隐藏移除/Resize；图标不侵入编辑区和错误命中正确。 |
| ⬜ | P1 | `ErrorProvider-05` | BlinkStyle 的 NeverBlink/BlinkIfDifferentError/AlwaysBlink、BlinkRate=0/负值/运行时变化；相同错误设值按标准决定是否重启闪烁。 |
| ⬜ | P1 | `ErrorProvider-06` | DataSource/DataMember、BindToDataAndErrors/UpdateBinding、CurrencyManager、IDataErrorInfo；当前位置更换、列表项属性变化后更新对应控制属性错误。 |
| ⬜ | P2 | `ErrorProvider-07` | 验证与显示分离：控件 Validating 处理器可 SetError，但 ErrorProvider 不擅自取消验证；RightToLeftChanged 顺序和重复赋值行为。 |
| ⬜ | P2 | `ErrorProvider-08` | Icon 替换与共享、提示文本多行/超长、辅助技术错误描述/状态；空错误不留下悬浮窗或无障碍节点。 |
| ⬜ | P2 | `ErrorProvider-09` | ContainerControl 更换、数据源替换、目标先 Dispose/Provider 先 Dispose、正在闪烁中释放；计时器/提示窗/图标和源订阅全部清理。 |
| ⬜ | P3 | `ErrorProvider-10` | GTK/AppKit 原生图标和 tooltip 的行为差异逐项测试；若系统原生 tooltip 不支持相同时机，明确可移植行为，不以静默不显示计入通过。 |

## 三平台实现分析

### Windows

托管 extender 状态 + 系统图标绘制/提示窗口 + UI Timer；图标可以是受控 Win32 子窗口或随容器的错误叠层，位置用窗口坐标转换，提示用 TOOLTIPS_CLASS。绑定使用标准管理器与 IDataErrorInfo。

### Linux · GTK 4

GtkImage 与 GtkTooltip，必要时 GtkOverlay 放置错误图标（内部组合，不暴露新公共容器）；GLib 主循环定时更新显隐。GTK 自带 CSS error 状态不能代替 WinForms ErrorProvider 的独立图标与 extender 属性。

### macOS · AppKit

NSImageView 作为宿主 sibling/overlay 子视图，toolTip 提供文本，NSTimer 控制闪烁；AppKit 没有完整 ErrorProvider 类型，位置/绑定/多控件状态由托管层实现。

## 边界、AOT 与验收

依赖 Timer、容器布局、公共 Icon/Image 跨平台策略、BindingContext。IDataErrorInfo 标准支持是首阶段；INotifyDataErrorInfo 如拟追加，先按基线核对，不能凭现代习惯悄悄改变错误推导规则。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.errorprovider?view=windowsdesktop-10.0
- https://learn.microsoft.com/dotnet/api/system.windows.forms.errorprovider.blinkstyle?view=windowsdesktop-10.0
- https://docs.gtk.org/gtk4/class.Tooltip.html
- https://docs.gtk.org/gtk4/class.Overlay.html
- https://developer.apple.com/documentation/appkit/nsimageview
