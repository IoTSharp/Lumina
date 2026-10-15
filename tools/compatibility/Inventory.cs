using System.Globalization;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

// Read-only metadata inventory. Signature matches are deliberately NOT reported as supported API.
internal static class Program
{
    const BindingFlags Declared = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
    static readonly UTF8Encoding Utf8 = new(false);
    // Resolve document names to full identities. VisualStyleElement has nested types named
    // Button, TextBox, ComboBox, etc.; selecting by leaf name silently inventories the wrong API.
    static string FocusType(string name) => name switch
    {
        "ComponentEditorForm" or "ComponentEditorPage" or "PropertyTab" => "System.Windows.Forms.Design." + name,
        "PrintDocument" => "System.Drawing.Printing.PrintDocument",
        "ListViewSubItem" => "System.Windows.Forms.ListViewItem+ListViewSubItem",
        "MenuItemCollection" => "System.Windows.Forms.Menu+MenuItemCollection",
        _ => "System.Windows.Forms." + name
    };

    record Api(string Kind, string Name, string Signature, string DeclaringType, string Visibility, string Metadata, string Slot);
    record Row(string Id, string Type, string Kind, string Name, string DeclaringType, bool Inherited, string Visibility, string Signature, string Metadata, bool CandidateTypeExists, bool SignatureMatches, bool TypeRelationshipMatches, string Windows, string Linux, string MacOS, string Evidence);
    record TypeRow(string Type, string Kind, string BaseType, string Interfaces, bool Abstract, bool Obsolete, bool CandidatePresent, string CandidateBase, bool RelationshipsMatch, int PublicProperties, int PublicEvents, int PublicMethods, int ProtectedMembers);

    static string Normalize(string value) => value.Replace("Lumina.Forms.", "System.Windows.Forms.", StringComparison.Ordinal);
    static string TypeName(Type? type)
    {
        if (type is null) return "";
        if (type.IsGenericParameter) return (type.DeclaringMethod is null ? "!" : "!!") + type.GenericParameterPosition;
        if (type.IsByRef) return TypeName(type.GetElementType()) + "&";
        if (type.IsPointer) return TypeName(type.GetElementType()) + "*";
        if (type.IsArray) return TypeName(type.GetElementType()) + "[" + new string(',', type.GetArrayRank() - 1) + "]";
        if (type.IsGenericType) return Normalize(type.GetGenericTypeDefinition().FullName ?? type.Name) + "<" + string.Join(",", type.GetGenericArguments().Select(TypeName)) + ">";
        return Normalize(type.FullName ?? type.Name);
    }
    static string Literal(object? value) => value switch
    {
        null => "null",
        string text => JsonSerializer.Serialize(text),
        char c => "'" + (c switch { '\0' => "\\0", '\r' => "\\r", '\n' => "\\n", '\t' => "\\t", '\\' => "\\\\", '\'' => "\\'", _ => char.IsControl(c) ? "\\u" + ((int)c).ToString("x4") : c.ToString() }) + "'",
        bool b => b ? "true" : "false",
        Type t => TypeName(t),
        IFormattable f => f.ToString(null, CultureInfo.InvariantCulture) ?? "",
        _ => value.ToString() ?? ""
    };
    static string Visibility(MethodBase? method) => method is null ? "" : method.IsPublic ? "public" : method.IsFamily ? "protected" : method.IsFamilyOrAssembly ? "protected internal" : "private";
    static bool Visible(MethodBase? method) => method is not null && (method.IsPublic || method.IsFamily || method.IsFamilyOrAssembly);
    static string Shape(MethodBase method) => (method.IsStatic ? "static " : "") + (method.IsAbstract ? "abstract " : method.IsVirtual && !method.IsFinal ? "virtual " : "");
    static string Parameter(ParameterInfo p) => (p.IsOut ? "out " : p.ParameterType.IsByRef ? p.IsIn ? "in " : "ref " : "") + TypeName(p.ParameterType.IsByRef ? p.ParameterType.GetElementType() : p.ParameterType) + " " + p.Name + (p.IsOptional ? " = " + Literal(p.RawDefaultValue) : "");
    static string Parameters(MethodBase method) => string.Join(", ", method.GetParameters().Select(Parameter));
    static string Attrs(MemberInfo member)
    {
        string[] names = ["DefaultValueAttribute", "DefaultEventAttribute", "DefaultPropertyAttribute", "DesignerSerializationVisibilityAttribute", "BrowsableAttribute", "BindableAttribute", "LocalizableAttribute", "ObsoleteAttribute", "TypeConverterAttribute", "EditorAttribute", "FlagsAttribute"];
        return string.Join("; ", member.GetCustomAttributesData().Where(a => names.Contains(a.AttributeType.Name)).Select(a => a.AttributeType.Name + "(" + string.Join(",", a.ConstructorArguments.Select(x => Literal(x.Value))) + ")"));
    }
    static string Constraints(MethodInfo method) => string.Join(" ", method.GetGenericArguments().Select(t => "where " + TypeName(t) + ": " + t.GenericParameterAttributes + ";" + string.Join(",", t.GetGenericParameterConstraints().Select(TypeName))));
    static IEnumerable<Api> DeclaredApi(Type type)
    {
        string owner = TypeName(type);
        foreach (ConstructorInfo ctor in type.GetConstructors(Declared).Where(Visible))
            yield return new("Constructor", ".ctor", Visibility(ctor) + " " + Shape(ctor) + type.Name + "(" + Parameters(ctor) + ")", owner, ctor.IsPublic ? "public" : "protected", Attrs(ctor), string.Join(",", ctor.GetParameters().Select(p => TypeName(p.ParameterType))));
        foreach (PropertyInfo p in type.GetProperties(Declared).Where(p => Visible(p.GetMethod) || Visible(p.SetMethod)))
        {
            var visible = new[] { p.GetMethod, p.SetMethod }.Where(Visible).Cast<MethodInfo>().ToArray();
            string index = p.GetIndexParameters().Length == 0 ? "" : "[" + string.Join(", ", p.GetIndexParameters().Select(Parameter)) + "]";
            string accessors = (Visible(p.GetMethod) ? Visibility(p.GetMethod) + " get; " : "") + (Visible(p.SetMethod) ? Visibility(p.SetMethod) + " set; " : "");
            yield return new("Property", p.Name, Shape(visible[0]) + TypeName(p.PropertyType) + " " + p.Name + index + " { " + accessors + "}", owner, visible.Any(x => x.IsPublic) ? "public" : "protected", Attrs(p), p.Name + "[" + string.Join(",", p.GetIndexParameters().Select(p => TypeName(p.ParameterType))) + "]");
        }
        foreach (EventInfo e in type.GetEvents(Declared).Where(e => Visible(e.AddMethod)))
            yield return new("Event", e.Name, Visibility(e.AddMethod) + " " + Shape(e.AddMethod!) + "event " + TypeName(e.EventHandlerType) + " " + e.Name, owner, e.AddMethod!.IsPublic ? "public" : "protected", Attrs(e), e.Name);
        foreach (MethodInfo m in type.GetMethods(Declared).Where(m => Visible(m) && !m.IsSpecialName))
            yield return new("Method", m.Name, Visibility(m) + " " + Shape(m) + TypeName(m.ReturnType) + " " + m.Name + (m.IsGenericMethod ? "<" + string.Join(",", m.GetGenericArguments().Select(TypeName)) + ">" : "") + "(" + Parameters(m) + ")" + (m.IsGenericMethod ? " " + Constraints(m) : ""), owner, m.IsPublic ? "public" : "protected", Attrs(m), m.Name + "`" + m.GetGenericArguments().Length + "(" + string.Join(",", m.GetParameters().Select(p => TypeName(p.ParameterType))) + ")");
        foreach (FieldInfo f in type.GetFields(Declared).Where(f => f.Name != "value__" && (f.IsPublic || f.IsFamily || f.IsFamilyOrAssembly)))
            yield return new("Field", f.Name, (f.IsPublic ? "public " : "protected ") + (f.IsLiteral ? "const " : (f.IsStatic ? "static " : "") + (f.IsInitOnly ? "readonly " : "")) + TypeName(f.FieldType) + " " + f.Name + (f.IsLiteral ? " = " + Literal(f.GetRawConstantValue()) : ""), owner, f.IsPublic ? "public" : "protected", Attrs(f), f.Name);
    }
    static Api[] EffectiveApi(Type type)
    {
        List<Api> result = [];
        HashSet<string> seen = new(StringComparer.Ordinal);
        bool inherited = false;
        for (Type? level = type; level is not null && level.FullName != "System.Object" && level.FullName != "System.MarshalByRefObject"; level = level.BaseType)
        {
            foreach (Api api in DeclaredApi(level))
            {
                if (inherited && api.Kind == "Constructor") continue;
                // Most-derived declaration wins for hidden/overridden slots, including covariant returns.
                if (seen.Add(api.Kind + ":" + api.Slot)) result.Add(api);
            }
            inherited = true;
        }
        if (type.IsInterface)
        {
            foreach (Type contract in type.GetInterfaces().Where(t => t.IsVisible).OrderBy(TypeName))
                foreach (Api api in DeclaredApi(contract))
                    if (seen.Add(api.Kind + ":" + api.Slot)) result.Add(api);
        }
        return result.OrderBy(x => x.Visibility).ThenBy(x => x.Kind).ThenBy(x => x.Name).ThenBy(x => x.Signature, StringComparer.Ordinal).ToArray();
    }
    static string Id(string type, Api api) => "API-" + Convert.ToHexString(SHA256.HashData(Utf8.GetBytes(type + "|" + api.Kind + "|" + api.Signature)))[..16];
    static string Csv(string? s) => "\"" + (s ?? "").Replace("\"", "\"\"") + "\"";
    static string Cell(string text) => text.Replace("|", "\\|").Replace("\r", " ").Replace("\n", " ").Replace("`", "&#96;");
    static string HashFile(string path) => Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(path))).ToLowerInvariant();
    static string Kind(Type type) => type.IsEnum ? "Enum" : type.IsInterface ? "Interface" : type.BaseType?.FullName == "System.MulticastDelegate" ? "Delegate" : "Type";

    static int Main(string[] args)
    {
        if (args.Length != 2) throw new ArgumentException("Expected repository root and fixed reference pack version.");
        string root = Path.GetFullPath(args[0]);
        string version = args[1];
        string dotnetRoot = Directory.GetParent(System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory().TrimEnd(Path.DirectorySeparatorChar))!.Parent!.Parent!.FullName;
        string desktopRef = Path.Combine(dotnetRoot, "packs", "Microsoft.WindowsDesktop.App.Ref", version, "ref", "net10.0");
        string coreRef = Path.Combine(dotnetRoot, "packs", "Microsoft.NETCore.App.Ref", version, "ref", "net10.0");
        string candidatePath = Path.Combine(root, "Lumina.Forms", "bin", "Release", "net10.0", "Lumina.Forms.dll");
        string candidateCore = Path.Combine(root, "Lumina.Core", "bin", "Release", "net10.0", "Lumina.Core.dll");
        string output = Path.Combine(root, "docs", "roadmap", "api");
        Directory.CreateDirectory(output);
        string[] refs = Directory.GetFiles(desktopRef, "*.dll").Concat(Directory.GetFiles(coreRef, "*.dll")).ToArray();
        using var baselineContext = new MetadataLoadContext(new PathAssemblyResolver(refs), "System.Runtime");
        using var candidateContext = new MetadataLoadContext(new PathAssemblyResolver(refs.Concat([candidatePath, candidateCore])), "System.Runtime");
        string[] baselinePaths = [Path.Combine(desktopRef, "System.Windows.Forms.dll"), Path.Combine(desktopRef, "System.Windows.Forms.Primitives.dll"), Path.Combine(desktopRef, "System.Drawing.Common.dll")];
        var baseline = baselinePaths.Select(baselineContext.LoadFromAssemblyPath).SelectMany(a => a.GetExportedTypes()).Where(t => t.Namespace?.StartsWith("System.Windows.Forms", StringComparison.Ordinal) == true || t.Namespace == "System.Drawing.Printing").DistinctBy(TypeName).OrderBy(TypeName, StringComparer.Ordinal).ToArray();
        var candidate = candidateContext.LoadFromAssemblyPath(candidatePath).GetExportedTypes().Where(t => t.Namespace?.StartsWith("Lumina.Forms", StringComparison.Ordinal) == true).ToDictionary(TypeName);
        List<Row> allRows = [];
        List<TypeRow> typeRows = [];
        Dictionary<string, Api[]> apis = new();
        Dictionary<string, Row[]> rowsByType = new();
        foreach (Type type in baseline)
        {
            string fullName = TypeName(type);
            candidate.TryGetValue(fullName, out Type? counterpart);
            Api[] sourceApi = EffectiveApi(type);
            var available = counterpart is null ? new HashSet<string>() : EffectiveApi(counterpart).Select(x => x.Kind + ":" + x.Signature).ToHashSet();
            string[] interfaces = type.GetInterfaces().Where(t => t.IsVisible).Select(TypeName).Order().ToArray();
            bool relationships = counterpart is not null && TypeName(type.BaseType) == TypeName(counterpart.BaseType) && interfaces.All(i => counterpart.GetInterfaces().Where(t => t.IsVisible).Select(TypeName).Contains(i));
            Row[] rows = sourceApi.Select(a => new Row(Id(fullName, a), fullName, a.Kind, a.Name, a.DeclaringType, fullName != a.DeclaringType, a.Visibility, a.Signature, a.Metadata, counterpart is not null, available.Contains(a.Kind + ":" + a.Signature), relationships, "unverified", "unverified", "unverified", "")).ToArray();
            allRows.AddRange(rows);
            apis[fullName] = sourceApi;
            rowsByType[fullName] = rows;
            typeRows.Add(new(fullName, Kind(type), TypeName(type.BaseType), string.Join(";", interfaces), type.IsAbstract, type.GetCustomAttributesData().Any(a => a.AttributeType.Name == "ObsoleteAttribute"), counterpart is not null, TypeName(counterpart?.BaseType), relationships, rows.Count(a => a.Visibility == "public" && a.Kind == "Property"), rows.Count(a => a.Visibility == "public" && a.Kind == "Event"), rows.Count(a => a.Visibility == "public" && a.Kind == "Method"), rows.Count(a => a.Visibility == "protected")));
        }
        List<string> csv = ["Id,Type,Kind,Name,DeclaringType,Inherited,Visibility,Signature,BaselineMetadata,CandidateTypeExists,SignatureMatches,TypeRelationshipMatches,WindowsVerification,LinuxVerification,MacOSVerification,Evidence"];
        csv.AddRange(allRows.Select(r => string.Join(",", new[] { r.Id, r.Type, r.Kind, r.Name, r.DeclaringType, r.Inherited.ToString(), r.Visibility, r.Signature, r.Metadata, r.CandidateTypeExists.ToString(), r.SignatureMatches.ToString(), r.TypeRelationshipMatches.ToString(), r.Windows, r.Linux, r.MacOS, r.Evidence }.Select(Csv))));
        File.WriteAllLines(Path.Combine(output, "members.csv"), csv, Utf8);
        List<string> interfaceCsv = ["Id,Type,Interface,Kind,Signature,CandidateImplementsInterface,WindowsVerification,LinuxVerification,MacOSVerification,Evidence"];
        foreach (Type type in baseline)
        {
            string owner = TypeName(type);
            candidate.TryGetValue(owner, out Type? counterpart);
            var candidateInterfaces = counterpart?.GetInterfaces().Where(t => t.IsVisible).Select(TypeName).ToHashSet() ?? [];
            foreach (Type contract in type.GetInterfaces().Where(t => t.IsVisible).OrderBy(TypeName))
            {
                string contractName = TypeName(contract);
                foreach (Api api in EffectiveApi(contract).Where(a => a.Visibility == "public"))
                {
                    string id = "IFC-" + Convert.ToHexString(SHA256.HashData(Utf8.GetBytes(owner + "|" + contractName + "|" + api.Signature)))[..16];
                    interfaceCsv.Add(string.Join(",", new[] { id, owner, contractName, api.Kind, api.Signature, candidateInterfaces.Contains(contractName).ToString(), "unverified", "unverified", "unverified", "" }.Select(Csv)));
                }
            }
        }
        File.WriteAllLines(Path.Combine(output, "interfaces.csv"), interfaceCsv, Utf8);
        var options = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(Path.Combine(output, "types.json"), JsonSerializer.Serialize(typeRows, options), Utf8);
        File.WriteAllText(Path.Combine(output, "baseline.json"), JsonSerializer.Serialize(new
        {
            referencePackVersion = version,
            date = "2026-10-15",
            baseline = baselinePaths.Select(p => new { file = Path.GetFileName(p), sha256 = HashFile(p), assembly = baselineContext.LoadFromAssemblyPath(p).FullName }),
            candidate = new { path = "Lumina.Forms/bin/Release/net10.0/Lumina.Forms.dll", sha256 = HashFile(candidatePath) },
            baselineTypeCount = typeRows.Count,
            effectiveMemberRows = allRows.Count,
            interfaceMemberRows = interfaceCsv.Count - 1,
            note = "Inherited APIs repeated per type. Shape matches are not supported API. No runtime behavior or platform verification performed by this exporter."
        }, options), Utf8);

        string[] focusNames = Directory.GetFiles(Path.Combine(root, "docs", "roadmap", "controls"), "*.md").Select(Path.GetFileNameWithoutExtension).Cast<string>().Order(StringComparer.Ordinal).ToArray();
        var baselineByName = baseline.ToDictionary(TypeName, StringComparer.Ordinal);
        List<string> pageMap = ["DocumentType,Type,Source,CandidatePresent,RelationshipsMatch,PublicProperties,PublicEvents,PublicMethods,ProtectedMembers"];
        foreach (string name in focusNames)
        {
            string expectedType = FocusType(name);
            baselineByName.TryGetValue(expectedType, out Type? type);
            if (type is null)
            {
                if (name != "ApplicationConfiguration") throw new InvalidOperationException("No exact baseline type for document " + name + ": " + expectedType);
                string detail = "真实 WinForms 的 ApplicationConfiguration 由 SDK 为消费项目生成，不是 System.Windows.Forms 运行时程序集的导出类型；核对生成的 Initialize、DPI、默认字体、视觉样式配置和消费源码。";
                File.WriteAllText(Path.Combine(output, name + ".md"), "# " + name + " 成员基线\n\n" + detail + "\n\n[控件任务](../controls/" + name + ".md) · [计量规则](../coverage.md)\n", Utf8);
                pageMap.Add(string.Join(",", new[] { name, expectedType, "sdk-generated", candidate.ContainsKey(expectedType).ToString(), "", "", "", "", "" }.Select(Csv)));
                continue;
            }
            string fullName = TypeName(type);
            var typeRow = typeRows.Single(t => t.Type == fullName);
            pageMap.Add(string.Join(",", new[] { name, fullName, "reference-assembly", typeRow.CandidatePresent.ToString(), typeRow.RelationshipsMatch.ToString(), typeRow.PublicProperties.ToString(), typeRow.PublicEvents.ToString(), typeRow.PublicMethods.ToString(), typeRow.ProtectedMembers.ToString() }.Select(Csv)));
            Row[] rows = rowsByType[fullName];
            StringBuilder page = new("# " + name + "：逐成员待办\n\n");
            page.AppendLine("自动生成于 2026-10-15；固定 Microsoft.WindowsDesktop.App.Ref **" + version + "**。每一行都是独立待办，包含继承成员；不以正文任务数量代替全量清单。");
            page.AppendLine("\n[控件任务](../controls/" + name + ".md) · [计量规则](../coverage.md) · [全量机器清单](members.csv) · [接口契约（含显式实现）](interfaces.csv) · [基线指纹](baseline.json)\n");
            page.AppendLine("类型：`" + fullName + "`；基类：`" + typeRow.BaseType + "`。Lumina 同名类型：" + (typeRow.CandidatePresent ? "🟡 存在" : "⬜ 缺失") + "；基类及基线接口形状：" + (typeRow.RelationshipsMatch ? "🟡 初步匹配，待源码消费验证" : "⬜ 未匹配") + "。");
            page.AppendLine("\n公开属性 **" + typeRow.PublicProperties + "**，公开事件 **" + typeRow.PublicEvents + "**，公开方法 **" + typeRow.PublicMethods + "**；受保护扩展成员 **" + typeRow.ProtectedMembers + "**。构造、字段和常量另列，均不能忽略。继承同一成员在每个具体控件上都要验收，但全库统计使用不同口径，见计量规则。\n");
            page.AppendLine("`🟡签名匹配` 仅指规范化命名空间后的元数据形状，**不是实现完成**。所有运行支持状态均为 `⬜未验收`；实际支持须补源码消费、默认值、运行与 AOT 证据。默认值栏是特性元数据，缺少特性不代表默认值不存在。\n");
            foreach (string visibility in new[] { "public", "protected" })
            {
                page.AppendLine("## " + (visibility == "public" ? "公开成员" : "受保护扩展成员（独立验收，不稀释公开 API 指标）") + "\n");
                page.AppendLine("| ID | 类别 | 签名 | 声明位置 / 元数据 | 当前形状 | Windows / Linux / macOS |\n| --- | --- | --- | --- | --- | --- |");
                foreach (Row row in rows.Where(r => r.Visibility == visibility))
                    page.AppendLine("| " + row.Id + " | " + row.Kind + " | `" + Cell(row.Signature) + "` | `" + Cell(row.DeclaringType) + "` " + Cell(row.Metadata) + " | " + (row.SignatureMatches ? "🟡 签名匹配" : "⬜ 缺少精确签名") + " | ⬜ / ⬜ / ⬜ |");
                page.AppendLine();
            }
            File.WriteAllText(Path.Combine(output, name + ".md"), page.ToString().TrimEnd() + "\n", Utf8);
        }
        File.WriteAllLines(Path.Combine(output, "page-map.csv"), pageMap, Utf8);
        Console.WriteLine(JsonSerializer.Serialize(new { baselineTypes = typeRows.Count, effectiveMemberRows = allRows.Count, focusedPages = focusNames.Length, output }, options));
        return 0;
    }
}
