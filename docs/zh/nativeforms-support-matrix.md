---
title: "LuminaForms 支持矩阵"
permalink: /zh/nativeforms-support-matrix/
layout: default
---

# LuminaForms 支持矩阵

更新于 2026-10-15，依据 [项目重新评估](luminaforms-reassessment-2026-10.md) 和 [运行/API 证据](../assessments/2026-10-15/README.md)。基线为 .NET 10 WinForms。本页中的“部分实现”不能解释为整个控件或全部成员已兼容。

## 状态口径

- **部分实现**：已有实际代码，仍存在 API 或行为缺口。
- **仅声明/局部逻辑**：存在类型或属性，但关键原生行为尚未接通。
- **未实现**：本次清点未发现对应类型。
- **已验证场景**：仅指明确列出的测试或发布结果，不扩大为全框架支持。

平台、AOT 和设计器验证单独记录；不以同名类型数计算完成度。

逐控件的属性、事件、方法、行为、三平台方案和修改历史见 [细化路线图](../roadmap/README.md)，完整成员与接口清单在其附件。目标口径见 [98%验收](../roadmap/coverage.md)，当前没有全量逐项证据可宣布已达标。

## 控件与组件

| 类型/控件族 | 当前状态 | 关键边界 |
| --- | --- | --- |
| Application / ApplicationConfiguration | 部分实现 | 基础启动与主题；HighDpiMode 为空实现，消息/线程/应用上下文待对齐 |
| Form / Control | 部分实现 | Form 尚未继承 Control；Component/容器体系与标准接口不完整 |
| Button / Label / TextBox | 部分实现 | 原生基础可用；基类、输入、测量、默认值及部分属性行为待补 |
| CheckBox / RadioButton | 部分实现 | 三态、分组、键盘和事件行为仍有缺口 |
| ComboBox / ListBox | 部分实现 | 部分集合与选择；绑定、多选/编辑、完整事件待补 |
| GroupBox / Panel / UserControl | 部分实现 | 嵌套与部分布局；滚动、验证、焦点及继承关系待补 |
| FlowLayoutPanel / TableLayoutPanel | 部分实现 | 有实际布局逻辑；自动尺寸、跨度/占位及完整扩展成员待补 |
| SplitContainer / Splitter | 部分实现 | 有拖动和布局逻辑；SplitterPanel 类型、键盘/事件/DPI 待对照 |
| TabControl / TabPage | 部分实现 | 页签选择和布局；TabPages、选择取消等成员待补 |
| PictureBox | 部分实现 | 图像与缩放有实际实现；AOT 资源、所有权、异步/动画等未完整验证 |
| ProgressBar | 部分实现 | 原生 Value；范围、Step、Style/Marquee 等待补 |
| DateTimePicker / MonthCalendar | 仅声明/局部逻辑 | 有原生窗口和主题；日期选择读写及事件未贯通完整模型 |
| LinkLabel / MaskedTextBox / NumericUpDown | 仅声明/局部逻辑 | 链接、掩码、数字增减/解析/范围与事件尚不等价 |
| RichTextBox | 仅声明/局部逻辑 | 当前为普通 Edit，未实现完整 RichEdit/RTF/选区格式 |
| TreeView / TreeNode | 仅声明/局部逻辑 | 有原生窗口与托管 Nodes，但本次探针节点未进入原生树 |
| ListView / ListViewItem / ColumnHeader | 仅声明/局部逻辑 | 项构造有确定性异常；列/项原生同步及大部分行为待补 |
| ToolStrip / MenuStrip / StatusStrip 及条目 | 部分实现 | 有菜单/宿主/布局；继承体系、溢出、快捷键、双向事件待补 |
| ContextMenuStrip | 部分实现 | 当前为 Component，未对齐官方 Control 继承及完整成员 |
| 文件/目录/颜色/字体对话框、MessageBox | 部分实现 | 有原生调用；CommonDialog 等基类、owner、重载及默认值待补 |
| NotifyIcon | 部分实现 | 真实托盘实现；恢复、气球、生命周期和事件待完整验证 |
| ToolTip | 仅声明/局部逻辑 | 当前仅保存和读取关联文本，未见实际显示实现 |
| DataGridView / PropertyGrid / BindingNavigator | 未实现 | 表格/属性编辑/绑定是后续业务兼容主干 |
| CheckedListBox / DomainUpDown / TrackBar / HScrollBar / VScrollBar | 未实现 | 常用输入与滚动控件族 |
| BindingSource / ErrorProvider / HelpProvider / ImageList / WinForms Timer | 未实现 | 非可视组件与数据模型尚需补齐 |
| 打印/预览、MDI、ToolStrip 完整容器、WebBrowser 及历史控件 | 未实现或缺主干 | 详见完整评估和 API 清单，需逐项处理平台/AOT 边界 |

全部已有 30 个视觉类型的逐项评估、基线缺失的 29 个非抽象顶层控件，见 [完整报告](luminaforms-reassessment-2026-10.md)。导出类型与声明成员清单见 [CSV](../assessments/2026-10-15/winforms-api-inventory.csv)。

## 横向能力与验证

| 能力 | 当前状态 | 已验证范围/差距 |
| --- | --- | --- |
| 窗口、控件、菜单主题与 JSON 调色板 | 部分实现 | 有主题与序列化测试；不等同于控件行为完整 |
| 自动缩放与嵌套布局 | 部分实现 | 有局部测试；跨屏 DPI、Anchor/AutoSize/滚动待补 |
| UI Invoke 系列 | 存在已复现差异 | 当前不正确投递 UI 线程，需优先修复 |
| Dispose | 部分实现 | 有资源处理；Component 契约及完整所有权仍需对齐 |
| NativeFormsDemo 双目标编译 | 已验证场景 | net10.0-windows 与 net10.0 均构建成功 |
| Native AOT 发布 | 已验证场景 | net10.0 / win-x64 产出原生 EXE，仍有 IL2026/IL3053 |
| 完整 Demo AOT 启动与交互 | 尚未完成验收 | 短时隐藏探针未观察到主窗，不作根因推断 |
| Visual Studio 设计器 | 保留真实 WinForms 路径 | 本次未执行可视设计器保存/重开验收 |
| 兼容分析器 | 有源码，消费效果待验 | 需验证包布局、Analyzer 引用和实际诊断触发 |
| Linux / macOS 原生 UI | 尚未实现/验证 | 当前依赖 Win32 与 Windows 专属 System.Drawing.Common |

下一步顺序：确定性阻塞 → 基础继承/线程/布局 → 已有控件真实行为 → 数据绑定/表格 → 完整 Windows 基线；原生跨平台先做小型实机验证。详见 [路线图](../../ROADMAP.md)。
