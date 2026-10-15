# ListViewGroup：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/ListViewGroup.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.ListViewGroup`；基类：`System.Object`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **13**，公开事件 **0**，公开方法 **1**；受保护扩展成员 **0**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-DBDE7A9C98A8561E | Constructor | `public ListViewGroup()` | `System.Windows.Forms.ListViewGroup`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-54896EF53A219B2D | Constructor | `public ListViewGroup(System.String header)` | `System.Windows.Forms.ListViewGroup`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-194E46EC07D0D43B | Constructor | `public ListViewGroup(System.String header, System.Windows.Forms.HorizontalAlignment headerAlignment)` | `System.Windows.Forms.ListViewGroup`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B2604C0CB5105471 | Constructor | `public ListViewGroup(System.String key, System.String headerText)` | `System.Windows.Forms.ListViewGroup`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-59D8D580D4DD96D3 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.ListViewGroup`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CE51EF5958ECB2C6 | Property | `System.Windows.Forms.ListViewGroupCollapsedState CollapsedState { public get; public set; }` | `System.Windows.Forms.ListViewGroup` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C101E940AFD5E533 | Property | `System.String Footer { public get; public set; }` | `System.Windows.Forms.ListViewGroup` DefaultValueAttribute("") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4A6D55F6F4F6B8E8 | Property | `System.Windows.Forms.HorizontalAlignment FooterAlignment { public get; public set; }` | `System.Windows.Forms.ListViewGroup` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6751E2F81136141E | Property | `System.String Header { public get; public set; }` | `System.Windows.Forms.ListViewGroup` DefaultValueAttribute("") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-971E29D849D180D8 | Property | `System.Windows.Forms.HorizontalAlignment HeaderAlignment { public get; public set; }` | `System.Windows.Forms.ListViewGroup` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E4D5B7F391840DF3 | Property | `System.Windows.Forms.ListView+ListViewItemCollection Items { public get; }` | `System.Windows.Forms.ListViewGroup` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-86A9835776EAD472 | Property | `System.Windows.Forms.ListView ListView { public get; }` | `System.Windows.Forms.ListViewGroup` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B5E2F6AD502EDA6 | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.ListViewGroup` BrowsableAttribute(true); DefaultValueAttribute("") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DC63E82D48406D44 | Property | `System.String Subtitle { public get; public set; }` | `System.Windows.Forms.ListViewGroup` DefaultValueAttribute("") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6430018E716E4CA5 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.ListViewGroup` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2241553BA204E75C | Property | `System.String TaskLink { public get; public set; }` | `System.Windows.Forms.ListViewGroup` DefaultValueAttribute("") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0950177E38324337 | Property | `System.Int32 TitleImageIndex { public get; public set; }` | `System.Windows.Forms.ListViewGroup` DefaultValueAttribute(-1); DesignerSerializationVisibilityAttribute(0); EditorAttribute("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); LocalizableAttribute(true); TypeConverterAttribute(System.Windows.Forms.NoneExcludedImageIndexConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ECF10FE8AEA449F5 | Property | `System.String TitleImageKey { public get; public set; }` | `System.Windows.Forms.ListViewGroup` DefaultValueAttribute(""); TypeConverterAttribute(System.Windows.Forms.ImageKeyConverter); EditorAttribute("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); DesignerSerializationVisibilityAttribute(0); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
