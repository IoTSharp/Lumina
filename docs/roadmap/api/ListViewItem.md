# ListViewItem：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/ListViewItem.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.ListViewItem`；基类：`System.Object`。Lumina 同名类型：🟡 存在；基类及基线接口形状：⬜ 未匹配。

公开属性 **22**，公开事件 **0**，公开方法 **8**；受保护扩展成员 **3**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-7D1CB325582E7DB5 | Constructor | `public ListViewItem()` | `System.Windows.Forms.ListViewItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FFCF1D21F2C817AE | Constructor | `public ListViewItem(System.String text)` | `System.Windows.Forms.ListViewItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-BA1B63D0CD25A2DA | Constructor | `public ListViewItem(System.String text, System.Int32 imageIndex)` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2093F9A715A7667E | Constructor | `public ListViewItem(System.String text, System.Int32 imageIndex, System.Windows.Forms.ListViewGroup group)` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6D3196770AD1CEDD | Constructor | `public ListViewItem(System.String text, System.String imageKey)` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B56F181EDAECDAE2 | Constructor | `public ListViewItem(System.String text, System.String imageKey, System.Windows.Forms.ListViewGroup group)` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8C14F6A12E6121CF | Constructor | `public ListViewItem(System.String text, System.Windows.Forms.ListViewGroup group)` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-405EB46AC0D127BE | Constructor | `public ListViewItem(System.String[] items)` | `System.Windows.Forms.ListViewItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-DDBC9847F0010C58 | Constructor | `public ListViewItem(System.String[] items, System.Int32 imageIndex)` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-08C1CBA149319CF3 | Constructor | `public ListViewItem(System.String[] items, System.Int32 imageIndex, System.Drawing.Color foreColor, System.Drawing.Color backColor, System.Drawing.Font font)` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-97C4F5747803DFAC | Constructor | `public ListViewItem(System.String[] items, System.Int32 imageIndex, System.Drawing.Color foreColor, System.Drawing.Color backColor, System.Drawing.Font font, System.Windows.Forms.ListViewGroup group)` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-73A7158790D78D21 | Constructor | `public ListViewItem(System.String[] items, System.Int32 imageIndex, System.Windows.Forms.ListViewGroup group)` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-81FBD59E52545FB2 | Constructor | `public ListViewItem(System.String[] items, System.String imageKey)` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FFC4406FEDBFADA9 | Constructor | `public ListViewItem(System.String[] items, System.String imageKey, System.Drawing.Color foreColor, System.Drawing.Color backColor, System.Drawing.Font font)` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9F6A18174FCECF39 | Constructor | `public ListViewItem(System.String[] items, System.String imageKey, System.Drawing.Color foreColor, System.Drawing.Color backColor, System.Drawing.Font font, System.Windows.Forms.ListViewGroup group)` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BF58F8427B489CF6 | Constructor | `public ListViewItem(System.String[] items, System.String imageKey, System.Windows.Forms.ListViewGroup group)` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2A8382703304885C | Constructor | `public ListViewItem(System.String[] items, System.Windows.Forms.ListViewGroup group)` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5A754E8733EF1D36 | Constructor | `public ListViewItem(System.Windows.Forms.ListViewGroup group)` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3B1686B71860E9E1 | Constructor | `public ListViewItem(System.Windows.Forms.ListViewItem+ListViewSubItem[] subItems, System.Int32 imageIndex)` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-497BFD5434D3CA44 | Constructor | `public ListViewItem(System.Windows.Forms.ListViewItem+ListViewSubItem[] subItems, System.Int32 imageIndex, System.Windows.Forms.ListViewGroup group)` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DE7BEF0148B40A18 | Constructor | `public ListViewItem(System.Windows.Forms.ListViewItem+ListViewSubItem[] subItems, System.String imageKey)` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CAD3217879686125 | Constructor | `public ListViewItem(System.Windows.Forms.ListViewItem+ListViewSubItem[] subItems, System.String imageKey, System.Windows.Forms.ListViewGroup group)` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BBAD22C36231B291 | Method | `public System.Void BeginEdit()` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-18BB2739B232383C | Method | `public virtual System.Object Clone()` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E0E5851DA0AAB96 | Method | `public virtual System.Void EnsureVisible()` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-55AE3F18D5A31CD0 | Method | `public System.Windows.Forms.ListViewItem FindNearestItem(System.Windows.Forms.SearchDirectionHint searchDirection)` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-88468EA83D1F8B6F | Method | `public System.Drawing.Rectangle GetBounds(System.Windows.Forms.ItemBoundsPortion portion)` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-15C789A43F553CFB | Method | `public System.Windows.Forms.ListViewItem+ListViewSubItem GetSubItemAt(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4789C4BF263BAA21 | Method | `public virtual System.Void Remove()` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3FB2846495B929DE | Method | `public virtual System.String ToString()` | `System.Windows.Forms.ListViewItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-EFA2B3ECD0D94E6A | Property | `System.Drawing.Color BackColor { public get; public set; }` | `System.Windows.Forms.ListViewItem` DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A724888CD778CEF5 | Property | `System.Drawing.Rectangle Bounds { public get; }` | `System.Windows.Forms.ListViewItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1F59284D73BCFD70 | Property | `System.Boolean Checked { public get; public set; }` | `System.Windows.Forms.ListViewItem` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-78F9F913FD9576B7 | Property | `System.Boolean Focused { public get; public set; }` | `System.Windows.Forms.ListViewItem` DesignerSerializationVisibilityAttribute(0); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-92C5089F19C5EDC9 | Property | `System.Drawing.Font Font { public get; public set; }` | `System.Windows.Forms.ListViewItem` LocalizableAttribute(true); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B239FB8B72A52F83 | Property | `System.Drawing.Color ForeColor { public get; public set; }` | `System.Windows.Forms.ListViewItem` DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-BAADBF6FAABF8AA9 | Property | `System.Windows.Forms.ListViewGroup Group { public get; public set; }` | `System.Windows.Forms.ListViewItem` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DA45AE71FC18C8FD | Property | `System.Int32 ImageIndex { public get; public set; }` | `System.Windows.Forms.ListViewItem` DefaultValueAttribute(-1); DesignerSerializationVisibilityAttribute(0); EditorAttribute("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); LocalizableAttribute(true); TypeConverterAttribute(System.Windows.Forms.NoneExcludedImageIndexConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3801478A8E7AC53A | Property | `System.String ImageKey { public get; public set; }` | `System.Windows.Forms.ListViewItem` DefaultValueAttribute(""); TypeConverterAttribute(System.Windows.Forms.ImageKeyConverter); EditorAttribute("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); DesignerSerializationVisibilityAttribute(0); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-60AAEB6AB15F1168 | Property | `System.Windows.Forms.ImageList ImageList { public get; }` | `System.Windows.Forms.ListViewItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-387AA7C69740DF53 | Property | `System.Int32 IndentCount { public get; public set; }` | `System.Windows.Forms.ListViewItem` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-38B16C8AD994C091 | Property | `System.Int32 Index { public get; }` | `System.Windows.Forms.ListViewItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B735FC36C5F0D13 | Property | `System.Windows.Forms.ListView ListView { public get; }` | `System.Windows.Forms.ListViewItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C830576299411E7 | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.ListViewItem` LocalizableAttribute(true); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D4789F6DF6C350EF | Property | `System.Drawing.Point Position { public get; public set; }` | `System.Windows.Forms.ListViewItem` DesignerSerializationVisibilityAttribute(0); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D73342F4C6CB1ECE | Property | `System.Boolean Selected { public get; public set; }` | `System.Windows.Forms.ListViewItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BBB2E0028F4AA688 | Property | `System.Int32 StateImageIndex { public get; public set; }` | `System.Windows.Forms.ListViewItem` LocalizableAttribute(true); TypeConverterAttribute(System.Windows.Forms.NoneExcludedImageIndexConverter); DefaultValueAttribute(-1); EditorAttribute("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-879A9B63B5A72915 | Property | `System.Windows.Forms.ListViewItem+ListViewSubItemCollection SubItems { public get; }` | `System.Windows.Forms.ListViewItem` DesignerSerializationVisibilityAttribute(0); EditorAttribute("System.Windows.Forms.Design.ListViewSubItemCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-424302643D86D879 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.ListViewItem` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-EEB7717F6AC80375 | Property | `System.String Text { public get; public set; }` | `System.Windows.Forms.ListViewItem` LocalizableAttribute(true); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FB4CBE09ED09AA30 | Property | `System.String ToolTipText { public get; public set; }` | `System.Windows.Forms.ListViewItem` DefaultValueAttribute("") | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-26C586548468FBAB | Property | `System.Boolean UseItemStyleForSubItems { public get; public set; }` | `System.Windows.Forms.ListViewItem` DefaultValueAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-BCCE99114C9B80B4 | Constructor | `protected ListViewItem(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DB7ADBFA7FC265EE | Method | `protected virtual System.Void Deserialize(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F02F429FAC237E6 | Method | `protected virtual System.Void Serialize(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)` | `System.Windows.Forms.ListViewItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
