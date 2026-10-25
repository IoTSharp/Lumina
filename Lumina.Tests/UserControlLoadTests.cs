using System.Runtime.ExceptionServices;
using System.Threading;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public sealed class UserControlLoadTests
{
    [Fact]
    public void Load_RaisesOnceAfterTheFirstSuccessfulCreateControl()
        => OnSta(() =>
        {
            using var control = new TraceUserControl();
            var events = new List<string>();
            control.HandleCreated += (_, _) => events.Add("handle");
            control.VisibleChanged += (_, _) => events.Add("visible");
            control.Load += (_, _) => events.Add("load");
            control.CreateControl();

            Assert.Equal(1, control.LoadCount);
            Assert.Equal(new[] { "handle", "load" }, events);

            control.Visible = false;
            control.Visible = true;
            control.CreateControl();
            Assert.Equal(1, control.LoadCount);

            control.RebuildNativeHandle();
            control.CreateControl();
            Assert.Equal(1, control.LoadCount);
        });

    [Fact]
    public void Load_DoesNotRaiseWhenNativeCreationFails()
        => OnSta(() =>
        {
            using var control = new FailingUserControl();
            int loads = 0;
            control.Load += (_, _) => loads++;

            Assert.Throws<InvalidOperationException>(() => control.CreateControl());
            Assert.Equal(0, loads);
        });

    [Fact]
    public void Load_HandlerExceptionsPropagateAndAreNotReplacedBySuccess()
        => OnSta(() =>
        {
            using var control = new UserControl();
            control.Load += (_, _) => throw new InvalidOperationException("load-failure");

            InvalidOperationException error = Assert.Throws<InvalidOperationException>(() => control.CreateControl());
            Assert.Equal("load-failure", error.Message);
        });

    private static void OnSta(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            try
            {
                WindowsFormsSynchronizationContext.AutoInstall = false;
                action();
            }
            catch (Exception exception)
            {
                failure = exception;
            }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(15)), "UserControl Load test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class TraceUserControl : UserControl
    {
        internal int LoadCount { get; private set; }

        protected override void OnLoad(EventArgs e)
        {
            LoadCount++;
            base.OnLoad(e);
        }

        internal void RebuildNativeHandle() => RecreateHandle();
    }

    private sealed class FailingUserControl : UserControl
    {
        protected override void CreateHandle() => throw new InvalidOperationException("create-failure");
    }
}
