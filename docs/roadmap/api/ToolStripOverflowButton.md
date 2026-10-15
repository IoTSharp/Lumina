# ToolStripOverflowButton：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/ToolStripOverflowButton.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.ToolStripOverflowButton`；基类：`System.Windows.Forms.ToolStripDropDownButton`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **67**，公开事件 **36**，公开方法 **21**；受保护扩展成员 **70**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-86CA2D3212FA18AB | Event | `public event System.EventHandler AvailableChanged` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-97A7A84AFFB55C30 | Event | `public event System.EventHandler BackColorChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-083D80980BB0C866 | Event | `public event System.EventHandler BindingContextChanged` | `System.Windows.Forms.BindableComponent`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4CCE19F14B18B586 | Event | `public event System.EventHandler Click` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6776C22EAC650811 | Event | `public event System.EventHandler CommandCanExecuteChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8AD1A829DDF83AFF | Event | `public event System.EventHandler CommandChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C60D6F9A7D19B406 | Event | `public event System.EventHandler CommandParameterChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50FF6A4EF9BF87B3 | Event | `public event System.EventHandler DisplayStyleChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-53EA1904D5071D07 | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4131E897318B415E | Event | `public event System.EventHandler DoubleClick` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1A0CE09223BCE4CB | Event | `public event System.Windows.Forms.DragEventHandler DragDrop` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EEA438E06D19F8C4 | Event | `public event System.Windows.Forms.DragEventHandler DragEnter` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8D24DA18DBA2B0F4 | Event | `public event System.EventHandler DragLeave` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0AAE93960E9F01E7 | Event | `public event System.Windows.Forms.DragEventHandler DragOver` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3FCCD4361CEE8171 | Event | `public event System.EventHandler DropDownClosed` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-63D12905E346473E | Event | `public event System.Windows.Forms.ToolStripItemClickedEventHandler DropDownItemClicked` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AB8B266486463371 | Event | `public event System.EventHandler DropDownOpened` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D2C3F36B5D0AB620 | Event | `public event System.EventHandler DropDownOpening` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FCD035782B0E5723 | Event | `public event System.EventHandler EnabledChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8111696C58A0C8B4 | Event | `public event System.EventHandler ForeColorChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-997FBED948BE700C | Event | `public event System.Windows.Forms.GiveFeedbackEventHandler GiveFeedback` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B738839D0E6C9004 | Event | `public event System.EventHandler LocationChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-49DEC844B023B646 | Event | `public event System.Windows.Forms.MouseEventHandler MouseDown` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AD2B738AB2B3382E | Event | `public event System.EventHandler MouseEnter` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-97280EDFDA6C4132 | Event | `public event System.EventHandler MouseHover` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-80AB6268B768A794 | Event | `public event System.EventHandler MouseLeave` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1C5E640CB7CA9176 | Event | `public event System.Windows.Forms.MouseEventHandler MouseMove` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AF6BC4E73FAF9140 | Event | `public event System.Windows.Forms.MouseEventHandler MouseUp` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6FFBD49FB13DBFDC | Event | `public event System.EventHandler OwnerChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-48B780C67683DD50 | Event | `public event System.Windows.Forms.PaintEventHandler Paint` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-352A69929EBEAD34 | Event | `public event System.Windows.Forms.QueryAccessibilityHelpEventHandler QueryAccessibilityHelp` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F9DEAC5F62BF6F68 | Event | `public event System.Windows.Forms.QueryContinueDragEventHandler QueryContinueDrag` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-836C4CC1AC2D927F | Event | `public event System.EventHandler RightToLeftChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AE0D823FE9679E55 | Event | `public event System.EventHandler SelectedChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EF1E615BD7C8C87F | Event | `public event System.EventHandler TextChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B59221282E1EB8DE | Event | `public event System.EventHandler VisibleChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6540DDEE01E9B349 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A22E488DBA6F1C70 | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-636C144ED4B2D37D | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-844320DAE6FAF47F | Method | `public System.Windows.Forms.ToolStrip GetCurrentParent()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8A1CF74F4415AD9C | Method | `public virtual System.Drawing.Size GetPreferredSize(System.Drawing.Size constrainingSize)` | `System.Windows.Forms.ToolStripOverflowButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-684A51D4D3B99F15 | Method | `public System.Void HideDropDown()` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E34B95772135B8C5 | Method | `public System.Void Invalidate()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4CC9F4B95A44CBA0 | Method | `public System.Void Invalidate(System.Drawing.Rectangle r)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2708E5B7FA4FAC2E | Method | `public System.Void PerformClick()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CFF86937FC5C8E4B | Method | `public virtual System.Void ResetBackColor()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FB66FE2DF3228CD4 | Method | `public virtual System.Void ResetDisplayStyle()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1D5589E1C4B9AF11 | Method | `public virtual System.Void ResetFont()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F92E86E98ADCE9C | Method | `public virtual System.Void ResetForeColor()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-64545FF07D6B6A1B | Method | `public virtual System.Void ResetImage()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9118F881EB3DE6FC | Method | `public System.Void ResetMargin()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9076FC8A9734026C | Method | `public System.Void ResetPadding()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E591C3CF91CFAF49 | Method | `public virtual System.Void ResetRightToLeft()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-080F6C3730FE43E3 | Method | `public virtual System.Void ResetTextDirection()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-72F1B44585F65A3E | Method | `public System.Void Select()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-94D6EED822354756 | Method | `public System.Void ShowDropDown()` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B01CD8222181891 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D05C543E6606085E | Property | `System.Windows.Forms.AccessibleObject AccessibilityObject { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D0FD72BDB564324B | Property | `System.String AccessibleDefaultActionDescription { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B0E916DCD5EEF635 | Property | `System.String AccessibleDescription { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CCE257128B9D4E2D | Property | `System.String AccessibleName { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2CF008C8EDCD84B1 | Property | `System.Windows.Forms.AccessibleRole AccessibleRole { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(-1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B229FCD4D4B3E7F3 | Property | `System.Windows.Forms.ToolStripItemAlignment Alignment { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-676FBF50ADD6BD90 | Property | `virtual System.Boolean AllowDrop { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(false); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D349B12AC6127E92 | Property | `System.Windows.Forms.AnchorStyles Anchor { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0); DefaultValueAttribute(5) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8504D712C33EFE28 | Property | `System.Boolean AutoSize { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(true); LocalizableAttribute(true); DesignerSerializationVisibilityAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-36C4B11E02C8BCB1 | Property | `System.Boolean AutoToolTip { public get; public set; }` | `System.Windows.Forms.ToolStripDropDownButton` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-82160A21C4EC6611 | Property | `System.Boolean Available { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B9B6D691E1B258B7 | Property | `virtual System.Drawing.Color BackColor { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2D1E2BDF6163A1A3 | Property | `virtual System.Drawing.Image BackgroundImage { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true); DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-17CB9E20A016DD3D | Property | `virtual System.Windows.Forms.ImageLayout BackgroundImageLayout { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(1); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E306C3F5DD5F56A1 | Property | `System.Windows.Forms.BindingContext BindingContext { public get; public set; }` | `System.Windows.Forms.BindableComponent` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4EFB4EB9AD2830E1 | Property | `virtual System.Drawing.Rectangle Bounds { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7E5C33842FA9758E | Property | `virtual System.Boolean CanSelect { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3B2B1ACF59721367 | Property | `System.Windows.Input.ICommand Command { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BindableAttribute(true); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-156F9E613634B43C | Property | `System.Object CommandParameter { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BindableAttribute(true); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2F03DAC7DBCE47CE | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-40BF00E159FA1892 | Property | `System.Drawing.Rectangle ContentRectangle { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5DBDE03CFEBC6CBD | Property | `System.Windows.Forms.ControlBindingsCollection DataBindings { public get; }` | `System.Windows.Forms.BindableComponent` DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-440267CA0F6CA93A | Property | `virtual System.Windows.Forms.ToolStripItemDisplayStyle DisplayStyle { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DC3FD2C396B0E692 | Property | `System.Windows.Forms.DockStyle Dock { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BB0F69F9C96A1A13 | Property | `System.Boolean DoubleClickEnabled { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-34621ED5A87808E6 | Property | `System.Windows.Forms.ToolStripDropDown DropDown { public get; public set; }` | `System.Windows.Forms.ToolStripDropDownItem` TypeConverterAttribute(System.ComponentModel.ReferenceConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-156BC03E2FE0579F | Property | `System.Windows.Forms.ToolStripDropDownDirection DropDownDirection { public get; public set; }` | `System.Windows.Forms.ToolStripDropDownItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BC51B4E657372D32 | Property | `System.Windows.Forms.ToolStripItemCollection DropDownItems { public get; }` | `System.Windows.Forms.ToolStripDropDownItem` DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-881D0012581BE149 | Property | `virtual System.Boolean Enabled { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true); DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-12553260B556E87A | Property | `virtual System.Drawing.Font Font { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1F788E8A9837B8ED | Property | `virtual System.Drawing.Color ForeColor { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0411BA7981E03577 | Property | `System.Boolean HasDropDown { public get; }` | `System.Windows.Forms.ToolStripDropDownItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EF0BF7A5FBCAAAD9 | Property | `virtual System.Boolean HasDropDownItems { public get; }` | `System.Windows.Forms.ToolStripOverflowButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2907E58F79DA3446 | Property | `System.Int32 Height { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A5FDDB01F1765950 | Property | `virtual System.Drawing.Image Image { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-51E0461DCEDCD550 | Property | `System.Drawing.ContentAlignment ImageAlign { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(32); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3363A371E0896AF5 | Property | `System.Int32 ImageIndex { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true); TypeConverterAttribute(System.Windows.Forms.NoneExcludedImageIndexConverter); EditorAttribute("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ABEA57741DE96CBA | Property | `System.String ImageKey { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true); TypeConverterAttribute(System.Windows.Forms.ImageKeyConverter); EditorAttribute("System.Windows.Forms.Design.ToolStripImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-988E6D63EC6430DB | Property | `System.Windows.Forms.ToolStripItemImageScaling ImageScaling { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(1); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-167B5EBEF1CFB7EE | Property | `System.Drawing.Color ImageTransparentColor { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B7A6D715BDC5387B | Property | `System.Boolean IsDisposed { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F280495122FA7779 | Property | `System.Boolean IsOnDropDown { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4C35E27EE9861486 | Property | `System.Boolean IsOnOverflow { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-25F7831DCEAD8D41 | Property | `System.Windows.Forms.Padding Margin { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A59791303AB8AD82 | Property | `System.Windows.Forms.MergeAction MergeAction { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C1F219882533E11 | Property | `System.Int32 MergeIndex { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(-1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B63680374DD7996 | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AF26712899D502BC | Property | `System.Windows.Forms.ToolStripItemOverflow Overflow { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CCF310B1638298C1 | Property | `System.Windows.Forms.ToolStrip Owner { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-942DFEC9543BCF89 | Property | `System.Windows.Forms.ToolStripItem OwnerItem { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B910FD0B4F23A6DA | Property | `virtual System.Windows.Forms.Padding Padding { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6A7F37535E07652A | Property | `System.Windows.Forms.ToolStripItemPlacement Placement { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-56C359AEFC60BB1B | Property | `virtual System.Boolean Pressed { public get; }` | `System.Windows.Forms.ToolStripDropDownItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E37FAE25A200DCE | Property | `virtual System.Windows.Forms.RightToLeft RightToLeft { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C6CEFF661C5BF3FF | Property | `System.Boolean RightToLeftAutoMirrorImage { public get; public set; }` | `System.Windows.Forms.ToolStripOverflowButton` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-956B1DE99644B868 | Property | `virtual System.Boolean Selected { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6979B46CE8728BD1 | Property | `System.Boolean ShowDropDownArrow { public get; public set; }` | `System.Windows.Forms.ToolStripDropDownButton` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-687700C744A8CA83 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A21024CECD40362A | Property | `virtual System.Drawing.Size Size { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8FC89F25C3906FCB | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(null); LocalizableAttribute(false); BindableAttribute(true); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C72BA6301956F82D | Property | `virtual System.String Text { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(""); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-07C4AFB75C728EFB | Property | `virtual System.Drawing.ContentAlignment TextAlign { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(32); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C6BB3596FA63477C | Property | `virtual System.Windows.Forms.ToolStripTextDirection TextDirection { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-06F743489D0BB781 | Property | `System.Windows.Forms.TextImageRelation TextImageRelation { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(4); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-11BD2CADED16CB41 | Property | `System.String ToolTipText { public get; public set; }` | `System.Windows.Forms.ToolStripItem` EditorAttribute("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3F65C1620959E785 | Property | `System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-44556E9E52E5E81A | Property | `System.Int32 Width { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-7B5443E0ECBDD841 | Method | `protected virtual System.Windows.Forms.AccessibleObject CreateAccessibilityInstance()` | `System.Windows.Forms.ToolStripOverflowButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-860E89CB09540C9A | Method | `protected virtual System.Windows.Forms.ToolStripDropDown CreateDefaultDropDown()` | `System.Windows.Forms.ToolStripOverflowButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B58EE01CC167DD9D | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.ToolStripOverflowButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5C901077802025FE | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1E00B42740C8CB22 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C1E9E2F82CD107A0 | Method | `protected internal virtual System.Boolean IsInputChar(System.Char charCode)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DDFDFE2EBBCEF7B8 | Method | `protected internal virtual System.Boolean IsInputKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-02877C765EF3984B | Method | `protected virtual System.Void OnAvailableChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FDFAC0A58821218B | Method | `protected virtual System.Void OnBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-34D570DEB1E7A4CF | Method | `protected virtual System.Void OnBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.BindableComponent`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-041E266AA181C5D2 | Method | `protected virtual System.Void OnBoundsChanged()` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-89BD42EC79904C01 | Method | `protected virtual System.Void OnClick(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A4F227ED744348B0 | Method | `protected virtual System.Void OnCommandCanExecuteChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0DBFE97C892486B3 | Method | `protected virtual System.Void OnCommandChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-72DD3F30079486B3 | Method | `protected virtual System.Void OnCommandParameterChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-28E4EFAD8C5FC687 | Method | `protected virtual System.Void OnDisplayStyleChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BE0DA10FC7A151A6 | Method | `protected virtual System.Void OnDoubleClick(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BA11BE1CCEB0F769 | Method | `protected virtual System.Void OnDragDrop(System.Windows.Forms.DragEventArgs dragEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4989C84DB77E24BF | Method | `protected virtual System.Void OnDragEnter(System.Windows.Forms.DragEventArgs dragEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0DB4C590E37F845D | Method | `protected virtual System.Void OnDragLeave(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A2AFC55ED448CB24 | Method | `protected virtual System.Void OnDragOver(System.Windows.Forms.DragEventArgs dragEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-04D3A25683074DD4 | Method | `protected internal virtual System.Void OnDropDownClosed(System.EventArgs e)` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-526DB72CF2779112 | Method | `protected virtual System.Void OnDropDownHide(System.EventArgs e)` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E859EABC8CF0556D | Method | `protected internal virtual System.Void OnDropDownItemClicked(System.Windows.Forms.ToolStripItemClickedEventArgs e)` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D25327CC33B21A1F | Method | `protected internal virtual System.Void OnDropDownOpened(System.EventArgs e)` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9433380FA37B8389 | Method | `protected virtual System.Void OnDropDownShow(System.EventArgs e)` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1E1459529AC7A4E9 | Method | `protected virtual System.Void OnEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8B7493F66F424481 | Method | `protected virtual System.Void OnFontChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-87F017563938463A | Method | `protected virtual System.Void OnForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BF773D29C99BA2A6 | Method | `protected virtual System.Void OnGiveFeedback(System.Windows.Forms.GiveFeedbackEventArgs giveFeedbackEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E7B894EB1AC31989 | Method | `protected internal virtual System.Void OnLayout(System.Windows.Forms.LayoutEventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D2A488DB8CB51612 | Method | `protected virtual System.Void OnLocationChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EBA1841F41CA6F80 | Method | `protected virtual System.Void OnMouseDown(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.ToolStripDropDownButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5D20D5AD19C6306A | Method | `protected virtual System.Void OnMouseEnter(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D3DA22CE8C052084 | Method | `protected virtual System.Void OnMouseHover(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DD3B424017EA8573 | Method | `protected virtual System.Void OnMouseLeave(System.EventArgs e)` | `System.Windows.Forms.ToolStripDropDownButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-04DA936269D64679 | Method | `protected virtual System.Void OnMouseMove(System.Windows.Forms.MouseEventArgs mea)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6D5D705923F25E1E | Method | `protected virtual System.Void OnMouseUp(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.ToolStripDropDownButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-627F1C6E92E81248 | Method | `protected virtual System.Void OnOwnerChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-78E1D4A0AEEE4042 | Method | `protected internal virtual System.Void OnOwnerFontChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C240FC046BD9EC12 | Method | `protected virtual System.Void OnPaint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.ToolStripOverflowButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EFB126277937BD99 | Method | `protected virtual System.Void OnParentBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F0987748C703F024 | Method | `protected virtual System.Void OnParentChanged(System.Windows.Forms.ToolStrip oldParent, System.Windows.Forms.ToolStrip newParent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FC66B5A7E011AE96 | Method | `protected internal virtual System.Void OnParentEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-367AC139389F4CFF | Method | `protected virtual System.Void OnParentForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0CCE856A638D32E0 | Method | `protected internal virtual System.Void OnParentRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DC99DF4DCFF5D69C | Method | `protected virtual System.Void OnQueryContinueDrag(System.Windows.Forms.QueryContinueDragEventArgs queryContinueDragEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B919FC1289566471 | Method | `protected virtual System.Void OnRequestCommandExecute(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6859D43B382A7EFC | Method | `protected virtual System.Void OnRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5B7A3F3DFE94F315 | Method | `protected virtual System.Void OnSelectedChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5BFBC29D65867408 | Method | `protected virtual System.Void OnTextChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C850E130174099C5 | Method | `protected virtual System.Void OnVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1F0A301F9A557AEA | Method | `protected internal virtual System.Boolean ProcessCmdKey(ref System.Windows.Forms.Message m, System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-88E36E534EBB4FB4 | Method | `protected internal virtual System.Boolean ProcessDialogKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-09EB2BE2FB1F8DF0 | Method | `protected internal virtual System.Boolean ProcessMnemonic(System.Char charCode)` | `System.Windows.Forms.ToolStripDropDownButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AD5CA12C329F3CE7 | Method | `protected internal virtual System.Void SetBounds(System.Drawing.Rectangle bounds)` | `System.Windows.Forms.ToolStripOverflowButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8449CD7948AE76A6 | Method | `protected virtual System.Void SetVisibleCore(System.Boolean visible)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ADFB4B6E63E99DE2 | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-69A501D2EEAFD734 | Property | `virtual System.Boolean DefaultAutoToolTip { protected get; }` | `System.Windows.Forms.ToolStripDropDownButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C24E86911CAEB2C1 | Property | `virtual System.Windows.Forms.ToolStripItemDisplayStyle DefaultDisplayStyle { protected get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-720EB78A1B4B8194 | Property | `virtual System.Windows.Forms.Padding DefaultMargin { protected internal get; }` | `System.Windows.Forms.ToolStripOverflowButton`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6E5402F781B9171B | Property | `virtual System.Windows.Forms.Padding DefaultPadding { protected get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-41DD36FCB5CAEDF0 | Property | `virtual System.Drawing.Size DefaultSize { protected get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C5593C4C7A7CBEAA | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CC174A54604F7141 | Property | `virtual System.Boolean DismissWhenClicked { protected internal get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-79C564AAC763842E | Property | `virtual System.Drawing.Point DropDownLocation { protected internal get; }` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C10A09B3657DF984 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-30BFC54A70DD722F | Property | `System.Windows.Forms.ToolStrip Parent { protected internal get; protected internal set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4A76D72349C50268 | Property | `virtual System.Windows.Forms.ToolStripRenderer Renderer { protected internal get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-339AC47FEC1BAC59 | Property | `virtual System.Boolean ShowKeyboardCues { protected internal get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
