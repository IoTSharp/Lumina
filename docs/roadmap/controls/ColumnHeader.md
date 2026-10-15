# ColumnHeader

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/ColumnHeader.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · 🟡

当前 sealed 普通对象，仅 Text/Width/TextAlign；标准是 Component/ICloneable，现有 Width 默认0、Text 为空，而 .NET10.0.11 参考端实测 Width=60、Text="ColumnHeader"、TextAlign=Left、DisplayIndex=-1。Columns 实际是顶层 ColumnHeaderCollection，标准类型为 ListView.ColumnHeaderCollection，且未接原生列。

- 标准继承/契约：`ColumnHeader : Component, ICloneable`；集合标准嵌套在 ListView。
- 代码证据：[当前源码](../../../Lumina.Forms/ListViewSupport.cs)。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `ColumnHeader-01` | 恢复 Component/构造/ICloneable/可继承契约；列集合嵌套类型和 Add/Insert 返回值及重载按标准，已有顶层兼容类型如何保留单列评估。 |
| ⬜ | P0 | `ColumnHeader-02` | Text/Width/TextAlign、Name/ImageKey/Tag 默认固定 .NET10 样本；负 Width=-1/-2 自动按内容/表头调整的语义与非法值区分。 |
| ⬜ | P1 | `ColumnHeader-03` | ListView/Index/DisplayIndex、列添加/插入/删除/重排；逻辑索引与显示索引分离，重复宿主和同列重复加入按官方规则。 |
| ⬜ | P1 | `ColumnHeader-04` | Text/Width/TextAlign/DisplayIndex 运行时变更，LVM_GETCOLUMN 与表头位置可读回；属性值不能仅存托管字段。 |
| ⬜ | P1 | `ColumnHeader-05` | ImageIndex/ImageKey/ImageList 与 ListView.SmallImageList，表头图像/文字对齐、key/index 互斥及图像列表替换。 |
| ⬜ | P1 | `ColumnHeader-06` | 用户拖宽/重排映射 ColumnWidthChanging（可取消）/ColumnWidthChanged/ColumnReordered，取消后托管与表头顺序/宽度保持一致。 |
| ⬜ | P2 | `ColumnHeader-07` | AutoResize(ColumnHeaderAutoResizeStyle)、ListView.AutoResizeColumn/Columns，空列/隐藏项/长文本/DPI/RTL；View 切换保留宽度。 |
| ⬜ | P2 | `ColumnHeader-08` | Clone 与 Site/Name/Tag/图像引用规则，设计器静态列序列化和属性 Reset；用户派生 ColumnHeader 不因 sealed 阻断。 |
| ⬜ | P2 | `ColumnHeader-09` | 列 Dispose、从集合移除与 ListView Dispose 的生命周期区别，原生列/图像释放和事件订阅一致。 |
| ⬜ | P3 | `ColumnHeader-10` | GtkColumnViewColumn、NSTableColumn 映射逻辑/显示索引与 native 排序重排通知；自绘表头 GDI 事件作为绘图边界单列。 |

## 三平台实现分析

### Windows

SysListView32 的列与 Header 子窗：LVM_INSERTCOLUMN/SETCOLUMN/DELETECOLUMN、LVM_SETCOLUMNORDERARRAY；HDN_ITEMCHANGING/ITEMCHANGED/ENDDRAG 等协调可取消事件。

### Linux · GTK 4

GtkColumnViewColumn 负责标题、固定宽/扩展、可调整和重排；列映射持有托管 ColumnHeader。GTK 按内容自动量测策略与 -1/-2 需要兼容计算，不能直接给负像素。

### macOS · AppKit

NSTableColumn/title/width/headerCell，NSTableView 列重排和 resize 通知更新 DisplayIndex/Width；可取消重排需在允许 delegate 阶段实现，非事后只改字段。

## 边界、AOT 与验收

依赖 ListView 与 Component。列顺序和动态属性同步比静态显示更关键；源文件真实最近仅一个相关提交，不能补造两条历史。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- `2fb513a` · 2026-04-09：ListViewSupport.cs 加入基础 ColumnHeader/集合及 View 枚举；尚无原生列同步。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.columnheader?view=windowsdesktop-10.0
- https://docs.gtk.org/gtk4/class.ColumnViewColumn.html
- https://developer.apple.com/documentation/appkit/nstablecolumn
