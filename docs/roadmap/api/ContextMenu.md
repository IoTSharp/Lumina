# ContextMenu：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/ContextMenu.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.ContextMenu`；基类：`System.Windows.Forms.Menu`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **10**，公开事件 **3**，公开方法 **8**；受保护扩展成员 **13**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-5D59B58E24EDA5CC | Constructor | `public ContextMenu()` | `System.Windows.Forms.ContextMenu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-268EFEFC30EED7BD | Constructor | `public ContextMenu(System.Windows.Forms.MenuItem[] menuItems)` | `System.Windows.Forms.ContextMenu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DB93FF18F3C24576 | Event | `public event System.EventHandler Collapse` | `System.Windows.Forms.ContextMenu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DE64EBD8562CCF5C | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-616171277F33DDCD | Event | `public event System.EventHandler Popup` | `System.Windows.Forms.ContextMenu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-74A7B8622104A898 | Field | `public const System.Int32 FindHandle = 0` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FD64B3FA18786677 | Field | `public const System.Int32 FindShortcut = 1` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-055CE07C08006CA0 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EE7A4D413833D9A4 | Method | `public System.Windows.Forms.MenuItem FindMenuItem(System.Int32 type, System.IntPtr value)` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E8349D1E9A4F88B3 | Method | `public System.Windows.Forms.ContextMenu GetContextMenu()` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3CFF20EE075128BE | Method | `public System.Windows.Forms.MainMenu GetMainMenu()` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-62E0BA6524F8787E | Method | `public virtual System.Void MergeMenu(System.Windows.Forms.Menu menuSrc)` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7AE057347EA3B00E | Method | `public System.Void Show(System.Windows.Forms.Control control, System.Drawing.Point pos)` | `System.Windows.Forms.ContextMenu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-41F510A3CF81CB6E | Method | `public System.Void Show(System.Windows.Forms.Control control, System.Drawing.Point pos, System.Windows.Forms.LeftRightAlignment alignment)` | `System.Windows.Forms.ContextMenu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A508A2AC185DDB59 | Method | `public virtual System.String ToString()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4019E315619AE6E4 | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-56C2B7CBB7C089B6 | Property | `System.IntPtr Handle { public get; }` | `System.Windows.Forms.Menu` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-027E9E394CF2C8B7 | Property | `virtual System.Boolean IsParent { public get; }` | `System.Windows.Forms.Menu` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-871ADF0DC03D75C2 | Property | `System.Windows.Forms.MenuItem MdiListItem { public get; }` | `System.Windows.Forms.Menu` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-84E0F385F48A0419 | Property | `System.Windows.Forms.Menu+MenuItemCollection MenuItems { public get; }` | `System.Windows.Forms.Menu` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0141AD773F6A626 | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.Menu` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3F2A9592E287EB7B | Property | `virtual System.Windows.Forms.RightToLeft RightToLeft { public get; public set; }` | `System.Windows.Forms.ContextMenu` LocalizableAttribute(true); DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ED73DE7A0FB7F812 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-81AC92F1E0B22101 | Property | `System.Windows.Forms.Control SourceControl { public get; }` | `System.Windows.Forms.ContextMenu` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9131158D12E8B2CA | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.Menu` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-2C0B19B4514D464E | Method | `protected internal System.Void CloneMenu(System.Windows.Forms.Menu menuSrc)` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D2CB93932902061A | Method | `protected virtual System.IntPtr CreateMenuHandle()` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-20AB1EB0A0EE57E1 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A60D13F4BC00F76B | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FD98A54B0BE040CE | Method | `protected System.Int32 FindMergePosition(System.Int32 mergeOrder)` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-37646E38F9EBC584 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-77372F41D01579CB | Method | `protected internal virtual System.Void OnCollapse(System.EventArgs e)` | `System.Windows.Forms.ContextMenu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-02FBD6FF20BF4299 | Method | `protected internal virtual System.Void OnPopup(System.EventArgs e)` | `System.Windows.Forms.ContextMenu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3DC97F35F2A73B97 | Method | `protected internal virtual System.Boolean ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D0FF51E07E72E70D | Method | `protected internal virtual System.Boolean ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData, System.Windows.Forms.Control control)` | `System.Windows.Forms.ContextMenu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-07D9AF24A83E7ADD | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F705C83BD3590009 | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A43917779AD0B82D | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
