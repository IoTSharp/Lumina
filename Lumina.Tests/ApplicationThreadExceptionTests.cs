using System.Threading;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public class ApplicationThreadExceptionTests
{
    [Fact]
    public void OnThreadException_PassesSameExceptionAndCurrentThreadToHandler()
    {
        var exception = new InvalidOperationException("callback failed");
        var currentThread = Thread.CurrentThread;
        object? observedSender = null;
        Exception? observedException = null;
        int calls = 0;
        ThreadExceptionEventHandler handler = (sender, args) =>
        {
            calls++;
            observedSender = sender;
            observedException = args.Exception;
        };

        try
        {
            Application.ThreadException += handler;

            Application.OnThreadException(exception);

            Assert.Equal(1, calls);
            Assert.Same(currentThread, observedSender);
            Assert.Same(exception, observedException);
        }
        finally
        {
            Application.ThreadException -= handler;
        }
    }

    [Fact]
    public void ThreadException_SubscriptionsBelongToRegisteringThread()
    {
        var mainException = new InvalidOperationException("main callback failed");
        var workerException = new InvalidOperationException("worker callback failed");
        var mainThread = Thread.CurrentThread;
        object? mainSender = null;
        object? workerSender = null;
        Exception? mainObservedException = null;
        Exception? workerObservedException = null;
        Exception? workerFailure = null;
        int mainCalls = 0;
        int workerCalls = 0;
        ThreadExceptionEventHandler mainHandler = (sender, args) =>
        {
            Interlocked.Increment(ref mainCalls);
            mainSender = sender;
            mainObservedException = args.Exception;
        };
        ThreadExceptionEventHandler workerHandler = (sender, args) =>
        {
            Interlocked.Increment(ref workerCalls);
            workerSender = sender;
            workerObservedException = args.Exception;
        };
        var worker = new Thread(() =>
        {
            try
            {
                Application.ThreadException += workerHandler;
                Application.OnThreadException(workerException);
            }
            catch (Exception exception)
            {
                workerFailure = exception;
            }
            finally
            {
                Application.ThreadException -= workerHandler;
            }
        })
        {
            IsBackground = true
        };

        try
        {
            Application.ThreadException += mainHandler;
            worker.Start();

            Assert.True(worker.Join(TimeSpan.FromSeconds(10)), "The worker did not finish reporting its exception.");
            Assert.Null(workerFailure);
            Assert.Equal(0, mainCalls);
            Assert.Equal(1, workerCalls);
            Assert.Same(worker, workerSender);
            Assert.Same(workerException, workerObservedException);

            Application.OnThreadException(mainException);

            Assert.Equal(1, mainCalls);
            Assert.Equal(1, workerCalls);
            Assert.Same(mainThread, mainSender);
            Assert.Same(mainException, mainObservedException);
        }
        finally
        {
            Application.ThreadException -= mainHandler;
        }
    }

    [Fact]
    public void OnThreadException_DoesNotReenterHandlerAndAllowsLaterReports()
    {
        var outerException = new InvalidOperationException("outer callback failed");
        var nestedException = new InvalidOperationException("nested callback failed");
        var laterException = new InvalidOperationException("later callback failed");
        var observedExceptions = new List<Exception>();
        ThreadExceptionEventHandler handler = (_, args) =>
        {
            observedExceptions.Add(args.Exception);
            if (ReferenceEquals(args.Exception, outerException))
            {
                Application.OnThreadException(nestedException);
            }
        };

        try
        {
            Application.ThreadException += handler;

            Application.OnThreadException(outerException);

            Assert.Same(outerException, Assert.Single(observedExceptions));

            Application.OnThreadException(laterException);

            Assert.Equal(2, observedExceptions.Count);
            Assert.Same(laterException, observedExceptions[1]);
        }
        finally
        {
            Application.ThreadException -= handler;
        }
    }

    [Fact]
    public void OnThreadException_PropagatesHandlerFailureAndRestoresReportingState()
    {
        var firstException = new InvalidOperationException("first callback failed");
        var laterException = new InvalidOperationException("later callback failed");
        var handlerFailure = new InvalidOperationException("handler failed");
        var observedExceptions = new List<Exception>();
        ThreadExceptionEventHandler handler = (_, args) =>
        {
            observedExceptions.Add(args.Exception);
            if (observedExceptions.Count == 1)
            {
                throw handlerFailure;
            }
        };

        try
        {
            Application.ThreadException += handler;

            var propagated = Assert.Throws<InvalidOperationException>(() => Application.OnThreadException(firstException));

            Assert.Same(handlerFailure, propagated);
            Assert.Same(firstException, Assert.Single(observedExceptions));

            Application.OnThreadException(laterException);

            Assert.Equal(2, observedExceptions.Count);
            Assert.Same(laterException, observedExceptions[1]);
        }
        finally
        {
            Application.ThreadException -= handler;
        }
    }
}
