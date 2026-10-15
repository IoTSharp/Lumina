# WebBrowser

⬜ **现状：**无 WebBrowser。标准继承 WebBrowserBase，依赖 Windows WebBrowser ActiveX/IE COM DOM；能打开网页或替成 WebView2/WebKit 不等于兼容。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/WebBrowser.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：同名标准类型尚未实现，无可冒充为完整契约的源码。
- 排序：WebBrowserBase/OLE/COM、HtmlDocument/HtmlElement/事件、AOT 静态 interop 可行性原型。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P1 / WebBrowser-01 | 先做 Windows Native AOT 原型：静态 COM 接口、连接点事件、导航和 HtmlDocument 访问；给出可运行证据与剩余阻塞，再承诺此类型 ≥98%，不以空壳计完成。 |
| ⬜ | P2 / WebBrowser-02 | 实现 Navigate 全重载、Url/DocumentText/DocumentStream、GoBack/GoForward/GoHome/GoSearch/Refresh/Stop 与 IsBusy/ReadyState/CanGoBack/CanGoForward 的双向状态。 |
| ⬜ | P2 / WebBrowser-03 | 对齐 Navigating.Cancel、Navigated、DocumentCompleted、ProgressChanged、NewWindow.Cancel、FileDownload；frame/重定向/取消/错误路径顺序不能简化为每次导航一次完成。 |
| ⬜ | P2 / WebBrowser-04 | HtmlDocument/HtmlElement/HtmlWindow、Document.InvokeScript、ObjectForScripting 的静态接口/IDispatch 适配、DOM 事件订退与 GC/COM 生命周期明确；任意未知托管类型脚本暴露是 AOT 重点限制。 |
| ⬜ | P2 / WebBrowser-05 | 支持 ScriptErrorsSuppressed、ScrollBarsEnabled、AllowNavigation/AllowWebBrowserDrop、快捷键/上下文菜单、DocumentTitle/StatusText/EncryptionLevel 与打印/保存/属性对话框等完整成员。 |
| ⬜ | P2 / WebBrowser-06 | Windows 承载标准 WebBrowser ActiveX、STA/OLE in-place activation 与 COM connection points；源生成或显式 ABI 的 AOT-safe 已知接口适配，不能用运行时生成代理兜底。 |
| ⬜ | P2 / WebBrowser-07 | Linux 🚫 WebKitGTK WebView 是 native 浏览器候选，但引擎/COM DOM/脚本/事件模型不同；仅能另行标注可移植网页子集，不能算标准 Windows WebBrowser 完全等价。 |
| ⬜ | P3 / WebBrowser-08 | macOS 🚫 WKWebView 不提供 IE COM DOM/ObjectForScripting/ActiveX，navigation delegate 可支持有限网页交互但不抵扣完整基线；不得暗中替换引擎后宣布 ≥98%。 |
| ⬜ | 验收 / WebBrowser-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| 标准 WebBrowser ActiveX + 静态已知 COM 适配，先用小原型确认 AOT 可行性。 | WebKitGTK 原生 WebView 仅可移植网页子集，COM/IE 行为明确 🚫。 | WKWebView 原生网页组件，WinForms DOM/ActiveX 契约不等价。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.webbrowser?view=windowsdesktop-10.0)
- [Native AOT 原生互操作](https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/interop)
- [GTK/GLib 原生依据](https://webkitgtk.org/reference/webkitgtk/stable/class.WebView.html)
- [Apple 原生依据](https://developer.apple.com/documentation/webkit/wkwebview)
