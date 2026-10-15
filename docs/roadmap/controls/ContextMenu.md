# ContextMenu

⬜ **现状：**无同名类型。官方10.0.11无参构造 PNSE，不能用 ContextMenuStrip 已有 popup 能力抵扣其类型，更不能称此基线支持可用旧上下文菜单。

- 基线：固定 .NET10 WinForms 10.0.11 / `b83f534`；评估日期 2026-10-15。
- 类型：`System.Windows.Forms.ContextMenu`。 [全部成员、继承及接口任务](../api/ContextMenu.md) · [98%口径](../coverage.md) · [公共前置](../foundations.md) · [平台条件](../platforms.md)。
- 排序：P2：固定类型/异常兼容；P3：可选恢复旧应用菜单。
- 证据：[官方10.0.11旧类型运行探针](../../assessments/2026-10-15/legacy-control-probes.json)；未单独探测的构造/成员不自动视为通过。

## 顺序任务

| 状态 | 优先级 / ID | 细节与验收 |
| --- | --- | --- |
| ⬜ | P2 / ContextMenu-01 | 补 ContextMenu : Menu、两种构造、Show(Control,Point)/Show(Control,Point,LeftRightAlignment)、RightToLeft/SourceControl、Popup/Collapse 的准确表面。 |
| ⬜ | P2 / ContextMenu-02 | 按持久证据保留无参构造的标准 PNSE；其他构造/非法参数/受保护 ProcessCmdKey/OnPopup/OnCollapse 的可达与异常范围需官方对照，不能假定全部同态。 |
| ⬜ | P2 / ContextMenu-03 | Control.ContextMenu/旧菜单赋值的过时元数据和类型编译测试保持；官方不能实例化的正常事实在设计器/迁移说明中清楚标出。 |
| ⬜ | P2 / ContextMenu-04 | 公开属性、事件、方法仍在全量清单，状态分“官方不可执行”与“Lumina未实现”；支持率不能把二者混成已提供功能。 |
| ⬜ | P3 / ContextMenu-05 | 若另加历史 UI：Windows TrackPopupMenuEx/HMENU，Popup→点击→Collapse、SourceControl、右键/Menu键/Shift+F10、位置对齐/关闭焦点与动态菜单独立验收。 |
| ⬜ | P3 / ContextMenu-06 | 历史扩展共用 NativeMenu popup/资源处理，IContainer/Dispose/Clone/Merge 的归属按所选.NETFramework基线固定，不推翻现代 ContextMenuStrip。 |
| ⬜ | P3 / ContextMenu-07 | Linux 可选 GtkPopoverMenu，使用 native widget 相对位置；Wayland 全局屏幕坐标/LeftRightAlignment 精确定位限制单列。 |
| ⬜ | P3 / ContextMenu-08 | macOS 可选 NSMenu 对源 NSView 弹出，事件/键盘/来源语义需适配；固定.NET10异常兼容不要求窗口显示。 |
| ⬜ | 验收 / ContextMenu-V | 对完整 API 表逐成员核对签名、默认、修改限制、异常、状态和事件轨迹；真实 WinForms 与 Lumina 使用同场景。AOT发布后实际运行、资源/回调释放、键盘/可访问性分别留证，Linux/macOS不借Windows结果抵扣。固定基线的兼容占位/标准异常与可选历史UI分别报告，不把异常一致称作原控件已可用。 |

## 原生方案与边界

| 平台 | 实现 |
| --- | --- |
| Windows | 固定10.0.11 PNSE；旧 UI 扩展才需 HMENU。 |
| Linux | 异常占位基线，无需原生菜单实例；可选 GtkPopoverMenu。 |
| macOS | 异常占位基线，无需原生菜单实例；可选 NSMenu。 |

普通属性/事件/行为按平台各自验收；Windows消息/句柄等真实专属契约单列🚫，但不抵扣Windows未实现项。Font/Image/Icon/Bitmap/Graphics公共类型的跨平台身份按统一平台计划先解决，不靠排除分母降低目标。

## 修改历史

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

## 原始资料

- [微软固定API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.contextmenu?view=windowsdesktop-10.0)
- [微软WFDEV006：旧菜单/控件仅为二进制兼容恢复](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/wfdev-diagnostics/wfdev006)
- [GTK/GIO原生依据](https://docs.gtk.org/gtk4/class.PopoverMenu.html)
- [AppKit原生依据](https://developer.apple.com/documentation/appkit/nsmenu)
