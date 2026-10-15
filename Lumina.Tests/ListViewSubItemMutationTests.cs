using System.Drawing;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public class ListViewSubItemMutationTests
{
    [Fact]
    public void TypedMutationMembers_MatchVoidSignaturesAndInterfaceMetadata()
    {
        Type collection = typeof(ListViewItem.ListViewSubItemCollection);
        foreach ((string name, Type[] parameters, string[] names, bool interfaceSlot) in new[]
        {
            ("Insert", new[] { typeof(int), typeof(ListViewItem.ListViewSubItem) }, new[] { "index", "item" }, false),
            ("Remove", new[] { typeof(ListViewItem.ListViewSubItem) }, new[] { "item" }, false),
            ("RemoveAt", new[] { typeof(int) }, new[] { "index" }, true),
            ("Clear", Type.EmptyTypes, Array.Empty<string>(), true)
        })
        {
            var method = collection.GetMethod(name, parameters);

            Assert.NotNull(method);
            Assert.True(method.IsPublic);
            Assert.Equal(typeof(void), method.ReturnType);
            Assert.Equal(names, method.GetParameters().Select(parameter => parameter.Name));
            Assert.Equal(interfaceSlot, method.IsVirtual);
            Assert.Equal(interfaceSlot, method.IsFinal);
            Assert.True(method.IsHideBySig);
        }
    }

    public static IEnumerable<object[]> InvalidInsertPositions()
    {
        foreach (string shape in Shapes)
        {
            int count = shape == "populated" ? 3 : 0;
            foreach (int index in new[] { int.MinValue, -1, count + 1, int.MaxValue }.Distinct())
            {
                yield return [shape, index];
            }
        }
    }

    [Theory]
    [MemberData(nameof(InvalidInsertPositions))]
    public void Insert_ValidatesIndexBeforeNullAndLeavesEveryViewUnchanged(string shape, int index)
    {
        var views = Create(shape);
        var before = views.First.ToArray();
        var candidate = new ListViewItem.ListViewSubItem(null, "candidate");

        Assert.Throws<ArgumentOutOfRangeException>("index", () => views.First.Insert(index, null!));
        Assert.Throws<ArgumentOutOfRangeException>("index", () => views.First.Insert(index, candidate));

        Assert.Equal(before, views.First);
        Assert.Equal(before, views.Second);
        Assert.Equal(shape == "populated" ? "primary" : string.Empty, views.Owner.Text);
        Assert.Equal(before.Length, views.First.Count);
    }

    [Theory]
    [InlineData("fresh", 0)]
    [InlineData("populated", 0)]
    [InlineData("populated", 1)]
    [InlineData("populated", 2)]
    [InlineData("populated", 3)]
    [InlineData("cleared-external", 0)]
    [InlineData("cleared-property", 0)]
    public void Insert_RejectsNullAtEveryValidPositionWithoutCreatingPrimary(string shape, int index)
    {
        var views = Create(shape);
        var before = views.First.ToArray();

        Assert.Throws<ArgumentNullException>("item", () => views.First.Insert(index, null!));

        Assert.Equal(before, views.First);
        Assert.Equal(before, views.Second);
        Assert.Equal(shape == "populated" ? "primary" : string.Empty, views.Owner.Text);
        Assert.Equal(before.Length, views.First.Count);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void Insert_PreservesDerivedObjectTextTagStyleAndOriginalOrderAcrossViews(int index)
    {
        using var font = CreateFont();
        var owner = new ListViewItem(new[] { "primary", "detail" });
        var property = owner.SubItems;
        var first = new ListViewItem.ListViewSubItemCollection(owner);
        var second = new ListViewItem.ListViewSubItemCollection(owner);
        var original = first.ToArray();
        string text = new string("identity text".ToCharArray());
        object tag = new();
        var item = new DerivedSubItem(null, text, Color.Crimson, Color.AliceBlue, font)
        {
            Name = "identity key", Tag = tag
        };

        first.Insert(index, item);

        Assert.Same(item, first[index]);
        Assert.Same(item, second[index]);
        Assert.Same(item, property[index]);
        Assert.Same(text, first[index].Text);
        Assert.Same(tag, first[index].Tag);
        Assert.Equal("identity key", first[index].Name);
        Assert.Equal(Color.Crimson, first[index].ForeColor);
        Assert.Equal(Color.AliceBlue, first[index].BackColor);
        Assert.Same(font, first[index].Font);
        Assert.Equal(index == 0 ? text : "primary", owner.Text);
        for (int originalIndex = 0; originalIndex < original.Length; originalIndex++)
        {
            Assert.Same(original[originalIndex], first[originalIndex < index ? originalIndex : originalIndex + 1]);
        }

        item.Text = "changed";
        Assert.Equal("changed", second[index].Text);
        Assert.Equal(index == 0 ? "changed" : "primary", owner.Text);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 0)]
    [InlineData(2, 1)]
    public void Insert_AllowsAnExistingObjectAtAnotherPositionWithoutCopying(int index, int originalIndex)
    {
        var owner = new ListViewItem(new[] { "primary", "detail" });
        var items = owner.SubItems;
        var item = items[originalIndex];

        items.Insert(index, item);

        Assert.Same(item, items[index]);
        Assert.Equal(2, items.Count(candidate => ReferenceEquals(item, candidate)));
        item.Text = "changed";
        Assert.Equal(2, items.Count(candidate => candidate.Text == "changed"));
        Assert.Equal(index == 0 || originalIndex == 0 ? "changed" : "primary", owner.Text);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    [InlineData(1, 1)]
    public void Insert_FromAnotherOwnerKeepsSourcePositionAndSharesObject(int targetIndex, int sourceIndex)
    {
        var source = new ListViewItem(new[] { "source primary", "source detail" });
        var target = new ListViewItem("target primary");
        var sourceItems = source.SubItems;
        var before = sourceItems.ToArray();
        var candidate = sourceItems[sourceIndex];

        target.SubItems.Insert(targetIndex, candidate);

        Assert.Same(candidate, target.SubItems[targetIndex]);
        Assert.Equal(before, sourceItems);
        candidate.Text = "shared changed";
        Assert.Equal("shared changed", sourceItems[sourceIndex].Text);
        Assert.Equal(targetIndex == 0 ? "shared changed" : "target primary", target.Text);
        Assert.Equal(sourceIndex == 0 ? "shared changed" : "source primary", source.Text);

        sourceItems.Remove(candidate);

        Assert.Same(candidate, target.SubItems[targetIndex]);
        Assert.Equal("shared changed", target.SubItems[targetIndex].Text);
    }

    [Fact]
    public void Insert_EmptyTextObjectBecomesPrimaryWithoutAddingAnotherSubItem()
    {
        var owner = new ListViewItem();
        var first = new ListViewItem.ListViewSubItemCollection(owner);
        var second = new ListViewItem.ListViewSubItemCollection(owner);
        var candidate = new ListViewItem.ListViewSubItem(null, null);

        first.Insert(0, candidate);

        Assert.Equal(string.Empty, owner.Text);
        Assert.Same(candidate, Assert.Single(first));
        Assert.Same(candidate, Assert.Single(second));
        Assert.Same(candidate, Assert.Single(owner.SubItems));
    }

    [Theory]
    [InlineData("fresh")]
    [InlineData("populated")]
    [InlineData("cleared-external")]
    [InlineData("cleared-property")]
    public void Remove_NullAndMissingObjectsDoNotThrowChangeDataOrRestorePrimary(string shape)
    {
        var views = Create(shape);
        var before = views.First.ToArray();
        var otherOwner = new ListViewItem(views.Owner.Text);
        var sameText = new ListViewItem.ListViewSubItem(null, views.Owner.Text);

        views.First.Remove(null);
        views.First.Remove(new ListViewItem.ListViewSubItem(null, "missing"));
        views.First.Remove(sameText);
        views.First.Remove(otherOwner.SubItems[0]);

        Assert.Equal(before, views.First);
        Assert.Equal(before, views.Second);
        Assert.Equal(shape == "populated" ? "primary" : string.Empty, views.Owner.Text);
        Assert.Equal(before.Length, views.First.Count);
        Assert.Single(otherOwner.SubItems);
    }

    [Fact]
    public void Remove_RepeatedObjectRemovesOnlyItsFirstPosition()
    {
        var owner = new ListViewItem("primary");
        var items = owner.SubItems;
        var primary = items[0];
        var repeated = new ListViewItem.ListViewSubItem(null, "repeated");
        var middle = new ListViewItem.ListViewSubItem(null, "middle");
        items.AddRange([repeated, middle, repeated]);

        items.Remove(repeated);

        Assert.Equal(new[] { primary, middle, repeated }, items);
        Assert.Same(repeated, items[2]);

        items.Remove(repeated);
        items.Remove(repeated);

        Assert.Equal(new[] { primary, middle }, items);
        Assert.Equal("primary", owner.Text);
    }

    [Fact]
    public void Remove_UsesReferenceIdentityEvenWhenDerivedSubItemsOverrideEquals()
    {
        var owner = new ListViewItem("primary");
        var items = owner.SubItems;
        var first = new EqualSubItem("first");
        var second = new EqualSubItem("second");
        items.AddRange([first, second]);

        items.Remove(new EqualSubItem("missing"));

        Assert.Equal(3, items.Count);
        Assert.Same(first, items[1]);
        Assert.Same(second, items[2]);

        items.Remove(second);

        Assert.Equal(2, items.Count);
        Assert.Same(first, items[1]);
        Assert.Equal("first", items[1].Text);
    }

    public static IEnumerable<object[]> InvalidRemoveAtPositions()
    {
        foreach (string shape in Shapes)
        {
            int count = shape == "populated" ? 3 : 0;
            foreach (int index in new[] { int.MinValue, -1, count, count + 1, int.MaxValue }.Distinct())
            {
                yield return [shape, index];
            }
        }
    }

    [Theory]
    [MemberData(nameof(InvalidRemoveAtPositions))]
    public void RemoveAt_InvalidPositionLeavesAllViewsUnchangedWithoutRestoringPrimary(string shape, int index)
    {
        var views = Create(shape);
        var before = views.First.ToArray();

        Assert.Throws<ArgumentOutOfRangeException>("index", () => views.First.RemoveAt(index));

        Assert.Equal(before, views.First);
        Assert.Equal(before, views.Second);
        Assert.Equal(shape == "populated" ? "primary" : string.Empty, views.Owner.Text);
        Assert.Equal(before.Length, views.First.Count);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void RemoveAt_RemovesExactPositionAndPreservesRemainingOrderAndSharedPrimary(int index)
    {
        var views = Create("populated");
        var property = views.Owner.SubItems;
        var before = views.First.ToArray();

        views.Second.RemoveAt(index);

        var expected = before.Where((_, originalIndex) => originalIndex != index).ToArray();
        Assert.Equal(expected, views.First);
        Assert.Equal(expected, property);
        Assert.Equal(index == 0 ? "detail" : "primary", views.Owner.Text);
        Assert.Equal(before[index].Text, index switch { 0 => "primary", 1 => "detail", _ => "last" });
    }

    [Theory]
    [InlineData("remove", "property")]
    [InlineData("remove", "text")]
    [InlineData("remove", "insert")]
    [InlineData("remove", "add")]
    [InlineData("remove", "add-range")]
    [InlineData("remove-at", "property")]
    [InlineData("remove-at", "text")]
    [InlineData("remove-at", "insert")]
    [InlineData("remove-at", "add")]
    [InlineData("remove-at", "add-range")]
    [InlineData("clear", "property")]
    [InlineData("clear", "text")]
    [InlineData("clear", "insert")]
    [InlineData("clear", "add")]
    [InlineData("clear", "add-range")]
    public void EmptyingCollection_DefersPrimaryRestorationUntilAnExplicitRecovery(string operation, string recovery)
    {
        var owner = new ListViewItem("primary");
        var property = owner.SubItems;
        var first = new ListViewItem.ListViewSubItemCollection(owner);
        var second = new ListViewItem.ListViewSubItemCollection(owner);
        var original = property[0];
        if (operation == "remove") first.Remove(original);
        else if (operation == "remove-at") first.RemoveAt(0);
        else first.Clear();

        Assert.Empty(first);
        Assert.Empty(second);
        Assert.Empty(property);
        Assert.Equal(string.Empty, owner.Text);
        Assert.Empty(first);
        var manual = new ListViewItem.ListViewSubItem(null, "manual primary");

        if (recovery == "property") _ = owner.SubItems;
        else if (recovery == "text") owner.Text = "restored";
        else if (recovery == "insert") second.Insert(0, manual);
        else if (recovery == "add") second.Add(manual);
        else second.AddRange([manual]);

        var primary = Assert.Single(first);
        Assert.NotSame(original, primary);
        Assert.Same(primary, Assert.Single(second));
        Assert.Same(primary, Assert.Single(property));
        Assert.Same(property, owner.SubItems);
        Assert.Equal("primary", original.Text);
        if (recovery == "property") Assert.Equal(string.Empty, primary.Text);
        else if (recovery == "text") Assert.Equal("restored", primary.Text);
        else Assert.Same(manual, primary);
        Assert.Equal(primary.Text, owner.Text);
    }

    [Theory]
    [InlineData("fresh")]
    [InlineData("populated")]
    [InlineData("cleared-external")]
    [InlineData("cleared-property")]
    public void Clear_CanBeRepeatedWithoutRestoringPrimaryOrChangingRemovedObjects(string shape)
    {
        var views = Create(shape);
        var original = views.First.ToArray();

        views.First.Clear();
        views.Second.Clear();

        Assert.Empty(views.First);
        Assert.Empty(views.Second);
        Assert.Equal(string.Empty, views.Owner.Text);
        Assert.Empty(views.First);
        Assert.Equal(shape == "populated" ? new[] { "primary", "detail", "last" } : Array.Empty<string>(), original.Select(item => item.Text));
    }

    [Theory]
    [InlineData("insert-first", 2, 1)]
    [InlineData("insert-middle", 2, 1)]
    [InlineData("insert-last", 2, 1)]
    [InlineData("insert-null", 2, 1)]
    [InlineData("insert-invalid", 2, 1)]
    [InlineData("remove-first", 0, 1)]
    [InlineData("remove-middle", 0, 1)]
    [InlineData("remove-last", 0, 1)]
    [InlineData("remove-missing", 2, 1)]
    [InlineData("remove-null", 2, 1)]
    [InlineData("remove-at-first", 0, 1)]
    [InlineData("remove-at-middle", 0, 1)]
    [InlineData("remove-at-last", 0, 1)]
    [InlineData("remove-at-invalid", 2, 1)]
    [InlineData("clear", -1, -1)]
    public void Mutations_PreserveIndependentKeyPositionsOverSharedData(string operation, int expectedFirst, int expectedOther)
    {
        var owner = new ListViewItem(new[] { "primary", "earlier", "later" });
        var property = owner.SubItems;
        var first = new ListViewItem.ListViewSubItemCollection(owner);
        var second = new ListViewItem.ListViewSubItemCollection(owner);
        first[2].Name = "target";
        Assert.Equal(2, first.IndexOfKey("target"));
        first[1].Name = "target";
        Assert.Equal(1, second.IndexOfKey("target"));
        Assert.Equal(1, property.IndexOfKey("target"));
        first[0].Name = "target";
        var candidate = new ListViewItem.ListViewSubItem(null, "inserted") { Name = "target" };

        switch (operation)
        {
            case "insert-first": first.Insert(0, candidate); break;
            case "insert-middle": first.Insert(1, candidate); break;
            case "insert-last": first.Insert(3, candidate); break;
            case "insert-null": Assert.Throws<ArgumentNullException>("item", () => first.Insert(1, null!)); break;
            case "insert-invalid": Assert.Throws<ArgumentOutOfRangeException>("index", () => first.Insert(4, candidate)); break;
            case "remove-first": first.Remove(first[0]); break;
            case "remove-middle": first.Remove(first[1]); break;
            case "remove-last": first.Remove(first[2]); break;
            case "remove-missing": first.Remove(candidate); break;
            case "remove-null": first.Remove(null); break;
            case "remove-at-first": first.RemoveAt(0); break;
            case "remove-at-middle": first.RemoveAt(1); break;
            case "remove-at-last": first.RemoveAt(2); break;
            case "remove-at-invalid": Assert.Throws<ArgumentOutOfRangeException>("index", () => first.RemoveAt(3)); break;
            case "clear": first.Clear(); break;
        }

        Assert.Equal(first.ToArray(), second);
        Assert.Equal(first.ToArray(), property);
        Assert.Equal(expectedFirst, first.IndexOfKey("target"));
        Assert.Equal(expectedOther, second.IndexOfKey("target"));
        Assert.Equal(expectedOther, property.IndexOfKey("target"));
        Assert.Equal(operation == "clear" ? -1 : 0, new ListViewItem.ListViewSubItemCollection(owner).IndexOfKey("target"));

        property.Clear();
        for (int index = 0; index < 4; index++)
        {
            second.Insert(index, new ListViewItem.ListViewSubItem(null, "replacement " + index) { Name = "target" });
        }

        Assert.Equal(Math.Max(0, expectedFirst), first.IndexOfKey("target"));
        Assert.Equal(Math.Max(0, expectedOther), second.IndexOfKey("target"));
        Assert.Equal(Math.Max(0, expectedOther), property.IndexOfKey("target"));
        Assert.Equal(0, new ListViewItem.ListViewSubItemCollection(owner).IndexOfKey("target"));
        Assert.Equal("replacement 0", owner.Text);
    }

    [Theory]
    [InlineData("insert")]
    [InlineData("remove")]
    [InlineData("remove-at")]
    [InlineData("clear")]
    [InlineData("failed-insert")]
    [InlineData("failed-remove-at")]
    [InlineData("remove-missing")]
    public void Mutations_PreserveBorrowedFontAndDetachedSubItemState(string operation)
    {
        using var font = CreateFont();
        float height = font.GetHeight(96f);
        var owner = new ListViewItem("primary");
        var items = owner.SubItems;
        object tag = new();
        var first = new ListViewItem.ListViewSubItem(null, "first", Color.Crimson, Color.AliceBlue, font) { Name = "key", Tag = tag };
        var second = new ListViewItem.ListViewSubItem(null, "second", Color.Green, Color.Yellow, font);
        items.AddRange([first, second]);

        switch (operation)
        {
            case "insert": items.Insert(0, first); break;
            case "remove": items.Remove(first); break;
            case "remove-at": items.RemoveAt(1); break;
            case "clear": items.Clear(); break;
            case "failed-insert": Assert.Throws<ArgumentOutOfRangeException>("index", () => items.Insert(-1, first)); break;
            case "failed-remove-at": Assert.Throws<ArgumentOutOfRangeException>("index", () => items.RemoveAt(3)); break;
            case "remove-missing": items.Remove(new ListViewItem.ListViewSubItem()); break;
        }

        Assert.Equal("first", first.Text);
        Assert.Equal("key", first.Name);
        Assert.Same(tag, first.Tag);
        Assert.Equal(Color.Crimson, first.ForeColor);
        Assert.Equal(Color.AliceBlue, first.BackColor);
        Assert.Same(font, first.Font);
        Assert.Same(font, second.Font);
        Assert.Equal(height, font.GetHeight(96f));
        Assert.Equal(height, first.Font.GetHeight(96f));
        Assert.Equal(height, second.Font.GetHeight(96f));
    }

    [Fact]
    public void LegacyGenericInsert_UsesTypedValidationAndSharedIdentity()
    {
        var owner = new ListViewItem();
        var first = new ListViewItem.ListViewSubItemCollection(owner);
        var second = new ListViewItem.ListViewSubItemCollection(owner);
        IList<ListViewItem.ListViewSubItem> generic = first;
        var item = new ListViewItem.ListViewSubItem(null, "primary");

        Assert.Throws<ArgumentOutOfRangeException>("index", () => generic.Insert(-1, null!));
        Assert.Throws<ArgumentNullException>("item", () => generic.Insert(0, null!));
        Assert.Empty(first);

        generic.Insert(0, item);
        generic.Insert(1, item);

        Assert.Equal(2, first.Count);
        Assert.Same(item, first[0]);
        Assert.Same(item, second[1]);
        Assert.Equal("primary", owner.Text);
    }

    [Fact]
    public void LegacyGenericRemove_RetainsBoolAndRemovesFirstExactObject()
    {
        var owner = new ListViewItem();
        var items = new ListViewItem.ListViewSubItemCollection(owner);
        ICollection<ListViewItem.ListViewSubItem> generic = items;
        var first = new EqualSubItem("first");
        var second = new EqualSubItem("second");
        items.AddRange([first, second, first]);

        Assert.False(generic.Remove(null!));
        Assert.False(generic.Remove(new EqualSubItem("missing")));
        Assert.True(generic.Remove(second));
        Assert.Equal(2, items.Count);
        Assert.Same(first, items[0]);
        Assert.Same(first, items[1]);
        Assert.True(generic.Remove(first));
        Assert.Same(first, Assert.Single(items));
        Assert.True(generic.Remove(first));
        Assert.False(generic.Remove(first));
        Assert.Empty(items);
        Assert.Equal(string.Empty, owner.Text);
        Assert.Empty(items);
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

    private static Font CreateFont() => new("Arial", 12.5f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);

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
