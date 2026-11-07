[English](README.md) | 简体中文

# LuminaForms

面向 .NET 10 与 Native AOT 的 WinForms 兼容层，包和命名空间均为 **Lumina.Forms**。目标是通过引用和命名空间切换迁移现有应用，继续使用熟悉的控件、属性、事件及设计器代码。

当前以 Windows / Win32 原生控件为实现基础。已有常用控件和 AOT 发布验证，**尚未达到全量 WinForms 兼容**；Linux GTK4、macOS AppKit 原生实现处于规划和原型阶段。

- [项目目标、需求与实施原则](PROJECT_REQUIREMENTS.md)：兼容约束、Windows 官方 / Linux 自有 Drawing 策略及验收边界。
- [文档导航](docs/index.md)：需求、状态、使用指南与历史证据各自的入口。
- [剩余路线图](ROADMAP.md)：未完成里程碑与依赖；连续代码实施已在 069 后停止。
- [当前任务索引](docs/roadmap/README.md)：实施状态的统一入口；完成任务 ID 保留在原页修改历史。
- [变更记录](CHANGELOG.md) · [批次历史](docs/roadmap/batches/README.md) · [验证及未关闭门槛](docs/roadmap/validation.md)。
- [98% 验收口径](docs/roadmap/coverage.md)：属性、事件、行为分别统计。
- [2026-10 历史评估](docs/zh/luminaforms-reassessment-2026-10.md)：当时的项目价值和缺口，不能作为当前实施状态。
- [迁移指南](docs/zh/nativeforms-migration.md) · [当前支持矩阵](docs/zh/nativeforms-support-matrix.md)。

`NativeFormsDemo` 固定双目标：`net10.0-windows` 使用真实 WinForms 并保留 Visual Studio 设计器；`net10.0` 使用 Lumina.Forms 并验证 Native AOT。静态布局保留在 `.Designer.cs` / `.resx`。

下列命令的使用说明见[快速开始](docs/quickstart.md)，不代表当前树已通过全部门槛。064 全量失败和 065 超时仍未解决；066～069 未运行全量检查。

```powershell
dotnet build Lumina.slnx -c Release
dotnet test Lumina.Tests/Lumina.Tests.csproj -c Release
dotnet publish NativeFormsDemo/NativeFormsDemo.csproj -f net10.0 -r win-x64 -c Release
```

主线保留 Lumina.Forms、Lumina.Forms.Analyzers、Lumina.Core、Lumina.Ext.WinForms、Lumina.Tests 和两个验证样例。Core 与 WinForms 适配器继续提供现有 Windows 主题能力。

WPF 适配器已移除。独立美化应用、DWM 注入和效果展示保留在 `Lumina.Effects.slnx` 单独构建；整合依据见 [项目决策](docs/roadmap/project-consolidation.md)。历史效果文档与发布记录不代表当前控件兼容程度。

[MIT](LICENSE) · [第三方声明](THIRD_PARTY_NOTICES.md)

<img src="docs/assets/wechat-discussion-group-qr.png" width="160" alt="微信交流群二维码" />
