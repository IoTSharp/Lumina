using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class ContainerControlTabBoundaryTests
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ProcessTabKey_AtBoundaryDoesNotWrap(bool forward)
    {
        using var container = new ExposedContainer();
        var first = new Control { TabIndex = 0 };
        var last = new Control { TabIndex = 1 };
        container.Controls.AddRange(first, last);
        container.ActiveControl = forward ? last : first;

        Assert.False(container.Tab(forward));
        Assert.Same(forward ? last : first, container.ActiveControl);
        Assert.False(container.IsHandleCreated);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void DialogTab_LeavesNestedContainerAtBoundary(bool forward)
    {
        using var parent = new ExposedContainer();
        var before = new Control { TabIndex = 0 };
        var nested = new ExposedContainer { TabIndex = 1 };
        var after = new Control { TabIndex = 2 };
        var inner = new Control();
        parent.Controls.AddRange(before, nested, after);
        nested.Controls.Add(inner);
        nested.ActiveControl = inner;

        Assert.True(nested.DialogKey(forward ? Keys.Tab : Keys.Shift | Keys.Tab));
        Assert.Same(forward ? after : before, parent.ActiveControl);
        Assert.False(parent.IsHandleCreated);
        Assert.False(nested.IsHandleCreated);
    }

    [Theory]
    [InlineData(Keys.Control | Keys.Tab)]
    [InlineData(Keys.Alt | Keys.Tab)]
    [InlineData(Keys.Control | Keys.Shift | Keys.Tab)]
    public void ModifiedTab_DoesNotUseOrdinaryTabNavigation(Keys keyData)
    {
        using var container = new ExposedContainer();
        var first = new Control { TabIndex = 0 };
        var second = new Control { TabIndex = 1 };
        container.Controls.AddRange(first, second);
        container.ActiveControl = first;

        Assert.False(container.DialogKey(keyData));
        Assert.Same(first, container.ActiveControl);
    }

    [Fact]
    public void EmptyContainer_OffersUnhandledTabToParent()
    {
        using var parent = new DialogKeyParent();
        var nested = new ExposedContainer();
        parent.Controls.Add(nested);

        Assert.True(nested.DialogKey(Keys.Tab));
        Assert.Equal(Keys.Tab, parent.LastKey);
        Assert.Null(nested.ActiveControl);
    }

    [Fact]
    public void UnhandledMnemonic_WithOrdinaryChildTerminatesWithoutParentRecursion()
    {
        using var container = new ExposedContainer();
        container.Controls.Add(new Control());

        Assert.False(container.Mnemonic('x'));
        Assert.False(container.IsHandleCreated);
    }

    private sealed class ExposedContainer : ContainerControl
    {
        public bool Tab(bool forward) => ProcessTabKey(forward);
        public bool DialogKey(Keys keyData) => ProcessDialogKey(keyData);
        public bool Mnemonic(char charCode) => ProcessMnemonic(charCode);
    }

    private sealed class DialogKeyParent : ContainerControl
    {
        public Keys LastKey { get; private set; }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            LastKey = keyData;
            return true;
        }
    }
}
