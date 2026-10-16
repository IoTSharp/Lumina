# ToolStrip

🟡 **当前状态：**已有真实宿主映射：Label/StatusLabel→Label，ProgressBar→ProgressBar，ComboBox→ComboBox，TextBox→TextBox，Button/下拉→Button 等；Items 订阅 Changed 并驱动布局。缺完整公开 API、溢出/停靠拖动、Owner 与编辑宿主双向同步验收。020事务与021默认布局必要衔接已完成明确子范围，完整算法与控件仍未完成。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[ToolStrip API](../api/ToolStrip.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：[ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs)
- 前置与排序：Control/布局/输入、全部 ToolStripItem 子类型；先于 MenuStrip/StatusStrip/DropDown/Container。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| ⬜ | P0 / ToolStrip-01 | 校准 ToolStrip : ScrollableControl 的标准继承、构造与 ToolStripItemCollection；Owner/OwnerItem/GetCurrentParent、Add/Insert/Remove/Clear、移动/重排和重复项约束保持一致。 |
| ⬜ | P0 / ToolStrip-02 | 逐宿主核验 Items→native 与 native→Items 双向同步：Text/Enabled/Visible/Checked/CheckState/SelectedIndex/Value/Minimum/Maximum；当前 ApplyItemState 只能证明一个方向存在。 |
| ⬜ | P1 / ToolStrip-03 | 补 AutoSize、CanOverflow、ShowItemToolTips、Stretch、GripStyle、LayoutStyle、ImageList/ImageScalingSize、RenderMode/Renderer、TextDirection、AllowMerge/AllowItemReorder 的标准默认和属性变更。 |
| ⬜ | P1 / ToolStrip-04 | 实现 ItemAdded/ItemRemoved/ItemClicked、LayoutCompleted、BeginDrag/EndDrag、RendererChanged；同一激活路径只触发一次 item.Click 与 strip.ItemClicked，顺序按官方捕获。 |
| 🟡 | P1 / ToolStrip-05 | 020事务、021父Dock/Orientation有限接入保留；022完成05c有效字体测量衔接。完整LayoutStyleChanged、Alignment/Overflow/Placement、全部item测量、RTL/DPI仍缺。 |
| ✅ | P1 / ToolStrip-05c | 批次022 MeasureTextCore使用本控件有效Font对应的独占HFONT，替代Form共享/临时系统字体；无HWND测量后Dispose释放本体资源，字体变化经标准布局重新测量。仅此测量资源衔接，完整条带/项目Font及Renderer/overflow另列。 |
| ✅ | P1 / ToolStrip-05b | 批次021：父级DefaultLayout统一定位，Dock内部事务按实际方向/几何提交并在完成后DockChanged；只读标准Orientation消费Dock及固定横竖LayoutStyle，已有host按方向排列。直属/嵌套/取消Dock有参考；完整Style事件、overflow/所有item测量另列。 |
| ✅ | P1 / ToolStrip-05a | 批次020：现有item布局迁到标准OnLayout；Form只对直属strip定位，嵌套strip不按Form重排，构造期items未初始化时可安全进入布局。 仅该事务接入范围代码完成，不代表完整容器算法/平台验收。 |
| ⬜ | P1 / ToolStrip-06 | 实现 Alt/助记键、F10、方向键、Tab、Esc、快捷键分发、焦点还原与禁用项跳过；打开下拉菜单后在同级项间切换不能失去输入模式。 |
| ⬜ | P1 / ToolStrip-07 | 实现工具提示、ImageKey/ImageIndex、RTL、DPI 和可访问性；自定义 Renderer/Paint 扩展与原生默认外观分别列入验收，不能忽略绘制 API 分母。 |
| 🟡 | P2 / ToolStrip-08 | 已有原生控件宿主组合和popup菜单，原全待办更正；022有效字体测量复用Control资源。完整宿主双向同步/资源/焦点仍缺，不为单一HWND破坏TextBox/ComboBox集成。 |
| ⬜ | P2 / ToolStrip-09 | Linux 用 GtkBox/GtkButton/GtkToggleButton/GtkEntry/GtkDropDown/GtkProgressBar/GtkSeparator 原生组合；GTK4 无旧 GtkToolbar，overflow 由原生 GtkMenuButton/GtkPopover 承载同一逻辑 item。 |
| ⬜ | P3 / ToolStrip-10 | macOS 兼容任意嵌入/Dock 时用 NSStackView/NSButton/NSTextField/NSPopUpButton/NSProgressIndicator 原生组合；NSToolbar 是窗口级工具栏，不能冒充所有 ToolStrip 布局。 |
| ⬜ | P1 / ToolStrip-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / ToolStrip-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI/非视觉组件写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / ToolStrip-V3 | 发布 win-x64 Native AOT，检查实际窗口/原生组件与本类型交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | 已有原生宿主组合与菜单实现，可增量补齐。特别保留 item 改变通知和宿主复用，避免推倒重写。 |
| Linux | GTK4 原生 widgets 组合；无 GtkToolbar 的一对一类，不等于必须自绘。Items 的 Owner、焦点/overflow 逻辑在兼容层实现。 |
| macOS | NSStackView 原生控件组合满足嵌入；经验证可将窗口顶部情形映射 NSToolbar，但不可改变 public API 的停靠和集合行为。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- 2026-10-16 · 批次022：05c测量使用有效字体与每控件HFONT，移除临时/父共享字体分支；继承变化经标准布局消费。08按已有真实宿主组合更正为部分，全部item契约/overflow/Renderer与平台仍缺，见[022](../batches/022.md)。

- 2026-10-16 · 批次021：父级DefaultLayout统一定位，Dock内部事务按实际方向/几何提交并在完成后DockChanged；只读标准Orientation消费Dock及固定横竖LayoutStyle，已有host按方向排列。直属/嵌套/取消Dock有参考；完整Style事件、overflow/所有item测量另列。 见[021](../batches/021.md)，父任务仍部分；020历史条带定位/无首选测量描述只代表当时状态。

- 2026-10-16 · 批次020：标准事务接入现有宿主布局并保留构造期安全；内部LayoutInParent只供直属Form条带消费，没有新增公共替代API。完整ToolStrip继承/溢出/Owner/双向同步未关闭。 见[020](../batches/020.md)；父任务保持部分完成。

- 2026-10-16 · 批次018必要集成修复：ControlCollection开始同步请求布局后，EnsureItemHosts在替换宿主时会重入并递归释放，首次全套测试出现栈溢出。现用重入保护及先移除旧映射再释放/创建，释放中不再重建宿主；原始失败日志保留，修复后既有测试通过。未扩展ToolStrip公开API，完整宿主双向同步、布局和Owner任务仍保持未完成。

- `ed64510`（2026-04-08）：改进下拉状态管理和菜单导航。
- `275c479`（2026-04-08）：文本布局与 DPI 支持调整。
- `83e9356`（2026-04-08）：菜单图像资源相关改动涉及宿主。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms ToolStrip 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.toolstrip?view=windowsdesktop-10.0)
- [GTK4 Box](https://docs.gtk.org/gtk4/class.Box.html)
- [GTK4 迁移：GtkToolbar 移除后采用 GtkBox 原生控件组合](https://docs.gtk.org/gtk4/migrating-3to4.html)
- [AppKit nstoolbar](https://developer.apple.com/documentation/appkit/nstoolbar)
