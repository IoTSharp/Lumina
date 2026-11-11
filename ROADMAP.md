# LuminaForms 剩余路线图

更新：2026-11-11（Asia/Shanghai），以批次 069 后的文档交接为基点。**用户已授权恢复 M0 公共基础、Application 和项目收敛实现；M1 及后续范围保持未授权。**

本页只保留尚未完成的工作与依赖。任务状态的唯一维护来源是[逐控件/公共任务页](docs/roadmap/README.md)，实施列与 [tasks.csv](docs/roadmap/tasks.csv)由现有工具生成。已完成的明确子范围在原任务页“已完成任务”中标记，变更历史统一见根目录 [CHANGELOG](CHANGELOG.md)。

## 未完成里程碑

| 顺序 | 未完成范围 | 依赖与完成条件 | 任务入口 |
| --- | --- | --- | --- |
| M0 | 线程/退出与回调、完整主窗 AOT/资源、分析警告、列表原生同步 | 先厘清循环/线程/窗口所有权；保留 064 全量失败、065 超时和已有严格差分失败，不能由局部 PASS 清除 | [公共基础](docs/roadmap/foundations.md)、[Application](docs/roadmap/controls/Application.md)、[REP-001](docs/roadmap/project-consolidation.md) |
| M1 | 完整句柄反射寿命、派生生命周期/布局、验证/模态、输入、缩放/DPI、Designer | 完整父项按原 ID 继续；基础类或完成子范围不自动关闭派生控件 | [Control](docs/roadmap/controls/Control.md)、[Form](docs/roadmap/controls/Form.md)、[基础与容器任务](docs/roadmap/README.md#基础与继承链) |
| M2 | 编辑控件、高频控件、集合/菜单/对话框的剩余原生行为 | RichTextBox 通知/文件/OLE/撤销/查找测量，MaskedTextProvider 真实模型，以及其他控件各自缺口 | [依赖顺序](docs/roadmap/execution.md)、[输入与显示](docs/roadmap/README.md#输入与显示)、[行为目录](docs/roadmap/behavior-catalog.md) |
| M3 | 绑定、数据表格、业务组件、常用缺项及真实应用迁移 | Binding/BindingContext → 数据与验证 → DataGridView 行列/样式/编辑控件 → 业务迁移；验证 AOT 和实际交互 | [Binding](docs/roadmap/controls/Binding.md)、[DataGridView](docs/roadmap/controls/DataGridView.md)、[附属契约](docs/roadmap/contracts.md) |
| M4 | 高级/派生绘制、可访问性、打印、OLE/MDI/浏览器与历史契约 | 逐成员披露缺失与平台差异；低优先级不从固定基线删除 | [全部任务](docs/roadmap/README.md)、[附属契约](docs/roadmap/contracts.md) |
| XPL → M5 | Linux 资源/原生控件原型，再扩展各平台；macOS 单列 | XPL-001 资源清单 → XPL-002 身份/依赖 → XPL-003 实际资源运行 → XPL-010～015 原生控件、线程、AOT 与分发 | [平台进入条件](docs/roadmap/platforms.md) |

详细优先级和当前依赖见[执行顺序](docs/roadmap/execution.md)。里程碑均未完成；当前没有控件具有完整逐项 98% 验收结论。

## 验收与不可变约束

- [项目需求](PROJECT_REQUIREMENTS.md)规定 .NET 10、固定双目标 Demo、WinForms API/行为与 Drawing 平台策略；需求不充当实现或验收记录。
- Windows 使用官方 System.Drawing.Common；Linux 自有 Drawing 与 GTK4、macOS AppKit 均需独立资源/运行证据。平台原型不以重写全部 Drawing 为前提。
- [验证门槛](docs/roadmap/validation.md)集中记录最新已执行范围、未解失败和 NOT_RUN；[98% 口径](docs/roadmap/coverage.md)保持每控件、每平台和属性/事件/行为的独立分母。
- 固定 .NET 10.0.11 库存的 1,095 类型、61,240 成员记录及全部 2,204 任务 ID 保留。任务完成数量不是 API 支持率。

[文档导航](docs/index.md) · [变更与完成历史](CHANGELOG.md) · [当前交接](HANDOFF.md)
