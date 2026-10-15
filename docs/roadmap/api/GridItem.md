# GridItem：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/GridItem.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.GridItem`；基类：`System.Object`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **9**，公开事件 **0**，公开方法 **1**；受保护扩展成员 **1**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-831E12658E35F1A4 | Method | `public abstract System.Boolean Select()` | `System.Windows.Forms.GridItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B767D8BC8D0E78B | Property | `virtual System.Boolean Expandable { public get; }` | `System.Windows.Forms.GridItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C2E2E4483C316187 | Property | `virtual System.Boolean Expanded { public get; public set; }` | `System.Windows.Forms.GridItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D5F30087707A23E7 | Property | `abstract System.Windows.Forms.GridItemCollection GridItems { public get; }` | `System.Windows.Forms.GridItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1E4CD0607102C252 | Property | `abstract System.Windows.Forms.GridItemType GridItemType { public get; }` | `System.Windows.Forms.GridItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0116FEA938A4755D | Property | `abstract System.String Label { public get; }` | `System.Windows.Forms.GridItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C096180221647112 | Property | `abstract System.Windows.Forms.GridItem Parent { public get; }` | `System.Windows.Forms.GridItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B3BC0EEAA572C316 | Property | `abstract System.ComponentModel.PropertyDescriptor PropertyDescriptor { public get; }` | `System.Windows.Forms.GridItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-88FC1F8CF88A013E | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.GridItem` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B4F0B8E08A62800 | Property | `abstract System.Object Value { public get; }` | `System.Windows.Forms.GridItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-F8169EC1ACFA913E | Constructor | `protected GridItem()` | `System.Windows.Forms.GridItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
