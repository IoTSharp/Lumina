# ToolStripMenuItem：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/ToolStripMenuItem.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.ToolStripMenuItem`；基类：`System.Windows.Forms.ToolStripDropDownItem`。Lumina 同名类型：🟡 存在；基类及基线接口形状：⬜ 未匹配。

公开属性 **73**，公开事件 **38**，公开方法 **21**；受保护扩展成员 **72**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-5C12FAF3FAF0D7F1 | Constructor | `public ToolStripMenuItem()` | `System.Windows.Forms.ToolStripMenuItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-BBF141E53AEF95BE | Constructor | `public ToolStripMenuItem(System.Drawing.Image image)` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B7867D6D99935338 | Constructor | `public ToolStripMenuItem(System.String text)` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29B1BC9C370E916D | Constructor | `public ToolStripMenuItem(System.String text, System.Drawing.Image image)` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-947E8799F78AD0C7 | Constructor | `public ToolStripMenuItem(System.String text, System.Drawing.Image image, System.EventHandler onClick)` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-67623004377BEAD7 | Constructor | `public ToolStripMenuItem(System.String text, System.Drawing.Image image, System.EventHandler onClick, System.String name)` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9979F63A1C5A57D7 | Constructor | `public ToolStripMenuItem(System.String text, System.Drawing.Image image, System.EventHandler onClick, System.Windows.Forms.Keys shortcutKeys)` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-73083B6FC915D696 | Constructor | `public ToolStripMenuItem(System.String text, System.Drawing.Image image, System.Windows.Forms.ToolStripItem[] dropDownItems)` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F1E8D92B810F3462 | Event | `public event System.EventHandler AvailableChanged` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A5D71EAD26D1698 | Event | `public event System.EventHandler BackColorChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CBB7AA92C6C17991 | Event | `public event System.EventHandler BindingContextChanged` | `System.Windows.Forms.BindableComponent`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-234B4834076A5D4E | Event | `public event System.EventHandler CheckedChanged` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B7FC9F6D9ED3DC51 | Event | `public event System.EventHandler CheckStateChanged` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7FB2F3EB2E99DA4B | Event | `public event System.EventHandler Click` | `System.Windows.Forms.ToolStripItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2DE366A12FCA996C | Event | `public event System.EventHandler CommandCanExecuteChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-089104FF88D86783 | Event | `public event System.EventHandler CommandChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F92EA63E88C20A40 | Event | `public event System.EventHandler CommandParameterChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F2B4B3DED6B61DAC | Event | `public event System.EventHandler DisplayStyleChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F55F9AD64AFD1D77 | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4898D944EBF8AB89 | Event | `public event System.EventHandler DoubleClick` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D19416B4AA9EC9F | Event | `public event System.Windows.Forms.DragEventHandler DragDrop` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CAA111905B5584A7 | Event | `public event System.Windows.Forms.DragEventHandler DragEnter` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2D65C016A0043140 | Event | `public event System.EventHandler DragLeave` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AA6E131EA5EC587A | Event | `public event System.Windows.Forms.DragEventHandler DragOver` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1882D6A5EE6ABBF3 | Event | `public event System.EventHandler DropDownClosed` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-59F0CE93FB04BF2F | Event | `public event System.Windows.Forms.ToolStripItemClickedEventHandler DropDownItemClicked` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E0B761CC24542A82 | Event | `public event System.EventHandler DropDownOpened` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B254BE62CEE961B1 | Event | `public event System.EventHandler DropDownOpening` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EF8096B9671105C6 | Event | `public event System.EventHandler EnabledChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4C2D7157B48F840C | Event | `public event System.EventHandler ForeColorChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-17BC60B4B0CEB57F | Event | `public event System.Windows.Forms.GiveFeedbackEventHandler GiveFeedback` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CF111919F1681093 | Event | `public event System.EventHandler LocationChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E3227EF1465B7ADC | Event | `public event System.Windows.Forms.MouseEventHandler MouseDown` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4015809E52B008C0 | Event | `public event System.EventHandler MouseEnter` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-74516CB82F069F42 | Event | `public event System.EventHandler MouseHover` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6154AC11A58D7A3A | Event | `public event System.EventHandler MouseLeave` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC68D33788AB32A9 | Event | `public event System.Windows.Forms.MouseEventHandler MouseMove` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-053E192E80E4C193 | Event | `public event System.Windows.Forms.MouseEventHandler MouseUp` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8D446FE2CF2C11EB | Event | `public event System.EventHandler OwnerChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D27E5BAFDE1C803B | Event | `public event System.Windows.Forms.PaintEventHandler Paint` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-93DB8BA21CB5DA8E | Event | `public event System.Windows.Forms.QueryAccessibilityHelpEventHandler QueryAccessibilityHelp` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-009D88A0A35F629A | Event | `public event System.Windows.Forms.QueryContinueDragEventHandler QueryContinueDrag` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A8300F0F754C2110 | Event | `public event System.EventHandler RightToLeftChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E97C20DA9B6C474 | Event | `public event System.EventHandler SelectedChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DD9DB9E9C1C8AB17 | Event | `public event System.EventHandler TextChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F697D86DDB5D1F5C | Event | `public event System.EventHandler VisibleChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B5809A335DF1A118 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1F0FE243DC2475A0 | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1FC2F827CFA5DA74 | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-988BE858B57B893A | Method | `public System.Windows.Forms.ToolStrip GetCurrentParent()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E79CBC558BCFBBC | Method | `public virtual System.Drawing.Size GetPreferredSize(System.Drawing.Size constrainingSize)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-899CFA7F01D57B47 | Method | `public System.Void HideDropDown()` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E4F8839419C7B840 | Method | `public System.Void Invalidate()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-458E031476CFCF36 | Method | `public System.Void Invalidate(System.Drawing.Rectangle r)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9988D574ABE24C8B | Method | `public System.Void PerformClick()` | `System.Windows.Forms.ToolStripItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2688D359FB623949 | Method | `public virtual System.Void ResetBackColor()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6E5F81850786CC58 | Method | `public virtual System.Void ResetDisplayStyle()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C83C90F753760994 | Method | `public virtual System.Void ResetFont()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3AD309F4A016082F | Method | `public virtual System.Void ResetForeColor()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-28896EC5C690FEC5 | Method | `public virtual System.Void ResetImage()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-97F93052EFCA8273 | Method | `public System.Void ResetMargin()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-043B57259A104305 | Method | `public System.Void ResetPadding()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D32F816623984641 | Method | `public virtual System.Void ResetRightToLeft()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D14BA362D708B101 | Method | `public virtual System.Void ResetTextDirection()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BA4062A8E3FFD8B6 | Method | `public System.Void Select()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-578E41A46482952C | Method | `public System.Void ShowDropDown()` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-89A4E55B601A34A4 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.ToolStripItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-425DAA03CCEAEB5E | Property | `System.Windows.Forms.AccessibleObject AccessibilityObject { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A27804119EF36A2E | Property | `System.String AccessibleDefaultActionDescription { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CA43F15733351654 | Property | `System.String AccessibleDescription { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4DA898A974C00D20 | Property | `System.String AccessibleName { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E75E84621B4A3DAF | Property | `System.Windows.Forms.AccessibleRole AccessibleRole { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(-1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3DB15F94848D011B | Property | `System.Windows.Forms.ToolStripItemAlignment Alignment { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9CC61836877593F8 | Property | `virtual System.Boolean AllowDrop { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(false); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BFC3501244DDCA6F | Property | `System.Windows.Forms.AnchorStyles Anchor { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0); DefaultValueAttribute(5) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-88AB4880416F299A | Property | `System.Boolean AutoSize { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(true); LocalizableAttribute(true); DesignerSerializationVisibilityAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-92406386A44760C8 | Property | `System.Boolean AutoToolTip { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A057EA76927325FD | Property | `System.Boolean Available { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-25A5B65601C722D0 | Property | `virtual System.Drawing.Color BackColor { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-98B59775D548E408 | Property | `virtual System.Drawing.Image BackgroundImage { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true); DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-902E741C088C25D1 | Property | `virtual System.Windows.Forms.ImageLayout BackgroundImageLayout { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(1); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B888C408204037BB | Property | `System.Windows.Forms.BindingContext BindingContext { public get; public set; }` | `System.Windows.Forms.BindableComponent` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-00B1B2ED4632E7DC | Property | `virtual System.Drawing.Rectangle Bounds { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9191FB1DD2B08C10 | Property | `virtual System.Boolean CanSelect { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D62FFF1504EF387A | Property | `System.Boolean Checked { public get; public set; }` | `System.Windows.Forms.ToolStripMenuItem` BindableAttribute(true); DefaultValueAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1FA6CA2703AA07B5 | Property | `System.Boolean CheckOnClick { public get; public set; }` | `System.Windows.Forms.ToolStripMenuItem` DefaultValueAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C1D37B396DA10C37 | Property | `System.Windows.Forms.CheckState CheckState { public get; public set; }` | `System.Windows.Forms.ToolStripMenuItem` BindableAttribute(true); DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1AA86EA403928258 | Property | `System.Windows.Input.ICommand Command { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BindableAttribute(true); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E18056E6EB8BD2D | Property | `System.Object CommandParameter { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BindableAttribute(true); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-07BFE7EC492F8BC7 | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-EFAB3A878AAAADB8 | Property | `System.Drawing.Rectangle ContentRectangle { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5BA626A13380692D | Property | `System.Windows.Forms.ControlBindingsCollection DataBindings { public get; }` | `System.Windows.Forms.BindableComponent` DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CA6BAA41356292B7 | Property | `virtual System.Windows.Forms.ToolStripItemDisplayStyle DisplayStyle { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-09810EFD874E2DB5 | Property | `System.Windows.Forms.DockStyle Dock { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3646833AB6726BC6 | Property | `System.Boolean DoubleClickEnabled { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-81C5C85AD26BAD52 | Property | `System.Windows.Forms.ToolStripDropDown DropDown { public get; public set; }` | `System.Windows.Forms.ToolStripDropDownItem` TypeConverterAttribute(System.ComponentModel.ReferenceConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-94A763E567E44560 | Property | `System.Windows.Forms.ToolStripDropDownDirection DropDownDirection { public get; public set; }` | `System.Windows.Forms.ToolStripDropDownItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0197573877A97397 | Property | `System.Windows.Forms.ToolStripItemCollection DropDownItems { public get; }` | `System.Windows.Forms.ToolStripDropDownItem` DesignerSerializationVisibilityAttribute(2) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-40E850027CDDB3CE | Property | `virtual System.Boolean Enabled { public get; public set; }` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C3BACA09794B754B | Property | `virtual System.Drawing.Font Font { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4DD8C126FC7FB0BE | Property | `virtual System.Drawing.Color ForeColor { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9DC9D1477A415C10 | Property | `System.Boolean HasDropDown { public get; }` | `System.Windows.Forms.ToolStripDropDownItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B44C9A3EA9DC501A | Property | `virtual System.Boolean HasDropDownItems { public get; }` | `System.Windows.Forms.ToolStripDropDownItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50E7D7FFCCA98B0E | Property | `System.Int32 Height { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A84A3B3AC428DE4C | Property | `virtual System.Drawing.Image Image { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0DE8B66244E5649B | Property | `System.Drawing.ContentAlignment ImageAlign { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(32); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F237AA95C111F17B | Property | `System.Int32 ImageIndex { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true); TypeConverterAttribute(System.Windows.Forms.NoneExcludedImageIndexConverter); EditorAttribute("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FA2AE3579C7AB5EC | Property | `System.String ImageKey { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true); TypeConverterAttribute(System.Windows.Forms.ImageKeyConverter); EditorAttribute("System.Windows.Forms.Design.ToolStripImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-52B777316A886473 | Property | `System.Windows.Forms.ToolStripItemImageScaling ImageScaling { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(1); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6F94EB164DD3B9A3 | Property | `System.Drawing.Color ImageTransparentColor { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9A608A1730B48105 | Property | `System.Boolean IsDisposed { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C7F59222D9772D88 | Property | `System.Boolean IsMdiWindowListEntry { public get; }` | `System.Windows.Forms.ToolStripMenuItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D0558E5C7F6616CE | Property | `System.Boolean IsOnDropDown { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C2EA13DABAFB605B | Property | `System.Boolean IsOnOverflow { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-483634E367A78BC0 | Property | `System.Windows.Forms.Padding Margin { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C5EDD303D9E3DAF8 | Property | `System.Windows.Forms.MergeAction MergeAction { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E8A353E6DF5D539 | Property | `System.Int32 MergeIndex { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(-1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7B19BD828677F025 | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DefaultValueAttribute(null) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-70A296C8A25F266F | Property | `System.Windows.Forms.ToolStripItemOverflow Overflow { public get; public set; }` | `System.Windows.Forms.ToolStripMenuItem` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B849343B22182EFB | Property | `System.Windows.Forms.ToolStrip Owner { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-56494109E768B4CA | Property | `System.Windows.Forms.ToolStripItem OwnerItem { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F93623D41F17CFAA | Property | `virtual System.Windows.Forms.Padding Padding { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-00152235A13EE6BC | Property | `System.Windows.Forms.ToolStripItemPlacement Placement { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C2CD70174D2FED48 | Property | `virtual System.Boolean Pressed { public get; }` | `System.Windows.Forms.ToolStripDropDownItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-10AF091E446E428D | Property | `virtual System.Windows.Forms.RightToLeft RightToLeft { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7CC96D2C4E420E21 | Property | `System.Boolean RightToLeftAutoMirrorImage { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(false); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-78C1CB69472EF5D3 | Property | `virtual System.Boolean Selected { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-35EC7867A3174578 | Property | `System.String ShortcutKeyDisplayString { public get; public set; }` | `System.Windows.Forms.ToolStripMenuItem` DefaultValueAttribute(null); LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FA6DA98C10DC54E7 | Property | `System.Windows.Forms.Keys ShortcutKeys { public get; public set; }` | `System.Windows.Forms.ToolStripMenuItem` LocalizableAttribute(true); DefaultValueAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F3BCCB94773F784B | Property | `System.Boolean ShowShortcutKeys { public get; public set; }` | `System.Windows.Forms.ToolStripMenuItem` DefaultValueAttribute(true); LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B15DA1E699D8BCE0 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CBB40ABEC98CF3B0 | Property | `virtual System.Drawing.Size Size { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-91DCBB520208A925 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(null); LocalizableAttribute(false); BindableAttribute(true); TypeConverterAttribute(System.ComponentModel.StringConverter) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-81635C61ACBDBF8C | Property | `virtual System.String Text { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(""); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-030C5BBD3193CAD8 | Property | `virtual System.Drawing.ContentAlignment TextAlign { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(32); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E8748999C02B0FC | Property | `virtual System.Windows.Forms.ToolStripTextDirection TextDirection { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D6ED2F4F1FD7DC2 | Property | `System.Windows.Forms.TextImageRelation TextImageRelation { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(4); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3503F6FD313C0ACA | Property | `System.String ToolTipText { public get; public set; }` | `System.Windows.Forms.ToolStripItem` EditorAttribute("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-58AA852747A894D8 | Property | `System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-271C03ED0C0EB86C | Property | `System.Int32 Width { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-4751C853E6D1D5C3 | Method | `protected virtual System.Windows.Forms.AccessibleObject CreateAccessibilityInstance()` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9754F074910091A4 | Method | `protected virtual System.Windows.Forms.ToolStripDropDown CreateDefaultDropDown()` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C82B4F07B81A0D0 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.ToolStripMenuItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9FE574E5527BB1F5 | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6427A2E28345ABF9 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-BD7D3B4846070CDA | Method | `protected internal virtual System.Boolean IsInputChar(System.Char charCode)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F0DD73B10FF7C57C | Method | `protected internal virtual System.Boolean IsInputKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-795D076F7595DEB5 | Method | `protected virtual System.Void OnAvailableChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1FB6990B8A9D552B | Method | `protected virtual System.Void OnBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FF6F7E6E25852EB2 | Method | `protected virtual System.Void OnBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.BindableComponent`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-351CEE499F300884 | Method | `protected virtual System.Void OnBoundsChanged()` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-19A807818E69A907 | Method | `protected virtual System.Void OnCheckedChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BCB022DF550258B3 | Method | `protected virtual System.Void OnCheckStateChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6DE3E0CDD42A9BC8 | Method | `protected virtual System.Void OnClick(System.EventArgs e)` | `System.Windows.Forms.ToolStripMenuItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-BAD2055A7632B059 | Method | `protected virtual System.Void OnCommandCanExecuteChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-10BB9F3936210F52 | Method | `protected virtual System.Void OnCommandChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3E2B15D0279D1D45 | Method | `protected virtual System.Void OnCommandParameterChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A42907DF265373E4 | Method | `protected virtual System.Void OnDisplayStyleChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2306BA02A329FA12 | Method | `protected virtual System.Void OnDoubleClick(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-33294A11CB8674FA | Method | `protected virtual System.Void OnDragDrop(System.Windows.Forms.DragEventArgs dragEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-45736B21BFDAA2A7 | Method | `protected virtual System.Void OnDragEnter(System.Windows.Forms.DragEventArgs dragEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F380DC926B7DFA8C | Method | `protected virtual System.Void OnDragLeave(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-793945F52FD0B7EA | Method | `protected virtual System.Void OnDragOver(System.Windows.Forms.DragEventArgs dragEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F1DA69EAB5681AEB | Method | `protected internal virtual System.Void OnDropDownClosed(System.EventArgs e)` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DC5679DBBE201A7A | Method | `protected virtual System.Void OnDropDownHide(System.EventArgs e)` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7A567DB04ACB5D44 | Method | `protected internal virtual System.Void OnDropDownItemClicked(System.Windows.Forms.ToolStripItemClickedEventArgs e)` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6A8F06A907560645 | Method | `protected internal virtual System.Void OnDropDownOpened(System.EventArgs e)` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-93FAB2A94B6A0139 | Method | `protected virtual System.Void OnDropDownShow(System.EventArgs e)` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0914DB13E73F0FC9 | Method | `protected virtual System.Void OnEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-86090D8D907AFC27 | Method | `protected virtual System.Void OnFontChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-21312627181E6EE9 | Method | `protected virtual System.Void OnForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-07EF6C37472A8228 | Method | `protected virtual System.Void OnGiveFeedback(System.Windows.Forms.GiveFeedbackEventArgs giveFeedbackEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-344AAE58B660D0EB | Method | `protected internal virtual System.Void OnLayout(System.Windows.Forms.LayoutEventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-32D6596DC7974757 | Method | `protected virtual System.Void OnLocationChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E3F74BB98A17F3E0 | Method | `protected virtual System.Void OnMouseDown(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6A22FC4F1ED92889 | Method | `protected virtual System.Void OnMouseEnter(System.EventArgs e)` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1553A170D359EC4E | Method | `protected virtual System.Void OnMouseHover(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-96C90A9CEF12FCA8 | Method | `protected virtual System.Void OnMouseLeave(System.EventArgs e)` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B213FF683E55CD8 | Method | `protected virtual System.Void OnMouseMove(System.Windows.Forms.MouseEventArgs mea)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B8DCDEF5A5C63D3 | Method | `protected virtual System.Void OnMouseUp(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D1E335E05F656985 | Method | `protected virtual System.Void OnOwnerChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A967030F3ED034C1 | Method | `protected internal virtual System.Void OnOwnerFontChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FA73DF46C55EDE4F | Method | `protected virtual System.Void OnPaint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D85153D101AB4C9 | Method | `protected virtual System.Void OnParentBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A7418CA600F21A6A | Method | `protected virtual System.Void OnParentChanged(System.Windows.Forms.ToolStrip oldParent, System.Windows.Forms.ToolStrip newParent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-998070A9741CE37D | Method | `protected internal virtual System.Void OnParentEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-959C7166D53933A6 | Method | `protected virtual System.Void OnParentForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-59AD881321EECB4A | Method | `protected internal virtual System.Void OnParentRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-01FB7343843E532B | Method | `protected virtual System.Void OnQueryContinueDrag(System.Windows.Forms.QueryContinueDragEventArgs queryContinueDragEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E3172F0DBA6DDF56 | Method | `protected virtual System.Void OnRequestCommandExecute(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F589D96E20AA7C14 | Method | `protected virtual System.Void OnRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FA791047047D6D1C | Method | `protected virtual System.Void OnSelectedChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BF9E65A4F6DC2A59 | Method | `protected virtual System.Void OnTextChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7E88BBEE38820BC2 | Method | `protected virtual System.Void OnVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1705E00FE1873CAC | Method | `protected internal virtual System.Boolean ProcessCmdKey(ref System.Windows.Forms.Message m, System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2C78CBE715F0DC68 | Method | `protected internal virtual System.Boolean ProcessDialogKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-38F5971E51C14AB4 | Method | `protected internal virtual System.Boolean ProcessMnemonic(System.Char charCode)` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C8F5A25210739E4D | Method | `protected internal virtual System.Void SetBounds(System.Drawing.Rectangle rect)` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3FC4D8E7CABF4664 | Method | `protected virtual System.Void SetVisibleCore(System.Boolean visible)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D4C5074CEAFD07AE | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3B522B3992CE35B0 | Property | `virtual System.Boolean DefaultAutoToolTip { protected get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C1377B75A7CE0708 | Property | `virtual System.Windows.Forms.ToolStripItemDisplayStyle DefaultDisplayStyle { protected get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-75175F4709FC94FF | Property | `virtual System.Windows.Forms.Padding DefaultMargin { protected internal get; }` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4EA1B119F7A0711B | Property | `virtual System.Windows.Forms.Padding DefaultPadding { protected get; }` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BD00B1924CDA95DC | Property | `virtual System.Drawing.Size DefaultSize { protected get; }` | `System.Windows.Forms.ToolStripMenuItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7A53D42ABFF2E311 | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FA06270FC30AADD8 | Property | `virtual System.Boolean DismissWhenClicked { protected internal get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9951B3D1D1DE1ECF | Property | `virtual System.Drawing.Point DropDownLocation { protected internal get; }` | `System.Windows.Forms.ToolStripDropDownItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7F93B0D0B05A2AED | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7ED16D00B217E8E2 | Property | `System.Windows.Forms.ToolStrip Parent { protected internal get; protected internal set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4A7CC97D9914AE98 | Property | `virtual System.Windows.Forms.ToolStripRenderer Renderer { protected internal get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D4A63B28D6AFB3B | Property | `virtual System.Boolean ShowKeyboardCues { protected internal get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
