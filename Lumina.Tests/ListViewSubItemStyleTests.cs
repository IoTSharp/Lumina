using System.Drawing;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public class ListViewSubItemStyleTests
{
    [Fact]
    public void DefaultStyle_UsesWindowColorsAndSharedMessageBoxFont()
    {
        var first = new ListViewItem.ListViewSubItem();
        var second = new ListViewItem.ListViewSubItem(null, "detail");
        Font? expected = SystemFonts.MessageBoxFont;

        Assert.Equal(SystemColors.WindowText, first.ForeColor);
        Assert.Equal(SystemColors.Window, first.BackColor);
        Assert.Same(first.Font, first.Font);
        Assert.Same(first.Font, second.Font);
        Assert.NotNull(expected);
        Assert.Equal(expected.Name, first.Font.Name);
        Assert.Equal(expected.Size, first.Font.Size);
        Assert.Equal(expected.Style, first.Font.Style);
        Assert.Equal(expected.Unit, first.Font.Unit);
        Assert.Equal(expected.GdiCharSet, first.Font.GdiCharSet);
        Assert.Equal(expected.GdiVerticalFont, first.Font.GdiVerticalFont);
    }

    [Fact]
    public void StyleConstructor_AcceptsNullOwnerTextAndFontAndEmptyColors()
    {
        var item = new ListViewItem.ListViewSubItem(null, null, Color.Empty, Color.Empty, null);

        Assert.Equal(string.Empty, item.Text);
        Assert.Equal(string.Empty, item.Name);
        Assert.Null(item.Tag);
        Assert.Equal(SystemColors.WindowText, item.ForeColor);
        Assert.Equal(SystemColors.Window, item.BackColor);
        Assert.Same(new ListViewItem.ListViewSubItem().Font, item.Font);
    }

    [Fact]
    public void StyleConstructor_PreservesTextAndBorrowedFontIdentity()
    {
        using var font = CreateFont();
        string text = new string("styled detail 中文".ToCharArray());

        var item = new ListViewItem.ListViewSubItem(null, text, Color.Crimson, Color.AliceBlue, font);

        Assert.Same(text, item.Text);
        Assert.Same(font, item.Font);
        Assert.Equal(Color.Crimson, item.ForeColor);
        Assert.Equal(Color.AliceBlue, item.BackColor);
        Assert.Equal(string.Empty, item.Name);
        Assert.Null(item.Tag);
    }

    [Fact]
    public void StyleConstructor_PreservesTransparentAndCustomAlphaColors()
    {
        Color backColor = Color.FromArgb(0, 7, 8, 9);

        var item = new ListViewItem.ListViewSubItem(null, "detail", Color.Transparent, backColor, null);

        Assert.Equal(Color.Transparent, item.ForeColor);
        Assert.Equal(backColor, item.BackColor);
        Assert.False(item.BackColor.IsEmpty);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void StyleConstructor_DoesNotInsertOrReplaceOwnerSubItems(bool populated)
    {
        using var font = CreateFont();
        var owner = populated ? new ListViewItem(new[] { "row", "detail" }) : new ListViewItem();
        var collection = owner.SubItems;
        var primary = collection[0];
        var before = collection.ToArray();
        string textBefore = owner.Text;

        var item = new ListViewItem.ListViewSubItem(owner, "styled detail", Color.Red, Color.Blue, font);
        item.ResetStyle();

        Assert.Same(collection, owner.SubItems);
        Assert.Same(primary, collection[0]);
        Assert.Equal(before, collection);
        Assert.Equal(textBefore, owner.Text);
        Assert.DoesNotContain(item, collection);
        Assert.Equal("styled detail", item.Text);
    }

    [Fact]
    public void UnattachedOwner_DoesNotSupplyItsExplicitForegroundToOtherSubItems()
    {
        var owner = new ListViewItem("row") { ForeColor = Color.DarkMagenta };
        var implicitStyle = new ListViewItem.ListViewSubItem(owner, "implicit detail");
        var emptyStyle = new ListViewItem.ListViewSubItem(owner, "empty detail", Color.Empty, Color.Empty, null);

        Assert.Equal(SystemColors.WindowText, implicitStyle.ForeColor);
        Assert.Equal(SystemColors.WindowText, emptyStyle.ForeColor);
        Assert.Equal(SystemColors.Window, implicitStyle.BackColor);
        Assert.Equal(SystemColors.Window, emptyStyle.BackColor);
        Assert.Same(implicitStyle.Font, emptyStyle.Font);
        Assert.Equal(Color.DarkMagenta, owner.ForeColor);
    }

    [Fact]
    public void ColorProperties_EmptyRestoresDefaultsWithoutChangingOtherStyle()
    {
        using var font = CreateFont();
        Color foreColor = Color.FromArgb(123, 11, 22, 33);
        Color backColor = Color.FromArgb(67, 44, 55, 66);
        var item = new ListViewItem.ListViewSubItem(null, "detail", foreColor, backColor, font);

        item.ForeColor = Color.Empty;

        Assert.Equal(SystemColors.WindowText, item.ForeColor);
        Assert.Equal(backColor, item.BackColor);
        Assert.Same(font, item.Font);

        item.BackColor = Color.Empty;

        Assert.Equal(SystemColors.WindowText, item.ForeColor);
        Assert.Equal(SystemColors.Window, item.BackColor);
        Assert.Same(font, item.Font);
    }

    [Fact]
    public void FontProperty_NullRestoresSameDefaultWithoutChangingColors()
    {
        using var font = CreateFont();
        var item = new ListViewItem.ListViewSubItem();
        Font defaultFont = item.Font;
        item.ForeColor = Color.Red;
        item.BackColor = Color.Blue;
        item.Font = font;

        Assert.Same(font, item.Font);

        item.Font = null;

        Assert.Same(defaultFont, item.Font);
        Assert.Equal(Color.Red, item.ForeColor);
        Assert.Equal(Color.Blue, item.BackColor);
    }

    [Fact]
    public void ResetStyle_RestoresDefaultsAndPreservesData()
    {
        using var font = CreateFont();
        var item = new ListViewItem.ListViewSubItem();
        Font defaultFont = item.Font;
        string text = new string("stable text".ToCharArray());
        string name = new string("stable name".ToCharArray());
        object tag = new();
        item.Text = text;
        item.Name = name;
        item.Tag = tag;
        item.ForeColor = Color.Green;
        item.BackColor = Color.Yellow;
        item.Font = font;

        item.ResetStyle();
        item.ResetStyle();

        Assert.Equal(SystemColors.WindowText, item.ForeColor);
        Assert.Equal(SystemColors.Window, item.BackColor);
        Assert.Same(defaultFont, item.Font);
        Assert.Same(text, item.Text);
        Assert.Same(name, item.Name);
        Assert.Same(tag, item.Tag);
    }

    [Fact]
    public void ResetStyle_OnUnattachedOwnerMemberPreservesCollectionAndPrimaryText()
    {
        using var font = CreateFont();
        var owner = new ListViewItem(new[] { "row", "detail" });
        var primary = owner.SubItems[0];
        var item = new ListViewItem.ListViewSubItem(owner, "styled detail", Color.Red, Color.Blue, font);
        owner.SubItems.Add(item);

        item.ResetStyle();

        Assert.Same(primary, owner.SubItems[0]);
        Assert.Same(item, owner.SubItems[2]);
        Assert.Equal(3, owner.SubItems.Count);
        Assert.Equal("row", owner.Text);
        Assert.Equal("styled detail", item.Text);
        Assert.Equal(SystemColors.WindowText, item.ForeColor);
        Assert.Equal(SystemColors.Window, item.BackColor);
    }

    [Theory]
    [InlineData("reset")]
    [InlineData("null")]
    [InlineData("replace")]
    public void ClearingBorrowedFont_DoesNotDisposeSharedFont(string operation)
    {
        using var shared = CreateFont();
        using var replacement = new Font("Consolas", 10f);
        float expectedHeight = shared.GetHeight(96f);
        var first = new ListViewItem.ListViewSubItem(null, "first", Color.Red, Color.Blue, shared);
        var second = new ListViewItem.ListViewSubItem(null, "second", Color.Green, Color.Yellow, shared);

        switch (operation)
        {
            case "reset":
                first.ResetStyle();
                break;
            case "null":
                first.Font = null;
                break;
            case "replace":
                first.Font = replacement;
                Assert.Same(replacement, first.Font);
                break;
        }

        Assert.Same(shared, second.Font);
        Assert.Equal(expectedHeight, shared.GetHeight(96f));
        Assert.Equal(expectedHeight, second.Font.GetHeight(96f));

        second.ResetStyle();

        Assert.Equal(expectedHeight, shared.GetHeight(96f));
    }

    [Fact]
    public void DerivedSubItem_CanUseStyleConstructorAndResetStyle()
    {
        using var font = CreateFont();
        var owner = new ListViewItem("row");

        var item = new DerivedSubItem(owner, "derived", Color.Orange, Color.Navy, font);

        Assert.Equal("derived", item.Text);
        Assert.Equal(Color.Orange, item.ForeColor);
        Assert.Equal(Color.Navy, item.BackColor);
        Assert.Same(font, item.Font);

        item.ResetStyle();

        Assert.Equal(SystemColors.WindowText, item.ForeColor);
        Assert.Equal(SystemColors.Window, item.BackColor);
        Assert.Equal("row", owner.Text);
        Assert.DoesNotContain(item, owner.SubItems);
    }

    private static Font CreateFont() => new("Arial", 12.5f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);

    private sealed class DerivedSubItem : ListViewItem.ListViewSubItem
    {
        internal DerivedSubItem(ListViewItem? owner, string? text, Color foreColor, Color backColor, Font? font)
            : base(owner, text, foreColor, backColor, font)
        {
        }
    }
}
