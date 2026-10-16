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

按用户 2026-10-16 要求，`-ExportTasks` 同时更新现有逐控件索引 README 的“代码实施与已完成子任务”列；每行只汇总该控件任务页的状态，列出明确完成任务 ID，父项有剩余仍标部分完成，基类完成不自动转移到派生控件。类型身份/成员数/三平台验收列保持独立；不带开关的校验会拒绝陈旧状态列。先按真实代码和证据复核源任务页，再导出；工具只保证文档一致，不替代真实性审查或兼容验收。

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

批次 013：`./tools/compatibility/Test-ControlSizeFromClientSize.ps1 -Variant both -PublishAot` 比较 protected virtual SizeFromClientSize 与标准尺寸/客户区入口的 10,086 条固定 WinForms 10.0.11/JIT/实际 AOT 观测，支持 `-Variant reference` 先取参考。覆盖四有限边框、无/有自身 HWND、正零负和 int 极值溢出、直接调用/base 绕过/覆盖不调 base/调整返回、重入/原异常，以及负请求/重入形成的独立客户区缓存。参考 ClientSize/SetClientSizeCore 不消费该虚扩展，直接 base 换算不创建句柄、不改状态或发通知；探针只为实际反射的两个方法精确保留元数据。构造默认/所有派生约束/完整布局/DPI/Form 生命周期仍缺。

`./tools/compatibility/Test-ListViewSubItemToString.ps1` 比较标准 ToString override 的 58 条固定参考/JIT 观测，支持 `-ReferenceOnly`。覆盖原样文字/空值/Unicode/换行/嵌入 NUL、Name/Tag/样式变化、共享 view/重复/移除/跨 owner、派生分派与原异常。字符串读取不恢复首项、不改变集合/枚举/各 view key 缓存，不消费 Tag 或借用 Font；历史泛型消费继续保留。新增 257 项换算和 63 项字符串测试，1,733 项全线通过；新字符串未计实际 AOT 运行验收，完整所属/挂接样式/native/Clone/序列化及复杂 UI/设备/设计器仍后置。日志和证据在 artifacts/compatibility-batch013，下一批见 [014](../../docs/roadmap/batches/014.md)。

批次 014：`./tools/compatibility/Test-ControlSizeConstraints.ps1 -Variant both -PublishAot` 比较明确初始化后基础 Min/Max 运行期约束的 33,408 条固定 WinForms 10.0.11/JIT/实际 AOT 观测，支持 `-Variant reference` 先取参考。四有限边框、无/有 HWND、负零/冲突/重复/取消、12 入口、派生 getter/default/core 消费、重入/原异常和独立客户区缓存均覆盖。元数据固定 en-US，六个实际反射 accessor 精确保留；AmbientValue 用 CustomAttributeData 核对标准 Size/string 构造元数据，初次 AOT 动态 Value 转换失败单列，不能视为 TypeDescriptor 运行通过。编译参考探针后手动运行 `dotnet artifacts/compatibility-batch014/control-size-constraints/reference/bin/Release/net10.0-windows/SizeConstraintsProbe.dll --constructors` 可观察构造期默认约束的原始状态/调用/异常；该独立组是批次 015 的未实现缺口，不属于本批运行期全同组。

`./tools/compatibility/Test-ListViewItemMetadata.ps1` 比较 Name/Tag/ToolTipText 的 151 条固定英文参考/JIT 观测，支持 `-ReferenceOnly`。Name 代理首子项，读空表不恢复、写 null/空也恢复；Tag/Tooltip 独立，保留文本及引用、共享 view/三枚举/key 缓存与毒丸。中文 Designer 分类有明确差异，原报告见 category-localization-gap.zh-CN.json，不用英文检查冒充全语言兼容。新增 394 项约束和 101 项列表元数据测试，2,228 项全线通过，23 个旧差分驱动保持；列表新范围未记实际 AOT 运行验收。证据及 AOT 动态属性转换失败摘要在 artifacts/compatibility-batch014，下一批见 [015](../../docs/roadmap/batches/015.md)。

批次 015：`./tools/compatibility/Test-ControlDefaultSize.ps1 -Variant both -PublishAot` 比较基础无参构造/DefaultSize/构造期默认 Min/Max 的 2,677 条固定 WinForms 10.0.11/JIT/实际 AOT 观测，支持 `-Variant reference` 先固定参考。初始 0×0、四有限边框、正零负极值/约束 profile、公共虚 setter 与重复读取、不调 base/动态默认、构造重入/原异常和 context 安装时序均覆盖。DefaultSize 直接提交尺寸，双轴非零才计算客户区，零轴保留重入缓存；同步上下文在默认初始化成功后安装。实际反射仅精确保留 get_DefaultSize，AOT 无 IL2xxx/IL3xxx。已有派生构造显式尺寸保持，全部具体派生默认值和其余公共构造仍另列。

`./tools/compatibility/Test-ListViewItemToString.ps1` 比较主项标准 ToString override 的 96 条固定 en-US 参考/JIT 观测，支持 `-ReferenceOnly`。覆盖 Object 虚槽、原样 Text/空值/Unicode/换行/NUL、元数据/样式、首项/空表/共享 view/三枚举/各 view key 缓存及派生隐藏/override/原异常；读取不恢复首项或换表，不调用 Tag.ToString 或消费/释放借用 Font。新增构造 387 项、列表字符串 98 项，全线 2,713 项通过，25 个旧差分驱动保持；新列表字符串未计实际 AOT 运行验收。日志/TRX/PE/项目/依赖摘要在 artifacts/compatibility-batch015，下一批见 [016](../../docs/roadmap/batches/016.md)。

批次 016：`./tools/compatibility/Test-ControlTextConstruction.ps1 -Variant reference` 先记录固定 WinForms 10.0.11 无 parent 的两种文本构造参考，`-Variant both -PublishAot` 比较 4,584 条参考/JIT/实际 Native AOT 指定观测。覆盖构造/参数元数据、null/空/Unicode/换行/NUL、正零负极值几何、默认扩展/文本/边界/通知/context 的实际次序，以及隐藏/虚覆盖/不调 base、重入与原异常。有效 text 空值契约严格比较，构造 NullableContext 的编译器放置及调用约定原始编码保留 stderr；CoreCLR 的 Standard|HasThis 与 AOT 的 HasThis 只比较实际实例/变参/未知位契约，不给 AOT 补造 Standard。精确 #ctor/Text 根化只在探针，最终无 IL2xxx/IL3xxx；含 parent 构造须等待 Parent/Controls/Form 继承边界，不在该探针模拟。

`./tools/compatibility/Test-ListViewItemStyle.ps1 -ReferenceOnly` 先记录固定 10.0.11/en-US 的未挂接样式参考，省略开关后比较候选，541 条观测全同。覆盖 BackColor/ForeColor/Font/UseItemStyleForSubItems 元数据、默认/Empty/null/透明、首子项代理、空表恢复换表、共享 view/三枚举/key 缓存、ResetStyle 与借用/已释放 Font 身份和寿命；已挂接继承/native、全局字体配置、Clone/序列化/Designer 资源与压力另列，新范围未计实际 AOT 运行验收。新增构造 311、列表样式 328 项，全线 3,352 项及 27 个旧驱动通过。两份探针仅向忽略的 artifacts/compatibility-batch016 写临时项目、日志与证据，不加入生产项目；初始空值/调用约定差异和探针 IL2037 日志保留，代码完成与完整兼容验收分开记录。下一批见 [017](../../docs/roadmap/batches/017.md)。


批次020：`./tools/compatibility/Test-ControlLayoutTransaction.ps1 -Variant both -PublishAot` 固定WinForms10.0.11，禁用滚动；24个独立STA场景覆盖布局参数/签名、嵌套延期、缓存/重入/异常/释放、Parent/Controls/Bounds/属性通知与必要容器。支持`-Variant reference`先固定参考；每run保存源码快照/hash、stdout/stderr/退出/timeout/complete及严格comparison。当前394行JIT/实际AOT同，但参考7行Dock/Split几何差异，脚本仍失败；有参考差异时继续完成AOT再报告，不能把aotMatches=true解释为参考全同。临时项目只在artifacts/compatibility-batch020，不进入发布物，原始失败不归一化。完整LayoutEngine/首选尺寸/InitLayout/默认布局与Windows完整Control验收见[020](../../docs/roadmap/batches/020.md)/[021](../../docs/roadmap/batches/021.md)。


批次021：`./tools/compatibility/Test-ControlDefaultLayout.ps1 -Variant both -PublishAot` 固定WinForms10.0.11且禁止运行时版本前滚；18个独立STA场景覆盖标准引擎/首选缓存/默认legacy及显式V2 Anchor/Dock/AutoSize/指定边界/必要容器/实际HWND。可先用`-Variant reference`冻结，再用`-Variant lumina -PublishAot`消费匹配源码hash的参考清单。每run保存源码、hash、构建/发布和进程stdout/stderr/退出/超时/完成标志，并严格按行比较，不归一化输出。最终512行参考/JIT/真实AOT全同；021复跑旧020事务394行也已参考/JIT全同，原020七行失败仍保留。完整边界见[021](../../docs/roadmap/batches/021.md)，复杂重入/派生测量/全部容器算法和整Control未完成。
