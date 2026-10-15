# PageSetupDialog

⬜ **现状：**无页面设置对话框，缺 PageSettings/PrinterSettings 的标准读写与平台映射。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/PageSetupDialog.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：同名标准类型尚未实现，无可冒充为完整契约的源码。
- 排序：CommonDialog、System.Drawing.Printing.PageSettings/PrinterSettings、PrintDocument。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P1 / PageSetupDialog-01 | 实现 PageSetupDialog : CommonDialog，Document/PageSettings/PrinterSettings、Reset 和准确 ShowDialog/HelpRequest 契约。 |
| ⬜ | P2 / PageSetupDialog-02 | 补 AllowMargins/AllowOrientation/AllowPaper/AllowPrinter、EnableMetric、MinMargins、ShowHelp/ShowNetwork，逐默认/动态效果/无效值验收。 |
| ⬜ | P2 / PageSetupDialog-03 | 设置方向、纸张、来源、边距和最小边距，校准百分之一英寸/毫米转换与 EnableMetric；切换打印机/纸张保留合法值。 |
| ⬜ | P2 / PageSetupDialog-04 | 确认/取消回写边界、初始 Document 与 PageSettings 谁为准、无打印机/非法纸张/驱动错误/重复显示和 Reset 与官方对照。 |
| ⬜ | P2 / PageSetupDialog-05 | 组件设计器可序列化设置对象/.resx；AOT 实际分页内容验证，native 内存与 caller-owned settings 归属明确。 |
| ⬜ | P2 / PageSetupDialog-06 | Windows 用 PageSetupDlgW/PAGESETUPDLGW、DEVMODE/DEVNAMES/HGLOBAL，PSD_* 与帮助 callback 处理在库内。 |
| ⬜ | P2 / PageSetupDialog-07 | Linux 用 GtkPageSetupUnixDialog/GtkPageSetup + PrintSettings（按固定版本选择）；精确禁用原生各区域/ShowNetwork 的可配置范围先验证。 |
| ⬜ | P3 / PageSetupDialog-08 | macOS 使用 NSPageLayout/NSPrintInfo；最小边距与系统可打印区域差异清楚，Windows 特有帮助/驱动字段例外但普通纸张/方向/边距仍必须实现。 |
| ⬜ | 验收 / PageSetupDialog-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| PageSetupDlgW 系统页面设置，统一页单位与设置归属。 | GtkPageSetupUnixDialog/GtkPageSetup，依赖标准打印类型兼容路线。 | NSPageLayout 原生面板，NSPrintInfo 转换需前置支持。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.pagesetupdialog?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/class.PageSetupUnixDialog.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nspagelayout)
