# SplitterPanel

⬜ **当前状态：**无标准 SplitterPanel。现有 SplitContainer 用普通 Panel 暴露 Panel1/Panel2，造成 API 返回类型和受限属性行为不匹配。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[SplitterPanel API](../api/SplitterPanel.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：同名标准类型尚未实现；不可用历史内部容器名称抵扣此类型。
- 前置与排序：Panel、SplitContainer；与 SplitContainer 的 P0 返回类型修正一起完成。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| ⬜ | P0 / SplitterPanel-01 | 新增标准 sealed SplitterPanel 类型及标准构造/所属 SplitContainer 语义；Panel1/Panel2 返回正确类型，不暴露新的替代工厂。 |
| ⬜ | P0 / SplitterPanel-02 | 对齐 SplitterPanel 的 Parent、Dock、Location、Width/Height、Size 等受限属性的可见性/设值行为，以 .NET10 公开基线和运行探针为准。 |
| ⬜ | P1 / SplitterPanel-03 | 支持 Controls、Padding、AutoScroll、AutoScrollMargin/MinSize、BackColor/ForeColor/Font 等正常 Panel 能力，不因属性受限把子容器功能裁掉。 |
| ⬜ | P1 / SplitterPanel-04 | 关联 Panel1MinSize/Panel2MinSize、Collapsed、SplitterDistance；布局只由所属 SplitContainer 提交，外部赋值按标准拒绝或处理。 |
| ⬜ | P1 / SplitterPanel-05 | 保留子控件 TabIndex/验证/焦点转移，折叠面板时转移活动控件，展开恢复后的事件顺序按官方验证。 |
| ⬜ | P1 / SplitterPanel-06 | 双面板静态 Designer 嵌套赋值可编译并 round-trip；Content 序列化保留 panel.Controls 而不重复创建面板。 |
| ⬜ | P1 / SplitterPanel-07 | 明确外部 Dispose 面板、移除/重设 Parent、父容器释放、重复 Dispose 的标准行为和资源归属。 |
| ⬜ | P2 / SplitterPanel-08 | Windows 用真实子 HWND 并共享 Panel 滚动/布局；不要为该类型另做独立消息循环。 |
| ⬜ | P2 / SplitterPanel-09 | Linux 对应 GtkPaned 的 start/end child 原生容器；native parent 为 GtkPaned，托管 Parent 保持 SplitContainer，不泄漏包装 widget。 |
| ⬜ | P3 / SplitterPanel-10 | macOS 对应 NSSplitView 的两个原生子 NSView/NSScrollView；约束和折叠由 SplitContainer 统一处理，面板本身不另建 divider。 |
| ⬜ | P1 / SplitterPanel-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / SplitterPanel-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI 写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / SplitterPanel-V3 | 发布 win-x64 Native AOT，检查实际窗口与本控件交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | SplitContainer 内两固定子面板 HWND，复用 Panel 实现。 |
| Linux | GtkPaned 的两原生容器；滚动时 GtkScrolledWindow，业务子控件仍真实 widgets。 |
| macOS | NSSplitView 两侧 NSView；按需 NSScrollView，不引入新的公共面板抽象。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- 无同名类型的实现修改历史；不得将规划文档创建计入运行库实现完成。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms SplitterPanel 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.splitterpanel?view=windowsdesktop-10.0)
- [GTK4 Paned](https://docs.gtk.org/gtk4/class.Paned.html)
- [AppKit nssplitview](https://developer.apple.com/documentation/appkit/nssplitview)
