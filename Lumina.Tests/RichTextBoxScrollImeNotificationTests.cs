using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class RichTextBoxScrollImeNotificationTests
{
    private const int Horizontal = 0;
    private const int Vertical = 1;
    private const int Ime = 2;
    private const int WmHScroll = 0x0114;
    private const int WmVScroll = 0x0115;
    private const int WmImeNotify = 0x0282;
    private const int EnHScroll = 0x0601;
    private const int EnVScroll = 0x0602;

    [Theory]
    [InlineData(nameof(RichTextBox.HScroll), "OnHScroll")]
    [InlineData(nameof(RichTextBox.VScroll), "OnVScroll")]
    [InlineData(nameof(RichTextBox.ImeChange), "OnImeChange")]
    public void EventsAndVirtualRaisersHaveTheStandardDeclaredSurface(string eventName, string raiserName)
    {
        EventInfo notification = typeof(RichTextBox).GetEvent(eventName,
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)!;
        Assert.NotNull(notification);
        Assert.Equal(typeof(EventHandler), notification.EventHandlerType);
        Assert.True(notification.AddMethod!.IsPublic);
        Assert.False(notification.AddMethod.IsVirtual);
        Assert.True(notification.RemoveMethod!.IsPublic);
        Assert.False(notification.RemoveMethod.IsVirtual);
        Assert.Equal("Behavior", notification.GetCustomAttribute<CategoryAttribute>()!.Category);

        MethodInfo raise = typeof(RichTextBox).GetMethod(raiserName,
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)!;
        Assert.True(raise.IsFamily);
        Assert.True(raise.IsVirtual);
        Assert.False(raise.IsFinal);
        Assert.Equal(typeof(void), raise.ReturnType);
        ParameterInfo argument = Assert.Single(raise.GetParameters());
        Assert.Equal(typeof(EventArgs), argument.ParameterType);
        Assert.Equal("e", argument.Name);
    }

    [Theory]
    [InlineData(Horizontal)]
    [InlineData(Vertical)]
    [InlineData(Ime)]
    public void DirectRaisingPreservesSenderAndArgumentIdentityIncludingNullWithoutCreatingAHandle(int signal)
        => OnSta(() =>
        {
            using var rich = new NotificationRichTextBox();
            rich.Raise(signal, EventArgs.Empty);
            Assert.False(rich.IsHandleCreated);
            var arguments = new EventArgs();
            object? sender = null;
            EventArgs? received = EventArgs.Empty;
            Add(rich, signal, (source, value) => { sender = source; received = value; });

            rich.Raise(signal, arguments);

            Assert.Same(rich, sender);
            Assert.Same(arguments, received);
            rich.Raise(signal, null);
            Assert.Null(received);
            Assert.Equal(3, rich.VirtualCalls);
            Assert.False(rich.IsHandleCreated);
        });

    [Fact]
    public void TheThreeEventsUseIndependentComponentSlots()
        => OnSta(() =>
        {
            using var rich = new NotificationRichTextBox();
            List<string> calls = [];
            rich.HScroll += (_, _) => calls.Add("horizontal");
            rich.VScroll += (_, _) => calls.Add("vertical");
            rich.ImeChange += (_, _) => calls.Add("ime");

            rich.Raise(Horizontal, EventArgs.Empty);
            rich.Raise(Vertical, EventArgs.Empty);
            rich.Raise(Ime, EventArgs.Empty);

            Assert.Equal(["horizontal", "vertical", "ime"], calls);
            Assert.False(rich.IsHandleCreated);
        });

    [Theory]
    [InlineData(Horizontal)]
    [InlineData(Vertical)]
    [InlineData(Ime)]
    public void DuplicateSubscriptionAndRemovalPreserveDelegateOrder(int signal)
        => OnSta(() =>
        {
            using var rich = new NotificationRichTextBox();
            List<string> calls = [];
            EventHandler first = (_, _) => calls.Add("first");
            EventHandler second = (_, _) => calls.Add("second");
            Add(rich, signal, first);
            Add(rich, signal, second);
            Add(rich, signal, first);
            rich.Raise(signal, EventArgs.Empty);
            Assert.Equal(["first", "second", "first"], calls);
            calls.Clear();
            Remove(rich, signal, first);
            rich.Raise(signal, EventArgs.Empty);
            Assert.Equal(["first", "second"], calls);
            calls.Clear();
            Remove(rich, signal, first);
            Remove(rich, signal, second);
            rich.Raise(signal, EventArgs.Empty);
            Assert.Empty(calls);
        });

    [Theory]
    [InlineData(Horizontal)]
    [InlineData(Vertical)]
    [InlineData(Ime)]
    public void CanRaiseEventsSuppressesTheSlotButNotTheVirtualRaiser(int signal)
        => OnSta(() =>
        {
            using var rich = new NotificationRichTextBox { EventsAllowed = false };
            int calls = 0;
            Add(rich, signal, (_, _) => calls++);
            rich.Raise(signal, EventArgs.Empty);
            Assert.Equal(1, rich.VirtualCalls);
            Assert.Equal(0, calls);
            rich.EventsAllowed = true;
            rich.Raise(signal, EventArgs.Empty);
            Assert.Equal(2, rich.VirtualCalls);
            Assert.Equal(1, calls);
        });

    [Theory]
    [InlineData(Horizontal)]
    [InlineData(Vertical)]
    [InlineData(Ime)]
    public void AnOverrideCanSuppressTheBaseEvent(int signal)
        => OnSta(() =>
        {
            using var rich = new NotificationRichTextBox { InvokeBaseRaiser = false };
            int calls = 0;
            Add(rich, signal, (_, _) => calls++);
            rich.Raise(signal, EventArgs.Empty);
            Assert.Equal(1, rich.VirtualCalls);
            Assert.Equal(0, calls);
            rich.InvokeBaseRaiser = true;
            rich.Raise(signal, EventArgs.Empty);
            Assert.Equal(2, rich.VirtualCalls);
            Assert.Equal(1, calls);
        });

    [Theory]
    [InlineData(Horizontal)]
    [InlineData(Vertical)]
    [InlineData(Ime)]
    public void AHandlerExceptionRetainsIdentityAndStopsOnlyItsCurrentInvocation(int signal)
        => OnSta(() =>
        {
            using var rich = new NotificationRichTextBox();
            var failure = new InvalidOperationException("notification failure");
            int later = 0;
            EventHandler throwing = (_, _) => throw failure;
            Add(rich, signal, throwing);
            Add(rich, signal, (_, _) => later++);

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => rich.Raise(signal, EventArgs.Empty)));
            Assert.Equal(0, later);
            Remove(rich, signal, throwing);
            rich.Raise(signal, EventArgs.Empty);
            Assert.Equal(1, later);
        });

    [Theory]
    [InlineData(Horizontal)]
    [InlineData(Vertical)]
    [InlineData(Ime)]
    public void AHandlerCanReenterOnceWithItsOwnArgument(int signal)
        => OnSta(() =>
        {
            using var rich = new NotificationRichTextBox();
            var outer = new EventArgs();
            var inner = new EventArgs();
            List<EventArgs> calls = [];
            Add(rich, signal, (_, value) =>
            {
                calls.Add(value);
                if (calls.Count == 1) { rich.Raise(signal, inner); }
                Assert.InRange(calls.Count, 1, 2);
            });

            rich.Raise(signal, outer);

            Assert.Equal(2, rich.VirtualCalls);
            Assert.Equal(2, calls.Count);
            Assert.Same(outer, calls[0]);
            Assert.Same(inner, calls[1]);
        });

    [Theory]
    [InlineData(EnHScroll, Horizontal)]
    [InlineData(EnVScroll, Vertical)]
    public void ReflectedScrollCommandsRaiseOnlyTheirMatchingEventWithoutCreatingAHandle(int code, int signal)
        => OnSta(() =>
        {
            using var rich = new NotificationRichTextBox();
            EventArgs? arguments = null;
            int calls = 0;
            Add(rich, signal, (sender, value) =>
            {
                Assert.Same(rich, sender);
                arguments = value;
                calls++;
            });

            Assert.True(rich.Command(code));

            Assert.Equal(1, calls);
            Assert.Equal([SignalName(signal)], rich.Trace);
            Assert.Same(EventArgs.Empty, arguments);
            Assert.False(rich.IsHandleCreated);
        });

    [Fact]
    public void OtherCommandNotificationsDoNotMasqueradeAsScrollOrImeEvents()
        => OnSta(() =>
        {
            using var rich = new NotificationRichTextBox();
            Assert.False(rich.Command(0x0603));
            Assert.False(rich.Command(0x0707));
            Assert.Equal(0, rich.VirtualCalls);
            Assert.Empty(rich.Trace);
            Assert.False(rich.IsHandleCreated);
        });

    [Theory]
    [InlineData(EnHScroll)]
    [InlineData(EnVScroll)]
    public void ReflectedScrollCommandsDuringHandleCreationFallBackWithoutRaising(int code)
        => OnSta(() =>
        {
            using var rich = new NotificationRichTextBox { CreationCommand = code };

            _ = rich.Handle;

            Assert.False(rich.CreationCommandHandled);
            Assert.Equal(0, rich.CreationCommandRaises);
            rich.Trace.Clear();
            int before = rich.VirtualCalls;
            Assert.True(rich.Command(code));
            Assert.Equal(before + 1, rich.VirtualCalls);
            Assert.Single(rich.Trace);
        });

    [Fact]
    public void CreationAndRecreationEnableTheNewMasksAndKeepExistingEditAndResizeMasks()
        => OnSta(() =>
        {
            using var rich = new NotificationRichTextBox();
            const int expectedMask = 1 | 2 | 4 | 8 | 0x40000 | 0x80000 | 0x200000 | 0x800000;
            Assert.Equal((nint)expectedMask, SendMessageW(rich.Handle, 0x043B, 0, 0));

            rich.RecreateNow();

            Assert.Equal((nint)expectedMask, SendMessageW(rich.Handle, 0x043B, 0, 0));
        });

    [Fact]
    public void ImeNotificationRaisesBeforeTheDefaultProcedureAndPreservesItsResult()
        => OnSta(() =>
        {
            using var rich = new NotificationRichTextBox();
            rich.ImeChange += (sender, value) =>
            {
                Assert.Same(rich, sender);
                Assert.Same(EventArgs.Empty, value);
                rich.Trace.Add("handler");
            };

            Message message = rich.Dispatch(WmImeNotify, 0x1742, 0x1234);

            Assert.Equal(["ime", "handler", "default"], rich.Trace);
            Assert.Equal((nint)0x1357, message.Result);
            Assert.Equal((nint)0x1742, message.WParam);
            Assert.Equal((nint)0x1234, message.LParam);
            Assert.Equal(WmImeNotify, rich.DefaultMessage.Msg);
            Assert.Equal(1, rich.DefaultCalls);
            Assert.False(rich.IsHandleCreated);
        });

    [Fact]
    public void AnImeHandlerExceptionPreventsTheFollowingDefaultProcedure()
        => OnSta(() =>
        {
            using var rich = new NotificationRichTextBox();
            var failure = new InvalidOperationException("ime event failure");
            rich.ImeChange += (_, _) => throw failure;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() =>
                rich.Dispatch(WmImeNotify, 0x1742, 0)));

            Assert.Equal(["ime"], rich.Trace);
            Assert.Equal(0, rich.DefaultCalls);
        });

    [Theory]
    [InlineData(WmHScroll, Horizontal, 4)]
    [InlineData(WmHScroll, Horizontal, 5)]
    [InlineData(WmVScroll, Vertical, 4)]
    [InlineData(WmVScroll, Vertical, 5)]
    public void ThumbPositionAndTrackingRaiseAfterTheDefaultProcedure(int messageId, int signal, int command)
        => OnSta(() =>
        {
            using var rich = new NotificationRichTextBox();
            Add(rich, signal, (_, value) =>
            {
                Assert.Same(EventArgs.Empty, value);
                Assert.Equal(1, rich.DefaultCalls);
                rich.Trace.Add("handler");
            });
            nint packed = unchecked((nint)(0xCAFE0000u | (uint)command));

            Message message = rich.Dispatch(messageId, packed, 0x1234);

            Assert.Equal(["default", SignalName(signal), "handler"], rich.Trace);
            Assert.Equal((nint)0x1357, message.Result);
            Assert.Equal(packed, message.WParam);
            Assert.Equal((nint)0x1234, message.LParam);
            Assert.False(rich.IsHandleCreated);
        });

    [Theory]
    [InlineData(WmHScroll)]
    [InlineData(WmVScroll)]
    public void OtherScrollbarCommandsUseOnlyTheDefaultProcedure(int messageId)
        => OnSta(() =>
        {
            using var rich = new NotificationRichTextBox();
            // Eight fixed commands, bounded both by count and the STA deadline.
            int[] commands = [0, 1, 2, 3, 6, 7, 8, 0xFFFF];
            var elapsed = Stopwatch.StartNew();
            foreach (int command in commands)
            {
                Assert.True(elapsed.Elapsed < TimeSpan.FromSeconds(3), "Scrollbar command batch timed out.");
                rich.Trace.Clear();
                int before = rich.DefaultCalls;
                Message message = rich.Dispatch(messageId, unchecked((nint)(0xCAFE0000u | (uint)command)), 0);
                Assert.Equal(["default"], rich.Trace);
                Assert.Equal(before + 1, rich.DefaultCalls);
                Assert.Equal((nint)0x1357, message.Result);
            }
            Assert.Equal(0, rich.VirtualCalls);
            Assert.False(rich.IsHandleCreated);
        });

    [Theory]
    [InlineData(WmHScroll, Horizontal)]
    [InlineData(WmVScroll, Vertical)]
    public void ScrollDecisionUsesTheLowWordAfterTheDefaultProcedureChangesTheMessage(int messageId, int signal)
        => OnSta(() =>
        {
            using var rich = new NotificationRichTextBox { ReplacementCommand = 5 };
            Message changedToThumb = rich.Dispatch(messageId, unchecked((nint)0xBCDE0000u), 0);
            Assert.Equal(["default", SignalName(signal)], rich.Trace);
            Assert.Equal(unchecked((nint)0xBCDE0005u), changedToThumb.WParam);
            rich.Trace.Clear();
            rich.ReplacementCommand = 3;

            Message changedFromThumb = rich.Dispatch(messageId, unchecked((nint)0xFEDC0005u), 0);

            Assert.Equal(["default"], rich.Trace);
            Assert.Equal(unchecked((nint)0xFEDC0003u), changedFromThumb.WParam);
            Assert.Equal(1, rich.VirtualCalls);
        });

    [Theory]
    [InlineData(WmHScroll)]
    [InlineData(WmVScroll)]
    public void ADefaultProcedureExceptionPreventsTheFollowingScrollEvent(int messageId)
        => OnSta(() =>
        {
            var failure = new InvalidOperationException("scroll default failure");
            using var rich = new NotificationRichTextBox { DefaultFailure = failure };

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => rich.Dispatch(messageId, 5, 0)));

            Assert.Equal(["default"], rich.Trace);
            Assert.Equal(0, rich.VirtualCalls);
        });

    [Theory]
    [InlineData(EnHScroll, Horizontal)]
    [InlineData(EnVScroll, Vertical)]
    public void TheRealParkingParentReflectsScrollCommandsBySourceWindow(int code, int signal)
        => OnSta(() =>
        {
            using var rich = new NotificationRichTextBox { Text = "scroll notification 文本" };
            nint handle = rich.Handle;
            nint receiver = GetParent(handle);
            Assert.NotEqual(0, receiver);
            string text = rich.Text;
            int start = rich.SelectionStart;
            int length = rich.SelectionLength;
            bool modified = rich.Modified;
            int calls = 0;
            Add(rich, signal, (sender, value) =>
            {
                Assert.Same(rich, sender);
                Assert.Same(EventArgs.Empty, value);
                calls++;
            });
            rich.Trace.Clear();

            _ = SendMessageW(receiver, 0x0111, (nint)((code << 16) | 0x3A7), handle);

            Assert.Equal(1, calls);
            Assert.Equal([SignalName(signal)], rich.Trace);
            Assert.Equal(handle, rich.Handle);
            Assert.Equal(text, rich.Text);
            Assert.Equal(start, rich.SelectionStart);
            Assert.Equal(length, rich.SelectionLength);
            Assert.Equal(modified, rich.Modified);
        });

    private static string SignalName(int signal) => signal switch
    {
        Horizontal => "horizontal",
        Vertical => "vertical",
        Ime => "ime",
        _ => throw new ArgumentOutOfRangeException(nameof(signal))
    };

    private static void Add(NotificationRichTextBox rich, int signal, EventHandler handler)
    {
        switch (signal)
        {
            case Horizontal: rich.HScroll += handler; break;
            case Vertical: rich.VScroll += handler; break;
            case Ime: rich.ImeChange += handler; break;
            default: throw new ArgumentOutOfRangeException(nameof(signal));
        }
    }

    private static void Remove(NotificationRichTextBox rich, int signal, EventHandler handler)
    {
        switch (signal)
        {
            case Horizontal: rich.HScroll -= handler; break;
            case Vertical: rich.VScroll -= handler; break;
            case Ime: rich.ImeChange -= handler; break;
            default: throw new ArgumentOutOfRangeException(nameof(signal));
        }
    }

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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "RichTextBox scroll/IME notification test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class NotificationRichTextBox : RichTextBox
    {
        internal List<string> Trace { get; } = [];
        internal bool EventsAllowed { get; set; } = true;
        internal bool InvokeBaseRaiser { get; set; } = true;
        internal int VirtualCalls { get; private set; }
        internal int DefaultCalls { get; private set; }
        internal Message DefaultMessage { get; private set; }
        internal int? ReplacementCommand { get; set; }
        internal Exception? DefaultFailure { get; set; }
        internal int? CreationCommand { get; set; }
        internal bool CreationCommandHandled { get; private set; }
        internal int CreationCommandRaises { get; private set; }
        protected override bool CanRaiseEvents => EventsAllowed;
        internal bool Command(int code) => base.OnCommand(code);
        internal void RecreateNow() => RecreateHandle();

        internal void Raise(int signal, EventArgs? arguments)
        {
            switch (signal)
            {
                case Horizontal: OnHScroll(arguments!); break;
                case Vertical: OnVScroll(arguments!); break;
                case Ime: OnImeChange(arguments!); break;
                default: throw new ArgumentOutOfRangeException(nameof(signal));
            }
        }

        internal Message Dispatch(int messageId, nint wParam, nint lParam)
        {
            Message message = Message.Create(IsHandleCreated ? Handle : 0, messageId, wParam, lParam);
            base.WndProc(ref message);
            return message;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            if (CreationCommand is int code)
            {
                int before = VirtualCalls;
                CreationCommandHandled = base.OnCommand(code);
                CreationCommandRaises = VirtualCalls - before;
            }
            base.OnHandleCreated(e);
        }

        protected override void OnHScroll(EventArgs e)
        {
            VirtualCalls++;
            Trace.Add("horizontal");
            if (InvokeBaseRaiser) { base.OnHScroll(e); }
        }

        protected override void OnVScroll(EventArgs e)
        {
            VirtualCalls++;
            Trace.Add("vertical");
            if (InvokeBaseRaiser) { base.OnVScroll(e); }
        }

        protected override void OnImeChange(EventArgs e)
        {
            VirtualCalls++;
            Trace.Add("ime");
            if (InvokeBaseRaiser) { base.OnImeChange(e); }
        }

        protected override void DefWndProc(ref Message message)
        {
            if (message.Msg is WmHScroll or WmVScroll or WmImeNotify)
            {
                DefaultCalls++;
                DefaultMessage = message;
                Trace.Add("default");
                if (DefaultFailure is not null) { ExceptionDispatchInfo.Capture(DefaultFailure).Throw(); }
                if (ReplacementCommand is int replacement)
                {
                    message.WParam = (message.WParam & ~((nint)0xFFFF)) | (nint)(ushort)replacement;
                }
                message.Result = 0x1357;
                return;
            }
            base.DefWndProc(ref message);
        }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint handle, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetParent(nint handle);
}
