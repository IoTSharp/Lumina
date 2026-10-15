# Clipboard：逐成员待办

自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **10.0.11**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。

[控件任务](../controls/Clipboard.md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)

类型：`System.Windows.Forms.Clipboard`；基类：`System.Object`。Lumina 同名类型：⬜ 缺失；基类及基线接口形状：⬜ 未匹配。

公开属性 **0**，公开事件 **0**，公开方法 **28**；受保护扩展成员 **0**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。

`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。

## 公开成员

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
| API-5F1CAA01EB61C517 | Method | `public static System.Void Clear()` | `System.Windows.Forms.Clipboard`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-6B0751AEAFFA211E | Method | `public static System.Boolean ContainsAudio()` | `System.Windows.Forms.Clipboard`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2307A24AC7B5E2AB | Method | `public static System.Boolean ContainsData(System.String format)` | `System.Windows.Forms.Clipboard`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-925A06B7CD81BB43 | Method | `public static System.Boolean ContainsFileDropList()` | `System.Windows.Forms.Clipboard`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-C75D4CE3E7FDA480 | Method | `public static System.Boolean ContainsImage()` | `System.Windows.Forms.Clipboard`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E3E56B31746D852D | Method | `public static System.Boolean ContainsText()` | `System.Windows.Forms.Clipboard`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-53B8E0B3D6F6A3F3 | Method | `public static System.Boolean ContainsText(System.Windows.Forms.TextDataFormat format)` | `System.Windows.Forms.Clipboard`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-722C8FCAA3C1C9BD | Method | `public static System.IO.Stream GetAudioStream()` | `System.Windows.Forms.Clipboard`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-96AA92E38BB536F7 | Method | `public static System.Object GetData(System.String format)` | `System.Windows.Forms.Clipboard` ObsoleteAttribute("\u0060Clipboard.GetData(string)\u0060 method is obsolete. Use \u0060Clipboard.TryGetData\u003CT\u003E\u0060 methods instead.",false) | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1FA71D421C97C97A | Method | `public static System.Windows.Forms.IDataObject GetDataObject()` | `System.Windows.Forms.Clipboard`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-489B9AE6A915E07E | Method | `public static System.Collections.Specialized.StringCollection GetFileDropList()` | `System.Windows.Forms.Clipboard`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-F22F1965DC9BC94A | Method | `public static System.Drawing.Image GetImage()` | `System.Windows.Forms.Clipboard`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-E5B6D03D35E3FC5E | Method | `public static System.String GetText()` | `System.Windows.Forms.Clipboard`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-16F767324D2E13C9 | Method | `public static System.String GetText(System.Windows.Forms.TextDataFormat format)` | `System.Windows.Forms.Clipboard`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-BC9F707ED5AE123A | Method | `public static System.Void SetAudio(System.Byte[] audioBytes)` | `System.Windows.Forms.Clipboard`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-5FC43746726097CE | Method | `public static System.Void SetAudio(System.IO.Stream audioStream)` | `System.Windows.Forms.Clipboard`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-B6FBA7580756E196 | Method | `public static System.Void SetData(System.String format, System.Object data)` | `System.Windows.Forms.Clipboard`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4EEC6827545E67BD | Method | `public static System.Void SetDataAsJson<!!0>(System.String format, !!0 data) where !!0: None;` | `System.Windows.Forms.Clipboard`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-48D726316DBC63EB | Method | `public static System.Void SetDataObject(System.Object data)` | `System.Windows.Forms.Clipboard`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-A8396EF551EA1BE8 | Method | `public static System.Void SetDataObject(System.Object data, System.Boolean copy)` | `System.Windows.Forms.Clipboard`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-9A17C793622E2F55 | Method | `public static System.Void SetDataObject(System.Object data, System.Boolean copy, System.Int32 retryTimes, System.Int32 retryDelay)` | `System.Windows.Forms.Clipboard`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-1BAA763C2F3CE150 | Method | `public static System.Void SetFileDropList(System.Collections.Specialized.StringCollection filePaths)` | `System.Windows.Forms.Clipboard`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2E2B4E23B210F524 | Method | `public static System.Void SetImage(System.Drawing.Image image)` | `System.Windows.Forms.Clipboard`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-2188A56F4F119510 | Method | `public static System.Void SetText(System.String text)` | `System.Windows.Forms.Clipboard`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-7F4F3598E93FEFFE | Method | `public static System.Void SetText(System.String text, System.Windows.Forms.TextDataFormat format)` | `System.Windows.Forms.Clipboard`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-388A929BF0F3CA88 | Method | `public static System.Boolean TryGetData<!!0>(System.String format, System.Func&#96;2<System.Reflection.Metadata.TypeName,System.Type> resolver, out !!0 data) where !!0: None;` | `System.Windows.Forms.Clipboard`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-D3E01320B29A1076 | Method | `public static System.Boolean TryGetData<!!0>(System.String format, out !!0 data) where !!0: None;` | `System.Windows.Forms.Clipboard`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |
| API-4C24A6B01AA518D1 | Method | `public static System.Boolean TryGetData<!!0>(out !!0 data) where !!0: None;` | `System.Windows.Forms.Clipboard`  | ⬜ 缺少精确签名 | ⬜ / ⬜ / ⬜ |

## 受保护扩展成员（独立验收，不稀释公开 API 指标）

| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |
| --- | --- | --- | --- | --- | --- |
