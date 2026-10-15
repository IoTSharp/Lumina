# AccessibleObject

⬜ **现状：**无标准 AccessibleObject。借用原生控件的默认 accessibility 只能覆盖一部分，不能替代派生对象/可访问树/通知 API。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/AccessibleObject.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：同名标准类型尚未实现，无可冒充为完整契约的源码。
- 排序：Control.AccessibilityObject/CreateAccessibilityInstance、各原生控件语义、静态 COM/AT-SPI/AppKit bridge。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P1 / AccessibleObject-01 | 实现标准 AccessibleObject 的虚属性 Name/Description/Role/State/Value/Bounds/Parent/DefaultAction/Help/KeyboardShortcut 与准确 setter；用户派生对象覆盖可用。 |
| ⬜ | P1 / AccessibleObject-02 | 实现 GetChildCount/GetChild/GetFocused/GetSelected/Navigate/HitTest/Select/DoDefaultAction/GetHelpTopic，virtual tree 与原生控件树保持同一焦点/选择。 |
| ⬜ | P1 / AccessibleObject-03 | 实现 RaiseAutomationNotification/RaiseLiveRegionChanged、Control.AccessibilityNotifyClients 和值/选择/结构/焦点变化通知；事件真实发往辅助技术而非只触发托管事件。 |
| ⬜ | P1 / AccessibleObject-04 | 原生控件优先复用系统 provider，仅对兼容逻辑项/组合控件补节点；避免既有 native child 又重复托管 child 导致读屏双读。 |
| ⬜ | P2 / AccessibleObject-05 | 验证坐标/DPI/RTL、动态增删项、隐藏/禁用/只读/展开/选中状态、动作和键盘快捷键，使用实际读屏软件和原生检查器。 |
| ⬜ | P2 / AccessibleObject-06 | Dispose/Handle 重建后 native provider 引用不会调用已释放对象；AOT COM CCW/回调静态可达，不依赖运行时生成代理。 |
| ⬜ | P1 / AccessibleObject-07 | Windows 接入 UI Automation/MSAA，标准 HWND provider 与派生 AccessibleObject 互操作；UseStdAccessibleObjects 的 Windows 扩展契约保留。 |
| ⬜ | P2 / AccessibleObject-08 | Linux 使用 GtkAccessible/AT-SPI，角色、属性、关系、状态和动作映射；Win32 MSAA 对象 ID/COM 专属方法单列，不因平台不同省略普通语义。 |
| ⬜ | P3 / AccessibleObject-09 | macOS 使用 NSAccessibility 协议，view/逻辑 item 的 parent/children/actions/notifications 与 WinForms 对象映射；不能仅设置 accessibilityLabel 当完整实现。 |
| ⬜ | 验收 / AccessibleObject-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| UIA/MSAA 原生 provider + 标准托管扩展，先确保 native 子控件不重复暴露。 | GtkAccessible/AT-SPI，原生 controls 默认实现加逻辑节点与通知桥。 | NSAccessibility，native views 与虚拟条目暴露系统可理解语义。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.accessibleobject?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/iface.Accessible.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nsaccessibility)
