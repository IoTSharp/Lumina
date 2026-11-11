---
title: "LuminaForms 概览"
permalink: /zh/nativeforms/
layout: default
---

# LuminaForms 概览

LuminaForms 通过 Lumina.Forms 包和命名空间提供 .NET 10 WinForms 替代路径，优先保留控件、属性、方法、事件与设计器源码，减少迁移改动。当前基于 Windows 原生控件，完整 WinForms 兼容仍在进行，Linux GTK4 与 macOS AppKit 需要独立原型和运行验收。

## 查看当前能力

控件与公共契约的状态只在[任务索引](../roadmap/README.md)维护。完成的明确子范围在原页标记，变更历史统一见根目录 [CHANGELOG](../../CHANGELOG.md)，未完成父项继续列在任务表。[验证门槛](../roadmap/validation.md)另列完整测试、设计器、Native AOT 实际运行与各平台状态。

现有主题/视觉 API 的用法见[主题指南](nativeforms-themes.md)，配置和构建命令见[快速开始](../quickstart.md)。主题能力、旧 Windows 视觉映射与控件兼容验收是不同范围；本页不由旧版本视觉样式映射推断 .NET 10 的系统支持。

## 固定 Demo

NativeFormsDemo 保持双目标：

- net10.0-windows：真实 WinForms，保留 Visual Studio Designer。
- net10.0：Lumina.Forms，保持 Native AOT 能力。

静态初始化继续保存在 .Designer.cs / .resx；主题与可复用行为属于库。Windows 两条路径都使用官方 System.Drawing.Common。

[迁移指南](nativeforms-migration.md) · [项目需求](../../PROJECT_REQUIREMENTS.md) · [文档导航](../index.md)
