# FontDialog

🟡 **当前状态：**已有 ChooseFontW/LOGFONTW、字体转换和主题 hook，仅 Font/Color/ShowColor；当前 sealed IDisposable，标准 CommonDialog 继承、选项/Apply 事件和字体跨平台类型为主要缺口。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[FontDialog API](../api/FontDialog.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：[FontDialog.cs](../../../Lumina.Forms/FontDialog.cs)
- 前置与排序：CommonDialog、System.Drawing.Font 的跨平台策略、native font descriptor 转换。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| ⬜ | P0 / FontDialog-01 | 建立 CommonDialog 继承、ShowDialog(IWin32Window)、Reset、HelpRequest/Apply 等标准成员，修正 ShowColor 等默认和所有构造签名。 |
| ⬜ | P0 / FontDialog-02 | 补 AllowScriptChange/AllowSimulations/AllowVectorFonts/AllowVerticalFonts、FixedPitchOnly、FontMustExist、ScriptsOnly、ShowApply/ShowEffects/ShowHelp、MinSize/MaxSize 与固定 API 清单剩余项。 |
| ⬜ | P1 / FontDialog-03 | Font 与 Color 输入/返回、粗斜体/下划线/删除线、大小单位/字符集和样式组合按官方对照；并不存在的字体与非法字号按标准处理。 |
| ⬜ | P1 / FontDialog-04 | ShowApply=true 触发 Apply 时不关闭、最终取消与已 Apply 状态边界清楚；HelpRequest、确认/取消、重复显示和 Reset 事件/状态顺序一致。 |
| ⬜ | P1 / FontDialog-05 | MinSize/MaxSize、FixedPitchOnly、字体不存在/缺字形、可变字体和系统字体替换需要 native 探针；不要只验证 Segoe UI 常规字体。 |
| ⬜ | P1 / FontDialog-06 | 字体对象借用/返回归属与 GDI HFONT/LOGFONT 生命周期分清；取消不释放调用者传入 Font，重复显示不漏 native 字体。 |
| ⬜ | P1 / FontDialog-07 | 设计器 components/.resx Font 与本地化能静态还原；公开 System.Drawing.Font 在现代 Linux/macOS 不受支持是 P0 阻塞，不能仅加入 AppKit/GTK 引用就标可跨平台。 |
| ⬜ | P1 / FontDialog-08 | Windows 继续 ChooseFontW/CHOOSEFONTW/CF_*，hook 处理 Apply/Help，AOT 下稳定回调和安全 LOGFONT 编码/DPI 点值转换。 |
| ⬜ | P2 / FontDialog-09 | Linux 用 GtkFontDialog + PangoFontDescription；font filters、Apply 按钮、ShowEffects 颜色/删除线不全等价，明确原生组成和需单列选项。 |
| ⬜ | P3 / FontDialog-10 | macOS 用 NSFontPanel/NSFontManager 与 NSFontDescriptor；系统共享非模态字体面板/颜色与 WinForms 独占模态对话框不同，原型验证会话隔离和 Apply/Cancel。 |
| ⬜ | P1 / FontDialog-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / FontDialog-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI/非视觉组件写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / FontDialog-V3 | 发布 win-x64 Native AOT，检查实际窗口/原生组件与本类型交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | ChooseFontW 原生实现可继续扩展，最先修 CommonDialog、标准选项和 Apply 事件。 |
| Linux | GtkFontDialog/PangoFontDescription；原生字体选择并不解决公开 System.Drawing.Font 的支持问题，需先做兼容绘图类型决策。 |
| macOS | NSFontPanel/NSFontManager；字体变更通过 responder action 返回，必须控制活动会话和转换，不把 native handle 暴露给消费者。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- `f666aa6`（2026-04-08）：UI/发布改进涉及字体对话框。
- `791793f`（2026-04-08）：接入主题调色板。
- `57ef06f`（2026-04-08）：添加字体对话框主题 hook。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms FontDialog 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.fontdialog?view=windowsdesktop-10.0)
- [GTK4 FontDialog](https://docs.gtk.org/gtk4/class.FontDialog.html)
- [AppKit nsfontpanel](https://developer.apple.com/documentation/appkit/nsfontpanel)
