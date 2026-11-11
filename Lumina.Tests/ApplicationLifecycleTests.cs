using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public class ApplicationLifecycleTests
{
    [Fact]
    public void Context_ExitUsesVirtualPolicyAndPreservesContextSender()
    {
        using var context = new RecordingContext();
        object? sender = null;
        context.ThreadExit += (source, _) =>
        {
            context.Order.Add("event");
            sender = source;
        };

        context.ExitThread();

        Assert.Same(context, sender);
        Assert.Equal(["before", "event", "after"], context.Order);
    }

    [Fact]
    public void Context_ReplacingMainFormDetachesPreviousLifetimeAndDisposesOnlyCurrentForm()
    {
        bool autoInstall = WindowsFormsSynchronizationContext.AutoInstall;
        WindowsFormsSynchronizationContext.AutoInstall = false;
        try
        {
            using var first = new HandlelessForm();
            using var second = new HandlelessForm();
            var context = new ApplicationContext(first);
            int exits = 0;
            context.ThreadExit += (_, _) => exits++;
            context.MainForm = second;

            first.RaiseHandleDestroyed();
            Assert.Equal(0, exits);
            second.RaiseHandleDestroyed();
            second.RaiseHandleDestroyed();
            Assert.Equal(1, exits);

            context.Dispose();
            Assert.False(first.IsDisposed);
            Assert.True(second.IsDisposed);
            Assert.Null(context.MainForm);
        }
        finally { WindowsFormsSynchronizationContext.AutoInstall = autoInstall; }
    }

    [Fact]
    public void Filters_ReentrantDispatchUsesOneSnapshotAndLeavesCallerMessageUnchanged()
    {
        var calls = new List<string>();
        bool nested = false;
        var third = new Filter((ref Message _) => { calls.Add("third"); return false; });
        var second = new Filter((ref Message m) =>
        {
            calls.Add("second");
            Assert.Equal(42, m.Msg);
            return false;
        });
        var first = new Filter((ref Message m) =>
        {
            calls.Add("first");
            m.Msg = 42;
            if (!nested)
            {
                nested = true;
                Application.RemoveMessageFilter(second);
                Application.AddMessageFilter(third);
                Message inner = Message.Create(0, 10, 0, 0);
                Assert.False(Application.FilterMessage(ref inner));
                Assert.Equal(10, inner.Msg);
            }
            return false;
        });

        try
        {
            Application.AddMessageFilter(first);
            Application.AddMessageFilter(second);
            Message message = Message.Create(0, 7, 0, 0);
            Assert.False(Application.FilterMessage(ref message));
            Assert.Equal(7, message.Msg);
            Assert.Equal(["first", "first", "second", "second"], calls);

            calls.Clear();
            Application.FilterMessage(ref message);
            Assert.Equal(["first", "third"], calls);
        }
        finally
        {
            Application.RemoveMessageFilter(first);
            Application.RemoveMessageFilter(second);
            Application.RemoveMessageFilter(third);
        }
    }

    [Fact]
    public void MessageLoop_CustomHostCallbackCanBeRegisteredAndRemovedWithoutStartingLoop()
    {
        try
        {
            Application.RegisterMessageLoop(() => true);
            Assert.True(Application.MessageLoop);
            Application.RegisterMessageLoop(() => false);
            Assert.False(Application.MessageLoop);
        }
        finally { Application.UnregisterMessageLoop(); }
        Assert.False(Application.MessageLoop);
    }

    private delegate bool FilterCallback(ref Message message);
    private sealed class Filter(FilterCallback callback) : IMessageFilter
    {
        public bool PreFilterMessage(ref Message m) => callback(ref m);
    }

    private sealed class RecordingContext : ApplicationContext
    {
        public List<string> Order { get; } = [];
        protected override void ExitThreadCore()
        {
            Order.Add("before");
            base.ExitThreadCore();
            Order.Add("after");
        }
    }

    private sealed class HandlelessForm : Form
    {
        public void RaiseHandleDestroyed() => OnHandleDestroyed(EventArgs.Empty);
    }
}
