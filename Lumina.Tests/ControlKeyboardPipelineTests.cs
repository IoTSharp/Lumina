using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class ControlKeyboardPipelineTests
{
    [Fact]
    public void WindowProcedureUsesKeyMessageOverrideAndPreservesHandledResult()
    {
        using var control = new PipelineControl { ConsumeMessage = true };
        Message message = Message.Create(0, 0x0100, (nint)Keys.A, 0);

        control.Dispatch(ref message);

        Assert.Equal(new[] { "message" }, control.Trace);
        Assert.Equal(0, message.Result);
        Assert.Equal(0, control.DefaultCalls);
        Assert.False(control.IsHandleCreated);
    }

    [Fact]
    public void ParentPreviewCanConsumeBeforeChildEventWithoutDefaultProcessing()
    {
        using var parent = new PipelineControl { ConsumePreview = true };
        using var child = new PipelineControl { Parent = parent };
        child.KeyDown += (_, _) => child.Trace.Add("down");
        Message message = Message.Create(0, 0x0100, (nint)Keys.A, 0);

        child.Dispatch(ref message);

        Assert.Equal(new[] { "preview" }, parent.Trace);
        Assert.Equal(new[] { "message" }, child.Trace);
        Assert.Equal(0, child.DefaultCalls);
        Assert.False(parent.IsHandleCreated);
        Assert.False(child.IsHandleCreated);
    }

    [Fact]
    public void UnhandledParentPreviewFallsThroughToChildEventAndDefaultProcedure()
    {
        using var parent = new PipelineControl();
        using var child = new PipelineControl { Parent = parent };
        child.KeyDown += (_, _) => child.Trace.Add("down");
        Message message = Message.Create(0, 0x0100, (nint)Keys.A, 0);

        child.Dispatch(ref message);

        Assert.Equal(new[] { "preview" }, parent.Trace);
        Assert.Equal(new[] { "message", "args", "down", "default" }, child.Trace);
        Assert.Equal(1, child.DefaultCalls);
        Assert.Equal(37, message.Result);
    }

    [Fact]
    public void ProcessKeyMessageReturnsEventHandledWithoutCallingWindowOrDefaultProcedure()
    {
        using var control = new PipelineControl();
        control.KeyDown += (_, e) => e.Handled = true;
        Message handled = Message.Create(0, 0x0100, (nint)Keys.A, 0);
        handled.Result = 91;

        Assert.True(control.Process(ref handled));
        Assert.Equal(91, handled.Result);
        Assert.Equal(0, control.DefaultCalls);
        Assert.Equal(new[] { "message", "args" }, control.Trace);

        Message unhandled = Message.Create(0, 0x0101, (nint)Keys.A, 0);
        unhandled.Result = 91;
        Assert.False(control.Process(ref unhandled));
        Assert.Equal(91, unhandled.Result);
        Assert.Equal(0, control.DefaultCalls);
    }

    [Fact]
    public void CharacterChangesReachDefaultProcedureForNormalAndSystemCharacters()
    {
        using var control = new PipelineControl();
        control.KeyPress += (_, e) => e.KeyChar = char.ToUpperInvariant(e.KeyChar);
        Message normal = Message.Create(0, 0x0102, (nint)'a', 0);
        Message system = Message.Create(0, 0x0106, (nint)'b', 0);

        control.Dispatch(ref normal);
        control.Dispatch(ref system);

        Assert.Equal((nint)'A', normal.WParam);
        Assert.Equal((nint)'B', system.WParam);
        Assert.Equal(2, control.DefaultCalls);
        Assert.False(control.IsHandleCreated);
    }

    [Fact]
    public void PreviewCanRaiseItsOwnEventsWithoutReenteringParentPreview()
    {
        using var parent = new PipelineControl { PreviewOwnEvents = true };
        using var child = new PipelineControl { Parent = parent };
        parent.KeyDown += (_, e) => { parent.Trace.Add("down"); e.Handled = true; };
        Message message = Message.Create(0, 0x0100, (nint)Keys.A, 0);

        child.Dispatch(ref message);

        Assert.Equal(new[] { "preview", "args", "down" }, parent.Trace);
        Assert.Equal(new[] { "message" }, child.Trace);
        Assert.Equal(0, child.DefaultCalls);
    }

    [Fact]
    public void BaseMnemonicDoesNotRouteBackToParentContainer()
    {
        using var parent = new ContainerControl();
        using var child = new PipelineControl { Parent = parent };

        Assert.False(child.Mnemonic('a'));
        Assert.False(child.IsHandleCreated);
        Assert.False(parent.IsHandleCreated);
    }

    private sealed class PipelineControl : Control
    {
        internal List<string> Trace { get; } = [];
        internal bool ConsumeMessage { get; init; }
        internal bool ConsumePreview { get; init; }
        internal bool PreviewOwnEvents { get; init; }
        internal int DefaultCalls { get; private set; }

        internal void Dispatch(ref Message message) => WndProc(ref message);
        internal bool Process(ref Message message) => ProcessKeyMessage(ref message);
        internal bool Mnemonic(char character) => ProcessMnemonic(character);

        protected override bool ProcessKeyMessage(ref Message msg)
        {
            Trace.Add("message");
            return ConsumeMessage || base.ProcessKeyMessage(ref msg);
        }

        protected override bool ProcessKeyPreview(ref Message msg)
        {
            Trace.Add("preview");
            if (ConsumePreview || (PreviewOwnEvents && ProcessKeyEventArgs(ref msg))) { return true; }
            return base.ProcessKeyPreview(ref msg);
        }

        protected override bool ProcessKeyEventArgs(ref Message msg)
        {
            Trace.Add("args");
            return base.ProcessKeyEventArgs(ref msg);
        }

        protected override void DefWndProc(ref Message message)
        {
            Trace.Add("default");
            DefaultCalls++;
            message.Result = 37;
        }
    }
}
