# GroupBox

🟡 **当前状态：**已有 BUTTON/BS_GROUPBOX、标题占位 DisplayRectangle、主题绘制。当前代码覆盖 WM_PAINT/WM_PRINTCLIENT 自行绘制边框标题，不能把所有视觉表现都记为系统原生绘制。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[GroupBox API](../api/GroupBox.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：[GroupBox.cs](../../../Lumina.Forms/GroupBox.cs)
- 前置与排序：Control 父子/布局与 Panel 级容器语义；分组焦点与 RadioButton。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| 🟡 | P0 / GroupBox-01 | 已继承018标准Controls集合；公开继承仍为历史ContainerControlBase，GroupBox : Control标准直接基类待校正，不要求消费者使用新基类。 |
| 🟡 | P0 / GroupBox-02 | 已有200×100及Padding=3默认和标题DisplayRectangle，022标题占位改用有效Font.Height；原全待办遗漏局部实现。完整Text/FlatStyle/AutoSize/AutoSizeMode/TabStop默认、首选测量及变化顺序仍待对照。 |
| 🟡 | P1 / GroupBox-03 | 已消费018标准Controls的Add/Remove/重排/父级转移；RadioButton自动互斥分组、组内禁用/隐藏和焦点导航仍待验证，不随集合基类关闭。 |
| ⬜ | P1 / GroupBox-04 | 实现文本 & 助记键、双 && 转义、Alt 导航到首个可选子控件；GroupBox 本身不得增加额外 Tab 焦点。 |
| ⬜ | P1 / GroupBox-05 | 对照 Layout/TextChanged/FontChanged/AutoSizeChanged 触发顺序，覆盖空标题、多行/超长标题、RTL 和容器缩小到零内容区域。 |
| ⬜ | P1 / GroupBox-06 | 核验 FlatStyle.System/Standard/Flat/Popup 与高对比、系统主题；当前自绘路径与原生路径分开记录，避免美化改变 WinForms 行为。 |
| 🟡 | P1 / GroupBox-07 | 022完成07a字体及显式颜色消费；完整Enabled/嵌套布局、标题遮挡、背景擦除闪烁、RTL/DPI仍待验收。 |
| ✅ | P1 / GroupBox-07a | 批次022标题度量使用有效Font.Height，绘制使用本控件独占HFONT；前景/父背景使用有效显式颜色链，默认theme不写公共属性。移除固定系统字体和吞异常回退；字体改变的DisplayRectangle及资源消费回归见022，仅此必要衔接完成。 |
| 🟡 | P2 / GroupBox-08 | 已有BUTTON/BS_GROUPBOX及WM_PAINT/WM_PRINTCLIENT主题补绘，原全待办更正为部分。完整系统主题/打印、HWND与可访问性、辅助技术仍待逐项验收，不把自绘称为全部系统原生绘制。 |
| ⬜ | P2 / GroupBox-09 | Linux 使用 GtkFrame + 原生标题 GtkLabel，child 为 GtkFixed；助记键关联首个可聚焦子控件。GTK frame 不提供 WinForms FlatStyle 全部外观。 |
| ⬜ | P3 / GroupBox-10 | macOS 使用 NSBox，contentView 承载原生子控件，title/boxType/borderType 映射；FlatStyle 枚举的逐像素等价不作为跨平台原生承诺。 |
| ⬜ | P1 / GroupBox-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / GroupBox-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI 写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / GroupBox-V3 | 发布 win-x64 Native AOT，检查实际窗口与本控件交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | 已有 BUTTON/BS_GROUPBOX，WM_PAINT 当前有自绘覆盖。后续优先保留系统控件语义并验证主题绘制。 |
| Linux | GtkFrame + GtkLabel + 原生容器；GTK4 不会自动实现 WinForms radio 分组与助记键选择逻辑。 |
| macOS | NSBox 的 title/contentView；原生 titlePosition 和边框视觉与 Windows 分开记录，功能场景仍需验收。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- 2026-10-16 · 批次022：完成07a有效字体/显式颜色消费，标题度量不再固定系统字体且不吞字体异常。按实际代码纠正01/02/03/08中遗漏标准Controls、已有默认/度量和Win32绘制的全待办状态；标准直接继承、全部默认/首选布局/焦点/打印/Designer与平台保持未完，见[022](../batches/022.md)。

- `cb090bc`（2026-04-08）：记事本与 UI 改进提交涉及本文件。
- `7d7a67e`（2026-04-08）：Lumina.Forms 初始实现整合涉及本文件。
- `bca1ffd`（2026-04-08）：修正 GroupBox/TabControl 主题应用。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms GroupBox 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.groupbox?view=windowsdesktop-10.0)
- [GTK4 Frame](https://docs.gtk.org/gtk4/class.Frame.html)
- [AppKit nsbox](https://developer.apple.com/documentation/appkit/nsbox)
