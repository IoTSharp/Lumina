# PictureBox

基线：2026-10-15 · .NET 10.0.11。目标继承：`PictureBox : Control`。本页是实施与验收任务，未声称已完成兼容。

## 当前证据与依赖

🟡 [PictureBox.cs](../../../Lumina.Forms/PictureBox.cs) 已有 STATIC/SS_BITMAP、Image/ImageLocation、五种SizeMode计算、GDI+/System.Drawing渲染、ISupportInitialize和Click；缺异步加载/错误初始图/事件。ReleaseBitmap与STM_SETIMAGE旧句柄返回的释放路径需核对双重删除风险，不能无探针直接宣称已泄漏或崩溃。

源码定位：[Lumina.Forms/PictureBox.cs](../../../Lumina.Forms/PictureBox.cs)；共同问题见 [Control](../../../Lumina.Forms/Control.cs) 和 [Win32 interop](../../../Lumina.Forms/Win32.cs)。先决依赖：Control 布局和图像资源、ISupportInitialize、System.Drawing 兼容边界；跨平台图像/字体策略先定小范围试验。

公开成员（含继承的全部属性、事件和方法）逐项见 [PictureBox API 清单](../api/PictureBox.md)；共同行为、AOT、设计器与计数方法见 [基础任务](../foundations.md)。本表列出具体实现/验收动作，API 清单中余下成员也必须逐条关闭，不能把表外成员当作已支持。

## 按依赖排序的任务

标记：✅ 代码完成；🧪 验收通过（有兼容证据）；🚧 正在编码；🟡 已有实现但不完整或未验收；⬜ 待实现/待验收；🔎 待验证；🚫 平台不可等价（不代表完成）。P0 先修层级/阻塞，P1 完成常用契约，P2 补边界/高级场景，P3 完成平台与发布验收。

| 任务 | 优先级 | 状态 | 具体范围与完成条件 |
|---|---|---|---|
| PictureBox-001 | P0 | 🟡 | 核对Image/HBITMAP所有权：STM_SETIMAGE返回旧句柄、ReleaseBitmap、Dispose、重复设图和错误加载各路径；用GDI对象计数/无效句柄检查验证，无证据不把风险标已确认故障。 |
| PictureBox-002 | P1 | 🟡 | Image、ImageLocation、Load() / Load(string) 的优先级、空/不存在/损坏/锁定文件和异常；外部Image不误Dispose，控件自己加载的图像生命周期按基线。 |
| PictureBox-003 | P1 | 🟡 | SizeMode默认Normal，Normal/StretchImage/AutoSize/CenterImage/Zoom逐场景：控件小于图、奇数宽高、透明PNG、DPI、边框与Padding；CenterImage当前Math.Max(0,偏移)需和基线裁剪对照。 |
| PictureBox-004 | P1 | ⬜ | SizeModeChanged、LoadCompleted、LoadProgressChanged；同值设置、成功/失败/取消的事件次数/线程与参数按WinForms记录。 |
| PictureBox-005 | P1 | ⬜ | LoadAsync各重载、CancelAsync、WaitOnLoad默认false；重复并发加载、过时结果覆盖、Dispose中取消、初始图/错误图、同步Load与异步互相影响。 |
| PictureBox-006 | P1 | ⬜ | InitialImage、ErrorImage、ImageLocation URI/本地路径与资源；不使用运行时动态代码加载，网络路径功能按标准API分开验收。 |
| PictureBox-007 | P2 | ⬜ | ISupportInitialize.BeginInit/EndInit 抑制中间加载和AutoSize，Designer静态Image/.resx反序列化与本地化；初始化结束的异常/事件顺序。 |
| PictureBox-008 | P2 | ⬜ | BorderStyle、AutoSize/GetPreferredSize、BackColor/背景图、Enabled/Visible、Click与MouseClick/DoubleClick；主题刷新不把透明图背景强制变白。 |
| PictureBox-009 | P2 | ⬜ | 动态图像/动画、图像被外部释放、换图后缩放缓存与AOT解码器保留；基线支持的格式逐个验收，不以能显示PNG代表Image全部兼容。 |
| PictureBox-010 | P2 | ⬜ | Linux/macOS先验证System.Drawing.Image/.resx迁移与原生解码通路；源代码只改命名空间的目标保持，无法兼容的公开类型不得从98%分母悄悄移除。 |
| PictureBox-011 | P3 | ⬜ | 静态Designer双目标与AOT真实加载/取消/释放，原生辅助技术图像角色/替代名称、三平台布局和资源计数记录。 |
| PictureBox-CLOSE | P3 | ⬜ | 对照 API 清单逐个关闭剩余属性、事件、方法/重载与受保护兼容点；属性写入后读回、通知顺序/取消/重入、默认值、异常、未建句柄/重建/释放后行为分别有证据。Windows 与两原生平台各出属性/事件/行为分子分母，未验收项不能标🧪 验收通过。 |

## 三平台的原生实现评估

**Windows**：STATIC + SS_BITMAP/SS_NOTIFY、STM_SETIMAGE；兼容层渲染缩放用已有GDI+，窗口本身原生但图像缩放由库执行。每个HBITMAP明确所有权，先脱离窗口再删除；不同SizeMode小控件裁剪与透明背景必须对照WinForms。

**Linux / GTK4**：GtkPicture/GtkImage+GdkTexture/GdkPaintable，content-fit/缩放/裁剪对应SizeMode；源Image公开类型仍是System.Drawing.Image，现代System.Drawing.Common不能作为Linux可运行解码器。必须先验证兼容类型/资源方案，不能只换GtkPicture就宣布跨平台。

**macOS / AppKit**：NSImageView+NSImage，imageScaling/imageAlignment/imageFrameStyle映射；Zoom/Stretch/Center/Normal/AutoSize须核对裁剪。System.Drawing.Image到原生图像的公共API兼容问题与Linux相同，避免依赖libgdiplus作为默认跨平台承诺。

跨平台工作排在 Windows 行为基线与该类最小原生试验之后。先复用同一公共 WinForms 类型与状态模型，在库内封装平台差异；本路线不要求新增平台项目或公开抽象。每平台至少覆盖：默认值/边界，代码与用户修改双向同步，事件顺序/取消/重入，键盘/输入法（适用时），DPI/RTL/本地化，无障碍，创建/重建/释放，AOT 实际运行；纯编译或仅显示窗口不足以通过。

验收口径：基线固定为 .NET 10.0.11 WinForms。Windows 的公开属性、公开事件、行为场景分别 ≥98%；Linux/macOS 在逐项确认的可移植分母中分别 ≥98%。每个重载、访问器、事件参数与行为场景独立登记，继承成员不遗漏；签名同名、字段存值、任务写完均不能作为通过分子。原生外观差异与 Windows 专属成员保留在清单中，不能用“平台不可等价”自动划掉普通业务需求。当前各项尚无完整对照证据，不发布已达标百分比。

## 修改历史

- 2026-10-16 · 批次023：基础Control启用标准UserPaint后，本控件构造显式保留UserPaint=false，使现有系统窗口类过程及原生字体/绘制继续生效。仅属消息与绘制基础的必要衔接，不代表本控件完整Styles、Paint事件或视觉/输入兼容完成，见[023](../batches/023.md)。

以下为该类型源路径的真实最近提交，提交标题是历史描述，不是本次完整验收结论：

- 2026-04-08 · `7d38999` · feat: 增强深色模式支持，优化多个控件的主题应用逻辑。
- 2026-03-31 · `ce86ab5` · 已经把 Lumina.Forms 的主题能力接到更像 WinForms 的核心表面上了。现在 Control.cs 和 Form.cs 提供了 BackColor / ForeColor、对应变更事件、主题槽位解析、父容器背景继承，以及打开窗口后的实时主题刷新；高频控件也统一改成走 ApplyExp…（原提交标题节录）。
- 2026-03-31 · `b30551a` · Refactor NativeTheme to align with Windows 11 design; update NotifyIcon for improved context menu handling; enhance PictureBox with click event; imple…（原提交标题节录）。
- 2026-10-15 · 本轮新增此独立路线图与验收任务，未修改该类型实现；后续实现需在此追加日期、提交、修复范围与验收证据。

## 官方依据

- [WinForms PictureBox API（.NET 10）](https://learn.microsoft.com/dotnet/api/system.windows.forms.picturebox?view=windowsdesktop-10.0)，精确签名以本机 10.0.11 引用程序集生成的 API 清单为准。
- [Windows 原生控件参考](https://learn.microsoft.com/windows/win32/controls/static-controls)。
- [GTK4 Picture](https://docs.gtk.org/gtk4/class.Picture.html)。
- [AppKit nsimageview](https://developer.apple.com/documentation/appkit/nsimageview)。
