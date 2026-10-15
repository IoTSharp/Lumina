# ImageList

基线：.NET 10 WinForms 10.0.11，源码检查点 `b83f534`；规划日期：2026-10-15。

状态：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有部分实现；⬜ 待实现或待验收；🔎 待验证；🚫 无法原生等价（不算完成）。以下复选状态是任务状态，不代表已达 98%。

计分：Windows 的属性、事件、行为场景分别达到 ≥98%；Linux、macOS 对已公布的可移植范围分别达到 ≥98%。全量 Windows API 分母保留，Windows 专属项另表说明；名称相同、可编译、字段能读写均不足以记为通过。每个本页任务必须拆入对应成员/场景验收记录，关联平台和证据；不能用一个“大任务完成”代替数百成员验收。

全量签名、继承成员、重载与缺口见 [API 清单](../api/ImageList.md)；公共 `Control`、`Component`、消息循环、线程、布局、可访问性与释放见 [共同基础](../foundations.md)。本页列出任务执行粒度和高风险行为，API 清单负责列尽成员。

已探测的关键默认值见 [真实 WinForms 参考端证据](../../assessments/2026-10-15/data-control-defaults.json)；该证据只覆盖参考端，未证明 Lumina 已支持。

## 当前结论 · ⬜

未实现；ListView/TreeView/ToolStrip 等常用图片接口尚无标准 ImageList/键控 ImageCollection。现代 .NET 的默认 ColorDepth 已是 Depth32Bit，不能照搬旧资料的 8 位默认。

- 标准继承/契约：`ImageList : Component`；需要嵌套 ImageCollection、ImageListStreamer、ColorDepth。
- 代码证据：[Lumina.Forms 库目录](../../../Lumina.Forms/)（未找到本类型定义）。
- 前置顺序：公共基础 P0 → 本页 P0 → 本页 P1 → 本页 P2 → 本页 P3；跨平台实现必须复用相同公开契约，Windows 行为样本作为对照。

## 按顺序执行的任务

| 状态 | 优先级 | 任务 | 范围与可验收结果 |
| --- | --- | --- | --- |
| ⬜ | P0 | `ImageList-01` | Component 构造/IContainer、Images/Handle/HandleCreated、ImageStream、ImageSize/ColorDepth/TransparentColor/Tag；默认 ImageSize=16×16、ColorDepth=Depth32Bit、TransparentColor=Transparent（本机10.0.11实测）。 |
| ⬜ | P0 | `ImageList-02` | 确定 System.Drawing.Image/Icon/Graphics 公共签名跨平台策略；不得让 GTK/AppKit 直接调用 Windows-only System.Drawing.Common，不能随意改为非标准公开图像类型。 |
| ⬜ | P1 | `ImageList-03` | Images.Add/AddRange/Remove/RemoveAt/Clear、IList.Insert 的标准不支持/异常规则、整数/字符串索引、SetKeyName/IndexOfKey/ContainsKey/RemoveByKey、Keys、重复 key/null、异常索引；图像与 key 映射实时一致。 |
| ⬜ | P1 | `ImageList-04` | Image/Icon/Bitmap 添加重载和 AddStrip、TransparentColor/color mask、alpha、不同源大小缩放；导入图像所有权与源 Dispose 后访问行为按标准核对。 |
| ⬜ | P1 | `ImageList-05` | 延迟创建 Handle、ImageSize/ColorDepth 改变时 RecreateHandle 与内容丢失/保留的官方实际规则；每次重建通知消费控件重新绑定，旧 HIMAGELIST 不再被使用。 |
| ⬜ | P1 | `ImageList-06` | ImageStream 的设计器 .resx 往返、ImageListStreamer 序列化/反序列化 AOT 安全路径；不以不受控 BinaryFormatter 或动态类型发现处理资源。 |
| ⬜ | P2 | `ImageList-07` | Draw(Graphics,Point,index)、Draw 各重载、缩放/透明度/裁剪/无效索引；Windows 绘图实际像素与标准对照。 |
| ⬜ | P2 | `ImageList-08` | DPI/多分辨率、ColorDepth 枚举和合法 ImageSize 边界，图像 key 在列表/树中映射；正在使用时清空或替换图像，控件索引回退一致。 |
| ⬜ | P2 | `ImageList-09` | 共享 ImageList 被多个控件使用、消费控件先释放/ImageList 先释放、Handle 重建、重复 Dispose；无 GDI/native 内存泄漏和 double free。 |
| ⬜ | P3 | `ImageList-10` | GTK 原生纹理和 AppKit NSImage 内部缓存，使用同一逻辑图像与 key 集合；HIMAGELIST/Graphics 绘图专属项单列，禁止用伪 handle 凑 API 通过。 |

## 三平台实现分析

### Windows

HIMAGELIST + comctl32 ImageList_Create/Add/Replace/Remove/Destroy 等；托管 Images 保持键值及延迟句柄，消费控件通过 LVM_SETIMAGELIST/TVM_SETIMAGELIST 等连接。系统句柄由库统一拥有。

### Linux · GTK 4

没有 HIMAGELIST 等价 GTK 控件；内部用 GdkTexture/GdkPixbuf（按支持版本明确依赖）和 GtkImage 显示，托管集合提供索引/key。不能把 GObject 指针伪装成 Windows handle；编码、alpha 和裁剪另做对照。

### macOS · AppKit

内部 NSImage/NSBitmapImageRep 缓存供 NSImageView/表树 cell 使用，管理 Retina representation 与 native retain/release。Win32 ImageListStreamer 内容需要可移植解码或明确 Windows 专属，不假定 NSImage 能读取该流。

## 边界、AOT 与验收

System.Drawing 类型身份和 AOT 资源读取为跨平台先决项。优先支持标准 .resx 设计器生成路径；未知运行时序列化对象不纳入“自动可AOT”的隐含承诺，但必须在成员清单中说明。

每个可移植任务至少运行“构造前设值 → 挂载 → 用户交互 → 程序设值 → 移除 → 重建/重新挂载 → Dispose”完整周期；事件记录应包含顺序、次数、旧/新值、是否可取消、线程。能取消的操作必须检查取消后集合、当前项和原生状态一致。

Windows 使用 `NativeFormsDemo` 固定双目标对照：`net10.0-windows` 保持真实 WinForms 与设计器，`net10.0` 保持 Lumina.Forms 和 Native AOT。数据模型、资源、事件处理代码尽量共用；不能通过 Demo 特殊分支掩盖库缺口。平台验证记录附 SDK、OS、GUI 后端版本与 AOT 发布/实际启动证据。

## 修改历史

- 截至源码检查点未发现本类型实现，暂无可归属的实现修改历史；不得把其他框架或设计稿记为本类型完成记录。
- 2026-10-15：本页首次细化任务、平台方案与验收顺序；仅规划变更，未实现上述缺口。

历史来自对应源码路径的 `git log -3 --date=short`；说明按本类型相关内容摘要，不把提交标题当成功能验收证明。

## 官方资料

- https://learn.microsoft.com/dotnet/api/system.windows.forms.imagelist?view=windowsdesktop-10.0
- https://learn.microsoft.com/dotnet/api/system.windows.forms.imagelist.colordepth?view=windowsdesktop-10.0
- https://learn.microsoft.com/windows/win32/controls/image-lists
- https://docs.gtk.org/gtk4/class.Image.html
- https://docs.gtk.org/gdk4/class.Texture.html
- https://developer.apple.com/documentation/appkit/nsimage
