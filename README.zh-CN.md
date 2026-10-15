[English](README.md) | 简体中文

# LuminaForms

面向 .NET 10 与 Native AOT 的 WinForms 兼容层，包和命名空间均为 **Lumina.Forms**。目标是通过引用和命名空间切换迁移现有应用，继续使用熟悉的控件、属性、事件及设计器代码。

当前以 Windows / Win32 原生控件为实现基础。已有常用控件和 AOT 发布验证，**尚未达到全量 WinForms 兼容**；Linux GTK4、macOS AppKit 原生实现处于规划和原型阶段。

- [路线图](ROADMAP.md)：下一步与里程碑。
- [逐控件任务](docs/roadmap/README.md)：每个控件的任务、三平台分析和修改历史。
- [98% 验收口径](docs/roadmap/coverage.md)：属性、事件、行为分别统计。
- [重新评估](docs/zh/luminaforms-reassessment-2026-10.md)：项目价值、微软与用户需求、技术缺口。
- [迁移指南](docs/zh/nativeforms-migration.md) · [当前支持矩阵](docs/zh/nativeforms-support-matrix.md)。

`NativeFormsDemo` 固定双目标：`net10.0-windows` 使用真实 WinForms 并保留 Visual Studio 设计器；`net10.0` 使用 Lumina.Forms 并验证 Native AOT。静态布局保留在 `.Designer.cs` / `.resx`。

```powershell
dotnet build Lumina.slnx -c Release
dotnet test Lumina.Tests/Lumina.Tests.csproj -c Release
dotnet publish NativeFormsDemo/NativeFormsDemo.csproj -f net10.0 -r win-x64 -c Release
```

主线保留 Lumina.Forms、Lumina.Forms.Analyzers、Lumina.Core、Lumina.Ext.WinForms、Lumina.Tests 和两个验证样例。Core 与 WinForms 适配器继续提供现有 Windows 主题能力。

WPF 适配器已移除。独立美化应用、DWM 注入和效果展示保留在 `Lumina.Effects.slnx` 单独构建；整合依据见 [项目决策](docs/roadmap/project-consolidation.md)。历史效果文档与发布记录不代表当前控件兼容程度。

[MIT](LICENSE) · [第三方声明](THIRD_PARTY_NOTICES.md)

<img src="docs/assets/wechat-discussion-group-qr.png" width="160" alt="微信交流群二维码" />
