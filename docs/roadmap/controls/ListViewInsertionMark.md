# ListViewInsertionMark

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/ListViewInsertionMark.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · ⬜

【M2】未发现 Lumina 对应实现。拖移时的可见插入提示对象，不等于自动实现DragDrop重排。

- 标准继承/契约：`System.Windows.Forms.ListViewInsertionMark : System.Object`。
- 代码证据：[Lumina.Forms 库目录](../../../Lumina.Forms/)（未找到本类型定义）。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `ListViewInsertionMark-01` | 准确类型/构造可见性，经ListView.InsertionMark取得稳定对象，Index/AppearsAfterItem/Color/Bounds默认值对照。 |
| ⬜ | P1 | `ListViewInsertionMark-02` | Index=-1隐藏和有效/越界索引、AppearsAfterItem前后位置，句柄创建前后设置结果；逻辑索引与排序后可见索引区别。 |
| ⬜ | P1 | `ListViewInsertionMark-03` | NearestIndex(Point)客户端坐标、空列表/边缘/组头/滚动区域/多列布局，返回值和不支持视图的基线规则。 |
| ⬜ | P1 | `ListViewInsertionMark-04` | Bounds只读且随滚动/缩放/字体/视图/方向变化，Color改变实际native提示，不只是字段读写。 |
| ⬜ | P2 | `ListViewInsertionMark-05` | 插入标记显示时删除/重排条目、切视图/Groups/VirtualMode和重建句柄，旧索引不得产生越界native调用。 |
| ⬜ | P2 | `ListViewInsertionMark-06` | DragEnter/DragOver/DragLeave/DragDrop由ListView标准事件触发，消费者决定重排；标记对象不自行移动业务集合。 |
| ⬜ | P2 | `ListViewInsertionMark-07` | RTL、DPI、Dark/HighContrast和鼠标捕获下命中，复用系统主题与颜色，原生没有标记能力时列出未达。 |
| ⬜ | P2 | `ListViewInsertionMark-08` | ListView释放后对象访问/句柄重建的准确行为与回调退订，不为此对象另创Dispose/Release公开契约。 |
| ⬜ | P3 | `ListViewInsertionMark-09` | 三平台空表/分组/滚动/拖移样本，比较NearestIndex、Bounds与屏幕提示；只有API字段不能计为通过。 |

## 三平台实现分析

### Windows

使用LVM_SETINSERTMARK/LVM_GETINSERTMARKRECT/LVM_INSERTMARKHITTEST/LVM_SETINSERTMARKCOLOR等原生消息，围绕当前ListView句柄同步；用户重排仍走标准DragDrop事件。

### Linux · GTK 4

优先使用Gtk原生列表/列视图的drop/插入指示能力和原生样式节点；如果特定视图缺少公共等价API，可内部组合原生提示部件，坐标/命中和拖放事件仍按标准。不得自称GTK自动提供WinForms插入标记。

### macOS · AppKit

NSTableView的setDropRow:dropOperation:或NSCollectionView拖放插入提示可复用，适配前/后项索引和组头坐标。不同视图后端逐一验证，不能把仅接受整行drop等同所有WinForms视图。

## 边界、AOT 与验收

依赖ListView命中/滚动/拖放，不新增自动排序/拖移API。AOT native回调静态可达；可移植的坐标与提示场景均须实际交互证据，Windows专属消息本身另表记录。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.listviewinsertionmark?view=windowsdesktop-10.0
- https://docs.gtk.org/gtk4/class.ColumnView.html
- https://developer.apple.com/documentation/appkit/nstableview
