using System.Drawing;
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
        using var font = new Font("Arial", 12.5f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
        var observations = new List<object>();
        ObserveSignatures(observations);
        ObserveInsertValidation(observations);
        ObserveRemoveAtValidation(observations);
        ObserveRemoveValidation(observations);
        ObserveInsertIdentity(observations, font);
        ObserveRemoveIdentity(observations);
        ObserveEmptyRecovery(observations);
        ObserveKeyCaches(observations);
        ObserveFontLifetime(observations, font);
        Console.WriteLine(JsonSerializer.Serialize(new { version, observations }));
    }

    private static void ObserveSignatures(List<object> observations)
    {
        foreach ((string name, Type[] parameters) in new[]
        {
            ("Insert", new[] { typeof(int), typeof(ListViewItem.ListViewSubItem) }),
            ("Remove", new[] { typeof(ListViewItem.ListViewSubItem) }),
            ("RemoveAt", new[] { typeof(int) }),
            ("Clear", Type.EmptyTypes)
        })
        {
            MethodInfo? method = typeof(ListViewItem.ListViewSubItemCollection).GetMethod(name, parameters);
            observations.Add(new
            {
                scenario = "signature-" + name, exists = method is not null,
                returnsVoid = method?.ReturnType == typeof(void), isPublic = method?.IsPublic,
                isVirtual = method?.IsVirtual, isFinal = method?.IsFinal, isHideBySig = method?.IsHideBySig,
                parameterNames = method?.GetParameters().Select(parameter => parameter.Name).ToArray()
            });
        }
    }

    private static void ObserveInsertValidation(List<object> observations)
    {
        foreach (string shape in Shapes)
        {
            foreach (int index in Indices(Create(shape).First.Count))
            {
                foreach (bool nullItem in new[] { false, true })
                {
                    var views = Create(shape);
                    var candidate = nullItem ? null : new ListViewItem.ListViewSubItem(null, "inserted") { Name = "inserted key" };
                    var before = views.First.Cast<ListViewItem.ListViewSubItem>().ToArray();
                    object result = Capture(() => views.First.Insert(index, candidate!));
                    observations.Add(new
                    {
                        scenario = "insert-validation-" + shape + "-" + index + "-" + nullItem,
                        result, beforeCount = before.Length, state = Snapshot(views.Owner, views.First),
                        sameOtherView = SameItems(views.First, views.Second),
                        unchanged = SameItems(views.First, before),
                        candidateStored = candidate is not null && views.First.Cast<ListViewItem.ListViewSubItem>().Any(item => ReferenceEquals(item, candidate))
                    });
                }
            }
        }
    }

    private static void ObserveRemoveAtValidation(List<object> observations)
    {
        foreach (string shape in Shapes)
        {
            foreach (int index in Indices(Create(shape).First.Count))
            {
                var views = Create(shape);
                var before = views.First.Cast<ListViewItem.ListViewSubItem>().ToArray();
                object result = Capture(() => views.First.RemoveAt(index));
                observations.Add(new
                {
                    scenario = "remove-at-validation-" + shape + "-" + index,
                    result, beforeCount = before.Length, state = Snapshot(views.Owner, views.First),
                    sameOtherView = SameItems(views.First, views.Second), unchanged = SameItems(views.First, before),
                    retained = before.Select(item => views.First.Cast<ListViewItem.ListViewSubItem>().Any(remaining => ReferenceEquals(item, remaining))).ToArray()
                });
            }
        }
    }

    private static void ObserveRemoveValidation(List<object> observations)
    {
        foreach (string shape in Shapes)
        {
            foreach (string candidateKind in new[] { "null", "missing", "same-text", "first", "last", "other-owner" })
            {
                var views = Create(shape);
                var source = new ListViewItem("source primary");
                ListViewItem.ListViewSubItem? candidate = candidateKind switch
                {
                    "null" => null,
                    "same-text" => new ListViewItem.ListViewSubItem(null, views.Owner.Text),
                    "first" => views.First.Count == 0 ? null : views.First[0],
                    "last" => views.First.Count == 0 ? null : views.First[views.First.Count - 1],
                    "other-owner" => source.SubItems[0],
                    _ => new ListViewItem.ListViewSubItem(null, "missing")
                };
                var before = views.First.Cast<ListViewItem.ListViewSubItem>().ToArray();
                object result = Capture(() => views.First.Remove(candidate!));
                observations.Add(new
                {
                    scenario = "remove-validation-" + shape + "-" + candidateKind,
                    result, beforeCount = before.Length, state = Snapshot(views.Owner, views.First),
                    sameOtherView = SameItems(views.First, views.Second), unchanged = SameItems(views.First, before),
                    sourceState = Snapshot(source, new ListViewItem.ListViewSubItemCollection(source))
                });
            }
        }
    }

    private static void ObserveInsertIdentity(List<object> observations, Font font)
    {
        foreach (int index in new[] { 0, 1, 2 })
        {
            foreach (string kind in new[] { "detached", "same-primary", "same-detail", "other-primary", "other-detail", "constructed-other-owner", "empty-text" })
            {
                var source = new ListViewItem(new[] { "source primary", "source detail" });
                var owner = new ListViewItem(new[] { "primary", "detail" });
                var property = owner.SubItems;
                var first = new ListViewItem.ListViewSubItemCollection(owner);
                var second = new ListViewItem.ListViewSubItemCollection(owner);
                var original = first.Cast<ListViewItem.ListViewSubItem>().ToArray();
                string identityText = new string("identity text".ToCharArray());
                object tag = new();
                var candidate = kind switch
                {
                    "same-primary" => first[0],
                    "same-detail" => first[1],
                    "other-primary" => source.SubItems[0],
                    "other-detail" => source.SubItems[1],
                    "constructed-other-owner" => new ListViewItem.ListViewSubItem(source, identityText, Color.Crimson, Color.AliceBlue, font),
                    "empty-text" => new ListViewItem.ListViewSubItem(null, null, Color.Crimson, Color.AliceBlue, font),
                    _ => new DerivedSubItem(null, identityText, Color.Crimson, Color.AliceBlue, font)
                };
                candidate.Tag = tag;
                candidate.Name = "identity key";
                string text = candidate.Text;
                var sourceBefore = source.SubItems.Cast<ListViewItem.ListViewSubItem>().ToArray();
                object result = Capture(() => first.Insert(index, candidate));
                observations.Add(new
                {
                    scenario = "insert-identity-" + index + "-" + kind, result,
                    state = Snapshot(owner, first), sourceState = Snapshot(source, source.SubItems),
                    insertedSame = ReferenceEquals(candidate, first[index]), sameText = ReferenceEquals(text, first[index].Text),
                    sameTag = ReferenceEquals(tag, first[index].Tag), sameFont = ReferenceEquals(candidate.Font, first[index].Font),
                    sameSecond = SameItems(first, second), sameProperty = SameItems(first, property),
                    retainedOrder = original.Select((item, originalIndex) => ReferenceEquals(item, first[originalIndex < index ? originalIndex : originalIndex + 1])).ToArray(),
                    sourceRetained = SameItems(source.SubItems, sourceBefore),
                    occurrences = first.Cast<ListViewItem.ListViewSubItem>().Count(item => ReferenceEquals(item, candidate)),
                    derived = first[index] is DerivedSubItem
                });
                candidate.Text = "shared changed";
                first.Remove(candidate);
                observations.Add(new
                {
                    scenario = "insert-then-remove-" + index + "-" + kind,
                    state = Snapshot(owner, first), sourceState = Snapshot(source, source.SubItems),
                    occurrences = first.Cast<ListViewItem.ListViewSubItem>().Count(item => ReferenceEquals(item, candidate)),
                    candidate.Text, candidate.Name, sameTag = ReferenceEquals(tag, candidate.Tag),
                    borrowedFontUsable = font.GetHeight(96f) > 0
                });
            }
        }
    }

    private static void ObserveRemoveIdentity(List<object> observations)
    {
        foreach (string operation in new[] { "remove", "remove-at", "clear" })
        {
            var owner = new ListViewItem("primary");
            var items = owner.SubItems;
            var repeated = new ListViewItem.ListViewSubItem(null, "repeat");
            items.AddRange([repeated, new ListViewItem.ListViewSubItem(null, "middle"), repeated]);
            object result = Capture(() =>
            {
                if (operation == "remove") items.Remove(repeated);
                else if (operation == "remove-at") items.RemoveAt(1);
                else items.Clear();
            });
            observations.Add(new
            {
                scenario = "repeated-object-" + operation, result, state = Snapshot(owner, items),
                repeatedPositions = Enumerable.Range(0, items.Count).Where(index => ReferenceEquals(items[index], repeated)).ToArray(),
                repeated.Text
            });
            items.Remove(repeated);
            items.Remove(repeated);
            observations.Add(new { scenario = "repeated-object-remove-until-missing-" + operation, state = Snapshot(owner, items) });
        }

        var equalOwner = new ListViewItem("primary");
        var firstEqual = new EqualSubItem("first");
        var secondEqual = new EqualSubItem("second");
        equalOwner.SubItems.AddRange([firstEqual, secondEqual]);
        var missingEqual = new EqualSubItem("missing");
        equalOwner.SubItems.Remove(missingEqual);
        observations.Add(new
        {
            scenario = "remove-distinct-object-overriding-equals", state = Snapshot(equalOwner, equalOwner.SubItems),
            firstRetained = equalOwner.SubItems.Cast<ListViewItem.ListViewSubItem>().Any(item => ReferenceEquals(item, firstEqual)),
            secondRetained = equalOwner.SubItems.Cast<ListViewItem.ListViewSubItem>().Any(item => ReferenceEquals(item, secondEqual))
        });
        equalOwner.SubItems.Remove(secondEqual);
        observations.Add(new
        {
            scenario = "remove-exact-object-overriding-equals", state = Snapshot(equalOwner, equalOwner.SubItems),
            firstRetained = equalOwner.SubItems.Cast<ListViewItem.ListViewSubItem>().Any(item => ReferenceEquals(item, firstEqual)),
            secondRetained = equalOwner.SubItems.Cast<ListViewItem.ListViewSubItem>().Any(item => ReferenceEquals(item, secondEqual))
        });
    }

    private static void ObserveEmptyRecovery(List<object> observations)
    {
        foreach (string operation in new[] { "remove", "remove-at", "clear" })
        {
            foreach (string recovery in new[] { "property", "owner-text", "insert", "add", "add-range" })
            {
                var owner = new ListViewItem("primary");
                var property = owner.SubItems;
                var first = new ListViewItem.ListViewSubItemCollection(owner);
                var second = new ListViewItem.ListViewSubItemCollection(owner);
                var original = first[0];
                if (operation == "remove") first.Remove(original);
                else if (operation == "remove-at") first.RemoveAt(0);
                else first.Clear();
                object before = Snapshot(owner, first);
                int propertyCountBefore = property.Count;
                var manual = new ListViewItem.ListViewSubItem(null, "manual primary");
                object result = Capture(() =>
                {
                    if (recovery == "property") _ = owner.SubItems;
                    else if (recovery == "owner-text") owner.Text = "restored";
                    else if (recovery == "insert") second.Insert(0, manual);
                    else if (recovery == "add") second.Add(manual);
                    else second.AddRange([manual]);
                });
                observations.Add(new
                {
                    scenario = "empty-recovery-" + operation + "-" + recovery,
                    before, propertyCountBefore, result, after = Snapshot(owner, first),
                    sameViews = SameItems(first, second) && SameItems(first, property),
                    stableProperty = ReferenceEquals(property, owner.SubItems),
                    originalRestored = ReferenceEquals(original, first[0]), manualRestored = ReferenceEquals(manual, first[0]),
                    original.Text
                });
            }
        }

        foreach (string shape in Shapes)
        {
            var views = Create(shape);
            object first = Capture(() => views.First.Clear());
            object afterFirst = Snapshot(views.Owner, views.First);
            object second = Capture(() => views.Second.Clear());
            observations.Add(new { scenario = "clear-repeat-" + shape, first, afterFirst, second, afterSecond = Snapshot(views.Owner, views.First), sameViews = SameItems(views.First, views.Second) });
        }
    }

    private static void ObserveKeyCaches(List<object> observations)
    {
        foreach (string operation in new[] { "insert-first", "insert-middle", "insert-last", "insert-null", "insert-invalid", "remove-first", "remove-middle", "remove-last", "remove-missing", "remove-null", "remove-at-first", "remove-at-middle", "remove-at-last", "remove-at-invalid", "clear" })
        {
            var owner = new ListViewItem(new[] { "primary", "earlier", "later" });
            var property = owner.SubItems;
            var first = new ListViewItem.ListViewSubItemCollection(owner);
            var second = new ListViewItem.ListViewSubItemCollection(owner);
            first[2].Name = "target";
            int firstBefore = first.IndexOfKey("target");
            first[1].Name = "target";
            int secondBefore = second.IndexOfKey("target");
            int propertyBefore = property.IndexOfKey("target");
            first[0].Name = "target";
            var inserted = new ListViewItem.ListViewSubItem(null, "inserted") { Name = "target" };
            object result = Capture(() =>
            {
                switch (operation)
                {
                    case "insert-first": first.Insert(0, inserted); break;
                    case "insert-middle": first.Insert(1, inserted); break;
                    case "insert-last": first.Insert(3, inserted); break;
                    case "insert-null": first.Insert(1, null!); break;
                    case "insert-invalid": first.Insert(4, inserted); break;
                    case "remove-first": first.Remove(first[0]); break;
                    case "remove-middle": first.Remove(first[1]); break;
                    case "remove-last": first.Remove(first[2]); break;
                    case "remove-missing": first.Remove(inserted); break;
                    case "remove-null": first.Remove(null!); break;
                    case "remove-at-first": first.RemoveAt(0); break;
                    case "remove-at-middle": first.RemoveAt(1); break;
                    case "remove-at-last": first.RemoveAt(2); break;
                    case "remove-at-invalid": first.RemoveAt(3); break;
                    case "clear": first.Clear(); break;
                }
            });
            observations.Add(new
            {
                scenario = "key-cache-" + operation, firstBefore, secondBefore, propertyBefore, result,
                state = Snapshot(owner, first), first = first.IndexOfKey("target"), second = second.IndexOfKey("target"),
                property = property.IndexOfKey("target"), fresh = new ListViewItem.ListViewSubItemCollection(owner).IndexOfKey("target")
            });
            property.Clear();
            for (int index = 0; index < 4; index++)
            {
                second.Insert(index, new ListViewItem.ListViewSubItem(null, "replacement " + index) { Name = "target" });
            }
            observations.Add(new
            {
                scenario = "key-cache-repopulate-" + operation, state = Snapshot(owner, first),
                first = first.IndexOfKey("target"), second = second.IndexOfKey("target"), property = property.IndexOfKey("target"),
                fresh = new ListViewItem.ListViewSubItemCollection(owner).IndexOfKey("target")
            });
        }
    }

    private static void ObserveFontLifetime(List<object> observations, Font font)
    {
        float height = font.GetHeight(96f);
        foreach (string operation in new[] { "insert", "remove", "remove-at", "clear", "failed-insert", "failed-remove-at", "remove-missing" })
        {
            var owner = new ListViewItem("primary");
            var items = owner.SubItems;
            var first = new ListViewItem.ListViewSubItem(null, "first", Color.Crimson, Color.AliceBlue, font) { Name = "key", Tag = "tag" };
            var second = new ListViewItem.ListViewSubItem(null, "second", Color.Green, Color.Yellow, font);
            items.AddRange([first, second]);
            object result = Capture(() =>
            {
                switch (operation)
                {
                    case "insert": items.Insert(0, first); break;
                    case "remove": items.Remove(first); break;
                    case "remove-at": items.RemoveAt(1); break;
                    case "clear": items.Clear(); break;
                    case "failed-insert": items.Insert(-1, first); break;
                    case "failed-remove-at": items.RemoveAt(3); break;
                    case "remove-missing": items.Remove(new ListViewItem.ListViewSubItem()); break;
                }
            });
            observations.Add(new
            {
                scenario = "borrowed-font-" + operation, result, state = Snapshot(owner, items),
                first.Text, first.Name, first.Tag, sameFont = ReferenceEquals(font, first.Font) && ReferenceEquals(font, second.Font),
                sameHeight = height == font.GetHeight(96f) && height == first.Font.GetHeight(96f) && height == second.Font.GetHeight(96f),
                foreColor = first.ForeColor.ToArgb(), backColor = first.BackColor.ToArgb()
            });
        }
    }

    private static readonly string[] Shapes = ["fresh", "populated", "cleared-external", "cleared-property"];

    private static (ListViewItem Owner, ListViewItem.ListViewSubItemCollection First, ListViewItem.ListViewSubItemCollection Second) Create(string shape)
    {
        var owner = shape == "fresh" ? new ListViewItem() : new ListViewItem(new[] { "primary", "detail", "last" });
        var first = new ListViewItem.ListViewSubItemCollection(owner);
        var second = new ListViewItem.ListViewSubItemCollection(owner);
        if (shape == "cleared-property") owner.SubItems.Clear();
        else if (shape == "cleared-external") first.Clear();
        return (owner, first, second);
    }

    private static int[] Indices(int count) => new[] { int.MinValue, -1, 0, 1, count - 1, count, count + 1, int.MaxValue }.Distinct().ToArray();

    private static object Snapshot(ListViewItem owner, ListViewItem.ListViewSubItemCollection items)
    {
        int count = items.Count;
        string text = owner.Text;
        return new { count, ownerText = text, countAfterText = items.Count, texts = items.Cast<ListViewItem.ListViewSubItem>().Select(item => item.Text).ToArray(), names = items.Cast<ListViewItem.ListViewSubItem>().Select(item => item.Name).ToArray() };
    }

    private static bool SameItems(ListViewItem.ListViewSubItemCollection first, System.Collections.IEnumerable second)
    {
        var others = second.Cast<ListViewItem.ListViewSubItem>().ToArray();
        return first.Count == others.Length && Enumerable.Range(0, first.Count).All(index => ReferenceEquals(first[index], others[index]));
    }

    private static object Capture(Action action)
    {
        try
        {
            action();
            return new { exception = "", parameter = "" };
        }
        catch (Exception exception)
        {
            return new { exception = exception.GetType().Name, parameter = (exception as ArgumentException)?.ParamName ?? "" };
        }
    }

    private sealed class DerivedSubItem(ListViewItem? owner, string? text, Color fore, Color back, Font? font)
        : ListViewItem.ListViewSubItem(owner, text, fore, back, font)
    {
    }

    private sealed class EqualSubItem(string text) : ListViewItem.ListViewSubItem(null, text)
    {
        public override bool Equals(object? obj) => obj is EqualSubItem;
        public override int GetHashCode() => 1;
    }
}
