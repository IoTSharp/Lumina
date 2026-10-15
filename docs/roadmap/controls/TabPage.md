# TabPage

🟡 **当前状态：**继承 Panel，仅声明 UseVisualStyleBackColor=true；其余依赖基类。默认值是否等同标准与图像/工具提示等未验收。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[TabPage API](../api/TabPage.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：[TabPage.cs](../../../Lumina.Forms/TabPage.cs)
- 前置与排序：Panel、TabControl/TabPageCollection、图像与背景支持。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| ⬜ | P0 / TabPage-01 | 对齐 TabPage 的构造重载、Text、Name、ImageIndex/ImageKey、ToolTipText、UseVisualStyleBackColor 的标准表面与初始默认值。 |
| ⬜ | P0 / TabPage-02 | 保证只能作为合适的页使用；TabPages.Add/Insert/Remove/Clear、Parent/TabIndex、页名索引和 TabControl.Controls 的视图一致；实现静态 GetTabPageOfComponent 查询并校准 null/非控件结果。 |
| ⬜ | P1 / TabPage-03 | Text/ImageIndex/ImageKey/ToolTipText 的运行时变化立即刷新宿主标签；未创建句柄时变更也应保存，重建后可恢复。 |
| ⬜ | P1 / TabPage-04 | 校准 Padding、AutoScroll、BorderStyle、BackColor/BackgroundImage 及 GetPreferredSize；页 Bounds 由 TabControl.DisplayRectangle 控制。 |
| ⬜ | P1 / TabPage-05 | 对子控件 Load/VisibleChanged/Enter/Leave 的触发顺序做基线捕获；切页隐藏不能被计成 Dispose/再次初始化。 |
| ⬜ | P1 / TabPage-06 | 处理页移至另一 TabControl、移除当前页、保留最后活动子控件、禁用标签行为和验证取消；键盘切页后焦点位置有明确验收。 |
| ⬜ | P1 / TabPage-07 | UseVisualStyleBackColor 与显式 BackColor/父背景的优先级按基线核对；记录当前 true 初始化与官方默认的差异，禁止靠外观猜值。 |
| ⬜ | P2 / TabPage-08 | Windows 保留真实页面 HWND，跟随 SysTabControl32 的 TCM_ADJUSTRECT 布局，页面没有独立顶层窗口。 |
| ⬜ | P2 / TabPage-09 | Linux 为 GtkNotebook 每页提供原生 GtkFixed/GtkScrolledWindow；同一个业务 Control 只能挂到一个 native parent，移动页时安全 reparent。 |
| ⬜ | P3 / TabPage-10 | macOS 用 NSTabViewItem.view/NSView，移除 item 与释放 view 分开遵循托管归属；UseVisualStyleBackColor 是系统视觉策略差异项。 |
| ⬜ | P1 / TabPage-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / TabPage-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI 写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / TabPage-V3 | 发布 win-x64 Native AOT，检查实际窗口与本控件交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | Panel HWND 作为页内容；标签状态由 TabControl 同步。 |
| Linux | GtkNotebook page widget + label widget；页面标题/工具提示/图像变化要同时更新 label。 |
| macOS | NSTabViewItem + NSView。TabPage 的 .resx 图片应转换为平台原生图像，System.Drawing 阻塞须先解决。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- `bca1ffd`（2026-04-08）：修正 TabPage 背景与前景色。
- `7149595`（2026-04-08）：深色模式与菜单相关提交涉及本文件。
- `ce86ab5`（2026-03-31）：接入核心主题继承。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms TabPage 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.tabpage?view=windowsdesktop-10.0)
- [GTK4 Notebook](https://docs.gtk.org/gtk4/class.Notebook.html)
- [AppKit nstabviewitem](https://developer.apple.com/documentation/appkit/nstabviewitem)
