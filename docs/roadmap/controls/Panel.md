# Panel

🟡 **当前状态：**已有 STATIC 原生子窗口、主题应用及 ContainerControlBase 子控件集合；没有独立的 ScrollableControl 继承层。属性存在不代表滚动、Anchor、布局挂起等语义已完成。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[Panel API](../api/Panel.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：[Panel.cs](../../../Lumina.Forms/Panel.cs)
- 前置与排序：Control、ScrollableControl；先于 FlowLayoutPanel、TableLayoutPanel、TabPage、SplitterPanel。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| ⬜ | P0 / Panel-01 | 将继承关系收敛到 Panel : ScrollableControl；Controls 使用兼容 Control.ControlCollection；同一控件转移父级、重复 Add、循环父级、SetChildIndex/Find/Remove/Clear 按官方基线处理。 |
| ⬜ | P0 / Panel-02 | 补齐 BorderStyle、AutoSize、AutoSizeMode、AutoScroll、AutoScrollMargin、AutoScrollMinSize、AutoScrollPosition；记录默认值、非法枚举和 handle 创建前后结果。 |
| ⬜ | P0 / Panel-03 | 打通 Dock、Anchor、Padding、Margin、DisplayRectangle、GetPreferredSize；SuspendLayout/ResumeLayout(true/false) 能延迟并合并布局，不能继续空实现。 |
| ⬜ | P1 / Panel-04 | 为 Controls.Add/Remove、可见性/边界/字体变化捕获 ControlAdded/ControlRemoved、ParentChanged、Layout、SizeChanged、Resize 顺序；移除不隐式 Dispose，父容器 Dispose 递归释放。 |
| ⬜ | P1 / Panel-05 | 验证 AutoSize GrowOnly/GrowAndShrink、嵌套滚动、右下锚定对滚动条的影响，以及 ScrollControlIntoView 后焦点仍正确。 |
| ⬜ | P1 / Panel-06 | 键盘不把 Panel 变为额外焦点站；Tab/Shift+Tab 穿过子控件，鼠标滚轮、触摸板和 RTL 滚动方向与基线对照。 |
| ⬜ | P1 / Panel-07 | 实现 BackgroundImage/BackgroundImageLayout、Paint、OnPaint/OnPaintBackground、Invalidate/Update 的实际消费；背景滚动、透明背景、用户派生绘制和父背景继承按基线核对，图像资源归属明确。 |
| ⬜ | P2 / Panel-08 | Windows 使用真实子 HWND，BorderStyle 对应窗口样式；由共享滚动层处理 WM_HSCROLL/WM_VSCROLL/WM_MOUSEWHEEL 和 SCROLLINFO；避免分别在各容器复制实现。 |
| ⬜ | P2 / Panel-09 | Linux 在 GtkScrolledWindow 内放 GtkFixed 容纳原生子控件；普通不滚动 Panel 可直用 GtkFixed。由托管兼容布局计算像素位置，不能用 GtkBox 的分配规则冒充 WinForms Dock。 |
| ⬜ | P3 / Panel-10 | macOS 用 NSView，滚动时 NSScrollView+documentView；将 WinForms 左上坐标转换为 flipped view，映射 contentInsets 和滚动偏移；原生边框外观允许系统差异。 |
| ⬜ | P1 / Panel-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / Panel-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI 写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / Panel-V3 | 发布 win-x64 Native AOT，检查实际窗口与本控件交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | STATIC/注册的子窗口 + 通用布局与滚动实现；Panel 自身没有可直接替代所有 WinForms 语义的单个 Win32 控件。 |
| Linux | GtkFixed + 按需 GtkScrolledWindow；原生子控件保持真实 widget。GtkFixed 的 absolute placement 不能自动提供 WinForms Anchor。 |
| macOS | NSView + 按需 NSScrollView。NSView frame 与 documentView 大小共同表达 DisplayRectangle；不要把 AppKit 自适应约束直接当 WinForms 布局算法。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- `7d38999`（2026-04-08）：涉及 Panel 的深色主题应用调整。
- `ce86ab5`（2026-03-31）：接入 BackColor/ForeColor、继承主题和实时刷新。
- `3323e20`（2026-03-31）：容器与布局交互改进涉及本文件。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms Panel 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.panel?view=windowsdesktop-10.0)
- [GTK4 Fixed](https://docs.gtk.org/gtk4/class.Fixed.html)
- [AppKit nsview](https://developer.apple.com/documentation/appkit/nsview)
