# PrintPreviewDialog：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/PrintPreviewDialog.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.PrintPreviewDialog`；基类：`System.Windows.Forms.Form`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **146**，公开事件 **99**，公开方法 **95**；受保护扩展成员 **216**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-3CA5C207B9F7E1E1 | Constructor | `public PrintPreviewDialog()` | `System.Windows.Forms.PrintPreviewDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F2441AEC1C6002A | Event | `public event System.EventHandler Activated` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8ED69E1BCF055746 | Event | `public event System.EventHandler AutoSizeChanged` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ADE92C1DE38C69F7 | Event | `public event System.EventHandler AutoValidateChanged` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3F85E9263BB340BA | Event | `public event System.EventHandler BackColorChanged` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7E8CA8F13961C4B0 | Event | `public event System.EventHandler BackgroundImageChanged` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6E9497DDA1173923 | Event | `public event System.EventHandler BackgroundImageLayoutChanged` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DEF3C2B2748B14E0 | Event | `public event System.EventHandler BindingContextChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C1A061EB31177C3A | Event | `public event System.EventHandler CausesValidationChanged` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-82CE4507BEAE31BF | Event | `public event System.Windows.Forms.UICuesEventHandler ChangeUICues` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8762D28843325620 | Event | `public event System.EventHandler Click` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DBBF7301A2D8D49A | Event | `public event System.EventHandler ClientSizeChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-85C9025F018F6F14 | Event | `public event System.EventHandler Closed` | `System.Windows.Forms.Form` BrowsableAttribute(false); ObsoleteAttribute("Form.OnClosing, Form.OnClosed and the corresponding events are obsolete. Use Form.OnFormClosing, Form.OnFormClosed, Form.FormClosing and Form.FormClosed instead.",false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2C4929523526D8C9 | Event | `public event System.ComponentModel.CancelEventHandler Closing` | `System.Windows.Forms.Form` BrowsableAttribute(false); ObsoleteAttribute("Form.OnClosing, Form.OnClosed and the corresponding events are obsolete. Use Form.OnFormClosing, Form.OnFormClosed, Form.FormClosing and Form.FormClosed instead.",false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-397D74E67A48E9D3 | Event | `public event System.EventHandler ContextMenuChanged` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-21951F5F2A33635C | Event | `public event System.EventHandler ContextMenuStripChanged` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-53D8D24B1E48772B | Event | `public event System.Windows.Forms.ControlEventHandler ControlAdded` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BCE88B1ACE876831 | Event | `public event System.Windows.Forms.ControlEventHandler ControlRemoved` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8A36F681F0EE8B07 | Event | `public event System.EventHandler CursorChanged` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-376811091FB39540 | Event | `public event System.EventHandler DataContextChanged` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-993F7BF3395067D1 | Event | `public event System.EventHandler Deactivate` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-59C10AA03D26B241 | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CEE771418B30A2B2 | Event | `public event System.EventHandler DockChanged` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4B7FFA8FC4350A06 | Event | `public event System.EventHandler DoubleClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-111186B16808E1B8 | Event | `public event System.Windows.Forms.DpiChangedEventHandler DpiChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4F6B8A024A06609A | Event | `public event System.EventHandler DpiChangedAfterParent` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6E0F6A2584C76757 | Event | `public event System.EventHandler DpiChangedBeforeParent` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BF52B77352F936FA | Event | `public event System.Windows.Forms.DragEventHandler DragDrop` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1F2389891913249E | Event | `public event System.Windows.Forms.DragEventHandler DragEnter` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E7EA16D683EE024 | Event | `public event System.EventHandler DragLeave` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-90A0EAA2FD2DC044 | Event | `public event System.Windows.Forms.DragEventHandler DragOver` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6230041899D5F0F1 | Event | `public event System.EventHandler EnabledChanged` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-338300C9CBD67F63 | Event | `public event System.EventHandler Enter` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0D253D686EB3C3D6 | Event | `public event System.EventHandler FontChanged` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AAA200FABE8B1CE8 | Event | `public event System.EventHandler ForeColorChanged` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A71267A1D1EB0BBA | Event | `public event System.EventHandler FormBorderColorChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-620D12F7289CE880 | Event | `public event System.EventHandler FormCaptionBackColorChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E1E9B591D6A88AE7 | Event | `public event System.EventHandler FormCaptionTextColorChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-45856AB1004F6A00 | Event | `public event System.Windows.Forms.FormClosedEventHandler FormClosed` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-05D0857B84EE9E87 | Event | `public event System.Windows.Forms.FormClosingEventHandler FormClosing` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-21751FC41161E17C | Event | `public event System.EventHandler FormCornerPreferenceChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FB8C825671365A84 | Event | `public event System.Windows.Forms.GiveFeedbackEventHandler GiveFeedback` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6C6AD02F27F3F36E | Event | `public event System.EventHandler GotFocus` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-22A4E07445E543F5 | Event | `public event System.EventHandler HandleCreated` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-930F87FC2C9D4B3F | Event | `public event System.EventHandler HandleDestroyed` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A7C6016F9E8A0DAF | Event | `public event System.ComponentModel.CancelEventHandler HelpButtonClicked` | `System.Windows.Forms.Form` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D02187B38D2C5DA4 | Event | `public event System.Windows.Forms.HelpEventHandler HelpRequested` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CB84CE31A750C058 | Event | `public event System.EventHandler ImeModeChanged` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C4292A344671A555 | Event | `public event System.Windows.Forms.InputLanguageChangedEventHandler InputLanguageChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5089C4A61F426093 | Event | `public event System.Windows.Forms.InputLanguageChangingEventHandler InputLanguageChanging` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CA5BED72286F0B24 | Event | `public event System.Windows.Forms.InvalidateEventHandler Invalidated` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-30596B12D9BEA2E8 | Event | `public event System.Windows.Forms.KeyEventHandler KeyDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-276C3948EB6F28B7 | Event | `public event System.Windows.Forms.KeyPressEventHandler KeyPress` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-58EF6DA5C8ADDAE9 | Event | `public event System.Windows.Forms.KeyEventHandler KeyUp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BDF7891BD55C1F56 | Event | `public event System.Windows.Forms.LayoutEventHandler Layout` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-150387862D94A424 | Event | `public event System.EventHandler Leave` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5B24EEEA70C61818 | Event | `public event System.EventHandler Load` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-97FE9972DA4E093C | Event | `public event System.EventHandler LocationChanged` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C8BB6E02A783EA4E | Event | `public event System.EventHandler LostFocus` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C4AB0C0D24C1684F | Event | `public event System.EventHandler MarginChanged` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A0B79891F1C1F3B5 | Event | `public event System.EventHandler MaximizedBoundsChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-088A1226E3C80878 | Event | `public event System.EventHandler MaximumSizeChanged` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F49747ADFCF0B7D | Event | `public event System.EventHandler MdiChildActivate` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E14CB05BCB57E1B8 | Event | `public event System.EventHandler MenuComplete` | `System.Windows.Forms.Form` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-76130B6C523E98BD | Event | `public event System.EventHandler MenuStart` | `System.Windows.Forms.Form` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F80E5858CC39A9C4 | Event | `public event System.EventHandler MinimumSizeChanged` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-162E034ECE596CAF | Event | `public event System.EventHandler MouseCaptureChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-797B062BCD4862FA | Event | `public event System.Windows.Forms.MouseEventHandler MouseClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5DCD9E8A469B44AE | Event | `public event System.Windows.Forms.MouseEventHandler MouseDoubleClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E06C37650364253 | Event | `public event System.Windows.Forms.MouseEventHandler MouseDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0343DFD969214645 | Event | `public event System.EventHandler MouseEnter` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-124437139516B91E | Event | `public event System.EventHandler MouseHover` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5C30F52ADDE8922F | Event | `public event System.EventHandler MouseLeave` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-75E4CD07B751D71F | Event | `public event System.Windows.Forms.MouseEventHandler MouseMove` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B809A9BF5B61B717 | Event | `public event System.Windows.Forms.MouseEventHandler MouseUp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-680F5EE7533EF4AE | Event | `public event System.Windows.Forms.MouseEventHandler MouseWheel` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6DFFA61589FDF15C | Event | `public event System.EventHandler Move` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B43DE2D751E1A4AD | Event | `public event System.EventHandler PaddingChanged` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F9A774CA0247A996 | Event | `public event System.Windows.Forms.PaintEventHandler Paint` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D4B00A58938B0BA | Event | `public event System.EventHandler ParentChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-95BE1E984A9EFE00 | Event | `public event System.Windows.Forms.PreviewKeyDownEventHandler PreviewKeyDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-09459B2073064F19 | Event | `public event System.Windows.Forms.QueryAccessibilityHelpEventHandler QueryAccessibilityHelp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4C7ACAD4CC3CCDE6 | Event | `public event System.Windows.Forms.QueryContinueDragEventHandler QueryContinueDrag` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-20A96B38BEB87708 | Event | `public event System.EventHandler RegionChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3F2C10EEF3BC5694 | Event | `public event System.EventHandler Resize` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C984652845B2BD0B | Event | `public event System.EventHandler ResizeBegin` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC45A2F5B5BF1A65 | Event | `public event System.EventHandler ResizeEnd` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-67850FC4A33D75A5 | Event | `public event System.EventHandler RightToLeftChanged` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-73106CFDD6F75174 | Event | `public event System.EventHandler RightToLeftLayoutChanged` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6E8FCA310ECB663F | Event | `public event System.Windows.Forms.ScrollEventHandler Scroll` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1E2CAE77254AD993 | Event | `public event System.EventHandler Shown` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AE485C78949A04F3 | Event | `public event System.EventHandler SizeChanged` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-74683364ED7BE0F9 | Event | `public event System.EventHandler StyleChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F7836DE976ACE1F2 | Event | `public event System.EventHandler SystemColorsChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-75FECF0BEACFEF2D | Event | `public event System.EventHandler TabIndexChanged` | `System.Windows.Forms.Form` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E8D34A95FC5A09E6 | Event | `public event System.EventHandler TabStopChanged` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E7419DCE04B57FF2 | Event | `public event System.EventHandler TextChanged` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-67744AC115D82247 | Event | `public event System.EventHandler Validated` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CAC21CF02E3A884D | Event | `public event System.ComponentModel.CancelEventHandler Validating` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BAA81E82F821FDB5 | Event | `public event System.EventHandler VisibleChanged` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4EEB4F489D3A5F69 | Method | `public System.Void Activate()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D36AEA68F0F4AFF2 | Method | `public System.Void AddOwnedForm(System.Windows.Forms.Form ownedForm)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4B06BF6BAFB54791 | Method | `public System.IAsyncResult BeginInvoke(System.Action method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E3966866EBD93BB6 | Method | `public System.IAsyncResult BeginInvoke(System.Delegate method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D5D76F2FC3581DE0 | Method | `public System.IAsyncResult BeginInvoke(System.Delegate method, System.Object[] args)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BD454CEF7E480D2E | Method | `public System.Void BringToFront()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4B15E08FB9F463DB | Method | `public System.Void Close()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-544E4D1F17D01C2C | Method | `public System.Boolean Contains(System.Windows.Forms.Control ctl)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C69950B8B4D753E | Method | `public System.Void CreateControl()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-85960AF12E6C60B6 | Method | `public System.Drawing.Graphics CreateGraphics()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E2D8533099088F4C | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F9C77EE98F500037 | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DDFE1816F032F3DE | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29EF2F8BFD325489 | Method | `public System.Windows.Forms.DragDropEffects DoDragDropAsJson<!!0>(!!0 data, System.Windows.Forms.DragDropEffects allowedEffects) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6709835F81F62BD4 | Method | `public System.Windows.Forms.DragDropEffects DoDragDropAsJson<!!0>(!!0 data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FF8A6969B8946805 | Method | `public System.Void DrawToBitmap(System.Drawing.Bitmap bitmap, System.Drawing.Rectangle targetBounds)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8DBE5C76F8E7FC84 | Method | `public System.Object EndInvoke(System.IAsyncResult asyncResult)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4A6FFB0582E3A624 | Method | `public System.Windows.Forms.Form FindForm()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3B616D14FFB78772 | Method | `public System.Boolean Focus()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-72166BDAE927FC09 | Method | `public static System.Windows.Forms.Control FromChildHandle(System.IntPtr handle)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A324C64645CFD9F3 | Method | `public static System.Windows.Forms.Control FromHandle(System.IntPtr handle)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A2FD5B0A905B5DC | Method | `public static System.Drawing.SizeF GetAutoScaleSize(System.Drawing.Font font)` | `System.Windows.Forms.Form` ObsoleteAttribute("This method has been deprecated. Use the AutoScaleDimensions property instead.  https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-59FCD3A749E5FDE6 | Method | `public System.Windows.Forms.Control GetChildAtPoint(System.Drawing.Point pt)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B0626F855B8F79B5 | Method | `public System.Windows.Forms.Control GetChildAtPoint(System.Drawing.Point pt, System.Windows.Forms.GetChildAtPointSkip skipValue)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9258D5A3BD8BB68D | Method | `public System.Windows.Forms.IContainerControl GetContainerControl()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1C08610A91B81A54 | Method | `public System.Windows.Forms.Control GetNextControl(System.Windows.Forms.Control ctl, System.Boolean forward)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FE5FB9F65194B44D | Method | `public virtual System.Drawing.Size GetPreferredSize(System.Drawing.Size proposedSize)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FA7F58923A0E9189 | Method | `public System.Void Hide()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1377DF38DA7E4A64 | Method | `public System.Void Invalidate()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E5D96ECB21B30D2C | Method | `public System.Void Invalidate(System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D8F78F2C37544E60 | Method | `public System.Void Invalidate(System.Drawing.Rectangle rc)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7EADC3DD64858ACD | Method | `public System.Void Invalidate(System.Drawing.Rectangle rc, System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F72ECC17DA507D68 | Method | `public System.Void Invalidate(System.Drawing.Region region)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5B80C31C4A57B8BD | Method | `public System.Void Invalidate(System.Drawing.Region region, System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-436931EEC3C73D9C | Method | `public !!0 Invoke<!!0>(System.Func&#96;1<!!0> method) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1DE4BBC56A4B9092 | Method | `public System.Object Invoke(System.Delegate method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC4F2174BA47134F | Method | `public System.Object Invoke(System.Delegate method, System.Object[] args)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6C6ED2796E7BA8DF | Method | `public System.Void Invoke(System.Action method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-97CE2CAB6197F2A2 | Method | `public System.Threading.Tasks.Task InvokeAsync(System.Action callback, System.Threading.CancellationToken cancellationToken = null)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B5B9546B1EE05845 | Method | `public System.Threading.Tasks.Task InvokeAsync(System.Func&#96;2<System.Threading.CancellationToken,System.Threading.Tasks.ValueTask> callback, System.Threading.CancellationToken cancellationToken = null)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FE09B21C186C0C39 | Method | `public System.Threading.Tasks.Task&#96;1<!!0> InvokeAsync<!!0>(System.Func&#96;1<!!0> callback, System.Threading.CancellationToken cancellationToken = null) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-171AD4F68B899B70 | Method | `public System.Threading.Tasks.Task&#96;1<!!0> InvokeAsync<!!0>(System.Func&#96;2<System.Threading.CancellationToken,System.Threading.Tasks.ValueTask&#96;1<!!0>> callback, System.Threading.CancellationToken cancellationToken = null) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5653468219651632 | Method | `public static System.Boolean IsKeyLocked(System.Windows.Forms.Keys keyVal)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-03A09366B69BDDD5 | Method | `public static System.Boolean IsMnemonic(System.Char charCode, System.String text)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8AA1D44F3AF279BD | Method | `public System.Void LayoutMdi(System.Windows.Forms.MdiLayout value)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-06B5C23F14F20C38 | Method | `public System.Drawing.Size LogicalToDeviceUnits(System.Drawing.Size value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-86DAE55B1729D5B6 | Method | `public System.Int32 LogicalToDeviceUnits(System.Int32 value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E64877F36EF88EAD | Method | `public System.Void PerformAutoScale()` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-65E4C34D705BFBD1 | Method | `public System.Void PerformLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F5E83C360160AD19 | Method | `public System.Void PerformLayout(System.Windows.Forms.Control affectedControl, System.String affectedProperty)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FE4D8EC16226C6D8 | Method | `public System.Drawing.Point PointToClient(System.Drawing.Point p)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DF579A67FBA343D1 | Method | `public System.Drawing.Point PointToScreen(System.Drawing.Point p)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8F8481887737C697 | Method | `public System.Windows.Forms.PreProcessControlState PreProcessControlMessage(ref System.Windows.Forms.Message msg)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-54FEA79A7261CC8A | Method | `public virtual System.Boolean PreProcessMessage(ref System.Windows.Forms.Message msg)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-479880FD7037FE8C | Method | `public System.Drawing.Rectangle RectangleToClient(System.Drawing.Rectangle r)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8628CC356C600715 | Method | `public System.Drawing.Rectangle RectangleToScreen(System.Drawing.Rectangle r)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E01904E4B089D038 | Method | `public virtual System.Void Refresh()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AF003E499AE807C9 | Method | `public System.Void RemoveOwnedForm(System.Windows.Forms.Form ownedForm)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C9EDD37FA1E1D95 | Method | `public virtual System.Void ResetBackColor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2503368A4EECAE7A | Method | `public System.Void ResetBindings()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-716E319A7DA03899 | Method | `public virtual System.Void ResetCursor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8999CE72172D3B3A | Method | `public virtual System.Void ResetFont()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FC05C9CEC8F95893 | Method | `public virtual System.Void ResetForeColor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-62DC6996A17E314C | Method | `public System.Void ResetImeMode()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-57457C7F3B58E249 | Method | `public virtual System.Void ResetRightToLeft()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B211F3EFA13E972E | Method | `public virtual System.Void ResetText()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-57D0D3CB6C407FFB | Method | `public System.Void ResumeLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D12E5F40FF8927F7 | Method | `public System.Void ResumeLayout(System.Boolean performLayout)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-685B12C6F55E6E52 | Method | `public System.Void Scale(System.Drawing.SizeF factor)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-01ED397D21F8FE66 | Method | `public System.Void Scale(System.Single dx, System.Single dy)` | `System.Windows.Forms.Control` ObsoleteAttribute("This method has been deprecated. Use the Scale(SizeF ratio) method instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AAE6D0296E134511 | Method | `public System.Void Scale(System.Single ratio)` | `System.Windows.Forms.Control` ObsoleteAttribute("This method has been deprecated. Use the Scale(SizeF ratio) method instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ED26050F5D94F6A6 | Method | `public System.Void ScaleBitmapLogicalToDevice(ref System.Drawing.Bitmap logicalBitmap)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4F97B197C13DC043 | Method | `public System.Void ScrollControlIntoView(System.Windows.Forms.Control activeControl)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EE2D3F7E7AB9A3BE | Method | `public System.Void Select()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B2D3E7A501EAD125 | Method | `public System.Boolean SelectNextControl(System.Windows.Forms.Control ctl, System.Boolean forward, System.Boolean tabStopOnly, System.Boolean nested, System.Boolean wrap)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-401F48A4D258DEF6 | Method | `public System.Void SendToBack()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-67C4B2E29AC5E57A | Method | `public System.Void SetAutoScrollMargin(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C0DB4ED834279A4C | Method | `public System.Void SetBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B496B11149FAD451 | Method | `public System.Void SetBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4196CD5C9131E469 | Method | `public System.Void SetDesktopBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7CB05F83BE98D12F | Method | `public System.Void SetDesktopLocation(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F3E208ACE2CB635C | Method | `public System.Void Show()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2CA58FDA3831CDDE | Method | `public System.Void Show(System.Windows.Forms.IWin32Window owner)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B1B9B286458ADD1C | Method | `public System.Threading.Tasks.Task ShowAsync(System.Windows.Forms.IWin32Window owner = null)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B89FDAB990FFAAED | Method | `public System.Windows.Forms.DialogResult ShowDialog()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-354F4F6939A2EB48 | Method | `public System.Windows.Forms.DialogResult ShowDialog(System.Windows.Forms.IWin32Window owner)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F050AD8F5CF3437F | Method | `public System.Threading.Tasks.Task&#96;1<System.Windows.Forms.DialogResult> ShowDialogAsync()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C8BCE39760A00889 | Method | `public System.Threading.Tasks.Task&#96;1<System.Windows.Forms.DialogResult> ShowDialogAsync(System.Windows.Forms.IWin32Window owner)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A1E9C28074207131 | Method | `public System.Void SuspendLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6CE4A6A502CA8AE3 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E380C0EDF2054E49 | Method | `public System.Void Update()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-525456D7B0108B98 | Method | `public System.Boolean Validate()` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-086E24F0488FD0B1 | Method | `public System.Boolean Validate(System.Boolean checkAutoValidate)` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-24344570CE5CEEA5 | Method | `public virtual System.Boolean ValidateChildren()` | `System.Windows.Forms.Form` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8F4473099A9266CC | Method | `public virtual System.Boolean ValidateChildren(System.Windows.Forms.ValidationConstraints validationConstraints)` | `System.Windows.Forms.Form` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9EEA3E7C8FE2B370 | Property | `System.Windows.Forms.IButtonControl AcceptButton { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5577AFA67FB3F65A | Property | `System.Windows.Forms.AccessibleObject AccessibilityObject { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C5255F8BCF8072CE | Property | `System.String AccessibleDefaultActionDescription { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-32B170620A079507 | Property | `System.String AccessibleDescription { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1D420C4847C5EBB4 | Property | `System.String AccessibleName { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3A6F53E084D87375 | Property | `System.Windows.Forms.AccessibleRole AccessibleRole { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D8186211999B3EB | Property | `System.Windows.Forms.Control ActiveControl { public get; public set; }` | `System.Windows.Forms.ContainerControl` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-25EEFA7877481839 | Property | `static System.Windows.Forms.Form ActiveForm { public get; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-515C620ABE626792 | Property | `System.Windows.Forms.Form ActiveMdiChild { public get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E80F2D651F26CA3 | Property | `virtual System.Boolean AllowDrop { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EF1D31333DC4BC88 | Property | `System.Boolean AllowTransparency { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AB27404DA6304AF7 | Property | `virtual System.Windows.Forms.AnchorStyles Anchor { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-093EF96281A64133 | Property | `System.Boolean AutoScale { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BD1DFA7B2C6A861E | Property | `virtual System.Drawing.Size AutoScaleBaseSize { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false); ObsoleteAttribute("This property has been deprecated. Use the AutoScaleDimensions property instead.  https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F1CB5B0F842DC1E | Property | `System.Drawing.SizeF AutoScaleDimensions { public get; public set; }` | `System.Windows.Forms.ContainerControl` LocalizableAttribute(true); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-75151ED79BCBCDE2 | Property | `System.Windows.Forms.AutoScaleMode AutoScaleMode { public get; public set; }` | `System.Windows.Forms.ContainerControl` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C39B0DDEA3DF61E4 | Property | `virtual System.Boolean AutoScroll { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C9C43076B6263DD4 | Property | `System.Drawing.Size AutoScrollMargin { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-071E915D96705281 | Property | `System.Drawing.Size AutoScrollMinSize { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-14BF628A784F3DEB | Property | `virtual System.Drawing.Point AutoScrollOffset { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DefaultValueAttribute(System.Drawing.Point,"0, 0") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7215FD17F7D8D89E | Property | `System.Drawing.Point AutoScrollPosition { public get; public set; }` | `System.Windows.Forms.ScrollableControl` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E5320EBCB6A55A1 | Property | `virtual System.Boolean AutoSize { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F33E2C4C3011C0F7 | Property | `System.Windows.Forms.AutoSizeMode AutoSizeMode { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(true); DefaultValueAttribute(1); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-43458203E5C03744 | Property | `virtual System.Windows.Forms.AutoValidate AutoValidate { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-68D6CE4CA7B788B9 | Property | `virtual System.Drawing.Color BackColor { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1649926A1D21EC0B | Property | `virtual System.Drawing.Image BackgroundImage { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F232B52062C38AFA | Property | `virtual System.Windows.Forms.ImageLayout BackgroundImageLayout { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F6034CE4793D80AF | Property | `virtual System.Windows.Forms.BindingContext BindingContext { public get; public set; }` | `System.Windows.Forms.ContainerControl` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D8E976D843B1D36E | Property | `System.Int32 Bottom { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-15531EF390D70346 | Property | `System.Drawing.Rectangle Bounds { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-80D0C41989C35A91 | Property | `System.Windows.Forms.IButtonControl CancelButton { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DF9669559C5F09FE | Property | `System.Boolean CanFocus { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-22F8E79BD35BB23E | Property | `System.Boolean CanSelect { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FE728EA9F2CB0951 | Property | `System.Boolean Capture { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F77B1AA68DD99B81 | Property | `System.Boolean CausesValidation { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C01BCBB27D234C1B | Property | `static System.Boolean CheckForIllegalCrossThreadCalls { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B31A82B4745BA7C1 | Property | `System.Drawing.Rectangle ClientRectangle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-55FD2BA6D7C65278 | Property | `System.Drawing.Size ClientSize { public get; public set; }` | `System.Windows.Forms.Form` LocalizableAttribute(true); DesignerSerializationVisibilityAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AB90A2F1873F2653 | Property | `System.String CompanyName { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-98736A5C35CAC608 | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A2596440EF563BFF | Property | `System.Boolean ContainsFocus { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-713C2D169E82FDA9 | Property | `virtual System.Windows.Forms.ContextMenu ContextMenu { public get; public set; }` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-63D085A515AF7F56 | Property | `virtual System.Windows.Forms.ContextMenuStrip ContextMenuStrip { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-808B696C3942AAB8 | Property | `System.Boolean ControlBox { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3EB41DDC6562A843 | Property | `System.Windows.Forms.Control+ControlCollection Controls { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E98F19A9FF70DA4B | Property | `System.Boolean Created { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D384ACC5F671F470 | Property | `System.Drawing.SizeF CurrentAutoScaleDimensions { public get; }` | `System.Windows.Forms.ContainerControl` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D3A7044BF04A16E6 | Property | `virtual System.Windows.Forms.Cursor Cursor { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-448EB44BA7CC6474 | Property | `System.Windows.Forms.ControlBindingsCollection DataBindings { public get; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F4262A183386F829 | Property | `virtual System.Object DataContext { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); BindableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-77E0892CBD28354E | Property | `static System.Drawing.Color DefaultBackColor { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CF10BE11EEB4DC2B | Property | `static System.Drawing.Font DefaultFont { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-397F47DC48764418 | Property | `static System.Drawing.Color DefaultForeColor { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E81C6F1741CFC464 | Property | `System.Drawing.Rectangle DesktopBounds { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-26037A8A6A45AD33 | Property | `System.Drawing.Point DesktopLocation { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8D0F91CE30452C9F | Property | `System.Int32 DeviceDpi { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AFA2E16D107B0A2A | Property | `System.Windows.Forms.DialogResult DialogResult { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3FC01570971AB740 | Property | `virtual System.Drawing.Rectangle DisplayRectangle { public get; }` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FB8500B66FC2C540 | Property | `System.Boolean Disposing { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-510D5643A2BC1B14 | Property | `virtual System.Windows.Forms.DockStyle Dock { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-750DD65C0335FACA | Property | `System.Windows.Forms.ScrollableControl+DockPaddingEdges DockPadding { public get; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-553074D035176E97 | Property | `System.Drawing.Printing.PrintDocument Document { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC28698382747463 | Property | `System.Boolean Enabled { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5A3F40105F83F6B5 | Property | `virtual System.Boolean Focused { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-460970E09BFBBFE1 | Property | `virtual System.Drawing.Font Font { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4DA6CC196DE93FDA | Property | `virtual System.Drawing.Color ForeColor { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-47F508286A0B7319 | Property | `System.Drawing.Color FormBorderColor { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-65833E4014867390 | Property | `System.Windows.Forms.FormBorderStyle FormBorderStyle { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-124A66C3886AD4E5 | Property | `System.Drawing.Color FormCaptionBackColor { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CA2CFC8D9418C613 | Property | `System.Drawing.Color FormCaptionTextColor { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50B0DE68CCA46341 | Property | `System.Windows.Forms.FormCornerPreference FormCornerPreference { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(0); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0743ABDD0951196B | Property | `System.Windows.Forms.ScreenCaptureMode FormScreenCaptureMode { public get; public set; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F9AC40C243779463 | Property | `System.IntPtr Handle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AE6E838F7462D4F6 | Property | `System.Boolean HasChildren { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-52192C609ECE547D | Property | `System.Int32 Height { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A04F3A045CCECC55 | Property | `System.Boolean HelpButton { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-33945858E6AA2A5D | Property | `System.Windows.Forms.HScrollProperties HorizontalScroll { public get; }` | `System.Windows.Forms.ScrollableControl` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6178BD514ED45E23 | Property | `System.Drawing.Icon Icon { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FB5023653EBBE972 | Property | `System.Windows.Forms.ImeMode ImeMode { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8AF3A5843789816D | Property | `System.Boolean InvokeRequired { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A46A9512D82C9DF7 | Property | `System.Boolean IsAccessible { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9DFC6AEAD62DC7E2 | Property | `System.Boolean IsAncestorSiteInDesignMode { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E5624E4B8DF600EA | Property | `System.Boolean IsDisposed { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D354A314024D9EC2 | Property | `System.Boolean IsHandleCreated { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-82C94283D73EB381 | Property | `System.Boolean IsMdiChild { public get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-46101971463E27BD | Property | `System.Boolean IsMdiContainer { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4A797A4B4B091949 | Property | `System.Boolean IsMirrored { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-18178274877A3CEF | Property | `System.Boolean IsRestrictedWindow { public get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B8F6FA5694DF24DE | Property | `System.Boolean KeyPreview { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2D81231DBE98C1B4 | Property | `virtual System.Windows.Forms.Layout.LayoutEngine LayoutEngine { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-08B65F774BD40477 | Property | `System.Int32 Left { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E2B2524E0BC8443A | Property | `System.Drawing.Point Location { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6AADA50F17AD984E | Property | `System.Windows.Forms.MenuStrip MainMenuStrip { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.ReferenceConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-231B4F8D57108385 | Property | `System.Windows.Forms.Padding Margin { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6F1B99DFFF1204C4 | Property | `System.Boolean MaximizeBox { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DAD37814A3286C65 | Property | `System.Drawing.Size MaximumSize { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FBE66F6AF4784B21 | Property | `System.Windows.Forms.Form[] MdiChildren { public get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B3240747B96D5DDE | Property | `System.Boolean MdiChildrenMinimizedAnchorBottom { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FE9F0E02FDDE17B4 | Property | `System.Windows.Forms.Form MdiParent { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AFFF3242ACC4E270 | Property | `System.Windows.Forms.MainMenu Menu { public get; public set; }` | `System.Windows.Forms.Form` ObsoleteAttribute("\u0060MainMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060MenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DAD689DF55429F0A | Property | `System.Windows.Forms.MainMenu MergedMenu { public get; }` | `System.Windows.Forms.Form` ObsoleteAttribute("\u0060MainMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060MenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-582FB3C27005CF0C | Property | `System.Boolean MinimizeBox { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false); DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CAD3B90B94216234 | Property | `System.Drawing.Size MinimumSize { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3E3014058DBAA19E | Property | `System.Boolean Modal { public get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-97FEE15262548D29 | Property | `static System.Windows.Forms.Keys ModifierKeys { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A90F4234AC82E27 | Property | `static System.Windows.Forms.MouseButtons MouseButtons { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7E764C6129BA6432 | Property | `static System.Drawing.Point MousePosition { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-85970758411CBE96 | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F00A3BA4CDD2FE4D | Property | `System.Double Opacity { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F55B3C73B01A7EA2 | Property | `System.Windows.Forms.Form[] OwnedForms { public get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-038CC7482000D969 | Property | `System.Windows.Forms.Form Owner { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-12944E7EB6E6E9CD | Property | `System.Windows.Forms.Padding Padding { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5BF6233B955B1595 | Property | `System.Windows.Forms.Control Parent { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-436EF22F58E6B400 | Property | `System.Windows.Forms.Form ParentForm { public get; }` | `System.Windows.Forms.ContainerControl` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4B029F34702FEED4 | Property | `System.Drawing.Size PreferredSize { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8050FF32E898F24A | Property | `System.Windows.Forms.PrintPreviewControl PrintPreviewControl { public get; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F624B245C1CA89E5 | Property | `System.String ProductName { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F1449EE15B714AC0 | Property | `System.String ProductVersion { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-49E5B2BCCD0AF2BC | Property | `System.Boolean RecreatingHandle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-07FE7EACCAB4EBF7 | Property | `System.Drawing.Region Region { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-03CED4BAB70E086A | Property | `System.Drawing.Rectangle RestoreBounds { public get; }` | `System.Windows.Forms.Form` DesignerSerializationVisibilityAttribute(0); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2738B6C0A75072B3 | Property | `System.Int32 Right { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F791CAEEF790D07F | Property | `virtual System.Windows.Forms.RightToLeft RightToLeft { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B44C562CC802E4D5 | Property | `virtual System.Boolean RightToLeftLayout { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-497DCAC82A80CAAF | Property | `System.Boolean ShowIcon { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DC4B8020B4E2627B | Property | `System.Boolean ShowInTaskbar { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false); DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5FF8ABDBD4724AE8 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B3CAE962E97EECC8 | Property | `System.Drawing.Size Size { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50329A8D82ECC633 | Property | `System.Windows.Forms.SizeGripStyle SizeGripStyle { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false); DefaultValueAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-62263BE15FD0CD08 | Property | `System.Windows.Forms.FormStartPosition StartPosition { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9BC56CFFED3E266D | Property | `System.Int32 TabIndex { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6CC4B732B7D89A27 | Property | `System.Boolean TabStop { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-36DE8D39EA2DE227 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B5EEAAAEF16393E1 | Property | `virtual System.String Text { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-91C24296DAB15734 | Property | `System.Int32 Top { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3C81916A4703615D | Property | `System.Boolean TopLevel { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-51CF002BEA7E16EA | Property | `System.Windows.Forms.Control TopLevelControl { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A73681720B959AD9 | Property | `System.Boolean TopMost { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2AA88338954DB224 | Property | `System.Drawing.Color TransparencyKey { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B6188747F1E08E5F | Property | `System.Boolean UseAntiAlias { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8BC746182BA7E63A | Property | `System.Boolean UseWaitCursor { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E9C86AE4FCD19EE1 | Property | `System.Windows.Forms.VScrollProperties VerticalScroll { public get; }` | `System.Windows.Forms.ScrollableControl` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-843DA20AB168F342 | Property | `System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BEAA57B8C0C0AD03 | Property | `System.Int32 Width { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-224A2DB2A1B84976 | Property | `System.Windows.Forms.FormWindowState WindowState { public get; public set; }` | `System.Windows.Forms.PrintPreviewDialog` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-357D23819B6CE72F | Property | `System.Windows.Forms.IWindowTarget WindowTarget { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-0613326F80FEAB5E | Field | `protected const System.Int32 ScrollStateAutoScrolling = 1` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-269E7F56653550CC | Field | `protected const System.Int32 ScrollStateFullDrag = 16` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-39C2F3D0D69F3508 | Field | `protected const System.Int32 ScrollStateHScrollVisible = 2` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0EEFB893F7EC1BC4 | Field | `protected const System.Int32 ScrollStateUserHasScrolled = 8` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6BCF1FF5CE17E0B2 | Field | `protected const System.Int32 ScrollStateVScrollVisible = 4` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4421760857E6E9D0 | Method | `protected System.Void AccessibilityNotifyClients(System.Windows.Forms.AccessibleEvents accEvent, System.Int32 objectID, System.Int32 childID)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D370DEC85677C948 | Method | `protected internal System.Void AccessibilityNotifyClients(System.Windows.Forms.AccessibleEvents accEvent, System.Int32 childID)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-263F3F5F313A872D | Method | `protected System.Void ActivateMdiChild(System.Windows.Forms.Form form)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-23331FE05C1807CE | Method | `protected virtual System.Void AdjustFormScrollbars(System.Boolean displayScrollbars)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2F0E8E4BFD3B86C7 | Method | `protected System.Void ApplyAutoScaling()` | `System.Windows.Forms.Form` ObsoleteAttribute("This method has been deprecated. Use the ApplyAutoScaling method instead.  https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9FFC347B9B3F8B26 | Method | `protected System.Void CenterToParent()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4C4E4E9E927C028F | Method | `protected System.Void CenterToScreen()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C397919CC7196AD | Method | `protected virtual System.Windows.Forms.AccessibleObject CreateAccessibilityInstance()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B94DF8E8178F751 | Method | `protected virtual System.Windows.Forms.Control+ControlCollection CreateControlsInstance()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2AFA6E782F5840CF | Method | `protected virtual System.Void CreateHandle()` | `System.Windows.Forms.PrintPreviewDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C573B60EB8E39EC | Method | `protected virtual System.Void DefWndProc(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BFC8DD14E6CBAE4B | Method | `protected virtual System.Void DestroyHandle()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A20A845A19DEC02E | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E7B379B6632A25FF | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8059D1650E9197DF | Method | `protected virtual System.Windows.Forms.AccessibleObject GetAccessibilityObjectById(System.Int32 objectId)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A7DBDB9B6B2A96CE | Method | `protected System.Windows.Forms.AutoSizeMode GetAutoSizeMode()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C027DF11B7975FF | Method | `protected virtual System.Drawing.Rectangle GetScaledBounds(System.Drawing.Rectangle bounds, System.Drawing.SizeF factor, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0034AD3FAADD728B | Method | `protected System.Boolean GetScrollState(System.Int32 bit)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-13DE7B30A8DC45A5 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A7592C3EA79ECAE0 | Method | `protected System.Boolean GetStyle(System.Windows.Forms.ControlStyles flag)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-041BC667EAE1A137 | Method | `protected System.Boolean GetTopLevel()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4236D9154CA0C94D | Method | `protected virtual System.Void InitLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2A746365F321693A | Method | `protected System.Void InvokeGotFocus(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2C4E9A0ADA08BE8B | Method | `protected System.Void InvokeLostFocus(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5D56BFB580A663C8 | Method | `protected System.Void InvokeOnClick(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E2FD2877739AC21 | Method | `protected System.Void InvokePaint(System.Windows.Forms.Control c, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AEF807C3154FD7F4 | Method | `protected System.Void InvokePaintBackground(System.Windows.Forms.Control c, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D38E5FACD00A6AA | Method | `protected virtual System.Boolean IsInputChar(System.Char charCode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-76A9A9E5282ADC25 | Method | `protected virtual System.Boolean IsInputKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-25B3A94C150B51BC | Method | `protected virtual System.Void NotifyInvalidate(System.Drawing.Rectangle invalidatedArea)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-31C293677671EC9C | Method | `protected virtual System.Void OnActivated(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F42EA7B75A237658 | Method | `protected virtual System.Void OnAutoSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D4AFF721E5E6688C | Method | `protected virtual System.Void OnAutoValidateChanged(System.EventArgs e)` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-93A1D734BD75E392 | Method | `protected virtual System.Void OnBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9EC3C2C26C2966E5 | Method | `protected virtual System.Void OnBackgroundImageChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1917C276796BDE07 | Method | `protected virtual System.Void OnBackgroundImageLayoutChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-873208DEEDB4A26B | Method | `protected virtual System.Void OnBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-57183479B3B713B3 | Method | `protected virtual System.Void OnCausesValidationChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-17A94BDF83CDCC65 | Method | `protected virtual System.Void OnChangeUICues(System.Windows.Forms.UICuesEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F6872047DA03A52F | Method | `protected virtual System.Void OnClick(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B8F7694832B2B4DB | Method | `protected virtual System.Void OnClientSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-26CC1B0F0CD856D3 | Method | `protected virtual System.Void OnClosed(System.EventArgs e)` | `System.Windows.Forms.Form` ObsoleteAttribute("Form.OnClosing, Form.OnClosed and the corresponding events are obsolete. Use Form.OnFormClosing, Form.OnFormClosed, Form.FormClosing and Form.FormClosed instead.",false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D6A405434AE15234 | Method | `protected virtual System.Void OnClosing(System.ComponentModel.CancelEventArgs e)` | `System.Windows.Forms.PrintPreviewDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0F9281D2D275E6A | Method | `protected virtual System.Void OnContextMenuChanged(System.EventArgs e)` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-65280552A665A095 | Method | `protected virtual System.Void OnContextMenuStripChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F00AB192B88B99F2 | Method | `protected virtual System.Void OnControlAdded(System.Windows.Forms.ControlEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-035952D3EB513590 | Method | `protected virtual System.Void OnControlRemoved(System.Windows.Forms.ControlEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2C2052F751E850CF | Method | `protected virtual System.Void OnCreateControl()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D79270BB52399E0D | Method | `protected virtual System.Void OnCursorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-74C741B5FC4C7398 | Method | `protected virtual System.Void OnDataContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A10A1378C0CD2CA4 | Method | `protected virtual System.Void OnDeactivate(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9F721A2BD6C40DB9 | Method | `protected virtual System.Void OnDockChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5108915703C8A337 | Method | `protected virtual System.Void OnDoubleClick(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9ADCBA009D09A695 | Method | `protected virtual System.Void OnDpiChanged(System.Windows.Forms.DpiChangedEventArgs e)` | `System.Windows.Forms.Form` BrowsableAttribute(true); DesignerSerializationVisibilityAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-58CB9E375797B56D | Method | `protected virtual System.Void OnDpiChangedAfterParent(System.EventArgs e)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-56C42F8FC3FB7C08 | Method | `protected virtual System.Void OnDpiChangedBeforeParent(System.EventArgs e)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E243E3A7599F1498 | Method | `protected virtual System.Void OnDragDrop(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-92DFD4B8AE795779 | Method | `protected virtual System.Void OnDragEnter(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BF33012144BFD218 | Method | `protected virtual System.Void OnDragLeave(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FD704998D5531CE3 | Method | `protected virtual System.Void OnDragOver(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-300CBACDD5836632 | Method | `protected virtual System.Void OnEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8C1FD7E8011F1A45 | Method | `protected internal virtual System.Void OnEnter(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CBF121CC936BB8CD | Method | `protected virtual System.Void OnFontChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E422CEDC7ED8F8E7 | Method | `protected virtual System.Void OnForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A145A4AA57EC4D33 | Method | `protected virtual System.Void OnFormBorderColorChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D5A447DBE04F52E2 | Method | `protected virtual System.Void OnFormCaptionBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-234A26F021746E5A | Method | `protected virtual System.Void OnFormCaptionTextColorChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1E63DE9A91266486 | Method | `protected virtual System.Void OnFormClosed(System.Windows.Forms.FormClosedEventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E40A877E60AC7C5 | Method | `protected virtual System.Void OnFormClosing(System.Windows.Forms.FormClosingEventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16B7614846C7E6A0 | Method | `protected virtual System.Void OnFormCornerPreferenceChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-08A7721B613A656F | Method | `protected virtual System.Boolean OnGetDpiScaledSize(System.Int32 deviceDpiOld, System.Int32 deviceDpiNew, ref System.Drawing.Size desiredSize)` | `System.Windows.Forms.Form` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6BA47FD11DE41B59 | Method | `protected virtual System.Void OnGiveFeedback(System.Windows.Forms.GiveFeedbackEventArgs gfbevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16F56EE9B3CBC1BA | Method | `protected virtual System.Void OnGotFocus(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EB2C4276DB621932 | Method | `protected virtual System.Void OnHandleCreated(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-108A47F4D0957605 | Method | `protected virtual System.Void OnHandleDestroyed(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FB08A925E12AC580 | Method | `protected virtual System.Void OnHelpButtonClicked(System.ComponentModel.CancelEventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9946FF4AD3D02CED | Method | `protected virtual System.Void OnHelpRequested(System.Windows.Forms.HelpEventArgs hevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-75C7E975E799A7C9 | Method | `protected virtual System.Void OnImeModeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DB776A090786BBA9 | Method | `protected virtual System.Void OnInputLanguageChanged(System.Windows.Forms.InputLanguageChangedEventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-81604EC82EAF1C10 | Method | `protected virtual System.Void OnInputLanguageChanging(System.Windows.Forms.InputLanguageChangingEventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F8D252749516D74B | Method | `protected virtual System.Void OnInvalidated(System.Windows.Forms.InvalidateEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-14E964A351B1B75E | Method | `protected virtual System.Void OnKeyDown(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F0BA053FE9109C65 | Method | `protected virtual System.Void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E474B1F84A2A184 | Method | `protected virtual System.Void OnKeyUp(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F0783221EA722449 | Method | `protected virtual System.Void OnLayout(System.Windows.Forms.LayoutEventArgs levent)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F1D6D7C96B22BDA9 | Method | `protected internal virtual System.Void OnLeave(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AE56B704E22FF124 | Method | `protected virtual System.Void OnLoad(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BA94536AA97C538C | Method | `protected virtual System.Void OnLocationChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0144F304E53BFA0B | Method | `protected virtual System.Void OnLostFocus(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7921C6A1EDAA024C | Method | `protected virtual System.Void OnMarginChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-43F1FCF089B49F62 | Method | `protected virtual System.Void OnMaximizedBoundsChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8C2EE115A65B4CCC | Method | `protected virtual System.Void OnMaximumSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6A7232A6AF036B04 | Method | `protected virtual System.Void OnMdiChildActivate(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-81005286DF3D6087 | Method | `protected virtual System.Void OnMenuComplete(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-36D138D17010AE2E | Method | `protected virtual System.Void OnMenuStart(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D21184DD916EC961 | Method | `protected virtual System.Void OnMinimumSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3F10BF906E5D5319 | Method | `protected virtual System.Void OnMouseCaptureChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A1066B7788A49DA5 | Method | `protected virtual System.Void OnMouseClick(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-63E2539EFDC45AE7 | Method | `protected virtual System.Void OnMouseDoubleClick(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D0EBC13B2A262ED6 | Method | `protected virtual System.Void OnMouseDown(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BBB681012EC4974A | Method | `protected virtual System.Void OnMouseEnter(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-97C221826C6A6B98 | Method | `protected virtual System.Void OnMouseHover(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9474895BC0220D00 | Method | `protected virtual System.Void OnMouseLeave(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7F7D243C6FA4D94B | Method | `protected virtual System.Void OnMouseMove(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16C734BE1ACACC01 | Method | `protected virtual System.Void OnMouseUp(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BE579C6D543439AC | Method | `protected virtual System.Void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-59ECFC7D74F86931 | Method | `protected virtual System.Void OnMove(System.EventArgs e)` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E533E912E54BF031 | Method | `protected virtual System.Void OnNotifyMessage(System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-168504DD8B03A92D | Method | `protected virtual System.Void OnPaddingChanged(System.EventArgs e)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-89B99E4D24F44DFA | Method | `protected virtual System.Void OnPaint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C0620AA3CF08B92F | Method | `protected virtual System.Void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7F1949DCEE32C966 | Method | `protected virtual System.Void OnParentBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3F181507E5686AAC | Method | `protected virtual System.Void OnParentBackgroundImageChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A14DDC92DA637C52 | Method | `protected virtual System.Void OnParentBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50766E76E8B1C9D2 | Method | `protected virtual System.Void OnParentChanged(System.EventArgs e)` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-920823D07EAABE94 | Method | `protected virtual System.Void OnParentCursorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1ABDAF1727E1807F | Method | `protected virtual System.Void OnParentDataContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-30A0D7B82740F5A3 | Method | `protected virtual System.Void OnParentEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CC713F3D94F08896 | Method | `protected virtual System.Void OnParentFontChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-47CF62FF6541D157 | Method | `protected virtual System.Void OnParentForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D9FED4F546CFCB52 | Method | `protected virtual System.Void OnParentRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BB5207D2D7F5406E | Method | `protected virtual System.Void OnParentVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8C7D370B25F2C329 | Method | `protected virtual System.Void OnPreviewKeyDown(System.Windows.Forms.PreviewKeyDownEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8C55F5A95A7925F4 | Method | `protected virtual System.Void OnPrint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-58ABBB4EADC30CE6 | Method | `protected virtual System.Void OnQueryContinueDrag(System.Windows.Forms.QueryContinueDragEventArgs qcdevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C5433B9D88447264 | Method | `protected virtual System.Void OnRegionChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0198BE137126B6B | Method | `protected virtual System.Void OnResize(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DC6DDBEBD78C3D6E | Method | `protected virtual System.Void OnResizeBegin(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F3265C8A64CA6A8 | Method | `protected virtual System.Void OnResizeEnd(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A217E7DC8B4096F1 | Method | `protected virtual System.Void OnRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F7988B7804078627 | Method | `protected virtual System.Void OnRightToLeftLayoutChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F98F8DD0E958A440 | Method | `protected virtual System.Void OnScroll(System.Windows.Forms.ScrollEventArgs se)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9447C63BF3C0E7CE | Method | `protected virtual System.Void OnShown(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-39766C8F90A4D58B | Method | `protected virtual System.Void OnSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F485185A958F817C | Method | `protected virtual System.Void OnStyleChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D7536D6374F4A90 | Method | `protected virtual System.Void OnSystemColorsChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A78E5D8564175C57 | Method | `protected virtual System.Void OnTabIndexChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1CF1886CD1D14A09 | Method | `protected virtual System.Void OnTabStopChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4C95535909791FE2 | Method | `protected virtual System.Void OnTextChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-350BCADEC624D526 | Method | `protected virtual System.Void OnValidated(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B87A6DCB1C31AD2E | Method | `protected virtual System.Void OnValidating(System.ComponentModel.CancelEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AE5C6FE706965AC1 | Method | `protected virtual System.Void OnVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0FF99D09D979F502 | Method | `protected virtual System.Boolean ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-26037ED13B1930BA | Method | `protected virtual System.Boolean ProcessDialogChar(System.Char charCode)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-296300F314F1487B | Method | `protected virtual System.Boolean ProcessDialogKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.PrintPreviewDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AE4EB2B9BD098771 | Method | `protected virtual System.Boolean ProcessKeyEventArgs(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-15B8CFBD9F6B8CC9 | Method | `protected internal virtual System.Boolean ProcessKeyMessage(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1FF30C5A02D0807F | Method | `protected virtual System.Boolean ProcessKeyPreview(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3B82A3FACA7D745A | Method | `protected internal virtual System.Boolean ProcessMnemonic(System.Char charCode)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C47165087ACA73DF | Method | `protected virtual System.Boolean ProcessTabKey(System.Boolean forward)` | `System.Windows.Forms.PrintPreviewDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2402386E4F1008D9 | Method | `protected System.Void RaiseDragEvent(System.Object key, System.Windows.Forms.DragEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C5C04ACC5C9F1A6F | Method | `protected System.Void RaiseKeyEvent(System.Object key, System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9653C867E53421A4 | Method | `protected System.Void RaiseMouseEvent(System.Object key, System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3DEC28C6643B4AF4 | Method | `protected System.Void RaisePaintEvent(System.Object key, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F333F2A067C01C34 | Method | `protected System.Void RecreateHandle()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D7244DCE6DAC645F | Method | `protected static System.Boolean ReflectMessage(System.IntPtr hWnd, ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-19C8531046018749 | Method | `protected virtual System.Void RescaleConstantsForDpi(System.Int32 deviceDpiOld, System.Int32 deviceDpiNew)` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3CE4D578B83E904C | Method | `protected System.Void ResetMouseEventArgs()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6BED1260A6DE8B83 | Method | `protected System.Drawing.ContentAlignment RtlTranslateAlignment(System.Drawing.ContentAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BC76C063C684EAAE | Method | `protected System.Windows.Forms.HorizontalAlignment RtlTranslateAlignment(System.Windows.Forms.HorizontalAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E5620A51A6BC4042 | Method | `protected System.Windows.Forms.LeftRightAlignment RtlTranslateAlignment(System.Windows.Forms.LeftRightAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F3E4EF2F1B0FE50A | Method | `protected internal System.Drawing.ContentAlignment RtlTranslateContent(System.Drawing.ContentAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4829202ED8E9651B | Method | `protected System.Windows.Forms.HorizontalAlignment RtlTranslateHorizontal(System.Windows.Forms.HorizontalAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6AA2D2E63E621429 | Method | `protected System.Windows.Forms.LeftRightAlignment RtlTranslateLeftRight(System.Windows.Forms.LeftRightAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-707603E651202B91 | Method | `protected virtual System.Void ScaleControl(System.Drawing.SizeF factor, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E891BD4D8446025A | Method | `protected virtual System.Void ScaleCore(System.Single x, System.Single y)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FBD8478F9DC55591 | Method | `protected virtual System.Void ScaleMinMaxSize(System.Single xScaleFactor, System.Single yScaleFactor, System.Boolean updateContainerSize = true)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2794B8531BC77DE3 | Method | `protected virtual System.Drawing.Point ScrollToControl(System.Windows.Forms.Control activeControl)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-13D4E92E545F3E33 | Method | `protected virtual System.Void Select(System.Boolean directed, System.Boolean forward)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-61B28B7EA759E636 | Method | `protected System.Void SetAutoSizeMode(System.Windows.Forms.AutoSizeMode mode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F81EA13692A588B5 | Method | `protected virtual System.Void SetBoundsCore(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D1961AB41DAC942F | Method | `protected virtual System.Void SetClientSizeCore(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FADB09067CE245A8 | Method | `protected System.Void SetDisplayRectLocation(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BD34551F98296F80 | Method | `protected System.Void SetScrollState(System.Int32 bit, System.Boolean value)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7E0733EFCAB68979 | Method | `protected System.Void SetStyle(System.Windows.Forms.ControlStyles flag, System.Boolean value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-040530CFD4A54CE0 | Method | `protected System.Void SetTopLevel(System.Boolean value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A80C8E6DFC2FDEF5 | Method | `protected virtual System.Void SetVisibleCore(System.Boolean value)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-80CAEAB149BB9DF8 | Method | `protected virtual System.Drawing.Size SizeFromClientSize(System.Drawing.Size clientSize)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-69FB199CDEABCEAA | Method | `protected System.Void UpdateBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A718EBD118D176C5 | Method | `protected System.Void UpdateBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Int32 clientWidth, System.Int32 clientHeight)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F657E2DEAD76E2ED | Method | `protected internal System.Void UpdateBounds()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E446571EB047A88 | Method | `protected virtual System.Void UpdateDefaultButton()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-08B5945818DC6FFF | Method | `protected System.Void UpdateStyles()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-751541D9C14B86D3 | Method | `protected System.Void UpdateZOrder()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-46E3965D915A9C6E | Method | `protected virtual System.Void WndProc(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-70A787C33B556FA3 | Property | `System.Drawing.SizeF AutoScaleFactor { protected get; }` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C0A1FB71714183B0 | Property | `virtual System.Boolean CanEnableIme { protected get; }` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6BFDCD181F2F871E | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-631E0F86FFFB815D | Property | `virtual System.Windows.Forms.CreateParams CreateParams { protected get; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DBBDB5CA2DAAF8F1 | Property | `virtual System.Windows.Forms.Cursor DefaultCursor { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-167D0B0D58D13BA0 | Property | `virtual System.Windows.Forms.ImeMode DefaultImeMode { protected get; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-922030A6D28E7076 | Property | `virtual System.Windows.Forms.Padding DefaultMargin { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-166EED6438F16132 | Property | `virtual System.Drawing.Size DefaultMaximumSize { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D3AC2CEC22FA9698 | Property | `virtual System.Drawing.Size DefaultMinimumSize { protected get; }` | `System.Windows.Forms.PrintPreviewDialog`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29F8426979D1FF97 | Property | `virtual System.Windows.Forms.Padding DefaultPadding { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FE7CBF07D7908528 | Property | `virtual System.Drawing.Size DefaultSize { protected get; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-51EC5B4ACCD28431 | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F5AFA37A61C84C07 | Property | `virtual System.Boolean DoubleBuffered { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8B4C186CD04CAD98 | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1063D1E2891B000F | Property | `System.Int32 FontHeight { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-44AD938031E81AD0 | Property | `System.Boolean HScroll { protected get; protected set; }` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-92924E5DFF87A455 | Property | `virtual System.Windows.Forms.ImeMode ImeModeBase { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0AB1C90CB349175D | Property | `System.Drawing.Rectangle MaximizedBounds { protected get; protected set; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4BB8BB55438B4F10 | Property | `static System.Windows.Forms.ImeMode PropagatingImeMode { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B93053EA5D6B5F5 | Property | `System.Boolean RenderRightToLeft { protected internal get; }` | `System.Windows.Forms.Control` ObsoleteAttribute("This property has been deprecated. Please use RightToLeft instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-425C5A08B0FB1179 | Property | `System.Boolean ResizeRedraw { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F3A817841E1865F9 | Property | `virtual System.Boolean ScaleChildren { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-06BE453600D4E243 | Property | `virtual System.Boolean ShowFocusCues { protected internal get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1D38E4D34DFF1EFF | Property | `virtual System.Boolean ShowKeyboardCues { protected internal get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-037E8BE525B2D249 | Property | `virtual System.Boolean ShowWithoutActivation { protected get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-12E043E76CF2A1CC | Property | `System.Boolean VScroll { protected get; protected set; }` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
