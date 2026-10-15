# LuminaForms 路线图

更新：2026-10-16。目标是 **.NET 10 WinForms 平滑替换 + Native AOT**；包与命名空间保持 `Lumina.Forms`。

实施遵循根目录 [项目目标、需求与实施原则](PROJECT_REQUIREMENTS.md)。Drawing 已确定为 **Windows 两个目标都用官方 System.Drawing.Common，Linux 使用自有 Lumina.Drawing.Common 包并保留 System.Drawing 源码消费方式**；优先验证同名程序集 System.Drawing.Common，通过 XPL-002 后冻结完整身份与发布方案。按目标 OS/RID 选择完整依赖图，保留原生控件、Designer 资源和 AOT；先做实际必需范围与 Linux 原型，不承诺先重写全套 Drawing。身份、资源与平台运行须有独立证据，不能把需求定案当作实现完成。

**Windows：每控件属性、事件、行为分别 ≥98%；Linux/macOS：可移植部分分别 ≥98%。** 方法、接口和派生扩展另行验收，Windows 专属项明确列出。当前尚未达到该指标。

[逐控件任务与历史](docs/roadmap/README.md) · [遗漏审计](docs/roadmap/scope-audit.md) · [98%计量](docs/roadmap/coverage.md) · [执行排序](docs/roadmap/execution.md) · [三平台方案](docs/roadmap/platforms.md) · [项目整合](docs/roadmap/project-consolidation.md)

**001～017的明确子范围代码完成；用户于2026-10-16另行授权在新会话完成Control整类型闭环。** [当前记录](HANDOFF.md) · [017结果](docs/roadmap/batches/017.md) · [完整任务索引](docs/roadmap/tasks.csv)。Control专项须取得Windows实际验收证据，并完成Linux/macOS对应代码，设备不可用时单列未验证范围；完整专项目标完成后结束，暂停的巡检不恢复。✅代码完成和🧪兼容验收通过分开记录；父任务按剩余项保持部分完成。每批提交前按真实代码/必要检查整理受影响条目，同步总览、任务页、历史和交接并重生成索引；不以文档、签名、基类或局部探针关闭整个控件/平台。既有未完项和固定基线分母保留，Control本次Windows验收不再统一后置。

| 顺序 | 工作 | 状态 | 任务入口 |
| --- | --- | --- | --- |
| 0 | 固定 .NET 10.0.11 类型、成员、继承与接口清单 | ✅ 清单已生成；运行验收仍待办 | [基线](docs/roadmap/api/baseline.json)、[逐成员](docs/roadmap/api/members.csv)、[接口](docs/roadmap/api/interfaces.csv) |
| 1 · M0 | UI线程、ListViewItem构造、启动签名、完整Demo AOT主窗、裁剪警告 | 🟡 线程/基础构造/签名等子范围已实现；主窗/警告与完整继承仍缺 | [基础阻塞](docs/roadmap/foundations.md) |
| 2 · M1 | 标准继承、生命周期、父子/集合、布局、焦点、验证、模态、DPI | 🟡 基础Control构造/边界/通知/释放等子范围已实现；Parent/Controls/Form链与完整布局仍缺 | [公共基础](docs/roadmap/foundations.md)、[Form](docs/roadmap/controls/Form.md) |
| 3 · M2 | 逐项补齐已有控件、原生双向同步、输入、菜单、TaskDialog与公共对话框 | 🟡 已有局部实现 | [每控件独立任务](docs/roadmap/README.md)、[行为场景](docs/roadmap/behavior-catalog.md) |
| 4 · M3 | 绑定、DataGridView、业务组件、真实应用迁移 | ⬜ | [执行顺序](docs/roadmap/execution.md)、[DataGridView](docs/roadmap/controls/DataGridView.md) |
| 5 · M4 | 高级控件、历史占位契约、Printing、派生绘制、可访问性、COM/MDI边界 | ⬜ | [完整索引](docs/roadmap/README.md)、[附属类型](docs/roadmap/contracts.md) |
| 6 · 平台原型 | 先解Font/Image/Graphics公共契约，再验证GTK4/AppKit原生控件与AOT | 🔎 | [平台进入条件](docs/roadmap/platforms.md) |
| 7 · M5 | 按相同控件顺序扩展Linux/macOS，并逐平台达到门槛 | ⬜ | 各控件Windows/Linux/macOS任务 |
| 仓库整合 | 移除WPF适配器；美化/注入/效果展示隔离到效果解决方案 | ✅ 本轮已执行 | [整合记录](docs/roadmap/project-consolidation.md) |

`NativeFormsDemo` 固定双目标：`net10.0-windows` 用真实WinForms并保留VS设计器；`net10.0` 用Lumina.Forms并保持AOT。静态UI仍由 `.Designer.cs` / `.resx` 管理。

批次017完成无父级无自身HWND的Text/ResetText及未挂接ImageIndex/ImageKey：3,934项测试和29个旧差分驱动通过，双目标Release 0错误；文本3,774条固定参考/JIT相同，实际AOT仅DispId属性被工具链主动删除，其他3,773条原始记录相同；图像状态518条参考/JIT相同，未计此范围实际AOT运行验收。图像转换器/编辑器、完整Demo主窗交互/正常关闭、既有IL3050、设计器、全量控件及Linux/macOS运行仍保留缺口。API分母仍为1,095类型/61,240行、运行状态unverified，不由数量推算98%，见[验证证据](docs/roadmap/validation.md)。Parent/Controls/Form、已有HWND文本、ImageList/图像构造/native和完整所属继续在原任务下保留，本次未分派。

首轮90天先做M0/M1及高频迁移阻塞，再依据真实样本重估；不把完整Windows98%和两个原生后端承诺在90天完成。详细排序与估算在[执行计划](docs/roadmap/execution.md)。

[项目价值与重新评估](docs/zh/luminaforms-reassessment-2026-10.md) · [首轮审计](docs/assessments/2026-10-15/README.md) · [此前路线记录](docs/assessments/2026-10-15/roadmap-before-reassessment.md)
