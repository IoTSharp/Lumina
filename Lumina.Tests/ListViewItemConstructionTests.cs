using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public class ListViewItemConstructionTests
{
    [Fact]
    public void ParameterlessConstructor_InitializesEmptyTextAndPrimarySubItem()
    {
        var item = new ListViewItem();

        Assert.Equal(string.Empty, item.Text);
        Assert.Equal(string.Empty, Assert.Single(item.SubItems).Text);
        Assert.Same(item.SubItems, item.SubItems);
    }

    public static TheoryData<string?[]?, string[]> ArrayConstructorData => new()
    {
        { null, [""] },
        { [], [""] },
        { ["row"], ["row"] },
        { ["row", "detail", "last"], ["row", "detail", "last"] },
        { [null], [""] },
        { [null, "", null, "detail"], ["", "", "", "detail"] }
    };

    [Theory]
    [MemberData(nameof(ArrayConstructorData))]
    public void ArrayConstructor_InitializesSubItemsInOrder(string?[]? texts, string[] expected)
    {
        var item = new ListViewItem(texts);

        Assert.Equal(expected[0], item.Text);
        Assert.Equal(expected, item.SubItems.Select(subItem => subItem.Text));
    }

    [Fact]
    public void ArrayConstructor_CopiesInputTextIntoIndependentSubItems()
    {
        string[] texts = ["row", "detail"];
        var item = new ListViewItem(texts);

        texts[0] = "changed source";
        texts[1] = "changed detail";

        Assert.Equal("row", item.Text);
        Assert.Equal(["row", "detail"], item.SubItems.Select(subItem => subItem.Text));

        item.SubItems[1].Text = "changed subitem";

        Assert.Equal("changed detail", texts[1]);
    }

    [Fact]
    public void ArrayConstructor_PrimaryTextStaysInSyncWithoutChangingOtherSubItems()
    {
        var item = new ListViewItem(new[] { "row", "detail" });
        var primary = item.SubItems[0];
        var detail = item.SubItems[1];

        item.Text = "updated row";

        Assert.Same(primary, item.SubItems[0]);
        Assert.Equal("updated row", primary.Text);

        primary.Text = null;

        Assert.Equal(string.Empty, item.Text);
        Assert.Same(detail, item.SubItems[1]);
        Assert.Equal("detail", detail.Text);
    }

    [Theory]
    [InlineData(null, "")]
    [InlineData("", "")]
    [InlineData("row", "row")]
    public void Constructor_InitializesOnePrimarySubItem(string? text, string expected)
    {
        var item = new ListViewItem(text);

        Assert.Equal(expected, item.Text);
        Assert.Equal(expected, Assert.Single(item.SubItems).Text);
    }

    [Fact]
    public void TextAndPrimarySubItem_StayInSyncWithoutChangingOtherSubItems()
    {
        var item = new ListViewItem("row");
        var primary = item.SubItems[0];
        var detail = item.SubItems.Add("detail");

        item.Text = "updated row";

        Assert.Same(primary, item.SubItems[0]);
        Assert.Equal("updated row", primary.Text);

        primary.Text = "updated through subitem";

        Assert.Equal("updated through subitem", item.Text);
        Assert.Equal(2, item.SubItems.Count);
        Assert.Same(detail, item.SubItems[1]);
        Assert.Equal("detail", detail.Text);
    }

    [Fact]
    public void NullText_IsNormalizedForItemAndSubItems()
    {
        var item = new ListViewItem("row");

        item.Text = null;

        Assert.Equal(string.Empty, item.Text);
        Assert.Equal(string.Empty, item.SubItems[0].Text);

        item.Text = "restored";
        item.SubItems[0].Text = null;

        Assert.Equal(string.Empty, item.Text);
        Assert.Equal(string.Empty, item.SubItems[0].Text);
        Assert.Equal(string.Empty, item.SubItems.Add((string?)null).Text);
    }

    [Fact]
    public void Text_AfterClearingSubItems_RecreatesPrimarySubItem()
    {
        var item = new ListViewItem("row");
        item.SubItems.Add("detail");
        item.SubItems.Clear();

        Assert.Equal(string.Empty, item.Text);

        item.Text = "restored";

        Assert.Equal("restored", item.Text);
        Assert.Equal("restored", Assert.Single(item.SubItems).Text);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void EmptySubItems_TextReadDoesNotRecreatePrimaryUntilCollectionIsAccessed(bool removeOnlyItem)
    {
        var item = new ListViewItem();
        var subItems = item.SubItems;
        var previousPrimary = Assert.Single(subItems);
        if (removeOnlyItem)
        {
            subItems.RemoveAt(0);
        }
        else
        {
            subItems.Clear();
        }

        Assert.Empty(subItems);
        Assert.Equal(string.Empty, item.Text);
        Assert.Empty(subItems);

        var reopened = item.SubItems;

        Assert.Same(subItems, reopened);
        Assert.NotSame(previousPrimary, Assert.Single(reopened));
        Assert.Equal(string.Empty, reopened[0].Text);
    }

    [Fact]
    public void EmptySubItems_AddBeforeCollectionAccessBecomesPrimary()
    {
        var item = new ListViewItem(Array.Empty<string>());
        var subItems = item.SubItems;
        subItems.Clear();

        var added = subItems.Add("manual first");

        Assert.Equal("manual first", item.Text);
        Assert.Same(added, Assert.Single(item.SubItems));
    }

    [Fact]
    public void Text_AfterRemovingPrimarySubItem_UsesNewFirstSubItem()
    {
        var item = new ListViewItem("row");
        var detail = item.SubItems.Add("detail");

        item.SubItems.RemoveAt(0);

        Assert.Equal("detail", item.Text);

        item.Text = "new primary";

        Assert.Same(detail, Assert.Single(item.SubItems));
        Assert.Equal("new primary", detail.Text);
    }

    [Fact]
    public void Text_AfterRemovingOnlySubItem_RecreatesPrimarySubItem()
    {
        var item = new ListViewItem("row");
        item.SubItems.RemoveAt(0);

        Assert.Equal(string.Empty, item.Text);

        item.Text = null;

        Assert.Equal(string.Empty, item.Text);
        Assert.Equal(string.Empty, Assert.Single(item.SubItems).Text);
    }

    [Fact]
    public void Text_AfterReplacingPrimarySubItem_TracksReplacement()
    {
        var item = new ListViewItem("row");
        var replacementOwner = new ListViewItem("replacement");
        var replacement = replacementOwner.SubItems[0];

        item.SubItems[0] = replacement;

        Assert.Equal("replacement", item.Text);

        item.Text = "updated replacement";

        Assert.Equal("updated replacement", replacement.Text);
    }
}
