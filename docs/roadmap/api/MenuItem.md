# MenuItem：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/MenuItem.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.MenuItem`；基类：`System.Windows.Forms.Menu`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **25**，公开事件 **6**，公开方法 **11**；受保护扩展成员 **18**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-700A808D8469283F | Constructor | `public MenuItem()` | `System.Windows.Forms.MenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-62811BB7A7AA6EF7 | Constructor | `public MenuItem(System.String text)` | `System.Windows.Forms.MenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3D326E15FDE48FF2 | Constructor | `public MenuItem(System.String text, System.EventHandler onClick)` | `System.Windows.Forms.MenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6615FD6884E8E719 | Constructor | `public MenuItem(System.String text, System.EventHandler onClick, System.Windows.Forms.Shortcut shortcut)` | `System.Windows.Forms.MenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A7F4C57494375B4F | Constructor | `public MenuItem(System.String text, System.Windows.Forms.MenuItem[] items)` | `System.Windows.Forms.MenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3B3559F0C7D5284F | Constructor | `public MenuItem(System.Windows.Forms.MenuMerge mergeType, System.Int32 mergeOrder, System.Windows.Forms.Shortcut shortcut, System.String text, System.EventHandler onClick, System.EventHandler onPopup, System.EventHandler onSelect, System.Windows.Forms.MenuItem[] items)` | `System.Windows.Forms.MenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-423208668ED83DC6 | Event | `public event System.EventHandler Click` | `System.Windows.Forms.MenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-195A89CFDBD394F8 | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-95611DE358416873 | Event | `public event System.Windows.Forms.DrawItemEventHandler DrawItem` | `System.Windows.Forms.MenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7477E8BD09AF12F4 | Event | `public event System.Windows.Forms.MeasureItemEventHandler MeasureItem` | `System.Windows.Forms.MenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E65BB6F9CCA38BE8 | Event | `public event System.EventHandler Popup` | `System.Windows.Forms.MenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E042E7747E435C69 | Event | `public event System.EventHandler Select` | `System.Windows.Forms.MenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2A7F01CC20A15A44 | Field | `public const System.Int32 FindHandle = 0` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A0945D48DF8A933D | Field | `public const System.Int32 FindShortcut = 1` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EBA7305B5880CF00 | Method | `public virtual System.Windows.Forms.MenuItem CloneMenu()` | `System.Windows.Forms.MenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9D727B93B212B340 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-189842CF2A8CFD80 | Method | `public System.Windows.Forms.MenuItem FindMenuItem(System.Int32 type, System.IntPtr value)` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-85EABF6927989060 | Method | `public System.Windows.Forms.ContextMenu GetContextMenu()` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC0FEA3800493D35 | Method | `public System.Windows.Forms.MainMenu GetMainMenu()` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B21670192276E410 | Method | `public System.Void MergeMenu(System.Windows.Forms.MenuItem itemSrc)` | `System.Windows.Forms.MenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F882364FB9E53DB9 | Method | `public virtual System.Void MergeMenu(System.Windows.Forms.Menu menuSrc)` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3314E0D28CF65462 | Method | `public virtual System.Windows.Forms.MenuItem MergeMenu()` | `System.Windows.Forms.MenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-914242DA5CE67963 | Method | `public System.Void PerformClick()` | `System.Windows.Forms.MenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8F2421D522DF1483 | Method | `public virtual System.Void PerformSelect()` | `System.Windows.Forms.MenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5D0CDAD81991086B | Method | `public virtual System.String ToString()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BEF87CC4103BDD8C | Property | `System.Boolean BarBreak { public get; public set; }` | `System.Windows.Forms.MenuItem` BrowsableAttribute(false); DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4349E7EE1CCE53D7 | Property | `System.Boolean Break { public get; public set; }` | `System.Windows.Forms.MenuItem` BrowsableAttribute(false); DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C109E41603280B83 | Property | `System.Boolean Checked { public get; public set; }` | `System.Windows.Forms.MenuItem` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B60EEE9BD486A24B | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E23648D3A68EB1BD | Property | `System.Boolean DefaultItem { public get; public set; }` | `System.Windows.Forms.MenuItem` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6EF85EE423BE0566 | Property | `System.Boolean Enabled { public get; public set; }` | `System.Windows.Forms.MenuItem` LocalizableAttribute(true); DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-63ECAAAB988E919E | Property | `System.IntPtr Handle { public get; }` | `System.Windows.Forms.Menu` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A0D33229F7AEDB79 | Property | `System.Int32 Index { public get; public set; }` | `System.Windows.Forms.MenuItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A3974F66AB03A9D9 | Property | `virtual System.Boolean IsParent { public get; }` | `System.Windows.Forms.MenuItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-219C9B86D66D495D | Property | `System.Boolean MdiList { public get; public set; }` | `System.Windows.Forms.MenuItem` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-966E4CDC1C619DED | Property | `System.Windows.Forms.MenuItem MdiListItem { public get; }` | `System.Windows.Forms.Menu` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1C3CD7485FAB666E | Property | `System.Windows.Forms.Menu+MenuItemCollection MenuItems { public get; }` | `System.Windows.Forms.Menu` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3989E2952631F9AD | Property | `System.Int32 MergeOrder { public get; public set; }` | `System.Windows.Forms.MenuItem` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1534E79BD0F15122 | Property | `System.Windows.Forms.MenuMerge MergeType { public get; public set; }` | `System.Windows.Forms.MenuItem` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-264B56DE14185070 | Property | `System.Char Mnemonic { public get; }` | `System.Windows.Forms.MenuItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-706056D259330D93 | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.Menu` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A7FB8E08E1702C1E | Property | `System.Boolean OwnerDraw { public get; public set; }` | `System.Windows.Forms.MenuItem` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5ACD0DC04546E212 | Property | `System.Windows.Forms.Menu Parent { public get; }` | `System.Windows.Forms.MenuItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-558973371C128AD7 | Property | `System.Boolean RadioCheck { public get; public set; }` | `System.Windows.Forms.MenuItem` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5483A7D4B5795BA4 | Property | `System.Windows.Forms.Shortcut Shortcut { public get; public set; }` | `System.Windows.Forms.MenuItem` LocalizableAttribute(true); DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-89AF1583959D693D | Property | `System.Boolean ShowShortcut { public get; public set; }` | `System.Windows.Forms.MenuItem` DefaultValueAttribute(true); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3623732523594976 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-787F61A4C84629EA | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.Menu` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-032DA9A9D44EAC4A | Property | `System.String Text { public get; public set; }` | `System.Windows.Forms.MenuItem` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1C629F7AB8F7EAFD | Property | `System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.MenuItem` LocalizableAttribute(true); DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-F2AD3CA9C0A53269 | Method | `protected System.Void CloneMenu(System.Windows.Forms.MenuItem itemSrc)` | `System.Windows.Forms.MenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B580C2BAAF6DCAE8 | Method | `protected internal System.Void CloneMenu(System.Windows.Forms.Menu menuSrc)` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-18E0D023FBEF367E | Method | `protected virtual System.IntPtr CreateMenuHandle()` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A3DBB477DCC9713B | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F38146A748BE6E89 | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BC57E6A6EB92C59F | Method | `protected System.Int32 FindMergePosition(System.Int32 mergeOrder)` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6EEBF9B2196A984B | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-52E4750F340358D2 | Method | `protected virtual System.Void OnClick(System.EventArgs e)` | `System.Windows.Forms.MenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5C3BC62D46754297 | Method | `protected virtual System.Void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)` | `System.Windows.Forms.MenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B0AF5085473562A | Method | `protected virtual System.Void OnInitMenuPopup(System.EventArgs e)` | `System.Windows.Forms.MenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-994F3DD99F1A6450 | Method | `protected virtual System.Void OnMeasureItem(System.Windows.Forms.MeasureItemEventArgs e)` | `System.Windows.Forms.MenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1AC34D090EEF1187 | Method | `protected virtual System.Void OnPopup(System.EventArgs e)` | `System.Windows.Forms.MenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-788A9BFB05765DEF | Method | `protected virtual System.Void OnSelect(System.EventArgs e)` | `System.Windows.Forms.MenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-69BFCBBBD6BA8263 | Method | `protected internal virtual System.Boolean ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BA1E4F5B5A578F8C | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F30F8CDF431BBC6F | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C7523A9201E4F203 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FC82301E8E19DB0C | Property | `System.Int32 MenuID { protected get; }` | `System.Windows.Forms.MenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
