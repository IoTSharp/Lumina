# TreeNode：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/TreeNode.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.TreeNode`；基类：`System.MarshalByRefObject`。Lumina 同名类型：🟡 存在；基类及基线接口形状：⬜ 未匹配。

公开属性 **33**，公开事件 **0**，公开方法 **13**；受保护扩展成员 **3**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-031454DA7640CCF2 | Constructor | `public TreeNode()` | `System.Windows.Forms.TreeNode`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B0B508FABD77F4A2 | Constructor | `public TreeNode(System.String text)` | `System.Windows.Forms.TreeNode`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9DC85B3FD8ED94A2 | Constructor | `public TreeNode(System.String text, System.Int32 imageIndex, System.Int32 selectedImageIndex)` | `System.Windows.Forms.TreeNode`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6E6FB64EB1ABD7AE | Constructor | `public TreeNode(System.String text, System.Int32 imageIndex, System.Int32 selectedImageIndex, System.Windows.Forms.TreeNode[] children)` | `System.Windows.Forms.TreeNode`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-195F9FD4F0DD446A | Constructor | `public TreeNode(System.String text, System.Windows.Forms.TreeNode[] children)` | `System.Windows.Forms.TreeNode`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1EECDF4C15176B69 | Method | `public System.Void BeginEdit()` | `System.Windows.Forms.TreeNode`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E27530BE2051008C | Method | `public virtual System.Object Clone()` | `System.Windows.Forms.TreeNode`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A33F88F6A0F022DB | Method | `public System.Void Collapse()` | `System.Windows.Forms.TreeNode`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-64C6FB69BDCF8244 | Method | `public System.Void Collapse(System.Boolean ignoreChildren)` | `System.Windows.Forms.TreeNode`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B9F92356FDD41FDF | Method | `public System.Void EndEdit(System.Boolean cancel)` | `System.Windows.Forms.TreeNode`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-81ACED345CC4B9C9 | Method | `public System.Void EnsureVisible()` | `System.Windows.Forms.TreeNode`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-30E6F1751F7D788B | Method | `public System.Void Expand()` | `System.Windows.Forms.TreeNode`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AACF07F724CEBAB2 | Method | `public System.Void ExpandAll()` | `System.Windows.Forms.TreeNode`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1BC744C6249F6EAE | Method | `public static System.Windows.Forms.TreeNode FromHandle(System.Windows.Forms.TreeView tree, System.IntPtr handle)` | `System.Windows.Forms.TreeNode`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EBF0CE9653FBD844 | Method | `public System.Int32 GetNodeCount(System.Boolean includeSubTrees)` | `System.Windows.Forms.TreeNode`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BD443225ED0613E7 | Method | `public System.Void Remove()` | `System.Windows.Forms.TreeNode`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B74901774810029B | Method | `public System.Void Toggle()` | `System.Windows.Forms.TreeNode`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9572A6215E6B04B3 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.TreeNode`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-80DB46125A25EB8C | Property | `System.Drawing.Color BackColor { public get; public set; }` | `System.Windows.Forms.TreeNode`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B96DF8B8406D69A3 | Property | `System.Drawing.Rectangle Bounds { public get; }` | `System.Windows.Forms.TreeNode` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D5FC4F8480F88F30 | Property | `System.Boolean Checked { public get; public set; }` | `System.Windows.Forms.TreeNode` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29D7D446154323A3 | Property | `virtual System.Windows.Forms.ContextMenuStrip ContextMenuStrip { public get; public set; }` | `System.Windows.Forms.TreeNode` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E1EEAB44D0CA0D69 | Property | `System.Windows.Forms.TreeNode FirstNode { public get; }` | `System.Windows.Forms.TreeNode` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ADA6ECF296697777 | Property | `System.Drawing.Color ForeColor { public get; public set; }` | `System.Windows.Forms.TreeNode`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A55E3197119C2003 | Property | `System.String FullPath { public get; }` | `System.Windows.Forms.TreeNode` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-178CB1FAA196F87A | Property | `System.IntPtr Handle { public get; }` | `System.Windows.Forms.TreeNode` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A2D065869C3B5871 | Property | `System.Int32 ImageIndex { public get; public set; }` | `System.Windows.Forms.TreeNode` LocalizableAttribute(true); TypeConverterAttribute(System.Windows.Forms.TreeViewImageIndexConverter); EditorAttribute("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); DefaultValueAttribute(-1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E1BA96A155444CF0 | Property | `System.String ImageKey { public get; public set; }` | `System.Windows.Forms.TreeNode` LocalizableAttribute(true); TypeConverterAttribute(System.Windows.Forms.TreeViewImageKeyConverter); DefaultValueAttribute(""); EditorAttribute("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5A9626758B1B2F54 | Property | `System.Int32 Index { public get; }` | `System.Windows.Forms.TreeNode`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A651DB12AF40CE5E | Property | `System.Boolean IsEditing { public get; }` | `System.Windows.Forms.TreeNode` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-371D1330C38A23A8 | Property | `System.Boolean IsExpanded { public get; }` | `System.Windows.Forms.TreeNode` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-97A7031ED2FC9376 | Property | `System.Boolean IsSelected { public get; }` | `System.Windows.Forms.TreeNode` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F146B621777F4860 | Property | `System.Boolean IsVisible { public get; }` | `System.Windows.Forms.TreeNode` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F61B6B54670B9F3F | Property | `System.Windows.Forms.TreeNode LastNode { public get; }` | `System.Windows.Forms.TreeNode` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BD487AC71AC161E3 | Property | `System.Int32 Level { public get; }` | `System.Windows.Forms.TreeNode` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D35B56214C318037 | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.TreeNode`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-AD22A7A593F6B164 | Property | `System.Windows.Forms.TreeNode NextNode { public get; }` | `System.Windows.Forms.TreeNode` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ACBF4ECC11133404 | Property | `System.Windows.Forms.TreeNode NextVisibleNode { public get; }` | `System.Windows.Forms.TreeNode` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A3430F97D46F2E91 | Property | `System.Drawing.Font NodeFont { public get; public set; }` | `System.Windows.Forms.TreeNode` LocalizableAttribute(true); DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1847EBB628DA27D9 | Property | `System.Windows.Forms.TreeNodeCollection Nodes { public get; }` | `System.Windows.Forms.TreeNode` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-50FDB19A81142240 | Property | `System.Windows.Forms.TreeNode Parent { public get; }` | `System.Windows.Forms.TreeNode` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4C17B8FA17B00E85 | Property | `System.Windows.Forms.TreeNode PrevNode { public get; }` | `System.Windows.Forms.TreeNode` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C8B62D892ADCEB1E | Property | `System.Windows.Forms.TreeNode PrevVisibleNode { public get; }` | `System.Windows.Forms.TreeNode` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-639CD3FFAA8E206B | Property | `System.Int32 SelectedImageIndex { public get; public set; }` | `System.Windows.Forms.TreeNode` LocalizableAttribute(true); TypeConverterAttribute(System.Windows.Forms.TreeViewImageIndexConverter); DefaultValueAttribute(-1); EditorAttribute("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-88350F1AE929FFDA | Property | `System.String SelectedImageKey { public get; public set; }` | `System.Windows.Forms.TreeNode` LocalizableAttribute(true); TypeConverterAttribute(System.Windows.Forms.TreeViewImageKeyConverter); DefaultValueAttribute(""); EditorAttribute("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FC11BC75BCD49AD0 | Property | `System.Int32 StateImageIndex { public get; public set; }` | `System.Windows.Forms.TreeNode` LocalizableAttribute(true); TypeConverterAttribute(System.Windows.Forms.NoneExcludedImageIndexConverter); DefaultValueAttribute(-1); EditorAttribute("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AFD468C2B986EDE0 | Property | `System.String StateImageKey { public get; public set; }` | `System.Windows.Forms.TreeNode` LocalizableAttribute(true); TypeConverterAttribute(System.Windows.Forms.ImageKeyConverter); DefaultValueAttribute(""); EditorAttribute("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-77E9E0A072267C24 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.TreeNode` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3B403529DC73E35A | Property | `System.String Text { public get; public set; }` | `System.Windows.Forms.TreeNode` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-DD16690AF1C86A15 | Property | `System.String ToolTipText { public get; public set; }` | `System.Windows.Forms.TreeNode` LocalizableAttribute(false); DefaultValueAttribute("") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-15415492DBB59502 | Property | `System.Windows.Forms.TreeView TreeView { public get; }` | `System.Windows.Forms.TreeNode` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-A6FB2AB1EA5DCD83 | Constructor | `protected TreeNode(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext context)` | `System.Windows.Forms.TreeNode`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8A2159F66CA64E60 | Method | `protected virtual System.Void Deserialize(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext context)` | `System.Windows.Forms.TreeNode`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B41427F068A57F0 | Method | `protected virtual System.Void Serialize(System.Runtime.Serialization.SerializationInfo si, System.Runtime.Serialization.StreamingContext context)` | `System.Windows.Forms.TreeNode`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
