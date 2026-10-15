# StatusStrip

🟡 **当前状态：**已有 ToolStrip 原生 item 宿主、底部布局和状态标签的 Spring 分配等局部实现；SizingGrip 只有表面不足以证明实际窗口缩放命中行为。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[StatusStrip API](../api/StatusStrip.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：[StatusStrip.cs](../../../Lumina.Forms/StatusStrip.cs)
- 前置与排序：ToolStrip/ToolStripStatusLabel、Form sizing；排在常用 ToolStrip 后。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| ⬜ | P0 / StatusStrip-01 | 对齐 StatusStrip 默认 Dock/AutoSize/LayoutStyle/GripStyle/SizingGrip 与 Items 集合，保持 ToolStripStatusLabel 等实际返回类型。 |
| ⬜ | P0 / StatusStrip-02 | 验证 ToolStripStatusLabel.Text、Spring、BorderSides、BorderStyle、IsLink、LinkVisited、Image 与宿主双向状态；不是仅把文字复制到 Label。 |
| ⬜ | P1 / StatusStrip-03 | 实现 Spring 多标签剩余空间分配、AutoSize=false、Margin/Padding、超宽项目、RTL 与 DPI；允许工具条支持的其他 item 类型。 |
| ⬜ | P1 / StatusStrip-04 | 校准 strip.ItemClicked、item.Click 的触发和键鼠顺序，IsLink=true 的状态标签仍使用标准 item.Click，禁用/隐藏后不可意外激活；状态区不劫持编辑控件焦点。 |
| ⬜ | P1 / StatusStrip-05 | SizingGrip 的可见、命中区、最大化/不可缩放窗体、RTL、右键菜单与窗体拖动场景均验收，属性切换实时生效。 |
| ⬜ | P1 / StatusStrip-06 | 支持 ItemAdded/Removed、运行时 Text/Visible 变化后的重排，GetPreferredSize 和 LayoutCompleted 与父级 Dock 形成稳定布局事务。 |
| ⬜ | P1 / StatusStrip-07 | 补状态提示的可访问性 live 文本/角色/子项遍历、.resx 图片和本地化长文本；持续更新时不制造重复 Click 或 UI 线程回调。 |
| ⬜ | P2 / StatusStrip-08 | Windows 保留原生 Label/ProgressBar 等宿主组合；若使用 msctls_statusbar32 只覆盖证实可等价的简单 parts，不限制 ToolStripItems 任意组合。 |
| ⬜ | P2 / StatusStrip-09 | Linux 用底部 GtkBox + GtkLabel/GtkProgressBar 等 native widgets；GTK 的状态提示组件不能自动实现 ToolStripStatusLabel.Spring，窗口缩放由 compositor 允许的 native 路径处理。 |
| ⬜ | P3 / StatusStrip-10 | macOS 用窗口底部 NSStackView/NSTextField/NSProgressIndicator；AppKit NSStatusBar 是菜单栏状态区，不是窗口 StatusStrip，不能误用；右下 sizing grip 是平台差异项。 |
| ⬜ | P1 / StatusStrip-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / StatusStrip-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI/非视觉组件写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / StatusStrip-V3 | 发布 win-x64 Native AOT，检查实际窗口/原生组件与本类型交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | 现有 ToolStrip 原生子控件组合，StatusStrip 增加布局/缩放语义。 |
| Linux | GtkBox 与原生子 widgets；Wayland 下程序不能用 Win32 风格坐标随意控制全局窗口大小，sizing grip 需单列能力验证。 |
| macOS | NSStackView 在内容区底部；原生窗口 resize 区域由 AppKit 管理。不可用 NSStatusBar 替代。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- `cb090bc`（2026-04-08）：记事本与 UI 改进涉及状态栏。
- `7d7a67e`（2026-04-08）：初始实现整合涉及状态栏。
- `108ef2b`（2026-04-08）：深色主题应用重构。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms StatusStrip 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.statusstrip?view=windowsdesktop-10.0)
- [GTK4 Box](https://docs.gtk.org/gtk4/class.Box.html)
- [AppKit nsstackview](https://developer.apple.com/documentation/appkit/nsstackview)
