using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public sealed class ScrollableControlWin32ScrollTests
{
    [Fact]
    public void WndProc_MapsLinePageThumbAndEndCommandsToManagedScrollEvents()
        => OnSta(() =>
        {
            using var control = new ProbeScrollableControl
            {
                ClientSize = new Size(100, 80),
                AutoScroll = true,
                AutoScrollMinSize = new Size(400, 300),
            };
            var events = new List<ScrollEventArgs>();
            control.Scroll += (_, args) => events.Add(args);

            control.Dispatch(WmHScroll, SbLineRight);
            Assert.Equal(1, control.HorizontalScroll.Value);
            Assert.Equal(ScrollEventType.SmallIncrement, Assert.Single(events).Type);

            events.Clear();
            control.Dispatch(WmVScroll, SbPageDown);
            Assert.Equal(80, control.VerticalScroll.Value);
            Assert.Equal(ScrollEventType.LargeIncrement, Assert.Single(events).Type);

            events.Clear();
            control.Dispatch(WmHScroll, SbThumbPosition, trackPosition: 125);
            Assert.Equal(125, control.HorizontalScroll.Value);
            Assert.Equal(ScrollEventType.ThumbPosition, Assert.Single(events).Type);

            events.Clear();
            control.Dispatch(WmHScroll, SbEndScroll);
            var end = Assert.Single(events);
            Assert.Equal(ScrollEventType.EndScroll, end.Type);
            Assert.Equal(end.OldValue, end.NewValue);
        });

    [Fact]
    public void NativeScrollMessages_UpdateStylesPositionAndSurviveHandleRecreation()
        => OnSta(() =>
        {
            using var form = new Form { ClientSize = new Size(220, 160) };
            using var control = new ProbeScrollableControl
            {
                Size = new Size(100, 80),
                AutoScroll = true,
                AutoScrollMinSize = new Size(400, 300),
            };
            form.Controls.Add(control);
            form.Show();

            Assert.True(control.IsHandleCreated);
            int style = GetWindowLongPtr(control.Handle, -16);
            Assert.NotEqual(0, style & WsHScroll);
            Assert.NotEqual(0, style & WsVScroll);

            _ = SendMessageW(control.Handle, WmHScroll, SbLineRight, 0);
            _ = SendMessageW(control.Handle, WmVScroll, SbLineDown, 0);
            Assert.Equal(new Point(-1, -1), control.AutoScrollPosition);

            control.RebuildNativeHandle();
            Assert.True(control.IsHandleCreated);
            Assert.Equal(new Point(-1, -1), control.AutoScrollPosition);

            _ = SendMessageW(control.Handle, WmHScroll, SbLeft, 0);
            _ = SendMessageW(control.Handle, WmVScroll, SbTop, 0);
            Assert.Equal(Point.Empty, control.AutoScrollPosition);
        });

    [Fact]
    public void NativeScrollMessage_OnDisposedControlDoesNotReenterManagedState()
        => OnSta(() =>
        {
            var control = new ProbeScrollableControl
            {
                ClientSize = new Size(100, 80),
                AutoScroll = true,
                AutoScrollMinSize = new Size(400, 300),
            };
            control.Dispose();

            control.Dispatch(WmHScroll, SbLineRight);

            Assert.Equal(Point.Empty, control.AutoScrollPosition);
        });

    private const int WmHScroll = 0x0114;
    private const int WmVScroll = 0x0115;
    private const int SbLineRight = 1;
    private const int SbLineDown = 1;
    private const int SbPageDown = 3;
    private const int SbThumbPosition = 4;
    private const int SbLeft = 6;
    private const int SbTop = 6;
    private const int SbEndScroll = 8;
    private const int WsHScroll = 0x00100000;
    private const int WsVScroll = 0x00200000;

    private static int GetWindowLongPtr(nint handle, int index)
        => unchecked((int)(nuint)GetWindowLongPtrW(handle, index));

    private static void OnSta(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            try { action(); }
            catch (Exception exception) { failure = exception; }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(15)), "ScrollableControl Win32 test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    [DllImport("user32.dll", EntryPoint = "GetWindowLongPtrW", SetLastError = true)]
    private static extern nint GetWindowLongPtrW(nint handle, int index);

    [DllImport("user32.dll", EntryPoint = "SendMessageW", SetLastError = true)]
    private static extern nint SendMessageW(nint handle, int message, nint wParam, nint lParam);

    private sealed class ProbeScrollableControl : ScrollableControl
    {
        internal void Dispatch(int message, int command, int trackPosition = 0)
        {
            nint wParam = (nint)(uint)((trackPosition << 16) | (command & 0xFFFF));
            Message managed = Message.Create(NativeHandleForTest, message, wParam, 0);
            WndProc(ref managed);
        }

        internal void RebuildNativeHandle() => RecreateHandle();

        private nint NativeHandleForTest => IsHandleCreated ? Handle : 0;
    }
}
