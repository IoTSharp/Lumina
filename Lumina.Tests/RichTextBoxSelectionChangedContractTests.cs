using System.ComponentModel;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class RichTextBoxSelectionChangedContractTests
{
    private const int EnSelectionChange = 0x0702;
    private const int EmSetEventMask = 0x0445;
    private const int EmGetImeCompMode = 0x047A;
    private const int EmSetSel = 0x00B1;
    private const int WmGetTextLength = 0x000E;
    private const int WmKillFocus = 0x0008;
    private const int WmSetFocus = 0x0007;

    [Fact]
    public void EventIsDeclaredWithTheStandardHandlerAndBehaviorCategory()
    {
        EventInfo notification = typeof(RichTextBox).GetEvent(nameof(RichTextBox.SelectionChanged),
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)!;
        Assert.NotNull(notification);
        Assert.Equal(typeof(EventHandler), notification.EventHandlerType);
        Assert.True(notification.AddMethod!.IsPublic);
        Assert.False(notification.AddMethod.IsVirtual);
        Assert.True(notification.RemoveMethod!.IsPublic);
        Assert.False(notification.RemoveMethod.IsVirtual);
        Assert.Equal("Behavior", notification.GetCustomAttribute<CategoryAttribute>()!.Category);
        Assert.Null(typeof(TextBoxBase).GetEvent(nameof(RichTextBox.SelectionChanged),
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly));
    }

    [Fact]
    public void RaiserIsTheStandardProtectedVirtualVoidMember()
    {
        MethodInfo raise = typeof(RichTextBox).GetMethod("OnSelectionChanged",
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
    public void RaiserWithoutSubscribersDoesNotCreateAWindow()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox { FailCreation = true };
            rich.Raise(EventArgs.Empty);
            Assert.Equal(1, rich.VirtualCalls);
            Assert.Same(EventArgs.Empty, rich.LastArguments);
            Assert.False(rich.IsHandleCreated);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void RaiserPreservesSenderAndSuppliedArgumentIdentity(bool useNull)
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox();
            EventArgs? supplied = useNull ? null : new EventArgs();
            object? sender = null;
            EventArgs? received = EventArgs.Empty;
            rich.SelectionChanged += (source, arguments) => { sender = source; received = arguments; };

            rich.Raise(supplied);

            Assert.Same(rich, sender);
            Assert.Same(supplied, received);
            Assert.Same(supplied, rich.LastArguments);
            Assert.False(rich.IsHandleCreated);
        });

    [Fact]
    public void DuplicateSubscriptionsAndRemovalPreserveDelegateOrder()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox();
            List<string> calls = [];
            EventHandler first = (_, _) => calls.Add("first");
            EventHandler second = (_, _) => calls.Add("second");
            rich.SelectionChanged += first;
            rich.SelectionChanged += second;
            rich.SelectionChanged += first;
            rich.Raise(EventArgs.Empty);
            Assert.Equal(["first", "second", "first"], calls);
            calls.Clear();
            rich.SelectionChanged -= first;
            rich.Raise(EventArgs.Empty);
            Assert.Equal(["first", "second"], calls);
            calls.Clear();
            rich.SelectionChanged -= first;
            rich.SelectionChanged -= second;
            rich.Raise(EventArgs.Empty);
            Assert.Empty(calls);
        });

    [Fact]
    public void CanRaiseEventsSuppressesTheSlotWhileTheVirtualRaiserStillRuns()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox { EventsAllowed = false };
            int calls = 0;
            rich.SelectionChanged += (_, _) => calls++;
            rich.Raise(EventArgs.Empty);
            Assert.Equal(0, calls);
            Assert.Equal(1, rich.VirtualCalls);
            rich.EventsAllowed = true;
            rich.Raise(EventArgs.Empty);
            Assert.Equal(1, calls);
            Assert.Equal(2, rich.VirtualCalls);
        });

    [Fact]
    public void OverrideCanOmitTheBaseRaiser()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox { InvokeBaseRaiser = false };
            int calls = 0;
            rich.SelectionChanged += (_, _) => calls++;
            rich.Raise(EventArgs.Empty);
            Assert.Equal(1, rich.VirtualCalls);
            Assert.Equal(0, calls);
            Assert.False(rich.IsHandleCreated);
        });

    [Fact]
    public void RaiserRetainsHandlerExceptionIdentityAndStopsTheCurrentInvocation()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox();
            var failure = new InvalidOperationException("selection handler");
            int laterCalls = 0;
            EventHandler throwing = (_, _) => throw failure;
            rich.SelectionChanged += throwing;
            rich.SelectionChanged += (_, _) => laterCalls++;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => rich.Raise(EventArgs.Empty)));
            Assert.Equal(0, laterCalls);
            rich.SelectionChanged -= throwing;
            rich.Raise(EventArgs.Empty);
            Assert.Equal(1, laterCalls);
        });

    [Fact]
    public void DirectRaiserAllowsOneFiniteReentryWithoutCreatingAWindow()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox();
            int calls = 0;
            rich.SelectionChanged += (_, _) =>
            {
                calls++;
                if (calls == 1) { rich.Raise(EventArgs.Empty); }
            };

            rich.Raise(EventArgs.Empty);

            Assert.Equal(2, calls);
            Assert.Equal(2, rich.VirtualCalls);
            Assert.False(rich.IsHandleCreated);
        });

    [Fact]
    public void NativeSelectionPayloadHasTheSdkPackingAndOffsets()
    {
        int headerSize = IntPtr.Size == 8 ? 24 : 12;
        Assert.Equal(headerSize, Marshal.SizeOf<NativeHeader>());
        Assert.Equal(headerSize, Marshal.OffsetOf<NativeSelection>(nameof(NativeSelection.Start)).ToInt32());
        Assert.Equal(headerSize + 4, Marshal.OffsetOf<NativeSelection>(nameof(NativeSelection.End)).ToInt32());
        Assert.Equal(headerSize + 8, Marshal.OffsetOf<NativeSelection>(nameof(NativeSelection.Type)).ToInt32());
        Assert.Equal(headerSize + 12, Marshal.SizeOf<NativeSelection>());
    }

    [Fact]
    public void TheInitialNegativeSentinelIsAlreadyCachedIncludingTheSignedType()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox { FailCreation = true };
            Assert.True(rich.NotifySelection(-1, -1, ushort.MaxValue));
            Assert.Equal(0, rich.VirtualCalls);
            Assert.False(rich.IsHandleCreated);
        });

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(1, 7, 1)]
    [InlineData(-5, -2, 65535)]
    [InlineData(7, -2, 32768)]
    [InlineData(int.MinValue, int.MaxValue, 32767)]
    public void NotificationUsesRawRangesAndTypeWithoutNormalizationOrVirtualTextQueries(
        int start, int end, int selectionType)
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox { FailCreation = true, FailTextGetters = true };
            int calls = 0;
            rich.SelectionChanged += (sender, arguments) =>
            {
                Assert.Same(rich, sender);
                Assert.Same(EventArgs.Empty, arguments);
                calls++;
            };

            Assert.True(rich.NotifySelection(start, end, (ushort)selectionType));
            Assert.True(rich.NotifySelection(start, end, (ushort)selectionType));

            Assert.Equal(1, calls);
            Assert.Equal(1, rich.VirtualCalls);
            Assert.False(rich.IsHandleCreated);
            Assert.Equal(0, rich.MaxLengthReads);
        });

    [Fact]
    public void EachOfTheThreeNativeValuesParticipatesInDuplicateSuppression()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox();
            Assert.True(rich.NotifySelection(1, 2, 1));
            Assert.Equal(1, rich.VirtualCalls);
            Assert.True(rich.NotifySelection(1, 2, 1));
            Assert.Equal(1, rich.VirtualCalls);
            Assert.True(rich.NotifySelection(2, 2, 1));
            Assert.Equal(2, rich.VirtualCalls);
            Assert.True(rich.NotifySelection(2, 3, 1));
            Assert.Equal(3, rich.VirtualCalls);
            Assert.True(rich.NotifySelection(2, 3, 0x8001));
            Assert.Equal(4, rich.VirtualCalls);
            Assert.True(rich.NotifySelection(2, 3, 0x8001));
            Assert.Equal(4, rich.VirtualCalls);
            Assert.True(rich.NotifySelection(1, 2, 1));
            Assert.Equal(5, rich.VirtualCalls);
        });

    [Fact]
    public void ReceivingTheNotificationDoesNotModifyItsNativeBuffer()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox();
            NativeSelection request = NewSelection(-10, int.MaxValue, 0xF123);
            WithSelection(request, pointer =>
            {
                byte[] before = ReadBytes(pointer);
                Assert.True(rich.Notify(EnSelectionChange, pointer));
                Assert.Equal(before, ReadBytes(pointer));
            });
            Assert.Equal(1, rich.VirtualCalls);
        });

    [Fact]
    public void CacheIsUpdatedBeforeCallingTheVirtualRaiser()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox();
            int calls = 0;
            rich.SelectionChanged += (_, _) =>
            {
                calls++;
                if (calls == 1)
                {
                    Assert.True(rich.NotifySelection(1, 4, 1));
                    Assert.Equal(1, calls);
                    Assert.True(rich.NotifySelection(2, 4, 1));
                }
            };

            Assert.True(rich.NotifySelection(1, 4, 1));
            Assert.Equal(2, calls);
            Assert.Equal(2, rich.VirtualCalls);
            Assert.True(rich.NotifySelection(2, 4, 1));
            Assert.Equal(2, calls);
        });

    [Fact]
    public void HandlerFailureDoesNotRollBackTheAlreadyAcceptedSelection()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox();
            var failure = new InvalidOperationException("native selection handler");
            EventHandler throwing = (_, _) => throw failure;
            rich.SelectionChanged += throwing;
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => rich.NotifySelection(3, 9, 1)));
            rich.SelectionChanged -= throwing;
            int calls = 0;
            rich.SelectionChanged += (_, _) => calls++;

            Assert.True(rich.NotifySelection(3, 9, 1));
            Assert.Equal(0, calls);
            Assert.Equal(1, rich.VirtualCalls);
            Assert.True(rich.NotifySelection(3, 9, 5));
            Assert.Equal(1, calls);
        });

    [Fact]
    public void SuppressedComponentEventsStillAdvanceTheNativeSelectionCache()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox { EventsAllowed = false };
            int calls = 0;
            rich.SelectionChanged += (_, _) => calls++;
            Assert.True(rich.NotifySelection(1, 3, 1));
            rich.EventsAllowed = true;
            Assert.True(rich.NotifySelection(1, 3, 1));
            Assert.Equal(0, calls);
            Assert.Equal(1, rich.VirtualCalls);
            Assert.True(rich.NotifySelection(1, 4, 1));
            Assert.Equal(1, calls);
        });

    [Fact]
    public void AnOverrideThatOmitsBaseStillAdvancesTheNativeSelectionCache()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox { InvokeBaseRaiser = false };
            int calls = 0;
            rich.SelectionChanged += (_, _) => calls++;
            Assert.True(rich.NotifySelection(1, 3, 1));
            rich.InvokeBaseRaiser = true;
            Assert.True(rich.NotifySelection(1, 3, 1));
            Assert.Equal(0, calls);
            Assert.Equal(1, rich.VirtualCalls);
            Assert.True(rich.NotifySelection(1, 4, 1));
            Assert.Equal(1, calls);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void HandleCreationResetsTheCacheBeforeInstallingTheNativeEventMask(bool recreate)
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox();
            if (recreate) { _ = rich.Handle; }
            Assert.True(rich.NotifySelection(91, 92, 1));
            rich.CheckSentinelDuringEventMask = true;

            if (recreate) { rich.RecreateNow(); }
            else { _ = rich.Handle; }

            Assert.Equal(1, rich.SentinelChecks);
            Assert.True(rich.SentinelHandled);
            Assert.Equal(0, rich.SentinelVirtualCallDelta);
        });

    [Fact]
    public void UnrelatedNotificationsDelegateWithoutReadingASelectionPayload()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox { FailCreation = true };
            Assert.False(rich.Notify(0x00771122, 0));
            Assert.Equal(0, rich.VirtualCalls);
            Assert.False(rich.IsHandleCreated);
        });

    [Fact]
    public void ImeGetterFailurePrecedesCacheAcceptanceAndTheSamePayloadCanBeRetried()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox();
            var failure = new InvalidOperationException("IME getter");
            rich.ImeGetterFailure = failure;
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => rich.NotifySelection(2, 4, 1)));
            Assert.Equal(0, rich.VirtualCalls);
            rich.ImeGetterFailure = null;
            Assert.True(rich.NotifySelection(2, 4, 1));
            Assert.Equal(1, rich.VirtualCalls);
        });

    [Theory]
    [InlineData(ImeMode.Hangul)]
    [InlineData(ImeMode.HangulFull)]
    public void OpenHangulCompositionAtTheRawLimitRestoresFocusAndPostsTheLastCharacterBeforeTheEvent(
        ImeMode mode)
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox();
            rich.BeginImeCapture(mode, compositionMode: 2, rawLength: 4, maxLength: 4);
            rich.FailTextGetters = true;
            bool eventSawPost = false;
            NativeMessage posted = default;
            int calls = 0;
            rich.SelectionChanged += (_, _) =>
            {
                calls++;
                eventSawPost = PeekMessageW(out posted, rich.Handle, EmSetSel, EmSetSel, 1);
            };

            Assert.True(rich.NotifySelection(3, 3, 0));

            Assert.Equal([EmGetImeCompMode, WmGetTextLength, WmKillFocus, WmSetFocus], rich.CapturedMessages);
            Assert.Equal(1, rich.MaxLengthReads);
            Assert.Equal(1, calls);
            Assert.True(eventSawPost);
            Assert.Equal(rich.Handle, posted.Window);
            Assert.Equal((uint)EmSetSel, posted.Message);
            Assert.Equal((nuint)2, posted.WParam);
            Assert.Equal((nint)3, posted.LParam);
            Assert.False(PeekMessageW(out _, rich.Handle, EmSetSel, EmSetSel, 1));
        });

    [Theory]
    [InlineData(ImeMode.NoControl, 1, 4, 4, 3, 3, 0, 0, 0)]
    [InlineData(ImeMode.On, 1, 4, 4, 3, 3, 0, 0, 0)]
    [InlineData(ImeMode.Hangul, 0, 4, 4, 3, 3, 1, 0, 0)]
    [InlineData(ImeMode.HangulFull, 0, 4, 4, 3, 3, 1, 0, 0)]
    [InlineData(ImeMode.Hangul, 1, 4, 5, 3, 3, 1, 1, 1)]
    [InlineData(ImeMode.HangulFull, -1, 4, 4, 1, 2, 1, 1, 0)]
    public void HangulGuardsAvoidFocusAndPostingUntilAllFixedSourceConditionsHold(
        ImeMode mode, int compositionMode, int rawLength, int maxLength, int start, int end,
        int expectedCompositionQueries, int expectedLengthQueries, int expectedLimitQueries)
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox();
            rich.BeginImeCapture(mode, compositionMode, rawLength, maxLength);
            rich.FailTextGetters = true;

            Assert.True(rich.NotifySelection(start, end, 0));

            Assert.Equal(expectedCompositionQueries, rich.CapturedMessages.Count(value => value == EmGetImeCompMode));
            Assert.Equal(expectedLengthQueries, rich.CapturedMessages.Count(value => value == WmGetTextLength));
            Assert.Equal(expectedLimitQueries, rich.MaxLengthReads);
            Assert.DoesNotContain(WmKillFocus, rich.CapturedMessages);
            Assert.DoesNotContain(WmSetFocus, rich.CapturedMessages);
            Assert.False(PeekMessageW(out _, rich.Handle, EmSetSel, EmSetSel, 1));
        });

    [Fact]
    public void HangulCompositionIsProcessedBeforeSuppressingADuplicateSelection()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox();
            rich.BeginImeCapture(ImeMode.Hangul, compositionMode: 1, rawLength: 4, maxLength: 4);
            int calls = 0;
            rich.SelectionChanged += (_, _) => calls++;

            Assert.True(rich.NotifySelection(4, 4, 0));
            Assert.True(PeekMessageW(out NativeMessage first, rich.Handle, EmSetSel, EmSetSel, 1));
            Assert.True(rich.NotifySelection(4, 4, 0));
            Assert.True(PeekMessageW(out NativeMessage second, rich.Handle, EmSetSel, EmSetSel, 1));

            Assert.Equal(1, calls);
            Assert.Equal(2, rich.MaxLengthReads);
            Assert.Equal([EmGetImeCompMode, WmGetTextLength, WmKillFocus, WmSetFocus,
                EmGetImeCompMode, WmGetTextLength, WmKillFocus, WmSetFocus], rich.CapturedMessages);
            Assert.Equal((nuint)3, first.WParam);
            Assert.Equal((nint)4, first.LParam);
            Assert.Equal(first.WParam, second.WParam);
            Assert.Equal(first.LParam, second.LParam);
            Assert.False(PeekMessageW(out _, rich.Handle, EmSetSel, EmSetSel, 1));
        });

    private static NativeSelection NewSelection(int start, int end, ushort type) => new()
    {
        Header = new NativeHeader { Window = (nint)0x1234, Id = (nuint)0x5678, Code = EnSelectionChange },
        Start = start,
        End = end,
        Type = type
    };

    private static byte[] ReadBytes(nint pointer)
    {
        byte[] bytes = new byte[Marshal.SizeOf<NativeSelection>()];
        Marshal.Copy(pointer, bytes, 0, bytes.Length);
        return bytes;
    }

    private static void WithSelection(NativeSelection request, Action<nint> action)
    {
        nint pointer = Marshal.AllocHGlobal(Marshal.SizeOf<NativeSelection>());
        try
        {
            Marshal.Copy(new byte[Marshal.SizeOf<NativeSelection>()], 0, pointer, Marshal.SizeOf<NativeSelection>());
            Marshal.StructureToPtr(request, pointer, false);
            action(pointer);
        }
        finally { Marshal.FreeHGlobal(pointer); }
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "RichTextBox SelectionChanged contract test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class SelectionRichTextBox : RichTextBox
    {
        internal bool EventsAllowed { get; set; } = true;
        internal bool InvokeBaseRaiser { get; set; } = true;
        internal bool FailCreation { get; set; }
        internal bool FailTextGetters { get; set; }
        internal int VirtualCalls { get; private set; }
        internal EventArgs? LastArguments { get; private set; }
        internal Exception? ImeGetterFailure { get; set; }
        internal ImeMode CurrentMode { get; set; } = ImeMode.NoControl;
        internal bool CheckSentinelDuringEventMask { get; set; }
        internal int SentinelChecks { get; private set; }
        internal bool SentinelHandled { get; private set; }
        internal int SentinelVirtualCallDelta { get; private set; }
        internal int MaxLengthReads { get; private set; }
        internal List<int> CapturedMessages { get; } = [];
        private bool CaptureIme { get; set; }
        private int CompositionMode { get; set; }
        private int RawLength { get; set; }
        private int? CapturedMaxLength { get; set; }

        protected override bool CanRaiseEvents => EventsAllowed;
        protected override ImeMode ImeModeBase
        {
            get => ImeGetterFailure is not null ? throw ImeGetterFailure : CurrentMode;
            set => CurrentMode = value;
        }

        public override string Text
        {
            get => FailTextGetters ? throw new InvalidOperationException("Selection notification must not read Text.") : base.Text;
            set => base.Text = value;
        }

        public override int TextLength
            => FailTextGetters ? throw new InvalidOperationException("Selection notification must not read TextLength.") : base.TextLength;

        public override int SelectionLength
        {
            get => FailTextGetters ? throw new InvalidOperationException("Selection notification must not read SelectionLength.") : base.SelectionLength;
            set => base.SelectionLength = value;
        }

        public override string SelectedText
        {
            get => FailTextGetters ? throw new InvalidOperationException("Selection notification must not read SelectedText.") : base.SelectedText;
            set => base.SelectedText = value;
        }

        public override int MaxLength
        {
            get
            {
                if (CaptureIme) { MaxLengthReads++; }
                return CapturedMaxLength ?? base.MaxLength;
            }
            set => base.MaxLength = value;
        }

        protected override CreateParams CreateParams
            => FailCreation ? throw new InvalidOperationException("Selection notification must not create a window.") : base.CreateParams;

        internal void Raise(EventArgs? arguments) => OnSelectionChanged(arguments!);
        internal bool Notify(int code, nint payload) => base.OnNotify(code, payload);
        internal bool NotifySelection(int start, int end, ushort type)
        {
            bool handled = false;
            WithSelection(NewSelection(start, end, type), pointer => handled = Notify(EnSelectionChange, pointer));
            return handled;
        }
        internal void RecreateNow() => RecreateHandle();

        internal void BeginImeCapture(ImeMode mode, int compositionMode, int rawLength, int maxLength)
        {
            _ = Handle;
            CurrentMode = mode;
            CompositionMode = compositionMode;
            RawLength = rawLength;
            CapturedMaxLength = maxLength;
            MaxLengthReads = 0;
            CapturedMessages.Clear();
            CaptureIme = true;
        }

        protected override void OnSelectionChanged(EventArgs e)
        {
            VirtualCalls++;
            LastArguments = e;
            if (InvokeBaseRaiser) { base.OnSelectionChanged(e); }
        }

        protected override void WndProc(ref Message message)
        {
            if (CheckSentinelDuringEventMask && message.Msg == EmSetEventMask)
            {
                SentinelChecks++;
                int before = VirtualCalls;
                SentinelHandled = NotifySelection(-1, -1, ushort.MaxValue);
                SentinelVirtualCallDelta = VirtualCalls - before;
            }
            if (CaptureIme && message.Msg is EmGetImeCompMode or WmGetTextLength or WmKillFocus or WmSetFocus)
            {
                CapturedMessages.Add(message.Msg);
                message.Result = message.Msg switch
                {
                    EmGetImeCompMode => CompositionMode,
                    WmGetTextLength => RawLength,
                    _ => 0
                };
                return;
            }
            base.WndProc(ref message);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct NativeHeader
    {
        internal nint Window;
        internal nuint Id;
        internal uint Code;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    private struct NativeSelection
    {
        internal NativeHeader Header;
        internal int Start;
        internal int End;
        internal ushort Type;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct NativeMessage
    {
        internal nint Window;
        internal uint Message;
        internal nuint WParam;
        internal nint LParam;
        internal uint Time;
        internal int X;
        internal int Y;
        internal uint Private;
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool PeekMessageW(out NativeMessage message, nint window, uint first, uint last, uint remove);
}
