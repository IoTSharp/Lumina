# ToolTip

🟡 **当前状态：**目前只有 Component 与 Control→string 字典、SetToolTip/GetToolTip；没有原生提示窗口、Popup/Draw 和时间控制。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[ToolTip API](../api/ToolTip.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：[ToolTip.cs](../../../Lumina.Forms/ToolTip.cs)
- 前置与排序：Control 句柄/位置/输入、Component/IExtenderProvider、UI 定时器。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| ⬜ | P0 / ToolTip-01 | 实现标准 IExtenderProvider、CanExtend、SetToolTip/GetToolTip、RemoveAll，与 components 容器/设计器扩展属性序列化兼容。 |
| ⬜ | P0 / ToolTip-02 | 实现 Active、AutomaticDelay、AutoPopDelay、InitialDelay、ReshowDelay、ShowAlways、IsBalloon、StripAmpersands、ToolTipIcon/ToolTipTitle、UseAnimation/UseFading/OwnerDraw 的默认和联动。 |
| ⬜ | P1 / ToolTip-03 | 补全部 Show/Hide 重载，窗口/Control/Point/x/y/duration 参数类型按基线；自动悬停与程序显示的超时、隐藏和重复显示行为分别验收。 |
| ⬜ | P1 / ToolTip-04 | 实现 Popup 可取消与 ToolTipSize 修改，OwnerDraw 下 Draw/DrawToolTipEventArgs；事件在提示真正显示前/绘制时触发，不只存委托。 |
| ⬜ | P1 / ToolTip-05 | 监听控件创建/销毁/移动/隐藏、父窗口失活与滚动；提示锚点和 text 更新无需重新 SetToolTip，Handle 重建后重新注册。 |
| ⬜ | P1 / ToolTip-06 | 校验延迟属性联动、重复赋值、null/空字符串移除、多个控件共享一实例、RemoveAll、禁用/隐藏控件与键盘焦点提示。 |
| ⬜ | P1 / ToolTip-07 | 文字换行/Unicode/&、RTL、DPI/屏幕边缘、工具提示可访问性、触摸/键盘入口；Dispose 清理定时器、字典和事件，避免强引用已释放控件。 |
| ⬜ | P1 / ToolTip-08 | Windows 用 tooltips_class32、TTM_ADDTOOLW/DELTOOLW/UPDATETIPTEXTW/TRACKACTIVATE/SETDELAYTIME，TTN_SHOW/POP 与 custom draw 通知。 |
| ⬜ | P2 / ToolTip-09 | Linux 简单提示用 GtkWidget.tooltip-text/tooltip-markup 与 query-tooltip；Popup 的取消/尺寸可通过 query-tooltip 与 GtkTooltip 自定义 native child 适配；精确超时/气泡/OwnerDraw 并非系统公开能力。 |
| ⬜ | P3 / ToolTip-10 | macOS 用 NSView toolTip 或 addToolTipRect/owner 回调；精确 delay/Popup veto/balloon/OwnerDraw 原生接口不全，普通提示原生可用，其他选项单列而非假称全部支持。 |
| ⬜ | P1 / ToolTip-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / ToolTip-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI/非视觉组件写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / ToolTip-V3 | 发布 win-x64 Native AOT，检查实际窗口/原生组件与本类型交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | tooltips_class32 原生提示；目前字典可作为注册数据源但不是显示实现。 |
| Linux | GtkTooltip/query-tooltip；系统主题控制延迟/动画，需平台例外或原生 Popover 组合原型，不能随意自绘整个提示系统。 |
| macOS | NSView 原生 tooltip；自定义 native NSPopover 可用于部分增强，行为/焦点与系统 tooltip 不完全一致，须明确验收边界。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- `2545995`（2026-04-08）：深色主题/UI 改进涉及提示组件。
- `dcdc7e9`（2026-03-31）：加入 ToolTip 字典与基础组件。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms ToolTip 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.tooltip?view=windowsdesktop-10.0)
- [GTK4 Tooltip](https://docs.gtk.org/gtk4/class.Tooltip.html)
- [AppKit nsview](https://developer.apple.com/documentation/appkit/nsview)
