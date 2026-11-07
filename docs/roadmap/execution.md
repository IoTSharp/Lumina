# 剩余工作依赖与验收顺序

更新：2026-11-07，代码停在批次 069。本页说明未来恢复实施时的依赖，不是启动授权；当前只做文档整理。归属与授权以[HANDOFF](../../HANDOFF.md)为准。

状态只在[控件与公共任务页](README.md)维护；本页不重复完成子项或每批测试总述。已完成范围见各任务页“修改历史”和[批次历史](batches/README.md)，失败和未运行门槛见[验证](validation.md)。

## 当前最接近的剩余依赖

| 依赖链 | 原任务范围 | 下一步需解决的问题 | 保持开放的边界 |
| --- | --- | --- | --- |
| UI 线程/循环归属 → 退出与模态 | FND-002；Application-01/03/04/06/12；Form-07；同步上下文父项 | 原同步上下文回调超时、Run 外关闭 WM_QUIT、关闭 LostFocus 严格差异、ApplicationContext/多 UI 线程及模态寿命 | 064 全量 FAIL 与 065 超时原因未定；独立分组通过不关闭失败 |
| 原生通知来源 → 接收者寿命 | FND-011；Control-17；TextBoxBase-006；Form-16 | 原反射父 Form 销毁/重建关系、其他通知结果语义和完整派生恢复 | 069 的结果 0/1 回传不等于完整反射或任意高位返回值验证 |
| RichEdit 文本/游标 → 余下通知与编辑 | RichTextBox-002/005/006/007/008/009/010/011 | LinkClicked/完整 TextChanged 与通知顺序；文件重载/StreamType/编码/锁；Undo/Redo/动作名/OLE；Find、选择方向与测量 | ambient/主题、字符段落交互、RTL/缩放/DPI、辅助技术仍待 |
| TextBoxBase → MaskedTextProvider → 掩码输入/验证 | MaskedTextBox-001～011；FND-001；TextBoxBase 未完父项 | 直接层级、构造与真实模型，继而文本/选区、输入拒绝、覆盖模式、文化和类型验证 | 不能以存值属性、基类完成或 RichEdit 进展代替真实掩码模型 |
| 共享输入/布局 → 派生控件逐项闭环 | FND-010～021/023/026；各控件父项 | 真正剪贴板/菜单/物理输入、正向可见点击、派生布局、AutoScale/跨屏 DPI 与 Designer | 不用 DefaultCursor/ToString 或元数据全集阻塞已确定的真实行为依赖；这些缺口仍登记 |

这里没有创建新任务或改变优先级。每个具体缺口继续使用原任务 ID，已完成子范围不重复分配。

## 全局顺序

| 顺序 | 阶段 | 剩余交付 | 进入/退出条件 |
| --- | --- | --- | --- |
| 1 | M0 | 线程/生命周期阻塞，列表所属/原生同步，主窗/资源/AOT 与告警 | [FND-001～006](foundations.md)、[REP-001/003/004](project-consolidation.md)按实际依赖闭环；记录历史差异和测试失败 |
| 2 | M1 | Control/容器/Form、ApplicationContext、布局/焦点/验证/输入/DPI | 基础与各派生类分别核查，不由继承自动获得完成状态 |
| 3 | M2a/M2b | 高频简单控件及已知薄弱的编辑/树/列表/日期/提示控件 | 原生状态与托管对象一致，程序与用户事件、重建/释放均有范围证据 |
| 4 | M2c | 列表/容器/菜单/工具条、公共对话框、NotifyIcon/TaskDialog | 嵌套布局、集合/Owner、取消/快捷键、页面导航和关闭语义 |
| 5 | M3 | Binding/BindingSource/BindingContext → 校验/错误 → DataGridView 行列/样式/编辑 → BindingNavigator；常用缺项 | 真实对象列表编辑、排序/虚拟模式、Clone/共享行/提交和 AOT 迁移 |
| 6 | M4 | PropertyGrid、Printing、派生绘制/UIA、OLE/拖放、WebBrowser/MDI、历史契约 | 历史项先匹配固定 .NET 10 异常契约；恢复旧版 UI 属可选扩展，不稀释活跃控件分母 |
| 7 | XPL | 资源/依赖身份 → Linux 有限原生原型；macOS 独立原型 | 按[项目需求](../../PROJECT_REQUIREMENTS.md)与[XPL 闸门](platforms.md)；不必先完成全部历史 Windows 控件或重写全部 Drawing |
| 8 | M5 | 按相同业务依赖扩展 Linux/macOS | 各平台实际 Native AOT 运行、原生资源消费、释放与逐控件兼容验收 |

P0 是当前阶段的前置阻塞，P1 是共同基础和高频场景，P2 是业务/高级能力，P3 是低频/历史补齐。同一阶段先解决共享依赖，再按控件内优先级执行；M4 页内的 P0 不越过 M0。任务数量与固定 API 分母分别统计。

## 完成与检查规则

明确范围实现并通过必要编译/轻量检查后才可标 ✅ 代码完成；父项仍有工作就保持 🟡/🚧。平台、设计器、AOT 实际运行、设备和完整兼容验收分别记录，🧪 只用于匹配其平台与范围的可复现证据。[完整 Control 专项](controls/Control.md)既定的 Windows/AOT/相关 Designer 验收仍是其完成条件。

未来获授权继续时，只更新对应任务状态与历史，使用 `tools/compatibility/Validate-Roadmap.ps1 -ExportTasks` 生成索引实施列及 tasks.csv，再普通校验。行为变更写 CHANGELOG；完整命令/首失败/诊断写批次页；当前未解门槛写 validation；HANDOFF 只写当前归属。入口页不再逐批复制相同摘要。

本轮文档检查只核链接、任务导出、归档可追溯性和状态一致性；不运行产品构建、全量/设备测试，不用文档检查 PASS 清除现有产品失败。
