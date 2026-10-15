# 遗漏控件与呈现对象范围审计

审计日期：2026-10-15。固定基线：Microsoft.WindowsDesktop.App.Ref **10.0.11**，原路线图检查点为119份独立类型页。下表“起点缺页”保留原检查点的缺漏；本轮已补对应文档，不能因此改成实现完成。

## 结论

原119页覆盖全部 **69 个导出的 Control 派生类型（62个非抽象、7个抽象）**，但这不足以证明覆盖 WinForms 全部可见 UI。ToolStripItem、现代 TaskDialog、旧菜单和非 Control 组件存在47个需要独立规划的直接遗漏，另有表格样式、集合、绑定及绘图等附属契约需要明确页或任务归属。

**补齐结果：新增68份独立任务页和对应API附件，总计187组、1948条正文任务。** 47个直接遗漏均已有入口，另补重要集合、绑定管理器、样式、属性窗格对象及关联事件参数；完整[索引](README.md)、[完整类型映射](api/page-map.csv)、[任务台账](tasks.csv)。三位子智能体分族分析并交叉复核；全部1095类型保留责任归属，未独立展开的附属类型仍须人工逐项复核，不能据此宣称全库行为已验收。

这里的“全部”来自固定参考包真实类型继承闭包，不是手写控件名单；没有发现额外导出的嵌套 Control 子类。ComponentEditorPage 是抽象类，不把它算为第63个具体 Control。**62也是类型形状数，不是62个官方仍可运行的UI：DataGrid/DataGridTextBox/ToolBar/StatusBar属于现代.NET占位类型。**

元数据原始清单：[types.json](api/types.json)、[成员](api/members.csv)、[接口](api/interfaces.csv)、[参考包指纹](api/baseline.json)。闭包判定沿 BaseType 向上，跨命名空间包含 `System.Drawing.Printing.PrintDocument`；抽象、嵌套、Obsolete 不被静默排除。

## 计数和分组

| 闭包 | 类型数 | 起点有独立页 | 起点缺独立页 |
| --- | --- | --- | --- |
| Control 派生闭包（含自身、抽象和嵌套检查） | 69 | 69 | 0 |
| ToolStripItem 派生闭包（含自身） | 14 | 13 | 1 |
| CommonDialog 派生闭包（含自身） | 9 | 9 | 0 |
| 旧 Menu 派生闭包（含自身） | 4 | 0 | 4 |
| 其他 Component 派生对象 | 17 | 9 | 8 |
| TaskDialog 呈现对象及集合 | 13 | 0 | 13 |
| DataGridViewElement 派生闭包（含自身） | 21 | 0 | 21 |

前五组互不重叠，它们的并集是完整 **113 个 Component 派生类型**（不包含程序集外的 Component 自身）。TaskDialog 与 DataGridViewElement 两组不继承 Component。上表不是功能完成率；已有独立页也不等于实现通过。

## 现代 .NET 与历史占位必须区分

[参考端 JIT 探针及完整可复现源码](../assessments/2026-10-15/legacy-control-probes.json) 实测 MainMenu、ContextMenu、MenuItem、DataGrid、DataGridTextBox、DataGridBoolColumn、DataGridTextBoxColumn、DataGridTableStyle、ToolBar、StatusBar、ToolBarButton、StatusBarPanel 的无参构造均抛 `PlatformNotSupportedException`；Menu 本身抽象。这些类型的 Obsolete 信息明确其 .NET Framework 二进制兼容用途。

它们继续留在类型清单，逐签名/异常对齐优先；重现 .NET Framework 时代可工作的旧UI属于额外扩展。现代.NET10的98%不能通过大量异常占位项稀释常用控件指标，也不能把“造出了比官方更多的旧UI”当作原目标必须成本。每个原来承诺旧式视觉行为的页面需据此调整顺序。

## 完整派生闭包与起点缺漏

以下每行给出精确类型、直接基类、抽象性及官方原始 API 出处。起点已有页只说明文档归属；全部运行支持仍须按 [98%计量](coverage.md) 验收。

### Control 派生闭包（含自身、抽象和嵌套检查）

| 精确类型 | 直接基类 | 形状 | 原119页状态 | 原始出处 |
| --- | --- | --- | --- | --- |
| `System.Windows.Forms.AxHost` | `System.Windows.Forms.Control` | 抽象 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.axhost?view=windowsdesktop-10.0) |
| `System.Windows.Forms.BindingNavigator` | `System.Windows.Forms.ToolStrip` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.bindingnavigator?view=windowsdesktop-10.0) |
| `System.Windows.Forms.Button` | `System.Windows.Forms.ButtonBase` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.button?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ButtonBase` | `System.Windows.Forms.Control` | 抽象 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.buttonbase?view=windowsdesktop-10.0) |
| `System.Windows.Forms.CheckBox` | `System.Windows.Forms.ButtonBase` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.checkbox?view=windowsdesktop-10.0) |
| `System.Windows.Forms.CheckedListBox` | `System.Windows.Forms.ListBox` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.checkedlistbox?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ComboBox` | `System.Windows.Forms.ListControl` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.combobox?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ContainerControl` | `System.Windows.Forms.ScrollableControl` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.containercontrol?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ContextMenuStrip` | `System.Windows.Forms.ToolStripDropDownMenu` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.contextmenustrip?view=windowsdesktop-10.0) |
| `System.Windows.Forms.Control` | `System.ComponentModel.Component` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.control?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGrid` | `System.Windows.Forms.Control` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagrid?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridTextBox` | `System.Windows.Forms.TextBox` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridtextbox?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridView` | `System.Windows.Forms.Control` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridview?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridViewComboBoxEditingControl` | `System.Windows.Forms.ComboBox` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewcomboboxeditingcontrol?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridViewTextBoxEditingControl` | `System.Windows.Forms.TextBox` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewtextboxeditingcontrol?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DateTimePicker` | `System.Windows.Forms.Control` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datetimepicker?view=windowsdesktop-10.0) |
| `System.Windows.Forms.Design.ComponentEditorForm` | `System.Windows.Forms.Form` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.design.componenteditorform?view=windowsdesktop-10.0) |
| `System.Windows.Forms.Design.ComponentEditorPage` | `System.Windows.Forms.Panel` | 抽象 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.design.componenteditorpage?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DomainUpDown` | `System.Windows.Forms.UpDownBase` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.domainupdown?view=windowsdesktop-10.0) |
| `System.Windows.Forms.FlowLayoutPanel` | `System.Windows.Forms.Panel` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.flowlayoutpanel?view=windowsdesktop-10.0) |
| `System.Windows.Forms.Form` | `System.Windows.Forms.ContainerControl` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.form?view=windowsdesktop-10.0) |
| `System.Windows.Forms.GroupBox` | `System.Windows.Forms.Control` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.groupbox?view=windowsdesktop-10.0) |
| `System.Windows.Forms.HScrollBar` | `System.Windows.Forms.ScrollBar` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.hscrollbar?view=windowsdesktop-10.0) |
| `System.Windows.Forms.Label` | `System.Windows.Forms.Control` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.label?view=windowsdesktop-10.0) |
| `System.Windows.Forms.LinkLabel` | `System.Windows.Forms.Label` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.linklabel?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ListBox` | `System.Windows.Forms.ListControl` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.listbox?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ListControl` | `System.Windows.Forms.Control` | 抽象 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.listcontrol?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ListView` | `System.Windows.Forms.Control` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.listview?view=windowsdesktop-10.0) |
| `System.Windows.Forms.MaskedTextBox` | `System.Windows.Forms.TextBoxBase` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.maskedtextbox?view=windowsdesktop-10.0) |
| `System.Windows.Forms.MdiClient` | `System.Windows.Forms.Control` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.mdiclient?view=windowsdesktop-10.0) |
| `System.Windows.Forms.MenuStrip` | `System.Windows.Forms.ToolStrip` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.menustrip?view=windowsdesktop-10.0) |
| `System.Windows.Forms.MonthCalendar` | `System.Windows.Forms.Control` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.monthcalendar?view=windowsdesktop-10.0) |
| `System.Windows.Forms.NumericUpDown` | `System.Windows.Forms.UpDownBase` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.numericupdown?view=windowsdesktop-10.0) |
| `System.Windows.Forms.Panel` | `System.Windows.Forms.ScrollableControl` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.panel?view=windowsdesktop-10.0) |
| `System.Windows.Forms.PictureBox` | `System.Windows.Forms.Control` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.picturebox?view=windowsdesktop-10.0) |
| `System.Windows.Forms.PrintPreviewControl` | `System.Windows.Forms.Control` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.printpreviewcontrol?view=windowsdesktop-10.0) |
| `System.Windows.Forms.PrintPreviewDialog` | `System.Windows.Forms.Form` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.printpreviewdialog?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ProgressBar` | `System.Windows.Forms.Control` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.progressbar?view=windowsdesktop-10.0) |
| `System.Windows.Forms.PropertyGrid` | `System.Windows.Forms.ContainerControl` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.propertygrid?view=windowsdesktop-10.0) |
| `System.Windows.Forms.RadioButton` | `System.Windows.Forms.ButtonBase` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.radiobutton?view=windowsdesktop-10.0) |
| `System.Windows.Forms.RichTextBox` | `System.Windows.Forms.TextBoxBase` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.richtextbox?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ScrollBar` | `System.Windows.Forms.Control` | 抽象 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.scrollbar?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ScrollableControl` | `System.Windows.Forms.Control` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.scrollablecontrol?view=windowsdesktop-10.0) |
| `System.Windows.Forms.SplitContainer` | `System.Windows.Forms.ContainerControl` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.splitcontainer?view=windowsdesktop-10.0) |
| `System.Windows.Forms.Splitter` | `System.Windows.Forms.Control` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.splitter?view=windowsdesktop-10.0) |
| `System.Windows.Forms.SplitterPanel` | `System.Windows.Forms.Panel` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.splitterpanel?view=windowsdesktop-10.0) |
| `System.Windows.Forms.StatusBar` | `System.Windows.Forms.Control` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.statusbar?view=windowsdesktop-10.0) |
| `System.Windows.Forms.StatusStrip` | `System.Windows.Forms.ToolStrip` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.statusstrip?view=windowsdesktop-10.0) |
| `System.Windows.Forms.TabControl` | `System.Windows.Forms.Control` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.tabcontrol?view=windowsdesktop-10.0) |
| `System.Windows.Forms.TabPage` | `System.Windows.Forms.Panel` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.tabpage?view=windowsdesktop-10.0) |
| `System.Windows.Forms.TableLayoutPanel` | `System.Windows.Forms.Panel` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.tablelayoutpanel?view=windowsdesktop-10.0) |
| `System.Windows.Forms.TextBox` | `System.Windows.Forms.TextBoxBase` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.textbox?view=windowsdesktop-10.0) |
| `System.Windows.Forms.TextBoxBase` | `System.Windows.Forms.Control` | 抽象 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.textboxbase?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ThreadExceptionDialog` | `System.Windows.Forms.Form` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.threadexceptiondialog?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ToolBar` | `System.Windows.Forms.Control` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolbar?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ToolStrip` | `System.Windows.Forms.ScrollableControl` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstrip?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ToolStripContainer` | `System.Windows.Forms.ContainerControl` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripcontainer?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ToolStripContentPanel` | `System.Windows.Forms.Panel` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripcontentpanel?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ToolStripDropDown` | `System.Windows.Forms.ToolStrip` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripdropdown?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ToolStripDropDownMenu` | `System.Windows.Forms.ToolStripDropDown` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripdropdownmenu?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ToolStripOverflow` | `System.Windows.Forms.ToolStripDropDown` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripoverflow?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ToolStripPanel` | `System.Windows.Forms.ContainerControl` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstrippanel?view=windowsdesktop-10.0) |
| `System.Windows.Forms.TrackBar` | `System.Windows.Forms.Control` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.trackbar?view=windowsdesktop-10.0) |
| `System.Windows.Forms.TreeView` | `System.Windows.Forms.Control` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.treeview?view=windowsdesktop-10.0) |
| `System.Windows.Forms.UpDownBase` | `System.Windows.Forms.ContainerControl` | 抽象 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.updownbase?view=windowsdesktop-10.0) |
| `System.Windows.Forms.UserControl` | `System.Windows.Forms.ContainerControl` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.usercontrol?view=windowsdesktop-10.0) |
| `System.Windows.Forms.VScrollBar` | `System.Windows.Forms.ScrollBar` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.vscrollbar?view=windowsdesktop-10.0) |
| `System.Windows.Forms.WebBrowser` | `System.Windows.Forms.WebBrowserBase` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.webbrowser?view=windowsdesktop-10.0) |
| `System.Windows.Forms.WebBrowserBase` | `System.Windows.Forms.Control` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.webbrowserbase?view=windowsdesktop-10.0) |

### ToolStripItem 派生闭包（含自身）

| 精确类型 | 直接基类 | 形状 | 原119页状态 | 原始出处 |
| --- | --- | --- | --- | --- |
| `System.Windows.Forms.ToolStripButton` | `System.Windows.Forms.ToolStripItem` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripbutton?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ToolStripComboBox` | `System.Windows.Forms.ToolStripControlHost` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripcombobox?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ToolStripControlHost` | `System.Windows.Forms.ToolStripItem` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripcontrolhost?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ToolStripDropDownButton` | `System.Windows.Forms.ToolStripDropDownItem` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripdropdownbutton?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ToolStripDropDownItem` | `System.Windows.Forms.ToolStripItem` | 抽象 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripdropdownitem?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ToolStripItem` | `System.Windows.Forms.BindableComponent` | 抽象 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripitem?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ToolStripLabel` | `System.Windows.Forms.ToolStripItem` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstriplabel?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ToolStripMenuItem` | `System.Windows.Forms.ToolStripDropDownItem` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripmenuitem?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ToolStripOverflowButton` | `System.Windows.Forms.ToolStripDropDownButton` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripoverflowbutton?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ToolStripProgressBar` | `System.Windows.Forms.ToolStripControlHost` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripprogressbar?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ToolStripSeparator` | `System.Windows.Forms.ToolStripItem` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripseparator?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ToolStripSplitButton` | `System.Windows.Forms.ToolStripDropDownItem` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripsplitbutton?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ToolStripStatusLabel` | `System.Windows.Forms.ToolStripLabel` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripstatuslabel?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ToolStripTextBox` | `System.Windows.Forms.ToolStripControlHost` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstriptextbox?view=windowsdesktop-10.0) |

### CommonDialog 派生闭包（含自身）

| 精确类型 | 直接基类 | 形状 | 原119页状态 | 原始出处 |
| --- | --- | --- | --- | --- |
| `System.Windows.Forms.ColorDialog` | `System.Windows.Forms.CommonDialog` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.colordialog?view=windowsdesktop-10.0) |
| `System.Windows.Forms.CommonDialog` | `System.ComponentModel.Component` | 抽象 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.commondialog?view=windowsdesktop-10.0) |
| `System.Windows.Forms.FileDialog` | `System.Windows.Forms.CommonDialog` | 抽象 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.filedialog?view=windowsdesktop-10.0) |
| `System.Windows.Forms.FolderBrowserDialog` | `System.Windows.Forms.CommonDialog` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.folderbrowserdialog?view=windowsdesktop-10.0) |
| `System.Windows.Forms.FontDialog` | `System.Windows.Forms.CommonDialog` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.fontdialog?view=windowsdesktop-10.0) |
| `System.Windows.Forms.OpenFileDialog` | `System.Windows.Forms.FileDialog` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.openfiledialog?view=windowsdesktop-10.0) |
| `System.Windows.Forms.PageSetupDialog` | `System.Windows.Forms.CommonDialog` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.pagesetupdialog?view=windowsdesktop-10.0) |
| `System.Windows.Forms.PrintDialog` | `System.Windows.Forms.CommonDialog` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.printdialog?view=windowsdesktop-10.0) |
| `System.Windows.Forms.SaveFileDialog` | `System.Windows.Forms.FileDialog` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.savefiledialog?view=windowsdesktop-10.0) |

### 旧 Menu 派生闭包（含自身）

| 精确类型 | 直接基类 | 形状 | 原119页状态 | 原始出处 |
| --- | --- | --- | --- | --- |
| `System.Windows.Forms.ContextMenu` | `System.Windows.Forms.Menu` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.contextmenu?view=windowsdesktop-10.0) |
| `System.Windows.Forms.MainMenu` | `System.Windows.Forms.Menu` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.mainmenu?view=windowsdesktop-10.0) |
| `System.Windows.Forms.Menu` | `System.ComponentModel.Component` | 抽象 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.menu?view=windowsdesktop-10.0) |
| `System.Windows.Forms.MenuItem` | `System.Windows.Forms.Menu` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.menuitem?view=windowsdesktop-10.0) |

### 其他 Component 派生对象

| 精确类型 | 直接基类 | 形状 | 原119页状态 | 原始出处 |
| --- | --- | --- | --- | --- |
| `System.Drawing.Printing.PrintDocument` | `System.ComponentModel.Component` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.drawing.printing.printdocument?view=windowsdesktop-10.0) |
| `System.Windows.Forms.BindableComponent` | `System.ComponentModel.Component` | 抽象 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.bindablecomponent?view=windowsdesktop-10.0) |
| `System.Windows.Forms.BindingSource` | `System.ComponentModel.Component` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.bindingsource?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ColumnHeader` | `System.ComponentModel.Component` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.columnheader?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridBoolColumn` | `System.Windows.Forms.DataGridColumnStyle` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridboolcolumn?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridColumnStyle` | `System.ComponentModel.Component` | 抽象 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridcolumnstyle?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridTableStyle` | `System.ComponentModel.Component` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridtablestyle?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridTextBoxColumn` | `System.Windows.Forms.DataGridColumnStyle` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridtextboxcolumn?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ErrorProvider` | `System.ComponentModel.Component` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.errorprovider?view=windowsdesktop-10.0) |
| `System.Windows.Forms.HelpProvider` | `System.ComponentModel.Component` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.helpprovider?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ImageList` | `System.ComponentModel.Component` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.imagelist?view=windowsdesktop-10.0) |
| `System.Windows.Forms.NotifyIcon` | `System.ComponentModel.Component` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.notifyicon?view=windowsdesktop-10.0) |
| `System.Windows.Forms.StatusBarPanel` | `System.ComponentModel.Component` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.statusbarpanel?view=windowsdesktop-10.0) |
| `System.Windows.Forms.Timer` | `System.ComponentModel.Component` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.timer?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ToolBarButton` | `System.ComponentModel.Component` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolbarbutton?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ToolStripPanelRow` | `System.ComponentModel.Component` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstrippanelrow?view=windowsdesktop-10.0) |
| `System.Windows.Forms.ToolTip` | `System.ComponentModel.Component` | 具体/静态 | 🟡 起点已有独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.tooltip?view=windowsdesktop-10.0) |

### TaskDialog 呈现对象及集合

| 精确类型 | 直接基类 | 形状 | 原119页状态 | 原始出处 |
| --- | --- | --- | --- | --- |
| `System.Windows.Forms.TaskDialog` | `System.Object` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.taskdialog?view=windowsdesktop-10.0) |
| `System.Windows.Forms.TaskDialogButton` | `System.Windows.Forms.TaskDialogControl` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.taskdialogbutton?view=windowsdesktop-10.0) |
| `System.Windows.Forms.TaskDialogButtonCollection` | `System.Collections.ObjectModel.Collection<System.Windows.Forms.TaskDialogButton>` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.taskdialogbuttoncollection?view=windowsdesktop-10.0) |
| `System.Windows.Forms.TaskDialogCommandLinkButton` | `System.Windows.Forms.TaskDialogButton` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.taskdialogcommandlinkbutton?view=windowsdesktop-10.0) |
| `System.Windows.Forms.TaskDialogControl` | `System.Object` | 抽象 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.taskdialogcontrol?view=windowsdesktop-10.0) |
| `System.Windows.Forms.TaskDialogExpander` | `System.Windows.Forms.TaskDialogControl` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.taskdialogexpander?view=windowsdesktop-10.0) |
| `System.Windows.Forms.TaskDialogFootnote` | `System.Windows.Forms.TaskDialogControl` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.taskdialogfootnote?view=windowsdesktop-10.0) |
| `System.Windows.Forms.TaskDialogIcon` | `System.Object` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.taskdialogicon?view=windowsdesktop-10.0) |
| `System.Windows.Forms.TaskDialogPage` | `System.Object` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.taskdialogpage?view=windowsdesktop-10.0) |
| `System.Windows.Forms.TaskDialogProgressBar` | `System.Windows.Forms.TaskDialogControl` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.taskdialogprogressbar?view=windowsdesktop-10.0) |
| `System.Windows.Forms.TaskDialogRadioButton` | `System.Windows.Forms.TaskDialogControl` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.taskdialogradiobutton?view=windowsdesktop-10.0) |
| `System.Windows.Forms.TaskDialogRadioButtonCollection` | `System.Collections.ObjectModel.Collection<System.Windows.Forms.TaskDialogRadioButton>` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.taskdialogradiobuttoncollection?view=windowsdesktop-10.0) |
| `System.Windows.Forms.TaskDialogVerificationCheckBox` | `System.Windows.Forms.TaskDialogControl` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.taskdialogverificationcheckbox?view=windowsdesktop-10.0) |

### DataGridViewElement 派生闭包（含自身）

| 精确类型 | 直接基类 | 形状 | 原119页状态 | 原始出处 |
| --- | --- | --- | --- | --- |
| `System.Windows.Forms.DataGridViewBand` | `System.Windows.Forms.DataGridViewElement` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewband?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridViewButtonCell` | `System.Windows.Forms.DataGridViewCell` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewbuttoncell?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridViewButtonColumn` | `System.Windows.Forms.DataGridViewColumn` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewbuttoncolumn?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridViewCell` | `System.Windows.Forms.DataGridViewElement` | 抽象 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewcell?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridViewCheckBoxCell` | `System.Windows.Forms.DataGridViewCell` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewcheckboxcell?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridViewCheckBoxColumn` | `System.Windows.Forms.DataGridViewColumn` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewcheckboxcolumn?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridViewColumn` | `System.Windows.Forms.DataGridViewBand` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewcolumn?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridViewColumnHeaderCell` | `System.Windows.Forms.DataGridViewHeaderCell` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewcolumnheadercell?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridViewComboBoxCell` | `System.Windows.Forms.DataGridViewCell` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewcomboboxcell?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridViewComboBoxColumn` | `System.Windows.Forms.DataGridViewColumn` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewcomboboxcolumn?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridViewElement` | `System.Object` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewelement?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridViewHeaderCell` | `System.Windows.Forms.DataGridViewCell` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewheadercell?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridViewImageCell` | `System.Windows.Forms.DataGridViewCell` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewimagecell?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridViewImageColumn` | `System.Windows.Forms.DataGridViewColumn` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewimagecolumn?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridViewLinkCell` | `System.Windows.Forms.DataGridViewCell` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewlinkcell?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridViewLinkColumn` | `System.Windows.Forms.DataGridViewColumn` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewlinkcolumn?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridViewRow` | `System.Windows.Forms.DataGridViewBand` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewrow?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridViewRowHeaderCell` | `System.Windows.Forms.DataGridViewHeaderCell` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewrowheadercell?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridViewTextBoxCell` | `System.Windows.Forms.DataGridViewCell` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewtextboxcell?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridViewTextBoxColumn` | `System.Windows.Forms.DataGridViewColumn` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewtextboxcolumn?view=windowsdesktop-10.0) |
| `System.Windows.Forms.DataGridViewTopLeftHeaderCell` | `System.Windows.Forms.DataGridViewColumnHeaderCell` | 具体/静态 | ⬜ 起点缺独立页 | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewtopleftheadercell?view=windowsdesktop-10.0) |

## 47个直接遗漏的补齐顺序与原因

| 类型 | 阶段 | 为什么必须规划 |
| --- | --- | --- |
| `System.Windows.Forms.BindableComponent` | M1 | ToolStripItem等的标准可绑定组件基类，缺它会破坏继承链与绑定设计器消费。 |
| `System.Windows.Forms.ContextMenu` | M0核基线 → M4占位 | 遗留菜单公开类型；现代.NET官方PNSE/Obsolete契约保留，可工作历史UI另列可选扩展。 |
| `System.Windows.Forms.DataGridBoolColumn` | M4占位 | 旧表格/工具栏/状态栏组件契约；先匹配现代.NET占位异常，历史视觉行为作为可选扩展。 |
| `System.Windows.Forms.DataGridColumnStyle` | M4占位 | 旧表格/工具栏/状态栏组件契约；先匹配现代.NET占位异常，历史视觉行为作为可选扩展。 |
| `System.Windows.Forms.DataGridTableStyle` | M4占位 | 旧表格/工具栏/状态栏组件契约；先匹配现代.NET占位异常，历史视觉行为作为可选扩展。 |
| `System.Windows.Forms.DataGridTextBoxColumn` | M4占位 | 旧表格/工具栏/状态栏组件契约；先匹配现代.NET占位异常，历史视觉行为作为可选扩展。 |
| `System.Windows.Forms.DataGridViewBand` | M3 | 表格真正可见的行/列/单元格及派生契约；不能用DataGridView一个大任务吸收全部编辑/样式/绘制差异。 |
| `System.Windows.Forms.DataGridViewButtonCell` | M3 | 表格真正可见的行/列/单元格及派生契约；不能用DataGridView一个大任务吸收全部编辑/样式/绘制差异。 |
| `System.Windows.Forms.DataGridViewButtonColumn` | M3 | 表格真正可见的行/列/单元格及派生契约；不能用DataGridView一个大任务吸收全部编辑/样式/绘制差异。 |
| `System.Windows.Forms.DataGridViewCell` | M3 | 表格真正可见的行/列/单元格及派生契约；不能用DataGridView一个大任务吸收全部编辑/样式/绘制差异。 |
| `System.Windows.Forms.DataGridViewCheckBoxCell` | M3 | 表格真正可见的行/列/单元格及派生契约；不能用DataGridView一个大任务吸收全部编辑/样式/绘制差异。 |
| `System.Windows.Forms.DataGridViewCheckBoxColumn` | M3 | 表格真正可见的行/列/单元格及派生契约；不能用DataGridView一个大任务吸收全部编辑/样式/绘制差异。 |
| `System.Windows.Forms.DataGridViewColumn` | M3 | 表格真正可见的行/列/单元格及派生契约；不能用DataGridView一个大任务吸收全部编辑/样式/绘制差异。 |
| `System.Windows.Forms.DataGridViewColumnHeaderCell` | M3 | 表格真正可见的行/列/单元格及派生契约；不能用DataGridView一个大任务吸收全部编辑/样式/绘制差异。 |
| `System.Windows.Forms.DataGridViewComboBoxCell` | M3 | 表格真正可见的行/列/单元格及派生契约；不能用DataGridView一个大任务吸收全部编辑/样式/绘制差异。 |
| `System.Windows.Forms.DataGridViewComboBoxColumn` | M3 | 表格真正可见的行/列/单元格及派生契约；不能用DataGridView一个大任务吸收全部编辑/样式/绘制差异。 |
| `System.Windows.Forms.DataGridViewElement` | M3 | 表格真正可见的行/列/单元格及派生契约；不能用DataGridView一个大任务吸收全部编辑/样式/绘制差异。 |
| `System.Windows.Forms.DataGridViewHeaderCell` | M3 | 表格真正可见的行/列/单元格及派生契约；不能用DataGridView一个大任务吸收全部编辑/样式/绘制差异。 |
| `System.Windows.Forms.DataGridViewImageCell` | M3 | 表格真正可见的行/列/单元格及派生契约；不能用DataGridView一个大任务吸收全部编辑/样式/绘制差异。 |
| `System.Windows.Forms.DataGridViewImageColumn` | M3 | 表格真正可见的行/列/单元格及派生契约；不能用DataGridView一个大任务吸收全部编辑/样式/绘制差异。 |
| `System.Windows.Forms.DataGridViewLinkCell` | M3 | 表格真正可见的行/列/单元格及派生契约；不能用DataGridView一个大任务吸收全部编辑/样式/绘制差异。 |
| `System.Windows.Forms.DataGridViewLinkColumn` | M3 | 表格真正可见的行/列/单元格及派生契约；不能用DataGridView一个大任务吸收全部编辑/样式/绘制差异。 |
| `System.Windows.Forms.DataGridViewRow` | M3 | 表格真正可见的行/列/单元格及派生契约；不能用DataGridView一个大任务吸收全部编辑/样式/绘制差异。 |
| `System.Windows.Forms.DataGridViewRowHeaderCell` | M3 | 表格真正可见的行/列/单元格及派生契约；不能用DataGridView一个大任务吸收全部编辑/样式/绘制差异。 |
| `System.Windows.Forms.DataGridViewTextBoxCell` | M3 | 表格真正可见的行/列/单元格及派生契约；不能用DataGridView一个大任务吸收全部编辑/样式/绘制差异。 |
| `System.Windows.Forms.DataGridViewTextBoxColumn` | M3 | 表格真正可见的行/列/单元格及派生契约；不能用DataGridView一个大任务吸收全部编辑/样式/绘制差异。 |
| `System.Windows.Forms.DataGridViewTopLeftHeaderCell` | M3 | 表格真正可见的行/列/单元格及派生契约；不能用DataGridView一个大任务吸收全部编辑/样式/绘制差异。 |
| `System.Windows.Forms.MainMenu` | M0核基线 → M4占位 | 遗留菜单公开类型；现代.NET官方PNSE/Obsolete契约保留，可工作历史UI另列可选扩展。 |
| `System.Windows.Forms.Menu` | M0核基线 → M4占位 | 遗留菜单公开类型；现代.NET官方PNSE/Obsolete契约保留，可工作历史UI另列可选扩展。 |
| `System.Windows.Forms.MenuItem` | M0核基线 → M4占位 | 遗留菜单公开类型；现代.NET官方PNSE/Obsolete契约保留，可工作历史UI另列可选扩展。 |
| `System.Windows.Forms.StatusBarPanel` | M4占位 | 旧表格/工具栏/状态栏组件契约；先匹配现代.NET占位异常，历史视觉行为作为可选扩展。 |
| `System.Windows.Forms.TaskDialog` | M2–M3 | 现代原生任务对话框及其交互/页面/内容对象，不是Control子类，不能由MessageBox页替代。 |
| `System.Windows.Forms.TaskDialogButton` | M2–M3 | 现代原生任务对话框及其交互/页面/内容对象，不是Control子类，不能由MessageBox页替代。 |
| `System.Windows.Forms.TaskDialogButtonCollection` | M2–M3 | 现代原生任务对话框及其交互/页面/内容对象，不是Control子类，不能由MessageBox页替代。 |
| `System.Windows.Forms.TaskDialogCommandLinkButton` | M2–M3 | 现代原生任务对话框及其交互/页面/内容对象，不是Control子类，不能由MessageBox页替代。 |
| `System.Windows.Forms.TaskDialogControl` | M2–M3 | 现代原生任务对话框及其交互/页面/内容对象，不是Control子类，不能由MessageBox页替代。 |
| `System.Windows.Forms.TaskDialogExpander` | M2–M3 | 现代原生任务对话框及其交互/页面/内容对象，不是Control子类，不能由MessageBox页替代。 |
| `System.Windows.Forms.TaskDialogFootnote` | M2–M3 | 现代原生任务对话框及其交互/页面/内容对象，不是Control子类，不能由MessageBox页替代。 |
| `System.Windows.Forms.TaskDialogIcon` | M2–M3 | 现代原生任务对话框及其交互/页面/内容对象，不是Control子类，不能由MessageBox页替代。 |
| `System.Windows.Forms.TaskDialogPage` | M2–M3 | 现代原生任务对话框及其交互/页面/内容对象，不是Control子类，不能由MessageBox页替代。 |
| `System.Windows.Forms.TaskDialogProgressBar` | M2–M3 | 现代原生任务对话框及其交互/页面/内容对象，不是Control子类，不能由MessageBox页替代。 |
| `System.Windows.Forms.TaskDialogRadioButton` | M2–M3 | 现代原生任务对话框及其交互/页面/内容对象，不是Control子类，不能由MessageBox页替代。 |
| `System.Windows.Forms.TaskDialogRadioButtonCollection` | M2–M3 | 现代原生任务对话框及其交互/页面/内容对象，不是Control子类，不能由MessageBox页替代。 |
| `System.Windows.Forms.TaskDialogVerificationCheckBox` | M2–M3 | 现代原生任务对话框及其交互/页面/内容对象，不是Control子类，不能由MessageBox页替代。 |
| `System.Windows.Forms.ToolBarButton` | M4占位 | 旧表格/工具栏/状态栏组件契约；先匹配现代.NET占位异常，历史视觉行为作为可选扩展。 |
| `System.Windows.Forms.ToolStripOverflowButton` | M2 | ToolStrip.OverflowButton公开返回类型和可见溢出入口，不等于ToolStripOverflow窗口。 |
| `System.Windows.Forms.ToolStripPanelRow` | M2 | 工具条停靠行对象，具Bounds/Controls/布局/移动行为，不能只列ToolStripPanel。 |
## 其他会影响UI的附属契约

下列对象不等同于独立Control，不能把它们伪称为新增控件，也不能仅因它们不继承Control就遗漏。可见编辑对象、标准样式和复杂集合宜独立页；简单值、工具函数和辅助集合允许在父控件页有明确任务ID/场景归属。完整成员已保留在机器清单，是否单页与是否需要实现是两件事。

### 表格状态/集合与样式

| 精确类型 | 直接基类 | 归属建议/原始出处 |
| --- | --- | --- |
| `System.Windows.Forms.DataGridViewCellStyle` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewcellstyle?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.DataGridViewAdvancedBorderStyle` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewadvancedborderstyle?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.DataGridViewCellCollection` | `System.Windows.Forms.BaseCollection` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewcellcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.DataGridViewColumnCollection` | `System.Windows.Forms.BaseCollection` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewcolumncollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.DataGridViewRowCollection` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewrowcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.DataGridViewSelectedCellCollection` | `System.Windows.Forms.BaseCollection` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewselectedcellcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.DataGridViewSelectedColumnCollection` | `System.Windows.Forms.BaseCollection` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewselectedcolumncollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.DataGridViewSelectedRowCollection` | `System.Windows.Forms.BaseCollection` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewselectedrowcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.DataGridViewComboBoxCell+ObjectCollection` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.datagridviewcomboboxcell.objectcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |

### 属性检查器可见项/页签

| 精确类型 | 直接基类 | 归属建议/原始出处 |
| --- | --- | --- |
| `System.Windows.Forms.GridItem` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.griditem?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.GridItemCollection` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.griditemcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.Design.PropertyTab` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.design.propertytab?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.Design.EventsTab` | `System.Windows.Forms.Design.PropertyTab` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.design.eventstab?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.PropertyGridInternal.PropertiesTab` | `System.Windows.Forms.Design.PropertyTab` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.propertygridinternal.propertiestab?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.PropertyGrid+PropertyTabCollection` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.propertygrid.propertytabcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |

### 绑定与列表附属

| 精确类型 | 直接基类 | 归属建议/原始出处 |
| --- | --- | --- |
| `System.Windows.Forms.BindingManagerBase` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.bindingmanagerbase?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.PropertyManager` | `System.Windows.Forms.BindingManagerBase` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.propertymanager?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.BindingsCollection` | `System.Windows.Forms.BaseCollection` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.bindingscollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ControlBindingsCollection` | `System.Windows.Forms.BindingsCollection` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.controlbindingscollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.BindingMemberInfo` | `System.ValueType` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.bindingmemberinfo?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ListBindingHelper` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.listbindinghelper?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.AutoCompleteStringCollection` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.autocompletestringcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ImageListStreamer` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.imageliststreamer?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ListViewGroupCollection` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.listviewgroupcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ListViewInsertionMark` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.listviewinsertionmark?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.NumericUpDownAcceleration` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.numericupdownacceleration?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.NumericUpDownAccelerationCollection` | `System.MarshalByRefObject` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.numericupdownaccelerationcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.SelectionRange` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.selectionrange?view=windowsdesktop-10.0)；按本族明确任务归属 |

### 链接/布局/滚动

| 精确类型 | 直接基类 | 归属建议/原始出处 |
| --- | --- | --- |
| `System.Windows.Forms.LinkLabel+Link` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.linklabel.link?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.LinkLabel+LinkCollection` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.linklabel.linkcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.FlatButtonAppearance` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.flatbuttonappearance?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.FlowLayoutSettings` | `System.Windows.Forms.LayoutSettings` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.flowlayoutsettings?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.TableLayoutSettings` | `System.Windows.Forms.LayoutSettings` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.tablelayoutsettings?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.TableLayoutStyle` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.tablelayoutstyle?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ColumnStyle` | `System.Windows.Forms.TableLayoutStyle` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.columnstyle?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.RowStyle` | `System.Windows.Forms.TableLayoutStyle` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.rowstyle?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.TableLayoutColumnStyleCollection` | `System.Windows.Forms.TableLayoutStyleCollection` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.tablelayoutcolumnstylecollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.TableLayoutRowStyleCollection` | `System.Windows.Forms.TableLayoutStyleCollection` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.tablelayoutrowstylecollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.TableLayoutControlCollection` | `System.Windows.Forms.Control+ControlCollection` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.tablelayoutcontrolcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.LayoutSettings` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.layoutsettings?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.Layout.LayoutEngine` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.layout.layoutengine?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ScrollProperties` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.scrollproperties?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.HScrollProperties` | `System.Windows.Forms.ScrollProperties` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.hscrollproperties?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.VScrollProperties` | `System.Windows.Forms.ScrollProperties` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.vscrollproperties?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ScrollableControl+DockPaddingEdges` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.scrollablecontrol.dockpaddingedges?view=windowsdesktop-10.0)；按本族明确任务归属 |

### 列表/树/宿主集合

| 精确类型 | 直接基类 | 归属建议/原始出处 |
| --- | --- | --- |
| `System.Windows.Forms.Control+ControlCollection` | `System.Windows.Forms.Layout.ArrangedElementCollection` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.control.controlcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.Form+ControlCollection` | `System.Windows.Forms.Control+ControlCollection` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.form.controlcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.MdiClient+ControlCollection` | `System.Windows.Forms.Control+ControlCollection` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.mdiclient.controlcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.TabControl+ControlCollection` | `System.Windows.Forms.Control+ControlCollection` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.tabcontrol.controlcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.TabPage+TabPageControlCollection` | `System.Windows.Forms.Control+ControlCollection` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.tabpage.tabpagecontrolcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.TabControl+TabPageCollection` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.tabcontrol.tabpagecollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ComboBox+ObjectCollection` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.combobox.objectcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ListBox+ObjectCollection` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.listbox.objectcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ListBox+IntegerCollection` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.listbox.integercollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ListBox+SelectedIndexCollection` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.listbox.selectedindexcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ListBox+SelectedObjectCollection` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.listbox.selectedobjectcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.CheckedListBox+ObjectCollection` | `System.Windows.Forms.ListBox+ObjectCollection` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.checkedlistbox.objectcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.CheckedListBox+CheckedIndexCollection` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.checkedlistbox.checkedindexcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.CheckedListBox+CheckedItemCollection` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.checkedlistbox.checkeditemcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ListView+ListViewItemCollection` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.listview.listviewitemcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ListView+ColumnHeaderCollection` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.listview.columnheadercollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ListView+CheckedIndexCollection` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.listview.checkedindexcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ListView+CheckedListViewItemCollection` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.listview.checkedlistviewitemcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ListView+SelectedIndexCollection` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.listview.selectedindexcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ListView+SelectedListViewItemCollection` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.listview.selectedlistviewitemcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ListViewItem+ListViewSubItemCollection` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.listviewitem.listviewsubitemcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ToolStripPanel+ToolStripPanelRowCollection` | `System.Windows.Forms.Layout.ArrangedElementCollection` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstrippanel.toolstrippanelrowcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ToolBar+ToolBarButtonCollection` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolbar.toolbarbuttoncollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.StatusBar+StatusBarPanelCollection` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.statusbar.statusbarpanelcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.Menu+MenuItemCollection` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.menu.menuitemcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.GridColumnStylesCollection` | `System.Windows.Forms.BaseCollection` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.gridcolumnstylescollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.GridTableStylesCollection` | `System.Windows.Forms.BaseCollection` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.gridtablestylescollection?view=windowsdesktop-10.0)；按本族明确任务归属 |

### 绘图与工具条呈现契约

| 精确类型 | 直接基类 | 归属建议/原始出处 |
| --- | --- | --- |
| `System.Windows.Forms.ControlPaint` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.controlpaint?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.TextRenderer` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.textrenderer?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ButtonRenderer` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.buttonrenderer?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.CheckBoxRenderer` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.checkboxrenderer?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.RadioButtonRenderer` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.radiobuttonrenderer?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ComboBoxRenderer` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.comboboxrenderer?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.TextBoxRenderer` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.textboxrenderer?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ProgressBarRenderer` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.progressbarrenderer?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.GroupBoxRenderer` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.groupboxrenderer?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ScrollBarRenderer` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.scrollbarrenderer?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.TabRenderer` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.tabrenderer?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.TrackBarRenderer` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.trackbarrenderer?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ToolStripRenderer` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstriprenderer?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ToolStripSystemRenderer` | `System.Windows.Forms.ToolStripRenderer` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripsystemrenderer?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ToolStripProfessionalRenderer` | `System.Windows.Forms.ToolStripRenderer` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripprofessionalrenderer?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ToolStripManager` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.toolstripmanager?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ProfessionalColorTable` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.professionalcolortable?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.ProfessionalColors` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.professionalcolors?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.VisualStyles.VisualStyleRenderer` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.visualstyles.visualstylerenderer?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.VisualStyles.VisualStyleInformation` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.visualstyles.visualstyleinformation?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.VisualStyles.VisualStyleElement` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.visualstyles.visualstyleelement?view=windowsdesktop-10.0)；按本族明确任务归属 |

### 桌面环境和文件选择附属

| 精确类型 | 直接基类 | 归属建议/原始出处 |
| --- | --- | --- |
| `System.Windows.Forms.Cursor` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.cursor?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.Cursors` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.cursors?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.Screen` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.screen?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.SystemInformation` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.systeminformation?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.InputLanguage` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.inputlanguage?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.InputLanguageCollection` | `System.Collections.ReadOnlyCollectionBase` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.inputlanguagecollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.SendKeys` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.sendkeys?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.Help` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.help?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.FileDialogCustomPlace` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.filedialogcustomplace?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.FileDialogCustomPlacesCollection` | `System.Collections.ObjectModel.Collection<System.Windows.Forms.FileDialogCustomPlace>` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.filedialogcustomplacescollection?view=windowsdesktop-10.0)；按本族明确任务归属 |

### 浏览器文档/宿主

| 精确类型 | 直接基类 | 归属建议/原始出处 |
| --- | --- | --- |
| `System.Windows.Forms.HtmlDocument` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.htmldocument?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.HtmlElement` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.htmlelement?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.HtmlElementCollection` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.htmlelementcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.HtmlHistory` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.htmlhistory?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.HtmlWindow` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.htmlwindow?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.HtmlWindowCollection` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.htmlwindowcollection?view=windowsdesktop-10.0)；按本族明确任务归属 |
| `System.Windows.Forms.WebBrowserSiteBase` | `System.Object` | [官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.webbrowsersitebase?view=windowsdesktop-10.0)；按本族明确任务归属 |

### 打印对象（已在1095类型分母，不能因不同命名空间漏掉）

| 精确类型 | 直接基类 | 归属 |
| --- | --- | --- |
| `System.Drawing.Printing.PrinterSettings` | `System.Object` | PrintDocument/PrintDialog/PageSetupDialog/PrintPreviewControl；[官方 API](https://learn.microsoft.com/dotnet/api/system.drawing.printing.printersettings?view=windowsdesktop-10.0) |
| `System.Drawing.Printing.PageSettings` | `System.Object` | PrintDocument/PrintDialog/PageSetupDialog/PrintPreviewControl；[官方 API](https://learn.microsoft.com/dotnet/api/system.drawing.printing.pagesettings?view=windowsdesktop-10.0) |
| `System.Drawing.Printing.PrintController` | `System.Object` | PrintDocument/PrintDialog/PageSetupDialog/PrintPreviewControl；[官方 API](https://learn.microsoft.com/dotnet/api/system.drawing.printing.printcontroller?view=windowsdesktop-10.0) |
| `System.Drawing.Printing.StandardPrintController` | `System.Drawing.Printing.PrintController` | PrintDocument/PrintDialog/PageSetupDialog/PrintPreviewControl；[官方 API](https://learn.microsoft.com/dotnet/api/system.drawing.printing.standardprintcontroller?view=windowsdesktop-10.0) |
| `System.Drawing.Printing.PreviewPrintController` | `System.Drawing.Printing.PrintController` | PrintDocument/PrintDialog/PageSetupDialog/PrintPreviewControl；[官方 API](https://learn.microsoft.com/dotnet/api/system.drawing.printing.previewprintcontroller?view=windowsdesktop-10.0) |
| `System.Drawing.Printing.PreviewPageInfo` | `System.Object` | PrintDocument/PrintDialog/PageSetupDialog/PrintPreviewControl；[官方 API](https://learn.microsoft.com/dotnet/api/system.drawing.printing.previewpageinfo?view=windowsdesktop-10.0) |
| `System.Drawing.Printing.Margins` | `System.Object` | PrintDocument/PrintDialog/PageSetupDialog/PrintPreviewControl；[官方 API](https://learn.microsoft.com/dotnet/api/system.drawing.printing.margins?view=windowsdesktop-10.0) |
| `System.Drawing.Printing.PaperSize` | `System.Object` | PrintDocument/PrintDialog/PageSetupDialog/PrintPreviewControl；[官方 API](https://learn.microsoft.com/dotnet/api/system.drawing.printing.papersize?view=windowsdesktop-10.0) |
| `System.Drawing.Printing.PaperSource` | `System.Object` | PrintDocument/PrintDialog/PageSetupDialog/PrintPreviewControl；[官方 API](https://learn.microsoft.com/dotnet/api/system.drawing.printing.papersource?view=windowsdesktop-10.0) |
| `System.Drawing.Printing.PrinterResolution` | `System.Object` | PrintDocument/PrintDialog/PageSetupDialog/PrintPreviewControl；[官方 API](https://learn.microsoft.com/dotnet/api/system.drawing.printing.printerresolution?view=windowsdesktop-10.0) |
| `System.Windows.Forms.PrintControllerWithStatusDialog` | `System.Drawing.Printing.PrintController` | PrintDocument/PrintDialog/PageSetupDialog/PrintPreviewControl；[官方 API](https://learn.microsoft.com/dotnet/api/system.windows.forms.printcontrollerwithstatusdialog?view=windowsdesktop-10.0) |

## 框架外依赖和明确不另算控件的对象

| 范围 | 处理原则 |
| --- | --- |
| System.ComponentModel.Component/IComponent/IContainer/ISite/ISupportInitialize | BCL既有契约；继承和组件生命周期必须对齐，不在Lumina.Forms重复创造同名BCL框架。 |
| PropertyDescriptor/TypeDescriptor/TypeConverter/ICustomTypeDescriptor/ITypedList/UITypeEditor | 属性窗格与绑定的外部契约；源生成/静态描述符必须保持既有消费写法。AOT与跨平台策略写入对应页，不把它们都计成控件。 |
| System.Drawing.Font/Image/Icon/Graphics/Bitmap 与 System.Drawing.Printing | Windows基线和公开类型身份需保留；Linux/macOS不能直接假定System.Drawing.Common受支持，属于平台先决决策。 |
| UIAutomationProvider/UIAutomationTypes/Accessibility、COM/OLE接口 | 可访问性、拖放、ActiveX和浏览器的外部边界；在共同基础和平台专属清单验收，不伪造HWnd/COM支持。 |
| System.Windows.Forms.Design等独立设计器程序集 | 本轮运行时参考包不是VS完整设计器SDK；真实WinForms目标设计器往返是独立发布门槛，不能以少量Design命名空间类型覆盖全部设计器。 |
| EventArgs、委托、枚举、Attribute、TypeConverter派生 | 不是额外的视觉控件；保留全量成员和责任映射，在所属事件/属性/设计器任务验收。取消、坐标、序列化等语义不能因此被忽略。 |
| AccessibleObject派生及VisualStyleElement嵌套静态类 | 需要类型/成员责任与无障碍/绘图验收，但不逐个统计成新Control；不能让同名Button/TextBox等覆盖真正控件索引。 |

## 防止再次漏项的验收

- 🟡 全部1095类型已有完整类型名→责任文档/任务族映射，186个运行时类型有独立页，另有SDK生成的ApplicationConfiguration；其余自动归组仍需逐项人工复核，短名只能作为显示标签。
- ✅ 文档检查已覆盖上述Control/ToolStripItem/Component/CommonDialog/Menu/TaskDialog及DataGridViewElement闭包；本轮无缺页。新增SDK版本仍须显式比较，不能悄悄改变分母。
- ✅ 已为TaskDialog、ToolStripOverflowButton、PropertyGrid项、表格Cell/Column/Row补独立规划入口。这里只确认文档覆盖。
- ✅ 按完整类型处理同名冲突，例如 `System.Windows.Forms.Button` 与 `System.Windows.Forms.VisualStyles.VisualStyleElement+Button`；真正Button参考端有101属性/75事件/76方法。附件身份及总索引的成员数/候选类型状态均自动校验。
- 🟡 已有12个具体历史类型的真实构造PNSE证据，Menu为抽象；未执行的重载/成员仍待核对，Framework旧UI恢复已移出固定.NET10首发必需项。
- ⬜ 文档覆盖、签名匹配、真实运行、Native AOT运行、三平台行为分别报告；本审计没有把任何遗漏类型标为实现完成。

## 修改记录

- 2026-10-15：在原119页基础上重新枚举全部派生闭包，发现47个直接页面遗漏及若干附属契约归属不足；加入遗留控件PNSE实证，纠正“62个Control即可代表全部WinForms UI”的范围理解。此记录为规划审计，不是控件实现记录。
- 2026-10-15：补齐68组独立文档/API附件，并统一索引、责任映射与1948条正文任务。387份Markdown结构检查通过；1920条任务待验、28条局部实现、0条完整实现验收通过。检查可复现命令为 `./tools/compatibility/Validate-Roadmap.ps1 -ExportTasks`。
