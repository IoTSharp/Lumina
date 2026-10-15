using System.Collections;
using System.Drawing;
using Lumina.Forms;
using Xunit;
using SubItem = Lumina.Forms.ListViewItem.ListViewSubItem;
using SubItems = Lumina.Forms.ListViewItem.ListViewSubItemCollection;

namespace Lumina.Tests;

public class ListViewSubItemInterfacesTests
{
    [Fact]
    public void StandardInterfacesAndTypedQueryMetadataMatchReference()
    {
        Type type = typeof(SubItems);
        Assert.True(typeof(IList).IsAssignableFrom(type));
        Assert.True(typeof(ICollection).IsAssignableFrom(type));
        foreach (string name in new[] { "Contains", "IndexOf" })
        {
            var method = type.GetMethod(name, [typeof(SubItem)]);
            Assert.NotNull(method);
            Assert.True(method.IsPublic);
            Assert.False(method.IsVirtual);
            Assert.False(method.IsFinal);
            Assert.Equal("subItem", Assert.Single(method.GetParameters()).Name);
        }

        Assert.Null(type.GetMethod("CopyTo", [typeof(Array), typeof(int)]));
        var mapping = type.GetInterfaceMap(typeof(ICollection));
        int position = Array.FindIndex(mapping.InterfaceMethods, method => method.Name == "CopyTo");
        var copy = mapping.TargetMethods[position];
        Assert.True(copy.IsPrivate);
        Assert.True(copy.IsVirtual);
        Assert.True(copy.IsFinal);
        Assert.Equal(new[] { "dest", "index" }, copy.GetParameters().Select(parameter => parameter.Name));
    }

    [Theory]
    [InlineData("fresh")]
    [InlineData("populated")]
    [InlineData("cleared-external")]
    [InlineData("cleared-property")]
    public void InterfaceFlagsAndSyncRootBelongToEachView(string shape)
    {
        var views = Create(shape);
        IList list = views.First;
        ICollection collection = views.First;
        ICollection second = views.Second;
        ICollection unrelated = new SubItems(new ListViewItem());
        int count = views.First.Count;

        Assert.False(list.IsReadOnly);
        Assert.False(list.IsFixedSize);
        Assert.True(collection.IsSynchronized);
        Assert.Same(views.First, collection.SyncRoot);
        Assert.Same(collection.SyncRoot, collection.SyncRoot);
        Assert.Same(views.Second, second.SyncRoot);
        Assert.NotSame(collection.SyncRoot, second.SyncRoot);
        Assert.NotSame(collection.SyncRoot, unrelated.SyncRoot);
        Assert.Equal(count, collection.Count);
        Assert.Equal(count, views.First.Count);
        Assert.Equal(count, views.Second.Count);
    }

    public static IEnumerable<object[]> WrongObjectMutations()
    {
        foreach (string shape in Shapes)
        foreach (string kind in new[] { "null", "text", "integer", "object", "item" })
        foreach (string operation in new[] { "add", "insert", "set" })
            yield return [shape, kind, operation];
    }

    [Theory]
    [MemberData(nameof(WrongObjectMutations))]
    public void WrongObjectMutationsValidateTypeBeforePositionAndLeaveSharedDataUnchanged(string shape, string kind, string operation)
    {
        var views = Create(shape);
        IList list = views.First;
        object? value = WrongValue(kind);
        var before = views.First.ToArray();
        string parameter = operation == "set" ? "value" : "item";

        Assert.Throws<ArgumentException>(parameter, () =>
        {
            if (operation == "add") list.Add(value);
            else if (operation == "insert") list.Insert(-1, value);
            else list[-1] = value;
        });

        AssertSameItems(before, views.First);
        AssertSameItems(before, views.Second);
        Assert.Equal(shape == "populated" ? "primary" : string.Empty, views.Owner.Text);
        Assert.Equal(before.Length, views.First.Count);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    [InlineData(3)]
    [InlineData(int.MaxValue)]
    public void ObjectIndexerGetAndValidReplacementValidatePosition(int index)
    {
        var views = Create("populated");
        IList list = views.First;
        var before = views.First.ToArray();
        var replacement = new SubItem(null, "replacement");

        Assert.Throws<ArgumentOutOfRangeException>("index", () => _ = list[index]);
        Assert.Throws<ArgumentOutOfRangeException>("index", () => list[index] = replacement);

        AssertSameItems(before, views.First);
        AssertSameItems(before, views.Second);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    [InlineData(4)]
    [InlineData(int.MaxValue)]
    public void ObjectInsertValidItemValidatesPositionAndLeavesSharedDataUnchanged(int index)
    {
        var views = Create("populated");
        IList list = views.First;
        var before = views.First.ToArray();

        Assert.Throws<ArgumentOutOfRangeException>("index", () => list.Insert(index, new SubItem(null, "replacement")));

        AssertSameItems(before, views.First);
        AssertSameItems(before, views.Second);
    }

    [Theory]
    [InlineData("add", 3)]
    [InlineData("insert", 0)]
    [InlineData("set", 0)]
    public void ObjectMutationsKeepExactSubItemIdentityAndOtherOwnerData(string operation, int position)
    {
        var views = Create("populated");
        IList list = views.First;
        var source = new ListViewItem(new[] { "source primary", "source detail" });
        var sourceItems = source.SubItems;
        var candidate = sourceItems[0];
        object tag = new();
        candidate.Tag = tag;
        candidate.Name = "key";

        if (operation == "add") Assert.Equal(position, list.Add(candidate));
        else if (operation == "insert") list.Insert(position, candidate);
        else list[position] = candidate;

        Assert.Same(candidate, list[position]);
        Assert.Same(candidate, views.Second[position]);
        Assert.Same(candidate, sourceItems[0]);
        Assert.Equal(2, sourceItems.Count);
        Assert.Same(tag, ((SubItem)list[position]!).Tag);
        candidate.Text = "shared changed";
        Assert.Equal("shared changed", source.Text);
        Assert.Equal(position == 0 ? "shared changed" : "primary", views.Owner.Text);
        AssertSameItems(views.First.ToArray(), views.Second);
    }

    [Fact]
    public void ObjectAddRepeatedReferenceReturnsFirstPositionAndAppendsEachTime()
    {
        var owner = new ListViewItem();
        var items = new SubItems(owner);
        IList list = items;
        var first = new EqualSubItem("first");
        var second = new EqualSubItem("second");

        Assert.Equal(0, list.Add(first));
        Assert.Equal(1, list.Add(second));
        Assert.Equal(0, list.Add(first));
        Assert.Equal(1, list.Add(second));

        AssertSameItems([first, second, first, second], items);
        Assert.Equal("first", owner.Text);
    }

    [Fact]
    public void ObjectAndTypedQueriesUseFirstReferenceWithoutCallingOverriddenEquals()
    {
        var owner = new ListViewItem();
        var items = new SubItems(owner);
        var first = new ThrowingEqualsSubItem();
        var second = new ThrowingEqualsSubItem();
        var missing = new ThrowingEqualsSubItem();
        items.AddRange([first, second, first]);
        IList list = items;
        IList<SubItem> generic = items;

        foreach (object? value in new object?[] { null, "wrong", new object(), missing })
        {
            Assert.False(list.Contains(value));
            Assert.Equal(-1, list.IndexOf(value));
            list.Remove(value);
        }

        Assert.True(list.Contains(second));
        Assert.Equal(1, list.IndexOf(second));
        Assert.True(items.Contains(first));
        Assert.True(items.Contains(second));
        Assert.False(items.Contains(null));
        Assert.False(items.Contains(missing));
        Assert.Equal(0, items.IndexOf(first));
        Assert.Equal(1, items.IndexOf(second));
        Assert.Equal(-1, items.IndexOf(null));
        Assert.Equal(-1, items.IndexOf(missing));
        Assert.True(generic.Contains(second));
        Assert.False(generic.Contains(missing));
        Assert.Equal(1, generic.IndexOf(second));
        Assert.Equal(-1, generic.IndexOf(missing));
        Assert.Equal(0, list.Add(first));

        list.Remove(first);

        AssertSameItems([second, first, first], items);
        Assert.Equal(1, list.IndexOf(first));
        Assert.True(generic.Remove(second));
        Assert.False(generic.Remove(missing));
        AssertSameItems([first, first], items);
    }

    [Theory]
    [InlineData("fresh")]
    [InlineData("cleared-external")]
    [InlineData("cleared-property")]
    public void EmptyQueriesRemovalAndFailedMutationsDoNotRestorePrimary(string shape)
    {
        var views = Create(shape);
        IList list = views.First;

        Assert.False(list.Contains(null));
        Assert.False(list.Contains("wrong"));
        Assert.Equal(-1, list.IndexOf(null));
        Assert.Equal(-1, list.IndexOf(new object()));
        Assert.False(views.First.Contains(null));
        Assert.Equal(-1, views.First.IndexOf(null));
        list.Remove(null);
        list.Remove(new SubItem());
        list.Clear();
        Assert.Throws<ArgumentException>("value", () => list[0] = null);
        Assert.Throws<ArgumentOutOfRangeException>("index", () => _ = list[0]);

        Assert.Empty(views.First);
        Assert.Empty(views.Second);
        Assert.Equal(string.Empty, views.Owner.Text);
        Assert.Empty(views.First);

        var restored = Assert.Single(views.Owner.SubItems);
        Assert.Same(restored, Assert.Single(views.First));
        Assert.Same(restored, Assert.Single(views.Second));
    }

    public static IEnumerable<object[]> EmptyArrayCopies()
    {
        foreach (string shape in new[] { "fresh", "cleared-external", "cleared-property" })
        foreach (string kind in new[] { "null", "empty", "object", "string", "int", "derived", "rank2", "lower2", "lower-minus2" })
        foreach (int index in new[] { int.MinValue, -1, 0, 5, int.MaxValue })
            yield return [shape, kind, index];
    }

    [Theory]
    [MemberData(nameof(EmptyArrayCopies))]
    public void EmptyArrayCopySkipsEveryDestinationValidationAndDoesNotRestorePrimary(string shape, string kind, int index)
    {
        var views = Create(shape);
        Array? destination = Destination(kind);
        object?[]? before = destination?.Cast<object?>().ToArray();

        ((ICollection)views.First).CopyTo(destination!, index);
        views.First.CopyTo(null!, index);
        ((ICollection<SubItem>)views.First).CopyTo([], index);

        if (destination is not null) Assert.Equal(before, destination.Cast<object?>());
        Assert.Empty(views.First);
        Assert.Empty(views.Second);
        Assert.Equal(string.Empty, views.Owner.Text);
        Assert.Empty(views.First);
    }

    [Theory]
    [InlineData("null", -1, "ArgumentNullException", "destinationArray")]
    [InlineData("null", 0, "ArgumentNullException", "destinationArray")]
    [InlineData("rank2", -1, "ArgumentException", null)]
    [InlineData("rank2", 0, "ArgumentException", null)]
    [InlineData("empty", -1, "ArgumentOutOfRangeException", "destinationIndex")]
    [InlineData("empty", 0, "ArgumentException", "destinationArray")]
    [InlineData("object", -1, "ArgumentOutOfRangeException", "destinationIndex")]
    [InlineData("object", 3, "ArgumentException", "destinationArray")]
    [InlineData("object", int.MaxValue, "ArgumentException", "destinationArray")]
    [InlineData("string", -1, "ArgumentOutOfRangeException", "destinationIndex")]
    [InlineData("string", 0, "ArgumentException", null)]
    [InlineData("string", 3, "ArgumentException", "destinationArray")]
    [InlineData("int", 0, "ArgumentException", null)]
    [InlineData("int", 3, "ArgumentException", "destinationArray")]
    [InlineData("derived", 0, "InvalidCastException", null)]
    [InlineData("derived", 3, "ArgumentException", "destinationArray")]
    [InlineData("lower2", 1, "ArgumentOutOfRangeException", "destinationIndex")]
    [InlineData("lower2", 5, "ArgumentException", "destinationArray")]
    [InlineData("lower-minus2", -3, "ArgumentOutOfRangeException", "destinationIndex")]
    [InlineData("lower-minus2", 1, "ArgumentException", "destinationArray")]
    public void PopulatedArrayCopyPreservesReferenceValidationOrderAndFailureState(string kind, int index, string exceptionType, string? parameter)
    {
        var views = Create("populated");
        var source = views.First.ToArray();
        Array? destination = Destination(kind);
        object?[]? before = destination?.Cast<object?>().ToArray();

        Exception? exception = Record.Exception(() => ((ICollection)views.First).CopyTo(destination!, index));

        Assert.NotNull(exception);
        Assert.Equal(exceptionType, exception.GetType().Name);
        Assert.Equal(parameter, (exception as ArgumentException)?.ParamName);
        if (destination is not null) Assert.Equal(before, destination.Cast<object?>());
        AssertSameItems(source, views.First);
        AssertSameItems(source, views.Second);
    }

    [Theory]
    [InlineData(2, 2)]
    [InlineData(2, 3)]
    [InlineData(2, 4)]
    [InlineData(-2, -2)]
    [InlineData(-2, -1)]
    [InlineData(-2, 0)]
    public void ArrayCopyUsesActualNonzeroLowerBoundAndPreservesElementIdentity(int lowerBound, int index)
    {
        var views = Create("populated");
        Array destination = Array.CreateInstance(typeof(SubItem), [5], [lowerBound]);

        ((ICollection)views.First).CopyTo(destination, index);

        for (int position = lowerBound; position < lowerBound + 5; position++)
        {
            if (position >= index && position < index + views.First.Count)
                Assert.Same(views.First[position - index], destination.GetValue(position));
            else Assert.Null(destination.GetValue(position));
        }
        AssertSameItems(views.First.ToArray(), views.Second);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void IncompatibleDerivedArrayKeepsEarlierWritesAndLeavesCollectionUnchanged(bool typedCopy)
    {
        var owner = new ListViewItem();
        var items = new SubItems(owner);
        var first = new EqualSubItem("first");
        var middle = new SubItem(null, "middle");
        var last = new EqualSubItem("last");
        items.AddRange([first, middle, last]);
        var destination = new EqualSubItem[4];

        Assert.Throws<InvalidCastException>(() =>
        {
            if (typedCopy) items.CopyTo(destination, 1);
            else ((ICollection)items).CopyTo(destination, 1);
        });

        Assert.Null(destination[0]);
        Assert.Same(first, destination[1]);
        Assert.Null(destination[2]);
        Assert.Null(destination[3]);
        AssertSameItems([first, middle, last], items);
        Assert.Equal("first", owner.Text);
    }

    [Fact]
    public void TypedAndGenericCopyUseStandardArrayValidationAndExactReferences()
    {
        var views = Create("populated");
        var destination = new SubItem[5];
        Assert.Throws<ArgumentNullException>("destinationArray", () => views.First.CopyTo(null!, -1));
        Assert.Throws<ArgumentOutOfRangeException>("destinationIndex", () => views.First.CopyTo(destination, -1));
        Assert.Throws<ArgumentException>("destinationArray", () => views.First.CopyTo(destination, 3));
        Assert.All(destination, Assert.Null);

        views.First.CopyTo(destination, 1);

        Assert.Null(destination[0]);
        Assert.Null(destination[4]);
        for (int index = 0; index < views.First.Count; index++) Assert.Same(views.First[index], destination[index + 1]);
        var genericDestination = new SubItem[3];
        ((ICollection<SubItem>)views.First).CopyTo(genericDestination, 0);
        AssertSameItems(genericDestination, views.First);
    }

    [Theory]
    [InlineData("add", 2, 1)]
    [InlineData("insert-first", 2, 1)]
    [InlineData("insert-middle", 2, 1)]
    [InlineData("set-first", 2, 1)]
    [InlineData("set-middle", 2, 1)]
    [InlineData("remove-first", 0, 1)]
    [InlineData("contains", 2, 1)]
    [InlineData("index-of", 2, 1)]
    [InlineData("typed-contains", 2, 1)]
    [InlineData("typed-index-of", 2, 1)]
    [InlineData("copy", 2, 1)]
    [InlineData("wrong-add", 2, 1)]
    [InlineData("wrong-insert", 2, 1)]
    [InlineData("wrong-set", 2, 1)]
    [InlineData("null-add", 2, 1)]
    [InlineData("null-set", 2, 1)]
    [InlineData("clear", -1, -1)]
    public void InterfaceOperationsPreserveIndependentViewKeyCaches(string operation, int firstExpected, int otherExpected)
    {
        var views = Create("populated");
        var property = views.Owner.SubItems;
        var first = views.First;
        var second = views.Second;
        IList list = first;
        first[2].Name = "target";
        Assert.Equal(2, first.IndexOfKey("target"));
        first[1].Name = "target";
        Assert.Equal(1, second.IndexOfKey("target"));
        Assert.Equal(1, property.IndexOfKey("target"));
        first[0].Name = "target";
        var candidate = new SubItem(null, "candidate") { Name = "target" };

        switch (operation)
        {
            case "add": list.Add(candidate); break;
            case "insert-first": list.Insert(0, candidate); break;
            case "insert-middle": list.Insert(1, candidate); break;
            case "set-first": list[0] = candidate; break;
            case "set-middle": list[1] = candidate; break;
            case "remove-first": list.Remove(first[0]); break;
            case "contains": Assert.True(list.Contains(first[0])); break;
            case "index-of": Assert.Equal(0, list.IndexOf(first[0])); break;
            case "typed-contains": Assert.True(first.Contains(first[0])); break;
            case "typed-index-of": Assert.Equal(0, first.IndexOf(first[0])); break;
            case "copy": ((ICollection)first).CopyTo(new object[3], 0); break;
            case "wrong-add": Assert.Throws<ArgumentException>("item", () => list.Add("wrong")); break;
            case "wrong-insert": Assert.Throws<ArgumentException>("item", () => list.Insert(-1, "wrong")); break;
            case "wrong-set": Assert.Throws<ArgumentException>("value", () => list[-1] = "wrong"); break;
            case "null-add": Assert.Throws<ArgumentException>("item", () => list.Add(null)); break;
            case "null-set": Assert.Throws<ArgumentException>("value", () => list[1] = null); break;
            case "clear": list.Clear(); break;
        }

        AssertSameItems(first.ToArray(), second);
        AssertSameItems(first.ToArray(), property);
        Assert.Equal(firstExpected, first.IndexOfKey("target"));
        Assert.Equal(otherExpected, second.IndexOfKey("target"));
        Assert.Equal(otherExpected, property.IndexOfKey("target"));
        Assert.Equal(operation == "clear" ? -1 : 0, new SubItems(views.Owner).IndexOfKey("target"));
    }

    [Theory]
    [InlineData("add")]
    [InlineData("insert")]
    [InlineData("set")]
    [InlineData("remove")]
    [InlineData("copy")]
    [InlineData("failed-copy")]
    [InlineData("wrong-add")]
    [InlineData("clear")]
    public void InterfaceOperationsPreserveBorrowedFontAndSubItemState(string operation)
    {
        using var font = new Font("Arial", 12.5f, FontStyle.Bold, GraphicsUnit.Point);
        float height = font.GetHeight(96f);
        var owner = new ListViewItem("primary");
        var items = owner.SubItems;
        var second = new SubItems(owner);
        IList list = items;
        object tag = new();
        var styled = new SubItem(null, "styled", Color.Crimson, Color.AliceBlue, font) { Name = "key", Tag = tag };
        items.Add(styled);
        var destination = new object[3];

        switch (operation)
        {
            case "add": Assert.Equal(1, list.Add(styled)); break;
            case "insert": list.Insert(0, styled); break;
            case "set": list[0] = styled; break;
            case "remove": list.Remove(styled); break;
            case "copy": ((ICollection)items).CopyTo(destination, 1); Assert.Same(styled, destination[2]); break;
            case "failed-copy": Assert.Throws<ArgumentException>("destinationArray", () => ((ICollection)items).CopyTo(destination, 2)); Assert.All(destination, Assert.Null); break;
            case "wrong-add": Assert.Throws<ArgumentException>("item", () => list.Add("wrong")); break;
            case "clear": list.Clear(); break;
        }

        AssertSameItems(items.ToArray(), second);
        Assert.Same(font, styled.Font);
        Assert.Same(tag, styled.Tag);
        Assert.Equal("styled", styled.Text);
        Assert.Equal("key", styled.Name);
        Assert.Equal(Color.Crimson, styled.ForeColor);
        Assert.Equal(Color.AliceBlue, styled.BackColor);
        Assert.Equal(height, font.GetHeight(96f));
        Assert.Equal(height, styled.Font.GetHeight(96f));
    }

    private static Array? Destination(string kind) => kind switch
    {
        "null" => null, "empty" => new object[0], "object" => new object[5], "string" => new string[5],
        "int" => new int[5], "derived" => new EqualSubItem[5], "rank2" => new object[2, 3],
        "lower2" => Array.CreateInstance(typeof(object), [5], [2]),
        "lower-minus2" => Array.CreateInstance(typeof(object), [5], [-2]),
        _ => throw new ArgumentException(kind)
    };

    private static object? WrongValue(string kind) => kind switch
    {
        "null" => null, "text" => "wrong", "integer" => 42, "object" => new object(),
        "item" => new ListViewItem("wrong"), _ => throw new ArgumentException(kind)
    };

    private static readonly string[] Shapes = ["fresh", "populated", "cleared-external", "cleared-property"];
    private static (ListViewItem Owner, SubItems First, SubItems Second) Create(string shape)
    {
        var owner = shape == "fresh" ? new ListViewItem() : new ListViewItem(new[] { "primary", "detail", "last" });
        var first = new SubItems(owner);
        var second = new SubItems(owner);
        if (shape == "cleared-property") owner.SubItems.Clear();
        else if (shape == "cleared-external") first.Clear();
        return (owner, first, second);
    }

    private static void AssertSameItems(SubItem[] expected, SubItems actual)
    {
        Assert.Equal(expected.Length, actual.Count);
        for (int index = 0; index < expected.Length; index++) Assert.Same(expected[index], actual[index]);
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
