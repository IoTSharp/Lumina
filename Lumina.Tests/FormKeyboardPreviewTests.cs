using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class FormKeyboardPreviewTests
{
    [Fact]
    public void PreviewDefaultsOffAndCanConsumeChildKeyBeforeItsHandler()
    {
        using var form = new Form();
        using var child = new KeyControl { Parent = form };
        var events = new List<string>();
        form.KeyDown += (_, e) => { events.Add("form"); e.Handled = true; };
        child.KeyDown += (_, _) => events.Add("child");
        var message = Message.Create(0, 0x0100, (nint)Keys.A, 0);

        Assert.False(form.KeyPreview);
        Assert.False(child.Process(ref message));
        Assert.Equal(new[] { "child" }, events);

        events.Clear();
        form.KeyPreview = true;
        Assert.True(child.Process(ref message));
        Assert.Equal(new[] { "form" }, events);
        Assert.False(form.IsHandleCreated);
        Assert.False(child.IsHandleCreated);
    }

    [Fact]
    public void UnhandledPreviewFlowsThroughNestedContainerToFocusedChild()
    {
        using var form = new Form { KeyPreview = true };
        using var container = new ContainerControl { Parent = form };
        using var child = new KeyControl { Parent = container };
        var events = new List<string>();
        form.KeyPress += (_, e) => { events.Add("form"); e.KeyChar = 'z'; };
        child.KeyPress += (_, e) => { events.Add($"child:{e.KeyChar}"); e.Handled = true; };
        var message = Message.Create(0, 0x0102, (nint)'a', 0);

        Assert.True(child.Process(ref message));
        Assert.Equal(new[] { "form", "child:z" }, events);
        Assert.False(form.IsHandleCreated);
    }

    [Fact]
    public void TopLevelFormWrapsTabAtBothEnds()
    {
        using var form = new TabForm();
        using var first = new Control { Parent = form, TabIndex = 0 };
        using var last = new Control { Parent = form, TabIndex = 1 };
        form.Show();
        form.ActiveControl = last;

        Assert.True(form.Tab(forward: true));
        Assert.Same(first, form.ActiveControl);
        Assert.True(form.Tab(forward: false));
        Assert.Same(last, form.ActiveControl);
        Assert.True(form.IsHandleCreated);
    }

    private sealed class KeyControl : Control
    {
        internal bool Process(ref Message message) => ProcessKeyMessage(ref message);
    }

    private sealed class TabForm : Form
    {
        internal bool Tab(bool forward) => ProcessTabKey(forward);
    }
}
