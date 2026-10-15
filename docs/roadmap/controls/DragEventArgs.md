# DragEventArgs

⬜ **现状：**无标准拖放事件参数；仅补一个属性袋不足以实现实际原生拖放协商。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/DragEventArgs.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：同名标准类型尚未实现，无可冒充为完整契约的源码。
- 排序：Control.AllowDrop/DoDragDrop、IDataObject/DataObject、DragDropEffects/DropImageType。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P1 / DragEventArgs-01 | 实现两个固定构造重载，Data/KeyState/X/Y/AllowedEffect 只读、Effect 可写，以及 DropImageType/Message/MessageReplacementToken 的 .NET10 表面与默认。 |
| ⬜ | P1 / DragEventArgs-02 | DragEnter→DragOver→DragLeave/DragDrop 生成准确参数，屏幕坐标/按键状态/允许效果与业务修改 Effect 同步回原生 drag protocol。 |
| ⬜ | P1 / DragEventArgs-03 | 实现 Copy/Move/Link/None/Scroll 与 modifier 的交互；业务拒绝拖放、外部源改变支持效果、数据缺格式和 Drop 期间异常不被误当成功。 |
| ⬜ | P2 / DragEventArgs-04 | 同进程/跨进程文件、文本、富文本/图像，异步取数和目标销毁/拖动取消/离开再进入的事件顺序对照；GiveFeedback/QueryContinueDrag 共享状态。 |
| ⬜ | P2 / DragEventArgs-05 | DropImageType 与消息 token 的 native drop description 对接，替换文本长度/Unicode/格式约定明确；系统不支持时单列差异，不能假称显示过反馈。 |
| ⬜ | P1 / DragEventArgs-06 | Windows OLE IDropTarget/IDropSource/DoDragDrop，IDataObject/坐标/MK_* 与 DROPEFFECT/SHDRAGIMAGE 等路径 AOT-safe；不只用 WM_DROPFILES 覆盖文件子集。 |
| ⬜ | P2 / DragEventArgs-07 | Linux 用 GtkDropTarget/GtkDropTargetAsync/GtkDragSource 与 GdkDragAction；Wayland 不保证全局 X/Y，screen 坐标兼容须标范围，其他协商/取消能力仍必须实现。 |
| ⬜ | P3 / DragEventArgs-08 | macOS 用 NSDraggingDestination/NSDraggingSession/NSPasteboard；坐标从原生 screen/view 转换，AllowedEffect/Effect 与 NSDragOperation 协商，Windows drop description 无完全对应。 |
| ⬜ | 验收 / DragEventArgs-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| 标准 OLE 拖放参数与结果映射，所有 native 细节在库内。 | GTK drag/drop controllers 与内容 provider，事件参数不是 native widget，不应为它造控件。 | AppKit dragging destination/session，数据/坐标/操作反馈逐项转换。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.drageventargs?view=windowsdesktop-10.0)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/class.DropTarget.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nsdraggingdestination)
