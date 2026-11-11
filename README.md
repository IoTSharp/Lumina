English | [简体中文](README.zh-CN.md)

# LuminaForms

A WinForms compatibility layer for .NET 10 and Native AOT. The package and namespace remain **Lumina.Forms**. The goal is to migrate existing applications through reference and namespace changes while preserving familiar controls, events, properties, and designer code.

The current implementation uses native Windows controls. Common controls and AOT publishing have initial validation; **full WinForms compatibility is still in progress**. Native Linux GTK4 and macOS AppKit backends are planned and require prototypes.

- [Project requirements](PROJECT_REQUIREMENTS.md): product goals, compatibility principles, and the Windows official / Linux custom Drawing strategy.
- [Documentation map](docs/index.md): where to find requirements, current status, guides and historical evidence.
- [Remaining roadmap](ROADMAP.md): unfinished milestones and dependencies; M0 implementation resumed on 2026-11-11.
- [Current task index](docs/roadmap/README.md): the single entry for implementation status. Task pages mark completed IDs; dated change history lives in the root Changelog.
- [Changelog](CHANGELOG.md) · [Batch history](docs/roadmap/batches/README.md) · [Validation and unresolved gates](docs/roadmap/validation.md).
- [98% acceptance criteria](docs/roadmap/coverage.md): properties, events, and behavior measured separately.
- [2026-10 project reassessment](docs/zh/luminaforms-reassessment-2026-10.md): historical findings, not current implementation status.
- [Migration guide](docs/zh/nativeforms-migration.md) · [Support matrix](docs/zh/nativeforms-support-matrix.md).

`NativeFormsDemo` keeps two targets: `net10.0-windows` uses real WinForms and preserves Visual Studio designer support; `net10.0` uses Lumina.Forms and validates Native AOT. Static layouts stay in `.Designer.cs` / `.resx`.

See [Quick Start](docs/quickstart.md) for the development commands below. They are usage examples, not evidence that the current tree has passed every gate. Batch 064 full-suite failures and the batch 065 timeout remain unresolved; batches 066–069 did not run the full suite.

```powershell
dotnet build Lumina.slnx -c Release
dotnet test Lumina.Tests/Lumina.Tests.csproj -c Release
dotnet publish NativeFormsDemo/NativeFormsDemo.csproj -f net10.0 -r win-x64 -c Release
```

The main solution retains Lumina.Forms, Lumina.Forms.Analyzers, Lumina.Core, Lumina.Ext.WinForms, Lumina.Tests, and two validation samples. Core and the WinForms adapter preserve the existing Windows theme APIs.

The WPF adapter has been removed. The standalone effects application, DWM injection, and effects showcase remain in `Lumina.Effects.slnx` for separate builds. See the [consolidation decisions](docs/roadmap/project-consolidation.md). Historical effects documentation is separate from control compatibility status.

[MIT](LICENSE) · [Third-party notices](THIRD_PARTY_NOTICES.md)
