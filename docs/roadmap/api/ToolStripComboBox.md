# ToolStripComboBox：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/ToolStripComboBox.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.ToolStripComboBox`；基类：`System.Windows.Forms.ToolStripControlHost`。Lumina 同名类型：🟡 存在；基类及基线接口形状：⬜ 未匹配。

公开属性 **84**，公开事件 **46**，公开方法 **29**；受保护扩展成员 **81**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-FF87759A6A51CB19 | Constructor | `public ToolStripComboBox()` | `System.Windows.Forms.ToolStripComboBox`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-BB594D064CC472B4 | Constructor | `public ToolStripComboBox(System.String name)` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6A89C82D0BA6472B | Constructor | `public ToolStripComboBox(System.Windows.Forms.Control c)` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-97741DC19A93A758 | Event | `public event System.EventHandler AvailableChanged` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C0F203EF206306EE | Event | `public event System.EventHandler BackColorChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B340A94EA119A6F | Event | `public event System.EventHandler BindingContextChanged` | `System.Windows.Forms.BindableComponent`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4858E2867F069AA4 | Event | `public event System.EventHandler Click` | `System.Windows.Forms.ToolStripItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-30017FDEF9610BC3 | Event | `public event System.EventHandler CommandCanExecuteChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DE6EB5BE02AF643C | Event | `public event System.EventHandler CommandChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-49BC3A74254DF3CE | Event | `public event System.EventHandler CommandParameterChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-044EB6868D9100F3 | Event | `public event System.EventHandler DisplayStyleChanged` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-323835A9FE9B39BC | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B8D6E6C0A4D6D0FB | Event | `public event System.EventHandler DoubleClick` | `System.Windows.Forms.ToolStripComboBox` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F1BE1FFE6040130F | Event | `public event System.Windows.Forms.DragEventHandler DragDrop` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-33C3549C912689EF | Event | `public event System.Windows.Forms.DragEventHandler DragEnter` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BAFC87FC57B42227 | Event | `public event System.EventHandler DragLeave` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-78278B59C2041336 | Event | `public event System.Windows.Forms.DragEventHandler DragOver` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-56A40DC2970B7AEB | Event | `public event System.EventHandler DropDown` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-516EFAFD363CF952 | Event | `public event System.EventHandler DropDownClosed` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C62FC327A3B2B6D0 | Event | `public event System.EventHandler DropDownStyleChanged` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-324EC6FAD7DE41FF | Event | `public event System.EventHandler EnabledChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5F98E03C4756A0C6 | Event | `public event System.EventHandler Enter` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-03EE906AFDB0F809 | Event | `public event System.EventHandler ForeColorChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C7A61E0E6388AF76 | Event | `public event System.Windows.Forms.GiveFeedbackEventHandler GiveFeedback` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C8B99A8859EA5F84 | Event | `public event System.EventHandler GotFocus` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-587C601E765FFA50 | Event | `public event System.Windows.Forms.KeyEventHandler KeyDown` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F784BBEE1E937E48 | Event | `public event System.Windows.Forms.KeyPressEventHandler KeyPress` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D6950E9F67242DFE | Event | `public event System.Windows.Forms.KeyEventHandler KeyUp` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-02E44EB68A5058C1 | Event | `public event System.EventHandler Leave` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C876EBE8EF84E30C | Event | `public event System.EventHandler LocationChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DB5B27E4E41D6428 | Event | `public event System.EventHandler LostFocus` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-944A046A6EBE32F3 | Event | `public event System.Windows.Forms.MouseEventHandler MouseDown` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8F03A28A3D352016 | Event | `public event System.EventHandler MouseEnter` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6E6EA32CC47EE7A2 | Event | `public event System.EventHandler MouseHover` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E1B3A2FA352B985 | Event | `public event System.EventHandler MouseLeave` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-271D5BB11CAFC7CE | Event | `public event System.Windows.Forms.MouseEventHandler MouseMove` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5EB906BDC7550DDC | Event | `public event System.Windows.Forms.MouseEventHandler MouseUp` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-342710F006FDB801 | Event | `public event System.EventHandler OwnerChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EE1AD9F143F46538 | Event | `public event System.Windows.Forms.PaintEventHandler Paint` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6BBDB1D1FB97D7B2 | Event | `public event System.Windows.Forms.QueryAccessibilityHelpEventHandler QueryAccessibilityHelp` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-41765D5574DFAB6A | Event | `public event System.Windows.Forms.QueryContinueDragEventHandler QueryContinueDrag` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BED901F7161E0029 | Event | `public event System.EventHandler RightToLeftChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-61281DFB13B69646 | Event | `public event System.EventHandler SelectedChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FB1E164B38C1CC70 | Event | `public event System.EventHandler SelectedIndexChanged` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-36548D0989829C88 | Event | `public event System.EventHandler TextChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FA44C3A0D2029C25 | Event | `public event System.EventHandler TextUpdate` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C7CE9D4889E29C2 | Event | `public event System.EventHandler Validated` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C7A93813B473E6E1 | Event | `public event System.ComponentModel.CancelEventHandler Validating` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5827F3E8270A2CE9 | Event | `public event System.EventHandler VisibleChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C939EBD3E0B866AC | Method | `public System.Void BeginUpdate()` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A7D7D929224D646F | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-375072DDAEAA4AA0 | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CC392C9D9B6B8B97 | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BD944117D6ABFE3C | Method | `public System.Void EndUpdate()` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6216E5C86F8E7B86 | Method | `public System.Int32 FindString(System.String s)` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C433D682127E4FE6 | Method | `public System.Int32 FindString(System.String s, System.Int32 startIndex)` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BA89662C3E3A5BB0 | Method | `public System.Int32 FindStringExact(System.String s)` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7202ED424A46EF75 | Method | `public System.Int32 FindStringExact(System.String s, System.Int32 startIndex)` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4C0C863FF9560AE4 | Method | `public System.Void Focus()` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-08E6BACC2ED474A1 | Method | `public System.Windows.Forms.ToolStrip GetCurrentParent()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FC66CD620E058AD0 | Method | `public System.Int32 GetItemHeight(System.Int32 index)` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A1828D0422AD8650 | Method | `public virtual System.Drawing.Size GetPreferredSize(System.Drawing.Size constrainingSize)` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1391955A6AD2039E | Method | `public System.Void Invalidate()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29E4DC6CBAE71725 | Method | `public System.Void Invalidate(System.Drawing.Rectangle r)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-94FACD42EB4DCF07 | Method | `public System.Void PerformClick()` | `System.Windows.Forms.ToolStripItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-51136B8441F89D02 | Method | `public virtual System.Void ResetBackColor()` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CBB00B472AFE6A74 | Method | `public virtual System.Void ResetDisplayStyle()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-52DCB24BC7CE9DF6 | Method | `public virtual System.Void ResetFont()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-23FF385603BDFB26 | Method | `public virtual System.Void ResetForeColor()` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EFC54FA43CCB877C | Method | `public virtual System.Void ResetImage()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5313BEA5F6048E14 | Method | `public System.Void ResetMargin()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CF9EC393C6C18CA8 | Method | `public System.Void ResetPadding()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4475515F811CCD08 | Method | `public virtual System.Void ResetRightToLeft()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D6F39B276189F6D0 | Method | `public virtual System.Void ResetTextDirection()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-60FD5D69E3024201 | Method | `public System.Void Select()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-57AB4FEECF0AB742 | Method | `public System.Void Select(System.Int32 start, System.Int32 length)` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EE1B55225EE818D5 | Method | `public System.Void SelectAll()` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-631D152735F2B786 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.ToolStripComboBox`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-857E73EF44F8C3CC | Property | `System.Windows.Forms.AccessibleObject AccessibilityObject { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C60561BF3040D75D | Property | `System.String AccessibleDefaultActionDescription { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D96A760C290D14E5 | Property | `System.String AccessibleDescription { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D41DB2EF8688E0B | Property | `System.String AccessibleName { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5D48922F96671739 | Property | `System.Windows.Forms.AccessibleRole AccessibleRole { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(-1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CDFAD5D49E379318 | Property | `System.Windows.Forms.ToolStripItemAlignment Alignment { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3D95BC8EB969E641 | Property | `virtual System.Boolean AllowDrop { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(false); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5E00F68A1E7BC50D | Property | `System.Windows.Forms.AnchorStyles Anchor { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0); DefaultValueAttribute(5) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9CF9D550EF4D7169 | Property | `System.Windows.Forms.AutoCompleteStringCollection AutoCompleteCustomSource { public get; public set; }` | `System.Windows.Forms.ToolStripComboBox` DesignerSerializationVisibilityAttribute(2); LocalizableAttribute(true); EditorAttribute("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5FC9165F6377BDE4 | Property | `System.Windows.Forms.AutoCompleteMode AutoCompleteMode { public get; public set; }` | `System.Windows.Forms.ToolStripComboBox` DefaultValueAttribute(0); BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2BCD713FCB5E37B8 | Property | `System.Windows.Forms.AutoCompleteSource AutoCompleteSource { public get; public set; }` | `System.Windows.Forms.ToolStripComboBox` DefaultValueAttribute(128); BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16EDDD6FF84ACEBA | Property | `System.Boolean AutoSize { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(true); LocalizableAttribute(true); DesignerSerializationVisibilityAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FA2AD21E0E43E952 | Property | `System.Boolean AutoToolTip { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EA4AEFCB05ED5554 | Property | `System.Boolean Available { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7F39724D4C565DE7 | Property | `virtual System.Drawing.Color BackColor { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8B0703DFAB23D1CE | Property | `virtual System.Drawing.Image BackgroundImage { public get; public set; }` | `System.Windows.Forms.ToolStripComboBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC165CAAF09594B2 | Property | `virtual System.Windows.Forms.ImageLayout BackgroundImageLayout { public get; public set; }` | `System.Windows.Forms.ToolStripComboBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A7E6D8BC9260833 | Property | `System.Windows.Forms.BindingContext BindingContext { public get; public set; }` | `System.Windows.Forms.BindableComponent` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-267CE17584F9F11B | Property | `virtual System.Drawing.Rectangle Bounds { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A93CDE56DB00192B | Property | `virtual System.Boolean CanSelect { public get; }` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DDDC11101C84F362 | Property | `System.Boolean CausesValidation { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B43CD01398BD99D7 | Property | `System.Windows.Forms.ComboBox ComboBox { public get; }` | `System.Windows.Forms.ToolStripComboBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8E61736B0C414748 | Property | `System.Windows.Input.ICommand Command { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BindableAttribute(true); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A6B8011E75FB3460 | Property | `System.Object CommandParameter { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BindableAttribute(true); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A718A426BA608C98 | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0D0794070B7271B8 | Property | `System.Drawing.Rectangle ContentRectangle { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0DE785401517C438 | Property | `System.Windows.Forms.Control Control { public get; }` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FE66CA3F65BF7E01 | Property | `System.Drawing.ContentAlignment ControlAlign { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost` DefaultValueAttribute(32); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC3388C3A19CB9FC | Property | `System.Windows.Forms.ControlBindingsCollection DataBindings { public get; }` | `System.Windows.Forms.BindableComponent` DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D95B8F052D98660B | Property | `System.Windows.Forms.ToolStripItemDisplayStyle DisplayStyle { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CD6AB4C49041786D | Property | `System.Windows.Forms.DockStyle Dock { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-62DA7D84C3793A81 | Property | `System.Boolean DoubleClickEnabled { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost` DefaultValueAttribute(false); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-97262543F17DC7E1 | Property | `System.Int32 DropDownHeight { public get; public set; }` | `System.Windows.Forms.ToolStripComboBox` BrowsableAttribute(true); DefaultValueAttribute(106) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BF9796CE816DE784 | Property | `System.Windows.Forms.ComboBoxStyle DropDownStyle { public get; public set; }` | `System.Windows.Forms.ToolStripComboBox` DefaultValueAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-11208483A774C5F4 | Property | `System.Int32 DropDownWidth { public get; public set; }` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7492199EB09B2935 | Property | `System.Boolean DroppedDown { public get; public set; }` | `System.Windows.Forms.ToolStripComboBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7F319CBB947C56A5 | Property | `virtual System.Boolean Enabled { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-70B3E306C8AFDED8 | Property | `System.Windows.Forms.FlatStyle FlatStyle { public get; public set; }` | `System.Windows.Forms.ToolStripComboBox` DefaultValueAttribute(1); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7DF03F8BBF9A35CE | Property | `virtual System.Boolean Focused { public get; }` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0505B43E1443EE6F | Property | `virtual System.Drawing.Font Font { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2603CDA8C9E4B586 | Property | `virtual System.Drawing.Color ForeColor { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9640902412833CF8 | Property | `System.Int32 Height { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-88BD80BDAE203827 | Property | `virtual System.Drawing.Image Image { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-600D2BF13ED99BDA | Property | `System.Drawing.ContentAlignment ImageAlign { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1139BBFB05F218F9 | Property | `System.Int32 ImageIndex { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true); TypeConverterAttribute(System.Windows.Forms.NoneExcludedImageIndexConverter); EditorAttribute("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-07C404DDB43A712B | Property | `System.String ImageKey { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true); TypeConverterAttribute(System.Windows.Forms.ImageKeyConverter); EditorAttribute("System.Windows.Forms.Design.ToolStripImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6B49DFE7EB47E668 | Property | `System.Windows.Forms.ToolStripItemImageScaling ImageScaling { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A0D058CA7E7072C5 | Property | `System.Drawing.Color ImageTransparentColor { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-EBE6F9758DC6F1E0 | Property | `System.Boolean IntegralHeight { public get; public set; }` | `System.Windows.Forms.ToolStripComboBox` DefaultValueAttribute(true); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-84FE9EA9F660FDB7 | Property | `System.Boolean IsDisposed { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-895BCE1922FEDAFE | Property | `System.Boolean IsOnDropDown { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-75E16CC4D24A1AB2 | Property | `System.Boolean IsOnOverflow { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-109C9A673A6DF1C1 | Property | `System.Windows.Forms.ComboBox+ObjectCollection Items { public get; }` | `System.Windows.Forms.ToolStripComboBox` DesignerSerializationVisibilityAttribute(2); LocalizableAttribute(true); EditorAttribute("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6FEC07288EBFEB2A | Property | `System.Windows.Forms.Padding Margin { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-25CA7248EB91B879 | Property | `System.Int32 MaxDropDownItems { public get; public set; }` | `System.Windows.Forms.ToolStripComboBox` DefaultValueAttribute(8); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7A9046DA6388E01A | Property | `System.Int32 MaxLength { public get; public set; }` | `System.Windows.Forms.ToolStripComboBox` DefaultValueAttribute(0); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4935EBB156431B51 | Property | `System.Windows.Forms.MergeAction MergeAction { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8369476E352A2058 | Property | `System.Int32 MergeIndex { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(-1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-994DC5F10D9BA174 | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DefaultValueAttribute(null) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-74409ACB882F009B | Property | `System.Windows.Forms.ToolStripItemOverflow Overflow { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B00CE2D0B5372E73 | Property | `System.Windows.Forms.ToolStrip Owner { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-20B215E7E05410E2 | Property | `System.Windows.Forms.ToolStripItem OwnerItem { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7EEF80152D7CFFC4 | Property | `virtual System.Windows.Forms.Padding Padding { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-20052E8DC41D3431 | Property | `System.Windows.Forms.ToolStripItemPlacement Placement { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DBDDE95663E8FEB3 | Property | `virtual System.Boolean Pressed { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BD64566073D2CD0F | Property | `virtual System.Windows.Forms.RightToLeft RightToLeft { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50A6DE3951CABD88 | Property | `System.Boolean RightToLeftAutoMirrorImage { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3B70C81DA1489781 | Property | `virtual System.Boolean Selected { public get; }` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3CC98526BD99CCA6 | Property | `System.Int32 SelectedIndex { public get; public set; }` | `System.Windows.Forms.ToolStripComboBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7B31B763D6C76CBB | Property | `System.Object SelectedItem { public get; public set; }` | `System.Windows.Forms.ToolStripComboBox` BrowsableAttribute(false); BindableAttribute(true); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-73F8AE3CCC88DF02 | Property | `System.String SelectedText { public get; public set; }` | `System.Windows.Forms.ToolStripComboBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6C7C0A4F61BC5345 | Property | `System.Int32 SelectionLength { public get; public set; }` | `System.Windows.Forms.ToolStripComboBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0079A1EC01FBA1B6 | Property | `System.Int32 SelectionStart { public get; public set; }` | `System.Windows.Forms.ToolStripComboBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7E8F6FF2CB5BBE80 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-338E72CDE31557AD | Property | `virtual System.Drawing.Size Size { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-294389C8F557B47B | Property | `System.Boolean Sorted { public get; public set; }` | `System.Windows.Forms.ToolStripComboBox` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-655F57FD08F294C5 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(null); LocalizableAttribute(false); BindableAttribute(true); TypeConverterAttribute(System.ComponentModel.StringConverter) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D9E47B6EE8D225C7 | Property | `virtual System.String Text { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost` DefaultValueAttribute("") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-44E68E3BACAFFF4E | Property | `System.Drawing.ContentAlignment TextAlign { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BB6100C456EFBF2F | Property | `virtual System.Windows.Forms.ToolStripTextDirection TextDirection { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false); DefaultValueAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B7BB0A2AE7C151D6 | Property | `System.Windows.Forms.TextImageRelation TextImageRelation { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A9CE9C3B7E69FF1F | Property | `System.String ToolTipText { public get; public set; }` | `System.Windows.Forms.ToolStripItem` EditorAttribute("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C8211969897202BA | Property | `System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B5C6AB1658A41C42 | Property | `System.Int32 Width { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-4911261154C0772D | Method | `protected virtual System.Windows.Forms.AccessibleObject CreateAccessibilityInstance()` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F8520F081A501098 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.ToolStripControlHost`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-97DEE1B3B3B5D164 | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-450F9EB9896ACA06 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A4DBD22EC5800454 | Method | `protected internal virtual System.Boolean IsInputChar(System.Char charCode)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E5A6C013739EFF9 | Method | `protected internal virtual System.Boolean IsInputKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-68A4CF2CEA6F8101 | Method | `protected virtual System.Void OnAvailableChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B1085927B56AFA1 | Method | `protected virtual System.Void OnBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-864B29BFFAC84B0C | Method | `protected virtual System.Void OnBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.BindableComponent`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0507188110299274 | Method | `protected virtual System.Void OnBoundsChanged()` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-45627B4F5D47445B | Method | `protected virtual System.Void OnClick(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FDAD5836CB1A3F8A | Method | `protected virtual System.Void OnCommandCanExecuteChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-15B4F2347482E60A | Method | `protected virtual System.Void OnCommandChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D76206865CF1567 | Method | `protected virtual System.Void OnCommandParameterChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AA948954176A7352 | Method | `protected virtual System.Void OnDisplayStyleChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16BD4F5AF1EB1D77 | Method | `protected virtual System.Void OnDoubleClick(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7F9F8487432FABFD | Method | `protected virtual System.Void OnDragDrop(System.Windows.Forms.DragEventArgs dragEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-848D2BD868E55660 | Method | `protected virtual System.Void OnDragEnter(System.Windows.Forms.DragEventArgs dragEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DB31DF2132B7D6FB | Method | `protected virtual System.Void OnDragLeave(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3412C82B4630CBE6 | Method | `protected virtual System.Void OnDragOver(System.Windows.Forms.DragEventArgs dragEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-05F481EEE1D764D7 | Method | `protected virtual System.Void OnDropDown(System.EventArgs e)` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BFFDCA8D76D04857 | Method | `protected virtual System.Void OnDropDownClosed(System.EventArgs e)` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F4F51806D744D62C | Method | `protected virtual System.Void OnDropDownStyleChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F4CFB27DD9513821 | Method | `protected virtual System.Void OnEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8FE163C96042E8C0 | Method | `protected virtual System.Void OnEnter(System.EventArgs e)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3DBF16F4B7CDD396 | Method | `protected virtual System.Void OnFontChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-05AE05FBD903F660 | Method | `protected virtual System.Void OnForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B92DA8BBB8711D1F | Method | `protected virtual System.Void OnGiveFeedback(System.Windows.Forms.GiveFeedbackEventArgs giveFeedbackEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D60E719B68037008 | Method | `protected virtual System.Void OnGotFocus(System.EventArgs e)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8A0E740E44F6D66F | Method | `protected virtual System.Void OnHostedControlResize(System.EventArgs e)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B7A2564128DA177 | Method | `protected virtual System.Void OnKeyDown(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-690836E0A29A9468 | Method | `protected virtual System.Void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BDA046F63A9D3AE4 | Method | `protected virtual System.Void OnKeyUp(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1C0D2CB9870105C9 | Method | `protected internal virtual System.Void OnLayout(System.Windows.Forms.LayoutEventArgs e)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-689D958CBE76E953 | Method | `protected virtual System.Void OnLeave(System.EventArgs e)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E3B5B3E6F8A743F | Method | `protected virtual System.Void OnLocationChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F77E5994A17A5EEA | Method | `protected virtual System.Void OnLostFocus(System.EventArgs e)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E7466EB0764430C9 | Method | `protected virtual System.Void OnMouseDown(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1578D5D5E12CFFDA | Method | `protected virtual System.Void OnMouseEnter(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-95F0A18304BED603 | Method | `protected virtual System.Void OnMouseHover(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5EF481B1F84BE9B9 | Method | `protected virtual System.Void OnMouseLeave(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DA0A8EA572B90D12 | Method | `protected virtual System.Void OnMouseMove(System.Windows.Forms.MouseEventArgs mea)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-524D6138B987DEB4 | Method | `protected virtual System.Void OnMouseUp(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-06984374A6E1321E | Method | `protected virtual System.Void OnOwnerChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A286A00F41702840 | Method | `protected internal virtual System.Void OnOwnerFontChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C4707F34582A066 | Method | `protected virtual System.Void OnPaint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-630027D4EBAC4D03 | Method | `protected virtual System.Void OnParentBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D9E0DC4F10CE257F | Method | `protected virtual System.Void OnParentChanged(System.Windows.Forms.ToolStrip oldParent, System.Windows.Forms.ToolStrip newParent)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8B9300800B6C6604 | Method | `protected internal virtual System.Void OnParentEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EF4EAAA0B2139F60 | Method | `protected virtual System.Void OnParentForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-915DBBE77BF40A12 | Method | `protected internal virtual System.Void OnParentRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BFD3692861C33AD0 | Method | `protected virtual System.Void OnQueryContinueDrag(System.Windows.Forms.QueryContinueDragEventArgs queryContinueDragEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B0E558A56DF89C1 | Method | `protected virtual System.Void OnRequestCommandExecute(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2256F6992148B836 | Method | `protected virtual System.Void OnRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-06C7F51AE5BA3417 | Method | `protected virtual System.Void OnSelectedChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D4F7C5DF3EA6852A | Method | `protected virtual System.Void OnSelectedIndexChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8E0167879C9809E3 | Method | `protected virtual System.Void OnSelectionChangeCommitted(System.EventArgs e)` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8198A1E8CFBF0F68 | Method | `protected virtual System.Void OnSubscribeControlEvents(System.Windows.Forms.Control control)` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8D9DA8E4A71EF2C3 | Method | `protected virtual System.Void OnTextChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D4E209FDA44B1573 | Method | `protected virtual System.Void OnTextUpdate(System.EventArgs e)` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-397E8482AE407410 | Method | `protected virtual System.Void OnUnsubscribeControlEvents(System.Windows.Forms.Control control)` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E5A72BA7A9102937 | Method | `protected virtual System.Void OnValidated(System.EventArgs e)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-28533D32930E0F7C | Method | `protected virtual System.Void OnValidating(System.ComponentModel.CancelEventArgs e)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F0F79C4D382421A7 | Method | `protected virtual System.Void OnVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-37CF4C24B348183B | Method | `protected internal virtual System.Boolean ProcessCmdKey(ref System.Windows.Forms.Message m, System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-318804E0241914B2 | Method | `protected internal virtual System.Boolean ProcessDialogKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E4DB18E5E41B4A7 | Method | `protected internal virtual System.Boolean ProcessMnemonic(System.Char charCode)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EDD49CEB4AAAF468 | Method | `protected internal virtual System.Void SetBounds(System.Drawing.Rectangle bounds)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-925F2A7CC8521DCC | Method | `protected virtual System.Void SetVisibleCore(System.Boolean visible)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F419F95C7DFBBF90 | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-24F35A43AF2B92CE | Property | `virtual System.Boolean DefaultAutoToolTip { protected get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-926C64F3EB8DA236 | Property | `virtual System.Windows.Forms.ToolStripItemDisplayStyle DefaultDisplayStyle { protected get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-32C95E9164E685DC | Property | `virtual System.Windows.Forms.Padding DefaultMargin { protected internal get; }` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A0DA0703A4A8E6D | Property | `virtual System.Windows.Forms.Padding DefaultPadding { protected get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-54BC1A036BE57639 | Property | `virtual System.Drawing.Size DefaultSize { protected get; }` | `System.Windows.Forms.ToolStripComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-63B0080F943533EB | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5F9ADBF7CC78011A | Property | `virtual System.Boolean DismissWhenClicked { protected internal get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DC040F814EECA5F1 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1A0CDBEE016A0CC6 | Property | `System.Windows.Forms.ToolStrip Parent { protected internal get; protected internal set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-360394CEC6E787AC | Property | `virtual System.Windows.Forms.ToolStripRenderer Renderer { protected internal get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-96096EF6F7079ABD | Property | `virtual System.Boolean ShowKeyboardCues { protected internal get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
