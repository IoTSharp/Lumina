# FontDialog：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/FontDialog.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.FontDialog`；基类：`System.Windows.Forms.CommonDialog`。Lumina 同名类型：🟡 存在；基类及基线接口形状：⬜ 未匹配。

公开属性 **18**，公开事件 **3**，公开方法 **5**；受保护扩展成员 **13**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-C305410C660EE254 | Constructor | `public FontDialog()` | `System.Windows.Forms.FontDialog`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-EA8F6CC9304D56DF | Event | `public event System.EventHandler Apply` | `System.Windows.Forms.FontDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6C1E8CF1C6A6CD93 | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6FB928E27C86AA9E | Event | `public event System.EventHandler HelpRequest` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3CA52C89B93724B8 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D9A2F172B0B35E4E | Method | `public virtual System.Void Reset()` | `System.Windows.Forms.FontDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-00FA2FD181B588AF | Method | `public System.Windows.Forms.DialogResult ShowDialog()` | `System.Windows.Forms.CommonDialog`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2602432A58ABCF62 | Method | `public System.Windows.Forms.DialogResult ShowDialog(System.Windows.Forms.IWin32Window owner)` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-861C3B46EE7C5312 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.FontDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16F016BDE625B453 | Property | `System.Boolean AllowScriptChange { public get; public set; }` | `System.Windows.Forms.FontDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-250F75EA2A25A238 | Property | `System.Boolean AllowSimulations { public get; public set; }` | `System.Windows.Forms.FontDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1DC2B3758371D74D | Property | `System.Boolean AllowVectorFonts { public get; public set; }` | `System.Windows.Forms.FontDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-20C3E5FD8B44F13D | Property | `System.Boolean AllowVerticalFonts { public get; public set; }` | `System.Windows.Forms.FontDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5920579A35E648E5 | Property | `System.Drawing.Color Color { public get; public set; }` | `System.Windows.Forms.FontDialog` DefaultValueAttribute(System.Drawing.Color,"Black") | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-95E31DB029C979DB | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0EA6424F1BCC2566 | Property | `System.Boolean FixedPitchOnly { public get; public set; }` | `System.Windows.Forms.FontDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7260A88F974403F0 | Property | `System.Drawing.Font Font { public get; public set; }` | `System.Windows.Forms.FontDialog`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B754FB3EF67FD8AC | Property | `System.Boolean FontMustExist { public get; public set; }` | `System.Windows.Forms.FontDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5E3DA21F6FF2F414 | Property | `System.Int32 MaxSize { public get; public set; }` | `System.Windows.Forms.FontDialog` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2885DB06A2911261 | Property | `System.Int32 MinSize { public get; public set; }` | `System.Windows.Forms.FontDialog` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AECDC09F47658B5F | Property | `System.Boolean ScriptsOnly { public get; public set; }` | `System.Windows.Forms.FontDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-41912D92F84A974A | Property | `System.Boolean ShowApply { public get; public set; }` | `System.Windows.Forms.FontDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7F8F0B3690DE0167 | Property | `System.Boolean ShowColor { public get; public set; }` | `System.Windows.Forms.FontDialog` DefaultValueAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-331CB1F5508A4A32 | Property | `System.Boolean ShowEffects { public get; public set; }` | `System.Windows.Forms.FontDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-06055AE39D90864D | Property | `System.Boolean ShowHelp { public get; public set; }` | `System.Windows.Forms.FontDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D33AA2133467338A | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6F4DA6D266818D1E | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.CommonDialog` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-BB0FE01CC79EF801 | Field | `protected static readonly System.Object EventApply` | `System.Windows.Forms.FontDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EE967CC85C3A1012 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1058E5FE83695641 | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1737CA90371B94C4 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8A76E4C0B401128E | Method | `protected virtual System.IntPtr HookProc(System.IntPtr hWnd, System.Int32 msg, System.IntPtr wparam, System.IntPtr lparam)` | `System.Windows.Forms.FontDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C738F2DAE488DBCE | Method | `protected virtual System.Void OnApply(System.EventArgs e)` | `System.Windows.Forms.FontDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-609672B1EB7C4D54 | Method | `protected virtual System.Void OnHelpRequest(System.EventArgs e)` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-02BC60A475D23EE8 | Method | `protected virtual System.IntPtr OwnerWndProc(System.IntPtr hWnd, System.Int32 msg, System.IntPtr wparam, System.IntPtr lparam)` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-884BD479920A7CBE | Method | `protected virtual System.Boolean RunDialog(System.IntPtr hWndOwner)` | `System.Windows.Forms.FontDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F7206ED494F50F2D | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C276DCEF86A61770 | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F29CAA91ADF064F | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-611D3C49B23D0D1C | Property | `System.Int32 Options { protected get; }` | `System.Windows.Forms.FontDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
