# SaveFileDialog：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/SaveFileDialog.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.SaveFileDialog`；基类：`System.Windows.Forms.FileDialog`。Lumina 同名类型：🟡 存在；基类及基线接口形状：⬜ 未匹配。

公开属性 **29**，公开事件 **3**，公开方法 **6**；受保护扩展成员 **14**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-234B5588C9AB9DD3 | Constructor | `public SaveFileDialog()` | `System.Windows.Forms.SaveFileDialog`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D0557E20EA3FED11 | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CE23F3F0FFA6D2DA | Event | `public event System.ComponentModel.CancelEventHandler FileOk` | `System.Windows.Forms.FileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7601DBBA4FA21955 | Event | `public event System.EventHandler HelpRequest` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A4DA46CD31924987 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C6134848B1070B26 | Method | `public System.IO.Stream OpenFile()` | `System.Windows.Forms.SaveFileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A6FFCBC456491465 | Method | `public virtual System.Void Reset()` | `System.Windows.Forms.SaveFileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-687352FBEAA24147 | Method | `public System.Windows.Forms.DialogResult ShowDialog()` | `System.Windows.Forms.CommonDialog`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-8CF279D285EAB868 | Method | `public System.Windows.Forms.DialogResult ShowDialog(System.Windows.Forms.IWin32Window owner)` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B7E1D92004CC3107 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.FileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-93EB1A2D7EEDB61A | Property | `System.Boolean AddExtension { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CC4E32321525F75B | Property | `System.Boolean AddToRecent { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BC195C8E636C00B9 | Property | `System.Boolean AutoUpgradeEnabled { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5A05E0E320DD8EC8 | Property | `virtual System.Boolean CheckFileExists { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-173126BEFCB0CE5E | Property | `System.Boolean CheckPathExists { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-20D8F69F64FE1B30 | Property | `System.Boolean CheckWriteAccess { public get; public set; }` | `System.Windows.Forms.SaveFileDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-341D4B4B4ED9AC38 | Property | `System.Nullable&#96;1<System.Guid> ClientGuid { public get; public set; }` | `System.Windows.Forms.FileDialog` LocalizableAttribute(false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-596F23CF5789A8C2 | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DEF138FB0961CE04 | Property | `System.Boolean CreatePrompt { public get; public set; }` | `System.Windows.Forms.SaveFileDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A31BBD8E7625C0AF | Property | `System.Windows.Forms.FileDialogCustomPlacesCollection CustomPlaces { public get; }` | `System.Windows.Forms.FileDialog` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-440745020698402B | Property | `System.String DefaultExt { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute("") | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-03200DA46BC0E152 | Property | `System.Boolean DereferenceLinks { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0D5FDDFD5BB86C97 | Property | `System.Boolean ExpandedMode { public get; public set; }` | `System.Windows.Forms.SaveFileDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5B2D72C844B98627 | Property | `System.String FileName { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute("") | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-629C17849FA6B13F | Property | `System.String[] FileNames { public get; }` | `System.Windows.Forms.FileDialog` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-781B41678E4A8B14 | Property | `System.String Filter { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(""); LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1ACE5F1C1F442EC9 | Property | `System.Int32 FilterIndex { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E38DD5EC1A79134B | Property | `System.String InitialDirectory { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(""); EditorAttribute("System.Windows.Forms.Design.InitialDirectoryEditor, System.Windows.Forms.Design, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",System.Drawing.Design.UITypeEditor) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-DB7E2B2B666D13C7 | Property | `System.Boolean OkRequiresInteraction { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C711D9EBA0B2047 | Property | `System.Boolean OverwritePrompt { public get; public set; }` | `System.Windows.Forms.SaveFileDialog` DefaultValueAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CCF8BBE092691D7F | Property | `System.Boolean RestoreDirectory { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2C794DF3CDA0CEBD | Property | `System.Boolean ShowHelp { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AFE26902B7CEA2BB | Property | `System.Boolean ShowHiddenFiles { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AA22870CEDA41FC2 | Property | `System.Boolean ShowPinnedPlaces { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DCE72C83E8ABCC05 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8FD91E8B8359248F | Property | `System.Boolean SupportMultiDottedExtensions { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CA83D1CC5756C7BF | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.CommonDialog` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0AC11CFFE0044782 | Property | `System.String Title { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(""); LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C911276B1B0C61D2 | Property | `System.Boolean ValidateNames { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-6004EE71F2A50B2F | Field | `protected static readonly System.Object EventFileOk` | `System.Windows.Forms.FileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5AACA967F67B4625 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EFF20F9F4CDC4F8E | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-73F7FF7297B870E1 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5A3BCBB33286191A | Method | `protected virtual System.IntPtr HookProc(System.IntPtr hWnd, System.Int32 msg, System.IntPtr wparam, System.IntPtr lparam)` | `System.Windows.Forms.FileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-04382837B567909E | Method | `protected System.Void OnFileOk(System.ComponentModel.CancelEventArgs e)` | `System.Windows.Forms.FileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ADB66B3B10030CC5 | Method | `protected virtual System.Void OnHelpRequest(System.EventArgs e)` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3B00A84F0B72133D | Method | `protected virtual System.IntPtr OwnerWndProc(System.IntPtr hWnd, System.Int32 msg, System.IntPtr wparam, System.IntPtr lparam)` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0BCBE6E64BE0A3EB | Method | `protected virtual System.Boolean RunDialog(System.IntPtr hWndOwner)` | `System.Windows.Forms.FileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BBBD789B2D7A4CA5 | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-291729FA6B2F2E1F | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C00F0447813AAA2F | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-622FDE44F2633B17 | Property | `virtual System.IntPtr Instance { protected get; }` | `System.Windows.Forms.FileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-524A61FE5C43D4A6 | Property | `System.Int32 Options { protected get; }` | `System.Windows.Forms.FileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
