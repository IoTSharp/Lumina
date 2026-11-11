# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

## [Unreleased]

### M0 implementation — 2026-11-11

- Add standard ApplicationContext and FormCollection/OpenForms, the three void Run overloads, per-thread loop ownership, cancellable application exit, thread/application exit notifications and message filters. Closing an ordinary form outside Run no longer posts a global WM_QUIT.
- Add the modal Form.ShowDialog overloads and Modal/FormClosed contracts, independent nested message dispatch, cancellable dialog close, owner/window enable restoration and dialog reuse. Keep external hosts, newly created modal windows and extreme reentrancy as explicit remaining work.
- Restore abstract ButtonBase and the Button/CheckBox/RadioButton inheritance chain while sharing their existing native BUTTON, theme and default-button state.
- Connect ListView item/column ownership and native row/column/subitem updates. Connect TreeNode/TreeNodeCollection ownership, cycle rejection, HTREEITEM mapping and real insert/update/delete messages, including rollback and mapping cleanup.
- Extend the existing resource conversion to Icon byte resources/file references and invariant-string Font records with static runtime construction. Preserve the original Designer/resx files, independent resource objects and required stream lifetimes. Binary Font resources, ApplyResources and the existing Container/InheritanceAttribute IL3050 path remain open.
- Add full NativeFormsDemo win-x64 Native AOT restore/publish and artifact upload to CI. Restrict Core DWM entry points to Windows, document the actual Forms/Core dependencies and correct the quick-start extension namespace.
- Correct CloseReason values to the fixed WinForms 0–6 identities. Existing compiled constants and persisted integers for UserClosing, ApplicationExitCall and FormOwnerClosing require review or recompilation.
- Consolidate all dated task-page history into this root file. Roadmap pages retain task IDs and mark defined code scopes complete. Preserve the 2,204-task ledger, fixed 1,095-type / 61,240-member baseline and historical FAIL/timeout/UNKNOWN/NOT_RUN results.
- Validation is limited to the fixed dual-target Release build, necessary lightweight regressions and roadmap checks. Windows device/designer/complete AOT interaction and other platform acceptance are deferred; current receipts and remaining limits are maintained in [validation](docs/roadmap/validation.md).

The entries below describe changes at the time they landed. Later batches may close some of their then-open subscopes; current implementation status is maintained only in the [task index](docs/roadmap/README.md) and its source task pages. Current unresolved checks are in [validation](docs/roadmap/validation.md).

### Documentation — 2026-11-07

- Stop the current roadmap at unfinished milestones and dependencies after batch 069. Move 212 completed task rows into their original pages' history without changing task IDs, statuses or scope. Keep the full 2,204-task export and fixed 1,095-type / 61,240-member inventory.
- Consolidate document roles and navigation; replace the stale support-matrix snapshot with links to the current task index. Record the stop on continuous implementation in AGENTS and HANDOFF.
- Preserve [all 001–069 batch records](docs/roadmap/batches/README.md), the [frozen handoff ledger](docs/roadmap/batches/handoff-through-069.md) and [full validation ledger](docs/roadmap/batches/validation-through-069.md). Original failures, strict comparison differences, unrun gates and resource limitations remain available.
- This is a documentation change. No product/test implementation, build, full-suite, device or platform validation is performed by this cleanup.

### Earlier task-page change records — 2026-11-11



These dated source notes were moved from roadmap pages. Completion tables remain on the task pages; this archive does not update their implementation or acceptance state.



#### docs/roadmap/controls/WebBrowserBase.md

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

#### docs/roadmap/controls/WebBrowser.md

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

#### docs/roadmap/controls/VScrollBar.md

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 尚无该类型实现文件的提交历史；不将其他控件或本轮规划计为已实现。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

#### docs/roadmap/controls/UpDownBase.md

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 尚无该类型实现文件的提交历史；不将其他控件或本轮规划计为已实现。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

#### docs/roadmap/controls/TreeView.md

- 2026-10-16 · 批次023：基础Control启用标准UserPaint后，本控件构造显式保留UserPaint=false，使现有系统窗口类过程及原生字体/绘制继续生效。仅属消息与绘制基础的必要衔接，不代表本控件完整Styles、Paint事件或视觉/输入兼容完成，见[023](docs/roadmap/batches/023.md)。

- `108ef2b` · 2026-04-08：原生深色模式。
- `7149595` · 2026-04-08：深色模式与菜单绘制相关增强。
- `1c89c0a` · 2026-04-01：主题、控件初始化与样式逻辑。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/TreeNodeCollection.md

- `dcdc7e9` · 2026-03-31：TreeNode.cs 初次加入基础集合；未建立节点所属关系及原生同步。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/TreeNode.md

- `dcdc7e9` · 2026-03-31：新增基础 TreeNode 与 TreeNodeCollection；当前实现只覆盖少量模型字段。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/TrackBar.md

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 尚无该类型实现文件的提交历史；不将其他控件或本轮规划计为已实现。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

#### docs/roadmap/controls/ToolTip.md

- `2545995`（2026-04-08）：深色主题/UI 改进涉及提示组件。
- `dcdc7e9`（2026-03-31）：加入 ToolTip 字典与基础组件。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

#### docs/roadmap/controls/ToolStripTextBox.md

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 2026-03-31 · `52f5662` · Add core UI components for Lumina.Forms。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

#### docs/roadmap/controls/ToolStripStatusLabel.md

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 2026-04-08 · `7d7a67e` · Add initial implementation of Lumina.Forms library。
- 2026-03-31 · `52f5662` · Add core UI components for Lumina.Forms。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

#### docs/roadmap/controls/ToolStripSplitButton.md

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 2026-03-31 · `52f5662` · Add core UI components for Lumina.Forms。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

#### docs/roadmap/controls/ToolStripSeparator.md

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 2026-03-31 · `52f5662` · Add core UI components for Lumina.Forms。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

#### docs/roadmap/controls/ToolStripProgressBar.md

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 2026-03-31 · `52f5662` · Add core UI components for Lumina.Forms。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

#### docs/roadmap/controls/ToolStripPanelRow.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/ToolStripPanel.md

- 无同名类型的实现修改历史；不得将规划文档创建计入运行库实现完成。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

#### docs/roadmap/controls/ToolStripOverflowButton.md

- 截至 `b83f534` 未发现本类型的实现文件，无可归属的本类型实现提交。
- 宿主相关历史（不代表 overflow 已实现）：`ed64510` · 2026-04-08 · 菜单下拉状态与相邻导航；`275c479` · 2026-04-08 · 文本布局和高 DPI；`83e9356` · 2026-04-08 · 菜单图像资源处理。均来自 `git log -3 -- Lumina.Forms/ToolStrip.cs`。
- 2026-10-15 · 新增本独立规划，未修改实现。后续实现需记录日期、提交、具体溢出场景与验收证据。

#### docs/roadmap/controls/ToolStripOverflow.md

- 无同名类型的实现修改历史；不得将规划文档创建计入运行库实现完成。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

#### docs/roadmap/controls/DataGridColumnStyle.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/ToolStripMenuItem.md

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 2026-03-31 · `72fe8f6` · Refactor ToolStrip and Menu handling。
- 2026-03-31 · `37a8795` · feat: 添加菜单项单选框支持，增强菜单交互体验。
- 2026-03-31 · `0087cef` · feat: 添加菜单快捷键支持，增强用户交互体验。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

#### docs/roadmap/controls/ToolStripLabel.md

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 2026-04-08 · `7149595` · Implement dark mode capabilities and menu rendering enhancements。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

#### docs/roadmap/controls/ToolStripItemCollection.md

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 2026-04-09 · `9d5165f` · feat: 添加多个控件和功能，增强用户界面和交互体验。
- 2026-03-31 · `72fe8f6` · Refactor ToolStrip and Menu handling。
- 2026-03-31 · `52f5662` · Add core UI components for Lumina.Forms。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

#### docs/roadmap/controls/ToolStripItem.md

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 2026-04-09 · `9d5165f` · feat: 添加多个控件和功能，增强用户界面和交互体验。
- 2026-03-31 · `72fe8f6` · Refactor ToolStrip and Menu handling。
- 2026-03-31 · `46a9d63` · feat: 添加菜单助记符支持，增强菜单交互体验。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

#### docs/roadmap/controls/ToolStripDropDownMenu.md

- 无同名类型的实现修改历史；不得将规划文档创建计入运行库实现完成。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

#### docs/roadmap/controls/ToolStripDropDownItem.md

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 2026-03-31 · `72fe8f6` · Refactor ToolStrip and Menu handling。
- 2026-03-31 · `52f5662` · Add core UI components for Lumina.Forms。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

#### docs/roadmap/controls/ToolStripDropDownButton.md

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 2026-03-31 · `52f5662` · Add core UI components for Lumina.Forms。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

#### docs/roadmap/controls/ToolStripDropDown.md

- 无同名类型的实现修改历史；不得将规划文档创建计入运行库实现完成。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

#### docs/roadmap/controls/ToolStripControlHost.md

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 尚无该类型实现文件的提交历史；现有相似宿主行为不能代替标准类型实现。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

#### docs/roadmap/controls/DataGridBoolColumn.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGrid.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/CurrencyManager.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/ControlBindingsCollection.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/ContextMenuStrip.md

- `108ef2b`（2026-04-08）：接入 Win32 深色主题。
- `52f5662`（2026-03-31）：加入 ContextMenuStrip 初始能力。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

#### docs/roadmap/controls/ContextMenu.md

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

#### docs/roadmap/controls/ComponentEditorPage.md

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

#### docs/roadmap/controls/ComponentEditorForm.md

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

#### docs/roadmap/controls/CommonDialog.md

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

#### docs/roadmap/controls/ComboBox.md

- 2026-10-16 · 批次023：基础Control启用标准UserPaint后，本控件构造显式保留UserPaint=false，使现有系统窗口类过程及原生字体/绘制继续生效。仅属消息与绘制基础的必要衔接，不代表本控件完整Styles、Paint事件或视觉/输入兼容完成，见[023](docs/roadmap/batches/023.md)。

- `108ef2b` · 2026-04-08：接入 Win32 深色模式。
- `2e8431f` · 2026-04-01：主题和 Demo UI 改进；不代表全部列表行为已完成。
- `ce86ab5` · 2026-03-31：接入公共主题、颜色和窗口实时刷新能力。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/ColumnHeader.md

- 2026-11-07 · 批次044：共享HorizontalAlignment按固定v10.0.11纠正为Left=0、Right=1、Center=2；本类型及列集合源码no-op，按名字重编译保留名字语义，旧二进制常量/按整数持久化的1和2须迁移核查。此枚举修正不关闭ColumnHeader-02/04、标准继承或原生列同步，见[044](docs/roadmap/batches/044.md)。

- `2fb513a` · 2026-04-09：ListViewSupport.cs 加入基础 ColumnHeader/集合及 View 枚举；尚无原生列同步。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/ColorDialog.md

- `f666aa6`（2026-04-08）：UI/发布改进涉及颜色对话框。
- `791793f`（2026-04-08）：接入主题调色板支持。
- `57ef06f`（2026-04-08）：添加对话框主题应用逻辑。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

#### docs/roadmap/controls/Clipboard.md

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

#### docs/roadmap/controls/CheckedListBox.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/ButtonBase.md

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 尚无该类型实现文件的提交历史；不将其他控件或本轮规划计为已实现。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

#### docs/roadmap/controls/BindingSource.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/BindingsCollection.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/BindingNavigator.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/BindingManagerBase.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/BindingContext.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/Binding.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/BindableComponent.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/AxHost.md

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

#### docs/roadmap/controls/ApplicationContext.md

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

#### docs/roadmap/controls/ApplicationConfiguration.md

- `1c89c0a`（2026-04-01）：增强应用主题/初始化。
- `dcdc7e9`（2026-03-31）：加入启动配置入口。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

#### docs/roadmap/controls/AccessibleObject.md

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

#### docs/roadmap/controls/DataGridViewTextBoxCell.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewSelectedRowCollection.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewSelectedColumnCollection.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewSelectedCellCollection.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewRowHeaderCell.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewRowCollection.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewRow.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewLinkColumn.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewLinkCell.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewImageColumn.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewImageCell.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewHeaderCell.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewElement.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewComboBoxEditingControl.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewComboBoxColumn.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewComboBoxCell.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewColumnHeaderCell.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewColumnCollection.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewColumn.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewCheckBoxColumn.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewCheckBoxCell.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewCellStyle.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewCellCollection.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewCell.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewButtonColumn.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewButtonCell.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewBand.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewAdvancedBorderStyle.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridView.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridTextBoxColumn.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridTextBox.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridTableStyle.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/MainMenu.md

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

#### docs/roadmap/controls/ListViewInsertionMark.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/ListViewGroupCollection.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/ListViewGroup.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/ListView.md

- 2026-10-16 · 批次023：基础Control启用标准UserPaint后，本控件构造显式保留UserPaint=false，使现有系统窗口类过程及原生字体/绘制继续生效。仅属消息与绘制基础的必要衔接，不代表本控件完整Styles、Paint事件或视觉/输入兼容完成，见[023](docs/roadmap/batches/023.md)。

- 2026-10-16 · 批次016历史回标：移除当前结论中已过时的ListViewItem(string)基础空引用阻塞，保留001/003的原始修复提交和证据；任务01只标部分完成，未改变ListView源码，Items/Columns/完整所属/native及默认值差异仍缺，不新增验收声明。

- `2fb513a` · 2026-04-09：提交包含 ListView 功能和异步调用增强；现有集合仍未与原生内容同步。
- `2545995` · 2026-04-08：主题应用优化。
- `10dc1f4` · 2026-04-08：Win32 深色模式与窗口主题重构。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/ListControl.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/ListBox.md

- 2026-10-16 · 批次023：基础Control启用标准UserPaint后，本控件构造显式保留UserPaint=false，使现有系统窗口类过程及原生字体/绘制继续生效。仅属消息与绘制基础的必要衔接，不代表本控件完整Styles、Paint事件或视觉/输入兼容完成，见[023](docs/roadmap/batches/023.md)。

- `108ef2b` · 2026-04-08：接入 Win32 深色模式。
- `2e8431f` · 2026-04-01：主题和 Demo UI 改进。
- `ce86ab5` · 2026-03-31：公共主题表面与颜色刷新。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/LinkLabel.md

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 2026-03-31 · `dcdc7e9` · Add theme and control components to Lumina.Forms。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

#### docs/roadmap/controls/Label.md

- 2026-10-16 · 批次023：基础Control启用标准UserPaint后，本控件构造显式保留UserPaint=false，使现有系统窗口类过程及原生字体/绘制继续生效。仅属消息与绘制基础的必要衔接，不代表本控件完整Styles、Paint事件或视觉/输入兼容完成，见[023](docs/roadmap/batches/023.md)。

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 2026-04-09 · `9d5165f` · feat: 添加多个控件和功能，增强用户界面和交互体验。
- 2026-04-08 · `7d38999` · feat: 增强深色模式支持，优化多个控件的主题应用逻辑。
- 2026-03-31 · `ce86ab5` · 已经把 Lumina.Forms 的主题能力接到更像 WinForms 的核心表面上了。现在 Control.cs 和 Form.cs 提供了 BackColor / ForeColor、对应变更事件、主题槽位解析、父容器背景继承，以及打开窗口后的实时主题刷新；高频控件也统一改成走 ApplyExp…（原提交标题节录）。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

#### docs/roadmap/controls/ImageList.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/HScrollBar.md

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 尚无该类型实现文件的提交历史；不将其他控件或本轮规划计为已实现。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

#### docs/roadmap/controls/HelpProvider.md

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

#### docs/roadmap/controls/GridTableStylesCollection.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/GridItemCollection.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/GridItem.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/GridColumnStylesCollection.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/FontDialog.md

- `f666aa6`（2026-04-08）：UI/发布改进涉及字体对话框。
- `791793f`（2026-04-08）：接入主题调色板。
- `57ef06f`（2026-04-08）：添加字体对话框主题 hook。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

#### docs/roadmap/controls/FolderBrowserDialog.md

- `cb090bc`（2026-04-08）：加入原生目录选择对话框；只有此条文件修改历史。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

#### docs/roadmap/controls/FileDialog.md

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

#### docs/roadmap/controls/ErrorProvider.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DragEventArgs.md

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

#### docs/roadmap/controls/DomainUpDown.md

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 尚无该类型实现文件的提交历史；不将其他控件或本轮规划计为已实现。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

#### docs/roadmap/controls/DateTimePicker.md

- 2026-10-16 · 批次023：基础Control启用标准UserPaint后，本控件构造显式保留UserPaint=false，使现有系统窗口类过程及原生字体/绘制继续生效。仅属消息与绘制基础的必要衔接，不代表本控件完整Styles、Paint事件或视觉/输入兼容完成，见[023](docs/roadmap/batches/023.md)。

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 2026-04-08 · `2545995` · feat: 增强深色模式支持，优化多个控件的主题应用逻辑。
- 2026-04-08 · `108ef2b` · feat: 添加对 Win32 黑暗模式的支持，重构多个控件以优化主题应用逻辑。
- 2026-04-08 · `7149595` · Implement dark mode capabilities and menu rendering enhancements。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

#### docs/roadmap/controls/DataObject.md

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

#### docs/roadmap/controls/DataGridViewTopLeftHeaderCell.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewTextBoxEditingControl.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/DataGridViewTextBoxColumn.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/SplitterPanel.md

- 无同名类型的实现修改历史；不得将规划文档创建计入运行库实现完成。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

#### docs/roadmap/controls/Splitter.md

- `7d7a67e`（2026-04-08）：加入/整合 Splitter 初始实现；该文件只有这一条可见修改历史。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

#### docs/roadmap/controls/ScrollBar.md

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 尚无该类型实现文件的提交历史；不将其他控件或本轮规划计为已实现。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

#### docs/roadmap/controls/SaveFileDialog.md

- `9d5165f`（2026-04-09）：扩展保存对话框兼容属性。
- `cb090bc`（2026-04-08）：加入记事本所需原生保存对话框。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

#### docs/roadmap/controls/PropertyTab.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/PropertyManager.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/PropertyGrid.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/ProgressBar.md

- 2026-10-16 · 批次023：基础Control启用标准UserPaint后，本控件构造显式保留UserPaint=false，使现有系统窗口类过程及原生字体/绘制继续生效。仅属消息与绘制基础的必要衔接，不代表本控件完整Styles、Paint事件或视觉/输入兼容完成，见[023](docs/roadmap/batches/023.md)。

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 2026-04-08 · `108ef2b` · feat: 添加对 Win32 黑暗模式的支持，重构多个控件以优化主题应用逻辑。
- 2026-03-31 · `ce86ab5` · 已经把 Lumina.Forms 的主题能力接到更像 WinForms 的核心表面上了。现在 Control.cs 和 Form.cs 提供了 BackColor / ForeColor、对应变更事件、主题槽位解析、父容器背景继承，以及打开窗口后的实时主题刷新；高频控件也统一改成走 ApplyExp…（原提交标题节录）。
- 2026-03-31 · `dcdc7e9` · Add theme and control components to Lumina.Forms。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

#### docs/roadmap/controls/PrintPreviewDialog.md

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

#### docs/roadmap/controls/PrintPreviewControl.md

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

#### docs/roadmap/controls/PrintDocument.md

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

#### docs/roadmap/controls/PrintDialog.md

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

#### docs/roadmap/controls/PageSetupDialog.md

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

#### docs/roadmap/controls/OpenFileDialog.md

- `9d5165f`（2026-04-09）：扩展文件对话框相关兼容属性。
- `cb090bc`（2026-04-08）：加入记事本所需原生打开对话框。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

#### docs/roadmap/controls/NumericUpDown.md

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 2026-04-08 · `2545995` · feat: 增强深色模式支持，优化多个控件的主题应用逻辑。
- 2026-03-31 · `dcdc7e9` · Add theme and control components to Lumina.Forms。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

#### docs/roadmap/controls/NotifyIcon.md

- `cb090bc`（2026-04-08）：记事本集成涉及托盘能力。
- `2545995`（2026-04-08）：深色主题和组件改进。
- `108ef2b`（2026-04-08）：接入 Win32 深色主题/菜单能力。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。
- 2026-10-15 · 批次 001：托盘消息窗口创建前通知 Application 永久关闭 DPI 设置入口；不改变托盘图标的注册、更新或释放行为，实际托盘交互验收后置。

#### docs/roadmap/controls/NativeWindow.md

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

#### docs/roadmap/controls/MonthCalendar.md

- 2026-10-16 · 批次023：基础Control启用标准UserPaint后，本控件构造显式保留UserPaint=false，使现有系统窗口类过程及原生字体/绘制继续生效。仅属消息与绘制基础的必要衔接，不代表本控件完整Styles、Paint事件或视觉/输入兼容完成，见[023](docs/roadmap/batches/023.md)。

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 2026-04-08 · `7d7a67e` · Add initial implementation of Lumina.Forms library。
- 2026-04-08 · `2545995` · feat: 增强深色模式支持，优化多个控件的主题应用逻辑。
- 2026-04-08 · `108ef2b` · feat: 添加对 Win32 黑暗模式的支持，重构多个控件以优化主题应用逻辑。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

#### docs/roadmap/controls/MessageBox.md

- `9d5165f`（2026-04-09）：扩展 Show 重载等兼容表面。
- `f666aa6`（2026-04-08）：UI/发布改进涉及消息框。
- `d02239c`（2026-04-08）：消息框前景/最上层与外观调整。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

#### docs/roadmap/controls/MenuItemCollection.md

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

#### docs/roadmap/controls/MenuItem.md

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

#### docs/roadmap/controls/Menu.md

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

#### docs/roadmap/controls/MdiClient.md

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

#### docs/roadmap/controls/MaskedTextBox.md

- 2026-11-07 · 批次069：069本类生产/原任务状态no-op；RichTextBox-005d与Control-17g只补保护拒改及必要原生结果连接，不增加本类完成项。完整共享/Masked模型、缩放/线程/模态/原父Form寿命和平台保持；064FAIL/065超时未解，见[069](docs/roadmap/batches/069.md)。下述068及以前为历史。

- 2026-11-07 · 批次068：005b/005c只关闭对应选择/滚动/IME程序通知范围；原ID/未完父项/固定分母及064FAIL/065超时保持，见[068](docs/roadmap/batches/068.md)。

- 2026-11-07 · 批次067：本类源码/原任务状态no-op，真实模型/层级/输入与平台均继续原任务，不由Rich新范围计完成，见[067](docs/roadmap/batches/067.md)。

- 2026-11-07 · 批次066：限定RichTextBox-003e颜色同步，原ID/未完父项/固定分母保持；064全量FAIL及065超时不由本批检查清除，见[066](docs/roadmap/batches/066.md)。

- 2026-11-07 · 批次065：源码/原任务状态no-op；Rich-003d不增加本类型完成项，原ID/未完父项/固定分母保持。 本批全量180秒超时未完成，064全量FAIL/超时未解门槛保留，见[065](docs/roadmap/batches/065.md)。

- 2026-11-07 · 批次064：Rich选择类型和专有查询不增加Masked实现范围，仍按真实模型依赖继续，原父项保持，见[064](docs/roadmap/batches/064.md)。

- 2026-11-07 · 批次063：限定完成Rich-004c/004d；86新增/6,045全量及固定双目标Release通过，首85/86的新AllowNull反射夹具失败回执保留。原ID/未完父项/固定分母保持，见[063](docs/roadmap/batches/063.md)。

- 2026-11-07 · 批次062：062源码/任务no-op；Rich的003b/004b不实现Masked直接层级、MaskedTextProvider或专有输入。仍直接继承TextBox、Mask仅存值，原掩码父项/ID/固定分母与平台门槛保持。见[062](docs/roadmap/batches/062.md)。

- 2026-11-07 · 批次061：源码/任务no-op，Rich-003a/004a不计掩码模型/直接层级完成，原ID/未完项/固定分母保持，见[061](docs/roadmap/batches/061.md)。

- 2026-11-07 · 批次060：源码no-op，Rich的002c/011a不计掩码模型/直接层级或专有输入完成，原父项与分母保持，见[060](docs/roadmap/batches/060.md)。

- 2026-11-07 · 批次059：源码/任务no-op，Rich-002a/002b不计掩码支持或修复直接层级，原ID/未完项/固定分母保持，见[059](docs/roadmap/batches/059.md)。

- 2026-11-07 · 批次058：源码/原任务no-op，RichTextBox-001a/007a不计本类型掩码、专有限制或标准继承支持，见[058](docs/roadmap/batches/058.md)。

- 2026-11-07 · 批次057：普通EDIT的基类002d/003h不代表掩码逻辑长度/显示或专有隐藏成员已完成，源码no-op、原父项/固定分母保持，见[057](docs/roadmap/batches/057.md)。

- 2026-11-07 · 批次056：普通EDIT基类003f/003g不计掩码专有输入/隐藏成员限制，源码no-op、标准直接继承及模型仍待，原父项/固定分母保持，见[056](docs/roadmap/batches/056.md)。

- 2026-11-07 · 批次055：普通EDIT的TextBox-004b/TextBoxBase-003e不计本类型模型/索引/尺寸/专有限制；源码no-op，所有原父项保持，见[055](docs/roadmap/batches/055.md)。

- 2026-11-07 · 批次054：普通EDIT的007c/007d/007e不计掩码模型、专有尺寸/默认或限制支持；本类型源码与全部原任务保持，见[054](docs/roadmap/batches/054.md)。

- 2026-11-07 · 批次053：普通EDIT的007b/008c不计掩码模型/尺寸/默认/索引支持，本类型源码与原任务保持，见[053](docs/roadmap/batches/053.md)。

- 2026-11-07 · 批次052：基类008a/008b只验普通EDIT，不将其计为掩码索引/输入模型支持，本类型源码与原任务状态保持，见[052](docs/roadmap/batches/052.md)。

- 2026-11-07 · 批次051：普通EDIT词删除的间接消费不补掩码模型/专有限制或标准层级，源码no-op，所有原父任务保持，见[051](docs/roadmap/batches/051.md)。

- 2026-11-07 · 批次050：源码no-op，共享005e命令入口/003d快捷键禁用不代表掩码数据复制、过滤/拒绝输入或标准层级已实现，原ID/父任务和固定分母保持，见[050](docs/roadmap/batches/050.md)。

- 2026-11-07 · 批次049：源码no-op，普通EDIT共享Clear/重挂命令不关闭掩码专有行为或真实输入模型，原任务及固定分母保持，见[049](docs/roadmap/batches/049.md)。

- 2026-11-07 · 批次048：源码no-op，共享普通EDIT追加/撤销不实现掩码输入与Undo专有限制，不能按继承关闭任何原父任务，见[048](docs/roadmap/batches/048.md)。

- 2026-11-07 · 批次047：源码no-op，共享普通EDIT替换/分行/输入限制不代表掩码模型、专有限制或标准直接层级实现；原ID/父任务保持，见[047](docs/roadmap/batches/047.md)。

- 2026-11-07 · 批次046：源码no-op，共同边框与选择方法的间接消费不实现掩码/验证或标准直接基类；专有限制和真实输入模型继续原任务，见[046](docs/roadmap/batches/046.md)。

- 2026-11-07 · 批次045：源码no-op，基类Modified和错误TextBox层级间接暴露的CharacterCasing不关闭专有限制/隐藏元数据或掩码功能；完整标准直接继承和真实输入模型仍待，见[045](docs/roadmap/batches/045.md)。

- 2026-11-07 · 批次044：源码no-op，基类HideSelection及间接TextAlign不关闭标准直接层级、Mask执行/类型验证和专有限制；原ID/状态保留，见[044](docs/roadmap/batches/044.md)。

- 2026-11-07 · 批次043：源码no-op，间接共享真实折行/滚动样式及重建路径，不能据此关闭001或掩码/类型验证；标准直接基类/派生默认仍待，见[043](docs/roadmap/batches/043.md)。

- 2026-11-07 · 批次042：源码no-op，随TextBox间接消费新TextBoxBase共享EDIT首范围；仍直接继承TextBox且Mask仅存值，001/掩码执行/类型验证及基线专有限制均未关闭，见[042](docs/roadmap/batches/042.md)。

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 2026-04-08 · `2545995` · feat: 增强深色模式支持，优化多个控件的主题应用逻辑。
- 2026-03-31 · `dcdc7e9` · Add theme and control components to Lumina.Forms。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

#### docs/roadmap/controls/TaskDialogLinkClickedEventArgs.md

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

#### docs/roadmap/controls/TaskDialogIcon.md

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

#### docs/roadmap/controls/TaskDialogFootnote.md

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

#### docs/roadmap/controls/TaskDialogExpander.md

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

#### docs/roadmap/controls/TaskDialogControl.md

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

#### docs/roadmap/controls/TaskDialogCommandLinkButton.md

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

#### docs/roadmap/controls/TaskDialogButtonCollection.md

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

#### docs/roadmap/controls/TaskDialogButton.md

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

#### docs/roadmap/controls/TaskDialog.md

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

#### docs/roadmap/controls/TabPage.md

- `bca1ffd`（2026-04-08）：修正 TabPage 背景与前景色。
- `7149595`（2026-04-08）：深色模式与菜单相关提交涉及本文件。
- `ce86ab5`（2026-03-31）：接入核心主题继承。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

#### docs/roadmap/controls/StatusBarPanel.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/StatusBar.md

- 无此同名类型的实现Git历史。
- 2026-10-15：初次细化历史控件任务。
- 2026-10-15：遗漏审计结合官方10.0.11运行证据，纠正“导出类型等于可用旧UI”的假设；将历史UI恢复列为独立可选扩展。本轮未实现运行库功能。

#### docs/roadmap/controls/ThreadExceptionDialog.md

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

#### docs/roadmap/controls/TaskDialogVerificationCheckBox.md

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

#### docs/roadmap/controls/TaskDialogRadioButtonCollection.md

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

#### docs/roadmap/controls/TaskDialogRadioButton.md

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

#### docs/roadmap/controls/TaskDialogProgressBar.md

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

#### docs/roadmap/controls/TaskDialogPage.md

- 无本标准类型的实现Git修改历史。内部同类调用/现代替代控件的提交不冒充本类型已实现。
- 2026-10-15：遗漏审计后新增此独立任务页；本轮仅规划，没有实现上述运行库任务。

#### docs/roadmap/controls/ToolBarButton.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/ToolBar.md

- 无此同名类型的实现Git历史。
- 2026-10-15：初次细化历史控件任务。
- 2026-10-15：遗漏审计结合官方10.0.11运行证据，纠正“导出类型等于可用旧UI”的假设；将历史UI恢复列为独立可选扩展。本轮未实现运行库功能。

#### docs/roadmap/controls/Timer.md

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

#### docs/roadmap/controls/ToolStripComboBox.md

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 2026-03-31 · `52f5662` · Add core UI components for Lumina.Forms。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

#### docs/roadmap/controls/ToolStripButton.md

以下是该类型源路径的真实最近提交；提交标题说明历史变更范围，不能代替本轮验收：

- 2026-04-08 · `7149595` · Implement dark mode capabilities and menu rendering enhancements。
- 2026-10-15 · 本轮新增独立路线图，未修改实现。后续每次实现需在此追加日期、提交、具体差异与验收结果，宿主侧修改也需记录在本类型历史中。

#### docs/roadmap/controls/ToolStripContainer.md

- 无同名类型的实现修改历史；不得将规划文档创建计入运行库实现完成。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

#### docs/roadmap/controls/ToolStripContentPanel.md

- 无同名类型的实现修改历史；不得将规划文档创建计入运行库实现完成。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

### Roadmap task history moved from task pages — 2026-11-11



Historical change records were consolidated here so roadmap pages keep current scope, completion state and remaining dependencies. Source headings are retained for traceability; links were rebased to repository-root paths.



#### docs/roadmap/project-consolidation.md

| 日期 | 记录 | 范围 |
| --- | --- | --- |
| 2026-10-15 | 完成本轮整合；对应提交见Git历史 | WPF移除、效果线隔离、文档和逐控件路线图；不代表控件实现被补齐 |
| 2026-10-15 | 批次 001 分析器注入与分发完成 | 用户明确所有项目只支持 .NET 10；分析器改 net10.0，保留 codefix，工具依赖私有化；修复 NuGet 还原图继承应用 AOT 属性导致的 NETSDK1207。实际消费与发布证据见批次记录 |
| 2026-10-16 | 批次016历史状态回标 | REP-001的CI双目标构建、REP-004的README定位已存在，原全待办状态纠正为部分完成；完整Demo AOT/可见操作纳入CI及完整消费示例仍未完成，未改CI或扩大测试 |

实际验证结果写入 [本轮验证](docs/roadmap/validation.md)。引用关系检查命令：`rg -n 'ProjectReference' -g '*.csproj'`，并核对 Lumina.Forms/Form.cs、NativeTheme.cs、Application.cs 与 NativeFormsDemo.csproj。Git可查看移除前历史；不额外复制一份废弃WPF目录。

#### docs/roadmap/foundations.md

- 2026-11-07 · 批次069：005d/17g限定完成；旧ID/未完父项/固定分母与064FAIL/065超时保持，见[069](docs/roadmap/batches/069.md)。

- 2026-11-07 · 批次068：005b/005c只关闭对应选择/滚动/IME程序通知范围；原ID/未完父项/固定分母及064FAIL/065超时保持，见[068](docs/roadmap/batches/068.md)。

- 2026-11-07 · 批次067：Font/ContentsResized与必要停车通知均归既有控件子任务，不关闭FND父项；保持原任务ID/固定分母及未解全量失败，见[067](docs/roadmap/batches/067.md)。

- 2026-11-07 · 批次066：仅Rich-003e，不增加共享完成项，原ID/未完父项/固定分母保持；详见[066](docs/roadmap/batches/066.md)。

- 2026-11-07 · 批次065：Rich-003d只增加选区字体限定范围，不新增FND完成项；完整Font/颜色同步及基础/平台父项保留。本批全量180秒超时，064全量FAIL/超时仍未解决，不由93新增通过清除，见[065](docs/roadmap/batches/065.md)。

- 2026-11-07 · 批次064：Rich-008从全待办按三项实际查询整理为部分，003等未完父项保持；标准五值flags不关闭OLE或完整输入，FND-001/FND-011等状态保持，见[064](docs/roadmap/batches/064.md)。

- 2026-11-07 · 批次063：限定Rich-004c/004d，不增共享完成项、不关父项；同步真实代码/检查，纠正索引总述停060的滞后文字。原ID/1,095类型/61,240成员行/分母保持，见[063](docs/roadmap/batches/063.md)。

- 2026-11-07 · 批次062：只新增Rich-003b/004b，不增FND任务、不关父项；同步实际代码/轻量检查和未完范围，保留原ID/1,095类型/61,240成员行/固定分母，不由任务/测试数量推98%。

- 2026-11-07 · 批次061：FND-001原当前行遗漏058已落地的Rich直接层级/真实后端，FND-011仍保留048通知缺口而遗漏049修复；按现有Rich生产/Control通知接收者连接、对应原任务及本批5,849全量回归纠正，原历史失败快照不改。两个父项保持部分，不新增FND子任务或宣称完整后端/平台，原ID/固定分母保持，见[061](docs/roadmap/batches/061.md)。

- 2026-11-07 · 批次048：只新增TextBoxBase-005b/005c与Control-17d，TextBox-002/009按真实继承交叉引用，Rich/Masked不计专有支持。重挂后编辑通知是新发现未完范围，保留诊断并进入原父任务，不因43新增通过或缩小测试范围称完整重挂兼容；原ID/1,095类型/61,240成员行和固定分母保留。README实施列/tasks.csv由既有工具生成，见[048](docs/roadmap/batches/048.md)。

- 2026-11-07 · 批次047：只新增TextBoxBase-002b/002c/003c，完整002/003/005父项保持部分；TextBox-009因真实继承SelectedText/MaxLength从全待办纠正为部分，002同步Lines范围。Rich/Masked原父项不关闭，API固定成员行/原ID/1,095类型/61,240成员行保持；README实施列/tasks.csv由既有工具生成，见[047](docs/roadmap/batches/047.md)。

- 2026-11-07 · 批次044：共同HideSelection标准设置/通知与TextBox对齐创建样式完成明确范围；HorizontalAlignment按固定10.0.11修正，旧二进制/整数1/2持久化须复核。FND-001/013/014/019等父项保持部分，Control源码no-op，原生RTL夹具只证既有创建管线的样式消费；完整设备/Designer/AOT/平台/98%不关闭，见[044](docs/roadmap/batches/044.md)。

- 2026-11-07 · 批次043：TextBoxBase-003a补共同设置/标准AcceptsTabChanged，TextBox-004a使滚动条与折行真实生效；同值WordWrap仍清首选缓存并条件参与父布局。FND-014/019及完整继承/编辑/派生后端父项保留部分；不把原生EDIT样式/行数回归称设备/Designer/AOT/平台验收，见[043](docs/roadmap/batches/043.md)。

- 2026-11-07 · 批次042：FND-001/019按实际TextBoxBase共享首范围与IME原生路由审计更新；TextBoxBase原001～009及TextBox-001由全待办改部分，完整父项不关闭。历史API类型缺失快照与当前源码分开，原ID/1,095类型/61,240成员行保留，见[042](docs/roadmap/batches/042.md)。

- 2026-11-07 · 批次041：FND-019增加OLE前置和限定退出配对，Application-12按真实OleRequired实现由全待办改部分，原ID/固定分母保留。没有实现完整ThreadContext/模态或自然线程终止清理；原始夹具失败、其他HRESULT失败策略及MUI有界差异明确保留，见[041](docs/roadmap/batches/041.md)。

- 2026-11-07 · 批次040：FND-019补HKL服务与标准语言通知/取消；Application-11由全待办改部分。固定源码OleRequired的持久前置尚无完整线程context，明确保留差异，未用空方法或不平衡初始化掩盖。Form/Control/附属契约和所有未完父项/原ID/固定分母保留。

- 2026-11-07 · 批次039：FND-019补TextBox-005a与Control-12l，父项仍部分；TextBox-005由全待办改部分，明确此前已有ReadOnly原生映射及本批必要密码同步，完整复制限制/UIA/AutoComplete仍缺。旧028无句柄焦点用例显式隔离/还原未初始化IME传播缓存，保留原断言，非删除历史差异；固定分母/原ID不变。

- 2026-11-07 · 批次038：FND-019新增Control-12k上下文和TextBox-003c生命周期前置，并纠正父表仍遗漏037的记录；FND-014/026明确UserControl-02d仅标准声明/事件与共享布局消费，不关闭完整布局或Designer。TextBoxBase缺失保留，但其“现有TextBox无句柄Select直接返回”旧描述按003c纠正，固定分母/原ID不变。

- 2026-11-07 · 批次037：FND-019补Control-12j有限IME字符消息与TextBox-003b派生键规则；FND-021由UserControl-05a覆盖嵌套焦点/验证有限组合。补录036原生输入判别、字符队列抑制与验证取消清理，纠正公共父项滞后，未关闭完整IME/验证/平台。

- 2026-11-07 · 批次035：FND-018/019/021吸收实际队列及默认/取消按钮必要验证消费，FND-014基础AutoSize命名行为在枚举数值修正后保留。仅明确子项代码完成，完整IME/原生点击/布局/设备/平台仍待；同步Button原全待办但已有局部实现与索引029停止状态滞后，见[035](docs/roadmap/batches/035.md)。

- 2026-10-26 · 批次031：FND-013补RightToLeft托管/Win32样式首范围；FND-016补AutoScroll/ScrollProperties/Scroll事件/受保护滚动契约首范围；FND-017补AutoScaleFactor；UserControl接入标准ContainerControl继承。所有父任务仍保持🟡，真实滚动条、Cursor、完整缩放/设计器、派生与平台门禁保留，见[031](docs/roadmap/batches/031.md)。
- 2026-10-26 · 批次032：FND-016补Panel迁移与Windows SCROLLINFO/WM_HSCROLL/WM_VSCROLL首范围；UserControl补Load一次触发。真实物理设备、完整滚动/Panel属性、Designer/AOT和平台仍待，见[032](docs/roadmap/batches/032.md)。

- 2026-11-07 · 批次033：FND-019补Control键盘预处理首范围，ContainerControl补Tab/助记键后代路由，FND-016补已有偏移下的后代可见坐标修正；完整输入、物理设备、Designer/AOT和平台仍待，见[033](docs/roadmap/batches/033.md)。

- 2026-10-26 · 批次030：FND-019回标为部分完成，新增Control-12e基础键消息/事件契约与5项Windows STA回归；完整键盘预处理、IME、真实设备、Designer、AOT及跨平台仍待办，见[030](docs/roadmap/batches/030.md)。

- 2026-10-17 · 批次029：验证正式归FND-021，纠正准备说明只沿FND-018/019而漏记验证任务；FND-019键盘/IME仍待办。有限程序验证/自动转移完成，关闭LostFocus/WM_QUIT及最终JIT-AOT差异保留，不关闭完整输入/验证，见[029](docs/roadmap/batches/029.md)。

- 2026-10-16 · 批次028：FND-018补基础选择/事件/活动链范围，FND-019键盘与验证前置仍未完成；纠正非标准Focus(bool)旧表述及Form局部能力漏标。4,343测试/30场景JIT-AOT全同不等于98%；12个参考差异、物理设备/Designer/跨平台边界保留，见[028](docs/roadmap/batches/028.md)。

- 2026-10-16 · 批次027：FND-020按真实坐标/捕获/鼠标基础实现改部分；CNS补标准参数/枚举，Control-12、CheckBox-004、RadioButton-003及RadioButton父链的过时状态已校正。4,289测试通过、25共同场景保留5项参考/1项AOT差异及锁屏命中限制；不关闭父项/平台，见[027](docs/roadmap/batches/027.md)。

- 2026-10-16 · 批次026：FND-002/004/012随05c/08c及实际Demo AOT更新；4,243测试、32场景JIT/AOT全同。固定参考25场景有9项关闭/释放增强差异，旧004/005各两行差异保留严格失败，025历史4差异保持。原生失败后后续托管错误仍短路，完整竞态/资源/设计器/平台不关闭；见[026](docs/roadmap/batches/026.md)。

- 2026-10-16 · 批次025：FND-002/004/011/012按普通Drain和native callback边界实际完成范围更新；保留InvokeAsync关闭、Dispose后续托管事务、完整Control/Form/Designer/资源与GTK4/AppKit缺口。8场景参考/JIT/AOT全部正常退出，4项真实差异保留，不由测试数或局部边界关闭父项，见[025](docs/roadmap/batches/025.md)。

- 2026-10-16 · 批次024：FND-002/011/012/013/023按04b/14b实际完成范围更新，修正“Close仍中断余队列”“背景图像与可见透明全缺”的过时描述。独立列出普通Drain、InvokeAsync关闭、Dispose后续托管清理和原生双错缺口；本批Windows屏幕证据不扩为Designer/全派生/跨平台验收，ID及固定分母不变，见[024](docs/roadmap/batches/024.md)。

- 2026-10-16 · 批次023：FND-023更新为标准消息/样式/基础绘制已实现明确子范围，CNS补Message/CreateParams/ControlStyles及Paint/Invalidate参数委托。修正“标准绘制全缺”的当前描述，保留所有未完父任务、ID和固定分母；原生Form消息及销毁边界本批修复，完整Control/平台未关闭，见[023](docs/roadmap/batches/023.md)。

- 2026-10-16 · 批次022：FND-012/013补基础字体寿命与外观父链子范围，FND-023/Control-14纠正已有Invalidate/内部窗口过程仍全待办；GroupBox标准Controls/部分默认、Form继承缩放/Win32、TabControl与条带原生实现回标部分。全部未完父项、ID/固定分母及真实Styles/theme差异保留，见[022](docs/roadmap/batches/022.md)。

- 2026-10-16 · 批次021：FND-014/015按真实默认布局/首选尺寸及引擎缓存子范围更新；CNS-008不再全待办。Control-17的既有Win32实现、Flow/Table有限测量从全待办改部分；020七行差异已修但原始历史保留。全部父项/平台/98%未关闭，见[021](docs/roadmap/batches/021.md)。

- 2026-10-16 · 批次020：FND-015改为已完成标准事务首范围而保留引擎/缓存/几何缺口，FND-014补必要属性通知。纠正Panel对018标准层/集合的遗漏、Form.Layout的过时状态、CNS事件/集合的历史遗漏；只有明确事务子任务代码完成，完整控件/平台和98%均未关闭，详见[020](docs/roadmap/batches/020.md)。

- 2026-10-16 · 批次019：FND-001/002/010～012补标准句柄和Form必要显示衔接，纠正“Handle仍非延迟/入口不存在”和“018重挂可见消息仍未修复”的当前状态；018历史快照与失败证据保留。FND-015按实际空实现明确下一批依赖，其他完整资源/布局/线程/Designer/平台父项仍部分或待办，不把204条运行对照或基类完成转成整类型/98%声明，见[019](docs/roadmap/batches/019.md)。

- 2026-10-16 · 批次018：按真实代码补Control-06a/Form-01/ContainerControl-01/ScrollableControl-01a完成子范围，更新原“Parent/Controls/Form链及标准容器不存在”的过时描述。FND-010～018按实际部分实现回标，保留全部剩余任务、原ID/分母；特别纠正FND-017遗漏历史Form局部缩放的状态。完整句柄/布局/滚动/验证、Designer/资源和GTK4/AppKit均未关闭，运行证据见[018](docs/roadmap/batches/018.md)。

- 2026-10-16 · 批次017：FND-003补未挂接ImageIndex/ImageKey首范围，父任务仍为部分完成。按实际代码核对Control-11/11a及ListViewItem-04/04b，已完成子项单列，ImageList/native/转换器/编辑器与完整所属保留；不把旧“图像全部待办”继续当作当前未挂接状态缺口，也不关闭完整图像功能。保留001～016历史快照、ID及固定分母，无新整控件或平台验收。

- 2026-10-16 · 批次 016：按实际代码与本批必要检查更新 FND-001/003 的已完成子范围，父任务仍 🟡；FND-014 原“待办”遗漏了 009～016 已完成的基础边界/客户区/约束子范围，纠正为 🟡 并保留布局等剩余项。没有把原父任务误标为 ✅，没有删除/重编号未完项，FND-004 真机主窗和 FND-006 既有 IL3050/完整资源仍待完成。此处为状态整理，不新增运行验收或98%声明。

#### docs/roadmap/contracts.md

- 2026-11-07 · 批次069：005d/17g限定完成；旧ID/未完父项/固定分母与064FAIL/065超时保持，见[069](docs/roadmap/batches/069.md)。

- 2026-11-07 · 批次068：005b/005c只关闭对应选择/滚动/IME程序通知范围；原ID/未完父项/固定分母及064FAIL/065超时保持，见[068](docs/roadmap/batches/068.md)。

- 2026-11-07 · 批次067：标准尺寸事件参数/delegate归Rich-005a，不以类型存在关闭共享契约；原ID/未完父项/分母及064FAIL/065超时保持，见[067](docs/roadmap/batches/067.md)。

- 2026-11-07 · 批次066：仅Rich-003e，不增加共享完成项，原ID/未完父项/固定分母保持；详见[066](docs/roadmap/batches/066.md)。

- 2026-11-07 · 批次065：SelectionFont仅增加Rich私有字体转换，不增加公共类型/依赖或CNS完成项；原ID/未完父项/分母保持，见[065](docs/roadmap/batches/065.md)。

- 2026-11-07 · 批次064：增加SelectionType所需标准五值flags，三项Rich位置/行查询沿既有内部POINT；CNS-002与完整附属契约仍部分，不重复计数，见[064](docs/roadmap/batches/064.md)。

- 2026-11-07 · 批次063：限定Rich-004c/004d，不增共享完成项、不关父项；同步真实代码/检查，纠正索引总述停060的滞后文字。原ID/1,095类型/61,240成员行/分母保持，见[063](docs/roadmap/batches/063.md)。

- 2026-11-07 · 批次062：复用既有Pack4结构/SendMessage与GetDC/GetDeviceCaps/ReleaseDC，DC在finally释放；无新PInvoke/依赖/公共模型，不新增CNS完成任务，原ID与分母保持，见[062](docs/roadmap/batches/062.md)。

- 2026-11-07 · 批次061：Rich-003a/004a使用私有Pack4 CHARFORMAT2W/PARAFORMAT映射及既有SendMessage，新增SelectionBackColor/SelectionAlignment标准属性，纠正SelectionColor存值行为；无新公共类型/依赖/PInvoke。不新增CNS完成项，完整格式/Font/ForeColor/BackColor、Designer/AOT运行与平台仍待，见[061](docs/roadmap/batches/061.md)。

- 2026-11-07 · 批次060：Rich新增标准Rtf/SelectedRtf与静态Stdcall流出回调、逐调用cookie/MemoryStream，复用Pack4 EDITSTREAM和既有SendMessage，无新PInvoke/公共流模型/依赖。归Rich-002c/011a，不新增CNS完成项；完整文件/Protected/OLE、格式API与Designer/AOT运行仍待，见[060](docs/roadmap/batches/060.md)。

- 2026-11-07 · 批次059：Rich纯文回调以静态UnmanagedCallersOnly/Stdcall和独立cookie实现，复用现有SendMessage；SDK核GETTEXTEX/EDITSTREAM的Pack4。必要内部结构归Rich-002a/002b，不新增公共契约或CNS完成项，完整RTF/格式/Protected/OLE/Designer/AOT运行保持待办，见[059](docs/roadmap/batches/059.md)。

- 2026-11-07 · 批次058：补标准RichTextBoxScrollBars七值0/1/2/3/17/18/19及真实消费者，归RichTextBox-007a，不新增共同完成任务或命名；复用现有Win32加载/消息声明，未扩写完整绘制/绑定/Designer/OLE。原CNS父项、ID及固定分母保持，见[058](docs/roadmap/batches/058.md)。

- 2026-11-07 · 批次057：默认事件/默认绑定、Paint转发和选区/编辑分类限定声明归TextBoxBase-003h，TextLength声明/原生读取归002d；不新增公开类型、绑定引擎或Designer，不扩写CNS-005/007/006等父项。原ID/固定分母与完整待办保持，见[057](docs/roadmap/batches/057.md)。

- 2026-11-07 · 批次056：必要隐藏/可见成员元数据与基类转发归TextBoxBase-003g，Click声明及构造样式/左键链归003f；无新公开类型或名称，不扩写VisualStyles/可访问性完成范围。原附属类型父项、任务ID与固定类型/成员分母保持，见[056](docs/roadmap/batches/056.md)。

- 2026-11-07 · 批次055：必要SM2/3按线程感知读取归TextBox-004b，创建请求/前置主题标志归TextBoxBase-003e；无新增公共类型/API，不扩写CNS-006a或主题VisualStyles完成范围。原父项、任务ID与固定类型/成员分母保持，见[055](docs/roadmap/batches/055.md)。

- 2026-11-07 · 批次054：新增尺寸范围及必要控件DPI/原生边框度量归TextBoxBase-007c/007d/007e；CNS-006a只保留053无DC测量范围，不将其扩写为完整TextRenderer/IDeviceContext/DrawText完成。原附属类型父项、任务ID及固定分母保持，见[054](docs/roadmap/batches/054.md)。

- 2026-11-07 · 批次053：CNS-006从全待办纠正为无DC测量子范围完成/完整父项部分，保留未完Drawing/VisualStyles任务；006a登记六重载及必要TextFormatFlags/Padding.Size/DPI辅助，控件页不重复计数。固定API库存与原ID/分母不改，见[053](docs/roadmap/batches/053.md)。

- 2026-11-07 · 批次041：CNS-012沿Application-11b/12a补OLE前置及最小owner退出配对；CNS-001/003的040事件/集合保持，未关闭任何完整父项。完整context/模态/输入法及平台仍待，固定源码其他失败HRESULT与MUI有界差异单列，见[041](docs/roadmap/batches/041.md)。

- 2026-11-07 · 批次040：CNS-001/003/012分别补标准输入语言事件参数/委托、只读集合和HKL服务，行为归Application-11a、Form-12d及Control-12m。所有父任务保持部分，OLE持久前置/完整输入法和平台验收不由新类型存在关闭，见[040](docs/roadmap/batches/040.md)。

- 2026-11-07 · 批次035：CNS-002同步AutoSizeMode修正和DialogResult/IButtonControl必要消费。原固定API10.0.11引用包缺失，新增面未重导库存，不以旧库存证明新API匹配；Keys.Return别名缺口由首次编译暴露并保留，见[035](docs/roadmap/batches/035.md)。

- 2026-10-16 · 批次027：CNS-001/002补MouseEventArgs.Location/可继承、HandledMouseEventArgs和XButton1/2及真实事件消费。错误委托强转的命名空间/Native AOT异常文本差异原样保留，完整契约和平台仍未关闭，见[027](docs/roadmap/batches/027.md)。

- 2026-10-16 · 批次024：CNS-002补ImageLayout与实际背景绘制消费，继承页面/API索引同步；五值存在不等于全部枚举或整个Control完成，见[024](docs/roadmap/batches/024.md)。

- 2026-10-16 · 批次023：CNS-001/002补标准绘制参数/委托、Message/CreateParams和ControlStyles明确首范围，未关闭所有附属契约；GetLParam(Type)/自定义Param动态封送与完整事件/平台仍保留，见[023](docs/roadmap/batches/023.md)。

- 2026-10-16 · 批次021：CNS-008由全待办纠正为LayoutEngine基础扩展已存在；LayoutSettings、非Control arranged对象与完整Flow/Table样式/序列化未关闭。标准默认base engine无几何策略，实际Control使用真实DefaultLayout；不将签名存在当完整布局验收。

- 2026-10-16 · 批次020：纠正CNS-001遗漏018 ControlEventArgs/Handler的全待办状态，并加入020 LayoutEventArgs/Handler明确范围；CNS-003补018标准父子集合。CNS-008的LayoutEngine/LayoutSettings仍待实现，不能把布局事件事务当完整布局命名空间；固定分母/历史ID均保留。

2026-10-15：本次新增全类型责任入口和验收要求，未新增上述缺失实现；各已有源文件历史以Git为准。公共类型已有历史名称不重命名，实施时优先标准WinForms契约。

2026-10-16 · 批次016历史回标：CNS-002补001的HighDpiMode与010的BoundsSpecified，CNS-003补006～012子项集合明确首范围，CNS-012补004/005标准InvokeAsync与007默认await恢复；从过时的全待办纠正为部分完成，其他枚举/集合/异步命令/完整所属/native及验收继续保留。仅整理已有实现和证据，不改变历史提交、任务ID或固定分母。

#### docs/roadmap/controls/Application.md

- 2026-11-07 · 批次069：069本类生产/原任务状态no-op；RichTextBox-005d与Control-17g只补保护拒改及必要原生结果连接，不增加本类完成项。完整共享/Masked模型、缩放/线程/模态/原父Form寿命和平台保持；064FAIL/065超时未解，见[069](docs/roadmap/batches/069.md)。下述068及以前为历史。

- 2026-11-07 · 批次068：005b/005c只关闭对应选择/滚动/IME程序通知范围；原ID/未完父项/固定分母及064FAIL/065超时保持，见[068](docs/roadmap/batches/068.md)。

- 2026-11-07 · 批次067：生产与原任务状态保持；测试STA清理沿既有ExitThread再释放私有视觉context，不新增应用层完成项，见[067](docs/roadmap/batches/067.md)。

- 2026-11-07 · 批次054：内部窗口感知判断按固定源码区分窗口与线程context，服务007c度量依赖；Application.cs源码no-op，原父任务保持，见[054](docs/roadmap/batches/054.md)。

- 2026-11-07 · 批次041：新增11b与12a，12由全待办改部分，保留全部原ID/未完API。最小ThreadStatic OLE状态复用现有退出路径，不引入完整ThreadContext；其他失败HRESULT显式传播是相对固定v10.0.11的明确差异。首轮9个内部服务测试在调用库前误假定CLR未初始化COM，保留失败并修正原生夹具；生产代码未为夹具改变，见[041](docs/roadmap/batches/041.md)。

- 2026-11-07 · 批次040：新增11a的真实HKL服务与Application入口，11由全待办改部分。明确保留固定v10.0.11的OleRequired初始化副作用差异与完整线程寿命，未造空OleRequired或假成功；完整11/12、输入法/模态和平台不关闭，见[040](docs/roadmap/batches/040.md)。

- 2026-11-07 · 批次039：Application.cs未改，Control-12l语言消息不关闭11或完整应用级语言服务；Form事件/取消与InputLanguage公共类型仍待，见[039](docs/roadmap/batches/039.md)。

- 2026-11-07 · 批次035：06a接入实际键盘队列预处理，补Tab、KeyPreview、Enter/Escape、异常及原生子窗映射限定证据；首轮改写KeyUp测试缺transition位导致真实Translate额外WM_CHAR，修正有效消息后保持原断言通过。既有WM_QUIT/多UI退出差异不因本批关闭，见[035](docs/roadmap/batches/035.md)。

- 2026-11-07 · 批次036：06a的队列消费继续由Application保持原路径，字符抑制由Control按固定WM_CHAR/WM_SYSCHAR/WM_IME_CHAR消息类型有界移除；未扩展消息过滤器、DoEvents、模态或完整退出语义，见[036](docs/roadmap/batches/036.md)。

- 2026-10-17 · 批次029：程序验证接入未修改Application退出实现；15个场景仍多出WM_QUIT，继续归01/04与Form-07。另有关闭LostFocus差异，不混为同一根因；用户要求本批后停止，见[029](docs/roadmap/batches/029.md)。

- 2026-10-16 · 批次028：未改Application代码；焦点探针的11个原生窗体场景关闭后出现参考没有的WM_QUIT，定位既有Form按全局OpenForms退出行为，归01/04保留待修。所有进程exit0不等于循环语义兼容；后续需覆盖Run外关闭、主窗与最后窗口、多UI线程及取消，见[028](docs/roadmap/batches/028.md)。

- 2026-10-16 · 批次016历史回标：任务04从全待办纠正为部分完成，只补007已有的无Form ExitThread请求/最外层线程activation释放和两个STA证据；Exit取消/事件/ApplicationContext/完整多UI与竞态仍待完成，未改源码或历史批次记录。

- `9d5165f`（2026-04-09）：新增启动兼容功能，含待修正 DPI 表面。
- `1c89c0a`（2026-04-01）：增强主题与控件初始化。
- `2e8431f`（2026-04-01）：主题和 Demo UI 改进涉及启动层。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。
- 2026-10-15 · 批次 001：修复启动签名、命名参数/null 主窗路径；新增 AOT 友好的 DPI interop、真实状态回读及建窗门禁；6 项无副作用回归、14 组独立进程 DPI 对照通过。Run 的其他重载及完整生命周期未关闭，Windows/Linux/macOS 的完整兼容验收仍待后续。
- 2026-10-16 · 批次 003：新增标准 ThreadException/OnThreadException 的首个可观察范围；4 项托管测试覆盖对象身份/线程隔离/重入/异常恢复，线程探针证明未调用 EndInvoke 的错误也在 UI 线程上报。默认原生 OK 提示不等同标准 ThreadExceptionDialog，SetUnhandledExceptionMode 及原生回调全族仍未完成；错误事件处理器再抛时显式 FailFast 防止越过 native ABI。

- 2026-10-16 · 批次 007：现有 Run/RunMessageLoop 共享安装与外层 finally 还原；204 条参考/JIT/实际 AOT 观测和新增 STA 检查覆盖标准 AutoInstall/Uninstall、默认 await 与两个不同 STA 的无 Form 循环。ExitThread 不在 HWND 回调内提前 DeactivateActCtx，实际释放移至外层循环退出后，激活资源改为线程归属并允许下次进入重新激活。全局 OpenForms 的最后窗体退出判断、ApplicationContext/完整 Run/模态与并行 UI 退出仍保留，见 [批次 007](docs/roadmap/batches/007.md)。

#### docs/roadmap/controls/Button.md

- 2026-11-07 · 批次035：实现001a/002a/003a/004a作为Form-12b必要消费者，18项按钮/默认路由新测试和1项真实队列集成通过。Button仍继承Control，001/003/004按实际局部实现由全待办改部分；原生点击前置、完整ButtonBase/模态/移除释放组合、辅助技术及平台仍缺，见[035](docs/roadmap/batches/035.md)。

- 2026-11-07 · 批次036：完成002b源码审计和真实HWND鼠标按下/抬起回归；Button.cs保留官方BN_CLICKED仅检查ValidationCancelled的路径，未添加重复验证，验证取消/父链清理由Control共享实现。完整ButtonBase、设备、样式、辅助技术和模态仍待，见[036](docs/roadmap/batches/036.md)。

- 2026-10-16 · 批次027：移除遮蔽Control的独立Click事件，OnClick覆盖转发base，PerformClick/BN_CLICKED进入同一虚事件槽；StandardClick/StandardDoubleClick关闭以避免base鼠标重复激活。只关闭该必要接入，ButtonBase/FlatStyle/验证/默认按钮仍缺。 027完成005a共享Click/原生通知衔接；鼠标与空格/Enter/Alt助记键、辅助技术、命令/验证/FormClosing完整顺序仍待对照。默认owner draw/UserMouse与当前native样式及BM_CLICK顺序差异保留。 25项必要消费回归合计通过；固定参考消费者差异及锁屏限制见[027](docs/roadmap/batches/027.md)，不关闭整个控件/平台。

- 2026-10-16 · 批次023：基础Control启用标准UserPaint后，本控件构造显式保留UserPaint=false，使现有系统窗口类过程及原生字体/绘制继续生效。仅属消息与绘制基础的必要衔接，不代表本控件完整Styles、Paint事件或视觉/输入兼容完成，见[023](docs/roadmap/batches/023.md)。

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 2026-04-08 · `7d7a67e` · Add initial implementation of Lumina.Forms library。
- 2026-04-08 · `108ef2b` · feat: 添加对 Win32 黑暗模式的支持，重构多个控件以优化主题应用逻辑。
- 2026-04-01 · `1c89c0a` · feat: 增强 Lumina.Forms 的主题支持，添加控件初始化和样式逻辑。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

#### docs/roadmap/controls/CheckBox.md

- 2026-10-16 · 批次027：BN_CLICKED先读取BM_GETCHECK并同步既有Checked/CheckedChanged，再调用共享OnClick一次；关闭StandardClick/StandardDoubleClick防重复。范围只限现有二态原生通知衔接，直接OnClick/AutoCheck/三态仍归父项。 已有CheckedChanged，027完成004a当前二态原生通知→共享Click；原全待办遗漏历史事件，现纠正。CheckStateChanged、三态/AutoCheck、直接OnClick自动切换与完整事件顺序仍缺，不由bool比较替代Indeterminate语义。 25项必要消费回归合计通过；固定参考消费者差异及锁屏限制见[027](docs/roadmap/batches/027.md)，不关闭整个控件/平台。

- 2026-10-16 · 批次023：基础Control启用标准UserPaint后，本控件构造显式保留UserPaint=false，使现有系统窗口类过程及原生字体/绘制继续生效。仅属消息与绘制基础的必要衔接，不代表本控件完整Styles、Paint事件或视觉/输入兼容完成，见[023](docs/roadmap/batches/023.md)。

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 2026-04-09 · `9d5165f` · feat: 添加多个控件和功能，增强用户界面和交互体验。
- 2026-04-08 · `2545995` · feat: 增强深色模式支持，优化多个控件的主题应用逻辑。
- 2026-04-08 · `108ef2b` · feat: 添加对 Win32 黑暗模式的支持，重构多个控件以优化主题应用逻辑。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

#### docs/roadmap/controls/Control.md

- 2026-11-07 · 批次069：069的Control-17g限定补WM_NOTIFY原生结果传递：保留旧OnNotify/内部入口默认0结果，Rich的拒改决定经Form、容器和自有停车接收者回到原生。Form以来源HWND识别发送者，避免ID碰撞；原父Form销毁/重建寿命、其他完整反射/平台仍待，见[069](docs/roadmap/batches/069.md)。064FAIL/065超时保持；下述068及以前为历史。

- 2026-11-07 · 批次068：005b/005c只关闭对应选择/滚动/IME程序通知范围；原ID/未完父项/固定分母及064FAIL/065超时保持，见[068](docs/roadmap/batches/068.md)。

- 2026-11-07 · 批次067：17f限定停车WM_NOTIFY连接，主仅改该分支，不关闭原父Form寿命/完整反射/17父项；原ID/平台/固定分母及064FAIL/065超时保持，见[067](docs/roadmap/batches/067.md)。

- 2026-11-07 · 批次066：限定RichTextBox-003e颜色同步，原ID/未完父项/固定分母保持；064全量FAIL及065超时不由本批检查清除，见[066](docs/roadmap/batches/066.md)。

- 2026-11-07 · 批次065：源码/原任务状态no-op；Rich-003d不增加本类型完成项，原ID/未完父项/固定分母保持。 本批全量180秒超时未完成，064全量FAIL/超时未解门槛保留，见[065](docs/roadmap/batches/065.md)。

- 2026-11-07 · 批次064：Rich选择类型与专有位置/行查询复用现有内部结构和消息入口，本类源码不变，不由局部查询关闭完整Windows后端或平台任务，见[064](docs/roadmap/batches/064.md)。

- 2026-11-07 · 批次063：限定完成Rich-004c/004d；86新增/6,045全量及固定双目标Release通过，首85/86的新AllowNull反射夹具失败回执保留。原ID/未完父项/固定分母保持，见[063](docs/roadmap/batches/063.md)。

- 2026-11-07 · 批次062：源码no-op，Rich偏移/保护标记/段落缩进沿既有interop，不新增公共API或Control完成范围；完整父项与平台门槛保持，见[062](docs/roadmap/batches/062.md)。

- 2026-11-07 · 批次061：源码/任务no-op，Rich-003a/004a的私有blittable格式结构与标准属性不扩写Control完成范围，原父项/ID/分母保持，见[061](docs/roadmap/batches/061.md)。

- 2026-11-07 · 批次060：源码no-op，Rich静态RTF流回调/缓存归002c/011a，现有生命周期连接保持；不由隐藏窗口格式往返关闭完整反射/线程/资源或平台父项，见[060](docs/roadmap/batches/060.md)。

- 2026-11-07 · 批次059：既有父链Disposing查询仅放宽同库可见性，无算法/公共面扩写，64新增/5,728全量通过；不新增Control完成项，见[059](docs/roadmap/batches/059.md)。

- 2026-11-07 · 批次058：源码与原父项no-op，复用现有系统类注册/消息/生命周期；真实Rich后端不关闭完整Control-17或其他Control任务，见[058](docs/roadmap/batches/058.md)。

- 2026-11-07 · 批次057：源码no-op，必要TextLength原生读取/编辑元数据归TextBoxBase-002d/003h；不新增Control完成项或改变现有Paint调用语义，完整父任务保持，见[057](docs/roadmap/batches/057.md)。

- 2026-11-07 · 批次056：生产no-op，专用文本框点击链保留通用鼠标异常finally边界，不将有限负向命中/元数据检查计为完整Control输入或Designer完成，见[056](docs/roadmap/batches/056.md)。

- 2026-11-07 · 批次055：源码no-op，固定Control重建guard与DPI内部读取只为TextBox-004b提供依赖；不把新增两个系统度量helper计作完整DPI或Control完成，见[055](docs/roadmap/batches/055.md)。

- 2026-11-07 · 批次054：补标准DeviceDpi/独立内部缓存和真实窗口创建读取，为TextBoxBase-007c服务；保持完整Control/DPI父任务部分，不以初值/窗口读取代替缩放及跨屏事件验收，见[054](docs/roadmap/batches/054.md)。

- 2026-11-07 · 批次051：14c仅补TextBox词删除必要内部redraw依赖，默认Invalidate不强制子树；10例新增及5,119全量通过。Handle销毁后End必要重建和原异常按固定10.0.11保持，完整绘制/生命周期/设备及平台仍待，见[051](docs/roadmap/batches/051.md)。

- 2026-11-07 · 批次049：新增17e，原因由固定Control.ReflectParent/UpdateReflectParent与独立原生EDIT对照闭环，不再把实际GetParent等同通知接收者。首跑13例11通过，两项重挂通知失败保留；修复后新18与旧停车/行为8组合26/26、完整4,954/4,954。048最终停车测试“重挂即释放”的断言是历史实现假设，现改为原EDIT销毁时释放；048原失败/诊断及17d限定历史保留，旧2次事件硬期望未改。完整17父项仍部分，见[049](docs/roadmap/batches/049.md)。

- 2026-11-07 · 批次048：全量首轮4,930/4,931暴露旧ControlBehaviorTests的TextChanged=2变1；保留旧硬断言与失败回执，根因是裸STATIC停车窗吞WM_COMMAND。只补17d通知连接，不在AppendText手工补发事件、不改固定基类算法，也不新建完整context；标准ParkingWindow固定源码依据及最终证据见[048](docs/roadmap/batches/048.md)。

- 2026-11-07 · 批次048未完成范围：新重挂探针在Form.Show后实际父映射/同HWND/停车窗销毁及native ID=1001均通过，但后续EDIT修改文本不触发Form/child命令和TextChanged。两份有界诊断JSON/TRX保留048；原因未闭环，不以本批停车期间通知测试称重挂后兼容，继续Control-17/TextBoxBase-006，下一批优先核此依赖。

- 2026-11-07 · 批次046：新宿主测试暴露旧superclass缓存跨activation context混用EDIT的潜在缺陷。首轮全量6个旧RTL失败，隔离28项通过、与新测试组合再次6失败；直接回归读到源过程与缓存原过程不一致，修复前两例全失败。仅在NativeWindowClass当前源类解析与缓存键加入真实源身份，保留旧RTL硬断言；Control.cs不改，完整17及平台/线程/模态仍待，见[046](docs/roadmap/batches/046.md)。

- 2026-11-07 · 批次045：为TextBoxBase-005a增加标准内部WindowText扩展点，将现有Text setter的native提交与公开OnTextChanged分开；仅EDIT在native提交期间抑制反射通知，处理器内native编辑仍正常通知。Control公开Text判等/提交/通知规则保持；相关ControlText/编辑等892项与全量4,782项通过。首次两例夹具父Form未建窗被停车窗承载，补真实父HWND和托管映射断言后通过；不改变生产Parent/创建/反射管线，不重复新增Control完成任务，完整11/17与生命周期仍待，见[045](docs/roadmap/batches/045.md)。

- 2026-11-07 · 批次044：Control.cs源码no-op。TextBox对齐夹具定位既有RTL创建后UpdateStyles会重新提交USER32规范化过的样式；独立EDIT复现此管线，仅验证TextAlign创建样式消费。原38/42失败保留，完整运行时RTL/镜像视觉及11/17父项不关闭，见[044](docs/roadmap/batches/044.md)。

- 2026-11-07 · 批次042：生产Control及Control.Ime均no-op；核固定源码确认组合/SETCONTEXT/候选通知交原生默认过程，新增11项消息参数/结果、去重状态/默认异常及真实隐藏EDIT回传回归。沿12父项记录审计，不新增重复实现子项；物理IME/候选UI、模态与平台仍待，见[042](docs/roadmap/batches/042.md)。

- 2026-11-07 · 批次040：12m将039语言变化消息接入标准Form通知，并补请求取消与原始参数/默认结果保留；上下文更新、unsupported传播和默认过程先后遵循固定源码。只关闭明确共享消息子范围，模态/OLE/组合串/设备与平台仍待，见[040](docs/roadmap/batches/040.md)。

- 2026-11-07 · 批次039：12l接语言消息和非Form宿主传播，普通Form不冒充模态宿主。9项新增、50项组合及4,590全量通过；原始夹具失败保留，旧028无句柄焦点用例隔离并还原未初始化传播缓存，原默认/LostFocus/不建句柄断言保留。完整IME与设备仍待，见[039](docs/roadmap/batches/039.md)。

- 2026-11-07 · 批次038：12k按固定10.0.11补有效ImeMode与CJK IMM上下文首范围；保留037字符计数语义。新interop用LibraryImport，HIMC借用与自有寿命分开；完整输入法/派生/平台仍未关闭，见[038](docs/roadmap/batches/038.md)。

- 2026-11-07 · 批次037：按固定10.0.11补12j，7项IME消息回归及4,536全量通过。纠正12父项遗漏036已完成子范围的滞后；完整IME/context/候选窗/派生与平台不关闭，详见[037](docs/roadmap/batches/037.md)。

- 2026-11-07 · 批次035：12h接入真实消息循环，默认按钮消费复用内部验证结果；AutoSizeMode修正数值身份时将基础字段显式初始化GrowOnly，保留原命名默认。旧整数0/1及已编译常量须复核，不以新增队列用例关闭完整键盘/Control，见[035](docs/roadmap/batches/035.md)。

- 2026-11-07 · 批次036：12i按固定WinForms 10.0.11源码补原生WM_GETDLGCODE判别和WM_SYSCHAR分流；SuppressKeyPress在真实队列中有界移除字符消息，直接消息测试仍保持兼容；验证取消状态按父链读取，并在鼠标抬起后清理。新增5项输入测试、真实HWND按钮验证2项及既有键盘14项通过；完整IME、派生路由、设备和平台仍未验收，见[036](docs/roadmap/batches/036.md)。

- 2026-11-07 · 批次034：完成12g键事件实际消息管线首范围，修复033中WndProc绕过预览、Handled返回误判及默认助记键回父递归；保留033已完成预处理范围，完整IME/队列字符抑制/设备及命令对话框仍待，见[034](docs/roadmap/batches/034.md)。

- 2026-10-26 · 批次030：补Control-12e基础键消息/事件契约和5项Windows STA回归；4,397项全线测试通过，完整键盘预处理、IME、物理设备、Designer/AOT和跨平台仍未关闭，见[030](docs/roadmap/batches/030.md)。

- 2026-10-17 · 批次029：新增49项回归与有限12c/12d实现；最终16场景仅5场景JIT/AOT严格一致，11场景相差关闭LostFocus的33行；对参考保留关闭LostFocus和既有WM_QUIT差异。父子集合初次回归发现可见兄弟句柄未创建，恢复既有创建路径后重新检查；原始失败保留。修正Control-02遗漏028基础Select/CanSelect的历史表述；整Control/98%不关闭，见[029](docs/roadmap/batches/029.md)。

- 2026-10-16 · 批次028：补12a/12b及必要容器合作，54项新增核心/消费回归与全部4,343测试通过。固定参考30场景18同，JIT/AOT全部30同；11个额外WM_QUIT和1个SplitContainer继承投影差异原样保留。修复Form句柄恢复误激活，旧生命周期204行恢复严格全同。基础导航不等于自动转移、验证/键盘或物理设备验收；父项/完整Control不关闭，见[028](docs/roadmap/batches/028.md)。

- 2026-10-16 · 批次027：Control-13由全待办改部分，只关闭13a/13b明确代码范围；核心21例、消费25例通过。固定参考25场景20同/5差异，JIT/AOT24同/1异常文本差异；LockScreenBackstopFrame阻止10/20次命中，成功Click命中与物理输入待解锁后验证。Control-12已有Enter/Leave/On*和局部焦点接入，纠正全待办但不关闭完整焦点/键盘。生命周期204、消息绘制558行参考/JIT回归同；旧AOT边界保留，见[027](docs/roadmap/batches/027.md)。

- 2026-10-16 · 批次026：05c/08c有限范围实现，48项新回归与全部4,243测试通过。25标准场景16同/9增强差异，另7旧Task仅Lumina；32场景JIT/实际AOT全同。旧Action59行/ValueTask90行各两条关闭差异首次出现并保留strict失败，其余观测不变；历史004/005原全同证据不删除。修正当前“所有InvokeAsync关闭均悬挂”“原生错后托管清理必需第二次Dispose”的过时描述，明确Parent/child/Site再失败仍需重试，未关闭完整父项/平台或98%。见[026](docs/roadmap/batches/026.md)。

- 2026-10-16 · 批次025：完成普通Drain的完成信号失败收尾，以及Control/Form/同步上下文的native callback与`WM_NCDESTROY`双错边界。固定参考/JIT/AOT各8场景全部正常退出；4项真实差异保留，未将增强行为归一化。完整Control、InvokeAsync关闭、Dispose后续托管事务、输入/Designer/资源、Linux/macOS仍未关闭，见[025](docs/roadmap/batches/025.md)。

- 2026-10-16 · 批次024：完成04b/14b，关闭失败仍遍历余队列并销毁实际HWND，明确偏离10.0.11的收尾行为。补基础背景图像和真实屏幕透明/alpha证据，Form取消默认COMPOSITED后修复唯一意外失效矩形差异。更新“背景/透明全缺”及“Close必被首个wait打断”的当前描述；普通Drain、Dispose后续清理和native回调/清理双错仍有缺口，父项/历史失败/固定分母不变，见[024](docs/roadmap/batches/024.md)。

- 2026-10-16 · 批次023：14a/17a补标准消息、样式和基础绘制，内部HandleWindowMessage收进标准虚入口，系统原生控件显式保留原生绘制。修复UpdateStyles后的客户区缓存、重建文本和公共OnPrint状态；Form默认绘制接共享入口，最外层销毁清理不依赖派生调用base。022基础Control字体差异由实际UserPaint类解决，保留默认palette差异与历史失败。AOT自定义Param/GetLParam动态封送、透明可见合成、完整绘制/输入/Designer和平台仍缺；仅明确子范围代码完成，详见[023](docs/roadmap/batches/023.md)。

- 2026-10-16 · 批次022：完成11b基础外观父链与08b借用Font/独占HFONT明确范围，移除TextBox隐藏Font所有权；父重挂/Reset/通知/FontHeight/布局缓存与内部theme局部值分离。固定参考仍记录基础Control原生字体和默认palette前景色差异，完整Styles/UserPaint由14接续；不以托管一致关闭原生绘制。历史Control-14从全待办纠正为已有Invalidate/内部窗口过程的部分状态，全部未完成员与原ID保留。详细运行、首次失败和剩余边界见[022](docs/roadmap/batches/022.md)。

- 2026-10-16 · 批次021：09d/10c完成有限基础几何/引擎/缓存。固定10.0.11默认AnchorLayoutV2=false，另进程显式true覆盖；不把V2误当默认。Dock↔Anchor内部切换避免多余DockChanged，真实几何提交保留标准事件；移除旧Form菜单高度二次偏移，native矩形与托管一致。13核心+11容器新测试及全部4,107项通过，512行JIT/实际AOT同参考；020七行差异已修，旧失败不覆盖。Control-17从全待办纠正为已有Win32部分实现，未把它或09/10父项关闭；完整边界见021。

- 2026-10-16 · 批次020：新增标准Layout参数/委托和非virtual公共布局入口，属性/集合/尺寸变更进入同一事务；OnLayout为标准派生扩展。历史Lumina覆盖virtual SuspendLayout/ResumeLayout/PerformLayout的消费者须按标准改为OnLayout(LayoutEventArgs)，这是对WinForms表面的兼容修正。byte暂停计数、缓存消费/重入/原异常有对照，不制造Dock缺失的Bounds事件。10父项及09/LayoutEngine/PreferredSize/InitLayout完整语义继续部分；56项新增回归与全部4,083项通过，394行JIT/AOT严格相同，参考7行几何差异保留，不宣称完整Control或98%。

- 2026-10-16 · 批次019：补07a标准句柄入口、事件、状态与映射。历史内部读Handle的调用改用NativeHandle，避免普通属性/销毁路径意外建窗；保留旧无参OnHandleCreated扩展。WM_SHOWWINDOW接入标准可见树与通知，018的391行父子集合参考/JIT差异已清零，历史失败不改写。重建期间保留dispatcher、子窗停放和重挂，普通销毁仍终止待执行项；Dispose覆盖异常/跳过base按固定参考保留对象/窗口状态。07父项保留全部派生恢复、资源与竞态；Form历史Created事件隐藏标准属性仍明确披露。全Control、Windows完整验收和GTK4/AppKit未关闭，详见[019](docs/roadmap/batches/019.md)。

- 2026-10-16 · 批次018：新增标准嵌套集合和Parent setter/含parent构造，共用所有控件父子树；Form进入标准继承后复用同一状态。Windows通过SetParent/停放窗口保留重挂HWND、恢复Z序；级联Dispose先销毁原生树再解除Parent，释放后重挂再次Dispose仍清理当前树。数组复制保留接口存储的逐元素转换与标准异常；对Find及释放通知失败补最小固定参考。同步集合布局暴露的ToolStrip重入由库内修复。历史“Parent/Controls/Form完全未实现”已改为明确首范围，Control-01/03/06/08/09/11及FND父项仍有剩余，不新增整控件或98%声明。最终记录与已知差异见[018](docs/roadmap/batches/018.md)。

- 2026-10-16 · 批次017：补ResetText标准虚槽、Text/TextChanged英文属性及OnTextChanged的Advanced，移除Text setter的释放后拒绝；维持虚getter判等、存值后通知和016构造顺序。固定参考/JIT 3,774条相同，实际AOT行为及其余元数据相同；内置COM关闭时CoreLib的RemoveAttributeInstances主动删除DispId=-517，保留原始差异，不改库AOT策略或全局根化。首轮一项新测试把setter退出回调中的嵌套Reset尾记录预期写错，已按参考改为完整16步断言并通过；219项新测试全部通过。父任务11保持部分完成；已有HWND/native、Parent/Form、布局、中文Designer和动态属性转换未关闭，见[017](docs/roadmap/batches/017.md)。

- 2026-10-16 · 批次016历史回标：任务11补构造必需的Text子范围为部分完成并引用01c，重置/完整运行期与native/继承仍缺；02b/c、09a、10a的后续基础构造/MinMax/客户区/通知/换算改为交叉引用已完成子任务，保留真实布局/派生/外部通知缺口。原批次快照和ID不变，不新增验收。

- 2026-10-16 · 批次 016：新增 Control(string? text) 与五参文本/几何构造，默认初始化和 context 安装后经虚 Text，再分别设置 Location、Size；约束可在 Location 阶段改变尺寸并发通知。Text 允许 null 且为标准 virtual；虚 getter 判等时保留原存储，不被空写覆盖。4,584 条固定参考/JIT/实际 AOT 指定观测相同，311 项新增回归，覆盖 null/Unicode/NUL、极值几何、隐藏/覆盖/不调 base/重入/原异常和 context 时序。初次有效空值差异已按参考修正；构造 NullableContext 放置按有效契约比较，CoreCLR 的 Standard|HasThis 与 Native AOT 的 HasThis 原始编码单列 stderr，只比较实例/变参等实际契约。精确 #ctor/Text 根化仅在探针，AOT EXE 2,075,648 字节、无 CLR Header/IL2xxx/IL3xxx。含 parent 构造、完整 Text/native/重置/继承、布局与 Form 仍缺，见 [016](docs/roadmap/batches/016.md)。

- 2026-10-16 · 批次 015：基础无参构造由固定 120×24 改为标准初始 0×0，新增 protected virtual DefaultSize。构造按参考重复读取非空默认 Min/Max 并调用公共虚 setter，再直接提交 DefaultSize 的宽高；双轴非零才计算边框客户区，否则保留构造重入留下的缓存，位置不被默认尺寸覆盖。默认扩展原异常发生时 context 尚未安装，成功后才安装、派生构造体可见。2,677 条固定 10.0.11/JIT/实际 AOT 全同，387 项新增回归；25 个旧差分驱动和全线 2,713 项测试通过，无旧测试或派生构造修改。AOT EXE 2,020,864 字节，无 CLR Header/IL2xxx/IL3xxx。已有派生构造显式尺寸保留；尚未自设默认尺寸的具体控件现在继承基础 0×0，其专属默认值仍待后续。其余公共构造、Parent/Controls、完整布局/DPI/Form 与全语言元数据另列，见 [015](docs/roadmap/batches/015.md)。

- `70a08e7`（2026-04-09）：补 ResumeLayout/IsDisposed 等设计器兼容表面。
- `2fb513a`（2026-04-09）：加入异步调用相关实现。
- `9d5165f`（2026-04-09）：扩展基础属性与控件功能。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。
- 2026-10-15 · 批次 002：Control 接入 Component/IWin32Window 并可实例化；新增标准 Disposing，资源清理幂等且重复 Dispose 继续触发 Disposed；终结器分支不遍历托管子树。新增 9 项纯托管测试及 1 项基础 HWND 测试通过，`Test-ControlComponent.ps1` 的 5 项观测与真实 WinForms 10.0.11 一致。Form 继承链、UI 子控件级联、构造族、UI 投递及复杂设备/设计器验收保留。
- 2026-10-16 · 批次 003：接入真实窗口队列、ISynchronizeInvoke 和 Action/Delegate/args/Func 的同步调用、BeginInvoke/EndInvoke。对照无句柄、UI/工作线程、父级回退、FIFO、ExecutionContext、特殊委托参数及释放唤醒；69 条三端观测相同。已有 HWND 的 null Delegate 在参考端会挂起，本实现以 ArgumentNullException 明确拒绝；无 HWND 的 null 仍按参考端抛 InvalidOperationException。异步异常报 ThreadException；无订阅者仅显示原生 OK 提示继续，完整标准异常对话框/模式和广泛关闭竞态待后续。
- 2026-10-16 · 批次 004：Action/Func<T> 的标准 InvokeAsync 与取消及既有两条直接调用路径统一复用真实队列；59 条固定参考/JIT/AOT 观测相同，旧 Task 路径另检初始 UI 线程、等待内层 Task、原异常身份与取消。按参考端保留 null 校验先于句柄、无自身句柄返回 Faulted Task、预取消成功并跳过回调（Func 返回 default）、排队后/回调内取消及原 token；任务异常不报告 ThreadException。Dispose 前已排队而未执行的 Task 等待后续 token 取消，与参考端相同；无 token 的关闭/重建/退出竞态、ValueTask 与标准同步上下文仍待后续。

- 2026-10-16 · 批次 005：Control/Form 补标准 ValueTask 两族并共用真实 HWND 队列；IValueTaskSource 同步/延迟/取消后单次消费，原异常（含 wrapper/OCE 身份）与结果/token 保留。固定 WinForms 10.0.11、JIT、实际 Native AOT 的 90 条观测相同；旧 Task 虚方法保留，用 OverloadResolutionPriority(-1) 使未强转 async token lambda 选择标准 ValueTask，显式 Task 委托和派生 override 专项通过。预取消/无自身句柄/释放后边界按独立参考观测保留；同步上下文/await 后 UI 恢复及关闭竞态未关闭。

- 2026-10-16 · 批次 007：Control 构造接入默认 context 安装，保留已有 custom Current 与禁用策略；dispatcher 将目的 UI Current 用于回调的 ExecutionContext 并 finally 还原。默认直接/BeginInvoke/ValueTask/带结果/工作线程投递 await UI 恢复有固定三端对照，旧 Task 专项保留；完整构造、继承、Controls/Parent 与竞态不关闭，见 [批次 007](docs/roadmap/batches/007.md)。

- 2026-10-16 · 批次 008：先固定 WinForms 10.0.11 参考，补标准 Focus 返回及三个查询属性和 Advanced/Browsable/Hidden 元数据。真实 HWND 程序切换、重复请求、外部原生切换/清空、原生 descendant、隐藏/禁用及释放后均按原生状态返回；不因查询或无自身句柄 Focus 创建窗口，也不回退 owner HWND。157 条参考/JIT/实际 AOT 相同，新增 17 项测试；未修改同步上下文/投递与现有 setter。禁用原生父窗口时 CanFocus 为 true 而实际 Focus 返回 false 的差异也按固定参考保留。Select、键盘/验证、Form 继承与广泛重建竞态仍待后续，见 [批次 008](docs/roadmap/batches/008.md)。

- 2026-10-16 · 批次 009：先固定参考再改尺寸链。未创建自身 HWND 时 WinForms 保留负尺寸，不能统一钳制到零；已有 HWND 时负数归零且零宽高真实应用到 MoveWindow。首次创建保留原始参数，负尺寸通过 GetWindowRect 回读；int.MinValue 宽度按 USER32 的 CW_USEDEFAULT 结果得到 0×0，HandleCreated 前完成缓存更新。已有 GetNativeHeight 的正逻辑高度扩展不写回为下拉区高度。756 条固定参考/JIT/实际 AOT 相同，新增 33 项独立 STA/原生矩形回归，AOT EXE 2,562,048 字节、无 CLR Header 和 IL2xxx/IL3xxx。构造默认、全部派生限制、Form.ScaleSize、完整布局/事件与 Min/Max 未关闭，见 [批次 009](docs/roadmap/batches/009.md)。

- 2026-10-16 · 批次 010：先固定 WinForms 10.0.11，再实现 BoundsSpecified 的八个标准值/Flags、x/y 命名的两族 SetBounds 和 protected virtual SetBoundsCore。外层按已知位保留其他分量，未知位原样交给 core，有效矩形未变不调用；core 本身接受完整矩形，包括派生调整的未指定分量和 specified=None，重复直接调用仍可被覆盖观察。七个位置/尺寸属性按对应标志进入同一路径，派生可拒绝、调整或抛异常。2,034 条固定参考/JIT/实际 AOT 完全相同，60 项新增回归通过；AOT EXE 2,576,896 字节，无 IL2xxx/IL3xxx。保留旧零/负尺寸、首次创建回读、GetNativeHeight 与 OnBoundsChanged，完整布局/Min/Max/事件和派生约束继续另列，见 [批次 010](docs/roadmap/batches/010.md)。

- 2026-10-16 · 批次 011：先运行固定 WinForms 10.0.11 参考，再接入 LocationChanged/Move、SizeChanged/Resize 与四个标准 protected virtual On*。整矩形先提交，两族变化先快照，保持 Move→LocationChanged→Resize→SizeChanged 的顺序及重入后外层待发通知。OnLocationChanged 向 OnMove 透传参数，OnSizeChanged 仅向 OnResize 传 EventArgs.Empty，SizeChanged 仍用原参数；覆盖不调 base 按标准抑制对应通知链。无自身 HWND setter 和直接 On* 传播原异常；有 HWND setter 转交既有 Application.ThreadException，默认异常 UI/模式仍按既有缺口披露。旧 ApplyBounds→OnBoundsChanged 布局衔接保留，标准通知独立于该旧 hook。2,280 条固定参考/JIT/实际 AOT 相同，99 项新增测试；通知 AOT EXE 2,618,880 字节，无 CLR Header 或 IL2xxx/IL3xxx。客户区、外部原生自主通知、完整布局、构造默认/全部派生约束与 Form 生命周期未关闭，见 [批次 011](docs/roadmap/batches/011.md)。

- 2026-10-16 · 批次 012：先固定参考，再补基础客户区与标准元数据。四有限边框/无有 HWND/11 入口/重复/零负恢复/覆盖重入异常及缓存异常态后仅位置更新共 5,556 条固定 WinForms 10.0.11/JIT/实际 AOT 全同，80 项新增回归通过。外框先提交，随后同步客户区缓存，位置/尺寸通知与客户区通知按参考顺序触发；缓存变化也算尺寸变化。SetClientSizeCore 正常执行至末尾时提交原始客户区请求并调用 OnClientSizeChanged（不检查是否同值，事件异常同步传播），故同值仍通知，raw 负值/重入可使缓存不同于原生客户区；不能简单实时 native getter 替换。GetClientRect/AdjustWindowRectEx 失败真实抛错，旧 OnBoundsChanged/GetNativeHeight 保留。客户区 AOT EXE 2,592,256 字节，无 CLR Header 或 IL2xxx/IL3xxx；探针的五个反射成员精确保留不改变库裁剪策略。SizeFromClientSize、构造默认、全部派生约束/外部自主通知/完整布局/Form 生命周期继续另列，见 [批次 012](docs/roadmap/batches/012.md)。
- 2026-10-16 · 批次 013：先取得固定参考，再新增标准 SizeFromClientSize 虚方法，复用真实非客户区计算并保留 unchecked 溢出。四有限边框/无有 HWND/极值、直接/base 绕过/覆盖不调 base/返回调整、重入和原异常共 10,086 条固定参考/JIT/实际 AOT 全同，257 项新增独立 STA/元数据/原生矩形回归通过。基础 ClientSize/SetClientSizeCore 不调用该虚扩展，原路由保留；直接 base 换算不创建 HWND，不改变外框/客户区缓存或发通知，负值和重入形成的缓存差异也不被读取操作刷新。探针仅精确保留实际反射的两个方法；构造默认、尺寸约束、全部派生/外部自主通知/完整布局/DPI/Form 生命周期仍另列，见 [批次 013](docs/roadmap/batches/013.md)。

- 2026-10-16 · 批次 014：明确初始化后的基础 Min/Max 运行期约束及默认虚扩展完成，33,408 条固定 10.0.11/JIT/实际 AOT 相同，新增 394 项回归；冲突保留属性值、负值/零轴、Empty 取消差异、getter/default/core 调用顺序与缓存/通知均按参考。core 原始相等先返回，约束后无 HWND 的重复矩形仍可能刷新客户区。AOT 仅精确保留六个 accessor，AmbientValue 构造元数据用 CustomAttributeData 核对，动态 Value/TypeDescriptor 转换初次实测失败仍单列；英文元数据相同不代表中文分类/描述本地化。构造期 DefaultMin/Max 在 DefaultSize 之前的状态已有独立参考，但实现统一留批次 015 的 01b。全部派生/完整布局/DPI/外部自主通知/Form 保持缺口，见 [014](docs/roadmap/batches/014.md)。

#### docs/roadmap/controls/ContainerControl.md

- 2026-11-07 · 批次039：ContainerControl.cs未改，继承Control-12l的有限语言消息/非Form根传播；未完成模态宿主、完整输入语言/IME和全部派生验证，父任务状态保留，见[039](docs/roadmap/batches/039.md)。

- 2026-11-07 · 批次038：ContainerControl.cs本批no-op；UserControl标准AutoSize声明继续消费共享布局，固定源码无UserControl独立算法。Control有限ImeMode继承/上下文不关闭完整容器输入/验证/缩放与平台，见[038](docs/roadmap/batches/038.md)。

- 2026-11-07 · 批次037：UserControl派生新增原生焦点入口，7项真实HWND回归验证共享活动链与Prevent/Allow/CausesValidation、Tab正反导航。ContainerControl.cs本批no-op；有限派生消费不关闭完整03/04/06/07，见[037](docs/roadmap/batches/037.md)。

- 2026-11-07 · 批次035：06d按固定10.0.11调用点通知Form默认按钮，基类空扩展点是标准派生契约，不代表验证成功；实际按钮激活仍消费真实验证结果。嵌套活动链/三种验证策略和队列Enter/Escape有回归，完整容器与点击输入不关闭，见[035](docs/roadmap/batches/035.md)。

- 2026-11-07 · 批次036：Control共享验证状态改为父链读取并在鼠标抬起后清理，真实HWND Prevent/Allow按钮回归覆盖取消不点击与允许点击；未扩展容器的完整原生提交、BindingContext、设备和平台，见[036](docs/roadmap/batches/036.md)。

- 2026-11-07 · 批次034：完成06c嵌套Tab不回绕、修饰键过滤及失败父链回退；纠正06父项遗漏033已完成子范围的描述。普通后代助记键终止依赖Control-12g默认false修复；完整输入/验证/平台仍待，见[034](docs/roadmap/batches/034.md)。

- 2026-10-26 · 批次031：补05a AutoScaleFactor和06a ParentForm的明确代码子范围；完整嵌套缩放、绑定、键盘和平台仍待，见[031](docs/roadmap/batches/031.md)。

- 2026-10-17 · 批次029：完成02b/03a有限子范围，将03/07由全待办改部分；移除后恢复可见树创建以维持旧父子集合行为。验证状态机按固定10.0.11源码适配，未将有限验证等同全容器或全部派生兼容，见[029](docs/roadmap/batches/029.md)。

- 2026-10-16 · 批次028：关闭02a限定逻辑链与程序焦点恢复子范围，04由全待办改部分，08补实际路由证据。公开/接口差异、嵌套重入、错误报告和TextBox去重有26项消费回归；未改AutoScale算法，自动候选、验证/Tab及完整容器仍待办，见[028](docs/roadmap/batches/028.md)。

- 2026-10-16 · 批次021：补GetPreferredSizeCore经真实LayoutEngine测量内容、边框/Padding和必要缓存，属于Control-09d的容器依赖；未改现有AutoScale算法，未以基础测量关闭完整容器/验证。

- 2026-10-16 · 批次020：随ScrollableControl移除重复布局状态，Form继承统一标准Layout；未修改现有AutoScale算法，也未完成验证/滚动/完整布局。01既有继承完成范围保留，其他父项不随基类完成关闭。

- 2026-10-16 · 批次018：新增标准类与IContainerControl，迁入Form缩放；按固定参考区分公开setter与显式接口、嵌套选择与释放。将历史“同名类不存在”和“Form独立IDisposable”更新为当前事实；02/05/08保持部分，其余验证/键盘/平台未关闭，见[018](docs/roadmap/batches/018.md)。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

#### docs/roadmap/controls/FlowLayoutPanel.md

- 2026-11-07 · 批次054：跟随Table全量失败审计同源子项AutoSize消费者，改用内部状态；原完整算法、FlowBreak/Settings/RTL和平台任务不关闭，见[054](docs/roadmap/batches/054.md)。

- 2026-10-16 · 批次021：专用引擎驱动既有flow排列及有限内容测量，避免布局覆盖指定尺寸；方向/换行属性清缓存并请求真实布局。简单两子项58×21测量/AutoSize与换向缓存独立回归通过；仅接入/有限测量，完整标准flow算法不在此范围。 见[021](docs/roadmap/batches/021.md)，父任务仍部分；020历史条带定位/无首选测量描述只代表当时状态。

- 2026-10-16 · 批次020：标准事件先于既有flow算法，原PerformLayout算法迁入标准OnLayout。原四方向/换行/Margin/Padding局部算法保留；FlowBreak/标准Settings/首选测量/完整算法继续待办。 见[020](docs/roadmap/batches/020.md)；父任务保持部分完成。

- `3323e20`（2026-03-31）：改进 flow 方向与布局交互。
- `52f5662`（2026-03-31）：加入基础布局控件。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

#### docs/roadmap/controls/Form.md

- 2026-11-07 · 批次069：069在Control-17g必要范围将Form的WM_NOTIFY按来源HWND转发并回传Rich保护通知0/1，原内部布尔入口保留，不新增Form完成项。活原Form重挂/冲突ID/未映射源有定向回归；原父Form销毁重建寿命、完整线程/模态/反射和平台仍待。064FAIL/065超时保持，见[069](docs/roadmap/batches/069.md)；下述为历史。

- 2026-11-07 · 批次049：非零lParam的控件WM_COMMAND先按来源HWND反射，修复重挂后旧Form字典不再拥有控件/ID已重用的通知缺口；OnCommand扩展仍由收到原生命令的Form调用，菜单限零lParam。8项新重挂回归及4,954全量通过，完整16/原反射父寿命仍部分，范围交叉引用Control-17e，见[049](docs/roadmap/batches/049.md)。

- 2026-11-07 · 批次042：只读核模态前置，Form.cs no-op。固定Control.Ime的模态失焦恢复依赖真实Modal，当前ShowDialog/owner禁用恢复和模态循环仍缺；不新增假状态或把普通嵌套循环测试算作模态完成。Form-03/12及Application/ApplicationContext依赖保留，见[042](docs/roadmap/batches/042.md)。

- 2026-11-07 · 批次040：12d补两组标准输入语言事件与参数/委托，保留组件事件槽、虚派发、订退、原异常和请求取消。Control共享路径先更新IME再向FindForm通知，不另造Form专用输入管线；完整模态/IME/OLE/Designer及平台仍待，见[040](docs/roadmap/batches/040.md)。

- 2026-11-07 · 批次039：Form.cs未改；普通Form根不走非Form宿主传播恢复，Form语言事件/取消、InputLanguage服务及模态传播仍缺，不把Control-12l消息接入算作完整Form输入或模态实现，见[039](docs/roadmap/batches/039.md)。

- 2026-11-07 · 批次038：Form.cs本批no-op，继承Control有限ImeMode/IMM路径；共享销毁通知新增的IME清理与原通知双错均保留。TextBox多行重建和UserControl声明不关闭Form输入/模态/生命周期，完整派生IME和设备仍待，见[038](docs/roadmap/batches/038.md)。

- 2026-11-07 · 批次037：TextBox多行Enter/AcceptsReturn通过现有Form.AcceptButton路由，UserControl嵌套活动链消费现有Form验证策略，Form.cs本批no-op。完整IME/模态/生命周期/平台仍待，见[037](docs/roadmap/batches/037.md)。

- 2026-11-07 · 批次035：12b实现标准默认/取消路由和结果消费；保留固定源码CancelButton重复赋值的None→Cancel及非Control接口Enter消费语义，不自造所有权限制。首轮编译发现当前Keys无Return别名，改用同值Enter，别名仍归附属契约缺口；模态/Designer/AOT仍待，见[035](docs/roadmap/batches/035.md)。

- 2026-11-07 · 批次036：12c按固定WinForms Dispose顺序清理默认/接受/取消按钮引用并撤销默认通知；Dispose(false)保持托管状态，未扩大为移除/隐藏/禁用自动清空。2项生命周期回归通过，模态、完整关闭与平台仍待，见[036](docs/roadmap/batches/036.md)。

- 2026-11-07 · 批次034：完成12a KeyPreview父链消费、可修改字符和顶层Tab正反回绕；默认隐藏窗体不可选择，测试改为真实可见HWND后通过，不以程序导航冒充物理键盘。完整默认/取消按钮、IME、Designer及AOT仍待，见[034](docs/roadmap/batches/034.md)。

- 2026-10-17 · 批次029：完成07a有限WM_CLOSE验证前置；验证取消不会跳过FormClosing处理器，无句柄关闭不验证。关闭LostFocus是本批记录的未定位差异，与既有WM_QUIT分别留待办，完整Form不关闭，见[029](docs/roadmap/batches/029.md)。

- 2026-10-16 · 批次028：完成08a限定激活/选择链；初次接入WM_ACTIVATE暴露外部销毁后Handle读取提前创建子项的回归，依据固定参考改SW_SHOWNOACTIVATE，保留原测试期望并恢复旧204行全同。补两项延迟恢复测试；原Form-08/12全待办遗漏既有Activate/Focused/ActiveControl已纠正为部分。额外WM_QUIT记录为未完成，完整Form/模态/Designer不关闭，见[028](docs/roadmap/batches/028.md)。

- 2026-10-16 · 批次027：Form.cs未改；既有标准消息路径继承027坐标/捕获/鼠标基础，真实Form父子与订阅/坐标回归通过。Demo程序BM_CLICK与WM_CLOSE证据单列，锁屏下成功命中及物理输入未验收；Form-12/完整容器导航、模态/Designer保持未完，见[027](docs/roadmap/batches/027.md)。

- 2026-10-16 · 批次026：继承05c/08c的真实Form队列关闭/重建、异步工作与托管子树/Site清理通过独立回归；未改Form.cs，不据基类完成关闭完整Form。按实际代码纠正Form-07仍全待办的历史状态：Close、WM_CLOSE、FormClosing.Cancel和Dispose已存在，父项改部分并保留所有剩余关闭原因/标准事件/资源事务。Demo AOT真实显示/复选框/正常退出/原始HWND失效通过，完整Designer/模态仍未验收，见[026](docs/roadmap/batches/026.md)。

- 2026-10-16 · 批次025：Form-16c补齐外部`WM_NCDESTROY` callback与清理同时失败时的异常边界和资源收尾；callback、清理与窗口过程错误按独立顺序报告。外部销毁的待执行 callback 在Lumina中随HWND销毁而得到`ObjectDisposedException`，该与固定WinForms参考的真实差异保留；完整Form/模态/Designer仍未关闭，见[025](docs/roadmap/batches/025.md)。

- 2026-10-16 · 批次024：独立诊断先仅清除真实Form的COMPOSITED位即恢复父失效矩形，随后修正默认样式；不伪造失效区域，也不扩大矩形。继承Control背景图像/透明绘制及04b关闭收尾，16b仅为必要衔接；外部NCDESTROY回调与清理同时失败可能覆盖原异常，完整Form未关闭，见[024](docs/roadmap/batches/024.md)。

- 2026-10-16 · 批次023：标准CreateParams/Message/WndProc接入Control共享入口，早期消息绑定和销毁清理移入原生callback异常边界；修复Form绘制绕过Control及派生截获NCDESTROY漏清理。06a历史限定范围保留，16a为本批必要衔接，完整Form/资源/模态/Designer仍未关闭，见[023](docs/roadmap/batches/023.md)。

- 2026-10-16 · 批次022：移除Form共享UiFontHandle，创建/销毁复用Control-08b独占HFONT，避免重建失败时停放子窗口引用已释放父字体。有效颜色与局部/theme默认分开，显式默认色与Reset即便不发属性事件也更新palette画刷。补实际资源寿命回归，完整Font/DPI缩放仍缺；纠正Form-11遗漏018继承缩放、Form-16遗漏已有Win32实现的全待办状态。完整资源、模态、Designer及平台验收不关闭，见[022](docs/roadmap/batches/022.md)。

- 2026-10-16 · 批次021：移除直属条带专用定位及旧GetClientContentTopInset，由标准父级DefaultLayout统一布局；ContainerControl首选测量供Form消费。更新020历史定位描述以免把旧算法当当前行为；Created事件仍隐藏Control.Created，完整模态/资源/设计器未关闭。

- 2026-10-16 · 批次020：标准Layout移到Control并统一事务入口，保留直接子条带定位、移除WM_SIZE重复布局。历史Form.Layout的EventHandler类型和protected无参OnLayout不再保留，消费者改用LayoutEventHandler和OnLayout(LayoutEventArgs)；仓库无旧覆盖消费者，固定Demo源码未变。Form-02/10按实际部分回标，10a为完成的必要衔接；完整Form/几何/Designer不关闭。

- 2026-10-16 · 批次019：Form覆盖标准protected CreateHandle并复用Control映射/事件/dispatcher。创建恢复真实ShowWindow时仅抑制内部恢复消息的重复托管通知，外部销毁后Handle重读保持Created=false及子窗未创建；直接销毁/重建不再冒充Close。Shown经UI队列触发；历史Created事件为兼容保留并明确其隐藏属性的差异。Form-06由全部未开始纠正为部分完成，06a为本批明确子范围；Form-02/03/07及完整窗口/设计器/资源/跨平台不关闭，见[019](docs/roadmap/batches/019.md)。

- 2026-10-16 · 批次018：Form由独立IDisposable类改为标准ContainerControl/ScrollableControl/Control/Component链，删除重复的属性、句柄/投递、集合和释放状态；保留真实顶层HWND、主题、菜单及原始Demo源码。原生移动/尺寸写回共享Bounds/ClientSize；Dispose清活动控件，销毁已显示窗口时静默清可见态。默认Text/Size/TabStop/Visible按参考校准，历史“Form独立状态/不存在标准链”回标。完整Form事件委托/模态/默认/布局/缩放/Designer/平台仍缺，见[018](docs/roadmap/batches/018.md)。

- `b83f534`（2026-04-09）：加入 Form.Invoke/BeginInvoke，当前实现仍需修正线程语义。
- `70a08e7`（2026-04-09）：增强布局/释放状态兼容。
- `2fb513a`（2026-04-09）：异步调用和布局改进涉及窗体。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。
- 2026-10-15 · 批次 001：在首个原生窗口创建前通知 Application 的永久 DPI 设置门禁，覆盖原生建窗回调期间的晚调用；未改变继承链、布局或关闭行为。实际建窗/DPI 交互验收后置。
- 2026-10-16 · 批次 003：真实顶层 HWND 创建时建立库内投递队列，WindowProc 分发注册消息、Dispose/WM_NCDESTROY 结束队列；同步/异步 Delegate、args、Action 和同步 Func 重载及接口已接入。指定线程探针在 WinForms/JIT/AOT 三端相同；Form 仍未继承 Control，InvokeAsync、所有关闭竞态和完整生命周期另列待办。
- 2026-10-16 · 批次 004：因标准继承链尚缺，在 Form 增加同名 Action/Func<T> 的 InvokeAsync(CancellationToken) 入口，复用 WindowThreadDispatcher；UI/工作线程及结果/释放后的指定观测纳入 59 条三端对照。没有改变 Form 继承链；ValueTask、标准同步上下文和完整关闭/生命周期仍保留。

- 2026-10-16 · 批次 005：增加标准 Func<CancellationToken,ValueTask> / ValueTask<T> 同名入口，复用 WindowThreadDispatcher；UI 首次调用、返回值与 async token lambda 纳入 90 条参考/JIT/AOT 观测。未改变独立 Form 的继承链；同步上下文与 callback 内 await 后 UI 恢复、全部关闭竞态继续待后续。

- 2026-10-16 · 批次 007：Form 构造接入默认 context，现有 Application.Run 在 Show 前安装并于最外层 finally 还原；默认 await/工作线程投递有对照。Form 仍独立于 Control，最后窗体退出判断仍使用进程全局 OpenForms 计数，多 UI 线程与 ApplicationContext 退出语义继续缺失；不把当前子范围写成完整 Form 生命周期兼容，见 [批次 007](docs/roadmap/batches/007.md)。

#### docs/roadmap/controls/GroupBox.md

- 2026-10-16 · 批次023：显式保留UserPaint=false及BUTTON原生字体路径，既有HandleWindowMessage内主题补绘继续保留。此处是共享Control新绘制入口的必要衔接，不代表GroupBox样式/全部Paint事件语义已同WinForms，07/08父项仍部分。

- 2026-10-16 · 批次022：完成07a有效字体/显式颜色消费，标题度量不再固定系统字体且不吞字体异常。按实际代码纠正01/02/03/08中遗漏标准Controls、已有默认/度量和Win32绘制的全待办状态；标准直接继承、全部默认/首选布局/焦点/打印/Designer与平台保持未完，见[022](docs/roadmap/batches/022.md)。

- `cb090bc`（2026-04-08）：记事本与 UI 改进提交涉及本文件。
- `7d7a67e`（2026-04-08）：Lumina.Forms 初始实现整合涉及本文件。
- `bca1ffd`（2026-04-08）：修正 GroupBox/TabControl 主题应用。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

#### docs/roadmap/controls/ListViewItem.md

- 2026-10-16 · 批次017：新增标准非虚ImageIndex/ImageKey及范围内英文元数据，独立保存未挂接图像选择，严格先验证再改状态；合法索引与key互斥，null key归空，等值字符串保留新引用。518条固定10.0.11参考/JIT相同、363项新测试通过；覆盖新建/空表/清空/替换/共享首项、三枚举/各view key缓存、Name/Tag/Tooltip/ToString、隐藏/毒丸与借用Font。原始设计器元数据单独记录：NoneExcludedImageIndexConverter、ImageKeyConverter和ImageIndexEditor未实现；参考这两属性没有RelatedImageListAttribute，不能把它记成当前不匹配。未计新范围实际AOT运行验收。父任务04/FND-003保持部分，图像构造、ImageList/native/Group/完整所属/Clone/序列化与资源仍缺，见[017](docs/roadmap/batches/017.md)。

- 2026-10-16 · 批次 016：BackColor/ForeColor/Font 改为当前首子项代理，空表 getter 不恢复、setter 对 Empty/null 也经既有 SubItems 换表恢复；移除/替换/ResetStyle 真实改变 item 的读取。默认 Window/WindowText，主项和子项共享同一惰性 MessageBoxFont；借用或已释放 Font 保持身份，不克隆/释放。UseItemStyleForSubItems 默认 true，未挂接时独立保存且不修改直接子项样式。541 条固定 en-US 参考/JIT 全同，328 项新增回归，三枚举/共享 view/独立 key 缓存、Name/Tag/Tooltip/两级 ToString 与既有样式构造保持；全线 3,352 项及 27 个旧驱动通过。标准非虚属性及英文元数据有证据；新范围未计实际 AOT 运行验收，已挂接继承/native/全局字体/完整所属/Clone/序列化/Designer 资源仍缺，见 [016](docs/roadmap/batches/016.md)。

- 2026-10-16 · 批次 015：新增标准 public override string ToString()，覆盖 Object 槽并使用主项原样 Text，格式为 ListViewItem: {Text}。96 条固定 en-US 参考/JIT 全同，98 项新增回归，覆盖空/null/Unicode/换行/NUL、元数据/样式变化、派生隐藏/override/原异常、首项清空/移除/替换/重复/跨 owner、三枚举与各 view 独立 key 缓存。读取不恢复首项/换表，不调用 Tag.ToString 或消费/释放借用 Font；Name 代理与独立 Tag/Tooltip 保持。全线 2,713 项及 25 个旧差分驱动通过，新范围未计实际 AOT 运行验收；未挂接完整样式、所属/native/Clone/序列化/设计器和压力另列，见 [015](docs/roadmap/batches/015.md)。

- `b8a98eb` · 2026-04-09：加入 ListViewItem 与基础子项模型；当时的初始化次序缺陷已由批次001修复基础范围，原提交历史保留，其余构造族仍待实现。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。
- 2026-10-15 · 批次 001：修复 string 构造空引用、首子项初始化及 null 文本归一；新增 9 项无界面回归且全部通过。父任务及原生集合未完成，完整兼容验收待后期集中进行。
- 2026-10-16 · 批次 003：补无参和 string[] 基础构造；保留输入数组独立性、null 文本归一及 Text/首子项同步。Clear 后读 Text 保持集合为空，重新访问 SubItems 才在同一集合创建新首项；与 WinForms 10.0.11 的 41 条观测相同。父任务其余构造和完整集合/native 同步未关闭。
- 2026-10-16 · 批次 004：嵌套 ListViewSubItem 增加公共基础构造、继承与 Name/Tag；集合 Add(string) 经 owner/text 构造保存库内关联。原 21 项构造回归及 41 条参考观测继续通过；完整集合重归属、样式和 native 同步没有扩面，见 [子项任务](docs/roadmap/controls/ListViewSubItem.md)。

- 2026-10-16 · 批次 006：嵌套集合增加四个标准 virtual key 成员，209 条固定参考/Lumina 观测相同，新增 38 项回归。保留单索引缓存、等长不变文化忽略大小写、重复 key 与 Name/集合修改及单项移除；原 41 条构造、37 条基础子项及 37 条样式回归保留。完整集合构造/IList/所属/native 仍另列。

历史按对应源码路径的相关内容摘要，不把提交标题当成功能验收证明。

- 2026-10-16 · 批次 007：owner 持有唯一子项数据，公共集合视图共享该数据但保留独立 key 缓存，Text 在外部视图添加/替换/清空后保持一致；整数索引首范围、对象身份与延迟首项恢复有 72 条固定参考对照，原 41 条基础构造回归保留。完整集合/所属/native 不随此关闭，见 [批次 007](docs/roadmap/batches/007.md)。

- 2026-10-16 · 批次 014：补 Name/Tag/ToolTipText 标准非虚属性。Name 不独立存储；读空表不恢复，写 null/空也恢复首项且旧空枚举器继续观察旧表。Tag 与 ToolTipText 独立，引用/文本原样保存，均不消费或释放外部资源。151 条固定英文参考/JIT 全同，新增 101 项测试；三种枚举/共享 view/独立 key 缓存及全部旧差分保持。中文 Designer 分类实测仍为 Data/Appearance，对参考“数据/外观”有差异；完整所属/已挂接 Tooltip/native、图像/分组/Clone/序列化和压力另列，新范围未记实际 AOT 运行验收，父任务 04 仅部分完成。见 [014](docs/roadmap/batches/014.md)。

#### docs/roadmap/controls/ListViewSubItem.md

- 2026-10-16 · 批次016历史回标：02c/d/e曾笼统写尚缺IList/Insert/Remove/枚举，现明确引用后续已完成02d/e/f首范围，真正并发/完整所属/已挂接样式/native仍未完成；保留原批次快照与任务ID，不把集合首范围扩大为完整所属验收。

- 2026-10-16 · 批次 016：主项样式新增后，默认惰性 Font 从子项私有存储移到主项/子项共享存储；借用 Font 寿命及既有 ResetStyle/直接子项样式保持，541 条主项样式对照含三枚举与各 view key 缓存，全部旧子项驱动通过。任务04仍部分完成，明确区分 UseItemStyleForSubItems 未挂接存值与已挂接绘制消费，后者、全局字体及所属/native仍未完成，见 [016](docs/roadmap/batches/016.md)。

- `b8a98eb` · 2026-04-09：ListViewItem.cs 同时加入基础 Text 子项及集合；未覆盖公开构造和样式契约。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。
- 2026-10-16 · 批次 004：先运行固定 WinForms 10.0.11 的 37 条观测，再补公共无参/owner-text、可继承性和 Text/Name/Tag；构造不会自动插入 owner.SubItems 或替换首项，null 字符串归空且保留非 null 字符串/Tag 身份。增加 13 项无 UI 回归，保留原构造与清空/恢复行为。Owner 只在库内保存，不新增公开属性；颜色字体/完整集合/native 未实现，不从指定场景推算完整兼容。

- 2026-10-16 · 批次 005：先运行固定 WinForms 10.0.11 参考端，再补五参构造、ForeColor/BackColor/Font 与 ResetStyle；37 条两端样式观测相同，新增 15 项测试。默认使用 WindowText/Window 与惰性共享的官方 SystemFonts.MessageBoxFont（本机为 Microsoft YaHei UI 9，不能代以 SystemFonts.DefaultFont）；显式 Font 保留借用身份，不因置空/替换/Reset 而释放。保留 owner/文字/首项语义。未接 Control.DefaultFont/Application 自定义默认字体与 item→ListView 回链，不能据此宣称已挂接继承/native 或完整样式兼容。

- 2026-10-16 · 批次 006：先运行固定参考端的 209 条观测，再补集合 string key 索引器、ContainsKey/IndexOfKey/RemoveByKey，新增 38 项回归。解除历史集合 sealed 以提供四个标准 virtual 成员；不改变现有泛型接口和内部构造形状。只保存一个最后命中索引，按参考端保留 Name 修改、Insert/Remove/Clear 后槽位的可观察选择，非空 miss 清除、null/空查询不清除；RemoveByKey 使用同一次查找结果只移除一项。比较先检 UTF-16 长度，再用不变文化忽略大小写，保留等长软连字符/NUL、Angstrom/Kelvin 等边界；不能以 OrdinalIgnoreCase 代替。完整集合构造、IList、所属和 native 缺口保留。

历史按对应源码路径的相关内容摘要，不把提交标题当成功能验收证明。

- 2026-10-16 · 批次 007：先固定参考，再让 owner 持有唯一子项数据，各公共集合为独立视图而非独立数据；构造 null owner 抛参数 owner，构造本身不生成首项，也不替换 owner.SubItems 的缓存视图。Text 直接读取 owner 数据；Clear 后访问 SubItems 或设置 Text 才恢复首项。整数 setter 先校验 index、后拒绝 null value，失败不改变数据；替换保留对象/Text/Tag 身份和各视图缓存。72 条对照与 27 项回归通过，旧基础/样式/key 回归保留。完整所属、Add/Insert/Remove/IList 和 native 仍缺，见 [批次 007](docs/roadmap/batches/007.md)。

- 2026-10-16 · 批次 008：先固定参考端，再补三个 typed Add 标准返回、参数及样式语义。Add(item) 拒绝 null（参数 item），保留派生/Tag/文字/字体身份，接受重复和其他 owner 的同一对象，原集合仍保留该对象；不将此行为当完整所属/native 同步。外部视图首次添加、Clear 后添加、主项 Text、共享数据与各视图最后 key 位置均保留。显式 ICollection<T>.Add 承接历史 void 消费；74 条参考/JIT 观测相同，40 项新增回归通过，原构造/样式/key/集合回归保留。清空、移除、ResetStyle、替换字体或子项不释放借用 Font，见 [批次 008](docs/roadmap/batches/008.md)。

- 2026-10-16 · 批次 009：先固定参考端，再补三个标准 typed AddRange。三重载 null 数组均抛 ArgumentNullException(items)，失败/空数组不改变共享数据或初始化首项；三者均跳过 null 元素，包括文本数组的 null 文字，与单项 Add(null) 归空不同。非 null 空 Text 对象与空字符串仍添加；原构造器的 null 元素归空行为保持，不改用 AddRange。顺序、原对象/派生/Tag/字符串身份、输入槽复制、重复/其他 owner、样式与借用 Font 寿命均有对照，外部首次/Clear 后视图、主项 Text 及各视图最后 key 索引保留。151 条固定参考/JIT 相同，121 项新增回归通过；未将本批新 AddRange 计为实际 AOT 运行验收，完整 IList/Insert/Remove/所属/native 仍缺，见 [批次 009](docs/roadmap/batches/009.md)。

- 2026-10-16 · 批次 010：先固定参考再实现 typed Insert/Remove 首范围。Insert 先校验 index（允许 Count），再拒绝 null item，原对象/文字/Tag/样式/其他 owner 数据及共享视图保留；公开非虚 void Remove 按 ReferenceEquals 查找首项，null/缺失不操作，派生 Equals 不参与。历史泛型 Insert 和 bool Remove 显式适配保留；Clear/RemoveAt 保持接口 virtual-final，不新增可覆盖 API。203 条固定参考/JIT 观测相同，110 项新增回归通过；首项移除、空集合惰性恢复、各视图独立 key 缓存与借用 Font 寿命均覆盖。未将新集合范围记为实际 AOT 运行验收，非泛型 IList、Contains/IndexOf/CopyTo 全契约、完整所属/挂接样式/native 仍缺，见 [批次 010](docs/roadmap/batches/010.md)。

- 2026-10-16 · 批次 011：固定参考后补非泛型 IList/ICollection 消费。对象 setter/Insert/Add 先验证类型，null/错型为 ArgumentException（value/item），typed 索引/Insert 的既有验证顺序保持；重复 Add 返回首次引用索引。Contains/IndexOf 标准 subItem 参数与非 virtual 形状、泛型显式适配均按引用身份，不调用派生 Equals。ICollection.CopyTo 为 private virtual-final 显式槽（dest/index），没有 public Array 重载；历史 typed CopyTo 通过同一路径保留。空集合跳过全部数组校验且不恢复首项，非空保留 rank/index/容量/类型顺序、非零下界和派生数组失败前写入；只把不兼容数组的 ArrayTypeMismatchException 转为标准 ArgumentException，保留 InvalidCastException。各 view 的 SyncRoot 是自身，IsSynchronized 按参考报告 true，不等于完整并发已验收。1,406 条固定参考/JIT 相同、270 项新增回归通过，共享数据/独立 key 缓存、借用 Font 与其他 owner 对象保持；新接口未提前计为实际 AOT 运行验收，完整枚举/并发、所属/挂接样式/native 另列，见 [批次 011](docs/roadmap/batches/011.md)。

- 2026-10-16 · 批次 012：固定参考后修正 public GetEnumerator 为 IEnumerator，并以 public virtual-final 直接实现 IEnumerable；历史 IEnumerable<T> 显式消费、typed 无效 Current=null 保留。2,531 条固定参考/JIT 全同，347 项新增回归通过。成功替换（包括同对象）/Add/Insert/Remove/非空 Clear 使 MoveNext/Reset 失效而 Current 保留缓存身份；失败/移除缺失/文字修改不失效。空 Clear 不改变版本，owner.SubItems 在空集合恢复首项时换数据表，所有 view 共享新表而旧空枚举器保持为空；不依赖 Array/List 内部反射或新增公开枚举类型。重复派生身份/双 view/独立 key 缓存与借用 Font 寿命保留。新枚举未记实际 AOT 运行验收；真实多线程/压力、完整所属/挂接样式/native 仍另列，见 [批次 012](docs/roadmap/batches/012.md)。
- 2026-10-16 · 批次 013：固定参考后补 public override string ToString()，复用 Object 虚槽，输出固定 ListViewSubItem 前缀与原样 Text（空值已归一为空括号）。58 条固定参考/JIT 相同，63 项新增回归通过，覆盖 Unicode/换行/嵌入 NUL、文本变更、派生覆盖/隐藏成员/原异常、共享 view/重复/移除/跨 owner、三种枚举消费与独立 key 缓存。不触发首项恢复，不修改数据表或枚举版本，不读取 Tag/样式或消费已释放的借用 Font。Clone/序列化/完整所属/已挂接样式/native 仍缺，新字符串未计实际 AOT 运行验收，见 [批次 013](docs/roadmap/batches/013.md)。

#### docs/roadmap/controls/MenuStrip.md

- 2026-10-16 · 批次027：私有TopLevelMenuItemHost复用Control.Click/OnClick，避免新基类事件与宿主独立事件分裂；既有鼠标释放处理位置不变，新增宿主回归通过。仅04a代码完成，04父项保持部分，完整键盘/激活/设备交互仍缺，见[027](docs/roadmap/batches/027.md)。

- 2026-10-16 · 批次022：07a接入有效Font/HFONT及显式前景/背景父链，默认palette不写public颜色。修复继承ToolStrip的宿主兼容检查把Label派生菜单标题误判为Button而在字体布局时替换的问题，现保留宿主引用并重新测量；08按实际顶层宿主/HMENU实现更正为部分。全部菜单激活/MDI/Renderer/DPI/Designer与三平台不关闭，见[022](docs/roadmap/batches/022.md)。

- 2026-10-16 · 批次021：移除历史菜单自定位，继承ToolStrip Dock事务并由实际父级DefaultLayout定位；保持既有菜单内容布局。仅父布局接入，不关闭完整菜单默认/激活/MDI/资源。 见[021](docs/roadmap/batches/021.md)，父任务仍部分；020历史条带定位/无首选测量描述只代表当时状态。

- 2026-10-16 · 批次020：保留既有菜单测量与宿主，只校正标准事务/直属Form衔接；MDI合并/完整激活/默认/资源和平台仍缺。 见[020](docs/roadmap/batches/020.md)；父任务保持部分完成。

- `b8da9a5`（2026-04-08）：调整菜单图像与文本间距。
- `573df6c`（2026-04-08）：进一步调整图文间距。
- `a1f0c5d`（2026-04-08）：调整水平 padding 与布局。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

#### docs/roadmap/controls/Panel.md

- 2026-10-26 · 批次032：完成01b标准ScrollableControl迁移和02a共享AutoScroll消费；完整Panel属性、派生布局、真实设备、Designer/AOT和平台仍待，见[032](docs/roadmap/batches/032.md)。

- 2026-10-17 · 批次029：完成01a必要验证容器style；未迁移历史基类或宣称整Panel/全部派生完成，见[029](docs/roadmap/batches/029.md)。

- 2026-10-16 · 批次021：03a按基础真实测量/布局子范围完成；继承迁移仍留01。Visible布局源于官方ScrollableControl.OnVisibleChanged，暂在历史Panel路径承接，不给所有Control伪造事件。未用局部测量关闭Panel或滚动。

- 2026-10-16 · 批次020：按实际代码纠正历史“没有独立ScrollableControl层”与“布局暂停空实现”：标准层自018存在但Panel未迁入，集合自018共用，020事务由Control提供。Panel-01/03回标部分，保留继承迁移/几何/滚动缺口；不把基类10b完成转为Panel完成。

- `7d38999`（2026-04-08）：涉及 Panel 的深色主题应用调整。
- `ce86ab5`（2026-03-31）：接入 BackColor/ForeColor、继承主题和实时刷新。
- `3323e20`（2026-03-31）：容器与布局交互改进涉及本文件。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

#### docs/roadmap/controls/PictureBox.md

- 2026-10-16 · 批次027：移除独立Click/OnClick和提前处理STN_CLICKED的路由，继承Control标准鼠标释放/点击链，typed与Control订阅共用事件槽。指定委托/消息去重回归通过，锁屏下WindowFromPoint未命中，成功实际Click/DoubleClick设备验收待补。 027完成008a共享Click及基础鼠标路径去重；BorderStyle、AutoSize/GetPreferredSize、背景图/透明主题、完整Enabled/Visible和点击设备行为仍待对照。当前native UserPaint=false与参考默认差异保留。 25项必要消费回归合计通过；固定参考消费者差异及锁屏限制见[027](docs/roadmap/batches/027.md)，不关闭整个控件/平台。

- 2026-10-16 · 批次023：基础Control启用标准UserPaint后，本控件构造显式保留UserPaint=false，使现有系统窗口类过程及原生字体/绘制继续生效。仅属消息与绘制基础的必要衔接，不代表本控件完整Styles、Paint事件或视觉/输入兼容完成，见[023](docs/roadmap/batches/023.md)。

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 2026-04-08 · `7d38999` · feat: 增强深色模式支持，优化多个控件的主题应用逻辑。
- 2026-03-31 · `ce86ab5` · 已经把 Lumina.Forms 的主题能力接到更像 WinForms 的核心表面上了。现在 Control.cs 和 Form.cs 提供了 BackColor / ForeColor、对应变更事件、主题槽位解析、父容器背景继承，以及打开窗口后的实时主题刷新；高频控件也统一改成走 ApplyExp…（原提交标题节录）。
- 2026-03-31 · `b30551a` · Refactor NativeTheme to align with Windows 11 design; update NotifyIcon for improved context menu handling; enhance PictureBox with click event; imple…（原提交标题节录）。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

#### docs/roadmap/controls/RadioButton.md

- 2026-10-16 · 批次027：BN_CLICKED同步当前按钮既有Checked/CheckedChanged，再调用共享OnClick一次；StandardClick关闭，保留参考StandardDoubleClick=true。仅当前按钮通知衔接代码完成，不关闭ButtonBase、AutoCheck、托管互斥组及键盘选择。 已有CheckedChanged，027完成003a当前按钮原生状态→共享Click；原全待办遗漏历史事件，现纠正。组内旧项取消通知/新项顺序、AutoCheck和直接OnClick状态切换仍缺，原生兄弟取消不等于托管组同步。 25项必要消费回归合计通过；固定参考消费者差异及锁屏限制见[027](docs/roadmap/batches/027.md)，不关闭整个控件/平台。

- 2026-10-16 · 批次023：基础Control启用标准UserPaint后，本控件构造显式保留UserPaint=false，使现有系统窗口类过程及原生字体/绘制继续生效。仅属消息与绘制基础的必要衔接，不代表本控件完整Styles、Paint事件或视觉/输入兼容完成，见[023](docs/roadmap/batches/023.md)。

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 2026-04-08 · `2545995` · feat: 增强深色模式支持，优化多个控件的主题应用逻辑。
- 2026-04-08 · `108ef2b` · feat: 添加对 Win32 黑暗模式的支持，重构多个控件以优化主题应用逻辑。
- 2026-03-31 · `ce86ab5` · 已经把 Lumina.Forms 的主题能力接到更像 WinForms 的核心表面上了。现在 Control.cs 和 Form.cs 提供了 BackColor / ForeColor、对应变更事件、主题槽位解析、父容器背景继承，以及打开窗口后的实时主题刷新；高频控件也统一改成走 ApplyExp…（原提交标题节录）。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

#### docs/roadmap/controls/RichTextBox.md

- 2026-11-07 · 批次069：069限定完成RichTextBox-005d标准Protected通知及真实保护拒改，并以Control-17g补必要WM_NOTIFY原生结果回传。Form改按来源HWND路由，容器/停车接收者保持0/1结果；旧OnNotify钩子保留。保护样式本身与全文替换允许，受保护选区替换/格式修改拒绝；流拒改消耗保护标志，不伪造成功修改。96新增及1048相关通过，Release固定双目标0错/2既有NU1510。完整LinkClicked/TextChanged/其他通知、文件/OLE/撤销、Find/完整选择测量、Masked模型与平台/98%仍待；064全量FAIL/未解决、065超时及066～069全量NOT_RUN保持。用户2026-11-07明确要求本批完成后停止连续代码实施；下一新会话只整理路线图/文档，将完成范围迁入变更记录并合并重复内容，不启动070。 005及其他父项保持部分，见[069](docs/roadmap/batches/069.md)；下述068及以前为历史。

- 2026-11-07 · 批次068：005b/005c只关闭对应选择/滚动/IME程序通知范围；原ID/未完父项/固定分母及064FAIL/065超时保持，见[068](docs/roadmap/batches/068.md)。

- 2026-11-07 · 批次067：完成003f/005a限定范围，005从待办整理为部分；纠正当前总述/011仍把已完成颜色同步列全待办的滞后。保留原父项/ID/固定分母与064FAIL/065超时，原失败与独立原生诊断见[067](docs/roadmap/batches/067.md)。

- 2026-11-07 · 批次066：限定RichTextBox-003e颜色同步，原ID/未完父项/固定分母保持；064全量FAIL及065超时不由本批检查清除，见[066](docs/roadmap/batches/066.md)。

- 2026-11-07 · 批次065：限定完成003d SelectionFont，93新增通过；首77/85与独立中英FACE缺位8/8保持，生产行为/旧断言未为失败改变，完整Font同步与003父项仍待。 本批全量180秒超时未完成，064全量FAIL/超时未解门槛保留，见[065](docs/roadmap/batches/065.md)。

- 2026-11-07 · 批次064：限定完成003c/008a，101新增通过，完整父项保留；首两轮100/101的长段落物理行/跨窗口布局假设失败保留，生产未为失败改变。只修新夹具并保70001硬索引、各窗口原生往返与本HWND精确行号对照；最终全量见[064](docs/roadmap/batches/064.md)。

- 2026-11-07 · 批次063：限定完成Rich-004c/004d；86新增/6,045全量及固定双目标Release通过，首85/86的新AllowNull反射夹具失败回执保留。原ID/未完父项/固定分母保持，见[063](docs/roadmap/batches/063.md)。

- 2026-11-07 · 批次062：003b字符偏移/选区保护属性与004b三项段落缩进限定范围完成；110新增/5,959全量首轮及固定双目标Release通过。屏幕DC/twip、原生mask/精确结构/混合读回和RTF重建保持；未完父项/平台/98%及固定分母保留，见[062](docs/roadmap/batches/062.md)。

- 2026-11-07 · 批次061：新增003a/004a，004由全待办纠正为部分；SelectionColor仅存值的当前描述按真实native实现更新，003/004及其他父项保持未完。67新增/5,849全量首轮及固定双目标Release通过；旧测试/项目/Demo未改，原ID/固定分母保持，见[061](docs/roadmap/batches/061.md)。

- 2026-11-07 · 批次060：新增002c/011a，011从全待办纠正为部分；002/001保留未完父项，当前RTF/格式缓存描述按实际代码更新。54新增/5,782全量及固定双目标Release通过；首测试unsafe编译失败与首50/54保留，修真实新纯文Modified恢复缺口及空选区新夹具。原ID/未完任务/分母保持，见[060](docs/roadmap/batches/060.md)。

- 2026-11-07 · 批次059：新增002a/002b，002由全待办改部分；当前Text/限长/逻辑读取描述按真实代码修正，058与此前失败证据保持。64新增/5,728全量通过，首61/64含真实Modified重建缺口及两项新夹具问题，058旧反例明确改为native17/流恢复3。只关闭纯文限定范围，原ID/未完父项/固定分母保持，见[059](docs/roadmap/batches/059.md)。

- 2026-11-07 · 批次058：新增001a/007a；001/007从全待办改部分，纠正当前TextBox层级/EDIT后端及HideSelection独立存值描述，旧042～057快照保持。最终96新增/5,664全量通过；首94/96中程序设文native扩限和BOOL返回-1经独立RichEdit核实，只修新夹具，生产/旧测试未为失败改动。真实Text/StreamIn缺口明确留002；全部原ID/未完项/分母保持，见[058](docs/roadmap/batches/058.md)。

- 2026-11-07 · 批次057：普通EDIT的基类002d/003h不关闭本类型001/002或其他父项，源码no-op、不重复计数；真实RICHEDIT50W、专有默认与格式行为仍待，见[057](docs/roadmap/batches/057.md)。

- 2026-11-07 · 批次056：普通EDIT的基类003f/003g不关闭本类型直接继承/真实后端或其他父项，源码no-op、不重复计数，见[056](docs/roadmap/batches/056.md)。

- 2026-11-07 · 批次055：普通EDIT的TextBox-004b/TextBoxBase-003e不关闭本类型001/007/008或其他父项；源码no-op，不重复计数，见[055](docs/roadmap/batches/055.md)。

- 2026-11-07 · 批次054：普通EDIT的007c/007d/007e不关闭RichTextBox-001/007或其他父项；源码与未完成真实后端保持，见[054](docs/roadmap/batches/054.md)。

- 2026-11-07 · 批次053：普通EDIT的007b/008c不关闭RichTextBox-001/007/008或其他父项，不重复计数，见[053](docs/roadmap/batches/053.md)。

- 2026-11-07 · 批次052：基类008a/008b只验普通EDIT，不关闭RichTextBox-001/008或其他父项，不重复计数；本类型源码保持，见[052](docs/roadmap/batches/052.md)。

- 2026-11-07 · 批次051：普通EDIT词删除的间接消费不补本类型真实后端/标准层级，源码no-op，所有原父任务保持，见[051](docs/roadmap/batches/051.md)。

- 2026-11-07 · 批次050：源码no-op，共享005e入口和003d禁用行为不关闭RichEdit的剪贴板格式、快捷键/富文本编辑或标准直接层级，原任务和固定分母保持，见[050](docs/roadmap/batches/050.md)。

- 2026-11-07 · 批次049：源码no-op，普通EDIT共享Clear/重挂命令不计RichEdit专有支持，原任务及固定分母保持，见[049](docs/roadmap/batches/049.md)。

- 2026-11-07 · 批次048：源码no-op，共享普通EDIT追加/撤销不计RichEdit专有支持，原父任务及真实后端继续待办，见[048](docs/roadmap/batches/048.md)。

- 2026-11-07 · 批次047：源码no-op，共享普通EDIT替换/分行/输入限制不实现RichEdit专有语义或标准直接层级；全部原父任务保留，见[047](docs/roadmap/batches/047.md)。

- 2026-11-07 · 批次046：源码no-op，共同边框与选择方法的间接消费不实现RichEdit选区、格式、撤销或标准直接基类；仍由原任务管理真实后端，见[046](docs/roadmap/batches/046.md)。

- 2026-11-07 · 批次045：源码no-op，间接共享Modified/程序设文规则不等于RichEdit修改/撤销完成；错误层级间接暴露CharacterCasing不计本类型标准支持。完整直接基类和真实后端保留原任务，见[045](docs/roadmap/batches/045.md)。

- 2026-11-07 · 批次044：HideSelection仅补显式new避免隐藏声明告警，不转发到标准基类，不改变派生存值语义。完整001/007和真实富文本后端仍未完成；通过错误TextBox层级暴露的TextAlign不计本类型标准支持。

- 2026-11-07 · 批次043：源码no-op，间接共享真实折行/滚动样式及重建路径，不能据此关闭001或RichEdit/富文本功能；标准直接基类/派生默认仍待，见[043](docs/roadmap/batches/043.md)。

- 2026-11-07 · 批次042：源码no-op，随TextBox间接消费新TextBoxBase的共享EDIT首范围；仍直接继承TextBox且原生仍EDIT，RichTextBox-001及富文本功能不关闭。不能把共同基类存在或EM_SETBKGNDCOLOR消息当RichEdit支持，见[042](docs/roadmap/batches/042.md)。

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 2026-04-09 · `2fb513a` · feat: 添加异步调用支持，增强 ListView 和 RichTextBox 控件功能，优化 TableLayoutPanel 布局逻辑。
- 2026-04-08 · `2545995` · feat: 增强深色模式支持，优化多个控件的主题应用逻辑。
- 2026-04-08 · `7149595` · Implement dark mode capabilities and menu rendering enhancements。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

#### docs/roadmap/controls/ScrollableControl.md

- 2026-11-07 · 批次034收尾：纠正01父项仍称Panel使用历史基类的滞后描述，保留032已完成迁移与033聚焦滚动范围；本批未修改ScrollableControl源码或关闭完整滚动任务。

- 2026-10-26 · 批次032：完成08a Windows滚动消息与SCROLLINFO代码首范围，Panel迁移并消费共享滚动层；真实物理输入、平台后端和完整验收仍待，见[032](docs/roadmap/batches/032.md)。

- 2026-10-26 · 批次031：完成02a～06a托管滚动首范围，补正设负读、内容范围、ScrollProperties/事件、受保护状态位和滚动路径；真实Win32滚动条消息、Panel迁移、平台和完整验收仍待，见[031](docs/roadmap/batches/031.md)。

- 2026-10-17 · 批次029：完成01b必要验证容器style；标准构造位有固定源码依据，未扩大为完整滚动或默认样式验收，见[029](docs/roadmap/batches/029.md)。

- 2026-10-16 · 批次021：按官方OnVisibleChanged补Visible时真正请求本体布局/缓存失效，ContainerControl/Form继承消费。原无滚动DisplayRectangle保留，未实现AutoScroll/ScrollProperties，也未迁移Panel；父项继续部分。

- 2026-10-16 · 批次020：移除第二份暂停计数/延期状态与无条件子树遍历，继承Control标准事务，基础恢复不会强制对子孙调用PerformLayout。既有无滚动DisplayRectangle保留；Panel继承、滚动状态/坐标/平台后端仍缺，不能由本改动关闭滚动父项。

- 2026-10-16 · 批次018：新增标准类支持Form真实继承，共用Control状态并提供客户区DisplayRectangle/布局暂停衔接。纠正历史“同名类不存在”为当前部分状态；Panel、所有滚动和GTK4/AppKit仍缺，未新增整类型验收，见[018](docs/roadmap/batches/018.md)。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

#### docs/roadmap/controls/SplitContainer.md

- 2026-10-16 · 批次028：本控件代码未改；container-lookup记录现有ContainerControlBase : Control不具标准ContainerControl身份，活动投影少split:null。保留严格失败并归01标准继承前置；基类导航实现不关闭SplitContainer导航或整个控件，见[028](docs/roadmap/batches/028.md)。

- 2026-10-16 · 批次021：正常Vertical/Horizontal尺寸变化使用总轴长/Panel1比例并向下取整，连续缩放不重设已舍入比例；FixedPanel两侧、直接距离/暂停后的真实逐轴Bounds操作保留Panel2/Bounds延期参数。固定探针双方向相关观测及往返测试通过，完整折叠/min/RTL另列。 见[021](docs/roadmap/batches/021.md)，父任务仍部分；020历史条带定位/无首选测量描述只代表当时状态。

- 2026-10-16 · 批次020：修复多余Suspend/Resume覆盖延期参数的问题，不硬替换事件参数；构造期Panel尚未初始化有保护。24场景对照保留5行比例几何差异；标准SplitterPanel/事件取消/完整算法不关闭。 见[020](docs/roadmap/batches/020.md)；父任务保持部分完成。

- `7d7a67e`（2026-04-08）：Lumina.Forms 实现整合涉及拆分容器。
- `108ef2b`（2026-04-08）：接入 Win32 深色主题应用。
- `ce86ab5`（2026-03-31）：接入核心颜色与继承主题。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

#### docs/roadmap/controls/StatusStrip.md

- 2026-10-16 · 批次021：移除历史Form底部专用定位，实际父级DefaultLayout消费Dock.Bottom，保留既有item/Spring局部布局。仅父布局衔接完成，完整首选测量/事件/命中及算法仍缺。 见[021](docs/roadmap/batches/021.md)，父任务仍部分；020历史条带定位/无首选测量描述只代表当时状态。

- 2026-10-16 · 批次020：移除重复的OnBoundsChanged/PerformLayout路径，保留原Spring局部算法；完整Spring/首选测量/事件/SizingGrip命中仍待验收。 见[020](docs/roadmap/batches/020.md)；父任务保持部分完成。

- `cb090bc`（2026-04-08）：记事本与 UI 改进涉及状态栏。
- `7d7a67e`（2026-04-08）：初始实现整合涉及状态栏。
- `108ef2b`（2026-04-08）：深色主题应用重构。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

#### docs/roadmap/controls/TabControl.md

- 2026-10-17 · 批次029：增加05a必要验证递归消费，保留可取消切页与导航缺口；按现有代码将01由全待办纠正为部分，未关闭集合/控件，见[029](docs/roadmap/batches/029.md)。

- 2026-10-16 · 批次023：基础Control启用标准UserPaint后，本控件构造显式保留UserPaint=false，使现有系统窗口类过程及原生字体/绘制继续生效。仅属消息与绘制基础的必要衔接，不代表本控件完整Styles、Paint事件或视觉/输入兼容完成，见[023](docs/roadmap/batches/023.md)。

- 2026-10-16 · 批次022：移除ApplyNativeColors公共属性写入，theme与显式页颜色分离；08a为必要消费子范围，08按既有TCM/TCN实现从全待办更正为部分。没有将基础Control字体/颜色契约当作完整TabControl兼容，见[022](docs/roadmap/batches/022.md)。

- 2026-10-16 · 批次020：现有页定位消费标准OnLayout，完整页边界/选择事件/标准集合与原生切换仍待后续；此批只完成事务衔接。 见[020](docs/roadmap/batches/020.md)；父任务保持部分完成。

- 2026-10-16 · 批次018集成：标准Control可见通知开始创建可见子树，暴露本地TabControl在无HWND时也提前切换TabPage.Visible的历史差异；官方SelectedIndex无句柄时只保存索引，读取DisplayRectangle才创建用于精确计算。现有布局测试改为核对实际DisplayRectangle与选中页Bounds一致，不再把本地4/21估计值当标准参考。未扩大本控件实现或关闭选择/布局任务；该差异继续留本页待办。

- `9d5165f`（2026-04-09）：扩展 TabControl 兼容功能。
- `f666aa6`（2026-04-08）：UI 和发布改进提交涉及本文件。
- `cb090bc`（2026-04-08）：记事本/UI 改进提交涉及本文件。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

#### docs/roadmap/controls/TableLayoutPanel.md

- 2026-11-07 · 批次054：首轮全量暴露公开AutoSize消费的真实缺口，修正为内部布局状态；原ContainerControlTests固定80宽/Margin硬期望不改，全部原父项保留，见[054](docs/roadmap/batches/054.md)。

- 2026-10-16 · 批次021：专用引擎执行既有cell算法并使用内部布局边界；绝对40/60列、20行、Padding2的104×24测量/AutoSize有对照。现有样式列表无变更通知，因此明确禁用PreferredSize缓存，未伪装标准样式集合；完整table算法另列。 见[021](docs/roadmap/batches/021.md)，父任务仍部分；020历史条带定位/无首选测量描述只代表当时状态。

- 2026-10-16 · 批次020：标准事件先于现有表格算法，保留行列分配局部实现；样式集合/RowSpan/完整扩展属性和首选测量未完成。 见[020](docs/roadmap/batches/020.md)；父任务保持部分完成。

- `2fb513a`（2026-04-09）：优化 TableLayoutPanel 布局逻辑。
- `9d5165f`（2026-04-09）：新增兼容控件功能涉及表布局。
- `3323e20`（2026-03-31）：布局和交互能力改进。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

#### docs/roadmap/controls/TextBox.md

- 2026-11-07 · 批次069：069本类生产/原任务状态no-op；RichTextBox-005d与Control-17g只补保护拒改及必要原生结果连接，不增加本类完成项。完整共享/Masked模型、缩放/线程/模态/原父Form寿命和平台保持；064FAIL/065超时未解，见[069](docs/roadmap/batches/069.md)。下述068及以前为历史。

- 2026-11-07 · 批次068：005b/005c只关闭对应选择/滚动/IME程序通知范围；原ID/未完父项/固定分母及064FAIL/065超时保持，见[068](docs/roadmap/batches/068.md)。

- 2026-11-07 · 批次067：本类源码/原任务状态保持，不把Rich专有Font/ContentsResized计为继承完成项；完整输入/平台门槛保留，见[067](docs/roadmap/batches/067.md)。

- 2026-11-07 · 批次066：限定RichTextBox-003e颜色同步，原ID/未完父项/固定分母保持；064全量FAIL及065超时不由本批检查清除，见[066](docs/roadmap/batches/066.md)。

- 2026-11-07 · 批次065：源码/原任务状态no-op；Rich-003d不增加本类型完成项，原ID/未完父项/固定分母保持。 本批全量180秒超时未完成，064全量FAIL/超时未解门槛保留，见[065](docs/roadmap/batches/065.md)。

- 2026-11-07 · 批次064：Rich-003c/008a独立完成限定范围，本类型及普通EDIT实现未变，不新增继承完成项，见[064](docs/roadmap/batches/064.md)。

- 2026-11-07 · 批次063：限定完成Rich-004c/004d；86新增/6,045全量及固定双目标Release通过，首85/86的新AllowNull反射夹具失败回执保留。原ID/未完父项/固定分母保持，见[063](docs/roadmap/batches/063.md)。

- 2026-11-07 · 批次062：062本类源码no-op；Rich字符偏移/保护标记和段落缩进不扩写普通EDIT完成范围。110新增/5,959全量首轮与固定双目标Release通过，完整输入/布局/Designer和平台父项保持。见[062](docs/roadmap/batches/062.md)。

- 2026-11-07 · 批次061：源码/原任务no-op，Rich-003a/004a不计本类型新增完成范围，原ID/未完父项/分母保持，见[061](docs/roadmap/batches/061.md)。

- 2026-11-07 · 批次060：源码no-op，Rich的002c/011a不重复计为普通TextBox完成任务；全量5,782回归通过，完整父项/ID/分母保持，见[060](docs/roadmap/batches/060.md)。

- 2026-11-07 · 批次059：本类源码/原任务no-op，必要共享选择恢复连接不重复计数，所有未完父项/固定分母保持，见[059](docs/roadmap/batches/059.md)。

- 2026-11-07 · 批次058：源码与原任务no-op，Rich直接层级/真实后端归RichTextBox-001a/007a，不重复计继承或TextBox完成项；固定分母及全部未验门槛保持，见[058](docs/roadmap/batches/058.md)。

- 2026-11-07 · 批次057：源码no-op，共享TextLength分支/必要编辑元数据归基类002d/003h，不重复计数；完整TextBox父项及未验门槛保持，见[057](docs/roadmap/batches/057.md)。

- 2026-11-07 · 批次056：源码no-op，构造四位/左键专用点击和必要绘图/间距声明归基类003f/003g，不重复计数。原生命中均false，不称完整点击或Designer验收；原父项及固定分母保持，见[056](docs/roadmap/batches/056.md)。

- 2026-11-07 · 批次055：新增004b，004/010按真实滚动条测量范围纠正当前缺口；固定无句柄重建缓存边界保留，原首选尺寸四个虚调用轨迹补新增两次Multiline前导，原尺寸/异常断言不降。基类003e不重复计数，完整父项保持，见[055](docs/roadmap/batches/055.md)。

- 2026-11-07 · 批次054：源码no-op，继承基类007c/007d/007e，不新增重复完成任务；旧尺寸/default当前缺口按有限实现纠正，完整缩放、样式和平台仍待，见[054](docs/roadmap/batches/054.md)。

- 2026-11-07 · 批次053：源码no-op，继承基类007b/008c，010按计算与完整生命周期分别记录；旧高度、AutoSize/default和平台父项保持，见[053](docs/roadmap/batches/053.md)。

- 2026-11-07 · 批次052：源码no-op，继承基类008a/008b，未新增重复完成任务；旧24/48最小高度、完整测量/default及平台缺口保持，见[052](docs/roadmap/batches/052.md)。

- 2026-11-07 · 批次051：源码no-op，继承009a启用词删除命令；普通EDIT限定回归不关闭完整009/输入/测量/平台，也不重复计数，见[051](docs/roadmap/batches/051.md)。

- 2026-11-07 · 批次050：源码no-op，009按真实继承的命令入口及快捷键禁用范围同步，不重复新增任务或关闭父项。15个剪贴板消息用例和62个快捷键用例均通过，未读写系统剪贴板或实际弹出custom菜单；完整编辑/尺寸及平台门禁保持，见[050](docs/roadmap/batches/050.md)。

- 2026-11-07 · 批次049：源码no-op，002按真实继承Clear虚调用及重挂命令连接同步，不重复新增继承完成任务；005/009/010及平台父项保持，见[049](docs/roadmap/batches/049.md)。

- 2026-11-07 · 批次048：源码no-op，继承标准AppendText与公开撤销入口，002/009按实际范围同步且父项仍部分，不重复计数。AppendText旧value命名实参须改为text；完整剪贴板/测量/输入/运行时RTL仍待，见[048](docs/roadmap/batches/048.md)。

- 2026-11-07 · 批次047：源码no-op，002/009按真实继承的SelectedText/Lines/MaxLength更新；009由全待办纠正为部分，不重复计数。程序替换可超过MaxLength，后续用户输入/Undo仍由原生限制；不把内部撤销分支当公开Undo或完整编辑实现，见[047](docs/roadmap/batches/047.md)。

- 2026-11-07 · 批次046：源码no-op，消费共同边框和两个标准选择方法；010按实际共享边框子范围纠正当前缺口描述，不重复新增TextBox完成任务。Upper原生初值在重建前明确观测，单线边框公开位按独立EDIT规范化读回核对，完整测量/输入和派生后端仍待，见[046](docs/roadmap/batches/046.md)。

- 2026-11-07 · 批次045：007b补CharacterCasing标准属性/枚举与原生消费；不把错误继承链中Rich/Masked间接暴露计为它们的标准成员或真实后端完成。继承TextBoxBase-005a的Modified/程序设文规则，完整Text/选区/撤销/尺寸和运行时RTL仍待；原ID及父任务保持，见[045](docs/roadmap/batches/045.md)。

- 2026-11-07 · 批次044：新增007a，007按实际范围由全待办改部分，004交叉引用对齐/水平条范围。HorizontalAlignment原Center/Right整数反置按固定v10.0.11修正，ColumnHeader等按名字重编译保留名字语义，旧二进制/整数存储须复核。首次42项中4例裸native RTL夹具缺少既有Control创建后UpdateStyles步骤而失败；保留原回执，复现该管线后42项通过。生产Control未改，不以样式位对照关闭完整RTL/视觉或007父项，见[044](docs/roadmap/batches/044.md)。

- 2026-11-07 · 批次043：新增004a，004由全待办纠正为部分；ScrollBars不再只存值，None纠正旧强制纵条，WordWrap实际影响折行并保留同值布局/缓存副作用。共享基类保留既有Enter规则/高度，默认Left以外对齐、首选尺寸/Designer/AOT/设备和派生后端仍未验收，见[043](docs/roadmap/batches/043.md)。

- 2026-11-07 · 批次042：001a迁移直接基类，001改部分；原002/003/005/010范围不重复计数。Multiline、ReadOnly等成员转为继承自TextBoxBase，TextLength为virtual；反射声明位置/基类变化明确记录，二进制/Designer/AOT完整消费仍待。旧SelectionChanged扩展保留在TextBox，不能由其存在计标准API支持，见[042](docs/roadmap/batches/042.md)。

- 2026-11-07 · 批次039：005a补只读/密码IME限制及必要原生密码同步，005从全待办纠正为部分，原ReadOnly映射与本批范围分别记账。多行EDIT的显示支持不决定EM_GETPASSWORDCHAR返回值，改用独立原生基线且保留原失败；受限setter缓存配置、设计模式不强制Disable。完整防泄漏/AutoComplete/TextBoxBase仍待，见[039](docs/roadmap/batches/039.md)。

- 2026-11-07 · 批次038：003c修复Multiline只存值及无句柄Select丢状态，新增标准MultilineChanged/OnMultilineChanged；选择与modified恢复在base.OnHandleCreated通知后，按固定源码保持顺序。负start/选择属性负值从旧钳制改为标准参数异常，负length有向选择与极值保留。完整层级、尺寸/布局、撤销和IME仍待，见[038](docs/roadmap/batches/038.md)。

- 2026-11-07 · 批次037：003b接入派生键判别及对话框路由，8项回归通过；已有AcceptsReturn/AcceptsTab不再仅存值，但完整样式、TextBoxBase/IME/编辑命令和设备仍待，见[037](docs/roadmap/batches/037.md)。

- 2026-10-16 · 批次028：完成003a必要消费，真实EDIT程序焦点切换不重复Enter/Leave，保留Selection更新路径。003按实际子范围改部分，全部文本行为/标准继承/键盘/IME/平台未关闭，见[028](docs/roadmap/batches/028.md)。

- 2026-10-16 · 批次023：基础Control启用标准UserPaint后，本控件构造显式保留UserPaint=false，使现有系统窗口类过程及原生字体/绘制继续生效。仅属消息与绘制基础的必要衔接，不代表本控件完整Styles、Paint事件或视觉/输入兼容完成，见[023](docs/roadmap/batches/023.md)。

- 2026-10-16 · 批次022：010由全待办改部分，010a明确关闭基础字体所有权/继承及必要颜色默认；不再将外部Font会被Dispose写成当前问题。公开属性准确默认与native palette消费分离，3条默认前景色差异保留；TextBoxBase标准继承、ReadOnlyChanged/完整事件、PreferredHeight/DPI/IME和Designer仍缺。固定TextBoxBase v10.0.11源码及hash保存在022证据目录。

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 2026-04-09 · `9d5165f` · feat: 添加多个控件和功能，增强用户界面和交互体验。
- 2026-04-08 · `cb090bc` · Add Lumina Notepad demo application with basic text editing features。
- 2026-04-08 · `108ef2b` · feat: 添加对 Win32 黑暗模式的支持，重构多个控件以优化主题应用逻辑。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

#### docs/roadmap/controls/TextBoxBase.md

- 2026-11-07 · 批次069：069本类生产/原任务状态no-op；RichTextBox-005d与Control-17g只补保护拒改及必要原生结果连接，不增加本类完成项。完整共享/Masked模型、缩放/线程/模态/原父Form寿命和平台保持；064FAIL/065超时未解，见[069](docs/roadmap/batches/069.md)。下述068及以前为历史。

- 2026-11-07 · 批次068：005b/005c只关闭对应选择/滚动/IME程序通知范围；原ID/未完父项/固定分母及064FAIL/065超时保持，见[068](docs/roadmap/batches/068.md)。

- 2026-11-07 · 批次067：本类源码/原任务状态no-op，Rich字体与尺寸通知不新增继承完成项，原父项/ID/分母保持，见[067](docs/roadmap/batches/067.md)。

- 2026-11-07 · 批次066：限定RichTextBox-003e颜色同步，原ID/未完父项/固定分母保持；064全量FAIL及065超时不由本批检查清除，见[066](docs/roadmap/batches/066.md)。

- 2026-11-07 · 批次065：源码/原任务状态no-op；Rich-003d不增加本类型完成项，原ID/未完父项/固定分母保持。 本批全量180秒超时未完成，064全量FAIL/超时未解门槛保留，见[065](docs/roadmap/batches/065.md)。

- 2026-11-07 · 批次064：Rich按专有POINTL/EXLINEFROMCHAR覆盖三项查询并新增SelectionType，本类源码不变；继承消费不新增或关闭基类父项，见[064](docs/roadmap/batches/064.md)。

- 2026-11-07 · 批次063：限定完成Rich-004c/004d；86新增/6,045全量及固定双目标Release通过，首85/86的新AllowNull反射夹具失败回执保留。原ID/未完父项/固定分母保持，见[063](docs/roadmap/batches/063.md)。

- 2026-11-07 · 批次062：062本类源码no-op；Rich的003b/004b沿现有句柄/选择/RTF恢复和原生消息连接，不新增共享基类完成任务。110新增/5,959全量首轮与固定双目标Release通过；完整基类/Masked模型与平台父项保持。见[062](docs/roadmap/batches/062.md)。

- 2026-11-07 · 批次061：源码/任务no-op，Rich字符颜色与段落对齐归003a/004a，不重复计共享基类完成项；所有未完父项/ID/分母保持，见[061](docs/roadmap/batches/061.md)。

- 2026-11-07 · 批次060：源码no-op，Rich专有RTF流与格式缓存归其002c/011a；不由派生限定范围关闭共享基类或Masked依赖，原ID/父项/分母保持，见[060](docs/roadmap/batches/060.md)。

- 2026-11-07 · 批次059：原选择恢复函数改internal供Rich消费，OnHandleCreated移除提前恢复、CreateHandle finally边界保持；仅必要依赖，不新增基类完成项。64新增/5,728全量通过，完整基类/缩放/平台门槛保持，见[059](docs/roadmap/batches/059.md)。

- 2026-11-07 · 批次058：源码no-op，001当前“Rich仍待直接继承”的描述按真实代码纠正并交叉引用RichTextBox-001a/007a；不新增基类完成任务，完整父项与Masked/Rich专有模型缺口保持，见[058](docs/roadmap/batches/058.md)。

- 2026-11-07 · 批次057：新增002d/003h，纠正有窗TextLength仍取虚Text以及必要默认事件/绑定、Paint/选区/两组分类缺口。003e当前行中仍全待办的构造点击描述改为交叉引用已实现003f，原055历史/正向门槛保持。无窗异常/虚调用及Control既有直接Paint行为保留，全部原父项和固定分母保持；41新增/5,568全量首轮及固定双目标Release通过、旧测试未改，见[057](docs/roadmap/batches/057.md)。不把设计声明/普通EDIT长度计为完整Designer、绑定或真实派生后端。

- 2026-11-07 · 批次056：新增003f/003g，仅关闭上述代码范围；纠正当前构造点击样式/这八项元数据仍全待办的描述，旧批历史不回写。46新增/5,527全量和固定双目标Release通过，未改旧测试。两次WindowFromPoint均no-hit，不将负向通过冒充正向点击验收，原始TRX保留；完整003/平台与固定分母保持，见[056](docs/roadmap/batches/056.md)。

- 2026-11-07 · 批次055：新增003e，只关闭创建请求位/前置标志有限范围；当前ES_WANTRETURN缺口按真实代码纠正，完整构造点击/主题/元数据父项仍待。首67/68的一例新夹具错误和最终68新增/5,481全量证据见[055](docs/roadmap/batches/055.md)，未为失败修改生产。

- 2026-11-07 · 批次054：新增007c/007d/007e限定范围，007仍部分；当前旧24/48与AutoSize/default/缓存待办描述按真实代码纠正，历史记录保留。全量首6失败中Table公开AutoSize消费为真实生产缺口，Flow同源消费者一并修复；其余5失败明确固定夹具AutoSize=false并保原断言。最终回执与未验范围见[054](docs/roadmap/batches/054.md)。

- 2026-11-07 · 批次053：007b只补真实首选尺寸计算，008c只补普通EDIT延期/空文本滚动，完整007/008保持部分；缺少的标准Padding.Size在首次编译后补齐，首构建、首104/105和第二107/108原回执保留。独立原生EDIT对照推翻“必须聚焦”假设，改有效可见顶层夹具而未改生产/原滚动状态硬期望；最终108新增/5,329全量通过，见[053](docs/roadmap/batches/053.md)。

- 2026-11-07 · 批次052：只新增008a/008b，纠正008中六个查询仍全待办的当前描述，完整父项保持部分。102新增/5,221全量及固定双目标Release通过；首93/102九例轨迹错误来自初始化Text setter及回调setter的合法虚getter读取，清初始化轨迹并保留回调末次读取，原字符/异常硬预期不改、生产不改。完整测量/default的真实依赖已核，不将PreferredHeight/DefaultSize或普通EDIT查询当完整布局/派生实现，见[052](docs/roadmap/batches/052.md)。

- 2026-11-07 · 批次051：005f只计固定词边界辅助，009a只计使用它的启用词删除命令，必要redraw辅助另归Control-14c。88新增/5,119全量及固定双目标Release通过；首轮85/88三个夹具预期错误已按固定无窗TextLength调用链纠正，原回执不改。完整003/005/009等父项保持部分，见[051](docs/roadmap/batches/051.md)。

- 2026-11-07 · 批次050：仅新增003d/005e，完整003/005/009保持部分。Copy/Cut/Paste直接转发真实HWND消息，测试截获命令以保护用户剪贴板；ShortcutsEnabled按实际固定14键表（CtrlJ而非摘要CtrlI）、父先调用/虚getter及只读精确对齐处理。custom菜单受限夹具仅证路由，完整启用编辑/菜单显示/派生/平台未验；77新增与5,031全量通过，见[050](docs/roadmap/batches/050.md)。

- 2026-11-07 · 批次049：只新增005d，Clear由Text=empty改为固定标准的虚Text=null，去掉无必要new（CLR签名/非virtual保持）。10项新增通过；006交叉引用Control-17e，不重复计数或关闭完整父项。首跑11/13中的两个失败来自重挂通知，不是Clear夹具；原回执及最终证据见[049](docs/roadmap/batches/049.md)。

- 2026-11-07 · 批次048：只新增005b/005c，完整005继续部分；AppendText参数名由value对齐标准text（旧value命名实参需改），nullable标记不改变CLR参数类型，null由异常改为no-op。首次38例37通过，一例误以为空撤销缓冲时EM_UNDO必返回0；独立原生对照与最后结果见[048](docs/roadmap/batches/048.md)，生产未为夹具改变。原任务/固定分母不变，剪贴板/完整编辑/测量和派生后端仍待。

- 2026-11-07 · 批次047：新增002b/002c/003c，完整002/003/005父项保持部分；原Lines空Text返回单个空行及不识别独立CR的行为按固定源码修正。新增SelectedText及必要MaxLength，无新公共概念。首次65例有3个夹具失败：AllowNull反射位置两例、较小输入限制下Undo期望一例，独立原生对照和最终结果见[047](docs/roadmap/batches/047.md)。StringArrayEditor元数据/Designer、完整编辑/测量和派生后端仍待；原ID/固定分母保留。

- 2026-11-07 · 批次046：新增002a/007a，002/004/007按真实子范围交叉引用，完整父项保持部分。首次40/44中三例错误要求原生EDIT保留公开WS_BORDER，另一例错误比较不同ReflectedType的MethodInfo对象；补匹配配置的独立EDIT，保留请求位硬断言并确认原生规范化后读回0，反射改核DeclaringType/Module/MetadataToken/签名。第二轮41/44暴露同三例启用Upper后误期待小写初值，改在重建前硬断言原生大写并核重建保持。原失败JSON/TRX保留，生产实现未为夹具改变；最终44/44。完整测量/编辑/派生后端仍待，见[046](docs/roadmap/batches/046.md)。

- 2026-11-07 · 批次045：005a补Modified/ModifiedChanged、EN_UPDATE和程序设文/销毁观测衔接，004/006交叉引用不重复新增事件任务。核固定源码纠正最初“EN_CHANGE后同步”理解：实际为EN_UPDATE分支；程序Text清native位后缓存由getter观测，不用setter提前通知。首次两例父Form无HWND而进入停车窗，补真实父句柄/映射硬断言后原期望通过，生产不为夹具改写。完整005/编辑/尺寸/派生后端仍部分，见[045](docs/roadmap/batches/045.md)。

- 2026-11-07 · 批次044：003b补标准HideSelection设置/通知/原生样式及状态恢复；004交叉引用003b，不重复创建事件任务。RichTextBox保留独立存值且仅显式new，基类引用与派生引用状态不同的现状明确披露；BorderStyle/其余编辑/尺寸和真实派生后端仍待，见[044](docs/roadmap/batches/044.md)。

- 2026-11-07 · 批次043：003a补共同设置标准事件、真实WordWrap与布局事务；移除基类强制纵条，由TextBox.ScrollBars决定实际条样式。旧WordWrap“仍存值”当前描述已纠正；高度/完整CreateParams/测量/编辑及派生后端保持缺口，不将17项回归当整基类或平台验收，见[043](docs/roadmap/batches/043.md)。

- 2026-11-07 · 批次042：新增001a，001～009依据实际迁入的旧实现由全待办改部分，保留全部原ID/未完能力。基类存在与TextBox迁移不关闭RichTextBox/MaskedTextBox或任何完整编辑任务；旧存值/高度/文本语义差异保留。固定API库存仍是10.0.11历史快照，未改1,095类型/61,240成员行；构建/程序消息不等于AOT/Designer/设备验收，见[042](docs/roadmap/batches/042.md)。

- 2026-11-07 · 批次039：现有TextBox消费CanEnableIme/ImeModeBase、ReadOnlyChanged及必要密码同步，但TextBoxBase类仍不存在，001～011/CLOSE全部原状态和ID保留；标准共享编辑/继承恢复另列，见[039](docs/roadmap/batches/039.md)。

- 2026-11-07 · 批次038：TextBox-003c已修复既有TextBox无句柄Select丢状态，并补多行切换的有限句柄/选择/原生modified保存。本页“现有Select直接返回”的当前描述按真实代码纠正；TextBoxBase本身仍缺失，全部原ID/分母/未完任务保留，见[038](docs/roadmap/batches/038.md)。

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 2026-11-07 · `bd9f4d2` · 批次042新增TextBoxBase共享EDIT首范围；此前“尚无该类型实现文件的提交历史”随源码存在已失效，本批按实际Git记录纠正。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

#### docs/roadmap/controls/ToolStrip.md

- 2026-10-16 · 批次022：05c测量使用有效字体与每控件HFONT，移除临时/父共享字体分支；继承变化经标准布局消费。08按已有真实宿主组合更正为部分，全部item契约/overflow/Renderer与平台仍缺，见[022](docs/roadmap/batches/022.md)。

- 2026-10-16 · 批次021：父级DefaultLayout统一定位，Dock内部事务按实际方向/几何提交并在完成后DockChanged；只读标准Orientation消费Dock及固定横竖LayoutStyle，已有host按方向排列。直属/嵌套/取消Dock有参考；完整Style事件、overflow/所有item测量另列。 见[021](docs/roadmap/batches/021.md)，父任务仍部分；020历史条带定位/无首选测量描述只代表当时状态。

- 2026-10-16 · 批次020：标准事务接入现有宿主布局并保留构造期安全；内部LayoutInParent只供直属Form条带消费，没有新增公共替代API。完整ToolStrip继承/溢出/Owner/双向同步未关闭。 见[020](docs/roadmap/batches/020.md)；父任务保持部分完成。

- 2026-10-16 · 批次018必要集成修复：ControlCollection开始同步请求布局后，EnsureItemHosts在替换宿主时会重入并递归释放，首次全套测试出现栈溢出。现用重入保护及先移除旧映射再释放/创建，释放中不再重建宿主；原始失败日志保留，修复后既有测试通过。未扩展ToolStrip公开API，完整宿主双向同步、布局和Owner任务仍保持未完成。

- `ed64510`（2026-04-08）：改进下拉状态管理和菜单导航。
- `275c479`（2026-04-08）：文本布局与 DPI 支持调整。
- `83e9356`（2026-04-08）：菜单图像资源相关改动涉及宿主。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

#### docs/roadmap/controls/UserControl.md

- 2026-11-07 · 批次039：UserControl.cs未改，继续继承有限Control IME路径；未以非Form根消息测试关闭HostedInWin32DialogManager、完整输入或AutoSize/共享约束，父任务保持原状态，见[039](docs/roadmap/batches/039.md)。

- 2026-11-07 · 批次038：02d接入标准AutoSize覆盖与事件公开，复用共享状态/布局链。源码审计确认固定UserControl无独立OnLayout/GetPreferredSize算法，不为补属性而新增布局覆盖；完整AutoSize/复杂约束和Designer往返仍归02/04。检查结果见[038](docs/roadmap/batches/038.md)。

- 2026-11-07 · 批次037：05a按固定10.0.11接入OnMouseDown/WM_SETFOCUS，7项真实HWND回归通过；完整HostedInWin32DialogManager、输入/验证/AutoSize、Designer/AOT/设备和平台仍待，见[037](docs/roadmap/batches/037.md)。

- 2026-11-07 · 批次035：02c修复历史数值反置，并显式保留基础Control的GrowOnly默认；UserControl.cs按名字实现本已正确，本批不重复修改。03父项仍全待办遗漏032的03a，04遗漏已有内容测量/基础布局，按真实局部实现纠正为部分并保留剩余范围。旧持久化整数和已编译枚举常量须复核；按名字源码重编译保持名字语义。10项限定回归通过，不关闭完整AutoSize，见[035](docs/roadmap/batches/035.md)。

- 2026-11-07 · 批次034：完成02b默认150×150、GrowOnly名字默认、AutoSizeMode合法值/布局通知和BorderStyle参数/真实HWND样式更新。CompatibilityEnums中AutoSizeMode数值身份与官方相反未修改，明确保留父项；完整AutoSize、Designer、AOT与平台仍待，见[034](docs/roadmap/batches/034.md)。

- 2026-10-26 · 批次032：完成03a Load一次触发生命周期；完整Designer/resx、BorderStyle、验证/键盘、AOT和平台仍待，见[032](docs/roadmap/batches/032.md)。

- 2026-10-26 · 批次031：完成01a标准ContainerControl继承和02a共享容器属性首范围；Load、BorderStyle、Designer/resx、键盘/验证和平台仍待，见[031](docs/roadmap/batches/031.md)。

- `70a08e7`（2026-04-09）：加入 UserControl 自动缩放相关属性。
- `b8a98eb`（2026-04-09）：加入 UserControl 与资源/主题管理支持。
- 2026-10-15：新增本控件评估与分平台待办；本轮仅规划，未实现上述任务。

#### docs/roadmap/controls/WindowsFormsSynchronizationContext.md

- 2026-10-16 · 批次025：同步上下文的native WindowProc按callback、默认过程、`WM_NCDESTROY`清理分层处理，保留每个异常并保证native边界不被managed异常穿透；完整退出/模态/多UI竞态仍未关闭，见[025](docs/roadmap/batches/025.md)。

- 2026-10-16 · 批次016历史回标：任务06的默认安装/还原首范围明确交叉引用007已完成的01b，删除笼统“默认生命周期待后续”；完整退出资源/模态/多UI/关闭竞态仍缺，父项保留部分完成，未新增验收。

- 2026-10-15：增加评估和待办；本轮未实现运行库功能。
- 2026-10-16 · 批次 006：先运行固定 WinForms 10.0.11，再实现显式标准类型；57 条参考/JIT/实际 Native AOT 观测一致，新增 7 项独立 STA 轻量回归。每线程共用 message-only HWND，通过现有 dispatcher 保留 FIFO、ExecutionContext、state、原异常与 ThreadException；成功建窗登记 DPI 门禁，WM_NCDESTROY 关闭队列并释放 GCHandle。普通 Form 关闭不销毁此目的地。按参考端保留共享释放：本实例 Dispose 后投递无动作，其他 copy/同线程构造实例仍持失效目的地并抛 InvalidOperationException；新构造不会修复失效 slot，已接受未执行回调丢弃。默认安装/还原不随显式安装检查关闭。

明确差异：参考端活跃 context 的 Send(null) 在独立进程 3 秒超时，本实现沿用防挂守卫抛 ArgumentNullException（参数 d）；Post(null) 两端均接受且不调用回调。此差异单列，不把挂起复制为支持；完整默认错误 UI、线程退出、跨线程释放、多 UI 线程和模态竞态仍待验。

- 2026-10-16 · 批次 007：标准默认安装首范围完成，204 条固定参考/JIT/实际 AOT 观测相同，18 项独立 STA 回归通过。AutoInstall 默认 true 且线程局部；null/base Current 安装，custom 保留；Uninstall 关闭自动安装但不 Dispose 目的地，已关闭时保留 Current，循环退出还原时保留策略。dispatcher 在 ExecutionContext 内使用目的 UI Current 并 finally 还原。无 Form ExitThread 暴露旧 DeactivateActCtx SEH，修正为最外层循环退出后释放本线程激活资源，顺序不同 STA 循环对照通过。完整 ApplicationContext/模态、多 UI 退出和竞态不随此关闭，见 [批次 007](docs/roadmap/batches/007.md)。

### Added

- Batch 069: implement RichTextBox Protected notifications and native protection vetoes, including allowed protection/full-document changes and protected stream error consumption. Carry WM_NOTIFY results through Form, containers and parking receivers, routing Form notifications by source HWND. 96 new and 1048 related tests pass; the fixed dual-target Release build has 0 errors/2 existing NU1510 warnings. Full LinkClicked/notifications/editing, MaskedTextProvider and platform acceptance remain pending; batch 064 failures and batch 065 timeout remain unresolved.

- Batch 068: add RichTextBox SelectionChanged from exact native ranges/types with cache-before-event deduplication, reset on handle creation and the fixed Hangul composition/MaxLength workaround. Add HScroll/VScroll/ImeChange with creation guards and fixed pre/post-default ordering. All 107 new and 952 related tests pass, and the fixed dual-target Release build has 0 errors/2 existing NU1510 warnings. Full notifications/editing, physical IME/scrolling, MaskedTextProvider and platform acceptance remain pending; batch 064 failures and batch 065 timeout remain unresolved.

- Batch 067: add the standard RichTextBox Font override with native default-format comparison, all-text formatting and resolved-font readback; add ContentsResized and its temporary suppression during the base font update. Route notifications from owned parking receivers by source HWND. Keep the fixed Drawing font-family identity, HFONT size quantization and native automatic-foreground/offset-reset behavior. Validation and retained initial fixture failures are recorded in batch 067; full rich editing, ambient/DPI, MaskedTextProvider and platform acceptance remain pending.

- Batch 066: synchronize RichTextBox ForeColor through native default-format reads and SCF_ALL writes before accepting managed changes; submit BackColor before its notification and initialize colors before text/RTF. Keep the existing Rich theme refresh consistent with the effective BackColor. Validation is recorded in batch 066; full Font, rich editing, MaskedTextProvider and platform acceptance remain pending. Retain batch 064 full-suite failures and batch 065 timeout.

- Batch 065: add standard SelectionFont using native character formats, fixed mixed-face/null and mixed-size/style semantics, point/twip conversion and a private blittable LOGFONT path. Release and all 93 new cases pass. Retain the initial 77/85 run: eight uniform-font fixtures selected mixed Latin/CJK runs; eight independent RichEdit checks confirm the missing FACE mask and correct null result. Correct only the new uniform-text fixture and retain the original mixed-script checks. Full Font/color synchronization, other rich editing APIs, MaskedTextProvider and platform acceptance remain pending. Batch 064 full-suite failures/timeouts remain unresolved; batch 065 full-suite evidence is recorded separately.

- Batch 064: add the standard SelectionType/flags enum and RichEdit-specific character index, position and physical-line queries. Preserve 32-bit POINTL values, end-of-text positions, virtual calls, handle creation and native results. All 101 new cases and the fixed dual-target Release build pass. Retain both initial 100/101 runs: correct only new long-paragraph physical-line and cross-window layout assumptions while keeping the 70001 index and native round trips. Full-suite validation remains unresolved: one completed run passed 6,145/6,146 with an existing UI callback timeout; two other runs timed out, the last reporting an existing click-test failure. Their groups pass independently (18/18 and 23/23); no cause is inferred and no production or old tests were changed for these failures. Full fonts, files, notifications/OLE, Find/selection/measurement, MaskedTextProvider and platform acceptance remain pending.

- Batch 063: add native SelectionBullet, cached BulletIndent and SelectionTabs with fixed paragraph masks, validation, screen-DC conversions and GET-before-SET preservation of unrelated fields and trailing slots. All 86 new cases and 6,045 tests pass; the fixed dual-target Release build has 0 errors and 2 existing NU1510 warnings. Retain the first 85/86 run: only correct the new AllowNull test to inspect the setter parameter, leaving production unchanged. Full paragraph behavior, fonts, files, Protected/OLE, notifications/coordinates, MaskedTextProvider, Designer, Native AOT runtime and platform acceptance remain pending.

- Batch 062: add native SelectionCharOffset and SelectionProtected properties and native SelectionIndent, SelectionHangingIndent and SelectionRightIndent. Preserve validation before handle creation, screen-DC pixel/twip conversion, precise packed payloads, per-assignment native changes, mixed-format reads and RTF recreation. All 110 new cases and 5,959 tests pass on their first runs; the fixed dual-target Release build has 0 errors and 2 existing NU1510 warnings. Full Protected notifications/rejected changes, font formatting, bullets/tabs, file/OLE/coordinate APIs, MaskedTextProvider, Designer, Native AOT runtime and platform acceptance remain pending.

- Batch 061: replace stored SelectionColor with native character-format reads and writes, add SelectionBackColor with its standard pre-handle cache, and add native paragraph SelectionAlignment. Preserve packed, blittable payloads, selection-specific masks, mixed-format results, creation order and RTF restoration. All 67 new cases and 5,849 tests pass on their first runs; the fixed dual-target Release build has 0 errors and 2 existing NU1510 warnings. Full character/paragraph formatting, files, Protected/OLE, notifications, coordinates, MaskedTextProvider, Designer, Native AOT runtime and platform acceptance remain pending.

- Batch 060: add standard Rtf/SelectedRtf properties backed by native RTF streams, and preserve the latest rich document formatting across handle recreation. Keep per-call AOT-friendly callbacks, code-page conversion, stream errors, selection, configured limits and Modified state. All 54 new cases and 5,782 tests pass; the fixed dual-target Release build has 0 errors and 2 existing NU1510 warnings. Retain the first unsafe-test compile failure and 50/54 run: fix the real stale Modified restoration for a newly assigned plain-text cache, and correct only the empty-selection fixture after an independent native stream confirms zero bytes. Full file APIs, public character/paragraph formatting, Protected/OLE, notifications/coordinates, MaskedTextProvider, Designer, Native AOT runtime and platform acceptance remain pending.

- Batch 059: stream RichTextBox plain Text and SelectedText as Unicode, restore configured input limits, and read logical text/length/selection through RichEdit messages. Preserve plain text, selection and Modified during handle recreation with static AOT-friendly callbacks and packed native structures. Final validation passes 64 new cases and all 5,728 tests; the fixed dual-target Release build has 0 errors and 2 existing NU1510 warnings. Retain the first missing-constant build and 61/64 run, including the real Modified defect and two new fixture corrections. Update the previous WM_SETTEXT test to keep its native limit-growth counterexample while requiring the new streamed path to restore the configured limit. Full RTF/format persistence, dedicated notifications/coordinates, MaskedTextProvider, Designer, Native AOT runtime and platform acceptance remain pending.

- Batch 058: move RichTextBox directly onto TextBoxBase and create a real system RICHEDIT50W backend. Restore the required rich defaults, shared HideSelection, extended text limits, native scroll-bar settings, URL detection and word-selection messages. Final validation passes 96 new cases and all 5,664 tests; the fixed dual-target Release build has 0 errors and 2 existing NU1510 warnings. Preserve the first message-type compile failure and the 94/96 fixture run; independent RichEdit windows confirm WM_SETTEXT limit growth and nonzero BOOL results. The dedicated Text/StreamIn, logical length/selection, RTF, formatting, MaskedTextProvider, Designer, Native AOT and platform acceptance remain pending. Existing tests are unchanged.

- Batch 057: read TextBoxBase.TextLength directly from the native window when a handle exists, while retaining the virtual Text path before creation. Add the standard default event/binding declarations, hidden selection metadata, editing property/event categories and shared Paint forwarding. All 41 new cases and 5,568 tests pass on the first run; the fixed dual-target Release build has 0 errors and 2 existing NU1510 warnings. Existing tests remain unchanged. Designer round-trips, binding workflows, visible painting, physical input, derived editing backends, Native AOT and platform acceptance remain pending.

- Batch 056: restore the TextBoxBase constructor's four native-edit style flags and its dedicated left mouse-up click/double-click dispatch, with the double-click marker set before the base window procedure. Redeclare the required image, padding, MouseClick and DoubleBuffered members with standard visibility metadata and shared base storage/events. All 46 new cases and 5,527 tests pass on the first run; the fixed dual-target Release build has 0 errors and 2 existing NU1510 warnings. Both independent WindowFromPoint checks report no hit, so only negative routing is validated; positive visible clicks, physical input, remaining metadata, derived backends, Designer, Native AOT and platform acceptance remain pending.

- Batch 055: measure TextBox scroll-bar insets before and after the shared text measurement, using the control DPI only on PerMonitorV2 threads. Match the fixed EDIT creation request by removing the forced multiline ES_WANTRETURN flag and setting ApplyThemingImplicitly before the base parameter projection. Preserve the existing no-handle ScrollBars cache boundary and managed AcceptsReturn rules. All 68 new cases and 5,481 tests pass; the fixed dual-target Release build has 0 errors and 2 existing NU1510 warnings. Retain the initial 67/68 run and correct only its fixture assumption about ReadOnly/IME/PasswordChar creating a handle. Full scaling, click behavior, derived backends, Designer, Native AOT and platform acceptance remain pending.

- Batch 054: implement standard TextBoxBase PreferredHeight and 100-pixel default width, independent AutoSize=true and requested-height lifetimes, and preferred-size caching. Remove the legacy 24/48 native height clamp; preserve free/multiline heights through font, padding, creation and AutoSize changes. Add the required per-control DPI observation and native border metrics. Table/Flow layout now reads the separate common AutoSize state, preserving assigned text-box widths. Final validation passes 84 new cases and all 5,413 tests; the fixed dual-target Release build has 0 errors and 2 existing NU1510 warnings. Retain the first full run's six failures: one real table consumer defect and five old fixed-geometry fixture assumptions. Full scaling, cross-monitor DPI events, derived backends, Designer, Native AOT and platform acceptance remain pending.

- Batch 053: add the six standard TextRenderer.MeasureText overloads without a device context, TextFormatFlags and the required Padding.Size/DPI snapshot support. TextBoxBase now measures preferred size through real GDI and defers ordinary EDIT caret scrolling until handle creation. All 108 new cases and 5,329 full-suite cases pass; the fixed dual-target Release build reports 0 errors and 2 existing NU1510 warnings. Preserve the initial missing-Padding.Size build failure, the 104/105 fixture run and the 107/108 diagnostic run. An independent EDIT reports first line 0 when hidden and 37 when visible, with or without focus; the corrected Lumina fixture reports 37 without changing production for the test failures. Full AutoSize/default/cache lifetimes, derived backends, designer, Native AOT and platform acceptance remain pending.

- Batch 052: add the six standard TextBoxBase line and character-position queries using native EDIT messages, preserving virtual dispatch, parameter validation, lazy handles, text snapshots and signed coordinate semantics. All 102 new cases and 5,221 full-suite cases pass; the fixed dual-target Release build reports 0 errors and 2 existing NU1510 warnings. Preserve the initial 93/102 run and its nine fixture trace errors; production code did not change for those failures. Full sizing, deferred caret scrolling, derived backends, designer and platform acceptance remain pending.

- Batch 051: implement the fixed v10.0.11 backwards word boundary and enabled Ctrl+Backspace/Ctrl+Shift+Backspace deletion through native undoable selection replacement. Add the required internal nested redraw helpers without changing the public API. All 88 new cases and 5,119 full-suite cases pass; the fixed dual-target Release build reports 0 errors and 2 existing NU1510 warnings. Preserve the initial 85/88 run and its three fixture expectation errors. Clipboard data, menus, visible redraw, full sizing, derived editing backends, designer and platform acceptance remain pending.

- Batch 050: add standard nonvirtual Copy/Cut/Paste command entrypoints and virtual ShortcutsEnabled with the fixed disable list, parent-first routing, read-only alignment guards and native context-menu suppression. Keep attached application menus on the existing route. All 77 new cases and 5,031 full-suite cases pass; the fixed dual-target Release build reports 0 errors and 2 existing NU1510 warnings. Clipboard messages are intercepted in tests without accessing the system clipboard; actual clipboard data, custom-menu display, enabled Ctrl+Backspace word deletion and platform acceptance remain pending.

- Batch 049: retain an original parking notification receiver until its native control is destroyed, and reflect Form control commands by source HWND after reparenting, including numeric ID collisions. An independent native EDIT confirms that SetParent preserves the original command receiver. Clear now passes null directly to the virtual Text setter. All 18 new cases plus 8 existing cases pass (26/26), the full suite passes 4,954/4,954, and the fixed dual-target Release build reports 0 errors and 2 existing NU1510 warnings. The original 11/13 run retains both real reparenting failures; designer, Native AOT, physical input and platform acceptance were not run.

- Batch 048: standard nullable AppendText now uses the existing selection/replacement path and restores the original selection for zero width or height. Add public CanUndo, Undo and ClearUndo with native EDIT semantics. A bounded internal parking-window command callback restores editing notifications while controls have no native parent. Notifications after an EDIT is reparented from parking to a Form remain an open Control/TextBoxBase gap.

- Batch 047: standard TextBoxBase.SelectedText with native replacement, null deletion, undo/modified clearing and input-limit restoration; Lines now returns no entries for empty text and recognizes CR, LF and CRLF. The required MaxLength property validates its standard default and updates real EDIT input limits without recreating handles or truncating existing/programmatic text. Public undo/clipboard APIs, AppendText parity, full sizing and derived editing backends remain open.

- Batch 046: standard TextBoxBase.BorderStyle/BorderStyleChanged with native border styles, handle recreation and conditional parent layout; SelectAll and DeselectAll now share the existing selection path through SelectInternal. Native system-class registrations now distinguish the source module/procedure resolved in the current activation context, preventing cross-STA EDIT superclass reuse across native versions. Full preferred-size behavior, SelectedText, undo/clipboard and derived editing backends remain open.

- Batch 045: standard TextBoxBase.Modified/ModifiedChanged with native observation and EN_UPDATE synchronization, plus the internal WindowText submission boundary needed to suppress programmatic EDIT notifications while preserving edits from TextChanged handlers. TextBox.CharacterCasing now validates its standard enum, recreates the handle and uses native upper/lowercase styles; no custom conversion or extra event is introduced. Full shared editing, sizing and derived backends remain open.

- Batch 044: standard TextBoxBase.HideSelection notifications and native EDIT selection style, plus TextBox.TextAlign notifications, handle recreation and creation alignment styles. Horizontal scroll bars now also require logical Left alignment. HorizontalAlignment is corrected to Left=0, Right=1, Center=2; existing binaries and persisted integer values 1/2 need review. RichTextBox keeps its existing separate HideSelection storage through an explicit new declaration.
- Batch 043: standard AcceptsTabChanged notifications and WordWrap handle recreation/cache invalidation/conditional parent layout, plus validated TextBox.ScrollBars with native multiline scroll styles. WordWrap now changes actual EDIT wrapping, and ScrollBars.None no longer forces a vertical scroll bar. Other alignment, preferred-size and derived backend work remains open.
- Batch 042: the standard abstract TextBoxBase now owns the existing shared EDIT text, selection, multiline/read-only, keyboard and handle-state behavior; TextBox directly inherits it and keeps its password/Return rules and legacy SelectionChanged extension. RichTextBox/MaskedTextBox direct inheritance, complete editing APIs and native backends remain open.
- Batch 041: standard Application.OleRequired with one cached OLE initialization per thread, balanced owner-thread release on explicit ExitThread or the outer message-loop exit, and the required InputLanguage Current getter/setter precondition. Existing STA/OLE references and external MTA initialization remain owned by their callers. Other failed HRESULTs propagate instead of being reported as STA; full thread-context and modal-host lifetimes remain open.
- Batch 040: native thread keyboard-layout services through the standard InputLanguage/InputLanguageCollection and Application.CurrentInputLanguage APIs, plus standard Form input-language event contracts and descendant Control notification/cancellation. The fixed WinForms OLE initialization side effect and full modal/IME behavior remain open; MUI buffer growth is bounded to one retry with native-failure fallback.
- Batch 039: TextBox read-only/password IME restrictions, effective mode with preserved configured mode, native password character readback and system-password handle recreation, standard ReadOnlyChanged, and bounded Control input-language message/non-Form host propagation. Form input-language events, the public InputLanguage service and modal propagation remain open.
- Batch 038: standard effective ImeMode contracts and bounded IMM context ownership/readback, TextBox Multiline handle recreation with pending selection and native modified-state preservation, and UserControl AutoSize declarations and event forwarding. Full IME input, TextBoxBase and AutoSize remain open.
- Batch 037: bounded IME character dispatch and duplicate KeyPress suppression, TextBox input-key/dialog routing using existing AcceptsReturn/AcceptsTab settings, and UserControl native focus entry with nested validation/navigation coverage. Full IME composition, TextBoxBase inheritance and AutoSize remain open.
- Batch 036: native `WM_GETDLGCODE` input-key and input-character classification, `WM_SYSCHAR` dialog routing, bounded queued character suppression for `SuppressKeyPress`, validation-cancellation cleanup across parent controls, and Form default-button disposal cleanup. Button native-click validation was audited against WinForms 10.0.11 and covered by real HWND regressions without duplicating validation in `Button.cs`.
- Batch 035: queued keyboard messages now call Control preprocessing before translation/dispatch. Forms route Enter/Escape through standard accept/cancel button contracts, and Button consumes validation policy and writes DialogResult before Click. ButtonBase and modal-loop compatibility remain open.
- Batch 034: key-event dispatch now honors parent preview and Handled, nested containers yield Tab at their boundaries, forms support KeyPreview and top-level Tab wrapping, and UserControl gains bounded default-size and border-style behavior.
- Initial bounded FND-019 keyboard pipeline: `KeyDown`, `KeyPress`, and `KeyUp` event contracts plus Win32 key-message dispatch in `Control`.
- Bounded foundation/inheritance slices: `RightToLeft` inheritance and Win32 RTL styles, `ScrollableControl` auto-scroll state and protected contracts, `ContainerControl.ParentForm`/`AutoScaleFactor`, and the standard `UserControl : ContainerControl` chain.
- Panel now consumes the shared `ScrollableControl` chain; `UserControl.Load` has a one-time creation lifecycle; Windows scroll messages update the shared scroll state and native SCROLLINFO within the bounded batch scope.
- Bounded keyboard and scrolling slices: `Control` now exposes the standard PreviewKeyDown and preprocessing chain, `ContainerControl` routes Tab/Shift+Tab and descendant mnemonics, and `ScrollableControl.ScrollControlIntoView` accounts for existing offsets in nested content.

### Verification

- Batch 048: Release fixed dual-target builds pass with 0 errors and 2 existing NU1510 warnings; 43 new tests plus 3 existing behavior tests pass, and all 4,936 full-suite tests pass without failures/skips. Original fixture, full-suite regression, parking/reparent diagnostics and an output-limit interruption are retained. Independent EDIT comparisons establish the observed empty-buffer EM_UNDO return and limited Undo behavior. The passing scope covers notifications while parked and reparenting state/lifetime; it does not establish editing notifications after reparenting. Designer, Native AOT, physical input and platform acceptance were not run.

- Batch 047: Release fixed dual-target builds pass with 0 errors and 2 existing NU1510 warnings; all 65 new and 4,893 full-suite tests pass without failures/skips. The first new run passed 62/65: two fixtures read AllowNull metadata from the property instead of the setter parameter, and another expected Undo to ignore a restored MaxLength. An independent EDIT confirms the bounded Undo result. Original JSON/TRX receipts are retained; production behavior was not changed to satisfy these fixture errors. Designer, Native AOT, physical input and platform acceptance were not run.

- Batch 045: Release fixed dual-target builds pass with 0 errors and 2 existing NU1510 warnings; final 45 new, 892 related and 4,782 full-suite tests pass without failures/skips. The first new run passed 43/45 because two fixtures created a child before its parent HWND, leaving native notifications at a parking window. Original receipts are retained; the corrected fixtures assert the actual parent HWND and managed mapping without changing production behavior or event expectations. Designer, Native AOT, physical input and platform acceptance were not run.

- Batch 044: Release fixed dual-target builds pass with 0 errors and 2 existing NU1510 warnings; final 42 new, 2,356 related and 4,737 full-suite tests pass with no failures/skips. The first new run passed 38/42: four RTL native fixtures lacked the existing Control creation-time style resubmission. Original receipts are retained; the corrected fixture verifies that pipeline without claiming full RTL/mirroring or visual parity. Designer, Native AOT, physical devices and platform acceptance were not run.
- Batch 043: the first Release fixed dual-target solution build passes with 0 errors and 2 existing NU1510 warnings; all 41 new, 359 related and 4,695 full-suite tests pass without failures or skips. Independent native EDIT comparisons cover 16 style combinations and native line counts verify wrapping. Designer, Native AOT, physical devices and platform acceptance were not run.
- Batch 042: Release fixed dual-target solution build passes with 0 errors and 2 existing NU1510 warnings; 19 new tests, 318 related tests and all 4,654 tests pass with no failures or skips. Eleven message tests verify native IME default dispatch and existing character state without changing production Control code. Full composition/candidate UI, modal hosts, designer, Native AOT and platform acceptance remain open.
- Batch 041: Release dual-target solution build passes with 0 errors and 2 existing NU1510 warnings; 18 new tests, 148 related tests and all 4,635 tests pass. The first 9 internal-service fixture failures assumed CLR threads had no COM initialization; original receipts are retained and only that fixture was corrected. Native OLE reference probes and real nested message loops verify the bounded lifetime; full host-context, physical IME, designer, Native AOT and platform acceptance remain open.
- Batch 040: Release dual-target solution build passes with 0 errors and 2 existing NU1510 warnings; 27 new tests, 96 related IME/focus tests and all 4,617 tests pass with no failures or skips. Native HKL readback, available-layout setters/default reset, registry MUI names, thread isolation and real HWND notifications/cancellation are covered. Physical language switching/composition, OLE lifetime, designer, Native AOT and platform acceptance remain open.
- Batch 039: Release dual-target solution build passes with 0 errors and 2 existing NU1510 warnings; 22 new tests, a 50-test focus/IME regression set and all 4,590 tests pass. Original compilation/fixture failures are retained; native multiline EDIT readback is compared with an independent native control, and the historical no-handle focus fixture now isolates its uninitialized propagation cache. Physical IME, designer, Native AOT and platform acceptance were not run.
- Batch 038: Release dual-target solution build passes with 0 errors and 2 existing NU1510 warnings; 32 new tests and all 4,568 tests pass. Initial uint tuple inference compilation errors were corrected, and simultaneous handle-notification/IME-release failures retain both errors. Physical IME input, designer, Native AOT and platform acceptance were not run.
- Batch 037: Release dual-target solution build passes with 0 errors and 2 existing NU1510 warnings; 22 new tests and all 4,536 tests pass. Physical input, designer, Native AOT and platform acceptance were not run.
- Batch 036: Release solution build passes with 0 errors and 2 existing NU1510 warnings; 9 new tests and all 4,514 tests pass. Existing keyboard queue/event regressions add 14 passing tests. Fixed 10.0.11 API inventory, designer, Native AOT, physical input and platform acceptance remain open.
- Batch 035: Release solution build passes with 0 errors and 2 existing NU1510 warnings; 37 new tests and all 4,505 tests pass. Fixed 10.0.11 API inventory remains blocked; designer, Native AOT, physical input and platform acceptance were not run. Initial missing Keys.Return compilation and malformed KeyUp fixture failures are recorded in the batch notes.
- Batch 034: Release solution build passes with 0 errors and 2 existing NU1510 warnings; 29 new tests and the full 4,468-test suite pass. Fixed 10.0.11 API inventory remains blocked by the missing reference pack; AutoSizeMode numeric identity, full input/dialog behavior, AOT, designer and platform acceptance remain open.
- Release solution build and the full `Lumina.Tests` suite pass for the 033 scope (4,439 tests); complete command/dialog routing, IME, physical keyboard/wheel/touch input, Cursor, Native AOT, designer, and cross-platform gates remain open.

### Changed

- Correct the batch 048 assumption that a parking window can be destroyed immediately after SetParent. The original receiver stays private and is released when its native control is destroyed or recreated. The existing two-TextChanged contract is preserved. Full original Form receiver lifetime and WM_NOTIFY reflection remain open.
- Clear keeps its nonvirtual CLR signature and now forwards null, rather than an empty string, to overridden Text setters as required by the fixed WinForms baseline.

- AppendText's parameter name is aligned from value to the standard text. Existing named calls using value must use text; its CLR string parameter type is unchanged. Null now follows WinForms and performs no operation.

- Correct AutoSizeMode numeric identity to GrowAndShrink=0 and GrowOnly=1 while preserving the existing named GrowOnly default. Previously compiled enum constants and persisted numeric 0/1 values require review; source using enum names retains the intended named behavior after recompilation.

### Historical effects scope

The former planned effects list (DWM injection, backdrop/blur effects, Direct2D UI, XML configuration and languages) belongs to the separate effects line; it is not a LuminaForms delivery claim or active compatibility milestone. See the [repository decisions](docs/roadmap/project-consolidation.md) and [effects documentation](docs/zh/blog/index.md).

---

## [0.1.0] — 2026-03-25

### Added
- Initial project structure: `Lumina.App` (AOT WinExe) + `Lumina.Ext` (AOT Shared DLL)
- Solution file (`Lumina.slnx`)
- Native AOT configuration for both projects (net10.0-windows, x64)
- `InlineHook.cs` — x64 inline hook engine using VirtualProtect trampoline, no minhook dependency
- `NativeMethods.cs` — Win32 and DWM P/Invoke definitions (`dwmapi.dll`, `kernel32.dll`, `user32.dll`)
- `OsVersion.cs` — Windows build number detection helper
- `ExtMain.cs` — Native DLL entry point with `[UnmanagedCallersOnly]` DllMain export
- `.gitignore` based on Visual Studio template with AOT `native/` entry
- `README.md`, `ROADMAP.md`, `CHANGELOG.md`
