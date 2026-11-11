using System.Drawing;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public sealed class ListViewImageListGroupTests
{
    [Fact]
    public void ImageList_DefaultsAndKeyMap_AreStable()
    {
        using ImageList images = new();
        Assert.Equal(new Size(16, 16), images.ImageSize);
        Assert.Equal(ColorDepth.Depth32Bit, images.ColorDepth);

        using Bitmap source = new(16, 16);
        images.Images.Add("primary", source);
        Assert.Equal(1, images.Images.Count);
        Assert.Equal(0, images.Images.IndexOfKey("PRIMARY"));
        Assert.Equal("primary", images.Images.Keys[0]);
    }

    [Fact]
    public void ImageList_AddStrip_RequiresConfiguredFrames()
    {
        using ImageList images = new() { ImageSize = new Size(8, 8) };
        using Bitmap strip = new(16, 8);
        Assert.Equal(0, images.Images.AddStrip(strip));
        Assert.Equal(2, images.Images.Count);

        using Bitmap invalid = new(15, 8);
        Assert.Throws<ArgumentException>(() => images.Images.AddStrip(invalid));
    }

    [Fact]
    public void ListView_ExposesNestedCollectionsAndPreservesOwnership()
    {
        using ListView list = new();
        Assert.IsType<ListView.ListViewItemCollection>(list.Items);
        Assert.IsType<ListView.ColumnHeaderCollection>(list.Columns);

        ListViewItem item = list.Items.Add("item");
        Assert.Same(list, item.ListView);
        list.Items.Remove(item);
        Assert.Null(item.ListView);
    }

    [Fact]
    public void ListViewGroup_AssignsExistingItemAndRejectsCrossListItems()
    {
        using ListView first = new();
        using ListView second = new();
        ListViewGroup group = new("group");
        first.Groups.Add(group);
        ListViewItem item = first.Items.Add("item");
        item.Group = group;
        Assert.Same(group, item.Group);
        Assert.Single(group.Items);

        Assert.Throws<ArgumentException>(() => second.Items.Add(item));
    }

    [Fact]
    public void DisposedImageList_UnhooksTheConsumingListView()
    {
        using ListView list = new();
        ImageList images = new();
        list.SmallImageList = images;
        images.Dispose();
        Assert.Null(list.SmallImageList);
    }
}
