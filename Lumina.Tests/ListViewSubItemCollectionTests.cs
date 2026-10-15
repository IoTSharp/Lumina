using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public class ListViewSubItemCollectionTests
{
    [Fact]
    public void PublicConstructor_RejectsNullOwner()
    {
        Assert.Throws<ArgumentNullException>("owner", () => new ListViewItem.ListViewSubItemCollection(null!));
    }

    [Theory]
    [InlineData("default", 0, "")]
    [InlineData("null-text", 1, "")]
    [InlineData("text", 1, "primary")]
    [InlineData("null-array", 0, "")]
    [InlineData("empty-array", 0, "")]
    [InlineData("array", 2, "primary")]
    public void PublicConstructor_DoesNotCreatePrimaryOrReplacePropertyView(string scenario, int expectedCount, string expectedText)
    {
        var owner = scenario switch
        {
            "null-text" => new ListViewItem((string?)null),
            "text" => new ListViewItem("primary"),
            "null-array" => new ListViewItem((string[]?)null),
            "empty-array" => new ListViewItem(Array.Empty<string>()),
            "array" => new ListViewItem(new[] { "primary", "detail" }),
            _ => new ListViewItem()
        };

        var first = new ListViewItem.ListViewSubItemCollection(owner);
        var second = new ListViewItem.ListViewSubItemCollection(owner);

        Assert.NotSame(first, second);
        Assert.Equal(expectedCount, first.Count);
        Assert.Equal(expectedCount, second.Count);
        Assert.Equal(expectedText, owner.Text);
        Assert.Equal(expectedCount, first.Count);

        var property = owner.SubItems;

        Assert.NotSame(first, property);
        Assert.NotSame(second, property);
        Assert.Same(property, owner.SubItems);
        Assert.Equal(Math.Max(1, expectedCount), first.Count);
        Assert.Equal(property.Count, second.Count);
        Assert.Same(property[0], first[0]);
        Assert.Same(property[0], second[0]);
    }

    [Fact]
    public void ViewsShareAddedRemovedAndEditedSubItems()
    {
        var owner = new ListViewItem(new[] { "primary", "detail" });
        var property = owner.SubItems;
        var first = new ListViewItem.ListViewSubItemCollection(owner);
        var second = new ListViewItem.ListViewSubItemCollection(owner);

        var added = first.Add("added");

        Assert.Equal(3, property.Count);
        Assert.Same(added, second[2]);
        Assert.Same(added, property[2]);
        Assert.Contains(added, second);
        Assert.Equal(2, property.IndexOf(added));
        Assert.Equal(first.ToArray(), second.ToArray());

        second.RemoveAt(0);

        Assert.Equal("detail", owner.Text);
        Assert.Equal(2, first.Count);
        Assert.Equal(first.ToArray(), property.ToArray());
        owner.Text = "owner update";
        Assert.Equal("owner update", first[0].Text);
        Assert.Equal("owner update", second[0].Text);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void EmptyingAnyView_DefersSharedPrimaryRestorationUntilPropertyAccess(bool removeOnlyItem)
    {
        var owner = new ListViewItem("primary");
        var property = owner.SubItems;
        var original = property[0];
        var first = new ListViewItem.ListViewSubItemCollection(owner);
        var second = new ListViewItem.ListViewSubItemCollection(owner);

        if (removeOnlyItem)
        {
            first.RemoveAt(0);
        }
        else
        {
            first.Clear();
        }

        var third = new ListViewItem.ListViewSubItemCollection(owner);
        Assert.Empty(first);
        Assert.Empty(second);
        Assert.Empty(property);
        Assert.Empty(third);
        Assert.Equal(string.Empty, owner.Text);
        Assert.Throws<ArgumentOutOfRangeException>("index", () => first[0]);
        Assert.Empty(first);

        Assert.Same(property, owner.SubItems);
        Assert.NotSame(original, Assert.Single(property));
        Assert.Same(property[0], Assert.Single(first));
        Assert.Same(property[0], Assert.Single(second));
        Assert.Same(property[0], Assert.Single(third));
        Assert.Equal(string.Empty, first[0].Text);
    }

    [Fact]
    public void SettingTextAfterClear_RestoresPrimaryForAllViews()
    {
        var owner = new ListViewItem("primary");
        var first = new ListViewItem.ListViewSubItemCollection(owner);
        var second = new ListViewItem.ListViewSubItemCollection(owner);
        first.Clear();

        owner.Text = "restored";

        Assert.Equal("restored", owner.Text);
        Assert.Equal("restored", Assert.Single(first).Text);
        Assert.Same(first[0], Assert.Single(second));
        Assert.Same(first[0], Assert.Single(owner.SubItems));
    }

    [Fact]
    public void AddingBeforePrimaryRestoration_PreservesAddedObjectAcrossViews()
    {
        var owner = new ListViewItem("primary");
        var property = owner.SubItems;
        var first = new ListViewItem.ListViewSubItemCollection(owner);
        var second = new ListViewItem.ListViewSubItemCollection(owner);
        property.Clear();

        var added = first.Add("manual primary");

        Assert.Equal("manual primary", owner.Text);
        Assert.Same(added, Assert.Single(first));
        Assert.Same(added, Assert.Single(second));
        Assert.Same(added, Assert.Single(owner.SubItems));
    }

    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(-1)]
    [InlineData(2)]
    [InlineData(int.MaxValue)]
    public void InvalidIntegerIndex_IsValidatedBeforeNullValueAndLeavesDataUnchanged(int index)
    {
        var owner = new ListViewItem(new[] { "primary", "detail" });
        var items = new ListViewItem.ListViewSubItemCollection(owner);
        var before = items.ToArray();

        Assert.Throws<ArgumentOutOfRangeException>("index", () => items[index]);
        Assert.Throws<ArgumentOutOfRangeException>("index", () => items[index] = null!);
        Assert.Throws<ArgumentOutOfRangeException>("index", () => items[index] = new ListViewItem.ListViewSubItem(null, "replacement"));

        Assert.Equal(before, items.ToArray());
        Assert.Equal("primary", owner.Text);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    public void IntegerIndexOnUninitializedOwner_DoesNotRestorePrimary(int index)
    {
        var owner = new ListViewItem();
        var items = new ListViewItem.ListViewSubItemCollection(owner);

        Assert.Throws<ArgumentOutOfRangeException>("index", () => items[index]);
        Assert.Throws<ArgumentOutOfRangeException>("index", () => items[index] = null!);
        Assert.Throws<ArgumentOutOfRangeException>("index", () => items[index] = new ListViewItem.ListViewSubItem());

        Assert.Empty(items);
        Assert.Equal(string.Empty, owner.Text);
        Assert.Empty(items);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void ValidIntegerIndex_RejectsNullWithoutChangingAnyView(int index)
    {
        var owner = new ListViewItem(new[] { "primary", "detail" });
        var first = new ListViewItem.ListViewSubItemCollection(owner);
        var second = new ListViewItem.ListViewSubItemCollection(owner);
        var before = first.ToArray();

        Assert.Throws<ArgumentNullException>("value", () => first[index] = null!);

        Assert.Equal(before, first.ToArray());
        Assert.Equal(before, second.ToArray());
        Assert.Equal("primary", owner.Text);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void ReplacingIntegerIndex_PreservesObjectTextAndTagIdentityAcrossViews(int index)
    {
        var owner = new ListViewItem(new[] { "primary", "detail" });
        var property = owner.SubItems;
        var first = new ListViewItem.ListViewSubItemCollection(owner);
        var second = new ListViewItem.ListViewSubItemCollection(owner);
        var original = first[index];
        string text = new string("replacement identity".ToCharArray());
        object tag = new();
        var replacement = new ListViewItem.ListViewSubItem(null, text) { Tag = tag };

        first[index] = replacement;

        Assert.Same(replacement, first[index]);
        Assert.Same(replacement, second[index]);
        Assert.Same(replacement, property[index]);
        Assert.Same(text, second[index].Text);
        Assert.Same(tag, second[index].Tag);
        Assert.DoesNotContain(original, second);
        Assert.Equal(index == 0 ? text : "primary", owner.Text);
        Assert.Equal(2, property.Count);
    }

    [Fact]
    public void IntegerSetter_PermitsSameSubItemAtMultiplePositions()
    {
        var owner = new ListViewItem(new[] { "primary", "detail" });
        var first = new ListViewItem.ListViewSubItemCollection(owner);
        var second = new ListViewItem.ListViewSubItemCollection(owner);
        var replacement = new ListViewItem.ListViewSubItem(null, "replacement");

        first[0] = replacement;
        second[1] = replacement;
        first[0] = first[0];
        replacement.Text = "shared object";

        Assert.Same(first[0], first[1]);
        Assert.Same(replacement, second[0]);
        Assert.Same(replacement, second[1]);
        Assert.Equal(0, second.IndexOf(replacement));
        Assert.Equal("shared object", owner.Text);
        Assert.Equal(new[] { "shared object", "shared object" }, owner.SubItems.Select(item => item.Text));
    }

    [Fact]
    public void ViewsMaintainIndependentKeyCachesOverSharedData()
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
        Assert.Equal(2, first.IndexOfKey("target"));
        Assert.Equal(-1, first.IndexOfKey(null));
        Assert.Equal(-1, second.IndexOfKey(""));
        Assert.Equal(2, first.IndexOfKey("target"));
        Assert.Equal(1, second.IndexOfKey("target"));

        var replacement = new ListViewItem.ListViewSubItem(null, "replacement") { Name = "target" };
        property[1] = replacement;

        Assert.Same(first[2], first["target"]);
        Assert.Same(replacement, second["target"]);
        Assert.Same(replacement, property["target"]);
        Assert.Equal(-1, second.IndexOfKey("missing"));
        Assert.Equal(2, first.IndexOfKey("target"));
        Assert.Equal(1, second.IndexOfKey("target"));
    }

    [Fact]
    public void SharedClearAndRepopulation_PreserveEachViewsPreviousKeyPosition()
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

        property.Clear();
        foreach (string text in new[] { "new primary", "new earlier", "new later" })
        {
            first.Add(new ListViewItem.ListViewSubItem(null, text) { Name = "target" });
        }

        Assert.Equal("new later", first["target"]!.Text);
        Assert.Equal("new earlier", second["target"]!.Text);
        Assert.Equal("new earlier", property["target"]!.Text);
        Assert.Equal("new primary", new ListViewItem.ListViewSubItemCollection(owner)["target"]!.Text);
    }

    [Fact]
    public void DerivedCollection_CanUsePublicOwnerConstructor()
    {
        var owner = new ListViewItem("primary");
        var items = new DerivedCollection(owner);

        Assert.Same(owner.SubItems[0], Assert.Single(items));
    }

    private sealed class DerivedCollection(ListViewItem owner) : ListViewItem.ListViewSubItemCollection(owner)
    {
    }
}
