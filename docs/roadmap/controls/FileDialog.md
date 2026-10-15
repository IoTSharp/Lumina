# FileDialog

⬜ **现状：**标准 FileDialog 基类缺失；OpenFileDialog/SaveFileDialog 独立实现导致过滤/默认/生命周期重复，FileOk 可取消与现代选项不完整。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/FileDialog.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：同名标准类型尚未实现，无可冒充为完整契约的源码。
- 排序：CommonDialog、FileDialogCustomPlace/Collection、AOT-safe Common Item Dialog；先于打开/保存细节。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P0 / FileDialog-01 | 实现 abstract FileDialog : CommonDialog 与共享 FileOk/Reset/RunDialog/HookProc，OpenFileDialog/SaveFileDialog 继承正确，保留受保护 Instance/Options 等扩展契约。 |
| ⬜ | P0 / FileDialog-02 | 共享 FileName/FileNames、Filter/FilterIndex、InitialDirectory/DefaultExt/Title、AddExtension/CheckFileExists/CheckPathExists 的默认、校验、回读和 Reset。 |
| ⬜ | P1 / FileDialog-03 | 补 AddToRecent、AutoUpgradeEnabled、ClientGuid、CustomPlaces、DereferenceLinks、OkRequiresInteraction、RestoreDirectory、ShowHelp/ShowHiddenFiles/ShowPinnedPlaces、SupportMultiDottedExtensions、ValidateNames 全部固定 .NET10 表面。 |
| ⬜ | P1 / FileDialog-04 | FileOk.Cancel 在 native 关闭前 veto；本次选项更改/多选/过滤/追加扩展与 FileOk 触发顺序对照，取消不得先关闭后重新打开。 |
| ⬜ | P1 / FileDialog-05 | 实现无效 Filter、零/越界 FilterIndex、Unicode/长路径/UNC/符号链接、非文件系统 shell item、复用/Reset 与取消结果；路径处理不自创限制。 |
| ⬜ | P1 / FileDialog-06 | Windows 现代 IFileDialog 与旧 OPENFILENAMEW 路径共享托管规则，interop 使用生成 COM/显式 vtable 等 AOT-safe 手段，不依赖运行时生成 RCW。 |
| ⬜ | P2 / FileDialog-07 | Linux GtkFileDialog/portal 的 filter/初始目录/多个返回与同步 ShowDialog 适配；ClientGuid/CustomPlaces 等 Windows Shell 配置逐项例外，FileOk veto 能力先原型核实。 |
| ⬜ | P3 / FileDialog-08 | macOS NSOpenPanel/NSSavePanel + UTType/filter/delegate，扩展和路径策略按平台文件系统转换；普通 Font/Image/.resx 能力仍受统一跨平台前置，不剔除分母。 |
| ⬜ | 验收 / FileDialog-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| 默认 Common Item Dialog/需要的旧路径；共享标准 FileDialog，避免打开/保存重复逻辑。 | GtkFileDialog 原生选择器/portal，异步调用共享 modal loop。 | NSOpenPanel/NSSavePanel，原生 delegate 校验与 WinForms FileOk 取消连接。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.filedialog?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/class.FileDialog.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nsopenpanel)
