# ComponentEditorForm

⬜ **现状：**缺少 System.Windows.Forms.Design.ComponentEditorForm 对应责任。它是引用程序集实际导出的组件编辑窗体，不代表本项目要取代 VS Designer。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/ComponentEditorForm.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：同名标准类型尚未实现，无可冒充为完整契约的源码。
- 排序：Form/ShowDialog、ComponentEditorPage/IComponentEditorPageSite、标准类型构造与 AOT。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P2 / ComponentEditorForm-01 | 在兼容命名空间边界实现标准 ComponentEditorForm(object component, Type[] pageTypes) : Form 与 ShowForm 全重载，不放到错误顶级命名空间。 |
| ⬜ | P2 / ComponentEditorForm-02 | 构建页面选择器/OK/Cancel/Apply/Help 与 native 标准子控件，按页类型建立 ComponentEditorPage；pageTypes 的构造器/派生类型/空值异常以基线为准。 |
| ⬜ | P2 / ComponentEditorForm-03 | 实现页选中、初始 page 索引、PreProcessMessage、OnSelChangeSelector、OnActivated/OnHelpRequested；页面 Activate/Deactivate 与验证/dirty 生命周期统一。 |
| ⬜ | P2 / ComponentEditorForm-04 | Apply/OK/Cancel 的保存与放弃、CommitOnDeactivate/SupportsHelp、页面加载异常、关闭取消和组件外部变更的状态轨迹对照。 |
| ⬜ | P2 / ComponentEditorForm-05 | Type[] 运行时构造对 Native AOT 的可达构造元数据进行构建期标注/诊断，静态页面列表的真实 AOT 示例先通过；不要求任意未知插件热加载。 |
| ⬜ | P2 / ComponentEditorForm-06 | Windows 由标准 Form、TreeView/列表、Button 和 Panel native hosts 组合，现有 VS Designer 目标仍用真实 WinForms。 |
| ⬜ | P3 / ComponentEditorForm-07 | Linux 用 GtkWindow/原生列表与页容器/按钮组合，普通页面切换和保存语义可移植；Windows IWin32Window/Message 扩展逐项例外。 |
| ⬜ | P3 / ComponentEditorForm-08 | macOS 用 NSPanel/NSOutlineView或原生列表/NSView/NSButton，保留同步 ShowForm 与保存语义；不引入完整跨平台设计器工程。 |
| ⬜ | 验收 / ComponentEditorForm-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| 标准运行时组件编辑窗体，复用 Form/原生控件；VS 设计器仍由真实目标负责。 | GTK 原生组合，运行时组件编辑能力不等于 IDE 设计器重建。 | AppKit 原生组合，反射构造页面的 AOT 前置必须先验证。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.design.componenteditorform?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/class.Window.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nspanel)
