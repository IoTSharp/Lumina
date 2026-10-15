using System.Globalization;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public class ListViewSubItemKeyTests
{
    [Fact]
    public void KeyMembers_HaveWinFormsVirtualShapeAndReadOnlyStringIndexer()
    {
        var collectionType = typeof(ListViewItem.ListViewSubItemCollection);
        var indexer = collectionType.GetProperty("Item", new[] { typeof(string) })!;
        var methods = new[]
        {
            indexer.GetMethod,
            collectionType.GetMethod(nameof(ListViewItem.ListViewSubItemCollection.ContainsKey)),
            collectionType.GetMethod(nameof(ListViewItem.ListViewSubItemCollection.IndexOfKey)),
            collectionType.GetMethod(nameof(ListViewItem.ListViewSubItemCollection.RemoveByKey))
        };

        Assert.False(collectionType.IsSealed);
        Assert.Null(indexer.SetMethod);
        Assert.All(methods, method =>
        {
            Assert.NotNull(method);
            Assert.True(method.IsVirtual);
            Assert.False(method.IsFinal);
            Assert.Equal("key", Assert.Single(method.GetParameters()).Name);
        });
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void NullOrEmptyKey_DoesNotFindUnnamedItemsOrRemoveThem(string? key)
    {
        var owner = new ListViewItem(new[] { "primary", "detail" });
        var items = owner.SubItems;
        var before = items.ToArray();

        Assert.Equal(-1, items.IndexOfKey(key));
        Assert.False(items.ContainsKey(key));
        Assert.Null(items[key]);
        items.RemoveByKey(key);

        Assert.Equal(before, items);
        Assert.Equal("primary", owner.Text);
    }

    [Theory]
    [InlineData("MIXED 中文", 1)]
    [InlineData("mixed 中文", 1)]
    [InlineData("detail text", -1)]
    [InlineData(" mixed 中文", -1)]
    [InlineData("mixed 中文 ", -1)]
    public void Lookup_UsesNameIgnoringCaseWithoutTrimming(string key, int expected)
    {
        var items = new ListViewItem(new[] { "primary", "detail text" }).SubItems;
        items[1].Name = "MiXeD 中文";

        Assert.Equal(expected, items.IndexOfKey(key));
        Assert.Equal(expected >= 0, items.ContainsKey(key));
        Assert.Same(expected < 0 ? null : items[expected], items[key]);
    }

    [Fact]
    public void WhitespaceKey_IsAValidName()
    {
        var items = new ListViewItem("primary").SubItems;
        items[0].Name = " ";

        Assert.Equal(0, items.IndexOfKey(" "));
        Assert.True(items.ContainsKey(" "));
        Assert.Same(items[0], items[" "]);
    }

    [Fact]
    public void KeyLookup_PreservesItemAndNameTextTagIdentity()
    {
        var owner = new ListViewItem("primary");
        var items = owner.SubItems;
        var primary = items[0];
        string name = new string("MiXeD 中文".ToCharArray());
        string text = new string("detail text".ToCharArray());
        object tag = new();
        var detail = items.Add(text);
        detail.Name = name;
        detail.Tag = tag;

        var found = items["mixed 中文"];

        Assert.Same(detail, found);
        Assert.Same(name, found!.Name);
        Assert.Same(text, found.Text);
        Assert.Same(tag, found.Tag);
        Assert.Same(primary, items[0]);
        Assert.Equal("primary", owner.Text);
        Assert.Equal(2, items.Count);
    }

    [Theory]
    [InlineData("en-US")]
    [InlineData("tr-TR")]
    public void KeyComparison_IsIndependentOfCurrentCulture(string culture)
    {
        CultureInfo original = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(culture);
            var items = new ListViewItem(new[] { "primary", "latin", "dotted", "accent", "sharp-s" }).SubItems;
            items[1].Name = "FILE";
            items[2].Name = "İ";
            items[3].Name = "é";
            items[4].Name = "ß";

            Assert.Equal(1, items.IndexOfKey("file"));
            Assert.Equal(-1, items.IndexOfKey("fıle"));
            Assert.Equal(-1, items.IndexOfKey("i"));
            Assert.Equal(3, items.IndexOfKey("É"));
            Assert.Equal(-1, items.IndexOfKey("e\u0301"));
            Assert.Equal(-1, items.IndexOfKey("ss"));
        }
        finally
        {
            CultureInfo.CurrentCulture = original;
        }
    }

    [Theory]
    [InlineData("a\u00ad", "\u00ada", true)]
    [InlineData("a\0b", "ab\0", true)]
    [InlineData("\u212b", "\u00c5", true)]
    [InlineData("\u212a", "k", true)]
    [InlineData("\u017f", "s", false)]
    [InlineData("\u00c6\0", "AE", false)]
    public void KeyComparison_PreservesWinFormsInvariantLinguisticBehavior(string name, string key, bool matches)
    {
        CultureInfo original = CultureInfo.CurrentCulture;
        try
        {
            foreach (string culture in new[] { "en-US", "tr-TR" })
            {
                CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(culture);
                var items = new ListViewItem("primary").SubItems;
                var primary = items[0];
                primary.Name = name;

                Assert.Equal(matches ? 0 : -1, items.IndexOfKey(key));
                Assert.Equal(matches, items.ContainsKey(key));
                Assert.Same(matches ? primary : null, items[key]);

                items.RemoveByKey(key);

                if (matches)
                {
                    Assert.Empty(items);
                }
                else
                {
                    Assert.Single(items);
                    Assert.Same(primary, items[0]);
                }
            }
        }
        finally
        {
            CultureInfo.CurrentCulture = original;
        }
    }

    [Fact]
    public void DuplicateNames_InitiallyFindFirstAndFollowNameChanges()
    {
        var items = new ListViewItem(new[] { "primary", "first", "second" }).SubItems;
        var first = items[1];
        var second = items[2];
        first.Name = "duplicate";
        second.Name = "DUPLICATE";

        Assert.Equal(1, items.IndexOfKey("DuPlIcAtE"));
        Assert.Same(first, items["duplicate"]);

        first.Name = "renamed";

        Assert.Same(second, items["duplicate"]);
        Assert.Same(first, items["RENAMED"]);
    }

    [Fact]
    public void NameChangeToEarlierDuplicate_PreservesCachedMatchUntilLookupMisses()
    {
        var items = new ListViewItem(new[] { "primary", "earlier", "later" }).SubItems;
        var earlier = items[1];
        var later = items[2];
        later.Name = "duplicate";
        Assert.Equal(2, items.IndexOfKey("duplicate"));

        earlier.Name = "DUPLICATE";

        Assert.Equal(2, items.IndexOfKey("duplicate"));
        Assert.True(items.ContainsKey("duplicate"));
        Assert.Same(later, items["duplicate"]);
        Assert.Equal(-1, items.IndexOfKey("missing"));
        Assert.Same(earlier, items["duplicate"]);
    }

    [Theory]
    [InlineData("index", null)]
    [InlineData("index", "")]
    [InlineData("contains", null)]
    [InlineData("contains", "")]
    [InlineData("item", null)]
    [InlineData("item", "")]
    [InlineData("remove", null)]
    [InlineData("remove", "")]
    public void NullOrEmptyKey_PreservesPreviouslyCachedDuplicate(string operation, string? key)
    {
        var items = new ListViewItem(new[] { "primary", "earlier", "later" }).SubItems;
        var later = items[2];
        later.Name = "target";
        Assert.Equal(2, items.IndexOfKey("target"));
        items[1].Name = "target";

        switch (operation)
        {
            case "index":
                Assert.Equal(-1, items.IndexOfKey(key));
                break;
            case "contains":
                Assert.False(items.ContainsKey(key));
                break;
            case "item":
                Assert.Null(items[key]);
                break;
            case "remove":
                items.RemoveByKey(key);
                break;
        }

        Assert.Equal(2, items.IndexOfKey("target"));
        Assert.Same(later, items["target"]);
        Assert.Equal(3, items.Count);
    }

    [Fact]
    public void LookupOfAnotherName_ChangesWhichDuplicateIsCached()
    {
        var items = new ListViewItem(new[] { "primary", "earlier", "later" }).SubItems;
        items[1].Name = "other";
        items[2].Name = "target";
        Assert.Equal(2, items.IndexOfKey("target"));
        Assert.Equal(1, items.IndexOfKey("other"));

        items[1].Name = "target";

        Assert.Equal(1, items.IndexOfKey("target"));
    }

    [Fact]
    public void ChangingCachedName_RescansForAnotherMatch()
    {
        var items = new ListViewItem(new[] { "primary", "earlier", "later" }).SubItems;
        items[2].Name = "target";
        Assert.Equal(2, items.IndexOfKey("target"));
        items[1].Name = "target";

        items[2].Name = null;

        Assert.Equal(1, items.IndexOfKey("target"));
        Assert.Same(items[1], items["TARGET"]);
    }

    [Fact]
    public void InsertAndRemovalBeforeCachedSlot_KeepWinFormsPositionSemantics()
    {
        var owner = new ListViewItem(new[] { "primary", "first", "second", "third" });
        var items = owner.SubItems;
        var second = items[2];
        var third = items[3];
        second.Name = "target";
        third.Name = "target";
        Assert.Equal(2, items.IndexOfKey("target"));
        var inserted = new ListViewItem.ListViewSubItem(owner, "inserted") { Name = "target" };

        items.Insert(1, inserted);

        Assert.Equal(1, items.IndexOfKey("target"));
        Assert.Same(inserted, items["target"]);
        items.RemoveAt(1);
        Assert.Equal(2, items.IndexOfKey("target"));
        items[1].Name = "target";
        items.RemoveAt(1);

        Assert.Equal(2, items.IndexOfKey("target"));
        Assert.Same(third, items["target"]);
        items.RemoveAt(2);
        Assert.Equal(1, items.IndexOfKey("target"));
        Assert.Same(second, items["target"]);
    }

    [Fact]
    public void ReplacingCachedSlot_UsesNewMatchingObject()
    {
        var owner = new ListViewItem(new[] { "primary", "earlier", "later" });
        var items = owner.SubItems;
        items[2].Name = "target";
        Assert.Equal(2, items.IndexOfKey("target"));
        items[1].Name = "target";
        var replacement = new ListViewItem.ListViewSubItem(owner, "replacement") { Name = "target" };

        items[2] = replacement;

        Assert.Equal(2, items.IndexOfKey("target"));
        Assert.Same(replacement, items["target"]);
    }

    [Fact]
    public void RemoveByKey_RemovesOneMatchAndLeavesUnrelatedObjects()
    {
        var owner = new ListViewItem(new[] { "primary", "first", "second", "other" });
        var items = owner.SubItems;
        var primary = items[0];
        var first = items[1];
        var second = items[2];
        var other = items[3];
        first.Name = "duplicate";
        second.Name = "DUPLICATE";

        items.RemoveByKey("DuPlIcAtE");

        Assert.Equal(new[] { primary, second, other }, items);
        Assert.DoesNotContain(first, items);
        Assert.Equal("primary", owner.Text);
        items.RemoveByKey("duplicate");
        Assert.Equal(new[] { primary, other }, items);
        items.RemoveByKey("duplicate");
        Assert.Equal(new[] { primary, other }, items);
    }

    [Fact]
    public void RemoveByKey_UsesCachedLaterDuplicate()
    {
        var items = new ListViewItem(new[] { "primary", "earlier", "later" }).SubItems;
        var earlier = items[1];
        var later = items[2];
        later.Name = "target";
        Assert.Same(later, items["target"]);
        earlier.Name = "target";

        items.RemoveByKey("target");

        Assert.Contains(earlier, items);
        Assert.DoesNotContain(later, items);
        Assert.Equal(2, items.Count);
    }

    [Fact]
    public void RemoveByKey_CanRemovePrimaryAndPromoteFollowingText()
    {
        var owner = new ListViewItem(new[] { "primary", "detail" });
        var items = owner.SubItems;
        var detail = items[1];
        items[0].Name = "primary";

        items.RemoveByKey("PRIMARY");

        Assert.Single(items);
        Assert.Same(detail, items[0]);
        Assert.Equal("detail", owner.Text);
    }

    [Fact]
    public void RemoveOnlyItem_DefersPrimaryRestorationUntilSubItemsGetter()
    {
        var owner = new ListViewItem("only");
        var items = owner.SubItems;
        var original = items[0];
        original.Name = "only-key";

        items.RemoveByKey("ONLY-KEY");

        Assert.Empty(items);
        Assert.Equal(string.Empty, owner.Text);
        Assert.Equal(-1, items.IndexOfKey("only-key"));
        Assert.False(items.ContainsKey("only-key"));
        Assert.Null(items["only-key"]);
        Assert.Empty(items);
        Assert.Same(items, owner.SubItems);
        Assert.Single(items);
        Assert.NotSame(original, items[0]);
        Assert.Equal(string.Empty, items[0].Text);
        Assert.Equal(string.Empty, items[0].Name);
    }

    [Fact]
    public void KeyOperationsAfterClear_DoNotRestorePrimary()
    {
        var owner = new ListViewItem("primary");
        var items = owner.SubItems;
        var original = items[0];
        original.Name = "key";
        Assert.Equal(0, items.IndexOfKey("key"));

        items.Clear();

        Assert.Equal(string.Empty, owner.Text);
        Assert.Equal(-1, items.IndexOfKey("key"));
        Assert.False(items.ContainsKey("key"));
        Assert.Null(items["key"]);
        items.RemoveByKey("key");
        Assert.Empty(items);
        Assert.Same(items, owner.SubItems);
        Assert.Single(items);
        Assert.NotSame(original, items[0]);
        Assert.Equal(string.Empty, items[0].Text);
        Assert.Equal(string.Empty, items[0].Name);
    }

    [Fact]
    public void ClearAndRepopulateBeforeLookup_PreservesCachedPosition()
    {
        var owner = new ListViewItem(new[] { "primary", "original" });
        var items = owner.SubItems;
        items[1].Name = "target";
        Assert.Equal(1, items.IndexOfKey("target"));

        items.Clear();
        var first = new ListViewItem.ListViewSubItem(owner, "first") { Name = "target" };
        var second = new ListViewItem.ListViewSubItem(owner, "second") { Name = "target" };
        items.Add(first);
        items.Add(second);

        Assert.Equal(1, items.IndexOfKey("target"));
        Assert.Same(second, items["target"]);
        Assert.Equal(-1, items.IndexOfKey("missing"));
        Assert.Same(first, items["target"]);
    }
}
