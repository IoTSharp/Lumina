# 2026-10-15 评估证据与方法

对应 [评估报告](../../zh/luminaforms-reassessment-2026-10.md) 和 [路线图](../../../ROADMAP.md)。代码基线 `b83f5340fe7a9893bd0a4857401031b7289e6930`。本目录保存观察结果，不是完整 WinForms 一致性认证。

## 环境与命令

Windows x64，.NET SDK 10.0.400，Microsoft.NETCore.App / Microsoft.WindowsDesktop.App 10.0.11。

```powershell
dotnet test Lumina.Tests/Lumina.Tests.csproj -c Release
dotnet build NativeFormsDemo/NativeFormsDemo.csproj -c Release
dotnet publish NativeFormsDemo/NativeFormsDemo.csproj -f net10.0 -r win-x64 -c Release -o artifacts/reassessment-2026-10-15/demo-aot
```

实际测试附加 TRX logger；构建和发布使用 quiet 输出并记录日志。本次发布目录放在操作系统临时目录，以上给出仓库内可选的复现输出位置，不需要修改项目文件。

结果：测试 77/77 通过；双目标构建成功，本次构建输出 178 个警告；AOT 发布成功，出现自定义资源相关 IL2026 和 TypeConverter 程序集相关 IL3053。EXE 4,083,712 字节，PE 结构没有 CLR Header。该大小不包含 PDB/XML，不能作为整个分发目录的大小。

另建最小真实 WinForms 临时项目，仅包含：

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net10.0-windows</TargetFramework>
    <UseWindowsForms>true</UseWindowsForms>
    <PublishAot>true</PublishAot>
  </PropertyGroup>
</Project>
```

代码为 STAThread 入口、Application.EnableVisualStyles()、Application.Run(new Form())。`dotnet publish -c Release -r win-x64` 返回 NETSDK1175：启用裁剪时不支持或不推荐使用 Windows Forms。没有启用非公开压制开关。这验证本机标准流程的阻断，不否定社区实验方案。

## API 清点口径

- [audit-summary.json](audit-summary.json)：程序集、类型数量和已有/缺失视觉类型名称。
- [winforms-api-inventory.csv](winforms-api-inventory.csv)：全部 804 行基线类型及其声明成员摸底。

临时检查程序在 JIT 下分别读取 System.Windows.Forms、System.Windows.Forms.Primitives 的 GetExportedTypes，去重后仅保留 Namespace 为 System.Windows.Forms 的类型，包含嵌套类型。Lumina 对照程序集是本次构建的 Lumina.Forms/bin/Release/net10.0-windows/Lumina.Forms.dll。

类型名称仅把 Lumina.Forms. 映射为 System.Windows.Forms.；其余保持完整名称，嵌套类型保留加号。视觉类型筛选为非嵌套、非抽象、可赋值给真实 WinForms Control 的类型。

成员使用 DeclaredOnly，记录 public/protected 方法、构造函数、属性、事件、字段/枚举常量；方法排除属性/事件生成的 special-name 方法。比较时记录方法返回类型、参数类型、泛型参数数量，属性类型及 get/set 形状等。

限制：不沿继承链归并，不完整比较静态/虚方法修饰、接口实现、属性访问器可见性、参数默认值、泛型约束、属性元数据、扩展方法、可调用性或行为。代码位置不同也可能使继承后的有效成员被记为声明缺口；继承不正确又可能让同名签名看似命中。CSV 是结构清单，不能计算产品兼容率。

CSV 中 LuminaTypePresent=True 只说明存在同名类型，Kind/Abstract/Obsolete 基于真实 WinForms；WinFormsBase/LuminaBase 用于显示结构差异，基类名称也经过同一命名空间映射。未覆盖完整设计器程序集、Drawing、Printing、第三方库。

进一步细化时已补充 [固定引用包基线](../../roadmap/api/baseline.json)、[含继承的完整成员](../../roadmap/api/members.csv)、[公开接口契约](../../roadmap/api/interfaces.csv) 和 [参考端默认值探针](data-control-defaults.json)。新范围纳入子命名空间与Printing，为1095类型、62个非抽象顶层Control，不能直接与本页根命名空间804/61作进度比较；成员与接口匹配仍不代表运行支持。

计数：基线 804 类型，Lumina 命名空间 110 类型，同名命中 97；基线顶层非抽象 Control 类型 61，同名命中 32。Lumina 自身可实例化视觉类型为 30（含独立 Form）。差异来自抽象的 Lumina Control 和继承 Component 的 ContextMenuStrip，不能把 32 当成已实现视觉控件数。

## 控件运行探针

[runtime-probes.json](runtime-probes.json) 保存一次 STA/JIT 进程中的结果。使用真实 Win32 HWND 和 SendMessage，创建 Lumina Form 后读取；没有使用真实 WinForms 控件替代 Lumina 控件。

关键步骤：

```csharp
using var form = new Lumina.Forms.Form();
var tree = new Lumina.Forms.TreeView();
tree.Nodes.Add(new Lumina.Forms.TreeNode("node"));
var list = new Lumina.Forms.ListView();
list.Columns.Add("column", 90, Lumina.Forms.HorizontalAlignment.Left);
// list.Items.Add("row") 捕获到 NullReferenceException 后继续检查其他项。
var rich = new Lumina.Forms.RichTextBox();
var mask = new Lumina.Forms.MaskedTextBox { Mask = "0000", Text = "abcd" };
var number = new Lumina.Forms.NumericUpDown { Minimum = 0, Maximum = 10, Value = 20 };
var button = new Lumina.Forms.Button();
form.Controls.AddRange(tree, list, rich, mask, number, button);
form.Show();
// TVM_GETCOUNT = 0x1105，LVM_GETITEMCOUNT = 0x1004。
// GetClassNameW(rich.Handle) 读取原生类。
// Task.Run 中读取 InvokeRequired，并通过 Invoke(Func<int>) 返回执行线程编号。
```

观察结果：

- UI 线程 2，工作线程 4，Invoke 回调在线程 4，InvokeRequired 为 false。
- 托管树节点 1，原生树节点 0。
- ListViewItem 构造抛 NullReferenceException；因此不能把该次列表原生项目数 0 当作独立插入同步实验，列表同步缺口还结合源码分析。
- RichTextBox 原生类为 Edit。
- 设置数字掩码后文本仍为 abcd。
- 普通赋值路径允许 Value 为 20，而 Maximum 为 10。
- 顶层按钮 Parent 为 null；Form 句柄实际创建成功。

这是针对性运行观察，不是 AOT 全控件测试，也没有穷举每项 WinForms 规则。

## AOT Demo 启动观察

[demo-aot-smoke.json](demo-aot-smoke.json) 保存短时隐藏启动的窗口枚举结果：WaitForInputIdle 返回 true，进程未退出，枚举到托盘宿主、GDI+ 和 IME 辅助窗口，未枚举到 LuminaFormsWindow。约数秒观察后向所枚举窗口发送关闭消息；未在收尾期限内退出，最终只终止该次测试创建的进程。

这里记录为“未完成主窗启动验收”。隐藏启动、短观察窗口与缺少交互的限制，意味着不能凭此判定根因、AOT 独有缺陷或所有机器都会失败。后续应做可见 UI 验证并记录初始化、资源、Form.Show 和退出过程。

未验证 Linux、macOS、arm64 或 Visual Studio 可视设计器往返；未做同功能性能对比。任何与这些有关的规划都是后续工作。

## 历史材料

[roadmap-before-reassessment.md](roadmap-before-reassessment.md) 为更新前 ROADMAP 原样快照。其勾选状态和旧项目称呼保留历史含义；当前目标与状态以根目录 ROADMAP 和本次报告为准。
