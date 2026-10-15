# NotifyIcon：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/NotifyIcon.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.NotifyIcon`；基类：`System.ComponentModel.Component`。Lumina 同名类型：🟡 存在；基类及基线接口形状：🟡 初步匹配，待源码消费验证。

公开属性 **10**，公开事件 **11**，公开方法 **4**；受保护扩展成员 **6**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-50838E43E02BE1B2 | Constructor | `public NotifyIcon()` | `System.Windows.Forms.NotifyIcon`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D51DCFACB5F51723 | Constructor | `public NotifyIcon(System.ComponentModel.IContainer container)` | `System.Windows.Forms.NotifyIcon`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-080383D6FF3B3529 | Event | `public event System.EventHandler BalloonTipClicked` | `System.Windows.Forms.NotifyIcon`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CDED46B46285A00C | Event | `public event System.EventHandler BalloonTipClosed` | `System.Windows.Forms.NotifyIcon`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FC364001FEEFB931 | Event | `public event System.EventHandler BalloonTipShown` | `System.Windows.Forms.NotifyIcon`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BC63C044E464F432 | Event | `public event System.EventHandler Click` | `System.Windows.Forms.NotifyIcon`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1C3E98D03011317C | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-59CA624293D1ABCF | Event | `public event System.EventHandler DoubleClick` | `System.Windows.Forms.NotifyIcon`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1D101F91A5ABA1E8 | Event | `public event System.Windows.Forms.MouseEventHandler MouseClick` | `System.Windows.Forms.NotifyIcon`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-139BA571568F3865 | Event | `public event System.Windows.Forms.MouseEventHandler MouseDoubleClick` | `System.Windows.Forms.NotifyIcon`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-171D11585F6A8A98 | Event | `public event System.Windows.Forms.MouseEventHandler MouseDown` | `System.Windows.Forms.NotifyIcon`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C84D0579BF9B68C1 | Event | `public event System.Windows.Forms.MouseEventHandler MouseMove` | `System.Windows.Forms.NotifyIcon`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-63CD92B4F7642D28 | Event | `public event System.Windows.Forms.MouseEventHandler MouseUp` | `System.Windows.Forms.NotifyIcon`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4DA32AE4A531DD2A | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CBFC45FC29FF597E | Method | `public System.Void ShowBalloonTip(System.Int32 timeout)` | `System.Windows.Forms.NotifyIcon`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-00B7EBE783E9BCEB | Method | `public System.Void ShowBalloonTip(System.Int32 timeout, System.String tipTitle, System.String tipText, System.Windows.Forms.ToolTipIcon tipIcon)` | `System.Windows.Forms.NotifyIcon`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0DA0678A18B56226 | Method | `public virtual System.String ToString()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C44173DACD42AE66 | Property | `System.Windows.Forms.ToolTipIcon BalloonTipIcon { public get; public set; }` | `System.Windows.Forms.NotifyIcon` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A55B3C4C176CB43 | Property | `System.String BalloonTipText { public get; public set; }` | `System.Windows.Forms.NotifyIcon` LocalizableAttribute(true); DefaultValueAttribute(""); EditorAttribute("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-68A95B8D4E46CDB9 | Property | `System.String BalloonTipTitle { public get; public set; }` | `System.Windows.Forms.NotifyIcon` LocalizableAttribute(true); DefaultValueAttribute("") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CABFD7F231276B75 | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D366B3F542D3BF91 | Property | `System.Windows.Forms.ContextMenuStrip ContextMenuStrip { public get; public set; }` | `System.Windows.Forms.NotifyIcon` DefaultValueAttribute(null) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F1D43DC52A292CD7 | Property | `System.Drawing.Icon Icon { public get; public set; }` | `System.Windows.Forms.NotifyIcon` LocalizableAttribute(true); DefaultValueAttribute(null) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C0FC883985EE2612 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-40850289C4CDF318 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.NotifyIcon` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2DD95812550A9F57 | Property | `System.String Text { public get; public set; }` | `System.Windows.Forms.NotifyIcon` LocalizableAttribute(true); DefaultValueAttribute(""); EditorAttribute("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E3F58D998F7EE590 | Property | `System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.NotifyIcon` LocalizableAttribute(true); DefaultValueAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-F215D02DCECEA470 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.NotifyIcon`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B626FF2D69E5F6DC | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C91B397DAC56EA1C | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-277A0BA21B93C3DC | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-456E59D57A3CCD71 | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-16031EB5B8A50FF4 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
