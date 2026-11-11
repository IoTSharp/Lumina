# 项目整合决策与本轮修改

本页记录项目职责和待完成的工程收敛工作；已完成任务行见[完成状态](#已完成任务)，按日期/批次的变更统一见根目录 [CHANGELOG](../../CHANGELOG.md)。M0 实现期间的归属与授权见[HANDOFF](../../HANDOFF.md)。

2026-10-15，用户明确授权整合和移除无关内容。依据实际 ProjectReference、源码调用、打包列表和双目标 Demo，而不是仅按项目名删代码。没有更名现有项目、包或命名空间。

| 项目/目录 | 决定 | 原因与本轮操作 |
| --- | --- | --- |
| Lumina.Forms | 保留，主产品 | 所有 WinForms 兼容、Windows 与后续平台内部实现的入口；不拆多个控件包 |
| Lumina.Forms.Analyzers | 保留独立项目 | Roslyn 分析器需独立编译/分发，不应并进运行库；真实消费触发与包目录验收进入 M0/M3 |
| NativeFormsDemo | 保留双目标 | `net10.0-windows` 真 WinForms/VS设计器；`net10.0` Lumina/AOT；不合并为单目标 |
| Lumina.Tests | 保留一个测试项目 | 兼容/行为差分在现有测试职责内扩展；不按每个控件新建项目 |
| Lumina.Core | 当前保留 | Form/Application/NativeTheme/ResolvedVisualStyle 公开或内部使用 EffectKind/EffectOptions/LuminaWindow；直接删除/合并会改变类型身份和包依赖。先将 DWM 调用封在 Windows 路径，待平台原型证明后再考虑包解耦 |
| Lumina.Ext.WinForms | 保留 | Demo 的真实 WinForms 分支使用同名主题方法；承担对照与设计器价值，不能因“非AOT”删除 |
| Lumina.NotepadDemo | 保留一个精简样例 | 已有 AOT CI 和小应用迁移价值；完整兼容验收转由 NativeFormsDemo 主导 |
| Lumina.Ext.Wpf | ✅ 已移除源码项目与发布入口 | 仅 Effect.cs 和项目文件，为 WPF 附加属性适配；主线没有 ProjectReference。对 WinForms替换、AOT、双目标设计器均无必要；Git历史可恢复 |
| Lumina.App / Lumina.Ext / Lumina.Advanced | ✅ 已从主线构建/打包隔离，源码保留 | 独立美化应用、DWM注入与高级效果，非控件兼容依赖。放到 Lumina.Effects.slnx；不再在主线CI单独restore/pack Advanced，不混进Forms的完成率 |
| Lumina.GlassMorphDemo | ✅ 已从主解决方案移至效果解决方案 | 效果展示不证明WinForms兼容；保留源码供效果线单独使用 |
| msix/GenAssets、图标素材 | 保留 | Forms当前嵌入并打包默认图标；不能连同美化工具一并删掉共享资源；后续仅在解除引用后精简 |
| installer/msix及旧效果文档 | 保留为效果线资料 | 尚有对应源码使用；不继续扩张到Forms里程碑，历史发布说明不改写成当前能力 |

主线现在是 **7 个解决方案项目**：Forms、Analyzers、Core、Ext.WinForms、Tests、NativeFormsDemo、NotepadDemo。效果线用 `dotnet build Lumina.Effects.slnx -c Release` 单独验证。新建的是解决方案文件，不是新运行库或平台抽象项目。

## 本次具体变更

- ✅ 删除 WPF 的 Effect.cs/.csproj，清理 Lumina.slnx、GitHub CI/Release、Gitee 打包列表中的引用。
- ✅ 清理当前 README、快速开始、SDK指南和兼容性页的 WPF 安装/支持入口；历史发布说明保留原时点，并标出当前去留。
- ✅ 主线解决方案移除 App/Ext/GlassMorphDemo，创建独立效果解决方案；停止在主线发布列表打包 Advanced。
- 🟡 Core 和 Ext.WinForms 保留原类型身份/职责；**本轮没有完成跨平台解耦**。
- ⬜ 后续确认是否仍有独立效果产品需求，再决定源码搬仓或完全删除。当前隔离已经去掉主线成本，不预先做包迁移/重命名。

## 后续任务，按收益排序

| ID | 优先级/状态 | 工作及验收 |
| --- | --- | --- |
| REP-001 | P0 🟡 | CI已接完整NativeFormsDemo net10.0/win-x64还原、Native AOT发布及产物上传；固定双目标解决方案构建保持。Hosted CI本次运行、可见启动/主窗操作/正常退出及集中Windows真机仍未验收 |
| REP-003 | P1 🟡 | 已建立[Forms/Core依赖清单](dependencies.md)，Core真实DWM入口显式限制Windows，公开类型身份保持。Windows两目标保留官方Drawing；Linux自有Drawing提供者/传递资产选择、GTK/AppKit后端及运行仍待 |
| REP-004 | P1 🟡 | README定位与主包README保持，快速开始中无效Lumina.WinForms命名空间已修为实际System.Windows.Forms扩展入口；全部消费示例与真实迁移/平台差异仍需逐项核查，不由单行修复关闭完整任务 |
| REP-005 | P2 ⬜ | 若图标生成/安装器确实不再使用，再逐文件清理；每项以引用扫描和构建验证为准，不一次性大扫除 |

## 已完成任务

### 已完成任务（历史台账）

以下原任务行从当前任务表迁入历史，ID、状态、明确范围与验证限制原样保留；现有导出工具仍读取这些行。✅ 仅表示定义范围代码完成，未完父项与平台验收仍在当前表。

| ID | 优先级/状态 | 工作及验收 |
| --- | --- | --- |
| REP-002 | P0 ✅ | 批次 001：分析器统一 net10.0，作为私有 Analyzer 引用注入；独立包与 Forms 包均在 analyzers/dotnet/cs 含唯一 DLL。两种真实 NuGet 消费各触发 LNF003，assets/deps/bin 无 Roslyn/MEF/分析器运行资产；隔离应用发布属性，Demo Native AOT 发布通过；IDE codefix 交互验收后置 |
