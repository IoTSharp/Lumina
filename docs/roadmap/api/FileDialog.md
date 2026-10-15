# FileDialog：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/FileDialog.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.FileDialog`；基类：`System.Windows.Forms.CommonDialog`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **25**，公开事件 **3**，公开方法 **5**；受保护扩展成员 **14**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-5BEDF5DFCDF411B2 | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1E9881D6CEE641C1 | Event | `public event System.ComponentModel.CancelEventHandler FileOk` | `System.Windows.Forms.FileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-229423F97F0862E2 | Event | `public event System.EventHandler HelpRequest` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D9820C9113C340EE | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BB9EDEB24B564F63 | Method | `public virtual System.Void Reset()` | `System.Windows.Forms.FileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-927DD26B7FAB0888 | Method | `public System.Windows.Forms.DialogResult ShowDialog()` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-045FA22D79FC066F | Method | `public System.Windows.Forms.DialogResult ShowDialog(System.Windows.Forms.IWin32Window owner)` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E21F10A6BE598432 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.FileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BFE6CF1A42433196 | Property | `System.Boolean AddExtension { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E96F4A0B011CE9C | Property | `System.Boolean AddToRecent { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BFAFC98A89274D26 | Property | `System.Boolean AutoUpgradeEnabled { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FD2874D39C77A745 | Property | `virtual System.Boolean CheckFileExists { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3C16E0E32B6E2238 | Property | `System.Boolean CheckPathExists { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-68A6E2F993B46DCB | Property | `System.Nullable&#96;1<System.Guid> ClientGuid { public get; public set; }` | `System.Windows.Forms.FileDialog` LocalizableAttribute(false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B0BA87BA99827708 | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A05950BEC3698668 | Property | `System.Windows.Forms.FileDialogCustomPlacesCollection CustomPlaces { public get; }` | `System.Windows.Forms.FileDialog` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-022C7ED8F0A015F0 | Property | `System.String DefaultExt { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute("") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E5F2F927FC83EB58 | Property | `System.Boolean DereferenceLinks { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D9C87C7A43DA7FC4 | Property | `System.String FileName { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute("") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DCD6E7C9AEFB386B | Property | `System.String[] FileNames { public get; }` | `System.Windows.Forms.FileDialog` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6072D050194384A3 | Property | `System.String Filter { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(""); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-669D7B04828DF304 | Property | `System.Int32 FilterIndex { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9EA55B7CB83BB1F3 | Property | `System.String InitialDirectory { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(""); EditorAttribute("System.Windows.Forms.Design.InitialDirectoryEditor, System.Windows.Forms.Design, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-174221A75EAC2B24 | Property | `System.Boolean OkRequiresInteraction { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7BE29FA2F20C7E6D | Property | `System.Boolean RestoreDirectory { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16132A3FE65A708D | Property | `System.Boolean ShowHelp { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1C9C416702E35781 | Property | `System.Boolean ShowHiddenFiles { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8DBBB9540BA1E69B | Property | `System.Boolean ShowPinnedPlaces { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-61FFED23507312B6 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3FEB318830CD1E4C | Property | `System.Boolean SupportMultiDottedExtensions { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-61DA1758F98A3A06 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.CommonDialog` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AD04B76D39484DDE | Property | `System.String Title { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(""); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EB96BDE4EFA82839 | Property | `System.Boolean ValidateNames { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-90999D419E7C5CD9 | Field | `protected static readonly System.Object EventFileOk` | `System.Windows.Forms.FileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C015597923B5648B | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-02642F262507640A | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F203C25530602531 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8F258DABCFA85BEE | Method | `protected virtual System.IntPtr HookProc(System.IntPtr hWnd, System.Int32 msg, System.IntPtr wparam, System.IntPtr lparam)` | `System.Windows.Forms.FileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F2B95B6D75E864CD | Method | `protected System.Void OnFileOk(System.ComponentModel.CancelEventArgs e)` | `System.Windows.Forms.FileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E25D1EDB260AB03D | Method | `protected virtual System.Void OnHelpRequest(System.EventArgs e)` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D82542DB7BF19149 | Method | `protected virtual System.IntPtr OwnerWndProc(System.IntPtr hWnd, System.Int32 msg, System.IntPtr wparam, System.IntPtr lparam)` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B71398E8D791C3DF | Method | `protected virtual System.Boolean RunDialog(System.IntPtr hWndOwner)` | `System.Windows.Forms.FileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F21FD606F7C14877 | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BE8B9A0E87CC2FE1 | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-31C40984BA46A538 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-160D490F3DFBC18B | Property | `virtual System.IntPtr Instance { protected get; }` | `System.Windows.Forms.FileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F0A0C122E4A3004C | Property | `System.Int32 Options { protected get; }` | `System.Windows.Forms.FileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
