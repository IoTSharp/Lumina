# DataObject

⬜ **现状：**无标准 DataObject/IDataObject 实现；它是剪贴板与拖放的共享多格式数据对象，不应拆成新的仓库数据协议。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/DataObject.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：同名标准类型尚未实现，无可冒充为完整契约的源码。
- 排序：IDataObject/DataFormats、typed data/AOT、Clipboard/DragDrop。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P1 / DataObject-01 | 实现全部 DataObject 构造、IDataObject 接口、Get/SetData/GetDataPresent/GetFormats 的 string/Type/object/autoConvert 重载和虚方法语义。 |
| ⬜ | P1 / DataObject-02 | 支持 Contains/Get/SetText、Audio、Image、FileDropList 的标准便捷 API，原始与转换格式枚举保持一致，不把 Type.FullName 简单当所有 native MIME。 |
| ⬜ | P1 / DataObject-03 | 实现 .NET10 SetDataAsJson<T>/TryGetData<T>/TryGetDataCore<T> 与 resolver/autoConvert；对静态已知类型建立 Native AOT 可验证序列化路径。 |
| ⬜ | P2 / DataObject-04 | 多格式共存、同格式覆盖、大小写/别名、autoConvert=false、null/空值/stream 重复读取/缺少格式的返回与异常逐项比较。 |
| ⬜ | P2 / DataObject-05 | 对象引用的同进程语义与跨进程序列化分开验收；不引入旧 BinaryFormatter 通用回退，按 .NET10 真实可接受类型/行为实现。 |
| ⬜ | P1 / DataObject-06 | Windows 对接 COM IDataObject 的 FORMATETC/STGMEDIUM、枚举/延迟供给/释放；managed 接口与 COM 接口不能混为同一类型随意强转。 |
| ⬜ | P2 / DataObject-07 | Linux 共享托管多格式模型，转换为 GdkContentProvider/MIME，异步读取返回 DataObject；类型/图像前置未通过时保持阻塞而非排除。 |
| ⬜ | P3 / DataObject-08 | macOS 转换 NSPasteboardItem/NSItemProvider 数据与 UTType；同一 data object 在 clipboard/拖放间复用，不复制出第二套公开集合。 |
| ⬜ | 验收 / DataObject-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| 标准托管 IDataObject + 显式原生 COM 适配，COM 内存归属封装库内。 | GdkContentProvider 原生内容桥，格式转换与原始字节保留。 | NSPasteboardItem/native providers 原生数据桥，UTType 与标准格式有明确映射表。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.dataobject?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gdk4/class.ContentProvider.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nspasteboarditem)
