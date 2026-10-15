# DataObject：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/DataObject.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.DataObject`；基类：`System.Object`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **0**，公开事件 **0**，公开方法 **34**；受保护扩展成员 **1**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-9CFC5F6373DC5799 | Constructor | `public DataObject()` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-65655B994021869E | Constructor | `public DataObject(System.Object data)` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F9CE4C75D3A5685B | Constructor | `public DataObject(System.String format, System.Object data)` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B33C0A5BC3EE2219 | Method | `public virtual System.Boolean ContainsAudio()` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5D32FB7EE2FE0ED1 | Method | `public virtual System.Boolean ContainsFileDropList()` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A9011A9FD738B9F0 | Method | `public virtual System.Boolean ContainsImage()` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-37B1D34277825643 | Method | `public virtual System.Boolean ContainsText()` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-27EB0768F7CAE388 | Method | `public virtual System.Boolean ContainsText(System.Windows.Forms.TextDataFormat format)` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-97485D2A16F8CE7B | Method | `public virtual System.IO.Stream GetAudioStream()` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2796E95044F8CAF5 | Method | `public virtual System.Object GetData(System.String format)` | `System.Windows.Forms.DataObject` ObsoleteAttribute("\u0060DataObject.GetData\u0060 methods are obsolete. Use the corresponding \u0060DataObject.TryGetData\u003CT\u003E\u0060 instead.",false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-946A0D771783E3B3 | Method | `public virtual System.Object GetData(System.String format, System.Boolean autoConvert)` | `System.Windows.Forms.DataObject` ObsoleteAttribute("\u0060DataObject.GetData\u0060 methods are obsolete. Use the corresponding \u0060DataObject.TryGetData\u003CT\u003E\u0060 instead.",false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E19D2C9CF609DD15 | Method | `public virtual System.Object GetData(System.Type format)` | `System.Windows.Forms.DataObject` ObsoleteAttribute("\u0060DataObject.GetData\u0060 methods are obsolete. Use the corresponding \u0060DataObject.TryGetData\u003CT\u003E\u0060 instead.",false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-46CFDBAADF9B2448 | Method | `public virtual System.Boolean GetDataPresent(System.String format)` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D3E44FB0AEB1453F | Method | `public virtual System.Boolean GetDataPresent(System.String format, System.Boolean autoConvert)` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A8A824CD51394F2 | Method | `public virtual System.Boolean GetDataPresent(System.Type format)` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-42B07005DDEA027F | Method | `public virtual System.Collections.Specialized.StringCollection GetFileDropList()` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A121EBC9CBC8A099 | Method | `public virtual System.String[] GetFormats()` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-278819E1DED1B19F | Method | `public virtual System.String[] GetFormats(System.Boolean autoConvert)` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FAC401D8050A8232 | Method | `public virtual System.Drawing.Image GetImage()` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2140CD4427B48E17 | Method | `public virtual System.String GetText()` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F9D1AADE2A4A92FD | Method | `public virtual System.String GetText(System.Windows.Forms.TextDataFormat format)` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D7212BB89C2B3DB1 | Method | `public virtual System.Void SetAudio(System.Byte[] audioBytes)` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A41AFAEC1D37C26C | Method | `public virtual System.Void SetAudio(System.IO.Stream audioStream)` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-005903827536678B | Method | `public virtual System.Void SetData(System.Object data)` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A90197E9D5595250 | Method | `public virtual System.Void SetData(System.String format, System.Boolean autoConvert, System.Object data)` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B7CB7277AB70F9A1 | Method | `public virtual System.Void SetData(System.String format, System.Object data)` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-72E4DCCEF499CBF7 | Method | `public virtual System.Void SetData(System.Type format, System.Object data)` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E12D0E24F29F3F67 | Method | `public System.Void SetDataAsJson<!!0>(!!0 data) where !!0: None;` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D5AC19052AB81A4D | Method | `public System.Void SetDataAsJson<!!0>(System.String format, !!0 data) where !!0: None;` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-623F90BB07E9A753 | Method | `public virtual System.Void SetFileDropList(System.Collections.Specialized.StringCollection filePaths)` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6999226B22B9AE67 | Method | `public virtual System.Void SetImage(System.Drawing.Image image)` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9E7DB8A9E3D124A2 | Method | `public virtual System.Void SetText(System.String textData)` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-17A6CD2ADC484052 | Method | `public virtual System.Void SetText(System.String textData, System.Windows.Forms.TextDataFormat format)` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F1B069209EBBBE31 | Method | `public System.Boolean TryGetData<!!0>(System.String format, System.Boolean autoConvert, out !!0 data) where !!0: None;` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-FF738F6222E8FA25 | Method | `public System.Boolean TryGetData<!!0>(System.String format, System.Func&#96;2<System.Reflection.Metadata.TypeName,System.Type> resolver, System.Boolean autoConvert, out !!0 data) where !!0: None;` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1EC5D0BD894E5393 | Method | `public System.Boolean TryGetData<!!0>(System.String format, out !!0 data) where !!0: None;` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B206D8475F73E714 | Method | `public System.Boolean TryGetData<!!0>(out !!0 data) where !!0: None;` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-4005166D36392909 | Method | `protected virtual System.Boolean TryGetDataCore<!!0>(System.String format, System.Func&#96;2<System.Reflection.Metadata.TypeName,System.Type> resolver, System.Boolean autoConvert, out !!0 data) where !!0: None;` | `System.Windows.Forms.DataObject`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
