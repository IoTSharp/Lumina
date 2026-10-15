# Menu：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/Menu.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.Menu`；基类：`System.ComponentModel.Component`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **8**，公开事件 **1**，公开方法 **6**；受保护扩展成员 **11**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-54A80E6BB63471A0 | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8A1221CA23E29778 | Field | `public const System.Int32 FindHandle = 0` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BB04B4CE970A1792 | Field | `public const System.Int32 FindShortcut = 1` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-10EF38EA260F8A95 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D71A4E66EEDCF911 | Method | `public System.Windows.Forms.MenuItem FindMenuItem(System.Int32 type, System.IntPtr value)` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A6E5A55DD5D8C70A | Method | `public System.Windows.Forms.ContextMenu GetContextMenu()` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-56820581A3E9061F | Method | `public System.Windows.Forms.MainMenu GetMainMenu()` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3E6BEBA32F7D6F02 | Method | `public virtual System.Void MergeMenu(System.Windows.Forms.Menu menuSrc)` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6AF5F9B7992CC5D0 | Method | `public virtual System.String ToString()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-521C9B89D6E2BC37 | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D188F91109EC1779 | Property | `System.IntPtr Handle { public get; }` | `System.Windows.Forms.Menu` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D58E9F0690459B36 | Property | `virtual System.Boolean IsParent { public get; }` | `System.Windows.Forms.Menu` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-605BE0006E1DABF1 | Property | `System.Windows.Forms.MenuItem MdiListItem { public get; }` | `System.Windows.Forms.Menu` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B52D2CC792C9B68 | Property | `System.Windows.Forms.Menu+MenuItemCollection MenuItems { public get; }` | `System.Windows.Forms.Menu` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3EC2313ADD11CFD9 | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.Menu` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1F4CFA1878DE1601 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-866C5C0AE6009F6B | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.Menu` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-FCF967B9CB132B7C | Constructor | `protected Menu(System.Windows.Forms.MenuItem[] items)` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B539F4FB1358D73B | Method | `protected internal System.Void CloneMenu(System.Windows.Forms.Menu menuSrc)` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0545F8A78C32126 | Method | `protected virtual System.IntPtr CreateMenuHandle()` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3768B7478B3C3012 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E22EC62DA05B395 | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C81D79F7142A04B | Method | `protected System.Int32 FindMergePosition(System.Int32 mergeOrder)` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-713F10427358F4C0 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5D81EC5FC728C060 | Method | `protected internal virtual System.Boolean ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.Menu`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-870F430BBA4DB329 | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-98A662B8C48DBDA1 | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9210082AFF522227 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
