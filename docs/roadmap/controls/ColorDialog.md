# ColorDialog

🟡 **当前状态：**已有 ChooseColorW、主题 hook 和 16 项 CustomColors；当前 sealed IDisposable，AnyColor=true/FullOpen=true 与标准默认待校正，CustomColors 只读返回也需对齐标准可设值表面。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[ColorDialog API](../api/ColorDialog.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：[ColorDialog.cs](../../../Lumina.Forms/ColorDialog.cs)
- 前置与排序：CommonDialog/Component、Color 值/interop、shared modal loop；跨平台无需先引入新颜色 API。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| ⬜ | P0 / ColorDialog-01 | 建立标准 CommonDialog 继承、ShowDialog(IWin32Window)、Reset/HelpRequest，公开构造/Dispose/自定义派生契约按固定基线。 |
| ⬜ | P0 / ColorDialog-02 | 补 AllowFullOpen、AnyColor、Color、CustomColors、FullOpen、ShowHelp、SolidColorOnly，核对默认和 CustomColors 数组复制/长度/颜色编码规则。 |
| ⬜ | P1 / ColorDialog-03 | 实现取消保持已提交 Color/CustomColors 的标准边界、重复 ShowDialog、Reset 后默认还原；禁止不同实例共享可写调色板。 |
| ⬜ | P1 / ColorDialog-04 | 验证 RGB/BGR 编码、预定义颜色、空/透明 Color 传入和 native 返回的 alpha 行为；不要扩展为自创 RGBA 公共模型。 |
| ⬜ | P1 / ColorDialog-05 | AllowFullOpen=false/FullOpen=true 组合、帮助按钮与 hook、owner 模态/焦点、Esc/Enter 和键盘颜色选择按官方对照。 |
| ⬜ | P1 / ColorDialog-06 | 主题 hook 只改必要外观，保留系统 accessibility/high contrast 与帮助事件；创建失败/取消/异常回调分清结果。 |
| ⬜ | P1 / ColorDialog-07 | 组件可放 components，.resx 的 Color/CustomColors 和本地化属性无需反射动态生成；非拥有画刷/窗口不误释放。 |
| ⬜ | P1 / ColorDialog-08 | Windows 继续 CHOOSECOLORW/ChooseColorW/CC_*，修正默认和 CustomColors setter；AOT 下 hook 委托静态 root，内存 finally 释放。 |
| ⬜ | P2 / ColorDialog-09 | Linux 使用 GTK4.10+ GtkColorDialog.choose_rgba，禁用或转换 alpha 到标准 RGB；GTK 不暴露 WinForms 16 格 CustomColors/FullOpen 全部配置，单列不可等价项。 |
| ⬜ | P3 / ColorDialog-10 | macOS 使用 NSColorPanel/native color well 交互包装成标准模态返回；NSColorPanel 为共享模型面板，需隔离每次会话和取消回滚，不可直接当同步独占对话框。 |
| ⬜ | P1 / ColorDialog-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / ColorDialog-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI/非视觉组件写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / ColorDialog-V3 | 发布 win-x64 Native AOT，检查实际窗口/原生组件与本类型交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | 已有 ChooseColorW，最短路径是修 API/默认、可靠资源和事件。 |
| Linux | GtkColorDialog 原生颜色选择；16 格自定义色板精确 UI 不可直接原生等价，不用自绘假称满足。Color 值回传和取消仍必须覆盖。 |
| macOS | NSColorPanel 系统颜色面板；共享/非模态模型与 WinForms ShowDialog 需受控适配，CustomColors/FullOpen 是明确差异。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- `f666aa6`（2026-04-08）：UI/发布改进涉及颜色对话框。
- `791793f`（2026-04-08）：接入主题调色板支持。
- `57ef06f`（2026-04-08）：添加对话框主题应用逻辑。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms ColorDialog 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.colordialog?view=windowsdesktop-10.0)
- [GTK4 ColorDialog](https://docs.gtk.org/gtk4/class.ColorDialog.html)
- [AppKit nscolorpanel](https://developer.apple.com/documentation/appkit/nscolorpanel)
