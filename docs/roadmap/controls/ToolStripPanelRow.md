# ToolStripPanelRow

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/ToolStripPanelRow.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · ⬜

【M2】未发现 Lumina 对应实现。工具条停靠行是Component对象；Controls是Control[]快照契约，不能误作可任意写的ControlCollection。

- 标准继承/契约：`System.Windows.Forms.ToolStripPanelRow : System.ComponentModel.Component`。
- 代码证据：[Lumina.Forms 库目录](../../../Lumina.Forms/)（未找到本类型定义）。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `ToolStripPanelRow-01` | 公开ToolStripPanelRow(ToolStripPanel)构造、Component基类、parent=null/无效参数，ToolStripPanel和Orientation只读契约。 |
| ⬜ | P1 | `ToolStripPanelRow-02` | Bounds/DisplayRectangle/Controls与Margin/Padding、DefaultMargin/DefaultPadding；布局前/后默认值和快照修改是否影响宿主按标准。 |
| ⬜ | P1 | `ToolStripPanelRow-03` | CanMove(toolStripToDrag)在锁定/拖动/同一行/跨行/不匹配方向场景的返回及参数反馈。 |
| ⬜ | P1 | `ToolStripPanelRow-04` | OnBoundsChanged/OnControlAdded/OnControlRemoved/OnOrientationChanged/OnLayout准确可见性与派生覆盖，控件顺序/索引和事件路由。 |
| ⬜ | P1 | `ToolStripPanelRow-05` | LayoutEngine、横纵方向、行内margin/间距、面板尺寸不足时换行；ToolStripPanel.Join/拖拽与Row归属同步。 |
| ⬜ | P2 | `ToolStripPanelRow-06` | 行插入/移除、ToolStrip换父面板、隐藏/显示、RTL与DPI变化，Bounds和实际原生条带一致。 |
| ⬜ | P2 | `ToolStripPanelRow-07` | 布局Suspended/ResumeLayout/PerformLayout批次与重入，不能递归创建重复Row或丢掉ToolStrip。 |
| ⬜ | P2 | `ToolStripPanelRow-08` | Site/Container/Disposed/Dispose，释放行和移动ToolStrip的所有权区别，外部共享图片/renderer不释放。 |
| ⬜ | P3 | `ToolStripPanelRow-09` | 三平台相同Join/多行/拖移/方向切换样本；标准属性和可见位置同时取证。 |

## 三平台实现分析

### Windows

使用现有ToolStripPanel托管行布局并放置各Win32工具条宿主；没有独立Win32 PanelRow公共控件。移动与命中由同一布局模型维护，不新增公开dock系统。

### Linux · GTK 4

GtkBox/GtkFixed等原生容器承载工具条原生部件；行布局和拖移由ToolStripPanel内部标准契约控制，GTK默认布局不能取代WinForms行归属和事件。

### macOS · AppKit

原生NSView容器承载各条带视图；NSToolbar是窗口工具条机制，不能直接等价任意可停靠多行ToolStripPanelRow，需保留托管行布局。

## 边界、AOT 与验收

依赖ToolStripPanel/ToolStrip与公共布局/捕获。内部复用既有布局代码，不设计通用工作区管理器；AOT回调和静态布局对象可达，三平台逐场景验收。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstrippanelrow?view=windowsdesktop-10.0
- https://docs.gtk.org/gtk4/class.ColumnView.html
- https://developer.apple.com/documentation/appkit/nstableview
