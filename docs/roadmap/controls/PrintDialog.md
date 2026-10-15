# PrintDialog

⬜ **现状：**无 PrintDialog；System.Drawing.Printing 的 PrinterSettings/PrintDocument 在 Windows 依赖可用性和 AOT 路径需另验。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/PrintDialog.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：同名标准类型尚未实现，无可冒充为完整契约的源码。
- 排序：CommonDialog、PrinterSettings/PrintDocument/打印设备；打印管线前置。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P1 / PrintDialog-01 | 实现 PrintDialog : CommonDialog、Document/PrinterSettings、Reset、ShowDialog(IWin32Window)，选中设置回写原同一标准对象，不能换成自创 printer DTO。 |
| ⬜ | P2 / PrintDialog-02 | 补 AllowCurrentPage/AllowSelection/AllowSomePages/AllowPrintToFile、PrintToFile、ShowHelp/ShowNetwork、UseEXDialog 的默认、启用与结果。 |
| ⬜ | P2 / PrintDialog-03 | 校准页范围、CurrentPage/Selection/SomePages、份数/逐份/双面/方向/纸张及打印机切换，Document 与 PrinterSettings 交叉赋值关系按基线。 |
| ⬜ | P2 / PrintDialog-04 | 确认/取消/Apply 的 DialogResult 和回写时机、HelpRequest、owner 模态/焦点、无默认打印机/设备断开/驱动错误不混为成功。 |
| ⬜ | P2 / PrintDialog-05 | .resx/设计器组件 tray、AOT 打印到 PDF 实际结果、native DEVMODE/DEVNAMES 与设置对象资源归属逐项验证。 |
| ⬜ | P2 / PrintDialog-06 | Windows 用 PrintDlg/PrintDlgEx 和标准 printer settings 互操作，UseEXDialog 区分 native 路径；生成 interop/COM，避免 runtime-only marshaling。 |
| ⬜ | P2 / PrintDialog-07 | Linux 用 GtkPrintDialog 或固定版本 GtkPrintUnixDialog/GtkPrintOperation；原生 print settings 映射 PrintRange/份数，Windows 驱动 DEVMODE/ShowNetwork 等专属项明确单列。 |
| ⬜ | P3 / PrintDialog-08 | macOS 用 NSPrintPanel/NSPrintInfo/NSPrintOperation，选择与执行分开适配；macOS panel 不能天然返回 .NET PrinterSettings，公共打印类型身份仍阻塞。 |
| ⬜ | 验收 / PrintDialog-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| 系统 PrintDlg/PrintDlgEx，驱动设置借用/拥有语义清楚。 | GTK 原生打印选择器，固定版本后选择最短 API；Windows 打印对象普通能力不能随意排除分母。 | NSPrintPanel/NSPrintInfo，需标准 .NET 打印对象与 native settings 的可支持转换。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.printdialog?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/class.PrintDialog.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nsprintpanel)
