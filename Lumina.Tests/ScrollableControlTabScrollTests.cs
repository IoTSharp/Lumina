using System.Drawing;
using System.Runtime.ExceptionServices;
using System.Threading;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public sealed class ScrollableControlTabScrollTests
{
    [Fact]
    public void ScrollControlIntoView_RevealsDescendantAboveAndLeftOfCurrentViewport()
        => OnStaThread(() =>
        {
            using var control = new ScrollableControl
            {
                ClientSize = new Size(100, 80),
                AutoScroll = true,
                AutoScrollMinSize = new Size(300, 200),
            };
            using var child = new Control { Bounds = new Rectangle(40, 30, 30, 20) };
            control.Controls.Add(child);

            control.AutoScrollPosition = new Point(50, 40);
            control.ScrollControlIntoView(child);

            Assert.Equal(new Point(-40, -30), control.AutoScrollPosition);
        });

    [Fact]
    public void ScrollControlIntoView_UsesNestedContentCoordinatesAndAutoScrollOffset()
        => OnStaThread(() =>
        {
            using var control = new ScrollableControl
            {
                ClientSize = new Size(100, 80),
                AutoScroll = true,
                AutoScrollMinSize = new Size(400, 300),
            };
            using var nested = new Control { Bounds = new Rectangle(30, 20, 100, 100) };
            using var child = new Control
            {
                Bounds = new Rectangle(40, 30, 20, 20),
                AutoScrollOffset = new Point(15, 10),
            };
            nested.Controls.Add(child);
            control.Controls.Add(nested);

            control.AutoScrollPosition = new Point(100, 80);
            control.ScrollControlIntoView(child);

            // The effective content location is (85, 60), so the existing
            // offset must be reduced to (85, 60) to put its top-left in view.
            Assert.Equal(new Point(-85, -60), control.AutoScrollPosition);
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
            throw new TimeoutException("ScrollableControl tab-scroll test thread did not finish within 10 seconds.");
        }
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }
}
