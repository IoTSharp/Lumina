# ToolTip：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/ToolTip.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.ToolTip`；基类：`System.ComponentModel.Component`。Lumina 同名类型：🟡 存在；基类及基线接口形状：⬜ 未匹配。

公开属性 **18**，公开事件 **3**，公开方法 **13**；受保护扩展成员 **8**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-23A00AF60EA88DEA | Constructor | `public ToolTip()` | `System.Windows.Forms.ToolTip`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3B57D82934C89900 | Constructor | `public ToolTip(System.ComponentModel.IContainer cont)` | `System.Windows.Forms.ToolTip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E46E936F5BC5EC8B | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7FC2636B49DAB24E | Event | `public event System.Windows.Forms.DrawToolTipEventHandler Draw` | `System.Windows.Forms.ToolTip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-55A914FF1C4F4192 | Event | `public event System.Windows.Forms.PopupEventHandler Popup` | `System.Windows.Forms.ToolTip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B383025B951D451 | Method | `public System.Boolean CanExtend(System.Object target)` | `System.Windows.Forms.ToolTip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A529B0193A9B8C35 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F975BE5D7BFB27B1 | Method | `public System.String GetToolTip(System.Windows.Forms.Control control)` | `System.Windows.Forms.ToolTip` DefaultValueAttribute(""); LocalizableAttribute(true); EditorAttribute("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F6F43A6F16A7F464 | Method | `public System.Void Hide(System.Windows.Forms.IWin32Window win)` | `System.Windows.Forms.ToolTip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B662DB42E749CDD1 | Method | `public System.Void RemoveAll()` | `System.Windows.Forms.ToolTip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EA3FDC26B7E1C754 | Method | `public System.Void SetToolTip(System.Windows.Forms.Control control, System.String caption)` | `System.Windows.Forms.ToolTip`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-63BD15C82F4E4B2E | Method | `public System.Void Show(System.String text, System.Windows.Forms.IWin32Window window)` | `System.Windows.Forms.ToolTip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DE65DE0F445C7232 | Method | `public System.Void Show(System.String text, System.Windows.Forms.IWin32Window window, System.Drawing.Point point)` | `System.Windows.Forms.ToolTip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7B5F0D48A64FCEB9 | Method | `public System.Void Show(System.String text, System.Windows.Forms.IWin32Window window, System.Drawing.Point point, System.Int32 duration)` | `System.Windows.Forms.ToolTip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2F95011CF2F01266 | Method | `public System.Void Show(System.String text, System.Windows.Forms.IWin32Window window, System.Int32 duration)` | `System.Windows.Forms.ToolTip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0EB77860B8AAE91D | Method | `public System.Void Show(System.String text, System.Windows.Forms.IWin32Window window, System.Int32 x, System.Int32 y)` | `System.Windows.Forms.ToolTip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-56F4315536774FDC | Method | `public System.Void Show(System.String text, System.Windows.Forms.IWin32Window window, System.Int32 x, System.Int32 y, System.Int32 duration)` | `System.Windows.Forms.ToolTip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-781E8C64EF1A2835 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.ToolTip`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6BFDB3F74F35F660 | Property | `System.Boolean Active { public get; public set; }` | `System.Windows.Forms.ToolTip` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7FFAC7FC9FFEA282 | Property | `System.Int32 AutomaticDelay { public get; public set; }` | `System.Windows.Forms.ToolTip` DefaultValueAttribute(500) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-09CB90B618A02EB0 | Property | `System.Int32 AutoPopDelay { public get; public set; }` | `System.Windows.Forms.ToolTip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DF6FAF31DB540F46 | Property | `System.Drawing.Color BackColor { public get; public set; }` | `System.Windows.Forms.ToolTip` DefaultValueAttribute(System.Drawing.Color,"Info") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9FCAC198350AC9C7 | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E1EE8F1D1E5B3BB6 | Property | `System.Drawing.Color ForeColor { public get; public set; }` | `System.Windows.Forms.ToolTip` DefaultValueAttribute(System.Drawing.Color,"InfoText") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5FB58BBA93617BF4 | Property | `System.Int32 InitialDelay { public get; public set; }` | `System.Windows.Forms.ToolTip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AA87164A57EA89DC | Property | `System.Boolean IsBalloon { public get; public set; }` | `System.Windows.Forms.ToolTip` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B2126BE3BDB31DF8 | Property | `System.Boolean OwnerDraw { public get; public set; }` | `System.Windows.Forms.ToolTip` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-316850D9D9F3E912 | Property | `System.Int32 ReshowDelay { public get; public set; }` | `System.Windows.Forms.ToolTip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9EC1110C32A77EBD | Property | `System.Boolean ShowAlways { public get; public set; }` | `System.Windows.Forms.ToolTip` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F29EDD639A055A3D | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6DA576E634C9201E | Property | `System.Boolean StripAmpersands { public get; public set; }` | `System.Windows.Forms.ToolTip` BrowsableAttribute(true); DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-154186FB4EB1C7E6 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.ToolTip` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-862EF18E19760268 | Property | `System.Windows.Forms.ToolTipIcon ToolTipIcon { public get; public set; }` | `System.Windows.Forms.ToolTip` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-851B692AF28D3DC4 | Property | `System.String ToolTipTitle { public get; public set; }` | `System.Windows.Forms.ToolTip` DefaultValueAttribute("") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6AB4B96B1DEDC2F2 | Property | `System.Boolean UseAnimation { public get; public set; }` | `System.Windows.Forms.ToolTip` BrowsableAttribute(true); DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0733CD993A3C4888 | Property | `System.Boolean UseFading { public get; public set; }` | `System.Windows.Forms.ToolTip` BrowsableAttribute(true); DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-A43F9C0AE78B3849 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.ToolTip`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-450B17EA985FA83D | Method | `protected virtual System.Void Finalize()` | `System.Windows.Forms.ToolTip`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B1CE17A9D6123A24 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D1ABACD15745884B | Method | `protected System.Void StopTimer()` | `System.Windows.Forms.ToolTip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-122A1ED975216D71 | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-89581DA757FB0ED9 | Property | `virtual System.Windows.Forms.CreateParams CreateParams { protected get; }` | `System.Windows.Forms.ToolTip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AEFF1CE00911DDED | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2A7A84DB1E56579A | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
