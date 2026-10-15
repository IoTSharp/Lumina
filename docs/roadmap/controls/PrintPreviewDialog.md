# PrintPreviewDialog

⬜ **现状：**无标准预览对话框；Form/ToolStrip/PrintPreviewControl 的共同前置尚不完整。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/PrintPreviewDialog.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：同名标准类型尚未实现，无可冒充为完整契约的源码。
- 排序：Form/ShowDialog、PrintPreviewControl、PrintDialog/PrintDocument、ToolStrip。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P1 / PrintPreviewDialog-01 | 实现标准 PrintPreviewDialog : Form、Document、PrintPreviewControl 只读属性、UseAntiAlias；继承的隐藏属性/事件仍按固定 API 验收，不忽略分母。 |
| ⬜ | P2 / PrintPreviewDialog-02 | 原生工具栏提供打印、缩放、单/多页、页码和关闭的标准交互；复用现有 ToolStrip 与标准控件，静态布局留 .Designer/.resx。 |
| ⬜ | P2 / PrintPreviewDialog-03 | Document 替换/InvalidatePreview、页数/StartPage/AutoZoom 联动；打印调用走同一标准 PrintDocument，不打印窗口截图。 |
| ⬜ | P2 / PrintPreviewDialog-04 | ShowDialog owner/Cancel/Esc、默认尺寸/最小尺寸、关闭后资源与再次显示按官方探针；文档异常、用户取消不被误记为成功。 |
| ⬜ | P2 / PrintPreviewDialog-05 | 校准工具栏键盘/焦点、辅助技术、RTL/DPI 与本地化文字，Document 属性设计器序列化保持标准。 |
| ⬜ | P2 / PrintPreviewDialog-06 | Windows 用真实 Form HWND + 原生工具控件 + PrintPreviewControl；无必要新建独立预览 App。 |
| ⬜ | P2 / PrintPreviewDialog-07 | Linux 用 GtkWindow/原生 toolbar widgets 与页显示；只有在 Graphics/PrintDocument 前置通过后才进入功能验收，不单纯调用系统 PDF 查看器冒充标准窗口。 |
| ⬜ | P3 / PrintPreviewDialog-08 | macOS 用 NSWindow/NSPanel + 原生控件/PDFView 组合；系统 NSPrintPanel 的预览不是可直接替换整个 PrintPreviewDialog public surface。 |
| ⬜ | 验收 / PrintPreviewDialog-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| Form 与共享预览/打印控件组成，保留标准 modal API。 | GtkWindow/工具控件/页显示组合，文档生成阻塞明确。 | NSPanel/PDFView 原生组合，WinForms 工具栏/页布局语义需适配。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.printpreviewdialog?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/class.Window.html)
- [Apple 原生依据](https://developer.apple.com/documentation/pdfkit/pdfview)
