---
title: "LuminaForms Documentation"
description: "Requirements, current work, usage guides and historical evidence."
permalink: /
layout: default
---

# LuminaForms Documentation / 文档导航

LuminaForms is the .NET 10 WinForms replacement track. Windows uses native controls and official System.Drawing.Common; Linux/macOS remain subject to separate implementation and runtime gates. Continuous implementation stopped after batch 069; the current work is documentation cleanup.

## Read by purpose / 按用途阅读

| Need / 目的 | Authoritative entry / 入口 | Responsibility / 职责 |
| --- | --- | --- |
| Product goals and constraints | [PROJECT_REQUIREMENTS](https://github.com/IoTSharp/Lumina/blob/main/PROJECT_REQUIREMENTS.md) | Fixed targets, API/behavior, Drawing and platform requirements; no completion claims |
| Remaining work | [ROADMAP](https://github.com/IoTSharp/Lumina/blob/main/ROADMAP.md) · [Dependencies](https://github.com/IoTSharp/Lumina/blob/main/docs/roadmap/execution.md) | Unfinished milestones and ordering |
| Current task state | [Task index](https://github.com/IoTSharp/Lumina/blob/main/docs/roadmap/README.md) | Single status entry; original task pages own statuses and completed-ID history; CSV is generated |
| Delivered changes | [CHANGELOG](https://github.com/IoTSharp/Lumina/blob/main/CHANGELOG.md) · [001–069 history](https://github.com/IoTSharp/Lumina/blob/main/docs/roadmap/batches/README.md) | What changed at each point, original failures and evidence |
| Acceptance and open failures | [Validation](https://github.com/IoTSharp/Lumina/blob/main/docs/roadmap/validation.md) · [98% criteria](https://github.com/IoTSharp/Lumina/blob/main/docs/roadmap/coverage.md) | Separate implementation, full tests, Designer, Native AOT runtime and each platform |
| Session ownership | [HANDOFF](https://github.com/IoTSharp/Lumina/blob/main/HANDOFF.md) · [AGENTS](https://github.com/IoTSharp/Lumina/blob/main/AGENTS.md) | Current authorization, one writing lead, delivery and resource rules |

## Usage guides / 使用指南

- [Quick Start](/quickstart/) · [中文文档](/zh/)
- [Migration](/zh/nativeforms-migration/) · [Compatibility guidance](/zh/nativeforms-compatibility/)
- [Themes and JSON configuration](/zh/nativeforms-themes/)
- [Support-status navigation](/zh/nativeforms-support-matrix/): links to the same task index, without a second status table.

## Historical material / 历史资料

The [October 2026 assessment](https://github.com/IoTSharp/Lumina/blob/main/docs/zh/luminaforms-reassessment-2026-10.md), fixed API inventory and scope audit describe their dated baseline. Later implementation is tracked by task pages, not by rewriting historical findings.

The [effects guides](/zh/blog/) and [release news](/zh/news/) belong to the separate Windows effects line and dated releases. They do not establish current LuminaForms control or platform support.

## WeChat Group

<img src="/assets/wechat-discussion-group-qr.png" width="180" alt="Lumina WeChat discussion group QR code" />
