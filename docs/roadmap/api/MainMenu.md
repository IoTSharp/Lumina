# MainMenu：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/MainMenu.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.MainMenu`；基类：`System.Windows.Forms.Menu`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **9**，公开事件 **2**，公开方法 **8**；受保护扩展成员 **11**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-44C04C2855306F03 | Constructor | `public MainMenu()` | `System.Windows.Forms.MainMenu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2BC10FEAF0AA7C03 | Constructor | `public MainMenu(System.ComponentModel.IContainer container)` | `System.Windows.Forms.MainMenu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-002B61D22771DFDB | Constructor | `public MainMenu(System.Windows.Forms.MenuItem[] items)` | `System.Windows.Forms.MainMenu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5A2D06082D607CCB | Event | `public event System.EventHandler Collapse` | `System.Windows.Forms.MainMenu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FD44D5E6D9FF4B4B | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4B7A4D053CBE699E | Field | `public const System.Int32 FindHandle = 0` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-82BD11BCBD0260BF | Field | `public const System.Int32 FindShortcut = 1` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8856883A522EC2A4 | Method | `public virtual System.Windows.Forms.MainMenu CloneMenu()` | `System.Windows.Forms.MainMenu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4B9856380B07A364 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BEC917BC4A3DF455 | Method | `public System.Windows.Forms.MenuItem FindMenuItem(System.Int32 type, System.IntPtr value)` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-191A820F17A57231 | Method | `public System.Windows.Forms.ContextMenu GetContextMenu()` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3C39DD3ECA860E2B | Method | `public System.Windows.Forms.Form GetForm()` | `System.Windows.Forms.MainMenu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-82DE318661158893 | Method | `public System.Windows.Forms.MainMenu GetMainMenu()` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D5D9AAD1E125EC9D | Method | `public virtual System.Void MergeMenu(System.Windows.Forms.Menu menuSrc)` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FB4D93909D8591D9 | Method | `public virtual System.String ToString()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ED4D79EB8018577C | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C49354DC900D7B6 | Property | `System.IntPtr Handle { public get; }` | `System.Windows.Forms.Menu` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EEF746A49F1208A6 | Property | `virtual System.Boolean IsParent { public get; }` | `System.Windows.Forms.Menu` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BBE2C999A8A92534 | Property | `System.Windows.Forms.MenuItem MdiListItem { public get; }` | `System.Windows.Forms.Menu` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-907B55CF82598117 | Property | `System.Windows.Forms.Menu+MenuItemCollection MenuItems { public get; }` | `System.Windows.Forms.Menu` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-986CB78CEF88C2D3 | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.Menu` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-861435B5BEAB4F68 | Property | `virtual System.Windows.Forms.RightToLeft RightToLeft { public get; public set; }` | `System.Windows.Forms.MainMenu` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9AA06ABBE86CE3D6 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4BFB500364B8834E | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.Menu` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-456DBB4792269A2A | Method | `protected internal System.Void CloneMenu(System.Windows.Forms.Menu menuSrc)` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-229DFF4CA4849E40 | Method | `protected virtual System.IntPtr CreateMenuHandle()` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E260E626393847C | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E9A834767DC9DF21 | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FD9488EC56B9CF2C | Method | `protected System.Int32 FindMergePosition(System.Int32 mergeOrder)` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-447BF3BB6C945D45 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8AF4B9B13F6D8035 | Method | `protected internal virtual System.Void OnCollapse(System.EventArgs e)` | `System.Windows.Forms.MainMenu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BDDB54340AED7B4F | Method | `protected internal virtual System.Boolean ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-02AF5104422BC171 | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FA29AD64EF40816F | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F33FE55890A2A994 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
