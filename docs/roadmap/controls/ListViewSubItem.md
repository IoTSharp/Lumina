# ListViewSubItem

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/ListViewSubItem.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · 🟡

标准完整类型名为 `System.Windows.Forms.ListViewItem.ListViewSubItem`（反射名含 `+`），本页使用短文件名。批次 004 完成基础公共构造、继承及 Text/Name/Tag；批次 005 完成五参样式构造、颜色/字体与 ResetStyle 的独立和未挂接 owner 子范围，新增 37 条样式观测与固定 WinForms 10.0.11 相同。已挂接控件样式继承、全局默认字体、Bounds、完整所属与 key 集合及 native 同步仍缺。

- 标准继承/契约：`ListViewItem.ListViewSubItem`，非视觉普通对象；嵌套 ListViewSubItemCollection 管理归属。
- 代码证据：[当前源码](../../../Lumina.Forms/ListViewItem.cs)。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| 🟡 | P0 | `ListViewSubItem-01` | 批次 004/005 已完成三种公共构造、继承、Text/Name/Tag 与独立/未挂接样式（01a/b）；已挂接控件及全局默认字体资源继承仍待后续。 |
| ✅ | P0 | `ListViewSubItem-01a` | 批次 004：公共无参/owner-text、可继承性、Text/Name/Tag 的默认/null/引用身份及首子项同步；37 条两端观测与 13 项新增回归通过，原 21 项构造/41 条观测保留。不新增公开 Owner；样式/完整所属/native 仍待后续。 |
| ✅ | P0 | `ListViewSubItem-01b` | 批次 005：五参样式构造、ForeColor/BackColor/Font、ResetStyle 的独立/未挂接 owner 行为；37 条两端样式观测与 15 项新增测试通过。空颜色/null Font 恢复、引用身份及共享借用 Font 寿命已检；已挂接继承、UseItemStyleForSubItems、全局默认字体和 native 刷新另列。 |
| ⬜ | P1 | `ListViewSubItem-02` | 所属项与 SubItems 集合索引规则、Text 与第0子项/ListViewItem.Text 同步；Add/Insert/Remove/替换/清空更新原生对应列；不新增公开 Owner 属性。 |
| 🟡 | P1 | `ListViewSubItem-03` | 批次 004 已有基础 Name/Tag 与 null/身份语义；按 key 查找、ContainsKey/IndexOfKey/RemoveByKey、重复 key 和改 Name 后缓存失效及整数索引错误仍待实现。 |
| ⬜ | P1 | `ListViewSubItem-03a` | 批次 006 首范围：集合 string key 索引器、ContainsKey/IndexOfKey/RemoveByKey，固定参考端比较 null/空/大小写/重复项、修改 Name 后查找和首次匹配移除；保留完整所属转移、IList/构造/整数索引规则及 native 同步缺口。 |
| 🟡 | P1 | `ListViewSubItem-04` | 批次 005 完成独立/未挂接的颜色字体及空值恢复；ListViewItem.UseItemStyleForSubItems、父控件继承优先级及全局默认字体仍待后续。 |
| ⬜ | P1 | `ListViewSubItem-05` | Bounds、文本/图像对齐、列重排、水平滚动、非 Details 模式/未挂载的结果；不能以固定矩形占位。 |
| 🟡 | P2 | `ListViewSubItem-06` | 批次 005 完成独立/未挂接 ResetStyle；ToString、样式序列化/设计器 .resx 及 Clone 由 owner 执行的复制语义、AOT 静态资源路径仍待后续。 |
| ⬜ | P2 | `ListViewSubItem-07` | DrawSubItem 与 UseItemStyleForSubItems=false 时样式、DPI/RTL/高对比度；不擅自让每子项可编辑（标准 LabelEdit 只针对标签）。 |
| ⬜ | P2 | `ListViewSubItem-08` | owner/item/column 移除、重建 ListView、native row/cell 回收，子项关联不悬挂；不擅自释放外部共享 Font。 |
| ⬜ | P3 | `ListViewSubItem-09` | GTK 原生 column cell、AppKit NSTableCellView 样式适配；GDI DrawListViewSubItemEventArgs 的绘图边界分项登记，文本显示不算样式全通过。 |

## 三平台实现分析

### Windows

ListViewItem 持有子项模型；通过 LVM_SETITEMTEXT/SETITEM 更新，WM_NOTIFY/NM_CUSTOMDRAW 支持每子项颜色/字体，LVM_GETSUBITEMRECT 获得 Bounds。

### Linux · GTK 4

GtkColumnView 每列 factory 绑定同一 item 的相应子项，GtkLabel/Pango 样式显示；列虚拟重用时解除旧值/旧 CSS，Bounds 需坐标转换到控件坐标。

### macOS · AppKit

NSTableCellView/NSTextField 对应列单元格，按 owner 样式决定字体/颜色，表格可见单元格 rect 转到 ListView 坐标；native view 回收不得改变托管子项。

## 边界、AOT 与验收

依赖 ListViewItem、ListView 与跨平台 Font/Color/Graphics 策略；API 清单使用短文件名，但内容必须保存真实嵌套类型名，避免误把独立顶层 ListViewSubItem 当兼容。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- `b8a98eb` · 2026-04-09：ListViewItem.cs 同时加入基础 Text 子项及集合；未覆盖公开构造和样式契约。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。
- 2026-10-16 · 批次 004：先运行固定 WinForms 10.0.11 的 37 条观测，再补公共无参/owner-text、可继承性和 Text/Name/Tag；构造不会自动插入 owner.SubItems 或替换首项，null 字符串归空且保留非 null 字符串/Tag 身份。增加 13 项无 UI 回归，保留原构造与清空/恢复行为。Owner 只在库内保存，不新增公开属性；颜色字体/完整集合/native 未实现，不从指定场景推算完整兼容。

- 2026-10-16 · 批次 005：先运行固定 WinForms 10.0.11 参考端，再补五参构造、ForeColor/BackColor/Font 与 ResetStyle；37 条两端样式观测相同，新增 15 项测试。默认使用 WindowText/Window 与惰性共享的官方 SystemFonts.MessageBoxFont（本机为 Microsoft YaHei UI 9，不能代以 SystemFonts.DefaultFont）；显式 Font 保留借用身份，不因置空/替换/Reset 而释放。保留 owner/文字/首项语义。未接 Control.DefaultFont/Application 自定义默认字体与 item→ListView 回链，不能据此宣称已挂接继承/native 或完整样式兼容。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.listviewitem.listviewsubitem?view=windowsdesktop-10.0
- https://docs.gtk.org/gtk4/class.ColumnView.html
- https://developer.apple.com/documentation/appkit/nstablecellview
