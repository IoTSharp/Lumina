# MenuStrip

🟡 **当前状态：**已有顶层菜单项宿主、原生 popup、同级导航和图文测量；当前顶层项含自绘逻辑，并非全部采用系统菜单栏绘制。MDI 合并与完整激活事件尚无验收。020已完成标准布局事务入口衔接，完整算法与控件仍未完成。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[MenuStrip API](../api/MenuStrip.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：[MenuStrip.cs](../../../Lumina.Forms/MenuStrip.cs)
- 前置与排序：ToolStrip、ToolStripMenuItem/DropDown、Form.MainMenuStrip、快捷键与 MDI。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| 🟡 | P0 / MenuStrip-01 | 020完成01a标准布局入口及直属Form菜单定位衔接；标准构造/Items/Owner/Dock/AutoSize/GripStyle默认、MainMenuStrip绑定/替换/Dispose注册仍待对齐。 |
| ✅ | P0 / MenuStrip-01a | 批次020：现有菜单布局通过标准OnLayout及内部直属Form定位入口执行；暂停延期与直属Form定位有回归；嵌套strip遵守父级约束，本批嵌套定位fixture仅直接覆盖ToolStrip。 仅该事务接入范围代码完成，不代表完整容器算法/平台验收。 |
| ⬜ | P0 / MenuStrip-02 | 完整传递 ToolStripMenuItem 的 ShortcutKeys/ShortcutKeyDisplayString/ShowShortcutKeys、CheckOnClick/Checked/CheckState、DropDownItems 和启用状态。 |
| ⬜ | P1 / MenuStrip-03 | 实现 MenuActivate/MenuDeactivate，Alt/F10 进入、Esc 逐层退出、左右同级切换、上下/Enter 选项与焦点还原；不依赖鼠标才能打开菜单。 |
| ⬜ | P1 / MenuStrip-04 | 动态 Items/DropDownItems 增删、文本/图像/勾选/可见性变化在已打开或下一次打开时准确同步；Click/ItemClicked 与关闭事件顺序捕获。 |
| ⬜ | P1 / MenuStrip-05 | 实现 MdiWindowListItem、AllowMerge、ToolStripManager.Merge/RevertMerge 相关场景；MdiClient 未完成时明确阻塞，不能单独标已支持。 |
| ⬜ | P1 / MenuStrip-06 | 核验助记键冲突、&& 转义、快捷键在 TextBox 等编辑控件聚焦时的优先级、禁用/隐藏父菜单下命令是否可触发。 |
| ⬜ | P1 / MenuStrip-07 | 覆盖 RTL、DPI、长标题、图像透明度、.resx 本地化和可访问性菜单层级；保留既有布局修复历史，装饰优化排在功能之后。 |
| ⬜ | P2 / MenuStrip-08 | Windows 可继续顶层原生宿主 + HMENU popup，或局部迁入 SetMenu/HMENU；任何替换先对照 Items/Renderer、焦点和主题，不为单一实现推翻已验证行为。 |
| ⬜ | P2 / MenuStrip-09 | Linux 使用 GtkPopoverMenuBar/GMenuModel/GAction；GAction 状态与 Checked/Enabled 双向同步。GtkPopoverMenuBar 的菜单栏只接受菜单模型，ToolStrip 任意嵌入项需原生组合并单列限制。 |
| ⬜ | P3 / MenuStrip-10 | macOS 首选 NSMenu/NSMenuItem 接入应用主菜单，按活动窗口切换命令；WinForms 可嵌入/多条 MenuStrip 与 macOS 全局菜单不等价，明确 portable 范围并验证，不暗中忽略 Dock。 |
| ⬜ | P1 / MenuStrip-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / MenuStrip-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI/非视觉组件写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / MenuStrip-V3 | 发布 win-x64 Native AOT，检查实际窗口/原生组件与本类型交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | 现有顶层 MenuStripItemHost 会处理绘制/hover，popup 复用原生菜单。是否采用 HMENU 主菜单应由兼容差异驱动。 |
| Linux | GtkPopoverMenuBar + GMenuModel/GAction，原生菜单语义与 Windows accelerator 模型不同，需兼容快捷键路由。 |
| macOS | NSMenu 全局菜单原生；任意嵌入的 MenuStrip 可用原生按钮/弹出菜单组合，但必须标明这与系统全局菜单的交互差异。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- 2026-10-16 · 批次020：保留既有菜单测量与宿主，只校正标准事务/直属Form衔接；MDI合并/完整激活/默认/资源和平台仍缺。 见[020](../batches/020.md)；父任务保持部分完成。

- `b8da9a5`（2026-04-08）：调整菜单图像与文本间距。
- `573df6c`（2026-04-08）：进一步调整图文间距。
- `a1f0c5d`（2026-04-08）：调整水平 padding 与布局。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms MenuStrip 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.menustrip?view=windowsdesktop-10.0)
- [GTK4 PopoverMenuBar](https://docs.gtk.org/gtk4/class.PopoverMenuBar.html)
- [GTK4 迁移：旧 GtkMenu/GtkMenuBar/GtkMenuItem 已移除](https://docs.gtk.org/gtk4/migrating-3to4.html)
- [AppKit nsmenu](https://developer.apple.com/documentation/appkit/nsmenu)
