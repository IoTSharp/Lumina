# Timer

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/Timer.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · ⬜

库中未实现标准 System.Windows.Forms.Timer 替代类型。不能把 System.Threading.Timer/Task.Delay 直接包装为 Tick；WinForms Tick 归属 UI 消息循环，阻塞与重入语义必须保留。

- 标准继承/契约：`Timer : Component`；带 IContainer 构造，公开 Start/Stop/Enabled/Interval/Tag 和 Tick。
- 代码证据：[Lumina.Forms 库目录](../../../Lumina.Forms/)（未找到本类型定义）。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `Timer-01` | 构造/Component/IContainer/Dispose(bool)/OnTick 受保护扩展点；默认 Enabled=false、Interval=100、Tag=null；和 System.Threading.Timer 命名冲突不改变 API。 |
| ⬜ | P1 | `Timer-02` | Start/Stop 与 Enabled 同步，重复启动/停止、启动前设 Interval、运行中改 Interval；非法 Interval<=0 按标准抛异常。 |
| ⬜ | P1 | `Timer-03` | Tick 在创建/运行该 Timer 的 UI 线程发出，有消息循环才触发；记录线程 ID 和 SynchronizationContext，不能线程池直接调用事件。 |
| ⬜ | P1 | `Timer-04` | UI 线程阻塞多个间隔后不按“漏掉次数”突发补发；长 Tick、处理器中 Stop/Start/改 Interval/Dispose、嵌套消息循环的实际重入与官方对照。 |
| ⬜ | P1 | `Timer-05` | 启动/停止/释放临界时的已排队回调；Stop 后不继续触发 Tick，Dispose 后移除 native source 和托管根引用。 |
| ⬜ | P2 | `Timer-06` | Begin/End 消息循环、Application.Exit/窗口关闭/组件容器 Dispose、没有可见窗口但有 UI loop；对照 Timer 是否保活及进程退出行为。 |
| ⬜ | P2 | `Timer-07` | 跨线程 Start/Stop/Interval 的标准许可范围与异常；不额外承诺线程安全，不添加无依据防御层；异常按 UI ThreadException/UnhandledException 策略流转。 |
| ⬜ | P2 | `Timer-08` | 设计器拖入组件托盘与 .Designer.cs 容器释放、Tick 订阅、AOT 裁剪后事件目标可达；无反射找 Tick 方法。 |
| ⬜ | P3 | `Timer-09` | Win32、GLib 与 NSTimer 的时钟精度/弹层和拖动期间运行模式差异实测，记录允许时序误差；行为门槛以线程、次数上界和停止保证为主，不假装实时计时器。 |

## 三平台实现分析

### Windows

隐藏/消息专用窗口 + SetTimer/KillTimer/WM_TIMER，或行为等价的 UI pump 定时源。窗口回调和 timer ID 由组件持有，退出与释放时同步移除。

### Linux · GTK 4

GLib g_timeout_add_full 或显式 GSource attach 到正确 UI GMainContext；回调在执行该 context 的线程，不能误 attach 默认 context 后声称一定 UI 线程。GLib 超时不补偿延迟符合 UI timer 常见需求，仍需实际事件对照。

### macOS · AppKit

Foundation NSTimer 安装到 UI NSRunLoop，记录 default/common/event-tracking run loop mode 的选择与弹层/拖动差异；Stop/Dispose 用 invalidate，释放 target/block 引用防止循环持有。

## 边界、AOT 与验收

禁止自创 async timer API 替代标准 Timer。Native AOT 需要静态 unmanaged callback、受控 delegate/GCHandle 生命周期；比较语义不要求毫秒级实时精度。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.timer?view=windowsdesktop-10.0
- https://learn.microsoft.com/windows/win32/winmsg/wm-timer
- https://docs.gtk.org/glib/func.timeout_add_full.html
- https://developer.apple.com/documentation/foundation/timer
