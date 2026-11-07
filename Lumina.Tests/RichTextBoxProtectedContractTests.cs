using System.ComponentModel;
using System.Reflection;
using System.Runtime.ExceptionServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class RichTextBoxProtectedContractTests
{
    [Fact]
    public void EventIsDeclaredWithTheStandardHandlerAndBehaviorCategory()
    {
        EventInfo notification = typeof(RichTextBox).GetEvent(nameof(RichTextBox.Protected),
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)!;
        Assert.NotNull(notification);
        Assert.Equal(typeof(EventHandler), notification.EventHandlerType);
        Assert.True(notification.AddMethod!.IsPublic);
        Assert.False(notification.AddMethod.IsVirtual);
        Assert.True(notification.RemoveMethod!.IsPublic);
        Assert.False(notification.RemoveMethod.IsVirtual);
        Assert.Equal("Behavior", notification.GetCustomAttribute<CategoryAttribute>()!.Category);
        Assert.Null(typeof(TextBoxBase).GetEvent(nameof(RichTextBox.Protected),
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly));
    }

    [Fact]
    public void RaiserIsTheStandardProtectedVirtualVoidMember()
    {
        MethodInfo raise = typeof(RichTextBox).GetMethod("OnProtected",
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)!;
        Assert.NotNull(raise);
        Assert.True(raise.IsFamily);
        Assert.True(raise.IsVirtual);
        Assert.False(raise.IsFinal);
        Assert.Equal(typeof(void), raise.ReturnType);
        ParameterInfo parameter = Assert.Single(raise.GetParameters());
        Assert.Equal(typeof(EventArgs), parameter.ParameterType);
        Assert.Equal("e", parameter.Name);
    }

    [Fact]
    public void EventDescriptorExposesTheSameStandardEvent()
    {
        EventDescriptor descriptor = TypeDescriptor.GetEvents(typeof(RichTextBox))[nameof(RichTextBox.Protected)]!;
        Assert.NotNull(descriptor);
        Assert.Equal(typeof(RichTextBox), descriptor.ComponentType);
        Assert.Equal(typeof(EventHandler), descriptor.EventType);
        Assert.Equal("Behavior", descriptor.Category);
        Assert.True(descriptor.IsMulticast);
    }

    [Fact]
    public void RaiserWithoutSubscribersRecordsTheProtectedErrorWithoutCreatingAWindow()
        => OnSta(() =>
        {
            using var rich = new ProtectedRichTextBox();
            Assert.False(rich.HasProtectedError);

            rich.Raise(EventArgs.Empty);

            Assert.True(rich.HasProtectedError);
            Assert.Equal(1, rich.VirtualCalls);
            Assert.Same(EventArgs.Empty, rich.LastArguments);
            Assert.False(rich.IsHandleCreated);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void HandlerSeesTheOriginalSenderAndArgumentAfterTheErrorIsRecorded(bool useNull)
        => OnSta(() =>
        {
            using var rich = new ProtectedRichTextBox();
            EventArgs? supplied = useNull ? null : new EventArgs();
            object? sender = null;
            EventArgs? received = EventArgs.Empty;
            bool errorDuringHandler = false;
            rich.Protected += (source, arguments) =>
            {
                sender = source;
                received = arguments;
                errorDuringHandler = rich.HasProtectedError;
            };

            rich.Raise(supplied);

            Assert.Same(rich, sender);
            Assert.Same(supplied, received);
            Assert.Same(supplied, rich.LastArguments);
            Assert.True(errorDuringHandler);
            Assert.True(rich.HasProtectedError);
            Assert.False(rich.IsHandleCreated);
        });

    [Fact]
    public void AddingAndRemovingSubscribersDoesNotRecordAnErrorOrCreateAWindow()
        => OnSta(() =>
        {
            using var rich = new ProtectedRichTextBox();
            EventHandler handler = (_, _) => throw new InvalidOperationException("Must not be invoked by subscription.");

            rich.Protected += handler;
            rich.Protected -= handler;
            rich.Protected -= handler;

            Assert.False(rich.HasProtectedError);
            Assert.Equal(0, rich.VirtualCalls);
            Assert.False(rich.IsHandleCreated);
        });

    [Fact]
    public void DuplicateSubscriptionsAndRemovalPreserveDelegateOrder()
        => OnSta(() =>
        {
            using var rich = new ProtectedRichTextBox();
            List<string> calls = [];
            EventHandler first = (_, _) => calls.Add("first");
            EventHandler second = (_, _) => calls.Add("second");
            rich.Protected += first;
            rich.Protected += second;
            rich.Protected += first;

            rich.Raise(EventArgs.Empty);
            Assert.Equal(["first", "second", "first"], calls);
            calls.Clear();
            rich.Protected -= first;
            rich.Raise(EventArgs.Empty);
            Assert.Equal(["first", "second"], calls);
            calls.Clear();
            rich.Protected -= first;
            rich.Protected -= second;
            rich.Raise(EventArgs.Empty);
            Assert.Empty(calls);
            Assert.True(rich.HasProtectedError);
        });

    [Fact]
    public void DescriptorSubscriptionsUseTheSameEventSlotAsDirectSubscriptions()
        => OnSta(() =>
        {
            using var rich = new ProtectedRichTextBox();
            EventDescriptor descriptor = TypeDescriptor.GetEvents(rich)[nameof(RichTextBox.Protected)]!;
            int calls = 0;
            EventHandler handler = (_, _) => calls++;

            descriptor.AddEventHandler(rich, handler);
            rich.Raise(EventArgs.Empty);
            Assert.Equal(1, calls);
            rich.Protected -= handler;
            rich.Raise(EventArgs.Empty);
            Assert.Equal(1, calls);
            rich.Protected += handler;
            descriptor.RemoveEventHandler(rich, handler);
            rich.Raise(EventArgs.Empty);
            Assert.Equal(1, calls);
            Assert.False(rich.IsHandleCreated);
        });

    [Fact]
    public void CanRaiseEventsSuppressesSubscribersButStillRecordsTheError()
        => OnSta(() =>
        {
            using var rich = new ProtectedRichTextBox { EventsAllowed = false };
            int calls = 0;
            rich.Protected += (_, _) => calls++;

            rich.Raise(EventArgs.Empty);

            Assert.Equal(0, calls);
            Assert.Equal(1, rich.VirtualCalls);
            Assert.True(rich.HasProtectedError);
            rich.ClearProtectedError();
            rich.EventsAllowed = true;
            rich.Raise(EventArgs.Empty);
            Assert.Equal(1, calls);
            Assert.Equal(2, rich.VirtualCalls);
            Assert.True(rich.HasProtectedError);
            Assert.False(rich.IsHandleCreated);
        });

    [Fact]
    public void OverrideCanOmitTheBaseRaiserAndItsErrorState()
        => OnSta(() =>
        {
            using var rich = new ProtectedRichTextBox { InvokeBaseRaiser = false };
            int calls = 0;
            rich.Protected += (_, _) => calls++;

            rich.Raise(EventArgs.Empty);

            Assert.Equal(1, rich.VirtualCalls);
            Assert.Equal(0, calls);
            Assert.False(rich.HasProtectedError);
            Assert.False(rich.IsHandleCreated);
        });

    [Fact]
    public void BaseRaiserRecordsTheErrorBetweenTheOverrideAndEventCallbacks()
        => OnSta(() =>
        {
            using var rich = new ProtectedRichTextBox();
            List<(string Stage, bool Error)> calls = [];
            rich.BeforeBase = () => calls.Add(("before", rich.HasProtectedError));
            rich.AfterBase = () => calls.Add(("after", rich.HasProtectedError));
            rich.Protected += (_, _) => calls.Add(("event", rich.HasProtectedError));

            rich.Raise(EventArgs.Empty);

            Assert.Equal(new[] { ("before", false), ("event", true), ("after", true) }, calls);
        });

    [Fact]
    public void HandlerExceptionPreservesIdentityStopsDispatchAndKeepsTheErrorRecorded()
        => OnSta(() =>
        {
            using var rich = new ProtectedRichTextBox();
            var failure = new InvalidOperationException("protected handler");
            int laterCalls = 0;
            bool afterBase = false;
            rich.AfterBase = () => afterBase = true;
            EventHandler throwing = (_, _) => throw failure;
            rich.Protected += throwing;
            rich.Protected += (_, _) => laterCalls++;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => rich.Raise(EventArgs.Empty)));
            Assert.Equal(0, laterCalls);
            Assert.False(afterBase);
            Assert.True(rich.HasProtectedError);
            rich.Protected -= throwing;
            rich.ClearProtectedError();
            rich.Raise(EventArgs.Empty);
            Assert.Equal(1, laterCalls);
            Assert.True(afterBase);
            Assert.True(rich.HasProtectedError);
            Assert.False(rich.IsHandleCreated);
        });

    [Fact]
    public void OverrideFailureBeforeBaseDoesNotPretendThatTheBaseRaiserRan()
        => OnSta(() =>
        {
            using var rich = new ProtectedRichTextBox();
            var failure = new InvalidOperationException("before base");
            int calls = 0;
            rich.BeforeBase = () => throw failure;
            rich.Protected += (_, _) => calls++;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => rich.Raise(EventArgs.Empty)));

            Assert.Equal(0, calls);
            Assert.False(rich.HasProtectedError);
            Assert.Equal(1, rich.VirtualCalls);
        });

    [Fact]
    public void OverrideFailureAfterBasePreservesTheDeliveredEventAndRecordedError()
        => OnSta(() =>
        {
            using var rich = new ProtectedRichTextBox();
            var failure = new InvalidOperationException("after base");
            int calls = 0;
            rich.AfterBase = () => throw failure;
            rich.Protected += (_, _) => calls++;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => rich.Raise(EventArgs.Empty)));

            Assert.Equal(1, calls);
            Assert.True(rich.HasProtectedError);
            Assert.Equal(1, rich.VirtualCalls);
        });

    [Fact]
    public void FiniteReentryPreservesBothArgumentIdentitiesAndTheRecordedError()
        => OnSta(() =>
        {
            using var rich = new ProtectedRichTextBox();
            EventArgs outer = new();
            EventArgs inner = new();
            List<EventArgs> calls = [];
            rich.Protected += (_, arguments) =>
            {
                Assert.True(rich.HasProtectedError);
                calls.Add(arguments);
                if (ReferenceEquals(arguments, outer)) { rich.Raise(inner); }
            };

            rich.Raise(outer);

            Assert.Equal(new[] { outer, inner }, calls);
            Assert.Equal(2, rich.VirtualCalls);
            Assert.Same(inner, rich.LastArguments);
            Assert.True(rich.HasProtectedError);
            Assert.False(rich.IsHandleCreated);
        });

    [Fact]
    public void SubscriptionChangesDuringDispatchApplyToTheNextInvocation()
        => OnSta(() =>
        {
            using var rich = new ProtectedRichTextBox();
            List<string> calls = [];
            EventHandler later = (_, _) => calls.Add("later");
            EventHandler added = (_, _) => calls.Add("added");
            bool changed = false;
            rich.Protected += (_, _) =>
            {
                calls.Add("first");
                if (!changed)
                {
                    changed = true;
                    rich.Protected -= later;
                    rich.Protected += added;
                }
            };
            rich.Protected += later;

            rich.Raise(EventArgs.Empty);
            Assert.Equal(["first", "later"], calls);
            calls.Clear();
            rich.Raise(EventArgs.Empty);
            Assert.Equal(["first", "added"], calls);
        });

    [Fact]
    public void OtherRichTextNotificationsDoNotShareTheProtectedSlotOrErrorState()
        => OnSta(() =>
        {
            using var rich = new ProtectedRichTextBox();
            int protectedCalls = 0;
            int otherCalls = 0;
            rich.Protected += (_, _) => protectedCalls++;
            rich.SelectionChanged += (_, _) => otherCalls++;
            rich.HScroll += (_, _) => otherCalls++;
            rich.VScroll += (_, _) => otherCalls++;
            rich.ImeChange += (_, _) => otherCalls++;

            rich.RaiseOtherNotifications();
            Assert.Equal(4, otherCalls);
            Assert.Equal(0, protectedCalls);
            Assert.False(rich.HasProtectedError);
            rich.Raise(EventArgs.Empty);
            Assert.Equal(4, otherCalls);
            Assert.Equal(1, protectedCalls);
            Assert.True(rich.HasProtectedError);
            Assert.False(rich.IsHandleCreated);
        });

    [Fact]
    public void EachInstanceOwnsItsSubscriptionsAndProtectedError()
        => OnSta(() =>
        {
            using var first = new ProtectedRichTextBox();
            using var second = new ProtectedRichTextBox();
            int firstCalls = 0;
            int secondCalls = 0;
            first.Protected += (_, _) => firstCalls++;
            second.Protected += (_, _) => secondCalls++;

            first.Raise(EventArgs.Empty);

            Assert.Equal(1, firstCalls);
            Assert.Equal(0, secondCalls);
            Assert.True(first.HasProtectedError);
            Assert.False(second.HasProtectedError);
            second.Raise(EventArgs.Empty);
            Assert.Equal(1, firstCalls);
            Assert.Equal(1, secondCalls);
            Assert.True(second.HasProtectedError);
            Assert.False(first.IsHandleCreated);
            Assert.False(second.IsHandleCreated);
        });

    private static void OnSta(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            try { action(); }
            catch (Exception exception) { failure = exception; }
            finally
            {
                try
                {
                    Application.ExitThread();
                    typeof(Application).GetMethod("DisposeThemedCommonControlsActivation",
                        BindingFlags.Static | BindingFlags.NonPublic)!.Invoke(null, null);
                }
                catch (Exception exception)
                {
                    failure = failure is null ? exception : new AggregateException(failure, exception);
                }
            }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "RichTextBox Protected contract test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class ProtectedRichTextBox : RichTextBox
    {
        private static readonly FieldInfo s_protectedError = typeof(RichTextBox).GetField("_protectedError",
            BindingFlags.Instance | BindingFlags.NonPublic)!;

        internal bool EventsAllowed { get; set; } = true;
        internal bool InvokeBaseRaiser { get; set; } = true;
        internal int VirtualCalls { get; private set; }
        internal EventArgs? LastArguments { get; private set; }
        internal Action? BeforeBase { get; set; }
        internal Action? AfterBase { get; set; }
        internal bool HasProtectedError => (bool)s_protectedError.GetValue(this)!;
        internal void ClearProtectedError() => s_protectedError.SetValue(this, false);
        internal void Raise(EventArgs? arguments) => OnProtected(arguments!);

        internal void RaiseOtherNotifications()
        {
            OnSelectionChanged(EventArgs.Empty);
            OnHScroll(EventArgs.Empty);
            OnVScroll(EventArgs.Empty);
            OnImeChange(EventArgs.Empty);
        }

        protected override bool CanRaiseEvents => EventsAllowed;
        protected override void CreateHandle()
            => throw new InvalidOperationException("Protected event contract must not create a native window.");

        protected override void OnProtected(EventArgs e)
        {
            VirtualCalls++;
            LastArguments = e;
            BeforeBase?.Invoke();
            if (InvokeBaseRaiser) { base.OnProtected(e); }
            AfterBase?.Invoke();
        }
    }
}
