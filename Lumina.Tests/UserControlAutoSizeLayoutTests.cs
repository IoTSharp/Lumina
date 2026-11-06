using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public sealed class UserControlAutoSizeLayoutTests
{
    [Fact]
    public void AutoSizeIsExposedForDesignerSerialization()
    {
        using var control = new UserControl();
        PropertyDescriptor property = TypeDescriptor.GetProperties(control)[nameof(UserControl.AutoSize)]!;

        Assert.True(property.IsBrowsable);
        Assert.False(property.IsReadOnly);
        Assert.Equal(EditorBrowsableState.Always,
            Assert.IsType<EditorBrowsableAttribute>(property.Attributes[typeof(EditorBrowsableAttribute)]).State);
        Assert.Equal(DesignerSerializationVisibility.Visible, property.SerializationVisibility);
        Assert.False((bool)property.GetValue(control)!);
        Assert.False(control.IsHandleCreated);
    }

    [Fact]
    public void UserControlDeclaresTheStandardOverrideAndReexposedEvent()
    {
        PropertyInfo property = typeof(UserControl).GetProperty(nameof(UserControl.AutoSize),
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)!;
        EventInfo changed = typeof(UserControl).GetEvent(nameof(UserControl.AutoSizeChanged),
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)!;
        EventDescriptor descriptor = TypeDescriptor.GetEvents(typeof(UserControl))[nameof(UserControl.AutoSizeChanged)]!;

        Assert.NotNull(property);
        Assert.Equal(typeof(Control), property.GetMethod!.GetBaseDefinition().DeclaringType);
        Assert.Equal(typeof(Control), property.SetMethod!.GetBaseDefinition().DeclaringType);
        Assert.NotNull(changed);
        Assert.Equal(typeof(EventHandler), changed.EventHandlerType);
        Assert.True(descriptor.IsBrowsable);
        Assert.Equal(EditorBrowsableState.Always,
            Assert.IsType<EditorBrowsableAttribute>(descriptor.Attributes[typeof(EditorBrowsableAttribute)]).State);
    }

    [Fact]
    public void BaseAndUserControlSubscriptionsShareOneNotificationSource()
    {
        using var control = new UserControl();
        Control baseReference = control;
        int calls = 0;
        EventHandler handler = (sender, args) =>
        {
            Assert.Same(control, sender);
            Assert.Same(EventArgs.Empty, args);
            calls++;
        };

        control.AutoSizeChanged += handler;
        baseReference.AutoSize = true;
        control.AutoSize = true;
        Assert.Equal(1, calls);

        baseReference.AutoSizeChanged -= handler;
        control.AutoSize = false;
        Assert.Equal(1, calls);

        baseReference.AutoSizeChanged += handler;
        control.AutoSize = true;
        Assert.Equal(2, calls);

        control.AutoSizeChanged -= handler;
        baseReference.AutoSize = false;
        Assert.Equal(2, calls);
        Assert.False(control.IsHandleCreated);
    }

    [Fact]
    public void BaseReferenceContinuesToDispatchToDerivedAutoSizeSetter()
    {
        using var control = new DerivedUserControl();
        Control baseReference = control;
        int changes = 0;
        control.AutoSizeChanged += (_, _) => changes++;

        baseReference.AutoSize = true;
        baseReference.AutoSize = true;

        Assert.Equal(2, control.SetterCalls);
        Assert.Equal(1, changes);
        Assert.True(control.AutoSize);
        Assert.False(control.IsHandleCreated);
    }

    [Fact]
    public void AutoSizeChangedObservesTheCompletedParentLayout()
    {
        using var parent = new Panel { Size = new Size(300, 200) };
        using var control = new UserControl
        {
            Size = new Size(100, 80),
            AutoSizeMode = AutoSizeMode.GrowAndShrink
        };
        using var content = new Control
        {
            Bounds = new Rectangle(10, 12, 30, 20),
            Margin = Padding.Empty
        };
        control.Controls.Add(content);
        parent.Controls.Add(control);
        var order = new List<string>();
        parent.Layout += (_, _) => order.Add("parent-layout");
        control.AutoSizeChanged += (_, _) =>
        {
            order.Add("autosize-changed");
            Assert.True(control.AutoSize);
            Assert.Equal(new Size(40, 32), control.PreferredSize);
            Assert.Equal(new Size(40, 32), control.Size);
        };

        control.AutoSize = true;

        Assert.Contains("parent-layout", order);
        Assert.Equal("autosize-changed", order[^1]);
        Assert.False(parent.IsHandleCreated);
        Assert.False(control.IsHandleCreated);
        Assert.False(content.IsHandleCreated);
    }

    [Fact]
    public void AutoSizeHandlerExceptionsPropagateAfterTheValueCommits()
    {
        using var control = new UserControl();
        var failure = new InvalidOperationException("AutoSizeChanged failure");
        EventHandler handler = (_, _) => throw failure;
        control.AutoSizeChanged += handler;

        Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => control.AutoSize = true));
        Assert.True(control.AutoSize);
        control.AutoSize = true;

        control.AutoSizeChanged -= handler;
        control.AutoSize = false;
        Assert.False(control.AutoSize);
        Assert.False(control.IsHandleCreated);
    }

    private sealed class DerivedUserControl : UserControl
    {
        internal int SetterCalls { get; private set; }

        public override bool AutoSize
        {
            get => base.AutoSize;
            set
            {
                SetterCalls++;
                base.AutoSize = value;
            }
        }
    }
}
