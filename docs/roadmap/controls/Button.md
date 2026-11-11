# Button

基线：2026-10-15 · .NET 10.0.11。目标继承：`Button : ButtonBase`。本页是实施与验收任务，未声称已完成兼容。

## 当前证据与依赖

🟡 [Button.cs](../../../Lumina.Forms/Button.cs) 仍直接继承 Control；027统一Click事件入口，035补IButtonControl/DialogResult、NotifyDefault及PerformClick必要验证/默认按钮消费，036按固定源码审计原生点击验证前置并以真实HWND回归确认Button.cs无需重复注入。ButtonBase、完整物理点击前置/命令/样式/辅助技术与生命周期仍缺，不能把有限默认按钮协作当完整兼容。

源码定位：[Lumina.Forms/Button.cs](../../../Lumina.Forms/Button.cs)；共同问题见 [Control](../../../Lumina.Forms/Control.cs) 和 [Win32 interop](../../../Lumina.Forms/Win32.cs)。先决依赖：ButtonBase、Control 输入路由、Form.AcceptButton / CancelButton / DialogResult、验证与焦点链。

公开成员（含继承的全部属性、事件和方法）逐项见 [Button API 清单](../api/Button.md)；共同行为、AOT、设计器与计数方法见 [基础任务](../foundations.md)。本表列出具体实现/验收动作，API 清单中余下成员也必须逐条关闭，不能把表外成员当作已支持。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 先修层级/阻塞，P1 完成常用契约，P2 补边界/高级场景，P3 完成平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| Button-001 | P0 | 🟡 | 035的001a完成IButtonControl；仍需Button : ButtonBase标准继承及完整Designer消费，接口存在不能关闭父项。 |
| Button-002 | P1 | 🟡 | 027统一Click入口，035的002a实现PerformClick可选/验证策略首范围；完整物理点击前置、命令/原生焦点与全部重入异常组合仍待，程序激活不关闭全部行为。 |
| Button-003 | P1 | 🟡 | 035的003a补默认None/合法值与点击写回Form结果；完整ShowDialog/Modal、模态关闭/取消及所有生命周期组合仍待。 |
| Button-004 | P1 | 🟡 | 035的004a补NotifyDefault与Form默认/取消首范围；IsDefault/完整ButtonBase、原生焦点切换、移除/Dispose全部组合及默认样式交互仍待。固定WinForms允许仍被AcceptButton引用的孤立按钮，不自行添加所有权限制。 |
| Button-005 | P1 | 🟡 | 027完成005a共享Click/原生通知衔接；鼠标与空格/Enter/Alt助记键、辅助技术、命令/验证/FormClosing完整顺序仍待对照。默认owner draw/UserMouse与当前native样式及BM_CLICK顺序差异保留。 |
| Button-006 | P1 | ⬜ | AutoSizeMode 默认 GrowOnly 按基线确认；配合 AutoSize=false 默认值、最小尺寸、换行图文、TextImageRelation 和 DPI，禁用当前硬编码最小高度造成的布局偏移。 |
| Button-007 | P2 | ⬜ | 按钮公开 DoubleClick / MouseDoubleClick 的隐藏与实际事件行为按 API 元数据和原生行为验证；不要因继承 Control 而引入基线不会触发的双击。 |
| Button-008 | P2 | ⬜ | Button 图像/背景/FlatStyle/UseVisualStyleBackColor 的创建前与创建后写入保持一致，句柄重建不能丢失默认按钮状态。 |
| Button-009 | P2 | ⬜ | 默认按钮辅助功能状态、名称、键盘快捷键及禁用动作；Dispose 后消息不再触发 Click，外部 Image 不被控件错误释放。 |
| Button-010 | P3 | ⬜ | 同一 .Designer.cs 静态按钮布局在真实 WinForms 与 Lumina.Forms 两目标编译、显示、键盘操作；win-x64 AOT 运行并保留事件序列证据。 |
| Button-CLOSE | P3 | ⬜ | 对照 API 清单逐个关闭剩余属性、事件、方法/重载与受保护兼容点；属性写入后读回、通知顺序/取消/重入、默认值、异常、未建句柄/重建/释放后行为分别有证据。Windows 与两原生平台各出属性/事件/行为分子分母，未验收项不能标🧪 验收通过。 |

## 三平台的原生实现评估

**Windows**：继续用 BUTTON + BS_PUSHBUTTON；默认按钮用 BS_DEFPUSHBUTTON/BM_SETSTYLE 及窗体默认按钮路由配合。BN_CLICKED 只作为激活动作入口，随后执行 WinForms 的验证、命令、DialogResult 与事件顺序；同一次点击避免原生通知和托管动作重复。

**Linux / GTK4**：GtkButton 是原生按钮；clicked 对接统一激活动作，GtkWindow 的默认控件与键盘激活配合 AcceptButton；Escape 由窗口取消按钮处理。DialogResult 是托管窗体语义，不能由 GTK 响应码直接取代；FlatAppearance 与 Windows 主题差异单列。

**macOS / AppKit**：NSButton 使用瞬时按压按钮类型；Return 对应默认按钮 keyEquivalent，Escape 配合窗口 CancelButton。target/action 回调汇入同一托管激活管线；macOS 默认键盘焦点策略、按钮最小尺寸和 Windows 外观不同，需验证公共行为而非强求像素一致。

跨平台工作排在 Windows 行为基线与该类最小原生试验之后。先复用同一公共 WinForms 类型与状态模型，在库内封装平台差异；本路线不要求新增平台项目或公开抽象。每平台至少覆盖：默认值/边界，代码与用户修改双向同步，事件顺序/取消/重入，键盘/输入法（适用时），DPI/RTL/本地化，无障碍，创建/重建/释放，AOT 实际运行；纯编译或仅显示窗口不足以通过。

验收口径：基线固定为 .NET 10.0.11 WinForms。Windows 的公开属性、公开事件、行为场景分别 ≥98%；Linux/macOS 在逐项确认的可移植分母中分别 ≥98%。每个重载、访问器、事件参数与行为场景独立登记，继承成员不遗漏；签名同名、字段存值、任务写完均不能作为通过分子。原生外观差异与 Windows 专属成员保留在清单中，不能用“平台不可等价”自动划掉普通业务需求。当前各项尚无完整对照证据，不发布已达标百分比。

## 已完成任务

✅ 表示该行定义范围代码完成；父项的剩余代码与平台验收仍按当前任务表记录。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| Button-001a | P0 | ✅ | 批次035：标准IButtonControl三成员与Button实现，DialogResult/NotifyDefault/PerformClick实际消费；ButtonBase继承仍待，不自造替代接口。 |
| Button-002a | P1 | ✅ | 批次035：PerformClick按CanSelect和ValidateActiveControl结果、允许焦点变化策略及ValidationCancelled决定是否激活；覆盖不可选、三种AutoValidate和CausesValidation=false。原生BN_CLICKED仅补取消守卫，完整物理点击验证前置仍待。 |
| Button-002b | P1 | ✅ | 批次036：按固定10.0.11源码核对Control鼠标按下→焦点/验证→BN_CLICKED顺序，确认Button.WndProc/OnCommand不应重复调用验证；真实HWND Prevent/Allow点击回归分别验证取消时不Click、允许时Validating后Click。完整物理设备和派生组合仍待。 |
| Button-003a | P1 | ✅ | 批次035：Button.DialogResult合法值与默认None，属性变更不关闭窗体；点击经FindForm先写结果再发Click，嵌套容器/无父和非模态范围有回归。DialogResult补齐Abort/Retry/Ignore/TryAgain/Continue，8/9仍非法；不宣称模态已实现。 |
| Button-004a | P1 | ✅ | 批次035：NotifyDefault更新默认创建样式，Form活动链切换旧/新默认通知；Enter/Escape及CancelButton的None→Cancel规则有回归，实际队列集成通过。未建句柄样式检查不等于屏幕外观或物理键盘验收。 |
| Button-005a | P1 | ✅ | 批次027：移除遮蔽Control的独立Click事件，OnClick覆盖转发base，PerformClick/BN_CLICKED进入同一虚事件槽；StandardClick/StandardDoubleClick关闭以避免base鼠标重复激活。只关闭该必要接入，ButtonBase/FlatStyle/验证/默认按钮仍缺。 |

## 官方依据

- [WinForms Button API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.button?view=windowsdesktop-10.0)，精确签名以本机 10.0.11 引用程序集生成的 API 清单为准。
- [Windows 原生控件参考](https://learn.microsoft.com/windows/win32/controls/buttons)。
- [GTK4 Button](https://docs.gtk.org/gtk4/class.Button.html)。
- [AppKit nsbutton](https://developer.apple.com/documentation/appkit/nsbutton)。
