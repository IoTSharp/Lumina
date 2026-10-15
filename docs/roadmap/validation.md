# 本轮验证与限制

日期：2026-10-15；实现基线：`b83f5340fe7a9893bd0a4857401031b7289e6930`。本轮修改是路线图/审计工具以及用户授权的项目整合，没有补齐缺失控件实现。以下验证范围分别记录。

| 项目 | 结果 | 范围与限制 |
| --- | --- | --- |
| 主线构建 | ✅ | `dotnet build Lumina.slnx -c Release`：0错误，本次314警告；包含固定双目标NativeFormsDemo，未做VS可视设计器往返 |
| 效果线构建 | ✅ | `dotnet build Lumina.Effects.slnx -c Release`：0错误，本次1警告；验证隔离后仍能构建，不代表注入运行验收 |
| 现有测试 | ✅ | `dotnet test Lumina.Tests/Lumina.Tests.csproj -c Release --no-build`：77通过、0失败、0跳过；不是全量WinForms行为认证 |
| 主线包冒烟 | ✅ | Core、WinForms适配器、Forms、Analyzers四包本地pack成功；没有向包源发布，分析器真实消费触发仍待验收 |
| 固定API导出 | ✅ | Microsoft.WindowsDesktop.App.Ref 10.0.11，1095类型、61,240条有效成员行、9,047条接口消费契约；包含继承重复，不能当去重API总数或支持率 |
| 显式/继承接口复核 | ✅ | BindingSource初始化/取消新增、BindingContext集合接口已纳入；公开接口筛选排除WinForms内部接口；接口自身继承也展开 |
| 控件范围复核 | ✅ | 固定基线69个公开Control（62具体/7抽象）、14个ToolStripItem、9个CommonDialog、4个Menu、113个Component并集、13个TaskDialog对象及21个DataGridViewElement均有独立页；含嵌套检查，见[补漏审计](scope-audit.md) |
| 默认值探针 | ✅ 仅参考端 | [46项观测与探针源码](../assessments/2026-10-15/data-control-defaults.json)，JIT真实WinForms参考端；未证明Lumina相应属性正确 |
| 历史占位复核 | ✅ 仅参考端 | [固定10.0.11探针与完整源码](../assessments/2026-10-15/legacy-control-probes.json)：12个具体旧类型构造PNSE，Menu抽象；标准异常与可选Framework历史UI分开规划 |
| 文档结构检查 | ✅ | 187组控件/API页面；1948条控件正文任务加49条FND/REP/CNS/XPL公共任务，共1997条索引；1095类责任映射；389份Markdown检查无本地断链、重复任务ID、损坏表格或NUL；1967条待办、28条局部实现、2条待研究，0条完整兼容验收通过 |
| 同名类型纠错 | ✅ 仅清单/索引 | 完整命名空间/嵌套路径绑定[独立页](api/page-map.csv)，纠正VisualStyleElement.Button等同名类造成的索引误计；附件身份、元数据和索引成员数/候选存在状态均校验，不把这项当作控件实现 |

构建/pack的详细日志在本机忽略目录 artifacts/compatibility-inventory，命令可重复运行；本页保留结果，日志不属于发布物。警告数依赖增量构建情况，不等同于新的警告数量。

文档检查复现：`./tools/compatibility/Validate-Roadmap.ps1 -ExportTasks`；自动排序导出[正文任务台账](tasks.csv)。成员和接口的运行状态全部为unverified；未将资料编写、签名命中或参考端探针计为控件行为支持。

实施接续已补根目录 [HANDOFF.md](../../HANDOFF.md)、首批任务及 AGENTS 接续规则；任务生成器分开输出代码状态与兼容验收状态。用户要求前期以代码完成推进，必要构建/轻量检查照常执行，复杂真机测试后期先 Windows、再统一 Linux，不据此提前宣称98%达标。

## 沿用的前轮证据

[首轮审计](../assessments/2026-10-15/README.md)：完整Demo的net10.0/win-x64 Native AOT发布成功，EXE 4,083,712字节、无CLR Header，仍有IL2026/IL3053。短时隐藏启动只看到辅助窗口，没有完成主窗交互与正常退出，后续需可见运行定位。**本轮没有将该项改为通过。**

## 尚未验证

- ⬜ 187组类型的完整属性/事件/方法/行为及公开接口支持率。
- ⬜ 完整Demo的AOT可见主窗、操作与正常关闭；资源警告闭环。
- ⬜ Visual Studio实际设计器打开/保存/重开及所有控件资源往返。
- ⬜ Linux GTK4/macOS AppKit原生后端、各平台Native AOT、真实设备输入法/辅助技术/部署。
- ⬜ System.Drawing.Common公开类型身份的跨平台可行路线。

不会因为规划已细化而宣布以上能力完成；下一项实施按 [M0排序](foundations.md)。
