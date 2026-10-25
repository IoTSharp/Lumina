using System.Drawing;
using System.Runtime.ExceptionServices;
using System.Threading;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public sealed class ScrollableControlFoundationTests
{
    [Fact]
    public void AutoScrollPosition_UsesPositiveSetterAndNegativeGetter()
        => OnStaThread(() =>
        {
            using var control = new ScrollableControl
            {
                ClientSize = new Size(100, 80),
                Padding = new Padding(2),
                AutoScroll = true,
                AutoScrollMinSize = new Size(300, 200),
            };

            Assert.Equal(new Rectangle(2, 2, 300, 200), control.DisplayRectangle);
            Assert.Equal(204, control.HorizontalScroll.Maximum);
            Assert.Equal(124, control.VerticalScroll.Maximum);

            control.AutoScrollPosition = new Point(40, 30);

            Assert.Equal(new Point(-40, -30), control.AutoScrollPosition);
            Assert.Equal(new Point(-38, -28), control.DisplayRectangle.Location);
            Assert.Equal(40, control.HorizontalScroll.Value);
            Assert.Equal(30, control.VerticalScroll.Value);
        });

    [Fact]
    public void AutoScrollMargin_AndChildBoundsExpandTheLogicalContentArea()
        => OnStaThread(() =>
        {
            using var control = new ScrollableControl
            {
                ClientSize = new Size(100, 80),
                Padding = new Padding(2),
                AutoScroll = true,
                AutoScrollMargin = new Size(10, 20),
            };
            using var child = new Control { Bounds = new Rectangle(2, 3, 120, 50) };
            control.Controls.Add(child);

            Assert.Equal(new Size(140, 90), control.DisplayRectangle.Size);
            Assert.Equal(44, control.HorizontalScroll.Maximum);
            Assert.Equal(14, control.VerticalScroll.Maximum);

            child.Width = 160;
            Assert.Equal(new Size(180, 90), control.DisplayRectangle.Size);
            Assert.Equal(84, control.HorizontalScroll.Maximum);
        });

    [Fact]
    public void DisablingAutoScroll_ResetsOffsetAndRestoresPaddedViewport()
        => OnStaThread(() =>
        {
            using var control = new ScrollableControl
            {
                ClientSize = new Size(100, 80),
                Padding = new Padding(3, 4, 5, 6),
                AutoScroll = true,
                AutoScrollMinSize = new Size(300, 200),
            };
            control.AutoScrollPosition = new Point(20, 10);
            control.AutoScroll = false;

            Assert.Equal(Point.Empty, control.AutoScrollPosition);
            Assert.Equal(new Rectangle(3, 4, 92, 70), control.DisplayRectangle);
            Assert.Equal(0, control.HorizontalScroll.Value);
            Assert.Equal(0, control.VerticalScroll.Value);
        });

    [Fact]
    public void ScrollProperties_ValueAndRangeStayLinkedToTheOwner()
        => OnStaThread(() =>
        {
            using var control = new ScrollableControl
            {
                ClientSize = new Size(100, 80),
                AutoScroll = true,
                AutoScrollMinSize = new Size(240, 160),
            };

            control.HorizontalScroll.Value = 25;
            control.VerticalScroll.Value = 15;

            Assert.Equal(new Point(-25, -15), control.AutoScrollPosition);
            Assert.Equal(0, control.HorizontalScroll.OldValue);
            Assert.Equal(0, control.VerticalScroll.OldValue);
            Assert.True(control.HorizontalScroll.IsEnabled);
            Assert.True(control.VerticalScroll.IsEnabled);
        });

    [Fact]
    public void Scroll_RaisesOneNotificationPerChangedAxis()
        => OnStaThread(() =>
        {
            using var control = new ScrollableControl
            {
                ClientSize = new Size(100, 80),
                AutoScroll = true,
                AutoScrollMinSize = new Size(300, 200),
            };
            var events = new List<ScrollEventArgs>();
            control.Scroll += (_, args) => events.Add(args);

            control.AutoScrollPosition = new Point(20, 10);

            Assert.Collection(
                events,
                horizontal =>
                {
                    Assert.Equal(ScrollOrientation.Horizontal, horizontal.ScrollOrientation);
                    Assert.Equal(0, horizontal.OldValue);
                    Assert.Equal(20, horizontal.NewValue);
                    Assert.Equal(ScrollEventType.ThumbPosition, horizontal.Type);
                },
                vertical =>
                {
                    Assert.Equal(ScrollOrientation.Vertical, vertical.ScrollOrientation);
                    Assert.Equal(0, vertical.OldValue);
                    Assert.Equal(10, vertical.NewValue);
                });
        });

    [Fact]
    public void ScrollControlIntoView_ClampsToTheScrollableContent()
        => OnStaThread(() =>
        {
            using var control = new ScrollableControl
            {
                ClientSize = new Size(100, 80),
                AutoScroll = true,
            };
            using var child = new Control { Bounds = new Rectangle(2, 2, 180, 100) };
            control.Controls.Add(child);

            control.ScrollControlIntoView(child);

            Assert.Equal(new Point(-control.HorizontalScroll.Maximum, -control.VerticalScroll.Maximum), control.AutoScrollPosition);
            Assert.Equal(control.HorizontalScroll.Maximum, control.HorizontalScroll.Value);
            Assert.Equal(control.VerticalScroll.Maximum, control.VerticalScroll.Value);
        });

    [Fact]
    public void InvalidScrollSizes_AreRejected()
        => OnStaThread(() =>
        {
            using var control = new ScrollableControl();
            Assert.Throws<ArgumentOutOfRangeException>(() => control.AutoScrollMargin = new Size(-1, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => control.AutoScrollMinSize = new Size(0, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => control.SetAutoScrollMargin(-1, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => control.SetAutoScrollMargin(0, -1));
        });

    private static void OnStaThread(Action action)
    {
        Exception? failure = null;
        Thread thread = new(() =>
        {
            try { action(); }
            catch (Exception exception) { failure = exception; }
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        if (!thread.Join(TimeSpan.FromSeconds(10)))
        {
            throw new TimeoutException("ScrollableControl test thread did not finish within 10 seconds.");
        }
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }
}
