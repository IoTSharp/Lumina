# DataGridViewColumnCollection：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/DataGridViewColumnCollection.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.DataGridViewColumnCollection`；基类：`System.Windows.Forms.BaseCollection`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **6**，公开事件 **1**，公开方法 **21**；受保护扩展成员 **3**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-B7CBC7C91020745C | Constructor | `public DataGridViewColumnCollection(System.Windows.Forms.DataGridView dataGridView)` | `System.Windows.Forms.DataGridViewColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B6A00770FAB2917 | Event | `public event System.ComponentModel.CollectionChangeEventHandler CollectionChanged` | `System.Windows.Forms.DataGridViewColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-096891CC17C07753 | Method | `public virtual System.Int32 Add(System.String columnName, System.String headerText)` | `System.Windows.Forms.DataGridViewColumnCollection` DesignerSerializationVisibilityAttribute(0) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-49CEBAF9F5F412BD | Method | `public virtual System.Int32 Add(System.Windows.Forms.DataGridViewColumn dataGridViewColumn)` | `System.Windows.Forms.DataGridViewColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0D69CBFE7A61A3A6 | Method | `public virtual System.Void AddRange(System.Windows.Forms.DataGridViewColumn[] dataGridViewColumns)` | `System.Windows.Forms.DataGridViewColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6198020B23CF8BEA | Method | `public virtual System.Void Clear()` | `System.Windows.Forms.DataGridViewColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-531ED9A367D964E5 | Method | `public virtual System.Boolean Contains(System.String columnName)` | `System.Windows.Forms.DataGridViewColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5FB5A095DEA8241B | Method | `public virtual System.Boolean Contains(System.Windows.Forms.DataGridViewColumn dataGridViewColumn)` | `System.Windows.Forms.DataGridViewColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8A1C0235685EED60 | Method | `public System.Void CopyTo(System.Array ar, System.Int32 index)` | `System.Windows.Forms.BaseCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C020052D4D0C21E2 | Method | `public System.Void CopyTo(System.Windows.Forms.DataGridViewColumn[] array, System.Int32 index)` | `System.Windows.Forms.DataGridViewColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-340024050426F33F | Method | `public System.Int32 GetColumnCount(System.Windows.Forms.DataGridViewElementStates includeFilter)` | `System.Windows.Forms.DataGridViewColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9BA1E7340EB78BBB | Method | `public System.Int32 GetColumnsWidth(System.Windows.Forms.DataGridViewElementStates includeFilter)` | `System.Windows.Forms.DataGridViewColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E472B5D9291C588E | Method | `public System.Collections.IEnumerator GetEnumerator()` | `System.Windows.Forms.BaseCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-76C5ED5F4C987351 | Method | `public System.Windows.Forms.DataGridViewColumn GetFirstColumn(System.Windows.Forms.DataGridViewElementStates includeFilter)` | `System.Windows.Forms.DataGridViewColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-AE09ED4E9312FA4B | Method | `public System.Windows.Forms.DataGridViewColumn GetFirstColumn(System.Windows.Forms.DataGridViewElementStates includeFilter, System.Windows.Forms.DataGridViewElementStates excludeFilter)` | `System.Windows.Forms.DataGridViewColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6BB75E70842A62AB | Method | `public System.Windows.Forms.DataGridViewColumn GetLastColumn(System.Windows.Forms.DataGridViewElementStates includeFilter, System.Windows.Forms.DataGridViewElementStates excludeFilter)` | `System.Windows.Forms.DataGridViewColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C3734CC4CB182709 | Method | `public System.Windows.Forms.DataGridViewColumn GetNextColumn(System.Windows.Forms.DataGridViewColumn dataGridViewColumnStart, System.Windows.Forms.DataGridViewElementStates includeFilter, System.Windows.Forms.DataGridViewElementStates excludeFilter)` | `System.Windows.Forms.DataGridViewColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2B9A428929F2485F | Method | `public System.Windows.Forms.DataGridViewColumn GetPreviousColumn(System.Windows.Forms.DataGridViewColumn dataGridViewColumnStart, System.Windows.Forms.DataGridViewElementStates includeFilter, System.Windows.Forms.DataGridViewElementStates excludeFilter)` | `System.Windows.Forms.DataGridViewColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F92C35CD9D155A77 | Method | `public System.Int32 IndexOf(System.Windows.Forms.DataGridViewColumn dataGridViewColumn)` | `System.Windows.Forms.DataGridViewColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-60B4F4B73C5036FF | Method | `public virtual System.Void Insert(System.Int32 columnIndex, System.Windows.Forms.DataGridViewColumn dataGridViewColumn)` | `System.Windows.Forms.DataGridViewColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4B9CFD10C4537B10 | Method | `public virtual System.Void Remove(System.String columnName)` | `System.Windows.Forms.DataGridViewColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A2DD0872207B789A | Method | `public virtual System.Void Remove(System.Windows.Forms.DataGridViewColumn dataGridViewColumn)` | `System.Windows.Forms.DataGridViewColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6EAE74A48AB27CED | Method | `public virtual System.Void RemoveAt(System.Int32 index)` | `System.Windows.Forms.DataGridViewColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-57949ABF755F6904 | Property | `virtual System.Int32 Count { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FE13CCCA4E8941AF | Property | `System.Boolean IsReadOnly { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7C8249743E38641E | Property | `System.Boolean IsSynchronized { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-EEE04B3CEAE14CBC | Property | `System.Windows.Forms.DataGridViewColumn Item[System.Int32 index] { public get; }` | `System.Windows.Forms.DataGridViewColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9B4AFBCB10A2074C | Property | `System.Windows.Forms.DataGridViewColumn Item[System.String columnName] { public get; }` | `System.Windows.Forms.DataGridViewColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1EF74D03FDAB0DA1 | Property | `System.Object SyncRoot { public get; }` | `System.Windows.Forms.BaseCollection` BrowsableAttribute(false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-29893514EBDE1941 | Method | `protected virtual System.Void OnCollectionChanged(System.ComponentModel.CollectionChangeEventArgs e)` | `System.Windows.Forms.DataGridViewColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CE28ADBE96A97BE4 | Property | `System.Windows.Forms.DataGridView DataGridView { protected get; }` | `System.Windows.Forms.DataGridViewColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A5427E753FEA431E | Property | `virtual System.Collections.ArrayList List { protected get; }` | `System.Windows.Forms.DataGridViewColumnCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
