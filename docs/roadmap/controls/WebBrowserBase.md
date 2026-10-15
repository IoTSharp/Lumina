# WebBrowserBase

⬜ **现状：**无标准 WebBrowserBase；它不是通用跨平台浏览器抽象，而是标准 WebBrowser 的 ActiveX 宿主基础。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/WebBrowserBase.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：同名标准类型尚未实现，无可冒充为完整契约的源码。
- 排序：Control/NativeWindow、STA/OLE COM 宿主与 WebBrowserSiteBase。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P1 / WebBrowserBase-01 | 保持 WebBrowserBase : Control 的标准抽象/构造/派生契约，补 ActiveXInstance、AttachInterfaces/DetachInterfaces、CreateSink/DetachSink、CreateWebBrowserSiteBase，不误改成 AxHost 继承。 |
| ⬜ | P2 / WebBrowserBase-02 | 实现 OLE in-place activation、可见/尺寸/焦点/parent 切换、handle 重建与 native site 关系；初次激活与再次显示不会重复创建 COM 对象。 |
| ⬜ | P2 / WebBrowserBase-03 | PreProcessMessage/ProcessDialogKey/ProcessMnemonic/IsInputChar、输入法、Tab/快捷键、HTML 内部焦点与外层 WinForms 验证语义逐项对照。 |
| ⬜ | P2 / WebBrowserBase-04 | 核对隐藏/不支持的继承属性与事件（BackColor/Font/Paint 等）的标准行为和异常；不能因基类存在就无条件转发全部 Control 事件。 |
| ⬜ | P2 / WebBrowserBase-05 | COM 事件 sink/对象/窗口过程的创建/销毁顺序和异常边界固定，Dispose 后断开连接点和 native 回调；AOT 下静态接口适配真实可运行。 |
| ⬜ | P2 / WebBrowserBase-06 | Windows 用标准 WebBrowser COM/OLE site 与原生 child HWND，保留可派生受保护接口；不要求消费者手动释放底层 COM。 |
| ⬜ | P2 / WebBrowserBase-07 | Linux 🚫 ActiveXInstance、WebBrowserSiteBase/OLE 消息没有 GTK 原生等价，WebKitGTK 的实例指针不得冒充标准 ActiveX 对象。 |
| ⬜ | P3 / WebBrowserBase-08 | macOS 🚫 WKWebView/NSView 的 delegate 不是 OLE site；普通 Control 语义可共享，ActiveX 契约保持明确平台专属。 |
| ⬜ | 验收 / WebBrowserBase-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| Control 原生 HWND + OLE in-place site/连接点，兼容标准派生扩展。 | 无 OLE/ActiveX native 对应，网页子集不能抵扣此宿主类型。 | 无 OLE/ActiveX native 对应，WKWebView 仅另作可移植网页能力研究。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.webbrowserbase?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://webkitgtk.org/reference/webkitgtk/stable/class.WebView.html)
- [Apple 原生依据](https://developer.apple.com/documentation/webkit/wkwebview)
