# FolderBrowserDialog

🟡 **当前状态：**已有 SHBrowseForFolderW/SHGetPathFromIDListW、Description/SelectedPath/ShowNewFolderButton；输出固定 MAX_PATH，缺标准 CommonDialog 继承、RootFolder、现代 picker 与初始选中处理。

基线为 `b83f534`，评估日期 2026-10-15。状态表示证据强度；本页的待办不代表已有功能完成。属性、事件、行为各自达到 ≥98% 才可通过；Windows 使用完整固定基线，Linux/macOS 使用已审核的可移植分母，平台例外逐项列明，不能当作完成。

- 完整成员及继承成员：[FolderBrowserDialog API](../api/FolderBrowserDialog.md)。本文展开高影响工作，API 清单中未展开的成员仍全部进入验收。
- 共同要求：[基础契约与验收](../foundations.md)。
- 源码：[FolderBrowserDialog.cs](../../../Lumina.Forms/FolderBrowserDialog.cs)
- 前置与排序：CommonDialog/IWin32Window、文件系统路径、shared modal loop。

## 顺序任务

| 状态 | 优先级 / ID | 修改内容与验收证据 |
| --- | --- | --- |
| ⬜ | P0 / FolderBrowserDialog-01 | 改为标准 CommonDialog/Component 继承和 ShowDialog(IWin32Window)、Reset，构造/Dispose 与设计器 components 容器完全兼容。 |
| ⬜ | P0 / FolderBrowserDialog-02 | 补 RootFolder、SelectedPath、Description、ShowNewFolderButton、UseDescriptionForTitle、AutoUpgradeEnabled 及固定 API 清单中的现代属性；默认字符串不得硬编码为应用文案。 |
| ⬜ | P1 / FolderBrowserDialog-03 | 显示时定位 SelectedPath，返回实际选择且取消不误改；不存在目录、空路径、根目录、UNC、长路径与非文件系统 Shell 节点分别验收。 |
| ⬜ | P1 / FolderBrowserDialog-04 | Description 与 UseDescriptionForTitle 的组合，ShowNewFolderButton=false 以及 RootFolder 的范围限制在现代/旧式两路径分别核对。 |
| ⬜ | P1 / FolderBrowserDialog-05 | 实现 owner 模态/焦点恢复、Esc 取消、键盘导航、HelpRequest 如基线提供；重复显示/Reset 后状态与标准一致。 |
| ⬜ | P1 / FolderBrowserDialog-06 | 纠正 PIDL/CoTaskMem 和缓冲区归属，取消/错误路径均释放；避免把路径截断后的字符串返回 OK。 |
| ⬜ | P1 / FolderBrowserDialog-07 | VS 设计器非视觉组件可序列化 .resx 的 Description/SelectedPath；系统文件夹显示名、可访问性由 native UI 验收。 |
| ⬜ | P1 / FolderBrowserDialog-08 | Windows 默认 IFileOpenDialog + FOS_PICKFOLDERS，legacy SHBrowseForFolderW 只承担需要的旧行为；AOT-safe COM 与回调封装放库内。 |
| ⬜ | P2 / FolderBrowserDialog-09 | Linux 使用 GtkFileDialog.select_folder；Windows RootFolder 的 Shell 虚拟目录没有跨平台等价物，普通真实目录边界需明确映射而非全部标不支持。 |
| ⬜ | P3 / FolderBrowserDialog-10 | macOS 用 NSOpenPanel.canChooseDirectories=true/canChooseFiles=false；canCreateDirectories、directoryURL 和可选择路径 delegate 分别映射，Windows SpecialFolder 与 AppKit sandbox 访问差异单列。 |
| ⬜ | P1 / FolderBrowserDialog-V1 | 对完整 API 表逐项记录：签名、默认、getter/setter、运行时变更、事件订退和派生调用；把官方与 Lumina 同一场景的状态/事件序列作差异证据。缺失成员和只存值成员保留为未通过。 |
| ⬜ | P1 / FolderBrowserDialog-V2 | NativeFormsDemo 保持 net10.0-windows 真 WinForms + net10.0 Lumina 双目标；静态 UI/非视觉组件写入 .Designer.cs/.resx；完成 VS 设计器打开/保存/再打开、本地化资源和发布后实际操作验收。 |
| ⬜ | P1 / FolderBrowserDialog-V3 | 发布 win-x64 Native AOT，检查实际窗口/原生组件与本类型交互、裁剪告警、可访问性名称/角色/键盘与释放；句柄重建、父级 Dispose、重复 Dispose 后无回调和 native 资源泄漏。Linux/macOS 分别原生发布与同场景测试，不能用 Windows 结果代替。 |

## 三平台实现边界

| 平台 | 原生实现和差异 |
| --- | --- |
| Windows | 现有 SHBrowseForFolderW，默认现代路径迁移到 folder-mode Common Item Dialog；不必保留 MAX_PATH 限制。 |
| Linux | GtkFileDialog.select_folder/portal；精确控制初始目录/创建目录按钮的能力受 picker 后端限制，先验证再纳入 portable 分母。 |
| macOS | NSOpenPanel 目录模式；选择真实路径可原生完成，Shell namespace/RootFolder 虚拟节点为 🚫。 |

System.Drawing.Font/Image/Icon 的跨平台可用性属于前置阻塞；原生后端的可行方案不等于现有代码已经支持。HWND、GDI 句柄、Win32 消息级扩展等 Windows 契约分别标记 🚫 或 🔎，普通属性和事件不能因后端选择而任意排除。

## 修改历史

- `cb090bc`（2026-04-08）：加入原生目录选择对话框；只有此条文件修改历史。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

## 原始资料

- [WinForms FolderBrowserDialog 固定 API](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.folderbrowserdialog?view=windowsdesktop-10.0)
- [GTK4 FileDialog](https://docs.gtk.org/gtk4/class.FileDialog.html)
- [AppKit nsopenpanel](https://developer.apple.com/documentation/appkit/nsopenpanel)
