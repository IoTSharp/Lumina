using System.ComponentModel;
using System.Drawing;
using System.Runtime.ExceptionServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public sealed class AutoSizeModeIdentityTests
{
    [Fact]
    public void NumericValuesMatchTheWinFormsContract()
    {
        Assert.Equal(0, (int)AutoSizeMode.GrowAndShrink);
        Assert.Equal(1, (int)AutoSizeMode.GrowOnly);
        Assert.Equal(AutoSizeMode.GrowAndShrink, default);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void ControlAndContainerDefaultsPreserveGrowOnlyByName(int controlKind)
        => OnSta(() =>
        {
            using Control control = controlKind switch
            {
                0 => new Control(),
                1 => new Panel(),
                2 => new ContainerControl(),
                _ => new UserControl()
            };

            Assert.Equal(AutoSizeMode.GrowOnly, control.AutoSizeMode);
            Assert.Equal(1, (int)control.AutoSizeMode);
            Assert.False(control.AutoSize);
            Assert.False(control.IsHandleCreated);
        });

    [Fact]
    public void UserControlDesignerDefaultUsesTheCorrectedGrowOnlyIdentity()
        => OnSta(() =>
        {
            using var control = new UserControl();
            PropertyDescriptor property = TypeDescriptor.GetProperties(control)[nameof(UserControl.AutoSizeMode)]!;
            var attribute = Assert.IsType<DefaultValueAttribute>(property.Attributes[typeof(DefaultValueAttribute)]);

            Assert.Equal((AutoSizeMode)1, Assert.IsType<AutoSizeMode>(attribute.Value));
            Assert.False(property.ShouldSerializeValue(control));
            control.AutoSizeMode = (AutoSizeMode)0;
            Assert.True(property.ShouldSerializeValue(control));
            Assert.False(control.IsHandleCreated);
        });

    [Theory]
    [InlineData(0, 20, 10)]
    [InlineData(1, 60, 50)]
    public void NumericControlModesSelectShrinkOrGrowthOnlyLayout(int numericMode, int width, int height)
        => OnSta(() =>
        {
            using var parent = new Control { Size = new Size(200, 120) };
            using var child = new PreferredControl
            {
                Size = new Size(60, 50),
                Desired = new Size(20, 10),
                AutoSize = true,
                AutoSizeMode = (AutoSizeMode)numericMode
            };

            parent.Controls.Add(child);
            parent.PerformLayout();

            Assert.Equal(new Size(width, height), child.Size);
            Assert.False(parent.IsHandleCreated);
            Assert.False(child.IsHandleCreated);
        });

    [Theory]
    [InlineData(0, 40, 32)]
    [InlineData(1, 100, 80)]
    public void NumericUserControlModesConsumeTheChildPreferredExtent(int numericMode, int width, int height)
        => OnSta(() =>
        {
            using var parent = new Panel { Size = new Size(300, 200) };
            using var control = new UserControl
            {
                Size = new Size(100, 80),
                AutoSizeMode = (AutoSizeMode)numericMode,
                AutoSize = true
            };
            using var content = new Control { Bounds = new Rectangle(10, 12, 30, 20), Margin = Padding.Empty };
            control.Controls.Add(content);
            parent.Controls.Add(control);
            parent.PerformLayout();

            Assert.Equal(new Size(40, 32), control.PreferredSize);
            Assert.Equal(new Size(width, height), control.Size);
            Assert.False(parent.IsHandleCreated);
            Assert.False(control.IsHandleCreated);
            Assert.False(content.IsHandleCreated);
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(15)), "The AutoSizeMode identity check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class PreferredControl : Control
    {
        internal Size Desired { get; init; }
        public override Size GetPreferredSize(Size proposedSize) => Desired;
    }
}
