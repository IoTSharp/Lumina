# HelpProvider

⬜ **现状：**无标准帮助扩展组件，缺设计器扩展属性、F1/上下文帮助和 HelpNavigator 路由。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/HelpProvider.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：同名标准类型尚未实现，无可冒充为完整契约的源码。
- 排序：Component/IExtenderProvider、Control.HelpRequested、ToolTip 与系统帮助。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P1 / HelpProvider-01 | 实现 HelpProvider : Component/IExtenderProvider、CanExtend、HelpNamespace/Tag，Set/GetHelpString/HelpKeyword/HelpNavigator/ShowHelp 与 ResetShowHelp。 |
| ⬜ | P1 / HelpProvider-02 | 设计器扩展属性写入 .Designer.cs/.resx，可绑定同一 provider 至多个控件；清空/重新赋值/移除/销毁控件解除订阅。 |
| ⬜ | P2 / HelpProvider-03 | F1/HelpRequested/窗体 HelpButton 的帮助路由、ShowHelp=false、父子回退、HelpString 弹出与 HelpNamespace+Keyword 的优先级对照。 |
| ⬜ | P2 / HelpProvider-04 | HelpNavigator.Topic/KeywordIndex/TableOfContents 等完整枚举和帮助参数，缺文件/错误 URI/空关键字的标准异常或结果记录。 |
| ⬜ | P2 / HelpProvider-05 | 原生帮助窗口/弹出内容可键盘访问、关闭恢复焦点；Dispose 后 provider 不保留控件引用或悬挂回调。 |
| ⬜ | P2 / HelpProvider-06 | Windows 复用标准 Help/HTML Help 的系统路径（必要时 HtmlHelpW），静态帮助资源可从发布目录定位；不把 CHM 格式改成自创帮助系统。 |
| ⬜ | P2 / HelpProvider-07 | Linux 用 GtkUriLauncher 打开 URL/真实帮助页面，局部 HelpString 用 native tooltip/popover；CHM/HtmlHelp 的目录/索引协议无原生等价，逐项 🚫。 |
| ⬜ | P3 / HelpProvider-08 | macOS 用 NSHelpManager/help book 或原生打开 URL；Windows CHM HelpNavigator 路径不原生等价，但可移植帮助字符串/F1/焦点语义仍需实现。 |
| ⬜ | 验收 / HelpProvider-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| Component 扩展属性 + 原生 Windows Help/HTML Help。 | GtkUriLauncher/原生提示子集，CHM 协议明确例外。 | NSHelpManager/URL/原生帮助提示，Help book 与 CHM 不冒称同格式。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.helpprovider?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/class.UriLauncher.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nshelpmanager)
