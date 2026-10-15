# NumericUpDown

基线：2026-10-15 · .NET 10.0.11。目标继承：`NumericUpDown : UpDownBase`。本页是实施与验收任务，未声称已完成兼容。

## 当前证据与依赖

🟡 [NumericUpDown.cs](../../../Lumina.Forms/NumericUpDown.cs) 为 TextBox 子类，Value 用 InvariantCulture 更新 Text，Minimum/Maximum 自动属性，只有 EndInit 钳制范围；无真实增减按钮、ValueChanged、Increment/DecimalPlaces 和文化解析。

源码定位：[Lumina.Forms/NumericUpDown.cs](../../../Lumina.Forms/NumericUpDown.cs)；共同问题见 [Control](../../../Lumina.Forms/Control.cs) 和 [Win32 interop](../../../Lumina.Forms/Win32.cs)。先决依赖：UpDownBase、decimal 格式化/解析、ISupportInitialize、NumericUpDownAcceleration/Collection。

公开成员（含继承的全部属性、事件和方法）逐项见 [NumericUpDown API 清单](../api/NumericUpDown.md)；共同行为、AOT、设计器与计数方法见 [基础任务](../foundations.md)。本表列出具体实现/验收动作，API 清单中余下成员也必须逐条关闭，不能把表外成员当作已支持。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 先修层级/阻塞，P1 完成常用契约，P2 补边界/高级场景，P3 完成平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| NumericUpDown-001 | P0 | ⬜ | 恢复 NumericUpDown : UpDownBase, ISupportInitialize；Value(decimal) 正确公开，补 NumericUpDownAcceleration/Collection；Designer BeginInit/EndInit 可任意标准顺序初始化范围和值。 |
| NumericUpDown-002 | P1 | 🟡 | Minimum=0、Maximum=100、Value=0；普通 Value 超范围应按 WinForms 抛错而非只存字段，改变上下限导致相互修正/Value钳制及事件次序分别验收。 |
| NumericUpDown-003 | P1 | ⬜ | Increment=1(decimal)、UpButton/DownButton、端点不回绕、步进超过边界时钳制；decimal.Min/MaxValue 附近不溢出，支持小数和负数步进范围。 |
| NumericUpDown-004 | P1 | ⬜ | DecimalPlaces=0、ThousandsSeparator=false、Hexadecimal=false；格式化、输入、舍入显示与内部 Value 区分，十六进制负数/小数行为对照基线。 |
| NumericUpDown-005 | P1 | ⬜ | ValueChanged、TextChanged：程序设值、相同值、用户键入后提交、步进、范围变化和 EndInit 各自次数与顺序；事件回调中再次赋值的最终状态。 |
| NumericUpDown-006 | P1 | ⬜ | CurrentCulture/NumberFormat、空文本、正负号、小数点、分组符、无效粘贴、失焦/Enter提交、ReadOnly；替换当前 InvariantCulture 单一格式逻辑。 |
| NumericUpDown-007 | P1 | ⬜ | Accelerations 的 Seconds/Increment、长按重复、键盘/滚轮增量与方向，释放鼠标/失焦/Dispose 后立即停止加速。 |
| NumericUpDown-008 | P2 | ⬜ | BeginInit 期间暂停范围检查、EndInit 恢复并格式化，嵌套/重复调用按基线；ISupportInitialize 不只是最后 Math.Clamp，非法范围处理明确。 |
| NumericUpDown-009 | P2 | ⬜ | 绑定 Value 与 Text 的回写次序、用户未提交文本和外部数据更新，精确 decimal 在 Linux/macOS 不经 double 丢精度。 |
| NumericUpDown-010 | P2 | ⬜ | AutoSize/Font/DPI/UpDownAlign/RTL、可访问性 range/value/可编辑文本和步进动作；极值格式不截断。 |
| NumericUpDown-011 | P3 | ⬜ | 静态 Designer 属性和加速集合序列化、AOT 文化解析与事件实际运行，释放编辑宿主/按钮/重复定时器。 |
| NumericUpDown-CLOSE | P3 | ⬜ | 对照 API 清单逐个关闭剩余属性、事件、方法/重载与受保护兼容点；属性写入后读回、通知顺序/取消/重入、默认值、异常、未建句柄/重建/释放后行为分别有证据。Windows 与两原生平台各出属性/事件/行为分子分母，未验收项不能标🧪 验收通过。 |

## 三平台的原生实现评估

**Windows**：通过 UpDownBase 的 EDIT+UPDOWN 原生组合，UDN_DELTAPOS 只提供方向/步数，decimal 真值与范围保留托管；取消原生整数自动写 buddy，使用精确格式化。不能用 UDM_SETPOS32 限制 decimal 范围和小数。

**Linux / GTK4**：GtkSpinButton 提供原生旋钮和可编辑文本，但 GtkAdjustment 使用 double；对于 decimal 精度、极大范围必须验证，必要时用原生 GtkEntry+上下按钮组合，以托管 decimal 为唯一真值。允许原生组合，不能默默舍入并记为支持。

**macOS / AppKit**：NSTextField+NSStepper，formatter/委托用 decimal 文本解析并由 UpButton/DownButton 更新；NSStepper.doubleValue 仅作动作信号。ThousandsSeparator、十六进制、位数与文化不直接交给 AppKit 默认 NSNumber 格式猜测。

跨平台工作排在 Windows 行为基线与该类最小原生试验之后。先复用同一公共 WinForms 类型与状态模型，在库内封装平台差异；本路线不要求新增平台项目或公开抽象。每平台至少覆盖：默认值/边界，代码与用户修改双向同步，事件顺序/取消/重入，键盘/输入法（适用时），DPI/RTL/本地化，无障碍，创建/重建/释放，AOT 实际运行；纯编译或仅显示窗口不足以通过。

验收口径：基线固定为 .NET 10.0.11 WinForms。Windows 的公开属性、公开事件、行为场景分别 ≥98%；Linux/macOS 在逐项确认的可移植分母中分别 ≥98%。每个重载、访问器、事件参数与行为场景独立登记，继承成员不遗漏；签名同名、字段存值、任务写完均不能作为通过分子。原生外观差异与 Windows 专属成员保留在清单中，不能用“平台不可等价”自动划掉普通业务需求。当前各项尚无完整对照证据，不发布已达标百分比。

## 修改历史

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 2026-04-08 · `2545995` · feat: 增强深色模式支持，优化多个控件的主题应用逻辑。
- 2026-03-31 · `dcdc7e9` · Add theme and control components to Lumina.Forms。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

## 官方依据

- [WinForms NumericUpDown API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.numericupdown?view=windowsdesktop-10.0)，精确签名以本机 10.0.11 引用程序集生成的 API 清单为准。
- [Windows 原生控件参考](https://learn.microsoft.com/windows/win32/controls/up-down-controls)。
- [GTK4 SpinButton](https://docs.gtk.org/gtk4/class.SpinButton.html)。
- [AppKit nsstepper](https://developer.apple.com/documentation/appkit/nsstepper)。
