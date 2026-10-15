using System.Drawing;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public class ListViewSubItemAddRangeTests
{
    [Fact]
    public void TypedAddRangeOverloads_ExposeStandardVoidNonVirtualSignatures()
    {
        foreach (Type[] parameters in new[]
        {
            new[] { typeof(ListViewItem.ListViewSubItem[]) },
            new[] { typeof(string[]) },
            new[] { typeof(string[]), typeof(Color), typeof(Color), typeof(Font) }
        })
        {
            var method = typeof(ListViewItem.ListViewSubItemCollection).GetMethod("AddRange", parameters);

            Assert.NotNull(method);
            Assert.Equal(typeof(void), method.ReturnType);
            Assert.True(method.IsPublic);
            Assert.False(method.IsVirtual);
            Assert.Equal("items", method.GetParameters()[0].Name);
        }
    }

    public static IEnumerable<object[]> ValidationCases()
    {
        foreach (string mode in new[] { "object", "text", "style" })
        {
            foreach (string shape in new[] { "fresh", "initialized", "cleared-external", "cleared-property" })
            {
                foreach (string payload in new[] { "null-array", "empty", "only-null", "null-first", "null-middle", "null-last", "all-nulls", "valid" })
                {
                    yield return [mode, shape, payload];
                }
            }
        }
    }

    [Theory]
    [MemberData(nameof(ValidationCases))]
    public void AddRange_ValidatesArrayThenSkipsNullEntriesWithoutRestoringPrimary(string mode, string shape, string payload)
    {
        using var font = CreateFont();
        var owner = shape == "fresh" ? new ListViewItem() : new ListViewItem("original primary");
        var first = new ListViewItem.ListViewSubItemCollection(owner);
        var second = new ListViewItem.ListViewSubItemCollection(owner);
        ListViewItem.ListViewSubItemCollection? property = shape == "cleared-property" ? owner.SubItems : null;
        if (shape.StartsWith("cleared-", StringComparison.Ordinal))
        {
            first.Clear();
        }

        var original = first.ToArray();
        string?[]? input = payload switch
        {
            "null-array" => null,
            "empty" => [],
            "only-null" => [null],
            "null-first" => [null, "first", "second"],
            "null-middle" => ["first", null, "second"],
            "null-last" => ["first", "second", null],
            "all-nulls" => [null, null, null],
            _ => ["first", "second"]
        };

        if (payload == "null-array")
        {
            Assert.Throws<ArgumentNullException>("items", () => AddRange(first, mode, input, font));
        }
        else
        {
            AddRange(first, mode, input, font);
        }

        string[] added = payload is "null-first" or "null-middle" or "null-last" or "valid" ? ["first", "second"] : [];
        string[] expected = shape == "initialized" ? ["original primary", .. added] : added;
        Assert.Equal(expected, Texts(first));
        Assert.Equal(first.ToArray(), second.ToArray());
        Assert.Equal(expected.FirstOrDefault() ?? string.Empty, owner.Text);
        Assert.Equal(expected.Length, first.Count);
        for (int index = 0; index < original.Length; index++)
        {
            Assert.Same(original[index], first[index]);
        }

        var recovery = first.Add("recovery");

        Assert.Equal<string>([.. expected, "recovery"], Texts(second));
        Assert.Same(recovery, second[second.Count - 1]);
        Assert.Equal(expected.FirstOrDefault() ?? "recovery", owner.Text);
        Assert.Equal(first.ToArray(), owner.SubItems.ToArray());
        if (property is not null)
        {
            Assert.Same(property, owner.SubItems);
        }
    }

    [Fact]
    public void ObjectRange_KeepsNonNullObjectsWithNullOrEmptyTextAsPrimaryAndDetail()
    {
        var owner = new ListViewItem();
        var items = new ListViewItem.ListViewSubItemCollection(owner);
        var nullText = new ListViewItem.ListViewSubItem(null, null);
        var emptyText = new ListViewItem.ListViewSubItem(null, "");

        items.AddRange([null, nullText, emptyText, null]);

        Assert.Equal(new[] { nullText, emptyText }, items.ToArray());
        Assert.Equal(new[] { "", "" }, Texts(items));
        Assert.Equal(string.Empty, owner.Text);
        Assert.Equal(2, items.Count);
        Assert.Equal(2, owner.SubItems.Count);
        Assert.Same(nullText, owner.SubItems[0]);
        Assert.Same(emptyText, owner.SubItems[1]);
    }

    [Fact]
    public void ObjectRange_PreservesDerivedIdentityStyleDuplicatesAndExistingPrimary()
    {
        using var font = CreateFont();
        var owner = new ListViewItem("primary");
        var items = owner.SubItems;
        var primary = items[0];
        string text = new string("identity text".ToCharArray());
        object tag = new();
        var item = new DerivedSubItem(null, text, Color.Crimson, Color.AliceBlue, font) { Name = "key", Tag = tag };
        ListViewItem.ListViewSubItem?[] input = [null, item, primary, item, null];

        items.AddRange(input);

        Assert.Equal(new[] { primary, item, primary, item }, items.ToArray());
        Assert.Same(text, items[1].Text);
        Assert.Same(tag, items[1].Tag);
        Assert.Equal("key", items[1].Name);
        Assert.IsType<DerivedSubItem>(items[1]);
        Assert.Equal(Color.Crimson, items[1].ForeColor);
        Assert.Equal(Color.AliceBlue, items[1].BackColor);
        Assert.Same(font, items[1].Font);

        input[1] = new ListViewItem.ListViewSubItem(null, "array replacement");
        item.Text = "changed detail";
        primary.Text = "changed primary";

        Assert.Same(item, items[1]);
        Assert.Same(items[1], items[3]);
        Assert.Equal(new[] { "changed primary", "changed detail", "changed primary", "changed detail" }, Texts(items));
        Assert.Equal("changed primary", owner.Text);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void ObjectRange_FromOtherOwnerRetainsOriginalCollectionAndSharesMutations(int sourceIndex)
    {
        var source = new ListViewItem(new[] { "source primary", "source detail" });
        var sourceItems = source.SubItems;
        var sourceBefore = sourceItems.ToArray();
        var target = new ListViewItem("target primary");
        var items = target.SubItems;
        var borrowed = sourceItems[sourceIndex];
        var constructedForSource = new ListViewItem.ListViewSubItem(source, "constructed for source");

        items.AddRange([borrowed, constructedForSource, borrowed]);

        Assert.Equal(sourceBefore, sourceItems.ToArray());
        Assert.Equal(new[] { items[0], borrowed, constructedForSource, borrowed }, items.ToArray());
        Assert.DoesNotContain(constructedForSource, sourceItems);
        borrowed.Text = "shared changed";
        Assert.Equal("shared changed", sourceItems[sourceIndex].Text);
        Assert.Equal("shared changed", items[1].Text);
        Assert.Equal("shared changed", items[3].Text);
        Assert.Equal(sourceIndex == 0 ? "shared changed" : "source primary", source.Text);
        Assert.Equal("target primary", target.Text);

        sourceItems.Clear();

        Assert.Empty(sourceItems);
        Assert.Same(borrowed, items[1]);
        Assert.Same(borrowed, items[3]);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void TextRanges_SkipNullButKeepEmptyStringsAndCopyInputSlotsIntoDistinctObjects(bool styled)
    {
        using var font = CreateFont();
        var owner = new ListViewItem("primary");
        var items = owner.SubItems;
        string text = new string("identity text".ToCharArray());
        string?[] input = [null, "", " ", "detail 中文", text, text, null];

        if (styled)
        {
            items.AddRange(input, Color.Crimson, Color.AliceBlue, font);
        }
        else
        {
            items.AddRange(input);
        }

        Assert.Equal(new[] { "primary", "", " ", "detail 中文", text, text }, Texts(items));
        Assert.Same(text, items[4].Text);
        Assert.Same(text, items[5].Text);
        Assert.NotSame(items[4], items[5]);
        Assert.All(items.Skip(1), item =>
        {
            Assert.Equal(string.Empty, item.Name);
            Assert.Null(item.Tag);
            Assert.Equal(styled ? Color.Crimson : SystemColors.WindowText, item.ForeColor);
            Assert.Equal(styled ? Color.AliceBlue : SystemColors.Window, item.BackColor);
            Assert.Same(styled ? font : new ListViewItem.ListViewSubItem().Font, item.Font);
        });

        input[4] = "array replacement";
        items[5].Text = "last changed";

        Assert.Same(text, items[4].Text);
        Assert.Equal("last changed", items[5].Text);
        Assert.Equal("primary", owner.Text);
    }

    [Fact]
    public void StyledTextRange_UsesDefaultsForEmptyColorsAndNullFont()
    {
        var owner = new ListViewItem("primary");

        owner.SubItems.AddRange([null, "", "detail"], Color.Empty, Color.Empty, null);

        Assert.Equal(new[] { "primary", "", "detail" }, Texts(owner.SubItems));
        Assert.All(owner.SubItems.Skip(1), item =>
        {
            Assert.Equal(SystemColors.WindowText, item.ForeColor);
            Assert.Equal(SystemColors.Window, item.BackColor);
            Assert.Same(new ListViewItem.ListViewSubItem().Font, item.Font);
        });
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void StyledTextRange_PreservesExplicitAlphaAndTransparentColors(bool transparent)
    {
        using var font = CreateFont();
        var owner = new ListViewItem("primary");
        Color fore = transparent ? Color.Transparent : Color.FromArgb(123, 11, 22, 33);
        Color back = transparent ? Color.FromArgb(0, 7, 8, 9) : Color.FromArgb(67, 44, 55, 66);

        owner.SubItems.AddRange(["first", "second"], fore, back, font);

        Assert.All(owner.SubItems.Skip(1), item =>
        {
            Assert.Equal(fore, item.ForeColor);
            Assert.Equal(back, item.BackColor);
            Assert.Same(font, item.Font);
        });
    }

    [Theory]
    [InlineData("object", false)]
    [InlineData("text", false)]
    [InlineData("style", false)]
    [InlineData("object", true)]
    [InlineData("text", true)]
    [InlineData("style", true)]
    public void AddedFirstEntryAndPrimaryTextAreSharedAcrossExternalAndPropertyViews(string mode, bool cleared)
    {
        using var font = CreateFont();
        var owner = cleared ? new ListViewItem("old primary") : new ListViewItem();
        var first = new ListViewItem.ListViewSubItemCollection(owner);
        var second = new ListViewItem.ListViewSubItemCollection(owner);
        ListViewItem.ListViewSubItemCollection? property = cleared ? owner.SubItems : null;
        if (cleared)
        {
            first.Clear();
        }

        AddRange(first, mode, [null, "new primary", "detail"], font);

        Assert.Equal("new primary", owner.Text);
        Assert.Equal(2, first.Count);
        Assert.Same(first[0], second[0]);
        Assert.Same(first[1], second[1]);
        Assert.Equal(first.ToArray(), owner.SubItems.ToArray());
        if (property is not null)
        {
            Assert.Same(property, owner.SubItems);
        }

        owner.Text = "changed through owner";
        Assert.Equal("changed through owner", second[0].Text);
        first[0].Text = "changed through subitem";
        Assert.Equal("changed through subitem", owner.Text);
        Assert.Equal("changed through subitem", owner.SubItems[0].Text);
        first[1].Text = "changed detail";
        Assert.Equal("changed detail", second[1].Text);
        Assert.Equal("changed detail", owner.SubItems[1].Text);
    }

    [Theory]
    [InlineData("object")]
    [InlineData("text")]
    [InlineData("style")]
    public void RangeChangesKeepIndependentKeyCachesIncludingNullArrayAndEmptyCalls(string mode)
    {
        using var font = CreateFont();
        var owner = new ListViewItem(new[] { "primary", "earlier", "later" });
        var property = owner.SubItems;
        var first = new ListViewItem.ListViewSubItemCollection(owner);
        var second = new ListViewItem.ListViewSubItemCollection(owner);
        first[2].Name = "target";
        Assert.Equal(2, first.IndexOfKey("target"));
        first[1].Name = "target";
        Assert.Equal(1, second.IndexOfKey("target"));
        Assert.Equal(1, property.IndexOfKey("target"));

        AddRange(first, mode, ["added first", "added second"], font);
        first[3].Name = first[4].Name = "target";

        Assert.Equal(2, first.IndexOfKey("target"));
        Assert.Equal(1, second.IndexOfKey("target"));
        Assert.Equal(1, property.IndexOfKey("target"));
        Assert.Equal(1, new ListViewItem.ListViewSubItemCollection(owner).IndexOfKey("target"));
        property.Clear();
        AddRange(second, mode, ["new primary", "new earlier", "new later", "new last"], font);
        foreach (ListViewItem.ListViewSubItem item in first)
        {
            item.Name = "target";
        }

        Assert.Equal("new primary", owner.Text);
        Assert.Equal(2, first.IndexOfKey("target"));
        Assert.Equal(1, second.IndexOfKey("target"));
        Assert.Equal(1, property.IndexOfKey("target"));
        Assert.Equal(0, new ListViewItem.ListViewSubItemCollection(owner).IndexOfKey("target"));
        Assert.Equal(-1, first.IndexOfKey(null));
        Assert.Equal(-1, second.IndexOfKey(""));
        Assert.Throws<ArgumentNullException>("items", () => AddRange(first, mode, null, font));
        AddRange(second, mode, [], font);

        Assert.Equal(2, first.IndexOfKey("target"));
        Assert.Equal(1, second.IndexOfKey("target"));
        Assert.Equal(1, property.IndexOfKey("target"));
    }

    [Theory]
    [InlineData("clear")]
    [InlineData("remove")]
    [InlineData("reset")]
    [InlineData("replace-font")]
    [InlineData("null-font")]
    [InlineData("replace-item")]
    public void StyledRange_BorrowsFontWithoutDisposingItDuringLaterChanges(string operation)
    {
        using var font = CreateFont();
        using var replacement = new Font("Consolas", 10f);
        float height = font.GetHeight(96f);
        var owner = new ListViewItem("primary");
        var items = owner.SubItems;
        items.AddRange(["first", "second"], Color.Red, Color.Blue, font);
        var first = items[1];
        var second = items[2];

        switch (operation)
        {
            case "clear": items.Clear(); break;
            case "remove": items.Remove(first); break;
            case "reset": first.ResetStyle(); break;
            case "replace-font": first.Font = replacement; break;
            case "null-font": first.Font = null; break;
            case "replace-item": items[1] = new ListViewItem.ListViewSubItem(); break;
        }

        Assert.Same(font, second.Font);
        Assert.Equal(height, font.GetHeight(96f));
        Assert.Equal(height, second.Font.GetHeight(96f));
    }

    private static void AddRange(ListViewItem.ListViewSubItemCollection items, string mode, string?[]? input, Font font)
    {
        switch (mode)
        {
            case "object":
                items.AddRange(input?.Select(text => text is null ? null : new ListViewItem.ListViewSubItem(null, text)).ToArray()!);
                break;
            case "text": items.AddRange(input!); break;
            case "style": items.AddRange(input!, Color.Crimson, Color.AliceBlue, font); break;
            default: throw new ArgumentOutOfRangeException(nameof(mode));
        }
    }

    private static string[] Texts(ListViewItem.ListViewSubItemCollection items) => items.Select(item => item.Text).ToArray();

    private static Font CreateFont() => new("Arial", 12.5f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);

    private sealed class DerivedSubItem(ListViewItem? owner, string? text, Color fore, Color back, Font? font)
        : ListViewItem.ListViewSubItem(owner, text, fore, back, font)
    {
    }
}
