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

批次 002 的轻量检查：`./tools/compatibility/Test-ControlComponent.ps1` 比较真实 WinForms 与 Lumina 的 5 项组件释放观测；`./tools/compatibility/Test-BitmapResources.ps1` 编译真实 Demo 的两条资源路径，再执行无界面的 win-x64 Native AOT 五图逐像素、身份、文化和错误语义对照。后者的 `-SkipPublish` 仅做托管对照；默认先运行 AOT EXE、保存结果，再严格检查全部详细 IL2xxx/IL3xxx 告警，剩余标准 Container/InheritanceAttribute 的 IL3050 会令脚本失败，不能把运行结果相同等同于告警清零。两者日志写入忽略的 artifacts/compatibility-batch002；资源探针仅支持 .NET 10，不加入主线解决方案或发布物。

批次 003：`./tools/compatibility/Test-ControlThreadDispatch.ps1 -Variant both -PublishAot` 比较固定 WinForms 10.0.11、Lumina JIT 和实际 Native AOT 的 69 条 UI 投递观测；`-Variant reference` 可先取得参考结果，进程有 30 秒外部超时。`./tools/compatibility/Test-ListViewItemConstruction.ps1` 比较 41 条基础构造观测，`-ReferenceOnly` 用于先记录固定基线再实现。临时项目、日志与结果只写 artifacts/compatibility-batch003。检查只覆盖明确轻量范围；已有 HWND 的 null Delegate 基线挂起未放入共享主流程，本实现防挂起守卫、标准异常 UI 与完整 InvokeAsync 缺口见批次记录。

批次 004/005：`./tools/compatibility/Test-ControlInvokeAsync.ps1 -Variant both -PublishAot` 保留 Action/Func<T> 的 59 条参考/JIT/AOT 观测及旧 Task 专项；`./tools/compatibility/Test-ControlInvokeValueTask.ps1 -Variant both -PublishAot` 比较 ValueTask 两族的 90 条观测，包括自定义 IValueTaskSource 单次消费、取消后继续消费、原异常/token/结果身份及 async lambda 重载选择。两者支持 `-Variant reference` 先记录固定参考端。标准同步上下文/await 后 UI 恢复及完整关闭竞态未覆盖，不据此宣称完整异步兼容。

`./tools/compatibility/Test-ListViewSubItemConstruction.ps1` 保留基础构造的 37 条观测；`./tools/compatibility/Test-ListViewSubItemStyle.ps1` 比较五参构造、颜色/字体/ResetStyle 的另 37 条观测，两者支持 `-ReferenceOnly`。样式只覆盖独立/未挂接 owner，未接全局默认字体配置、已挂接继承或 native 刷新。原始 Designer/resx 保持不变；批次 005 日志/结果在 artifacts/compatibility-batch005，现有回归脚本仍写原批次忽略目录。

批次 006：`./tools/compatibility/Test-WindowsFormsSynchronizationContext.ps1 -Variant both -PublishAot` 比较显式标准 context 的 57 条固定参考/JIT/实际 Native AOT 观测，包含无 Form 投递、FIFO/state/ExecutionContext/原异常、复制/共享释放与显式安装后的 await 恢复。null Send/Post 在各自独立进程以 3 秒超时记录：参考 null Send 挂起，Lumina 明确抛 ArgumentNullException（d），null Post 两端均接受无回调；这两项不混进相等计数。`-Variant reference` 先取基线，`-SkipIsolated` 可跳过独立边界进程；默认安装/还原、完整线程退出/跨线程释放/多 UI/模态仍待后续。

`./tools/compatibility/Test-ListViewSubItemKeys.ps1` 比较四个标准 virtual key 成员的 209 条参考/JIT 观测，支持 `-ReferenceOnly`；保留上次索引缓存与等长 InvariantCultureIgnoreCase，覆盖 Name/集合修改、重复 key、null/空、两文化的 Unicode/嵌入 NUL 及首项延迟恢复。完整 IList/公开集合构造/所属/native 仍缺。两个新驱动日志写 artifacts/compatibility-batch006；共享文档、任务索引与运行验收状态继续分开记录。

批次 007：`./tools/compatibility/Test-WindowsFormsSynchronizationContextInstall.ps1 -Variant both -PublishAot` 比较默认安装首范围的 204 条固定 WinForms 10.0.11/JIT/实际 AOT 观测，支持 `-Variant reference` 先取得参考。独立 STA 覆盖线程局部 AutoInstall、null/base/custom/forms Current、Uninstall/重复/前驱缓存、构造异常、默认直接/BeginInvoke/ValueTask/工作线程 callback await UI 恢复、Run 还原和两个不同 STA 的无 Form ExitThread。旧显式投递 57 条继续由批次 006 驱动保留；默认安装不关闭完整模态/多 UI 退出与生命周期竞态。

`./tools/compatibility/Test-ListViewSubItemCollection.ps1` 比较公共构造、同 owner 多集合视图、延迟首项、整数索引验证顺序/身份及独立 key 缓存的 72 条观测，支持 `-ReferenceOnly`；完整非泛型 IList、添加/插入/所属/native 仍缺。新日志/结果在 artifacts/compatibility-batch007；18 项独立 STA 和 27 项集合回归纳入 Lumina.Tests。完整 Demo 仍保持固定双目标和原始 Designer/resx，代码完成与兼容验收分开。

批次 008：`./tools/compatibility/Test-ControlFocus.ps1 -Variant both -PublishAot` 比较标准 bool Focus、Focused/ContainsFocus/CanFocus 的 157 条固定 WinForms 10.0.11/JIT/实际 AOT 观测，支持 `-Variant reference` 先取参考。覆盖标准元数据、无 HWND 不创建、真实 HWND/外部原生切换、原生 descendant、隐藏/禁用及释放；Select/CanSelect、完整键盘/验证事件、Form 继承和全部重建竞态仍待后续。

`./tools/compatibility/Test-ListViewSubItemAdd.ps1` 比较三个 typed Add 的 74 条固定参考/JIT 观测，支持 `-ReferenceOnly`；覆盖标准返回、null/重复/其他 owner 对象、文字/样式/借用 Font、外部视图首次与 Clear 后添加、共享数据/主项 Text 和独立 key 缓存。保留历史泛型 ICollection<T>.Add 显式适配的独立回归；非泛型 IList、AddRange/Insert/完整所属和 native 同步未关闭，新增 Add 对照不声称实际 AOT 运行验收。日志/结果、293 项全线测试 TRX 和 PE/依赖证据在 artifacts/compatibility-batch008。

批次 009：`./tools/compatibility/Test-ControlBounds.ps1 -Variant both -PublishAot` 比较零尺寸、负尺寸和首次创建的 756 条固定 WinForms 10.0.11/JIT/实际 AOT 观测，支持 `-Variant reference` 先取参考。无自身 HWND 保留负尺寸且不创建句柄；已有 HWND 负数归零、实际零矩形可恢复，首次创建保留 USER32 对原始参数的解释并在 HandleCreated 前回读。构造默认、派生全部限制、Form.ScaleSize、完整布局/Min/Max/事件仍另列。

`./tools/compatibility/Test-ListViewSubItemAddRange.ps1` 比较三个 typed AddRange 的 151 条固定参考/JIT 观测，支持 `-ReferenceOnly`；null 数组拒绝且不改状态，null 元素三重载均跳过，非 null 空 Text 对象仍添加。覆盖顺序/身份/输入复制、重复/其他 owner、颜色/借用 Font、首次/Clear 后外部视图/主项 Text 和各视图独立 key 缓存。新 AddRange 未计为实际 AOT 运行验收，完整 IList/Insert/Remove/所属/native 仍缺；447 项全线回归与日志/PE/项目/依赖证据在 artifacts/compatibility-batch009。

批次 010：`./tools/compatibility/Test-ControlBoundsSpecified.ps1 -Variant both -PublishAot` 比较 BoundsSpecified、标准 SetBounds/SetBoundsCore 与七个属性入口的 2,034 条固定 WinForms 10.0.11/JIT/实际 AOT 观测，支持 `-Variant reference` 先记录。覆盖所有已知/未知位、保留值、重复、直接 core、派生拒绝/调整/None/异常与真实 HWND 矩形，保留 009 尺寸回归；完整布局/事件/MinMax/派生约束另列。

`./tools/compatibility/Test-ListViewSubItemMutation.ps1` 比较 typed Insert/void Remove/RemoveAt/Clear 的 203 条参考/JIT 观测，支持 `-ReferenceOnly`；涵盖元数据 virtual-final、null/索引验证顺序、引用身份/重复/派生 Equals、首项/空/Text恢复、共享视图/独立 key 缓存和借用 Font。历史泛型 Insert/bool Remove 显式适配由独立测试保留，未提前记为新集合范围实际 AOT 运行验收；非泛型 IList、查询/复制全契约、完整所属/挂接样式/native 仍缺。617 项全线测试与日志/TRX/PE/项目/依赖证据在 artifacts/compatibility-batch010。

批次 011：`./tools/compatibility/Test-ControlBoundsNotifications.ps1 -Variant both -PublishAot` 比较标准 LocationChanged/Move、SizeChanged/Resize 与 protected virtual On* 的 2,280 条固定 WinForms 10.0.11/JIT/实际 AOT 观测，支持 `-Variant reference` 先取参考。覆盖七属性/两族 SetBounds/直接 core、指定/忽略/未知位、重复/零负归一/恢复、参数/矩形/顺序、派生不调 base/重入/原异常。OnSizeChanged 向 OnResize 传 Empty，SizeChanged 保留原参数；有 HWND setter 异常转既有 ThreadException，无 HWND/直接 protected 调用同步传播。旧 OnBoundsChanged 独立保留；客户区/外部原生自主通知与完整布局另列。

`./tools/compatibility/Test-ListViewSubItemInterfaces.ps1` 比较非泛型 IList/ICollection 与必要 typed 查询复制的 1,406 条参考/JIT 观测，支持 `-ReferenceOnly`。对象变更类型先于位置、重复 Add 返回首次引用位置、Contains/IndexOf/Remove 按身份；CopyTo 为 private 显式 dest/index，空集合跳过验证不恢复首项，非空保留 rank/索引/容量/类型顺序、非零下界/派生数组部分写入。共享 view/独立 key 缓存/SyncRoot、借用 Font 与历史泛型消费有回归；完整枚举/并发/所属/挂接样式/native 仍缺，新接口未记实际 AOT 运行验收。986 项全线测试及日志/TRX/PE/项目/依赖证据在 artifacts/compatibility-batch011。

批次 012：`./tools/compatibility/Test-ControlClientSize.ps1 -Variant both -PublishAot` 比较基础 ClientSize/ClientRectangle/ClientSizeChanged 与 protected core/On* 的 5,556 条固定 WinForms 10.0.11/JIT/实际 AOT 轨迹，支持 `-Variant reference` 先取参考。覆盖四有限边框、明确初始化基础 Control、无/有自身 HWND、外框/客户区/缓存、11 入口与重复/零负恢复、覆盖/重入/原异常及仅位置变化时的客户区刷新。探针为自身实际反射的五成员精确保留元数据，库未全局根化或关闭裁剪；构造默认/SizeFromClientSize/全部派生约束/外部原生自主通知/完整布局与 Form 生命周期另列。

`./tools/compatibility/Test-ListViewSubItemEnumeration.ps1` 比较公开非泛型 GetEnumerator/IEnumerable/IEnumerator 的 2,531 条参考/JIT 观测，支持 `-ReferenceOnly`。覆盖 Current/MoveNext/Reset、双 view 与开始前/中途/结束后替换/Add/Insert/Remove/Clear、同对象/移除缺失/容量/惰性恢复。结构变更失效但 Current 缓存身份保留；空 Clear 不失效，owner.SubItems 恢复首项换表，旧空枚举器仍为空。历史泛型 typed 无效 Current=null 另有测试，与标准非泛型契约分开；真实多线程/压力、完整所属/挂接样式/native 未关闭，新枚举未计实际 AOT 运行验收。1,413 项全线测试及日志/TRX/PE/项目/依赖证据在 artifacts/compatibility-batch012。
