# MainMenu

⬜ **现状：**无同名类型。官方 Microsoft.WindowsDesktop.App 10.0.11 无参构造实际抛 PlatformNotSupportedException；类型存在是二进制兼容恢复，不是可用的旧主菜单。

- 基线：固定 .NET10 WinForms 10.0.11 / `b83f534`；评估日期 2026-10-15。
- 类型：`System.Windows.Forms.MainMenu`。 [全部成员、继承及接口任务](../api/MainMenu.md) · [98%口径](../coverage.md) · [公共前置](../foundations.md) · [平台条件](../platforms.md)。
- 排序：P2：标准类型/构造异常；P3：可选.NETFramework功能扩展。
- 证据：[官方10.0.11旧类型运行探针](../../assessments/2026-10-15/legacy-control-probes.json)；未单独探测的构造/成员不自动视为通过。

## 顺序任务

| 状态 | 优先级 / ID | 细节与验收 |
| --- | --- | --- |
| ⬜ | P2 / MainMenu-01 | 实现 MainMenu : Menu 的三个构造（无参、IContainer、MenuItem[]）、CloneMenu/GetForm/RightToLeft/Collapse 与继承公开表面及过时元数据。 |
| ⬜ | P2 / MainMenu-02 | 复用已持久化无参构造 PNSE 证据，补其他构造和派生扩展的准确异常时点；不能宣称所有方法都运行通过。 |
| ⬜ | P2 / MainMenu-03 | 固定.NET10兼容测试同时断言元数据/签名和官方异常；将不可使用 UI 的状态明确显示，98% 报告不可把类型存在写成原菜单可用。 |
| ⬜ | P2 / MainMenu-04 | 核对与 Form.Menu/MDI合并的旧签名及标准废弃处理，保持现代 MainMenuStrip/MenuStrip 为实际业务菜单目标。 |
| ⬜ | P2 / MainMenu-05 | 组件/IContainer/CloneMenu 的可达性与资源处理依基线确认；不要求真实 WinForms 设计器实例化它来证明支持。 |
| ⬜ | P3 / MainMenu-06 | 若追加恢复旧主菜单：Windows SetMenu/DrawMenuBar/HMENU、菜单合并、MDI窗单、Collapse/Select/快捷键完整对齐，单列扩展分母。 |
| ⬜ | P3 / MainMenu-07 | Linux 可选 GtkPopoverMenuBar；Windows窗体级多菜单/MDI合并无现成对应，旧语义逐项实现不直接复用 ToolStripItem 替换类型。 |
| ⬜ | P3 / MainMenu-08 | macOS 可选 NSMenu 主菜单按活动窗口路由；嵌入/窗体级菜单与应用级菜单不能一对一，不以全局菜单可显示就声称历史行为完成。 |
| ⬜ | 验收 / MainMenu-V | 对完整 API 表逐成员核对签名、默认、修改限制、异常、状态和事件轨迹；真实 WinForms 与 Lumina 使用同场景。AOT发布后实际运行、资源/回调释放、键盘/可访问性分别留证，Linux/macOS不借Windows结果抵扣。固定基线的兼容占位/标准异常与可选历史UI分别报告，不把异常一致称作原控件已可用。 |

## 原生方案与边界

| 平台 | 实现 |
| --- | --- |
| Windows | 官方固定基线 PNSE；若需要历史 UI 再接 HMENU。 |
| Linux | 占位/异常基线无需UI；可选原生 GtkPopoverMenuBar。 |
| macOS | 占位/异常基线无需UI；可选 NSMenu，窗口/应用边界明确。 |

普通属性/事件/行为按平台各自验收；Windows消息/句柄等真实专属契约单列🚫，但不抵扣Windows未实现项。Font/Image/Icon/Bitmap/Graphics公共类型的跨平台身份按统一平台计划先解决，不靠排除分母降低目标。

## 修改历史

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

## 原始资料

- [微软固定API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.mainmenu?view=windowsdesktop-10.0)
- [微软WFDEV006：旧菜单/控件仅为二进制兼容恢复](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/wfdev-diagnostics/wfdev006)
- [GTK/GIO原生依据](https://docs.gtk.org/gtk4/class.PopoverMenuBar.html)
- [AppKit原生依据](https://developer.apple.com/documentation/appkit/nsmenu)
