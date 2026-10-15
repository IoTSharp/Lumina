# ToolStripDropDownItem：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/ToolStripDropDownItem.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.ToolStripDropDownItem`；基类：`System.Windows.Forms.ToolStripItem`。Lumina 同名类型：🟡 存在；基类及基线接口形状：⬜ 未匹配。

公开属性 **66**，公开事件 **36**，公开方法 **21**；受保护扩展成员 **74**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-746D750E6EF310CF | Event | `public event System.EventHandler AvailableChanged` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3B193C49182BAEE5 | Event | `public event System.EventHandler BackColorChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2D31470E3616B7B2 | Event | `public event System.EventHandler BindingContextChanged` | `System.Windows.Forms.BindableComponent`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8980B016A1684775 | Event | `public event System.EventHandler Click` | `System.Windows.Forms.ToolStripItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B4E2A12F55503E31 | Event | `public event System.EventHandler CommandCanExecuteChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D9154A769B3EEBA4 | Event | `public event System.EventHandler CommandChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FC9C0D5D47ADCEDB | Event | `public event System.EventHandler CommandParameterChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-09748B784DEEEB79 | Event | `public event System.EventHandler DisplayStyleChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4CD7FAEB326D90CA | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C03E24970E61DEA8 | Event | `public event System.EventHandler DoubleClick` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-360446C2F237FBFC | Event | `public event System.Windows.Forms.DragEventHandler DragDrop` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7A9AE2FCDD38F8E2 | Event | `public event System.Windows.Forms.DragEventHandler DragEnter` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F44B82E1223BB8D | Event | `public event System.EventHandler DragLeave` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A8981E0C3DCEA119 | Event | `public event System.Windows.Forms.DragEventHandler DragOver` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D4C4452E929B65A4 | Event | `public event System.EventHandler DropDownClosed` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7FAC58C4B70D4DCB | Event | `public event System.Windows.Forms.ToolStripItemClickedEventHandler DropDownItemClicked` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A1DFAE5672F80769 | Event | `public event System.EventHandler DropDownOpened` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FB248F4B49AC3564 | Event | `public event System.EventHandler DropDownOpening` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5B1B98CE3DABEFB7 | Event | `public event System.EventHandler EnabledChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-033377A98939C1BD | Event | `public event System.EventHandler ForeColorChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BCF54C881581715B | Event | `public event System.Windows.Forms.GiveFeedbackEventHandler GiveFeedback` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E0DF69D2EFDACA1 | Event | `public event System.EventHandler LocationChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6731BB32740813A8 | Event | `public event System.Windows.Forms.MouseEventHandler MouseDown` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5C746C9F4B47C364 | Event | `public event System.EventHandler MouseEnter` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-57F50DF48CF36465 | Event | `public event System.EventHandler MouseHover` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AF04FAC944D2258B | Event | `public event System.EventHandler MouseLeave` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7ECADF26ACC34BA4 | Event | `public event System.Windows.Forms.MouseEventHandler MouseMove` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC7896C136AE1AAD | Event | `public event System.Windows.Forms.MouseEventHandler MouseUp` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F3A5B06A776FC5AA | Event | `public event System.EventHandler OwnerChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2D5D9D310A8DE61A | Event | `public event System.Windows.Forms.PaintEventHandler Paint` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F1238B7162A69926 | Event | `public event System.Windows.Forms.QueryAccessibilityHelpEventHandler QueryAccessibilityHelp` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F192260DF8599E1D | Event | `public event System.Windows.Forms.QueryContinueDragEventHandler QueryContinueDrag` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E245AE93DF3C2189 | Event | `public event System.EventHandler RightToLeftChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BD42A453B0766899 | Event | `public event System.EventHandler SelectedChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-35F15899655400E4 | Event | `public event System.EventHandler TextChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-10C009FB95246194 | Event | `public event System.EventHandler VisibleChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-15F58055C841A3E0 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1556BD239F92BCC0 | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-297CB33DB7A5B942 | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D6B33090F6E0F18 | Method | `public System.Windows.Forms.ToolStrip GetCurrentParent()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E28028DE4B8A7FB7 | Method | `public virtual System.Drawing.Size GetPreferredSize(System.Drawing.Size constrainingSize)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AB7A85465436B133 | Method | `public System.Void HideDropDown()` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DF7193565DD9F4CE | Method | `public System.Void Invalidate()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-31AE4FD47F953C91 | Method | `public System.Void Invalidate(System.Drawing.Rectangle r)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EAD0364C7C551028 | Method | `public System.Void PerformClick()` | `System.Windows.Forms.ToolStripItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5E930956BFFEFE12 | Method | `public virtual System.Void ResetBackColor()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-292CB09978B85D1E | Method | `public virtual System.Void ResetDisplayStyle()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-872899390A0BD052 | Method | `public virtual System.Void ResetFont()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FE1EF162B4B08442 | Method | `public virtual System.Void ResetForeColor()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0FE6859FA5C711EF | Method | `public virtual System.Void ResetImage()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3072642259FC8029 | Method | `public System.Void ResetMargin()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4DA19E5DF59E83DC | Method | `public System.Void ResetPadding()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CE90A449540FBDB1 | Method | `public virtual System.Void ResetRightToLeft()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3C289C82E88C216F | Method | `public virtual System.Void ResetTextDirection()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EA5F4280BA5CAD96 | Method | `public System.Void Select()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E96785D1EB43DE84 | Method | `public System.Void ShowDropDown()` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F938FB7A00750B4C | Method | `public virtual System.String ToString()` | `System.Windows.Forms.ToolStripItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-417FFFE2B6D01D37 | Property | `System.Windows.Forms.AccessibleObject AccessibilityObject { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DA7DD63F6DC98A22 | Property | `System.String AccessibleDefaultActionDescription { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-353AFF525563DBA4 | Property | `System.String AccessibleDescription { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1FA5E67A1EE1FCA3 | Property | `System.String AccessibleName { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6649E48C7DEB8C5B | Property | `System.Windows.Forms.AccessibleRole AccessibleRole { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(-1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FBAEA9A22CC47E9B | Property | `System.Windows.Forms.ToolStripItemAlignment Alignment { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F90F8988C2B1835F | Property | `virtual System.Boolean AllowDrop { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(false); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D8E5E75D66A13193 | Property | `System.Windows.Forms.AnchorStyles Anchor { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0); DefaultValueAttribute(5) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3D33C54777EAAF84 | Property | `System.Boolean AutoSize { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(true); LocalizableAttribute(true); DesignerSerializationVisibilityAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5ED57739F717EDEB | Property | `System.Boolean AutoToolTip { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-08077362FB5F97B4 | Property | `System.Boolean Available { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F6A4E482F827A253 | Property | `virtual System.Drawing.Color BackColor { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D0472B8314B5862B | Property | `virtual System.Drawing.Image BackgroundImage { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true); DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0013AEC1FCC58310 | Property | `virtual System.Windows.Forms.ImageLayout BackgroundImageLayout { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(1); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E8C0403B44AFC30 | Property | `System.Windows.Forms.BindingContext BindingContext { public get; public set; }` | `System.Windows.Forms.BindableComponent` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D68C7BD727A4030F | Property | `virtual System.Drawing.Rectangle Bounds { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B129563073794E10 | Property | `virtual System.Boolean CanSelect { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FA65F50A3FFCEB1C | Property | `System.Windows.Input.ICommand Command { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BindableAttribute(true); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7F2EB6EEB86C8085 | Property | `System.Object CommandParameter { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BindableAttribute(true); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A6B11868CB58DAC4 | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-49006A34740E4595 | Property | `System.Drawing.Rectangle ContentRectangle { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-65459256B2088EA8 | Property | `System.Windows.Forms.ControlBindingsCollection DataBindings { public get; }` | `System.Windows.Forms.BindableComponent` DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-58DBEBCFCCB2F714 | Property | `virtual System.Windows.Forms.ToolStripItemDisplayStyle DisplayStyle { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5FCC2A0525333153 | Property | `System.Windows.Forms.DockStyle Dock { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-79086A972128E671 | Property | `System.Boolean DoubleClickEnabled { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5B9B7AD0F7A41141 | Property | `System.Windows.Forms.ToolStripDropDown DropDown { public get; public set; }` | `System.Windows.Forms.ToolStripDropDownItem` TypeConverterAttribute(System.ComponentModel.ReferenceConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-830F5FBFEFFD0340 | Property | `System.Windows.Forms.ToolStripDropDownDirection DropDownDirection { public get; public set; }` | `System.Windows.Forms.ToolStripDropDownItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2712ECBE002507BE | Property | `System.Windows.Forms.ToolStripItemCollection DropDownItems { public get; }` | `System.Windows.Forms.ToolStripDropDownItem` DesignerSerializationVisibilityAttribute(2) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-081AF13E62E1476A | Property | `virtual System.Boolean Enabled { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true); DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4F0443C07A1C52CF | Property | `virtual System.Drawing.Font Font { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4FBD1220DCF08708 | Property | `virtual System.Drawing.Color ForeColor { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AB35BF9E34645655 | Property | `System.Boolean HasDropDown { public get; }` | `System.Windows.Forms.ToolStripDropDownItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CD5E7C2D0FBE6FA1 | Property | `virtual System.Boolean HasDropDownItems { public get; }` | `System.Windows.Forms.ToolStripDropDownItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC0D24B5344A9011 | Property | `System.Int32 Height { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E304943646159F5A | Property | `virtual System.Drawing.Image Image { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-885DD1552ACC890C | Property | `System.Drawing.ContentAlignment ImageAlign { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(32); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-69DF067DD76237BF | Property | `System.Int32 ImageIndex { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true); TypeConverterAttribute(System.Windows.Forms.NoneExcludedImageIndexConverter); EditorAttribute("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-873F0C935F26DAFF | Property | `System.String ImageKey { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true); TypeConverterAttribute(System.Windows.Forms.ImageKeyConverter); EditorAttribute("System.Windows.Forms.Design.ToolStripImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D8C3468638ECF7AA | Property | `System.Windows.Forms.ToolStripItemImageScaling ImageScaling { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(1); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-72FFC09E9F519A25 | Property | `System.Drawing.Color ImageTransparentColor { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-48D060F73B572C7F | Property | `System.Boolean IsDisposed { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-92956D83FDB0BA99 | Property | `System.Boolean IsOnDropDown { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1E5807809F067A0C | Property | `System.Boolean IsOnOverflow { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C047D64758DB1BE9 | Property | `System.Windows.Forms.Padding Margin { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A3BCB5321893FB0F | Property | `System.Windows.Forms.MergeAction MergeAction { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BC8C7FAB8EEF9205 | Property | `System.Int32 MergeIndex { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(-1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-675C9F580559E818 | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DefaultValueAttribute(null) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-352F7BD8F5C4A981 | Property | `System.Windows.Forms.ToolStripItemOverflow Overflow { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F3FF92A5E6B976A9 | Property | `System.Windows.Forms.ToolStrip Owner { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7CCC3F106AC96C78 | Property | `System.Windows.Forms.ToolStripItem OwnerItem { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1D207AAC412ABB41 | Property | `virtual System.Windows.Forms.Padding Padding { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3416ED2E690BC9A4 | Property | `System.Windows.Forms.ToolStripItemPlacement Placement { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4B49C93EB6397E18 | Property | `virtual System.Boolean Pressed { public get; }` | `System.Windows.Forms.ToolStripDropDownItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-92078C4A9C78C772 | Property | `virtual System.Windows.Forms.RightToLeft RightToLeft { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9AE6D61E12020110 | Property | `System.Boolean RightToLeftAutoMirrorImage { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(false); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A88C01744C915E72 | Property | `virtual System.Boolean Selected { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-913F2985ABFC9A9B | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-8AFCFE59ADFCFDD0 | Property | `virtual System.Drawing.Size Size { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A5156BD1E47B96D8 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(null); LocalizableAttribute(false); BindableAttribute(true); TypeConverterAttribute(System.ComponentModel.StringConverter) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6114CA2EBC650404 | Property | `virtual System.String Text { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(""); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DE7D73E19ACD758D | Property | `virtual System.Drawing.ContentAlignment TextAlign { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(32); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-93887FA9BDB5DBA6 | Property | `virtual System.Windows.Forms.ToolStripTextDirection TextDirection { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7DF7257224FC371B | Property | `System.Windows.Forms.TextImageRelation TextImageRelation { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(4); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-49B1C7B2F398AB56 | Property | `System.String ToolTipText { public get; public set; }` | `System.Windows.Forms.ToolStripItem` EditorAttribute("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-379DC82450AFAEE6 | Property | `System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-09EC34A6248024AB | Property | `System.Int32 Width { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-0BBC5365D26AA658 | Constructor | `protected ToolStripDropDownItem()` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-02587D8E9FEAD020 | Constructor | `protected ToolStripDropDownItem(System.String text, System.Drawing.Image image, System.EventHandler onClick)` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B5BE29D3BA045050 | Constructor | `protected ToolStripDropDownItem(System.String text, System.Drawing.Image image, System.EventHandler onClick, System.String name)` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FAB1D9A56279FFD1 | Constructor | `protected ToolStripDropDownItem(System.String text, System.Drawing.Image image, System.Windows.Forms.ToolStripItem[] dropDownItems)` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16563817B6528F08 | Method | `protected virtual System.Windows.Forms.AccessibleObject CreateAccessibilityInstance()` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-33AD56E2E6EDAFA6 | Method | `protected virtual System.Windows.Forms.ToolStripDropDown CreateDefaultDropDown()` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BEB712C05FA98E29 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.ToolStripDropDownItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FE76163296A6A8C3 | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E497A832CC6B92B5 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-BA3BC5D283C55097 | Method | `protected internal virtual System.Boolean IsInputChar(System.Char charCode)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7CF104DE6A2B6018 | Method | `protected internal virtual System.Boolean IsInputKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1E4DAB2DEA2A5991 | Method | `protected virtual System.Void OnAvailableChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2DF18133DA346DC1 | Method | `protected virtual System.Void OnBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-001FC74A2EB1DAE8 | Method | `protected virtual System.Void OnBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.BindableComponent`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B395A81A5D7D4F61 | Method | `protected virtual System.Void OnBoundsChanged()` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9850F7A43E2E7B31 | Method | `protected virtual System.Void OnClick(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5122ECB94ABF9690 | Method | `protected virtual System.Void OnCommandCanExecuteChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2F5F560BEAE21AD7 | Method | `protected virtual System.Void OnCommandChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F433A6D9592E43D6 | Method | `protected virtual System.Void OnCommandParameterChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0978FD577718925 | Method | `protected virtual System.Void OnDisplayStyleChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4077D6B566A3CF25 | Method | `protected virtual System.Void OnDoubleClick(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D4354F822E3487ED | Method | `protected virtual System.Void OnDragDrop(System.Windows.Forms.DragEventArgs dragEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3AEED48789E54528 | Method | `protected virtual System.Void OnDragEnter(System.Windows.Forms.DragEventArgs dragEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DF8704AAB3C5195C | Method | `protected virtual System.Void OnDragLeave(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-617772297B44FFC3 | Method | `protected virtual System.Void OnDragOver(System.Windows.Forms.DragEventArgs dragEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-44CA47F3A5149EA2 | Method | `protected internal virtual System.Void OnDropDownClosed(System.EventArgs e)` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-89AFE482F32CAE62 | Method | `protected virtual System.Void OnDropDownHide(System.EventArgs e)` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3575E9F05DD12924 | Method | `protected internal virtual System.Void OnDropDownItemClicked(System.Windows.Forms.ToolStripItemClickedEventArgs e)` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-00FA0699BD51DD54 | Method | `protected internal virtual System.Void OnDropDownOpened(System.EventArgs e)` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2042C447A8BF88F0 | Method | `protected virtual System.Void OnDropDownShow(System.EventArgs e)` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B6DFAC756713DB6 | Method | `protected virtual System.Void OnEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E1602FAD4A36927 | Method | `protected virtual System.Void OnFontChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-34986894C47A6CD1 | Method | `protected virtual System.Void OnForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-161EE0AE0C37A07E | Method | `protected virtual System.Void OnGiveFeedback(System.Windows.Forms.GiveFeedbackEventArgs giveFeedbackEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3E8B9BC28379625A | Method | `protected internal virtual System.Void OnLayout(System.Windows.Forms.LayoutEventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C325EE74BE24B317 | Method | `protected virtual System.Void OnLocationChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-228135039CE73FE1 | Method | `protected virtual System.Void OnMouseDown(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F5CFD34DD11C9B66 | Method | `protected virtual System.Void OnMouseEnter(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CE41ED98090971A7 | Method | `protected virtual System.Void OnMouseHover(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4DCA530296E0CFEF | Method | `protected virtual System.Void OnMouseLeave(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E8C0B719B1848857 | Method | `protected virtual System.Void OnMouseMove(System.Windows.Forms.MouseEventArgs mea)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-300C7F60D236305E | Method | `protected virtual System.Void OnMouseUp(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BA2483656539F79A | Method | `protected virtual System.Void OnOwnerChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-692A5EB6A64B8590 | Method | `protected internal virtual System.Void OnOwnerFontChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F82AB01D4670DDEE | Method | `protected virtual System.Void OnPaint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-341DE3DACC5F82E9 | Method | `protected virtual System.Void OnParentBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AD4E122E4522CEFF | Method | `protected virtual System.Void OnParentChanged(System.Windows.Forms.ToolStrip oldParent, System.Windows.Forms.ToolStrip newParent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-85E4F102E8E22574 | Method | `protected internal virtual System.Void OnParentEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-39F796B73D7EF192 | Method | `protected virtual System.Void OnParentForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AB57075BB76617E2 | Method | `protected internal virtual System.Void OnParentRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7EFAFDA8FB7A2B96 | Method | `protected virtual System.Void OnQueryContinueDrag(System.Windows.Forms.QueryContinueDragEventArgs queryContinueDragEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FD9C044ED16BE371 | Method | `protected virtual System.Void OnRequestCommandExecute(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D1306942B75A09DE | Method | `protected virtual System.Void OnRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BE98186DD19AF299 | Method | `protected virtual System.Void OnSelectedChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CB1A4E370462BB84 | Method | `protected virtual System.Void OnTextChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4B41E5D8B9877B95 | Method | `protected virtual System.Void OnVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A52C9BB8EBE985F | Method | `protected internal virtual System.Boolean ProcessCmdKey(ref System.Windows.Forms.Message m, System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D5CD3AD06D6ABBDC | Method | `protected internal virtual System.Boolean ProcessDialogKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-61C390B0D178DBA0 | Method | `protected internal virtual System.Boolean ProcessMnemonic(System.Char charCode)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5CA7905492C11528 | Method | `protected internal virtual System.Void SetBounds(System.Drawing.Rectangle bounds)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F297871F3C4B7FA6 | Method | `protected virtual System.Void SetVisibleCore(System.Boolean visible)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-28EC15E53348B851 | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1E5DC0D8900442D0 | Property | `virtual System.Boolean DefaultAutoToolTip { protected get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AD470921A99DD407 | Property | `virtual System.Windows.Forms.ToolStripItemDisplayStyle DefaultDisplayStyle { protected get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-663347DA7B61C4C0 | Property | `virtual System.Windows.Forms.Padding DefaultMargin { protected internal get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6B268CF8B663EBA7 | Property | `virtual System.Windows.Forms.Padding DefaultPadding { protected get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9DA0909ACB43FA9A | Property | `virtual System.Drawing.Size DefaultSize { protected get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-19C4B6CDA5A80696 | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F3665028085FF96A | Property | `virtual System.Boolean DismissWhenClicked { protected internal get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5224D200A9F4BEBA | Property | `virtual System.Drawing.Point DropDownLocation { protected internal get; }` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B8D1050A369B6607 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-DEBC26D326B9579C | Property | `System.Windows.Forms.ToolStrip Parent { protected internal get; protected internal set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9CEF166004C5A072 | Property | `virtual System.Windows.Forms.ToolStripRenderer Renderer { protected internal get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ED06B39747369CA1 | Property | `virtual System.Boolean ShowKeyboardCues { protected internal get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
