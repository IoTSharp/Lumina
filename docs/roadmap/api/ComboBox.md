# ComboBox：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/ComboBox.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.ComboBox`；基类：`System.Windows.Forms.ListControl`。Lumina 同名类型：🟡 存在；基类及基线接口形状：⬜ 未匹配。

公开属性 **112**，公开事件 **88**，公开方法 **84**；受保护扩展成员 **192**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-3F10A7B4A21D110B | Constructor | `public ComboBox()` | `System.Windows.Forms.ComboBox`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-DDB9D21D4B1B4C23 | Event | `public event System.EventHandler AutoSizeChanged` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0204E1416C6D3352 | Event | `public event System.EventHandler BackColorChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CF1D70E8344C57EE | Event | `public event System.EventHandler BackgroundImageChanged` | `System.Windows.Forms.ComboBox` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B81DA7A5D8119E10 | Event | `public event System.EventHandler BackgroundImageLayoutChanged` | `System.Windows.Forms.ComboBox` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5F77844C99BD2987 | Event | `public event System.EventHandler BindingContextChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-930E515B2D738770 | Event | `public event System.EventHandler CausesValidationChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D5B3EB605E4F44E | Event | `public event System.Windows.Forms.UICuesEventHandler ChangeUICues` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-461031D29CF855B3 | Event | `public event System.EventHandler Click` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2AED280E582E2840 | Event | `public event System.EventHandler ClientSizeChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F9CAE51D76CA8F9A | Event | `public event System.EventHandler ContextMenuChanged` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-09267074A508DB9B | Event | `public event System.EventHandler ContextMenuStripChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F6897AD65D9BE41A | Event | `public event System.Windows.Forms.ControlEventHandler ControlAdded` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D3E78C9B00034843 | Event | `public event System.Windows.Forms.ControlEventHandler ControlRemoved` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-42B072118E5A120D | Event | `public event System.EventHandler CursorChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50B21AAC067D8D15 | Event | `public event System.EventHandler DataContextChanged` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EFACB9A790104DAC | Event | `public event System.EventHandler DataSourceChanged` | `System.Windows.Forms.ListControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F116C9D9E78A3E89 | Event | `public event System.EventHandler DisplayMemberChanged` | `System.Windows.Forms.ListControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-36174E116F6FB16C | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D72CFC60B5067A6A | Event | `public event System.EventHandler DockChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-65B6D33078A5F55A | Event | `public event System.EventHandler DoubleClick` | `System.Windows.Forms.ComboBox` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4AFBE4FCE3B3E55B | Event | `public event System.EventHandler DpiChangedAfterParent` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B4DA435E4DD61E9E | Event | `public event System.EventHandler DpiChangedBeforeParent` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-291F5A2F7E1CA74E | Event | `public event System.Windows.Forms.DragEventHandler DragDrop` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2BB5CB5402F76963 | Event | `public event System.Windows.Forms.DragEventHandler DragEnter` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6104F4DEA78802B3 | Event | `public event System.EventHandler DragLeave` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AEFCE5CB31521FE5 | Event | `public event System.Windows.Forms.DragEventHandler DragOver` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D991BC88FE3789D9 | Event | `public event System.Windows.Forms.DrawItemEventHandler DrawItem` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50042BD1E8915E3F | Event | `public event System.EventHandler DropDown` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-51E5CDA967D06719 | Event | `public event System.EventHandler DropDownClosed` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1BECE7E75212EA5C | Event | `public event System.EventHandler DropDownStyleChanged` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-962A9DE957C87824 | Event | `public event System.EventHandler EnabledChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E8BD0F6CCC3AFD9 | Event | `public event System.EventHandler Enter` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1129BE1F150EE042 | Event | `public event System.EventHandler FontChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8E4C1F81A6C939B0 | Event | `public event System.EventHandler ForeColorChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A283D9FEF3A01CC8 | Event | `public event System.Windows.Forms.ListControlConvertEventHandler Format` | `System.Windows.Forms.ListControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-07547426BB179830 | Event | `public event System.EventHandler FormatInfoChanged` | `System.Windows.Forms.ListControl` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-717AFBC823DC68C7 | Event | `public event System.EventHandler FormatStringChanged` | `System.Windows.Forms.ListControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-210370D7F4111146 | Event | `public event System.EventHandler FormattingEnabledChanged` | `System.Windows.Forms.ListControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-99EBC7BF1C6F860F | Event | `public event System.Windows.Forms.GiveFeedbackEventHandler GiveFeedback` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CEAD27957B07A74A | Event | `public event System.EventHandler GotFocus` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A3227442F5A7DC5 | Event | `public event System.EventHandler HandleCreated` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-376DE4723261E9B5 | Event | `public event System.EventHandler HandleDestroyed` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A88D130FA9A0716 | Event | `public event System.Windows.Forms.HelpEventHandler HelpRequested` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8C37DCB7C9298EA7 | Event | `public event System.EventHandler ImeModeChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6C30935918C4C9B3 | Event | `public event System.Windows.Forms.InvalidateEventHandler Invalidated` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A281F341DFA33F6B | Event | `public event System.Windows.Forms.KeyEventHandler KeyDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BD9538585CB5833A | Event | `public event System.Windows.Forms.KeyPressEventHandler KeyPress` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9D940DFFB8620A84 | Event | `public event System.Windows.Forms.KeyEventHandler KeyUp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-883C40E3826C9EF0 | Event | `public event System.Windows.Forms.LayoutEventHandler Layout` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-96A17DF0B16AAA19 | Event | `public event System.EventHandler Leave` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7BE392F264EF0AB7 | Event | `public event System.EventHandler LocationChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-41DC4E04D9001077 | Event | `public event System.EventHandler LostFocus` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AF44D5E88CF41BE8 | Event | `public event System.EventHandler MarginChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-225BAA3288D3F988 | Event | `public event System.Windows.Forms.MeasureItemEventHandler MeasureItem` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D331E6F8431B77C8 | Event | `public event System.EventHandler MouseCaptureChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F0DD4AF883FEC3EB | Event | `public event System.Windows.Forms.MouseEventHandler MouseClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8E35966EDEF38B4D | Event | `public event System.Windows.Forms.MouseEventHandler MouseDoubleClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-05303739E3841C55 | Event | `public event System.Windows.Forms.MouseEventHandler MouseDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D1A0F01C45B7DE53 | Event | `public event System.EventHandler MouseEnter` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC2EC7D28803D0AC | Event | `public event System.EventHandler MouseHover` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8C68BA5EFCCA3A1F | Event | `public event System.EventHandler MouseLeave` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E807AB37ABAC96DB | Event | `public event System.Windows.Forms.MouseEventHandler MouseMove` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4A83E6E92D75C75A | Event | `public event System.Windows.Forms.MouseEventHandler MouseUp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EF1631CF76A40890 | Event | `public event System.Windows.Forms.MouseEventHandler MouseWheel` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4776AA3020A5E9A7 | Event | `public event System.EventHandler Move` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E273C435B044FCD9 | Event | `public event System.EventHandler PaddingChanged` | `System.Windows.Forms.ComboBox` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AD61DCFCC70235B6 | Event | `public event System.Windows.Forms.PaintEventHandler Paint` | `System.Windows.Forms.ComboBox` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F39B14EC467F947 | Event | `public event System.EventHandler ParentChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BCADFC41ADCECBA8 | Event | `public event System.Windows.Forms.PreviewKeyDownEventHandler PreviewKeyDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4DE87413F5D66D99 | Event | `public event System.Windows.Forms.QueryAccessibilityHelpEventHandler QueryAccessibilityHelp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AF8410696E2C9D83 | Event | `public event System.Windows.Forms.QueryContinueDragEventHandler QueryContinueDrag` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F6187B943C7D5CB1 | Event | `public event System.EventHandler RegionChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F322D1FF17BF90E | Event | `public event System.EventHandler Resize` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D2F37F44A572A535 | Event | `public event System.EventHandler RightToLeftChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C29909466370BFBE | Event | `public event System.EventHandler SelectedIndexChanged` | `System.Windows.Forms.ComboBox`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2792A71E7EE94B2E | Event | `public event System.EventHandler SelectedValueChanged` | `System.Windows.Forms.ListControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-70C2FD4EDB354472 | Event | `public event System.EventHandler SelectionChangeCommitted` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7A48F4AE9339566A | Event | `public event System.EventHandler SizeChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1407E36EC34BBF28 | Event | `public event System.EventHandler StyleChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4AD8767842AD4919 | Event | `public event System.EventHandler SystemColorsChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D873DFB4E688516F | Event | `public event System.EventHandler TabIndexChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B7A252A41D890C2E | Event | `public event System.EventHandler TabStopChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BD3FC7BF3715AC26 | Event | `public event System.EventHandler TextChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-663E2A5B076EB32E | Event | `public event System.EventHandler TextUpdate` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F5B01265696B2213 | Event | `public event System.EventHandler Validated` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EF5E263C3151C147 | Event | `public event System.ComponentModel.CancelEventHandler Validating` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1019B7FF4353FFF4 | Event | `public event System.EventHandler ValueMemberChanged` | `System.Windows.Forms.ListControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50996C4F8D67DF56 | Event | `public event System.EventHandler VisibleChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-982B39435A7E2EEB | Method | `public System.IAsyncResult BeginInvoke(System.Action method)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A63DF4FBB0E743C1 | Method | `public System.IAsyncResult BeginInvoke(System.Delegate method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F7C00B91EB4B86D0 | Method | `public System.IAsyncResult BeginInvoke(System.Delegate method, System.Object[] args)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A47825CAA4A9D8C0 | Method | `public System.Void BeginUpdate()` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C3E30FA64CC4BFFC | Method | `public System.Void BringToFront()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1E3A9CBB168734EF | Method | `public System.Boolean Contains(System.Windows.Forms.Control ctl)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8163984B942C0162 | Method | `public System.Void CreateControl()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-87E2405580596873 | Method | `public System.Drawing.Graphics CreateGraphics()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0AECA142ED4F1730 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-55A4094CD3C6CB5B | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6A75D7DE5F967721 | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B86D03DE35D50DC0 | Method | `public System.Windows.Forms.DragDropEffects DoDragDropAsJson<!!0>(!!0 data, System.Windows.Forms.DragDropEffects allowedEffects) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E666A67F57E1AD60 | Method | `public System.Windows.Forms.DragDropEffects DoDragDropAsJson<!!0>(!!0 data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D052A25EB5770D32 | Method | `public System.Void DrawToBitmap(System.Drawing.Bitmap bitmap, System.Drawing.Rectangle targetBounds)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1E5AE0964A107F42 | Method | `public System.Object EndInvoke(System.IAsyncResult asyncResult)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1CFE9118C0CE60DC | Method | `public System.Void EndUpdate()` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8C3FEF1563A5D1F2 | Method | `public System.Windows.Forms.Form FindForm()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-208DD3A15DC1D652 | Method | `public System.Int32 FindString(System.String s)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8CCA22CB47792143 | Method | `public System.Int32 FindString(System.String s, System.Int32 startIndex)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9AB5EACE7A00313C | Method | `public System.Int32 FindStringExact(System.String s)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-46FCF72DC86A29A6 | Method | `public System.Int32 FindStringExact(System.String s, System.Int32 startIndex)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D214BCBF926E992D | Method | `public System.Boolean Focus()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-31004E2B55D71794 | Method | `public static System.Windows.Forms.Control FromChildHandle(System.IntPtr handle)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CB5BC2592D7BABCF | Method | `public static System.Windows.Forms.Control FromHandle(System.IntPtr handle)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4B1278324C28BF0A | Method | `public System.Windows.Forms.Control GetChildAtPoint(System.Drawing.Point pt)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7FED9406BFBAE812 | Method | `public System.Windows.Forms.Control GetChildAtPoint(System.Drawing.Point pt, System.Windows.Forms.GetChildAtPointSkip skipValue)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5ABB4494687341AB | Method | `public System.Windows.Forms.IContainerControl GetContainerControl()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8D9DE1F7340A2BE4 | Method | `public System.Int32 GetItemHeight(System.Int32 index)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0CB03661F8CE58D7 | Method | `public System.String GetItemText(System.Object item)` | `System.Windows.Forms.ListControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C4E676F6C075F5AF | Method | `public System.Windows.Forms.Control GetNextControl(System.Windows.Forms.Control ctl, System.Boolean forward)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BE19072C1B578447 | Method | `public virtual System.Drawing.Size GetPreferredSize(System.Drawing.Size proposedSize)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AE2DB942B043DAA5 | Method | `public System.Void Hide()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-233AA34A1481B544 | Method | `public System.Void Invalidate()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7D27FF42630937A4 | Method | `public System.Void Invalidate(System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C267DF346CEE6E68 | Method | `public System.Void Invalidate(System.Drawing.Rectangle rc)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BCAB939E49171C04 | Method | `public System.Void Invalidate(System.Drawing.Rectangle rc, System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A259BCC40D9FDF5A | Method | `public System.Void Invalidate(System.Drawing.Region region)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3B5C8801E4972005 | Method | `public System.Void Invalidate(System.Drawing.Region region, System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D462D7FC8D39EB50 | Method | `public !!0 Invoke<!!0>(System.Func&#96;1<!!0> method) where !!0: None;` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-AABDB2ACCE6B96D8 | Method | `public System.Object Invoke(System.Delegate method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29883205A8BAFAF3 | Method | `public System.Object Invoke(System.Delegate method, System.Object[] args)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7ECB7C83B6C35F16 | Method | `public System.Void Invoke(System.Action method)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CB97ED03B627138D | Method | `public System.Threading.Tasks.Task InvokeAsync(System.Action callback, System.Threading.CancellationToken cancellationToken = null)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-82B2FAAD6822BE48 | Method | `public System.Threading.Tasks.Task InvokeAsync(System.Func&#96;2<System.Threading.CancellationToken,System.Threading.Tasks.ValueTask> callback, System.Threading.CancellationToken cancellationToken = null)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7221FEB8AE78C1BD | Method | `public System.Threading.Tasks.Task&#96;1<!!0> InvokeAsync<!!0>(System.Func&#96;1<!!0> callback, System.Threading.CancellationToken cancellationToken = null) where !!0: None;` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E83B06642475E1FA | Method | `public System.Threading.Tasks.Task&#96;1<!!0> InvokeAsync<!!0>(System.Func&#96;2<System.Threading.CancellationToken,System.Threading.Tasks.ValueTask&#96;1<!!0>> callback, System.Threading.CancellationToken cancellationToken = null) where !!0: None;` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7398F17B0CD36E77 | Method | `public static System.Boolean IsKeyLocked(System.Windows.Forms.Keys keyVal)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DA67CBE0F0FA444A | Method | `public static System.Boolean IsMnemonic(System.Char charCode, System.String text)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6DED1A818A874017 | Method | `public System.Drawing.Size LogicalToDeviceUnits(System.Drawing.Size value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-77FB597799E2E06B | Method | `public System.Int32 LogicalToDeviceUnits(System.Int32 value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8A4BE31CE5A84F29 | Method | `public System.Void PerformLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-31DA6B8C5EFCF4F1 | Method | `public System.Void PerformLayout(System.Windows.Forms.Control affectedControl, System.String affectedProperty)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6F925549C37B2FAC | Method | `public System.Drawing.Point PointToClient(System.Drawing.Point p)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DA8A03AEF2B608E8 | Method | `public System.Drawing.Point PointToScreen(System.Drawing.Point p)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5217E140297BF65B | Method | `public System.Windows.Forms.PreProcessControlState PreProcessControlMessage(ref System.Windows.Forms.Message msg)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E9DD2C2FFC8C295 | Method | `public virtual System.Boolean PreProcessMessage(ref System.Windows.Forms.Message msg)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ED62ECDC2BB77ADF | Method | `public System.Drawing.Rectangle RectangleToClient(System.Drawing.Rectangle r)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D7A3C96751256B14 | Method | `public System.Drawing.Rectangle RectangleToScreen(System.Drawing.Rectangle r)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-02C996ABB3BD04A0 | Method | `public virtual System.Void Refresh()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BAAA2C49C2E9AE36 | Method | `public virtual System.Void ResetBackColor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3BC9103B2DAD8D70 | Method | `public System.Void ResetBindings()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FFFF9C848A92C69D | Method | `public virtual System.Void ResetCursor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-456ED716B6788511 | Method | `public virtual System.Void ResetFont()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-98657628D91ECF98 | Method | `public virtual System.Void ResetForeColor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7FA50EC51711FD6D | Method | `public System.Void ResetImeMode()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5D2C695980999BDC | Method | `public virtual System.Void ResetRightToLeft()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CCF99340FAF8562C | Method | `public virtual System.Void ResetText()` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7475B7E29DBCC4C8 | Method | `public System.Void ResumeLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7EF621803AF51C83 | Method | `public System.Void ResumeLayout(System.Boolean performLayout)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-06C0DF2C67D5378C | Method | `public System.Void Scale(System.Drawing.SizeF factor)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3811326228E8D1FA | Method | `public System.Void Scale(System.Single dx, System.Single dy)` | `System.Windows.Forms.Control` ObsoleteAttribute("This method has been deprecated. Use the Scale(SizeF ratio) method instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-601C4B9BB6BE3C8D | Method | `public System.Void Scale(System.Single ratio)` | `System.Windows.Forms.Control` ObsoleteAttribute("This method has been deprecated. Use the Scale(SizeF ratio) method instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-282E4CD40C2F0B82 | Method | `public System.Void ScaleBitmapLogicalToDevice(ref System.Drawing.Bitmap logicalBitmap)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-41B6C5B45BE97742 | Method | `public System.Void Select()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F9C99B52C05787C | Method | `public System.Void Select(System.Int32 start, System.Int32 length)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3DAD5658A1CF1355 | Method | `public System.Void SelectAll()` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CC8E0B0337E6F019 | Method | `public System.Boolean SelectNextControl(System.Windows.Forms.Control ctl, System.Boolean forward, System.Boolean tabStopOnly, System.Boolean nested, System.Boolean wrap)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5FF9ABC530E83366 | Method | `public System.Void SendToBack()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1D13B2FFF905C716 | Method | `public System.Void SetBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C3A15765061A1C51 | Method | `public System.Void SetBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CE1B57F9EE3C68EE | Method | `public System.Void Show()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D1D2FBB823EF23C | Method | `public System.Void SuspendLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5B2BD1C700B6444F | Method | `public virtual System.String ToString()` | `System.Windows.Forms.ComboBox`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9E32365489143E59 | Method | `public System.Void Update()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3B8EFA8736201023 | Property | `System.Windows.Forms.AccessibleObject AccessibilityObject { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC84E492025C5721 | Property | `System.String AccessibleDefaultActionDescription { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C171A00F528B99D2 | Property | `System.String AccessibleDescription { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DE6E97057BD64B2B | Property | `System.String AccessibleName { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DC64B47819537196 | Property | `System.Windows.Forms.AccessibleRole AccessibleRole { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(-1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-91B7C52CF7A1825F | Property | `virtual System.Boolean AllowDrop { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6AC13B5CD78A77B0 | Property | `virtual System.Windows.Forms.AnchorStyles Anchor { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true); DefaultValueAttribute(5) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-011DF19A8CBC6790 | Property | `System.Windows.Forms.AutoCompleteStringCollection AutoCompleteCustomSource { public get; public set; }` | `System.Windows.Forms.ComboBox` DesignerSerializationVisibilityAttribute(2); LocalizableAttribute(true); EditorAttribute("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D5C1BC7B79B90F4C | Property | `System.Windows.Forms.AutoCompleteMode AutoCompleteMode { public get; public set; }` | `System.Windows.Forms.ComboBox` DefaultValueAttribute(0); BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-628271391FD78C8C | Property | `System.Windows.Forms.AutoCompleteSource AutoCompleteSource { public get; public set; }` | `System.Windows.Forms.ComboBox` DefaultValueAttribute(128); BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C905F58BDFD13E13 | Property | `virtual System.Drawing.Point AutoScrollOffset { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DefaultValueAttribute(System.Drawing.Point,"0, 0") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29F226B0EAB8E8B3 | Property | `virtual System.Boolean AutoSize { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true); DefaultValueAttribute(false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D5642DEA36B9357 | Property | `virtual System.Drawing.Color BackColor { public get; public set; }` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-335A9725636AC004 | Property | `virtual System.Drawing.Image BackgroundImage { public get; public set; }` | `System.Windows.Forms.ComboBox` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1FCACA84332FFDD1 | Property | `virtual System.Windows.Forms.ImageLayout BackgroundImageLayout { public get; public set; }` | `System.Windows.Forms.ComboBox` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E89797B10B4876B | Property | `virtual System.Windows.Forms.BindingContext BindingContext { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DD8F9EF8D2BF19D6 | Property | `System.Int32 Bottom { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1FAE2048119D6367 | Property | `System.Drawing.Rectangle Bounds { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2324AC53AD675D45 | Property | `System.Boolean CanFocus { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-887A7C95BBFA5A23 | Property | `System.Boolean CanSelect { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A8AD310A1B4CF2BC | Property | `System.Boolean Capture { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A36A6C41B1E75ACA | Property | `System.Boolean CausesValidation { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D11FDCE7DD04C49 | Property | `static System.Boolean CheckForIllegalCrossThreadCalls { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-383124E35F6F332F | Property | `System.Drawing.Rectangle ClientRectangle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E25B80DBE643A6C | Property | `System.Drawing.Size ClientSize { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3CC2CD03D128B6F9 | Property | `System.String CompanyName { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-27BDC7A28F255B79 | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-DEEFDDAE949BDAA9 | Property | `System.Boolean ContainsFocus { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0629509037C2227B | Property | `virtual System.Windows.Forms.ContextMenu ContextMenu { public get; public set; }` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50A7A2FAC004BE55 | Property | `virtual System.Windows.Forms.ContextMenuStrip ContextMenuStrip { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EFB5B593876EF74E | Property | `System.Windows.Forms.Control+ControlCollection Controls { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4BED663C5CC83499 | Property | `System.Boolean Created { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3CD8762617D57DA0 | Property | `virtual System.Windows.Forms.Cursor Cursor { public get; public set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-118255E442BA9509 | Property | `System.Windows.Forms.ControlBindingsCollection DataBindings { public get; }` | `System.Windows.Forms.Control` DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C3B28CE2E948BC1F | Property | `virtual System.Object DataContext { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); BindableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AFDE24D16DEA33E1 | Property | `System.Object DataSource { public get; public set; }` | `System.Windows.Forms.ComboBox` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5828E17C711EA2F0 | Property | `static System.Drawing.Color DefaultBackColor { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-90F25204AC7F3040 | Property | `static System.Drawing.Font DefaultFont { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2BF39A5BF83AC2B4 | Property | `static System.Drawing.Color DefaultForeColor { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D983FE9634DD60F | Property | `System.Int32 DeviceDpi { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3403CFB633ED1E64 | Property | `System.String DisplayMember { public get; public set; }` | `System.Windows.Forms.ListControl` DefaultValueAttribute(""); TypeConverterAttribute("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"); EditorAttribute("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D7325F51AEC3729A | Property | `virtual System.Drawing.Rectangle DisplayRectangle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-DEB90A158C257F91 | Property | `System.Boolean Disposing { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1080E3DDE59A1D32 | Property | `virtual System.Windows.Forms.DockStyle Dock { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true); DefaultValueAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B79F687E548D3B8D | Property | `System.Windows.Forms.DrawMode DrawMode { public get; public set; }` | `System.Windows.Forms.ComboBox` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1AA4B7BF629DCDE6 | Property | `System.Int32 DropDownHeight { public get; public set; }` | `System.Windows.Forms.ComboBox` BrowsableAttribute(true); DefaultValueAttribute(106) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9AD9513743712EB9 | Property | `System.Windows.Forms.ComboBoxStyle DropDownStyle { public get; public set; }` | `System.Windows.Forms.ComboBox` DefaultValueAttribute(1) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D947DBF85319B779 | Property | `System.Int32 DropDownWidth { public get; public set; }` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-936BD01A8C33A124 | Property | `System.Boolean DroppedDown { public get; public set; }` | `System.Windows.Forms.ComboBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D1F83C8BEB1D049 | Property | `System.Boolean Enabled { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4F7AA1E9702A5FAD | Property | `System.Windows.Forms.FlatStyle FlatStyle { public get; public set; }` | `System.Windows.Forms.ComboBox` DefaultValueAttribute(2); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EDF255BD57762AE2 | Property | `virtual System.Boolean Focused { public get; }` | `System.Windows.Forms.ComboBox`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4677D1E093861609 | Property | `virtual System.Drawing.Font Font { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DB75A2C259812565 | Property | `virtual System.Drawing.Color ForeColor { public get; public set; }` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-98FA4B54DFF72D59 | Property | `System.IFormatProvider FormatInfo { public get; public set; }` | `System.Windows.Forms.ListControl` BrowsableAttribute(false); DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-49D3A659DC2F1040 | Property | `System.String FormatString { public get; public set; }` | `System.Windows.Forms.ListControl` DefaultValueAttribute(""); EditorAttribute("System.Windows.Forms.Design.FormatStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0DD3549265BBDC3 | Property | `System.Boolean FormattingEnabled { public get; public set; }` | `System.Windows.Forms.ListControl` DefaultValueAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-AB547EB4B9D25A3A | Property | `System.IntPtr Handle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7AAE953AA4447B9E | Property | `System.Boolean HasChildren { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D10919F0E089541A | Property | `System.Int32 Height { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5A5664FFF836C45B | Property | `System.Windows.Forms.ImeMode ImeMode { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CA144978859A6F0B | Property | `System.Boolean IntegralHeight { public get; public set; }` | `System.Windows.Forms.ComboBox` DefaultValueAttribute(true); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CAF95D7BD0F9ADEB | Property | `System.Boolean InvokeRequired { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2F0330D4C3CD114A | Property | `System.Boolean IsAccessible { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9573BC70A46C4ADD | Property | `System.Boolean IsAncestorSiteInDesignMode { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5140788140B13CA2 | Property | `System.Boolean IsDisposed { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0215154A6CAE8A0B | Property | `System.Boolean IsHandleCreated { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B54DC9AC22ADFEC1 | Property | `System.Boolean IsMirrored { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-444434C3EDC99112 | Property | `System.Int32 ItemHeight { public get; public set; }` | `System.Windows.Forms.ComboBox` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-32BBE643113197A8 | Property | `System.Windows.Forms.ComboBox+ObjectCollection Items { public get; }` | `System.Windows.Forms.ComboBox` DesignerSerializationVisibilityAttribute(2); LocalizableAttribute(true); EditorAttribute("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E32A227B8F133B1 | Property | `virtual System.Windows.Forms.Layout.LayoutEngine LayoutEngine { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C39F606AC5F905B | Property | `System.Int32 Left { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0B41C81363C4911D | Property | `System.Drawing.Point Location { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3125C81A82BD4328 | Property | `System.Windows.Forms.Padding Margin { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6637041F23BD12FA | Property | `System.Int32 MaxDropDownItems { public get; public set; }` | `System.Windows.Forms.ComboBox` DefaultValueAttribute(8); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-62F4769AB106729C | Property | `virtual System.Drawing.Size MaximumSize { public get; public set; }` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FF3336EFE6FD3A3F | Property | `System.Int32 MaxLength { public get; public set; }` | `System.Windows.Forms.ComboBox` DefaultValueAttribute(0); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7444F971F86C771B | Property | `virtual System.Drawing.Size MinimumSize { public get; public set; }` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2526156BE5063A5A | Property | `static System.Windows.Forms.Keys ModifierKeys { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-02C1F8A3B1B55296 | Property | `static System.Windows.Forms.MouseButtons MouseButtons { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6121CDA0CFB476E6 | Property | `static System.Drawing.Point MousePosition { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FD3A46A7A1D91CB3 | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5C40BC507A7D17F5 | Property | `System.Windows.Forms.Padding Padding { public get; public set; }` | `System.Windows.Forms.ComboBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-8F79FD65CFA86746 | Property | `System.Windows.Forms.Control Parent { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-57F42B8ABEFC7A02 | Property | `System.Int32 PreferredHeight { public get; }` | `System.Windows.Forms.ComboBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5BDA83ED211C6930 | Property | `System.Drawing.Size PreferredSize { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D16D66747A24034F | Property | `System.String ProductName { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B397CC4550D8257A | Property | `System.String ProductVersion { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-841A7C3A626CCA68 | Property | `System.Boolean RecreatingHandle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-21E4FE870FCB2B33 | Property | `System.Drawing.Region Region { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C59D40230039CFC1 | Property | `System.Int32 Right { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7BE67A4238E82EC1 | Property | `virtual System.Windows.Forms.RightToLeft RightToLeft { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DC4B28DC827B01FB | Property | `virtual System.Int32 SelectedIndex { public get; public set; }` | `System.Windows.Forms.ComboBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7BCD80E968F84B2F | Property | `System.Object SelectedItem { public get; public set; }` | `System.Windows.Forms.ComboBox` BrowsableAttribute(false); BindableAttribute(true); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CCB9F897239A7BE8 | Property | `System.String SelectedText { public get; public set; }` | `System.Windows.Forms.ComboBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9BBE515B4F9AD05D | Property | `System.Object SelectedValue { public get; public set; }` | `System.Windows.Forms.ListControl` DefaultValueAttribute(null); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0); BindableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E208FA24B6792BE0 | Property | `System.Int32 SelectionLength { public get; public set; }` | `System.Windows.Forms.ComboBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B82EDFC565E5310 | Property | `System.Int32 SelectionStart { public get; public set; }` | `System.Windows.Forms.ComboBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1EADA7B08E367B99 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5E21AD719A987748 | Property | `System.Drawing.Size Size { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B42D89C2029E30BD | Property | `System.Boolean Sorted { public get; public set; }` | `System.Windows.Forms.ComboBox` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0970BA9BAC029B48 | Property | `System.Int32 TabIndex { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B69656EE225CDE57 | Property | `System.Boolean TabStop { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D5D569883982C199 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-EE51263DE599F92F | Property | `virtual System.String Text { public get; public set; }` | `System.Windows.Forms.ComboBox` LocalizableAttribute(true); BindableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FA20DF284265AEE7 | Property | `System.Int32 Top { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6D1BBF244A1CD074 | Property | `System.Windows.Forms.Control TopLevelControl { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3FB17CB14EB3D829 | Property | `System.Boolean UseWaitCursor { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(false); BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-91E86D971D9AB228 | Property | `System.String ValueMember { public get; public set; }` | `System.Windows.Forms.ListControl` DefaultValueAttribute(""); EditorAttribute("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-04FDBDEE7C8ECEDD | Property | `System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-BCA46AA25C94C58C | Property | `System.Int32 Width { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-48F8F1670A305864 | Property | `System.Windows.Forms.IWindowTarget WindowTarget { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-33FA88753EB9C4BE | Method | `protected System.Void AccessibilityNotifyClients(System.Windows.Forms.AccessibleEvents accEvent, System.Int32 objectID, System.Int32 childID)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC9F350CBC65B342 | Method | `protected internal System.Void AccessibilityNotifyClients(System.Windows.Forms.AccessibleEvents accEvent, System.Int32 childID)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-44F6527D3B14268D | Method | `protected virtual System.Void AddItemsCore(System.Object[] value)` | `System.Windows.Forms.ComboBox` ObsoleteAttribute("This method has been deprecated.  There is no replacement.  https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1EDC4AB2CC936BD1 | Method | `protected virtual System.Windows.Forms.AccessibleObject CreateAccessibilityInstance()` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F342D171F6566C6E | Method | `protected virtual System.Windows.Forms.Control+ControlCollection CreateControlsInstance()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C27D12A8A8360AC3 | Method | `protected virtual System.Void CreateHandle()` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6D0BD1FF16DD7A62 | Method | `protected virtual System.Void DefWndProc(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2BEB2E44EDC9216B | Method | `protected virtual System.Void DestroyHandle()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-53C097F50BCCBBA9 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.ComboBox`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-DC53AB987663FBE0 | Method | `protected System.Object FilterItemOnProperty(System.Object item)` | `System.Windows.Forms.ListControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C6A3BA8D6B7EB6F0 | Method | `protected System.Object FilterItemOnProperty(System.Object item, System.String field)` | `System.Windows.Forms.ListControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0BED725247474FC6 | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4B6DAAF37BC3139A | Method | `protected virtual System.Windows.Forms.AccessibleObject GetAccessibilityObjectById(System.Int32 objectId)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F927CCB6BE868268 | Method | `protected System.Windows.Forms.AutoSizeMode GetAutoSizeMode()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C3C18C0E949EA1F8 | Method | `protected virtual System.Drawing.Rectangle GetScaledBounds(System.Drawing.Rectangle bounds, System.Drawing.SizeF factor, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-884D5E1FF8317B59 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C50BAA0AA8E94A38 | Method | `protected System.Boolean GetStyle(System.Windows.Forms.ControlStyles flag)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3038985E0B7E4B01 | Method | `protected System.Boolean GetTopLevel()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2A2473A83158F78F | Method | `protected virtual System.Void InitLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-88B0EF7C07921550 | Method | `protected System.Void InvokeGotFocus(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C1E6F53706BFBEC8 | Method | `protected System.Void InvokeLostFocus(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-355202B357B40F4C | Method | `protected System.Void InvokeOnClick(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-23DABF4C41236575 | Method | `protected System.Void InvokePaint(System.Windows.Forms.Control c, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-55A91D9BAFBF42F0 | Method | `protected System.Void InvokePaintBackground(System.Windows.Forms.Control c, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3612B651E43834AB | Method | `protected virtual System.Boolean IsInputChar(System.Char charCode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F6A38A728E11EC0E | Method | `protected virtual System.Boolean IsInputKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CBDC99F41AE1715C | Method | `protected virtual System.Void NotifyInvalidate(System.Drawing.Rectangle invalidatedArea)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D3D54EC7BFB46A29 | Method | `protected virtual System.Void OnAutoSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-09E00EE591B70F26 | Method | `protected virtual System.Void OnBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.ComboBox`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-122CD20316A40555 | Method | `protected virtual System.Void OnBackgroundImageChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7E8B4F36041472C4 | Method | `protected virtual System.Void OnBackgroundImageLayoutChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5760BC40A3F79F91 | Method | `protected virtual System.Void OnBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.ListControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D41848C8CDCCCECB | Method | `protected virtual System.Void OnCausesValidationChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CC27E13E13045ACA | Method | `protected virtual System.Void OnChangeUICues(System.Windows.Forms.UICuesEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F9EDC181DA58A1DA | Method | `protected virtual System.Void OnClick(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B1965E4E76E27D50 | Method | `protected virtual System.Void OnClientSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-60A43644B6C89D9D | Method | `protected virtual System.Void OnContextMenuChanged(System.EventArgs e)` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1D4DEAFCB500AA29 | Method | `protected virtual System.Void OnContextMenuStripChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-03242434B34293AC | Method | `protected virtual System.Void OnControlAdded(System.Windows.Forms.ControlEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-574701A97140061D | Method | `protected virtual System.Void OnControlRemoved(System.Windows.Forms.ControlEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-80096B549737030C | Method | `protected virtual System.Void OnCreateControl()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8E5AC497187DCC62 | Method | `protected virtual System.Void OnCursorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6328CA34220E8D59 | Method | `protected virtual System.Void OnDataContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-86DEFCE914C39FCD | Method | `protected virtual System.Void OnDataSourceChanged(System.EventArgs e)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2DB911616C07BEAC | Method | `protected virtual System.Void OnDisplayMemberChanged(System.EventArgs e)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B474F4F9135860E8 | Method | `protected virtual System.Void OnDockChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D90F898E676F18C4 | Method | `protected virtual System.Void OnDoubleClick(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3A6B7A1E54BCD694 | Method | `protected virtual System.Void OnDpiChangedAfterParent(System.EventArgs e)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-10BAE6350B7FCB4F | Method | `protected virtual System.Void OnDpiChangedBeforeParent(System.EventArgs e)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3A86537E6F56DA31 | Method | `protected virtual System.Void OnDragDrop(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B048D13EA1F08961 | Method | `protected virtual System.Void OnDragEnter(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9CA95EC0BDAB3CEC | Method | `protected virtual System.Void OnDragLeave(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CA09B1C01F17223A | Method | `protected virtual System.Void OnDragOver(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-59EA4FC13F063884 | Method | `protected virtual System.Void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3C8845D022F8A54A | Method | `protected virtual System.Void OnDropDown(System.EventArgs e)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-807D8BFAD5A85332 | Method | `protected virtual System.Void OnDropDownClosed(System.EventArgs e)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C963546306D95907 | Method | `protected virtual System.Void OnDropDownStyleChanged(System.EventArgs e)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C505980A76E57E62 | Method | `protected virtual System.Void OnEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FB2F37BDC280F7BC | Method | `protected internal virtual System.Void OnEnter(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7E2C9538A639D24D | Method | `protected virtual System.Void OnFontChanged(System.EventArgs e)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-66EF8FB644322DC6 | Method | `protected virtual System.Void OnForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.ComboBox`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5EB950E04355A6E5 | Method | `protected virtual System.Void OnFormat(System.Windows.Forms.ListControlConvertEventArgs e)` | `System.Windows.Forms.ListControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9DB9F382B034E09E | Method | `protected virtual System.Void OnFormatInfoChanged(System.EventArgs e)` | `System.Windows.Forms.ListControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1757FC3B7F4E3967 | Method | `protected virtual System.Void OnFormatStringChanged(System.EventArgs e)` | `System.Windows.Forms.ListControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B93C6103D07F1125 | Method | `protected virtual System.Void OnFormattingEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.ListControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0FA18FB9C320DD26 | Method | `protected virtual System.Void OnGiveFeedback(System.Windows.Forms.GiveFeedbackEventArgs gfbevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D2051244431C13E3 | Method | `protected virtual System.Void OnGotFocus(System.EventArgs e)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AD588C09B6361A94 | Method | `protected virtual System.Void OnHandleCreated(System.EventArgs e)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B440B4400E2FA375 | Method | `protected virtual System.Void OnHandleDestroyed(System.EventArgs e)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B75FDBCA46035FCC | Method | `protected virtual System.Void OnHelpRequested(System.Windows.Forms.HelpEventArgs hevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E74AF1D43AAD0EC5 | Method | `protected virtual System.Void OnImeModeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A6D4B1FB72325E9F | Method | `protected virtual System.Void OnInvalidated(System.Windows.Forms.InvalidateEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-06D9EFDDE3CB6526 | Method | `protected virtual System.Void OnKeyDown(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C71210AB64F7B2B8 | Method | `protected virtual System.Void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3F83EF78CDD70E07 | Method | `protected virtual System.Void OnKeyUp(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BA2487D7FD4CD02B | Method | `protected virtual System.Void OnLayout(System.Windows.Forms.LayoutEventArgs levent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-055CC432B76C17A4 | Method | `protected internal virtual System.Void OnLeave(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3EC2E7990E30599E | Method | `protected virtual System.Void OnLocationChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E57AF14D98AD4B1 | Method | `protected virtual System.Void OnLostFocus(System.EventArgs e)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-157402E4DB819A5D | Method | `protected virtual System.Void OnMarginChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D04C42670A6572D8 | Method | `protected virtual System.Void OnMeasureItem(System.Windows.Forms.MeasureItemEventArgs e)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FDD6C6D62FC4353D | Method | `protected virtual System.Void OnMouseCaptureChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2226834BF0B677E3 | Method | `protected virtual System.Void OnMouseClick(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FDB971559C0424FD | Method | `protected virtual System.Void OnMouseDoubleClick(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-645E2B8675B61BD8 | Method | `protected virtual System.Void OnMouseDown(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-724543F38E01FCA8 | Method | `protected virtual System.Void OnMouseEnter(System.EventArgs e)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3E48312A1809A126 | Method | `protected virtual System.Void OnMouseHover(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EA88753E525342EB | Method | `protected virtual System.Void OnMouseLeave(System.EventArgs e)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-90D1358111BA597D | Method | `protected virtual System.Void OnMouseMove(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CF4D995F98CAAFEA | Method | `protected virtual System.Void OnMouseUp(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D98CD68B57D03D4 | Method | `protected virtual System.Void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B3D1DD3940A6A665 | Method | `protected virtual System.Void OnMove(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-94D1CACAE88F193D | Method | `protected virtual System.Void OnNotifyMessage(System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-90FF1CB6F1979F67 | Method | `protected virtual System.Void OnPaddingChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-58062D8FB28A9A5B | Method | `protected virtual System.Void OnPaint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C8C3D6BF902E11E9 | Method | `protected virtual System.Void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC8231497CA53924 | Method | `protected virtual System.Void OnParentBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4096837A917C872F | Method | `protected virtual System.Void OnParentBackgroundImageChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-15A45FFDA976696C | Method | `protected virtual System.Void OnParentBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E2BC68E13B8AE6A0 | Method | `protected virtual System.Void OnParentChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A1CC1D264171798A | Method | `protected virtual System.Void OnParentCursorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B95B2B41A3FA20FC | Method | `protected virtual System.Void OnParentDataContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CB7A6D30F4F73D71 | Method | `protected virtual System.Void OnParentEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EB1ED834A4595C88 | Method | `protected virtual System.Void OnParentFontChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C6332622F485CAC1 | Method | `protected virtual System.Void OnParentForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2C75193838041392 | Method | `protected virtual System.Void OnParentRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E8FDD2EB7CB8AA37 | Method | `protected virtual System.Void OnParentVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9954CDDC1CDAFE86 | Method | `protected virtual System.Void OnPreviewKeyDown(System.Windows.Forms.PreviewKeyDownEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9EDF01A5C07900A3 | Method | `protected virtual System.Void OnPrint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-73DC6AD9996DE881 | Method | `protected virtual System.Void OnQueryContinueDrag(System.Windows.Forms.QueryContinueDragEventArgs qcdevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-72F858EE26DA5BAF | Method | `protected virtual System.Void OnRegionChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FB7E33F6C8967FCD | Method | `protected virtual System.Void OnResize(System.EventArgs e)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1D1F3B0BEEA6CB4B | Method | `protected virtual System.Void OnRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-17B4D15D96EC2A93 | Method | `protected virtual System.Void OnSelectedIndexChanged(System.EventArgs e)` | `System.Windows.Forms.ComboBox`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7AC4024C4524F3E9 | Method | `protected virtual System.Void OnSelectedItemChanged(System.EventArgs e)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-26D09CBC0CF27409 | Method | `protected virtual System.Void OnSelectedValueChanged(System.EventArgs e)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B23AA363A1FC91C0 | Method | `protected virtual System.Void OnSelectionChangeCommitted(System.EventArgs e)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AB5BEAD61B0C9DB6 | Method | `protected virtual System.Void OnSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DEBBFE4DD77563E1 | Method | `protected virtual System.Void OnStyleChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BDAADD99EAC05F38 | Method | `protected virtual System.Void OnSystemColorsChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29F800BEC076723B | Method | `protected virtual System.Void OnTabIndexChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-075DAE5585B5D885 | Method | `protected virtual System.Void OnTabStopChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-55B6A0D6C28EB535 | Method | `protected virtual System.Void OnTextChanged(System.EventArgs e)` | `System.Windows.Forms.ComboBox`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E7454B52588A3EE0 | Method | `protected virtual System.Void OnTextUpdate(System.EventArgs e)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F6CCFDF12C2E97B6 | Method | `protected virtual System.Void OnValidated(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1567E4BBCF9DA8C2 | Method | `protected virtual System.Void OnValidating(System.ComponentModel.CancelEventArgs e)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6C6A846544F33A0F | Method | `protected virtual System.Void OnValueMemberChanged(System.EventArgs e)` | `System.Windows.Forms.ListControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6B9DF89D9479DEAC | Method | `protected virtual System.Void OnVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-14C059D60387066C | Method | `protected virtual System.Boolean ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5ADD2AAC3B242B94 | Method | `protected virtual System.Boolean ProcessDialogChar(System.Char charCode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B65C57E00041FD76 | Method | `protected virtual System.Boolean ProcessDialogKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B86BC567E79F9289 | Method | `protected virtual System.Boolean ProcessKeyEventArgs(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-740D1509D87088A3 | Method | `protected internal virtual System.Boolean ProcessKeyMessage(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-15183F8A68DBF4F5 | Method | `protected virtual System.Boolean ProcessKeyPreview(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2107423C2C2ACA0B | Method | `protected internal virtual System.Boolean ProcessMnemonic(System.Char charCode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-84207D9A50CADB68 | Method | `protected System.Void RaiseDragEvent(System.Object key, System.Windows.Forms.DragEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E2080FE4D51CBBEA | Method | `protected System.Void RaiseKeyEvent(System.Object key, System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-264A9ACB1F95E7DE | Method | `protected System.Void RaiseMouseEvent(System.Object key, System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E6D6FBC4DF05A85 | Method | `protected System.Void RaisePaintEvent(System.Object key, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-40906578CE40CA3B | Method | `protected System.Void RecreateHandle()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-174BD2034307018D | Method | `protected static System.Boolean ReflectMessage(System.IntPtr hWnd, ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-79716FD63EAFE6CF | Method | `protected virtual System.Void RefreshItem(System.Int32 index)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3826290665734C4B | Method | `protected virtual System.Void RefreshItems()` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4F33F8A08DD72C36 | Method | `protected virtual System.Void RescaleConstantsForDpi(System.Int32 deviceDpiOld, System.Int32 deviceDpiNew)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A81F8246FC0CFAC9 | Method | `protected System.Void ResetMouseEventArgs()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9929B4AB94E0B1CB | Method | `protected System.Drawing.ContentAlignment RtlTranslateAlignment(System.Drawing.ContentAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F86533C39F8A797D | Method | `protected System.Windows.Forms.HorizontalAlignment RtlTranslateAlignment(System.Windows.Forms.HorizontalAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5E84A51724EED3C9 | Method | `protected System.Windows.Forms.LeftRightAlignment RtlTranslateAlignment(System.Windows.Forms.LeftRightAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-14CB5DF964E4BB5A | Method | `protected internal System.Drawing.ContentAlignment RtlTranslateContent(System.Drawing.ContentAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2887C34841D5AE54 | Method | `protected System.Windows.Forms.HorizontalAlignment RtlTranslateHorizontal(System.Windows.Forms.HorizontalAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ED2C8A2B0FE24604 | Method | `protected System.Windows.Forms.LeftRightAlignment RtlTranslateLeftRight(System.Windows.Forms.LeftRightAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FB5F28193EA18BB4 | Method | `protected virtual System.Void ScaleControl(System.Drawing.SizeF factor, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-175F10AD3EEC4879 | Method | `protected virtual System.Void ScaleCore(System.Single dx, System.Single dy)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-006B34BDBCAB420D | Method | `protected virtual System.Void Select(System.Boolean directed, System.Boolean forward)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DC8F69FFE018C08D | Method | `protected System.Void SetAutoSizeMode(System.Windows.Forms.AutoSizeMode mode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-70A6D4BED1127212 | Method | `protected virtual System.Void SetBoundsCore(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.ComboBox`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-DA0969D44BDF2E5F | Method | `protected virtual System.Void SetClientSizeCore(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D1D9F92CCDEFF32A | Method | `protected virtual System.Void SetItemCore(System.Int32 index, System.Object value)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A4FFD03A587DA4DE | Method | `protected virtual System.Void SetItemsCore(System.Collections.IList value)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C6BFFBF4A1E7523F | Method | `protected System.Void SetStyle(System.Windows.Forms.ControlStyles flag, System.Boolean value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CD349B23476DFA1E | Method | `protected System.Void SetTopLevel(System.Boolean value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-684311294FF1AC06 | Method | `protected virtual System.Void SetVisibleCore(System.Boolean value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6F379D24D188E46C | Method | `protected virtual System.Drawing.Size SizeFromClientSize(System.Drawing.Size clientSize)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-81ED246B6EC4FEE7 | Method | `protected System.Void UpdateBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-76DCD47649E679B5 | Method | `protected System.Void UpdateBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Int32 clientWidth, System.Int32 clientHeight)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-503F9A0634FC3E3B | Method | `protected internal System.Void UpdateBounds()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-450C6A2A20652A76 | Method | `protected System.Void UpdateStyles()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-58220FF43D573F7B | Method | `protected System.Void UpdateZOrder()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EE73C972CD11E713 | Method | `protected virtual System.Void WndProc(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F3761BFD4C093FBA | Property | `virtual System.Boolean AllowSelection { protected get; }` | `System.Windows.Forms.ListControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4B919B89265E3A80 | Property | `virtual System.Boolean CanEnableIme { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E1971E0688C36F74 | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-61D82FD8422F03D3 | Property | `virtual System.Windows.Forms.CreateParams CreateParams { protected get; }` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B84296BF872EC8D5 | Property | `System.Windows.Forms.CurrencyManager DataManager { protected get; }` | `System.Windows.Forms.ListControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7654721FBC043BEB | Property | `virtual System.Windows.Forms.Cursor DefaultCursor { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-356F534A93FC0012 | Property | `virtual System.Windows.Forms.ImeMode DefaultImeMode { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A3B17BB89AB612D8 | Property | `virtual System.Windows.Forms.Padding DefaultMargin { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B4EFA0D5BEF5037B | Property | `virtual System.Drawing.Size DefaultMaximumSize { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-09F57DCB54C28A24 | Property | `virtual System.Drawing.Size DefaultMinimumSize { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-957793533FC7CBE9 | Property | `virtual System.Windows.Forms.Padding DefaultPadding { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D0CD0D2342817409 | Property | `virtual System.Drawing.Size DefaultSize { protected get; }` | `System.Windows.Forms.ComboBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-76DBACF522DBD636 | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-8B36B15718E0019F | Property | `virtual System.Boolean DoubleBuffered { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E055D3B1962BEB96 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9FA423DB697B1C00 | Property | `System.Int32 FontHeight { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-14E40737ECFC6E1A | Property | `virtual System.Windows.Forms.ImeMode ImeModeBase { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-40EA785C6B96B91A | Property | `static System.Windows.Forms.ImeMode PropagatingImeMode { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3FF2BBD4409CBAE5 | Property | `System.Boolean RenderRightToLeft { protected internal get; }` | `System.Windows.Forms.Control` ObsoleteAttribute("This property has been deprecated. Please use RightToLeft instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-02E9CF6D1645474A | Property | `System.Boolean ResizeRedraw { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A282EE795B96824 | Property | `virtual System.Boolean ScaleChildren { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-860CAF9C73DCAB90 | Property | `virtual System.Boolean ShowFocusCues { protected internal get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-144D437482651037 | Property | `virtual System.Boolean ShowKeyboardCues { protected internal get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
