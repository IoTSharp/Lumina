# MenuItemCollection

⬜ **现状：**无标准嵌套集合实现。真实类型是 Menu.MenuItemCollection，不能新增顶级 Lumina.Forms.MenuItemCollection。父菜单在.NET10为兼容占位，集合本身行为尚未独立实测。

- 基线：固定 .NET10 WinForms 10.0.11 / `b83f534`；评估日期 2026-10-15。
- 类型：`System.Windows.Forms.Menu+MenuItemCollection`。 [全部成员、继承及接口任务](../api/MenuItemCollection.md) · [98%口径](../coverage.md) · [公共前置](../foundations.md) · [平台条件](../platforms.md)。
- 排序：P2：嵌套类型/完整接口与可达性；P3：只有恢复历史菜单时才扩展运行时集合同步。
- 证据：[官方10.0.11旧类型运行探针](../../assessments/2026-10-15/legacy-control-probes.json)；未单独探测的构造/成员不自动视为通过。

## 顺序任务

| 状态 | 优先级 / ID | 细节与验收 |
| --- | --- | --- |
| ⬜ | P2 / MenuItemCollection-01 | 在标准 Menu 内实现 MenuItemCollection(Menu owner)、IList/ICollection/IEnumerable、Count/IsReadOnly、整数/字符串索引器与全部公开/显式接口；不改嵌套身份。 |
| ⬜ | P2 / MenuItemCollection-02 | 补全部 Add 重载（含index、caption/onClick/children）、AddRange、Remove/RemoveAt/RemoveByKey/Clear、Contains/ContainsKey/Find、IndexOf/IndexOfKey/CopyTo。 |
| ⬜ | P2 / MenuItemCollection-03 | 先探测官方集合在 null owner/可取得 owner 下的构造与可达成员；不能从 MenuItem构造PNSE 推断每个集合成员均抛相同异常，未验收保持🔎。 |
| ⬜ | P2 / MenuItemCollection-04 | 枚举修改、重复/跨parent项、Name大小写/空key、索引越界、IList错误类型的基线可达时逐项记录；不可达则公开说明原因，不虚计功能支持。 |
| ⬜ | P3 / MenuItemCollection-05 | 若恢复历史UI：Add/Remove/索引重排同步 Parent/Index/native ID，Clone/Merge 不共享错误归属，Remove 不误 Dispose/重复退订。 |
| ⬜ | P3 / MenuItemCollection-06 | Windows 历史扩展将集合变化提交 HMENU/DrawMenuBar 或popup重建，映射稳定ID；集合不创建单独 HWND。 |
| ⬜ | P3 / MenuItemCollection-07 | Linux 可选把标准托管嵌套集合映射 GMenu/GAction，native模型不替代IList契约；固定异常占位无需native模型。 |
| ⬜ | P3 / MenuItemCollection-08 | macOS 可选映射 NSMenu.itemArray，保持托管 Parent/Index/Name规则而非直接暴露NSArray；固定基线不要求恢复旧菜单。 |
| ⬜ | 验收 / MenuItemCollection-V | 对完整 API 表逐成员核对签名、默认、修改限制、异常、状态和事件轨迹；真实 WinForms 与 Lumina 使用同场景。AOT发布后实际运行、资源/回调释放、键盘/可访问性分别留证，Linux/macOS不借Windows结果抵扣。固定基线的兼容占位/标准异常与可选历史UI分别报告，不把异常一致称作原控件已可用。 |

## 原生方案与边界

| 平台 | 实现 |
| --- | --- |
| Windows | 嵌套集合标准契约；历史 UI 扩展才需要 HMENU 同步。 |
| Linux | 固定契约先验实际可达性；可选 GMenu 模型桥。 |
| macOS | 固定契约先验实际可达性；可选 NSMenu itemArray 桥。 |

普通属性/事件/行为按平台各自验收；Windows消息/句柄等真实专属契约单列🚫，但不抵扣Windows未实现项。Font/Image/Icon/Bitmap/Graphics公共类型的跨平台身份按统一平台计划先解决，不靠排除分母降低目标。

## 修改历史

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

## 原始资料

- [微软固定API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.menu.menuitemcollection?view=windowsdesktop-10.0)
- [微软WFDEV006：旧菜单/控件仅为二进制兼容恢复](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/wfdev-diagnostics/wfdev006)
- [GTK/GIO原生依据](https://docs.gtk.org/gio/class.Menu.html)
- [AppKit原生依据](https://developer.apple.com/documentation/appkit/nsmenu)
