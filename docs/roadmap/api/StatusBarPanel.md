# StatusBarPanel：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/StatusBarPanel.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.StatusBarPanel`；基类：`System.ComponentModel.Component`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **14**，公开事件 **1**，公开方法 **4**；受保护扩展成员 **6**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-E9ACBCD25A01F17E | Constructor | `public StatusBarPanel()` | `System.Windows.Forms.StatusBarPanel`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-34A1F1CD5B2CC54A | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0976BA801FCDA4E | Method | `public System.Void BeginInit()` | `System.Windows.Forms.StatusBarPanel`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FAD788244E7F42EB | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-14451A8E35099A7D | Method | `public System.Void EndInit()` | `System.Windows.Forms.StatusBarPanel`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-693C127B7C7E4348 | Method | `public virtual System.String ToString()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1CACB3CD7A28256F | Property | `System.Windows.Forms.HorizontalAlignment Alignment { public get; public set; }` | `System.Windows.Forms.StatusBarPanel` DefaultValueAttribute(0); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3A54B6C92A01D32F | Property | `System.Windows.Forms.StatusBarPanelAutoSize AutoSize { public get; public set; }` | `System.Windows.Forms.StatusBarPanel` DefaultValueAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A1B2D1F4F4D45FE3 | Property | `System.Windows.Forms.StatusBarPanelBorderStyle BorderStyle { public get; public set; }` | `System.Windows.Forms.StatusBarPanel` DefaultValueAttribute(3) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-71644EA64F960A8F | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-07200E97A2963113 | Property | `System.Drawing.Icon Icon { public get; public set; }` | `System.Windows.Forms.StatusBarPanel` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FD96F2E9758B9776 | Property | `System.Int32 MinWidth { public get; public set; }` | `System.Windows.Forms.StatusBarPanel` DefaultValueAttribute(10); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E1348C3A00DE924D | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.StatusBarPanel` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F08403744ECE271E | Property | `System.Windows.Forms.StatusBar Parent { public get; }` | `System.Windows.Forms.StatusBarPanel` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-72423792EAF7D427 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E2290408A8177607 | Property | `System.Windows.Forms.StatusBarPanelStyle Style { public get; public set; }` | `System.Windows.Forms.StatusBarPanel` DefaultValueAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4AEE9572F38A2AC1 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.StatusBarPanel` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E2DE86C17D7B7B61 | Property | `System.String Text { public get; public set; }` | `System.Windows.Forms.StatusBarPanel` LocalizableAttribute(true); DefaultValueAttribute("") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C18F151C658FC0EE | Property | `System.String ToolTipText { public get; public set; }` | `System.Windows.Forms.StatusBarPanel` LocalizableAttribute(true); DefaultValueAttribute("") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-838ECB0A986856E2 | Property | `System.Int32 Width { public get; public set; }` | `System.Windows.Forms.StatusBarPanel` LocalizableAttribute(true); DefaultValueAttribute(100) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-9791D8E648085909 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AAB7CA4A9694FB08 | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DEF1DBD32E05DD34 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C55EC817E406624F | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8979F67EF3FFEF3B | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7EB9A4CFBB3C89A7 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
