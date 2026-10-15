# PrintDocument

🔎 **现状：**这是 System.Drawing.Printing.PrintDocument 依赖契约，不属于 Lumina.Forms 命名空间；Windows 应先评估复用既有类型，不能新增同名 Lumina.Forms.PrintDocument 把源码迁移变成重写。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/PrintDocument.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：标准依赖来自 System.Drawing.Printing，仓库没有对应实现文件；优先验证原类型复用，不在 Lumina.Forms 创建同名新类型。
- 排序：System.Drawing.Common/Printing、Graphics/PrinterSettings/PageSettings/PrintController；跨平台类型身份 P0 原型。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P0 / PrintDocument-01 | 固定真实 System.Drawing.Printing.PrintDocument 类型身份，检查现有依赖与 Native AOT 下可用路径；机器清单 Lumina 同名缺失不等于应复制第三方类型。 |
| ⬜ | P1 / PrintDocument-02 | 逐项验证 DocumentName、DefaultPageSettings、PrinterSettings、PrintController、OriginAtMargins、Print() 与 Component 释放；是否可直接复用用真实 AOT 样例结论决定。 |
| ⬜ | P1 / PrintDocument-03 | 捕获 BeginPrint→每页 QueryPageSettings/PrintPage→EndPrint，HasMorePages/Cancel/异常与 PrintAction 的准确顺序；预览/真实打印两路均验证。 |
| ⬜ | P2 / PrintDocument-04 | PrintPageEventArgs.Graphics/PageBounds/MarginBounds、硬边距、横竖纸、页码/纸张/分辨率变化与 OriginAtMargins 对照；不另引入自创绘图回调。 |
| ⬜ | P2 / PrintDocument-05 | 打印到 PDF/实际设备、取消、无设备、驱动设置、不同文化单位、连续多页和重新打印；原生打印控制器及 page images/HDC 完整释放。 |
| ⬜ | P2 / PrintDocument-06 | Windows 优先复用受支持标准打印实现，AOT 出现问题追到具体 GDI/COM/marshaling 路径修适配；不能全局 suppress 告警宣称打印完成。 |
| ⬜ | P2 / PrintDocument-07 | Linux GTK 原生打印用 GtkPrintOperation/PrintDialog 与 Cairo，但调用方仍构造标准 PrintDocument/Graphics 时不自动跨平台；先冻结可保持类型身份的方案，否则明确阻塞，不剔除普通打印功能。 |
| ⬜ | P3 / PrintDocument-08 | macOS NSPrintOperation/NSPrintInfo/CoreGraphics 原生打印同样受标准 Graphics/Printing 类型前置；PDF 输出/纸页绘制必须实际通过才可扩展预览与对话框。 |
| ⬜ | 验收 / PrintDocument-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| System.Drawing.Printing.PrintDocument 既有依赖 + native printing，先验证复用而非重造。 | GtkPrintOperation/PrintDialog 是 native 候选，但不解决公开 .NET 打印/Graphics 类型支持。 | NSPrintOperation/CoreGraphics 是 native 候选，保持标准类型身份是继续条件。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.drawing.printing.printdocument?view=windowsdesktop-10.0)
- [System.Drawing.Common 的 Windows 平台限制](https://learn.microsoft.com/en-us/dotnet/core/compatibility/core-libraries/6.0/system-drawing-common-windows-only)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/class.PrintOperation.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nsprintoperation)
