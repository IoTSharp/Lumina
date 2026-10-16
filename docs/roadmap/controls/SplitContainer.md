# SplitContainer

🟡 **当前状态：**已有拖动、两面板、最小大小、折叠、FixedPanel 和 ISupportInitialize；Panel1/Panel2 公开类型为 Panel，SplitterMoving 使用 SplitterEventHandler，和标准 SplitterPanel/可取消事件类型不一致。020事务与021默认布局必要衔接已完成明确子范围，完整算法与控件仍未完成。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[SplitContainer API](../api/SplitContainer.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：[SplitContainer.cs](../../../Lumina.Forms/SplitContainer.cs)
- 前置与排序：ContainerControl、SplitterPanel、布局/输入捕获。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| ⬜ | P0 / SplitContainer-01 | 028探针确认现有ContainerControlBase : Control不进入IContainerControl活动投影（参考多出split:null），此真实差异尚未修复；校正标准继承关系与 Panel1/Panel2 的 SplitterPanel 返回类型，保证只读固定两面板，外部不能把任意子控件直接加入 SplitContainer.Controls。 |
| ⬜ | P0 / SplitContainer-02 | 将 SplitterMoving 对齐 SplitterCancelEventHandler/SplitterCancelEventArgs，支持 Cancel；SplitterMoved 保持标准 SplitterEventHandler，处理 MouseDown→Moving→Moved 时序。 |
| ⬜ | P0 / SplitContainer-03 | 对齐 SplitterDistance/Width/Increment、Panel1MinSize/Panel2MinSize、Orientation、FixedPanel、IsSplitterFixed、BorderStyle 的默认、异常与范围语义。 |
| ⬜ | P1 / SplitContainer-04 | BeginInit/EndInit 期间允许设计器先赋值再调尺寸，结束时统一校验；不能用永久 clamp 隐藏与官方不同的异常。 |
| 🟡 | P1 / SplitContainer-05 | 020事务05a、021正常双轴比例05b完成；240/90→260保持参考97/101/159，原五行比例差异已修。完整折叠保存/还原、最小尺寸/RTL/边界及标准SplitterPanel仍缺。 |
| ✅ | P1 / SplitContainer-05b | 批次021：正常Vertical/Horizontal尺寸变化使用总轴长/Panel1比例并向下取整，连续缩放不重设已舍入比例；FixedPanel两侧、直接距离/暂停后的真实逐轴Bounds操作保留Panel2/Bounds延期参数。固定探针双方向相关观测及往返测试通过，完整折叠/min/RTL另列。 |
| ✅ | P1 / SplitContainer-05a | 批次020：标准OnLayout暂停两panel、提交几何后恢复再发自身事件；直接SplitterDistance仍即时改变panel，复用Control暂停状态保留Panel2/Bounds延期参数。 仅该事务接入范围代码完成，不代表完整容器算法/平台验收。 |
| ⬜ | P1 / SplitContainer-06 | 拖动过程支持鼠标捕获、Esc 取消、失去捕获、边界限制、SplitterMoving 中修改 SplitX/SplitY 和 Cancel；仅 MouseUp 触发最终 Moved。 |
| ⬜ | P1 / SplitContainer-07 | 实现键盘聚焦分隔条、方向键/Tab 导航、IsSplitterFixed 后键盘与鼠标一致；RTL 与多 DPI 下距离坐标一致。 |
| ⬜ | P2 / SplitContainer-08 | Windows 用子 HWND 两面板 + 原生鼠标捕获/光标；分隔条为托管布局区域，保留现有实现并补语义，无须新建绘制框架。 |
| ⬜ | P2 / SplitContainer-09 | Linux 用 GtkPaned 承载两原生面板；position/resizable/shrink 属性不足以直接等价 FixedPanel/MinSize，兼容层负责策略和可取消拖动前置。 |
| ⬜ | P3 / SplitContainer-10 | macOS 用 NSSplitView + delegate 约束 min/max/proposed position，divider 拖动与托管 Moving 取消统一；系统 divider 宽度与 SplitterWidth 精确设定差异需实测。 |
| ⬜ | P1 / SplitContainer-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / SplitContainer-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI 写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / SplitContainer-V3 | 发布 win-x64 Native AOT，检查实际窗口与本控件交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | 子 HWND 组合；现有拖动实现可增量改正，优先修公开类型和取消事件。 |
| Linux | GtkPaned，start/end child 为原生容器；gtk_paned 的 handle 交互和分隔厚度不等同 WinForms。 |
| macOS | NSSplitView + delegate，折叠/约束/布局需托管状态参与，不能只同步 position。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- 2026-10-16 · 批次028：本控件代码未改；container-lookup记录现有ContainerControlBase : Control不具标准ContainerControl身份，活动投影少split:null。保留严格失败并归01标准继承前置；基类导航实现不关闭SplitContainer导航或整个控件，见[028](../batches/028.md)。

- 2026-10-16 · 批次021：正常Vertical/Horizontal尺寸变化使用总轴长/Panel1比例并向下取整，连续缩放不重设已舍入比例；FixedPanel两侧、直接距离/暂停后的真实逐轴Bounds操作保留Panel2/Bounds延期参数。固定探针双方向相关观测及往返测试通过，完整折叠/min/RTL另列。 见[021](../batches/021.md)，父任务仍部分；020历史条带定位/无首选测量描述只代表当时状态。

- 2026-10-16 · 批次020：修复多余Suspend/Resume覆盖延期参数的问题，不硬替换事件参数；构造期Panel尚未初始化有保护。24场景对照保留5行比例几何差异；标准SplitterPanel/事件取消/完整算法不关闭。 见[020](../batches/020.md)；父任务保持部分完成。

- `7d7a67e`（2026-04-08）：Lumina.Forms 实现整合涉及拆分容器。
- `108ef2b`（2026-04-08）：接入 Win32 深色主题应用。
- `ce86ab5`（2026-03-31）：接入核心颜色与继承主题。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms SplitContainer 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.splitcontainer?view=windowsdesktop-10.0)
- [GTK4 Paned](https://docs.gtk.org/gtk4/class.Paned.html)
- [AppKit nssplitview](https://developer.apple.com/documentation/appkit/nssplitview)
