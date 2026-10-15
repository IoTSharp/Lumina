# FlowLayoutPanel

🟡 **当前状态：**已有四方向排列、换行和 Margin/Padding 计算；缺 FlowBreak 扩展属性与标准 FlowLayoutSettings，布局算法仍需官方对照。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[FlowLayoutPanel API](../api/FlowLayoutPanel.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：[FlowLayoutPanel.cs](../../../Lumina.Forms/FlowLayoutPanel.cs)
- 前置与排序：Panel/ScrollableControl、GetPreferredSize、布局事务。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| ⬜ | P0 / FlowLayoutPanel-01 | 实现 LayoutSettings/FlowLayoutSettings、GetFlowBreak/SetFlowBreak/IExtenderProvider；保持设计器生成 SetFlowBreak(child, true) 原样可编译。 |
| ⬜ | P0 / FlowLayoutPanel-02 | 对齐 FlowDirection=LeftToRight、WrapContents=true、AutoSize/AutoSizeMode、AutoScroll；属性赋值后均应在正确时点请求布局。 |
| ⬜ | P1 / FlowLayoutPanel-03 | 逐方向校验 LeftToRight/RightToLeft/TopDown/BottomUp 和 RightToLeft 组合；修正现有三元条件优先级造成非预期换行的风险。 |
| ⬜ | P1 / FlowLayoutPanel-04 | 支持每个子控件 Margin、容器 Padding、FlowBreak、Visible=false、Dock/Anchor 在行/列内的标准约束，避免简单顺序 SetBounds 视作完成。 |
| ⬜ | P1 / FlowLayoutPanel-05 | 实现 AutoSize/首选大小测量、跨行最大尺寸、边界刚好容纳/差一个像素、超大首项；滚动范围从真实测量结果计算。 |
| ⬜ | P1 / FlowLayoutPanel-06 | 处理 Controls 增删/重排、字体与 DPI 改变、子控件 AutoSize 更新；SuspendLayout 后一次性重排并稳定 Layout 事件数量。 |
| ⬜ | P1 / FlowLayoutPanel-07 | 保留 TabIndex 与视觉排列不同的键盘顺序；子控件焦点、验证、鼠标事件不被容器吞掉。 |
| ⬜ | P2 / FlowLayoutPanel-08 | Windows 使用真实子 HWND，托管 flow 算法计算后批量 SetWindowPos；勿引入独立绘制控件树。 |
| ⬜ | P2 / FlowLayoutPanel-09 | Linux 优先 GtkFixed + 共享 flow 算法；GtkFlowBox 可作为原型对照，但其 selection、均分/行宽规则不能直接取代 WinForms FlowLayoutPanel。 |
| ⬜ | P3 / FlowLayoutPanel-10 | macOS 用 NSView 承载原生 children 并共享 flow 算法；NSStackView 单轴排列不提供 WinForms 换行，不应宣称可一对一替换。 |
| ⬜ | P1 / FlowLayoutPanel-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / FlowLayoutPanel-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI 写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / FlowLayoutPanel-V3 | 发布 win-x64 Native AOT，检查实际窗口与本控件交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | Panel 子 HWND + 托管 flow 布局；保留 HWND 级焦点与输入。 |
| Linux | GtkFixed 能准确落位 native children；GtkFlowBox 是真实原生组件，但有选择和行列分配语义差异，需先证明等价才采用。 |
| macOS | NSView + 原生控件组合；NSStackView 只适用于不换行子集，完整换行由兼容布局计算 frame。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- `3323e20`（2026-03-31）：改进 flow 方向与布局交互。
- `52f5662`（2026-03-31）：加入基础布局控件。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms FlowLayoutPanel 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.flowlayoutpanel?view=windowsdesktop-10.0)
- [GTK4 FlowBox](https://docs.gtk.org/gtk4/class.FlowBox.html)
- [AppKit nsstackview](https://developer.apple.com/documentation/appkit/nsstackview)
