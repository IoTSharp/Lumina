# MessageBox

🟡 **现状：**已有 TaskDialog 优先、内部自定义窗体、MessageBoxW 回退与少量 Show 重载；owner 参数仅 Form，非 Windows 直接返回 None。不能把静默 None 视作跨平台已支持。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/MessageBox.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：[MessageBox.cs](../../../Lumina.Forms/MessageBox.cs)
- 排序：Form/IWin32Window、共享模态循环、MessageBox 枚举/帮助系统。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P0 / MessageBox-01 | 补全部 Show 重载并使用 IWin32Window owner，含 MessageBoxDefaultButton、MessageBoxOptions、帮助文件/关键字/HelpNavigator；返回/异常准确而非仅可编译。 |
| ⬜ | P1 / MessageBox-02 | 逐一支持 OK/OKCancel/AbortRetryIgnore/YesNoCancel/YesNo/RetryCancel/CancelTryContinue，按钮结果、关闭叉/Alt+F4/Esc 与 DefaultButton1/2/3 的组合有基线。 |
| ⬜ | P1 / MessageBox-03 | 校准图标别名、空 text/caption、长文本/换行、RTL/RightAlign、ServiceNotification/DefaultDesktopOnly 与非法组合；原生局限明确不能一律 TOPMOST。 |
| ⬜ | P1 / MessageBox-04 | owner 模态禁用/恢复、父窗销毁、nested modal、跨线程调用和焦点恢复遵守标准；TaskDialog 默认行为差异先验证，必要时用 MessageBoxW 最短等价路径。 |
| ⬜ | P1 / MessageBox-05 | 帮助按钮/HelpRequested、默认按钮 Enter、Tab 循环与可访问性文本/角色、系统高对比/DPI/本地化按钮名称实机验证。 |
| ⬜ | P1 / MessageBox-06 | Windows 优先 MessageBoxW/MessageBoxIndirectW 满足标准契约；保留 TaskDialog 仅当功能等价，减少 TaskDialog→自建窗→MessageBox 多层分叉造成差异。 |
| ⬜ | P2 / MessageBox-07 | Linux 使用 GtkAlertDialog 或原生 GtkDialog+GtkButton（取决固定 GTK 版本）并以共享消息循环接成同步返回；原生按钮 labels/顺序跟随平台，DialogResult 仍精确映射。 |
| ⬜ | P3 / MessageBox-08 | macOS 使用 NSAlert 的按钮、alertStyle、runModal/beginSheetModal；默认/取消键等价，Windows ServiceNotification/Desktop/CHM 帮助属明确差异。 |
| ⬜ | 验收 / MessageBox-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| 已有三条实现路径，先用标准 MessageBoxW 验证并简化，保留系统模态/键盘/可访问性。 | GtkAlertDialog（4.10+）或原生 GtkDialog 组合，不再返回 None；异步回调需要受控 UI 消息循环。 | NSAlert；按钮顺序可遵守平台习惯，语义/结果与取消链按固定场景验收。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- `9d5165f`（2026-04-09）：扩展 Show 重载等兼容表面。
- `f666aa6`（2026-04-08）：UI/发布改进涉及消息框。
- `d02239c`（2026-04-08）：消息框前景/最上层与外观调整。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.messagebox?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/class.AlertDialog.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nsalert)
