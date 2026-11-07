# UserControl

🟡 **当前状态：**031～037范围保留；038补02d标准AutoSize属性覆盖、事件公开和设计器声明，仍由共享ContainerControl/DefaultLayout执行布局。完整AutoSize/设计器、全部验证/键盘、AOT与平台语义仍缺，见[038](../batches/038.md)。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[UserControl API](../api/UserControl.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：[UserControl.cs](../../../Lumina.Forms/UserControl.cs)
- 前置与排序：ContainerControl、布局/焦点/缩放；复用窗体静态设计器与 .resx 链路。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| 🟡 | P0 / UserControl-01 | 031完成01a标准 `UserControl : ContainerControl`、STATIC子窗口和释放链；partial/InitializeComponent、完整Designer/resx和全部生命周期仍待。 |
| 🟡 | P0 / UserControl-02 | 031～035的02a～02c保留；038的02d补AutoSize属性/事件公开与设计器序列化声明。完整AutoSize、其余默认元数据和Designer往返仍待，完整02/04不随局部声明关闭。 |
| 🟡 | P0 / UserControl-03 | 032的03a已实现成功OnCreateControl后一次Load，重复Visible/重建不重复；035纠正父项仍全待办的历史状态。完整首次展示、嵌套创建/父级可见性、Designer与所有异常生命周期边界仍待对照。 |
| 🟡 | P1 / UserControl-04 | 已继承ContainerControl内容首选测量和基础MinMax/默认布局；035以明确数值模式验证有限AutoSize父反馈。完整嵌套Dock/Anchor、派生覆盖顺序/复杂反馈、缩放和Designer仍待，不由基类/局部布局用例关闭完整AutoSize。 |
| 🟡 | P1 / UserControl-05 | 037的05a补原生焦点入口与嵌套Tab/Shift+Tab、Prevent/Allow/CausesValidation限定组合；原全待办按实际实现改部分。完整助记键、Enter/Escape、HostedInWin32DialogManager、全部验证重入/输入和设备仍待。 |
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

### 已完成任务（历史台账）

以下原任务行从当前任务表迁入历史，ID、状态、明确范围与验证限制原样保留；现有导出工具仍读取这些行。✅ 仅表示定义范围代码完成，未完父项与平台验收仍在当前表。

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| ✅ | P0 / UserControl-02d | 批次038：按固定10.0.11补AutoSize标准override及Browsable/EditorBrowsable/DesignerSerializationVisibility声明，AutoSizeChanged订退转发同一基础事件槽。6项回归覆盖序列化属性、派生setter、单次通知、父布局完成状态和原异常。固定源码无UserControl独立OnLayout/GetPreferredSize覆盖，不新增另一套算法，不关闭完整02/04或Designer往返。 |
| ✅ | P0 / UserControl-02c | 批次035：AutoSizeMode修正为GrowAndShrink=0/GrowOnly=1，Control字段显式GrowOnly，UserControl已有构造/setter按名字保持正确无需改算法。10项数值/四类默认/元数据和实际基础/复合布局回归通过。旧二进制及整数持久化0/1需复核；完整02/04不关闭。 |
| ✅ | P0 / UserControl-02b | 批次034：DefaultSize=150×150，AutoSizeMode合法名字校验与父/自身AutoSize布局请求；BorderStyle校验、CreateParams边框位及UpdateStyles保持现有句柄。10项新增回归含真实HWND样式回读通过。范围不含AutoSizeMode枚举数值身份修正、完整AutoSize或Designer元数据；这些仍归02父项。 |
| ✅ | P0 / UserControl-01a | 批次031：UserControl改为标准ContainerControl派生，保留无参构造、STATIC类名/子窗口样式、主题应用和Component/IDisposable链。 |
| ✅ | P0 / UserControl-02a | 批次031：移除重复AutoScale存值属性，UserControl直接消费ContainerControl的ActiveControl/AutoValidate/AutoScale契约；不关闭完整默认值和设计器语义。 |
| ✅ | P0 / UserControl-03a | 批次032：增加标准Load/OnLoad，首次成功OnCreateControl后一次触发；重复Visible切换和句柄重建不重复，创建失败不伪造成功，处理器异常原样传播。 |
| ✅ | P1 / UserControl-05a | 批次037：OnMouseDown焦点不在内部时先Focus再事件；WM_SETFOCUS无活动项时选首个可选后代，并消费ValidationCancelled守卫。7项真实HWND回归覆盖嵌套首次/已有子焦点、鼠标事件顺序、三种验证组合及Tab双向活动链。完整05和AutoSize仍未完成。 |

### 变更记录


- 2026-11-07 · 批次039：UserControl.cs未改，继续继承有限Control IME路径；未以非Form根消息测试关闭HostedInWin32DialogManager、完整输入或AutoSize/共享约束，父任务保持原状态，见[039](../batches/039.md)。

- 2026-11-07 · 批次038：02d接入标准AutoSize覆盖与事件公开，复用共享状态/布局链。源码审计确认固定UserControl无独立OnLayout/GetPreferredSize算法，不为补属性而新增布局覆盖；完整AutoSize/复杂约束和Designer往返仍归02/04。检查结果见[038](../batches/038.md)。

- 2026-11-07 · 批次037：05a按固定10.0.11接入OnMouseDown/WM_SETFOCUS，7项真实HWND回归通过；完整HostedInWin32DialogManager、输入/验证/AutoSize、Designer/AOT/设备和平台仍待，见[037](../batches/037.md)。

- 2026-11-07 · 批次035：02c修复历史数值反置，并显式保留基础Control的GrowOnly默认；UserControl.cs按名字实现本已正确，本批不重复修改。03父项仍全待办遗漏032的03a，04遗漏已有内容测量/基础布局，按真实局部实现纠正为部分并保留剩余范围。旧持久化整数和已编译枚举常量须复核；按名字源码重编译保持名字语义。10项限定回归通过，不关闭完整AutoSize，见[035](../batches/035.md)。

- 2026-11-07 · 批次034：完成02b默认150×150、GrowOnly名字默认、AutoSizeMode合法值/布局通知和BorderStyle参数/真实HWND样式更新。CompatibilityEnums中AutoSizeMode数值身份与官方相反未修改，明确保留父项；完整AutoSize、Designer、AOT与平台仍待，见[034](../batches/034.md)。

- 2026-10-26 · 批次032：完成03a Load一次触发生命周期；完整Designer/resx、BorderStyle、验证/键盘、AOT和平台仍待，见[032](../batches/032.md)。

- 2026-10-26 · 批次031：完成01a标准ContainerControl继承和02a共享容器属性首范围；Load、BorderStyle、Designer/resx、键盘/验证和平台仍待，见[031](../batches/031.md)。

- `70a08e7`（2026-04-09）：加入 UserControl 自动缩放相关属性。
- `b8a98eb`（2026-04-09）：加入 UserControl 与资源/主题管理支持。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms UserControl 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.usercontrol?view=windowsdesktop-10.0)
- [GTK4 Fixed](https://docs.gtk.org/gtk4/class.Fixed.html)
- [AppKit nsview](https://developer.apple.com/documentation/appkit/nsview)
