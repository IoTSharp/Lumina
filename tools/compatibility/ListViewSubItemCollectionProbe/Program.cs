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
        ObserveConstruction(observations);
        ObserveSharedViews(observations);
        ObserveDeferredPrimary(observations);
        ObserveIntegerBoundaries(observations);
        ObserveReplacementIdentity(observations);
        ObserveIndependentKeyCaches(observations);
        Console.WriteLine(JsonSerializer.Serialize(new { version, observations }));
    }

    private static void ObserveConstruction(List<object> observations)
    {
        observations.Add(new { scenario = "null-owner-constructor", value = Capture(() => { _ = new ListViewItem.ListViewSubItemCollection(null!); return "completed"; }) });
        observations.Add(new { scenario = "null-owner-count", value = Capture(() => new ListViewItem.ListViewSubItemCollection(null!).Count) });
        foreach ((string scenario, ListViewItem owner) in new[]
        {
            ("default", new ListViewItem()),
            ("text-null", new ListViewItem((string?)null)),
            ("text", new ListViewItem("primary")),
            ("array-null", new ListViewItem((string[]?)null)),
            ("array-empty", new ListViewItem(Array.Empty<string>())),
            ("array", new ListViewItem(new[] { "primary", "detail" }))
        })
        {
            string textBefore = owner.Text;
            var first = new ListViewItem.ListViewSubItemCollection(owner);
            int initialCount = first.Count;
            string textAfterFirst = owner.Text;
            var second = new ListViewItem.ListViewSubItemCollection(owner);
            observations.Add(new
            {
                scenario = "construct-" + scenario, textBefore, initialCount, textAfterFirst,
                countAfterSecond = first.Count, sameExternalViews = ReferenceEquals(first, second),
                first = Texts(first), second = Texts(second)
            });
            var property = owner.SubItems;
            observations.Add(new
            {
                scenario = "property-after-construct-" + scenario,
                sameAsFirst = ReferenceEquals(property, first), sameAsSecond = ReferenceEquals(property, second),
                stableProperty = ReferenceEquals(property, owner.SubItems),
                first = Texts(first), second = Texts(second), property = Texts(property), owner.Text,
                samePrimary = ReferenceEquals(first[0], property[0]) && ReferenceEquals(second[0], property[0])
            });
        }
    }

    private static void ObserveSharedViews(List<object> observations)
    {
        var owner = new ListViewItem(new[] { "primary", "detail" });
        var property = owner.SubItems;
        var first = new ListViewItem.ListViewSubItemCollection(owner);
        var second = new ListViewItem.ListViewSubItemCollection(owner);
        var added = first.Add("added through first");
        observations.Add(new
        {
            scenario = "shared-add", owner.Text, first = Texts(first), second = Texts(second), property = Texts(property),
            sameAdded = ReferenceEquals(added, second[2]) && ReferenceEquals(added, property[2]),
            contains = second.Contains(added), index = property.IndexOf(added)
        });
        second.RemoveAt(0);
        observations.Add(new { scenario = "shared-remove-primary", owner.Text, first = Texts(first), second = Texts(second), property = Texts(property) });
        owner.Text = "owner update";
        observations.Add(new { scenario = "shared-owner-text", owner.Text, first = Texts(first), second = Texts(second), property = Texts(property) });
        var enumerated = second.Cast<ListViewItem.ListViewSubItem>().ToArray();
        observations.Add(new
        {
            scenario = "shared-enumerate", samePrimary = ReferenceEquals(first[0], enumerated[0]), sameDetail = ReferenceEquals(first[1], enumerated[1]),
            texts = enumerated.Select(item => item.Text).ToArray()
        });
    }

    private static void ObserveDeferredPrimary(List<object> observations)
    {
        var owner = new ListViewItem("primary");
        var property = owner.SubItems;
        var first = new ListViewItem.ListViewSubItemCollection(owner);
        var second = new ListViewItem.ListViewSubItemCollection(owner);
        var oldPrimary = property[0];
        first.Clear();
        var third = new ListViewItem.ListViewSubItemCollection(owner);
        observations.Add(new
        {
            scenario = "clear-external-view", owner.Text, firstCount = first.Count, secondCount = second.Count,
            propertyCount = property.Count, thirdCount = third.Count,
            firstIndex = Capture(() => first[0]?.Text), secondIndex = Capture(() => second[0]?.Text)
        });
        var restored = owner.SubItems;
        observations.Add(new
        {
            scenario = "clear-property-restore", sameProperty = ReferenceEquals(property, restored),
            samePrimary = ReferenceEquals(oldPrimary, restored[0]), first = Texts(first), second = Texts(second), third = Texts(third),
            allSamePrimary = ReferenceEquals(first[0], second[0]) && ReferenceEquals(first[0], third[0])
        });
        second.Clear();
        owner.Text = "restored through text";
        observations.Add(new { scenario = "clear-text-set-restore", owner.Text, first = Texts(first), second = Texts(second), property = Texts(property) });
        property.Clear();
        var manual = first.Add("manual primary");
        observations.Add(new
        {
            scenario = "clear-external-add-before-property", owner.Text, first = Texts(first), second = Texts(second),
            samePrimary = ReferenceEquals(manual, owner.SubItems[0])
        });
    }

    private static void ObserveIntegerBoundaries(List<object> observations)
    {
        foreach (bool empty in new[] { false, true })
        {
            foreach (int index in new[] { int.MinValue, -2, -1, 0, 1, 2, int.MaxValue })
            {
                var owner = empty ? new ListViewItem() : new ListViewItem(new[] { "primary", "detail" });
                var items = new ListViewItem.ListViewSubItemCollection(owner);
                observations.Add(new { scenario = "integer-get", empty, index, value = Capture(() => items[index]?.Text), items.Count, ownerText = Capture(() => owner.Text) });
                foreach (bool nullValue in new[] { false, true })
                {
                    owner = empty ? new ListViewItem() : new ListViewItem(new[] { "primary", "detail" });
                    items = new ListViewItem.ListViewSubItemCollection(owner);
                    var value = nullValue ? null : new ListViewItem.ListViewSubItem(null, "replacement");
                    object result = Capture(() => { items[index] = value!; return "completed"; });
                    observations.Add(new
                    {
                        scenario = "integer-set", empty, index, nullValue, result,
                        items.Count, ownerText = Capture(() => owner.Text), texts = Capture(() => Texts(items)),
                        sameAssigned = index >= 0 && index < items.Count && ReferenceEquals(value, items[index])
                    });
                }
            }
        }
    }

    private static void ObserveReplacementIdentity(List<object> observations)
    {
        var owner = new ListViewItem(new[] { "primary", "detail" });
        var property = owner.SubItems;
        var first = new ListViewItem.ListViewSubItemCollection(owner);
        var second = new ListViewItem.ListViewSubItemCollection(owner);
        var oldPrimary = first[0];
        string text = new string("replacement identity".ToCharArray());
        object tag = new();
        var replacement = new ListViewItem.ListViewSubItem(null, text) { Name = "replacement", Tag = tag };
        first[0] = replacement;
        observations.Add(new
        {
            scenario = "replace-primary-identity", owner.Text, sameText = ReferenceEquals(text, owner.Text),
            sameReplacement = ReferenceEquals(replacement, property[0]) && ReferenceEquals(replacement, second[0]),
            sameTag = ReferenceEquals(tag, second[0].Tag), containsOld = second.Contains(oldPrimary),
            first = Texts(first), second = Texts(second)
        });
        second[1] = replacement;
        replacement.Text = "same object in both slots";
        observations.Add(new
        {
            scenario = "replace-duplicate-identity", owner.Text, first = Texts(first), property = Texts(property),
            sameSlots = ReferenceEquals(first[0], first[1]), replacementIndex = property.IndexOf(replacement)
        });
        property[0] = property[0];
        observations.Add(new { scenario = "replace-same-object", samePrimary = ReferenceEquals(replacement, first[0]), first = Texts(first) });
    }

    private static void ObserveIndependentKeyCaches(List<object> observations)
    {
        var owner = new ListViewItem(new[] { "primary", "earlier", "later" });
        var property = owner.SubItems;
        var first = new ListViewItem.ListViewSubItemCollection(owner);
        var second = new ListViewItem.ListViewSubItemCollection(owner);
        first[2].Name = "target";
        int firstPrimed = first.IndexOfKey("target");
        first[1].Name = "target";
        int secondPrimed = second.IndexOfKey("target");
        int propertyPrimed = property.IndexOfKey("target");
        observations.Add(new
        {
            scenario = "independent-key-caches", firstPrimed, secondPrimed, propertyPrimed,
            first = first.IndexOfKey("target"), second = second.IndexOfKey("target"), property = property.IndexOfKey("target")
        });
        first.IndexOfKey(null);
        second.IndexOfKey("");
        observations.Add(new { scenario = "independent-empty-key", first = first.IndexOfKey("target"), second = second.IndexOfKey("target") });
        property[1] = new ListViewItem.ListViewSubItem(null, "replacement earlier") { Name = "target" };
        observations.Add(new { scenario = "shared-replace-keeps-cache", first = first["target"]?.Text, second = second["target"]?.Text, property = property["target"]?.Text });
        second.IndexOfKey("missing");
        observations.Add(new { scenario = "independent-miss-cache", first = first.IndexOfKey("target"), second = second.IndexOfKey("target") });
        property.Clear();
        first.Add(new ListViewItem.ListViewSubItem(null, "new primary") { Name = "target" });
        first.Add(new ListViewItem.ListViewSubItem(null, "new earlier") { Name = "target" });
        first.Add(new ListViewItem.ListViewSubItem(null, "new later") { Name = "target" });
        observations.Add(new
        {
            scenario = "shared-clear-repopulate-cache", first = first["target"]?.Text, second = second["target"]?.Text,
            property = property["target"]?.Text, fresh = new ListViewItem.ListViewSubItemCollection(owner)["target"]?.Text
        });
    }

    private static string?[] Texts(ListViewItem.ListViewSubItemCollection items)
    {
        var result = new string?[items.Count];
        for (int index = 0; index < result.Length; index++)
        {
            result[index] = items[index]?.Text;
        }

        return result;
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
