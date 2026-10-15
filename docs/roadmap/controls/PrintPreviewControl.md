# PrintPreviewControl

⬜ **现状：**无同名预览控件。普通 Panel/PictureBox 不能直接抵扣多页、缩放、分页与 PrintDocument 事件契约。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/PrintPreviewControl.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：同名标准类型尚未实现，无可冒充为完整契约的源码。
- 排序：Control/滚动、System.Drawing.Printing.PrintDocument/PreviewPrintController、Graphics 前置。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P1 / PrintPreviewControl-01 | 实现标准 PrintPreviewControl 与 Document、AutoZoom、Zoom、Rows、Columns、StartPage、UseAntiAlias、StartPageChanged/InvalidatePreview；逐属性范围/默认/变更核验。 |
| ⬜ | P1 / PrintPreviewControl-02 | Document 渲染实际分页，BeginPrint/QueryPageSettings/PrintPage/EndPrint 与 HasMorePages/Cancel 在预览路径保持标准语义；不能把一张截图当全部打印页。 |
| ⬜ | P2 / PrintPreviewControl-03 | 实现 AutoZoom 与显式 Zoom 切换、行列预览、StartPage 钳制/事件、滚动与 resize；空文档、页数改变、刷新和取消恢复有基线。 |
| ⬜ | P2 / PrintPreviewControl-04 | 缓存/重绘只在需更新文档时再生成页，释放旧页/图元文件/打印控制器资源；同一 PrintDocument 被替换或 Dispose 后无残留事件。 |
| ⬜ | P2 / PrintPreviewControl-05 | 键盘/滚轮/高 DPI、页码可访问性、RightToLeft、.resx/Designer Document 属性和真实打印预览内容对照；Graphics 类型身份必须先解决。 |
| ⬜ | P2 / PrintPreviewControl-06 | Windows 原生子 HWND/滚动条承载预览，复用标准 PreviewPrintController 输出与 GDI/GDI+ 纸页绘制；纸面内容绘制与控件原生交互明确区分，没有单个等价 Win32 预览控件。 |
| ⬜ | P2 / PrintPreviewControl-07 | Linux 用原生 GtkScrolledWindow/工具控件承载纸页结果，可借 GtkPrintOperation 预览协议；PrintPage.Graphics 到 Cairo/PDF 的等价转换是独立阻塞，不因 native picker 可用就宣称预览可用。 |
| ⬜ | P3 / PrintPreviewControl-08 | macOS 原生 PDFKit PDFView 可显示文档页，需先把标准 PrintPage/Graphics 输出可靠转换为 PDF；PDFView 自带缩放/页排列不等于 Rows/Columns/StartPage 全契约。 |
| ⬜ | 验收 / PrintPreviewControl-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| 原生 HWND/滚动 + 标准打印页渲染，WinForms 自身也需要预览绘制语义；不造新绘图公共 API。 | GtkScrolledWindow 与原生控件，纸页来自经验证的共享打印管线；无一对一预览控件。 | PDFKit PDFView 是候选原生页显示组件，前置为可兼容的页生成/图形类型。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.printpreviewcontrol?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/class.PrintOperation.html)
- [Apple 原生依据](https://developer.apple.com/documentation/pdfkit/pdfview)
