# CheckedListBox

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/CheckedListBox.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · ⬜

无实现。不能通过 ListBox 行前加一个字符模拟完成；复选状态、选择状态、ItemCheck 的可修改 NewValue 和集合语义都必须独立。

- 标准继承/契约：`CheckedListBox : ListBox : ListControl : Control`。
- 代码证据：[Lumina.Forms 库目录](../../../Lumina.Forms/)（未找到本类型定义）。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `CheckedListBox-01` | 先完成 ListBox/ListControl 继承与标准 CheckedListBox.ObjectCollection；补 CheckedIndexCollection/CheckedItemCollection，索引器和 Add(object,CheckState) 等重载。 |
| ⬜ | P0 | `CheckedListBox-02` | 默认 CheckOnClick=false、ThreeDCheckBoxes=false、DisplayMember/ValueMember 与父类一致；确认 SelectionMode 仅支持官方许可值，非法设置按标准抛异常。 |
| ⬜ | P1 | `CheckedListBox-03` | Items 的 checked/checkState 重载、SetItemChecked/SetItemCheckState/GetItemChecked/GetItemCheckState；Checked/Unchecked/Indeterminate 转换、无效索引和批量更新。 |
| ⬜ | P1 | `CheckedListBox-04` | ItemCheck 在状态提交前发出，处理器可重写 NewValue；处理器读取旧 CheckedItems、重入或删除当前项时按基线保持一致，事件后视图和状态同步。 |
| ⬜ | P1 | `CheckedListBox-05` | CheckedItems/CheckedIndices 为实时只读视图；插入、排序、删除、Items.Clear 与绑定 Reset 后索引和对象映射不残留。 |
| ⬜ | P1 | `CheckedListBox-06` | Space、鼠标复选区域、点击文本、CheckOnClick 两种模式、双击、方向键；SelectedIndexChanged 与 ItemCheck 顺序分别记录，不能把选中自动当勾选。 |
| ⬜ | P2 | `CheckedListBox-07` | DataSource/DisplayMember/ValueMember、列表位置、Format 继承行为；标准 CheckedListBox 不自动把勾选写回任意 bool 属性，避免发明隐式绑定。 |
| ⬜ | P2 | `CheckedListBox-08` | GetItemHeight/ItemHeight、RTL/DPI/字体、高对比度、可访问性 checked/mixed 状态；滚动和三态框点击命中准确。 |
| ⬜ | P2 | `CheckedListBox-09` | 原生控件创建前设 CheckState、重建后还原，BeginUpdate/EndUpdate 不丢 ItemCheck；Dispose 后取消回调和订阅。 |
| ⬜ | P3 | `CheckedListBox-10` | GTK 和 AppKit 的原生 CheckButton/NSButton 行需在切换前进入 ItemCheck 仲裁，取消/改写 NewValue 后能回滚原生状态且不产生二次事件。 |

## 三平台实现分析

### Windows

WinForms CheckedListBox 建于 LISTBOX 与托管勾选绘制/状态，不是独立的系统 CheckedListBox 类。使用 LISTBOX 行与系统主题复选框绘制，WM_DRAWITEM/输入消息管理；保留可访问性和 ItemCheck 提交前语义。

### Linux · GTK 4

GtkListView 行中组合 GtkCheckButton 与 GtkLabel，选择模型和 checked 模型分离；GTK toggled 发生后的回滚必须避免重复事件，可在库内统一切换入口。

### macOS · AppKit

NSTableView 每行 NSButton checkbox + NSTextField；mixed state 通过 NSControlStateValueMixed 表达。选择索引与按钮状态分离，NSButton target/action 先经过兼容状态逻辑。

## 边界、AOT 与验收

依赖 ListBox、AOT 绑定与公共可访问性。纯原生行组合可覆盖视觉，但 ItemCheck 的 NewValue、索引集合和输入序列仍需托管兼容实现。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.checkedlistbox?view=windowsdesktop-10.0
- https://learn.microsoft.com/dotnet/api/system.windows.forms.checkedlistbox.itemcheck?view=windowsdesktop-10.0
- https://docs.gtk.org/gtk4/class.CheckButton.html
- https://developer.apple.com/documentation/appkit/nsbutton
