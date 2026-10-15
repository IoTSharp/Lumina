# OpenFileDialog

🟡 **当前状态：**已有 GetOpenFileNameW/OPENFILENAMEW 单文件打开、路径/文件检查；类为 sealed IDisposable 而非 OpenFileDialog : FileDialog，Filter/Title 默认与标准有差异，FilterIndex 固定为 1。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[OpenFileDialog API](../api/OpenFileDialog.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：[OpenFileDialog.cs](../../../Lumina.Forms/OpenFileDialog.cs)
- 前置与排序：CommonDialog/FileDialog/Component、IWin32Window、AOT-safe 原生对话框与资源。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| ⬜ | P0 / OpenFileDialog-01 | 建立标准 CommonDialog→FileDialog→OpenFileDialog 继承，ShowDialog(IWin32Window) 返回 DialogResult，Reset/HelpRequest/FileOk、Component 容器释放与公开签名按固定基线。 |
| ⬜ | P0 / OpenFileDialog-02 | 对齐 FileName/FileNames、SafeFileName/SafeFileNames、Filter/FilterIndex、InitialDirectory、DefaultExt/Title；修正硬编码中文默认与固定 FilterIndex，非法 Filter/索引验证和标准异常明确。 |
| ⬜ | P1 / OpenFileDialog-03 | 补 Multiselect、CheckFileExists/CheckPathExists、ReadOnlyChecked/ShowReadOnly、AddExtension、SupportMultiDottedExtensions、ValidateNames、DereferenceLinks、RestoreDirectory、AutoUpgradeEnabled 和 CustomPlaces。 |
| ⬜ | P1 / OpenFileDialog-04 | 实现 FileOk 可取消：取消后对话框保持打开且选择状态保留；HelpRequest 与键盘 Enter/Esc 的 native 通知正确路由到 UI 线程。 |
| ⬜ | P1 / OpenFileDialog-05 | 解析多选返回文件名/目录，Unicode、长路径、网络路径、符号链接、空选择和文件删除竞争场景按 WinForms 语义验收，不截断为 MAX_PATH。 |
| ⬜ | P1 / OpenFileDialog-06 | 父 owner 禁用/恢复、模态嵌套循环、取消后的 FileName/FilterIndex 保留、重复 ShowDialog 与 Reset 逐项对照，不能只验证成功选一个文件。 |
| ⬜ | P1 / OpenFileDialog-07 | 设计器非视觉组件可放 components 容器，.resx 中 Title/Filter 本地化生效；可访问性与键盘由 native picker 提供并实测。 |
| ⬜ | P1 / OpenFileDialog-08 | Windows 优先 Common Item Dialog IFileOpenDialog/IFileDialogEvents，使用 AOT-safe 生成 COM/封装；保留 AutoUpgradeEnabled=false 对应 legacy GetOpenFileNameW 路径，避免运行时动态 COM 依赖。 |
| ⬜ | P2 / OpenFileDialog-09 | Linux 使用 GTK4.10+ GtkFileDialog.open/open_multiple 的 native picker/portal；异步完成通过共享受控嵌套消息循环保持同步 ShowDialog，而非阻塞 UI 线程导致死锁。 |
| ⬜ | P3 / OpenFileDialog-10 | macOS 使用 NSOpenPanel，canChooseFiles/allowsMultipleSelection、allowedContentTypes、directoryURL 和 delegate 映射；FileOk veto/读取链接/扩展过滤行为与 NSOpenPanel 不同需逐项适配。 |
| ⬜ | P1 / OpenFileDialog-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / OpenFileDialog-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI/非视觉组件写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / OpenFileDialog-V3 | 发布 win-x64 Native AOT，检查实际窗口/原生组件与本类型交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | 已有旧式 GetOpenFileNameW；升级默认实现不改变 public API，现代/旧式 flags、回调和长路径均验收。 |
| Linux | GtkFileDialog/portal；Windows Shell CustomPlaces/ClientGuid 等 shell 专属属性单列，普通多选/过滤/取消不可排除。portal 自己的覆盖界面不保证支持所有 FileOk veto 钩子，必须原型验证。 |
| macOS | NSOpenPanel + delegate/响应选择，所有访问在 AppKit 主线程；筛选器文本和类型/扩展名规则需转换，不能只显示全部文件。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- `9d5165f`（2026-04-09）：扩展文件对话框相关兼容属性。
- `cb090bc`（2026-04-08）：加入记事本所需原生打开对话框。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms OpenFileDialog 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog?view=windowsdesktop-10.0)
- [GTK4 FileDialog](https://docs.gtk.org/gtk4/class.FileDialog.html)
- [AppKit nsopenpanel](https://developer.apple.com/documentation/appkit/nsopenpanel)
