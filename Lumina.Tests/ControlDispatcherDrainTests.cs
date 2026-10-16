using System.Runtime.ExceptionServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class ControlDispatcherDrainTests
{
    [Fact]
    public void DrainContinuesAfterDisposedCompletionAndPreservesFifo()
        => OnSta(() =>
        {
            using var control = new ExposedControl();
            _ = control.Handle;
            List<string> order = [];
            IAsyncResult first = control.BeginInvoke((Action)(() => order.Add("first")));
            IAsyncResult second = control.BeginInvoke((Action)(() => order.Add("second")));
            first.AsyncWaitHandle.Dispose();

            Assert.IsType<ObjectDisposedException>(Assert.Throws<ObjectDisposedException>(() => control.EndInvoke(first)));
            Assert.Equal(["first", "second"], order);
            Assert.True(first.IsCompleted);
            Assert.True(second.IsCompleted);
            Assert.Null(control.EndInvoke(second));
        });

    [Fact]
    public void DrainReportsCallbackFailureWhenCompletionSignalAlsoFails()
        => OnSta(() =>
        {
            using var control = new ExposedControl();
            _ = control.Handle;
            var expected = new InvalidOperationException("control callback failure");
            List<string> order = [];
            List<Exception> reported = [];
            ThreadExceptionEventHandler handler = (_, args) => reported.Add(args.Exception);
            Application.ThreadException += handler;
            try
            {
                IAsyncResult first = control.BeginInvoke((Action)(() =>
                {
                    order.Add("first");
                    throw expected;
                }));
                IAsyncResult second = control.BeginInvoke((Action)(() => order.Add("second")));
                first.AsyncWaitHandle.Dispose();

                Assert.IsType<ObjectDisposedException>(Assert.Throws<ObjectDisposedException>(() => control.EndInvoke(first)));
                Assert.Equal(["first", "second"], order);
                Assert.Empty(reported);
                Assert.Same(expected, Assert.Throws<InvalidOperationException>(() => control.EndInvoke(first)));
                Assert.Null(control.EndInvoke(second));
            }
            finally
            {
                Application.ThreadException -= handler;
            }
        });

    [Fact]
    public void DrainAggregatesMultipleCompletionFailuresAfterExecutingEveryCallback()
        => OnSta(() =>
        {
            using var control = new ExposedControl();
            _ = control.Handle;
            List<string> order = [];
            IAsyncResult first = control.BeginInvoke((Action)(() => order.Add("first")));
            IAsyncResult second = control.BeginInvoke((Action)(() => order.Add("second")));
            IAsyncResult third = control.BeginInvoke((Action)(() => order.Add("third")));
            first.AsyncWaitHandle.Dispose();
            second.AsyncWaitHandle.Dispose();

            AggregateException failure = Assert.Throws<AggregateException>(() => control.EndInvoke(third));
            Assert.Equal(2, failure.InnerExceptions.Count);
            Assert.All(failure.InnerExceptions, exception => Assert.IsType<ObjectDisposedException>(exception));
            Assert.Equal(["first", "second", "third"], order);
            Assert.All(new[] { first, second, third }, result => Assert.True(result.IsCompleted));
            Assert.Null(control.EndInvoke(third));
        });

    [Fact]
    public void SameThreadInvokeDrainsQueuedCallbacksAndKeepsNestedFifo()
        => OnSta(() =>
        {
            using var control = new ExposedControl();
            _ = control.Handle;
            List<string> order = [];
            IAsyncResult queued = control.BeginInvoke((Action)(() => order.Add("queued")));

            Assert.Equal(42, control.Invoke((Func<int>)(() =>
            {
                order.Add("invoke");
                return 42;
            })));
            Assert.Equal(["queued", "invoke"], order);
            Assert.Null(control.EndInvoke(queued));

            IAsyncResult? nestedQueued = null;
            IAsyncResult outer = control.BeginInvoke((Action)(() =>
            {
                order.Add("outer-enter");
                control.Invoke((Action)(() => order.Add("inner-invoke")));
                order.Add("outer-exit");
                nestedQueued = control.BeginInvoke((Action)(() => order.Add("nested-queued")));
            }));

            Assert.Null(control.EndInvoke(outer));
            Assert.Equal(["queued", "invoke", "outer-enter", "inner-invoke", "outer-exit", "nested-queued"], order);
            Assert.NotNull(nestedQueued);
            Assert.Null(control.EndInvoke(nestedQueued!));
        });

    private static void OnSta(Action action)
    {
        Exception? failure = null;
        Thread thread = new(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            try { action(); }
            catch (Exception error) { failure = error; }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(15)), "The dispatcher drain check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class ExposedControl : Control
    {
    }
}
