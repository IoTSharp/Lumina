# SaveFileDialog

🟡 **当前状态：**已有 GetSaveFileNameW、OverwritePrompt 与 CheckPathExists；类为 sealed IDisposable，标准 FileDialog 继承/事件和 FilterIndex 返回不足，Filter/Title 使用 Demo 风格默认。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[SaveFileDialog API](../api/SaveFileDialog.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：[SaveFileDialog.cs](../../../Lumina.Forms/SaveFileDialog.cs)
- 前置与排序：CommonDialog/FileDialog、AOT-safe 对话框封装，与 OpenFileDialog 共用文件属性逻辑。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| ⬜ | P0 / SaveFileDialog-01 | 建立标准 FileDialog 继承和 ShowDialog(IWin32Window)、Reset、FileOk/HelpRequest；遵循 Component 生命周期，不增加消费者侧 native 清理 API。 |
| ⬜ | P0 / SaveFileDialog-02 | 对齐 FileName/FileNames、Filter/FilterIndex、InitialDirectory、DefaultExt、Title、AddExtension 与 SupportMultiDottedExtensions；移除库层硬编码文本文件过滤默认。 |
| ⬜ | P1 / SaveFileDialog-03 | 补 CreatePrompt、OverwritePrompt、CheckFileExists、CheckPathExists、ValidateNames、RestoreDirectory、DereferenceLinks、AutoUpgradeEnabled/CustomPlaces；默认和保存原有文件提示顺序按基线。 |
| ⬜ | P1 / SaveFileDialog-04 | 实现 FileOk.Cancel 保持窗口、再次更名/选择；覆盖确认拒绝、无效目录、只读目标/共享冲突等 native 结果不得误报 DialogResult.OK。 |
| ⬜ | P1 / SaveFileDialog-05 | 扩展名为空/已有/多点/大小写/末尾点、选不同 filter 后默认扩展更新等用例精确对照，不能简单无条件拼 DefaultExt。 |
| ⬜ | P1 / SaveFileDialog-06 | 重复显示、取消、Reset、FilterIndex 变化、Unicode/长路径/UNC/符号链接，以及文件路径确认后业务写入归属分别验收；对话框本身不替业务保存内容。 |
| ⬜ | P1 / SaveFileDialog-07 | 非视觉设计器 components 容器与 .resx Title/Filter 本地化保持标准序列化；native 键盘/可访问性、owner 模态和焦点恢复验收。 |
| ⬜ | P1 / SaveFileDialog-08 | Windows 默认 IFileSaveDialog + IFileDialogEvents/FileOk，旧式路径用 GetSaveFileNameW；COM interop 用生成或显式 AOT-safe 方式，事件回调对象生命周期有边界。 |
| ⬜ | P2 / SaveFileDialog-09 | Linux 用 GtkFileDialog.save；GTK/portal 的覆盖确认与 WinForms CreatePrompt/OverwritePrompt 可控范围先实测，不能在用户确认前提前关闭后再假装 FileOk 可取消。 |
| ⬜ | P3 / SaveFileDialog-10 | macOS 用 NSSavePanel 的 nameFieldStringValue/directoryURL/allowedContentTypes 与 delegate；扩展名显示/补齐和覆盖提示由平台参与，需以契约而非截图验收。 |
| ⬜ | P1 / SaveFileDialog-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / SaveFileDialog-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI/非视觉组件写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / SaveFileDialog-V3 | 发布 win-x64 Native AOT，检查实际窗口/原生组件与本类型交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | 已有 legacy GetSaveFileNameW；现代 IFileSaveDialog 与现有缓冲区路径可共享标准逻辑。 |
| Linux | GtkFileDialog.save，异步原生 picker 通过共享消息循环接成标准 ShowDialog；Windows Shell 专属项列例外。 |
| macOS | NSSavePanel；CheckPathExists/ValidateNames 与平台文件系统语义可能不同，普通保存结果与取消链仍需对齐。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- `9d5165f`（2026-04-09）：扩展保存对话框兼容属性。
- `cb090bc`（2026-04-08）：加入记事本所需原生保存对话框。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms SaveFileDialog 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.savefiledialog?view=windowsdesktop-10.0)
- [GTK4 FileDialog](https://docs.gtk.org/gtk4/class.FileDialog.html)
- [AppKit nssavepanel](https://developer.apple.com/documentation/appkit/nssavepanel)
