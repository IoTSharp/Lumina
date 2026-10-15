# ToolStripContainer

⬜ **当前状态：**无同名实现，不能通过 ToolStrip 本身的 Dock 能力抵扣此标准复合容器。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[ToolStripContainer API](../api/ToolStripContainer.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：同名标准类型尚未实现；内部近似功能不抵扣其公开契约。
- 前置与排序：ContainerControl、ToolStripPanel、ToolStripContentPanel、布局与拖放。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| ⬜ | P1 / ToolStripContainer-01 | 实现标准 ToolStripContainer 与 ContentPanel、TopToolStripPanel、BottomToolStripPanel、LeftToolStripPanel、RightToolStripPanel 只读子对象。 |
| ⬜ | P1 / ToolStripContainer-02 | 对齐 TopToolStripPanelVisible/BottomToolStripPanelVisible/LeftToolStripPanelVisible/RightToolStripPanelVisible、AutoSize、Padding、Size 默认和序列化。 |
| ⬜ | P1 / ToolStripContainer-03 | 只允许内容进入 ContentPanel、工具条进入合适 ToolStripPanel；直接 Controls 修改的标准限制与子对象 Owner/Parent 一致。 |
| ⬜ | P1 / ToolStripContainer-04 | 实现四边 panel 加 content 的布局优先级、动态显示/隐藏、行增加后的内容尺寸、RTL 与高 DPI；嵌套停靠不能无限触发 Layout。 |
| ⬜ | P1 / ToolStripContainer-05 | 连接 ToolStripPanel.Join、跨边拖动、行重排和 BeginDrag/EndDrag；拖动结束后 Items/Owner 不变且原生子控件不重建丢失编辑状态。 |
| ⬜ | P1 / ToolStripContainer-06 | 验证 AutoSize、GetPreferredSize、SuspendLayout/ResumeLayout、ContentPanel 子控件 Dock/Anchor 和自动滚动互相作用。 |
| ⬜ | P1 / ToolStripContainer-07 | VS 设计器静态序列化四边与内容属性，不额外创建五个子容器；复制/粘贴与 .resx 本地化往返仍正确。 |
| ⬜ | P2 / ToolStripContainer-08 | Windows 用五个真实子 HWND/通用布局，工具条继续 native item hosts；不引入单独 docking 框架。 |
| ⬜ | P2 / ToolStripContainer-09 | Linux 用外层 GtkGrid/GtkBox 组合四边 native containers + 中间原生内容容器；拖放只改变内部 parent 并保持托管标准结构。 |
| ⬜ | P3 / ToolStripContainer-10 | macOS 用 NSView/NSStackView 四边布局与内容 NSView；NSToolbar 只有窗口级单条工具栏语义，不等价 ToolStripContainer 四边容器。 |
| ⬜ | P1 / ToolStripContainer-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / ToolStripContainer-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI/非视觉组件写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / ToolStripContainer-V3 | 发布 win-x64 Native AOT，检查实际窗口/原生组件与本类型交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | 由标准子容器 HWND 组合；没有必须绑定的单一 Win32 类。 |
| Linux | GtkGrid/Box 原生组合，兼容层管理边/行，不新增公共 docking API。 |
| macOS | NSView/NSStackView 原生组合；保留四边布局要求，NSToolbar 只可作部分场景优化。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- 无同名类型的实现修改历史；不得将规划文档创建计入运行库实现完成。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms ToolStripContainer 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.toolstripcontainer?view=windowsdesktop-10.0)
- [GTK4 Grid](https://docs.gtk.org/gtk4/class.Grid.html)
- [AppKit nsstackview](https://developer.apple.com/documentation/appkit/nsstackview)
