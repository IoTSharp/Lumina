# DataGridViewRowCollection：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/DataGridViewRowCollection.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.DataGridViewRowCollection`；基类：`System.Object`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **2**，公开事件 **1**，公开方法 **30**；受保护扩展成员 **3**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-9B3CC0B86FFF1302 | Constructor | `public DataGridViewRowCollection(System.Windows.Forms.DataGridView dataGridView)` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C8D38850F7F75095 | Event | `public event System.ComponentModel.CollectionChangeEventHandler CollectionChanged` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6D97667ACB0948A7 | Method | `public virtual System.Int32 Add()` | `System.Windows.Forms.DataGridViewRowCollection` DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5A0871B757DEADFE | Method | `public virtual System.Int32 Add(System.Int32 count)` | `System.Windows.Forms.DataGridViewRowCollection` DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F942356207E826F3 | Method | `public virtual System.Int32 Add(System.Object[] values)` | `System.Windows.Forms.DataGridViewRowCollection` DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1E98DF941C91DE0E | Method | `public virtual System.Int32 Add(System.Windows.Forms.DataGridViewRow dataGridViewRow)` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8D31CA6F6294E469 | Method | `public virtual System.Int32 AddCopies(System.Int32 indexSource, System.Int32 count)` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F73105B39FA30691 | Method | `public virtual System.Int32 AddCopy(System.Int32 indexSource)` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-757D9CC5776E1ED9 | Method | `public virtual System.Void AddRange(System.Windows.Forms.DataGridViewRow[] dataGridViewRows)` | `System.Windows.Forms.DataGridViewRowCollection` DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-30032F041C8F2173 | Method | `public virtual System.Void Clear()` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EC821D324053A732 | Method | `public virtual System.Boolean Contains(System.Windows.Forms.DataGridViewRow dataGridViewRow)` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B78BE78D5CD4F199 | Method | `public System.Void CopyTo(System.Windows.Forms.DataGridViewRow[] array, System.Int32 index)` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-54B9656FAE096D91 | Method | `public System.Int32 GetFirstRow(System.Windows.Forms.DataGridViewElementStates includeFilter)` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7AD3F7C36CB3C219 | Method | `public System.Int32 GetFirstRow(System.Windows.Forms.DataGridViewElementStates includeFilter, System.Windows.Forms.DataGridViewElementStates excludeFilter)` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BA0593C333F0482A | Method | `public System.Int32 GetLastRow(System.Windows.Forms.DataGridViewElementStates includeFilter)` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-30C4D9E75A2358BE | Method | `public System.Int32 GetNextRow(System.Int32 indexStart, System.Windows.Forms.DataGridViewElementStates includeFilter)` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B0336251B9226069 | Method | `public System.Int32 GetNextRow(System.Int32 indexStart, System.Windows.Forms.DataGridViewElementStates includeFilter, System.Windows.Forms.DataGridViewElementStates excludeFilter)` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E4B61748BF6F476F | Method | `public System.Int32 GetPreviousRow(System.Int32 indexStart, System.Windows.Forms.DataGridViewElementStates includeFilter)` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9C654ECD2C376613 | Method | `public System.Int32 GetPreviousRow(System.Int32 indexStart, System.Windows.Forms.DataGridViewElementStates includeFilter, System.Windows.Forms.DataGridViewElementStates excludeFilter)` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F504D39AF86CB3ED | Method | `public System.Int32 GetRowCount(System.Windows.Forms.DataGridViewElementStates includeFilter)` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-DAC1BB5B56A9611D | Method | `public System.Int32 GetRowsHeight(System.Windows.Forms.DataGridViewElementStates includeFilter)` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-242C51F2B7A36331 | Method | `public virtual System.Windows.Forms.DataGridViewElementStates GetRowState(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C3AA4194973B180B | Method | `public System.Int32 IndexOf(System.Windows.Forms.DataGridViewRow dataGridViewRow)` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B2A75207450B69F5 | Method | `public virtual System.Void Insert(System.Int32 rowIndex, System.Int32 count)` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E45DF4A7455B5E67 | Method | `public virtual System.Void Insert(System.Int32 rowIndex, System.Object[] values)` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5D5C70BBD8FC8FB5 | Method | `public virtual System.Void Insert(System.Int32 rowIndex, System.Windows.Forms.DataGridViewRow dataGridViewRow)` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B76E828966F7F331 | Method | `public virtual System.Void InsertCopies(System.Int32 indexSource, System.Int32 indexDestination, System.Int32 count)` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2D54A137BF7C8032 | Method | `public virtual System.Void InsertCopy(System.Int32 indexSource, System.Int32 indexDestination)` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A7AD129EEF74B7E1 | Method | `public virtual System.Void InsertRange(System.Int32 rowIndex, System.Windows.Forms.DataGridViewRow[] dataGridViewRows)` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1A0377EB203CCF69 | Method | `public virtual System.Void Remove(System.Windows.Forms.DataGridViewRow dataGridViewRow)` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2FDDC85AAD36F100 | Method | `public virtual System.Void RemoveAt(System.Int32 index)` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2598C0C976CA4D92 | Method | `public System.Windows.Forms.DataGridViewRow SharedRow(System.Int32 rowIndex)` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-3D534E40D7F9583D | Property | `System.Int32 Count { public get; }` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-50CC0B6C7AF1B880 | Property | `System.Windows.Forms.DataGridViewRow Item[System.Int32 index] { public get; }` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-9B785F306409DADB | Method | `protected virtual System.Void OnCollectionChanged(System.ComponentModel.CollectionChangeEventArgs e)` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FD987A3A1BD8E143 | Property | `System.Windows.Forms.DataGridView DataGridView { protected get; }` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-39E31A492B08BF45 | Property | `System.Collections.ArrayList List { protected get; }` | `System.Windows.Forms.DataGridViewRowCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
