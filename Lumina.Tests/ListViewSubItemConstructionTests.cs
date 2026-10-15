using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public class ListViewSubItemConstructionTests
{
    [Fact]
    public void ParameterlessConstructor_InitializesEmptyTextAndNameAndNullTag()
    {
        var item = new ListViewItem.ListViewSubItem();

        Assert.Equal(string.Empty, item.Text);
        Assert.Equal(string.Empty, item.Name);
        Assert.Null(item.Tag);
    }

    [Theory]
    [InlineData(null, "")]
    [InlineData("", "")]
    [InlineData("detail 中文", "detail 中文")]
    public void OwnerTextConstructor_AcceptsNullOwnerAndNormalizesText(string? text, string expected)
    {
        var item = new ListViewItem.ListViewSubItem(null, text);

        Assert.Equal(expected, item.Text);
        Assert.Equal(string.Empty, item.Name);
        Assert.Null(item.Tag);
    }

    [Fact]
    public void OwnerTextConstructor_PreservesStringIdentity()
    {
        string text = new string("detail 中文".ToCharArray());

        var item = new ListViewItem.ListViewSubItem(null, text);

        Assert.Same(text, item.Text);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void OwnerTextConstructor_DoesNotInsertOrReplaceSubItems(bool populated)
    {
        var owner = populated ? new ListViewItem(new[] { "row", "detail" }) : new ListViewItem();
        var subItems = owner.SubItems;
        var primary = subItems[0];
        var before = subItems.ToArray();
        string textBefore = owner.Text;

        var item = new ListViewItem.ListViewSubItem(owner, "constructed detail");
        item.Text = "changed detached detail";

        Assert.Same(subItems, owner.SubItems);
        Assert.Equal(before, subItems);
        Assert.Same(primary, subItems[0]);
        Assert.Equal(textBefore, owner.Text);
        Assert.DoesNotContain(item, subItems);

        subItems.Add(item);
        item.Text = "changed member detail";

        Assert.Same(item, subItems[^1]);
        Assert.Equal("changed member detail", subItems[^1].Text);
        Assert.Equal(textBefore, owner.Text);
    }

    [Fact]
    public void OwnerTextConstructor_AcceptsItemAlreadyAssociatedWithListView()
    {
        using var view = new ListView();
        var owner = new ListViewItem(new[] { "row", "detail" });
        view.Items.Add(owner);
        var before = owner.SubItems.ToArray();

        var item = new ListViewItem.ListViewSubItem(owner, "constructed detail");
        item.Text = null;

        Assert.Equal("row", owner.Text);
        Assert.Equal(before, owner.SubItems);
        Assert.DoesNotContain(item, owner.SubItems);
    }

    [Fact]
    public void TextNameAndTag_PreserveIdentityAndNormalizeNull()
    {
        var item = new ListViewItem.ListViewSubItem();
        string text = new string("updated text 中文".ToCharArray());
        string name = new string("MiXeD name 中文".ToCharArray());
        object tag = new();

        item.Text = text;
        item.Name = name;
        item.Tag = tag;

        Assert.Same(text, item.Text);
        Assert.Same(name, item.Name);
        Assert.Same(tag, item.Tag);

        item.Text = null;
        item.Name = null;
        item.Tag = null;

        Assert.Equal(string.Empty, item.Text);
        Assert.Equal(string.Empty, item.Name);
        Assert.Null(item.Tag);
    }

    [Fact]
    public void Tag_PreservesBoxedValueIdentity()
    {
        var item = new ListViewItem.ListViewSubItem { Tag = 42 };
        object? tag = item.Tag;

        Assert.Equal(42, item.Tag);
        Assert.Same(tag, item.Tag);
    }

    [Fact]
    public void PrimarySubItem_TextTracksItemWithoutChangingNameOrTag()
    {
        var owner = new ListViewItem("row");
        var primary = owner.SubItems[0];
        object tag = new();
        primary.Name = "primary-name";
        primary.Tag = tag;
        string text = new string("provided text 中文".ToCharArray());

        owner.Text = text;

        Assert.Same(primary, owner.SubItems[0]);
        Assert.Same(text, primary.Text);
        Assert.Equal("primary-name", primary.Name);
        Assert.Same(tag, primary.Tag);

        primary.Text = null;

        Assert.Equal(string.Empty, owner.Text);
        Assert.Equal("primary-name", primary.Name);
        Assert.Same(tag, primary.Tag);
    }

    [Fact]
    public void PublicConstructedPrimaryReplacement_TracksItemAndLeavesOriginalOwnerText()
    {
        var owner = new ListViewItem("row");
        var originalOwner = new ListViewItem(new[] { "original row", "original detail" });
        var replacement = new ListViewItem.ListViewSubItem(originalOwner, "replacement");

        owner.SubItems[0] = replacement;
        owner.Text = "updated replacement";

        Assert.Same(replacement, owner.SubItems[0]);
        Assert.Equal("updated replacement", replacement.Text);
        Assert.Equal("original row", originalOwner.Text);
        Assert.Equal(2, originalOwner.SubItems.Count);
    }

    [Fact]
    public void DerivedSubItem_CanUseBothPublicConstructors()
    {
        var defaultItem = new DerivedSubItem();
        var owner = new ListViewItem("row");
        var item = new DerivedSubItem(owner, "derived");

        Assert.Equal(string.Empty, defaultItem.Text);
        Assert.Equal(string.Empty, defaultItem.Name);
        Assert.Null(defaultItem.Tag);
        Assert.Equal("derived", item.Text);
        Assert.Equal(string.Empty, item.Name);
        Assert.Null(item.Tag);
        Assert.Equal("row", owner.Text);
        Assert.DoesNotContain(item, owner.SubItems);
    }

    private sealed class DerivedSubItem : ListViewItem.ListViewSubItem
    {
        internal DerivedSubItem()
        {
        }

        internal DerivedSubItem(ListViewItem? owner, string? text) : base(owner, text)
        {
        }
    }
}
