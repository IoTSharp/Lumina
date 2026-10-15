# ListViewItem

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/ListViewItem.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · 🟡

当前只有 string 构造、Text/SubItems 和 ForeColor；构造在 SubItems 赋值前调用 Text setter，审计已复现 NullReferenceException。缺全部所属、选择、勾选、图像、分组和多数集合语义。

- 标准继承/契约：`ListViewItem : ICloneable, ISerializable`；不是 Control/Component。
- 代码证据：[当前源码](../../../Lumina.Forms/ListViewItem.cs)。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `ListViewItem-01` | 修复构造初始化顺序并补全部标准构造；Text 与 SubItems[0].Text 双向一致，无参构造默认和 string[]/image/group 重载均能工作。 |
| ⬜ | P1 | `ListViewItem-02` | ListView/Index/Bounds/Position、Remove/EnsureVisible/FindNearestItem/GetBounds/GetSubItemAt/BeginEdit；无宿主/无句柄/不同 View 模式下结果和异常一致。 |
| ⬜ | P1 | `ListViewItem-03` | Selected/Focused/Checked/StateImageIndex、对应 ListView 选择/复选事件和 native 状态；Items.Clear 后旧对象属性回落规则。 |
| ⬜ | P1 | `ListViewItem-04` | ImageIndex/ImageKey/ImageList、IndentCount/Group、Name/Tag/ToolTipText；key/index 互斥、组替换、图像列表重建同步。 |
| ⬜ | P1 | `ListViewItem-05` | SubItems 集合的 Add/AddRange/Insert/Remove/Clear/索引 setter/Name key；子项 Owner 和第0项特殊规则、删除第0项后的 Text 正确。 |
| ⬜ | P2 | `ListViewItem-06` | BackColor/ForeColor/Font/UseItemStyleForSubItems，父 ListView 继承值与每子项独立样式，变更实时更新对应 native item/subitem。 |
| ⬜ | P2 | `ListViewItem-07` | Clone 深浅拷贝边界、SubItems 和 Group/Tag/Font 引用规则；序列化与设计器 .resx 常见项资源往返，AOT 不走任意类型反序列化。 |
| ⬜ | P2 | `ListViewItem-08` | VirtualMode 的 RetrieveVirtualItem 返回值及 item 所属关系、重复返回同一 item、模式切换；不把常规 Items 所有权强套到虚拟模式。 |
| ⬜ | P2 | `ListViewItem-09` | 项移除/重插入、源 ListView Dispose、native item delete 通知和回调重入，不残留 handle/id/订阅；Item 无 IDisposable，不新增释放 API。 |
| ⬜ | P3 | `ListViewItem-10` | GTK/AppKit item ID 固定，行 widget/cell 复用与项身份分离；View 切换保留 Selected/Focused/Group，三平台对照。 |

## 三平台实现分析

### Windows

托管项用稳定 ID/lParam 关联 SysListView32；LVM_SETITEM/GETITEMSTATE/SETITEMSTATE/GETITEMRECT 等读写；SubItems 和图像、Group 状态同步，使用 ListView 通知发事件。

### Linux · GTK 4

托管 item 经 GListModel 与 native row factory 显示；多模式切换复用同一 ID，GTK 行选区映射 Selected，Focused 仍需兼容状态管理。

### macOS · AppKit

托管 item 经 NSTableView/NSCollectionView dataSource 显示；native cell/view 生命周期不持有错误 item，不用显示行号作永久对象身份。

## 边界、AOT 与验收

本页依赖 ListView、ListViewSubItem、ColumnHeader 和 ImageList。Windows 构造异常为首要阻塞；先消除缺陷再扩展主题/性能。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- `b8a98eb` · 2026-04-09：加入 ListViewItem 与基础子项模型；当前初始化次序仍需修复。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.listviewitem?view=windowsdesktop-10.0
- https://docs.gtk.org/gtk4/class.ListItemFactory.html
- https://developer.apple.com/documentation/appkit/nstableviewdatasource
