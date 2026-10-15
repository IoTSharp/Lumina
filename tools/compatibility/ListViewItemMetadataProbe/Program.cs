using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.Json;
#if REFERENCE
using System.Windows.Forms;
#else
using Lumina.Forms;
#endif
using SubItem =
#if REFERENCE
    System.Windows.Forms.ListViewItem.ListViewSubItem;
#else
    Lumina.Forms.ListViewItem.ListViewSubItem;
#endif
using SubItems =
#if REFERENCE
    System.Windows.Forms.ListViewItem.ListViewSubItemCollection;
#else
    Lumina.Forms.ListViewItem.ListViewSubItemCollection;
#endif

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
        Console.OutputEncoding = new UTF8Encoding(false);
        string version = typeof(ListViewItem).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "";
#if REFERENCE
        if (version.Split('+')[0] != "10.0.11")
            throw new InvalidOperationException($"Expected WinForms 10.0.11; found {version}.");
#endif
        var observations = new List<object>();
        ObserveMetadata(observations);
        foreach (string construction in new[] { "default", "null-text", "text", "null-array", "empty-array", "array" })
            ObserveDefaults(construction, observations);
        string?[] texts = [null, "", "plain", "中文项目", "emoji 😀", "two\nlines", "two\r\nlines", "\t spaced \t", "nul\0tail", "אבג العربية", "e\u0301", "{braces}: text"];
        foreach (string property in new[] { "Name", "ToolTipText" })
        foreach (string? text in texts)
        foreach (bool populated in new[] { false, true })
            ObserveStrings(property, text, populated, observations);
        ObserveTags(observations);
        foreach (string shape in new[] { "fresh", "property-cleared", "external-cleared", "populated", "removed-primary", "removed-only", "replacement", "duplicates", "cross-owner" })
        foreach (string operation in new[] { "read-name", "write-name", "write-name-null", "write-name-empty", "read-tag", "write-tag", "read-tooltip", "write-tooltip" })
            ObserveCollection(shape, operation, observations);
        ObserveRelationship(observations);
        ObserveCaches(observations);
        ObserveHiddenMembers(observations);
        Console.WriteLine(JsonSerializer.Serialize(new { version, uiCulture = CultureInfo.CurrentUICulture.Name, observations }));
    }

    private static void ObserveMetadata(List<object> observations)
    {
        foreach (string name in new[] { "Name", "Tag", "ToolTipText" })
        {
            PropertyInfo? property = typeof(ListViewItem).GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            observations.Add(new
            {
                scenario = "metadata", name, declared = property is not null,
                type = property?.PropertyType.FullName,
                getter = MethodShape(property?.GetMethod), setter = MethodShape(property?.SetMethod),
                allowNull = property?.GetCustomAttribute<AllowNullAttribute>() is not null,
                attributes = property is null ? [] : property.GetCustomAttributes().Select(AttributeShape).Where(value => value is not null).OrderBy(value => value, StringComparer.Ordinal).ToArray()
            });
        }
    }

    private static object? MethodShape(MethodInfo? method) => method is null ? null : new
    {
        method.IsPublic, method.IsVirtual, method.IsFinal, method.IsStatic,
        parameters = method.GetParameters().Select(parameter => new { parameter.Name, type = parameter.ParameterType.FullName }).ToArray()
    };

    private static string? AttributeShape(Attribute attribute) => attribute switch
    {
        BrowsableAttribute value => "Browsable=" + value.Browsable,
        LocalizableAttribute value => "Localizable=" + value.IsLocalizable,
        BindableAttribute value => "Bindable=" + value.Bindable + ";Direction=" + value.Direction,
        DefaultValueAttribute value => "DefaultValue=" + (value.Value is null ? "<null>" : value.Value.GetType().FullName + ":" + value.Value),
        DesignerSerializationVisibilityAttribute value => "DesignerSerializationVisibility=" + value.Visibility,
        TypeConverterAttribute value => "TypeConverter=" + value.ConverterTypeName,
        CategoryAttribute value => "Category=" + value.Category,
        _ => null
    };

    private static ListViewItem Create(string construction) => construction switch
    {
        "null-text" => new ListViewItem((string?)null),
        "text" => new ListViewItem("primary"),
        "null-array" => new ListViewItem((string?[]?)null),
        "empty-array" => new ListViewItem(Array.Empty<string?>()),
        "array" => new ListViewItem(new[] { "primary", "detail" }),
        _ => new ListViewItem()
    };

    private static void ObserveDefaults(string construction, List<object> observations)
    {
        var owner = Create(construction);
        var view = new SubItems(owner);
        int countBefore = view.Count;
        string name = owner.Name;
        int countAfterName = view.Count;
        object? tag = owner.Tag;
        string tooltip = owner.ToolTipText;
        observations.Add(new
        {
            scenario = "defaults-" + construction, countBefore, name, countAfterName,
            tagNull = tag is null, tooltip, countAfter = view.Count, owner.Text,
            firstName = view.Count == 0 ? null : view[0].Name,
            firstTagNull = view.Count == 0 || view[0].Tag is null
        });
    }

    private static void ObserveStrings(string property, string? text, bool populated, List<object> observations)
    {
        var owner = populated ? new ListViewItem(new[] { "primary", "detail" }) : new ListViewItem();
        var view = new SubItems(owner);
        string? assigned = text is null ? null : new string(text.ToCharArray());
        int before = view.Count;
        SetString(owner, property, assigned);
        string first = GetString(owner, property);
        int afterFirst = view.Count;
        SetString(owner, property, assigned);
        string repeated = GetString(owner, property);
        observations.Add(new
        {
            scenario = "strings", property, inputText = text, populated, before, afterFirst, afterRepeat = view.Count,
            first, repeated, sameAssigned = ReferenceEquals(assigned, first), sameRepeated = ReferenceEquals(first, repeated),
            owner.Text, firstName = view.Count == 0 ? null : view[0].Name
        });
    }

    private static void SetString(ListViewItem owner, string property, string? value)
    {
        if (property == "Name") owner.Name = value;
        else owner.ToolTipText = value;
    }

    private static string GetString(ListViewItem owner, string property) => property == "Name" ? owner.Name : owner.ToolTipText;

    private static void ObserveTags(List<object> observations)
    {
        var poison = new PoisonTag();
        string text = new("tag text 中文".ToCharArray());
        object?[] values = [null, new object(), text, poison, poison, null, text];
        var owner = new ListViewItem();
        var view = new SubItems(owner);
        for (int index = 0; index < values.Length; index++)
        {
            object? value = values[index];
            owner.Tag = value;
            object? first = owner.Tag;
            owner.Tag = value;
            observations.Add(new
            {
                scenario = "tags", index, sameAssigned = ReferenceEquals(value, first),
                sameRepeated = ReferenceEquals(first, owner.Tag), tagNull = owner.Tag is null,
                count = view.Count, poison.Reads, poison.Disposals
            });
        }
        owner.SubItems[0].Tag = poison;
        owner.Tag = text;
        observations.Add(new
        {
            scenario = "item-and-primary-tags", sameItemTag = ReferenceEquals(text, owner.Tag),
            samePrimaryTag = ReferenceEquals(poison, view[0].Tag), poison.Reads, poison.Disposals
        });
    }

    private static void ObserveCollection(string shape, string operation, List<object> observations)
    {
        var owner = new ListViewItem();
        var first = new SubItems(owner);
        var second = new SubItems(owner);
        var primary = new SubItem(null, "primary") { Name = "primary-name" };
        var detail = new SubItem(null, "detail") { Name = "detail-name" };
        var replacement = new SubItem(null, "replacement") { Name = "replacement-name" };
        var crossOwner = new ListViewItem();
        var crossView = new SubItems(crossOwner);
        if (shape == "property-cleared") owner.SubItems.Clear();
        else if (shape != "fresh")
        {
            first.Add(primary);
            if (shape != "removed-only") first.Add(detail);
            if (shape == "external-cleared") first.Clear();
            if (shape is "removed-primary" or "removed-only") first.RemoveAt(0);
            if (shape == "replacement") first[0] = replacement;
            if (shape == "duplicates") first.Add(primary);
            if (shape == "cross-owner") crossView.Add(primary);
        }
        SubItem[] known = [primary, detail, replacement];
        SubItem[] beforeItems = Enumerable.Range(0, first.Count).Select(index => first[index]).ToArray();
        int countBefore = first.Count;
        string textBefore = owner.Text;
        IEnumerator firstEnumerator = first.GetEnumerator();
        IEnumerator secondEnumerator = ((IEnumerable)second).GetEnumerator();
        object firstMoveBefore = EnumMove(firstEnumerator, known);
        object secondMoveBefore = EnumMove(secondEnumerator, known);
        var poison = new PoisonTag();
        object action = Capture(() =>
        {
            switch (operation)
            {
                case "read-name": return owner.Name;
                case "write-name": owner.Name = "changed-name"; break;
                case "write-name-null": owner.Name = null; break;
                case "write-name-empty": owner.Name = ""; break;
                case "read-tag": return owner.Tag is null;
                case "write-tag": owner.Tag = poison; break;
                case "read-tooltip": return owner.ToolTipText;
                case "write-tooltip": owner.ToolTipText = "tooltip 中文\r\n\0end"; break;
            }
            return null;
        });
        // Snapshot after the operation before observing Name, whose getter may itself have effects.
        int countAfterAction = first.Count;
        string[] namesAfterAction = Enumerable.Range(0, first.Count).Select(index => first[index].Name).ToArray();
        object firstCurrent = EnumCurrent(firstEnumerator, known);
        object secondCurrent = EnumCurrent(secondEnumerator, known);
        object firstMove = EnumMove(firstEnumerator, known);
        object secondMove = EnumMove(secondEnumerator, known);
        object firstReset = Capture(() => { firstEnumerator.Reset(); return null; });
        object secondReset = Capture(() => { secondEnumerator.Reset(); return null; });
        string nameAfter = owner.Name;
        observations.Add(new
        {
            scenario = "collection", shape, operation, countBefore, textBefore,
            firstMoveBefore, secondMoveBefore, action, countAfterAction, namesAfterAction,
            sameItemsAfterAction = countBefore == countAfterAction && beforeItems.Select((entry, index) => ReferenceEquals(entry, first[index])).All(value => value),
            firstCurrent, secondCurrent, firstMove, secondMove, firstReset, secondReset,
            nameAfter, countAfterRead = first.Count, secondCount = second.Count,
            textAfter = owner.Text, tooltipAfter = owner.ToolTipText,
            itemTag = ReferenceEquals(poison, owner.Tag), primary.Name, detailName = detail.Name,
            replacementName = replacement.Name, crossCount = crossView.Count, crossName = crossView.Count == 0 ? null : crossView[0].Name,
            poison.Reads, poison.Disposals
        });
    }

    private static void ObserveRelationship(List<object> observations)
    {
        var owner = new ListViewItem(new[] { "primary", "detail" });
        var first = owner.SubItems;
        var second = new SubItems(owner);
        SubItem original = first[0];
        for (int stage = 0; stage < 8; stage++)
        {
            switch (stage)
            {
                case 0: owner.Name = "item-name"; break;
                case 1: first[0].Name = "primary-name"; break;
                case 2: owner.Text = "changed-text"; break;
                case 3: first.RemoveAt(0); break;
                case 4: first[0] = new SubItem(null, "replacement") { Name = "replacement-name" }; break;
                case 5: first.Add(first[0]); owner.Name = "duplicate-name"; break;
                case 6: first.Clear(); break;
                case 7: first.Add(original); break;
            }
            int countBeforeName = first.Count;
            string name = owner.Name;
            observations.Add(new
            {
                scenario = "relationship", stage, countBeforeName, name, countAfterName = first.Count,
                owner.Text, originalName = original.Name, originalText = original.Text,
                names = Enumerable.Range(0, first.Count).Select(index => first[index].Name).ToArray(),
                texts = Enumerable.Range(0, first.Count).Select(index => first[index].Text).ToArray(),
                sameViews = first.Count == second.Count && Enumerable.Range(0, first.Count).All(index => ReferenceEquals(first[index], second[index]))
            });
        }
    }

    private static void ObserveCaches(List<object> observations)
    {
        foreach (string operation in new[] { "read-name", "write-name", "write-tag", "write-tooltip" })
        {
            var owner = new ListViewItem(new[] { "primary", "earlier", "later" });
            var first = owner.SubItems;
            var second = new SubItems(owner);
            first[0].Name = "primary";
            first[1].Name = "earlier";
            first[2].Name = "target";
            int firstBefore = first.IndexOfKey("target");
            int secondBefore = second.IndexOfKey("earlier");
            first[1].Name = "target";
            switch (operation)
            {
                case "read-name": _ = owner.Name; break;
                case "write-name": owner.Name = "target"; break;
                case "write-tag": owner.Tag = new PoisonTag(); break;
                case "write-tooltip": owner.ToolTipText = "tip"; break;
            }
            observations.Add(new
            {
                scenario = "key-caches", operation, firstBefore, secondBefore,
                firstAfter = first.IndexOfKey("target"), secondAfter = second.IndexOfKey("target"), owner.Name, count = first.Count
            });
        }
        var emptyOwner = new ListViewItem();
        var emptyView = new SubItems(emptyOwner);
        IEnumerator oldEmpty = emptyView.GetEnumerator();
        string emptyName = emptyOwner.Name;
        emptyView.Add(new SubItem(null, "external-added") { Name = "external-key" });
        observations.Add(new
        {
            scenario = "empty-name-and-old-table", emptyName, countAfterAdd = emptyView.Count,
            oldMove = EnumMove(oldEmpty, []), oldReset = Capture(() => { oldEmpty.Reset(); return null; }), emptyOwner.Name, emptyOwner.Text
        });
    }

    private static void ObserveHiddenMembers(List<object> observations)
    {
        var hidden = new HiddenItem();
        var owner = (ListViewItem)hidden;
        var view = new SubItems(owner);
        var subItem = new HiddenSubItem();
        ((SubItem)subItem).Name = "base-primary-name";
        ((SubItem)subItem).Text = "base-primary-text";
        view.Add(subItem);
        string before = owner.Name;
        owner.Name = "base-name";
        owner.ToolTipText = "base-tip";
        var poison = new PoisonTag();
        owner.Tag = poison;
        observations.Add(new
        {
            scenario = "hidden-members", before, owner.Name, owner.ToolTipText, owner.Text,
            sameTag = ReferenceEquals(poison, owner.Tag), hidden.HiddenReads, hidden.HiddenWrites,
            subItemHiddenReads = subItem.HiddenReads, subItemHiddenWrites = subItem.HiddenWrites, poison.Reads, poison.Disposals
        });
    }

    private static object EnumMove(IEnumerator enumerator, SubItem[] known) => Capture(() => new { moved = enumerator.MoveNext(), current = EnumCurrent(enumerator, known) });
    private static object EnumCurrent(IEnumerator enumerator, SubItem[] known) => Capture(() =>
    {
        object? value = enumerator.Current;
        if (value is null) return "<null>";
        for (int index = 0; index < known.Length; index++)
            if (ReferenceEquals(value, known[index])) return "known-" + index;
        return "restored";
    });

    private static object Capture(Func<object?> action)
    {
        try { return new { exception = (string?)null, value = action() }; }
        catch (Exception exception) { return new { exception = exception.GetType().Name, value = (object?)null }; }
    }

    private sealed class PoisonTag : IDisposable
    {
        public int Reads { get; private set; }
        public int Disposals { get; private set; }
        public override string ToString() { Reads++; throw new InvalidOperationException("Metadata must not format Tag."); }
        public void Dispose() { Disposals++; throw new InvalidOperationException("Metadata must not dispose Tag."); }
    }

    private sealed class HiddenItem : ListViewItem
    {
        public int HiddenReads { get; private set; }
        public int HiddenWrites { get; private set; }
        public new string Name { get { HiddenReads++; throw new InvalidOperationException(); } set { HiddenWrites++; throw new InvalidOperationException(); } }
        public new string ToolTipText { get { HiddenReads++; throw new InvalidOperationException(); } set { HiddenWrites++; throw new InvalidOperationException(); } }
        public new object? Tag { get { HiddenReads++; throw new InvalidOperationException(); } set { HiddenWrites++; throw new InvalidOperationException(); } }
        public new SubItems SubItems { get { HiddenReads++; throw new InvalidOperationException(); } }
    }

    private sealed class HiddenSubItem : SubItem
    {
        public int HiddenReads { get; private set; }
        public int HiddenWrites { get; private set; }
        public new string Name { get { HiddenReads++; throw new InvalidOperationException(); } set { HiddenWrites++; throw new InvalidOperationException(); } }
        public new string Text { get { HiddenReads++; throw new InvalidOperationException(); } set { HiddenWrites++; throw new InvalidOperationException(); } }
    }
}
