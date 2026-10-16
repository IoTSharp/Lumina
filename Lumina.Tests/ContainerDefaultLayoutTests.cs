using System.Drawing;
using System.Runtime.ExceptionServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public class ContainerDefaultLayoutTests
{
    [Fact]
    public void Panel_DefaultLayoutUsesThePaddedClientArea()
        => OnStaThread(() =>
        {
            using var panel = new Panel { ClientSize = new Size(160, 90), Padding = new Padding(3, 5, 7, 11) };
            var child = new Control { Dock = DockStyle.Fill, Margin = new Padding(20) };
            panel.Controls.Add(child);

            Assert.Equal(new Rectangle(3, 5, 150, 74), panel.DisplayRectangle);
            Assert.Equal(panel.DisplayRectangle, child.Bounds);
            Assert.False(panel.IsHandleCreated);
            Assert.False(child.IsHandleCreated);
        });

    [Fact]
    public void Panel_PreferredSizeIncludesChildExtentMarginAndPadding()
        => OnStaThread(() =>
        {
            using var panel = new Panel { Size = new Size(160, 90), Padding = new Padding(3, 5, 7, 11) };
            var child = new Control { Bounds = new Rectangle(13, 15, 20, 10), Margin = new Padding(2) };
            panel.Controls.Add(child);

            Assert.Equal(new Size(42, 38), panel.PreferredSize);
            child.Width = 30;
            Assert.Equal(new Size(52, 38), panel.PreferredSize);
            child.Visible = false;
            Assert.Equal(new Size(10, 16), panel.PreferredSize);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void FlowLayout_PreservesItsArrangementAndMeasuresItsContent(bool autoSize)
        => OnStaThread(() =>
        {
            using var parent = new Panel { Size = new Size(300, 180) };
            var flow = new FlowLayoutPanel
            {
                Size = new Size(160, 90), Padding = new Padding(2), WrapContents = false,
                AutoSize = autoSize, AutoSizeMode = AutoSizeMode.GrowAndShrink,
            };
            var first = new Control { Size = new Size(20, 10), Margin = new Padding(1), Dock = DockStyle.Top };
            var second = new Control { Size = new Size(30, 15), Margin = new Padding(1) };
            flow.Controls.AddRange(first, second);
            parent.Controls.Add(flow);
            parent.PerformLayout();
            flow.PerformLayout();

            Assert.Equal(new Size(58, 21), flow.PreferredSize);
            Assert.Equal(autoSize ? new Size(58, 21) : new Size(160, 90), flow.Size);
            Assert.Equal(new Rectangle(3, 3, 20, 10), first.Bounds);
            Assert.Equal(new Rectangle(25, 3, 30, 15), second.Bounds);
            Assert.NotSame(parent.LayoutEngine, flow.LayoutEngine);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void TableLayout_PreservesAbsoluteCellsAndMeasuresTheirExtent(bool autoSize)
        => OnStaThread(() =>
        {
            using var parent = new Panel { Size = new Size(300, 180) };
            var table = new TableLayoutPanel
            {
                Size = new Size(104, 24), Padding = new Padding(2), ColumnCount = 2, RowCount = 1,
                AutoSize = autoSize, AutoSizeMode = AutoSizeMode.GrowAndShrink,
            };
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));
            var first = new Control { Size = new Size(20, 10), Margin = new Padding(1) };
            var second = new Control { Size = new Size(30, 12), Margin = new Padding(1) };
            table.Controls.Add(first, 0, 0);
            table.Controls.Add(second, 1, 0);
            parent.Controls.Add(table);
            parent.PerformLayout();
            table.PerformLayout();

            Assert.Equal(new Size(104, 24), table.PreferredSize);
            Assert.Equal(new Size(104, 24), table.Size);
            Assert.Equal(new Rectangle(3, 3, 20, 10), first.Bounds);
            Assert.Equal(new Rectangle(43, 3, 30, 12), second.Bounds);
            Assert.NotSame(parent.LayoutEngine, table.LayoutEngine);
        });

    [Fact]
    public void FlowLayout_DirectionChangeInvalidatesItsPreferredSizeAndWrapChangeRearranges()
        => OnStaThread(() =>
        {
            using var flow = new FlowLayoutPanel { Size = new Size(40, 90), Padding = new Padding(2), WrapContents = false };
            var first = new Control { Size = new Size(20, 10), Margin = new Padding(1) };
            var second = new Control { Size = new Size(30, 15), Margin = new Padding(1) };
            flow.Controls.AddRange(first, second);

            Assert.Equal(new Size(58, 21), flow.PreferredSize);
            Assert.Equal(new Size(58, 21), flow.PreferredSize);
            flow.FlowDirection = FlowDirection.TopDown;
            Assert.Equal(new Size(36, 33), flow.PreferredSize);
            Assert.Equal(new Point(3, 15), second.Location);

            flow.FlowDirection = FlowDirection.LeftToRight;
            Assert.Equal(new Point(25, 3), second.Location);
            flow.WrapContents = true;
            Assert.Equal(new Point(3, 15), second.Location);
            flow.WrapContents = false;
            Assert.Equal(new Point(25, 3), second.Location);
            Assert.Equal(new Size(58, 21), flow.PreferredSize);
        });

    [Fact]
    public void Form_DefaultLayoutOrdersStripsAndContentByTheControlCollection()
        => OnStaThread(() =>
        {
            using var form = new Form { AutoScaleMode = AutoScaleMode.None, ClientSize = new Size(320, 180) };
            var content = new Panel { Dock = DockStyle.Fill };
            var status = new StatusStrip { AutoSize = false, Height = 18 };
            var tool = new ToolStrip { AutoSize = false, Height = 25 };
            var menu = new MenuStrip { AutoSize = false, Height = 20 };
            form.MainMenuStrip = menu;
            form.Controls.AddRange(content, status, tool, menu);
            form.PerformLayout();

            Assert.Equal(new Rectangle(0, 0, 320, 20), menu.Bounds);
            Assert.Equal(new Rectangle(0, 20, 320, 25), tool.Bounds);
            Assert.Equal(new Rectangle(0, 162, 320, 18), status.Bounds);
            Assert.Equal(new Rectangle(0, 45, 320, 117), content.Bounds);

            tool.Dock = DockStyle.None;
            tool.Bounds = new Rectangle(7, 9, 100, 25);
            form.PerformLayout();
            tool.PerformLayout();
            Assert.Equal(new Rectangle(7, 9, 100, 25), tool.Bounds);
        });

    [Fact]
    public void NestedStrip_UsesItsImmediateParentForDocking()
        => OnStaThread(() =>
        {
            using var form = new Form { ClientSize = new Size(320, 180) };
            var panel = new Panel { Size = new Size(140, 90), Padding = new Padding(3, 5, 7, 11) };
            var strip = new ToolStrip { AutoSize = false, Height = 25 };
            panel.Controls.Add(strip);
            form.Controls.Add(panel);
            form.PerformLayout();
            strip.PerformLayout();

            Assert.Equal(new Rectangle(3, 5, 130, 25), strip.Bounds);
        });

    [Theory]
    [InlineData(Orientation.Vertical)]
    [InlineData(Orientation.Horizontal)]
    public void SplitResize_PreservesTheOriginalRatioAcrossSmallStepsAndRoundTrips(Orientation orientation)
        => OnStaThread(() =>
        {
            using var split = new SplitContainer
            {
                Orientation = orientation,
                Size = new Size(240, 240),
                SplitterDistance = 90,
            };

            SetPrimarySize(split, 241);
            Assert.Equal(90, split.SplitterDistance);
            SetPrimarySize(split, 242);
            Assert.Equal(90, split.SplitterDistance);
            SetPrimarySize(split, 260);
            Assert.Equal(97, split.SplitterDistance);
            Assert.Equal(orientation == Orientation.Vertical ? 101 : 0, split.Panel2.Left);
            Assert.Equal(orientation == Orientation.Horizontal ? 101 : 0, split.Panel2.Top);
            SetPrimarySize(split, 240);
            Assert.Equal(90, split.SplitterDistance);
        });

    private static void SetPrimarySize(SplitContainer split, int size)
    {
        if (split.Orientation == Orientation.Vertical)
        {
            split.Width = size;
        }
        else
        {
            split.Height = size;
        }
    }

    private static void OnStaThread(Action action)
    {
        ExceptionDispatchInfo? failure = null;
        var thread = new Thread(() =>
        {
            try { action(); }
            catch (Exception exception) { failure = ExceptionDispatchInfo.Capture(exception); }
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(30)), "The container default-layout test did not finish.");
        failure?.Throw();
    }
}
