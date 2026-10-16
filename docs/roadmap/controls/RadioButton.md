# RadioButton

基线：2026-10-15 · .NET 10.0.11。目标继承：`RadioButton : ButtonBase`。本页是实施与验收任务，未声称已完成兼容。

## 当前证据与依赖

🟡 [RadioButton.cs](../../../Lumina.Forms/RadioButton.cs) 有 BUTTON/BS_AUTORADIOBUTTON、Checked 和 CheckedChanged，但直接继承 Control；尚无 AutoCheck、Appearance、CheckAlign 或明确托管互斥组同步，原生兄弟自动取消不保证另一托管对象发事件。

源码定位：[Lumina.Forms/RadioButton.cs](../../../Lumina.Forms/RadioButton.cs)；共同问题见 [Control](../../../Lumina.Forms/Control.cs) 和 [Win32 interop](../../../Lumina.Forms/Win32.cs)。先决依赖：ButtonBase、真实 Parent/容器树、焦点与 Tab 导航；必须先修复顶层控件 Parent 为空的问题。

公开成员（含继承的全部属性、事件和方法）逐项见 [RadioButton API 清单](../api/RadioButton.md)；共同行为、AOT、设计器与计数方法见 [基础任务](../foundations.md)。本表列出具体实现/验收动作，API 清单中余下成员也必须逐条关闭，不能把表外成员当作已支持。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 先修层级/阻塞，P1 完成常用契约，P2 补边界/高级场景，P3 完成平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| RadioButton-001 | P0 | ⬜ | 恢复 RadioButton : ButtonBase；Parent、GroupBox、Panel、UserControl、Form 下的分组关系可查询，按钮移组立即清理旧组状态。 |
| RadioButton-002 | P1 | 🟡 | Checked=false、AutoCheck=true 默认值；程序写入、鼠标、空格、助记键分别测试单选互斥；AutoCheck=false 不自动取消同组其他控件。 |
| RadioButton-003 | P1 | ⬜ | CheckedChanged 与 Click：同组 A→B 时旧按钮取消事件和新按钮选中事件顺序按 WinForms 记录；同值赋值不重复，点击已选项不变成全未选。 |
| RadioButton-004 | P1 | ⬜ | Appearance=Normal、AppearanceChanged、CheckAlign=MiddleLeft、TextAlign=MiddleLeft；切换 Appearance.Button 仍保持相同组和事件契约。 |
| RadioButton-005 | P1 | ⬜ | TabStop 及箭头导航：单选组进入/离开、无选中项、禁用项、隐藏项、RTL、Home/End（按基线支持情况）与焦点移动行为逐场景验收。 |
| RadioButton-006 | P1 | ⬜ | AutoCheck、CausesValidation 与验证取消协同；验证失败时选中状态、焦点与 Click 是否改变按真实 WinForms 对照。 |
| RadioButton-007 | P2 | ⬜ | Parent 变化、AddRange 批量加载、句柄重建、当前选中项被移除/Dispose 后保持组状态；不让原生自动勾选和托管同步互相递归。 |
| RadioButton-008 | P2 | ⬜ | Checked 绑定回写与模型设置互斥，先后两项数据更新后的事件次数和最终组值可预测；支持 BeginInit/布局暂停期间静态初始化顺序。 |
| RadioButton-009 | P2 | ⬜ | AutoSize/图文排列/FlatStyle/主题/DPI，UIA/AT-SPI/AppKit radio role、组关系和选中状态通知。 |
| RadioButton-010 | P3 | ⬜ | 同一 Designer 静态单选组跨两目标编译与 AOT 运行，三平台保留独立组、键盘与释放的验收记录。 |
| RadioButton-CLOSE | P3 | ⬜ | 对照 API 清单逐个关闭剩余属性、事件、方法/重载与受保护兼容点；属性写入后读回、通知顺序/取消/重入、默认值、异常、未建句柄/重建/释放后行为分别有证据。Windows 与两原生平台各出属性/事件/行为分子分母，未验收项不能标🧪 验收通过。 |

## 三平台的原生实现评估

**Windows**：BUTTON + BS_RADIOBUTTON/BS_AUTORADIOBUTTON；原生分组与 WinForms Parent 容器定义对齐，不把所有同 HWND 兄弟误归一组。互斥切换需要更新旧选项托管 Checked 与事件，不能仅依赖 Windows 自动取消。

**Linux / GTK4**：GtkCheckButton.set_group 形成原生单选组；按托管 Parent 分组，迁移 Parent 时重新分组，toggled 的旧值和新值事件去重。GTK4 没有必要另造公开 RadioGroup；继续暴露 RadioButton API。

**macOS / AppKit**：NSButton 类型 radio；按 WinForms 容器显式维护组并设置其他按钮 state，避免依赖 AppKit 的视图层级自动分组细节。键盘箭头和 TabStop 需补 WinForms 组语义；任意 CheckAlign 外观能力差异逐项登记。

跨平台工作排在 Windows 行为基线与该类最小原生试验之后。先复用同一公共 WinForms 类型与状态模型，在库内封装平台差异；本路线不要求新增平台项目或公开抽象。每平台至少覆盖：默认值/边界，代码与用户修改双向同步，事件顺序/取消/重入，键盘/输入法（适用时），DPI/RTL/本地化，无障碍，创建/重建/释放，AOT 实际运行；纯编译或仅显示窗口不足以通过。

验收口径：基线固定为 .NET 10.0.11 WinForms。Windows 的公开属性、公开事件、行为场景分别 ≥98%；Linux/macOS 在逐项确认的可移植分母中分别 ≥98%。每个重载、访问器、事件参数与行为场景独立登记，继承成员不遗漏；签名同名、字段存值、任务写完均不能作为通过分子。原生外观差异与 Windows 专属成员保留在清单中，不能用“平台不可等价”自动划掉普通业务需求。当前各项尚无完整对照证据，不发布已达标百分比。

## 修改历史

- 2026-10-16 · 批次023：基础Control启用标准UserPaint后，本控件构造显式保留UserPaint=false，使现有系统窗口类过程及原生字体/绘制继续生效。仅属消息与绘制基础的必要衔接，不代表本控件完整Styles、Paint事件或视觉/输入兼容完成，见[023](../batches/023.md)。

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 2026-04-08 · `2545995` · feat: 增强深色模式支持，优化多个控件的主题应用逻辑。
- 2026-04-08 · `108ef2b` · feat: 添加对 Win32 黑暗模式的支持，重构多个控件以优化主题应用逻辑。
- 2026-03-31 · `ce86ab5` · 已经把 Lumina.Forms 的主题能力接到更像 WinForms 的核心表面上了。现在 Control.cs 和 Form.cs 提供了 BackColor / ForeColor、对应变更事件、主题槽位解析、父容器背景继承，以及打开窗口后的实时主题刷新；高频控件也统一改成走 ApplyExp…（原提交标题节录）。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

## 官方依据

- [WinForms RadioButton API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.radiobutton?view=windowsdesktop-10.0)，精确签名以本机 10.0.11 引用程序集生成的 API 清单为准。
- [Windows 原生控件参考](https://learn.microsoft.com/windows/win32/controls/buttons)。
- [GTK4 CheckButton](https://docs.gtk.org/gtk4/class.CheckButton.html)。
- [AppKit nsbutton](https://developer.apple.com/documentation/appkit/nsbutton)。
