using System.Drawing;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public sealed class PanelScrollableInheritanceTests
{
    [Fact]
    public void Panel_UsesTheStandardScrollableControlInheritanceChain()
    {
        Assert.Equal(typeof(ScrollableControl), typeof(Panel).BaseType);

        using var panel = new Panel
        {
            ClientSize = new Size(100, 80),
            Padding = new Padding(2),
        };

        Assert.IsAssignableFrom<Control>(panel);
        Assert.Equal(new Rectangle(2, 2, 96, 76), panel.DisplayRectangle);
        Assert.False(panel.AutoScroll);
    }

    [Fact]
    public void Panel_ConsumesTheSharedAutoScrollPositionContract()
    {
        using var panel = new Panel
        {
            ClientSize = new Size(100, 80),
            AutoScroll = true,
            AutoScrollMinSize = new Size(240, 160),
        };

        panel.AutoScrollPosition = new Point(40, 25);

        Assert.Equal(new Point(-40, -25), panel.AutoScrollPosition);
        Assert.Equal(new Point(-40, -25), panel.DisplayRectangle.Location);
        Assert.Equal(140, panel.HorizontalScroll.Maximum);
        Assert.Equal(80, panel.VerticalScroll.Maximum);
    }

    [Fact]
    public void Panel_StillMeasuresChildrenThroughItsExistingLayoutEngine()
    {
        using var panel = new Panel
        {
            Padding = new Padding(3, 5, 7, 11),
        };
        using var child = new Control
        {
            Bounds = new Rectangle(13, 15, 20, 10),
            Margin = new Padding(2),
        };
        panel.Controls.Add(child);

        Size preferred = panel.GetPreferredSize(Size.Empty);

        Assert.True(preferred.Width >= 35);
        Assert.True(preferred.Height >= 31);
    }

    [Fact]
    public void Panel_DisposesChildrenThroughTheSharedControlTree()
    {
        using var child = new Control();
        var panel = new Panel();
        panel.Controls.Add(child);

        panel.Dispose();

        Assert.True(panel.IsDisposed);
        Assert.True(child.IsDisposed);
    }

    [Fact]
    public void FlowAndTablePanels_RetainPanelAndScrollableBases()
    {
        using var flow = new FlowLayoutPanel();
        using var table = new TableLayoutPanel();

        Assert.IsAssignableFrom<Panel>(flow);
        Assert.IsAssignableFrom<ScrollableControl>(flow);
        Assert.IsAssignableFrom<Panel>(table);
        Assert.IsAssignableFrom<ScrollableControl>(table);
    }
}
