# ToolStripOverflow

⬜ **当前状态：**CanOverflow 已有属性，但无标准 ToolStripOverflow 类与可靠 overflow 状态/交互验收。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[ToolStripOverflow API](../api/ToolStripOverflow.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：同名标准类型尚未实现；内部近似功能不抵扣其公开契约。
- 前置与排序：ToolStripDropDown、ToolStripOverflowButton、ToolStripItem.Overflow/Placement。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| ⬜ | P1 / ToolStripOverflow-01 | 实现标准 ToolStripOverflow 及标准构造/OwnerItem 关联，由 parent ToolStrip 创建与管理；不让消费者改用仓库自创 overflow API。 |
| ⬜ | P1 / ToolStripOverflow-02 | 建立 Always/AsNeeded/Never 的 Overflow 与 Placement 状态；主条/overflow 项集合视图不复制 logical item，不改变真实 Owner。 |
| ⬜ | P1 / ToolStripOverflow-03 | 计算可用空间、overflow 按钮宽度、Alignment、隐藏项与分隔项；resize、DPI、RTL、AutoSize 和 item 文字变化触发重新布局。 |
| ⬜ | P1 / ToolStripOverflow-04 | 在 overflow 中保持按钮勾选、下拉菜单、TextBox/ComboBox/ProgressBar 的状态与事件双向同步，native host 重挂载不丢失选择。 |
| ⬜ | P1 / ToolStripOverflow-05 | 保留 Opening/Closing 可取消与父 ToolStrip.ItemClicked 的正确路由；点击一次不得因两个宿主重复订阅而触发两次。 |
| ⬜ | P1 / ToolStripOverflow-06 | 键盘导航可到达 overflow 按钮和内容，Esc/Tab 返回父条，禁用/隐藏项被跳过，焦点与可访问性 parent 同步。 |
| ⬜ | P1 / ToolStripOverflow-07 | 父条销毁、临时隐藏、打开时 resize 或 Remove 项时安全关闭与释放；ToolStripOverflow 不额外 Dispose 不属于它的业务 item。 |
| ⬜ | P2 / ToolStripOverflow-08 | Windows overflow 为 popup HWND/HMENU 子集，继续承载真实子控件；复用 DropDown 的 DPI/焦点/生命周期实现。 |
| ⬜ | P2 / ToolStripOverflow-09 | Linux 使用 GtkMenuButton + GtkPopover 原生溢出区域；GMenuModel 仅足够菜单子集，编辑控件需原生 widget host。 |
| ⬜ | P3 / ToolStripOverflow-10 | macOS 任意嵌入条的 overflow 用 NSPopover/NSPanel；NSToolbar 的系统 overflow 只能用于实际采用 NSToolbar 的子集，不能覆盖普通 ToolStrip。 |
| ⬜ | P1 / ToolStripOverflow-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / ToolStripOverflow-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI/非视觉组件写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / ToolStripOverflow-V3 | 发布 win-x64 Native AOT，检查实际窗口/原生组件与本类型交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | ToolStrip popup 窗口与标准 overflow 按钮，布局数据源保持原 Items。 |
| Linux | GtkMenuButton/Popover 原生组合，主条与溢出间移动 native widget 必须维护 parent/引用生命周期。 |
| macOS | NSPopover/NSPanel，或者实际 NSToolbar 的原生溢出；不能让 native 移动改变 public Owner。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- 无同名类型的实现修改历史；不得将规划文档创建计入运行库实现完成。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms ToolStripOverflow 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.toolstripoverflow?view=windowsdesktop-10.0)
- [GTK4 Popover](https://docs.gtk.org/gtk4/class.Popover.html)
- [AppKit nspopover](https://developer.apple.com/documentation/appkit/nspopover)
