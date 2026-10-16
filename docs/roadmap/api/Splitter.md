# Splitter：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/Splitter.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.Splitter`；基类：`System.Windows.Forms.Control`。Lumina 同名类型：🟡 存在；基类及基线接口形状：⬜ 未匹配。

公开属性 **88**，公开事件 **74**，公开方法 **74**；受保护扩展成员 **168**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-3FD0929C320B2884 | Constructor | `public Splitter()` | `System.Windows.Forms.Splitter`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-8D82EB6A1C596A53 | Event | `public event System.EventHandler AutoSizeChanged` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-02518634F2406E29 | Event | `public event System.EventHandler BackColorChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9693ED6E13A17310 | Event | `public event System.EventHandler BackgroundImageChanged` | `System.Windows.Forms.Splitter` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E65848E882CEED95 | Event | `public event System.EventHandler BackgroundImageLayoutChanged` | `System.Windows.Forms.Splitter` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2330E62AA1DF5602 | Event | `public event System.EventHandler BindingContextChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-94B0D0A39B851FEE | Event | `public event System.EventHandler CausesValidationChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0DE689CFA72C5BC2 | Event | `public event System.Windows.Forms.UICuesEventHandler ChangeUICues` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-208BF25F62D705F1 | Event | `public event System.EventHandler Click` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-913B77B695C3EB9B | Event | `public event System.EventHandler ClientSizeChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7795375A2A330A99 | Event | `public event System.EventHandler ContextMenuChanged` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C5D304CA34B343F4 | Event | `public event System.EventHandler ContextMenuStripChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F712EA0B4ED87591 | Event | `public event System.Windows.Forms.ControlEventHandler ControlAdded` | `System.Windows.Forms.Control` BrowsableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A139C5E50383CDFA | Event | `public event System.Windows.Forms.ControlEventHandler ControlRemoved` | `System.Windows.Forms.Control` BrowsableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-501D9A7AA968D75E | Event | `public event System.EventHandler CursorChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C20AF5D4CB595EC3 | Event | `public event System.EventHandler DataContextChanged` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C0040988F6AC28D3 | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CCB3056389D9815E | Event | `public event System.EventHandler DockChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C94CE015C195D2E2 | Event | `public event System.EventHandler DoubleClick` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4C9AB03FCE58861A | Event | `public event System.EventHandler DpiChangedAfterParent` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A4211FFCD30C8EB6 | Event | `public event System.EventHandler DpiChangedBeforeParent` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B766F2D2A4FCBC4C | Event | `public event System.Windows.Forms.DragEventHandler DragDrop` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DFEBC09C4D36AE02 | Event | `public event System.Windows.Forms.DragEventHandler DragEnter` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AE16D9875A17ADD1 | Event | `public event System.EventHandler DragLeave` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6881CB4BD64F92A8 | Event | `public event System.Windows.Forms.DragEventHandler DragOver` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FA03DA9EB51A7F7B | Event | `public event System.EventHandler EnabledChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2796EAEDF9362CDB | Event | `public event System.EventHandler Enter` | `System.Windows.Forms.Splitter` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-11B15E5EBCAF08EB | Event | `public event System.EventHandler FontChanged` | `System.Windows.Forms.Splitter` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3CBC4E2693A30380 | Event | `public event System.EventHandler ForeColorChanged` | `System.Windows.Forms.Splitter` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A3AFB6507F9A0B60 | Event | `public event System.Windows.Forms.GiveFeedbackEventHandler GiveFeedback` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3359CF3DE3079907 | Event | `public event System.EventHandler GotFocus` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-786FB00F7F2C3D1E | Event | `public event System.EventHandler HandleCreated` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-74F513EBBBF34AAE | Event | `public event System.EventHandler HandleDestroyed` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9ECC1C8F40AA98DA | Event | `public event System.Windows.Forms.HelpEventHandler HelpRequested` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4889F8D71AD2B172 | Event | `public event System.EventHandler ImeModeChanged` | `System.Windows.Forms.Splitter` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50B8194B04D23E19 | Event | `public event System.Windows.Forms.InvalidateEventHandler Invalidated` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-50910BE68960ECA0 | Event | `public event System.Windows.Forms.KeyEventHandler KeyDown` | `System.Windows.Forms.Splitter` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-66121CB9158E26E7 | Event | `public event System.Windows.Forms.KeyPressEventHandler KeyPress` | `System.Windows.Forms.Splitter` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0FB37FEE96AEF014 | Event | `public event System.Windows.Forms.KeyEventHandler KeyUp` | `System.Windows.Forms.Splitter` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-03BB4EFB51332DC9 | Event | `public event System.Windows.Forms.LayoutEventHandler Layout` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-DC3F15FC9FC6E5F5 | Event | `public event System.EventHandler Leave` | `System.Windows.Forms.Splitter` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C3F1C249234FA7CA | Event | `public event System.EventHandler LocationChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-11E853E282AF721E | Event | `public event System.EventHandler LostFocus` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CE92AA2C80FFF1DE | Event | `public event System.EventHandler MarginChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-EDF0F990A0B34436 | Event | `public event System.EventHandler MouseCaptureChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-ED3CD43D56D8C2C1 | Event | `public event System.Windows.Forms.MouseEventHandler MouseClick` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-680C2943957AD8F0 | Event | `public event System.Windows.Forms.MouseEventHandler MouseDoubleClick` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-30CAAF390F34CB1C | Event | `public event System.Windows.Forms.MouseEventHandler MouseDown` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2CCE2176981D653F | Event | `public event System.EventHandler MouseEnter` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A71A330E253F924C | Event | `public event System.EventHandler MouseHover` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E99CD95938E93638 | Event | `public event System.EventHandler MouseLeave` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C75F2F6300DC60A1 | Event | `public event System.Windows.Forms.MouseEventHandler MouseMove` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C36D82F6A6055003 | Event | `public event System.Windows.Forms.MouseEventHandler MouseUp` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-8BF911CDE20A8A24 | Event | `public event System.Windows.Forms.MouseEventHandler MouseWheel` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-296A8EA0F4C68B2B | Event | `public event System.EventHandler Move` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B9B7BFA57A595A64 | Event | `public event System.EventHandler PaddingChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6881FEC1D6905176 | Event | `public event System.Windows.Forms.PaintEventHandler Paint` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7CB06FA6A1DC28F0 | Event | `public event System.EventHandler ParentChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1BB84CE6C7D23327 | Event | `public event System.Windows.Forms.PreviewKeyDownEventHandler PreviewKeyDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A773D100436548FA | Event | `public event System.Windows.Forms.QueryAccessibilityHelpEventHandler QueryAccessibilityHelp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2C52C9486F544C29 | Event | `public event System.Windows.Forms.QueryContinueDragEventHandler QueryContinueDrag` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-62AB66935F3CD22E | Event | `public event System.EventHandler RegionChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EDD3AFB96F8B5A18 | Event | `public event System.EventHandler Resize` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FB5C5E3431AF9E33 | Event | `public event System.EventHandler RightToLeftChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8082FBE526A415F0 | Event | `public event System.EventHandler SizeChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4C3C575C25DACE25 | Event | `public event System.Windows.Forms.SplitterEventHandler SplitterMoved` | `System.Windows.Forms.Splitter`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F9184A12FCB190CC | Event | `public event System.Windows.Forms.SplitterEventHandler SplitterMoving` | `System.Windows.Forms.Splitter`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0570D69ED86E51C8 | Event | `public event System.EventHandler StyleChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-450AB0809967320C | Event | `public event System.EventHandler SystemColorsChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9DE5FB223DE28B31 | Event | `public event System.EventHandler TabIndexChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B080303B28B2D79 | Event | `public event System.EventHandler TabStopChanged` | `System.Windows.Forms.Splitter` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-68AF6AB60D5A80BE | Event | `public event System.EventHandler TextChanged` | `System.Windows.Forms.Splitter` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E5E4688E52A08219 | Event | `public event System.EventHandler Validated` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-92D3825D8F3BEAC6 | Event | `public event System.ComponentModel.CancelEventHandler Validating` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A67A15FE2197A985 | Event | `public event System.EventHandler VisibleChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2D49AF67B3943C55 | Method | `public System.IAsyncResult BeginInvoke(System.Action method)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F7DE30DA891566D2 | Method | `public System.IAsyncResult BeginInvoke(System.Delegate method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-32C7C2E25E4D0DC6 | Method | `public System.IAsyncResult BeginInvoke(System.Delegate method, System.Object[] args)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-BF80507923AD4069 | Method | `public System.Void BringToFront()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-73A99FBC0E0814CD | Method | `public System.Boolean Contains(System.Windows.Forms.Control ctl)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E6BC414D3298269E | Method | `public System.Void CreateControl()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9B335E4AC355FABA | Method | `public System.Drawing.Graphics CreateGraphics()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-639C47E87BB78258 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5B99D6BCD39BE1DE | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8E5978D73A0278DD | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4B00C0E6D673D7D9 | Method | `public System.Windows.Forms.DragDropEffects DoDragDropAsJson<!!0>(!!0 data, System.Windows.Forms.DragDropEffects allowedEffects) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-46545478290C8C7F | Method | `public System.Windows.Forms.DragDropEffects DoDragDropAsJson<!!0>(!!0 data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-253A7E4F3F810F58 | Method | `public System.Void DrawToBitmap(System.Drawing.Bitmap bitmap, System.Drawing.Rectangle targetBounds)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-60D65290FF9F8C38 | Method | `public System.Object EndInvoke(System.IAsyncResult asyncResult)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FE55AD19BCF40EAE | Method | `public System.Windows.Forms.Form FindForm()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-33B44F74F82718B8 | Method | `public System.Boolean Focus()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-81F6D51DABA6A816 | Method | `public static System.Windows.Forms.Control FromChildHandle(System.IntPtr handle)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B484233BFAD2531D | Method | `public static System.Windows.Forms.Control FromHandle(System.IntPtr handle)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-8969C752E4A2EE36 | Method | `public System.Windows.Forms.Control GetChildAtPoint(System.Drawing.Point pt)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B099F1FB97616D5 | Method | `public System.Windows.Forms.Control GetChildAtPoint(System.Drawing.Point pt, System.Windows.Forms.GetChildAtPointSkip skipValue)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-603C7A45099B3E30 | Method | `public System.Windows.Forms.IContainerControl GetContainerControl()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FD378A38B617AFE7 | Method | `public System.Windows.Forms.Control GetNextControl(System.Windows.Forms.Control ctl, System.Boolean forward)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F1A2FBC1019F6B60 | Method | `public virtual System.Drawing.Size GetPreferredSize(System.Drawing.Size proposedSize)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F098CE14AC447541 | Method | `public System.Void Hide()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5E33F26952847AAB | Method | `public System.Void Invalidate()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0B22E17F547249B3 | Method | `public System.Void Invalidate(System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5D21C136C76F2FB6 | Method | `public System.Void Invalidate(System.Drawing.Rectangle rc)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-BF00BE3CB9831903 | Method | `public System.Void Invalidate(System.Drawing.Rectangle rc, System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A0E59F1069568F72 | Method | `public System.Void Invalidate(System.Drawing.Region region)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4B1FE8453BC5B3FA | Method | `public System.Void Invalidate(System.Drawing.Region region, System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A72C69BF56575661 | Method | `public !!0 Invoke<!!0>(System.Func&#96;1<!!0> method) where !!0: None;` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6C698A16DED0A91D | Method | `public System.Object Invoke(System.Delegate method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9ACD4C24CDECA798 | Method | `public System.Object Invoke(System.Delegate method, System.Object[] args)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5A13DBCCC82875AA | Method | `public System.Void Invoke(System.Action method)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6CA0C2B2259E5D84 | Method | `public System.Threading.Tasks.Task InvokeAsync(System.Action callback, System.Threading.CancellationToken cancellationToken = null)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C7172AE5E876F74C | Method | `public System.Threading.Tasks.Task InvokeAsync(System.Func&#96;2<System.Threading.CancellationToken,System.Threading.Tasks.ValueTask> callback, System.Threading.CancellationToken cancellationToken = null)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-63969698900C665E | Method | `public System.Threading.Tasks.Task&#96;1<!!0> InvokeAsync<!!0>(System.Func&#96;1<!!0> callback, System.Threading.CancellationToken cancellationToken = null) where !!0: None;` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-21D7974D973E7261 | Method | `public System.Threading.Tasks.Task&#96;1<!!0> InvokeAsync<!!0>(System.Func&#96;2<System.Threading.CancellationToken,System.Threading.Tasks.ValueTask&#96;1<!!0>> callback, System.Threading.CancellationToken cancellationToken = null) where !!0: None;` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0647BAA14420DB8B | Method | `public static System.Boolean IsKeyLocked(System.Windows.Forms.Keys keyVal)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E7577217AAFD569C | Method | `public static System.Boolean IsMnemonic(System.Char charCode, System.String text)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4DD7E9F359C063A3 | Method | `public System.Drawing.Size LogicalToDeviceUnits(System.Drawing.Size value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-08BE0FF7B3588DE4 | Method | `public System.Int32 LogicalToDeviceUnits(System.Int32 value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8ECB4F078F81FEBA | Method | `public System.Void PerformLayout()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0BC35F4BDC54E272 | Method | `public System.Void PerformLayout(System.Windows.Forms.Control affectedControl, System.String affectedProperty)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-8BB99E6E38A041AA | Method | `public System.Drawing.Point PointToClient(System.Drawing.Point p)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-556F2B85EE3EE7C2 | Method | `public System.Drawing.Point PointToScreen(System.Drawing.Point p)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-05EE00955A07E5B1 | Method | `public System.Windows.Forms.PreProcessControlState PreProcessControlMessage(ref System.Windows.Forms.Message msg)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F419E034CBC854B9 | Method | `public virtual System.Boolean PreProcessMessage(ref System.Windows.Forms.Message msg)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DE6BECCAD7300E7F | Method | `public System.Drawing.Rectangle RectangleToClient(System.Drawing.Rectangle r)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6A0578D4A52A05E1 | Method | `public System.Drawing.Rectangle RectangleToScreen(System.Drawing.Rectangle r)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D0F66AEFBE834264 | Method | `public virtual System.Void Refresh()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0CA9FA6A32FE19C6 | Method | `public virtual System.Void ResetBackColor()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-05E3D6C5B3E0C07E | Method | `public System.Void ResetBindings()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-321DD421EBE4A62D | Method | `public virtual System.Void ResetCursor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-881EA715F98E6BB2 | Method | `public virtual System.Void ResetFont()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7B1A3253106B1CFD | Method | `public virtual System.Void ResetForeColor()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F73B102827EDE2DC | Method | `public System.Void ResetImeMode()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4A7C0E1228800AD5 | Method | `public virtual System.Void ResetRightToLeft()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D82DF4499831AB6B | Method | `public virtual System.Void ResetText()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4CD6D5DE6657663B | Method | `public System.Void ResumeLayout()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-794D9F00FF581B39 | Method | `public System.Void ResumeLayout(System.Boolean performLayout)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-93B85D5FF8AC0545 | Method | `public System.Void Scale(System.Drawing.SizeF factor)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-42DB22C3D8175A41 | Method | `public System.Void Scale(System.Single dx, System.Single dy)` | `System.Windows.Forms.Control` ObsoleteAttribute("This method has been deprecated. Use the Scale(SizeF ratio) method instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C38FE789E92BBE07 | Method | `public System.Void Scale(System.Single ratio)` | `System.Windows.Forms.Control` ObsoleteAttribute("This method has been deprecated. Use the Scale(SizeF ratio) method instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC12770CA29EF318 | Method | `public System.Void ScaleBitmapLogicalToDevice(ref System.Drawing.Bitmap logicalBitmap)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-215C6AE8FDD7867F | Method | `public System.Void Select()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8C42A699B5BC77D5 | Method | `public System.Boolean SelectNextControl(System.Windows.Forms.Control ctl, System.Boolean forward, System.Boolean tabStopOnly, System.Boolean nested, System.Boolean wrap)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EE4D95375D0213D3 | Method | `public System.Void SendToBack()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-67883DFD90E43D72 | Method | `public System.Void SetBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E7F1805E825E6000 | Method | `public System.Void SetBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B8552809F60FD76E | Method | `public System.Void Show()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-672EAC03A83474A4 | Method | `public System.Void SuspendLayout()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A306B987772FC617 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.Splitter`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-AA153DBAD8CE95B5 | Method | `public System.Void Update()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-59C7F1620D6892F7 | Property | `System.Windows.Forms.AccessibleObject AccessibilityObject { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E059F5871FD930CD | Property | `System.String AccessibleDefaultActionDescription { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F7E597E72C9304BC | Property | `System.String AccessibleDescription { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E2E988A08DCA38CE | Property | `System.String AccessibleName { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B4B3A9802B04B8D | Property | `System.Windows.Forms.AccessibleRole AccessibleRole { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(-1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6B7661869AB1B772 | Property | `virtual System.Boolean AllowDrop { public get; public set; }` | `System.Windows.Forms.Splitter` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-583B84E020A4FEA2 | Property | `virtual System.Windows.Forms.AnchorStyles Anchor { public get; public set; }` | `System.Windows.Forms.Splitter` BrowsableAttribute(false); DefaultValueAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B646B42EADE07FFB | Property | `virtual System.Drawing.Point AutoScrollOffset { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DefaultValueAttribute(System.Drawing.Point,"0, 0") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-703F38F044557B6D | Property | `virtual System.Boolean AutoSize { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true); DefaultValueAttribute(false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9CFB8B1B8565EB84 | Property | `virtual System.Drawing.Color BackColor { public get; public set; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4CEDD3A624AB5BCB | Property | `virtual System.Drawing.Image BackgroundImage { public get; public set; }` | `System.Windows.Forms.Splitter` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-930A9BED5360BAB3 | Property | `virtual System.Windows.Forms.ImageLayout BackgroundImageLayout { public get; public set; }` | `System.Windows.Forms.Splitter` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3B16643D1BE31DB1 | Property | `virtual System.Windows.Forms.BindingContext BindingContext { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F167252E1A0D2FDC | Property | `System.Windows.Forms.BorderStyle BorderStyle { public get; public set; }` | `System.Windows.Forms.Splitter` DefaultValueAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2633EDF8085518E6 | Property | `System.Int32 Bottom { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-045FDAF49158C0DB | Property | `System.Drawing.Rectangle Bounds { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C60C9376279F7A3F | Property | `System.Boolean CanFocus { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-EC21F1E67CF65B12 | Property | `System.Boolean CanSelect { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-86A00B6F85A87CAB | Property | `System.Boolean Capture { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1FD13D94F59BE114 | Property | `System.Boolean CausesValidation { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9544F0159B8EBEBA | Property | `static System.Boolean CheckForIllegalCrossThreadCalls { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8DD72D74C0D3A3A9 | Property | `System.Drawing.Rectangle ClientRectangle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-693DC4E3F63B2AA7 | Property | `System.Drawing.Size ClientSize { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FFD72BD67F72E8F5 | Property | `System.String CompanyName { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6C3BE1386F4F1E95 | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9BFD7DA5FD14F54A | Property | `System.Boolean ContainsFocus { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CD0551A34A993995 | Property | `virtual System.Windows.Forms.ContextMenu ContextMenu { public get; public set; }` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-72AB487CA7DF4D15 | Property | `virtual System.Windows.Forms.ContextMenuStrip ContextMenuStrip { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F5D972A0367D0695 | Property | `System.Windows.Forms.Control+ControlCollection Controls { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(2) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-28509ECE04B34646 | Property | `System.Boolean Created { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-8AFB2EDDC686F3C5 | Property | `virtual System.Windows.Forms.Cursor Cursor { public get; public set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A7FB1E1AB78ECC3 | Property | `System.Windows.Forms.ControlBindingsCollection DataBindings { public get; }` | `System.Windows.Forms.Control` DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B9E1870D477A13B2 | Property | `virtual System.Object DataContext { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); BindableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E055BA5FE8C27565 | Property | `static System.Drawing.Color DefaultBackColor { public get; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-BDD9697B26C97868 | Property | `static System.Drawing.Font DefaultFont { public get; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CA5387F731CB1669 | Property | `static System.Drawing.Color DefaultForeColor { public get; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CFEC2BBFA1CC3AAC | Property | `System.Int32 DeviceDpi { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC0E94293141FC76 | Property | `virtual System.Drawing.Rectangle DisplayRectangle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CF51FD6E2B9BD2AB | Property | `System.Boolean Disposing { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5F926D00EEF6B8C5 | Property | `virtual System.Windows.Forms.DockStyle Dock { public get; public set; }` | `System.Windows.Forms.Splitter` LocalizableAttribute(true); DefaultValueAttribute(3) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-DF42BDF0AB3ED1B7 | Property | `System.Boolean Enabled { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-429EE557699F098F | Property | `virtual System.Boolean Focused { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2A7F30EE3ED53211 | Property | `virtual System.Drawing.Font Font { public get; public set; }` | `System.Windows.Forms.Splitter` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FD8CE4E1D6AE83F1 | Property | `virtual System.Drawing.Color ForeColor { public get; public set; }` | `System.Windows.Forms.Splitter` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A8DE66CEE149A567 | Property | `System.IntPtr Handle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1724F9F345F3C00D | Property | `System.Boolean HasChildren { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F6FB8BA31B2FFF73 | Property | `System.Int32 Height { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-AE6A97B8B2EF95F4 | Property | `System.Windows.Forms.ImeMode ImeMode { public get; public set; }` | `System.Windows.Forms.Splitter` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8E58A08851F841F7 | Property | `System.Boolean InvokeRequired { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-79F696D4D4875968 | Property | `System.Boolean IsAccessible { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5286B74B01B53DBA | Property | `System.Boolean IsAncestorSiteInDesignMode { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E518E65FA1C12E73 | Property | `System.Boolean IsDisposed { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D4E476971712AE8A | Property | `System.Boolean IsHandleCreated { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D11807138F472BD5 | Property | `System.Boolean IsMirrored { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-808A587E6700241D | Property | `virtual System.Windows.Forms.Layout.LayoutEngine LayoutEngine { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-03712AE51979AC3D | Property | `System.Int32 Left { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-AFAF832419C5C0F2 | Property | `System.Drawing.Point Location { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-8FE3CBB299579B2E | Property | `System.Windows.Forms.Padding Margin { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E6E12C2C6267BD0E | Property | `virtual System.Drawing.Size MaximumSize { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4A48A7A24E1689D8 | Property | `System.Int32 MinExtra { public get; public set; }` | `System.Windows.Forms.Splitter` LocalizableAttribute(true); DefaultValueAttribute(25) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C47932649592920B | Property | `virtual System.Drawing.Size MinimumSize { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2F952798E6208E68 | Property | `System.Int32 MinSize { public get; public set; }` | `System.Windows.Forms.Splitter` LocalizableAttribute(true); DefaultValueAttribute(25) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0FCA56B7EA3D6015 | Property | `static System.Windows.Forms.Keys ModifierKeys { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-773622E49FF140FF | Property | `static System.Windows.Forms.MouseButtons MouseButtons { public get; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0BA6D6ABDD867ADE | Property | `static System.Drawing.Point MousePosition { public get; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1BCC0874FF1F4FD8 | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1C7C7608EFB2A9F1 | Property | `System.Windows.Forms.Padding Padding { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F2B51FCF6431E0DE | Property | `System.Windows.Forms.Control Parent { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2EBA7D7FA575CDE7 | Property | `System.Drawing.Size PreferredSize { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CBFD1287CCC66803 | Property | `System.String ProductName { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-17303610B41BBF7B | Property | `System.String ProductVersion { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BAC46731CB6E37A9 | Property | `System.Boolean RecreatingHandle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-530FCC14B9C1E915 | Property | `System.Drawing.Region Region { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6ADC72BDF7A3E807 | Property | `System.Int32 Right { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9EF36429BC569DB2 | Property | `virtual System.Windows.Forms.RightToLeft RightToLeft { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0709FFCCDB03000C | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B10E55D2A675A0AA | Property | `System.Drawing.Size Size { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9CE3DCB705F6D095 | Property | `System.Int32 SplitPosition { public get; public set; }` | `System.Windows.Forms.Splitter` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0845FECBB8C9F207 | Property | `System.Int32 TabIndex { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-92C01381BAADFDD0 | Property | `System.Boolean TabStop { public get; public set; }` | `System.Windows.Forms.Splitter` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B147D17C8DD3DCCF | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A39EB41BD4F3E43C | Property | `virtual System.String Text { public get; public set; }` | `System.Windows.Forms.Splitter` BrowsableAttribute(false); BindableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CE8B76154DF7F663 | Property | `System.Int32 Top { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-284CCB868A35E215 | Property | `System.Windows.Forms.Control TopLevelControl { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3E06D4FAFEE22FA4 | Property | `System.Boolean UseWaitCursor { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(false); BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-005C39C4175B7DD4 | Property | `System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A1DC35B4BCA8E084 | Property | `System.Int32 Width { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F41EA1123CC57BB1 | Property | `System.Windows.Forms.IWindowTarget WindowTarget { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-D62A8DF3526DFF37 | Method | `protected System.Void AccessibilityNotifyClients(System.Windows.Forms.AccessibleEvents accEvent, System.Int32 objectID, System.Int32 childID)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-07C8BBBB8891E9F4 | Method | `protected internal System.Void AccessibilityNotifyClients(System.Windows.Forms.AccessibleEvents accEvent, System.Int32 childID)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D3C2803C5B177B15 | Method | `protected virtual System.Windows.Forms.AccessibleObject CreateAccessibilityInstance()` | `System.Windows.Forms.Splitter`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-307E940BF7E8AC01 | Method | `protected virtual System.Windows.Forms.Control+ControlCollection CreateControlsInstance()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2B3B32CD6A70DCB7 | Method | `protected virtual System.Void CreateHandle()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-EAC3D323749CCA7B | Method | `protected virtual System.Void DefWndProc(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4C95AB221507F83B | Method | `protected virtual System.Void DestroyHandle()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-36757AEC42F7590F | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F0D103BC8BCF624B | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-409F1C9DFDA920CB | Method | `protected virtual System.Windows.Forms.AccessibleObject GetAccessibilityObjectById(System.Int32 objectId)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E11D9F7B8F98295D | Method | `protected System.Windows.Forms.AutoSizeMode GetAutoSizeMode()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-BE21F70D23EC30A1 | Method | `protected virtual System.Drawing.Rectangle GetScaledBounds(System.Drawing.Rectangle bounds, System.Drawing.SizeF factor, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7228F7E0E8B23D0F | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B8659CB8A5167859 | Method | `protected System.Boolean GetStyle(System.Windows.Forms.ControlStyles flag)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2EAF00D159104F36 | Method | `protected System.Boolean GetTopLevel()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6D2EBF29131665A0 | Method | `protected virtual System.Void InitLayout()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4226587A162CDA12 | Method | `protected System.Void InvokeGotFocus(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DE533B611833AFB7 | Method | `protected System.Void InvokeLostFocus(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-64028D998A625DD5 | Method | `protected System.Void InvokeOnClick(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7350097DBD6A23C3 | Method | `protected System.Void InvokePaint(System.Windows.Forms.Control c, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4540199AEB78B3D2 | Method | `protected System.Void InvokePaintBackground(System.Windows.Forms.Control c, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-00E32BD69CF70E26 | Method | `protected virtual System.Boolean IsInputChar(System.Char charCode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E50D367A0A1D5E3C | Method | `protected virtual System.Boolean IsInputKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B4462AC82B45DC5D | Method | `protected virtual System.Void NotifyInvalidate(System.Drawing.Rectangle invalidatedArea)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F0CAD82993127A5C | Method | `protected virtual System.Void OnAutoSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CDB6C2AF6E59D38B | Method | `protected virtual System.Void OnBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-547884B3B25F4CA1 | Method | `protected virtual System.Void OnBackgroundImageChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B0A4CED9D2A9C721 | Method | `protected virtual System.Void OnBackgroundImageLayoutChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CB9B1AFC4A3387BC | Method | `protected virtual System.Void OnBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FDDEEF8DD16122CC | Method | `protected virtual System.Void OnCausesValidationChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E67CF1F136297652 | Method | `protected virtual System.Void OnChangeUICues(System.Windows.Forms.UICuesEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-603CF4DF9CEAFE79 | Method | `protected virtual System.Void OnClick(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-63E2A2F34AA77309 | Method | `protected virtual System.Void OnClientSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B3B69A245F3D5535 | Method | `protected virtual System.Void OnContextMenuChanged(System.EventArgs e)` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A624850430F18D1E | Method | `protected virtual System.Void OnContextMenuStripChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4B7A40C2430CFFBF | Method | `protected virtual System.Void OnControlAdded(System.Windows.Forms.ControlEventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-22774FA256BEA0F2 | Method | `protected virtual System.Void OnControlRemoved(System.Windows.Forms.ControlEventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-21D3700C7040831D | Method | `protected virtual System.Void OnCreateControl()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7F000EC77EED4544 | Method | `protected virtual System.Void OnCursorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A50CEB2AD4FEECE0 | Method | `protected virtual System.Void OnDataContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7BC44EC082A6E129 | Method | `protected virtual System.Void OnDockChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5B25EAD63B65B613 | Method | `protected virtual System.Void OnDoubleClick(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-99D09CE47D3F15F1 | Method | `protected virtual System.Void OnDpiChangedAfterParent(System.EventArgs e)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4A7BEB58155389B3 | Method | `protected virtual System.Void OnDpiChangedBeforeParent(System.EventArgs e)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5793494B23018565 | Method | `protected virtual System.Void OnDragDrop(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D528E797E00C30EE | Method | `protected virtual System.Void OnDragEnter(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6273880FF5D76E03 | Method | `protected virtual System.Void OnDragLeave(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D2921C0D197F4421 | Method | `protected virtual System.Void OnDragOver(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C71896DA59D626FC | Method | `protected virtual System.Void OnEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7825B01C2C277C12 | Method | `protected internal virtual System.Void OnEnter(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A8DC80F53CF5BEF4 | Method | `protected virtual System.Void OnFontChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3806DEB9E2396E5C | Method | `protected virtual System.Void OnForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A10F1DB420D8A245 | Method | `protected virtual System.Void OnGiveFeedback(System.Windows.Forms.GiveFeedbackEventArgs gfbevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-992CA53E09BBDF2D | Method | `protected virtual System.Void OnGotFocus(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F2B5FA3F789EADC0 | Method | `protected virtual System.Void OnHandleCreated(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9A7A345964D23696 | Method | `protected virtual System.Void OnHandleDestroyed(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C3026A23DDC589B2 | Method | `protected virtual System.Void OnHelpRequested(System.Windows.Forms.HelpEventArgs hevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E96DD7BD8463B62 | Method | `protected virtual System.Void OnImeModeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6E25DF81AE919DDB | Method | `protected virtual System.Void OnInvalidated(System.Windows.Forms.InvalidateEventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-31151016CD9A2762 | Method | `protected virtual System.Void OnKeyDown(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Splitter`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1F5CD4018E031DBA | Method | `protected virtual System.Void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9BD38DAF402039F3 | Method | `protected virtual System.Void OnKeyUp(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DD064B6BB3129B65 | Method | `protected virtual System.Void OnLayout(System.Windows.Forms.LayoutEventArgs levent)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-BC5EC16C99492365 | Method | `protected internal virtual System.Void OnLeave(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-52FAB097B6596396 | Method | `protected virtual System.Void OnLocationChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-BA153E4C35218330 | Method | `protected virtual System.Void OnLostFocus(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6A095FB1A287BF1F | Method | `protected virtual System.Void OnMarginChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-38425A2B077E99AE | Method | `protected virtual System.Void OnMouseCaptureChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7118E78FAAF3E468 | Method | `protected virtual System.Void OnMouseClick(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FE73DB9D52DD891E | Method | `protected virtual System.Void OnMouseDoubleClick(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-067B73C1605CDD63 | Method | `protected virtual System.Void OnMouseDown(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Splitter`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-EE234B843D4111A7 | Method | `protected virtual System.Void OnMouseEnter(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-014E83D142B59F9B | Method | `protected virtual System.Void OnMouseHover(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3E06C375C82F262B | Method | `protected virtual System.Void OnMouseLeave(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-600CA406094F5CA7 | Method | `protected virtual System.Void OnMouseMove(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Splitter`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4B47EDF7E14CA756 | Method | `protected virtual System.Void OnMouseUp(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Splitter`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F7F3934B6A6B81EE | Method | `protected virtual System.Void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F828746D23CD852A | Method | `protected virtual System.Void OnMove(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E9E98183BD5A4DCD | Method | `protected virtual System.Void OnNotifyMessage(System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B6B652E4372CFD78 | Method | `protected virtual System.Void OnPaddingChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-853DF16BEEF1FFCF | Method | `protected virtual System.Void OnPaint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-43BEDE6E4863AB58 | Method | `protected virtual System.Void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-EBA3E07FABA40856 | Method | `protected virtual System.Void OnParentBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-943785B0F6FC4EB2 | Method | `protected virtual System.Void OnParentBackgroundImageChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7156407BD75B5D3A | Method | `protected virtual System.Void OnParentBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D15DB4F139C5AC04 | Method | `protected virtual System.Void OnParentChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3F3574543C65332C | Method | `protected virtual System.Void OnParentCursorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-59433EBB1F6F142B | Method | `protected virtual System.Void OnParentDataContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D513106727C30E54 | Method | `protected virtual System.Void OnParentEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-962AC28B5BC7D766 | Method | `protected virtual System.Void OnParentFontChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E7E938531B4B42CD | Method | `protected virtual System.Void OnParentForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E8AE2510B970BA56 | Method | `protected virtual System.Void OnParentRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FF29675C1CF7C369 | Method | `protected virtual System.Void OnParentVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0CB304FD5E852908 | Method | `protected virtual System.Void OnPreviewKeyDown(System.Windows.Forms.PreviewKeyDownEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B4948BAB51C701CE | Method | `protected virtual System.Void OnPrint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9A772BD9B2A9032F | Method | `protected virtual System.Void OnQueryContinueDrag(System.Windows.Forms.QueryContinueDragEventArgs qcdevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7BFAC48937735FD2 | Method | `protected virtual System.Void OnRegionChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DFEA44A9BDB0F3B3 | Method | `protected virtual System.Void OnResize(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0109A2D992C7B04C | Method | `protected virtual System.Void OnRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A411D3BF737E1A3C | Method | `protected virtual System.Void OnSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-97DA01351F2FD5E0 | Method | `protected virtual System.Void OnSplitterMoved(System.Windows.Forms.SplitterEventArgs sevent)` | `System.Windows.Forms.Splitter`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-94AB3CC9A8FD267F | Method | `protected virtual System.Void OnSplitterMoving(System.Windows.Forms.SplitterEventArgs sevent)` | `System.Windows.Forms.Splitter`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A06B42445BA9AA02 | Method | `protected virtual System.Void OnStyleChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-AD91941E06500214 | Method | `protected virtual System.Void OnSystemColorsChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D401977C7D627AF0 | Method | `protected virtual System.Void OnTabIndexChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC39AB28B3EDC146 | Method | `protected virtual System.Void OnTabStopChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E560E539862AB87E | Method | `protected virtual System.Void OnTextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-39F48C9AD148E52C | Method | `protected virtual System.Void OnValidated(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E18318C27D0D6CAA | Method | `protected virtual System.Void OnValidating(System.ComponentModel.CancelEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-42E5FE5F0CBAACA3 | Method | `protected virtual System.Void OnVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-35EF3B44B26B2ED6 | Method | `protected virtual System.Boolean ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F322B379C1E7A025 | Method | `protected virtual System.Boolean ProcessDialogChar(System.Char charCode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-484B88E9ADA42B90 | Method | `protected virtual System.Boolean ProcessDialogKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A4A637B0807145B8 | Method | `protected virtual System.Boolean ProcessKeyEventArgs(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-19793529B05125FE | Method | `protected internal virtual System.Boolean ProcessKeyMessage(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0DF41C136FEBDEE2 | Method | `protected virtual System.Boolean ProcessKeyPreview(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E6A41F8B5C62D8C5 | Method | `protected internal virtual System.Boolean ProcessMnemonic(System.Char charCode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6B81CD32E69B53F0 | Method | `protected System.Void RaiseDragEvent(System.Object key, System.Windows.Forms.DragEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5F517E87E1A3402D | Method | `protected System.Void RaiseKeyEvent(System.Object key, System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F8C61D2ABE2F1D14 | Method | `protected System.Void RaiseMouseEvent(System.Object key, System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-61F4EBEF42483449 | Method | `protected System.Void RaisePaintEvent(System.Object key, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-159A1FE57A1334DE | Method | `protected System.Void RecreateHandle()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-41E49AB5045C2DFB | Method | `protected static System.Boolean ReflectMessage(System.IntPtr hWnd, ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0228D4F2DC8F869 | Method | `protected virtual System.Void RescaleConstantsForDpi(System.Int32 deviceDpiOld, System.Int32 deviceDpiNew)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0D70201FFFB3EBAF | Method | `protected System.Void ResetMouseEventArgs()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2EF6F6447BEC5A59 | Method | `protected System.Drawing.ContentAlignment RtlTranslateAlignment(System.Drawing.ContentAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-24CC40E833A3386F | Method | `protected System.Windows.Forms.HorizontalAlignment RtlTranslateAlignment(System.Windows.Forms.HorizontalAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B2F0A1786FF28CB4 | Method | `protected System.Windows.Forms.LeftRightAlignment RtlTranslateAlignment(System.Windows.Forms.LeftRightAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7B655FF72D7D0672 | Method | `protected internal System.Drawing.ContentAlignment RtlTranslateContent(System.Drawing.ContentAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C5F63D5736039F91 | Method | `protected System.Windows.Forms.HorizontalAlignment RtlTranslateHorizontal(System.Windows.Forms.HorizontalAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0AD4B67DC2E5A583 | Method | `protected System.Windows.Forms.LeftRightAlignment RtlTranslateLeftRight(System.Windows.Forms.LeftRightAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9DF742D57A86F8A7 | Method | `protected virtual System.Void ScaleControl(System.Drawing.SizeF factor, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2C14BE79FDDDDF10 | Method | `protected virtual System.Void ScaleCore(System.Single dx, System.Single dy)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D18A1E9E510A79C | Method | `protected virtual System.Void Select(System.Boolean directed, System.Boolean forward)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B8D6658C1FE23390 | Method | `protected System.Void SetAutoSizeMode(System.Windows.Forms.AutoSizeMode mode)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FC55EE2FD4C75D8F | Method | `protected virtual System.Void SetBoundsCore(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Splitter`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FD1D4283C13570DD | Method | `protected virtual System.Void SetClientSizeCore(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2707B74483D764E9 | Method | `protected System.Void SetStyle(System.Windows.Forms.ControlStyles flag, System.Boolean value)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FAE7BF05FB0045D7 | Method | `protected System.Void SetTopLevel(System.Boolean value)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A25BFAA9F0449518 | Method | `protected virtual System.Void SetVisibleCore(System.Boolean value)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9F7B106C0D51E1E2 | Method | `protected virtual System.Drawing.Size SizeFromClientSize(System.Drawing.Size clientSize)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FA33C5351C6D721F | Method | `protected System.Void UpdateBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BAD988828E6753C9 | Method | `protected System.Void UpdateBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Int32 clientWidth, System.Int32 clientHeight)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B51DAD4A2ED49EC1 | Method | `protected internal System.Void UpdateBounds()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C37867ACB0A5872C | Method | `protected System.Void UpdateStyles()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B323ABF9DFCB30AB | Method | `protected System.Void UpdateZOrder()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D326563E8024616D | Method | `protected virtual System.Void WndProc(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9397E37D441C4E05 | Property | `virtual System.Boolean CanEnableIme { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1DFD2839DCFADBA6 | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F1D88CEF4C875563 | Property | `virtual System.Windows.Forms.CreateParams CreateParams { protected get; }` | `System.Windows.Forms.Splitter`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6023D2E18D37D7DE | Property | `virtual System.Windows.Forms.Cursor DefaultCursor { protected get; }` | `System.Windows.Forms.Splitter`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-24844D9AF2818E2F | Property | `virtual System.Windows.Forms.ImeMode DefaultImeMode { protected get; }` | `System.Windows.Forms.Splitter`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FE064D2A5964DF7C | Property | `virtual System.Windows.Forms.Padding DefaultMargin { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-937880A18FC086EA | Property | `virtual System.Drawing.Size DefaultMaximumSize { protected get; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-BDAFDAD886AC83D2 | Property | `virtual System.Drawing.Size DefaultMinimumSize { protected get; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-EBBF2F468A4A1038 | Property | `virtual System.Windows.Forms.Padding DefaultPadding { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E1275F09919D3E7 | Property | `virtual System.Drawing.Size DefaultSize { protected get; }` | `System.Windows.Forms.Splitter`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3D2A5A342A3A735E | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6E3E98DAF544EA33 | Property | `virtual System.Boolean DoubleBuffered { protected get; protected set; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A210C4AB4C6E7103 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-8EC317B4E76A8E89 | Property | `System.Int32 FontHeight { protected get; protected set; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A718A993592EACE7 | Property | `virtual System.Windows.Forms.ImeMode ImeModeBase { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-996D599CD324462E | Property | `static System.Windows.Forms.ImeMode PropagatingImeMode { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6C1A24410F19EF3C | Property | `System.Boolean RenderRightToLeft { protected internal get; }` | `System.Windows.Forms.Control` ObsoleteAttribute("This property has been deprecated. Please use RightToLeft instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B4106208EF561791 | Property | `System.Boolean ResizeRedraw { protected get; protected set; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9D35D699E4BF4819 | Property | `virtual System.Boolean ScaleChildren { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C7787165A6B5878C | Property | `virtual System.Boolean ShowFocusCues { protected internal get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-830A7854DB9223F5 | Property | `virtual System.Boolean ShowKeyboardCues { protected internal get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
