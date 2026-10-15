# ToolStripOverflowButton

基线：2026-10-15 · Microsoft.WindowsDesktop.App.Ref 10.0.11。精确类型：`System.Windows.Forms.ToolStripOverflowButton : ToolStripDropDownButton`；兼容层采用对应的 `Lumina.Forms` 类型形状。实施阶段为 **P2：ToolStrip 布局与 overflow → P3：三平台验收**。

## 当前证据与依赖

⬜ 尚无 `Lumina.Forms/ToolStripOverflowButton.cs`。当前 [ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs) 设置 `CanOverflow=true`、`LayoutStyle=StackWithOverflow`，但布局沿一个方向累计子控件宽度，未建立 overflow 条目集合、按钮或 `Placement` 关系；这两个属性存在不等于溢出行为已经实现。[StatusStrip.cs](../../../Lumina.Forms/StatusStrip.cs) 默认关闭 overflow，应保留其标准差别。

这是 ToolStrip 创建并通过 `OverflowButton` 属性提供的标准条目，固定基线**没有公开构造器**。不应为了补类型而新增 `public ToolStripOverflowButton()`。先完成 [ToolStripItem](ToolStripItem.md)、[ToolStripDropDownButton](ToolStripDropDownButton.md)、[ToolStripOverflow](ToolStripOverflow.md)、[ToolStripControlHost](ToolStripControlHost.md) 和宿主布局。

全部公开属性、事件、方法与继承成员见 [精确 API 清单](../api/ToolStripOverflowButton.md)；共同计量与生命周期见 [基础任务](../foundations.md)。本页仅补真正遗漏的公开条目，不将私有绘制辅助对象另算公开控件。

## 按顺序执行的任务

✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已存在但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。

| 任务 | 阶段 | 状态 | 具体实现与验收条件 |
|---|---|---|---|
| ToolStripOverflowButton-001 | P2 | ⬜ | 提供正确继承、可见性和标准受保护覆盖点；由 ToolStrip 创建并公开只读 OverflowButton，未显示/显示/隐藏/重建后对象身份按真实 WinForms 比对，不新增公开构造器。 |
| ToolStripOverflowButton-002 | P2 | ⬜ | ToolStripItem.Overflow=Always/AsNeeded/Never、ToolStrip.CanOverflow、LayoutStyle 与可用宽高共同决定迁移；Always/Never 混合、零空间、刚好容纳和差一个像素分别验收，不只把右侧条目整体隐藏。 |
| ToolStripOverflowButton-003 | P2 | ⬜ | 按钮自身占用空间应纳入一次完整测量；显示/隐藏按钮、条目 Available/Visible/Size/AutoSize/Margin/Padding、Font/DPI 变化后稳定重排，避免按钮出现又消失的布局循环。 |
| ToolStripOverflowButton-004 | P2 | ⬜ | 条目 Owner 保持原 ToolStrip，Parent/Placement/IsOnOverflow/GetCurrentParent 与实际显示表面一致；溢出迁移保留同一条目及事件/绑定/Checked/输入值，窗口重新变宽可还原。 |
| ToolStripOverflowButton-005 | P2 | ⬜ | HasDropDownItems、DropDown/DropDownItems 与实际 overflow 内容一致；按基线检验系统维护内容的可写/只读边界，不能用一个任意用户集合替代溢出列表。 |
| ToolStripOverflowButton-006 | P2 | ⬜ | GetPreferredSize、SetBounds、DefaultMargin、RightToLeftAutoMirrorImage 及继承 ShowDropDownArrow/DisplayStyle/Text/Size 的元数据与行为；不要把普通 DropDownButton 的自动属性默认值直接套用。 |
| ToolStripOverflowButton-007 | P2 | ⬜ | 横/纵向工具栏、RTL、RightToLeftLayout、Alignment、拖拽换行与多屏 DPI 下按钮位置、箭头方向、命中区和菜单工作区翻转逐组合对照。 |
| ToolStripOverflowButton-008 | P2 | ⬜ | 鼠标、键盘方向/Enter/空格/Escape、助记键/辅助动作打开与关闭；DropDownOpening/Opened/Closed/DropDownItemClicked、Click/宿主 ItemClicked 的实际触发条件和顺序按基线，不能假设与普通按钮相同。 |
| ToolStripOverflowButton-009 | P2 | ⬜ | 取消 DropDown.Opening/Closing、弹出中调整窗体/修改集合/Dispose、条目恢复主条带、焦点恢复与鼠标捕获取消；不存在的 OverflowButton 专属事件不得自创。 |
| ToolStripOverflowButton-010 | P2 | ⬜ | TextBox/ComboBox/ProgressBar 等 ControlHost 溢出时仍可操作；公开 Control 对象不因迁移更换，用户输入不被单向 ApplyItemState 覆盖，验证取消与菜单关闭规则一致。 |
| ToolStripOverflowButton-011 | P2 | ⬜ | 菜单按钮辅助角色、Name、has-popup/expanded、当前溢出项关系和键盘导航；Owner Dispose/Remove/Clear 后清理旧原生子宿主、菜单句柄、订阅与 hook，不留下可点击的过期条目。 |
| ToolStripOverflowButton-012 | P3 | ⬜ | 同一静态 Designer 工具栏样本只通过调整窗口尺寸触发 overflow，保持 Demo 静态定义；双目标编译/VS 往返、AOT 实际展开/编辑/恢复/退出以及三平台资源与事件记录通过。 |
| ToolStripOverflowButton-CLOSE | P3 | ⬜ | API 清单中的剩余访问器、事件、方法/重载与受保护兼容点逐条关闭；Windows 的公开属性/公开事件/行为各≥98%，Linux/macOS 可移植三项各≥98%。签名匹配、按钮能显示或任务写完不能作为通过分子，普通业务迁移不得因实现困难被剔除。 |

## 三平台的原生实现

**Windows**：沿用 ToolStrip 的原生子控件宿主；溢出按钮可由原生 Button 承载，展开使用 ToolStripOverflow 的原生容器/菜单。普通命令可用 HMENU，但 HMENU 命令行不能直接承载任意 EDIT/COMBOBOX，因此 ControlHost 溢出必须有原生子控件表面，不能沿用当前 NativeMenu 禁用这些条目的结果后宣称完整。按钮绘制样式与系统 toolbar 自动 overflow 并非 WinForms 契约本身，首先保证托管布局/对象/事件正确。

**Linux / GTK4**：GtkBox/共享兼容布局计算主条带，GtkMenuButton 打开 GtkPopoverMenu，普通菜单项由 GMenuModel/GAction 承载；任意 ControlHost 用 GtkPopover 中的原生子控件容器。GTK4 核心不提供一个完整同义的 WinForms ToolStripOverflowButton，也不应使用已移除的 GtkToolbar/GtkToolItem API。菜单模型和可编辑控件容器的边界先用实证样本确认，原生 widget 信号随条目迁移正确解绑/重接。

**macOS / AppKit**：窗体内 ToolStrip 可由 NSView 中的 NSButton/NSPopUpButton 打开 NSMenu 或原生 NSPopover；窗口工具栏可评估 NSToolbarItem 的 overflow/menuFormRepresentation。AppKit 系统工具栏的自动收纳策略不天然满足 WinForms 的 Never/Always、Parent/Placement 和可编辑 ControlHost 契约，需共享托管布局并验证；不能仅因为 NSToolbar 有系统 overflow 就标为等价。

本类型不是独立应用创建的普通按钮，也不需要单独新项目。先完成 Windows 溢出状态与对象身份，再验证两平台原生组合。样式像素、Windows 消息与 GDI 绘制扩展各自登记；没有原生等价时标 🚫/未达，而不是将未实现项计作完成。

## 内部条目与嵌套控件的归属

固定程序集中的非公开 `System.Windows.Forms.ToolStripScrollButton : ToolStripControlHost` 与 `System.Windows.Forms.ToolStripScrollButton+StickyLabel : Label` 属于下拉内容滚动实现，行为归 [ToolStripOverflow](ToolStripOverflow.md)、[ToolStripDropDown](ToolStripDropDown.md) 的超长内容/键盘/按住滚动/释放任务（P2/P3）。`System.Windows.Forms.ToolStripGrip : ToolStripButton` 的拖拽手柄行为归 [ToolStrip](ToolStrip.md) 与 [ToolStripPanel](ToolStripPanel.md)（P2）。它们不是遗漏的公开控件；不新增公开 API，也不要求照搬微软内部类。

## 修改历史

- 截至 `b83f534` 未发现本类型的实现文件，无可归属的本类型实现提交。
- 宿主相关历史（不代表 overflow 已实现）：`ed64510` · 2026-04-08 · 菜单下拉状态与相邻导航；`275c479` · 2026-04-08 · 文本布局和高 DPI；`83e9356` · 2026-04-08 · 菜单图像资源处理。均来自 `git log -3 -- Lumina.Forms/ToolStrip.cs`。
- 2026-10-15 · 新增本独立规划，未修改实现。后续实现需记录日期、提交、具体溢出场景与验收证据。

## 官方依据

- [WinForms ToolStripOverflowButton（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripoverflowbutton?view=windowsdesktop-10.0)。
- [WinForms ToolStripItemOverflow](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripitemoverflow?view=windowsdesktop-10.0)。
- [GTK4 MenuButton](https://docs.gtk.org/gtk4/class.MenuButton.html)、[Popover](https://docs.gtk.org/gtk4/class.Popover.html)。
- [AppKit NSToolbarItem](https://developer.apple.com/documentation/appkit/nstoolbaritem)、[NSPopover](https://developer.apple.com/documentation/appkit/nspopover)。
