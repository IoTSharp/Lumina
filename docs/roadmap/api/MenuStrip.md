# MenuStrip：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/MenuStrip.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.MenuStrip`；基类：`System.Windows.Forms.ToolStrip`。Lumina 同名类型：🟡 存在；基类及基线接口形状：⬜ 未匹配。

公开属性 **115**，公开事件 **84**，公开方法 **80**；受保护扩展成员 **201**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-11C550965292C64C | Constructor | `public MenuStrip()` | `System.Windows.Forms.MenuStrip`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7B4684BA064C3F45 | Event | `public event System.EventHandler AutoSizeChanged` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D781AA3146369F04 | Event | `public event System.EventHandler BackColorChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-78BF46338EC902C8 | Event | `public event System.EventHandler BackgroundImageChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-64615088F048FFC8 | Event | `public event System.EventHandler BackgroundImageLayoutChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B855F474613764F4 | Event | `public event System.EventHandler BeginDrag` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B7BC65B01B967C4 | Event | `public event System.EventHandler BindingContextChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6978E1E880D92831 | Event | `public event System.EventHandler CausesValidationChanged` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6600816992E70360 | Event | `public event System.Windows.Forms.UICuesEventHandler ChangeUICues` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2BFAC2805A1639E1 | Event | `public event System.EventHandler Click` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5046626FBE32B740 | Event | `public event System.EventHandler ClientSizeChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E537BABAFA7A5643 | Event | `public event System.EventHandler ContextMenuChanged` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D3D3D50FA9D52CA0 | Event | `public event System.EventHandler ContextMenuStripChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1FEA9AC677F9B232 | Event | `public event System.Windows.Forms.ControlEventHandler ControlAdded` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DCABBE8CEC5B1C90 | Event | `public event System.Windows.Forms.ControlEventHandler ControlRemoved` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-84767B02DBDE2D27 | Event | `public event System.EventHandler CursorChanged` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2312EB6B825A96B7 | Event | `public event System.EventHandler DataContextChanged` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-747F624475673654 | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-82C07A5F3DE08EF9 | Event | `public event System.EventHandler DockChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0591E0A6E1D493D1 | Event | `public event System.EventHandler DoubleClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-03BF03652C6F7C1E | Event | `public event System.EventHandler DpiChangedAfterParent` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B96F5BEE21F9ABB | Event | `public event System.EventHandler DpiChangedBeforeParent` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-04DED7D02D8BE8BA | Event | `public event System.Windows.Forms.DragEventHandler DragDrop` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F3BF49DD716DBC4D | Event | `public event System.Windows.Forms.DragEventHandler DragEnter` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C7A91F64EB6BA27E | Event | `public event System.EventHandler DragLeave` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B8D27E3D19AAC3C2 | Event | `public event System.Windows.Forms.DragEventHandler DragOver` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9514C334A1B0C741 | Event | `public event System.EventHandler EnabledChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1CAAD359F8103645 | Event | `public event System.EventHandler EndDrag` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9D741D8E46B01289 | Event | `public event System.EventHandler Enter` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0BA7FC6961C173FA | Event | `public event System.EventHandler FontChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8C032229863899F5 | Event | `public event System.EventHandler ForeColorChanged` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4E1C29033BDAC618 | Event | `public event System.Windows.Forms.GiveFeedbackEventHandler GiveFeedback` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FEB307152FF8E8B1 | Event | `public event System.EventHandler GotFocus` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9F80F50673A7B8A2 | Event | `public event System.EventHandler HandleCreated` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CC35A4D90A988B95 | Event | `public event System.EventHandler HandleDestroyed` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CB61EE1A05C2702C | Event | `public event System.Windows.Forms.HelpEventHandler HelpRequested` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-11980F0A39ACC7D4 | Event | `public event System.EventHandler ImeModeChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-83FA5AE674D3C5CD | Event | `public event System.Windows.Forms.InvalidateEventHandler Invalidated` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-599523F6D5659F69 | Event | `public event System.Windows.Forms.ToolStripItemEventHandler ItemAdded` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DB4A3A69762D43F1 | Event | `public event System.Windows.Forms.ToolStripItemClickedEventHandler ItemClicked` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3206F33319424709 | Event | `public event System.Windows.Forms.ToolStripItemEventHandler ItemRemoved` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-73A6F82D5FF8C98C | Event | `public event System.Windows.Forms.KeyEventHandler KeyDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ACF7DA11D775BF2C | Event | `public event System.Windows.Forms.KeyPressEventHandler KeyPress` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-30E0EF4999ED7F65 | Event | `public event System.Windows.Forms.KeyEventHandler KeyUp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2D0F2B11C040C6CB | Event | `public event System.Windows.Forms.LayoutEventHandler Layout` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2120F431C915A8C4 | Event | `public event System.EventHandler LayoutCompleted` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E1C9B6831978F00 | Event | `public event System.EventHandler LayoutStyleChanged` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A2624BC5FDEF91E | Event | `public event System.EventHandler Leave` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-420344C0DE15FE09 | Event | `public event System.EventHandler LocationChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E1B9A35D4754B0E3 | Event | `public event System.EventHandler LostFocus` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8BC9360CCB1FF7F1 | Event | `public event System.EventHandler MarginChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6E8B8CFD1540E0C8 | Event | `public event System.EventHandler MenuActivate` | `System.Windows.Forms.MenuStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-48BD4774EF3F870F | Event | `public event System.EventHandler MenuDeactivate` | `System.Windows.Forms.MenuStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5B2201D637ADFD07 | Event | `public event System.EventHandler MouseCaptureChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EDD8CB69806A33AF | Event | `public event System.Windows.Forms.MouseEventHandler MouseClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6691AB728FA41F7B | Event | `public event System.Windows.Forms.MouseEventHandler MouseDoubleClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4054AC58D784854A | Event | `public event System.Windows.Forms.MouseEventHandler MouseDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DEDFF32BB024B233 | Event | `public event System.EventHandler MouseEnter` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D0364CCD6D30A97F | Event | `public event System.EventHandler MouseHover` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-04C8F833431B21A8 | Event | `public event System.EventHandler MouseLeave` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3C941C1255F30BA7 | Event | `public event System.Windows.Forms.MouseEventHandler MouseMove` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CD3EFCC655D87393 | Event | `public event System.Windows.Forms.MouseEventHandler MouseUp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-81FF8DAADAC7221D | Event | `public event System.Windows.Forms.MouseEventHandler MouseWheel` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BD80D6835C888625 | Event | `public event System.EventHandler Move` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29980750C80B9C1E | Event | `public event System.EventHandler PaddingChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D830DBF0FF183414 | Event | `public event System.Windows.Forms.PaintEventHandler Paint` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B898B766503208A8 | Event | `public event System.Windows.Forms.PaintEventHandler PaintGrip` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2174242855C71CD6 | Event | `public event System.EventHandler ParentChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EA64191D2A4E7212 | Event | `public event System.Windows.Forms.PreviewKeyDownEventHandler PreviewKeyDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4CA90BB467DD58BE | Event | `public event System.Windows.Forms.QueryAccessibilityHelpEventHandler QueryAccessibilityHelp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-68079CC1AC21639F | Event | `public event System.Windows.Forms.QueryContinueDragEventHandler QueryContinueDrag` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B043B7FF1345EF6 | Event | `public event System.EventHandler RegionChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BA265C9F6AFD1EB0 | Event | `public event System.EventHandler RendererChanged` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-668BB06BCFF6A4BB | Event | `public event System.EventHandler Resize` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C1B62F270384CB6D | Event | `public event System.EventHandler RightToLeftChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16D198038DF2D7FA | Event | `public event System.Windows.Forms.ScrollEventHandler Scroll` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-200C331F7624B82B | Event | `public event System.EventHandler SizeChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DF87AAABB50C318D | Event | `public event System.EventHandler StyleChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-23D8458A02DF660D | Event | `public event System.EventHandler SystemColorsChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BBF3272BE15E9DAF | Event | `public event System.EventHandler TabIndexChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D8AF3C699C138269 | Event | `public event System.EventHandler TabStopChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7C40DB3C1694A71D | Event | `public event System.EventHandler TextChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-BB49C54C5882893B | Event | `public event System.EventHandler Validated` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1D335F1A2A6BE04A | Event | `public event System.ComponentModel.CancelEventHandler Validating` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D5133C136B65B148 | Event | `public event System.EventHandler VisibleChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0D26E090729D8ADC | Method | `public System.IAsyncResult BeginInvoke(System.Action method)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-15173BD54C5E9F3C | Method | `public System.IAsyncResult BeginInvoke(System.Delegate method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5996704F3108EAB5 | Method | `public System.IAsyncResult BeginInvoke(System.Delegate method, System.Object[] args)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E07B396BD36F2A85 | Method | `public System.Void BringToFront()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A17AA1EBD971930E | Method | `public System.Boolean Contains(System.Windows.Forms.Control ctl)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D89A867EB9F1BD9B | Method | `public System.Void CreateControl()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D22AEA6C17120798 | Method | `public System.Drawing.Graphics CreateGraphics()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3EFF18F9038A556F | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-EECEBCF138B94489 | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9AE14FE13A3FFA9D | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EB328872D2513A72 | Method | `public System.Windows.Forms.DragDropEffects DoDragDropAsJson<!!0>(!!0 data, System.Windows.Forms.DragDropEffects allowedEffects) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-86E2B8E79D6F4C37 | Method | `public System.Windows.Forms.DragDropEffects DoDragDropAsJson<!!0>(!!0 data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0BEDF56076C46608 | Method | `public System.Void DrawToBitmap(System.Drawing.Bitmap bitmap, System.Drawing.Rectangle targetBounds)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-931887F81A855F9F | Method | `public System.Object EndInvoke(System.IAsyncResult asyncResult)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-898BE07F27B5E16C | Method | `public System.Windows.Forms.Form FindForm()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-377961EC6A6B82BE | Method | `public System.Boolean Focus()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FCD645D90BFB47D9 | Method | `public static System.Windows.Forms.Control FromChildHandle(System.IntPtr handle)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A6A4E974D3B468CE | Method | `public static System.Windows.Forms.Control FromHandle(System.IntPtr handle)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BBA882E22D3555C7 | Method | `public System.Windows.Forms.Control GetChildAtPoint(System.Drawing.Point point)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-978EEB746F9AB228 | Method | `public System.Windows.Forms.Control GetChildAtPoint(System.Drawing.Point pt, System.Windows.Forms.GetChildAtPointSkip skipValue)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9152C5505839429F | Method | `public System.Windows.Forms.IContainerControl GetContainerControl()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E3061B9C4F5D588D | Method | `public System.Windows.Forms.ToolStripItem GetItemAt(System.Drawing.Point point)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C67A0881E83CCF2A | Method | `public System.Windows.Forms.ToolStripItem GetItemAt(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A26E9D140F1B2D0B | Method | `public System.Windows.Forms.Control GetNextControl(System.Windows.Forms.Control ctl, System.Boolean forward)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6554399CDE8DCEAC | Method | `public virtual System.Windows.Forms.ToolStripItem GetNextItem(System.Windows.Forms.ToolStripItem start, System.Windows.Forms.ArrowDirection direction)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D1F7DA75C70C79BD | Method | `public virtual System.Drawing.Size GetPreferredSize(System.Drawing.Size proposedSize)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D1F8BE8F714EAEF7 | Method | `public System.Void Hide()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6E868E459BFEF92B | Method | `public System.Void Invalidate()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6D11B1ED5FE9ED68 | Method | `public System.Void Invalidate(System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-798BFD8097596673 | Method | `public System.Void Invalidate(System.Drawing.Rectangle rc)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3E88944A9E12E743 | Method | `public System.Void Invalidate(System.Drawing.Rectangle rc, System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B7C3C5926E66996B | Method | `public System.Void Invalidate(System.Drawing.Region region)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6A8F26CE329D59AB | Method | `public System.Void Invalidate(System.Drawing.Region region, System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-21694EC629A24B5C | Method | `public !!0 Invoke<!!0>(System.Func&#96;1<!!0> method) where !!0: None;` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9051F917B0621863 | Method | `public System.Object Invoke(System.Delegate method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BBAA65A83D4A9DE6 | Method | `public System.Object Invoke(System.Delegate method, System.Object[] args)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6D3B8367D6828881 | Method | `public System.Void Invoke(System.Action method)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B47D0B516A8E7525 | Method | `public System.Threading.Tasks.Task InvokeAsync(System.Action callback, System.Threading.CancellationToken cancellationToken = null)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-062EF5F82928509B | Method | `public System.Threading.Tasks.Task InvokeAsync(System.Func&#96;2<System.Threading.CancellationToken,System.Threading.Tasks.ValueTask> callback, System.Threading.CancellationToken cancellationToken = null)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-72C6B99E70E670D4 | Method | `public System.Threading.Tasks.Task&#96;1<!!0> InvokeAsync<!!0>(System.Func&#96;1<!!0> callback, System.Threading.CancellationToken cancellationToken = null) where !!0: None;` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C6AEBAB359C0CE85 | Method | `public System.Threading.Tasks.Task&#96;1<!!0> InvokeAsync<!!0>(System.Func&#96;2<System.Threading.CancellationToken,System.Threading.Tasks.ValueTask&#96;1<!!0>> callback, System.Threading.CancellationToken cancellationToken = null) where !!0: None;` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-BFAAF3E44E30A16B | Method | `public static System.Boolean IsKeyLocked(System.Windows.Forms.Keys keyVal)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C07FA03D4D99CC23 | Method | `public static System.Boolean IsMnemonic(System.Char charCode, System.String text)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CD890DD8FABB3B48 | Method | `public System.Drawing.Size LogicalToDeviceUnits(System.Drawing.Size value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B54A8DC4D7A4211 | Method | `public System.Int32 LogicalToDeviceUnits(System.Int32 value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E9893D900FC83434 | Method | `public System.Void PerformLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2DC391BCD9383351 | Method | `public System.Void PerformLayout(System.Windows.Forms.Control affectedControl, System.String affectedProperty)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-568FB43A72CB3044 | Method | `public System.Drawing.Point PointToClient(System.Drawing.Point p)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D2F338ADAD59D6F8 | Method | `public System.Drawing.Point PointToScreen(System.Drawing.Point p)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7BE7D5F30841DC99 | Method | `public System.Windows.Forms.PreProcessControlState PreProcessControlMessage(ref System.Windows.Forms.Message msg)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2EE9C436BBD4FA87 | Method | `public virtual System.Boolean PreProcessMessage(ref System.Windows.Forms.Message msg)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-82B35B4DE555479D | Method | `public System.Drawing.Rectangle RectangleToClient(System.Drawing.Rectangle r)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5BDFF05842FC9BF4 | Method | `public System.Drawing.Rectangle RectangleToScreen(System.Drawing.Rectangle r)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7A0C8AD7D923B0CF | Method | `public virtual System.Void Refresh()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3241E879634CEBF7 | Method | `public virtual System.Void ResetBackColor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-44F53DAE87E14484 | Method | `public System.Void ResetBindings()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3690AB8969A58F32 | Method | `public virtual System.Void ResetCursor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-719F44EE7F3F2BCA | Method | `public virtual System.Void ResetFont()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D7BB2BDA021D6571 | Method | `public virtual System.Void ResetForeColor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7A4B1EFC2CF6113F | Method | `public System.Void ResetImeMode()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F2C5C6BE223BFB43 | Method | `public System.Void ResetMinimumSize()` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9CB417191E4D2C30 | Method | `public virtual System.Void ResetRightToLeft()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-197B429E105CB478 | Method | `public virtual System.Void ResetText()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-30DF6BEC04D322FF | Method | `public System.Void ResumeLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FF2C4AF39B623049 | Method | `public System.Void ResumeLayout(System.Boolean performLayout)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3FF9F651631CDD7A | Method | `public System.Void Scale(System.Drawing.SizeF factor)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6B8E081F8C0CA199 | Method | `public System.Void Scale(System.Single dx, System.Single dy)` | `System.Windows.Forms.Control` ObsoleteAttribute("This method has been deprecated. Use the Scale(SizeF ratio) method instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-18C9433A452B9BDD | Method | `public System.Void Scale(System.Single ratio)` | `System.Windows.Forms.Control` ObsoleteAttribute("This method has been deprecated. Use the Scale(SizeF ratio) method instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2ECE7843090EDC0B | Method | `public System.Void ScaleBitmapLogicalToDevice(ref System.Drawing.Bitmap logicalBitmap)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-742920D55080F809 | Method | `public System.Void ScrollControlIntoView(System.Windows.Forms.Control activeControl)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-05B6ECFAF090BF14 | Method | `public System.Void Select()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3C80F56D08904D64 | Method | `public System.Boolean SelectNextControl(System.Windows.Forms.Control ctl, System.Boolean forward, System.Boolean tabStopOnly, System.Boolean nested, System.Boolean wrap)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-78A43137DEC2249D | Method | `public System.Void SendToBack()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-02BA2A297F8603CA | Method | `public System.Void SetAutoScrollMargin(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-85A3B67FF17B42BB | Method | `public System.Void SetBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8DD669A099A31722 | Method | `public System.Void SetBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6DA9B9929E07CE31 | Method | `public System.Void Show()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4FD1A47877130140 | Method | `public System.Void SuspendLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29B0E5F2A2A9A347 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.ToolStrip`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B05AE4C689CA9A03 | Method | `public System.Void Update()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-07882CC229861483 | Property | `System.Windows.Forms.AccessibleObject AccessibilityObject { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-03AC13124BB51655 | Property | `System.String AccessibleDefaultActionDescription { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A2F0BA927F276430 | Property | `System.String AccessibleDescription { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3DB17778922DF10E | Property | `System.String AccessibleName { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B408FF9082BCC49D | Property | `System.Windows.Forms.AccessibleRole AccessibleRole { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(-1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5F411072F8178CCB | Property | `System.Boolean AllowClickThrough { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(true); DefaultValueAttribute(false); DesignerSerializationVisibilityAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CA72F3BCFF9242C0 | Property | `virtual System.Boolean AllowDrop { public get; public set; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2DD114DEBC70237C | Property | `System.Boolean AllowItemReorder { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CA9ADF7F211610D4 | Property | `System.Boolean AllowMerge { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BB3F91263A7510EF | Property | `virtual System.Windows.Forms.AnchorStyles Anchor { public get; public set; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3EEB20032791F468 | Property | `virtual System.Boolean AutoScroll { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B89CE2BDCF1E037C | Property | `System.Drawing.Size AutoScrollMargin { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-19A9B05D92FD47DD | Property | `System.Drawing.Size AutoScrollMinSize { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F890888BDA49EEED | Property | `virtual System.Drawing.Point AutoScrollOffset { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DefaultValueAttribute(System.Drawing.Point,"0, 0") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2643152E5AD4B721 | Property | `System.Drawing.Point AutoScrollPosition { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-56A2BCDDA47A5559 | Property | `virtual System.Boolean AutoSize { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(true); BrowsableAttribute(true); DesignerSerializationVisibilityAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-803A7A0FD824AD9F | Property | `System.Drawing.Color BackColor { public get; public set; }` | `System.Windows.Forms.ToolStrip`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1B6337AEC52E58E0 | Property | `virtual System.Drawing.Image BackgroundImage { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B06E544ED8DE5BF | Property | `virtual System.Windows.Forms.ImageLayout BackgroundImageLayout { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(1); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-53D1CCFE10D446DE | Property | `virtual System.Windows.Forms.BindingContext BindingContext { public get; public set; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-01A345AC851B9660 | Property | `System.Int32 Bottom { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8EE193EDBC76F39F | Property | `System.Drawing.Rectangle Bounds { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4B072E4B26C0819F | Property | `System.Boolean CanFocus { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D032F26EB89265A2 | Property | `System.Boolean CanOverflow { public get; public set; }` | `System.Windows.Forms.MenuStrip` DefaultValueAttribute(false); BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-24711E88F5AD295F | Property | `System.Boolean CanSelect { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-30C928835FA4A329 | Property | `System.Boolean Capture { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-68EAE159E76EE38A | Property | `System.Boolean CausesValidation { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false); DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4A37B1634C47F61E | Property | `static System.Boolean CheckForIllegalCrossThreadCalls { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4FCE167EAF69DB7C | Property | `System.Drawing.Rectangle ClientRectangle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A60F797DFC234A0A | Property | `System.Drawing.Size ClientSize { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-10419C19A2CED1EA | Property | `System.String CompanyName { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0FF35BBC97F0AB3C | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0FB489ABFCC42867 | Property | `System.Boolean ContainsFocus { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8F2DD25AD699A051 | Property | `virtual System.Windows.Forms.ContextMenu ContextMenu { public get; public set; }` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-706B61F406D16F98 | Property | `virtual System.Windows.Forms.ContextMenuStrip ContextMenuStrip { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-82AEBD5F3BAB600F | Property | `System.Windows.Forms.Control+ControlCollection Controls { public get; }` | `System.Windows.Forms.ToolStrip` DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A14E423DD6E5FC08 | Property | `System.Boolean Created { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B24F9E7A8CD9EBEE | Property | `virtual System.Windows.Forms.Cursor Cursor { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-15EBA8500A832EFD | Property | `System.Windows.Forms.ControlBindingsCollection DataBindings { public get; }` | `System.Windows.Forms.Control` DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-37BD5D96BAB25AC2 | Property | `virtual System.Object DataContext { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); BindableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5D80D60B2E74D5CC | Property | `static System.Drawing.Color DefaultBackColor { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-33E5BE2D0BD8EBE7 | Property | `virtual System.Windows.Forms.ToolStripDropDownDirection DefaultDropDownDirection { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C331430F5560AE68 | Property | `static System.Drawing.Font DefaultFont { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F72BEEAEA8957313 | Property | `static System.Drawing.Color DefaultForeColor { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29C0B8C2E2533F04 | Property | `System.Int32 DeviceDpi { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D024C36980ACBA7F | Property | `virtual System.Drawing.Rectangle DisplayRectangle { public get; }` | `System.Windows.Forms.ToolStrip`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-450AFF20ACA1A97F | Property | `System.Boolean Disposing { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-AB39F6A66C527B88 | Property | `virtual System.Windows.Forms.DockStyle Dock { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(1) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7C47535EEC3D6D3B | Property | `System.Windows.Forms.ScrollableControl+DockPaddingEdges DockPadding { public get; }` | `System.Windows.Forms.ScrollableControl` DesignerSerializationVisibilityAttribute(0); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8FE54D6FC71B1A58 | Property | `System.Boolean Enabled { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7CAF7E174A2B998E | Property | `virtual System.Boolean Focused { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8F6FB0CD61546671 | Property | `virtual System.Drawing.Font Font { public get; public set; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-72E53068E6D07222 | Property | `System.Drawing.Color ForeColor { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2852BDB303D863E1 | Property | `System.Windows.Forms.ToolStripGripDisplayStyle GripDisplayStyle { public get; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CF240DBBD85BE353 | Property | `System.Windows.Forms.Padding GripMargin { public get; public set; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FF8A295B66758394 | Property | `System.Drawing.Rectangle GripRectangle { public get; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-820675AC955796FE | Property | `System.Windows.Forms.ToolStripGripStyle GripStyle { public get; public set; }` | `System.Windows.Forms.MenuStrip` DefaultValueAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2773F948F0C501D7 | Property | `System.IntPtr Handle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-EB5A7B43D8257350 | Property | `System.Boolean HasChildren { public get; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F1112ED4798FD65D | Property | `System.Int32 Height { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B9B8829264C10A4C | Property | `System.Windows.Forms.HScrollProperties HorizontalScroll { public get; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-74E66A60EA9D945C | Property | `System.Windows.Forms.ImageList ImageList { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(null); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-43BD5DE0F9BF704C | Property | `System.Drawing.Size ImageScalingSize { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(System.Drawing.Size,"16,16") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-85D0A0D34C2F92B9 | Property | `System.Windows.Forms.ImeMode ImeMode { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5C54E6ADA59F1BBC | Property | `System.Boolean InvokeRequired { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-852D2E734C0A7913 | Property | `System.Boolean IsAccessible { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E13553756655CB5 | Property | `System.Boolean IsAncestorSiteInDesignMode { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6766DBE69CCCA3C6 | Property | `System.Boolean IsCurrentlyDragging { public get; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D0C887C2E09E6FC4 | Property | `System.Boolean IsDisposed { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A5500AB4FD50574D | Property | `System.Boolean IsDropDown { public get; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1205957FF70B8DCD | Property | `System.Boolean IsHandleCreated { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-113509C5FC946E7E | Property | `System.Boolean IsMirrored { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6FE9CA0AEB1D545C | Property | `virtual System.Windows.Forms.ToolStripItemCollection Items { public get; }` | `System.Windows.Forms.ToolStrip` DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FE274CC85578661C | Property | `virtual System.Windows.Forms.Layout.LayoutEngine LayoutEngine { public get; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-61D923E3858700E0 | Property | `System.Windows.Forms.LayoutSettings LayoutSettings { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(null); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-227AFADF97D65DE9 | Property | `System.Windows.Forms.ToolStripLayoutStyle LayoutStyle { public get; public set; }` | `System.Windows.Forms.ToolStrip`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-AA03C6D16BDA1A0F | Property | `System.Int32 Left { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-40CB8AFCF69DCC03 | Property | `System.Drawing.Point Location { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A3E34FC439D98B9C | Property | `System.Windows.Forms.Padding Margin { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FFBA2A0ECF2FE189 | Property | `virtual System.Drawing.Size MaximumSize { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-858E39E7D94ACCD3 | Property | `System.Windows.Forms.ToolStripMenuItem MdiWindowListItem { public get; public set; }` | `System.Windows.Forms.MenuStrip` DefaultValueAttribute(null); TypeConverterAttribute(System.Windows.Forms.MdiWindowListItemConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C1A615E9DC24CD24 | Property | `virtual System.Drawing.Size MinimumSize { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-63D8B8B9F57AF0A8 | Property | `static System.Windows.Forms.Keys ModifierKeys { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FE8C05A2DE2EC560 | Property | `static System.Windows.Forms.MouseButtons MouseButtons { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D7483F4BB6BD8323 | Property | `static System.Drawing.Point MousePosition { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9149882EF7CBDB39 | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6B94EADF161CC337 | Property | `System.Windows.Forms.Orientation Orientation { public get; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-21A54D8F24D09937 | Property | `System.Windows.Forms.ToolStripOverflowButton OverflowButton { public get; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D63873FF3337DAC6 | Property | `System.Windows.Forms.Padding Padding { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3935B19D9E07E979 | Property | `System.Windows.Forms.Control Parent { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AB3CAFC8A0A483C1 | Property | `System.Drawing.Size PreferredSize { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C9009085A5F33BEC | Property | `System.String ProductName { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3A4D2AEE6B225BF9 | Property | `System.String ProductVersion { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2C85E790ADB99D64 | Property | `System.Boolean RecreatingHandle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16B5EDCB7CBFF56B | Property | `System.Drawing.Region Region { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-75A1E50C5881FCEB | Property | `System.Windows.Forms.ToolStripRenderer Renderer { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D9BAFD6BF3CF11C | Property | `System.Windows.Forms.ToolStripRenderMode RenderMode { public get; public set; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1AD1A80429220A8F | Property | `System.Int32 Right { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0655E6E8D63266BA | Property | `virtual System.Windows.Forms.RightToLeft RightToLeft { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4DDBAB013FD6FB91 | Property | `System.Boolean ShowItemToolTips { public get; public set; }` | `System.Windows.Forms.MenuStrip` DefaultValueAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B2012C39AE0D3C09 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-547F4D5FB967A1DF | Property | `System.Drawing.Size Size { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-881DD09C0D5F9899 | Property | `System.Boolean Stretch { public get; public set; }` | `System.Windows.Forms.MenuStrip` DefaultValueAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-42CDB652B80882FC | Property | `System.Int32 TabIndex { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-79F7BEE71590B307 | Property | `System.Boolean TabStop { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5252B2BA11292E67 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4BECFD68D9D8756A | Property | `virtual System.String Text { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true); BindableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-329906CD99DBC9EB | Property | `virtual System.Windows.Forms.ToolStripTextDirection TextDirection { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A95CDCE8A83A72B7 | Property | `System.Int32 Top { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B8EDAFF662BC271E | Property | `System.Windows.Forms.Control TopLevelControl { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-87BFA5D77ACF6577 | Property | `System.Boolean UseWaitCursor { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(false); BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-00ABD30304DA6A1A | Property | `System.Windows.Forms.VScrollProperties VerticalScroll { public get; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2BE525D79411342C | Property | `System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-BFAD8445934350AA | Property | `System.Int32 Width { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-25C2C17DC090C2E2 | Property | `System.Windows.Forms.IWindowTarget WindowTarget { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-951CE335EA8CE812 | Field | `protected const System.Int32 ScrollStateAutoScrolling = 1` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4DE035F4F505E1E3 | Field | `protected const System.Int32 ScrollStateFullDrag = 16` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1260559CB5C9D060 | Field | `protected const System.Int32 ScrollStateHScrollVisible = 2` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AA3C0FD4C63094C4 | Field | `protected const System.Int32 ScrollStateUserHasScrolled = 8` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-86E2A48D078644B1 | Field | `protected const System.Int32 ScrollStateVScrollVisible = 4` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D382FF8DDC7CC18B | Method | `protected System.Void AccessibilityNotifyClients(System.Windows.Forms.AccessibleEvents accEvent, System.Int32 objectID, System.Int32 childID)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B74D3871D208CE2E | Method | `protected internal System.Void AccessibilityNotifyClients(System.Windows.Forms.AccessibleEvents accEvent, System.Int32 childID)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BA3198BFD60F077A | Method | `protected virtual System.Void AdjustFormScrollbars(System.Boolean displayScrollbars)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3CD68F2F82AAC4ED | Method | `protected virtual System.Windows.Forms.AccessibleObject CreateAccessibilityInstance()` | `System.Windows.Forms.MenuStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7ECFC1DB80A8824C | Method | `protected virtual System.Windows.Forms.Control+ControlCollection CreateControlsInstance()` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B221AE881D33C924 | Method | `protected internal virtual System.Windows.Forms.ToolStripItem CreateDefaultItem(System.String text, System.Drawing.Image image, System.EventHandler onClick)` | `System.Windows.Forms.MenuStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A4F017E23C859AA | Method | `protected virtual System.Void CreateHandle()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B53C52A515CEDDE1 | Method | `protected virtual System.Windows.Forms.LayoutSettings CreateLayoutSettings(System.Windows.Forms.ToolStripLayoutStyle layoutStyle)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A95E21FF8C802EDF | Method | `protected virtual System.Void DefWndProc(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-12E1092F7FEEF71A | Method | `protected virtual System.Void DestroyHandle()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E9396FEE8739BBEE | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.ToolStrip`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-76CB94644B5BDD9C | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-22237A10238716E5 | Method | `protected virtual System.Windows.Forms.AccessibleObject GetAccessibilityObjectById(System.Int32 objectId)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EE85E5FDC81444D7 | Method | `protected System.Windows.Forms.AutoSizeMode GetAutoSizeMode()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-133A5F82DEF989FC | Method | `protected virtual System.Drawing.Rectangle GetScaledBounds(System.Drawing.Rectangle bounds, System.Drawing.SizeF factor, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A99643F8FD96CBFA | Method | `protected System.Boolean GetScrollState(System.Int32 bit)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-733BA3C48EB5C739 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7CEAB27E42AC018B | Method | `protected System.Boolean GetStyle(System.Windows.Forms.ControlStyles flag)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CCE0014AE0A9E297 | Method | `protected System.Boolean GetTopLevel()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-799787F51BE6C42A | Method | `protected virtual System.Void InitLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D619B67E36C7A34A | Method | `protected System.Void InvokeGotFocus(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D080CA7E7D42CF1F | Method | `protected System.Void InvokeLostFocus(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3B52610FBCE3F590 | Method | `protected System.Void InvokeOnClick(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-87BBF6853E9AF761 | Method | `protected System.Void InvokePaint(System.Windows.Forms.Control c, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16C10D0EF2120EBA | Method | `protected System.Void InvokePaintBackground(System.Windows.Forms.Control c, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-001749AA783C019B | Method | `protected virtual System.Boolean IsInputChar(System.Char charCode)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E057F3DF9F300B4B | Method | `protected virtual System.Boolean IsInputKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BDA4F2DA8349217F | Method | `protected virtual System.Void NotifyInvalidate(System.Drawing.Rectangle invalidatedArea)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9CA09B3B432C07EB | Method | `protected virtual System.Void OnAutoSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C24C067523C415BB | Method | `protected virtual System.Void OnBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-ED5E3A5C09E50AE5 | Method | `protected virtual System.Void OnBackgroundImageChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-96AA72A5D24AB514 | Method | `protected virtual System.Void OnBackgroundImageLayoutChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2C3052D7FDEF8E93 | Method | `protected virtual System.Void OnBeginDrag(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-63D68C0789786C97 | Method | `protected virtual System.Void OnBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4F6962E1AD40CCCE | Method | `protected virtual System.Void OnCausesValidationChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7A547CB9B83606EE | Method | `protected virtual System.Void OnChangeUICues(System.Windows.Forms.UICuesEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EF879AFB7E1C5FEF | Method | `protected virtual System.Void OnClick(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FBD4F2A1E28C1190 | Method | `protected virtual System.Void OnClientSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A1F97869D17F139 | Method | `protected virtual System.Void OnContextMenuChanged(System.EventArgs e)` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-587651745223BCE3 | Method | `protected virtual System.Void OnContextMenuStripChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7F2DF1251614542D | Method | `protected virtual System.Void OnControlAdded(System.Windows.Forms.ControlEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B27B834B4E1F9CF3 | Method | `protected virtual System.Void OnControlRemoved(System.Windows.Forms.ControlEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AA3EC2BE984C7D36 | Method | `protected virtual System.Void OnCreateControl()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2A2346B2C85B5837 | Method | `protected virtual System.Void OnCursorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-229A77567F0DBEE5 | Method | `protected virtual System.Void OnDataContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0814E342EA31D95D | Method | `protected virtual System.Void OnDockChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7AC1E12CD6EAA1A1 | Method | `protected virtual System.Void OnDoubleClick(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FFA8B856A68A53BE | Method | `protected virtual System.Void OnDpiChangedAfterParent(System.EventArgs e)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C85A0A4C22F14FAB | Method | `protected virtual System.Void OnDpiChangedBeforeParent(System.EventArgs e)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A4DA72F548E70349 | Method | `protected virtual System.Void OnDragDrop(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4117CC3A7D2E697A | Method | `protected virtual System.Void OnDragEnter(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5CDC70A7CEFA5BBD | Method | `protected virtual System.Void OnDragLeave(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-582783890DD90451 | Method | `protected virtual System.Void OnDragOver(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7C89F07AAEE273AB | Method | `protected virtual System.Void OnEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-62BB748B31931C36 | Method | `protected virtual System.Void OnEndDrag(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B239D1232F44958C | Method | `protected internal virtual System.Void OnEnter(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B24B2F0E612672D | Method | `protected virtual System.Void OnFontChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AE5AC4599B67DF1D | Method | `protected virtual System.Void OnForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5AE272F6FE993A6A | Method | `protected virtual System.Void OnGiveFeedback(System.Windows.Forms.GiveFeedbackEventArgs gfbevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1CBA2D6C0E3CB68E | Method | `protected virtual System.Void OnGotFocus(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D55C1C4CF4C27BAA | Method | `protected virtual System.Void OnHandleCreated(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B8925C5500657967 | Method | `protected virtual System.Void OnHandleDestroyed(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-235E6ADB7D2FED33 | Method | `protected virtual System.Void OnHelpRequested(System.Windows.Forms.HelpEventArgs hevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5D89B93FA26D74AA | Method | `protected virtual System.Void OnImeModeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-921391E61D101DE8 | Method | `protected virtual System.Void OnInvalidated(System.Windows.Forms.InvalidateEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ADF0AF9FE644DEC0 | Method | `protected internal virtual System.Void OnItemAdded(System.Windows.Forms.ToolStripItemEventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-78BB0B5DF53D7D74 | Method | `protected virtual System.Void OnItemClicked(System.Windows.Forms.ToolStripItemClickedEventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7548E53E18C89BA9 | Method | `protected internal virtual System.Void OnItemRemoved(System.Windows.Forms.ToolStripItemEventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D42CE4F310DB0657 | Method | `protected virtual System.Void OnKeyDown(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E22A3C126F70FCF | Method | `protected virtual System.Void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B6294B33245A9197 | Method | `protected virtual System.Void OnKeyUp(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B6AC50BF63C78531 | Method | `protected virtual System.Void OnLayout(System.Windows.Forms.LayoutEventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-61390A330072F011 | Method | `protected virtual System.Void OnLayoutCompleted(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5F3D039C08476AB2 | Method | `protected virtual System.Void OnLayoutStyleChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-85D83F79D0869026 | Method | `protected internal virtual System.Void OnLeave(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D06BAF146BA9BBC5 | Method | `protected virtual System.Void OnLocationChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4858D89C197452E6 | Method | `protected virtual System.Void OnLostFocus(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A9F2D911049DDCF6 | Method | `protected virtual System.Void OnMarginChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-85E12D22C870710C | Method | `protected virtual System.Void OnMenuActivate(System.EventArgs e)` | `System.Windows.Forms.MenuStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CF4B4572CDF7F92A | Method | `protected virtual System.Void OnMenuDeactivate(System.EventArgs e)` | `System.Windows.Forms.MenuStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-121F4E8786A4113B | Method | `protected virtual System.Void OnMouseCaptureChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F687FB63F2F4359A | Method | `protected virtual System.Void OnMouseClick(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9CEE23F71ABB7408 | Method | `protected virtual System.Void OnMouseDoubleClick(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-31535B251BB93B43 | Method | `protected virtual System.Void OnMouseDown(System.Windows.Forms.MouseEventArgs mea)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C0A45A2E4C7B2F09 | Method | `protected virtual System.Void OnMouseEnter(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9428BF14C8692A76 | Method | `protected virtual System.Void OnMouseHover(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C1838F11051310E | Method | `protected virtual System.Void OnMouseLeave(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-99FA0E5D653D4340 | Method | `protected virtual System.Void OnMouseMove(System.Windows.Forms.MouseEventArgs mea)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7CA533B846B805B3 | Method | `protected virtual System.Void OnMouseUp(System.Windows.Forms.MouseEventArgs mea)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DEEDCB8EC16FEBF2 | Method | `protected virtual System.Void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F196A8822A6FC81D | Method | `protected virtual System.Void OnMove(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4ECED65AD1132903 | Method | `protected virtual System.Void OnNotifyMessage(System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B380577D608C8D32 | Method | `protected virtual System.Void OnPaddingChanged(System.EventArgs e)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-58FA5244FDA8E12D | Method | `protected virtual System.Void OnPaint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C2790307BA62C720 | Method | `protected virtual System.Void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-287A09F83B931FE1 | Method | `protected internal virtual System.Void OnPaintGrip(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A7C2A1F0732F4BE0 | Method | `protected virtual System.Void OnParentBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-25E653E8BE347D00 | Method | `protected virtual System.Void OnParentBackgroundImageChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-15B835364A1B0097 | Method | `protected virtual System.Void OnParentBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B4E7C06B0251C146 | Method | `protected virtual System.Void OnParentChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6E25194BE921C722 | Method | `protected virtual System.Void OnParentCursorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-156023CE413EB56A | Method | `protected virtual System.Void OnParentDataContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-226C87B75B6ED60B | Method | `protected virtual System.Void OnParentEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E6291748CE55F395 | Method | `protected virtual System.Void OnParentFontChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-740A210D59E7990D | Method | `protected virtual System.Void OnParentForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5E39DB362E8BD8C0 | Method | `protected virtual System.Void OnParentRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-72C200B185729C93 | Method | `protected virtual System.Void OnParentVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-06D2CDE4BB5D6C94 | Method | `protected virtual System.Void OnPreviewKeyDown(System.Windows.Forms.PreviewKeyDownEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AE129CB908B4306C | Method | `protected virtual System.Void OnPrint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F2E3EFBB4AE2787A | Method | `protected virtual System.Void OnQueryContinueDrag(System.Windows.Forms.QueryContinueDragEventArgs qcdevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D1ABE77C083315FC | Method | `protected virtual System.Void OnRegionChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A3959C18FEDC9F09 | Method | `protected virtual System.Void OnRendererChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-641E636670526C07 | Method | `protected virtual System.Void OnResize(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BD37111349E9553B | Method | `protected virtual System.Void OnRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FA08A8755FC91E89 | Method | `protected virtual System.Void OnScroll(System.Windows.Forms.ScrollEventArgs se)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-32D521E2965CA38C | Method | `protected virtual System.Void OnSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FCDACB52B58C0387 | Method | `protected virtual System.Void OnStyleChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DA9B74EB95B28C44 | Method | `protected virtual System.Void OnSystemColorsChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-31731ED6EBE857A5 | Method | `protected virtual System.Void OnTabIndexChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-486853C84B67006E | Method | `protected virtual System.Void OnTabStopChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-17BE8D42B0DDC42B | Method | `protected virtual System.Void OnTextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-EE8818CB132BA9D0 | Method | `protected virtual System.Void OnValidated(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-83051353FA399C35 | Method | `protected virtual System.Void OnValidating(System.ComponentModel.CancelEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C075664AC3619B36 | Method | `protected virtual System.Void OnVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C4C0515B358B5FA1 | Method | `protected virtual System.Boolean ProcessCmdKey(ref System.Windows.Forms.Message m, System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.MenuStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5A99B53A6A5B440B | Method | `protected virtual System.Boolean ProcessDialogChar(System.Char charCode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3F82EA3AD5068569 | Method | `protected virtual System.Boolean ProcessDialogKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A5100693021DC5F3 | Method | `protected virtual System.Boolean ProcessKeyEventArgs(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C33A56F8D678093 | Method | `protected internal virtual System.Boolean ProcessKeyMessage(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3865F70DAF577FA7 | Method | `protected virtual System.Boolean ProcessKeyPreview(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3A767DAAD0427933 | Method | `protected internal virtual System.Boolean ProcessMnemonic(System.Char charCode)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F004CB221AE34F91 | Method | `protected System.Void RaiseDragEvent(System.Object key, System.Windows.Forms.DragEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C979E1EB9E95E3C7 | Method | `protected System.Void RaiseKeyEvent(System.Object key, System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8D3CB673D4273F54 | Method | `protected System.Void RaiseMouseEvent(System.Object key, System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-905A4629EB1D12D9 | Method | `protected System.Void RaisePaintEvent(System.Object key, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-22FA9C44B18EB389 | Method | `protected System.Void RecreateHandle()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DD0EDCD86D23A6B4 | Method | `protected static System.Boolean ReflectMessage(System.IntPtr hWnd, ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-78744586CEB844C4 | Method | `protected virtual System.Void RescaleConstantsForDpi(System.Int32 deviceDpiOld, System.Int32 deviceDpiNew)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-00AA61EC36FC96C2 | Method | `protected System.Void ResetMouseEventArgs()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F856B4DF4F00F669 | Method | `protected virtual System.Void RestoreFocus()` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A17A409019470BC | Method | `protected System.Drawing.ContentAlignment RtlTranslateAlignment(System.Drawing.ContentAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-08F52341EEA8BA30 | Method | `protected System.Windows.Forms.HorizontalAlignment RtlTranslateAlignment(System.Windows.Forms.HorizontalAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AD8C8F736BFF1B04 | Method | `protected System.Windows.Forms.LeftRightAlignment RtlTranslateAlignment(System.Windows.Forms.LeftRightAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9DA7BB4F7284A06B | Method | `protected internal System.Drawing.ContentAlignment RtlTranslateContent(System.Drawing.ContentAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6CCF28EDD8A24979 | Method | `protected System.Windows.Forms.HorizontalAlignment RtlTranslateHorizontal(System.Windows.Forms.HorizontalAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-200E5559AC8CA9CC | Method | `protected System.Windows.Forms.LeftRightAlignment RtlTranslateLeftRight(System.Windows.Forms.LeftRightAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7E0C3F83514BA9E7 | Method | `protected virtual System.Void ScaleControl(System.Drawing.SizeF factor, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AF0C964439C4CA00 | Method | `protected virtual System.Void ScaleCore(System.Single dx, System.Single dy)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1AF2389E35768A32 | Method | `protected virtual System.Drawing.Point ScrollToControl(System.Windows.Forms.Control activeControl)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-979A779FFF04B371 | Method | `protected virtual System.Void Select(System.Boolean directed, System.Boolean forward)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C993E9D4D2315BD1 | Method | `protected System.Void SetAutoSizeMode(System.Windows.Forms.AutoSizeMode mode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8B9CDD51D362FFB1 | Method | `protected virtual System.Void SetBoundsCore(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C4EA0D5F10CCABBA | Method | `protected virtual System.Void SetClientSizeCore(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7095E023126F1D24 | Method | `protected virtual System.Void SetDisplayedItems()` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DF09DAB5E522B1A1 | Method | `protected System.Void SetDisplayRectLocation(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-825BE99EC45B007C | Method | `protected internal System.Void SetItemLocation(System.Windows.Forms.ToolStripItem item, System.Drawing.Point location)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4FF7FD3407F59443 | Method | `protected static System.Void SetItemParent(System.Windows.Forms.ToolStripItem item, System.Windows.Forms.ToolStrip parent)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-89F5CEF70D9A5453 | Method | `protected System.Void SetScrollState(System.Int32 bit, System.Boolean value)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E2AA2F534C26D676 | Method | `protected System.Void SetStyle(System.Windows.Forms.ControlStyles flag, System.Boolean value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-18D369CD80EEBDB3 | Method | `protected System.Void SetTopLevel(System.Boolean value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E11AE43FF3A758F7 | Method | `protected virtual System.Void SetVisibleCore(System.Boolean visible)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8C35EAC7F91DC949 | Method | `protected virtual System.Drawing.Size SizeFromClientSize(System.Drawing.Size clientSize)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-91E85ABE1C0CDC4D | Method | `protected System.Void UpdateBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-35FE793B7F1684BD | Method | `protected System.Void UpdateBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Int32 clientWidth, System.Int32 clientHeight)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-017C22A133FBCE84 | Method | `protected internal System.Void UpdateBounds()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B566068ABC645708 | Method | `protected System.Void UpdateStyles()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AA59419425D71408 | Method | `protected System.Void UpdateZOrder()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E41FA11919E93D7B | Method | `protected virtual System.Void WndProc(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.MenuStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-76E15B15D320444C | Property | `virtual System.Boolean CanEnableIme { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B1766A9589BC6165 | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-22597A2579A957A5 | Property | `virtual System.Windows.Forms.CreateParams CreateParams { protected get; }` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B5F32F49FFA7EEC | Property | `virtual System.Windows.Forms.Cursor DefaultCursor { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E8A66D82F12C508E | Property | `virtual System.Windows.Forms.DockStyle DefaultDock { protected get; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6CFEC560E9380401 | Property | `virtual System.Windows.Forms.Padding DefaultGripMargin { protected get; }` | `System.Windows.Forms.MenuStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-745407DC40CDB6BF | Property | `virtual System.Windows.Forms.ImeMode DefaultImeMode { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9BA501229C4D1AAD | Property | `virtual System.Windows.Forms.Padding DefaultMargin { protected get; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1C9231FEF201249E | Property | `virtual System.Drawing.Size DefaultMaximumSize { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5B0DDABBBCF34624 | Property | `virtual System.Drawing.Size DefaultMinimumSize { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DF67E6EE4EBF61CE | Property | `virtual System.Windows.Forms.Padding DefaultPadding { protected get; }` | `System.Windows.Forms.MenuStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0413D2A769177AB8 | Property | `virtual System.Boolean DefaultShowItemToolTips { protected get; }` | `System.Windows.Forms.MenuStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-70882F392EEB04F7 | Property | `virtual System.Drawing.Size DefaultSize { protected get; }` | `System.Windows.Forms.MenuStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-84768BB6B62A55C8 | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F1342121DD34AD25 | Property | `virtual System.Windows.Forms.ToolStripItemCollection DisplayedItems { protected internal get; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FF019F5C4DD99716 | Property | `virtual System.Boolean DoubleBuffered { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D6921236A54C6F96 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3C46C95D21198DA4 | Property | `System.Int32 FontHeight { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5DF46ADC96A7EF6C | Property | `System.Boolean HScroll { protected get; protected set; }` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5495DE2D613E0156 | Property | `virtual System.Windows.Forms.ImeMode ImeModeBase { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BEE19D1E65C762B0 | Property | `virtual System.Drawing.Size MaxItemSize { protected internal get; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1E946B707298991E | Property | `static System.Windows.Forms.ImeMode PropagatingImeMode { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3B666323F69FE821 | Property | `System.Boolean RenderRightToLeft { protected internal get; }` | `System.Windows.Forms.Control` ObsoleteAttribute("This property has been deprecated. Please use RightToLeft instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2C8FCB3C0F17CAB1 | Property | `System.Boolean ResizeRedraw { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-45A8DFEBD95E0127 | Property | `virtual System.Boolean ScaleChildren { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9FAD6DF7C9571354 | Property | `virtual System.Boolean ShowFocusCues { protected internal get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E927B5C48A234AC5 | Property | `virtual System.Boolean ShowKeyboardCues { protected internal get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EE2E945D91AC423E | Property | `System.Boolean VScroll { protected get; protected set; }` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
