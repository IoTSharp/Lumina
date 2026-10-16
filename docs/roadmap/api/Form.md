# Form：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/Form.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.Form`；基类：`System.Windows.Forms.ContainerControl`。Lumina 同名类型：🟡 存在；基类及基线接口形状：⬜ 未匹配。

公开属性 **143**，公开事件 **99**，公开方法 **95**；受保护扩展成员 **216**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-AA4B38C2C10FF423 | Constructor | `public Form()` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E23EA155875EEBE0 | Event | `public event System.EventHandler Activated` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1BA9182A7D247689 | Event | `public event System.EventHandler AutoSizeChanged` | `System.Windows.Forms.Form` BrowsableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5CA7F4F021B697FF | Event | `public event System.EventHandler AutoValidateChanged` | `System.Windows.Forms.Form` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-93CFBD47B30A665A | Event | `public event System.EventHandler BackColorChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-EF1704113C6D794B | Event | `public event System.EventHandler BackgroundImageChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-EA656FFD12E2F652 | Event | `public event System.EventHandler BackgroundImageLayoutChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-EAA1E3719D2E5CC1 | Event | `public event System.EventHandler BindingContextChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-32851F8AFE7E7DFA | Event | `public event System.EventHandler CausesValidationChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A25A15FFDBCFB1F8 | Event | `public event System.Windows.Forms.UICuesEventHandler ChangeUICues` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9078BA68EE24B70F | Event | `public event System.EventHandler Click` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E32E5EE0969C23B5 | Event | `public event System.EventHandler ClientSizeChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-12E90CDC7206DFF9 | Event | `public event System.EventHandler Closed` | `System.Windows.Forms.Form` BrowsableAttribute(false); ObsoleteAttribute("Form.OnClosing, Form.OnClosed and the corresponding events are obsolete. Use Form.OnFormClosing, Form.OnFormClosed, Form.FormClosing and Form.FormClosed instead.",false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-445D41A7B1E54297 | Event | `public event System.ComponentModel.CancelEventHandler Closing` | `System.Windows.Forms.Form` BrowsableAttribute(false); ObsoleteAttribute("Form.OnClosing, Form.OnClosed and the corresponding events are obsolete. Use Form.OnFormClosing, Form.OnFormClosed, Form.FormClosing and Form.FormClosed instead.",false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-188BEBF229703356 | Event | `public event System.EventHandler ContextMenuChanged` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-92C2CB62E9CC1C82 | Event | `public event System.EventHandler ContextMenuStripChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-18E5A6FF0EE32849 | Event | `public event System.Windows.Forms.ControlEventHandler ControlAdded` | `System.Windows.Forms.Control` BrowsableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3031538363E00D4D | Event | `public event System.Windows.Forms.ControlEventHandler ControlRemoved` | `System.Windows.Forms.Control` BrowsableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E9B2BB011FB35036 | Event | `public event System.EventHandler CursorChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CFB52D8419C8282C | Event | `public event System.EventHandler DataContextChanged` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-763F873C5E54F478 | Event | `public event System.EventHandler Deactivate` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC5A8A7854D75415 | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-92BD383EC8DC330C | Event | `public event System.EventHandler DockChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-26F583695A28144F | Event | `public event System.EventHandler DoubleClick` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F144A2F0CB1F2F14 | Event | `public event System.Windows.Forms.DpiChangedEventHandler DpiChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6F58B73C4C830852 | Event | `public event System.EventHandler DpiChangedAfterParent` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D50917218E6BA81C | Event | `public event System.EventHandler DpiChangedBeforeParent` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-56358B8638E81579 | Event | `public event System.Windows.Forms.DragEventHandler DragDrop` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FED152C3B7CDF6D9 | Event | `public event System.Windows.Forms.DragEventHandler DragEnter` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A79EF74368928B86 | Event | `public event System.EventHandler DragLeave` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-26C2BFCABF106891 | Event | `public event System.Windows.Forms.DragEventHandler DragOver` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-57CE4FD15E2D1343 | Event | `public event System.EventHandler EnabledChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-571B8D4AC89D54C1 | Event | `public event System.EventHandler Enter` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-33FE0B5FD0B052E0 | Event | `public event System.EventHandler FontChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D9D466A6A865BBB8 | Event | `public event System.EventHandler ForeColorChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7D5D093DCCA30EE1 | Event | `public event System.EventHandler FormBorderColorChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FE124476719F34A4 | Event | `public event System.EventHandler FormCaptionBackColorChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F913ACCF6F98F3A3 | Event | `public event System.EventHandler FormCaptionTextColorChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B3EA38E1B6E051A3 | Event | `public event System.Windows.Forms.FormClosedEventHandler FormClosed` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D9DD447F2AF51331 | Event | `public event System.Windows.Forms.FormClosingEventHandler FormClosing` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BB7C1C8894AB0A35 | Event | `public event System.EventHandler FormCornerPreferenceChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FF973CC9815DEB5B | Event | `public event System.Windows.Forms.GiveFeedbackEventHandler GiveFeedback` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-46DF253B116B7027 | Event | `public event System.EventHandler GotFocus` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E9E6C7C26E38734 | Event | `public event System.EventHandler HandleCreated` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2F67D3F5DCD1DB47 | Event | `public event System.EventHandler HandleDestroyed` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-DCAD946731BB0AAF | Event | `public event System.ComponentModel.CancelEventHandler HelpButtonClicked` | `System.Windows.Forms.Form` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AA22180C7C98A51A | Event | `public event System.Windows.Forms.HelpEventHandler HelpRequested` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DB6410EC437EAD1E | Event | `public event System.EventHandler ImeModeChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8480C254C682CA97 | Event | `public event System.Windows.Forms.InputLanguageChangedEventHandler InputLanguageChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C5B022D57C56C05A | Event | `public event System.Windows.Forms.InputLanguageChangingEventHandler InputLanguageChanging` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7CC85CA29C7AAF9F | Event | `public event System.Windows.Forms.InvalidateEventHandler Invalidated` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0776323B32DE2CAA | Event | `public event System.Windows.Forms.KeyEventHandler KeyDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-27B080153041D761 | Event | `public event System.Windows.Forms.KeyPressEventHandler KeyPress` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-40D82DFC7AB45A29 | Event | `public event System.Windows.Forms.KeyEventHandler KeyUp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E771511E87F1A880 | Event | `public event System.Windows.Forms.LayoutEventHandler Layout` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4C2AD1765D37479C | Event | `public event System.EventHandler Leave` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-85F29A113A71F655 | Event | `public event System.EventHandler Load` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9B0E727EEF905D16 | Event | `public event System.EventHandler LocationChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1F2C181990250195 | Event | `public event System.EventHandler LostFocus` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-959BF56F8AC75641 | Event | `public event System.EventHandler MarginChanged` | `System.Windows.Forms.Form` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-AE2BE9B7001DBE74 | Event | `public event System.EventHandler MaximizedBoundsChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-49ED7A67AE1D74B1 | Event | `public event System.EventHandler MaximumSizeChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9280C43916CC7F8F | Event | `public event System.EventHandler MdiChildActivate` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16E049AAB5C3761B | Event | `public event System.EventHandler MenuComplete` | `System.Windows.Forms.Form` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC6BA64666F98ED3 | Event | `public event System.EventHandler MenuStart` | `System.Windows.Forms.Form` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-02DD725F69C4D68E | Event | `public event System.EventHandler MinimumSizeChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2F34660FE46C7620 | Event | `public event System.EventHandler MouseCaptureChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-8FB209BE8DF665B0 | Event | `public event System.Windows.Forms.MouseEventHandler MouseClick` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F28E63D821411765 | Event | `public event System.Windows.Forms.MouseEventHandler MouseDoubleClick` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B6376823D3C05B6E | Event | `public event System.Windows.Forms.MouseEventHandler MouseDown` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-69EEFBC447B84D9D | Event | `public event System.EventHandler MouseEnter` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-635B1D316328D257 | Event | `public event System.EventHandler MouseHover` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-721869CA1CE6D336 | Event | `public event System.EventHandler MouseLeave` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-ABE6CDC91B5A0CEE | Event | `public event System.Windows.Forms.MouseEventHandler MouseMove` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-8CC3D4E28CD09B4C | Event | `public event System.Windows.Forms.MouseEventHandler MouseUp` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5C8DD9972A33CF66 | Event | `public event System.Windows.Forms.MouseEventHandler MouseWheel` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-759E51315943AD90 | Event | `public event System.EventHandler Move` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-82E60AF488494ED2 | Event | `public event System.EventHandler PaddingChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-11D6D3D324D0CCF9 | Event | `public event System.Windows.Forms.PaintEventHandler Paint` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F4474E75C4B34530 | Event | `public event System.EventHandler ParentChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4097C2B7DD180CA8 | Event | `public event System.Windows.Forms.PreviewKeyDownEventHandler PreviewKeyDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BFB812580B8EF0B0 | Event | `public event System.Windows.Forms.QueryAccessibilityHelpEventHandler QueryAccessibilityHelp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6B40E5BC27E89C38 | Event | `public event System.Windows.Forms.QueryContinueDragEventHandler QueryContinueDrag` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-60F74AF1ECEA5F18 | Event | `public event System.EventHandler RegionChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8298A2A487356002 | Event | `public event System.EventHandler Resize` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-985D4208C4F0BFED | Event | `public event System.EventHandler ResizeBegin` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DAF907E0F7FD69C9 | Event | `public event System.EventHandler ResizeEnd` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-696B9A901A57BF58 | Event | `public event System.EventHandler RightToLeftChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D0A0F6A34F266B96 | Event | `public event System.EventHandler RightToLeftLayoutChanged` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A4A9CD2D017CFC7F | Event | `public event System.Windows.Forms.ScrollEventHandler Scroll` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9D4C3059F039D761 | Event | `public event System.EventHandler Shown` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B4CFF3E28B48EE55 | Event | `public event System.EventHandler SizeChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-20D207E9CE054FAB | Event | `public event System.EventHandler StyleChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9DDD318285C7951C | Event | `public event System.EventHandler SystemColorsChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E9397894018C9B4 | Event | `public event System.EventHandler TabIndexChanged` | `System.Windows.Forms.Form` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6650C2F8C8ED078B | Event | `public event System.EventHandler TabStopChanged` | `System.Windows.Forms.Form` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3C8299FA213FBB68 | Event | `public event System.EventHandler TextChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C6BEA8E706263EA2 | Event | `public event System.EventHandler Validated` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3A5F9CE5567A745C | Event | `public event System.ComponentModel.CancelEventHandler Validating` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6965C5E68966EF0D | Event | `public event System.EventHandler VisibleChanged` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D4A17B59B542CDDB | Method | `public System.Void Activate()` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A0A1F209CE136984 | Method | `public System.Void AddOwnedForm(System.Windows.Forms.Form ownedForm)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E5D61145FD4BA154 | Method | `public System.IAsyncResult BeginInvoke(System.Action method)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E3A04ABAAC986769 | Method | `public System.IAsyncResult BeginInvoke(System.Delegate method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ED89CF3E189B2F0D | Method | `public System.IAsyncResult BeginInvoke(System.Delegate method, System.Object[] args)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-726B0EACBF76249C | Method | `public System.Void BringToFront()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CA183B24AE8CE07D | Method | `public System.Void Close()` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-40F60A0223F75EB7 | Method | `public System.Boolean Contains(System.Windows.Forms.Control ctl)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-24D2C0F1A8963BE0 | Method | `public System.Void CreateControl()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-06CD84BD4BD70C23 | Method | `public System.Drawing.Graphics CreateGraphics()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A2168B935B7045A0 | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E949954BF592742B | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-272F658DFDB87BE2 | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7571B4A70E57A74F | Method | `public System.Windows.Forms.DragDropEffects DoDragDropAsJson<!!0>(!!0 data, System.Windows.Forms.DragDropEffects allowedEffects) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B43270DF9BB5015 | Method | `public System.Windows.Forms.DragDropEffects DoDragDropAsJson<!!0>(!!0 data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-31882AF434C9C5EA | Method | `public System.Void DrawToBitmap(System.Drawing.Bitmap bitmap, System.Drawing.Rectangle targetBounds)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E79542338E228FEC | Method | `public System.Object EndInvoke(System.IAsyncResult asyncResult)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-745F62FAD7A3286A | Method | `public System.Windows.Forms.Form FindForm()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-27E8371835B9F0F2 | Method | `public System.Boolean Focus()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-66FBAF9158E2F0C7 | Method | `public static System.Windows.Forms.Control FromChildHandle(System.IntPtr handle)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A7DF3BEEC4E2B457 | Method | `public static System.Windows.Forms.Control FromHandle(System.IntPtr handle)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-12AA603152CDB170 | Method | `public static System.Drawing.SizeF GetAutoScaleSize(System.Drawing.Font font)` | `System.Windows.Forms.Form` ObsoleteAttribute("This method has been deprecated. Use the AutoScaleDimensions property instead.  https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F8EBC65C0BC21258 | Method | `public System.Windows.Forms.Control GetChildAtPoint(System.Drawing.Point pt)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29561EE5A191C8E8 | Method | `public System.Windows.Forms.Control GetChildAtPoint(System.Drawing.Point pt, System.Windows.Forms.GetChildAtPointSkip skipValue)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-40FF3D5BBE6A2D98 | Method | `public System.Windows.Forms.IContainerControl GetContainerControl()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B902BAE7401618F | Method | `public System.Windows.Forms.Control GetNextControl(System.Windows.Forms.Control ctl, System.Boolean forward)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0D0BC4444ED475FA | Method | `public virtual System.Drawing.Size GetPreferredSize(System.Drawing.Size proposedSize)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-905B6FA293DC4930 | Method | `public System.Void Hide()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-305213A8488358EC | Method | `public System.Void Invalidate()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-256F9BAFF41CD659 | Method | `public System.Void Invalidate(System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-54B9AD6E8077EE89 | Method | `public System.Void Invalidate(System.Drawing.Rectangle rc)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6CB7059A6C8DA53C | Method | `public System.Void Invalidate(System.Drawing.Rectangle rc, System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-ECD40198BC81365E | Method | `public System.Void Invalidate(System.Drawing.Region region)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-DCAB96066F5B2C9B | Method | `public System.Void Invalidate(System.Drawing.Region region, System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-349F25983DD56EC0 | Method | `public !!0 Invoke<!!0>(System.Func&#96;1<!!0> method) where !!0: None;` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1310291BD3588E40 | Method | `public System.Object Invoke(System.Delegate method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-322A785ED92DC447 | Method | `public System.Object Invoke(System.Delegate method, System.Object[] args)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-14D7A0A62BEDAA33 | Method | `public System.Void Invoke(System.Action method)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FEA1E298496E4A5B | Method | `public System.Threading.Tasks.Task InvokeAsync(System.Action callback, System.Threading.CancellationToken cancellationToken = null)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A8AFCAEC1D55EA4F | Method | `public System.Threading.Tasks.Task InvokeAsync(System.Func&#96;2<System.Threading.CancellationToken,System.Threading.Tasks.ValueTask> callback, System.Threading.CancellationToken cancellationToken = null)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-12F478DE7736983C | Method | `public System.Threading.Tasks.Task&#96;1<!!0> InvokeAsync<!!0>(System.Func&#96;1<!!0> callback, System.Threading.CancellationToken cancellationToken = null) where !!0: None;` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7637EC80A6484709 | Method | `public System.Threading.Tasks.Task&#96;1<!!0> InvokeAsync<!!0>(System.Func&#96;2<System.Threading.CancellationToken,System.Threading.Tasks.ValueTask&#96;1<!!0>> callback, System.Threading.CancellationToken cancellationToken = null) where !!0: None;` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-8BD2C3A908F0AB2B | Method | `public static System.Boolean IsKeyLocked(System.Windows.Forms.Keys keyVal)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-18800E25CEFB4E5F | Method | `public static System.Boolean IsMnemonic(System.Char charCode, System.String text)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-404FC80636872CE9 | Method | `public System.Void LayoutMdi(System.Windows.Forms.MdiLayout value)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F8C37430FFF53D27 | Method | `public System.Drawing.Size LogicalToDeviceUnits(System.Drawing.Size value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9FEF65EEA0C7A31C | Method | `public System.Int32 LogicalToDeviceUnits(System.Int32 value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AE4E1A4296A51AFF | Method | `public System.Void PerformAutoScale()` | `System.Windows.Forms.ContainerControl`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E6435DB3143573AB | Method | `public System.Void PerformLayout()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B666C8F67384E26D | Method | `public System.Void PerformLayout(System.Windows.Forms.Control affectedControl, System.String affectedProperty)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-45DC586A37E7EEF6 | Method | `public System.Drawing.Point PointToClient(System.Drawing.Point p)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-01DBE5589D6BDCF3 | Method | `public System.Drawing.Point PointToScreen(System.Drawing.Point p)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-DB02740986482ED8 | Method | `public System.Windows.Forms.PreProcessControlState PreProcessControlMessage(ref System.Windows.Forms.Message msg)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ACF310B5DD43980F | Method | `public virtual System.Boolean PreProcessMessage(ref System.Windows.Forms.Message msg)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AECB162B3606ECC1 | Method | `public System.Drawing.Rectangle RectangleToClient(System.Drawing.Rectangle r)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-853A2AA072437E2A | Method | `public System.Drawing.Rectangle RectangleToScreen(System.Drawing.Rectangle r)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D647AF23D2418FEC | Method | `public virtual System.Void Refresh()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1BC5EEF353412EBD | Method | `public System.Void RemoveOwnedForm(System.Windows.Forms.Form ownedForm)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-59696FACECADA019 | Method | `public virtual System.Void ResetBackColor()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B2EE080DBA20C41F | Method | `public System.Void ResetBindings()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-479860DFD0D01C6E | Method | `public virtual System.Void ResetCursor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-005AA2C866CB9542 | Method | `public virtual System.Void ResetFont()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-AE4FB12DC5DDBF3F | Method | `public virtual System.Void ResetForeColor()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4C7C4B128DCE4EC4 | Method | `public System.Void ResetImeMode()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A2A154E2D3B5C7DC | Method | `public virtual System.Void ResetRightToLeft()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FE50F16714B559D1 | Method | `public virtual System.Void ResetText()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F65CEE4F29EFFA9D | Method | `public System.Void ResumeLayout()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-07516B38B50660A4 | Method | `public System.Void ResumeLayout(System.Boolean performLayout)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-48D7BFC4EACDDAC8 | Method | `public System.Void Scale(System.Drawing.SizeF factor)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F71F7EA8D40EAC49 | Method | `public System.Void Scale(System.Single dx, System.Single dy)` | `System.Windows.Forms.Control` ObsoleteAttribute("This method has been deprecated. Use the Scale(SizeF ratio) method instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F39F9970A9DB3008 | Method | `public System.Void Scale(System.Single ratio)` | `System.Windows.Forms.Control` ObsoleteAttribute("This method has been deprecated. Use the Scale(SizeF ratio) method instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1DAC42829DE8DA50 | Method | `public System.Void ScaleBitmapLogicalToDevice(ref System.Drawing.Bitmap logicalBitmap)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-12CE580877E4B207 | Method | `public System.Void ScrollControlIntoView(System.Windows.Forms.Control activeControl)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B080760B1E018F78 | Method | `public System.Void Select()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A032265CE5647792 | Method | `public System.Boolean SelectNextControl(System.Windows.Forms.Control ctl, System.Boolean forward, System.Boolean tabStopOnly, System.Boolean nested, System.Boolean wrap)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2DED1584482E7540 | Method | `public System.Void SendToBack()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-51A161CB490CE348 | Method | `public System.Void SetAutoScrollMargin(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C96AC8828B24D617 | Method | `public System.Void SetBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1E439CA96AFBE61F | Method | `public System.Void SetBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5B1926752B4F3B26 | Method | `public System.Void SetDesktopBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-82B338D3CA9507C6 | Method | `public System.Void SetDesktopLocation(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D1D4BDD83C17EED8 | Method | `public System.Void Show()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5EA13A7C84482630 | Method | `public System.Void Show(System.Windows.Forms.IWin32Window owner)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1755771590364304 | Method | `public System.Threading.Tasks.Task ShowAsync(System.Windows.Forms.IWin32Window owner = null)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E4D632397AD6AF02 | Method | `public System.Windows.Forms.DialogResult ShowDialog()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BDF3E610CEF74305 | Method | `public System.Windows.Forms.DialogResult ShowDialog(System.Windows.Forms.IWin32Window owner)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D648ECCAFE8C80E8 | Method | `public System.Threading.Tasks.Task&#96;1<System.Windows.Forms.DialogResult> ShowDialogAsync()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3B4F319888B5F3B4 | Method | `public System.Threading.Tasks.Task&#96;1<System.Windows.Forms.DialogResult> ShowDialogAsync(System.Windows.Forms.IWin32Window owner)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-806B789E0A6441FD | Method | `public System.Void SuspendLayout()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-17D3456E6B3FDE66 | Method | `public virtual System.String ToString()` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0E8A18A503DCC806 | Method | `public System.Void Update()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-323EB1D1A1612E2C | Method | `public System.Boolean Validate()` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0730C2EC9A56C28C | Method | `public System.Boolean Validate(System.Boolean checkAutoValidate)` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-75CCDC593F28C4D7 | Method | `public virtual System.Boolean ValidateChildren()` | `System.Windows.Forms.Form` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3AB98130238A2A43 | Method | `public virtual System.Boolean ValidateChildren(System.Windows.Forms.ValidationConstraints validationConstraints)` | `System.Windows.Forms.Form` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2754219F9613C123 | Property | `System.Windows.Forms.IButtonControl AcceptButton { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-25157642B60EB114 | Property | `System.Windows.Forms.AccessibleObject AccessibilityObject { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D9E7E9B56CCEC3EF | Property | `System.String AccessibleDefaultActionDescription { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-49FD18E4B8B94B81 | Property | `System.String AccessibleDescription { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-18AE80DB623CBC24 | Property | `System.String AccessibleName { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-45F50F80129CFEC4 | Property | `System.Windows.Forms.AccessibleRole AccessibleRole { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(-1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-474E4EC6EC70C6C9 | Property | `System.Windows.Forms.Control ActiveControl { public get; public set; }` | `System.Windows.Forms.ContainerControl` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-AC703D5C321781FA | Property | `static System.Windows.Forms.Form ActiveForm { public get; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-44A72EBFCCFDD8A6 | Property | `System.Windows.Forms.Form ActiveMdiChild { public get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B38FFC54BA1F876 | Property | `virtual System.Boolean AllowDrop { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B3223B6475C63FCA | Property | `System.Boolean AllowTransparency { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-490F6E7FEBB372EA | Property | `virtual System.Windows.Forms.AnchorStyles Anchor { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true); DefaultValueAttribute(5) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-857573EBBE885756 | Property | `System.Boolean AutoScale { public get; public set; }` | `System.Windows.Forms.Form` ObsoleteAttribute("This property has been deprecated. Use the AutoScaleMode property instead.  https://go.microsoft.com/fwlink/?linkid=14202"); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0D00DF66580A89B2 | Property | `virtual System.Drawing.Size AutoScaleBaseSize { public get; public set; }` | `System.Windows.Forms.Form` LocalizableAttribute(true); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-80BB8B79EEC30106 | Property | `System.Drawing.SizeF AutoScaleDimensions { public get; public set; }` | `System.Windows.Forms.ContainerControl` LocalizableAttribute(true); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B47CF4974263CDEA | Property | `System.Windows.Forms.AutoScaleMode AutoScaleMode { public get; public set; }` | `System.Windows.Forms.ContainerControl` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C30562FB5233C892 | Property | `virtual System.Boolean AutoScroll { public get; public set; }` | `System.Windows.Forms.Form` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-31859D1294419BA9 | Property | `System.Drawing.Size AutoScrollMargin { public get; public set; }` | `System.Windows.Forms.ScrollableControl` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A079C0F416649520 | Property | `System.Drawing.Size AutoScrollMinSize { public get; public set; }` | `System.Windows.Forms.ScrollableControl` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8CDC4E24F7D7C9A9 | Property | `virtual System.Drawing.Point AutoScrollOffset { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DefaultValueAttribute(System.Drawing.Point,"0, 0") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8E938456B1796918 | Property | `System.Drawing.Point AutoScrollPosition { public get; public set; }` | `System.Windows.Forms.ScrollableControl` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D27352166382484 | Property | `virtual System.Boolean AutoSize { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(true); DesignerSerializationVisibilityAttribute(1) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-15DC56B00231A06D | Property | `System.Windows.Forms.AutoSizeMode AutoSizeMode { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(true); DefaultValueAttribute(1); LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-C4AA7532658D90A3 | Property | `virtual System.Windows.Forms.AutoValidate AutoValidate { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-20E4AEA05A1E8CA8 | Property | `virtual System.Drawing.Color BackColor { public get; public set; }` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7B44236EC984A939 | Property | `virtual System.Drawing.Image BackgroundImage { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7A36B8F1E87BC3BF | Property | `virtual System.Windows.Forms.ImageLayout BackgroundImageLayout { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(1); LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FC6690355CBF049C | Property | `virtual System.Windows.Forms.BindingContext BindingContext { public get; public set; }` | `System.Windows.Forms.ContainerControl` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-52CF9AF62B33AC9B | Property | `System.Int32 Bottom { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FFAF70229209A70B | Property | `System.Drawing.Rectangle Bounds { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B350CB18A4405EB0 | Property | `System.Windows.Forms.IButtonControl CancelButton { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E327CA84887C1F4E | Property | `System.Boolean CanFocus { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-059D77C874140327 | Property | `System.Boolean CanSelect { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-681EBBD0CF3E7BCD | Property | `System.Boolean Capture { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FE6362CE3DAD92F1 | Property | `System.Boolean CausesValidation { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F90A6E873364D009 | Property | `static System.Boolean CheckForIllegalCrossThreadCalls { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B1B1D68F34576D1 | Property | `System.Drawing.Rectangle ClientRectangle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E0B70EF9EFA54553 | Property | `System.Drawing.Size ClientSize { public get; public set; }` | `System.Windows.Forms.Form` LocalizableAttribute(true); DesignerSerializationVisibilityAttribute(1) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-23D7EDED86743CFA | Property | `System.String CompanyName { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D5A06466FA3ACF94 | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9AB33A184B5C7A22 | Property | `System.Boolean ContainsFocus { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4548870A8B5A06E0 | Property | `virtual System.Windows.Forms.ContextMenu ContextMenu { public get; public set; }` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E523B99E4FB1FF32 | Property | `virtual System.Windows.Forms.ContextMenuStrip ContextMenuStrip { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9F22D532C5553D30 | Property | `System.Boolean ControlBox { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-45F15FB98AF0460B | Property | `System.Windows.Forms.Control+ControlCollection Controls { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(2) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9229B790C91C35A1 | Property | `System.Boolean Created { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E153B8AE56700AF4 | Property | `System.Drawing.SizeF CurrentAutoScaleDimensions { public get; }` | `System.Windows.Forms.ContainerControl` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B03B452D3FF20A05 | Property | `virtual System.Windows.Forms.Cursor Cursor { public get; public set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2C37D36746B4E61B | Property | `System.Windows.Forms.ControlBindingsCollection DataBindings { public get; }` | `System.Windows.Forms.Control` DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-042887163051665D | Property | `virtual System.Object DataContext { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); BindableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E78259CE001E80B8 | Property | `static System.Drawing.Color DefaultBackColor { public get; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F8D0FA09DF8492D3 | Property | `static System.Drawing.Font DefaultFont { public get; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2B8AC3BE2054A9F6 | Property | `static System.Drawing.Color DefaultForeColor { public get; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-65E992CEA3C603B9 | Property | `System.Drawing.Rectangle DesktopBounds { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EA972E7B582CC2D7 | Property | `System.Drawing.Point DesktopLocation { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-97A95399FD472550 | Property | `System.Int32 DeviceDpi { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CC79E2D21CB8BC49 | Property | `System.Windows.Forms.DialogResult DialogResult { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FFDA7BD32504EC5E | Property | `virtual System.Drawing.Rectangle DisplayRectangle { public get; }` | `System.Windows.Forms.ScrollableControl`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-BBB6625E4C423E83 | Property | `System.Boolean Disposing { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F7F8C83C736C2570 | Property | `virtual System.Windows.Forms.DockStyle Dock { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true); DefaultValueAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E1953B758ED639C0 | Property | `System.Windows.Forms.ScrollableControl+DockPaddingEdges DockPadding { public get; }` | `System.Windows.Forms.ScrollableControl` DesignerSerializationVisibilityAttribute(0); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5405F63DB5E60E3A | Property | `System.Boolean Enabled { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A07A97AB0ED4BCAA | Property | `virtual System.Boolean Focused { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FD23EB61F3C30833 | Property | `virtual System.Drawing.Font Font { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-AD180FA24B0BD6C4 | Property | `virtual System.Drawing.Color ForeColor { public get; public set; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9F9BDF296BFB520D | Property | `System.Drawing.Color FormBorderColor { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E6E314240A7FC233 | Property | `System.Windows.Forms.FormBorderStyle FormBorderStyle { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(4) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-04B5A4C571A68B0E | Property | `System.Drawing.Color FormCaptionBackColor { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-71E3D3D0CCD9DE0A | Property | `System.Drawing.Color FormCaptionTextColor { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5E2F005A6716F4D2 | Property | `System.Windows.Forms.FormCornerPreference FormCornerPreference { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(0); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-84A64B90126F7B6B | Property | `System.Windows.Forms.ScreenCaptureMode FormScreenCaptureMode { public get; public set; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B35093758729C4E2 | Property | `System.IntPtr Handle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9B38EE0CB4526212 | Property | `System.Boolean HasChildren { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A31036905199F460 | Property | `System.Int32 Height { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2408BDC709C25BB6 | Property | `System.Boolean HelpButton { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4ED2A7875040E47E | Property | `System.Windows.Forms.HScrollProperties HorizontalScroll { public get; }` | `System.Windows.Forms.ScrollableControl` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-906A42A241B4B7B3 | Property | `System.Drawing.Icon Icon { public get; public set; }` | `System.Windows.Forms.Form` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-867C4352CFB92DA8 | Property | `System.Windows.Forms.ImeMode ImeMode { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B9899BCBA8A25151 | Property | `System.Boolean InvokeRequired { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8AA705BDD0EE3C8A | Property | `System.Boolean IsAccessible { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC1DA60BAF70BACC | Property | `System.Boolean IsAncestorSiteInDesignMode { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-26D08256505BD66A | Property | `System.Boolean IsDisposed { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-229A8BE90F4D68E8 | Property | `System.Boolean IsHandleCreated { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FB0459D7431B34E3 | Property | `System.Boolean IsMdiChild { public get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C60CA8577DC2B907 | Property | `System.Boolean IsMdiContainer { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3DD9B0075AD0F6E1 | Property | `System.Boolean IsMirrored { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D530B8912E96C15E | Property | `System.Boolean IsRestrictedWindow { public get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E19363EA5C7C1190 | Property | `System.Boolean KeyPreview { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-975DEE25E9AC5FB2 | Property | `virtual System.Windows.Forms.Layout.LayoutEngine LayoutEngine { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E3C926B17D963856 | Property | `System.Int32 Left { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5238B6202A03551F | Property | `System.Drawing.Point Location { public get; public set; }` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B3172109F55378BB | Property | `System.Windows.Forms.MenuStrip MainMenuStrip { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.ReferenceConverter) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6291817A54C4F8E3 | Property | `System.Windows.Forms.Padding Margin { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1BF6F4323908593B | Property | `System.Boolean MaximizeBox { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-38FF1E39C779A640 | Property | `virtual System.Drawing.Size MaximumSize { public get; public set; }` | `System.Windows.Forms.Form` LocalizableAttribute(true); DefaultValueAttribute(System.Drawing.Size,"0, 0") | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-EB5E7F3E4778A4F9 | Property | `System.Windows.Forms.Form[] MdiChildren { public get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ECC581592C330F56 | Property | `System.Boolean MdiChildrenMinimizedAnchorBottom { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2954DFA8B89E298F | Property | `System.Windows.Forms.Form MdiParent { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8E32F918F121C0CD | Property | `System.Windows.Forms.MainMenu Menu { public get; public set; }` | `System.Windows.Forms.Form` ObsoleteAttribute("\u0060MainMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060MenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8515347958161310 | Property | `System.Windows.Forms.MainMenu MergedMenu { public get; }` | `System.Windows.Forms.Form` ObsoleteAttribute("\u0060MainMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060MenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-48B48108CA952A59 | Property | `System.Boolean MinimizeBox { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-11A0647F3C6CEA54 | Property | `virtual System.Drawing.Size MinimumSize { public get; public set; }` | `System.Windows.Forms.Form` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-233E3E5373DDF070 | Property | `System.Boolean Modal { public get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B71A551B5EA3ABEF | Property | `static System.Windows.Forms.Keys ModifierKeys { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EF3D3DEBCE7C1415 | Property | `static System.Windows.Forms.MouseButtons MouseButtons { public get; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-26355FDAE95CBDA1 | Property | `static System.Drawing.Point MousePosition { public get; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9A6D40EC659DE073 | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-38C457D7175B719E | Property | `System.Double Opacity { public get; public set; }` | `System.Windows.Forms.Form` TypeConverterAttribute(System.Windows.Forms.OpacityConverter); DefaultValueAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3E8A7C306AE847E9 | Property | `System.Windows.Forms.Form[] OwnedForms { public get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AD71550999A03872 | Property | `System.Windows.Forms.Form Owner { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6CD085BE56E917CA | Property | `System.Windows.Forms.Padding Padding { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B64C87859602E5C8 | Property | `System.Windows.Forms.Control Parent { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D0375BFB647A9423 | Property | `System.Windows.Forms.Form ParentForm { public get; }` | `System.Windows.Forms.ContainerControl` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F1CFB4F8CCDA930C | Property | `System.Drawing.Size PreferredSize { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B1BB87A4ADD90463 | Property | `System.String ProductName { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-040C7820B498777F | Property | `System.String ProductVersion { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E3C00B70DD2196A1 | Property | `System.Boolean RecreatingHandle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-321995E5273186B8 | Property | `System.Drawing.Region Region { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5436280A256C01F6 | Property | `System.Drawing.Rectangle RestoreBounds { public get; }` | `System.Windows.Forms.Form` DesignerSerializationVisibilityAttribute(0); BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1F3394F5A0230ECE | Property | `System.Int32 Right { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A9BDF83D2744668F | Property | `virtual System.Windows.Forms.RightToLeft RightToLeft { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AE8D4785C4520C0B | Property | `virtual System.Boolean RightToLeftLayout { public get; public set; }` | `System.Windows.Forms.Form` LocalizableAttribute(true); DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-88BE55C8F48BF256 | Property | `System.Boolean ShowIcon { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3631525B2CA92423 | Property | `System.Boolean ShowInTaskbar { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-433B767DF4740978 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-099B0F91ACA95E48 | Property | `System.Drawing.Size Size { public get; public set; }` | `System.Windows.Forms.Form` DesignerSerializationVisibilityAttribute(0); LocalizableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B8D3581E4972B4ED | Property | `System.Windows.Forms.SizeGripStyle SizeGripStyle { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FBF7D414E9927D04 | Property | `System.Windows.Forms.FormStartPosition StartPosition { public get; public set; }` | `System.Windows.Forms.Form` LocalizableAttribute(true); DefaultValueAttribute(2) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3603BFF194267B67 | Property | `System.Int32 TabIndex { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-649B80C64214B9FE | Property | `System.Boolean TabStop { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(true); BrowsableAttribute(false) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CC8ED7C6B7E10102 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-DED0DF84AE018474 | Property | `virtual System.String Text { public get; public set; }` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3AF2BCE67626643B | Property | `System.Int32 Top { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-792C3FECF488FA21 | Property | `System.Boolean TopLevel { public get; public set; }` | `System.Windows.Forms.Form` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A0D5B691C457959B | Property | `System.Windows.Forms.Control TopLevelControl { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6FA992292FA7F766 | Property | `System.Boolean TopMost { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-43A6F168B47F55B4 | Property | `System.Drawing.Color TransparencyKey { public get; public set; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7B4E05A7A3D2F822 | Property | `System.Boolean UseWaitCursor { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(false); BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5FEE6708B54A0AFA | Property | `System.Windows.Forms.VScrollProperties VerticalScroll { public get; }` | `System.Windows.Forms.ScrollableControl` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-99EAE5DDDA84B80F | Property | `System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FAEC011B24759879 | Property | `System.Int32 Width { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-25AAB3ED8A539D57 | Property | `System.Windows.Forms.FormWindowState WindowState { public get; public set; }` | `System.Windows.Forms.Form` DefaultValueAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A993D15136076B92 | Property | `System.Windows.Forms.IWindowTarget WindowTarget { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-8AF3EC8FA9DAD1F0 | Field | `protected const System.Int32 ScrollStateAutoScrolling = 1` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-916F41FC45D5D23D | Field | `protected const System.Int32 ScrollStateFullDrag = 16` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4C773DB8FD8A569A | Field | `protected const System.Int32 ScrollStateHScrollVisible = 2` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6E2FFB47D4BB546D | Field | `protected const System.Int32 ScrollStateUserHasScrolled = 8` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-98F7AE71AA023E86 | Field | `protected const System.Int32 ScrollStateVScrollVisible = 4` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E71467B2C7D28080 | Method | `protected System.Void AccessibilityNotifyClients(System.Windows.Forms.AccessibleEvents accEvent, System.Int32 objectID, System.Int32 childID)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2F2478EF8545CFDE | Method | `protected internal System.Void AccessibilityNotifyClients(System.Windows.Forms.AccessibleEvents accEvent, System.Int32 childID)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C7559431CE821DF0 | Method | `protected System.Void ActivateMdiChild(System.Windows.Forms.Form form)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FBBF8B962E75A6E0 | Method | `protected virtual System.Void AdjustFormScrollbars(System.Boolean displayScrollbars)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1A18658F5E864B06 | Method | `protected System.Void ApplyAutoScaling()` | `System.Windows.Forms.Form` ObsoleteAttribute("This method has been deprecated. Use the ApplyAutoScaling method instead.  https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-02961642A2ACE51D | Method | `protected System.Void CenterToParent()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9DC6CDBE96D06B6C | Method | `protected System.Void CenterToScreen()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8A767A6EC76F9FDC | Method | `protected virtual System.Windows.Forms.AccessibleObject CreateAccessibilityInstance()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C70CB3A9C8BA0D0F | Method | `protected virtual System.Windows.Forms.Control+ControlCollection CreateControlsInstance()` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B9EB3CCE3A538607 | Method | `protected virtual System.Void CreateHandle()` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6143F59F70986F21 | Method | `protected virtual System.Void DefWndProc(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-43759712FDDDC55F | Method | `protected virtual System.Void DestroyHandle()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1AB7ECE17C970477 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-AF799B36114E903E | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4CE52090E0D6434B | Method | `protected virtual System.Windows.Forms.AccessibleObject GetAccessibilityObjectById(System.Int32 objectId)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-60A759661965C230 | Method | `protected System.Windows.Forms.AutoSizeMode GetAutoSizeMode()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5707ED7FBA846328 | Method | `protected virtual System.Drawing.Rectangle GetScaledBounds(System.Drawing.Rectangle bounds, System.Drawing.SizeF factor, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-689153A90D7BAD42 | Method | `protected System.Boolean GetScrollState(System.Int32 bit)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7F9E5A7B9382D827 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FF75F12955A3AEA4 | Method | `protected System.Boolean GetStyle(System.Windows.Forms.ControlStyles flag)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E1359FF8089EC1AE | Method | `protected System.Boolean GetTopLevel()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F9771C20971D7498 | Method | `protected virtual System.Void InitLayout()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9C04FC8F3CCEF742 | Method | `protected System.Void InvokeGotFocus(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4C58CB1E8A5730A3 | Method | `protected System.Void InvokeLostFocus(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ABACF4BBEF1D47F6 | Method | `protected System.Void InvokeOnClick(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6E3A39B7C6C206D1 | Method | `protected System.Void InvokePaint(System.Windows.Forms.Control c, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-55F7890C71C7B615 | Method | `protected System.Void InvokePaintBackground(System.Windows.Forms.Control c, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2C7BD0042B832EEA | Method | `protected virtual System.Boolean IsInputChar(System.Char charCode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B423B9FB17DE2D2 | Method | `protected virtual System.Boolean IsInputKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-98736272688F4B28 | Method | `protected virtual System.Void NotifyInvalidate(System.Drawing.Rectangle invalidatedArea)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-54F8B89B762C66EA | Method | `protected virtual System.Void OnActivated(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-71C2BF5024E5B8B7 | Method | `protected virtual System.Void OnAutoSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-AD887927B3C99436 | Method | `protected virtual System.Void OnAutoValidateChanged(System.EventArgs e)` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-010729D0B6E9A91F | Method | `protected virtual System.Void OnBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B2BDC2FED930EA4A | Method | `protected virtual System.Void OnBackgroundImageChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-775475DF853BD588 | Method | `protected virtual System.Void OnBackgroundImageLayoutChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-89F3C093731275EE | Method | `protected virtual System.Void OnBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-68B6CA7C56FE9304 | Method | `protected virtual System.Void OnCausesValidationChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-02FC1C4B4AF04333 | Method | `protected virtual System.Void OnChangeUICues(System.Windows.Forms.UICuesEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-836D19C586E0B697 | Method | `protected virtual System.Void OnClick(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F786CEE285AD6A0A | Method | `protected virtual System.Void OnClientSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FB5AE647489F0394 | Method | `protected virtual System.Void OnClosed(System.EventArgs e)` | `System.Windows.Forms.Form` ObsoleteAttribute("Form.OnClosing, Form.OnClosed and the corresponding events are obsolete. Use Form.OnFormClosing, Form.OnFormClosed, Form.FormClosing and Form.FormClosed instead.",false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-70F900DCB76D111C | Method | `protected virtual System.Void OnClosing(System.ComponentModel.CancelEventArgs e)` | `System.Windows.Forms.Form` ObsoleteAttribute("Form.OnClosing, Form.OnClosed and the corresponding events are obsolete. Use Form.OnFormClosing, Form.OnFormClosed, Form.FormClosing and Form.FormClosed instead.",false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BEF232E4EF2242B0 | Method | `protected virtual System.Void OnContextMenuChanged(System.EventArgs e)` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-614E0812D54F4B6D | Method | `protected virtual System.Void OnContextMenuStripChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ACDD658F2E964560 | Method | `protected virtual System.Void OnControlAdded(System.Windows.Forms.ControlEventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-EF31E9D405E2E8F8 | Method | `protected virtual System.Void OnControlRemoved(System.Windows.Forms.ControlEventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-8CDBBCB48496761B | Method | `protected virtual System.Void OnCreateControl()` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-990858CC934F93CA | Method | `protected virtual System.Void OnCursorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-25DAFC0C9D018A86 | Method | `protected virtual System.Void OnDataContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6D155E6372270B95 | Method | `protected virtual System.Void OnDeactivate(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9F5D953C20991B4F | Method | `protected virtual System.Void OnDockChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-131395B348A6C7A0 | Method | `protected virtual System.Void OnDoubleClick(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CEDCA27B069E2C91 | Method | `protected virtual System.Void OnDpiChanged(System.Windows.Forms.DpiChangedEventArgs e)` | `System.Windows.Forms.Form` BrowsableAttribute(true); DesignerSerializationVisibilityAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B45F3C5279294201 | Method | `protected virtual System.Void OnDpiChangedAfterParent(System.EventArgs e)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B49F81AAAB043339 | Method | `protected virtual System.Void OnDpiChangedBeforeParent(System.EventArgs e)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-581BFDB02856D6A7 | Method | `protected virtual System.Void OnDragDrop(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-612AE0CDC2A225C2 | Method | `protected virtual System.Void OnDragEnter(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-79C127A5DDD9DCBD | Method | `protected virtual System.Void OnDragLeave(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A2E10800063851ED | Method | `protected virtual System.Void OnDragOver(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D5C7FC6741B194A2 | Method | `protected virtual System.Void OnEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D3BA4562A5059D51 | Method | `protected internal virtual System.Void OnEnter(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DA45C4C7307A60BD | Method | `protected virtual System.Void OnFontChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0964635F7A8BB394 | Method | `protected virtual System.Void OnForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-94C9070BD0E75441 | Method | `protected virtual System.Void OnFormBorderColorChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-577C931AF8A22D2A | Method | `protected virtual System.Void OnFormCaptionBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-398C0D39067BD24B | Method | `protected virtual System.Void OnFormCaptionTextColorChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3B969C85AB73471A | Method | `protected virtual System.Void OnFormClosed(System.Windows.Forms.FormClosedEventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6CA51EF768BA715D | Method | `protected virtual System.Void OnFormClosing(System.Windows.Forms.FormClosingEventArgs e)` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-58B2D380891BEAC9 | Method | `protected virtual System.Void OnFormCornerPreferenceChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-204F8487F2E732BD | Method | `protected virtual System.Boolean OnGetDpiScaledSize(System.Int32 deviceDpiOld, System.Int32 deviceDpiNew, ref System.Drawing.Size desiredSize)` | `System.Windows.Forms.Form` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1CC6D32F523F2A05 | Method | `protected virtual System.Void OnGiveFeedback(System.Windows.Forms.GiveFeedbackEventArgs gfbevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D9B95CCB46F411D8 | Method | `protected virtual System.Void OnGotFocus(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-983B6FCD3F7C2FEC | Method | `protected virtual System.Void OnHandleCreated(System.EventArgs e)` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B85C5982FA607D93 | Method | `protected virtual System.Void OnHandleDestroyed(System.EventArgs e)` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F699B628E55F9643 | Method | `protected virtual System.Void OnHelpButtonClicked(System.ComponentModel.CancelEventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B4A4FC87B932E50D | Method | `protected virtual System.Void OnHelpRequested(System.Windows.Forms.HelpEventArgs hevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-00E42E21D34A0EF5 | Method | `protected virtual System.Void OnImeModeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-42CDE4A0A18B1196 | Method | `protected virtual System.Void OnInputLanguageChanged(System.Windows.Forms.InputLanguageChangedEventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B9571A161C984FCE | Method | `protected virtual System.Void OnInputLanguageChanging(System.Windows.Forms.InputLanguageChangingEventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DB2E9C88CC8EA1C9 | Method | `protected virtual System.Void OnInvalidated(System.Windows.Forms.InvalidateEventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A32020A701B7B55B | Method | `protected virtual System.Void OnKeyDown(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-322C5F28934565E1 | Method | `protected virtual System.Void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-938E78C55F969301 | Method | `protected virtual System.Void OnKeyUp(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DE9997656B37FEB4 | Method | `protected virtual System.Void OnLayout(System.Windows.Forms.LayoutEventArgs levent)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-419A3B15F3AF7E67 | Method | `protected internal virtual System.Void OnLeave(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F30B3DC7C30AC607 | Method | `protected virtual System.Void OnLoad(System.EventArgs e)` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D38414A2985DF930 | Method | `protected virtual System.Void OnLocationChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-268A2EB50BCF5C7A | Method | `protected virtual System.Void OnLostFocus(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5748BC8D51225463 | Method | `protected virtual System.Void OnMarginChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-E72FB4DD0EF62204 | Method | `protected virtual System.Void OnMaximizedBoundsChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AB67BFCF528A8750 | Method | `protected virtual System.Void OnMaximumSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-093D6DD8B51A80AA | Method | `protected virtual System.Void OnMdiChildActivate(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E5E576A6D6E065B1 | Method | `protected virtual System.Void OnMenuComplete(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2FD39F5C69B78270 | Method | `protected virtual System.Void OnMenuStart(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AFE610BC761AD47F | Method | `protected virtual System.Void OnMinimumSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3AAD64D4B85ED22C | Method | `protected virtual System.Void OnMouseCaptureChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-0C0C55C63FDCB72B | Method | `protected virtual System.Void OnMouseClick(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F61E779998F9705C | Method | `protected virtual System.Void OnMouseDoubleClick(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-584955CD69229B14 | Method | `protected virtual System.Void OnMouseDown(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-BF6541E4AC033DD0 | Method | `protected virtual System.Void OnMouseEnter(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-99D7B74ACE61F227 | Method | `protected virtual System.Void OnMouseHover(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5AD7B856FEB938E5 | Method | `protected virtual System.Void OnMouseLeave(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-FC465A0268BD9BB6 | Method | `protected virtual System.Void OnMouseMove(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B3FC00E01750C487 | Method | `protected virtual System.Void OnMouseUp(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5C9858B6117B804E | Method | `protected virtual System.Void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.ScrollableControl`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-52EDDBA265EC72B6 | Method | `protected virtual System.Void OnMove(System.EventArgs e)` | `System.Windows.Forms.ContainerControl`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-27B12231675B0BC7 | Method | `protected virtual System.Void OnNotifyMessage(System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C4A18F4791DF9D7D | Method | `protected virtual System.Void OnPaddingChanged(System.EventArgs e)` | `System.Windows.Forms.ScrollableControl`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5BB4835A41835142 | Method | `protected virtual System.Void OnPaint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6FDFDC2A83421E44 | Method | `protected virtual System.Void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0AEAA5EC6ED6263F | Method | `protected virtual System.Void OnParentBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-064F61BD84C425B4 | Method | `protected virtual System.Void OnParentBackgroundImageChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-AF354D9E971F4CA7 | Method | `protected virtual System.Void OnParentBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C0041CA85FC8A116 | Method | `protected virtual System.Void OnParentChanged(System.EventArgs e)` | `System.Windows.Forms.ContainerControl`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-F2A734A0C82840BC | Method | `protected virtual System.Void OnParentCursorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F95FABF03663870D | Method | `protected virtual System.Void OnParentDataContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EA8C8E44D864F7B0 | Method | `protected virtual System.Void OnParentEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B5B16912BF2C7463 | Method | `protected virtual System.Void OnParentFontChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-4584DF78BAB074F1 | Method | `protected virtual System.Void OnParentForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3F251FBF17569026 | Method | `protected virtual System.Void OnParentRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D47B1EAEECA1FA0 | Method | `protected virtual System.Void OnParentVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-71DA43082925A41E | Method | `protected virtual System.Void OnPreviewKeyDown(System.Windows.Forms.PreviewKeyDownEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-54B0673267D33CC9 | Method | `protected virtual System.Void OnPrint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-95F3149C4D628274 | Method | `protected virtual System.Void OnQueryContinueDrag(System.Windows.Forms.QueryContinueDragEventArgs qcdevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F1F0EA11113518F5 | Method | `protected virtual System.Void OnRegionChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-33AC9BE0E75D97A5 | Method | `protected virtual System.Void OnResize(System.EventArgs e)` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-04E1B27406D6F2DF | Method | `protected virtual System.Void OnResizeBegin(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C0048CB234526F3F | Method | `protected virtual System.Void OnResizeEnd(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-637EF9EF8F11EF8B | Method | `protected virtual System.Void OnRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AF4BF4C35C7791AF | Method | `protected virtual System.Void OnRightToLeftLayoutChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-10DB62DE7C83AE44 | Method | `protected virtual System.Void OnScroll(System.Windows.Forms.ScrollEventArgs se)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-397D48AE2F57D63B | Method | `protected virtual System.Void OnShown(System.EventArgs e)` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3B671EB29A01D0A4 | Method | `protected virtual System.Void OnSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6718AF2AC0480A8F | Method | `protected virtual System.Void OnStyleChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-7AB155BCF9B0838E | Method | `protected virtual System.Void OnSystemColorsChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F0FE4ED3BA36AAD8 | Method | `protected virtual System.Void OnTabIndexChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C17C859033D1B94 | Method | `protected virtual System.Void OnTabStopChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FAA82C7B2F3FAFE9 | Method | `protected virtual System.Void OnTextChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-AF5AD9E842789018 | Method | `protected virtual System.Void OnValidated(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AE3E710AD8B12352 | Method | `protected virtual System.Void OnValidating(System.ComponentModel.CancelEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-81EFBBB84A9930A0 | Method | `protected virtual System.Void OnVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1D0AA412E2A75997 | Method | `protected virtual System.Boolean ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-83D7846ADF72AEDC | Method | `protected virtual System.Boolean ProcessDialogChar(System.Char charCode)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E1D1585829BC8DDB | Method | `protected virtual System.Boolean ProcessDialogKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-360A26E466EE8116 | Method | `protected virtual System.Boolean ProcessKeyEventArgs(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CC0A733DAE6F4F54 | Method | `protected internal virtual System.Boolean ProcessKeyMessage(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A56511C78F99B968 | Method | `protected virtual System.Boolean ProcessKeyPreview(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5A41A5CF7759C88D | Method | `protected internal virtual System.Boolean ProcessMnemonic(System.Char charCode)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B0B59881DF5F8A62 | Method | `protected virtual System.Boolean ProcessTabKey(System.Boolean forward)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A935FEC5C4385405 | Method | `protected System.Void RaiseDragEvent(System.Object key, System.Windows.Forms.DragEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1AF0C5CCA53779B2 | Method | `protected System.Void RaiseKeyEvent(System.Object key, System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-94CB916E2DAE591F | Method | `protected System.Void RaiseMouseEvent(System.Object key, System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-B583637020AB199D | Method | `protected System.Void RaisePaintEvent(System.Object key, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-95FC58561AB0221C | Method | `protected System.Void RecreateHandle()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CD719648D8FA2B8E | Method | `protected static System.Boolean ReflectMessage(System.IntPtr hWnd, ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8EA56D509A9E6117 | Method | `protected virtual System.Void RescaleConstantsForDpi(System.Int32 deviceDpiOld, System.Int32 deviceDpiNew)` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-71AD389341D15EC1 | Method | `protected System.Void ResetMouseEventArgs()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-369DE606E0C12B69 | Method | `protected System.Drawing.ContentAlignment RtlTranslateAlignment(System.Drawing.ContentAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EDCA89FB9A08842D | Method | `protected System.Windows.Forms.HorizontalAlignment RtlTranslateAlignment(System.Windows.Forms.HorizontalAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C99212FE7240F634 | Method | `protected System.Windows.Forms.LeftRightAlignment RtlTranslateAlignment(System.Windows.Forms.LeftRightAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8925601D710C276B | Method | `protected internal System.Drawing.ContentAlignment RtlTranslateContent(System.Drawing.ContentAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E52661042890B57D | Method | `protected System.Windows.Forms.HorizontalAlignment RtlTranslateHorizontal(System.Windows.Forms.HorizontalAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FDAD7A23F9058302 | Method | `protected System.Windows.Forms.LeftRightAlignment RtlTranslateLeftRight(System.Windows.Forms.LeftRightAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3FE9E1AC557C67A3 | Method | `protected virtual System.Void ScaleControl(System.Drawing.SizeF factor, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-659985723B0BAAE0 | Method | `protected virtual System.Void ScaleCore(System.Single x, System.Single y)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9D51900ABA29A71B | Method | `protected virtual System.Void ScaleMinMaxSize(System.Single xScaleFactor, System.Single yScaleFactor, System.Boolean updateContainerSize = true)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-62D4F795F26983DA | Method | `protected virtual System.Drawing.Point ScrollToControl(System.Windows.Forms.Control activeControl)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5B816134CAAE50CD | Method | `protected virtual System.Void Select(System.Boolean directed, System.Boolean forward)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DAB46DFBBF8F81D9 | Method | `protected System.Void SetAutoSizeMode(System.Windows.Forms.AutoSizeMode mode)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-6BB1F43622FF1A50 | Method | `protected virtual System.Void SetBoundsCore(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2A0686A4D6EA80BC | Method | `protected virtual System.Void SetClientSizeCore(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-1D3FBE33D4F8C860 | Method | `protected System.Void SetDisplayRectLocation(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3208424529475403 | Method | `protected System.Void SetScrollState(System.Int32 bit, System.Boolean value)` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E789013FFD916804 | Method | `protected System.Void SetStyle(System.Windows.Forms.ControlStyles flag, System.Boolean value)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-2363F4C3C306B7B1 | Method | `protected System.Void SetTopLevel(System.Boolean value)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-D8CE9BF9CC59C597 | Method | `protected virtual System.Void SetVisibleCore(System.Boolean value)` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3276C52AFED21D94 | Method | `protected virtual System.Drawing.Size SizeFromClientSize(System.Drawing.Size clientSize)` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-55DD0FFCD7B95D21 | Method | `protected System.Void UpdateBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3BF81C1303A11545 | Method | `protected System.Void UpdateBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Int32 clientWidth, System.Int32 clientHeight)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ADA1E16A360D1B57 | Method | `protected internal System.Void UpdateBounds()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-34931B18CED7549C | Method | `protected virtual System.Void UpdateDefaultButton()` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F6F43A727F48742 | Method | `protected System.Void UpdateStyles()` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5EE6E606317AF39F | Method | `protected System.Void UpdateZOrder()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-39F879588BD9DDA2 | Method | `protected virtual System.Void WndProc(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D5A5B378AF66BD15 | Property | `System.Drawing.SizeF AutoScaleFactor { protected get; }` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A8DB13C64D9608B4 | Property | `virtual System.Boolean CanEnableIme { protected get; }` | `System.Windows.Forms.ContainerControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B8682CEAC6F247EB | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-43101EA34923AE02 | Property | `virtual System.Windows.Forms.CreateParams CreateParams { protected get; }` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-44508246D1C363BB | Property | `virtual System.Windows.Forms.Cursor DefaultCursor { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B2716C2B245154A1 | Property | `virtual System.Windows.Forms.ImeMode DefaultImeMode { protected get; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-03063D4DC5206B17 | Property | `virtual System.Windows.Forms.Padding DefaultMargin { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-073C1EA64292DBE1 | Property | `virtual System.Drawing.Size DefaultMaximumSize { protected get; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-854AB2C685A3A2FC | Property | `virtual System.Drawing.Size DefaultMinimumSize { protected get; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-A7C153E7BE661F4D | Property | `virtual System.Windows.Forms.Padding DefaultPadding { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D8566BB7859D9A39 | Property | `virtual System.Drawing.Size DefaultSize { protected get; }` | `System.Windows.Forms.Form`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-3F508C98AF42308A | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-97BF040258140356 | Property | `virtual System.Boolean DoubleBuffered { protected get; protected set; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-CFB055EC3DEC4C0F | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-DA0424C2B3479750 | Property | `System.Int32 FontHeight { protected get; protected set; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-9FAAD2BBF9ED4378 | Property | `System.Boolean HScroll { protected get; protected set; }` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E4997453AF65F963 | Property | `virtual System.Windows.Forms.ImeMode ImeModeBase { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F8449EDD2B293F1A | Property | `System.Drawing.Rectangle MaximizedBounds { protected get; protected set; }` | `System.Windows.Forms.Form`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AAD74830B8EE6712 | Property | `static System.Windows.Forms.ImeMode PropagatingImeMode { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F9882D3B4F3A42D5 | Property | `System.Boolean RenderRightToLeft { protected internal get; }` | `System.Windows.Forms.Control` ObsoleteAttribute("This property has been deprecated. Please use RightToLeft instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F98B547CD0E48B78 | Property | `System.Boolean ResizeRedraw { protected get; protected set; }` | `System.Windows.Forms.Control`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-00DA57D114558556 | Property | `virtual System.Boolean ScaleChildren { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-412C5A57CF2E724E | Property | `virtual System.Boolean ShowFocusCues { protected internal get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-672497F25D9A497F | Property | `virtual System.Boolean ShowKeyboardCues { protected internal get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4C0B30EA4AD72C26 | Property | `virtual System.Boolean ShowWithoutActivation { protected get; }` | `System.Windows.Forms.Form` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AD7BF922D7689A93 | Property | `System.Boolean VScroll { protected get; protected set; }` | `System.Windows.Forms.ScrollableControl`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
