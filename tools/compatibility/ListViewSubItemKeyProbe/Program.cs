using System.Globalization;
using System.Reflection;
using System.Text.Json;
#if REFERENCE
using System.Windows.Forms;
#else
using Lumina.Forms;
#endif

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        string version = typeof(ListViewItem).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "";
#if REFERENCE
        if (version.Split('+')[0] != "10.0.11")
        {
            throw new InvalidOperationException($"Expected WinForms 10.0.11; found {version}.");
        }
#endif
        var observations = new List<object>();
        ObserveBasicKeys(observations);
        ObserveNameChangesAndDuplicates(observations);
        ObserveEmptyKeyCache(observations);
        ObserveMutationHistory(observations);
        ObserveRemoval(observations);
        ObserveLazyPrimary(observations);
        ObserveCulture(observations);
        ObserveComparisonEdgeCases(observations);
        Console.WriteLine(JsonSerializer.Serialize(new { version, observations }));
    }

    private static void ObserveBasicKeys(List<object> observations)
    {
        var owner = new ListViewItem("primary text");
        var items = owner.SubItems;
        var primary = items[0];
        primary.Name = "Primary";
        string suppliedName = new string("MiXeD 中文".ToCharArray());
        string suppliedText = new string("detail identity".ToCharArray());
        object tag = new();
        var detail = items.Add(suppliedText);
        detail.Name = suppliedName;
        detail.Tag = tag;
        var unnamed = items.Add("unnamed");
        unnamed.Name = null;
        var whitespace = items.Add("whitespace");
        whitespace.Name = " ";
        foreach (string? key in new string?[] { null, "", "missing", "Primary", "PRIMARY", "primary text", "MiXeD 中文", "mixed 中文", " MiXeD 中文", "MiXeD 中文 ", " " })
        {
            ObserveLookup("basic", items, key, observations);
        }

        var selected = items["mixed 中文"];
        observations.Add(new
        {
            scenario = "lookup-preserves-identity", sameItem = ReferenceEquals(detail, selected),
            sameName = ReferenceEquals(suppliedName, selected?.Name), sameText = ReferenceEquals(suppliedText, selected?.Text),
            sameTag = ReferenceEquals(tag, selected?.Tag), samePrimary = ReferenceEquals(primary, items[0]),
            owner.Text, items.Count
        });
    }

    private static void ObserveNameChangesAndDuplicates(List<object> observations)
    {
        var owner = new ListViewItem(new[] { "primary", "first", "second", "third" });
        var items = owner.SubItems;
        items[1].Name = "duplicate";
        items[2].Name = "DUPLICATE";
        items[3].Name = "other";
        ObserveLookup("duplicate-initial", items, "DuPlIcAtE", observations);
        items[1].Name = "renamed";
        ObserveLookup("duplicate-first-renamed", items, "duplicate", observations);
        ObserveLookup("renamed-first", items, "RENAMED", observations);
        items[1].Name = "duplicate";
        ObserveLookup("duplicate-restored-after-other-key", items, "duplicate", observations);
        items[1].Name = "first-only";
        ObserveLookup("cache-prime-later", items, "duplicate", observations);
        items[1].Name = "DUPLICATE";
        ObserveLookup("cache-earlier-renamed-to-match", items, "duplicate", observations);
        ObserveLookup("cache-missing-key", items, "missing", observations);
        ObserveLookup("cache-after-miss", items, "duplicate", observations);
        items[1].Name = null;
        ObserveLookup("name-null-is-unsearchable", items, null, observations);
        ObserveLookup("name-empty-is-unsearchable", items, "", observations);
        ObserveLookup("name-null-former-key", items, "duplicate", observations);
    }

    private static void ObserveMutationHistory(List<object> observations)
    {
        var owner = new ListViewItem(new[] { "primary", "first", "second", "third" });
        var items = owner.SubItems;
        items[2].Name = "target";
        items[3].Name = "target";
        ObserveLookup("mutation-prime", items, "target", observations);
        var inserted = new ListViewItem.ListViewSubItem(owner, "inserted") { Name = "target" };
        items.Insert(1, inserted);
        ObserveLookup("mutation-insert-before", items, "target", observations);
        items.RemoveAt(1);
        ObserveLookup("mutation-remove-inserted", items, "target", observations);
        items[1].Name = "target";
        ObserveLookup("mutation-earlier-name", items, "target", observations);
        items[2] = new ListViewItem.ListViewSubItem(owner, "replacement") { Name = "target" };
        ObserveLookup("mutation-replace-cached-slot", items, "target", observations);
        items.RemoveAt(1);
        ObserveLookup("mutation-remove-before", items, "target", observations);
        items.RemoveAt(items.Count - 1);
        ObserveLookup("mutation-remove-last", items, "target", observations);
        items.Add(new ListViewItem.ListViewSubItem(owner, "appended") { Name = "target" });
        ObserveLookup("mutation-add-after", items, "target", observations);

        items.Clear();
        items.Add(new ListViewItem.ListViewSubItem(owner, "after-clear-first") { Name = "target" });
        items.Add(new ListViewItem.ListViewSubItem(owner, "after-clear-second") { Name = "target" });
        ObserveLookup("mutation-clear-repopulate-before-query", items, "target", observations);
    }

    private static void ObserveEmptyKeyCache(List<object> observations)
    {
        foreach (string operation in new[] { "index", "contains", "item", "remove" })
        {
            foreach (string? key in new string?[] { null, "" })
            {
                var items = new ListViewItem(new[] { "primary", "earlier", "later" }).SubItems;
                items[2].Name = "target";
                int primed = items.IndexOfKey("target");
                items[1].Name = "target";
                object value = operation switch
                {
                    "index" => Capture(() => items.IndexOfKey(key!)),
                    "contains" => Capture(() => items.ContainsKey(key!)),
                    "item" => Capture(() => items[key!]?.Text),
                    _ => Capture(() => { items.RemoveByKey(key!); return "completed"; })
                };
                observations.Add(new { scenario = "empty-key-cache", operation, key, primed, value, after = items.IndexOfKey("target") });
            }
        }
    }

    private static void ObserveRemoval(List<object> observations)
    {
        var owner = new ListViewItem(new[] { "primary", "first", "second", "third" });
        var items = owner.SubItems;
        items[1].Name = "duplicate";
        items[2].Name = "DUPLICATE";
        items[3].Name = "third";
        Remove("remove-null", items, null, observations);
        Remove("remove-empty", items, "", observations);
        Remove("remove-missing", items, "missing", observations);
        Remove("remove-first-match", items, "DuPlIcAtE", observations);
        ObserveLookup("remove-next-duplicate", items, "duplicate", observations);
        Remove("remove-next-match", items, "duplicate", observations);
        Remove("remove-no-match-left", items, "duplicate", observations);
        items[0].Name = "primary";
        Remove("remove-primary", items, "PRIMARY", observations);
        observations.Add(new { scenario = "remove-primary-text", owner.Text, items.Count, firstText = items[0].Text });

        var cachedOwner = new ListViewItem(new[] { "primary", "earlier", "later" });
        var cachedItems = cachedOwner.SubItems;
        var earlier = cachedItems[1];
        var later = cachedItems[2];
        later.Name = "target";
        ObserveLookup("remove-cache-prime", cachedItems, "target", observations);
        earlier.Name = "target";
        Remove("remove-after-earlier-name-change", cachedItems, "target", observations);
        observations.Add(new { scenario = "remove-cache-identity", containsEarlier = cachedItems.Contains(earlier), containsLater = cachedItems.Contains(later) });
    }

    private static void ObserveLazyPrimary(List<object> observations)
    {
        var owner = new ListViewItem("only");
        var items = owner.SubItems;
        var oldPrimary = items[0];
        oldPrimary.Name = "only-key";
        Remove("remove-only", items, "only-key", observations);
        observations.Add(new { scenario = "remove-only-owner-text", owner.Text, items.Count });
        ObserveLookup("remove-only-lookup", items, "only-key", observations);
        observations.Add(new { scenario = "remove-only-restored", sameCollection = ReferenceEquals(items, owner.SubItems), samePrimary = ReferenceEquals(oldPrimary, items[0]), items.Count, items[0].Text, items[0].Name });
        items[0].Name = "clear-key";
        ObserveLookup("clear-prime", items, "clear-key", observations);
        items.Clear();
        observations.Add(new { scenario = "clear-owner-text", owner.Text, items.Count });
        ObserveLookup("clear-lookup", items, "clear-key", observations);
        Remove("clear-remove", items, "clear-key", observations);
        observations.Add(new { scenario = "clear-remains-empty", items.Count });
        observations.Add(new { scenario = "clear-restored", sameCollection = ReferenceEquals(items, owner.SubItems), items.Count, items[0].Text, items[0].Name });
        ObserveLookup("clear-restored-empty-key", items, "", observations);
    }

    private static void ObserveCulture(List<object> observations)
    {
        CultureInfo original = CultureInfo.CurrentCulture;
        try
        {
            foreach (string culture in new[] { "en-US", "tr-TR" })
            {
                CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(culture);
                var items = new ListViewItem(new[] { "primary", "latin-i", "dotted-i", "accent", "sharp-s" }).SubItems;
                items[1].Name = "FILE";
                items[2].Name = "İ";
                items[3].Name = "é";
                items[4].Name = "ß";
                foreach (string key in new[] { "file", "fıle", "i", "É", "e\u0301", "ss" })
                {
                    ObserveLookup("culture-" + culture, items, key, observations);
                }
            }
        }
        finally
        {
            CultureInfo.CurrentCulture = original;
        }
    }

    private static void ObserveComparisonEdgeCases(List<object> observations)
    {
        CultureInfo original = CultureInfo.CurrentCulture;
        try
        {
            foreach (string culture in new[] { "en-US", "tr-TR" })
            {
                CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(culture);
                foreach ((string scenario, string name, string key) in new[]
                {
                    ("soft-hyphen", "a\u00ad", "\u00ada"),
                    ("embedded-null", "a\0b", "ab\0"),
                    ("angstrom", "\u212b", "\u00c5"),
                    ("kelvin", "\u212a", "k"),
                    ("long-s", "\u017f", "s"),
                    ("ligature-padding", "\u00c6\0", "AE")
                })
                {
                    var items = new ListViewItem("comparison").SubItems;
                    items[0].Name = name;
                    ObserveLookup("comparison-" + culture + "-" + scenario, items, key, observations);
                }
            }
        }
        finally
        {
            CultureInfo.CurrentCulture = original;
        }
    }

    private static void ObserveLookup(string scenario, ListViewItem.ListViewSubItemCollection items, string? key, List<object> observations)
    {
        observations.Add(new { scenario, key, operation = "index", value = Capture(() => items.IndexOfKey(key!)) });
        observations.Add(new { scenario, key, operation = "contains", value = Capture(() => items.ContainsKey(key!)) });
        observations.Add(new { scenario, key, operation = "item", value = Capture(() => items[key!]?.Text) });
    }

    private static void Remove(string scenario, ListViewItem.ListViewSubItemCollection items, string? key, List<object> observations)
    {
        object result = Capture(() => { items.RemoveByKey(key!); return "completed"; });
        var texts = new List<string>();
        for (int i = 0; i < items.Count; i++)
        {
            texts.Add(items[i].Text);
        }

        observations.Add(new { scenario, key, operation = "remove", result, items.Count, texts });
    }

    private static object Capture<T>(Func<T> action)
    {
        try
        {
            return new { result = action(), exception = (string?)null, parameter = (string?)null };
        }
        catch (Exception exception)
        {
            return new { result = (object?)null, exception = exception.GetType().FullName, parameter = (exception as ArgumentException)?.ParamName };
        }
    }
}
