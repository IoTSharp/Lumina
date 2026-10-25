using System.Runtime.InteropServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public sealed class ControlRightToLeftNativeTests
{
    private const int GwlExStyle = -20;
    private const int WsExLayoutRtl = 0x00400000;
    private const int WsExRtlReading = 0x00002000;
    private const int RtlMask = WsExLayoutRtl | WsExRtlReading;

    [Fact]
    public void EffectiveCreateParamsUseTheInheritedRightToLeftStyleWithoutCreatingAHandle()
        => OnSta(() =>
        {
            using var parent = new ProbeControl { RightToLeft = RightToLeft.Yes };
            using var child = new ProbeControl { Parent = parent };

            Assert.False(child.IsHandleCreated);
            Assert.Equal(RtlMask, child.EffectiveExStyle & RtlMask);

            child.RightToLeft = RightToLeft.No;
            Assert.Equal(0, child.EffectiveExStyle & RtlMask);
            Assert.False(child.IsHandleCreated);

            child.ResetRightToLeft();
            Assert.Equal(RtlMask, child.EffectiveExStyle & RtlMask);
            Assert.False(child.IsHandleCreated);
        });

    [Fact]
    public void NativeStyleTracksExplicitOverrideAndSurvivesHandleRecreation()
        => OnSta(() =>
        {
            using var form = new Form { RightToLeft = RightToLeft.Yes };
            using var child = new ProbeControl();
            form.Controls.Add(child);
            form.Show();

            Assert.True(child.IsHandleCreated);
            Assert.Equal(RtlMask, ReadExStyle(child.Handle) & RtlMask);

            child.RightToLeft = RightToLeft.No;
            Assert.Equal(0, ReadExStyle(child.Handle) & RtlMask);

            child.RebuildNativeHandle();
            Assert.True(child.IsHandleCreated);
            int rebuiltStyle = ReadExStyle(child.Handle);
            Assert.True((rebuiltStyle & RtlMask) == 0,
                $"rebuilt native exstyle=0x{rebuiltStyle:X8}, managed CreateParams=0x{child.EffectiveExStyle:X8}");

            child.ResetRightToLeft();
            Assert.Equal(RtlMask, ReadExStyle(child.Handle) & RtlMask);
        });

    private static int ReadExStyle(nint handle)
        => unchecked((int)(nuint)GetWindowLongPtrW(handle, GwlExStyle));

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
        Assert.True(thread.Join(TimeSpan.FromSeconds(15)), "RightToLeft native test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    [DllImport("user32.dll", EntryPoint = "GetWindowLongPtrW", SetLastError = true)]
    private static extern nint GetWindowLongPtrW(nint handle, int index);

    private sealed class ProbeControl : Control
    {
        internal int EffectiveExStyle => CreateParams.ExStyle;

        internal void RebuildNativeHandle() => RecreateHandle();
    }
}
