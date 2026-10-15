# Clipboard

⬜ **现状：**无标准剪贴板 API；文本编辑原生可复制不等于 Clipboard 全公开表面、IDataObject 和 .NET10 typed data 已实现。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/Clipboard.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：同名标准类型尚未实现，无可冒充为完整契约的源码。
- 排序：DataObject/IDataObject/DataFormats、STA/OLE、Font/Image 公共图形前置。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P1 / Clipboard-01 | 实现 Clear、Contains/Get/SetText、Audio、FileDropList、Image、Get/SetData、Get/SetDataObject 全重载，格式/空值/异常与固定基线一致。 |
| ⬜ | P1 / Clipboard-02 | 支持 .NET10 SetDataAsJson<T>/TryGetData<T> 与 resolver 重载；类型信息/JSON 静态根/AOT 生成策略用已知实际数据验证，不能退回任意旧 BinaryFormatter 反序列化。 |
| ⬜ | P1 / Clipboard-03 | 校准 TextDataFormat UnicodeText/Text/Html/Rtf/Csv、格式自动转换、DataFormats 名称和原始 bytes/streams；图片、文件 URI、换行与路径 Unicode 互操作。 |
| ⬜ | P2 / Clipboard-04 | SetDataObject(copy,retryTimes,retryDelay) 的持久化/占用重试/STA 约束，GetDataObject 跨进程 native 延迟渲染和应用退出后内容保留分别验收。 |
| ⬜ | P2 / Clipboard-05 | 剪贴板被其他应用改变/读取失败/owner 销毁、bitmap/stream 内存归属与线程切换对照；Windows/Linux/macOS 各与系统原生应用实际复制粘贴。 |
| ⬜ | P1 / Clipboard-06 | Windows 使用 OLE clipboard/IDataObject 与已知 FORMATETC/STGMEDIUM、HGLOBAL/HBITMAP，生成/显式 COM interop 保证 Native AOT；仅文本可先用 USER32，但不能据此标全量通过。 |
| ⬜ | P2 / Clipboard-07 | Linux 用 GdkClipboard/GdkContentProvider 与 MIME 多格式映射；读数据异步需同一主上下文，Wayland clipboard/primary selection 与生命周期限制记录，不伪造 copy=true 永久保存。 |
| ⬜ | P3 / Clipboard-08 | macOS 用 NSPasteboard/UTType 和 native data providers，文件 URL/富文本/图像格式映射；跨进程序列化和 lifetime 不能沿用 Windows HGLOBAL。 |
| ⬜ | 验收 / Clipboard-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| OLE/USER32 标准系统剪贴板，图像/文件/富文本和已知 typed data 都需实际互操作。 | GdkClipboard/GdkContentProvider；现代 compositor 的选择/owner 模型不同，PRIMARY 不是标准 Clipboard 的隐式替代。 | NSPasteboard/NSPasteboardItem，UTType 格式转换与提供者生命周期在内部处理。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.clipboard?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gdk4/class.Clipboard.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nspasteboard)
