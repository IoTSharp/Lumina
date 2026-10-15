# ApplicationConfiguration

🟡 **现状：**已有 Lumina.Forms.ApplicationConfiguration.Initialize，仅启用视觉样式和 SetCompatibleTextRenderingDefault(false)。真实 WinForms 的同名启动类由 SDK 为消费方生成，不属于运行时导出类型，不能算一次同名类型覆盖。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/ApplicationConfiguration.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：[ApplicationConfiguration.cs](../../../Lumina.Forms/ApplicationConfiguration.cs)
- 排序：Application 启动/DPI/默认字体、消费项目 MSBuild/SDK 生成流程。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P0 / ApplicationConfiguration-01 | 审计 net10.0-windows SDK 生成的 ApplicationConfiguration.g.cs 与 net10.0 Lumina 路径，确保同一 Program.cs 调用可编译且不发生同名歧义。 |
| ⬜ | P1 / ApplicationConfiguration-02 | 明确 Initialize 执行顺序：DPI/默认字体/视觉样式/兼容文本渲染等按所用 SDK 生成内容；库固定默认不能覆盖消费方构建属性。 |
| ⬜ | P1 / ApplicationConfiguration-03 | 读取 ApplicationHighDpiMode、ApplicationDefaultFont、ApplicationVisualStyles、ApplicationUseCompatibleTextRendering 等实际 SDK 支持配置；新增颜色配置以生成文件为准，不猜 MSBuild 名称。 |
| ⬜ | P1 / ApplicationConfiguration-04 | 无配置/显式配置、重复调用、创建窗体后调用和启用/关闭生成器分别验收；错误时不静默回退导致两目标行为分叉。 |
| ⬜ | P1 / ApplicationConfiguration-05 | NativeFormsDemo 保持真实 WinForms 设计器目标和 net10.0 AOT 目标，验证 generated source/引用顺序/.resx 默认字体，不能为方便把 Demo 单目标化。 |
| ⬜ | P2 / ApplicationConfiguration-06 | Windows 调用已修正 Application 原生初始化，发布后的 DPI awareness/默认字体与实际 native 控件一致。 |
| ⬜ | P2 / ApplicationConfiguration-07 | Linux 先执行 GTK 初始化和可移植字体/资源方案，再进入同名 Application API；Windows awareness 选项在 portable 映射中明确意义，不能空执行全部初始化。 |
| ⬜ | P3 / ApplicationConfiguration-08 | macOS 通过 NSApplication 主线程初始化，使用标准默认字体映射；不用复制 WinForms SDK 的完整设计时工具链来实现一个启动入口。 |
| ⬜ | 验收 / ApplicationConfiguration-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| SDK 生成类与现有库入口协作，优先构建期简单配置，避免不必要的新生成器框架。 | 可移植启动调用 GTK 原生初始化；Font/Image 类型身份仍受平台计划前置约束。 | NSApplication 主线程初始化，维持消费源码的 Initialize 调用形式。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- `1c89c0a`（2026-04-01）：增强应用主题/初始化。
- `dcdc7e9`（2026-03-31）：加入启动配置入口。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/whats-new/net60#application-bootstrap)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/func.init.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nsapplication)
