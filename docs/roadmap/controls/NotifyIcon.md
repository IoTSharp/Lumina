# NotifyIcon

🟡 **当前状态：**已有 Shell_NotifyIconW、隐藏消息窗口、Icon/Text/Visible、点击/双击和 ContextMenuStrip；缺 BalloonTip 系列、MouseDown/Move/Up、shell 重启恢复，且当前额外 IconLocation/IconHandle 不应继续扩大为替代标准 API。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[NotifyIcon API](../api/NotifyIcon.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：[NotifyIcon.cs](../../../Lumina.Forms/NotifyIcon.cs)
- 前置与排序：Component、ContextMenuStrip、Icon 跨平台与 UI 消息循环；属于托盘能力，Linux 桌面环境另设可用性边界。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| ⬜ | P0 / NotifyIcon-01 | 对齐标准构造/IContainer、Icon、Text、Visible、ContextMenuStrip、Tag 及 Component 生命周期，Text 长度/空值/Visible 默认与异常语义以基线为准。 |
| ⬜ | P1 / NotifyIcon-02 | 实现 BalloonTipText/Title/Icon、ShowBalloonTip 重载以及 BalloonTipShown/BalloonTipClosed/BalloonTipClicked；timeout 与 shell 实际策略分开记录。 |
| ⬜ | P1 / NotifyIcon-03 | 补 MouseDown/MouseMove/MouseUp，校准 Click/MouseClick/DoubleClick/MouseDoubleClick 与上下键/鼠标的真实事件顺序；当前只处理 button-up/double-click 不足。 |
| ⬜ | P1 / NotifyIcon-04 | 实现 NIM_SETVERSION/NOTIFYICON_VERSION_4 或兼容消息解析、键盘激活、上下文菜单、焦点归还与 shell 任务栏重启 TaskbarCreated 重新注册。 |
| ⬜ | P1 / NotifyIcon-05 | Icon/Text/Visible/ContextMenuStrip 运行时变化、无窗体消息循环、多个图标、取消菜单/退出应用路径分别验收；Shell_NotifyIconW 失败不能仍记录为已添加。 |
| ⬜ | P1 / NotifyIcon-06 | 区分外部 Icon/HICON 借用和内部 LoadImage 拥有；隐藏/Dispose/重复 Dispose 删除图标、销毁消息窗口与释放图标资源，退出不留幽灵图标。 |
| ⬜ | P1 / NotifyIcon-07 | 设计器组件托盘与 .resx Icon 本地化/静态资源读取可 AOT；可访问性名称、键盘菜单、系统高对比/DPI/亮暗托盘验证。 |
| ⬜ | P1 / NotifyIcon-08 | Windows 继续 Shell_NotifyIconW/NOTIFYICONDATAW 与隐藏 HWND，补 native balloon/恢复/消息路径，不需要另建托盘框架。 |
| ⬜ | P2 / NotifyIcon-09 | Linux GTK4 无 GtkStatusIcon；使用桌面原生 StatusNotifierItem/D-Bus 与 DBusMenu，系统通知走桌面通知接口。桌面无托盘宿主时明确 🚫/不可用，不能显示普通窗口冒充原生托盘。 |
| ⬜ | P3 / NotifyIcon-10 | macOS 用 NSStatusBar.statusItem/NSStatusItem.button 与 NSMenu，系统通知可用 UserNotifications；Windows balloon/点击坐标与 macOS 菜单栏状态区不一一对应。 |
| ⬜ | P1 / NotifyIcon-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / NotifyIcon-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI/非视觉组件写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / NotifyIcon-V3 | 发布 win-x64 Native AOT，检查实际窗口/原生组件与本类型交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | 已有 Shell_NotifyIconW；补错误返回、版本化回调、TaskbarCreated 和 balloon 事件，遵循 shell 原生策略。 |
| Linux | StatusNotifierItem/DBusMenu 是桌面协议而非 GTK4 控件；宿主缺失是平台能力差异，GNOME 等环境需要实际测试，不能承诺所有发行版均存在。 |
| macOS | NSStatusItem/NSMenu 为正确原生对应，与窗口 StatusStrip/StatusBar 严格区分；通知权限/投递策略由系统控制。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- `cb090bc`（2026-04-08）：记事本集成涉及托盘能力。
- `2545995`（2026-04-08）：深色主题和组件改进。
- `108ef2b`（2026-04-08）：接入 Win32 深色主题/菜单能力。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。
- 2026-10-15 · 批次 001：托盘消息窗口创建前通知 Application 永久关闭 DPI 设置入口；不改变托盘图标的注册、更新或释放行为，实际托盘交互验收后置。

## 原始资料

- [WinForms NotifyIcon 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.notifyicon?view=windowsdesktop-10.0)
- [GTK4 Application](https://docs.gtk.org/gtk4/class.Application.html)
- [freedesktop StatusNotifierItem 原生托盘协议](https://www.freedesktop.org/wiki/Specifications/StatusNotifierItem/)
- [AppKit nsstatusitem](https://developer.apple.com/documentation/appkit/nsstatusitem)
