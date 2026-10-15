using System.Collections;
using System.Reflection;
using Lumina.Forms;
using Xunit;
using SubItem = Lumina.Forms.ListViewItem.ListViewSubItem;
using SubItems = Lumina.Forms.ListViewItem.ListViewSubItemCollection;

namespace Lumina.Tests;

public class ListViewSubItemEnumerationTests
{
    [Fact]
    public void PublicEnumeratorHasTheStandardNonGenericSignatureAndInterfaceSlot()
    {
        Type type = typeof(SubItems);
        MethodInfo method = Assert.Single(type.GetMethods(BindingFlags.Public | BindingFlags.Instance), entry => entry.Name == "GetEnumerator");
        Assert.Equal(typeof(IEnumerator), method.ReturnType);
        Assert.Empty(method.GetParameters());
        Assert.True(method.IsPublic);
        Assert.True(method.IsVirtual);
        Assert.True(method.IsFinal);
        InterfaceMapping mapping = type.GetInterfaceMap(typeof(IEnumerable));
        Assert.Equal(method, Assert.Single(mapping.TargetMethods));
        Assert.True(typeof(IEnumerable<SubItem>).IsAssignableFrom(type));
    }

    [Theory]
    [InlineData("public")]
    [InlineData("interface")]
    [InlineData("generic")]
    public void EnumeratorPreservesOrderDuplicateIdentityAndDerivedObjects(string consumer)
    {
        var owner = new ListViewItem();
        var items = new SubItems(owner);
        var repeated = new ThrowingEqualsSubItem("repeated");
        var middle = new ThrowingEqualsSubItem("middle");
        items.AddRange([repeated, middle, repeated]);
        IEnumerator enumerator = GetEnumerator(items, consumer);

        Assert.Throws<InvalidOperationException>(() => _ = enumerator.Current);
        foreach (SubItem expected in new SubItem[] { repeated, middle, repeated })
        {
            Assert.True(enumerator.MoveNext());
            Assert.Same(expected, enumerator.Current);
            Assert.Same(expected, enumerator.Current);
        }
        Assert.False(enumerator.MoveNext());
        Assert.Throws<InvalidOperationException>(() => _ = enumerator.Current);
        Assert.False(enumerator.MoveNext());
        enumerator.Reset();
        Assert.Throws<InvalidOperationException>(() => _ = enumerator.Current);
        Assert.True(enumerator.MoveNext());
        Assert.Same(repeated, enumerator.Current);
    }

    [Theory]
    [InlineData("public")]
    [InlineData("interface")]
    [InlineData("generic")]
    public void EnumeratorsHaveIndependentPositionsAndReset(string consumer)
    {
        var owner = new ListViewItem(new[] { "first", "second" });
        var items = owner.SubItems;
        IEnumerator first = GetEnumerator(items, consumer);
        IEnumerator second = GetEnumerator(items, consumer);
        Assert.True(first.MoveNext());
        Assert.True(first.MoveNext());
        Assert.True(second.MoveNext());
        Assert.Same(items[1], first.Current);
        Assert.Same(items[0], second.Current);
        first.Reset();
        Assert.Same(items[0], second.Current);
        Assert.True(first.MoveNext());
        Assert.Same(items[0], first.Current);
    }

    public static IEnumerable<object[]> StructuralChanges()
    {
        foreach (string consumer in Consumers)
        foreach (string stage in new[] { "before", "middle", "ended" })
        foreach (bool otherView in new[] { false, true })
        foreach (string operation in new[] { "replace-first", "replace-middle", "replace-last", "replace-same", "add", "insert", "remove-first", "remove-middle", "remove-last", "remove-reference", "clear", "clear-restore" })
            yield return [consumer, stage, otherView, operation];
    }

    [Theory]
    [MemberData(nameof(StructuralChanges))]
    public void SuccessfulStructuralChangesInvalidateMoveAndResetButKeepTheCachedCurrent(string consumer, string stage, bool otherView, string operation)
    {
        var owner = new ListViewItem(new[] { "first", "middle", "last" });
        var items = owner.SubItems;
        var second = new SubItems(owner);
        IEnumerator enumerator = GetEnumerator(items, consumer);
        if (stage == "middle") Assert.True(enumerator.MoveNext());
        else if (stage == "ended") while (enumerator.MoveNext()) { }
        SubItem original = items[0];

        Change(owner, otherView ? second : items, operation);

        AssertCachedCurrent(enumerator, stage, original);
        Assert.Throws<InvalidOperationException>(() => enumerator.MoveNext());
        AssertCachedCurrent(enumerator, stage, original);
        Assert.Throws<InvalidOperationException>(() => enumerator.Reset());
        AssertCachedCurrent(enumerator, stage, original);
        Assert.Throws<InvalidOperationException>(() => enumerator.MoveNext());
    }

    public static IEnumerable<object[]> UnchangedCollections()
    {
        foreach (string consumer in Consumers)
        foreach (bool otherView in new[] { false, true })
        foreach (string operation in new[] { "read", "edit-text", "remove-missing", "remove-null", "bad-set", "bad-insert", "bad-remove", "null-add", "empty-range", "null-range-entries" })
            yield return [consumer, otherView, operation];
    }

    [Theory]
    [MemberData(nameof(UnchangedCollections))]
    public void ReadsObjectEditsAndUnsuccessfulMutationsPreserveTheEnumerator(string consumer, bool otherView, string operation)
    {
        var owner = new ListViewItem(new[] { "first", "middle", "last" });
        var items = owner.SubItems;
        SubItem[] expected = items.ToArray();
        var second = new SubItems(owner);
        IEnumerator enumerator = GetEnumerator(items, consumer);
        Assert.True(enumerator.MoveNext());

        ChangeWithoutStructure(owner, otherView ? second : items, operation);

        Assert.Same(expected[0], enumerator.Current);
        Assert.True(enumerator.MoveNext());
        Assert.Same(expected[1], enumerator.Current);
        Assert.True(enumerator.MoveNext());
        Assert.Same(expected[2], enumerator.Current);
        Assert.False(enumerator.MoveNext());
        enumerator.Reset();
        Assert.True(enumerator.MoveNext());
        Assert.Same(expected[0], enumerator.Current);
    }

    public static IEnumerable<object[]> EmptyViews()
    {
        foreach (string consumer in Consumers)
        foreach (string shape in new[] { "fresh", "cleared-external", "cleared-property", "last-removed" })
        foreach (string operation in new[] { "clear", "clear-other", "restore", "clear-restore", "restore-then-add" })
            yield return [consumer, shape, operation];
    }

    [Theory]
    [MemberData(nameof(EmptyViews))]
    public void EmptyClearAndPrimaryRestorationPreserveTheOldEmptyEnumeration(string consumer, string shape, string operation)
    {
        var owner = shape == "fresh" ? new ListViewItem() : new ListViewItem("primary");
        var items = new SubItems(owner);
        var second = new SubItems(owner);
        if (shape == "last-removed") items.RemoveAt(0);
        else if (shape == "cleared-external") items.Clear();
        else if (shape == "cleared-property") owner.SubItems.Clear();
        IEnumerator enumerator = GetEnumerator(items, consumer);
        Assert.Empty(items);

        if (operation == "clear") items.Clear();
        else if (operation == "clear-other") second.Clear();
        else
        {
            if (operation == "clear-restore") items.Clear();
            var restored = owner.SubItems;
            Assert.Single(restored);
            Assert.Same(restored[0], items[0]);
            Assert.Same(restored[0], second[0]);
            if (operation == "restore-then-add") restored.Add("new detail");
        }

        Assert.False(enumerator.MoveNext());
        Assert.Throws<InvalidOperationException>(() => _ = enumerator.Current);
        enumerator.Reset();
        Assert.False(enumerator.MoveNext());
        Assert.Equal(operation is "clear" or "clear-other" ? 0 : operation == "restore-then-add" ? 2 : 1, items.Count);
        Assert.Equal(items.Count, second.Count);
    }

    [Theory]
    [InlineData("public")]
    [InlineData("interface")]
    [InlineData("generic")]
    public void AddToTheSameEmptyDataInvalidatesTheEnumerator(string consumer)
    {
        var owner = new ListViewItem();
        var items = new SubItems(owner);
        IEnumerator enumerator = GetEnumerator(items, consumer);
        items.Add("new first");
        Assert.Throws<InvalidOperationException>(() => enumerator.MoveNext());
        Assert.Throws<InvalidOperationException>(() => enumerator.Reset());
        Assert.Throws<InvalidOperationException>(() => _ = enumerator.Current);
    }

    [Fact]
    public void GenericConsumptionKeepsTypedCurrentAndTheSameSubitemReferences()
    {
        var owner = new ListViewItem(new[] { "first", "second" });
        var items = owner.SubItems;
        using IEnumerator<SubItem> enumerator = ((IEnumerable<SubItem>)items).GetEnumerator();
        Assert.Null(enumerator.Current);
        Assert.True(enumerator.MoveNext());
        Assert.Same(items[0], enumerator.Current);
        Assert.True(enumerator.MoveNext());
        Assert.Same(items[1], enumerator.Current);
        Assert.False(enumerator.MoveNext());
        Assert.Null(enumerator.Current);
        ((IEnumerator)enumerator).Reset();
        Assert.Null(enumerator.Current);
        Assert.True(enumerator.MoveNext());
        Assert.Same(items[0], enumerator.Current);
    }

    private static readonly string[] Consumers = ["public", "interface", "generic"];
    private static IEnumerator GetEnumerator(SubItems items, string consumer) => consumer switch
    {
        "public" => items.GetEnumerator(),
        "interface" => ((IEnumerable)items).GetEnumerator(),
        _ => ((IEnumerable<SubItem>)items).GetEnumerator()
    };

    private static void AssertCachedCurrent(IEnumerator enumerator, string stage, SubItem original)
    {
        if (stage == "middle") Assert.Same(original, enumerator.Current);
        else Assert.Throws<InvalidOperationException>(() => _ = enumerator.Current);
    }

    private static void Change(ListViewItem owner, SubItems items, string operation)
    {
        var replacement = new ThrowingEqualsSubItem("replacement");
        switch (operation)
        {
            case "replace-first": items[0] = replacement; break;
            case "replace-middle": items[1] = replacement; break;
            case "replace-last": items[2] = replacement; break;
            case "replace-same": items[0] = items[0]; break;
            case "add": items.Add(replacement); break;
            case "insert": items.Insert(1, replacement); break;
            case "remove-first": items.RemoveAt(0); break;
            case "remove-middle": items.RemoveAt(1); break;
            case "remove-last": items.RemoveAt(2); break;
            case "remove-reference": items.Remove(items[1]); break;
            case "clear": items.Clear(); break;
            case "clear-restore": items.Clear(); _ = owner.SubItems; break;
            default: throw new InvalidOperationException(operation);
        }
    }

    private static void ChangeWithoutStructure(ListViewItem owner, SubItems items, string operation)
    {
        switch (operation)
        {
            case "read": _ = owner.SubItems; _ = items.Count; _ = items.IndexOf(items[1]); break;
            case "edit-text": items[0].Text = "changed"; break;
            case "remove-missing": items.Remove(new ThrowingEqualsSubItem("missing")); break;
            case "remove-null": items.Remove(null); break;
            case "bad-set": Assert.Throws<ArgumentOutOfRangeException>(() => items[-1] = items[0]); break;
            case "bad-insert": Assert.Throws<ArgumentOutOfRangeException>(() => items.Insert(-1, items[0])); break;
            case "bad-remove": Assert.Throws<ArgumentOutOfRangeException>(() => items.RemoveAt(items.Count)); break;
            case "null-add": Assert.Throws<ArgumentNullException>(() => items.Add((SubItem)null!)); break;
            case "empty-range": items.AddRange(Array.Empty<SubItem>()); break;
            case "null-range-entries": items.AddRange(new SubItem?[] { null, null }); break;
            default: throw new InvalidOperationException(operation);
        }
    }

    private sealed class ThrowingEqualsSubItem(string text) : SubItem(null, text)
    {
        public override bool Equals(object? obj) => throw new InvalidOperationException("Enumeration must not invoke Equals.");
        public override int GetHashCode() => 1;
    }
}
