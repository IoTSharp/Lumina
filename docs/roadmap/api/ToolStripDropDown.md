# ToolStripDropDown：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/ToolStripDropDown.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.ToolStripDropDown`；基类：`System.Windows.Forms.ToolStrip`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **121**，公开事件 **86**，公开方法 **88**；受保护扩展成员 **204**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-E2107D4B7430B890 | Constructor | `public ToolStripDropDown()` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A23831FA4C718DA1 | Event | `public event System.EventHandler AutoSizeChanged` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5C1206E123CFB925 | Event | `public event System.EventHandler BackColorChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-10EF961BC3AA6EA8 | Event | `public event System.EventHandler BackgroundImageChanged` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-73AB3B813A222AB1 | Event | `public event System.EventHandler BackgroundImageLayoutChanged` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C14DAA6498EC017D | Event | `public event System.EventHandler BeginDrag` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-831164D13C97D496 | Event | `public event System.EventHandler BindingContextChanged` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E6853E1F1DED0A77 | Event | `public event System.EventHandler CausesValidationChanged` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-45DB51CA7BB06FD9 | Event | `public event System.Windows.Forms.UICuesEventHandler ChangeUICues` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1C112D9D2EEDDE81 | Event | `public event System.EventHandler Click` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-167D4B06D09F7B3E | Event | `public event System.EventHandler ClientSizeChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A07EC84CF0343F1 | Event | `public event System.Windows.Forms.ToolStripDropDownClosedEventHandler Closed` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E5A5F85CC0A94C72 | Event | `public event System.Windows.Forms.ToolStripDropDownClosingEventHandler Closing` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6DD1544B8BCD400C | Event | `public event System.EventHandler ContextMenuChanged` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FAE8D557AE9D01A1 | Event | `public event System.EventHandler ContextMenuStripChanged` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-63A4528B497BE013 | Event | `public event System.Windows.Forms.ControlEventHandler ControlAdded` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50353D46AFDC8A53 | Event | `public event System.Windows.Forms.ControlEventHandler ControlRemoved` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-06A269C7E9B644C8 | Event | `public event System.EventHandler CursorChanged` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-634D30673CCD9041 | Event | `public event System.EventHandler DataContextChanged` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6DEECA02C42B8F88 | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2C88AA9F96D701BF | Event | `public event System.EventHandler DockChanged` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B65BEC3D2191DD00 | Event | `public event System.EventHandler DoubleClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B5CD88C87EAB9904 | Event | `public event System.EventHandler DpiChangedAfterParent` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8CFD1B18374F6202 | Event | `public event System.EventHandler DpiChangedBeforeParent` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FE9B3744519D0235 | Event | `public event System.Windows.Forms.DragEventHandler DragDrop` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-82817F9304BBCC4D | Event | `public event System.Windows.Forms.DragEventHandler DragEnter` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E7BD84EB400B5442 | Event | `public event System.EventHandler DragLeave` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E809724D2283FEC | Event | `public event System.Windows.Forms.DragEventHandler DragOver` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5CE53253B75E20B1 | Event | `public event System.EventHandler EnabledChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0553979E3AA42DD0 | Event | `public event System.EventHandler EndDrag` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-883F2701245BD481 | Event | `public event System.EventHandler Enter` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A5327BE2E3684FE1 | Event | `public event System.EventHandler FontChanged` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-14872F74E95EDB9C | Event | `public event System.EventHandler ForeColorChanged` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CBE3D9171C4DF269 | Event | `public event System.Windows.Forms.GiveFeedbackEventHandler GiveFeedback` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8C6FDA574A0DE305 | Event | `public event System.EventHandler GotFocus` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B67FE5151BE2B94A | Event | `public event System.EventHandler HandleCreated` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A0BAEA19428BC085 | Event | `public event System.EventHandler HandleDestroyed` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B01A4DECD26D363A | Event | `public event System.Windows.Forms.HelpEventHandler HelpRequested` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-12DCC29B08514DE9 | Event | `public event System.EventHandler ImeModeChanged` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F33738E52866FD65 | Event | `public event System.Windows.Forms.InvalidateEventHandler Invalidated` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6E6273B5899F8D4D | Event | `public event System.Windows.Forms.ToolStripItemEventHandler ItemAdded` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5C7E0FFA7F7E2E8C | Event | `public event System.Windows.Forms.ToolStripItemClickedEventHandler ItemClicked` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FF48CF0D48A78042 | Event | `public event System.Windows.Forms.ToolStripItemEventHandler ItemRemoved` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8D769977A6B5140A | Event | `public event System.Windows.Forms.KeyEventHandler KeyDown` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E4A50094A69D277 | Event | `public event System.Windows.Forms.KeyPressEventHandler KeyPress` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E85EF4422F0C61FA | Event | `public event System.Windows.Forms.KeyEventHandler KeyUp` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-37465BB646C96EB3 | Event | `public event System.Windows.Forms.LayoutEventHandler Layout` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E550BF5D54D105B5 | Event | `public event System.EventHandler LayoutCompleted` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-69A25316F133CA91 | Event | `public event System.EventHandler LayoutStyleChanged` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6BAD4B51DCDE7B37 | Event | `public event System.EventHandler Leave` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BCFDE00CD62EDB44 | Event | `public event System.EventHandler LocationChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-94F4DF664C5B66FA | Event | `public event System.EventHandler LostFocus` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EDAEB071257DAA35 | Event | `public event System.EventHandler MarginChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-95A73081D0813CC6 | Event | `public event System.EventHandler MouseCaptureChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D740C351ED721B8D | Event | `public event System.Windows.Forms.MouseEventHandler MouseClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4618376FDE25F80E | Event | `public event System.Windows.Forms.MouseEventHandler MouseDoubleClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2C8B807C21E9FF71 | Event | `public event System.Windows.Forms.MouseEventHandler MouseDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-541FE1D2723DF8CB | Event | `public event System.EventHandler MouseEnter` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7454D5BF490DA634 | Event | `public event System.EventHandler MouseHover` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FC73D840394DDF90 | Event | `public event System.EventHandler MouseLeave` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A29D6130BC2F2AA6 | Event | `public event System.Windows.Forms.MouseEventHandler MouseMove` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-30316C41B84CFCBE | Event | `public event System.Windows.Forms.MouseEventHandler MouseUp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4B9D6A19744DEA43 | Event | `public event System.Windows.Forms.MouseEventHandler MouseWheel` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-913B792F381CBB1D | Event | `public event System.EventHandler Move` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-46532582DF128382 | Event | `public event System.EventHandler Opened` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B9EA38C7453F294F | Event | `public event System.ComponentModel.CancelEventHandler Opening` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C3F869C2A67CBD5D | Event | `public event System.EventHandler PaddingChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6F1D7FF605D55BB7 | Event | `public event System.Windows.Forms.PaintEventHandler Paint` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-51B8DECEFF5410ED | Event | `public event System.Windows.Forms.PaintEventHandler PaintGrip` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C99A0E9AA534120 | Event | `public event System.EventHandler ParentChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9EEB003594CB2318 | Event | `public event System.Windows.Forms.PreviewKeyDownEventHandler PreviewKeyDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1CEB057B0717A964 | Event | `public event System.Windows.Forms.QueryAccessibilityHelpEventHandler QueryAccessibilityHelp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BF12E8520B34A39D | Event | `public event System.Windows.Forms.QueryContinueDragEventHandler QueryContinueDrag` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F3AA3E1084B0E823 | Event | `public event System.EventHandler RegionChanged` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C7B3285B457923FC | Event | `public event System.EventHandler RendererChanged` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2037346E04749621 | Event | `public event System.EventHandler Resize` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-55110606174470CF | Event | `public event System.EventHandler RightToLeftChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-80B6DD018135D549 | Event | `public event System.Windows.Forms.ScrollEventHandler Scroll` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F790364FEFA7BC6A | Event | `public event System.EventHandler SizeChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2EAE4BF1DB79F957 | Event | `public event System.EventHandler StyleChanged` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9FA819089E4E7463 | Event | `public event System.EventHandler SystemColorsChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-60EB5B03E9C46207 | Event | `public event System.EventHandler TabIndexChanged` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C03F20712D6E4A38 | Event | `public event System.EventHandler TabStopChanged` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-626EADE223C4968F | Event | `public event System.EventHandler TextChanged` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E74812A911CE4803 | Event | `public event System.EventHandler Validated` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-20CE7C0A1E832B3D | Event | `public event System.ComponentModel.CancelEventHandler Validating` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BDA5A07323307EBB | Event | `public event System.EventHandler VisibleChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-41E311E5930ADFD8 | Method | `public System.IAsyncResult BeginInvoke(System.Action method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B6276E581165FBC3 | Method | `public System.IAsyncResult BeginInvoke(System.Delegate method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-77CA91689C07AF4B | Method | `public System.IAsyncResult BeginInvoke(System.Delegate method, System.Object[] args)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9198C76C6604D617 | Method | `public System.Void BringToFront()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CC7A934255B72D77 | Method | `public System.Void Close()` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-132FFB831B853E9A | Method | `public System.Void Close(System.Windows.Forms.ToolStripDropDownCloseReason reason)` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B8AF1DB9D7240629 | Method | `public System.Boolean Contains(System.Windows.Forms.Control ctl)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FDEBE80E6506487B | Method | `public System.Void CreateControl()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AD1A81CC5B25030D | Method | `public System.Drawing.Graphics CreateGraphics()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-652C5B39707B6F80 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0CDDE7422610494B | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9540206C74E6ABFE | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-99B80E46E3D940E1 | Method | `public System.Windows.Forms.DragDropEffects DoDragDropAsJson<!!0>(!!0 data, System.Windows.Forms.DragDropEffects allowedEffects) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-186ED8EE46AF2A04 | Method | `public System.Windows.Forms.DragDropEffects DoDragDropAsJson<!!0>(!!0 data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7176E3BB790C843F | Method | `public System.Void DrawToBitmap(System.Drawing.Bitmap bitmap, System.Drawing.Rectangle targetBounds)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FC5EAAAB8B0F2940 | Method | `public System.Object EndInvoke(System.IAsyncResult asyncResult)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C307400BE9B7A097 | Method | `public System.Windows.Forms.Form FindForm()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CE99D78953DDF328 | Method | `public System.Boolean Focus()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-44ECCE3AB0521F77 | Method | `public static System.Windows.Forms.Control FromChildHandle(System.IntPtr handle)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0454D38EC7C28778 | Method | `public static System.Windows.Forms.Control FromHandle(System.IntPtr handle)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D1705D6C7D5B06AD | Method | `public System.Windows.Forms.Control GetChildAtPoint(System.Drawing.Point point)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ADD45FF0A5DA2B4E | Method | `public System.Windows.Forms.Control GetChildAtPoint(System.Drawing.Point pt, System.Windows.Forms.GetChildAtPointSkip skipValue)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-38D7B53DFA206D4F | Method | `public System.Windows.Forms.IContainerControl GetContainerControl()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F898647C833F7B78 | Method | `public System.Windows.Forms.ToolStripItem GetItemAt(System.Drawing.Point point)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6BDC8E4E5DCF6F50 | Method | `public System.Windows.Forms.ToolStripItem GetItemAt(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A27EDFC7AD49596C | Method | `public System.Windows.Forms.Control GetNextControl(System.Windows.Forms.Control ctl, System.Boolean forward)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-34AD7B3B15E8FD12 | Method | `public virtual System.Windows.Forms.ToolStripItem GetNextItem(System.Windows.Forms.ToolStripItem start, System.Windows.Forms.ArrowDirection direction)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC5DB5F356E67698 | Method | `public virtual System.Drawing.Size GetPreferredSize(System.Drawing.Size proposedSize)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A3D7711C761D64BA | Method | `public System.Void Hide()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F12D857A4DA78C28 | Method | `public System.Void Invalidate()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B81C11DCDD0B6061 | Method | `public System.Void Invalidate(System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5EA639CF273D71D6 | Method | `public System.Void Invalidate(System.Drawing.Rectangle rc)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-60B58587A9FE44AA | Method | `public System.Void Invalidate(System.Drawing.Rectangle rc, System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1A25A2A65EFDD550 | Method | `public System.Void Invalidate(System.Drawing.Region region)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A4653A7353A2200 | Method | `public System.Void Invalidate(System.Drawing.Region region, System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9766C78B96FEB185 | Method | `public !!0 Invoke<!!0>(System.Func&#96;1<!!0> method) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6C89B601B58F16EB | Method | `public System.Object Invoke(System.Delegate method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4625DA59D3D4880D | Method | `public System.Object Invoke(System.Delegate method, System.Object[] args)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AB27F07B9CAB4E72 | Method | `public System.Void Invoke(System.Action method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F0E3357A508ACFEC | Method | `public System.Threading.Tasks.Task InvokeAsync(System.Action callback, System.Threading.CancellationToken cancellationToken = null)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F2542846F500DAFF | Method | `public System.Threading.Tasks.Task InvokeAsync(System.Func&#96;2<System.Threading.CancellationToken,System.Threading.Tasks.ValueTask> callback, System.Threading.CancellationToken cancellationToken = null)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A186BD946677DA0D | Method | `public System.Threading.Tasks.Task&#96;1<!!0> InvokeAsync<!!0>(System.Func&#96;1<!!0> callback, System.Threading.CancellationToken cancellationToken = null) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F41D6AECF9712BC0 | Method | `public System.Threading.Tasks.Task&#96;1<!!0> InvokeAsync<!!0>(System.Func&#96;2<System.Threading.CancellationToken,System.Threading.Tasks.ValueTask&#96;1<!!0>> callback, System.Threading.CancellationToken cancellationToken = null) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-718F590714AEDEC0 | Method | `public static System.Boolean IsKeyLocked(System.Windows.Forms.Keys keyVal)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0D2DF9DD6B092616 | Method | `public static System.Boolean IsMnemonic(System.Char charCode, System.String text)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A8619A16A5D2591 | Method | `public System.Drawing.Size LogicalToDeviceUnits(System.Drawing.Size value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-44E5C3FA3B0CA356 | Method | `public System.Int32 LogicalToDeviceUnits(System.Int32 value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-041B51CEAB8740DE | Method | `public System.Void PerformLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-79C722A0695EEF4F | Method | `public System.Void PerformLayout(System.Windows.Forms.Control affectedControl, System.String affectedProperty)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EA37FE02DA91EAB9 | Method | `public System.Drawing.Point PointToClient(System.Drawing.Point p)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F2B95F4EE7417D78 | Method | `public System.Drawing.Point PointToScreen(System.Drawing.Point p)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-97C99AD032065042 | Method | `public System.Windows.Forms.PreProcessControlState PreProcessControlMessage(ref System.Windows.Forms.Message msg)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-82A5943265C96669 | Method | `public virtual System.Boolean PreProcessMessage(ref System.Windows.Forms.Message msg)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DA9D778F5EAC66F6 | Method | `public System.Drawing.Rectangle RectangleToClient(System.Drawing.Rectangle r)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6116E26617B2CBC8 | Method | `public System.Drawing.Rectangle RectangleToScreen(System.Drawing.Rectangle r)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4AC7E5AFE46C2607 | Method | `public virtual System.Void Refresh()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D2A4D9044AC34E59 | Method | `public virtual System.Void ResetBackColor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-707DB629019D3D0C | Method | `public System.Void ResetBindings()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-89B9D0C5F7E9B065 | Method | `public virtual System.Void ResetCursor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C1D2EDCD8525BD9D | Method | `public virtual System.Void ResetFont()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F06FE129ABA369C0 | Method | `public virtual System.Void ResetForeColor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5611666DFF0337EF | Method | `public System.Void ResetImeMode()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-64C70E79404C97A5 | Method | `public System.Void ResetMinimumSize()` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-036960652FD7EEC9 | Method | `public virtual System.Void ResetRightToLeft()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1E1AC7FF3FF844B0 | Method | `public virtual System.Void ResetText()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A033DFA0F04B64EC | Method | `public System.Void ResumeLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-11D9656D3D2FC64A | Method | `public System.Void ResumeLayout(System.Boolean performLayout)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A61BD5191F8F42EB | Method | `public System.Void Scale(System.Drawing.SizeF factor)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-028A688BED255CA3 | Method | `public System.Void Scale(System.Single dx, System.Single dy)` | `System.Windows.Forms.Control` ObsoleteAttribute("This method has been deprecated. Use the Scale(SizeF ratio) method instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D04B3B5891788D4A | Method | `public System.Void Scale(System.Single ratio)` | `System.Windows.Forms.Control` ObsoleteAttribute("This method has been deprecated. Use the Scale(SizeF ratio) method instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-40016444B28947B5 | Method | `public System.Void ScaleBitmapLogicalToDevice(ref System.Drawing.Bitmap logicalBitmap)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F96961BDB47AD0FA | Method | `public System.Void ScrollControlIntoView(System.Windows.Forms.Control activeControl)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CBDE5DF871561D11 | Method | `public System.Void Select()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-221179BF8B53B5B9 | Method | `public System.Boolean SelectNextControl(System.Windows.Forms.Control ctl, System.Boolean forward, System.Boolean tabStopOnly, System.Boolean nested, System.Boolean wrap)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5BB664C5D910281F | Method | `public System.Void SendToBack()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1CBCE3ECDAC6EF84 | Method | `public System.Void SetAutoScrollMargin(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F45B3E7E54094FB8 | Method | `public System.Void SetBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5CBEBFB472BF203C | Method | `public System.Void SetBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C16EE88F1E65B863 | Method | `public System.Void Show()` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B2596BFD6A726233 | Method | `public System.Void Show(System.Drawing.Point position, System.Windows.Forms.ToolStripDropDownDirection direction)` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-11CBE5DDE268D4ED | Method | `public System.Void Show(System.Drawing.Point screenLocation)` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-240BC63EFFDFA49F | Method | `public System.Void Show(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-28900DB449F8E0AE | Method | `public System.Void Show(System.Windows.Forms.Control control, System.Drawing.Point position)` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9BA186BB5F9E1D19 | Method | `public System.Void Show(System.Windows.Forms.Control control, System.Drawing.Point position, System.Windows.Forms.ToolStripDropDownDirection direction)` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3728A6F37CFFF518 | Method | `public System.Void Show(System.Windows.Forms.Control control, System.Int32 x, System.Int32 y)` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2D612E3F6FC86013 | Method | `public System.Void SuspendLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16D7B0C3B2B02490 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-76DC2271C92062D2 | Method | `public System.Void Update()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-92A23C68ED37BA23 | Property | `System.Windows.Forms.AccessibleObject AccessibilityObject { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1383FABE6C93AE3B | Property | `System.String AccessibleDefaultActionDescription { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7DA1BF0ED80F8CA9 | Property | `System.String AccessibleDescription { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-81502CC8EB5585B0 | Property | `System.String AccessibleName { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B4BE2F833F7EEFA5 | Property | `System.Windows.Forms.AccessibleRole AccessibleRole { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(-1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5AE4C02A2AB8B43D | Property | `System.Boolean AllowClickThrough { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(true); DefaultValueAttribute(false); DesignerSerializationVisibilityAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7667E44518457505 | Property | `virtual System.Boolean AllowDrop { public get; public set; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-149615DA32C9B7AC | Property | `System.Boolean AllowItemReorder { public get; public set; }` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ADE528D94625B8F9 | Property | `System.Boolean AllowMerge { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-64F504C590CE6249 | Property | `System.Boolean AllowTransparency { public get; public set; }` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DF57877D820FCB5B | Property | `virtual System.Windows.Forms.AnchorStyles Anchor { public get; public set; }` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F626A15BCDBE7402 | Property | `System.Boolean AutoClose { public get; public set; }` | `System.Windows.Forms.ToolStripDropDown` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5E5A3A6BBBF7218C | Property | `virtual System.Boolean AutoScroll { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-14BDF62E1C5011B3 | Property | `System.Drawing.Size AutoScrollMargin { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FBE359BC1F444D06 | Property | `System.Drawing.Size AutoScrollMinSize { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7E4012CEFD203697 | Property | `virtual System.Drawing.Point AutoScrollOffset { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DefaultValueAttribute(System.Drawing.Point,"0, 0") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F3FA7A9A6AC5F11 | Property | `System.Drawing.Point AutoScrollPosition { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C098969E57964C78 | Property | `virtual System.Boolean AutoSize { public get; public set; }` | `System.Windows.Forms.ToolStripDropDown` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-18B03039CC7AC865 | Property | `System.Drawing.Color BackColor { public get; public set; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-697080C6575DA23D | Property | `virtual System.Drawing.Image BackgroundImage { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-303661CE985ECF0B | Property | `virtual System.Windows.Forms.ImageLayout BackgroundImageLayout { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(1); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A130CFE447C3F9E8 | Property | `virtual System.Windows.Forms.BindingContext BindingContext { public get; public set; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-74BE03506612F76A | Property | `System.Int32 Bottom { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50A26945DB7593C6 | Property | `System.Drawing.Rectangle Bounds { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3D273AB11BDEFE49 | Property | `System.Boolean CanFocus { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8CA02874663F625F | Property | `System.Boolean CanOverflow { public get; public set; }` | `System.Windows.Forms.ToolStripDropDown` DefaultValueAttribute(false); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7379D4DB8CD0EC51 | Property | `System.Boolean CanSelect { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AFA286A60B5FD79F | Property | `System.Boolean Capture { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3B82D8AD549E9908 | Property | `System.Boolean CausesValidation { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false); DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-55D0B0DE9BB481C2 | Property | `static System.Boolean CheckForIllegalCrossThreadCalls { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5F0E2C08AA6BBC52 | Property | `System.Drawing.Rectangle ClientRectangle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D04E08D5A1A7E5A1 | Property | `System.Drawing.Size ClientSize { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7E83B42061B4C948 | Property | `System.String CompanyName { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EEB99432DDDC4853 | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-57F42D353C62E7BE | Property | `System.Boolean ContainsFocus { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-395DF83E82798B88 | Property | `virtual System.Windows.Forms.ContextMenu ContextMenu { public get; public set; }` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4AA2A8F5C66A638B | Property | `System.Windows.Forms.ContextMenuStrip ContextMenuStrip { public get; public set; }` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C89C32D33F88AFA3 | Property | `System.Windows.Forms.Control+ControlCollection Controls { public get; }` | `System.Windows.Forms.ToolStrip` DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2FE52EB16F9CB5F7 | Property | `System.Boolean Created { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F18B99C48A3B94E2 | Property | `virtual System.Windows.Forms.Cursor Cursor { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D6D99E3BBCCE4F07 | Property | `System.Windows.Forms.ControlBindingsCollection DataBindings { public get; }` | `System.Windows.Forms.Control` DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B66524F2F34839FA | Property | `virtual System.Object DataContext { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); BindableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2ACA12B6C1B2CAB9 | Property | `static System.Drawing.Color DefaultBackColor { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C52DE02F58F9BA6D | Property | `virtual System.Windows.Forms.ToolStripDropDownDirection DefaultDropDownDirection { public get; public set; }` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DC653ADF495002A3 | Property | `static System.Drawing.Font DefaultFont { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CA337DBCF8E368E3 | Property | `static System.Drawing.Color DefaultForeColor { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-33D574DB8E2EABAB | Property | `System.Int32 DeviceDpi { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B3E557986330D1D7 | Property | `virtual System.Drawing.Rectangle DisplayRectangle { public get; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0DC3EACE0113B36F | Property | `System.Boolean Disposing { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C5BEA244B1170613 | Property | `virtual System.Windows.Forms.DockStyle Dock { public get; public set; }` | `System.Windows.Forms.ToolStripDropDown` DefaultValueAttribute(0); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E206500784EB706F | Property | `System.Windows.Forms.ScrollableControl+DockPaddingEdges DockPadding { public get; }` | `System.Windows.Forms.ScrollableControl` DesignerSerializationVisibilityAttribute(0); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-770FED876A833187 | Property | `System.Boolean DropShadowEnabled { public get; public set; }` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5C3A284569892E89 | Property | `System.Boolean Enabled { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BEB688986D26FA22 | Property | `virtual System.Boolean Focused { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FD5E6FF4B3160432 | Property | `virtual System.Drawing.Font Font { public get; public set; }` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D6F75CD5BF4E6E89 | Property | `System.Drawing.Color ForeColor { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3BBD37BA78626B9E | Property | `System.Windows.Forms.ToolStripGripDisplayStyle GripDisplayStyle { public get; }` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B4C208E97E71B623 | Property | `System.Windows.Forms.Padding GripMargin { public get; public set; }` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9594143E0C35D900 | Property | `System.Drawing.Rectangle GripRectangle { public get; }` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-54C82C810908509E | Property | `System.Windows.Forms.ToolStripGripStyle GripStyle { public get; public set; }` | `System.Windows.Forms.ToolStripDropDown` DefaultValueAttribute(0); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F408729D83A07115 | Property | `System.IntPtr Handle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E41D99952A4B0B39 | Property | `System.Boolean HasChildren { public get; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E319505A471192A | Property | `System.Int32 Height { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-89231053D2919EBC | Property | `System.Windows.Forms.HScrollProperties HorizontalScroll { public get; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-14093E37BA1D80CE | Property | `System.Windows.Forms.ImageList ImageList { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(null); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F4ADBBB0A36AFBF8 | Property | `System.Drawing.Size ImageScalingSize { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(System.Drawing.Size,"16,16") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-06B34EF451DFFB98 | Property | `System.Windows.Forms.ImeMode ImeMode { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-03149F67FA3E4DE3 | Property | `System.Boolean InvokeRequired { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4CAEEF06BA59D9E1 | Property | `System.Boolean IsAccessible { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A25900BCE8FB4B89 | Property | `System.Boolean IsAncestorSiteInDesignMode { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E42D89CB928FFDCC | Property | `System.Boolean IsAutoGenerated { public get; }` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DA3F6E5AC5D8AE27 | Property | `System.Boolean IsCurrentlyDragging { public get; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2ED208CB868E2CAB | Property | `System.Boolean IsDisposed { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1DB0CA55647355F5 | Property | `System.Boolean IsDropDown { public get; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CF0B1BDDFACE19C0 | Property | `System.Boolean IsHandleCreated { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-70D68B1D72FD026B | Property | `System.Boolean IsMirrored { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4881BF70D94AFEBF | Property | `virtual System.Windows.Forms.ToolStripItemCollection Items { public get; }` | `System.Windows.Forms.ToolStrip` DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FCEDA09C1B2C5A5B | Property | `virtual System.Windows.Forms.Layout.LayoutEngine LayoutEngine { public get; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-67ED75D1726E72AB | Property | `System.Windows.Forms.LayoutSettings LayoutSettings { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(null); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E457DFAC98CA43A7 | Property | `System.Windows.Forms.ToolStripLayoutStyle LayoutStyle { public get; public set; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B5A1D539D99C05BD | Property | `System.Int32 Left { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8D6577E3B1A35EE0 | Property | `System.Drawing.Point Location { public get; public set; }` | `System.Windows.Forms.ToolStripDropDown` DesignerSerializationVisibilityAttribute(0); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4087C2714BC1BC6A | Property | `System.Windows.Forms.Padding Margin { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F16FE20F2FA73BA0 | Property | `virtual System.Drawing.Size MaximumSize { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3AB56150ABC85EA6 | Property | `virtual System.Drawing.Size MinimumSize { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6A08FC153C18B7A9 | Property | `static System.Windows.Forms.Keys ModifierKeys { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-891D8A54BA7755C8 | Property | `static System.Windows.Forms.MouseButtons MouseButtons { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B72A7F3339F01C44 | Property | `static System.Drawing.Point MousePosition { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BE67F8137CEFC642 | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4303447662599C0E | Property | `System.Double Opacity { public get; public set; }` | `System.Windows.Forms.ToolStripDropDown` TypeConverterAttribute(System.Windows.Forms.OpacityConverter); DefaultValueAttribute(1); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ADDE043528B8E453 | Property | `System.Windows.Forms.Orientation Orientation { public get; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D4D1A215633187F2 | Property | `System.Windows.Forms.ToolStripOverflowButton OverflowButton { public get; }` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5FBD573419BCE730 | Property | `System.Windows.Forms.ToolStripItem OwnerItem { public get; public set; }` | `System.Windows.Forms.ToolStripDropDown` DefaultValueAttribute(null); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-544C455C0D2FB543 | Property | `System.Windows.Forms.Padding Padding { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F1CC27550F5CCC8 | Property | `System.Windows.Forms.Control Parent { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-424EADE9E5EAB7B1 | Property | `System.Drawing.Size PreferredSize { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-24475FA0ED8E2424 | Property | `System.String ProductName { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F34D28E8985DDE08 | Property | `System.String ProductVersion { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-90AC9B97F5CD0333 | Property | `System.Boolean RecreatingHandle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C046235E003361D1 | Property | `System.Drawing.Region Region { public get; public set; }` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-95CA7A0E7646DCE7 | Property | `System.Windows.Forms.ToolStripRenderer Renderer { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-38AD7AA83657DDF2 | Property | `System.Windows.Forms.ToolStripRenderMode RenderMode { public get; public set; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3FC24AA54A5F7801 | Property | `System.Int32 Right { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8B9E4C49D34DFEEE | Property | `virtual System.Windows.Forms.RightToLeft RightToLeft { public get; public set; }` | `System.Windows.Forms.ToolStripDropDown` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4066A223ACE0EEE1 | Property | `System.Boolean ShowItemToolTips { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B054C3D561103C4C | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0D378AB1F3ECE9E4 | Property | `System.Drawing.Size Size { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2CA79F3FCF171CD5 | Property | `System.Boolean Stretch { public get; public set; }` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50975CD5394FFE3C | Property | `System.Int32 TabIndex { public get; public set; }` | `System.Windows.Forms.ToolStripDropDown` DesignerSerializationVisibilityAttribute(0); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-529F6A5C44E6B03F | Property | `System.Boolean TabStop { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-115C85D151CE4644 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E31787F3D3E0A6CB | Property | `virtual System.String Text { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true); BindableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E769F8C6FD059C73 | Property | `virtual System.Windows.Forms.ToolStripTextDirection TextDirection { public get; public set; }` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false); DefaultValueAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BB637A49B834EA24 | Property | `System.Int32 Top { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9AAA7920F6B354DE | Property | `System.Boolean TopLevel { public get; public set; }` | `System.Windows.Forms.ToolStripDropDown` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EDC0E371FA1ED3FD | Property | `System.Windows.Forms.Control TopLevelControl { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-66CEA3E90A4BDB14 | Property | `System.Boolean UseWaitCursor { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(false); BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-34E585AB4B1018DC | Property | `System.Windows.Forms.VScrollProperties VerticalScroll { public get; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A5212D19E7EAFCF5 | Property | `System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.ToolStripDropDown` LocalizableAttribute(true); DefaultValueAttribute(false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-475C746EBBCE9F6E | Property | `System.Int32 Width { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-750E81C6D87FA29B | Property | `System.Windows.Forms.IWindowTarget WindowTarget { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-65AC7E16FDDEAE04 | Field | `protected const System.Int32 ScrollStateAutoScrolling = 1` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B01D47A22AB9735C | Field | `protected const System.Int32 ScrollStateFullDrag = 16` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DDFC4E48164C0685 | Field | `protected const System.Int32 ScrollStateHScrollVisible = 2` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CBE27C6D2749EEBC | Field | `protected const System.Int32 ScrollStateUserHasScrolled = 8` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A82764BB4824C13A | Field | `protected const System.Int32 ScrollStateVScrollVisible = 4` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A308D898C3C5F32B | Method | `protected System.Void AccessibilityNotifyClients(System.Windows.Forms.AccessibleEvents accEvent, System.Int32 objectID, System.Int32 childID)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-28B27041EB5BB358 | Method | `protected internal System.Void AccessibilityNotifyClients(System.Windows.Forms.AccessibleEvents accEvent, System.Int32 childID)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D6AC19C388DB9AA9 | Method | `protected virtual System.Void AdjustFormScrollbars(System.Boolean displayScrollbars)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A2FD5EE4280F9539 | Method | `protected virtual System.Windows.Forms.AccessibleObject CreateAccessibilityInstance()` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E355A20B1D9479EC | Method | `protected virtual System.Windows.Forms.Control+ControlCollection CreateControlsInstance()` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-89F509F82734F784 | Method | `protected internal virtual System.Windows.Forms.ToolStripItem CreateDefaultItem(System.String text, System.Drawing.Image image, System.EventHandler onClick)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F6AC075004BB47AE | Method | `protected virtual System.Void CreateHandle()` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BE11BF26E950BEE6 | Method | `protected virtual System.Windows.Forms.LayoutSettings CreateLayoutSettings(System.Windows.Forms.ToolStripLayoutStyle style)` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D2718404579B04BC | Method | `protected virtual System.Void DefWndProc(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-09C240F343112FB0 | Method | `protected virtual System.Void DestroyHandle()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-47DBA41113D302B6 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8A83384732C8C7D0 | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-87D5EBACB766260E | Method | `protected virtual System.Windows.Forms.AccessibleObject GetAccessibilityObjectById(System.Int32 objectId)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-28CF19BECF6F9DF4 | Method | `protected System.Windows.Forms.AutoSizeMode GetAutoSizeMode()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E14130B0EB972217 | Method | `protected virtual System.Drawing.Rectangle GetScaledBounds(System.Drawing.Rectangle bounds, System.Drawing.SizeF factor, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CF8C5E752523CDFC | Method | `protected System.Boolean GetScrollState(System.Int32 bit)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-858CC4113434AC72 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-36019B36ADDD2655 | Method | `protected System.Boolean GetStyle(System.Windows.Forms.ControlStyles flag)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B01B106494A49D6C | Method | `protected System.Boolean GetTopLevel()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-009CA65BAE51EE71 | Method | `protected virtual System.Void InitLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6761971CB6CECCC2 | Method | `protected System.Void InvokeGotFocus(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-99377993A69262FD | Method | `protected System.Void InvokeLostFocus(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4A0E8602DA1D90C3 | Method | `protected System.Void InvokeOnClick(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B73461614295E524 | Method | `protected System.Void InvokePaint(System.Windows.Forms.Control c, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7B7097E3D94595BF | Method | `protected System.Void InvokePaintBackground(System.Windows.Forms.Control c, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E08C94B6B36775B4 | Method | `protected virtual System.Boolean IsInputChar(System.Char charCode)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-57572E03A3B6B8E7 | Method | `protected virtual System.Boolean IsInputKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3B06463DE10E7400 | Method | `protected virtual System.Void NotifyInvalidate(System.Drawing.Rectangle invalidatedArea)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EE6050A215409532 | Method | `protected virtual System.Void OnAutoSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC21B5F39B77BB4C | Method | `protected virtual System.Void OnBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-23BE4B1EFDE77228 | Method | `protected virtual System.Void OnBackgroundImageChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3FB46E3BD6A4C6B0 | Method | `protected virtual System.Void OnBackgroundImageLayoutChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-87F667F75FB9E280 | Method | `protected virtual System.Void OnBeginDrag(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-56E5A0151E68AFBF | Method | `protected virtual System.Void OnBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8FF10C1CFE4DDFCE | Method | `protected virtual System.Void OnCausesValidationChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9EF8AA3196B251E3 | Method | `protected virtual System.Void OnChangeUICues(System.Windows.Forms.UICuesEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-65FACFA32A5A395C | Method | `protected virtual System.Void OnClick(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC09C274176AA530 | Method | `protected virtual System.Void OnClientSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2D96852511FC3B16 | Method | `protected virtual System.Void OnClosed(System.Windows.Forms.ToolStripDropDownClosedEventArgs e)` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-530EEE99B9213EDB | Method | `protected virtual System.Void OnClosing(System.Windows.Forms.ToolStripDropDownClosingEventArgs e)` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B4A1A9A0FFB4CEE | Method | `protected virtual System.Void OnContextMenuChanged(System.EventArgs e)` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9335BEDFB804EE0F | Method | `protected virtual System.Void OnContextMenuStripChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-680676E6E6C95991 | Method | `protected virtual System.Void OnControlAdded(System.Windows.Forms.ControlEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1F457A86FA545A07 | Method | `protected virtual System.Void OnControlRemoved(System.Windows.Forms.ControlEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0149494CD02DF8A | Method | `protected virtual System.Void OnCreateControl()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1CDF4A4175E70DCA | Method | `protected virtual System.Void OnCursorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EDE6F9F6627CD564 | Method | `protected virtual System.Void OnDataContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CE9C45E87A6063F9 | Method | `protected virtual System.Void OnDockChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29BF4121312BC7B1 | Method | `protected virtual System.Void OnDoubleClick(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C8698A36FEE362E8 | Method | `protected virtual System.Void OnDpiChangedAfterParent(System.EventArgs e)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6A5F71B2CC725FF9 | Method | `protected virtual System.Void OnDpiChangedBeforeParent(System.EventArgs e)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E43991D16286F0F1 | Method | `protected virtual System.Void OnDragDrop(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5B26A4CE2CC8F40A | Method | `protected virtual System.Void OnDragEnter(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-82B9F6C8F9A28570 | Method | `protected virtual System.Void OnDragLeave(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-05D094BE9E19018B | Method | `protected virtual System.Void OnDragOver(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EDB17B4725BC43FA | Method | `protected virtual System.Void OnEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-238ED7B096B3BC47 | Method | `protected virtual System.Void OnEndDrag(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-91115B5DB8545111 | Method | `protected internal virtual System.Void OnEnter(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-19EDD471781FB954 | Method | `protected virtual System.Void OnFontChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2835A17970FCE0B4 | Method | `protected virtual System.Void OnForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8CAEC3C0C281F999 | Method | `protected virtual System.Void OnGiveFeedback(System.Windows.Forms.GiveFeedbackEventArgs gfbevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7E4B4005528934C0 | Method | `protected virtual System.Void OnGotFocus(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2ED141AE732A8B75 | Method | `protected virtual System.Void OnHandleCreated(System.EventArgs e)` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-11C7112B9E7E598B | Method | `protected virtual System.Void OnHandleDestroyed(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-90D096BE86F2C6AB | Method | `protected virtual System.Void OnHelpRequested(System.Windows.Forms.HelpEventArgs hevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C4371179B627B54C | Method | `protected virtual System.Void OnImeModeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-941EDEC27CF888C2 | Method | `protected virtual System.Void OnInvalidated(System.Windows.Forms.InvalidateEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B1A4FD834CB472AA | Method | `protected internal virtual System.Void OnItemAdded(System.Windows.Forms.ToolStripItemEventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E1CA645E620AF2F9 | Method | `protected virtual System.Void OnItemClicked(System.Windows.Forms.ToolStripItemClickedEventArgs e)` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-782B8FF88FD3AB12 | Method | `protected internal virtual System.Void OnItemRemoved(System.Windows.Forms.ToolStripItemEventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C746F768AF30BA80 | Method | `protected virtual System.Void OnKeyDown(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6FC411F8BBDA1F89 | Method | `protected virtual System.Void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-93545D9FBC9EF205 | Method | `protected virtual System.Void OnKeyUp(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-263CCEF84EEF91D7 | Method | `protected virtual System.Void OnLayout(System.Windows.Forms.LayoutEventArgs e)` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-96594EEDE28E4D30 | Method | `protected virtual System.Void OnLayoutCompleted(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-09A5884A13E05901 | Method | `protected virtual System.Void OnLayoutStyleChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FCE7FB1F80C3AA3B | Method | `protected internal virtual System.Void OnLeave(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C768DF155E460B23 | Method | `protected virtual System.Void OnLocationChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C8B7A222DEDE6A34 | Method | `protected virtual System.Void OnLostFocus(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1E1C19878F013F55 | Method | `protected virtual System.Void OnMarginChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E4A67980EABAD02 | Method | `protected virtual System.Void OnMouseCaptureChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-428E951DD14F3A03 | Method | `protected virtual System.Void OnMouseClick(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C10ED223B2CF002E | Method | `protected virtual System.Void OnMouseDoubleClick(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-359FC556B0EE9891 | Method | `protected virtual System.Void OnMouseDown(System.Windows.Forms.MouseEventArgs mea)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3270DDD70D1F1DA6 | Method | `protected virtual System.Void OnMouseEnter(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7A6C3ED3D0FD4A4C | Method | `protected virtual System.Void OnMouseHover(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8AC9E324AC4AA4B1 | Method | `protected virtual System.Void OnMouseLeave(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-28921923EE204E98 | Method | `protected virtual System.Void OnMouseMove(System.Windows.Forms.MouseEventArgs mea)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29F1C7BA4C040441 | Method | `protected virtual System.Void OnMouseUp(System.Windows.Forms.MouseEventArgs mea)` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A924A938C9FD3E06 | Method | `protected virtual System.Void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-60554A09B419EA21 | Method | `protected virtual System.Void OnMove(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9DFACF36264170FF | Method | `protected virtual System.Void OnNotifyMessage(System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-96AABDF789368C0F | Method | `protected virtual System.Void OnOpened(System.EventArgs e)` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3D5F0B62EE136699 | Method | `protected virtual System.Void OnOpening(System.ComponentModel.CancelEventArgs e)` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4C8D2522599C043F | Method | `protected virtual System.Void OnPaddingChanged(System.EventArgs e)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B6B4E53899FA7AB | Method | `protected virtual System.Void OnPaint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4749FF633BEBDF5B | Method | `protected virtual System.Void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-74F975B0946B4347 | Method | `protected internal virtual System.Void OnPaintGrip(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-542F967EA0BDD192 | Method | `protected virtual System.Void OnParentBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3E34F0EECE5A09A1 | Method | `protected virtual System.Void OnParentBackgroundImageChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AE9011FD41C126B2 | Method | `protected virtual System.Void OnParentBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B5923983F87F571C | Method | `protected virtual System.Void OnParentChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D70AD41BE2A84BFE | Method | `protected virtual System.Void OnParentCursorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-66762484D85C00B5 | Method | `protected virtual System.Void OnParentDataContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A012487B389EE7E1 | Method | `protected virtual System.Void OnParentEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3CD8440E6F3A25F5 | Method | `protected virtual System.Void OnParentFontChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-79C85DE01370925A | Method | `protected virtual System.Void OnParentForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-660A398E0BAF31C6 | Method | `protected virtual System.Void OnParentRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-93E328FD47AF8939 | Method | `protected virtual System.Void OnParentVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-888B1BA7EC315D3B | Method | `protected virtual System.Void OnPreviewKeyDown(System.Windows.Forms.PreviewKeyDownEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BF96FFC57FC7F839 | Method | `protected virtual System.Void OnPrint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0DAC364A4DDA30BD | Method | `protected virtual System.Void OnQueryContinueDrag(System.Windows.Forms.QueryContinueDragEventArgs qcdevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CB404AF7037BFB43 | Method | `protected virtual System.Void OnRegionChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9D018BFAD92BE08B | Method | `protected virtual System.Void OnRendererChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BD542D8A33467761 | Method | `protected virtual System.Void OnResize(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E41184278C323F80 | Method | `protected virtual System.Void OnRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F90451A3EEBFBCE0 | Method | `protected virtual System.Void OnScroll(System.Windows.Forms.ScrollEventArgs se)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6CAED4DA1A0BC626 | Method | `protected virtual System.Void OnSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A2C7DA032024956A | Method | `protected virtual System.Void OnStyleChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C3D559D31D4C2314 | Method | `protected virtual System.Void OnSystemColorsChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D31F62017C3DE131 | Method | `protected virtual System.Void OnTabIndexChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E72884B1C58F2332 | Method | `protected virtual System.Void OnTabStopChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5903A934D7F6CEDC | Method | `protected virtual System.Void OnTextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C8B5B08E440E7092 | Method | `protected virtual System.Void OnValidated(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DE8F829D0C6B4BA8 | Method | `protected virtual System.Void OnValidating(System.ComponentModel.CancelEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EF7294EBDAB9F015 | Method | `protected virtual System.Void OnVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-288F585EAB8F9DE0 | Method | `protected virtual System.Boolean ProcessCmdKey(ref System.Windows.Forms.Message m, System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D92EDE78CEC4E03 | Method | `protected virtual System.Boolean ProcessDialogChar(System.Char charCode)` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-07E9CF4CEC49CF8D | Method | `protected virtual System.Boolean ProcessDialogKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-25FA653EB0D9D2C0 | Method | `protected virtual System.Boolean ProcessKeyEventArgs(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-091A55811B8739C7 | Method | `protected internal virtual System.Boolean ProcessKeyMessage(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8AAC28DD3A1D78CF | Method | `protected virtual System.Boolean ProcessKeyPreview(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D55CE60922308C0E | Method | `protected internal virtual System.Boolean ProcessMnemonic(System.Char charCode)` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-257CE8041D49E4E4 | Method | `protected System.Void RaiseDragEvent(System.Object key, System.Windows.Forms.DragEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1288150EE0489AD3 | Method | `protected System.Void RaiseKeyEvent(System.Object key, System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-97F409FB17F6EB7C | Method | `protected System.Void RaiseMouseEvent(System.Object key, System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3AA65208AE2CB373 | Method | `protected System.Void RaisePaintEvent(System.Object key, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CE6E91BC09F9222B | Method | `protected System.Void RecreateHandle()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E8388D19F31BC75 | Method | `protected static System.Boolean ReflectMessage(System.IntPtr hWnd, ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-233A69086D373398 | Method | `protected virtual System.Void RescaleConstantsForDpi(System.Int32 deviceDpiOld, System.Int32 deviceDpiNew)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-51B4D42BF8DA1104 | Method | `protected System.Void ResetMouseEventArgs()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2BF0FCB4571F4B0B | Method | `protected virtual System.Void RestoreFocus()` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DD2B5F1F919488D0 | Method | `protected System.Drawing.ContentAlignment RtlTranslateAlignment(System.Drawing.ContentAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F62E9349AB1DCFE4 | Method | `protected System.Windows.Forms.HorizontalAlignment RtlTranslateAlignment(System.Windows.Forms.HorizontalAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-841C50C640E4405F | Method | `protected System.Windows.Forms.LeftRightAlignment RtlTranslateAlignment(System.Windows.Forms.LeftRightAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-109B332D7B65D534 | Method | `protected internal System.Drawing.ContentAlignment RtlTranslateContent(System.Drawing.ContentAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7F053B0BFDC9831D | Method | `protected System.Windows.Forms.HorizontalAlignment RtlTranslateHorizontal(System.Windows.Forms.HorizontalAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-64821FEBEBEF3138 | Method | `protected System.Windows.Forms.LeftRightAlignment RtlTranslateLeftRight(System.Windows.Forms.LeftRightAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B1F6611156C6FC37 | Method | `protected virtual System.Void ScaleControl(System.Drawing.SizeF factor, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D450E4F1E08A5F69 | Method | `protected virtual System.Void ScaleCore(System.Single dx, System.Single dy)` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EE3E96B7115087A4 | Method | `protected virtual System.Drawing.Point ScrollToControl(System.Windows.Forms.Control activeControl)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C5A07A36DFAD7E66 | Method | `protected virtual System.Void Select(System.Boolean directed, System.Boolean forward)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-698FEB5407CFFE45 | Method | `protected System.Void SetAutoSizeMode(System.Windows.Forms.AutoSizeMode mode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-84BC997BF9821967 | Method | `protected virtual System.Void SetBoundsCore(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D17C16438B3B5972 | Method | `protected virtual System.Void SetClientSizeCore(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D1F9818898C547AA | Method | `protected virtual System.Void SetDisplayedItems()` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FE3B91A3AA0DA1F1 | Method | `protected System.Void SetDisplayRectLocation(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DCD4400BB0DCC04C | Method | `protected internal System.Void SetItemLocation(System.Windows.Forms.ToolStripItem item, System.Drawing.Point location)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D05898055E88C61 | Method | `protected static System.Void SetItemParent(System.Windows.Forms.ToolStripItem item, System.Windows.Forms.ToolStrip parent)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6228ACB4D22ADE8E | Method | `protected System.Void SetScrollState(System.Int32 bit, System.Boolean value)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DF1D1C7015BAD50F | Method | `protected System.Void SetStyle(System.Windows.Forms.ControlStyles flag, System.Boolean value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2AAA64A6BF6F69CA | Method | `protected System.Void SetTopLevel(System.Boolean value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0F76B72ECC7E105 | Method | `protected virtual System.Void SetVisibleCore(System.Boolean visible)` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-97F48AB38E8C03F7 | Method | `protected virtual System.Drawing.Size SizeFromClientSize(System.Drawing.Size clientSize)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-772770E3FE780CD1 | Method | `protected System.Void UpdateBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C3C80B42DAF27FA | Method | `protected System.Void UpdateBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Int32 clientWidth, System.Int32 clientHeight)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-35DE1E009CA67F7E | Method | `protected internal System.Void UpdateBounds()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7F01088701DEF33A | Method | `protected System.Void UpdateStyles()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-13361BAE771B6A4A | Method | `protected System.Void UpdateZOrder()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D3ED542D68148FFB | Method | `protected virtual System.Void WndProc(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D44BC008D1A50D83 | Property | `virtual System.Boolean CanEnableIme { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-86707612B37759AC | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8D3396FCB98F162E | Property | `virtual System.Windows.Forms.CreateParams CreateParams { protected get; }` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C4FE8462BD3AB53 | Property | `virtual System.Windows.Forms.Cursor DefaultCursor { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C14A424BDD288993 | Property | `virtual System.Windows.Forms.DockStyle DefaultDock { protected get; }` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AEDD69976F74BDDF | Property | `virtual System.Windows.Forms.Padding DefaultGripMargin { protected get; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-631F5DFE63F62CC5 | Property | `virtual System.Windows.Forms.ImeMode DefaultImeMode { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E123352B6B1804D | Property | `virtual System.Windows.Forms.Padding DefaultMargin { protected get; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6AD7248F2BC2702B | Property | `virtual System.Drawing.Size DefaultMaximumSize { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F2888207B59B24E1 | Property | `virtual System.Drawing.Size DefaultMinimumSize { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B55EE571742950DB | Property | `virtual System.Windows.Forms.Padding DefaultPadding { protected get; }` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FCACA0749377FDCB | Property | `virtual System.Boolean DefaultShowItemToolTips { protected get; }` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D2B1A7F3DBF235D4 | Property | `virtual System.Drawing.Size DefaultSize { protected get; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D2F7169C61C6FEDA | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FB3432A62B16C61A | Property | `virtual System.Windows.Forms.ToolStripItemCollection DisplayedItems { protected internal get; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-936BF4D3A459C494 | Property | `virtual System.Boolean DoubleBuffered { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F8E35B7BD9C54E95 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-36D7F44C4B547110 | Property | `System.Int32 FontHeight { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0932526D79BA8EDB | Property | `System.Boolean HScroll { protected get; protected set; }` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E8D714FC0B65DA9A | Property | `virtual System.Windows.Forms.ImeMode ImeModeBase { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3E545820011A529D | Property | `virtual System.Drawing.Size MaxItemSize { protected internal get; }` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-84BA97D27666ACEA | Property | `static System.Windows.Forms.ImeMode PropagatingImeMode { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8563891D7A24BD0F | Property | `System.Boolean RenderRightToLeft { protected internal get; }` | `System.Windows.Forms.Control` ObsoleteAttribute("This property has been deprecated. Please use RightToLeft instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FE877F2453E2F25C | Property | `System.Boolean ResizeRedraw { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-151587F6997F13C4 | Property | `virtual System.Boolean ScaleChildren { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A940A83356981DD6 | Property | `virtual System.Boolean ShowFocusCues { protected internal get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-66A78B73E963F8F0 | Property | `virtual System.Boolean ShowKeyboardCues { protected internal get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6931C4A6DCC68C2B | Property | `virtual System.Boolean TopMost { protected get; }` | `System.Windows.Forms.ToolStripDropDown`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-12BD78C62E294160 | Property | `System.Boolean VScroll { protected get; protected set; }` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
