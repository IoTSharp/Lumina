# TabControl

🟡 **当前状态：**已有SysTabControl32、选中页同步/SelectedIndex/SelectedTab和部分TCN路径；020布局/022主题范围保留。029补05a批量验证递归入口；标准TabPages集合、可取消选择、完整键盘/焦点及平台仍未完成。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[TabControl API](../api/TabControl.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：[TabControl.cs](../../../Lumina.Forms/TabControl.cs)
- 前置与排序：Control/父子集合、TabPage；容器可见性/焦点与图像支持。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| 🟡 | P0 / TabControl-01 | 已有SelectedIndex/SelectedTab及部分原生同步，纠正原全待办遗漏；标准TabPageCollection/TabPages、Controls类型约束、加入/移除选中页/空集合/越界和句柄前赋值全部边界仍缺。 |
| ⬜ | P0 / TabControl-02 | 补齐 Alignment、Appearance、DrawMode、HotTrack、ImageList、ItemSize、Multiline、Padding、SizeMode、ShowToolTips、RightToLeftLayout 和事件参数类型。 |
| ⬜ | P1 / TabControl-03 | 实现 Deselecting/Deselected/Selecting/Selected 的可取消事件链，记录 SelectedIndexChanged 的准确相对顺序；鼠标、键盘、程序赋值三条路径一致。 |
| ⬜ | P1 / TabControl-04 | 原生选项卡点击时在 TCN_SELCHANGING 阶段允许 Cancel；取消后原页 Visible、焦点、SelectedIndex 和原生索引不能分叉。 |
| 🟡 | P1 / TabControl-05 | 029完成05a批量验证后代递归；GetTabRect、SelectTab/DeselectTab、焦点页导航/Ctrl+Tab/Ctrl+Shift+Tab/方向键完整契约仍待补齐；TabPage静态查询另行验收。 |
| ✅ | P1 / TabControl-05a | 批次029：ShouldPerformContainerValidation覆盖允许批量验证进入TabPage及其后代，不依赖TabControl自身ContainerControl style。普通递归/ImmediateChildren及页面内leaf顺序有回归；不代表切页取消或全部派生默认样式完成。 |
| ⬜ | P1 / TabControl-06 | 支持 ImageIndex/ImageKey、文字运行时变更、页工具提示和 DrawItem；绘制扩展是兼容能力，非跨平台全自绘默认路线。 |
| 🟡 | P1 / TabControl-07 | 020完成07a标准布局入口衔接；完整DisplayRectangle/页边界/RTL/多行垂直/DPI/隐藏和重建仍待对照，切页不得重复建销业务控件。 |
| ✅ | P1 / TabControl-07a | 批次020：现有页布局迁到标准OnLayout，复用Control暂停/恢复并移除重复Bounds布局；独立事务回归通过。 仅该事务接入范围代码完成，不代表完整容器算法/平台验收。 |
| 🟡 | P2 / TabControl-08 | 已有SysTabControl32、TCM插入/选择/矩形和部分TCN同步，原全待办更正；022完成08a主题默认隔离。完整native item/lParam归属、删除/可取消选择与资源寿命仍待验证。 |
| ✅ | P2 / TabControl-08a | 批次022主题使用内部Surface默认槽，不再向public BackColor/ForeColor回写或清空TabPage显式颜色。主题刷新/局部状态及页面颜色保留有独立回归；完整Tab绘制/集合/事件和平台仍归父项。 |
| ⬜ | P2 / TabControl-09 | Linux 使用 GtkNotebook + 每页原生容器；switch-page 默认非 WinForms 全套可取消链，需在动作提交前路由选择与取消。OwnerDraw/多行标签无完整原生等价项，单列。 |
| ⬜ | P3 / TabControl-10 | macOS 使用 NSTabView/NSTabViewItem 与 delegate shouldSelectTabViewItem；多行/垂直排列、OwnerDraw 标签和工具提示差异逐项记录，禁止假称全量等价。 |
| ⬜ | P1 / TabControl-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / TabControl-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI 写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / TabControl-V3 | 发布 win-x64 Native AOT，检查实际窗口与本控件交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | 已用 SysTabControl32；重点补 TCN_SELCHANGING、完整样式/图像和集合生命周期。 |
| Linux | GtkNotebook 原生页签；不支持的外观或自定义绘制契约必须列例外，而非把支持 switch-page 当支持全部事件。 |
| macOS | NSTabView 与 delegate；NSView 页面承载原生 children，native selection 的 veto 和托管取消链统一。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- 2026-10-17 · 批次029：增加05a必要验证递归消费，保留可取消切页与导航缺口；按现有代码将01由全待办纠正为部分，未关闭集合/控件，见[029](../batches/029.md)。

- 2026-10-16 · 批次023：基础Control启用标准UserPaint后，本控件构造显式保留UserPaint=false，使现有系统窗口类过程及原生字体/绘制继续生效。仅属消息与绘制基础的必要衔接，不代表本控件完整Styles、Paint事件或视觉/输入兼容完成，见[023](../batches/023.md)。

- 2026-10-16 · 批次022：移除ApplyNativeColors公共属性写入，theme与显式页颜色分离；08a为必要消费子范围，08按既有TCM/TCN实现从全待办更正为部分。没有将基础Control字体/颜色契约当作完整TabControl兼容，见[022](../batches/022.md)。

- 2026-10-16 · 批次020：现有页定位消费标准OnLayout，完整页边界/选择事件/标准集合与原生切换仍待后续；此批只完成事务衔接。 见[020](../batches/020.md)；父任务保持部分完成。

- 2026-10-16 · 批次018集成：标准Control可见通知开始创建可见子树，暴露本地TabControl在无HWND时也提前切换TabPage.Visible的历史差异；官方SelectedIndex无句柄时只保存索引，读取DisplayRectangle才创建用于精确计算。现有布局测试改为核对实际DisplayRectangle与选中页Bounds一致，不再把本地4/21估计值当标准参考。未扩大本控件实现或关闭选择/布局任务；该差异继续留本页待办。

- `9d5165f`（2026-04-09）：扩展 TabControl 兼容功能。
- `f666aa6`（2026-04-08）：UI 和发布改进提交涉及本文件。
- `cb090bc`（2026-04-08）：记事本/UI 改进提交涉及本文件。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms TabControl 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.tabcontrol?view=windowsdesktop-10.0)
- [GTK4 Notebook](https://docs.gtk.org/gtk4/class.Notebook.html)
- [AppKit nstabview](https://developer.apple.com/documentation/appkit/nstabview)
