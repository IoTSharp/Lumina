---
title: "LuminaForms 兼容与迁移差异"
permalink: /zh/nativeforms-compatibility/
layout: default
---

# LuminaForms 兼容与迁移差异

目标是保留 WinForms 名称、签名、默认值、事件顺序、生命周期、设计器与释放语义。完整目标见[项目需求](../../PROJECT_REQUIREMENTS.md)。本页解释如何查差异，不维护第二套控件支持状态。

## 查找具体差异

1. 从[任务索引](../roadmap/README.md)进入控件页，当前表列未完成父项和成员范围，修改历史保存已完成子项的 ID、行为与限制。
2. 对照该页 API 附件查完整成员、继承和接口；固定库存是静态快照，不是当前支持率。
3. 从[验证记录](../roadmap/validation.md)区分局部 PASS、全量 FAIL、UNKNOWN 与 NOT_RUN；按[98% 口径](../roadmap/coverage.md)分别核各平台。
4. 按[迁移指南](nativeforms-migration.md)保留固定双目标，对业务使用的真实路径逐项验证。

## 已记录的迁移注意事项

- AutoSizeMode 数值已对齐 GrowAndShrink=0 / GrowOnly=1；已编译常量或按整数保存的 0/1 需复核。
- HorizontalAlignment 已对齐 Left=0 / Right=1 / Center=2；旧二进制和整数持久化的 1/2 需复核。
- AppendText 参数名已对齐为 text；使用旧 value 命名参数的源码需调整。
- 这些具体变化及当时验证见[CHANGELOG](../../CHANGELOG.md)，不能由它们推断完整输入、布局或 API 兼容。

Windows 的真实 WinForms/设计器路径保持在 net10.0-windows；net10.0 使用 Lumina.Forms 并保持 AOT 方向。Linux/macOS 的 Drawing、原生控件与运行验收遵循[平台要求](../roadmap/platforms.md)，不由 Windows 编译结果代替。
