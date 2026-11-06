using System.ComponentModel;
using System.Drawing;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public sealed class UserControlDefaultsTests
{
    [Fact]
    public void DefaultsUseTheStandardSizeAndGrowOnlyModeWithoutCreatingAHandle()
    {
        using var control = new Probe();
        Assert.Equal(new Size(150, 150), control.Size);
        Assert.Equal(new Size(150, 150), control.ProtectedDefaultSize);
        Assert.Equal(AutoSizeMode.GrowOnly, control.AutoSizeMode);
        Assert.Equal(BorderStyle.None, control.BorderStyle);
        Assert.False(control.AutoSize);
        Assert.False(control.IsHandleCreated);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(3)]
    public void BorderStyleRejectsInvalidValuesWithoutChangingState(int value)
    {
        using var control = new Probe { BorderStyle = BorderStyle.FixedSingle };
        InvalidEnumArgumentException exception = Assert.Throws<InvalidEnumArgumentException>(
            () => control.BorderStyle = (BorderStyle)value);
        Assert.Equal("value", exception.ParamName);
        Assert.Equal(BorderStyle.FixedSingle, control.BorderStyle);
        Assert.False(control.IsHandleCreated);
    }

    [Theory]
    [InlineData(BorderStyle.None, 0, 0)]
    [InlineData(BorderStyle.FixedSingle, 0x00800000, 0)]
    [InlineData(BorderStyle.Fixed3D, 0, 0x00000200)]
    public void BorderStyleChangesCreationParametersWithoutCreatingAHandle(BorderStyle style, int border, int edge)
    {
        using var control = new Probe { BorderStyle = style };
        Assert.Equal(border, control.Parameters.Style & 0x00800000);
        Assert.Equal(edge, control.Parameters.ExStyle & 0x00000200);
        Assert.NotEqual(0, control.Parameters.ExStyle & 0x00010000);
        Assert.False(control.IsHandleCreated);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(2)]
    public void AutoSizeModeRejectsInvalidValuesWithoutChangingState(int value)
    {
        using var control = new UserControl();
        Assert.Throws<InvalidEnumArgumentException>(() => control.AutoSizeMode = (AutoSizeMode)value);
        Assert.Equal(AutoSizeMode.GrowOnly, control.AutoSizeMode);
        Assert.False(control.IsHandleCreated);
    }

    [Fact]
    public void AutoSizeModeRequestsParentLayoutOnceWithTheStandardAutoSizeReason()
    {
        using var parent = new Panel();
        using var control = new UserControl();
        parent.Controls.Add(control);
        var changes = new List<LayoutEventArgs>();
        parent.Layout += (_, e) => changes.Add(e);

        control.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        LayoutEventArgs change = Assert.Single(changes);
        Assert.Same(control, change.AffectedControl);
        Assert.Equal(nameof(Control.AutoSize), change.AffectedProperty);
        control.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        Assert.Single(changes);
        Assert.False(control.IsHandleCreated);
    }

    [Fact]
    public void BorderStyleUpdatesAnExistingNativeWindowWithoutRecreatingIt()
        => OnSta(() =>
        {
            using var form = new Form();
            using var control = new UserControl();
            form.Controls.Add(control);
            form.Show();
            nint handle = control.Handle;

            control.BorderStyle = BorderStyle.FixedSingle;
            Assert.Equal(handle, control.Handle);
            Assert.NotEqual(0, ReadStyle(handle, -16) & 0x00800000);
            Assert.Equal(0, ReadStyle(handle, -20) & 0x00000200);

            control.BorderStyle = BorderStyle.Fixed3D;
            Assert.Equal(handle, control.Handle);
            Assert.Equal(0, ReadStyle(handle, -16) & 0x00800000);
            Assert.NotEqual(0, ReadStyle(handle, -20) & 0x00000200);

            control.BorderStyle = BorderStyle.None;
            Assert.Equal(handle, control.Handle);
            Assert.Equal(0, ReadStyle(handle, -16) & 0x00800000);
            Assert.Equal(0, ReadStyle(handle, -20) & 0x00000200);
        });

    private static int ReadStyle(nint handle, int index)
        => unchecked((int)(nuint)GetWindowLongPtrW(handle, index));

    [DllImport("user32.dll", EntryPoint = "GetWindowLongPtrW", SetLastError = true)]
    private static extern nint GetWindowLongPtrW(nint handle, int index);

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
        Assert.True(thread.Join(TimeSpan.FromSeconds(15)), "UserControl native style test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class Probe : UserControl
    {
        internal Size ProtectedDefaultSize => DefaultSize;
        internal CreateParams Parameters => CreateParams;
    }
}
