# ToolStripContentPanel

⬜ **当前状态：**无标准实现；ToolStripContainer 中央内容区域尚无可验收契约。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[ToolStripContentPanel API](../api/ToolStripContentPanel.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：同名标准类型尚未实现；内部近似功能不抵扣其公开契约。
- 前置与排序：Panel、ToolStripContainer、渲染兼容与布局。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| ⬜ | P1 / ToolStripContentPanel-01 | 实现标准 ToolStripContentPanel : Panel，ContentPanel 对象由所属 ToolStripContainer 建立与保持，校正构造和设计器 content 序列化契约。 |
| ⬜ | P1 / ToolStripContentPanel-02 | 补 RenderMode、Renderer、Load 以及 RendererChanged 等标准表面，先以固定 API 清单核对公开成员，避免把 ToolStrip 的属性整套误搬。 |
| ⬜ | P1 / ToolStripContentPanel-03 | 实现 BackColor/ForeColor/BackgroundImage/Padding 与父容器主题继承；显式颜色和 Renderer 绘制优先级对照官方。 |
| ⬜ | P1 / ToolStripContentPanel-04 | 实现 Controls、Dock/Anchor、AutoScroll 及 DisplayRectangle；四边工具条改变后中央区域边界只由父容器统一计算。 |
| ⬜ | P1 / ToolStripContentPanel-05 | 校准 Load、HandleCreated、VisibleChanged、Layout 的真实顺序；内容重挂载和句柄重建不重复初始化业务数据。 |
| ⬜ | P1 / ToolStripContentPanel-06 | 补 Paint/背景绘制及 renderer 回调扩展，记录 Linux/macOS 的 System.Drawing 绘图契约阻塞，不能从分母偷偷删除。 |
| ⬜ | P1 / ToolStripContentPanel-07 | 验证键盘导航、验证、嵌套业务控件可访问性和设计器将子控件写在 ContentPanel.Controls 的往返结果。 |
| ⬜ | P2 / ToolStripContentPanel-08 | Windows 使用 Panel 子 HWND，调用已有布局/滚动与主题实现；RenderMode 的显示应实际生效。 |
| ⬜ | P2 / ToolStripContentPanel-09 | Linux 用 GtkFixed/按需 GtkScrolledWindow 原生内容容器，保持 child widgets；外观映射平台主题，自定义 Renderer 是单独兼容任务。 |
| ⬜ | P3 / ToolStripContentPanel-10 | macOS 用 NSView/NSScrollView 承载内容，父级安排 bounds；Renderer 自定义绘图须先解决跨平台绘图类型，不能用默认背景假称完成。 |
| ⬜ | P1 / ToolStripContentPanel-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / ToolStripContentPanel-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI/非视觉组件写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / ToolStripContentPanel-V3 | 发布 win-x64 Native AOT，检查实际窗口/原生组件与本类型交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | 复用 Panel HWND 与主题渲染；不另造内容宿主框架。 |
| Linux | GtkFixed/ScrolledWindow，按 WinForms 布局结果安排原生控件。 |
| macOS | NSView/NSScrollView；内容区不同于 NSToolbar 本身。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- 无同名类型的实现修改历史；不得将规划文档创建计入运行库实现完成。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms ToolStripContentPanel 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.toolstripcontentpanel?view=windowsdesktop-10.0)
- [GTK4 Fixed](https://docs.gtk.org/gtk4/class.Fixed.html)
- [AppKit nsview](https://developer.apple.com/documentation/appkit/nsview)
