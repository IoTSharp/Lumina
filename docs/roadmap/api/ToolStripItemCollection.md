# ToolStripItemCollection：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/ToolStripItemCollection.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.ToolStripItemCollection`；基类：`System.Windows.Forms.Layout.ArrangedElementCollection`。Lumina 同名类型：🟡 存在；基类及基线接口形状：⬜ 未匹配。

公开属性 **4**，公开事件 **0**，公开方法 **22**；受保护扩展成员 **0**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-5FD4D4E44E217DFE | Constructor | `public ToolStripItemCollection(System.Windows.Forms.ToolStrip owner, System.Windows.Forms.ToolStripItem[] value)` | `System.Windows.Forms.ToolStripItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E97F532CA36AD837 | Method | `public System.Int32 Add(System.Windows.Forms.ToolStripItem value)` | `System.Windows.Forms.ToolStripItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E581D684C87F462C | Method | `public System.Windows.Forms.ToolStripItem Add(System.Drawing.Image image)` | `System.Windows.Forms.ToolStripItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CCA34AA23164AAAE | Method | `public System.Windows.Forms.ToolStripItem Add(System.String text)` | `System.Windows.Forms.ToolStripItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E29C24556651ACF2 | Method | `public System.Windows.Forms.ToolStripItem Add(System.String text, System.Drawing.Image image)` | `System.Windows.Forms.ToolStripItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CA655308BE984244 | Method | `public System.Windows.Forms.ToolStripItem Add(System.String text, System.Drawing.Image image, System.EventHandler onClick)` | `System.Windows.Forms.ToolStripItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A9EDF9914DC30249 | Method | `public System.Void AddRange(System.Windows.Forms.ToolStripItemCollection toolStripItems)` | `System.Windows.Forms.ToolStripItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B641041C38B25A62 | Method | `public System.Void AddRange(System.Windows.Forms.ToolStripItem[] toolStripItems)` | `System.Windows.Forms.ToolStripItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F14A933E3F4C8193 | Method | `public virtual System.Void Clear()` | `System.Windows.Forms.ToolStripItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4345F13CAB0BD535 | Method | `public System.Boolean Contains(System.Windows.Forms.ToolStripItem value)` | `System.Windows.Forms.ToolStripItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-0B851298C2484567 | Method | `public virtual System.Boolean ContainsKey(System.String key)` | `System.Windows.Forms.ToolStripItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-18C23148BF704550 | Method | `public System.Void CopyTo(System.Array array, System.Int32 index)` | `System.Windows.Forms.Layout.ArrangedElementCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E7697B1DBBDB668B | Method | `public System.Void CopyTo(System.Windows.Forms.ToolStripItem[] array, System.Int32 index)` | `System.Windows.Forms.ToolStripItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-977EABB1F67E8888 | Method | `public virtual System.Boolean Equals(System.Object obj)` | `System.Windows.Forms.Layout.ArrangedElementCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-CA42D2B466163F12 | Method | `public System.Windows.Forms.ToolStripItem[] Find(System.String key, System.Boolean searchAllChildren)` | `System.Windows.Forms.ToolStripItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-144E76A1E6F63CEB | Method | `public virtual System.Collections.IEnumerator GetEnumerator()` | `System.Windows.Forms.Layout.ArrangedElementCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2980884F2D7A54AD | Method | `public virtual System.Int32 GetHashCode()` | `System.Windows.Forms.Layout.ArrangedElementCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-263ECEA2F7514683 | Method | `public System.Int32 IndexOf(System.Windows.Forms.ToolStripItem value)` | `System.Windows.Forms.ToolStripItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F4751C7172144D90 | Method | `public virtual System.Int32 IndexOfKey(System.String key)` | `System.Windows.Forms.ToolStripItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-72233EDA0D386522 | Method | `public System.Void Insert(System.Int32 index, System.Windows.Forms.ToolStripItem value)` | `System.Windows.Forms.ToolStripItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5C129A359339E996 | Method | `public System.Void Remove(System.Windows.Forms.ToolStripItem value)` | `System.Windows.Forms.ToolStripItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-765802D83F37763C | Method | `public System.Void RemoveAt(System.Int32 index)` | `System.Windows.Forms.ToolStripItemCollection`  | 🟡 签名匹配 | ⬜ / ⬜ / ⬜ |
| API-5560AE9DC71D4920 | Method | `public virtual System.Void RemoveByKey(System.String key)` | `System.Windows.Forms.ToolStripItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E78920667E8B284 | Property | `virtual System.Int32 Count { public get; }` | `System.Windows.Forms.Layout.ArrangedElementCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D9D129BBAAD7846B | Property | `virtual System.Boolean IsReadOnly { public get; }` | `System.Windows.Forms.ToolStripItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-8321F24AE1A736CB | Property | `virtual System.Windows.Forms.ToolStripItem Item[System.Int32 index] { public get; }` | `System.Windows.Forms.ToolStripItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BCBF2F4A85A774E1 | Property | `virtual System.Windows.Forms.ToolStripItem Item[System.String key] { public get; }` | `System.Windows.Forms.ToolStripItemCollection`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
