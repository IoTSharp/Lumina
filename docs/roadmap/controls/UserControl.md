# UserControl

🟡 **当前状态：**已有 STATIC 容器和 AutoScaleMode/AutoScaleDimensions 存值；缺标准 ContainerControl 继承和完整 Load/验证/活动控件语义。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[UserControl API](../api/UserControl.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：[UserControl.cs](../../../Lumina.Forms/UserControl.cs)
- 前置与排序：ContainerControl、布局/焦点/缩放；复用窗体静态设计器与 .resx 链路。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| ⬜ | P0 / UserControl-01 | 改为标准 UserControl : ContainerControl；保留消费者无参构造、partial + InitializeComponent 和 IDisposable/Component 容器模式。 |
| ⬜ | P0 / UserControl-02 | 补齐 AutoSize/AutoSizeMode、AutoScaleMode、AutoScaleDimensions、ActiveControl、AutoValidate、BorderStyle；默认值与 DesignerSerializationVisibility 取固定基线。 |
| ⬜ | P0 / UserControl-03 | 实现 Load 在第一次展示/句柄生命周期的真实触发边界；对照 HandleCreated/ParentChanged/VisibleChanged/Load，避免每次 Visible=true 重复 Load。 |
| ⬜ | P1 / UserControl-04 | 实现 GetPreferredSize、MinimumSize/MaximumSize、嵌套 Dock/Anchor、AutoSize 容器反馈；处理用户派生控件覆盖受保护方法的调用顺序。 |
| ⬜ | P1 / UserControl-05 | 验证 Tab/Shift+Tab、助记键、Enter/Escape、Validating 取消以及将焦点设为内部子控件；活动链应穿透嵌套 UserControl。 |
| ⬜ | P1 / UserControl-06 | 校准 AutoScaleMode.Font/Dpi/Inherit/None、父容器字体继承、独立设计基准尺寸和跨 DPI 移动，无二次缩放。 |
| ⬜ | P1 / UserControl-07 | 实现 ControlAdded/Removed、BindingContextChanged 和用户数据绑定传播；Controls.Clear 与 Dispose 的资源归属分别验收。 |
| ⬜ | P2 / UserControl-08 | Windows 继续真实子 HWND，明确容器窗口样式与透明背景转发；不需要独立视觉框架。 |
| ⬜ | P2 / UserControl-09 | Linux 用 GtkFixed 承载同样的 native child widgets；由共享 ContainerControl 执行布局/验证/缩放，避免 GtkWidget scale-factor 再次缩放业务坐标。 |
| ⬜ | P3 / UserControl-10 | macOS 用可翻转 NSView 组合原生控件；key-view loop 与可访问性 children 暴露内部子控件，拒绝把整个 UserControl 只暴露成一幅图。 |
| ⬜ | P1 / UserControl-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / UserControl-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI 写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / UserControl-V3 | 发布 win-x64 Native AOT，检查实际窗口与本控件交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | 子 HWND + Control/ContainerControl 通用能力；UserControl 本质为复合容器，不是特定 comctl32 控件。 |
| Linux | GtkFixed/按需要的原生容器组合。静态设计器生成代码继续运行在相同 public API 上。 |
| macOS | NSView 承载原生子控件，并通过 responder/key-view 链保持组合控件交互。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- `70a08e7`（2026-04-09）：加入 UserControl 自动缩放相关属性。
- `b8a98eb`（2026-04-09）：加入 UserControl 与资源/主题管理支持。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms UserControl 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.usercontrol?view=windowsdesktop-10.0)
- [GTK4 Fixed](https://docs.gtk.org/gtk4/class.Fixed.html)
- [AppKit nsview](https://developer.apple.com/documentation/appkit/nsview)
