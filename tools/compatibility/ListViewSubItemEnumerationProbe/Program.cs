using System.Collections;
using System.Reflection;
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
        string version = typeof(ListViewItem).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "";
#if REFERENCE
        if (version.Split('+')[0] != "10.0.11")
            throw new InvalidOperationException($"Expected WinForms 10.0.11; found {version}.");
#endif
        var observations = new List<object>();
        ObserveMetadata(observations);
        foreach (string consumer in new[] { "public", "interface" })
        foreach (string shape in Shapes)
        {
            ObserveBaseline(observations, consumer, shape);
            foreach (string stage in new[] { "before", "middle", "ended" })
            foreach (bool sharedView in new[] { false, true })
            foreach (string mutation in Mutations)
                ObserveMutation(observations, consumer, shape, stage, sharedView, mutation);
        }
        Console.WriteLine(JsonSerializer.Serialize(new { version, observations }));
    }

    private static void ObserveMetadata(List<object> observations)
    {
        MethodInfo? method = typeof(SubItems).GetMethod("GetEnumerator", BindingFlags.Public | BindingFlags.Instance, Type.EmptyTypes);
        InterfaceMapping mapping = typeof(SubItems).GetInterfaceMap(typeof(IEnumerable));
        MethodInfo mapped = mapping.TargetMethods[Array.FindIndex(mapping.InterfaceMethods, entry => entry.Name == "GetEnumerator")];
        observations.Add(new
        {
            scenario = "enumerator-metadata", exists = method is not null,
            returnType = method?.ReturnType.FullName, method?.IsPublic, method?.IsVirtual, method?.IsFinal,
            parameters = method?.GetParameters().Select(parameter => parameter.Name).ToArray(),
            mappedPublic = mapped.IsPublic, mappedPrivate = mapped.IsPrivate,
            mappedVirtual = mapped.IsVirtual, mappedFinal = mapped.IsFinal,
            mappedReturnType = mapped.ReturnType.FullName
        });
    }

    private static void ObserveBaseline(List<object> observations, string consumer, string shape)
    {
        var setup = Create(shape);
        var identities = new Identities(setup.First);
        int before = setup.First.Count;
        IEnumerator enumerator = GetEnumerator(setup.First, consumer);
        var trace = new List<object>();
        RecordCurrent(trace, "created-current", enumerator, identities);
        Drain(trace, "initial", enumerator, identities);
        RecordMove(trace, "end-again", enumerator);
        RecordCurrent(trace, "end-again-current", enumerator, identities);
        RecordReset(trace, "reset", enumerator);
        RecordCurrent(trace, "reset-current", enumerator, identities);
        Drain(trace, "replay", enumerator, identities);
        // Two enumerators over the same view must advance independently.
        IEnumerator second = GetEnumerator(setup.First, consumer);
        RecordMove(trace, "second-first", second);
        RecordCurrent(trace, "second-current", second, identities);
        RecordCurrent(trace, "first-still-ended", enumerator, identities);
        observations.Add(new { scenario = $"baseline-{consumer}-{shape}", countBefore = before,
            countAfter = setup.First.Count, state = State(setup, identities), trace });
    }

    private static void ObserveMutation(List<object> observations, string consumer, string shape, string stage, bool sharedView, string mutation)
    {
        var setup = Create(shape);
        var identities = new Identities(setup.First);
        int count = setup.First.Count;
        IEnumerator enumerator = GetEnumerator(setup.First, consumer);
        int countAfterCreation = setup.First.Count;
        var trace = new List<object>();
        RecordCurrent(trace, "created-current", enumerator, identities);
        if (stage == "middle")
        {
            RecordMove(trace, "advance-one", enumerator);
            RecordCurrent(trace, "advance-one-current", enumerator, identities);
        }
        else if (stage == "ended")
        {
            Drain(trace, "advance-end", enumerator, identities);
        }

        SubItems changed = sharedView ? setup.Second : setup.First;
        object result = Capture(() => { Mutate(setup.Owner, changed, mutation); return null; });
        object state = State(setup, identities);
        RecordCurrent(trace, "mutated-current", enumerator, identities);
        Drain(trace, "mutated", enumerator, identities);
        RecordMove(trace, "mutated-end-again", enumerator);
        RecordCurrent(trace, "mutated-end-current", enumerator, identities);
        RecordReset(trace, "mutated-reset", enumerator);
        RecordCurrent(trace, "mutated-reset-current", enumerator, identities);
        Drain(trace, "mutated-replay", enumerator, identities);
        observations.Add(new { scenario = $"mutation-{consumer}-{shape}-{stage}-{sharedView}-{mutation}",
            countBefore = count, countAfterCreation, result, state, trace });
    }

    private static IEnumerator GetEnumerator(SubItems items, string consumer) => consumer == "public" ? items.GetEnumerator() : ((IEnumerable)items).GetEnumerator();

    private static void Mutate(ListViewItem owner, SubItems items, string mutation)
    {
        SubItem New(string suffix) => new DerivedSubItem("new-" + suffix);
        switch (mutation)
        {
            case "replace-first": items[0] = New("first"); break;
            case "replace-middle": items[items.Count / 2] = New("middle"); break;
            case "replace-last": items[items.Count - 1] = New("last"); break;
            case "replace-same": items[0] = items[0]; break;
            case "add-one": items.Add(New("added")); break;
            case "add-many": for (int i = 0; i < 20; i++) items.Add(New("added-" + i)); break;
            case "insert-first": items.Insert(0, New("inserted-first")); break;
            case "insert-middle": items.Insert(items.Count / 2, New("inserted-middle")); break;
            case "insert-last": items.Insert(items.Count, New("inserted-last")); break;
            case "remove-first": items.RemoveAt(0); break;
            case "remove-middle": items.RemoveAt(items.Count / 2); break;
            case "remove-last": items.RemoveAt(items.Count - 1); break;
            case "remove-reference": items.Remove(items.Count > 0 ? items[0] : null); break;
            case "remove-missing": items.Remove(New("missing")); break;
            case "clear": items.Clear(); break;
            case "clear-add": items.Clear(); items.Add(New("after-clear")); break;
            case "restore-property": _ = owner.SubItems; break;
            case "clear-restore": items.Clear(); _ = owner.SubItems; break;
            case "edit-text": items[0].Text = "edited"; break;
            default: throw new InvalidOperationException(mutation);
        }
    }

    private static void Drain(List<object> trace, string label, IEnumerator enumerator, Identities identities)
    {
        for (int i = 0; i < 64; i++)
        {
            bool? moved = null;
            object result = Capture(() => moved = enumerator.MoveNext());
            trace.Add(new { step = label + "-move-" + i, result });
            RecordCurrent(trace, label + "-current-" + i, enumerator, identities);
            if (moved != true) return;
        }
        throw new InvalidOperationException("Enumeration exceeded the bounded probe's 64 slots.");
    }

    private static void RecordCurrent(List<object> trace, string step, IEnumerator enumerator, Identities identities) =>
        trace.Add(new { step, result = Capture(() => identities.Describe(enumerator.Current)) });
    private static void RecordMove(List<object> trace, string step, IEnumerator enumerator) =>
        trace.Add(new { step, result = Capture(() => enumerator.MoveNext()) });
    private static void RecordReset(List<object> trace, string step, IEnumerator enumerator) =>
        trace.Add(new { step, result = Capture(() => { enumerator.Reset(); return null; }) });
    private static object Capture(Func<object?> action)
    {
        try { return new { exception = (string?)null, parameter = (string?)null, value = action() }; }
        catch (Exception exception) { return new { exception = exception.GetType().Name, parameter = (exception as ArgumentException)?.ParamName, value = (object?)null }; }
    }

    private static object State((ListViewItem Owner, SubItems First, SubItems Second) setup, Identities identities) => new
    {
        count = setup.First.Count, secondCount = setup.Second.Count, ownerText = setup.Owner.Text,
        items = Enumerable.Range(0, setup.First.Count).Select(index => identities.Describe(setup.First[index])).ToArray(),
        sameViews = setup.First.Count == setup.Second.Count && Enumerable.Range(0, setup.First.Count).All(index => ReferenceEquals(setup.First[index], setup.Second[index]))
    };

    private static readonly string[] Shapes = ["fresh", "property-one", "added-one", "added-four", "added-five", "ctor-three", "ctor-five", "cleared-external", "cleared-property", "duplicate-derived", "prepared-spare"];
    private static readonly string[] Mutations = ["replace-first", "replace-middle", "replace-last", "replace-same", "add-one", "add-many", "insert-first", "insert-middle", "insert-last", "remove-first", "remove-middle", "remove-last", "remove-reference", "remove-missing", "clear", "clear-add", "restore-property", "clear-restore", "edit-text"];
    private static (ListViewItem Owner, SubItems First, SubItems Second) Create(string shape)
    {
        var owner = shape switch
        {
            "ctor-three" or "cleared-external" or "cleared-property" => new ListViewItem(new[] { "first", "second", "third" }),
            "ctor-five" => new ListViewItem(new[] { "first", "second", "third", "fourth", "fifth" }),
            _ => new ListViewItem()
        };
        var first = new SubItems(owner);
        var second = new SubItems(owner);
        if (shape == "property-one") _ = owner.SubItems;
        else if (shape == "cleared-external") first.Clear();
        else if (shape == "cleared-property") owner.SubItems.Clear();
        else if (shape == "duplicate-derived")
        {
            var duplicate = new DerivedSubItem("duplicate");
            first.AddRange([duplicate, new DerivedSubItem("middle"), duplicate]);
        }
        else if (shape.StartsWith("added-") || shape == "prepared-spare")
        {
            int count = shape switch { "added-one" => 1, "added-four" => 4, "added-five" => 5, _ => 9 };
            for (int index = 0; index < count; index++) first.Add(new DerivedSubItem("item-" + index));
            if (shape == "prepared-spare") while (first.Count > 3) first.RemoveAt(first.Count - 1);
        }
        return (owner, first, second);
    }

    private sealed class Identities
    {
        private readonly Dictionary<object, int> _ids = new(ReferenceEqualityComparer.Instance);
        public Identities(SubItems items)
        {
            for (int index = 0; index < items.Count; index++) Describe(items[index]);
        }
        public object Describe(object? value)
        {
            if (value is null) return new { kind = "null" };
            if (value is not SubItem item) return new { kind = "other", type = value.GetType().FullName };
            if (!_ids.TryGetValue(item, out int id)) _ids.Add(item, id = _ids.Count);
            return new { kind = "subitem", id, item.Text, derived = item is DerivedSubItem };
        }
    }

    private sealed class DerivedSubItem(string text) : SubItem(null, text)
    {
        public override bool Equals(object? obj) => throw new InvalidOperationException("Enumeration must preserve identity without calling Equals.");
        public override int GetHashCode() => 1;
    }
}
