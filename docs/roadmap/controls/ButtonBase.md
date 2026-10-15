# ButtonBase

基线：2026-10-15 · .NET 10.0.11。目标继承：`ButtonBase : Control`。本页是实施与验收任务，未声称已完成兼容。

## 当前证据与依赖

⬜ 当前没有 Lumina.Forms/ButtonBase.cs，也没有公开 ButtonBase 类型。三个按钮类各自直接继承 Control，公共图片、排列、命令和默认按钮契约尚未集中到 WinForms 的既有基类。

源码定位：`Lumina.Forms/ButtonBase.cs`（尚不存在）；共同问题见 [Control](../../../Lumina.Forms/Control.cs) 和 [Win32 interop](../../../Lumina.Forms/Win32.cs)。先决依赖：Control 的句柄、键盘、布局、焦点和 Component/IDisposable 契约；随后 Button、CheckBox、RadioButton 同时迁移。

公开成员（含继承的全部属性、事件和方法）逐项见 [ButtonBase API 清单](../api/ButtonBase.md)；共同行为、AOT、设计器与计数方法见 [基础任务](../foundations.md)。本表列出具体实现/验收动作，API 清单中余下成员也必须逐条关闭，不能把表外成员当作已支持。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 先修层级/阻塞，P1 完成常用契约，P2 补边界/高级场景，P3 完成平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| ButtonBase-001 | P0 | ⬜ | 恢复 abstract ButtonBase : Control；Button、CheckBox、RadioButton 的赋值、多态、受保护覆盖点和 IButtonControl 由正确派生层提供，不能增加另一套公开基类。 |
| ButtonBase-002 | P1 | ⬜ | 属性逐项：Text、Image、ImageList、ImageIndex、ImageKey、ImageAlign、TextAlign、TextImageRelation；检验索引/键互斥、图文布局、图片替换、RTL 和句柄重建前后状态。 |
| ButtonBase-003 | P1 | ⬜ | UseMnemonic=true、AutoEllipsis=false 的默认值按基线确认；& 与 &&、长文本省略、Alt 激活和焦点提示分别列场景，不用静态文字显示代替助记键。 |
| ButtonBase-004 | P1 | ⬜ | AutoSize、GetPreferredSize、Padding、Margin 与 AutoSizeChanged：图片/字体/DPI/文本变更须触发布局；PreferredSize 在有无句柄时均有效。 |
| ButtonBase-005 | P1 | ⬜ | FlatStyle、FlatAppearance 的 BorderColor/BorderSize/CheckedBackColor/MouseDownBackColor/MouseOverBackColor，UseVisualStyleBackColor 和 UseCompatibleTextRendering 逐项比对基线；不可移植外观单列。 |
| ButtonBase-006 | P1 | ⬜ | IsDefault（受保护）、空格按下/释放、鼠标捕获离开后释放、禁用期间点击、Tab/助记键激活；Click 只触发一次，取消按压不误触发。 |
| ButtonBase-007 | P1 | ⬜ | .NET 10 Command、CommandParameter；订阅/更换/解除 ICommand.CanExecuteChanged，CanExecute 与 Enabled 合成及执行顺序；CommandChanged、CommandParameterChanged、CommandCanExecuteChanged 分别记录次数。 |
| ButtonBase-008 | P2 | ⬜ | 派生 OnClick、OnMouseDown/Up、OnKeyDown/Up、OnPaint、WndProc 等受保护兼容点按完整签名清单验收；测试用户派生类而不只测试库自带控件。 |
| ButtonBase-009 | P2 | ⬜ | ImageList 生命周期：外部图片不误释放，ImageList 被释放/重建时清理引用；Dispose 解除命令订阅、原生回调和图像原生对象。 |
| ButtonBase-010 | P3 | ⬜ | 三平台按钮族共用同一组属性/事件/行为样例；原生辅助技术暴露 Button/CheckBox/RadioButton 角色、Name、默认动作和键盘快捷键。 |
| ButtonBase-CLOSE | P3 | ⬜ | 对照 API 清单逐个关闭剩余属性、事件、方法/重载与受保护兼容点；属性写入后读回、通知顺序/取消/重入、默认值、异常、未建句柄/重建/释放后行为分别有证据。Windows 与两原生平台各出属性/事件/行为分子分母，未验收项不能标🧪 验收通过。 |

## 三平台的原生实现评估

**Windows**：用 BUTTON 原生窗口承载按钮族；将 BS_* 风格、BM_* 消息和 BN_* 通知收敛到这个既有 WinForms 基类的内部实现。FlatStyle/FlatAppearance 需要分别评估 Win32 标准样式能够实现的部分；Windows 原生无法表达的逐像素样式不能仅因有 HWND 就判定完成。

**Linux / GTK4**：按派生类型分别用 GtkButton / GtkCheckButton；图片和文本用原生 GtkImage、GtkLabel 组合，动作接 clicked / toggled，助记键接原生 mnemonic。GtkWidget 没有 WinForms FlatAppearance 同义契约，需列差异；普通 GTK 原生控件组合可以接受，另起自绘按钮不计作原生实现。

**macOS / AppKit**：按派生类型用 NSButton 的 momentaryPushIn / switch / radio 按钮类型，target/action、imagePosition 与 keyEquivalent 做映射。bezelStyle 只能覆盖部分 FlatStyle；Windows 的系统主题像素、HWND/消息语义标 🚫，公共 Enabled/点击/命令行为仍须验收。

跨平台工作排在 Windows 行为基线与该类最小原生试验之后。先复用同一公共 WinForms 类型与状态模型，在库内封装平台差异；本路线不要求新增平台项目或公开抽象。每平台至少覆盖：默认值/边界，代码与用户修改双向同步，事件顺序/取消/重入，键盘/输入法（适用时），DPI/RTL/本地化，无障碍，创建/重建/释放，AOT 实际运行；纯编译或仅显示窗口不足以通过。

验收口径：基线固定为 .NET 10.0.11 WinForms。Windows 的公开属性、公开事件、行为场景分别 ≥98%；Linux/macOS 在逐项确认的可移植分母中分别 ≥98%。每个重载、访问器、事件参数与行为场景独立登记，继承成员不遗漏；签名同名、字段存值、任务写完均不能作为通过分子。原生外观差异与 Windows 专属成员保留在清单中，不能用“平台不可等价”自动划掉普通业务需求。当前各项尚无完整对照证据，不发布已达标百分比。

## 修改历史

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 尚无该类型实现文件的提交历史；不将其他控件或本轮规划计为已实现。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

## 官方依据

- [WinForms ButtonBase API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.buttonbase?view=windowsdesktop-10.0)，精确签名以本机 10.0.11 引用程序集生成的 API 清单为准。
- [Windows 原生控件参考](https://learn.microsoft.com/windows/win32/controls/buttons)。
- [GTK4 Button](https://docs.gtk.org/gtk4/class.Button.html)。
- [AppKit nsbutton](https://developer.apple.com/documentation/appkit/nsbutton)。
