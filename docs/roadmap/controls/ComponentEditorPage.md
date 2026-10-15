# ComponentEditorPage

⬜ **现状：**缺少 System.Windows.Forms.Design.ComponentEditorPage；标准派生 Panel 与组件编辑页面生命周期要纳入责任，优先级低于业务控件。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/ComponentEditorPage.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：同名标准类型尚未实现，无可冒充为完整契约的源码。
- 排序：Panel、IComponentEditorPageSite/IComponent、ComponentEditorForm；P2/3。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P2 / ComponentEditorPage-01 | 实现标准 abstract ComponentEditorPage : Panel，GetControl/SetComponent/SetSite、Icon/Title/CommitOnDeactivate 与受保护 Component/PageSite/Loading/FirstActivate/LoadRequired。 |
| ⬜ | P2 / ComponentEditorPage-02 | 实现 Activate/Deactivate、LoadComponent/ReloadComponent/SaveComponent、ApplyChanges/OnApplyComplete 的准确调用顺序，首次加载与后续激活不重复保存。 |
| ⬜ | P2 / ComponentEditorPage-03 | EnterLoadingMode/ExitLoadingMode/IsLoading/IsFirstActivate/SetDirty 与 IComponentEditorPageSite 的 dirty 通知协作；加载期间设属性不错误标 dirty。 |
| ⬜ | P2 / ComponentEditorPage-04 | 支持 IsPageMessage/ShowHelp/SupportsHelp、CommitOnDeactivate、组件更换、验证失败/保存异常/取消编辑与 Dispose 解除关联。 |
| ⬜ | P2 / ComponentEditorPage-05 | 派生页面可静态 Designer 初始化，.resx Icon/Font/本地化使用原标准类型，Native AOT 保留派生构造与虚调用；不要新增替代编辑页 API。 |
| ⬜ | P2 / ComponentEditorPage-06 | Windows 用 Panel HWND 与原生子控件，共享普通容器布局/焦点，页面专属部分只处理标准编辑生命周期。 |
| ⬜ | P3 / ComponentEditorPage-07 | Linux 用 GtkFixed/GtkScrolledWindow 原生容器和标准事件桥；页面数据加载/保存可共享，IsPageMessage 的 Win32 参数明确平台专属。 |
| ⬜ | P3 / ComponentEditorPage-08 | macOS 用 NSView/NSScrollView 原生容器；共享组件 dirty/apply 状态机，公开 Icon/Graphics 类型前置未解决时保持阻塞。 |
| ⬜ | 验收 / ComponentEditorPage-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| Panel 原生子 HWND，共享容器实现与标准编辑生命周期。 | GTK 原生容器，受保护 Win32 消息扩展不是可移植等价。 | AppKit 原生容器，共享页面生命周期而非新建 IDE/设计器。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.design.componenteditorpage?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/class.Fixed.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nsview)
