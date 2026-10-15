# ComponentEditorForm：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/ComponentEditorForm.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.Design.ComponentEditorForm`；基类：`System.Windows.Forms.Form`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **143**，公开事件 **99**，公开方法 **99**；受保护扩展成员 **217**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-FE3EDE9591DC0538 | Constructor | `public ComponentEditorForm(System.Object component, System.Type[] pageTypes)` | `System.Windows.Forms.Design.ComponentEditorForm`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-61778867330D7A7E | Event | `public event System.EventHandler Activated` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6381D6223BD0D04D | Event | `public event System.EventHandler AutoSizeChanged` | `System.Windows.Forms.Design.ComponentEditorForm` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F67484792DACBCE2 | Event | `public event System.EventHandler AutoValidateChanged` | `System.Windows.Forms.Form` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3587417F23820FAA | Event | `public event System.EventHandler BackColorChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E69698874EA31859 | Event | `public event System.EventHandler BackgroundImageChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8790C7455CC16067 | Event | `public event System.EventHandler BackgroundImageLayoutChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6F7ACBDCFCEBF73B | Event | `public event System.EventHandler BindingContextChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-38017BD56861ED68 | Event | `public event System.EventHandler CausesValidationChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C83C9771EBA6B2E | Event | `public event System.Windows.Forms.UICuesEventHandler ChangeUICues` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B92CEFDE4EA5FD89 | Event | `public event System.EventHandler Click` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1551DF2E0AA7E9DF | Event | `public event System.EventHandler ClientSizeChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-68C5068094014511 | Event | `public event System.EventHandler Closed` | `System.Windows.Forms.Form` BrowsableAttribute(false); ObsoleteAttribute("Form.OnClosing, Form.OnClosed and the corresponding events are obsolete. Use Form.OnFormClosing, Form.OnFormClosed, Form.FormClosing and Form.FormClosed instead.",false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EEA5583FBE674922 | Event | `public event System.ComponentModel.CancelEventHandler Closing` | `System.Windows.Forms.Form` BrowsableAttribute(false); ObsoleteAttribute("Form.OnClosing, Form.OnClosed and the corresponding events are obsolete. Use Form.OnFormClosing, Form.OnFormClosed, Form.FormClosing and Form.FormClosed instead.",false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7B75D0CD1EC5DEBE | Event | `public event System.EventHandler ContextMenuChanged` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F5F5218DDC9732C6 | Event | `public event System.EventHandler ContextMenuStripChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E9D9F2BC6A30B682 | Event | `public event System.Windows.Forms.ControlEventHandler ControlAdded` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6C2595ABB0A69873 | Event | `public event System.Windows.Forms.ControlEventHandler ControlRemoved` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4CB98F011CA139F4 | Event | `public event System.EventHandler CursorChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-187782339C697CF2 | Event | `public event System.EventHandler DataContextChanged` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-22F938572C5CD344 | Event | `public event System.EventHandler Deactivate` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EA3849EEE3C30FBD | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-798F67185D69D075 | Event | `public event System.EventHandler DockChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-304EA7672DB1E530 | Event | `public event System.EventHandler DoubleClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-179AD1AC98146AD4 | Event | `public event System.Windows.Forms.DpiChangedEventHandler DpiChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4838A54AC6E43606 | Event | `public event System.EventHandler DpiChangedAfterParent` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E5FAE389100DE6C | Event | `public event System.EventHandler DpiChangedBeforeParent` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E736BA068A2A870 | Event | `public event System.Windows.Forms.DragEventHandler DragDrop` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5DF4F3595B7FF775 | Event | `public event System.Windows.Forms.DragEventHandler DragEnter` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A4A8EA2177F2535E | Event | `public event System.EventHandler DragLeave` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2D998F046B1DDF5D | Event | `public event System.Windows.Forms.DragEventHandler DragOver` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-47B119D98740A3F3 | Event | `public event System.EventHandler EnabledChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E96E69CF82416F0 | Event | `public event System.EventHandler Enter` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C939C0234E5131D | Event | `public event System.EventHandler FontChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DF02C36778C65CCC | Event | `public event System.EventHandler ForeColorChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2D27BB6750301944 | Event | `public event System.EventHandler FormBorderColorChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0B5396CA2774D1E | Event | `public event System.EventHandler FormCaptionBackColorChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EA5B216F28C45F94 | Event | `public event System.EventHandler FormCaptionTextColorChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AEDA7A529AFD28D8 | Event | `public event System.Windows.Forms.FormClosedEventHandler FormClosed` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D75CFC09A44DF14 | Event | `public event System.Windows.Forms.FormClosingEventHandler FormClosing` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-86EE401FBF9CEBE3 | Event | `public event System.EventHandler FormCornerPreferenceChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7CC07E31CC4086F0 | Event | `public event System.Windows.Forms.GiveFeedbackEventHandler GiveFeedback` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-138187BECC7281C6 | Event | `public event System.EventHandler GotFocus` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-395F8C6F7836A61B | Event | `public event System.EventHandler HandleCreated` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1E468751B069F4A1 | Event | `public event System.EventHandler HandleDestroyed` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0BBC6DA39D1B5221 | Event | `public event System.ComponentModel.CancelEventHandler HelpButtonClicked` | `System.Windows.Forms.Form` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-41923337B12C9C35 | Event | `public event System.Windows.Forms.HelpEventHandler HelpRequested` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-18EA3F16B7B3BB92 | Event | `public event System.EventHandler ImeModeChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3D368D7DE46D5416 | Event | `public event System.Windows.Forms.InputLanguageChangedEventHandler InputLanguageChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6BE072ACC66A29EA | Event | `public event System.Windows.Forms.InputLanguageChangingEventHandler InputLanguageChanging` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F280A661E4B769D3 | Event | `public event System.Windows.Forms.InvalidateEventHandler Invalidated` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1A676AECB67DAF86 | Event | `public event System.Windows.Forms.KeyEventHandler KeyDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2DBD7EB13C7F1856 | Event | `public event System.Windows.Forms.KeyPressEventHandler KeyPress` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2D4E6DC1DE2B8298 | Event | `public event System.Windows.Forms.KeyEventHandler KeyUp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B237A750C03D08F8 | Event | `public event System.Windows.Forms.LayoutEventHandler Layout` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-07DA79AF30046E08 | Event | `public event System.EventHandler Leave` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B9409BAB4ED5777 | Event | `public event System.EventHandler Load` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DBB8602A11340A44 | Event | `public event System.EventHandler LocationChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E65A313D34362718 | Event | `public event System.EventHandler LostFocus` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A0D93C198765EB64 | Event | `public event System.EventHandler MarginChanged` | `System.Windows.Forms.Form` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ABD4390C6C1372CB | Event | `public event System.EventHandler MaximizedBoundsChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3AED0A5D85D335E7 | Event | `public event System.EventHandler MaximumSizeChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A065666EE2FD843A | Event | `public event System.EventHandler MdiChildActivate` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-65DC742C48BDCAE9 | Event | `public event System.EventHandler MenuComplete` | `System.Windows.Forms.Form` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-112C5293EE2B5C9D | Event | `public event System.EventHandler MenuStart` | `System.Windows.Forms.Form` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-51FB245D3643E891 | Event | `public event System.EventHandler MinimumSizeChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D51CF01256D13C2C | Event | `public event System.EventHandler MouseCaptureChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-372BB2C68C031FE3 | Event | `public event System.Windows.Forms.MouseEventHandler MouseClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BC72F9F5487EF8CC | Event | `public event System.Windows.Forms.MouseEventHandler MouseDoubleClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-53D3472579F46441 | Event | `public event System.Windows.Forms.MouseEventHandler MouseDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A70111E48E208C23 | Event | `public event System.EventHandler MouseEnter` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-21AE8C40F6B5291F | Event | `public event System.EventHandler MouseHover` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-69EE30A9DEECFD20 | Event | `public event System.EventHandler MouseLeave` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AFCB34D4297A5A21 | Event | `public event System.Windows.Forms.MouseEventHandler MouseMove` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DABEE909D26447C0 | Event | `public event System.Windows.Forms.MouseEventHandler MouseUp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E3469969FAD7D431 | Event | `public event System.Windows.Forms.MouseEventHandler MouseWheel` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A335C7D1893C31A | Event | `public event System.EventHandler Move` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-90886C33EF34227B | Event | `public event System.EventHandler PaddingChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ADACB756AD8E96B5 | Event | `public event System.Windows.Forms.PaintEventHandler Paint` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-81148278B5E8CB07 | Event | `public event System.EventHandler ParentChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A7A3DD7BD5A73F5A | Event | `public event System.Windows.Forms.PreviewKeyDownEventHandler PreviewKeyDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3782F143C1F5F8DA | Event | `public event System.Windows.Forms.QueryAccessibilityHelpEventHandler QueryAccessibilityHelp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0CBD30979307AD12 | Event | `public event System.Windows.Forms.QueryContinueDragEventHandler QueryContinueDrag` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1C6D1F0FD2472879 | Event | `public event System.EventHandler RegionChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-741523801F8DBA79 | Event | `public event System.EventHandler Resize` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-68A43C9525C84D56 | Event | `public event System.EventHandler ResizeBegin` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-23822C8972F1E8AB | Event | `public event System.EventHandler ResizeEnd` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D7212AF9077FB4A | Event | `public event System.EventHandler RightToLeftChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-35301F3A5EB5E50A | Event | `public event System.EventHandler RightToLeftLayoutChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F9ACBAABC8A30E6E | Event | `public event System.Windows.Forms.ScrollEventHandler Scroll` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BD818A982B946B95 | Event | `public event System.EventHandler Shown` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-505F0162F537AB3A | Event | `public event System.EventHandler SizeChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-64DBD76B3246D6FF | Event | `public event System.EventHandler StyleChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1F25EBC9BF0F2FF9 | Event | `public event System.EventHandler SystemColorsChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-534432E70E9821A7 | Event | `public event System.EventHandler TabIndexChanged` | `System.Windows.Forms.Form` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CFFE801FD18A71C0 | Event | `public event System.EventHandler TabStopChanged` | `System.Windows.Forms.Form` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E1AC8295EF15E5FB | Event | `public event System.EventHandler TextChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-83F04199F39B40CA | Event | `public event System.EventHandler Validated` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-53DDD15B1A3D415C | Event | `public event System.ComponentModel.CancelEventHandler Validating` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8D13B42262CF8373 | Event | `public event System.EventHandler VisibleChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F5EF053C292ED730 | Method | `public System.Void Activate()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A4ABAA477FDE87B1 | Method | `public System.Void AddOwnedForm(System.Windows.Forms.Form ownedForm)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9BF633751D8D0AED | Method | `public System.IAsyncResult BeginInvoke(System.Action method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-800C4A02FEAC387B | Method | `public System.IAsyncResult BeginInvoke(System.Delegate method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-386C490666CF58E7 | Method | `public System.IAsyncResult BeginInvoke(System.Delegate method, System.Object[] args)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E6A7104C41B53938 | Method | `public System.Void BringToFront()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D8B892B87C1D1B28 | Method | `public System.Void Close()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-13F87E802014B4F1 | Method | `public System.Boolean Contains(System.Windows.Forms.Control ctl)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-66EEE7CD19524A6A | Method | `public System.Void CreateControl()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-27AE873458986CDA | Method | `public System.Drawing.Graphics CreateGraphics()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1038DCA351523C95 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3C1F0069FB725DF6 | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D26F08547A068EC8 | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-122E653E8FB867DA | Method | `public System.Windows.Forms.DragDropEffects DoDragDropAsJson<!!0>(!!0 data, System.Windows.Forms.DragDropEffects allowedEffects) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-524EF2C164FAE9C6 | Method | `public System.Windows.Forms.DragDropEffects DoDragDropAsJson<!!0>(!!0 data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9DEFCD8D398ACB0B | Method | `public System.Void DrawToBitmap(System.Drawing.Bitmap bitmap, System.Drawing.Rectangle targetBounds)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E1DEDD9D1B452AE | Method | `public System.Object EndInvoke(System.IAsyncResult asyncResult)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-40B05888AEF92414 | Method | `public System.Windows.Forms.Form FindForm()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F7F69D7AD5352C69 | Method | `public System.Boolean Focus()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2822CAB32D940241 | Method | `public static System.Windows.Forms.Control FromChildHandle(System.IntPtr handle)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E6C0ADAE7D23FAE | Method | `public static System.Windows.Forms.Control FromHandle(System.IntPtr handle)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6FBAE39BA7B5AA95 | Method | `public static System.Drawing.SizeF GetAutoScaleSize(System.Drawing.Font font)` | `System.Windows.Forms.Form` ObsoleteAttribute("This method has been deprecated. Use the AutoScaleDimensions property instead.  https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-60BB2E5B67477B1A | Method | `public System.Windows.Forms.Control GetChildAtPoint(System.Drawing.Point pt)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B42668FACFDCE4B1 | Method | `public System.Windows.Forms.Control GetChildAtPoint(System.Drawing.Point pt, System.Windows.Forms.GetChildAtPointSkip skipValue)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5AF96A86FB255DA7 | Method | `public System.Windows.Forms.IContainerControl GetContainerControl()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A3AB5479813560F4 | Method | `public System.Windows.Forms.Control GetNextControl(System.Windows.Forms.Control ctl, System.Boolean forward)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DD5906103ED61151 | Method | `public virtual System.Drawing.Size GetPreferredSize(System.Drawing.Size proposedSize)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8208B0654741F810 | Method | `public System.Void Hide()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BCA946B26C5B1875 | Method | `public System.Void Invalidate()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E1EF7EF0B543DCEF | Method | `public System.Void Invalidate(System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BA1C523B9D9E052C | Method | `public System.Void Invalidate(System.Drawing.Rectangle rc)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ED683BA04C55B3E3 | Method | `public System.Void Invalidate(System.Drawing.Rectangle rc, System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-838189B00CBBAA8C | Method | `public System.Void Invalidate(System.Drawing.Region region)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0DB00FE2D0D0F6E1 | Method | `public System.Void Invalidate(System.Drawing.Region region, System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A1460A21DE4977D2 | Method | `public !!0 Invoke<!!0>(System.Func&#96;1<!!0> method) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B97094E1222B9C07 | Method | `public System.Object Invoke(System.Delegate method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DC53FF360563BC5D | Method | `public System.Object Invoke(System.Delegate method, System.Object[] args)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-68FE762CB2F5832B | Method | `public System.Void Invoke(System.Action method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CE8743FDD1B5B30C | Method | `public System.Threading.Tasks.Task InvokeAsync(System.Action callback, System.Threading.CancellationToken cancellationToken = null)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B30A23FC75AE8F1F | Method | `public System.Threading.Tasks.Task InvokeAsync(System.Func&#96;2<System.Threading.CancellationToken,System.Threading.Tasks.ValueTask> callback, System.Threading.CancellationToken cancellationToken = null)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0DECFBAEEC3DFFF6 | Method | `public System.Threading.Tasks.Task&#96;1<!!0> InvokeAsync<!!0>(System.Func&#96;1<!!0> callback, System.Threading.CancellationToken cancellationToken = null) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-93749611CAD0A045 | Method | `public System.Threading.Tasks.Task&#96;1<!!0> InvokeAsync<!!0>(System.Func&#96;2<System.Threading.CancellationToken,System.Threading.Tasks.ValueTask&#96;1<!!0>> callback, System.Threading.CancellationToken cancellationToken = null) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DC8DD0BB0D179C0D | Method | `public static System.Boolean IsKeyLocked(System.Windows.Forms.Keys keyVal)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CF2308AB504D4FC2 | Method | `public static System.Boolean IsMnemonic(System.Char charCode, System.String text)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-89E19504DF5002CD | Method | `public System.Void LayoutMdi(System.Windows.Forms.MdiLayout value)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0869E70708FF4939 | Method | `public System.Drawing.Size LogicalToDeviceUnits(System.Drawing.Size value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-70A6B6EB4DCEB73E | Method | `public System.Int32 LogicalToDeviceUnits(System.Int32 value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FC126E05347640BE | Method | `public System.Void PerformAutoScale()` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-70BDD9B1D2BC09EE | Method | `public System.Void PerformLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5513001031C3A1ED | Method | `public System.Void PerformLayout(System.Windows.Forms.Control affectedControl, System.String affectedProperty)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DB79A8371529D805 | Method | `public System.Drawing.Point PointToClient(System.Drawing.Point p)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-43BA5B73A4F42EAC | Method | `public System.Drawing.Point PointToScreen(System.Drawing.Point p)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8856DA02AAB87D49 | Method | `public System.Windows.Forms.PreProcessControlState PreProcessControlMessage(ref System.Windows.Forms.Message msg)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D242261679AA0298 | Method | `public virtual System.Boolean PreProcessMessage(ref System.Windows.Forms.Message msg)` | `System.Windows.Forms.Design.ComponentEditorForm`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-67A2A4D4CE3CB72E | Method | `public System.Drawing.Rectangle RectangleToClient(System.Drawing.Rectangle r)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-99BCD14DD037AC64 | Method | `public System.Drawing.Rectangle RectangleToScreen(System.Drawing.Rectangle r)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F06948B2AC797F48 | Method | `public virtual System.Void Refresh()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-41699EE2B6B28857 | Method | `public System.Void RemoveOwnedForm(System.Windows.Forms.Form ownedForm)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2D8A98E6C0F53BCA | Method | `public virtual System.Void ResetBackColor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0CA14899FF44F875 | Method | `public System.Void ResetBindings()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-42DE4B72C071FFBD | Method | `public virtual System.Void ResetCursor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A94D1656ACE2B06C | Method | `public virtual System.Void ResetFont()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9CC6591C4FC8F1CF | Method | `public virtual System.Void ResetForeColor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C630EF556D588FA3 | Method | `public System.Void ResetImeMode()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-392DA198C54EF8BB | Method | `public virtual System.Void ResetRightToLeft()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6486A1A3DB56FF60 | Method | `public virtual System.Void ResetText()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4F7D5DD99D0712C7 | Method | `public System.Void ResumeLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-47E1AF1B64EC86DF | Method | `public System.Void ResumeLayout(System.Boolean performLayout)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-385E57F34FED3A2B | Method | `public System.Void Scale(System.Drawing.SizeF factor)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-75AEF0F0A0B1D528 | Method | `public System.Void Scale(System.Single dx, System.Single dy)` | `System.Windows.Forms.Control` ObsoleteAttribute("This method has been deprecated. Use the Scale(SizeF ratio) method instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EBD51D21A0608DD2 | Method | `public System.Void Scale(System.Single ratio)` | `System.Windows.Forms.Control` ObsoleteAttribute("This method has been deprecated. Use the Scale(SizeF ratio) method instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8E092DAD35E16B2F | Method | `public System.Void ScaleBitmapLogicalToDevice(ref System.Drawing.Bitmap logicalBitmap)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0CF6B26CE242F623 | Method | `public System.Void ScrollControlIntoView(System.Windows.Forms.Control activeControl)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-65BE1BF20A5C3830 | Method | `public System.Void Select()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E5DE8619D31C74E | Method | `public System.Boolean SelectNextControl(System.Windows.Forms.Control ctl, System.Boolean forward, System.Boolean tabStopOnly, System.Boolean nested, System.Boolean wrap)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E60FEF68A6DA56E4 | Method | `public System.Void SendToBack()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7353F8B7E0137158 | Method | `public System.Void SetAutoScrollMargin(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-35A2DE7945BD8A5F | Method | `public System.Void SetBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2DDC1221BF421B85 | Method | `public System.Void SetBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-559E364599370A4E | Method | `public System.Void SetDesktopBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F31494D564BE53D5 | Method | `public System.Void SetDesktopLocation(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A72E9735A695E636 | Method | `public System.Void Show()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29D0D23205C4EB99 | Method | `public System.Void Show(System.Windows.Forms.IWin32Window owner)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2DF3986CFA826275 | Method | `public System.Threading.Tasks.Task ShowAsync(System.Windows.Forms.IWin32Window owner = null)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-71782C7C0B1F864D | Method | `public System.Windows.Forms.DialogResult ShowDialog()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-22D9F5292D2A4460 | Method | `public System.Windows.Forms.DialogResult ShowDialog(System.Windows.Forms.IWin32Window owner)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-170921BC5925408D | Method | `public System.Threading.Tasks.Task&#96;1<System.Windows.Forms.DialogResult> ShowDialogAsync()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A7D564A19120279C | Method | `public System.Threading.Tasks.Task&#96;1<System.Windows.Forms.DialogResult> ShowDialogAsync(System.Windows.Forms.IWin32Window owner)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5F359182CC683BDF | Method | `public virtual System.Windows.Forms.DialogResult ShowForm()` | `System.Windows.Forms.Design.ComponentEditorForm`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5D650427A6FA518C | Method | `public virtual System.Windows.Forms.DialogResult ShowForm(System.Int32 page)` | `System.Windows.Forms.Design.ComponentEditorForm`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-92CE8387893BC98D | Method | `public virtual System.Windows.Forms.DialogResult ShowForm(System.Windows.Forms.IWin32Window owner)` | `System.Windows.Forms.Design.ComponentEditorForm`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-019361C30F972AB5 | Method | `public virtual System.Windows.Forms.DialogResult ShowForm(System.Windows.Forms.IWin32Window owner, System.Int32 page)` | `System.Windows.Forms.Design.ComponentEditorForm`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D07EE0EC2B1F84B1 | Method | `public System.Void SuspendLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CDD34245B20EF966 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2FCB2F12191EC8EC | Method | `public System.Void Update()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5F5DFEECA01A1841 | Method | `public System.Boolean Validate()` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FDFFDBD66B41119C | Method | `public System.Boolean Validate(System.Boolean checkAutoValidate)` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FDA8678A5B2AD51C | Method | `public virtual System.Boolean ValidateChildren()` | `System.Windows.Forms.Form` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6364B407FEBEC980 | Method | `public virtual System.Boolean ValidateChildren(System.Windows.Forms.ValidationConstraints validationConstraints)` | `System.Windows.Forms.Form` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-13C75A3CE3C10A69 | Property | `System.Windows.Forms.IButtonControl AcceptButton { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-28D06F559CE4E6B5 | Property | `System.Windows.Forms.AccessibleObject AccessibilityObject { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AB1260C35F98B3D7 | Property | `System.String AccessibleDefaultActionDescription { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-919A4626641E55D8 | Property | `System.String AccessibleDescription { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5172D64842FE9D87 | Property | `System.String AccessibleName { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5720D760DB9EC499 | Property | `System.Windows.Forms.AccessibleRole AccessibleRole { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(-1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9901B596F0F2DB87 | Property | `System.Windows.Forms.Control ActiveControl { public get; public set; }` | `System.Windows.Forms.ContainerControl` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-437B6D7D09D48F2D | Property | `static System.Windows.Forms.Form ActiveForm { public get; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-490C53F3689F7EDE | Property | `System.Windows.Forms.Form ActiveMdiChild { public get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-723B5F1D9C327DFD | Property | `virtual System.Boolean AllowDrop { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-41DEE6C2C5170538 | Property | `System.Boolean AllowTransparency { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F0CFB231D0505A30 | Property | `virtual System.Windows.Forms.AnchorStyles Anchor { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true); DefaultValueAttribute(5) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F9109B23B224A93A | Property | `System.Boolean AutoScale { public get; public set; }` | `System.Windows.Forms.Form` ObsoleteAttribute("This property has been deprecated. Use the AutoScaleMode property instead.  https://go.microsoft.com/fwlink/?linkid=14202"); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-323DA11CBDD8084B | Property | `virtual System.Drawing.Size AutoScaleBaseSize { public get; public set; }` | `System.Windows.Forms.Form` LocalizableAttribute(true); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A4CE7DEB46F1A10E | Property | `System.Drawing.SizeF AutoScaleDimensions { public get; public set; }` | `System.Windows.Forms.ContainerControl` LocalizableAttribute(true); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A251F0056F588E6 | Property | `System.Windows.Forms.AutoScaleMode AutoScaleMode { public get; public set; }` | `System.Windows.Forms.ContainerControl` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D4A4CBBDB8CAF105 | Property | `virtual System.Boolean AutoScroll { public get; public set; }` | `System.Windows.Forms.Form` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-34B8AF9ACFA6C74E | Property | `System.Drawing.Size AutoScrollMargin { public get; public set; }` | `System.Windows.Forms.ScrollableControl` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-866E63857FC6294C | Property | `System.Drawing.Size AutoScrollMinSize { public get; public set; }` | `System.Windows.Forms.ScrollableControl` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F8FAF944FF5BA847 | Property | `virtual System.Drawing.Point AutoScrollOffset { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DefaultValueAttribute(System.Drawing.Point,"0, 0") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-22868843292EA4C4 | Property | `System.Drawing.Point AutoScrollPosition { public get; public set; }` | `System.Windows.Forms.ScrollableControl` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9FFC386099D3E933 | Property | `virtual System.Boolean AutoSize { public get; public set; }` | `System.Windows.Forms.Design.ComponentEditorForm` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-563344496D98B84E | Property | `System.Windows.Forms.AutoSizeMode AutoSizeMode { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(true); DefaultValueAttribute(1); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D7E2C6F629E5B3BE | Property | `virtual System.Windows.Forms.AutoValidate AutoValidate { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-378CB117E31D7267 | Property | `virtual System.Drawing.Color BackColor { public get; public set; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-943E3F7D99924440 | Property | `virtual System.Drawing.Image BackgroundImage { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D459827F3460D30A | Property | `virtual System.Windows.Forms.ImageLayout BackgroundImageLayout { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(1); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D60F173B21FDD2C8 | Property | `virtual System.Windows.Forms.BindingContext BindingContext { public get; public set; }` | `System.Windows.Forms.ContainerControl` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CAD99045004E9846 | Property | `System.Int32 Bottom { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-98E5A0C916507ECF | Property | `System.Drawing.Rectangle Bounds { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BC9C1DA4904355F9 | Property | `System.Windows.Forms.IButtonControl CancelButton { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A8886D22A0429694 | Property | `System.Boolean CanFocus { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-67E83544BA610736 | Property | `System.Boolean CanSelect { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E4A2C8FA0CB2E04 | Property | `System.Boolean Capture { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F46EB5735D6CB7CD | Property | `System.Boolean CausesValidation { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CDC166F38C033CBB | Property | `static System.Boolean CheckForIllegalCrossThreadCalls { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-62CD2196435D5EBC | Property | `System.Drawing.Rectangle ClientRectangle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-813116C0EFA2AEE6 | Property | `System.Drawing.Size ClientSize { public get; public set; }` | `System.Windows.Forms.Form` LocalizableAttribute(true); DesignerSerializationVisibilityAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-12396470043B73FE | Property | `System.String CompanyName { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B8366CDF0C9B63B1 | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EBE5B14C3B223B7B | Property | `System.Boolean ContainsFocus { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1F18465733BCEE28 | Property | `virtual System.Windows.Forms.ContextMenu ContextMenu { public get; public set; }` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-27A94D1EF388D0D7 | Property | `virtual System.Windows.Forms.ContextMenuStrip ContextMenuStrip { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0699A6AB066122E9 | Property | `System.Boolean ControlBox { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-375E1E8F59DB998E | Property | `System.Windows.Forms.Control+ControlCollection Controls { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5371602534B07AAF | Property | `System.Boolean Created { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4AD38AD0FCD928E4 | Property | `System.Drawing.SizeF CurrentAutoScaleDimensions { public get; }` | `System.Windows.Forms.ContainerControl` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E25880D5AF9FD72 | Property | `virtual System.Windows.Forms.Cursor Cursor { public get; public set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-66522CB766E02AD3 | Property | `System.Windows.Forms.ControlBindingsCollection DataBindings { public get; }` | `System.Windows.Forms.Control` DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EE3AEA394EE3BB19 | Property | `virtual System.Object DataContext { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); BindableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7DB11B1B6FA2E93D | Property | `static System.Drawing.Color DefaultBackColor { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-785605D6D3104F27 | Property | `static System.Drawing.Font DefaultFont { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F58713212F9FDEC6 | Property | `static System.Drawing.Color DefaultForeColor { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2679369629417C9F | Property | `System.Drawing.Rectangle DesktopBounds { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-41393BE198A9D41D | Property | `System.Drawing.Point DesktopLocation { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8E23AFD4FD6C8718 | Property | `System.Int32 DeviceDpi { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F93F82A998ED023C | Property | `System.Windows.Forms.DialogResult DialogResult { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-97BA69C4B3C066B9 | Property | `virtual System.Drawing.Rectangle DisplayRectangle { public get; }` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1A4E9C254FFF0679 | Property | `System.Boolean Disposing { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C019D37D4048C1D3 | Property | `virtual System.Windows.Forms.DockStyle Dock { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true); DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2733D991F8D08BC7 | Property | `System.Windows.Forms.ScrollableControl+DockPaddingEdges DockPadding { public get; }` | `System.Windows.Forms.ScrollableControl` DesignerSerializationVisibilityAttribute(0); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-10D6461E0B95E5FA | Property | `System.Boolean Enabled { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C1AEF7C620843EF8 | Property | `virtual System.Boolean Focused { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1023D006BED5141F | Property | `virtual System.Drawing.Font Font { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29A201F10A1E9896 | Property | `virtual System.Drawing.Color ForeColor { public get; public set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-78D242DA78033E77 | Property | `System.Drawing.Color FormBorderColor { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-28B228C44D641586 | Property | `System.Windows.Forms.FormBorderStyle FormBorderStyle { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(4) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-48D3FC2B777F0AA5 | Property | `System.Drawing.Color FormCaptionBackColor { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29DCC4C4520FB982 | Property | `System.Drawing.Color FormCaptionTextColor { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A49C1455D141AF55 | Property | `System.Windows.Forms.FormCornerPreference FormCornerPreference { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(0); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CE852BB5E235B64D | Property | `System.Windows.Forms.ScreenCaptureMode FormScreenCaptureMode { public get; public set; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F62EFB82EAFA4C90 | Property | `System.IntPtr Handle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9176E979947B6F36 | Property | `System.Boolean HasChildren { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-727FF1B61DD1B108 | Property | `System.Int32 Height { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-260ACB8486B49885 | Property | `System.Boolean HelpButton { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AD78A535B40855D6 | Property | `System.Windows.Forms.HScrollProperties HorizontalScroll { public get; }` | `System.Windows.Forms.ScrollableControl` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-531E0D6EF64106B7 | Property | `System.Drawing.Icon Icon { public get; public set; }` | `System.Windows.Forms.Form` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D63CC11E0846FA3F | Property | `System.Windows.Forms.ImeMode ImeMode { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-292D42B74A60857F | Property | `System.Boolean InvokeRequired { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-481DFC6223F5312A | Property | `System.Boolean IsAccessible { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E05CF7ADB764C90 | Property | `System.Boolean IsAncestorSiteInDesignMode { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B34E47CA57D17330 | Property | `System.Boolean IsDisposed { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-729038C040BA2A13 | Property | `System.Boolean IsHandleCreated { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A30AB8B993CCD883 | Property | `System.Boolean IsMdiChild { public get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-93AA0FAFD06BA658 | Property | `System.Boolean IsMdiContainer { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2522A02BCB191DEC | Property | `System.Boolean IsMirrored { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E49B6B11B73C0CDD | Property | `System.Boolean IsRestrictedWindow { public get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E043703CECB742E3 | Property | `System.Boolean KeyPreview { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8AB23B4A57EAD65B | Property | `virtual System.Windows.Forms.Layout.LayoutEngine LayoutEngine { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-97E8484FB0F57B51 | Property | `System.Int32 Left { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C4F9B2C57A177D8E | Property | `System.Drawing.Point Location { public get; public set; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-76D782A5B87CC4C6 | Property | `System.Windows.Forms.MenuStrip MainMenuStrip { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.ReferenceConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F8B4F0F2A3082A40 | Property | `System.Windows.Forms.Padding Margin { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-71277B2ADF75F527 | Property | `System.Boolean MaximizeBox { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E4E0F211D56742DF | Property | `virtual System.Drawing.Size MaximumSize { public get; public set; }` | `System.Windows.Forms.Form` LocalizableAttribute(true); DefaultValueAttribute(System.Drawing.Size,"0, 0") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EBB707AE58CC6482 | Property | `System.Windows.Forms.Form[] MdiChildren { public get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DCD5BC5B5A457BAD | Property | `System.Boolean MdiChildrenMinimizedAnchorBottom { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CFC8861004C5CAEA | Property | `System.Windows.Forms.Form MdiParent { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E3A770016066582 | Property | `System.Windows.Forms.MainMenu Menu { public get; public set; }` | `System.Windows.Forms.Form` ObsoleteAttribute("\u0060MainMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060MenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-86EB8DF537DEB8B6 | Property | `System.Windows.Forms.MainMenu MergedMenu { public get; }` | `System.Windows.Forms.Form` ObsoleteAttribute("\u0060MainMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060MenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-450A36BA11F80C53 | Property | `System.Boolean MinimizeBox { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-589E19B9ACA88B7A | Property | `virtual System.Drawing.Size MinimumSize { public get; public set; }` | `System.Windows.Forms.Form` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C96E82294BA96BD1 | Property | `System.Boolean Modal { public get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-644E51716C3317C2 | Property | `static System.Windows.Forms.Keys ModifierKeys { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-83BDB9F96405F373 | Property | `static System.Windows.Forms.MouseButtons MouseButtons { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6AB31B27C992AAC0 | Property | `static System.Drawing.Point MousePosition { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F1C9E715516C340B | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-32AB4816817E201F | Property | `System.Double Opacity { public get; public set; }` | `System.Windows.Forms.Form` TypeConverterAttribute(System.Windows.Forms.OpacityConverter); DefaultValueAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5147B097E215751F | Property | `System.Windows.Forms.Form[] OwnedForms { public get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E8F45E3C88B48BE | Property | `System.Windows.Forms.Form Owner { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-60EA0BBDAF4132ED | Property | `System.Windows.Forms.Padding Padding { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2A93072EA58AA38F | Property | `System.Windows.Forms.Control Parent { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AFE2CCBA5A330FBA | Property | `System.Windows.Forms.Form ParentForm { public get; }` | `System.Windows.Forms.ContainerControl` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C9C0C3337D759C67 | Property | `System.Drawing.Size PreferredSize { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9CB63E108B3F2F81 | Property | `System.String ProductName { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-64B8A1C5747B8C60 | Property | `System.String ProductVersion { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B6821E6B3398372B | Property | `System.Boolean RecreatingHandle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7969847606A7E24B | Property | `System.Drawing.Region Region { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F8A24EC7E978B839 | Property | `System.Drawing.Rectangle RestoreBounds { public get; }` | `System.Windows.Forms.Form` DesignerSerializationVisibilityAttribute(0); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B927CB5E51C3CC87 | Property | `System.Int32 Right { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3F1C8E52687A0D39 | Property | `virtual System.Windows.Forms.RightToLeft RightToLeft { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-154B99901DF6C6C6 | Property | `virtual System.Boolean RightToLeftLayout { public get; public set; }` | `System.Windows.Forms.Form` LocalizableAttribute(true); DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ADDC1A6795BD76C7 | Property | `System.Boolean ShowIcon { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E3BD1A25D5EDDDD | Property | `System.Boolean ShowInTaskbar { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-63321E8488298120 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1DC8ED990D1FC173 | Property | `System.Drawing.Size Size { public get; public set; }` | `System.Windows.Forms.Form` DesignerSerializationVisibilityAttribute(0); LocalizableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-66319602899887D7 | Property | `System.Windows.Forms.SizeGripStyle SizeGripStyle { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DC83E6BDFDED87D1 | Property | `System.Windows.Forms.FormStartPosition StartPosition { public get; public set; }` | `System.Windows.Forms.Form` LocalizableAttribute(true); DefaultValueAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7109CC9F37100A14 | Property | `System.Int32 TabIndex { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-42741E3706A2F678 | Property | `System.Boolean TabStop { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(true); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3AA6428494E93E08 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DF97AE82785D5C0B | Property | `virtual System.String Text { public get; public set; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6E9524B749C5AD37 | Property | `System.Int32 Top { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1BFC874965D373F3 | Property | `System.Boolean TopLevel { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B61F0AC23AE515C9 | Property | `System.Windows.Forms.Control TopLevelControl { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-399DEBE7093AB912 | Property | `System.Boolean TopMost { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-332BE886A4F855FE | Property | `System.Drawing.Color TransparencyKey { public get; public set; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2A8BC152760B5073 | Property | `System.Boolean UseWaitCursor { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(false); BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7830C0C059888FA9 | Property | `System.Windows.Forms.VScrollProperties VerticalScroll { public get; }` | `System.Windows.Forms.ScrollableControl` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E9626FC03EB6B33 | Property | `System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DAD0CD9DC0E673A1 | Property | `System.Int32 Width { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-621D7F143507E1BE | Property | `System.Windows.Forms.FormWindowState WindowState { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0DD408A987E30F5D | Property | `System.Windows.Forms.IWindowTarget WindowTarget { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-92D846632DCE0AD5 | Field | `protected const System.Int32 ScrollStateAutoScrolling = 1` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DB69FC64DA554B49 | Field | `protected const System.Int32 ScrollStateFullDrag = 16` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-321EC292BB92B78E | Field | `protected const System.Int32 ScrollStateHScrollVisible = 2` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E1DD3170E367CB5B | Field | `protected const System.Int32 ScrollStateUserHasScrolled = 8` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-45DF0C217A048A7B | Field | `protected const System.Int32 ScrollStateVScrollVisible = 4` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6FCF75BFB86CE427 | Method | `protected System.Void AccessibilityNotifyClients(System.Windows.Forms.AccessibleEvents accEvent, System.Int32 objectID, System.Int32 childID)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-61BCD70AC3D8C51C | Method | `protected internal System.Void AccessibilityNotifyClients(System.Windows.Forms.AccessibleEvents accEvent, System.Int32 childID)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BC3EF2B3809B16E0 | Method | `protected System.Void ActivateMdiChild(System.Windows.Forms.Form form)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-46C1DC1CEA831DA8 | Method | `protected virtual System.Void AdjustFormScrollbars(System.Boolean displayScrollbars)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FB398C33F220D86C | Method | `protected System.Void ApplyAutoScaling()` | `System.Windows.Forms.Form` ObsoleteAttribute("This method has been deprecated. Use the ApplyAutoScaling method instead.  https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F3888B51ABB1E385 | Method | `protected System.Void CenterToParent()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-74C8F70B9FF45975 | Method | `protected System.Void CenterToScreen()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A781AD899688A463 | Method | `protected virtual System.Windows.Forms.AccessibleObject CreateAccessibilityInstance()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-01B730D2CAA6B8D7 | Method | `protected virtual System.Windows.Forms.Control+ControlCollection CreateControlsInstance()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7B7D5131F0939410 | Method | `protected virtual System.Void CreateHandle()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DB3DE21B3203B172 | Method | `protected virtual System.Void DefWndProc(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3409AC29840C4C8C | Method | `protected virtual System.Void DestroyHandle()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6CB75587941B0347 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EFF4215DEDD31D3E | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BC57952D93E4E3AA | Method | `protected virtual System.Windows.Forms.AccessibleObject GetAccessibilityObjectById(System.Int32 objectId)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9154FFFD33605CA9 | Method | `protected System.Windows.Forms.AutoSizeMode GetAutoSizeMode()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-71E3A8BD769F8C53 | Method | `protected virtual System.Drawing.Rectangle GetScaledBounds(System.Drawing.Rectangle bounds, System.Drawing.SizeF factor, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-71DA5EBC8939844C | Method | `protected System.Boolean GetScrollState(System.Int32 bit)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8F090A43253320C4 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D501BB6F05879C13 | Method | `protected System.Boolean GetStyle(System.Windows.Forms.ControlStyles flag)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F958372919CFD970 | Method | `protected System.Boolean GetTopLevel()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B9E6630BF379F0BA | Method | `protected virtual System.Void InitLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C01AA3B7CA485504 | Method | `protected System.Void InvokeGotFocus(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2AC0AB034C7A246C | Method | `protected System.Void InvokeLostFocus(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A5CD37E91467199E | Method | `protected System.Void InvokeOnClick(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8C39770E977B8E5C | Method | `protected System.Void InvokePaint(System.Windows.Forms.Control c, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-350C65E3D3873A8E | Method | `protected System.Void InvokePaintBackground(System.Windows.Forms.Control c, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DF16E7A9D282A3CC | Method | `protected virtual System.Boolean IsInputChar(System.Char charCode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-09A115B3CD026100 | Method | `protected virtual System.Boolean IsInputKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D5D7DD1693F89EAD | Method | `protected virtual System.Void NotifyInvalidate(System.Drawing.Rectangle invalidatedArea)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E507149DDF190B7C | Method | `protected virtual System.Void OnActivated(System.EventArgs e)` | `System.Windows.Forms.Design.ComponentEditorForm`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DE67ABCAD1C4F7A6 | Method | `protected virtual System.Void OnAutoSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5BDC66EEF57EDFB7 | Method | `protected virtual System.Void OnAutoValidateChanged(System.EventArgs e)` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3FAF4CE774872706 | Method | `protected virtual System.Void OnBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B730FF441982D8A1 | Method | `protected virtual System.Void OnBackgroundImageChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A7DD3A088CE35273 | Method | `protected virtual System.Void OnBackgroundImageLayoutChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-57F09A0D5CDE40EA | Method | `protected virtual System.Void OnBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-556CBE64CEDDAE30 | Method | `protected virtual System.Void OnCausesValidationChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-31D89D44166FD8B4 | Method | `protected virtual System.Void OnChangeUICues(System.Windows.Forms.UICuesEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-76714DA23B7933D9 | Method | `protected virtual System.Void OnClick(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-07C5B0050BE9EB7A | Method | `protected virtual System.Void OnClientSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E43F378B01B8E961 | Method | `protected virtual System.Void OnClosed(System.EventArgs e)` | `System.Windows.Forms.Form` ObsoleteAttribute("Form.OnClosing, Form.OnClosed and the corresponding events are obsolete. Use Form.OnFormClosing, Form.OnFormClosed, Form.FormClosing and Form.FormClosed instead.",false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B2EEFDA568AFAC8D | Method | `protected virtual System.Void OnClosing(System.ComponentModel.CancelEventArgs e)` | `System.Windows.Forms.Form` ObsoleteAttribute("Form.OnClosing, Form.OnClosed and the corresponding events are obsolete. Use Form.OnFormClosing, Form.OnFormClosed, Form.FormClosing and Form.FormClosed instead.",false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E718AE6846A81458 | Method | `protected virtual System.Void OnContextMenuChanged(System.EventArgs e)` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AA704F5B2E82275C | Method | `protected virtual System.Void OnContextMenuStripChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-976E63FCD8E3D304 | Method | `protected virtual System.Void OnControlAdded(System.Windows.Forms.ControlEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6AFA644355F427FE | Method | `protected virtual System.Void OnControlRemoved(System.Windows.Forms.ControlEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ABA7BB70CDD06847 | Method | `protected virtual System.Void OnCreateControl()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-66AB0C880C4678A9 | Method | `protected virtual System.Void OnCursorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A881092A326FB590 | Method | `protected virtual System.Void OnDataContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E3E9B0EF693E3FBA | Method | `protected virtual System.Void OnDeactivate(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8025AF9280B46CF6 | Method | `protected virtual System.Void OnDockChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-492F73A8A9A64C9E | Method | `protected virtual System.Void OnDoubleClick(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2DED1D93E5561CAC | Method | `protected virtual System.Void OnDpiChanged(System.Windows.Forms.DpiChangedEventArgs e)` | `System.Windows.Forms.Form` BrowsableAttribute(true); DesignerSerializationVisibilityAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2ADBDB963B93032B | Method | `protected virtual System.Void OnDpiChangedAfterParent(System.EventArgs e)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3E3D2A61F07907D2 | Method | `protected virtual System.Void OnDpiChangedBeforeParent(System.EventArgs e)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BD7E00D111497409 | Method | `protected virtual System.Void OnDragDrop(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-161161F2D4DCA4EE | Method | `protected virtual System.Void OnDragEnter(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1783E9A3CE4A469D | Method | `protected virtual System.Void OnDragLeave(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3B2408569B1BF6EE | Method | `protected virtual System.Void OnDragOver(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D5B27E7E5C620588 | Method | `protected virtual System.Void OnEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A06EA26464F3869E | Method | `protected internal virtual System.Void OnEnter(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DCB0442FDFF20994 | Method | `protected virtual System.Void OnFontChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-35AC4CDB43797F55 | Method | `protected virtual System.Void OnForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4A2C2641D34E57C8 | Method | `protected virtual System.Void OnFormBorderColorChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0FA3EBC923ED14D0 | Method | `protected virtual System.Void OnFormCaptionBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0D3BE9293932FC9E | Method | `protected virtual System.Void OnFormCaptionTextColorChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E5A1D274697D6E8B | Method | `protected virtual System.Void OnFormClosed(System.Windows.Forms.FormClosedEventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4BAB9676FCDC95F3 | Method | `protected virtual System.Void OnFormClosing(System.Windows.Forms.FormClosingEventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-77F006924FF2A08E | Method | `protected virtual System.Void OnFormCornerPreferenceChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D041AD311CC02748 | Method | `protected virtual System.Boolean OnGetDpiScaledSize(System.Int32 deviceDpiOld, System.Int32 deviceDpiNew, ref System.Drawing.Size desiredSize)` | `System.Windows.Forms.Form` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B65D33B0521DE296 | Method | `protected virtual System.Void OnGiveFeedback(System.Windows.Forms.GiveFeedbackEventArgs gfbevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DA503DD70E32786C | Method | `protected virtual System.Void OnGotFocus(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9006F477C98994A0 | Method | `protected virtual System.Void OnHandleCreated(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-73CA568482FE2DA1 | Method | `protected virtual System.Void OnHandleDestroyed(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5EE4EAA84C614C0C | Method | `protected virtual System.Void OnHelpButtonClicked(System.ComponentModel.CancelEventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4901FA0CDE6BF291 | Method | `protected virtual System.Void OnHelpRequested(System.Windows.Forms.HelpEventArgs e)` | `System.Windows.Forms.Design.ComponentEditorForm`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4423DAF816A9292A | Method | `protected virtual System.Void OnImeModeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C6A061FD1EB96BC | Method | `protected virtual System.Void OnInputLanguageChanged(System.Windows.Forms.InputLanguageChangedEventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F5A159E84CDC67D6 | Method | `protected virtual System.Void OnInputLanguageChanging(System.Windows.Forms.InputLanguageChangingEventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9D96934402609B69 | Method | `protected virtual System.Void OnInvalidated(System.Windows.Forms.InvalidateEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A97C520A86AE04A5 | Method | `protected virtual System.Void OnKeyDown(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A8241CC9AA9794CA | Method | `protected virtual System.Void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-87331FC1BC4AEB52 | Method | `protected virtual System.Void OnKeyUp(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F1DCAA141B9F5A7C | Method | `protected virtual System.Void OnLayout(System.Windows.Forms.LayoutEventArgs levent)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-09620A208F53C2AD | Method | `protected internal virtual System.Void OnLeave(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9FA4EA6E346DEFA6 | Method | `protected virtual System.Void OnLoad(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-02480903FF5DB6CB | Method | `protected virtual System.Void OnLocationChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-01ECAB4A3D80A743 | Method | `protected virtual System.Void OnLostFocus(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ED9EE5E8A296DD5C | Method | `protected virtual System.Void OnMarginChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1D50BA1B5825FEFA | Method | `protected virtual System.Void OnMaximizedBoundsChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-18A15A82F10B9A79 | Method | `protected virtual System.Void OnMaximumSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8ADD5ABEFFDCA3EA | Method | `protected virtual System.Void OnMdiChildActivate(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D2742EED6E8C5611 | Method | `protected virtual System.Void OnMenuComplete(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-79F76FED19162723 | Method | `protected virtual System.Void OnMenuStart(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-85D480558E6E0200 | Method | `protected virtual System.Void OnMinimumSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4F9A0DED36901FF9 | Method | `protected virtual System.Void OnMouseCaptureChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6C97BC32ED8B9354 | Method | `protected virtual System.Void OnMouseClick(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DC28911787829FF4 | Method | `protected virtual System.Void OnMouseDoubleClick(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D481D62F62E0E732 | Method | `protected virtual System.Void OnMouseDown(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-42B2157F8387B795 | Method | `protected virtual System.Void OnMouseEnter(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-09D9CF6689C01662 | Method | `protected virtual System.Void OnMouseHover(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7C85F99BF725FE7F | Method | `protected virtual System.Void OnMouseLeave(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-257AB2F03A19DFF2 | Method | `protected virtual System.Void OnMouseMove(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D54A9C098882D5D7 | Method | `protected virtual System.Void OnMouseUp(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-42CBF7EE12CF6FE8 | Method | `protected virtual System.Void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4C9693B765A786D2 | Method | `protected virtual System.Void OnMove(System.EventArgs e)` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-802D6423E486AA23 | Method | `protected virtual System.Void OnNotifyMessage(System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B4690548B249441 | Method | `protected virtual System.Void OnPaddingChanged(System.EventArgs e)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BEB1A87EE884D296 | Method | `protected virtual System.Void OnPaint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B98A5D492E790097 | Method | `protected virtual System.Void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-590FE7178E1F4ED2 | Method | `protected virtual System.Void OnParentBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-288F8CA56E766190 | Method | `protected virtual System.Void OnParentBackgroundImageChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B650BB8AA40CE7DA | Method | `protected virtual System.Void OnParentBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-17D0A1DFC0724BD2 | Method | `protected virtual System.Void OnParentChanged(System.EventArgs e)` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5454BE1BF7C02F80 | Method | `protected virtual System.Void OnParentCursorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E8EF26D47186D46F | Method | `protected virtual System.Void OnParentDataContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CFE260E00A0ADE9C | Method | `protected virtual System.Void OnParentEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4132EC7B00B27029 | Method | `protected virtual System.Void OnParentFontChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EADD45B8E0142EEF | Method | `protected virtual System.Void OnParentForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D13D820C0D20EC89 | Method | `protected virtual System.Void OnParentRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-531D0E71554576B2 | Method | `protected virtual System.Void OnParentVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EAE3130CFBD6D215 | Method | `protected virtual System.Void OnPreviewKeyDown(System.Windows.Forms.PreviewKeyDownEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-453188E920156BD5 | Method | `protected virtual System.Void OnPrint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-25603FA96FDD1555 | Method | `protected virtual System.Void OnQueryContinueDrag(System.Windows.Forms.QueryContinueDragEventArgs qcdevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7A0E475A5D7EFCD9 | Method | `protected virtual System.Void OnRegionChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7E2F9F90A8B96461 | Method | `protected virtual System.Void OnResize(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0A0DA153F6F2DBC | Method | `protected virtual System.Void OnResizeBegin(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B188B246855B08A6 | Method | `protected virtual System.Void OnResizeEnd(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-23AAC7B388BA05CC | Method | `protected virtual System.Void OnRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0D9E00CB2D138AB2 | Method | `protected virtual System.Void OnRightToLeftLayoutChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F69CE04ECC52370C | Method | `protected virtual System.Void OnScroll(System.Windows.Forms.ScrollEventArgs se)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F16FC1B1FF526493 | Method | `protected virtual System.Void OnSelChangeSelector(System.Object source, System.Windows.Forms.TreeViewEventArgs e)` | `System.Windows.Forms.Design.ComponentEditorForm`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-309E70D76E05975F | Method | `protected virtual System.Void OnShown(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FEF643A7AC48C195 | Method | `protected virtual System.Void OnSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29AD887095C0F3DA | Method | `protected virtual System.Void OnStyleChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9F8F82043CE33447 | Method | `protected virtual System.Void OnSystemColorsChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-22FA6A8FA234721F | Method | `protected virtual System.Void OnTabIndexChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-061C41491028E18E | Method | `protected virtual System.Void OnTabStopChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CF0D9953EB65E287 | Method | `protected virtual System.Void OnTextChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F8AD95CD7F390A9F | Method | `protected virtual System.Void OnValidated(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-34CB5C6139192055 | Method | `protected virtual System.Void OnValidating(System.ComponentModel.CancelEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8D6D8AD423428B4E | Method | `protected virtual System.Void OnVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3C0B7DCDD18B2096 | Method | `protected virtual System.Boolean ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5FF9C29C4C6ECE20 | Method | `protected virtual System.Boolean ProcessDialogChar(System.Char charCode)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-106E40D38A3E6BDC | Method | `protected virtual System.Boolean ProcessDialogKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-59F9D6F519F38A21 | Method | `protected virtual System.Boolean ProcessKeyEventArgs(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F06C0D41EEE434B7 | Method | `protected internal virtual System.Boolean ProcessKeyMessage(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2FA99E3FD7DC3333 | Method | `protected virtual System.Boolean ProcessKeyPreview(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B9DFF861C2F32FFA | Method | `protected internal virtual System.Boolean ProcessMnemonic(System.Char charCode)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F5D642827DE5C15B | Method | `protected virtual System.Boolean ProcessTabKey(System.Boolean forward)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-336EED4A7F8CCFB2 | Method | `protected System.Void RaiseDragEvent(System.Object key, System.Windows.Forms.DragEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E7FC32311B2241A9 | Method | `protected System.Void RaiseKeyEvent(System.Object key, System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7965A5B78F52DF18 | Method | `protected System.Void RaiseMouseEvent(System.Object key, System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DC5F47D229BAD7C1 | Method | `protected System.Void RaisePaintEvent(System.Object key, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-23355DCF42366E40 | Method | `protected System.Void RecreateHandle()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F1BD163C252A808D | Method | `protected static System.Boolean ReflectMessage(System.IntPtr hWnd, ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F93974232A118D72 | Method | `protected virtual System.Void RescaleConstantsForDpi(System.Int32 deviceDpiOld, System.Int32 deviceDpiNew)` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C29782BCB57D8F9F | Method | `protected System.Void ResetMouseEventArgs()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E9B62DB4D00435AC | Method | `protected System.Drawing.ContentAlignment RtlTranslateAlignment(System.Drawing.ContentAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2DA05411192244D2 | Method | `protected System.Windows.Forms.HorizontalAlignment RtlTranslateAlignment(System.Windows.Forms.HorizontalAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-83B388A4540EFF2E | Method | `protected System.Windows.Forms.LeftRightAlignment RtlTranslateAlignment(System.Windows.Forms.LeftRightAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C30A2F38FAAFE8D | Method | `protected internal System.Drawing.ContentAlignment RtlTranslateContent(System.Drawing.ContentAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E1042645A8FF3CC0 | Method | `protected System.Windows.Forms.HorizontalAlignment RtlTranslateHorizontal(System.Windows.Forms.HorizontalAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-362167A971C4D490 | Method | `protected System.Windows.Forms.LeftRightAlignment RtlTranslateLeftRight(System.Windows.Forms.LeftRightAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B266E1ADCCC9EE21 | Method | `protected virtual System.Void ScaleControl(System.Drawing.SizeF factor, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-088630B5F6AA1B8A | Method | `protected virtual System.Void ScaleCore(System.Single x, System.Single y)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D4D9472382E18B6A | Method | `protected virtual System.Void ScaleMinMaxSize(System.Single xScaleFactor, System.Single yScaleFactor, System.Boolean updateContainerSize = true)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-78821DB9C3F6008C | Method | `protected virtual System.Drawing.Point ScrollToControl(System.Windows.Forms.Control activeControl)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-59FEF4FF3878AEF8 | Method | `protected virtual System.Void Select(System.Boolean directed, System.Boolean forward)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-64B483A565F724A0 | Method | `protected System.Void SetAutoSizeMode(System.Windows.Forms.AutoSizeMode mode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B89E825F04FDF394 | Method | `protected virtual System.Void SetBoundsCore(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A07369CF681978DA | Method | `protected virtual System.Void SetClientSizeCore(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-17D6F4D92FCAF63C | Method | `protected System.Void SetDisplayRectLocation(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4DE8204C435C49A2 | Method | `protected System.Void SetScrollState(System.Int32 bit, System.Boolean value)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DD152DDDE3CCC4D0 | Method | `protected System.Void SetStyle(System.Windows.Forms.ControlStyles flag, System.Boolean value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B18C493E9E1BF14 | Method | `protected System.Void SetTopLevel(System.Boolean value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5D44A01334A54D04 | Method | `protected virtual System.Void SetVisibleCore(System.Boolean value)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E17791E76B950C7 | Method | `protected virtual System.Drawing.Size SizeFromClientSize(System.Drawing.Size clientSize)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9EF26D71C1F734C6 | Method | `protected System.Void UpdateBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C43AEC88B44EB202 | Method | `protected System.Void UpdateBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Int32 clientWidth, System.Int32 clientHeight)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-234D1089F39B41C1 | Method | `protected internal System.Void UpdateBounds()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6E00109612930CA6 | Method | `protected virtual System.Void UpdateDefaultButton()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7CBEED699A58F8A8 | Method | `protected System.Void UpdateStyles()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5DECDA6147E9D142 | Method | `protected System.Void UpdateZOrder()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-81F5AA540E5B5738 | Method | `protected virtual System.Void WndProc(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B1525E2C0B200752 | Property | `System.Drawing.SizeF AutoScaleFactor { protected get; }` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-128E4848456C618A | Property | `virtual System.Boolean CanEnableIme { protected get; }` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8ADFAF291086BD25 | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-54235F4CA02B198C | Property | `virtual System.Windows.Forms.CreateParams CreateParams { protected get; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-66F1EB0F850F9343 | Property | `virtual System.Windows.Forms.Cursor DefaultCursor { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BC31D97E18DE7E83 | Property | `virtual System.Windows.Forms.ImeMode DefaultImeMode { protected get; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6105299079C41370 | Property | `virtual System.Windows.Forms.Padding DefaultMargin { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-65F699271336D91B | Property | `virtual System.Drawing.Size DefaultMaximumSize { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6413498DEAE4353C | Property | `virtual System.Drawing.Size DefaultMinimumSize { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4602B12DF79E8D68 | Property | `virtual System.Windows.Forms.Padding DefaultPadding { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D4BA0BC178E4E034 | Property | `virtual System.Drawing.Size DefaultSize { protected get; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0AB7AFEADAE175EB | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E1A51194F575B024 | Property | `virtual System.Boolean DoubleBuffered { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5DE55B3B062ED155 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E95B57CC98316319 | Property | `System.Int32 FontHeight { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D792F9BE64152222 | Property | `System.Boolean HScroll { protected get; protected set; }` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7E7C71062E5DBF73 | Property | `virtual System.Windows.Forms.ImeMode ImeModeBase { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-85D5F27FD2DE93B8 | Property | `System.Drawing.Rectangle MaximizedBounds { protected get; protected set; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EF4C70A5751A145A | Property | `static System.Windows.Forms.ImeMode PropagatingImeMode { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-27FEA654C2DF02CC | Property | `System.Boolean RenderRightToLeft { protected internal get; }` | `System.Windows.Forms.Control` ObsoleteAttribute("This property has been deprecated. Please use RightToLeft instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8C5A9F28DFD416E8 | Property | `System.Boolean ResizeRedraw { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-33B876AD9AFBBF63 | Property | `virtual System.Boolean ScaleChildren { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4EE64154B429777B | Property | `virtual System.Boolean ShowFocusCues { protected internal get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F21A61F74E63601 | Property | `virtual System.Boolean ShowKeyboardCues { protected internal get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BB2F3A60BE692579 | Property | `virtual System.Boolean ShowWithoutActivation { protected get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2457A77C0963494E | Property | `System.Boolean VScroll { protected get; protected set; }` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
