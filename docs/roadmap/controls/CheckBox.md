# CheckBox

基线：2026-10-15 · .NET 10.0.11。目标继承：`CheckBox : ButtonBase`。本页是实施与验收任务，未声称已完成兼容。

## 当前证据与依赖

🟡 [CheckBox.cs](../../../Lumina.Forms/CheckBox.cs) 已用 BUTTON/BS_AUTOCHECKBOX、BM_GETCHECK/BM_SETCHECK 和 CheckedChanged；CheckState 被投影为 bool，Indeterminate 会丢失，缺 ThreeState/AutoCheck/CheckStateChanged，直接继承 Control。

源码定位：[Lumina.Forms/CheckBox.cs](../../../Lumina.Forms/CheckBox.cs)；共同问题见 [Control](../../../Lumina.Forms/Control.cs) 和 [Win32 interop](../../../Lumina.Forms/Win32.cs)。先决依赖：ButtonBase、Control.Click 与键盘消息、绑定写回和无障碍状态通知。

公开成员（含继承的全部属性、事件和方法）逐项见 [CheckBox API 清单](../api/CheckBox.md)；共同行为、AOT、设计器与计数方法见 [基础任务](../foundations.md)。本表列出具体实现/验收动作，API 清单中余下成员也必须逐条关闭，不能把表外成员当作已支持。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 先修层级/阻塞，P1 完成常用契约，P2 补边界/高级场景，P3 完成平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| CheckBox-001 | P0 | ⬜ | 继承恢复 CheckBox : ButtonBase；补齐 CheckState、Appearance 等关联枚举正确数值和公开签名，避免已有 bool 实现吞掉 Indeterminate。 |
| CheckBox-002 | P1 | 🟡 | Checked=false、CheckState=Unchecked 默认状态；程序设同值不重复通知，CheckState=Indeterminate 时 Checked 的值按 WinForms 基线，含创建前后双向同步。 |
| CheckBox-003 | P1 | ⬜ | ThreeState=false / AutoCheck=true；测试两态、三态点击循环，AutoCheck=false 时点击保留状态，键盘和辅助动作与鼠标相同。 |
| CheckBox-004 | P1 | 🟡 | 已有CheckedChanged，027完成004a当前二态原生通知→共享Click；原全待办遗漏历史事件，现纠正。CheckStateChanged、三态/AutoCheck、直接OnClick自动切换与完整事件顺序仍缺，不由bool比较替代Indeterminate语义。 |
| CheckBox-004a | P1 | ✅ | 批次027：BN_CLICKED先读取BM_GETCHECK并同步既有Checked/CheckedChanged，再调用共享OnClick一次；关闭StandardClick/StandardDoubleClick防重复。范围只限现有二态原生通知衔接，直接OnClick/AutoCheck/三态仍归父项。 |
| CheckBox-005 | P1 | ⬜ | Appearance 默认 Normal、AppearanceChanged；切换 Button 外观保持三态值；CheckAlign 默认 MiddleLeft 与 TextAlign 默认 MiddleLeft 对照基线验证布局。 |
| CheckBox-006 | P1 | ⬜ | 父容器点击、空格、Alt 助记键、Disabled/ReadOnly（不存在的 CheckBox 属性不新增）、验证取消和鼠标捕获取消按基线验收。 |
| CheckBox-007 | P2 | ⬜ | 状态绑定：数据源更新→控件和控件点击→数据源，三态 nullable 绑定与 Checked bool 绑定分开测试，重入期间一次变更不产生无限循环。 |
| CheckBox-008 | P2 | ⬜ | AutoSize、FlatStyle、图像、RTL、DPI 与主题下勾选区域和文字测量；当前 UseVisualStyleBackColor 不应只存值而无行为。 |
| CheckBox-009 | P2 | ⬜ | UIA/AT-SPI/NSAccessibility 的可切换、mixed、禁用状态通知；Tab 焦点、焦点框和键盘可操作性。 |
| CheckBox-010 | P3 | ⬜ | 保存三态的 Designer 往返、AOT 发布、句柄重建、Dispose 后原生回调解绑与绑定订阅释放。 |
| CheckBox-CLOSE | P3 | ⬜ | 对照 API 清单逐个关闭剩余属性、事件、方法/重载与受保护兼容点；属性写入后读回、通知顺序/取消/重入、默认值、异常、未建句柄/重建/释放后行为分别有证据。Windows 与两原生平台各出属性/事件/行为分子分母，未验收项不能标🧪 验收通过。 |

## 三平台的原生实现评估

**Windows**：BUTTON 的 BS_CHECKBOX/BS_AUTOCHECKBOX/BS_3STATE/BS_AUTO3STATE 与 BM_*CHECK 组合；BN_CLICKED 后读取原生三态并同步托管模型，程序赋值走相同状态通知规则。自动切换或托管切换只能选一个执行入口，防止点击切换两次。

**Linux / GTK4**：GtkCheckButton 使用 active 与 inconsistent 表达状态，toggled 同步；GTK 的 inconsistent 不是 WinForms 三态循环的自动实现，需按 ThreeState/AutoCheck 驱动状态机。Appearance.Button 可评估原生 GtkToggleButton，样式/角色差异须单列。

**macOS / AppKit**：NSButton 类型 switch，allowsMixedState、state（off/on/mixed）映射三态；target/action 同步。Appearance.Button 可用 toggle 型 NSButton；CheckAlign/TextAlign 的任意组合未必由 AppKit 完整支持，须先做原生排版验证。

跨平台工作排在 Windows 行为基线与该类最小原生试验之后。先复用同一公共 WinForms 类型与状态模型，在库内封装平台差异；本路线不要求新增平台项目或公开抽象。每平台至少覆盖：默认值/边界，代码与用户修改双向同步，事件顺序/取消/重入，键盘/输入法（适用时），DPI/RTL/本地化，无障碍，创建/重建/释放，AOT 实际运行；纯编译或仅显示窗口不足以通过。

验收口径：基线固定为 .NET 10.0.11 WinForms。Windows 的公开属性、公开事件、行为场景分别 ≥98%；Linux/macOS 在逐项确认的可移植分母中分别 ≥98%。每个重载、访问器、事件参数与行为场景独立登记，继承成员不遗漏；签名同名、字段存值、任务写完均不能作为通过分子。原生外观差异与 Windows 专属成员保留在清单中，不能用“平台不可等价”自动划掉普通业务需求。当前各项尚无完整对照证据，不发布已达标百分比。

## 修改历史

- 2026-10-16 · 批次027：BN_CLICKED先读取BM_GETCHECK并同步既有Checked/CheckedChanged，再调用共享OnClick一次；关闭StandardClick/StandardDoubleClick防重复。范围只限现有二态原生通知衔接，直接OnClick/AutoCheck/三态仍归父项。 已有CheckedChanged，027完成004a当前二态原生通知→共享Click；原全待办遗漏历史事件，现纠正。CheckStateChanged、三态/AutoCheck、直接OnClick自动切换与完整事件顺序仍缺，不由bool比较替代Indeterminate语义。 25项必要消费回归合计通过；固定参考消费者差异及锁屏限制见[027](../batches/027.md)，不关闭整个控件/平台。

- 2026-10-16 · 批次023：基础Control启用标准UserPaint后，本控件构造显式保留UserPaint=false，使现有系统窗口类过程及原生字体/绘制继续生效。仅属消息与绘制基础的必要衔接，不代表本控件完整Styles、Paint事件或视觉/输入兼容完成，见[023](../batches/023.md)。

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 2026-04-09 · `9d5165f` · feat: 添加多个控件和功能，增强用户界面和交互体验。
- 2026-04-08 · `2545995` · feat: 增强深色模式支持，优化多个控件的主题应用逻辑。
- 2026-04-08 · `108ef2b` · feat: 添加对 Win32 黑暗模式的支持，重构多个控件以优化主题应用逻辑。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

## 官方依据

- [WinForms CheckBox API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.checkbox?view=windowsdesktop-10.0)，精确签名以本机 10.0.11 引用程序集生成的 API 清单为准。
- [Windows 原生控件参考](https://learn.microsoft.com/windows/win32/controls/buttons)。
- [GTK4 CheckButton](https://docs.gtk.org/gtk4/class.CheckButton.html)。
- [AppKit nsbutton](https://developer.apple.com/documentation/appkit/nsbutton)。
