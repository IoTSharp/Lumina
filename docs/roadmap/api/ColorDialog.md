# ColorDialog：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/ColorDialog.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.ColorDialog`；基类：`System.Windows.Forms.CommonDialog`。Lumina 同名类型：🟡 存在；基类及基线接口形状：⬜ 未匹配。

公开属性 **10**，公开事件 **2**，公开方法 **5**；受保护扩展成员 **12**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-F07ABAD0CA77A086 | Constructor | `public ColorDialog()` | `System.Windows.Forms.ColorDialog`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-221EA453BAEE6437 | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0EECF5AD6BEA3DC | Event | `public event System.EventHandler HelpRequest` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F8F0BBF4BEE4BBC3 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-DE362CE94D5B93A6 | Method | `public virtual System.Void Reset()` | `System.Windows.Forms.ColorDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7E37C2E9AA54C618 | Method | `public System.Windows.Forms.DialogResult ShowDialog()` | `System.Windows.Forms.CommonDialog`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7EF9C9BD7562A9E4 | Method | `public System.Windows.Forms.DialogResult ShowDialog(System.Windows.Forms.IWin32Window owner)` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3C6498B22C43C11A | Method | `public virtual System.String ToString()` | `System.Windows.Forms.ColorDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-069A04C5E180C5EC | Property | `virtual System.Boolean AllowFullOpen { public get; public set; }` | `System.Windows.Forms.ColorDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0664EE49D01D334A | Property | `virtual System.Boolean AnyColor { public get; public set; }` | `System.Windows.Forms.ColorDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FC27EB1C3DE5AD8E | Property | `System.Drawing.Color Color { public get; public set; }` | `System.Windows.Forms.ColorDialog`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A484D307021A94A4 | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9FDE4C060733FA3F | Property | `System.Int32[] CustomColors { public get; public set; }` | `System.Windows.Forms.ColorDialog` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-38E68FB36F1FA548 | Property | `virtual System.Boolean FullOpen { public get; public set; }` | `System.Windows.Forms.ColorDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-332510517F297DB0 | Property | `virtual System.Boolean ShowHelp { public get; public set; }` | `System.Windows.Forms.ColorDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A3F13E5DAA154FB8 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7B9045435F10D2CF | Property | `virtual System.Boolean SolidColorOnly { public get; public set; }` | `System.Windows.Forms.ColorDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A3E823FC99F457A5 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.CommonDialog` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-A0C40C01CE90400D | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D1A9CBE1E2825723 | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C3D74BF609E638F6 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-92718E8DBD101137 | Method | `protected virtual System.IntPtr HookProc(System.IntPtr hWnd, System.Int32 msg, System.IntPtr wparam, System.IntPtr lparam)` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C939EC8E2CCF02C5 | Method | `protected virtual System.Void OnHelpRequest(System.EventArgs e)` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B6C9E0CB8C7C35EA | Method | `protected virtual System.IntPtr OwnerWndProc(System.IntPtr hWnd, System.Int32 msg, System.IntPtr wparam, System.IntPtr lparam)` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-03C034C51A2C0476 | Method | `protected virtual System.Boolean RunDialog(System.IntPtr hwndOwner)` | `System.Windows.Forms.ColorDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-204CFA2D4757CCA0 | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-322D92891F87B07E | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DEC7C7502875836A | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5F48039BBE36891D | Property | `virtual System.IntPtr Instance { protected get; }` | `System.Windows.Forms.ColorDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9737582B0450905E | Property | `virtual System.Int32 Options { protected get; }` | `System.Windows.Forms.ColorDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
