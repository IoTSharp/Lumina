# 原生跨平台：实现选择与进入条件

Windows 兼容优先推进；Linux/macOS 先做受限原型，确认可行再扩展全部控件。原型不是另一套产品 API。用户接受的“可移植 ≥98%”仍要求真实事件、属性和行为，不能靠平台空实现通过。

| 领域 | Windows | Linux | macOS |
| --- | --- | --- | --- |
| 窗口/基础控件 | HWND、USER32、COMCTL32 | GTK4 的 GtkWindow/GtkWidget，GMainContext | AppKit NSWindow/NSView/NSControl，NSApplication |
| 主循环/UI线程 | GetMessage/TranslateMessage/DispatchMessage 与投递 | GLib 主上下文投递，不能由 Task.Run 操作 GTK | AppKit 主线程/主 run loop，target-action/delegate |
| 菜单/对话框 | HMENU、系统公共对话框 | GtkPopoverMenu、GtkFileDialog 等 | NSMenu、NSOpenPanel/NSSavePanel/NSAlert 等 |
| 表格/树 | ListView/TreeView 原生；DataGridView 需托管组合 | GtkColumnView/GtkListView/GtkTreeListModel | NSTableView/NSOutlineView |
| 可访问性 | 控件默认实现 + UIA/MSAA 补足 | GtkAccessible/AT-SPI | NSAccessibility |
| 自定义绘制 | GDI/GDI+ 等现有契约 | Cairo/Pango 与 GTK snapshot 的适配需验证 | CoreGraphics/CoreText 与 NSView 绘制适配需验证 |

以上是候选实现，**不是已实现状态**。GTK 的样式是 Linux 工具包原生；不承诺每个桌面环境外观完全相同。AppKit 组合必须使用系统控件，不将 NSTableView/NSOutlineView 等同于完整 DataGridView/TreeView。表格虚拟化、编辑、数据绑定、事件顺序仍由库对齐。

## 必须先解决的资源契约

`System.Drawing.Primitives` 的 Point/Size/Rectangle/Color 与 `System.Drawing.Common` 的 Font/Image/Icon/Bitmap/Graphics 不是同一个跨平台问题。现有公开 API、Designer 代码和资源直接使用后者；现代 .NET 的 System.Drawing.Common 是 Windows 专属。仅在内部改用 Cairo 或 CoreGraphics 不能使消费方 `new System.Drawing.Font(...)` 在 Linux/macOS 自动工作。

| ID | 状态 | 具体工作与通过条件 |
| --- | --- | --- |
| XPL-001 | 🔎 | 从真实 Designer 导出的 Font、Bitmap、Icon、ImageList、.resx 各取样例，列出公开类型身份/构造/转换/绘图依赖；不把普通字体图像全部划为 Windows 专属以压低分母 |
| XPL-002 | 🔎 | 查证能否保留这些类型的受支持跨平台实现；若只能更换类型/改源码，写清改动点和设计器往返影响，更新兼容承诺再进入完整后端，不能假装透明替换已解决 |
| XPL-003 | ⬜ | 验证同一静态 Designer 布局与资源在所选平台实际可用；至少中文文本、字体度量、位图、图标、缩放和释放；不引入全仓图形抽象或复制整个 Drawing 栈来跳过原型判断 |

## 原型排序

| 顺序/ID | 状态 | 内容 | 进入下一步的证据 |
| --- | --- | --- | --- |
| 1 XPL-010 | ⬜ | M0/M1 的线程、生命周期、布局契约稳定；冻结上述资源路线，确定首个真实用户平台 | 双目标基线可运行，资源选择有可编译/运行样例 |
| 2 XPL-011 | ⬜ | Linux 选择一套 GTK4 版本及发行版；先不同时兼容 GTK3/Qt；macOS 选择 AppKit，在真实 Mac 上开发 | 明确依赖版本、设备、CPU/RID、工具链 |
| 3 XPL-012 | ⬜ | Form/Button/Label/TextBox/ComboBox/ListBox、菜单/文件对话框、UI投递 | 同源业务操作可用；原生控件类型与事件轨迹记录 |
| 4 XPL-013 | ⬜ | 布局/滚动/DPI/中文IME/剪贴板/拖放/焦点/模态/辅助技术 | 本地交互验收，平台差异逐条入清单 |
| 5 XPL-014 | ⬜ | 各自系统构建 Native AOT；GTK系统依赖、应用打包；macOS .app、资源目录、签名/公证分发 | 干净机器安装、启动、操作、关闭；不得将 Windows 上交叉编译结果当已运行 |
| 6 XPL-015 | ⬜ | 按控件任务顺序扩展列表/树、容器、数据绑定、表格和高级控件 | 每控件三项独立 ≥98%，方法单列达标，未支持项公开 |

原型建议各限制为 2～3 个工程周，输出能否继续的结论；不将这段时间当作完整跨平台后端工期。首个平台选择有实际用户的平台，不同时招聘三套完整团队；若缺 Mac/桌面 Linux 环境，对应状态保持 🔎。

## 已知不能一一对应的范围

- HWND/Win32 Message/WndProc、Windows Shell/OLE/ActiveX 不跨平台等价；保留在 Windows 范围内，不能以随机原生指针冒充 HWND。
- Linux Wayland 的全局窗口定位、跨应用窗口控制与 Windows 不同；X11 特权能力不能代表全部 Linux。
- macOS 的应用级菜单、Command 快捷键、首响应者、模态 sheet 和全屏不能直接照抄 Windows 消息。可移植的事件/结果语义要对齐，平台差异要披露。
- AppKit 没有 WinForms MDI 的直接同等物；GTK4 也没有完整 MDI 子窗口框架。未证明可接受的原生组合前，不声称全量跨平台 MDI。
- WebBrowser 的 IE/COM DOM 与 WebKitGTK/WKWebView 不相同；能显示网页不代表 WebBrowser API 已兼容。
- 托盘在 Linux 桌面环境/协议之间不同；GTK4 没有 GtkStatusIcon，不能引用 GTK3 删除的控件作为已选 GTK4 实现。

官方依据：[System.Drawing.Common 平台限制](https://learn.microsoft.com/en-us/dotnet/core/compatibility/core-libraries/6.0/system-drawing-common-windows-only)、[Native AOT 互操作](https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/interop)、[GTK4 文档](https://docs.gtk.org/gtk4/)、[Wayland](https://docs.gtk.org/gdk4/wayland.html)、[AppKit](https://developer.apple.com/documentation/appkit)、[NSApplication](https://developer.apple.com/documentation/appkit/nsapplication)。
