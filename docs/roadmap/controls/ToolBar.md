# ToolBar

⬜ **现状：**Lumina 无同名实现。官方 Microsoft.WindowsDesktop.App 10.0.11 无参构造实际抛 `PlatformNotSupportedException`；该旧类型在.NET10恢复是为二进制兼容，不能从“类型导出”推导其UI仍可使用。

- [完整成员清单](../api/ToolBar.md) · [98%口径](../coverage.md) · [官方运行探针](../../assessments/2026-10-15/legacy-control-probes.json)。
- 固定基线：`b83f534` / 10.0.11，评估日期2026-10-15。
- 排序：P2保留标准类型/元数据/异常；P3仅在另有.NETFramework旧应用需求时恢复历史UI。当前实际业务优先 [ToolStrip](./ToolStrip.md)。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P2 / ToolBar-01 | 实现标准继承、构造、公开/受保护成员、嵌套类型与过时元数据，逐行对照API；不得以现代ToolStrip类抵扣同名签名责任。 |
| ⬜ | P2 / ToolBar-02 | 依据真实官方探针对齐无参构造PNSE；其余可达重载/成员分别验证，未测保持待验，不能把整类所有成员都自动标已测或抛同一异常。 |
| ⬜ | P2 / ToolBar-03 | 列全ToolBarButton/ToolBarButtonCollection、Buttons/ButtonSize/ImageList、Appearance/BorderStyle/Divider/DropDownArrows/ShowToolTips/TextAlign/Wrappable/AutoSize、ButtonClick/ButtonDropDown的元数据/签名。官方不可实例化后无法正常执行的成员注明原因，不以“能编译”声称原UI功能完整。 |
| ⬜ | P2 / ToolBar-04 | Native AOT静态消费验证准确类型和标准异常；现代.NET10设计器同样不能正常实例化旧类，不用静态Demo构造失败证明兼容，也不把设计器不可用归咎Lumina。 |
| ⬜ | P3 / ToolBar-05 | 若额外恢复.NETFramework UI，先固定独立历史基线，再验证按钮增删/重排、Push/PartialPush、下拉、图像、自动换行、快捷键/工具提示与DPI；这属于扩展，不计入固定.NET10必须恢复的功能。 |
| ⬜ | P3 / ToolBar-06 | Windows历史扩展可用ToolbarWindow32 + TB_* 消息，全部状态/集合/事件双向同步、资源/句柄/图像释放明确；禁止只放一个相似原生控件就标≥98%。 |
| ⬜ | P3 / ToolBar-07 | Linux历史扩展可用GtkBox/GtkButton/GtkToggleButton/GtkMenuButton原生组合；GTK4无旧GtkToolbar，集合/布局/键盘/事件仍需逐项对齐。固定占位/异常契约不需要创建真实widget。 |
| ⬜ | P3 / ToolBar-08 | macOS历史扩展可用NSStackView/NSButton/NSMenu原生组合；NSToolbar为窗口级工具栏，不能覆盖任意Dock/Wrap。固定占位契约无需创建原生UI。 |

**验收分开记录：**标准签名/官方异常一致、真实历史UI恢复是两种结果。前者不得宣传为后者；全量成员仍留在责任清单，官方不可用与Lumina未实现分别说明。

## 三平台实现边界

| 平台 | 固定.NET10责任 | 可选历史UI |
| --- | --- | --- |
| Windows | 过时类型、标准元数据和实际异常；保持未实现/待验状态直到验证 | ToolbarWindow32 + TB_* 消息 |
| Linux | 同一兼容占位契约；不冒称原控件UI已跨平台 | GtkBox/GtkButton/GtkToggleButton/GtkMenuButton原生组合，逐项平台差异 |
| macOS | 同一兼容占位契约；不冒称原控件UI已跨平台 | NSStackView/NSButton/NSMenu原生组合，逐项平台差异 |

## 修改历史

- 无此同名类型的实现Git历史。
- 2026-10-15：初次细化历史控件任务。
- 2026-10-15：遗漏审计结合官方10.0.11运行证据，纠正“导出类型等于可用旧UI”的假设；将历史UI恢复列为独立可选扩展。本轮未实现运行库功能。

## 原始资料

- [微软固定API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.toolbar?view=windowsdesktop-10.0)
- [WFDEV006：二进制兼容占位](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/wfdev-diagnostics/wfdev006)
- [GTK4原生容器](https://docs.gtk.org/gtk4/class.Box.html)
- [AppKit原生依据](https://developer.apple.com/documentation/appkit/nstoolbar)
