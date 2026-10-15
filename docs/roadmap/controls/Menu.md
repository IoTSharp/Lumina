# Menu

⬜ **现状：**无同名类型。固定 .NET10 中 Menu 为抽象、过时的二进制兼容类型；不能把 .NET Framework 的旧菜单完整 UI 自动当作 .NET10 必须恢复的行为。

- 基线：固定 .NET10 WinForms 10.0.11 / `b83f534`；评估日期 2026-10-15。
- 类型：`System.Windows.Forms.Menu`。 [全部成员、继承及接口任务](../api/Menu.md) · [98%口径](../coverage.md) · [公共前置](../foundations.md) · [平台条件](../platforms.md)。
- 排序：P2：类型/元数据与基线异常；P3：仅在额外旧应用迁移需求明确后恢复历史 UI。
- 证据：[官方10.0.11旧类型运行探针](../../assessments/2026-10-15/legacy-control-probes.json)；未单独探测的构造/成员不自动视为通过。

## 顺序任务

| 状态 | 优先级 / ID | 细节与验收 |
| --- | --- | --- |
| ⬜ | P2 / Menu-01 | 实现标准 abstract Menu : Component、protected Menu(MenuItem[])、Name/Tag/Handle/IsParent/MenuItems/MdiListItem、FindHandle/FindShortcut 常量与固定 obsolete 元数据，不能用 NativeMenu helper 充当同名类型。 |
| ⬜ | P2 / Menu-02 | 保留 FindMenuItem/GetMainMenu/GetContextMenu/MergeMenu、CreateMenuHandle/ProcessCmdKey/CloneMenu/FindMergePosition 等签名；成员行为先与固定10.0.11运行端逐项比较。 |
| ⬜ | P2 / Menu-03 | 现有证据中 Menu 为 abstract，未直接构造；补最小派生调用 protected ctor 的探针以确认异常边界。不得从 MainMenu 的 PNSE 推断每个未执行成员均已验证。 |
| ⬜ | P2 / Menu-04 | 将“公开类型存在/标准异常一致”和“真实可用菜单功能”分别记录；官方不可达成员明确标原因，不能靠批量抛异常宣称完成旧 UI。 |
| ⬜ | P2 / Menu-05 | 继承/Component 释放/Obsolete/构造的静态消费与 Native AOT 编译验证；不在 Demo 中放会构造失败的设计器静态实例。 |
| ⬜ | P3 / Menu-06 | 若另行恢复 .NETFramework 旧行为：Windows 复用 HMENU/native menu helper，集合、合并、快捷键、绘制事件与资源归属作为独立扩展计划，不混入固定.NET10验收。 |
| ⬜ | P3 / Menu-07 | Linux 可选历史 UI 采用 GtkPopoverMenu/GMenuModel；HMENU/Win32消息无等价，跨平台基线异常兼容不需要 native 菜单替身。 |
| ⬜ | P3 / Menu-08 | macOS 可选历史 UI 采用 NSMenu；应用级菜单与 Windows窗体菜单的差异披露，不将原生近似方案当固定.NET10已支持。 |
| ⬜ | 验收 / Menu-V | 对完整 API 表逐成员核对签名、默认、修改限制、异常、状态和事件轨迹；真实 WinForms 与 Lumina 使用同场景。AOT发布后实际运行、资源/回调释放、键盘/可访问性分别留证，Linux/macOS不借Windows结果抵扣。固定基线的兼容占位/标准异常与可选历史UI分别报告，不把异常一致称作原控件已可用。 |

## 原生方案与边界

| 平台 | 实现 |
| --- | --- |
| Windows | 固定.NET10先对齐过时类型和实际异常；恢复 HMENU UI 是独立可选扩展。 |
| Linux | 固定.NET10占位契约不需生成 native UI；历史功能可选 GtkPopoverMenu。 |
| macOS | 固定.NET10占位契约不需生成 native UI；历史功能可选 NSMenu。 |

普通属性/事件/行为按平台各自验收；Windows消息/句柄等真实专属契约单列🚫，但不抵扣Windows未实现项。Font/Image/Icon/Bitmap/Graphics公共类型的跨平台身份按统一平台计划先解决，不靠排除分母降低目标。

## 修改历史

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

## 原始资料

- [微软固定API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.menu?view=windowsdesktop-10.0)
- [微软WFDEV006：旧菜单/控件仅为二进制兼容恢复](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/wfdev-diagnostics/wfdev006)
- [GTK/GIO原生依据](https://docs.gtk.org/gtk4/class.PopoverMenu.html)
- [AppKit原生依据](https://developer.apple.com/documentation/appkit/nsmenu)
