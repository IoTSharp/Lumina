using System.ComponentModel;
using System.Runtime.ExceptionServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public sealed class ControlInheritedPropertiesTests
{
    [Fact]
    public void RightToLeft_DefaultAndExplicitValues_PreserveAmbientContract()
        => OnSta(() =>
        {
            using var control = new ExposedControl();
            Assert.Equal(RightToLeft.Inherit, control.RightToLeft);
            Assert.False(control.EffectiveRightToLeft);

            int changes = 0;
            control.RightToLeftChanged += (_, _) => changes++;
            control.RightToLeft = RightToLeft.Yes;
            Assert.Equal(RightToLeft.Yes, control.RightToLeft);
            Assert.True(control.EffectiveRightToLeft);
            Assert.Equal(1, changes);

            control.RightToLeft = RightToLeft.Yes;
            Assert.Equal(1, changes);

            control.ResetRightToLeft();
            Assert.Equal(RightToLeft.Inherit, control.RightToLeft);
            Assert.False(control.EffectiveRightToLeft);
            Assert.Equal(2, changes);

            Assert.Throws<InvalidEnumArgumentException>(() => control.RightToLeft = (RightToLeft)3);
            Assert.Equal(RightToLeft.Inherit, control.RightToLeft);
            Assert.Equal(2, changes);
        });

    [Fact]
    public void RightToLeft_ParentChanges_NotifyOnlyInheritingDescendants()
        => OnSta(() =>
        {
            using var parent = new ExposedControl();
            using var child = new ExposedControl(parent, "child");
            using var nested = new ExposedControl(child, "nested");
            var trace = new List<string>();
            parent.RightToLeftChanged += (_, _) => trace.Add("parent");
            child.RightToLeftChanged += (_, _) => trace.Add("child");
            nested.RightToLeftChanged += (_, _) => trace.Add("nested");

            parent.RightToLeft = RightToLeft.Yes;
            Assert.Equal(new[] { "parent", "child", "nested" }, trace);
            Assert.True(parent.EffectiveRightToLeft);
            Assert.True(child.EffectiveRightToLeft);
            Assert.True(nested.EffectiveRightToLeft);

            trace.Clear();
            child.RightToLeft = RightToLeft.No;
            Assert.Equal(new[] { "child", "nested" }, trace);
            Assert.False(child.EffectiveRightToLeft);
            Assert.False(nested.EffectiveRightToLeft);

            trace.Clear();
            parent.RightToLeft = RightToLeft.No;
            Assert.Equal(new[] { "parent" }, trace);
            Assert.False(parent.EffectiveRightToLeft);
            Assert.False(child.EffectiveRightToLeft);
            Assert.False(nested.EffectiveRightToLeft);

            trace.Clear();
            child.ResetRightToLeft();
            Assert.Empty(trace);
            Assert.False(child.EffectiveRightToLeft);
            Assert.False(nested.EffectiveRightToLeft);
        });

    [Fact]
    public void RightToLeft_Reparenting_NotifiesWhenEffectiveDirectionChanges()
        => OnSta(() =>
        {
            using var leftToRight = new ExposedControl { RightToLeft = RightToLeft.No };
            using var rightToLeft = new ExposedControl { RightToLeft = RightToLeft.Yes };
            using var child = new ExposedControl { RightToLeft = RightToLeft.Inherit };
            var changes = 0;
            child.RightToLeftChanged += (_, _) => changes++;

            child.Parent = rightToLeft;
            Assert.True(child.EffectiveRightToLeft);
            Assert.Equal(1, changes);

            child.Parent = leftToRight;
            Assert.False(child.EffectiveRightToLeft);
            Assert.Equal(2, changes);

            using var anotherLeftToRight = new ExposedControl { RightToLeft = RightToLeft.No };
            child.Parent = anotherLeftToRight;
            Assert.False(child.EffectiveRightToLeft);
            Assert.Equal(2, changes);

            child.RightToLeft = RightToLeft.Yes;
            Assert.True(child.EffectiveRightToLeft);
            Assert.Equal(3, changes);
            child.Parent = rightToLeft;
            Assert.True(child.EffectiveRightToLeft);
            Assert.Equal(3, changes);
        });

    private static void OnSta(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            try { action(); }
            catch (Exception exception) { failure = exception; }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(15)), "The RightToLeft inheritance check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class ExposedControl : Control
    {
        internal ExposedControl() { }
        internal ExposedControl(Control parent, string text) : base(parent, text) { }
        internal bool EffectiveRightToLeft => RenderRightToLeft;
    }
}
