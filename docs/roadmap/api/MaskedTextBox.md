# MaskedTextBox：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/MaskedTextBox.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.MaskedTextBox`；基类：`System.Windows.Forms.TextBoxBase`。Lumina 同名类型：🟡 存在；基类及基线接口形状：⬜ 未匹配。

公开属性 **123**，公开事件 **83**，公开方法 **92**；受保护扩展成员 **176**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-E20B97937B230ABC | Constructor | `public MaskedTextBox()` | `System.Windows.Forms.MaskedTextBox`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E3743B0F6C4EF5BF | Constructor | `public MaskedTextBox(System.ComponentModel.MaskedTextProvider maskedTextProvider)` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E71EB02DBD9614B1 | Constructor | `public MaskedTextBox(System.String mask)` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EDF8FDC56977EB86 | Event | `public event System.EventHandler AcceptsTabChanged` | `System.Windows.Forms.MaskedTextBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-783C5C98D89F0557 | Event | `public event System.EventHandler AutoSizeChanged` | `System.Windows.Forms.TextBoxBase` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D063363F74F5F9AA | Event | `public event System.EventHandler BackColorChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-15C55C683E098CBA | Event | `public event System.EventHandler BackgroundImageChanged` | `System.Windows.Forms.TextBoxBase` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-465BE591CAD4C28C | Event | `public event System.EventHandler BackgroundImageLayoutChanged` | `System.Windows.Forms.TextBoxBase` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3B82272270B0377A | Event | `public event System.EventHandler BindingContextChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-83F873049B480C09 | Event | `public event System.EventHandler BorderStyleChanged` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A540C756250313B9 | Event | `public event System.EventHandler CausesValidationChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BA1BFC8959D26EE1 | Event | `public event System.Windows.Forms.UICuesEventHandler ChangeUICues` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C226E6BC085B71B9 | Event | `public event System.EventHandler Click` | `System.Windows.Forms.TextBoxBase` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4F738347205F0247 | Event | `public event System.EventHandler ClientSizeChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CFC4667A6E252692 | Event | `public event System.EventHandler ContextMenuChanged` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1830575E7E6D89A9 | Event | `public event System.EventHandler ContextMenuStripChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6F34183FC0B9FF76 | Event | `public event System.Windows.Forms.ControlEventHandler ControlAdded` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-540E1311F34B1F62 | Event | `public event System.Windows.Forms.ControlEventHandler ControlRemoved` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-38C66C1802D6BADB | Event | `public event System.EventHandler CursorChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C749F5D5C3E5697 | Event | `public event System.EventHandler DataContextChanged` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8875DEE11FEAC660 | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7D1053171F07351F | Event | `public event System.EventHandler DockChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5C5F5D73445EC440 | Event | `public event System.EventHandler DoubleClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C8BF67AE3947329A | Event | `public event System.EventHandler DpiChangedAfterParent` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2192E47A5E9FA0AD | Event | `public event System.EventHandler DpiChangedBeforeParent` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-909BF60D80CC7BFA | Event | `public event System.Windows.Forms.DragEventHandler DragDrop` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1A06211436AE2288 | Event | `public event System.Windows.Forms.DragEventHandler DragEnter` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-81259D088DEB04CF | Event | `public event System.EventHandler DragLeave` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E790589FA8C00D7B | Event | `public event System.Windows.Forms.DragEventHandler DragOver` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A95751903A092944 | Event | `public event System.EventHandler EnabledChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-425945730AAF6A16 | Event | `public event System.EventHandler Enter` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E749AE4AF317CA01 | Event | `public event System.EventHandler FontChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-07992E55C8DE6A54 | Event | `public event System.EventHandler ForeColorChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C67CC5381CD2010A | Event | `public event System.Windows.Forms.GiveFeedbackEventHandler GiveFeedback` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-201661A3D13375FA | Event | `public event System.EventHandler GotFocus` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E76E8D00281D7434 | Event | `public event System.EventHandler HandleCreated` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9064E8C4E4A79C93 | Event | `public event System.EventHandler HandleDestroyed` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2F4287BDB23C6C46 | Event | `public event System.Windows.Forms.HelpEventHandler HelpRequested` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-55B44E271AE46489 | Event | `public event System.EventHandler HideSelectionChanged` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C8D21B69E824F8DB | Event | `public event System.EventHandler ImeModeChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16143D36DB537661 | Event | `public event System.Windows.Forms.InvalidateEventHandler Invalidated` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BE19983766EBDFA9 | Event | `public event System.EventHandler IsOverwriteModeChanged` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4236CDDDCE23C87E | Event | `public event System.Windows.Forms.KeyEventHandler KeyDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B2B02C25E2D628B6 | Event | `public event System.Windows.Forms.KeyPressEventHandler KeyPress` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EBF4E59C567652B0 | Event | `public event System.Windows.Forms.KeyEventHandler KeyUp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AD70BA8CE7CEA276 | Event | `public event System.Windows.Forms.LayoutEventHandler Layout` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DF930C261FBE5206 | Event | `public event System.EventHandler Leave` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2E23FE08774AD3E8 | Event | `public event System.EventHandler LocationChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3EEBA4AA9B27103B | Event | `public event System.EventHandler LostFocus` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DB23E2E67D514D85 | Event | `public event System.EventHandler MarginChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BA2845A3C688B832 | Event | `public event System.EventHandler MaskChanged` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8413481D29B4E0DD | Event | `public event System.Windows.Forms.MaskInputRejectedEventHandler MaskInputRejected` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-398BC72859C8AE8D | Event | `public event System.EventHandler ModifiedChanged` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-235A475EC20970F5 | Event | `public event System.EventHandler MouseCaptureChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-97A9B9986CD10D74 | Event | `public event System.Windows.Forms.MouseEventHandler MouseClick` | `System.Windows.Forms.TextBoxBase` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-13A07A140F53D35B | Event | `public event System.Windows.Forms.MouseEventHandler MouseDoubleClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8913B7EDA5CA400E | Event | `public event System.Windows.Forms.MouseEventHandler MouseDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FC0F5F7440C1E8E8 | Event | `public event System.EventHandler MouseEnter` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E875E124B6A27F67 | Event | `public event System.EventHandler MouseHover` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A612F462F2DAD7B6 | Event | `public event System.EventHandler MouseLeave` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1CBFB8E7F9374C7E | Event | `public event System.Windows.Forms.MouseEventHandler MouseMove` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16B0C934EB2F597E | Event | `public event System.Windows.Forms.MouseEventHandler MouseUp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A8D28C68975E3DEF | Event | `public event System.Windows.Forms.MouseEventHandler MouseWheel` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D77545867032F1AA | Event | `public event System.EventHandler Move` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-046D030E17396542 | Event | `public event System.EventHandler MultilineChanged` | `System.Windows.Forms.MaskedTextBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-664081BD56D848D9 | Event | `public event System.EventHandler PaddingChanged` | `System.Windows.Forms.TextBoxBase` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9081470E6E652C3B | Event | `public event System.Windows.Forms.PaintEventHandler Paint` | `System.Windows.Forms.TextBoxBase` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5E18CADA8EE5A659 | Event | `public event System.EventHandler ParentChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-784E4658B0ECDA56 | Event | `public event System.Windows.Forms.PreviewKeyDownEventHandler PreviewKeyDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D2D3251ED969459 | Event | `public event System.Windows.Forms.QueryAccessibilityHelpEventHandler QueryAccessibilityHelp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7163DBAAB1D12CBD | Event | `public event System.Windows.Forms.QueryContinueDragEventHandler QueryContinueDrag` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-969D2AB9E8CF9C23 | Event | `public event System.EventHandler ReadOnlyChanged` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B0AFDE01CF75FF3F | Event | `public event System.EventHandler RegionChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4C952E0C73572780 | Event | `public event System.EventHandler Resize` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4564F3AB45F9E7B4 | Event | `public event System.EventHandler RightToLeftChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D52B551AB0635572 | Event | `public event System.EventHandler SizeChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-557480BC87830E2D | Event | `public event System.EventHandler StyleChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3EEBFBF895325F3A | Event | `public event System.EventHandler SystemColorsChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-534A58AF220EF7E1 | Event | `public event System.EventHandler TabIndexChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7F6D57564A288D58 | Event | `public event System.EventHandler TabStopChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-48BAF9EB7F96B5B1 | Event | `public event System.EventHandler TextAlignChanged` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7426ED4E02998FA2 | Event | `public event System.EventHandler TextChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F99CE2D1EA56F997 | Event | `public event System.Windows.Forms.TypeValidationEventHandler TypeValidationCompleted` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A10332FFE70B32B1 | Event | `public event System.EventHandler Validated` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-068B2873657A81AE | Event | `public event System.ComponentModel.CancelEventHandler Validating` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AA46629462359545 | Event | `public event System.EventHandler VisibleChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-048E78D937DA388D | Method | `public System.Void AppendText(System.String text)` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-034CA50581D0E69E | Method | `public System.IAsyncResult BeginInvoke(System.Action method)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D37F187083697AFD | Method | `public System.IAsyncResult BeginInvoke(System.Delegate method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-75E6666771B114A2 | Method | `public System.IAsyncResult BeginInvoke(System.Delegate method, System.Object[] args)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CA2F801C2ACD2D89 | Method | `public System.Void BringToFront()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0D535BA507218ED | Method | `public System.Void Clear()` | `System.Windows.Forms.TextBoxBase`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-246E2F17C14CED6C | Method | `public System.Void ClearUndo()` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3FA7C7CB41A46FF9 | Method | `public System.Boolean Contains(System.Windows.Forms.Control ctl)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F7978CB7BD1593AF | Method | `public System.Void Copy()` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-54F1FA562F314DD4 | Method | `public System.Void CreateControl()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-98E697AC731D12BD | Method | `public System.Drawing.Graphics CreateGraphics()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-06402B631165D583 | Method | `public System.Void Cut()` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-179D5F6DDDCE857E | Method | `public System.Void DeselectAll()` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8A17E264B94B3E60 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7167B1F7A861265C | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C60361244EE8EE09 | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-999617050E7A1CA0 | Method | `public System.Windows.Forms.DragDropEffects DoDragDropAsJson<!!0>(!!0 data, System.Windows.Forms.DragDropEffects allowedEffects) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B203DF75D9A49DFE | Method | `public System.Windows.Forms.DragDropEffects DoDragDropAsJson<!!0>(!!0 data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9532E1B0AE12D5F0 | Method | `public System.Void DrawToBitmap(System.Drawing.Bitmap bitmap, System.Drawing.Rectangle targetBounds)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2645877BD1527C36 | Method | `public System.Object EndInvoke(System.IAsyncResult asyncResult)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-47D7083177033098 | Method | `public System.Windows.Forms.Form FindForm()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-473A1EEFCFD995CA | Method | `public System.Boolean Focus()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FA81915A67C01E64 | Method | `public static System.Windows.Forms.Control FromChildHandle(System.IntPtr handle)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B9B594BB9854619 | Method | `public static System.Windows.Forms.Control FromHandle(System.IntPtr handle)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F0B85C63F9787B38 | Method | `public virtual System.Char GetCharFromPosition(System.Drawing.Point pt)` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A0A188B82F90F422 | Method | `public virtual System.Int32 GetCharIndexFromPosition(System.Drawing.Point pt)` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2162B0DA6345A4BF | Method | `public System.Windows.Forms.Control GetChildAtPoint(System.Drawing.Point pt)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BF55B56B69767255 | Method | `public System.Windows.Forms.Control GetChildAtPoint(System.Drawing.Point pt, System.Windows.Forms.GetChildAtPointSkip skipValue)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FE40F6D814834A32 | Method | `public System.Windows.Forms.IContainerControl GetContainerControl()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D32225F1FC76D1F | Method | `public System.Int32 GetFirstCharIndexFromLine(System.Int32 lineNumber)` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-89783F96800B2282 | Method | `public System.Int32 GetFirstCharIndexOfCurrentLine()` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1D4006D57CF93E26 | Method | `public virtual System.Int32 GetLineFromCharIndex(System.Int32 index)` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D6E87D41305848B6 | Method | `public System.Windows.Forms.Control GetNextControl(System.Windows.Forms.Control ctl, System.Boolean forward)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC41626AF8267985 | Method | `public virtual System.Drawing.Point GetPositionFromCharIndex(System.Int32 index)` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5536863FE4A82F49 | Method | `public virtual System.Drawing.Size GetPreferredSize(System.Drawing.Size proposedSize)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-195929CD605A4CEE | Method | `public System.Void Hide()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6BB1720C7CB08195 | Method | `public System.Void Invalidate()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5B024F2B7B734C73 | Method | `public System.Void Invalidate(System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8BDEFDB0FF15550F | Method | `public System.Void Invalidate(System.Drawing.Rectangle rc)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A93E38C65489BC88 | Method | `public System.Void Invalidate(System.Drawing.Rectangle rc, System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-69F4B6DC309D2D20 | Method | `public System.Void Invalidate(System.Drawing.Region region)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D8C57D6C7E327A68 | Method | `public System.Void Invalidate(System.Drawing.Region region, System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-40F93DFEC2726F4A | Method | `public !!0 Invoke<!!0>(System.Func&#96;1<!!0> method) where !!0: None;` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-67E63AC5BDA97343 | Method | `public System.Object Invoke(System.Delegate method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-01B192CE2E884A6B | Method | `public System.Object Invoke(System.Delegate method, System.Object[] args)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-53F9ABB561777A04 | Method | `public System.Void Invoke(System.Action method)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-21AF95056C8DA29C | Method | `public System.Threading.Tasks.Task InvokeAsync(System.Action callback, System.Threading.CancellationToken cancellationToken = null)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6B7F7AE1E32DA1B3 | Method | `public System.Threading.Tasks.Task InvokeAsync(System.Func&#96;2<System.Threading.CancellationToken,System.Threading.Tasks.ValueTask> callback, System.Threading.CancellationToken cancellationToken = null)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-8A1FA64E51E39CCC | Method | `public System.Threading.Tasks.Task&#96;1<!!0> InvokeAsync<!!0>(System.Func&#96;1<!!0> callback, System.Threading.CancellationToken cancellationToken = null) where !!0: None;` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B5B6507716D8CF55 | Method | `public System.Threading.Tasks.Task&#96;1<!!0> InvokeAsync<!!0>(System.Func&#96;2<System.Threading.CancellationToken,System.Threading.Tasks.ValueTask&#96;1<!!0>> callback, System.Threading.CancellationToken cancellationToken = null) where !!0: None;` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0D836D73EDE2B9CC | Method | `public static System.Boolean IsKeyLocked(System.Windows.Forms.Keys keyVal)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-003A2B630F1ADF1B | Method | `public static System.Boolean IsMnemonic(System.Char charCode, System.String text)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F52C97A2DD39F33F | Method | `public System.Drawing.Size LogicalToDeviceUnits(System.Drawing.Size value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-948AE26966B8DCA0 | Method | `public System.Int32 LogicalToDeviceUnits(System.Int32 value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C9EDAF721C906145 | Method | `public System.Void Paste()` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-34AA7C9293AC4B23 | Method | `public System.Void PerformLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1BC54BA620AD8EB6 | Method | `public System.Void PerformLayout(System.Windows.Forms.Control affectedControl, System.String affectedProperty)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5E2CACFA66F368F8 | Method | `public System.Drawing.Point PointToClient(System.Drawing.Point p)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E8822BF253D62B5 | Method | `public System.Drawing.Point PointToScreen(System.Drawing.Point p)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-35D0E512375C8A33 | Method | `public System.Windows.Forms.PreProcessControlState PreProcessControlMessage(ref System.Windows.Forms.Message msg)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C66CDD250BDA78E5 | Method | `public virtual System.Boolean PreProcessMessage(ref System.Windows.Forms.Message msg)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4C4121669F878C3E | Method | `public System.Drawing.Rectangle RectangleToClient(System.Drawing.Rectangle r)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BDBB347908D2E8B0 | Method | `public System.Drawing.Rectangle RectangleToScreen(System.Drawing.Rectangle r)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-994F003E58724953 | Method | `public virtual System.Void Refresh()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-616922C662E104D2 | Method | `public virtual System.Void ResetBackColor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-00D4656BE322F09D | Method | `public System.Void ResetBindings()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-49DB7271499C4EE2 | Method | `public virtual System.Void ResetCursor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-625F775D58B8552D | Method | `public virtual System.Void ResetFont()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5FB2FA899750463E | Method | `public virtual System.Void ResetForeColor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A6B581A7C2DE3700 | Method | `public System.Void ResetImeMode()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-19F8D155EB766BC8 | Method | `public virtual System.Void ResetRightToLeft()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2A0D7CE3964F1A85 | Method | `public virtual System.Void ResetText()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-42668AFE89F3A07B | Method | `public System.Void ResumeLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F47CF308BE985D4E | Method | `public System.Void ResumeLayout(System.Boolean performLayout)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B541593CEB6DAE01 | Method | `public System.Void Scale(System.Drawing.SizeF factor)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5AD1806E6FBD10CD | Method | `public System.Void Scale(System.Single dx, System.Single dy)` | `System.Windows.Forms.Control` ObsoleteAttribute("This method has been deprecated. Use the Scale(SizeF ratio) method instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-81A25C26DE21659C | Method | `public System.Void Scale(System.Single ratio)` | `System.Windows.Forms.Control` ObsoleteAttribute("This method has been deprecated. Use the Scale(SizeF ratio) method instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC82AF5ECB0E0BD6 | Method | `public System.Void ScaleBitmapLogicalToDevice(ref System.Drawing.Bitmap logicalBitmap)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3ACBF4CFB5520259 | Method | `public System.Void ScrollToCaret()` | `System.Windows.Forms.MaskedTextBox`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C9C837932C01BD24 | Method | `public System.Void Select()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D172EE051842734E | Method | `public System.Void Select(System.Int32 start, System.Int32 length)` | `System.Windows.Forms.TextBoxBase`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E52F54EDA3EE6CC6 | Method | `public System.Void SelectAll()` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-483B5F0FE3261627 | Method | `public System.Boolean SelectNextControl(System.Windows.Forms.Control ctl, System.Boolean forward, System.Boolean tabStopOnly, System.Boolean nested, System.Boolean wrap)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6F0B9F7AF023653F | Method | `public System.Void SendToBack()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A1CEBABB66A16EB | Method | `public System.Void SetBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6D2948D144B953B1 | Method | `public System.Void SetBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D973B02B0F10B070 | Method | `public System.Void Show()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-045DED44DF387873 | Method | `public System.Void SuspendLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-00E4E862D19305D8 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.MaskedTextBox`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6C4038F9E6BDBF9B | Method | `public System.Void Undo()` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8A21766446FD03CE | Method | `public System.Void Update()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-158B3873E34AD221 | Method | `public System.Object ValidateText()` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2A7B788B71651B92 | Property | `System.Boolean AcceptsTab { public get; public set; }` | `System.Windows.Forms.MaskedTextBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-232406C1803DFB96 | Property | `System.Windows.Forms.AccessibleObject AccessibilityObject { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-817A516F6DF4AC90 | Property | `System.String AccessibleDefaultActionDescription { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EEDD5C97A4C092FE | Property | `System.String AccessibleDescription { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EF783B475EE1553A | Property | `System.String AccessibleName { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E7F5ACCDE2A0B3DC | Property | `System.Windows.Forms.AccessibleRole AccessibleRole { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(-1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3566281F7DFD6DB5 | Property | `virtual System.Boolean AllowDrop { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5EE052DF270FD774 | Property | `System.Boolean AllowPromptAsInput { public get; public set; }` | `System.Windows.Forms.MaskedTextBox` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5C13D541D7EAC5B9 | Property | `virtual System.Windows.Forms.AnchorStyles Anchor { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true); DefaultValueAttribute(5) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B02376223939F2D | Property | `System.Boolean AsciiOnly { public get; public set; }` | `System.Windows.Forms.MaskedTextBox` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6C2CBE5A8132731A | Property | `virtual System.Drawing.Point AutoScrollOffset { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DefaultValueAttribute(System.Drawing.Point,"0, 0") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-19C9ADB7BE894C21 | Property | `virtual System.Boolean AutoSize { public get; public set; }` | `System.Windows.Forms.TextBoxBase` DefaultValueAttribute(true); LocalizableAttribute(true); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FDA2D250C5300A56 | Property | `virtual System.Drawing.Color BackColor { public get; public set; }` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-60FFAD0D6435F691 | Property | `virtual System.Drawing.Image BackgroundImage { public get; public set; }` | `System.Windows.Forms.TextBoxBase` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A67F73D62CE31EBF | Property | `virtual System.Windows.Forms.ImageLayout BackgroundImageLayout { public get; public set; }` | `System.Windows.Forms.TextBoxBase` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BF01E84915C89F96 | Property | `System.Boolean BeepOnError { public get; public set; }` | `System.Windows.Forms.MaskedTextBox` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-002BB553121676CA | Property | `virtual System.Windows.Forms.BindingContext BindingContext { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6F147FFC955DF32A | Property | `System.Windows.Forms.BorderStyle BorderStyle { public get; public set; }` | `System.Windows.Forms.TextBoxBase` DefaultValueAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D56A362E19702AF4 | Property | `System.Int32 Bottom { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E86E190187F1CA59 | Property | `System.Drawing.Rectangle Bounds { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9D0A52D55AAC6639 | Property | `System.Boolean CanFocus { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-566BE0958C9AE201 | Property | `System.Boolean CanSelect { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F0FAC187DEA74DB6 | Property | `System.Boolean CanUndo { public get; }` | `System.Windows.Forms.MaskedTextBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-76EE28E2090D304B | Property | `System.Boolean Capture { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F51EB086BEE3BDA8 | Property | `System.Boolean CausesValidation { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A300386C644E8EC9 | Property | `static System.Boolean CheckForIllegalCrossThreadCalls { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-67A8F48FF912173B | Property | `System.Drawing.Rectangle ClientRectangle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B52884CF41A052CA | Property | `System.Drawing.Size ClientSize { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E9B708A1D9684F54 | Property | `System.String CompanyName { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A58C3B64A602FAE8 | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CF31A1E3AE9DEA6B | Property | `System.Boolean ContainsFocus { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D141FE14F7D49CF7 | Property | `virtual System.Windows.Forms.ContextMenu ContextMenu { public get; public set; }` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6C1B95391DCE05FB | Property | `virtual System.Windows.Forms.ContextMenuStrip ContextMenuStrip { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-78348B34FD27499E | Property | `System.Windows.Forms.Control+ControlCollection Controls { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-860477000E485EDA | Property | `System.Boolean Created { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-128F2A44263D306B | Property | `System.Globalization.CultureInfo Culture { public get; public set; }` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3196DD3902F80238 | Property | `virtual System.Windows.Forms.Cursor Cursor { public get; public set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-08E2B73C59F4A049 | Property | `System.Windows.Forms.MaskFormat CutCopyMaskFormat { public get; public set; }` | `System.Windows.Forms.MaskedTextBox` DefaultValueAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5D9A643DB6DA4F02 | Property | `System.Windows.Forms.ControlBindingsCollection DataBindings { public get; }` | `System.Windows.Forms.Control` DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8BC5744FB2A12ECD | Property | `virtual System.Object DataContext { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); BindableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-925458958DAF26D3 | Property | `static System.Drawing.Color DefaultBackColor { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2C9838061846A302 | Property | `static System.Drawing.Font DefaultFont { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1F39EE4B6E252B18 | Property | `static System.Drawing.Color DefaultForeColor { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BD83580BE385583C | Property | `System.Int32 DeviceDpi { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C42E005E8447E815 | Property | `virtual System.Drawing.Rectangle DisplayRectangle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-79B8C88E74168024 | Property | `System.Boolean Disposing { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1DDEEC23F08FED94 | Property | `virtual System.Windows.Forms.DockStyle Dock { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true); DefaultValueAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B2B23ABFF455ED54 | Property | `System.Boolean Enabled { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-211D06D933EF6AEC | Property | `virtual System.Boolean Focused { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4C1F370C29418951 | Property | `virtual System.Drawing.Font Font { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B3CE1412EFAFD368 | Property | `virtual System.Drawing.Color ForeColor { public get; public set; }` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9CAC3EAE5575DDDF | Property | `System.IFormatProvider FormatProvider { public get; public set; }` | `System.Windows.Forms.MaskedTextBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-85D43D0E39AF994C | Property | `System.IntPtr Handle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3B5FF6684039D3D1 | Property | `System.Boolean HasChildren { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A7A6C1205D368EF | Property | `System.Int32 Height { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E6A1232C78767397 | Property | `System.Boolean HidePromptOnLeave { public get; public set; }` | `System.Windows.Forms.MaskedTextBox` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-394778720FCCB0DC | Property | `System.Boolean HideSelection { public get; public set; }` | `System.Windows.Forms.TextBoxBase` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DDF7AA5EC2F619A5 | Property | `System.Windows.Forms.ImeMode ImeMode { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6D004F1D7BF7BCA5 | Property | `System.Windows.Forms.InsertKeyMode InsertKeyMode { public get; public set; }` | `System.Windows.Forms.MaskedTextBox` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-771204CEB79FD4E2 | Property | `System.Boolean InvokeRequired { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-82A9518A990E28E2 | Property | `System.Boolean IsAccessible { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9AB88423D95BF5D3 | Property | `System.Boolean IsAncestorSiteInDesignMode { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5C086F91AC3286B1 | Property | `System.Boolean IsDisposed { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-05D176DDF294262F | Property | `System.Boolean IsHandleCreated { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-54F35EB27CB7A9C6 | Property | `System.Boolean IsMirrored { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B7C5DF8A0924ADE3 | Property | `System.Boolean IsOverwriteMode { public get; }` | `System.Windows.Forms.MaskedTextBox` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8F40BC8B68ECF7DA | Property | `virtual System.Windows.Forms.Layout.LayoutEngine LayoutEngine { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6C7A9CFB65312BD0 | Property | `System.Int32 Left { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5BF2C05B42CE5FA4 | Property | `System.String[] Lines { public get; public set; }` | `System.Windows.Forms.MaskedTextBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-01680D2765F753CC | Property | `System.Drawing.Point Location { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4CD7B904E6D080D1 | Property | `System.Windows.Forms.Padding Margin { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C8023269EA06D447 | Property | `System.String Mask { public get; public set; }` | `System.Windows.Forms.MaskedTextBox` DefaultValueAttribute(""); LocalizableAttribute(true); EditorAttribute("System.Windows.Forms.Design.MaskPropertyEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7F526D4E461D00DF | Property | `System.Boolean MaskCompleted { public get; }` | `System.Windows.Forms.MaskedTextBox` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-089DAEE29625DD9B | Property | `System.ComponentModel.MaskedTextProvider MaskedTextProvider { public get; }` | `System.Windows.Forms.MaskedTextBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-14C258603BD9BA8E | Property | `System.Boolean MaskFull { public get; }` | `System.Windows.Forms.MaskedTextBox` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BFB6F8BD29A9DB5E | Property | `virtual System.Drawing.Size MaximumSize { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D84B7246DDCBDB71 | Property | `virtual System.Int32 MaxLength { public get; public set; }` | `System.Windows.Forms.MaskedTextBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B21BAFD79D224215 | Property | `virtual System.Drawing.Size MinimumSize { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3E37EED6A059800A | Property | `System.Boolean Modified { public get; public set; }` | `System.Windows.Forms.TextBoxBase` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B40310D46E9BE25 | Property | `static System.Windows.Forms.Keys ModifierKeys { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F638D90CFEF86FA1 | Property | `static System.Windows.Forms.MouseButtons MouseButtons { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6BEC649AADD0FF65 | Property | `static System.Drawing.Point MousePosition { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DCF30EE329FDE7A0 | Property | `virtual System.Boolean Multiline { public get; public set; }` | `System.Windows.Forms.MaskedTextBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E3A9CC5458B7F00B | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7E6F270CC9246898 | Property | `System.Windows.Forms.Padding Padding { public get; public set; }` | `System.Windows.Forms.TextBoxBase` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-327DE4649701F440 | Property | `System.Windows.Forms.Control Parent { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-99244C38DEC3D998 | Property | `System.Char PasswordChar { public get; public set; }` | `System.Windows.Forms.MaskedTextBox` DefaultValueAttribute('\0') | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E165F40AC0C17EEA | Property | `System.Int32 PreferredHeight { public get; }` | `System.Windows.Forms.TextBoxBase` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D12A84EC0DE47526 | Property | `System.Drawing.Size PreferredSize { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A6481EFD415CA84 | Property | `System.String ProductName { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-31B3F7767D7A52EA | Property | `System.String ProductVersion { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B35DCA3BD2441F8E | Property | `System.Char PromptChar { public get; public set; }` | `System.Windows.Forms.MaskedTextBox` LocalizableAttribute(true); DefaultValueAttribute('_') | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B1A93893E2AFC201 | Property | `System.Boolean ReadOnly { public get; public set; }` | `System.Windows.Forms.MaskedTextBox`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-575F112F449C9429 | Property | `System.Boolean RecreatingHandle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B01753576F6D6C9E | Property | `System.Drawing.Region Region { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-25E961D5CDDAA627 | Property | `System.Boolean RejectInputOnFirstFailure { public get; public set; }` | `System.Windows.Forms.MaskedTextBox` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6D399622FB1CFACD | Property | `System.Boolean ResetOnPrompt { public get; public set; }` | `System.Windows.Forms.MaskedTextBox` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-46F712A64311D006 | Property | `System.Boolean ResetOnSpace { public get; public set; }` | `System.Windows.Forms.MaskedTextBox` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C5C8A77C5AF3ED26 | Property | `System.Int32 Right { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7B250A851CDF9B85 | Property | `virtual System.Windows.Forms.RightToLeft RightToLeft { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3E9DF0383C3B6C88 | Property | `virtual System.String SelectedText { public get; public set; }` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-811583BAF6555FAB | Property | `virtual System.Int32 SelectionLength { public get; public set; }` | `System.Windows.Forms.TextBoxBase` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-113E461E65244E30 | Property | `System.Int32 SelectionStart { public get; public set; }` | `System.Windows.Forms.TextBoxBase` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-DF56BB134C1C5902 | Property | `virtual System.Boolean ShortcutsEnabled { public get; public set; }` | `System.Windows.Forms.TextBoxBase` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8F27577975F223F2 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-863C8ECC6BFF3C93 | Property | `System.Drawing.Size Size { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-72FD1D8AD1EE1E4A | Property | `System.Boolean SkipLiterals { public get; public set; }` | `System.Windows.Forms.MaskedTextBox` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EE3C95D57EC0873B | Property | `System.Int32 TabIndex { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-838FEFDACFE68FE8 | Property | `System.Boolean TabStop { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D3A3E4CE7F65A4E3 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1DBE83C0E4D15364 | Property | `virtual System.String Text { public get; public set; }` | `System.Windows.Forms.MaskedTextBox` EditorAttribute("System.Windows.Forms.Design.MaskedTextBoxTextEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); BindableAttribute(true); DefaultValueAttribute(""); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F7C10CCBACA09E69 | Property | `System.Windows.Forms.HorizontalAlignment TextAlign { public get; public set; }` | `System.Windows.Forms.MaskedTextBox` LocalizableAttribute(true); DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A06A8AF6905A838E | Property | `virtual System.Int32 TextLength { public get; }` | `System.Windows.Forms.MaskedTextBox` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8CA28AE05DA2CE96 | Property | `System.Windows.Forms.MaskFormat TextMaskFormat { public get; public set; }` | `System.Windows.Forms.MaskedTextBox` DefaultValueAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-09D3A19DE70D107C | Property | `System.Int32 Top { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-AD8BF8DD54633A2F | Property | `System.Windows.Forms.Control TopLevelControl { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-072AC2D246987756 | Property | `System.Boolean UseSystemPasswordChar { public get; public set; }` | `System.Windows.Forms.MaskedTextBox` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9D2A11F505262817 | Property | `System.Boolean UseWaitCursor { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(false); BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CB8266254136DAB0 | Property | `System.Type ValidatingType { public get; public set; }` | `System.Windows.Forms.MaskedTextBox` BrowsableAttribute(false); DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BD2E066706DBB001 | Property | `System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2B086ACFF25D9AD6 | Property | `System.Int32 Width { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3EA46D07F7BCD2ED | Property | `System.Windows.Forms.IWindowTarget WindowTarget { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-72EA6D95CF042B4B | Property | `System.Boolean WordWrap { public get; public set; }` | `System.Windows.Forms.MaskedTextBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-CBC6EF2A60D5195D | Method | `protected System.Void AccessibilityNotifyClients(System.Windows.Forms.AccessibleEvents accEvent, System.Int32 objectID, System.Int32 childID)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-74182658FAAB6066 | Method | `protected internal System.Void AccessibilityNotifyClients(System.Windows.Forms.AccessibleEvents accEvent, System.Int32 childID)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8DFEF23E57031BF2 | Method | `protected System.Boolean ContainsNavigationKeyCode(System.Windows.Forms.Keys keyCode)` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9962CC2F1F53B578 | Method | `protected virtual System.Windows.Forms.AccessibleObject CreateAccessibilityInstance()` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A9160EDD558A7BD4 | Method | `protected virtual System.Windows.Forms.Control+ControlCollection CreateControlsInstance()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-735C338F325F63C7 | Method | `protected virtual System.Void CreateHandle()` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-44E393540AD97F00 | Method | `protected virtual System.Void DefWndProc(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EDD2CD5A3D8364DF | Method | `protected virtual System.Void DestroyHandle()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DDCE2F156BDCC168 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-21B893307DF9D715 | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D885680A1D8F2CFD | Method | `protected virtual System.Windows.Forms.AccessibleObject GetAccessibilityObjectById(System.Int32 objectId)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6B148D80622B2F62 | Method | `protected System.Windows.Forms.AutoSizeMode GetAutoSizeMode()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C1A54AB1C85325EE | Method | `protected virtual System.Drawing.Rectangle GetScaledBounds(System.Drawing.Rectangle bounds, System.Drawing.SizeF factor, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EBB1AD6378B31AF2 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C14E836B2D1E7A2C | Method | `protected System.Boolean GetStyle(System.Windows.Forms.ControlStyles flag)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F6956F5307F3438F | Method | `protected System.Boolean GetTopLevel()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-309DBAAADD9BFF85 | Method | `protected virtual System.Void InitLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-011B27C564126939 | Method | `protected System.Void InvokeGotFocus(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C62A4B094D3D4BC5 | Method | `protected System.Void InvokeLostFocus(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C2CEA7EF95B7035 | Method | `protected System.Void InvokeOnClick(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-535E3A6E276CA576 | Method | `protected System.Void InvokePaint(System.Windows.Forms.Control c, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CF448947165CF965 | Method | `protected System.Void InvokePaintBackground(System.Windows.Forms.Control c, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F53B7229C72B43B4 | Method | `protected virtual System.Boolean IsInputChar(System.Char charCode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-55FF7B558EE1ADE6 | Method | `protected virtual System.Boolean IsInputKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-999C4340234B1743 | Method | `protected virtual System.Void NotifyInvalidate(System.Drawing.Rectangle invalidatedArea)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-810EDD7AE0F44574 | Method | `protected virtual System.Void OnAcceptsTabChanged(System.EventArgs e)` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-424200B71043D7A1 | Method | `protected virtual System.Void OnAutoSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-582E28B6F763918D | Method | `protected virtual System.Void OnBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.MaskedTextBox`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F7682667454C307B | Method | `protected virtual System.Void OnBackgroundImageChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-89ADC52DC7199691 | Method | `protected virtual System.Void OnBackgroundImageLayoutChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2BC8A75154C6AA42 | Method | `protected virtual System.Void OnBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-76BFA23BD55AC9FE | Method | `protected virtual System.Void OnBorderStyleChanged(System.EventArgs e)` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E76AE5720C9FC62F | Method | `protected virtual System.Void OnCausesValidationChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C7574C975829F17C | Method | `protected virtual System.Void OnChangeUICues(System.Windows.Forms.UICuesEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-855E54BA138232B2 | Method | `protected virtual System.Void OnClick(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DE0383B9E6C26921 | Method | `protected virtual System.Void OnClientSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D4A268DD0A8C62BE | Method | `protected virtual System.Void OnContextMenuChanged(System.EventArgs e)` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EE344AA15B29002F | Method | `protected virtual System.Void OnContextMenuStripChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6399B969076CD490 | Method | `protected virtual System.Void OnControlAdded(System.Windows.Forms.ControlEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FFAC417B69B35EF4 | Method | `protected virtual System.Void OnControlRemoved(System.Windows.Forms.ControlEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5F202160C21D140F | Method | `protected virtual System.Void OnCreateControl()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC680F66B8CF4972 | Method | `protected virtual System.Void OnCursorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DACA9996167B2C59 | Method | `protected virtual System.Void OnDataContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B896D5C34DC9675 | Method | `protected virtual System.Void OnDockChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1C077BA299CBD879 | Method | `protected virtual System.Void OnDoubleClick(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1980B37564DE33CE | Method | `protected virtual System.Void OnDpiChangedAfterParent(System.EventArgs e)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C39E8FFE64460696 | Method | `protected virtual System.Void OnDpiChangedBeforeParent(System.EventArgs e)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4CB95181E9BEF56F | Method | `protected virtual System.Void OnDragDrop(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9EEE3DB955C05504 | Method | `protected virtual System.Void OnDragEnter(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4B0CCE40A4537F9D | Method | `protected virtual System.Void OnDragLeave(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F36256EF5CA82C2B | Method | `protected virtual System.Void OnDragOver(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9F34E6462C0D932B | Method | `protected virtual System.Void OnEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC9DA29EF7B08AB5 | Method | `protected internal virtual System.Void OnEnter(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A3ADA27E4853F904 | Method | `protected virtual System.Void OnFontChanged(System.EventArgs e)` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3BC8DEB5F9F272EE | Method | `protected virtual System.Void OnForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D0EF2651470BD747 | Method | `protected virtual System.Void OnGiveFeedback(System.Windows.Forms.GiveFeedbackEventArgs gfbevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CBBD24D6C5236BEA | Method | `protected virtual System.Void OnGotFocus(System.EventArgs e)` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B4E0A4C65BD88E26 | Method | `protected virtual System.Void OnHandleCreated(System.EventArgs e)` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C73827C26ADBDB62 | Method | `protected virtual System.Void OnHandleDestroyed(System.EventArgs e)` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FA034590BA5290BA | Method | `protected virtual System.Void OnHelpRequested(System.Windows.Forms.HelpEventArgs hevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-207C2B053DEB7813 | Method | `protected virtual System.Void OnHideSelectionChanged(System.EventArgs e)` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A972222AA1DFB97C | Method | `protected virtual System.Void OnImeModeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E901E399632D6A5D | Method | `protected virtual System.Void OnInvalidated(System.Windows.Forms.InvalidateEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0ECAECB6C3CBF844 | Method | `protected virtual System.Void OnIsOverwriteModeChanged(System.EventArgs e)` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6D46E8891375AA08 | Method | `protected virtual System.Void OnKeyDown(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D8B145F7AE8CBAE | Method | `protected virtual System.Void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4F00D7FBEF9C6D05 | Method | `protected virtual System.Void OnKeyUp(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A4B29A9BB1F38555 | Method | `protected virtual System.Void OnLayout(System.Windows.Forms.LayoutEventArgs levent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F5F7C12A7C2A4FBC | Method | `protected internal virtual System.Void OnLeave(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7275B69EE2D856F7 | Method | `protected virtual System.Void OnLocationChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3276A012E7C31DE9 | Method | `protected virtual System.Void OnLostFocus(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1BAEF3B9799C406C | Method | `protected virtual System.Void OnMarginChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DD0D01E21F8A53E7 | Method | `protected virtual System.Void OnMaskChanged(System.EventArgs e)` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C5B7F53AC52FC73 | Method | `protected virtual System.Void OnModifiedChanged(System.EventArgs e)` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E1B21C1845253A5F | Method | `protected virtual System.Void OnMouseCaptureChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6A9303C9F710AF1E | Method | `protected virtual System.Void OnMouseClick(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-19DE1C03A9324B9A | Method | `protected virtual System.Void OnMouseDoubleClick(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DAE1AD35A8C82609 | Method | `protected virtual System.Void OnMouseDown(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7A56A46F4ABE24C6 | Method | `protected virtual System.Void OnMouseEnter(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-14205E47CE5CB092 | Method | `protected virtual System.Void OnMouseHover(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4A5ED155B363459E | Method | `protected virtual System.Void OnMouseLeave(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DF56E714EB941483 | Method | `protected virtual System.Void OnMouseMove(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8D3CB18C7A685889 | Method | `protected virtual System.Void OnMouseUp(System.Windows.Forms.MouseEventArgs mevent)` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A806CFE52BA7A0DA | Method | `protected virtual System.Void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7683E5922A4FF5BB | Method | `protected virtual System.Void OnMove(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4AEEAD7BF523D95F | Method | `protected virtual System.Void OnMultilineChanged(System.EventArgs e)` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2FF82B788A77F265 | Method | `protected virtual System.Void OnNotifyMessage(System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6CA1F3AF8FFC02D9 | Method | `protected virtual System.Void OnPaddingChanged(System.EventArgs e)` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BB54E1FA8EBCF896 | Method | `protected virtual System.Void OnPaint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7F07CB8656CE76CF | Method | `protected virtual System.Void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-235678E5E4B3A90A | Method | `protected virtual System.Void OnParentBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B2B9954C10FEE6A0 | Method | `protected virtual System.Void OnParentBackgroundImageChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-034C3DFA504039C2 | Method | `protected virtual System.Void OnParentBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9417C90478EF763C | Method | `protected virtual System.Void OnParentChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4CB7EC4E3EAB21D4 | Method | `protected virtual System.Void OnParentCursorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-456D59E428EE341B | Method | `protected virtual System.Void OnParentDataContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D571C1CB762DFF03 | Method | `protected virtual System.Void OnParentEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C2D6EB25E839F7BD | Method | `protected virtual System.Void OnParentFontChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6147BBAD684E7436 | Method | `protected virtual System.Void OnParentForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3F936729F885859E | Method | `protected virtual System.Void OnParentRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7F05506DB312E4B4 | Method | `protected virtual System.Void OnParentVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A06F19CB4AF55ECD | Method | `protected virtual System.Void OnPreviewKeyDown(System.Windows.Forms.PreviewKeyDownEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-65536BA9B60DFC5A | Method | `protected virtual System.Void OnPrint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D2DEB0BCE39DA40 | Method | `protected virtual System.Void OnQueryContinueDrag(System.Windows.Forms.QueryContinueDragEventArgs qcdevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A9BE2FD8270EC36C | Method | `protected virtual System.Void OnReadOnlyChanged(System.EventArgs e)` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-26B41AF332DBB569 | Method | `protected virtual System.Void OnRegionChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC6A6D6DD5E41473 | Method | `protected virtual System.Void OnResize(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-73A2D75A78BA26A7 | Method | `protected virtual System.Void OnRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2032A9015BC93302 | Method | `protected virtual System.Void OnSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-DB6013DFC895D112 | Method | `protected virtual System.Void OnStyleChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A1BFC4DA141B7EF1 | Method | `protected virtual System.Void OnSystemColorsChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B9B904977D3D8E86 | Method | `protected virtual System.Void OnTabIndexChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-67238226FC29D962 | Method | `protected virtual System.Void OnTabStopChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DA15259D9784F0E6 | Method | `protected virtual System.Void OnTextAlignChanged(System.EventArgs e)` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-42B380ACD87005E3 | Method | `protected virtual System.Void OnTextChanged(System.EventArgs e)` | `System.Windows.Forms.MaskedTextBox`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CC8F09EBD30BAAC9 | Method | `protected virtual System.Void OnValidated(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-12AC0DD33C5FB069 | Method | `protected virtual System.Void OnValidating(System.ComponentModel.CancelEventArgs e)` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-794C8D459F40DCD4 | Method | `protected virtual System.Void OnVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5126716A57A8D021 | Method | `protected virtual System.Boolean ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-33CA84400067BACB | Method | `protected virtual System.Boolean ProcessDialogChar(System.Char charCode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8DAAF379F778B9FF | Method | `protected virtual System.Boolean ProcessDialogKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-38F95B17C6FBAA78 | Method | `protected virtual System.Boolean ProcessKeyEventArgs(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0D78FB2BB026898B | Method | `protected internal virtual System.Boolean ProcessKeyMessage(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-583F345E463A8A35 | Method | `protected virtual System.Boolean ProcessKeyPreview(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-299B07F810599744 | Method | `protected internal virtual System.Boolean ProcessMnemonic(System.Char charCode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-81D1E96ECBF7F565 | Method | `protected System.Void RaiseDragEvent(System.Object key, System.Windows.Forms.DragEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D9A495C5E00FB90D | Method | `protected System.Void RaiseKeyEvent(System.Object key, System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C5D2115CF70B1070 | Method | `protected System.Void RaiseMouseEvent(System.Object key, System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-64CB53D2B8F113BC | Method | `protected System.Void RaisePaintEvent(System.Object key, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A0E8FDECF67E7FE | Method | `protected System.Void RecreateHandle()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC0CF7154E7B8726 | Method | `protected static System.Boolean ReflectMessage(System.IntPtr hWnd, ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3C34C19503DEE9A6 | Method | `protected virtual System.Void RescaleConstantsForDpi(System.Int32 deviceDpiOld, System.Int32 deviceDpiNew)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-416E3F0592DFF4BF | Method | `protected System.Void ResetMouseEventArgs()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2DB54A779F84850E | Method | `protected System.Drawing.ContentAlignment RtlTranslateAlignment(System.Drawing.ContentAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9960ED203AD20095 | Method | `protected System.Windows.Forms.HorizontalAlignment RtlTranslateAlignment(System.Windows.Forms.HorizontalAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CFC6393635A24235 | Method | `protected System.Windows.Forms.LeftRightAlignment RtlTranslateAlignment(System.Windows.Forms.LeftRightAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F09292B80A8B6150 | Method | `protected internal System.Drawing.ContentAlignment RtlTranslateContent(System.Drawing.ContentAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C290C6AB18880690 | Method | `protected System.Windows.Forms.HorizontalAlignment RtlTranslateHorizontal(System.Windows.Forms.HorizontalAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0D87AFDBC0259032 | Method | `protected System.Windows.Forms.LeftRightAlignment RtlTranslateLeftRight(System.Windows.Forms.LeftRightAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1863F4CEC14E995E | Method | `protected virtual System.Void ScaleControl(System.Drawing.SizeF factor, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CD5BD7672F60BD9B | Method | `protected virtual System.Void ScaleCore(System.Single dx, System.Single dy)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5F99B74B206CACB4 | Method | `protected virtual System.Void Select(System.Boolean directed, System.Boolean forward)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B815BFDAC1724723 | Method | `protected System.Void SetAutoSizeMode(System.Windows.Forms.AutoSizeMode mode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8E57375190CF1D78 | Method | `protected virtual System.Void SetBoundsCore(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.TextBoxBase`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-72A5E20402D5B57A | Method | `protected virtual System.Void SetClientSizeCore(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-8420D417954EBB89 | Method | `protected System.Void SetStyle(System.Windows.Forms.ControlStyles flag, System.Boolean value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C903FE13A46F2785 | Method | `protected System.Void SetTopLevel(System.Boolean value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-310EDD2316563DCE | Method | `protected virtual System.Void SetVisibleCore(System.Boolean value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-21E3A8E247F0480F | Method | `protected virtual System.Drawing.Size SizeFromClientSize(System.Drawing.Size clientSize)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9B17202102C7C38C | Method | `protected System.Void UpdateBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-736E922705D840CA | Method | `protected System.Void UpdateBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Int32 clientWidth, System.Int32 clientHeight)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DEFABC0ADFE11B6D | Method | `protected internal System.Void UpdateBounds()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-89391004788E8D67 | Method | `protected System.Void UpdateStyles()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B848474BAA3DD7B2 | Method | `protected System.Void UpdateZOrder()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A534549E6269F4B3 | Method | `protected virtual System.Void WndProc(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B92715E8DE457FED | Property | `virtual System.Boolean CanEnableIme { protected get; }` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F369C382D6847844 | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-427616F67884B211 | Property | `virtual System.Windows.Forms.CreateParams CreateParams { protected get; }` | `System.Windows.Forms.MaskedTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9952FC13A5F90E77 | Property | `virtual System.Windows.Forms.Cursor DefaultCursor { protected get; }` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F9D9F5D3D60713A1 | Property | `virtual System.Windows.Forms.ImeMode DefaultImeMode { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C03BA154FA9B0678 | Property | `virtual System.Windows.Forms.Padding DefaultMargin { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-84374F0C169E5F48 | Property | `virtual System.Drawing.Size DefaultMaximumSize { protected get; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-414CBEE4D98185A1 | Property | `virtual System.Drawing.Size DefaultMinimumSize { protected get; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D1EE87850644A1BC | Property | `virtual System.Windows.Forms.Padding DefaultPadding { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B4B267129C8DEAC3 | Property | `virtual System.Drawing.Size DefaultSize { protected get; }` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-48AF207FF14131B1 | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-091F4A20DC563296 | Property | `virtual System.Boolean DoubleBuffered { protected get; protected set; }` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2657236E5773EB02 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A8A44E57D12C2CAE | Property | `System.Int32 FontHeight { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-901373E44BBEF5EE | Property | `virtual System.Windows.Forms.ImeMode ImeModeBase { protected get; protected set; }` | `System.Windows.Forms.TextBoxBase`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16A2E3B0C4D384CC | Property | `static System.Windows.Forms.ImeMode PropagatingImeMode { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-28ED47332A38E644 | Property | `System.Boolean RenderRightToLeft { protected internal get; }` | `System.Windows.Forms.Control` ObsoleteAttribute("This property has been deprecated. Please use RightToLeft instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FDF10F84AF50FA3B | Property | `System.Boolean ResizeRedraw { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-946ACDB0ADCF35F8 | Property | `virtual System.Boolean ScaleChildren { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FC324579AA61C528 | Property | `virtual System.Boolean ShowFocusCues { protected internal get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-05BE2FB2400A8FD9 | Property | `virtual System.Boolean ShowKeyboardCues { protected internal get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
