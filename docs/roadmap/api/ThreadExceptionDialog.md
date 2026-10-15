# ThreadExceptionDialog：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/ThreadExceptionDialog.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.ThreadExceptionDialog`；基类：`System.Windows.Forms.Form`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **143**，公开事件 **99**，公开方法 **95**；受保护扩展成员 **216**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-804C375F093EE374 | Constructor | `public ThreadExceptionDialog(System.Exception t)` | `System.Windows.Forms.ThreadExceptionDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D4A2693A94633965 | Event | `public event System.EventHandler Activated` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29FB6386B2792523 | Event | `public event System.EventHandler AutoSizeChanged` | `System.Windows.Forms.ThreadExceptionDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A0EA4343EE3B288C | Event | `public event System.EventHandler AutoValidateChanged` | `System.Windows.Forms.Form` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A0FA50D539F87C56 | Event | `public event System.EventHandler BackColorChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-48AC563792F8B8F3 | Event | `public event System.EventHandler BackgroundImageChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-483E661810169748 | Event | `public event System.EventHandler BackgroundImageLayoutChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC1D70A2AB5CC132 | Event | `public event System.EventHandler BindingContextChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CC8EE87C2F58055B | Event | `public event System.EventHandler CausesValidationChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1184A0DB7120D97C | Event | `public event System.Windows.Forms.UICuesEventHandler ChangeUICues` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F8194764D8182920 | Event | `public event System.EventHandler Click` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0DBF3D94808DAAE3 | Event | `public event System.EventHandler ClientSizeChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-91923693165998D6 | Event | `public event System.EventHandler Closed` | `System.Windows.Forms.Form` BrowsableAttribute(false); ObsoleteAttribute("Form.OnClosing, Form.OnClosed and the corresponding events are obsolete. Use Form.OnFormClosing, Form.OnFormClosed, Form.FormClosing and Form.FormClosed instead.",false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A3A6764F12A8681C | Event | `public event System.ComponentModel.CancelEventHandler Closing` | `System.Windows.Forms.Form` BrowsableAttribute(false); ObsoleteAttribute("Form.OnClosing, Form.OnClosed and the corresponding events are obsolete. Use Form.OnFormClosing, Form.OnFormClosed, Form.FormClosing and Form.FormClosed instead.",false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B865EB96F93E596E | Event | `public event System.EventHandler ContextMenuChanged` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-76A808B7AF1F44F5 | Event | `public event System.EventHandler ContextMenuStripChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9AE338CF5D4BAEC3 | Event | `public event System.Windows.Forms.ControlEventHandler ControlAdded` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CCE993CB48958341 | Event | `public event System.Windows.Forms.ControlEventHandler ControlRemoved` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DBF3203390A72856 | Event | `public event System.EventHandler CursorChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-01256EA07A7D7CF2 | Event | `public event System.EventHandler DataContextChanged` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F5B5E5D1C546F55 | Event | `public event System.EventHandler Deactivate` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8B63D6C51B6B147D | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BE2BAF5515238C8E | Event | `public event System.EventHandler DockChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1657513286032C34 | Event | `public event System.EventHandler DoubleClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-07C6F4257C109977 | Event | `public event System.Windows.Forms.DpiChangedEventHandler DpiChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B40AFE8F9B51D99 | Event | `public event System.EventHandler DpiChangedAfterParent` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EAFC8712567E5715 | Event | `public event System.EventHandler DpiChangedBeforeParent` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3F781D9CA7B25ED8 | Event | `public event System.Windows.Forms.DragEventHandler DragDrop` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4BBA429A94D3D188 | Event | `public event System.Windows.Forms.DragEventHandler DragEnter` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DB49A3DA4950080A | Event | `public event System.EventHandler DragLeave` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-861FC77FD13D775A | Event | `public event System.Windows.Forms.DragEventHandler DragOver` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-667A3B5AF239EB8E | Event | `public event System.EventHandler EnabledChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7E55208A914EC72C | Event | `public event System.EventHandler Enter` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C0AE978676C2726A | Event | `public event System.EventHandler FontChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C8A6E2F6E673D03 | Event | `public event System.EventHandler ForeColorChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1BF83CE5E8DF29F5 | Event | `public event System.EventHandler FormBorderColorChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-25F88E0ED3A35FB6 | Event | `public event System.EventHandler FormCaptionBackColorChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-411D81ED50A7EB37 | Event | `public event System.EventHandler FormCaptionTextColorChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A25D9F581D947579 | Event | `public event System.Windows.Forms.FormClosedEventHandler FormClosed` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9AB22B4D78DE0523 | Event | `public event System.Windows.Forms.FormClosingEventHandler FormClosing` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9852D91347B0722A | Event | `public event System.EventHandler FormCornerPreferenceChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-60C9E7F9FF957BDA | Event | `public event System.Windows.Forms.GiveFeedbackEventHandler GiveFeedback` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-98A4DD2E5018F2B7 | Event | `public event System.EventHandler GotFocus` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-67C711AC7C0ACF5E | Event | `public event System.EventHandler HandleCreated` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3A6648FC3672D04A | Event | `public event System.EventHandler HandleDestroyed` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-48D71035C06998A2 | Event | `public event System.ComponentModel.CancelEventHandler HelpButtonClicked` | `System.Windows.Forms.Form` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-335D5B481FF8CB81 | Event | `public event System.Windows.Forms.HelpEventHandler HelpRequested` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A22EBFC9E50667FE | Event | `public event System.EventHandler ImeModeChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C7ACD52DF9B40F93 | Event | `public event System.Windows.Forms.InputLanguageChangedEventHandler InputLanguageChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F25F56E2D047E053 | Event | `public event System.Windows.Forms.InputLanguageChangingEventHandler InputLanguageChanging` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2A7F16E1D4FBA2F9 | Event | `public event System.Windows.Forms.InvalidateEventHandler Invalidated` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BF08294BFEC4A854 | Event | `public event System.Windows.Forms.KeyEventHandler KeyDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-029F3A24C43F522F | Event | `public event System.Windows.Forms.KeyPressEventHandler KeyPress` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FE3C288120ED1C87 | Event | `public event System.Windows.Forms.KeyEventHandler KeyUp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E2DB9F197E5AC4F3 | Event | `public event System.Windows.Forms.LayoutEventHandler Layout` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B1DC2F9BB0D2739 | Event | `public event System.EventHandler Leave` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-34412F72AA8C9A3F | Event | `public event System.EventHandler Load` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A48D6849210C3A93 | Event | `public event System.EventHandler LocationChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4A1D81841FAA1BB0 | Event | `public event System.EventHandler LostFocus` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-57E9E2759BAD4E74 | Event | `public event System.EventHandler MarginChanged` | `System.Windows.Forms.Form` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-63808D57B308578D | Event | `public event System.EventHandler MaximizedBoundsChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-75C8DA4B00960C98 | Event | `public event System.EventHandler MaximumSizeChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-786EF90123AE5653 | Event | `public event System.EventHandler MdiChildActivate` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D82F93042E925A91 | Event | `public event System.EventHandler MenuComplete` | `System.Windows.Forms.Form` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CBF66CAB8FFC7112 | Event | `public event System.EventHandler MenuStart` | `System.Windows.Forms.Form` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4C6541C8DCC5857B | Event | `public event System.EventHandler MinimumSizeChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0DE2B5BFF9637C0F | Event | `public event System.EventHandler MouseCaptureChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-529F68FDE9AC3F54 | Event | `public event System.Windows.Forms.MouseEventHandler MouseClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3C0FEADC2614D464 | Event | `public event System.Windows.Forms.MouseEventHandler MouseDoubleClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0333C7B7143DF3FB | Event | `public event System.Windows.Forms.MouseEventHandler MouseDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E9E9C8B439FB634 | Event | `public event System.EventHandler MouseEnter` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-01A8904E0E0E1760 | Event | `public event System.EventHandler MouseHover` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7EB4F42FA4D12819 | Event | `public event System.EventHandler MouseLeave` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-80156BD4900A446E | Event | `public event System.Windows.Forms.MouseEventHandler MouseMove` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B87FE23189E0FF1C | Event | `public event System.Windows.Forms.MouseEventHandler MouseUp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C857166E75B3092A | Event | `public event System.Windows.Forms.MouseEventHandler MouseWheel` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-84ECADEB222447A7 | Event | `public event System.EventHandler Move` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2F98ED9CDD355FAC | Event | `public event System.EventHandler PaddingChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-64645DAB1093E0A5 | Event | `public event System.Windows.Forms.PaintEventHandler Paint` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4AABF1228A921712 | Event | `public event System.EventHandler ParentChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7829A5714FB55E9A | Event | `public event System.Windows.Forms.PreviewKeyDownEventHandler PreviewKeyDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3A499DF1AC2542BA | Event | `public event System.Windows.Forms.QueryAccessibilityHelpEventHandler QueryAccessibilityHelp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-305619ACFFE40AEF | Event | `public event System.Windows.Forms.QueryContinueDragEventHandler QueryContinueDrag` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2273816D53162058 | Event | `public event System.EventHandler RegionChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5C987E1EDF6D83DD | Event | `public event System.EventHandler Resize` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E10FB74DEBFF56E8 | Event | `public event System.EventHandler ResizeBegin` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D60235E6459CA07D | Event | `public event System.EventHandler ResizeEnd` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DABDBD6F21BE3328 | Event | `public event System.EventHandler RightToLeftChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5A1AF99FFF76B406 | Event | `public event System.EventHandler RightToLeftLayoutChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-695CCE204E46A723 | Event | `public event System.Windows.Forms.ScrollEventHandler Scroll` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-99745DC808C24FD2 | Event | `public event System.EventHandler Shown` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B5CB344BE6AF02E1 | Event | `public event System.EventHandler SizeChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A76598A5BE2537D4 | Event | `public event System.EventHandler StyleChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A00FED00B84A3905 | Event | `public event System.EventHandler SystemColorsChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D614150FF87DF4A | Event | `public event System.EventHandler TabIndexChanged` | `System.Windows.Forms.Form` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F3716DC55FC32F3 | Event | `public event System.EventHandler TabStopChanged` | `System.Windows.Forms.Form` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50F099D861B4B11C | Event | `public event System.EventHandler TextChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CC5F1E2788212EB6 | Event | `public event System.EventHandler Validated` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-958B8D904D861E3C | Event | `public event System.ComponentModel.CancelEventHandler Validating` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-64C96A7795AD6815 | Event | `public event System.EventHandler VisibleChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-21A8957D307343F7 | Method | `public System.Void Activate()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-41588ED1F7147BC1 | Method | `public System.Void AddOwnedForm(System.Windows.Forms.Form ownedForm)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-816D9925CD3C51D0 | Method | `public System.IAsyncResult BeginInvoke(System.Action method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-31F72162EC845468 | Method | `public System.IAsyncResult BeginInvoke(System.Delegate method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CE8F25814705675B | Method | `public System.IAsyncResult BeginInvoke(System.Delegate method, System.Object[] args)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3BA9D2226188339E | Method | `public System.Void BringToFront()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4154BA91C8D0968E | Method | `public System.Void Close()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-033136B8C034B6DD | Method | `public System.Boolean Contains(System.Windows.Forms.Control ctl)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7EBA1AE006ACB48C | Method | `public System.Void CreateControl()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8E6E61D0E65E21EF | Method | `public System.Drawing.Graphics CreateGraphics()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-52C5EE5B665F7FDD | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-24920DEBC096DFEB | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-108141CE00964684 | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0221BFE53ED63029 | Method | `public System.Windows.Forms.DragDropEffects DoDragDropAsJson<!!0>(!!0 data, System.Windows.Forms.DragDropEffects allowedEffects) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FBF3EBDAA5F18496 | Method | `public System.Windows.Forms.DragDropEffects DoDragDropAsJson<!!0>(!!0 data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-319891EE2F74A743 | Method | `public System.Void DrawToBitmap(System.Drawing.Bitmap bitmap, System.Drawing.Rectangle targetBounds)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D097E0E7AF882920 | Method | `public System.Object EndInvoke(System.IAsyncResult asyncResult)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2F6D4CC9A6162460 | Method | `public System.Windows.Forms.Form FindForm()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8C62BE5B2CB6D076 | Method | `public System.Boolean Focus()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BEF54BA2F2D73AB0 | Method | `public static System.Windows.Forms.Control FromChildHandle(System.IntPtr handle)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-44BFAB118E939293 | Method | `public static System.Windows.Forms.Control FromHandle(System.IntPtr handle)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-288C117528C97848 | Method | `public static System.Drawing.SizeF GetAutoScaleSize(System.Drawing.Font font)` | `System.Windows.Forms.Form` ObsoleteAttribute("This method has been deprecated. Use the AutoScaleDimensions property instead.  https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-933BD1E3D71EA54D | Method | `public System.Windows.Forms.Control GetChildAtPoint(System.Drawing.Point pt)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7118C2D1385F675C | Method | `public System.Windows.Forms.Control GetChildAtPoint(System.Drawing.Point pt, System.Windows.Forms.GetChildAtPointSkip skipValue)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1BF49AFD3C414CB7 | Method | `public System.Windows.Forms.IContainerControl GetContainerControl()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DBDF5EBA5B396E7B | Method | `public System.Windows.Forms.Control GetNextControl(System.Windows.Forms.Control ctl, System.Boolean forward)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C7A801E73A48D637 | Method | `public virtual System.Drawing.Size GetPreferredSize(System.Drawing.Size proposedSize)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC1B38B61B37BC0B | Method | `public System.Void Hide()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-582C87F9AECB24E4 | Method | `public System.Void Invalidate()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5F8685F30B383D65 | Method | `public System.Void Invalidate(System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E595DF871CB8AC76 | Method | `public System.Void Invalidate(System.Drawing.Rectangle rc)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5333955159C90E87 | Method | `public System.Void Invalidate(System.Drawing.Rectangle rc, System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5971C62426DD0D85 | Method | `public System.Void Invalidate(System.Drawing.Region region)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D706C9420D7A2106 | Method | `public System.Void Invalidate(System.Drawing.Region region, System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1F3762D21CBA1A95 | Method | `public !!0 Invoke<!!0>(System.Func&#96;1<!!0> method) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D3CD07501115ECD5 | Method | `public System.Object Invoke(System.Delegate method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C5779E7CE4F182B8 | Method | `public System.Object Invoke(System.Delegate method, System.Object[] args)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BB8D551AB926E9F2 | Method | `public System.Void Invoke(System.Action method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D447F33F1FF8CFD6 | Method | `public System.Threading.Tasks.Task InvokeAsync(System.Action callback, System.Threading.CancellationToken cancellationToken = null)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C27149172FC1C879 | Method | `public System.Threading.Tasks.Task InvokeAsync(System.Func&#96;2<System.Threading.CancellationToken,System.Threading.Tasks.ValueTask> callback, System.Threading.CancellationToken cancellationToken = null)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4127AD70791459AB | Method | `public System.Threading.Tasks.Task&#96;1<!!0> InvokeAsync<!!0>(System.Func&#96;1<!!0> callback, System.Threading.CancellationToken cancellationToken = null) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FF1F7B2A35EF5BE6 | Method | `public System.Threading.Tasks.Task&#96;1<!!0> InvokeAsync<!!0>(System.Func&#96;2<System.Threading.CancellationToken,System.Threading.Tasks.ValueTask&#96;1<!!0>> callback, System.Threading.CancellationToken cancellationToken = null) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D8C00BBE3C48F144 | Method | `public static System.Boolean IsKeyLocked(System.Windows.Forms.Keys keyVal)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-801010F7088F26AA | Method | `public static System.Boolean IsMnemonic(System.Char charCode, System.String text)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CA21623E765F34B0 | Method | `public System.Void LayoutMdi(System.Windows.Forms.MdiLayout value)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-84740D54259D26C6 | Method | `public System.Drawing.Size LogicalToDeviceUnits(System.Drawing.Size value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D01F56F820E2B35F | Method | `public System.Int32 LogicalToDeviceUnits(System.Int32 value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BC005597AB28B2D7 | Method | `public System.Void PerformAutoScale()` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E4BB6916F88B838 | Method | `public System.Void PerformLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B94AA99F84DDB24 | Method | `public System.Void PerformLayout(System.Windows.Forms.Control affectedControl, System.String affectedProperty)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1428C0B89E3D88FD | Method | `public System.Drawing.Point PointToClient(System.Drawing.Point p)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4CFECFA0F5D8B1E6 | Method | `public System.Drawing.Point PointToScreen(System.Drawing.Point p)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2EF644A4DC7EFF33 | Method | `public System.Windows.Forms.PreProcessControlState PreProcessControlMessage(ref System.Windows.Forms.Message msg)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AAEB2537BB218277 | Method | `public virtual System.Boolean PreProcessMessage(ref System.Windows.Forms.Message msg)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-990FB6E1BD58384D | Method | `public System.Drawing.Rectangle RectangleToClient(System.Drawing.Rectangle r)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-809645BA23EEBE63 | Method | `public System.Drawing.Rectangle RectangleToScreen(System.Drawing.Rectangle r)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D06C21AA6AE37C87 | Method | `public virtual System.Void Refresh()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-94994A5F3807B719 | Method | `public System.Void RemoveOwnedForm(System.Windows.Forms.Form ownedForm)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7C79095BBB502200 | Method | `public virtual System.Void ResetBackColor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-59C9F6D36E0B41AB | Method | `public System.Void ResetBindings()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-59A37956D0427A5B | Method | `public virtual System.Void ResetCursor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6E0CF8571F98A14D | Method | `public virtual System.Void ResetFont()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C42798F5C3E73407 | Method | `public virtual System.Void ResetForeColor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C8B63DF4E7D722B1 | Method | `public System.Void ResetImeMode()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D07C9A6A2CC3DA44 | Method | `public virtual System.Void ResetRightToLeft()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-352BE7AC23ABABFE | Method | `public virtual System.Void ResetText()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-152C6742554C9885 | Method | `public System.Void ResumeLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-85ED003ACD920DF5 | Method | `public System.Void ResumeLayout(System.Boolean performLayout)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2667F0A00147692E | Method | `public System.Void Scale(System.Drawing.SizeF factor)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-025095B68C56B295 | Method | `public System.Void Scale(System.Single dx, System.Single dy)` | `System.Windows.Forms.Control` ObsoleteAttribute("This method has been deprecated. Use the Scale(SizeF ratio) method instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B9BCC1F76F4F1E11 | Method | `public System.Void Scale(System.Single ratio)` | `System.Windows.Forms.Control` ObsoleteAttribute("This method has been deprecated. Use the Scale(SizeF ratio) method instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4F68BD1BEA7C9286 | Method | `public System.Void ScaleBitmapLogicalToDevice(ref System.Drawing.Bitmap logicalBitmap)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-56E703FE7BD0592F | Method | `public System.Void ScrollControlIntoView(System.Windows.Forms.Control activeControl)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EAF009CAE4628B3C | Method | `public System.Void Select()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B092134ECE918755 | Method | `public System.Boolean SelectNextControl(System.Windows.Forms.Control ctl, System.Boolean forward, System.Boolean tabStopOnly, System.Boolean nested, System.Boolean wrap)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B39FF0586085E2DF | Method | `public System.Void SendToBack()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1A3C16C65D0E9015 | Method | `public System.Void SetAutoScrollMargin(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A0BAA8AE315CAB9E | Method | `public System.Void SetBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0B546837B699CDB | Method | `public System.Void SetBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E764DC588D18B1E | Method | `public System.Void SetDesktopBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AF6BAA4DA34EC3CC | Method | `public System.Void SetDesktopLocation(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CB71D63FC5E361A7 | Method | `public System.Void Show()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B03DC50A3214D922 | Method | `public System.Void Show(System.Windows.Forms.IWin32Window owner)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5F0234B1FF1E3D91 | Method | `public System.Threading.Tasks.Task ShowAsync(System.Windows.Forms.IWin32Window owner = null)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5F999D82F15FCE3B | Method | `public System.Windows.Forms.DialogResult ShowDialog()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-506A5ED6E9A86328 | Method | `public System.Windows.Forms.DialogResult ShowDialog(System.Windows.Forms.IWin32Window owner)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C34B81ADF9D57A8 | Method | `public System.Threading.Tasks.Task&#96;1<System.Windows.Forms.DialogResult> ShowDialogAsync()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5C80D71D35E13141 | Method | `public System.Threading.Tasks.Task&#96;1<System.Windows.Forms.DialogResult> ShowDialogAsync(System.Windows.Forms.IWin32Window owner)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F7B9A2FAB6BC9F5 | Method | `public System.Void SuspendLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5AC0E1B7464B4A46 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DB6C162E75E85161 | Method | `public System.Void Update()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F972F23CAE889055 | Method | `public System.Boolean Validate()` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A0A9A554CD3F5D6 | Method | `public System.Boolean Validate(System.Boolean checkAutoValidate)` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FBE80FAC6D731974 | Method | `public virtual System.Boolean ValidateChildren()` | `System.Windows.Forms.Form` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-599BDB60361CB0A4 | Method | `public virtual System.Boolean ValidateChildren(System.Windows.Forms.ValidationConstraints validationConstraints)` | `System.Windows.Forms.Form` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5BC2C7FEE456D727 | Property | `System.Windows.Forms.IButtonControl AcceptButton { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8D4DF5E9E96204D1 | Property | `System.Windows.Forms.AccessibleObject AccessibilityObject { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-569564FE694E07F6 | Property | `System.String AccessibleDefaultActionDescription { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F068D554492AE91B | Property | `System.String AccessibleDescription { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-01587EC202B70533 | Property | `System.String AccessibleName { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F58457C744AE736A | Property | `System.Windows.Forms.AccessibleRole AccessibleRole { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(-1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-17CDEE396A150B74 | Property | `System.Windows.Forms.Control ActiveControl { public get; public set; }` | `System.Windows.Forms.ContainerControl` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6EEC90A39429AD89 | Property | `static System.Windows.Forms.Form ActiveForm { public get; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0AEC0CD48A6E2A05 | Property | `System.Windows.Forms.Form ActiveMdiChild { public get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1E7853A024FA4735 | Property | `virtual System.Boolean AllowDrop { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9CBFEBF02D75870D | Property | `System.Boolean AllowTransparency { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8C529EF9D527F4CC | Property | `virtual System.Windows.Forms.AnchorStyles Anchor { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true); DefaultValueAttribute(5) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-85C9E47608947FEA | Property | `System.Boolean AutoScale { public get; public set; }` | `System.Windows.Forms.Form` ObsoleteAttribute("This property has been deprecated. Use the AutoScaleMode property instead.  https://go.microsoft.com/fwlink/?linkid=14202"); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4BB7F27128739DD4 | Property | `virtual System.Drawing.Size AutoScaleBaseSize { public get; public set; }` | `System.Windows.Forms.Form` LocalizableAttribute(true); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B7FEDD7D13D47CEE | Property | `System.Drawing.SizeF AutoScaleDimensions { public get; public set; }` | `System.Windows.Forms.ContainerControl` LocalizableAttribute(true); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4B77100364C24109 | Property | `System.Windows.Forms.AutoScaleMode AutoScaleMode { public get; public set; }` | `System.Windows.Forms.ContainerControl` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-75AA468FD710BDBD | Property | `virtual System.Boolean AutoScroll { public get; public set; }` | `System.Windows.Forms.Form` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4823DCE331EB0612 | Property | `System.Drawing.Size AutoScrollMargin { public get; public set; }` | `System.Windows.Forms.ScrollableControl` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A84AF9322EBEAD89 | Property | `System.Drawing.Size AutoScrollMinSize { public get; public set; }` | `System.Windows.Forms.ScrollableControl` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-00AE9BFCD8DE0F48 | Property | `virtual System.Drawing.Point AutoScrollOffset { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DefaultValueAttribute(System.Drawing.Point,"0, 0") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5C97CAFD741605FB | Property | `System.Drawing.Point AutoScrollPosition { public get; public set; }` | `System.Windows.Forms.ScrollableControl` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B1D0C2BC4F2EC59 | Property | `virtual System.Boolean AutoSize { public get; public set; }` | `System.Windows.Forms.ThreadExceptionDialog` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BF003E65D7EEE243 | Property | `System.Windows.Forms.AutoSizeMode AutoSizeMode { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(true); DefaultValueAttribute(1); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-05F6D207904F158D | Property | `virtual System.Windows.Forms.AutoValidate AutoValidate { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4223FD1E2E8DC907 | Property | `virtual System.Drawing.Color BackColor { public get; public set; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6B250D8B03BDA5B9 | Property | `virtual System.Drawing.Image BackgroundImage { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-170C5C14370DEC26 | Property | `virtual System.Windows.Forms.ImageLayout BackgroundImageLayout { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(1); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9F446DCD4A058F37 | Property | `virtual System.Windows.Forms.BindingContext BindingContext { public get; public set; }` | `System.Windows.Forms.ContainerControl` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D3DC4C41B6C73703 | Property | `System.Int32 Bottom { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B03CE266FF9D5234 | Property | `System.Drawing.Rectangle Bounds { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-545C5BD90B212F5A | Property | `System.Windows.Forms.IButtonControl CancelButton { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FD870D5F7656AA90 | Property | `System.Boolean CanFocus { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-34800B95E210C99D | Property | `System.Boolean CanSelect { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-93A9EFE72E01C81A | Property | `System.Boolean Capture { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BA7D41B5B55E8972 | Property | `System.Boolean CausesValidation { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2D0313ECC5B90EEA | Property | `static System.Boolean CheckForIllegalCrossThreadCalls { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-455548F0B69AECA9 | Property | `System.Drawing.Rectangle ClientRectangle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-89C3892E69E4E13D | Property | `System.Drawing.Size ClientSize { public get; public set; }` | `System.Windows.Forms.Form` LocalizableAttribute(true); DesignerSerializationVisibilityAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-91189CA86433A92B | Property | `System.String CompanyName { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B61BBF2631C9AB3A | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FFDE87D208F68FBD | Property | `System.Boolean ContainsFocus { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C8DF384FD93E6B54 | Property | `virtual System.Windows.Forms.ContextMenu ContextMenu { public get; public set; }` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-603C51D48E0CED5F | Property | `virtual System.Windows.Forms.ContextMenuStrip ContextMenuStrip { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ED7472F2A4F00925 | Property | `System.Boolean ControlBox { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-55ABE40FDD0EA4CA | Property | `System.Windows.Forms.Control+ControlCollection Controls { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-598F1D80EF4B83F3 | Property | `System.Boolean Created { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9F6FD10012106A1D | Property | `System.Drawing.SizeF CurrentAutoScaleDimensions { public get; }` | `System.Windows.Forms.ContainerControl` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FDCE21D0A7D093DA | Property | `virtual System.Windows.Forms.Cursor Cursor { public get; public set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8184512119F18FF3 | Property | `System.Windows.Forms.ControlBindingsCollection DataBindings { public get; }` | `System.Windows.Forms.Control` DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E60223965FB7E979 | Property | `virtual System.Object DataContext { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); BindableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4985069558A716AF | Property | `static System.Drawing.Color DefaultBackColor { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FE4A20DA8EAFEC63 | Property | `static System.Drawing.Font DefaultFont { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E1E29AF0EAAAAA7 | Property | `static System.Drawing.Color DefaultForeColor { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-188AB8FC001ECAD2 | Property | `System.Drawing.Rectangle DesktopBounds { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2F7FD021977422A8 | Property | `System.Drawing.Point DesktopLocation { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A628C43437923569 | Property | `System.Int32 DeviceDpi { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-929D1DC04035B0FB | Property | `System.Windows.Forms.DialogResult DialogResult { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6BA42CB0D6DEEE0B | Property | `virtual System.Drawing.Rectangle DisplayRectangle { public get; }` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F00043FA3BFC6A85 | Property | `System.Boolean Disposing { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B9F2F0A0D2E21CE2 | Property | `virtual System.Windows.Forms.DockStyle Dock { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true); DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9939E3190F6F2023 | Property | `System.Windows.Forms.ScrollableControl+DockPaddingEdges DockPadding { public get; }` | `System.Windows.Forms.ScrollableControl` DesignerSerializationVisibilityAttribute(0); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-34B06ED47D1E5626 | Property | `System.Boolean Enabled { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8CB7023AE43A9DFE | Property | `virtual System.Boolean Focused { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7B4E27D9E7E74EE3 | Property | `virtual System.Drawing.Font Font { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C1526CF04F82FE25 | Property | `virtual System.Drawing.Color ForeColor { public get; public set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-08FE14A699A17BFE | Property | `System.Drawing.Color FormBorderColor { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-42E0FD12E6C246FD | Property | `System.Windows.Forms.FormBorderStyle FormBorderStyle { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(4) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A275A198D7309583 | Property | `System.Drawing.Color FormCaptionBackColor { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3F1F6FAF97C684E5 | Property | `System.Drawing.Color FormCaptionTextColor { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9053F658D68BF26A | Property | `System.Windows.Forms.FormCornerPreference FormCornerPreference { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(0); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9BD96D18A0C079F8 | Property | `System.Windows.Forms.ScreenCaptureMode FormScreenCaptureMode { public get; public set; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DEB00D846132BAB1 | Property | `System.IntPtr Handle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C18F722E78F6D91 | Property | `System.Boolean HasChildren { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C5BE96D67888796E | Property | `System.Int32 Height { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0834C17FFD7BF1D7 | Property | `System.Boolean HelpButton { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-669DA0FF98D6F0A7 | Property | `System.Windows.Forms.HScrollProperties HorizontalScroll { public get; }` | `System.Windows.Forms.ScrollableControl` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5C522C5BEF651FB8 | Property | `System.Drawing.Icon Icon { public get; public set; }` | `System.Windows.Forms.Form` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DF81DADDAED0B408 | Property | `System.Windows.Forms.ImeMode ImeMode { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DF37616F7EEE6218 | Property | `System.Boolean InvokeRequired { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E2FBA79BBEAFD369 | Property | `System.Boolean IsAccessible { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D9FCCFD0A5774442 | Property | `System.Boolean IsAncestorSiteInDesignMode { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-275F435153D2D7F2 | Property | `System.Boolean IsDisposed { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-630059A12454235C | Property | `System.Boolean IsHandleCreated { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-52F08AEC3FA43C82 | Property | `System.Boolean IsMdiChild { public get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-26C4436635133768 | Property | `System.Boolean IsMdiContainer { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D3A7525D9D04C8CC | Property | `System.Boolean IsMirrored { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4DDC1D68FAE6284E | Property | `System.Boolean IsRestrictedWindow { public get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C13E3A15C1C2AC2E | Property | `System.Boolean KeyPreview { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C83C07D5A3F1B796 | Property | `virtual System.Windows.Forms.Layout.LayoutEngine LayoutEngine { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC6FDCEC3F5CEBC1 | Property | `System.Int32 Left { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-10E1F75C5B4FD9E9 | Property | `System.Drawing.Point Location { public get; public set; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-48C2C1E970DBEE31 | Property | `System.Windows.Forms.MenuStrip MainMenuStrip { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.ReferenceConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C7373CC1ABFF78E8 | Property | `System.Windows.Forms.Padding Margin { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC5E9D9826F84A31 | Property | `System.Boolean MaximizeBox { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BC3B67DFC6B25D05 | Property | `virtual System.Drawing.Size MaximumSize { public get; public set; }` | `System.Windows.Forms.Form` LocalizableAttribute(true); DefaultValueAttribute(System.Drawing.Size,"0, 0") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-52ABA16D3F62F8AF | Property | `System.Windows.Forms.Form[] MdiChildren { public get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-24D94BF72AAE44D3 | Property | `System.Boolean MdiChildrenMinimizedAnchorBottom { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D0681B8E17A5291 | Property | `System.Windows.Forms.Form MdiParent { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A081BEFCADA7AF6E | Property | `System.Windows.Forms.MainMenu Menu { public get; public set; }` | `System.Windows.Forms.Form` ObsoleteAttribute("\u0060MainMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060MenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-06B652B2D95FCEDC | Property | `System.Windows.Forms.MainMenu MergedMenu { public get; }` | `System.Windows.Forms.Form` ObsoleteAttribute("\u0060MainMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060MenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-14FC2647BE02BEDA | Property | `System.Boolean MinimizeBox { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C4D014FF9644384A | Property | `virtual System.Drawing.Size MinimumSize { public get; public set; }` | `System.Windows.Forms.Form` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4CF434AB3771D427 | Property | `System.Boolean Modal { public get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B0E140D45F0EC6B3 | Property | `static System.Windows.Forms.Keys ModifierKeys { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4953A33CFE5A3984 | Property | `static System.Windows.Forms.MouseButtons MouseButtons { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E7EEAAC1CC4F65E9 | Property | `static System.Drawing.Point MousePosition { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B68814675F6731F5 | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C22A8B2F14443F22 | Property | `System.Double Opacity { public get; public set; }` | `System.Windows.Forms.Form` TypeConverterAttribute(System.Windows.Forms.OpacityConverter); DefaultValueAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-771DBA2A2A40AE34 | Property | `System.Windows.Forms.Form[] OwnedForms { public get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BBBF1A11DBA50273 | Property | `System.Windows.Forms.Form Owner { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F59395F279DED370 | Property | `System.Windows.Forms.Padding Padding { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1C950A09DF8CA028 | Property | `System.Windows.Forms.Control Parent { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AF090A631C7228CB | Property | `System.Windows.Forms.Form ParentForm { public get; }` | `System.Windows.Forms.ContainerControl` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6B52B93B6ED18949 | Property | `System.Drawing.Size PreferredSize { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4298DC43C2A63D5E | Property | `System.String ProductName { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A598021A36361CFD | Property | `System.String ProductVersion { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-28F803DE8894B574 | Property | `System.Boolean RecreatingHandle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6521D59A635DB465 | Property | `System.Drawing.Region Region { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A03AE688A1F0454 | Property | `System.Drawing.Rectangle RestoreBounds { public get; }` | `System.Windows.Forms.Form` DesignerSerializationVisibilityAttribute(0); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2CE5C975577C930A | Property | `System.Int32 Right { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E756F8C6349D745 | Property | `virtual System.Windows.Forms.RightToLeft RightToLeft { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A6D8A530A8883515 | Property | `virtual System.Boolean RightToLeftLayout { public get; public set; }` | `System.Windows.Forms.Form` LocalizableAttribute(true); DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FF0FD5C093D6B646 | Property | `System.Boolean ShowIcon { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4B481494945D26FC | Property | `System.Boolean ShowInTaskbar { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0ACA6E034198C21 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A63C8676CE3420C4 | Property | `System.Drawing.Size Size { public get; public set; }` | `System.Windows.Forms.Form` DesignerSerializationVisibilityAttribute(0); LocalizableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8FBAA8F5EA99F6ED | Property | `System.Windows.Forms.SizeGripStyle SizeGripStyle { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8F9FF1B776A32F00 | Property | `System.Windows.Forms.FormStartPosition StartPosition { public get; public set; }` | `System.Windows.Forms.Form` LocalizableAttribute(true); DefaultValueAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-85A22EA5D3842A1B | Property | `System.Int32 TabIndex { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A3CCFB1E58BF5F29 | Property | `System.Boolean TabStop { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(true); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-11CBFD8982932096 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D763ECACB929BBFF | Property | `virtual System.String Text { public get; public set; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DBEE8991789FEB4C | Property | `System.Int32 Top { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5CF3712B24E801FC | Property | `System.Boolean TopLevel { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3F58E02096D9160F | Property | `System.Windows.Forms.Control TopLevelControl { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-395D62420D27A35E | Property | `System.Boolean TopMost { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ECEC91DD9E1B1760 | Property | `System.Drawing.Color TransparencyKey { public get; public set; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-54E347589F931CA7 | Property | `System.Boolean UseWaitCursor { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(false); BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-61BEF1CB707E743B | Property | `System.Windows.Forms.VScrollProperties VerticalScroll { public get; }` | `System.Windows.Forms.ScrollableControl` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8AF34EF8E1430732 | Property | `System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E1E6F755CC632973 | Property | `System.Int32 Width { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-74EF56395827598A | Property | `System.Windows.Forms.FormWindowState WindowState { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D292510263F4DF50 | Property | `System.Windows.Forms.IWindowTarget WindowTarget { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-C7CC40875C10A252 | Field | `protected const System.Int32 ScrollStateAutoScrolling = 1` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-97B785B2B77E5D56 | Field | `protected const System.Int32 ScrollStateFullDrag = 16` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-96AA3F31A2164A90 | Field | `protected const System.Int32 ScrollStateHScrollVisible = 2` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-66BE690489344F85 | Field | `protected const System.Int32 ScrollStateUserHasScrolled = 8` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B4AE260B3B0F4B4 | Field | `protected const System.Int32 ScrollStateVScrollVisible = 4` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-48B7AF7BC7E0C86A | Method | `protected System.Void AccessibilityNotifyClients(System.Windows.Forms.AccessibleEvents accEvent, System.Int32 objectID, System.Int32 childID)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-375B445EB5716C53 | Method | `protected internal System.Void AccessibilityNotifyClients(System.Windows.Forms.AccessibleEvents accEvent, System.Int32 childID)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E73820393457B30 | Method | `protected System.Void ActivateMdiChild(System.Windows.Forms.Form form)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5F1655525A9BC49C | Method | `protected virtual System.Void AdjustFormScrollbars(System.Boolean displayScrollbars)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-53A529A5377A8C46 | Method | `protected System.Void ApplyAutoScaling()` | `System.Windows.Forms.Form` ObsoleteAttribute("This method has been deprecated. Use the ApplyAutoScaling method instead.  https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E3BAD381552328CF | Method | `protected System.Void CenterToParent()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A62C1431C83FC2DC | Method | `protected System.Void CenterToScreen()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-06AEBC57E2D20B4A | Method | `protected virtual System.Windows.Forms.AccessibleObject CreateAccessibilityInstance()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9AC5C46F3E3EC842 | Method | `protected virtual System.Windows.Forms.Control+ControlCollection CreateControlsInstance()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-67E7DC8314AF4B9E | Method | `protected virtual System.Void CreateHandle()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7FD8DC50D566C880 | Method | `protected virtual System.Void DefWndProc(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3E7CBEE4180A4165 | Method | `protected virtual System.Void DestroyHandle()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C85C20482CA66F3F | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-832B8D6B77DC3F38 | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9D14719E756070D3 | Method | `protected virtual System.Windows.Forms.AccessibleObject GetAccessibilityObjectById(System.Int32 objectId)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-43378B1289259936 | Method | `protected System.Windows.Forms.AutoSizeMode GetAutoSizeMode()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC68D3F416B05F1E | Method | `protected virtual System.Drawing.Rectangle GetScaledBounds(System.Drawing.Rectangle bounds, System.Drawing.SizeF factor, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1E0CB0EB6499928F | Method | `protected System.Boolean GetScrollState(System.Int32 bit)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C53583012EEC5D42 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BC915B3DC7DA94FB | Method | `protected System.Boolean GetStyle(System.Windows.Forms.ControlStyles flag)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4BE4ECCE4F58A90E | Method | `protected System.Boolean GetTopLevel()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-58F324002D48AC9C | Method | `protected virtual System.Void InitLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8F03D7990EAD0043 | Method | `protected System.Void InvokeGotFocus(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7A7F36D9ADA773DB | Method | `protected System.Void InvokeLostFocus(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29B0507948E6598C | Method | `protected System.Void InvokeOnClick(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FF2D96C3DAD0873C | Method | `protected System.Void InvokePaint(System.Windows.Forms.Control c, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-809408EA86FD7604 | Method | `protected System.Void InvokePaintBackground(System.Windows.Forms.Control c, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A73314061D4BD97C | Method | `protected virtual System.Boolean IsInputChar(System.Char charCode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0534AE19770948C7 | Method | `protected virtual System.Boolean IsInputKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8C5935E537347365 | Method | `protected virtual System.Void NotifyInvalidate(System.Drawing.Rectangle invalidatedArea)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8A7DC5F6BBDE5BD4 | Method | `protected virtual System.Void OnActivated(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1F33B1493F0F1BF2 | Method | `protected virtual System.Void OnAutoSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D44D0E6BDA679429 | Method | `protected virtual System.Void OnAutoValidateChanged(System.EventArgs e)` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C7B68ED613D57C14 | Method | `protected virtual System.Void OnBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-51A2D532A5670BBB | Method | `protected virtual System.Void OnBackgroundImageChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9BB8124FFE13CA3C | Method | `protected virtual System.Void OnBackgroundImageLayoutChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D6CBC1F35834FC2A | Method | `protected virtual System.Void OnBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-04B4E65FF66D6D79 | Method | `protected virtual System.Void OnCausesValidationChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E5D9AF45DEFF0905 | Method | `protected virtual System.Void OnChangeUICues(System.Windows.Forms.UICuesEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7A43D8940EC09148 | Method | `protected virtual System.Void OnClick(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2EA20C25EF073EEF | Method | `protected virtual System.Void OnClientSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F726C1C6FE46E015 | Method | `protected virtual System.Void OnClosed(System.EventArgs e)` | `System.Windows.Forms.Form` ObsoleteAttribute("Form.OnClosing, Form.OnClosed and the corresponding events are obsolete. Use Form.OnFormClosing, Form.OnFormClosed, Form.FormClosing and Form.FormClosed instead.",false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E8D3303195744FCC | Method | `protected virtual System.Void OnClosing(System.ComponentModel.CancelEventArgs e)` | `System.Windows.Forms.Form` ObsoleteAttribute("Form.OnClosing, Form.OnClosed and the corresponding events are obsolete. Use Form.OnFormClosing, Form.OnFormClosed, Form.FormClosing and Form.FormClosed instead.",false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5DB9EF0B6416C82D | Method | `protected virtual System.Void OnContextMenuChanged(System.EventArgs e)` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B4D2391A1675C450 | Method | `protected virtual System.Void OnContextMenuStripChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-15B95C2AAE2CA6C3 | Method | `protected virtual System.Void OnControlAdded(System.Windows.Forms.ControlEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-33D12BCD8A38BF9A | Method | `protected virtual System.Void OnControlRemoved(System.Windows.Forms.ControlEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A1B780DF5270705 | Method | `protected virtual System.Void OnCreateControl()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-793D20DAA9E28077 | Method | `protected virtual System.Void OnCursorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-62B96F264A634E3D | Method | `protected virtual System.Void OnDataContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-46F5D69B104784DC | Method | `protected virtual System.Void OnDeactivate(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E4140DB11F9FFD5 | Method | `protected virtual System.Void OnDockChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D437524DC717795B | Method | `protected virtual System.Void OnDoubleClick(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-14618762C286989C | Method | `protected virtual System.Void OnDpiChanged(System.Windows.Forms.DpiChangedEventArgs e)` | `System.Windows.Forms.Form` BrowsableAttribute(true); DesignerSerializationVisibilityAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9EA348D7BB080B2D | Method | `protected virtual System.Void OnDpiChangedAfterParent(System.EventArgs e)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B3D0FACF2323A987 | Method | `protected virtual System.Void OnDpiChangedBeforeParent(System.EventArgs e)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-70D1A0FEF09CE034 | Method | `protected virtual System.Void OnDragDrop(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC127D3F96C47ED8 | Method | `protected virtual System.Void OnDragEnter(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FDB042F2243B2070 | Method | `protected virtual System.Void OnDragLeave(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2DA9C7325CC41DEF | Method | `protected virtual System.Void OnDragOver(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-733CE84E5DF64167 | Method | `protected virtual System.Void OnEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1ED0D44A49625363 | Method | `protected internal virtual System.Void OnEnter(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4AF9A9BC79CF3253 | Method | `protected virtual System.Void OnFontChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-68E032BE65649BE1 | Method | `protected virtual System.Void OnForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3A122F9582CEEE1C | Method | `protected virtual System.Void OnFormBorderColorChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E3C58B10EE328DDB | Method | `protected virtual System.Void OnFormCaptionBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9BB31B7836EC5D12 | Method | `protected virtual System.Void OnFormCaptionTextColorChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6F1A8D1F6F92ED94 | Method | `protected virtual System.Void OnFormClosed(System.Windows.Forms.FormClosedEventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F1722B41D3580F8C | Method | `protected virtual System.Void OnFormClosing(System.Windows.Forms.FormClosingEventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C15DF1360ADBD221 | Method | `protected virtual System.Void OnFormCornerPreferenceChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-75D9709495CFE379 | Method | `protected virtual System.Boolean OnGetDpiScaledSize(System.Int32 deviceDpiOld, System.Int32 deviceDpiNew, ref System.Drawing.Size desiredSize)` | `System.Windows.Forms.Form` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3183C4C1E8AF3997 | Method | `protected virtual System.Void OnGiveFeedback(System.Windows.Forms.GiveFeedbackEventArgs gfbevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3ECDD7C5A2C931FB | Method | `protected virtual System.Void OnGotFocus(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CD7A0B8712EB545F | Method | `protected virtual System.Void OnHandleCreated(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-12D80DFBCA74C71F | Method | `protected virtual System.Void OnHandleDestroyed(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-19AEA41084AC6C43 | Method | `protected virtual System.Void OnHelpButtonClicked(System.ComponentModel.CancelEventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DDFDB18BB1551614 | Method | `protected virtual System.Void OnHelpRequested(System.Windows.Forms.HelpEventArgs hevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-064B65A00E4B448C | Method | `protected virtual System.Void OnImeModeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DB87A313AC38050F | Method | `protected virtual System.Void OnInputLanguageChanged(System.Windows.Forms.InputLanguageChangedEventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DDA6577650A21F77 | Method | `protected virtual System.Void OnInputLanguageChanging(System.Windows.Forms.InputLanguageChangingEventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B3D795F9EE766A3D | Method | `protected virtual System.Void OnInvalidated(System.Windows.Forms.InvalidateEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9035B88042A3CEBF | Method | `protected virtual System.Void OnKeyDown(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-344EBBA201691946 | Method | `protected virtual System.Void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7BA73636C6815EDC | Method | `protected virtual System.Void OnKeyUp(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6C5E2CB48B273E0F | Method | `protected virtual System.Void OnLayout(System.Windows.Forms.LayoutEventArgs levent)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C243233C8E908C8E | Method | `protected internal virtual System.Void OnLeave(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-471AFEC7644AEFAE | Method | `protected virtual System.Void OnLoad(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A3C3B1EBA4FBC23A | Method | `protected virtual System.Void OnLocationChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F2E5B256B0837F2D | Method | `protected virtual System.Void OnLostFocus(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-196ECBBFAB2D14BB | Method | `protected virtual System.Void OnMarginChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7B6A806EBDF93828 | Method | `protected virtual System.Void OnMaximizedBoundsChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-964083756D6F1186 | Method | `protected virtual System.Void OnMaximumSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A6F2B0D53EAAC396 | Method | `protected virtual System.Void OnMdiChildActivate(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1FB1FD03A3844DCD | Method | `protected virtual System.Void OnMenuComplete(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2DE8A8826A2FB5A2 | Method | `protected virtual System.Void OnMenuStart(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-03E0A1B823AC9525 | Method | `protected virtual System.Void OnMinimumSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7E8D94FA777D15F4 | Method | `protected virtual System.Void OnMouseCaptureChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16F7776D9A96F5DA | Method | `protected virtual System.Void OnMouseClick(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-82BC5352CB8BF3FF | Method | `protected virtual System.Void OnMouseDoubleClick(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-65F2D21500422B5C | Method | `protected virtual System.Void OnMouseDown(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-80F1C2BE53341A3C | Method | `protected virtual System.Void OnMouseEnter(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-25998CB7F3F4EEE6 | Method | `protected virtual System.Void OnMouseHover(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E26A19A91552D2C9 | Method | `protected virtual System.Void OnMouseLeave(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3441DEF0BE0F3028 | Method | `protected virtual System.Void OnMouseMove(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F31BF51670590719 | Method | `protected virtual System.Void OnMouseUp(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A4D393C72188870B | Method | `protected virtual System.Void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-88515A99B4A80772 | Method | `protected virtual System.Void OnMove(System.EventArgs e)` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B2C26D93257E979 | Method | `protected virtual System.Void OnNotifyMessage(System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-898B03B3E8CB3293 | Method | `protected virtual System.Void OnPaddingChanged(System.EventArgs e)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FCC1141B0CCAABB5 | Method | `protected virtual System.Void OnPaint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-26722A77FDBFBDD7 | Method | `protected virtual System.Void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FE6819F0BC5626FD | Method | `protected virtual System.Void OnParentBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A68C94169554A8CD | Method | `protected virtual System.Void OnParentBackgroundImageChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B3601B29E2F9B26 | Method | `protected virtual System.Void OnParentBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E46959DEC5532C2B | Method | `protected virtual System.Void OnParentChanged(System.EventArgs e)` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9DC5A0C087DA1ADB | Method | `protected virtual System.Void OnParentCursorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1AEA22F9DDE7E89F | Method | `protected virtual System.Void OnParentDataContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C591C8D699B08481 | Method | `protected virtual System.Void OnParentEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A5AC45AE197B2DE8 | Method | `protected virtual System.Void OnParentFontChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E77A76D82F207E63 | Method | `protected virtual System.Void OnParentForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-352E67D0BDA5BB62 | Method | `protected virtual System.Void OnParentRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1123CB3F7717F99D | Method | `protected virtual System.Void OnParentVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D6FA70D2385289B2 | Method | `protected virtual System.Void OnPreviewKeyDown(System.Windows.Forms.PreviewKeyDownEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D728045EFB32D00A | Method | `protected virtual System.Void OnPrint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BD727437EC417CD0 | Method | `protected virtual System.Void OnQueryContinueDrag(System.Windows.Forms.QueryContinueDragEventArgs qcdevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-68FC6BFADE12CB47 | Method | `protected virtual System.Void OnRegionChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D97A261F4FCBCD84 | Method | `protected virtual System.Void OnResize(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A33790E3E922492D | Method | `protected virtual System.Void OnResizeBegin(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-63946B012657B43C | Method | `protected virtual System.Void OnResizeEnd(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-84373B1304B81131 | Method | `protected virtual System.Void OnRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E350CFC902F84D78 | Method | `protected virtual System.Void OnRightToLeftLayoutChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2106427AAC717FDF | Method | `protected virtual System.Void OnScroll(System.Windows.Forms.ScrollEventArgs se)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-54C29803E0345940 | Method | `protected virtual System.Void OnShown(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-27B8B77F65A36026 | Method | `protected virtual System.Void OnSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-81B2E00B67FC597B | Method | `protected virtual System.Void OnStyleChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-111E6F69AEF13B66 | Method | `protected virtual System.Void OnSystemColorsChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-79D9059679E837F9 | Method | `protected virtual System.Void OnTabIndexChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BC65D935DCDB0ECA | Method | `protected virtual System.Void OnTabStopChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B3FC6214C1ADE0D | Method | `protected virtual System.Void OnTextChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7EF1C59BE899719D | Method | `protected virtual System.Void OnValidated(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9DEC4E87F12FDBD8 | Method | `protected virtual System.Void OnValidating(System.ComponentModel.CancelEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2A8BC2E3D72A10C8 | Method | `protected virtual System.Void OnVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B56AF156FC802E95 | Method | `protected virtual System.Boolean ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-22A1DE71B4B1D139 | Method | `protected virtual System.Boolean ProcessDialogChar(System.Char charCode)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-54916BCB24B9D25D | Method | `protected virtual System.Boolean ProcessDialogKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E111A4E01BB8CDBB | Method | `protected virtual System.Boolean ProcessKeyEventArgs(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B7A8FF6E79755995 | Method | `protected internal virtual System.Boolean ProcessKeyMessage(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E1C2DD1FB17F1BE | Method | `protected virtual System.Boolean ProcessKeyPreview(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-69662B779C2250D4 | Method | `protected internal virtual System.Boolean ProcessMnemonic(System.Char charCode)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8E9D449F43A8DE4C | Method | `protected virtual System.Boolean ProcessTabKey(System.Boolean forward)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-33988F0CFEC9FC45 | Method | `protected System.Void RaiseDragEvent(System.Object key, System.Windows.Forms.DragEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2F89E0116A623C62 | Method | `protected System.Void RaiseKeyEvent(System.Object key, System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CE13D190CC0567EB | Method | `protected System.Void RaiseMouseEvent(System.Object key, System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-56CF91892EB6B869 | Method | `protected System.Void RaisePaintEvent(System.Object key, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50AE2960290BB311 | Method | `protected System.Void RecreateHandle()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A7DF09AEE745FDEA | Method | `protected static System.Boolean ReflectMessage(System.IntPtr hWnd, ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-94AD37CC1836BFE2 | Method | `protected virtual System.Void RescaleConstantsForDpi(System.Int32 deviceDpiOld, System.Int32 deviceDpiNew)` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16F3E53252EDC4B0 | Method | `protected System.Void ResetMouseEventArgs()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8B89AAB63D8F707E | Method | `protected System.Drawing.ContentAlignment RtlTranslateAlignment(System.Drawing.ContentAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7272825E4ADA23EC | Method | `protected System.Windows.Forms.HorizontalAlignment RtlTranslateAlignment(System.Windows.Forms.HorizontalAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9BA4ADC2622A686E | Method | `protected System.Windows.Forms.LeftRightAlignment RtlTranslateAlignment(System.Windows.Forms.LeftRightAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7827CD9D39ED7BB8 | Method | `protected internal System.Drawing.ContentAlignment RtlTranslateContent(System.Drawing.ContentAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-77912E7CC76F0BA6 | Method | `protected System.Windows.Forms.HorizontalAlignment RtlTranslateHorizontal(System.Windows.Forms.HorizontalAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-711BA6799C52BFAF | Method | `protected System.Windows.Forms.LeftRightAlignment RtlTranslateLeftRight(System.Windows.Forms.LeftRightAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-00E8865D960E834F | Method | `protected virtual System.Void ScaleControl(System.Drawing.SizeF factor, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-354B8CE969C144F8 | Method | `protected virtual System.Void ScaleCore(System.Single x, System.Single y)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3E49A74974B05976 | Method | `protected virtual System.Void ScaleMinMaxSize(System.Single xScaleFactor, System.Single yScaleFactor, System.Boolean updateContainerSize = true)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-80961F3E4452F3D6 | Method | `protected virtual System.Drawing.Point ScrollToControl(System.Windows.Forms.Control activeControl)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C2A5CD30E75075CF | Method | `protected virtual System.Void Select(System.Boolean directed, System.Boolean forward)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4633B21C0B1BEED8 | Method | `protected System.Void SetAutoSizeMode(System.Windows.Forms.AutoSizeMode mode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CE1DE1C5EE313E8C | Method | `protected virtual System.Void SetBoundsCore(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FBAB29AF6EA5C82E | Method | `protected virtual System.Void SetClientSizeCore(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-534B20D670FB96E3 | Method | `protected System.Void SetDisplayRectLocation(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A17BF9824BDC9764 | Method | `protected System.Void SetScrollState(System.Int32 bit, System.Boolean value)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-31EDD693853EE495 | Method | `protected System.Void SetStyle(System.Windows.Forms.ControlStyles flag, System.Boolean value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D5117F2139C37986 | Method | `protected System.Void SetTopLevel(System.Boolean value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4CE16B28B11A203F | Method | `protected virtual System.Void SetVisibleCore(System.Boolean value)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4F36A68AD8F5F67E | Method | `protected virtual System.Drawing.Size SizeFromClientSize(System.Drawing.Size clientSize)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-015BE2C50BA968D3 | Method | `protected System.Void UpdateBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F10F39E564FC44BA | Method | `protected System.Void UpdateBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Int32 clientWidth, System.Int32 clientHeight)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3F7677791DFA88EA | Method | `protected internal System.Void UpdateBounds()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CD832A2DDF327EC8 | Method | `protected virtual System.Void UpdateDefaultButton()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5BB8BA6816E51F46 | Method | `protected System.Void UpdateStyles()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E3FA3B16DE820A0 | Method | `protected System.Void UpdateZOrder()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-44EF3C2A91FF0B7E | Method | `protected virtual System.Void WndProc(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F3440BB52E0A4F98 | Property | `System.Drawing.SizeF AutoScaleFactor { protected get; }` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-965D0540A9D28B5B | Property | `virtual System.Boolean CanEnableIme { protected get; }` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B797126E78AC607A | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E1A69F75AF49761F | Property | `virtual System.Windows.Forms.CreateParams CreateParams { protected get; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9598D8DB7FB95831 | Property | `virtual System.Windows.Forms.Cursor DefaultCursor { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A51A8BCC130DB68D | Property | `virtual System.Windows.Forms.ImeMode DefaultImeMode { protected get; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A9059391C4C3F0E1 | Property | `virtual System.Windows.Forms.Padding DefaultMargin { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-24339E454F75C03E | Property | `virtual System.Drawing.Size DefaultMaximumSize { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E496201E926F16B0 | Property | `virtual System.Drawing.Size DefaultMinimumSize { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-634BC5588C068528 | Property | `virtual System.Windows.Forms.Padding DefaultPadding { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8054EC19CB9F05BA | Property | `virtual System.Drawing.Size DefaultSize { protected get; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2A976396D31ED17C | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-96267B7279C16913 | Property | `virtual System.Boolean DoubleBuffered { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C0984D8283E89D68 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-156E255091756FF5 | Property | `System.Int32 FontHeight { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0559D4B26E286CCA | Property | `System.Boolean HScroll { protected get; protected set; }` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F84ED10951310A02 | Property | `virtual System.Windows.Forms.ImeMode ImeModeBase { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EE78613831BE4A55 | Property | `System.Drawing.Rectangle MaximizedBounds { protected get; protected set; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1CCDAF7CF1630A6A | Property | `static System.Windows.Forms.ImeMode PropagatingImeMode { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1786149E76A8AC41 | Property | `System.Boolean RenderRightToLeft { protected internal get; }` | `System.Windows.Forms.Control` ObsoleteAttribute("This property has been deprecated. Please use RightToLeft instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CFDE5F7E4DE9E8A9 | Property | `System.Boolean ResizeRedraw { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C9BB80FCA74095A | Property | `virtual System.Boolean ScaleChildren { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D049A7EE7E713062 | Property | `virtual System.Boolean ShowFocusCues { protected internal get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-86334F6AFE7659E5 | Property | `virtual System.Boolean ShowKeyboardCues { protected internal get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-684411F354622706 | Property | `virtual System.Boolean ShowWithoutActivation { protected get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C95105836711DEC | Property | `System.Boolean VScroll { protected get; protected set; }` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
