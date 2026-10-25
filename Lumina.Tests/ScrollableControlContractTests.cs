using System.Drawing;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public sealed class ScrollableControlContractTests
{
    [Fact]
    public void ScrollStateBits_TrackAutoScrollAndVisibleRanges()
    {
        using var control = new ProbeScrollableControl { ClientSize = new Size(100, 80) };

        Assert.False(control.ReadState(ProbeScrollableControl.AutoScrollingBit));
        Assert.False(control.ExposedHScroll);
        Assert.False(control.ExposedVScroll);

        control.AutoScrollMinSize = new Size(240, 160);
        control.AutoScroll = true;

        Assert.True(control.ReadState(ProbeScrollableControl.AutoScrollingBit));
        Assert.True(control.ExposedHScroll);
        Assert.True(control.ExposedVScroll);

        control.ExposeAdjustFormScrollbars(false);
        Assert.False(control.ExposedHScroll);
        Assert.False(control.ExposedVScroll);

        control.ExposeAdjustFormScrollbars(true);
        Assert.True(control.ExposedHScroll);
        Assert.True(control.ExposedVScroll);
    }

    [Fact]
    public void SetDisplayRectLocation_UsesNegativeDisplayCoordinates()
    {
        using var control = new ProbeScrollableControl
        {
            ClientSize = new Size(100, 80),
            AutoScroll = true,
            AutoScrollMinSize = new Size(240, 160),
        };

        control.ExposeSetDisplayRectLocation(-40, -25);

        Assert.Equal(new Point(-40, -25), control.AutoScrollPosition);
        Assert.Equal(new Point(-40, -25), control.DisplayRectangle.Location);
        Assert.True(control.ReadState(ProbeScrollableControl.UserScrolledBit));
    }

    [Fact]
    public void ProtectedStateBits_CanBeSetAndClearedByDerivedControls()
    {
        using var control = new ProbeScrollableControl();

        control.ExposeSetState(ProbeScrollableControl.FullDragBit, true);
        Assert.True(control.ReadState(ProbeScrollableControl.FullDragBit));

        control.ExposeSetState(ProbeScrollableControl.FullDragBit, false);
        Assert.False(control.ReadState(ProbeScrollableControl.FullDragBit));
    }

    private sealed class ProbeScrollableControl : ScrollableControl
    {
        internal const int AutoScrollingBit = 1;
        internal const int FullDragBit = 16;
        internal const int UserScrolledBit = 8;

        internal bool ExposedHScroll => HScroll;
        internal bool ExposedVScroll => VScroll;

        internal bool ReadState(int bit) => GetScrollState(bit);

        internal void ExposeAdjustFormScrollbars(bool displayScrollbars)
            => AdjustFormScrollbars(displayScrollbars);

        internal void ExposeSetDisplayRectLocation(int x, int y)
            => SetDisplayRectLocation(x, y);

        internal void ExposeSetState(int bit, bool value)
            => SetScrollState(bit, value);
    }
}
