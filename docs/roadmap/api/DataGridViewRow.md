# DataGridViewRow：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/DataGridViewRow.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.DataGridViewRow`；基类：`System.Windows.Forms.DataGridViewBand`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **24**，公开事件 **0**，公开方法 **11**；受保护扩展成员 **17**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-0DDB5130F109E7D5 | Constructor | `public DataGridViewRow()` | `System.Windows.Forms.DataGridViewRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B408177E0E38C5B | Method | `public virtual System.Windows.Forms.DataGridViewAdvancedBorderStyle AdjustRowHeaderBorderStyle(System.Windows.Forms.DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyleInput, System.Windows.Forms.DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder, System.Boolean singleVerticalBorderAdded, System.Boolean singleHorizontalBorderAdded, System.Boolean isFirstDisplayedRow, System.Boolean isLastVisibleRow)` | `System.Windows.Forms.DataGridViewRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-21E395D312A87DC1 | Method | `public virtual System.Object Clone()` | `System.Windows.Forms.DataGridViewRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7E70097AF32C00A3 | Method | `public System.Void CreateCells(System.Windows.Forms.DataGridView dataGridView)` | `System.Windows.Forms.DataGridViewRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-183C98CDD1C135AD | Method | `public System.Void CreateCells(System.Windows.Forms.DataGridView dataGridView, System.Object[] values)` | `System.Windows.Forms.DataGridViewRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FFC14B0CAEBE7E02 | Method | `public System.Void Dispose()` | `System.Windows.Forms.DataGridViewBand`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-60F7DCC6E63D4C98 | Method | `public System.Windows.Forms.ContextMenuStrip GetContextMenuStrip(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-67DC0AAD7272AD9B | Method | `public System.String GetErrorText(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F90B7B5F9E87A2FD | Method | `public virtual System.Int32 GetPreferredHeight(System.Int32 rowIndex, System.Windows.Forms.DataGridViewAutoSizeRowMode autoSizeRowMode, System.Boolean fixedWidth)` | `System.Windows.Forms.DataGridViewRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D9BBA25D82FD7FEF | Method | `public virtual System.Windows.Forms.DataGridViewElementStates GetState(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-02EACF591BADC14B | Method | `public System.Boolean SetValues(System.Object[] values)` | `System.Windows.Forms.DataGridViewRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8BB38DA320CFD8CC | Method | `public virtual System.String ToString()` | `System.Windows.Forms.DataGridViewRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-984C3589E9A400E7 | Property | `System.Windows.Forms.AccessibleObject AccessibilityObject { public get; }` | `System.Windows.Forms.DataGridViewRow` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2CBCAF4E6A2EC3DF | Property | `System.Windows.Forms.DataGridViewCellCollection Cells { public get; }` | `System.Windows.Forms.DataGridViewRow` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2BADA5BF5B7EAD6A | Property | `virtual System.Windows.Forms.ContextMenuStrip ContextMenuStrip { public get; public set; }` | `System.Windows.Forms.DataGridViewRow` DefaultValueAttribute(null) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E2BE31CFC31495CB | Property | `System.Object DataBoundItem { public get; }` | `System.Windows.Forms.DataGridViewRow` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-076EFA22892674F0 | Property | `System.Windows.Forms.DataGridView DataGridView { public get; }` | `System.Windows.Forms.DataGridViewElement` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-34D5105032DAAED1 | Property | `virtual System.Windows.Forms.DataGridViewCellStyle DefaultCellStyle { public get; public set; }` | `System.Windows.Forms.DataGridViewRow` BrowsableAttribute(true); DesignerSerializationVisibilityAttribute(2) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C4652F53CDAB5A93 | Property | `System.Type DefaultHeaderCellType { public get; public set; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-25DC94C86DFB344B | Property | `virtual System.Boolean Displayed { public get; }` | `System.Windows.Forms.DataGridViewRow` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D8F4D5F48533BEE2 | Property | `System.Int32 DividerHeight { public get; public set; }` | `System.Windows.Forms.DataGridViewRow` DefaultValueAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7DBF4240B5F75F0F | Property | `System.String ErrorText { public get; public set; }` | `System.Windows.Forms.DataGridViewRow` DefaultValueAttribute("") | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4681DF35AFB34622 | Property | `virtual System.Boolean Frozen { public get; public set; }` | `System.Windows.Forms.DataGridViewRow` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-08FE916181DC4C1E | Property | `System.Boolean HasDefaultCellStyle { public get; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-94C45BAC2406F544 | Property | `System.Windows.Forms.DataGridViewRowHeaderCell HeaderCell { public get; public set; }` | `System.Windows.Forms.DataGridViewRow` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8B1292A49FA65560 | Property | `System.Int32 Height { public get; public set; }` | `System.Windows.Forms.DataGridViewRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DA7F52A16E47D993 | Property | `System.Int32 Index { public get; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C1063F18473B2968 | Property | `virtual System.Windows.Forms.DataGridViewCellStyle InheritedStyle { public get; }` | `System.Windows.Forms.DataGridViewRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-ECA671751AFB0DC7 | Property | `System.Boolean IsNewRow { public get; }` | `System.Windows.Forms.DataGridViewRow` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5028D93C089ADC4B | Property | `System.Int32 MinimumHeight { public get; public set; }` | `System.Windows.Forms.DataGridViewRow` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-438E2246640E7E6C | Property | `virtual System.Boolean ReadOnly { public get; public set; }` | `System.Windows.Forms.DataGridViewRow` BrowsableAttribute(true); DefaultValueAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EB753F209C23286A | Property | `virtual System.Windows.Forms.DataGridViewTriState Resizable { public get; public set; }` | `System.Windows.Forms.DataGridViewRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FDC827BC66C24E4A | Property | `virtual System.Boolean Selected { public get; public set; }` | `System.Windows.Forms.DataGridViewRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2A546069DDFBF146 | Property | `virtual System.Windows.Forms.DataGridViewElementStates State { public get; }` | `System.Windows.Forms.DataGridViewRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5AD8340749B60BD5 | Property | `System.Object Tag { public get; public set; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F3F742226954C575 | Property | `virtual System.Boolean Visible { public get; public set; }` | `System.Windows.Forms.DataGridViewRow` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-FB75A88AAC400C76 | Method | `protected virtual System.Windows.Forms.AccessibleObject CreateAccessibilityInstance()` | `System.Windows.Forms.DataGridViewRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-656975583BBD8E01 | Method | `protected virtual System.Windows.Forms.DataGridViewCellCollection CreateCellsInstance()` | `System.Windows.Forms.DataGridViewRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-29FC0C34932144A5 | Method | `protected virtual System.Void Dispose(System.Boolean disposing)` | `System.Windows.Forms.DataGridViewBand`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-336250DD50F4A27F | Method | `protected internal virtual System.Void DrawFocus(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle bounds, System.Int32 rowIndex, System.Windows.Forms.DataGridViewElementStates rowState, System.Windows.Forms.DataGridViewCellStyle cellStyle, System.Boolean cellsPaintSelectionBackground)` | `System.Windows.Forms.DataGridViewRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E33CA20C874C2351 | Method | `protected virtual System.Void Finalize()` | `System.Windows.Forms.DataGridViewBand`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8D2315C6224B04B8 | Method | `protected virtual System.Void OnDataGridViewChanged()` | `System.Windows.Forms.DataGridViewBand`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C20DFE3AB0D22F2E | Method | `protected internal virtual System.Void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle rowBounds, System.Int32 rowIndex, System.Windows.Forms.DataGridViewElementStates rowState, System.Boolean isFirstDisplayedRow, System.Boolean isLastVisibleRow)` | `System.Windows.Forms.DataGridViewRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6E72F729B2AF64C9 | Method | `protected internal virtual System.Void PaintCells(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle rowBounds, System.Int32 rowIndex, System.Windows.Forms.DataGridViewElementStates rowState, System.Boolean isFirstDisplayedRow, System.Boolean isLastVisibleRow, System.Windows.Forms.DataGridViewPaintParts paintParts)` | `System.Windows.Forms.DataGridViewRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BE8D1C00F498B0AB | Method | `protected internal virtual System.Void PaintHeader(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle rowBounds, System.Int32 rowIndex, System.Windows.Forms.DataGridViewElementStates rowState, System.Boolean isFirstDisplayedRow, System.Boolean isLastVisibleRow, System.Windows.Forms.DataGridViewPaintParts paintParts)` | `System.Windows.Forms.DataGridViewRow`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7B6DA9A78CB9A34F | Method | `protected System.Void RaiseCellClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-400B09F33FD8F674 | Method | `protected System.Void RaiseCellContentClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-704F3FDC6B3F90B6 | Method | `protected System.Void RaiseCellContentDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E9DD33EC05D5012 | Method | `protected System.Void RaiseCellValueChanged(System.Windows.Forms.DataGridViewCellEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E9975C42B5A9303B | Method | `protected System.Void RaiseDataError(System.Windows.Forms.DataGridViewDataErrorEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-437C9E3EC68A4C9C | Method | `protected System.Void RaiseMouseWheel(System.Windows.Forms.MouseEventArgs e)` | `System.Windows.Forms.DataGridViewElement`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AAEAE4D0134B5F79 | Property | `System.Windows.Forms.DataGridViewHeaderCell HeaderCellCore { protected get; protected set; }` | `System.Windows.Forms.DataGridViewBand` BrowsableAttribute(false); DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0310261544DDF37E | Property | `System.Boolean IsRow { protected get; }` | `System.Windows.Forms.DataGridViewBand`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
