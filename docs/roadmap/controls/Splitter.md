# Splitter

🟡 **当前状态：**已有 STATIC 分隔条、Dock、MinSize/MinExtra、SplitPosition 与拖动事件；旧式相邻 Dock 控件语义和键盘/取消路径未验收。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[Splitter API](../api/Splitter.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：[Splitter.cs](../../../Lumina.Forms/Splitter.cs)
- 前置与排序：Control、父容器 Dock 排序、鼠标捕获；属于历史兼容控件，排在 SplitContainer 后。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| ⬜ | P1 / Splitter-01 | 校准 BorderStyle、Dock、MinSize、MinExtra、SplitPosition 默认与非法值，尤其 Dock 只允许支持方向时的异常语义。 |
| ⬜ | P1 / Splitter-02 | 按父 Controls z-order 与 Dock 找到标准目标邻接控件；插入/删除/重排相邻控件后重新解析目标，不能固定绑定第一个兄弟。 |
| ⬜ | P1 / Splitter-03 | 实现 SplitterMoving/SplitterMoved 的标准 SplitterEventArgs 坐标和可修改拆分位置；不要把 SplitContainer 的 Cancel 事件类型错误套用。 |
| ⬜ | P1 / Splitter-04 | 拖动时实现鼠标捕获、Esc 恢复、MinSize/MinExtra 约束、边界条件与拖动结束事件；失焦/销毁期间安全终止。 |
| ⬜ | P1 / Splitter-05 | SplitPosition 程序赋值与鼠标拖动路径分别对照布局/事件顺序，目标大小变化后 getter 反映实际结果。 |
| ⬜ | P2 / Splitter-06 | 校准 TabStop、Cursor、RightToLeft、Enabled、Visible 以及高 DPI 分隔厚度；旧式行为与 SplitContainer 的键盘能力分别验收。 |
| ⬜ | P2 / Splitter-07 | 父级移除、Dispose、控件转移时解除事件和 native capture，不额外释放相邻业务控件。 |
| ⬜ | P2 / Splitter-08 | Windows 复用 STATIC 子 HWND、WM_LBUTTONDOWN/MOUSEMOVE/UP 与 SetCapture；邻接控件仍真实 HWND。 |
| ⬜ | P2 / Splitter-09 | Linux 无 GtkSplitter 单体等价；沿同一父原生容器使用原生 widget 命中/拖动行为调节兄弟尺寸。GtkPaned 需包裹兄弟并改变层次，必须证明 Parent 不泄漏变化后才可采用。 |
| ⬜ | P3 / Splitter-10 | macOS NSSplitView 需要拥有两侧 view，不能直接保持独立 Splitter 的父子语义；优先 NSView 分隔部件 + 原生 tracking，明确视觉与原生 NSSplitView 的差异。 |
| ⬜ | P1 / Splitter-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / Splitter-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI 写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / Splitter-V3 | 发布 win-x64 Native AOT，检查实际窗口与本控件交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | 已有 STATIC/SS_NOTIFY 与鼠标拖动；核心是标准 Dock 邻接目标和事件语义。 |
| Linux | 原生 GtkWidget 分隔命中区 + native siblings，或经验证的内部 GtkPaned 组合；不存在单个一对一控件。 |
| macOS | NSView 分隔组件使用 AppKit 输入/光标，或内部 NSSplitView 组合；不得把原生父子差异暴露到 public Parent。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- `7d7a67e`（2026-04-08）：加入/整合 Splitter 初始实现；该文件只有这一条可见修改历史。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms Splitter 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.splitter?view=windowsdesktop-10.0)
- [GTK4 Paned](https://docs.gtk.org/gtk4/class.Paned.html)
- [AppKit nssplitview](https://developer.apple.com/documentation/appkit/nssplitview)
