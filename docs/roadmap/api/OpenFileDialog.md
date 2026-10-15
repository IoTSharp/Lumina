# OpenFileDialog：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/OpenFileDialog.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.OpenFileDialog`；基类：`System.Windows.Forms.FileDialog`。Lumina 同名类型：🟡 存在；基类及基线接口形状：⬜ 未匹配。

公开属性 **32**，公开事件 **3**，公开方法 **6**；受保护扩展成员 **14**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-3D388D90E8294AA1 | Constructor | `public OpenFileDialog()` | `System.Windows.Forms.OpenFileDialog`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A3A8129A01BC53D4 | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8034F78EF1494DE2 | Event | `public event System.ComponentModel.CancelEventHandler FileOk` | `System.Windows.Forms.FileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-06C41A52CD1A6161 | Event | `public event System.EventHandler HelpRequest` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-58CE4087BF7028E2 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-56B11590E8B42948 | Method | `public System.IO.Stream OpenFile()` | `System.Windows.Forms.OpenFileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CCD3D24F74CFBC7A | Method | `public virtual System.Void Reset()` | `System.Windows.Forms.OpenFileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6EBF4C9F486322DD | Method | `public System.Windows.Forms.DialogResult ShowDialog()` | `System.Windows.Forms.CommonDialog`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1B44BFA5ED9D4981 | Method | `public System.Windows.Forms.DialogResult ShowDialog(System.Windows.Forms.IWin32Window owner)` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E2839AA9BAEE6D7B | Method | `public virtual System.String ToString()` | `System.Windows.Forms.FileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8BF85FF1427CBC67 | Property | `System.Boolean AddExtension { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D584B2E19EABF705 | Property | `System.Boolean AddToRecent { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AA20D40C204B8826 | Property | `System.Boolean AutoUpgradeEnabled { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AF4CAFC79EC81CBF | Property | `virtual System.Boolean CheckFileExists { public get; public set; }` | `System.Windows.Forms.OpenFileDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CBF180127453B7B7 | Property | `System.Boolean CheckPathExists { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-38499A0E42FFD46E | Property | `System.Nullable&#96;1<System.Guid> ClientGuid { public get; public set; }` | `System.Windows.Forms.FileDialog` LocalizableAttribute(false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-72AE1607B063D392 | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AEF1F28D16475CF8 | Property | `System.Windows.Forms.FileDialogCustomPlacesCollection CustomPlaces { public get; }` | `System.Windows.Forms.FileDialog` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-62AD947A765007F2 | Property | `System.String DefaultExt { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute("") | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A4D82B5D95430969 | Property | `System.Boolean DereferenceLinks { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E551C327050DF0D | Property | `System.String FileName { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute("") | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1F12701A34D3AA90 | Property | `System.String[] FileNames { public get; }` | `System.Windows.Forms.FileDialog` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8AAADC12865A0BD8 | Property | `System.String Filter { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(""); LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2AB5ECAFD59E1A63 | Property | `System.Int32 FilterIndex { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CF5D469789924DC4 | Property | `System.String InitialDirectory { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(""); EditorAttribute("System.Windows.Forms.Design.InitialDirectoryEditor, System.Windows.Forms.Design, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",System.Drawing.Design.UITypeEditor) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-21338F5864B5D27E | Property | `System.Boolean Multiselect { public get; public set; }` | `System.Windows.Forms.OpenFileDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7BD6FCC5C0370356 | Property | `System.Boolean OkRequiresInteraction { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A29C51A71C6E1675 | Property | `System.Boolean ReadOnlyChecked { public get; public set; }` | `System.Windows.Forms.OpenFileDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E2CE87B107166A32 | Property | `System.Boolean RestoreDirectory { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9D2141F8B95FA756 | Property | `System.String SafeFileName { public get; }` | `System.Windows.Forms.OpenFileDialog` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CD91B244345072CE | Property | `System.String[] SafeFileNames { public get; }` | `System.Windows.Forms.OpenFileDialog` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-61535AFDFC0DA720 | Property | `System.Boolean SelectReadOnly { public get; public set; }` | `System.Windows.Forms.OpenFileDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-93F7AFB0F7840308 | Property | `System.Boolean ShowHelp { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D16CD5801281C8E0 | Property | `System.Boolean ShowHiddenFiles { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-58937BE1A331E49A | Property | `System.Boolean ShowPinnedPlaces { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C37640F0AD4752DC | Property | `System.Boolean ShowPreview { public get; public set; }` | `System.Windows.Forms.OpenFileDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-40FC2CD8C1AFE8A9 | Property | `System.Boolean ShowReadOnly { public get; public set; }` | `System.Windows.Forms.OpenFileDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C814A2353F0E11E3 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9CC9FC1308FA4E83 | Property | `System.Boolean SupportMultiDottedExtensions { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5C276C03EF179B85 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.CommonDialog` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DB56547D8226D104 | Property | `System.String Title { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(""); LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C5277C19253551DF | Property | `System.Boolean ValidateNames { public get; public set; }` | `System.Windows.Forms.FileDialog` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-5D7D8DE8B29ADE33 | Field | `protected static readonly System.Object EventFileOk` | `System.Windows.Forms.FileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-74958DEA92D912FE | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CE1E058025F1DB6B | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1881356D9D35349B | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D4EA89D0AF8F0183 | Method | `protected virtual System.IntPtr HookProc(System.IntPtr hWnd, System.Int32 msg, System.IntPtr wparam, System.IntPtr lparam)` | `System.Windows.Forms.FileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FF757D2FD51B4DE2 | Method | `protected System.Void OnFileOk(System.ComponentModel.CancelEventArgs e)` | `System.Windows.Forms.FileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-19A1D3D6EEE314F6 | Method | `protected virtual System.Void OnHelpRequest(System.EventArgs e)` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A91CB5906DB306AA | Method | `protected virtual System.IntPtr OwnerWndProc(System.IntPtr hWnd, System.Int32 msg, System.IntPtr wparam, System.IntPtr lparam)` | `System.Windows.Forms.CommonDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B1B6D0CBEBD35AC | Method | `protected virtual System.Boolean RunDialog(System.IntPtr hWndOwner)` | `System.Windows.Forms.FileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CD8EA56135A642EB | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F49AA6EC15E67C6 | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5EC63971409C5075 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5582AA4CEA949237 | Property | `virtual System.IntPtr Instance { protected get; }` | `System.Windows.Forms.FileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-420F0528725130DC | Property | `System.Int32 Options { protected get; }` | `System.Windows.Forms.FileDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
