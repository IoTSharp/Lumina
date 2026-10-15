# ToolStripPanelRow：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/ToolStripPanelRow.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.ToolStripPanelRow`；基类：`System.ComponentModel.Component`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **10**，公开事件 **1**，公开方法 **3**；受保护扩展成员 **13**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-9C3A02127025A7B4 | Constructor | `public ToolStripPanelRow(System.Windows.Forms.ToolStripPanel parent)` | `System.Windows.Forms.ToolStripPanelRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9F296540F540DDA7 | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4A2E762102020F00 | Method | `public System.Boolean CanMove(System.Windows.Forms.ToolStrip toolStripToDrag)` | `System.Windows.Forms.ToolStripPanelRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-54C788B870CF09E8 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-65A86792806E0FAD | Method | `public virtual System.String ToString()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E614E7898AA8F63A | Property | `System.Drawing.Rectangle Bounds { public get; }` | `System.Windows.Forms.ToolStripPanelRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A2F0EB139F84E2F2 | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-188B186F78E3E109 | Property | `System.Windows.Forms.Control[] Controls { public get; }` | `System.Windows.Forms.ToolStripPanelRow` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C8046F762A7E592D | Property | `System.Drawing.Rectangle DisplayRectangle { public get; }` | `System.Windows.Forms.ToolStripPanelRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC9C39080C0FCF70 | Property | `System.Windows.Forms.Layout.LayoutEngine LayoutEngine { public get; }` | `System.Windows.Forms.ToolStripPanelRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-81C728A2B854EEBA | Property | `System.Windows.Forms.Padding Margin { public get; public set; }` | `System.Windows.Forms.ToolStripPanelRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7C3D795D989BE2FB | Property | `System.Windows.Forms.Orientation Orientation { public get; }` | `System.Windows.Forms.ToolStripPanelRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-43CF2B4E0C4AE53E | Property | `virtual System.Windows.Forms.Padding Padding { public get; public set; }` | `System.Windows.Forms.ToolStripPanelRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2DD28AFCADE5FF06 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-44BE23285FA1D94C | Property | `System.Windows.Forms.ToolStripPanel ToolStripPanel { public get; }` | `System.Windows.Forms.ToolStripPanelRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-F0BD150F10444825 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.ToolStripPanelRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C216A0BD591D6889 | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-18E8D83E0670EEFF | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A257C9AF8057D3D7 | Method | `protected System.Void OnBoundsChanged(System.Drawing.Rectangle oldBounds, System.Drawing.Rectangle newBounds)` | `System.Windows.Forms.ToolStripPanelRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-614C6C8F2A89E1D8 | Method | `protected internal virtual System.Void OnControlAdded(System.Windows.Forms.Control control, System.Int32 index)` | `System.Windows.Forms.ToolStripPanelRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-28FBD01F9A1576A6 | Method | `protected internal virtual System.Void OnControlRemoved(System.Windows.Forms.Control control, System.Int32 index)` | `System.Windows.Forms.ToolStripPanelRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B841CA812E8FCBBD | Method | `protected virtual System.Void OnLayout(System.Windows.Forms.LayoutEventArgs e)` | `System.Windows.Forms.ToolStripPanelRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AF0BE651585A4B62 | Method | `protected internal virtual System.Void OnOrientationChanged()` | `System.Windows.Forms.ToolStripPanelRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0AA7C1102F8E7239 | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-13376AA20402C8F6 | Property | `virtual System.Windows.Forms.Padding DefaultMargin { protected get; }` | `System.Windows.Forms.ToolStripPanelRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FF34E665C175B90D | Property | `virtual System.Windows.Forms.Padding DefaultPadding { protected get; }` | `System.Windows.Forms.ToolStripPanelRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-45D1AA1453418017 | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DFA88B59D014173B | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
