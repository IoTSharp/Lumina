using System.Collections;
using System.Drawing;
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
        ObserveObjectOperations(observations);
        ObserveIdentity(observations);
        ObserveArrays(observations);
        ObserveMixedArrays(observations);
        ObserveTypedArrays(observations);
        ObserveEmptyQueries(observations);
        ObserveCaches(observations);
        ObserveBorrowedFont(observations);
        Console.WriteLine(JsonSerializer.Serialize(new { version, observations }));
    }

    private static void ObserveMetadata(List<object> observations)
    {
        Type type = typeof(ListViewItem.ListViewSubItemCollection);
        observations.Add(new { scenario = "interfaces", list = typeof(IList).IsAssignableFrom(type), collection = typeof(ICollection).IsAssignableFrom(type), enumerable = typeof(IEnumerable).IsAssignableFrom(type) });
        foreach ((string name, Type[] parameters) in new[]
        {
            ("Contains", new[] { typeof(SubItem) }),
            ("IndexOf", new[] { typeof(SubItem) })
        })
        {
            MethodInfo? method = type.GetMethod(name, parameters);
            observations.Add(new { scenario = "signature-" + name, exists = method is not null, isPublic = method?.IsPublic, isVirtual = method?.IsVirtual, isFinal = method?.IsFinal, parameters = method?.GetParameters().Select(parameter => parameter.Name).ToArray() });
        }
        MethodInfo? publicCopy = type.GetMethod("CopyTo", [typeof(Array), typeof(int)]);
        MethodInfo contractCopy = typeof(ICollection).GetMethod("CopyTo")!;
        InterfaceMapping mapping = type.GetInterfaceMap(typeof(ICollection));
        MethodInfo mappedCopy = mapping.TargetMethods[Array.IndexOf(mapping.InterfaceMethods, contractCopy)];
        observations.Add(new { scenario = "signature-copy", isPublic = publicCopy?.IsPublic, isVirtual = publicCopy?.IsVirtual, isFinal = publicCopy?.IsFinal, parameters = publicCopy?.GetParameters().Select(parameter => parameter.Name).ToArray(), mappedPublic = mappedCopy.IsPublic, mappedPrivate = mappedCopy.IsPrivate, mappedVirtual = mappedCopy.IsVirtual, mappedFinal = mappedCopy.IsFinal, mappedParameters = mappedCopy.GetParameters().Select(parameter => parameter.Name).ToArray() });
        foreach (string shape in Shapes)
        {
            var views = Create(shape);
            IList list = (IList)views.First;
            ICollection first = (ICollection)views.First;
            ICollection second = (ICollection)views.Second;
            ICollection otherOwner = (ICollection)new ListViewItem.ListViewSubItemCollection(new ListViewItem());
            observations.Add(new { scenario = "flags-" + shape, list.IsReadOnly, list.IsFixedSize, first.IsSynchronized, rootIsView = ReferenceEquals(first.SyncRoot, first), rootIsOwner = ReferenceEquals(first.SyncRoot, views.Owner), rootStable = ReferenceEquals(first.SyncRoot, first.SyncRoot), viewsShareRoot = ReferenceEquals(first.SyncRoot, second.SyncRoot), otherOwnerSharesRoot = ReferenceEquals(first.SyncRoot, otherOwner.SyncRoot), count = first.Count, ownerText = views.Owner.Text });
        }
    }

    private static void ObserveObjectOperations(List<object> observations)
    {
        foreach (string shape in Shapes)
        {
            foreach (string kind in new[] { "null", "text", "integer", "object", "item", "new-subitem", "first", "last", "equal", "throwing-equals" })
            {
                foreach (string operation in new[] { "add", "contains", "index-of", "remove", "typed-contains", "typed-index-of" })
                {
                    if (operation.StartsWith("typed-") && kind is "text" or "integer" or "object" or "item") continue;
                    var views = Create(shape);
                    IList list = (IList)views.First;
                    object? value = Candidate(kind, views.First);
                    var before = Items(views.First);
                    object result = operation switch
                    {
                        "add" => CaptureValue(() => list.Add(value)),
                        "contains" => CaptureValue(() => list.Contains(value)),
                        "index-of" => CaptureValue(() => list.IndexOf(value)),
                        "typed-contains" => CaptureValue(() => views.First.Contains((SubItem)value!)),
                        "typed-index-of" => CaptureValue(() => views.First.IndexOf((SubItem)value!)),
                        _ => Capture(() => list.Remove(value))
                    };
                    observations.Add(new { scenario = operation + "-" + shape + "-" + kind, result, state = State(views.Owner, views.First), unchanged = Same(views.First, before), sameViews = Same(views.First, Items(views.Second)), candidatePositions = Positions(views.First, value) });
                }
            }
            int count = Create(shape).First.Count;
            foreach (int index in new[] { int.MinValue, -1, 0, 1, count - 1, count, count + 1, int.MaxValue }.Distinct())
            {
                var readViews = Create(shape);
                IList readList = (IList)readViews.First;
                SubItem? read = null;
                object getResult = Capture(() => read = (SubItem?)readList[index]);
                observations.Add(new { scenario = "get-" + shape + "-" + index, result = getResult, text = read?.Text, storedSame = read is not null && Positions(readViews.First, read).Contains(index), state = State(readViews.Owner, readViews.First) });
                foreach (string kind in new[] { "null", "text", "integer", "object", "item", "new-subitem", "first", "equal", "throwing-equals" })
                {
                    foreach (string operation in new[] { "set", "insert" })
                    {
                        var views = Create(shape);
                        IList list = (IList)views.First;
                        object? value = Candidate(kind, views.First);
                        var before = Items(views.First);
                        object result = Capture(() => { if (operation == "set") list[index] = value; else list.Insert(index, value); });
                        observations.Add(new { scenario = operation + "-" + shape + "-" + index + "-" + kind, result, state = State(views.Owner, views.First), unchanged = Same(views.First, before), sameViews = Same(views.First, Items(views.Second)), candidatePositions = Positions(views.First, value) });
                    }
                }
                foreach (string operation in new[] { "remove-at", "clear" })
                {
                    if (operation == "clear" && index != 0) continue;
                    var views = Create(shape);
                    IList list = (IList)views.First;
                    object result = Capture(() => { if (operation == "clear") list.Clear(); else list.RemoveAt(index); });
                    observations.Add(new { scenario = operation + "-" + shape + "-" + index, result, state = State(views.Owner, views.First), sameViews = Same(views.First, Items(views.Second)) });
                }
            }
        }
    }

    private static void ObserveIdentity(List<object> observations)
    {
        foreach (string operation in new[] { "contains", "index-of", "typed-contains", "typed-index-of", "remove", "add", "insert", "set" })
        {
            foreach (bool missing in new[] { false, true })
            {
                var owner = new ListViewItem();
                var items = new ListViewItem.ListViewSubItemCollection(owner);
                var first = new EqualSubItem("first");
                var second = new EqualSubItem("second");
                items.AddRange([first, second, first]);
                SubItem candidate = missing ? new EqualSubItem("missing") : second;
                IList list = (IList)items;
                object result = operation switch
                {
                    "contains" => CaptureValue(() => list.Contains(candidate)),
                    "index-of" => CaptureValue(() => list.IndexOf(candidate)),
                    "typed-contains" => CaptureValue(() => items.Contains(candidate)),
                    "typed-index-of" => CaptureValue(() => items.IndexOf(candidate)),
                    "add" => CaptureValue(() => list.Add(candidate)),
                    "insert" => Capture(() => list.Insert(1, candidate)),
                    "set" => Capture(() => list[1] = candidate),
                    _ => Capture(() => list.Remove(candidate))
                };
                observations.Add(new { scenario = "identity-" + operation + "-" + missing, result, state = State(owner, items), firstPositions = Positions(items, first), secondPositions = Positions(items, second), candidatePositions = Positions(items, candidate) });
            }
        }
        foreach (string operation in new[] { "add", "insert", "set" })
        {
            var source = new ListViewItem(new[] { "source primary", "source detail" });
            var owner = new ListViewItem(new[] { "primary", "detail" });
            var items = owner.SubItems;
            var second = new ListViewItem.ListViewSubItemCollection(owner);
            var candidate = source.SubItems[0];
            IList list = (IList)items;
            object result = operation == "add" ? CaptureValue(() => list.Add(candidate)) : Capture(() => { if (operation == "insert") list.Insert(0, candidate); else list[0] = candidate; });
            candidate.Text = "shared changed";
            observations.Add(new { scenario = "other-owner-" + operation, result, state = State(owner, items), sourceState = State(source, source.SubItems), sameViews = Same(items, Items(second)), candidatePositions = Positions(items, candidate), sourcePositions = Positions(source.SubItems, candidate) });
        }
        foreach (string candidateKind in new[] { "null", "same", "missing", "other-type" })
        {
            var owner = new ListViewItem();
            var items = new ListViewItem.ListViewSubItemCollection(owner);
            var stored = new ThrowingEqualsSubItem();
            items.Add(stored);
            IList list = (IList)items;
            object? candidate = candidateKind switch { "null" => null, "same" => stored, "missing" => new ThrowingEqualsSubItem(), _ => new object() };
            observations.Add(new { scenario = "throwing-equals-query-" + candidateKind, contains = CaptureValue(() => list.Contains(candidate)), indexOf = CaptureValue(() => list.IndexOf(candidate)), typedContains = candidate is null or SubItem ? CaptureValue(() => items.Contains((SubItem)candidate!)) : null, typedIndexOf = candidate is null or SubItem ? CaptureValue(() => items.IndexOf((SubItem)candidate!)) : null, remove = Capture(() => list.Remove(candidate)), state = State(owner, items) });
        }
    }

    private static void ObserveArrays(List<object> observations)
    {
        foreach (string shape in new[] { "fresh", "populated", "cleared-property" })
        {
            foreach (string kind in ArrayKinds)
            {
                foreach (int index in new[] { int.MinValue, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, int.MaxValue })
                {
                    var views = Create(shape);
                    Array? array = CreateArray(kind);
                    object before = ArrayState(array, views.First);
                    object result = Capture(() => ((ICollection)views.First).CopyTo(array!, index));
                    observations.Add(new { scenario = "copy-" + shape + "-" + kind + "-" + index, result, before, after = ArrayState(array, views.First), state = State(views.Owner, views.First), sameViews = Same(views.First, Items(views.Second)) });
                }
            }
        }
    }

    private static void ObserveTypedArrays(List<object> observations)
    {
        foreach (string shape in new[] { "fresh", "populated", "cleared-property" })
        {
            foreach (int length in new[] { -1, 0, 1, 3, 5 })
            {
                foreach (int index in new[] { int.MinValue, -1, 0, 1, 3, 5, int.MaxValue })
                {
                    var views = Create(shape);
                    SubItem[]? array = length < 0 ? null : new SubItem[length];
                    object result = Capture(() =>
                    {
#if REFERENCE
                        ((ICollection)views.First).CopyTo(array!, index);
#else
                        views.First.CopyTo(array!, index);
#endif
                    });
                    observations.Add(new { scenario = "typed-copy-" + shape + "-" + length + "-" + index, result, after = ArrayState(array, views.First), state = State(views.Owner, views.First) });
                }
            }
        }
    }

    private static void ObserveMixedArrays(List<object> observations)
    {
        foreach (string kind in new[] { "object", "base", "derived", "rank2", "nonzero-derived" })
        {
            foreach (int index in new[] { -1, 0, 1, 2, 3, 4 })
            {
                var owner = new ListViewItem();
                var items = new ListViewItem.ListViewSubItemCollection(owner);
                items.AddRange([new EqualSubItem("first"), new SubItem(null, "base"), new EqualSubItem("last")]);
                Array array = kind switch { "object" => new object[4], "base" => new SubItem[4], "derived" => new EqualSubItem[4], "rank2" => new object[2, 2], _ => Array.CreateInstance(typeof(EqualSubItem), [4], [1]) };
                object result = Capture(() => ((ICollection)items).CopyTo(array, index));
                observations.Add(new { scenario = "mixed-copy-" + kind + "-" + index, result, after = ArrayState(array, items), state = State(owner, items) });
            }
        }
    }

    private static void ObserveEmptyQueries(List<object> observations)
    {
        foreach (string shape in new[] { "fresh", "cleared-external", "cleared-property" })
        {
            var views = Create(shape);
            IList list = (IList)views.First;
            ICollection collection = (ICollection)views.First;
            object[] results =
            [
                CaptureValue(() => list.Contains(null)), CaptureValue(() => list.Contains(new object())),
                CaptureValue(() => list.IndexOf(null)), CaptureValue(() => list.IndexOf(new object())),
                CaptureValue(() => views.First.Contains(null!)), CaptureValue(() => views.First.IndexOf(null!)),
                Capture(() => list.Remove(null)), Capture(() => list.Remove(new object())),
                Capture(() => collection.CopyTo(null!, -1)), Capture(() => collection.CopyTo(new object[0], 100)),
                CaptureValue(() => list.IsFixedSize), CaptureValue(() => list.IsReadOnly),
                CaptureValue(() => collection.IsSynchronized)
            ];
            observations.Add(new { scenario = "empty-queries-" + shape, results, state = State(views.Owner, views.First), otherCount = views.Second.Count, rootIsView = ReferenceEquals(collection, collection.SyncRoot) });
        }
    }

    private static void ObserveCaches(List<object> observations)
    {
        foreach (string operation in new[] { "add", "insert-first", "insert-middle", "set-first", "set-middle", "remove-first", "contains", "index-of", "typed-contains", "typed-index-of", "copy", "wrong-add", "wrong-insert", "wrong-set", "null-add", "null-set", "clear" })
        {
            var owner = new ListViewItem(new[] { "primary", "earlier", "later" });
            var property = owner.SubItems;
            var first = new ListViewItem.ListViewSubItemCollection(owner);
            var second = new ListViewItem.ListViewSubItemCollection(owner);
            first[2].Name = "target";
            int firstBefore = first.IndexOfKey("target");
            first[1].Name = "target";
            int otherBefore = second.IndexOfKey("target");
            _ = property.IndexOfKey("target");
            first[0].Name = "target";
            var candidate = new SubItem(null, "candidate") { Name = "target" };
            IList list = (IList)first;
            object result = operation switch
            {
                "add" => CaptureValue(() => list.Add(candidate)),
                "insert-first" => Capture(() => list.Insert(0, candidate)),
                "insert-middle" => Capture(() => list.Insert(1, candidate)),
                "set-first" => Capture(() => list[0] = candidate),
                "set-middle" => Capture(() => list[1] = candidate),
                "remove-first" => Capture(() => list.Remove(first[0])),
                "contains" => CaptureValue(() => list.Contains(first[0])),
                "index-of" => CaptureValue(() => list.IndexOf(first[0])),
                "typed-contains" => CaptureValue(() => first.Contains(first[0])),
                "typed-index-of" => CaptureValue(() => first.IndexOf(first[0])),
                "copy" => Capture(() => ((ICollection)first).CopyTo(new object[3], 0)),
                "wrong-add" => CaptureValue(() => list.Add("wrong")),
                "wrong-insert" => Capture(() => list.Insert(-1, "wrong")),
                "wrong-set" => Capture(() => list[-1] = "wrong"),
                "null-add" => CaptureValue(() => list.Add(null)),
                "null-set" => Capture(() => list[1] = null),
                _ => Capture(() => list.Clear())
            };
            observations.Add(new { scenario = "cache-" + operation, result, firstBefore, otherBefore, firstAfter = first.IndexOfKey("target"), secondAfter = second.IndexOfKey("target"), propertyAfter = property.IndexOfKey("target"), freshAfter = new ListViewItem.ListViewSubItemCollection(owner).IndexOfKey("target"), state = State(owner, first), sameViews = Same(first, Items(second)) && Same(first, Items(property)) });
        }
    }

    private static void ObserveBorrowedFont(List<object> observations)
    {
        using var font = new Font("Arial", 12.5f, FontStyle.Bold, GraphicsUnit.Point);
        foreach (string operation in new[] { "add", "insert", "set", "remove", "copy", "failed-copy", "wrong-add", "clear" })
        {
            var owner = new ListViewItem("primary");
            var items = owner.SubItems;
            var second = new ListViewItem.ListViewSubItemCollection(owner);
            object tag = new();
            var styled = new SubItem(null, "styled", Color.Crimson, Color.AliceBlue, font) { Name = "key", Tag = tag };
            items.Add(styled);
            IList list = (IList)items;
            var target = new object[3];
            object result = operation switch
            {
                "add" => CaptureValue(() => list.Add(styled)),
                "insert" => Capture(() => list.Insert(0, styled)),
                "set" => Capture(() => list[0] = styled),
                "remove" => Capture(() => list.Remove(styled)),
                "copy" => Capture(() => ((ICollection)items).CopyTo(target, 1)),
                "failed-copy" => Capture(() => ((ICollection)items).CopyTo(target, 2)),
                "wrong-add" => CaptureValue(() => list.Add("wrong")),
                _ => Capture(() => list.Clear())
            };
            observations.Add(new { scenario = "borrowed-font-" + operation, result, state = State(owner, items), sameViews = Same(items, Items(second)), sameFont = ReferenceEquals(font, styled.Font), sameTag = ReferenceEquals(tag, styled.Tag), styled.Text, styled.Name, fore = styled.ForeColor.ToArgb(), back = styled.BackColor.ToArgb(), fontUsable = font.GetHeight(96f) > 0, copiedPositions = Enumerable.Range(0, target.Length).Where(index => ReferenceEquals(styled, target[index])).ToArray() });
        }
    }

    private static object? Candidate(string kind, ListViewItem.ListViewSubItemCollection items) => kind switch
    {
        "null" => null, "text" => "wrong", "integer" => 42, "object" => new object(), "item" => new ListViewItem("wrong"),
        "first" => items.Count == 0 ? null : items[0], "last" => items.Count == 0 ? null : items[items.Count - 1],
        "equal" => new EqualSubItem("equal"), "throwing-equals" => new ThrowingEqualsSubItem(),
        _ => new SubItem(null, "candidate") { Name = "candidate key" }
    };

    private static readonly string[] ArrayKinds = ["null", "object-0", "object-1", "object-3", "object-5", "subitem-5", "derived-5", "string-5", "int-5", "object-rank2", "subitem-rank2", "object-lower2", "object-lower-minus2", "subitem-lower2", "string-lower2"];
    private static Array? CreateArray(string kind) => kind switch
    {
        "null" => null,
        "object-0" => new object[0], "object-1" => new object[1], "object-3" => new object[3], "object-5" => new object[5],
        "subitem-5" => new SubItem[5], "derived-5" => new EqualSubItem[5], "string-5" => new string[5], "int-5" => new int[5],
        "object-rank2" => new object[2, 3], "subitem-rank2" => new SubItem[2, 3],
        "object-lower2" => Array.CreateInstance(typeof(object), [5], [2]),
        "object-lower-minus2" => Array.CreateInstance(typeof(object), [5], [-2]),
        "subitem-lower2" => Array.CreateInstance(typeof(SubItem), [5], [2]),
        "string-lower2" => Array.CreateInstance(typeof(string), [5], [2]),
        _ => throw new InvalidOperationException(kind)
    };

    private static object ArrayState(Array? array, ListViewItem.ListViewSubItemCollection source)
    {
        if (array is null) return new { isNull = true };
        return new
        {
            isNull = false, rank = array.Rank, lengths = Enumerable.Range(0, array.Rank).Select(array.GetLength).ToArray(), lowerBounds = Enumerable.Range(0, array.Rank).Select(array.GetLowerBound).ToArray(),
            values = array.Cast<object?>().Select(value => value is SubItem item ? "subitem:" + item.Text : value is null ? "null" : value.ToString()).ToArray(),
            sourcePositions = array.Cast<object?>().Select(value => Positions(source, value).FirstOrDefault(-1)).ToArray()
        };
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

    private static SubItem[] Items(ListViewItem.ListViewSubItemCollection items) => items.Cast<SubItem>().ToArray();
    private static bool Same(ListViewItem.ListViewSubItemCollection items, SubItem[] expected) => items.Count == expected.Length && expected.Select((item, index) => ReferenceEquals(item, items[index])).All(value => value);
    private static int[] Positions(ListViewItem.ListViewSubItemCollection items, object? value) => Enumerable.Range(0, items.Count).Where(index => ReferenceEquals(items[index], value)).ToArray();
    private static object State(ListViewItem owner, ListViewItem.ListViewSubItemCollection items) => new { count = items.Count, ownerText = owner.Text, texts = Items(items).Select(item => item.Text).ToArray(), names = Items(items).Select(item => item.Name).ToArray() };
    private static object Capture(Action action) => CaptureValue(() => { action(); return null; });
    private static object CaptureValue(Func<object?> action)
    {
        try { return new { exception = (string?)null, parameter = (string?)null, value = action() }; }
        catch (Exception exception) { return new { exception = exception.GetType().Name, parameter = (exception as ArgumentException)?.ParamName, value = (object?)null }; }
    }

    private sealed class EqualSubItem(string text) : SubItem(null, text)
    {
        public override bool Equals(object? obj) => obj is EqualSubItem;
        public override int GetHashCode() => 1;
    }
    private sealed class ThrowingEqualsSubItem() : SubItem(null, "throwing")
    {
        public override bool Equals(object? obj) => throw new InvalidOperationException("Equals must not be called for identity queries.");
        public override int GetHashCode() => 1;
    }
}
