# ListViewSubItem：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/ListViewSubItem.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.ListViewItem+ListViewSubItem`；基类：`System.Object`。Lumina 同名类型：🟡 存在；基类及基线接口形状：🟡 初步匹配，待源码消费验证。

公开属性 **7**，公开事件 **0**，公开方法 **2**；受保护扩展成员 **0**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-86B84D4F2AB94E7D | Constructor | `public ListViewSubItem()` | `System.Windows.Forms.ListViewItem+ListViewSubItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5B5E15D6AC1CF3C0 | Constructor | `public ListViewSubItem(System.Windows.Forms.ListViewItem owner, System.String text)` | `System.Windows.Forms.ListViewItem+ListViewSubItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-DE6B1F13CD855B90 | Constructor | `public ListViewSubItem(System.Windows.Forms.ListViewItem owner, System.String text, System.Drawing.Color foreColor, System.Drawing.Color backColor, System.Drawing.Font font)` | `System.Windows.Forms.ListViewItem+ListViewSubItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-81FFED0D673E83AE | Method | `public System.Void ResetStyle()` | `System.Windows.Forms.ListViewItem+ListViewSubItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-033D2304C3701549 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.ListViewItem+ListViewSubItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-8C57E8D5177797BE | Property | `System.Drawing.Color BackColor { public get; public set; }` | `System.Windows.Forms.ListViewItem+ListViewSubItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C4EBF62201EC3185 | Property | `System.Drawing.Rectangle Bounds { public get; }` | `System.Windows.Forms.ListViewItem+ListViewSubItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7F0EAA948BD5FD37 | Property | `System.Drawing.Font Font { public get; public set; }` | `System.Windows.Forms.ListViewItem+ListViewSubItem` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C9E39C9B2E2904C9 | Property | `System.Drawing.Color ForeColor { public get; public set; }` | `System.Windows.Forms.ListViewItem+ListViewSubItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5EAD28D071388269 | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.ListViewItem+ListViewSubItem` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-83D607805F1614DA | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.ListViewItem+ListViewSubItem` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A47B45CDD438D937 | Property | `System.String Text { public get; public set; }` | `System.Windows.Forms.ListViewItem+ListViewSubItem` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
