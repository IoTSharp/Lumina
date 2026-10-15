# 兼容清单工具

在仓库根目录运行：

```powershell
./tools/compatibility/Export-Inventory.ps1
./tools/compatibility/Validate-Roadmap.ps1 -ExportTasks
```

导出器固定使用本机安装的 **10.0.11** 引用包；缺失时安装该基线，不自动追随较新版本。默认先构建 Lumina.Forms/net10.0；已经构建且源码无变化时可加 `-SkipBuild`。通过SDK自带MetadataLoadContext读取元数据，临时审计项目仅写入已忽略的 artifacts/compatibility-inventory，不进入解决方案或生产依赖。

输出为 docs/roadmap/api 下的类型、成员、接口和所有独立控件任务页的附件，逐页身份见 page-map.csv。按完整命名空间/嵌套路径匹配；禁止短名匹配，避免VisualStyleElement.Button等覆盖真实控件。新增独立页后重新导出即可；非根命名空间和嵌套类型在 Inventory.cs 的 FocusType 显式映射，未找到精确类型会失败，SDK生成的ApplicationConfiguration单列。各类型展开继承后会重复公共基础，61,240行不是全库去重API数量；9,047行接口也包含同一继承接口的不同消费入口。接口去重与实际验收规则见 [coverage.md](../../docs/roadmap/coverage.md)。导出器不执行UI、不验证行为，签名匹配从不算已实现。

Validate-Roadmap 检查公开Control（含抽象基类）、ToolStripItem、CommonDialog、Menu、非可视Component及TaskDialog的完整继承范围和独立页；校验附件完整类型身份/成员数、阶段、三平台/历史/API入口、全类型责任映射、本地链接、表格列、NUL和任务ID，并可生成 tasks.csv。任务台账是正文任务索引，不代替完整API/行为分母。它验证文档结构，不证明原生控件实现正确。

手工维护的范围、优先级、平台适用性、修改历史和后续运行证据不由元数据导出覆盖；重新导出后须复核 ownership.csv 的归组和索引。当前任务状态与行为结果都不能从类型存在或成员数量推导。

`tasks.csv` 同时覆盖控件页与 FND/REP/CNS/XPL 公共任务。任务表是状态来源，不手工编辑生成的 CSV；导出保留 `Status`（代码开发）与 `Verification`（兼容验收）两个独立字段。✅ 代码完成导出为 code-complete，只有状态栏明确写 🧪 验收通过才导出 verified。前期真机后置不影响关闭开发部分。当前批次/子智能体文件责任见根目录 [HANDOFF.md](../../HANDOFF.md)，不另建调度框架。
