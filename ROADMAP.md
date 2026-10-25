# LuminaForms 路线图

更新：2026-10-17。目标是 **.NET 10 WinForms 平滑替换 + Native AOT**；包与命名空间保持 `Lumina.Forms`。

实施遵循根目录 [项目目标、需求与实施原则](PROJECT_REQUIREMENTS.md)。Drawing 已确定为 **Windows 两个目标都用官方 System.Drawing.Common，Linux 使用自有 Lumina.Drawing.Common 包并保留 System.Drawing 源码消费方式**；优先验证同名程序集 System.Drawing.Common，通过 XPL-002 后冻结完整身份与发布方案。按目标 OS/RID 选择完整依赖图，保留原生控件、Designer 资源和 AOT；先做实际必需范围与 Linux 原型，不承诺先重写全套 Drawing。身份、资源与平台运行须有独立证据，不能把需求定案当作实现完成。

**Windows：每控件属性、事件、行为分别 ≥98%；Linux/macOS：可移植部分分别 ≥98%。** 方法、接口和派生扩展另行验收，Windows 专属项明确列出。当前尚未达到该指标。

[逐控件任务与历史](docs/roadmap/README.md) · [遗漏审计](docs/roadmap/scope-audit.md) · [98%计量](docs/roadmap/coverage.md) · [执行排序](docs/roadmap/execution.md) · [三平台方案](docs/roadmap/platforms.md) · [项目整合](docs/roadmap/project-consolidation.md)

**001～030的明确子范围代码完成；030已完成FND-019基础键消息/事件契约首范围。** [当前记录](HANDOFF.md) · [030结果](docs/roadmap/batches/030.md) · [完整任务索引](docs/roadmap/tasks.csv)。029的停止记录已被2026-10-26新的明确用户授权覆盖；不恢复无关巡检，完整Control、Windows全面验收及Linux/macOS仍未完成。✅代码完成与🧪兼容验收分开，未完父项/固定分母/任务ID保留。每批按真实代码/必要检查同步索引，不以签名/基类/局部探针或测试数量推出整控件完成/98%。

| 顺序 | 工作 | 状态 | 任务入口 |
| --- | --- | --- | --- |
| 0 | 固定 .NET 10.0.11 类型、成员、继承与接口清单 | ✅ 清单已生成；运行验收仍待办 | [基线](docs/roadmap/api/baseline.json)、[逐成员](docs/roadmap/api/members.csv)、[接口](docs/roadmap/api/interfaces.csv) |
| 1 · M0 | UI线程、ListViewItem构造、启动签名、完整Demo AOT主窗、裁剪警告 | 🟡 线程/基础构造/签名及Form标准链已实现；完整主窗/模态/警告仍未闭环 | [基础阻塞](docs/roadmap/foundations.md) |
| 2 · M1 | 标准继承、生命周期、父子/集合、布局、焦点、验证、模态、DPI | 🟡 基础Control构造/边界/通知/释放及Parent/Controls/Form链首范围已实现；标准句柄、布局事务及基础默认布局/首选尺寸已实现明确范围，完整派生布局/继承属性/验证/模态仍缺 | [公共基础](docs/roadmap/foundations.md)、[Form](docs/roadmap/controls/Form.md) |
| 3 · M2 | 逐项补齐已有控件、原生双向同步、输入、菜单、TaskDialog与公共对话框 | 🟡 已有局部实现 | [每控件独立任务](docs/roadmap/README.md)、[行为场景](docs/roadmap/behavior-catalog.md) |
| 4 · M3 | 绑定、DataGridView、业务组件、真实应用迁移 | ⬜ | [执行顺序](docs/roadmap/execution.md)、[DataGridView](docs/roadmap/controls/DataGridView.md) |
| 5 · M4 | 高级控件、历史占位契约、Printing、派生绘制、可访问性、COM/MDI边界 | ⬜ | [完整索引](docs/roadmap/README.md)、[附属类型](docs/roadmap/contracts.md) |
| 6 · 平台原型 | 先解Font/Image/Graphics公共契约，再验证GTK4/AppKit原生控件与AOT | 🔎 | [平台进入条件](docs/roadmap/platforms.md) |
| 7 · M5 | 按相同控件顺序扩展Linux/macOS，并逐平台达到门槛 | ⬜ | 各控件Windows/Linux/macOS任务 |
| 仓库整合 | 移除WPF适配器；美化/注入/效果展示隔离到效果解决方案 | ✅ 本轮已执行 | [整合记录](docs/roadmap/project-consolidation.md) |

`NativeFormsDemo` 固定双目标：`net10.0-windows` 用真实WinForms并保留VS设计器；`net10.0` 用Lumina.Forms并保持AOT。静态UI仍由 `.Designer.cs` / `.resx` 管理。

批次029完成Control-12c/12d自动焦点转移与验证事件、ContainerControl-02b/03a移除候选及程序验证状态机、Form-07a关闭验证前置，并补Panel/ScrollableControl/TabControl必要批量验证消费。4,392项测试（新增49）全通过，双目标Demo Release 0错误/339警告。16场景全部正常退出/无HWND残留；最终固定参考1,832行、JIT1,880行、AOT1,847行：JIT对参考1/16严格相同，其余为15个额外WM_QUIT及11场景关闭LostFocus；JIT/AOT仅5/16严格相同，11场景相差关闭LostFocus的33行，未定位根因，严格失败保留。旧Focus157/父子集合391/生命周期204行参考JIT全同；028仍18/30同参考，保留11个WM_QUIT及SplitContainer投影差异。探针/Demo AOT为3,065,344/4,297,216字节，CLR目录0，Demo实际显示/复选框0→1/WM_CLOSE退出及原HWND失效通过。全部派生、点击提交取消、键盘/IME、物理设备、Designer/资源及GTK4/AppKit仍未完成，不宣称整Control或98%。 详情见[029](docs/roadmap/batches/029.md)及[验证记录](docs/roadmap/validation.md)。029后停止，未安排030。

批次030完成Control-12e/FND-019基础键消息与事件契约首范围；批次031完成Control-11c/FND-013 RightToLeft托管与Win32样式、ScrollableControl-02a～06a托管滚动首范围、ContainerControl-05a/06a、UserControl-01a/02a；批次032完成Panel-01b/02a、UserControl-03a和ScrollableControl-08a Windows滚动消息首范围。4,430项全线测试、Forms双目标Release构建通过。PreviewKeyDown、命令/对话框预处理、KeyPreview、完整Tab/助记键/IME、真实物理滚轮/触摸板、Cursor、完整缩放/设计器、Native AOT和跨平台仍未完成，不宣称完整基础链或98%。详情见[032](docs/roadmap/batches/032.md)。
首轮90天先做M0/M1及高频迁移阻塞，再依据真实样本重估；不把完整Windows98%和两个原生后端承诺在90天完成。详细排序与估算在[执行计划](docs/roadmap/execution.md)。

[项目价值与重新评估](docs/zh/luminaforms-reassessment-2026-10.md) · [首轮审计](docs/assessments/2026-10-15/README.md) · [此前路线记录](docs/assessments/2026-10-15/roadmap-before-reassessment.md)
