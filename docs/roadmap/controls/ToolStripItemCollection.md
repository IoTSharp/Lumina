# ToolStripItemCollection

基线：2026-10-15 · .NET 10.0.11。目标继承：`ToolStripItemCollection : Layout.ArrangedElementCollection`。本页为逐类型实施与验收任务，没有把规划当作已完成支持。

## 当前证据与依赖

🟡 [ToolStripItemCollection.cs](../../../Lumina.Forms/ToolStripItemCollection.cs) 当前sealed IEnumerable<ToolStripItem>，Add返回void（基线Add(ToolStripItem)返回int），只有Add/AddRange/RemoveAt/IndexOf和整数索引。RemoveAt有Changed解绑，但[ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs) EnsureItemHosts只迭代当前Items，旧宿主清理需专项验证。

源码定位：[Lumina.Forms/ToolStripItemCollection.cs](../../../Lumina.Forms/ToolStripItemCollection.cs)；条目显示/更新必须同时检查 [ToolStrip.cs](../../../Lumina.Forms/ToolStrip.cs)、[StatusStrip.cs](../../../Lumina.Forms/StatusStrip.cs)、[NativeMenu.cs](../../../Lumina.Forms/NativeMenu.cs)、[ToolStripPopupMenu.cs](../../../Lumina.Forms/ToolStripPopupMenu.cs)，不能只凭派生类型文件的长短判断完整度。先决依赖：ToolStripItem.Owner/Parent、ToolStrip和DropDown宿主布局/释放；不需要引入通用集合框架。

完整属性、事件、方法/重载（含继承）见 [ToolStripItemCollection API 清单](../api/ToolStripItemCollection.md)，共同契约见 [基础任务](../foundations.md)。条目是 WinForms 已有公开概念，原生控件组合留在库内部；本页不要求新增公开UI概念、平台项目或新依赖。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 是继承/阻塞，P1 是主要契约，P2 是边界/高级能力，P3 是平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| ToolStripItemCollection-001 | P0 | 🟡 | 对齐ToolStripItemCollection基类/接口/构造器可见性；Add(ToolStripItem)返回int，整数/字符串索引器、IList/ICollection兼容面逐签名验收，不能仅保留泛型枚举。 |
| ToolStripItemCollection-002 | P1 | ⬜ | Add(string)、Add(Image)、Add(string,Image)、Add(string,Image,EventHandler)的返回条目类型、特殊分隔文本、null和事件订阅；标准返回对象可直接继续设属性。 |
| ToolStripItemCollection-003 | P1 | 🟡 | AddRange数组/另一集合重载与Insert；重复同实例、已属于另一个Owner、null元素、自身集合与枚举期间修改的行为按基线。 |
| ToolStripItemCollection-004 | P1 | ⬜ | Remove/RemoveAt/RemoveByKey/Clear：更新Owner/Parent、解除Changed订阅、删除旧原生宿主；Remove是否Dispose与宿主Dispose是否销毁条目分别比较。 |
| ToolStripItemCollection-005 | P1 | ⬜ | Contains/ContainsKey/IndexOf/IndexOfKey、Find(key,searchAllChildren)、字符串索引；大小写、重名、空key、嵌套DropDown和无匹配结果。 |
| ToolStripItemCollection-006 | P1 | ⬜ | 集合变化的ItemAdded/ItemRemoved等宿主事件、OwnerChanged、Layout顺序；一次AddRange是否多次重排按基线，不以性能优化改变可观察事件。 |
| ToolStripItemCollection-007 | P2 | ⬜ | CopyTo/枚举器/IsReadOnly/IList显式成员的类型检查、索引边界、只读异常和返回值；集合没有自创Changed公开事件。 |
| ToolStripItemCollection-008 | P2 | ⬜ | 所有者Dispose、条目独立Dispose、从toolbar迁移到dropdown/overflow后宿主唯一，不出现重复按钮、旧可点击区域或幽灵菜单命令。 |
| ToolStripItemCollection-009 | P2 | ⬜ | Designer Items.AddRange/DropDownItems.AddRange静态代码可直接编译；集合构造与派生按标准API，不把内部Action回调暴露成替代API。 |
| ToolStripItemCollection-010 | P3 | ⬜ | AOT批量初始化、动态增删、嵌套查找和三平台原生对象计数；布局完成后逻辑集合/原生子对象/命令表一致性有证据。 |
| ToolStripItemCollection-CLOSE | P3 | ⬜ | 按完整API清单逐个关闭剩余成员；每个访问器/重载、默认值、异常、通知次数/顺序、取消/重入、未挂载/overflow/移除/Dispose分别验证。Windows公开属性、公开事件、行为各≥98%；Linux/macOS可移植三项各≥98%，不可移植明细保留且需证据，未验收不计通过。 |

## 三平台的原生实现评估

**Windows**：集合本身是托管数据结构；变更同步现有原生控件宿主、HMENU命令映射和布局。单条或批量变更完成后同时修复Owner/Parent、事件和资源，不另造公开transaction API。

**Linux / GTK4**：同一托管集合与GtkBox/GMenuModel中的原生子项同步，移除时disconnect信号并解除父子引用；不因GTK对象refcount而改变IDisposable/集合所有权语义。

**macOS / AppKit**：同一托管集合与NSToolbarItem/NSView/NSMenuItem同步；逻辑Owner迁移与原生view重挂靠分开处理，移除不直接等于Dispose，按WinForms确认所有权。

实施顺序是 Windows 标准API/状态/事件→该条目最小原生试验→平台映射与完整验收。三平台分别记录属性/事件/行为的分子分母；继承API不能漏记，签名命中、自动属性、控件可以显示或本页任务已写完都不算通过。原生系统绘制、原生容器承载库绘制、原生控件组合应准确区分；公共业务行为难映射时仍然是待实现需求，不自动从可移植分母剔除。

各平台最少覆盖：代码与用户动作双向同步，Owner/Parent/overflow关系，事件顺序/取消/重入，键盘/助记键/输入法（适用时），DPI/RTL/本地化，无障碍，静态Designer和资源，AOT运行，移除与Dispose。没有完整证据时保持🟡/⬜，不发布已达标百分比。

## 修改历史

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 2026-04-09 · `9d5165f` · feat: 添加多个控件和功能，增强用户界面和交互体验。
- 2026-03-31 · `72fe8f6` · Refactor ToolStrip and Menu handling。
- 2026-03-31 · `52f5662` · Add core UI components for Lumina.Forms。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

## 官方依据

- [WinForms ToolStripItemCollection API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripitemcollection?view=windowsdesktop-10.0)，精确签名以本机10.0.11引用程序集生成的清单为准。
- [ToolStrip 控件概述](https://learn.microsoft.com/dotnet/desktop/winforms/controls/toolstrip-control-overview)。
- [Windows 原生参考](https://learn.microsoft.com/windows/win32/menurc/menus)。
- [GTK4 Box](https://docs.gtk.org/gtk4/class.Box.html)。
- [AppKit nsmenuitem](https://developer.apple.com/documentation/appkit/nsmenuitem)。
