# MenuItem

⬜ **现状：**无同名标准类型。官方10.0.11无参构造 PNSE；现有 ToolStripMenuItem 是另一套现代 API，不抵扣 MenuItem 继承/成员责任。

- 基线：固定 .NET10 WinForms 10.0.11 / `b83f534`；评估日期 2026-10-15。
- 类型：`System.Windows.Forms.MenuItem`。 [全部成员、继承及接口任务](../api/MenuItem.md) · [98%口径](../coverage.md) · [公共前置](../foundations.md) · [平台条件](../platforms.md)。
- 排序：P2：固定类型/异常兼容；P3：可选.NETFramework旧菜单行为。
- 证据：[官方10.0.11旧类型运行探针](../../assessments/2026-10-15/legacy-control-probes.json)；未单独探测的构造/成员不自动视为通过。

## 顺序任务

| 状态 | 优先级 / ID | 细节与验收 |
| --- | --- | --- |
| ⬜ | P2 / MenuItem-01 | 补 MenuItem : Menu、全部构造、Click/Popup/Select/DrawItem/MeasureItem、Parent/Index/Text/Checked/Enabled/Visible、Shortcut/ShowShortcut/Mnemonic 的标准签名。 |
| ⬜ | P2 / MenuItem-02 | 补 Break/BarBreak/DefaultItem/RadioCheck/OwnerDraw/MdiList、MergeOrder/MergeType、CloneMenu/MergeMenu/PerformClick/PerformSelect 等剩余表面，不能借现代 ToolStripItem 对象冒充。 |
| ⬜ | P2 / MenuItem-03 | 复用无参 PNSE 证据，其他重载/派生/静态可达路径追加固定基线探针；全量 API 状态显示原始异常/不可执行，未测项目保持待验。 |
| ⬜ | P2 / MenuItem-04 | 固定废弃元数据与事件委托类型、继承可用性、AOT静态引用验证；不通过 unsafe 未初始化对象绕过官方构造异常来人为制造“行为基线”。 |
| ⬜ | P3 / MenuItem-05 | 若恢复旧 UI：Windows native menu item ID、MIIM_*状态与 WM_COMMAND/INITMENUPOPUP/MENUSELECT，Click/Popup/Select 准确顺序、禁用项/快捷键/默认项/单选勾选独立验收。 |
| ⬜ | P3 / MenuItem-06 | 历史 OwnerDraw 的 MeasureItem/DrawItem、Break/BarBreak 多列、MDI窗单、Clone/Merge 与集合移动是额外功能，不计成标准.NET10必须完成。 |
| ⬜ | P3 / MenuItem-07 | Linux 可选 GMenuItem/GAction/GtkPopoverMenu 原生菜单，OwnerDraw/列断点/MDI无完整原生对应；Windows专属外观/消息单列。 |
| ⬜ | P3 / MenuItem-08 | macOS 可选 NSMenuItem，Check/shortcut/native action 可映射，OwnerDraw/多列断点和MDI语义不同，不能近似计成完全支持。 |
| ⬜ | 验收 / MenuItem-V | 对完整 API 表逐成员核对签名、默认、修改限制、异常、状态和事件轨迹；真实 WinForms 与 Lumina 使用同场景。AOT发布后实际运行、资源/回调释放、键盘/可访问性分别留证，Linux/macOS不借Windows结果抵扣。固定基线的兼容占位/标准异常与可选历史UI分别报告，不把异常一致称作原控件已可用。 |

## 原生方案与边界

| 平台 | 实现 |
| --- | --- |
| Windows | 固定基线是不可构造占位；历史扩展用 HMENU items。 |
| Linux | 占位异常契约可实现；可选 GMenu/GAction 菜单项并非全部旧语义。 |
| macOS | 占位异常契约可实现；可选 NSMenuItem 但部分历史外观不等价。 |

普通属性/事件/行为按平台各自验收；Windows消息/句柄等真实专属契约单列🚫，但不抵扣Windows未实现项。Font/Image/Icon/Bitmap/Graphics公共类型的跨平台身份按统一平台计划先解决，不靠排除分母降低目标。

## 修改历史

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

## 原始资料

- [微软固定API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.menuitem?view=windowsdesktop-10.0)
- [微软WFDEV006：旧菜单/控件仅为二进制兼容恢复](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/wfdev-diagnostics/wfdev006)
- [GTK/GIO原生依据](https://docs.gtk.org/gio/class.MenuItem.html)
- [AppKit原生依据](https://developer.apple.com/documentation/appkit/nsmenuitem)
