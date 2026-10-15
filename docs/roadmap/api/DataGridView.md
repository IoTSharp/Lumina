# DataGridView：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/DataGridView.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.DataGridView`；基类：`System.Windows.Forms.Control`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **157**，公开事件 **190**，公开方法 **116**；受保护扩展成员 **324**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-2FFBDD547C168448 | Constructor | `public DataGridView()` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0AEFD2D897A7619C | Event | `public event System.EventHandler AllowUserToAddRowsChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3182C21CEE2FE8C4 | Event | `public event System.EventHandler AllowUserToDeleteRowsChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-794DC1E71E24447B | Event | `public event System.EventHandler AllowUserToOrderColumnsChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6850F0FAA5BE767D | Event | `public event System.EventHandler AllowUserToResizeColumnsChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C09D13CEE1805236 | Event | `public event System.EventHandler AllowUserToResizeRowsChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-534B2E65FB0079FE | Event | `public event System.EventHandler AlternatingRowsDefaultCellStyleChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3F351BD396545A39 | Event | `public event System.EventHandler AutoGenerateColumnsChanged` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E14F1FF640AB883C | Event | `public event System.EventHandler AutoSizeChanged` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E1E8591DA504BE77 | Event | `public event System.Windows.Forms.DataGridViewAutoSizeColumnModeEventHandler AutoSizeColumnModeChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4599E11E9EAA99FB | Event | `public event System.Windows.Forms.DataGridViewAutoSizeColumnsModeEventHandler AutoSizeColumnsModeChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5283B50C247AE134 | Event | `public event System.Windows.Forms.DataGridViewAutoSizeModeEventHandler AutoSizeRowsModeChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-033FE90351BAF47F | Event | `public event System.EventHandler BackColorChanged` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E77C54FC1E4117EB | Event | `public event System.EventHandler BackgroundColorChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2EABCF98FB649C72 | Event | `public event System.EventHandler BackgroundImageChanged` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-09A02762B3B9835E | Event | `public event System.EventHandler BackgroundImageLayoutChanged` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B3E38C4C2EC72E82 | Event | `public event System.EventHandler BindingContextChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-17DEC40887B9F25D | Event | `public event System.EventHandler BorderStyleChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E1C818B831D16D75 | Event | `public event System.Windows.Forms.QuestionEventHandler CancelRowEdit` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2C847CE814A912F5 | Event | `public event System.EventHandler CausesValidationChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A15A3204DC8D553B | Event | `public event System.Windows.Forms.DataGridViewCellCancelEventHandler CellBeginEdit` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-725971E1F6387F5F | Event | `public event System.EventHandler CellBorderStyleChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3F9A9B616C2AFDE0 | Event | `public event System.Windows.Forms.DataGridViewCellEventHandler CellClick` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F11ADCD680187A15 | Event | `public event System.Windows.Forms.DataGridViewCellEventHandler CellContentClick` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F6FB3C9D9E447764 | Event | `public event System.Windows.Forms.DataGridViewCellEventHandler CellContentDoubleClick` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-188E2B1A40C976E3 | Event | `public event System.Windows.Forms.DataGridViewCellEventHandler CellContextMenuStripChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EFDB155F7A88864B | Event | `public event System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler CellContextMenuStripNeeded` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8ACD3F7EE1790CAD | Event | `public event System.Windows.Forms.DataGridViewCellEventHandler CellDoubleClick` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E08904FFB4A4E5A3 | Event | `public event System.Windows.Forms.DataGridViewCellEventHandler CellEndEdit` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EDB6957F13F76020 | Event | `public event System.Windows.Forms.DataGridViewCellEventHandler CellEnter` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-08F0252CA3F9C9CB | Event | `public event System.Windows.Forms.DataGridViewCellEventHandler CellErrorTextChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B2977C1DDC3A8D4D | Event | `public event System.Windows.Forms.DataGridViewCellErrorTextNeededEventHandler CellErrorTextNeeded` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D0AD73615F9A8157 | Event | `public event System.Windows.Forms.DataGridViewCellFormattingEventHandler CellFormatting` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D9EF6548AF0DB9EA | Event | `public event System.Windows.Forms.DataGridViewCellEventHandler CellLeave` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EBE4B93D5F7C02C0 | Event | `public event System.Windows.Forms.DataGridViewCellMouseEventHandler CellMouseClick` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AEB4D7E8D77A80BB | Event | `public event System.Windows.Forms.DataGridViewCellMouseEventHandler CellMouseDoubleClick` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CDFC5FB5AEB3FD9C | Event | `public event System.Windows.Forms.DataGridViewCellMouseEventHandler CellMouseDown` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AAA2BBC2EF865FA4 | Event | `public event System.Windows.Forms.DataGridViewCellEventHandler CellMouseEnter` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-28E433EFD21EE478 | Event | `public event System.Windows.Forms.DataGridViewCellEventHandler CellMouseLeave` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-25EE7F514A02FEC1 | Event | `public event System.Windows.Forms.DataGridViewCellMouseEventHandler CellMouseMove` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7A66C635D6070D26 | Event | `public event System.Windows.Forms.DataGridViewCellMouseEventHandler CellMouseUp` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AF681A5E80E64D26 | Event | `public event System.Windows.Forms.DataGridViewCellPaintingEventHandler CellPainting` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8169656A6FE852F0 | Event | `public event System.Windows.Forms.DataGridViewCellParsingEventHandler CellParsing` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D3DB927B6D3996A | Event | `public event System.Windows.Forms.DataGridViewCellStateChangedEventHandler CellStateChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC73F3F5BD5CBAD4 | Event | `public event System.Windows.Forms.DataGridViewCellEventHandler CellStyleChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0F908F6E67DC10C0 | Event | `public event System.Windows.Forms.DataGridViewCellStyleContentChangedEventHandler CellStyleContentChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1EE9490B8482A06B | Event | `public event System.Windows.Forms.DataGridViewCellEventHandler CellToolTipTextChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BA1A94B812197A4E | Event | `public event System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler CellToolTipTextNeeded` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CFDFE5EEEA349108 | Event | `public event System.Windows.Forms.DataGridViewCellEventHandler CellValidated` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-03F4606017D50738 | Event | `public event System.Windows.Forms.DataGridViewCellValidatingEventHandler CellValidating` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-28F097B865A84D60 | Event | `public event System.Windows.Forms.DataGridViewCellEventHandler CellValueChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CD71EC3B80A22526 | Event | `public event System.Windows.Forms.DataGridViewCellValueEventHandler CellValueNeeded` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F2149B2A5D565FDD | Event | `public event System.Windows.Forms.DataGridViewCellValueEventHandler CellValuePushed` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-41B3CECF47BC61E6 | Event | `public event System.Windows.Forms.UICuesEventHandler ChangeUICues` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8394B9B342DEE8A5 | Event | `public event System.EventHandler Click` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6C0F3E9587B3316F | Event | `public event System.EventHandler ClientSizeChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A11E3FE69A56ECB1 | Event | `public event System.Windows.Forms.DataGridViewColumnEventHandler ColumnAdded` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-592F6F544300B83E | Event | `public event System.Windows.Forms.DataGridViewColumnEventHandler ColumnContextMenuStripChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E99447193B24D22 | Event | `public event System.Windows.Forms.DataGridViewColumnEventHandler ColumnDataPropertyNameChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E954696CDFD50BCA | Event | `public event System.Windows.Forms.DataGridViewColumnEventHandler ColumnDefaultCellStyleChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D6760678371D7808 | Event | `public event System.Windows.Forms.DataGridViewColumnEventHandler ColumnDisplayIndexChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0800C57E5B7DF678 | Event | `public event System.Windows.Forms.DataGridViewColumnDividerDoubleClickEventHandler ColumnDividerDoubleClick` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6E96FCD8F07A4820 | Event | `public event System.Windows.Forms.DataGridViewColumnEventHandler ColumnDividerWidthChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A01692F2B336C593 | Event | `public event System.Windows.Forms.DataGridViewColumnEventHandler ColumnHeaderCellChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-949F2D80D642B653 | Event | `public event System.Windows.Forms.DataGridViewCellMouseEventHandler ColumnHeaderMouseClick` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-15E11911B3E5FB2B | Event | `public event System.Windows.Forms.DataGridViewCellMouseEventHandler ColumnHeaderMouseDoubleClick` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D0E27F219CC42CEB | Event | `public event System.EventHandler ColumnHeadersBorderStyleChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E720B285CCDC7C75 | Event | `public event System.EventHandler ColumnHeadersDefaultCellStyleChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8D0089385219165A | Event | `public event System.EventHandler ColumnHeadersHeightChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-33A6496084511A15 | Event | `public event System.Windows.Forms.DataGridViewAutoSizeModeEventHandler ColumnHeadersHeightSizeModeChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1C3B97644C164597 | Event | `public event System.Windows.Forms.DataGridViewColumnEventHandler ColumnMinimumWidthChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BE60618857E80649 | Event | `public event System.Windows.Forms.DataGridViewColumnEventHandler ColumnNameChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CDCEDCE5BE2779B5 | Event | `public event System.Windows.Forms.DataGridViewColumnEventHandler ColumnRemoved` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8A37334A25D94404 | Event | `public event System.Windows.Forms.DataGridViewColumnEventHandler ColumnSortModeChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BB974FB5F4CDF664 | Event | `public event System.Windows.Forms.DataGridViewColumnStateChangedEventHandler ColumnStateChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-48B945456B9F729F | Event | `public event System.Windows.Forms.DataGridViewColumnEventHandler ColumnToolTipTextChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D46E159F123E7020 | Event | `public event System.Windows.Forms.DataGridViewColumnEventHandler ColumnWidthChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ABCD47EE4D32B6C8 | Event | `public event System.EventHandler ContextMenuChanged` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FD0AC1C879EFF6C6 | Event | `public event System.EventHandler ContextMenuStripChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F4C6A181EA3D237F | Event | `public event System.Windows.Forms.ControlEventHandler ControlAdded` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CE2B32A638F4EDA7 | Event | `public event System.Windows.Forms.ControlEventHandler ControlRemoved` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6E4BA7927B03657D | Event | `public event System.EventHandler CurrentCellChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F05D2363AB4BCA0D | Event | `public event System.EventHandler CurrentCellDirtyStateChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7C22608FCF2369AE | Event | `public event System.EventHandler CursorChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-897417051E91F543 | Event | `public event System.Windows.Forms.DataGridViewBindingCompleteEventHandler DataBindingComplete` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9EA751978429A87F | Event | `public event System.EventHandler DataContextChanged` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-69D44326A0FACC3C | Event | `public event System.Windows.Forms.DataGridViewDataErrorEventHandler DataError` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6FC0940031440A5B | Event | `public event System.EventHandler DataMemberChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6787BF93CC425B3E | Event | `public event System.EventHandler DataSourceChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CD96A1524A2A26F9 | Event | `public event System.EventHandler DefaultCellStyleChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1349B9555214B5A5 | Event | `public event System.Windows.Forms.DataGridViewRowEventHandler DefaultValuesNeeded` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AD9838E4AAB78859 | Event | `public event System.EventHandler Disposed` | `System.ComponentModel.Component` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50D043FA9A2C7649 | Event | `public event System.EventHandler DockChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-46ECD7D48FE4A449 | Event | `public event System.EventHandler DoubleClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC0ADCB3492E13A6 | Event | `public event System.EventHandler DpiChangedAfterParent` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-92DAAB89D7D8B1A4 | Event | `public event System.EventHandler DpiChangedBeforeParent` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-36D59153A0FDA93E | Event | `public event System.Windows.Forms.DragEventHandler DragDrop` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-66C2BB43931D9FE4 | Event | `public event System.Windows.Forms.DragEventHandler DragEnter` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D1F48F39958F15B | Event | `public event System.EventHandler DragLeave` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29300F30B11D98F6 | Event | `public event System.Windows.Forms.DragEventHandler DragOver` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-611A05F37DAD4F8F | Event | `public event System.Windows.Forms.DataGridViewEditingControlShowingEventHandler EditingControlShowing` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D4D244D9A839EFD7 | Event | `public event System.EventHandler EditModeChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-66F9700EDB8B826D | Event | `public event System.EventHandler EnabledChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-79D51B42B38C3DD1 | Event | `public event System.EventHandler Enter` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-866AF9F7D352C48B | Event | `public event System.EventHandler FontChanged` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AB996584538A86A2 | Event | `public event System.EventHandler ForeColorChanged` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FBC0DDF6422B3AC0 | Event | `public event System.Windows.Forms.GiveFeedbackEventHandler GiveFeedback` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-002127332386B280 | Event | `public event System.EventHandler GotFocus` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-35B1FBF98657D8D5 | Event | `public event System.EventHandler GridColorChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2EEC3B149088691B | Event | `public event System.EventHandler HandleCreated` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BA2D4B2E948D8477 | Event | `public event System.EventHandler HandleDestroyed` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0698E2ED4D1FD264 | Event | `public event System.Windows.Forms.HelpEventHandler HelpRequested` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D64FCB2D5918DFB0 | Event | `public event System.EventHandler ImeModeChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-86776507DE8D4269 | Event | `public event System.Windows.Forms.InvalidateEventHandler Invalidated` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B641AFD70163813 | Event | `public event System.Windows.Forms.KeyEventHandler KeyDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC1BA90F91FFC8A7 | Event | `public event System.Windows.Forms.KeyPressEventHandler KeyPress` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-22D6F8D4B7F85B46 | Event | `public event System.Windows.Forms.KeyEventHandler KeyUp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3A80BF4DEC3DA988 | Event | `public event System.Windows.Forms.LayoutEventHandler Layout` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-84880306BB994A8D | Event | `public event System.EventHandler Leave` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D675F6E0A9FCF091 | Event | `public event System.EventHandler LocationChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F2A5800F15347AAB | Event | `public event System.EventHandler LostFocus` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E6F67736708559C | Event | `public event System.EventHandler MarginChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5B072423113E2567 | Event | `public event System.EventHandler MouseCaptureChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B9A01185262011B | Event | `public event System.Windows.Forms.MouseEventHandler MouseClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6F6126CFA4F30A5C | Event | `public event System.Windows.Forms.MouseEventHandler MouseDoubleClick` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CD4FF5A5B15AC59A | Event | `public event System.Windows.Forms.MouseEventHandler MouseDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5F38A9624725D9F3 | Event | `public event System.EventHandler MouseEnter` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E1BBFFC3C03C0771 | Event | `public event System.EventHandler MouseHover` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-933DB37DD07D63BE | Event | `public event System.EventHandler MouseLeave` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1FD14B05095621D1 | Event | `public event System.Windows.Forms.MouseEventHandler MouseMove` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-25BB513DBEE9ACC0 | Event | `public event System.Windows.Forms.MouseEventHandler MouseUp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D8ECFC1DB1EA7106 | Event | `public event System.Windows.Forms.MouseEventHandler MouseWheel` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DC910DFB63BAF142 | Event | `public event System.EventHandler Move` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2F0B8A3D4A8444F4 | Event | `public event System.EventHandler MultiSelectChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B3AE976EC5B1F42 | Event | `public event System.Windows.Forms.DataGridViewRowEventHandler NewRowNeeded` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AD8BBFE9274658BA | Event | `public event System.EventHandler PaddingChanged` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0129D60D9A5916F8 | Event | `public event System.Windows.Forms.PaintEventHandler Paint` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-986C2BE712F23A8E | Event | `public event System.EventHandler ParentChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AC94847B8AFF753E | Event | `public event System.Windows.Forms.PreviewKeyDownEventHandler PreviewKeyDown` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C83FA66005597CD | Event | `public event System.Windows.Forms.QueryAccessibilityHelpEventHandler QueryAccessibilityHelp` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-52D1A214BF8B17D1 | Event | `public event System.Windows.Forms.QueryContinueDragEventHandler QueryContinueDrag` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1674B05C951166CA | Event | `public event System.EventHandler ReadOnlyChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1F151CFC611189A3 | Event | `public event System.EventHandler RegionChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A78BA8F470B3822 | Event | `public event System.EventHandler Resize` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B1463320AA4AFE0 | Event | `public event System.EventHandler RightToLeftChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-94685A377C07A103 | Event | `public event System.Windows.Forms.DataGridViewRowEventHandler RowContextMenuStripChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-56054ABC0AB12E36 | Event | `public event System.Windows.Forms.DataGridViewRowContextMenuStripNeededEventHandler RowContextMenuStripNeeded` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EF1723C55D7181CB | Event | `public event System.Windows.Forms.DataGridViewRowEventHandler RowDefaultCellStyleChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-09469A1B3822D46C | Event | `public event System.Windows.Forms.QuestionEventHandler RowDirtyStateNeeded` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F9F17432A812CE90 | Event | `public event System.Windows.Forms.DataGridViewRowDividerDoubleClickEventHandler RowDividerDoubleClick` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D67A9DF60FC1044F | Event | `public event System.Windows.Forms.DataGridViewRowEventHandler RowDividerHeightChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B0C1F09A35F668A9 | Event | `public event System.Windows.Forms.DataGridViewCellEventHandler RowEnter` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-33FC7605028863A0 | Event | `public event System.Windows.Forms.DataGridViewRowEventHandler RowErrorTextChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-87DCD5061F7F0B16 | Event | `public event System.Windows.Forms.DataGridViewRowErrorTextNeededEventHandler RowErrorTextNeeded` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7F2EED25F85B0752 | Event | `public event System.Windows.Forms.DataGridViewRowEventHandler RowHeaderCellChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3E24E7EC5C907A9F | Event | `public event System.Windows.Forms.DataGridViewCellMouseEventHandler RowHeaderMouseClick` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B91DD4058161AFC | Event | `public event System.Windows.Forms.DataGridViewCellMouseEventHandler RowHeaderMouseDoubleClick` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-03B81FB6785E765F | Event | `public event System.EventHandler RowHeadersBorderStyleChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-46B6E8448E0D7CFF | Event | `public event System.EventHandler RowHeadersDefaultCellStyleChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7EE5362751A5B428 | Event | `public event System.EventHandler RowHeadersWidthChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-44220FF4E6A62ADE | Event | `public event System.Windows.Forms.DataGridViewAutoSizeModeEventHandler RowHeadersWidthSizeModeChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CE4CE1233C2CDE4B | Event | `public event System.Windows.Forms.DataGridViewRowEventHandler RowHeightChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C64F19B25AD9F916 | Event | `public event System.Windows.Forms.DataGridViewRowHeightInfoNeededEventHandler RowHeightInfoNeeded` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-39EBF2CDCB3E4ABC | Event | `public event System.Windows.Forms.DataGridViewRowHeightInfoPushedEventHandler RowHeightInfoPushed` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8887CDB3BBDE7A7B | Event | `public event System.Windows.Forms.DataGridViewCellEventHandler RowLeave` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-62E0A608D4DA8F37 | Event | `public event System.Windows.Forms.DataGridViewRowEventHandler RowMinimumHeightChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-295382285B36122B | Event | `public event System.Windows.Forms.DataGridViewRowPostPaintEventHandler RowPostPaint` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D8F1EADD65510703 | Event | `public event System.Windows.Forms.DataGridViewRowPrePaintEventHandler RowPrePaint` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B5629E4EB276D0A7 | Event | `public event System.Windows.Forms.DataGridViewRowsAddedEventHandler RowsAdded` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0CE34C9CB68419F4 | Event | `public event System.EventHandler RowsDefaultCellStyleChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-900D0DC520A9F9DE | Event | `public event System.Windows.Forms.DataGridViewRowsRemovedEventHandler RowsRemoved` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D1210670332FDE4 | Event | `public event System.Windows.Forms.DataGridViewRowStateChangedEventHandler RowStateChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B4D9BDFA1BE7648F | Event | `public event System.Windows.Forms.DataGridViewRowEventHandler RowUnshared` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C2AFEA2307CB14B | Event | `public event System.Windows.Forms.DataGridViewCellEventHandler RowValidated` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-673181211FE02C50 | Event | `public event System.Windows.Forms.DataGridViewCellCancelEventHandler RowValidating` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BC8CF0EA0DFE8711 | Event | `public event System.Windows.Forms.ScrollEventHandler Scroll` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6CE7FE6C2C2746DC | Event | `public event System.EventHandler SelectionChanged` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C2852CC5A48C737A | Event | `public event System.EventHandler SizeChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D97A6E6D3395C169 | Event | `public event System.Windows.Forms.DataGridViewSortCompareEventHandler SortCompare` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A5D48E7FFA454443 | Event | `public event System.EventHandler Sorted` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BBF34105669FCB42 | Event | `public event System.EventHandler StyleChanged` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-53B329C00F0F0E09 | Event | `public event System.EventHandler SystemColorsChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B76F0748FCFE7FE | Event | `public event System.EventHandler TabIndexChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A79EF74496C86301 | Event | `public event System.EventHandler TabStopChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CC1DB04FA4DF9F39 | Event | `public event System.EventHandler TextChanged` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B20A304A64EA7634 | Event | `public event System.Windows.Forms.DataGridViewRowEventHandler UserAddedRow` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5AC32B8BC50D8CD9 | Event | `public event System.Windows.Forms.DataGridViewRowEventHandler UserDeletedRow` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AAB0A98BE7B77EE3 | Event | `public event System.Windows.Forms.DataGridViewRowCancelEventHandler UserDeletingRow` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-44409194DB1E344E | Event | `public event System.EventHandler Validated` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-36A983737063232C | Event | `public event System.ComponentModel.CancelEventHandler Validating` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-641494EA046C19A1 | Event | `public event System.EventHandler VisibleChanged` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-901594662E2CE2A6 | Method | `public virtual System.Windows.Forms.DataGridViewAdvancedBorderStyle AdjustColumnHeaderBorderStyle(System.Windows.Forms.DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyleInput, System.Windows.Forms.DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder, System.Boolean isFirstDisplayedColumn, System.Boolean isLastVisibleColumn)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B6B1DD33D5917F32 | Method | `public System.Boolean AreAllCellsSelected(System.Boolean includeInvisibleCells)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B9C90CBA49E9F79 | Method | `public System.Void AutoResizeColumn(System.Int32 columnIndex)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-693E64588B53A9CE | Method | `public System.Void AutoResizeColumn(System.Int32 columnIndex, System.Windows.Forms.DataGridViewAutoSizeColumnMode autoSizeColumnMode)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9ADE8429CD3D3B59 | Method | `public System.Void AutoResizeColumnHeadersHeight()` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-825F9268B564E103 | Method | `public System.Void AutoResizeColumnHeadersHeight(System.Int32 columnIndex)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B0143167179BDD24 | Method | `public System.Void AutoResizeColumns()` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B027B08763A7139F | Method | `public System.Void AutoResizeColumns(System.Windows.Forms.DataGridViewAutoSizeColumnsMode autoSizeColumnsMode)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A501A9360D628B25 | Method | `public System.Void AutoResizeRow(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0D3BA3FBB8AE92CD | Method | `public System.Void AutoResizeRow(System.Int32 rowIndex, System.Windows.Forms.DataGridViewAutoSizeRowMode autoSizeRowMode)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A7683F76064FAB13 | Method | `public System.Void AutoResizeRowHeadersWidth(System.Int32 rowIndex, System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode rowHeadersWidthSizeMode)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5256D26225547FC5 | Method | `public System.Void AutoResizeRowHeadersWidth(System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode rowHeadersWidthSizeMode)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9DD47940841911D9 | Method | `public System.Void AutoResizeRows()` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-18839ADB90CE0FD5 | Method | `public System.Void AutoResizeRows(System.Windows.Forms.DataGridViewAutoSizeRowsMode autoSizeRowsMode)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9DC60AF8E1964510 | Method | `public virtual System.Boolean BeginEdit(System.Boolean selectAll)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F4D4628E4B1CEA10 | Method | `public System.IAsyncResult BeginInvoke(System.Action method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-88D56043332AC8E4 | Method | `public System.IAsyncResult BeginInvoke(System.Delegate method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-787F0A7D81604275 | Method | `public System.IAsyncResult BeginInvoke(System.Delegate method, System.Object[] args)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2D80FFD8A4FB3752 | Method | `public System.Void BringToFront()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9CBF5F9CD2915F7D | Method | `public System.Boolean CancelEdit()` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5A41ED3477DA70E0 | Method | `public System.Void ClearSelection()` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DF9864061F0E63FA | Method | `public System.Boolean CommitEdit(System.Windows.Forms.DataGridViewDataErrorContexts context)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B6105597A633672F | Method | `public System.Boolean Contains(System.Windows.Forms.Control ctl)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AF019AA6AF5F7938 | Method | `public System.Void CreateControl()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E500FE6B858A74C7 | Method | `public System.Drawing.Graphics CreateGraphics()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A98B25C4C4DA03DD | Method | `public System.Int32 DisplayedColumnCount(System.Boolean includePartialColumns)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AD78FD02A735B59C | Method | `public System.Int32 DisplayedRowCount(System.Boolean includePartialRow)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-84DC46A32A0D1E5F | Method | `public System.Void Dispose()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-641C3429E88DA5A1 | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-128FB0AFDFAD979B | Method | `public System.Windows.Forms.DragDropEffects DoDragDrop(System.Object data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7783AF7F84E7E17D | Method | `public System.Windows.Forms.DragDropEffects DoDragDropAsJson<!!0>(!!0 data, System.Windows.Forms.DragDropEffects allowedEffects) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F3C9AEFF8BF4AADA | Method | `public System.Windows.Forms.DragDropEffects DoDragDropAsJson<!!0>(!!0 data, System.Windows.Forms.DragDropEffects allowedEffects, System.Drawing.Bitmap dragImage, System.Drawing.Point cursorOffset, System.Boolean useDefaultDragImage) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DCFD9D8F7D9F0B1F | Method | `public System.Void DrawToBitmap(System.Drawing.Bitmap bitmap, System.Drawing.Rectangle targetBounds)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8ED6D697A608120F | Method | `public System.Boolean EndEdit()` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5D293BFA7B80E494 | Method | `public System.Boolean EndEdit(System.Windows.Forms.DataGridViewDataErrorContexts context)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D27C0490C2D6D2FB | Method | `public System.Object EndInvoke(System.IAsyncResult asyncResult)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8257CE9426E102A8 | Method | `public System.Windows.Forms.Form FindForm()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B03D7D010F0D28C0 | Method | `public System.Boolean Focus()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9F86664DA090349D | Method | `public static System.Windows.Forms.Control FromChildHandle(System.IntPtr handle)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E1170CAC53DBED89 | Method | `public static System.Windows.Forms.Control FromHandle(System.IntPtr handle)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1DE2B48AFD13A0D1 | Method | `public System.Int32 GetCellCount(System.Windows.Forms.DataGridViewElementStates includeFilter)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3741C35DACBC280F | Method | `public System.Drawing.Rectangle GetCellDisplayRectangle(System.Int32 columnIndex, System.Int32 rowIndex, System.Boolean cutOverflow)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-34047FB103490C71 | Method | `public System.Windows.Forms.Control GetChildAtPoint(System.Drawing.Point pt)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-980880C3D8048109 | Method | `public System.Windows.Forms.Control GetChildAtPoint(System.Drawing.Point pt, System.Windows.Forms.GetChildAtPointSkip skipValue)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7BF75E06E4170C97 | Method | `public virtual System.Windows.Forms.DataObject GetClipboardContent()` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F427F47611A0D3D6 | Method | `public System.Drawing.Rectangle GetColumnDisplayRectangle(System.Int32 columnIndex, System.Boolean cutOverflow)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EA926BEDB12C1D94 | Method | `public System.Windows.Forms.IContainerControl GetContainerControl()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-95A38C33B0AEDE9A | Method | `public System.Windows.Forms.Control GetNextControl(System.Windows.Forms.Control ctl, System.Boolean forward)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2CAAE9D8C64403CC | Method | `public virtual System.Drawing.Size GetPreferredSize(System.Drawing.Size proposedSize)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C10921257C9AC89 | Method | `public System.Drawing.Rectangle GetRowDisplayRectangle(System.Int32 rowIndex, System.Boolean cutOverflow)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F7DFD1145535835E | Method | `public System.Void Hide()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-012BBBF70EACF93E | Method | `public System.Windows.Forms.DataGridView+HitTestInfo HitTest(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-73E18FDB6D8B558F | Method | `public System.Void Invalidate()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0961824733CF42F7 | Method | `public System.Void Invalidate(System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A8E7DD2358AF5154 | Method | `public System.Void Invalidate(System.Drawing.Rectangle rc)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0AFFA652DCF45995 | Method | `public System.Void Invalidate(System.Drawing.Rectangle rc, System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-66F0751E786BF072 | Method | `public System.Void Invalidate(System.Drawing.Region region)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-70F9459115EFB202 | Method | `public System.Void Invalidate(System.Drawing.Region region, System.Boolean invalidateChildren)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6BB390D70F97A9A2 | Method | `public System.Void InvalidateCell(System.Int32 columnIndex, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-03B6110B36A2B9D4 | Method | `public System.Void InvalidateCell(System.Windows.Forms.DataGridViewCell dataGridViewCell)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C5B17291392C5001 | Method | `public System.Void InvalidateColumn(System.Int32 columnIndex)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C92622C2EE1529C | Method | `public System.Void InvalidateRow(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-60521159AB24D965 | Method | `public !!0 Invoke<!!0>(System.Func&#96;1<!!0> method) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8565BF684100FB1F | Method | `public System.Object Invoke(System.Delegate method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D36E2D72BDB0105 | Method | `public System.Object Invoke(System.Delegate method, System.Object[] args)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0D3828B79167FE3B | Method | `public System.Void Invoke(System.Action method)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F9312BC834800A16 | Method | `public System.Threading.Tasks.Task InvokeAsync(System.Action callback, System.Threading.CancellationToken cancellationToken = null)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E3CAB4335ED383A8 | Method | `public System.Threading.Tasks.Task InvokeAsync(System.Func&#96;2<System.Threading.CancellationToken,System.Threading.Tasks.ValueTask> callback, System.Threading.CancellationToken cancellationToken = null)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3F9F6DE3CEB180F8 | Method | `public System.Threading.Tasks.Task&#96;1<!!0> InvokeAsync<!!0>(System.Func&#96;1<!!0> callback, System.Threading.CancellationToken cancellationToken = null) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-578340C4B7D206A2 | Method | `public System.Threading.Tasks.Task&#96;1<!!0> InvokeAsync<!!0>(System.Func&#96;2<System.Threading.CancellationToken,System.Threading.Tasks.ValueTask&#96;1<!!0>> callback, System.Threading.CancellationToken cancellationToken = null) where !!0: None;` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-84E52234917FF89C | Method | `public static System.Boolean IsKeyLocked(System.Windows.Forms.Keys keyVal)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FD7D4CD6F426D2B2 | Method | `public static System.Boolean IsMnemonic(System.Char charCode, System.String text)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6EF7944807D299B0 | Method | `public System.Drawing.Size LogicalToDeviceUnits(System.Drawing.Size value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5F756AE92D3320B5 | Method | `public System.Int32 LogicalToDeviceUnits(System.Int32 value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B4B2F60971953016 | Method | `public virtual System.Void NotifyCurrentCellDirty(System.Boolean dirty)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B7B558FDE3E77A2 | Method | `public System.Void PerformLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8A742FFC78383E20 | Method | `public System.Void PerformLayout(System.Windows.Forms.Control affectedControl, System.String affectedProperty)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-71E3308EF2A408A2 | Method | `public System.Drawing.Point PointToClient(System.Drawing.Point p)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A974A6B12A601152 | Method | `public System.Drawing.Point PointToScreen(System.Drawing.Point p)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B1DF28D267E24FB7 | Method | `public System.Windows.Forms.PreProcessControlState PreProcessControlMessage(ref System.Windows.Forms.Message msg)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7CF24441597D3533 | Method | `public virtual System.Boolean PreProcessMessage(ref System.Windows.Forms.Message msg)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CCE55A922C38E706 | Method | `public System.Drawing.Rectangle RectangleToClient(System.Drawing.Rectangle r)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CF641A4D6C7434E8 | Method | `public System.Drawing.Rectangle RectangleToScreen(System.Drawing.Rectangle r)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3AFADF276C58FC45 | Method | `public virtual System.Void Refresh()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DE6EB457FE4C54B7 | Method | `public System.Boolean RefreshEdit()` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DBA3F189BEB85552 | Method | `public virtual System.Void ResetBackColor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-886932A30C9F1278 | Method | `public System.Void ResetBindings()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-31C65B7DDDCE3232 | Method | `public virtual System.Void ResetCursor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C674CCA0CB5E6869 | Method | `public virtual System.Void ResetFont()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2026AB9BFDEB4771 | Method | `public virtual System.Void ResetForeColor()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3EC7153F8CE4C8A0 | Method | `public System.Void ResetImeMode()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-127DA176FC723687 | Method | `public virtual System.Void ResetRightToLeft()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A4D04172E295155 | Method | `public virtual System.Void ResetText()` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-068079C7E4CB835F | Method | `public System.Void ResumeLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B75738D70F26BA21 | Method | `public System.Void ResumeLayout(System.Boolean performLayout)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-191A44CB38EB3277 | Method | `public System.Void Scale(System.Drawing.SizeF factor)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2F9DF52497A7E524 | Method | `public System.Void Scale(System.Single dx, System.Single dy)` | `System.Windows.Forms.Control` ObsoleteAttribute("This method has been deprecated. Use the Scale(SizeF ratio) method instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-81093D9253B8CF93 | Method | `public System.Void Scale(System.Single ratio)` | `System.Windows.Forms.Control` ObsoleteAttribute("This method has been deprecated. Use the Scale(SizeF ratio) method instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FE1EC36F5C7D8C23 | Method | `public System.Void ScaleBitmapLogicalToDevice(ref System.Drawing.Bitmap logicalBitmap)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A4A2F03E37EB5EAB | Method | `public System.Void Select()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CE7334AF41B7B7E8 | Method | `public System.Void SelectAll()` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-235A03B9D071DD1C | Method | `public System.Boolean SelectNextControl(System.Windows.Forms.Control ctl, System.Boolean forward, System.Boolean tabStopOnly, System.Boolean nested, System.Boolean wrap)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B17F69591538A85 | Method | `public System.Void SendToBack()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CACC6F769DB23B39 | Method | `public System.Void SetBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B1A9F4BF211C5FFD | Method | `public System.Void SetBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FD81A93D41985F34 | Method | `public System.Void Show()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7CF523538276FADB | Method | `public virtual System.Void Sort(System.Collections.IComparer comparer)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-86E6CB222645E496 | Method | `public virtual System.Void Sort(System.Windows.Forms.DataGridViewColumn dataGridViewColumn, System.ComponentModel.ListSortDirection direction)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-659273DA2FDC501D | Method | `public System.Void SuspendLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0864BCA07B02CAF5 | Method | `public virtual System.String ToString()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0AF09E72AD02ABB9 | Method | `public System.Void Update()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B9B20C95C1635D0E | Method | `public System.Void UpdateCellErrorText(System.Int32 columnIndex, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-82B254E88333E529 | Method | `public System.Void UpdateCellValue(System.Int32 columnIndex, System.Int32 rowIndex)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5A8F8245E5F7C011 | Method | `public System.Void UpdateRowErrorText(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6C6F86B1833D24E9 | Method | `public System.Void UpdateRowErrorText(System.Int32 rowIndexStart, System.Int32 rowIndexEnd)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0FDEF636EF570F31 | Method | `public System.Void UpdateRowHeightInfo(System.Int32 rowIndex, System.Boolean updateToEnd)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2BD99415618F4939 | Property | `System.Windows.Forms.AccessibleObject AccessibilityObject { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-332DA4CC6B98D775 | Property | `System.String AccessibleDefaultActionDescription { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-08166234BE236858 | Property | `System.String AccessibleDescription { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-27661638FE9ECE14 | Property | `System.String AccessibleName { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B641F657C8BA4952 | Property | `System.Windows.Forms.AccessibleRole AccessibleRole { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(-1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3B594E85045C855C | Property | `virtual System.Windows.Forms.DataGridViewAdvancedBorderStyle AdjustedTopLeftHeaderBorderStyle { public get; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8B1F9C621602656F | Property | `System.Windows.Forms.DataGridViewAdvancedBorderStyle AdvancedCellBorderStyle { public get; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A6B4D76442932BBB | Property | `System.Windows.Forms.DataGridViewAdvancedBorderStyle AdvancedColumnHeadersBorderStyle { public get; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-43BCDF056768AEF5 | Property | `System.Windows.Forms.DataGridViewAdvancedBorderStyle AdvancedRowHeadersBorderStyle { public get; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9EFBFB95AFED9507 | Property | `virtual System.Boolean AllowDrop { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EDB65C7F8CC8D670 | Property | `System.Boolean AllowUserToAddRows { public get; public set; }` | `System.Windows.Forms.DataGridView` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-280F8EC53EA4FBC5 | Property | `System.Boolean AllowUserToDeleteRows { public get; public set; }` | `System.Windows.Forms.DataGridView` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-840F0BA113F21A35 | Property | `System.Boolean AllowUserToOrderColumns { public get; public set; }` | `System.Windows.Forms.DataGridView` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-73D8FF640EE30DE7 | Property | `System.Boolean AllowUserToResizeColumns { public get; public set; }` | `System.Windows.Forms.DataGridView` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B328FA19DB75CCA6 | Property | `System.Boolean AllowUserToResizeRows { public get; public set; }` | `System.Windows.Forms.DataGridView` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-46C8324DBEB02FA5 | Property | `System.Windows.Forms.DataGridViewCellStyle AlternatingRowsDefaultCellStyle { public get; public set; }` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-65E8E8665D5EC296 | Property | `virtual System.Windows.Forms.AnchorStyles Anchor { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true); DefaultValueAttribute(5) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-121649677091C579 | Property | `System.Boolean AutoGenerateColumns { public get; public set; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false); DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3F1F53A6DC87D448 | Property | `virtual System.Drawing.Point AutoScrollOffset { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DefaultValueAttribute(System.Drawing.Point,"0, 0") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-835C6210E98DFF9F | Property | `virtual System.Boolean AutoSize { public get; public set; }` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-25CD14974AFC6816 | Property | `System.Windows.Forms.DataGridViewAutoSizeColumnsMode AutoSizeColumnsMode { public get; public set; }` | `System.Windows.Forms.DataGridView` DefaultValueAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1E9ABF148ACBBAC4 | Property | `System.Windows.Forms.DataGridViewAutoSizeRowsMode AutoSizeRowsMode { public get; public set; }` | `System.Windows.Forms.DataGridView` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DFA49E6F59DF8989 | Property | `virtual System.Drawing.Color BackColor { public get; public set; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AA243C552DA0C473 | Property | `System.Drawing.Color BackgroundColor { public get; public set; }` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D6399BFED48E130 | Property | `virtual System.Drawing.Image BackgroundImage { public get; public set; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C314B56FEAD69C16 | Property | `virtual System.Windows.Forms.ImageLayout BackgroundImageLayout { public get; public set; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FB50E66C6968B327 | Property | `virtual System.Windows.Forms.BindingContext BindingContext { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5F567E3E2A888186 | Property | `System.Windows.Forms.BorderStyle BorderStyle { public get; public set; }` | `System.Windows.Forms.DataGridView` DefaultValueAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5E757AEC95411708 | Property | `System.Int32 Bottom { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-98451A884C4837A4 | Property | `System.Drawing.Rectangle Bounds { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5884F0C23727FDA3 | Property | `System.Boolean CanFocus { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A79725546B6D388A | Property | `System.Boolean CanSelect { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6DDEE683DA0FBFE7 | Property | `System.Boolean Capture { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DCD01E8F2597B872 | Property | `System.Boolean CausesValidation { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-15FFA9D1DAC67070 | Property | `System.Windows.Forms.DataGridViewCellBorderStyle CellBorderStyle { public get; public set; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(true); DefaultValueAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5C8F419E6E19B812 | Property | `static System.Boolean CheckForIllegalCrossThreadCalls { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-48BA290F776E0339 | Property | `System.Drawing.Rectangle ClientRectangle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-81DF1F27D254D8E3 | Property | `System.Drawing.Size ClientSize { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-001A20AC8C91A6F4 | Property | `System.Windows.Forms.DataGridViewClipboardCopyMode ClipboardCopyMode { public get; public set; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(true); DefaultValueAttribute(1) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4A0710D3FB38777D | Property | `System.Int32 ColumnCount { public get; public set; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0); DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7CD7DED217941DF9 | Property | `System.Windows.Forms.DataGridViewHeaderBorderStyle ColumnHeadersBorderStyle { public get; public set; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(true); DefaultValueAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-668FCA23D9117AB8 | Property | `System.Windows.Forms.DataGridViewCellStyle ColumnHeadersDefaultCellStyle { public get; public set; }` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2C511E7BA0786825 | Property | `System.Int32 ColumnHeadersHeight { public get; public set; }` | `System.Windows.Forms.DataGridView` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5FE032AD41A5406A | Property | `System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode ColumnHeadersHeightSizeMode { public get; public set; }` | `System.Windows.Forms.DataGridView` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4657B25D9B8126A8 | Property | `System.Boolean ColumnHeadersVisible { public get; public set; }` | `System.Windows.Forms.DataGridView` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6EEC463A4290E6FA | Property | `System.Windows.Forms.DataGridViewColumnCollection Columns { public get; }` | `System.Windows.Forms.DataGridView` EditorAttribute("System.Windows.Forms.Design.DataGridViewColumnCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor); DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-62F1143E1E8BE006 | Property | `System.String CompanyName { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3D68F32C991063EB | Property | `System.ComponentModel.IContainer Container { public get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-34157BED60CEFFD0 | Property | `System.Boolean ContainsFocus { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4A2674B24B919E95 | Property | `virtual System.Windows.Forms.ContextMenu ContextMenu { public get; public set; }` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D6DDF8A4CBD7C5BB | Property | `virtual System.Windows.Forms.ContextMenuStrip ContextMenuStrip { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ACC9D11949428D64 | Property | `System.Windows.Forms.Control+ControlCollection Controls { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-01C4E3B43D74B649 | Property | `System.Boolean Created { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-76EE9E81E534EBBA | Property | `System.Windows.Forms.DataGridViewCell CurrentCell { public get; public set; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0D7938595A1E3F65 | Property | `System.Drawing.Point CurrentCellAddress { public get; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-52329BBEEF76D050 | Property | `System.Windows.Forms.DataGridViewRow CurrentRow { public get; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2765905E98CBBB25 | Property | `virtual System.Windows.Forms.Cursor Cursor { public get; public set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AAFCBEE8C1372E86 | Property | `System.Windows.Forms.ControlBindingsCollection DataBindings { public get; }` | `System.Windows.Forms.Control` DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-33C8370182CBD253 | Property | `virtual System.Object DataContext { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); BindableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4C2FC742046F26CE | Property | `System.String DataMember { public get; public set; }` | `System.Windows.Forms.DataGridView` DefaultValueAttribute(""); EditorAttribute("System.Windows.Forms.Design.DataMemberListEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",System.Drawing.Design.UITypeEditor) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E2AE2A87C945B529 | Property | `System.Object DataSource { public get; public set; }` | `System.Windows.Forms.DataGridView` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B02F942F9FDA9072 | Property | `static System.Drawing.Color DefaultBackColor { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4C271F38C3860C11 | Property | `System.Windows.Forms.DataGridViewCellStyle DefaultCellStyle { public get; public set; }` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CCD22AE7BA41738E | Property | `static System.Drawing.Font DefaultFont { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6494B5CAD2056C23 | Property | `static System.Drawing.Color DefaultForeColor { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-71380757E0A14BFD | Property | `System.Int32 DeviceDpi { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-636CE0BF2F8AAACA | Property | `virtual System.Drawing.Rectangle DisplayRectangle { public get; }` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2CA5A396E21CFD76 | Property | `System.Boolean Disposing { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1522F85FB0AB47EB | Property | `virtual System.Windows.Forms.DockStyle Dock { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true); DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7421856A8D44A478 | Property | `System.Windows.Forms.Control EditingControl { public get; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5774FFD63BFA04CE | Property | `System.Windows.Forms.Panel EditingPanel { public get; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-11D40B120F7B544E | Property | `System.Windows.Forms.DataGridViewEditMode EditMode { public get; public set; }` | `System.Windows.Forms.DataGridView` DefaultValueAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F42D2EAC4576EA58 | Property | `System.Boolean Enabled { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-36DA786EF678E1EE | Property | `System.Boolean EnableHeadersVisualStyles { public get; public set; }` | `System.Windows.Forms.DataGridView` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-499D2408EEE607C1 | Property | `System.Windows.Forms.DataGridViewCell FirstDisplayedCell { public get; public set; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0EC6690164FB0BEE | Property | `System.Int32 FirstDisplayedScrollingColumnHiddenWidth { public get; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-22078A2C54D24223 | Property | `System.Int32 FirstDisplayedScrollingColumnIndex { public get; public set; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-37E8B1A498ABCF74 | Property | `System.Int32 FirstDisplayedScrollingRowIndex { public get; public set; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-26C6936AC4C52F89 | Property | `virtual System.Boolean Focused { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C38BDF92F8332E9 | Property | `virtual System.Drawing.Font Font { public get; public set; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CDD3DE4890DE6473 | Property | `virtual System.Drawing.Color ForeColor { public get; public set; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2072432EA98760CE | Property | `System.Drawing.Color GridColor { public get; public set; }` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-319B3D02EC121EE5 | Property | `System.IntPtr Handle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9793A90B7095EB02 | Property | `System.Boolean HasChildren { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B12F042B06A4DCAD | Property | `System.Int32 Height { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E5CF882212D9D07E | Property | `System.Int32 HorizontalScrollingOffset { public get; public set; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EBF8D89F29B67084 | Property | `System.Windows.Forms.ImeMode ImeMode { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8F037D78BEF29AD5 | Property | `System.Boolean InvokeRequired { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-77217C81A00BD1D3 | Property | `System.Boolean IsAccessible { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-49A4710B14BF34E8 | Property | `System.Boolean IsAncestorSiteInDesignMode { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-206392EA2D9E79F4 | Property | `System.Boolean IsCurrentCellDirty { public get; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AF02F233C875ADA4 | Property | `System.Boolean IsCurrentCellInEditMode { public get; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FA25CB3F83056614 | Property | `System.Boolean IsCurrentRowDirty { public get; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0D8D7FAAED3B8448 | Property | `System.Boolean IsDisposed { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E67D96068DF34906 | Property | `System.Boolean IsHandleCreated { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EF6E53A83EDE7E99 | Property | `System.Boolean IsMirrored { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-97AB14FC7A295D1F | Property | `System.Windows.Forms.DataGridViewCell Item[System.Int32 columnIndex, System.Int32 rowIndex] { public get; public set; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B60C8EF8B2E49056 | Property | `System.Windows.Forms.DataGridViewCell Item[System.String columnName, System.Int32 rowIndex] { public get; public set; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CDCA2BFBCA06FCF3 | Property | `virtual System.Windows.Forms.Layout.LayoutEngine LayoutEngine { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DDD20CC16F895379 | Property | `System.Int32 Left { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B15C14BE21DC731F | Property | `System.Drawing.Point Location { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-491C9BE9EDA05543 | Property | `System.Windows.Forms.Padding Margin { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-59BA02EC81402365 | Property | `virtual System.Drawing.Size MaximumSize { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4431D82601D765EE | Property | `virtual System.Drawing.Size MinimumSize { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8CAC8441998DADC5 | Property | `static System.Windows.Forms.Keys ModifierKeys { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D08E62428B1C447D | Property | `static System.Windows.Forms.MouseButtons MouseButtons { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-981941F45605D993 | Property | `static System.Drawing.Point MousePosition { public get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5D800FC4D3E89598 | Property | `System.Boolean MultiSelect { public get; public set; }` | `System.Windows.Forms.DataGridView` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2FD49A299D265D31 | Property | `System.String Name { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-469B0F40F6EE3266 | Property | `System.Int32 NewRowIndex { public get; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-109A1B16A53A00B8 | Property | `System.Windows.Forms.Padding Padding { public get; public set; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D94BFADC4D4E9BC1 | Property | `System.Windows.Forms.Control Parent { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0FBDC324ACBC741D | Property | `System.Drawing.Size PreferredSize { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5E5A8EB3D2C80ECB | Property | `System.String ProductName { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-51D47EAB88714127 | Property | `System.String ProductVersion { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ADAC960658F62817 | Property | `System.Boolean ReadOnly { public get; public set; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(true); DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A0423B67A886DAEF | Property | `System.Boolean RecreatingHandle { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8B18781E2405F3EB | Property | `System.Drawing.Region Region { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3FC35BBA3E393C66 | Property | `System.Int32 Right { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AB1A03FCF39A5CC0 | Property | `virtual System.Windows.Forms.RightToLeft RightToLeft { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-10750BB76C3047B0 | Property | `System.Int32 RowCount { public get; public set; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0); DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8B025FB2B0B0AF8F | Property | `System.Windows.Forms.DataGridViewHeaderBorderStyle RowHeadersBorderStyle { public get; public set; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(true); DefaultValueAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-69ACAB482CE93ABA | Property | `System.Windows.Forms.DataGridViewCellStyle RowHeadersDefaultCellStyle { public get; public set; }` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-178CBA11123CCBB8 | Property | `System.Boolean RowHeadersVisible { public get; public set; }` | `System.Windows.Forms.DataGridView` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6185393C0CC285FA | Property | `System.Int32 RowHeadersWidth { public get; public set; }` | `System.Windows.Forms.DataGridView` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E361318E9094BA8 | Property | `System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode RowHeadersWidthSizeMode { public get; public set; }` | `System.Windows.Forms.DataGridView` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-183D2B20570C759C | Property | `System.Windows.Forms.DataGridViewRowCollection Rows { public get; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D7C6229EC452FBE9 | Property | `System.Windows.Forms.DataGridViewCellStyle RowsDefaultCellStyle { public get; public set; }` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6BE2AF5DD350781F | Property | `System.Windows.Forms.DataGridViewRow RowTemplate { public get; public set; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(true); DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6CE30AC7DAAD6A8C | Property | `System.Windows.Forms.ScrollBars ScrollBars { public get; public set; }` | `System.Windows.Forms.DataGridView` DefaultValueAttribute(3); LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-778DA146E8CD0CC8 | Property | `System.Windows.Forms.DataGridViewSelectedCellCollection SelectedCells { public get; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3D0299DFC998FBE0 | Property | `System.Windows.Forms.DataGridViewSelectedColumnCollection SelectedColumns { public get; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A2D74A09FA31D7D7 | Property | `System.Windows.Forms.DataGridViewSelectedRowCollection SelectedRows { public get; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BA738279B4FF26F1 | Property | `System.Windows.Forms.DataGridViewSelectionMode SelectionMode { public get; public set; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(true); DefaultValueAttribute(3) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EED874C47822A81C | Property | `System.Boolean ShowCellErrors { public get; public set; }` | `System.Windows.Forms.DataGridView` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A2491C566A1F4B9E | Property | `System.Boolean ShowCellToolTips { public get; public set; }` | `System.Windows.Forms.DataGridView` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0A6FB8250FC83192 | Property | `System.Boolean ShowEditingIcon { public get; public set; }` | `System.Windows.Forms.DataGridView` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9D4EA85720157272 | Property | `System.Boolean ShowRowErrors { public get; public set; }` | `System.Windows.Forms.DataGridView` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7B98B15BEB3C7571 | Property | `virtual System.ComponentModel.ISite Site { public get; public set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1FC37E76F7A0B21D | Property | `System.Drawing.Size Size { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-842B2D89EF3E5C48 | Property | `System.Windows.Forms.DataGridViewColumn SortedColumn { public get; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-26FCA0CCFAD6C453 | Property | `System.Windows.Forms.SortOrder SortOrder { public get; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-351B2FAD9CA35EAF | Property | `System.Boolean StandardTab { public get; public set; }` | `System.Windows.Forms.DataGridView` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5E3B0DD73847CC72 | Property | `System.Int32 TabIndex { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0174C35F4CCA03E1 | Property | `System.Boolean TabStop { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6D02843BEF4CA46D | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(false); BindableAttribute(true); DefaultValueAttribute(null); TypeConverterAttribute(System.ComponentModel.StringConverter) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FC3BD37D7BA25FF4 | Property | `virtual System.String Text { public get; public set; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false); BindableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7A8C2793A711B03F | Property | `System.Int32 Top { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2766079269688015 | Property | `System.Windows.Forms.DataGridViewHeaderCell TopLeftHeaderCell { public get; public set; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0747DDCA7A658762 | Property | `System.Windows.Forms.Control TopLevelControl { public get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E1F17403763B5A97 | Property | `System.Windows.Forms.Cursor UserSetCursor { public get; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CEE69FE799FCD13C | Property | `System.Boolean UseWaitCursor { public get; public set; }` | `System.Windows.Forms.Control` DefaultValueAttribute(false); BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-806C2B8E7323970C | Property | `System.Int32 VerticalScrollingOffset { public get; }` | `System.Windows.Forms.DataGridView` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8D426F32A00888A3 | Property | `System.Boolean VirtualMode { public get; public set; }` | `System.Windows.Forms.DataGridView` DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D2843B55FB2F5A01 | Property | `System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.Control` LocalizableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4D35EEB64C603E5B | Property | `System.Int32 Width { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-80EE021165F142A9 | Property | `System.Windows.Forms.IWindowTarget WindowTarget { public get; public set; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-01D488CDCB207D54 | Method | `protected System.Void AccessibilityNotifyClients(System.Windows.Forms.AccessibleEvents accEvent, System.Int32 objectID, System.Int32 childID)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4F09DBE808B66354 | Method | `protected internal System.Void AccessibilityNotifyClients(System.Windows.Forms.AccessibleEvents accEvent, System.Int32 childID)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-12117E394CB34CF5 | Method | `protected virtual System.Void AccessibilityNotifyCurrentCellChanged(System.Drawing.Point cellAddress)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7FFC3E5428508AA6 | Method | `protected System.Void AutoResizeColumn(System.Int32 columnIndex, System.Windows.Forms.DataGridViewAutoSizeColumnMode autoSizeColumnMode, System.Boolean fixedHeight)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-070E64C4494E31D4 | Method | `protected System.Void AutoResizeColumnHeadersHeight(System.Boolean fixedRowHeadersWidth, System.Boolean fixedColumnsWidth)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DC4CB9F7110792FE | Method | `protected System.Void AutoResizeColumnHeadersHeight(System.Int32 columnIndex, System.Boolean fixedRowHeadersWidth, System.Boolean fixedColumnWidth)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D87211726D7AE017 | Method | `protected System.Void AutoResizeColumns(System.Windows.Forms.DataGridViewAutoSizeColumnsMode autoSizeColumnsMode, System.Boolean fixedHeight)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-67EDFA73A2887887 | Method | `protected System.Void AutoResizeRow(System.Int32 rowIndex, System.Windows.Forms.DataGridViewAutoSizeRowMode autoSizeRowMode, System.Boolean fixedWidth)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CED8DA0ABD1B0448 | Method | `protected System.Void AutoResizeRowHeadersWidth(System.Int32 rowIndex, System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode rowHeadersWidthSizeMode, System.Boolean fixedColumnHeadersHeight, System.Boolean fixedRowHeight)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-21CB9788D659AD31 | Method | `protected System.Void AutoResizeRowHeadersWidth(System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode rowHeadersWidthSizeMode, System.Boolean fixedColumnHeadersHeight, System.Boolean fixedRowsHeight)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-26D04D68596B4503 | Method | `protected System.Void AutoResizeRows(System.Int32 rowIndexStart, System.Int32 rowsCount, System.Windows.Forms.DataGridViewAutoSizeRowMode autoSizeRowMode, System.Boolean fixedWidth)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C10048254D933B87 | Method | `protected System.Void AutoResizeRows(System.Windows.Forms.DataGridViewAutoSizeRowsMode autoSizeRowsMode, System.Boolean fixedWidth)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-84A77E3C6C45E1E9 | Method | `protected System.Void ClearSelection(System.Int32 columnIndexException, System.Int32 rowIndexException, System.Boolean selectExceptionElement)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9055C07E991EC1A7 | Method | `protected virtual System.Windows.Forms.AccessibleObject CreateAccessibilityInstance()` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29C2AA7361469233 | Method | `protected virtual System.Windows.Forms.DataGridViewColumnCollection CreateColumnsInstance()` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B2B81247AED75AE | Method | `protected virtual System.Windows.Forms.Control+ControlCollection CreateControlsInstance()` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F0A3C5203C50AAC7 | Method | `protected virtual System.Void CreateHandle()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E78D794846F2DAAF | Method | `protected virtual System.Windows.Forms.DataGridViewRowCollection CreateRowsInstance()` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0FC10FBA30D92ACE | Method | `protected virtual System.Void DefWndProc(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-25629653BF4B684A | Method | `protected virtual System.Void DestroyHandle()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EAF3985D2717EAE3 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CF3959C347F131E4 | Method | `protected virtual System.Void Finalize()` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8570C1ADE90BC183 | Method | `protected virtual System.Windows.Forms.AccessibleObject GetAccessibilityObjectById(System.Int32 objectId)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2F7E982E5CDF6679 | Method | `protected System.Windows.Forms.AutoSizeMode GetAutoSizeMode()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C3E4972F1C37DFFB | Method | `protected virtual System.Drawing.Rectangle GetScaledBounds(System.Drawing.Rectangle bounds, System.Drawing.SizeF factor, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BAEE9BAA5A6F2812 | Method | `protected virtual System.Object GetService(System.Type service)` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5033E4266B8A5165 | Method | `protected System.Boolean GetStyle(System.Windows.Forms.ControlStyles flag)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-04B25F81C6FC51ED | Method | `protected System.Boolean GetTopLevel()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0FF5E50A7BA1327D | Method | `protected virtual System.Void InitLayout()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E01996895DF72E01 | Method | `protected System.Void InvokeGotFocus(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B32401F917D277B | Method | `protected System.Void InvokeLostFocus(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5C384F73D1F617E1 | Method | `protected System.Void InvokeOnClick(System.Windows.Forms.Control toInvoke, System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BAAD9D422B498C92 | Method | `protected System.Void InvokePaint(System.Windows.Forms.Control c, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-084F497D6842A8D6 | Method | `protected System.Void InvokePaintBackground(System.Windows.Forms.Control c, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3182BB719577D39E | Method | `protected virtual System.Boolean IsInputChar(System.Char charCode)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-099EC0A200E60509 | Method | `protected virtual System.Boolean IsInputKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F8494DC6EB2DE055 | Method | `protected virtual System.Void NotifyInvalidate(System.Drawing.Rectangle invalidatedArea)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-96B5EFB877609EB9 | Method | `protected virtual System.Void OnAllowUserToAddRowsChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BBBFB3CE4238BDAE | Method | `protected virtual System.Void OnAllowUserToDeleteRowsChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5E6255BC4380F24C | Method | `protected virtual System.Void OnAllowUserToOrderColumnsChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E360ECDE9E429D9 | Method | `protected virtual System.Void OnAllowUserToResizeColumnsChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-274FE54078591A21 | Method | `protected virtual System.Void OnAllowUserToResizeRowsChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2ED0DDD688DABDC7 | Method | `protected virtual System.Void OnAlternatingRowsDefaultCellStyleChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B992A49788F421DD | Method | `protected virtual System.Void OnAutoGenerateColumnsChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6832388EED85CAC8 | Method | `protected virtual System.Void OnAutoSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E6FD82DF6AC6377 | Method | `protected virtual System.Void OnAutoSizeColumnModeChanged(System.Windows.Forms.DataGridViewAutoSizeColumnModeEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0DA77102D7AEE606 | Method | `protected virtual System.Void OnAutoSizeColumnsModeChanged(System.Windows.Forms.DataGridViewAutoSizeColumnsModeEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-68A40348D122567F | Method | `protected virtual System.Void OnAutoSizeRowsModeChanged(System.Windows.Forms.DataGridViewAutoSizeModeEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BF3877F36AC7A2A6 | Method | `protected virtual System.Void OnBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4ABF4E4FA5AE7537 | Method | `protected virtual System.Void OnBackgroundColorChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-51C1BE3B788ECC77 | Method | `protected virtual System.Void OnBackgroundImageChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0D2FA6D440EAA06F | Method | `protected virtual System.Void OnBackgroundImageLayoutChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1C89C03E9A69D508 | Method | `protected virtual System.Void OnBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E2B54A6F60E95665 | Method | `protected virtual System.Void OnBorderStyleChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-278D412C86CE3EFD | Method | `protected virtual System.Void OnCancelRowEdit(System.Windows.Forms.QuestionEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7904656F09DB6BDF | Method | `protected virtual System.Void OnCausesValidationChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EE6E81D1A1738E99 | Method | `protected virtual System.Void OnCellBeginEdit(System.Windows.Forms.DataGridViewCellCancelEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C8DFC2A32AC09F51 | Method | `protected virtual System.Void OnCellBorderStyleChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-18C0EEF6A1066F79 | Method | `protected virtual System.Void OnCellClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B2FCF199447D011 | Method | `protected virtual System.Void OnCellContentClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A228E0E61510E448 | Method | `protected virtual System.Void OnCellContentDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F3E47D82B5E4A2F4 | Method | `protected virtual System.Void OnCellContextMenuStripChanged(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9CD0AC18204E6EEA | Method | `protected virtual System.Void OnCellContextMenuStripNeeded(System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8801A98FDFB1B024 | Method | `protected virtual System.Void OnCellDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CA98999CD3645E1F | Method | `protected virtual System.Void OnCellEndEdit(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FFEDADB664F72AF7 | Method | `protected virtual System.Void OnCellEnter(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-165C7FDCFC9FD444 | Method | `protected virtual System.Void OnCellErrorTextChanged(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7103F8ADD9CEB836 | Method | `protected virtual System.Void OnCellErrorTextNeeded(System.Windows.Forms.DataGridViewCellErrorTextNeededEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-92378D16A0FA3B0C | Method | `protected virtual System.Void OnCellFormatting(System.Windows.Forms.DataGridViewCellFormattingEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5DF8685F22D9E058 | Method | `protected virtual System.Void OnCellLeave(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B6EB7E0618B085DA | Method | `protected virtual System.Void OnCellMouseClick(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EF5590531E01169B | Method | `protected virtual System.Void OnCellMouseDoubleClick(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-58731B24C9828B0C | Method | `protected virtual System.Void OnCellMouseDown(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-251412F2D388E8CF | Method | `protected virtual System.Void OnCellMouseEnter(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6B91D9C0F6B93E5A | Method | `protected virtual System.Void OnCellMouseLeave(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D9C18DB3787E61F4 | Method | `protected virtual System.Void OnCellMouseMove(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F2990BA5EE0CAFAC | Method | `protected virtual System.Void OnCellMouseUp(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B0C5E732516838F7 | Method | `protected internal virtual System.Void OnCellPainting(System.Windows.Forms.DataGridViewCellPaintingEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-86E4A7B6BF8636F4 | Method | `protected virtual System.Void OnCellParsing(System.Windows.Forms.DataGridViewCellParsingEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1674F77C3BA0506E | Method | `protected virtual System.Void OnCellStateChanged(System.Windows.Forms.DataGridViewCellStateChangedEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D032C04CA248F159 | Method | `protected virtual System.Void OnCellStyleChanged(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3A68498164009986 | Method | `protected virtual System.Void OnCellStyleContentChanged(System.Windows.Forms.DataGridViewCellStyleContentChangedEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C44DC47DA6A37E83 | Method | `protected virtual System.Void OnCellToolTipTextChanged(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0CBA6888B9613366 | Method | `protected virtual System.Void OnCellToolTipTextNeeded(System.Windows.Forms.DataGridViewCellToolTipTextNeededEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-369A5690274CC8E0 | Method | `protected virtual System.Void OnCellValidated(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E36FBF2871236955 | Method | `protected virtual System.Void OnCellValidating(System.Windows.Forms.DataGridViewCellValidatingEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BCD78F967095BF90 | Method | `protected virtual System.Void OnCellValueChanged(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6AC784E6363D687E | Method | `protected virtual System.Void OnCellValueNeeded(System.Windows.Forms.DataGridViewCellValueEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3517E9F648ADC0AB | Method | `protected virtual System.Void OnCellValuePushed(System.Windows.Forms.DataGridViewCellValueEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7C8AF010B6DA0FF2 | Method | `protected virtual System.Void OnChangeUICues(System.Windows.Forms.UICuesEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FEF527564FFE3FC0 | Method | `protected virtual System.Void OnClick(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-85EAF0E6E31344C2 | Method | `protected virtual System.Void OnClientSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8066357557A4DF84 | Method | `protected virtual System.Void OnColumnAdded(System.Windows.Forms.DataGridViewColumnEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-709B46727A9BC056 | Method | `protected virtual System.Void OnColumnContextMenuStripChanged(System.Windows.Forms.DataGridViewColumnEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BBBCD73BF19B3F34 | Method | `protected virtual System.Void OnColumnDataPropertyNameChanged(System.Windows.Forms.DataGridViewColumnEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D315B675C2DE3E8E | Method | `protected virtual System.Void OnColumnDefaultCellStyleChanged(System.Windows.Forms.DataGridViewColumnEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-25DCCE32426C493D | Method | `protected virtual System.Void OnColumnDisplayIndexChanged(System.Windows.Forms.DataGridViewColumnEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5357364EA63E3EDA | Method | `protected virtual System.Void OnColumnDividerDoubleClick(System.Windows.Forms.DataGridViewColumnDividerDoubleClickEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-87F784D77E2B7E98 | Method | `protected virtual System.Void OnColumnDividerWidthChanged(System.Windows.Forms.DataGridViewColumnEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-482EE67432D762D2 | Method | `protected virtual System.Void OnColumnHeaderCellChanged(System.Windows.Forms.DataGridViewColumnEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B20B122506FA4B62 | Method | `protected virtual System.Void OnColumnHeaderMouseClick(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-625B24678BE36904 | Method | `protected virtual System.Void OnColumnHeaderMouseDoubleClick(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D50B10C5CEDE032E | Method | `protected virtual System.Void OnColumnHeadersBorderStyleChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A4CB9398C8997B0 | Method | `protected virtual System.Void OnColumnHeadersDefaultCellStyleChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-313358F53516F877 | Method | `protected virtual System.Void OnColumnHeadersHeightChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-85F9917839D989D7 | Method | `protected virtual System.Void OnColumnHeadersHeightSizeModeChanged(System.Windows.Forms.DataGridViewAutoSizeModeEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-350ECCD0E903C304 | Method | `protected virtual System.Void OnColumnMinimumWidthChanged(System.Windows.Forms.DataGridViewColumnEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0FF4F0DFA3639398 | Method | `protected virtual System.Void OnColumnNameChanged(System.Windows.Forms.DataGridViewColumnEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-34DB2CDA5AABE4BD | Method | `protected virtual System.Void OnColumnRemoved(System.Windows.Forms.DataGridViewColumnEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0E6FC06824F9B6EC | Method | `protected virtual System.Void OnColumnSortModeChanged(System.Windows.Forms.DataGridViewColumnEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D7C5F279A80E4A9F | Method | `protected virtual System.Void OnColumnStateChanged(System.Windows.Forms.DataGridViewColumnStateChangedEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-205A6234F0B542FA | Method | `protected virtual System.Void OnColumnToolTipTextChanged(System.Windows.Forms.DataGridViewColumnEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8943FB05743980C4 | Method | `protected virtual System.Void OnColumnWidthChanged(System.Windows.Forms.DataGridViewColumnEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3900A3BF7913FDFE | Method | `protected virtual System.Void OnContextMenuChanged(System.EventArgs e)` | `System.Windows.Forms.Control` ObsoleteAttribute("\u0060ContextMenu\u0060 is provided for binary compatibility with .NET Framework and is not intended to be used directly from your code. Use \u0060ContextMenuStrip\u0060 instead.",false); BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-82D10ED2BBC293CA | Method | `protected virtual System.Void OnContextMenuStripChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FA3EE4F923A91AE1 | Method | `protected virtual System.Void OnControlAdded(System.Windows.Forms.ControlEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D7032C8CF1623DDA | Method | `protected virtual System.Void OnControlRemoved(System.Windows.Forms.ControlEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-66A84601582112AC | Method | `protected virtual System.Void OnCreateControl()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-050F0D6EDE9FEFC1 | Method | `protected virtual System.Void OnCurrentCellChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-98642ABF9C188264 | Method | `protected virtual System.Void OnCurrentCellDirtyStateChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-038246F70FDF40DE | Method | `protected virtual System.Void OnCursorChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C57842B0E7D00EC1 | Method | `protected virtual System.Void OnDataBindingComplete(System.Windows.Forms.DataGridViewBindingCompleteEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-57D90C3705ECF263 | Method | `protected virtual System.Void OnDataContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EA4D373833E7B022 | Method | `protected virtual System.Void OnDataError(System.Boolean displayErrorDialogIfNoHandler, System.Windows.Forms.DataGridViewDataErrorEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B9ADF817D148B58 | Method | `protected virtual System.Void OnDataMemberChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4997DA2A663FD90D | Method | `protected virtual System.Void OnDataSourceChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2349959F741E12D2 | Method | `protected virtual System.Void OnDefaultCellStyleChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-30E098A551CEA1F7 | Method | `protected virtual System.Void OnDefaultValuesNeeded(System.Windows.Forms.DataGridViewRowEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7A6167E64754BFC4 | Method | `protected virtual System.Void OnDockChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AF6FF70EEE06AB70 | Method | `protected virtual System.Void OnDoubleClick(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B7A1B45429A0D575 | Method | `protected virtual System.Void OnDpiChangedAfterParent(System.EventArgs e)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F00F79D550E602E4 | Method | `protected virtual System.Void OnDpiChangedBeforeParent(System.EventArgs e)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8E41B7CFE3AF1F2B | Method | `protected virtual System.Void OnDragDrop(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3398AF70B3B1CBB5 | Method | `protected virtual System.Void OnDragEnter(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B3F1FD84F723722C | Method | `protected virtual System.Void OnDragLeave(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EE2BB6AFB1B971CC | Method | `protected virtual System.Void OnDragOver(System.Windows.Forms.DragEventArgs drgevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F76BADD626A52484 | Method | `protected virtual System.Void OnEditingControlShowing(System.Windows.Forms.DataGridViewEditingControlShowingEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-358D33A4C52953D9 | Method | `protected virtual System.Void OnEditModeChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-00161C32EE931264 | Method | `protected virtual System.Void OnEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-83E4DA60AEE8D823 | Method | `protected internal virtual System.Void OnEnter(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A7AF3B22BED23876 | Method | `protected virtual System.Void OnFontChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CA3ED0125B42CE9C | Method | `protected virtual System.Void OnForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2EF223EF8FC6F8A3 | Method | `protected virtual System.Void OnGiveFeedback(System.Windows.Forms.GiveFeedbackEventArgs gfbevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-39C0414AF1952BAF | Method | `protected virtual System.Void OnGotFocus(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1AE22DBBB2FC7201 | Method | `protected virtual System.Void OnGridColorChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1A9A97DFE4B68134 | Method | `protected virtual System.Void OnHandleCreated(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-580F1688D025287F | Method | `protected virtual System.Void OnHandleDestroyed(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-074B30C1B601DD9F | Method | `protected virtual System.Void OnHelpRequested(System.Windows.Forms.HelpEventArgs hevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6F6F0E41FFA14200 | Method | `protected virtual System.Void OnImeModeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8E92F25992BBE909 | Method | `protected virtual System.Void OnInvalidated(System.Windows.Forms.InvalidateEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BCA3667201444E0F | Method | `protected virtual System.Void OnKeyDown(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EA96634C4A175CB6 | Method | `protected virtual System.Void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6EF8D2DCC776E64E | Method | `protected virtual System.Void OnKeyUp(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E6F6F088F37F81B | Method | `protected virtual System.Void OnLayout(System.Windows.Forms.LayoutEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AAAE36853450D89D | Method | `protected internal virtual System.Void OnLeave(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-458225DB8C8BC117 | Method | `protected virtual System.Void OnLocationChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A29DCC90C863F18A | Method | `protected virtual System.Void OnLostFocus(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-45E95B5F7B1474DF | Method | `protected virtual System.Void OnMarginChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-82D12C223EAF8E17 | Method | `protected virtual System.Void OnMouseCaptureChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-484F371D49F049FC | Method | `protected virtual System.Void OnMouseClick(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C7E2BF3753997A01 | Method | `protected virtual System.Void OnMouseDoubleClick(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B825BA253E29D622 | Method | `protected virtual System.Void OnMouseDown(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C024CF6A2CDCA436 | Method | `protected virtual System.Void OnMouseEnter(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B478B2C994147906 | Method | `protected virtual System.Void OnMouseHover(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E45D7423B389E0D9 | Method | `protected virtual System.Void OnMouseLeave(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-984A1E4A81B0E942 | Method | `protected virtual System.Void OnMouseMove(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4DD9FA1FAB84795A | Method | `protected virtual System.Void OnMouseUp(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D9E2607CCEDD79E1 | Method | `protected virtual System.Void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1D96AC806C4FF7A3 | Method | `protected virtual System.Void OnMove(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F620C605C15204A7 | Method | `protected virtual System.Void OnMultiSelectChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1A547204D1D5AA33 | Method | `protected virtual System.Void OnNewRowNeeded(System.Windows.Forms.DataGridViewRowEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F080024B6721AB5C | Method | `protected virtual System.Void OnNotifyMessage(System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8E8B17AC5536B453 | Method | `protected virtual System.Void OnPaddingChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7D84607F75CBB22C | Method | `protected virtual System.Void OnPaint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CB7F5A4628AA7C5E | Method | `protected virtual System.Void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-052FC02610425920 | Method | `protected virtual System.Void OnParentBackColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2430E7EC58030540 | Method | `protected virtual System.Void OnParentBackgroundImageChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4A182359E7E893EB | Method | `protected virtual System.Void OnParentBindingContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4A026C57A028C072 | Method | `protected virtual System.Void OnParentChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A859DB70C569D51F | Method | `protected virtual System.Void OnParentCursorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50803EB5617ECA0D | Method | `protected virtual System.Void OnParentDataContextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C43FC3D4B8F78A43 | Method | `protected virtual System.Void OnParentEnabledChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6FC4C67E5FF4B64F | Method | `protected virtual System.Void OnParentFontChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F9134628FAFB7DA1 | Method | `protected virtual System.Void OnParentForeColorChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC3371A1CFC1FD26 | Method | `protected virtual System.Void OnParentRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B335D3EEF243D420 | Method | `protected virtual System.Void OnParentVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BBD5AFA9C8F8E8A0 | Method | `protected virtual System.Void OnPreviewKeyDown(System.Windows.Forms.PreviewKeyDownEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8A8161AF8F30617D | Method | `protected virtual System.Void OnPrint(System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A20B6E5AF54B1C36 | Method | `protected virtual System.Void OnQueryContinueDrag(System.Windows.Forms.QueryContinueDragEventArgs qcdevent)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-983747FE96E5BE45 | Method | `protected virtual System.Void OnReadOnlyChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6FDB00CF4677AECE | Method | `protected virtual System.Void OnRegionChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-84C5423325485496 | Method | `protected virtual System.Void OnResize(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E89707839FE802D | Method | `protected virtual System.Void OnRightToLeftChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-238F5CEF1C43354D | Method | `protected virtual System.Void OnRowContextMenuStripChanged(System.Windows.Forms.DataGridViewRowEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2406563C4D9E00AB | Method | `protected virtual System.Void OnRowContextMenuStripNeeded(System.Windows.Forms.DataGridViewRowContextMenuStripNeededEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6FC385DBFAD0B1A2 | Method | `protected virtual System.Void OnRowDefaultCellStyleChanged(System.Windows.Forms.DataGridViewRowEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A0CC55E79E9999C2 | Method | `protected virtual System.Void OnRowDirtyStateNeeded(System.Windows.Forms.QuestionEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E34CB571101607B | Method | `protected virtual System.Void OnRowDividerDoubleClick(System.Windows.Forms.DataGridViewRowDividerDoubleClickEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5A6F3071CB5F0D05 | Method | `protected virtual System.Void OnRowDividerHeightChanged(System.Windows.Forms.DataGridViewRowEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B7A84AF91ACEE8BD | Method | `protected virtual System.Void OnRowEnter(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4B0220FE60E3CFEE | Method | `protected virtual System.Void OnRowErrorTextChanged(System.Windows.Forms.DataGridViewRowEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2371EA256C575300 | Method | `protected virtual System.Void OnRowErrorTextNeeded(System.Windows.Forms.DataGridViewRowErrorTextNeededEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ED8F74BDEAB6B838 | Method | `protected virtual System.Void OnRowHeaderCellChanged(System.Windows.Forms.DataGridViewRowEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-238C5CB12FF62D5D | Method | `protected virtual System.Void OnRowHeaderMouseClick(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F49C6D48B359FC2C | Method | `protected virtual System.Void OnRowHeaderMouseDoubleClick(System.Windows.Forms.DataGridViewCellMouseEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A0C20A173F65D43F | Method | `protected virtual System.Void OnRowHeadersBorderStyleChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1FA49B5F26FEC86E | Method | `protected virtual System.Void OnRowHeadersDefaultCellStyleChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-378CA32180AE0C8E | Method | `protected virtual System.Void OnRowHeadersWidthChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-32B63D4C4A92C8AA | Method | `protected virtual System.Void OnRowHeadersWidthSizeModeChanged(System.Windows.Forms.DataGridViewAutoSizeModeEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5F9F37610F114CEF | Method | `protected virtual System.Void OnRowHeightChanged(System.Windows.Forms.DataGridViewRowEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8656BD07F33CF2B7 | Method | `protected virtual System.Void OnRowHeightInfoNeeded(System.Windows.Forms.DataGridViewRowHeightInfoNeededEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-383B3C7527FE984E | Method | `protected virtual System.Void OnRowHeightInfoPushed(System.Windows.Forms.DataGridViewRowHeightInfoPushedEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9062FC30E390F0C7 | Method | `protected virtual System.Void OnRowLeave(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1DD170ED10EC6CD3 | Method | `protected virtual System.Void OnRowMinimumHeightChanged(System.Windows.Forms.DataGridViewRowEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FDA704CD7C1B963F | Method | `protected internal virtual System.Void OnRowPostPaint(System.Windows.Forms.DataGridViewRowPostPaintEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3704F377CAF8B3FD | Method | `protected internal virtual System.Void OnRowPrePaint(System.Windows.Forms.DataGridViewRowPrePaintEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CB216D4A643ED5A4 | Method | `protected virtual System.Void OnRowsAdded(System.Windows.Forms.DataGridViewRowsAddedEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C571D9690C7C04D8 | Method | `protected virtual System.Void OnRowsDefaultCellStyleChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50515C36F7662402 | Method | `protected virtual System.Void OnRowsRemoved(System.Windows.Forms.DataGridViewRowsRemovedEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8B69DB4EDBC5E9B0 | Method | `protected virtual System.Void OnRowStateChanged(System.Int32 rowIndex, System.Windows.Forms.DataGridViewRowStateChangedEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B7E09B717F9A5ED | Method | `protected virtual System.Void OnRowUnshared(System.Windows.Forms.DataGridViewRowEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F26C42A44A4DEC23 | Method | `protected virtual System.Void OnRowValidated(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0C01B56D921990BF | Method | `protected virtual System.Void OnRowValidating(System.Windows.Forms.DataGridViewCellCancelEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FEC3DA79AD3A853C | Method | `protected virtual System.Void OnScroll(System.Windows.Forms.ScrollEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C501DC62548EE367 | Method | `protected virtual System.Void OnSelectionChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8141B319AFC4E472 | Method | `protected virtual System.Void OnSizeChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-49231822CF157DDE | Method | `protected virtual System.Void OnSortCompare(System.Windows.Forms.DataGridViewSortCompareEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-21EE3666B8F4E525 | Method | `protected virtual System.Void OnSorted(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-478711F37AC011A9 | Method | `protected virtual System.Void OnStyleChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D67233136F0B4277 | Method | `protected virtual System.Void OnSystemColorsChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-541EE299BA66651C | Method | `protected virtual System.Void OnTabIndexChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-78D1C5DE9DF1BE52 | Method | `protected virtual System.Void OnTabStopChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F71C9A63A2D7CD0F | Method | `protected virtual System.Void OnTextChanged(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2D6B438D7E4D512D | Method | `protected virtual System.Void OnUserAddedRow(System.Windows.Forms.DataGridViewRowEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-881B92C93B3A1F20 | Method | `protected virtual System.Void OnUserDeletedRow(System.Windows.Forms.DataGridViewRowEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2745533878DFA375 | Method | `protected virtual System.Void OnUserDeletingRow(System.Windows.Forms.DataGridViewRowCancelEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E4CE2B232164A162 | Method | `protected virtual System.Void OnValidated(System.EventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D503DE4B5B07AA0E | Method | `protected virtual System.Void OnValidating(System.ComponentModel.CancelEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-546F8AF03DCDD9F9 | Method | `protected virtual System.Void OnVisibleChanged(System.EventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4B639F94EE714C23 | Method | `protected virtual System.Void PaintBackground(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle gridBounds)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-80CDB0FF0CE41913 | Method | `protected System.Boolean ProcessAKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-64F219828B04EDE0 | Method | `protected virtual System.Boolean ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50B653D5AA583589 | Method | `protected System.Boolean ProcessControlShiftF10Keys(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8119B73DFC9905EE | Method | `protected virtual System.Boolean ProcessDataGridViewKey(System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8F6C5FC76F8D7D2A | Method | `protected System.Boolean ProcessDeleteKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-805674AFA01152ED | Method | `protected virtual System.Boolean ProcessDialogChar(System.Char charCode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-232A152B765B97A1 | Method | `protected virtual System.Boolean ProcessDialogKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5B31D58D7575A015 | Method | `protected System.Boolean ProcessDownKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-64EB2FB2BEBC7E92 | Method | `protected System.Boolean ProcessEndKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-95DEB647B62FC269 | Method | `protected System.Boolean ProcessEnterKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E65994B877CBAE5 | Method | `protected System.Boolean ProcessEscapeKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B4645EB26045F2F7 | Method | `protected System.Boolean ProcessF2Key(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3F389439838EB5A9 | Method | `protected System.Boolean ProcessF3Key(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-164437C3D49F8CE6 | Method | `protected System.Boolean ProcessHomeKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FBE56E942469F128 | Method | `protected System.Boolean ProcessInsertKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-277D8A66A559B0E7 | Method | `protected virtual System.Boolean ProcessKeyEventArgs(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ADBE50B33A258B19 | Method | `protected internal virtual System.Boolean ProcessKeyMessage(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-893AA0B871509BCA | Method | `protected virtual System.Boolean ProcessKeyPreview(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A90787D9BE83C987 | Method | `protected System.Boolean ProcessLeftKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-64CDDFF642995551 | Method | `protected internal virtual System.Boolean ProcessMnemonic(System.Char charCode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D72557BA21FDA391 | Method | `protected System.Boolean ProcessNextKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7CBB8D247884F38F | Method | `protected System.Boolean ProcessPriorKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8CE16FC4A6F6EE63 | Method | `protected System.Boolean ProcessRightKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DD8ED3B08C0E919F | Method | `protected System.Boolean ProcessSpaceKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3CDE83612A1310F8 | Method | `protected System.Boolean ProcessTabKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A6A6793050C463BA | Method | `protected System.Boolean ProcessUpKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DB2DE816A6822828 | Method | `protected System.Boolean ProcessZeroKey(System.Windows.Forms.Keys keyData)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-17B6FC1301D084A5 | Method | `protected System.Void RaiseDragEvent(System.Object key, System.Windows.Forms.DragEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-71F9198453A3E153 | Method | `protected System.Void RaiseKeyEvent(System.Object key, System.Windows.Forms.KeyEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-56B95B26A60A670F | Method | `protected System.Void RaiseMouseEvent(System.Object key, System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F0E1E3E1356DDA0F | Method | `protected System.Void RaisePaintEvent(System.Object key, System.Windows.Forms.PaintEventArgs e)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8F688101D045B523 | Method | `protected System.Void RecreateHandle()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B2F18D11D1C15EBF | Method | `protected static System.Boolean ReflectMessage(System.IntPtr hWnd, ref System.Windows.Forms.Message m)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3766BD7F2990B6E4 | Method | `protected virtual System.Void RescaleConstantsForDpi(System.Int32 deviceDpiOld, System.Int32 deviceDpiNew)` | `System.Windows.Forms.Control` BrowsableAttribute(true) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C9FB0AD0AE8A53AF | Method | `protected System.Void ResetMouseEventArgs()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-21132B2600D2A29C | Method | `protected System.Drawing.ContentAlignment RtlTranslateAlignment(System.Drawing.ContentAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50EB5A3FBD342A21 | Method | `protected System.Windows.Forms.HorizontalAlignment RtlTranslateAlignment(System.Windows.Forms.HorizontalAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0894851BE034F3E9 | Method | `protected System.Windows.Forms.LeftRightAlignment RtlTranslateAlignment(System.Windows.Forms.LeftRightAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B2F8902BEA9A57D | Method | `protected internal System.Drawing.ContentAlignment RtlTranslateContent(System.Drawing.ContentAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-86AB430A17B3F92B | Method | `protected System.Windows.Forms.HorizontalAlignment RtlTranslateHorizontal(System.Windows.Forms.HorizontalAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FFF60597D3014B8B | Method | `protected System.Windows.Forms.LeftRightAlignment RtlTranslateLeftRight(System.Windows.Forms.LeftRightAlignment align)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A591C2FEF0362DA7 | Method | `protected virtual System.Void ScaleControl(System.Drawing.SizeF factor, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-72BFD75C1C9F23FF | Method | `protected virtual System.Void ScaleCore(System.Single dx, System.Single dy)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F3F7AC6DF9238682 | Method | `protected virtual System.Void Select(System.Boolean directed, System.Boolean forward)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-06F6F2484BED2C15 | Method | `protected System.Void SetAutoSizeMode(System.Windows.Forms.AutoSizeMode mode)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EA0984F7A5A62C35 | Method | `protected virtual System.Void SetBoundsCore(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Windows.Forms.BoundsSpecified specified)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D99E033AD03F21CD | Method | `protected virtual System.Void SetClientSizeCore(System.Int32 x, System.Int32 y)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E53EC471AAFAA8B | Method | `protected virtual System.Boolean SetCurrentCellAddressCore(System.Int32 columnIndex, System.Int32 rowIndex, System.Boolean setAnchorCellAddress, System.Boolean validateCurrentCell, System.Boolean throughMouseClick)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F78E0D1D4971708C | Method | `protected virtual System.Void SetSelectedCellCore(System.Int32 columnIndex, System.Int32 rowIndex, System.Boolean selected)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-174915E506B1E61C | Method | `protected virtual System.Void SetSelectedColumnCore(System.Int32 columnIndex, System.Boolean selected)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-14330FBDC06746DD | Method | `protected virtual System.Void SetSelectedRowCore(System.Int32 rowIndex, System.Boolean selected)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-475EC86F20645437 | Method | `protected System.Void SetStyle(System.Windows.Forms.ControlStyles flag, System.Boolean value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B4BAF98BE0C4859 | Method | `protected System.Void SetTopLevel(System.Boolean value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-69ED2AF6B033F8A8 | Method | `protected virtual System.Void SetVisibleCore(System.Boolean value)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B59A59CD37C8D895 | Method | `protected virtual System.Drawing.Size SizeFromClientSize(System.Drawing.Size clientSize)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5CECC3CF40F0CFCC | Method | `protected System.Void UpdateBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-76F3842A5A98C114 | Method | `protected System.Void UpdateBounds(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Int32 clientWidth, System.Int32 clientHeight)` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1B052DA6E560F829 | Method | `protected internal System.Void UpdateBounds()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7A1A1E4C1BB086C9 | Method | `protected System.Void UpdateStyles()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-47B2E0E1175F812C | Method | `protected System.Void UpdateZOrder()` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-624B460FA183F05D | Method | `protected virtual System.Void WndProc(ref System.Windows.Forms.Message m)` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8E01360325E10A61 | Property | `virtual System.Boolean CanEnableIme { protected get; }` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-543BD2DB5B30918B | Property | `virtual System.Boolean CanRaiseEvents { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-651227EB418D408C | Property | `virtual System.Windows.Forms.CreateParams CreateParams { protected get; }` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CA24B17BCA63A7CC | Property | `virtual System.Windows.Forms.Cursor DefaultCursor { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-72E84C2D0CC61D42 | Property | `virtual System.Windows.Forms.ImeMode DefaultImeMode { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4E9CD2FF8B508610 | Property | `virtual System.Windows.Forms.Padding DefaultMargin { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1C895673EE0B6416 | Property | `virtual System.Drawing.Size DefaultMaximumSize { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-015FA282B6D6E1CF | Property | `virtual System.Drawing.Size DefaultMinimumSize { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-044A47A671BFA343 | Property | `virtual System.Windows.Forms.Padding DefaultPadding { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7EA6B44C4E588A10 | Property | `virtual System.Drawing.Size DefaultSize { protected get; }` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-514F60685ED5A359 | Property | `System.Boolean DesignMode { protected get; }` | `System.ComponentModel.Component` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0ABA597EEF79C17E | Property | `virtual System.Boolean DoubleBuffered { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-06CC63D6652D001C | Property | `System.ComponentModel.EventHandlerList Events { protected get; }` | `System.ComponentModel.Component`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AEA142865072977C | Property | `System.Int32 FontHeight { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-69C6383FB104154D | Property | `System.Windows.Forms.ScrollBar HorizontalScrollBar { protected get; }` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-418B95166548E2D1 | Property | `virtual System.Windows.Forms.ImeMode ImeModeBase { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4294416287DBEB3D | Property | `static System.Windows.Forms.ImeMode PropagatingImeMode { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3FBE5D8283988436 | Property | `System.Boolean RenderRightToLeft { protected internal get; }` | `System.Windows.Forms.Control` ObsoleteAttribute("This property has been deprecated. Please use RightToLeft instead. https://go.microsoft.com/fwlink/?linkid=14202") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CF7F281FF0A8D5C7 | Property | `System.Boolean ResizeRedraw { protected get; protected set; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AD3A47A23C92200B | Property | `virtual System.Boolean ScaleChildren { protected get; }` | `System.Windows.Forms.Control`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-94C4772FFC0CB31E | Property | `virtual System.Boolean ShowFocusCues { protected internal get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29F16580C3FFB7DF | Property | `virtual System.Boolean ShowKeyboardCues { protected internal get; }` | `System.Windows.Forms.Control` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-40AEF94E6FB15972 | Property | `System.Windows.Forms.ScrollBar VerticalScrollBar { protected get; }` | `System.Windows.Forms.DataGridView`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
