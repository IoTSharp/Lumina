# 项目整合决策与本轮修改

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
| REP-001 | P0 ⬜ | 完整Demo双目标构建、win-x64 AOT发布/可见启动纳入主线CI；通过主窗、操作和退出记录，不复用Notepad成功来代替 |
| REP-002 | P0 ⬜ | 分析器作为 Analyzer 注入并正确打包到 analyzers/dotnet/cs；NuGet消费方实际触发至少一条诊断；不把普通ProjectReference当分析器生效 |
| REP-003 | P1 ⬜ | Forms/Core 的System.Drawing与DWM平台依赖清单，Windows条件隔离；公开类型不在没有迁移证据时搬迁 |
| REP-004 | P1 ⬜ | 主包README改为兼容能力、当前限制与验证入口，主题/美化说明下沉；消费示例校验当前签名和已知差异 |
| REP-005 | P2 ⬜ | 若图标生成/安装器确实不再使用，再逐文件清理；每项以引用扫描和构建验证为准，不一次性大扫除 |

## 修改历史

| 日期 | 记录 | 范围 |
| --- | --- | --- |
| 2026-10-15 | 完成本轮整合；对应提交见Git历史 | WPF移除、效果线隔离、文档和逐控件路线图；不代表控件实现被补齐 |

实际验证结果写入 [本轮验证](validation.md)。引用关系检查命令：`rg -n 'ProjectReference' -g '*.csproj'`，并核对 Lumina.Forms/Form.cs、NativeTheme.cs、Application.cs 与 NativeFormsDemo.csproj。Git可查看移除前历史；不额外复制一份废弃WPF目录。
