using System.Drawing;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public class ListViewSubItemAddTests
{
    [Fact]
    public void TypedAddOverloads_ReturnSubItemsWithoutBecomingVirtual()
    {
        Type collection = typeof(ListViewItem.ListViewSubItemCollection);
        foreach (Type[] parameters in new[]
        {
            new[] { typeof(ListViewItem.ListViewSubItem) },
            new[] { typeof(string) },
            new[] { typeof(string), typeof(Color), typeof(Color), typeof(Font) }
        })
        {
            var method = collection.GetMethod("Add", parameters);

            Assert.NotNull(method);
            Assert.Equal(typeof(ListViewItem.ListViewSubItem), method.ReturnType);
            Assert.True(method.IsPublic);
            Assert.False(method.IsVirtual);
        }
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void AddNullItem_RejectsParameterWithoutChangingOrInitializingData(bool initialized)
    {
        var owner = initialized ? new ListViewItem("primary") : new ListViewItem();
        var first = new ListViewItem.ListViewSubItemCollection(owner);
        var second = new ListViewItem.ListViewSubItemCollection(owner);
        var before = first.ToArray();

        Assert.Throws<ArgumentNullException>("item", () => first.Add((ListViewItem.ListViewSubItem)null!));

        Assert.Equal(before, first);
        Assert.Equal(before, second);
        Assert.Equal(initialized ? "primary" : string.Empty, owner.Text);
        Assert.Equal(initialized ? 1 : 0, second.Count);
    }

    [Fact]
    public void AddItem_ReturnsSameDerivedObjectAndPreservesDataAndStyle()
    {
        using var font = CreateFont();
        var owner = new ListViewItem("primary");
        object tag = new();
        string text = new string("detail 中文".ToCharArray());
        var item = new DerivedSubItem(null, text, Color.Crimson, Color.AliceBlue, font)
        {
            Name = "detail-key", Tag = tag
        };

        var result = owner.SubItems.Add(item);

        Assert.Same(item, result);
        Assert.Same(item, owner.SubItems[1]);
        Assert.Same(text, result.Text);
        Assert.Equal("detail-key", result.Name);
        Assert.Same(tag, result.Tag);
        Assert.Equal(Color.Crimson, result.ForeColor);
        Assert.Equal(Color.AliceBlue, result.BackColor);
        Assert.Same(font, result.Font);
        Assert.Equal("primary", owner.Text);
    }

    [Fact]
    public void AddItem_AllowsRepeatedObjectAndExistingPrimaryWithoutCopying()
    {
        var owner = new ListViewItem("primary");
        var items = owner.SubItems;
        var primary = items[0];
        var item = new ListViewItem.ListViewSubItem(null, "detail");

        Assert.Same(item, items.Add(item));
        Assert.Same(item, items.Add(item));
        Assert.Same(primary, items.Add(primary));

        Assert.Equal(new[] { primary, item, item, primary }, items);
        item.Text = "changed detail";
        primary.Text = "changed primary";
        Assert.Equal("changed primary", owner.Text);
        Assert.Equal(new[] { "changed primary", "changed detail", "changed detail", "changed primary" }, Texts(items));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void AddItem_FromOtherCollectionPreservesItsOriginalPositionAndSharesObject(int sourceIndex)
    {
        var source = new ListViewItem(new[] { "source primary", "source detail" });
        var target = new ListViewItem("target primary");
        var sourceItems = source.SubItems;
        var sourceBefore = sourceItems.ToArray();
        var item = sourceItems[sourceIndex];

        var added = target.SubItems.Add(item);

        Assert.Same(item, added);
        Assert.Same(item, target.SubItems[1]);
        Assert.Equal(sourceBefore, sourceItems);
        item.Text = "shared change";
        Assert.Equal("shared change", sourceItems[sourceIndex].Text);
        Assert.Equal("shared change", target.SubItems[1].Text);
        Assert.Equal(sourceIndex == 0 ? "shared change" : "source primary", source.Text);
        Assert.Equal("target primary", target.Text);

        sourceItems.Clear();

        Assert.Same(item, target.SubItems[1]);
        Assert.Equal("shared change", target.SubItems[1].Text);
    }

    [Fact]
    public void AddItem_ConstructedForOtherOwnerDoesNotInsertIntoThatOwnersCollection()
    {
        var source = new ListViewItem("source primary");
        var target = new ListViewItem("target primary");
        var item = new ListViewItem.ListViewSubItem(source, "detail");

        Assert.Same(item, target.SubItems.Add(item));

        Assert.DoesNotContain(item, source.SubItems);
        Assert.Single(source.SubItems);
        Assert.Same(item, target.SubItems[1]);
    }

    [Theory]
    [InlineData(null, "")]
    [InlineData("", "")]
    [InlineData(" ", " ")]
    [InlineData("detail 中文", "detail 中文")]
    public void AddText_ReturnsStoredNewObjectWithNormalizedTextAndDefaultStyle(string? text, string expected)
    {
        var owner = new ListViewItem("primary");

        var added = owner.SubItems.Add(text);

        Assert.Same(added, owner.SubItems[1]);
        Assert.Equal(expected, added.Text);
        Assert.Equal(string.Empty, added.Name);
        Assert.Null(added.Tag);
        Assert.Equal(SystemColors.WindowText, added.ForeColor);
        Assert.Equal(SystemColors.Window, added.BackColor);
        Assert.Same(new ListViewItem.ListViewSubItem().Font, added.Font);
        Assert.Equal("primary", owner.Text);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void AddTextOverloads_PreserveStringIdentityAndCreateIndependentObjects(bool styled)
    {
        using var font = CreateFont();
        var owner = new ListViewItem("primary");
        string text = new string("identity text".ToCharArray());
        var first = styled ? owner.SubItems.Add(text, Color.Red, Color.Blue, font) : owner.SubItems.Add(text);
        var second = styled ? owner.SubItems.Add(text, Color.Red, Color.Blue, font) : owner.SubItems.Add(text);

        Assert.NotSame(first, second);
        Assert.Same(text, first.Text);
        Assert.Same(text, second.Text);
        first.Text = "first changed";
        Assert.Equal(text, second.Text);
        Assert.Same(first, owner.SubItems[1]);
        Assert.Same(second, owner.SubItems[2]);
        if (styled)
        {
            Assert.Same(font, first.Font);
            Assert.Same(font, second.Font);
        }
    }

    [Fact]
    public void AddStyledText_AcceptsNullTextFontAndEmptyColors()
    {
        var owner = new ListViewItem("primary");

        var added = owner.SubItems.Add(null, Color.Empty, Color.Empty, null);

        Assert.Same(added, owner.SubItems[1]);
        Assert.Equal(string.Empty, added.Text);
        Assert.Equal(string.Empty, added.Name);
        Assert.Null(added.Tag);
        Assert.Equal(SystemColors.WindowText, added.ForeColor);
        Assert.Equal(SystemColors.Window, added.BackColor);
        Assert.Same(new ListViewItem.ListViewSubItem().Font, added.Font);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void AddStyledText_PreservesExplicitColorsAndBorrowedFont(bool transparent)
    {
        using var font = CreateFont();
        var owner = new ListViewItem("primary");
        Color foreColor = transparent ? Color.Transparent : Color.FromArgb(123, 11, 22, 33);
        Color backColor = transparent ? Color.FromArgb(0, 7, 8, 9) : Color.FromArgb(67, 44, 55, 66);

        var added = owner.SubItems.Add("styled", foreColor, backColor, font);

        Assert.Same(added, owner.SubItems[1]);
        Assert.Equal(foreColor, added.ForeColor);
        Assert.Equal(backColor, added.BackColor);
        Assert.Same(font, added.Font);
        Assert.Equal(string.Empty, added.Name);
        Assert.Null(added.Tag);
    }

    [Theory]
    [InlineData("object", "fresh")]
    [InlineData("text", "fresh")]
    [InlineData("style", "fresh")]
    [InlineData("object", "cleared-external")]
    [InlineData("text", "cleared-external")]
    [InlineData("style", "cleared-external")]
    [InlineData("object", "cleared-property")]
    [InlineData("text", "cleared-property")]
    [InlineData("style", "cleared-property")]
    public void AddToEmptyView_BecomesPrimaryAcrossAllViewsWithoutExtraSubItem(string mode, string shape)
    {
        using var font = CreateFont();
        var owner = shape == "fresh" ? new ListViewItem() : new ListViewItem("original primary");
        var first = new ListViewItem.ListViewSubItemCollection(owner);
        var second = new ListViewItem.ListViewSubItemCollection(owner);
        ListViewItem.ListViewSubItemCollection? property = shape == "cleared-property" ? owner.SubItems : null;
        if (shape != "fresh")
        {
            first.Clear();
        }
        Assert.Empty(first);

        var added = Add(first, mode, "new primary", font);

        Assert.Equal("new primary", owner.Text);
        Assert.Same(added, Assert.Single(first));
        Assert.Same(added, Assert.Single(second));
        Assert.Same(added, Assert.Single(owner.SubItems));
        if (property is not null)
        {
            Assert.Same(property, owner.SubItems);
        }

        owner.Text = "through owner";
        Assert.Equal("through owner", added.Text);
        added.Text = "through added";
        Assert.Equal("through added", owner.Text);
        Assert.Equal("through added", second[0].Text);
    }

    [Theory]
    [InlineData("object")]
    [InlineData("text")]
    [InlineData("style")]
    public void AddToPopulatedView_SharesNewSubItemWithoutChangingPrimary(string mode)
    {
        using var font = CreateFont();
        var owner = new ListViewItem("primary");
        var first = new ListViewItem.ListViewSubItemCollection(owner);
        var second = new ListViewItem.ListViewSubItemCollection(owner);
        var primary = first[0];

        var added = Add(first, mode, "detail", font);

        Assert.Equal("primary", owner.Text);
        Assert.Same(primary, owner.SubItems[0]);
        Assert.Equal(2, second.Count);
        Assert.Same(added, second[1]);
        Assert.Same(added, owner.SubItems[1]);
        added.Text = "changed detail";
        Assert.Equal("changed detail", second[1].Text);
        Assert.Equal("primary", owner.Text);
    }

    [Theory]
    [InlineData("object")]
    [InlineData("text")]
    [InlineData("style")]
    public void AddAndClearRepopulation_PreserveIndependentLastKeyPositions(string mode)
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

        Add(first, mode, "added", font).Name = "target";

        Assert.Equal(2, first.IndexOfKey("target"));
        Assert.Equal(1, second.IndexOfKey("target"));
        Assert.Equal(1, property.IndexOfKey("target"));

        property.Clear();
        foreach (string text in new[] { "new primary", "new earlier", "new later", "new last" })
        {
            Add(second, mode, text, font).Name = "target";
        }

        Assert.Equal("new primary", owner.Text);
        Assert.Equal(2, first.IndexOfKey("target"));
        Assert.Equal(1, second.IndexOfKey("target"));
        Assert.Equal(1, property.IndexOfKey("target"));
        Assert.Equal(0, new ListViewItem.ListViewSubItemCollection(owner).IndexOfKey("target"));
        Assert.Equal(-1, first.IndexOfKey(null));
        Assert.Equal(-1, second.IndexOfKey(""));

        Add(first, mode, "last", font).Name = "target";

        Assert.Equal(2, first.IndexOfKey("target"));
        Assert.Equal(1, second.IndexOfKey("target"));
    }

    [Theory]
    [InlineData("clear")]
    [InlineData("remove")]
    [InlineData("reset")]
    [InlineData("replace-font")]
    [InlineData("null-font")]
    [InlineData("replace-item")]
    public void AddStyledText_BorrowsSharedFontWithoutDisposingItOnLaterChanges(string operation)
    {
        using var font = CreateFont();
        using var replacementFont = new Font("Consolas", 10f);
        float height = font.GetHeight(96f);
        var owner = new ListViewItem("primary");
        var first = owner.SubItems.Add("first", Color.Red, Color.Blue, font);
        var second = owner.SubItems.Add("second", Color.Green, Color.Yellow, font);

        switch (operation)
        {
            case "clear": owner.SubItems.Clear(); break;
            case "remove": owner.SubItems.Remove(first); break;
            case "reset": first.ResetStyle(); break;
            case "replace-font": first.Font = replacementFont; break;
            case "null-font": first.Font = null; break;
            case "replace-item": owner.SubItems[1] = new ListViewItem.ListViewSubItem(); break;
        }

        Assert.Same(font, second.Font);
        Assert.Equal(height, font.GetHeight(96f));
        Assert.Equal(height, second.Font.GetHeight(96f));
    }

    [Fact]
    public void LegacyGenericCollectionAdd_AdaptsVoidContractAndTypedIdentityRules()
    {
        var owner = new ListViewItem();
        var view = new ListViewItem.ListViewSubItemCollection(owner);
        ICollection<ListViewItem.ListViewSubItem> generic = view;
        var item = new ListViewItem.ListViewSubItem(null, "primary");

        generic.Add(item);
        generic.Add(item);

        Assert.Equal(2, generic.Count);
        Assert.Same(item, view[0]);
        Assert.Same(item, view[1]);
        Assert.Equal("primary", owner.Text);
        Assert.Same(item, view.Add(item));
        Assert.Equal(3, generic.Count);
    }

    [Fact]
    public void LegacyGenericCollectionAdd_RejectsNullWithTypedParameterName()
    {
        var owner = new ListViewItem();
        var view = new ListViewItem.ListViewSubItemCollection(owner);
        ICollection<ListViewItem.ListViewSubItem> generic = view;

        Assert.Throws<ArgumentNullException>("item", () => generic.Add(null!));

        Assert.Empty(view);
        Assert.Equal(string.Empty, owner.Text);
    }

    private static ListViewItem.ListViewSubItem Add(ListViewItem.ListViewSubItemCollection items, string mode, string text, Font font) => mode switch
    {
        "object" => items.Add(new ListViewItem.ListViewSubItem(null, text)),
        "text" => items.Add(text),
        "style" => items.Add(text, Color.Crimson, Color.AliceBlue, font),
        _ => throw new ArgumentOutOfRangeException(nameof(mode))
    };

    private static string[] Texts(ListViewItem.ListViewSubItemCollection items) => items.Select(item => item.Text).ToArray();

    private static Font CreateFont() => new("Arial", 12.5f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);

    private sealed class DerivedSubItem(ListViewItem? owner, string? text, Color fore, Color back, Font? font)
        : ListViewItem.ListViewSubItem(owner, text, fore, back, font)
    {
    }
}
