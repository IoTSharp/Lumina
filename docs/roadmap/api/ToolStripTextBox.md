# ToolStripTextBox：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/ToolStripTextBox.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.ToolStripTextBox`；基类：`System.Windows.Forms.ToolStripControlHost`。Lumina 同名类型：🟡 存在；基类及基线接口形状：⬜ 未匹配。

公开属性 **87**，公开事件 **48**，公开方法 **37**；受保护扩展成员 **81**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-44E9086681DE4553 | Constructor | `public ToolStripTextBox()` | `System.Windows.Forms.ToolStripTextBox`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-942A705A4C103C40 | Constructor | `public ToolStripTextBox(System.String name)` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-562F798C318E959A | Constructor | `public ToolStripTextBox(System.Windows.Forms.Control c)` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A37F745762566FB6 | Event | `public event System.EventHandler AcceptsTabChanged` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8DE8C7A7FD17345C | Event | `public event System.EventHandler AvailableChanged` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ADB3628C1AE607BD | Event | `public event System.EventHandler BackColorChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50E5C2EC59409374 | Event | `public event System.EventHandler BindingContextChanged` | `System.Windows.Forms.BindableComponent`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9F1E92D8EBCA6703 | Event | `public event System.EventHandler BorderStyleChanged` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E5B86C3BE6193E9B | Event | `public event System.EventHandler Click` | `System.Windows.Forms.ToolStripItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CA4360DF81703572 | Event | `public event System.EventHandler CommandCanExecuteChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C9486E36F6BB8A05 | Event | `public event System.EventHandler CommandChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9CCB81DD5F7BB0B2 | Event | `public event System.EventHandler CommandParameterChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FB0ABA511580F4D9 | Event | `public event System.EventHandler DisplayStyleChanged` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-08C1F8BA94D0A564 | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-006DC632F1BF8299 | Event | `public event System.EventHandler DoubleClick` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-55D7EE429758FF8B | Event | `public event System.Windows.Forms.DragEventHandler DragDrop` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-467FD93AA5EC06BE | Event | `public event System.Windows.Forms.DragEventHandler DragEnter` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F492CB00F5BE08B9 | Event | `public event System.EventHandler DragLeave` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A49056C1E291F7DF | Event | `public event System.Windows.Forms.DragEventHandler DragOver` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5BC21C2AC2C0B82A | Event | `public event System.EventHandler EnabledChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6A059977E1EE6BD5 | Event | `public event System.EventHandler Enter` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-39DC8023C2AF5129 | Event | `public event System.EventHandler ForeColorChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-86E6AB7FC1D84B72 | Event | `public event System.Windows.Forms.GiveFeedbackEventHandler GiveFeedback` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-35F33C22DD9BA350 | Event | `public event System.EventHandler GotFocus` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D0E9B8EE9707815 | Event | `public event System.EventHandler HideSelectionChanged` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0968390C7DB70BB1 | Event | `public event System.Windows.Forms.KeyEventHandler KeyDown` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C7E770376F29B162 | Event | `public event System.Windows.Forms.KeyPressEventHandler KeyPress` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C2F6FBAFAE88BC78 | Event | `public event System.Windows.Forms.KeyEventHandler KeyUp` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7CA6886D9063B954 | Event | `public event System.EventHandler Leave` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-96D09D5194EE812A | Event | `public event System.EventHandler LocationChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8653FEB3714C8D7B | Event | `public event System.EventHandler LostFocus` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-21CA8E1F6E9AFEE3 | Event | `public event System.EventHandler ModifiedChanged` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-926C465BDBC9E070 | Event | `public event System.Windows.Forms.MouseEventHandler MouseDown` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A32F92D6DD4CB3F4 | Event | `public event System.EventHandler MouseEnter` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-117F040D1DEA0EEC | Event | `public event System.EventHandler MouseHover` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A82E63244CD8CD8 | Event | `public event System.EventHandler MouseLeave` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F1ADE27539D9D321 | Event | `public event System.Windows.Forms.MouseEventHandler MouseMove` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FC4433CCFDB57BC4 | Event | `public event System.Windows.Forms.MouseEventHandler MouseUp` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6DEED3299F242585 | Event | `public event System.EventHandler MultilineChanged` | `System.Windows.Forms.ToolStripTextBox` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D3FA047B3C93BAE9 | Event | `public event System.EventHandler OwnerChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-82651CF75A1BB65E | Event | `public event System.Windows.Forms.PaintEventHandler Paint` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8B77DDAA0FD9D4A9 | Event | `public event System.Windows.Forms.QueryAccessibilityHelpEventHandler QueryAccessibilityHelp` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6CB55D4ECC3A9D9C | Event | `public event System.Windows.Forms.QueryContinueDragEventHandler QueryContinueDrag` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2F262370408F41F9 | Event | `public event System.EventHandler ReadOnlyChanged` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5037FFC07D39D175 | Event | `public event System.EventHandler RightToLeftChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-072B26F608A03A6C | Event | `public event System.EventHandler SelectedChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9140579DBDB42B21 | Event | `public event System.EventHandler TextBoxTextAlignChanged` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3A5BBD23C1BF0A92 | Event | `public event System.EventHandler TextChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6D6EF03E0E79F2EA | Event | `public event System.EventHandler Validated` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-578C5B20EAA9D121 | Event | `public event System.ComponentModel.CancelEventHandler Validating` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A0A8A3C1294AFF7 | Event | `public event System.EventHandler VisibleChanged` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-28A7ACEBCDFBEB31 | Method | `public System.Void AppendText(System.String text)` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AE5981759AEA2E24 | Method | `public System.Void Clear()` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CB3CE019766235FD | Method | `public System.Void ClearUndo()` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-852463235723A88D | Method | `public System.Void Copy()` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C5BF4D606B1961B3 | Method | `public System.Void Cut()` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E8D931402166CBB9 | Method | `public System.Void DeselectAll()` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-75D0B6B05E904D25 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3448757B6DF2110E | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-228BA2A91379B9C0 | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BF34A60EAAE46574 | Method | `public System.Void Focus()` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2156E1050A8CA9BE | Method | `public System.Char GetCharFromPosition(System.Drawing.Point pt)` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1C0203B5F711DDB8 | Method | `public System.Int32 GetCharIndexFromPosition(System.Drawing.Point pt)` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-15D7B7BFEA142870 | Method | `public System.Windows.Forms.ToolStrip GetCurrentParent()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E16196AF158AE769 | Method | `public System.Int32 GetFirstCharIndexFromLine(System.Int32 lineNumber)` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A052724187929838 | Method | `public System.Int32 GetFirstCharIndexOfCurrentLine()` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-25284A10B66F6DF5 | Method | `public System.Int32 GetLineFromCharIndex(System.Int32 index)` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EDFD6DD1F61D08BF | Method | `public System.Drawing.Point GetPositionFromCharIndex(System.Int32 index)` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1BEA01AF25BB6525 | Method | `public virtual System.Drawing.Size GetPreferredSize(System.Drawing.Size constrainingSize)` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E5CC0268758CD6B5 | Method | `public System.Void Invalidate()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-53C581DF57834DB9 | Method | `public System.Void Invalidate(System.Drawing.Rectangle r)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3247492F9A6239C3 | Method | `public System.Void Paste()` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B190F7A802755040 | Method | `public System.Void PerformClick()` | `System.Windows.Forms.ToolStripItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-73D454AA69FF5842 | Method | `public virtual System.Void ResetBackColor()` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D7498EB233763A10 | Method | `public virtual System.Void ResetDisplayStyle()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EA77CD8742A377E5 | Method | `public virtual System.Void ResetFont()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-585F32789BFC26DD | Method | `public virtual System.Void ResetForeColor()` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-976B3766AB451C2C | Method | `public virtual System.Void ResetImage()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6C536F18D5A83CD9 | Method | `public System.Void ResetMargin()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B4BADEAFC96DC36A | Method | `public System.Void ResetPadding()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F0656E58EBE7E4D | Method | `public virtual System.Void ResetRightToLeft()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AB63D1E539A062B6 | Method | `public virtual System.Void ResetTextDirection()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A3E1048219DA07C | Method | `public System.Void ScrollToCaret()` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BC3AAF10D7501943 | Method | `public System.Void Select()` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-67C316E486DA2076 | Method | `public System.Void Select(System.Int32 start, System.Int32 length)` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-59D48501DE9D6BDA | Method | `public System.Void SelectAll()` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-32C0DF22ACE33872 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.ToolStripItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D52DEC4477EAE5D3 | Method | `public System.Void Undo()` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AA571C1EBA47F71F | Property | `System.Boolean AcceptsReturn { public get; public set; }` | `System.Windows.Forms.ToolStripTextBox` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-79EE32174AB239D6 | Property | `System.Boolean AcceptsTab { public get; public set; }` | `System.Windows.Forms.ToolStripTextBox` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A4A03AFB0C2F4EC2 | Property | `System.Windows.Forms.AccessibleObject AccessibilityObject { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1DBA5B7ED4C4D55B | Property | `System.String AccessibleDefaultActionDescription { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CD2D3144A83DC026 | Property | `System.String AccessibleDescription { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-637B555ACC6041D4 | Property | `System.String AccessibleName { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5DD8E317981ADA7E | Property | `System.Windows.Forms.AccessibleRole AccessibleRole { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(-1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5EDB3389FAF45AFE | Property | `System.Windows.Forms.ToolStripItemAlignment Alignment { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC911C09C38F6EF5 | Property | `virtual System.Boolean AllowDrop { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(false); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C99B8E430B674506 | Property | `System.Windows.Forms.AnchorStyles Anchor { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0); DefaultValueAttribute(5) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A2671006763BEC3D | Property | `System.Windows.Forms.AutoCompleteStringCollection AutoCompleteCustomSource { public get; public set; }` | `System.Windows.Forms.ToolStripTextBox` DesignerSerializationVisibilityAttribute(2); LocalizableAttribute(true); EditorAttribute("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DDBB1B1674DCAD51 | Property | `System.Windows.Forms.AutoCompleteMode AutoCompleteMode { public get; public set; }` | `System.Windows.Forms.ToolStripTextBox` DefaultValueAttribute(0); BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D83E58554B0DA0A | Property | `System.Windows.Forms.AutoCompleteSource AutoCompleteSource { public get; public set; }` | `System.Windows.Forms.ToolStripTextBox` DefaultValueAttribute(128); BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-46A7AE24F749C94B | Property | `System.Boolean AutoSize { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(true); LocalizableAttribute(true); DesignerSerializationVisibilityAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0447C0893C07EC99 | Property | `System.Boolean AutoToolTip { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B956AF738ECA354 | Property | `System.Boolean Available { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D60FB71E89D31BB8 | Property | `virtual System.Drawing.Color BackColor { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5ECBF9FE0FF45FA7 | Property | `virtual System.Drawing.Image BackgroundImage { public get; public set; }` | `System.Windows.Forms.ToolStripTextBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4194B0DF9FE48E94 | Property | `virtual System.Windows.Forms.ImageLayout BackgroundImageLayout { public get; public set; }` | `System.Windows.Forms.ToolStripTextBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3D97F6D9D5CBBE58 | Property | `System.Windows.Forms.BindingContext BindingContext { public get; public set; }` | `System.Windows.Forms.BindableComponent` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AF9CBA17699AB79A | Property | `System.Windows.Forms.BorderStyle BorderStyle { public get; public set; }` | `System.Windows.Forms.ToolStripTextBox` DefaultValueAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BAE4C4DF133B616B | Property | `virtual System.Drawing.Rectangle Bounds { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-960B550FED2B4647 | Property | `virtual System.Boolean CanSelect { public get; }` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F40A7B077E36B290 | Property | `System.Boolean CanUndo { public get; }` | `System.Windows.Forms.ToolStripTextBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D707C8D2454BCE55 | Property | `System.Boolean CausesValidation { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D3E5325D483B35F4 | Property | `System.Windows.Forms.CharacterCasing CharacterCasing { public get; public set; }` | `System.Windows.Forms.ToolStripTextBox` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-81185E89D3FBC357 | Property | `System.Windows.Input.ICommand Command { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BindableAttribute(true); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-288F77929F0134AD | Property | `System.Object CommandParameter { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BindableAttribute(true); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CF8C5A19094D32B4 | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-ACA851C4F3F8AB4A | Property | `System.Drawing.Rectangle ContentRectangle { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E8C5C7C54F518C5 | Property | `System.Windows.Forms.Control Control { public get; }` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B6DF7A0FCE33D458 | Property | `System.Drawing.ContentAlignment ControlAlign { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost` DefaultValueAttribute(32); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8AEC01E4CD6B9ECC | Property | `System.Windows.Forms.ControlBindingsCollection DataBindings { public get; }` | `System.Windows.Forms.BindableComponent` DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D818A985ECE0ECE3 | Property | `System.Windows.Forms.ToolStripItemDisplayStyle DisplayStyle { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F79EF5647E150360 | Property | `System.Windows.Forms.DockStyle Dock { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C6EFD7A4885D9603 | Property | `System.Boolean DoubleClickEnabled { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost` DefaultValueAttribute(false); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4C4216688E2381D6 | Property | `virtual System.Boolean Enabled { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C7F98C3300EC8A26 | Property | `virtual System.Boolean Focused { public get; }` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3F8C35628138AE66 | Property | `virtual System.Drawing.Font Font { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5BC5B0CFE7E854A3 | Property | `virtual System.Drawing.Color ForeColor { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-499B54EF630B7D7E | Property | `System.Int32 Height { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1A9B2A0E72ACFCF5 | Property | `System.Boolean HideSelection { public get; public set; }` | `System.Windows.Forms.ToolStripTextBox` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-59666D55BD72DF2A | Property | `virtual System.Drawing.Image Image { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C941969B594FB3D | Property | `System.Drawing.ContentAlignment ImageAlign { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A5DEE3285BA47BCE | Property | `System.Int32 ImageIndex { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true); TypeConverterAttribute(System.Windows.Forms.NoneExcludedImageIndexConverter); EditorAttribute("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ECA5262CB5D3536A | Property | `System.String ImageKey { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true); TypeConverterAttribute(System.Windows.Forms.ImageKeyConverter); EditorAttribute("System.Windows.Forms.Design.ToolStripImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D45034EAA5DEDB00 | Property | `System.Windows.Forms.ToolStripItemImageScaling ImageScaling { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AB2088C050F416B1 | Property | `System.Drawing.Color ImageTransparentColor { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F4A7635B0C42C811 | Property | `System.Boolean IsDisposed { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6BBB6C58ACE3A1E7 | Property | `System.Boolean IsOnDropDown { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F4984AE9CE84A7E5 | Property | `System.Boolean IsOnOverflow { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-82D763037DE8A40A | Property | `System.String[] Lines { public get; public set; }` | `System.Windows.Forms.ToolStripTextBox` DesignerSerializationVisibilityAttribute(0); LocalizableAttribute(true); EditorAttribute("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E9198B560A71E715 | Property | `System.Windows.Forms.Padding Margin { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-03BD3C85C6C6E6CC | Property | `System.Int32 MaxLength { public get; public set; }` | `System.Windows.Forms.ToolStripTextBox` DefaultValueAttribute(32767); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-58F9B455F78882D2 | Property | `System.Windows.Forms.MergeAction MergeAction { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-76A5186A4BFE0097 | Property | `System.Int32 MergeIndex { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(-1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4070509BA6395E2B | Property | `System.Boolean Modified { public get; public set; }` | `System.Windows.Forms.ToolStripTextBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A6542CD60027C5D1 | Property | `System.Boolean Multiline { public get; public set; }` | `System.Windows.Forms.ToolStripTextBox` DefaultValueAttribute(false); LocalizableAttribute(true); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F00E430E52E3BC13 | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DefaultValueAttribute(null) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-33781A852E2C27E7 | Property | `System.Windows.Forms.ToolStripItemOverflow Overflow { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6CD00DAD6566973A | Property | `System.Windows.Forms.ToolStrip Owner { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2348796EC2ED4892 | Property | `System.Windows.Forms.ToolStripItem OwnerItem { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FCE904CB8B3E0263 | Property | `virtual System.Windows.Forms.Padding Padding { public get; public set; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9441D61D5DA99FD6 | Property | `System.Windows.Forms.ToolStripItemPlacement Placement { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-63E9BE08B043EAE7 | Property | `virtual System.Boolean Pressed { public get; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-92B4DB3E09E6FE62 | Property | `System.Boolean ReadOnly { public get; public set; }` | `System.Windows.Forms.ToolStripTextBox` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-34D2005B257A4E26 | Property | `virtual System.Windows.Forms.RightToLeft RightToLeft { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-043FDEF64F751994 | Property | `System.Boolean RightToLeftAutoMirrorImage { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4A078B417C2CC64C | Property | `virtual System.Boolean Selected { public get; }` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4C5DB747B49A09E4 | Property | `System.String SelectedText { public get; public set; }` | `System.Windows.Forms.ToolStripTextBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-710B5173B2A8421D | Property | `System.Int32 SelectionLength { public get; public set; }` | `System.Windows.Forms.ToolStripTextBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D6E5885AAD2CB995 | Property | `System.Int32 SelectionStart { public get; public set; }` | `System.Windows.Forms.ToolStripTextBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-52B1D7ABA4CB1647 | Property | `System.Boolean ShortcutsEnabled { public get; public set; }` | `System.Windows.Forms.ToolStripTextBox` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C4854CE82960ABB5 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-168FA5584072BAEC | Property | `virtual System.Drawing.Size Size { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BF385789364FE756 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.ToolStripItem` DefaultValueAttribute(null); LocalizableAttribute(false); BindableAttribute(true); TypeConverterAttribute(System.ComponentModel.StringConverter) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1608FE7F1728AD18 | Property | `virtual System.String Text { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost` DefaultValueAttribute("") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F82F493754D57AD7 | Property | `System.Drawing.ContentAlignment TextAlign { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-40B8DD63924A9072 | Property | `System.Windows.Forms.TextBox TextBox { public get; }` | `System.Windows.Forms.ToolStripTextBox` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E9CB621B5E3F353F | Property | `System.Windows.Forms.HorizontalAlignment TextBoxTextAlign { public get; public set; }` | `System.Windows.Forms.ToolStripTextBox` LocalizableAttribute(true); DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A6914C775DCB36E4 | Property | `virtual System.Windows.Forms.ToolStripTextDirection TextDirection { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false); DefaultValueAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7C589C49994478B8 | Property | `System.Windows.Forms.TextImageRelation TextImageRelation { public get; public set; }` | `System.Windows.Forms.ToolStripControlHost` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D69657EA9E74DBCA | Property | `System.Int32 TextLength { public get; }` | `System.Windows.Forms.ToolStripTextBox` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-580D9CE782F4DC3C | Property | `System.String ToolTipText { public get; public set; }` | `System.Windows.Forms.ToolStripItem` EditorAttribute("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D9ADAF743F51DE58 | Property | `System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.ToolStripItem` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-65D0E9C268C75106 | Property | `System.Int32 Width { public get; public set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FE3019B0CB9E41AE | Property | `System.Boolean WordWrap { public get; public set; }` | `System.Windows.Forms.ToolStripTextBox` LocalizableAttribute(true); DefaultValueAttribute(true); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-6A3BC55A99FB0D1F | Method | `protected virtual System.Windows.Forms.AccessibleObject CreateAccessibilityInstance()` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4C08F06F6862181B | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.ToolStripControlHost`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E86F555CE30DB0F4 | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-12264B8DEB93CD73 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E088C44A2A19C5E0 | Method | `protected internal virtual System.Boolean IsInputChar(System.Char charCode)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16B559228F838D30 | Method | `protected internal virtual System.Boolean IsInputKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-347A35772EBCEAE1 | Method | `protected virtual System.Void OnAcceptsTabChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1928468A3226654A | Method | `protected virtual System.Void OnAvailableChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B2201F394962FE0 | Method | `protected virtual System.Void OnBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CABA31A4E7017FCA | Method | `protected virtual System.Void OnBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.BindableComponent`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E497E0F86A34AAE8 | Method | `protected virtual System.Void OnBorderStyleChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-44DA9EB5C5D79738 | Method | `protected virtual System.Void OnBoundsChanged()` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A0EC15A1BFAB00CE | Method | `protected virtual System.Void OnClick(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2367D05B825E5CBF | Method | `protected virtual System.Void OnCommandCanExecuteChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-43BF60BD17E29FF0 | Method | `protected virtual System.Void OnCommandChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A665A233FA4C61A7 | Method | `protected virtual System.Void OnCommandParameterChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4A712270BE031836 | Method | `protected virtual System.Void OnDisplayStyleChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C16104BF0DA48003 | Method | `protected virtual System.Void OnDoubleClick(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5DA408C3DBFC6298 | Method | `protected virtual System.Void OnDragDrop(System.Windows.Forms.DragEventArgs dragEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C871FCD91F5EB3DC | Method | `protected virtual System.Void OnDragEnter(System.Windows.Forms.DragEventArgs dragEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8466916430774124 | Method | `protected virtual System.Void OnDragLeave(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9DDC406D645749BC | Method | `protected virtual System.Void OnDragOver(System.Windows.Forms.DragEventArgs dragEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C9342DF1F6A44149 | Method | `protected virtual System.Void OnEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1E0B7D1564B6E845 | Method | `protected virtual System.Void OnEnter(System.EventArgs e)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-67DBBB49749CE899 | Method | `protected virtual System.Void OnFontChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BCC2A35BBECA2081 | Method | `protected virtual System.Void OnForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D37F95BD29E80370 | Method | `protected virtual System.Void OnGiveFeedback(System.Windows.Forms.GiveFeedbackEventArgs giveFeedbackEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FD6E9002384638F5 | Method | `protected virtual System.Void OnGotFocus(System.EventArgs e)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-801750E58FD6BA61 | Method | `protected virtual System.Void OnHideSelectionChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-46C538ABB2768F78 | Method | `protected virtual System.Void OnHostedControlResize(System.EventArgs e)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6CA1333095A97C4D | Method | `protected virtual System.Void OnKeyDown(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BAC86C4CAB232ACA | Method | `protected virtual System.Void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-040C273CC7A35CE0 | Method | `protected virtual System.Void OnKeyUp(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-44218BCDA299DD64 | Method | `protected internal virtual System.Void OnLayout(System.Windows.Forms.LayoutEventArgs e)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-952DEDB3AC2A8533 | Method | `protected virtual System.Void OnLeave(System.EventArgs e)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-265E0687CF265692 | Method | `protected virtual System.Void OnLocationChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4F418B85A212E214 | Method | `protected virtual System.Void OnLostFocus(System.EventArgs e)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4205E96B691D4F4F | Method | `protected virtual System.Void OnModifiedChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7139196BC1029546 | Method | `protected virtual System.Void OnMouseDown(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DBF5C90DC7C9F932 | Method | `protected virtual System.Void OnMouseEnter(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E9E3F9E545825625 | Method | `protected virtual System.Void OnMouseHover(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-648AA962688E3ACB | Method | `protected virtual System.Void OnMouseLeave(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E646AE5BBE481EC3 | Method | `protected virtual System.Void OnMouseMove(System.Windows.Forms.MouseEventArgs mea)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-76A553D8D8E869D3 | Method | `protected virtual System.Void OnMouseUp(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1DFD84358758F5FB | Method | `protected virtual System.Void OnMultilineChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7CAD97D98AB9E970 | Method | `protected virtual System.Void OnOwnerChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DCA2D5B016F4A41F | Method | `protected internal virtual System.Void OnOwnerFontChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B4CB92CADD8CBF71 | Method | `protected virtual System.Void OnPaint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C5DAE87EBABC2D55 | Method | `protected virtual System.Void OnParentBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EA2AD5C93168C2F9 | Method | `protected virtual System.Void OnParentChanged(System.Windows.Forms.ToolStrip oldParent, System.Windows.Forms.ToolStrip newParent)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D769A14755AA595 | Method | `protected internal virtual System.Void OnParentEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F744CA68AFE21A74 | Method | `protected virtual System.Void OnParentForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B625C085829E66E3 | Method | `protected internal virtual System.Void OnParentRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2CC8F7B088BC64F6 | Method | `protected virtual System.Void OnQueryContinueDrag(System.Windows.Forms.QueryContinueDragEventArgs queryContinueDragEvent)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E5E425B5F7EB15C9 | Method | `protected virtual System.Void OnReadOnlyChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1FB06DA2CD9A5FA2 | Method | `protected virtual System.Void OnRequestCommandExecute(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8F6D2B0CB5A7AD6A | Method | `protected virtual System.Void OnRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E188BD932559BD4 | Method | `protected virtual System.Void OnSelectedChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EB92D3F894A27C13 | Method | `protected virtual System.Void OnSubscribeControlEvents(System.Windows.Forms.Control control)` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-76E35CC9BA4BAE3A | Method | `protected virtual System.Void OnTextChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-724F6B6B12BFB664 | Method | `protected virtual System.Void OnUnsubscribeControlEvents(System.Windows.Forms.Control control)` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-80086DF4031687D4 | Method | `protected virtual System.Void OnValidated(System.EventArgs e)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-704D4FB3B240DC10 | Method | `protected virtual System.Void OnValidating(System.ComponentModel.CancelEventArgs e)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16D11DA062879DC0 | Method | `protected virtual System.Void OnVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-54970E367B9FB284 | Method | `protected internal virtual System.Boolean ProcessCmdKey(ref System.Windows.Forms.Message m, System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8710FE6854E23AD1 | Method | `protected internal virtual System.Boolean ProcessDialogKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E093A8C4F172AF17 | Method | `protected internal virtual System.Boolean ProcessMnemonic(System.Char charCode)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AE116B12A746B4AC | Method | `protected internal virtual System.Void SetBounds(System.Drawing.Rectangle bounds)` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A0759AC11D1F9B2B | Method | `protected virtual System.Void SetVisibleCore(System.Boolean visible)` | `System.Windows.Forms.ToolStripControlHost`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-282BE1D39C35D9A1 | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-031C6D8010816E48 | Property | `virtual System.Boolean DefaultAutoToolTip { protected get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-435712564432A9E9 | Property | `virtual System.Windows.Forms.ToolStripItemDisplayStyle DefaultDisplayStyle { protected get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-224C0A416335630D | Property | `virtual System.Windows.Forms.Padding DefaultMargin { protected internal get; }` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-79F744E9DD0CA706 | Property | `virtual System.Windows.Forms.Padding DefaultPadding { protected get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-83BDAC8366F6F380 | Property | `virtual System.Drawing.Size DefaultSize { protected get; }` | `System.Windows.Forms.ToolStripTextBox`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A04F88B24515D0A | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4D1CB4F255A03569 | Property | `virtual System.Boolean DismissWhenClicked { protected internal get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-805073D3A2DEA25D | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-612866784AA757DD | Property | `System.Windows.Forms.ToolStrip Parent { protected internal get; protected internal set; }` | `System.Windows.Forms.ToolStripItem` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AA8701F0EE015B15 | Property | `virtual System.Windows.Forms.ToolStripRenderer Renderer { protected internal get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6A52121C422AFCC2 | Property | `virtual System.Boolean ShowKeyboardCues { protected internal get; }` | `System.Windows.Forms.ToolStripItem`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
