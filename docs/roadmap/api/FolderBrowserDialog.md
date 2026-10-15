# FolderBrowserDialog：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/FolderBrowserDialog.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.FolderBrowserDialog`；基类：`System.Windows.Forms.CommonDialog`。Lumina 同名类型：🟡 存在；基类及基线接口形状：⬜ 未匹配。

公开属性 **17**，公开事件 **2**，公开方法 **5**；受保护扩展成员 **10**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-AA0B394C7CEA842B | Constructor | `public FolderBrowserDialog()` | `System.Windows.Forms.FolderBrowserDialog`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FB1F9E3E869DD5E1 | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D971A2648EE41CB8 | Event | `public event System.EventHandler HelpRequest` | `System.Windows.Forms.FolderBrowserDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-73D9B8C85274E7E5 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6DFE41551784FD72 | Method | `public virtual System.Void Reset()` | `System.Windows.Forms.FolderBrowserDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DF05A19E589F528A | Method | `public System.Windows.Forms.DialogResult ShowDialog()` | `System.Windows.Forms.CommonDialog`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9D542773BC010F87 | Method | `public System.Windows.Forms.DialogResult ShowDialog(System.Windows.Forms.IWin32Window owner)` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9447ADDCFB1F7008 | Method | `public virtual System.String ToString()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4C74D3D37AC70341 | Property | `System.Boolean AddToRecent { public get; public set; }` | `System.Windows.Forms.FolderBrowserDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5AEC8F31DD9DC1FD | Property | `System.Boolean AutoUpgradeEnabled { public get; public set; }` | `System.Windows.Forms.FolderBrowserDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D61B07D17D31844F | Property | `System.Nullable&#96;1<System.Guid> ClientGuid { public get; public set; }` | `System.Windows.Forms.FolderBrowserDialog` LocalizableAttribute(false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EF4166941D41AEBA | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B2FAD41D7EECB95 | Property | `System.String Description { public get; public set; }` | `System.Windows.Forms.FolderBrowserDialog` BrowsableAttribute(true); DefaultValueAttribute(""); LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2E2D1AE7861892B1 | Property | `System.String InitialDirectory { public get; public set; }` | `System.Windows.Forms.FolderBrowserDialog` DefaultValueAttribute(""); EditorAttribute("System.Windows.Forms.Design.InitialDirectoryEditor, System.Windows.Forms.Design, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B426DB3836BE7B7D | Property | `System.Boolean Multiselect { public get; public set; }` | `System.Windows.Forms.FolderBrowserDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-706663CACF107E94 | Property | `System.Boolean OkRequiresInteraction { public get; public set; }` | `System.Windows.Forms.FolderBrowserDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E028DBBE54F04AEA | Property | `System.Environment+SpecialFolder RootFolder { public get; public set; }` | `System.Windows.Forms.FolderBrowserDialog` BrowsableAttribute(true); DefaultValueAttribute(0); LocalizableAttribute(false); TypeConverterAttribute(System.Windows.Forms.SpecialFolderEnumConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5EB9DDB920EE6C65 | Property | `System.String SelectedPath { public get; public set; }` | `System.Windows.Forms.FolderBrowserDialog` BrowsableAttribute(true); DefaultValueAttribute(""); EditorAttribute("System.Windows.Forms.Design.SelectedPathEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CBBC656AD70E8ACA | Property | `System.String[] SelectedPaths { public get; }` | `System.Windows.Forms.FolderBrowserDialog` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-852E0D509C8D3750 | Property | `System.Boolean ShowHiddenFiles { public get; public set; }` | `System.Windows.Forms.FolderBrowserDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BFF962F7ACF27B85 | Property | `System.Boolean ShowNewFolderButton { public get; public set; }` | `System.Windows.Forms.FolderBrowserDialog` BrowsableAttribute(true); DefaultValueAttribute(true); LocalizableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A318454A24675471 | Property | `System.Boolean ShowPinnedPlaces { public get; public set; }` | `System.Windows.Forms.FolderBrowserDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-833FA4C2822638E4 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5AF1B54E95A5DB97 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.CommonDialog` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2FD6327A69937239 | Property | `System.Boolean UseDescriptionForTitle { public get; public set; }` | `System.Windows.Forms.FolderBrowserDialog` BrowsableAttribute(true); DefaultValueAttribute(false); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-D3CFEFBECBF249F5 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-38E21D2D278AAEF4 | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B16C71EA00B08BC2 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E3BA104B0ECBCDA5 | Method | `protected virtual System.IntPtr HookProc(System.IntPtr hWnd, System.Int32 msg, System.IntPtr wparam, System.IntPtr lparam)` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-33A6EF39AB4C80D7 | Method | `protected virtual System.Void OnHelpRequest(System.EventArgs e)` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FDBD82D46C2BA1A2 | Method | `protected virtual System.IntPtr OwnerWndProc(System.IntPtr hWnd, System.Int32 msg, System.IntPtr wparam, System.IntPtr lparam)` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DF6A2A1044C6D0FF | Method | `protected virtual System.Boolean RunDialog(System.IntPtr hwndOwner)` | `System.Windows.Forms.FolderBrowserDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3A8C587D9B85876C | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BC4D0293EDD39D7D | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B043EDCA16B450F1 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
