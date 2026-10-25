using System.Drawing;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public sealed class UserControlInheritanceTests
{
    [Fact]
    public void UserControl_UsesTheStandardContainerControlInheritanceChain()
    {
        Assert.Equal(typeof(ContainerControl), typeof(UserControl).BaseType);

        using var control = new UserControlProbe();
        Assert.IsAssignableFrom<ScrollableControl>(control);
        Assert.Equal("STATIC", control.NativeClassName);
        Assert.Equal(Win32Style.Child | Win32Style.Visible | Win32Style.ClipChildren | Win32Style.ClipSiblings,
            control.NativeStyle & Win32Style.ChildStyleMask);
    }

    [Fact]
    public void UserControl_InheritsActiveValidationAndScalingContracts()
    {
        using var control = new UserControl();
        using var child = new Control();
        control.Controls.Add(child);

        control.ActiveControl = child;
        Assert.Same(child, control.ActiveControl);

        control.AutoValidate = AutoValidate.EnableAllowFocusChange;
        Assert.Equal(AutoValidate.EnableAllowFocusChange, control.AutoValidate);

        control.AutoScaleMode = AutoScaleMode.Dpi;
        control.AutoScaleDimensions = new SizeF(96f, 96f);
        Assert.Equal(AutoScaleMode.Dpi, control.AutoScaleMode);
        Assert.Equal(new SizeF(96f, 96f), control.AutoScaleDimensions);
    }

    [Fact]
    public void UserControl_ChildOwnershipAndDisposeFollowContainerControl()
    {
        var control = new UserControl();
        var child = new Control();
        control.Controls.Add(child);

        Assert.Same(control, child.Parent);
        control.Dispose();

        Assert.True(control.IsDisposed);
        Assert.True(child.IsDisposed);
    }

    private sealed class UserControlProbe : UserControl
    {
        internal string NativeClassName => ClassName;

        internal uint NativeStyle => Style;
    }

    private static class Win32Style
    {
        internal const uint Child = 0x40000000;
        internal const uint Visible = 0x10000000;
        internal const uint ClipChildren = 0x02000000;
        internal const uint ClipSiblings = 0x04000000;
        internal const uint ChildStyleMask = Child | Visible | ClipChildren | ClipSiblings;
    }
}
