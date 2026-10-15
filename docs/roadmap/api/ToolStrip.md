# ToolStrip：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/ToolStrip.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.ToolStrip`；基类：`System.Windows.Forms.ScrollableControl`。Lumina 同名类型：🟡 存在；基类及基线接口形状：⬜ 未匹配。

公开属性 **114**，公开事件 **82**，公开方法 **80**；受保护扩展成员 **199**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-ED6243766E0CBE45 | Constructor | `public ToolStrip()` | `System.Windows.Forms.ToolStrip`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2137A84FD1F08641 | Constructor | `public ToolStrip(System.Windows.Forms.ToolStripItem[] items)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-937083CCB6EFBC31 | Event | `public event System.EventHandler AutoSizeChanged` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-31233CBF4E87DA75 | Event | `public event System.EventHandler BackColorChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4D89DFC2F85E5BC9 | Event | `public event System.EventHandler BackgroundImageChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F41BCE0060AFC367 | Event | `public event System.EventHandler BackgroundImageLayoutChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-657EFC510543B450 | Event | `public event System.EventHandler BeginDrag` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6665ADB2721D7B2A | Event | `public event System.EventHandler BindingContextChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-40DA0A41FB240455 | Event | `public event System.EventHandler CausesValidationChanged` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EA380842FEB97BB8 | Event | `public event System.Windows.Forms.UICuesEventHandler ChangeUICues` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-81699C31898DE079 | Event | `public event System.EventHandler Click` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B7415A4F69CC04E0 | Event | `public event System.EventHandler ClientSizeChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B773CA680EE5D7A8 | Event | `public event System.EventHandler ContextMenuChanged` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A806E33101729B02 | Event | `public event System.EventHandler ContextMenuStripChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-768744C54960AD1F | Event | `public event System.Windows.Forms.ControlEventHandler ControlAdded` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3F2715C2374B047E | Event | `public event System.Windows.Forms.ControlEventHandler ControlRemoved` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-370F6211930221AD | Event | `public event System.EventHandler CursorChanged` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6BD0339C517F5D68 | Event | `public event System.EventHandler DataContextChanged` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CBE45F843566DDDC | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9984601347AD1C36 | Event | `public event System.EventHandler DockChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BC7CBA7A2430F610 | Event | `public event System.EventHandler DoubleClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7F53E4341D3FD795 | Event | `public event System.EventHandler DpiChangedAfterParent` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B8B969ED61777E9D | Event | `public event System.EventHandler DpiChangedBeforeParent` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7319C76706AD866C | Event | `public event System.Windows.Forms.DragEventHandler DragDrop` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CF378B5D5C4F3DFA | Event | `public event System.Windows.Forms.DragEventHandler DragEnter` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C45A527C6813C990 | Event | `public event System.EventHandler DragLeave` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B76F46CFE937B82B | Event | `public event System.Windows.Forms.DragEventHandler DragOver` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-05026E9D14300749 | Event | `public event System.EventHandler EnabledChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EF7BFE4011CFED97 | Event | `public event System.EventHandler EndDrag` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C87624D7964D9116 | Event | `public event System.EventHandler Enter` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-352E8AF4CC3F4095 | Event | `public event System.EventHandler FontChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8CAB699D7309B24B | Event | `public event System.EventHandler ForeColorChanged` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-BD7C242A95AB7E3B | Event | `public event System.Windows.Forms.GiveFeedbackEventHandler GiveFeedback` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29887CA74DBAFFE9 | Event | `public event System.EventHandler GotFocus` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AD550ECD328A3603 | Event | `public event System.EventHandler HandleCreated` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-57CA19DEC593A92A | Event | `public event System.EventHandler HandleDestroyed` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CC7F4D4566E24133 | Event | `public event System.Windows.Forms.HelpEventHandler HelpRequested` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1326E0EFC614C355 | Event | `public event System.EventHandler ImeModeChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5E423F7098391BBB | Event | `public event System.Windows.Forms.InvalidateEventHandler Invalidated` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-38C7A7A325D4CEC3 | Event | `public event System.Windows.Forms.ToolStripItemEventHandler ItemAdded` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8BD46E9A6A3CAB3A | Event | `public event System.Windows.Forms.ToolStripItemClickedEventHandler ItemClicked` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-69DDBF88A8CF9717 | Event | `public event System.Windows.Forms.ToolStripItemEventHandler ItemRemoved` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-38CC88B7A5F9EBDD | Event | `public event System.Windows.Forms.KeyEventHandler KeyDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A2CB80FDF9403525 | Event | `public event System.Windows.Forms.KeyPressEventHandler KeyPress` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C8A464AFAC222A8A | Event | `public event System.Windows.Forms.KeyEventHandler KeyUp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1EADE4E2F1E1AEE4 | Event | `public event System.Windows.Forms.LayoutEventHandler Layout` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6702EA1E06AE67F4 | Event | `public event System.EventHandler LayoutCompleted` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7C3CE628789400F5 | Event | `public event System.EventHandler LayoutStyleChanged` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E324983240C30264 | Event | `public event System.EventHandler Leave` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-36E8ACFE0F53F557 | Event | `public event System.EventHandler LocationChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B978E06F484412AE | Event | `public event System.EventHandler LostFocus` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-307A6FB62B517D21 | Event | `public event System.EventHandler MarginChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-18C6995290A2EEFE | Event | `public event System.EventHandler MouseCaptureChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-86341889B9E903D2 | Event | `public event System.Windows.Forms.MouseEventHandler MouseClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-083EACE67DA6DEF0 | Event | `public event System.Windows.Forms.MouseEventHandler MouseDoubleClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0D17159A4FCA4466 | Event | `public event System.Windows.Forms.MouseEventHandler MouseDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DBEA2513E3B67CB9 | Event | `public event System.EventHandler MouseEnter` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A998272E20BCE51E | Event | `public event System.EventHandler MouseHover` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DC23C80240695EBB | Event | `public event System.EventHandler MouseLeave` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DE0ECD6015BF8CF1 | Event | `public event System.Windows.Forms.MouseEventHandler MouseMove` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E55E9B0AE06DECFC | Event | `public event System.Windows.Forms.MouseEventHandler MouseUp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E4C801B28A13804 | Event | `public event System.Windows.Forms.MouseEventHandler MouseWheel` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1D453F57228942C8 | Event | `public event System.EventHandler Move` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4CA73E75B5AA00DC | Event | `public event System.EventHandler PaddingChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1A2EC7F15A247D15 | Event | `public event System.Windows.Forms.PaintEventHandler Paint` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7020504328C0F58C | Event | `public event System.Windows.Forms.PaintEventHandler PaintGrip` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7EE845CEB70D5BA9 | Event | `public event System.EventHandler ParentChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4CCF21D0FB53B816 | Event | `public event System.Windows.Forms.PreviewKeyDownEventHandler PreviewKeyDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3CD3BB065F7985AC | Event | `public event System.Windows.Forms.QueryAccessibilityHelpEventHandler QueryAccessibilityHelp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D3EB737DCC608EC5 | Event | `public event System.Windows.Forms.QueryContinueDragEventHandler QueryContinueDrag` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CA44468CEFE1D832 | Event | `public event System.EventHandler RegionChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-97AC2C5923DDEDC2 | Event | `public event System.EventHandler RendererChanged` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-329C8F92FFFE128A | Event | `public event System.EventHandler Resize` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-8EEA46854395F129 | Event | `public event System.EventHandler RightToLeftChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E38A7B9098AFD56D | Event | `public event System.Windows.Forms.ScrollEventHandler Scroll` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B10BCF6FDBA52D92 | Event | `public event System.EventHandler SizeChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-AD121F61F4881F6C | Event | `public event System.EventHandler StyleChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9433EEC423F016C9 | Event | `public event System.EventHandler SystemColorsChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-025DAC4BCFF089EC | Event | `public event System.EventHandler TabIndexChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FF8DE01846779B5A | Event | `public event System.EventHandler TabStopChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-813384DE015ED03C | Event | `public event System.EventHandler TextChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-49332982B84964DC | Event | `public event System.EventHandler Validated` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4C8B99EB6ED326E0 | Event | `public event System.ComponentModel.CancelEventHandler Validating` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6E8C7B0F1695BE0F | Event | `public event System.EventHandler VisibleChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-71A370D9403279EB | Method | `public System.IAsyncResult BeginInvoke(System.Action method)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7756C67A34A0500E | Method | `public System.IAsyncResult BeginInvoke(System.Delegate method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-40A155E48CE486EE | Method | `public System.IAsyncResult BeginInvoke(System.Delegate method, System.Object[] args)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0EC5B140CCE302A9 | Method | `public System.Void BringToFront()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-68D7ACF09EB0413F | Method | `public System.Boolean Contains(System.Windows.Forms.Control ctl)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F528212543F8CD32 | Method | `public System.Void CreateControl()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F143277FA8FA4B09 | Method | `public System.Drawing.Graphics CreateGraphics()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-76BFBC752DE47233 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-8E84CA8EB659B8B0 | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-58E5D5A5776B8F47 | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C3B61E34403C15D7 | Method | `public System.Windows.Forms.DragDropEffects DoDragDropAsJson<!!0>(!!0 data, System.Windows.Forms.DragDropEffects allowedEffects) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E952CA9F7B33D725 | Method | `public System.Windows.Forms.DragDropEffects DoDragDropAsJson<!!0>(!!0 data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-57816ECEA0E32701 | Method | `public System.Void DrawToBitmap(System.Drawing.Bitmap bitmap, System.Drawing.Rectangle targetBounds)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-84E1929DF0788C35 | Method | `public System.Object EndInvoke(System.IAsyncResult asyncResult)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F96B1C90CAF0A559 | Method | `public System.Windows.Forms.Form FindForm()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6F5FD6D1E0936C70 | Method | `public System.Boolean Focus()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5EF07FA032403FCE | Method | `public static System.Windows.Forms.Control FromChildHandle(System.IntPtr handle)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7E840BEE05D16246 | Method | `public static System.Windows.Forms.Control FromHandle(System.IntPtr handle)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4ECE2DECCFE3862E | Method | `public System.Windows.Forms.Control GetChildAtPoint(System.Drawing.Point point)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-69D39D8617CB4E38 | Method | `public System.Windows.Forms.Control GetChildAtPoint(System.Drawing.Point pt, System.Windows.Forms.GetChildAtPointSkip skipValue)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-75783EF6315D29E7 | Method | `public System.Windows.Forms.IContainerControl GetContainerControl()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9458B5BF00389B00 | Method | `public System.Windows.Forms.ToolStripItem GetItemAt(System.Drawing.Point point)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BCA95A12BDCA7C85 | Method | `public System.Windows.Forms.ToolStripItem GetItemAt(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A1541A545D8C91E | Method | `public System.Windows.Forms.Control GetNextControl(System.Windows.Forms.Control ctl, System.Boolean forward)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C2824A3C4FA955FF | Method | `public virtual System.Windows.Forms.ToolStripItem GetNextItem(System.Windows.Forms.ToolStripItem start, System.Windows.Forms.ArrowDirection direction)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C2E566AAFE328448 | Method | `public virtual System.Drawing.Size GetPreferredSize(System.Drawing.Size proposedSize)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9BD668CF4A28E225 | Method | `public System.Void Hide()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E10602F5CE2BD3C | Method | `public System.Void Invalidate()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A82AA510FC0F694B | Method | `public System.Void Invalidate(System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0D607CA97F6AF62B | Method | `public System.Void Invalidate(System.Drawing.Rectangle rc)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6E0D0E7A703CC33F | Method | `public System.Void Invalidate(System.Drawing.Rectangle rc, System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D5797E08991FDD93 | Method | `public System.Void Invalidate(System.Drawing.Region region)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-23CEC1E8B628D305 | Method | `public System.Void Invalidate(System.Drawing.Region region, System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F6A73B30D3A73A5D | Method | `public !!0 Invoke<!!0>(System.Func&#96;1<!!0> method) where !!0: None;` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0174854400DF3B5E | Method | `public System.Object Invoke(System.Delegate method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3BF14640BCC73AF5 | Method | `public System.Object Invoke(System.Delegate method, System.Object[] args)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-226250F4504E2620 | Method | `public System.Void Invoke(System.Action method)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9E853646BDA516F4 | Method | `public System.Threading.Tasks.Task InvokeAsync(System.Action callback, System.Threading.CancellationToken cancellationToken = null)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-275B21A491BDFDB6 | Method | `public System.Threading.Tasks.Task InvokeAsync(System.Func&#96;2<System.Threading.CancellationToken,System.Threading.Tasks.ValueTask> callback, System.Threading.CancellationToken cancellationToken = null)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7FC6761C06263F92 | Method | `public System.Threading.Tasks.Task&#96;1<!!0> InvokeAsync<!!0>(System.Func&#96;1<!!0> callback, System.Threading.CancellationToken cancellationToken = null) where !!0: None;` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-BCD7B3C83A862FC4 | Method | `public System.Threading.Tasks.Task&#96;1<!!0> InvokeAsync<!!0>(System.Func&#96;2<System.Threading.CancellationToken,System.Threading.Tasks.ValueTask&#96;1<!!0>> callback, System.Threading.CancellationToken cancellationToken = null) where !!0: None;` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1F788A31F7184003 | Method | `public static System.Boolean IsKeyLocked(System.Windows.Forms.Keys keyVal)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FEDEAD25DA0A347D | Method | `public static System.Boolean IsMnemonic(System.Char charCode, System.String text)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6A7686608EE1369C | Method | `public System.Drawing.Size LogicalToDeviceUnits(System.Drawing.Size value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B256361A62BE6D8F | Method | `public System.Int32 LogicalToDeviceUnits(System.Int32 value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-534B4634FCDEA844 | Method | `public System.Void PerformLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B891CB8816A1751B | Method | `public System.Void PerformLayout(System.Windows.Forms.Control affectedControl, System.String affectedProperty)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EE28495DC9305350 | Method | `public System.Drawing.Point PointToClient(System.Drawing.Point p)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BAC2D6E57831A2C5 | Method | `public System.Drawing.Point PointToScreen(System.Drawing.Point p)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-92612FB8D5F373A9 | Method | `public System.Windows.Forms.PreProcessControlState PreProcessControlMessage(ref System.Windows.Forms.Message msg)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5A45EFE41BBD73E1 | Method | `public virtual System.Boolean PreProcessMessage(ref System.Windows.Forms.Message msg)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-00CD8D0DAABA5EA7 | Method | `public System.Drawing.Rectangle RectangleToClient(System.Drawing.Rectangle r)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A5AD9E0D0D489C54 | Method | `public System.Drawing.Rectangle RectangleToScreen(System.Drawing.Rectangle r)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-185489CD854AAE63 | Method | `public virtual System.Void Refresh()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B872B3A5026CCB02 | Method | `public virtual System.Void ResetBackColor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-90664B133BCCD3A5 | Method | `public System.Void ResetBindings()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A2AC1FCDCED715F4 | Method | `public virtual System.Void ResetCursor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC844A559B3AD5CE | Method | `public virtual System.Void ResetFont()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7B414D298079693C | Method | `public virtual System.Void ResetForeColor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E10F72128272B00B | Method | `public System.Void ResetImeMode()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-39E26A73156F6A8E | Method | `public System.Void ResetMinimumSize()` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6A22B8A6D119C6A5 | Method | `public virtual System.Void ResetRightToLeft()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DB3434D5670A0144 | Method | `public virtual System.Void ResetText()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3360ACE61503CD15 | Method | `public System.Void ResumeLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E801F351616F948 | Method | `public System.Void ResumeLayout(System.Boolean performLayout)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-09291FB5AD6A619E | Method | `public System.Void Scale(System.Drawing.SizeF factor)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F9B240F3BDD71E60 | Method | `public System.Void Scale(System.Single dx, System.Single dy)` | `System.Windows.Forms.Control` ObsoleteAttribute("This method has been deprecated. Use the Scale(SizeF ratio) method instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C42369770CEB6A2B | Method | `public System.Void Scale(System.Single ratio)` | `System.Windows.Forms.Control` ObsoleteAttribute("This method has been deprecated. Use the Scale(SizeF ratio) method instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7A6D3FDBE69E8B66 | Method | `public System.Void ScaleBitmapLogicalToDevice(ref System.Drawing.Bitmap logicalBitmap)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B059465CE2FE63A | Method | `public System.Void ScrollControlIntoView(System.Windows.Forms.Control activeControl)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-956F1E070C287211 | Method | `public System.Void Select()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F42A9366A6298C8B | Method | `public System.Boolean SelectNextControl(System.Windows.Forms.Control ctl, System.Boolean forward, System.Boolean tabStopOnly, System.Boolean nested, System.Boolean wrap)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-87606A1DFD9821F9 | Method | `public System.Void SendToBack()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3A7A2DC7F210753D | Method | `public System.Void SetAutoScrollMargin(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-06404D06078C133B | Method | `public System.Void SetBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-54A76F94ABDB839D | Method | `public System.Void SetBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C7557C20E2F66E52 | Method | `public System.Void Show()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F6E56DBA7A855328 | Method | `public System.Void SuspendLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EBAC64951F93B60F | Method | `public virtual System.String ToString()` | `System.Windows.Forms.ToolStrip`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1F499147A44CCD1D | Method | `public System.Void Update()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BB0BB26C39C5DB8B | Property | `System.Windows.Forms.AccessibleObject AccessibilityObject { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D2FEB211A6F9EA1A | Property | `System.String AccessibleDefaultActionDescription { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D6B8822B7DE83A55 | Property | `System.String AccessibleDescription { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-58190AABE630AC5E | Property | `System.String AccessibleName { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B151CD96DB7C8CC | Property | `System.Windows.Forms.AccessibleRole AccessibleRole { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(-1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A55B3CE37E28D0F9 | Property | `System.Boolean AllowClickThrough { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(true); DefaultValueAttribute(false); DesignerSerializationVisibilityAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5359BDD2421A9E5B | Property | `virtual System.Boolean AllowDrop { public get; public set; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16A86126FDAB44CB | Property | `System.Boolean AllowItemReorder { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B99A9267A55D257C | Property | `System.Boolean AllowMerge { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-47B16AFCB5E3C7BA | Property | `virtual System.Windows.Forms.AnchorStyles Anchor { public get; public set; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1E276F190509B558 | Property | `virtual System.Boolean AutoScroll { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3762924DA7B2F255 | Property | `System.Drawing.Size AutoScrollMargin { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F38255921B3C4807 | Property | `System.Drawing.Size AutoScrollMinSize { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1EF57C56A92217C0 | Property | `virtual System.Drawing.Point AutoScrollOffset { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DefaultValueAttribute(System.Drawing.Point,"0, 0") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F36A4E645924CCBE | Property | `System.Drawing.Point AutoScrollPosition { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ACF4229C2CF67F5C | Property | `virtual System.Boolean AutoSize { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(true); BrowsableAttribute(true); DesignerSerializationVisibilityAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EE72D5AFA4610634 | Property | `System.Drawing.Color BackColor { public get; public set; }` | `System.Windows.Forms.ToolStrip`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-10432D4FFD863871 | Property | `virtual System.Drawing.Image BackgroundImage { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BC65641F5EC9884E | Property | `virtual System.Windows.Forms.ImageLayout BackgroundImageLayout { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(1); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2D945A95849E8D4B | Property | `virtual System.Windows.Forms.BindingContext BindingContext { public get; public set; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E6FB241A504F546D | Property | `System.Int32 Bottom { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2D2437EC8B6E1605 | Property | `System.Drawing.Rectangle Bounds { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-49F9813D81BB711E | Property | `System.Boolean CanFocus { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5FEDE1DDF604D923 | Property | `System.Boolean CanOverflow { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2ADF34414C39AAD7 | Property | `System.Boolean CanSelect { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1781EE8523258665 | Property | `System.Boolean Capture { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D1AE157AFAAE2FE | Property | `System.Boolean CausesValidation { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false); DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F31BF9EB8D07F776 | Property | `static System.Boolean CheckForIllegalCrossThreadCalls { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-089A33C0CFB5588A | Property | `System.Drawing.Rectangle ClientRectangle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0E3268388F8A04E4 | Property | `System.Drawing.Size ClientSize { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-8BFCA315361AD41D | Property | `System.String CompanyName { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-08D975C3414C4B5C | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-8DEB3C74752B8F1F | Property | `System.Boolean ContainsFocus { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7B05EC0CC3E9DFD8 | Property | `virtual System.Windows.Forms.ContextMenu ContextMenu { public get; public set; }` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D8BEABCCBC0A983F | Property | `virtual System.Windows.Forms.ContextMenuStrip ContextMenuStrip { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B377D812ACA372C | Property | `System.Windows.Forms.Control+ControlCollection Controls { public get; }` | `System.Windows.Forms.ToolStrip` DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E76EF52BCEC635EB | Property | `System.Boolean Created { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9132A6E6250435EA | Property | `virtual System.Windows.Forms.Cursor Cursor { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8CFC8589FBC5C5BA | Property | `System.Windows.Forms.ControlBindingsCollection DataBindings { public get; }` | `System.Windows.Forms.Control` DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-14A1163C53A5084F | Property | `virtual System.Object DataContext { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); BindableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F613CE8D8FB2625F | Property | `static System.Drawing.Color DefaultBackColor { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1560C371D8065E01 | Property | `virtual System.Windows.Forms.ToolStripDropDownDirection DefaultDropDownDirection { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-15F760E42302FE64 | Property | `static System.Drawing.Font DefaultFont { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-36320569DF6D9BF2 | Property | `static System.Drawing.Color DefaultForeColor { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D1A69549CBA8D6E9 | Property | `System.Int32 DeviceDpi { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-07FD00E9EAA83BD6 | Property | `virtual System.Drawing.Rectangle DisplayRectangle { public get; }` | `System.Windows.Forms.ToolStrip`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5ADD6089E434C465 | Property | `System.Boolean Disposing { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-93092D7B5656469C | Property | `virtual System.Windows.Forms.DockStyle Dock { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(1) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7AD3EB5D5C7542D0 | Property | `System.Windows.Forms.ScrollableControl+DockPaddingEdges DockPadding { public get; }` | `System.Windows.Forms.ScrollableControl` DesignerSerializationVisibilityAttribute(0); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-545A3829D0ACACCE | Property | `System.Boolean Enabled { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-516DE63670D20F01 | Property | `virtual System.Boolean Focused { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-908246A548543F40 | Property | `virtual System.Drawing.Font Font { public get; public set; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-81F7BBC48846184C | Property | `System.Drawing.Color ForeColor { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1863CE2C3FC5071A | Property | `System.Windows.Forms.ToolStripGripDisplayStyle GripDisplayStyle { public get; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E6BDFA0613AECC3B | Property | `System.Windows.Forms.Padding GripMargin { public get; public set; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-564B8CB7FED915F6 | Property | `System.Drawing.Rectangle GripRectangle { public get; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E59518EE66FEE8B2 | Property | `System.Windows.Forms.ToolStripGripStyle GripStyle { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(1) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-29722CCAC1889BB8 | Property | `System.IntPtr Handle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1F1D23046B4D38CD | Property | `System.Boolean HasChildren { public get; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1D4AA45F6F5D7D35 | Property | `System.Int32 Height { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7A43D75690051A92 | Property | `System.Windows.Forms.HScrollProperties HorizontalScroll { public get; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BA725389E2856D0F | Property | `System.Windows.Forms.ImageList ImageList { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(null); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BEACDAA38CF277A8 | Property | `System.Drawing.Size ImageScalingSize { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(System.Drawing.Size,"16,16") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BE3F1E1527461A26 | Property | `System.Windows.Forms.ImeMode ImeMode { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6A0FF2E2CA9A394B | Property | `System.Boolean InvokeRequired { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2F00A6F67665A7C7 | Property | `System.Boolean IsAccessible { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5E1DFF6AFADF5DD8 | Property | `System.Boolean IsAncestorSiteInDesignMode { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8B7EA223CDD4E2A0 | Property | `System.Boolean IsCurrentlyDragging { public get; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C94D8D7B1FD4EB8D | Property | `System.Boolean IsDisposed { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0C9AB21F360C04AF | Property | `System.Boolean IsDropDown { public get; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-28CCFEE48B37269C | Property | `System.Boolean IsHandleCreated { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DD9DDEC5A2CB62BF | Property | `System.Boolean IsMirrored { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6D33380B0609058C | Property | `virtual System.Windows.Forms.ToolStripItemCollection Items { public get; }` | `System.Windows.Forms.ToolStrip` DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FFFF13B6562B500C | Property | `virtual System.Windows.Forms.Layout.LayoutEngine LayoutEngine { public get; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4DDC620856B5C185 | Property | `System.Windows.Forms.LayoutSettings LayoutSettings { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(null); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AF403AA4B600CDC1 | Property | `System.Windows.Forms.ToolStripLayoutStyle LayoutStyle { public get; public set; }` | `System.Windows.Forms.ToolStrip`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FF54683F27ADFB35 | Property | `System.Int32 Left { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-706C08C87B06953D | Property | `System.Drawing.Point Location { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7B9DCC70C7973B88 | Property | `System.Windows.Forms.Padding Margin { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4830F329169A52B7 | Property | `virtual System.Drawing.Size MaximumSize { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-915365CB22606261 | Property | `virtual System.Drawing.Size MinimumSize { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5FBB7108713D972D | Property | `static System.Windows.Forms.Keys ModifierKeys { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A7C93BC95775C67E | Property | `static System.Windows.Forms.MouseButtons MouseButtons { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8B1DA114A0D96F86 | Property | `static System.Drawing.Point MousePosition { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0DC615525279552 | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B4F41EDDC0FC6229 | Property | `System.Windows.Forms.Orientation Orientation { public get; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DB0C7570406B9FB2 | Property | `System.Windows.Forms.ToolStripOverflowButton OverflowButton { public get; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EDEAFEC5ED5326E7 | Property | `System.Windows.Forms.Padding Padding { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4A97458C3A5A890B | Property | `System.Windows.Forms.Control Parent { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-01BAC7D9EA81C3F1 | Property | `System.Drawing.Size PreferredSize { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0DD490BE1EE5A625 | Property | `System.String ProductName { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-40151CCEE2A0F9F4 | Property | `System.String ProductVersion { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-30D48416388FE0B4 | Property | `System.Boolean RecreatingHandle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B3F94499770036E0 | Property | `System.Drawing.Region Region { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7C2BF17EAAB487DF | Property | `System.Windows.Forms.ToolStripRenderer Renderer { public get; public set; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2C705A73BBD920DB | Property | `System.Windows.Forms.ToolStripRenderMode RenderMode { public get; public set; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-795299755DD43A99 | Property | `System.Int32 Right { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EBB5690D5EE55E55 | Property | `virtual System.Windows.Forms.RightToLeft RightToLeft { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-104E674326E05B1B | Property | `System.Boolean ShowItemToolTips { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2E9499249BBB311C | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7C5FB2CAA55A87B1 | Property | `System.Drawing.Size Size { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-92E5A62CFC9A6762 | Property | `System.Boolean Stretch { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D00E1CDB5D60A72C | Property | `System.Int32 TabIndex { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-14721344CBF65E45 | Property | `System.Boolean TabStop { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-8FE393783238DB5E | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0193DA0859CC978B | Property | `virtual System.String Text { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true); BindableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FDF7852894D8F652 | Property | `virtual System.Windows.Forms.ToolStripTextDirection TextDirection { public get; public set; }` | `System.Windows.Forms.ToolStrip` DefaultValueAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-360A3AAFBAEDD522 | Property | `System.Int32 Top { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7CCE842E94C15E72 | Property | `System.Windows.Forms.Control TopLevelControl { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-93E0CD7A47A387B9 | Property | `System.Boolean UseWaitCursor { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(false); BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-18695AF5EF3BBD69 | Property | `System.Windows.Forms.VScrollProperties VerticalScroll { public get; }` | `System.Windows.Forms.ToolStrip` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C2126625A4994325 | Property | `System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-DC1C28FD21D10327 | Property | `System.Int32 Width { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-BC6E8273418AAE3C | Property | `System.Windows.Forms.IWindowTarget WindowTarget { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-3816153E9FCC1992 | Field | `protected const System.Int32 ScrollStateAutoScrolling = 1` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BE2D73AB3AA56544 | Field | `protected const System.Int32 ScrollStateFullDrag = 16` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B7B29CBEB1272074 | Field | `protected const System.Int32 ScrollStateHScrollVisible = 2` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E116CC1C6C52CB1E | Field | `protected const System.Int32 ScrollStateUserHasScrolled = 8` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B6275950DCA550E3 | Field | `protected const System.Int32 ScrollStateVScrollVisible = 4` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CA1679D9ACE218DB | Method | `protected System.Void AccessibilityNotifyClients(System.Windows.Forms.AccessibleEvents accEvent, System.Int32 objectID, System.Int32 childID)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FC324BDE1705836B | Method | `protected internal System.Void AccessibilityNotifyClients(System.Windows.Forms.AccessibleEvents accEvent, System.Int32 childID)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0EFAAC2B008E5C47 | Method | `protected virtual System.Void AdjustFormScrollbars(System.Boolean displayScrollbars)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F8CF25F3D2318614 | Method | `protected virtual System.Windows.Forms.AccessibleObject CreateAccessibilityInstance()` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-102872B0C0969BEE | Method | `protected virtual System.Windows.Forms.Control+ControlCollection CreateControlsInstance()` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-035685F8485D653F | Method | `protected internal virtual System.Windows.Forms.ToolStripItem CreateDefaultItem(System.String text, System.Drawing.Image image, System.EventHandler onClick)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F1812615FEC5B632 | Method | `protected virtual System.Void CreateHandle()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F97D1979A5FCE959 | Method | `protected virtual System.Windows.Forms.LayoutSettings CreateLayoutSettings(System.Windows.Forms.ToolStripLayoutStyle layoutStyle)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8873AA7C94BA4212 | Method | `protected virtual System.Void DefWndProc(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9503805869543640 | Method | `protected virtual System.Void DestroyHandle()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-031CFBD579174A5F | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.ToolStrip`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-03B04253E8046F38 | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-12EF57528B9FB412 | Method | `protected virtual System.Windows.Forms.AccessibleObject GetAccessibilityObjectById(System.Int32 objectId)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E034DA1B6D41DBAA | Method | `protected System.Windows.Forms.AutoSizeMode GetAutoSizeMode()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-080C1D6A41E49334 | Method | `protected virtual System.Drawing.Rectangle GetScaledBounds(System.Drawing.Rectangle bounds, System.Drawing.SizeF factor, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-62B2014B16FD0F11 | Method | `protected System.Boolean GetScrollState(System.Int32 bit)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-26C85AA826C0B642 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3D005E94198B01B1 | Method | `protected System.Boolean GetStyle(System.Windows.Forms.ControlStyles flag)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E25697AD6E17375 | Method | `protected System.Boolean GetTopLevel()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8C492CC0BBBBC0EE | Method | `protected virtual System.Void InitLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-99F62A7142FA714A | Method | `protected System.Void InvokeGotFocus(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-620E1BCD3925A1A4 | Method | `protected System.Void InvokeLostFocus(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1A18F372A26AD030 | Method | `protected System.Void InvokeOnClick(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-780F25804D88DBE3 | Method | `protected System.Void InvokePaint(System.Windows.Forms.Control c, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-25BA89DEA0A1D296 | Method | `protected System.Void InvokePaintBackground(System.Windows.Forms.Control c, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-89DB7FA40CB8D075 | Method | `protected virtual System.Boolean IsInputChar(System.Char charCode)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DA3C11B17193BB65 | Method | `protected virtual System.Boolean IsInputKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-11D232EF5D39C7F3 | Method | `protected virtual System.Void NotifyInvalidate(System.Drawing.Rectangle invalidatedArea)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-59489922D033710B | Method | `protected virtual System.Void OnAutoSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3C4A9C182B54390C | Method | `protected virtual System.Void OnBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-85A01431E5CDF9E4 | Method | `protected virtual System.Void OnBackgroundImageChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A9E6CD609153EA93 | Method | `protected virtual System.Void OnBackgroundImageLayoutChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A0A26C292395655A | Method | `protected virtual System.Void OnBeginDrag(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-751C84E3613E6800 | Method | `protected virtual System.Void OnBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-63ED562627C0B408 | Method | `protected virtual System.Void OnCausesValidationChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D16C377393B9746 | Method | `protected virtual System.Void OnChangeUICues(System.Windows.Forms.UICuesEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-51EF5A3D47FE93AE | Method | `protected virtual System.Void OnClick(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2F023D89A9899E56 | Method | `protected virtual System.Void OnClientSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0D2BCE44FC7DC997 | Method | `protected virtual System.Void OnContextMenuChanged(System.EventArgs e)` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BA6E9E2CAA40C4A9 | Method | `protected virtual System.Void OnContextMenuStripChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC1DAEBDC0343C76 | Method | `protected virtual System.Void OnControlAdded(System.Windows.Forms.ControlEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9ADF5025804EC653 | Method | `protected virtual System.Void OnControlRemoved(System.Windows.Forms.ControlEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1AD271277BFB0802 | Method | `protected virtual System.Void OnCreateControl()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4ECC57A138A3213B | Method | `protected virtual System.Void OnCursorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2CEA503F9FD0EC8E | Method | `protected virtual System.Void OnDataContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5B773457C682DFC7 | Method | `protected virtual System.Void OnDockChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-46792CD4784EA671 | Method | `protected virtual System.Void OnDoubleClick(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8754910DA1B6604C | Method | `protected virtual System.Void OnDpiChangedAfterParent(System.EventArgs e)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2AE6768AE5880D77 | Method | `protected virtual System.Void OnDpiChangedBeforeParent(System.EventArgs e)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B26D973787AC95CF | Method | `protected virtual System.Void OnDragDrop(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FAD0F6EF9589A499 | Method | `protected virtual System.Void OnDragEnter(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8C7F5470DD7232B1 | Method | `protected virtual System.Void OnDragLeave(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C98EAC9FEAE5BF8C | Method | `protected virtual System.Void OnDragOver(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E98CF5F21E58B61C | Method | `protected virtual System.Void OnEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7BEED3B6E142BAEC | Method | `protected virtual System.Void OnEndDrag(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6EF609EEA7A82BE9 | Method | `protected internal virtual System.Void OnEnter(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-00967B1770B0498B | Method | `protected virtual System.Void OnFontChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4CBEDD3318522A3E | Method | `protected virtual System.Void OnForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-096339023393A739 | Method | `protected virtual System.Void OnGiveFeedback(System.Windows.Forms.GiveFeedbackEventArgs gfbevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B0D3E0F917F1D037 | Method | `protected virtual System.Void OnGotFocus(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CBD7E1F98CB4B0A1 | Method | `protected virtual System.Void OnHandleCreated(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ED6093E60A6821FE | Method | `protected virtual System.Void OnHandleDestroyed(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B6D75A9C8946ABC3 | Method | `protected virtual System.Void OnHelpRequested(System.Windows.Forms.HelpEventArgs hevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B999248318A4FCD2 | Method | `protected virtual System.Void OnImeModeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-65403B91BC3D105B | Method | `protected virtual System.Void OnInvalidated(System.Windows.Forms.InvalidateEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-09CA4EF3AC2563BC | Method | `protected internal virtual System.Void OnItemAdded(System.Windows.Forms.ToolStripItemEventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BD10ED55B571CBD1 | Method | `protected virtual System.Void OnItemClicked(System.Windows.Forms.ToolStripItemClickedEventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-94C4BF36FD642B58 | Method | `protected internal virtual System.Void OnItemRemoved(System.Windows.Forms.ToolStripItemEventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3F6DECAB266F5606 | Method | `protected virtual System.Void OnKeyDown(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F4B307E57FACC5C0 | Method | `protected virtual System.Void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-28C9BF8EB92F7A9E | Method | `protected virtual System.Void OnKeyUp(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-82B05BB58C8BFFB6 | Method | `protected virtual System.Void OnLayout(System.Windows.Forms.LayoutEventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-48BA4A1A560BDFDF | Method | `protected virtual System.Void OnLayoutCompleted(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D8B8AA7E81B22A0 | Method | `protected virtual System.Void OnLayoutStyleChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-055654CABB0B6E10 | Method | `protected internal virtual System.Void OnLeave(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E85E595364535CC3 | Method | `protected virtual System.Void OnLocationChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-492FAB4CDB0D5DD0 | Method | `protected virtual System.Void OnLostFocus(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3BB37CC153CBD65D | Method | `protected virtual System.Void OnMarginChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7CEC900442A0C9ED | Method | `protected virtual System.Void OnMouseCaptureChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7AFADD18A56A19C9 | Method | `protected virtual System.Void OnMouseClick(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D0799F8629735E39 | Method | `protected virtual System.Void OnMouseDoubleClick(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C75A8388A619D6E5 | Method | `protected virtual System.Void OnMouseDown(System.Windows.Forms.MouseEventArgs mea)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9D79A93AC023CB7D | Method | `protected virtual System.Void OnMouseEnter(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C7B6140ECFA5285C | Method | `protected virtual System.Void OnMouseHover(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-87CD3EEBBE7706CD | Method | `protected virtual System.Void OnMouseLeave(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0ABF402728EB78C2 | Method | `protected virtual System.Void OnMouseMove(System.Windows.Forms.MouseEventArgs mea)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9BDB4B1B85FD0D17 | Method | `protected virtual System.Void OnMouseUp(System.Windows.Forms.MouseEventArgs mea)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B4E97E327C472DD | Method | `protected virtual System.Void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-062ECBF1C51EBBAD | Method | `protected virtual System.Void OnMove(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A5B3E5F9655FF800 | Method | `protected virtual System.Void OnNotifyMessage(System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-12B132C75C52CA15 | Method | `protected virtual System.Void OnPaddingChanged(System.EventArgs e)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5201028698DB6E14 | Method | `protected virtual System.Void OnPaint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D341953E7477A3E1 | Method | `protected virtual System.Void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EE96FD83F453D683 | Method | `protected internal virtual System.Void OnPaintGrip(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-511346DB227049DB | Method | `protected virtual System.Void OnParentBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D5966DAC3E78A606 | Method | `protected virtual System.Void OnParentBackgroundImageChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-96891B1EFBD3A6F1 | Method | `protected virtual System.Void OnParentBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DB580500C4604807 | Method | `protected virtual System.Void OnParentChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-64BD0C786E48E49F | Method | `protected virtual System.Void OnParentCursorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DE242F6774AB83D2 | Method | `protected virtual System.Void OnParentDataContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2584BB78F656D221 | Method | `protected virtual System.Void OnParentEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B38D30A45EF73E48 | Method | `protected virtual System.Void OnParentFontChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2ABADB10BE7AD1F9 | Method | `protected virtual System.Void OnParentForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E95F27DF86E1272 | Method | `protected virtual System.Void OnParentRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-03A3F6DC29E24390 | Method | `protected virtual System.Void OnParentVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-58DF4D07005319FB | Method | `protected virtual System.Void OnPreviewKeyDown(System.Windows.Forms.PreviewKeyDownEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7A848924E8C0995B | Method | `protected virtual System.Void OnPrint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F1DE47E091E48F23 | Method | `protected virtual System.Void OnQueryContinueDrag(System.Windows.Forms.QueryContinueDragEventArgs qcdevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BC2AF6C29560C4E6 | Method | `protected virtual System.Void OnRegionChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-844710F811428D86 | Method | `protected virtual System.Void OnRendererChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-07B49ADB67AABB89 | Method | `protected virtual System.Void OnResize(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0366D69594357B65 | Method | `protected virtual System.Void OnRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CDC3D9F707F53E3F | Method | `protected virtual System.Void OnScroll(System.Windows.Forms.ScrollEventArgs se)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-09356D97D65F2CB3 | Method | `protected virtual System.Void OnSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CF1AD60B5AA3559D | Method | `protected virtual System.Void OnStyleChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E65625E9A3E99095 | Method | `protected virtual System.Void OnSystemColorsChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8BA3FC0F672A25D0 | Method | `protected virtual System.Void OnTabIndexChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FEFFD2CAE703BEB3 | Method | `protected virtual System.Void OnTabStopChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E8F45A508E99D096 | Method | `protected virtual System.Void OnTextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FC18FA54F90C4D5A | Method | `protected virtual System.Void OnValidated(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-096C6F5A5E2F9942 | Method | `protected virtual System.Void OnValidating(System.ComponentModel.CancelEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C7C94DF3C9B5CCE | Method | `protected virtual System.Void OnVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0883A0510E0D57BB | Method | `protected virtual System.Boolean ProcessCmdKey(ref System.Windows.Forms.Message m, System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B6776B2462A795C | Method | `protected virtual System.Boolean ProcessDialogChar(System.Char charCode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-12CE14C8487B23AE | Method | `protected virtual System.Boolean ProcessDialogKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BCF7F4AD7F812E0E | Method | `protected virtual System.Boolean ProcessKeyEventArgs(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E3EB4C7440872914 | Method | `protected internal virtual System.Boolean ProcessKeyMessage(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC8A753F00F022F0 | Method | `protected virtual System.Boolean ProcessKeyPreview(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-036896C81E59619C | Method | `protected internal virtual System.Boolean ProcessMnemonic(System.Char charCode)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EF129AE06A8C0479 | Method | `protected System.Void RaiseDragEvent(System.Object key, System.Windows.Forms.DragEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-81D7DD652550EEFD | Method | `protected System.Void RaiseKeyEvent(System.Object key, System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D3A842AE7758B0BD | Method | `protected System.Void RaiseMouseEvent(System.Object key, System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B2290335EFE1BE8 | Method | `protected System.Void RaisePaintEvent(System.Object key, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1C2C96A8E41B344A | Method | `protected System.Void RecreateHandle()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-824EC31FF0403E34 | Method | `protected static System.Boolean ReflectMessage(System.IntPtr hWnd, ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FAE015737DEC4D4A | Method | `protected virtual System.Void RescaleConstantsForDpi(System.Int32 deviceDpiOld, System.Int32 deviceDpiNew)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-797E0249194E9604 | Method | `protected System.Void ResetMouseEventArgs()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3B1E63A5A87C2D6D | Method | `protected virtual System.Void RestoreFocus()` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A539A815E00B9E60 | Method | `protected System.Drawing.ContentAlignment RtlTranslateAlignment(System.Drawing.ContentAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E5E23EA6A8127808 | Method | `protected System.Windows.Forms.HorizontalAlignment RtlTranslateAlignment(System.Windows.Forms.HorizontalAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2C95FCA993ED9697 | Method | `protected System.Windows.Forms.LeftRightAlignment RtlTranslateAlignment(System.Windows.Forms.LeftRightAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DA62A07DE30C796F | Method | `protected internal System.Drawing.ContentAlignment RtlTranslateContent(System.Drawing.ContentAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F78BBFA41244C848 | Method | `protected System.Windows.Forms.HorizontalAlignment RtlTranslateHorizontal(System.Windows.Forms.HorizontalAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-46ABEDCE740CF53F | Method | `protected System.Windows.Forms.LeftRightAlignment RtlTranslateLeftRight(System.Windows.Forms.LeftRightAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D1FB50D8B92BDBF3 | Method | `protected virtual System.Void ScaleControl(System.Drawing.SizeF factor, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-02D2761FB042E466 | Method | `protected virtual System.Void ScaleCore(System.Single dx, System.Single dy)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-085F3D18EE55A617 | Method | `protected virtual System.Drawing.Point ScrollToControl(System.Windows.Forms.Control activeControl)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-32D6002853A8A444 | Method | `protected virtual System.Void Select(System.Boolean directed, System.Boolean forward)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1C1A14B79B4A5B91 | Method | `protected System.Void SetAutoSizeMode(System.Windows.Forms.AutoSizeMode mode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2986FAE4DC7FAFAE | Method | `protected virtual System.Void SetBoundsCore(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.ToolStrip`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D32DDADF07190BC2 | Method | `protected virtual System.Void SetClientSizeCore(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-AB39B4501C68D266 | Method | `protected virtual System.Void SetDisplayedItems()` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C5F986C25095BF28 | Method | `protected System.Void SetDisplayRectLocation(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4FDB2982BDAA8AF3 | Method | `protected internal System.Void SetItemLocation(System.Windows.Forms.ToolStripItem item, System.Drawing.Point location)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-567BC6A145FB37AF | Method | `protected static System.Void SetItemParent(System.Windows.Forms.ToolStripItem item, System.Windows.Forms.ToolStrip parent)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B03BA7AAE3B47B1A | Method | `protected System.Void SetScrollState(System.Int32 bit, System.Boolean value)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7650CF646A9109AA | Method | `protected System.Void SetStyle(System.Windows.Forms.ControlStyles flag, System.Boolean value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D8934C7F9BB861D | Method | `protected System.Void SetTopLevel(System.Boolean value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CD904978ABF80A12 | Method | `protected virtual System.Void SetVisibleCore(System.Boolean visible)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-351608FBEDAD97AE | Method | `protected virtual System.Drawing.Size SizeFromClientSize(System.Drawing.Size clientSize)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-00F2318BF266378F | Method | `protected System.Void UpdateBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-585F1FFE41A4A590 | Method | `protected System.Void UpdateBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Int32 clientWidth, System.Int32 clientHeight)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-993E79CB128D3328 | Method | `protected internal System.Void UpdateBounds()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F80C481A2E6B269C | Method | `protected System.Void UpdateStyles()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7A5F2536C751F2FB | Method | `protected System.Void UpdateZOrder()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D7FEEEC00C4BDF67 | Method | `protected virtual System.Void WndProc(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B9C6B57CBBBC282F | Property | `virtual System.Boolean CanEnableIme { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F7D89D2BA97636CB | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-238DF929DD304418 | Property | `virtual System.Windows.Forms.CreateParams CreateParams { protected get; }` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2A07A3640EAC984F | Property | `virtual System.Windows.Forms.Cursor DefaultCursor { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-728CDFB1FD0A10E8 | Property | `virtual System.Windows.Forms.DockStyle DefaultDock { protected get; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EACA10F085DD23B9 | Property | `virtual System.Windows.Forms.Padding DefaultGripMargin { protected get; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-391A00F0510CA532 | Property | `virtual System.Windows.Forms.ImeMode DefaultImeMode { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6F0B84BB5A3B6A32 | Property | `virtual System.Windows.Forms.Padding DefaultMargin { protected get; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7998F9F339E014FE | Property | `virtual System.Drawing.Size DefaultMaximumSize { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B1802C764F3F6504 | Property | `virtual System.Drawing.Size DefaultMinimumSize { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-27F7947064A5FBA6 | Property | `virtual System.Windows.Forms.Padding DefaultPadding { protected get; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D7EBFEEA51582B54 | Property | `virtual System.Boolean DefaultShowItemToolTips { protected get; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-761A223B7C4FD645 | Property | `virtual System.Drawing.Size DefaultSize { protected get; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B2B148E14468BB81 | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C5167C1A93A31780 | Property | `virtual System.Windows.Forms.ToolStripItemCollection DisplayedItems { protected internal get; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2F3A23925D6433E3 | Property | `virtual System.Boolean DoubleBuffered { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F4BC996B91066D3E | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B54700DD4D2D6ACF | Property | `System.Int32 FontHeight { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6129C4FEC2314C43 | Property | `System.Boolean HScroll { protected get; protected set; }` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DD808BECD4778C3B | Property | `virtual System.Windows.Forms.ImeMode ImeModeBase { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-61FCF693F58E0099 | Property | `virtual System.Drawing.Size MaxItemSize { protected internal get; }` | `System.Windows.Forms.ToolStrip`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-03BB10CDAF9562BB | Property | `static System.Windows.Forms.ImeMode PropagatingImeMode { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A6F1319592AFD074 | Property | `System.Boolean RenderRightToLeft { protected internal get; }` | `System.Windows.Forms.Control` ObsoleteAttribute("This property has been deprecated. Please use RightToLeft instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-10B530738EC441D9 | Property | `System.Boolean ResizeRedraw { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6416FF5FC870A00F | Property | `virtual System.Boolean ScaleChildren { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-09DF16A47A7E2C64 | Property | `virtual System.Boolean ShowFocusCues { protected internal get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C29A56A5DA7BB6FF | Property | `virtual System.Boolean ShowKeyboardCues { protected internal get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A2E36E6B47C10DC4 | Property | `System.Boolean VScroll { protected get; protected set; }` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
