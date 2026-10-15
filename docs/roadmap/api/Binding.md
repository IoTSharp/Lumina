# Binding：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/Binding.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.Binding`；基类：`System.Object`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **14**，公开事件 **3**，公开方法 **2**；受保护扩展成员 **3**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-0CC6CBFBFF21CDD1 | Constructor | `public Binding(System.String propertyName, System.Object dataSource, System.String dataMember)` | `System.Windows.Forms.Binding`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DDA8716FAE998589 | Constructor | `public Binding(System.String propertyName, System.Object dataSource, System.String dataMember, System.Boolean formattingEnabled)` | `System.Windows.Forms.Binding`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-195E59F1A4A6AD86 | Constructor | `public Binding(System.String propertyName, System.Object dataSource, System.String dataMember, System.Boolean formattingEnabled, System.Windows.Forms.DataSourceUpdateMode dataSourceUpdateMode)` | `System.Windows.Forms.Binding`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7395C9F5B5107E84 | Constructor | `public Binding(System.String propertyName, System.Object dataSource, System.String dataMember, System.Boolean formattingEnabled, System.Windows.Forms.DataSourceUpdateMode dataSourceUpdateMode, System.Object nullValue)` | `System.Windows.Forms.Binding`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-144AF9D5C2C6F745 | Constructor | `public Binding(System.String propertyName, System.Object dataSource, System.String dataMember, System.Boolean formattingEnabled, System.Windows.Forms.DataSourceUpdateMode dataSourceUpdateMode, System.Object nullValue, System.String formatString)` | `System.Windows.Forms.Binding`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C2E2A1B07C993E6F | Constructor | `public Binding(System.String propertyName, System.Object dataSource, System.String dataMember, System.Boolean formattingEnabled, System.Windows.Forms.DataSourceUpdateMode dataSourceUpdateMode, System.Object nullValue, System.String formatString, System.IFormatProvider formatInfo)` | `System.Windows.Forms.Binding`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C3D5B8A7997D9D95 | Event | `public event System.Windows.Forms.BindingCompleteEventHandler BindingComplete` | `System.Windows.Forms.Binding`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8A35C869AF0FE22F | Event | `public event System.Windows.Forms.ConvertEventHandler Format` | `System.Windows.Forms.Binding`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-242A54847594BD17 | Event | `public event System.Windows.Forms.ConvertEventHandler Parse` | `System.Windows.Forms.Binding`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E134829DC849E1B9 | Method | `public System.Void ReadValue()` | `System.Windows.Forms.Binding`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F8A5A24ED5E82A29 | Method | `public System.Void WriteValue()` | `System.Windows.Forms.Binding`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-82979532FD2462FA | Property | `System.Windows.Forms.IBindableComponent BindableComponent { public get; }` | `System.Windows.Forms.Binding` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DC3DB34EB7BF29FF | Property | `System.Windows.Forms.BindingManagerBase BindingManagerBase { public get; }` | `System.Windows.Forms.Binding`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A17B16EDF0432153 | Property | `System.Windows.Forms.BindingMemberInfo BindingMemberInfo { public get; }` | `System.Windows.Forms.Binding`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-87C57FB9C7C10E68 | Property | `System.Windows.Forms.Control Control { public get; }` | `System.Windows.Forms.Binding` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B5A876A8DAEFF571 | Property | `System.Windows.Forms.ControlUpdateMode ControlUpdateMode { public get; public set; }` | `System.Windows.Forms.Binding` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B4D134ED7D92B6B8 | Property | `System.Object DataSource { public get; }` | `System.Windows.Forms.Binding`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A5B03C0695CDC5D3 | Property | `System.Object DataSourceNullValue { public get; public set; }` | `System.Windows.Forms.Binding`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-91B373F04EDE6C40 | Property | `System.Windows.Forms.DataSourceUpdateMode DataSourceUpdateMode { public get; public set; }` | `System.Windows.Forms.Binding` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-39F6A39798250EAE | Property | `System.IFormatProvider FormatInfo { public get; public set; }` | `System.Windows.Forms.Binding` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-198CD9D39A69E8BC | Property | `System.String FormatString { public get; public set; }` | `System.Windows.Forms.Binding`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6DEF30672773178B | Property | `System.Boolean FormattingEnabled { public get; public set; }` | `System.Windows.Forms.Binding` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AB48711169BAB9FC | Property | `System.Boolean IsBinding { public get; }` | `System.Windows.Forms.Binding`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-89501569C179902F | Property | `System.Object NullValue { public get; public set; }` | `System.Windows.Forms.Binding`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4B7EBDC99BB2E51A | Property | `System.String PropertyName { public get; }` | `System.Windows.Forms.Binding` DefaultValueAttribute("") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-319DC500216BD11C | Method | `protected virtual System.Void OnBindingComplete(System.Windows.Forms.BindingCompleteEventArgs e)` | `System.Windows.Forms.Binding`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-27A8353FA786ABB7 | Method | `protected virtual System.Void OnFormat(System.Windows.Forms.ConvertEventArgs cevent)` | `System.Windows.Forms.Binding`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2223815FCAB1F636 | Method | `protected virtual System.Void OnParse(System.Windows.Forms.ConvertEventArgs cevent)` | `System.Windows.Forms.Binding`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
