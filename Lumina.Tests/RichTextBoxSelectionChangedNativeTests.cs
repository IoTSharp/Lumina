using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;
using Xunit.Abstractions;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class RichTextBoxSelectionChangedNativeTests
{
    private readonly ITestOutputHelper _output;

    public RichTextBoxSelectionChangedNativeTests(ITestOutputHelper output) => _output = output;

    private const int EmExGetSel = 0x0434;
    private const int EmExSetSel = 0x0437;
    private const int EmGetEventMask = 0x043B;
    private const int EmSelectionType = 0x0442;
    private const int EnSelChange = 0x0702;
    private const int EnmSelChange = 0x00080000;

    [Fact]
    public void IndependentEmptyRichEditRecordsTheSetSelAndExSetSelNotificationDifference()
        => OnSta(() =>
        {
            // Loading the system module is the only use of Lumina RichEdit in
            // this diagnostic. Each command runs against a separate raw native
            // RICHEDIT50W child with its own initially empty document.
            using var moduleLoader = new SelectionRichTextBox();
            _ = moduleLoader.Handle;
            using var form = new RawNotificationForm();
            _ = form.Handle;

            EmptySelectionProbe setSel = ProbeIndependentEmptySelection(form, useExtended: false);
            EmptySelectionProbe exSetSel = ProbeIndependentEmptySelection(form, useExtended: true);

            _output.WriteLine("raw EM_SETSEL(0,0): notifications={0}; before={1}; after={2}; textLength={3}; mask=0x{4:X}",
                setSel.Notifications.Length, setSel.Before, setSel.After, setSel.TextLength, setSel.EventMask);
            _output.WriteLine("raw EM_EXSETSEL(0,0): notifications={0}; before={1}; after={2}; textLength={3}; mask=0x{4:X}",
                exSetSel.Notifications.Length, exSetSel.Before, exSetSel.After, exSetSel.TextLength, exSetSel.EventMask);
            AssertEmptyProbe(setSel);
            AssertEmptyProbe(exSetSel);
            Assert.Empty(setSel.Notifications);
            Assert.Empty(exSetSel.Notifications);
        });

    [Fact]
    public void CreationAndRecreationEnableSelectionNotificationsBeforeHandleCreated()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox { Text = "selection mask" };
            List<nint> masks = [];
            rich.HandleCreated += (_, _) => masks.Add(SendMessageW(rich.Handle, EmGetEventMask, 0, 0));

            _ = rich.Handle;
            rich.RecreateNow();

            Assert.Equal(2, rich.CreatedCount);
            Assert.Equal(2, masks.Count);
            Assert.Equal(EnmSelChange, (int)masks[0] & EnmSelChange);
            Assert.Equal(EnmSelChange, (int)masks[1] & EnmSelChange);
            Assert.Equal("selection mask", rich.Text);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void NativeSelectionChangesReachTheManagedControlWithOrWithoutAForm(bool attachToForm)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new SelectionRichTextBox { Text = "abcdefgh" };
            CreateHandle(form, rich, attachToForm);
            List<Observation> events = Track(rich);

            SetNativeSelection(rich.Handle, 2, 6);

            Observation observed = SingleObservation(rich, events);
            Assert.Equal(new NativeRange(2, 6), observed.Range);
            Assert.Equal(rich.LastNativeNotification.Range, observed.Range);
            Assert.Equal(unchecked((short)rich.LastNativeNotification.SelectionType), observed.Type);
            Assert.Equal(2, rich.SelectionStart);
            Assert.Equal(4, rich.SelectionLength);
            Assert.Equal("cdef", rich.SelectedText);
            Assert.Equal("abcdefgh", rich.Text);
            Assert.Equal(1, rich.CreatedCount);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ManagedSelectRaisesOnlyTheNativeSelectionNotification(bool attachToForm)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new SelectionRichTextBox { Text = "managed selection" };
            CreateHandle(form, rich, attachToForm);
            List<Observation> events = Track(rich);

            rich.Select(3, 5);

            Assert.Equal(new NativeRange(3, 8), SingleObservation(rich, events).Range);
            Assert.NotEmpty(rich.NativeNotifications);
            Assert.Equal(new NativeRange(3, 8), rich.LastNativeNotification.Range);
            Assert.Equal(3, rich.SelectionStart);
            Assert.Equal(5, rich.SelectionLength);
            Assert.Equal("aged ", rich.SelectedText);
            Assert.Single(events);
        });

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(1, 5)]
    public void RepeatingTheNativeRangeDoesNotRaiseAnotherManagedEvent(int start, int end)
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox { Text = "abcdefgh" };
            SetNativeSelection(rich.Handle, 6, 6);
            List<Observation> events = Track(rich);

            SetNativeSelection(rich.Handle, start, end);
            Assert.Equal(new NativeRange(start, end), SingleObservation(rich, events).Range);
            SetNativeSelection(rich.Handle, start, end);
            SetNativeSelection(rich.Handle, start, end);

            Assert.Single(events);
            Assert.Equal(new NativeRange(start, end), ReadNativeSelection(rich.Handle));
            Assert.Equal("abcdefgh", rich.Text);
        });

    [Theory]
    [InlineData("A😀B中", 1, 3, "😀")]
    [InlineData("A😀B中", 3, 5, "B中")]
    [InlineData("中文éx", 0, 3, "中文é")]
    [InlineData("one\ntwo\n", 3, 7, "\ntwo")]
    [InlineData("one\ntwo\n", 8, 8, "")]
    public void NativeNotificationsPreserveUtf16AndLineEndIndices(
        string text, int start, int end, string selected)
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox { Text = text };
            _ = rich.Handle;
            List<Observation> events = Track(rich);

            SetNativeSelection(rich.Handle, start, end);

            Assert.Equal(new NativeRange(start, end), SingleObservation(rich, events).Range);
            Assert.Equal(start, rich.SelectionStart);
            Assert.Equal(end - start, rich.SelectionLength);
            Assert.Equal(selected, rich.SelectedText);
            Assert.Equal(text, rich.Text);
        });

    [Fact]
    public void SelectionNotificationsKeepFullIndicesBeyondTheSixteenBitBoundary()
        => OnSta(() =>
        {
            string text = new string('a', 70000) + "中😀z";
            using var rich = new SelectionRichTextBox { Text = text };
            _ = rich.Handle;
            List<Observation> events = Track(rich);

            SetNativeSelection(rich.Handle, 70000, 70003);

            Assert.Equal(new NativeRange(70000, 70003), SingleObservation(rich, events).Range);
            Assert.Equal(new NativeRange(70000, 70003), rich.LastNativeNotification.Range);
            Assert.Equal(70000, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.Equal("中😀", rich.SelectedText);
            Assert.Equal(text, rich.Text);
        });

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 1)]
    [InlineData(0, 4)]
    public void EventSeesTheNativeSelectionTypeAndLatestRange(int start, int end)
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox { Text = "type changes" };
            SetNativeSelection(rich.Handle, 8, 8);
            List<Observation> events = Track(rich);

            SetNativeSelection(rich.Handle, start, end);

            Observation observed = SingleObservation(rich, events);
            Assert.Equal(new NativeRange(start, end), observed.Range);
            Assert.Equal(unchecked((short)rich.LastNativeNotification.SelectionType), observed.Type);
            Assert.Equal((short)(int)SendMessageW(rich.Handle, EmSelectionType, 0, 0), observed.Type);
        });

    [Fact]
    public void ReadingTheSelectionAfterANativeChangeDoesNotSynthesizeExtraEvents()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox { Text = "latest native state" };
            _ = rich.Handle;
            List<Observation> events = Track(rich);
            SetNativeSelection(rich.Handle, 7, 13);
            _ = SingleObservation(rich, events);

            Assert.Equal(7, rich.SelectionStart);
            Assert.Equal(6, rich.SelectionLength);
            Assert.Equal("native", rich.SelectedText);
            Assert.Equal(7, ((TextBoxBase)rich).SelectionStart);
            Assert.Equal(6, ((TextBoxBase)rich).SelectionLength);
            _ = rich.SelectionType;
            Assert.Equal(new NativeRange(7, 13), ReadNativeSelection(rich.Handle));

            Assert.Single(events);
        });

    [Fact]
    public void NativeReplacementReportsItsNewCaretWithoutChangingTheNotificationIntoATextSetter()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox { Text = "abcdef" };
            SetNativeSelection(rich.Handle, 2, 4);
            rich.ClearUndo();
            rich.Modified = false;
            List<Observation> events = Track(rich);
            nint replacement = Marshal.StringToHGlobalUni("XYZ");
            try { _ = SendMessageW(rich.Handle, 0x00C2, 1, replacement); }
            finally { Marshal.FreeHGlobal(replacement); }

            Assert.NotEmpty(events);
            AssertObservation(rich, events[^1]);
            Assert.Equal(new NativeRange(5, 5), events[^1].Range);
            Assert.Equal(new NativeRange(5, 5), ReadNativeSelection(rich.Handle));
            Assert.Equal("abXYZef", rich.Text);
            Assert.True(rich.Modified);
            Assert.True(rich.CanUndo);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void SelectionOnlyChangesPreserveDocumentFormattingModifiedAndUndo(bool modified)
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox
            {
                Rtf = "{\\rtf1\\ansi plain \\b bold\\b0\\par second}"
            };
            SetNativeSelection(rich.Handle, 0, 1);
            rich.ClearUndo();
            rich.Modified = modified;
            string? rtf = rich.Rtf;
            string text = rich.Text;
            List<Observation> events = Track(rich);

            SetNativeSelection(rich.Handle, 6, 10);

            Assert.Equal(new NativeRange(6, 10), SingleObservation(rich, events).Range);
            Assert.Equal("bold", rich.SelectedText);
            using Font selected = Assert.IsType<Font>(rich.SelectionFont);
            Assert.True(selected.Bold);
            Assert.Equal(rtf, rich.Rtf);
            Assert.Equal(text, rich.Text);
            Assert.Equal(modified, rich.Modified);
            Assert.False(rich.CanUndo);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void RecreatedRtfDocumentRestoresSelectionAndContinuesNativeNotifications(bool modified)
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox
            {
                Rtf = "{\\rtf1\\ansi plain \\b bold\\b0\\par second}"
            };
            SetNativeSelection(rich.Handle, 6, 10);
            rich.Modified = modified;
            string text = rich.Text;
            List<Observation> events = Track(rich);

            rich.RecreateNow();

            Assert.Equal(2, rich.CreatedCount);
            Assert.Equal(new NativeRange(6, 10), ReadNativeSelection(rich.Handle));
            Assert.Equal(modified, rich.Modified);
            Assert.Equal(text, rich.Text);
            Assert.False(rich.CanUndo);
            Assert.NotEmpty(events);
            AssertObservation(rich, events[^1]);
            Assert.Equal(new NativeRange(6, 10), events[^1].Range);
            using (Font selected = Assert.IsType<Font>(rich.SelectionFont)) { Assert.True(selected.Bold); }
            events.Clear();
            rich.NativeNotifications.Clear();

            SetNativeSelection(rich.Handle, 1, 3);

            Assert.Equal(new NativeRange(1, 3), SingleObservation(rich, events).Range);
            Assert.Equal(modified, rich.Modified);
            Assert.Equal(text, rich.Text);
        });

    [Fact]
    public void RecreatingAnEmptyDocumentDoesNotSynthesizeASelectionNotification()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox();
            _ = rich.Handle;
            // The independent raw RichEdit diagnostic confirms that neither
            // EM_SETSEL nor EM_EXSETSEL notifies for this unchanged empty range.
            SetNativeSelection(rich.Handle, 0, 0);
            List<Observation> events = Track(rich);

            rich.RecreateNow();

            Assert.Equal(2, rich.CreatedCount);
            Assert.Empty(events);
            Assert.Empty(rich.NativeNotifications);
            Assert.Equal(new NativeRange(0, 0), ReadNativeSelection(rich.Handle));
            Assert.Equal(string.Empty, rich.Text);
        });

    [Fact]
    public void ReparentedRichEditStillReceivesNativeSelectionFromItsRetainedParkingReceiver()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new SelectionRichTextBox { Text = "retained selection receiver" };
            nint handle = rich.Handle;
            nint parking = GetParent(handle);
            Assert.NotEqual((nint)0, parking);
            _ = form.Handle;
            form.Controls.Add(rich);
            Assert.Equal(handle, rich.Handle);
            Assert.Equal(form.Handle, GetParent(handle));
            Assert.NotEqual(parking, GetParent(handle));
            List<Observation> events = Track(rich);

            SetNativeSelection(handle, 9, 18);

            Assert.Equal(new NativeRange(9, 18), SingleObservation(rich, events).Range);
            Assert.Equal("selection", rich.SelectedText);
            Assert.Equal(handle, rich.Handle);
            Assert.Equal(form.Handle, GetParent(handle));
            Assert.True(IsWindow(parking));
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ReadOnlyControlsStillReportNativeSelectionChanges(bool attachToForm)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new SelectionRichTextBox { Text = "read only selection", ReadOnly = true };
            CreateHandle(form, rich, attachToForm);
            rich.Modified = false;
            List<Observation> events = Track(rich);

            SetNativeSelection(rich.Handle, 5, 9);

            Assert.Equal(new NativeRange(5, 9), SingleObservation(rich, events).Range);
            Assert.Equal("only", rich.SelectedText);
            Assert.True(rich.ReadOnly);
            Assert.False(rich.Modified);
            Assert.Equal("read only selection", rich.Text);
        });

    [Fact]
    public void ChangingOneControlSelectionDoesNotNotifyAnotherControl()
        => OnSta(() =>
        {
            using var first = new SelectionRichTextBox { Text = "first text" };
            using var second = new SelectionRichTextBox { Text = "second text" };
            _ = first.Handle;
            _ = second.Handle;
            List<Observation> firstEvents = Track(first);
            List<Observation> secondEvents = Track(second);

            SetNativeSelection(first.Handle, 1, 5);
            Assert.Equal(new NativeRange(1, 5), SingleObservation(first, firstEvents).Range);
            Assert.Empty(secondEvents);
            SetNativeSelection(second.Handle, 2, 7);
            Assert.Single(firstEvents);
            Assert.Equal(new NativeRange(2, 7), SingleObservation(second, secondEvents).Range);
            Assert.Equal(new NativeRange(1, 5), ReadNativeSelection(first.Handle));
            Assert.Equal(new NativeRange(2, 7), ReadNativeSelection(second.Handle));
        });

    private static List<Observation> Track(SelectionRichTextBox rich)
    {
        rich.NativeNotifications.Clear();
        List<Observation> events = [];
        rich.SelectionChanged += (sender, arguments) =>
        {
            NativeSelectionChange? notification = rich.NativeNotifications.Count > 0
                ? rich.LastNativeNotification : null;
            NativeRange range = ReadNativeSelection(rich.Handle);
            short type = (short)(int)SendMessageW(rich.Handle, EmSelectionType, 0, 0);
            events.Add(new Observation(range, type, notification, rich.Handle, sender, arguments));
        };
        return events;
    }

    private static Observation SingleObservation(SelectionRichTextBox rich, List<Observation> events)
    {
        Observation observation = Assert.Single(events);
        AssertObservation(rich, observation);
        return observation;
    }

    private static void AssertObservation(SelectionRichTextBox rich, Observation observation)
    {
        Assert.Same(rich, observation.Sender);
        Assert.Same(EventArgs.Empty, observation.Arguments);
        Assert.True(observation.Notification.HasValue, "SelectionChanged must originate from an EN_SELCHANGE notification.");
        NativeSelectionChange notification = observation.Notification.Value;
        Assert.Equal(observation.Handle, notification.Header.Window);
        Assert.Equal(EnSelChange, notification.Header.Code);
        Assert.Equal(notification.Range, observation.Range);
        Assert.Equal(unchecked((short)notification.SelectionType), observation.Type);
    }

    private static void CreateHandle(Form form, SelectionRichTextBox rich, bool attachToForm)
    {
        if (attachToForm)
        {
            _ = form.Handle;
            form.Controls.Add(rich);
        }
        _ = rich.Handle;
    }

    private static EmptySelectionProbe ProbeIndependentEmptySelection(RawNotificationForm form, bool useExtended)
    {
        nint handle = CreateWindowExW(0, "RICHEDIT50W", string.Empty,
            0x40000004 /* WS_CHILD | ES_MULTILINE */, 0, 0, 240, 120, form.Handle, 0, 0, 0);
        Assert.NotEqual((nint)0, handle);
        try
        {
            form.Source = handle;
            _ = SendMessageW(handle, 0x0445 /* EM_SETEVENTMASK */, 0, EnmSelChange);
            NativeRange before = ReadNativeSelection(handle);
            form.Notifications.Clear();
            if (useExtended) { SetNativeSelection(handle, 0, 0); }
            else { _ = SendMessageW(handle, 0x00B1 /* EM_SETSEL */, 0, 0); }
            return new EmptySelectionProbe(handle, before, ReadNativeSelection(handle),
                (int)SendMessageW(handle, 0x000E /* WM_GETTEXTLENGTH */, 0, 0),
                (int)SendMessageW(handle, EmGetEventMask, 0, 0), form.Notifications.ToArray());
        }
        finally
        {
            try { Assert.True(DestroyWindow(handle)); }
            finally { form.Source = 0; }
        }
    }

    private static void AssertEmptyProbe(EmptySelectionProbe probe)
    {
        Assert.Equal(new NativeRange(0, 0), probe.Before);
        Assert.Equal(new NativeRange(0, 0), probe.After);
        Assert.Equal(0, probe.TextLength);
        Assert.Equal(EnmSelChange, probe.EventMask);
        Assert.All(probe.Notifications, notification =>
        {
            Assert.Equal(probe.Handle, notification.Header.Window);
            Assert.Equal(EnSelChange, notification.Header.Code);
            Assert.Equal(new NativeRange(0, 0), notification.Range);
        });
    }

    private static void SetNativeSelection(nint handle, int start, int end)
    {
        nint buffer = Marshal.AllocHGlobal(8);
        try
        {
            Marshal.WriteInt32(buffer, 0, start);
            Marshal.WriteInt32(buffer, 4, end);
            _ = SendMessageW(handle, EmExSetSel, 0, buffer);
        }
        finally { Marshal.FreeHGlobal(buffer); }
    }

    private static NativeRange ReadNativeSelection(nint handle)
    {
        nint buffer = Marshal.AllocHGlobal(8);
        try
        {
            Marshal.WriteInt32(buffer, 0, -123);
            Marshal.WriteInt32(buffer, 4, -456);
            _ = SendMessageW(handle, EmExGetSel, 0, buffer);
            return new NativeRange(Marshal.ReadInt32(buffer, 0), Marshal.ReadInt32(buffer, 4));
        }
        finally { Marshal.FreeHGlobal(buffer); }
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "RichTextBox SelectionChanged native test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class SelectionRichTextBox : RichTextBox
    {
        internal List<NativeSelectionChange> NativeNotifications { get; } = [];
        internal NativeSelectionChange LastNativeNotification => NativeNotifications[^1];
        internal int CreatedCount { get; private set; }
        internal void RecreateNow() => RecreateHandle();

        protected override bool OnNotify(int notificationCode, nint lParam)
        {
            if (notificationCode == EnSelChange)
            {
                NativeNotifications.Add(Marshal.PtrToStructure<NativeSelectionChange>(lParam));
            }
            return base.OnNotify(notificationCode, lParam);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            CreatedCount++;
        }
    }

    private sealed class RawNotificationForm : Form
    {
        internal nint Source { get; set; }
        internal List<NativeSelectionChange> Notifications { get; } = [];

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x004E /* WM_NOTIFY */ && m.LParam != 0)
            {
                NativeHeader header = Marshal.PtrToStructure<NativeHeader>(m.LParam);
                if (header.Window == Source && header.Code == EnSelChange)
                {
                    Notifications.Add(Marshal.PtrToStructure<NativeSelectionChange>(m.LParam));
                }
            }
            base.WndProc(ref m);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct NativeHeader
    {
        internal nint Window;
        internal nuint Id;
        internal int Code;
    }

    [StructLayout(LayoutKind.Sequential)]
    private readonly record struct NativeRange(int Start, int End);

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    private struct NativeSelectionChange
    {
        internal NativeHeader Header;
        internal NativeRange Range;
        internal ushort SelectionType;
    }

    private readonly record struct Observation(NativeRange Range, short Type,
        NativeSelectionChange? Notification, nint Handle, object? Sender, EventArgs? Arguments);

    private readonly record struct EmptySelectionProbe(nint Handle, NativeRange Before,
        NativeRange After, int TextLength, int EventMask, NativeSelectionChange[] Notifications);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint handle, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetParent(nint handle);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint CreateWindowExW(uint exStyle, string className, string text,
        uint style, int x, int y, int width, int height, nint parent, nint menu, nint instance, nint parameter);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint handle);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint handle);
}
