# TableLayoutPanel

🟡 **当前状态：**已有行列分配、Absolute/AutoSize/Percent 尺寸与 ColumnSpan 的部分算法；RowStyles/ColumnStyles 当前是 List，缺标准集合与 RowSpan、完整扩展属性/查询 API。020已完成标准布局事务入口衔接，完整算法与控件仍未完成。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[TableLayoutPanel API](../api/TableLayoutPanel.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：[TableLayoutPanel.cs](../../../Lumina.Forms/TableLayoutPanel.cs)
- 前置与排序：Panel/ScrollableControl、首选大小与布局事务；ColumnStyle/RowStyle/标准集合。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| ⬜ | P0 / TableLayoutPanel-01 | 补齐 TableLayoutSettings、TableLayoutControlCollection、TableLayoutColumnStyleCollection/TableLayoutRowStyleCollection，保证设计器代码与公开返回类型正确。 |
| ⬜ | P0 / TableLayoutPanel-02 | 补 Get/SetColumn、Row、ColumnSpan、RowSpan、CellPosition，以及 GetControlFromPosition/GetPositionFromControl；列出负值、越界、null、重复占格的标准异常。 |
| ⬜ | P1 / TableLayoutPanel-03 | 校准 ColumnCount/RowCount、GrowStyle、CellBorderStyle 默认与扩容；固定尺寸表满、跨格覆盖和删除控件后占位回收。 |
| ⬜ | P1 / TableLayoutPanel-04 | 实现 AutoSize/GetPreferredSize、SizeType.Absolute/AutoSize/Percent 的分配顺序、跨行列测量、百分比剩余分配及舍入；不以当前 Width/Height 直接代替首选大小。 |
| ⬜ | P1 / TableLayoutPanel-05 | 支持单元内 Dock/Anchor、Margin/Padding、隐藏控件、RTL、AutoScroll；当前 LayoutControlInCell 只落左上角需补齐对齐/拉伸。 |
| 🟡 | P1 / TableLayoutPanel-06 | 020完成06a标准Layout入口与暂停/恢复衔接；CellPaint、Row/ColumnStyles动态变化、完整SetRowSpan/SetColumnSpan事务及算法仍待实现/对照。 |
| ✅ | P1 / TableLayoutPanel-06a | 批次020：标准OnLayout先发事件再执行现有table几何，SuspendLayout实际延期；原PerformLayout算法迁入标准OnLayout。 仅该事务接入范围代码完成，不代表完整容器算法/平台验收。 |
| ⬜ | P1 / TableLayoutPanel-07 | 实现 GetColumnWidths/GetRowHeights 与 LayoutSettings 序列化；设计器本地化 .resx 的行列样式和跨格设置可以还原。 |
| ⬜ | P2 / TableLayoutPanel-08 | Windows 在 Panel 原生宿主上批量移动原生子 HWND；边框绘制属容器装饰，不能将完整表布局改成画布子控件。 |
| ⬜ | P2 / TableLayoutPanel-09 | Linux 可用 GtkGrid 承载原生子控件，但 WinForms Percent/AutoSize/跨度与 GTK 分配规则不同；先跑共享测量，必要时 GtkFixed 精确落位，避免双重布局。 |
| ⬜ | P3 / TableLayoutPanel-10 | macOS 可用 NSGridView 表达简单网格，完整 WinForms 百分比/跨格/滚动需求用 NSView + 共享测量摆放原生子控件；不能静默降低跨格能力。 |
| ⬜ | P1 / TableLayoutPanel-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / TableLayoutPanel-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI 写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / TableLayoutPanel-V3 | 发布 win-x64 Native AOT，检查实际窗口与本控件交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | Panel 子 HWND、共享表布局；已有算法可修正复用，不另建复杂约束求解框架。 |
| Linux | GtkGrid 提供原生行列/跨度，但不是百分比分配的等价物；选择 GtkFixed 时 children 仍是原生 widgets。 |
| macOS | NSGridView 与 NSView 组合；实际选择由固定场景验证决定。原生 cell 元素保留可访问性和焦点。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- 2026-10-16 · 批次020：标准事件先于现有表格算法，保留行列分配局部实现；样式集合/RowSpan/完整扩展属性和首选测量未完成。 见[020](../batches/020.md)；父任务保持部分完成。

- `2fb513a`（2026-04-09）：优化 TableLayoutPanel 布局逻辑。
- `9d5165f`（2026-04-09）：新增兼容控件功能涉及表布局。
- `3323e20`（2026-03-31）：布局和交互能力改进。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms TableLayoutPanel 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.tablelayoutpanel?view=windowsdesktop-10.0)
- [GTK4 Grid](https://docs.gtk.org/gtk4/class.Grid.html)
- [AppKit nsgridview](https://developer.apple.com/documentation/appkit/nsgridview)
