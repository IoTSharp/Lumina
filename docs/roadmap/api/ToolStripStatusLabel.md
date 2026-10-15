# ToolStripStatusLabel：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/ToolStripStatusLabel.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.ToolStripStatusLabel`；基类：`System.Windows.Forms.ToolStripLabel`。Lumina 同名类型：🟡 存在；基类及基线接口形状：⬜ 未匹配。

公开属性 **71**，公开事件 **32**，公开方法 **19**；受保护扩展成员 **63**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-8E542AE8B0521E0E | Constructor | `public ToolStripStatusLabel()` | `System.Windows.Forms.ToolStripStatusLabel`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-89ECB662E1D85095 | Constructor | `public ToolStripStatusLabel(System.Drawing.Image image)` | `System.Windows.Forms.ToolStripStatusLabel`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B8A291CF4B08245 | Constructor | `public ToolStripStatusLabel(System.String text)` | `System.Windows.Forms.ToolStripStatusLabel`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E90710820E8BF29 | Constructor | `public ToolStripStatusLabel(System.String text, System.Drawing.Image image)` | `System.Windows.Forms.ToolStripStatusLabel`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A4C50A375140C436 | Constructor | `public ToolStripStatusLabel(System.String text, System.Drawing.Image image, System.EventHandler onClick)` | `System.Windows.Forms.ToolStripStatusLabel`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-809C83BF0C8B7338 | Constructor | `public ToolStripStatusLabel(System.String text, System.Drawing.Image image, System.EventHandler onClick, System.String name)` | `System.Windows.Forms.ToolStripStatusLabel`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C45822FBC38BFB3E | Event | `public event System.EventHandler AvailableChanged` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BF263E670AF94311 | Event | `public event System.EventHandler BackColorChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-65242BFF795CA5AE | Event | `public event System.EventHandler BindingContextChanged` | `System.Windows.Forms.BindableComponent`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E91FD9A54D522D81 | Event | `public event System.EventHandler Click` | `System.Windows.Forms.ToolStripItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0BA7834EE7694513 | Event | `public event System.EventHandler CommandCanExecuteChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C2143833C6ED27B6 | Event | `public event System.EventHandler CommandChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-71FD4AD1731505CE | Event | `public event System.EventHandler CommandParameterChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EADCC5E0B7C77097 | Event | `public event System.EventHandler DisplayStyleChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5C3315EF223052FC | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A8561F95FB8A11AE | Event | `public event System.EventHandler DoubleClick` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-867579A7F7980B3B | Event | `public event System.Windows.Forms.DragEventHandler DragDrop` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B9B3846DB2491D55 | Event | `public event System.Windows.Forms.DragEventHandler DragEnter` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9245AD53108FAACF | Event | `public event System.EventHandler DragLeave` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AA2925DCE5D53A79 | Event | `public event System.Windows.Forms.DragEventHandler DragOver` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-10349D61B785A2EC | Event | `public event System.EventHandler EnabledChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D1DED6F2B092FF9E | Event | `public event System.EventHandler ForeColorChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FAAB458566FA6B22 | Event | `public event System.Windows.Forms.GiveFeedbackEventHandler GiveFeedback` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-121FFDA866851FEF | Event | `public event System.EventHandler LocationChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B387274959E8E33E | Event | `public event System.Windows.Forms.MouseEventHandler MouseDown` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CC897B318AE46136 | Event | `public event System.EventHandler MouseEnter` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6A1E515F2538BC63 | Event | `public event System.EventHandler MouseHover` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FA3312CB429D7424 | Event | `public event System.EventHandler MouseLeave` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7CA3C7AA5C5C109E | Event | `public event System.Windows.Forms.MouseEventHandler MouseMove` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2CDBD1AFD1B01838 | Event | `public event System.Windows.Forms.MouseEventHandler MouseUp` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-83E529FDF6B6DB98 | Event | `public event System.EventHandler OwnerChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D9DD44D75CC23EB | Event | `public event System.Windows.Forms.PaintEventHandler Paint` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-601035893D1A82EC | Event | `public event System.Windows.Forms.QueryAccessibilityHelpEventHandler QueryAccessibilityHelp` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E977E8B95CE02DF9 | Event | `public event System.Windows.Forms.QueryContinueDragEventHandler QueryContinueDrag` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C1CFC29E25F52023 | Event | `public event System.EventHandler RightToLeftChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7C1B41A60D827E3F | Event | `public event System.EventHandler SelectedChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-52D237C4F2756A1A | Event | `public event System.EventHandler TextChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F7DE56B3C410118C | Event | `public event System.EventHandler VisibleChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3A8476FDDDC3457F | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-74E3540C58E4BBBD | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C7DFBF4C0C9A9A54 | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1C735B96DA15FC2A | Method | `public System.Windows.Forms.ToolStrip GetCurrentParent()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-06E88BBE4D555FA6 | Method | `public virtual System.Drawing.Size GetPreferredSize(System.Drawing.Size constrainingSize)` | `System.Windows.Forms.ToolStripStatusLabel`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E7DD350ECEDEB4FC | Method | `public System.Void Invalidate()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CBF83D874CEAE5E2 | Method | `public System.Void Invalidate(System.Drawing.Rectangle r)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1773AA6395DB5D1A | Method | `public System.Void PerformClick()` | `System.Windows.Forms.ToolStripItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-90015479B5DBB08F | Method | `public virtual System.Void ResetBackColor()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-923185812BDC8814 | Method | `public virtual System.Void ResetDisplayStyle()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-54E2F3FEB4F706DB | Method | `public virtual System.Void ResetFont()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6BD0B48747819F02 | Method | `public virtual System.Void ResetForeColor()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B6AFDB11D4B49037 | Method | `public virtual System.Void ResetImage()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B5A150B3E8FEE7F1 | Method | `public System.Void ResetMargin()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-33FDE450D2D133EB | Method | `public System.Void ResetPadding()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-31989ACDD34FEB8E | Method | `public virtual System.Void ResetRightToLeft()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5E5DA4FB68CC0820 | Method | `public virtual System.Void ResetTextDirection()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F277CCE435A749E6 | Method | `public System.Void Select()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2489AD67AC444A10 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.ToolStripItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-312C0A6B2EC4D441 | Property | `System.Windows.Forms.AccessibleObject AccessibilityObject { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A941099E66B9681 | Property | `System.String AccessibleDefaultActionDescription { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-08A1AE903DF02D2F | Property | `System.String AccessibleDescription { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-36CA75106EC6E723 | Property | `System.String AccessibleName { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B7F5D5C67D63FBF7 | Property | `System.Windows.Forms.AccessibleRole AccessibleRole { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(-1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FF154C20D37DD915 | Property | `System.Drawing.Color ActiveLinkColor { public get; public set; }` | `System.Windows.Forms.ToolStripLabel`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-332F5744E587C29E | Property | `System.Windows.Forms.ToolStripItemAlignment Alignment { public get; public set; }` | `System.Windows.Forms.ToolStripStatusLabel` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E586807D0CE113D6 | Property | `virtual System.Boolean AllowDrop { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(false); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-94341F26AE2CBFEB | Property | `System.Windows.Forms.AnchorStyles Anchor { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0); DefaultValueAttribute(5) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E754176220A0863 | Property | `System.Boolean AutoSize { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(true); LocalizableAttribute(true); DesignerSerializationVisibilityAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E8FF02E4D791B1ED | Property | `System.Boolean AutoToolTip { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-85BFBEA3BC4C4063 | Property | `System.Boolean Available { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CC7221FAE0180ED1 | Property | `virtual System.Drawing.Color BackColor { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-415247D37CA4D9BC | Property | `virtual System.Drawing.Image BackgroundImage { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true); DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8A5C36A773CE2FF4 | Property | `virtual System.Windows.Forms.ImageLayout BackgroundImageLayout { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(1); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-94143EF52ADDC59E | Property | `System.Windows.Forms.BindingContext BindingContext { public get; public set; }` | `System.Windows.Forms.BindableComponent` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4955EF5D4724FCD2 | Property | `System.Windows.Forms.ToolStripStatusLabelBorderSides BorderSides { public get; public set; }` | `System.Windows.Forms.ToolStripStatusLabel` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1F0E70932F51DF04 | Property | `System.Windows.Forms.Border3DStyle BorderStyle { public get; public set; }` | `System.Windows.Forms.ToolStripStatusLabel` DefaultValueAttribute(16394) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8123F9929EF0C9B4 | Property | `virtual System.Drawing.Rectangle Bounds { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F7F863FE18E5F7FF | Property | `virtual System.Boolean CanSelect { public get; }` | `System.Windows.Forms.ToolStripLabel`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-473AFABE1D8CC692 | Property | `System.Windows.Input.ICommand Command { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BindableAttribute(true); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C8DB255C442CDAD | Property | `System.Object CommandParameter { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BindableAttribute(true); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FA247F1FFA10B98B | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F880848FCC72D000 | Property | `System.Drawing.Rectangle ContentRectangle { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-07B3A1DA739471BC | Property | `System.Windows.Forms.ControlBindingsCollection DataBindings { public get; }` | `System.Windows.Forms.BindableComponent` DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-240E48770ECF816E | Property | `virtual System.Windows.Forms.ToolStripItemDisplayStyle DisplayStyle { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D600E6D09B7F8FD4 | Property | `System.Windows.Forms.DockStyle Dock { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AE1F47B1D9A1DE87 | Property | `System.Boolean DoubleClickEnabled { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B86FA7708E9C2ABD | Property | `virtual System.Boolean Enabled { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true); DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-94979288E7B87200 | Property | `virtual System.Drawing.Font Font { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B7A469FB24C9727F | Property | `virtual System.Drawing.Color ForeColor { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-695565789F03AB36 | Property | `System.Int32 Height { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3BC5F435AC951CA8 | Property | `virtual System.Drawing.Image Image { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2CE5E2D4A2E3725F | Property | `System.Drawing.ContentAlignment ImageAlign { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(32); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0EB958CD18627DE4 | Property | `System.Int32 ImageIndex { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true); TypeConverterAttribute(System.Windows.Forms.NoneExcludedImageIndexConverter); EditorAttribute("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-851EEECB862AC522 | Property | `System.String ImageKey { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true); TypeConverterAttribute(System.Windows.Forms.ImageKeyConverter); EditorAttribute("System.Windows.Forms.Design.ToolStripImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F8B102B6833F8392 | Property | `System.Windows.Forms.ToolStripItemImageScaling ImageScaling { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(1); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BA3C16A1460F1591 | Property | `System.Drawing.Color ImageTransparentColor { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-92B563A860FC8E12 | Property | `System.Boolean IsDisposed { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9D5F6FBFFDB8A4B0 | Property | `System.Boolean IsLink { public get; public set; }` | `System.Windows.Forms.ToolStripLabel` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-11E556676209AC5A | Property | `System.Boolean IsOnDropDown { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-28F6286174E04155 | Property | `System.Boolean IsOnOverflow { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5D2FC4A01B8CEEA4 | Property | `System.Windows.Forms.LinkBehavior LinkBehavior { public get; public set; }` | `System.Windows.Forms.ToolStripLabel` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-17197ED70C089AE3 | Property | `System.Drawing.Color LinkColor { public get; public set; }` | `System.Windows.Forms.ToolStripLabel`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E928C713CD7F490E | Property | `System.Boolean LinkVisited { public get; public set; }` | `System.Windows.Forms.ToolStripLabel` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-967CD92BC48648E3 | Property | `System.Windows.Forms.Automation.AutomationLiveSetting LiveSetting { public get; public set; }` | `System.Windows.Forms.ToolStripStatusLabel` DefaultValueAttribute(0); BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5CC275E92C7B78C0 | Property | `System.Windows.Forms.Padding Margin { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A39C5A8C916FC9FF | Property | `System.Windows.Forms.MergeAction MergeAction { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7DF7830B0D181F48 | Property | `System.Int32 MergeIndex { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(-1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-94DF5E468323F96D | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DefaultValueAttribute(null) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9EB22CBF06623E57 | Property | `System.Windows.Forms.ToolStripItemOverflow Overflow { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C12EC32D47B7C03A | Property | `System.Windows.Forms.ToolStrip Owner { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B693F3B26C95879 | Property | `System.Windows.Forms.ToolStripItem OwnerItem { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A4F0CAF9E1862BBA | Property | `virtual System.Windows.Forms.Padding Padding { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A1AF306972A2DED5 | Property | `System.Windows.Forms.ToolStripItemPlacement Placement { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7BA621F25D142252 | Property | `virtual System.Boolean Pressed { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-518A38450AFDA790 | Property | `virtual System.Windows.Forms.RightToLeft RightToLeft { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D28CB4D9213F2390 | Property | `System.Boolean RightToLeftAutoMirrorImage { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(false); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-53E3E0C3A629D934 | Property | `virtual System.Boolean Selected { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-338416E28BA81CD8 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E937D4EE15B24EC1 | Property | `virtual System.Drawing.Size Size { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5156AEDFF88B9B8F | Property | `System.Boolean Spring { public get; public set; }` | `System.Windows.Forms.ToolStripStatusLabel` DefaultValueAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F14D5BB3D201AE87 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(null); LocalizableAttribute(false); BindableAttribute(true); TypeConverterAttribute(System.ComponentModel.StringConverter) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D82620ECC748A579 | Property | `virtual System.String Text { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(""); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DED7312133E7AE66 | Property | `virtual System.Drawing.ContentAlignment TextAlign { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(32); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D6A577B19610FC06 | Property | `virtual System.Windows.Forms.ToolStripTextDirection TextDirection { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1098A19F7E8B997B | Property | `System.Windows.Forms.TextImageRelation TextImageRelation { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(4); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-279376A892C65F09 | Property | `System.String ToolTipText { public get; public set; }` | `System.Windows.Forms.ToolStripItem` EditorAttribute("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-932A7F72E4AC0C34 | Property | `System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E4E26C9C93A5751A | Property | `System.Drawing.Color VisitedLinkColor { public get; public set; }` | `System.Windows.Forms.ToolStripLabel`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D864A7678411DE12 | Property | `System.Int32 Width { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-CC9481BCB2C95FFC | Method | `protected virtual System.Windows.Forms.AccessibleObject CreateAccessibilityInstance()` | `System.Windows.Forms.ToolStripStatusLabel`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8FD4EDCB7EF9E17F | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.ToolStripItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9F8D96CB4CA67702 | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D549703E4CC64130 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-33D1967B872AF933 | Method | `protected internal virtual System.Boolean IsInputChar(System.Char charCode)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C11747B73DA02469 | Method | `protected internal virtual System.Boolean IsInputKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9BB25F6B4B44A8D4 | Method | `protected virtual System.Void OnAvailableChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-899C3658F0DF555F | Method | `protected virtual System.Void OnBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C04AB8B266BE7B3F | Method | `protected virtual System.Void OnBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.BindableComponent`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B5A34FFC705DD9DE | Method | `protected virtual System.Void OnBoundsChanged()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2A82E8AD8A64453B | Method | `protected virtual System.Void OnClick(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F8AAE1EC70C5BB84 | Method | `protected virtual System.Void OnCommandCanExecuteChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F8B9D121E282C023 | Method | `protected virtual System.Void OnCommandChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F3430B18942DF8A7 | Method | `protected virtual System.Void OnCommandParameterChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F15C85853FB6E2F4 | Method | `protected virtual System.Void OnDisplayStyleChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-389C49732D10205F | Method | `protected virtual System.Void OnDoubleClick(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B070B650D26E1495 | Method | `protected virtual System.Void OnDragDrop(System.Windows.Forms.DragEventArgs dragEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50F31AD017A8AFA5 | Method | `protected virtual System.Void OnDragEnter(System.Windows.Forms.DragEventArgs dragEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D9317F08EABF54A2 | Method | `protected virtual System.Void OnDragLeave(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2F67520F795C7065 | Method | `protected virtual System.Void OnDragOver(System.Windows.Forms.DragEventArgs dragEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4035289C166912AE | Method | `protected virtual System.Void OnEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-18AFB8F02803FC74 | Method | `protected virtual System.Void OnFontChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripLabel`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C4037F7E2DA5F5B8 | Method | `protected virtual System.Void OnForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A7E1FCE815915C58 | Method | `protected virtual System.Void OnGiveFeedback(System.Windows.Forms.GiveFeedbackEventArgs giveFeedbackEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-394A730C7FEE6942 | Method | `protected internal virtual System.Void OnLayout(System.Windows.Forms.LayoutEventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-47010CE1C11E14A4 | Method | `protected virtual System.Void OnLocationChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-133F1AF422E28D9F | Method | `protected virtual System.Void OnMouseDown(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3BC3BCED40D5CDDB | Method | `protected virtual System.Void OnMouseEnter(System.EventArgs e)` | `System.Windows.Forms.ToolStripLabel`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5803ECC2264E7A66 | Method | `protected virtual System.Void OnMouseHover(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-48489712F1D9244E | Method | `protected virtual System.Void OnMouseLeave(System.EventArgs e)` | `System.Windows.Forms.ToolStripLabel`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E1773D8732C5C79D | Method | `protected virtual System.Void OnMouseMove(System.Windows.Forms.MouseEventArgs mea)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A5E0C37FD2365AA6 | Method | `protected virtual System.Void OnMouseUp(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1D38292CED41F169 | Method | `protected virtual System.Void OnOwnerChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-163684539047E753 | Method | `protected internal virtual System.Void OnOwnerFontChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-92B3BDC150063EA5 | Method | `protected virtual System.Void OnPaint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.ToolStripStatusLabel`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3C9D5ACECB8AFFD6 | Method | `protected virtual System.Void OnParentBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-26835B755152C181 | Method | `protected virtual System.Void OnParentChanged(System.Windows.Forms.ToolStrip oldParent, System.Windows.Forms.ToolStrip newParent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-10598D209C9BF445 | Method | `protected internal virtual System.Void OnParentEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DDE1C4AF33CBE128 | Method | `protected virtual System.Void OnParentForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A9C967789FC3C8B0 | Method | `protected internal virtual System.Void OnParentRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4AF5DE7F44904E2D | Method | `protected virtual System.Void OnQueryContinueDrag(System.Windows.Forms.QueryContinueDragEventArgs queryContinueDragEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8C588FD99911ABD1 | Method | `protected virtual System.Void OnRequestCommandExecute(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-25F472F22209D990 | Method | `protected virtual System.Void OnRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CCB9179B9741F8E6 | Method | `protected virtual System.Void OnSelectedChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC8366153ACD1A8D | Method | `protected virtual System.Void OnTextChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripStatusLabel`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D70706A39BF051F3 | Method | `protected virtual System.Void OnVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E195115A598FBC4 | Method | `protected internal virtual System.Boolean ProcessCmdKey(ref System.Windows.Forms.Message m, System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-14AE6CC9A992188B | Method | `protected internal virtual System.Boolean ProcessDialogKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ED3841DEB0D710F9 | Method | `protected internal virtual System.Boolean ProcessMnemonic(System.Char charCode)` | `System.Windows.Forms.ToolStripLabel`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-45C0CE77CF6FCDAD | Method | `protected internal virtual System.Void SetBounds(System.Drawing.Rectangle bounds)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9826BFB01B777A4F | Method | `protected virtual System.Void SetVisibleCore(System.Boolean visible)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A97B192C40E3AF7 | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-DB277C696C5C8DD5 | Property | `virtual System.Boolean DefaultAutoToolTip { protected get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-80504797BF47BA50 | Property | `virtual System.Windows.Forms.ToolStripItemDisplayStyle DefaultDisplayStyle { protected get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E6F0C8EDC452B68E | Property | `virtual System.Windows.Forms.Padding DefaultMargin { protected internal get; }` | `System.Windows.Forms.ToolStripStatusLabel`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-001552BF75EDC939 | Property | `virtual System.Windows.Forms.Padding DefaultPadding { protected get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C4D27FC48E7DE0EC | Property | `virtual System.Drawing.Size DefaultSize { protected get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FE2DDFEC902ACE1E | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3CBEBB3BBA9A791D | Property | `virtual System.Boolean DismissWhenClicked { protected internal get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6DEA7D218B204928 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D331C92AE583E9B0 | Property | `System.Windows.Forms.ToolStrip Parent { protected internal get; protected internal set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4A8FDAA37A0DFA7F | Property | `virtual System.Windows.Forms.ToolStripRenderer Renderer { protected internal get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A3877F6613CAE4DF | Property | `virtual System.Boolean ShowKeyboardCues { protected internal get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
