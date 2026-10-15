# AxHost

⬜ **现状：**无 AxHost；标准为任意 ActiveX 的 Windows 宿主，公开/受保护 API、生成包装器和 OcxState 与 AOT 动态行为存在实质边界。

- 固定基线：`b83f534` / WinForms 引用程序集 10.0.11，2026-10-15。
- [完整成员与继承成员](../api/AxHost.md) · [基础前置](../foundations.md) · [三平台边界](../platforms.md) · [98%计量](../coverage.md)。未在正文逐字展开的成员仍逐行验收，不靠任务数计算兼容率。
- 源码：同名标准类型尚未实现，无可冒充为完整契约的源码。
- 排序：Control/NativeWindow、COM/OLE/STA、静态 aximp 包装器/资源加载；Windows 高成本 P2。

## 顺序任务

| 状态 | 优先级 / ID | 具体工作与验收 |
| --- | --- | --- |
| ⬜ | P1 / AxHost-01 | 以一个静态已知 ActiveX 控件验证 AOT：生成 wrapper、已知 COM 接口、in-place 激活、事件和 .resx OcxState；先写限制结论，不假设“支持 COM”即兼容任意 ActiveX。 |
| ⬜ | P2 / AxHost-02 | 对齐 AxHost : Control、protected clsid/flags 构造、ISupportInitialize/BeginInit/EndInit、ContainingControl/OcxState、GetOcx/DoVerb 与标准派生扩展。 |
| ⬜ | P2 / AxHost-03 | 实现 AttachInterfaces/CreateInstanceCore/CreateSink/DetachSink、site/容器/键盘/焦点/size/可见状态机，事件连接点在正确时机建立和解除。 |
| ⬜ | P2 / AxHost-04 | 实现 OcxState 持久化、授权状态/属性页、HasPropertyPages/ShowPropertyPages/ShowAboutBox、MakeDirty/InvokeEditMode 等；设计器与 runtime 责任分开，静态资源不得靠运行时生成代码读取。 |
| ⬜ | P2 / AxHost-05 | 校准 OLE Color/Font/Picture/日期转换与 RaiseOnMouse*、异常/无效 CLSID/注册缺失、Dispose/重建时 COM 引用归属；任意 IDispatch 适配的 AOT 元数据需求列清。 |
| ⬜ | P2 / AxHost-06 | Windows 使用原生 COM/OLE in-place 宿主，wrapper 在构建期静态引用；未知托管程序集动态载入/运行时 Emit 不在 Native AOT 能力范围，无法支持时保留未通过并披露兼容边界。 |
| ⬜ | P2 / AxHost-07 | Linux 🚫 GTK4 没有原生 Windows ActiveX/COM 注册体系；不把替代 widget 当 AxHost 等价，不引入其他 UI 栈规避。 |
| ⬜ | P3 / AxHost-08 | macOS 🚫 AppKit 原生控件不提供 Windows ActiveX，无法映射 GetOcx/OcxState；平台专属记录不能抵扣 Windows 分母。 |
| ⬜ | 验收 / AxHost-V | 逐属性/事件/方法与官方端比较签名、默认、运行变更、取消、异常、事件顺序和释放；NativeFormsDemo 保持双目标与静态 .Designer.cs/.resx，真实设计器往返、AOT 发布后交互/正常退出、原生资源与可访问性有证据。三平台分别验收，未实现/只存值/未运行均不得标 🧪 验收通过。 |

## 原生实现与不可等价项

| Windows | Linux | macOS |
| --- | --- | --- |
| 已知 ActiveX 的静态 AOT 包装值得原型，任意组件/动态插件兼容不先承诺。 | Windows 专属，GTK 原生控件不能透明模拟 ActiveX。 | Windows 专属，AppKit 原生控件不能透明模拟 ActiveX。 |

Font/Image/Icon/Graphics 的公开 System.Drawing.Common 类型身份属于共同前置；内部换 GTK/AppKit 不会自动解决，不能把普通图形能力排除来抬高可移植分数。Windows 消息/OLE 等真实专属契约逐成员审核并记录 🚫；该状态不等于 Windows 已完成。

## 修改历史

- 无同名实现的 Git 修改历史。
- 2026-10-15：增加评估和待办；本轮未实现运行库功能。

## 原始资料

- [微软固定契约](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.axhost?view=windowsdesktop-10.0)
- [Native AOT 限制与运行时动态行为](https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/)
- [Native AOT 原生互操作](https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/interop)
- [GTK/GLib 原生依据](https://docs.gtk.org/gtk4/class.Widget.html)
- [Apple 原生依据](https://developer.apple.com/documentation/appkit/nsview)
