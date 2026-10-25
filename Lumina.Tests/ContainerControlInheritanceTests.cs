using System.Drawing;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public class ContainerControlInheritanceTests
{
    [Fact]
    public void ParentForm_ResolvesTheCurrentParentChainAfterReparenting()
    {
        using var first = new Form();
        using var second = new Form();
        using var container = new ContainerControl();

        first.Controls.Add(container);
        Assert.Same(first, container.ParentForm);

        second.Controls.Add(container);
        Assert.Same(second, container.ParentForm);

        second.Controls.Remove(container);
        Assert.Null(container.ParentForm);
    }

    [Fact]
    public void AutoScaleFactor_UsesANeutralFactorWithoutADesignBaseline()
    {
        using var container = new ScaleProbe
        {
            AutoScaleMode = AutoScaleMode.None,
        };

        Assert.Equal(new SizeF(1f, 1f), container.ExposedAutoScaleFactor);
    }

    [Fact]
    public void AutoScaleFactor_ReportsTheCurrentToDesignRatio()
    {
        using var container = new ScaleProbe
        {
            AutoScaleMode = AutoScaleMode.Dpi,
        };

        SizeF current = container.CurrentAutoScaleDimensions;
        Assert.True(current.Width > 0f);
        Assert.True(current.Height > 0f);

        container.AutoScaleDimensions = new SizeF(current.Width / 2f, current.Height / 2f);

        Assert.Equal(2f, container.ExposedAutoScaleFactor.Width, precision: 3);
        Assert.Equal(2f, container.ExposedAutoScaleFactor.Height, precision: 3);
    }

    [Fact]
    public void AutoScaleFactor_UsesANeutralAxisWhenOnlyOneDesignDimensionIsMissing()
    {
        using var container = new ScaleProbe
        {
            AutoScaleMode = AutoScaleMode.Dpi,
            AutoScaleDimensions = new SizeF(96f, 0f),
        };

        SizeF factor = container.ExposedAutoScaleFactor;

        Assert.True(factor.Width > 0f);
        Assert.Equal(1f, factor.Height);
    }

    private sealed class ScaleProbe : ContainerControl
    {
        public SizeF ExposedAutoScaleFactor => AutoScaleFactor;
    }
}
